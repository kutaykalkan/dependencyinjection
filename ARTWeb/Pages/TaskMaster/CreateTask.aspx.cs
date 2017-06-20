using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.Model;
using SkyStem.Library.Controls.WebControls;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.Library.Controls.TelerikWebControls.Common;
using System.Data.SqlClient;
using SkyStem.ART.Web.Data;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.IServices;
using Telerik.Web.UI;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Exception;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.ServiceModel;
using System.Collections;
using SkyStem.ART.Client.Params;
using System.Data;

public partial class Pages_CreateTask : PopupPageBaseTaskMaster
{
    #region Global Variables and const
    short _TaskTypeID;
    long? _TaskID;
    DateTime? _TaskRecPeriodEndDate;
    long? _TaskDetailID;
    //int defaultItemCount = Convert.ToInt32(AppSettingHelper.GetAppSettingValue(AppSettingConstants.DEFAULT_CHUNK_SIZE));
    string _PopupUrlNotify = string.Empty;
    const string POPUP_PAGE = "../AddUserMailOnDataImport.aspx";
    const int POPUP_WIDTH = 800;
    const int POPUP_HEIGHT = 480;
    private const string TEXTBOX_ONBLUR_VALUE = "HideProgressBar('{0}')";
    List<HolidayCalendarInfo> oHolidayCalendarInfoCollection = null;
    IList<WeekDayMstInfo> oWeekDayMstInfoCollection = null;
    IList<CompanyWeekDayInfo> oCompanyWorkWeekInfoCollectionForFincncialyear = null;

    # endregion

    #region Public Properties
    public List<AccountHdrInfo> AccountHdrInfoListSearchResults
    {
        get { return (List<AccountHdrInfo>)ViewState[SessionConstants.SEARCH_RESULTS_ACCOUNTS]; }
        set { ViewState[SessionConstants.SEARCH_RESULTS_ACCOUNTS] = value; }
    }
    public List<AccountHdrInfo> AccountHdrInfoListAdded
    {
        get { return (List<AccountHdrInfo>)Session[SessionConstants.TASK_ACCOUNT_LIST]; }
        set { Session[SessionConstants.TASK_ACCOUNT_LIST] = value; }
    }
    public List<long> AccessibleAccountIDList
    {
        get { return (List<long>)ViewState[ViewStateConstants.ACCESSIBLE_ACCOUNT_LIST]; }
        set { ViewState[ViewStateConstants.ACCESSIBLE_ACCOUNT_LIST] = value; }
    }
    # endregion

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        ucSkyStemARTGridAccountsAdded.GridItemDataBound += new Telerik.Web.UI.GridItemEventHandler(ucSkyStemARTGridAccountsAdded_GridItemDataBound);
        ucSkyStemARTGridAccountSearchResult.GridItemDataBound += new GridItemEventHandler(ucSkyStemARTGridAccountSearchResult_GridItemDataBound);
        ucSkyStemARTGridAccountSearchResult.Grid_NeedDataSourceEventHandler += new UserControls_SkyStemARTGrid.Grid_NeedDataSource(ucSkyStemARTGrid_Grid_NeedDataSourceEventHandler);
        ucSkyStemARTGridAccountsAdded.Grid_NeedDataSourceEventHandler += new UserControls_SkyStemARTGrid.Grid_NeedDataSource(ucSkyStemARTGridAccountsAdded_Grid_NeedDataSourceEventHandler);
        ucAccountSearchControl.SearchClickEventHandler += new UserControls_AccountSearchControl.ShowSearchResults(ucAccountSearchControl_SearchClickEventHandler);
        ucAccountSearchControl.PnlMassAndBulk.Visible = false;
        ucAccountSearchControl.ShowMissing.Visible = false;
        ucAccountSearchControl.PnlSearchAndMail.Visible = false;
        ucAccountSearchControl.ShowDueDaysRow = true;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            GetQueryStringValues();
            SetErrorMessages();
            string[] oAccountID = { "AccountID" };
            ucSkyStemARTGridAccountSearchResult.Grid.MasterTableView.DataKeyNames = oAccountID;
            ucSkyStemARTGridAccountsAdded.Grid.MasterTableView.DataKeyNames = oAccountID;
            ucSkyStemARTGridAccountsAdded.CompanyID = SessionHelper.CurrentCompanyID;
            ucSkyStemARTGridAccountSearchResult.CompanyID = SessionHelper.CurrentCompanyID;
            ucSkyStemARTGridAccountSearchResult.ShowSelectCheckBoxColum = true;
            ucSkyStemARTGridAccountsAdded.ShowSelectCheckBoxColum = true;
            txtAssignedTo.Attributes.Add("readonly", "readonly");
            txtReviewer.Attributes.Add("readonly", "readonly");
            txtApprover.Attributes.Add("readonly", "readonly");
            txtNotify.Attributes.Add("readonly", "readonly");
            btnClearAssignedTo.Attributes.Add("onclick", "return ConfirmDeletion('" + GetReplacementValue(2649) + "','" + (short)ARTEnums.TaskAttribute.AssignedTo + "');");
            btnClearReviewer.Attributes.Add("onclick", "return ConfirmDeletion('" + GetReplacementValue(1131) + "','" + (short)ARTEnums.TaskAttribute.Reviewer + "');");
            btnClearApprover.Attributes.Add("onclick", "return ConfirmDeletion('" + GetReplacementValue(1132) + "','" + (short)ARTEnums.TaskAttribute.Approver + "');");
            btnClearNotify.Attributes.Add("onclick", "return ConfirmDeletion('" + GetReplacementValue(2525) + "','" + (short)ARTEnums.TaskAttribute.Notify + "');");
            /*
            2652 - If Approver is mentioned, Assinee due date is mandatory
            2653 - For Recurring Tasks, If Approver is mentioned, "Assignee due days" is mandatory. 
            2654 - For Non-Recurring Tasks, If "Assignee Due Date" is provided, it has to be greater than "Task Due Date". 
            2655 - For Recurring Tasks, If "Assignee Due Days" is provided, it has to be greater than "Task Due Days".
            */
            PopupHelper.ShowInputRequirementSection(this, 2652, 2653, 2654, 2655, 2736, 2772, 2789);
            if (!Page.IsPostBack)
            {
                Session.Remove(SessionConstants.TASK_MASTER_ATTACHMENT);
                ClearGrids();
                FillRecPeriodNumberCheckBoxList();
                BindDdlRecurrence();
                BindddlTaskListName();
                BindddlTaskSubListName();
                BindddlDueDaysBasis();
                BindddlDaysType();
                ShowHideTaskListName(false);
                ShowHideTaskSubListName(false);
                if (this.Mode == QueryStringConstants.EDIT)
                {

                    hdnDateToCompare.Value = DateTime.Now.ToShortDateString();
                    PopupHelper.SetPageTitle(this, 2605);
                    //2605
                    EnableDisableControls(true);
                    LoadTaskAttributesInControls();
                    ShowHideControls(true);
                    pnlCreatedBy.Visible = true;

                }
                else if (this.Mode == QueryStringConstants.INSERT)
                {
                    FillReconciliationPeriodCheckBoxList(SessionHelper.CurrentReconciliationPeriodEndDate);
                    hdnDateToCompare.Value = DateTime.Now.ToShortDateString();
                    PopupHelper.SetPageTitle(this, 2604);
                    // FillReconciliationPeriodCheckBoxListAdd();
                    //2604
                    ShowHidePnlRecurrenceType((short)ARTEnums.TaskRecurrenceType.NoRecurrence);
                    ShowHideControls(true);
                    if (SessionHelper.CurrentUserID.HasValue)
                        hdnAssignedTo.Value = SessionHelper.CurrentUserID.Value.ToString();
                    UserHdrInfo oCurrentUser = SessionHelper.GetCurrentUser();
                    if (oCurrentUser != null)
                    {
                        txtAssignedTo.Text = Helper.GetDisplayUserFullName(oCurrentUser.FirstName, oCurrentUser.LastName);
                    }
                    if (!string.IsNullOrEmpty(hdnAssignedTo.Value))
                    {
                        AccessibleAccountIDList = null;
                        AccessibleAccountIDList = TaskMasterHelper.GetAccountIDListByUserIDs(this.GetSelectedUserID(), SessionHelper.CurrentReconciliationPeriodID);
                    }
                    EnableDisableControls(true);
                    if (_TaskTypeID == (short)ARTEnums.TaskType.AccountTask)
                        BindGrids();
                    pnlCreatedBy.Visible = false;
                }
                else
                {
                    LoadTaskAttributesInLabelControls();
                    ShowHideControls(false);
                    EnableDisableControls(false);
                    pnlCreatedBy.Visible = true;
                }

                SetTaskCustomField();
            }
            if (AccountHdrInfoListSearchResults != null && AccountHdrInfoListSearchResults.Count > 0)
                btnAdd.Visible = true;
            //  txtAssignedTo.Attributes.Add(WebConstants.ONBLUR, string.Format(TEXTBOX_ONBLUR_VALUE, txtAssignedTo.ClientID));
            // txtApprover.Attributes.Add(WebConstants.ONBLUR, string.Format(TEXTBOX_ONBLUR_VALUE, txtApprover.ClientID));
            //_PopupUrlNotify = ResolveClientUrlPath(POPUP_PAGE) + "?" + QueryStringConstants.FROMPOPUP + "=1";           
            //hlNotify.NavigateUrl = "javascript:{OpenNewWindow('" + _PopupUrlNotify + "','" + QueryStringConstants.SELECTED_USER_ID + "');}";
            hlAssignedTo.NavigateUrl = GetPopupUrlForUser((short)ARTEnums.TaskAttribute.AssignedTo);
            hlReviewer.NavigateUrl = GetPopupUrlForUser((short)ARTEnums.TaskAttribute.Reviewer);
            hlApprover.NavigateUrl = GetPopupUrlForUser((short)ARTEnums.TaskAttribute.Approver);
            hlNotify.NavigateUrl = GetPopupUrlForUser((short)ARTEnums.TaskAttribute.Notify);

            string _PopupUrl = string.Empty;
            string windowName = "Attchment";
            string _attachmentMode = (this.Mode == QueryStringConstants.READ_ONLY) ? QueryStringConstants.READ_ONLY : QueryStringConstants.INSERT;
            _PopupUrl = this.ResolveUrl(Helper.SetDocumentUploadURLForTasks(_TaskID, _TaskTypeID, _TaskID, (int)ARTEnums.RecordType.TaskCreation, _attachmentMode, out windowName));

            hlAttachment.NavigateUrl = "javascript:OpenRadWindowFromRadWindow('" + _PopupUrl + "', " + WebConstants.POPUP_HEIGHT + " , " + WebConstants.POPUP_WIDTH + ");";

