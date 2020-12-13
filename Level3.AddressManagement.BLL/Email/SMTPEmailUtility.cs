using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace Level3.AddressManagement.BLL
{
    public class SMTPEmailUtility
    {

        // Private Members
        string _strSMTPServer = "scanmail.level3.com";
        string _strEmailAddress_Sender = String.Empty;
        List<string> _lstEmailAddresses_Recipients;

        // Public Properties
        private List<string> _lstErrorMessages;
        public List<string> ErrorMessages
        {
            get { return _lstErrorMessages; }
            set { _lstErrorMessages = value; }
        }



        public SMTPEmailUtility()
        {
            _lstEmailAddresses_Recipients = new List<string>();
            _lstErrorMessages = new List<string>();
        }



        public bool SendEmail(string strSubject, string strEmailBody, string strEmailAddressString_Recipients, bool blnIsHTML = false)
        {
            try
            {
                // Validate the input params
                string strValidationError = String.Empty;

                _strEmailAddress_Sender = ConfigHelper.GetNotifactionSenderEmailAddressValue();

                if (string.IsNullOrEmpty(strSubject))
                {
                    strValidationError += ("  'Subject' cannot be blank or empty string.");
                }

                if (string.IsNullOrEmpty(strEmailAddressString_Recipients))
                {
                    strValidationError += ("  'Subject' cannot be blank or empty string.");
                }

                if (string.IsNullOrEmpty(strEmailBody))
                {
                    strValidationError += ("  'Subject' cannot be blank or empty string.");
                }

                if (string.IsNullOrEmpty(strValidationError) == false)
                {
                    throw new Exception(String.Concat("The email cannot be sent.  ", strValidationError));
                }

                // Try to send the email, using the standard .NET library and the SMTP server named in the config file
                using (MailMessage mm = new MailMessage(_strEmailAddress_Sender, strEmailAddressString_Recipients.Replace(";", ",")))
                {
                    mm.Subject = strSubject;
                    mm.Body = strEmailBody;
                    mm.IsBodyHtml = blnIsHTML;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = _strSMTPServer;
                    smtp.EnableSsl = false;
                    smtp.Send(mm);
                }
            }
            catch (Exception ex)
            {
                _lstErrorMessages.Add(ex.Message);
            }

            return (_lstErrorMessages.Count == 0);
        }

    }
}
