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
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.Client.Data;
using SkyStem.Library.Controls.WebControls;
using Telerik.Web.UI;
using SkyStem.ART.Client.Model.Report;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Exception;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using SkyStem.Library.Controls.TelerikWebControls;

public partial class Pages_Reports_AccountOwnershipReport : PageBaseReport
{
    bool _isExportPDF;
    bool _isExportExcel;
    short? _reportID = 0;
    private int? _ReconciliationPeriodID = null;
    Dictionary<string, string> _oCriteriaCollection = null;
    short? dSumTotalAccountAssigned = 0;
    short? dSumCountHighAccounts = 0;
    short? dSumCountMediumAccounts = 0;
    short? dSumCountLowAccounts = 0;
    short? dSumCountKeyAccounts = 0;
    short? dSumCountMaterialAccount = 0;

    #region Properties
    public string TotalReconcilableAccountsValue
    {
        set { lblTotalRecAccounts.Text = value; }
        get
        {
            return lblTotalRecAccounts.Text;

        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        Helper.RegisterPostBackToControls(this, rgReport);
        if (!Page.IsPostBack)
        {
            _isExportPDF = false;
            _isExportExcel = false;
        }
        Helper.SetPageTitle(this, 1119);
        ReportMstInfo oReportInfo = (ReportMstInfo)Session[SessionConstants.REPORT_INFO_OBJECT];
        if (oReportInfo != null && oReportInfo.ReportID.HasValue)
        {
            _reportID = oReportInfo.ReportID;
        }

        if (!IsPostBack)
        {
            // Set default Sorting
            GridHelper.SetSortExpression(rgReport.MasterTableView, "Name");
            //SetTextForTotalLabels();

        }

        if (Session[SessionConstants.REPORT_CRITERIA] != null)
        {
            string reportType = Request.QueryString[QueryStringConstants.REPORT_TYPE];

            if (!string.IsNullOrEmpty(reportType) && Convert.ToInt16(reportType) == (short)WebEnums.ReportType.ArchivedReport)
            {
                ReportArchiveInfo oRptArchiveInfo = (ReportArchiveInfo)Session[SessionConstants.REPORT_ARCHIVED_DATA];
                //_ReconciliationPeriodID = oRptArchiveInfo.ReconciliationPeriodID;
                _oCriteriaCollection = (Dictionary<string, string>)Session[SessionConstants.REPORT_CRITERIA];
                _ReconciliationPeriodID = Convert.ToInt32(_oCriteriaCollection[ReportCriteriaKeyName.RPTCRITERIAKEYNAME_RECPERIOD]);
                List<AccountOwnershipReportInfo> oAccountOwnershipReportInfoCollection = ReportHelper.GetBinaryDeSerializedReportData(oRptArchiveInfo.ReportData) as List<AccountOwnershipReportInfo>;
                if (oAccountOwnershipReportInfoCollection != null)
                {
                    Session[SessionConstants.REPORT_DATA_ACCOUNT_OWNERSHIP] = oAccountOwnershipReportInfoCollection;
                }
            }
            else
            {
                _oCriteriaCollection = (Dictionary<string, string>)Session[SessionConstants.REPORT_CRITERIA];
                _ReconciliationPeriodID = Convert.ToInt32(_oCriteriaCollection[ReportCriteriaKeyName.RPTCRITERIAKEYNAME_RECPERIOD]);
                //if (Request.QueryString[QueryStringConstants.REPORT_ID] != null)
                //    _reportID = Convert.ToInt16(Request.QueryString[QueryStringConstants.REPORT_ID]);
                if (!IsPostBack)
                {
                    SessionHelper.ClearSession(SessionConstants.REPORT_DATA_ACCOUNT_OWNERSHIP);
                    GridHelper.ShowHideColumnsBasedOnFeatureCapability(rgReport.MasterTableView);
                    //ShowHideLabelsForTotalBasedOnCapability();
                    RunReport();
                }
            }
        }

    }

    #region private methods related to labels showing totals (same on main and peint page of the report)

    //private void SetValueForTotalLabel(List<AccountOwnershipReportInfo> oAccountOwnershipReportInfoCollection)
    //{
    //    if (oAccountOwnershipReportInfoCollection != null)
    //    {
    //        lblTotalTotalNoOfAccountsValue.Text = oAccountOwnershipReportInfoCollection.Where(r => r != null).Sum(r => r.CountTotalAccountAssigned).ToString();
    //        lblTotalKeyValue.Text = oAccountOwnershipReportInfoCollection.Where(r => r != null).Sum(r => r.CountKeyAccounts).ToString();
    //        lblTotalHighValue.Text = oAccountOwnershipReportInfoCollection.Where(r => r != null).Sum(r => r.CountHighAccounts).ToString();
    //        lblTotalMediumValue.Text = oAccountOwnershipReportInfoCollection.Where(r => r != null).Sum(r => r.CountMediumAccounts).ToString();
    //        lblTotalLowValue.Text = oAccountOwnershipReportInfoCollection.Where(r => r != null).Sum(r => r.CountLowAccounts).ToString();
    //        lblTotalMaterialValue.Text = oAccountOwnershipReportInfoCollection.Where(r => r != null).Sum(r => r.CountMaterialAccounts).ToString();
    //    }



    //}



    //private void SetTextForTotalLabels()
    //{
    //    lblTotalTotalNoOfAccounts.Text = GetTextForTotalLabels(1881);
    //    lblTotalKey.Text = GetTextForTotalLabels(1883);
    //    lblTotalHigh.Text = GetTextForTotalLabels(1127);
    //    lblTotalMedium.Text = GetTextForTotalLabels(1128);
    //    lblTotalLow.Text = GetTextForTotalLabels(1129);
    //    lblTotalMaterial.Text = GetTextForTotalLabels(1433);
    //}
    //private void ShowHideLabelsForTotalBasedOnCapability()
    //{
    //    lblTotalKeyValue.Visible = lblTotalKey.Visible = Helper.IsCapabilityActivatedForRecPeriodID(ARTEnums.Capability.KeyAccount , _ReconciliationPeriodID.Value, false);
    //    lblTotalHighValue.Visible = lblTotalHigh.Visible = Helper.IsCapabilityActivatedForRecPeriodID(ARTEnums.Capability.RiskRating, _ReconciliationPeriodID.Value, false);
    //    lblTotalMediumValue.Visible = lblTotalMedium.Visible = Helper.IsCapabilityActivatedForRecPeriodID(ARTEnums.Capability.RiskRating, _ReconciliationPeriodID.Value, false);
    //    lblTotalLowValue.Visible = lblTotalLow.Visible = Helper.IsCapabilityActivatedForRecPeriodID(ARTEnums.Capability.RiskRating, _ReconciliationPeriodID.Value, false);
    //    lblTotalMaterialValue.Visible = lblTotalMaterial.Visible = Helper.IsCapabilityActivatedForRecPeriodID(ARTEnums.Capability.MaterialitySelection, _ReconciliationPeriodID.Value, false);
    //}

    private string GetTextForTotalLabels(int labelID)
    {
        string message = string.Format(LanguageUtil.GetValue(1981) + ": ", LanguageUtil.GetValue(labelID));
        return message;
    }

    #endregion

    protected void rgReport_NeedDataSourceEventHandler(object sender, GridNeedDataSourceEventArgs e)
    {
        List<AccountOwnershipReportInfo> oAccountOwnershipReportInfoCollection = null;
        try
        {
            oAccountOwnershipReportInfoCollection = (List<AccountOwnershipReportInfo>)HttpContext.Current.Session[SessionConstants.REPORT_DATA_ACCOUNT_OWNERSHIP];
            if (oAccountOwnershipReportInfoCollection == null || oAccountOwnershipReportInfoCollection.Count == 0)
            {
                ReportSearchCriteria oReportSearchCriteria = this.GetNormalSearchCriteria();
                //DataTable dtEntity = ReportHelper.GetEntitySearchCriteria(_oCriteriaCollection);
                DataTable dtUser = ReportHelper.GetUserSearchCriteria(_oCriteriaCollection);
                DataTable dtRole = ReportHelper.GetRoleSearchCriteria(_oCriteriaCollection);

                //when no user is selected 
                ReportHelper.SendUserSearchCriteriaWhenNoUserSelected(_reportID.Value, ref dtUser, ref dtRole);

                IReport oReportClient = RemotingHelper.GetReportObject();
                oAccountOwnershipReportInfoCollection = oReportClient.GetReportAccountOwnershipReport(oReportSearchCriteria, dtUser, dtRole, Helper.GetAppUserInfo());
                oAccountOwnershipReportInfoCollection = LanguageHelper.TranslateLabelsAccountOwnershipReport(oAccountOwnershipReportInfoCollection);
                Session[SessionConstants.REPORT_DATA_ACCOUNT_OWNERSHIP] = oAccountOwnershipReportInfoCollection;
            }

            //08112010 - Set Total Reconcilable Accounts Value
            if (oAccountOwnershipReportInfoCollection != null
                && oAccountOwnershipReportInfoCollection.Count > 0
                && oAccountOwnershipReportInfoCollection[0] != null)
            {
                if (oAccountOwnershipReportInfoCollection[0].CountTotalAccounts != null)
                    this.TotalReconcilableAccountsValue = oAccountOwnershipReportInfoCollection[0].CountTotalAccounts.ToString();
            }

            GridHelper.ShowHideColumnsBasedOnFeatureCapability(rgReport.CreateTableView());
            //ShowHideLabelsForTotalBasedOnCapability();
            rgReport.DataSource = oAccountOwnershipReportInfoCollection;
            // Sort the Data
            //GridHelper.SortDataSource(rgReport.MasterTableView);
            //SetValueForTotalLabel(oAccountOwnershipReportInfoCollection);




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


    private void RunReport()
    {
        ReportHelper.SetReportRadGridProperty(rgReport);
        rgReport.EntityNameLabelID = 1119;

    }

    protected void rgReport_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {

        ReportHelper.ItemDataBoundAccountOwnershipReport(e, ref dSumTotalAccountAssigned,
            ref dSumCountHighAccounts, ref dSumCountMediumAccounts, ref dSumCountLowAccounts, ref dSumCountKeyAccounts,
            ref dSumCountMaterialAccount);
    }


    private ReportSearchCriteria GetNormalSearchCriteria()
    {
        ReportSearchCriteria oReportSearchCriteria = new ReportSearchCriteria();
        foreach (KeyValuePair<string, string> keyValuePair in _oCriteriaCollection)
        {
            switch (keyValuePair.Key)
            {
                case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_RECPERIOD:
                    oReportSearchCriteria.ReconciliationPeriodID = Convert.ToInt32(keyValuePair.Value);
                    break;
            }
        }


        oReportSearchCriteria.RequesterUserID = SessionHelper.CurrentUserID.Value;
        oReportSearchCriteria.RequesterRoleID = SessionHelper.CurrentRoleID.Value;

        return oReportSearchCriteria;
    }


    public override string GetMenuKey()
    {
        return "";
    }

    protected void rgReport_ItemCreated(object sender, GridItemEventArgs e)
    {
        GridHelper.SetStylesForExportGrid(e, _isExportPDF, _isExportExcel);
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {

        int? ReportRoleMandatoryReportID = null;
        ReportRoleMandatoryReportID = Convert.ToInt32(Request.QueryString[QueryStringConstants.MANDATORY_REPORT_ID]);
        if (ReportRoleMandatoryReportID != null && ReportRoleMandatoryReportID > 0)
            Helper.SetBreadcrumbs(this, 1072, 1016, rgReport.EntityNameLabelID);
    }

    protected void rgReport_OnItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == TelerikConstants .GridExportToPDFCommandName)
        {
            _isExportPDF = true;
            //rgCompanyList.MasterTableView.Columns.FindByUniqueName("IconColumn").Visible = false;
            GridHelper.ExportGridToPDF(rgReport, 1119);

        }
        if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
        {
            _isExportExcel = true;
            //rgCompanyList.MasterTableView.Columns.FindByUniqueName("IconColumn").Visible = false;
            GridHelper.ExportGridToExcel(rgReport, 1119);

        }

    }

}//end of class
