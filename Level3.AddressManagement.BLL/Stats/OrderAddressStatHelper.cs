using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Level3.AddressManagement.DAL;
using Level3.AddressManagement.Model;

namespace Level3.AddressManagement.BLL
{
    public class OrderAddressStatHelper
    {
        // Private Members
        List<string> _lstSORs;

        private List<string> _lstErrorMessages;
        public List<string> ErrorMessages
        {
            get { return _lstErrorMessages; }
            set { _lstErrorMessages = value; }
        }


        private List<OrderAddress> _lstOrderAddresss_All;
        private bool _blnAllOrderAddresssLoaded;

        // Constructor
        public OrderAddressStatHelper()
        {
            _lstSORs = new List<string>();
            _lstErrorMessages = new List<string>();
            _lstOrderAddresss_All = new List<OrderAddress>();
            _blnAllOrderAddresssLoaded = false;
        }

        public bool GetNumberOfOrderAddresssAddedOrModifiedOnSpecificDay(DateTime? dteDate, RecordTimeStampType enmRecordTimeStampType, out int intCount)
        {
            // Clear the error messages
            _lstErrorMessages = new List<string>();

            // Intialize the out parameer
            intCount = 0;

            try
            {
                // Set the date variables
                DateTime? dteTimeStampDate_Start = dteDate.Value.Date;
                DateTime? dteTimeStampDate_End = dteDate.Value.Date;

                // Instantiate and load a OrderAddress Search Manager
                OrderAddressSearchManager objOrderAddressSearchManager = new OrderAddressSearchManager();

                switch (enmRecordTimeStampType)
                {
                    case RecordTimeStampType.Added:

                        if (objOrderAddressSearchManager.Load(null, null, null, null, null, dteTimeStampDate_Start, dteTimeStampDate_End, string.Empty, string.Empty) == false)
                        {
                            _lstErrorMessages.AddRange(objOrderAddressSearchManager.ErrorMessages);
                            return false;
                        }
                        break;

                    default:

                        _lstErrorMessages.Add("Unrecognized RecordTimeStampType");
                        return false; ;
                }



                // Execute the search against the DB
                if (objOrderAddressSearchManager.ExecuteSearch(1, 100000000) == false)
                {
                    _lstErrorMessages.AddRange(objOrderAddressSearchManager.ErrorMessages);
                    return false;
                }

                // Set the return variable
                intCount = objOrderAddressSearchManager.SearchResults.Count;


            }
            catch (Exception ex)
            {
                _lstErrorMessages.Add(ex.Message);

            }

            return (_lstErrorMessages.Count == 0);
        }

        public bool LoadAllOrderAddresss()
        {
            try
            {
                _lstOrderAddresss_All = new OrderAddress().GetAllRecords();
                _blnAllOrderAddresssLoaded = true;
            }
            catch (Exception ex)
            {
                _lstErrorMessages.Add(ex.Message);
            }

            return (_lstErrorMessages.Count == 0);
        }
        private void EnforceAllOrderAddresssLoaded()
        {
            if (_blnAllOrderAddresssLoaded == false)
            {
                throw new Exception("All OrderAddresss must be loaded from the DB before this method can be invoked.");
            }
        }

        public bool GetTotalNumberOfOrderAddresssInSystem(out int intCount)
        {
            // Clear the error messages
            _lstErrorMessages = new List<string>();

            // Intialize the out parameter
            intCount = 0;

            try
            {
                // make sure all OrderAddresss were loaded
                EnforceAllOrderAddresssLoaded();

                // Set the return variable
                intCount = _lstOrderAddresss_All.Count;
            }
            catch (Exception ex)
            {
                _lstErrorMessages.Add(ex.Message);
            }

            return (_lstErrorMessages.Count == 0);
        }
        public bool GetOrderAddressCountsGroupedByStatus(out List<StatValuePair> lstStatValuePairs)
        {
            lstStatValuePairs = new List<StatValuePair>();

            try
            {
                // make sure all OrderAddresss were loaded
                EnforceAllOrderAddresssLoaded();

                var lstGroups = _lstOrderAddresss_All.GroupBy(a => a.MigrationStatusID).ToList();
                foreach (var lstStatusGroup in lstGroups)
                {
                    lstStatValuePairs.Add(new StatValuePair(((MigrationStatuses)lstStatusGroup.Key).ToString(), lstStatusGroup.Count().ToString()));
                }
            }
            catch (Exception ex)
            {
                _lstErrorMessages.Add(ex.Message);
            }

            return _lstErrorMessages.Count == 0;
        }

