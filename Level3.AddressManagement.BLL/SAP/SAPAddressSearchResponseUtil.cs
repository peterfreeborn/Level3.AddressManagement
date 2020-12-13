using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level3.AddressManagement.BLL
{
    public class SAPAddressSearchResponseUtil
    {

        public static bool RecordFoundInSAP(Model.SAPAddressSearchResponse objSAPAddressSearchResponse, System.Net.HttpStatusCode objHttpStatusCode, string strPLOrSLNumber, out int intNumberOfValidRecords)
        {
            // Initialize the out param variable
            intNumberOfValidRecords = 0;

            try
            {
                // Check the http response code
                if (objHttpStatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    intNumberOfValidRecords = 0;
                    return false;
                }

                // Check the response object to see if it is null
                if (objSAPAddressSearchResponse == null)
                {
                    intNumberOfValidRecords = 0;
                    return false;
                }

                // Check the response object to see if the error property is populated
                if (objSAPAddressSearchResponse.ITAB.Where(t => t.ERROR?.Length > 0).Count() > 0)
                {
                    intNumberOfValidRecords = 0;
                    return false;
                }

                // Set the number of valid records
                intNumberOfValidRecords = objSAPAddressSearchResponse.ITAB.Where(t => t.SORT2?.Trim().ToUpper() == strPLOrSLNumber?.Trim().ToUpper()).Count();

                return intNumberOfValidRecords > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("There was an exception while trying to analyze the response received from SAP, in an effort to determine if the record was actually found and how many times it appears in SAP with the corresponding PL or SL Number.  PLorSLNumber = [{0}], Error Message = [{1}].", strPLOrSLNumber, ex.Message));
            }
        }

    }
}
