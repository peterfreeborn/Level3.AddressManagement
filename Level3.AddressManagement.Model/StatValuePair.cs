using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level3.AddressManagement.Model
{
    public class StatValuePair
    {

        public string StatDescriptor { get; set; }
        public string StatValue { get; set; }

        public StatValuePair()
        {

        }

        public StatValuePair(string strStatDescriptor, string strStatValue)
        {
            StatDescriptor = strStatDescriptor;
            StatValue = strStatValue;
        }
    }
}
