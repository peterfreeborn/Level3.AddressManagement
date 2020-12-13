using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

using Level3.AddressManagement.Model.SearchLocationService;
using Level3.AddressManagement.Model.LocationService;
using System.Linq;

namespace Level3.AddressManagement.UnitTest
{
    [TestClass]
    public class GLMCallManagerTester
    {
        string _strBaseUrl = @"http://glmenv1/";
        //string _strBaseUrl = @"http://glm.level3.com//";
        string _strAuthorizationHeaderUsername_Authorization = "SVC_SAP";
        string _strAuthorizationHeaderApplicationID_XGLMAPIAuthorization = "3843";

        [TestMethod]
        public void Test_GetAddress_GLMSample_ByCLII()
        {
            string strCLII = "LTTNCOGW";

            RAL.GLMCallManager objGLMCallManager = new RAL.GLMCallManager(_strBaseUrl, _strAuthorizationHeaderUsername_Authorization, _strAuthorizationHeaderApplicationID_XGLMAPIAuthorization);
            List<Model.ResponseItem> lstResponseItems = objGLMCallManager.GetAddressFromGLMByMasterSideIDOrCLII(strCLII);
            Assert.IsTrue(lstResponseItems != null);
            Assert.IsTrue(objGLMCallManager.ErrorMessages.Count == 0);

            Assert.IsTrue(lstResponseItems.Where(s => s.SiteId.ToUpper().Trim() == "PL0000012684").Count() > 0);
            Assert.IsTrue(lstResponseItems.Where(s => s.SiteCodes.Where(c => c.Contains(strCLII.Trim().ToUpper())).Count() > 0).Count() > 0);

        }


        [TestMethod]
        public void Test_GetAddress_GLMSample_By_PLNumber()
        {
            //string strPLNUmber = "PL0000069878";
            string strPLNUmber = "PL0011796739";


            RAL.GLMCallManager objGLMCallManager = new RAL.GLMCallManager(_strBaseUrl, _strAuthorizationHeaderUsername_Authorization, _strAuthorizationHeaderApplicationID_XGLMAPIAuthorization);
            List<Model.ResponseItem> lstResponseItems = objGLMCallManager.GetAddressFromGLMByMasterSideIDOrCLII(strPLNUmber);
            Assert.IsTrue(lstResponseItems != null);
            Assert.IsTrue(objGLMCallManager.ErrorMessages.Count == 0);

            Assert.IsTrue(lstResponseItems.Where(s => s.SiteId.ToUpper().Trim() == strPLNUmber).Count() > 0);
            Assert.IsTrue(lstResponseItems.Where(s => s.SiteCodes.Where(c => c.Contains(strPLNUmber.Trim().ToUpper())).Count() > 0).Count() > 0);

        }


        [TestMethod]
        public void Test_GetAddress_GLMSample_By_BOGUS_PLNumber()
        {
            //string strPLNUmber = "PL0000069878";
            string strPLNUmber = "PL0011796739112";


            RAL.GLMCallManager objGLMCallManager = new RAL.GLMCallManager(_strBaseUrl, _strAuthorizationHeaderUsername_Authorization, _strAuthorizationHeaderApplicationID_XGLMAPIAuthorization);
            List<Model.ResponseItem> lstResponseItems = objGLMCallManager.GetAddressFromGLMByMasterSideIDOrCLII(strPLNUmber);
            Assert.IsTrue(lstResponseItems != null);
            Assert.IsTrue(objGLMCallManager.ErrorMessages.Count == 0);

            Assert.IsTrue(lstResponseItems.Where(s => s.SiteId.ToUpper().Trim() == strPLNUmber).Count() > 0);
            Assert.IsTrue(lstResponseItems.Where(s => s.SiteCodes.Where(c => c.Contains(strPLNUmber.Trim().ToUpper())).Count() > 0).Count() > 0);

        }


