using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Level3.AddressManagement.Model;
using Newtonsoft.Json;

namespace Level3.AddressManagement.BLL
{
    public static class OrderAddressSearchControlStateUtil
    {
        public static OrderAddressSearchControlState Deserialize(string strJSON)
        {
            return JsonConvert.DeserializeObject<OrderAddressSearchControlState>(strJSON);
        }

        public static string GetQueryStringParamNameWhereClause()
        {
            return "searchQuery";
        }

        public static string GetQueryStringParamNameJsonSearchControlState()
        {
            return "searchState";
        }
    }
}