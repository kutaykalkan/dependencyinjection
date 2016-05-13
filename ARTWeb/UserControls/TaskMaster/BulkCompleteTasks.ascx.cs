using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes.UserControl;
using SkyStem.ART.Client.Model;
using Telerik.Web.UI;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Web.Utility;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.IServices;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Web.Classes;

public partial class UserControls_TaskMaster_BulkCompleteTasks : UserControlTaskMasterBase
{
    #region Variables

    private const string TEXTBOX_ONBLUR_VALUE = "HideProgressBar('{0}')";
    const string POPUP_PAGE = "../AddUserMailOnDataImport.aspx";
    string _PopupUrlNotify = string.Empty;


    long? _taskID = null;
    string _taskDetailIDs = string.Empty;
    bool _showGeneralTaskGrid = false;
    bool _showAcctTaskGrid = false;
    long? _accountID = null;
    int? _netAccountID = null;

    List<long> _getSelectedTaskDetailIDs = null;

    short? _taskTypeID = null;
    short? _taskActionTypeID = null;

    List<string> generalGridHideColumnList;
    List<string> acctGridHideColumnList;

    #endregion

    #region Properties

    public long? TaskID
    {
        get { return _taskID; }
        set { _taskID = value; }
    }

    public string TaskDetailIDs
    {
        get { return _taskDetailIDs; }
        set { _taskDetailIDs = value; }
    }

    public bool ShowGeneralTaskGrid
    {
        get { return _showGeneralTaskGrid; }
        set { _showGeneralTaskGrid = value; }
    }

    public bool ShowAcctTaskGrid
    {
        get { return _showAcctTaskGrid; }
        set { _showAcctTaskGrid = value; }
    }
    public List<long> GetSelectedTaskDetailIDs
    {
        get
        {
            if (ShowGeneralTaskGrid)
            {
                _getSelectedTaskDetailIDs = ucGeneralTaskGrid.SelectedTaskIDs();
            }
            else
            {
                _getSelectedTaskDetailIDs = new List<long>();
            }
            return _getSelectedTaskDetailIDs;
        }
        set { _getSelectedTaskDetailIDs = value; }
    }
    public long? AccountID
    {
        get { return _accountID; }
        set { _accountID = value; }
    }
    public int? NetAccountID
    {
        get { return _netAccountID; }
        set { _netAccountID = value; }
    }
    public List<TaskHdrInfo> DataSource
    {
        get
        {
            if (Session[GetUniqueSessionKey() + "_List"] == null)
                return null;
            return Session[GetUniqueSessionKey() + "_List"] as List<TaskHdrInfo>;
        }
        set
        {
            Session[GetUniqueSessionKey() + "_List"] = value;
        }
    }

    #endregion