        [TestMethod]
        public void Test_GetPreferredSiteCode_By_PLNumber()
        {
            string strPLNumber = "PL0011796739";

            RAL.GLMCallManager objGLMCallManager = new RAL.GLMCallManager(_strBaseUrl, _strAuthorizationHeaderUsername_Authorization, _strAuthorizationHeaderApplicationID_XGLMAPIAuthorization);
            List<Model.ResponseItem> lstResponseItems = objGLMCallManager.GetAddressFromGLMByMasterSideIDOrCLII(strPLNumber);
            Assert.IsTrue(lstResponseItems != null);
            Assert.IsTrue(objGLMCallManager.ErrorMessages.Count == 0);

            Assert.IsTrue(lstResponseItems.Where(s => s.SiteId.ToUpper().Trim() == strPLNumber).Count() > 0);

            // Try to parse the value
            string strPreferredSiteCode = String.Empty;
            string strErrorMessage = String.Empty;
            Assert.IsTrue(BLL.GLMResponseUtil.TryGetPreferredSiteCodeForPLNumber(strPLNumber, lstResponseItems, out strPreferredSiteCode, out strErrorMessage));
            Assert.IsTrue(strPreferredSiteCode == "KATYTXCE");
            Assert.IsTrue(strErrorMessage == String.Empty);

        }

        [TestMethod]
        public void Test_PostAddressSearch_GLMSample()
        {

            ///    POST http://glmenv1/GLMSWebServices/SearchLocationService.svc/AddressLocationQuery/v2 HTTP/1.1
            ///    Authorization: svc.asset_management
            ///    X - GLM - API - Authorization: 6775
            ///    Content - Type: application / json
            ///    Host: glmenv1
            ///    Content - Length: 155
            ///    
            ///    {
            ///    "AddressLine1":"10475 Park Meadows Dr",
            ///    "City":"Lone Tree",
            ///    "StateCode":"CO",
            ///    "CountryCode":"USA",
            ///    "PostalCode":"",
            ///    "CreateSiteIfNotFound": true
            ///    }


            Model.SearchLocationService.AddressLocationQuery.AddressLocationQueryRequest objAdvancedLocationQueryV2Request = new Model.SearchLocationService.AddressLocationQuery.AddressLocationQueryRequest();

            objAdvancedLocationQueryV2Request.CreateSiteIfNotFound = true;
            objAdvancedLocationQueryV2Request.AddressLine1 = "10475 Park Meadows Dr";
            objAdvancedLocationQueryV2Request.City = "Lone Tree";
            objAdvancedLocationQueryV2Request.StateCode = "CO";
            objAdvancedLocationQueryV2Request.CountryCode = "USA";

            RAL.GLMCallManager objGLMCallManager = new RAL.GLMCallManager(_strBaseUrl, _strAuthorizationHeaderUsername_Authorization, _strAuthorizationHeaderApplicationID_XGLMAPIAuthorization);
            List<Model.SearchLocationService.AddressLocationQuery.AddressSearchLocationV2> lstSearchResultV2 = objGLMCallManager.PostAddressSearchToGLM(objAdvancedLocationQueryV2Request);
            Assert.IsTrue(lstSearchResultV2 != null);
            Assert.IsTrue(objGLMCallManager.ErrorMessages.Count == 0);
        }

        [TestMethod]
        public void Test_PostAddressSearch_StallingValues()
        {

            ///    POST http://glmenv1/GLMSWebServices/SearchLocationService.svc/AddressLocationQuery/v2 HTTP/1.1
            ///    Authorization: svc.asset_management
            ///    X - GLM - API - Authorization: 6775
            ///    Content - Type: application / json
            ///    Host: glmenv1
            ///    Content - Length: 155
            ///    
            ///    {
            ///    "AddressLine1":"15757 MAIN ST",
            ///    "City":"HESPERIA",
            ///    "StateCode":"CO",
            ///    "CountryCode":"USA",
            ///    "PostalCode":"92345-3410",
            ///    "CreateSiteIfNotFound": true
            ///    }


            Model.SearchLocationService.AddressLocationQuery.AddressLocationQueryRequest objAdvancedLocationQueryV2Request = new Model.SearchLocationService.AddressLocationQuery.AddressLocationQueryRequest();

            objAdvancedLocationQueryV2Request.CreateSiteIfNotFound = true;
            objAdvancedLocationQueryV2Request.AddressLine1 = "15757 MAIN ST";
            objAdvancedLocationQueryV2Request.City = "HESPERIA";
            objAdvancedLocationQueryV2Request.StateCode = "CA";
            objAdvancedLocationQueryV2Request.CountryCode = "USA";

            RAL.GLMCallManager objGLMCallManager = new RAL.GLMCallManager(_strBaseUrl, _strAuthorizationHeaderUsername_Authorization, _strAuthorizationHeaderApplicationID_XGLMAPIAuthorization);
            List<Model.SearchLocationService.AddressLocationQuery.AddressSearchLocationV2> lstSearchResultV2 = objGLMCallManager.PostAddressSearchToGLM(objAdvancedLocationQueryV2Request);
            Assert.IsTrue(lstSearchResultV2 != null);
            Assert.IsTrue(objGLMCallManager.ErrorMessages.Count == 0);
        }

