using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Linq;

namespace Level3.AddressManagement.UnitTest
{
    [TestClass]
    public class SAPCallManagerTester
    {
        string _strBaseUrl = @"https://sapecq.corp.intranet"; // TEST
        //string _strBaseUrl = @"https://sapprd.corp.intranet"; // PROD
        //string _strBaseUrl = @"https://sapdev1.corp.intranet"; // DEV



        // SEARCH -----------------------------------------------------------

        [TestMethod]
        public void Test_PostAddressSearch_ByPL_Success()
        {
            string strPLorSLNumber = "PL0000025421";
            //string strPLorSLNumber = "SL0000606479";

            RAL.SAPCallManager objSAPCallManager = new RAL.SAPCallManager(_strBaseUrl);
            Model.SAPAddressSearchResponse objSAPAddressSearchResponse = objSAPCallManager.GetAddressRecordsFromSAP(strPLorSLNumber);
            Assert.IsTrue(objSAPAddressSearchResponse != null);
            Assert.IsTrue(objSAPCallManager.ErrorMessages.Count == 0);

            Assert.IsTrue(objSAPAddressSearchResponse.ITAB.Length > 0);
            Assert.IsTrue(objSAPAddressSearchResponse.ITAB.Where(t => t.SORT2.ToUpper() == strPLorSLNumber.ToUpper()).Count() > 0);
            Assert.IsTrue(objSAPAddressSearchResponse.ITAB[0].ERROR == null);
            Assert.IsTrue(objSAPAddressSearchResponse.ITAB[0].MESSAGE == null);
        }


        [TestMethod]
        public void Test_PostAddressSearch_BySL_Success()
        {
            string strPLorSLNumber = "SL0000606479";

            RAL.SAPCallManager objSAPCallManager = new RAL.SAPCallManager(_strBaseUrl);
            Model.SAPAddressSearchResponse objSAPAddressSearchResponse = objSAPCallManager.GetAddressRecordsFromSAP(strPLorSLNumber);
            Assert.IsTrue(objSAPAddressSearchResponse != null);
            Assert.IsTrue(objSAPCallManager.ErrorMessages.Count == 0);

            Assert.IsTrue(objSAPAddressSearchResponse.ITAB.Length > 0);
            Assert.IsTrue(objSAPAddressSearchResponse.ITAB.Where(t => t.SORT2.ToUpper() == strPLorSLNumber.ToUpper()).Count() > 0);
            Assert.IsTrue(objSAPAddressSearchResponse.ITAB[0].ERROR == null);
            Assert.IsTrue(objSAPAddressSearchResponse.ITAB[0].MESSAGE == null);
        }


        [TestMethod]
        public void Test_PostAddressSearch_ByPL_NotFound()
        {
            string strPLorSLNumber = "PL0000000000";

            RAL.SAPCallManager objSAPCallManager = new RAL.SAPCallManager(_strBaseUrl);
            Model.SAPAddressSearchResponse objSAPAddressSearchResponse = objSAPCallManager.GetAddressRecordsFromSAP(strPLorSLNumber);
            Assert.IsTrue(objSAPAddressSearchResponse != null);
            Assert.IsTrue(objSAPCallManager.ErrorMessages.Count == 0);
            Assert.IsTrue(objSAPCallManager.HttpStatusCodeResult == System.Net.HttpStatusCode.NotFound);

            Assert.IsTrue(objSAPAddressSearchResponse.ITAB.Length == 1);
            Assert.IsTrue(objSAPAddressSearchResponse.ITAB[0].ERROR?.Length > 0);
            Assert.IsTrue(objSAPAddressSearchResponse.ITAB[0].MESSAGE?.Length > 0);
        }



        // CREATE -----------------------------------------------------------

        [TestMethod]
        public void Test_Post_CreateAddress_ByPL_Success()
        {
            string strPLorSLNumber = "PL0000799911";

            RAL.SAPCallManager objSAPCallManager = new RAL.SAPCallManager(_strBaseUrl);
            Model.SAP.Responses.CreateAddressResponse objCreateAddressResponse = objSAPCallManager.PostCreateAddressQueueItem(strPLorSLNumber);
            Assert.IsTrue(objCreateAddressResponse != null);
            Assert.IsTrue(objSAPCallManager.ErrorMessages.Count == 0);
            Assert.IsTrue(objSAPCallManager.HttpStatusCodeResult == System.Net.HttpStatusCode.OK);

            Assert.IsTrue(objCreateAddressResponse.ET_RETURN.Count() > 0);
            Assert.IsTrue(objCreateAddressResponse.PL_SL_CODE.Count() > 0);
            Assert.IsTrue(objCreateAddressResponse.PL_SL_CODE.Where(p => p.PL_SL_ID.Trim().ToUpper() == strPLorSLNumber.Trim().ToUpper()).Count() > 0);
        }


