using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Level3.AddressManagement.BLL;

namespace Level3.AddressManagement.UnitTest
{
    [TestClass]
    public class OrderAddressBatchOrchestratorTester
    {
        [TestMethod]
        public void Test_Load()
        {
            OrderAddressBatchOrchestrator objOrderAddressBatchOrchestrator = new OrderAddressBatchOrchestrator();
            Assert.IsTrue(objOrderAddressBatchOrchestrator.Load(Model.BatchType.AllAddressesNeedingProcessing));

        }


        [TestMethod]
        public void Test_LoadAndProcess()
        {
            OrderAddressBatchOrchestrator objOrderAddressBatchOrchestrator = new OrderAddressBatchOrchestrator();
            Assert.IsTrue(objOrderAddressBatchOrchestrator.Load(Model.BatchType.AllAddressesNeedingProcessing));
            Assert.IsTrue(objOrderAddressBatchOrchestrator.Process() || objOrderAddressBatchOrchestrator.ErrorMessages.Count > 0);
        }

        [TestMethod]
        public void Test_LoadAndProcess_AlreadyInWorkflow()
        {
            OrderAddressBatchOrchestrator objOrderAddressBatchOrchestrator = new OrderAddressBatchOrchestrator();
            Assert.IsTrue(objOrderAddressBatchOrchestrator.Load(Model.BatchType.AlreadyInWorkflow));
            Assert.IsTrue(objOrderAddressBatchOrchestrator.Process() || objOrderAddressBatchOrchestrator.ErrorMessages.Count > 0);
        }
    }
}
