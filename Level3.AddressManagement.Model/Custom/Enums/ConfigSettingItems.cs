using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level3.AddressManagement.Model
{
    public enum SystemConfigItems
    {
        StatsEmailRecipientList = 1,
        SystemNotificationEmailRecipientList = 2,
        CDWQueryPreciseProductCodeListForInClause = 3,
        CDWQueryWildcardProductCodeListForLikeClauses = 4,
        CDWQuerySourceSystemCodesForInClause = 5,
        CDWQueryExcludedRegionsNotInClause = 6,
    }
}
