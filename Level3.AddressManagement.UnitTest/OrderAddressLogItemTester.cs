using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;


using Level3.AddressManagement.DAL;
using Level3.AddressManagement.Model;


namespace Level3.AddressManagement.UnitTest
{
    [TestClass]
    public class OrderAddressLogItemTester
    {

        [TestMethod]
        public void Test_Insert()
        {

            OrderAddressLogItem objOrderAddressLogItem = new OrderAddressLogItem();

            objOrderAddressLogItem.OrderAddressID = 1;
            objOrderAddressLogItem.MigrationStatusID = 1;
            objOrderAddressLogItem.LogMessage = "Testing the log message.  This is a long message. The quick brown fox jumps over the lazy dog. The quick brown fox jumps over the lazy dog. The quick brown fox jumps over the lazy dog. The quick brown fox jumps over the lazy dog. The quick brown fox jumps over the lazy dog. The quick brown fox jumps over the lazy dog. END";
            objOrderAddressLogItem.DateCreated = new DateTime(2018, 08, 01);
            objOrderAddressLogItem.DateUpdated = new DateTime(2018, 08, 01);

            string strLastError = String.Empty;
            Assert.IsTrue(objOrderAddressLogItem.Insert(out strLastError));
            Assert.IsTrue(objOrderAddressLogItem.OrderAddressID > 0);
            Assert.IsTrue(strLastError == String.Empty);
        }

    }
}
