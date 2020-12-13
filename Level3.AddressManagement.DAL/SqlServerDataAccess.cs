using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Web;

using System.Data.SqlClient;

namespace Level3.AddressManagement.DAL
{
    public class SqlServerDataAccess
    {
        public static string ConnectionStringKey = "SQLSERVER";

        public static string GetConnectionString(string key = null)
        {
            //return System.Web.Configuration.WebConfigurationManager.ConnectionStrings[key ?? ConnectionStringKey].ConnectionString;
            return System.Configuration.ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;
        }

        public static DataTable Execute(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            DataSet ds = Execute(commandText, commandType, new string[] { "return" }, parameters);

            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }

        public static DataTable Execute(SqlCommand cmd)
        {
            DataSet ds = null;
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                cmd.Connection = conn;

                SqlDataAdapter oda = new SqlDataAdapter(cmd);
                ds = new DataSet();
                cmd.Connection.Open();

                //Execute the command
                oda.Fill(ds);

                if (ds.Tables.Count > 0)
                    return ds.Tables[0];
                else
                    return null;
            }
        }


        public static DataSet Execute(string commandText, CommandType commandType, string[] tableNames, params SqlParameter[] parameters)
        {
            DataSet ds = null;
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = commandText;
                cmd.CommandType = commandType;
                cmd.Connection = conn;

                foreach (SqlParameter p in parameters)
                {
                    cmd.Parameters.Add(p);
                }

                SqlDataAdapter oda = new SqlDataAdapter(cmd);
                ds = new DataSet();
                cmd.Connection.Open();

                //Execute the command
                oda.Fill(ds);

                for (int i = 0; i < tableNames.Length; i++)
                {
                    ds.Tables[i].TableName = tableNames[i];
                }
            }

