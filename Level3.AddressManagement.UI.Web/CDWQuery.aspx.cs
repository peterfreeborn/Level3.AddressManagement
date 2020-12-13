using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Level3.AddressManagement.BLL;

namespace Level3.AddressManagement.UI.Web
{
    public partial class CDWQuery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadQuery();
        }

        private void LoadQuery()
        {
            DateTime dteDateTimeCutoff = DateTime.Now;

            string strSQL = DynamicQueryBuilder.GetDynamicManagedServiceOrdersQuery(dteDateTimeCutoff, dteDateTimeCutoff, dteDateTimeCutoff, dteDateTimeCutoff);
            strSQL = strSQL.Replace(Environment.NewLine, "<br/>");
            lblQuery.Text = strSQL;
        }
    }
}