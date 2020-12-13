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
    public class APICallLogItem : ILoadDataRow
    {
        // log4net logger declaration
        private static ILog _objLogger = LogManager.GetLogger(typeof(APICallLogItem));


        #region Properties

        // Properties
        public string DataHash { get; set; }
        public int? APICallLogItemID { get; set; }
        public int? OrderAddressID { get; set; }
        public string Host { get; set; }
        public string FullUrl { get; set; }
        public string ResponseCode { get; set; }
        public string RunTimeHumanReadable { get; set; }
        public string RunTimeInTicksString { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }



        #endregion  // Properties

        #region Map Database Field Methods (AssignLocalVariables) - processes a System.Data.Row to the properties of this object

        public void AssignLocalVariables(DataRow dr)
        {
            DataHash = DBSafeConvert.ToStringN(dr["DataHash"]);
            APICallLogItemID = DBSafeConvert.ToInt32N(dr["APICallLogItemID"]);
            OrderAddressID = DBSafeConvert.ToInt32N(dr["OrderAddressID"]);
            Host = DBSafeConvert.ToStringN(dr["Host"]);
            FullUrl = DBSafeConvert.ToStringN(dr["FullUrl"]);
            ResponseCode = DBSafeConvert.ToStringN(dr["ResponseCode"]);
            RunTimeHumanReadable = DBSafeConvert.ToStringN(dr["RunTimeHumanReadable"]);
            RunTimeInTicksString = DBSafeConvert.ToStringN(dr["RunTimeInTicksString"]);
            DateCreated = DBSafeConvert.ToDateTimeUtcN(dr["DateCreated"]);
            DateUpdated = DBSafeConvert.ToDateTimeUtcN(dr["DateUpdated"]);
        }

        #endregion  // Map Database Field Methods

        public List<APICallLogItem> Get(DateTime dteDateCreated_GToEQ, out string LastError)
        {
            List<APICallLogItem> lstAPICallLogItems = null;

            LastError = String.Empty;

            try
            {
                SqlCommand objQuery = new SqlCommand();
                objQuery.CommandText = "tblAPICallLogItem_SelBy_DateCreated_GToEQ";
                objQuery.CommandType = CommandType.StoredProcedure;

                // Add the parameter to the Parameters collection. 
                objQuery.Parameters.Add(new SqlParameter("DateCreated", dteDateCreated_GToEQ));

                lstAPICallLogItems = DBUtil.DataTableToList<APICallLogItem>(SqlServerDataAccess.Execute(objQuery));

            }
            catch (System.Data.SqlClient.SqlException exSql)
            {
                LastError = String.Concat("", exSql.Message, exSql.Number, null);
            }
            catch (Exception ex)
            {
                LastError = String.Concat("", ex.Message, null);
            }

            return lstAPICallLogItems;
        }
        public List<APICallLogItem> GetByFK(int intOrderAdddressID, out string LastError)
        {
            List<APICallLogItem> lstOrderAddress = null;

            LastError = String.Empty;

            try
            {
                if (intOrderAdddressID == 0)
                {
                    LastError = "The value supplied for 'intOrderAdddressID' cannot be zero and must be a valid integer that corresponds to an existing record";
                    return null;
                }

                SqlCommand objQuery = new SqlCommand();
                objQuery.CommandText = "tblAPICallLogItem_SelByFK_OrderAddressID_EQ";
                objQuery.CommandType = CommandType.StoredProcedure;

                // Add the parameter to the Parameters collection. 
                objQuery.Parameters.Add(new SqlParameter("OrderAddressID", (object)intOrderAdddressID ?? String.Empty));


                lstOrderAddress = DBUtil.DataTableToList<APICallLogItem>(SqlServerDataAccess.Execute(objQuery));

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
                if (this.APICallLogItemID > 0)
                {
                    LastError = "This object's identity column is already populated, which is not allowed since the field is automatically assigned by the database --> APICallLogItemID";
                    return false;
                }

                SqlCommand objQuery = new SqlCommand();
                objQuery.CommandText = "tblAPICallLogItem_Insert";
                objQuery.CommandType = CommandType.StoredProcedure;

                AddCommonSQLParams(ref objQuery);

                intID = (int)SqlServerDataAccess.ExecuteScalar(objQuery);

                blnRecordsInserted = intID > 0;

                this.APICallLogItemID = intID;
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
            objQuery.Parameters.Add(new SqlParameter("Host", (object)this.Host ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("FullUrl", (object)this.FullUrl ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("ResponseCode", (object)this.ResponseCode ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("RunTimeHumanReadable", (object)this.RunTimeHumanReadable ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("RunTimeInTicksString", (object)this.RunTimeInTicksString ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("DateCreated", (object)this.DateCreated ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("DateUpdated", (object)this.DateUpdated ?? DBNull.Value));
        }

    }
}