        [TestMethod]
        public void Test_Post_SiteCodeCreation()
        {
            // Missing Site Codes:
            // http://glmenv1.level3.com/GLMSWeb/Location/Details/PL0011796739
            // http://glmenv1.level3.com/GLMSWeb/Location/Details/PL0000798549

            RAL.GLMCallManager objGLMCallManager = new RAL.GLMCallManager(_strBaseUrl, _strAuthorizationHeaderUsername_Authorization, _strAuthorizationHeaderApplicationID_XGLMAPIAuthorization);
            Level3.AddressManagement.Model.LocationService.OrderLocation.OrderNotificationResponse objOrderNotificationResponse = objGLMCallManager.PostOrderNotificationToGLMToCreateSiteCodeOrSCode("PL0011796739");
            Assert.IsTrue(objOrderNotificationResponse != null);
            Assert.IsTrue(objGLMCallManager.HttpStatusCodeResult == System.Net.HttpStatusCode.OK);
            Assert.IsTrue((objOrderNotificationResponse.WorkQueueId == 0) || (objOrderNotificationResponse.WorkQueueId == null)); // If WorkQueueId > 0, then that means the turnaround time for the site location will be a few days
            Assert.IsTrue(objGLMCallManager.ErrorMessages.Count == 0);
        }


        [TestMethod]
        public void Test_Post_NewServiceLocation()
        {
            //    http://glmenv1.level3.com/GLMSWeb/Location/Details/PL0000713667
            //    1011JXRT
            //    7579 W 151st ST
            //    Florr 1st, Room telco, Stanley, KS, 66223
            //    (in GLM, no Site Code)

            Level3.AddressManagement.Model.ServiceLocationService.Requests.AddServiceLocationRequest objAddServiceLocationRequest = new Model.ServiceLocationService.Requests.AddServiceLocationRequest();

            objAddServiceLocationRequest.MasterSiteId = "PL0000713667";
            objAddServiceLocationRequest.PrimaryServiceLocationDesignator = new Model.ServiceLocationService.Requests.Primaryservicelocationdesignator();

            objAddServiceLocationRequest.PrimaryServiceLocationDesignator.FirstDesignatorTypeId = "FL";
            objAddServiceLocationRequest.PrimaryServiceLocationDesignator.FirstDesignatorValue = "1";

            objAddServiceLocationRequest.PrimaryServiceLocationDesignator.SecondDesignatorTypeId = "RM";
            objAddServiceLocationRequest.PrimaryServiceLocationDesignator.SecondDesignatorValue = "telco";

            //objAddServiceLocationRequest.PrimaryServiceLocationDesignator.ThirdDesignatorTypeId = "";
            //objAddServiceLocationRequest.PrimaryServiceLocationDesignator.ThirdDesignatorValue = "";
            //objAddServiceLocationRequest.SiteDesignatorId = null; // THIS HAS TO BE NULL, DUE TO A DEFECT IN THE GLM INTERFACE

            RAL.GLMCallManager objGLMCallManager = new RAL.GLMCallManager(_strBaseUrl, _strAuthorizationHeaderUsername_Authorization, _strAuthorizationHeaderApplicationID_XGLMAPIAuthorization);
            Level3.AddressManagement.Model.ServiceLocationService.Responses.AddServiceLocationResponse objAddServiceLocationResponse = objGLMCallManager.PutNewServiceLocation(objAddServiceLocationRequest);
            Assert.IsTrue(objAddServiceLocationResponse != null);
            Assert.IsTrue(objGLMCallManager.ErrorMessages.Count == 0);
        }