        public bool GetOrderAddressValueGroupedByCountry(out List<StatValuePair> lstStatValuePairs)
        {
            lstStatValuePairs = new List<StatValuePair>();

            try
            {
                // make sure all OrderAddresss were loaded
                EnforceAllOrderAddresssLoaded();

                var lstGroups = _lstOrderAddresss_All.GroupBy(a => a.CDWCountry).ToList();
                foreach (var lstCountryGroup in lstGroups)
                {
                    string strCount = String.Empty;
                    try
                    {
                        int intCount = lstCountryGroup.Select(l => l.CDWCountry).Count();
                        strCount = intCount.ToString();
                    }
                    catch (Exception ex)
                    {
                        strCount = "could not be calc'd";
                    }

                    lstStatValuePairs.Add(new StatValuePair(lstCountryGroup.Key, strCount));
                }
            }
            catch (Exception ex)
            {
                _lstErrorMessages.Add(ex.Message);
            }

            return _lstErrorMessages.Count == 0;
        }


        public bool GetSuccessfullyProcessedCount(out int intSuccessfullyProcessedCount)
        {
            intSuccessfullyProcessedCount = 0;

            try
            {
                // Get all the processed records
                List<OrderAddress> lstProcessed = _lstOrderAddresss_All.Where(v => v.MigrationStatusID == (int)MigrationStatuses.Processing_COMPLETE).ToList();
                intSuccessfullyProcessedCount = lstProcessed.Count();
            }
            catch (Exception ex)
            {
                _lstErrorMessages.Add(ex.Message);
            }

            return _lstErrorMessages.Count == 0;
        }


