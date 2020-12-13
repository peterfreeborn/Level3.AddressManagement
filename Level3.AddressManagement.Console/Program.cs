using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;
using Level3.AddressManagement.BLL;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
[assembly: log4net.Config.Repository()]


namespace Level3.AddressManagement.Console
{
    class Program
    {
        // Declare the static logger object that will be used to implement logging via log4net
        private static ILog _objLogger = LogManager.GetLogger(typeof(Program));


        private const string _cstrHelp = "/?";
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                _objLogger.Info("Main Console Application: Entry Point.");
                List<string> lstSwitches = new List<string>();

                bool blnShowUsage = false;

                if (args != null && args.Length > 0)
                {
                    foreach (string strItem in args)
                    {
                        if (strItem == _cstrHelp)
                        {
                            blnShowUsage = true;
                        }
                        lstSwitches.Add(strItem);
                    }

                    _objLogger.Info(String.Concat("Switches supplied:", String.Join(",", lstSwitches.ToArray())));
                }
                else
                {
                    // At least one switch is required.  Give message.
                    _objLogger.Info("   No switches found.  Help info upcoming... prepare yourself to be wow'd");
                    ShowUsage();
                }

                if (!blnShowUsage)
                {
#if (DEBUG)
                    DateTime dtNow = DateTime.Now;
                    System.Console.WriteLine(String.Concat("Launching Console App at", dtNow.TimeOfDay.ToString()));
#endif

                    LaunchProcesses(lstSwitches);


#if (DEBUG)
                    System.Console.WriteLine();
                    System.Console.WriteLine();
                    System.Console.WriteLine("Press the ENTER key to exit...");
                    System.Console.ReadLine();
#endif
                }
                else
                {
                    // Usage /help was requested
                    ShowUsage();
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("An error occurred while processing.  Please check the application log file(s) for additional information. Error Message = [", ex.Message, "]");
            }
            _objLogger.Info("Main Console Application: Exit Point");
        }

