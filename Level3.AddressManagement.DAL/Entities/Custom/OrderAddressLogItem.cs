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
    public class OrderAddressLogItem : ILoadDataRow
    {
        // log4net logger declaration
        private static ILog _objLogger = LogManager.GetLogger(typeof(OrderAddress));


        #region Properties

        // Properties
        public string DataHash { get; set; }
        public int? OrderAddressLogItemID { get; set; }
        public int? OrderAddressID { get; set; }
        public int? MigrationStatusID { get; set; }
        public string LogMessage { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }


        // JOINS
        public string MigrationStatusDesc { get; set; }
        public string MigrationStatusExtendedDescription { get; set; }
        

        #endregion  // Properties

        #region Map Database Field Methods (AssignLocalVariables) - processes a System.Data.Row to the properties of this object

        public void AssignLocalVariables(DataRow dr)
        {
            DataHash = DBSafeConvert.ToStringN(dr["DataHash"]);
            OrderAddressLogItemID = DBSafeConvert.ToInt32N(dr["OrderAddressLogItemID"]);
            OrderAddressID = DBSafeConvert.ToInt32N(dr["OrderAddressID"]);
            MigrationStatusID = DBSafeConvert.ToInt32N(dr["MigrationStatusID"]);
            LogMessage = DBSafeConvert.ToStringN(dr["LogMessage"]);
            DateCreated = DBSafeConvert.ToDateTimeUtcN(dr["DateCreated"]);
            DateUpdated = DBSafeConvert.ToDateTimeUtcN(dr["DateUpdated"]);

            // JOINS
            if (dr.GetValue("MigrationStatusDesc") != null)
            {
                MigrationStatusDesc = DBSafeConvert.ToStringN(dr["MigrationStatusDesc"]);
            }

            if (dr.GetValue("MigrationStatusExtendedDescription") != null)
            {
                MigrationStatusExtendedDescription = DBSafeConvert.ToStringN(dr["MigrationStatusExtendedDescription"]);
            }
        }

        #endregion  // Map Database Field Methods


        public List<OrderAddressLogItem> GetRecords(string strTSQL)
        {
            DataTable objDataTable = Get(strTSQL);
            return DBUtil.DataTableToList<OrderAddressLogItem>(objDataTable);
        }



        public List<OrderAddressLogItem> GetByFK(int intOrderAdddressID, out string LastError)
        {
            List<OrderAddressLogItem> lstOrderAddress = null;

            LastError = String.Empty;

            try
            {
                if (intOrderAdddressID == 0)
                {
                    LastError = "The value supplied for 'intOrderAdddressID' cannot be zero and must be a valid integer that corresponds to an existing record";
                    return null;
                }

                SqlCommand objQuery = new SqlCommand();
                objQuery.CommandText = "tblOrderAddressLogItem_SelByFK_H1_OrderAddressID_EQ";
                objQuery.CommandType = CommandType.StoredProcedure;

                // Add the parameter to the Parameters collection. 
                objQuery.Parameters.Add(new SqlParameter("OrderAddressID", (object)intOrderAdddressID ?? String.Empty));


                lstOrderAddress = DBUtil.DataTableToList<OrderAddressLogItem>(SqlServerDataAccess.Execute(objQuery));

            }
            catch (System.Data.SqlClient.SqlException exSql)
            {
                LastError = String.Concat("", exSql.Message, exSql.Number, null);
            }
            catch (Exception ex)
            {
                LastError = String.Concat("", ex.Message, null);
            }

            return lstOrderAddress;
        }

        public bool Insert(out string LastError)
        {
            bool blnRecordsInserted = false;
            int? intID;

            LastError = String.Empty;

            try
            {
                if (this.OrderAddressLogItemID > 0)
                {
                    LastError = "This object's identity column is already populated, which is not allowed since the field is automatically assigned by the database --> OrderAddressID";
                    return false;
                }

                SqlCommand objQuery = new SqlCommand();
                objQuery.CommandText = "tblOrderAddressLogItem_Insert";
                objQuery.CommandType = CommandType.StoredProcedure;


                AddCommonSQLParams(ref objQuery);

                intID = SqlServerDataAccess.ExecuteScalar(objQuery);

                blnRecordsInserted = intID > 0;

                this.OrderAddressLogItemID = intID;
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
            objQuery.Parameters.Add(new SqlParameter("OrderAddressID", (object)this.OrderAddressID ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("MigrationStatusID", (object)this.MigrationStatusID ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("LogMessage", (object)this.LogMessage ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("DateCreated", (object)this.DateCreated ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("DateUpdated", (object)this.DateUpdated ?? DBNull.Value));
        }
        #region Private Methods

        private DataTable Get(string strTSQL)
        {
            try
            {
                _objLogger.Debug(String.Concat("About to execute query --> [", strTSQL, "]"));

                DataTable dt = SqlServerDataAccess.Execute
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
