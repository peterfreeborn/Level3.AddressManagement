using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Telerik.Web.UI;

using log4net;
using System.Collections.Specialized;

using Level3.AddressManagement.DAL;
using Level3.AddressManagement.BLL;
using System.Web.UI.HtmlControls;

namespace Level3.AddressManagement.UI.Web
{
    public partial class OrderAddressDetail : System.Web.UI.Page
    {
        // log4net logging object
        private static ILog _objLogger = LogManager.GetLogger(typeof(OrderAddressDetail));

        // Private Member Variables
        private int _intOrderAddressID;
        public List<OrderAddressLogItem> _lstOrderAddressLogItems;
        private DAL.OrderAddress _objOrderAddress;
        private OrderAddressStateEvaluator _objOrderAddressStateEvaluator;


        string _strQSParamName_ReferringURL = NavigationUtil.GetReferringURLVariableName();
        string _strQSParamValue_ReferringUrl;
        string _strDecoded_ReferringUrl;

        string _strQSParamName_pageNum = NavigationUtil.GetPageNumberVariableName();
        string _strQSParamName_pageSize = NavigationUtil.GetPageSizeVariableName();
        string _strQSParamValue_PageNumber;
        string _strQSParamValue_PageSize;


        protected void Page_Load(object sender, EventArgs e)
        {
            _lstOrderAddressLogItems = new List<OrderAddressLogItem>();
            _objOrderAddressStateEvaluator = new OrderAddressStateEvaluator();

            LoadOrderAddressInfo();

            // Payment ID
            if (int.TryParse(Request.QueryString["orderAddressId"], out _intOrderAddressID) == false)
            {
                lblErrorMessage.Text = String.Concat("The Order Address ID was not recognized.");
            }

            // URL of the page that was clicked to bring the user here
            if (Request.QueryString[_strQSParamName_ReferringURL] != null)
            {
                _strQSParamValue_ReferringUrl = Request.QueryString[_strQSParamName_ReferringURL];
                DecodeQSReferringUrlParam();

                btnBack.Text = "<< Back to List";
            }
            else
            {
                // This window was a popup, and so set the text on the button to CLOSE the windows, since there is no list to go back to
                btnBack.Text = "Close this Popup";
            }

        }

        private void DecodeQSReferringUrlParam()
        {
            if (string.IsNullOrEmpty(_strQSParamValue_ReferringUrl) == false)
            {
                // Decode the URL
                byte[] encodedDataAsBytes = System.Convert.FromBase64String(Server.UrlDecode(_strQSParamValue_ReferringUrl));
                _strDecoded_ReferringUrl = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);

                // Check to see if the url contains a QueryString
                if (_strDecoded_ReferringUrl.Contains('?'))
                {
                    string[] arrURLSplitFromQS = _strDecoded_ReferringUrl.Split('?');
                    NameValueCollection nvcQueryString = System.Web.HttpUtility.ParseQueryString(arrURLSplitFromQS[1]);

                    // Page Number
                    if (nvcQueryString[_strQSParamName_pageNum] != null)
                    {
                        _strQSParamValue_PageNumber = nvcQueryString[_strQSParamName_pageNum];
                    }

                    // Page Size
                    if (nvcQueryString[_strQSParamName_pageSize] != null)
                    {
                        // Remove the value
                        _strQSParamValue_PageSize = nvcQueryString[_strQSParamName_pageSize];
                    }
                }
            }
        }
        private void LoadOrderAddressInfo()
        {
            if (int.TryParse(Request.QueryString["orderAddressId"], out _intOrderAddressID))
            {
                // The value was successfulll retrieved from the Query String... so load the worflow payment record
                string strLastError = String.Empty;

                // Execute the query against CDW and load the results into the list of address records to be processed
                _objOrderAddress = new DAL.OrderAddress().Get(_intOrderAddressID, out strLastError);

                if (_objOrderAddress == null)
                {
                    lblErrorMessage.Text = String.Concat("There was an ERROR while trying to retrieve the order address record from the DB.  Error Message = [", strLastError, "]");
                }
                else
                {
                    LoadOrderAddressDetailInfo();
                    LoadCurrentStatusDetails();
                    LoadOrderAddressLogItems();
                    LoadOrderAddressStateEvaluator();
                    HideShowLinks();
                    SetAPIHistoryLink();
                }
            }
            else
            {
                // The value could NOT be retrieved from the Query String and/or it could not be parsed to an integer
                _intOrderAddressID = 0;
            }
        }
        private void LoadOrderAddressDetailInfo()
        {

            // Set the header value
            lblOrderAddressHeader.InnerText = String.Concat(_objOrderAddress.ServiceOrderNumber, " (", (Model.OrderSystemOfRecords)_objOrderAddress.OrderSystemOfRecordID, ")");

            // Set the ORDER INFO
            if (_objOrderAddress.FIRST_ORDER_CREATE_DT.HasValue)
            {
                lblOrderDate.InnerText = _objOrderAddress.FIRST_ORDER_CREATE_DT.Value.ToString("F");
            }

            lblCustomerOrderNumber.InnerText = _objOrderAddress.ServiceOrderNumber;


            // SET the Address Info
            lblAddressLineOne.InnerText = _objOrderAddress.CDWAddressOne;
            lblCity.InnerText = _objOrderAddress.CDWCity;
            lblState.InnerText = _objOrderAddress.CDWState;
            lblPostalCode.InnerText = _objOrderAddress.CDWPostalCode;
            lblCountry.InnerText = _objOrderAddress.CDWCountry;
            lblFloor.InnerText = _objOrderAddress.CDWFloor;
            lblRoom.InnerText = _objOrderAddress.CDWRoom;
            lblSuite.InnerText = _objOrderAddress.CDWSuite;

            // Other Info
            lblPLNumber.InnerText = _objOrderAddress.GLMPLNumber;
            lblSLNumber.InnerText = _objOrderAddress.GLMSLNumber;
            lblSiteCode.InnerText = _objOrderAddress.GLMSiteCode;
            lblSCode.InnerText = _objOrderAddress.GLMSCode;
            lblProcessingTime.InnerText = _objOrderAddress.TotalProcessingTimeAsHumanReadable;
            lblDateCreated.InnerText = _objOrderAddress.DateCreated.Value.ToString("F");
            lblDateUpdated.InnerText = _objOrderAddress.DateUpdated.Value.ToString("F");
            lblLastStatusUpdate.InnerText = _objOrderAddress.DateTimeOfLastMigrationStatusUpdate.Value.ToString("F");

            // Build the live link to GLM's UI for quick access
            if (String.IsNullOrEmpty(_objOrderAddress.GLMPLNumber) == false)
            {
                lnkGLMLookup.NavigateUrl = String.Format(@"{0}/GLMSWeb/Location/Details/{1}", BLL.ConfigHelper.GetGLMBaseUrl(), _objOrderAddress.GLMPLNumber);
                lnkGLMLookup.Text = lnkGLMLookup.NavigateUrl;
            }
        }