            return ds;
        }

        /// <summary>
        /// Turn sqlparameter value into DBNull if sqlparameter type is String and value is an empty string.
        /// This makes SQLServer behave like Oracle does when saving empty strings
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static SqlParameter MakeEmptyStringNull(SqlParameter p)
        {
            if (p.DbType == DbType.String && p.Value is String && Convert.ToString(p.Value) == String.Empty)
            {
                p.Value = DBNull.Value;
            }
            return p;
        }

        public static int ExecuteNonQuery(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            int returnCode = 0;

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = commandText;
                cmd.CommandType = commandType;
                cmd.Connection = conn;

                foreach (SqlParameter p in parameters)
                {
                    cmd.Parameters.Add(MakeEmptyStringNull(p));
                }

                cmd.Connection.Open();
                SqlTransaction txn = cmd.Connection.BeginTransaction();
                cmd.Transaction = txn;

                //Execute the command
                try
                {
                    returnCode = cmd.ExecuteNonQuery();
                    txn.Commit();
                }
                catch (SqlException ex)
                {
                    txn.Rollback();
                    throw ex;
                }
            }
            return returnCode;
        }

        public static int ExecuteScalar(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            int returnCode = 0;

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = commandText;
                cmd.CommandType = commandType;
                cmd.Connection = conn;

                foreach (SqlParameter p in parameters)
                {
                    cmd.Parameters.Add(MakeEmptyStringNull(p));
                }

                cmd.Connection.Open();
                SqlTransaction txn = cmd.Connection.BeginTransaction();
                cmd.Transaction = txn;

                //Execute the command
                try
                {
                    returnCode = (int)cmd.ExecuteScalar();
                    txn.Commit();
                }
                catch (SqlException ex)
                {
                    txn.Rollback();
                    throw ex;
                }
            }
            return returnCode;
        }

        public static int ExecuteScalar(SqlCommand cmd)
        {
            int returnCode = 0;

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                cmd.Connection = conn;

                cmd.Connection.Open();
                SqlTransaction txn = cmd.Connection.BeginTransaction();
                cmd.Transaction = txn;

                //Execute the command
                try
                {
                    object temp = cmd.ExecuteScalar();

                    if (int.TryParse(temp.ToString(), out returnCode))
                    {

                    }
                    else
                    {
                        returnCode = (int)(decimal)cmd.ExecuteScalar();
                    }
                    
                    txn.Commit();
                }
                catch (SqlException ex)
                {
                    txn.Rollback();
                    throw ex;
                }
            }
            return returnCode;
        }

        public static int ExecuteNonQuery(SqlCommand cmd)
        {
            int returnCode = 0;

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                cmd.Connection = conn;

                cmd.Connection.Open();
                SqlTransaction txn = cmd.Connection.BeginTransaction();
                cmd.Transaction = txn;

                //Execute the command
                try
                {
                    returnCode = cmd.ExecuteNonQuery();
                    txn.Commit();
                }
                catch (SqlException ex)
                {
                    txn.Rollback();
                    throw ex;
                }
            }
            return returnCode;
        }



    }





    public class SqlServerDataClient : IDisposable
    {
        protected string _sqlConnectionString;
        protected string _sqlConnectionStringKey;
        protected SqlConnection _sqlConnection;
        protected SqlTransaction _sqlTrx;

        public string SqlConnectionString
        {
            get
            {
                return _sqlConnectionString ?? SqlServerDataAccess.GetConnectionString(_sqlConnectionStringKey);
            }
            set
            {
                _sqlConnectionString = value;
            }
        }

        protected void OpenConnection()
        {
            if (_sqlConnection == null || _sqlConnection.State == ConnectionState.Closed)
            {
                _sqlConnection?.Dispose();
                _sqlConnection = new SqlConnection(SqlConnectionString);
                _sqlConnection.Open();
            }
        }

        public SqlServerDataClient(Boolean BeginTrans = false)
        {
            if (BeginTrans) { BeginTransaction(); }
        }

        public SqlServerDataClient(string connectionString)
        {
            SqlConnectionString = connectionString;
        }

        public void BeginTransaction()
        {
            OpenConnection();
            _sqlTrx = _sqlConnection.BeginTransaction();
        }

        public void CommitTransaction(Boolean Dispose = false)
        {
            _sqlTrx.Commit();
            _sqlTrx = null;
            if (Dispose) { this.Dispose(); }
        }

        public void RollbackTransaction(Boolean Dispose = false)
        {
            _sqlTrx.Rollback();
            _sqlTrx = null;
            if (Dispose) { this.Dispose(); }
        }

        public DataTable Execute(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            DataSet ds = Execute(commandText, commandType, new string[] { "return" }, parameters);

            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }

        public DataSet Execute(string commandText, CommandType commandType, string[] tableNames, params SqlParameter[] parameters)
        {
            DataSet ds = null;
            OpenConnection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = commandText;
            cmd.CommandType = commandType;
            cmd.Connection = _sqlConnection;
            cmd.Transaction = _sqlTrx;

            foreach (SqlParameter p in parameters)
            {
                cmd.Parameters.Add(p);
            }

            SqlDataAdapter oda = new SqlDataAdapter(cmd);
            ds = new DataSet();

            //Execute the command
            oda.Fill(ds);

            for (int i = 0; i < tableNames.Length; i++)
            {
                ds.Tables[i].TableName = tableNames[i];
            }

            return ds;
        }

        public int ExecuteNonQuery(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            int returnCode = 0;

            OpenConnection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = commandText;
            cmd.CommandType = commandType;
            cmd.Connection = _sqlConnection;
            cmd.Transaction = _sqlTrx;

            foreach (SqlParameter p in parameters)
            {
                cmd.Parameters.Add(SqlServerDataAccess.MakeEmptyStringNull(p));
            }

            returnCode = cmd.ExecuteNonQuery();

            return returnCode;
        }

        public int ExecuteNonQuery(SqlCommand cmd)
        {
            int returnCode = 0;
            OpenConnection();
            cmd.Connection = _sqlConnection;
            cmd.Transaction = _sqlTrx;
            returnCode = cmd.ExecuteNonQuery();
            return returnCode;
        }

        public void Dispose()
        {
            _sqlConnection?.Dispose();
            _sqlTrx?.Dispose();
            _sqlConnection = null;
            _sqlTrx = null;
        }
    }

}