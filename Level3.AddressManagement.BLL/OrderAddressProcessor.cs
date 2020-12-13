using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;
using System.Diagnostics;

namespace Level3.AddressManagement.BLL
{
    public class OrderAddressProcessor
    {
        // declare a log4net logger
        private static ILog _objLogger = LogManager.GetLogger(typeof(OrderAddressProcessor));


        // Private Members
        private int _intOrderAddressID;
        private DAL.OrderAddress _objOrderAddress;
        private bool _blnIsLoaded;
        private bool _blnFirstStepHasExecuted;
        private bool _blnProcessNextStep;
        private string _strMultipleFieldLogIdentifier;
        private bool _blnTriggeredByUserRefresh;
        private string _strUsername = String.Empty;

        private int _intInadvertantInfiniteLoopCounterForAbort = 0;

        // Public Properties
        private List<string> _lstErrorMessages;
        public List<string> ErrorMessages
        {
            get { return _lstErrorMessages; }
            set { _lstErrorMessages = value; }
        }

        // Constructor
        public OrderAddressProcessor()
        {
            _blnIsLoaded = false;
            _lstErrorMessages = new List<string>();
            _blnProcessNextStep = false;
            _blnFirstStepHasExecuted = false;
        }


        // Public Methods
        public bool Load(int intOrderAddressID)
        {
            // Declare a base logging string
            string strBaseLoggingString = String.Format("OrderAddressID = [{0}]", intOrderAddressID.ToString());

            try
            {
                // Instantiate a stopwatch to write runtimes to a log file
                Stopwatch objStopWatch = new Stopwatch();
                objStopWatch.Start();

                // Set the local variable for the id
                _intOrderAddressID = intOrderAddressID;

                string strLastError = String.Empty;

                // Execute the query against CDW and load the results into the list of address records to be processed
                _objOrderAddress = new DAL.OrderAddress().Get(intOrderAddressID, out strLastError);

                if (_objOrderAddress == null)
                {
                    throw new Exception(String.Format("There was an error invoking the stored proc to pull the address record from the DB by its PK value.  Error Message = [{0}]", strLastError));
                }

                // Log the time to retrieve the date
                string strTimeElapsed = Model.StopwatchUtil.GetHumanReadableTimeElapsedString(objStopWatch);
                _objLogger.Debug(String.Concat("Time elapsed while retrieving the Order Address records from the DB so that it can be processed was [", strTimeElapsed, "]"));

            }
            catch (Exception ex)
            {
                // Create the error message
                string strErrorMessage = String.Format("There was an error while trying to retrieve/load the address record from the database so it can be processed.  Error Message = [{0}].  {1}", ex.Message, strBaseLoggingString);

                // Log a warning to the log file
                _objLogger.Warn(strErrorMessage);

                // Add the error message to the error list so that the caller can access it
                _lstErrorMessages.Add(strErrorMessage);
            }

            _blnIsLoaded = (_lstErrorMessages.Count == 0);

            return _blnIsLoaded;
        }
        public bool Process(bool blnTriggeredByUserRefresh = false, string strUsername = "")
        {
            // Instantiate a stopwatch to write runtimes to a log file
            Stopwatch objStopWatch = new Stopwatch();
            objStopWatch.Start();

            try
            {
                _intInadvertantInfiniteLoopCounterForAbort = 0;

                _blnTriggeredByUserRefresh = blnTriggeredByUserRefresh;
                _strUsername = strUsername;

                // Set the property used by the other methods in logging
                _strMultipleFieldLogIdentifier = OrderAddressUtil.CalcUniqueRecordIdentifierLoggingString(_objOrderAddress);

                // Ensure that the load method was called and succeeded
                EnforceLoadSuccess();

                // Declare a base logging string
                string strBaseLoggingString = OrderAddressUtil.CalcUniqueRecordIdentifierLoggingString(_objOrderAddress);

                // Process the next step for this object
                do
                {
                    // This will execute at least once
                    ProcessNextStep();

                    // Force an exit if there have been 5 failures, to avoid infinite looping if any of the APIs we are consuming are misbehaving or yielding unexpected results (for example:  Like GLM Site Code creation reporting success but not creating it) 
                    if (_intInadvertantInfiniteLoopCounterForAbort >= 3)
                    {
                        _blnProcessNextStep = false;
                    }
                }
                // Recursively process next steps until we hit a stopping point
                while (_blnProcessNextStep);



            }
            catch (Exception ex)
            {
                _objLogger.Warn(String.Format("There was an unhanlded error while trying to process the record.  Error Message = [{0}].  {1}", ex.Message, _strMultipleFieldLogIdentifier));
            }

            // Stop the timer
            objStopWatch.Stop();

            TimeSpan objTimeSpan = objStopWatch.Elapsed;

            // Log the time to retrieve the date
            string strTimeElapsed = Model.StopwatchUtil.GetHumanReadableTimeElapsedString(objStopWatch);
            _objLogger.Info(String.Concat("Time elapsed while Processing the Order Address record during this run was [", strTimeElapsed, "]", _strMultipleFieldLogIdentifier));

            long intTemp = objStopWatch.ElapsedTicks;

            // Add to the value in the DB that stores the total processing time
            AddProcessingTimeToRecordAndUpdate(objTimeSpan.Ticks);

            return (_lstErrorMessages.Count == 0);
        }
        public bool AdjustStatus(Model.MigrationStatuses enmMigrationStatus, bool blnTriggeredByUserRefresh = false, string strUsername = "")
        {
            // Declare a base logging string
            string strBaseLoggingString = OrderAddressUtil.CalcUniqueRecordIdentifierLoggingString(_objOrderAddress);
            _blnTriggeredByUserRefresh = blnTriggeredByUserRefresh;
            _strUsername = strUsername;

            try
            {
                _objOrderAddress.MigrationStatusID = (int)enmMigrationStatus;
                string strOrderAddressLogItemLogMessage = "The order address record's status was overridden by an end user.";

                UpdateAndReloadTheAddressRecord(strOrderAddressLogItemLogMessage);

            }
            catch (Exception ex)
            {
                // Create the error message
                string strErrorMessage = String.Format("There was an error while trying to adjust the status on the address record.  Error Message = [{0}].  {1}", ex.Message, strBaseLoggingString);

                // Log a warning to the log file
                _objLogger.Warn(strErrorMessage);

                // Add the error message to the error list so that the caller can access it
                _lstErrorMessages.Add(strErrorMessage);

            }

            return (_lstErrorMessages.Count == 0);
        }

        // Private Methods
        private void EnforceLoadSuccess()
        {
            if (_blnIsLoaded == false)
            {
                throw new Exception("The load method was not call or did not succeed, and so subsequent and dependant methods in this class cannot be invoked.");
            }
        }
        private void ProcessNextStep()
        {
            switch ((Model.MigrationStatuses)_objOrderAddress.MigrationStatusID)
            {

                // SITE 
                case Model.MigrationStatuses.STAGED_for_Processing:

                    Handle_1_STAGED_for_Processing();
                    break;

                case Model.MigrationStatuses.GLM_SITE_FOUND_or_CREATED:

                    Handle_2_GLM_SITE_FOUND_or_CREATED();
                    break;

                case Model.MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SITE_existence_CHECK:

                    Handle_3_API_Call_ERRORED_while_executing_GLM_SITE_existence_CHECK();
                    break;

                case Model.MigrationStatuses.ABANDONED_GLM_SITE_existence_CHECK_due_to_API_Call_Errors:

                    Handle_4_ABANDONED_GLM_SITE_existence_CHECK_due_to_API_Call_Errors();
                    break;

                // SITE CODE EXISTENCE
                case Model.MigrationStatuses.STAGED_for_SITE_CODE_existence_CHECK:

                    Handle_5_STAGED_for_SITE_CODE_existence_CHECK();
                    break;

                case Model.MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SITE_CODE_existence_CHECK:

                    Handle_6_API_Call_ERRORED_while_executing_GLM_SITE_CODE_existence_CHECK_();
                    break;

                case Model.MigrationStatuses.ABANDONED_GLM_SITE_CODE_existence_CHECK_due_to_API_Call_Errors:

                    Handle_7_ABANDONED_GLM_SITE_CODE_existence_CHECK_due_to_API_Call_Errors();
                    break;

                case Model.MigrationStatuses.SITE_CODE_FOUND_in_GLM:

                    Handle_8_SITE_CODE_FOUND_in_GLM();
                    break;

                case Model.MigrationStatuses.SITE_CODE_does_NOT_EXIST_in_GLM:

                    Handle_9_SITE_CODE_does_NOT_EXIST_in_GLM();
                    break;

                // SITE CODE CREATION
                case Model.MigrationStatuses.STAGED_for_SITE_CODE_ASSIGNMENT_TRIGGERING_in_GLM:

                    Handle_10_STAGED_for_SITE_CODE_ASSIGNMENT_TRIGGERING_in_GLM();
                    break;

                case Model.MigrationStatuses.SITE_CODE_ASSIGNMENT_Successfully_TRIGGERED_in_GLM:

                    Handle_11_SITE_CODE_ASSIGNMENT_Successfully_TRIGGERED_in_GLM();
                    break;

                case Model.MigrationStatuses.API_Call_ERRORED_while_trying_to_TRIGGER_the_ASSIGNMENT_of_a_SITE_CODE_in_GLM:

                    Handle_12_API_Call_ERRORED_while_trying_to_TRIGGER_the_ASSIGNMENT_of_a_SITE_CODE_in_GLM();
                    break;

                case Model.MigrationStatuses.ABANDONED_GLM_SITE_CODE_ASSIGNMENT_due_to_API_Call_Errors:

                    Handle_13_ABANDONED_GLM_SITE_CODE_ASSIGNMENT_due_to_API_Call_Errors();
                    break;

                case Model.MigrationStatuses.WORK_ITEM_QUEUED_in_GLM_for_DELAYED_SITE_CODE_ASSIGNMENT:

                    Handle_14_WORK_ITEM_QUEUED_in_GLM_for_DELAYED_SITE_CODE_ASSIGNMENT();
                    break;

                // SAP SITE LOCATION ADDRESS SEARCH
                case Model.MigrationStatuses.STAGED_for_SAP_SITE_LOCATION_SEARCH:

                    Handle_15_STAGED_for_SAP_SITE_LOCATION_SEARCH();
                    break;

                case Model.MigrationStatuses.API_Call_ERRORED_while_executing_SAP_SITE_LOCATION_ADDRESS_SEARCH:

                    Handle_16_API_Call_ERRORED_while_executing_SAP_SITE_LOCATION_ADDRESS_SEARCH();
                    break;

                case Model.MigrationStatuses.ABANDONED_SAP_SITE_LOCATION_SEARCH_due_to_API_Call_Errors:

                    Handle_17_ABANDONED_SAP_SITE_LOCATION_SEARCH_due_to_API_Call_Errors();
                    break;

                case Model.MigrationStatuses.Site_Location_ADDRESS_FOUND_in_SAP:

                    Handle_18_Site_Location_ADDRESS_FOUND_in_SAP();
                    break;

                case Model.MigrationStatuses.Site_Location_ADDRESS_does_NOT_EXIST_in_SAP:

                    Handle_19_Site_Location_ADDRESS_does_NOT_EXIST_in_SAP();
                    break;

                // SAP SITE LOCATION ADDRESS IMPORT
                case Model.MigrationStatuses.READY_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP:

                    Handle_20_READY_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP();
                    break;

                case Model.MigrationStatuses.API_Call_ERRORED_while_attempting_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP:

                    Handle_21_API_Call_ERRORED_while_attempting_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP();
                    break;

                case Model.MigrationStatuses.ABANDONED_attempts_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP_due_to_API_Call_Errors:

                    Handle_22_ABANDONED_attempts_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP_due_to_API_Call_Errors();
                    break;

                // BRANCHING LOGIC
                case Model.MigrationStatuses.READY_for_ADDRESS_TYPE_BRANCHING_Logic_EVALUATION:

                    Handle_23_READY_for_ADDRESS_TYPE_BRANCHING_Logic_EVALUATION();
                    break;


                // SERVICE LOCATION SEARCH
                case Model.MigrationStatuses.STAGED_for_GLM_SERVICE_LOCATION_SEARCH:

                    Handle_24_STAGED_for_GLM_SERVICE_LOCATION_SEARCH();
                    break;

                case Model.MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SERVICE_LOCATION_SEARCH:

                    Handle_25_API_Call_ERRORED_while_executing_GLM_SERVICE_LOCATION_SEARCH();
                    break;

                case Model.MigrationStatuses.ABANDONED_GLM_SERVICE_LOCATION_SEARCH_due_to_API_Call_Errors:

                    Handle_26_ABANDONED_GLM_SERVICE_LOCATION_SEARCH_due_to_API_Call_Errors();
                    break;

                case Model.MigrationStatuses.SERVICE_LOCATION_FOUND_in_GLM:

                    Handle_27_SERVICE_LOCATION_FOUND_in_GLM();
                    break;

                case Model.MigrationStatuses.SERVICE_LOCATION_does_NOT_EXIST_in_GLM:

                    Handle_28_SERVICE_LOCATION_does_NOT_EXIST_in_GLM();
                    break;


                // SERVICE LOCATION CREATION
                case Model.MigrationStatuses.STAGED_for_SERVICE_LOCATION_CREATION_in_GLM:

                    Handle_29_STAGED_for_SERVICE_LOCATION_CREATION_in_GLM();
                    break;

                case Model.MigrationStatuses.SERVICE_LOCATION_Successfully_CREATED_in_GLM:

                    Handle_30_SERVICE_LOCATION_Successfully_CREATED_in_GLM();
                    break;

                case Model.MigrationStatuses.API_Call_ERRORED_while_executing_the_GLM_SERVICE_LOCATION_CREATION_attempt:

                    Handle_31_API_Call_ERRORED_while_executing_the_GLM_SERVICE_LOCATION_CREATION_attempt();
                    break;

                case Model.MigrationStatuses.ABANDONED_GLM_SERVICE_LOCATION_CREATION_due_to_API_Call_Errors:

                    Handle_32_ABANDONED_GLM_SERVICE_LOCATION_CREATION_due_to_API_Call_Errors();
                    break;


                // S CODE EXISTENCE
                case Model.MigrationStatuses.STAGED_for_S_CODE_existence_CHECK:

                    Handle_33_STAGED_for_S_CODE_existence_CHECK();
                    break;

                case Model.MigrationStatuses.API_Call_ERRORED_while_executing_GLM_S_CODE_existence_CHECK:

                    Handle_34_API_Call_ERRORED_while_executing_GLM_S_CODE_existence_CHECK_();
                    break;

                case Model.MigrationStatuses.ABANDONED_GLM_S_CODE_existence_CHECK_due_to_API_Call_Errors:

                    Handle_35_ABANDONED_GLM_S_CODE_existence_CHECK_due_to_API_Call_Errors();
                    break;

                case Model.MigrationStatuses.S_CODE_FOUND_in_GLM:

                    Handle_36_S_CODE_FOUND_in_GLM();
                    break;

                case Model.MigrationStatuses.S_CODE_does_NOT_EXIST_in_GLM:

                    Handle_37_S_CODE_does_NOT_EXIST_in_GLM();
                    break;


                // S CODE CREATION
                case Model.MigrationStatuses.STAGED_for_S_CODE_CREATION_in_GLM:

                    Handle_38_STAGED_for_S_CODE_CREATION_in_GLM();
                    break;

                case Model.MigrationStatuses.S_CODE_Successfully_CREATED_in_GLM:

                    Handle_39_S_CODE_Successfully_CREATED_in_GLM();
                    break;

                case Model.MigrationStatuses.API_Call_ERRORED_while_executing_GLM_S_CODE_CREATION_attempt:

                    Handle_40_API_Call_ERRORED_while_executing_GLM_S_CODE_CREATION_attempt();
                    break;

                case Model.MigrationStatuses.ABANDONED_GLM_S_CODE_CREATION_due_to_API_Call_Errors:

                    Handle_41_ABANDONED_GLM_S_CODE_CREATION_due_to_API_Call_Errors();
                    break;

                case Model.MigrationStatuses.WORK_ITEM_QUEUED_in_GLM_for_DELAYED_S_CODE_CREATION:

                    Handle_42_WORK_ITEM_QUEUED_in_GLM_for_DELAYED_S_CODE_CREATION();
                    break;

                case Model.MigrationStatuses.STAGED_for_SAP_SERVICE_LOCATION_SEARCH:

                    Handle_43_STAGED_for_SAP_SERVICE_LOCATION_SEARCH();
                    break;

                case Model.MigrationStatuses.API_Call_ERRORED_while_executing_SAP_SERVICE_LOCATION_ADDRESS_SEARCH:

                    Handle_44_API_Call_ERRORED_while_executing_SAP_SERVICE_LOCATION_ADDRESS_SEARCH();
                    break;

                case Model.MigrationStatuses.ABANDONED_SAP_SERVICE_LOCATION_SEARCH_due_to_API_Call_Errors:

                    Handle_45_ABANDONED_SAP_SERVICE_LOCATION_SEARCH_due_to_API_Call_Errors();
                    break;

                case Model.MigrationStatuses.Service_Location_ADDRESS_FOUND_in_SAP:

                    Handle_46_Service_Location_ADDRESS_FOUND_in_SAP();
                    break;

                case Model.MigrationStatuses.Service_Location_ADDRESS_does_NOT_EXIST_in_SAP:

                    Handle_47_Service_Location_ADDRESS_does_NOT_EXIST_in_SAP();
                    break;

                case Model.MigrationStatuses.READY_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP:

                    Handle_48_READY_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP();
                    break;

                case Model.MigrationStatuses.API_Call_ERRORED_while_attempting_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP:

                    Handle_49_API_Call_ERRORED_while_attempting_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP();
                    break;

                case Model.MigrationStatuses.ABANDONED_attempts_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP_due_to_API_Call_Errors:

                    Handle_50_ABANDONED_attempts_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP_due_to_API_Call_Errors();
                    break;

                case Model.MigrationStatuses.Processing_COMPLETE:

                    Handle_51_Processing_COMPLETE();
                    break;

                case Model.MigrationStatuses.IGNORED_indefinitely:

                    Handle_52_IGNORED_indefinitely();
                    break;
                default:
                    throw new Exception("Did you add a new status to the system, but forget to add a case to the switch statement to handle that scenario?  Get it together dorkus! :)");
            }

        }

