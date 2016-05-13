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
using SkyStem.ART.Client.Exception;
using SkyStem.Library.Controls.TelerikWebControls;

public partial class Pages_Reports_UnusualBalancesReport : PageBaseReport
{
    short? _reportID = 0;
    private int? _ReconciliationPeriodID = null;
    Dictionary<string, string> _oCriteriaCollection = null;

    protected void Page_Load(object sender, EventArgs e)
    {

        Helper.RegisterPostBackToControls(this, ucSkyStemARTGrid.Grid);

        Helper.SetPageTitle(this, 1107);
        ucSkyStemARTGrid.BasePageTitle = 1107;
        ReportMstInfo oReportInfo = (ReportMstInfo)Session[SessionConstants.REPORT_INFO_OBJECT];

        if (!IsPostBack)
        {
            // Set default Sorting
            GridHelper.SetSortExpression(ucSkyStemARTGrid.Grid.MasterTableView, "AccountName");
        }

        if (oReportInfo != null && oReportInfo.ReportID.HasValue)
        {
            _reportID = oReportInfo.ReportID;
        }
        //if (!Page.IsPostBack)
        //{
        string reportType = Request.QueryString[QueryStringConstants.REPORT_TYPE];

        if (!string.IsNullOrEmpty(reportType) && Convert.ToInt16(reportType) == (short)WebEnums.ReportType.ArchivedReport)
        {
            SetGridProperties();

            ReportArchiveInfo oRptArchiveInfo = (ReportArchiveInfo)Session[SessionConstants.REPORT_ARCHIVED_DATA];
            List<UnusualBalancesReportInfo> oUnusualBalancesReportInfoCollection = ReportHelper.GetBinaryDeSerializedReportData(oRptArchiveInfo.ReportData) as List<UnusualBalancesReportInfo>;
            if (oUnusualBalancesReportInfoCollection != null)
            {
                Session[SessionConstants.REPORT_DATA_UNUSUAL_BALANCE] = oUnusualBalancesReportInfoCollection;
                if (!IsPostBack)
                {
                    GridHelper.ShowHideColumnsBasedOnFeatureCapability(ucSkyStemARTGrid.Grid.CreateTableView());

                    ucSkyStemARTGrid.DataSource = oUnusualBalancesReportInfoCollection;
                    ucSkyStemARTGrid.BindGrid();
                }
            }
        }
        else
        {



            if (Session[SessionConstants.REPORT_CRITERIA] != null)
            {
                _oCriteriaCollection = (Dictionary<string, string>)Session[SessionConstants.REPORT_CRITERIA];
                _ReconciliationPeriodID = Convert.ToInt32(_oCriteriaCollection[ReportCriteriaKeyName.RPTCRITERIAKEYNAME_RECPERIOD]);
                SetGridProperties();
                //List<CompanyCapabilityInfo> oCompanyCapabilityInfoCollection = SessionHelper.GetCompanyCapabilityCollectionForCurrentRecPeriod();
                //SetCapabilityInfo(oCompanyCapabilityInfoCollection);
                if (!IsPostBack)
                {
                    SessionHelper.ClearSession(SessionConstants.REPORT_DATA_UNUSUAL_BALANCE);
                    GetGridData();
                    ucSkyStemARTGrid.BindGrid();
                }
            }
        }
        //}
    }


