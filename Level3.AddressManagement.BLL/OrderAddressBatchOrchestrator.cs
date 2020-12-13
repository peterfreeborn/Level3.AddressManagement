using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using log4net;
using System.Diagnostics;
using Level3.AddressManagement.Model;

namespace Level3.AddressManagement.BLL
{
    public class OrderAddressBatchOrchestrator
    {
        // declare a log4net logger
        private static ILog _objLogger = LogManager.GetLogger(typeof(OrderAddressBatchOrchestrator));

        // Private Members
        private bool _blnIsLoaded;
        private List<DAL.OrderAddress> _lstOrderAddressRecords_ToProcess;
        private BatchType _enmBatchType;
        private int _intNumberOfThreads = 1;


        // Public Properties
        private List<string> _lstErrorMessages;
        public List<string> ErrorMessages
        {
            get { return _lstErrorMessages; }
            set { _lstErrorMessages = value; }
        }


        //Constructor
        public OrderAddressBatchOrchestrator()
        {
            _blnIsLoaded = false;
            _lstErrorMessages = new List<string>();
            _lstOrderAddressRecords_ToProcess = new List<DAL.OrderAddress>();
        }

        // Public Methods
        public bool Load(Model.BatchType enmBatchType)
        {
            try
            {
                // Instantiate a stopwatch to write runtimes to a log file
                Stopwatch objStopWatch = new Stopwatch();
                objStopWatch.Start();

                // Set the local variable for the batch type
                _enmBatchType = enmBatchType;

                // Execute the query against the SQL tables to load the list of records to process as part of this batch
                LoadBatchRecordsFromDB();

                // Log the time to retrieve the date
                string strTimeElapsed = StopwatchUtil.GetHumanReadableTimeElapsedString(objStopWatch);
                _objLogger.Info(String.Concat("Time elapsed while Loading the Order Address records from the DB that are to be included as part of this batch for processing.  Batch Type [", _enmBatchType, "] was [", strTimeElapsed, "] to retrieve [", _lstOrderAddressRecords_ToProcess.Count, "] records."));

            }
            catch (Exception ex)
            {
                // Create the error message
                string strErrorMessage = String.Format("An error was caught while trying to Load the list of order addresses that are to be included as part of this batch.  Error Message = [{0}]", ex.Message);

                // Log a warning to the log file
                _objLogger.Warn(strErrorMessage);

                // Add the error message to the error list so that the caller can access it
                _lstErrorMessages.Add(strErrorMessage);
            }

            // Get the number of threads to be used for multi-threading multiple address records in this batch.  i.e. - running more than one address through the workflow in parallel.
            _intNumberOfThreads = ConfigHelper.GetNumberOfThreads();

            _objLogger.Info(String.Concat("NUBER OF THREADS = [", _intNumberOfThreads.ToString(), "]"));

            _blnIsLoaded = (_lstErrorMessages.Count == 0);

            return _blnIsLoaded;
        }
        public bool Process()
        {
            try
            {
                // Instantiate a stopwatch to write runtimes to a log file
                Stopwatch objStopWatch = new Stopwatch();
                objStopWatch.Start();

                // Iterate over the order addresses that require processing
                Parallel.For(0, _lstOrderAddressRecords_ToProcess.Count, new ParallelOptions { MaxDegreeOfParallelism = _intNumberOfThreads }, i =>
              {
                   // Declare a base logging string
                   string strBaseLoggingString = OrderAddressUtil.CalcUniqueRecordIdentifierLoggingString(_lstOrderAddressRecords_ToProcess[i]);

                  try
                  {
                       // Instantiate an OrderAddressProcessor for the order
                       OrderAddressProcessor objOrderAddressProcessor = new OrderAddressProcessor();
                      if (objOrderAddressProcessor.Load(_lstOrderAddressRecords_ToProcess[i].OrderAddressID.Value) == false)
                      {
                          throw new Exception(String.Format("The OrderAddressProcessor could not be loaded from the address record, and so processing of this single transaction will be aborted and skipped. Error Messages = [{0}]", String.Join(" | ", objOrderAddressProcessor.ErrorMessages.ToArray())));
                      }

                      if (objOrderAddressProcessor.Process() == false)
                      {
                          throw new Exception(String.Format("The OrderAddressProcessor experienced an issue. Error Messages = [{0}]", String.Join(" | ", objOrderAddressProcessor.ErrorMessages.ToArray())));
                      }
                  }
                  catch (Exception ex)
                  {
                      string strErrorMessage = String.Format("There was an error while processing one of the order address records that needs processing into GLM and SAP.  This error is contained to a single Order Address record, which will now be skipped.  Other records contained in the result set will continue to process.  Error Message = [{0}].  {1}", ex.Message, strBaseLoggingString);

                       // Log a warning to the log file
                       _objLogger.Warn(strErrorMessage);

                       // Continue to the next record, so that one bad record cannot hault the processing of subsequent records that are next in the list
                       return;
                  }
              });

                // This is the code, without MULTI-THREADING, if ever needed.
                //// Iterate over the order addresses that require processing
                //for (int i = 0; i < _lstOrderAddressRecords_ToProcess.Count; i++)
                //{
                //    // Declare a base logging string
                //    string strBaseLoggingString = OrderAddressUtil.CalcUniqueRecordIdentifierLoggingString(_lstOrderAddressRecords_ToProcess[i]);

                //    try
                //    {
                //        // Instantiate an OrderAddressProcessor for the order
                //        OrderAddressProcessor objOrderAddressProcessor = new OrderAddressProcessor();
                //        if (objOrderAddressProcessor.Load(_lstOrderAddressRecords_ToProcess[i].OrderAddressID.Value) == false)
                //        {
                //            throw new Exception(String.Format("The OrderAddressProcessor could not be loaded from the address record, and so processing of this single transaction will be aborted and skipped. Error Messages = [{0}]", String.Join(" | ", objOrderAddressProcessor.ErrorMessages.ToArray())));
                //        }

                //        if (objOrderAddressProcessor.Process() == false)
                //        {
                //            throw new Exception(String.Format("The OrderAddressProcessor experienced an issue. Error Messages = [{0}]", String.Join(" | ", objOrderAddressProcessor.ErrorMessages.ToArray())));
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        string strErrorMessage = String.Format("There was an error while processing one of the order address records that needs processing into GLM and SAP.  This error is contained to a single Order Address record, which will now be skipped.  Other records contained in the result set will continue to process.  Error Message = [{0}].  {1}", ex.Message, strBaseLoggingString);

                //        // Log a warning to the log file
                //        _objLogger.Warn(strErrorMessage);

                //        // Continue to the next record, so that one bad record cannot hault the processing of subsequent records that are next in the list
                //        continue;
                //    }
                //}


                // Log the time to retrieve the date
                string strTimeElapsed = StopwatchUtil.GetHumanReadableTimeElapsedString(objStopWatch);
                _objLogger.Info(String.Concat("Time elapsed while processing the Order Address records included in this batch.  Batch Type [", _enmBatchType, "] was [", strTimeElapsed, "] to retrieve [", _lstOrderAddressRecords_ToProcess.Count, "] records."));

            }
            catch (Exception ex)
            {
                // Create the error message
                string strErrorMessage = String.Format("A global error was caught while trying to process the order addresses loaded into memory and that were to be included as part of this batch.  This failure occured outside the try/catch/continue meaning that subsequent records will NOT be processed.  Please investigate the issue and run the process again to process any records that had not yet been processed when things errored.  Error Message = [{0}]", ex.Message);

                // Log a warning to the log file
                _objLogger.Warn(strErrorMessage);

                // Add the error message to the error list so that the caller can access it
                _lstErrorMessages.Add(strErrorMessage);
            }

            _blnIsLoaded = (_lstErrorMessages.Count == 0);

            return _blnIsLoaded;
        }

