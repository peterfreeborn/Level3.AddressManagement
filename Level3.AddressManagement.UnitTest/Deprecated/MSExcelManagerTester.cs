using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Level3.AddressManagement.UnitTest
{
    [TestClass]
    public class MSExcelManagerTester
    {
        // Private Members
        string strTestFileName = @"..\..\_test_files\Sample_Addresses_2018_08_20.xlsx";
        //string strTestFileName = @"C:\Sandbox\Level3.AddressManagement\Level3.AddressManagement.UnitTest\_test_files\Sample_Addresses_2018_08_20.xlsx";



        [TestMethod]
        public void Test_ReadWorksheetRowsIntoSourceAddressObjects()
        {

            RAL.MSExcelManager objMSExcelManager = new RAL.MSExcelManager();
            Assert.IsTrue(objMSExcelManager.Load(strTestFileName));
            Assert.IsTrue(objMSExcelManager.ReadWorksheetRowsIntoSourceAddressObjects());
            Assert.IsTrue(objMSExcelManager.Load(strTestFileName));
            Assert.IsTrue(objMSExcelManager.SourceAddresses.Count == 66);
        }
    }
}