            if (_TaskTypeID == (short)ARTEnums.TaskType.AccountTask)
            {
                if (this.Mode == QueryStringConstants.INSERT || this.Mode == QueryStringConstants.EDIT)
                    ShowAccountPanel(true);
                else
                {
                    pnlTaskAccounts.Visible = true;
                    pnlSearchGrid.Visible = false;
                    btnRemoveAccount.Visible = false;
                }
            }
            else
            {
                ShowAccountPanel(false);
            }
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }
    # endregion

    #region Private Methods
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
    private void LoadTaskAttributesInControls()
    {
        TaskHdrInfo oTaskHdrInfo;
        oTaskHdrInfo = TaskMasterHelper.GetTaskHdrInfoByTaskID(_TaskID, SessionHelper.CurrentReconciliationPeriodID);
        if (oTaskHdrInfo != null)
        {
            _TaskRecPeriodEndDate = oTaskHdrInfo.TaskRecPeriodEndDate;
            lblTaskNumberVal.Text = oTaskHdrInfo.TaskNumber;
            lblCreatedByVal.Text = oTaskHdrInfo.AddedBy;
            lblDateCreatedVal.Text = Helper.GetDisplayDate(oTaskHdrInfo.DateAdded);
            lblRecPeriodValue.Text = Helper.GetDisplayDate(oTaskHdrInfo.TaskRecPeriodEndDate);
            FillReconciliationPeriodCheckBoxList(_TaskRecPeriodEndDate);
            FillRecPeriodNumberCheckBoxList();
            IAttachment oAttachmentClient = RemotingHelper.GetAttachmentObject();
            List<AttachmentInfo> oAttachmentInfoList = oAttachmentClient.GetAttachment(_TaskID.Value, (int)ARTEnums.RecordType.TaskCreation, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
            // List<AttachmentInfo> oAttachmentInfoList = TaskMasterHelper.GetTaskAttachments(_TaskID, ARTEnums.RecordType.TaskCreation);
            if (oAttachmentInfoList != null)
            {
                Session.Remove(SessionConstants.TASK_MASTER_ATTACHMENT);
                Session[SessionConstants.TASK_MASTER_ATTACHMENT] = oAttachmentInfoList;
            }
            txtTaskName.Text = oTaskHdrInfo.TaskName;
            if (oTaskHdrInfo.TaskList != null && oTaskHdrInfo.TaskList.TaskListID.HasValue)
                ddlTaskListName.SelectedValue = oTaskHdrInfo.TaskList.TaskListID.Value.ToString();
            if (oTaskHdrInfo.TaskSubList != null && oTaskHdrInfo.TaskSubList.TaskSubListID.HasValue)
                ddlTaskSubListName.SelectedValue = oTaskHdrInfo.TaskSubList.TaskSubListID.Value.ToString();
            txtDescription.Text = Page.Server.HtmlDecode(Helper.GetDisplayStringValue(oTaskHdrInfo.TaskDescription));

            if (oTaskHdrInfo.RecurrenceType != null && oTaskHdrInfo.RecurrenceType.TaskRecurrenceTypeID.HasValue)
            {
                short? RecurrenceType = oTaskHdrInfo.RecurrenceType.TaskRecurrenceTypeID;
                bool isCompleteTask = oTaskHdrInfo.RecurrenceType.IsTaskCompleted.Value;

                if (RecurrenceType.HasValue)
                {
                    ddlRecurrence.SelectedValue = RecurrenceType.Value.ToString();
                    if (_TaskRecPeriodEndDate == SessionHelper.CurrentReconciliationPeriodEndDate)
                    {
                        ddlRecurrence.Enabled = !isCompleteTask;
                    }
                    else
                    {
                        ddlRecurrence.Enabled = false;
                    }
                    ShowHidePnlRecurrenceType(RecurrenceType.Value);
                    if (RecurrenceType.Value == (short)ARTEnums.TaskRecurrenceType.Custom)
                    {
                        List<ReconciliationPeriodInfo> oRecPeriodIDList = oTaskHdrInfo.RecurrenceFrequency;
                        if (oRecPeriodIDList != null)
                        {
                            for (int i = 0; i < oRecPeriodIDList.Count; i++)
                            {
                                string recPeriod = Convert.ToString(oRecPeriodIDList[i].ReconciliationPeriodID);
                                bool isTaskCompleted = oRecPeriodIDList[i].IsTaskCompleted.Value;
                                for (int j = 0; j < cblRecPeriodsCustom.Items.Count; j++)
                                {
                                    if (cblRecPeriodsCustom.Items[j].Value == recPeriod)
                                    {
                                        cblRecPeriodsCustom.Items[j].Selected = true;
                                        cblRecPeriodsCustom.Items[j].Enabled = !isTaskCompleted;
                                    }
                                }
                            }
                        }
                        for (int j = 0; j < cblRecPeriodsCustom.Items.Count; j++)
                        {
                            if (Convert.ToDateTime(cblRecPeriodsCustom.Items[j].Text) < SessionHelper.CurrentReconciliationPeriodEndDate)
                                cblRecPeriodsCustom.Items[j].Enabled = false;
                        }
                    }
                    else if (RecurrenceType.Value == (short)ARTEnums.TaskRecurrenceType.Quarterly)
                    {
                        List<ReconciliationPeriodInfo> RecPeriodNumberList = oTaskHdrInfo.RecurrencePeriodNumberList;
                        if (RecPeriodNumberList != null)
                        {
                            for (int i = 0; i < RecPeriodNumberList.Count; i++)
                            {
                                string recPeriodNumber = Convert.ToString(RecPeriodNumberList[i].PeriodNumber);
                                for (int j = 0; j < cblRecurrencePeriodNumber.Items.Count; j++)
                                {
                                    if (cblRecurrencePeriodNumber.Items[j].Value == recPeriodNumber)
                                    {
                                        cblRecurrencePeriodNumber.Items[j].Selected = true;
                                    }
                                }
                            }
                        }
                    }
                    else if (RecurrenceType.Value == (short)ARTEnums.TaskRecurrenceType.Annually)
                    {
                        List<ReconciliationPeriodInfo> RecPeriodNumberList = oTaskHdrInfo.RecurrencePeriodNumberList;
                        if (RecPeriodNumberList != null && RecPeriodNumberList.Count > 0)
                        {
                            ddlRecurrencePeriodNumber.SelectedValue = RecPeriodNumberList[0].PeriodNumber.Value.ToString();
                        }
                    }
                    else if (RecurrenceType.Value == (short)ARTEnums.TaskRecurrenceType.MultipleRecPeriod)
                    {
                        List<ReconciliationPeriodInfo> RecPeriodNumberList = oTaskHdrInfo.RecurrencePeriodNumberList;
                        if (RecPeriodNumberList != null)
                        {
                            for (int i = 0; i < RecPeriodNumberList.Count; i++)
                            {
                                string recPeriodNumber = Convert.ToString(RecPeriodNumberList[i].PeriodNumber);
                                for (int j = 0; j < cblMRRecurrencePeriodNumber.Items.Count; j++)
                                {
                                    if (cblMRRecurrencePeriodNumber.Items[j].Value == recPeriodNumber)
                                    {
                                        cblMRRecurrencePeriodNumber.Items[j].Selected = true;
                                    }
                                }
                            }
                        }
                    }

                }
            }
            if (oTaskHdrInfo.TaskDueDate.HasValue)
                calTaskDueDate.Text = Helper.GetDisplayDateForCalendar(oTaskHdrInfo.TaskDueDate);

            if (oTaskHdrInfo.AssigneeDueDate.HasValue)
                calAssigneeDueDate.Text = Helper.GetDisplayDateForCalendar(oTaskHdrInfo.AssigneeDueDate);
            if (oTaskHdrInfo.ReviewerDueDate.HasValue)
                calReviewerDueDate.Text = Helper.GetDisplayDateForCalendar(oTaskHdrInfo.ReviewerDueDate);

            if (oTaskHdrInfo.TaskDueDays.HasValue)
                txtCustomTaskDueDays.Text = Helper.GetDisplayIntegerValueWitoutComma(oTaskHdrInfo.TaskDueDays);
            if (oTaskHdrInfo.AssigneeDueDays.HasValue)
                txtCustomAssigneeDueDays.Text = Helper.GetDisplayIntegerValueWitoutComma(oTaskHdrInfo.AssigneeDueDays);
            if (oTaskHdrInfo.ReviewerDueDays.HasValue)
                txtCustomReviewerDueDays.Text = Helper.GetDisplayIntegerValueWitoutComma(oTaskHdrInfo.ReviewerDueDays.Value);


            if (oTaskHdrInfo.AssignedTo != null)
            {
                txtAssignedTo.Text = oTaskHdrInfo.AssignedToUserName;
                for (int i = 0; i < oTaskHdrInfo.AssignedTo.Count; i++)
                {
                    if (string.IsNullOrEmpty(hdnAssignedTo.Value))
                        hdnAssignedTo.Value = oTaskHdrInfo.AssignedTo[0].UserID.ToString();
                    else
                        hdnAssignedTo.Value = hdnAssignedTo.Value + "," + oTaskHdrInfo.AssignedTo[i].UserID.ToString();
                }
            }

            if (oTaskHdrInfo.Reviewer != null)
            {
                txtReviewer.Text = oTaskHdrInfo.ReviewerUserName;
                for (int i = 0; i < oTaskHdrInfo.Reviewer.Count; i++)
                {
                    if (string.IsNullOrEmpty(hdnReviewer.Value))
                        hdnReviewer.Value = oTaskHdrInfo.Reviewer[0].UserID.ToString();
                    else
                        hdnReviewer.Value = hdnReviewer.Value + "," + oTaskHdrInfo.Reviewer[i].UserID.ToString();
                }
            }

            if (oTaskHdrInfo.Approver != null)
            {
                txtApprover.Text = oTaskHdrInfo.ApproverUserName;
                for (int i = 0; i < oTaskHdrInfo.Approver.Count; i++)
                {
                    if (string.IsNullOrEmpty(hdnApprover.Value))
                        hdnApprover.Value = oTaskHdrInfo.Approver[0].UserID.ToString();
                    else
                        hdnApprover.Value = hdnApprover.Value + "," + oTaskHdrInfo.Approver[i].UserID.ToString();
                }
            }

            if (oTaskHdrInfo.Notify != null)
            {
                txtNotify.Text = oTaskHdrInfo.NotifyUserName;
                for (int i = 0; i < oTaskHdrInfo.Notify.Count; i++)
                {
                    if (string.IsNullOrEmpty(hdnNotify.Value))
                        hdnNotify.Value = oTaskHdrInfo.Notify[0].UserID.ToString();
                    else
                        hdnNotify.Value = hdnNotify.Value + "," + oTaskHdrInfo.Notify[i].UserID.ToString();
                }
            }
            if (!string.IsNullOrEmpty(hdnAssignedTo.Value) || !string.IsNullOrEmpty(hdnApprover.Value) || !string.IsNullOrEmpty(hdnNotify.Value) || !string.IsNullOrEmpty(hdnReviewer.Value))
            {
                AccessibleAccountIDList = null;
                AccessibleAccountIDList = TaskMasterHelper.GetAccountIDListByUserIDs(this.GetSelectedUserID(), SessionHelper.CurrentReconciliationPeriodID);
            }
            // Task  Accounts
            List<long> oAccountIDList = (from obj in oTaskHdrInfo.TaskAccount
                                         select obj.AccountID.Value).ToList();
            if (oAccountIDList != null)
            {
                AccountHdrInfoListAdded = TaskMasterHelper.GetTaskAccountHdrInfoList(oAccountIDList, SessionHelper.CurrentReconciliationPeriodID);
            }
            for (int i = 0; i < AccountHdrInfoListAdded.Count; i++)
            {
                AccountHdrInfoListAdded[i].IsTaskCompleted = oTaskHdrInfo.TaskAccount.Find(o => o.AccountID == AccountHdrInfoListAdded[i].AccountID).IsTaskCompleted;
            }
            if (_TaskTypeID == (short)ARTEnums.TaskType.AccountTask)
            {
                BindGrids();
            }
            if (oTaskHdrInfo.TaskDueDaysBasis.HasValue)
                ddlTaskDueDaysBasis.SelectedValue = oTaskHdrInfo.TaskDueDaysBasis.Value.ToString();
            if (oTaskHdrInfo.TaskDueDaysBasisSkipNumber.HasValue)
                txtSkipTaskDueDays.Text = oTaskHdrInfo.TaskDueDaysBasisSkipNumber.Value.ToString();
            if (oTaskHdrInfo.AssigneeDueDaysBasis.HasValue)
                ddlAssigneeDueDaysBasis.SelectedValue = oTaskHdrInfo.AssigneeDueDaysBasis.Value.ToString();
            if (oTaskHdrInfo.AssigneeDueDaysBasisSkipNumber.HasValue)
                txtSkipAssigneeDueDays.Text = oTaskHdrInfo.AssigneeDueDaysBasisSkipNumber.Value.ToString();
            if (oTaskHdrInfo.ReviewerDueDaysBasis.HasValue)
                ddlReviewerDueDaysBasis.SelectedValue = oTaskHdrInfo.ReviewerDueDaysBasis.Value.ToString();
            if (oTaskHdrInfo.ReviewerDueDaysBasisSkipNumber.HasValue)
                txtSkipReviewerDueDays.Text = oTaskHdrInfo.ReviewerDueDaysBasisSkipNumber.Value.ToString();
            if (!string.IsNullOrEmpty(oTaskHdrInfo.CustomField1))
                txtCustomField1.Text = oTaskHdrInfo.CustomField1;
            if (!string.IsNullOrEmpty(oTaskHdrInfo.CustomField2))
                txtCustomField2.Text = oTaskHdrInfo.CustomField2;
            if (oTaskHdrInfo.DaysTypeID.HasValue)
                ddlDaysType.SelectedValue = oTaskHdrInfo.DaysTypeID.Value.ToString();
        }
    }
    private void LoadTaskAttributesInLabelControls()
    {
        TaskHdrInfo oTaskHdrInfo;
        oTaskHdrInfo = TaskMasterHelper.GetTaskHdrInfoByTaskID(_TaskID, SessionHelper.CurrentReconciliationPeriodID);
        if (oTaskHdrInfo != null)
        {
            _TaskRecPeriodEndDate = oTaskHdrInfo.TaskRecPeriodEndDate;
            lblTaskNumberVal.Text = oTaskHdrInfo.TaskNumber;
            lblCreatedByVal.Text = oTaskHdrInfo.AddedBy;
            lblDateCreatedVal.Text = Helper.GetDisplayDate(oTaskHdrInfo.DateAdded);
            lblRecPeriodValue.Text = Helper.GetDisplayDate(oTaskHdrInfo.TaskRecPeriodEndDate);
            List<AttachmentInfo> oAttachmentInfoList = TaskMasterHelper.GetTaskAttachments(_TaskID, ARTEnums.RecordType.TaskCreation);
            if (oAttachmentInfoList != null)
            {
                Session.Remove(SessionConstants.TASK_MASTER_ATTACHMENT);
                Session[SessionConstants.TASK_MASTER_ATTACHMENT] = oAttachmentInfoList;
            }
            FillReconciliationPeriodCheckBoxList(_TaskRecPeriodEndDate);
            FillRecPeriodNumberCheckBoxList();
            lblTaskNameValue.Text = oTaskHdrInfo.TaskName;
            if (oTaskHdrInfo.TaskList.TaskListID.HasValue)
                ddlTaskListName.SelectedValue = oTaskHdrInfo.TaskList.TaskListID.Value.ToString();
            if (oTaskHdrInfo.TaskSubList != null && oTaskHdrInfo.TaskSubList.TaskSubListID.HasValue)
                ddlTaskSubListName.SelectedValue = oTaskHdrInfo.TaskSubList.TaskSubListID.Value.ToString();
            lblDescriptionValue.Text = Helper.GetDisplayStringValue(oTaskHdrInfo.TaskDescription);

            //if (oTaskHdrInfo.RecurrenceTypeID.HasValue)
            if (oTaskHdrInfo.RecurrenceType != null && oTaskHdrInfo.RecurrenceType.TaskRecurrenceTypeID.HasValue)
            {
                short? RecurrenceType = oTaskHdrInfo.RecurrenceType.TaskRecurrenceTypeID;

                if (RecurrenceType.HasValue)
                {
                    ddlRecurrence.SelectedValue = RecurrenceType.Value.ToString();
                    ShowHidePnlRecurrenceType(RecurrenceType.Value);
                    if (RecurrenceType.Value == (short)ARTEnums.TaskRecurrenceType.Custom)
                    {
                        List<ReconciliationPeriodInfo> oRecPeriodIDList = oTaskHdrInfo.RecurrenceFrequency;
                        if (oRecPeriodIDList != null)
                        {
                            for (int i = 0; i < oRecPeriodIDList.Count; i++)
                            {
                                string recPeriod = Convert.ToString(oRecPeriodIDList[i].ReconciliationPeriodID);
                                for (int j = 0; j < cblRecPeriodsCustom.Items.Count; j++)
                                {
                                    if (cblRecPeriodsCustom.Items[j].Value == recPeriod)
                                    {
                                        cblRecPeriodsCustom.Items[j].Selected = true;
                                    }
                                }
                            }
                        }
                    }
                    else if (RecurrenceType.Value == (short)ARTEnums.TaskRecurrenceType.Quarterly)
                    {
                        List<ReconciliationPeriodInfo> RecPeriodNumberList = oTaskHdrInfo.RecurrencePeriodNumberList;
                        if (RecPeriodNumberList != null)
                        {
                            for (int i = 0; i < RecPeriodNumberList.Count; i++)
                            {
                                string recPeriodNumber = Convert.ToString(RecPeriodNumberList[i].PeriodNumber);
                                for (int j = 0; j < cblRecurrencePeriodNumber.Items.Count; j++)
                                {
                                    if (cblRecurrencePeriodNumber.Items[j].Value == recPeriodNumber)
                                    {
                                        cblRecurrencePeriodNumber.Items[j].Selected = true;
                                    }
                                }
                            }
                        }
                    }
                    else if (RecurrenceType.Value == (short)ARTEnums.TaskRecurrenceType.Annually)
                    {
                        List<ReconciliationPeriodInfo> RecPeriodNumberList = oTaskHdrInfo.RecurrencePeriodNumberList;
                        if (RecPeriodNumberList != null && RecPeriodNumberList.Count > 0)
                        {
                            ddlRecurrencePeriodNumber.SelectedValue = RecPeriodNumberList[0].PeriodNumber.Value.ToString();
                        }
                    }
                    else if (RecurrenceType.Value == (short)ARTEnums.TaskRecurrenceType.MultipleRecPeriod)
                    {
                        List<ReconciliationPeriodInfo> RecPeriodNumberList = oTaskHdrInfo.RecurrencePeriodNumberList;
                        if (RecPeriodNumberList != null)
                        {
                            for (int i = 0; i < RecPeriodNumberList.Count; i++)
                            {
                                string recPeriodNumber = Convert.ToString(RecPeriodNumberList[i].PeriodNumber);
                                for (int j = 0; j < cblMRRecurrencePeriodNumber.Items.Count; j++)
                                {
                                    if (cblMRRecurrencePeriodNumber.Items[j].Value == recPeriodNumber)
                                    {
                                        cblMRRecurrencePeriodNumber.Items[j].Selected = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (oTaskHdrInfo.TaskDueDate.HasValue)
                calTaskDueDate.Text = Helper.GetDisplayDateForCalendar(oTaskHdrInfo.TaskDueDate);

            if (oTaskHdrInfo.AssigneeDueDate.HasValue)
                calAssigneeDueDate.Text = Helper.GetDisplayDateForCalendar(oTaskHdrInfo.AssigneeDueDate);

            if (oTaskHdrInfo.ReviewerDueDate.HasValue)
                calReviewerDueDate.Text = Helper.GetDisplayDateForCalendar(oTaskHdrInfo.ReviewerDueDate);

            if (oTaskHdrInfo.TaskDueDays.HasValue)
                lblTaskDueDaysValue.Text = Helper.GetDisplayIntegerValue(oTaskHdrInfo.TaskDueDays);
            if (oTaskHdrInfo.AssigneeDueDays.HasValue)
                lblAssigneeDueDaysValue.Text = Helper.GetDisplayIntegerValue(oTaskHdrInfo.AssigneeDueDays);
            if (oTaskHdrInfo.ReviewerDueDays.HasValue)
                lblReviewerDueDaysValue.Text = Helper.GetDisplayIntegerValue(oTaskHdrInfo.ReviewerDueDays);



            //if (oTaskHdrInfo.AssignedTo != null)
            //{
            //    lblAssignedToValue.Text = oTaskHdrInfo.AssignedTo.Name;
            //    hdnAssignedTo.Value = oTaskHdrInfo.AssignedTo.UserID.ToString();
            //}
            //if (oTaskHdrInfo.Reviewer != null)
            //{
            //    lblApproverValue.Text = oTaskHdrInfo.Reviewer.Name;
            //    hdnApprover.Value = oTaskHdrInfo.Reviewer.UserID.ToString();
            //}

            if (oTaskHdrInfo.AssignedTo != null)
            {
                lblAssignedToValue.Text = oTaskHdrInfo.AssignedToUserName;
                for (int i = 0; i < oTaskHdrInfo.AssignedTo.Count; i++)
                {
                    if (string.IsNullOrEmpty(hdnAssignedTo.Value))
                        hdnAssignedTo.Value = oTaskHdrInfo.AssignedTo[0].UserID.ToString();
                    else
                        hdnAssignedTo.Value = hdnAssignedTo.Value + "," + oTaskHdrInfo.AssignedTo[i].UserID.ToString();
                }
            }

            if (oTaskHdrInfo.Reviewer != null)
            {
                lblReviewerValue.Text = oTaskHdrInfo.ReviewerUserName;
                for (int i = 0; i < oTaskHdrInfo.Reviewer.Count; i++)
                {
                    if (string.IsNullOrEmpty(hdnReviewer.Value))
                        hdnReviewer.Value = oTaskHdrInfo.Reviewer[0].UserID.ToString();
                    else
                        hdnReviewer.Value = hdnReviewer.Value + "," + oTaskHdrInfo.Reviewer[i].UserID.ToString();
                }
            }

            if (oTaskHdrInfo.Approver != null)
            {
                lblApproverValue.Text = oTaskHdrInfo.ApproverUserName;
                for (int i = 0; i < oTaskHdrInfo.Approver.Count; i++)
                {
                    if (string.IsNullOrEmpty(hdnApprover.Value))
                        hdnApprover.Value = oTaskHdrInfo.Approver[0].UserID.ToString();
                    else
                        hdnApprover.Value = hdnApprover.Value + "," + oTaskHdrInfo.Approver[i].UserID.ToString();
                }
            }

            if (oTaskHdrInfo.Notify != null)
            {
                lblNotifyValue.Text = oTaskHdrInfo.NotifyUserName;
                for (int i = 0; i < oTaskHdrInfo.Notify.Count; i++)
                {
                    if (string.IsNullOrEmpty(hdnNotify.Value))
                        hdnNotify.Value = oTaskHdrInfo.Notify[0].UserID.ToString();
                    else
                        hdnNotify.Value = hdnNotify.Value + "," + oTaskHdrInfo.Notify[0].UserID.ToString();
                }
            }
            // Task  Accounts
            List<long> oAccountIDList = (from obj in oTaskHdrInfo.TaskAccount
                                         select obj.AccountID.Value).ToList();
            if (oAccountIDList != null)
            {
                AccountHdrInfoListAdded = TaskMasterHelper.GetTaskAccountHdrInfoList(oAccountIDList, SessionHelper.CurrentReconciliationPeriodID);
            }
            if (_TaskTypeID == (short)ARTEnums.TaskType.AccountTask)
            {
                BindGrids();
            }

            if (oTaskHdrInfo.TaskDueDaysBasis.HasValue)
                ddlTaskDueDaysBasis.SelectedValue = oTaskHdrInfo.TaskDueDaysBasis.Value.ToString();
            if (oTaskHdrInfo.TaskDueDaysBasisSkipNumber.HasValue)
                txtSkipTaskDueDays.Text = oTaskHdrInfo.TaskDueDaysBasisSkipNumber.Value.ToString();
            if (oTaskHdrInfo.AssigneeDueDaysBasis.HasValue)
                ddlAssigneeDueDaysBasis.SelectedValue = oTaskHdrInfo.AssigneeDueDaysBasis.Value.ToString();
            if (oTaskHdrInfo.AssigneeDueDaysBasisSkipNumber.HasValue)
                txtSkipAssigneeDueDays.Text = oTaskHdrInfo.AssigneeDueDaysBasisSkipNumber.Value.ToString();

            if (oTaskHdrInfo.TaskDueDaysBasisSkipNumber.HasValue)
                lblTaskSkipDueDays.Text = oTaskHdrInfo.TaskDueDaysBasisSkipNumber.Value.ToString();
            if (oTaskHdrInfo.AssigneeDueDaysBasisSkipNumber.HasValue)
                lblSkipAssigneeDueDays.Text = oTaskHdrInfo.AssigneeDueDaysBasisSkipNumber.Value.ToString();

            if (oTaskHdrInfo.ReviewerDueDaysBasis.HasValue)
                ddlReviewerDueDaysBasis.SelectedValue = oTaskHdrInfo.ReviewerDueDaysBasis.Value.ToString();
            if (oTaskHdrInfo.ReviewerDueDaysBasisSkipNumber.HasValue)
                lblSkipReviewerDueDays.Text = oTaskHdrInfo.ReviewerDueDaysBasisSkipNumber.Value.ToString();
            if (oTaskHdrInfo.DaysTypeID.HasValue)
                ddlDaysType.SelectedValue = oTaskHdrInfo.DaysTypeID.Value.ToString();

            if (!string.IsNullOrEmpty(oTaskHdrInfo.CustomField1))
                lblCustomField1Value.Text = oTaskHdrInfo.CustomField1;
            if (!string.IsNullOrEmpty(oTaskHdrInfo.CustomField2))
                lblCustomField2Value.Text = oTaskHdrInfo.CustomField2;

        }
    }
    private void ShowHideControls(bool flag)
    {
        this.txtApprover.Visible = flag;
        this.txtAssignedTo.Visible = flag;
        this.txtReviewer.Visible = flag;
        this.txtCustomAssigneeDueDays.Visible = flag;
        this.txtCustomTaskDueDays.Visible = flag;
        this.txtCustomReviewerDueDays.Visible = flag;
        this.txtDescription.Visible = flag;
        this.txtNotify.Visible = flag;
        this.hlAssignedTo.Visible = flag;
        this.hlReviewer.Visible = flag;
        this.hlApprover.Visible = flag;
        this.hlNotify.Visible = flag;
        this.btnClearAssignedTo.Visible = flag;
        this.btnClearReviewer.Visible = flag;
        this.btnClearApprover.Visible = flag;
        this.btnClearNotify.Visible = flag;
        this.txtTaskName.Visible = flag;
        txtSkipTaskDueDays.Visible = flag;
        txtSkipAssigneeDueDays.Visible = flag;
        txtSkipReviewerDueDays.Visible = flag;

        txtCustomField1.Visible = flag;
        txtCustomField2.Visible = flag;

        this.lblApproverValue.Visible = !flag;
        this.lblAssignedToValue.Visible = !flag;
        this.lblReviewerValue.Visible = !flag;
        this.lblAssigneeDueDaysValue.Visible = !flag;
        this.lblTaskDueDaysValue.Visible = !flag;
        this.lblReviewerDueDaysValue.Visible = !flag;
        this.lblDescriptionValue.Visible = !flag;
        this.lblNotifyValue.Visible = !flag;
        this.lblTaskNameValue.Visible = !flag;
        lblSkipAssigneeDueDays.Visible = !flag;
        lblTaskSkipDueDays.Visible = !flag;
        lblSkipReviewerDueDays.Visible = !flag;
        lblCustomField1Value.Visible = !flag;
        lblCustomField2Value.Visible = !flag;
    }
    private void EnableDisableControls(bool flag)
    {
        ddlRecurrence.Enabled = flag;
        ddlTaskListName.Enabled = flag;
        ddlTaskSubListName.Enabled = flag;
        calAssigneeDueDate.Enabled = flag;
        calTaskDueDate.Enabled = flag;
        calReviewerDueDate.Enabled = flag;
        cblRecPeriodsCustom.Enabled = flag;
        ucSkyStemARTGridAccountsAdded.ShowSelectCheckBoxColum = flag;
        btnUpdate.Visible = flag;
        ddlTaskDueDaysBasis.Enabled = flag;
        ddlAssigneeDueDaysBasis.Enabled = flag;
        ddlReviewerDueDaysBasis.Enabled = flag;
        ddlDaysType.Enabled = flag;

    }
    /// <summary>
    /// Get Query String Values
    /// </summary>
    private void GetQueryStringValues()
    {
        if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.TASK_TYPE_ID]))
            _TaskTypeID = short.Parse(Request.QueryString[QueryStringConstants.TASK_TYPE_ID]);
        if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.TASK_ID]))
            _TaskID = long.Parse(Request.QueryString[QueryStringConstants.TASK_ID]);
        if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.TASK_DETAIL_ID]))
            _TaskDetailID = long.Parse(Request.QueryString[QueryStringConstants.TASK_DETAIL_ID]);

    }
    private void SearchAccounts(List<AccountHdrInfo> oAccountHdrInfoCollection)
    {
        try
        {
            AccountSearchCriteria oAccountSearchCriteria = this.GetSearchCriteria();
            AccountHdrInfoListSearchResults = RefineSearchResult(oAccountHdrInfoCollection);
            ucSkyStemARTGridAccountSearchResult.Visible = true;
            ucSkyStemARTGridAccountSearchResult.ShowSelectCheckBoxColum = true;
            int defaultItemCount = Helper.GetDefaultChunkSize(ucSkyStemARTGridAccountSearchResult.Grid.PageSize);
            btnAdd.Visible = true;
            if (AccountHdrInfoListSearchResults.Count == defaultItemCount)
            {
                ucSkyStemARTGridAccountSearchResult.Grid.VirtualItemCount = AccountHdrInfoListSearchResults.Count + 1;
            }
            else
            {
                ucSkyStemARTGridAccountSearchResult.Grid.VirtualItemCount = AccountHdrInfoListSearchResults.Count;
            }
            ucSkyStemARTGridAccountSearchResult.DataSource = AccountHdrInfoListSearchResults;
            ucSkyStemARTGridAccountSearchResult.BindGrid();
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }
    private AccountSearchCriteria GetSearchCriteria()
    {
        var oAccountSearchCriteria = HttpContext.Current.Session[SessionConstants.ACCOUNT_SEARCH_CRITERIA] as AccountSearchCriteria;
        return oAccountSearchCriteria;
    }
    private TaskHdrInfo GetTaskHdrInfo(bool FromRemoveAccounts)
    {
        TaskHdrInfo oTaskHdrInfo = new TaskHdrInfo();
        oTaskHdrInfo.RecPeriodID = SessionHelper.CurrentReconciliationPeriodID;
        oTaskHdrInfo.TaskTypeID = _TaskTypeID;
        oTaskHdrInfo.TempTaskSequenceNumber = 1;
        oTaskHdrInfo.IsActive = true;
        if (FromRemoveAccounts)
        {
            if (_TaskTypeID == (short)ARTEnums.TaskType.AccountTask)
            {
                //Task Account
                oTaskHdrInfo.TaskAccount = getTaskAccounts();
            }
        }
        else
        {

            ValidateTaskOwners();
            // Task List Name
            if (ddlTaskListName.SelectedValue == WebConstants.CREATE_NEW)
            {
                TaskListHdrInfo oTaskListHdrInfo = new TaskListHdrInfo();
                oTaskListHdrInfo.TaskListName = txtNewTaskListName.Text;
                oTaskHdrInfo.TaskList = oTaskListHdrInfo;
            }
            else
            {
                TaskListHdrInfo oTaskListHdrInfo = new TaskListHdrInfo();
                oTaskListHdrInfo.TaskListID = Convert.ToInt16(ddlTaskListName.SelectedValue);
                oTaskHdrInfo.TaskList = oTaskListHdrInfo;
            }
            // Task Sublist Name
            if (ddlTaskSubListName.SelectedValue == WebConstants.CREATE_NEW)
            {
                TaskSubListHdrInfo oTaskSubListHdrInfo = new TaskSubListHdrInfo();
                oTaskSubListHdrInfo.TaskSubListName = txtNewTaskSubListName.Text;
                oTaskHdrInfo.TaskSubList = oTaskSubListHdrInfo;
            }
            else
            {
                TaskSubListHdrInfo oTaskSubListHdrInfo = new TaskSubListHdrInfo();
                oTaskSubListHdrInfo.TaskSubListID = Convert.ToInt32(ddlTaskSubListName.SelectedValue);
                oTaskHdrInfo.TaskSubList = oTaskSubListHdrInfo;
            }
            //Task Name       
            oTaskHdrInfo.TaskName = this.txtTaskName.Text;
            //Description
            oTaskHdrInfo.TaskDescription = this.txtDescription.Text;
            //Assigned To           
            if (!string.IsNullOrEmpty(this.hdnAssignedTo.Value))
            {
                List<UserHdrInfo> oAssignedToUserHdrInfoList = new List<UserHdrInfo>();
                string[] AssignedTo = hdnAssignedTo.Value.Split(',');
                for (int i = 0; i < AssignedTo.Length; i++)
                {
                    UserHdrInfo oAssignedToUserHdrInfo = new UserHdrInfo();
                    oAssignedToUserHdrInfo.UserID = Convert.ToInt32(AssignedTo[i]);
                    oAssignedToUserHdrInfoList.Add(oAssignedToUserHdrInfo);
                }
                oTaskHdrInfo.AssignedTo = oAssignedToUserHdrInfoList;
            }
            //Reviewer          
            if (!string.IsNullOrEmpty(this.hdnReviewer.Value))
            {
                List<UserHdrInfo> oReviewerUserHdrInfoList = new List<UserHdrInfo>();
                string[] Reviewer = hdnReviewer.Value.Split(',');
                for (int i = 0; i < Reviewer.Length; i++)
                {
                    UserHdrInfo oReviewerUserHdrInfo = new UserHdrInfo();
                    oReviewerUserHdrInfo.UserID = Convert.ToInt32(Reviewer[i]);
                    oReviewerUserHdrInfoList.Add(oReviewerUserHdrInfo);
                }
                oTaskHdrInfo.Reviewer = oReviewerUserHdrInfoList;
            }

            //Approver          
            if (!string.IsNullOrEmpty(this.hdnApprover.Value))
            {
                List<UserHdrInfo> oApproverUserHdrInfoList = new List<UserHdrInfo>();
                string[] Approver = hdnApprover.Value.Split(',');
                for (int i = 0; i < Approver.Length; i++)
                {
                    UserHdrInfo oApproverUserHdrInfo = new UserHdrInfo();
                    oApproverUserHdrInfo.UserID = Convert.ToInt32(Approver[i]);
                    oApproverUserHdrInfoList.Add(oApproverUserHdrInfo);
                }
                oTaskHdrInfo.Approver = oApproverUserHdrInfoList;
            }

            //Notify
            if (!string.IsNullOrEmpty(this.hdnNotify.Value))
            {
                List<UserHdrInfo> oNotifyUserHdrInfoList = new List<UserHdrInfo>();
                string[] Notiy = hdnNotify.Value.Split(',');
                for (int i = 0; i < Notiy.Length; i++)
                {
                    UserHdrInfo oNotiyUserHdrInfo = new UserHdrInfo();
                    oNotiyUserHdrInfo.UserID = Convert.ToInt32(Notiy[i]);
                    oNotifyUserHdrInfoList.Add(oNotiyUserHdrInfo);
                }
                oTaskHdrInfo.Notify = oNotifyUserHdrInfoList;
            }
            //Recurrence Type
            TaskRecurrenceTypeMstInfo oTaskRecurr = new TaskRecurrenceTypeMstInfo();
            oTaskRecurr.TaskRecurrenceTypeID = Convert.ToInt16(ddlRecurrence.SelectedValue);
            oTaskHdrInfo.RecurrenceType = oTaskRecurr;

            if (Convert.ToInt16(ddlRecurrence.SelectedValue) == (short)ARTEnums.TaskRecurrenceType.NoRecurrence)
            {

                //Task Due Date
                oTaskHdrInfo.TaskDueDate = Convert.ToDateTime(calTaskDueDate.Text);
                //Assignee Due Date
                if (!string.IsNullOrEmpty(calAssigneeDueDate.Text) && !string.IsNullOrEmpty(this.hdnReviewer.Value))
                    oTaskHdrInfo.AssigneeDueDate = Convert.ToDateTime(calAssigneeDueDate.Text);
                //Reviewer Due Date
                if (!string.IsNullOrEmpty(calReviewerDueDate.Text) && !string.IsNullOrEmpty(this.hdnApprover.Value))
                    oTaskHdrInfo.ReviewerDueDate = Convert.ToDateTime(calReviewerDueDate.Text);
            }
            else
            {
                //Task Due Days Basis
                oTaskHdrInfo.TaskDueDaysBasis = Convert.ToInt16(ddlTaskDueDaysBasis.SelectedValue);
                //Task Due Days Basis Skip Number
                if (string.IsNullOrEmpty(txtSkipTaskDueDays.Text))
                    oTaskHdrInfo.TaskDueDaysBasisSkipNumber = 0;
                else
                    oTaskHdrInfo.TaskDueDaysBasisSkipNumber = Convert.ToInt16(txtSkipTaskDueDays.Text);

                //Assignee Due Days Basis
                oTaskHdrInfo.AssigneeDueDaysBasis = Convert.ToInt16(ddlAssigneeDueDaysBasis.SelectedValue);
                //Assignee Due Days Basis Skip Number
                if (string.IsNullOrEmpty(txtSkipAssigneeDueDays.Text))
                    oTaskHdrInfo.AssigneeDueDaysBasisSkipNumber = 0;
                else
                    oTaskHdrInfo.AssigneeDueDaysBasisSkipNumber = Convert.ToInt16(txtSkipAssigneeDueDays.Text);

                //Reviewer Due Days Basis
                oTaskHdrInfo.ReviewerDueDaysBasis = Convert.ToInt16(ddlReviewerDueDaysBasis.SelectedValue);
                //Reviewer Due Days Basis Skip Number
                if (string.IsNullOrEmpty(txtSkipReviewerDueDays.Text))
                    oTaskHdrInfo.ReviewerDueDaysBasisSkipNumber = 0;
                else
                    oTaskHdrInfo.ReviewerDueDaysBasisSkipNumber = Convert.ToInt16(txtSkipReviewerDueDays.Text);

                //Task Due Days
                oTaskHdrInfo.TaskDueDays = Convert.ToInt32(txtCustomTaskDueDays.Text);
                //Assignee Due Days  
                if (!string.IsNullOrEmpty(txtCustomAssigneeDueDays.Text) && !string.IsNullOrEmpty(this.hdnReviewer.Value))
                    oTaskHdrInfo.AssigneeDueDays = Convert.ToInt32(txtCustomAssigneeDueDays.Text);
                //Reviewer Due Days  
                if (!string.IsNullOrEmpty(txtCustomReviewerDueDays.Text) && !string.IsNullOrEmpty(this.hdnApprover.Value))
                    oTaskHdrInfo.ReviewerDueDays = Convert.ToInt32(txtCustomReviewerDueDays.Text);

                oTaskHdrInfo.DaysTypeID = Convert.ToInt16(ddlDaysType.SelectedValue);

            }

            if (Convert.ToInt16(ddlRecurrence.SelectedValue) == (short)ARTEnums.TaskRecurrenceType.Custom)
            {
                //Recurrence Frequency
                oTaskHdrInfo.RecurrenceFrequency = GetRecurrenceFrequency();

            }
            else if (Convert.ToInt16(ddlRecurrence.SelectedValue) == (short)ARTEnums.TaskRecurrenceType.Quarterly)
            {
                oTaskHdrInfo.RecurrencePeriodNumberList = GetQuarterlyRecurrencePeriodNumberList();
            }
            else if (Convert.ToInt16(ddlRecurrence.SelectedValue) == (short)ARTEnums.TaskRecurrenceType.Annually)
            {
                oTaskHdrInfo.RecurrencePeriodNumberList = GetYearlyRecurrencePeriodNumberList();
            }
            else if (Convert.ToInt16(ddlRecurrence.SelectedValue) == (short)ARTEnums.TaskRecurrenceType.MultipleRecPeriod)
            {
                oTaskHdrInfo.RecurrencePeriodNumberList = GetMRRecurrencePeriodNumberList();
            }
            if (_TaskTypeID == (short)ARTEnums.TaskType.AccountTask)
            {
                //Task Account
                oTaskHdrInfo.TaskAccount = getTaskAccounts();
            }
            //CustomField1
            if (!string.IsNullOrEmpty(txtCustomField1.Text))
            {
                oTaskHdrInfo.CustomField1 = txtCustomField1.Text;
            }
            //CustomField2
            if (!string.IsNullOrEmpty(txtCustomField2.Text))
            {
                oTaskHdrInfo.CustomField2 = txtCustomField2.Text;
            }


        }
        return oTaskHdrInfo;
    }
    private List<AccountHdrInfo> getTaskAccounts()
    {
        List<AccountHdrInfo> oAccountHdrInfoList = new List<AccountHdrInfo>();
        foreach (GridDataItem item in ucSkyStemARTGridAccountsAdded.Grid.Items)
        {
            long accountID = long.Parse(item.GetDataKeyValue("AccountID").ToString());
            AccountHdrInfo oAccount = null;//new AccountHdrInfo();
            oAccount = AccountHdrInfoListAdded.Find(r => r.AccountID == accountID);
            //oAccount.AccountID = accountID;
            oAccountHdrInfoList.Add(oAccount);
        }
        return oAccountHdrInfoList;

    }
    private void ShowHideTaskListName(bool flag)
    {
        lblNewTaskListName.Visible = flag;
        txtNewTaskListName.Visible = flag;
        txtNewTaskListName.Text = "";
    }
    private void ShowHideTaskSubListName(bool flag)
    {
        lblNewTaskSubListName.Visible = flag;
        txtNewTaskSubListName.Visible = flag;
        txtNewTaskSubListName.Text = "";
    }
    private void ShowAccountPanel(bool flag)
    {
        pnlTaskAccounts.Visible = flag;
        pnlSearchGrid.Visible = flag;
        btnRemoveAccount.Visible = flag;
    }
    private void ValidateTaskOwners()
    {
        if (string.IsNullOrEmpty(txtApprover.Text))
            hdnApprover.Value = "";
        if (string.IsNullOrEmpty(txtReviewer.Text))
            hdnReviewer.Value = "";
        if (string.IsNullOrEmpty(txtAssignedTo.Text))
            hdnAssignedTo.Value = "";
        if (string.IsNullOrEmpty(txtNotify.Text))
            hdnNotify.Value = "";
    }
    private void ShowHidePnlRecurrenceType(short RecurrenceType)
    {
        if (RecurrenceType == (short)ARTEnums.TaskRecurrenceType.NoRecurrence)
        {
            PanelDueDate.Visible = true;
            PanelDueDays.Visible = false;
            pnlCustomRecurrenceType.Visible = false;
            pnlRecurrenceQuarterlyType.Visible = false;
            pnlRecurrenceYearlyType.Visible = false;
            pnlRecurrenceMRType.Visible = false;
        }
        else if (RecurrenceType == (short)ARTEnums.TaskRecurrenceType.EveryRecPeriod)
        {
            PanelDueDate.Visible = false;
            PanelDueDays.Visible = true;
            pnlCustomRecurrenceType.Visible = false;
            pnlRecurrenceQuarterlyType.Visible = false;
            pnlRecurrenceYearlyType.Visible = false;
            pnlRecurrenceMRType.Visible = false;
        }
        else if (RecurrenceType == (short)ARTEnums.TaskRecurrenceType.Custom)
        {
            PanelDueDate.Visible = false;
            PanelDueDays.Visible = true;
            pnlCustomRecurrenceType.Visible = true;
            pnlRecurrenceQuarterlyType.Visible = false;
            pnlRecurrenceYearlyType.Visible = false;
            pnlRecurrenceMRType.Visible = false;
        }
        else if (RecurrenceType == (short)ARTEnums.TaskRecurrenceType.Quarterly)
        {
            PanelDueDate.Visible = false;
            PanelDueDays.Visible = true;
            pnlCustomRecurrenceType.Visible = false;
            pnlRecurrenceQuarterlyType.Visible = true;
            pnlRecurrenceYearlyType.Visible = false;
            pnlRecurrenceMRType.Visible = false;
        }
        else if (RecurrenceType == (short)ARTEnums.TaskRecurrenceType.Annually)
        {
            PanelDueDate.Visible = false;
            PanelDueDays.Visible = true;
            pnlCustomRecurrenceType.Visible = false;
            pnlRecurrenceQuarterlyType.Visible = false;
            pnlRecurrenceYearlyType.Visible = true;
            pnlRecurrenceMRType.Visible = false;
        }
        else if (RecurrenceType == (short)ARTEnums.TaskRecurrenceType.MultipleRecPeriod)
        {
            PanelDueDate.Visible = false;
            PanelDueDays.Visible = true;
            pnlCustomRecurrenceType.Visible = false;
            pnlRecurrenceQuarterlyType.Visible = false;
            pnlRecurrenceYearlyType.Visible = false;
            pnlRecurrenceMRType.Visible = true;
        }
    }
    private List<AccountHdrInfo> GetSelectedTaskAccount()
    {
        List<AccountHdrInfo> oRemoveTaskAccountHdrInfoCollection = new List<AccountHdrInfo>();
        foreach (GridDataItem item in ucSkyStemARTGridAccountsAdded.Grid.SelectedItems)
        {
            int accountID = int.Parse(item.GetDataKeyValue("AccountID").ToString());
            if (AccountHdrInfoListAdded != null && AccountHdrInfoListAdded.Count > 0)
            {
                oRemoveTaskAccountHdrInfoCollection.Add(AccountHdrInfoListAdded.Find(T => T.AccountID == accountID));
            }
        }
        return oRemoveTaskAccountHdrInfoCollection;
    }
    private void RemoveAccountFromTaskAccount(List<AccountHdrInfo> oRemoveTaskAccountHdrInfoCollection)
    {
        //if rows selected in top grid
        if (oRemoveTaskAccountHdrInfoCollection.Count > 0)
        {
            AccountHdrInfoListAdded.RemoveAll(obj => oRemoveTaskAccountHdrInfoCollection.Exists(objr => objr.AccountID == obj.AccountID) == true);
            //Add selected row to SearchResults
            if (AccountHdrInfoListSearchResults != null)
                AccountHdrInfoListSearchResults.AddRange(oRemoveTaskAccountHdrInfoCollection);
            BindGrids();
        }
        else
        {
            throw new ARTException(5000186);
        }
    }
    private void AddAccount()
    {
        try
        {
            List<int> lstSelectedAccountId = new List<int>();
            if (AccountHdrInfoListAdded == null)
                AccountHdrInfoListAdded = new List<AccountHdrInfo>();
            foreach (GridDataItem item in ucSkyStemARTGridAccountSearchResult.Grid.SelectedItems)
            {
                int accountID = int.Parse(item.GetDataKeyValue("AccountID").ToString());
                AccountHdrInfo oAccountHdrInfo = AccountHdrInfoListSearchResults.Find(r => r.AccountID == accountID);

                AccountHdrInfo oAddedAccountHdrInfo = AccountHdrInfoListAdded.Find(r => r.AccountID == accountID);
                if (oAddedAccountHdrInfo == null)
                {
                    AccountHdrInfoListAdded.Add(oAccountHdrInfo);
                    lstSelectedAccountId.Add(accountID);
                }
            }
            AccountHdrInfoListSearchResults.RemoveAll(obj => lstSelectedAccountId.Contains((int)obj.AccountID) == true);
            BindGrids();
        }
        catch (ARTException ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }
    private void UpdateTask(bool FromRemoveAccounts)
    {
        try
        {
            // Get Initial TaskHdrInfo for task
            TaskHdrInfo oTaskHdrInfoOriginal;
            oTaskHdrInfoOriginal = TaskMasterHelper.GetTaskHdrInfoByTaskID(_TaskID, SessionHelper.CurrentReconciliationPeriodID);

            // Task  Accounts
            if (_TaskTypeID == (short)ARTEnums.TaskType.AccountTask)
            {
                List<long> oAccIDList = (from obj in oTaskHdrInfoOriginal.TaskAccount
                                         select obj.AccountID.Value).ToList();
                TaskMasterHelper.GetTaskAccountHdrInfoList(oAccIDList, SessionHelper.CurrentReconciliationPeriodID);
                oTaskHdrInfoOriginal.TaskAccount = TaskMasterHelper.GetTaskAccountHdrInfoList(oAccIDList, SessionHelper.CurrentReconciliationPeriodID);
            }
            List<int> assignedAssignedToIDCollection = new List<int>();
            List<int> assignedReviewerIDCollection = new List<int>();
            List<int> assignedApproverIDCollection = new List<int>();
            List<int> assignedNotifyIDCollection = new List<int>();

            List<int> oldAssignedAssignedToIDCollection = new List<int>();
            List<int> oldAssignedReviewerIDCollection = new List<int>();
            List<int> oldAssignedApproverIDCollection = new List<int>();
            List<int> oldAssignedNotifyIDCollection = new List<int>();

            TaskHdrInfo oTaskHdrInfo = this.GetTaskHdrInfo(FromRemoveAccounts);
            oTaskHdrInfo.TaskID = _TaskID;
            //Added to retain tasknumber
            oTaskHdrInfo.TaskNumber = oTaskHdrInfoOriginal.TaskNumber;
            //oTaskHdrInfo.IsDeleted = false;
            oTaskHdrInfo.CreationAttachment = GetAttechmentInfoList();
            oTaskHdrInfo.RevisedBy = SessionHelper.CurrentUserLoginID;
            oTaskHdrInfo.DateRevised = System.DateTime.Now;
            List<TaskHdrInfo> oTaskHdrInfoList = new List<TaskHdrInfo>();
            oTaskHdrInfoList.Add(oTaskHdrInfo);
            TaskMasterHelper.EditTask(oTaskHdrInfoList, SessionHelper.CurrentReconciliationPeriodID.GetValueOrDefault(), SessionHelper.CurrentUserLoginID, System.DateTime.Now);

            //Check AssignedTo has changed if yes send alert to new AssignedTo
            GetUsersForTaskAlert(oTaskHdrInfo.AssignedTo, oTaskHdrInfoOriginal.AssignedTo, assignedAssignedToIDCollection, oldAssignedAssignedToIDCollection);
            //Check Reviewer has changed if yes send alert to new Reviewer
            GetUsersForTaskAlert(oTaskHdrInfo.Reviewer, oTaskHdrInfoOriginal.Reviewer, assignedReviewerIDCollection, oldAssignedReviewerIDCollection);
            //Check Approver has changed if yes send alert to new Approver
            GetUsersForTaskAlert(oTaskHdrInfo.Approver, oTaskHdrInfoOriginal.Approver, assignedApproverIDCollection, oldAssignedApproverIDCollection);
            //Check Notify has changed if yes send alert to new Notify
            GetUsersForTaskAlert(oTaskHdrInfo.Notify, oTaskHdrInfoOriginal.Notify, assignedNotifyIDCollection, oldAssignedNotifyIDCollection);


            AlertHelper.RaiseAlertForAssignedTask(assignedAssignedToIDCollection, assignedReviewerIDCollection, assignedApproverIDCollection, assignedNotifyIDCollection, oTaskHdrInfoList);

            List<TaskHdrInfo> oListTaskHdrInfoOriginal = new List<TaskHdrInfo>();
            oListTaskHdrInfoOriginal.Add(oTaskHdrInfoOriginal);
            AlertHelper.RaiseAlertForUnAssignedTask(oldAssignedAssignedToIDCollection, oldAssignedReviewerIDCollection, oldAssignedApproverIDCollection, oldAssignedNotifyIDCollection, oListTaskHdrInfoOriginal);
        }
        catch (ARTException ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }

    private void GetUsersForTaskAlert(List<UserHdrInfo> oNewUserList, List<UserHdrInfo> oOldUserList, List<int> oAssignedUserIDList, List<int> oUnAssignedUserIDList)
    {
        if (oNewUserList != null && oNewUserList.Count > 0)
        {
            for (int i = 0; i < oNewUserList.Count; i++)
            {
                if (oOldUserList != null && oOldUserList.Count > 0)
                {
                    var OldUser = oOldUserList.Find(obj => obj.UserID.GetValueOrDefault() == oNewUserList[i].UserID.GetValueOrDefault());
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
                    var NewUser = oNewUserList.Find(obj => obj.UserID.GetValueOrDefault() == oOldUserList[i].UserID.GetValueOrDefault());
                    if (NewUser == null && oOldUserList[i].UserID.HasValue && !oUnAssignedUserIDList.Contains(Convert.ToInt32(oOldUserList[i].UserID.Value)))
                        oUnAssignedUserIDList.Add(Convert.ToInt32(oOldUserList[i].UserID.Value));
                }
            }
        }
    }
    private void BindGrids()
    {
        if (AccountHdrInfoListSearchResults == null)
            AccountHdrInfoListSearchResults = new List<AccountHdrInfo>();
        if (AccountHdrInfoListAdded == null)
            AccountHdrInfoListAdded = new List<AccountHdrInfo>();
        ucSkyStemARTGridAccountSearchResult.DataSource = AccountHdrInfoListSearchResults;
        ucSkyStemARTGridAccountSearchResult.BindGrid();
        ucSkyStemARTGridAccountsAdded.DataSource = AccountHdrInfoListAdded;
        ucSkyStemARTGridAccountsAdded.BindGrid();
    }
    private void ClearGrids()
    {
        AccountHdrInfoListAdded = null;
        AccountHdrInfoListSearchResults = null;
    }
    private void FillReconciliationPeriodCheckBoxList(DateTime? TaskRecPeriodEndDate)
    {
        ListControlHelper.BindReconciliationPeriodForTaskMaster(cblRecPeriodsCustom, null, TaskRecPeriodEndDate);
    }
    private void FillRecPeriodNumberCheckBoxList()
    {
        ListControlHelper.BindRecPeriodNumberForTaskMaster(cblMRRecurrencePeriodNumber, ddlRecurrencePeriodNumber, cblRecurrencePeriodNumber, SessionHelper.CurrentCompanyID, SessionHelper.CurrentReconciliationPeriodEndDate);
    }
    private void FillReconciliationPeriodCheckBoxListAdd()
    {
        ListControlHelper.BindReconciliationPeriodForRiskRating(cblRecPeriodsCustom, SessionHelper.CurrentFinancialYearID);
    }
    private void BindDdlRecurrence()
    {
        ListControlHelper.BindRecurrenceDropdown(ddlRecurrence);
    }
    private void BindddlTaskListName()
    {
        ListControlHelper.BindTaskListNameDropdown(ddlTaskListName);
    }
    private void BindddlTaskSubListName()
    {
        ListControlHelper.BindTaskSubListNameDropdown(ddlTaskSubListName);
    }
    private void BindddlDueDaysBasis()
    {
        ListControlHelper.BindDueDaysBasisDropdown(ddlTaskDueDaysBasis, ddlAssigneeDueDaysBasis, ddlReviewerDueDaysBasis);
    }
    private void BindddlDaysType()
    {
        ListControlHelper.BindddlDaysType(ddlDaysType, true);
    }

    private void SetTaskCustomField()
    {
        List<TaskCustomFieldInfo> oTaskCustomFieldInfoList = TaskMasterHelper.GetTaskCustomFields(SessionHelper.CurrentReconciliationPeriodID, SessionHelper.CurrentCompanyID);
        if (oTaskCustomFieldInfoList != null && oTaskCustomFieldInfoList.Count > 0)
        {

            string CustomField1 = oTaskCustomFieldInfoList.Find(obj => obj.TaskCustomFieldID.GetValueOrDefault() == (short)WebEnums.TaskCustomField.CustomField1).CustomFieldValue;
            if (!string.IsNullOrEmpty(CustomField1))
            {
                lblCustomField1.Text = CustomField1;

                //txtCustomField1.Visible = true;
                //lblCustomField1Value.Visible = true;
            }
            else
            {
                lblCustomField1.Text = "";
                txtCustomField1.Visible = false;
                lblCustomField1Value.Visible = false;
            }

            string CustomField2 = oTaskCustomFieldInfoList.Find(obj => obj.TaskCustomFieldID.GetValueOrDefault() == (short)WebEnums.TaskCustomField.CustomField2).CustomFieldValue;
            if (!string.IsNullOrEmpty(CustomField2))
            {
                lblCustomField2.Text = CustomField2;
                //txtCustomField2.Visible = true;
                //lblCustomField2Value.Visible = true;
            }
            else
            {
                lblCustomField2.Text = "";
                txtCustomField2.Visible = false;
                lblCustomField2Value.Visible = false;
            }
        }
        else
        {
            lblCustomField1.Text = "";
            txtCustomField1.Visible = false;
            lblCustomField1Value.Visible = false;
            lblCustomField2.Text = "";
            txtCustomField2.Visible = false;
            lblCustomField2Value.Visible = false;
        }
    }
    private List<int> GetSelectedUserID()
    {
        List<int> UserIDList = new List<int>();

        if (!string.IsNullOrEmpty(this.hdnAssignedTo.Value))
        {
            AddUser(hdnAssignedTo.Value, UserIDList);
        }
        if (!string.IsNullOrEmpty(this.hdnReviewer.Value))
        {
            AddUser(hdnReviewer.Value, UserIDList);
        }
        if (!string.IsNullOrEmpty(this.hdnApprover.Value))
        {
            AddUser(hdnApprover.Value, UserIDList);
        }
        if (!string.IsNullOrEmpty(this.hdnNotify.Value))
        {
            AddUser(hdnNotify.Value, UserIDList);
        }
        return UserIDList;
    }
    private void AddUser(string UserIDString, List<int> UserIDList)
    {
        string[] UserIDs = UserIDString.Split(',');
        for (int i = 0; i < UserIDs.Length; i++)
        {
            int UserID = Convert.ToInt32(UserIDs[i]);
            UserIDList.Add(UserID);
        }
    }
    private List<AccountHdrInfo> RefineSearchResult(List<AccountHdrInfo> oAccountHdrInfoCollection)
    {
        List<AccountHdrInfo> oRefineAccountHdrInfoCollection = oAccountHdrInfoCollection;
        if ((oAccountHdrInfoCollection != null && AccessibleAccountIDList != null) && (!string.IsNullOrEmpty(hdnAssignedTo.Value) || !string.IsNullOrEmpty(hdnApprover.Value) || !string.IsNullOrEmpty(hdnNotify.Value)))
        {
            oRefineAccountHdrInfoCollection = (from oAccountHdrInfo in oAccountHdrInfoCollection
                                               join oAccountID in AccessibleAccountIDList on oAccountHdrInfo.AccountID equals oAccountID
                                               select oAccountHdrInfo).ToList();
        }
        if (AccountHdrInfoListAdded != null && AccountHdrInfoListAdded.Count > 0 && oRefineAccountHdrInfoCollection != null && oRefineAccountHdrInfoCollection.Count > 0)
        {
            oRefineAccountHdrInfoCollection.RemoveAll(obj => AccountHdrInfoListAdded.Exists(objr => objr.AccountID == obj.AccountID) == true);
        }
        return oRefineAccountHdrInfoCollection;
    }
    private void SetErrorMessages()
    {
        // Set Error Messages
        txtNewTaskListName.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 2579);
        txtTaskName.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 2545);
        rfvTaskListName.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 2584);
        rfvCalenderTaskDueDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 2566);
        txtCustomTaskDueDays.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 2582);
        txtCustomAssigneeDueDays.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 2570);
        string errorMessage = LanguageUtil.GetValue(5000342);
        cvcalTaskDueDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateFormatField, 2566);
        cvcalAssigneeDueDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateFormatField, 2567);
        cvReviewerDueDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateFormatField, 1418);
        string errorMessage2 = LanguageUtil.GetValue(5000354);
        string str = string.Format(errorMessage2, LanguageUtil.GetValue(2564), LanguageUtil.GetValue(1131), LanguageUtil.GetValue(1132), LanguageUtil.GetValue(2525));
        // cvApproverAssignedTo.ErrorMessage = str;
        cvTaskUser.ErrorMessage = str;
        cvApprover.ErrorMessage = LanguageUtil.GetValue(2959);
        string errorMessage3 = LanguageUtil.GetValue(5000356);
        cvAssigneeDD.ErrorMessage = string.Format(errorMessage3, LanguageUtil.GetValue(2567), LanguageUtil.GetValue(1131));
        cvReviewerDD.ErrorMessage = string.Format(errorMessage3, LanguageUtil.GetValue(1418), LanguageUtil.GetValue(1132));
        cvAssigneeDueDays.ErrorMessage = string.Format(errorMessage3, LanguageUtil.GetValue(2570), LanguageUtil.GetValue(1132));
        cvAssigneeDueDaysBasis.ErrorMessage = string.Format(errorMessage3, LanguageUtil.GetValue(2778), LanguageUtil.GetValue(1131));
        cvReviewerDueDaysBasis.ErrorMessage = string.Format(errorMessage3, LanguageUtil.GetValue(2947), LanguageUtil.GetValue(1132));
        string errorMessageAssignedTo = LanguageUtil.GetValue(5000359);
        cvAssignedTo.ErrorMessage = string.Format(errorMessageAssignedTo, LanguageUtil.GetValue(2564));
        cvcblRecPeriods.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 2565);
        cvcblRecPeriodNumbers.ErrorMessage = LanguageUtil.GetValue(2739);
        //string errorMessage4 = LanguageUtil.GetValue(5000357);
        // cvValidateAssigneeDueDays.ErrorMessage = string.Format(errorMessage4, LanguageUtil.GetValue(2570));
        // cvValidateTaskDueDays.ErrorMessage = string.Format(errorMessage4, LanguageUtil.GetValue(2582));
        //////cvApprover.ErrorMessage = string.Format(LanguageUtil.GetValue(5000358), LanguageUtil.GetValue(1132));// "cannot contains invalid text or No Records Found. ;
        string errorMessage5 = LanguageUtil.GetValue(5000353);
        cvTaskAccountsAssignedTo.ErrorMessage = string.Format(errorMessage5, LanguageUtil.GetValue(2649));
        cvTaskAccountsReviewer.ErrorMessage = string.Format(errorMessage5, LanguageUtil.GetValue(1131));
        cvTaskAccountsApprover.ErrorMessage = string.Format(errorMessage5, LanguageUtil.GetValue(1132));
        cvTaskAccountsNotify.ErrorMessage = string.Format(errorMessage5, LanguageUtil.GetValue(2525));
        cvTaskListName.ErrorMessage = LanguageUtil.GetValue(5000363);
        cvComparecalTaskDueDateWithCurrentDate.Attributes.Add("dateToCompare", DateTime.Now.ToShortDateString());
        cvComparecalTaskDueDateWithCurrentDate.ErrorMessage = LanguageUtil.GetValue(2737);
        cvComparecalAssigneeDueDateWithCurrentDate.Attributes.Add("dateToCompare", DateTime.Now.ToShortDateString());
        cvComparecalAssigneeDueDateWithCurrentDate.ErrorMessage = LanguageUtil.GetValue(2738);
        cvComparecalReviewerDueDateWithCurrentDate.Attributes.Add("dateToCompare", DateTime.Now.ToShortDateString());
        cvComparecalReviewerDueDateWithCurrentDate.ErrorMessage = LanguageUtil.GetValue(2668);
        rfvRecurrencePeriodNumber.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 2735);
        cvcblMRRecPeriodNumbers.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 2735);
        cvValidateTaskDates.ErrorMessage = string.Format(errorMessage, LanguageUtil.GetValue(2585));
        //cvValidateTaskDates.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateCompareFieldGreaterThan, 2566, 2567);
        rfvTaskDueDaysBasis.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 2777);
        rfvDaysType.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 2963);

    }

    private bool DaysInOrder(List<int> daysList)
    {
        bool retval = true;
        for (int i = 1; i < daysList.Count; i++)
        {
            int dyMin = daysList[i - 1];
            int dyMax = daysList[i];
            if (dyMin > dyMax)
            {
                retval = false;
                break;
            }
        }
        return retval;
    }
    private List<AttachmentInfo> GetAttechmentInfoList()
    {
        return Session[SessionConstants.TASK_MASTER_ATTACHMENT] as List<AttachmentInfo>;
    }
    private List<ReconciliationPeriodInfo> GetRecurrenceFrequency()
    {
        List<ReconciliationPeriodInfo> oReconciliationPeriodInfolist = new List<ReconciliationPeriodInfo>();
        for (int i = 0; i < cblRecPeriodsCustom.Items.Count; i++)
        {
            // if selected
            if (cblRecPeriodsCustom.Items[i].Selected == true)
            {
                ReconciliationPeriodInfo oRecPeriod = new ReconciliationPeriodInfo();
                oRecPeriod.ReconciliationPeriodID = Convert.ToInt32(cblRecPeriodsCustom.Items[i].Value);
                oReconciliationPeriodInfolist.Add(oRecPeriod);
            }
        }
        return oReconciliationPeriodInfolist;
    }
    private List<ReconciliationPeriodInfo> GetQuarterlyRecurrencePeriodNumberList()
    {
        List<ReconciliationPeriodInfo> oReconciliationPeriodInfolist = new List<ReconciliationPeriodInfo>();
        for (int i = 0; i < cblRecurrencePeriodNumber.Items.Count; i++)
        {
            // if selected
            if (cblRecurrencePeriodNumber.Items[i].Selected == true)
            {
                ReconciliationPeriodInfo oRecPeriod = new ReconciliationPeriodInfo();
                oRecPeriod.PeriodNumber = Convert.ToInt16(cblRecurrencePeriodNumber.Items[i].Value);
                oReconciliationPeriodInfolist.Add(oRecPeriod);
            }
        }
        return oReconciliationPeriodInfolist;
    }
    private List<ReconciliationPeriodInfo> GetMRRecurrencePeriodNumberList()
    {
        List<ReconciliationPeriodInfo> oReconciliationPeriodInfolist = new List<ReconciliationPeriodInfo>();
        for (int i = 0; i < cblMRRecurrencePeriodNumber.Items.Count; i++)
        {
            // if selected
            if (cblMRRecurrencePeriodNumber.Items[i].Selected == true)
            {
                ReconciliationPeriodInfo oRecPeriod = new ReconciliationPeriodInfo();
                oRecPeriod.PeriodNumber = Convert.ToInt16(cblMRRecurrencePeriodNumber.Items[i].Value);
                oReconciliationPeriodInfolist.Add(oRecPeriod);
            }
        }
        return oReconciliationPeriodInfolist;
    }
    private List<ReconciliationPeriodInfo> GetYearlyRecurrencePeriodNumberList()
    {
        List<ReconciliationPeriodInfo> oReconciliationPeriodInfolist = new List<ReconciliationPeriodInfo>();
        ReconciliationPeriodInfo oRecPeriod = new ReconciliationPeriodInfo();
        oRecPeriod.PeriodNumber = Convert.ToInt16(ddlRecurrencePeriodNumber.SelectedValue);
        oReconciliationPeriodInfolist.Add(oRecPeriod);
        return oReconciliationPeriodInfolist;
    }


    private bool IsDateOnHoliday(string date, int rowRecPeriodID)
    {
        DateTime dateTime = Convert.ToDateTime(date);
        bool isDateOnHoliday = false;
        short? selectedDateID = 0;
        selectedDateID = (short)dateTime.DayOfWeek;

        oWeekDayMstInfoCollection = SessionHelper.GetAllWeekDays();
        IList<CompanyWeekDayInfo> oCompanyWorkWeekInfoCollection = null;
        oCompanyWorkWeekInfoCollection = SetCompanyWorkWeekByRecPeriodID(rowRecPeriodID);

        short? WeekDayID = null;
        if (oCompanyWorkWeekInfoCollection != null)
            WeekDayID = (from oCompanyWorkWeekInfo in oCompanyWorkWeekInfoCollection
                         where oCompanyWorkWeekInfo.WeekDayID ==
                         ((from oWeekDayMstInfo in this.oWeekDayMstInfoCollection
                           where oWeekDayMstInfo.WeekDayNumber == selectedDateID
                           select oWeekDayMstInfo.WeekDayID).FirstOrDefault())
                         select oCompanyWorkWeekInfo.WeekDayID).FirstOrDefault();

        if (WeekDayID != null)
        {


            if (oHolidayCalendarInfoCollection == null)
            {
                IHolidayCalendar oHolidayCalendarClient = RemotingHelper.GetHolidayCalendarObject();
                oHolidayCalendarInfoCollection = oHolidayCalendarClient.GetHolidayCalendarByCompanyID(SessionHelper.CurrentCompanyID.Value, Helper.GetAppUserInfo());
            }
            foreach (HolidayCalendarInfo oHolidayCalendarInfo in oHolidayCalendarInfoCollection)
            {
                if (oHolidayCalendarInfo.HolidayDate.HasValue && (oHolidayCalendarInfo.HolidayDate.Value.ToShortDateString() == dateTime.ToShortDateString()))
                {
                    isDateOnHoliday = true;
                    return isDateOnHoliday;
                }
            }
        }
        else
        {
            isDateOnHoliday = true;
            return isDateOnHoliday;

        }

        return isDateOnHoliday;
    }
    private IList<CompanyWeekDayInfo> SetCompanyWorkWeekByRecPeriodID(int RecPeriodID)
    {
        int? financialYearID = null;
        financialYearID = SessionHelper.CurrentFinancialYearID;
        if (ViewState["CompanyWorkWeekInfoCollectionForFincncialyear"] == null)
        {
            // Fetch from DB
            ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
            oCompanyWorkWeekInfoCollectionForFincncialyear = (List<CompanyWeekDayInfo>)oCompanyClient.SelectAllWorkWeekByFinancialYearIDAndCompanyID(SessionHelper.CurrentCompanyID.Value, financialYearID, Helper.GetAppUserInfo());
            ViewState["CompanyWorkWeekInfoCollectionForFincncialyear"] = oCompanyWorkWeekInfoCollectionForFincncialyear;
        }
        else
        {
            oCompanyWorkWeekInfoCollectionForFincncialyear = (IList<CompanyWeekDayInfo>)ViewState["CompanyWorkWeekInfoCollectionForFincncialyear"];
        }
        var oCompanyWeekDayInfocollection =
                        (from o in oCompanyWorkWeekInfoCollectionForFincncialyear
                         where o.RecPeriodID.Value == RecPeriodID
                         select o).ToList<CompanyWeekDayInfo>();
        return oCompanyWeekDayInfocollection;


    }


    # endregion

    #region EventHandler
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (this.Mode == QueryStringConstants.INSERT)
                {
                    TaskHdrInfo oTaskHdrInfo = this.GetTaskHdrInfo(false);
                    oTaskHdrInfo.AddedBy = SessionHelper.CurrentUserLoginID;
                    oTaskHdrInfo.DateAdded = System.DateTime.Now;
                    oTaskHdrInfo.IsDeleted = false;
                    List<TaskHdrInfo> oTaskHdrInfoList = new List<TaskHdrInfo>();
                    List<AttachmentInfo> oAttachmentInfoList = GetAttechmentInfoList();
                    if (oAttachmentInfoList != null && oAttachmentInfoList.Count > 0)
                    {
                        foreach (AttachmentInfo attachment in oAttachmentInfoList)
                        {
                            attachment.RecordID = 1;
                        }
                    }
                    oTaskHdrInfoList.Add(oTaskHdrInfo);

                    DataTable dtSequence = TaskMasterHelper.AddTask(oTaskHdrInfoList, SessionHelper.CurrentReconciliationPeriodID.GetValueOrDefault(), SessionHelper.CurrentUserLoginID, System.DateTime.Now, oAttachmentInfoList);
                    Session.Remove(SessionConstants.TASK_MASTER_ATTACHMENT);

                    //Update TaskNumber
                    if (dtSequence != null && dtSequence.Rows.Count > 0 && oTaskHdrInfoList.Count > 0)
                    {
                        for (int i = 0; i < oTaskHdrInfoList.Count; i++)
                        {
                            oTaskHdrInfoList[i].TaskNumber = dtSequence.Rows[i][1].ToString();
                        }

                    }
                    TaskMasterHelper.RaiseTaskAlert(oTaskHdrInfoList);

                }
                else if (this.Mode == QueryStringConstants.EDIT)
                {
                    UpdateTask(false);
                    Session.Remove(SessionConstants.TASK_MASTER_ATTACHMENT);

                }
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopupAndRefreshParentPage", ScriptHelper.GetJSForClosePopupAndSubmitParentPage(true));

            }

        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }
    void ucAccountSearchControl_SearchClickEventHandler(List<AccountHdrInfo> oAccountHdrInfoCollection)
    {
        ValidateTaskOwners();
        AccessibleAccountIDList = null;
        AccessibleAccountIDList = TaskMasterHelper.GetAccountIDListByUserIDs(this.GetSelectedUserID(), SessionHelper.CurrentReconciliationPeriodID);
        SearchAccounts(oAccountHdrInfoCollection);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string script = PopupHelper.GetScriptForClosingRadWindow();
        ClientScript.RegisterClientScriptBlock(this.GetType(), "CloseWindow", script);
    }
    protected void ddlTaskListName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            PopupHelper.HideErrorMessage(this);
            if (ddlTaskListName.SelectedValue == WebConstants.CREATE_NEW)
            {
                ShowHideTaskListName(true);
            }
            else
            {
                ShowHideTaskListName(false);
            }
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }
    protected void ddlTaskSubListName_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            PopupHelper.HideErrorMessage(this);
            if (ddlTaskSubListName.SelectedValue == WebConstants.CREATE_NEW)
            {
                ShowHideTaskSubListName(true);
            }
            else
            {
                ShowHideTaskSubListName(false);
            }
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }
    protected void ddlRecurrence_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ShowHidePnlRecurrenceType(Convert.ToInt16(ddlRecurrence.SelectedValue));
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }
    protected void btnRemoveAccount_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (this.Mode == QueryStringConstants.EDIT)
            {
                List<AccountHdrInfo> oRemoveTaskAccountHdrInfoCollection = GetSelectedTaskAccount();
                if (this.getTaskAccounts().Count > oRemoveTaskAccountHdrInfoCollection.Count)
                {
                    RemoveAccountFromTaskAccount(oRemoveTaskAccountHdrInfoCollection);
                    UpdateTask(true);
                    PopupHelper.HideErrorMessage(this);
                }
                else
                    PopupHelper.ShowErrorMessage(this, new ARTException(5000362));
                //throw new ARTException(5000362);

            }
            else if (this.Mode == QueryStringConstants.INSERT)
            {
                List<AccountHdrInfo> oRemoveTaskAccountHdrInfoCollection = GetSelectedTaskAccount();
                RemoveAccountFromTaskAccount(oRemoveTaskAccountHdrInfoCollection);
            }

        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }

    }
    protected void btnAdd_OnClick(object sender, EventArgs e)
    {
        try
        {
            AddAccount();
            if (this.Mode == QueryStringConstants.EDIT)
            {
                Page.Validate();
                if (Page.IsValid)

                    UpdateTask(false);
            }
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }
    protected void cblRecPeriodsCustom_DataBinding(object sender, EventArgs e)
    {
        for (int i = 0; i < cblRecPeriodsCustom.Items.Count; i++)
        {
            cblRecPeriodsCustom.Items[i].Text = Helper.GetDisplayDate(Convert.ToDateTime(cblRecPeriodsCustom.Items[i].Text));
        }
    }
    //protected void cblRecurrencePeriodNumber_DataBinding(object sender, EventArgs e)
    //{
    //    string strPertiodNumber = LanguageUtil.GetValue(2735);
    //    for (int i = 0; i < cblRecurrencePeriodNumber.Items.Count; i++)
    //    {
    //        cblRecurrencePeriodNumber.Items[i].Text = Helper.GetDisplayStringValue(strPertiodNumber + " : " + cblRecurrencePeriodNumber.Items[i].Text);
    //    }
    //}
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

    # endregion

    #region Grid Events
    void ucSkyStemARTGridAccountsAdded_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                AccountHdrInfo oAccountHdrInfo = (AccountHdrInfo)e.Item.DataItem;
                AccountViewerHelper.BindCommonFields(e, oAccountHdrInfo, "AddedGrid");
                Helper.SetTextForLabel(e.Item, "lblAccountNumberAddedGrid", oAccountHdrInfo.AccountNumber);
                CheckBox checkBox = (CheckBox)(e.Item as GridDataItem)["CheckboxSelectColumn"].Controls[0];
                if (checkBox != null && oAccountHdrInfo.IsTaskCompleted.HasValue)
                    checkBox.Visible = !oAccountHdrInfo.IsTaskCompleted.Value;
            }
        }
        catch (ARTException ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }
    void ucSkyStemARTGridAccountSearchResult_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                AccountHdrInfo oAccountHdrInfo = (AccountHdrInfo)e.Item.DataItem;

                AccountViewerHelper.BindCommonFields(e, oAccountHdrInfo);

                Helper.SetTextForLabel(e.Item, "lblAccountNumber", oAccountHdrInfo.AccountNumber);

            }
        }
        catch (ARTException ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }
    protected object ucSkyStemARTGridAccountsAdded_Grid_NeedDataSourceEventHandler(int count)
    {
        try
        {
            if (AccountHdrInfoListAdded == null || AccountHdrInfoListAdded.Count == 0)
            {
                //AccountHdrInfoListAdded = new List<AccountHdrInfo>();
                //AccountHdrInfoListAdded = NetAccountHelper.GetNetAccountAssociatedAccounts((int)SessionHelper.CurrentReconciliationPeriodID, Convert.ToInt32(ddlNetAccount.SelectedValue));
            }
        }
        catch (ARTException ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }

        return AccountHdrInfoListAdded;
    }
    protected object ucSkyStemARTGrid_Grid_NeedDataSourceEventHandler(int count)
    {
        try
        {
            int defaultItemCount = Helper.GetDefaultChunkSize(ucSkyStemARTGridAccountSearchResult.Grid.PageSize);
            int totalCount = 0;
            if (AccountHdrInfoListSearchResults != null)
                totalCount = AccountHdrInfoListSearchResults.Count;

            if (AccountHdrInfoListAdded != null)
                totalCount += AccountHdrInfoListAdded.Count;

            if (totalCount <= count - defaultItemCount)
            {
                AccountSearchCriteria oAccountSearchCriteria = (AccountSearchCriteria)HttpContext.Current.Session[SessionConstants.ACCOUNT_SEARCH_CRITERIA];

                if (oAccountSearchCriteria != null)
                {
                    if (ucSkyStemARTGridAccountSearchResult.Grid.MasterTableView.SortExpressions != null && ucSkyStemARTGridAccountSearchResult.Grid.MasterTableView.SortExpressions.Count > 0)
                    {
                        GridHelper.GetSortExpressionAndDirection(oAccountSearchCriteria, ucSkyStemARTGridAccountSearchResult.Grid.MasterTableView);
                    }

                    oAccountSearchCriteria.Count = count;
                    AccountHdrInfoListSearchResults = NetAccountHelper.SearchAccounts(oAccountSearchCriteria);

                    if (AccountHdrInfoListAdded != null && AccountHdrInfoListAdded.Count > 0 && AccountHdrInfoListSearchResults != null && AccountHdrInfoListSearchResults.Count > 0)
                    {
                        foreach (AccountHdrInfo oAccountHdrInfo in AccountHdrInfoListAdded)
                        {
                            for (int i = AccountHdrInfoListSearchResults.Count - 1; i < AccountHdrInfoListSearchResults.Count; i--)
                            {
                                if (AccountHdrInfoListSearchResults[i].AccountID == oAccountHdrInfo.AccountID)
                                    AccountHdrInfoListSearchResults.Remove(AccountHdrInfoListSearchResults[i]);
                            }
                        }
                    }
                }
            }
        }
        catch (ARTException ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }

        return AccountHdrInfoListSearchResults;
    }
    #endregion

    # region  Services WebMethod
    /// <summary>
    /// This method is used to auto populate FS Caption text box based on the basis of 
    /// the prefix text typed in the text box
    /// </summary>
    /// <param name="prefixText">The text which was typed in the text box</param>
    /// <param name="count">Number of results to be returned</param>
    /// <returns>List of FS Caption</returns>
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] AutoCompleteFSCaption(string prefixText, int count)
    {
        string[] oFSCaptionCollection = null;
        try
        {
            if (SessionHelper.CurrentCompanyID.HasValue)
            {
                int companyId = SessionHelper.CurrentCompanyID.Value;
                IFSCaption oFSCaptionClient = RemotingHelper.GetFSCaptioneObject();
                oFSCaptionCollection = oFSCaptionClient.SelectFSCaptionByCompanyIDAndPrefixText(companyId, prefixText, count
                    , SessionHelper.GetUserLanguage(), SessionHelper.GetBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID(), Helper.GetAppUserInfo());

                if (oFSCaptionCollection == null || oFSCaptionCollection.Length == 0)
                {
                    oFSCaptionCollection = new string[] { "No Records Found" };
                }
            }
        }
        catch (ARTException ex)
        {
            PopupHelper.ShowErrorMessage(null, ex);
            throw ex;
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(null, ex);
            throw ex;
        }

        return oFSCaptionCollection;
    }

    /// <summary>
    /// This method is used to auto populate User Name text box based on the basis of 
    /// the prefix text typed in the text box
    /// </summary>
    /// <param name="prefixText">The text which was typed in the text box</param>
    /// <param name="count">Number of results to be returned</param>
    /// <returns>List of User Names</returns>
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    public static List<string> AutoCompleteUserName(string prefixText, int count)
    {
        List<string> UserNames = new List<string>();

        try
        {
            if (SessionHelper.CurrentCompanyID.HasValue)
            {
                int companyId = SessionHelper.CurrentCompanyID.Value;
                IUser oUserClient = RemotingHelper.GetUserObject();
                List<UserHdrInfo> oUserHdrInfoList = oUserClient.SelectActiveUserHdrInfoByCompanyIdAndPrefixText(companyId, prefixText, count, Helper.GetAppUserInfo());
                for (int i = 0; i < oUserHdrInfoList.Count; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(oUserHdrInfoList[i].Name.ToString(), oUserHdrInfoList[i].UserID.ToString());
                    UserNames.Add(item);
                }
                if (oUserHdrInfoList == null || oUserHdrInfoList.Count == 0)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("No Records Found", "0");
                    UserNames.Add(item);
                }
            }
        }
        catch (ARTException ex)
        {
            PopupHelper.ShowErrorMessage(null, ex);
            throw ex;
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(null, ex);
            throw ex;
        }

        return UserNames;
    }
    #endregion

    # region Public Methods
    public string ResolveClientUrlPath(string relativeUrl)
    {
        string url;
        url = Page.ResolveClientUrl(relativeUrl);
        return url;
    }
    # endregion

    # region Validations

    protected void cvcblRecPeriods_ServerValidate(object source, ServerValidateEventArgs args)
    {
        List<ReconciliationPeriodInfo> oReconciliationPeriodInfoList = GetRecurrenceFrequency();
        if (oReconciliationPeriodInfoList.Count <= 0)
        {
            args.IsValid = false;
            return;
        }
    }
    protected void cvcblRecPeriodNumbers_ServerValidate(object source, ServerValidateEventArgs args)
    {
        List<ReconciliationPeriodInfo> oReconciliationPeriodInfoList = GetQuarterlyRecurrencePeriodNumberList();
        if (oReconciliationPeriodInfoList.Count != 4)
        {
            args.IsValid = false;
            return;
        }
    }
    protected void cvcblMRRecPeriodNumbers_ServerValidate(object source, ServerValidateEventArgs args)
    {
        List<ReconciliationPeriodInfo> oReconciliationPeriodInfoList = GetMRRecurrencePeriodNumberList();
        if (oReconciliationPeriodInfoList.Count <= 0)
        {
            args.IsValid = false;
            return;
        }
    }
    protected void cvIsOnHoliday_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            ExCustomValidator cv = (ExCustomValidator)source;
            Control oControl = cv.NamingContainer;
            ExCalendar cal = (ExCalendar)oControl.FindControl(cv.ControlToValidate);
            int rowRecPeriodID = SessionHelper.CurrentReconciliationPeriodID.GetValueOrDefault();
            if (IsDateOnHoliday(cal.Text, rowRecPeriodID))
            {
                PopupMasterPageBase oMasterPageBase = (PopupMasterPageBase)this.Page.Master;
                oMasterPageBase.ShowErrorMessage(cv.LabelID);
                args.IsValid = false;
            }
        }
        catch (ARTException ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }
    protected void cvTaskUser_OnServerValidate(object sender, ServerValidateEventArgs args)
    {
        string[] TempArray;
        int TempArrayLength = 0;
        int tAL;
        int MaxArrayLength = 0;
        string[] AssignedToArray = null;
        string[] ReviewerArray = null;
        string[] ApproverArray = null;
        string[] NotifyArray = null;

        if (!String.IsNullOrEmpty(hdnAssignedTo.Value) || !String.IsNullOrEmpty(txtAssignedTo.Text))
        {
            AssignedToArray = hdnAssignedTo.Value.Split(',');
            tAL = AssignedToArray.Length;
            if (MaxArrayLength < tAL)
                MaxArrayLength = tAL;
            TempArrayLength += tAL;
        }
        if (!String.IsNullOrEmpty(hdnReviewer.Value) || !String.IsNullOrEmpty(txtReviewer.Text))
        {
            ReviewerArray = hdnReviewer.Value.Split(',');
            tAL = ReviewerArray.Length;
            if (MaxArrayLength < tAL)
                MaxArrayLength = tAL;
            TempArrayLength += tAL;
        }
        if (!String.IsNullOrEmpty(hdnApprover.Value) || !String.IsNullOrEmpty(txtApprover.Text))
        {
            ApproverArray = hdnApprover.Value.Split(',');
            tAL = ApproverArray.Length;
            if (MaxArrayLength < tAL)
                MaxArrayLength = tAL;
            TempArrayLength += tAL;
        }
        if (!String.IsNullOrEmpty(hdnNotify.Value) || !String.IsNullOrEmpty(txtNotify.Text))
        {
            NotifyArray = hdnNotify.Value.Split(',');
            tAL = NotifyArray.Length;
            if (MaxArrayLength < tAL)
                MaxArrayLength = tAL;
            TempArrayLength += tAL;
        }
        if (MaxArrayLength > 0 && TempArrayLength > 0)
        {
            TempArray = new string[TempArrayLength];
            int j = 0;
            for (int i = 0; i < MaxArrayLength; i++)
            {
                if (AssignedToArray != null && AssignedToArray.Length > i)
                {
                    if (TempArray.Contains(AssignedToArray[i]))
                    {
                        args.IsValid = false;
                        break;
                    }
                    TempArray[j++] = AssignedToArray[i];
                }
                if (ReviewerArray != null && ReviewerArray.Length > i)
                {
                    if (TempArray.Contains(ReviewerArray[i]))
                    {
                        args.IsValid = false;
                        break;
                    }
                    TempArray[j++] = ReviewerArray[i];
                }
                if (ApproverArray != null && ApproverArray.Length > i)
                {
                    if (TempArray.Contains(ApproverArray[i]))
                    {
                        args.IsValid = false;
                        break;
                    }
                    TempArray[j++] = ApproverArray[i];
                }
                if (NotifyArray != null && NotifyArray.Length > i)
                {
                    if (TempArray.Contains(NotifyArray[i]))
                    {
                        args.IsValid = false;
                        break;
                    }
                    TempArray[j++] = NotifyArray[i];
                }

            }
        }
    }
    protected void cvValidateTaskDates_OnServerValidate(object sender, ServerValidateEventArgs args)
    {

        List<DateTime> TaskDateList = new List<DateTime>();
        if (!String.IsNullOrEmpty(calAssigneeDueDate.Text))
        {
            DateTime dtAssigneeDueDate = new DateTime();
            if (DateTime.TryParse(calAssigneeDueDate.Text, out dtAssigneeDueDate))
                TaskDateList.Add(Convert.ToDateTime(dtAssigneeDueDate));
        }
        if (!String.IsNullOrEmpty(calReviewerDueDate.Text))
        {
            DateTime dtReviewerDueDate = new DateTime();
            if (DateTime.TryParse(calReviewerDueDate.Text, out dtReviewerDueDate))
                TaskDateList.Add(Convert.ToDateTime(dtReviewerDueDate));
        }
        if (!String.IsNullOrEmpty(calTaskDueDate.Text))
        {
            DateTime dtTaskDueDate = new DateTime();
            if (DateTime.TryParse(calTaskDueDate.Text, out dtTaskDueDate))
                TaskDateList.Add(Convert.ToDateTime(dtTaskDueDate));
        }
        if (!Helper.DatesInOrder(TaskDateList))
        {
            args.IsValid = false;
        }
    }
    protected void cvAssigneeDueDate_OnServerValidate(object sender, ServerValidateEventArgs args)
    {
        try
        {
            DateTime Dt = Convert.ToDateTime(calAssigneeDueDate.Text);
        }
        catch
        {
            args.IsValid = false;
        }
    }
    protected void cvTaskDueDate_OnServerValidate(object sender, ServerValidateEventArgs args)
    {
        try
        {
            DateTime Dt = Convert.ToDateTime(calTaskDueDate.Text);
        }
        catch
        {
            args.IsValid = false;
        }
    }
    protected void cvReviewerDueDate_OnServerValidate(object sender, ServerValidateEventArgs args)
    {
        try
        {
            DateTime Dt = Convert.ToDateTime(calReviewerDueDate.Text);
        }
        catch
        {
            args.IsValid = false;
        }
    }
    protected void cvTaskDueDays_OnServerValidate(object sender, ServerValidateEventArgs args)
    {
        int TaskDueDays;
        string errorMessage1 = LanguageUtil.GetValue(5000351);
        bool isError = false;
        StringBuilder oSBError = new StringBuilder();
        //5000351 not a valid number 
        if (!String.IsNullOrEmpty(txtCustomTaskDueDays.Text))
        {
            if (int.TryParse(txtCustomTaskDueDays.Text, out TaskDueDays))
            {
                if (TaskDueDays > WebConstants.TASK_DUE_DAYS)
                {

                    string Err2 = LanguageUtil.GetValue(5000380);
                    isError = true;
                    oSBError.Append(string.Format(Err2, LanguageUtil.GetValue(2582), WebConstants.TASK_DUE_DAYS.ToString()));
                    oSBError.Append("<br/>");
                }
            }
            else
            {
                isError = true;
                oSBError.Append(string.Format(errorMessage1, LanguageUtil.GetValue(2582)));
                oSBError.Append("<br/>");
            }
        }
        if (isError)
        {
            args.IsValid = false;
            cvTaskDueDays.ErrorMessage = oSBError.ToString();
        }
    }
    protected void cvTaskAccounts_OnServerValidate(object sender, ServerValidateEventArgs args)
    {
        List<AccountHdrInfo> oTaskAccountlist = getTaskAccounts();
        List<AccountHdrInfo> oValidTaskAccountlist = null;

        AccessibleAccountIDList = TaskMasterHelper.GetAccountIDListByUserIDs(this.GetSelectedUserID(), SessionHelper.CurrentReconciliationPeriodID);
        if (oTaskAccountlist != null && oTaskAccountlist.Count > 0)
        {
            if (!string.IsNullOrEmpty(hdnAssignedTo.Value) || !string.IsNullOrEmpty(hdnReviewer.Value) || !string.IsNullOrEmpty(hdnApprover.Value) || !string.IsNullOrEmpty(hdnNotify.Value))
            {
                AccessibleAccountIDList = TaskMasterHelper.GetAccountIDListByUserIDs(this.GetSelectedUserID(), SessionHelper.CurrentReconciliationPeriodID);
            }
            if (AccessibleAccountIDList != null)
            {
                oValidTaskAccountlist = (from oAccountHdrInfo in oTaskAccountlist
                                         join oAccountID in AccessibleAccountIDList on oAccountHdrInfo.AccountID equals oAccountID
                                         select oAccountHdrInfo).ToList();
            }
            if (oValidTaskAccountlist != null && oValidTaskAccountlist.Count != oTaskAccountlist.Count)
            {
                args.IsValid = false;
                string errorMessage = LanguageUtil.GetValue(5000353);
                cvTaskAccounts.ErrorMessage = string.Format(errorMessage, LanguageUtil.GetValue(1204));
            }
        }
        else
        {
            args.IsValid = false;
            string errorMessage = LanguageUtil.GetValue(5000352);
            cvTaskAccounts.ErrorMessage = string.Format(errorMessage, LanguageUtil.GetValue(2581));
        }
        if (oTaskAccountlist.Count < 1)
        {
            args.IsValid = false;
            string errorMessage = LanguageUtil.GetValue(5000352);
            cvTaskAccounts.ErrorMessage = string.Format(errorMessage, LanguageUtil.GetValue(2581));
        }

    }
    protected void cvTaskAccountsAssignedTo_OnServerValidate(object sender, ServerValidateEventArgs args)
    {
        List<AccountHdrInfo> oTaskAccountlist = getTaskAccounts();
        List<AccountHdrInfo> oValidTaskAccountlist = null;
        List<int> UserIDList = new List<int>();
        if (!string.IsNullOrEmpty(hdnAssignedTo.Value) && oTaskAccountlist != null && oTaskAccountlist.Count > 0)
        {
            string[] AssignedTo = hdnAssignedTo.Value.Split(',');
            for (int i = 0; i < AssignedTo.Length; i++)
            {
                int UserID = Convert.ToInt32(AssignedTo[i]);
                UserIDList.Add(UserID);
            }
            AccessibleAccountIDList = TaskMasterHelper.GetAccountIDListByUserIDs(UserIDList, SessionHelper.CurrentReconciliationPeriodID);
            if (AccessibleAccountIDList != null)
            {
                oValidTaskAccountlist = (from oAccountHdrInfo in oTaskAccountlist
                                         join oAccountID in AccessibleAccountIDList on oAccountHdrInfo.AccountID equals oAccountID
                                         select oAccountHdrInfo).ToList();
            }
            if (oValidTaskAccountlist != null && oValidTaskAccountlist.Count != oTaskAccountlist.Count)
            {
                args.IsValid = false;
            }
        }
    }
    protected void cvTaskAccountsReviewer_OnServerValidate(object sender, ServerValidateEventArgs args)
    {
        List<AccountHdrInfo> oTaskAccountlist = getTaskAccounts();
        List<AccountHdrInfo> oValidTaskAccountlist = null;
        List<int> UserIDList = new List<int>();
        if (!string.IsNullOrEmpty(hdnReviewer.Value) && oTaskAccountlist != null && oTaskAccountlist.Count > 0)
        {
            string[] Reviewer = hdnReviewer.Value.Split(',');
            for (int i = 0; i < Reviewer.Length; i++)
            {
                int UserID = Convert.ToInt32(Reviewer[i]);
                UserIDList.Add(UserID);
            }
            AccessibleAccountIDList = TaskMasterHelper.GetAccountIDListByUserIDs(UserIDList, SessionHelper.CurrentReconciliationPeriodID);
            if (AccessibleAccountIDList != null)
            {
                oValidTaskAccountlist = (from oAccountHdrInfo in oTaskAccountlist
                                         join oAccountID in AccessibleAccountIDList on oAccountHdrInfo.AccountID equals oAccountID
                                         select oAccountHdrInfo).ToList();
            }
            if (oValidTaskAccountlist != null && oValidTaskAccountlist.Count != oTaskAccountlist.Count)
            {
                args.IsValid = false;
            }
        }
    }
    protected void cvTaskAccountsApprover_OnServerValidate(object sender, ServerValidateEventArgs args)
    {


        List<AccountHdrInfo> oTaskAccountlist = getTaskAccounts();
        List<AccountHdrInfo> oValidTaskAccountlist = null;
        List<int> UserIDList = new List<int>();
        if (!string.IsNullOrEmpty(hdnApprover.Value) && oTaskAccountlist != null && oTaskAccountlist.Count > 0)
        {
            string[] Approver = hdnApprover.Value.Split(',');
            for (int i = 0; i < Approver.Length; i++)
            {
                int UserID = Convert.ToInt32(Approver[i]);
                UserIDList.Add(UserID);
            }
            AccessibleAccountIDList = TaskMasterHelper.GetAccountIDListByUserIDs(UserIDList, SessionHelper.CurrentReconciliationPeriodID);
            if (AccessibleAccountIDList != null)
            {
                oValidTaskAccountlist = (from oAccountHdrInfo in oTaskAccountlist
                                         join oAccountID in AccessibleAccountIDList on oAccountHdrInfo.AccountID equals oAccountID
                                         select oAccountHdrInfo).ToList();
            }
            if (oValidTaskAccountlist != null && oValidTaskAccountlist.Count != oTaskAccountlist.Count)
            {
                args.IsValid = false;
            }
        }
    }
    protected void cvTaskAccountsNotify_OnServerValidate(object sender, ServerValidateEventArgs args)
    {
        List<AccountHdrInfo> oTaskAccountlist = getTaskAccounts();
        List<AccountHdrInfo> oValidTaskAccountlist = null;
        List<int> UserIDList = new List<int>();
        if (!string.IsNullOrEmpty(this.hdnNotify.Value) && oTaskAccountlist != null && oTaskAccountlist.Count > 0)
        {
            string[] Notiy = hdnNotify.Value.Split(',');
            for (int i = 0; i < Notiy.Length; i++)
            {
                int UserID = Convert.ToInt32(Notiy[i]);
                UserIDList.Add(UserID);
            }
            AccessibleAccountIDList = TaskMasterHelper.GetAccountIDListByUserIDs(UserIDList, SessionHelper.CurrentReconciliationPeriodID);
            if (AccessibleAccountIDList != null)
            {
                oValidTaskAccountlist = (from oAccountHdrInfo in oTaskAccountlist
                                         join oAccountID in AccessibleAccountIDList on oAccountHdrInfo.AccountID equals oAccountID
                                         select oAccountHdrInfo).ToList();
            }
            if (oValidTaskAccountlist != null && oValidTaskAccountlist.Count != oTaskAccountlist.Count)
            {
                args.IsValid = false;
            }
        }
    }
    protected void cvAssignedTo_OnServerValidate(object sender, ServerValidateEventArgs args)
    {
        if (string.IsNullOrEmpty(txtAssignedTo.Text))
        {
            args.IsValid = false;
            return;
        }
    }
    protected void cvApprover_OnServerValidate(object sender, ServerValidateEventArgs args)
    {
        if ((string.IsNullOrEmpty(txtReviewer.Text) || string.IsNullOrEmpty(hdnReviewer.Value)) && ((!string.IsNullOrEmpty(txtApprover.Text) || !string.IsNullOrEmpty(hdnApprover.Value))))
        {
            args.IsValid = false;
            return;
        }
    }
    protected void cvAssigneeDD_OnServerValidate(object sender, ServerValidateEventArgs args)
    {
        if (string.IsNullOrEmpty(txtReviewer.Text) && !string.IsNullOrEmpty(calAssigneeDueDate.Text))
        {
            args.IsValid = false;
            return;
        }
        else if (!string.IsNullOrEmpty(txtReviewer.Text) && string.IsNullOrEmpty(calAssigneeDueDate.Text))
        {
            args.IsValid = false;
            return;
        }
    }
    protected void cvReviewerDD_OnServerValidate(object sender, ServerValidateEventArgs args)
    {
        if (string.IsNullOrEmpty(txtApprover.Text) && !string.IsNullOrEmpty(calReviewerDueDate.Text))
        {
            args.IsValid = false;
            return;
        }
        else if (!string.IsNullOrEmpty(txtApprover.Text) && string.IsNullOrEmpty(calReviewerDueDate.Text))
        {
            args.IsValid = false;
            return;
        }
    }
    protected void cvCompareAssigneeDueDateWithCurrentDate_OnServerValidate(object sender, ServerValidateEventArgs args)
    {
        if (!string.IsNullOrEmpty(txtReviewer.Text) && !string.IsNullOrEmpty(calAssigneeDueDate.Text))
        {
            DateTime dtAssigneeDD = new DateTime();
            DateTime dtCompareWith = new DateTime();
            DateTime.TryParse(hdnDateToCompare.Value, out dtCompareWith);
            if (DateTime.TryParse(calAssigneeDueDate.Text, out dtAssigneeDD))
            {
                if (DateTime.Compare(dtAssigneeDD.Date, dtCompareWith) < 0)
                    args.IsValid = false;
            }
        }
    }
    protected void cvComparecalTaskDueDateWithCurrentDate_OnServerValidate(object sender, ServerValidateEventArgs args)
    {
        if (!string.IsNullOrEmpty(calTaskDueDate.Text))
        {
            DateTime dtTaskDueDate = new DateTime();
            DateTime dtCompareWith = new DateTime();
            DateTime.TryParse(hdnDateToCompare.Value, out dtCompareWith);
            if (DateTime.TryParse(calTaskDueDate.Text, out dtTaskDueDate))
            {
                if (DateTime.Compare(dtTaskDueDate.Date, dtCompareWith) < 0)
                    args.IsValid = false;
            }
        }
    }
    protected void cvComparecalReviewerDueDateWithCurrentDate_OnServerValidate(object sender, ServerValidateEventArgs args)
    {
        if (!string.IsNullOrEmpty(txtApprover.Text) && !string.IsNullOrEmpty(calReviewerDueDate.Text))
        {
            DateTime dtReviewerDD = new DateTime();
            DateTime dtCompareWith = new DateTime();
            DateTime.TryParse(hdnDateToCompare.Value, out dtCompareWith);
            if (DateTime.TryParse(calReviewerDueDate.Text, out dtReviewerDD))
            {
                if (DateTime.Compare(dtReviewerDD.Date, dtCompareWith) < 0)
                    args.IsValid = false;
            }
        }
    }
    protected void cvAssigneeDueDays_OnServerValidate(object sender, ServerValidateEventArgs args)
    {
        int AssigneeDueDays;
        string errorMessage1 = LanguageUtil.GetValue(5000351);
        bool isError = false;
        StringBuilder oSBError = new StringBuilder();
        string errorMessage3 = LanguageUtil.GetValue(5000356);

        //5000351 not a valid number      
        if (!String.IsNullOrEmpty(txtCustomAssigneeDueDays.Text))
        {
            if (int.TryParse(txtCustomAssigneeDueDays.Text, out AssigneeDueDays))
            {
                if (AssigneeDueDays > WebConstants.TASK_DUE_DAYS)
                {

                    string Err2 = LanguageUtil.GetValue(5000380);
                    isError = true;
                    oSBError.Append(string.Format(Err2, LanguageUtil.GetValue(2570), WebConstants.TASK_DUE_DAYS.ToString()));
                    oSBError.Append("<br/>");
                }
            }
            else
            {
                isError = true;
                oSBError.Append(string.Format(errorMessage1, LanguageUtil.GetValue(2570)));
                oSBError.Append("<br/>");
            }
        }
        if (isError)
        {
            args.IsValid = false;
            cvAssigneeDueDays.ErrorMessage = oSBError.ToString();
        }
        if (string.IsNullOrEmpty(txtReviewer.Text) && !string.IsNullOrEmpty(txtCustomAssigneeDueDays.Text))
        {
            args.IsValid = false;
            cvAssigneeDueDays.ErrorMessage = string.Format(errorMessage3, LanguageUtil.GetValue(2570), LanguageUtil.GetValue(1131));
            return;
        }
        else if (!string.IsNullOrEmpty(txtReviewer.Text) && string.IsNullOrEmpty(txtCustomAssigneeDueDays.Text))
        {
            args.IsValid = false;
            cvAssigneeDueDays.ErrorMessage = string.Format(errorMessage3, LanguageUtil.GetValue(2570), LanguageUtil.GetValue(1131));
            return;
        }
    }
    protected void cvReviewerDueDays_OnServerValidate(object sender, ServerValidateEventArgs args)
    {
        int ReviewerDueDays;
        string errorMessage1 = LanguageUtil.GetValue(5000351);
        bool isError = false;
        StringBuilder oSBError = new StringBuilder();
        string errorMessage3 = LanguageUtil.GetValue(5000356);

        if (!String.IsNullOrEmpty(txtCustomReviewerDueDays.Text))
        {
            if (int.TryParse(txtCustomReviewerDueDays.Text, out ReviewerDueDays))
            {
                if (ReviewerDueDays > WebConstants.TASK_DUE_DAYS)
                {

                    string Err2 = LanguageUtil.GetValue(5000380);
                    isError = true;
                    oSBError.Append(string.Format(Err2, LanguageUtil.GetValue(2753), WebConstants.TASK_DUE_DAYS.ToString()));
                    oSBError.Append("<br/>");
                }
            }
            else
            {
                isError = true;
                oSBError.Append(string.Format(errorMessage1, LanguageUtil.GetValue(2753)));
                oSBError.Append("<br/>");
            }
        }
        if (isError)
        {
            args.IsValid = false;
            cvReviewerDueDays.ErrorMessage = oSBError.ToString();
        }

        if (string.IsNullOrEmpty(txtApprover.Text) && !string.IsNullOrEmpty(txtCustomReviewerDueDays.Text))
        {
            args.IsValid = false;
            cvReviewerDueDays.ErrorMessage = string.Format(errorMessage3, LanguageUtil.GetValue(2753), LanguageUtil.GetValue(1132));
            return;
        }
        else if (!string.IsNullOrEmpty(txtApprover.Text) && string.IsNullOrEmpty(txtCustomReviewerDueDays.Text))
        {
            args.IsValid = false;
            cvReviewerDueDays.ErrorMessage = string.Format(errorMessage3, LanguageUtil.GetValue(2753), LanguageUtil.GetValue(1132));
            return;
        }
    }
    protected void cvTaskListName_OnServerValidate(object sender, ServerValidateEventArgs args)
    {
        if (!string.IsNullOrEmpty(txtNewTaskListName.Text))
        {
            int TaskListID = TaskMasterHelper.GetTaskListIDByName(txtNewTaskListName.Text.Trim());
            if (TaskListID > 0)
            {
                args.IsValid = false;
                return;
            }
        }
    }
    protected void cvSkipTaskDueDays_OnServerValidate(object sender, ServerValidateEventArgs args)
    {
        int SkipTaskDueDays;
        string errorMessage1 = LanguageUtil.GetValue(5000351);
        bool isError = false;
        StringBuilder oSBError = new StringBuilder();
        //5000351 not a valid number
        List<int> TaskDueDayList = new List<int>();
        if (!String.IsNullOrEmpty(txtSkipTaskDueDays.Text))
        {
            if (!int.TryParse(txtSkipTaskDueDays.Text, out SkipTaskDueDays))
            {
                isError = true;
                oSBError.Append(string.Format(errorMessage1, LanguageUtil.GetValue(2781)));
                oSBError.Append("<br/>");
            }
        }
        if (isError)
        {
            args.IsValid = false;
            cvSkipTaskDueDays.ErrorMessage = oSBError.ToString();
        }
    }
    protected void cvSkipAssigneeDueDays_OnServerValidate(object sender, ServerValidateEventArgs args)
    {

        int SkipAssigneeDueDays;
        string errorMessage1 = LanguageUtil.GetValue(5000351);
        bool isError = false;
        StringBuilder oSBError = new StringBuilder();
        //5000351 not a valid number
        if (!String.IsNullOrEmpty(txtSkipAssigneeDueDays.Text))
        {
            if (!int.TryParse(txtSkipAssigneeDueDays.Text, out SkipAssigneeDueDays))
            {
                isError = true;
                oSBError.Append(string.Format(errorMessage1, LanguageUtil.GetValue(2782)));
                oSBError.Append("<br/>");
            }
        }
        if (isError)
        {
            args.IsValid = false;
            cvSkipAssigneeDueDays.ErrorMessage = oSBError.ToString();
        }
    }
    protected void cvAssigneeDueDaysBasis_OnServerValidate(object sender, ServerValidateEventArgs args)
    {
        if (string.IsNullOrEmpty(txtReviewer.Text) && ddlAssigneeDueDaysBasis.SelectedValue != WebConstants.SELECT_ONE)
        {
            args.IsValid = false;
            return;
        }
        else if (!string.IsNullOrEmpty(txtReviewer.Text) && ddlAssigneeDueDaysBasis.SelectedValue == WebConstants.SELECT_ONE)
        {
            args.IsValid = false;
            return;
        }
    }
    protected void cvReviewerDueDaysBasis_OnServerValidate(object sender, ServerValidateEventArgs args)
    {
        if (string.IsNullOrEmpty(txtApprover.Text) && ddlReviewerDueDaysBasis.SelectedValue != WebConstants.SELECT_ONE)
        {
            args.IsValid = false;
            return;
        }
        else if (!string.IsNullOrEmpty(txtApprover.Text) && ddlReviewerDueDaysBasis.SelectedValue == WebConstants.SELECT_ONE)
        {
            args.IsValid = false;
            return;
        }
    }
    protected void cvTaskSkipDueDaysOrder_OnServerValidate(object sender, ServerValidateEventArgs args)
    {
        int SkipTaskDueDays;
        int SkipReviewerDueDays;
        List<int> SkipAssigneeReviewerDueDayList = new List<int>();
        List<int> SkipAssigneeTaskDueDayList = new List<int>();
        List<int> SkipReviewerTaskDueDayList = new List<int>();
        int SkipAssigneeDueDays;
        if (!String.IsNullOrEmpty(txtSkipAssigneeDueDays.Text))
        {
            if (int.TryParse(txtSkipAssigneeDueDays.Text, out SkipAssigneeDueDays))
            {
                SkipAssigneeReviewerDueDayList.Add(SkipAssigneeDueDays);
                SkipAssigneeTaskDueDayList.Add(SkipAssigneeDueDays);
            }
        }
        if (!String.IsNullOrEmpty(txtSkipReviewerDueDays.Text))
        {
            if (int.TryParse(txtSkipReviewerDueDays.Text, out SkipReviewerDueDays))
            {
                SkipAssigneeReviewerDueDayList.Add(SkipReviewerDueDays);
                SkipReviewerTaskDueDayList.Add(SkipReviewerDueDays);
            }

        }
        if (!String.IsNullOrEmpty(txtSkipTaskDueDays.Text))
        {
            if (int.TryParse(txtSkipTaskDueDays.Text, out SkipTaskDueDays))
            {
                SkipAssigneeTaskDueDayList.Add(SkipTaskDueDays);
                SkipReviewerTaskDueDayList.Add(SkipTaskDueDays);
            }
        }
        if (!this.DaysInOrder(SkipAssigneeReviewerDueDayList) && ddlAssigneeDueDaysBasis.SelectedValue == ddlReviewerDueDaysBasis.SelectedValue)
        {
            args.IsValid = false;
            string errorMessage = LanguageUtil.GetValue(5000342);
            cvTaskSkipDueDaysOrder.ErrorMessage = string.Format(errorMessage, LanguageUtil.GetValue(2782));
        }
        if (!this.DaysInOrder(SkipReviewerTaskDueDayList) && ddlTaskDueDaysBasis.SelectedValue == ddlReviewerDueDaysBasis.SelectedValue)
        {
            args.IsValid = false;
            string errorMessage = LanguageUtil.GetValue(5000342);
            cvTaskSkipDueDaysOrder.ErrorMessage = string.Format(errorMessage, LanguageUtil.GetValue(2782));
        }
        if (!this.DaysInOrder(SkipAssigneeTaskDueDayList) && ddlTaskDueDaysBasis.SelectedValue == ddlAssigneeDueDaysBasis.SelectedValue)
        {
            args.IsValid = false;
            string errorMessage = LanguageUtil.GetValue(5000342);
            cvTaskSkipDueDaysOrder.ErrorMessage = string.Format(errorMessage, LanguageUtil.GetValue(2782));
        }
    }
    protected void cvTaskDueDaysOrder_OnServerValidate(object sender, ServerValidateEventArgs args)
    {
        int TaskDueDays;
        int ReviewerDueDays;
        List<int> AssigneeReviewerDueDayList = new List<int>();
        List<int> AssigneeTaskDueDayList = new List<int>();
        List<int> ReviewerTaskDueDayList = new List<int>();
        int AssigneeDueDays;
        if (!String.IsNullOrEmpty(txtCustomAssigneeDueDays.Text))
        {
            if (int.TryParse(txtCustomAssigneeDueDays.Text, out AssigneeDueDays))
            {
                AssigneeReviewerDueDayList.Add(AssigneeDueDays);
                AssigneeTaskDueDayList.Add(AssigneeDueDays);
            }
        }
        if (!String.IsNullOrEmpty(txtCustomReviewerDueDays.Text))
        {
            if (int.TryParse(txtCustomReviewerDueDays.Text, out ReviewerDueDays))
            {
                AssigneeReviewerDueDayList.Add(ReviewerDueDays);
                ReviewerTaskDueDayList.Add(ReviewerDueDays);
            }

        }
        if (!String.IsNullOrEmpty(txtCustomTaskDueDays.Text))
        {
            if (int.TryParse(txtCustomTaskDueDays.Text, out TaskDueDays))
            {
                AssigneeTaskDueDayList.Add(TaskDueDays);
                ReviewerTaskDueDayList.Add(TaskDueDays);
            }
        }
        if (!this.DaysInOrder(AssigneeReviewerDueDayList) && ddlAssigneeDueDaysBasis.SelectedValue == ddlReviewerDueDaysBasis.SelectedValue)
        {
            args.IsValid = false;
            string errorMessage = LanguageUtil.GetValue(5000342);
            cvTaskDueDaysOrder.ErrorMessage = string.Format(errorMessage, LanguageUtil.GetValue(2760));
        }
        if (!this.DaysInOrder(ReviewerTaskDueDayList) && ddlTaskDueDaysBasis.SelectedValue == ddlReviewerDueDaysBasis.SelectedValue)
        {
            args.IsValid = false;
            string errorMessage = LanguageUtil.GetValue(5000342);
            cvTaskDueDaysOrder.ErrorMessage = string.Format(errorMessage, LanguageUtil.GetValue(2760));
        }
        if (!this.DaysInOrder(AssigneeTaskDueDayList) && ddlTaskDueDaysBasis.SelectedValue == ddlAssigneeDueDaysBasis.SelectedValue)
        {
            args.IsValid = false;
            string errorMessage = LanguageUtil.GetValue(5000342);
            cvTaskDueDaysOrder.ErrorMessage = string.Format(errorMessage, LanguageUtil.GetValue(2760));
        }
    }


    # endregion
}
