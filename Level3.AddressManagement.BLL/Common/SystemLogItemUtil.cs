using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;
using Level3.AddressManagement.DAL;

namespace Level3.AddressManagement.BLL
{
   public class SystemLogItemUtil
    {
        // Declare the static logger object that will be used to implement logging via log4net
        private static ILog _objLogger = LogManager.GetLogger(typeof(SystemLogItemUtil));

        public static bool InsertLogItem(string strNote, string strActionDescription)
        {
            string strLastError = String.Empty;

            try
            {
                // Trunc the string if necessary
                int intNumOfCharsAllowedInDB = 2000;
                if (strNote.Length > intNumOfCharsAllowedInDB)
                {
                    strNote = String.Concat(strNote.Substring(0, 2000), "..."); // DB field can't support more than 4000 chars
                }

                DateTime dteNow = DateTime.Now;

                // Log the Changeset Sync
                SystemLogItem objSystemLogItem = new SystemLogItem();
                objSystemLogItem.Note = strNote;
                objSystemLogItem.DateCreated = dteNow;
                objSystemLogItem.DateUpdated = dteNow;
                if (objSystemLogItem.Insert(out strLastError) == false)
                {
                    throw new Exception(String.Concat(strActionDescription, "  The log message could NOT be inserted into the database.  Would-be log message = [", objSystemLogItem.Note, "]"));
                }

                return true;
            }
            catch (Exception ex)
            {
                _objLogger.Error(String.Concat("There was an error while trying to insert the system log item.  Error Message = [", ex.Message, "]"));
                return false;
            }
        }
    }
}
