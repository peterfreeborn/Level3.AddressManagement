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

namespace Level3.AddressManagement.UI.Web
{
    public partial class APICallLog : System.Web.UI.Page
    {
        // log4net logging object
        private static ILog _objLogger = LogManager.GetLogger(typeof(APICallLog));
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // Data Grid
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid1.MasterTableView.NoMasterRecordsText = "No Records Found";

            APICallLogItemListLoader objAPICallLogItemListLoader = new APICallLogItemListLoader();
            if (objAPICallLogItemListLoader.Load())
            {
                RadGrid1.DataSource = objAPICallLogItemListLoader.APICallLogItems;
            }
            else
            {
                // Display an error message
                lblErrorMessage.Text = String.Concat("There was an ERROR while trying to retrieve the API Call list from the DB.  Error Message = [", String.Join(",", objAPICallLogItemListLoader.ErrorMessages.ToArray()), "]");
            }


            lblListTitle.InnerText = "API Calls made in the last 7 Days...(sorted as most recent to oldest)";
        }
    }
}