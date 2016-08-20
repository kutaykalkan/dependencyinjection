using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Shared.Data;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Utility;
using SkyStem.Language.LanguageUtility;
using SkyStem.Language.LanguageUtility.Classes;
using SkyStem.Library.Controls.WebControls;

namespace SkyStem.ART.Web.Areas.mvc.Controllers
{
    public class LoginController : Controller
    {
        private readonly ExLabel _label = new ExLabel();

        [HttpGet]
        public ActionResult Index(string old)
        {
            if (old != null) return Redirect("~/login.aspx?old=true");

            if (Request.QueryString[QueryStringConstants.LOGOUT_MESSAGE] != null)
            {
                ViewBag.Message = "Your session has ended, please log in.";
                return View();
            }

            // Get the Browser Language and store in Session
            if (Request.UserLanguages != null)
            {
                var oCurrentCultureInfo = CultureInfo.CreateSpecificCulture(Request.UserLanguages[0]);

                // Check for Test LCID
                oCurrentCultureInfo = Helper.GetTestCurrentCultureInfoWithoutSession(oCurrentCultureInfo);
                System.Threading.Thread.CurrentThread.CurrentCulture = oCurrentCultureInfo;
                System.Threading.Thread.CurrentThread.CurrentUICulture = oCurrentCultureInfo;

                // For Login Page - Business Entity is Default
                LanguageUtil.SetMultilingualAttributes(AppSettingHelper.GetApplicationID(), oCurrentCultureInfo.LCID, AppSettingHelper.GetDefaultBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID(), AppSettingHelper.GetDefaultBusinessEntityID());
            }

            
            //TODO CG: client side error message
            // Set the Error Message
            //rfvUserName.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1003);
            //rfvPassword.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1004);
            
            ViewBag.Title = LanguageUtil.GetValue(1002);
            //TODO CG: Focus
            //Page.SetFocus(txtUserName);

            return View();
        }

