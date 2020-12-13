using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;
using Level3.AddressManagement.DAL;
using System.IO;

namespace Level3.AddressManagement.BLL
{
    public class EmailBodyHelper
    {
        // log4net logger declaration
        private static ILog _objLogger = LogManager.GetLogger(typeof(EmailBodyHelper));

        private int _intPadRightToCharCount = 160;
        private char _cPad = ' ';
        private List<string> _lstGreetings;
        private string _strHeaderSystemName = "ADDRESS MANAGEMENT";
        private string _strHeaderSystemNotificationString = "(System Generated Email)";

        // Public Properties
        private List<string> _lstErrorMessages;
        public List<string> ErrorMessages
        {
            get { return _lstErrorMessages; }
            set { _lstErrorMessages = value; }
        }

        // Constructor
        public EmailBodyHelper()
        {
            _lstGreetings = new List<string>();
            _lstErrorMessages = new List<string>();
        }

        private string GetHeaderExtended()
        {
            StringBuilder sbHeader = new StringBuilder();

            sbHeader.AppendLine("".PadRight(_intPadRightToCharCount, '*'));

            sbHeader.AppendLine(GetCenteredStringForBoxedChar(" ", '*'));

            string strSystemName = GetCenteredStringForBoxedChar(_strHeaderSystemName, '*');
            sbHeader.AppendLine(GetCenteredStringForBoxedChar(_strHeaderSystemName, '*'));

            string strNotification = GetCenteredStringForBoxedChar(_strHeaderSystemNotificationString, '*');
            sbHeader.AppendLine(GetCenteredStringForBoxedChar(_strHeaderSystemNotificationString, '*'));

            sbHeader.AppendLine(GetCenteredStringForBoxedChar(" ", '*'));

            sbHeader.AppendLine("".PadRight(_intPadRightToCharCount, '*'));
            sbHeader.AppendLine().AppendLine();

            sbHeader.Append(GetRandomGreeting()).AppendLine("...").AppendLine();

            return sbHeader.ToString();
        }
        private string GetHeader()
        {
            StringBuilder sbHeader = new StringBuilder();

            sbHeader.AppendLine("".PadRight(_intPadRightToCharCount, '-'));
            sbHeader.AppendLine(GetCenteredStringForBoxedChar(_strHeaderSystemName, ' '));
            sbHeader.AppendLine(GetCenteredStringForBoxedChar(_strHeaderSystemNotificationString, ' '));
            sbHeader.AppendLine("".PadRight(_intPadRightToCharCount, '-'));
            sbHeader.AppendLine().AppendLine();

            sbHeader.Append(GetRandomGreeting()).AppendLine("...").AppendLine();

            return sbHeader.ToString();
        }
        private string GetCenteredStringForBoxedChar(string strString, char cPadChar)
        {
            int intPadValue = (_intPadRightToCharCount / 2) - (strString.Length / 2);
            string strRow = cPadChar.ToString().PadRight(intPadValue, ' ');
            strRow = String.Concat(strRow, strString).PadRight(_intPadRightToCharCount - 1, ' ') + cPadChar;
            return strRow;
        }
        private string GetFooterExtended()
        {
            StringBuilder sbFooter = new StringBuilder();
            DateTime dteNow = DateTime.Now;

            string strCreationDate = dteNow.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss \"GMT\"zzz");
            string strDateString = String.Concat("Info in this email was pulled at:  ", strCreationDate);

            sbFooter.AppendLine().AppendLine().AppendLine().AppendLine();

            sbFooter.AppendLine("".PadRight(_intPadRightToCharCount, '-'));

            sbFooter.AppendLine(GetCenteredStringForBoxedChar(strDateString, '-'));

            sbFooter.AppendLine(GetCenteredStringForBoxedChar(" ", '-'));

            string strFunny = String.Concat('"', "There are only 10 types of people in the world. Those that understand binary and those that don’t :)", '"');
            sbFooter.AppendLine(GetCenteredStringForBoxedChar(strFunny, '-'));

            sbFooter.AppendLine("".PadRight(_intPadRightToCharCount, '-'));

            return sbFooter.ToString();
        }
        private string GetFooter()
        {
            StringBuilder sbFooter = new StringBuilder();
            DateTime dteNow = DateTime.Now;

            string strCreationDate = dteNow.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss \"GMT\"zzz");
            string strDateString = String.Concat("Info in this email was pulled at:  ", strCreationDate);

            sbFooter.AppendLine().AppendLine().AppendLine().AppendLine();

            sbFooter.AppendLine("".PadRight(_intPadRightToCharCount, '-'));

            sbFooter.AppendLine(GetCenteredStringForBoxedChar(strDateString, ' '));

