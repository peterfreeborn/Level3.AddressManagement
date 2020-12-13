using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;
using System.Net.Http;
using System.Net;
using Level3.AddressManagement.Model;
using System.Net.Http.Headers;
using System.Diagnostics;
using Level3.AddressManagement.DAL;

namespace Level3.AddressManagement.RAL
{
    public class SAPCallManager
    {


        // Declare the static logger object that will be used to implement logging via log4net
        private static ILog _objLogger = LogManager.GetLogger(typeof(SAPCallManager));

        // Private Members
        private static HttpClient _objHttpClient;
        private string _strRoute_AddressSearch = @"sap/bc/rest/lvlt/address/verify";
        private string _strRoute_CreateAddress = @"sap/bc/rest/lvlt/create/address";

        private int _intLogID;



        // Public Properties
        private List<string> _lstErrorMessages;
        public List<string> ErrorMessages
        {
            get { return _lstErrorMessages; }
            set { _lstErrorMessages = value; }
        }


        private HttpStatusCode _httpStatusCodeResult;
        public HttpStatusCode HttpStatusCodeResult
        {
            get { return _httpStatusCodeResult; }
            set { _httpStatusCodeResult = value; }
        }


        private string _strFinalRouteAndQueryString;
        public string FinalRouteAndQueryString
        {
            get { return _strFinalRouteAndQueryString; }
            set { _strFinalRouteAndQueryString = value; }
        }


        private string _strBaseUrl = String.Empty;
        public string BaseUrl
        {
            get { return _strBaseUrl; }
            set { _strBaseUrl = value; }
        }


        

        // Constructor
        public SAPCallManager(string strBaseUrl, int intLogID = 0)
        {
            _strBaseUrl = strBaseUrl;
            _lstErrorMessages = new List<string>();
            _intLogID = intLogID;
        }


