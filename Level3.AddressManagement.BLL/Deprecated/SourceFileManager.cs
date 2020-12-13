using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;

namespace Level3.AddressManagement.BLL
{
    public class SourceFileManager
    {
        // Declare the static logger object that will be used to implement logging via log4net
        private static ILog _objLogger = LogManager.GetLogger(typeof(SourceFileManager));


        // Private Members
        BLL.SourceAddressFileManager _objSourceAddressFileManager;


        // Public Props
        private List<string> _lstErrorMessages;
        public List<string> ErrorMessages
        {
            get { return _lstErrorMessages; }
            set { _lstErrorMessages = value; }
        }


        // Constructor
        public SourceFileManager()
        {
            _lstErrorMessages = new List<string>();
        }


        // Public Methods
        public bool Load()
        {

            // Instantiate a new Source Address file manager to load the applicable filenames and paths from disk
            _objSourceAddressFileManager = new BLL.SourceAddressFileManager();

            try
            {
                // Get the  pathing info from the config file
                FileAndDirectoryLocationCalculator objFileAndDirLocationCalc = new FileAndDirectoryLocationCalculator();

                // Load the filenames from disk
                if (_objSourceAddressFileManager.LoadFilesNamesFromDisk(objFileAndDirLocationCalc.GetProcessingRoot()))
                {
                    // The file names have been loaded from disk


                }
                else
                {
                    throw new Exception(String.Format("Could not load the files from disk.  Error Messages = [{0}]", String.Join(" | ", _objSourceAddressFileManager.ErrorMessages.ToArray())));
                }

            }
            catch (Exception ex)
            {
                string strErrorMessage = String.Format("There was an error while trying to load the Excel files from disk.  Error Messages = [{0}]", ex.Message);
                _objLogger.Warn(String.Format("An error condition was detected, and should eventually be thrown as an exception.  Error Message = [{0}]", strErrorMessage));

                _lstErrorMessages.Add(strErrorMessage);
            }

            return (_lstErrorMessages.Count == 0);

        }
        public List<string> GetLoadedFileNamesToProcess()
        {
            return _objSourceAddressFileManager.XLSXFilesToProcess;
        }

    }
}
