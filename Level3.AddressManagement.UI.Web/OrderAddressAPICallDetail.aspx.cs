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
    public partial class OrderAddressAPICallDetail : System.Web.UI.Page
    {
        // log4net logging object
        private static ILog _objLogger = LogManager.GetLogger(typeof(OrderAddressAPICallDetail));

        // Private Member Variables
        private int _intOrderAddressID;
        public List<APICallLogItem> _lstAPICallLogItems;

        protected void Page_Load(object sender, EventArgs e)
        {
            _lstAPICallLogItems = new List<APICallLogItem>();
            LoadData();
        }

        private void LoadData()
        {
            if (int.TryParse(Request.QueryString["orderAddressId"], out _intOrderAddressID))
            {
                // The value was successfulll retrieved from the Query String... so load the worflow payment record
                string strLastError = String.Empty;

                string strErrorMessage = String.Empty;
                _lstAPICallLogItems = new APICallLogItem().GetByFK(_intOrderAddressID, out strErrorMessage);

                _lstAPICallLogItems = _lstAPICallLogItems?.OrderBy(i => i.DateCreated)?.Reverse()?.ToList();
            }
            else
            {
                // The value could NOT be retrieved from the Query String and/or it could not be parsed to an integer
                _intOrderAddressID = 0;
            }
        }

        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid1.MasterTableView.NoMasterRecordsText = "No Records Found";
            RadGrid1.DataSource = _lstAPICallLogItems;
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
        }

    }
}