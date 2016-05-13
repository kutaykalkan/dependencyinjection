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
using System.Collections.Generic;
using SkyStem.ART.Client.IServices;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Web.Classes;
using Telerik.Web.UI;


public partial class Pages_DashboardPreferences : PageBaseRecPeriod
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
        MasterPageBase ompage = (MasterPageBase)this.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);
    }

    protected void Page_Load(object sender, EventArgs e)
    {



        try
        {
            Helper.SetPageTitle(this, 2417);
            // Helper.ShowInputRequirementSection(this, 2077, 2078,2079);    
            if (SessionHelper.CurrentRoleID == (short)WebEnums.UserRole.PREPARER
                || SessionHelper.CurrentRoleID == (short)WebEnums.UserRole.REVIEWER
                || SessionHelper.CurrentRoleID == (short)WebEnums.UserRole.APPROVER)
            {
                upnlBackupRoleNotifications.Visible = true;
                BindBackupNotofocationGrid();
                pnlHeaderBackup.Style.Add("display", "display");
                string _PopupUrl = Page.ResolveClientUrl("~/Pages/EmailPopupForRecForm.aspx");
                btnSendMail.Attributes.Add("onclick", "javascript:return OpenRadWindow('" + _PopupUrl + "?" + QueryStringConstants.PAGE_ID + "=" + (short)WebEnums.ARTPages.DashboardPreferences + "', " + 520 + " , " + 880 + "); return false; ");
            }
            else
            {
                upnlBackupRoleNotifications.Visible = false;
                pnlHeaderBackup.Style.Add("display", "none");
            }

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
    #endregion

    #region Grid Events
    #region rgDashboard
    protected void rgDashboardPreferences_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item ||
           e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            DashboardMstInfo oDashboardMstInfo = (DashboardMstInfo)e.Item.DataItem;
            ExLabel lblDashBoardName = e.Item.FindControl("lblDashBoardName") as ExLabel;
            if (oDashboardMstInfo.DashboardTitleLabelID.HasValue)
                lblDashBoardName.LabelID = oDashboardMstInfo.DashboardTitleLabelID.Value;
            ExLabel lblDashBoardDesc = e.Item.FindControl("lblDashBoardDesc") as ExLabel;
            if (oDashboardMstInfo.DescriptionLabelID.HasValue)
                lblDashBoardDesc.LabelID = oDashboardMstInfo.DescriptionLabelID.Value;
            List<DashboardMstInfo> oDashboardMstInfoCollection = GetUserDashboardPreferences();
            DashboardMstInfo oPreferencesDashboardMstInfo = oDashboardMstInfoCollection.Find(obj => obj.DashboardID == oDashboardMstInfo.DashboardID);
            if (oPreferencesDashboardMstInfo != null)
            {
                if (oPreferencesDashboardMstInfo.IsActive.HasValue)
                    e.Item.Selected = oPreferencesDashboardMstInfo.IsActive.Value;
            }
            else
                e.Item.Selected = true;
        }

    }
    protected void rgDashboardPreferences_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            List<DashboardMstInfo> oDashboardMstInfoCollection = SessionHelper.GetDashboards();
            rgDashboardPreferences.DataSource = oDashboardMstInfoCollection;
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }

    }
    #endregion
    #region MyPreferences
    protected void rgMyPreferences_ItemDataBound(object sender, GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                int oPreferncesMstInfo = (int)e.Item.DataItem;
                ExLabel lblBackupNotifications = (ExLabel)e.Item.FindControl("lblBackupNotifications");
                lblBackupNotifications.LabelID = oPreferncesMstInfo;
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
    }
    #endregion
    #endregion

    #region Other Events
    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        try
        {
            IUser oUserClient = RemotingHelper.GetUserObject();
            List<DashboardMstInfo> oDashboardMstInfoCollection = new List<DashboardMstInfo>();
            foreach (GridDataItem item in rgDashboardPreferences.Items)
            {
                DashboardMstInfo oDashboardMstInfo = new DashboardMstInfo();
                oDashboardMstInfo.DashboardID = Convert.ToInt16(item.GetDataKeyValue("DashboardID"));
                oDashboardMstInfo.IsActive = item.Selected;
                oDashboardMstInfoCollection.Add(oDashboardMstInfo);
            }
            oUserClient.SaveUserDashboardPreferences(oDashboardMstInfoCollection, SessionHelper.CurrentUserID, SessionHelper.CurrentRoleID, Helper.GetAppUserInfo());
            int LabelID = 2414;
            Response.Redirect("Home.aspx?" + QueryStringConstants.CONFIRMATION_MESSAGE_LABEL_ID + "=" + LabelID.ToString());

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

    protected void btnCancel_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    public void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        btnSave.Visible = true;
        Sel.Value = string.Empty;
        LoadAccessibleDashboards();
        //if (SessionHelper.CurrentRecProcessStatusEnum != WebEnums.RecPeriodStatus.NotStarted)
        //{
        //    pnlGidDashboardPreferences.Enabled = false;
        //    btnSave.Visible = false;
        //}
        //else
        //{
        //    pnlGidDashboardPreferences.Enabled = true;
        //    btnSave.Visible = true;
        //}
    }
    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods
    private void BindBackupNotofocationGrid()
    {
        List<int> dataSourceBackupNotifications = new List<int>();
        dataSourceBackupNotifications.Add(2506);
        rgMyPreferences.DataSource = dataSourceBackupNotifications;
        rgMyPreferences.DataBind();
    }
    private void LoadAccessibleDashboards()
    {
        List<DashboardMstInfo> oDashboardMstInfoCollection = SessionHelper.GetDashboards();
        rgDashboardPreferences.DataSource = oDashboardMstInfoCollection;
        rgDashboardPreferences.DataBind();
    }
    private List<DashboardMstInfo> GetUserDashboardPreferences()
    {
        List<DashboardMstInfo> oDashboardMstInfoCollection = null;
        String ViewStateKey = SessionHelper.CurrentUserID.ToString() + SessionHelper.CurrentRoleID.ToString();
        if (ViewState[ViewStateKey] == null)
        {
            IUser oUserClient = RemotingHelper.GetUserObject();
            oDashboardMstInfoCollection = oUserClient.GetUserDashboardPreferences(SessionHelper.CurrentUserID, SessionHelper.CurrentRoleID, Helper.GetAppUserInfo());
            ViewState[ViewStateKey] = oDashboardMstInfoCollection;
        }
        else
        {
            oDashboardMstInfoCollection = (List<DashboardMstInfo>)ViewState[ViewStateKey];
        }

        return oDashboardMstInfoCollection;

    }
    #endregion

    #region Other Methods
    public override string GetMenuKey()
    {

        return "DashboardPreferences";
    }

    #endregion

}