        private void AddProcessingTimeToRecordAndUpdate(Int64 intNumberOfTicksToProcessThisTime)
        {
            try
            {
                long intTotalTimeBeforeThisProcessing;
                if (String.IsNullOrEmpty(_objOrderAddress.TotalProcessingTimeInTickString))
                {
                    _objOrderAddress.TotalProcessingTimeInTickString = "0";
                }

                if (long.TryParse(_objOrderAddress.TotalProcessingTimeInTickString, out intTotalTimeBeforeThisProcessing))
                {
                    long intNewTotal = intTotalTimeBeforeThisProcessing + intNumberOfTicksToProcessThisTime;

                    _objOrderAddress.TotalProcessingTimeInTickString = intNewTotal.ToString();
                    _objOrderAddress.TotalProcessingTimeAsHumanReadable = Model.StopwatchUtil.GetHumanReadableTimeElapsedStringFromTicks(intNewTotal);

                    // Update the record
                    Update();
                }
                else
                {
                    throw new Exception("There was an ERROR while trying to parse the existing time value stored in the DB to an integer so it can be added to the latest processing time interval.");
                }
            }
            catch (Exception ex)
            {
                // Eat the errror
                _objLogger.Error(String.Format("There was an ERROR while trying to calculate the Processing time for the record, and to update it in the DB.  Error Message = [{0}].  {1}", ex.Message, _strMultipleFieldLogIdentifier));
            }

        }

        // GLM SITE LOCATION -----------------------------------------------

        /// <summary>
        /// // Calls the GLM API to Search For or Create the Service Location that corresponds to the CLII or RAW ADDRESS fields supplied 
        /// </summary>
        private void Handle_1_STAGED_for_Processing()
        {
            SearchOrCreateSiteInGLM();
        }

        /// <summary>
        /// Updates the STATUS on the record in order to PREP if for the next step which will check its SITE CODE
        /// </summary>
        private void Handle_2_GLM_SITE_FOUND_or_CREATED()
        {
            _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.STAGED_for_SITE_CODE_existence_CHECK;
            string strOrderAddressLogItemLogMessage = "A call the the GLM API to capture the SITE CODE value has been staged and is forthcoming.";

            UpdateAndReloadTheAddressRecord(strOrderAddressLogItemLogMessage);

            _blnProcessNextStep = true;
        }

        /// <summary>
        /// // REPEATs the Call the GLM API to Search For or Create the Service Location that corresponds to the CLII or RAW ADDRESS fields supplied 
        /// </summary>
        private void Handle_3_API_Call_ERRORED_while_executing_GLM_SITE_existence_CHECK()
        {
            SearchOrCreateSiteInGLM();
        }

        /// <summary>
        /// Unless the calling action was triggered by a user (_blnTriggeredByUserRefresh == true), this method will do nothing.  If it is triggered by a user, then another attempt to SEARCH FOR or CREATE the SITE LOCATION record in GLM will be made.
        /// </summary>
        private void Handle_4_ABANDONED_GLM_SITE_existence_CHECK_due_to_API_Call_Errors()
        {
            if (_blnTriggeredByUserRefresh)
            {
                // Since a user has intiated the refresh, trigger the Search Again
                SearchOrCreateSiteInGLM();
            }
            else
            {
                // Do nothing to this record, as it is awaiting user action
            }
        }



        // SITE CODE EXISTENCE CHECK -----------------------------------------------

        /// <summary>
        /// Calls the GLM Auto-Complete API using the stored PLNumber to check if the Site has a SITE CODE
        /// </summary>
        private void Handle_5_STAGED_for_SITE_CODE_existence_CHECK()
        {
            SearchGLMForSiteCode();
        }

        /// <summary>
        ///  REPEATS the Call to the GLM Auto-Complete API to check if the Site has been assigned a SITE CODE
        /// </summary>
        private void Handle_6_API_Call_ERRORED_while_executing_GLM_SITE_CODE_existence_CHECK_()
        {
            SearchGLMForSiteCode();
        }

        /// <summary>
        /// Unless the calling action was triggered by a user (_blnTriggeredByUserRefresh == true), this method will do nothing.  If it is triggered by a user, then another attempt to check for the SITE CODE record in GLM will be made.
        /// </summary>
        private void Handle_7_ABANDONED_GLM_SITE_CODE_existence_CHECK_due_to_API_Call_Errors()
        {
            if (_blnTriggeredByUserRefresh)
            {
                // Since a user has intiated the refresh, trigger the check Again
                SearchGLMForSiteCode();
            }
            else
            {
                // Do nothing to this record, as it is awaiting user action
            }
        }

        /// <summary>
        /// Updates the STATUS on the record in order to PREP if for the next step which will check for the ADDRESS in SAP using the PLNumber
        /// </summary>
        private void Handle_8_SITE_CODE_FOUND_in_GLM()
        {

            _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.STAGED_for_SAP_SITE_LOCATION_SEARCH;
            string strOrderAddressLogItemLogMessage = "The record is awaiting a call to the SAP API, to check for the existence of the SITE LOCATION'S ADDRESS in that system.";

            UpdateAndReloadTheAddressRecord(strOrderAddressLogItemLogMessage);

            _blnProcessNextStep = true;

            _intInadvertantInfiniteLoopCounterForAbort = 0;
        }

        /// <summary>
        /// Updates the STATUS on the record in order to PREP if for the next step which will attempt to ASSIGN a SITE CODE to the record in GLM
        /// </summary>
        private void Handle_9_SITE_CODE_does_NOT_EXIST_in_GLM()
        {
            _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.STAGED_for_SITE_CODE_ASSIGNMENT_TRIGGERING_in_GLM;
            string strOrderAddressLogItemLogMessage = "The record is awaiting a call to the GLM API, to trigger the assignment of a SITE CODE.";


            UpdateAndReloadTheAddressRecord(strOrderAddressLogItemLogMessage);

            _blnProcessNextStep = true;

            _intInadvertantInfiniteLoopCounterForAbort++;
        }



        // SITE CODE CREATION -----------------------------------------------

        /// <summary>
        /// Calls the GLM API to TRIGGER the ASSIGNMENT/CREATION of a SITE CODE
        /// </summary>
        private void Handle_10_STAGED_for_SITE_CODE_ASSIGNMENT_TRIGGERING_in_GLM()
        {
            CreateSiteCodeInGLM();
        }

        /// <summary>
        /// REVERT the STATUS on the record so that another check for the API is run, now that it has supposidly been created AND to store the new value in the DB since it does NOT come in the response to the CREATE API Call
        /// </summary>
        private void Handle_11_SITE_CODE_ASSIGNMENT_Successfully_TRIGGERED_in_GLM()
        {
            _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.STAGED_for_SITE_CODE_existence_CHECK;
            string strOrderAddressLogItemLogMessage = "The GLM API indicated that a SITE CODE was successfully created, but another call to the API is needed to capture the new SITE CODE value and to ensure that things worked as intended.";

            UpdateAndReloadTheAddressRecord(strOrderAddressLogItemLogMessage);

            _blnProcessNextStep = true;
        }

        /// <summary>
        /// REPEATS the Call to the GLM API that attempts to TRIGGER the ASSIGNMENT/CREATION of a SITE CODE
        /// </summary>
        private void Handle_12_API_Call_ERRORED_while_trying_to_TRIGGER_the_ASSIGNMENT_of_a_SITE_CODE_in_GLM()
        {
            CreateSiteCodeInGLM();
        }

        /// <summary>
        /// Unless the calling action was triggered by a user(_blnTriggeredByUserRefresh == true), this method will do nothing.  If it is triggered by a user, then another attempt to Trigger the ASSIGNMENT of a SITE CODE in GLM will be made.
        /// </summary>
        private void Handle_13_ABANDONED_GLM_SITE_CODE_ASSIGNMENT_due_to_API_Call_Errors()
        {
            if (_blnTriggeredByUserRefresh)
            {
                // Since a user has intiated the refresh, trigger the check Again
                CreateSiteCodeInGLM();
            }
            else
            {
                // Do nothing to this record, as it is awaiting user action
            }
        }

        /// <summary>
        /// Updates the STATUS on the record in order to PREP it for the next step which will check to see if the BASE, SITE LOCATION ADDRESS RECORD already exists in SAP
        /// </summary>
        private void Handle_14_WORK_ITEM_QUEUED_in_GLM_for_DELAYED_SITE_CODE_ASSIGNMENT()
        {
            _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.STAGED_for_SAP_SITE_LOCATION_SEARCH;
            string strOrderAddressLogItemLogMessage = "The record is awaiting a call to the GLM API, which will SEARCH FOR or CREATE teh SITE LOCATION.";

            UpdateAndReloadTheAddressRecord(strOrderAddressLogItemLogMessage);

            _blnProcessNextStep = true;
        }



        // SAP SITE LOCATION SEARCH -----------------------------------------------

        /// <summary>
        /// Calls the SAP API to SEARCH for the SITE LOCATION ADDRESS using the stored PLNumber
        /// </summary>
        private void Handle_15_STAGED_for_SAP_SITE_LOCATION_SEARCH()
        {
            SearchForSiteLocationAddressInSAP();
        }

        /// <summary>
        /// REPEATS the Call to the SAP API that attempts to SEARCH for the SITE LOCATION ADDRESS using the stored PLNumber
        /// </summary>
        private void Handle_16_API_Call_ERRORED_while_executing_SAP_SITE_LOCATION_ADDRESS_SEARCH()
        {
            SearchForSiteLocationAddressInSAP();
        }