        [TestMethod]
        public void Test_Post_CreateAddress_ByPL_Failure_404_Bad_PLNUmber()
        {

            // WAS UNABLE TO FIND A WAY TO GENERATE A 404

            //string strPLorSLNumber = "";

            //RAL.SAPCallManager objSAPCallManager = new RAL.SAPCallManager(_strBaseUrl);
            //Model.SAP.Responses.CreateAddressResponse objCreateAddressResponse = objSAPCallManager.PostCreateAddressQueueItem(strPLorSLNumber);
            //Assert.IsTrue(objCreateAddressResponse != null);
            //Assert.IsTrue(objSAPCallManager.ErrorMessages.Count == 0);
            //Assert.IsTrue(objSAPCallManager.HttpStatusCodeResult == System.Net.HttpStatusCode.NotFound);

            //Assert.IsTrue(objCreateAddressResponse.ET_RETURN.Count() > 0);
            //Assert.IsTrue(objCreateAddressResponse.PL_SL_CODE.Count() > 0);
            //Assert.IsTrue(objCreateAddressResponse.PL_SL_CODE.Where(p => p.PL_SL_ID.Trim().ToUpper() == strPLorSLNumber.Trim().ToUpper()).Count() > 0);
        }

        [TestMethod]
        public void Test_Post_CreateAddress_ByPL_Success_NoSiteCodeInGLM()
        {
            //http://glmenv1.level3.com/GLMSWeb/Location/Details/PL0000798549

            string strPLorSLNumber = "PL0000798549";

            RAL.SAPCallManager objSAPCallManager = new RAL.SAPCallManager(_strBaseUrl);
            Model.SAP.Responses.CreateAddressResponse objCreateAddressResponse = objSAPCallManager.PostCreateAddressQueueItem(strPLorSLNumber);
            Assert.IsTrue(objCreateAddressResponse != null);
            Assert.IsTrue(objSAPCallManager.ErrorMessages.Count == 0);
            Assert.IsTrue(objSAPCallManager.HttpStatusCodeResult == System.Net.HttpStatusCode.OK);

            Assert.IsTrue(objCreateAddressResponse.ET_RETURN.Count() > 0);
            Assert.IsTrue(objCreateAddressResponse.PL_SL_CODE.Count() > 0);
            Assert.IsTrue(objCreateAddressResponse.PL_SL_CODE.Where(p => p.PL_SL_ID.Trim().ToUpper() == strPLorSLNumber.Trim().ToUpper()).Count() > 0);
        }


        [TestMethod]
        public void Test_Post_CreateAddress_ByPL_Success_NoSiteCodeInGLM_Test2()
        {
            //http://glmenv1.level3.com/GLMSWeb/Location/Details/PL0000713667

            string strPLorSLNumber = "PL0000713667";

            RAL.SAPCallManager objSAPCallManager = new RAL.SAPCallManager(_strBaseUrl);
            Model.SAP.Responses.CreateAddressResponse objCreateAddressResponse = objSAPCallManager.PostCreateAddressQueueItem(strPLorSLNumber);
            Assert.IsTrue(objCreateAddressResponse != null);
            Assert.IsTrue(objSAPCallManager.ErrorMessages.Count == 0);
            Assert.IsTrue(objSAPCallManager.HttpStatusCodeResult == System.Net.HttpStatusCode.OK);

            Assert.IsTrue(objCreateAddressResponse.ET_RETURN.Count() > 0);
            Assert.IsTrue(objCreateAddressResponse.PL_SL_CODE.Count() > 0);
            Assert.IsTrue(objCreateAddressResponse.PL_SL_CODE.Where(p => p.PL_SL_ID.Trim().ToUpper() == strPLorSLNumber.Trim().ToUpper()).Count() > 0);
        }


    }
}
