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
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Data;
using Telerik.Web.UI;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.IServices;
using System.Collections.Generic;
using SkyStem.Language.LanguageUtility;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.ART.Client.Data;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using SkyStem.ART.Client.Exception;

//TODO: if system admin then which ceo_Cfo status will be shown on top grid. what if multiple CEOCFO, also
//should we show it on bottom grid
//WHen user is admin then it should see all the approvers in the bottom grid, so imclude this in sp

public partial class Pages_CertificationHome : PageBaseRecPeriod
{

    bool isExportPDF;
    bool isExportExcel;
    private const WebEnums.ARTPages eARTPages = WebEnums.ARTPages.CertificationStatus;
    int? _CompanyID = 0;
    int? _ReconciliationPeriodID = 0;
    short? _CurrentUserRole = 0;

    private bool _IsDualReviewEnabled;
    private bool _IsCertificationEnabled;
    private bool _IsCEOCFOCertificationEnabled;
    private bool _IsMandatoryReportSignoffEnabled;

    protected void Page_Init(object sender, EventArgs e)
    {
        MasterPageBase ompage = (MasterPageBase)this.Master.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);

        Helper.SetPageTitle(this, 1464);
        Helper.ShowExportToolbarOnCertificationPages(this, false);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Helper.RegisterPostBackToControls(this, rgStatus);
            Helper.RegisterPostBackToControls(this, rgOtherUsers);

