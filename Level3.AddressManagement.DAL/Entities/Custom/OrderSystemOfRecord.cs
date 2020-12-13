using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using log4net;
using System.Data.SqlClient;

namespace Level3.AddressManagement.DAL
{
    public class OrderSystemOfRecord : ILoadDataRow
    {
        // log4net logger declaration
        private static ILog _objLogger = LogManager.GetLogger(typeof(OrderSystemOfRecord));

        #region Properties

        // Properties
        public string DataHash { get; set; }
        public int? OrderSystemOfRecordID { get; set; }
        public string OrderSystemOfRecordDesc { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }



        #endregion  // Properties

        #region Map Database Field Methods (AssignLocalVariables) - processes a System.Data.Row to the properties of this object

        public void AssignLocalVariables(DataRow dr)
        {
            DataHash = DBSafeConvert.ToStringN(dr["DataHash"]);
            OrderSystemOfRecordID = DBSafeConvert.ToInt32N(dr["OrderSystemOfRecordID"]);
            OrderSystemOfRecordDesc = DBSafeConvert.ToStringN(dr["OrderSystemOfRecordDesc"]);
            DateCreated = DBSafeConvert.ToDateTimeUtcN(dr["DateCreated"]);
            DateUpdated = DBSafeConvert.ToDateTimeUtcN(dr["DateUpdated"]);
        }

        #endregion  // Map Database Field Methods


        public List<OrderSystemOfRecord> GetAllRecords(out string LastError)
        {
            List<OrderSystemOfRecord> lstMigrationStatuses = null;

            LastError = String.Empty;

            try
            {
                SqlCommand objQuery = new SqlCommand();
                objQuery.CommandText = "tblOrderSystemOfRecord_SelAll";

                objQuery.CommandType = CommandType.StoredProcedure;
                lstMigrationStatuses = DBUtil.DataTableToList<OrderSystemOfRecord>(SqlServerDataAccess.Execute(objQuery));

            }
            catch (System.Data.SqlClient.SqlException exSql)
            {
                LastError = String.Concat("", exSql.Message, exSql.Number, null);
            }
            catch (Exception ex)
            {
                LastError = String.Concat("", ex.Message, null);
            }

            return lstMigrationStatuses;
        }


        // Constructor
        public OrderSystemOfRecord()
        {

        }
    }
}
