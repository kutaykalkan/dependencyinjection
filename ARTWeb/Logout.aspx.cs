using System;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Utility;
using SkyStem.Language.LanguageUtility;
using System.Globalization;
using SkyStem.ART.Web.Data;

public partial class Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Abandon();
        if (Request.QueryString["old"] == null)
        {
            //Response.Redirect($"{ResolveUrl("~/login")}?{QueryStringConstants.LOGOUT_MESSAGE}=true");
            SessionHelper.RedirectToUrl($"{ResolveUrl("~/login")}?{QueryStringConstants.LOGOUT_MESSAGE}=true");
            return;
        }
        string loginURL = this.ResolveUrl("~/Login.aspx");
        // Put user code to initialize the page here
        UserHdrInfo oUserInfo = SessionHelper.GetCurrentUser();
        // Clear out the Session
        Session.Abandon();

        if (oUserInfo != null)
        {
            // Means user clicked on Logout Link
            SessionHelper.RedirectToUrl(loginURL);
            return;
        }
        else
        {
            // Since it was Session timeout, so set the default values again
            // Get the Browser Language and store in Session
            CultureInfo oCurrentCultureInfo = CultureInfo.CreateSpecificCulture(Request.UserLanguages[0]);

            // Check for Test LCID
            oCurrentCultureInfo = Helper.GetTestCurrentCultureInfoWithoutSession(oCurrentCultureInfo);
            System.Threading.Thread.CurrentThread.CurrentCulture = oCurrentCultureInfo;
            System.Threading.Thread.CurrentThread.CurrentUICulture = oCurrentCultureInfo;

            // For Login Page - Business Entity is Default
            LanguageUtil.SetMultilingualAttributes(AppSettingHelper.GetApplicationID(), oCurrentCultureInfo.LCID, AppSettingHelper.GetDefaultBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID(), AppSettingHelper.GetDefaultBusinessEntityID());

            lblLoginLinkMessage.Text = String.Format(LanguageUtil.GetValue(5000010), loginURL);
            this.Title = LanguageUtil.GetValue(1719);
        }
    }
}