        /// <summary>
        ///  Unless the calling action was triggered by a user(_blnTriggeredByUserRefresh == true), this method will do nothing.  If it is triggered by a user, then another attempt to SEARCH for the SITE LOCATION ADDRESS in SAP will be made.
        /// </summary>
        private void Handle_17_ABANDONED_SAP_SITE_LOCATION_SEARCH_due_to_API_Call_Errors()
        {
            if (_blnTriggeredByUserRefresh)
            {
                // Since a user has intiated the refresh, trigger the check Again
                SearchForSiteLocationAddressInSAP();
            }
            else
            {
                // Do nothing to this record, as it is awaiting user action
            }
        }

        /// <summary>
        /// Updates the STATUS on the record in order to PREP it for the next step, which will choose where the transactino goes from here based on its ADDRESS TYPE (SITE LOCATION vs SERVICE LOCATION)
        /// </summary>
        private void Handle_18_Site_Location_ADDRESS_FOUND_in_SAP()
        {
            _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.READY_for_ADDRESS_TYPE_BRANCHING_Logic_EVALUATION;
            string strOrderAddressLogItemLogMessage = "The record is awaiting additional analysis, which will determine the next step in processing this address.";

            UpdateAndReloadTheAddressRecord(strOrderAddressLogItemLogMessage);

            _blnProcessNextStep = true;
        }

        /// <summary>
        /// Update the STATUS on the reocrd in order to PREP it for the next step, which will try to Force SAP to import the record from GLM
        /// </summary>
        private void Handle_19_Site_Location_ADDRESS_does_NOT_EXIST_in_SAP()
        {
            _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.READY_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP;
            string strOrderAddressLogItemLogMessage = "The record is awaiting a call to the SAP API, which will attempt to tigger the import of the record from GLM into SAP.";

            UpdateAndReloadTheAddressRecord(strOrderAddressLogItemLogMessage);

            _blnProcessNextStep = true;
        }



        //  TRIGGER SAP ADDRESS IMPORT -----------------------------------------------

        /// <summary>
        /// Calls the SAP API to TRIGGER the IMPORT of the SITE LOCATION and its SERVICE LOCATIONS (circumventing and/or pre-empting GLM and EMP).  While SAP processes this request right away, it does so ASYC since the call to GLM, and so we PAUSE processing after the attempt to trigger the event is made
        /// </summary>
        private void Handle_20_READY_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP()
        {
            ForceSAPSiteAddressCreation();
        }

        /// <summary>
        /// REPEATS the Call to the SAP API to TRIGGER THE IMPORT of the SITE LOCATION and its SERVICE LOCATIONS (circumventing and/or pre-empting GLM and EMP)
        /// </summary>
        private void Handle_21_API_Call_ERRORED_while_attempting_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP()
        {
            ForceSAPSiteAddressCreation();
        }

        /// <summary>
        ///  Unless the calling action was triggered by a user(_blnTriggeredByUserRefresh == true), this method will do nothing.  If it is triggered by a user, then another attempt to TRIGGER the IMPORT of the SITE LOCATION and its SERIVE LOCATIONs into  SAP will be made.
        /// </summary>
        private void Handle_22_ABANDONED_attempts_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP_due_to_API_Call_Errors()
        {
            if (_blnTriggeredByUserRefresh)
            {
                // Since a user has intiated the refresh, trigger the check Again
                ForceSAPSiteAddressCreation();
            }
            else
            {
                // Do nothing to this record, as it is awaiting user action
            }
        }


        // BRANCHING LOGIC -----------------------------------------------

        /// <summary>
        /// CHECKs the ADDRESS TYPE to either COMPLETE processing (SITE LOCATION) or PREP's the record for FURTHER PROCESSING as a SERVICE LOCATION
        /// </summary>
        private void Handle_23_READY_for_ADDRESS_TYPE_BRANCHING_Logic_EVALUATION()
        {
            ExecuteBranchingLogic();
        }


        // SERVICE LOCATION SEARCH -----------------------------------------------

        /// <summary>
        /// Calls the GLM API to see if a SERVICE LOCATION with the Floor, Room, and Suite values belonging to the address EXISTS in GLM
        /// </summary>
        private void Handle_24_STAGED_for_GLM_SERVICE_LOCATION_SEARCH()
        {
            SearchGLMForServiceLocation();
        }

        /// <summary>
        /// REPEATS the Call to the GLM API to SEARCH for the SERVICE LOCATION
        /// </summary>
        private void Handle_25_API_Call_ERRORED_while_executing_GLM_SERVICE_LOCATION_SEARCH()
        {
            SearchGLMForServiceLocation();
        }

        /// <summary>
        /// Unless the calling action was triggered by a user(_blnTriggeredByUserRefresh == true), this method will do nothing.  If it is triggered by a user, then another attempt to SEARCH for the SERVICE LOCATION in GLM.
        /// </summary>
        private void Handle_26_ABANDONED_GLM_SERVICE_LOCATION_SEARCH_due_to_API_Call_Errors()
        {
            if (_blnTriggeredByUserRefresh)
            {
                // Since a user has intiated the refresh, trigger the check Again
                SearchGLMForServiceLocation();
            }
            else
            {
                // Do nothing to this record, as it is awaiting user action
            }
        }

        /// <summary>
        ///  Updates the STATUS on the record in order to PREP if for the next step which will check to see if the SERVICE LOCATION has been ASSIGNED an S CODE in GLM
        /// </summary>
        private void Handle_27_SERVICE_LOCATION_FOUND_in_GLM()
        {
            _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.STAGED_for_S_CODE_existence_CHECK;
            string strOrderAddressLogItemLogMessage = "The record is awaiting a call to the GLM API, to check whether the SERVICE LOCATION has an S CODE value assigned.";

            UpdateAndReloadTheAddressRecord(strOrderAddressLogItemLogMessage);

            _blnProcessNextStep = true;
        }

        /// <summary>
        /// Updates the STATUS on the record in order to PREP if for the next step which will call GLM to try to CREATE the NEW SERVICE LOCATION
        /// </summary>
        private void Handle_28_SERVICE_LOCATION_does_NOT_EXIST_in_GLM()
        {
            _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.STAGED_for_SERVICE_LOCATION_CREATION_in_GLM;
            string strOrderAddressLogItemLogMessage = "The record is awaiting a call to the GLM API, which will attempt to add the NEW SERVICE LOCATION to the EXISTING site location.";

            UpdateAndReloadTheAddressRecord(strOrderAddressLogItemLogMessage);

            _blnProcessNextStep = true;
        }



        // SERVICE LOCATION CREATION -----------------------------------------------

        /// <summary>
        /// Calls the GLM API to CREATE a NEW SERVICE LOCATION with the Floor, Room, and Suite on this record
        /// </summary>
        private void Handle_29_STAGED_for_SERVICE_LOCATION_CREATION_in_GLM()
        {
            CreateServiceLocationInGLM();
        }

        /// <summary>
        /// REVERT the STATUS on the record so that another check for the SERVUCE LOCATION is run, now that it has supposidly been created AND to store the SL Number in the DB since it does NOT come in the response to the CREATE API Call
        /// </summary>
        private void Handle_30_SERVICE_LOCATION_Successfully_CREATED_in_GLM()
        {
            _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.STAGED_for_GLM_SERVICE_LOCATION_SEARCH;
            string strOrderAddressLogItemLogMessage = "The GLM API indicated that a SERVICE LOCATION was successfully created, but another call to the API is needed to capture the new SL Number value and to ensure that things worked as intended.";

            UpdateAndReloadTheAddressRecord(strOrderAddressLogItemLogMessage);

            _blnProcessNextStep = true;
        }

        /// <summary>
        /// REPEATS the Call to the GLM API to CREATE the NEW SERVICE LOCATION
        /// </summary>
        private void Handle_31_API_Call_ERRORED_while_executing_the_GLM_SERVICE_LOCATION_CREATION_attempt()
        {
            CreateServiceLocationInGLM();
        }

        /// <summary>
        /// Unless the calling action was triggered by a user(_blnTriggeredByUserRefresh == true), this method will do nothing.  If it is triggered by a user, then another attempt to CREATE the NEW SERVICE LOCATION in GLM is made.
        /// </summary>
        private void Handle_32_ABANDONED_GLM_SERVICE_LOCATION_CREATION_due_to_API_Call_Errors()
        {
            if (_blnTriggeredByUserRefresh)
            {
                // Since a user has intiated the refresh, trigger the check Again
                CreateServiceLocationInGLM();
            }
            else
            {
                // Do nothing to this record, as it is awaiting user action
            }
        }



        // S CODE EXISTENCE CHECK -----------------------------------------------

        /// <summary>
        /// Calls the GLM API to RETIEVE the DETAILS of the SITE LOCATION and the SERVICE LOCATION to DETERMINE if it has been ASSIGNED a SITE CODE
        /// </summary>
        private void Handle_33_STAGED_for_S_CODE_existence_CHECK()
        {
            SearchGLMForSCode();
        }

        /// <summary>
        ///  REPEATS toe Call to the GLM API to DETERMINE if a SITE CODE has been ASSIGNED to the SERVICE LOCATION
        /// </summary>
        private void Handle_34_API_Call_ERRORED_while_executing_GLM_S_CODE_existence_CHECK_()
        {
            SearchGLMForSCode();
        }

        /// <summary>
        /// Unless the calling action was triggered by a user(_blnTriggeredByUserRefresh == true), this method will do nothing.  If it is triggered by a user, then another attempt to CHECK GLM to see if the SERVICE LOCATION has a SITE CODE.
        /// </summary>
        private void Handle_35_ABANDONED_GLM_S_CODE_existence_CHECK_due_to_API_Call_Errors()
        {
            if (_blnTriggeredByUserRefresh)
            {
                // Since a user has intiated the refresh, trigger the check Again
                SearchGLMForSCode();
            }
            else
            {
                // Do nothing to this record, as it is awaiting user action
            }
        }

        /// <summary>
        ///  Updates the STATUS on the record in order to PREP if for the next step which will check for the ADDRESS in SAP using the SLNumber
        /// </summary>
        private void Handle_36_S_CODE_FOUND_in_GLM()
        {
            _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.STAGED_for_SAP_SERVICE_LOCATION_SEARCH;
            string strOrderAddressLogItemLogMessage = "The record is awaiting a call to the SAP API, which will check to see if the SERVICE LOCATION address already exists in the pool of SAP 'delivery' addresses.";

            UpdateAndReloadTheAddressRecord(strOrderAddressLogItemLogMessage);

            _blnProcessNextStep = true;

            _intInadvertantInfiniteLoopCounterForAbort = 0;
        }

        /// <summary>
        /// Updates the STATUS on the record in order to PREP if for the next step which will attempt to ASSIGN an S CODE to the record in GLM
        /// </summary>
        private void Handle_37_S_CODE_does_NOT_EXIST_in_GLM()
        {
            _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.STAGED_for_S_CODE_CREATION_in_GLM;
            string strOrderAddressLogItemLogMessage = "The record is awaiting a call to the GLM API, which will attempt to trigger the assignment of an S CODE.";

            UpdateAndReloadTheAddressRecord(strOrderAddressLogItemLogMessage);

            _intInadvertantInfiniteLoopCounterForAbort++;

            _blnProcessNextStep = true;
        }



        // S CODE CREATION -----------------------------------------------


        /// <summary>
        /// Calls the GLM API to TRIGGER the ASSIGNMENT/CREATION of an S CODE
        /// </summary>
        private void Handle_38_STAGED_for_S_CODE_CREATION_in_GLM()
        {
            CreateSCodeInGLM();
        }

        /// <summary>
        /// REVERT the STATUS on the record so that another check for the S CODE is run, now that it has supposidly been created AND to store the new value in the DB since it does NOT come in the response to the CREATE API Call
        /// </summary>
        private void Handle_39_S_CODE_Successfully_CREATED_in_GLM()
        {
            _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.STAGED_for_S_CODE_existence_CHECK;
            string strOrderAddressLogItemLogMessage = "The GLM API indicated that an S CODE was successfully created, but another call to the API is needed to capture the new S CODE value and to ensure that things worked as intended.";

            UpdateAndReloadTheAddressRecord(strOrderAddressLogItemLogMessage);

            _blnProcessNextStep = true;
        }

        /// <summary>
        /// REPEATS the Call to the GLM API that attempts to TRIGGER the ASSIGNMENT/CREATION of an S CODE
        /// </summary>
        private void Handle_40_API_Call_ERRORED_while_executing_GLM_S_CODE_CREATION_attempt()
        {
            CreateSCodeInGLM();
        }

        /// <summary>
        /// Unless the calling action was triggered by a user(_blnTriggeredByUserRefresh == true), this method will do nothing.  If it is triggered by a user, then another attempt to Trigger the ASSIGNMENT of an S CODE in GLM will be made.
        /// </summary>
        private void Handle_41_ABANDONED_GLM_S_CODE_CREATION_due_to_API_Call_Errors()
        {
            if (_blnTriggeredByUserRefresh)
            {
                // Since a user has intiated the refresh, trigger the check Again
                CreateSCodeInGLM();
            }
            else
            {
                // Do nothing to this record, as it is awaiting user action
            }
        }

        /// <summary>
        /// Updates the STATUS on the record in order to PREP it for the next step which will check to see if the SERVICE LOCATION ADDRESS RECORD already exists in SAP
        /// </summary>
        private void Handle_42_WORK_ITEM_QUEUED_in_GLM_for_DELAYED_S_CODE_CREATION()
        {
            _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.STAGED_for_SAP_SERVICE_LOCATION_SEARCH;
            string strOrderAddressLogItemLogMessage = "The call to the GLM API to trigger the assignment of an S CODE succeeded BUT the assignment will take up to 48 hours to complete.";

            UpdateAndReloadTheAddressRecord(strOrderAddressLogItemLogMessage);

            _blnProcessNextStep = true;
        }


