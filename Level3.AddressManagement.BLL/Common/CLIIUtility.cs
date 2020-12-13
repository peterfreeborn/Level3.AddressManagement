using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using log4net;

namespace Level3.AddressManagement.BLL
{
    public class CLIIUtility
    {
        // declare a log4net logger
        private static ILog _objLogger = LogManager.GetLogger(typeof(CLIIUtility));

        public static bool IsValidCLII(string strCLII)
        {

            try
            {
                // Check to make sure its not empty string or null
                if (String.IsNullOrEmpty(strCLII) || strCLII == null)
                {
                    return false;
                }

                // Check to see if the first letter is Z
                if (strCLII[0].ToString().ToUpper() == "Z")
                {
                    // Check to see if the next value is a number
                    int n;
                    bool isNumeric = int.TryParse(strCLII[1].ToString(), out n);

                    if (isNumeric)
                    {
                        return false;
                    }
                }

                // If we get here, it must be a valid CLII
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
