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

public partial class Pages_Reports_IncompleteAccountAttributeReport : PageBaseReport
{
    
    private int? _ReconciliationPeriodID = null;
    Dictionary<string, string> _oCriteriaCollection = null;

    protected void Page_Load(object sender, EventArgs e)
    {

        Helper.RegisterPostBackToControls(this, ucSkyStemARTGrid.Grid);

        Helper.SetPageTitle(this, 1125);
        ucSkyStemARTGrid.BasePageTitle = 1125;
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
            List<IncompleteAccountAttributeReportInfo> oIncompleteAccountAttributeReportInfoCollection = ReportHelper.GetBinaryDeSerializedReportData(oRptArchiveInfo.ReportData) as List<IncompleteAccountAttributeReportInfo>;
            if (oIncompleteAccountAttributeReportInfoCollection != null)
            {
                Session[SessionConstants.REPORT_DATA_INCOMPLETE_ACCOUNT_ATTRIBUTE] = oIncompleteAccountAttributeReportInfoCollection;
                if (!IsPostBack)
                {
                    GridHelper.ShowHideColumnsBasedOnFeatureCapability(ucSkyStemARTGrid.Grid.CreateTableView());
                    ucSkyStemARTGrid.DataSource = oIncompleteAccountAttributeReportInfoCollection;
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
                    SessionHelper.ClearSession(SessionConstants.REPORT_DATA_INCOMPLETE_ACCOUNT_ATTRIBUTE);
                    GetGridData();
                    ucSkyStemARTGrid.BindGrid();
                }
            }
        }
        //}
    }

    private List<IncompleteAccountAttributeReportInfo> GetGridData()
    {
        List<IncompleteAccountAttributeReportInfo> oIncompleteAccountAttributeReportInfoCollection = null;
        try
        {
            oIncompleteAccountAttributeReportInfoCollection = (List<IncompleteAccountAttributeReportInfo>)HttpContext.Current.Session[SessionConstants.REPORT_DATA_INCOMPLETE_ACCOUNT_ATTRIBUTE];

            if (oIncompleteAccountAttributeReportInfoCollection == null || oIncompleteAccountAttributeReportInfoCollection.Count == 0)
            {
                ReportSearchCriteria oReportSearchCriteria = this.GetNormalSearchCriteria();
                DataTable dtEntity = ReportHelper.GetEntitySearchCriteria(_oCriteriaCollection);
                IReport oReportClient = RemotingHelper.GetReportObject();
                oIncompleteAccountAttributeReportInfoCollection = oReportClient.GetReportIncompleteAccountAttributeReport(oReportSearchCriteria, dtEntity, Helper.GetAppUserInfo());
                oIncompleteAccountAttributeReportInfoCollection = LanguageHelper.TranslateLabelsIncompleteAccountAttributeReport(oIncompleteAccountAttributeReportInfoCollection);
                Session[SessionConstants.REPORT_DATA_INCOMPLETE_ACCOUNT_ATTRIBUTE] = oIncompleteAccountAttributeReportInfoCollection;

                GridHelper.ShowHideColumnsBasedOnFeatureCapability(ucSkyStemARTGrid.Grid.CreateTableView());

                ucSkyStemARTGrid.DataSource = oIncompleteAccountAttributeReportInfoCollection;
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

        return oIncompleteAccountAttributeReportInfoCollection;
    }

    protected object ucSkyStemARTGrid_NeedDataSourceEventHandler(int count)
    {
        return GetGridData();
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {

        int? ReportRoleMandatoryReportID = null;
        ReportRoleMandatoryReportID = Convert.ToInt32(Request.QueryString[QueryStringConstants.MANDATORY_REPORT_ID]);
        if (ReportRoleMandatoryReportID != null && ReportRoleMandatoryReportID > 0)
            Helper.SetBreadcrumbs(this, 1072, 1016, ucSkyStemARTGrid.BasePageTitle);
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
        ucSkyStemARTGrid.Grid.EntityNameLabelID = 1125;
    }

    protected void ucSkyStemARTGrid_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {

        ReportHelper.ItemDataBoundIncompleteAccountAttributeReport(e);
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
            //oReportSearchCriteria.IsDualReviewEnabled = this._IsDualReviewEnabled;
            oReportSearchCriteria.IsKeyAccountEnabled = Helper.IsCapabilityActivatedForRecPeriodID(ARTEnums.Capability.KeyAccount, _ReconciliationPeriodID.Value, false);
            oReportSearchCriteria.IsRiskRatingEnabled = Helper.IsCapabilityActivatedForRecPeriodID(ARTEnums.Capability.RiskRating, _ReconciliationPeriodID.Value, false);
            oReportSearchCriteria.IsZeroBalanceAccountEnabled = Helper.IsCapabilityActivatedForRecPeriodID(ARTEnums.Capability.ZeroBalanceAccount, _ReconciliationPeriodID.Value, false);
            oReportSearchCriteria.IsDueDateByAccountEnabled = Helper.IsCapabilityActivatedForRecPeriodID(ARTEnums.Capability.DueDateByAccount, _ReconciliationPeriodID.Value, false);
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
