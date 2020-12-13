using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using log4net;
using Level3.AddressManagement.Model.SearchLocationService;
using Level3.AddressManagement.Model.LocationService;
using Level3.AddressManagement.DAL;
using System.Diagnostics;

namespace Level3.AddressManagement.RAL
{
    public class GLMCallManager
    {
        // Declare the static logger object that will be used to implement logging via log4net
        private static ILog _objLogger = LogManager.GetLogger(typeof(GLMCallManager));

        // Private Members
        private static HttpClient _objHttpClient;
        private string _strAuthorizationHeaderUsername_Authorization;
        private string _strAuthorizationHeaderApplicationID_XGLMAPIAuthorization;

        private string _strRoute_GetAddress = @"/GLMSWebServices/LocationService.svc/SiteLocation/v2";
        private string _strRoute_CreateNewServiceLocation = @"/GLMSWebServices/ServiceLocationService.svc/NewServiceLocation";
        private string _strRoute_CreateSiteCode = @"/GLMSWebServices/LocationService.svc/OrderNotification";

        // Best used to search for a PL Number by CLII
        private const string _strRoute_AutocompleteService_GetAddress = @"/GLMSWebServices/AutocompleteService.svc/GetAddresses"; // INFO --> http://glm.level3.com/GLMSWebServices/AutocompleteService.svc/help/operations/GetAddresses

        // Best used to serach for a PL Number by RAW Address fields
        private string _strRoute_SearchLocationService_AddressLocationQueryV2 = @"/GLMSWebServices/SearchLocationService.svc/AddressLocationQuery/v2"; // INFO --> http://glm.level3.com/GLMSWebServices/SearchLocationService.svc/help/operations/AddressSearchLocationsV2


        private string _strAuthorizationHeaderUsername_Authorization_HeaderFieldName = "Authorization";
        private string _strAuthorizationHeaderApplicationID_XGLMAPIAuthorization_HeaderFieldName = "X-GLM-API-Authorization";

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
        public GLMCallManager(string strBaseUrl, string strAuthorizationHeaderUsername_Authorization, string strAuthorizationHeaderApplicationID_XGLMAPIAuthorization, int intLogID = 0)
        {
            _strBaseUrl = strBaseUrl;
            _strAuthorizationHeaderUsername_Authorization = strAuthorizationHeaderUsername_Authorization;
            _strAuthorizationHeaderApplicationID_XGLMAPIAuthorization = strAuthorizationHeaderApplicationID_XGLMAPIAuthorization;

            _intLogID = intLogID;

            _lstErrorMessages = new List<string>();
        }