        [HttpPost]
        [ActionName("Index")]        
        public ActionResult IndexPost(string userName, string password, bool remember = false)
        {
            try
            {
                if (password.Trim() != string.Empty)
                {
                    //Min 6 characters,at least one caps, at least 1 letter and 1 number is required, special characters optional

                    var loginId = userName.Trim();                    
                    var hashedPassword = Helper.GetHashedPassword(password);

                    var userClient = RemotingHelper.GetUserObject();

                    var userHdrInfo = userClient.AuthenticateUser(loginId, hashedPassword);

                    /*
                     * 1. Check for Valid UserName / Password - If NOT valid, show Error on UI (Invalid UserName / Password)
                     * 2. Active User -> Your account is deactivated. Please contact system administrator. 
                     * 3. Active Company -> The Company is no longer available. Please contact system administrator.
                     * 3a.               -> The Company's Subscription has expired. Please contact system administrator. 
                     * 3b.               -> The Company Subscription has not yet started. Please contact system administrator. 
                     * 4. Role Check -> You do not have Roles assigned. Please contact system administrator. 
                     */
                    // Check whether account is locked, if locked then disable FTP and Send Notification to SA
                    if (userHdrInfo != null && userHdrInfo.IsUserLocked.GetValueOrDefault())
                    {
                        FTPHelper.SetupFTPUser(userHdrInfo, false);
                        SendNotificationToSystemAdmin(userHdrInfo);
                        throw new ARTException(5000411);
                    }

                    if (userHdrInfo != null)
                    {
                        if (userHdrInfo.IsActive == false)
                        {
                            // 2. Your account is deactivated. Please contact system administrator. 
                            throw new ARTSystemException(5000012);
                        }
                        else
                        {
                            if (userHdrInfo.CompanyID != null
                                && userHdrInfo.CompanyID != 0)
                            {
                                var oCompanyHdrInfo = Helper.GetCompanyInfoLiteObject(userHdrInfo.CompanyID);

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
                                else if (userHdrInfo.UserRoleByUserID == null || (userHdrInfo.UserRoleByUserID.Count <= 0))
                                {
                                    // 4. Role Check -> You do not have Roles assigned. Please contact system administrator. 
                                    throw new ARTSystemException(5000014);
                                }
                            }
                        }
                        // Clear the session if user is on Login Page
                        if (System.Web.HttpContext.Current.Session != null && System.Web.HttpContext.Current.Session[SessionConstants.USER_INFO] != null)
                        {
                            // to clear all previous values of session
                            Session.Clear();                           
                        }

                        SessionHelper.SetCurrentUser(userHdrInfo);
                        /* Set Current Role for the Logged in User
                         * If Default Role available, that becomes Current Role, 
                         * else pick the first from all available roles 
                         */
                        if (userHdrInfo.DefaultRoleID != null)
                        {
                            SessionHelper.CurrentRoleID = userHdrInfo.DefaultRoleID;
                        }
                        else
                        {
                            SessionHelper.CurrentRoleID = userHdrInfo.UserRoleByUserID[0].RoleID;
                        }

                        // Update the Last Logged In Information in DB
                        userHdrInfo.LastLoggedIn = DateTime.Now;
                        userClient.UpdateLastLoggedInInfo(userHdrInfo, Helper.GetAppUserInfo());

                        // TODO: Open an Asyc Call for Cleaning up Temporary Files

                        MembershipCreateStatus obj;

                        var mUser = Membership.GetUser(loginId) ??
                                               Membership.CreateUser(loginId, "3rdp@rty", "xyz@tdsc.com", "no question", "no answer", true, out obj);

                        if (mUser != null) FormsAuthentication.RedirectFromLoginPage(mUser.UserName, remember);

                        // check for SkyStem Admin
                        if (userHdrInfo.DefaultRoleID != (short)WebEnums.UserRole.SKYSTEM_ADMIN)
                        {
                            // Set the Current Company as User's Company
                            SessionHelper.CurrentCompanyID = userHdrInfo.CompanyID;
                        }
                        // Company User                       
                        var url = SessionHelper.ResolveUrl(Helper.GetHomePageUrl());
                        return Redirect(url);

                    }                    
                    Helper.FormatAndShowErrorMessage(_label, LanguageUtil.GetValue(5000001));
                    ViewBag.Message = _label.Text;
                    return View();
                }
            }
            catch (ARTException ex)
            {
                Helper.FormatAndShowErrorMessage(_label, ex);
                ViewBag.Message = _label.Text;
            }
            catch (Exception ex)
            {
                Helper.FormatAndShowErrorMessage(_label, ex);
                ViewBag.Message = _label.Text;
            }
            return View();
        }

        [HttpGet]
        public ActionResult ForgotPassword(string old)
        {
            ViewBag.Title = LanguageUtil.GetValue(1005);
            //// Get the Browser Language and store in Session
            if (Request.UserLanguages != null)
            {
                var oCurrentCultureInfo = CultureInfo.CreateSpecificCulture(Request.UserLanguages[0]);

                // Check for Test LCID
                oCurrentCultureInfo = Helper.GetTestCurrentCultureInfoWithoutSession(oCurrentCultureInfo);
                System.Threading.Thread.CurrentThread.CurrentCulture = oCurrentCultureInfo;
                System.Threading.Thread.CurrentThread.CurrentUICulture = oCurrentCultureInfo;

                //// For Login Page - Business Entity is Default
                LanguageUtil.SetMultilingualAttributes(AppSettingHelper.GetApplicationID(), oCurrentCultureInfo.LCID, AppSettingHelper.GetDefaultBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID(), AppSettingHelper.GetDefaultBusinessEntityID());
            }

            ViewBag.Message = LanguageUtil.GetValue(1561);
            //// Set the Error Message
            //TODO CG: not sure if we need below?
            //ViewBag.Message = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1003);
            //TODO CG
            //Page.SetFocus(txtUserName);
            //pnlForgotPassword.DefaultButton = "btnGetPassword";

            return View();
        }

