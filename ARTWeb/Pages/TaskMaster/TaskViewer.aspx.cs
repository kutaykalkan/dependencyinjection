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
using System.Collections.Generic;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Web.Data;
using Telerik.Web.UI;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.Exception;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using SkyStem.Language.LanguageUtility;


public partial class Pages_TaskMaster_TaskViewer : PageBaseRecForm
{

    PageSettings oPageSettings;
    #region Public Properties
    public string FilterPageURLGeneraltaskGridPending
    {
        get
        {
            string url = Page.ResolveUrl("~/Pages/GridApplyFilter.aspx");
            return url + "?" + QueryStringConstants.GRID_TYPE + "=" + this.ucGeneraltaskGridPending.GridType.ToString("d");
        }
    }
    public string FilterPageURLGeneraltaskGridCompleted
    {
        get
        {
            string url = Page.ResolveUrl("~/Pages/GridApplyFilter.aspx");
            return url + "?" + QueryStringConstants.GRID_TYPE + "=" + this.ucGeneralTaskGridCompleted.GridType.ToString("d");
        }
    }
    public string FilterPageURLAccounttaskGridPending
    {
        get
        {
            string url = Page.ResolveUrl("~/Pages/GridApplyFilter.aspx");
            return url + "?" + QueryStringConstants.GRID_TYPE + "=" + this.ucAccountTaskGridPending.GridType.ToString("d");
        }
    }
    public string FilterPageURLAccounttaskGridCompleted
    {
        get
        {
            string url = Page.ResolveUrl("~/Pages/GridApplyFilter.aspx");
            return url + "?" + QueryStringConstants.GRID_TYPE + "=" + this.ucAccountTaskGridCompleted.GridType.ToString("d");
        }
    }
    public string CustomizationURLForGTPending
    {
        get
        {
            string url = Page.ResolveUrl("~/Pages/GridCustomization.aspx");
            return url + "?" + QueryStringConstants.GRID_TYPE + "=" + this.ucGeneraltaskGridPending.GridType.ToString("d");
        }
    }
    public string CustomizationURLForGTCompleted
    {
        get
        {
            string url = Page.ResolveUrl("~/Pages/GridCustomization.aspx");
            return url + "?" + QueryStringConstants.GRID_TYPE + "=" + this.ucGeneralTaskGridCompleted.GridType.ToString("d");
        }
    }
    #region GeneralTaskProperties
    public string AddPageURLGeneraltaskGridPending
    {
        get
        {
            string url = Page.ResolveUrl(URLConstants.CREATE_TASK_URL);
            return PopupHelper.GetJavascriptParameterListForAddEditTask("", null, "OpenRadWindowWithName", url, QueryStringConstants.INSERT, null, (short)ARTEnums.TaskType.GeneralTask, this.hdIsRefreshData.ClientID);
        }
    }
    public string EditPageURLGeneraltaskGridPending
    {
        get
        {
            string url = Page.ResolveUrl(URLConstants.URL_BULK_COMPLETE_TASK);
            return PopupHelper.GetJavascriptParameterListForBulkStatusUpdate("", "OpenRadWindowWithName", url, "BulkEditTasks", ARTEnums.TaskActionType.BulkEdit, QueryStringConstants.INSERT
                    , ARTEnums.TaskType.GeneralTask, ARTEnums.RecordType.TaskComplition, null, null);
        }
    }
    public string DeletePageURLGeneraltaskGridPending
    {
        get
        {
            string url = Page.ResolveClientUrl(URLConstants.URL_BULK_COMPLETE_TASK);
            return PopupHelper.GetJavascriptParameterListForBulkStatusUpdate("", "OpenRadWindowWithName", url, "BulkDeleteTasks", ARTEnums.TaskActionType.Remove, QueryStringConstants.INSERT
                , ARTEnums.TaskType.GeneralTask, ARTEnums.RecordType.TaskComplition, null, null);
        }
    }
    public string ReviewPageURLGeneraltaskGridPending
    {
        get
        {
            string url = Page.ResolveClientUrl(URLConstants.URL_BULK_COMPLETE_TASK);
            return PopupHelper.GetJavascriptParameterListForBulkStatusUpdate("", "OpenRadWindowWithName", url, "BulkReviewTasks", ARTEnums.TaskActionType.Review, QueryStringConstants.INSERT
                 , ARTEnums.TaskType.GeneralTask, ARTEnums.RecordType.TaskComplition, null, null);
        }
    }
    public string ApprovePageURLGeneraltaskGridPending
    {
        get
        {
            string url = Page.ResolveClientUrl(URLConstants.URL_BULK_COMPLETE_TASK);
            return PopupHelper.GetJavascriptParameterListForBulkStatusUpdate("", "OpenRadWindowWithName", url, "BulkApproveTasks", ARTEnums.TaskActionType.Approve, QueryStringConstants.INSERT
                 , ARTEnums.TaskType.GeneralTask, ARTEnums.RecordType.TaskComplition, null, null);
        }
    }
    public string RejectPageURLGeneraltaskGridPending
    {
        get
        {
            string url = Page.ResolveClientUrl(URLConstants.URL_BULK_COMPLETE_TASK);
            return PopupHelper.GetJavascriptParameterListForBulkStatusUpdate("", "OpenRadWindowWithName", url, "BulkRejectTasks", ARTEnums.TaskActionType.Reject, QueryStringConstants.INSERT
                 , ARTEnums.TaskType.GeneralTask, ARTEnums.RecordType.TaskComplition, null, null);
        }
    }
    public string DonePageURLGeneraltaskGridPending
    {
        get
        {
            string url = Page.ResolveClientUrl(URLConstants.URL_BULK_COMPLETE_TASK);
            return PopupHelper.GetJavascriptParameterListForBulkStatusUpdate("", "OpenRadWindowWithName", url, "BulkCompleteTasks", ARTEnums.TaskActionType.Complete, QueryStringConstants.INSERT
                , ARTEnums.TaskType.GeneralTask, ARTEnums.RecordType.TaskComplition, null, null);
        }
    }