        // Public Methods
        /// <summary>
        /// GET any applicable search Results from GLM.  This is a SOLR indexed storage, so all fields are searched for the value supplied
        /// </summary>
        /// <param name="objSourceAddress"></param>
        /// <returns></returns>
        public List<Model.ResponseItem> GetAddressFromGLMByMasterSideIDOrCLII(string strMasterSiteIDOrCLIIId)
        {
            // Instantiate a stopwatch to write runtimes to a log file
            Stopwatch objStopWatch = new Stopwatch();
            objStopWatch.Start();


            // Instantiate the return object
            List<Model.ResponseItem> lstResponseItems = new List<Model.ResponseItem>();

            try
            {
                string strFinalRouteAndQueryString = String.Format("content={0}", strMasterSiteIDOrCLIIId);
                _strFinalRouteAndQueryString = String.Concat(_strRoute_AutocompleteService_GetAddress, "?", strFinalRouteAndQueryString);

                // Initialize the objects used to GET the record fromfff GLM
                HttpResponseMessage objHttpResponseMessage = null;
                using (_objHttpClient = new HttpClient())
                {
                    // Prep the http client object with the required http header info and endpoint
                    PrepHttpClient();

                    // Since this is a POST, which has a concent body... create the http content body, and set the corresponding content-type header value
                    _objLogger.Debug(String.Concat("About to execute the GET to GLM via REST.  GET Url = [", _strBaseUrl, "]"));

                    // Invoke the RESTful POST method
                    objHttpResponseMessage = _objHttpClient.GetAsync(_strFinalRouteAndQueryString).Result;

                    _objLogger.Debug(String.Concat("GET Complete. Resulting Http Status Code = [", objHttpResponseMessage.StatusCode, "]"));

                    _httpStatusCodeResult = objHttpResponseMessage.StatusCode;

                    if (objHttpResponseMessage.IsSuccessStatusCode)
                    {
                        string strResult = objHttpResponseMessage.Content.ReadAsStringAsync().Result;
                        lstResponseItems = Model.SerializationUtil.DeserializeFromJson<List<Model.ResponseItem>>(strResult);
                    }
                    else
                    {
                        string strErrorResult = objHttpResponseMessage.Content.ReadAsStringAsync().Result;
                        string strErrorMessage = String.Format("The HTTP response code received from GLM indicated failure.  The object returned to the caller will be null.  Http Response Code = [{0}], Http Content Body Received from GLM = [{1}]", objHttpResponseMessage.StatusCode, strErrorResult);

                        _objLogger.Warn(String.Format("The call to GLM failed.  An error message has been added to the list on this object. Error Message Added = [{0}]", strErrorMessage));

                        throw new Exception(strErrorMessage);
                    }

                    // We should not get here if the status code evaluation above is good, but just in case anything slips through... we now throw an exception if this was not a success status code
                    objHttpResponseMessage.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                _objLogger.Warn(String.Concat("There was an issue while trying to POST the ADDRESS SEARCH to GLM.  An exception will be thrown.  Error Message = [", ex.Message, "]"));
                _lstErrorMessages.Add(ex.Message);
                lstResponseItems = null;
            }

            // Stop the watch and log the call to the DB
            objStopWatch.Stop();
            TimeSpan objTimeSpan = objStopWatch.Elapsed;
            LogAPICallToDB( objTimeSpan);

            return lstResponseItems;

        }


        /// <summary>
        /// Posts a Search Query to GLM to retrieve any applicable search Results.  NOTE:  Since GLM is a bit goofy and return a JSON collection that does NOT have a name, we have to return an array of items appearing in the payload as opposed to a rapper named list or object
        /// </summary>
        /// <param name="objSourceAddress"></param>
        /// <returns></returns>
        public List<Model.SearchLocationService.AddressLocationQuery.AddressSearchLocationV2> PostAddressSearchToGLM(Model.SearchLocationService.AddressLocationQuery.AddressLocationQueryRequest objSourceAddress)
        {
            // Instantiate a stopwatch to write runtimes to a log file
            Stopwatch objStopWatch = new Stopwatch();
            objStopWatch.Start();

            List<Model.SearchLocationService.AddressLocationQuery.AddressSearchLocationV2> lstArrayOfSearchResult = null;

            try
            {
                // Prep the Request object so it can be sent as the Content Body of a POST to GLM
                string strSourceAddressAsJsonContentString = Model.SerializationUtil.SerializeToJsonString(objSourceAddress);

                _strFinalRouteAndQueryString = _strRoute_SearchLocationService_AddressLocationQueryV2;

                // Initialize the objects used to POST the message to GLM
                HttpResponseMessage objHttpResponseMessage = null;
                using (_objHttpClient = new HttpClient())
                {
                    // Prep the http client object with the required http header info and endpoint
                    PrepHttpClient();

                    // Since this is a POST, which has a concent body... create the http content body, and set the corresponding content-type header value
                    HttpContent objHttpContentBody = new StringContent(strSourceAddressAsJsonContentString);
                    objHttpContentBody.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    _objLogger.Debug(String.Concat("About to execute the post to GLM via REST.  Post Url = [", _strBaseUrl, "]"));

                    // Invoke the RESTful POST method
                    objHttpResponseMessage = _objHttpClient.PostAsync(_strFinalRouteAndQueryString, objHttpContentBody).Result;

                    _objLogger.Debug(String.Concat("Post Complete. Resulting Http Status Code = [", objHttpResponseMessage.StatusCode, "]"));

                    _httpStatusCodeResult = objHttpResponseMessage.StatusCode;

                    if (objHttpResponseMessage.IsSuccessStatusCode)
                    {
                        string strResult = objHttpResponseMessage.Content.ReadAsStringAsync().Result;
                        lstArrayOfSearchResult = Model.SerializationUtil.DeserializeFromJson<List<Model.SearchLocationService.AddressLocationQuery.AddressSearchLocationV2>>(strResult);

                    }
                    else
                    {
                        string strErrorResult = objHttpResponseMessage.Content.ReadAsStringAsync().Result;
                        string strErrorMessage = String.Format("The HTTP response code received from GLM indicated failure.  The object returned to the caller will be null.  Http Response Code = [{0}], Http Content Body Received from GLM = [{1}]", objHttpResponseMessage.StatusCode, strErrorResult);

                        _objLogger.Warn(String.Format("The call to GLM failed.  An error message has been added to the list on this object. Error Message Added = [{0}]", strErrorMessage));

                        throw new Exception(strErrorMessage);
                    }

                    // We should not get here if the status code evaluation above is good, but just in case anything slips through... we now throw an exception if this was not a success status code
                    objHttpResponseMessage.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                _objLogger.Warn(String.Concat("There was an issue while trying to POST the ADDRESS SEARCH to GLM.  An exception will be thrown.  Error Message = [", ex.Message, "]"));
                _lstErrorMessages.Add(ex.Message);
                lstArrayOfSearchResult = null;
            }

            // Stop the watch and log the call to the DB
            objStopWatch.Stop();
            TimeSpan objTimeSpan = objStopWatch.Elapsed;
            LogAPICallToDB( objTimeSpan);


            // Return the result
            return lstArrayOfSearchResult;
        }


        /// <summary>
        ///  Posts a request to GLM to trigger the creation of a "Site Code" (If SITE LOCATION) or an "S Code" (If SERVICE LOCATION)
        /// </summary>
        /// <param name="strMasterSiteIDOrSLNumber"></param>
        /// <returns></returns>
        public Model.LocationService.OrderLocation.OrderNotificationResponse PostOrderNotificationToGLMToCreateSiteCodeOrSCode(string strMasterSiteIDOrSLNumber)
        {
            // Instantiate a stopwatch to write runtimes to a log file
            Stopwatch objStopWatch = new Stopwatch();
            objStopWatch.Start();

            Model.LocationService.OrderLocation.OrderNotificationResponse objOrderNotificationResponse = null;

            try
            {
                // Although this is a POST, it has no Content Body
                _strRoute_CreateSiteCode = String.Format("{0}/{1}", _strRoute_CreateSiteCode, strMasterSiteIDOrSLNumber);

                string strQueryString = String.Format("orderOnNet={0}&legacyProduct={1}", "false", "false");

                _strFinalRouteAndQueryString = String.Concat(_strRoute_CreateSiteCode, "?", strQueryString);

                // Initialize the objects used to POST the message to GLM
                HttpResponseMessage objHttpResponseMessage = null;
                using (_objHttpClient = new HttpClient())
                {
                    // Prep the http client object with the required http header info and endpoint
                    PrepHttpClient();

                    // Since this is a POST, which has a concent body... create the http content body, and set the corresponding content-type header value
                    HttpContent objHttpContentBody = new StringContent(String.Empty);
                    objHttpContentBody.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    _objLogger.Debug(String.Concat("About to execute the post to GLM via REST.  Post Url = [", _strBaseUrl, "]"));

                    // Invoke the RESTful POST method
                    objHttpResponseMessage = _objHttpClient.PostAsync(_strFinalRouteAndQueryString, objHttpContentBody).Result ;

                    _objLogger.Debug(String.Concat("Post Complete. Resulting Http Status Code = [", objHttpResponseMessage.StatusCode, "]"));

                    _httpStatusCodeResult = objHttpResponseMessage.StatusCode;

                    if (objHttpResponseMessage.IsSuccessStatusCode)
                    {
                        string strResult = objHttpResponseMessage.Content.ReadAsStringAsync().Result;
                        objOrderNotificationResponse = Model.SerializationUtil.DeserializeFromJson<Level3.AddressManagement.Model.LocationService.OrderLocation.OrderNotificationResponse>(strResult);

                        if (String.IsNullOrEmpty(objOrderNotificationResponse.ErrorMessage?.Trim()) == false)
                        {
                            throw new Exception(objOrderNotificationResponse.ErrorMessage);
                        }
                    }
                    else
                    {
                        string strErrorResult = objHttpResponseMessage.Content.ReadAsStringAsync().Result;
                        string strErrorMessage = String.Format("The HTTP response code received from GLM indicated failure.  The object returned to the caller will be null.  Http Response Code = [{0}], Http Content Body Received from GLM = [{1}]", objHttpResponseMessage.StatusCode, strErrorResult);

                        _objLogger.Warn(String.Format("The call to GLM failed.  An error message has been added to the list on this object. Error Message Added = [{0}]", strErrorMessage));

                        throw new Exception(strErrorMessage);
                    }

                    // We should not get here if the status code evaluation above is good, but just in case anything slips through... we now throw an exception if this was not a success status code
                    objHttpResponseMessage.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                _objLogger.Warn(String.Concat("There was an issue while trying to POST the SITE CODE or S-CODE CREATION request to GLM.  An exception will be thrown.  Error Message = [", ex.Message, "]"));
                _lstErrorMessages.Add(ex.Message);
                objOrderNotificationResponse = null;
            }

            // Stop the watch and log the call to the DB
            objStopWatch.Stop();
            TimeSpan objTimeSpan = objStopWatch.Elapsed;
            LogAPICallToDB( objTimeSpan);


            // Return the result
            return objOrderNotificationResponse;
        }


        /// <summary>
        /// Posts a NEW SERVICE LOCATION to GLM
        /// </summary>
        /// <param name="objAddServiceLocationRequest"></param>
        /// <returns></returns>
        public Model.ServiceLocationService.Responses.AddServiceLocationResponse PutNewServiceLocation(Level3.AddressManagement.Model.ServiceLocationService.Requests.AddServiceLocationRequest objAddServiceLocationRequest)
        {
            // Instantiate a stopwatch to write runtimes to a log file
            Stopwatch objStopWatch = new Stopwatch();
            objStopWatch.Start();

            Model.ServiceLocationService.Responses.AddServiceLocationResponse objAddServiceLocationResponse = null;

            try
            {
                string strJsonContentString = Model.SerializationUtil.SerializeToJsonString(objAddServiceLocationRequest);

                _strFinalRouteAndQueryString = _strRoute_CreateNewServiceLocation;

                // Initialize the objects used to POST the message to GLM
                HttpResponseMessage objHttpResponseMessage = null;
                using (_objHttpClient = new HttpClient())
                {
                    // Prep the http client object with the required http header info and endpoint
                    PrepHttpClient();

                    // Since this is a POST, which has a concent body... create the http content body, and set the corresponding content-type header value
                    HttpContent objHttpContentBody = new StringContent(strJsonContentString);
                    objHttpContentBody.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    _objLogger.Debug(String.Concat("About to execute the post to GLM via REST.  Post Url = [", _strBaseUrl, "]"));

                    // Invoke the RESTful POST method
                    objHttpResponseMessage = _objHttpClient.PutAsync(_strFinalRouteAndQueryString, objHttpContentBody).Result ;

                    _objLogger.Debug(String.Concat("Post Complete. Resulting Http Status Code = [", objHttpResponseMessage.StatusCode, "]"));

                    _httpStatusCodeResult = objHttpResponseMessage.StatusCode;

                    if (objHttpResponseMessage.IsSuccessStatusCode)
                    {
                        string strResult = objHttpResponseMessage.Content.ReadAsStringAsync().Result;
                        objAddServiceLocationResponse = Model.SerializationUtil.DeserializeFromJson<Model.ServiceLocationService.Responses.AddServiceLocationResponse>(strResult);

                        if (String.IsNullOrEmpty(objAddServiceLocationResponse.ErrorMessage?.Trim()) == false)
                        {
                            throw new Exception(objAddServiceLocationResponse.ErrorMessage);
                        }
                    }
                    else
                    {
                        string strErrorResult = objHttpResponseMessage.Content.ReadAsStringAsync().Result;
                        string strErrorMessage = String.Format("The HTTP response code received from GLM indicated failure.  The object returned to the caller will be null.  Http Response Code = [{0}], Http Content Body Received from GLM = [{1}]", objHttpResponseMessage.StatusCode, strErrorResult);

                        _objLogger.Warn(String.Format("The call to GLM failed.  An error message has been added to the list on this object. Error Message Added = [{0}]", strErrorMessage));

                        throw new Exception(strErrorMessage);
                    }

                    // We should not get here if the status code evaluation above is good, but just in case anything slips through... we now throw an exception if this was not a success status code
                    objHttpResponseMessage.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                _objLogger.Warn(String.Concat("There was an issue while trying to POST the NEW SERVICE LOCATION to GLM.  An exception will be thrown.  Error Message = [", ex.Message, "]"));
                _lstErrorMessages.Add(ex.Message);
                objAddServiceLocationResponse = null;
            }


            // Stop the watch and log the call to the DB
            objStopWatch.Stop();
            TimeSpan objTimeSpan = objStopWatch.Elapsed;
            LogAPICallToDB( objTimeSpan);


            return objAddServiceLocationResponse;
        }


        #region DEPRECATED

        public List<SiteLocationV2> GetSiteLocationFromGLMByPLOrClII(string strPLNumberOrCLIIId)
        {
            // Return the result
            return GetSiteLocationFromGLM(strPLNumberOrCLIIId, false, false, true, false, false, false, false);
        }
        /// <summary>
        /// Invokes an Http GET against GLM's REST api to return all  the inf
        /// </summary>
        /// <param name="strMasterSiteID"></param>
        /// <param name="blnIncludeAllData_ad"></param>
        /// <param name="blnIncludeBuildingDetail_bd"></param>
        /// <param name="blnIncludeServiceLocations_sd"></param>
        /// <param name="blnIncludeNetworkEntities_nd"></param>
        /// <param name="blnIncludeSiteNotes_sn"></param>
        /// <param name="blnIncludeAllAddresses_sa"></param>
        /// <param name="blnIncludeBuildingAccess_ba"></param>
        /// <returns></returns>
        public List<SiteLocationV2> GetSiteLocationFromGLM(string strMasterSiteID, bool blnIncludeAllData_ad, bool blnIncludeBuildingDetail_bd, bool blnIncludeServiceLocations_sd, bool blnIncludeNetworkEntities_nd, bool blnIncludeSiteNotes_sn, bool blnIncludeAllAddresses_sa, bool blnIncludeBuildingAccess_ba)
        {
            // Instantiate a stopwatch to write runtimes to a log file
            Stopwatch objStopWatch = new Stopwatch();
            objStopWatch.Start();

            List<SiteLocationV2> lstSiteLocationV2 = null;

            try
            {
                string strFinalRouteAndQueryString = String.Format("{0}?sd={1}&ad={2}&bd={3}&nd={4}&sn={5}&ba={6}", strMasterSiteID, blnIncludeServiceLocations_sd.ToString().ToLower(), blnIncludeAllData_ad.ToString().ToLower(), blnIncludeBuildingDetail_bd.ToString().ToLower(), blnIncludeNetworkEntities_nd.ToString().ToLower(), blnIncludeSiteNotes_sn.ToString().ToLower(), blnIncludeBuildingAccess_ba.ToString().ToLower());

                _strFinalRouteAndQueryString = String.Concat(_strRoute_GetAddress, "/", strFinalRouteAndQueryString);

                // Initialize the objects used to GET the record fromfff GLM
                HttpResponseMessage objHttpResponseMessage = null;
                using (_objHttpClient = new HttpClient())
                {
                    // Prep the http client object with the required http header info and endpoint
                    PrepHttpClient();

                    // Since this is a POST, which has a concent body... create the http content body, and set the corresponding content-type header value
                    _objLogger.Debug(String.Concat("About to execute the GET to GLM via REST.  GET Url = [", _strBaseUrl, "]"));

                    // Invoke the RESTful POST method
                    objHttpResponseMessage = _objHttpClient.GetAsync(_strFinalRouteAndQueryString).Result;

                    _objLogger.Debug(String.Concat("GET Complete. Resulting Http Status Code = [", objHttpResponseMessage.StatusCode, "]"));

                    _httpStatusCodeResult = objHttpResponseMessage.StatusCode;

                    if (objHttpResponseMessage.IsSuccessStatusCode)
                    {
                        string strResult = objHttpResponseMessage.Content.ReadAsStringAsync().Result;
                        lstSiteLocationV2 = Model.SerializationUtil.DeserializeFromJson<List<SiteLocationV2>>(strResult);
                    }
                    else
                    {
                        string strErrorResult = objHttpResponseMessage.Content.ReadAsStringAsync().Result;
                        string strErrorMessage = String.Format("The HTTP response code received from GLM indicated failure.  The object returned to the caller will be null.  Http Response Code = [{0}], Http Content Body Received from GLM = [{1}]", objHttpResponseMessage.StatusCode, strErrorResult);

                        _objLogger.Warn(String.Format("The call to GLM failed.  An error message has been added to the list on this object. Error Message Added = [{0}]", strErrorMessage));

                        throw new Exception(strErrorMessage);
                    }

                    // We should not get here if the status code evaluation above is good, but just in case anything slips through... we now throw an exception if this was not a success status code
                    objHttpResponseMessage.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                _objLogger.Warn(String.Concat("There was an issue while trying to POST the ADDRESS SEARCH to GLM.  An exception will be thrown.  Error Message = [", ex.Message, "]"));
                _lstErrorMessages.Add(ex.Message);
                lstSiteLocationV2 = null;
            }

            // Stop the watch and log the call to the DB
            objStopWatch.Stop();
            TimeSpan objTimeSpan = objStopWatch.Elapsed;
            LogAPICallToDB(objTimeSpan);


            return lstSiteLocationV2;

        }


        ///// <summary>
        ///// Posts a Search Query to GLM to retrieve any applicable search Results.  NOTE:  Since GLM is a bit goofy and return a JSON collection that does NOT have a name, we have to return an array of items appearing in the payload as opposed to a rapper named list or object
        ///// </summary>
        ///// <param name="objSourceAddress"></param>
        ///// <returns></returns>


        //public List<SearchResultV2> PostAddressSearchToGLM(AdvancedLocationQueryV2Request objSourceAddress)
        //{
        //    List<SearchResultV2> lstSearchResultV2 = null;

        //    try
        //    {
        //        // Prep the Request object so it can be sent as the Content Body of a POST to GLM
        //        string strSourceAddressAsJsonContentString = Model.SerializationUtil.SerializeToJsonString(objSourceAddress);

        //        // Initialize the objects used to POST the message to GLM
        //        HttpResponseMessage objHttpResponseMessage = null;
        //        using (_objHttpClient = new HttpClient())
        //        {
        //            // Prep the http client object with the required http header info and endpoint
        //            PrepHttpClient();

        //            // Since this is a POST, which has a concent body... create the http content body, and set the corresponding content-type header value
        //            HttpContent objHttpContentBody = new StringContent(strSourceAddressAsJsonContentString);
        //            objHttpContentBody.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //            _objLogger.Debug(String.Concat("About to execute the post to GLM via REST.  Post Url = [", _strBaseUrl, "]"));

        //            // Invoke the RESTful POST method
        //            objHttpResponseMessage = _objHttpClient.PostAsync(_strRoute_AdvancedLocationQueryV2, objHttpContentBody).Result;

        //            _objLogger.Debug(String.Concat("Post Complete. Resulting Http Status Code = [", objHttpResponseMessage.StatusCode, "]"));

        //            _httpStatusCodeResult = objHttpResponseMessage.StatusCode;

        //            if (objHttpResponseMessage.IsSuccessStatusCode)
        //            {
        //                string strResult = objHttpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        //                lstSearchResultV2 = Model.SerializationUtil.DeserializeFromJson<List<SearchResultV2>>(strResult);
        //            }
        //            else
        //            {
        //                string strErrorResult = objHttpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        //                string strErrorMessage = String.Format("The HTTP response code received from GLM indicated failure.  The object returned to the caller will be null.  Http Response Code = [{0}], Http Content Body Received from GLM = [{1}]", objHttpResponseMessage.StatusCode, strErrorResult);

        //                _objLogger.Warn(String.Format("The call to GLM failed.  An error message has been added to the list on this object. Error Message Added = [{0}]", strErrorMessage));

        //                throw new Exception(strErrorMessage);
        //            }

        //            // We should not get here if the status code evaluation above is good, but just in case anything slips through... we now throw an exception if this was not a success status code
        //            objHttpResponseMessage.EnsureSuccessStatusCode();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _objLogger.Warn(String.Concat("There was an issue while trying to POST the ADDRESS SEARCH to GLM.  An exception will be thrown.  Error Message = [", ex.Message, "]"));
        //        _lstErrorMessages.Add(ex.Message);
        //        lstSearchResultV2 = null;
        //    }

        //    // Return the result
        //    return lstSearchResultV2;
        //}


        #endregion


        // Private Methods
        private void PrepHttpClient()
        {
            // Set base url  and content type header
            _objHttpClient.BaseAddress = new Uri(_strBaseUrl);
            _objHttpClient.DefaultRequestHeaders.Accept.Clear();

            // Set security header (GLM Specific Stuff)
            _objHttpClient.DefaultRequestHeaders.Add(_strAuthorizationHeaderUsername_Authorization_HeaderFieldName, _strAuthorizationHeaderUsername_Authorization);
            _objHttpClient.DefaultRequestHeaders.Add(_strAuthorizationHeaderApplicationID_XGLMAPIAuthorization_HeaderFieldName, _strAuthorizationHeaderApplicationID_XGLMAPIAuthorization);

        }

        private void LogAPICallToDB( TimeSpan objTimeSpan)
        {
            try
            {
                APICallLogItemUtil.LogAPICallToDB(_intLogID,_strBaseUrl,_strFinalRouteAndQueryString, _httpStatusCodeResult, objTimeSpan);
            }
            catch (Exception ex)
            {
                // Ignore and proceed
            }
            
        }
    }
}
