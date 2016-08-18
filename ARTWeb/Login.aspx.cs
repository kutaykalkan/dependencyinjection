using System;
using System.Web.UI;
using SkyStem.Language.LanguageUtility;
using System.Text.RegularExpressions;

using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.IServices;
using System.Globalization;
using SkyStem.ART.Client.Exception;
using System.Data.SqlClient;
using SkyStem.ART.Client.Data;
using System.Web;
using System.Web.Security;
using System.Collections.Generic;
using System.Text;
using SkyStem.Language.LanguageUtility.Classes;

namespace SkyStem.ART.Web
{
    public partial class Login : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["old"] == null) Response.Redirect("login");

            // Get the Browser Language and store in Session
            CultureInfo oCurrentCultureInfo = CultureInfo.CreateSpecificCulture(Request.UserLanguages[0]);

            // Check for Test LCID
            oCurrentCultureInfo = Helper.GetTestCurrentCultureInfoWithoutSession(oCurrentCultureInfo);
            System.Threading.Thread.CurrentThread.CurrentCulture = oCurrentCultureInfo;
            System.Threading.Thread.CurrentThread.CurrentUICulture = oCurrentCultureInfo;

            // For Login Page - Business Entity is Default
            LanguageUtil.SetMultilingualAttributes(AppSettingHelper.GetApplicationID(), oCurrentCultureInfo.LCID, AppSettingHelper.GetDefaultBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID(), AppSettingHelper.GetDefaultBusinessEntityID());

            if (!Page.IsPostBack)
            {
                // Set the Error Message
                rfvUserName.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1003);
                rfvPassword.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1004);
            }

            this.Title = LanguageUtil.GetValue(1002);
            Page.SetFocus(txtUserName);
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid && txtPassword.Text.Trim() != string.Empty)
                {
                    //Min 6 characters,at least one caps, at least 1 letter and 1 number is required, special characters optional
                    UserHdrInfo oUserHdrInfo = new UserHdrInfo();

                    string loginID = txtUserName.Text.Trim();
                    string password = txtPassword.Text;
                    string hashedPassword = Helper.GetHashedPassword(password);

                    IUser oUserClient = RemotingHelper.GetUserObject();

                    oUserHdrInfo = oUserClient.AuthenticateUser(loginID, hashedPassword);

                    /*
                     * 1. Check for Valid UserName / Password - If NOT valid, show Error on UI (Invalid UserName / Password)
                     * 2. Active User -> Your account is deactivated. Please contact system administrator. 
                     * 3. Active Company -> The Company is no longer available. Please contact system administrator.
                     * 3a.               -> The Company's Subscription has expired. Please contact system administrator. 
                     * 3b.               -> The Company Subscription has not yet started. Please contact system administrator. 
                     * 4. Role Check -> You do not have Roles assigned. Please contact system administrator. 
                     */
                    // Check whether account is locked, if locked then disable FTP and Send Notification to SA
                    if (oUserHdrInfo != null && oUserHdrInfo.IsUserLocked.GetValueOrDefault())
                    {
                        FTPHelper.SetupFTPUser(oUserHdrInfo, false);
                        SendNotificationToSystemAdmin(oUserHdrInfo);
                        oUserHdrInfo = null;
                        hlForgotPassword.Visible = false;
                        throw new ARTException(5000411);
                    }

                    if (oUserHdrInfo != null)
                    {
                        if (oUserHdrInfo.IsActive == false)
                        {
                            // 2. Your account is deactivated. Please contact system administrator. 
                            throw new ARTSystemException(5000012);
                        }
                        else
                        {
                            if (oUserHdrInfo.CompanyID != null
                                && oUserHdrInfo.CompanyID != 0)
                            {
                                CompanyHdrInfo oCompanyHdrInfo = Helper.GetCompanyInfoLiteObject(oUserHdrInfo.CompanyID);

                                if (oCompanyHdrInfo.IsActive == false)
                                {
                                    // 3. Active Company -> The Company is no longer available. Please contact system administrator.
                                    throw new ARTSystemException(5000013);
                                }
                                else if (oCompanyHdrInfo.SubscriptionEndDate < DateTime.Now)
                                {
                                    // 3a.               -> The Company's Subscription has expired. Please contact system administrator. 
                                    throw new ARTSystemException(5000041);
                                }
                                else if (oCompanyHdrInfo.SubscriptionStartDate > DateTime.Now)
                                {
                                    // 3b. The Company Subscription has not yet started. Please contact system administrator. 
                                    throw new ARTSystemException(5000040);
                                }
                                else if (oUserHdrInfo.UserRoleByUserID == null || (oUserHdrInfo.UserRoleByUserID.Count <= 0))
                                {
                                    // 4. Role Check -> You do not have Roles assigned. Please contact system administrator. 
                                    throw new ARTSystemException(5000014);
                                }
                            }
                        }
                        // Clear the session if user is on Login Page
                        if (HttpContext.Current.Session != null && HttpContext.Current.Session[SessionConstants.USER_INFO] != null)
                        {
                            // to clear all previous values of session
                            Session.Clear();
                            //UserHdrInfo oUserHdrInfoPrevious = (UserHdrInfo)HttpContext.Current.Session[SessionConstants.USER_INFO];
                            //if (oUserHdrInfoPrevious.UserID != oUserHdrInfo.UserID)
                            //{
                            //    Session.Clear();// to clear all previous values of session
                            //}
                        }

                        SessionHelper.SetCurrentUser(oUserHdrInfo);
                        /* Set Current Role for the Logged in User
                         * If Default Role available, that becomes Current Role, 
                         * else pick the first from all available roles 
                         */
                        if (oUserHdrInfo.DefaultRoleID != null)
                        {
                            SessionHelper.CurrentRoleID = oUserHdrInfo.DefaultRoleID;
                        }
                        else
                        {
                            SessionHelper.CurrentRoleID = oUserHdrInfo.UserRoleByUserID[0].RoleID;
                        }

                        // Update the Last Logged In Information in DB
                        oUserHdrInfo.LastLoggedIn = DateTime.Now;
                        oUserClient.UpdateLastLoggedInInfo(oUserHdrInfo, Helper.GetAppUserInfo());

                        // TODO: Open an Asyc Call for Cleaning up Temporary Files

                        MembershipCreateStatus obj;

                        MembershipUser mUser = Membership.GetUser(txtUserName.Text);

                        if (mUser == null)
                        {
                            mUser = Membership.CreateUser(txtUserName.Text, "3rdp@rty", "xyz@tdsc.com", "no question", "no answer", true, out obj);
                        }
                        FormsAuthentication.RedirectFromLoginPage(mUser.UserName, false);

                        // check for SkyStem Admin
                        if (oUserHdrInfo.DefaultRoleID != (short)WebEnums.UserRole.SKYSTEM_ADMIN)
                        {
                            // Set the Current Company as User's Company
                            SessionHelper.CurrentCompanyID = oUserHdrInfo.CompanyID;
                        }
                        // Company User
                        Helper.RedirectToHomePage();
                    }
                    else
                    {
                        Helper.FormatAndShowErrorMessage(lblErrorMessage, LanguageUtil.GetValue(5000001));
                    }

                }
            }
            catch (ARTException ex)
            {
                Helper.FormatAndShowErrorMessage(lblErrorMessage, ex);
            }
            catch (Exception ex)
            {
                Helper.FormatAndShowErrorMessage(lblErrorMessage, ex);
            }
        }

        protected void cvUserName_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            string loginID = txtUserName.Text.Trim();
            bool? isUserLocked = null;
            hlForgotPassword.Visible = true;
            if (!string.IsNullOrEmpty(loginID))
            {
                IUser oIUser = RemotingHelper.GetUserObject();
                isUserLocked = oIUser.IsUserLocked(loginID);
                if (isUserLocked.HasValue && isUserLocked.Value)
                {
                    lblErrorMessage.Text = string.Empty;
                    args.IsValid = false;
                    cvUserName.ErrorMessage = LanguageUtil.GetValue(5000411);
                    hlForgotPassword.Visible = false;
                }
            }
        }

        private void SendNotificationToSystemAdmin(UserHdrInfo oLockedUserHdrInfo)
        {
            IUser oUser = RemotingHelper.GetUserObject();
            AppUserInfo oAppUserInfo = new AppUserInfo();
            oAppUserInfo.LoginID = oLockedUserHdrInfo.LoginID;
            oAppUserInfo.CompanyID = oLockedUserHdrInfo.CompanyID;
            List<UserHdrInfo> oUserHdrInfoList = oUser.SelectAllUserHdrInfoByCompanyIDAndRoleID(oLockedUserHdrInfo.CompanyID.Value, (int)WebEnums.UserRole.SYSTEM_ADMIN, oAppUserInfo);
            foreach (UserHdrInfo oUserHdrInfo in oUserHdrInfoList)
            {
                StringBuilder oMailBody = new StringBuilder();
                MultilingualAttributeInfo oMultilingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(SessionHelper.CurrentCompanyID, oUserHdrInfo.DefaultLanguageID);

                oMailBody.Append(string.Format("{0} ", LanguageUtil.GetValue(1845, oMultilingualAttributeInfo)));
                oMailBody.Append(oUserHdrInfo.Name);
                oMailBody.Append(",");
                oMailBody.Append("<br>");
                oMailBody.Append(string.Format("{0}: ", LanguageUtil.GetValue(2937, oMultilingualAttributeInfo)));
                oMailBody.Append("<br>");
                oMailBody.Append(string.Format("{0}: ", LanguageUtil.GetValue(1003, oMultilingualAttributeInfo)));
                oMailBody.Append(oLockedUserHdrInfo.Name);
                oMailBody.Append("<br>");
                oMailBody.Append(string.Format("{0}: ", LanguageUtil.GetValue(1269, oMultilingualAttributeInfo)));
                oMailBody.Append(oLockedUserHdrInfo.LoginID);
                oMailBody.Append("<br>");

                string fromAddress = AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_FROM_DEFAULT);

                oMailBody.Append("<br/><b>" + LanguageUtil.GetValue(1133, oMultilingualAttributeInfo) + "</b>");
                oMailBody.Append("<br/>" + oLockedUserHdrInfo.CompanyDisplayName);

                string mailSubject = string.Format("{0}: {1}", LanguageUtil.GetValue(2938, oMultilingualAttributeInfo), oLockedUserHdrInfo.Name);

                string toAddress = oUserHdrInfo.EmailID;
                MailHelper.SendEmail(fromAddress, toAddress, mailSubject, oMailBody.ToString());
            }
        }
    }//end of class

}//end of namespace