using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Level3.AddressManagement.Model;

namespace Level3.AddressManagement.BLL
{
    public class DeveloperControlledConfigOptionDisplayHelper
    {

        // Private Members
        private List<DeveloperControlledConfigOption> _lstDeveloperControlledConfigOptions;


        // Constructor
        public DeveloperControlledConfigOptionDisplayHelper()
        {
            _lstDeveloperControlledConfigOptions = new List<DeveloperControlledConfigOption>();
        }

        // Public Methods
        public List<DeveloperControlledConfigOption> GetListForDisplay()
        {
            PopulateConfigSettings();
            return _lstDeveloperControlledConfigOptions;
        }


        private void PopulateConfigSettings()
        {
            _lstDeveloperControlledConfigOptions.Add(new DeveloperControlledConfigOption("MinimumFirstOrderCreateDateForCDWPull", ConfigHelper.GetMinimumFirstOrderCreateDateForCDWPull().ToString("MMM dd yyyy")));
            _lstDeveloperControlledConfigOptions.Add(new DeveloperControlledConfigOption("GLMBaseUrl", ConfigHelper.GetGLMBaseUrl().ToString()));
            _lstDeveloperControlledConfigOptions.Add(new DeveloperControlledConfigOption("GLMAuthorizationHeaderUsername", ConfigHelper.GetGLMAuthorizationHeaderUsername().ToString()));
            _lstDeveloperControlledConfigOptions.Add(new DeveloperControlledConfigOption("GLMAuthorizationHeaderApplicationID", "*** (Ask Developer)"));
            _lstDeveloperControlledConfigOptions.Add(new DeveloperControlledConfigOption("SAPBaseUrl", ConfigHelper.GetSAPBaseUrl().ToString()));

        }
    }
}
