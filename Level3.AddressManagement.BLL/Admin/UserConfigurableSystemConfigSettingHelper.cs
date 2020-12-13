using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Level3.AddressManagement.DAL;
using Level3.AddressManagement.Model;

namespace Level3.AddressManagement.BLL
{
    public class UserConfigurableSystemConfigSettingHelper
    {

        /// <summary>
        /// Retrieves all the user editable config settings from the DB
        /// </summary>
        /// <returns></returns>
        public List<SystemConfigItem> GetAllSystemConfigSettings()
        {
            string strLastError = String.Empty;
            return new SystemConfigItem().GetAllSystemConfigItems(out strLastError);
        }


        /// <summary>
        /// Retrieves a single config setting from the DB
        /// </summary>
        /// <param name="enmUserConfigurableSystemConfigSetting"></param>
        /// <returns></returns>
        public SystemConfigItem GetConfigSetting(SystemConfigItems enmUserConfigurableSystemConfigSetting)
        {
            try
            {
                string strLastError = String.Empty;
                return new SystemConfigItem().Get((int)enmUserConfigurableSystemConfigSetting, out strLastError);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Concat("There was an error while trying to retrieve the Config Setting from the DB using its name. Config Setting = [", enmUserConfigurableSystemConfigSetting, "]  Error Message = [", ex.Message, "]"));
            }
        }


        /// <summary>
        /// Updates a single config setting in the DB
        /// </summary>
        /// <param name="intSystemConfigItemID"></param>
        /// <param name="enmUserConfigurableSystemConfigSetting"></param>
        /// <param name="strNewConfigValue"></param>
        /// <returns></returns>
        public bool UpdateConfigSetting(int intSystemConfigItemID, SystemConfigItems enmUserConfigurableSystemConfigSetting, string strNewConfigValue, out string strErrorMessage)
        {
            string strLastError = String.Empty;

            try
            {
                strErrorMessage = String.Empty;

                // Make sure that there was a non-empty-string value supplied
                if (String.IsNullOrEmpty(strNewConfigValue.Trim()))
                {
                    throw new Exception("A non empty string or blank value must be supplied as the new config setting value.");
                }

                // Get the corresponding Config Setting from the DB for update
                SystemConfigItem objSystemConfigItem = GetConfigSetting(enmUserConfigurableSystemConfigSetting);

                // Check to make sure that the record retrieved with the name matches the ID supplied
                if (objSystemConfigItem.SystemConfigItemID != intSystemConfigItemID)
                {
                    throw new Exception(String.Concat("The value supplied for the SystemConfigItemID does not correspond to the setting type specified. Config Setting =[", enmUserConfigurableSystemConfigSetting, "], SystemConfigItemID supplied = [", "], Actual SystemConfigItemID= [", objSystemConfigItem.SystemConfigItemID.ToString(), "]"));
                }

                // Set the new value
                objSystemConfigItem.ConfigSettingValue = strNewConfigValue.Trim();
                objSystemConfigItem.DateUpdated = DateTime.Now;

                // Update the record
                if (objSystemConfigItem.UpdateOptimistic(out strErrorMessage) == false)
                {
                    throw new Exception(String.Concat("The System Config Setting/Item could NOT be updated with the new value.  There was an unexpected error.  Config Setting = [", enmUserConfigurableSystemConfigSetting, "], Value that failed to update = [", strNewConfigValue, "]"));
                }

                return true;
            }
            catch (Exception ex)
            {
                strErrorMessage = ex.Message + strLastError;
                return false;
            }
        }

    }
}
