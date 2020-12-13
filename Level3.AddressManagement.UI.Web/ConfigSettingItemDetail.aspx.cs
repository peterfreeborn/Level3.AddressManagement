
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Level3.AddressManagement.BLL;
using Level3.AddressManagement.DAL;
using log4net;

namespace Level3.AddressManagement.UI.Web
{
    public partial class ConfigSettingItemDetail : System.Web.UI.Page
    {
        // Declare the static logger object that will be used to implement logging via log4net
        private static ILog _objLogger = LogManager.GetLogger(typeof(ConfigSettingItemDetail));


        // Private Member Variables
        private int _intSystemConfigSettingID;
        private bool _blnEditMode = false;
        const string _strEditMode = "edit";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (int.TryParse(Request.QueryString["settingId"], out _intSystemConfigSettingID) == false)
            {
                lblErrorMessage.Text = String.Concat("The Config Setting ID was not recognized.  Please try to navigate to this page again.  If the issue persists, please contact I.T.");
            }

            _blnEditMode = Request.QueryString["mode"] == _strEditMode;

            if (!Page.IsPostBack)
            {
                LoadDetailPane();
            }
        }


        private void LoadDetailPane()
        {
            try
            {
                SystemConfigItem objSystemConfigItem = new UserConfigurableSystemConfigSettingHelper().GetConfigSetting((Model.SystemConfigItems)_intSystemConfigSettingID);

                if (objSystemConfigItem == null)
                {
                    throw new Exception("The SystemConfigSettingID was NOT recognized!");
                }

                lblHeader.InnerText = objSystemConfigItem.ConfigSettingName;
                lblConfigSettingName.InnerText = objSystemConfigItem.ConfigSettingName;

                if (_blnEditMode)
                {
                    lblConfigSettingValue.Visible = false;
                    txtConfigSettingValue.Visible = true;
                    txtConfigSettingValue.Text = objSystemConfigItem.ConfigSettingValue;
                    lnkEdit.Visible = false;
                    lnkSave.Visible = true;
                    lnkCancel.Visible = true;
                }
                else
                {
                    txtConfigSettingValue.Visible = false;
                    lblConfigSettingValue.Visible = true;
                    lblConfigSettingValue.InnerText = objSystemConfigItem.ConfigSettingValue;
                    lnkEdit.Visible = true;
                    lnkSave.Visible = false;
                    lnkCancel.Visible = false;
                }

                lblLastUpdateDate.InnerText = objSystemConfigItem.DateUpdated.Value.ToString("F");
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = String.Concat("There was an unexpected error while trying to load the setting details from the DB.  Error Message = [", ex.Message, "]"); ;
            }

        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri + "&mode=" + _strEditMode);
        }

        protected void lnkCancel_Click(object sender, EventArgs e)
        {
            lblErrorMessage.Text = String.Empty;
            RedirectBackToView();
        }



        protected void lnkSave_Click(object sender, EventArgs e)
        {
            try
            {
                lblErrorMessage.Text = String.Empty;

                // Validate the value
                string strNewVal = txtConfigSettingValue.Text.Trim();

                if (string.IsNullOrEmpty(strNewVal))
                {
                    lblErrorMessage.Text = "Please supply a valid value, and try again!";
                    return; // bail out here since the value supplied by the user is invalid
                }

                // Load the existing record from the DB
                SystemConfigItem objSystemConfigItem = new UserConfigurableSystemConfigSettingHelper().GetConfigSetting((Model.SystemConfigItems)_intSystemConfigSettingID);

                string strOldVal = objSystemConfigItem.ConfigSettingValue;

                objSystemConfigItem.ConfigSettingValue = txtConfigSettingValue.Text.Trim();
                objSystemConfigItem.DateUpdated = DateTime.Parse(lblLastUpdateDate.InnerText);

                string strErrorMessage;

                UserConfigurableSystemConfigSettingHelper objUserConfigurableSystemConfigSettingHelper = new UserConfigurableSystemConfigSettingHelper();
                if (objUserConfigurableSystemConfigSettingHelper.UpdateConfigSetting(objSystemConfigItem.SystemConfigItemID.Value, (Model.SystemConfigItems)objSystemConfigItem.SystemConfigItemID, strNewVal, out strErrorMessage) == false)
                {
                    lblErrorMessage.Text = String.Concat("The new setting value could not be saved to the DB!  ", strErrorMessage);
                }
                else
                {

                    // Add a system wide log messages
                    string strNote = String.Concat("The DB-driven ATI file Config Setting was just updated by a user.  Setting Name = [", objSystemConfigItem.ConfigSettingName, "], Old Value = [", strOldVal, "], New Value = [", strNewVal, "]");
                    AddSystemLog(strNote);

                    RedirectBackToView();
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = String.Concat("There was an error while trying to save the new value to the DB.  The value was not saved.  Error Message = [", ex.Message, "]");
            }
        }

        private void RedirectBackToView()
        {
            Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri.Replace(String.Concat("&mode=", _strEditMode), String.Empty));
        }

        private void AddSystemLog(string strLoggingString)
        {
            try
            {
                string strUsername = String.Empty;
                HttpContext objHttpContext = HttpContext.Current;
                if (objHttpContext != null && objHttpContext.User != null && objHttpContext.User.Identity.IsAuthenticated)
                {
                    strUsername = objHttpContext.User.Identity.Name;
                }

                strLoggingString = String.Concat(strLoggingString, " Windows Http Username/Context that Executed the Update = [", strUsername, "]");

                // Log the update to to the user-accessible system log table
                if (SystemLogItemUtil.InsertLogItem(strLoggingString, "A user just updated a DB-driven config setting"))
                {
                    // Log to the log file
                    _objLogger.Info(strLoggingString);

                }
                else
                {
                    _objLogger.Warn(String.Format("There was an error while trying to insert the system log item."));
                }

                
            }
            catch (Exception ex)
            {
                string strLoggingErrorMessage = String.Concat("There was an error while trying to log the config setting change.  ", strLoggingString, ", Error Message = ", ex.Message, "]");
                _objLogger.Error(strLoggingErrorMessage);

                // Eat the error by continuing since this only affects a log of the action that already completed
                return;
            }
        }
    }
}