    public string ReopenPageURLGeneraltaskGridPending
    {
        get
        {
            string url = Page.ResolveClientUrl(URLConstants.URL_BULK_COMPLETE_TASK);
            return PopupHelper.GetJavascriptParameterListForBulkStatusUpdate("", "OpenRadWindowWithName", url, "BulkReopenTasks", ARTEnums.TaskActionType.Reopen, QueryStringConstants.INSERT
                , ARTEnums.TaskType.GeneralTask, ARTEnums.RecordType.TaskComplition, null, null);
        }
    }

    #endregion
    #region AccountTaskProperties
    public string AddPageURLAccounttaskGridPending
    {
        get
        {
            string url = Page.ResolveClientUrl(URLConstants.CREATE_TASK_URL);
            short taskTypeID = (short)ARTEnums.TaskType.AccountTask;
            return PopupHelper.GetJavascriptParameterListForAddEditTask("", null, "OpenRadWindowWithName", url, QueryStringConstants.INSERT, null, taskTypeID, this.hdIsRefreshData.ClientID);
        }
    }

    public string DeletePageURLAccounttaskGridPending
    {
        get
        {
            string url = Page.ResolveClientUrl(URLConstants.URL_BULK_COMPLETE_TASK);
            return PopupHelper.GetJavascriptParameterListForBulkStatusUpdate("", "OpenRadWindowWithName", url, "BulkDeleteTasks", ARTEnums.TaskActionType.Remove, QueryStringConstants.INSERT
                , ARTEnums.TaskType.AccountTask, ARTEnums.RecordType.TaskComplition, null, null);
        }
    }
    public string ReviewPageURLAccounttaskGridPending
    {
        get
        {
            string url = Page.ResolveClientUrl(URLConstants.URL_BULK_COMPLETE_TASK);
            return PopupHelper.GetJavascriptParameterListForBulkStatusUpdate("", "OpenRadWindowWithName", url, "BulkReviewTasks", ARTEnums.TaskActionType.Review, QueryStringConstants.INSERT
                , ARTEnums.TaskType.AccountTask, ARTEnums.RecordType.TaskComplition, null, null);
        }
    }
    public string ApprovePageURLAccounttaskGridPending
    {
        get
        {
            string url = Page.ResolveClientUrl(URLConstants.URL_BULK_COMPLETE_TASK);
            return PopupHelper.GetJavascriptParameterListForBulkStatusUpdate("", "OpenRadWindowWithName", url, "BulkApproveTasks", ARTEnums.TaskActionType.Approve, QueryStringConstants.INSERT
                , ARTEnums.TaskType.AccountTask, ARTEnums.RecordType.TaskComplition, null, null);
        }
    }
    public string RejectPageURLAccounttaskGridPending
    {
        get
        {
            string url = Page.ResolveClientUrl(URLConstants.URL_BULK_COMPLETE_TASK);
            return PopupHelper.GetJavascriptParameterListForBulkStatusUpdate("", "OpenRadWindowWithName", url, "BulkRejectTasks", ARTEnums.TaskActionType.Reject, QueryStringConstants.INSERT
                , ARTEnums.TaskType.AccountTask, ARTEnums.RecordType.TaskComplition, null, null);
        }
    }
    public string DonePageURLAccounttaskGridPending
    {
        get
        {
            string url = Page.ResolveClientUrl(URLConstants.URL_BULK_COMPLETE_TASK);
            return PopupHelper.GetJavascriptParameterListForBulkStatusUpdate("", "OpenRadWindowWithName", url, "BulkCompleteTasks", ARTEnums.TaskActionType.Complete, QueryStringConstants.INSERT
                , ARTEnums.TaskType.AccountTask, ARTEnums.RecordType.TaskComplition, null, null);
        }
    }
    public string ReopenPageURLAccounttaskGridPending
    {
        get
        {
            string url = Page.ResolveClientUrl(URLConstants.URL_BULK_COMPLETE_TASK);
            return PopupHelper.GetJavascriptParameterListForBulkStatusUpdate("", "OpenRadWindowWithName", url, "BulkReopenTasks", ARTEnums.TaskActionType.Reopen, QueryStringConstants.INSERT
                , ARTEnums.TaskType.AccountTask, ARTEnums.RecordType.TaskComplition, null, null);
        }
    }

    #endregion
    #endregion

    #region Private attributes
    string sessionKeyGeneralTaskPending = string.Empty;
    string sessionKeyGeneralTaskCompleted = string.Empty;
    string sessionKeyAccountTaskPending = string.Empty;
    string sessionKeyAccountTaskCompleted = string.Empty;

    List<string> pendingGridHideColumnList = new List<string>(); //{ "CompletionDate", "CompletionDocs","CompletionComment"};
    List<string> completedGridHideColumnList = new List<string>();// { "Description", "AttachmentCount", "ApproverComment", "ApprovalDuration"};

    #endregion

    #region Page events

    protected void Page_Init(object sender, EventArgs e)
    {
        this.PageID = WebEnums.ARTPages.TaskViewer;
        MasterPageBase ompage = (MasterPageBase)this.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);

        ucGeneraltaskGridPending.HideGridColumns(pendingGridHideColumnList);
        ucGeneralTaskGridCompleted.HideGridColumns(completedGridHideColumnList);
        ucAccountTaskGridPending.HideGridColumns(pendingGridHideColumnList);
        ucAccountTaskGridCompleted.HideGridColumns(completedGridHideColumnList);

