using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Web.Utility;
using System.Collections.Generic;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Data;
using SkyStem.Library.Controls.WebControls;
using SkyStem.Library.Controls.TelerikWebControls;
using System.Text;
using SkyStem.ART.Client.Exception;

public partial class MasterPages_ARTMasterPage : MasterPageBase
{

    #region Variables & Constants
    bool _IsFYChange = false;
    string popupCheckSkippedRecPeriodurl;
    #endregion

    #region Properties
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Session[SessionConstants.DATE_TIME] = DateTime.Now.ToString();
            //imgBtnSARReprocess.Attributes.Add("onFocus", "this.blur();");

            hlDueDates.NavigateUrl = "javascript:OpenRadWindowForHyperlinkWithOffset('" + Page.ResolveClientUrl("~/Pages/DueDates.aspx") + "', 300, 300, '" + hlDueDates.ClientID + "');";
            hlIssueReporting.NavigateUrl = "javascript:OpenRadWindowForHyperlink('" + Page.ResolveClientUrl("~/Pages/Support/ReportIssue.aspx") + "', 600, 900, '" + hlIssueReporting.ClientID + "');";
            hlMaterialty.NavigateUrl = "javascript:OpenRadWindowForHyperlinkWithOffset('" + Page.ResolveClientUrl("~/Pages/MaterialityUnexplainedthresholdDetail.aspx") + "', 500, 500, '" + hlMaterialty.ClientID + "');";
            imgBtnSARReprocess.Attributes.Add("onclick", "return confirmSRAReprocess(); ");

            string fyPopupUrl = "~/Pages/FinancialYearSelectionPopup.aspx?" + QueryStringConstants.POSTBACK_CONTROL_ID + "=" + ddlReconciliationPeriod.UniqueID;
            hlFinanceYearSelection.NavigateUrl = "javascript:OpenRadWindowForHyperlinkWithOffset('" + Page.ResolveClientUrl(fyPopupUrl) + "', 210,350, '" + hlFinanceYearSelection.ClientID + "');";

