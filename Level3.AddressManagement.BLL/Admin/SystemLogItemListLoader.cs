using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using log4net;
using Level3.AddressManagement.DAL;

namespace Level3.AddressManagement.BLL
{
    public class SystemLogItemListLoader
    {
        // Declare the static logger object that will be used to implement logging via log4net
        private static ILog _objLogger = LogManager.GetLogger(typeof(SystemLogItemListLoader));

        private string _strBaseLoggingMessage;
        private bool _blnIsLoaded;
        private DateTime? _dteStartDate;
        private DateTime? _dteEndDate;


        // Public Properties
        private List<string> _lstErrorMessages;
        public List<string> ErrorMessages
        {
            get { return _lstErrorMessages; }
            set { _lstErrorMessages = value; }
        }
        private List<SystemLogItem> _lstSystemLogItems;

        public List<SystemLogItem> SystemLogItems
        {
            get { return _lstSystemLogItems; }
            set { _lstSystemLogItems = value; }
        }


        // Constructor
        public SystemLogItemListLoader()
        {
            _lstErrorMessages = new List<string>();
            _lstSystemLogItems = new List<SystemLogItem>();
        }


        public bool Load()
        {
            try
            {
                LoadApplicableSystemLogItemsFromDB();
            }
            catch (Exception ex)
            {
                _lstErrorMessages.Add(String.Concat("There was an error while trying to LOAD the list of system log messages from the database.  ", _strBaseLoggingMessage, ",  Error Message: [", ex.Message, "]"));
            }

            _blnIsLoaded = _lstErrorMessages.Count() == 0;

            // return 
            return _blnIsLoaded;
        }
        private void LoadApplicableSystemLogItemsFromDB()
        {
            string strLastError = String.Empty;

            _lstSystemLogItems = new SystemLogItem().Get(DateTime.Now.Date.AddDays(-14), out strLastError);
            _lstSystemLogItems = _lstSystemLogItems?.OrderBy(i => i.DateCreated)?.Reverse()?.ToList();

            _objLogger.Info(String.Concat("Number of SystemLogItems retrieved from the database = [", _lstSystemLogItems.Count, "]"));
        }
        private void EnforceLoadSuccess()
        {
            if (_blnIsLoaded == false)
            {
                throw new Exception("The load method was not call or did not succeed, and so subsequent and dependant methods in this class cannot be invoked.");
            }
        }

    }
}

