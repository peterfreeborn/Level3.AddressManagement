using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Level3.AddressManagement.DAL;

namespace Level3.AddressManagement.UnitTest
{
    [TestClass]
    public class ChangesetDateGroupTester
    {
        [TestMethod]
        public void Test_Get()
        {
            ChangesetDateGroup objChangesetDateGroup = new ChangesetDateGroup().GetRecords();
            Assert.IsTrue(objChangesetDateGroup != null);
        }
    }
}