        [TestMethod]
        public void Test_GetAddress_GLMSample_By_CLII()
        {
            string strCLII = "USRVNJ90";

            RAL.GLMCallManager objGLMCallManager = new RAL.GLMCallManager(_strBaseUrl, _strAuthorizationHeaderUsername_Authorization, _strAuthorizationHeaderApplicationID_XGLMAPIAuthorization);
            List<Model.ResponseItem> lstResponseItems = objGLMCallManager.GetAddressFromGLMByMasterSideIDOrCLII(strCLII);
            Assert.IsTrue(lstResponseItems != null);
            Assert.IsTrue(objGLMCallManager.ErrorMessages.Count == 0);

            Assert.IsTrue(lstResponseItems.Where(s => s.SiteId.ToUpper().Trim() == strCLII).Count() > 0);
            Assert.IsTrue(lstResponseItems.Where(s => s.SiteCodes.Where(c => c.Contains(strCLII.Trim().ToUpper())).Count() > 0).Count() > 0);

        }



        #region DEPRECATED - WE ARE NO LONGER CONSUMING THESE, BECAUSE WE LEARNED OF NEWER, BETTER WEB SERVICES TO USE FOR OUR PURPOSES

        //[TestMethod]
        //public void Test_PostAddressSearch_GLMSample()
        //{

        //    ///    POST http://glmenv1/GLMSWebServices/SearchLocationService.svc/AddressLocationQuery/v2 HTTP/1.1
        //    ///    Authorization: svc.asset_management
        //    ///    X - GLM - API - Authorization: 6775
        //    ///    Content - Type: application / json
        //    ///    Host: glmenv1
        //    ///    Content - Length: 155
        //    ///    
        //    ///    {
        //    ///    "AddressLine1":"10475 Park Meadows Dr",
        //    ///    "City":"Lone Tree",
        //    ///    "StateCode":"CO",
        //    ///    "CountryCode":"USA",
        //    ///    "PostalCode":"",
        //    ///    "CreateSiteIfNotFound": true
        //    ///    }


        //    AdvancedLocationQueryV2Request objAdvancedLocationQueryV2Request = new AdvancedLocationQueryV2Request();

        //    objAdvancedLocationQueryV2Request.CreateSiteIfNotFound = true;
        //    objAdvancedLocationQueryV2Request.AddressLine1 = "10475 Park Meadows Dr";
        //    objAdvancedLocationQueryV2Request.City = "Lone Tree";
        //    objAdvancedLocationQueryV2Request.StateCode = "CO";
        //    objAdvancedLocationQueryV2Request.CountryCode = "USA";

        //    RAL.GLMCallManager objGLMCallManager = new RAL.GLMCallManager(_strBaseUrl, _strAuthorizationHeaderUsername_Authorization, _strAuthorizationHeaderApplicationID_XGLMAPIAuthorization);
        //    List<SearchResultV2> lstSearchResultV2 = objGLMCallManager.PostAddressSearchToGLM(objAdvancedLocationQueryV2Request);
        //    Assert.IsTrue(lstSearchResultV2 != null);
        //    Assert.IsTrue(objGLMCallManager.ErrorMessages.Count == 0);
        //}

        //[TestMethod]
        //public void Test_PostAddressSearch_GLMSample_ByCLII()
        //{
        //    string strCLII = "LTTNCOGW";

        //    RAL.GLMCallManager objGLMCallManager = new RAL.GLMCallManager(_strBaseUrl, _strAuthorizationHeaderUsername_Authorization, _strAuthorizationHeaderApplicationID_XGLMAPIAuthorization);
        //    List<SiteLocationV2> lstSiteLocationV2 = objGLMCallManager.GetSiteLocationFromGLMByClii(strCLII);
        //    Assert.IsTrue(lstSiteLocationV2 != null);
        //    Assert.IsTrue(objGLMCallManager.ErrorMessages.Count == 0);

