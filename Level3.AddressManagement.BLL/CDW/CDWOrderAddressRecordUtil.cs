using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Level3.AddressManagement.DAL;
using log4net;

namespace Level3.AddressManagement.BLL
{
    public class CDWOrderAddressRecordUtil
    {

        // log4net logger declaration
        private static ILog _objLogger = LogManager.GetLogger(typeof(CDWOrderAddressRecordUtil));

        public static string CalcUniqueRecordIdentifierLoggingString(DAL.CDWOrderAddressRecord objCDWOrderAddressRecord)
        {

            return String.Format("CWD Order Address -> DataWarehouseSourceSystemCode = [{0}], CustomerOrderNumber = [{1}], Line1Address = [{2}], CityName = [{3}], StateCode = [{4}], PostalCode = [{5}],  CountryName = [{6}]", objCDWOrderAddressRecord.DataWarehouseSourceSystemCode, objCDWOrderAddressRecord.CustomerOrderNumber, objCDWOrderAddressRecord.Line1Address, objCDWOrderAddressRecord.CityName, objCDWOrderAddressRecord.StateCode, objCDWOrderAddressRecord.PostalCode, objCDWOrderAddressRecord.CountryName);
        }



        public static bool CleanupRawAddressFields(DAL.CDWOrderAddressRecord objCDWOrderAddressRecord, out string strCDWCustomerOrderNumber, out string strCDWAddressOne, out string strCDWCity, out string strCDWState, out string strCDWPostalCode, out string strCDWCountry, out string strCDWFloor, out string strCDWRoom, out string strCDWSuite)
        {
            // Initialize the return fields
            strCDWCustomerOrderNumber = String.Empty;
            strCDWAddressOne = String.Empty;
            strCDWCity = String.Empty;
            strCDWState = String.Empty;
            strCDWPostalCode = String.Empty;
            strCDWCountry = String.Empty;
            strCDWFloor = String.Empty;
            strCDWRoom = String.Empty;
            strCDWSuite = String.Empty;

            try
            {
                strCDWCustomerOrderNumber = objCDWOrderAddressRecord.CustomerOrderNumber?.Trim();
                strCDWAddressOne = objCDWOrderAddressRecord.Line1Address?.Trim();
                strCDWCity = objCDWOrderAddressRecord.CityName?.Trim();
                strCDWState = objCDWOrderAddressRecord.StateCode?.Trim();
                strCDWPostalCode = objCDWOrderAddressRecord.PostalCode?.Trim();
                strCDWCountry = objCDWOrderAddressRecord.CountryName?.Trim();
                strCDWFloor = objCDWOrderAddressRecord.FloorName?.Trim();
                strCDWRoom = objCDWOrderAddressRecord.RoomName?.Trim();
                strCDWSuite = objCDWOrderAddressRecord.SuiteName?.Trim();

                return true;
            }
            catch (Exception ex)
            {
                _objLogger.Error(String.Format("There was an exception thrown while trying to Cleanup the raw address fields received from CDW.  Error Message = [{0}]", ex.Message));
                return false;
            }

        }

        public static Model.OrderAddressTypes TranslateRawAddressFieldsToOrderAddressType(string strCDWFloor, string strCDWRoom, string strCDWSuite)
        {
            bool blnHasFloor = !(String.IsNullOrEmpty(strCDWFloor?.Trim()));
            bool blnHasRoom = !(String.IsNullOrEmpty(strCDWRoom?.Trim()));
            bool blnHasSuite = !(String.IsNullOrEmpty(strCDWSuite?.Trim()));

            bool blnHasAny = (blnHasFloor) || (blnHasRoom) || (blnHasSuite);

            if (blnHasAny)
            {
                return Model.OrderAddressTypes.Service_Location;
            }
            else
            {
                return Model.OrderAddressTypes.Site;
            }

        }
    }
}
