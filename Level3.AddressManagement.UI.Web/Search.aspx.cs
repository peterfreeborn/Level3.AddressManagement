﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Level3.AddressManagement.BLL;

namespace Level3.AddressManagement.UI.Web
{
    public partial class Search : System.Web.UI.Page
    {

        public OrderAddressListFilter OrderAddressListFilter { get; private set; }
        public OrderAddressListDateRangeFilterType OrderAddressListDateRangeFilterType { get; private set; }
        public DateTime? BaseDateForOrderAddressListFilter { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Set the values that the embedded user control will use to select the appropriate list selection criteria to use to build the dynamic PL-SQL used to retrieve the "current" order addresses form the DB
            OrderAddressListFilter = OrderAddressListFilter.Search;
            OrderAddressListDateRangeFilterType = OrderAddressListDateRangeFilterType.None;
            BaseDateForOrderAddressListFilter = null;
        }
    }
}