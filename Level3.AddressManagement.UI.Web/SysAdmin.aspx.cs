using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Level3.AddressManagement.BLL;

namespace Level3.AddressManagement.UI.Web
{
    public partial class SysAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                RadGrid1.MasterTableView.NoMasterRecordsText = "ERROR READING CONFIG SETTINGS FROM THE DB.  PLEASE CONTACT I.T.";
                RadGrid1.DataSource = new UserConfigurableSystemConfigSettingHelper().GetAllSystemConfigSettings();
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = String.Concat("There was an ERROR while trying to retrieve the CONFIG SETTINGS from the DB.  Error Message = [", ex.Message, "]");
            }
        }

        protected void RadGrid2_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                RadGrid2.MasterTableView.NoMasterRecordsText = "ERROR READING SETTINGS FROM THE CONFIG FILE";
                DeveloperControlledConfigOptionDisplayHelper objDeveloperControlledConfigOptionDisplayHelp = new DeveloperControlledConfigOptionDisplayHelper();
                RadGrid2.DataSource = objDeveloperControlledConfigOptionDisplayHelp.GetListForDisplay();
            }
            catch (Exception ex)
            {
                lblErrorMessage2.Text = String.Concat("There was an ERROR while trying to retrieve the CONFIG SETTINGS from the CONFIG FILE.  Error Message = [", ex.Message, "]");
            }
        }
    }
}