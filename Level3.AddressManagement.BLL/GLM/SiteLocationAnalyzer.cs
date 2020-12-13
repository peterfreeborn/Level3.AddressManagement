using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;
using Level3.AddressManagement.Model.SearchLocationService;
using Level3.AddressManagement.Model.LocationService;


namespace Level3.AddressManagement.BLL
{
    public class SiteLocationAnalyzer
    {

        // Private Members
        private List<SiteLocationV2> _lstSiteLocationV2s;
        private bool _blnIsLoaded;



        // Public Props
        private List<string> _lstErrorMessages;
        public List<string> ErrorMessages
        {
            get { return _lstErrorMessages; }
            set { _lstErrorMessages = value; }
        }



        // Constructor 
        public SiteLocationAnalyzer()
        {
            _blnIsLoaded = false;
            _lstErrorMessages = new List<string>();
            _lstSiteLocationV2s = new List<SiteLocationV2>();
        }

        public bool Load(List<SiteLocationV2> lstSiteLocationsV2s)
        {

            try
            {
                _lstSiteLocationV2s = lstSiteLocationsV2s;

                if (_lstSiteLocationV2s.Count == 0)
                {
                    throw new Exception("There must be at least one site location in the list to instantiate a SITE LOCATION ANALYZER.");
                }


                _blnIsLoaded = true;
            }
            catch (Exception ex)
            {
                string strErrorMessage = String.Format("There was an error while trying to load the addresses that are to be migrated.  Error Message = [{0}]", ex.Message);
                _lstErrorMessages.Add(strErrorMessage);
            }

            _blnIsLoaded = (_lstErrorMessages.Count == 0);

            return _blnIsLoaded;

        }

        /// <summary>
        /// Compares the Floor, Room, and Suite provided by the caller... and returns the SL Number corresponding to the existing service location if a match is found
        /// </summary>
        /// <param name="strFloor"></param>
        /// <param name="strRoom"></param>
        /// <param name="strSuite"></param>
        /// <param name="strSLNumber">If the method returns TRUE, then this variable will hold the SL Number corresponding to the existing record</param>
        /// <returns></returns>
        public bool FindServiceLocation_SLNumber(string strFloor, string strRoom, string strSuite, out string strSLNumber)
        {
            // Initialize the SL Number out variable
            strSLNumber = String.Empty;
            //            string strSCode_CLLIIdSuffix = String.Empty;

            bool blnHasFloor = !(String.IsNullOrEmpty(strFloor?.Trim()));
            bool blnHasRoom = !(String.IsNullOrEmpty(strRoom?.Trim()));
            bool blnHasSuite = !(String.IsNullOrEmpty(strSuite?.Trim()));


            // Iterate over the list of SITE LOCATIONS
            for (int i = 0; i < _lstSiteLocationV2s.Count; i++)
            {
                bool blnServiceLocationMatches = false;
                string strCurrentSLNumber = String.Empty;

                if (_lstSiteLocationV2s[i]?.ServiceLocations != null)
                {
                    // For the current SITE LOCATION, iterate over the SERVICE LOCATIONS
                    for (int j = 0; j < _lstSiteLocationV2s[i]?.ServiceLocations?.Count(); j++)
                    {
                        // Set the variable to hold the SLNumber for this designator
                        strCurrentSLNumber = _lstSiteLocationV2s[i]?.ServiceLocations[j]?.MasterServiceLocationId;
                        //strSCode_CLLIIdSuffix = _lstSiteLocationV2s[i]?.ServiceLocations[j]?.CLLIIdSuffix;

                        bool blnFloorMatches = false;
                        bool blnRoomMatches = false;
                        bool blnSuiteMatches = false;

                        // Translate the complex, cryptic object into something a little more manageable and strongly typed enough to search with linq
                        List<DesinatorTypeValuePair> lstPertinantDesignatorTypePairs = Translate_Servicelocationdesignator_Array_To_StronglyTyped_List(_lstSiteLocationV2s[i].ServiceLocations[j].PrimaryServiceLocationDesignator, strCurrentSLNumber);

                        // Skip this Service Location if it doesn't have any pertinant designators
                        if (lstPertinantDesignatorTypePairs.Count == 0)
                        {
                            // Skip to the next service location
                            continue;
                        }

                        if (blnHasFloor)
                        {
                            blnFloorMatches = lstPertinantDesignatorTypePairs.Where(p => ((p.DesignatorType == DesignatorType.Floor) && (p.DesignatorValue?.Trim()?.ToUpper() == strFloor?.Trim()?.ToUpper()))).Count() > 0;
                        }
                        else
                        {
                            blnFloorMatches = true;
                        }

                        if (blnHasRoom)
                        {
                            blnRoomMatches = lstPertinantDesignatorTypePairs.Where(p => ((p.DesignatorType == DesignatorType.Room) && (p.DesignatorValue?.Trim()?.ToUpper() == strRoom?.Trim()?.ToUpper()))).Count() > 0;
                        }
                        else
                        {
                            blnRoomMatches = true;
                        }

                        if (blnHasSuite)
                        {
                            blnSuiteMatches = lstPertinantDesignatorTypePairs.Where(p => ((p.DesignatorType == DesignatorType.Suite) && (p.DesignatorValue?.Trim()?.ToUpper() == strSuite?.Trim()?.ToUpper()))).Count() > 0;
                        }
                        else
                        {
                            blnSuiteMatches = true;
                        }

                        // Set the bool that denotes whether the current iteration's service location is an exact match
                        blnServiceLocationMatches = (blnFloorMatches && blnRoomMatches && blnSuiteMatches);

                        if (blnServiceLocationMatches)
                        {
                            // Since we found a match, break out of the loop.... the matching SL Number is populated in the current SL number variable
                            break;
                        }
                    }
                }

                // Set the return value
                if (blnServiceLocationMatches)
                {
                    strSLNumber = strCurrentSLNumber;
                }
                else
                {
                    strSLNumber = String.Empty;
                }
            }

            return (strSLNumber != String.Empty);
        }


