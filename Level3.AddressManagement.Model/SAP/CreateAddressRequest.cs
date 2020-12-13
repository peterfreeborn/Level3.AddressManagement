using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level3.AddressManagement.Model.SAP.Requests
{

    public class CreateAddressRequest
    {
        public PL_SL_CODE[] PL_SL_CODE { get; set; }
    }

    public class PL_SL_CODE
    {
        public string PL_SL_ID { get; set; }
    }



}
