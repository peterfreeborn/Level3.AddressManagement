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
    public class OrderAddress : ILoadDataRow
    {
        // log4net logger declaration
        private static ILog _objLogger = LogManager.GetLogger(typeof(OrderAddress));


        #region Properties

        // Properties
        public string DataHash { get; set; }
        public int? OrderAddressID { get; set; }
        public int? OrderAddressTypeID { get; set; }
        public int? MigrationStatusID { get; set; }
        public int? OrderSystemOfRecordID { get; set; }
        public string CDWCustomerOrderNumber { get; set; }
        public string CDWAddressOne { get; set; }
        public string CDWCity { get; set; }
        public string CDWState { get; set; }
        public string CDWPostalCode { get; set; }
        public string CDWCountry { get; set; }
        public string CDWFloor { get; set; }
        public string CDWRoom { get; set; }
        public string CDWSuite { get; set; }
        public string CDWCLII { get; set; }
        public bool? ValidCLII { get; set; }
        public int? NumberOfFailedGLMSiteCalls { get; set; }
        public bool? ExistsInGLMAsSite { get; set; }
        public string GLMPLNumber { get; set; }
        public int? NumberOfFailedGLMSiteCodeExistenceCalls { get; set; }
        public int? NumberOfFailedGLMSiteCodeCreationCalls { get; set; }
        public string GLMSiteCode { get; set; }
        public bool? HasGLMSiteCode { get; set; }
        public int? NumberOfFailedSAPSiteAddressSearchCalls { get; set; }
        public int? NumberOfFailedSAPSiteAddressImportCalls { get; set; }
        public bool? ExistsInSAPAsSiteAddress { get; set; }
        public int? NumberOfRecordsInSAPWithPL { get; set; }
        public int? NumberOfFailedGLMServiceLocationSearchCalls { get; set; }
        public int? NumberOfFailedGLMServiceLocationCreationCalls { get; set; }
        public string GLMSLNumber { get; set; }
        public bool? ExistsInGLMAsServiceLocation { get; set; }
        public int? NumberOfFailedGLMSCodeExistenceCalls { get; set; }
        public int? NumberOfFailedGLMSCodeCreationCalls { get; set; }
        public string GLMSCode { get; set; }
        public bool? HasGLMSCode { get; set; }
        public int? NumberOfFailedSAPServiceLocationAddressSearchCalls { get; set; }
        public int? NumberOfFailedSAPServiceLocationAddressImportCalls { get; set; }
        public bool? ExistsInSAPAsServiceLocationAddress { get; set; }
        public int? NumberOfRecordsInSAPWithSL { get; set; }
        public DateTime? SourceCreationDate { get; set; }
        public DateTime? SourceLastModifyDate { get; set; }
        public DateTime? DateTimeOfLastMigrationStatusUpdate { get; set; }
        public DateTime? DateTimeOfLastDupDetection { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string ServiceOrderNumber { get; set; }
        public DateTime? FIRST_ORDER_CREATE_DT { get; set; }
        public DateTime? OPE_LAST_MODIFY_DATE { get; set; }
        public DateTime? PL_LAST_MODIFY_DATE { get; set; }
        public DateTime? PS_LAST_MODIFY_DATE { get; set; }
        public string TotalProcessingTimeInTickString { get; set; }
        public string TotalProcessingTimeAsHumanReadable { get; set; }

        // JOINS
        public string MigrationStatusDesc { get; set; }
        public string OrderAddressTypeDesc { get; set; }
        public string OrderSystemOfRecordDesc { get; set; }



        #endregion  // Properties

        #region Map Database Field Methods (AssignLocalVariables) - processes a System.Data.Row to the properties of this object

        public void AssignLocalVariables(DataRow dr)
        {
            DataHash = DBSafeConvert.ToStringN(dr["DataHash"]);
            OrderAddressID = DBSafeConvert.ToInt32N(dr["OrderAddressID"]);
            OrderAddressTypeID = DBSafeConvert.ToInt32N(dr["OrderAddressTypeID"]);
            MigrationStatusID = DBSafeConvert.ToInt32N(dr["MigrationStatusID"]);
            OrderSystemOfRecordID = DBSafeConvert.ToInt32N(dr["OrderSystemOfRecordID"]);
            CDWCustomerOrderNumber = DBSafeConvert.ToStringN(dr["CDWCustomerOrderNumber"]);
            CDWAddressOne = DBSafeConvert.ToStringN(dr["CDWAddressOne"]);
            CDWCity = DBSafeConvert.ToStringN(dr["CDWCity"]);
            CDWState = DBSafeConvert.ToStringN(dr["CDWState"]);
            CDWPostalCode = DBSafeConvert.ToStringN(dr["CDWPostalCode"]);
            CDWCountry = DBSafeConvert.ToStringN(dr["CDWCountry"]);
            CDWFloor = DBSafeConvert.ToStringN(dr["CDWFloor"]);
            CDWRoom = DBSafeConvert.ToStringN(dr["CDWRoom"]);
            CDWSuite = DBSafeConvert.ToStringN(dr["CDWSuite"]);
            CDWCLII = DBSafeConvert.ToStringN(dr["CDWCLII"]);
            ValidCLII = DBSafeConvert.ToBooleanN(dr["ValidCLII"]);
            NumberOfFailedGLMSiteCalls = DBSafeConvert.ToInt32N(dr["NumberOfFailedGLMSiteCalls"]);
            ExistsInGLMAsSite = DBSafeConvert.ToBooleanN(dr["ExistsInGLMAsSite"]);
            GLMPLNumber = DBSafeConvert.ToStringN(dr["GLMPLNumber"]);
            NumberOfFailedGLMSiteCodeExistenceCalls = DBSafeConvert.ToInt32N(dr["NumberOfFailedGLMSiteCodeExistenceCalls"]);
            NumberOfFailedGLMSiteCodeCreationCalls = DBSafeConvert.ToInt32N(dr["NumberOfFailedGLMSiteCodeCreationCalls"]);
            GLMSiteCode = DBSafeConvert.ToStringN(dr["GLMSiteCode"]);
            HasGLMSiteCode = DBSafeConvert.ToBooleanN(dr["HasGLMSiteCode"]);
            NumberOfFailedSAPSiteAddressSearchCalls = DBSafeConvert.ToInt32N(dr["NumberOfFailedSAPSiteAddressSearchCalls"]);
            NumberOfFailedSAPSiteAddressImportCalls = DBSafeConvert.ToInt32N(dr["NumberOfFailedSAPSiteAddressImportCalls"]);
            ExistsInSAPAsSiteAddress = DBSafeConvert.ToBooleanN(dr["ExistsInSAPAsSiteAddress"]);
            NumberOfRecordsInSAPWithPL = DBSafeConvert.ToInt32N(dr["NumberOfRecordsInSAPWithPL"]);
            NumberOfFailedGLMServiceLocationSearchCalls = DBSafeConvert.ToInt32N(dr["NumberOfFailedGLMServiceLocationSearchCalls"]);
            NumberOfFailedGLMServiceLocationCreationCalls = DBSafeConvert.ToInt32N(dr["NumberOfFailedGLMServiceLocationCreationCalls"]);
            GLMSLNumber = DBSafeConvert.ToStringN(dr["GLMSLNumber"]);
            ExistsInGLMAsServiceLocation = DBSafeConvert.ToBooleanN(dr["ExistsInGLMAsServiceLocation"]);
            NumberOfFailedGLMSCodeExistenceCalls = DBSafeConvert.ToInt32N(dr["NumberOfFailedGLMSCodeExistenceCalls"]);
            NumberOfFailedGLMSCodeCreationCalls = DBSafeConvert.ToInt32N(dr["NumberOfFailedGLMSCodeCreationCalls"]);
            GLMSCode = DBSafeConvert.ToStringN(dr["GLMSCode"]);
            HasGLMSCode = DBSafeConvert.ToBooleanN(dr["HasGLMSCode"]);
            NumberOfFailedSAPServiceLocationAddressSearchCalls = DBSafeConvert.ToInt32N(dr["NumberOfFailedSAPServiceLocationAddressSearchCalls"]);
            NumberOfFailedSAPServiceLocationAddressImportCalls = DBSafeConvert.ToInt32N(dr["NumberOfFailedSAPServiceLocationAddressImportCalls"]);
            ExistsInSAPAsServiceLocationAddress = DBSafeConvert.ToBooleanN(dr["ExistsInSAPAsServiceLocationAddress"]);
            NumberOfRecordsInSAPWithSL = DBSafeConvert.ToInt32N(dr["NumberOfRecordsInSAPWithSL"]);
            SourceCreationDate = DBSafeConvert.ToDateTimeUtcN(dr["SourceCreationDate"]);
            SourceLastModifyDate = DBSafeConvert.ToDateTimeUtcN(dr["SourceLastModifyDate"]);
            DateTimeOfLastMigrationStatusUpdate = DBSafeConvert.ToDateTimeUtcN(dr["DateTimeOfLastMigrationStatusUpdate"]);
            DateTimeOfLastDupDetection = DBSafeConvert.ToDateTimeUtcN(dr["DateTimeOfLastDupDetection"]);
            DateCreated = DBSafeConvert.ToDateTimeUtcN(dr["DateCreated"]);
            DateUpdated = DBSafeConvert.ToDateTimeUtcN(dr["DateUpdated"]);
            ServiceOrderNumber = DBSafeConvert.ToStringN(dr["ServiceOrderNumber"]);
            FIRST_ORDER_CREATE_DT = DBSafeConvert.ToDateTimeUtcN(dr["FIRST_ORDER_CREATE_DT"]);
            OPE_LAST_MODIFY_DATE = DBSafeConvert.ToDateTimeUtcN(dr["OPE_LAST_MODIFY_DATE"]);
            PL_LAST_MODIFY_DATE = DBSafeConvert.ToDateTimeUtcN(dr["PL_LAST_MODIFY_DATE"]);
            PS_LAST_MODIFY_DATE = DBSafeConvert.ToDateTimeUtcN(dr["PS_LAST_MODIFY_DATE"]);
            TotalProcessingTimeInTickString = DBSafeConvert.ToStringN(dr["TotalProcessingTimeInTickString"]);
            TotalProcessingTimeAsHumanReadable = DBSafeConvert.ToStringN(dr["TotalProcessingTimeAsHumanReadable"]);

            // JOINS
            if (dr.GetValue("MigrationStatusDesc") != null)
            {
                MigrationStatusDesc = DBSafeConvert.ToStringN(dr["MigrationStatusDesc"]);
            }

            if (dr.GetValue("OrderAddressTypeDesc") != null)
            {
                OrderAddressTypeDesc = DBSafeConvert.ToStringN(dr["OrderAddressTypeDesc"]);
            }

            if (dr.GetValue("OrderSystemOfRecordDesc") != null)
            {
                OrderSystemOfRecordDesc = DBSafeConvert.ToStringN(dr["OrderSystemOfRecordDesc"]);
            }
        }

        #endregion  // Map Database Field Methods



        public List<OrderAddress> SearchPaymentWithCount(string strWhereClause, string strOrderByClause, int? intPageNumber, int? intPageSize, out int intCount)
        {
            intCount = 0;
            string strErrorMessage = String.Empty;

            List<OrderAddress> lstPagedResults = Search(strWhereClause, strOrderByClause, out strErrorMessage, intPageNumber, intPageSize);

            intCount = Count(strWhereClause, out strErrorMessage);

            return lstPagedResults;
        }


      
        public List<OrderAddress> SearchPayment(string strWhereClause, string strOrderByClause, int intPageNumber, int intPageSize)
        {
            string strErrorMessage = String.Empty;

            return Search(strWhereClause, strOrderByClause, out strErrorMessage, intPageNumber, intPageSize);
        }

        public List<OrderAddress> GetRecords(string strTSQL)
        {
            DataTable objDataTable = Get(strTSQL);
            return DBUtil.DataTableToList<OrderAddress>(objDataTable);
        }

        public List<OrderAddress> GetAllRecords()
        {
            List<OrderAddress> lstOrderAddress = null;
            
            string LastError = String.Empty;

            try
            {
                SqlCommand objQuery = new SqlCommand();
                objQuery.CommandText = "tblOrderAddress_SelAll";
                objQuery.CommandType = CommandType.StoredProcedure;

                lstOrderAddress = DBUtil.DataTableToList<OrderAddress>(SqlServerDataAccess.Execute(objQuery));
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
        public List<OrderAddress> Search(string strWhereClause, string strOrderByClause, out string LastError, int? intPageNumber = null, int? intPageSize = null)
        {
            List<OrderAddress> lstOrderAddress = new List<OrderAddress>();

            LastError = String.Empty;

            try
            {
                SqlCommand objQuery = new SqlCommand();
                objQuery.CommandText = "tblOrderAddress_SelByDyn_H1_Custom";

                objQuery.CommandType = CommandType.StoredProcedure;

                // Add the parameter to the Parameters collection. 
                objQuery.Parameters.Add(new SqlParameter("DynamicWhereClause", strWhereClause));
                objQuery.Parameters.Add(new SqlParameter("DynamicOrderByClause", strOrderByClause));
                objQuery.Parameters.Add(new SqlParameter("RowsPerPage", intPageSize ?? 100000));
                objQuery.Parameters.Add(new SqlParameter("PageNumber", intPageNumber ?? 1));

                lstOrderAddress = DBUtil.DataTableToList<OrderAddress>(SqlServerDataAccess.Execute(objQuery));

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
        public int Count(string strWhereClause, out string LastError)
        {
            int intCount = 0;

            LastError = String.Empty;

            try
            {
                SqlCommand objQuery = new SqlCommand();
                objQuery.CommandText = "tblOrderAddress_SelByDyn_H1_Custom_Ct";

                objQuery.CommandType = CommandType.StoredProcedure;

                // Add the parameter to the Parameters collection. 
                objQuery.Parameters.Add(new SqlParameter("DynamicWhereClause", strWhereClause));

                intCount =(SqlServerDataAccess.ExecuteScalar(objQuery));

            }
            catch (System.Data.SqlClient.SqlException exSql)
            {
                LastError = String.Concat("", exSql.Message, exSql.Number, null);
            }
            catch (Exception ex)
            {
                LastError = String.Concat("", ex.Message, null);
            }

            return intCount;

        }

        public OrderAddress Get(int intOrderAddressID, out string LastError)
        {
            OrderAddress objOrderAddress = null;

            LastError = String.Empty;

            try
            {
                if (intOrderAddressID == 0)
                {
                    LastError = "The identity value supplied for the records PK cannot be zero and must be a valid integer --> OrderAddressID";
                    return null;
                }

                SqlCommand objQuery = new SqlCommand();
                objQuery.CommandText = "tblOrderAddress_SelByIDENT_OrderAddressID_EQ";
                objQuery.CommandType = CommandType.StoredProcedure;

                // Add the parameter to the Parameters collection. 
                objQuery.Parameters.Add(new SqlParameter("OrderAddressID", intOrderAddressID));

                objOrderAddress = DBUtil.DataTableToObject<OrderAddress>(SqlServerDataAccess.Execute(objQuery));

            }
            catch (System.Data.SqlClient.SqlException exSql)
            {
                LastError = String.Concat("", exSql.Message, exSql.Number, null);
            }
            catch (Exception ex)
            {
                LastError = String.Concat("", ex.Message, null);
            }

            return objOrderAddress;
        }

        public OrderAddress Get(int intOrderSystemOfRecordID, string strCDWCustomerOrderNumber, string strCDWAddressOne, string strCDWCity, string strCDWState, string strCDWPostalCode, string strCDWCountry, string strCDWFloor, string strCDWRoom, string strCDWSuite, out string LastError)
        {

            OrderAddress objOrderAddress = null;

            LastError = String.Empty;

            try
            {
                if (intOrderSystemOfRecordID == 0)
                {
                    LastError = "The value supplied for 'intOrderSystemOfRecordID' cannot be zero and must be a valid integer that corresponds to an enum";
                    return null;
                }

                SqlCommand objQuery = new SqlCommand();
                objQuery.CommandText = "tblOrderAddress_SelBy_OrderSystemOfRecordID_EQ_CDWCustomerOrderNumber_EQ_CDWAddressOne_EQ_CDWCity_EQ_CDWState_EQ_C_TRUNC";
                objQuery.CommandType = CommandType.StoredProcedure;

                // Add the parameter to the Parameters collection. 
                objQuery.Parameters.Add(new SqlParameter("OrderSystemOfRecordID", (object)intOrderSystemOfRecordID ?? String.Empty));
                objQuery.Parameters.Add(new SqlParameter("CDWCustomerOrderNumber", (object)strCDWCustomerOrderNumber ?? String.Empty));
                objQuery.Parameters.Add(new SqlParameter("CDWAddressOne", (object)strCDWAddressOne ?? String.Empty));
                objQuery.Parameters.Add(new SqlParameter("CDWCity", (object)strCDWCity ?? String.Empty));
                objQuery.Parameters.Add(new SqlParameter("CDWState", (object)strCDWState ?? String.Empty));
                objQuery.Parameters.Add(new SqlParameter("CDWPostalCode", (object)strCDWPostalCode ?? String.Empty));
                objQuery.Parameters.Add(new SqlParameter("CDWCountry", (object)strCDWCountry ?? String.Empty));
                objQuery.Parameters.Add(new SqlParameter("CDWFloor", (object)strCDWFloor ?? String.Empty));
                objQuery.Parameters.Add(new SqlParameter("CDWRoom", (object)strCDWRoom ?? String.Empty));
                objQuery.Parameters.Add(new SqlParameter("CDWSuite", (object)strCDWSuite ?? String.Empty));

                objOrderAddress = DBUtil.DataTableToObject<OrderAddress>(SqlServerDataAccess.Execute(objQuery));

            }
            catch (System.Data.SqlClient.SqlException exSql)
            {
                LastError = String.Concat("", exSql.Message, exSql.Number, null);
            }
            catch (Exception ex)
            {
                LastError = String.Concat("", ex.Message, null);
            }

            return objOrderAddress;
        }

        public bool Insert(out string LastError)
        {
            bool blnRecordsInserted = false;
            int? intID;

            LastError = String.Empty;

            try
            {
                if (this.OrderAddressID > 0)
                {
                    LastError = "This object's identity column is already populated, which is not allowed since the field is automatically assigned by the database --> OrderAddressID";
                    return false;
                }

                SqlCommand objQuery = new SqlCommand();
                objQuery.CommandText = "tblOrderAddress_Insert";
                objQuery.CommandType = CommandType.StoredProcedure;

                AddCommonSQLParams(ref objQuery);

                intID = SqlServerDataAccess.ExecuteScalar(objQuery);

                blnRecordsInserted = intID > 0;

                this.OrderAddressID = intID;
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

        public bool Update(out string LastError)
        {
            bool blnRecordUpdated = false;
            int? intNumberOfRecordsUpdated;

            LastError = String.Empty;

            try
            {
                if ((this.OrderAddressID == 0) || (this.OrderAddressID == null))
                {
                    LastError = "This object's identity column MUST be populated, so that we know which record needs update --> OrderAddressID";
                    return false;
                }

                SqlCommand objQuery = new SqlCommand();
                objQuery.CommandText = "tblOrderAddress_Update";
                objQuery.CommandType = CommandType.StoredProcedure;

                // Add the parameter to the Parameters collection. 
                objQuery.Parameters.Add(new SqlParameter("OrderAddressID", (object)this.OrderAddressID ?? DBNull.Value));
                AddCommonSQLParams(ref objQuery);

                intNumberOfRecordsUpdated = SqlServerDataAccess.ExecuteNonQuery(objQuery);

                blnRecordUpdated = (intNumberOfRecordsUpdated == 1);

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

        public bool UpdateOptimistic(out string LastError)
        {
            bool blnRecordUpdated = false;
            int? intNumberOfRecordsUpdated;

            LastError = String.Empty;

            try
            {
                if ((this.OrderAddressID == 0) || (this.OrderAddressID == null))
                {
                    LastError = "This object's identity column MUST be populated, so that we know which record needs update --> OrderAddressID";
                    return false;
                }

                SqlCommand objQuery = new SqlCommand();
                objQuery.CommandText = "tblOrderAddress_UpdateOptimistic";
                objQuery.CommandType = CommandType.StoredProcedure;

                // Add the parameter to the Parameters collection. 
                objQuery.Parameters.Add(new SqlParameter("DataHash", (object)this.DataHash ?? DBNull.Value));
                objQuery.Parameters.Add(new SqlParameter("OrderAddressID", (object)this.OrderAddressID ?? DBNull.Value));
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
            objQuery.Parameters.Add(new SqlParameter("OrderAddressTypeID", (object)this.OrderAddressTypeID ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("MigrationStatusID", (object)this.MigrationStatusID ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("OrderSystemOfRecordID", (object)this.OrderSystemOfRecordID ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("CDWCustomerOrderNumber", (object)this.CDWCustomerOrderNumber ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("CDWAddressOne", (object)this.CDWAddressOne ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("CDWCity", (object)this.CDWCity ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("CDWState", (object)this.CDWState ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("CDWPostalCode", (object)this.CDWPostalCode ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("CDWCountry", (object)this.CDWCountry ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("CDWFloor", (object)this.CDWFloor ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("CDWRoom", (object)this.CDWRoom ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("CDWSuite", (object)this.CDWSuite ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("CDWCLII", (object)this.CDWCLII ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("ValidCLII", (object)this.ValidCLII ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("NumberOfFailedGLMSiteCalls", (object)this.NumberOfFailedGLMSiteCalls ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("ExistsInGLMAsSite", (object)this.ExistsInGLMAsSite ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("GLMPLNumber", (object)this.GLMPLNumber ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("NumberOfFailedGLMSiteCodeExistenceCalls", (object)this.NumberOfFailedGLMSiteCodeExistenceCalls ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("NumberOfFailedGLMSiteCodeCreationCalls", (object)this.NumberOfFailedGLMSiteCodeCreationCalls ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("GLMSiteCode", (object)this.GLMSiteCode ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("HasGLMSiteCode", (object)this.HasGLMSiteCode ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("NumberOfFailedSAPSiteAddressSearchCalls", (object)this.NumberOfFailedSAPSiteAddressSearchCalls ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("NumberOfFailedSAPSiteAddressImportCalls", (object)this.NumberOfFailedSAPSiteAddressImportCalls ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("ExistsInSAPAsSiteAddress", (object)this.ExistsInSAPAsSiteAddress ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("NumberOfRecordsInSAPWithPL", (object)this.NumberOfRecordsInSAPWithPL ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("NumberOfFailedGLMServiceLocationSearchCalls", (object)this.NumberOfFailedGLMServiceLocationSearchCalls ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("NumberOfFailedGLMServiceLocationCreationCalls", (object)this.NumberOfFailedGLMServiceLocationCreationCalls ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("GLMSLNumber", (object)this.GLMSLNumber ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("ExistsInGLMAsServiceLocation", (object)this.ExistsInGLMAsServiceLocation ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("NumberOfFailedGLMSCodeExistenceCalls", (object)this.NumberOfFailedGLMSCodeExistenceCalls ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("NumberOfFailedGLMSCodeCreationCalls", (object)this.NumberOfFailedGLMSCodeCreationCalls ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("GLMSCode", (object)this.GLMSCode ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("HasGLMSCode", (object)this.HasGLMSCode ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("NumberOfFailedSAPServiceLocationAddressSearchCalls", (object)this.NumberOfFailedSAPServiceLocationAddressSearchCalls ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("NumberOfFailedSAPServiceLocationAddressImportCalls", (object)this.NumberOfFailedSAPServiceLocationAddressImportCalls ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("ExistsInSAPAsServiceLocationAddress", (object)this.ExistsInSAPAsServiceLocationAddress ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("NumberOfRecordsInSAPWithSL", (object)this.NumberOfRecordsInSAPWithSL ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("SourceCreationDate", (object)this.SourceCreationDate ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("SourceLastModifyDate", (object)this.SourceLastModifyDate ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("DateTimeOfLastMigrationStatusUpdate", (object)this.DateTimeOfLastMigrationStatusUpdate ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("DateTimeOfLastDupDetection", (object)this.DateTimeOfLastDupDetection ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("DateCreated", (object)this.DateCreated ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("DateUpdated", (object)this.DateUpdated ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("ServiceOrderNumber", (object)this.ServiceOrderNumber ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("FIRST_ORDER_CREATE_DT", (object)this.FIRST_ORDER_CREATE_DT ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("OPE_LAST_MODIFY_DATE", (object)this.OPE_LAST_MODIFY_DATE ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("PL_LAST_MODIFY_DATE", (object)this.PL_LAST_MODIFY_DATE ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("PS_LAST_MODIFY_DATE", (object)this.PS_LAST_MODIFY_DATE ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("TotalProcessingTimeInTickString", (object)this.TotalProcessingTimeInTickString ?? DBNull.Value));
            objQuery.Parameters.Add(new SqlParameter("TotalProcessingTimeAsHumanReadable", (object)this.TotalProcessingTimeAsHumanReadable ?? DBNull.Value));
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
    static class DataRowExtensions
    {
        public static object GetValue(this DataRow row, string column)
        {
            return row.Table.Columns.Contains(column) ? row[column] : null;
        }
    }

}