        public bool FindServiceLocation_SCode(string strSLNumber, out string strSCode)
        {
            // Initialize the S Code out variable
            strSCode = String.Empty;

            // Iterate over the list of SITE LOCATIONS
            for (int i = 0; i < _lstSiteLocationV2s.Count; i++)
            {
                string strCurrentSLNumber = String.Empty;

                if (_lstSiteLocationV2s[i]?.ServiceLocations != null)
                {
                    // For the current SITE LOCATION, iterate over the SERVICE LOCATIONS
                    for (int j = 0; j < _lstSiteLocationV2s[i]?.ServiceLocations?.Count(); j++)
                    {
                        // Set the variable to hold the SLNumber for this designator
                        strCurrentSLNumber = _lstSiteLocationV2s[i]?.ServiceLocations[j]?.MasterServiceLocationId;

                        if (strCurrentSLNumber?.Trim().ToUpper() == strSLNumber?.Trim().ToUpper())
                        {
                            // Set the OUT param
                            strSCode = _lstSiteLocationV2s[i]?.ServiceLocations[j]?.CLLIIdSuffix ?? String.Empty;

                            // Since we found the SL and have the SCode value, break out of the loop
                            break;    
                        }
                    }
                }
            }

            return (strSCode != String.Empty);
        }

        public bool FindSiteLocation_SiteCode(string strPLNumber, out string strSiteCode)
        {
            // Initialize the Site Code out variable
            strSiteCode = String.Empty;

            // Iterate over the list of SITE LOCATIONS
            for (int i = 0; i < _lstSiteLocationV2s.Count; i++)
            {
                string strCurrentPLNumber = String.Empty;

                if (_lstSiteLocationV2s[i]?.ServiceLocations != null)
                {
                    // For the current SITE LOCATION, iterate over the SERVICE LOCATIONS
                    for (int j = 0; j < _lstSiteLocationV2s[i]?.ServiceLocations?.Count(); j++)
                    {
                        // Set the variable to hold the SLNumber for this designator
                        strCurrentPLNumber = _lstSiteLocationV2s[i]?.ServiceLocations[j]?.MasterSiteId;

                        if (strCurrentPLNumber?.Trim().ToUpper() == strPLNumber?.Trim().ToUpper())
                        {
                            // Set the OUT param
                            strSiteCode = _lstSiteLocationV2s[i]?.ServiceLocations[j]?.CLLIId ?? String.Empty;

                            // Since we found the PL and have the Site Code value, break out of the loop
                            break;
                        }
                    }
                }
            }

            return (strSiteCode != String.Empty);
        }

