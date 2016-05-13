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

public partial class Pages_Reports_TaskCompletionReportPrint : PageBaseReport
{
    short? _reportID = 0;
    Dictionary<string, string> _oCriteriaCollection = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        ucSkyStemARTGrid.BasePageTitle = 2704;
        _reportID = (short)WebEnums.Reports.TASK_COMPLETION_REPORT;

        ucSkyStemARTGrid.CompanyID = SessionHelper.CurrentCompanyID.Value;
        GridHelper.ShowHideColumnsBasedOnFeatureCapability(ucSkyStemARTGrid.Grid.CreateTableView());
        ucSkyStemARTGrid.DataSource = GetGridData();
        ucSkyStemARTGrid.BindGrid();
        _oCriteriaCollection = (Dictionary<string, string>)Session[SessionConstants.REPORT_CRITERIA];
        short taskType;
        taskType = Convert.ToInt16(ReportHelper.GetCriteriaForCriteriaKey(_oCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TASKTYPE));
        string SelectedDisplaycolumn = ReportHelper.GetCriteriaForCriteriaKey(_oCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_DISPLAYCOLUMN);
        ReportHelper.ShowHideGridColumns(ucSkyStemARTGrid.Grid, _reportID.Value, SelectedDisplaycolumn, taskType);

    }
    private List<TaskCompletionReportInfo> GetGridData()
    {
        List<TaskCompletionReportInfo> oTaskCompletionReportInfoList = null;
        oTaskCompletionReportInfoList = (List<TaskCompletionReportInfo>)HttpContext.Current.Session[SessionConstants.REPORT_DATA_TASK_COMPLETION_REPORT];
        return oTaskCompletionReportInfoList;
    }
    protected void ucSkyStemARTGrid_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        ReportHelper.ItemDataBoundTaskCompletionReport(e, ucSkyStemARTGrid.Grid);
    }

}
