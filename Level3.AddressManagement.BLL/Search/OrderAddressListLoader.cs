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
    public class OrderAddressListLoader
    {
        // Declare the static logger object that will be used to implement logging via log4net
        private static ILog _objLogger = LogManager.GetLogger(typeof(OrderAddressListLoader));

        private string _strBaseLoggingMessage;
        private bool _blnIsLoaded;
        private OrderAddressListFilter _enmOrderAddressListFilter;
        private OrderAddressListDateRangeFilterType _enmOrderAddressListDateRangeFilterType;
        private DateTime? _dteStartDate;
        private DateTime? _dteEndDate;
        private string _strDynamicSearchWhereClause;
        private int? _intPageNumber;
        private int? _intPageSize;




        // Public Properties
        private List<string> _lstErrorMessages;
        public List<string> ErrorMessages
        {
            get { return _lstErrorMessages; }
            set { _lstErrorMessages = value; }
        }
        private List<OrderAddress> _lstOrderAddresses;

        public List<OrderAddress> OrderAddresses
        {
            get { return _lstOrderAddresses; }
            set { _lstOrderAddresses = value; }
        }


        private int _intTotalRecordCount;

        public int TotalRecordCount
        {
            get { return _intTotalRecordCount; }
            set { _intTotalRecordCount = value; }
        }


        // Constructor
        public OrderAddressListLoader()
        {
            _lstErrorMessages = new List<string>();
            _lstOrderAddresses = new List<OrderAddress>();
        }


        public bool Load(OrderAddressListFilter enmOrderAddressListFilter, OrderAddressListDateRangeFilterType enmOrderAddressListDateRangeFilterType = OrderAddressListDateRangeFilterType.None, DateTime? dteStartDate = null, DateTime? dteEndDate = null, string strDynamicSearchWhereClause = "", int? intPageNumber = null, int? intPageSize = null)
        {
            _enmOrderAddressListFilter = enmOrderAddressListFilter;
            _enmOrderAddressListDateRangeFilterType = enmOrderAddressListDateRangeFilterType;
            _dteStartDate = dteStartDate;
            _dteEndDate = dteEndDate;
            _strDynamicSearchWhereClause = strDynamicSearchWhereClause;

            _intPageNumber = intPageNumber;
            _intPageSize = intPageSize;

            try
            {
                switch (_enmOrderAddressListFilter)
                {
                    case OrderAddressListFilter.Current:
                        LoadApplicableOrderAddressesFromDB();
                        break;
                    case OrderAddressListFilter.Issues:
                        LoadApplicableOrderAddressesFromDB();
                        break;
                    case OrderAddressListFilter.AllNonIssuesInWorkflow:
                        LoadApplicableOrderAddressesFromDB();
                        break;
                    case OrderAddressListFilter.Search:
                        LoadDynamicSearchResultOrderAddressesFromDB();
                        break;
                    default:
                        throw new Exception("The order address list filter type was unrecognized.  A coder messed up.");
                }

            }
            catch (Exception ex)
            {
                _lstErrorMessages.Add(String.Concat("There was an error while trying to LOAD the list of order addresses from the database.  ", _strBaseLoggingMessage, ",  Error Message: [", ex.Message, "]"));
            }

            _blnIsLoaded = _lstErrorMessages.Count() == 0;

            // return 
            return _blnIsLoaded;
        }

        private void LoadApplicableOrderAddressesFromDB()
        {
            string strWhereClause = String.Empty;
            string strMigrationStatusInClause = String.Empty;
            string strWhereClauseDateSnippet = GetDateRangeStringToIncludeInDynamicWhereClause();

            // Get the list of statuses that apply to the PaymentListFilter supplied
            List<MigrationStatuses> lstMigrationStatuses = GetStatusesToIncludeInDynamicWhereInClause();

            if (lstMigrationStatuses.Count > 0)
            {
                List<int> lstMigrationStatusIDs = lstMigrationStatuses.ConvertAll(value => (int)value);
                strMigrationStatusInClause = String.Concat(" tblOrderAddress.MigrationStatusID IN (", String.Join(",", lstMigrationStatusIDs.ToArray()), ") ");
            }

            bool blnNeedsKeyword_AND = false;
            if (String.IsNullOrEmpty(strMigrationStatusInClause) == false)
            {
                strWhereClause = strWhereClause + strMigrationStatusInClause;
                blnNeedsKeyword_AND = true;
            }

            if (String.IsNullOrEmpty(strWhereClauseDateSnippet) == false)
            {
                if (blnNeedsKeyword_AND)
                {
                    strWhereClause = strWhereClause + " AND ";
                }
                strWhereClause = strWhereClause + strWhereClauseDateSnippet;
            }

            int intCount;

            _lstOrderAddresses = new OrderAddress().SearchPaymentWithCount(strWhereClause, "tblOrderAddress.DateCreated asc, tblOrderAddress.OrderAddressID asc", _intPageNumber, _intPageSize, out intCount);
            _intTotalRecordCount = intCount;

            _objLogger.Info(String.Concat("Number of Order Addresses retrieved from the database = [", _lstOrderAddresses.Count, "]"));
        }
        private void LoadDynamicSearchResultOrderAddressesFromDB()
        {
            string strWhereClause = _strDynamicSearchWhereClause;

            if (string.IsNullOrEmpty(strWhereClause) == false)
            {
                int intCount;

                _lstOrderAddresses = new OrderAddress().SearchPaymentWithCount(strWhereClause, "tblOrderAddress.DateCreated asc, tblOrderAddress.OrderAddressID asc", _intPageNumber, _intPageSize, out intCount);
                _intTotalRecordCount = intCount;
                _objLogger.Info(String.Concat("Number of Order Addresses retrieved from the database = [", _lstOrderAddresses.Count, "], Total Count = [", intCount.ToString(), "]"));
            }
        }

        private void EnforceLoadSuccess()
        {
            if (_blnIsLoaded == false)
            {
                throw new Exception("The load method was not call or did not succeed, and so subsequent and dependant methods in this class cannot be invoked.");
            }
        }

        private string GetDateRangeStringToIncludeInDynamicWhereClause()
        {
            // Set variable names that might be used to create the PL-SQL snippet
            string strOrderDateAttributeName = "tblOrderAddress.FIRST_ORDER_CREATE_DT";  
            string strCreatedDateAttributeName = "tblOrderAddress.DateCreated";
            string strLastUpdateDateAttributeName = "tblOrderAddress.DateUpdated";
            string strStartDateCSTString = String.Empty;
            string strEndDateCSTString = String.Empty;


            // Set the START DATE string if a value was supplied by the caller
            if (_dteStartDate.HasValue)
            {
                strStartDateCSTString = "'" + (_dteStartDate.Value).ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
            }

            // Set the END DATE string if a value was supplied by the caller
            if (_dteEndDate.HasValue)
            {
                strEndDateCSTString = "'" + (_dteEndDate.Value).ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
            }


            // Declare the string that will serve as the return variable
            string strDateCondition;


            // Switch based on the Filter Type supplied by the caller
            switch (_enmOrderAddressListDateRangeFilterType)
            {
                case OrderAddressListDateRangeFilterType.OrderDate_StartEnd:

                    // Validate the dates 
                    if ((_dteStartDate.HasValue && _dteEndDate.HasValue) == false)
                    {
                        throw new Exception("The OrderAddressListDateRangeFilterType supplied necessitates that a NON-NULL values be supplied for both the start and end date.  One or both of those values was NULL.");
                    }

                    // Build the pl-sql snippet
                    strDateCondition = String.Format("{0} >= {1} AND {0} <={2} ", strOrderDateAttributeName, strStartDateCSTString, strEndDateCSTString);

                    // Return the pl-sql snippet
                    return strDateCondition;

                case OrderAddressListDateRangeFilterType.OrderDate_End:

                    if ((_dteEndDate.HasValue) == false)
                    {
                        throw new Exception("The OrderAddressListDateRangeFilterType supplied necessitates that a NON-NULL value be supplied for the end date.  The value is NULL.");
                    }

                    strDateCondition = String.Format("{0} <= {1}", strOrderDateAttributeName, strEndDateCSTString);

                    return strDateCondition;

                case OrderAddressListDateRangeFilterType.CreatedOrModifiedDate_StartEnd:

                    // Validate the dates 
                    if ((_dteStartDate.HasValue && _dteEndDate.HasValue) == false)
                    {
                        throw new Exception("The OrderAddressListDateRangeFilterType supplied necessitates that a NON-NULL values be supplied for both the start and end date.  One or both of those values was NULL.");
                    }

                    // Build the pl-sql snippet
                    strDateCondition = String.Format("({0} >= {1} AND {0} <= {2}) OR ({3} >= {1} AND {3} <= {2})", strCreatedDateAttributeName, strStartDateCSTString, strEndDateCSTString, strLastUpdateDateAttributeName);

                    // Return the pl-sql snippet
                    return strDateCondition;

                case OrderAddressListDateRangeFilterType.CreatedOrModifiedDeadline_End:

                    if ((_dteEndDate.HasValue) == false)
                    {
                        throw new Exception("The OrderAddressListDateRangeFilterType supplied necessitates that a NON-NULL value be supplied for the end date.  The value is NULL.");
                    }

                    strDateCondition = String.Format("({0} <= {1}) OR ({2} <= {1})", strCreatedDateAttributeName, strEndDateCSTString, strLastUpdateDateAttributeName);

                    return strDateCondition;

                case OrderAddressListDateRangeFilterType.None:
                    return String.Empty;
                default:
                    throw new Exception("The OrderAddressListDateRangeFilterType enum was not recognized.  Did the developer miss a case in this switch statement?");
            }
        }

        private List<MigrationStatuses> GetStatusesToIncludeInDynamicWhereInClause()
        {
            switch (_enmOrderAddressListFilter)
            {
                case OrderAddressListFilter.Current:
                    return GetStatuses_Current();
                case OrderAddressListFilter.Issues:
                    return GetStatuses_Issues();
                case OrderAddressListFilter.Search:
                    return GetStatuses_Search();
                case OrderAddressListFilter.AllNonIssuesInWorkflow:
                    return GetStatuses_AllNonIssuesInWorkflow();
                default:
                    throw new Exception("The Order Address List Filter enum was not recognized.  Did the developer miss a case in this switch statement?");
            }
        }
        private List<MigrationStatuses> GetStatuses_Current()
        {
            //Since current will be based on date range, get all the statuses
            List<MigrationStatuses> lstMigrationStatuses = EnumUtil.GetValues<MigrationStatuses>().ToList();
            return lstMigrationStatuses;
        }
        private List<MigrationStatuses> GetStatuses_Issues()
        {
            List<MigrationStatuses> lstMigrationStatuses = new List<MigrationStatuses>();
            // TODO: Fill in the correct statuses
            //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_Processing);
            //lstMigrationStatuses.Add(MigrationStatuses.GLM_SITE_FOUND_or_CREATED);
            lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SITE_existence_CHECK);
            lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SITE_existence_CHECK_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SITE_CODE_existence_CHECK);
            lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SITE_CODE_existence_CHECK);
            lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SITE_CODE_existence_CHECK_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.SITE_CODE_FOUND_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.SITE_CODE_does_NOT_EXIST_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SITE_CODE_ASSIGNMENT_TRIGGERING_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.SITE_CODE_ASSIGNMENT_Successfully_TRIGGERED_in_GLM);
            lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_trying_to_TRIGGER_the_ASSIGNMENT_of_a_SITE_CODE_in_GLM);
            lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SITE_CODE_ASSIGNMENT_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.WORK_ITEM_QUEUED_in_GLM_for_DELAYED_SITE_CODE_ASSIGNMENT);
            //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SAP_SITE_LOCATION_SEARCH);
            lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_SAP_SITE_LOCATION_ADDRESS_SEARCH);
            lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_SAP_SITE_LOCATION_SEARCH_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.Site_Location_ADDRESS_FOUND_in_SAP);
            //lstMigrationStatuses.Add(MigrationStatuses.Site_Location_ADDRESS_does_NOT_EXIST_in_SAP);
            //lstMigrationStatuses.Add(MigrationStatuses.READY_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP);
            lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_attempting_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP);
            lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_attempts_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.READY_for_ADDRESS_TYPE_BRANCHING_Logic_EVALUATION);
            //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_GLM_SERVICE_LOCATION_SEARCH);
            lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SERVICE_LOCATION_SEARCH);
            lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SERVICE_LOCATION_SEARCH_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.SERVICE_LOCATION_FOUND_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.SERVICE_LOCATION_does_NOT_EXIST_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SERVICE_LOCATION_CREATION_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.SERVICE_LOCATION_Successfully_CREATED_in_GLM);
            lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_the_GLM_SERVICE_LOCATION_CREATION_attempt);
            lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SERVICE_LOCATION_CREATION_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_S_CODE_existence_CHECK);
            lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_S_CODE_existence_CHECK);
            lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_S_CODE_existence_CHECK_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.S_CODE_FOUND_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.S_CODE_does_NOT_EXIST_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_S_CODE_CREATION_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.S_CODE_Successfully_CREATED_in_GLM);
            lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_S_CODE_CREATION_attempt);
            lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_S_CODE_CREATION_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.WORK_ITEM_QUEUED_in_GLM_for_DELAYED_S_CODE_CREATION);
            //lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SAP_SERVICE_LOCATION_SEARCH);
            lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_SAP_SERVICE_LOCATION_ADDRESS_SEARCH);
            lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_SAP_SERVICE_LOCATION_SEARCH_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.Service_Location_ADDRESS_FOUND_in_SAP);
            //lstMigrationStatuses.Add(MigrationStatuses.Service_Location_ADDRESS_does_NOT_EXIST_in_SAP);
            //lstMigrationStatuses.Add(MigrationStatuses.READY_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP);
            lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_attempting_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP);
            lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_attempts_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.Processing_COMPLETE);
            //lstMigrationStatuses.Add(MigrationStatuses.IGNORED_indefinitely);


            return lstMigrationStatuses;
        }
        private List<MigrationStatuses> GetStatuses_AllNonIssuesInWorkflow()
        {
            List<MigrationStatuses> lstMigrationStatuses = new List<MigrationStatuses>();

            lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_Processing);
            lstMigrationStatuses.Add(MigrationStatuses.GLM_SITE_FOUND_or_CREATED);
            //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SITE_existence_CHECK);
            //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SITE_existence_CHECK_due_to_API_Call_Errors);
            lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SITE_CODE_existence_CHECK);
            //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SITE_CODE_existence_CHECK);
            //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SITE_CODE_existence_CHECK_due_to_API_Call_Errors);
            lstMigrationStatuses.Add(MigrationStatuses.SITE_CODE_FOUND_in_GLM);
            lstMigrationStatuses.Add(MigrationStatuses.SITE_CODE_does_NOT_EXIST_in_GLM);
            lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SITE_CODE_ASSIGNMENT_TRIGGERING_in_GLM);
            lstMigrationStatuses.Add(MigrationStatuses.SITE_CODE_ASSIGNMENT_Successfully_TRIGGERED_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_trying_to_TRIGGER_the_ASSIGNMENT_of_a_SITE_CODE_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SITE_CODE_ASSIGNMENT_due_to_API_Call_Errors);
            lstMigrationStatuses.Add(MigrationStatuses.WORK_ITEM_QUEUED_in_GLM_for_DELAYED_SITE_CODE_ASSIGNMENT);
            lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SAP_SITE_LOCATION_SEARCH);
            //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_SAP_SITE_LOCATION_ADDRESS_SEARCH);
            //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_SAP_SITE_LOCATION_SEARCH_due_to_API_Call_Errors);
            lstMigrationStatuses.Add(MigrationStatuses.Site_Location_ADDRESS_FOUND_in_SAP);
            lstMigrationStatuses.Add(MigrationStatuses.Site_Location_ADDRESS_does_NOT_EXIST_in_SAP);
            lstMigrationStatuses.Add(MigrationStatuses.READY_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP);
            //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_attempting_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP);
            //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_attempts_to_TRIGGER_SITE_LOCATION_ADDRESS_IMPORT_to_SAP_due_to_API_Call_Errors);
            lstMigrationStatuses.Add(MigrationStatuses.READY_for_ADDRESS_TYPE_BRANCHING_Logic_EVALUATION);
            lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_GLM_SERVICE_LOCATION_SEARCH);
            //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_SERVICE_LOCATION_SEARCH);
            //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SERVICE_LOCATION_SEARCH_due_to_API_Call_Errors);
            lstMigrationStatuses.Add(MigrationStatuses.SERVICE_LOCATION_FOUND_in_GLM);
            lstMigrationStatuses.Add(MigrationStatuses.SERVICE_LOCATION_does_NOT_EXIST_in_GLM);
            lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SERVICE_LOCATION_CREATION_in_GLM);
            lstMigrationStatuses.Add(MigrationStatuses.SERVICE_LOCATION_Successfully_CREATED_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_the_GLM_SERVICE_LOCATION_CREATION_attempt);
            //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_SERVICE_LOCATION_CREATION_due_to_API_Call_Errors);
            lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_S_CODE_existence_CHECK);
            //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_S_CODE_existence_CHECK);
            //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_S_CODE_existence_CHECK_due_to_API_Call_Errors);
            lstMigrationStatuses.Add(MigrationStatuses.S_CODE_FOUND_in_GLM);
            lstMigrationStatuses.Add(MigrationStatuses.S_CODE_does_NOT_EXIST_in_GLM);
            lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_S_CODE_CREATION_in_GLM);
            lstMigrationStatuses.Add(MigrationStatuses.S_CODE_Successfully_CREATED_in_GLM);
            //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_GLM_S_CODE_CREATION_attempt);
            //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_GLM_S_CODE_CREATION_due_to_API_Call_Errors);
            lstMigrationStatuses.Add(MigrationStatuses.WORK_ITEM_QUEUED_in_GLM_for_DELAYED_S_CODE_CREATION);
            lstMigrationStatuses.Add(MigrationStatuses.STAGED_for_SAP_SERVICE_LOCATION_SEARCH);
            //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_executing_SAP_SERVICE_LOCATION_ADDRESS_SEARCH);
            //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_SAP_SERVICE_LOCATION_SEARCH_due_to_API_Call_Errors);
            lstMigrationStatuses.Add(MigrationStatuses.Service_Location_ADDRESS_FOUND_in_SAP);
            lstMigrationStatuses.Add(MigrationStatuses.Service_Location_ADDRESS_does_NOT_EXIST_in_SAP);
            lstMigrationStatuses.Add(MigrationStatuses.READY_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP);
            //lstMigrationStatuses.Add(MigrationStatuses.API_Call_ERRORED_while_attempting_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP);
            //lstMigrationStatuses.Add(MigrationStatuses.ABANDONED_attempts_to_TRIGGER_SERVICE_LOCATION_ADDRESS_IMPORT_to_SAP_due_to_API_Call_Errors);
            //lstMigrationStatuses.Add(MigrationStatuses.Processing_COMPLETE);
            //lstMigrationStatuses.Add(MigrationStatuses.IGNORED_indefinitely);

            return lstMigrationStatuses;
        }
        private List<MigrationStatuses> GetStatuses_Search()
        {
            // Since search results shouls search all statues, check this value
            List<MigrationStatuses> lstMigrationStatuses = EnumUtil.GetValues<MigrationStatuses>().ToList();
            return lstMigrationStatuses;
        }

    }
}

