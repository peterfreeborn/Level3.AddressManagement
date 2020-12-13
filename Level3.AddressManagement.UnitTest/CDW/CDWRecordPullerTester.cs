using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using log4net;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
[assembly: log4net.Config.Repository()]



namespace Level3.AddressManagement.UnitTest.CDW
{
    [TestClass]
    public class CDWRecordPullerTester
    {
        [TestMethod]
        public void Test_Pull_w_ConfigFileDate()
        {
            BLL.CDWRecordPuller objCDWRecordPuller = new BLL.CDWRecordPuller();
            Assert.IsTrue(objCDWRecordPuller.Load());
            Assert.IsTrue(objCDWRecordPuller.ErrorMessages.Count == 0);
        }


        [TestMethod]
        public void Test_Pull_w_ConfigFileDate_LoadAndProcess()
        {
            BLL.CDWRecordPuller objCDWRecordPuller = new BLL.CDWRecordPuller();
            Assert.IsTrue(objCDWRecordPuller.Load());
            Assert.IsTrue(objCDWRecordPuller.ErrorMessages.Count == 0);

            Assert.IsTrue(objCDWRecordPuller.Process());
            Assert.IsTrue(objCDWRecordPuller.ErrorMessages.Count == 0);
        }
    }
}