        private void LoadCurrentStatusDetails()
        {
            string strErrorMessage = String.Empty;

            if ((_objOrderAddress != null) && (_objOrderAddress.MigrationStatusID.HasValue))
            {
                MigrationStatus objMigrationStatus = new MigrationStatus().GetByPK(_objOrderAddress.MigrationStatusID.Value, out strErrorMessage);

                lblCurrentMigrationStatusDesc.InnerText = objMigrationStatus.MigrationStatusDesc;
                lblCurrentMigrationStatusDescExtended.InnerText = objMigrationStatus.MigrationStatusExtendedDescription;
            }
        }
        private void LoadOrderAddressStateEvaluator()
        {
            if (_objOrderAddressStateEvaluator.Load(_objOrderAddress) == false)
            {
                lblErrorMessage.Text = String.Concat("There was an ERROR while trying to evaluate the State of the Order Address for end user action link building on the UI.  Error Message = [", String.Join(",", _objOrderAddressStateEvaluator.ErrorMessages.ToArray()), "]");
            }
        }

        private void SetAPIHistoryLink()
        {
            string strNagivateUrl = String.Format("~/OrderAddressAPICallDetail?orderAddressId={0}", _objOrderAddress.OrderAddressID.ToString());
            lbtnAPICallHistory.NavigateUrl = strNagivateUrl;
        }


