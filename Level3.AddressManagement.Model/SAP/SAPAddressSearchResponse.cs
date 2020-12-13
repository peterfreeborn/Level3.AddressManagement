using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level3.AddressManagement.Model
{

    public class SAPAddressSearchResponse
    {
        public ITAB[] ITAB { get; set; }
    }

    public class ITAB
    {
        public string ADDRNUMBER { get; set; }
        public string SORT1 { get; set; }
        public string SORT2 { get; set; }
        public string NAME1 { get; set; }
        public string NAME2 { get; set; }
        public string NAME3 { get; set; }
        public string MC_STREET { get; set; }
        public string MC_CITY1 { get; set; }
        public string REGION { get; set; }
        public string POST_CODE1 { get; set; }
        public string TELNR { get; set; }
        public string STR_SUPPL3 { get; set; }
        public string LOCATION { get; set; }
        public string COUNTRY { get; set; }

        // ERROR PROPERTIES
        public string ERROR { get; set; }
        public string MESSAGE { get; set; }
    }
          


}