        //RadToolBarDropDown ddlAction = this.ucGeneraltaskGridPending.ActionDropDown;
        //RadToolBar toolBar = this.ucGeneraltaskGridPending.RadToolBar;
        //toolBar.OnClientButtonClicked = TaskViewerConstants.TOOLBAR_COMMAND_ADD_ON_CLIENT_CLICKED_GENERAL_TASK;

        //if (ddlAction != null)
        //{
        //    string url;
        //    string para;
        //    RadToolBarButtonCollection ddlActionButtons = ddlAction.Buttons;

        //    RadToolBarButton btnAdd = ddlActionButtons.FindButtonByValue(ToolBarCommmand.TOOLBAR_COMMAND_ADD);
        //    if (btnAdd != null)
        //    {
        //        btnAdd.PostBack = false;
        //        url = Page.ResolveUrl(URLConstants.CREATE_TASK_URL);
        //        para = PopupHelper.GetJavascriptParameterListForAddEditTask(null, "OpenRadWindowWithName", url, QueryStringConstants.INSERT, null, (short)ARTEnums.TaskType.GeneralTask, this.hdIsRefreshData.ClientID);
        //        btnAdd.Attributes.Add(TaskViewerConstants.TOOLBAR_COMMAND_ADD_ATTRIBUTE_NAME_GENERAL_TASK, para);
        //    }

        //    RadToolBarButton btnBulkEdit = ddlActionButtons.FindButtonByValue(ToolBarCommmand.TOOLBAR_COMMAND_BULKEDIT);
        //    if (btnBulkEdit != null)
        //    {
        //        btnBulkEdit.PostBack = false;
        //        url = Page.ResolveUrl(URLConstants.URL_BULK_COMPLETE_TASK);
        //        para = PopupHelper.GetJavascriptParameterListForBulkStatusUpdate("OpenRadWindowWithName", url, "BulkEditTasks", ARTEnums.TaskActionType.BulkEdit, QueryStringConstants.EDIT
        //            , ARTEnums.TaskType.GeneralTask, ARTEnums.RecordType.TaskComplition, null);
        //        btnBulkEdit.Attributes.Add(TaskViewerConstants.TOOLBAR_COMMAND_ADD_ATTRIBUTE_NAME_GENERAL_TASK, para);
        //    }

        //    RadToolBarButton btnDelete = ddlActionButtons.FindButtonByValue(ToolBarCommmand.TOOLBAR_COMMAND_DELETE);
        //    if (btnDelete != null)
        //    {
        //        btnDelete.PostBack = false;
        //        url = Page.ResolveClientUrl(URLConstants.URL_BULK_COMPLETE_TASK);
        //        para = PopupHelper.GetJavascriptParameterListForBulkStatusUpdate("OpenRadWindowWithName", url, "BulkDeleteTasks", ARTEnums.TaskActionType.Remove, QueryStringConstants.EDIT
        //            , ARTEnums.TaskType.GeneralTask, ARTEnums.RecordType.TaskComplition, null);
        //        btnDelete.Attributes.Add(TaskViewerConstants.TOOLBAR_COMMAND_ADD_ATTRIBUTE_NAME_GENERAL_TASK, para);
        //    }

        //    RadToolBarButton btnApprove = ddlActionButtons.FindButtonByValue(ToolBarCommmand.TOOLBAR_COMMAND_APPROVE);
        //    if (btnApprove != null)
        //    {
        //        btnApprove.PostBack = false;
        //        url = Page.ResolveClientUrl(URLConstants.URL_BULK_COMPLETE_TASK);
        //        para = PopupHelper.GetJavascriptParameterListForBulkStatusUpdate("OpenRadWindowWithName", url, "BulkApproveTasks", ARTEnums.TaskActionType.Approve, QueryStringConstants.EDIT
        //            , ARTEnums.TaskType.GeneralTask, ARTEnums.RecordType.TaskComplition, null);
        //        btnApprove.Attributes.Add(TaskViewerConstants.TOOLBAR_COMMAND_ADD_ATTRIBUTE_NAME_GENERAL_TASK, para);
        //    }

        //    RadToolBarButton btnReject = ddlActionButtons.FindButtonByValue(ToolBarCommmand.TOOLBAR_COMMAND_REJECT);
        //    if (btnReject != null)
        //    {
        //        btnReject.PostBack = false;
        //        url = Page.ResolveClientUrl(URLConstants.URL_BULK_COMPLETE_TASK);
        //        para = PopupHelper.GetJavascriptParameterListForBulkStatusUpdate("OpenRadWindowWithName", url, "BulkRejectTasks", ARTEnums.TaskActionType.Reject, QueryStringConstants.EDIT
        //            , ARTEnums.TaskType.GeneralTask, ARTEnums.RecordType.TaskComplition, null);
        //        btnReject.Attributes.Add(TaskViewerConstants.TOOLBAR_COMMAND_ADD_ATTRIBUTE_NAME_GENERAL_TASK, para);
        //    }

        //    RadToolBarButton btnDone = ddlActionButtons.FindButtonByValue(ToolBarCommmand.TOOLBAR_COMMAND_DONE);
        //    if (btnDone != null)
        //    {
        //        btnDone.PostBack = false;
        //        url = Page.ResolveClientUrl(URLConstants.URL_BULK_COMPLETE_TASK);
        //        para = PopupHelper.GetJavascriptParameterListForBulkStatusUpdate("OpenRadWindowWithName", url, "BulkCompleteTasks", ARTEnums.TaskActionType.Complete, QueryStringConstants.EDIT
        //            , ARTEnums.TaskType.GeneralTask, ARTEnums.RecordType.TaskComplition, null);
        //        btnDone.Attributes.Add(TaskViewerConstants.TOOLBAR_COMMAND_ADD_ATTRIBUTE_NAME_GENERAL_TASK, para);
        //    }

        //}