        // SAP SERVICE LOCATION SEARCH -----------------------------------------------

        /// <summary>
        /// Calls the SAP API to SEARCH for the SERVICE LOCATION ADDRESS using the stored SLNumber
        /// </summary>
        private void Handle_43_STAGED_for_SAP_SERVICE_LOCATION_SEARCH()
        {
            SearchForServiceLocationInSAP();
        }

        /// <summary>
        /// REPEATS the Call to the SAP API that attempts to SEARCH for the SERVICE LOCATION ADDRESS using the stored SLNumber
        /// </summary>
        private void Handle_44_API_Call_ERRORED_while_executing_SAP_SERVICE_LOCATION_ADDRESS_SEARCH()
        {
            SearchForServiceLocationInSAP();
        }

        /// <summary>
        ///  Unless the calling action was triggered by a user(_blnTriggeredByUserRefresh == true), this method will do nothing.  If it is triggered by a user, then another attempt to SEARCH for the SERVICE LOCATION ADDRESS in SAP will be made.
        /// </summary>
        private void Handle_45_ABANDONED_SAP_SERVICE_LOCATION_SEARCH_due_to_API_Call_Errors()
        {
            if (_blnTriggeredByUserRefresh)
            {
                // Since a user has intiated the refresh, trigger the check Again
                SearchForServiceLocationInSAP();
            }
            else
            {
                // Do nothing to this record, as it is awaiting user action
            }
        }

        /// <summary>
        /// Updates the STATUS on the record in order to COMPLETE it, since we are now done
        /// </summary>
        private void Handle_46_Service_Location_ADDRESS_FOUND_in_SAP()
        {
            SetServiceLocationAddressToComplete();
        }

        /// <summary>
        /// Update the STATUS on the reocrd in order to PREP it for the next step, which will try to Force SAP to import the record from GLM
        /// </summary>
        private void Handle_47_Service_Location_ADDRESS_does_NOT_EXIST_in_SAP()
        {
            _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.READY_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP;
            string strOrderAddressLogItemLogMessage = "The record is awaiting a call to the SAP API, which will attempt to trigger the import of the SERVICE LOCATION and its SITE LOCATION from GLM.";

            UpdateAndReloadTheAddressRecord(strOrderAddressLogItemLogMessage);

            _blnProcessNextStep = true;
        }


        /// <summary>
        /// Calls the SAP API to TRIGGER the IMPORT of the SITE LOCATION and its SERVICE LOCATIONS (circumventing and/or pre-empting GLM and EMP).  While SAP processes this request right away, it does so ASYC since the call to GLM, and so we PAUSE processing after the attempt to trigger the event is made
        /// </summary>
        private void Handle_48_READY_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP()
        {
            ForceSAPServiceLocationAddressCreation();
        }

        /// <summary>
        /// REPEATS the Call to the SAP API to TRIGGER THE IMPORT of the SITE LOCATION and its SERVICE LOCATIONS (circumventing and/or pre-empting GLM and EMP)
        /// </summary>
        private void Handle_49_API_Call_ERRORED_while_attempting_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP()
        {
            ForceSAPServiceLocationAddressCreation();
        }

        /// <summary>
        ///  Unless the calling action was triggered by a user(_blnTriggeredByUserRefresh == true), this method will do nothing.  If it is triggered by a user, then another attempt to TRIGGER the IMPORT of the SITE LOCATION and its SERIVE LOCATIONs into  SAP will be made.
        /// </summary>
        private void Handle_50_ABANDONED_attempts_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP_due_to_API_Call_Errors()
        {
            if (_blnTriggeredByUserRefresh)
            {
                // Since a user has intiated the refresh, trigger the check Again
                ForceSAPServiceLocationAddressCreation();
            }
            else
            {
                // Do nothing to this record, as it is awaiting user action
            }
        }

        /// <summary>
        /// Does Nothing since this status is the end of the line
        /// </summary>
        private void Handle_51_Processing_COMPLETE()
        {
            // We are DONE, so do NOTHING
            _blnProcessNextStep = false;
        }

        /// <summary>
        /// Does Nothing since this status is intentionally a Dead end
        /// </summary>
        private void Handle_52_IGNORED_indefinitely()
        {
            // We are DONE, so do NOTHING
            _blnProcessNextStep = false;
        }



        // SITE LOCATION -----------------------

        /// <summary>
        /// Calls GLM's API to SEARCH by CLII or by RAW ADDRESS fields, and then sets the PL Number, Status, and failure count accordingly
        /// </summary>
        private void SearchOrCreateSiteInGLM()
        {
            // Search/Create for the Site in GLM... via the API
            // If we have a valid CLII, then search by CLII
            // If we don't have a valid CLII, then search by raw address fields

            string strOrderAddressLogItemLogMessage = String.Empty;

            // Instantiate a GLM Call Manager, to be used to invoke the GLM Web Service APIs
            RAL.GLMCallManager objGLMCallManager = new RAL.GLMCallManager(ConfigHelper.GetGLMBaseUrl(), ConfigHelper.GetGLMAuthorizationHeaderUsername(), ConfigHelper.GetGLMAuthorizationHeaderApplicationID(), _objOrderAddress.OrderAddressID.Value);

            // Declare a variable to reflect whether the record was found in GLM
            bool blnFoundOrCreated = false;
            string strPLNumber = String.Empty;
            string strSiteCode = String.Empty;


            // SEARCH GLM BY CLII -> Get the CLII that is stored in our custom DB table
            if (_objOrderAddress.ValidCLII.Value)
            {
                // Trim the value
                string strCLII = _objOrderAddress.CDWCLII.Trim().ToUpper();

                _objLogger.Debug(String.Format("The order address has a valid CLII, and so we are about to try to search for the record in GLM.  CLII = [{0}]. {1}", strCLII, _strMultipleFieldLogIdentifier));

                // Call the GLM API
                List<Model.ResponseItem> lstResponseItems = objGLMCallManager.GetAddressFromGLMByMasterSideIDOrCLII(strCLII);

                // Since the GLM API Response is a bit cryptic, use our Utility to try to parse out the PLNumber
                // Intialize the values
                strPLNumber = String.Empty;
                string strErrorMessage;

                // Try to parse the value
                if (BLL.GLMResponseUtil.TryGetPLNumberForCLII(strCLII, lstResponseItems, out strPLNumber, out strErrorMessage))
                {
                    // If we have a PL Number, then the parsing worked and we should have a value for the PLNumber that came from GLM
                    blnFoundOrCreated = strPLNumber != String.Empty;
                    strSiteCode = strCLII;

                    if (blnFoundOrCreated)
                    {
                        _objLogger.Info(String.Format("The order address was found in GLM using the valid CLII retrieved from CDW.  CLII = [{0}], PL Number = [{1}].  {2}", strCLII, strPLNumber, _strMultipleFieldLogIdentifier));
                        strOrderAddressLogItemLogMessage = String.Format("The base address record (Site Location) was found in GLM using the CLII ({0}) sourced from CDW.  ", strCLII);
                    }
                    else
                    {
                        // The value was not found... so log a warning to indicate that we have a "bad" CLII from CDW and set the variable that will cause a secondary GLM search by raw address fields
                        strOrderAddressLogItemLogMessage = String.Format("A CLII ({0}) was pulled from CDW, but the address could NOT be found in GLM using that value.  ", strCLII);

                        _objLogger.Warn(String.Format("The order address could NOT be found using the valid CLII retrieved from the CDW, and so the order address does not have a CLII, and so GLM must be searched using the raw address fields. Valid but BAD CLII = [{0}]. {1}", strCLII, _strMultipleFieldLogIdentifier));
                        blnFoundOrCreated = false;
                    }
                }
                else
                {
                    // The value could not be parsed, so something went really wrong... throw an exception so IT support gets an email and/or has a log of the error, and then set the variable that will cause a secondary GLM search by raw address fields.
                    strOrderAddressLogItemLogMessage = String.Format("A CLII ({0}) was pulled from CDW, but the address could NOT be found in GLM using that value because the attempt to PARSE the response from GLM FAILED.  Error Message = [{1}] ", strCLII, strErrorMessage);
                    _objLogger.Error(String.Format("The attempt to parse the response from GLM while searching for the PL Number by CLII failed unexepectedly.  Valid but BAD CLII = [{0}]. {1}  Error Message = [{2}]", strCLII, _strMultipleFieldLogIdentifier, strErrorMessage));
                    blnFoundOrCreated = false;
                }
            }
            else
            {
                _objLogger.Info("The order address does not have a CLII, and so GLM must be searched using the raw address fields.");
            }


            // SEARCH GLM BY Raw Address Fields -> Get the address fields stored in our custom DB table, which have already been cleansed and normalized
            if (blnFoundOrCreated == false)
            {
                // Clean off any error messages from the previous call, since we don't need them
                objGLMCallManager.ErrorMessages = new List<string>();

                // Instiate a search request object that will get POSTed to the GLM API
                Model.SearchLocationService.AddressLocationQuery.AddressLocationQueryRequest objAddressLocationQueryV2Request = new Model.SearchLocationService.AddressLocationQuery.AddressLocationQueryRequest();

                objAddressLocationQueryV2Request.CreateSiteIfNotFound = true;
                objAddressLocationQueryV2Request.AddressLine1 = _objOrderAddress.CDWAddressOne;
                objAddressLocationQueryV2Request.City = _objOrderAddress.CDWCity;
                objAddressLocationQueryV2Request.StateCode = _objOrderAddress.CDWState;
                objAddressLocationQueryV2Request.CountryCode = _objOrderAddress.CDWCountry;
                objAddressLocationQueryV2Request.PostalCode = _objOrderAddress.CDWPostalCode;

                // If the COUNTRY is USA or US, TRUNCATE the ZIP code to 5 digits, since that's what GLM expects
                if ((objAddressLocationQueryV2Request.PostalCode.Length > 5) && ((objAddressLocationQueryV2Request.CountryCode?.ToUpper() == "USA") || (objAddressLocationQueryV2Request.CountryCode?.ToUpper() == "US")))
                {
                    objAddressLocationQueryV2Request.PostalCode = objAddressLocationQueryV2Request.PostalCode.Substring(0, 5);
                }

                _objLogger.Debug(String.Format("We are about to try to search for the record in GLM using its RAW ADDRESS fields.  {0}  ", _strMultipleFieldLogIdentifier));


                // Invoke the GLM Web Method to search for the Site by Advanced Query
                List<Model.SearchLocationService.AddressLocationQuery.AddressSearchLocationV2> lstAddressSearchLocationV2 = objGLMCallManager.PostAddressSearchToGLM(objAddressLocationQueryV2Request);

                if (lstAddressSearchLocationV2 != null)
                {
                    if (lstAddressSearchLocationV2[0]?.ErrorMessage != null && lstAddressSearchLocationV2[0]?.ErrorMessage.Length > 0)
                    {
                        // There was an error, even though we received a good response code :(

                        blnFoundOrCreated = false;
                        strOrderAddressLogItemLogMessage += String.Format("There was an ERROR while trying to SEARCH for or CREATE the SITE LOCATION in GLM using the raw address fields sourced from CDW.  Error Message = [{0}]", lstAddressSearchLocationV2[0]?.ErrorMessage);
                    }
                    else
                    {

                        strPLNumber = lstAddressSearchLocationV2[0]?.MasterSiteId?.Trim().ToUpper();
                        strSiteCode = lstAddressSearchLocationV2[0]?.CLLIId?.Trim().ToUpper();
                        _objLogger.Info(String.Format("The order address was found in GLM using the RAW ADDRESS fields retrieved from CDW. PL Number = [{0}]. {1} ", strPLNumber, _strMultipleFieldLogIdentifier));

                        strOrderAddressLogItemLogMessage += String.Format("The base address record (Site Location) was found in GLM using  the raw address fields sourced from CDW.  ");
                        blnFoundOrCreated = true;

                    }

                }
                else
                {
                    _objLogger.Warn(String.Format("The order address could NOT be found in GLM using the RAW ADDRESS fields retrieved from the CDW. {0}  ", _strMultipleFieldLogIdentifier));
                    blnFoundOrCreated = false;

                    string strErrorMessages = String.Join(" | ", objGLMCallManager.ErrorMessages.ToArray());
                    if (strErrorMessages?.Length > 0)
                    {
                        TruncateString(ref strErrorMessages);
                    }

                    strOrderAddressLogItemLogMessage += String.Format("The base address record (Site Location) could NOT be found in GLM using the raw address fields sourced from CDW.  Error Message = [{0}]  ", strErrorMessages);
                }
            }

            // Set the fields on the Order Address record so that we can 
            _objOrderAddress.DateTimeOfLastMigrationStatusUpdate = DateTime.Now;

            if (blnFoundOrCreated)
            {
                _blnProcessNextStep = true;

                _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.GLM_SITE_FOUND_or_CREATED;
                _objOrderAddress.NumberOfFailedGLMSiteCalls = 0;
                _objOrderAddress.GLMPLNumber = strPLNumber;
                _objOrderAddress.GLMSiteCode = strSiteCode;
                _objOrderAddress.HasGLMSiteCode = (String.IsNullOrEmpty(_objOrderAddress.GLMSiteCode) == false);
                _objOrderAddress.ExistsInGLMAsSite = true;
            }
            else
            {
                _blnProcessNextStep = false;

                _objOrderAddress.NumberOfFailedGLMSiteCalls++;
                if (_objOrderAddress.NumberOfFailedGLMSiteCalls >= 3)
                {
                    _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.ABANDONED_GLM_SITE_existence_CHECK_due_to_API_Call_Errors;
                    strOrderAddressLogItemLogMessage += String.Format("  Since there have been multiple failed attempts to locate the address in GLM, future efforts will be abandoned until a user intervenes.");
                }
                else
                {
                    _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SITE_existence_CHECK;
                }
            }

            // Update the record in the DB
            UpdateAndReloadTheAddressRecord(strOrderAddressLogItemLogMessage);

        }