        public bool GetNumberOfOrderAddressesErroring(out List<OrderAddress> lstOrderAddresssFiltered)
        {
            lstOrderAddresssFiltered = new List<OrderAddress>();

            try
            {
                List<int> lstMigrationStatuses = new List<int>();
                //lstMigrationStatuses.Add((int)MigrationStatuses.STAGED_for_Processing);
                //lstMigrationStatuses.Add((int)MigrationStatuses.GLM_SITE_FOUND_or_CREATED);
                lstMigrationStatuses.Add((int)MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SITE_existence_CHECK);
                //lstMigrationStatuses.Add((int)MigrationStatuses.ABANDONED_GLM_SITE_existence_CHECK_due_to_API_Call_Errors);
                //lstMigrationStatuses.Add((int)MigrationStatuses.STAGED_for_SITE_CODE_existence_CHECK);
                lstMigrationStatuses.Add((int)MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SITE_CODE_existence_CHECK);
                //lstMigrationStatuses.Add((int)MigrationStatuses.ABANDONED_GLM_SITE_CODE_existence_CHECK_due_to_API_Call_Errors);
                //lstMigrationStatuses.Add((int)MigrationStatuses.SITE_CODE_FOUND_in_GLM);
                //lstMigrationStatuses.Add((int)MigrationStatuses.SITE_CODE_does_NOT_EXIST_in_GLM);
                //lstMigrationStatuses.Add((int)MigrationStatuses.STAGED_for_SITE_CODE_ASSIGNMENT_TRIGGERING_in_GLM);
                //lstMigrationStatuses.Add((int)MigrationStatuses.SITE_CODE_ASSIGNMENT_Successfully_TRIGGERED_in_GLM);
                lstMigrationStatuses.Add((int)MigrationStatuses.API_Call_ERRORED_while_trying_to_TRIGGER_the_ASSIGNMENT_of_a_SITE_CODE_in_GLM);
                //lstMigrationStatuses.Add((int)MigrationStatuses.ABANDONED_GLM_SITE_CODE_ASSIGNMENT_due_to_API_Call_Errors);
                //lstMigrationStatuses.Add((int)MigrationStatuses.WORK_ITEM_QUEUED_in_GLM_for_DELAYED_SITE_CODE_ASSIGNMENT);
                //lstMigrationStatuses.Add((int)MigrationStatuses.STAGED_for_SAP_SITE_LOCATION_SEARCH);
                lstMigrationStatuses.Add((int)MigrationStatuses.API_Call_ERRORED_while_executing_SAP_SITE_LOCATION_ADDRESS_SEARCH);
                //lstMigrationStatuses.Add((int)MigrationStatuses.ABANDONED_SAP_SITE_LOCATION_SEARCH_due_to_API_Call_Errors);
                //lstMigrationStatuses.Add((int)MigrationStatuses.Site_Location_ADDRESS_FOUND_in_SAP);
                //lstMigrationStatuses.Add((int)MigrationStatuses.Site_Location_ADDRESS_does_NOT_EXIST_in_SAP);
                //lstMigrationStatuses.Add((int)MigrationStatuses.READY_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP);
                lstMigrationStatuses.Add((int)MigrationStatuses.API_Call_ERRORED_while_attempting_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP);
                //lstMigrationStatuses.Add((int)MigrationStatuses.ABANDONED_attempts_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP_due_to_API_Call_Errors);
                //lstMigrationStatuses.Add((int)MigrationStatuses.READY_for_ADDRESS_TYPE_BRANCHING_Logic_EVALUATION);
                //lstMigrationStatuses.Add((int)MigrationStatuses.STAGED_for_GLM_SERVICE_LOCATION_SEARCH);
                lstMigrationStatuses.Add((int)MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SERVICE_LOCATION_SEARCH);
                //lstMigrationStatuses.Add((int)MigrationStatuses.ABANDONED_GLM_SERVICE_LOCATION_SEARCH_due_to_API_Call_Errors);
                //lstMigrationStatuses.Add((int)MigrationStatuses.SERVICE_LOCATION_FOUND_in_GLM);
                //lstMigrationStatuses.Add((int)MigrationStatuses.SERVICE_LOCATION_does_NOT_EXIST_in_GLM);
                //lstMigrationStatuses.Add((int)MigrationStatuses.STAGED_for_SERVICE_LOCATION_CREATION_in_GLM);
                //lstMigrationStatuses.Add((int)MigrationStatuses.SERVICE_LOCATION_Successfully_CREATED_in_GLM);
                lstMigrationStatuses.Add((int)MigrationStatuses.API_Call_ERRORED_while_executing_the_GLM_SERVICE_LOCATION_CREATION_attempt);
                //lstMigrationStatuses.Add((int)MigrationStatuses.ABANDONED_GLM_SERVICE_LOCATION_CREATION_due_to_API_Call_Errors);
                //lstMigrationStatuses.Add((int)MigrationStatuses.STAGED_for_S_CODE_existence_CHECK);
                lstMigrationStatuses.Add((int)MigrationStatuses.API_Call_ERRORED_while_executing_GLM_S_CODE_existence_CHECK);
                //lstMigrationStatuses.Add((int)MigrationStatuses.ABANDONED_GLM_S_CODE_existence_CHECK_due_to_API_Call_Errors);
                //lstMigrationStatuses.Add((int)MigrationStatuses.S_CODE_FOUND_in_GLM);
                //lstMigrationStatuses.Add((int)MigrationStatuses.S_CODE_does_NOT_EXIST_in_GLM);
                //lstMigrationStatuses.Add((int)MigrationStatuses.STAGED_for_S_CODE_CREATION_in_GLM);
                //lstMigrationStatuses.Add((int)MigrationStatuses.S_CODE_Successfully_CREATED_in_GLM);
                lstMigrationStatuses.Add((int)MigrationStatuses.API_Call_ERRORED_while_executing_GLM_S_CODE_CREATION_attempt);
                //lstMigrationStatuses.Add((int)MigrationStatuses.ABANDONED_GLM_S_CODE_CREATION_due_to_API_Call_Errors);
                //lstMigrationStatuses.Add((int)MigrationStatuses.WORK_ITEM_QUEUED_in_GLM_for_DELAYED_S_CODE_CREATION);
                //lstMigrationStatuses.Add((int)MigrationStatuses.STAGED_for_SAP_SERVICE_LOCATION_SEARCH);
                lstMigrationStatuses.Add((int)MigrationStatuses.API_Call_ERRORED_while_executing_SAP_SERVICE_LOCATION_ADDRESS_SEARCH);
                //lstMigrationStatuses.Add((int)MigrationStatuses.ABANDONED_SAP_SERVICE_LOCATION_SEARCH_due_to_API_Call_Errors);
                //lstMigrationStatuses.Add((int)MigrationStatuses.Service_Location_ADDRESS_FOUND_in_SAP);
                //lstMigrationStatuses.Add((int)MigrationStatuses.Service_Location_ADDRESS_does_NOT_EXIST_in_SAP);
                //lstMigrationStatuses.Add((int)MigrationStatuses.READY_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP);
                lstMigrationStatuses.Add((int)MigrationStatuses.API_Call_ERRORED_while_attempting_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP);
                //lstMigrationStatuses.Add((int)MigrationStatuses.ABANDONED_attempts_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP_due_to_API_Call_Errors);
                //lstMigrationStatuses.Add((int)MigrationStatuses.Processing_COMPLETE);
                //lstMigrationStatuses.Add((int)MigrationStatuses.IGNORED_indefinitely);

                lstOrderAddresssFiltered = _lstOrderAddresss_All.Where(o => lstMigrationStatuses.Contains(o.MigrationStatusID.Value)).ToList();

            }
            catch (Exception ex)
            {
                _lstErrorMessages.Add(String.Concat("There was an error while trying to filter out the required address records.  Error Message = [", ex.Message, "]"));
            }

            return (_lstErrorMessages.Count == 0);
        }

