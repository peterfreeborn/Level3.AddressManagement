using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Level3.AddressManagement.BLL;

namespace Level3.AddressManagement.UnitTest
{
    [TestClass]
    public class SystemLogItemUtilTester
    {
        [TestMethod]
        public void Test_Insert()
        {
            SystemLogItemUtil.InsertLogItem("This is the note", "This is the action");

        }

        [TestMethod]
        public void Test_Insert_Failing()
        {
            string strNote = String.Concat("The back-end console application just sent the SYSTEM STATs email to all recipients.");
            SystemLogItemUtil.InsertLogItem(strNote, "The back-end console application just sent the SYSTEM STATs email to all recipients.");
        }

        
    }
}
