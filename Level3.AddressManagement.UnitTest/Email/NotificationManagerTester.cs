using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Level3.AddressManagement.BLL;

namespace Level3.AddressManagement.UnitTest.Email
{
    [TestClass]
    public class NotificationManagerTester
    {
        [TestMethod]
        public void Test_SendStatsEmail()
        {
            NotificationManager objNotificationManager = new NotificationManager();
            Assert.IsTrue(objNotificationManager.SendEmail_Stats());
        }

        [TestMethod]
        public void Test_SendUserActionReminderEmail()
        {
            NotificationManager objNotificationManager = new NotificationManager();
            Assert.IsTrue(objNotificationManager.SendEmail_UserActionNeededReminder());
        }
    }
}
