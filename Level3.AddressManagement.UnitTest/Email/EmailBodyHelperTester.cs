using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Level3.AddressManagement.BLL;

namespace Level3.AddressManagement.UnitTest.Email
{
    [TestClass]
    public class EmailBodyHelperTester
    {
        [TestMethod]
        public void Test_GetTemplate()
        {
            string strContents;

            EmailBodyHelper objEmailBodyHelper = new EmailBodyHelper();
            Assert.IsTrue(objEmailBodyHelper.GetTemplateFileContents(EmailTemplates.EmailTemplate_Stats, out strContents));
            Assert.IsTrue(string.IsNullOrEmpty(strContents) == false);
        }


        [TestMethod]
        public void Test_GetTemplate_UserActionNeeded()
        {
            string strContents;

            EmailBodyHelper objEmailBodyHelper = new EmailBodyHelper();
            Assert.IsTrue(objEmailBodyHelper.GetTemplateFileContents(EmailTemplates.EmailTemplate_UserActionNeededReminder, out strContents));
            Assert.IsTrue(string.IsNullOrEmpty(strContents) == false);
        }
    }
}