    #region Page Events

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.TASK_ACTION_TYPE_ID]))
        {
            _taskActionTypeID = Convert.ToInt16(Request.QueryString[QueryStringConstants.TASK_ACTION_TYPE_ID]);
        }

        ARTEnums.TaskActionType _taskActionType = (ARTEnums.TaskActionType)_taskActionTypeID;
        switch (_taskActionType)
        {

            case ARTEnums.TaskActionType.Complete:
                pnlcomment.Visible = true;
                pnlUserAssignment.Visible = false;
                btnDeletePermanent.Visible = false;
                btnSave.Visible = true;
                generalGridHideColumnList = new List<string>() { "AttachmentCount", "CompletionDate", "DateCreated", "RevisedBy", "RevisedBy", "Edit" };

                acctGridHideColumnList = new List<string>() { "AttachmentCount", "FSCaption", "AccountType", "TaskDuration", "RecurrenceType", "ApproverComment",
                                                                "TaskOwner", "TaskApprover", "ApprovalDuration", "CompletionDate", "CompletionDocs","CompletionComment", 
                                                                "CreatedBy", "DateCreated", "RevisedBy", "DateRevised", "Edit"};
                btnMarkComplete.Text = LanguageUtil.GetValue(1238);
                break;

            case ARTEnums.TaskActionType.Reopen:
            case ARTEnums.TaskActionType.Review:
            case ARTEnums.TaskActionType.Approve:
            case ARTEnums.TaskActionType.Reject:
            case ARTEnums.TaskActionType.Add:
                btnSave.Visible = false;
                pnlcomment.Visible = true;
                pnlUserAssignment.Visible = false;
                btnDeletePermanent.Visible = false;
                generalGridHideColumnList = new List<string>() { "AttachmentCount", "CompletionDate", "DateCreated", "RevisedBy", "RevisedBy", "Edit" };

                acctGridHideColumnList = new List<string>() { "AttachmentCount", "FSCaption", "AccountType", "TaskDuration", "RecurrenceType", "ApproverComment",
                                                                "TaskOwner", "TaskApprover", "ApprovalDuration", "CompletionDate", "CompletionDocs","CompletionComment", 
                                                                "CreatedBy", "DateCreated", "RevisedBy", "DateRevised", "Edit"};
                btnMarkComplete.Text = LanguageUtil.GetValue(1238);
                break;
            case ARTEnums.TaskActionType.BulkEdit:
                AddHideColumnsForEditDelete();
                btnSave.Visible = false;
                pnlcomment.Visible = false;
                btnDeletePermanent.Visible = false;
                pnlUserAssignment.Visible = true;
                generalGridHideColumnList = new List<string>() { "TaskStatus", " StartDate", "AssigneeDueDate", "DueDate", "TaskDuration", "Comment", "DateRevised", "AttachmentCount", "CompletionDate", "DateCreated", "RevisedBy", "RevisedBy", "Edit" };

                acctGridHideColumnList = new List<string>() { "TaskStatus"," StartDate","AssigneeDueDate","DueDate","TaskDuration","Comment","DateRevised","AttachmentCount", "FSCaption", "AccountType", "TaskDuration", "RecurrenceType", "ApproverComment",
                                                                "TaskOwner", "TaskApprover", "ApprovalDuration", "CompletionDate", "CompletionDocs","CompletionComment", 
                                                                "CreatedBy", "DateCreated", "RevisedBy", "DateRevised", "Edit"};
                btnMarkComplete.Text = LanguageUtil.GetValue(1238);
                break;
            case ARTEnums.TaskActionType.Remove:
                AddHideColumnsForEditDelete();
                btnSave.Visible = false;
                btnDeletePermanent.Attributes.Add("onclick", "return ConfirmDeletion('" + LanguageUtil.GetValue(2684) + "');");
                btnDeletePermanent.Visible = true;
                pnlcomment.Visible = false;
                pnlUserAssignment.Visible = false;
                generalGridHideColumnList = new List<string>() { "TaskStatus", " StartDate", "AssigneeDueDate", "DueDate", "TaskDuration", "Comment", "DateRevised", "AttachmentCount", "CompletionDate", "DateCreated", "RevisedBy", "RevisedBy", "Edit" };

                acctGridHideColumnList = new List<string>() { "TaskStatus"," StartDate","AssigneeDueDate","DueDate","TaskDuration","Comment","DateRevised","AttachmentCount", "FSCaption", "AccountType", "TaskDuration", "RecurrenceType", "ApproverComment",
                                                                "TaskOwner", "TaskApprover", "ApprovalDuration", "CompletionDate", "CompletionDocs","CompletionComment", 
                                                                "CreatedBy", "DateCreated", "RevisedBy", "DateRevised", "Edit"};
                btnMarkComplete.Text = LanguageUtil.GetValue(2683);
                break;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        var lblError = this.Page.Master.FindControl("lblErrorMessage") as ExLabel;
        if (lblError != null)
        {
            lblError.Text = string.Empty;
            lblError.Visible = false;
        }

        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.TASK_TYPE_ID]))
        {
            _taskTypeID = Convert.ToInt16(Request.QueryString[QueryStringConstants.TASK_TYPE_ID]);
        }
        //if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.TASK_ACTION_TYPE_ID]))
        //{
        //    _taskActionTypeID = Convert.ToInt16(Request.QueryString[QueryStringConstants.TASK_ACTION_TYPE_ID]);
        //}

        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.ACCOUNT_ID]))
        {
            _accountID = Convert.ToInt64(Request.QueryString[QueryStringConstants.ACCOUNT_ID]);
        }

        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.NETACCOUNT_ID]))
        {
            _netAccountID = Convert.ToInt32(Request.QueryString[QueryStringConstants.NETACCOUNT_ID]);
        }
        if (_taskTypeID.HasValue)
        {
            if (_taskTypeID == (short)ARTEnums.TaskType.GeneralTask || (ARTEnums.TaskActionType)_taskActionTypeID == ARTEnums.TaskActionType.Remove)
            {
                ShowAcctTaskGrid = false;
                ShowGeneralTaskGrid = true;
            }
            else if (_taskTypeID == (short)ARTEnums.TaskType.AccountTask)
            {
                ShowAcctTaskGrid = true;
                ShowGeneralTaskGrid = false;
            }
        }
        txtAssignedTo.Attributes.Add("readonly", "readonly");
        txtReviewer.Attributes.Add("readonly", "readonly");
        txtApprover.Attributes.Add("readonly", "readonly");
        txtNotify.Attributes.Add("readonly", "readonly");
        btnClearAssignedTo.Attributes.Add("onclick", "return ConfirmDeletion('" + GetReplacementValue(2649) + "','" + (short)ARTEnums.TaskAttribute.AssignedTo + "');");
        btnClearReviewer.Attributes.Add("onclick", "return ConfirmDeletion('" + GetReplacementValue(1131) + "','" + (short)ARTEnums.TaskAttribute.Reviewer + "');");
        btnClearApprover.Attributes.Add("onclick", "return ConfirmDeletion('" + GetReplacementValue(1132) + "','" + (short)ARTEnums.TaskAttribute.Approver + "');");
        btnClearNotify.Attributes.Add("onclick", "return ConfirmDeletion('" + GetReplacementValue(2525) + "','" + (short)ARTEnums.TaskAttribute.Notify + "');");
        hlAssignedTo.NavigateUrl = GetPopupUrlForUser((short)ARTEnums.TaskAttribute.AssignedTo);
        hlReviewer.NavigateUrl = GetPopupUrlForUser((short)ARTEnums.TaskAttribute.Reviewer);
        hlApprover.NavigateUrl = GetPopupUrlForUser((short)ARTEnums.TaskAttribute.Approver);
        hlNotify.NavigateUrl = GetPopupUrlForUser((short)ARTEnums.TaskAttribute.Notify);
        SetErrorMessages();
        if (!Page.IsPostBack)
        {
            DataSource = null;
            Session[SessionConstants.TASK_MASTER_ATTACHMENT] = null;
            LoadData();
            SetGridsProperties(ShowAcctTaskGrid, ShowGeneralTaskGrid);
        }
    }

    #endregion

    #region Control's Events

    #region Grid Events


    void ucGeneralTaskGrid_GridItemDataBound(object sender, GridItemEventArgs e)
    {
        //CheckBox checkBox = (CheckBox)(e.Item as GridDataItem)["CheckboxSelectColumn"].Controls[0];
        //if (checkBox != null)
        //{
        //    checkBox.Checked = true;
        //}
    }
    #endregion

    protected void btnClearAssignedTo_Click(object sender, ImageClickEventArgs e)
    {
        hdnAssignedTo.Value = "";
        txtAssignedTo.Text = "";
    }
    protected void btnClearReviewer_Click(object sender, ImageClickEventArgs e)
    {
        hdnReviewer.Value = "";
        txtReviewer.Text = "";
    }
    protected void btnClearApprover_Click(object sender, ImageClickEventArgs e)
    {
        hdnApprover.Value = "";
        txtApprover.Text = "";
    }
    protected void btnClearNotify_Click(object sender, ImageClickEventArgs e)
    {
        hdnNotify.Value = "";
        txtNotify.Text = "";
    }

    protected void btnMarkComplete_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (BulkUpdateTasksStatus())
            {
                Session[SessionConstants.TASK_MASTER_ATTACHMENT] = null;
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopupAndRefreshParentPage", ScriptHelper.GetJSForClosePopupAndSubmitParentPage(true));
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (BulkSaveTasksStatus())
        {
            Session[SessionConstants.TASK_MASTER_ATTACHMENT] = null;
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopupAndRefreshParentPage", ScriptHelper.GetJSForClosePopupAndSubmitParentPage(true));
        }

    }

    protected void btnDeletePermanent_Click(object sender, EventArgs e)
    {
        try
        {
            List<long> _selectedIDs = null;
            if (ShowAcctTaskGrid)
                _selectedIDs = ucAccountTaskGrid.GetSelectedTaskIDList(SessionHelper.CurrentReconciliationPeriodID.Value);
            else if (ShowGeneralTaskGrid)
                _selectedIDs = ucGeneralTaskGrid.GetSelectedTaskIDList(SessionHelper.CurrentReconciliationPeriodID.Value);


            if (_selectedIDs.Count > 0)
            {
                List<int> AllUserIDCollection = TaskMasterHelper.GetAllNotifyUserIDCollection(_selectedIDs);
                List<TaskHdrInfo> oSelectedTaskHdrInfoList = (from objTaskHdrInfo in DataSource
                                                              join oTaskID in _selectedIDs on objTaskHdrInfo.TaskID equals oTaskID
                                                              select objTaskHdrInfo).ToList();


                List<DataImportHdrInfo> oDataImportHdrInfoList = null;
                if (SessionHelper.CurrentCompanyID.HasValue)
                    oDataImportHdrInfoList = TaskMasterHelper.DeleteTasks(_selectedIDs, SessionHelper.CurrentCompanyID.Value, SessionHelper.CurrentUserLoginID, DateTime.Now);
                if (oDataImportHdrInfoList != null && oDataImportHdrInfoList.Count > 0)
                {
                    foreach (DataImportHdrInfo oDataImportHdrInfo in oDataImportHdrInfoList)
                    {
                        DataImportHelper.DeleteFile(oDataImportHdrInfo.PhysicalPath);
                    }
                }
                TaskMasterHelper.NotifyUsers(oSelectedTaskHdrInfoList, AllUserIDCollection, Helper.GetLabelIDValue(2686), Helper.GetLabelIDValue(2685));
                Session[SessionConstants.TASK_MASTER_ATTACHMENT] = null;
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopupAndRefreshParentPage", ScriptHelper.GetJSForClosePopupAndSubmitParentPage(true));
            }
            else
            {
                Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, LanguageUtil.GetValue(2757));
            }
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage((PopupPageBase)this.Page, ex);
        }
    }
    #endregion

    #region Public Functions

    internal bool BulkUpdateTasksStatus()
    {
        bool flag = false;
        if (Page.IsValid)
        {
            List<long> _selectedIDs = null;
            string _subjectLine = string.Empty;
            string _comment = string.Empty;
            try
            {
                if (ShowAcctTaskGrid)
                    _selectedIDs = ucAccountTaskGrid.GetSelectedTaskIDList();
                else if (ShowGeneralTaskGrid)
                    _selectedIDs = ucGeneralTaskGrid.GetSelectedTaskIDList();

                if (_selectedIDs != null && _selectedIDs.Count > 0)
                {
                    ARTEnums.TaskActionType? _taskActionType = (ARTEnums.TaskActionType)_taskActionTypeID;
                    if (_taskActionType.HasValue)
                    {
                        if (_taskActionType == ARTEnums.TaskActionType.Remove || _taskActionType == ARTEnums.TaskActionType.BulkEdit)
                        {


                            //oTaskMaster.DeleteAccessibleTasks(taskIDList, SessionHelper.CurrentReconciliationPeriodID.Value, SessionHelper.CurrentUserLoginID, DateTime.Now);
                            flag = UpdateTask(_selectedIDs, _taskActionType);

                        }
                        else
                        {
                            List<AttachmentInfo> newlyAddedAttachment = null;
                            var attachmentList = Session[SessionConstants.TASK_MASTER_ATTACHMENT] as List<AttachmentInfo>;

                            if (attachmentList != null && attachmentList.Count > 0)
                            {
                                newlyAddedAttachment = (from at in attachmentList
                                                        where (at.AttachmentID == null || at.AttachmentID == 0)
                                                        select at).ToList();
                            }

                            List<long> taskDetailIDList = null;
                            if (ShowAcctTaskGrid)
                                taskDetailIDList = ucAccountTaskGrid.GetSelectedTaskDetailIDList();
                            else if (ShowGeneralTaskGrid)
                                taskDetailIDList = ucGeneralTaskGrid.GetSelectedTaskDetailIDList();

                            TaskMasterHelper.UpdateTaskStatusCommentAttachments(taskDetailIDList, (ARTEnums.TaskActionType)_taskActionTypeID
                                , _subjectLine, ucAddTaskComment.Text, newlyAddedAttachment);

                            //Process alert for reject by approver and done by Assignee

                            List<TaskHdrInfo> oSelectedTaskHdrInfoList = (from oTaskHdrInfo in DataSource
                                                                          join oTaskID in _selectedIDs on oTaskHdrInfo.TaskID equals oTaskID
                                                                          select oTaskHdrInfo).ToList();

                            List<TaskHdrInfo> oSelectedTaskHdrInfoListForAlert;
                            List<TaskHdrInfo> oSelectedTaskHdrInfoListForRejectAlert_AssignedTo;
                            List<TaskHdrInfo> oSelectedTaskHdrInfoListForRejectAlert_Reviewer;

                            List<int> assignedToIDCollection = new List<int>();
                            List<int> ReviewerIDCollection = new List<int>();
                            List<int> approverIDCollection = new List<int>();

                            if (_taskActionType == ARTEnums.TaskActionType.Reject)
                            {
                                oSelectedTaskHdrInfoListForRejectAlert_AssignedTo = oSelectedTaskHdrInfoList.FindAll(o => o.AssignedTo != null && o.AssignedTo.Count > 0 && o.TaskStatusID.HasValue && o.TaskStatusID.Value == (short)ARTEnums.TaskStatus.PendReview);
                                foreach (TaskHdrInfo oSelectedTaskHdrInfo in oSelectedTaskHdrInfoListForRejectAlert_AssignedTo)
                                {
                                    if (oSelectedTaskHdrInfo.Reviewer != null)
                                        assignedToIDCollection.AddRange(oSelectedTaskHdrInfo.AssignedTo.Where(obj => obj.UserID.HasValue).Select(x => x.UserID.Value).Distinct());
                                }
                                //Send alert to Assignee for task rejection
                                AlertHelper.RaiseAlertForRejecteddTask(assignedToIDCollection, oSelectedTaskHdrInfoListForRejectAlert_AssignedTo);
                                oSelectedTaskHdrInfoListForRejectAlert_Reviewer = oSelectedTaskHdrInfoList.FindAll(o => o.Reviewer != null && o.Reviewer.Count > 0 && o.TaskStatusID.HasValue && o.TaskStatusID.Value == (short)ARTEnums.TaskStatus.PendApproval);
                                foreach (TaskHdrInfo oSelectedTaskHdrInfo in oSelectedTaskHdrInfoListForRejectAlert_Reviewer)
                                {
                                    if (oSelectedTaskHdrInfo.Reviewer != null)
                                        ReviewerIDCollection.AddRange(oSelectedTaskHdrInfo.Reviewer.Where(obj => obj.UserID.HasValue).Select(x => x.UserID.Value).Distinct());
                                }
                                //Send alert to Reviewer for task rejection
                                AlertHelper.RaiseAlertForRejecteddTask(ReviewerIDCollection, oSelectedTaskHdrInfoListForRejectAlert_Reviewer);
                            }
                            if (_taskActionType == ARTEnums.TaskActionType.Complete)
                            {
                                oSelectedTaskHdrInfoListForAlert = oSelectedTaskHdrInfoList.FindAll(o => o.Reviewer != null && o.Reviewer.Count > 0);
                                foreach (TaskHdrInfo oSelectedTaskHdrInfo in oSelectedTaskHdrInfoListForAlert)
                                {
                                    if (oSelectedTaskHdrInfo.Reviewer != null)
                                        ReviewerIDCollection.AddRange(oSelectedTaskHdrInfo.Reviewer.Where(obj => obj.UserID.HasValue).Select(x => x.UserID.Value).Distinct());
                                }
                                //Send alert to Reviewer for task approval( Task is to be reviewed by Reviewer)
                                AlertHelper.RaiseAlertForReviewer(ReviewerIDCollection, oSelectedTaskHdrInfoListForAlert);
                            }
                            if (_taskActionType == ARTEnums.TaskActionType.Review)
                            {
                                oSelectedTaskHdrInfoListForAlert = oSelectedTaskHdrInfoList.FindAll(o => o.Approver != null && o.Approver.Count > 0);
                                foreach (TaskHdrInfo oSelectedTaskHdrInfo in oSelectedTaskHdrInfoListForAlert)
                                {
                                    if (oSelectedTaskHdrInfo.Approver != null)
                                        approverIDCollection.AddRange(oSelectedTaskHdrInfo.Approver.Where(obj => obj.UserID.HasValue).Select(x => x.UserID.Value).Distinct());
                                }
                                //Send alert to Approver for task approval( Task is to be approved by Approver)
                                AlertHelper.RaiseAlertForApproval(approverIDCollection, oSelectedTaskHdrInfoListForAlert);
                            }
                            flag = true;
                        }

                        if (flag)
                            Session[SessionConstants.TASK_MASTER_ATTACHMENT] = null;
                    }
                }
                else
                {
                    flag = false;
                    Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, LanguageUtil.GetValue(2013));
                }
            }
            catch (Exception ex)
            {
                flag = false;
                Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, ex);
            }
            finally
            {
                _selectedIDs = null;
            }
        }
        return flag;

    }

    internal bool BulkSaveTasksStatus()
    {
        bool flag = false;
        List<long> _selectedIDs = null;
        string _subjectLine = string.Empty;
        string _comment = string.Empty;
        try
        {
            if (ShowAcctTaskGrid)
                _selectedIDs = ucAccountTaskGrid.GetSelectedTaskIDList();
            else if (ShowGeneralTaskGrid)
                _selectedIDs = ucGeneralTaskGrid.GetSelectedTaskIDList();

            if (_selectedIDs != null && _selectedIDs.Count > 0)
            {
                ARTEnums.TaskActionType? _taskActionType = (ARTEnums.TaskActionType)_taskActionTypeID;
                if (_taskActionType.HasValue && _taskActionType == ARTEnums.TaskActionType.Complete)
                {
                    List<AttachmentInfo> newlyAddedAttachment = null;
                    var attachmentList = Session[SessionConstants.TASK_MASTER_ATTACHMENT] as List<AttachmentInfo>;

                    if (attachmentList != null && attachmentList.Count > 0)
                    {
                        newlyAddedAttachment = (from at in attachmentList
                                                where (at.AttachmentID == null || at.AttachmentID == 0)
                                                select at).ToList();
                    }

                    List<long> taskDetailIDList = null;
                    if (ShowAcctTaskGrid)
                        taskDetailIDList = ucAccountTaskGrid.GetSelectedTaskDetailIDList();
                    else if (ShowGeneralTaskGrid)
                        taskDetailIDList = ucGeneralTaskGrid.GetSelectedTaskDetailIDList();

                    TaskMasterHelper.UpdateTaskStatusCommentAttachments(taskDetailIDList, ARTEnums.TaskActionType.Save
                        , _subjectLine, ucAddTaskComment.Text, newlyAddedAttachment);

                    flag = true;

                    if (flag)
                        Session[SessionConstants.TASK_MASTER_ATTACHMENT] = null;
                }
            }
            else
            {
                flag = false;
                Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, LanguageUtil.GetValue(2013));
            }
        }
        catch (Exception ex)
        {
            flag = false;
            Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, ex);
        }
        finally
        {
            _selectedIDs = null;
        }

        return flag;

    }

    public string ResolveClientUrlPath(string relativeUrl)
    {
        string url;
        url = Page.ResolveClientUrl(relativeUrl);
        return url;
    }

    public string GetUniqueSessionKey()
    {

        return (new Guid().ToString() + SessionConstants.GENERAL_TASK_GRID_DATA);
    }

    #endregion

    #region Private Functions

    private string GetPopupUrlForUser(short TaskUserRole)
    {
        _PopupUrlNotify = ResolveClientUrlPath(POPUP_PAGE) + "?" + QueryStringConstants.FROMPOPUP + "=1" + "&" + QueryStringConstants.TASK_USER_ROLE_ID + "=" + TaskUserRole.ToString();
        return "javascript:{OpenNewWindow('" + _PopupUrlNotify + "','" + QueryStringConstants.SELECTED_USER_ID + "','" + TaskUserRole + "');}";

    }
    private string GetReplacementValue(int LabelID)
    {
        string msg = LanguageUtil.GetValue(2664);
        return string.Format(msg, LanguageUtil.GetValue(LabelID));
    }
    void AddHideColumnsForEditDelete()
    {
        if (generalGridHideColumnList != null)
            generalGridHideColumnList.Add("comment");
        else
        {
            generalGridHideColumnList = new List<string>();
            generalGridHideColumnList.Add("comment");
        }
    }


    /* should be deleted */


    void LoadData()
    {
        try
        {
            if (_taskActionTypeID.HasValue)
            {
                ARTEnums.TaskActionType _taskActionType = (ARTEnums.TaskActionType)_taskActionTypeID;
                List<long> AccountIDs = GetAccountIdList();
                switch (_taskActionType)
                {
                    case ARTEnums.TaskActionType.Review:
                    case ARTEnums.TaskActionType.Approve:
                    case ARTEnums.TaskActionType.Reject:
                    case ARTEnums.TaskActionType.Reopen:
                    case ARTEnums.TaskActionType.Complete:
                        DataSource = TaskMasterHelper.GetAccessibleTasksByActionTypeID(SessionHelper.CurrentUserID.Value, SessionHelper.CurrentRoleID.Value, SessionHelper.CurrentReconciliationPeriodID.Value
                        , (ARTEnums.TaskType)_taskTypeID.Value, _taskActionType, AccountIDs, null, false);
                        if (DataSource.Count > 0)
                            ucAddTaskComment.DisAbleAttachementControl = false;
                        else
                            ucAddTaskComment.DisAbleAttachementControl = true;
                        break;
                    case ARTEnums.TaskActionType.Remove:
                        if (SessionHelper.CurrentRoleID.GetValueOrDefault() == (short)WebEnums.UserRole.SYSTEM_ADMIN)
                        {
                            DataSource = TaskMasterHelper.GetDeleteAccessableTaskByUserID(SessionHelper.CurrentUserID.Value, SessionHelper.CurrentRoleID.Value, SessionHelper.CurrentReconciliationPeriodID.Value
                                , (ARTEnums.TaskType)_taskTypeID.Value, null, false);
                        }
                        else
                        {
                            DataSource = TaskMasterHelper.GetDeleteAccessableTaskByUserID(SessionHelper.CurrentUserID.Value, SessionHelper.CurrentRoleID.Value, SessionHelper.CurrentReconciliationPeriodID.Value
                                , (ARTEnums.TaskType)_taskTypeID.Value, (short)ARTEnums.TaskCategory.CreatedTask, false);
                        }
                        break;
                    case ARTEnums.TaskActionType.BulkEdit:
                        DataSource = TaskMasterHelper.GetAccessableTaskForBulkEdit(SessionHelper.CurrentUserID.Value, SessionHelper.CurrentRoleID.Value, SessionHelper.CurrentReconciliationPeriodID.Value
                            , (ARTEnums.TaskType)_taskTypeID.Value, (short)ARTEnums.TaskCategory.All, false);
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            DataSource = null;
            Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, ex);
        }
    }

    List<long> GetAccountIdList()
    {
        List<long> AccountIDs = new List<long>();
        if (AccountID.GetValueOrDefault() > 0)
        {
            AccountIDs.Add(AccountID.Value);
        }
        else if (NetAccountID.GetValueOrDefault() > 0 && SessionHelper.CurrentReconciliationPeriodID.GetValueOrDefault() > 0)
        {
            AccountIDs = NetAccountHelper.GetAllConstituentAccounts(NetAccountID.Value, SessionHelper.CurrentReconciliationPeriodID.Value);
        }
        return AccountIDs;
    }
    void SetGridsProperties(bool ShowAccountGrid, bool ShowGeneralTaskGrid)
    {
        ucAccountTaskGrid.Visible = ShowAcctTaskGrid;
        ucGeneralTaskGrid.Visible = ShowGeneralTaskGrid;

        if (ShowAcctTaskGrid)
        {
            ucAccountTaskGrid.IsCompletionDocsEditable = true;
            ucAccountTaskGrid.ShowGridColumns(new List<string>() { "CheckboxSelectColumn", "AccountName", "AccountNumber" });
            ucAccountTaskGrid.HideGridColumns(generalGridHideColumnList);
            BindAccountTaskGrid();
        }
        if (ShowGeneralTaskGrid)
        {
            ucGeneralTaskGrid.IsCompletionDocsEditable = true;
            ucGeneralTaskGrid.HideGridColumns(generalGridHideColumnList);
            BindGeneralTaskGrid();
        }
    }

    void BindGeneralTaskGrid()
    {
        ucGeneralTaskGrid.SetGeneralTaskGridData(DataSource);
        ucGeneralTaskGrid.LoadGridData();
    }

    void BindAccountTaskGrid()
    {
        this.ucAccountTaskGrid.SetAccountTaskGridData(DataSource);
        this.ucAccountTaskGrid.LoadGridData();
    }
    private bool UpdateTask(List<long> SelectedIDs, ARTEnums.TaskActionType? TaskActionType)
    {
        bool flag = false;
        try
        {
            TaskHdrInfo oTaskHdrInfo = null;
            TaskHdrInfo oTaskHdrInfoForAlert = null;
            List<int> assignedAssignedToIDCollection = new List<int>();
            List<int> assignedReviewerToIDCollection = new List<int>();
            List<int> assignedApproverIDCollection = new List<int>();
            List<int> assignedNotifyIDCollection = new List<int>();

            List<int> oldAssignedAssignedToIDCollection = new List<int>();
            List<int> oldAssignedReviewerIDCollection = new List<int>();
            List<int> oldAssignedApproverIDCollection = new List<int>();
            List<int> oldAssignedNotifyIDCollection = new List<int>();

            List<TaskHdrInfo> oTaskHdrInfoList = new List<TaskHdrInfo>();
            List<TaskHdrInfo> oTaskHdrInfoListForAlert = new List<TaskHdrInfo>();
            List<TaskHdrInfo> oListTaskHdrInfoOriginal = new List<TaskHdrInfo>();
            if (TaskActionType == ARTEnums.TaskActionType.BulkEdit)
            {
                string errorMsg = string.Empty;
                //AssignTo
                List<UserHdrInfo> oAssignToUserHdrInfoList = null;
                if (!string.IsNullOrEmpty(this.hdnAssignedTo.Value))
                {
                    oAssignToUserHdrInfoList = new List<UserHdrInfo>();
                    string[] AssignTo = hdnAssignedTo.Value.Split(',');
                    for (int i = 0; i < AssignTo.Length; i++)
                    {
                        UserHdrInfo oAssignToUserHdrInfo = new UserHdrInfo();
                        oAssignToUserHdrInfo.UserID = Convert.ToInt32(AssignTo[i]);
                        oAssignToUserHdrInfoList.Add(oAssignToUserHdrInfo);
                    }
                }
                //Reviewer
                List<UserHdrInfo> oReviewerUserHdrInfoList = null;
                if (!string.IsNullOrEmpty(this.hdnReviewer.Value))
                {
                    oReviewerUserHdrInfoList = new List<UserHdrInfo>();
                    string[] Reviewer = hdnReviewer.Value.Split(',');
                    for (int i = 0; i < Reviewer.Length; i++)
                    {
                        UserHdrInfo oReviewerUserHdrInfo = new UserHdrInfo();
                        oReviewerUserHdrInfo.UserID = Convert.ToInt32(Reviewer[i]);
                        oReviewerUserHdrInfoList.Add(oReviewerUserHdrInfo);
                    }
                }
                //Approver
                List<UserHdrInfo> oApproverUserHdrInfoList = null;
                if (!string.IsNullOrEmpty(this.hdnApprover.Value))
                {
                    oApproverUserHdrInfoList = new List<UserHdrInfo>();
                    string[] Approver = hdnApprover.Value.Split(',');
                    for (int i = 0; i < Approver.Length; i++)
                    {
                        UserHdrInfo oApproverUserHdrInfo = new UserHdrInfo();
                        oApproverUserHdrInfo.UserID = Convert.ToInt32(Approver[i]);
                        oApproverUserHdrInfoList.Add(oApproverUserHdrInfo);
                    }
                }
                //Notify
                List<UserHdrInfo> oNotifyUserHdrInfoList = null;
                if (!string.IsNullOrEmpty(this.hdnNotify.Value))
                {
                    oNotifyUserHdrInfoList = new List<UserHdrInfo>();
                    string[] Notiy = hdnNotify.Value.Split(',');
                    for (int i = 0; i < Notiy.Length; i++)
                    {
                        UserHdrInfo oNotiyUserHdrInfo = new UserHdrInfo();
                        oNotiyUserHdrInfo.UserID = Convert.ToInt32(Notiy[i]);
                        oNotifyUserHdrInfoList.Add(oNotiyUserHdrInfo);
                    }
                }

                List<long> _selectedIDs = null;
                if (ShowAcctTaskGrid)
                    _selectedIDs = ucAccountTaskGrid.GetSelectedTaskIDList();
                else if (ShowGeneralTaskGrid)
                    _selectedIDs = ucGeneralTaskGrid.GetSelectedTaskIDList();

                List<short> _attributeIDs = new List<short>();
                _attributeIDs.Add((short)ARTEnums.TaskAttribute.AssignedTo);
                _attributeIDs.Add((short)ARTEnums.TaskAttribute.Reviewer);
                _attributeIDs.Add((short)ARTEnums.TaskAttribute.Approver);
                _attributeIDs.Add((short)ARTEnums.TaskAttribute.Notify);
                var taskAttributeList = TaskMasterHelper.GetTaskAttributeList(SessionHelper.CurrentReconciliationPeriodID, _selectedIDs, _attributeIDs);
                if (ValidateData(oAssignToUserHdrInfoList, oReviewerUserHdrInfoList, oApproverUserHdrInfoList, oNotifyUserHdrInfoList, taskAttributeList, _selectedIDs, out errorMsg))
                {

                    foreach (long taskID in SelectedIDs)
                    {
                        TaskHdrInfo oTaskHdrInfoOriginal;
                        oTaskHdrInfoOriginal = TaskMasterHelper.GetTaskHdrInfoByTaskID(taskID, SessionHelper.CurrentReconciliationPeriodID);

                        var assignedToList = (from ta in taskAttributeList
                                              where ta.TaskAttributeID.Value == ((short)ARTEnums.TaskAttribute.AssignedTo) && ta.TaskID.GetValueOrDefault() == taskID && ta.ReferenceID.HasValue
                                              select ta).ToList();
                        var reviewerList = (from ta in taskAttributeList
                                            where ta.TaskAttributeID.Value == ((short)ARTEnums.TaskAttribute.Reviewer) && ta.TaskID.GetValueOrDefault() == taskID && ta.ReferenceID.HasValue
                                            select ta).ToList();
                        var approverList = (from ta in taskAttributeList
                                            where ta.TaskAttributeID.Value == ((short)ARTEnums.TaskAttribute.Approver) && ta.TaskID.GetValueOrDefault() == taskID && ta.ReferenceID.HasValue
                                            select ta).ToList();
                        var notifyList = (from ta in taskAttributeList
                                          where ta.TaskAttributeID.Value == ((short)ARTEnums.TaskAttribute.Notify) && ta.TaskID.GetValueOrDefault() == taskID && ta.ReferenceID.HasValue
                                          select ta).ToList();



                        oTaskHdrInfo = DataSource.FirstOrDefault(t => t.TaskID == taskID);
                        oTaskHdrInfoForAlert = DataSource.FirstOrDefault(t => t.TaskID == taskID);

                        //Check AssignedTo has changed if yes send alert to new AssignedTo
                        if (oAssignToUserHdrInfoList != null && oAssignToUserHdrInfoList.Count > 0)
                        {
                            GetUsersForTaskAlert(oAssignToUserHdrInfoList, assignedToList, assignedAssignedToIDCollection, oldAssignedAssignedToIDCollection);
                        }
                        //Check Reviewer has changed if yes send alert to new Reviewer
                        if (oReviewerUserHdrInfoList != null && oReviewerUserHdrInfoList.Count > 0)
                        {
                            GetUsersForTaskAlert(oReviewerUserHdrInfoList, reviewerList, assignedReviewerToIDCollection, oldAssignedReviewerIDCollection);
                        }
                        //Check Approver has changed if yes send alert to new Approver
                        if (oApproverUserHdrInfoList != null && oApproverUserHdrInfoList.Count > 0)
                        {
                            GetUsersForTaskAlert(oApproverUserHdrInfoList, approverList, assignedApproverIDCollection, oldAssignedApproverIDCollection);
                        }
                        //Check Notify has changed if yes send alert to new Notify
                        if (oNotifyUserHdrInfoList != null && oNotifyUserHdrInfoList.Count > 0)
                        {
                            GetUsersForTaskAlert(oNotifyUserHdrInfoList, notifyList, assignedNotifyIDCollection, oldAssignedNotifyIDCollection);
                        }
                        //////ResetTaskAttributes(oTaskHdrInfo);
                        if (oTaskHdrInfo != null)
                        {
                            oTaskHdrInfo.RevisedBy = SessionHelper.CurrentUserLoginID;
                            oTaskHdrInfo.DateRevised = System.DateTime.Now;
                            if (oAssignToUserHdrInfoList != null && oAssignToUserHdrInfoList.Count > 0)
                            {
                                oTaskHdrInfo.AssignedTo = oAssignToUserHdrInfoList;
                                oTaskHdrInfoForAlert.AssignedTo = oAssignToUserHdrInfoList;
                            }
                            else
                            {
                                oTaskHdrInfoForAlert.AssignedTo = GetUserList(assignedToList);
                            }
                            if (oReviewerUserHdrInfoList != null && oReviewerUserHdrInfoList.Count > 0)
                            {
                                oTaskHdrInfo.Reviewer = oReviewerUserHdrInfoList;
                                oTaskHdrInfoForAlert.Reviewer = oReviewerUserHdrInfoList;
                            }
                            else
                            {
                                oTaskHdrInfoForAlert.Reviewer = GetUserList(reviewerList);
                            }
                            if (oApproverUserHdrInfoList != null && oApproverUserHdrInfoList.Count > 0)
                            {
                                oTaskHdrInfo.Approver = oApproverUserHdrInfoList;
                                oTaskHdrInfoForAlert.Approver = oApproverUserHdrInfoList;
                            }
                            else
                            {
                                oTaskHdrInfoForAlert.Approver = GetUserList(approverList);
                            }
                            if (oNotifyUserHdrInfoList != null && oNotifyUserHdrInfoList.Count > 0)
                            {
                                oTaskHdrInfo.Notify = oNotifyUserHdrInfoList;
                                oTaskHdrInfoForAlert.Notify = oNotifyUserHdrInfoList;
                            }
                            else
                            {
                                oTaskHdrInfoForAlert.Notify = GetUserList(notifyList);
                            }
                            oTaskHdrInfo.IsActive = true;
                            oTaskHdrInfoList.Add(oTaskHdrInfo);
                            oTaskHdrInfoListForAlert.Add(oTaskHdrInfoForAlert);


                        }

                        oListTaskHdrInfoOriginal.Add(oTaskHdrInfoOriginal);
                    }

                    flag = TaskMasterHelper.BulkEditTask(oTaskHdrInfoList, SessionHelper.CurrentReconciliationPeriodID.GetValueOrDefault(), SessionHelper.CurrentUserLoginID, System.DateTime.Now);
                    //// Raise Alert   
                    AlertHelper.RaiseAlertForAssignedTask(assignedAssignedToIDCollection, assignedReviewerToIDCollection, assignedApproverIDCollection, assignedNotifyIDCollection, oTaskHdrInfoListForAlert);
                    AlertHelper.RaiseAlertForUnAssignedTask(oldAssignedAssignedToIDCollection, oldAssignedReviewerIDCollection, oldAssignedApproverIDCollection, oldAssignedNotifyIDCollection, oListTaskHdrInfoOriginal);
                }
                else
                {
                    flag = false;
                    Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, errorMsg);
                    return flag;
                }
            }
            else if (TaskActionType == ARTEnums.TaskActionType.Remove)
            {
                //foreach (long taskdetailID in SelectedIDs)
                //{
                //    oTaskHdrInfo = DataSource.FirstOrDefault(t => t.TaskID == taskdetailID);
                //    ResetTaskAttributes(oTaskHdrInfo);
                //    if (oTaskHdrInfo != null)
                //    {
                //        oTaskHdrInfo.IsDeleted = true;
                //        oTaskHdrInfo.IsActive = true;
                //        oTaskHdrInfoList.Add(oTaskHdrInfo);
                //    }

                //}
                List<int> AllUserIDCollection = TaskMasterHelper.GetAllNotifyUserIDCollection(SelectedIDs);
                List<TaskHdrInfo> oSelectedTaskHdrInfoList = (from objTaskHdrInfo in DataSource
                                                              join oTaskID in SelectedIDs on objTaskHdrInfo.TaskID equals oTaskID
                                                              select objTaskHdrInfo).ToList();
                TaskMasterHelper.DeleteAccessibleTasks(SelectedIDs, SessionHelper.CurrentReconciliationPeriodID.GetValueOrDefault(), SessionHelper.CurrentUserLoginID, System.DateTime.Now);
                //Notify users               
                TaskMasterHelper.NotifyUsers(oSelectedTaskHdrInfoList, AllUserIDCollection, Helper.GetLabelIDValue(2687), Helper.GetLabelIDValue(2688));
                flag = true;
            }


        }
        catch (ARTException ex)
        {
            flag = false;
            PopupHelper.ShowErrorMessage((PopupPageBase)this.Page, ex);
        }
        catch (Exception ex)
        {
            flag = false;
            PopupHelper.ShowErrorMessage((PopupPageBase)this.Page, ex);
        }
        return flag;
    }
    private List<UserHdrInfo> GetUserList(List<TaskAttributeValueInfo> oTaskAttributeValueInfoList)
    {
        List<UserHdrInfo> oUserHdrInfoList = new List<UserHdrInfo>();
        if (oTaskAttributeValueInfoList != null && oTaskAttributeValueInfoList.Count > 0)
        {
            foreach (var item in oTaskAttributeValueInfoList)
            {
                if (item.ReferenceID.HasValue)
                {
                    UserHdrInfo oUH = new UserHdrInfo();
                    oUH.UserID = Convert.ToInt32(item.ReferenceID.Value);
                    oUserHdrInfoList.Add(oUH);
                }
            }
        }
        return oUserHdrInfoList;
    }
    private void GetUsersForTaskAlert(List<UserHdrInfo> oNewUserList, List<TaskAttributeValueInfo> oOldUserList, List<int> oAssignedUserIDList, List<int> oUnAssignedUserIDList)
    {
        if (oNewUserList != null && oNewUserList.Count > 0)
        {
            for (int i = 0; i < oNewUserList.Count; i++)
            {
                if (oOldUserList != null && oOldUserList.Count > 0)
                {
                    var OldUser = oOldUserList.Find(obj => obj.ReferenceID.GetValueOrDefault() == oNewUserList[i].UserID.GetValueOrDefault());
                    if (OldUser == null && oNewUserList[i].UserID.HasValue && !oAssignedUserIDList.Contains(oNewUserList[i].UserID.Value))
                        oAssignedUserIDList.Add(oNewUserList[i].UserID.Value);
                }
            }
        }
        if (oOldUserList != null && oOldUserList.Count > 0)
        {
            for (int i = 0; i < oOldUserList.Count; i++)
            {
                if (oNewUserList != null && oNewUserList.Count > 0)
                {
                    var NewUser = oNewUserList.Find(obj => obj.UserID.GetValueOrDefault() == oOldUserList[i].ReferenceID.GetValueOrDefault());
                    if (NewUser == null && oOldUserList[i].ReferenceID.HasValue && !oUnAssignedUserIDList.Contains(Convert.ToInt32(oOldUserList[i].ReferenceID.Value)))
                        oUnAssignedUserIDList.Add(Convert.ToInt32(oOldUserList[i].ReferenceID.Value));
                }
            }
        }
    }

    private void SetErrorMessages()
    {
        // Set Error Messages        
    }
    private void ResetTaskAttributes(TaskHdrInfo TaskHdr)
    {
        if (TaskHdr != null)
        {
            TaskHdr.Reviewer = null;
            TaskHdr.AssignedTo = null;
            TaskHdr.CreationAttachment = null;
            TaskHdr.ReviewerDueDate = null;
            TaskHdr.ReviewerDueDays = null;
            TaskHdr.AssigneeDueDate = null;
            TaskHdr.AssigneeDueDays = null;
            TaskHdr.TaskDescription = null;
            TaskHdr.Notify = null;
            TaskHdr.RecurrenceFrequency = null;
            TaskHdr.RecurrenceType = null;
            TaskHdr.TaskAccount = null;
            TaskHdr.TaskDueDate = null;
            TaskHdr.TaskDueDays = null;
            TaskHdr.TaskList = null;
            TaskHdr.TaskName = null;
            TaskHdr.TaskStartDate = null;
            TaskHdr.IsDeleted = null;
        }
    }

    #endregion

    #region Validation

    private bool ValidateData(List<UserHdrInfo> oAssignToUserHdrInfoList, List<UserHdrInfo> oReviewerUserHdrInfoList, List<UserHdrInfo> oApproverUserHdrInfoList, List<UserHdrInfo> oNotifyUserHdrInfoList, List<TaskAttributeValueInfo> taskAttributeList, List<long> _selectedIDs, out string ErrorMessage)
    {
        bool flag = true;
        ErrorMessage = string.Empty;
        if (taskAttributeList != null)
        {
            if (string.IsNullOrEmpty(txtAssignedTo.Text))
                hdnAssignedTo.Value = string.Empty;
            if (string.IsNullOrEmpty(txtApprover.Text))
                hdnApprover.Value = string.Empty;
            if (string.IsNullOrEmpty(txtNotify.Text))
                hdnNotify.Value = string.Empty;

            foreach (long _taskID in _selectedIDs)
            {

                var assignedToList = (from ta in taskAttributeList
                                      where ta.TaskAttributeID.Value == ((short)ARTEnums.TaskAttribute.AssignedTo) && ta.TaskID.GetValueOrDefault() == _taskID && ta.ReferenceID.HasValue
                                      select ta).ToList();
                var reviewerList = (from ta in taskAttributeList
                                    where ta.TaskAttributeID.Value == ((short)ARTEnums.TaskAttribute.Reviewer) && ta.TaskID.GetValueOrDefault() == _taskID && ta.ReferenceID.HasValue
                                    select ta).ToList();
                var approverList = (from ta in taskAttributeList
                                    where ta.TaskAttributeID.Value == ((short)ARTEnums.TaskAttribute.Approver) && ta.TaskID.GetValueOrDefault() == _taskID && ta.ReferenceID.HasValue
                                    select ta).ToList();
                var notifyList = (from ta in taskAttributeList
                                  where ta.TaskAttributeID.Value == ((short)ARTEnums.TaskAttribute.Notify) && ta.TaskID.GetValueOrDefault() == _taskID && ta.ReferenceID.HasValue
                                  select ta).ToList();
                //************* Added By Manoj**************
                // if user specify the owners check shoud be apply on newely selected users
                //assignedTo
                if (!string.IsNullOrEmpty(hdnAssignedTo.Value) && oAssignToUserHdrInfoList != null && oAssignToUserHdrInfoList.Count > 0)
                {
                    if (assignedToList != null && assignedToList.Count > 0)
                    {
                        assignedToList.Clear();
                        foreach (var oUser in oAssignToUserHdrInfoList)
                        {
                            TaskAttributeValueInfo oTaskAttributeValueInfo = new TaskAttributeValueInfo();
                            oTaskAttributeValueInfo.ReferenceID = Convert.ToInt64(oUser.UserID.GetValueOrDefault());
                            assignedToList.Add(oTaskAttributeValueInfo);
                        }
                    }
                }
                //Reviewer
                if (!string.IsNullOrEmpty(hdnReviewer.Value) && oReviewerUserHdrInfoList != null && oReviewerUserHdrInfoList.Count > 0)
                {
                    if (reviewerList != null && reviewerList.Count > 0)
                    {
                        reviewerList.Clear();
                        foreach (var oUser in oReviewerUserHdrInfoList)
                        {
                            TaskAttributeValueInfo oTaskAttributeValueInfo = new TaskAttributeValueInfo();
                            oTaskAttributeValueInfo.ReferenceID = Convert.ToInt64(oUser.UserID.GetValueOrDefault());
                            reviewerList.Add(oTaskAttributeValueInfo);
                        }
                    }
                    else
                    {

                        string errorMessageR = LanguageUtil.GetValue(2969);
                        ErrorMessage = string.Format(errorMessageR, LanguageUtil.GetValue(1131), LanguageUtil.GetValue(1131));
                        flag = false;
                    }

                }

                //Approver
                if (!string.IsNullOrEmpty(hdnApprover.Value) && oApproverUserHdrInfoList != null && oApproverUserHdrInfoList.Count > 0)
                {
                    if (approverList != null && approverList.Count > 0)
                    {
                        approverList.Clear();
                        foreach (var oUser in oApproverUserHdrInfoList)
                        {
                            TaskAttributeValueInfo oTaskAttributeValueInfo = new TaskAttributeValueInfo();
                            oTaskAttributeValueInfo.ReferenceID = Convert.ToInt64(oUser.UserID.GetValueOrDefault());
                            approverList.Add(oTaskAttributeValueInfo);
                        }
                    }
                    else
                    {
                        string errorMessageR = LanguageUtil.GetValue(2969);
                        ErrorMessage = string.Format(errorMessageR, LanguageUtil.GetValue(1132), LanguageUtil.GetValue(1132));
                        flag = false;
                    }

                }

                //Notify
                if (!string.IsNullOrEmpty(hdnNotify.Value) && oNotifyUserHdrInfoList != null && oNotifyUserHdrInfoList.Count > 0)
                {
                    if (notifyList != null && notifyList.Count > 0)
                        notifyList.Clear();
                    foreach (var oUser in oNotifyUserHdrInfoList)
                    {
                        TaskAttributeValueInfo oTaskAttributeValueInfo = new TaskAttributeValueInfo();
                        oTaskAttributeValueInfo.ReferenceID = Convert.ToInt64(oUser.UserID.GetValueOrDefault());
                        notifyList.Add(oTaskAttributeValueInfo);
                    }

                }
                //******************************************
                if (assignedToList == null || assignedToList.Count == 0)
                {
                    string errorMessageAssignedTo = LanguageUtil.GetValue(5000359);
                    ErrorMessage = string.Format(errorMessageAssignedTo, LanguageUtil.GetValue(2564));
                    flag = false;
                }
                if ((approverList != null && approverList.Count > 0) && (reviewerList == null || reviewerList.Count == 0))
                {
                    ErrorMessage = LanguageUtil.GetValue(2959);// "Reviewer is mandatory if Approver is given for the task. ";
                    flag = false;
                }

                if (IsDuplicateTaskOwners(assignedToList, reviewerList, approverList, notifyList))
                {
                    string errorMessage2 = LanguageUtil.GetValue(5000354);
                    ErrorMessage = string.Format(errorMessage2, LanguageUtil.GetValue(2564), LanguageUtil.GetValue(1131), LanguageUtil.GetValue(1132), LanguageUtil.GetValue(2525));
                    flag = false;
                }

                if (oAssignToUserHdrInfoList == null && oReviewerUserHdrInfoList == null && oApproverUserHdrInfoList == null && oNotifyUserHdrInfoList == null)
                {
                    ErrorMessage = LanguageUtil.GetValue(2877);
                    flag = false;
                }

                if (flag == false)
                    break;
            }
        }
        return flag;
    }
    private bool IsDuplicateTaskOwners(List<TaskAttributeValueInfo> assignedToList, List<TaskAttributeValueInfo> reviewerList, List<TaskAttributeValueInfo> approverList, List<TaskAttributeValueInfo> notifyList)
    {
        bool flag = false;
        List<long> oTempTaskUserIDList = new List<long>();
        if (assignedToList != null && assignedToList.Count > 0 && !flag)
            flag = ValidateTaskOwner(oTempTaskUserIDList, assignedToList);
        if (reviewerList != null && reviewerList.Count > 0 && !flag)
            flag = ValidateTaskOwner(oTempTaskUserIDList, reviewerList);
        if (approverList != null && approverList.Count > 0 && !flag)
            flag = ValidateTaskOwner(oTempTaskUserIDList, approverList);
        if (notifyList != null && notifyList.Count > 0 && !flag)
            flag = ValidateTaskOwner(oTempTaskUserIDList, notifyList);
        return flag;
    }
    private bool ValidateTaskOwner(List<long> oTempTaskUserIDList, List<TaskAttributeValueInfo> TaskOwnerList)
    {
        bool IsDuplicate = false;
        for (int i = 0; i < TaskOwnerList.Count; i++)
        {
            if (TaskOwnerList[i].ReferenceID.HasValue)
            {
                if (oTempTaskUserIDList.Contains(TaskOwnerList[i].ReferenceID.Value))
                {
                    IsDuplicate = true;
                    break;
                }
                else
                    oTempTaskUserIDList.Add(TaskOwnerList[i].ReferenceID.Value);
            }
        }
        return IsDuplicate;
    }
    #endregion

}