    private List<UnusualBalancesReportInfo> GetGridData()
    {
        List<UnusualBalancesReportInfo> oUnusualBalancesReportInfoCollection = null;
        try
        {
            oUnusualBalancesReportInfoCollection = (List<UnusualBalancesReportInfo>)HttpContext.Current.Session[SessionConstants.REPORT_DATA_UNUSUAL_BALANCE];

            if (oUnusualBalancesReportInfoCollection == null || oUnusualBalancesReportInfoCollection.Count == 0)
            {
                ReportSearchCriteria oReportSearchCriteria = this.GetNormalSearchCriteria();
                DataTable dtEntity = ReportHelper.GetEntitySearchCriteria(_oCriteriaCollection);
                IReport oReportClient = RemotingHelper.GetReportObject();
                oUnusualBalancesReportInfoCollection = oReportClient.GetReportUnusualBalancesReport(oReportSearchCriteria, dtEntity, Helper.GetAppUserInfo());
                oUnusualBalancesReportInfoCollection = LanguageHelper.TranslateLabelsUnusualBalancesReport(oUnusualBalancesReportInfoCollection);
                Session[SessionConstants.REPORT_DATA_UNUSUAL_BALANCE] = oUnusualBalancesReportInfoCollection;

                GridHelper.ShowHideColumnsBasedOnFeatureCapability(ucSkyStemARTGrid.Grid.CreateTableView());

                ucSkyStemARTGrid.DataSource = oUnusualBalancesReportInfoCollection;
                // Sort the Data
                //GridHelper.SortDataSource(ucSkyStemARTGrid.Grid.MasterTableView);
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

        return oUnusualBalancesReportInfoCollection;
    }

    protected object ucSkyStemARTGrid_NeedDataSourceEventHandler(int count)
    {
        return GetGridData();
    }


    private void SetGridProperties()
    {
        ucSkyStemARTGrid.Grid.AllowCustomPaging = false;
        ucSkyStemARTGrid.Grid.AllowPaging = true;
        ucSkyStemARTGrid.Grid.PagerStyle.AlwaysVisible = true;
        ucSkyStemARTGrid.ShowStatusImageColumn = false;
        ucSkyStemARTGrid.ShowFSCaptionAndAccountType();
        ucSkyStemARTGrid.ShowSelectCheckBoxColum = false;
        ucSkyStemARTGrid.CompanyID = SessionHelper.CurrentCompanyID.Value;
        ucSkyStemARTGrid.Grid.EntityNameLabelID = 1107;
        ucSkyStemARTGrid.Grid.AllowExportToExcel = true;
    }

    protected void ucSkyStemARTGrid_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        ReportHelper.ItemDataBoundUnusualBalancesReport(e);


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
                case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_FROMACCOUNT:
                    oReportSearchCriteria.FromAccountNumber = keyValuePair.Value;
                    break;
                case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TOACCOUNT:
                    oReportSearchCriteria.ToAccountNumber = keyValuePair.Value;
                    break;
                case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_ISMATERIALACCOUNT:
                    oReportSearchCriteria.IsMaterialAccount = ReportHelper.GetBoolValueFromKeyValue(keyValuePair.Value);
                    break;
                case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_RISKRATING:
                    oReportSearchCriteria.RiskRatingIDs = keyValuePair.Value;
                    break;
                case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_ISKEYACCOUNT:
                    oReportSearchCriteria.IsKeyccount = ReportHelper.GetBoolValueFromKeyValue(keyValuePair.Value);
                    break;
                case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_REASON:
                    oReportSearchCriteria.ReasonCodeIDs = keyValuePair.Value;
                    break;
            }
        }

        //Other fields required for sp
        {
            ReportHelper.SetParameterValueForRequesterUserAndLanguage(oReportSearchCriteria);

            oReportSearchCriteria.CompanyID = SessionHelper.CurrentCompanyID;
            //oReportSearchCriteria.IsDualReviewEnabled = this._IsDualReviewEnabled;
            //oReportSearchCriteria.IsKeyAccountEnabled = this._IsKeyAccountEnabled;
            //oReportSearchCriteria.IsRiskRatingEnabled = this._IsRiskRatingEnabled;
            oReportSearchCriteria.IsZeroBalanceAccountEnabled = Helper.IsCapabilityActivatedForRecPeriodID(ARTEnums.Capability.ZeroBalanceAccount, _ReconciliationPeriodID.Value, false); // this._IsZeroBalanceEnabled;

            oReportSearchCriteria.ExcludeNetAccount = true;
            oReportSearchCriteria.IsRequesterUserIDToBeConsideredForPermission = true;
        }
        return oReportSearchCriteria;
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {

        int? ReportRoleMandatoryReportID = null;
        ReportRoleMandatoryReportID = Convert.ToInt32(Request.QueryString[QueryStringConstants.MANDATORY_REPORT_ID]);
        if (ReportRoleMandatoryReportID != null && ReportRoleMandatoryReportID > 0)
            Helper.SetBreadcrumbs(this, 1072, 1016, ucSkyStemARTGrid.BasePageTitle);
    }


    public override string GetMenuKey()
    {
        return "";
    }
}
