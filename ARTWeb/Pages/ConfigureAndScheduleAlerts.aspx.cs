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
using Telerik.Web.UI;
using SkyStem.ART.Client.IServices;
using System.Collections.Generic;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.Exception;
using SkyStem.Library.Controls.WebControls;
using SkyStem.Language.LanguageUtility;
using SkyStem.Library.Controls.TelerikWebControls;
using System.Text.RegularExpressions;
using SkyStem.ART.Client.Data;

public partial class Pages_ConfigureAndScheduleAlerts : PageBaseCompany
{
    #region Variables & Constants

    bool _isCertificationStarted = false;

    #endregion

    #region Properties
    private List<CompanyAlertInfo> _CompanyAlertInfoCollection = null;
    public List<AlertMstInfo> SelectGeneralAlerts
    {
        get
        {
            if (SessionHelper.CurrentReconciliationPeriodID.HasValue && SessionHelper.CurrentReconciliationPeriodID.Value > 0)
            {
                if (ViewState[ViewStateConstants.GENERAL_ALERTS] != null)
                {
                    return (List<AlertMstInfo>)ViewState[ViewStateConstants.GENERAL_ALERTS];
                }
                else
                {
                    IAlert oAlertClient = RemotingHelper.GetAlertObject();
                    List<AlertMstInfo> oAlertMstInfoCollection = oAlertClient.SelectAllAlertMstInfo(SessionHelper.GetUserLanguage(), SessionHelper.GetBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID(), SessionHelper.CurrentCompanyID, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());

                    List<AlertMstInfo> oGeneralAlertsCollection = oAlertMstInfoCollection.Where(a => a.AlertCategoryID == 1).ToList();
                    ViewState[ViewStateConstants.GENERAL_ALERTS] = oGeneralAlertsCollection;
                    return oGeneralAlertsCollection;
                }
            }
            else
            {
                return new List<AlertMstInfo>();
            }
        }
    }

    public List<AlertMstInfo> SelectOtherAlerts
    {
        get
        {
            List<AlertMstInfo> oOtherAlertsCollection = null;
            if (ViewState[ViewStateConstants.OTHER_ALERTS] != null)
            {
                oOtherAlertsCollection = (List<AlertMstInfo>)ViewState[ViewStateConstants.OTHER_ALERTS];
            }
            else
            {
                IAlert oAlertClient = RemotingHelper.GetAlertObject();
                List<AlertMstInfo> oAlertMstInfoCollection = oAlertClient.SelectAllAlertMstInfo(SessionHelper.GetUserLanguage(), SessionHelper.GetBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID(), SessionHelper.CurrentCompanyID, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());

                oOtherAlertsCollection = oAlertMstInfoCollection.Where(a => a.AlertCategoryID == 3).ToList();
                ViewState[ViewStateConstants.OTHER_ALERTS] = oOtherAlertsCollection;
            }

            if (!SessionHelper.CurrentReconciliationPeriodID.HasValue || SessionHelper.CurrentReconciliationPeriodID.Value <= 0)
            {
                oOtherAlertsCollection.RemoveAll(a => a.AlertID != (short)WebEnums.Alert.PeriodInformationNotSetUp && a.AlertID != (short)WebEnums.Alert.HolidayCalendarNotSetUp);
            }

            return oOtherAlertsCollection;
        }
    }

