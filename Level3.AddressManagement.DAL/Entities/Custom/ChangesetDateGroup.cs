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
    public class ChangesetDateGroup : ILoadDataRow
    {
        // log4net logger declaration
        private static ILog _objLogger = LogManager.GetLogger(typeof(MigrationStatus));

        #region Properties

        // Properties
        public DateTime? MAX_OPE_LAST_MODIFY_DATE { get; set; }
        public DateTime? MAX_PL_LAST_MODIFY_DATE { get; set; }
        public DateTime? MAX_PS_LAST_MODIFY_DATE { get; set; }


        #endregion  // Properties

        #region Map Database Field Methods (AssignLocalVariables) - processes a System.Data.Row to the properties of this object

        public void AssignLocalVariables(DataRow dr)
        {
            MAX_OPE_LAST_MODIFY_DATE = DBSafeConvert.ToDateTimeUtcN(dr["MAX_OPE_LAST_MODIFY_DATE"]);
            MAX_PL_LAST_MODIFY_DATE = DBSafeConvert.ToDateTimeUtcN(dr["MAX_PL_LAST_MODIFY_DATE"]);
            MAX_PS_LAST_MODIFY_DATE = DBSafeConvert.ToDateTimeUtcN(dr["MAX_PS_LAST_MODIFY_DATE"]);
        }

        #endregion  // Map Database Field Methods

        public ChangesetDateGroup GetRecords()
        {
            string strTSQL = "SELECT MAX(OPE_LAST_MODIFY_DATE) AS MAX_OPE_LAST_MODIFY_DATE, MAX(PL_LAST_MODIFY_DATE) AS MAX_PL_LAST_MODIFY_DATE, MAX(PS_LAST_MODIFY_DATE) as MAX_PS_LAST_MODIFY_DATE FROM tblOrderAddress";


            DataTable objDataTable = Get(strTSQL);
            return DBUtil.DataTableToObject<ChangesetDateGroup>(objDataTable);
        }
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
    }
}