        [HttpPost]
        [ActionName("ForgotPassword")]
        public ActionResult ForgotPasswordPost(string userName)
        {
            var loginId = userName.Trim();
            var generatedPassword = Helper.CreateRandomPassword(SharedConstants.LENGTH_GENERATED_PASSWORD, loginId);
            var hashedPassword = Helper.GetHashedPassword(generatedPassword);
            var oUserClient = RemotingHelper.GetUserObject();
            var oAppUserInfo = new AppUserInfo();
            oAppUserInfo.LoginID = loginId;
            var result = oUserClient.UpdatePassword(loginId, hashedPassword, oAppUserInfo);

            if (result > 0)
            {
                var oUserHdrInfo = oUserClient.GetUserByLoginID(loginId, oAppUserInfo);
                // Create multilingual attribute info
                var oMultilingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(SessionHelper.CurrentCompanyID, oUserHdrInfo.DefaultLanguageID);

                if (SendMailToUser(oUserHdrInfo, generatedPassword, oUserHdrInfo.EmailID, oMultilingualAttributeInfo))
                {
                    ViewBag.Message = LanguageUtil.GetValue(1537);                    
                }
                else
                {                    
                    Helper.FormatAndShowErrorMessage(_label, LanguageUtil.GetValue(5000386));
                    ViewBag.Message = _label.Text;
                }
            }
            else
            {                
                Helper.FormatAndShowErrorMessage(_label, LanguageUtil.GetValue(5000004));
                ViewBag.Message = _label.Text;
            }

            return View();
        }

        //TODO: CG
        //protected void cvUserName_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        //{
        //    string loginID = txtUserName.Text.Trim();
        //    bool? isUserLocked = null;
        //    hlForgotPassword.Visible = true;
        //    if (!string.IsNullOrEmpty(loginID))
        //    {
        //        IUser oIUser = RemotingHelper.GetUserObject();
        //        isUserLocked = oIUser.IsUserLocked(loginID);
        //        if (isUserLocked.HasValue && isUserLocked.Value)
        //        {
        //            lblErrorMessage.Text = string.Empty;
        //            args.IsValid = false;
        //            cvUserName.ErrorMessage = LanguageUtil.GetValue(5000411);
        //            hlForgotPassword.Visible = false;
        //        }
        //    }
        //}

        private bool SendMailToUser(UserHdrInfo oUserHdrInfo, string password, string emailId, MultilingualAttributeInfo oMultilingualAttributeInfo)
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
                //if (Request.Url != null)
                //{
                //    var url = Request.Url.AbsoluteUri;
                //    url = url.Replace("Pages/CreateUser.aspx", AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_SYSTEM_URL));
                //}
                oMailBody.Append(string.Format(msg, AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_SYSTEM_URL)));

                string fromAddress = AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_FROM_DEFAULT);
                oMailBody.Append("<br/>" + MailHelper.GetEmailSignature(WebEnums.SignatureEnum.SendBySkyStemSystem, fromAddress, oMultilingualAttributeInfo));


                string mailSubject = string.Format("{0}", LanguageUtil.GetValue(1760, oMultilingualAttributeInfo));

                string toAddress = emailId;
                MailHelper.SendEmail(fromAddress, toAddress, mailSubject, oMailBody.ToString());
                return true;
            }
            catch (Exception ex)
            {
                Helper.FormatAndShowErrorMessage(null, ex);
            }
            return false;
        }

        private void SendNotificationToSystemAdmin(UserHdrInfo oLockedUserHdrInfo)
        {
            IUser oUser = RemotingHelper.GetUserObject();
            AppUserInfo oAppUserInfo = new AppUserInfo();
            oAppUserInfo.LoginID = oLockedUserHdrInfo.LoginID;
            oAppUserInfo.CompanyID = oLockedUserHdrInfo.CompanyID;
            if (oLockedUserHdrInfo.CompanyID == null) return;
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
    }
}