using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Level3.AddressManagement.UnitTest.GLM
{
    [TestClass]
    public class GLMResponseUtilTester
    {
        string _strBaseUrl = @"http://glmenv1/";
        //string _strBaseUrl = @"http://glm.level3.com//";
        string _strAuthorizationHeaderUsername_Authorization = "svc.asset_management";
        string _strAuthorizationHeaderApplicationID_XGLMAPIAuthorization = "6775";

        [TestMethod]
        public void Test_TryGetPLNumberForCLII()
        {
            string strCLII = "LTTNCOGW";

            RAL.GLMCallManager objGLMCallManager = new RAL.GLMCallManager(_strBaseUrl, _strAuthorizationHeaderUsername_Authorization, _strAuthorizationHeaderApplicationID_XGLMAPIAuthorization);
            List<Model.ResponseItem> lstResponseItems = objGLMCallManager.GetAddressFromGLMByMasterSideIDOrCLII(strCLII);

            string strPLNumber;
            string strErrorMessage;

            Assert.IsTrue(BLL.GLMResponseUtil.TryGetPLNumberForCLII(strCLII, lstResponseItems, out strPLNumber, out strErrorMessage));
            Assert.IsTrue(strPLNumber == "PL0000012684");
            Assert.IsTrue(strErrorMessage == String.Empty);

            // Now try it with a bogus Clii
            Assert.IsTrue(BLL.GLMResponseUtil.TryGetPLNumberForCLII("BOGUS", lstResponseItems, out strPLNumber, out strErrorMessage));
            Assert.IsTrue(strPLNumber == String.Empty);
            Assert.IsTrue(strErrorMessage == String.Empty);

            // Now try it with an EMPTY list
            Assert.IsTrue(BLL.GLMResponseUtil.TryGetPLNumberForCLII(strCLII, new List<Model.ResponseItem>(), out strPLNumber, out strErrorMessage));
            Assert.IsTrue(strPLNumber == String.Empty);
            Assert.IsTrue(strErrorMessage == String.Empty);

            // Now try it with a NULL list
            Assert.IsTrue(BLL.GLMResponseUtil.TryGetPLNumberForCLII(strCLII, null, out strPLNumber, out strErrorMessage));
            Assert.IsTrue(strPLNumber == String.Empty);
            Assert.IsTrue(strErrorMessage == String.Empty);
        }
    }
}
