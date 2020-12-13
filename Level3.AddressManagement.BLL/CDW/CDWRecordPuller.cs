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
    public class CDWRecordPuller
    {
        // declare a log4net logger
        private static ILog _objLogger = LogManager.GetLogger(typeof(CDWRecordPuller));


        // Private Members
        private DateTime? _dteMinimumFirstOrderCreateDate;
        private DAL.ChangesetDateGroup _objChangesetDateGroup;

        private bool _blnIsLoaded;
        private List<DAL.CDWOrderAddressRecord> _lstCDWOrderAddressRecords_ToProcess;


        // Public Properties
        private List<string> _lstErrorMessages;
        public List<string> ErrorMessages
        {
            get { return _lstErrorMessages; }
            set { _lstErrorMessages = value; }
        }




        // Constructor
        public CDWRecordPuller()
        {
            _blnIsLoaded = false;
            _lstErrorMessages = new List<string>();
            _lstCDWOrderAddressRecords_ToProcess = new List<DAL.CDWOrderAddressRecord>();
        }


        // Public Methods
        /// <summary>
        /// Executes a query against the CDW to retrieve a list of EON and Pipeline orders that are NOT yet in GLM.  The DateTime supplied as the input parameter is used in the WHERE clause of the query to ensure we only pull in Order Addresses created SINCE a specific date.  If no date is supplied, the default value will be pulled from the Config file.  If records in GLM have a "bad CLII" ("Z{integer}") then there is a possibility that the address may already exist in GLM. "
        /// </summary>
        /// <param name="dteMinimumFirstOrderCreateDate"></param>
        /// <returns></returns>
        public bool Load(DateTime? dteMinimumFirstOrderCreateDate = null)
        {
            try
            {
                // Instantiate a stopwatch to write runtimes to a log file
                Stopwatch objStopWatch = new Stopwatch();
                objStopWatch.Start();


                // Set the local variable for the date
                _dteMinimumFirstOrderCreateDate = dteMinimumFirstOrderCreateDate;

                // If no date was supplied, pull the value from the config file.  If the value is not found and/or cannot be parsed, an exception will be thrown so that we hard stop here.
                if (_dteMinimumFirstOrderCreateDate.HasValue == false)
                {
                    LoadDateFromConfig();
                }

                // Load the dates for the changeset
                LoadChangesetDatesFromDB();


                // Get the PL-SQL query text to be executed against the CDW (Oracle)
                // OLD - HARD CODED QUERY (ie - NOT Dynamic) --> string strSQL = DAL.SQLQueryBuilder.GetSQLStatement_DWOrderAddressesChangeset(_dteMinimumFirstOrderCreateDate.Value,_objChangesetDateGroup.MAX_OPE_LAST_MODIFY_DATE.Value,_objChangesetDateGroup.MAX_PL_LAST_MODIFY_DATE.Value,_objChangesetDateGroup.MAX_PS_LAST_MODIFY_DATE.Value);
                string strSQL = DynamicQueryBuilder.GetDynamicManagedServiceOrdersQuery(_dteMinimumFirstOrderCreateDate.Value, _objChangesetDateGroup.MAX_OPE_LAST_MODIFY_DATE.Value, _objChangesetDateGroup.MAX_PL_LAST_MODIFY_DATE.Value, _objChangesetDateGroup.MAX_PS_LAST_MODIFY_DATE.Value);
                _objLogger.Info(String.Concat("About to Execute the following Query against CDW.  PL-SQL =  [", strSQL, "]."));

                // Execute the query against CDW and load the results into the list of address records to be processed
                _lstCDWOrderAddressRecords_ToProcess = new DAL.CDWOrderAddressRecord().GetRecords(strSQL);

                // Log the time to retrieve the date
                string strTimeElapsed = StopwatchUtil.GetHumanReadableTimeElapsedString(objStopWatch);
                _objLogger.Info(String.Concat("Time elapsed while retrieving the Order Address records from CDW that are not yet in GLM and that have a FirstOrderCreateDate >= [", _dteMinimumFirstOrderCreateDate.Value.ToString("MM/dd/yyyy"), "] was [", strTimeElapsed, "] to retrieve [", _lstCDWOrderAddressRecords_ToProcess.Count, "] records."));


            }
            catch (Exception ex)
            {
                // Create the error message
                string strErrorMessage = String.Format("An error was caught while trying to Load the list of order addresses that are not yet in GLM from the CDW.  Error Message = [{0}]", ex.Message);

                // Log a warning to the log file
                _objLogger.Warn(strErrorMessage);

                // Add the error message to the error list so that the caller can access it
                _lstErrorMessages.Add(strErrorMessage);
            }

            _blnIsLoaded = (_lstErrorMessages.Count == 0);

            return _blnIsLoaded;
        }


        /// <summary>
        /// Iterates over the list populated via the load method, and stages a record in the customer SQL server database if it does not yet already exist
        /// </summary>
        /// <returns></returns>
        public bool Process()
        {
            try
            {
                // Ensure that the load method was called and succeeded
                EnforceLoadSuccess();

                // Instantiate a stopwatch to write runtimes to a log file
                Stopwatch objStopWatch = new Stopwatch();
                objStopWatch.Start();

                int intAlreadyExisted = 0;
                int intNewRecords = 0;

                // Iterate over the order addresses that require processing
                for (int i = 0; i < _lstCDWOrderAddressRecords_ToProcess.Count; i++)
                {
                    // Declare a base logging string
                    string strBaseLoggingString = CDWOrderAddressRecordUtil.CalcUniqueRecordIdentifierLoggingString(_lstCDWOrderAddressRecords_ToProcess[i]);

                    try
                    {
                        // Translate the string valye from CDW for the Order Sytem Code to the corresponding ENUM
                        Model.OrderSystemOfRecords enmOrderSystemOfRecord = TranslateDW_SOURCE_SYSTEM_CDToOrderSystemOfRecord.Translate(_lstCDWOrderAddressRecords_ToProcess[i].DataWarehouseSourceSystemCode);

                        // Declare some variables to send in to the cleanup method
                        // Initialize the return fields
                        string strCDWCustomerOrderNumber = String.Empty;
                        string strCDWAddressOne = String.Empty;
                        string strCDWCity = String.Empty;
                        string strCDWState = String.Empty;
                        string strCDWPostalCode = String.Empty;
                        string strCDWCountry = String.Empty;
                        string strCDWFloor = String.Empty;
                        string strCDWRoom = String.Empty;
                        string strCDWSuite = String.Empty;

                        // Trim and set the values that will get inserted/compared with out custom record store.  If there is an issue or exception, an error will be added to the error list and the current record will be skipped.
                        if (CDWOrderAddressRecordUtil.CleanupRawAddressFields(_lstCDWOrderAddressRecords_ToProcess[i], out strCDWCustomerOrderNumber, out strCDWAddressOne, out strCDWCity, out strCDWState, out strCDWPostalCode, out strCDWCountry, out strCDWFloor, out strCDWRoom, out strCDWSuite) == false)
                        {
                            throw new Exception(String.Format("There was an error while trying to cleanse the order address record pulled from CDW.  {0}", strBaseLoggingString));
                        }

                        string strLastError = String.Empty;

                        // Check to see if the record exist... by trying to load it from the custom sql server table
                        DAL.OrderAddress objOrderAddress = new DAL.OrderAddress().Get((int)enmOrderSystemOfRecord, strCDWCustomerOrderNumber, strCDWAddressOne, strCDWCity, strCDWState, strCDWPostalCode, strCDWCountry, strCDWFloor, strCDWRoom, strCDWSuite, out strLastError);

                        if (objOrderAddress != null)
                        {
                            intAlreadyExisted++;

                            // The record already exists so check its status
                            if ((objOrderAddress.MigrationStatusID == (int)MigrationStatuses.STAGED_for_Processing) || (objOrderAddress.MigrationStatusID == (int)MigrationStatuses.IGNORED_indefinitely))
                            {
                                // Leave this record as is, and don't bother with a log message

                            }
                            else
                            {
                                // Add a log item to reflect that this record has been detected again... after it began its trip through the workflow

                            }
                        }
                        else
                        {
                            intNewRecords++;

                            // The record does NOT yet exist, so add it and then set the local member
                            InsertAsNewOrderAddress(_lstCDWOrderAddressRecords_ToProcess[i], enmOrderSystemOfRecord, strCDWCustomerOrderNumber, strCDWAddressOne, strCDWCity, strCDWState, strCDWPostalCode, strCDWCountry, strCDWFloor, strCDWRoom, strCDWSuite);
                        }
                    }
                    catch (Exception ex)
                    {
                        string strErrorMessage = String.Format("There was an error while processing one of the order address records pulled from the CDW.  This error is contained to a single CDW record, which will now be skipped.  Other records contained in the result set will continue to process.  Error Message = [{0}].  {1}", ex.Message, strBaseLoggingString);

                        // Log a warning to the log file
                        _objLogger.Warn(strErrorMessage);

                        // Continue to the next record, so that one bad record cannot hault the processing of subsequent records that are next in the list
                        continue;
                    }

                }

                // Log the time to retrieve the date
                string strTimeElapsed = StopwatchUtil.GetHumanReadableTimeElapsedString(objStopWatch);
                string strLogMessage = String.Concat("Time elapsed while processing the Order Address records received from CDW to insert them into the custom database tables and queue them for further was [", strTimeElapsed, "]. -->  Number of Errors = [", _lstErrorMessages.Count.ToString(), "], Number of Total Results Processed =  [", _lstCDWOrderAddressRecords_ToProcess.Count, "] records.  Number of Records that ALREADY existed = [", intAlreadyExisted.ToString(), "], Number of NEW records added to the DB = [", intNewRecords.ToString(), "]");
                _objLogger.Info(strLogMessage);

                string strNote = String.Concat("The newest changeset of new or changed address records from the CDW has been processed.  ", strLogMessage);
                SystemLogItemUtil.InsertLogItem(strNote, "CDW Sync");

            }
            catch (Exception ex)
            {
                // Create the error message
                string strErrorMessage = String.Format("An error was caught while trying to iterate over the raw records retrieved from CDW.  The nature of this issue will cause the system to dump the rest of the records that have yet to be processed, as the problem cannot be skipped over.  Error Message = [{0}]", ex.Message);

                // Log a warning to the log file
                _objLogger.Warn(strErrorMessage);

                // Add the error message to the error list so that the caller can access it
                _lstErrorMessages.Add(strErrorMessage);
            }

            return (_lstErrorMessages.Count == 0);

        }


        // Private Methods
        private void LoadDateFromConfig()
        {
            // Get the value from the config file
            _dteMinimumFirstOrderCreateDate = ConfigHelper.GetMinimumFirstOrderCreateDateForCDWPull();
        }
        private void LoadChangesetDatesFromDB()
        {
            // SELECT the MAX changeset dates from the SQL Server Table
            _objChangesetDateGroup = new DAL.ChangesetDateGroup().GetRecords();

            if (_objChangesetDateGroup == null)
            {
                throw new Exception("There was an error while trying to query the SQL Server DB for the MAX Change Dates to be used for the changeset query to be executed against the CDW.");
            }

            if (_objChangesetDateGroup.MAX_OPE_LAST_MODIFY_DATE.HasValue == false)
            {
                _objChangesetDateGroup.MAX_OPE_LAST_MODIFY_DATE = _dteMinimumFirstOrderCreateDate;
            }

            if (_objChangesetDateGroup.MAX_PL_LAST_MODIFY_DATE.HasValue == false)
            {
                _objChangesetDateGroup.MAX_PL_LAST_MODIFY_DATE = _dteMinimumFirstOrderCreateDate;
            }

            if (_objChangesetDateGroup.MAX_PS_LAST_MODIFY_DATE.HasValue == false)
            {
                _objChangesetDateGroup.MAX_PS_LAST_MODIFY_DATE = _dteMinimumFirstOrderCreateDate;
            }
        }
        private void EnforceLoadSuccess()
        {
            if (_blnIsLoaded == false)
            {
                throw new Exception("The load method was not call or did not succeed, and so subsequent and dependant methods in this class cannot be invoked.");
            }
        }
        private void InsertAsNewOrderAddress(DAL.CDWOrderAddressRecord objCDWOrderAddressRecord, Model.OrderSystemOfRecords enmOrderSystemOfRecord, string strCDWCustomerOrderNumber, string strCDWAddressOne, string strCDWCity, string strCDWState, string strCDWPostalCode, string strCDWCountry, string strCDWFloor, string strCDWRoom, string strCDWSuite)
        {

            // Instantiate a new object
            DAL.OrderAddress objOrderAddress = new DAL.OrderAddress();

            // Declare a common date value to use
            DateTime dteNow = DateTime.Now;

            // Analyze the floor, room, and suite to determine if this address should be considerd a SITE or a SERVICE LOCATION
            objOrderAddress.OrderAddressTypeID = (int)CDWOrderAddressRecordUtil.TranslateRawAddressFieldsToOrderAddressType(strCDWFloor, strCDWRoom, strCDWSuite);

            // Set the default system status for new records
            objOrderAddress.MigrationStatusID = (int)MigrationStatuses.STAGED_for_Processing;

            // Set the SOR already translated/provided by the caller
            objOrderAddress.OrderSystemOfRecordID = (int)enmOrderSystemOfRecord;

            // Set the address field values, using the strings that were already cleaned by the caller as part of the existence check
            // The null overrride to empty string is important, since SQL SQL Syntax and the way the SELECT queries are written see a difference between EmptyString and NULL.  i.e. - NULL values received from the CDW are translated to empty string
            objOrderAddress.CDWCustomerOrderNumber = strCDWCustomerOrderNumber ?? String.Empty;
            objOrderAddress.CDWAddressOne = strCDWAddressOne ?? String.Empty;
            objOrderAddress.CDWCity = strCDWCity ?? String.Empty;
            objOrderAddress.CDWState = strCDWState ?? String.Empty;
            objOrderAddress.CDWPostalCode = strCDWPostalCode ?? String.Empty;
            objOrderAddress.CDWCountry = strCDWCountry ?? String.Empty;
            objOrderAddress.CDWFloor = strCDWFloor ?? String.Empty;
            objOrderAddress.CDWRoom = strCDWRoom ?? String.Empty;
            objOrderAddress.CDWSuite = strCDWSuite ?? String.Empty;

            objOrderAddress.CDWCLII = objCDWOrderAddressRecord.CLIICode;

            // Validate the CLII provided
            objOrderAddress.ValidCLII = CLIIUtility.IsValidCLII(objCDWOrderAddressRecord.CLIICode);

            // Set all appropriate defaults for values that we don't yet have
            objOrderAddress.NumberOfFailedGLMSiteCalls = 0;
            objOrderAddress.ExistsInGLMAsSite = false;
            objOrderAddress.GLMPLNumber = String.Empty;
            objOrderAddress.NumberOfFailedGLMSiteCodeExistenceCalls = 0;
            objOrderAddress.NumberOfFailedGLMSiteCodeCreationCalls = 0;
            objOrderAddress.GLMSiteCode = String.Empty;
            objOrderAddress.HasGLMSiteCode = false;
            objOrderAddress.NumberOfFailedSAPSiteAddressSearchCalls = 0;
            objOrderAddress.NumberOfFailedSAPSiteAddressImportCalls = 0;
            objOrderAddress.ExistsInSAPAsSiteAddress = false;
            objOrderAddress.NumberOfRecordsInSAPWithPL = 0;
            objOrderAddress.NumberOfFailedGLMServiceLocationSearchCalls = 0;
            objOrderAddress.NumberOfFailedGLMServiceLocationCreationCalls = 0;
            objOrderAddress.GLMSLNumber = String.Empty;
            objOrderAddress.ExistsInGLMAsServiceLocation = false;
            objOrderAddress.NumberOfFailedGLMSCodeExistenceCalls = 0;
            objOrderAddress.NumberOfFailedGLMSCodeCreationCalls = 0;
            objOrderAddress.GLMSCode = String.Empty;
            objOrderAddress.HasGLMSCode = false;
            objOrderAddress.NumberOfFailedSAPServiceLocationAddressSearchCalls = 0;
            objOrderAddress.NumberOfFailedSAPServiceLocationAddressImportCalls = 0;
            objOrderAddress.ExistsInSAPAsServiceLocationAddress = false;
            objOrderAddress.NumberOfRecordsInSAPWithSL = 0;

            objOrderAddress.DateTimeOfLastMigrationStatusUpdate = dteNow;
            //objOrderAddress.DateTimeOfLastDupDetection = leave null
            objOrderAddress.DateCreated = dteNow;
            objOrderAddress.DateUpdated = dteNow;

            objOrderAddress.ServiceOrderNumber = objCDWOrderAddressRecord.ServiceOrderNumber;
            objOrderAddress.FIRST_ORDER_CREATE_DT = objCDWOrderAddressRecord.FIRST_ORDER_CREATE_DT;
            objOrderAddress.OPE_LAST_MODIFY_DATE = objCDWOrderAddressRecord.OPE_LAST_MODIFY_DATE;
            objOrderAddress.PL_LAST_MODIFY_DATE = objCDWOrderAddressRecord.PL_LAST_MODIFY_DATE;
            objOrderAddress.PS_LAST_MODIFY_DATE = objCDWOrderAddressRecord.PS_LAST_MODIFY_DATE;

            string strLastError = String.Empty;
            if (objOrderAddress.Insert(out strLastError))
            {
                // The insert worked
                // Add a log item with the log message
                DAL.OrderAddressLogItem objOrderAddressLogItem = new DAL.OrderAddressLogItem();
                objOrderAddressLogItem.OrderAddressID = objOrderAddress.OrderAddressID;
                objOrderAddressLogItem.MigrationStatusID = objOrderAddress.MigrationStatusID;
                objOrderAddressLogItem.LogMessage = "The order address was just pulled from the CDW and is now staged for further processing downstream into GLM and SAP.";
                objOrderAddressLogItem.DateCreated = dteNow;
                objOrderAddressLogItem.DateUpdated = dteNow;

                strLastError = String.Empty;
                if (objOrderAddressLogItem.Insert(out strLastError) == false)
                {
                    throw new Exception(String.Format("There was an error while tyring to insert the log message detailing the order address record update.  Error Message = [{0}]", strLastError));
                }
            }
            else
            {
                throw new Exception(String.Format("There was an error while trying to insert the record retrieved from the CDW into the customer Order Address table in SQL Server (tblOrderAddress).  Error Message = [{0}]", strLastError));
            }

        }
    }
}
