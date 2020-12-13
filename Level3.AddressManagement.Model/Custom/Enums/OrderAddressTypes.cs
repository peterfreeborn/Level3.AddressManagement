using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level3.AddressManagement.Model
{
    // EXECUTE THIS AGAINST THE DB TO PRODUCE THE ENUM VALUES SHOWN BELOW
    // SELECT CONCAT(REPLACE(OrderAddressTypeDesc,' ','_'),' = ', OrderAddressTypeID, ',' ) FROM tblOrderAddressType 
    public enum OrderAddressTypes
    {
        Site = 1,
        Service_Location = 2
    }
}
