using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;

namespace Level3.AddressManagement.BLL
{
    public class TranslateCDWOrderAddressRecordToOrderAddress
    {
        // declare a log4net logger
        //private static ILog _objLogger = LogManager.GetLogger(typeof(TranslateCDWOrderAddressRecordToOrderAddress));

        //public static DAL.OrderAddress Translate(DAL.CDWOrderAddressRecord objCDWOrderAddressRecord)
        //{
        //    // Initialize the return fields
        //    string strCDWCustomerOrderNumber = String.Empty;
        //    string strCDWAddressOne = String.Empty;
        //    string strCDWCity = String.Empty;
        //    string strCDWState = String.Empty;
        //    string strCDWPostalCode = String.Empty;
        //    string strCDWCountry = String.Empty;
        //    string strCDWFloor = String.Empty;
        //    string strCDWRoom = String.Empty;
        //    string strCDWSuite = String.Empty;

        //    try
        //    {
        //        CDWOrderAddressRecordUtil.CalcUniqueRecordIdentifierLoggingString();

        //        DAL.OrderAddress objOrderAddress_New = new DAL.OrderAddress();

        //        strCDWCustomerOrderNumber = objCDWOrderAddressRecord.CustomerOrderNumber.Trim();
        //        strCDWAddressOne = objCDWOrderAddressRecord.Line1Address.Trim();
        //        strCDWCity = objCDWOrderAddressRecord.CityName.Trim();
        //        strCDWState = objCDWOrderAddressRecord.StateCode.Trim();
        //        strCDWPostalCode = objCDWOrderAddressRecord.PostalCode.Trim();
        //        strCDWCountry = objCDWOrderAddressRecord.CountryName.Trim();
        //        strCDWFloor = objCDWOrderAddressRecord.FloorName.Trim();
        //        strCDWRoom = objCDWOrderAddressRecord.RoomName.Trim();
        //        strCDWSuite = objCDWOrderAddressRecord.SuiteName.Trim();

        //        return objOrderAddress_New;
        //    }
        //    catch (Exception ex)
        //    {
        //        _objLogger.Error(String.Format("There was an exception thrown while trying to Cleanup the raw address fields received from CDW.  Error Message = [{0}]", ex.Message));
        //        return null;
        //    }
        }
}
