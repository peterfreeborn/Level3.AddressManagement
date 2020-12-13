using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level3.AddressManagement.DAL
{
    public class SQLQueryBuilder
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strMinimumFirstOrderCreateDate">as 'MM/DD/YYYY'</param>
        /// <returns></returns>
        public static string GetSQLStatement_DWOrderAddressesNotInGLM(string strMinimumFirstOrderCreateDate = "04/01/2018")
        {
            string strSQL = String.Format(@"

                            SELECT 
                                    COP.CUST_ORDER_NBR,
                                    CO.FIRST_ORDER_CREATE_DT, 
                                    COP.SERV_ORDER_NBR,
                                    COP.SERV_ORDER_STATUS_CD,
                                    COP.DW_SOURCE_SYSTEM_CD,
                                    PS.CLLI_CD, 
                                    PS.LINE1_ADDR,
                                    PS.CITY_NAME,
                                    PS.STATE_CD,
                                    PS.POSTAL_SHORT_CD,
                                    PS.COUNTRY_ISO_3_CD,
                                    PL.FLOOR_NAME,
                                    PL.ROOM_NAME,
                                    PL.SUITE_NAME

                            FROM 
                            
                                    CODS.CUSTOMER_ORDER co
                                        
                                        INNER JOIN
                                            CODS.CUSTOMER_ORDER_PRODUCT cop
                                            ON 
                                            co.CUST_ORDER_ODS_ID = cop.CUST_ORDER_ODS_ID
                                    
                                        INNER JOIN 
                                            CODS.ORDER_PRODUCT_COMPNT opc 
                                            ON 
                                            cop.CUST_ORDER_PRODUCT_ODS_ID = opc.CUST_ORDER_PRODUCT_ODS_ID
                                            AND 
                                                (
                                                 OPC.PRODUCT_CD IN ('BMGC046', 'BMGC093', 'BMGC019', 'BMGC020', 'BMGC034', 'BMGC038', 'BMGC018', 'BMGC028',  'BMGC090')
                                                 OR 
                                                 OPC.PRODUCT_CD LIKE 'gcms%'
                                                 OR 
                                                 (OPC.FRAMING_CD = 'ETHERNET' AND OPC.ONNET_IND = 'N') 
                                                )
                                        
                                        INNER JOIN 
                                            CODS.ORDER_PRODUCT_ENDPNT ope
                                            ON 
                                            cop.CUST_ORDER_PRODUCT_ODS_ID = ope.CUST_ORDER_PRODUCT_ODS_ID
                                            AND 
                                            UPPER(endpnt_role_typ) = 'ULTIMATE A LOC'
                                        
                                        INNER JOIN
                                            CODS.PRODUCT_LOCATION pl
                                            ON
                                            ope.PRODUCT_LOC_ODS_ID = pl.PRODUCT_LOC_ODS_ID
                                        
                                        INNER JOIN
                                            CODS_NETINV.PHYS_STRUCT ps
                                            ON
                                            pl.BLDG_PHYS_STRUCT_ODS_ID = ps.PHYS_STRUCT_ODS_ID
                                            AND 
                                            UPPER(ps.DW_SOURCE_SYSTEM_CD) <> 'GLM'
                                
                                WHERE 
                                           cop.SERV_ORDER_ACTIVE_IND = 'Y'
                                           AND
                                           UPPER(cop.DW_SOURCE_SYSTEM_CD) in ('PIPELINE')
                                           AND 
                                           UPPER(cop.SERV_ORDER_STATUS_CD) NOT IN ('COMPLETE','CANCELED')
                                           AND UPPER(ps.MGT_REGION_NAME) NOT IN ('LATAM') 
                                           AND UPPER(cop.SERV_ORDER_ACTION_TYP)NOT IN('DISCONNECT', 'RENEW', 'DT DISCONNECT')
                                           and FIRST_ORDER_CREATE_DT > to_date('{0}', 'mm/dd/yyyy')
                                
                                ORDER BY
                                           FIRST_ORDER_CREATE_DT ASC", strMinimumFirstOrderCreateDate);






            return strSQL;
        }

        public static string GetSQLStatement_DWOrderAddressesChangeset(DateTime dteMinimumFirstOrderCreateDate, DateTime dteDWLastModifyDate_OPE, DateTime dteDWLastModifyDate_PL, DateTime dteDWLastModifyDate_PS)
        {

            // Convert the date values to their appropriate PL-SQL Friendly Dates
            string strMinimumFirstOrderCreateDate = dteMinimumFirstOrderCreateDate.ToString("yyyy/MM/dd HH:mm:ss");
            string strDWLastModifyDate_OPE = dteDWLastModifyDate_OPE.ToString("yyyy/MM/dd HH:mm:ss");
            string strDWLastModifyDate_PL = dteDWLastModifyDate_PL.ToString("yyyy/MM/dd HH:mm:ss");
            string strDWLastModifyDate_PS = dteDWLastModifyDate_PS.ToString("yyyy/MM/dd HH:mm:ss");

            string strSQL = String.Format(@"

                            SELECT 
                                    COP.CUST_ORDER_NBR,
                                    CO.FIRST_ORDER_CREATE_DT, 
                                    COP.SERV_ORDER_NBR,
                                    COP.SERV_ORDER_STATUS_CD,
                                    COP.DW_SOURCE_SYSTEM_CD,
                                    PS.CLLI_CD, 
                                    PS.LINE1_ADDR,
                                    PS.CITY_NAME,
                                    PS.STATE_CD,
                                    PS.POSTAL_SHORT_CD,
                                    PS.COUNTRY_ISO_3_CD,
                                    PL.FLOOR_NAME,
                                    PL.ROOM_NAME,
                                    PL.SUITE_NAME,
                                    OPE.DW_LAST_MODIFY_DT AS OPE_LAST_MODIFY_DATE,
                                    PL.DW_LAST_MODIFY_DT AS PL_LAST_MODIFY_DATE,
                                    PS.DW_LAST_MODIFY_DT AS PS_LAST_MODIFY_DATE

                            FROM 
                            
                                    CODS.CUSTOMER_ORDER co
                                        
                                        INNER JOIN
                                            CODS.CUSTOMER_ORDER_PRODUCT cop
                                            ON 
                                            co.CUST_ORDER_ODS_ID = cop.CUST_ORDER_ODS_ID
                                    
                                        INNER JOIN 
                                            CODS.ORDER_PRODUCT_COMPNT opc 
                                            ON 
                                            cop.CUST_ORDER_PRODUCT_ODS_ID = opc.CUST_ORDER_PRODUCT_ODS_ID
                                            AND 
                                                (
                                                 OPC.PRODUCT_CD IN ('BMGC046', 'BMGC093', 'BMGC019', 'BMGC020', 'BMGC034', 'BMGC038', 'BMGC018', 'BMGC028',  'BMGC090')
                                                 OR 
                                                 OPC.PRODUCT_CD LIKE 'gcms%'
                                                 OR 
                                                 (OPC.FRAMING_CD = 'ETHERNET' AND OPC.ONNET_IND = 'N') 
                                                )
                                        
                                        INNER JOIN 
                                            CODS.ORDER_PRODUCT_ENDPNT ope
                                            ON 
                                            cop.CUST_ORDER_PRODUCT_ODS_ID = ope.CUST_ORDER_PRODUCT_ODS_ID
                                            AND 
                                            UPPER(endpnt_role_typ) = 'ULTIMATE A LOC'
                                        
                                        INNER JOIN
                                            CODS.PRODUCT_LOCATION pl
                                            ON
                                            ope.PRODUCT_LOC_ODS_ID = pl.PRODUCT_LOC_ODS_ID
                                        
                                        INNER JOIN
                                            CODS_NETINV.PHYS_STRUCT ps
                                            ON
                                            pl.BLDG_PHYS_STRUCT_ODS_ID = ps.PHYS_STRUCT_ODS_ID

                                WHERE 
                                           cop.SERV_ORDER_ACTIVE_IND = 'Y'
                                           AND
                                           UPPER(cop.DW_SOURCE_SYSTEM_CD) in ('PIPELINE','EON')
                                           AND 
                                           UPPER(cop.SERV_ORDER_STATUS_CD) NOT IN ('COMPLETE','CANCELED')
                                           AND UPPER(ps.MGT_REGION_NAME) NOT IN ('LATAM') 
                                           AND UPPER(cop.SERV_ORDER_ACTION_TYP)NOT IN('DISCONNECT', 'RENEW', 'DT DISCONNECT')
                                           AND
                                                FIRST_ORDER_CREATE_DT > TO_DATE('{0}', 'YYYY/MM/DD HH24:MI:SS')
                                           AND
                                                (
                                                    OPE.DW_LAST_MODIFY_DT >= TO_DATE('{1}', 'YYYY/MM/DD HH24:MI:SS')
                                                OR
                                                    PL.DW_LAST_MODIFY_DT  >= TO_DATE('{2}', 'YYYY/MM/DD HH24:MI:SS')
                                                OR
                                                    PS.DW_LAST_MODIFY_DT  >= TO_DATE('{3}', 'YYYY/MM/DD HH24:MI:SS')
                                                ) 
                                
                                ORDER BY
                                           FIRST_ORDER_CREATE_DT ASC", strMinimumFirstOrderCreateDate, strDWLastModifyDate_OPE, strDWLastModifyDate_PL, strDWLastModifyDate_PS);



            return strSQL;
        }

        public static string GetSQLStatement_DWOrderAddressesChangeset_DYNAMIC(DateTime dteMinimumFirstOrderCreateDate, DateTime dteDWLastModifyDate_OPE, DateTime dteDWLastModifyDate_PL, DateTime dteDWLastModifyDate_PS, string strProductCodeListForInClause, string strProductCodeOrClauses, string strSourceSystemCodes, string strExcludedRegionCodes, string strIncludedCountryCodes)
        {
            // Convert the date values to their appropriate PL-SQL Friendly Dates
            string strMinimumFirstOrderCreateDate = dteMinimumFirstOrderCreateDate.ToString("yyyy/MM/dd HH:mm:ss");
            string strDWLastModifyDate_OPE = dteDWLastModifyDate_OPE.ToString("yyyy/MM/dd HH:mm:ss");
            string strDWLastModifyDate_PL = dteDWLastModifyDate_PL.ToString("yyyy/MM/dd HH:mm:ss");
            string strDWLastModifyDate_PS = dteDWLastModifyDate_PS.ToString("yyyy/MM/dd HH:mm:ss");

            string strSQL = String.Format(@"

                            SELECT 
                                    COP.CUST_ORDER_NBR,
                                    CO.FIRST_ORDER_CREATE_DT, 
                                    COP.SERV_ORDER_NBR,
                                    COP.SERV_ORDER_STATUS_CD,
                                    COP.DW_SOURCE_SYSTEM_CD,
                                    PS.CLLI_CD, 
                                    PS.LINE1_ADDR,
                                    PS.CITY_NAME,
                                    PS.STATE_CD,
                                    PS.POSTAL_SHORT_CD,
                                    PS.COUNTRY_ISO_3_CD,
                                    PL.FLOOR_NAME,
                                    PL.ROOM_NAME,
                                    PL.SUITE_NAME,
                                    OPE.DW_LAST_MODIFY_DT AS OPE_LAST_MODIFY_DATE,
                                    PL.DW_LAST_MODIFY_DT AS PL_LAST_MODIFY_DATE,
                                    PS.DW_LAST_MODIFY_DT AS PS_LAST_MODIFY_DATE

                            FROM 
                            
                                    CODS.CUSTOMER_ORDER co
                                        
                                        INNER JOIN
                                            CODS.CUSTOMER_ORDER_PRODUCT cop
                                            ON 
                                            co.CUST_ORDER_ODS_ID = cop.CUST_ORDER_ODS_ID
                                    
                                        INNER JOIN 
                                            CODS.ORDER_PRODUCT_COMPNT opc 
                                            ON 
                                            cop.CUST_ORDER_PRODUCT_ODS_ID = opc.CUST_ORDER_PRODUCT_ODS_ID
                                            AND 
                                                (
                                                 OPC.PRODUCT_CD IN ({4})
                                                 {5}
                                                 OR 
                                                 (OPC.FRAMING_CD = 'ETHERNET' AND OPC.ONNET_IND = 'N') 
                                                )
                                        
                                        INNER JOIN 
                                            CODS.ORDER_PRODUCT_ENDPNT ope
                                            ON 
                                            cop.CUST_ORDER_PRODUCT_ODS_ID = ope.CUST_ORDER_PRODUCT_ODS_ID
                                            AND 
                                            UPPER(endpnt_role_typ) = 'ULTIMATE A LOC'
                                        
                                        INNER JOIN
                                            CODS.PRODUCT_LOCATION pl
                                            ON
                                            ope.PRODUCT_LOC_ODS_ID = pl.PRODUCT_LOC_ODS_ID
                                        
                                        INNER JOIN
                                            CODS_NETINV.PHYS_STRUCT ps
                                            ON
                                            pl.BLDG_PHYS_STRUCT_ODS_ID = ps.PHYS_STRUCT_ODS_ID

                                WHERE 
                                           cop.SERV_ORDER_ACTIVE_IND = 'Y'
                                           AND
                                           UPPER(cop.DW_SOURCE_SYSTEM_CD) in ({6})
                                           AND 
                                           UPPER(cop.SERV_ORDER_STATUS_CD) NOT IN ('COMPLETE','CANCELED')
                                           AND (UPPER(NVL(pl.REGION_CD, 'NULL'))) NOT IN ({7}) 
                                           AND UPPER(cop.SERV_ORDER_ACTION_TYP)NOT IN('DISCONNECT', 'RENEW', 'DT DISCONNECT')
                                           AND
                                                FIRST_ORDER_CREATE_DT > TO_DATE('{0}', 'YYYY/MM/DD HH24:MI:SS')
                                           AND
                                                (
                                                    OPE.DW_LAST_MODIFY_DT >= TO_DATE('{1}', 'YYYY/MM/DD HH24:MI:SS')
                                                OR
                                                    PL.DW_LAST_MODIFY_DT  >= TO_DATE('{2}', 'YYYY/MM/DD HH24:MI:SS')
                                                OR
                                                    PS.DW_LAST_MODIFY_DT  >= TO_DATE('{3}', 'YYYY/MM/DD HH24:MI:SS')
                                                )
                                           AND  (UPPER(NVL(PS.COUNTRY_ISO_3_CD, 'NULL'))) IN ({8})                                             
                                
                                ORDER BY
                                           FIRST_ORDER_CREATE_DT ASC", strMinimumFirstOrderCreateDate, strDWLastModifyDate_OPE, strDWLastModifyDate_PL, strDWLastModifyDate_PS, strProductCodeListForInClause, strProductCodeOrClauses, strSourceSystemCodes, strExcludedRegionCodes, strIncludedCountryCodes);



            return strSQL;
        }

        /// <summary>
        /// PHASE 2
        /// </summary>
        /// <param name="lstProductCodeInItems"></param>
        /// <param name="lstProductCodeLikeItems"></param>
        /// <param name="lstServiceOrderActionTypeNotInItems"></param>
        /// <param name="lstServiceOrderStatusCodeNotInItems"></param>
        /// <returns></returns>
        public static string GetSQLStatement_DWOrderAddressesNotInGLM_TokenReplaceFromDB(List<string> lstProductCodeInItems, List<string> lstProductCodeLikeItems, List<string> lstServiceOrderActionTypeNotInItems, List<string> lstServiceOrderStatusCodeNotInItems)
        {
            string strProductCodeInClause = "";
            string strProductCodeLikeClause = "";
            string strServiceOrderActionTypeNotInClause = "";
            string strServiceOrderStatusCodeNotInClause = "";

            if (lstProductCodeInItems.Count > 0)
            {
                strProductCodeInClause = String.Format(" OPC.PRODUCT_CD IN({0})", CombineListItemsIntoDelimitedQuotedStrings(lstProductCodeInItems));
            }

            if (lstProductCodeLikeItems.Count > 0)
            {
                strProductCodeLikeClause = String.Format("  OPC.PRODUCT_CD LIKE ({0})", CombineListItemsIntoDelimitedQuotedStrings(lstProductCodeLikeItems));
            }


            string strSQL = @"Select co.FIRST_ORDER_CREATE_DT, cop.cust_order_nbr, cop.serv_order_nbr, cop.PRODUCT_INST_ID, cop.SERV_ORDER_STATUS_CD ,COP.SERV_ORDER_TYP, COP.SERV_ORDER_ACTION_TYP, opc.ORDER_COMPNT_ACTION_TYP, 
                            opc.SERV_COMPNT_ORDER_TYP, opc.product_cd, OPC.PRODUCT_NAME, ope.ENDPNT_ROLE_TYP, 
                            ps.DW_SOURCE_SYSTEM_CD,ps.PHYS_STRUCT_ID,ps.SOURCE_CREATE_DT, ps.CLLI_CD, ps.LINE1_ADDR,  pl.*
                            from CODS.CUSTOMER_ORDER co
                            inner
                            join CODS.CUSTOMER_ORDER_PRODUCT cop
                            on co.CUST_ORDER_ODS_ID = cop.CUST_ORDER_ODS_ID
                            inner
                            join CODS.ORDER_PRODUCT_COMPNT opc
                            on cop.CUST_ORDER_PRODUCT_ODS_ID = opc.CUST_ORDER_PRODUCT_ODS_ID
                            AND
                            (
                                 OPC.PRODUCT_CD IN
                                 (
                                        'BMGC046', 'BMGC093', 'BMGC019', 'BMGC020',
                                        'BMGC034', 'BMGC038', 'BMGC018', 'BMGC028', 'BMGC090'
                                 )

                            OR OPC.PRODUCT_CD LIKE 'gcms%'

                            )


                            inner join CODS.ORDER_PRODUCT_ENDPNT ope
                            on cop.CUST_ORDER_PRODUCT_ODS_ID = ope.CUST_ORDER_PRODUCT_ODS_ID
                            and endpnt_role_typ = 'Ultimate A Loc'
                            inner join CODS.PRODUCT_LOCATION pl
                            on ope.PRODUCT_LOC_ODS_ID = pl.PRODUCT_LOC_ODS_ID
                            inner join CODS_NETINV.PHYS_STRUCT ps
                            on pl.BLDG_PHYS_STRUCT_ODS_ID = ps.PHYS_STRUCT_ODS_ID
                            and ps.DW_SOURCE_SYSTEM_CD <> 'GLM'
                            where cop.SERV_ORDER_ACTIVE_IND = 'Y'
                            and cop.DW_SOURCE_SYSTEM_CD in ('EON','PIPELINE')
                            
                            AND cop.SERV_ORDER_STATUS_CD NOT IN('Complete', 'Canceled')
                            AND cop.SERV_ORDER_ACTION_TYP NOT IN('Disconnect', 'Renew', 'DT Disconnect')
                            
                            and FIRST_ORDER_CREATE_DT > to_date('01/01/2018', 'mm/dd/yyyy')
                            
                            order by FIRST_ORDER_CREATE_DT";




            return strSQL;
        }

        public static string CombineListItemsIntoDelimitedQuotedStrings(List<string> lstItems)
        {
            string strReturnString = String.Empty;

            for (int i = 0; i < lstItems.Count; i++)
            {
                if ((i > 0) && (i != lstItems.Count))
                {
                    strReturnString = String.Concat(strReturnString, ",");
                }

                strReturnString = String.Concat(strReturnString, "'", lstItems[i], "'");
            }

            return strReturnString;
        }

        public static string CombineListItemsIntoProductCodeLikeORClause(List<string> lstItems)
        {
            string strReturnString = String.Empty;

            // OR OPC.PRODUCT_CD LIKE 'gcms%'

            for (int i = 0; i < lstItems.Count; i++)
            {
                strReturnString = String.Concat(strReturnString, " OR OPC.PRODUCT_CD LIKE '", lstItems[i], "'");
            }

            return strReturnString;
        }

     

        private static string CreateOrClauseString(List<string> lstItems)
        {
            string strReturnString = String.Empty;

            for (int i = 0; i < lstItems.Count; i++)
            {
                strReturnString = String.Concat(strReturnString, String.Format(" OPC.PRODUCT_CD LIKE '{0}'", lstItems[i]));
            }

            return strReturnString;
        }



    }
}
