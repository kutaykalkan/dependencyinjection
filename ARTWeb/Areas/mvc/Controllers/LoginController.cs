using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Utility;
using SkyStem.Language.LanguageUtility;
using SkyStem.Language.LanguageUtility.Classes;

namespace SkyStem.ART.Web.Areas.mvc.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index(string old)
        {
            if (old != null) return Redirect("login.aspx?old=true");

            return View();
        }

        [HttpPost]
        [ActionName("Index")]        
        public ActionResult IndexPost(string user_name, string password)
        {
            try
            {
                if (password.Trim() != string.Empty)
                {
                    //Min 6 characters,at least one caps, at least 1 letter and 1 number is required, special characters optional

                    var loginId = user_name.Trim();                    
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
                        userHdrInfo = null;
                        //TODO: CG
                        //hlForgotPassword.Visible = false;
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
                                CompanyHdrInfo oCompanyHdrInfo = Helper.GetCompanyInfoLiteObject(userHdrInfo.CompanyID);

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

                        MembershipUser mUser = Membership.GetUser(loginId);

                        if (mUser == null)
                        {
                            mUser = Membership.CreateUser(loginId, "3rdp@rty", "xyz@tdsc.com", "no question", "no answer", true, out obj);
                        }
                        FormsAuthentication.RedirectFromLoginPage(mUser.UserName, false);

                        // check for SkyStem Admin
                        if (userHdrInfo.DefaultRoleID != (short)WebEnums.UserRole.SKYSTEM_ADMIN)
                        {
                            // Set the Current Company as User's Company
                            SessionHelper.CurrentCompanyID = userHdrInfo.CompanyID;
                        }
                        // Company User
                        //TODO: CG
                        //Helper.RedirectToHomePage();
                        var url = SessionHelper.ResolveUrl(Helper.GetHomePageUrl());
                        return Redirect(url);

                    }
                    else
                    {
                        //TODO: CG
                        //Helper.FormatAndShowErrorMessage(lblErrorMessage, LanguageUtil.GetValue(5000001));
                        return View();
                    }

                }
            }
            catch (ARTException ex)
            {
                //TODO: CG
                //Helper.FormatAndShowErrorMessage(lblErrorMessage, ex);
            }
            catch (Exception ex)
            {
                //TODO: CG
                //Helper.FormatAndShowErrorMessage(lblErrorMessage, ex);
            }            
            return Redirect("login.aspx");
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
    }
}