        //    Assert.IsTrue(lstSiteLocationV2[0].MasterSiteId.Trim().ToUpper() == "PL0000012684".Trim().ToUpper());
        //    Assert.IsTrue(lstSiteLocationV2[0].CLLIId.Trim().ToUpper() == strCLII.Trim().ToUpper());
        //}

        //[TestMethod]
        //public void Test_PostAddressSearch()
        //{
        //    AdvancedLocationQueryV2Request objAdvancedLocationQueryV2Request = new AdvancedLocationQueryV2Request();


        //    //"10311 WESTPARK DR ", "", "", "Houston", "TX", "77042"

        //    objAdvancedLocationQueryV2Request.CreateSiteIfNotFound = false;
        //    objAdvancedLocationQueryV2Request.AddressLine1 = "10311 WESTPARK DR";
        //    objAdvancedLocationQueryV2Request.City = "Houston";
        //    objAdvancedLocationQueryV2Request.StateCode = "TX";
        //    objAdvancedLocationQueryV2Request.PostalCode = "77042";

        //    RAL.GLMCallManager objGLMCallManager = new RAL.GLMCallManager(_strBaseUrl, _strAuthorizationHeaderUsername_Authorization, _strAuthorizationHeaderApplicationID_XGLMAPIAuthorization);
        //    List<SearchResultV2> lstSearchResultV2 = objGLMCallManager.PostAddressSearchToGLM(objAdvancedLocationQueryV2Request);
        //    Assert.IsTrue(lstSearchResultV2 != null);
        //    Assert.IsTrue(objGLMCallManager.ErrorMessages.Count == 0);
        //}

        //[TestMethod]
        //public void Test_PostAddressSearch_DeansSampleFile_Rec1()
        //{
        //    /// ADDRESS	                CITY	    ZIP	        STATE	COUNTRY	    FLOOR	ROOM	BUILDING
        //    /// 5465 E CHERYL PARKWAY	FITCHBURG	53711	    WI	    US	        1	    SAGE


        //    /// PL0001185239 (MasterSiteID)	
        //    AdvancedLocationQueryV2Request objAdvancedLocationQueryV2Request = new AdvancedLocationQueryV2Request();

        //    objAdvancedLocationQueryV2Request.CreateSiteIfNotFound = true;
        //    objAdvancedLocationQueryV2Request.AddressLine1 = "5465 E CHERYL PARKWAY";
        //    objAdvancedLocationQueryV2Request.City = "FITCHBURG";
        //    objAdvancedLocationQueryV2Request.StateCode = "WI";
        //    objAdvancedLocationQueryV2Request.CountryCode = "US";
        //    objAdvancedLocationQueryV2Request.PostalCode = "53711";


        //    RAL.GLMCallManager objGLMCallManager = new RAL.GLMCallManager(_strBaseUrl, _strAuthorizationHeaderUsername_Authorization, _strAuthorizationHeaderApplicationID_XGLMAPIAuthorization);

        //    // Search for the SITE - PL Id and CLII
        //    List<SearchResultV2> lstSearchResultV2 = objGLMCallManager.PostAddressSearchToGLM(objAdvancedLocationQueryV2Request);
        //    Assert.IsTrue(lstSearchResultV2 != null);
        //    Assert.IsTrue(objGLMCallManager.ErrorMessages.Count == 0);

        //    // PL0001185239 (MasterSiteID)	
        //    // FTBG5504 (CLII)
        //    Assert.IsTrue(lstSearchResultV2[0].MasterSiteId.Trim().ToUpper() == "PL0001185239".Trim().ToUpper());
        //    Assert.IsTrue(lstSearchResultV2[0].CLLIId.Trim().ToUpper() == "FTBG5504".Trim().ToUpper());



        //    // Search for the SERVICE LOCATION
        //    // SL0002160909

        //}