        // SITE CODE -----------------------

        /// <summary>
        /// Reviews the values persisted on the record that were retrieved from GLM when the site was searched/created, and sets its status and related properties accordingly so that it can continue down the workflow
        /// </summary>
        private void SearchGLMForSiteCode()
        {
            string strPLNumber = _objOrderAddress.GLMPLNumber?.Trim().ToUpper();

            string strPreferredSiteCode = String.Empty;

            bool blnCallSucceeded = false;
            string strErrorMessage = String.Empty;

            // Call GLM to determine if the record has a Site Code (PLNumber to Autocomplete service to retrieved the "PreferredSiteCode")
            RAL.GLMCallManager objGLMCallManager = new RAL.GLMCallManager(ConfigHelper.GetGLMBaseUrl(), ConfigHelper.GetGLMAuthorizationHeaderUsername(), ConfigHelper.GetGLMAuthorizationHeaderApplicationID(), _objOrderAddress.OrderAddressID.Value);
            List<Model.ResponseItem> lstResponseItems = objGLMCallManager.GetAddressFromGLMByMasterSideIDOrCLII(strPLNumber);
            if (lstResponseItems != null)
            {

                // Try to parse the value
                if (BLL.GLMResponseUtil.TryGetPreferredSiteCodeForPLNumber(strPLNumber, lstResponseItems, out strPreferredSiteCode, out strErrorMessage))
                {
                    blnCallSucceeded = true;
                    // strPreferredSiteCode should hold the value
                }
                else
                {
                    blnCallSucceeded = false;
                }
            }
            else
            {
                blnCallSucceeded = false;
            }

            string strOrderAddressLogItemLogMessage = String.Empty;


            _objOrderAddress.GLMSiteCode = strPreferredSiteCode;
            _objOrderAddress.HasGLMSiteCode = ((_objOrderAddress.GLMSiteCode != String.Empty) && (_objOrderAddress.GLMSiteCode != null));

            if (blnCallSucceeded)
            {
                if ((_objOrderAddress.HasGLMSiteCode.Value))
                {
                    _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.SITE_CODE_FOUND_in_GLM;
                    strOrderAddressLogItemLogMessage = String.Format("The Site Code corresponding to the Site Location was found in GLM. Site Code = [{0}].", _objOrderAddress.GLMSiteCode);

                    if (lstResponseItems.Count == 0)
                    {
                        // This means that the call to retrieve the RECORD via AUTOM-COMPLETE did NOT HAVE ANY RESULTS... which likely means we need to pause and wait for the AUTO-COMPLETE service to finish refreshing (about 5 mins or less)
                        _blnProcessNextStep = false;
                    }
                    else
                    {
                        _blnProcessNextStep = true;
                    }

                }
                else
                {
                    _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.SITE_CODE_does_NOT_EXIST_in_GLM;
                    strOrderAddressLogItemLogMessage = String.Format("The Site Location does NOT yet have a SITE CODE assigned to it in GLM, and so the record's status will be adjusted to stage it for Site Code creation.");

                    // Since there can be a delay in publishing the new SITE LOCATION and/or ITS new SITE CODE to the AUTOMCOMPLETE SERVICE used to check to EXISTENCE, Sleep HERE.
                    _blnProcessNextStep = false;
                }
            }
            else
            {
                _blnProcessNextStep = false;

                string strCompositeErrorMessage = String.Format("The call to GLM to check for a SITE CODE failed. Error Messages = [{0} | {1}]", strErrorMessage, String.Join(" |--> ", objGLMCallManager.ErrorMessages.ToArray()));

                _objOrderAddress.NumberOfFailedGLMSiteCodeExistenceCalls++;
                if (_objOrderAddress.NumberOfFailedGLMSiteCodeExistenceCalls >= 3)
                {
                    _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.ABANDONED_GLM_SITE_CODE_existence_CHECK_due_to_API_Call_Errors;
                    strOrderAddressLogItemLogMessage += String.Format("Since there have been multiple failed attempts to see if the SITE CODE exists in GLM, future efforts will be abandoned until a user intervenes. {0}", strCompositeErrorMessage);
                }
                else
                {
                    _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SITE_CODE_existence_CHECK;
                    strOrderAddressLogItemLogMessage += String.Format("The API call to see if the SITE CODE exists in GLM has failed.  The system will attempt another call and so a user DOES NOT need to intervene. {0}", strCompositeErrorMessage);
                }
            }

            TruncateString(ref strOrderAddressLogItemLogMessage);

            UpdateAndReloadTheAddressRecord(strOrderAddressLogItemLogMessage);


        }

        #region TO DELETE AFTER SOME TESTING
        // THIS TURNED OUT NOT TO MAKE A DIFFERENCE, SINCE THE SITE CODE WAS ALSO BEING DELAYED IN AUTOCOMPLETE PUSH
        //private void SearchGLMForSiteCodeTheSlowWay()
        //{
        //    // Set the PL number to be used in the call to SAP
        //    string strPLNumber = _objOrderAddress.GLMPLNumber.Trim().ToUpper();

        //    bool blnCallSucceeded = false;
        //    bool blnSiteCodeExists = false;
        //    string strSiteCode = String.Empty;

        //    // Instantiate a GLM Call Manager, to be used to invoke the GLM Web Service APIs
        //    RAL.GLMCallManager objGLMCallManager = new RAL.GLMCallManager(ConfigHelper.GetGLMBaseUrl(), ConfigHelper.GetGLMAuthorizationHeaderUsername(), ConfigHelper.GetGLMAuthorizationHeaderApplicationID());

        //    // Invoke the GLM API, to search for the full set of data for the SITE LOCATION... including ALL of its SERVICE LOCATIONs
        //    List<Model.LocationService.SiteLocationV2> lstSiteLocationV2 = objGLMCallManager.GetSiteLocationFromGLMByPLOrClII(strPLNumber);

        //    // Check to see if the call succeeded
        //    if (lstSiteLocationV2 != null)
        //    {
        //        // Declare an analzer class that will parse through the very cryptic GLM response to determine if the Site Location Exists
        //        BLL.SiteLocationAnalyzer objSiteLocationAnalyzer = new BLL.SiteLocationAnalyzer();

        //        // Load the analyser
        //        if (objSiteLocationAnalyzer.Load(lstSiteLocationV2))
        //        {
        //            // Search for the SERVICE LOCATION using its Floor, Room, and Suite
        //            blnSiteCodeExists = objSiteLocationAnalyzer.FindSiteLocation_SiteCode(strPLNumber, out strSiteCode);
        //            // NOTE: The strSiteCode variable now holds the Site Code for the SITE LOCATION retrieved from GLM
        //            blnCallSucceeded = true;
        //        }
        //        else
        //        {
        //            // The call result cannot be loaded, which is the same end result as a failed call
        //            blnCallSucceeded = false;
        //            strSiteCode = String.Empty;
        //        }
        //    }
        //    else
        //    {
        //        // The call failed.
        //        blnCallSucceeded = false;
        //        strSiteCode = String.Empty;
        //    }


        //    // Declare the log message string
        //    string strOrderAddressLogItemLogMessage = String.Empty;

        //    _objOrderAddress.GLMSiteCode = strSiteCode;
        //    _objOrderAddress.HasGLMSiteCode = ((_objOrderAddress.GLMSiteCode != String.Empty) && (_objOrderAddress.GLMSiteCode != null));

        //    if (blnCallSucceeded)
        //    {
        //        if ((_objOrderAddress.HasGLMSiteCode.Value))
        //        {
        //            _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.SITE_CODE_FOUND_in_GLM;
        //            strOrderAddressLogItemLogMessage = String.Format("The Site Code corresponding to the Site Location was found in GLM via the SLOW METHOD. Site Code = [{0}].", _objOrderAddress.GLMSiteCode);
        //        }
        //        else
        //        {
        //            _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.SITE_CODE_does_NOT_EXIST_in_GLM;
        //            strOrderAddressLogItemLogMessage = String.Format("The Site Location does NOT yet have a SITE CODE assigned to it in GLM, and so the record's status will be adjusted to stage it for Site Code creation.");
        //        }
        //    }
        //    else
        //    {
        //        _blnProcessNextStep = false;

        //        string strCompositeErrorMessage = String.Format("The call to GLM to check for a SITE CODE failed. Error Messages = [{0}]",  String.Join(" |--> ", objGLMCallManager.ErrorMessages.ToArray()));

        //        _objOrderAddress.NumberOfFailedGLMSiteCodeExistenceCalls++;
        //        if (_objOrderAddress.NumberOfFailedGLMSiteCodeExistenceCalls >= 3)
        //        {
        //            _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.ABANDONED_GLM_SITE_CODE_existence_CHECK_due_to_API_Call_Errors;
        //            strOrderAddressLogItemLogMessage += String.Format("Since there have been multiple failed attempts to see if the SITE CODE exists in GLM, future efforts will be abandoned until a user intervenes. {0}", strCompositeErrorMessage);
        //        }
        //        else
        //        {
        //            _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SITE_CODE_existence_CHECK;
        //            strOrderAddressLogItemLogMessage += String.Format("The API call to see if the SITE CODE exists in GLM has failed.  The system will attempt another call and so a user DOES NOT need to intervene. {0}", strCompositeErrorMessage);
        //        }
        //    }

        //    TruncateString(ref strOrderAddressLogItemLogMessage);

        //    UpdateAndReloadTheAddressRecord(strOrderAddressLogItemLogMessage);
        //} 
        #endregion

        /// <summary>
        /// Calls GLM's API to trigger the CREATION of a SITE CODE for the SITE LOCATION.  In some cases, a WorkItem may be created that requires manual intervention, in which case we should call SAP
        /// </summary>
        private void CreateSiteCodeInGLM()
        {
            // Set the PL number to be used in the call to SAP
            string strPLNumber = _objOrderAddress.GLMPLNumber.Trim().ToUpper();

            // Instantiate a GLM Call Manager, to be used to invoke the GLM Web Service APIs
            RAL.GLMCallManager objGLMCallManager = new RAL.GLMCallManager(ConfigHelper.GetGLMBaseUrl(), ConfigHelper.GetGLMAuthorizationHeaderUsername(), ConfigHelper.GetGLMAuthorizationHeaderApplicationID(), _objOrderAddress.OrderAddressID.Value);

            string strSiteCode = String.Empty;
            Level3.AddressManagement.Model.LocationService.OrderLocation.OrderNotificationResponse objOrderNotificationResponse = objGLMCallManager.PostOrderNotificationToGLMToCreateSiteCodeOrSCode(strPLNumber);

            // Declare a string to hold the log message
            string strOrderAddressLogItemLogMessage = String.Empty;

            if (objOrderNotificationResponse != null)
            {
                // Declare a variable to reflect whether the record was found in GLM
                bool blnDelayedCreation = false;
                blnDelayedCreation = ((objOrderNotificationResponse.WorkQueueId.HasValue) && (objOrderNotificationResponse.WorkQueueId.Value > 0));

                if (blnDelayedCreation)
                {
                    // The site code's creation should trigger an EMP to SAP, we should now wait to check for the 
                    _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.WORK_ITEM_QUEUED_in_GLM_for_DELAYED_SITE_CODE_ASSIGNMENT;

                    strOrderAddressLogItemLogMessage = "Although the API call to GLM to TRIGGER the ASSIGNMENT of a SITE CODE was SUCCESSFUL, the response received from the API indicated that it COULD NOT be created in REAL-TIME.  It could take up to two days for the VALUE to be assigned, and so the system will SKIP the existence check and will try to add the address directly to SAP if it does not yet exist.";
                }
                else
                {
                    strOrderAddressLogItemLogMessage = "The call to GLM to request the ASSIGNMENT of a SITE CODE was successful";
                    _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.SITE_CODE_ASSIGNMENT_Successfully_TRIGGERED_in_GLM;
                }

                _blnProcessNextStep = true;
            }
            else
            {
                // There was an error of some type
                string strErrorMessage = objOrderNotificationResponse?.ErrorMessage;
                _blnProcessNextStep = false;

                // Increment the error counter
                _objOrderAddress.NumberOfFailedGLMSiteCodeCreationCalls++;

                if (_objOrderAddress.NumberOfFailedGLMSiteCodeCreationCalls >= 3)
                {
                    _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.ABANDONED_GLM_SITE_CODE_ASSIGNMENT_due_to_API_Call_Errors;
                    strOrderAddressLogItemLogMessage = "Multiple attempts to trigger the creation of a SITE CODE in GLM have failed, and USER INTERVENTION is not REQUIRED for processing to proceed.";
                }
                else
                {
                    _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.API_Call_ERRORED_while_trying_to_TRIGGER_the_ASSIGNMENT_of_a_SITE_CODE_in_GLM;
                    strOrderAddressLogItemLogMessage = "The attempt to trigger the creation of a SITE CODE in GLM failed. The system will try the request again the next time that this record is processed.";
                }
            }

            UpdateAndReloadTheAddressRecord(strOrderAddressLogItemLogMessage);

        }
        private void CreateSCodeInGLM()
        {
            // Set the SL number to be used in the call to SAP
            string strSLNumber = _objOrderAddress.GLMSLNumber.Trim().ToUpper();

            // Instantiate a GLM Call Manager, to be used to invoke the GLM Web Service APIs
            RAL.GLMCallManager objGLMCallManager = new RAL.GLMCallManager(ConfigHelper.GetGLMBaseUrl(), ConfigHelper.GetGLMAuthorizationHeaderUsername(), ConfigHelper.GetGLMAuthorizationHeaderApplicationID(), _objOrderAddress.OrderAddressID.Value);

            string strSiteCode = String.Empty;
            Level3.AddressManagement.Model.LocationService.OrderLocation.OrderNotificationResponse objOrderNotificationResponse = objGLMCallManager.PostOrderNotificationToGLMToCreateSiteCodeOrSCode(strSLNumber);

            // Declare a string to hold the log message
            string strOrderAddressLogItemLogMessage = String.Empty;
            _objOrderAddress.DateTimeOfLastMigrationStatusUpdate = DateTime.Now;


            if (objOrderNotificationResponse != null)
            {
                // Declare a variable to reflect whether the record was found in GLM
                bool blnDelayedCreation = false;
                blnDelayedCreation = ((objOrderNotificationResponse.WorkQueueId.HasValue) && (objOrderNotificationResponse.WorkQueueId.Value > 0));

                if (blnDelayedCreation)
                {
                    // The site code's creation should trigger an EMP to SAP, we should now wait to check for the 
                    _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.WORK_ITEM_QUEUED_in_GLM_for_DELAYED_S_CODE_CREATION;

                    strOrderAddressLogItemLogMessage = "Although the API call to GLM to TRIGGER the ASSIGNMENT of an SCODE was SUCCESSFUL, the response received from the API indicated that it COULD NOT be created in REAL-TIME.  It could take up to two days for the VALUE to be assigned, and so the system will SKIP the existence check and will try to add the address directly to SAP if it does not yet exist.";
                }
                else
                {
                    strOrderAddressLogItemLogMessage = "The call to GLM to request the ASSIGNMENT of an SCODE was successful";
                    _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.S_CODE_Successfully_CREATED_in_GLM;

                }

                _blnProcessNextStep = true;
            }
            else
            {
                // There was an error of some type
                string strErrorMessage = objOrderNotificationResponse?.ErrorMessage;
                _blnProcessNextStep = false;

                // Increment the error counter
                _objOrderAddress.NumberOfFailedGLMSCodeCreationCalls++;

                if (_objOrderAddress.NumberOfFailedGLMSCodeCreationCalls >= 3)
                {
                    _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.ABANDONED_GLM_S_CODE_CREATION_due_to_API_Call_Errors;
                    strOrderAddressLogItemLogMessage = "Multiple attempts to trigger the creation of an S CODE in GLM have failed, and USER INTERVENTION is not REQUIRED for processing to proceed.";
                }
                else
                {
                    _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.API_Call_ERRORED_while_executing_GLM_S_CODE_CREATION_attempt;
                    strOrderAddressLogItemLogMessage = "The attempt to trigger the creation of an S CODE in GLM failed. The system will try the request again the next time that this record is processed.";
                }

            }

            UpdateAndReloadTheAddressRecord(strOrderAddressLogItemLogMessage);

        }

