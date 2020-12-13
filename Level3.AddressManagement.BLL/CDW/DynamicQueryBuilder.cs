using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Level3.AddressManagement.DAL;

namespace Level3.AddressManagement.BLL
{
    public static class DynamicQueryBuilder
    {
        public static string GetDynamicManagedServiceOrdersQuery(DateTime dteMinimumFirstOrderCreateDate, DateTime dteDWLastModifyDate_OPE, DateTime dteDWLastModifyDate_PL, DateTime dteDWLastModifyDate_PS)
        {
            // Get all the config settings from the DB
            UserConfigurableSystemConfigSettingHelper objUserConfigurableSystemConfigSettingHelper = new UserConfigurableSystemConfigSettingHelper();
            List<SystemConfigItem> lstConfigSettings = objUserConfigurableSystemConfigSettingHelper.GetAllSystemConfigSettings();

            // Delcare a list for each of the sub-clauses appearing in the WHERE clause
            List<string> lstCDWQueryPreciseProductCodeListForInClause = new List<string>();
            List<string> lstCDWQueryWildcardProductCodeListForLikeClauses = new List<string>();
            List<string> lstCDWQuerySourceSystemCodesForInClause = new List<string>();
            List<string> lstCDWQueryExcludedRegionsNotInClause = new List<string>();
            List<string> lstCDWQueryCountryCodesForInClause = new List<string>();


            string strProductCodeListForInClause = String.Empty;
            string strProductCodeOrClauses = String.Empty;
            string strSourceSystemCodes = String.Empty;
            string strExcludedRegionCodes = String.Empty;
            string strIncludedCountryCodes = String.Empty;


            // Iterate over all the config settins from the DB, creating a list for each of the ones we need for our dynamic where clause
            for (int i = 0; i < lstConfigSettings.Count; i++)
            {
                string strConfigSettingValue = lstConfigSettings[i].ConfigSettingValue?.Trim();

                if (String.IsNullOrEmpty(strConfigSettingValue) == false)
                {
                    switch (lstConfigSettings[i].ConfigSettingName)
                    {
                        case "CDWQueryPreciseProductCodeListForInClause":

                            lstCDWQueryPreciseProductCodeListForInClause = lstConfigSettings[i].ConfigSettingValue.Split(',').ToList();
                            strProductCodeListForInClause = SQLQueryBuilder.CombineListItemsIntoDelimitedQuotedStrings(lstCDWQueryPreciseProductCodeListForInClause);
                            break;

                        case "CDWQueryWildcardProductCodeListForLikeClauses":

                            lstCDWQueryWildcardProductCodeListForLikeClauses = lstConfigSettings[i].ConfigSettingValue.Split(',').ToList();
                            strProductCodeOrClauses = SQLQueryBuilder.CombineListItemsIntoProductCodeLikeORClause(lstCDWQueryWildcardProductCodeListForLikeClauses);
                            break;

                        case "CDWQuerySourceSystemCodesForInClause":

                            lstCDWQuerySourceSystemCodesForInClause = lstConfigSettings[i].ConfigSettingValue.Split(',').ToList();
                            lstCDWQuerySourceSystemCodesForInClause = lstCDWQuerySourceSystemCodesForInClause.ConvertAll(d => d.ToUpper());
                            strSourceSystemCodes = SQLQueryBuilder.CombineListItemsIntoDelimitedQuotedStrings(lstCDWQuerySourceSystemCodesForInClause);
                            break;

                        case "CDWQueryExcludedRegionsNotInClause":

                            lstCDWQueryExcludedRegionsNotInClause = lstConfigSettings[i].ConfigSettingValue.Split(',').ToList();
                            lstCDWQueryExcludedRegionsNotInClause = lstCDWQueryExcludedRegionsNotInClause.ConvertAll(d => d.ToUpper());
                            strExcludedRegionCodes = SQLQueryBuilder.CombineListItemsIntoDelimitedQuotedStrings(lstCDWQueryExcludedRegionsNotInClause);
                            break;

                        case "CDWQueryIncludedCountryCodes":

                            lstCDWQueryCountryCodesForInClause = lstConfigSettings[i].ConfigSettingValue.Split(',').ToList();
                            lstCDWQueryCountryCodesForInClause = lstCDWQueryCountryCodesForInClause.ConvertAll(d => d.ToUpper());
                            strIncludedCountryCodes = SQLQueryBuilder.CombineListItemsIntoDelimitedQuotedStrings(lstCDWQueryCountryCodesForInClause);
                            break;

                        default:
                            break;
                    }
                }
            }

            string strSQL = SQLQueryBuilder.GetSQLStatement_DWOrderAddressesChangeset_DYNAMIC(dteMinimumFirstOrderCreateDate, dteDWLastModifyDate_OPE, dteDWLastModifyDate_PL, dteDWLastModifyDate_PS, strProductCodeListForInClause, strProductCodeOrClauses, strSourceSystemCodes, strExcludedRegionCodes, strIncludedCountryCodes);
            return strSQL;
        }

    }
}
