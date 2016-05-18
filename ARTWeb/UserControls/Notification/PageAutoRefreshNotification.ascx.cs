using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class UserControls_Notification_PageAutoRefreshNotification : System.Web.UI.UserControl
{
    ReconciliationPeriodStatusMstInfo oRecPeriodStatusInfo = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        //    rnPageAutoRefresh.Value = Request.Url.ToString();
        //    SetSessionExpiryNotification();
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (oRecPeriodStatusInfo == null)
            oRecPeriodStatusInfo = SessionHelper.GetRecPeriodStatus();
        if (oRecPeriodStatusInfo != null && oRecPeriodStatusInfo.ReconciliationPeriodStatusID.HasValue)
            hdnPreviousPeriodStatusID.Value = oRecPeriodStatusInfo.ReconciliationPeriodStatusID.Value.ToString();
    }

    private void SetSessionExpiryNotification()
    {
        // hdnTimeoutWarningInterval.Value = "10";
        rnPageAutoRefresh.ShowInterval = 3000;
        rnPageAutoRefresh.AutoCloseDelay = 3000;
    }

    protected void OnCallbackUpdate(object sender, RadNotificationEventArgs e)
    {
        oRecPeriodStatusInfo = SessionHelper.GetRecPeriodStatus();
        if (oRecPeriodStatusInfo != null 
            && hdnPreviousPeriodStatusID.Value != oRecPeriodStatusInfo.ReconciliationPeriodStatusID.GetValueOrDefault().ToString())
        {
            e.Value = oRecPeriodStatusInfo.ReconciliationPeriodStatusID.GetValueOrDefault().ToString();
            SessionHelper.ClearRecStatusFromSession();
            //rnPageAutoRefresh.Show();
        }
    }

    public bool IsAutoPageRefreshRequired()
    {
        return true;
    }
}