        // SAP IMPORT -----------------------

        /// <summary>
        /// Calls SAP's API to trigger the IMPORT of the SITE LOCATION ADDRESS and any SERVICE LOCATION ADDRESSES into SAP from GLM
        /// </summary>
        private void ForceSAPSiteAddressCreation()
        {
            // Set the PL number to be used in the call to SAP
            string strPLNumber = _objOrderAddress.GLMPLNumber.Trim().ToUpper();

            RAL.SAPCallManager objSAPCallManager = new RAL.SAPCallManager(ConfigHelper.GetSAPBaseUrl(), _objOrderAddress.OrderAddressID.Value);
            Model.SAP.Responses.CreateAddressResponse objCreateAddressResponse = objSAPCallManager.PostCreateAddressQueueItem(strPLNumber);

            string strOrderAddressLogItemLogMessage = String.Empty;

            if (objCreateAddressResponse != null)
            {
                _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.STAGED_for_SAP_SITE_LOCATION_SEARCH;
                strOrderAddressLogItemLogMessage = String.Format("The address was SUCCESSFULLY staged for processing by SAP.  It SHOULD be created in SAP as part of SAP's next scheduled GLM address import run.  SAP will pull in all addresses for the SITE and SERVICE LOCATIONS that are default and have a SITE CODE or S-CODE assigned in GLM. The record was staged using its PL Number. Site Code = [{0}].  The record will be RE-staged for SAP Address SEARCH to ensure it makes it in.", strPLNumber);
            }
            else
            {
                _objOrderAddress.NumberOfFailedSAPSiteAddressImportCalls++;
                if (_objOrderAddress.NumberOfFailedSAPSiteAddressImportCalls >= 3)
                {
                    _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.ABANDONED_attempts_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP_due_to_API_Call_Errors;
                    strOrderAddressLogItemLogMessage = String.Format("The attempt to FORCE SAP to pull in the record from GLM as part of its next scheduled run has FAILED.  Another attempt to force the import into SAP will NOT be made.  GLM will eventually trigger the PULL, which is when this record will make its way into SAP if it has a valid Site Code or S-Code assigned in GLM.");
                }
                else
                {
                    _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.API_Call_ERRORED_while_attempting_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP;
                    strOrderAddressLogItemLogMessage = String.Format("The attempt to FORCE SAP to pull in the record from GLM as part of its next scheduled run has FAILED.  Another attempt to force the import into SAP is forthcoming.");
                }
            }

            UpdateAndReloadTheAddressRecord(strOrderAddressLogItemLogMessage);

            // We pause processing here, because the IMPORT process we are calling is ASYNC and doesn't yield a result right away
            _blnProcessNextStep = false;
        }
        private void ForceSAPServiceLocationAddressCreation()
        {
            // Set the PL number to be used in the call to SAP
            string strPLNumber = _objOrderAddress.GLMPLNumber.Trim().ToUpper();

            RAL.SAPCallManager objSAPCallManager = new RAL.SAPCallManager(ConfigHelper.GetSAPBaseUrl(), _objOrderAddress.OrderAddressID.Value);
            Model.SAP.Responses.CreateAddressResponse objCreateAddressResponse = objSAPCallManager.PostCreateAddressQueueItem(strPLNumber);

            string strOrderAddressLogItemLogMessage = String.Empty;

            if (objCreateAddressResponse != null)
            {
                _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.STAGED_for_SAP_SERVICE_LOCATION_SEARCH;
                strOrderAddressLogItemLogMessage = String.Format("The address was SUCCESSFULLY staged for processing by SAP.  It SHOULD be created in SAP as part of SAP's next scheduled GLM address import run.  SAP will pull in all addresses for the SITE and SERVICE LOCATIONS that are default and have a SITE CODE or S-CODE assigned in GLM. The record was staged using its PL Number. Site Code = [{0}].  The record will be RE-staged for SAP Address SEARCH to ensure it makes it in.", strPLNumber);
            }
            else
            {
                _objOrderAddress.NumberOfFailedSAPServiceLocationAddressImportCalls++;
                if (_objOrderAddress.NumberOfFailedSAPServiceLocationAddressImportCalls >= 3)
                {
                    _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.ABANDONED_attempts_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP_due_to_API_Call_Errors;
                    strOrderAddressLogItemLogMessage = String.Format("The attempt to FORCE SAP to pull in the record from GLM as part of its next scheduled run has FAILED.  Another attempt to force the import into SAP will NOT be made.  GLM will eventually trigger the PULL, which is when this record will make its way into SAP if it has a valid Site Code or S-Code assigned in GLM.");
                }
                else
                {
                    _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.API_Call_ERRORED_while_attempting_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP;
                    strOrderAddressLogItemLogMessage = String.Format("The attempt to FORCE SAP to pull in the record from GLM as part of its next scheduled run has FAILED.  Another attempt to force the import into SAP is forthcoming.");
                }
            }

            UpdateAndReloadTheAddressRecord(strOrderAddressLogItemLogMessage);

            // We pause processing here, because the IMPORT process we are calling is ASYNC and doesn't yield a result right away
            _blnProcessNextStep = false;
        }

        // BRANCHING LOGIC -----------------------
        /// <summary>
        /// If this is a SITE LOCATION, then UPDATES the STATUS to COMPLETE.  If this is a SERVICE LOCATION, then the STATUS is UPDATED to SEARCH for the SERVICE LOCATION in GLM
        /// </summary>
        private void ExecuteBranchingLogic()
        {
            string strOrderAddressLogItemLogMessage;

            switch ((Model.OrderAddressTypes)_objOrderAddress.OrderAddressTypeID)
            {
                case Model.OrderAddressTypes.Site:

                    strOrderAddressLogItemLogMessage = "The SITE LOCATION address was found in SAP.  Processing is now COMPLETE.";

                    // This is a SITE LOCATION and its address now EXISTS in SAP
                    _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.Processing_COMPLETE;

                    // STOP processing, because we have reached the natural end for this record
                    _blnProcessNextStep = false;

                    break;

                case Model.OrderAddressTypes.Service_Location:

                    // We still need to make sure that the Service Location exists in GLM (and has an S-Code)
                    _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.STAGED_for_GLM_SERVICE_LOCATION_SEARCH;

                    strOrderAddressLogItemLogMessage = String.Format("The SITE LOCATION address was found in SAP, but this address has a Floor, Room, or Suite making it a SERVICE LOCATION and so will be processing further.  Floor = [ {0} ], Room = [ {1} ],  Suite = [ {2} ]", _objOrderAddress.CDWFloor, _objOrderAddress.CDWRoom, _objOrderAddress.CDWSuite);

                    // Continue processing
                    _blnProcessNextStep = true;

                    break;
                default:
                    _blnProcessNextStep = false;
                    throw new Exception("Unrecognized Address Type fell through to the default case of the switch statement while trying to branch based on the address type.  Bad Coder.  Did you forget a case statement?");
            }

            UpdateAndReloadTheAddressRecord(strOrderAddressLogItemLogMessage);
        }


        // SAP SEARCH
        /// <summary>
        /// Searches for the PL Number in SAP
        /// </summary>
        private void SearchForSiteLocationAddressInSAP()
        {

            // Declare a string to hold the log message
            string strOrderAddressLogItemLogMessage = String.Empty;

            // Set the PL number to be used in the call to SAP
            string strPLNumber = _objOrderAddress.GLMPLNumber.Trim().ToUpper();

            bool blnCallSucceeded = false;

            // Declare a call manager to use to invoke the SAP web service layer
            RAL.SAPCallManager objSAPCallManager = new RAL.SAPCallManager(ConfigHelper.GetSAPBaseUrl(), _objOrderAddress.OrderAddressID.Value);

            // Search for the record in SAP with an http GET
            Model.SAPAddressSearchResponse objSAPAddressSearchResponse = objSAPCallManager.GetAddressRecordsFromSAP(strPLNumber);

            // Check the RESPONSE received from the API
            if (objSAPCallManager.HttpStatusCodeResult == System.Net.HttpStatusCode.NotFound)
            {
                // The record was found in SAP
                _objOrderAddress.ExistsInSAPAsSiteAddress = false;
                _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.Site_Location_ADDRESS_does_NOT_EXIST_in_SAP;
                // Reset the 'Failed Call Counter'
                _objOrderAddress.NumberOfFailedSAPSiteAddressSearchCalls = 0;

                strOrderAddressLogItemLogMessage = "The SITE LOCATION DOES NOT EXIST in SAP";

                // The call to the API Suceeded, but the records was NOT FOUND
                blnCallSucceeded = true;
            }
            else if (objSAPCallManager.HttpStatusCodeResult == System.Net.HttpStatusCode.OK)
            {
                // The HTTP status code returned from the API indicated that the RECORD EXISTS

                // Declare an out param to cound the number of results returned
                int intNumberOfValidRecords = 0;
                if (SAPAddressSearchResponseUtil.RecordFoundInSAP(objSAPAddressSearchResponse, objSAPCallManager.HttpStatusCodeResult, strPLNumber, out intNumberOfValidRecords))
                {
                    // The record was found in SAP
                    _objOrderAddress.ExistsInSAPAsSiteAddress = true;
                    _objOrderAddress.NumberOfRecordsInSAPWithPL = intNumberOfValidRecords;
                    _objOrderAddress.NumberOfFailedSAPSiteAddressSearchCalls = 0;
                    _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.Site_Location_ADDRESS_FOUND_in_SAP;

                    strOrderAddressLogItemLogMessage = String.Format("The SITE LOCATION address was FOUND in SAP.  There are [ {0} ] records in SAP with the SITE LOCATION's PLNumber [{1}] ", intNumberOfValidRecords, strPLNumber);

                    blnCallSucceeded = true;
                }
                else
                {
                    blnCallSucceeded = false;
                }
            }
            else
            {
                blnCallSucceeded = false;
            }


            if (blnCallSucceeded == false)
            {
                // The record could NOT be parsed and/or the call to the API failed
                _objOrderAddress.NumberOfFailedSAPSiteAddressSearchCalls++;

                if (_objOrderAddress.NumberOfFailedSAPSiteAddressSearchCalls >= 5)
                {
                    _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.ABANDONED_SAP_SITE_LOCATION_SEARCH_due_to_API_Call_Errors;
                    strOrderAddressLogItemLogMessage = "The SITE LOCATION could not be found in SAP.  The maximum number of attempts to check for the record in SAP has been reached and a USER MUST NOW INTERVENE.";
                }
                else
                {
                    _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.API_Call_ERRORED_while_executing_SAP_SITE_LOCATION_ADDRESS_SEARCH;
                    strOrderAddressLogItemLogMessage = "The SITE LOCATION could NOT be FOUND in SAP.  The system will continue to check SAP for the address until this check fails 5 times.";
                }

                _blnProcessNextStep = false;
            }
            else
            {
                _blnProcessNextStep = true;
            }

            UpdateAndReloadTheAddressRecord(strOrderAddressLogItemLogMessage);
        }

