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
using SkyStem.Library.Controls.TelerikWebControls;

public partial class Pages_Reports_TaskCompletionReport : PageBaseReport
{
    short? _reportID = 0;
    private int? _ReconciliationPeriodID = null;
    Dictionary<string, string> _oCriteriaCollection = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.RegisterPostBackToControls(this, ucSkyStemARTGrid.Grid);
        Helper.SetPageTitle(this, 2704);
        ucSkyStemARTGrid.BasePageTitle = 2704;
        _reportID = (short)WebEnums.Reports.TASK_COMPLETION_REPORT;
        string reportType = Request.QueryString[QueryStringConstants.REPORT_TYPE];
        if (!IsPostBack)
        {
            // Set default Sorting
            GridHelper.SetSortExpression(ucSkyStemARTGrid.Grid.MasterTableView, "TaskName");
        }
        if (!string.IsNullOrEmpty(reportType) && Convert.ToInt16(reportType) == (short)WebEnums.ReportType.ArchivedReport)
        {
            SetGridProperties();
            _oCriteriaCollection = (Dictionary<string, string>)Session[SessionConstants.REPORT_CRITERIA];
            ReportArchiveInfo oRptArchiveInfo = (ReportArchiveInfo)Session[SessionConstants.REPORT_ARCHIVED_DATA];
            List<TaskCompletionReportInfo> oTaskCompletionReportInfoList = ReportHelper.GetBinaryDeSerializedReportData(oRptArchiveInfo.ReportData) as List<TaskCompletionReportInfo>;
            if (oTaskCompletionReportInfoList != null)
            {
                Session[SessionConstants.REPORT_DATA_TASK_COMPLETION_REPORT] = oTaskCompletionReportInfoList;
                if (!IsPostBack)
                {
                    GridHelper.ShowHideColumnsBasedOnFeatureCapability(ucSkyStemARTGrid.Grid.CreateTableView());
                    ucSkyStemARTGrid.DataSource = oTaskCompletionReportInfoList;
                    ucSkyStemARTGrid.BindGrid();
                    if (oTaskCompletionReportInfoList.Count > 0 && oTaskCompletionReportInfoList[0].TaskTypeID.HasValue)
                    {
                        short taskType = Convert.ToInt16(oTaskCompletionReportInfoList[0].TaskTypeID.Value);
                        string SelectedDisplaycolumn = ReportHelper.GetCriteriaForCriteriaKey(_oCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_DISPLAYCOLUMN);
                        ReportHelper.ShowHideGridColumns(ucSkyStemARTGrid.Grid, _reportID.Value, SelectedDisplaycolumn, taskType);
                    }
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
                    SessionHelper.ClearSession(SessionConstants.REPORT_DATA_TASK_COMPLETION_REPORT);
                    GetGridData();
                    ucSkyStemARTGrid.BindGrid();
                    string SelectedDisplaycolumn = ReportHelper.GetCriteriaForCriteriaKey(_oCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_DISPLAYCOLUMN);
                    short TaskTypeID;
                    TaskTypeID = Convert.ToInt16(ReportHelper.GetCriteriaForCriteriaKey(_oCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TASKTYPE));
                    ReportHelper.ShowHideGridColumns(ucSkyStemARTGrid.Grid, _reportID.Value, SelectedDisplaycolumn, TaskTypeID);
                }
            }
        }
        // Hide  FSCaption and AccountType
        List<string> ColumnNameList = new List<string>();
        ColumnNameList.Add("FSCaption");
        ColumnNameList.Add("AccountType");
        GridHelper.HideColumns(ucSkyStemARTGrid.Grid.Columns, ColumnNameList);

    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        int? ReportRoleMandatoryReportID = null;
        ReportRoleMandatoryReportID = Convert.ToInt32(Request.QueryString[QueryStringConstants.MANDATORY_REPORT_ID]);
        if (ReportRoleMandatoryReportID != null && ReportRoleMandatoryReportID > 0)
            Helper.SetBreadcrumbs(this, 1072, 1016, ucSkyStemARTGrid.BasePageTitle);
    }
    private List<TaskCompletionReportInfo> GetGridData()
    {
        List<TaskCompletionReportInfo> oTaskCompletionReportInfoList = null;
        try
        {
            oTaskCompletionReportInfoList = (List<TaskCompletionReportInfo>)HttpContext.Current.Session[SessionConstants.REPORT_DATA_TASK_COMPLETION_REPORT];
            if (oTaskCompletionReportInfoList == null || oTaskCompletionReportInfoList.Count == 0)
            {
                ReportSearchCriteria oReportSearchCriteria = this.GetNormalSearchCriteria();
                DataTable dtEntity = ReportHelper.GetEntitySearchCriteria(_oCriteriaCollection);
                DataTable dtUser = ReportHelper.GetUserSearchCriteria(_oCriteriaCollection);
                DataTable dtRole = ReportHelper.GetRoleSearchCriteria(_oCriteriaCollection);
                IReport oReportClient = RemotingHelper.GetReportObject();
                FilterCriteria oFilterCriteria;
                List<FilterCriteria> oTaskFilterCriteriaCollection = new List<FilterCriteria>();
                //string SelectedDisplaycolumn = ReportHelper.GetCriteriaForCriteriaKey(_oCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_DISPLAYCOLUMN);

                string TaskStatusFilterValue = ReportHelper.GetCriteriaForCriteriaKey(_oCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TASKSTATUS);

                if (!string.IsNullOrEmpty(TaskStatusFilterValue))
                {
                    oFilterCriteria = new FilterCriteria();
                    oFilterCriteria.OperatorID = 9;
                    oFilterCriteria.ParameterID = 52;
                    oFilterCriteria.Value = TaskStatusFilterValue;
                    oTaskFilterCriteriaCollection.Add(oFilterCriteria);
                }
                string TaskListFilterValue = ReportHelper.GetCriteriaForCriteriaKey(_oCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TASKLISTNAME);
                if (!string.IsNullOrEmpty(TaskListFilterValue))
                {
                    oFilterCriteria = new FilterCriteria();
                    oFilterCriteria.OperatorID = 9;
                    oFilterCriteria.ParameterID = 37;
                    oFilterCriteria.Value = TaskListFilterValue;
                    oTaskFilterCriteriaCollection.Add(oFilterCriteria);
                }
                short taskType;
                taskType = Convert.ToInt16(ReportHelper.GetCriteriaForCriteriaKey(_oCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TASKTYPE));
                oTaskCompletionReportInfoList = oReportClient.GetReportTaskCompletionReport(taskType, oReportSearchCriteria, dtEntity, dtUser, dtRole, oTaskFilterCriteriaCollection, LanguageUtil.GetValue(2689), Helper.GetAppUserInfo());
                oTaskCompletionReportInfoList = LanguageHelper.TranslateLabelsTaskCompletionReport(oTaskCompletionReportInfoList);
                Session[SessionConstants.REPORT_DATA_TASK_COMPLETION_REPORT] = oTaskCompletionReportInfoList;
                ucSkyStemARTGrid.DataSource = oTaskCompletionReportInfoList;
                //ucSkyStemARTGrid.BindGrid();
                //ReportHelper.ShowHideGridColumns(ucSkyStemARTGrid.Grid, _reportID.Value, SelectedDisplaycolumn, taskType);
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

        return oTaskCompletionReportInfoList;
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
        ucSkyStemARTGrid.Grid.EntityNameLabelID = 2704;
    }
    protected void ucSkyStemARTGrid_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        ReportHelper.ItemDataBoundTaskCompletionReport(e, ucSkyStemARTGrid.Grid);
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
                case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_RECSTATUS:
                    oReportSearchCriteria.ReconciliationStatusIDs = keyValuePair.Value;
                    break;
            }
        }
        ReportHelper.SetParameterValueForRequesterUserAndLanguage(oReportSearchCriteria);
        oReportSearchCriteria.CompanyID = SessionHelper.CurrentCompanyID;
        oReportSearchCriteria.ExcludeNetAccount = true;
        oReportSearchCriteria.IsRequesterUserIDToBeConsideredForPermission = true;
        return oReportSearchCriteria;
    }
    public override string GetMenuKey()
    {
        return "";
    }

}
