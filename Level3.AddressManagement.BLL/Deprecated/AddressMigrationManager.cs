using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Level3.AddressManagement.Model.SearchLocationService;
using Level3.AddressManagement.Model.LocationService;

namespace Level3.AddressManagement.BLL
{

    // TODO: DELETE THIS CLASS AFTER TESTING
    //public class AddressMigrationManager
    //{

    //    // Private Members
    //    BLL.FileAndDirectoryLocationCalculator _objFileAndDirLocationCalc;
    //    private string _strExcelFileName;

    //    // Public Props
    //    private List<string> _lstErrorMessages;
    //    public List<string> ErrorMessages
    //    {
    //        get { return _lstErrorMessages; }
    //        set { _lstErrorMessages = value; }
    //    }


    //    private List<Model.SourceAddress> _lstSourceAddresses_Raw;
    //    public List<Model.SourceAddress> SourceAddresses_Raw
    //    {
    //        get { return _lstSourceAddresses_Raw; }
    //        set { _lstSourceAddresses_Raw = value; }
    //    }


    //    // Private Members
    //    private bool _blnIsLoaded;



    //    // Constructor
    //    public AddressMigrationManager()
    //    {
    //        _lstErrorMessages = new List<string>();
    //        _lstSourceAddresses_Raw = new List<Model.SourceAddress>();
    //        _blnIsLoaded = false;
    //    }


    //    // Public Methods
    //    public bool Load(string strExcelFileName)
    //    {
    //        try
    //        {
    //            _strExcelFileName = strExcelFileName;

    //            // Open the spreadhsheet, and read all the rows into a list of SourceAddress objects
    //            RAL.MSExcelManager objMSExcelManager = new RAL.MSExcelManager();
    //            if (objMSExcelManager.Load(_strExcelFileName))
    //            {
    //                if (objMSExcelManager.ReadWorksheetRowsIntoSourceAddressObjects())
    //                {

    //                }
    //                else
    //                {
    //                    _lstErrorMessages.AddRange(objMSExcelManager.ErrorMessages);
    //                }
    //            }
    //            else
    //            {
    //                throw new Exception(String.Format("The Excel Manager could not be loaded.  FileName = [{0}]", _strExcelFileName));
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            string strErrorMessage = String.Format("There was an error while trying to load the addresses that are to be migrated.  Error Message = [{0}]", ex.Message);
    //            _lstErrorMessages.Add(strErrorMessage);
    //        }

    //        _blnIsLoaded = (_lstErrorMessages.Count == 0);

    //        return _blnIsLoaded;
    //    }


    //    public bool MigrateAddresses()
    //    {
    //        try
    //        {
    //            // Make sure that the load method was called, and succeeded
    //            if (_blnIsLoaded == false)
    //            {
    //                throw new Exception("The LOAD method was NOT successful, and so this subsequent method CANNOT be called.");
    //            }

    //            string strLoggingString = String.Empty;

    //            // Load a GLM Call Manager
    //            RAL.GLMCallManager objGLMClient = new RAL.GLMCallManager(ConfigHelper.GetGLMBaseUrl(), ConfigHelper.GetGLMAuthorizationHeaderUsername(), ConfigHelper.GetGLMAuthorizationHeaderApplicationID());

    //            // Iterate over each address, making a call to GLM for each record
    //            for (int i = 0; i < _lstSourceAddresses_Raw.Count; i++)
    //            {
    //                try
    //                {
    //                    //AdvancedLocationQueryV2Request objAdvancedLocationQueryV2Request = Translate(_lstSourceAddresses_Raw[i]);


    //                    //objGLMClient.PostAddressSearchToGLM()
    //                    //strLoggingString = String.Format("Address = {0}", "TODO - add address fields");



    //                }
    //                catch (Exception ex)
    //                {
    //                    string strErrorMessage = String.Format("There was an error while trying to migrate the address record.  {0}, Error Message = [{1}]", strLoggingString, ex.Message);

    //                    _lstErrorMessages.Add(strErrorMessage);

    //                    // Continue to the next address
    //                    continue;
    //                }
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            string strErrorMessage = String.Format("There was an error while trying to load the addresses that are to be migrated.  Error Message = [{0}]", ex.Message);
    //            _lstErrorMessages.Add(strErrorMessage);

    //        }

    //        return (_lstErrorMessages.Count == 0);
    //    }


    //    #region   DEPRECATED
    //    //private AdvancedLocationQueryV2Request Translate(Model.SourceAddress objSourceAddress)
    //    //{
    //    //    // TODO:  Trim and Ucase values
    //    //    // Dean has Address, City, and Country as required...
    //    //    //this.setCreateSiteIfNotFound(true);
    //    //    //this.setReturnServiceLocations(true);
    //    //    //this.setExactMatchSearch(false);

    //    //    AdvancedLocationQueryV2Request objAdvancedLocationQueryV2Request = new AdvancedLocationQueryV2Request();

    //    //    objAdvancedLocationQueryV2Request.CreateSiteIfNotFound = true;
    //    //    objAdvancedLocationQueryV2Request.AddressLine1 = objSourceAddress.Address1;
    //    //    objAdvancedLocationQueryV2Request.City = objSourceAddress.City;
    //    //    objAdvancedLocationQueryV2Request.StateCode = objSourceAddress.State;
    //    //    objAdvancedLocationQueryV2Request.CountryCode = objSourceAddress.Country;


    //    //    bool blnHasFloor = (!(String.IsNullOrEmpty(objSourceAddress.Floor)));
    //    //    bool blnHasSuite = (!(String.IsNullOrEmpty(objSourceAddress.Room)));
    //    //    bool blnHasBuilding = (!(String.IsNullOrEmpty(objSourceAddress.Building)));


    //    //    {

    //    //    }

    //    //    //objAdvancedLocationQueryV2Request.SearchAddressLine2Values.


    //    //    return objAdvancedLocationQueryV2Request;
    //    //} 
    //    #endregion

    //}
}
