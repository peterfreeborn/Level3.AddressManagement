using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;

namespace Level3.AddressManagement.BLL
{
    class OrderAddressUtil
    {
        // log4net logger declaration
        private static ILog _objLogger = LogManager.GetLogger(typeof(OrderAddressUtil));

        public static string CalcUniqueRecordIdentifierLoggingString(DAL.OrderAddress objOrderAddressRecord)
        {
            return String.Format("Order Address ->  OrderAddressID = [{0}],  System of Record = [{1}], OrderAddressType = [{2}], CustomerOrderNumber = [{3}], Line1Address = [{4}], CityName = [{5}], StateCode = [{6}], PostalCode = [{7}],  CountryName = [{8}], Floor = [{9}], Room = [{8}], Suite = [{10}]", objOrderAddressRecord.OrderAddressID.ToString(), (Model.OrderSystemOfRecords)objOrderAddressRecord.OrderSystemOfRecordID, (Model.OrderAddressTypes)objOrderAddressRecord.OrderAddressTypeID, objOrderAddressRecord.CDWCustomerOrderNumber, objOrderAddressRecord.CDWAddressOne, objOrderAddressRecord.CDWCity, objOrderAddressRecord.CDWState, objOrderAddressRecord.CDWPostalCode, objOrderAddressRecord.CDWCountry, objOrderAddressRecord.CDWFloor, objOrderAddressRecord.CDWRoom, objOrderAddressRecord.CDWSuite);
        }

    }
}
