using System;
using System.Globalization;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using SkyStem.ART.Client.Exception;
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

            SetLanguageFromRequest();

            //TODO CG: client side error message
            // Set the Error Message
            //rfvUserName.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1003);
            //rfvPassword.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1004);
            
            ViewBag.Title = LanguageUtil.GetValue(1002);
            
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
                        ViewBag.IsUserLocked = true;
                        throw new ARTException(5000411);
                    }

                    if (userHdrInfo != null)
                    {
                        ValidateCompanyUser(userHdrInfo);

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
                        SessionHelper.CurrentRoleID = userHdrInfo.DefaultRoleID ??
                                                      userHdrInfo.UserRoleByUserID[0].RoleID;

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

            SetLanguageFromRequest();

            ViewBag.Message = LanguageUtil.GetValue(1561);
            //// Set the Error Message
            //TODO CG: not sure if we need below?
            //ViewBag.Message = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1003);
            
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
            var oAppUserInfo = new AppUserInfo {LoginID = loginId};
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

        private static void ValidateCompanyUser(UserHdrInfo userHdrInfo)
        {
            if (userHdrInfo.IsActive == false)
            {
                // 2. Your account is deactivated. Please contact system administrator. 
                throw new ARTSystemException(5000012);
            }
            if (userHdrInfo.CompanyID != null
                && userHdrInfo.CompanyID != 0)
            {
                var oCompanyHdrInfo = Helper.GetCompanyInfoLiteObject(userHdrInfo.CompanyID);

                if (oCompanyHdrInfo.IsActive == false)
                {
                    // 3. Active Company -> The Company is no longer available. Please contact system administrator.
                    throw new ARTSystemException(5000013);
                }
                if (oCompanyHdrInfo.SubscriptionEndDate < DateTime.Now)
                {
                    // 3a.               -> The Company's Subscription has expired. Please contact system administrator. 
                    throw new ARTSystemException(5000041);
                }
                if (oCompanyHdrInfo.SubscriptionStartDate > DateTime.Now)
                {
                    // 3b. The Company Subscription has not yet started. Please contact system administrator. 
                    throw new ARTSystemException(5000040);
                }
                if (userHdrInfo.UserRoleByUserID == null || (userHdrInfo.UserRoleByUserID.Count <= 0))
                {
                    // 4. Role Check -> You do not have Roles assigned. Please contact system administrator. 
                    throw new ARTSystemException(5000014);
                }
            }
        }

        private static bool SendMailToUser(UserHdrInfo oUserHdrInfo, string password, string emailId, MultilingualAttributeInfo oMultilingualAttributeInfo)
        {
            try
            {
                AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_PASSWORD);
                var oMailBody = new StringBuilder();

                oMailBody.Append($"{LanguageUtil.GetValue(1845, oMultilingualAttributeInfo)} ");
                oMailBody.Append(oUserHdrInfo.Name);
                oMailBody.Append(",");
                oMailBody.Append("<br>");

                oMailBody.Append($"{LanguageUtil.GetValue(1935, oMultilingualAttributeInfo)}: ");
                oMailBody.Append("<br>");
                oMailBody.Append($"{LanguageUtil.GetValue(1004, oMultilingualAttributeInfo)}: ");
                oMailBody.Append(password);
                oMailBody.Append("<br>");
                oMailBody.Append("<br>");
                var msg = LanguageUtil.GetValue(2384, oMultilingualAttributeInfo);
                
                oMailBody.Append(string.Format(msg, AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_SYSTEM_URL)));

                var fromAddress = AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_FROM_DEFAULT);
                oMailBody.Append("<br/>" + MailHelper.GetEmailSignature(WebEnums.SignatureEnum.SendBySkyStemSystem, fromAddress, oMultilingualAttributeInfo));


                var mailSubject = $"{LanguageUtil.GetValue(1760, oMultilingualAttributeInfo)}";

                var toAddress = emailId;
                MailHelper.SendEmail(fromAddress, toAddress, mailSubject, oMailBody.ToString());
                return true;
            }
            catch (Exception ex)
            {
                Helper.FormatAndShowErrorMessage(null, ex);
            }
            return false;
        }

        private static void SendNotificationToSystemAdmin(UserHdrInfo oLockedUserHdrInfo)
        {
            var oUser = RemotingHelper.GetUserObject();
            var oAppUserInfo = new AppUserInfo
            {
                LoginID = oLockedUserHdrInfo.LoginID,
                CompanyID = oLockedUserHdrInfo.CompanyID
            };

            if (oLockedUserHdrInfo.CompanyID == null) return;

            var oUserHdrInfoList = oUser.SelectAllUserHdrInfoByCompanyIDAndRoleID(oLockedUserHdrInfo.CompanyID.Value, (int)WebEnums.UserRole.SYSTEM_ADMIN, oAppUserInfo);
            foreach (var oUserHdrInfo in oUserHdrInfoList)
            {
                var oMailBody = new StringBuilder();
                var oMultilingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(SessionHelper.CurrentCompanyID, oUserHdrInfo.DefaultLanguageID);

                oMailBody.Append($"{LanguageUtil.GetValue(1845, oMultilingualAttributeInfo)} ");
                oMailBody.Append(oUserHdrInfo.Name);
                oMailBody.Append(",");
                oMailBody.Append("<br>");
                oMailBody.Append($"{LanguageUtil.GetValue(2937, oMultilingualAttributeInfo)}: ");
                oMailBody.Append("<br>");
                oMailBody.Append($"{LanguageUtil.GetValue(1003, oMultilingualAttributeInfo)}: ");
                oMailBody.Append(oLockedUserHdrInfo.Name);
                oMailBody.Append("<br>");
                oMailBody.Append($"{LanguageUtil.GetValue(1269, oMultilingualAttributeInfo)}: ");
                oMailBody.Append(oLockedUserHdrInfo.LoginID);
                oMailBody.Append("<br>");

                var fromAddress = AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_FROM_DEFAULT);

                oMailBody.Append("<br/><b>" + LanguageUtil.GetValue(1133, oMultilingualAttributeInfo) + "</b>");
                oMailBody.Append("<br/>" + oLockedUserHdrInfo.CompanyDisplayName);

                var mailSubject =
                    $"{LanguageUtil.GetValue(2938, oMultilingualAttributeInfo)}: {oLockedUserHdrInfo.Name}";

                var toAddress = oUserHdrInfo.EmailID;
                MailHelper.SendEmail(fromAddress, toAddress, mailSubject, oMailBody.ToString());
            }
        }

        private void SetLanguageFromRequest()
        {
            // Get the Browser Language and store in Session
            if (Request.UserLanguages == null) return;

            var oCurrentCultureInfo = CultureInfo.CreateSpecificCulture(Request.UserLanguages[0]);

            // Check for Test LCID
            oCurrentCultureInfo = Helper.GetTestCurrentCultureInfoWithoutSession(oCurrentCultureInfo);
            System.Threading.Thread.CurrentThread.CurrentCulture = oCurrentCultureInfo;
            System.Threading.Thread.CurrentThread.CurrentUICulture = oCurrentCultureInfo;

            // For Login Page - Business Entity is Default
            LanguageUtil.SetMultilingualAttributes(AppSettingHelper.GetApplicationID(), oCurrentCultureInfo.LCID,
                AppSettingHelper.GetDefaultBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID(),
                AppSettingHelper.GetDefaultBusinessEntityID());
        }

        private static bool HideForgotPassword(string username)
        {
            var isUserLocked = false;
            if (string.IsNullOrEmpty(username)) return false;

            var oIUser = RemotingHelper.GetUserObject();
            var userLocked = oIUser.IsUserLocked(username);
            if (userLocked != null) isUserLocked = (bool)userLocked;

            return isUserLocked;
        }
        //Below will hide forgot pwd link after username entered
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
    }
}