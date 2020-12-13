using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;
using Level3.AddressManagement.DAL;
using Level3.AddressManagement.Model;

namespace Level3.AddressManagement.BLL
{
    public class OrderAddressSearchManager
    {// log4net logger declaration
        private static ILog _objLogger = LogManager.GetLogger(typeof(OrderAddressSearchManager));

        // Public Properties
        private List<string> _lstValidationErrors;
        public List<string> ValidationErrors
        {
            get { return _lstValidationErrors; }
            set { _lstValidationErrors = value; }
        }


        private List<string> _lstErrorMessages;
        public List<string> ErrorMessages
        {
            get { return _lstErrorMessages; }
            set { _lstErrorMessages = value; }
        }

        private List<OrderAddress> _lstSearchResults;

        public List<OrderAddress> SearchResults
        {
            get { return _lstSearchResults; }
            set { _lstSearchResults = value; }
        }



        // Private Members
        List<string> _lstOrderSystemOfRecords;
        List<string> _lstMigrationStatuses;
        List<string> _lstOrderAddressTypes;

        DateTime? _dteOrderDate_Start;
        DateTime? _dteOrderDate_End;

        DateTime? _dteCreatedDate_Start;
        DateTime? _dteCreatedDate_End;

        string _strCustomerOrderNumber;
        string _strAddressLine1;

        private bool _blnIsValid;
        private string _strWhereClause;

        private OrderAddressSearchControlState _objOrderAddressSearchControlState;

        // Constructor
        public OrderAddressSearchManager()
        {
            _lstMigrationStatuses = new List<string>();
            _lstOrderAddressTypes = new List<string>();
            _lstOrderSystemOfRecords = new List<string>();
            _lstValidationErrors = new List<string>();
            _lstErrorMessages = new List<string>();
            _lstSearchResults = new List<OrderAddress>();
        }


        // Public Methods
        public bool Load(OrderAddressSearchControlState objOrderAddressSearchControlState)
        {
            try
            {
                // Set the local variables
                _lstOrderSystemOfRecords = objOrderAddressSearchControlState.OrderSystemOfRecords;
                _lstMigrationStatuses = objOrderAddressSearchControlState.MigrationStatuses;
                _lstOrderAddressTypes = objOrderAddressSearchControlState.OrderAddressTypes;

                _dteOrderDate_Start = objOrderAddressSearchControlState.OrderDate_Start;
                _dteOrderDate_End = objOrderAddressSearchControlState.OrderDate_End;

                _dteCreatedDate_Start = objOrderAddressSearchControlState.DateCreated_Start;
                _dteCreatedDate_End = objOrderAddressSearchControlState.DateCreated_End;

                _strCustomerOrderNumber = objOrderAddressSearchControlState.CustomerOrderNumber;
                _strAddressLine1 = objOrderAddressSearchControlState.AddressLine1;


                _objOrderAddressSearchControlState = objOrderAddressSearchControlState;

                // Validate the input params supplied
                ValidateSearchParams();

                // Build the WHERE Clause
                BuildWhereClause();
            }
            catch (Exception ex)
            {
                _lstErrorMessages.Add(String.Concat("There was an unexepected ERROR while trying to retrieve the SEARCH results.  Please adjust your search criteria and try again.  Error Message [ ", ex.Message, "]"));
            }

            return ((_lstErrorMessages.Count == 0) && (_lstValidationErrors.Count == 0));

        }
        public bool Load(List<string> lstOrderSystemOfRecords, List<string> lstMigrationStatuses, List<string> lstOrderAddressTypes, DateTime? dteOrderDateStart, DateTime? dteOrderDateEnd, DateTime? dteDateCreatedStart, DateTime? dteDateCreatedEnd, string strCustomerOrderNumber, string strAddressLine1)
        {
            try
            {
                // Set the local variables
                _lstOrderSystemOfRecords = lstOrderSystemOfRecords;
                _lstMigrationStatuses = lstMigrationStatuses;
                _lstOrderAddressTypes = lstOrderAddressTypes;

                _dteOrderDate_Start = dteOrderDateStart;
                _dteOrderDate_End = dteOrderDateEnd;

                _dteCreatedDate_Start = dteDateCreatedStart;
                _dteCreatedDate_End = dteDateCreatedEnd;

                _strCustomerOrderNumber = strCustomerOrderNumber;
                _strAddressLine1 = strAddressLine1;

                // Validate the input params supplied
                ValidateSearchParams();

                // Build the WHERE Clause
                BuildWhereClause();

            }
            catch (Exception ex)
            {
                _lstErrorMessages.Add(String.Concat("There was an unexepected ERROR while trying to retrieve the SEARCH results.  Please adjust your search criteria and try again.  Error Message [ ", ex.Message, "]"));
            }

            return ((_lstErrorMessages.Count == 0) && (_lstValidationErrors.Count == 0));
        }
        public bool IsValid()
        {
            return _blnIsValid;
        }
        public bool ExecuteSearch(int? intPageNumber, int? intPageSize)
        {
            try
            {
                MakeDBCall(intPageNumber, intPageSize);

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public bool MakeDBCall(int? intPageNumber, int? intPageSize)
        {
            string strLastError = String.Empty;

            try
            {
                _lstSearchResults = new OrderAddress().Search(_strWhereClause, String.Empty, out strLastError, intPageNumber, intPageSize);

                return true;
            }
            catch (Exception ex)
            {
                string strLoggingString = String.Concat("There was an exception while trying to execute the dynamic where clause against the payment table as part of a record search.  Error Message [ ", ex.Message, " - ", strLastError ,"], WHERE Clause = [", _strWhereClause, "]");
                _objLogger.Error(strLoggingString);

                return false;
            }
        }
        public String GetWhereClause()
        {
            return _strWhereClause;
        }
        public String GetWhereClauseBase64Encoded()
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(GetWhereClause());
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }
        public String GetOrderAddressStateAsBase64EncodedJsonString()
        {
            return Base64EncodeString(JsonConvert.SerializeObject(_objOrderAddressSearchControlState));
        }
        private String Base64EncodeString(string strToEncode)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(strToEncode);
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }


