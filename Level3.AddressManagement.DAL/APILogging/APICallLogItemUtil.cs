using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;

namespace Level3.AddressManagement.DAL
{
    public static class APICallLogItemUtil
    {
        // Declare the static logger object that will be used to implement logging via log4net
        private static ILog _objLogger = LogManager.GetLogger(typeof(APICallLogItemUtil));


        public static void LogAPICallToDB(int intOrderAdressID, string strHost, string strFullRoute, System.Net.HttpStatusCode objHttpResponseCode, TimeSpan objTimeSpan)
        {

            try
            {
                DateTime dteNow = DateTime.Now;

                APICallLogItem objAPICallLogItem = new APICallLogItem();
                objAPICallLogItem.OrderAddressID = intOrderAdressID;
                objAPICallLogItem.Host = strHost;
                objAPICallLogItem.FullUrl = strFullRoute;
                objAPICallLogItem.ResponseCode = objHttpResponseCode.ToString();
                objAPICallLogItem.RunTimeHumanReadable = Model.StopwatchUtil.GetHumanReadableTimeElapsedString(objTimeSpan);
                objAPICallLogItem.RunTimeInTicksString = objTimeSpan.Ticks.ToString();
                objAPICallLogItem.DateUpdated = dteNow;
                objAPICallLogItem.DateCreated = dteNow;

                string strLastError = String.Empty;
                if (objAPICallLogItem.Insert(out strLastError) == false)
                {
                    throw new Exception(String.Format("The API Call Log Item failed to insert, as the identity/PK value returned was not valid.  OrderAddressID = [{0}], Host = [{1}], Route = [{2}], Http Status Code = [{3}], Error Message = [{4}]", intOrderAdressID, strHost, strFullRoute, objHttpResponseCode.ToString(), strLastError));
                }

            }
            catch (Exception ex)
            {
                // Don't abort processing since this is a post-action log... just log the error
                _objLogger.Error(String.Format("There was an error while trying to insert the API Call Log Item to the DB.  This error will be eaten, so that processing can continue. Error Message = [{0}]", ex.Message));
            }
        }


    }
}

