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
    public class MigrationStatus : ILoadDataRow
    {
        // log4net logger declaration
        private static ILog _objLogger = LogManager.GetLogger(typeof(MigrationStatus));

        #region Properties

        // Properties
        public string DataHash { get; set; }
        public int? MigrationStatusID { get; set; }
        public string MigrationStatusDesc { get; set; }
        public string MigrationStatusExtendedDescription { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }


        #endregion  // Properties

        #region Map Database Field Methods (AssignLocalVariables) - processes a System.Data.Row to the properties of this object

        public void AssignLocalVariables(DataRow dr)
        {
            DataHash = DBSafeConvert.ToStringN(dr["DataHash"]);
            MigrationStatusID = DBSafeConvert.ToInt32N(dr["MigrationStatusID"]);
            MigrationStatusDesc = DBSafeConvert.ToStringN(dr["MigrationStatusDesc"]);
            MigrationStatusExtendedDescription = DBSafeConvert.ToStringN(dr["MigrationStatusExtendedDescription"]);
            DateCreated = DBSafeConvert.ToDateTimeUtcN(dr["DateCreated"]);
            DateUpdated = DBSafeConvert.ToDateTimeUtcN(dr["DateUpdated"]);
        }

        #endregion  // Map Database Field Methods

        public MigrationStatus GetByPK(int intMigrationStatusID, out string LastError)
        {
            MigrationStatus objMigrationStatus = null;

            LastError = String.Empty;

            try
            {
                if (intMigrationStatusID == 0)
                {
                    LastError = "The identity value supplied for the records PK cannot be zero and must be a valid integer --> MigrationStatusID";
                    return null;
                }

                SqlCommand objQuery = new SqlCommand();
                objQuery.CommandText = "tblMigrationStatus_SelByPK_MigrationStatusID_EQ";
                objQuery.CommandType = CommandType.StoredProcedure;

                // Add the parameter to the Parameters collection. 
                objQuery.Parameters.Add(new SqlParameter("MigrationStatusID", intMigrationStatusID));

                objMigrationStatus = DBUtil.DataTableToObject<MigrationStatus>(SqlServerDataAccess.Execute(objQuery));

            }
            catch (System.Data.SqlClient.SqlException exSql)
            {
                LastError = String.Concat("", exSql.Message, exSql.Number, null);
            }
            catch (Exception ex)
            {
                LastError = String.Concat("", ex.Message, null);
            }

            return objMigrationStatus;
        }

        public List<MigrationStatus> GetAllRecords(out string LastError)
        {
            List<MigrationStatus> lstMigrationStatuses = null;

            LastError = String.Empty;

            try
            {
                SqlCommand objQuery = new SqlCommand();
                objQuery.CommandText = "tblMigrationStatus_SelAll";

                objQuery.CommandType = CommandType.StoredProcedure;
                lstMigrationStatuses = DBUtil.DataTableToList<MigrationStatus>(SqlServerDataAccess.Execute(objQuery));

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


        public MigrationStatus()
        {

        }
    }
}