        // Public Properties
        /// <summary>
        /// Gets a collection of address records from SAP using the PL or SL Number supplied
        /// </summary>
        /// <param name="strPLorSLNumber"></param>
        /// <returns></returns>
        public SAPAddressSearchResponse GetAddressRecordsFromSAP(string strPLorSLNumber)
        {
            // Instantiate a stopwatch to write runtimes to a log file
            Stopwatch objStopWatch = new Stopwatch();
            objStopWatch.Start();

            SAPAddressSearchResponse objSAPAddressSearchResponse = null;

            try
            {

                // Sample:  https://sapecq.corp.intranet/sap/bc/rest/lvlt/address/verify?saml2=disabled&im_sort2=SL0000680796

                string strQueryString = String.Format("saml2=disabled&im_sort2={0}", strPLorSLNumber);

                _strFinalRouteAndQueryString = String.Concat(_strRoute_AddressSearch, "?", strQueryString);

                // Initialize the objects used to GET the record from SAP
                HttpResponseMessage objHttpResponseMessage = null;
                using (_objHttpClient = new HttpClient())
                {
                    // Prep the http client object with the required http header info and endpoint
                    PrepHttpClient();

                    // Since this is a POST, which has a concent body... create the http content body, and set the coSAPrresponding content-type header value
                    _objLogger.Debug(String.Concat("About to execute the GET to SAP via REST.  GET Url = [", _strBaseUrl, "]"));

                    // Invoke the RESTful POST method
                    objHttpResponseMessage = _objHttpClient.GetAsync(_strFinalRouteAndQueryString).Result;

                    _objLogger.Debug(String.Concat("GET Complete. Resulting Http Status Code = [", objHttpResponseMessage.StatusCode, "]"));

                    _httpStatusCodeResult = objHttpResponseMessage.StatusCode;

                    if (objHttpResponseMessage.IsSuccessStatusCode)
                    {
                        string strResult = objHttpResponseMessage.Content.ReadAsStringAsync().Result;
                        objSAPAddressSearchResponse = Model.SerializationUtil.DeserializeFromJson<SAPAddressSearchResponse>(strResult);
                    }
                    else if (_httpStatusCodeResult == HttpStatusCode.NotFound)
                    {
                        string strNotFoundResult = objHttpResponseMessage.Content.ReadAsStringAsync().Result;
                        objSAPAddressSearchResponse = Model.SerializationUtil.DeserializeFromJson<SAPAddressSearchResponse>(strNotFoundResult);

                        _objLogger.Warn(String.Format("The call to SAP yielded a Not Found status code."));
                    }
                    else
                    {
                        string strErrorResult = objHttpResponseMessage.Content.ReadAsStringAsync().Result;
                        objSAPAddressSearchResponse = Model.SerializationUtil.DeserializeFromJson<SAPAddressSearchResponse>(strErrorResult);
                        string strErrorMessage = String.Format("The HTTP response code received from SAP indicated failure.  The object returned to the caller will be null.  Http Response Code = [{0}], Http Content Body Received from SAP = [{1}]", objHttpResponseMessage.StatusCode, strErrorResult);

                        _objLogger.Warn(String.Format("The call to SAP experienced a HARD failure, meaning a response code that was not a 200, 201, or 404.  This is unexpected and may indicate an issue with the API.  An error message has been added to the list on this object. Error Message Added = [{0}]", strErrorMessage));

                        throw new Exception(strErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                _objLogger.Warn(String.Concat("There was an issue while trying to SEARCH for the address record in SAP.  An exception will be thrown.  Error Message = [", ex.Message, "]"));
                _lstErrorMessages.Add(String.Format("Exception Message = [{0}].  INNER Exception Message = [{1}]",ex.Message, ex.InnerException.Message));
                objSAPAddressSearchResponse = null;
            }


            // Stop the watch and log the call to the DB
            objStopWatch.Stop();
            TimeSpan objTimeSpan = objStopWatch.Elapsed;
            LogAPICallToDB(objTimeSpan);

            return objSAPAddressSearchResponse;
        }



        public Model.SAP.Responses.CreateAddressResponse PostCreateAddressQueueItem(string strPLNumber)
        {
            // Instantiate a stopwatch to write runtimes to a log file
            Stopwatch objStopWatch = new Stopwatch();
            objStopWatch.Start();

            Model.SAP.Responses.CreateAddressResponse objCreateAddressResponse = null;

            try
            {
                // Although this is a POST, it has no Content Body

                string strFinalRouteAndQueryString = String.Format("saml2={0}", "disabled");
                _strFinalRouteAndQueryString = String.Concat(_strRoute_CreateAddress, "?", strFinalRouteAndQueryString);

                Model.SAP.Requests.CreateAddressRequest objCreateAddressRequest = new Model.SAP.Requests.CreateAddressRequest();
                objCreateAddressRequest.PL_SL_CODE = new Model.SAP.Requests.PL_SL_CODE[1];
                objCreateAddressRequest.PL_SL_CODE[0] = new Model.SAP.Requests.PL_SL_CODE();
                objCreateAddressRequest.PL_SL_CODE[0].PL_SL_ID = strPLNumber;

                string strJsonContentString = Model.SerializationUtil.SerializeToJsonString(objCreateAddressRequest);

                // Initialize the objects used to POST the message to SAP
                HttpResponseMessage objHttpResponseMessage = null;
                using (_objHttpClient = new HttpClient())
                {
                    // Prep the http client object with the required http header info and endpoint
                    PrepHttpClient();

                    // Since this is a POST, which has a content body... create the http content body, and set the corresponding content-type header value
                    HttpContent objHttpContentBody = new StringContent(strJsonContentString);
                    objHttpContentBody.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    _objLogger.Debug(String.Concat("About to execute the post to SAP via REST.  Post Url = [", _strBaseUrl, "]"));



                    // Invoke the RESTful POST method
                    objHttpResponseMessage = _objHttpClient.PostAsync(_strFinalRouteAndQueryString, objHttpContentBody).Result;

                    _objLogger.Debug(String.Concat("Post Complete. Resulting Http Status Code = [", objHttpResponseMessage.StatusCode, "]"));

                    _httpStatusCodeResult = objHttpResponseMessage.StatusCode;

                    if (objHttpResponseMessage.IsSuccessStatusCode)
                    {
                        string strResult = objHttpResponseMessage.Content.ReadAsStringAsync().Result;
                        objCreateAddressResponse = Model.SerializationUtil.DeserializeFromJson<Model.SAP.Responses.CreateAddressResponse>(strResult);
                    }
                    else
                    {
                        string strErrorResult = objHttpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        string strErrorMessage = String.Format("The HTTP response code received from SAP indicated failure.  The object returned to the caller will be null.  Http Response Code = [{0}], Http Content Body Received from SAP = [{1}]", objHttpResponseMessage.StatusCode, strErrorResult);

                        _objLogger.Warn(String.Format("The call to SAP failed.  An error message has been added to the list on this object. Error Message Added = [{0}]", strErrorMessage));

                        throw new Exception(strErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                _objLogger.Warn(String.Concat("There was an issue while trying to POST the CREATE ADDRESS request to SAP.  Error Message = [", ex.Message, "]"));
                _lstErrorMessages.Add(String.Format("Exception Message = [{0}].  INNER Exception Message = [{1}]", ex.Message, ex.InnerException.Message));
                objCreateAddressResponse = null;
            }


            // Stop the watch and log the call to the DB
            objStopWatch.Stop();
            TimeSpan objTimeSpan = objStopWatch.Elapsed;
            LogAPICallToDB(objTimeSpan);


            // Return the result
            return objCreateAddressResponse;
        }



        // Private Methods
        private void PrepHttpClient()
        {
            // Set base url  and content type header
            _objHttpClient.BaseAddress = new Uri(_strBaseUrl);
            _objHttpClient.DefaultRequestHeaders.Accept.Clear();

            // Set security header (no security on the SAP API)

        }

        private void LogAPICallToDB(TimeSpan objTimeSpan)
        {
            try
            {
                APICallLogItemUtil.LogAPICallToDB(_intLogID, _strBaseUrl, _strFinalRouteAndQueryString, _httpStatusCodeResult, objTimeSpan);
            }
            catch (Exception ex)
            {
                // Ignore and proceed
            }

        }
    }
}
