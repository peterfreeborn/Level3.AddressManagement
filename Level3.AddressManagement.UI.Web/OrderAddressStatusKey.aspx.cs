using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Level3.AddressManagement.DAL;

namespace Level3.AddressManagement.UI.Web
{
    public partial class OrderAddressStatusKey : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            string strLastError = String.Empty;

            try
            {
                RadGrid1.MasterTableView.NoMasterRecordsText = "ERROR READING DATA FROM THE DB.  PLEASE CONTACT I.T.";
                RadGrid1.DataSource = new MigrationStatus().GetAllRecords(out strLastError);
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = String.Concat("There was an ERROR while trying to retrieve the STATUSES from the DB.  Error Message = [", ex.Message, " - ", strLastError, "]");
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
        }

    }
}