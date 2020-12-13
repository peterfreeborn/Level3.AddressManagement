using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Level3.AddressManagement.UnitTest
{
    [TestClass]
    public class SourceAddressFileManagerTester
    {


        [TestMethod]
        public void Test_LoadFilesFromDisk()
        {
            BLL.FileAndDirectoryLocationCalculator objFileAndDirLocationCalc = new BLL.FileAndDirectoryLocationCalculator();
            
            BLL.SourceAddressFileManager objSourceAddressFileManager = new BLL.SourceAddressFileManager();
            Assert.IsTrue(objSourceAddressFileManager.LoadFilesNamesFromDisk(objFileAndDirLocationCalc.GetProcessingRoot()));
            Assert.IsTrue(objSourceAddressFileManager.XLSXFilesToProcess.Count == 3);
            Assert.IsTrue(objSourceAddressFileManager.ErrorMessages.Count == 0);
        }


    }
}