            string strFunny = String.Concat('"', "There are only 10 types of people in the world. Those that understand binary and those that don’t :)", '"');
            sbFooter.AppendLine(GetCenteredStringForBoxedChar(strFunny, ' '));

            sbFooter.AppendLine("".PadRight(_intPadRightToCharCount, '-'));

            return sbFooter.ToString();
        }
        private string GetRandomGreeting()
        {
            // Populate the greetings list
            PopulateGreetings();

            Random r = new Random();
            int rInt = r.Next(0, _lstGreetings.Count - 1);

            return _lstGreetings[rInt];
        }
        private void PopulateGreetings()
        {
            _lstGreetings = new List<string>();

            _lstGreetings.Add("Howdy!");
            _lstGreetings.Add("Top O' the day to ya!");
            _lstGreetings.Add(String.Concat("Happy ", DateTime.Now.ToString("dddd"), "!"));
            _lstGreetings.Add("Hello there... que pasa?!");
            _lstGreetings.Add("How's it going?!");
            _lstGreetings.Add("Get ready... I hope you've had your coffee! ;)");
        }


        public bool GetEmailBody_HTML(EmailTemplates enmEmailTemplate, out string strEmailBody_HTML, DateTime? dteDate)
        {
            // Initialize the out param
            strEmailBody_HTML = String.Empty;

            string strBaseLoggingString = String.Concat("EmailTemplate = [", enmEmailTemplate.ToString(), "]");

            try
            {
                // Get the template file contents
                string strTemplateFileContents;
                if (GetTemplateFileContents(enmEmailTemplate, out strTemplateFileContents) == false)
                {
                    throw new Exception("There was an error while trying to retrieve the email template contents");
                }



                // Declare a dictionaty to hold the replaces for each type
                Dictionary<string, string> dctTemplateValuesToReplace = new Dictionary<string, string>();

                switch (enmEmailTemplate)
                {
                    case EmailTemplates.EmailTemplate_Stats:

                        dctTemplateValuesToReplace = GetTokenValueDictionary_Stats(dteDate.Value);
                        break;

                    case EmailTemplates.EmailTemplate_UserActionNeededReminder:

                        dctTemplateValuesToReplace = GetTokenValueDictionary_UserActionNeededReminder(dteDate.Value);
                        break;

                    default:
                        throw new Exception("The EmailTempate name was not recognized.  Did you forget to add a case to the switch statement?");
                }


                // Set the out param
                strEmailBody_HTML = ReplaceTemplateTokens(strTemplateFileContents, dctTemplateValuesToReplace, enmEmailTemplate);
            }
            catch (Exception ex)
            {

                _lstErrorMessages.Add(String.Concat("There was an exception while trying to compose the email body.  Error Message = [", ex.Message, "], ", strBaseLoggingString));
            }

            return (_lstErrorMessages.Count == 0);
        }


        private Dictionary<string, string> GetTokenValueDictionary_Stats(DateTime dteDate)
        {
            OrderAddressStatHelper objOrderAddressStatHelper = new OrderAddressStatHelper();

            // Append the number of addresses ADDED
            int intNumberOfNewAddressesCreated = 0;
            string strNumberOfNewAddresses = String.Empty;
            if (objOrderAddressStatHelper.LoadAllOrderAddresss())
            {
                if (objOrderAddressStatHelper.GetNumberOfOrderAddresssAddedOrModifiedOnSpecificDay(dteDate, RecordTimeStampType.Added, out intNumberOfNewAddressesCreated))
                {
                    strNumberOfNewAddresses = intNumberOfNewAddressesCreated.ToString();
                }
                else
                {
                    strNumberOfNewAddresses = "(Could not be calculated at this time)";
                }


                // Total Number of Addresses
                int intTotalNumAddresses = 0;
                string strNumberOfTotalAddresses = String.Empty;

                if (objOrderAddressStatHelper.GetTotalNumberOfOrderAddresssInSystem(out intTotalNumAddresses))
                {
                    strNumberOfTotalAddresses = intTotalNumAddresses.ToString();
                }
                else
                {
                    strNumberOfTotalAddresses = "(Could not be calculated at this time)";
                }



                List<Model.StatValuePair> lstStatValuePair;
                string strTableMarkup = String.Empty;
                if (objOrderAddressStatHelper.GetOrderAddressCountsGroupedByStatus(out lstStatValuePair))
                {
                    strTableMarkup = GetTableRowsMarkup(lstStatValuePair);
                }
                else
                {
                    strTableMarkup = "(Could not be calculated at this time)";
                }

                List<Model.StatValuePair> lstStatValuePair_Country;
                string strTableMarkup_Currency = String.Empty;
                if (objOrderAddressStatHelper.GetOrderAddressValueGroupedByCountry(out lstStatValuePair_Country))
                {
                    strTableMarkup_Currency = GetTableRowsMarkup(lstStatValuePair_Country);
                }
                else
                {
                    strTableMarkup_Currency = "(Could not be calculated at this time)";
                }


                // Total Number of Addresses
                int intSuccessfulATIFileTransCount = 0;
                string strNumberOfSuccessfullyProcessed = String.Empty;

                if (objOrderAddressStatHelper.GetSuccessfullyProcessedCount(out intSuccessfulATIFileTransCount))
                {
                    strNumberOfSuccessfullyProcessed = intSuccessfulATIFileTransCount.ToString();
                }
                else
                {
                    strNumberOfSuccessfullyProcessed = "(Could not be calculated at this time)";
                }

                string strGreeting = GetRandomGreeting();


                Dictionary<string, string> dctTemplateValuesToReplace = new Dictionary<string, string>();
                dctTemplateValuesToReplace.Add("{Greeting}", strGreeting);
                dctTemplateValuesToReplace.Add("{Date}", dteDate.ToString("D"));
                dctTemplateValuesToReplace.Add("{NumberAddressesAdded}", strNumberOfNewAddresses);
                dctTemplateValuesToReplace.Add("{NumberTotalAddresses}", strNumberOfTotalAddresses);
                dctTemplateValuesToReplace.Add("{NumberOfSuccessfullyProcessed}", strNumberOfSuccessfullyProcessed);
                dctTemplateValuesToReplace.Add("<!--{StatusTableRows}-->", strTableMarkup);
                dctTemplateValuesToReplace.Add("<!--{CountryTableRows}-->", strTableMarkup_Currency);

                return dctTemplateValuesToReplace;
            }

            return null;

        }

        private Dictionary<string, string> GetTokenValueDictionary_UserActionNeededReminder(DateTime dteDate)
        {
            OrderAddressStatHelper objOrderAddressStatHelper = new OrderAddressStatHelper();

            if (objOrderAddressStatHelper.LoadAllOrderAddresss())
            {
                List<OrderAddress> lstOrderAddressesErroring;
                List<OrderAddress> lstOrderAddressesAbandoned;

                // Append the number of Addresses ADDED
                string strNumberOfAddressesErroring = String.Empty;
                if (objOrderAddressStatHelper.GetNumberOfOrderAddressesErroring(out lstOrderAddressesErroring))
                {
                    strNumberOfAddressesErroring = lstOrderAddressesErroring.Count.ToString();
                }
                else
                {
                    strNumberOfAddressesErroring = "(Could not be calculated at this time)";
                }

                string strNumberOfAddressesAbandoned = String.Empty;
                if (objOrderAddressStatHelper.GetNumberOfOrderAddressesAbandoned(out lstOrderAddressesAbandoned))
                {
                    strNumberOfAddressesAbandoned = lstOrderAddressesErroring.Count.ToString();
                }
                else
                {
                    strNumberOfAddressesAbandoned = "(Could not be calculated at this time)";
                }



                string strTableMarkup = String.Empty;
                string strNumberOfPaymentsNeedingAttention;

                List<OrderAddress> lstOrderAddressesNeedingAttention = new List<OrderAddress>();
                lstOrderAddressesNeedingAttention.AddRange(lstOrderAddressesErroring);
                lstOrderAddressesNeedingAttention.AddRange(lstOrderAddressesAbandoned);

                strNumberOfPaymentsNeedingAttention = lstOrderAddressesAbandoned.Count().ToString();
                strTableMarkup = GetOrderAddressTableRowsMarkup(lstOrderAddressesNeedingAttention);


                string strGreeting = GetRandomGreeting();

                Dictionary<string, string> dctTemplateValuesToReplace = new Dictionary<string, string>();
                dctTemplateValuesToReplace.Add("{Greeting}", strGreeting);

                dctTemplateValuesToReplace.Add("{Date}", dteDate.ToString("f"));

                dctTemplateValuesToReplace.Add("{NumberAddressesErrored}", strNumberOfAddressesErroring);
                dctTemplateValuesToReplace.Add("{NumberAddressesAbandoned}", strNumberOfPaymentsNeedingAttention);

                dctTemplateValuesToReplace.Add("<!--{OrderAddressTableRows}-->", strTableMarkup);

                return dctTemplateValuesToReplace;
            }

            return null;
        }


        public static string GetTableRowsMarkup(List<Model.StatValuePair> lstTableItems)
        {
            StringBuilder objSBTableRow = new StringBuilder();

            for (int i = 0; i < lstTableItems.Count; i++)
            {
                // Iterate over each project item, creating a table row for each one
                objSBTableRow.AppendLine("<tr>");
                objSBTableRow.AppendLine(String.Concat("<td class=\"stringvalue\">", lstTableItems[i].StatDescriptor, "</td>"));
                objSBTableRow.AppendLine(String.Concat("<td align=\"center\" class=\"numbervalue\">", lstTableItems[i].StatValue, "</td>"));
                objSBTableRow.AppendLine("</tr>");
            }

            return objSBTableRow.ToString();
        }
        public static string GetOrderAddressTableRowsMarkup(List<OrderAddress> lstTableItems)
        {
            StringBuilder objSBTableRow = new StringBuilder();

            for (int i = 0; i < lstTableItems.Count; i++)
            {
                // Iterate over each project item, creating a table row for each one
                objSBTableRow.AppendLine("<tr>");
                objSBTableRow.AppendLine(String.Concat("<td class=\"stringvalue\">", ((Model.OrderSystemOfRecords)lstTableItems[i].OrderSystemOfRecordID).ToString(), "</td>"));
                objSBTableRow.AppendLine(String.Concat("<td class=\"stringvalue\">", lstTableItems[i].CDWCustomerOrderNumber, "</td>"));
                objSBTableRow.AppendLine(String.Concat("<td class=\"stringvalue\">", lstTableItems[i].CDWAddressOne, "</td>"));
                objSBTableRow.AppendLine(String.Concat("<td class=\"stringvalue\">", lstTableItems[i].CDWCity, "</td>"));
                objSBTableRow.AppendLine(String.Concat("<td class=\"stringvalue\">", lstTableItems[i].CDWState, "</td>"));
                objSBTableRow.AppendLine(String.Concat("<td class=\"stringvalue\">", lstTableItems[i].CDWCountry, "</td>"));
                objSBTableRow.AppendLine(String.Concat("<td class=\"stringvalue\">", lstTableItems[i].CDWFloor, "</td>"));
                objSBTableRow.AppendLine(String.Concat("<td class=\"stringvalue\">", lstTableItems[i].CDWRoom, "</td>"));
                objSBTableRow.AppendLine(String.Concat("<td class=\"stringvalue\">", lstTableItems[i].CDWSuite, "</td>"));
                objSBTableRow.AppendLine(String.Concat("<td class=\"stringvalue\">", ((Model.MigrationStatuses)lstTableItems[i].MigrationStatusID).ToString(), "</td>"));
                objSBTableRow.AppendLine(String.Concat("<td class=\"stringvalue\">", ((Model.OrderAddressTypes)lstTableItems[i].OrderAddressTypeID).ToString(), "</td>"));

                objSBTableRow.AppendLine("</tr>");
            }

            return objSBTableRow.ToString();
        }

        public bool GetTemplateFileContents(EmailTemplates enmEmailTemplate, out string strTemplateContents)
        {
            strTemplateContents = string.Empty;

            try
            {
                // Calculate the template names using the enum type name
                string strEmailTemplateDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Replace("file:\\", ""); // //Directory.GetCurrentDirectory()

                string strTemplateFilePath = System.IO.Path.Combine(strEmailTemplateDirectory, "Content", String.Concat(enmEmailTemplate.ToString(), ".html"));
                strTemplateContents = System.IO.File.ReadAllText(strTemplateFilePath);

            }
            catch (Exception ex)
            {
                _lstErrorMessages.Add(String.Concat("There was an exception while trying to retrieve the contents of the Email Template file from disk.  Error Message = [", ex.Message, "]"));
            }

            return (_lstErrorMessages.Count == 0);
        }
        public string ReplaceTemplateTokens(string strTemplateContents, Dictionary<string, string> dctTemplateValuesToReplace, EmailTemplates enmEmailTemplate)
        {
            try
            {
                // Do token/value replacements on the body and subject of the email template.
                if (dctTemplateValuesToReplace != null && dctTemplateValuesToReplace.Count > 0)
                {
                    _objLogger.Debug("Begin token replacements...");
                    foreach (string strKey in dctTemplateValuesToReplace.Keys)
                    {
                        _objLogger.Debug(String.Concat("   Key/Value: " + strKey + "/" + dctTemplateValuesToReplace[strKey]));
                        strTemplateContents = strTemplateContents.Replace(strKey, dctTemplateValuesToReplace[strKey]);
                    }
                    _objLogger.Debug("Token replacements complete.");
                }
                else
                {
                    _objLogger.Debug("The token replacement dictionary is empty; no token replacements needed.");
                }
            }
            catch (Exception ex)
            {
                _objLogger.Error("An unhandled exception was thrown while processing the token replacements within the email template. Template = [" + enmEmailTemplate.ToString() + "], ErrorMessage = [" + ex.Message + "]");
            }

            return strTemplateContents;
        }


    }
}