        //RadToolBarDropDown ddlActionAccountTask = this.ucAccountTaskGridPending.ActionDropDown;
        //RadToolBar toolBarAccountTask = this.ucAccountTaskGridPending.RadToolBar;
        //toolBarAccountTask.OnClientButtonClicked = TaskViewerConstants.TOOLBAR_COMMAND_ADD_ON_CLIENT_CLICKED_ACCOUNT_TASK;

        //if (ddlActionAccountTask != null)
        //{
        //    string url;
        //    string para;
        //    RadToolBarButtonCollection ddlActionButtons = ddlActionAccountTask.Buttons;
        //    RadToolBarButton btnAdd = ddlActionButtons.FindButtonByValue(ToolBarCommmand.TOOLBAR_COMMAND_ADD);
        //    if (btnAdd != null)
        //    {
        //        btnAdd.PostBack = false;
        //        url = Page.ResolveClientUrl(URLConstants.CREATE_TASK_URL);
        //        short taskTypeID = (short)ARTEnums.TaskType.AccountTask;
        //        para = PopupHelper.GetJavascriptParameterListForAddEditTask(null, "OpenRadWindowWithName", url, QueryStringConstants.INSERT, null, taskTypeID, this.hdIsRefreshData.ClientID);
        //        btnAdd.Attributes.Add(TaskViewerConstants.TOOLBAR_COMMAND_ADD_ATTRIBUTE_NAME_ACCOUNT_TASK, para);
        //    }

        //    RadToolBarButton btnDelete = ddlActionButtons.FindButtonByValue(ToolBarCommmand.TOOLBAR_COMMAND_DELETE);
        //    if (btnDelete != null)
        //    {
        //        btnDelete.PostBack = false;
        //        url = Page.ResolveClientUrl(URLConstants.URL_BULK_COMPLETE_TASK);
        //        para = PopupHelper.GetJavascriptParameterListForBulkStatusUpdate("OpenRadWindowWithName", url, "Bulk Complete Tasks", ARTEnums.TaskActionType.Remove, QueryStringConstants.EDIT
        //            , ARTEnums.TaskType.AccountTask, ARTEnums.RecordType.TaskComplition, null);
        //        btnDelete.Attributes.Add(TaskViewerConstants.TOOLBAR_COMMAND_ADD_ATTRIBUTE_NAME_ACCOUNT_TASK, para);
        //    }

        //    RadToolBarButton btnApprove = ddlActionButtons.FindButtonByValue(ToolBarCommmand.TOOLBAR_COMMAND_APPROVE);
        //    if (btnApprove != null)
        //    {
        //        btnApprove.PostBack = false;
        //        url = Page.ResolveClientUrl(URLConstants.URL_BULK_COMPLETE_TASK);
        //        para = PopupHelper.GetJavascriptParameterListForBulkStatusUpdate("OpenRadWindowWithName", url, "Bulk Complete Tasks", ARTEnums.TaskActionType.Approve, QueryStringConstants.EDIT
        //            , ARTEnums.TaskType.AccountTask, ARTEnums.RecordType.TaskComplition, null);
        //        btnApprove.Attributes.Add(TaskViewerConstants.TOOLBAR_COMMAND_ADD_ATTRIBUTE_NAME_ACCOUNT_TASK, para);
        //    }

        //    RadToolBarButton btnReject = ddlActionButtons.FindButtonByValue(ToolBarCommmand.TOOLBAR_COMMAND_REJECT);
        //    if (btnReject != null)
        //    {
        //        btnReject.PostBack = false;
        //        url = Page.ResolveClientUrl(URLConstants.URL_BULK_COMPLETE_TASK);
        //        para = PopupHelper.GetJavascriptParameterListForBulkStatusUpdate("OpenRadWindowWithName", url, "Bulk Complete Tasks", ARTEnums.TaskActionType.Reject, QueryStringConstants.EDIT
        //            , ARTEnums.TaskType.AccountTask, ARTEnums.RecordType.TaskComplition, null);
        //        btnReject.Attributes.Add(TaskViewerConstants.TOOLBAR_COMMAND_ADD_ATTRIBUTE_NAME_ACCOUNT_TASK, para);
        //    }

