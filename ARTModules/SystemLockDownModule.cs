using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using SkyStem.ART.App.Services;
using SkyStem.ART.Client.Model;
using System.Web.SessionState;
using System.Diagnostics;
using SkyStem.ART.App;
using SkyStem.ART.Client.Data;

namespace ARTModules
{
    public class SystemLockdownModule : IHttpModule
    {
        #region IHttpModule Members

        private HttpApplication cApp;


        public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {
            cApp = context;
            // context.BeginRequest += new EventHandler(context_BeginRequest);
            cApp.PreRequestHandlerExecute += new EventHandler(context_PreRequestHandlerExecute);
        }

        void context_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            HttpContext currentContext = ((HttpApplication)sender).Context;
            if (currentContext.Session != null)
            {
                if (currentContext.Session[ARTConstants.APP_NAME + "SessionKey_" + "CurrentCompanyID"] != null)
                {
                    UserHdrInfo oUserHdrInfo = null;
                    string userInfoKey = ARTConstants.APP_NAME + "SessionKey_" + "UserInfo";
                    if (HttpContext.Current.Session != null && HttpContext.Current.Session[userInfoKey] != null)
                    {
                        oUserHdrInfo = (UserHdrInfo)HttpContext.Current.Session[userInfoKey];
                    }
                    if (oUserHdrInfo != null && oUserHdrInfo.CompanyID.HasValue)
                    {
                        AppUserInfo oAppUserInfo = new AppUserInfo();
                        oAppUserInfo.CompanyID = oUserHdrInfo.CompanyID;
                        oAppUserInfo.UserID = oUserHdrInfo.UserID;
                        oAppUserInfo.LoginID = oUserHdrInfo.LoginID;
                        SystemLockdown oSystemLockdown = new SystemLockdown();
                        SystemLockdownInfo oSystemLockdownInfo = null;
                        int timeOut = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings.Get("SystemLockdownTimeOutIntervalInMinutes"));
                        if (timeOut == 0)
                            timeOut = 30; // Default timeout
                        oSystemLockdownInfo = oSystemLockdown.GetSystemLockdownStautsAndHandleTimeout(oUserHdrInfo.CompanyID, timeOut, oAppUserInfo);
                        if (oSystemLockdownInfo != null)
                        {
                            string url = @"~\pages\GLProcessError.aspx?ReasonID=" + oSystemLockdownInfo.SystemLockdownReasonID.ToString();
                            cApp.Context.Server.Transfer(url, true);
                        }
                    }
                }
            }
        }
        #endregion
    }

}
