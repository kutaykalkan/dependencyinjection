using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Web.Utility;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.Data;
using System.Text;
using System.Threading.Tasks;

public partial class UserControls_Dashboard_TaskStatus : UserControlWebPartBase
{
    private const char _ARGUMENT_SEPARATOR = '^';
    protected void Page_Init(object sender, EventArgs e)
    {
        Page oPage = (Page)this.Parent.Page;
        MasterPageBase ompage = (MasterPageBase)oPage.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);
        chrtTaskStatus.Click += new ImageMapEventHandler(chrtTaskStatus_Click);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        //ConfigureTaskChartSettings();
    }

    public override IAsyncResult GetDataAsync()
    {
        int userID = Convert.ToInt32(SessionHelper.CurrentUserID);
        short roleID = Convert.ToInt16(SessionHelper.CurrentRoleID);
        int recPeriodID = Convert.ToInt32(SessionHelper.CurrentReconciliationPeriodID);
        return DashboardHelper.GetDataForTaskCompletionStatusCountAsync(SessionHelper.CurrentUserID, roleID, recPeriodID, Helper.GetAppUserInfo());
    }

    public override void DataLoaded(IAsyncResult result)
    {
        Task<List<TaskCompletionStatusMstInfo>> oTask = (Task<List<TaskCompletionStatusMstInfo>>)result;
        if (oTask.IsCompleted)
        {
            ConfigureTaskChartSettings(oTask.Result);
        }
    }
    void ConfigureTaskChartSettings()
    {
        int userID = Convert.ToInt32(SessionHelper.CurrentUserID);
        short roleID = Convert.ToInt16(SessionHelper.CurrentRoleID);
        int recPeriodID = Convert.ToInt32(SessionHelper.CurrentReconciliationPeriodID);
        List<TaskCompletionStatusMstInfo> oTaskStatusList = DashboardHelper.GetDataForTaskCompletionStatusCount(SessionHelper.CurrentUserID, roleID, recPeriodID, Helper.GetAppUserInfo());
    }

    void ConfigureTaskChartSettings(List<TaskCompletionStatusMstInfo> oTaskStatusList)
    {
        try
        {

            int? totalCount = 0;

            int? pendingCount = 0;
            int? pendingATCount = 0;
            int? pendingGTCount = 0;
            int? completedCount = 0;
            int? completedATCount = 0;
            int? completedGTCount = 0;
            int? overdueCount = 0;
            int? overdueATCount = 0;
            int? overdueGTCount = 0;

            string pendingColor = string.Empty;
            string completedColor = string.Empty;
            string overdueColor = string.Empty;

            if (oTaskStatusList != null)
            {
                TaskCompletionStatusMstInfo oTaskStatus = null;

                oTaskStatus = oTaskStatusList.FirstOrDefault(ts => ts.TaskCompletionStatusID == (short)ARTEnums.TaskCompletionStatus.Pending);

                if (oTaskStatus != null)
                {
                    pendingCount = oTaskStatus.CompletionStatusCount;
                    pendingATCount = oTaskStatus.ATCount;
                    pendingGTCount = oTaskStatus.GTCount;
                    pendingColor = oTaskStatus.StatusColor;
                }
                oTaskStatus = oTaskStatusList.FirstOrDefault(ts => ts.TaskCompletionStatusID == (short)ARTEnums.TaskCompletionStatus.Completed);
                if (oTaskStatus != null)
                {
                    completedCount = oTaskStatus.CompletionStatusCount;
                    completedATCount = oTaskStatus.ATCount;
                    completedGTCount = oTaskStatus.GTCount;
                    completedColor = oTaskStatus.StatusColor;
                }
                oTaskStatus = oTaskStatusList.FirstOrDefault(ts => ts.TaskCompletionStatusID == (short)ARTEnums.TaskCompletionStatus.Overdue);
                if (oTaskStatus != null)
                {
                    overdueCount = oTaskStatus.CompletionStatusCount;
                    overdueATCount = oTaskStatus.ATCount;
                    overdueGTCount = oTaskStatus.GTCount;
                    overdueColor = oTaskStatus.StatusColor;
                }
                totalCount = (pendingCount + completedCount + overdueCount);
            }

            //***********Chart Setting******************************************************************************

            ////check whether there is any existing Chart/Series,and remove it if any 

            if (chrtTaskStatus.ChartAreas.Count > 0)
            {
                ChartArea oChartAreaTaskConverage = chrtTaskStatus.ChartAreas["ChartAreaTaskConverage"];
                Series oSeriesTaskConverage = chrtTaskStatus.Series["TaskConverage"];

                chrtTaskStatus.Series.Remove(oSeriesTaskConverage);
                chrtTaskStatus.ChartAreas.Remove(oChartAreaTaskConverage);
            }

            ChartArea oChartArea = new ChartArea();
            oChartArea.Name = "ChartAreaTaskConverage";
            oChartArea.Area3DStyle.Enable3D = true;
            oChartArea.BorderColor = System.Drawing.Color.Red;

            oChartArea.Position.X = 0;
            oChartArea.Position.Y = 0;
            oChartArea.Position.Height = 100;           //****its in  percent. Can't be greater than 100
            oChartArea.Position.Width = 100;            //****(in  percent)
            oChartArea.InnerPlotPosition.X = 10;
            oChartArea.InnerPlotPosition.Y = 10;
            oChartArea.InnerPlotPosition.Height = 80;   //****(in  percent)(Area of chart corresponding to plot area )
            oChartArea.InnerPlotPosition.Width = 80;    //****(in  percent) (Area of chart corresponding to plot area)

            ////*****Note:  ChartArea.Area3DStyle.PointDepth : to show the depth  in the  pie Chart Control
            oChartArea.Area3DStyle.PointDepth = 600;
            chrtTaskStatus.ChartAreas.Add(oChartArea);

            //****** Populate series data

            Series oSeries = new Series();
            oSeries.Name = "TaskConverage";
            oSeries.ChartType = SeriesChartType.Pie;
            string labelText = LanguageUtil.GetValue(2629);

            DataPoint oDataPointPending = new DataPoint();
            oDataPointPending.SetValueXY("", pendingCount);
            oDataPointPending.AxisLabel = "\n" + string.Format(labelText, LanguageUtil.GetValue(2561), pendingCount, pendingATCount, pendingGTCount);
            oDataPointPending.ToolTip = string.Format("{0} ", LanguageUtil.GetValue(2561));
            oDataPointPending.Color = ColorTranslator.FromHtml(pendingColor);

            oSeries.Points.Add(oDataPointPending);
            oDataPointPending["Exploded"] = "true";
            oDataPointPending.PostBackValue = ARTEnums.TaskCompletionStatus.Pending.ToString("D");

            DataPoint oDataPointOverdue = new DataPoint();
            oDataPointOverdue.SetValueXY("", overdueCount);
            oDataPointOverdue.AxisLabel = "\n" + string.Format(labelText, LanguageUtil.GetValue(2562), overdueCount, overdueATCount, overdueGTCount);
            oDataPointOverdue.Color = ColorTranslator.FromHtml(overdueColor);
            oDataPointOverdue.ToolTip = string.Format("{0} ", LanguageUtil.GetValue(2562));

            oSeries.Points.Add(oDataPointOverdue);
            oDataPointOverdue["Exploded"] = "true";
            oDataPointOverdue.PostBackValue = ARTEnums.TaskCompletionStatus.Overdue.ToString("D");


            DataPoint oDataPointCompleted = new DataPoint();
            oDataPointCompleted.SetValueXY("", completedCount);
            oDataPointCompleted.AxisLabel = "\n" + string.Format(labelText, LanguageUtil.GetValue(2559), completedCount, completedATCount, completedGTCount);
            oDataPointCompleted.Color = ColorTranslator.FromHtml(completedColor);
            oDataPointCompleted.ToolTip = string.Format("{0} ", LanguageUtil.GetValue(2559));

            oSeries.Points.Add(oDataPointCompleted);
            oDataPointCompleted["Exploded"] = "true";
            oDataPointCompleted.PostBackValue = ARTEnums.TaskCompletionStatus.Completed.ToString("D");



            //Add the Series to the chart
            chrtTaskStatus.Series.Add(oSeries);
            //*******Property whether to show Labels outside or inside Pie. Default is Inside 
            chrtTaskStatus.Series["TaskConverage"]["PieLabelStyle"] = "Outside";
            //*******Label Style Setting 
            chrtTaskStatus.Series["TaskConverage"].Label = "#PERCENT" + "#AXISLABEL";

            //**********Label font Setting
            for (int i = 0; i < chrtTaskStatus.Series["TaskConverage"].Points.Count; i++)
            {
                chrtTaskStatus.Series["TaskConverage"].Points[i].Font = new Font("Arial", 8, FontStyle.Regular);

            }
            //*******Display the total accounts
            lblTotalTasks.Text = Helper.GetDisplayIntegerValue(totalCount);
            //***************************************************************************************
        }
        catch (ARTException ex)
        {
            WebPartHelper.ShowErrorMessage(tblMessage, tblContent, lblMessage, ex);
        }
        catch (Exception ex)
        {
            WebPartHelper.ShowErrorMessage(tblMessage, tblContent, lblMessage, ex);
        }

    }

    protected void chrtTaskStatus_Click(object sender, ImageMapEventArgs e)
    {

        Int16 TaskStatusID = Convert.ToInt16( e.PostBackValue);
        SendToTaskViewer(TaskStatusID);

    }

    protected void SendToTaskViewer(short TaskCompletionStatus)
    {


        string sessionKeyGeneralTaskPending = string.Empty;
        string sessionKeyGeneralTaskCompleted = string.Empty;
        string sessionKeyAccountTaskPending = string.Empty;
        string sessionKeyAccountTaskCompleted = string.Empty;
        sessionKeyGeneralTaskPending = SessionHelper.GetSessionKeyForGridFilter(ARTEnums.Grid.GeneralTaskPending);
        sessionKeyGeneralTaskCompleted = SessionHelper.GetSessionKeyForGridFilter(ARTEnums.Grid.GeneralTaskCompleted);
        sessionKeyAccountTaskPending = SessionHelper.GetSessionKeyForGridFilter(ARTEnums.Grid.AccountTaskPending);
        sessionKeyAccountTaskCompleted = SessionHelper.GetSessionKeyForGridFilter(ARTEnums.Grid.AccountTaskCompleted);

        List<FilterCriteria> oFilterCriteriaCollection = new List<FilterCriteria>();
        FilterCriteria oFilterCriteria;
        string[] CommandArg = null;
        if (TaskCompletionStatus == (short)ARTEnums.TaskCompletionStatus.Completed)
        {
            CommandArg = GetValueForCompleted().Split(_ARGUMENT_SEPARATOR);
        }
        else
        {
            CommandArg = GetValueForPendingOverdue().Split(_ARGUMENT_SEPARATOR);
        }
        if (CommandArg != null && CommandArg.Length > 0)
        {
            oFilterCriteria = new FilterCriteria();
            oFilterCriteria.OperatorID = (short)WebEnums.Operator.Matches;
            oFilterCriteria.ParameterID = (short)WebEnums.TaskColumnForFilter.TaskStatus;
            oFilterCriteria.DisplayValue = CommandArg[1];
            oFilterCriteria.Value = CommandArg[0];
            oFilterCriteriaCollection.Add(oFilterCriteria);
        }

        if (!string.IsNullOrEmpty(sessionKeyGeneralTaskCompleted) && !string.IsNullOrEmpty(sessionKeyGeneralTaskPending) && oFilterCriteriaCollection.Count > 0)
        {
            Session[sessionKeyGeneralTaskPending] = oFilterCriteriaCollection;
            Session[sessionKeyGeneralTaskCompleted] = oFilterCriteriaCollection;
        }
        if (!string.IsNullOrEmpty(sessionKeyAccountTaskCompleted) && !string.IsNullOrEmpty(sessionKeyAccountTaskPending) && oFilterCriteriaCollection.Count > 0)
        {
            Session[sessionKeyAccountTaskPending] = oFilterCriteriaCollection;
            Session[sessionKeyAccountTaskCompleted] = oFilterCriteriaCollection;
        }

        PageSettings oPageSettings = PageSettingHelper.GetPageSettings(WebEnums.ARTPages.TaskViewer);
        oPageSettings.ShowAccountHiddenTask = true;
        oPageSettings.ShowAllPendingAccountTask = true;
        oPageSettings.ShowAllPendingGeneralTask = true;
        oPageSettings.ShowGeneralHiddenTask = true;
        PageSettingHelper.SavePageSettings(WebEnums.ARTPages.TaskViewer, oPageSettings);

        string url = "~/Pages/TaskMaster/TaskViewer.aspx?" + QueryStringConstants.ACTIVE_TAB_INDEX + "=0&" + QueryStringConstants.TASK_COMPLETION_STATUS_ID + "=" + TaskCompletionStatus.ToString();
        //Response.Redirect(url);
        SessionHelper.RedirectToUrl(url);
        return;
    }

    private string GetValueForPendingOverdue()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append((short)ARTEnums.TaskStatus.NotStarted);
        sb.Append(Helper.FilterValueSeparator);
        sb.Append((short)ARTEnums.TaskStatus.PendReview);
        sb.Append(Helper.FilterValueSeparator);
        sb.Append((short)ARTEnums.TaskStatus.PendModAssignee);
        sb.Append(Helper.FilterValueSeparator);
        sb.Append((short)ARTEnums.TaskStatus.InProgress);
        sb.Append(Helper.FilterValueSeparator);
        sb.Append((short)ARTEnums.TaskStatus.PendModReviewer);
        sb.Append(Helper.FilterValueSeparator);
        sb.Append((short)ARTEnums.TaskStatus.PendApproval);
        sb.Append(_ARGUMENT_SEPARATOR);
        sb.Append(LanguageUtil.GetValue((int)ARTEnums.TaskStatusLabelID.NotStarted));
        sb.Append(", ");
        sb.Append(LanguageUtil.GetValue((int)ARTEnums.TaskStatusLabelID.PendReview));
        sb.Append(", ");
        sb.Append(LanguageUtil.GetValue((int)ARTEnums.TaskStatusLabelID.PendModAssignee));
        sb.Append(", ");
        sb.Append(LanguageUtil.GetValue((int)ARTEnums.TaskStatusLabelID.InProgress));
        sb.Append(", ");
        sb.Append(LanguageUtil.GetValue((int)ARTEnums.TaskStatusLabelID.PendModReviewer));
        sb.Append(", ");
        sb.Append(LanguageUtil.GetValue((int)ARTEnums.TaskStatusLabelID.PendApproval));
        return sb.ToString();
    }

    private string GetValueForCompleted()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append((short)ARTEnums.TaskStatus.Completed);
        sb.Append(_ARGUMENT_SEPARATOR);
        sb.Append(LanguageUtil.GetValue((int)ARTEnums.TaskStatusLabelID.Completed));
        return sb.ToString();
    }


}