        public static DesignatorType Translate_DesignatorTypeIDString_To_Enum(string strStringID)
        {
            switch (strStringID?.Trim()?.ToUpper())
            {
                case "FL":
                    return DesignatorType.Floor;
                case "STE":
                    return DesignatorType.Suite;
                case "RM":
                    return DesignatorType.Room;
                case "":
                    return DesignatorType.Blank;
                case "NULL":
                    return DesignatorType.Blank;
                default:
                    return DesignatorType.Other;
            }

        }


        public List<DesinatorTypeValuePair> Translate_Servicelocationdesignator_Array_To_StronglyTyped_List(Primaryservicelocationdesignator objPrimaryServiceLocationDesignators, string strMasterServiceLocationID)
        {
            // Instantiate the return list
            List<DesinatorTypeValuePair> lstDesignatorTypeValuePairs = new List<DesinatorTypeValuePair>();

            try
            {
                if (objPrimaryServiceLocationDesignators != null)
                {
                    // Set the designator type for the FIRST, SECOND, and THIRD Designators

                    // First
                    DesignatorType enmDesignatorType_First = Translate_DesignatorTypeIDString_To_Enum(objPrimaryServiceLocationDesignators?.FirstDesignatorTypeId?.Trim().ToUpper());
                    string strDesignatorValue_First = objPrimaryServiceLocationDesignators?.FirstDesignatorValue?.Trim();

                    if (IsPertinantDesignatorType(enmDesignatorType_First))
                    {
                        lstDesignatorTypeValuePairs.Add(new DesinatorTypeValuePair { DesignatorType = enmDesignatorType_First, DesignatorValue = strDesignatorValue_First, SLNumber = strMasterServiceLocationID?.Trim().ToUpper() });
                    }

                    // Second
                    DesignatorType enmDesignatorType_Second = Translate_DesignatorTypeIDString_To_Enum(objPrimaryServiceLocationDesignators?.SecondDesignatorTypeId?.Trim().ToUpper());
                    string strDesignatorValue_Second = objPrimaryServiceLocationDesignators?.SecondDesignatorValue?.Trim();

                    if (IsPertinantDesignatorType(enmDesignatorType_Second))
                    {
                        lstDesignatorTypeValuePairs.Add(new DesinatorTypeValuePair { DesignatorType = enmDesignatorType_Second, DesignatorValue = strDesignatorValue_Second, SLNumber = strMasterServiceLocationID?.Trim().ToUpper() });
                    }

                    // Third
                    DesignatorType enmDesignatorType_Third = Translate_DesignatorTypeIDString_To_Enum(objPrimaryServiceLocationDesignators?.ThirdDesignatorTypeId?.Trim().ToUpper());
                    string strDesignatorValue_Third = objPrimaryServiceLocationDesignators?.ThirdDesignatorValue?.Trim();

                    if (IsPertinantDesignatorType(enmDesignatorType_Third))
                    {
                        lstDesignatorTypeValuePairs.Add(new DesinatorTypeValuePair { DesignatorType = enmDesignatorType_Third, DesignatorValue = strDesignatorValue_Third, SLNumber = strMasterServiceLocationID?.Trim().ToUpper() });
                    }

                }
            }
            catch (Exception ex)
            {
                string strError = ex.Message;
                throw;
            }

            return lstDesignatorTypeValuePairs;
        }

        public bool IsPertinantDesignatorType(DesignatorType enmDesignatorType)
        {
            switch (enmDesignatorType)
            {
                case DesignatorType.Suite:
                    return true;

                case DesignatorType.Floor:
                    return true;

                case DesignatorType.Room:
                    return true;

                case DesignatorType.Other:
                    return false;

                case DesignatorType.Blank:
                    return false;
                default:
                    return false;
            }
        }

    }

    public enum DesignatorType
    {
        Suite,
        Floor,
        Room,
        Other,
        Blank
    }

    public class DesinatorTypeValuePair
    {
        public DesignatorType DesignatorType { get; set; }
        public string DesignatorValue { get; set; }
        public string SLNumber { get; set; }

    }


}
