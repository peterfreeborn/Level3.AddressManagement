using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level3.AddressManagement.BLL
{
    public class TranslateDW_SOURCE_SYSTEM_CDToOrderSystemOfRecord
    {

        public static Model.OrderSystemOfRecords Translate(string strDW_SOURCE_SYSTEM_CD)
        {
            switch (strDW_SOURCE_SYSTEM_CD?.ToUpper().Trim())
            {
                case "EON":
                    return Model.OrderSystemOfRecords.EON;
                case "PIPELINE":
                    return Model.OrderSystemOfRecords.Pipeline;
                default:
                    return Model.OrderSystemOfRecords.Unknown;
            }
        }

}
}