        //[TestMethod]
        //public void Test_GetServiceLocations_DeansSampleFile_Rec1()
        //{
        //    /// ADDRESS	                CITY	    ZIP	        STATE	COUNTRY	    FLOOR	ROOM	BUILDING
        //    /// 5465 E CHERYL PARKWAY	FITCHBURG	53711	    WI	    US	        1	    SAGE


        //    ///// PL0001185239 (MasterSiteID)	
        //    //AdvancedLocationQueryV2Request objAdvancedLocationQueryV2Request = new AdvancedLocationQueryV2Request();

        //    //objAdvancedLocationQueryV2Request.CreateSiteIfNotFound = true;
        //    //objAdvancedLocationQueryV2Request.AddressLine1 = "5465 E CHERYL PARKWAY";
        //    //objAdvancedLocationQueryV2Request.City = "FITCHBURG";
        //    //objAdvancedLocationQueryV2Request.StateCode = "WI";
        //    //objAdvancedLocationQueryV2Request.CountryCode = "US";
        //    //objAdvancedLocationQueryV2Request.PostalCode = "53711";


        //    RAL.GLMCallManager objGLMCallManager = new RAL.GLMCallManager(_strBaseUrl, _strAuthorizationHeaderUsername_Authorization, _strAuthorizationHeaderApplicationID_XGLMAPIAuthorization);

        //    //// Search for the SITE - PL Id and CLII
        //    //List<SearchResultV2> lstSearchResultV2 = objGLMCallManager.PostAddressSearchToGLM(objAdvancedLocationQueryV2Request);
        //    //Assert.IsTrue(lstSearchResultV2 != null);
        //    //Assert.IsTrue(objGLMCallManager.ErrorMessages.Count == 0);

        //    //// PL0001185239 (MasterSiteID)	
        //    //// FTBG5504 (CLII)
        //    //Assert.IsTrue(lstSearchResultV2[0].MasterSiteId.Trim().ToUpper() == "PL0001185239".Trim().ToUpper());
        //    //Assert.IsTrue(lstSearchResultV2[0].CLLIId.Trim().ToUpper() == "FTBG5504".Trim().ToUpper());

        //    string strMasterSiteID = "PL0001185239";

        //    // Search for the SERVICE LOCATION
        //    // SL0002160909 "CLLIId":"FTBG5504",
        //    // LocationService.svc/SiteLocation/v2/{masterSiteID}?sd=true&ad=false&bd=false&nd=false&sn=false&ba=false

        //    List<SiteLocationV2> lstSiteLocationV2 = objGLMCallManager.GetSiteLocationFromGLM(strMasterSiteID, false, false, true, false, false, false, false);
        //    Assert.IsTrue(lstSiteLocationV2 != null);
        //    Assert.IsTrue(objGLMCallManager.ErrorMessages.Count == 0);

        //   // List<Servicelocation> lstFiltered = lstSiteLocationV2.Where(s => String.Equals(s.ServiceLocations.Where(l => l.MasterServiceLocationId == "SL0002160909")), "FTBG5504".Trim().ToUpper())).ToList();


        //}


        [TestMethod]
        public void Test_PostAddressSearch_By_PLNumber()
        {
            //string strPLNumber = "PL0000012684";
            string strPLNumber = "PL0011796739";


            RAL.GLMCallManager objGLMCallManager = new RAL.GLMCallManager(_strBaseUrl, _strAuthorizationHeaderUsername_Authorization, _strAuthorizationHeaderApplicationID_XGLMAPIAuthorization);
            List<SiteLocationV2> lstSiteLocationV2 = objGLMCallManager.GetSiteLocationFromGLMByPLOrClII(strPLNumber);
            Assert.IsTrue(lstSiteLocationV2 != null);
            Assert.IsTrue(objGLMCallManager.ErrorMessages.Count == 0);

            Assert.IsTrue(lstSiteLocationV2[0].MasterSiteId.Trim().ToUpper() == "PL0000012684".Trim().ToUpper());

            Assert.IsTrue(lstSiteLocationV2[0].ServiceLocations.Count() > 0);
            Assert.IsTrue(lstSiteLocationV2[0].ServiceLocations.Count() > 0);

        }
        #endregion



    }
}
