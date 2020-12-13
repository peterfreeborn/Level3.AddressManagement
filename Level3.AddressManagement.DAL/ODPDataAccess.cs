using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Linq;

using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

using log4net;
using System.Data.SqlClient;

namespace Level3.AddressManagement.DAL
{

    public abstract partial class ODPDataAccess
    {

        // Instantiate a log4net logger object
        private static ILog _objLogger = LogManager.GetLogger(typeof(ODPDataAccess));

        public static string GetConnectionString()
        {
            // Get the connection string from the config file
            string strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["DS_USER"].ConnectionString;
            _objLogger.Debug(String.Concat("Connection String retrieved from config file. ConnString = [", strConnString, "]"));
            return strConnString;
        }

        public static DataTable Execute(string commandText, CommandType commandType, params OracleParameter[] parameters)
        {
            DataSet ds = Execute(commandText, commandType, new string[] { "return" }, String.Empty, parameters);

            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }

        // Conn String Manipulation Override
        public static DataTable Execute(string commandText, CommandType commandType, string strOptionalDBCatalogNameOverride = "", params OracleParameter[] parameters)
        {
            DataSet ds = Execute(commandText, commandType, new string[] { "return" }, strOptionalDBCatalogNameOverride, parameters);

            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }

        public static DataSet Execute(string commandText, CommandType commandType, string[] tableNames, string strOptionalDBCatalogNameOverride = "", params OracleParameter[] parameters)
        {
            DataSet ds = null;
            string strConnString = GetConnectionString();

            using (OracleConnection conn = new OracleConnection(strConnString))
            {
                OracleCommand cmd = new OracleCommand();
                cmd.CommandText = commandText;
                cmd.CommandType = commandType;
                cmd.Connection = conn;

                foreach (OracleParameter p in parameters)
                {
                    cmd.Parameters.Add(p);
                }

                foreach (string tableName in tableNames)
                {
                    cmd.Parameters.Add(new OracleParameter(String.Format("p_{0}_cur", tableName), OracleDbType.RefCursor)).Direction = ParameterDirection.InputOutput;
                }

                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                ds = new DataSet();
                cmd.BindByName = true;
                cmd.Connection.Open();

                //Execute the command
                //If we get an ORA-04068: existing state of packages has been discarded
                //then automatically retry the command 
                int retries = 1;
                while (retries-- >= 0)
                {
                    try
                    {
                        //SetCurrentUser(conn);
                        oda.Fill(ds);
                        break;
                    }
                    catch (OracleException ex)
                    {
                        if (ex.Number != 4068) throw ex;
                    }
                }

                for (int i = 0; i < tableNames.Length; i++)
                {
                    ds.Tables[i].TableName = tableNames[i];
                }
            }

            return ds;
        }

        public static int ExecuteNonQuery(string commandText, CommandType commandType, params OracleParameter[] parameters)
        {
            return ExecuteNonQuery(commandText, commandType, 0, null, parameters);
        }

        public static int ExecuteNonQuery(string commandText, CommandType commandType, UserIdentity objUserIdentity = null, params OracleParameter[] parameters)
        {
            return ExecuteNonQuery(commandText, commandType, 0, objUserIdentity, parameters);
        }
        //BJM Added
        public static int ExecuteNonQueryNoInnerTxn(string commandText, CommandType commandType, OracleTransaction txn = null, params OracleParameter[] parameters)
        {
            return ExecuteNonQueryNoInnerTxn(commandText, commandType, 0, txn, parameters);
        }

        public static int ExecuteNonQueryNoInnerTxn(string commandText, CommandType commandType, int arrayBindCount, OracleTransaction txn = null, params OracleParameter[] parameters)
        {
            bool blnNeedsNewTxn = (txn == null);

            int returnCode = 0;

            using (OracleConnection conn = new OracleConnection(GetConnectionString()))
            {
                OracleCommand cmd = new OracleCommand();
                cmd.CommandText = commandText;
                cmd.CommandType = commandType;
                cmd.Connection = conn;
                cmd.ArrayBindCount = arrayBindCount;

                foreach (OracleParameter p in parameters)
                {
                    cmd.Parameters.Add(p);
                }

                cmd.BindByName = true;

                cmd.Connection.Open();
                if (blnNeedsNewTxn)
                {
                    txn = cmd.Connection.BeginTransaction();
                }


                //Execute the command
                //If we get an ORA-04068: existing state of packages has been discarded
                //then automatically retry the command 
                int retries = 1;
                while (retries-- >= 0)
                {
                    try
                    {
                        //SetCurrentUser(conn);
                        returnCode = cmd.ExecuteNonQuery();
                        if (blnNeedsNewTxn)
                        {
                            txn.Commit();
                        }
                        break;
                    }
                    catch (OracleException ex)
                    {
                        if (ex.Number != 4068)
                        {
                            if (blnNeedsNewTxn)
                            {
                                txn.Rollback();
                            }
                            throw ex;
                        }
                    }
                }
            }
            return returnCode;
        }

