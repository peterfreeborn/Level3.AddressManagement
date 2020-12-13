
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

using Level3.AddressManagement.BLL;
using Level3.AddressManagement.DAL;
using Level3.AddressManagement.Model;

using log4net;

namespace Level3.AddressManagement.UI.Web
{
    public partial class OrderAddressSearchControls : System.Web.UI.UserControl
    {

        // Declare the static logger object that will be used to implement logging via log4net
        private static ILog _objLogger = LogManager.GetLogger(typeof(OrderAddressSearchControls));

        string _strQSParamName = OrderAddressSearchControlStateUtil.GetQueryStringParamNameWhereClause();
        string _strQSParamNameJsonState = OrderAddressSearchControlStateUtil.GetQueryStringParamNameJsonSearchControlState();
        string _strEncodedSearchQuery;
        string _strEncodedSearchJsonState;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {


                if (!(Page.IsPostBack))
                {
                    LoadDDL_OrderSystemOfRecord();
                    LoadDDL_MigrationStatus();
                    LoadDDL_OrderAddressTypes();
                    ResetSearchForm();
                }

                if (string.IsNullOrEmpty(Request.QueryString[_strQSParamName]) == false)
                {
                    _strEncodedSearchQuery = Request.QueryString[_strQSParamName];
                    _strEncodedSearchJsonState = Request.QueryString[_strQSParamNameJsonState];

                    if (!(Page.IsPostBack))
                    {
                        DecodeQSParam();
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.InnerText = String.Concat("There was an error loading the search controls.  Please try your request again and contact IT if the issue persists.  Error Message = [", ex.Message, "]");
                _objLogger.Error(String.Concat("There was an exception thrown while trying to load the user control that hosts the order address search controls.  Error Message = [", ex.Message, "], Current URL = [", HttpContext.Current.Request.Url.AbsoluteUri, "]"));
            }
        }

        private void DecodeQSParam()
        {
            if (string.IsNullOrEmpty(_strEncodedSearchQuery) == false)
            {
                byte[] encodedDataAsBytes = System.Convert.FromBase64String(Server.UrlDecode(_strEncodedSearchQuery));
                string strWhereClauseDecoded = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);

                byte[] encodedDataAsBytesJson = System.Convert.FromBase64String(Server.UrlDecode(_strEncodedSearchJsonState));
                string strJsonDecoded = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytesJson);

                lblWhereClause.InnerText = "Decoded PL-SQL WHERE clause: " + System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);// + "Json = " + System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytesJson);

