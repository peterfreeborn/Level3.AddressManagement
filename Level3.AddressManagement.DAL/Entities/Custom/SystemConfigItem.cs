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
    public class SystemConfigItem : ILoadDataRow
    {
        // log4net logger declaration
        private static ILog _objLogger = LogManager.GetLogger(typeof(SystemConfigItem));

        #region Properties

        // Properties
        public string DataHash { get; set; }
        public int? SystemConfigItemID { get; set; }
        public string ConfigSettingName { get; set; }
        public string ConfigSettingValue { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }


        #endregion  // Properties

        #region Map Database Field Methods (AssignLocalVariables) - processes a System.Data.Row to the properties of this object

        public void AssignLocalVariables(DataRow dr)
        {
            DataHash = DBSafeConvert.ToStringN(dr["DataHash"]);
            SystemConfigItemID = DBSafeConvert.ToInt32N(dr["SystemConfigItemID"]);
            ConfigSettingName = DBSafeConvert.ToStringN(dr["ConfigSettingName"]);
            ConfigSettingValue = DBSafeConvert.ToStringN(dr["ConfigSettingValue"]);
            DateCreated = DBSafeConvert.ToDateTimeUtcN(dr["DateCreated"]);
            DateUpdated = DBSafeConvert.ToDateTimeUtcN(dr["DateUpdated"]);
        }

        #endregion  // Map Database Field Methods

        public SystemConfigItem Get(int intSystemConfigItemID, out string LastError)
        {
            SystemConfigItem objSystemConfigItems = null;

            LastError = String.Empty;

            try
            {
                SqlCommand objQuery = new SqlCommand();
                objQuery.CommandText = "tblSystemConfigItem_SelByIDENT_SystemConfigItemID_EQ";
                objQuery.CommandType = CommandType.StoredProcedure;

                // Add the parameter to the Parameters collection. 
                objQuery.Parameters.Add(new SqlParameter("SystemConfigItemID", intSystemConfigItemID));

                objSystemConfigItems = DBUtil.DataTableToObject<SystemConfigItem>(SqlServerDataAccess.Execute(objQuery));

            }
            catch (System.Data.SqlClient.SqlException exSql)
            {
                LastError = String.Concat("", exSql.Message, exSql.Number, null);
            }
            catch (Exception ex)
            {
                LastError = String.Concat("", ex.Message, null);
            }

            return objSystemConfigItems;
        }
        public List<SystemConfigItem> GetAllSystemConfigItems(out string LastError)
        {
            List<SystemConfigItem> lstSystemConfigItems = null;

            LastError = String.Empty;

            try
            {
                SqlCommand objQuery = new SqlCommand();
                objQuery.CommandText = "tblSystemConfigItem_SelAll";
                objQuery.CommandType = CommandType.StoredProcedure;

                lstSystemConfigItems = DBUtil.DataTableToList<SystemConfigItem>(SqlServerDataAccess.Execute(objQuery));

            }
            catch (System.Data.SqlClient.SqlException exSql)
            {
                LastError = String.Concat("", exSql.Message, exSql.Number, null);
            }
            catch (Exception ex)
            {
                LastError = String.Concat("", ex.Message, null);
            }

            return lstSystemConfigItems;
        }

        public bool Insert(out string LastError)
        {
            bool blnRecordsInserted = false;
            int? intID;

            LastError = String.Empty;

            try
            {
                if (this.SystemConfigItemID > 0)
                {
                    LastError = "This object's identity column is already populated, which is not allowed since the field is automatically assigned by the database --> SystemConfigItemID";
                    return false;
                }

                SqlCommand objQuery = new SqlCommand();
                objQuery.CommandText = "tblSystemConfigItem_Insert";
                objQuery.CommandType = CommandType.StoredProcedure;

                AddCommonSQLParams(ref objQuery);

                intID = SqlServerDataAccess.ExecuteScalar(objQuery);

                blnRecordsInserted = intID > 0;

                this.SystemConfigItemID = intID;
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
        public bool UpdateOptimistic(out string LastError)
        {
            bool blnRecordUpdated = false;
            int? intNumberOfRecordsUpdated;

            LastError = String.Empty;

            try
            {
                if ((this.SystemConfigItemID == 0) || (this.SystemConfigItemID == null))
                {
                    LastError = "This object's identity column MUST be populated, so that we know which record needs update --> SystemConfigItemID";
                    return false;
                }

                SqlCommand objQuery = new SqlCommand();
                objQuery.CommandText = "tblSystemConfigItem_UpdateOptimistic";
                objQuery.CommandType = CommandType.StoredProcedure;

                // Add the parameter to the Parameters collection. 
                objQuery.Parameters.Add(new SqlParameter("DataHash", (object)this.DataHash ?? DBNull.Value));
                objQuery.Parameters.Add(new SqlParameter("SystemConfigItemID", (object)this.SystemConfigItemID ?? DBNull.Value));
                AddCommonSQLParams(ref objQuery);

                // Create parameter with Direction as Output
                SqlParameter outputIdParam = new SqlParameter("UpdateResult", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                objQuery.Parameters.Add(outputIdParam);

                intNumberOfRecordsUpdated = SqlServerDataAccess.ExecuteNonQuery(objQuery);

                int intResultValue = (int)outputIdParam.Value;
                if (intResultValue == -1)
                {
                    LastError = String.Format("The update failed.  Concurrent action violation.  Another user or process may have modified the record, as the datahash provided on the object is no longer valid.  DataHash provided = [{0}]", this.DataHash);
                }

                blnRecordUpdated = (intResultValue == 1);

            }
            catch (System.Data.SqlClient.SqlException exSql)
            {
                LastError = String.Concat("", exSql.Message, exSql.Number, null);
            }
            catch (Exception ex)
            {
                LastError = String.Concat("", ex.Message, null);
            }

            return blnRecordUpdated;
        }

        private void AddCommonSQLParams(ref SqlCommand objQuery)
        {
            objQuery.Parameters.Add(new SqlParameter("ConfigSettingName", (object)this.ConfigSettingName ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("ConfigSettingValue", (object)this.ConfigSettingValue ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("DateCreated", (object)this.DateCreated ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("DateUpdated", (object)this.DateUpdated ?? DBNull.Value));
        }

    }
}