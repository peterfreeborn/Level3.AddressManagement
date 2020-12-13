using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level3.AddressManagement.BLL
{
    public enum OrderAddressListDateRangeFilterType
    {
        OrderDate_StartEnd,
        OrderDate_End,
        CreatedOrModifiedDate_StartEnd,
        CreatedOrModifiedDeadline_End,
        None

    }
}
