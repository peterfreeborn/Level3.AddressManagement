using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;
using Level3.AddressManagement.Model;
using Level3.AddressManagement.DAL;

namespace Level3.AddressManagement.BLL
{


    public class OrderAddressStateEvaluator
    {
        // Declare the static logger object that will be used to implement logging via log4net
        private static ILog _objLogger = LogManager.GetLogger(typeof(OrderAddressStateEvaluator));


        // Private Members
        private string _strBaseLoggingMessage;
        private bool _blnIsLoaded;
        private OrderAddress _objOrderAddress_Existing;


        // Public Properties
        private List<string> _lstErrorMessages;
        public List<string> ErrorMessages
        {
            get { return _lstErrorMessages; }
            set { _lstErrorMessages = value; }
        }


        // Constructor 
        public OrderAddressStateEvaluator()
        {
            _lstErrorMessages = new List<string>();
        }


        // Public Methods
        public bool Load(OrderAddress objOrderAddress)
        {
            try
            {
                if (objOrderAddress == null)
                {
                    throw new Exception("The workflow address object supplied was null.  The Workflow address State evaluator requires a loaded, non-null address to function. ");
                }

                // Set the local property
                _objOrderAddress_Existing = objOrderAddress;

                // Set the base logging string
                _strBaseLoggingMessage = OrderAddressUtil.CalcUniqueRecordIdentifierLoggingString(_objOrderAddress_Existing);
            }
            catch (Exception ex)
            {
                _lstErrorMessages.Add(String.Concat("There was an error while trying to LOAD the address Evaluator.  ", _strBaseLoggingMessage, ",  Error Message: [", ex.Message, "]"));
            }

            _blnIsLoaded = _lstErrorMessages.Count() == 0;

            // return 
            return _blnIsLoaded;
        }

