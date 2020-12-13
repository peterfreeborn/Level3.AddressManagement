using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Level3.AddressManagement.BLL;

namespace Level3.AddressManagement.UnitTest
{
    [TestClass]
    public class CLIIUtilityTester
    {
        [TestMethod]
        public void Test_CLIIValidation()
        {
            Assert.IsFalse(CLIIUtility.IsValidCLII("Z0222222"));
            Assert.IsFalse(CLIIUtility.IsValidCLII("z0222222"));
            Assert.IsFalse(CLIIUtility.IsValidCLII("z"));

            Assert.IsTrue(CLIIUtility.IsValidCLII("ZP222222"));
            Assert.IsTrue(CLIIUtility.IsValidCLII("P222222"));
            Assert.IsTrue(CLIIUtility.IsValidCLII("PZ222222"));
            


        }
    }
}
