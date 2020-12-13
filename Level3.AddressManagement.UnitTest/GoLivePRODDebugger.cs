using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Level3.AddressManagement.BLL;
using System.Collections.Generic;

namespace Level3.AddressManagement.UnitTest
{
    [TestClass]
    public class GoLivePRODDebugger
    {
        string _strBaseUrl = @"http://glm.level3.com";
        string _strAuthorizationHeaderUsername_Authorization = "SVC_SAP";
        string _strAuthorizationHeaderApplicationID_XGLMAPIAuthorization = "3843";

        [TestMethod]
        public void Check_Why_SL_Not_Created()
        {
            int intOrderAddressID = 2;

            // Instantiate a GLM Call Manager, to be used to invoke the GLM Web Service APIs
            RAL.GLMCallManager objGLMCallManager = new RAL.GLMCallManager(_strBaseUrl, _strAuthorizationHeaderUsername_Authorization, _strAuthorizationHeaderApplicationID_XGLMAPIAuthorization, intOrderAddressID);

            bool blnServiceLocationExists;
            bool blnCallSucceeded;
            string strSLNumber;
            string strErrorMessage;
            

            DAL.OrderAddress _objOrderAddress = new DAL.OrderAddress().Get(intOrderAddressID, out strErrorMessage);

            // Invoke the GLM API, to search for the full set of data for the SITE LOCATION... including ALL of its SERVICE LOCATIONs
            List<Model.LocationService.SiteLocationV2> lstSiteLocationV2 = objGLMCallManager.GetSiteLocationFromGLMByPLOrClII("PL0001336837");

            // Check to see if the call succeeded
            if (lstSiteLocationV2 != null)
            {
                // Declare an analzer class that will parse through the very cryptic GLM response to determine if the Site Location Exists
                BLL.SiteLocationAnalyzer objSiteLocationAnalyzer = new BLL.SiteLocationAnalyzer();

                // Load the analyser
                if (objSiteLocationAnalyzer.Load(lstSiteLocationV2))
                {
                    // Search for the SERVICE LOCATION using its Floor, Room, and Suite
                    blnServiceLocationExists = objSiteLocationAnalyzer.FindServiceLocation_SLNumber(_objOrderAddress.CDWFloor, _objOrderAddress.CDWRoom, _objOrderAddress.CDWSuite, out strSLNumber);
                    // NOTE: The strSLNumber variable now holds the SL Number for the SERVICE LOCATION retrieved from GLM
                    blnCallSucceeded = true;
                }
                else
                {
                    // The call result cannot be loaded, which is the same end result as a failed call
                    blnCallSucceeded = false;
                    strSLNumber = String.Empty;
                }
            }
            else
            {
                // The call failed.
                blnCallSucceeded = false;
                strSLNumber = String.Empty;
            }

        }



        [TestMethod]
        public void OrderAddressID_6_NewServiceLocation_GLMBug_DesignatorsImproperlyStripped_RegressionTester()
        {
            OrderAddressProcessor objOrderAddressProcessor = new OrderAddressProcessor();
            Assert.IsTrue(objOrderAddressProcessor.Load(6));
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
            Assert.IsTrue(objOrderAddressProcessor.Process());
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
        }


        [TestMethod]
        public void OrderAddressID_7_StrangeGLMError_RegressionTester()
        {
            OrderAddressProcessor objOrderAddressProcessor = new OrderAddressProcessor();
            Assert.IsTrue(objOrderAddressProcessor.Load(7));
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
            Assert.IsTrue(objOrderAddressProcessor.Process());
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
        }


        [TestMethod]
        public void OrderAddressID_20_StrangeGLMError_RegressionTester()
        {
            OrderAddressProcessor objOrderAddressProcessor = new OrderAddressProcessor();
            Assert.IsTrue(objOrderAddressProcessor.Load(20));
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
            Assert.IsTrue(objOrderAddressProcessor.Process(true,@"level3\muir.brendan"));
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
        }
    }
}
