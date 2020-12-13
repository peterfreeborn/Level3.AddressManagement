using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Level3.AddressManagement.BLL;

namespace Level3.AddressManagement.UnitTest
{
    [TestClass]
    public class GoLiveRehearsalTester
    {
        [TestMethod]
        public void TestMethod1()
        {
            // http://addressmanagementtest.corp.global.level3.com/OrderAddressDetail?orderAddressId=6595&referringUrl=aHR0cDovL2FkZHJlc3NtYW5hZ2VtZW50dGVzdC5jb3JwLmdsb2JhbC5sZXZlbDMuY29tL0lzc3Vlcz9wYWdlTnVtPTAmcGFnZVNpemU9MTAw

            OrderAddressProcessor objOrderAddressProcessor = new OrderAddressProcessor();
            Assert.IsTrue(objOrderAddressProcessor.Load(6595));
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
            Assert.IsTrue(objOrderAddressProcessor.Process(true,"BJM Unit Test"));
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
        }


        [TestMethod]
        public void Test_6747_SCodeCreationFailure()
        {
            // http://addressmanagementtest.corp.global.level3.com/OrderAddressDetail?orderAddressId=6595&referringUrl=aHR0cDovL2FkZHJlc3NtYW5hZ2VtZW50dGVzdC5jb3JwLmdsb2JhbC5sZXZlbDMuY29tL0lzc3Vlcz9wYWdlTnVtPTAmcGFnZVNpemU9MTAw

            OrderAddressProcessor objOrderAddressProcessor = new OrderAddressProcessor();
            Assert.IsTrue(objOrderAddressProcessor.Load(6747));
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
            Assert.IsTrue(objOrderAddressProcessor.Process(true, "BJM Unit Test"));
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
        }


        [TestMethod]
        public void Test_6607_ServiceLocationSearch_Failure()
        {
            // http://addressmanagementtest.corp.global.level3.com/OrderAddressDetail?orderAddressId=6595&referringUrl=aHR0cDovL2FkZHJlc3NtYW5hZ2VtZW50dGVzdC5jb3JwLmdsb2JhbC5sZXZlbDMuY29tL0lzc3Vlcz9wYWdlTnVtPTAmcGFnZVNpemU9MTAw

            OrderAddressProcessor objOrderAddressProcessor = new OrderAddressProcessor();
            Assert.IsTrue(objOrderAddressProcessor.Load(6607));
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
            Assert.IsTrue(objOrderAddressProcessor.Process(true, "BJM Unit Test"));
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
        }


        [TestMethod]
        public void Test_6628_ServiceLocationSearch_Failure()
        {
            // http://addressmanagementtest.corp.global.level3.com/OrderAddressDetail?orderAddressId=6595&referringUrl=aHR0cDovL2FkZHJlc3NtYW5hZ2VtZW50dGVzdC5jb3JwLmdsb2JhbC5sZXZlbDMuY29tL0lzc3Vlcz9wYWdlTnVtPTAmcGFnZVNpemU9MTAw

            OrderAddressProcessor objOrderAddressProcessor = new OrderAddressProcessor();
            Assert.IsTrue(objOrderAddressProcessor.Load(6628));
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
            Assert.IsTrue(objOrderAddressProcessor.Process(true, "BJM Unit Test"));
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
        }

        [TestMethod]
        public void Test_6759_ServiceLocationSearch_Failure()
        {
            // http://addressmanagementtest.corp.global.level3.com/OrderAddressDetail?orderAddressId=6595&referringUrl=aHR0cDovL2FkZHJlc3NtYW5hZ2VtZW50dGVzdC5jb3JwLmdsb2JhbC5sZXZlbDMuY29tL0lzc3Vlcz9wYWdlTnVtPTAmcGFnZVNpemU9MTAw

            OrderAddressProcessor objOrderAddressProcessor = new OrderAddressProcessor();
            Assert.IsTrue(objOrderAddressProcessor.Load(6759));
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
            Assert.IsTrue(objOrderAddressProcessor.Process(true, "BJM Unit Test"));
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
        }


        [TestMethod]
        public void Test_7174_SCodeCreation_Failure()
        {
            // http://addressmanagementtest.corp.global.level3.com/OrderAddressDetail?orderAddressId=6595&referringUrl=aHR0cDovL2FkZHJlc3NtYW5hZ2VtZW50dGVzdC5jb3JwLmdsb2JhbC5sZXZlbDMuY29tL0lzc3Vlcz9wYWdlTnVtPTAmcGFnZVNpemU9MTAw

            OrderAddressProcessor objOrderAddressProcessor = new OrderAddressProcessor();
            Assert.IsTrue(objOrderAddressProcessor.Load(7174));
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
            Assert.IsTrue(objOrderAddressProcessor.Process(true, "BJM Unit Test"));
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
        }


        [TestMethod]
        public void Test_6441_SAP_NotFoundComparison()
        {
            OrderAddressProcessor objOrderAddressProcessor = new OrderAddressProcessor();
            Assert.IsTrue(objOrderAddressProcessor.Load(6441));
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
            Assert.IsTrue(objOrderAddressProcessor.Process(true, "BJM Unit Test"));
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
        }

        [TestMethod]
        public void Test_7062_SAP_NotFoundComparison()
        {
            OrderAddressProcessor objOrderAddressProcessor = new OrderAddressProcessor();
            Assert.IsTrue(objOrderAddressProcessor.Load(7062));
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
            Assert.IsTrue(objOrderAddressProcessor.Process(true, "BJM Unit Test"));
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
        }

    }
}