            if (!Page.IsPostBack)
            {
                isExportPDF = false;
                isExportExcel = false;
            }
            bool isShowContent = Helper.ShowHideContentOnCertificationPages(this, eARTPages);
            if (isShowContent)
            {
                CallEveryTime();
                if (!IsPostBack)
                {
                    CallFirstTime();
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
    protected void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {

        try
        {
            if (IsPostBack)
            {
                bool isShowContent = Helper.ShowHideContentOnCertificationPages(this, eARTPages);
                if (isShowContent)
                {
                    CallEveryTime();
                    CallFirstTime();
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
    private void CallEveryTime()
    {
        SetCapabilityInfo();
        SetPrivateVariableValue();
    }

    private void CallFirstTime()
    {
        ShowHideBasedOnRoles();
        if (pnlMyStatus.Visible == true)
        {
            FillMyStatus();
        }
        if (pnlJuniorStatus.Visible == true)
        {
            FillJuniorGridStatus();
        }

        if (pnlJuniorStatus.Visible == true)
        {
            FillJuniorOtherThanPRAGridStatus();
        }



    }
    private void FillMyStatus()
    {
        ICertification oCertificationClient = RemotingHelper.GetCertificationObject();
        List<CertificationSignOffInfo> oCertificationSignOffInfoCollection = oCertificationClient.GetCertificationSignOff(SessionHelper.CurrentReconciliationPeriodID, SessionHelper.CurrentUserID, SessionHelper.CurrentRoleID, Helper.GetAppUserInfo());
        if (oCertificationSignOffInfoCollection != null && oCertificationSignOffInfoCollection.Count > 0)
        {
            SetYesNoCodeBasedOnSignOffDate(oCertificationSignOffInfoCollection[0].MadatoryReportSignOffDate, lblMandatoryReportSignoffStatus, lblMandatoryReportSignoffDate);
            SetYesNoCodeBasedOnSignOffDate(oCertificationSignOffInfoCollection[0].CertificationBalancesDate, lblCertificationBalancesStatus, lblCertificationBalancesDate);
            SetYesNoCodeBasedOnSignOffDate(oCertificationSignOffInfoCollection[0].ExceptionCertificationDate, lblExceptionCertificationStatus, lblExceptionCertificationDate);
            SetYesNoCodeBasedOnSignOffDate(oCertificationSignOffInfoCollection[0].AccountCertificationDate, lblCertificationStatus, lblCertificationDate);
        }
        else
        {
            SetYesNoCodeBasedOnSignOffDate(null, lblMandatoryReportSignoffStatus, lblMandatoryReportSignoffDate);
            SetYesNoCodeBasedOnSignOffDate(null, lblCertificationBalancesStatus, lblCertificationBalancesDate);
            SetYesNoCodeBasedOnSignOffDate(null, lblExceptionCertificationStatus, lblExceptionCertificationDate);
            SetYesNoCodeBasedOnSignOffDate(null, lblCertificationStatus, lblCertificationDate);
        }
        string urlToBeAddedForBreadCrum = "?" + QueryStringConstants.User_ID + "=" + SessionHelper.CurrentUserID;
        hlMandatoryReportSignoff.NavigateUrl = "MandatoryReportsList.aspx" + urlToBeAddedForBreadCrum;
        hlCertificationBalances.NavigateUrl = "CertificationBalances.aspx" + urlToBeAddedForBreadCrum;
        hlExceptionCertification.NavigateUrl = "CertificationException.aspx" + urlToBeAddedForBreadCrum;
        hlCertification.NavigateUrl = "CertificationSignOff.aspx" + urlToBeAddedForBreadCrum;
        lblStatusHeading.Text = string.Format(LanguageUtil.GetValue(1840), Helper.GetDisplayDate(DateTime.Now));
    }

    private void FillJuniorGridStatus()
    {
        if (_CurrentUserRole == (short)WebEnums.UserRole.REVIEWER)
        {
            rgStatus.MasterTableView.DataSource = LoadUserData(SessionHelper.CurrentUserID, WebEnums.UserRole.REVIEWER);
            rgStatus.MasterTableView.Columns[0].HeaderText = LanguageUtil.GetValue(1130);
            rgStatus.MasterTableView.DetailTables.Clear();
            lblStatusLinkHeading.LabelID = 1458;
            GridColumn oGridColumn1 = rgStatus.MasterTableView.Columns.FindByUniqueNameSafe("UserName");
            GridColumn oGridColumn2 = rgStatus.MasterTableView.Columns.FindByUniqueNameSafe("BackupUserName");
            if (oGridColumn1 != null)
            {
                oGridColumn1.HeaderText = LanguageUtil.GetValue(1130);
            }
            if (oGridColumn2 != null)
            {
                oGridColumn2.HeaderText = LanguageUtil.GetValue(2501);
            }
            rgStatus.DataBind();
        }
        else
            if (_CurrentUserRole == (short)WebEnums.UserRole.BACKUP_REVIEWER)
            {
                rgStatus.MasterTableView.DataSource = LoadUserData(SessionHelper.CurrentUserID, WebEnums.UserRole.BACKUP_REVIEWER);
                rgStatus.MasterTableView.Columns[0].HeaderText = LanguageUtil.GetValue(1130);
                rgStatus.MasterTableView.DetailTables.Clear();
                lblStatusLinkHeading.LabelID = 1458;
                GridColumn oGridColumn1 = rgStatus.MasterTableView.Columns.FindByUniqueNameSafe("UserName");
                GridColumn oGridColumn2 = rgStatus.MasterTableView.Columns.FindByUniqueNameSafe("BackupUserName");
                if (oGridColumn1 != null)
                {
                    oGridColumn1.HeaderText = LanguageUtil.GetValue(1130);
                }
                if (oGridColumn2 != null)
                {
                    oGridColumn2.HeaderText = LanguageUtil.GetValue(2501);
                }
                rgStatus.DataBind();
            }
            else
                if (_CurrentUserRole == (short)WebEnums.UserRole.APPROVER)
                {
                    rgStatus.MasterTableView.DataSource = LoadUserData(SessionHelper.CurrentUserID, WebEnums.UserRole.APPROVER);
                    rgStatus.MasterTableView.Columns[0].HeaderText = LanguageUtil.GetValue(1131);
                    rgStatus.MasterTableView.DetailTables[0].DetailTables.Clear();
                    lblStatusLinkHeading.LabelID = 1703;
                    GridColumn oGridColumn1 = rgStatus.MasterTableView.Columns.FindByUniqueNameSafe("UserName");
                    GridColumn oGridColumn2 = rgStatus.MasterTableView.Columns.FindByUniqueNameSafe("BackupUserName");
                    if (oGridColumn1 != null)
                    {
                        oGridColumn1.HeaderText = LanguageUtil.GetValue(1131);
                    }
                    if (oGridColumn2 != null)
                    {
                        oGridColumn2.HeaderText = LanguageUtil.GetValue(2502);
                    }
                    GridColumn oGridColumn3 = rgStatus.MasterTableView.DetailTables[0].Columns.FindByUniqueNameSafe("UserName");
                    GridColumn oGridColumn4 = rgStatus.MasterTableView.DetailTables[0].Columns.FindByUniqueNameSafe("BackupUserName");
                    if (oGridColumn3 != null)
                    {
                        oGridColumn3.HeaderText = LanguageUtil.GetValue(1130);
                    }
                    if (oGridColumn4 != null)
                    {
                        oGridColumn4.HeaderText = LanguageUtil.GetValue(2501);
                    }
                    rgStatus.DataBind();
                }
                else
                    if (_CurrentUserRole == (short)WebEnums.UserRole.BACKUP_APPROVER)
                    {
                        rgStatus.MasterTableView.DataSource = LoadUserData(SessionHelper.CurrentUserID, WebEnums.UserRole.BACKUP_APPROVER);
                        rgStatus.MasterTableView.Columns[0].HeaderText = LanguageUtil.GetValue(1131);
                        rgStatus.MasterTableView.DetailTables[0].DetailTables.Clear();
                        lblStatusLinkHeading.LabelID = 1703;
                        GridColumn oGridColumn1 = rgStatus.MasterTableView.Columns.FindByUniqueNameSafe("UserName");
                        GridColumn oGridColumn2 = rgStatus.MasterTableView.Columns.FindByUniqueNameSafe("BackupUserName");
                        if (oGridColumn1 != null)
                        {
                            oGridColumn1.HeaderText = LanguageUtil.GetValue(1131);
                        }
                        if (oGridColumn2 != null)
                        {
                            oGridColumn2.HeaderText = LanguageUtil.GetValue(2502);
                        }
                        GridColumn oGridColumn3 = rgStatus.MasterTableView.DetailTables[0].Columns.FindByUniqueNameSafe("UserName");
                        GridColumn oGridColumn4 = rgStatus.MasterTableView.DetailTables[0].Columns.FindByUniqueNameSafe("BackupUserName");
                        if (oGridColumn3 != null)
                        {
                            oGridColumn3.HeaderText = LanguageUtil.GetValue(1130);
                        }
                        if (oGridColumn4 != null)
                        {
                            oGridColumn4.HeaderText = LanguageUtil.GetValue(2501);
                        }
                        rgStatus.DataBind();
                    }
                    else
                        if (_CurrentUserRole == (short)WebEnums.UserRole.CEO_CFO || _CurrentUserRole == (short)WebEnums.UserRole.EXECUTIVE || _CurrentUserRole == (short)WebEnums.UserRole.CONTROLLER
                             || _CurrentUserRole == (short)WebEnums.UserRole.FINANCIAL_MANAGER || _CurrentUserRole == (short)WebEnums.UserRole.ACCOUNT_MANAGER)
                        {

                            if (_CurrentUserRole == (short)WebEnums.UserRole.CEO_CFO)
                            {
                                rgStatus.MasterTableView.DataSource = LoadUserData(SessionHelper.CurrentUserID, WebEnums.UserRole.CEO_CFO);

                            }
                            else if (_CurrentUserRole == (short)WebEnums.UserRole.EXECUTIVE)
                            {
                                rgStatus.MasterTableView.DataSource = LoadUserData(SessionHelper.CurrentUserID, WebEnums.UserRole.EXECUTIVE);

                            }
                            else if (_CurrentUserRole == (short)WebEnums.UserRole.CONTROLLER)
                            {
                                rgStatus.MasterTableView.DataSource = LoadUserData(SessionHelper.CurrentUserID, WebEnums.UserRole.CONTROLLER);

                            }
                            else if (_CurrentUserRole == (short)WebEnums.UserRole.FINANCIAL_MANAGER)
                            {
                                rgStatus.MasterTableView.DataSource = LoadUserData(SessionHelper.CurrentUserID, WebEnums.UserRole.FINANCIAL_MANAGER);

                            }
                            else if (_CurrentUserRole == (short)WebEnums.UserRole.ACCOUNT_MANAGER)
                            {
                                rgStatus.MasterTableView.DataSource = LoadUserData(SessionHelper.CurrentUserID, WebEnums.UserRole.ACCOUNT_MANAGER);

                            }


                            if (_IsDualReviewEnabled)
                            {
                                lblStatusLinkHeading.LabelID = 1833;

                                GridColumn oGridColumn1 = rgStatus.MasterTableView.Columns.FindByUniqueNameSafe("UserName");
                                if (oGridColumn1 != null)
                                {
                                    oGridColumn1.HeaderText = LanguageUtil.GetValue(1132);
                                }
                                GridColumn oGridColumn2 = rgStatus.MasterTableView.DetailTables[0].Columns.FindByUniqueNameSafe("UserName");
                                if (oGridColumn2 != null)
                                {
                                    oGridColumn2.HeaderText = LanguageUtil.GetValue(1131);
                                }
                                GridColumn oGridColumn3 = rgStatus.MasterTableView.DetailTables[0].DetailTables[0].Columns.FindByUniqueNameSafe("UserName");
                                if (oGridColumn3 != null)
                                {
                                    oGridColumn3.HeaderText = LanguageUtil.GetValue(1130);
                                }
                            }
                            else
                            {
                                lblStatusLinkHeading.LabelID = 1703;

                                GridColumn oGridColumn1 = rgStatus.MasterTableView.Columns.FindByUniqueNameSafe("UserName");
                                if (oGridColumn1 != null)
                                {
                                    oGridColumn1.HeaderText = LanguageUtil.GetValue(1131);
                                }
                                GridColumn oGridColumn2 = rgStatus.MasterTableView.DetailTables[0].Columns.FindByUniqueNameSafe("UserName");
                                if (oGridColumn2 != null)
                                {
                                    oGridColumn2.HeaderText = LanguageUtil.GetValue(1130);
                                }
                            }
                            rgStatus.DataBind();
                        }
                        else
                            lblStatusLinkHeading.Visible = false;
    }

    private void ShowHideBasedOnRoles()
    {
        pnlMyStatus.Visible = false;
        pnlJuniorStatus.Visible = false;
        pnlOtherThanPRAStatus.Visible = false;

        bool bMyStatus = false;
        bool bJuniorsStatus = false;
        bool bOtherThanPRAStatus = false;
        bool bMyStatusMandatoryReport = false;
        bool bMyStatusCertificationActivation = false;

        bMyStatus = _IsCertificationEnabled;
        bMyStatusCertificationActivation = _IsCertificationEnabled;
        bJuniorsStatus = _IsCertificationEnabled;

        WebEnums.UserRole eUserRole = (WebEnums.UserRole)System.Enum.Parse(typeof(WebEnums.UserRole), _CurrentUserRole.Value.ToString());

        switch (eUserRole)
        {
            case WebEnums.UserRole.PREPARER: // Juniors will never be visible to P
            case WebEnums.UserRole.BACKUP_PREPARER: // Juniors will never be visible to Backup PREPARER
                bJuniorsStatus = false;
                break;

            case WebEnums.UserRole.REVIEWER: // Override MyStatus for R and R
            case WebEnums.UserRole.APPROVER:
            case WebEnums.UserRole.BACKUP_REVIEWER:
            case WebEnums.UserRole.BACKUP_APPROVER:
                bMyStatusMandatoryReport = _IsMandatoryReportSignoffEnabled;
                bMyStatus = _IsMandatoryReportSignoffEnabled || _IsCertificationEnabled;
                break;

            case WebEnums.UserRole.CEO_CFO: // Override MyStatus for CEO / CFO
                bMyStatus = _IsCEOCFOCertificationEnabled;
                bOtherThanPRAStatus = _IsCertificationEnabled;
                bMyStatusCertificationActivation = _IsCEOCFOCertificationEnabled;
                break;

            case WebEnums.UserRole.CONTROLLER:
                bOtherThanPRAStatus = _IsCertificationEnabled;
                break;
        }

        pnlMyStatus.Visible = bMyStatus;
        if (bMyStatus)
        {
            trMandatoryReport.Visible = bMyStatusMandatoryReport;
            trCertificationBalances.Visible = bMyStatusCertificationActivation;
            trExceptionCertification.Visible = bMyStatusCertificationActivation;
            trCertification.Visible = bMyStatusCertificationActivation;
        }

        pnlJuniorStatus.Visible = bJuniorsStatus;
        pnlOtherThanPRAStatus.Visible = bOtherThanPRAStatus;

        //if (_IsCertificationEnabled && _IsCEOCFOCertificationEnabled)
        //{
        //    switch (_CurrentUserRole)
        //    {
        //        case (short)WebEnums.UserRole.PREPARER:
        //        case (short)WebEnums.UserRole.REVIEWER:
        //        case (short)WebEnums.UserRole.APPROVER:
        //        case (short)WebEnums.UserRole.EXECUTIVE:
        //        case (short)WebEnums.UserRole.CONTROLLER:
        //        case (short)WebEnums.UserRole.ACCOUNT_MANAGER:
        //        case (short)WebEnums.UserRole.FINANCIAL_MANAGER:

        //        case (short)WebEnums.UserRole.CEO_CFO:
        //            pnlMyStatus.Visible = true;
        //            pnlJuniorStatus.Visible = true;

        //            //FillMyStatus();
        //            break;
        //        case (short)WebEnums.UserRole.SYSTEM_ADMIN:
        //        case (short)WebEnums.UserRole.SKYSTEM_ADMIN:
        //            pnlJuniorStatus.Visible = true;
        //            break;
        //        default:
        //            break;
        //    }
        //}
        //else if (!_IsCertificationEnabled && _IsCEOCFOCertificationEnabled)
        //{
        //    switch (_CurrentUserRole)
        //    {
        //        case (short)WebEnums.UserRole.CEO_CFO:
        //            pnlMyStatus.Visible = true;
        //            //FillMyStatus();
        //            break;
        //        default:
        //            break;
        //    }
        //}
        //else if (_IsCertificationEnabled && !_IsCEOCFOCertificationEnabled)
        //{
        //    switch (_CurrentUserRole)
        //    {
        //        case (short)WebEnums.UserRole.PREPARER:
        //        case (short)WebEnums.UserRole.REVIEWER:
        //        case (short)WebEnums.UserRole.APPROVER:
        //            pnlMyStatus.Visible = true;
        //            pnlJuniorStatus.Visible = true;

        //            //FillMyStatus();
        //            break;
        //        case (short)WebEnums.UserRole.CEO_CFO:
        //        case (short)WebEnums.UserRole.SYSTEM_ADMIN:
        //        case (short)WebEnums.UserRole.SKYSTEM_ADMIN:
        //            pnlJuniorStatus.Visible = true;
        //            break;
        //        default:
        //            break;
        //    }
        //}

        //if (_CurrentUserRole == (short)WebEnums.UserRole.PREPARER)
        //{
        //    trMandatoryReport.Visible = false;
        //    pnlJuniorStatus.Visible = false;
        //}
        //else if (_CurrentUserRole == (short)WebEnums.UserRole.CEO_CFO)
        //{
        //    pnlOtherThanPRAStatus.Visible = true;
        //    trMandatoryReport.Visible = false;
        //}
        //else if (_CurrentUserRole == (short)WebEnums.UserRole.CONTROLLER)
        //{
        //    pnlOtherThanPRAStatus.Visible = true;

        //}

    }

    private void SetYesNoCodeBasedOnSignOffDate(DateTime? dateSignoff, ExLabel lblStatus, ExLabel lblDate)
    {
        if (dateSignoff.HasValue)
        {
            lblStatus.Text = LanguageUtil.GetValue(WebConstants.LABEL_ID_YES);
        }
        else
        {
            lblStatus.Text = LanguageUtil.GetValue(WebConstants.LABEL_ID_NO);
        }
        lblDate.Text = Helper.GetDisplayDate(dateSignoff);
    }
    private void SetYesNoCodeBasedOnSignOffDate(DateTime? dateSignoff, ExHyperLink hlStatus, ExHyperLink hlDate)
    {
        if (dateSignoff.HasValue)
        {
            hlStatus.Text = LanguageUtil.GetValue(WebConstants.LABEL_ID_YES);
        }
        else
        {
            hlStatus.Text = LanguageUtil.GetValue(WebConstants.LABEL_ID_NO);
        }
        hlDate.Text = Helper.GetDisplayDate(dateSignoff);
    }

    private void SetYesNoCodeBasedOnSignOffDate(DateTime? dateSignoff, DateTime? dateBackupSignoff, string userName, string backupUserName, ExHyperLink hlStatus, ExHyperLink hlDate)
    {
        if (dateSignoff.HasValue)
        {
            hlStatus.Text = LanguageUtil.GetValue(WebConstants.LABEL_ID_YES) + " (" + userName + ")";
            hlDate.Text = Helper.GetDisplayDate(dateSignoff);
        }
        else if (dateBackupSignoff.HasValue)
        {
            hlStatus.Text = LanguageUtil.GetValue(WebConstants.LABEL_ID_YES) + " (" + backupUserName + ")";
            hlDate.Text = Helper.GetDisplayDate(dateBackupSignoff);
        }
        else
        {
            hlStatus.Text = LanguageUtil.GetValue(WebConstants.LABEL_ID_NO);
            hlDate.Text = Helper.GetDisplayDate(dateSignoff);
        }
    }

    private void SetUrlBasedOnSignOffDate(string pageUrl, DateTime? dateSignoff, DateTime? dateBackupSignoff, int? userID, int? roleID, int? backupUserID, short? backupRoleID, ExHyperLink hlStatus, ExHyperLink hlDate)
    {
        if (dateSignoff.HasValue && userID.HasValue && roleID.HasValue)
        {
            hlStatus.NavigateUrl = pageUrl + QueryStringConstants.User_ID + "=" + userID.ToString() + "&" + QueryStringConstants.ROLE_ID + "=" + roleID.ToString();
            hlDate.NavigateUrl = hlStatus.NavigateUrl;
        }
        else if (dateBackupSignoff.HasValue && backupUserID.HasValue && backupRoleID.HasValue)
        {
            hlStatus.NavigateUrl = pageUrl + QueryStringConstants.User_ID + "=" + backupUserID.ToString() + "&" + QueryStringConstants.ROLE_ID + "=" + backupRoleID.ToString();
            hlDate.NavigateUrl = hlStatus.NavigateUrl;
        }
    }

    private List<CertificationSignOffInfo> LoadUserData(int? userID, WebEnums.UserRole eUserRole)
    {
        ICertification oCertificationClient = RemotingHelper.GetCertificationObject();
        List<CertificationSignOffInfo> oCertificationSignOffInfoCollection = oCertificationClient.GetCertificationSignOffForJuniors(SessionHelper.CurrentReconciliationPeriodID, userID, (short)eUserRole, SessionHelper.CurrentUserID, SessionHelper.CurrentRoleID, Helper.GetAppUserInfo());
        return oCertificationSignOffInfoCollection;
    }

    protected void rgStatus_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            CertificationSignOffInfo oCertificationSignOffInfo = (CertificationSignOffInfo)e.Item.DataItem;

            ExHyperLink hlCertificationBalancesStatus = (ExHyperLink)e.Item.FindControl("hlCertificationBalancesStatus");
            ExHyperLink hlDateCertificationBalances = (ExHyperLink)e.Item.FindControl("hlDateCertificationBalances");
            ExHyperLink hlCertificationExceptionStatus = (ExHyperLink)e.Item.FindControl("hlCertificationExceptionStatus");
            ExHyperLink hlDateCertificationException = (ExHyperLink)e.Item.FindControl("hlDateCertificationException");
            ExHyperLink hlCertificationStatus = (ExHyperLink)e.Item.FindControl("hlCertificationStatus");
            ExHyperLink hlDateCertification = (ExHyperLink)e.Item.FindControl("hlDateCertification");
            ExLabel lblUserName = (ExLabel)e.Item.FindControl("lblUserName");
            ExLabel lblBackupUserName = (ExLabel)e.Item.FindControl("lblBackupUserName");
            ExImage imgNoAccess = (ExImage)e.Item.FindControl("imgNoAccess");
            ExImage imgSharedAccess = (ExImage)e.Item.FindControl("imgSharedAccess");


            SetYesNoCodeBasedOnSignOffDate(oCertificationSignOffInfo.CertificationBalancesDate, oCertificationSignOffInfo.BackupCertificationBalancesDate, oCertificationSignOffInfo.Name, oCertificationSignOffInfo.BackupUserName, hlCertificationBalancesStatus, hlDateCertificationBalances);
            SetYesNoCodeBasedOnSignOffDate(oCertificationSignOffInfo.ExceptionCertificationDate, oCertificationSignOffInfo.BackupExceptionCertificationDate, oCertificationSignOffInfo.Name, oCertificationSignOffInfo.BackupUserName, hlCertificationExceptionStatus, hlDateCertificationException);
            SetYesNoCodeBasedOnSignOffDate(oCertificationSignOffInfo.AccountCertificationDate, oCertificationSignOffInfo.BackupAccountCertificationDate, oCertificationSignOffInfo.Name, oCertificationSignOffInfo.BackupUserName, hlCertificationStatus, hlDateCertification);
            if (lblUserName != null)
            {
                lblUserName.Text = Helper.GetDisplayStringValue(oCertificationSignOffInfo.Name);
            }
            if (lblBackupUserName != null)
            {
                lblBackupUserName.Text = Helper.GetDisplayStringValue(oCertificationSignOffInfo.BackupUserName);
            }
            if (oCertificationSignOffInfo.IsSameAccess.HasValue && !oCertificationSignOffInfo.IsSameAccess.Value)
            {
                imgNoAccess.Visible = true;
                imgSharedAccess.Visible = false;
            }
            else if (!oCertificationSignOffInfo.IsSameAccess.HasValue || (oCertificationSignOffInfo.IsSameAccess.HasValue && oCertificationSignOffInfo.IsSameAccess.Value))
            {
                short? roleID = oCertificationSignOffInfo.RoleID;
                SetUrlBasedOnSignOffDate("CertificationBalances.aspx?", oCertificationSignOffInfo.CertificationBalancesDate, oCertificationSignOffInfo.BackupCertificationBalancesDate, oCertificationSignOffInfo.UserID, oCertificationSignOffInfo.RoleID, oCertificationSignOffInfo.BackupUserID, oCertificationSignOffInfo.BackupRoleID, hlCertificationBalancesStatus, hlDateCertificationBalances);
                SetUrlBasedOnSignOffDate("CertificationException.aspx?", oCertificationSignOffInfo.ExceptionCertificationDate, oCertificationSignOffInfo.BackupExceptionCertificationDate, oCertificationSignOffInfo.UserID, oCertificationSignOffInfo.RoleID, oCertificationSignOffInfo.BackupUserID, oCertificationSignOffInfo.BackupRoleID, hlCertificationExceptionStatus, hlDateCertificationException);
                SetUrlBasedOnSignOffDate("CertificationSignOff.aspx?", oCertificationSignOffInfo.AccountCertificationDate, oCertificationSignOffInfo.BackupAccountCertificationDate, oCertificationSignOffInfo.UserID, oCertificationSignOffInfo.RoleID, oCertificationSignOffInfo.BackupUserID, oCertificationSignOffInfo.BackupRoleID, hlCertificationStatus, hlDateCertification);

                if (!oCertificationSignOffInfo.IsSameAccess.HasValue)//Shared access
                {
                    imgNoAccess.Visible = false;
                    imgSharedAccess.Visible = true;
                }
                else //Complete access
                {
                    imgNoAccess.Visible = false;
                    imgSharedAccess.Visible = false;
                }
            }

        }
    }

    protected void rgStatus_DetailTableDataBind(object source, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
    {
        //string expandImageUrl= "~/App_Themes/SkyStemBlueBrown/Images/ExpandRow.gif";
        //string collapseImageUrl= "~/App_Themes/SkyStemBlueBrown/Images/CollapseRow.gif";
        _CurrentUserRole = SessionHelper.CurrentRoleID;
        GridDataItem oGridItem = e.DetailTableView.ParentItem;
        int? userID = Convert.ToInt32(oGridItem.GetDataKeyValue("UserID"));

        switch (e.DetailTableView.Name)
        {
            //case "lavel1":
            //    break;
            case "lavel2":
                switch (_CurrentUserRole)
                {
                    case (short)WebEnums.UserRole.APPROVER:
                        e.DetailTableView.DataSource = LoadUserData(userID, WebEnums.UserRole.REVIEWER);
                        break;
                    case (short)WebEnums.UserRole.BACKUP_APPROVER:
                        e.DetailTableView.DataSource = LoadUserData(userID, WebEnums.UserRole.BACKUP_REVIEWER);
                        break;
                    case (short)WebEnums.UserRole.CEO_CFO:
                    case (short)WebEnums.UserRole.EXECUTIVE:
                    case (short)WebEnums.UserRole.CONTROLLER:
                    case (short)WebEnums.UserRole.FINANCIAL_MANAGER:
                    case (short)WebEnums.UserRole.ACCOUNT_MANAGER:
                        if (_IsDualReviewEnabled)
                        {
                            e.DetailTableView.DataSource = LoadUserData(userID, WebEnums.UserRole.APPROVER);
                        }
                        else
                        {
                            e.DetailTableView.DataSource = LoadUserData(userID, WebEnums.UserRole.REVIEWER);
                        }
                        break;
                }
                break;
            case "lavel3":
                switch (_CurrentUserRole)
                {
                    case (short)WebEnums.UserRole.CEO_CFO:
                    case (short)WebEnums.UserRole.EXECUTIVE:
                    case (short)WebEnums.UserRole.CONTROLLER:
                    case (short)WebEnums.UserRole.FINANCIAL_MANAGER:
                    case (short)WebEnums.UserRole.ACCOUNT_MANAGER:
                        if (_IsDualReviewEnabled)
                        {
                            e.DetailTableView.DataSource = LoadUserData(userID, WebEnums.UserRole.REVIEWER);
                        }
                        else
                        {
                            e.DetailTableView.DataSource = LoadUserData(userID, WebEnums.UserRole.PREPARER);

                        }
                        break;
                }
                break;
        }
    }
    public override string GetMenuKey()
    {
        return "CertificationStatus";
    }
    private void SetPrivateVariableValue()
    {
        _CompanyID = SessionHelper.CurrentCompanyID;
        _ReconciliationPeriodID = SessionHelper.CurrentReconciliationPeriodID;
        _CurrentUserRole = SessionHelper.CurrentRoleID;
    }

    private void SetCapabilityInfo()
    {
        this._IsDualReviewEnabled = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.DualLevelReview);
        this._IsCertificationEnabled = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.CertificationActivation);
        this._IsCEOCFOCertificationEnabled = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.CEOCFOCertification);
        this._IsMandatoryReportSignoffEnabled = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.MandatoryReportSignoff);

        //List<CompanyCapabilityInfo> oCompanyCapabilityInfoCollection = SessionHelper.GetCompanyCapabilityCollectionForCurrentRecPeriod();

        //foreach (CompanyCapabilityInfo oCompanyCapabilityInfo in oCompanyCapabilityInfoCollection)
        //{
        //    if (oCompanyCapabilityInfo.CapabilityID.HasValue)
        //    {
        //        ARTEnums.Capability oCapability = (ARTEnums.Capability)Enum.Parse(typeof(ARTEnums.Capability), oCompanyCapabilityInfo.CapabilityID.Value.ToString());

        //        switch (oCapability)
        //        {
        //            case ARTEnums.Capability.DualLevelReview:
        //                if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
        //                {
        //                    this._IsDualReviewEnabled = true;
        //                }
        //                break;
        //            case ARTEnums.Capability.CertificationActivation:
        //                if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
        //                {
        //                     = true;
        //                }
        //                break;
        //            case ARTEnums.Capability.CEO_CFOCertificationActivation:
        //                if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
        //                {
        //                     = true;
        //                }
        //                break;
        //            case ARTEnums.Capability.MandatoryReportSignoff:
        //                if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
        //                {
        //                     = true;
        //                }
        //                break;
        //        }
        //    }
        //}
    }



    protected void rgStatus_ItemCreated(object sender, GridItemEventArgs e)
    {
        // If DualReviewEnabled is false hide the expand\collapse button in Preparer Table View (lavel2)
        if (e.Item is GridDataItem)
        {
            if (!_IsDualReviewEnabled && e.Item.OwnerTableView.Name.Equals("lavel2"))
            {
                TableCell cell = (TableCell)(e.Item as GridDataItem)["ExpandColumn"];
                cell.Controls[0].Visible = false;
            }

        }
        GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
    }
    protected void rgStatus_OnItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
        {
            isExportPDF = true;
            //rgCompanyList.MasterTableView.Columns.FindByUniqueName("IconColumn").Visible = false;
            GridHelper.ExportGridToPDF(rgStatus, 1464);

        }
        if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
        {
            isExportExcel = true;
            //rgCompanyList.MasterTableView.Columns.FindByUniqueName("IconColumn").Visible = false;
            GridHelper.ExportGridToExcel(rgStatus, 1464);

        }

    }


