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
using SkyStem.ART.Web.Classes;
using System.Collections.Generic;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Web.WebParts;
using System.Threading;
using SkyStem.ART.Client.Data;

public partial class Pages_Home : PageBaseRole
{
    #region Variables & Constants
    #endregion

    #region Properties
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
        oMasterPageBase.ReconciliationPeriodChangedEventHandler += new EventHandler(oMasterPageBase_ReconciliationPeriodChangedEventHandler);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Request.IsAuthenticated)
            Helper.RedirectToLogoutPage();

        hlExceptionMessages.NavigateUrl = "javascript:OpenAlertRadWindowForHyperlinkWithOffset('ExceptionAlertDetail.aspx', 380, 500, '" + hlExceptionMessages.ClientID + "');";
        hlAccountMessages.NavigateUrl = "javascript:OpenAlertRadWindowForHyperlinkWithOffset('AccountAlertDetail.aspx', 380, 500, '" + hlAccountMessages.ClientID + "');";

        Helper.SetPageTitle(this, 1243);
        Helper.SetBreadcrumbs(this, LanguageUtil.GetValue(1243));

        if (!IsPostBack)
        {
            MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
            MasterPageSettings objMasterPageSettings = new MasterPageSettings();
            objMasterPageSettings.EnableWebPartCustomisation = true;
            oMasterPageBase.SetMasterPageSettings(objMasterPageSettings);
        }
    }
    protected void Page_PreRenderComplete(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDashboardsAsync();
        }
    }

    #endregion

    #region Grid Events
    #endregion

    #region Other Events
    void oMasterPageBase_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        if (SessionHelper.CurrentReconciliationPeriodID == null)
        {
            trWebParts.Visible = false;
            trErrorMessage.Visible = true;
            if (SessionHelper.CurrentRoleEnum != WebEnums.UserRole.SYSTEM_ADMIN)
            {
                lblErrorMessage.Text = string.Format(LanguageUtil.GetValue(5000062), LanguageUtil.GetValue(5000061));
            }
            else
            {
                lblErrorMessage.LabelID = 5000061;
            }

        }
        else
        {
            switch (CurrentRecProcessStatus.Value)
            {
                case WebEnums.RecPeriodStatus.Skipped:
                    trWebParts.Visible = false;
                    trErrorMessage.Visible = true;
                    lblErrorMessage.LabelID = 5000183;
                    break;

                default:
                    trWebParts.Visible = true;
                    trErrorMessage.Visible = false;
                    break;
            }
            LoadDashboardsAsync();
        }
    }
    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods
    private void LoadAlertTypeMsg()
    {
        // Show / Hide New Alert Flag 
        IAlert oAlert = RemotingHelper.GetAlertObject();
        int? exceptionType = (int)WebEnums.AlertType.EXCEPTION_TYPE;
        int? recID = SessionHelper.CurrentReconciliationPeriodID;
        bool numberOfUnReadMsgEx = oAlert.CheckIsReadMsg(SessionHelper.CurrentUserID, SessionHelper.CurrentRoleID, recID, exceptionType, Helper.GetAppUserInfo());
        if (numberOfUnReadMsgEx)
            imgReadExceptionMessages.Visible = true;
        else
            imgReadExceptionMessages.Visible = false;
        int? accountType = (int)WebEnums.AlertType.ACCOUNT_TYPE;
        bool numberOfUnReadMsgAcc = oAlert.CheckIsReadMsg(SessionHelper.CurrentUserID, SessionHelper.CurrentRoleID, recID, accountType, Helper.GetAppUserInfo());
        if (numberOfUnReadMsgAcc)
            imgReadAccountMessages.Visible = true;
        else
            imgReadAccountMessages.Visible = false;
    }

    private void LoadRoleSpecificFeatures()
    {
        // Get the Dashbaords for the User
        List<DashboardMstInfo> oDashboardMstInfoCollection = GetUserDashboardList();
        WebPartBase oWebPartBase = null;
        // Add closed web parts back
        for (int i = wpmDashboards.WebParts.Count - 1; i >= 0; i--)
        {
            if (wpmDashboards.WebParts[i].IsClosed)
            {
                oWebPartBase = (WebPartBase)wpmDashboards.WebParts[i];
                if (oWebPartBase.DefaultZoneID == 1)
                    wpmDashboards.AddWebPart(oWebPartBase, wpzLeft, 0);
                else
                    wpmDashboards.AddWebPart(oWebPartBase, wpzRight, 0);
            }
        }
        // Zone 1
        ShowHideWebPartsInZone(wpzLeft, oDashboardMstInfoCollection);

        // Zone 2
        ShowHideWebPartsInZone(wpzRight, oDashboardMstInfoCollection);
        // Remove dynamic web parts to avoid multiple copies of same webpart
        for (int i = wpzLeft.WebParts.Count - 1; i >= 0; i--)
        {
            if (!wpzLeft.WebParts[i].IsStatic)
                wpmDashboards.DeleteWebPart(wpzLeft.WebParts[i]);
        }
        for (int i = wpzRight.WebParts.Count - 1; i >= 0; i--)
        {
            if (!wpzRight.WebParts[i].IsStatic)
                wpmDashboards.DeleteWebPart(wpzRight.WebParts[i]);
        }
        // Special Case
        /*
         * If Dual Review Is Off, then we need to show the Account Rec Converage WP to Reviewer
         * So, check if Current Role is Reviewer
         */

        if (SessionHelper.CurrentRoleEnum == WebEnums.UserRole.REVIEWER)
        {
            if (Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.DualLevelReview))
            {
                if (!wpAccountReconciliationCoverage.IsClosed)
                    wpmDashboards.CloseWebPart(wpAccountReconciliationCoverage);
            }
        }
    }


    private List<DashboardMstInfo> GetUserDashboardList()
    {
        List<DashboardMstInfo> oDashboardMstInfoCollection = null;
        List<DashboardMstInfo> oActiveDashboardMstInfoCollection = null;
        List<DashboardMstInfo> oAccessibleDashboardMstInfoCollection = SessionHelper.GetDashboards();
        List<DashboardMstInfo> oPreferencesDashboardMstInfoCollection = null;
        IUser oUserClient = RemotingHelper.GetUserObject();
        oPreferencesDashboardMstInfoCollection = oUserClient.GetUserDashboardPreferences(SessionHelper.CurrentUserID, SessionHelper.CurrentRoleID, Helper.GetAppUserInfo());
        if (oPreferencesDashboardMstInfoCollection.Count > 0)
        {
            //if there is any  Preferences 
            oActiveDashboardMstInfoCollection = oPreferencesDashboardMstInfoCollection.FindAll(o => o.IsActive.Value == true);
            if (oActiveDashboardMstInfoCollection != null)
            {
                oDashboardMstInfoCollection = new List<DashboardMstInfo>();
                for (int i = 0; i < oActiveDashboardMstInfoCollection.Count; i++)
                {
                    oDashboardMstInfoCollection.Add(oAccessibleDashboardMstInfoCollection.Find(o => o.DashboardID == oActiveDashboardMstInfoCollection[i].DashboardID));
                }
            }
            else
                oDashboardMstInfoCollection = new List<DashboardMstInfo>();
        }
        else
        {
            //if no  Preferences then all accessible deshboard
            oDashboardMstInfoCollection = oAccessibleDashboardMstInfoCollection;
        }
        return oDashboardMstInfoCollection;
    }

    private void ShowHideWebPartsInZone(WebPartZone wpz, List<DashboardMstInfo> oDashboardMstInfoCollection)
    {
        List<WebPartBase> listWebPartsHidden = new List<WebPartBase>();
        DashboardMstInfo oDashboardMstInfo = null;

        int i;
        WebPartBase oWebPartBase = null;
        // Loop thru all Web Parts in each Zone
        for (i = 0; i < wpz.WebParts.Count; i++)
        {
            oWebPartBase = (WebPartBase)wpz.WebParts[i];
            //oWebPartBase.Hidden = true;

            if (oDashboardMstInfoCollection != null)
            {
                oDashboardMstInfo = oDashboardMstInfoCollection.Find(c => c.DashboardID == oWebPartBase.DashboardID);
                if (oDashboardMstInfo != null)
                {
                    // Means this role has access to the Web Part, so un-hide
                    oWebPartBase.Hidden = false;
                    oWebPartBase.TitleLabelID = oDashboardMstInfo.DashboardTitleLabelID;
                    oWebPartBase.DescriptionLabelID = oDashboardMstInfo.DescriptionLabelID;
                    oWebPartBase.UserControlUrl = oDashboardMstInfo.UserControlUrl;
                }
                else
                {
                    listWebPartsHidden.Add(oWebPartBase);
                }
            }
        }

        foreach (WebPartBase objHiddenWebParts in listWebPartsHidden)
        {
            wpmDashboards.CloseWebPart(objHiddenWebParts);
        }
    }

    private void ShowAccountCountMessage()
    {
        if (SessionHelper.CurrentReconciliationPeriodID != null)
        {
            // Get the Total Accounts Count Message
            IUser oUserClient = RemotingHelper.GetUserObject();
            int? accountCount = oUserClient.GetTotalAccountsCount(SessionHelper.CurrentUserID, SessionHelper.CurrentRoleID, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
            if (SessionHelper.CurrentRoleID != (short)WebEnums.UserRole.SYSTEM_ADMIN
                && accountCount != null)
                lblMessage.Text = string.Format(LanguageUtil.GetValue(1250), accountCount);
        }
    }
    #endregion

    #region Other Methods
    protected override void OnLoadComplete(EventArgs e)
    {
        base.OnLoadComplete(e);

        try
        {
            List<DashboardMstInfo> oDashboardMstInfoCollection = GetUserDashboardList();
            if (!Page.IsPostBack)
            {
                wpmDashboards.DisplayMode = WebPartManager.BrowseDisplayMode;
            }
            LoadRoleSpecificFeatures();

            ShowAccountCountMessage();

            LoadAlertTypeMsg();

        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }

    private void LoadDashboardsAsync()
    {
        Dictionary<UserControlWebPartBase, IAsyncResult> oAsyncCallList = new Dictionary<UserControlWebPartBase, IAsyncResult>();
        List<WaitHandle> oWaitHandleList = new List<WaitHandle>();
        for (int i = wpmDashboards.WebParts.Count - 1; i >= 0; i--)
        {
            WebPartBase oWebPartBase = (WebPartBase)wpmDashboards.WebParts[i];
            UserControlWebPartBase oUserControlWebPartBase = oWebPartBase.LoadedDashboardControl;
            if (oUserControlWebPartBase != null)
            {
                IAsyncResult oIAsyncResult = oUserControlWebPartBase.GetDataAsync();
                if (oIAsyncResult != null)
                {
                    oAsyncCallList.Add(oUserControlWebPartBase, oIAsyncResult);
                    oWaitHandleList.Add(oIAsyncResult.AsyncWaitHandle);
                }
            }
        }
        if (oWaitHandleList.Count > 0)
        {
            WaitHandle.WaitAll(oWaitHandleList.ToArray());
            foreach (UserControlWebPartBase oUserControlWebPartBase in oAsyncCallList.Keys)
            {
                oUserControlWebPartBase.DataLoaded(oAsyncCallList[oUserControlWebPartBase]);
            }
        }
    }

    public override string GetMenuKey()
    {
        return "";
    }
    #endregion

}
