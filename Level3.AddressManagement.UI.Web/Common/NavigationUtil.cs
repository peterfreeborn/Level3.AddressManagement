using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Level3.AddressManagement.UI.Web
{
    public static class NavigationUtil
    {
        public static string GetReferringURLVariableName()
        {
            return "referringUrl";
        }

        public static string EncodeStringAsBase64(string strStringToEncode)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(strStringToEncode);
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        public static string GetPageNumberVariableName()
        {
            return "pageNum";
        }

        public static string GetPageSizeVariableName()
        {
            return "pageSize";
        }

    }
}