        // Private Methods
        private void LoadBatchRecordsFromDB()
        {
            List<MigrationStatuses> lstMigrationStatuses = new List<MigrationStatuses>();

            switch (_enmBatchType)
            {
                case BatchType.AllAddressesNeedingProcessing:

                    lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_Processing);
                    lstMigrationStatuses.Add(MigrationStatuses.GLM_SITE_FOUND_or_CREATED);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SITE_existence_CHECK);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SITE_existence_CHECK_due_to_API_Call_Errors);
                    lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SITE_CODE_existence_CHECK);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SITE_CODE_existence_CHECK);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SITE_CODE_existence_CHECK_due_to_API_Call_Errors);
                    lstMigrationStatuses.Add(MigrationStatuses.SITE_CODE_FOUND_in_GLM);
                    lstMigrationStatuses.Add(MigrationStatuses.SITE_CODE_does_NOT_EXIST_in_GLM);
                    lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SITE_CODE_ASSIGNMENT_TRIGGERING_in_GLM);
                    lstMigrationStatuses.Add(MigrationStatuses.SITE_CODE_ASSIGNMENT_Successfully_TRIGGERED_in_GLM);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_trying_to_TRIGGER_the_ASSIGNMENT_of_a_SITE_CODE_in_GLM);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SITE_CODE_ASSIGNMENT_due_to_API_Call_Errors);
                    lstMigrationStatuses.Add(MigrationStatuses.WORK_ITEM_QUEUED_in_GLM_for_DELAYED_SITE_CODE_ASSIGNMENT);
                    lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SAP_SITE_LOCATION_SEARCH);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_SAP_SITE_LOCATION_ADDRESS_SEARCH);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_SAP_SITE_LOCATION_SEARCH_due_to_API_Call_Errors);
                    lstMigrationStatuses.Add(MigrationStatuses.Site_Location_ADDRESS_FOUND_in_SAP);
                    lstMigrationStatuses.Add(MigrationStatuses.Site_Location_ADDRESS_does_NOT_EXIST_in_SAP);
                    lstMigrationStatuses.Add(MigrationStatuses.READY_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_attempting_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_attempts_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP_due_to_API_Call_Errors);
                    lstMigrationStatuses.Add(MigrationStatuses.READY_for_ADDRESS_TYPE_BRANCHING_Logic_EVALUATION);
                    lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_GLM_SERVICE_LOCATION_SEARCH);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SERVICE_LOCATION_SEARCH);
                    ///lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SERVICE_LOCATION_SEARCH_due_to_API_Call_Errors);
                    lstMigrationStatuses.Add(MigrationStatuses.SERVICE_LOCATION_FOUND_in_GLM);
                    lstMigrationStatuses.Add(MigrationStatuses.SERVICE_LOCATION_does_NOT_EXIST_in_GLM);
                    lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SERVICE_LOCATION_CREATION_in_GLM);
                    lstMigrationStatuses.Add(MigrationStatuses.SERVICE_LOCATION_Successfully_CREATED_in_GLM);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_the_GLM_SERVICE_LOCATION_CREATION_attempt);
                    // lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SERVICE_LOCATION_CREATION_due_to_API_Call_Errors);
                    lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_S_CODE_existence_CHECK);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_S_CODE_existence_CHECK);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_S_CODE_existence_CHECK_due_to_API_Call_Errors);
                    lstMigrationStatuses.Add(MigrationStatuses.S_CODE_FOUND_in_GLM);
                    lstMigrationStatuses.Add(MigrationStatuses.S_CODE_does_NOT_EXIST_in_GLM);
                    lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_S_CODE_CREATION_in_GLM);
                    lstMigrationStatuses.Add(MigrationStatuses.S_CODE_Successfully_CREATED_in_GLM);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_S_CODE_CREATION_attempt);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_S_CODE_CREATION_due_to_API_Call_Errors);
                    lstMigrationStatuses.Add(MigrationStatuses.WORK_ITEM_QUEUED_in_GLM_for_DELAYED_S_CODE_CREATION);
                    lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SAP_SERVICE_LOCATION_SEARCH);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_SAP_SERVICE_LOCATION_ADDRESS_SEARCH);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_SAP_SERVICE_LOCATION_SEARCH_due_to_API_Call_Errors);
                    lstMigrationStatuses.Add(MigrationStatuses.Service_Location_ADDRESS_FOUND_in_SAP);
                    lstMigrationStatuses.Add(MigrationStatuses.Service_Location_ADDRESS_does_NOT_EXIST_in_SAP);
                    lstMigrationStatuses.Add(MigrationStatuses.READY_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_attempting_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_attempts_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP_due_to_API_Call_Errors);
                    //lstMigrationStatuses.Add(MigrationStatuses.Processing_COMPLETE);
                    //lstMigrationStatuses.Add(MigrationStatuses.IGNORED_indefinitely);

                    break;

                case BatchType.AlreadyInWorkflow:

                    //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_Processing);
                    lstMigrationStatuses.Add(MigrationStatuses.GLM_SITE_FOUND_or_CREATED);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SITE_existence_CHECK);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SITE_existence_CHECK_due_to_API_Call_Errors);
                    lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SITE_CODE_existence_CHECK);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SITE_CODE_existence_CHECK);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SITE_CODE_existence_CHECK_due_to_API_Call_Errors);
                    lstMigrationStatuses.Add(MigrationStatuses.SITE_CODE_FOUND_in_GLM);
                    lstMigrationStatuses.Add(MigrationStatuses.SITE_CODE_does_NOT_EXIST_in_GLM);
                    lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SITE_CODE_ASSIGNMENT_TRIGGERING_in_GLM);
                    lstMigrationStatuses.Add(MigrationStatuses.SITE_CODE_ASSIGNMENT_Successfully_TRIGGERED_in_GLM);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_trying_to_TRIGGER_the_ASSIGNMENT_of_a_SITE_CODE_in_GLM);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SITE_CODE_ASSIGNMENT_due_to_API_Call_Errors);
                    lstMigrationStatuses.Add(MigrationStatuses.WORK_ITEM_QUEUED_in_GLM_for_DELAYED_SITE_CODE_ASSIGNMENT);
                    lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SAP_SITE_LOCATION_SEARCH);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_SAP_SITE_LOCATION_ADDRESS_SEARCH);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_SAP_SITE_LOCATION_SEARCH_due_to_API_Call_Errors);
                    lstMigrationStatuses.Add(MigrationStatuses.Site_Location_ADDRESS_FOUND_in_SAP);
                    lstMigrationStatuses.Add(MigrationStatuses.Site_Location_ADDRESS_does_NOT_EXIST_in_SAP);
                    lstMigrationStatuses.Add(MigrationStatuses.READY_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_attempting_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_attempts_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP_due_to_API_Call_Errors);
                    lstMigrationStatuses.Add(MigrationStatuses.READY_for_ADDRESS_TYPE_BRANCHING_Logic_EVALUATION);
                    lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_GLM_SERVICE_LOCATION_SEARCH);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SERVICE_LOCATION_SEARCH);
                    ///lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SERVICE_LOCATION_SEARCH_due_to_API_Call_Errors);
                    lstMigrationStatuses.Add(MigrationStatuses.SERVICE_LOCATION_FOUND_in_GLM);
                    lstMigrationStatuses.Add(MigrationStatuses.SERVICE_LOCATION_does_NOT_EXIST_in_GLM);
                    lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SERVICE_LOCATION_CREATION_in_GLM);
                    lstMigrationStatuses.Add(MigrationStatuses.SERVICE_LOCATION_Successfully_CREATED_in_GLM);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_the_GLM_SERVICE_LOCATION_CREATION_attempt);
                    // lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SERVICE_LOCATION_CREATION_due_to_API_Call_Errors);
                    lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_S_CODE_existence_CHECK);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_S_CODE_existence_CHECK);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_S_CODE_existence_CHECK_due_to_API_Call_Errors);
                    lstMigrationStatuses.Add(MigrationStatuses.S_CODE_FOUND_in_GLM);
                    lstMigrationStatuses.Add(MigrationStatuses.S_CODE_does_NOT_EXIST_in_GLM);
                    lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_S_CODE_CREATION_in_GLM);
                    lstMigrationStatuses.Add(MigrationStatuses.S_CODE_Successfully_CREATED_in_GLM);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_S_CODE_CREATION_attempt);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_S_CODE_CREATION_due_to_API_Call_Errors);
                    lstMigrationStatuses.Add(MigrationStatuses.WORK_ITEM_QUEUED_in_GLM_for_DELAYED_S_CODE_CREATION);
                    lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SAP_SERVICE_LOCATION_SEARCH);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_SAP_SERVICE_LOCATION_ADDRESS_SEARCH);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_SAP_SERVICE_LOCATION_SEARCH_due_to_API_Call_Errors);
                    lstMigrationStatuses.Add(MigrationStatuses.Service_Location_ADDRESS_FOUND_in_SAP);
                    lstMigrationStatuses.Add(MigrationStatuses.Service_Location_ADDRESS_does_NOT_EXIST_in_SAP);
                    lstMigrationStatuses.Add(MigrationStatuses.READY_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_attempting_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_attempts_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP_due_to_API_Call_Errors);
                    //lstMigrationStatuses.Add(MigrationStatuses.Processing_COMPLETE);
                    //lstMigrationStatuses.Add(MigrationStatuses.IGNORED_indefinitely);

                    break;
                case BatchType.OnlyNewFromCDW:

                    lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_Processing);
                    break;

                case BatchType.AlreadyInWorkflowWithNoErrors:

                    //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_Processing);
                    lstMigrationStatuses.Add(MigrationStatuses.GLM_SITE_FOUND_or_CREATED);
                    //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SITE_existence_CHECK);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SITE_existence_CHECK_due_to_API_Call_Errors);
                    lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SITE_CODE_existence_CHECK);
                    //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SITE_CODE_existence_CHECK);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SITE_CODE_existence_CHECK_due_to_API_Call_Errors);
                    lstMigrationStatuses.Add(MigrationStatuses.SITE_CODE_FOUND_in_GLM);
                    lstMigrationStatuses.Add(MigrationStatuses.SITE_CODE_does_NOT_EXIST_in_GLM);
                    lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SITE_CODE_ASSIGNMENT_TRIGGERING_in_GLM);
                    lstMigrationStatuses.Add(MigrationStatuses.SITE_CODE_ASSIGNMENT_Successfully_TRIGGERED_in_GLM);
                    //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_trying_to_TRIGGER_the_ASSIGNMENT_of_a_SITE_CODE_in_GLM);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SITE_CODE_ASSIGNMENT_due_to_API_Call_Errors);
                    lstMigrationStatuses.Add(MigrationStatuses.WORK_ITEM_QUEUED_in_GLM_for_DELAYED_SITE_CODE_ASSIGNMENT);
                    lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SAP_SITE_LOCATION_SEARCH);
                    //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_SAP_SITE_LOCATION_ADDRESS_SEARCH);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_SAP_SITE_LOCATION_SEARCH_due_to_API_Call_Errors);
                    lstMigrationStatuses.Add(MigrationStatuses.Site_Location_ADDRESS_FOUND_in_SAP);
                    lstMigrationStatuses.Add(MigrationStatuses.Site_Location_ADDRESS_does_NOT_EXIST_in_SAP);
                    lstMigrationStatuses.Add(MigrationStatuses.READY_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP);
                    //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_attempting_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_attempts_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP_due_to_API_Call_Errors);
                    lstMigrationStatuses.Add(MigrationStatuses.READY_for_ADDRESS_TYPE_BRANCHING_Logic_EVALUATION);
                    lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_GLM_SERVICE_LOCATION_SEARCH);
                    //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SERVICE_LOCATION_SEARCH);
                    ///lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SERVICE_LOCATION_SEARCH_due_to_API_Call_Errors);
                    lstMigrationStatuses.Add(MigrationStatuses.SERVICE_LOCATION_FOUND_in_GLM);
                    lstMigrationStatuses.Add(MigrationStatuses.SERVICE_LOCATION_does_NOT_EXIST_in_GLM);
                    lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SERVICE_LOCATION_CREATION_in_GLM);
                    lstMigrationStatuses.Add(MigrationStatuses.SERVICE_LOCATION_Successfully_CREATED_in_GLM);
                    //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_the_GLM_SERVICE_LOCATION_CREATION_attempt);
                    // lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SERVICE_LOCATION_CREATION_due_to_API_Call_Errors);
                    lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_S_CODE_existence_CHECK);
                    //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_S_CODE_existence_CHECK);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_S_CODE_existence_CHECK_due_to_API_Call_Errors);
                    lstMigrationStatuses.Add(MigrationStatuses.S_CODE_FOUND_in_GLM);
                    lstMigrationStatuses.Add(MigrationStatuses.S_CODE_does_NOT_EXIST_in_GLM);
                    lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_S_CODE_CREATION_in_GLM);
                    lstMigrationStatuses.Add(MigrationStatuses.S_CODE_Successfully_CREATED_in_GLM);
                    //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_S_CODE_CREATION_attempt);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_S_CODE_CREATION_due_to_API_Call_Errors);
                    lstMigrationStatuses.Add(MigrationStatuses.WORK_ITEM_QUEUED_in_GLM_for_DELAYED_S_CODE_CREATION);
                    lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SAP_SERVICE_LOCATION_SEARCH);
                    //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_SAP_SERVICE_LOCATION_ADDRESS_SEARCH);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_SAP_SERVICE_LOCATION_SEARCH_due_to_API_Call_Errors);
                    lstMigrationStatuses.Add(MigrationStatuses.Service_Location_ADDRESS_FOUND_in_SAP);
                    lstMigrationStatuses.Add(MigrationStatuses.Service_Location_ADDRESS_does_NOT_EXIST_in_SAP);
                    lstMigrationStatuses.Add(MigrationStatuses.READY_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP);
                    //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_attempting_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_attempts_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP_due_to_API_Call_Errors);
                    //lstMigrationStatuses.Add(MigrationStatuses.Processing_COMPLETE);
                    //lstMigrationStatuses.Add(MigrationStatuses.IGNORED_indefinitely);

                    break;

                case BatchType.Errors:

                    //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_Processing);
                    //lstMigrationStatuses.Add(MigrationStatuses.GLM_SITE_FOUND_or_CREATED);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SITE_existence_CHECK);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SITE_existence_CHECK_due_to_API_Call_Errors);
                    //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SITE_CODE_existence_CHECK);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SITE_CODE_existence_CHECK);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SITE_CODE_existence_CHECK_due_to_API_Call_Errors);
                    //lstMigrationStatuses.Add(MigrationStatuses.SITE_CODE_FOUND_in_GLM);
                    //lstMigrationStatuses.Add(MigrationStatuses.SITE_CODE_does_NOT_EXIST_in_GLM);
                    //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SITE_CODE_ASSIGNMENT_TRIGGERING_in_GLM);
                    //lstMigrationStatuses.Add(MigrationStatuses.SITE_CODE_ASSIGNMENT_Successfully_TRIGGERED_in_GLM);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_trying_to_TRIGGER_the_ASSIGNMENT_of_a_SITE_CODE_in_GLM);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SITE_CODE_ASSIGNMENT_due_to_API_Call_Errors);
                    //lstMigrationStatuses.Add(MigrationStatuses.WORK_ITEM_QUEUED_in_GLM_for_DELAYED_SITE_CODE_ASSIGNMENT);
                    //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SAP_SITE_LOCATION_SEARCH);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_SAP_SITE_LOCATION_ADDRESS_SEARCH);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_SAP_SITE_LOCATION_SEARCH_due_to_API_Call_Errors);
                    //lstMigrationStatuses.Add(MigrationStatuses.Site_Location_ADDRESS_FOUND_in_SAP);
                    //lstMigrationStatuses.Add(MigrationStatuses.Site_Location_ADDRESS_does_NOT_EXIST_in_SAP);
                    //lstMigrationStatuses.Add(MigrationStatuses.READY_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_attempting_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_attempts_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP_due_to_API_Call_Errors);
                    //lstMigrationStatuses.Add(MigrationStatuses.READY_for_ADDRESS_TYPE_BRANCHING_Logic_EVALUATION);
                    //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_GLM_SERVICE_LOCATION_SEARCH);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SERVICE_LOCATION_SEARCH);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SERVICE_LOCATION_SEARCH_due_to_API_Call_Errors);
                    //lstMigrationStatuses.Add(MigrationStatuses.SERVICE_LOCATION_FOUND_in_GLM);
                    //lstMigrationStatuses.Add(MigrationStatuses.SERVICE_LOCATION_does_NOT_EXIST_in_GLM);
                    //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SERVICE_LOCATION_CREATION_in_GLM);
                    //lstMigrationStatuses.Add(MigrationStatuses.SERVICE_LOCATION_Successfully_CREATED_in_GLM);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_the_GLM_SERVICE_LOCATION_CREATION_attempt);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SERVICE_LOCATION_CREATION_due_to_API_Call_Errors);
                    //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_S_CODE_existence_CHECK);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_S_CODE_existence_CHECK);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_S_CODE_existence_CHECK_due_to_API_Call_Errors);
                    //lstMigrationStatuses.Add(MigrationStatuses.S_CODE_FOUND_in_GLM);
                    //lstMigrationStatuses.Add(MigrationStatuses.S_CODE_does_NOT_EXIST_in_GLM);
                    //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_S_CODE_CREATION_in_GLM);
                    //lstMigrationStatuses.Add(MigrationStatuses.S_CODE_Successfully_CREATED_in_GLM);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_S_CODE_CREATION_attempt);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_S_CODE_CREATION_due_to_API_Call_Errors);
                    //lstMigrationStatuses.Add(MigrationStatuses.WORK_ITEM_QUEUED_in_GLM_for_DELAYED_S_CODE_CREATION);
                    //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SAP_SERVICE_LOCATION_SEARCH);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_SAP_SERVICE_LOCATION_ADDRESS_SEARCH);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_SAP_SERVICE_LOCATION_SEARCH_due_to_API_Call_Errors);
                    //lstMigrationStatuses.Add(MigrationStatuses.Service_Location_ADDRESS_FOUND_in_SAP);
                    //lstMigrationStatuses.Add(MigrationStatuses.Service_Location_ADDRESS_does_NOT_EXIST_in_SAP);
                    //lstMigrationStatuses.Add(MigrationStatuses.READY_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_attempting_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_attempts_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP_due_to_API_Call_Errors);
                    //lstMigrationStatuses.Add(MigrationStatuses.Processing_COMPLETE);
                    //lstMigrationStatuses.Add(MigrationStatuses.IGNORED_indefinitely);

                    break;

                case BatchType.SAPChecks:

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
                    lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SAP_SITE_LOCATION_SEARCH);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_SAP_SITE_LOCATION_ADDRESS_SEARCH);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_SAP_SITE_LOCATION_SEARCH_due_to_API_Call_Errors);
                    //lstMigrationStatuses.Add(MigrationStatuses.Site_Location_ADDRESS_FOUND_in_SAP);
                    lstMigrationStatuses.Add(MigrationStatuses.Site_Location_ADDRESS_does_NOT_EXIST_in_SAP);
                    lstMigrationStatuses.Add(MigrationStatuses.READY_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_attempting_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP);
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
                    lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SAP_SERVICE_LOCATION_SEARCH);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_SAP_SERVICE_LOCATION_ADDRESS_SEARCH);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_SAP_SERVICE_LOCATION_SEARCH_due_to_API_Call_Errors);
                    //lstMigrationStatuses.Add(MigrationStatuses.Service_Location_ADDRESS_FOUND_in_SAP);
                    lstMigrationStatuses.Add(MigrationStatuses.Service_Location_ADDRESS_does_NOT_EXIST_in_SAP);
                    lstMigrationStatuses.Add(MigrationStatuses.READY_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP);
                    lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_attempting_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP);
                    //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_attempts_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP_due_to_API_Call_Errors);
                    //lstMigrationStatuses.Add(MigrationStatuses.Processing_COMPLETE);
                    //lstMigrationStatuses.Add(MigrationStatuses.IGNORED_indefinitely);
                    break;

                default:
                    throw new NotImplementedException();
            }

            int[] arrMigrationStatusIDs = Array.ConvertAll<MigrationStatuses, int>(lstMigrationStatuses.ToArray(), value => (int)value);

            string strInClause = String.Concat("tblOrderAddress.MigrationStatusID IN (", String.Join(",", arrMigrationStatusIDs), ")");

            string strWhereClause = String.Format("{0}", strInClause);
            string strOrderByClause = String.Format("{0}", "tblOrderAddress.OrderAddressID");

            string strLastError = String.Empty;

            _lstOrderAddressRecords_ToProcess = new DAL.OrderAddress().Search(strWhereClause, strOrderByClause, out strLastError, null, null);

        }



    }
}
