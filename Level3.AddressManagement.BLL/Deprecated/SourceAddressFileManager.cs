using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;

namespace Level3.AddressManagement.BLL
{
    public class SourceAddressFileManager
    {
        // Declare the static logger object that will be used to implement logging via log4net
        private static ILog _objLogger = LogManager.GetLogger(typeof(SourceAddressFileManager));


        // Private Members
        private string _strBaseLoggingString;
        private string _strRootDirectoryPath;


        // Public Props
        private List<string> _lstErrorMessages;
        public List<string> ErrorMessages
        {
            get { return _lstErrorMessages; }
            set { _lstErrorMessages = value; }
        }


        private List<string> _lstXLSXFilesToProcess;
        public List<string> XLSXFilesToProcess
        {
            get { return _lstXLSXFilesToProcess; }
            set { _lstXLSXFilesToProcess = value; }
        }



        // Constructor
        public SourceAddressFileManager()
        {
            _lstErrorMessages = new List<string>();
            _lstXLSXFilesToProcess = new List<string>();
        }

        // Public Methods
        public bool LoadFilesNamesFromDisk(string strProcessingRootDirectoryPath)
        {
            _strRootDirectoryPath = strProcessingRootDirectoryPath;
            _strBaseLoggingString = String.Format("Processing Directory Path = [{0}]", _strRootDirectoryPath);

            try
            {
                if (Directory.Exists(_strRootDirectoryPath) == false)
                {
                    throw new Exception(String.Format("The Processing Directory could not be found.  Please check the path.  Path supplied = [{0}]", _strRootDirectoryPath));
                }

                // Get all the Excel files in the processing directory
                _lstXLSXFilesToProcess = Directory.GetFiles(_strRootDirectoryPath, "*.xlsx", SearchOption.TopDirectoryOnly).ToList();
                _objLogger.Info(String.Format("Number of Excel files (.xsls) found in the processing directory = [{0}]", _lstXLSXFilesToProcess.Count.ToString()));

            }
            catch (Exception ex)
            {
                _lstErrorMessages.Add(String.Format("There was an error while trying to get a list of Excel files in the processing directory.  {0},  Error Message = [{1}]", _strBaseLoggingString, ex.Message.ToString()));
            }

            return (_lstErrorMessages.Count == 0);
        }

    }
}