        public static int ExecuteNonQuery(string commandText, CommandType commandType, int arrayBindCount, UserIdentity objUserIdentity = null, params OracleParameter[] parameters)
        {
            int returnCode = 0;

            using (OracleConnection conn = new OracleConnection(GetConnectionString()))
            {
                OracleCommand cmd = new OracleCommand();
                cmd.CommandText = commandText;
                cmd.CommandType = commandType;
                cmd.Connection = conn;
                cmd.ArrayBindCount = arrayBindCount;

                foreach (OracleParameter p in parameters)
                {
                    cmd.Parameters.Add(p);
                }

                cmd.BindByName = true;
                cmd.Connection.Open();
                OracleTransaction txn = cmd.Connection.BeginTransaction();

                if (objUserIdentity != null)
                {
                    SetOracleUserContextForDBTransaction(cmd, objUserIdentity);
                }

                //Execute the command
                //If we get an ORA-04068: existing state of packages has been discarded
                //then automatically retry the command 
                int retries = 1;
                while (retries-- >= 0)
                {
                    try
                    {
                        returnCode = cmd.ExecuteNonQuery();
                        txn.Commit();
                        break;
                    }
                    catch (OracleException ex)
                    {
                        if (ex.Number != 4068)
                        {
                            txn.Rollback();
                            throw ex;
                        }
                    }
                }
            }
            return returnCode;
        }

        public static int ExecuteNonQuery(OracleCommand cmd)
        {
            int returnCode = 0;

            using (OracleConnection conn = new OracleConnection(GetConnectionString()))
            {
                cmd.Connection = conn;
                cmd.Connection.Open();
                OracleTransaction txn = cmd.Connection.BeginTransaction();

                //Execute the command
                //If we get an ORA-04068: existing state of packages has been discarded
                //then automatically retry the command 
                int retries = 1;
                while (retries-- >= 0)
                {
                    try
                    {
                        //SetCurrentUser(conn);
                        returnCode = cmd.ExecuteNonQuery();
                        txn.Commit();
                        break;
                    }
                    catch (OracleException ex)
                    {
                        if (ex.Number != 4068)
                        {
                            txn.Rollback();
                            throw ex;
                        }
                    }
                }
            }
            return returnCode;
        }


        /// <summary>
        /// Creates boolean columns in the given DataTable and populates them
        /// using values in the source columns.  The conversion from the source
        /// column to boolean is performed by Convert.ToBoolean(object).  Null
        /// values are preserved.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="replaceExisting"></param>
        /// <param name="columnNames"></param>
        public static void FixupBooleanColumns(DataTable dt, bool replaceExisting, params String[] columnNames)
        {
            string newColumnName = null;
            DataColumn oldCol = null, newCol = null;

            foreach (String columnName in columnNames)
            {
                //verify the existing column
                if (!dt.Columns.Contains(columnName)) continue;
                oldCol = dt.Columns[columnName];

                //create a new column and add it to the table
                newColumnName = columnName + "_BOOL";
                newCol = new DataColumn(newColumnName, typeof(bool));
                dt.Columns.Add(newCol);

                //populate the new column
                foreach (DataRow dr in dt.Rows)
                {
                    if (!dr.IsNull(oldCol))
                        dr[newCol] = Convert.ToBoolean(dr[oldCol]);
                }

                //if we're replacing, then remove the old column and rename the new one
                if (replaceExisting)
                {
                    dt.Columns.Remove(oldCol);
                    newCol.ColumnName = columnName;
                }
            }
        }

        private static void SetOracleUserContextForDBTransaction(OracleCommand cmd, UserIdentity objUserIdentity)
        {

            int returnCode = 0;

            OracleCommand cmdSetUser = new OracleCommand();
            cmdSetUser.CommandText = "APPS.l3fnd_common_api_pkg.set_user_std";
            cmdSetUser.CommandType = CommandType.StoredProcedure;
            cmdSetUser.Connection = cmd.Connection;
            cmdSetUser.ArrayBindCount = 0;


            cmdSetUser.Parameters.Add(new OracleParameter("p_user_id", objUserIdentity.UserID));
            cmdSetUser.Parameters.Add(new OracleParameter("p_responsibility_id", objUserIdentity.ResponsibilityID));
            cmdSetUser.Parameters.Add(new OracleParameter("p_operating_unit_id", null));

            //Execute the command
            //If we get an ORA-04068: existing state of packages has been discarded
            //then automatically retry the command 
            int retries = 1;
            while (retries-- >= 0)
            {
                try
                {
                    returnCode = cmdSetUser.ExecuteNonQuery();
                    break;
                }
                catch (OracleException ex)
                {
                    if (ex.Number != 4068)
                    {
                        throw ex;
                    }
                }
            }
        }
    }
}