        // Link Button actions
        private void HideShowLinks()
        {
            lnkRetryAPICall.Visible = _objOrderAddressStateEvaluator.CanBeAPIRetriedByEndUser();
            lnkDontIgnore.Visible = _objOrderAddressStateEvaluator.CanBeRevertedByEndUser();
            lnkSetToIgnored.Visible = _objOrderAddressStateEvaluator.CanBeSetToIgnoredByEndUser();
            lnkForceNextStep.Visible = _objOrderAddressStateEvaluator.CanBeForcedToProcessByEndUser();
        }
        protected void lnkRetryAPICall_Click(object sender, EventArgs e)
        {
            BLL.OrderAddressProcessor objOrderAddressProcessor = new BLL.OrderAddressProcessor();
            if (objOrderAddressProcessor.Load(_objOrderAddress.OrderAddressID.Value) == false)
            {
                lblErrorMessage.Text += String.Concat("There was an error while trying to load the record from the database for processing.  Error Message = [", String.Join(",", objOrderAddressProcessor.ErrorMessages.ToArray()), "]");

                //logUserAction(false, "Load of the ATI File for subsequent tranmission to SAT via Indicium.");
                return;
            }

            if (objOrderAddressProcessor.Process(true, Page.User.Identity.Name) == false)
            {
                lblErrorMessage.Text += String.Concat("There was an error while trying to process the record. Error Message = [", String.Join(",", objOrderAddressProcessor.ErrorMessages.ToArray()), "]");
                //logUserAction(false, "ATI File tranmission to SAT via Indicium.");
            }
            else
            {
                //logUserAction(true, "ATI File tranmission to SAT via Indicium.");
                Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
            }
        }
        protected void lnkSetToIgnored_Click(object sender, EventArgs e)
        {
            // Bump the status
            BLL.OrderAddressProcessor objOrderAddressProcessor = new BLL.OrderAddressProcessor();
            if (objOrderAddressProcessor.Load(_objOrderAddress.OrderAddressID.Value) == false)
            {
                lblErrorMessage.Text += String.Concat("There was an error while trying to load the record from the database for processing.  Error Message = [", String.Join(",", objOrderAddressProcessor.ErrorMessages.ToArray()), "]");

                //logUserAction(false, "Load of the ATI File for subsequent tranmission to SAT via Indicium.");
                return;
            }

            if (objOrderAddressProcessor.AdjustStatus(Model.MigrationStatuses.IGNORED_indefinitely, true, Page.User.Identity.Name) == false)
            {
                lblErrorMessage.Text += String.Concat("There was an error while trying to adjust the migration status of the record. Error Message = [", String.Join(",", objOrderAddressProcessor.ErrorMessages.ToArray()), "]");
                //logUserAction(false, "ATI File tranmission to SAT via Indicium.");
            }
            else
            {
                //logUserAction(true, "ATI File tranmission to SAT via Indicium.");
                Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
            }
        }
        protected void lnkDontIgnore_Click(object sender, EventArgs e)
        {
            // Get the last status
            if (_lstOrderAddressLogItems.Count > 1)
            {
                Model.MigrationStatuses enmLastStatus = (Model.MigrationStatuses)_lstOrderAddressLogItems[1].MigrationStatusID;

                // Revert the status
                BLL.OrderAddressProcessor objOrderAddressProcessor = new BLL.OrderAddressProcessor();
                if (objOrderAddressProcessor.Load(_objOrderAddress.OrderAddressID.Value) == false)
                {
                    lblErrorMessage.Text += String.Concat("There was an error while trying to load the record from the database for processing.  Error Message = [", String.Join(",", objOrderAddressProcessor.ErrorMessages.ToArray()), "]");

                    //logUserAction(false, "Load of the ATI File for subsequent tranmission to SAT via Indicium.");
                    return;
                }

                if (objOrderAddressProcessor.AdjustStatus(enmLastStatus, true, Page.User.Identity.Name) == false)
                {
                    lblErrorMessage.Text += String.Concat("There was an error while trying to adjust the migration status on the record. Error Message = [", String.Join(",", objOrderAddressProcessor.ErrorMessages.ToArray()), "]");
                    //logUserAction(false, "ATI File tranmission to SAT via Indicium.");
                }
                else
                {
                    //logUserAction(true, "ATI File tranmission to SAT via Indicium.");
                    Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
                }

            }
            else
            {
                lblErrorMessage.Text += String.Concat("The record is not in a state that will allow for its status to be reverted.");
            }

        }
        protected void lnkForceNextStep_Click(object sender, EventArgs e)
        {
            BLL.OrderAddressProcessor objOrderAddressProcessor = new BLL.OrderAddressProcessor();
            if (objOrderAddressProcessor.Load(_objOrderAddress.OrderAddressID.Value) == false)
            {
                lblErrorMessage.Text += String.Concat("There was an error while trying to load the record from the database for processing.  Error Message = [", String.Join(",", objOrderAddressProcessor.ErrorMessages.ToArray()), "]");

                //logUserAction(false, "Load of the ATI File for subsequent tranmission to SAT via Indicium.");
                return;
            }

            if (objOrderAddressProcessor.Process(true, Page.User.Identity.Name) == false)
            {
                lblErrorMessage.Text += String.Concat("There was an error while trying to force the processing the record. Error Message = [", String.Join(",", objOrderAddressProcessor.ErrorMessages.ToArray()), "]");
                //logUserAction(false, "ATI File tranmission to SAT via Indicium.");
            }
            else
            {
                //logUserAction(true, "ATI File tranmission to SAT via Indicium.");
                Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
            }
        }


        protected void RadGridLogItems_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGridLogItems.MasterTableView.NoMasterRecordsText = "No Records Found";
            RadGridLogItems.DataSource = _lstOrderAddressLogItems;
        }

        private void LoadOrderAddressLogItems()
        {
            string strErrorMessage = String.Empty;
            _lstOrderAddressLogItems = new OrderAddressLogItem().GetByFK(_intOrderAddressID, out strErrorMessage);

            _lstOrderAddressLogItems = _lstOrderAddressLogItems?.OrderBy(i => i.DateCreated)?.Reverse()?.ToList();
        }

        // Back button click
        protected void btnBack_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(_strDecoded_ReferringUrl))
            {
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
            }
            else
            {
                // Redirect back to the page that was sent in the URL as an encoded QS param
                Response.Redirect(_strDecoded_ReferringUrl);
            }
          
        }
    }
}