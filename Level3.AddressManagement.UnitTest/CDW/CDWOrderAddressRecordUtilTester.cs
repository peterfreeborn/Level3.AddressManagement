using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Level3.AddressManagement.UnitTest
{
    [TestClass]
    public class CDWOrderAddressRecordUtilTester
    {
        [TestMethod]
        public void Test_TranslateRawAddressFieldsToOrderAddressType()
        {
            Assert.AreEqual(BLL.CDWOrderAddressRecordUtil.TranslateRawAddressFieldsToOrderAddressType("", "", ""), Model.OrderAddressTypes.Site);
            Assert.AreEqual(BLL.CDWOrderAddressRecordUtil.TranslateRawAddressFieldsToOrderAddressType(null, "", ""), Model.OrderAddressTypes.Site);
            Assert.AreEqual(BLL.CDWOrderAddressRecordUtil.TranslateRawAddressFieldsToOrderAddressType(null, null, null), Model.OrderAddressTypes.Site);

            Assert.AreEqual(BLL.CDWOrderAddressRecordUtil.TranslateRawAddressFieldsToOrderAddressType("1", null, null), Model.OrderAddressTypes.Service_Location);
            Assert.AreEqual(BLL.CDWOrderAddressRecordUtil.TranslateRawAddressFieldsToOrderAddressType("", "204", null), Model.OrderAddressTypes.Service_Location);
            Assert.AreEqual(BLL.CDWOrderAddressRecordUtil.TranslateRawAddressFieldsToOrderAddressType("1", "Closet", "Ste 204"), Model.OrderAddressTypes.Service_Location);

        }
    }
}