        //    RadToolBarButton btnDone = ddlActionButtons.FindButtonByValue(ToolBarCommmand.TOOLBAR_COMMAND_DONE);
        //    if (btnDone != null)
        //    {
        //        btnDone.PostBack = false;
        //        url = Page.ResolveClientUrl(URLConstants.URL_BULK_COMPLETE_TASK);
        //        para = PopupHelper.GetJavascriptParameterListForBulkStatusUpdate("OpenRadWindowWithName", url, "Bulk Complete Tasks", ARTEnums.TaskActionType.Complete, QueryStringConstants.EDIT
        //            , ARTEnums.TaskType.AccountTask, ARTEnums.RecordType.TaskComplition, null);
        //        btnDone.Attributes.Add(TaskViewerConstants.TOOLBAR_COMMAND_ADD_ATTRIBUTE_NAME_ACCOUNT_TASK, para);
        //    }
        //}
        this.PopulateSessionKeysForGrids();
        this.PageSettingLoadedEvent += new PageSettingLoaded(Pages_TaskMaster_TaskViewer_PageSettingLoadedEvent);
        this.NeedPageSettingEvent += new NeedPageSetting(Pages_TaskMaster_TaskViewer_NeedPageSettingEvent);

    }

    PageSettings Pages_TaskMaster_TaskViewer_NeedPageSettingEvent()
    {
        PageSettings oPageSettings = PageSettingHelper.GetPageSettings(WebEnums.ARTPages.TaskViewer);
        oPageSettings.ShowAccountHiddenTask = chkShowAccountHiddenTask.Checked;
        oPageSettings.ShowAllPendingAccountTask = chkShowAllPendingAccountTask.Checked;
        oPageSettings.ShowAllPendingGeneralTask = chkShowAllPendingGeneralTask.Checked;
        oPageSettings.ShowGeneralHiddenTask = chkShowGeneralHiddenTask.Checked;
        return oPageSettings;
    }

    void Pages_TaskMaster_TaskViewer_PageSettingLoadedEvent()
    {
        oPageSettings = this.PageSettings;
        if (oPageSettings != null)
        {
            chkShowAllPendingAccountTask.Checked = oPageSettings.ShowAllPendingAccountTask;
            chkShowAllPendingGeneralTask.Checked = oPageSettings.ShowAllPendingGeneralTask;
            chkShowGeneralHiddenTask.Checked = oPageSettings.ShowGeneralHiddenTask;
            chkShowAccountHiddenTask.Checked = oPageSettings.ShowAccountHiddenTask;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.SetPageTitle(this, 2541);
        if ((!Page.IsPostBack) && HttpContext.Current.Request.QueryString[QueryStringConstants.ACTIVE_TAB_INDEX] != null)
        {
            ViewState[ViewStateConstants.ACTIVE_TAB_INDEX] = HttpContext.Current.Request.QueryString[QueryStringConstants.ACTIVE_TAB_INDEX];
            //chkShowAllPendingAccountTask.Checked = true;
            //chkShowAllPendingGeneralTask.Checked = true;
            //chkShowGeneralHiddenTask.Checked = true;
            //chkShowAccountHiddenTask.Checked = true;
        }
        else
        {
            if (rtsTabs.SelectedIndex > 0)
                ViewState[ViewStateConstants.ACTIVE_TAB_INDEX] = rtsTabs.SelectedIndex.ToString();
            else
                ViewState[ViewStateConstants.ACTIVE_TAB_INDEX] = "0";
        }
        rtsTabs.SelectedIndex = Convert.ToInt32(ViewState[ViewStateConstants.ACTIVE_TAB_INDEX]);
        rmpTabPages.SelectedIndex = Convert.ToInt32(ViewState[ViewStateConstants.ACTIVE_TAB_INDEX]);
        if ((!Page.IsPostBack) && HttpContext.Current.Request.QueryString[QueryStringConstants.TASK_COMPLETION_STATUS_ID] != null)
            Session[SessionConstants.TASK_COMPLETION_STATUS_ID] = HttpContext.Current.Request.QueryString[QueryStringConstants.TASK_COMPLETION_STATUS_ID];


        if (!this.IsPostBack)
        {
            ListControlHelper.BindTaskCategoryDropDopwn(ddlTaskCategory);
        }
        //this.ucGeneraltaskGridPending.SetGridGroupByExpression();
        //this.ucGeneralTaskGridCompleted.SetGridGroupByExpression();
        this.ucAccountTaskGridPending.SetGridGroupByExpression();
        this.ucAccountTaskGridCompleted.SetGridGroupByExpression();
        ShowHideReopenBtn();

    }

    public override void RefreshPage(object sender, RefreshEventArgs args)
    {
        base.RefreshPage(sender, args);
        this.ompage_ReconciliationPeriodChangedEventHandler(null, null);
    }
    #endregion

    #region Postback Events
    protected void ucGeneraltaskGridPending_GridCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == "BulkClose")
        {

        }
        if (e.CommandName == TelerikConstants.GridClearFilterCommandName)
        {
            //SessionHelper.ClearDynamicFilterData(GetGridClientIDKey(rgGeneralTasks));
            //ApplyFilterGeneralTasksGrid();
            Session[SessionConstants.TASK_COMPLETION_STATUS_ID] = null;
            Session.Remove(this.sessionKeyGeneralTaskPending);
            this.PopulateGeneralTaskPending();
        }
        if (e.CommandName == "DeleteTaskLoad")
        {
            int DataImportID = Convert.ToInt32(e.CommandArgument);
            if (DataImportID > 0)
            {
                List<TaskHdrInfo> oTaskHdrInfoList = TaskMasterHelper.GetTasksByDataImportID(DataImportID);
                List<long> TaskIdList = oTaskHdrInfoList.Select(obj => obj.TaskID.Value).ToList();
                List<int> AllUserIDCollection = TaskMasterHelper.GetAllNotifyUserIDCollection(TaskIdList);
                List<DataImportHdrInfo> oDataImportHdrInfoList = TaskMasterHelper.DeleteTaskLoad(DataImportID, SessionHelper.CurrentUserLoginID, DateTime.Now);
                if (oDataImportHdrInfoList != null && oDataImportHdrInfoList.Count > 0)
                {
                    foreach (DataImportHdrInfo oDataImportHdrInfo in oDataImportHdrInfoList)
                    {
                        DataImportHelper.DeleteFile(oDataImportHdrInfo.PhysicalPath);
                    }
                }
                TaskMasterHelper.NotifyUsers(oTaskHdrInfoList, AllUserIDCollection, Helper.GetLabelIDValue(2686), Helper.GetLabelIDValue(2685));
                this.PopulateGeneralTaskPending();
            }

        }
    }

    protected void ucGeneralTaskGridCompleted_GridCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == "BulkClose")
        {

        }
        if (e.CommandName == TelerikConstants.GridClearFilterCommandName)
        {
            //SessionHelper.ClearDynamicFilterData(GetGridClientIDKey(rgGeneralTasks));
            //ApplyFilterGeneralTasksGrid();
            Session[SessionConstants.TASK_COMPLETION_STATUS_ID] = null;
            Session.Remove(this.sessionKeyGeneralTaskCompleted);
            this.PopulateGeneralTaskCompleted();
        }
        if (e.CommandName == TelerikConstants.GridApplyHideCommandName)
        {
            List<TaskHdrInfo> oListTaskHdrInfo = ucGeneralTaskGridCompleted.GetTaskHdrInfoListCollection;
            if (oListTaskHdrInfo != null && oListTaskHdrInfo.Count > 0)
                HideUnhideTask(oListTaskHdrInfo, ucGeneralTaskGridCompleted.Grid.SelectedItems, true);
        }
        if (e.CommandName == TelerikConstants.GridApplyUnhideCommandName)
        {
            List<TaskHdrInfo> oListTaskHdrInfo = ucGeneralTaskGridCompleted.GetTaskHdrInfoListCollection;
            if (oListTaskHdrInfo != null && oListTaskHdrInfo.Count > 0)
                HideUnhideTask(oListTaskHdrInfo, ucGeneralTaskGridCompleted.Grid.SelectedItems, false);
        }
        if (e.CommandName == TelerikConstants.GridApplyReopenCommandName)
        {
            List<TaskHdrInfo> oListTaskHdrInfo = ucGeneralTaskGridCompleted.GetTaskHdrInfoListCollection;
            if (oListTaskHdrInfo != null && oListTaskHdrInfo.Count > 0)
                ReopenTasks(oListTaskHdrInfo, ucGeneralTaskGridCompleted.Grid.SelectedItems, ARTEnums.TaskType.GeneralTask);
        }
        if (e.CommandName == TelerikConstants.GridApplyDeleteCommandName)
        {
            List<TaskHdrInfo> oListTaskHdrInfo = ucGeneralTaskGridCompleted.GetTaskHdrInfoListCollection;
            if (oListTaskHdrInfo != null && oListTaskHdrInfo.Count > 0)
                DeleteTasks(oListTaskHdrInfo, ucGeneralTaskGridCompleted.Grid.SelectedItems, ARTEnums.TaskType.GeneralTask);
        }
    }

    protected void ucAccountTaskGridPending_GridCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == TelerikConstants.GridClearFilterCommandName)
        {
            Session[SessionConstants.TASK_COMPLETION_STATUS_ID] = null;
            Session.Remove(this.sessionKeyAccountTaskPending);
            this.PopulateAccountTaskPending();
        }
        if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
        {
            ucAccountTaskGridPending.isExportPDF = true;
        }
        if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
        {
            ucAccountTaskGridPending.isExportExcel = true;
        }
    }

    protected void ucAccountTaskGridCompleted_GridCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == TelerikConstants.GridClearFilterCommandName)
        {
            Session[SessionConstants.TASK_COMPLETION_STATUS_ID] = null;
            Session.Remove(this.sessionKeyAccountTaskCompleted);
            this.PopulateAccountTaskCompleted();
        }
        if (e.CommandName == TelerikConstants.GridApplyHideCommandName)
        {
            List<TaskHdrInfo> oListTaskHdrInfo = ucAccountTaskGridCompleted.GetTaskHdrInfoListCollection;
            if (oListTaskHdrInfo != null && oListTaskHdrInfo.Count > 0)
                HideUnhideTask(oListTaskHdrInfo, ucAccountTaskGridCompleted.Grid.SelectedItems, true);
        }
        if (e.CommandName == TelerikConstants.GridApplyUnhideCommandName)
        {
            List<TaskHdrInfo> oListTaskHdrInfo = ucAccountTaskGridCompleted.GetTaskHdrInfoListCollection;
            if (oListTaskHdrInfo != null && oListTaskHdrInfo.Count > 0)
                HideUnhideTask(oListTaskHdrInfo, ucAccountTaskGridCompleted.Grid.SelectedItems, false);
        }
        if (e.CommandName == TelerikConstants.GridApplyReopenCommandName)
        {
            List<TaskHdrInfo> oListTaskHdrInfo = ucAccountTaskGridCompleted.GetTaskHdrInfoListCollection;
            if (oListTaskHdrInfo != null && oListTaskHdrInfo.Count > 0)
                ReopenTasks(oListTaskHdrInfo, ucAccountTaskGridCompleted.Grid.SelectedItems, ARTEnums.TaskType.AccountTask);
        }
        if (e.CommandName == TelerikConstants.GridApplyDeleteCommandName)
        {
            List<TaskHdrInfo> oListTaskHdrInfo = ucAccountTaskGridCompleted.GetTaskHdrInfoListCollection;
            if (oListTaskHdrInfo != null && oListTaskHdrInfo.Count > 0)
                DeleteTasks(oListTaskHdrInfo, ucAccountTaskGridCompleted.Grid.SelectedItems, ARTEnums.TaskType.AccountTask);
        }
        if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
        {
            ucAccountTaskGridCompleted.isExportPDF = true;
        }
        if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
        {
            ucAccountTaskGridCompleted.isExportExcel = true;
        }

    }

    private void HideUnhideTask(List<TaskHdrInfo> oListTaskHdrInfo, GridItemCollection gridItemCollection, bool IsHidden)
    {
        try
        {
            List<TaskHdrInfo> oTaskHdrInfoCollection = new List<TaskHdrInfo>();
            foreach (GridDataItem item in gridItemCollection)
            {
                Int64 taskID = int.Parse(item.GetDataKeyValue("TaskID").ToString());
                Int64? taskDetailID = null;
                if (item.GetDataKeyValue("TaskDetailID") != null)
                {
                    taskDetailID = int.Parse(item.GetDataKeyValue("TaskDetailID").ToString());
                }
                oTaskHdrInfoCollection.Add((TaskHdrInfo)oListTaskHdrInfo.Find(T => T.TaskID == taskID && T.TaskDetailID == taskDetailID));
            }
            if (oTaskHdrInfoCollection != null && oTaskHdrInfoCollection.Count > 0)
                TaskMasterHelper.AddEditUserTaskVisibility(oTaskHdrInfoCollection, SessionHelper.CurrentUserID, SessionHelper.CurrentReconciliationPeriodID, SessionHelper.CurrentUserLoginID, SessionHelper.CurrentUserLoginID, IsHidden);
            PopulateGeneralTaskCompleted();
            PopulateAccountTaskCompleted();
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

    private void ReopenTasks(List<TaskHdrInfo> oListTaskHdrInfo, GridItemCollection gridItemCollection, ARTEnums.TaskType eTaskType)
    {
        try
        {
            List<long> oTaskDetailIDList = new List<long>();
            foreach (GridDataItem item in gridItemCollection)
            {
                Int64 taskID = int.Parse(item.GetDataKeyValue("TaskID").ToString());
                Int64? taskDetailID = null;
                if (item.GetDataKeyValue("TaskDetailID") != null)
                {
                    taskDetailID = Int64.Parse(item.GetDataKeyValue("TaskDetailID").ToString());
                    if (taskDetailID.GetValueOrDefault() > 0)
                        oTaskDetailIDList.Add(taskDetailID.Value);
                }
            }
            if (oTaskDetailIDList != null && oTaskDetailIDList.Count > 0)
                TaskMasterHelper.UpdateTaskStatus(oTaskDetailIDList, ARTEnums.TaskActionType.Reopen);
            if (eTaskType == ARTEnums.TaskType.GeneralTask)
            {
                PopulateGeneralTaskCompleted();
                PopulateGeneralTaskPending();
            }
            else
            {
                PopulateAccountTaskCompleted();
                PopulateAccountTaskPending();
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

    private void DeleteTasks(List<TaskHdrInfo> oListTaskHdrInfo, GridItemCollection gridItemCollection, ARTEnums.TaskType eTaskType)
    {
        try
        {
            List<long> oTaskDetailIDList = new List<long>();
            List<long> oTaskIDList = new List<long>();
            foreach (GridDataItem item in gridItemCollection)
            {
                Int64 taskID = int.Parse(item.GetDataKeyValue("TaskID").ToString());
                Int32 recPeriodID = int.Parse(item.GetDataKeyValue("RecPeriodID").ToString());
                Int64? taskDetailID = null;
                Int16? taskStatusID = null;
                if (item.GetDataKeyValue("TaskDetailID") != null && item.GetDataKeyValue("TaskStatusID") != null)
                {
                    taskDetailID = Int64.Parse(item.GetDataKeyValue("TaskDetailID").ToString());
                    taskStatusID = Int16.Parse(item.GetDataKeyValue("TaskStatusID").ToString());
                    if (taskDetailID.GetValueOrDefault() > 0 && taskStatusID.GetValueOrDefault() == (short)ARTEnums.TaskStatus.Deleted)
                        oTaskDetailIDList.Add(taskDetailID.Value);
                }
                else
                {
                    if (recPeriodID == SessionHelper.CurrentReconciliationPeriodID.Value)
                        oTaskIDList.Add(taskID);
                }
            }
            if (oTaskDetailIDList != null && oTaskDetailIDList.Count > 0)
                TaskMasterHelper.UpdateTaskStatus(oTaskDetailIDList, ARTEnums.TaskActionType.RemoveDeleted);
            if (oTaskIDList != null && oTaskIDList.Count > 0)
                TaskMasterHelper.DeleteTasks(oTaskIDList, SessionHelper.CurrentCompanyID.Value, SessionHelper.CurrentUserLoginID, DateTime.Now);
            if (eTaskType == ARTEnums.TaskType.GeneralTask)
            {
                PopulateGeneralTaskCompleted();
                PopulateGeneralTaskPending();
            }
            else
            {
                PopulateAccountTaskCompleted();
                PopulateAccountTaskPending();
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
            this.PopulateGeneralTaskPending();
            this.PopulateGeneralTaskCompleted();
            this.PopulateAccountTaskPending();
            this.PopulateAccountTaskCompleted();
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

    protected void ddlTaskCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.PopulateGeneralTaskPending();
            this.PopulateGeneralTaskCompleted();
            this.PopulateAccountTaskPending();
            this.PopulateAccountTaskCompleted();
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
    #endregion

    #region "Private Methods"
    private void ShowHideReopenBtn()
    {
        if (SessionHelper.CurrentRoleID.GetValueOrDefault() == (short)ARTEnums.UserRole.SYSTEM_ADMIN)
        {
            ucGeneralTaskGridCompleted.AllowCustomReopen = true;
            ucAccountTaskGridCompleted.AllowCustomReopen = true;
        }
        else
        {
            ucGeneralTaskGridCompleted.AllowCustomReopen = false;
            ucAccountTaskGridCompleted.AllowCustomReopen = false;
        }
    }
    private List<TaskHdrInfo> GetDataForTaskGrid(ARTEnums.Grid grid, string gridSessionKeyForFilter, bool showHidden)
    {
        short? TaskCompletionStatus = null;

        if (Session[SessionConstants.TASK_COMPLETION_STATUS_ID] != null)
            TaskCompletionStatus = Convert.ToInt16(Session[SessionConstants.TASK_COMPLETION_STATUS_ID]);

        int userID = SessionHelper.CurrentUserID.GetValueOrDefault();
        short roleID = SessionHelper.CurrentRoleID.GetValueOrDefault();
        int recPeriodID = SessionHelper.CurrentReconciliationPeriodID.GetValueOrDefault();
        ARTEnums.TaskType? taskType = null;
        List<TaskHdrInfo> TaskHdrInfoList = null;
        short? taskCategoryID = null;
        bool? isshowHidden = showHidden;

        //Filter Criteria
        List<FilterCriteria> oFilterCriteriaCollection = null;
        if (!String.IsNullOrEmpty(gridSessionKeyForFilter))
            oFilterCriteriaCollection = (List<FilterCriteria>)Session[gridSessionKeyForFilter];

        short gridType = (short)grid;
        List<TaskStatusMstInfo> taskStatusInfoList = new List<TaskStatusMstInfo>();

        taskCategoryID = Convert.ToInt16(this.ddlTaskCategory.SelectedValue);
        //isshowHidden = false;

        switch (gridType)
        {
            case (short)ARTEnums.Grid.GeneralTaskPending:
                taskType = ARTEnums.TaskType.GeneralTask;
                TaskHdrInfoList = TaskMasterHelper.GetPendingOverdueAccessableTaskByUserID(userID, roleID, recPeriodID, taskType.Value, oFilterCriteriaCollection, taskCategoryID, isshowHidden, TaskCompletionStatus, chkShowAllPendingGeneralTask.Checked);
                break;

            case (short)ARTEnums.Grid.GeneralTaskCompleted:
                taskType = ARTEnums.TaskType.GeneralTask;
                TaskHdrInfoList = TaskMasterHelper.GetCompletedAccessableTaskByUserID(userID, roleID, recPeriodID, taskType.Value, oFilterCriteriaCollection, taskCategoryID, isshowHidden);
                break;

            case (short)ARTEnums.Grid.AccountTaskPending:
                taskType = ARTEnums.TaskType.AccountTask;
                TaskHdrInfoList = TaskMasterHelper.GetPendingOverdueAccessableTaskByUserID(userID, roleID, recPeriodID, taskType.Value, oFilterCriteriaCollection, taskCategoryID, isshowHidden, TaskCompletionStatus, chkShowAllPendingAccountTask.Checked);
                break;

            case (short)ARTEnums.Grid.AccountTaskCompleted:
                taskType = ARTEnums.TaskType.AccountTask;
                TaskHdrInfoList = TaskMasterHelper.GetCompletedAccessableTaskByUserID(userID, roleID, recPeriodID, taskType.Value, oFilterCriteriaCollection, taskCategoryID, isshowHidden);
                break;
        }

        return TaskHdrInfoList;
    }

    private void PopulateGeneralTaskPending()
    {
        //Open/pending tasks
        bool isShowHidden = false;
        List<TaskHdrInfo> oOpenGeneralTaskHdrinfoList = this.GetDataForTaskGrid(ARTEnums.Grid.GeneralTaskPending, this.sessionKeyGeneralTaskPending, isShowHidden);
        this.ucGeneraltaskGridPending.SetGeneralTaskGridData(oOpenGeneralTaskHdrinfoList);
        ucGeneraltaskGridPending.LoadGridData();
        this.LoadPageSettings();
    }

    private void PopulateGeneralTaskCompleted()
    {
        //Completed task
        bool isShowHidden = chkShowGeneralHiddenTask.Checked;
        ucGeneralTaskGridCompleted.GridApplyDeleteOnClientClick = "return ConfirmDeletion('" + LanguageUtil.GetValue(2968) + "');";
        List<TaskHdrInfo> oOpenGeneralTaskHdrinfoList = this.GetDataForTaskGrid(ARTEnums.Grid.GeneralTaskCompleted, this.sessionKeyGeneralTaskCompleted, isShowHidden);
        this.ucGeneralTaskGridCompleted.SetGeneralTaskGridData(oOpenGeneralTaskHdrinfoList);
        ucGeneralTaskGridCompleted.LoadGridData();
        this.LoadPageSettings();
        ShowHideReopenBtn();

    }

    private void PopulateAccountTaskPending()
    {
        bool isShowHidden = false;
        List<TaskHdrInfo> oOpenAccountTaskHdrInfoList = this.GetDataForTaskGrid(ARTEnums.Grid.AccountTaskPending, this.sessionKeyAccountTaskPending, isShowHidden);
        this.ucAccountTaskGridPending.SetAccountTaskGridData(oOpenAccountTaskHdrInfoList);
        this.ucAccountTaskGridPending.LoadGridData();
        this.LoadPageSettings();


    }

    private void PopulateAccountTaskCompleted()
    {
        bool isShowHidden = chkShowAccountHiddenTask.Checked;
        ucAccountTaskGridCompleted.GridApplyDeleteOnClientClick = "return ConfirmDeletion('" + LanguageUtil.GetValue(2968) + "');";
        List<TaskHdrInfo> oCompletedAccountTaskHdrInfoList = this.GetDataForTaskGrid(ARTEnums.Grid.AccountTaskCompleted, this.sessionKeyAccountTaskCompleted, isShowHidden);
        this.ucAccountTaskGridCompleted.SetAccountTaskGridData(oCompletedAccountTaskHdrInfoList);
        this.ucAccountTaskGridCompleted.LoadGridData();
        this.LoadPageSettings();
        ShowHideReopenBtn();
    }

    private void PopulateSessionKeysForGrids()
    {
        this.sessionKeyGeneralTaskPending = SessionHelper.GetSessionKeyForGridFilter(this.ucGeneraltaskGridPending.GridType);
        this.sessionKeyGeneralTaskCompleted = SessionHelper.GetSessionKeyForGridFilter(this.ucGeneralTaskGridCompleted.GridType);
        this.sessionKeyAccountTaskPending = SessionHelper.GetSessionKeyForGridFilter(this.ucAccountTaskGridPending.GridType);
        this.sessionKeyAccountTaskCompleted = SessionHelper.GetSessionKeyForGridFilter(this.ucAccountTaskGridCompleted.GridType);

    }

    protected void chkShowHiddenTask_OnCheckedChanged(object sender, EventArgs e)
    {
        this.SavePageSettings();
        PopulateGeneralTaskCompleted();
        PopulateAccountTaskCompleted();

    }
    protected void chkShowAllPendingAccountTask_OnCheckedChanged(object sender, EventArgs e)
    {
        this.SavePageSettings();
        this.PopulateAccountTaskPending();

    }
    protected void chkShowAllPendingGeneralTask_OnCheckedChanged(object sender, EventArgs e)
    {
        this.SavePageSettings();
        this.PopulateGeneralTaskPending();

    }
    #endregion

    public override string GetMenuKey()
    {
        return "TaskViewer";
    }
}
