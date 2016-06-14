using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using System.Net.Mail;
using SkyStem.ART.Web.Data;
using System.Collections.Generic;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.IServices;
using SkyStem.Language.LanguageUtility.Classes;
/// <summary>
/// Summary description for MailHelper
/// </summary>

namespace SkyStem.ART.Web.Utility
{
    public class MailHelper
    {
        public MailHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void SendEmail(string strFromAddress, string strToAddress, string strSubject, string strBody)
        {
            MailHelper.SendEmail(strFromAddress, strToAddress, strSubject, strBody, null);

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

        public static void SendEmail(string strFromAddress, string strToAddress, string strSubject, string strBody, List<string> oFilePathCollection)
        {
            try
            {
                //smtpserver from web.config
                string smtpServer = AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_SMTP_SERVER);
                //smtpPort from web.config
                string smtpPort = AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_SMTP_PORT);
                //Network Credentials from web.config
                string userName = AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_USER_NAME);
                string pwd = AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_PASSWORD);

                string toAddressTest = AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_USE_TEST_ACCOUNT);
                //Above code commented and replaced by below code for testing  by Prafull on 01-Jun-2010
                //string toAddressTest = String.Empty;


                bool bEnableSSL = false;
                if (AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_ENABLE_SSL) != null)
                {
                    bEnableSSL = Convert.ToBoolean(AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_ENABLE_SSL));
                }

                //new instance of smtp client
                SmtpClient oSmtpClient = new SmtpClient();

                //Host
                oSmtpClient.Host = smtpServer;

                //Port
                oSmtpClient.Port = Convert.ToInt32(smtpPort);
                //
                oSmtpClient.EnableSsl = bEnableSSL;


                if (!string.IsNullOrEmpty(userName))
                {
                    //userName and password of network
                    oSmtpClient.Credentials = new System.Net.NetworkCredential(userName, pwd);
                }
                else
                {
                    oSmtpClient.UseDefaultCredentials = true;
                }

                // new instance of MailMessage           
                MailMessage oMailMessage = new MailMessage();

                // Sender Address        
                oMailMessage.From = new MailAddress(strFromAddress);
                // Recepient Address     

                if (string.IsNullOrEmpty(toAddressTest))
                {
                    AddToAddress(strToAddress, oMailMessage);
                    //mailMessage.To.Add(new MailAddress(strToAddress));
                }
                else
                {
                    // Send to test user
                    AddToAddress(toAddressTest, oMailMessage);
                }
                // Subject         
                oMailMessage.Subject = strSubject;
                // Body         
                oMailMessage.Body = strBody;
                // format of mail message      
                oMailMessage.IsBodyHtml = true;

                // Append Attachment
                if (oFilePathCollection != null)
                {
                    for (int i = 0; i < oFilePathCollection.Count; i++)
                    {
                        string fileName = oFilePathCollection[i];
                        Attachment oAttachment = new Attachment(fileName);
                        string actFileName = ExportHelper.GetOriginalFileName(fileName);
                        if (!string.IsNullOrEmpty(actFileName))
                        {
                            oAttachment.ContentDisposition.FileName = actFileName;
                        }
                        oMailMessage.Attachments.Add(oAttachment);
                    }
                }
                oSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                //mail sent
                oSmtpClient.Send(oMailMessage);
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// GetEmailSignature() is used to append signature in mail.
        /// </summary>
        /// <param name="oSignatureEnum"></param>
        /// <returns></returns>
        public static string GetEmailSignature(SkyStem.ART.Web.Data.WebEnums.SignatureEnum oSignatureEnum, string fromAddress)
        {
            UserHdrInfo oUserHrdInfo = SessionHelper.GetCurrentUser();
            CompanyHdrInfo oCompanyHdrInfo = Helper.GetCompanyInfoLiteObject(SessionHelper.CurrentCompanyID);

            StringBuilder sb = new StringBuilder();

            sb.Append("<br/><br/>");

            switch (oSignatureEnum)
            {
                case WebEnums.SignatureEnum.SendBySystemAdmin:
                    // 2019 -- Regards
                    // 2020 -- administrator
                    // 2021 -- Please note: This is an auto-generated email. Please do not reply directly to this email.
                    sb.Append("<br/><b>" + LanguageUtil.GetValue(1133) + "</b>");
                    sb.Append("<br/>" + oCompanyHdrInfo.DisplayName);
                    break;

                case WebEnums.SignatureEnum.SendByUser:
                    IRole oIRoleClient = RemotingHelper.GetRoleObject();
                    RoleMstInfo oRoleMstInfo = oIRoleClient.GetRole(SessionHelper.CurrentRoleID, Helper.GetAppUserInfo());

                    sb.Append("<br/><b>" + oUserHrdInfo.Name + "</b>");
                    sb.Append("<br/>" + LanguageUtil.GetValue((Int32)oRoleMstInfo.RoleLabelID));
                    sb.Append("<br/>" + oCompanyHdrInfo.DisplayName);
                    break;

                case WebEnums.SignatureEnum.SendBySkyStemSystem:
                    sb.Append("<br/><b>" + LanguageUtil.GetValue(1198) + "</b>");
                    sb.Append("<br/><b>" + LanguageUtil.GetValue(1201) + "</b>");
                    break;
            }

            string fromEmailAddressConfig = AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_FROM_DEFAULT);

            if (fromAddress != fromEmailAddressConfig)
            {
                sb.Append("<br/><br/>" + LanguageUtil.GetValue(2042));
            }
            else
            {
                sb.Append("<br/><br/>" + LanguageUtil.GetValue(2021));
            }

            return sb.ToString();
        }

        /// <summary>
        /// GetEmailSignature() is used to append signature in mail.
        /// </summary>
        /// <param name="oSignatureEnum"></param>
        /// <returns></returns>
        public static string GetEmailSignature(SkyStem.ART.Web.Data.WebEnums.SignatureEnum oSignatureEnum, string fromAddress, MultilingualAttributeInfo oMultilingualAttributeInfo)
        {
            UserHdrInfo oUserHrdInfo = SessionHelper.GetCurrentUser();
            CompanyHdrInfo oCompanyHdrInfo = Helper.GetCompanyInfoLiteObject(SessionHelper.CurrentCompanyID);

            StringBuilder sb = new StringBuilder();

            sb.Append("<br/><br/>");

            switch (oSignatureEnum)
            {
                case WebEnums.SignatureEnum.SendBySystemAdmin:
                    // 2019 -- Regards
                    // 2020 -- administrator
                    // 2021 -- Please note: This is an auto-generated email. Please do not reply directly to this email.
                    sb.Append("<br/><b>" + LanguageUtil.GetValue(1133, oMultilingualAttributeInfo) + "</b>");
                    sb.Append("<br/>" + oCompanyHdrInfo.DisplayName);
                    break;

                case WebEnums.SignatureEnum.SendByUser:
                    IRole oIRoleClient = RemotingHelper.GetRoleObject();
                    RoleMstInfo oRoleMstInfo = oIRoleClient.GetRole(SessionHelper.CurrentRoleID, Helper.GetAppUserInfo());

                    sb.Append("<br/><b>" + oUserHrdInfo.Name + "</b>");
                    if (oRoleMstInfo.RoleLabelID.HasValue)
                        sb.Append("<br/>" + LanguageUtil.GetValue(oRoleMstInfo.RoleLabelID.Value, oMultilingualAttributeInfo));
                    sb.Append("<br/>" + oCompanyHdrInfo.DisplayName);
                    break;

                case WebEnums.SignatureEnum.SendBySkyStemSystem:
                    sb.Append("<br/><b>" + LanguageUtil.GetValue(1198, oMultilingualAttributeInfo) + "</b>");
                    sb.Append("<br/><b>" + LanguageUtil.GetValue(1201, oMultilingualAttributeInfo) + "</b>");
                    break;
            }

            string fromEmailAddressConfig = AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_FROM_DEFAULT);

            if (fromAddress != fromEmailAddressConfig)
            {
                sb.Append("<br/><br/>" + LanguageUtil.GetValue(2042, oMultilingualAttributeInfo));
            }
            else
            {
                sb.Append("<br/><br/>" + LanguageUtil.GetValue(2021, oMultilingualAttributeInfo));
            }

            return sb.ToString();
        }

        public static string GetAccountDetailTable(List<AccountHdrInfo> oAccountHdrInfoCollection)
        {
            StringBuilder oAccountDetailTable = new StringBuilder();
            oAccountDetailTable.Append("<table border= 1 >");
            for (int i = 0; i < oAccountHdrInfoCollection.Count; i++)
            {
                oAccountDetailTable.Append("<tr><td>");
                string AccountDetail = " manoj -kumar-mafn-safvds-asdfm dasfv-a dsfv-asdfvdasv-asdfvdasfv-sadfkd";
                AccountDetail = Helper.GetAccountEntityStringToDisplay(oAccountHdrInfoCollection[i]);
                oAccountDetailTable.AppendFormat("{0}", AccountDetail);
                oAccountDetailTable.Append(" </td>  </tr>");
            }
            oAccountDetailTable.Append(" </table>");
            return oAccountDetailTable.ToString();
        }


    }//end of class
}//end of namespace
