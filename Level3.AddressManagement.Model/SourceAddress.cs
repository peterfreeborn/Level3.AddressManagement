using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level3.AddressManagement.Model
{
    public class SourceAddress
    {
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Floor { get; set; }
        public string Room { get; set; }
        public string Building { get; set; }

        // Constructor
        public SourceAddress()
        {

        }
    }
}