        // Private methods

        private void ValidateSearchParams()
        {
            // If there is a Ordder Date End Date, Make sure there is a start date that is <=
            if (_dteOrderDate_End.HasValue && !_dteOrderDate_Start.HasValue)
            {
                _lstValidationErrors.Add("The Order Date START Date cannot be blank if a value is supplied for the Order Date TO/END date.");
            }
            else
            {
                if (_dteOrderDate_Start > _dteOrderDate_End)
                {
                    _lstValidationErrors.Add("Order Date START date must <= the Order Date TO/END date");
                }
            }

            // If there is a Created End Date, Make sure there is a start date that is <=
            if (_dteCreatedDate_End.HasValue && !_dteCreatedDate_Start.HasValue)
            {
                _lstValidationErrors.Add("The Date Created START Date cannot be blank if a value is supplied for the Created Date TO/END date.");
            }
            else
            {
                if (_dteCreatedDate_Start > _dteCreatedDate_End)
                {
                    _lstValidationErrors.Add("Date Created START date must <= the Date Created TO/END date");
                }
            }

            _blnIsValid = (_lstValidationErrors.Count == 0);
        }
        private void EnforceIsValid()
        {
            if (_blnIsValid == false)
            {
                throw new Exception("There was an issue validating the search Criteria: ");
            }
        }
        private void BuildWhereClause()
        {

            _strWhereClause = String.Empty;

            string strVariable_OrderSystemOfRecordID = "tblOrderAddress.OrderSystemOfRecordID";
            string strVariable_MigrationStatusID = "tblOrderAddress.MigrationStatusID";
            string strVariable_OrderAddressTypeID = "tblOrderAddress.OrderAddressTypeID";
            string strVariable_CustomerOrderNumber = "tblOrderAddress.ServiceOrderNumber";
            string strVariable_AddressOne = "tblOrderAddress.CDWAddressOne";
            string strVariable_OrderDate = "tblOrderAddress.FIRST_ORDER_CREATE_DT";
            string strVariable_DateCreated = "tblOrderAddress.DateCreated";

            // Declare a stringbuilder to use to build the WHERE clause
            StringBuilder sbWhereClause = new StringBuilder();


            // System of Records
            if (_lstOrderSystemOfRecords?.Count > 0)
            {
                sbWhereClause.Append(String.Concat(strVariable_OrderSystemOfRecordID, " IN (", String.Join(",", _lstOrderSystemOfRecords.ToArray()), ")"));
            }


            // Customer Order Number
            if (!(string.IsNullOrEmpty(_strCustomerOrderNumber)))
            {
                if (sbWhereClause.ToString().Length > 0)
                {
                    sbWhereClause.Append(" AND ");
                }

                sbWhereClause.Append(String.Concat("UPPER(", strVariable_CustomerOrderNumber, ")", " LIKE ", "'%", _strCustomerOrderNumber.ToUpper(), "%'"));
            }


            // Order Date
            if (_dteOrderDate_Start.HasValue)
            {
                if (sbWhereClause.ToString().Length > 0)
                {
                    sbWhereClause.Append(" AND ");
                }

                string strDateString;
                if (_dteOrderDate_End.HasValue)
                {
                    // This is a range, so add the appropriate syntax and date precision
                    strDateString = "'" + (_dteOrderDate_Start.Value).ToString("yyyy-MM-dd HH:mm:ss.fff") + "'"; 
                    sbWhereClause.Append(String.Concat(strVariable_OrderDate, " >= ", strDateString));
                }
                else
                {
                    strDateString = "'" + (_dteOrderDate_Start.Value).ToString("yyyy-MM-dd") + "'"; ;
                    sbWhereClause.Append(String.Concat("cast(", strVariable_OrderDate, " as date ) = ", strDateString));
                }
            }

            if (_dteOrderDate_End.HasValue)
            {
                if (sbWhereClause.ToString().Length > 0)
                {
                    sbWhereClause.Append(" AND ");
                }

                string strDateString = "'" + (_dteOrderDate_End.Value.Date.AddDays(1).AddTicks(-1)).ToString("yyyy-MM-dd HH:mm:ss.fff") + "'"; ;
                sbWhereClause.Append(String.Concat(strVariable_OrderDate, " <= ", strDateString));
            }



            // Address One
            if (!(string.IsNullOrEmpty(_strAddressLine1)))
            {
                if (sbWhereClause.ToString().Length > 0)
                {
                    sbWhereClause.Append(" AND ");
                }

                sbWhereClause.Append(String.Concat("UPPER(", strVariable_AddressOne, ")", " LIKE ", "UPPER('%", _strAddressLine1.ToUpper(), "%')"));
            }


            // Migration Status
            if (_lstMigrationStatuses?.Count > 0)
            {
                if (sbWhereClause.ToString().Length > 0)
                {
                    sbWhereClause.Append(" AND ");
                }

                sbWhereClause.Append(String.Concat(strVariable_MigrationStatusID, " IN (", String.Join(",", _lstMigrationStatuses.ToArray()), ")"));
            }


            // Date Created
            if (_dteCreatedDate_Start.HasValue)
            {
                if (sbWhereClause.ToString().Length > 0)
                {
                    sbWhereClause.Append(" AND ");
                }

                string strDateString;
                if (_dteCreatedDate_End.HasValue)
                {
                    // This is a range, so add the appropriate syntax and date precision 
                    strDateString = "'" + (_dteCreatedDate_Start.Value).ToString("yyyy-MM-dd HH:mm:ss.fff") + "'"; ;
                    sbWhereClause.Append(String.Concat(strVariable_DateCreated, " >= ", strDateString));
                }
                else
                {
                    strDateString = "'" + (_dteCreatedDate_Start.Value).ToString("yyyy-MM-dd") + "'"; ;
                    sbWhereClause.Append(String.Concat("cast(", strVariable_DateCreated, " as date ) = ", strDateString));
                }
            }

            if (_dteCreatedDate_End.HasValue)
            {
                if (sbWhereClause.ToString().Length > 0)
                {
                    sbWhereClause.Append(" AND ");
                }

                string strDateString = "'" + (_dteCreatedDate_End.Value.Date.AddDays(1).AddTicks(-1)).ToString("yyyy-MM-dd HH:mm:ss.fff") + "'"; ;
                sbWhereClause.Append(String.Concat(strVariable_DateCreated, " <= ", strDateString));
            }


            // Order Address Type
            if (_lstOrderAddressTypes?.Count > 0)
            {
                if (sbWhereClause.ToString().Length > 0)
                {
                    sbWhereClause.Append(" AND ");
                }

                sbWhereClause.Append(String.Concat(strVariable_OrderAddressTypeID, " IN (", String.Join(",", _lstOrderAddressTypes.ToArray()), ")"));
            }

            _strWhereClause = sbWhereClause.ToString();
        }

    }
}
