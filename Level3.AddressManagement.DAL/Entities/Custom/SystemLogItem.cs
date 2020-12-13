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
    public class SystemLogItem : ILoadDataRow
    {
        // log4net logger declaration
        private static ILog _objLogger = LogManager.GetLogger(typeof(SystemLogItem));


        #region Properties

        // Properties
        public string DataHash { get; set; }
        public int? SystemLogItemID { get; set; }
        public string Note { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }


        #endregion  // Properties

        #region Map Database Field Methods (AssignLocalVariables) - processes a System.Data.Row to the properties of this object

        public void AssignLocalVariables(DataRow dr)
        {
            DataHash = DBSafeConvert.ToStringN(dr["DataHash"]);
            SystemLogItemID = DBSafeConvert.ToInt32N(dr["SystemLogItemID"]);
            Note = DBSafeConvert.ToStringN(dr["Note"]);
            DateCreated = DBSafeConvert.ToDateTimeUtcN(dr["DateCreated"]);
            DateUpdated = DBSafeConvert.ToDateTimeUtcN(dr["DateUpdated"]);
        }

        #endregion  // Map Database Field Methods

        public List<SystemLogItem> Get(DateTime dteDateCreated_GToEQ, out string LastError)
        {
            List<SystemLogItem> lstSystemLogItems = null;

            LastError = String.Empty;

            try
            {
                SqlCommand objQuery = new SqlCommand();
                objQuery.CommandText = "tblSystemLogItem_SelBy_DateCreated_GToEQ";
                objQuery.CommandType = CommandType.StoredProcedure;

                // Add the parameter to the Parameters collection. 
                objQuery.Parameters.Add(new SqlParameter("DateCreated", dteDateCreated_GToEQ));

                lstSystemLogItems = DBUtil.DataTableToList<SystemLogItem>(SqlServerDataAccess.Execute(objQuery));

            }
            catch (System.Data.SqlClient.SqlException exSql)
            {
                LastError = String.Concat("", exSql.Message, exSql.Number, null);
            }
            catch (Exception ex)
            {
                LastError = String.Concat("", ex.Message, null);
            }

            return lstSystemLogItems;
        }

        public bool Insert(out string LastError)
        {
            bool blnRecordsInserted = false;
            int? intID;

            LastError = String.Empty;

            try
            {
                if (this.SystemLogItemID > 0)
                {
                    LastError = "This object's identity column is already populated, which is not allowed since the field is automatically assigned by the database --> SystemLogItemID";
                    return false;
                }

                SqlCommand objQuery = new SqlCommand();
                objQuery.CommandText = "tblSystemLogItem_Insert";
                objQuery.CommandType = CommandType.StoredProcedure;

                AddCommonSQLParams(ref objQuery);

                intID = (int)SqlServerDataAccess.ExecuteScalar(objQuery);

                blnRecordsInserted = intID > 0;

                this.SystemLogItemID = intID;
            }
            catch (System.Data.SqlClient.SqlException exSql)
            {
                LastError = String.Concat("", exSql.Message, exSql.Number, null);
            }
            catch (Exception ex)
            {
                LastError = String.Concat("", ex.Message, null);
            }

            return blnRecordsInserted;
        }


        private void AddCommonSQLParams(ref SqlCommand objQuery)
        {
            objQuery.Parameters.Add(new SqlParameter("Note", (object)this.Note ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("DateCreated", (object)this.DateCreated ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("DateUpdated", (object)this.DateUpdated ?? DBNull.Value));
        }

    }
}