            if (!IsPostBack)
            {
                //Exchange rate popup 
                tdExchangeRates.Visible = false;
                tdToolBarSeparatorExchangeRates.Visible = false;

                lblUserName.Text = Helper.GetUserFullName();
                lblCompanyName.Text = Helper.GetCompanyName();

                WebEnums.UserRole eUserRole = (WebEnums.UserRole)System.Enum.Parse(typeof(WebEnums.UserRole), SessionHelper.CurrentRoleID.Value.ToString());
                LoadRoleSpecificFeatures();

                if (eUserRole == WebEnums.UserRole.SKYSTEM_ADMIN)
                {
                    lblCurrentRole.Text = LanguageUtil.GetValue(1198);

                }
                else
                {
                    lblCurrentRole.Text = Helper.GetCurrentRoleName();

                    CompanyHdrInfo oCompanyHdrInfo = SessionHelper.GetCurrentCompanyHdrInfo();

                    if (oCompanyHdrInfo != null)
                    {
                        if (Convert.ToBoolean(oCompanyHdrInfo.ShowLogoOnMasterPage))
                        {
                            lblCompanyName.Visible = false;
                            imgCompanyLogo.Visible = true;
                            ReportHelper.ShowCompanyLogo(imgCompanyLogo, Page);
                        }
                        else
                        {
                            lblCompanyName.Visible = true;
                            imgCompanyLogo.Visible = false;
                        }
                    }
                }

                ListItem oListItem = null;
                oListItem = null;
                if (SessionHelper.CurrentReconciliationPeriodID != null)
                {
                    oListItem = ddlReconciliationPeriod.Items.FindByValue(SessionHelper.CurrentReconciliationPeriodID.Value.ToString());
                    if (oListItem != null)
                    {
                        ddlReconciliationPeriod.SelectedItem.Selected = false;
                        oListItem.Selected = true;
                    }

                }

                if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.CONFIRMATION_MESSAGE_LABEL_ID]))
                {
                    int labelID = Convert.ToInt32(Request.QueryString[QueryStringConstants.CONFIRMATION_MESSAGE_LABEL_ID]);
                    ShowConfirmationMessage(labelID);
                }
            }
            if (hdIsPostBackFlag.Value == "1")
            {
                // Clear FY + Rec Period Data 
                SessionHelper.ClearFYDataFromSession();

                _IsFYChange = true;
                GetFYText();
                BindReconciliationPeriod(true);
                hdIsPostBackFlag.Value = "0";
                RaiseFinancialYearChangedEvent(sender, e);
            }
        }
        catch (ARTException ex)
        {
            ShowErrorMessage(ex.ExceptionPhraseID);
        }
        catch (Exception ex)
        {
            ShowErrorMessage(ex);
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        // Render the JS required to check for Skipped Rec Period Status
        RenderJSForRecPeriodIDAndStatusID();
    }
    #endregion

    #region Grid Events
    #endregion

    #region Other Events
    protected void Refresh_Click(object sender, EventArgs args)
    {
        ReconciliationPeriodStatusMstInfo oRecPeriodStatusInfo = SessionHelper.GetRecPeriodStatus();
        if (oRecPeriodStatusInfo != null && hdnRecPeriodStatus.Value != oRecPeriodStatusInfo.ReconciliationPeriodStatusID.GetValueOrDefault().ToString())
            ddlReconciliationPeriod_SelectedIndexChanged(null, null);
        RaiseRefreshRequested(null, new RefreshEventArgs());
    }
    protected void btnDummy_Click(object sender, EventArgs e)
    {
        Helper.ReprocessSRA();

    }
    protected void lnkEditDashboard_Click(object sender, EventArgs e)
    {
        if (WebPartManager.GetCurrentWebPartManager(this.Page).DisplayMode == WebPartManager.BrowseDisplayMode)
        {
            WebPartManager.GetCurrentWebPartManager(this.Page).DisplayMode = WebPartManager.DesignDisplayMode;
        }
        pnlDesignMode.Visible = true;
        pnlBrowseMode.Visible = false;
    }

    protected void lnkResetDashboard_Click(object sender, EventArgs e)
    {
        WebPartManager.GetCurrentWebPartManager(this.Page).Personalization.ResetPersonalizationState();
        Response.Redirect(Helper.GetHomePageUrl());
    }

    protected void lnkSave_Click(object sender, EventArgs e)
    {
        pnlDesignMode.Visible = false;
        pnlBrowseMode.Visible = true;
        WebPartManager.GetCurrentWebPartManager(this.Page).DisplayMode = WebPartManager.BrowseDisplayMode;
        lnkEditDashboard.Text = LanguageUtil.GetValue(2446);
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        /*
         * This method can be called in two ways
         * 1. Thru Page_Load
         * 2. Thru Dropdown Change on UI
         * 
         * ONLY If called from #2 - Clear the Existing Company Data, since the Company was changed from UI
         */

        if (sender != null)
        {
            // Clear the existing Company + FY + Rec Period related data
            SessionHelper.ClearCompanyDataFromSession();

            // Clear the existing Company + FY + Rec Period related data
            CacheHelper.ClearCompanyDataFromCache();
        }

        // Set Multi-lingual Attributes
        int lcid = Convert.ToInt32(HttpContext.Current.Session[SessionConstants.CURRENT_LANGUAGE]);

        if (!string.IsNullOrEmpty(ddlCompany.SelectedValue))
        {
            // Set Current Company in Session
            SessionHelper.CurrentCompanyID = Convert.ToInt32(ddlCompany.SelectedValue);
            lblCompanyName.Text = Helper.GetCompanyName();

            // Reset Business Entity ID in session as well

            LanguageUtil.SetMultilingualAttributes(AppSettingHelper.GetApplicationID(), lcid, SessionHelper.CurrentCompanyID.Value, AppSettingHelper.GetDefaultLanguageID(), AppSettingHelper.GetDefaultBusinessEntityID());

            //****Additional code added (by Prafull on 02-Jul-2010) to redirect the user to the home page on company selection Change 
            //***********************************************************************************************************************
            if (sender == null)
            {
                // means First time Load and coming from Page_Load
                if (SessionHelper.CurrentCompanyDatabaseExists.GetValueOrDefault())
                {
                    // Show FY
                    SetFYData();

                    // Show Rec Periods
                    BindReconciliationPeriod(true);
                }
                RaiseCompanyChangedEvent(sender, e);
            }
            else
            {
                Helper.RedirectToHomePage();
            }
            //**********************************************************************************************************************
        }
        else
        {
            LanguageUtil.SetMultilingualAttributes(AppSettingHelper.GetApplicationID(), lcid, AppSettingHelper.GetDefaultBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID(), AppSettingHelper.GetDefaultBusinessEntityID());
        }

    }


    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Set Current Role in Session
        short roleID = 0;
        if (!string.IsNullOrEmpty(ddlRole.SelectedValue) && short.TryParse(ddlRole.SelectedValue, out roleID))
        {
            SessionHelper.CurrentRoleID = roleID;
            lblCurrentRole.Text = Helper.GetCurrentRoleName();
        }

        // Clear the existing Role related data
        SessionHelper.ClearRoleDataFromSession();

        if (SessionHelper.CurrentRoleID.HasValue && SessionHelper.CurrentRoleID == (short)WebEnums.UserRole.AUDIT)
        {
            string key = SessionHelper.GetSessionKeyForMenu();
            SessionHelper.ClearSession(key);
        }

        if (roleID > 0)
            Helper.RedirectToHomePage();

        // TODO: Commented by Apoorv, since we are NOT implementing this as part of R1
        //// If Role is Approver, and Dual Review Disabled, redirect to Error Page
        //if (SessionHelper.CurrentRoleEnum == WebEnums.UserRole.APPROVER
        //    && !Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.DualLevelReview))
        //{
        //    // Role = Approver, and Dual Review disabled
        //    Helper.RedirectToErrorPage(5000166);
        //}
        //else
        //{
        //    Helper.RedirectToHomePage();
        //}
    }

    protected void ddlReconciliationPeriod_SelectedIndexChanged(object sender, EventArgs e)
    {
        SessionHelper.ClearFYDataFromSession();

        HandleRecPeriodChange(sender, e, true);
    }
    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods
    private void LoadRoleSpecificFeatures()
    {
        // Toolbar - > Home Page 
        hlHome.NavigateUrl = Helper.GetHomePageUrl();

        // check for SkyStem Admin
        UserHdrInfo oUserHdrInfo = SessionHelper.GetCurrentUser();
        int skyStemAdminRoleID = CacheHelper.GetRoleID(ARTConstants.ROLE_TEXT_SKYSTEM_ADMIN);
        WebEnums.UserRole eUserRole = (WebEnums.UserRole)System.Enum.Parse(typeof(WebEnums.UserRole), SessionHelper.CurrentRoleID.Value.ToString());

        if (eUserRole == WebEnums.UserRole.SKYSTEM_ADMIN)
        {
            // SkyStem Admin
            // 1. Hide Switch To / Rec Period
            // 2. Load Companies

            lblRole.Visible = false;
            ddlRole.Visible = false;

            // Load Company Dropdown
            BindCompany();
        }
        else if (eUserRole == WebEnums.UserRole.SYSTEM_ADMIN || eUserRole == WebEnums.UserRole.BUSINESS_ADMIN)
        {
            SetFYData();
            // Other Roles
            // 1. Hide Company
            // 2. Load Rec Periods
            // 3. Load Roles

            // 1.
            lblCompany.Visible = false;
            ddlCompany.Visible = false;

            // 2.
            BindReconciliationPeriod(true);
            // 3.
            BindRoleDropdown();

            //imgReProcessSRAToolBarSeparator.Visible = true;
            //imgBtnSARReprocess.Visible = true;
            EnableDisablePageBasedOnRecPeriodStatus();

        }
        else
        {
            SetFYData();
            // Other Roles
            // 1. Hide Company
            // 2. Load Rec Periods
            // 3. Load Roles

            // 1.
            lblCompany.Visible = false;
            ddlCompany.Visible = false;

            // 2.
            BindReconciliationPeriod(true);

            // 3.
            BindRoleDropdown();
            imgReProcessSRAToolBarSeparator.CssClass = "displayNone";
            imgBtnSARReprocess.CssClass = "displayNone";
            imgToolBarSeparatorExchangeRates.Visible = false;

            if (SessionHelper.CurrentRoleEnum == WebEnums.UserRole.USER_ADMIN)
            {
                hlMyReport.Visible = false;
                imgToolBarSeparatorMyReport.Visible = false;
                hlAccountViewer.Visible = false;
                imgToolBarSeparatorAccountViewer.Visible = false;
                hlMaterialty.Visible = false;
                imgToolBarSeparatorMateriality.Visible = false;
                hlDueDates.Visible = false;
                imgToolBarSeparatorDueDates.Visible = false;
                hlExchangeRates.Visible = false;
                imgToolBarSeparatorExchangeRates.Visible = false;
                tdReProcessSRA.Visible = false;
                imgReProcessSRAToolBarSeparator.Visible = false;
            }

        }
    }
    /// <summary>
    /// Bind Role Dropdown
    /// </summary>
    private void BindRoleDropdown()
    {
        if (SessionHelper.CurrentRoleID.GetValueOrDefault() != (short)WebEnums.UserRole.SKYSTEM_ADMIN)
        {
            ListItem oListItem = null;
            ListControlHelper.BindRoleDropdown(ddlRole);
            // Set the Current Role
            oListItem = ddlRole.Items.FindByValue(SessionHelper.CurrentRoleID.Value.ToString());
            if (oListItem != null)
            {
                ddlRole.SelectedItem.Selected = false;
                oListItem.Selected = true;
            }
            else
                ddlRole_SelectedIndexChanged(null, null);
        }
    }
    /// <summary>
    /// SetFYData() is used to set Current financial year text.
    /// </summary>
    private void SetFYData()
    {
        // default value
        lblFinanceYearValue.Text = WebConstants.HYPHEN;
        if (!SessionHelper.CurrentFinancialYearID.HasValue)
        {
            int? autoSaveFYID = Helper.GetAutoSavedFinancialYear();
            if (autoSaveFYID.HasValue)
                SessionHelper.CurrentFinancialYearID = autoSaveFYID;
            else
            {
                ReconciliationPeriodInfo oReconciliationPeriodInfo = Helper.GetMaxReconciliationPeriodInfo();
                if (oReconciliationPeriodInfo != null)
                {
                    if (oReconciliationPeriodInfo.FinancialYearID != null)
                    {
                        SessionHelper.CurrentFinancialYearID = oReconciliationPeriodInfo.FinancialYearID;
                    }
                    else
                    {
                        FinancialYearHdrInfo oFinancialYearHdrInfo = Helper.GetFirstFinancialYear();
                        if (oFinancialYearHdrInfo != null)
                        {
                            SessionHelper.CurrentFinancialYearID = oFinancialYearHdrInfo.FinancialYearID;
                        }
                    }
                }
            }
        }
        GetFYText();
    }

    private void GetFYText()
    {
        FinancialYearHdrInfo oFinancialYearHdrInfo = Helper.GetFinancialYearInfo(SessionHelper.CurrentFinancialYearID);
        if (oFinancialYearHdrInfo != null)
        {
            lblFinanceYearValue.Text = oFinancialYearHdrInfo.FinancialYear;
        }
    }

    private void BindCompany()
    {
        ListItem oListItem = null;
        ListControlHelper.BindCompanyDropdown(ddlCompany);

        // Set the Current Company
        if (SessionHelper.CurrentCompanyID != null)
        {
            oListItem = ddlCompany.Items.FindByValue(SessionHelper.CurrentCompanyID.Value.ToString());
            if (oListItem != null)
            {
                ddlCompany.SelectedItem.Selected = false;
                oListItem.Selected = true;
            }
        }
        // Handle Company on Change - Load the Rec Periods based on Company ID
        ddlCompany_SelectedIndexChanged(null, null);
    }

    private void BindReconciliationPeriod(bool raiseEvent)
    {
        int? recPeriodID = null;
        if (ddlReconciliationPeriod.SelectedItem != null)
            recPeriodID = Convert.ToInt32(ddlReconciliationPeriod.SelectedValue);
        ListControlHelper.BindReconciliationPeriod(ddlReconciliationPeriod, SessionHelper.CurrentFinancialYearID);

        if (ddlReconciliationPeriod.Items.Count > 0)
        {
            // Set the First Rec Period as Default
            ddlReconciliationPeriod.Items[0].Selected = true;

            // Check the Current Rec Period in Session, if available set that as Selected Item
            // Else get the Current Rec Period for the Company
            // If both are not available, first Rec Period will be selected
            if (recPeriodID == null)
                recPeriodID = SessionHelper.CurrentReconciliationPeriodID;
            if (recPeriodID != null)
            {
                SetCurrentRecPeriod(recPeriodID);
            }
            else
            {
                // do this only if not coming from Change FY page
                if (!_IsFYChange)
                {
                    int? recPeriodIDPrev = Helper.GetAutoSavedRecPeriod();
                    if (recPeriodIDPrev == null)
                    {
                        // Fetch the Current Rec Period for Company
                        IReconciliationPeriod oReconciliationPeriodClient = RemotingHelper.GetReconciliationPeriodObject();
                        ReconciliationPeriodInfo oReconciliationPeriodInfo = oReconciliationPeriodClient.GetMinCurrentPeriodByCompanyId(SessionHelper.CurrentCompanyID, Helper.GetAppUserInfo());
                        recPeriodIDPrev = oReconciliationPeriodInfo.ReconciliationPeriodID;
                    }
                    SetCurrentRecPeriod(recPeriodIDPrev);
                }
            }
        }

        // Set the Open Rec Period as Current Selection
        HandleRecPeriodChange(null, null, raiseEvent);
    }

    private void RenderJSForRecPeriodIDAndStatusID()
    {

        // Render a JS Array for Rec Period Status
        List<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollection = CacheHelper.GetAllReconciliationPeriods(SessionHelper.CurrentFinancialYearID);

        StringBuilder oRecPeriodIDAndStatusIDArray = new StringBuilder();
        oRecPeriodIDAndStatusIDArray.Append("var RecPeriodIDAndStatusArray = new Array();");
        oRecPeriodIDAndStatusIDArray.Append(System.Environment.NewLine);
        if (oReconciliationPeriodInfoCollection != null)
        {
            for (int i = 0; i < oReconciliationPeriodInfoCollection.Count; i++)
            {
                oRecPeriodIDAndStatusIDArray.Append("RecPeriodIDAndStatusArray['" + oReconciliationPeriodInfoCollection[i].ReconciliationPeriodID.Value.ToString() + "'] = " + oReconciliationPeriodInfoCollection[i].ReconciliationPeriodStatusID.Value.ToString() + ";");
                oRecPeriodIDAndStatusIDArray.Append(System.Environment.NewLine);
            }
        }

        if (!string.IsNullOrEmpty(ddlReconciliationPeriod.SelectedValue))
        {
            oRecPeriodIDAndStatusIDArray.Append("var oldRecPeriodID = " + ddlReconciliationPeriod.SelectedValue);
            oRecPeriodIDAndStatusIDArray.Append(System.Environment.NewLine);
        }

        if (!this.Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "RecPeriodIDAndStatusArray"))
        {
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RecPeriodIDAndStatusArray", oRecPeriodIDAndStatusIDArray.ToString(), true);
        }
        popupCheckSkippedRecPeriodurl = "~/Pages/ShowSkippedRecPeriodMessage.aspx";
        string url = "javascript:return CheckSkippedRecPeriod(this,'" + Page.ResolveUrl(popupCheckSkippedRecPeriodurl) + "', 200, 350);";
        ddlReconciliationPeriod.Attributes.Add("onchange", url);
    }

    private void SetCurrentRecPeriod(int? recPeriodID)
    {
        if (recPeriodID != null)
        {
            ListItem oListItem = ddlReconciliationPeriod.Items.FindByValue(recPeriodID.ToString());
            if (oListItem != null)
            {
                ddlReconciliationPeriod.SelectedItem.Selected = false;
                oListItem.Selected = true;
            }
        }
    }
    private void HandleRecPeriodChange(object sender, EventArgs e, bool raiseEvent)
    {
        // Set Current Rec Period in Session
        SessionHelper.SetCurrentReconciliationPeriodInfo(ddlReconciliationPeriod.SelectedItem);

        // Reload Roles
        BindRoleDropdown();

        ReconciliationPeriodStatusMstInfo oReconciliationPeriodStatusMstInfo = SessionHelper.GetRecPeriodStatus();
        // Show Hide Image and Label
        ShowRecPeriodStatusDetail(oReconciliationPeriodStatusMstInfo);

        // Also Set the Countdown Days Values
        ShowCountdownDetail(oReconciliationPeriodStatusMstInfo);

        // Save Auto Save Attribute Values
        List<ARTEnums.AutoSaveAttribute> eAutoSaveEnumList = new List<ARTEnums.AutoSaveAttribute>();
        eAutoSaveEnumList.Add(ARTEnums.AutoSaveAttribute.AutoSaveRecPeriodSelection);
        Helper.SaveAutoSaveAttributeValues(eAutoSaveEnumList);

        if (raiseEvent)
        {
            RaiseReconciliationPeriodChangedEvent(sender, e);
        }

        //Exchange rate Popup
        if (SessionHelper.CurrentReconciliationPeriodID != null)
        {
            if (Helper.IsCapabilityActivatedForRecPeriodID(ARTEnums.Capability.MultiCurrency, (int)SessionHelper.CurrentReconciliationPeriodID, true))
            {
                tdExchangeRates.Visible = true;
                tdToolBarSeparatorExchangeRates.Visible = true;
                hlExchangeRates.NavigateUrl = "javascript:OpenRadWindowForHyperlinkWithOffset('" + Page.ResolveClientUrl("~/Pages/PopupExchangeRates.aspx") + "', 600, 550, '" + hlExchangeRates.ClientID + "');";
            }
        }

        PopulateMenus();
        EnableDisablePageBasedOnRecPeriodStatus();
    }


    protected void PopulateMenus()
    {
        string breadCrumbs = "";
        // 
        PageBase oPageBase = (PageBase)this.Page;
        string menuKey = oPageBase.GetMenuKey();

        mnuSkyStemART.Items.Clear();

        breadCrumbs = CreateMenuItems(breadCrumbs, "", menuKey, null, null);

        // remove the separator for last Menu Item
        if (mnuSkyStemART.Items.Count > 0)
            mnuSkyStemART.Items[mnuSkyStemART.Items.Count - 1].SeparatorImageUrl = "";

        if (!string.IsNullOrEmpty(breadCrumbs))
        {
            lblBreadcrumbs.Text = breadCrumbs;
        }
    }

    private string CreateMenuItems(string breadCrumbs, string breadCrumbsPath, string menuKey, Int16? parentMenuID, MenuItem oParentMenuItem)
    {
        List<MenuMstInfo> oAllMenuMstInfoCollection = SessionHelper.GetUserMenu();
        List<MenuMstInfo> oMenuMstInfoCollection = oAllMenuMstInfoCollection.FindAll(c => c.ParentMenuID == parentMenuID);
        for (int i = 0; i < oMenuMstInfoCollection.Count; i++)
        {
            string menuBreadCrumbsPath = "";
            MenuMstInfo oMenuMstInfo = oMenuMstInfoCollection[i];
            MenuItem oMenuItem = new MenuItem(LanguageUtil.GetValue(oMenuMstInfo.MenuLabelID.Value));
            menuBreadCrumbsPath = breadCrumbsPath + ARTConstants.BREADCRUMBS_SEPARATOR + oMenuItem.Text;

            if (string.IsNullOrEmpty(oMenuMstInfo.MenuURL))
            {
                oMenuItem.Selectable = false;
            }
            else
            {
                oMenuItem.NavigateUrl = oMenuMstInfo.MenuURL;
                if (oMenuMstInfo.MenuKey == menuKey)
                {
                    oMenuItem.Selected = true;
                    breadCrumbs = menuBreadCrumbsPath;
                }
            }

            if (oParentMenuItem == null)
            {
                // Add to Main Menu Control
                oMenuItem.SeparatorImageUrl = this.ResolveUrl("~/App_Themes/SkyStemBlueBrown/Images/MenuDivider.gif");
                mnuSkyStemART.Items.Add(oMenuItem);
            }
            else
            {
                oParentMenuItem.ChildItems.Add(oMenuItem);
            }

            breadCrumbs = CreateMenuItems(breadCrumbs, menuBreadCrumbsPath, menuKey, oMenuMstInfo.MenuID, oMenuItem);
        }
        return breadCrumbs;
    }

    private int? GetDays(DateTime? dtClose)
    {
        if (dtClose != null)
        {
            return dtClose.Value.Date.Subtract(DateTime.Today.Date).Days + 1;
        }
        else
        {
            return null;
        }
    }

    private void EnableDisablePageBasedOnRecPeriodStatus()
    {
        ReconciliationPeriodStatusMstInfo oReconciliationPeriodStatusMstInfo = SessionHelper.GetRecPeriodStatus();

        if (oReconciliationPeriodStatusMstInfo != null)
        {
            WebEnums.RecPeriodStatus eRecPeriodStatus = (WebEnums.RecPeriodStatus)oReconciliationPeriodStatusMstInfo.ReconciliationPeriodStatusID;
            if ((Int32)eRecPeriodStatus > 0)
            {
                switch (eRecPeriodStatus)
                {
                    case WebEnums.RecPeriodStatus.NotStarted:
                    case WebEnums.RecPeriodStatus.Skipped:
                    case WebEnums.RecPeriodStatus.Closed:
                        {
                            imgReProcessSRAToolBarSeparator.CssClass = "displayNone";
                            imgBtnSARReprocess.Visible = false;
                            imgToolBarSeparatorExchangeRates.Visible = false;
                            break;
                        }
                    case WebEnums.RecPeriodStatus.Open:
                    case WebEnums.RecPeriodStatus.InProgress:
                        {
                            imgBtnSARReprocess.Visible = true;
                            imgToolBarSeparatorExchangeRates.Visible = true;
                            if (CertificationHelper.IsCertificationStarted())
                            {
                                imgReProcessSRAToolBarSeparator.CssClass = "displayNone";
                                imgBtnSARReprocess.Visible = false;
                                imgToolBarSeparatorExchangeRates.Visible = false;
                            }
                            break;
                        }
                }
            }

        }
        else
        {
            imgReProcessSRAToolBarSeparator.CssClass = "displayNone";
            imgBtnSARReprocess.Visible = false;
            imgToolBarSeparatorExchangeRates.Visible = false;

        }
    }

    #endregion

    #region Other Methods
    protected void ScriptManager_OnAsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
    {
        Exception objError = this.Server.GetLastError();
        if (objError != null)
        {
            objError = objError.GetBaseException();
            SkyStem.ART.Web.Utility.Helper.LogException(objError);
            this.Server.ClearError();
        }
        _scriptManager.AsyncPostBackErrorMessage = LanguageUtil.GetValue(5000030); //objError.Message;
    }
    protected void ShowRecPeriodStatusDetail(ReconciliationPeriodStatusMstInfo oReconciliationPeriodStatusMstInfo)
    {
        if (oReconciliationPeriodStatusMstInfo != null)
        {
            lblRecPeriodStatus.Text = LanguageUtil.GetValue(oReconciliationPeriodStatusMstInfo.LabelID.Value);
            hdnRecPeriodStatus.Value = oReconciliationPeriodStatusMstInfo.ReconciliationPeriodStatusID.GetValueOrDefault().ToString();

            // Icon
            imgRecPeriodStatusNotStarted.Visible = false;
            imgRecPeriodStatusOpen.Visible = false;
            imgRecPeriodStatusInProgress.Visible = false;
            imgRecPeriodStatusClosed.Visible = false;
            imgRecPeriodStatusSkipped.Visible = false;
            imgRecPeriodStatusOpeningInProgress.Visible = false;

            WebEnums.RecPeriodStatus eRecPeriodStatus = (WebEnums.RecPeriodStatus)oReconciliationPeriodStatusMstInfo.ReconciliationPeriodStatusID;
            if ((Int32)eRecPeriodStatus > 0)
            {
                switch (eRecPeriodStatus)
                {
                    case WebEnums.RecPeriodStatus.NotStarted:
                        imgRecPeriodStatusNotStarted.Visible = true;
                        break;

                    case WebEnums.RecPeriodStatus.Open:
                        imgRecPeriodStatusOpen.Visible = true;
                        break;

                    case WebEnums.RecPeriodStatus.InProgress:
                        imgRecPeriodStatusInProgress.Visible = true;
                        break;

                    case WebEnums.RecPeriodStatus.Closed:
                        imgRecPeriodStatusClosed.Visible = true;
                        break;

                    case WebEnums.RecPeriodStatus.Skipped:
                        imgRecPeriodStatusSkipped.Visible = true;
                        break;
                    case WebEnums.RecPeriodStatus.OpeningInProgress:
                        imgRecPeriodStatusOpeningInProgress.Visible = true;
                        break;
                }
            }
        }
    }

    private void ShowCountdownDetail(ReconciliationPeriodStatusMstInfo oReconciliationPeriodStatusMstInfo)
    {
        /*
         * 1. Lockdown
         *          - Certification Lockdown 
         *          - Rec Close Date
         */

        tblCountdown.Visible = false;
        if (oReconciliationPeriodStatusMstInfo != null)
        {
            WebEnums.RecPeriodStatus eRecPeriodStatus = (WebEnums.RecPeriodStatus)oReconciliationPeriodStatusMstInfo.ReconciliationPeriodStatusID;
            if ((Int32)eRecPeriodStatus > 0)
            {
                switch (eRecPeriodStatus)
                {
                    case WebEnums.RecPeriodStatus.NotStarted:
                    case WebEnums.RecPeriodStatus.Open:
                    case WebEnums.RecPeriodStatus.InProgress:
                        tblCountdown.Visible = true;

                        ReconciliationPeriodInfo oReconciliationPeriodInfo = Helper.GetRecPeriodInfo(SessionHelper.CurrentReconciliationPeriodID);
                        int? days = null;
                        if (oReconciliationPeriodInfo != null)
                        {
                            if (oReconciliationPeriodInfo.CertificationLockDownDate != null)
                            {
                                lblLockDownDays.Text = LanguageUtil.GetValue(1244);
                                days = GetDays(oReconciliationPeriodInfo.CertificationLockDownDate);
                            }
                            else if (oReconciliationPeriodInfo.ReconciliationCloseDate != null)
                            {
                                lblLockDownDays.Text = LanguageUtil.GetValue(1789);
                                days = GetDays(oReconciliationPeriodInfo.ReconciliationCloseDate);
                            }
                        }

                        if (days != null)
                        {
                            lblDaysCount.Text = days.Value.ToString("0#");
                        }
                        else
                        {
                            // Hide since we cannot calculate number of days
                            tblCountdown.Visible = false;
                        }

                        break;
                }
            }
        }

    }
    public override void SetPageTitle(int LabelID)
    {
        trPageTitle.Visible = true;
        lblPageTitle.LabelID = LabelID;
    }

    public override void SetPageTitle(string labeltext)
    {
        trPageTitle.Visible = true;
        lblPageTitle.Text = labeltext;
    }

    public override void SetBreadcrumbs(string path)
    {
        lblBreadcrumbs.Text = ARTConstants.BREADCRUMBS_SEPARATOR + path;
    }

    /// <summary>
    /// Show the Notes / Input Requirements Sections
    /// </summary>
    /// <param name="oLabelIDCollection"></param>
    public override void ShowInputRequirements(int[] oLabelIDCollection)
    {
        trInputRequirements.Visible = true;
        ucInputRequirements.Datasource = oLabelIDCollection;
    }

    public override void ShowErrorMessage(Exception ex)
    {
        lblConfirmationMessage.Visible = false;
        Helper.FormatAndShowErrorMessage(lblErrorMessage, ex);
        upnlMessages.Update();
    }

    public override void ShowErrorMessage(string errorMessage)
    {
        lblConfirmationMessage.Visible = false;
        Helper.FormatAndShowErrorMessage(lblErrorMessage, errorMessage);
        upnlMessages.Update();
    }

    public override void ShowErrorMessage(int errorMessageLabelID)
    {
        string errorMessage = LanguageUtil.GetValue(errorMessageLabelID);
        ShowErrorMessage(errorMessage);
    }
    public override void ShowErrorMessageWithNoBullet(string errorMessage)
    {
        lblConfirmationMessage.Visible = false;
        Helper.FormatAndShowErrorMessageNoBullet(lblErrorMessage, errorMessage);
        upnlMessages.Update();
    }

    public override void HideMessage()
    {
        lblErrorMessage.Visible = false;
        lblConfirmationMessage.Visible = false;
        upnlMessages.Update();
    }

    public override void ShowRequirement(int label, int[] oLabelIDCollection)
    {
        ShowInputRequirements(oLabelIDCollection);
        ExLabel lblInputRequirements = (ExLabel)ucInputRequirements.FindControl("lblInputRequirements");
        lblInputRequirements.LabelID = label;


    }


    public override void ShowConfirmationMessage(string message)
    {
        lblErrorMessage.Visible = false;
        lblConfirmationMessage.Visible = true;
        lblConfirmationMessage.Text = message;
        upnlMessages.Update();
    }

    public override void ShowConfirmationMessage(int confirmationMessageLabelID)
    {
        ShowConfirmationMessage(LanguageUtil.GetValue(confirmationMessageLabelID));
    }
    public override void ReloadRecPeriods()
    {
        ReloadRecPeriods(true);
    }

    public override void ReloadRecPeriods(bool raiseEvent)
    {
        string cacheKey = CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_RECONCILIATION_PERIODS_BASED_ON_FY);
        CacheHelper.ClearCache(cacheKey);
        BindReconciliationPeriod(raiseEvent);
        upnlRecPeriodDropDown.Update();
        upnlRecPeriodStatus.Update();
        upnlLockdownDays.Update();

    }

    public override void ReloadCompanies(int? CompanyID)
    {
        SessionHelper.ClearSession(SessionConstants.ALL_COMPANIES_LITE_OBJECT);
        SessionHelper.ClearCompanyDataFromSession();

        CacheHelper.ClearCache(CacheConstants.ALL_COMPANIES_LITE_OBJECT);
        CacheHelper.ClearCompanyDataFromCache();

        SessionHelper.CurrentCompanyID = CompanyID;
    }


    public override ScriptManager GetScriptManager()
    {
        return _scriptManager;
    }

    public override void RegisterPostBackToControls(Control oControl)
    {
        ScriptManager ScriptManager1 = this.GetScriptManager();
        ScriptManager1.RegisterPostBackControl(oControl);
    }
    public override void SetMasterPageSettings(MasterPageSettings oMasterPageSettings)
    {
        base.SetMasterPageSettings(oMasterPageSettings);
        if (oMasterPageSettings.EnableCompanySelection.HasValue)
            ddlCompany.Enabled = oMasterPageSettings.EnableCompanySelection.Value;
        if (oMasterPageSettings.EnableRoleSelection.HasValue)
            ddlRole.Enabled = oMasterPageSettings.EnableRoleSelection.Value;
        if (oMasterPageSettings.EnableRecPeriodSelection.HasValue)
            ddlReconciliationPeriod.Enabled = oMasterPageSettings.EnableRecPeriodSelection.Value;
        if (oMasterPageSettings.EnableWebPartCustomisation.HasValue)
            lnkEditDashboard.Visible = oMasterPageSettings.EnableWebPartCustomisation.Value;
        if (oMasterPageSettings.HideValidationSummary.HasValue)
            valSummary.Visible = !oMasterPageSettings.HideValidationSummary.Value;
        if (oMasterPageSettings.HideMenu.HasValue)
            mnuSkyStemART.Visible = !oMasterPageSettings.HideMenu.Value;
        if (oMasterPageSettings.HideToolBar.HasValue)
            tblToolBar.Visible = !oMasterPageSettings.HideToolBar.Value;
        if (oMasterPageSettings.HidePanelLockdownDays.HasValue)
            upnlLockdownDays.Visible = !oMasterPageSettings.HidePanelLockdownDays.Value;
        if (oMasterPageSettings.HideRecPeriodBar.HasValue)
            trRecPeriodBar.Visible = !oMasterPageSettings.HideRecPeriodBar.Value;
    }
    #endregion

}// end of Class