        /// <summary>
        /// Searches for the SL Number in SAP:
        /// </summary>
        private void SearchForServiceLocationInSAP()
        {
            // Declare a string to hold the log message
            string strOrderAddressLogItemLogMessage = String.Empty;

            // Set the PL number to be used in the call to SAP
            string strSLNumber = _objOrderAddress.GLMSLNumber.Trim().ToUpper();

            bool blnCallSucceeded = false;

            // Declare a call manager to use to invoke the SAP web service layer
            RAL.SAPCallManager objSAPCallManager = new RAL.SAPCallManager(ConfigHelper.GetSAPBaseUrl(), _objOrderAddress.OrderAddressID.Value);

            // Search for the record in SAP with an http GET
            Model.SAPAddressSearchResponse objSAPAddressSearchResponse = objSAPCallManager.GetAddressRecordsFromSAP(strSLNumber);

            // Check the RESPONSE received from the API
            if (objSAPCallManager.HttpStatusCodeResult == System.Net.HttpStatusCode.NotFound)
            {
                // The record was found in SAP
                _objOrderAddress.ExistsInSAPAsServiceLocationAddress = false;
                _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.Service_Location_ADDRESS_does_NOT_EXIST_in_SAP;
                // Reset the 'Failed Call Counter'
                _objOrderAddress.NumberOfFailedSAPServiceLocationAddressSearchCalls = 0;

                strOrderAddressLogItemLogMessage = "The SERVICE LOCATION does NOT EXIST in SAP";

                // The call to the API Suceeded, but the records was NOT FOUND
                blnCallSucceeded = true;
            }
            else if (objSAPCallManager.HttpStatusCodeResult == System.Net.HttpStatusCode.OK)
            {
                // The HTTP status code returned from the API indicated that the RECORD EXISTS

                // Declare an out param to cound the number of results returned
                int intNumberOfValidRecords = 0;
                if (SAPAddressSearchResponseUtil.RecordFoundInSAP(objSAPAddressSearchResponse, objSAPCallManager.HttpStatusCodeResult, strSLNumber, out intNumberOfValidRecords))
                {
                    // The record was found in SAP
                    _objOrderAddress.ExistsInSAPAsServiceLocationAddress = true;
                    _objOrderAddress.NumberOfRecordsInSAPWithSL = intNumberOfValidRecords;
                    _objOrderAddress.NumberOfFailedSAPServiceLocationAddressSearchCalls = 0;
                    _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.Service_Location_ADDRESS_FOUND_in_SAP;

                    strOrderAddressLogItemLogMessage = String.Format("The SERVICE LOCATION address was FOUND in SAP.  There are [ {0} ] records in SAP with the SERVICE LOCATION's SLNumber [{1}] ", intNumberOfValidRecords, strSLNumber);

                    blnCallSucceeded = true;
                }
                else
                {
                    blnCallSucceeded = false;
                }
            }
            else
            {
                blnCallSucceeded = false;
            }

            if (blnCallSucceeded == false)
            {
                // The record could NOT be parsed and/or the call to the API failed
                _objOrderAddress.NumberOfFailedSAPSiteAddressSearchCalls++;

                if (_objOrderAddress.NumberOfFailedSAPSiteAddressSearchCalls >= 5)
                {
                    _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.ABANDONED_SAP_SERVICE_LOCATION_SEARCH_due_to_API_Call_Errors;
                    strOrderAddressLogItemLogMessage = "The SERVICE LOCATION could not be found in SAP.  The maximum number of attempts to check for the record in SAP has been reached and a USER MUST NOW INTERVENE.";
                }
                else
                {
                    _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.API_Call_ERRORED_while_executing_SAP_SERVICE_LOCATION_ADDRESS_SEARCH;
                    strOrderAddressLogItemLogMessage = "The SERVICE LOCATION could NOT be FOUND in SAP.  The system will continue to check SAP for the address until this check fails 5 times.";
                }

                _blnProcessNextStep = false;
            }
            else
            {
                _blnProcessNextStep = true;
            }

            UpdateAndReloadTheAddressRecord(strOrderAddressLogItemLogMessage);

        }


        // SERVICE LOCATION SEARCH

        /// <summary>
        /// Calls GLM's API to RETRIEVE all the details for SITE LOCATION and to see if the SERVICE LOCATION EXISTs
        /// </summary>
        private void SearchGLMForServiceLocation()
        {
            // Set the PL number to be used in the call to SAP
            string strPLNumber = _objOrderAddress.GLMPLNumber.Trim().ToUpper();

            bool blnCallSucceeded = false;
            bool blnServiceLocationExists = false;
            string strSLNumber = String.Empty;

            // Since there is a new scenario where we may already have an SLNumber, which means this SERVICE LOCATION exists in GLM... check the value
            if ((_objOrderAddress.GLMSLNumber != null) && (_objOrderAddress.GLMSLNumber != String.Empty) && (_objOrderAddress.GLMSLNumber?.Length > 0))
            {
                // We already have an SLNumber and so we know the record exists in GLM and have its identifier
                blnCallSucceeded = true;
                strSLNumber = _objOrderAddress.GLMSLNumber;
                blnServiceLocationExists = true;
            }
            else
            {

                // Instantiate a GLM Call Manager, to be used to invoke the GLM Web Service APIs
                RAL.GLMCallManager objGLMCallManager = new RAL.GLMCallManager(ConfigHelper.GetGLMBaseUrl(), ConfigHelper.GetGLMAuthorizationHeaderUsername(), ConfigHelper.GetGLMAuthorizationHeaderApplicationID(), _objOrderAddress.OrderAddressID.Value);

                // Invoke the GLM API, to search for the full set of data for the SITE LOCATION... including ALL of its SERVICE LOCATIONs
                List<Model.LocationService.SiteLocationV2> lstSiteLocationV2 = objGLMCallManager.GetSiteLocationFromGLMByPLOrClII(strPLNumber);

                // Check to see if the call succeeded
                if (lstSiteLocationV2 != null)
                {
                    // Declare an analzer class that will parse through the very cryptic GLM response to determine if the Site Location Exists
                    BLL.SiteLocationAnalyzer objSiteLocationAnalyzer = new BLL.SiteLocationAnalyzer();

                    // Load the analyser
                    if (objSiteLocationAnalyzer.Load(lstSiteLocationV2))
                    {
                        // Search for the SERVICE LOCATION using its Floor, Room, and Suite
                        blnServiceLocationExists = objSiteLocationAnalyzer.FindServiceLocation_SLNumber(_objOrderAddress.CDWFloor, _objOrderAddress.CDWRoom, _objOrderAddress.CDWSuite, out strSLNumber);
                        // NOTE: The strSLNumber variable now holds the SL Number for the SERVICE LOCATION retrieved from GLM
                        blnCallSucceeded = true;
                    }
                    else
                    {
                        // The call result cannot be loaded, which is the same end result as a failed call
                        blnCallSucceeded = false;
                        strSLNumber = String.Empty;
                    }
                }
                else
                {
                    // The call failed.
                    blnCallSucceeded = false;
                    strSLNumber = String.Empty;
                }
            }



            // Declare the log message string
            string strOrderAddressLogItemLogMessage = String.Empty;

            //objGLMCallManager.ErrorMessages
            if (blnCallSucceeded)
            {
                // Reset the failed call counter, since a call was successful
                _objOrderAddress.NumberOfFailedGLMServiceLocationSearchCalls = 0;
                _objOrderAddress.GLMSLNumber = strSLNumber;

                // Check to see if the SERVICE LOCATION EXISTS
                if (blnServiceLocationExists)
                {
                    // EXISTS
                    _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.SERVICE_LOCATION_FOUND_in_GLM;
                    _objOrderAddress.ExistsInGLMAsServiceLocation = true;
                    strOrderAddressLogItemLogMessage = "The SERVICE LOCATION was found in GLM.";
                }
                else
                {
                    // DOES NOT EXIST, so STAGE a CREATION attempt
                    _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.SERVICE_LOCATION_does_NOT_EXIST_in_GLM;
                    _objOrderAddress.ExistsInGLMAsServiceLocation = false;
                    strOrderAddressLogItemLogMessage = "The SERVICE LOCATION does NOT EXIST in GLM.";
                }

                _blnProcessNextStep = true;
            }
            else
            {
                _objOrderAddress.NumberOfFailedGLMServiceLocationSearchCalls++;
                if (_objOrderAddress.NumberOfFailedGLMServiceLocationSearchCalls >= 3)
                {
                    _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.ABANDONED_GLM_SERVICE_LOCATION_SEARCH_due_to_API_Call_Errors;
                    strOrderAddressLogItemLogMessage += String.Format("Since there have been multiple failed attempts to SEARCH for the SERVICE LOCATION in GLM, future efforts will be abandoned until a user intervenes.");
                }
                else
                {
                    _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SERVICE_LOCATION_SEARCH;
                    strOrderAddressLogItemLogMessage += String.Format("The API Call to SEARCH for the SERVICE LOCATION in GLM failed.  Another attempt to search GLM for the record is forthcoming.");
                }

                _blnProcessNextStep = false;
            }

            // Update the record in the DB
            UpdateAndReloadTheAddressRecord(strOrderAddressLogItemLogMessage);
        }


        // SERVICE LOCATION CREATION
        /// <summary>
        /// Calls GLM's API to CREATE a NEW SERVICE LOCATION.  If the SERVICE LOCATION already exists, the response will look the same as an ADD. In both cases, the MasterSiteLocationID value will hold the identifier for thew new or existing record
        /// </summary>
        private void CreateServiceLocationInGLM()
        {

            // Instantiate a REQUEST OBJECT, which we will populate and then POST to the API
            Level3.AddressManagement.Model.ServiceLocationService.Requests.AddServiceLocationRequest objAddServiceLocationRequest = new Model.ServiceLocationService.Requests.AddServiceLocationRequest();

            // Set the PL number to be used in the call to SAP
            objAddServiceLocationRequest.MasterSiteId = _objOrderAddress.GLMPLNumber.Trim().ToUpper();

            // Initialize the Desingator object that holds the Floor, Room, or Suite
            objAddServiceLocationRequest.PrimaryServiceLocationDesignator = new Model.ServiceLocationService.Requests.Primaryservicelocationdesignator();

            // Create a list to hold any designators.  When these get added to the request, the first, second, third field is not specific to the TypeID.  Instead, we just populate them in seqeuence with any valie TypeID/Value combination.  If the second and third are unused, they can be left out of the JSON payload all together... meaning they don't ahve to be initialized or present in the request.
            List<Model.GLMDesignator> lstDesignators = new List<Model.GLMDesignator>();

            // Floor 
            if (String.IsNullOrEmpty(_objOrderAddress.CDWFloor) == false)
            {
                Model.GLMDesignator objGLMDesignator = new Model.GLMDesignator();
                objGLMDesignator.DesignatorTypeID = "FL";
                objGLMDesignator.DesignatorValue = _objOrderAddress.CDWFloor;

                lstDesignators.Add(objGLMDesignator);
            }

            // Room
            if (String.IsNullOrEmpty(_objOrderAddress.CDWRoom) == false)
            {
                Model.GLMDesignator objGLMDesignator = new Model.GLMDesignator();
                objGLMDesignator.DesignatorTypeID = "RM";
                objGLMDesignator.DesignatorValue = _objOrderAddress.CDWRoom;

                lstDesignators.Add(objGLMDesignator);
            }

            // Suite
            if (String.IsNullOrEmpty(_objOrderAddress.CDWSuite) == false)
            {
                Model.GLMDesignator objGLMDesignator = new Model.GLMDesignator();
                objGLMDesignator.DesignatorTypeID = "STE";
                objGLMDesignator.DesignatorValue = _objOrderAddress.CDWSuite;

                lstDesignators.Add(objGLMDesignator);
            }

            // Iterate over the designators, adding them to the appropriate fields in the request, and in sequence
            for (int i = 0; i < lstDesignators.Count; i++)
            {
                string strDesignatorTypeID = lstDesignators[i].DesignatorTypeID;
                string strDesignatorValue = lstDesignators[i].DesignatorValue;

                // Switch here, to make sure we fill the props in numeric sequence
                switch (i)
                {
                    case 0:

                        objAddServiceLocationRequest.PrimaryServiceLocationDesignator.FirstDesignatorTypeId = strDesignatorTypeID;
                        objAddServiceLocationRequest.PrimaryServiceLocationDesignator.FirstDesignatorValue = strDesignatorValue;
                        break;

                    case 1:

                        objAddServiceLocationRequest.PrimaryServiceLocationDesignator.SecondDesignatorTypeId = strDesignatorTypeID;
                        objAddServiceLocationRequest.PrimaryServiceLocationDesignator.SecondDesignatorValue = strDesignatorValue;
                        break;

                    case 2:

                        objAddServiceLocationRequest.PrimaryServiceLocationDesignator.ThirdDesignatorTypeId = strDesignatorTypeID;
                        objAddServiceLocationRequest.PrimaryServiceLocationDesignator.ThirdDesignatorValue = strDesignatorValue;
                        break;

                    default:
                        break;
                }
            }

            // Due to a bug in GLM, we need to nullify this value, else we will get a 405 error as a result of a defect in their system... which is slated for fixing:
            objAddServiceLocationRequest.SiteDesignatorId = null;

            bool blnSuccessfullyCreated = false;

            // Instantiate a GLM Call Manager, to be used to invoke the GLM Web Service APIs
            RAL.GLMCallManager objGLMCallManager = new RAL.GLMCallManager(ConfigHelper.GetGLMBaseUrl(), ConfigHelper.GetGLMAuthorizationHeaderUsername(), ConfigHelper.GetGLMAuthorizationHeaderApplicationID(), _objOrderAddress.OrderAddressID.Value);

            // Post the record to the Interface
            Level3.AddressManagement.Model.ServiceLocationService.Responses.AddServiceLocationResponse objAddServiceLocationResponse = objGLMCallManager.PutNewServiceLocation(objAddServiceLocationRequest);

            // Delcare a string to hold any errors
            string strErrorMessage = String.Empty;
            string strMasterServiceLocationID = String.Empty;

            // Check the result
            if (objAddServiceLocationResponse != null)
            {
                // Check to see if the MasterServiceLocationId has a value
                strMasterServiceLocationID = objAddServiceLocationResponse.MasterServiceLocationId;
                blnSuccessfullyCreated = ((strMasterServiceLocationID != null) && (strMasterServiceLocationID != String.Empty));
            }
            else
            {
                blnSuccessfullyCreated = false;
                strErrorMessage = String.Format("There was an ERROR while trying to CREATE the NEW SERVICE LOCATION in GLM.  Error Message = [{0}]", String.Join(",", objGLMCallManager.ErrorMessages.ToArray()));
                TruncateString(ref strErrorMessage);
            }

            string strOrderAddressLogItemLogMessage = String.Empty;

            // Check to see if the CREATION was successul, and update the object properties so they can be updated in the DB
            if (blnSuccessfullyCreated)
            {
                strOrderAddressLogItemLogMessage = "The SERVICE LOCATION was SUCCESSFULLY CREATED in GLM.";

                // Reset the failed call counter, since a call was successful
                _objOrderAddress.NumberOfFailedGLMServiceLocationCreationCalls = 0;
                _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.SERVICE_LOCATION_Successfully_CREATED_in_GLM;
                _objOrderAddress.GLMSLNumber = strMasterServiceLocationID;

                _blnProcessNextStep = true;
            }
            else
            {
                _objOrderAddress.NumberOfFailedGLMServiceLocationCreationCalls++;
                if (_objOrderAddress.NumberOfFailedGLMServiceLocationCreationCalls >= 3)
                {
                    _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.ABANDONED_GLM_SERVICE_LOCATION_CREATION_due_to_API_Call_Errors;
                    strOrderAddressLogItemLogMessage += String.Format("Since there have been multiple failed attempts to CREATE the NEW SERVICE LOCATION in GLM, future efforts will be abandoned until a user intervenes.");
                }
                else
                {
                    _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.API_Call_ERRORED_while_executing_the_GLM_SERVICE_LOCATION_CREATION_attempt;
                    strOrderAddressLogItemLogMessage += String.Format("The API Call to CREATE the NEW SERVICE LOCATION in GLM failed.  Another attempt to CREATE the SERVICE LOCATION in GLM is forthcoming.");
                }

                strOrderAddressLogItemLogMessage += String.Format(" Error Messsage = [{0}]", strErrorMessage);

                _blnProcessNextStep = false;
            }

            // Update the record in the DB
            UpdateAndReloadTheAddressRecord(strOrderAddressLogItemLogMessage);

        }