    protected void rgOtherUsers_ItemCreated(object sender, GridItemEventArgs e)
    {
        GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
    }

    protected void rgOtherUsers_OnItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
        {
            isExportPDF = true;
            //rgCompanyList.MasterTableView.Columns.FindByUniqueName("IconColumn").Visible = false;
            GridHelper.ExportGridToPDF(rgOtherUsers, 1464);

        }
        if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
        {
            isExportExcel = true;
            //rgCompanyList.MasterTableView.Columns.FindByUniqueName("IconColumn").Visible = false;
            GridHelper.ExportGridToExcel(rgOtherUsers, 1464);

        }

    }

    protected void rgOtherUsers_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            CertificationSignOffInfo oCertificationSignOffInfo = (CertificationSignOffInfo)e.Item.DataItem;

            ExHyperLink hlCertificationBalancesStatus = (ExHyperLink)e.Item.FindControl("hlCertificationBalancesStatus");
            ExHyperLink hlDateCertificationBalances = (ExHyperLink)e.Item.FindControl("hlDateCertificationBalances");
            ExHyperLink hlCertificationExceptionStatus = (ExHyperLink)e.Item.FindControl("hlCertificationExceptionStatus");
            ExHyperLink hlDateCertificationException = (ExHyperLink)e.Item.FindControl("hlDateCertificationException");
            ExHyperLink hlCertificationStatus = (ExHyperLink)e.Item.FindControl("hlCertificationStatus");
            ExHyperLink hlDateCertification = (ExHyperLink)e.Item.FindControl("hlDateCertification");
            ExLabel lblUserName = (ExLabel)e.Item.FindControl("lblUserName");
            ExLabel lblUserRole = (ExLabel)e.Item.FindControl("lblUserRole");
            ExImage imgNoAccess = (ExImage)e.Item.FindControl("imgNoAccess");
            ExImage imgSharedAccess = (ExImage)e.Item.FindControl("imgSharedAccess");


            SetYesNoCodeBasedOnSignOffDate(oCertificationSignOffInfo.CertificationBalancesDate, hlCertificationBalancesStatus, hlDateCertificationBalances);
            SetYesNoCodeBasedOnSignOffDate(oCertificationSignOffInfo.ExceptionCertificationDate, hlCertificationExceptionStatus, hlDateCertificationException);
            SetYesNoCodeBasedOnSignOffDate(oCertificationSignOffInfo.AccountCertificationDate, hlCertificationStatus, hlDateCertification);
            if (lblUserName != null)
            {
                lblUserName.Text = Helper.GetDisplayStringValue(oCertificationSignOffInfo.Name);
            }

            if (lblUserRole != null)
            {
                if (oCertificationSignOffInfo.RoleID != null)
                {
                    lblUserRole.Text = Helper.GetRoleName(oCertificationSignOffInfo.RoleID);
                }
            }

            if (oCertificationSignOffInfo.IsSameAccess.HasValue && !oCertificationSignOffInfo.IsSameAccess.Value)
            {
                imgNoAccess.Visible = true;
                imgSharedAccess.Visible = false;
            }
            else if (!oCertificationSignOffInfo.IsSameAccess.HasValue || (oCertificationSignOffInfo.IsSameAccess.HasValue && oCertificationSignOffInfo.IsSameAccess.Value))
            {
                short? roleID = oCertificationSignOffInfo.RoleID;
                string urlCertificationBalances = "CertificationBalances.aspx?" + QueryStringConstants.User_ID + "=" + oCertificationSignOffInfo.UserID + "&" + QueryStringConstants.ROLE_ID + "=" + roleID;//+ recPeriodID 
                string urlCertificationException = "CertificationException.aspx?" + QueryStringConstants.User_ID + "=" + oCertificationSignOffInfo.UserID + "&" + QueryStringConstants.ROLE_ID + "=" + roleID;//+ recPeriodID 
                string urlCertificationSignOff = "CertificationSignOff.aspx?" + QueryStringConstants.User_ID + "=" + oCertificationSignOffInfo.UserID + "&" + QueryStringConstants.ROLE_ID + "=" + roleID;//+ recPeriodID 

                hlCertificationBalancesStatus.NavigateUrl = urlCertificationBalances;
                hlDateCertificationBalances.NavigateUrl = urlCertificationBalances;

                hlCertificationExceptionStatus.NavigateUrl = urlCertificationException;
                hlDateCertificationException.NavigateUrl = urlCertificationException;

                hlCertificationStatus.NavigateUrl = urlCertificationSignOff;
                hlDateCertification.NavigateUrl = urlCertificationSignOff;
                if (!oCertificationSignOffInfo.IsSameAccess.HasValue)//Shared access
                {
                    imgNoAccess.Visible = false;
                    imgSharedAccess.Visible = true;
                }
                else //Complete access
                {
                    imgNoAccess.Visible = false;
                    imgSharedAccess.Visible = false;
                }
            }

        }
    }


    private void FillJuniorOtherThanPRAGridStatus()
    {

        if (_CurrentUserRole == (short)WebEnums.UserRole.CEO_CFO || _CurrentUserRole == (short)WebEnums.UserRole.CONTROLLER)
        {

            if (_CurrentUserRole == (short)WebEnums.UserRole.CEO_CFO)
            {
                rgOtherUsers.MasterTableView.DataSource = LoadUserOtherThanPRAData(SessionHelper.CurrentUserID, WebEnums.UserRole.CEO_CFO);


            }

            else if (_CurrentUserRole == (short)WebEnums.UserRole.CONTROLLER)
            {
                rgOtherUsers.MasterTableView.DataSource = LoadUserOtherThanPRAData(SessionHelper.CurrentUserID, WebEnums.UserRole.CONTROLLER);

            }

            GridColumn oGridColumn1 = rgOtherUsers.MasterTableView.Columns.FindByUniqueNameSafe("UserName");
            if (oGridColumn1 != null)
            {
                oGridColumn1.HeaderText = LanguageUtil.GetValue(1003);
            }

            GridColumn oGridColumn2 = rgOtherUsers.MasterTableView.Columns.FindByUniqueNameSafe("UserRole");
            if (oGridColumn2 != null)
            {
                oGridColumn2.HeaderText = LanguageUtil.GetValue(1278);
            }


            rgOtherUsers.DataBind();
        }
        else
            lblStatusLinkHeading.Visible = false;
    }


    private List<CertificationSignOffInfo> LoadUserOtherThanPRAData(int? userID, WebEnums.UserRole eUserRole)
    {
        ICertification oCertificationClient = RemotingHelper.GetCertificationObject();
        List<CertificationSignOffInfo> oCertificationSignOffInfoCollection = oCertificationClient.GetCertificationSignOffForJuniorsOfControllerAndCEOCFO(SessionHelper.CurrentReconciliationPeriodID, userID, (short)eUserRole, SessionHelper.CurrentCompanyID, Helper.GetAppUserInfo());

        return oCertificationSignOffInfoCollection;
    }

}//end of class
