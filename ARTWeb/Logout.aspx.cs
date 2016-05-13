using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Utility;
using SkyStem.Language.LanguageUtility;
using System.Globalization;

public partial class Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string loginURL = this.ResolveUrl("~/Login.aspx");
        // Put user code to initialize the page here
        UserHdrInfo oUserInfo = SessionHelper.GetCurrentUser();
        // Clear out the Session
        Session.Abandon();

        if (oUserInfo != null)
        {
            // Means user clicked on Logout Link
            SessionHelper.RedirectToUrl(loginURL);
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
