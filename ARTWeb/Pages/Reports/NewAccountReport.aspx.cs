using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Model.Report;
using System.Data;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Exception;

public partial class Pages_Reports_NewAccountReport : PageBaseReport
{
    
    private int? _ReconciliationPeriodID = null;
    Dictionary<string, string> _oCriteriaCollection = null;

    protected void Page_Load(object sender, EventArgs e)
    {

        Helper.RegisterPostBackToControls(this, ucSkyStemARTGrid.Grid);

        Helper.SetPageTitle(this, 2690);
        ucSkyStemARTGrid.BasePageTitle = 2690;
        //if (!Page.IsPostBack)
        //{
        string reportType = Request.QueryString[QueryStringConstants.REPORT_TYPE];

        if (!IsPostBack)
        {
            // Set default Sorting
            GridHelper.SetSortExpression(ucSkyStemARTGrid.Grid.MasterTableView, "DateUploaded", Telerik.Web.UI.GridSortOrder.Descending);
        }

        if (!string.IsNullOrEmpty(reportType) && Convert.ToInt16(reportType) == (short)WebEnums.ReportType.ArchivedReport)
        {
            SetGridProperties();
            ReportArchiveInfo oRptArchiveInfo = (ReportArchiveInfo)Session[SessionConstants.REPORT_ARCHIVED_DATA];
            List<NewAccountReportInfo> oNewAccountReportInfoCollection = ReportHelper.GetBinaryDeSerializedReportData(oRptArchiveInfo.ReportData) as List<NewAccountReportInfo>;
            if (oNewAccountReportInfoCollection != null)
            {
                Session[SessionConstants.REPORT_DATA_NEW_ACCOUNT] = oNewAccountReportInfoCollection;
                if (!IsPostBack)
                {
                    GridHelper.ShowHideColumnsBasedOnFeatureCapability(ucSkyStemARTGrid.Grid.CreateTableView());
                    ucSkyStemARTGrid.DataSource = oNewAccountReportInfoCollection;
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

                if (!IsPostBack)
                {
                    SessionHelper.ClearSession(SessionConstants.REPORT_DATA_NEW_ACCOUNT);
                    GetGridData();
                    ucSkyStemARTGrid.BindGrid();
                }
            }
        }
        //}
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {

        int? ReportRoleMandatoryReportID = null;
        ReportRoleMandatoryReportID = Convert.ToInt32(Request.QueryString[QueryStringConstants.MANDATORY_REPORT_ID]);
        if (ReportRoleMandatoryReportID != null && ReportRoleMandatoryReportID > 0)
            Helper.SetBreadcrumbs(this, 1072, 1016, ucSkyStemARTGrid.BasePageTitle);
    }

    private List<NewAccountReportInfo> GetGridData()
    {
        List<NewAccountReportInfo> oNewAccountReportInfoCollection = null;
        try
        {
            oNewAccountReportInfoCollection = (List<NewAccountReportInfo>)HttpContext.Current.Session[SessionConstants.REPORT_DATA_NEW_ACCOUNT];

            if (oNewAccountReportInfoCollection == null || oNewAccountReportInfoCollection.Count == 0)
            {
                ReportSearchCriteria oReportSearchCriteria = this.GetNormalSearchCriteria();
                DataTable dtEntity = ReportHelper.GetEntitySearchCriteria(_oCriteriaCollection);
                IReport oReportClient = RemotingHelper.GetReportObject();
                oNewAccountReportInfoCollection = oReportClient.GetReportNewAccountReport(oReportSearchCriteria, dtEntity, Helper.GetAppUserInfo());
                oNewAccountReportInfoCollection = LanguageHelper.TranslateLabelsNewAccountReport(oNewAccountReportInfoCollection);
                Session[SessionConstants.REPORT_DATA_NEW_ACCOUNT] = oNewAccountReportInfoCollection;

                GridHelper.ShowHideColumnsBasedOnFeatureCapability(ucSkyStemARTGrid.Grid.CreateTableView());

                ucSkyStemARTGrid.DataSource = oNewAccountReportInfoCollection;
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

        return oNewAccountReportInfoCollection;
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
        //ucSkyStemARTGrid.ShowFSCaptionAndAccountType();
        ucSkyStemARTGrid.ShowSelectCheckBoxColum = false;
        ucSkyStemARTGrid.CompanyID = SessionHelper.CurrentCompanyID.Value;
        ucSkyStemARTGrid.Grid.EntityNameLabelID = 2690;
    }


    protected void ucSkyStemARTGrid_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        ReportHelper.ItemDataBoundNewAccountReport(e);
    }


    private ReportSearchCriteria GetNormalSearchCriteria()
    {
        ReportSearchCriteria oReportSearchCriteria = new ReportSearchCriteria();
        foreach (KeyValuePair<string, string> keyValuePair in _oCriteriaCollection)
        {
            switch (keyValuePair.Key)
            {
                case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_RECPERIOD:
                    int recPeriod = Convert.ToInt32(keyValuePair.Value);
                    if (recPeriod > 0)
                        oReportSearchCriteria.ReconciliationPeriodID = recPeriod;
                    break;
                case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_FROMDATECREATED:
                    DateTime dtFrom;
                    DateTime.TryParse(keyValuePair.Value, out dtFrom);
                    if (dtFrom != DateTime.MinValue)
                        oReportSearchCriteria.FromDateCreated = dtFrom;
                    break;
                case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TODATECREATED:
                    DateTime dtTo;
                    DateTime.TryParse(keyValuePair.Value, out dtTo);
                    if (dtTo != DateTime.MinValue)
                        oReportSearchCriteria.ToDateCreated = dtTo;
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
            //oReportSearchCriteria.IsZeroBalanceAccountEnabled = this._IsZeroBalanceEnabled;

            oReportSearchCriteria.ExcludeNetAccount = true;
            oReportSearchCriteria.IsRequesterUserIDToBeConsideredForPermission = false;
        }
        return oReportSearchCriteria;
    }


    public override string GetMenuKey()
    {
        return "";
    }
}
