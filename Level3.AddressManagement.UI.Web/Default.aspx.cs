using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Level3.AddressManagement.UI.Web
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SetDocumentationLinks();
            }
            
        }

        private void SetDocumentationLinks()
        {
            try
            {
                string strBaseFileName = "Address_Management_High_Level_Design"; // This reference the file in AppData, which gets published to the web server with the code

                lnkPDF.NavigateUrl = String.Concat(lnkPDF.NavigateUrl, strBaseFileName, ".pdf");
            }
            catch (Exception ex)
            {
                // Eat any error

            }
        }
    }
}