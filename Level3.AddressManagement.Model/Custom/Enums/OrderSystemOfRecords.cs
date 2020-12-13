using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level3.AddressManagement.Model
{
    // EXECUTE THIS AGAINST THE DB TO PRODUCE THE ENUM VALUES SHOWN BELOW
    // SELECT CONCAT(REPLACE(OrderSystemOfRecordDesc,' ','_'),' = ', OrderSystemOfRecordID, ',' ) FROM tblOrderSystemOfRecord 

    public enum OrderSystemOfRecords
    {
        EON = 1,
        Pipeline = 2,
        Unknown = 3
    }
}
