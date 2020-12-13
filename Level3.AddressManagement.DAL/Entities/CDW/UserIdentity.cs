using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level3.AddressManagement.DAL
{
    public class UserIdentity
    {

        public int? UserID { get; set; }

        public int? ResponsibilityID { get; set; }

        public int? OperatingUnitID { get; set; }

        // Constructor
        public UserIdentity()
        {

        }

    }
}
