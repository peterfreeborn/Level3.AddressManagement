using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using log4net;

namespace Level3.AddressManagement.DAL
{
    public class CDWOrderAddressRecord : ILoadDataRow
    {
        // log4net logger declaration
        private static ILog _objLogger = LogManager.GetLogger(typeof(CDWOrderAddressRecord));

        #region Properties

        // Properties
        public string CustomerOrderNumber { get; set; }
        public string ServiceOrderNumber { get; set; }
        public string ServiceOrderStatusCode { get; set; }
        public string DataWarehouseSourceSystemCode { get; set; }
        public string CLIICode { get; set; }
        public string Line1Address { get; set; }
        public string CityName { get; set; }
        public string StateCode { get; set; }
        public string PostalCode { get; set; }
        public string CountryName { get; set; }
        public string FloorName { get; set; }
        public string RoomName { get; set; }
        public string SuiteName { get; set; }
        public string BuildingName { get; set; }

        public DateTime? FIRST_ORDER_CREATE_DT { get; set; }
        public DateTime? OPE_LAST_MODIFY_DATE { get; set; }
        public DateTime? PL_LAST_MODIFY_DATE { get; set; }
        public DateTime? PS_LAST_MODIFY_DATE { get; set; }

        #endregion  // Properties

        #region Map Database Field Methods (AssignLocalVariables) - processes a System.Data.Row to the properties of this object

        public void AssignLocalVariables(DataRow dr)
        {
            CustomerOrderNumber = DBSafeConvert.ToStringN(dr["CUST_ORDER_NBR"]);
            ServiceOrderNumber = DBSafeConvert.ToStringN(dr["SERV_ORDER_NBR"]);
            ServiceOrderStatusCode = DBSafeConvert.ToStringN(dr["SERV_ORDER_STATUS_CD"]);
            DataWarehouseSourceSystemCode = DBSafeConvert.ToStringN(dr["DW_SOURCE_SYSTEM_CD"]);
            CLIICode = DBSafeConvert.ToStringN(dr["CLLI_CD"]);
            Line1Address = DBSafeConvert.ToStringN(dr["LINE1_ADDR"]);
            CityName = DBSafeConvert.ToStringN(dr["CITY_NAME"]);
            StateCode = DBSafeConvert.ToStringN(dr["STATE_CD"]);
            PostalCode = DBSafeConvert.ToStringN(dr["POSTAL_SHORT_CD"]);
            CountryName = DBSafeConvert.ToStringN(dr["COUNTRY_ISO_3_CD"]);
            FloorName = DBSafeConvert.ToStringN(dr["FLOOR_NAME"]);
            RoomName = DBSafeConvert.ToStringN(dr["ROOM_NAME"]);
            SuiteName = DBSafeConvert.ToStringN(dr["SUITE_NAME"]);

            FIRST_ORDER_CREATE_DT = DBSafeConvert.ToDateTimeUtcN(dr["FIRST_ORDER_CREATE_DT"]);
            OPE_LAST_MODIFY_DATE = DBSafeConvert.ToDateTimeUtcN(dr["OPE_LAST_MODIFY_DATE"]);
            PL_LAST_MODIFY_DATE = DBSafeConvert.ToDateTimeUtcN(dr["PL_LAST_MODIFY_DATE"]);
            PS_LAST_MODIFY_DATE = DBSafeConvert.ToDateTimeUtcN(dr["PS_LAST_MODIFY_DATE"]);
        }

        #endregion  // Map Database Field Methods

        public List<CDWOrderAddressRecord> GetRecords(string strTSQL)
        {
            DataTable objDataTable = Get(strTSQL);
            return DBUtil.DataTableToList<CDWOrderAddressRecord>(objDataTable);
        }

        #region Private Methods

        private DataTable Get(string strTSQL)
        {
            try
            {
                _objLogger.Debug(String.Concat("About to execute query --> [", strTSQL, "]"));

                DataTable dt = ODPDataAccess.Execute
                                (strTSQL
                                , CommandType.Text
                                );
                return dt;
            }
            catch (Exception ex)
            {
                _objLogger.Warn(String.Concat("Query Execution Failed --> [", strTSQL, "]"));
                throw new ArgumentException(String.Format("DataAccess error: {0}", ex.Message));
            }
        }


        #endregion

    }



}
