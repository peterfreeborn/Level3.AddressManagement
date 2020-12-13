using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Telerik.Web.UI;
using Level3.AddressManagement.BLL;
using System.Collections.Specialized;

namespace Level3.AddressManagement.UI.Web
{
    public partial class OrderAddressListViewControl : System.Web.UI.UserControl
    {

        string _strQSParamName = OrderAddressSearchControlStateUtil.GetQueryStringParamNameWhereClause();
        string _strEncodedSearchQuery;
        string _strWhereClauseDecoded;
        string _strQSParamName_ReferringURL = NavigationUtil.GetReferringURLVariableName();

        string _strQSParamName_pageNum = NavigationUtil.GetPageNumberVariableName();
        string _strQSParamName_pageSize = NavigationUtil.GetPageSizeVariableName();

        string _strQSParamValue_PageNumber;
        string _strQSParamValue_PageSize;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString[_strQSParamName]) == false)
            {
                _strEncodedSearchQuery = Request.QueryString[_strQSParamName];
                DecodeQSParam();
            }



            if (!(Page.IsPostBack))
            {
                // If this is a clean, full page load
                if (SetPaging())
                {
                    RadGrid1.CurrentPageIndex = int.Parse(_strQSParamValue_PageNumber);
                    RadGrid1.PageSize = int.Parse(_strQSParamValue_PageSize);
                }
            }
        }

        private void DecodeQSParam()
        {
            if (string.IsNullOrEmpty(_strEncodedSearchQuery) == false)
            {
                byte[] encodedDataAsBytes = System.Convert.FromBase64String(Server.UrlDecode(_strEncodedSearchQuery));
                _strWhereClauseDecoded = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
            }
        }


        // Data Grid
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid1.MasterTableView.NoMasterRecordsText = "No Records Found";

            DateTime? dteCurrentOrders_Filter_StartDate = null;
            DateTime? dteCurrentOrders_Filter_EndDate = null;

            // Get the base date value taht's used for date range driven lists.  Get the value from the asPx page that holds this user control (this control is shared)
            if ((this.Page as dynamic).BaseDateForOrderAddressListFilter != null)
            {
                // This routine converts to CST internally, so we should supply UTC here....
                if (OrderDateRangeCalculator.CalcCurrentDateRangeDate((this.Page as dynamic).BaseDateForOrderAddressListFilter, out dteCurrentOrders_Filter_StartDate, out dteCurrentOrders_Filter_EndDate) == false)
                {
                    lblErrorMessage.Text += "The 'Current Orders' date range could NOT be calculated, and so the order address list cannot be loaded.";
                    return;
                }
            }

            // Set the datagrid paging properties if this is NOT a postback and if the appropriate values appear in the querystring
            int? intPageNumber; // Add one to our zero based index since the underlying proc expects a 1-based index
            int? intPageSize;

            // If this is a clean, full page load
            if (SetPaging())
            {
                if (string.IsNullOrEmpty(_strQSParamValue_PageNumber) == false)
                {
                    RadGrid1.CurrentPageIndex = int.Parse(_strQSParamValue_PageNumber);
                    RadGrid1.PageSize = int.Parse(_strQSParamValue_PageSize);
                }
            }

            intPageNumber = RadGrid1.CurrentPageIndex + 1; // Add one to our zero based index since the underlying proc expects a 1-based index
            intPageSize = RadGrid1.PageSize;

            OrderAddressListLoader objOrderAddressListLoader = new OrderAddressListLoader();
            if (objOrderAddressListLoader.Load((this.Page as dynamic).OrderAddressListFilter, (this.Page as dynamic).OrderAddressListDateRangeFilterType, dteCurrentOrders_Filter_StartDate, dteCurrentOrders_Filter_EndDate, _strWhereClauseDecoded, intPageNumber, intPageSize))
            {
                RadGrid1.DataSource = objOrderAddressListLoader.OrderAddresses;
                RadGrid1.VirtualItemCount = objOrderAddressListLoader.TotalRecordCount;
            }
            else
            {
                // Display an error message
                lblErrorMessage.Text = String.Concat("There was an ERROR while trying to retrieve the payment list from the DB.  Error Message = [", String.Join(",", objOrderAddressListLoader.ErrorMessages.ToArray()), "]");
            }

            string strListHeaderText = String.Empty;
            OrderAddressListFilter objPaymentListFilter = (this.Page as dynamic).OrderAddressListFilter; // Get the value from the aspx that holds this user control, which determines some of the variables that go into determining which records to load into the list
            switch (objPaymentListFilter)
            {
                case OrderAddressListFilter.Current:
                    strListHeaderText = "Recent Order Addresses (ORDER DATE within the Last 2 weeks;  All Statuses)";
                    break;
                case OrderAddressListFilter.Issues:
                    strListHeaderText = "All Order Addresses that are FAILING and/or that NEED USER INTERVENTION";
                    break;
                case OrderAddressListFilter.AllNonIssuesInWorkflow:
                    strListHeaderText = "All Order Addresses Moving through the Migration Workflow (Non Issues; 'OK' Orders Accross all time)";
                    break;
                case OrderAddressListFilter.Search:
                    strListHeaderText = "Search Results";
                    break;
                default:
                    throw new Exception("Unexpected case encountered during the execution of a switch statement.  Did the developer add a list filter type but forget to add a case for the type?  Please have a developer investigate this issue.");
            }

            lblListTitle.InnerText = strListHeaderText;
        }

        private bool SetPaging()
        {
            bool blnUsePagingValues = false;

            // This means the user clicked something on the page... potentially the paging control itself to adjust the page number or size... so we let this event happen without updating the grid
            if (Page.IsPostBack)
            {
                return false;
            }

            // Page Number
            if (Request.QueryString[_strQSParamName_pageNum] != null)
            {
                _strQSParamValue_PageNumber = Request.QueryString[_strQSParamName_pageNum];
                blnUsePagingValues = true;
            }

            // Page Size
            if (Request.QueryString[_strQSParamName_pageSize] != null)
            {
                _strQSParamValue_PageSize = Request.QueryString[_strQSParamName_pageSize];
                blnUsePagingValues = true;
            }

            return blnUsePagingValues;
        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                // Add a QS variable to hold the referring page URL so that the 'back' button on the detail page will know from whence it came :)
                GridDataItem objItem = (GridDataItem)e.Item;

                //string strGoToLinkText = objItem["GoToLink"].Text;
                HyperLink objGoToHyperlink = (HyperLink)objItem["GoToLink"].Controls[0];

                // The base url for the link is built using the out-of-the box format and data function built into the telerik grid
                string strCurrentNavigateUrl = objGoToHyperlink.NavigateUrl;

                // Then we manually append additional querystring params to the base link to add the referring url w/paging info
                objGoToHyperlink.NavigateUrl = CaculateDetailLinkUrl(strCurrentNavigateUrl);

                //objGoToHyperlink.NavigateUrl = String.Concat(strCurrentNavigateUrl, "&", _strQSParamName_ReferringURL, "=", _strEncodedQSValue_ReferringUrl, "&", _strQSParamName_pageNum, "=", RadGrid1.CurrentPageIndex.ToString(), "&", _strQSParamName_pageSize, "=", RadGrid1.PageSize.ToString());
                //objGoToHyperlink.NavigateUrl = String.Concat(strCurrentNavigateUrl, "&", _strQSParamName_ReferringURL, "=", _strEncodedQSValue_ReferringUrl);
            }
        }

        private string CaculateDetailLinkUrl(string strBaseNavigateUrl)
        {
            string strEncoded_ReferringUrl = String.Empty;

            // In case the current url already contains paging variables, remove the variables so they can be recalculated based on the current properties of the datagrid
            string strCurrentUrl = CleanseUrlOfPagingAndReferringUrlVariables(HttpContext.Current.Request.Url.AbsoluteUri);

            // Add the paging variables to the referring url string
            string strReferringUrlWithPaging = AppendPagingAndReferringUrlVariablesToBaseUrl(strCurrentUrl);

            // Base 64 encode the url
            string strBase64EncodedQSValue_ReferringUrl = NavigationUtil.EncodeStringAsBase64(strReferringUrlWithPaging);

            // URL encode the values to make it querystring safe
            string strURLEncodedQSValue_ReferringUrl = Server.UrlEncode(strBase64EncodedQSValue_ReferringUrl);

            // We know that the incoming URL already has a '?' with the paymentID as an existing QS param, so here we add things with an '&'
            strEncoded_ReferringUrl = String.Concat(strBaseNavigateUrl, "&", _strQSParamName_ReferringURL, "=", strURLEncodedQSValue_ReferringUrl);

            return strEncoded_ReferringUrl;
        }

        private string CleanseUrlOfPagingAndReferringUrlVariables(string strAbsoluteURI)
        {
            string strReturnString = String.Empty;
            NameValueCollection nvcQueryString;
            string strBaseURL;

            if (strAbsoluteURI.Contains('?'))
            {
                string[] arrURLSplitFromQS = strAbsoluteURI.Split('?');
                nvcQueryString = System.Web.HttpUtility.ParseQueryString(arrURLSplitFromQS[1]);
                strBaseURL = arrURLSplitFromQS[0];
            }
            else
            {
                strBaseURL = strAbsoluteURI;
                nvcQueryString = System.Web.HttpUtility.ParseQueryString("");
            }

            // PAGE NUMBER
            if (nvcQueryString[_strQSParamName_pageNum] != null)
            {
                // Set the page number
                _strQSParamValue_PageNumber = nvcQueryString[_strQSParamName_pageNum];

                // Remove the value
                nvcQueryString.Remove(_strQSParamName_pageNum);
            }

            // PAGE SIZE
            if (nvcQueryString[_strQSParamName_pageSize] != null)
            {
                // Set the Page Size
                _strQSParamValue_PageSize = nvcQueryString[_strQSParamName_pageSize];
                // Remove the value
                nvcQueryString.Remove(_strQSParamName_pageSize);
            }

            // REFERRING URL
            if (nvcQueryString[_strQSParamName_ReferringURL] != null)
            {
                // Remove the value
                nvcQueryString.Remove(_strQSParamName_ReferringURL);
            }

            // Set the return variable
            strReturnString = strBaseURL;

            // Add back any querystring params that remain and/or were added
            if (nvcQueryString.Count > 0)
            {
                strReturnString = strReturnString + "?" + System.Web.HttpUtility.ParseQueryString(nvcQueryString.ToString());
            }

            return strReturnString;
        }

        private string AppendPagingAndReferringUrlVariablesToBaseUrl(string strCurrentURI)
        {
            string strReturnString = String.Empty;
            NameValueCollection nvcQueryString;
            string strBaseURL;

            if (strCurrentURI.Contains('?'))
            {
                string[] arrURLSplitFromQS = strCurrentURI.Split('?');
                nvcQueryString = System.Web.HttpUtility.ParseQueryString(arrURLSplitFromQS[1]);
                strBaseURL = arrURLSplitFromQS[0];
            }
            else
            {
                strBaseURL = strCurrentURI;
                nvcQueryString = System.Web.HttpUtility.ParseQueryString("");
            }

            // PAGE NUMBER
            if (nvcQueryString[_strQSParamName_pageNum] != null)
            {
                // Cahnge the page number value
                nvcQueryString[_strQSParamName_pageNum] = RadGrid1.CurrentPageIndex.ToString();
            }
            else
            {
                // Add the param and its value
                nvcQueryString.Set(_strQSParamName_pageNum, RadGrid1.CurrentPageIndex.ToString());
            }

            // PAGE SIZE
            if (nvcQueryString[_strQSParamName_pageSize] != null)
            {
                // Set the Page Size
                nvcQueryString[_strQSParamName_pageSize] = RadGrid1.PageSize.ToString();
            }
            else
            {
                // Add the param and its value
                nvcQueryString.Set(_strQSParamName_pageSize, RadGrid1.PageSize.ToString());
            }

            strReturnString = strBaseURL + "?" + System.Web.HttpUtility.ParseQueryString(nvcQueryString.ToString());
            return strReturnString;
        }

        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.ExportToExcelCommandName)
            {
                RadGrid1.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
                RadGrid1.ExportSettings.IgnorePaging = true;
                RadGrid1.ExportSettings.ExportOnlyData = true;
                RadGrid1.ExportSettings.OpenInNewWindow = true;

                RadGrid1.ExportSettings.FileName = String.Concat("OrderAddress_Record_Export_", DateTime.Now.ToString("MMM_dd_yyyy_HH_MM_ss"));
            }

        }

    }
}