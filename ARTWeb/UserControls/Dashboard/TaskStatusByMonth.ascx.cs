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
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using Telerik.Web.UI;
using SkyStem.ART.Client.Exception;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Classes;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using SkyStem.ART.Client.Model.Base;
using SkyStem.ART.Client.Data;
using System.Text;
using SkyStem.Language.LanguageUtility;
using SkyStem.Language.LanguageUtility.Classes;
using System.Threading.Tasks;

public partial class UserControls_Dashboard_TaskStatusBuMonth : UserControlWebPartBase
{
    #region Variables and Constants

    bool isExportPDF;
    bool isExportExcel;
    List<TaskStatusCountInfo> _AllTaskStatusCountInfo = null;
    private const char _ARGUMENT_SEPARATOR = '^';

    #endregion

    #region Event Handlers

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        Page oPage = (Page)this.Parent.Page;
        MasterPageBase ompage = (MasterPageBase)oPage.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.LoadData = true;
            isExportPDF = false;
            isExportExcel = false;

            // Set default Sorting
            GridHelper.SetSortExpression(rgGeneralTaskStatusByMonth.MasterTableView, "MonthNumber");
            GridHelper.SetSortExpression(rgAccountTaskStatusByMonth.MasterTableView, "MonthNumber");
        }

    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (this.LoadData.Value && this.Visible)
            OnPageLoad();

    }

    #endregion

    #region "Grid Events"

    #region General Task Status Grid
    protected void rgGeneralTaskStatusByMonth_ItemCreated(object sender, GridItemEventArgs e)
    {
        GridHelper.RegisterPDFAndExcelForPostback(e, isExportPDF, isExportExcel, this.Page);
        GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
    }

    protected void rgGeneralTaskStatusByMonth_ItemDataBound(object sender, GridItemEventArgs e)
    {
        GridItemDataBound(e);
    }

    protected void rgGeneralTaskStatusByMonth_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        LoadGeneralTaskStatusByMonthGridData();
    }

    protected void rgGeneralTaskStatusByMonth_SortCommand(object source, GridSortCommandEventArgs e)
    {
        GridHelper.HandleSortCommand(e);
        LoadGeneralTaskStatusByMonthGridData();
        rgGeneralTaskStatusByMonth.DataBind();
    }

    protected void rgGeneralTaskStatusByMonth_OnItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
        {
            isExportPDF = true;
            LoadGeneralTaskStatusByMonthGridData();
            ShowHideColumnsForExport();
            GridHelper.ExportGridToPDF(rgGeneralTaskStatusByMonth, 2547);
        }
        if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
        {
            isExportExcel = true;
            LoadGeneralTaskStatusByMonthGridData();
            ShowHideColumnsForExport();
            GridHelper.ExportGridToExcel(rgGeneralTaskStatusByMonth, 2547);
        }
    }

    protected void SendToGeneralTaskViewer(object sender, CommandEventArgs e)
    {
        string[] CommandArg = e.CommandArgument.ToString().Split(_ARGUMENT_SEPARATOR);
        short TaskCompletionStatus = Convert.ToInt16(CommandArg[0]);
        int itemIndex = Convert.ToInt32(CommandArg[1]);
        GridDataItem item = rgGeneralTaskStatusByMonth.MasterTableView.Items[itemIndex];
        MultilingualAttributeInfo oMultilingualAttributeInfo = new MultilingualAttributeInfo();
        oMultilingualAttributeInfo.ApplicationID = AppSettingHelper.GetApplicationID();
        oMultilingualAttributeInfo.BusinessEntityID = SessionHelper.CurrentCompanyID.Value;
        oMultilingualAttributeInfo.LanguageID = AppSettingHelper.GetDefaultLanguageID();
        
        DateTime MonthStartDate = Convert.ToDateTime(item.GetDataKeyValue("MonthStartDate"));
        DateTime MonthEndDate = Convert.ToDateTime(item.GetDataKeyValue("MonthEndDate"));

        string sessionKeyGeneralTaskPending = string.Empty;
        string sessionKeyGeneralTaskCompleted = string.Empty;

        sessionKeyGeneralTaskPending = SessionHelper.GetSessionKeyForGridFilter(ARTEnums.Grid.GeneralTaskPending);
        sessionKeyGeneralTaskCompleted = SessionHelper.GetSessionKeyForGridFilter(ARTEnums.Grid.GeneralTaskCompleted);

        List<FilterCriteria> oFilterCriteriaCollection = new List<FilterCriteria>();
        FilterCriteria oFilterCriteria;
        oFilterCriteria = new FilterCriteria();
        oFilterCriteria.OperatorID = (short)WebEnums.Operator.Between;
        oFilterCriteria.ParameterID = (short)WebEnums.TaskColumnForFilter.DueDate;
        oFilterCriteria.Value = Helper.GetDisplayDate(MonthStartDate, oMultilingualAttributeInfo) + Helper.FilterValueSeparator + Helper.GetDisplayDate(MonthEndDate, oMultilingualAttributeInfo);
        oFilterCriteria.DisplayValue = MonthStartDate.ToShortDateString() + " - " + MonthEndDate.ToShortDateString();
        oFilterCriteriaCollection.Add(oFilterCriteria);

        oFilterCriteria = new FilterCriteria();
        oFilterCriteria.OperatorID = (short)WebEnums.Operator.Matches;
        oFilterCriteria.ParameterID = (short)WebEnums.TaskColumnForFilter.TaskStatus;
        oFilterCriteria.DisplayValue = CommandArg[3];
        oFilterCriteria.Value = CommandArg[2];
        oFilterCriteriaCollection.Add(oFilterCriteria);

        if (!string.IsNullOrEmpty(sessionKeyGeneralTaskCompleted) && !string.IsNullOrEmpty(sessionKeyGeneralTaskPending) && oFilterCriteriaCollection.Count > 0)
        {
            Session[sessionKeyGeneralTaskPending] = oFilterCriteriaCollection;
            Session[sessionKeyGeneralTaskCompleted] = oFilterCriteriaCollection;
        }

        PageSettings oPageSettings = PageSettingHelper.GetPageSettings(WebEnums.ARTPages.TaskViewer);
        oPageSettings.ShowAccountHiddenTask = true;
        oPageSettings.ShowAllPendingAccountTask = true;
        oPageSettings.ShowAllPendingGeneralTask = true;
        oPageSettings.ShowGeneralHiddenTask = true;
        PageSettingHelper.SavePageSettings(WebEnums.ARTPages.TaskViewer, oPageSettings);


        string url = "~/Pages/TaskMaster/TaskViewer.aspx?" + QueryStringConstants.ACTIVE_TAB_INDEX + "=0&" + QueryStringConstants.TASK_COMPLETION_STATUS_ID + "=" + TaskCompletionStatus;
        //Response.Redirect(url);
        SessionHelper.RedirectToUrl(url);
        return;
    }

    #endregion


    #region Account Task Status Grid

    protected void rgAccountTaskStatusByMonth_ItemCreated(object sender, GridItemEventArgs e)
    {
        GridHelper.RegisterPDFAndExcelForPostback(e, isExportPDF, isExportExcel, this.Page);
        GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
    }

    protected void rgAccountTaskStatusByMonth_ItemDataBound(object sender, GridItemEventArgs e)
    {
        GridItemDataBound(e);
    }

    protected void rgAccountTaskStatusByMonth_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        LoadAccountTaskStatusByMonthGridData();
    }

    protected void rgAccountTaskStatusByMonth_SortCommand(object source, GridSortCommandEventArgs e)
    {
        GridHelper.HandleSortCommand(e);
        LoadAccountTaskStatusByMonthGridData();
        rgAccountTaskStatusByMonth.DataBind();
    }

    protected void rgAccountTaskStatusByMonth_OnItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
        {
            isExportPDF = true;
            LoadAccountTaskStatusByMonthGridData();
            ShowHideColumnsForExportForAccountTask();
            GridHelper.ExportGridToPDF(rgAccountTaskStatusByMonth, 2546);

        }
        if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
        {
            isExportExcel = true;
            LoadAccountTaskStatusByMonthGridData();
            ShowHideColumnsForExportForAccountTask();
            GridHelper.ExportGridToExcel(rgAccountTaskStatusByMonth, 2546);
        }
    }

    protected void SendToAccountTaskViewer(object sender, CommandEventArgs e)
    {
        string[] CommandArg = e.CommandArgument.ToString().Split(_ARGUMENT_SEPARATOR);
        short TaskCompletionStatus = Convert.ToInt16(CommandArg[0]);
        int itemIndex = Convert.ToInt32(CommandArg[1]);
        GridDataItem item = rgAccountTaskStatusByMonth.MasterTableView.Items[itemIndex];
        MultilingualAttributeInfo oMultilingualAttributeInfo = new MultilingualAttributeInfo();
        oMultilingualAttributeInfo.ApplicationID = AppSettingHelper.GetApplicationID();
        oMultilingualAttributeInfo.BusinessEntityID = SessionHelper.CurrentCompanyID.Value;
        oMultilingualAttributeInfo.LanguageID = AppSettingHelper.GetDefaultLanguageID();

        DateTime MonthStartDate = Convert.ToDateTime(item.GetDataKeyValue("MonthStartDate"));
        DateTime MonthEndDate = Convert.ToDateTime(item.GetDataKeyValue("MonthEndDate"));

        string sessionKeyAccountTaskPending = string.Empty;
        string sessionKeyAccountTaskCompleted = string.Empty;

        sessionKeyAccountTaskPending = SessionHelper.GetSessionKeyForGridFilter(ARTEnums.Grid.AccountTaskPending);
        sessionKeyAccountTaskCompleted = SessionHelper.GetSessionKeyForGridFilter(ARTEnums.Grid.AccountTaskCompleted);

        List<FilterCriteria> oFilterCriteriaCollection = new List<FilterCriteria>();
        FilterCriteria oFilterCriteria;
        oFilterCriteria = new FilterCriteria();
        oFilterCriteria.OperatorID = (short)WebEnums.Operator.Between;
        oFilterCriteria.ParameterID = (short)WebEnums.TaskColumnForFilter.DueDate;
        oFilterCriteria.Value = Helper.GetDisplayDate(MonthStartDate, oMultilingualAttributeInfo) + Helper.FilterValueSeparator + Helper.GetDisplayDate(MonthEndDate, oMultilingualAttributeInfo);
        oFilterCriteria.DisplayValue = MonthStartDate.ToShortDateString() + " - " + MonthEndDate.ToShortDateString();
        oFilterCriteriaCollection.Add(oFilterCriteria);

        oFilterCriteria = new FilterCriteria();
        oFilterCriteria.OperatorID = (short)WebEnums.Operator.Matches;
        oFilterCriteria.ParameterID = (short)WebEnums.TaskColumnForFilter.TaskStatus;
        oFilterCriteria.Value = CommandArg[2];
        oFilterCriteria.DisplayValue = CommandArg[3];
        oFilterCriteriaCollection.Add(oFilterCriteria);

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
        string url = "~/Pages/TaskMaster/TaskViewer.aspx?" + QueryStringConstants.ACTIVE_TAB_INDEX + "=1&" + QueryStringConstants.TASK_COMPLETION_STATUS_ID + "=" + TaskCompletionStatus;
        //Response.Redirect(url);
        SessionHelper.RedirectToUrl(url);
        return;
    }

    #endregion

    #endregion

    #region Other Events

    protected void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        _AllTaskStatusCountInfo = null;
        this.LoadData = true;
    }

    #endregion

    #endregion

    #region Private Methods
    private void OnPageLoad()
    {
        rgGeneralTaskStatusByMonth.NeedDataSource += new Telerik.Web.UI.GridNeedDataSourceEventHandler(rgGeneralTaskStatusByMonth_NeedDataSource);
        rgAccountTaskStatusByMonth.NeedDataSource += new GridNeedDataSourceEventHandler(rgAccountTaskStatusByMonth_NeedDataSource);
        rgGeneralTaskStatusByMonth.Rebind();
        rgAccountTaskStatusByMonth.Rebind();
    }

    private void ShowHideColumnsForExport()
    {
        GridColumn oGridNameDataColumn = rgGeneralTaskStatusByMonth.MasterTableView.Columns.FindByUniqueName("MonthNameLblColumn");
        if (oGridNameDataColumn != null)
        {
            oGridNameDataColumn.Visible = true;
        }
        oGridNameDataColumn = rgGeneralTaskStatusByMonth.MasterTableView.Columns.FindByUniqueName("PendingDataColumn");
        if (oGridNameDataColumn != null)
        {
            oGridNameDataColumn.Visible = true;
        }
        oGridNameDataColumn = rgGeneralTaskStatusByMonth.MasterTableView.Columns.FindByUniqueName("OverdueDataColumn");
        if (oGridNameDataColumn != null)
        {
            oGridNameDataColumn.Visible = true;
        }
        oGridNameDataColumn = rgGeneralTaskStatusByMonth.MasterTableView.Columns.FindByUniqueName("CompletedDataColumn");
        if (oGridNameDataColumn != null)
        {
            oGridNameDataColumn.Visible = true;
        }

        oGridNameDataColumn = rgGeneralTaskStatusByMonth.MasterTableView.Columns.FindByUniqueName("PendingLinkButtonColumn");
        if (oGridNameDataColumn != null)
        {
            oGridNameDataColumn.Visible = false;
        }

        oGridNameDataColumn = rgGeneralTaskStatusByMonth.MasterTableView.Columns.FindByUniqueName("OverdueLinkButtonColumn");
        if (oGridNameDataColumn != null)
        {
            oGridNameDataColumn.Visible = false;
        }
        oGridNameDataColumn = rgGeneralTaskStatusByMonth.MasterTableView.Columns.FindByUniqueName("CompletedLinkButtonColumn");
        if (oGridNameDataColumn != null)
        {
            oGridNameDataColumn.Visible = false;
        }

    }

    private void ShowHideColumnsForExportForAccountTask()
    {
        rgAccountTaskStatusByMonth.MasterTableView.Columns.FindByUniqueName("MonthNameLblColumn").Visible = true;
        rgAccountTaskStatusByMonth.MasterTableView.Columns.FindByUniqueName("PendingDataColumn").Visible = true;
        rgAccountTaskStatusByMonth.MasterTableView.Columns.FindByUniqueName("OverdueDataColumn").Visible = true;
        rgAccountTaskStatusByMonth.MasterTableView.Columns.FindByUniqueName("CompletedDataColumn").Visible = true;

        rgAccountTaskStatusByMonth.MasterTableView.Columns.FindByUniqueName("PendingLinkButtonColumn").Visible = false;
        rgAccountTaskStatusByMonth.MasterTableView.Columns.FindByUniqueName("OverdueLinkButtonColumn").Visible = false;
        rgAccountTaskStatusByMonth.MasterTableView.Columns.FindByUniqueName("CompletedLinkButtonColumn").Visible = false;
    }

    private void GridItemDataBound(GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.Item
            || e.Item.ItemType == GridItemType.AlternatingItem)
        {
            TaskStatusCountInfo oTaskStatusCountInfo = (TaskStatusCountInfo)e.Item.DataItem;

            ExLinkButton lnkBtnPending = (ExLinkButton)e.Item.FindControl("lnkBtnPending");
            ExLinkButton lnkBtnOverdue = (ExLinkButton)e.Item.FindControl("lnkBtnOverdue");
            ExLinkButton lnkBtnCompleted = (ExLinkButton)e.Item.FindControl("lnkBtnCompleted");

            lnkBtnPending.Text = Helper.GetDisplayIntegerValue(oTaskStatusCountInfo.Pending);
            lnkBtnOverdue.Text = Helper.GetDisplayIntegerValue(oTaskStatusCountInfo.Overdue);
            lnkBtnCompleted.Text = Helper.GetDisplayIntegerValue(oTaskStatusCountInfo.Completed);
            if (oTaskStatusCountInfo.Pending.HasValue)
                lnkBtnPending.Enabled = true;
            else
                lnkBtnPending.Enabled = false;

            if (oTaskStatusCountInfo.Overdue.HasValue)
                lnkBtnOverdue.Enabled = true;
            else
                lnkBtnOverdue.Enabled = false;

            if (oTaskStatusCountInfo.Completed.HasValue)
                lnkBtnCompleted.Enabled = true;
            else
                lnkBtnCompleted.Enabled = false;

            lnkBtnPending.CommandArgument = GetCommandArgumentForPendingOverdue(e, ARTEnums.TaskCompletionStatus.Pending);

            lnkBtnOverdue.CommandArgument = GetCommandArgumentForPendingOverdue(e, ARTEnums.TaskCompletionStatus.Overdue);

            lnkBtnCompleted.CommandArgument = GetCommandArgumentForCompleted(e);

            ExLabel lblMonthName = (ExLabel)e.Item.FindControl("lblMonthName");
            ExLabel lblPending = (ExLabel)e.Item.FindControl("lblPending");
            ExLabel lblOverdue = (ExLabel)e.Item.FindControl("lblOverdue");
            ExLabel lblCompleted = (ExLabel)e.Item.FindControl("lblCompleted");

            lblMonthName.Text = Helper.GetDisplayStringValue(oTaskStatusCountInfo.MonthName) + " " + Helper.GetDisplayStringValue(oTaskStatusCountInfo.Year);
            lblPending.Text = Helper.GetDisplayIntegerValue(oTaskStatusCountInfo.Pending);
            lblOverdue.Text = Helper.GetDisplayIntegerValue(oTaskStatusCountInfo.Overdue);
            lblCompleted.Text = Helper.GetDisplayIntegerValue(oTaskStatusCountInfo.Completed);
        }
    }

    private void ShowHideControl(WebEnums.FeatureCapabilityMode eMode, Control ctl)
    {
        if (eMode == WebEnums.FeatureCapabilityMode.Hidden || eMode == WebEnums.FeatureCapabilityMode.Disable)
        {
            ctl.Visible = false;
        }
        else
        {
            ctl.Visible = true;
        }
    }

    private void LoadGeneralTaskStatusByMonthGridData()
    {
        List<TaskStatusCountInfo> oTaskStatusCountInfoCollection = null;

        try
        {
            //AllTaskStatus();
            if (_AllTaskStatusCountInfo != null)
            {
                oTaskStatusCountInfoCollection = (from obj in _AllTaskStatusCountInfo
                                                  where obj.TaskTypeID == (short)ARTEnums.TaskType.GeneralTask
                                                  select obj).ToList();
            }
            rgGeneralTaskStatusByMonth.DataSource = oTaskStatusCountInfoCollection;
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

    public override IAsyncResult GetDataAsync()
    {
        return DashboardHelper.GetDataForTaskStatusCountByMonthAsync(SessionHelper.CurrentUserID, SessionHelper.CurrentRoleID, SessionHelper.CurrentReconciliationPeriodID, System.DateTime.Now, Helper.GetAppUserInfo());
    }

    public override void DataLoaded(IAsyncResult result)
    {
        Task<List<TaskStatusCountInfo>> oTask = (Task<List<TaskStatusCountInfo>>)result;
        if (oTask.IsCompleted)
        {
            _AllTaskStatusCountInfo = oTask.Result;
            LanguageHelper.TranslateMonthName(_AllTaskStatusCountInfo);
            rgGeneralTaskStatusByMonth.Rebind();
            rgAccountTaskStatusByMonth.Rebind();
        }        
    }

    private void AllTaskStatus()
    {

        if (_AllTaskStatusCountInfo == null)
        {
            _AllTaskStatusCountInfo = DashboardHelper.GetDataForTaskStatusCountByMonth(SessionHelper.CurrentUserID, SessionHelper.CurrentRoleID, SessionHelper.CurrentReconciliationPeriodID, System.DateTime.Now, Helper.GetAppUserInfo());
            LanguageHelper.TranslateMonthName(_AllTaskStatusCountInfo);
        }
    }

    private void LoadAccountTaskStatusByMonthGridData()
    {
        List<TaskStatusCountInfo> oTaskStatusCountInfoCollection = null;
        try
        {
            //if (_AllTaskStatusCountInfo == null)
                //AllTaskStatus();
            if (_AllTaskStatusCountInfo != null)
            {
                oTaskStatusCountInfoCollection = (from obj in _AllTaskStatusCountInfo
                                                  where obj.TaskTypeID == (short)ARTEnums.TaskType.AccountTask
                                                  select obj).ToList();
            }
            rgAccountTaskStatusByMonth.DataSource = oTaskStatusCountInfoCollection;
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

    private string GetCommandArgumentForPendingOverdue(GridItemEventArgs e, ARTEnums.TaskCompletionStatus eCompletionStatus)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append((short)eCompletionStatus);
        sb.Append(_ARGUMENT_SEPARATOR);
        sb.Append(e.Item.ItemIndex.ToString());
        sb.Append(_ARGUMENT_SEPARATOR);
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

    private string GetCommandArgumentForCompleted(GridItemEventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append((short)ARTEnums.TaskCompletionStatus.Completed);
        sb.Append(_ARGUMENT_SEPARATOR);
        sb.Append(e.Item.ItemIndex.ToString());
        sb.Append(_ARGUMENT_SEPARATOR);
        sb.Append((short)ARTEnums.TaskStatus.Completed);
        sb.Append(_ARGUMENT_SEPARATOR);
        sb.Append(LanguageUtil.GetValue((int)ARTEnums.TaskStatusLabelID.Completed));
        return sb.ToString();
    }

    #endregion
}