                // Now that we have the info derived from 
                SetSearchControlValues(strJsonDecoded);
            }
        }
        private void SetSearchControlValues(string strJsonDecoded)
        {
            // Set the search controls
            OrderAddressSearchControlState objOrderAddressSearchControlStateUtil = OrderAddressSearchControlStateUtil.Deserialize(strJsonDecoded);

            // SORs
            for (int i = 0; i < objOrderAddressSearchControlStateUtil.OrderSystemOfRecords?.Count; i++)
            {
                if (ddlOrderSystemOfRecords.FindItemByValue(objOrderAddressSearchControlStateUtil.OrderSystemOfRecords[i].ToString()) != null)
                {
                    ddlOrderSystemOfRecords.FindItemByValue(objOrderAddressSearchControlStateUtil.OrderSystemOfRecords[i].ToString()).Checked = true;
                    ddlOrderSystemOfRecords.FindItemByValue(objOrderAddressSearchControlStateUtil.OrderSystemOfRecords[i].ToString()).Selected = true;
                }
                else
                {
                    throw new Exception("Value not found in list.  Your saved query is no longer valid.");
                }
            }


            // Migration Statuses
            for (int i = 0; i < objOrderAddressSearchControlStateUtil.MigrationStatuses?.Count; i++)
            {
                if (ddlMigrationStatuses.FindItemByValue(objOrderAddressSearchControlStateUtil.MigrationStatuses[i].ToString()) != null)
                {
                    ddlMigrationStatuses.FindItemByValue(objOrderAddressSearchControlStateUtil.MigrationStatuses[i].ToString()).Checked = true;
                    ddlMigrationStatuses.FindItemByValue(objOrderAddressSearchControlStateUtil.MigrationStatuses[i].ToString()).Selected = true;
                }
                else
                {
                    throw new Exception("Value not found in list.  Your saved query is no longer valid.");
                }
            }


            // Order Address Types
            for (int i = 0; i < objOrderAddressSearchControlStateUtil.OrderAddressTypes?.Count; i++)
            {
                if (ddlOrderAddressTypes.FindItemByValue(objOrderAddressSearchControlStateUtil.OrderAddressTypes[i].ToString()) != null)
                {
                    ddlOrderAddressTypes.FindItemByValue(objOrderAddressSearchControlStateUtil.OrderAddressTypes[i].ToString()).Checked = true;
                    ddlOrderAddressTypes.FindItemByValue(objOrderAddressSearchControlStateUtil.OrderAddressTypes[i].ToString()).Selected = true;
                }
                else
                {
                    throw new Exception("Value not found in list.  Your saved query is no longer valid.");
                }
            }

            // OrderAddress Date
            dteOrderDateStart.SelectedDate = objOrderAddressSearchControlStateUtil.OrderDate_Start;
            dteOrderDateEnd.SelectedDate = objOrderAddressSearchControlStateUtil.OrderDate_End;

            // Date Created
            dteDateCreatedStart.SelectedDate = objOrderAddressSearchControlStateUtil.DateCreated_Start;
            dteDateCreatedEnd.SelectedDate = objOrderAddressSearchControlStateUtil.DateCreated_End;

            // Customer Order Number
            txtCustomerOrderNumber.Text = objOrderAddressSearchControlStateUtil.CustomerOrderNumber;

            // Address Line 1
            txtAddressLine1.Text = objOrderAddressSearchControlStateUtil.AddressLine1;

        }

        // Button/Link Click Methods
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblError.InnerText = String.Empty;

            // Order System of Records
            List<string> lstOrderSystemsOfRecord;
            if (ddlOrderSystemOfRecords.CheckedItems.Count() > 0)
            {
                lstOrderSystemsOfRecord = new List<string>();
                lstOrderSystemsOfRecord.AddRange(ddlOrderSystemOfRecords.CheckedItems.ToList().Select(ddl => ddl.Value.ToString()).ToList());
            }
            else
            {
                // Set to null, so that the search manager will return records for all SORs (ie - records are ONLY filtered OUT if one or more values are checked)
                lstOrderSystemsOfRecord = null;
            }


            // Migration Statuses
            List<string> lstMigrationStatuses;
            if (ddlMigrationStatuses.CheckedItems.Count() > 0)
            {
                lstMigrationStatuses = new List<string>();
                lstMigrationStatuses.AddRange(ddlMigrationStatuses.CheckedItems.ToList().Select(ddl => ddl.Value.ToString()).ToList());
            }
            else
            {
                // Set to null, so that the search manager will return records for all statuses (ie - records are ONLY filtered OUT if one or more values are checked)
                lstMigrationStatuses = null;
            }


            // Order Address Types
            List<string> lstOrderAddressTypes;
            if (ddlOrderAddressTypes.CheckedItems.Count() > 0)
            {
                lstOrderAddressTypes = new List<string>();
                lstOrderAddressTypes.AddRange(ddlOrderAddressTypes.CheckedItems.ToList().Select(ddl => ddl.Value.ToString()).ToList());
            }
            else
            {
                // Set to null, so that the search manager will return records for all trans types (ie - records are ONLY filtered OUT if one or more values are checked)
                lstOrderAddressTypes = null;
            }

            // Order Date
            DateTime? dteOrderAddressDate_Start = dteOrderDateStart.SelectedDate;
            DateTime? dteOrderAddressDate_End = dteOrderDateEnd.SelectedDate;


            // Date Created
            DateTime? dteDateCreated_Start = dteDateCreatedStart.SelectedDate;
            DateTime? dteDateCreated_End = dteDateCreatedEnd.SelectedDate;


            // Customer Order Number
            string strCustomerOrderNumber = txtCustomerOrderNumber.Text.Trim().ToUpper();

            // Address Line 1
            string strAddressLine1 = txtAddressLine1.Text.Trim().ToUpper();


            OrderAddressSearchControlState objOrderAddressSearchControlState = new OrderAddressSearchControlState();
            objOrderAddressSearchControlState.OrderSystemOfRecords = lstOrderSystemsOfRecord;
            objOrderAddressSearchControlState.MigrationStatuses = lstMigrationStatuses;
            objOrderAddressSearchControlState.OrderAddressTypes = lstOrderAddressTypes;
            objOrderAddressSearchControlState.OrderDate_Start = dteOrderAddressDate_Start;
            objOrderAddressSearchControlState.OrderDate_End = dteOrderAddressDate_End;
            objOrderAddressSearchControlState.DateCreated_Start = dteDateCreated_Start;
            objOrderAddressSearchControlState.DateCreated_End = dteDateCreated_End;
            objOrderAddressSearchControlState.CustomerOrderNumber = strCustomerOrderNumber;
            objOrderAddressSearchControlState.AddressLine1 = strAddressLine1;

            OrderAddressSearchManager objOrderAddressSearchManager = new OrderAddressSearchManager();
            if (objOrderAddressSearchManager.Load(objOrderAddressSearchControlState))
            {
                // Response Redirect or load the datagrid
                lblWhereClause.InnerText = objOrderAddressSearchManager.GetWhereClause() + " # Records = [" + objOrderAddressSearchManager.SearchResults.Count() + "]";
                string strWhereClauseAsBase64 = objOrderAddressSearchManager.GetWhereClauseBase64Encoded();
                string strJsonOrderAddressStateAsBase64 = objOrderAddressSearchManager.GetOrderAddressStateAsBase64EncodedJsonString();
                string strQSParam = Server.UrlEncode(strWhereClauseAsBase64);
                string strQSParamJson = Server.UrlEncode(strJsonOrderAddressStateAsBase64);
                string strURL = HttpContext.Current.Request.Url.AbsoluteUri;

                if (strURL.Contains('?'))
                {
                    string[] arrURLSplitFromQS = strURL.Split('?');
                    NameValueCollection nvcQueryString = System.Web.HttpUtility.ParseQueryString(arrURLSplitFromQS[1]);
                    nvcQueryString[_strQSParamName] = strQSParam;
                    nvcQueryString[_strQSParamNameJsonState] = strQSParamJson;
                    strURL = arrURLSplitFromQS[0] + "?" + nvcQueryString.ToString();
                }
                else
                {
                    string strBaseUrl = strURL;
                    NameValueCollection nvcQueryString = System.Web.HttpUtility.ParseQueryString(strBaseUrl);
                    strURL = strURL + "?" + _strQSParamName + "=" + strQSParam + "&" + _strQSParamNameJsonState + "=" + strQSParamJson;
                }

                Response.Redirect(strURL);
            }
            else
            {
                lblError.InnerText = String.Concat("There was an error while trying to search the payments.  ", String.Join(",", objOrderAddressSearchManager.ErrorMessages.ToArray()), String.Join(",", objOrderAddressSearchManager.ValidationErrors.ToArray()));
            }
        }
        protected void lnkReset_Click(object sender, EventArgs e)
        {
            //ResetSearchForm();
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            if (url.Contains('?'))
            {
                string[] separateURL = url.Split('?');
                url = separateURL[0];
            }

            Response.Redirect(url);
        }

        // Custom Methods
        public void ResetSearchForm()
        {
            ScriptManager.GetCurrent(this.Page).SetFocus(txtCustomerOrderNumber);

            // Clear the DDL selections
            ddlOrderSystemOfRecords.ClearCheckedItems();
            ddlMigrationStatuses.ClearCheckedItems();
            ddlOrderAddressTypes.ClearCheckedItems();

            // Clear the text entry fields
            txtCustomerOrderNumber.Attributes.Add("placeholder", "ex: 1077770/2");
            txtCustomerOrderNumber.Text = String.Empty;

            txtAddressLine1.Attributes.Add("placeholder", "ex: 17 PARKWAY DR");
            txtAddressLine1.Text = String.Empty;

            // Clear the calendars
            dteOrderDateStart.Clear();
            dteOrderDateEnd.Clear();

            dteDateCreatedStart.Clear();
            dteDateCreatedEnd.Clear();

            // Clear the Error Message
            lblError.InnerText = String.Empty;

            // Clear the results grid
            //BindGrid();

        }

        public void LoadDDL_OrderSystemOfRecord()
        {
            string strErrorMessage = String.Empty;
            List<OrderSystemOfRecord> lstSORs = new DAL.OrderSystemOfRecord().GetAllRecords(out strErrorMessage);

            // Iterate over the records and add a corresponding item to the drop down list
            for (int i = 0; i < lstSORs.Count; i++)
            {
                RadComboBoxItem objItem = new RadComboBoxItem();
                objItem.Text = lstSORs[i].OrderSystemOfRecordDesc;
                objItem.Value = lstSORs[i].OrderSystemOfRecordID.ToString();
                ddlOrderSystemOfRecords.Items.Add(objItem);
            }
        }
        public void LoadDDL_MigrationStatus()
        {
            string strErrorMessage = String.Empty;
            List<MigrationStatus> lstOrderAddressStatuses = new MigrationStatus().GetAllRecords(out strErrorMessage);

            // Iterate over the records and add a corresponding item to the drop down list
            for (int i = 0; i < lstOrderAddressStatuses.Count; i++)
            {
                RadComboBoxItem objItem = new RadComboBoxItem();
                objItem.Text = lstOrderAddressStatuses[i].MigrationStatusDesc;
                objItem.Value = lstOrderAddressStatuses[i].MigrationStatusID.ToString();
                ddlMigrationStatuses.Items.Add(objItem);
            }
        }
        public void LoadDDL_OrderAddressTypes()
        {
            string strErrorMessage = String.Empty;

            List<DAL.OrderAddressType> lstOrderAddressTypes = new DAL.OrderAddressType().GetAllRecords(out strErrorMessage);

            // Iterate over the records and add a corresponding item to the drop down list
            for (int i = 0; i < lstOrderAddressTypes.Count; i++)
            {
                RadComboBoxItem objItem = new RadComboBoxItem();
                objItem.Text = lstOrderAddressTypes[i].OrderAddressTypeDesc;
                objItem.Value = lstOrderAddressTypes[i].OrderAddressTypeID.ToString();
                ddlOrderAddressTypes.Items.Add(objItem);
            }
        }
    }
}