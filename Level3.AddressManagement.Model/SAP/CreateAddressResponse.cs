using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level3.AddressManagement.Model.SAP.Responses
{
     public class CreateAddressResponse
    {
        public ET_RETURN[] ET_RETURN { get; set; }
        public PL_SL_CODE[] PL_SL_CODE { get; set; }
    }

    public class ET_RETURN
    {
        public string TYPE { get; set; }
        public string ID { get; set; }
        public string NUMBER { get; set; }
        public string MESSAGE { get; set; }
        public string LOG_NO { get; set; }
        public string LOG_MSG_NO { get; set; }
        public string MESSAGE_V1 { get; set; }
        public string MESSAGE_V2 { get; set; }
        public string MESSAGE_V3 { get; set; }
        public string MESSAGE_V4 { get; set; }
        public string DETAILED_MESSAGE { get; set; }
        public string PARAMETER { get; set; }
        public int ROW { get; set; }
        public string FIELD { get; set; }
        public string SYSTEM { get; set; }
    }

    public class PL_SL_CODE
    {
        public string PL_SL_ID { get; set; }
    }

}
