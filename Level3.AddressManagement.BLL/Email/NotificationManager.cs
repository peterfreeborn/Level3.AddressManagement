using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level3.AddressManagement.BLL
{
    public class NotificationManager
    {
        // Daily Stats

        // User Notification

        // Public Properties
        private List<string> _lstErrorMessages;

        public List<string> ErrorMessages
        {
            get { return _lstErrorMessages; }
            set { _lstErrorMessages = value; }
        }


        public NotificationManager()
        {
            _lstErrorMessages = new List<string>();
        }


        public bool SendEmail_Stats()
        {
            try
            {
                EmailBodyHelper objEmailBodyHelper = new EmailBodyHelper();

                string strEmailBody;
                if (objEmailBodyHelper.GetEmailBody_HTML(EmailTemplates.EmailTemplate_Stats, out strEmailBody, DateTime.Now.AddDays(-1)) == false)
                {
                    _lstErrorMessages.AddRange(objEmailBodyHelper.ErrorMessages);
                    throw new Exception("There was an error while trying to create the email body");
                }

                SMTPEmailUtility objSMTPEmailUtility = new SMTPEmailUtility();
                if (objSMTPEmailUtility.SendEmail("Address Management : System STATs", strEmailBody, ConfigHelper.GetStatsEmailRecipientList(), true))
                {
                    // Log the successful send
                }
                else
                {
                    _lstErrorMessages.AddRange(objSMTPEmailUtility.ErrorMessages);
                }

            }
            catch (Exception ex)
            {
                _lstErrorMessages.Add(ex.Message);
            }


            return (_lstErrorMessages.Count == 0);
        }

        public bool SendEmail_UserActionNeededReminder()
        {
            try
            {
                EmailBodyHelper objEmailBodyHelper = new EmailBodyHelper();

                string strEmailBody;
                if (objEmailBodyHelper.GetEmailBody_HTML(EmailTemplates.EmailTemplate_UserActionNeededReminder, out strEmailBody, DateTime.Now) == false)
                {
                    _lstErrorMessages.AddRange(objEmailBodyHelper.ErrorMessages);
                    throw new Exception("There was an error while trying to create the email body");
                }

                SMTPEmailUtility objSMTPEmailUtility = new SMTPEmailUtility();
                if (objSMTPEmailUtility.SendEmail("Address Management Reminder:  Failed or Abandoned Transactions", strEmailBody, ConfigHelper.GetSystemNotificationEmailRecipientList(), true))
                {
                    // Log the successful send
                }
                else
                {
                    _lstErrorMessages.AddRange(objSMTPEmailUtility.ErrorMessages);
                }

            }
            catch (Exception ex)
            {
                _lstErrorMessages.Add(ex.Message);
            }


            return (_lstErrorMessages.Count == 0);
        }


    }


    public enum EmailTemplates
    {
        EmailTemplate_Stats,
        EmailTemplate_UserActionNeededReminder
    }
}
