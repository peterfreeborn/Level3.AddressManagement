using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level3.AddressManagement.Model
{
    public class DeveloperControlledConfigOption
    {
        public string Name { get; set; }
        public string Value { get; set; }



        public DeveloperControlledConfigOption()
        {

        }
        public DeveloperControlledConfigOption(string strName, string strValue)
        {
            Name = strName;
            Value = strValue;
        }
    }
}
