using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Utility;

public partial class UserControls_SessionExpiryNotification : System.Web.UI.UserControl
{
    int SessionWarningTime = Convert.ToInt32(AppSettingHelper.GetAppSettingValue(AppSettingConstants.SESSION_TIMEOUT_WARNING_INTERVAL_IN_MINUTES));
    bool displaySessionTimeoutWarning = Convert.ToBoolean(AppSettingHelper.GetAppSettingValue(AppSettingConstants.DISPLAY_SESSION_TIMEOUT_WARNING));
    protected void Page_Load(object sender, EventArgs e)
    {
        RadNotification1.Value = this.ResolveUrl(URLConstants.URL_LOGOUT);
        SetSessionExpiryNotification();
    }

    private void SetSessionExpiryNotification()
    {
        if (displaySessionTimeoutWarning)
            hdnDisplaySessionTimeoutWarning.Value = "1";
        hdnSessionTimeoutWarningInterval.Value = (SessionWarningTime * 60).ToString();
        RadNotification1.ShowInterval = (Session.Timeout - SessionWarningTime) * 60000 + 6000;
        RadNotification1.AutoCloseDelay = SessionWarningTime * 60000;
    }

    protected void OnCallbackUpdate(object sender, RadNotificationEventArgs e)
    {
        Session[SessionConstants.DATE_TIME] = DateTime.Now.ToString();
    }
}
