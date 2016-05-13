using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using SkyStem.ART.Shared.Data;

namespace SkyStem.ART.Shared.Utility
{
    public class MailHelper
    {
        public static bool SendMail(string FromMailID, string ToMailID, string MailSubject, string MailBody, List<string> oFilePathCollection,
            string HostName, int PortNumber, bool EnableSSL, string NetworkUserID, string NetworkPassword)
        {
            #region Send Mail

            bool isMailSuccessFull = false;
            string toAddressTest = SharedAppSettingHelper.GetAppSettingValue(SharedAppSettingConstants.EMAIL_USE_TEST_ACCOUNT);
            if (!string.IsNullOrEmpty(ToMailID.Trim() + toAddressTest.Trim()))
            {
                SmtpClient oSmtpClient = new SmtpClient();

                if (!string.IsNullOrEmpty(NetworkUserID))
                {
                    //userName and password of network
                    oSmtpClient.Credentials = new System.Net.NetworkCredential(NetworkUserID, NetworkPassword);
                }
                else
                {
                    oSmtpClient.UseDefaultCredentials = true;
                }

                //Host
                oSmtpClient.Host = HostName;

                //Port
                oSmtpClient.Port = PortNumber;
                //
                oSmtpClient.EnableSsl = EnableSSL;

                MailMessage oMailMessage = new MailMessage();

                // Sender Address        
                oMailMessage.From = new MailAddress(FromMailID);
                // Recepient Address     

                if (string.IsNullOrEmpty(toAddressTest))
                {
                    //mailMessage.To.Add(new MailAddress(strToAddress));
                    AddToAddress(ToMailID, oMailMessage);
                }
                else
                {
                    // Send to test user
                    AddToAddress(toAddressTest, oMailMessage);
                }
                // Subject         
                oMailMessage.Subject = MailSubject;
                // Body         
                oMailMessage.Body = MailBody;
                oMailMessage.IsBodyHtml = true;

                if (oFilePathCollection != null)
                {
                    for (int i = 0; i < oFilePathCollection.Count; i++)
                    {
                        Attachment oAttachment = new Attachment(oFilePathCollection[i]);
                        oMailMessage.Attachments.Add(oAttachment);
                    }
                }

                oSmtpClient.Send(oMailMessage);
                isMailSuccessFull = true;
            }
            return isMailSuccessFull;
            #endregion
        }

        private static void AddToAddress(string strToAddress, MailMessage mailMessage)
        {
            char[] separators = new char[2];
            separators[0] = ',';
            separators[1] = ';';

            string[] toAddressArray = strToAddress.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            // Send to test user
            foreach (var toAddr in toAddressArray)
            {
                if (!string.IsNullOrEmpty(toAddr))
                    mailMessage.To.Add(new MailAddress(toAddr));
            }
        }


        public static string GetBeginTableHTML()
        {
            return "<table border='1' cellspacing='0' cellpadding='3' width=100% style='font-family: Arial;font-size: 11px;border-collapse: collapse;'>";
        }
        public static string GetEndTableHTML()
        {
            return "</table>";
        }
        public static string GetBeginHaderRowHTML()
        {
            return "<tr style='background: #ab6501; color: #ffffff;height:25px;'>";
        }
        public static string GetBeginRowHTML()
        {
            return "<tr>";
        }
        public static string GetBeginRowHTML(string style)
        {
            return "<tr " + style + ">";
        }
        public static string GetEndRowHTML()
        {
            return "</tr>";
        }
        public static string GetBeginColumnHTML()
        {
            return "<td>";
        }
        public static string GetBeginColumnHTML(string style)
        {
            return "<td " + style + ">";
        }
        public static string GetEndColumnHTML()
        {
            return "</td>";
        }
    }
}