        public bool CanBeSetToIgnoredByEndUser()
        {
            bool blnCanBeSetToIgnored = true;
            try
            {
                EnforceLoadSuccess();

                if (blnCanBeSetToIgnored)
                {
                    List<MigrationStatuses> lstStatusesToAllow = GetStatuses_CanBeSetToIgnoredByUser();

                    if (ListContainsStatus(_objOrderAddress_Existing.MigrationStatusID.Value, lstStatusesToAllow))
                    {
                        blnCanBeSetToIgnored = true;
                    }
                    else
                    {
                        blnCanBeSetToIgnored = false;
                    }
                }
            }
            catch (Exception ex)
            {
                blnCanBeSetToIgnored = false;
            }

            return blnCanBeSetToIgnored;
        }
        public bool CanBeAPIRetriedByEndUser()
        {
            bool blnCanBeRetried = true;
            try
            {
                EnforceLoadSuccess();

                if (blnCanBeRetried)
                {
                    List<MigrationStatuses> lstStatusesToAllow = GetStatuses_CanBeRetriedByUser();

                    if (ListContainsStatus(_objOrderAddress_Existing.MigrationStatusID.Value, lstStatusesToAllow))
                    {
                        blnCanBeRetried = true;
                    }
                    else
                    {
                        blnCanBeRetried = false;
                    }
                }
            }
            catch (Exception ex)
            {
                blnCanBeRetried = false;
            }

            return blnCanBeRetried;
        }
        public bool CanBeRevertedByEndUser()
        {
            bool blnCanBeReverted = true;
            try
            {
                EnforceLoadSuccess();

                if (blnCanBeReverted)
                {
                    List<MigrationStatuses> lstStatusesToAllow = GetStatuses_CanBeRevertedByUser();

                    if (ListContainsStatus(_objOrderAddress_Existing.MigrationStatusID.Value, lstStatusesToAllow))
                    {
                        blnCanBeReverted = true;
                    }
                    else
                    {
                        blnCanBeReverted = false;
                    }
                }
            }
            catch (Exception ex)
            {
                blnCanBeReverted = false;
            }

            return blnCanBeReverted;
        }
        public bool CanBeForcedToProcessByEndUser()
        {
            bool blnCanBeForcedToProcess = true;
            try
            {
                EnforceLoadSuccess();

                if (blnCanBeForcedToProcess)
                {
                    List<MigrationStatuses> lstStatusesToAllow = GetStatuses_CanBeForcedToProcessByUser();

                    if (ListContainsStatus(_objOrderAddress_Existing.MigrationStatusID.Value, lstStatusesToAllow))
                    {
                        blnCanBeForcedToProcess = true;
                    }
                    else
                    {
                        blnCanBeForcedToProcess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                blnCanBeForcedToProcess = false;
            }

            return blnCanBeForcedToProcess;
        }

        // Private methods
        private void EnforceLoadSuccess()
        {
            if (_blnIsLoaded == false)
            {
                throw new Exception("The load method was not call or did not succeed, and so subsequent and dependant methods in this class cannot be invoked.");
            }
        }

        private List<MigrationStatuses> GetStatuses_CanBeRetriedByUser()
        {
            List<MigrationStatuses> lstMigrationStatuses = new List<MigrationStatuses>();

            //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_Processing);
            //lstMigrationStatuses.Add(MigrationStatuses.GLM_SITE_FOUND_or_CREATED);
            lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SITE_existence_CHECK);
            lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SITE_existence_CHECK_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SITE_CODE_existence_CHECK);
            lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SITE_CODE_existence_CHECK);
            lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SITE_CODE_existence_CHECK_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.SITE_CODE_FOUND_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.SITE_CODE_does_NOT_EXIST_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SITE_CODE_ASSIGNMENT_TRIGGERING_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.SITE_CODE_ASSIGNMENT_Successfully_TRIGGERED_in_GLM);
            lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_trying_to_TRIGGER_the_ASSIGNMENT_of_a_SITE_CODE_in_GLM);
            lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SITE_CODE_ASSIGNMENT_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.WORK_ITEM_QUEUED_in_GLM_for_DELAYED_SITE_CODE_ASSIGNMENT);
            //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SAP_SITE_LOCATION_SEARCH);
            lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_SAP_SITE_LOCATION_ADDRESS_SEARCH);
            lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_SAP_SITE_LOCATION_SEARCH_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.Site_Location_ADDRESS_FOUND_in_SAP);
            //lstMigrationStatuses.Add(MigrationStatuses.Site_Location_ADDRESS_does_NOT_EXIST_in_SAP);
            //lstMigrationStatuses.Add(MigrationStatuses.READY_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP);
            lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_attempting_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP);
            lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_attempts_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.READY_for_ADDRESS_TYPE_BRANCHING_Logic_EVALUATION);
            //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_GLM_SERVICE_LOCATION_SEARCH);
            lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SERVICE_LOCATION_SEARCH);
            lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SERVICE_LOCATION_SEARCH_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.SERVICE_LOCATION_FOUND_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.SERVICE_LOCATION_does_NOT_EXIST_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SERVICE_LOCATION_CREATION_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.SERVICE_LOCATION_Successfully_CREATED_in_GLM);
            lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_the_GLM_SERVICE_LOCATION_CREATION_attempt);
            lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SERVICE_LOCATION_CREATION_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_S_CODE_existence_CHECK);
            lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_S_CODE_existence_CHECK);
            lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_S_CODE_existence_CHECK_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.S_CODE_FOUND_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.S_CODE_does_NOT_EXIST_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_S_CODE_CREATION_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.S_CODE_Successfully_CREATED_in_GLM);
            lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_S_CODE_CREATION_attempt);
            lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_S_CODE_CREATION_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.WORK_ITEM_QUEUED_in_GLM_for_DELAYED_S_CODE_CREATION);
            //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SAP_SERVICE_LOCATION_SEARCH);
            lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_SAP_SERVICE_LOCATION_ADDRESS_SEARCH);
            lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_SAP_SERVICE_LOCATION_SEARCH_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.Service_Location_ADDRESS_FOUND_in_SAP);
            //lstMigrationStatuses.Add(MigrationStatuses.Service_Location_ADDRESS_does_NOT_EXIST_in_SAP);
            //lstMigrationStatuses.Add(MigrationStatuses.READY_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP);
            lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_attempting_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP);
            lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_attempts_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.Processing_COMPLETE);
            //lstMigrationStatuses.Add(MigrationStatuses.IGNORED_indefinitely);
            return lstMigrationStatuses;
        }
        private List<MigrationStatuses> GetStatuses_CanBeSetToIgnoredByUser()
        {
            List<MigrationStatuses> lstMigrationStatuses = new List<MigrationStatuses>();

            lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_Processing);
            lstMigrationStatuses.Add(MigrationStatuses.GLM_SITE_FOUND_or_CREATED);
            lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SITE_existence_CHECK);
            lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SITE_existence_CHECK_due_to_API_Call_Errors);
            lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SITE_CODE_existence_CHECK);
            lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SITE_CODE_existence_CHECK);
            lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SITE_CODE_existence_CHECK_due_to_API_Call_Errors);
            lstMigrationStatuses.Add(MigrationStatuses.SITE_CODE_FOUND_in_GLM);
            lstMigrationStatuses.Add(MigrationStatuses.SITE_CODE_does_NOT_EXIST_in_GLM);
            lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SITE_CODE_ASSIGNMENT_TRIGGERING_in_GLM);
            lstMigrationStatuses.Add(MigrationStatuses.SITE_CODE_ASSIGNMENT_Successfully_TRIGGERED_in_GLM);
            lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_trying_to_TRIGGER_the_ASSIGNMENT_of_a_SITE_CODE_in_GLM);
            lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SITE_CODE_ASSIGNMENT_due_to_API_Call_Errors);
            lstMigrationStatuses.Add(MigrationStatuses.WORK_ITEM_QUEUED_in_GLM_for_DELAYED_SITE_CODE_ASSIGNMENT);
            lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SAP_SITE_LOCATION_SEARCH);
            lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_SAP_SITE_LOCATION_ADDRESS_SEARCH);
            lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_SAP_SITE_LOCATION_SEARCH_due_to_API_Call_Errors);
            lstMigrationStatuses.Add(MigrationStatuses.Site_Location_ADDRESS_FOUND_in_SAP);
            lstMigrationStatuses.Add(MigrationStatuses.Site_Location_ADDRESS_does_NOT_EXIST_in_SAP);
            lstMigrationStatuses.Add(MigrationStatuses.READY_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP);
            lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_attempting_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP);
            lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_attempts_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP_due_to_API_Call_Errors);
            lstMigrationStatuses.Add(MigrationStatuses.READY_for_ADDRESS_TYPE_BRANCHING_Logic_EVALUATION);
            lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_GLM_SERVICE_LOCATION_SEARCH);
            lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SERVICE_LOCATION_SEARCH);
            lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SERVICE_LOCATION_SEARCH_due_to_API_Call_Errors);
            lstMigrationStatuses.Add(MigrationStatuses.SERVICE_LOCATION_FOUND_in_GLM);
            lstMigrationStatuses.Add(MigrationStatuses.SERVICE_LOCATION_does_NOT_EXIST_in_GLM);
            lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SERVICE_LOCATION_CREATION_in_GLM);
            lstMigrationStatuses.Add(MigrationStatuses.SERVICE_LOCATION_Successfully_CREATED_in_GLM);
            lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_the_GLM_SERVICE_LOCATION_CREATION_attempt);
            lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SERVICE_LOCATION_CREATION_due_to_API_Call_Errors);
            lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_S_CODE_existence_CHECK);
            lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_S_CODE_existence_CHECK);
            lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_S_CODE_existence_CHECK_due_to_API_Call_Errors);
            lstMigrationStatuses.Add(MigrationStatuses.S_CODE_FOUND_in_GLM);
            lstMigrationStatuses.Add(MigrationStatuses.S_CODE_does_NOT_EXIST_in_GLM);
            lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_S_CODE_CREATION_in_GLM);
            lstMigrationStatuses.Add(MigrationStatuses.S_CODE_Successfully_CREATED_in_GLM);
            lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_S_CODE_CREATION_attempt);
            lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_S_CODE_CREATION_due_to_API_Call_Errors);
            lstMigrationStatuses.Add(MigrationStatuses.WORK_ITEM_QUEUED_in_GLM_for_DELAYED_S_CODE_CREATION);
            lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SAP_SERVICE_LOCATION_SEARCH);
            lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_SAP_SERVICE_LOCATION_ADDRESS_SEARCH);
            lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_SAP_SERVICE_LOCATION_SEARCH_due_to_API_Call_Errors);
            lstMigrationStatuses.Add(MigrationStatuses.Service_Location_ADDRESS_FOUND_in_SAP);
            lstMigrationStatuses.Add(MigrationStatuses.Service_Location_ADDRESS_does_NOT_EXIST_in_SAP);
            lstMigrationStatuses.Add(MigrationStatuses.READY_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP);
            lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_attempting_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP);
            lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_attempts_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.Processing_COMPLETE);
            //lstMigrationStatuses.Add(MigrationStatuses.IGNORED_indefinitely);
            return lstMigrationStatuses;
        }
        private List<MigrationStatuses> GetStatuses_CanBeRevertedByUser()
        {
            List<MigrationStatuses> lstMigrationStatuses = new List<MigrationStatuses>();

            //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_Processing);
            //lstMigrationStatuses.Add(MigrationStatuses.GLM_SITE_FOUND_or_CREATED);
            //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SITE_existence_CHECK);
            //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SITE_existence_CHECK_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SITE_CODE_existence_CHECK);
            //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SITE_CODE_existence_CHECK);
            //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SITE_CODE_existence_CHECK_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.SITE_CODE_FOUND_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.SITE_CODE_does_NOT_EXIST_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SITE_CODE_ASSIGNMENT_TRIGGERING_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.SITE_CODE_ASSIGNMENT_Successfully_TRIGGERED_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_trying_to_TRIGGER_the_ASSIGNMENT_of_a_SITE_CODE_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SITE_CODE_ASSIGNMENT_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.WORK_ITEM_QUEUED_in_GLM_for_DELAYED_SITE_CODE_ASSIGNMENT);
            //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SAP_SITE_LOCATION_SEARCH);
            //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_SAP_SITE_LOCATION_ADDRESS_SEARCH);
            //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_SAP_SITE_LOCATION_SEARCH_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.Site_Location_ADDRESS_FOUND_in_SAP);
            //lstMigrationStatuses.Add(MigrationStatuses.Site_Location_ADDRESS_does_NOT_EXIST_in_SAP);
            //lstMigrationStatuses.Add(MigrationStatuses.READY_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP);
            //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_attempting_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP);
            //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_attempts_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.READY_for_ADDRESS_TYPE_BRANCHING_Logic_EVALUATION);
            //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_GLM_SERVICE_LOCATION_SEARCH);
            //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SERVICE_LOCATION_SEARCH);
            //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SERVICE_LOCATION_SEARCH_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.SERVICE_LOCATION_FOUND_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.SERVICE_LOCATION_does_NOT_EXIST_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SERVICE_LOCATION_CREATION_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.SERVICE_LOCATION_Successfully_CREATED_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_the_GLM_SERVICE_LOCATION_CREATION_attempt);
            //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SERVICE_LOCATION_CREATION_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_S_CODE_existence_CHECK);
            //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_S_CODE_existence_CHECK);
            //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_S_CODE_existence_CHECK_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.S_CODE_FOUND_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.S_CODE_does_NOT_EXIST_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_S_CODE_CREATION_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.S_CODE_Successfully_CREATED_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_S_CODE_CREATION_attempt);
            //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_S_CODE_CREATION_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.WORK_ITEM_QUEUED_in_GLM_for_DELAYED_S_CODE_CREATION);
            //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SAP_SERVICE_LOCATION_SEARCH);
            //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_SAP_SERVICE_LOCATION_ADDRESS_SEARCH);
            //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_SAP_SERVICE_LOCATION_SEARCH_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.Service_Location_ADDRESS_FOUND_in_SAP);
            //lstMigrationStatuses.Add(MigrationStatuses.Service_Location_ADDRESS_does_NOT_EXIST_in_SAP);
            //lstMigrationStatuses.Add(MigrationStatuses.READY_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP);
            //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_attempting_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP);
            //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_attempts_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.Processing_COMPLETE);
            lstMigrationStatuses.Add(MigrationStatuses.IGNORED_indefinitely);
            return lstMigrationStatuses;
        }
        private List<MigrationStatuses> GetStatuses_CanBeForcedToProcessByUser()
        {
            List<MigrationStatuses> lstMigrationStatuses = new List<MigrationStatuses>();

            lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_Processing);
            //lstMigrationStatuses.Add(MigrationStatuses.GLM_SITE_FOUND_or_CREATED);
            //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SITE_existence_CHECK);
            //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SITE_existence_CHECK_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SITE_CODE_existence_CHECK);
            //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SITE_CODE_existence_CHECK);
            //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SITE_CODE_existence_CHECK_due_to_API_Call_Errors);
            lstMigrationStatuses.Add(MigrationStatuses.SITE_CODE_FOUND_in_GLM);
            lstMigrationStatuses.Add(MigrationStatuses.SITE_CODE_does_NOT_EXIST_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SITE_CODE_ASSIGNMENT_TRIGGERING_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.SITE_CODE_ASSIGNMENT_Successfully_TRIGGERED_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_trying_to_TRIGGER_the_ASSIGNMENT_of_a_SITE_CODE_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SITE_CODE_ASSIGNMENT_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.WORK_ITEM_QUEUED_in_GLM_for_DELAYED_SITE_CODE_ASSIGNMENT);
            lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SAP_SITE_LOCATION_SEARCH);
            //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_SAP_SITE_LOCATION_ADDRESS_SEARCH);
            //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_SAP_SITE_LOCATION_SEARCH_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.Site_Location_ADDRESS_FOUND_in_SAP);
            //lstMigrationStatuses.Add(MigrationStatuses.Site_Location_ADDRESS_does_NOT_EXIST_in_SAP);
            //lstMigrationStatuses.Add(MigrationStatuses.READY_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP);
            //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_attempting_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP);
            //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_attempts_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.READY_for_ADDRESS_TYPE_BRANCHING_Logic_EVALUATION);
            lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_GLM_SERVICE_LOCATION_SEARCH);
            //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SERVICE_LOCATION_SEARCH);
            //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SERVICE_LOCATION_SEARCH_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.SERVICE_LOCATION_FOUND_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.SERVICE_LOCATION_does_NOT_EXIST_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SERVICE_LOCATION_CREATION_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.SERVICE_LOCATION_Successfully_CREATED_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_the_GLM_SERVICE_LOCATION_CREATION_attempt);
            //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SERVICE_LOCATION_CREATION_due_to_API_Call_Errors);
            lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_S_CODE_existence_CHECK);
            //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_S_CODE_existence_CHECK);
            //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_S_CODE_existence_CHECK_due_to_API_Call_Errors);
            lstMigrationStatuses.Add(MigrationStatuses.S_CODE_FOUND_in_GLM);
            lstMigrationStatuses.Add(MigrationStatuses.S_CODE_does_NOT_EXIST_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_S_CODE_CREATION_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.S_CODE_Successfully_CREATED_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_S_CODE_CREATION_attempt);
            //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_S_CODE_CREATION_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.WORK_ITEM_QUEUED_in_GLM_for_DELAYED_S_CODE_CREATION);
            lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SAP_SERVICE_LOCATION_SEARCH);
            //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_SAP_SERVICE_LOCATION_ADDRESS_SEARCH);
            //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_SAP_SERVICE_LOCATION_SEARCH_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.Service_Location_ADDRESS_FOUND_in_SAP);
            //lstMigrationStatuses.Add(MigrationStatuses.Service_Location_ADDRESS_does_NOT_EXIST_in_SAP);
            //lstMigrationStatuses.Add(MigrationStatuses.READY_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP);
            //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_attempting_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP);
            //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_attempts_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.Processing_COMPLETE);
            //lstMigrationStatuses.Add(MigrationStatuses.IGNORED_indefinitely);
            return lstMigrationStatuses;
        }

        public bool ListContainsStatus(int intaddressStatusID, List<MigrationStatuses> lstMigrationStatus)
        {
            bool blnExistsInList = false;
            try
            {
                for (int i = 0; i < lstMigrationStatus.Count; i++)
                {
                    if (lstMigrationStatus[i] == (MigrationStatuses)intaddressStatusID)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                blnExistsInList = true;
            }
            return blnExistsInList;
        }



    }
}
