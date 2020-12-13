using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;


namespace Level3.AddressManagement.BLL
{
    public class ConfigHelper
    {
        private static ILog _objLogger = LogManager.GetLogger(typeof(ConfigHelper));


        // MS Excel File Processing
        public static string GetRootDirectory()
        {
            // Get the app setting value from the web.config file
            return System.Configuration.ConfigurationManager.AppSettings["RootWorkspaceDirectory"];
        }



        // GLM
        public static string GetGLMBaseUrl()
        {
            // Get the app setting value from the web.config file
            return System.Configuration.ConfigurationManager.AppSettings["GLMBaseUrl"];
        }

        public static string GetGLMAuthorizationHeaderUsername()
        {
            // Get the app setting value from the web.config file
            return System.Configuration.ConfigurationManager.AppSettings["GLMAuthorizationHeaderUsername"];
        }

        public static string GetGLMAuthorizationHeaderApplicationID()
        {
            // Get the app setting value from the web.config file
            return System.Configuration.ConfigurationManager.AppSettings["GLMAuthorizationHeaderApplicationID"];
        }


        // CDW

        public static DateTime GetMinimumFirstOrderCreateDateForCDWPull()
        {
            try
            {
                // Get the value from the config file
                string strMinimumFirstOrderCreateDateForCDWPull = System.Configuration.ConfigurationManager.AppSettings["MinimumFirstOrderCreateDateForCDWPull"];

                // Try to convert it to a DateTime
                DateTime dteMinimumFirstOrderCreateDateForCDWPull;
                if (DateTime.TryParse(strMinimumFirstOrderCreateDateForCDWPull, out dteMinimumFirstOrderCreateDateForCDWPull))
                {
                    _objLogger.Debug(String.Concat("The 'MinimumFirstOrderCreateDateForCDWPull' configuration key was just retrieved from the web.config file.  MinimumFirstOrderCreateDateForCDWPull = [", dteMinimumFirstOrderCreateDateForCDWPull.ToString(), "]"));

                    return dteMinimumFirstOrderCreateDateForCDWPull;
                }
                else
                {
                    // It cannot be converted to a DateTime, so throw an exception.  We DO NOT want the code to proceed with a Min Date as that may cause us to pull-in many more addresses than we want!!!
                    throw new Exception(String.Format("The value was found in the config file, but could NOT be parsed to a valid datetime. MinimumFirstOrderCreateDateForCDWPull =[{0}]", strMinimumFirstOrderCreateDateForCDWPull));
                }
            }
            catch (Exception ex)
            {
                string strErrorMessage = String.Format("The value for the 'MinimumFirstOrderCreateDateForCDWPull' app setting was not found in the config file OR it could not be parsed to a valid date.  Error Message = [{0}] ", ex.Message);
                _objLogger.Error(strErrorMessage);
                throw new Exception(strErrorMessage);
            }
        }



        // SAP
        public static string GetSAPBaseUrl()
        {
            // Get the app setting value from the web.config file
            return System.Configuration.ConfigurationManager.AppSettings["SAPBaseUrl"];
        }


        // Email notifications sent by system
        public static string GetStatsEmailRecipientList()
        {
            string strLastError = String.Empty;
            return new DAL.SystemConfigItem().Get((int)Model.SystemConfigItems.StatsEmailRecipientList, out strLastError).ConfigSettingValue;
        }

        public static string GetSystemNotificationEmailRecipientList()
        {
            string strLastError = String.Empty;
            return new DAL.SystemConfigItem().Get((int)Model.SystemConfigItems.SystemNotificationEmailRecipientList, out strLastError).ConfigSettingValue;
        }


        public static string GetNotifactionSenderEmailAddressValue()
        {
            return "no-reply@level3.com";
        }

        public static int GetNumberOfThreads()
        {
            // Declare a DEFAULT number of threads
            int intNumberOfThreads = 1;
            int intMaxNumberOfThreads = 25; // Don't change this unless you know damn well what you are doing and have talked to GLM and SAP API Manageers
            int intAdjustedNumberOfThreads = intMaxNumberOfThreads;

            try
            {
                if (int.TryParse(System.Configuration.ConfigurationManager.AppSettings["NumberOfThreads"], out intNumberOfThreads))
                {
                    // The value was read from the config file, so ENFORCE a hard coded max of 25 threads
                    if (intNumberOfThreads > intMaxNumberOfThreads)
                    {
                        intNumberOfThreads = intAdjustedNumberOfThreads;
                        _objLogger.Error(String.Format("The value supplied in the config file for the 'NumberOfThreads' exceeds the MAX hard coded, allowable value intended to ensure we are good stewards of the APIs we consume.  MAX number of threads = [{0}], Value in Config File = [{1}], Adjusted Thread Count that will be used = [{2}].  Please adjust the value in the config file after this process finishes.", intMaxNumberOfThreads, intNumberOfThreads, intAdjustedNumberOfThreads));
                    }
                }
                else
                {
                    intNumberOfThreads = 1;
                    _objLogger.Error(String.Format("There value supplied in the config file for the 'NumberOfThreads' failed to parse to an integer.  A default value will be used for this run, but plase add the required app key when this process completes."));
                }
            }
            catch (Exception ex)
            {
                // Leave the default thread count of 1 in tact
                _objLogger.Error(String.Format("There is NO value supplied in the config file for the 'NumberOfThreads' or the value could not be parsed to an integer.  A default value will be used for this run, but plase add the required app key when this process completes."));
                // EAT THE ERROR
            }

            return intNumberOfThreads;
        }


    }
}