        /// <summary>
        /// Calls GLM's API to RETRIEVE all the details for SITE LOCATION and to see if the SERVICE LOCATION has a SITE CODE ASSIGNED in GLM
        /// </summary>
        private void SearchGLMForSCode()
        {
            // Set the PL number to be used in the call to SAP
            string strPLNumber = _objOrderAddress.GLMPLNumber.Trim().ToUpper();
            string strSLNumber = _objOrderAddress.GLMSLNumber.Trim().ToUpper();

            bool blnCallSucceeded = false;
            bool blnSCodeExists = false;
            string strSCode = String.Empty;

            // Instantiate a GLM Call Manager, to be used to invoke the GLM Web Service APIs
            RAL.GLMCallManager objGLMCallManager = new RAL.GLMCallManager(ConfigHelper.GetGLMBaseUrl(), ConfigHelper.GetGLMAuthorizationHeaderUsername(), ConfigHelper.GetGLMAuthorizationHeaderApplicationID(), _objOrderAddress.OrderAddressID.Value);

            // Invoke the GLM API, to search for the full set of data for the SITE LOCATION... including ALL of its SERVICE LOCATIONs and THEIR S CODES
            List<Model.LocationService.SiteLocationV2> lstSiteLocationV2 = objGLMCallManager.GetSiteLocationFromGLMByPLOrClII(strPLNumber);

            // Check to see if the call succeeded
            if (lstSiteLocationV2 != null)
            {
                // Declare an analzer class that will parse through the very cryptic GLM response to determine if the SCode Exists
                BLL.SiteLocationAnalyzer objSiteLocationAnalyzer = new BLL.SiteLocationAnalyzer();

                // Load the analyser
                if (objSiteLocationAnalyzer.Load(lstSiteLocationV2))
                {
                    // Search for the SCODE using its SL Number
                    blnSCodeExists = objSiteLocationAnalyzer.FindServiceLocation_SCode(strSLNumber, out strSCode);
                    // NOTE: The strSCode variable now holds the SCode for the SERVICE LOCATION retrieved from GLM... which could be an EMPTY STRING / BLANK Value!!
                    blnCallSucceeded = true;
                }
                else
                {
                    // The call result cannot be loaded, which is the same end result as a failed call
                    blnCallSucceeded = false;
                    strSCode = String.Empty;
                }
            }
            else
            {
                // The call failed.
                blnCallSucceeded = false;
                strSCode = String.Empty;
            }

            // Declare the log message string
            string strOrderAddressLogItemLogMessage = String.Empty;

            if (blnCallSucceeded)
            {
                // Reset the failed call counter, since a call was successful
                _objOrderAddress.NumberOfFailedGLMSCodeExistenceCalls = 0;
                _objOrderAddress.GLMSCode = strSCode;

                // Check to see if the SCODE EXISTS
                if (blnSCodeExists)
                {
                    // EXISTS
                    _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.S_CODE_FOUND_in_GLM;
                    _objOrderAddress.ExistsInGLMAsServiceLocation = true;
                    strOrderAddressLogItemLogMessage = "The SCODE was FOUND and EXISTS in GLM.";
                }
                else
                {
                    // DOES NOT EXIST, so STAGE a CREATION attempt
                    _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.S_CODE_does_NOT_EXIST_in_GLM;
                    strOrderAddressLogItemLogMessage = "The S CODE does NOT exists in GLM.";
                    _objOrderAddress.ExistsInGLMAsServiceLocation = false;
                }

                _blnProcessNextStep = true;
            }
            else
            {
                _objOrderAddress.NumberOfFailedGLMSCodeExistenceCalls++;
                if (_objOrderAddress.NumberOfFailedGLMSCodeExistenceCalls >= 3)
                {
                    _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.ABANDONED_GLM_S_CODE_existence_CHECK_due_to_API_Call_Errors;
                    strOrderAddressLogItemLogMessage += String.Format("Since there have been multiple failed attempts to check if the SERVICE LOCATION has been ASSIGNED an S CODE in GLM, future efforts will be abandoned until a user intervenes.");
                }
                else
                {
                    _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.API_Call_ERRORED_while_executing_GLM_S_CODE_existence_CHECK;
                    strOrderAddressLogItemLogMessage += String.Format("The API Call to check if the SERVICE LOCATION has been ASSIGNED an S CODE in GLM failed.  Another attempt to search GLM for the record is forthcoming.");
                }

                _blnProcessNextStep = false;
            }

            // Update the record in the DB
            UpdateAndReloadTheAddressRecord(strOrderAddressLogItemLogMessage);
        }


        /// <summary>
        /// UPDATES the status on the record to COMPLETE all processing
        /// </summary>
        private void SetServiceLocationAddressToComplete()
        {
            string strOrderAddressLogItemLogMessage = "The SERVICE LOCATION address was found in SAP.  Processing is now COMPLETE.";

            // This is a SITE LOCATION and its address now EXISTS in SAP
            _objOrderAddress.MigrationStatusID = (int)Model.MigrationStatuses.Processing_COMPLETE;

            UpdateAndReloadTheAddressRecord(strOrderAddressLogItemLogMessage);

            // STOP processing, because we have reached the natural end for this record
            _blnProcessNextStep = false;

        }


        /// <summary>
        /// Sets the variable that is used for loggging, to fully identify the address record
        /// </summary>
        /// <returns></returns>
        private string CalcChangeLogString()
        {
            return String.Empty;
        }


        /// <summary>
        /// Updates the DB with the current values stored in the order address object's properties
        /// </summary>
        /// <param name="strLogMessage"></param>
        private void UpdateAndReloadTheAddressRecord(string strLogMessage)
        {
            string strLastError = String.Empty;

            DateTime dteNow = DateTime.Now;

            if (String.IsNullOrEmpty(_strUsername) == false)
            {
                strLogMessage = "(Processing triggered by User Action -->  Username = [" + _strUsername + "] ).  " + strLogMessage;
            }

            _objOrderAddress.DateTimeOfLastMigrationStatusUpdate = dteNow;
            _objOrderAddress.DateUpdated = dteNow;

            if (_objOrderAddress.UpdateOptimistic(out strLastError))
            {
                // Add a log item with the log message
                DAL.OrderAddressLogItem objOrderAddressLogItem = new DAL.OrderAddressLogItem();
                objOrderAddressLogItem.OrderAddressID = _objOrderAddress.OrderAddressID;
                objOrderAddressLogItem.MigrationStatusID = _objOrderAddress.MigrationStatusID;
                objOrderAddressLogItem.LogMessage = strLogMessage;
                objOrderAddressLogItem.DateCreated = DateTime.Now;
                objOrderAddressLogItem.DateUpdated = DateTime.Now;

                strLastError = String.Empty;
                if (objOrderAddressLogItem.Insert(out strLastError) == false)
                {
                    throw new Exception(String.Format("There was an error while trying to insert the log message detailing the order address record update.  Error Message = [{0}]", strLastError));
                }

                // repopulate the data from the DB
                _objOrderAddress = new DAL.OrderAddress().Get(_objOrderAddress.OrderAddressID.Value, out strLastError);

                if (_objOrderAddress == null)
                {
                    throw new Exception(String.Format("There was an error invoking the stored proc to pull the address record from the DB by its PK value.  Error Message = [{0}]", strLastError));
                }
            }
            else
            {
                throw new Exception(String.Format("The order address record's state could not be saved to the database and failed to Update. Error Message [{0}]", strLastError, _strMultipleFieldLogIdentifier));
            }
        }


        private void Update()
        {
            string strLastError = String.Empty;

            _objOrderAddress.DateUpdated = DateTime.Now;

            if (_objOrderAddress.UpdateOptimistic(out strLastError))
            {
                _objLogger.Debug("The record was updated in the DB");
            }
            else
            {
                throw new Exception(String.Format("The order address record's state could not be saved to the database and failed to Update. Error Message [{0}]", strLastError, _strMultipleFieldLogIdentifier));
            }
        }
        private void TruncateString(ref string strString)
        {
            if (strString?.Length > 1000)
            {
                strString = strString.Substring(0, 1000);
            }
        }


        //Decided not to use this guy, but left it in case we decide otherwise before go-live
        //private bool PreValidateDesiredStatusChange(Model.MigrationStatuses enmToBeMigrationStatus, Model.MigrationStatuses enmCurrentMigrationStatus)
        //{
        //    List<Model.MigrationStatuses> lstValidCurrentStatuses = new List<Model.MigrationStatuses>();
        //    switch (enmToBeMigrationStatus)
        //    {
        //        case Model.MigrationStatuses.STAGED_for_Processing:

        //            // None - this is for new records
        //            break;

        //        //case Model.MigrationStatuses.SITE_FOUND_or_CREATED_in_GLM:

        //        //    // If you would like to check GLM for a Site's existence, then the current status for the record must be:
        //        //    lstValidCurrentStatuses.Add(Model.MigrationStatuses.STAGED_for_Processing);
        //        //    lstValidCurrentStatuses.Add(Model.MigrationStatuses.FAILED_GLM_Site_EXISTENCE_CHECK);
        //        //    lstValidCurrentStatuses.Add(Model.MigrationStatuses.GLM_Site_EXISTENCE_CHECK_ABANDONED);
        //        //    break;

        //        //case Model.MigrationStatuses.FAILED_GLM_Site_EXISTENCE_CHECK:
        //        //    break;
        //        //case Model.MigrationStatuses.GLM_Site_EXISTENCE_CHECK_ABANDONED:
        //        //    break;
        //        //case Model.MigrationStatuses.SITE_CODE_FOUND_in_GLM:

        //        //    // If you would like to update the record to indicate that the SiteCode exists in GLM, then the current status for the record must be:
        //        //    lstValidCurrentStatuses.Add(Model.MigrationStatuses.SITE_FOUND_or_CREATED_in_GLM);
        //        //    break;

        //        //case Model.MigrationStatuses.STAGED_for_SITE_CODE_Creation_in_GLM:
        //        //    break;
        //        //case Model.MigrationStatuses.SITE_CODE_Succesfully_CREATED_in_GLM:
        //        //    break;
        //        //case Model.MigrationStatuses.FAILED_GLM_SITE_CODE_CREATION:
        //        //    break;
        //        //case Model.MigrationStatuses.GLM_SITE_CODE_CREATION_ABANDONED:
        //        //    break;
        //        //case Model.MigrationStatuses.SERVICE_LOCATION_STAGED_for_Processing:
        //        //    break;
        //        //case Model.MigrationStatuses.SERVICE_LOCATION_FOUND_or_CREATED_in_GLM:
        //        //    break;
        //        //case Model.MigrationStatuses.FAILED_GLM_Service_Location_EXISTENCE_CHECK_or_CREATION:
        //        //    break;
        //        //case Model.MigrationStatuses.GLM_Serive_Location_EXISTENCE_CHECK_ABANDONED:
        //        //    break;
        //        //case Model.MigrationStatuses.ADDRESS_AWAITING_CREATION_or_VALIDATION_in_SAP:
        //        //    break;
        //        //case Model.MigrationStatuses.ADDRESS_FOUND_IN_SAP_and_PROCESSING_COMPLETE:
        //        //    break;
        //        //case Model.MigrationStatuses.SAP_VALIDATION_FAILED:
        //        //    break;
        //        //case Model.MigrationStatuses.SAP_VALIDATION_ABANDONED:
        //        //    break;
        //        //case Model.MigrationStatuses.IGNORED_INDEFINITELY:
        //        //    break;
        //        default:
        //            break;
        //    }


        //    if (lstValidCurrentStatuses.Where(s => (s == enmCurrentMigrationStatus)).ToList().Count() > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}


        // Update the DB with the current state of the record
    }
}