        public bool GetNumberOfOrderAddressesAbandoned(out List<OrderAddress> lstOrderAddresssFiltered)
        {
            lstOrderAddresssFiltered = new List<OrderAddress>();

            try
            {
                List<int> lstMigrationStatuses = new List<int>();

                //lstMigrationStatuses.Add((int)MigrationStatuses.STAGED_for_Processing);
                //lstMigrationStatuses.Add((int)MigrationStatuses.GLM_SITE_FOUND_or_CREATED);
                //lstMigrationStatuses.Add((int)MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SITE_existence_CHECK);
                lstMigrationStatuses.Add((int)MigrationStatuses.ABANDONED_GLM_SITE_existence_CHECK_due_to_API_Call_Errors);
                //lstMigrationStatuses.Add((int)MigrationStatuses.STAGED_for_SITE_CODE_existence_CHECK);
                //lstMigrationStatuses.Add((int)MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SITE_CODE_existence_CHECK);
                lstMigrationStatuses.Add((int)MigrationStatuses.ABANDONED_GLM_SITE_CODE_existence_CHECK_due_to_API_Call_Errors);
                //lstMigrationStatuses.Add((int)MigrationStatuses.SITE_CODE_FOUND_in_GLM);
                //lstMigrationStatuses.Add((int)MigrationStatuses.SITE_CODE_does_NOT_EXIST_in_GLM);
                //lstMigrationStatuses.Add((int)MigrationStatuses.STAGED_for_SITE_CODE_ASSIGNMENT_TRIGGERING_in_GLM);
                //lstMigrationStatuses.Add((int)MigrationStatuses.SITE_CODE_ASSIGNMENT_Successfully_TRIGGERED_in_GLM);
                //lstMigrationStatuses.Add((int)MigrationStatuses.API_Call_ERRORED_while_trying_to_TRIGGER_the_ASSIGNMENT_of_a_SITE_CODE_in_GLM);
                lstMigrationStatuses.Add((int)MigrationStatuses.ABANDONED_GLM_SITE_CODE_ASSIGNMENT_due_to_API_Call_Errors);
                //lstMigrationStatuses.Add((int)MigrationStatuses.WORK_ITEM_QUEUED_in_GLM_for_DELAYED_SITE_CODE_ASSIGNMENT);
                //lstMigrationStatuses.Add((int)MigrationStatuses.STAGED_for_SAP_SITE_LOCATION_SEARCH);
                //lstMigrationStatuses.Add((int)MigrationStatuses.API_Call_ERRORED_while_executing_SAP_SITE_LOCATION_ADDRESS_SEARCH);
                lstMigrationStatuses.Add((int)MigrationStatuses.ABANDONED_SAP_SITE_LOCATION_SEARCH_due_to_API_Call_Errors);
                //lstMigrationStatuses.Add((int)MigrationStatuses.Site_Location_ADDRESS_FOUND_in_SAP);
                //lstMigrationStatuses.Add((int)MigrationStatuses.Site_Location_ADDRESS_does_NOT_EXIST_in_SAP);
                //lstMigrationStatuses.Add((int)MigrationStatuses.READY_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP);
                //lstMigrationStatuses.Add((int)MigrationStatuses.API_Call_ERRORED_while_attempting_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP);
                lstMigrationStatuses.Add((int)MigrationStatuses.ABANDONED_attempts_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP_due_to_API_Call_Errors);
                //lstMigrationStatuses.Add((int)MigrationStatuses.READY_for_ADDRESS_TYPE_BRANCHING_Logic_EVALUATION);
                //lstMigrationStatuses.Add((int)MigrationStatuses.STAGED_for_GLM_SERVICE_LOCATION_SEARCH);
                //lstMigrationStatuses.Add((int)MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SERVICE_LOCATION_SEARCH);
                lstMigrationStatuses.Add((int)MigrationStatuses.ABANDONED_GLM_SERVICE_LOCATION_SEARCH_due_to_API_Call_Errors);
                //lstMigrationStatuses.Add((int)MigrationStatuses.SERVICE_LOCATION_FOUND_in_GLM);
                //lstMigrationStatuses.Add((int)MigrationStatuses.SERVICE_LOCATION_does_NOT_EXIST_in_GLM);
                //lstMigrationStatuses.Add((int)MigrationStatuses.STAGED_for_SERVICE_LOCATION_CREATION_in_GLM);
                //lstMigrationStatuses.Add((int)MigrationStatuses.SERVICE_LOCATION_Successfully_CREATED_in_GLM);
                //lstMigrationStatuses.Add((int)MigrationStatuses.API_Call_ERRORED_while_executing_the_GLM_SERVICE_LOCATION_CREATION_attempt);
                lstMigrationStatuses.Add((int)MigrationStatuses.ABANDONED_GLM_SERVICE_LOCATION_CREATION_due_to_API_Call_Errors);
                //lstMigrationStatuses.Add((int)MigrationStatuses.STAGED_for_S_CODE_existence_CHECK);
                //lstMigrationStatuses.Add((int)MigrationStatuses.API_Call_ERRORED_while_executing_GLM_S_CODE_existence_CHECK);
                lstMigrationStatuses.Add((int)MigrationStatuses.ABANDONED_GLM_S_CODE_existence_CHECK_due_to_API_Call_Errors);
                //lstMigrationStatuses.Add((int)MigrationStatuses.S_CODE_FOUND_in_GLM);
                //lstMigrationStatuses.Add((int)MigrationStatuses.S_CODE_does_NOT_EXIST_in_GLM);
                //lstMigrationStatuses.Add((int)MigrationStatuses.STAGED_for_S_CODE_CREATION_in_GLM);
                //lstMigrationStatuses.Add((int)MigrationStatuses.S_CODE_Successfully_CREATED_in_GLM);
                //lstMigrationStatuses.Add((int)MigrationStatuses.API_Call_ERRORED_while_executing_GLM_S_CODE_CREATION_attempt);
                lstMigrationStatuses.Add((int)MigrationStatuses.ABANDONED_GLM_S_CODE_CREATION_due_to_API_Call_Errors);
                //lstMigrationStatuses.Add((int)MigrationStatuses.WORK_ITEM_QUEUED_in_GLM_for_DELAYED_S_CODE_CREATION);
                //lstMigrationStatuses.Add((int)MigrationStatuses.STAGED_for_SAP_SERVICE_LOCATION_SEARCH);
                //lstMigrationStatuses.Add((int)MigrationStatuses.API_Call_ERRORED_while_executing_SAP_SERVICE_LOCATION_ADDRESS_SEARCH);
                lstMigrationStatuses.Add((int)MigrationStatuses.ABANDONED_SAP_SERVICE_LOCATION_SEARCH_due_to_API_Call_Errors);
                //lstMigrationStatuses.Add((int)MigrationStatuses.Service_Location_ADDRESS_FOUND_in_SAP);
                //lstMigrationStatuses.Add((int)MigrationStatuses.Service_Location_ADDRESS_does_NOT_EXIST_in_SAP);
                //lstMigrationStatuses.Add((int)MigrationStatuses.READY_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP);
                //lstMigrationStatuses.Add((int)MigrationStatuses.API_Call_ERRORED_while_attempting_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP);
                lstMigrationStatuses.Add((int)MigrationStatuses.ABANDONED_attempts_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP_due_to_API_Call_Errors);
                //lstMigrationStatuses.Add((int)MigrationStatuses.Processing_COMPLETE);
                //lstMigrationStatuses.Add((int)MigrationStatuses.IGNORED_indefinitely);

                lstOrderAddresssFiltered = _lstOrderAddresss_All.Where(o => lstMigrationStatuses.Contains(o.MigrationStatusID.Value)).ToList();

            }
            catch (Exception ex)
            {
                _lstErrorMessages.Add(String.Concat("There was an error while trying to filter out the required address records.  Error Message = [", ex.Message, "]"));
            }

            return (_lstErrorMessages.Count == 0);
        }


    }


    // Class specific ENUM
    public enum RecordTimeStampType
    {
        Added,
        Modified
    }

}