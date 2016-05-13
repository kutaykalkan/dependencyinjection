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

public partial class Pages_Reports_UnassignedAccountsReport : PageBaseReport
{
    
    private int? _ReconciliationPeriodID = null;
    Dictionary<string, string> _oCriteriaCollection = null;

   

    protected void Page_Load(object sender, EventArgs e)
    {

        Helper.RegisterPostBackToControls(this, ucSkyStemARTGrid.Grid);

        Helper.SetPageTitle(this, 1123);
        ucSkyStemARTGrid.BasePageTitle = 1123;
        //if (!Page.IsPostBack)
        //{
        string reportType = Request.QueryString[QueryStringConstants.REPORT_TYPE];

        if (!IsPostBack)
        {
            // Set default Sorting
            GridHelper.SetSortExpression(ucSkyStemARTGrid.Grid.MasterTableView, "AccountName");
        }

        if (!string.IsNullOrEmpty(reportType) && Convert.ToInt16(reportType) == (short)WebEnums.ReportType.ArchivedReport)
        {
            SetGridProperties();

            ReportArchiveInfo oRptArchiveInfo = (ReportArchiveInfo)Session[SessionConstants.REPORT_ARCHIVED_DATA];
            List<UnassignedAccountsReportInfo> oUnassignedAccountsReportInfoCollection = ReportHelper.GetBinaryDeSerializedReportData(oRptArchiveInfo.ReportData) as List<UnassignedAccountsReportInfo>;
            if (oUnassignedAccountsReportInfoCollection != null)
            {
                Session[SessionConstants.REPORT_DATA_UNASSIGNED_ACCOUNT] = oUnassignedAccountsReportInfoCollection;
                if (!IsPostBack)
                {
                    GridHelper.ShowHideColumnsBasedOnFeatureCapability(ucSkyStemARTGrid.Grid.CreateTableView());

                    ucSkyStemARTGrid.DataSource = oUnassignedAccountsReportInfoCollection;
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
                    SessionHelper.ClearSession(SessionConstants.REPORT_DATA_UNASSIGNED_ACCOUNT);
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

    private List<UnassignedAccountsReportInfo> GetGridData()
    {
        List<UnassignedAccountsReportInfo> oUnassignedAccountsReportInfoCollection = null;
        try
        {
            oUnassignedAccountsReportInfoCollection = (List<UnassignedAccountsReportInfo>)HttpContext.Current.Session[SessionConstants.REPORT_DATA_UNASSIGNED_ACCOUNT];

            if (oUnassignedAccountsReportInfoCollection == null || oUnassignedAccountsReportInfoCollection.Count == 0)
            {
                ReportSearchCriteria oReportSearchCriteria = this.GetNormalSearchCriteria();
                DataTable dtEntity = ReportHelper.GetEntitySearchCriteria(_oCriteriaCollection);
                IReport oReportClient = RemotingHelper.GetReportObject();
                oUnassignedAccountsReportInfoCollection = oReportClient.GetReportUnassignedAccountsReport(oReportSearchCriteria, dtEntity, Helper.GetAppUserInfo());
                oUnassignedAccountsReportInfoCollection = LanguageHelper.TranslateLabelsUnassignedAccountsReport(oUnassignedAccountsReportInfoCollection);
                Session[SessionConstants.REPORT_DATA_UNASSIGNED_ACCOUNT] = oUnassignedAccountsReportInfoCollection;

                GridHelper.ShowHideColumnsBasedOnFeatureCapability(ucSkyStemARTGrid.Grid.CreateTableView());

                ucSkyStemARTGrid.DataSource = oUnassignedAccountsReportInfoCollection;
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

        return oUnassignedAccountsReportInfoCollection;
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
        ucSkyStemARTGrid.Grid.EntityNameLabelID = 1123;
    }

    protected void ucSkyStemARTGrid_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {

        ReportHelper.ItemDataBoundUnassignedAccountsReport(e);

    }


    private ReportSearchCriteria GetNormalSearchCriteria()
    {
        ReportSearchCriteria oReportSearchCriteria = new ReportSearchCriteria();

        //From Session keyValuePair parameters
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
            }
        }

        //Other fields required for sp
        {
            ReportHelper.SetParameterValueForRequesterUserAndLanguage(oReportSearchCriteria);

            oReportSearchCriteria.CompanyID = SessionHelper.CurrentCompanyID;
            oReportSearchCriteria.IsDualReviewEnabled = Helper.IsCapabilityActivatedForRecPeriodID(ARTEnums.Capability.DualLevelReview, _ReconciliationPeriodID.Value, false);
            //oReportSearchCriteria.IsKeyAccountEnabled = this._IsKeyAccountEnabled;
            //oReportSearchCriteria.IsRiskRatingEnabled = this._IsRiskRatingEnabled;
            //oReportSearchCriteria.IsZeroBalanceAccountEnabled = this._IsZeroBalanceEnabled;

            oReportSearchCriteria.ExcludeNetAccount = true;
            oReportSearchCriteria.IsRequesterUserIDToBeConsideredForPermission = true;
        }

        return oReportSearchCriteria;
    }


    public override string GetMenuKey()
    {
        return "";
    }
}