        private static void LaunchProcesses(List<string> lstSwitches)
        {

            // For some of the operations, we may want to abort if there were previous issues
            bool blnProcessResult = true;

            #region EMAIL SENDS - THESE RUN EVEN IF ANOTHER PROCESS IS RUNNING IN PARALLEL
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Trigger the workflow engine to send the stats email
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (blnProcessResult && lstSwitches.Contains("/statsEmail"))
            {
                _objLogger.Info("   STARTED sending STATs email...");

                try
                {
                    NotificationManager objNotificationManager = new NotificationManager();
                    if (objNotificationManager.SendEmail_Stats())
                    {
                        string strLoggingMessage = String.Empty;
                        strLoggingMessage = "The STATS email was sent without issue.";
                        _objLogger.Info(strLoggingMessage);
                    }
                    else
                    {
                        _objLogger.Error(String.Concat("There was one or more errors while trying to send the STATs email. Errors = [", String.Join(" <|> ", objNotificationManager.ErrorMessages.ToArray()), "]"));
                    }


                    string strNote = String.Concat("The back-end console application just sent the SYSTEM STATs email to all recipients.");
                    SystemLogItemUtil.InsertLogItem(strNote, "The back-end console application just sent the SYSTEM STATs email to all recipients.");

                }
                catch (Exception ex)
                {
                    string strErrorMessage = String.Concat("There was a SERIOUS GLOBAL exception that affects the sening of the SYSTEM STATs. The console app will continue to process additional switches, but the function to send the STATs email will not continue. Error Message = [", ex.Message, "]");
                    _objLogger.Error(strErrorMessage);

                    SystemLogItemUtil.InsertLogItem(strErrorMessage, "The back-end console application COULD NOT COMPLETE the sending of the STATs email.");
                }

                _objLogger.Info("   finished sending STATs email.");
            }


            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Trigger the workflow engine to send the user reminder
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (blnProcessResult && lstSwitches.Contains("/reminderEmail"))
            {
                _objLogger.Info("   STARTED sending USER REMINDER email...");

                try
                {
                    NotificationManager objNotificationManager = new NotificationManager();
                    if (objNotificationManager.SendEmail_UserActionNeededReminder())
                    {
                        string strLoggingMessage = String.Empty;
                        strLoggingMessage = "The User Reminder email was sent without issue.";
                        _objLogger.Info(strLoggingMessage);
                    }
                    else
                    {
                        _objLogger.Error(String.Concat("There was one or more errors while trying to send the User Reminder email. Errors = [", String.Join(" <|> ", objNotificationManager.ErrorMessages.ToArray()), "]"));
                    }


                    string strNote = String.Concat("The back-end console application just sent the USER ACTION REQUIRED REMINDER email to all recipients.");
                    SystemLogItemUtil.InsertLogItem(strNote, "The back-end console application just sent the USER REMINDER email to all recipients.");

                }
                catch (Exception ex)
                {
                    string strErrorMessage = String.Concat("There was a SERIOUS GLOBAL exception that affects the sening of the USER REMINDER. The console app will continue to process additional switches, but the function to send the USER REMINDER email will not continue. Error Message = [", ex.Message, "]");
                    _objLogger.Error(strErrorMessage);

                    SystemLogItemUtil.InsertLogItem(strErrorMessage, "The back-end console application COULD NOT COMPLETE the sending of the USER REMINDER email.");
                }

                _objLogger.Info("   finished sending USER REMINDER email.");
            }

            if (!blnProcessResult)
            {
                System.Console.WriteLine("An error occurred while processing.  Please check the log file for additional information.");
            }
            #endregion


            // THIS ENSURES THAT ONLY ONE CONSOLE APP PROCESS RUNS AT A TIME...
            // If the console app is not being run in the VS Debugger....Check to see if another instance of this same console app is running, and if so, exit this one without further processing... since we don't want to run more than one instance at a time
#if !(DEBUG)
            int intNumberOfRunningProcessesWithSameName = System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count();
            if ((intNumberOfRunningProcessesWithSameName == 1)) // Since this current process fired, then we know ther is at least one.. so check for others||())
            {

            }
            else
            {
                string strNote = String.Concat("The back-end console application started, BUT since another instance of it is still running from the last scheduled start time, this new instance will now exit.");
                SystemLogItemUtil.InsertLogItem(strNote, "The back-end console application just QUIT.");
                _objLogger.Error("Another instance of this CONSOLE APP is already running, and so this new process will now exit to allow the existing process to run to completion without interference.");
                return;
            }
#endif

            _objLogger.Info("   Launching Processes");



            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Trigger the pull of address data from CDW
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (blnProcessResult && lstSwitches.Any(l => l.Contains("/pullFromCDW")))
            {
                try
                {

                    _objLogger.Info("About to Query CDW for NEW missing addresses");
                    BLL.CDWRecordPuller objCDWRecordPuller = new BLL.CDWRecordPuller();
                    if (objCDWRecordPuller.Load())
                    {
                        if (objCDWRecordPuller.Process())
                        {

                        }
                        else
                        {
                            // Log the errors that were experienced to file
                            string strErrorMessage = String.Format("There were one or more issues while trying to process the records pulled from the CDW into the customer SQL server tables. Some of the records may have been succesffuly processed, while others failed. Only addresses with issues will be listed within the error messages logged here.  Error Messages = [{0}]", String.Join(" | ", objCDWRecordPuller.ErrorMessages.ToArray()));
                            _objLogger.Error(strErrorMessage);
                        }
                    }
                    else
                    {
                        // There were one or more issues with trying to pull in the records from CDW  
                        throw new Exception(String.Format("There was an error while trying to LOAD / PULL missing address records from the CDW.  The program will NOT attempt to process any records, as the issue was  been processed. Only addresses with issues will be listed within the error messages logged here.  Error Messages = [{0}]", String.Join(" | ", objCDWRecordPuller.ErrorMessages.ToArray())));
                    }

                    _objLogger.Info("Done staging NEW missing addresses from CDW");
                }
                catch (Exception ex)
                {
                    string strErrorMessage = String.Format("There was a global exception while trying to execute the Console application responsible for migrating addresses into GLM and SAP.  All processing has haulted.  Error Message = [{0}]", ex.Message);

                    // Log the error to the log file
                    _objLogger.Error(strErrorMessage);

                    // Write the error to the console incase someone is watching this process from the console
                    System.Console.WriteLine(strErrorMessage);

                    // Add a system log record
                    SystemLogItemUtil.InsertLogItem(strErrorMessage, "The back-end console application COULD NOT complete the pull of records in from CDW.");
                }

                string strNote = String.Concat("The back-end console application just finished pulling in the newest changeset of new or changed address records from the CDW.  It may or may not have completed without errors.  Please review earlier log messages for details.");
                SystemLogItemUtil.InsertLogItem(strNote, "CDW Sync");

            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Trigger the workflow engine to analyze any order addresses that are currently active within the workflow and that require further processing against SAP
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (blnProcessResult && lstSwitches.Contains("/sapChecks"))
            {
                string strNote = String.Empty;

                try
                {
                    _objLogger.Info("   STARTED procesing all order addresses that require handling...");

                    BLL.OrderAddressBatchOrchestrator objOrderAddressBatchOrchestrator = new BLL.OrderAddressBatchOrchestrator();
                    if (objOrderAddressBatchOrchestrator.Load(Model.BatchType.SAPChecks))
                    {
                        string strLoggingMessage = String.Empty;
                        if (objOrderAddressBatchOrchestrator.Process())
                        {
                            strLoggingMessage = "The order address batch orchestrator completed processing without issue";
                        }
                        else
                        {
                            strLoggingMessage = "The order address batch orchestrator experienced one or more errors while trying to process the group of records.";
                            _objLogger.Error(String.Concat("There was one or more errors while trying to process the addresses in the system requiring processing. Errors = [", String.Join(" <|> ", objOrderAddressBatchOrchestrator.ErrorMessages.ToArray()), "]"));
                        }
                        _objLogger.Info(strLoggingMessage);
                    }
                    else
                    {
                        _objLogger.Error(String.Concat("There was one or more errors while trying to load the order address batch orchestrator. Errors = [", String.Join(" <|> ", objOrderAddressBatchOrchestrator.ErrorMessages.ToArray()), "]"));
                    }

                    _objLogger.Info("   FINISHED procesing all order addresses that required handling.");

                    strNote = String.Concat("The back-end console application just completed procesing all order addresses that required existence checks in SAP.");
                    SystemLogItemUtil.InsertLogItem(strNote, "The back-end console application just completed procesing all order addresses that required handling.");

                }
                catch (Exception ex)
                {
                    string strErrorMessage = String.Concat("There was a SERIOUS GLOBAL exception that affects the Order Address Batch Orchestrator and the processing of Order Address transactions.  The console app will continue to process additional switches, but the function to process order addresses has failed. Error Message = [", ex.Message, "]");
                    _objLogger.Error(strErrorMessage);
                    SystemLogItemUtil.InsertLogItem(strErrorMessage, "The back-end console application COULD NOT complete the processing of all order addresses requiring existence check in SAP.");
                }

                strNote = String.Concat("The back-end console application just finished running existence checks against SAP for any records queued for such an action.");
                SystemLogItemUtil.InsertLogItem(strNote, "SAP Existence Check");

            }


            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Trigger the workflow engine to analyze any order addresses that are currently active within the workflow and that require further processing
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (blnProcessResult && lstSwitches.Contains("/processInWorkflow"))
            {
                try
                {
                    _objLogger.Info("   STARTED procesing all order addresses that require handling...");

                    BLL.OrderAddressBatchOrchestrator objOrderAddressBatchOrchestrator = new BLL.OrderAddressBatchOrchestrator();
                    if (objOrderAddressBatchOrchestrator.Load(Model.BatchType.AlreadyInWorkflow))
                    {
                        string strLoggingMessage = String.Empty;
                        if (objOrderAddressBatchOrchestrator.Process())
                        {
                            strLoggingMessage = "The order address batch orchestrator completed processing without issue";
                        }
                        else
                        {
                            strLoggingMessage = "The order address batch orchestrator experienced one or more errors while trying to process the group of records.";
                            _objLogger.Error(String.Concat("There was one or more errors while trying to process the addresses in the system requiring processing. Errors = [", String.Join(" <|> ", objOrderAddressBatchOrchestrator.ErrorMessages.ToArray()), "]"));
                        }
                        _objLogger.Info(strLoggingMessage);
                    }
                    else
                    {
                        _objLogger.Error(String.Concat("There was one or more errors while trying to load the order address batch orchestrator. Errors = [", String.Join(" <|> ", objOrderAddressBatchOrchestrator.ErrorMessages.ToArray()), "]"));
                    }

                    _objLogger.Info("   FINISHED procesing all order addresses that required handling.");

                    string strNote = String.Concat("The back-end console application just completed procesing all order addresses that are or were IN WORKFLOW at the time the process kicked off.");
                    SystemLogItemUtil.InsertLogItem(strNote, "Back-end console application; IN WORKFLOW.");

                }
                catch (Exception ex)
                {
                    string strErrorMessage = String.Concat("There was a SERIOUS GLOBAL exception that affects the Order Address Batch Orchestrator and the processing of Order Address transactions.  The console app will continue to process additional switches, but the function to process order addresses has failed. Error Message = [", ex.Message, "]");
                    _objLogger.Error(strErrorMessage);
                    SystemLogItemUtil.InsertLogItem(strErrorMessage, "The back-end console application COULD NOT complete the processing of all order addresses that are currently IN WORKFLOW.");
                }
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Trigger the workflow engine to analyze any order addresses that are currently active within the workflow and that require further processing
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (blnProcessResult && lstSwitches.Contains("/processNonErrorsInWorkflow"))
            {
                try
                {
                    _objLogger.Info("   STARTED procesing all order addresses that require handling...");

                    BLL.OrderAddressBatchOrchestrator objOrderAddressBatchOrchestrator = new BLL.OrderAddressBatchOrchestrator();
                    if (objOrderAddressBatchOrchestrator.Load(Model.BatchType.AlreadyInWorkflowWithNoErrors))
                    {
                        string strLoggingMessage = String.Empty;
                        if (objOrderAddressBatchOrchestrator.Process())
                        {
                            strLoggingMessage = "The order address batch orchestrator completed processing without issue";
                        }
                        else
                        {
                            strLoggingMessage = "The order address batch orchestrator experienced one or more errors while trying to process the group of records.";
                            _objLogger.Error(String.Concat("There was one or more errors while trying to process the addresses in the system requiring processing. Errors = [", String.Join(" <|> ", objOrderAddressBatchOrchestrator.ErrorMessages.ToArray()), "]"));
                        }
                        _objLogger.Info(strLoggingMessage);
                    }
                    else
                    {
                        _objLogger.Error(String.Concat("There was one or more errors while trying to load the order address batch orchestrator. Errors = [", String.Join(" <|> ", objOrderAddressBatchOrchestrator.ErrorMessages.ToArray()), "]"));
                    }

                    _objLogger.Info("   FINISHED procesing all order addresses that required handling.");

                    string strNote = String.Concat("The back-end console application just completed procesing all order addresses that are or were IN WORKFLOW and that WERE NOT ERRORING or ABANDONED at the time the process kicked off.");
                    SystemLogItemUtil.InsertLogItem(strNote, "Back-end console application; IN WORKFLOW NOT ERRORING OR ABANDONED.");

                }
                catch (Exception ex)
                {
                    string strErrorMessage = String.Concat("There was a SERIOUS GLOBAL exception that affects the Order Address Batch Orchestrator and the processing of Order Address transactions.  The console app will continue to process additional switches, but the function to process order addresses has failed. Error Message = [", ex.Message, "]");
                    _objLogger.Error(strErrorMessage);
                    SystemLogItemUtil.InsertLogItem(strErrorMessage, "The back-end console application COULD NOT complete the processing of all order addresses that are currently IN WORKFLOW and that WERE NOT ERRORING or ABANDONED.");
                }
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Trigger the workflow engine to analyze any order addresses that are currently active within the workflow, have failed on the last call, and that wil be reattempted
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (blnProcessResult && lstSwitches.Contains("/retryErrors"))
            {
                try
                {
                    _objLogger.Info("   STARTED procesing all order addresses that require handling...");

                    BLL.OrderAddressBatchOrchestrator objOrderAddressBatchOrchestrator = new BLL.OrderAddressBatchOrchestrator();
                    if (objOrderAddressBatchOrchestrator.Load(Model.BatchType.Errors))
                    {
                        string strLoggingMessage = String.Empty;
                        if (objOrderAddressBatchOrchestrator.Process())
                        {
                            strLoggingMessage = "The order address batch orchestrator completed processing without issue";
                        }
                        else
                        {
                            strLoggingMessage = "The order address batch orchestrator experienced one or more errors while trying to process the group of records.";
                            _objLogger.Error(String.Concat("There was one or more errors while trying to process the addresses in the system requiring processing. Errors = [", String.Join(" <|> ", objOrderAddressBatchOrchestrator.ErrorMessages.ToArray()), "]"));
                        }
                        _objLogger.Info(strLoggingMessage);
                    }
                    else
                    {
                        _objLogger.Error(String.Concat("There was one or more errors while trying to load the order address batch orchestrator. Errors = [", String.Join(" <|> ", objOrderAddressBatchOrchestrator.ErrorMessages.ToArray()), "]"));
                    }

                    _objLogger.Info("   FINISHED procesing all order addresses that required handling.");

                    string strNote = String.Concat("The back-end console application just completed procesing all order addresses that required RE-TRY due to previously FAILED API Calls.");
                    SystemLogItemUtil.InsertLogItem(strNote, "Back-end console application; RETRY ERRORS.");

                }
                catch (Exception ex)
                {
                    string strErrorMessage = String.Concat("There was a SERIOUS GLOBAL exception that affects the Order Address Batch Orchestrator and the processing of Order Address transactions that needed RETRY due to previous API Call Errors.  The console app will continue to process additional switches, but the function to process order addresses has failed. Error Message = [", ex.Message, "]");
                    _objLogger.Error(strErrorMessage);
                    SystemLogItemUtil.InsertLogItem(strErrorMessage, "FAILED: Back-end console application; RETRY ERRORS.");
                }
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Trigger the workflow engine to analyze any order addresses that were just added to the system from the CDW
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (blnProcessResult && lstSwitches.Contains("/startNew"))
            {
                try
                {
                    _objLogger.Info("   STARTED procesing all order addresses that require handling...");

                    BLL.OrderAddressBatchOrchestrator objOrderAddressBatchOrchestrator = new BLL.OrderAddressBatchOrchestrator();
                    if (objOrderAddressBatchOrchestrator.Load(Model.BatchType.OnlyNewFromCDW))
                    {
                        string strLoggingMessage = String.Empty;
                        if (objOrderAddressBatchOrchestrator.Process())
                        {
                            strLoggingMessage = "The order address batch orchestrator completed processing without issue";
                        }
                        else
                        {
                            strLoggingMessage = "The order address batch orchestrator experienced one or more errors while trying to process the group of records.";
                            _objLogger.Error(String.Concat("There was one or more errors while trying to process the addresses in the system requiring processing. Errors = [", String.Join(" <|> ", objOrderAddressBatchOrchestrator.ErrorMessages.ToArray()), "]"));
                        }
                        _objLogger.Info(strLoggingMessage);
                    }
                    else
                    {
                        _objLogger.Error(String.Concat("There was one or more errors while trying to load the order address batch orchestrator. Errors = [", String.Join(" <|> ", objOrderAddressBatchOrchestrator.ErrorMessages.ToArray()), "]"));
                    }

                    _objLogger.Info("   FINISHED procesing all order addresses that required handling.");

                    string strNote = String.Concat("The back-end console application just completed procesing all order addresses that were NEWLY SYNC'D in from CDW.");
                    SystemLogItemUtil.InsertLogItem(strNote, "Back-end console application; NEWLY SYNC'D.");

                }
                catch (Exception ex)
                {
                    string strErrorMessage = String.Concat("There was a SERIOUS GLOBAL exception that affects the Order Address Batch Orchestrator and the processing of Order Address transactions that were NEWLY Sync'd in from CDW.  The console app will continue to process additional switches, but the function to process order addresses has failed. Error Message = [", ex.Message, "]");
                    _objLogger.Error(strErrorMessage);
                    SystemLogItemUtil.InsertLogItem(strErrorMessage, "FAILED: Back-end console application; START NEW.");
                }
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Trigger the workflow engine to analyze any order addresses that are currently active within the workflow and that require further processing
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (blnProcessResult && lstSwitches.Contains("/processAll"))
            {
                try
                {
                    _objLogger.Info("   STARTED procesing all order addresses that require handling...");

                    BLL.OrderAddressBatchOrchestrator objOrderAddressBatchOrchestrator = new BLL.OrderAddressBatchOrchestrator();
                    if (objOrderAddressBatchOrchestrator.Load(Model.BatchType.AllAddressesNeedingProcessing))
                    {
                        string strLoggingMessage = String.Empty;
                        if (objOrderAddressBatchOrchestrator.Process())
                        {
                            strLoggingMessage = "The order address batch orchestrator completed processing without issue";
                        }
                        else
                        {
                            strLoggingMessage = "The order address batch orchestrator experienced one or more errors while trying to process the group of records.";
                            _objLogger.Error(String.Concat("There was one or more errors while trying to process the addresses in the system requiring processing. Errors = [", String.Join(" <|> ", objOrderAddressBatchOrchestrator.ErrorMessages.ToArray()), "]"));
                        }
                        _objLogger.Info(strLoggingMessage);
                    }
                    else
                    {
                        _objLogger.Error(String.Concat("There was one or more errors while trying to load the order address batch orchestrator. Errors = [", String.Join(" <|> ", objOrderAddressBatchOrchestrator.ErrorMessages.ToArray()), "]"));
                    }

                    _objLogger.Info("   FINISHED procesing all order addresses that required handling.");

                    string strNote = String.Concat("The back-end console application just completed PROCESSING ALL order addresses that required handling in the system.");
                    SystemLogItemUtil.InsertLogItem(strNote, "Back-end console application; PROCESS ALL.");

                }
                catch (Exception ex)
                {
                    string strErrorMessage = String.Concat("There was a SERIOUS GLOBAL exception that affects the Order Address Batch Orchestrator and the PROCESSING of ALL Order Address transactions.  The console app will continue to process additional switches, but the function to process order addresses has failed. Error Message = [", ex.Message, "]");
                    _objLogger.Error(strErrorMessage);
                    SystemLogItemUtil.InsertLogItem(strErrorMessage, "FAILED: Back-end console application; PROCESS ALL.");
                }
            }


        }

        private static void ShowUsage()
        {
            System.Console.WriteLine("Description:");
            System.Console.WriteLine("This application allows for the execution of various backend processes comprising Level3.AddressManagement's workflow logic engine accessible via the commmand-line interface.  It is leveraged to identity managed order addresses missing from GLM and/or SAP, and handles the addition of the address to those systems.");
            System.Console.WriteLine("");
            System.Console.WriteLine("Switches:");
            System.Console.WriteLine("");
            System.Console.WriteLine("[/?]");
            System.Console.WriteLine("Displays HELP information");
            System.Console.WriteLine("");
            System.Console.WriteLine("[/pullFromCDW]");
            System.Console.WriteLine("Queries The Oracle for new/changed payments and pulls them into workflow");
            System.Console.WriteLine("");
            System.Console.WriteLine("[/processAll]");
            System.Console.WriteLine("Analyzes payment records active within the workflow for applicable status adjustments");
            System.Console.WriteLine("");
            System.Console.WriteLine("(Press the ENTER key to exit)");
            System.Console.ReadLine();
        }

        //[STAThread]
        //static void Main(string[] args)
        //{


        //    try
        //    {

        //        #region DEPRECATED
        //        //// Load the filenames to process from disk
        //        //BLL.SourceFileManager objSourceFileManager = new BLL.SourceFileManager();
        //        //if (objSourceFileManager.Load())
        //        //{
        //        //    List<string> lstFileNamesToProcess = objSourceFileManager.GetLoadedFileNamesToProcess();

        //        //    _objLogger.Debug("About to iterate over the files that need processing");

        //        //    string strCurrentFile = String.Empty;

        //        //    // Iterate over the FILE NAMES to process
        //        //    for (int i = 0; i < lstFileNamesToProcess.Count; i++)
        //        //    {
        //        //        try
        //        //        {
        //        //            // Set the variable that holds the full path to the current file
        //        //            strCurrentFile = lstFileNamesToProcess[i];

        //        //            // Open the spreadhsheet, and read all the rows into a list of SourceAddress objects, and then process each record against GLM's web service layer
        //        //            BLL.AddressMigrationManager objAddressMigrationManager = new BLL.AddressMigrationManager();
        //        //            if (objAddressMigrationManager.Load(strCurrentFile))
        //        //            {
        //        //                _objLogger.Info(String.Format("The Excel rows containing addresses have been read into memory and are ready for processing to GLM.  Number of Addresses to Process = [{0}]", objAddressMigrationManager.SourceAddresses_Raw.Count));

        //        //                // The address records have been loaded from the Excel file, so process them against GLM
        //        //                if (objAddressMigrationManager.MigrateAddresses())
        //        //                {

        //        //                }
        //        //                else
        //        //                {
        //        //                    // There were one or more issues with trying to migrate the addresses to GLM    
        //        //                    throw new Exception(String.Format("There were one or more issues with trying to migrate the addresses to GLM. Some of the records may have been processed. Only addresses with issues will be listed within the error messages logged here.  Filename = [{0}], Error Messages = [{1}]", strCurrentFile, String.Join(" | ", objAddressMigrationManager.ErrorMessages.ToArray())));
        //        //                }

        //        //            }
        //        //            else
        //        //            {
        //        //                throw new Exception(String.Format("The Excel File could not be opened and/or processed.  None of the address records contained in the spreadsheet were processed.  Filename = [{0}], Error Messages = [{1}]", strCurrentFile, String.Join(" | ", objAddressMigrationManager.ErrorMessages.ToArray())));
        //        //            }
        //        //        }
        //        //        catch (Exception ex)
        //        //        {
        //        //            _objLogger.Error(String.Format("There was an error while trying to process the excel file. The current file will be SKIPPED, and any other files in the processing directory WILL CONTINUE to process. File [{0}], Error Messages [{1}]", strCurrentFile, ex.Message));

        //        //            // Continue to the next FILE
        //        //            continue;
        //        //        }


        //        //    }
        //        //}
        //        //else
        //        //{
        //        //    throw new Exception(String.Format("Could not load the files from disk.  Error Messages = [{0}]", String.Join(" | ", objSourceFileManager.ErrorMessages.ToArray())));
        //        //} 
        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        string strErrorMessage = String.Format("There was a global exception while trying to execute the Console application responsible for migrating addresses into GLM and SAP.  All processing has haulted.  Error Message = [{0}]", ex.Message);

        //        // Log the error to the log file
        //        _objLogger.Error(strErrorMessage);

        //        // Write the error to the console incase someone is watching this process from the console
        //        System.Console.WriteLine(strErrorMessage);
        //    }



        //}
    }
}
