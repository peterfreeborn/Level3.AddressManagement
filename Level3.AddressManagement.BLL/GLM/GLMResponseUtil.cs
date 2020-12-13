using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level3.AddressManagement.BLL
{
    public class GLMResponseUtil
    {

        public static bool TryGetPLNumberForCLII(string strCLII, List<Model.ResponseItem> lstResponseItems, out string strPLNumber, out string strErrorMessages)
        {
            // Initialize the return variables
            strPLNumber = String.Empty;
            strErrorMessages = String.Empty;

            try
            {
                // Iterate over the response items
                for (int i = 0; i < lstResponseItems?.Count; i++)
                {
                    // Iterate over each of the site codes for the current response item
                    for (int j = 0; j < lstResponseItems[i]?.SiteCodes?.Count(); j++)
                    {
                        string strCleansedSiteCode = CleanseGoofyXMLTag(lstResponseItems[i]?.SiteCodes[j]?.Trim().ToUpper(), "Strong");

                        // Check to see if the current site code is equal to the CLII provided by the caller
                        if (strCleansedSiteCode.Trim().ToUpper() == strCLII.Trim().ToUpper())
                        {
                            // It matches, so set the output return variable, and break out of this inner loop
                            strPLNumber = lstResponseItems[i].SiteId.Trim().ToUpper();
                            break;
                        }
                    }

                    // Check to see if we need to keep looping.  If we've found the PL Number, then we can quit
                    if (strPLNumber != String.Empty)
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                strErrorMessages = String.Format("There was an error while trying to inspect the GLM REST response items to find the corresping PL Number. Error Message =[{0}]", ex.Message);
            }

            return (strErrorMessages == String.Empty);

        }
        private static string CleanseGoofyXMLTag(string strValueToCleanse, string strTagElementName)
        {
            strValueToCleanse = strValueToCleanse?.ToUpper()?.Replace(String.Format("<{0}>", strTagElementName?.ToUpper()), "");
            strValueToCleanse = strValueToCleanse?.ToUpper()?.Replace(String.Format("</{0}>", strTagElementName?.ToUpper()), "");

            return strValueToCleanse;
        }




        public static bool TryGetPreferredSiteCodeForPLNumber(string strPLNumber, List<Model.ResponseItem> lstResponseItems, out string strPreferredSiteCode, out string strErrorMessages)
        {
            // Initialize the return variables
            strPreferredSiteCode = String.Empty;
            strErrorMessages = String.Empty;

            try
            {
                // Iterate over the response items
                for (int i = 0; i < lstResponseItems?.Count; i++)
                {
                    // Check to see if the current site code is equal to the CLII provided by the caller
                    if (lstResponseItems[i].SiteId?.Trim().ToUpper() == strPLNumber.Trim().ToUpper())
                    {
                        // It matches, so set the output return variable, and break out of this inner loop
                        strPreferredSiteCode = lstResponseItems[i].PreferredSiteCode;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                strErrorMessages = String.Format("There was an error while trying to inspect the GLM REST response items to find the corresping Preferred Site Code. Error Message =[{0}]", ex.Message);
            }

            return ((strErrorMessages == String.Empty));

        }
    }
}