    public List<AlertMstInfo> SelectSystemAdminAlerts
    {
        get
        {
            List<AlertMstInfo> oSystemAdminAlertsCollection = null;
            if (ViewState[ViewStateConstants.SYSTEM_ADMIN_ALERTS] != null)
            {
                oSystemAdminAlertsCollection = (List<AlertMstInfo>)ViewState[ViewStateConstants.SYSTEM_ADMIN_ALERTS];
            }
            else
            {
                IAlert oAlertClient = RemotingHelper.GetAlertObject();
                List<AlertMstInfo> oAlertMstInfoCollection = oAlertClient.SelectAllAlertMstInfo(SessionHelper.GetUserLanguage(), SessionHelper.GetBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID(), SessionHelper.CurrentCompanyID, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());

                oSystemAdminAlertsCollection = oAlertMstInfoCollection.Where(a => a.AlertCategoryID == 2).ToList();
                ViewState[ViewStateConstants.SYSTEM_ADMIN_ALERTS] = oSystemAdminAlertsCollection;
            }

            if (!SessionHelper.CurrentReconciliationPeriodID.HasValue || SessionHelper.CurrentReconciliationPeriodID.Value <= 0)
            {
                oSystemAdminAlertsCollection.RemoveAll(a => a.AlertID != (short)WebEnums.Alert.PeriodInformationNotSetUp && a.AlertID != (short)WebEnums.Alert.HolidayCalendarNotSetUp);
            }

            return oSystemAdminAlertsCollection;
        }
    }
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        MasterPageBase ompage = (MasterPageBase)this.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);
        rgGeneralAlerts.EntityNameLabelID = 1865;
        rgSysAdminAlerts.EntityNameLabelID = 1866;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Helper.SetPageTitle(this, 1222);
            this._CompanyAlertInfoCollection = SessionHelper.SelectComapnyAlertByCompanyIDAndRecPeriodID();
            if (!IsPostBack)
            {

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

    #region rgGeneralAlerts
    protected void rgGeneralAlerts_ItemDataBound(object sender, GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == GridItemType.Header)
            {
                _isCertificationStarted = CertificationHelper.IsCertificationStarted();
            }
            else if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                AlertMstInfo oAlertMstInfo = (AlertMstInfo)e.Item.DataItem;
                BindCommonItems(e);

                CheckBox checkBox = (CheckBox)(e.Item as GridDataItem)["CheckboxSelectColumn"].Controls[0];


                if (SessionHelper.CurrentReconciliationPeriodID.HasValue && SessionHelper.CurrentReconciliationPeriodID.Value > 0)
                {
                    if (CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Closed
                        || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Skipped
                        || _isCertificationStarted)
                    {
                        checkBox.Enabled = false;
                        DeselectGeneralAlerts.Value += e.Item.ItemIndex.ToString() + ":";
                    }
                }
                else
                {
                    checkBox.Enabled = true;
                }

                if (oAlertMstInfo.CapabilityID.HasValue && oAlertMstInfo.CapabilityID > 0)
                {
                    ARTEnums.Capability eCapability = (ARTEnums.Capability)Enum.Parse(typeof(ARTEnums.Capability), oAlertMstInfo.CapabilityID.ToString());

                    if (!Helper.IsCapabilityActivatedForCurrentRecPeriod(eCapability))
                    {
                        checkBox.Enabled = false;
                        DeselectGeneralAlerts.Value += e.Item.ItemIndex.ToString() + ":";
                    }
                }
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
    protected void rgGeneralAlerts_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        try
        {
            rgGeneralAlerts.DataSource = this.SelectGeneralAlerts;
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

    #region rgSysAdminAlerts
    protected void rgSysAdminAlerts_ItemDataBound(object sender, GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == GridItemType.Header)
            {
                _isCertificationStarted = CertificationHelper.IsCertificationStarted();
            }
            else if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                AlertMstInfo oAlertMstInfo = (AlertMstInfo)e.Item.DataItem;
                BindCommonItems(e);

                CheckBox checkBox = (CheckBox)(e.Item as GridDataItem)["CheckboxSelectColumn"].Controls[0];

                if (SessionHelper.CurrentReconciliationPeriodID.HasValue && SessionHelper.CurrentReconciliationPeriodID.Value > 0)
                {
                    if (CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Closed
                        || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Skipped
                        || _isCertificationStarted)
                    {
                        checkBox.Enabled = false;
                        DeselectSysAdminAlerts.Value += e.Item.ItemIndex.ToString() + ":";
                    }
                }
                else
                {
                    checkBox.Enabled = true;
                }

                if (oAlertMstInfo.CapabilityID.HasValue && oAlertMstInfo.CapabilityID > 0)
                {
                    ARTEnums.Capability eCapability = (ARTEnums.Capability)Enum.Parse(typeof(ARTEnums.Capability), oAlertMstInfo.CapabilityID.ToString());

                    if (!Helper.IsCapabilityActivatedForCurrentRecPeriod(eCapability))
                    {
                        checkBox.Enabled = false;
                        DeselectSysAdminAlerts.Value += e.Item.ItemIndex.ToString() + ":";
                    }
                }
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

    protected void rgSysAdminAlerts_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        try
        {
            rgSysAdminAlerts.DataSource = this.SelectSystemAdminAlerts;
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

    #region rgOtherAlerts
    protected void rgOtherAlerts_ItemDataBound(object sender, GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == GridItemType.Header)
            {
                _isCertificationStarted = CertificationHelper.IsCertificationStarted();
            }
            else if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                AlertMstInfo oAlertMstInfo = (AlertMstInfo)e.Item.DataItem;
                BindCommonItems(e);

                CheckBox checkBox = (CheckBox)(e.Item as GridDataItem)["CheckboxSelectColumn"].Controls[0];

                if (SessionHelper.CurrentReconciliationPeriodID.HasValue && SessionHelper.CurrentReconciliationPeriodID.Value > 0)
                {
                    if (CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Closed
                        || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Skipped
                        || _isCertificationStarted)
                    {
                        checkBox.Enabled = false;
                        DeselectOtherAlerts.Value += e.Item.ItemIndex.ToString() + ":";
                    }
                }
                else
                {
                    checkBox.Enabled = true;
                }

                if (oAlertMstInfo.CapabilityID.HasValue && oAlertMstInfo.CapabilityID > 0)
                {
                    ARTEnums.Capability eCapability = (ARTEnums.Capability)Enum.Parse(typeof(ARTEnums.Capability), oAlertMstInfo.CapabilityID.ToString());

                    if (!Helper.IsCapabilityActivatedForCurrentRecPeriod(eCapability))
                    {
                        checkBox.Enabled = false;
                        DeselectOtherAlerts.Value += e.Item.ItemIndex.ToString() + ":";
                    }
                }
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

    protected void rgOtherAlerts_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        try
        {
            rgOtherAlerts.DataSource = this.SelectSystemAdminAlerts;
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

    #endregion

    #region Other Events
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            List<CompanyAlertInfo> oCompanyAlertInfoCollection = new List<CompanyAlertInfo>();
            GetAlertsSelected(rgGeneralAlerts, oCompanyAlertInfoCollection);
            GetAlertsSelected(rgSysAdminAlerts, oCompanyAlertInfoCollection);
            GetAlertsSelected(rgOtherAlerts, oCompanyAlertInfoCollection);

            if (oCompanyAlertInfoCollection != null && oCompanyAlertInfoCollection.Count > 0)
            {
                IAlert oAlertClient = RemotingHelper.GetAlertObject();
                oAlertClient.UpdateCompanyAlert(oCompanyAlertInfoCollection, SessionHelper.CurrentReconciliationPeriodID.HasValue ? SessionHelper.CurrentReconciliationPeriodID.Value : 0, Helper.GetAppUserInfo());
                HttpContext.Current.Session[SessionConstants.ALL_COMPANY_ALERT_LIST] = null;
                int LabelID = 1929;
                //Response.Redirect("Home.aspx?" + QueryStringConstants.CONFIRMATION_MESSAGE_LABEL_ID + "=" + LabelID.ToString());
                SessionHelper.RedirectToUrl("Home.aspx?" + QueryStringConstants.CONFIRMATION_MESSAGE_LABEL_ID + "=" + LabelID.ToString());
                return;
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            //Response.Redirect("Home.aspx");
            SessionHelper.RedirectToUrl("Home.aspx");
            return;
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
    public void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        try
        {

            IAlert oAlertClient = RemotingHelper.GetAlertObject();

            this._CompanyAlertInfoCollection = SessionHelper.SelectComapnyAlertByCompanyIDAndRecPeriodID();
            _isCertificationStarted = CertificationHelper.IsCertificationStarted();

            if (SessionHelper.CurrentReconciliationPeriodID.HasValue && SessionHelper.CurrentReconciliationPeriodID.Value > 0)
            {
                if (CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Closed
                    || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Skipped
                    || _isCertificationStarted)
                {
                    btnSave.Visible = false;
                    pnlGrid.Enabled = false;
                    pnlSystemAdminGrid.Enabled = false;
                    pnlOtherAlerts.Enabled = false;
                }
                else
                {
                    pnlGrid.Enabled = true;
                    pnlSystemAdminGrid.Enabled = true;
                    pnlOtherAlerts.Enabled = true;
                    btnSave.Visible = true;
                }
            }
            else
            {
                btnSave.Visible = true;
            }

            PopulateGrids();

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

    #region Validation Control Events
    protected void NonEmptyAndPositive_OnServerValidate(object source, ServerValidateEventArgs args)
    {
        if (Regex.IsMatch(args.Value, @"^[1-9]+[0-9]*$"))
            args.IsValid = true;
        else
            args.IsValid = false;
    }
    #endregion

    #region Private Methods
    private void PopulateGrids()
    {
        DeselectGeneralAlerts.Value = string.Empty;
        DeselectSysAdminAlerts.Value = string.Empty;
        DeselectOtherAlerts.Value = string.Empty;

        rgGeneralAlerts.DataSource = this.SelectGeneralAlerts;
        rgGeneralAlerts.DataBind();

        rgSysAdminAlerts.DataSource = this.SelectSystemAdminAlerts;
        rgSysAdminAlerts.DataBind();

        rgOtherAlerts.DataSource = this.SelectOtherAlerts;
        rgOtherAlerts.DataBind();
    }
    private void BindCommonItems(GridItemEventArgs e)
    {
        AlertMstInfo oAlertMstInfo = (AlertMstInfo)e.Item.DataItem;
        CompanyAlertInfo oCompanyAlertInfo = (from alert in this._CompanyAlertInfoCollection where alert.AlertID == oAlertMstInfo.AlertID select alert).FirstOrDefault();


        CheckBox checkBox = (CheckBox)(e.Item as GridDataItem)["CheckboxSelectColumn"].Controls[0];
        ExLabel lblAlert = (ExLabel)e.Item.FindControl("lblAlert");
        ExLabel lblCondition = (ExLabel)e.Item.FindControl("lblCondition");
        ExLabel lblAlertType = (ExLabel)e.Item.FindControl("lblAlertType");
        TextBox txtThreshold = (TextBox)e.Item.FindControl("txtThreshold");
        ExLabel lblPercentOrDays = (ExLabel)e.Item.FindControl("lblPercentOrDays");
        ExLabel lblRecurrence = (ExLabel)e.Item.FindControl("lblRecurrence");
        TextBox txtRecurrence = (TextBox)e.Item.FindControl("txtRecurrence");
        ExLabel lblHours = (ExLabel)e.Item.FindControl("lblHours");
        ExCustomValidator vldThreshold = (ExCustomValidator)e.Item.FindControl("vldThreshold");
        ExCustomValidator vldRecurrence = (ExCustomValidator)e.Item.FindControl("vldRecurrence");

        lblAlert.Text = oAlertMstInfo.AlertDisplay;
        lblCondition.Text = oAlertMstInfo.Condition;
        lblAlertType.Text = oAlertMstInfo.AlertType;
        lblPercentOrDays.Text = oAlertMstInfo.ThresholdType;
        lblRecurrence.Text = Helper.GetLabelIDValue(1863);
        lblHours.Text = Helper.GetLabelIDValue(1864);
        string errorMessage = LanguageUtil.GetValue(5000357);
        vldThreshold.ErrorMessage = string.Format(errorMessage, LanguageUtil.GetValue(1860));
        vldRecurrence.ErrorMessage = string.Format(errorMessage, LanguageUtil.GetValue(1861));

        if (oAlertMstInfo.AlertResponseTypeID == (short)WebEnums.AlertResponseType.Recurring
            || oAlertMstInfo.AlertResponseTypeID == (short)WebEnums.AlertResponseType.Mix)
        {
            if (oCompanyAlertInfo != null)
            {
                if (oAlertMstInfo.AlertResponseTypeID == (short)WebEnums.AlertResponseType.Recurring)
                {
                    if (oCompanyAlertInfo.Threshold.HasValue && oCompanyAlertInfo.Threshold.Value > 0)
                    {
                        txtThreshold.Text = oCompanyAlertInfo.Threshold.ToString();
                    }
                    else
                    {
                        txtThreshold.Text = oAlertMstInfo.DefaultThreshold.ToString();
                    }
                }

                if (oCompanyAlertInfo.NoOfHours.HasValue && oCompanyAlertInfo.NoOfHours.Value > 0)
                {
                    txtRecurrence.Text = oCompanyAlertInfo.NoOfHours.ToString();
                }
            }

            txtRecurrence.Visible = true;
            vldRecurrence.Visible = true;
            lblRecurrence.Visible = true;
            lblHours.Visible = true;

            if (oAlertMstInfo.AlertResponseTypeID == (short)WebEnums.AlertResponseType.Recurring)
            {
                txtThreshold.Visible = true;
                vldThreshold.Visible = true;
                lblPercentOrDays.Visible = true;
            }
            else
            {
                txtThreshold.Visible = false;
                lblPercentOrDays.Visible = false;
                vldThreshold.Visible = false;
            }
        }
        else
        {
            txtThreshold.Visible = false;
            vldThreshold.Visible = false;
            txtRecurrence.Visible = false;
            vldRecurrence.Visible = false;
            lblPercentOrDays.Visible = false;
            lblRecurrence.Visible = false;
            lblHours.Visible = false;
        }

        if (oCompanyAlertInfo != null)
        {
            e.Item.Selected = true;
            vldThreshold.Enabled = true;
            vldRecurrence.Enabled = true;
        }
    }
    private void GetAlertsSelected(ExRadGrid rgAlerts, List<CompanyAlertInfo> oCompanyAlertInfoCollection)
    {
        DateTime updatedTime = DateTime.Now;

        foreach (GridDataItem item in rgAlerts.Items)
        {
            short alertID = (short)item.GetDataKeyValue("AlertID");
            short? thresholdTypeID = (short?)item.GetDataKeyValue("DefaultThresholdTypeID");
            TextBox txtRecurrence = (TextBox)item.FindControl("txtRecurrence");
            TextBox txtThreshold = (TextBox)item.FindControl("txtThreshold");
            CheckBox chkSelectItem = (CheckBox)item["CheckboxSelectColumn"].Controls[0];
            CompanyAlertInfo oCompanyAlertInfo = new CompanyAlertInfo();
            oCompanyAlertInfo.AddedBy = SessionHelper.CurrentUserLoginID;
            oCompanyAlertInfo.AlertID = alertID;
            oCompanyAlertInfo.CompanyID = SessionHelper.CurrentCompanyID;
            oCompanyAlertInfo.DateAdded = updatedTime;
            if (chkSelectItem != null)
                oCompanyAlertInfo.IsActive = chkSelectItem.Checked;

            if (txtRecurrence != null && !string.IsNullOrEmpty(txtRecurrence.Text))
            {
                oCompanyAlertInfo.NoOfHours = Convert.ToInt16(txtRecurrence.Text);
            }

            if (txtThreshold != null && !string.IsNullOrEmpty(txtThreshold.Text))
            {
                oCompanyAlertInfo.Threshold = Convert.ToInt16(txtThreshold.Text);
            }

            if (thresholdTypeID.HasValue && thresholdTypeID.Value > 0)
            {
                oCompanyAlertInfo.ThresholdTypeID = thresholdTypeID;
            }

            oCompanyAlertInfoCollection.Add(oCompanyAlertInfo);
        }
    }

    private void GetGeneralAlertsSelected(List<CompanyAlertInfo> oCompanyAlertInfoCollection)
    {
        throw new NotImplementedException();
    }


    #endregion

    #region Other Methods
    public override string GetMenuKey()
    {
        return "ConfigureAlerts";
    }
    #endregion

}
