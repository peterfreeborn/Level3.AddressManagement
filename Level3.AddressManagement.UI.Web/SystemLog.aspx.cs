using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Level3.AddressManagement.BLL;

namespace Level3.AddressManagement.UI.Web
{
    public partial class SystemLog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        // Data Grid
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid1.MasterTableView.NoMasterRecordsText = "No Records Found";

            SystemLogItemListLoader objSystemLogItemListLoader = new SystemLogItemListLoader();
            if (objSystemLogItemListLoader.Load())
            {
                RadGrid1.DataSource = objSystemLogItemListLoader.SystemLogItems;
            }
            else
            {
                // Display an error message
                lblErrorMessage.Text = String.Concat("There was an ERROR while trying to retrieve the payment list from the DB.  Error Message = [", String.Join(",", objSystemLogItemListLoader.ErrorMessages.ToArray()), "]");
            }



            lblListTitle.InnerText = "System Log Messages for the last 7 Days...(sorted as most recent to oldest)";
        }
    }
}