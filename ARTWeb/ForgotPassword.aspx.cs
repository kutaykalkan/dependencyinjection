using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.Language.LanguageUtility;

using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using System.Text;
using System.Globalization;
using SkyStem.ART.Client.Exception;
using SkyStem.Language.LanguageUtility.Classes;
using SkyStem.ART.Shared.Data;

namespace SkyStem.ART.Web
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        #region "Event Handlers"
        #region "Page Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = LanguageUtil.GetValue(1005);
            //// Get the Browser Language and store in Session
            CultureInfo oCurrentCultureInfo = CultureInfo.CreateSpecificCulture(Request.UserLanguages[0]);

            // Check for Test LCID
            oCurrentCultureInfo = Helper.GetTestCurrentCultureInfoWithoutSession(oCurrentCultureInfo);
            System.Threading.Thread.CurrentThread.CurrentCulture = oCurrentCultureInfo;
            System.Threading.Thread.CurrentThread.CurrentUICulture = oCurrentCultureInfo;

            //// For Login Page - Business Entity is Default
            LanguageUtil.SetMultilingualAttributes(AppSettingHelper.GetApplicationID(), oCurrentCultureInfo.LCID, AppSettingHelper.GetDefaultBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID(), AppSettingHelper.GetDefaultBusinessEntityID());

            //// Set the Error Message
            rfvUserName.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1003);
            Page.SetFocus(txtUserName);
            pnlForgotPassword.DefaultButton = "btnGetPassword";
        }
        #endregion
        #region "Other Events"
        protected void btnGetPassword_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string loginID = txtUserName.Text.Trim();
                string generatedPassword = Helper.CreateRandomPassword(SharedConstants.LENGTH_GENERATED_PASSWORD, loginID);
                string hashedPassword = Helper.GetHashedPassword(generatedPassword);
                IUser oUserClient = RemotingHelper.GetUserObject();
                AppUserInfo oAppUserInfo = new AppUserInfo();
                oAppUserInfo.LoginID = loginID;
                int result = oUserClient.UpdatePassword(loginID, hashedPassword, oAppUserInfo);

                if (result > 0)
                {
                    UserHdrInfo oUserHdrInfo = new UserHdrInfo();

                    oUserHdrInfo = oUserClient.GetUserByLoginID(loginID, oAppUserInfo);
                    // Create multilingual attribute info
                    MultilingualAttributeInfo oMultilingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(SessionHelper.CurrentCompanyID, oUserHdrInfo.DefaultLanguageID);

                    if (SendMailToUser(oUserHdrInfo, generatedPassword, oUserHdrInfo.EmailID, oMultilingualAttributeInfo))
                    {
                        string msg = string.Format(LanguageUtil.GetValue(1537), oUserHdrInfo.EmailID);
                        lblErrorMessage.CssClass = "ConfirmationLabel";
                        lblErrorMessage.Visible = true;
                        lblErrorMessage.Text = msg;
                    }
                    else
                    {
                        lblErrorMessage.CssClass = "ErrorLabel";
                        Helper.FormatAndShowErrorMessage(lblErrorMessage, LanguageUtil.GetValue(5000386));
                    }
                }
                else
                {
                    lblErrorMessage.CssClass = "ErrorLabel";
                    Helper.FormatAndShowErrorMessage(lblErrorMessage, LanguageUtil.GetValue(5000004));
                }
            }
        }
        #endregion
        #endregion
        #region Validation Control Events
        protected void cvUserName_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            string loginID = txtUserName.Text.Trim();
            bool? isUserLocked = null;
            if (!string.IsNullOrEmpty(loginID))
            {
                IUser oIUser = RemotingHelper.GetUserObject();
                isUserLocked = oIUser.IsUserLocked(loginID);
                if (isUserLocked.HasValue && isUserLocked.Value)
                {
                    args.IsValid = false;
                    cvUserName.ErrorMessage = LanguageUtil.GetValue(5000411);
                }
            }
        }

        #endregion
        #region "Private Methods"
        private bool SendMailToUser(UserHdrInfo oUserHdrInfo, string password, string emailID, MultilingualAttributeInfo oMultilingualAttributeInfo)
        {
            try
            {
                AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_PASSWORD);
                StringBuilder oMailBody = new StringBuilder();


                oMailBody.Append(string.Format("{0} ", LanguageUtil.GetValue(1845, oMultilingualAttributeInfo)));
                oMailBody.Append(oUserHdrInfo.Name);
                oMailBody.Append(",");
                oMailBody.Append("<br>");

                oMailBody.Append(string.Format("{0}: ", LanguageUtil.GetValue(1935, oMultilingualAttributeInfo)));
                oMailBody.Append("<br>");
                oMailBody.Append(string.Format("{0}: ", LanguageUtil.GetValue(1004, oMultilingualAttributeInfo)));
                oMailBody.Append(password);
                oMailBody.Append("<br>");
                oMailBody.Append("<br>");
                String msg;
                msg = LanguageUtil.GetValue(2384, oMultilingualAttributeInfo);
                String Url = Request.Url.AbsoluteUri;
                string NewUrl = Url.Replace("Pages/CreateUser.aspx", AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_SYSTEM_URL));
                oMailBody.Append(string.Format(msg, AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_SYSTEM_URL)));

                string fromAddress = AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_FROM_DEFAULT);
                oMailBody.Append("<br/>" + MailHelper.GetEmailSignature(WebEnums.SignatureEnum.SendBySkyStemSystem, fromAddress, oMultilingualAttributeInfo));


                string mailSubject = string.Format("{0}", LanguageUtil.GetValue(1760, oMultilingualAttributeInfo));

                string toAddress = emailID;
                MailHelper.SendEmail(fromAddress, toAddress, mailSubject, oMailBody.ToString());
                return true;
            }
            catch (Exception ex)
            {
                Helper.FormatAndShowErrorMessage(null, ex);
            }
            return false;
        }
        #endregion
    }
}//end of namespace