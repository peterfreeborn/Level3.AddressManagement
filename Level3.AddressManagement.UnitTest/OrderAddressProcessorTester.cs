using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Level3.AddressManagement.BLL;

namespace Level3.AddressManagement.UnitTest
{
    [TestClass]
    public class OrderAddressProcessorTester
    {
        [TestMethod]
        public void Test_StagedDBRecor_ValidCLII()
        {

            OrderAddressProcessor objOrderAddressProcessor = new OrderAddressProcessor();
            Assert.IsTrue(objOrderAddressProcessor.Load(2));
            //  Assert.IsTrue(objOrderAddressProcessor.Load(5));
            //  Assert.IsTrue(objOrderAddressProcessor.Load(54));
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
            Assert.IsTrue(objOrderAddressProcessor.Process());
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);


            // FAILING GLM SERVICE LOCATION CREATION:
            // Assert.IsTrue(objOrderAddressProcessor.Load(1));
            // Assert.IsTrue(objOrderAddressProcessor.Load(2));

            // FAILING GLM SERVICE LOCATION CREATION... BUT --->  GOOD EXAMPLE OF BAD DATA ENTRY IN ORDER SYSTEM (will create DUP Site Location b/c of SL info)
            Assert.IsTrue(objOrderAddressProcessor.Load(5));  // Unit Test 5: http://glmenv1.twtelecom.com/GLMSWeb/Location/Details/PL0009599612

            // WHY IS this coming through in our QUERY?
            // Untouched, exists as it should --> Assert.IsTrue(objOrderAddressProcessor.Load(3)); // Unit Test 3: http://glmenv1.level3.com/GLMSWeb/Location/Details/PL0000994407 PROD: http://glm.level3.com/GLMSWeb/Location/Details/PL0000994407
            // Untouched in GLM, exists as it should but wasn't in SAP --> Assert.IsTrue(objOrderAddressProcessor.Load(4)); // Unit Test 4: http://glmenv1.level3.com/GLMSWeb/Location/Details/PL0002212806 PROD: http://glm.level3.com/GLMSWeb/Location/Details/PL0002212806

            // SITE LOCATION
            //Assert.IsTrue(objOrderAddressProcessor.Load(54));
        }


        [TestMethod]
        public void Test_StagedDBRecor_BrendansApt_1290()
        {
            OrderAddressProcessor objOrderAddressProcessor = new OrderAddressProcessor();
            Assert.IsTrue(objOrderAddressProcessor.Load(1290));
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
            Assert.IsTrue(objOrderAddressProcessor.Process());
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
        }

        [TestMethod]
        public void Test_StagedDBRecor_RawAddress()
        {
        }


        [TestMethod]
        public void Test_OneOffTester_99xx()
        {
            OrderAddressProcessor objOrderAddressProcessor = new OrderAddressProcessor();
            Assert.IsTrue(objOrderAddressProcessor.Load(9990));
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
            Assert.IsTrue(objOrderAddressProcessor.Process());
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
        }




    }
}
