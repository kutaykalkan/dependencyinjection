using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Utility;
using SkyStem.Language.LanguageUtility;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.Library.Controls.WebControls;
using Telerik.Web.UI;

namespace SkyStem.ART.Web.Utility
{
    /// <summary>
    /// Summary description for TaskMasterHelper
    /// </summary>
    public class TaskMasterHelper
    {
        public TaskMasterHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region "Service Calls"
        public static List<TaskHdrInfo> GetAccessableTaskByUserID(int userID, short roleID, int recPeriodID, short taskCategoryID, ARTEnums.TaskType taskType
            , List<TaskStatusMstInfo> taskStatusMstInfoList, List<FilterCriteria> filterCriteriaList, bool? isShowHidden)
        {
            ITaskMaster oTaskMasterClient = RemotingHelper.GetTaskMasterObject();
            List<TaskHdrInfo> oTaskHdrInfoCollection = oTaskMasterClient.GetAccessableTaskByUserID(userID, roleID, recPeriodID, taskCategoryID, 
                taskType, taskStatusMstInfoList, filterCriteriaList, isShowHidden, SessionHelper.GetUserLanguage(), AppSettingHelper.GetDefaultLanguageID(), Helper.GetAppUserInfo());
            LanguageHelper.TranslateLabelsTaskHdrInfo(oTaskHdrInfoCollection);
            return oTaskHdrInfoCollection;

        }

        public static List<TaskHdrInfo> GetAccessibleTasksByActionTypeID(int userID, short roleID, int recPeriodID, ARTEnums.TaskType taskType,
            ARTEnums.TaskActionType taskActionType, List<long> AccountIDs, short? taskCategoryID, bool? isShowHidden)
        {
            ITaskMaster oTaskMasterClient = RemotingHelper.GetTaskMasterObject();
            List<TaskHdrInfo> oTaskHdrInfoCollection = oTaskMasterClient.GetAccessibleTasksByActionTypeID(userID, roleID, recPeriodID, taskType, taskActionType,
                AccountIDs, taskCategoryID, isShowHidden, SessionHelper.GetUserLanguage(), AppSettingHelper.GetDefaultLanguageID(), Helper.GetAppUserInfo());
            LanguageHelper.TranslateLabelsTaskHdrInfo(oTaskHdrInfoCollection);
            return oTaskHdrInfoCollection;
        }
        //public static List<TaskHdrInfo> GetApproverAccessableTaskByUserID(int userID, short roleID, int recPeriodID, ARTEnums.TaskType taskType,
        //    List<long> AccountIDs, short? taskCategoryID, bool? isShowHidden)
        //{
        //    ITaskMaster oTaskMasterClient = RemotingHelper.GetTaskMasterObject();
        //    List<TaskHdrInfo> oTaskHdrInfoCollection = oTaskMasterClient.GetApproverAccessableTaskByUserID(userID, roleID, recPeriodID, taskType,
        //        AccountIDs, taskCategoryID, isShowHidden, SessionHelper.GetUserLanguage(), AppSettingHelper.GetDefaultLanguageID(), Helper.GetAppUserInfo());
        //    LanguageHelper.TranslateLabelsTaskHdrInfo(oTaskHdrInfoCollection);
        //    return oTaskHdrInfoCollection;

        //}

        //public static List<TaskHdrInfo> GetAssingneeAccessableTaskByUserID(int userID, short roleID, int recPeriodID, ARTEnums.TaskType taskType,
        //    List<long> AccountIDs, short? taskCategoryID, bool? isShowHidden)
        //{
        //    ITaskMaster oTaskMasterClient = RemotingHelper.GetTaskMasterObject();
        //    List<TaskHdrInfo> oTaskHdrInfoCollection = oTaskMasterClient.GetAssingneeAccessableTaskByUserID(userID, roleID, recPeriodID, taskType,
        //        AccountIDs, taskCategoryID, isShowHidden, SessionHelper.GetUserLanguage(), AppSettingHelper.GetDefaultLanguageID(), Helper.GetAppUserInfo());
        //    LanguageHelper.TranslateLabelsTaskHdrInfo(oTaskHdrInfoCollection);
        //    return oTaskHdrInfoCollection;

        //}

        public static List<TaskHdrInfo> GetDeleteAccessableTaskByUserID(int userID, short roleID, int recPeriodID, ARTEnums.TaskType taskType,
            short? taskCategoryID, bool? isShowHidden)
        {
            ITaskMaster oTaskMasterClient = RemotingHelper.GetTaskMasterObject();
            List<TaskHdrInfo> oTaskHdrInfoCollection = oTaskMasterClient.GetDeleteAccessableTaskByUserID(userID, roleID, recPeriodID, taskType
                , taskCategoryID, isShowHidden, SessionHelper.GetUserLanguage(), AppSettingHelper.GetDefaultLanguageID(), Helper.GetAppUserInfo());
            LanguageHelper.TranslateLabelsTaskHdrInfo(oTaskHdrInfoCollection);
            return oTaskHdrInfoCollection;

        }

        public static List<TaskHdrInfo> GetAccessableTaskForBulkEdit(int userID, short roleID, int recPeriodID, ARTEnums.TaskType taskType,
             short? taskCategoryID, bool? isShowHidden)
        {
            ITaskMaster oTaskMasterClient = RemotingHelper.GetTaskMasterObject();
            List<TaskHdrInfo> oTaskHdrInfoCollection = oTaskMasterClient.GetAccessableTaskForBulkEdit(userID, roleID, recPeriodID, taskType
                , taskCategoryID, isShowHidden, SessionHelper.GetUserLanguage(), AppSettingHelper.GetDefaultLanguageID(), Helper.GetAppUserInfo());
            LanguageHelper.TranslateLabelsTaskHdrInfo(oTaskHdrInfoCollection);
            return oTaskHdrInfoCollection;

        }


        public static List<TaskHdrInfo> GetPendingOverdueAccessableTaskByUserID(int userID, short roleID, int recPeriodID, ARTEnums.TaskType taskType
                                                                                , List<FilterCriteria> filterCriteriaList, short? taskCategory, bool? isShowHidden, short? TaskCompletionStatusID, bool? isShowAllTask)
        {
            ITaskMaster oTaskMasterClient = RemotingHelper.GetTaskMasterObject();
            List<TaskHdrInfo> oTaskHdrInfoCollection = oTaskMasterClient.GetPendingOverdueAccessableTaskByUserID(userID, roleID, recPeriodID, taskType, filterCriteriaList,
                taskCategory, isShowHidden, SessionHelper.GetUserLanguage(), AppSettingHelper.GetDefaultLanguageID(), Helper.GetAppUserInfo(), TaskCompletionStatusID, isShowAllTask);
            LanguageHelper.TranslateLabelsTaskHdrInfo(oTaskHdrInfoCollection);
            return oTaskHdrInfoCollection;

        }

        public static List<TaskHdrInfo> GetCompletedAccessableTaskByUserID(int userID, short roleID, int recPeriodID, ARTEnums.TaskType taskType
                                                                                , List<FilterCriteria> filterCriteriaList, short? taskCategory, bool? isShowHidden)
        {
            ITaskMaster oTaskMasterClient = RemotingHelper.GetTaskMasterObject();
            List<TaskHdrInfo> oTaskHdrInfoCollection = oTaskMasterClient.GetCompletedAccessableTaskByUserID(userID, roleID, recPeriodID, taskType, filterCriteriaList,
                taskCategory, isShowHidden, SessionHelper.GetUserLanguage(), AppSettingHelper.GetDefaultLanguageID(), Helper.GetAppUserInfo());
            LanguageHelper.TranslateLabelsTaskHdrInfo(oTaskHdrInfoCollection);
            return oTaskHdrInfoCollection;

        }

        public static List<TaskHdrInfo> GetAccessableTaskByAccountIDs(int userID, short roleID, int recPeriodID, List<long> AccountIDs,
            short? taskCategoryID, bool? isShowHidden)
        {
            ITaskMaster oTaskMasterClient = RemotingHelper.GetTaskMasterObject();
            List<TaskHdrInfo> oTaskHdrInfoCollection = oTaskMasterClient.GetAccessableTaskByAccountIDs(userID, roleID, recPeriodID, AccountIDs,
                taskCategoryID, isShowHidden, SessionHelper.GetUserLanguage(), AppSettingHelper.GetDefaultLanguageID(), Helper.GetAppUserInfo());
            LanguageHelper.TranslateLabelsTaskHdrInfo(oTaskHdrInfoCollection);
            return oTaskHdrInfoCollection;

        }
        public static List<TaskHdrInfo> GetAccessableTaskByAccountIDs(long? AccountID, int? NetAccountID)
        {

            int userID = SessionHelper.CurrentUserID.GetValueOrDefault();
            short roleID = SessionHelper.CurrentRoleID.GetValueOrDefault();
            int recPeriodID = SessionHelper.CurrentReconciliationPeriodID.GetValueOrDefault();

            List<long> AccountIDs = new List<long>();
            if (AccountID.GetValueOrDefault() > 0)
            {
                AccountIDs.Add(AccountID.Value);
            }
            else if (NetAccountID.GetValueOrDefault() > 0)
            {
                AccountIDs = NetAccountHelper.GetAllConstituentAccounts(NetAccountID.Value, recPeriodID);
            }
            List<TaskHdrInfo> taskGridData = TaskMasterHelper.GetAccessableTaskByAccountIDs(userID, roleID, recPeriodID, AccountIDs, null, false);
            return taskGridData;
        }

        public static DataTable AddTask(List<TaskHdrInfo> oTasksHdrList, int recPeriodID, string addedBy, DateTime dateAdded, List<AttachmentInfo> oAttachmentInfoCollection)
        {
            ITaskMaster oTaskMasterClient = RemotingHelper.GetTaskMasterObject();
            return oTaskMasterClient.AddTask(oTasksHdrList, recPeriodID, addedBy, dateAdded, oAttachmentInfoCollection, Helper.GetAppUserInfo());

        }

        public static List<TaskRecurrenceTypeMstInfo> GetTaskRecurrenceTypeMstInfoCollection()
        {
            ITaskMaster oTaskMasterClient = RemotingHelper.GetTaskMasterObject();
            return LanguageHelper.TranslateTaskRecurrenceTypeMstInfoCollection(oTaskMasterClient.GetTaskRecurrenceTypeMstInfoCollection(Helper.GetAppUserInfo()));

        }

        public static List<TaskListHdrInfo> GetTaskListHdrInfoCollection(int recPeriodID)
        {
            ITaskMaster oTaskMasterClient = RemotingHelper.GetTaskMasterObject();
            return oTaskMasterClient.GetTaskListHdrInfoCollection(recPeriodID, Helper.GetAppUserInfo());

        }
        public static List<TaskSubListHdrInfo> GetTaskSubListHdrInfoCollection(int recPeriodID)
        {
            ITaskMaster oTaskMasterClient = RemotingHelper.GetTaskMasterObject();
            return oTaskMasterClient.GetTaskSubListHdrInfoCollection(recPeriodID, Helper.GetAppUserInfo());

        }
        public static TaskHdrInfo GetTaskHdrInfoByTaskID(long? TaskID, int? recPeriodID)
        {
            ITaskMaster oTaskMasterClient = RemotingHelper.GetTaskMasterObject();
            return oTaskMasterClient.GetTaskHdrInfoByTaskID(TaskID, recPeriodID, Helper.GetAppUserInfo());
        }

        public static List<AccountHdrInfo> GetTaskAccountHdrInfoList(List<long> oAccountIDList, int? recperiodID)
        {
            IAccount oAccountClient = RemotingHelper.GetAccountObject();
            List<AccountHdrInfo> objAccountHdrInfoCollection = oAccountClient.GetAccountHdrInfoListByAccountIDs(oAccountIDList, recperiodID, Helper.GetAppUserInfo());
            return LanguageHelper.TranslateLabelsAccountHdr(objAccountHdrInfoCollection);
        }

        public static List<long> GetAccountIDListByUserIDs(List<int> oUserIDList, int? recperiodID)
        {
            IAccount oAccountClient = RemotingHelper.GetAccountObject();
            return oAccountClient.GetAccountIDListByUserIDs(oUserIDList, recperiodID, Helper.GetAppUserInfo());
        }

        public static bool EditTask(List<TaskHdrInfo> oTasksHdrList, int recPeriodID, string revisedBy, DateTime dateRevised)
        {
            ITaskMaster oTaskMasterClient = RemotingHelper.GetTaskMasterObject();
            return oTaskMasterClient.EditTask(oTasksHdrList, recPeriodID, revisedBy, dateRevised, Helper.GetAppUserInfo());

        }
        public static List<int> GetRecurrenceFrequencyByTaskID(long? TaskID, int? recPeriodID)
        {
            ITaskMaster oTaskMasterClient = RemotingHelper.GetTaskMasterObject();
            return oTaskMasterClient.GetRecurrenceFrequencyByTaskID(TaskID, recPeriodID, Helper.GetAppUserInfo());
        }

        public static List<TaskAttributeValueInfo> GetTaskAttributeList(int? RecPeriodID, List<long> TaskIDs, List<short> AttributeIDs)
        {
            ITaskMaster oTaskMasterClient = RemotingHelper.GetTaskMasterObject();
            return oTaskMasterClient.GetTaskAttributeList(RecPeriodID.Value, TaskIDs, AttributeIDs, Helper.GetAppUserInfo());
        }

        public static void DeleteAccessibleTasks(List<long> TaskIDs, int RecPeriodID, string ModifiedBy, DateTime DateModified)
        {
            ITaskMaster oTaskMasterClient = RemotingHelper.GetTaskMasterObject();
            oTaskMasterClient.DeleteAccessibleTasks(TaskIDs, RecPeriodID, ModifiedBy, DateModified, Helper.GetAppUserInfo());
        }

        public static bool BulkEditTask(List<TaskHdrInfo> oTasksHdrList, int recPeriodID, string revisedBy, DateTime dateRevised)
        {
            ITaskMaster oTaskMasterClient = RemotingHelper.GetTaskMasterObject();
            return oTaskMasterClient.BulkEditTask(oTasksHdrList, recPeriodID, revisedBy, dateRevised, Helper.GetAppUserInfo());

        }

        public static List<TaskHdrInfo> GetTasksByDataImportID(int DataImportID)
        {
            ITaskMaster oTaskMasterClient = RemotingHelper.GetTaskMasterObject();
            List<TaskHdrInfo> oTaskHdrInfoCollection = oTaskMasterClient.GetTasksByDataImportID(DataImportID, Helper.GetAppUserInfo());
            return oTaskHdrInfoCollection;

        }
        #endregion

        public static void SaveAttachementsToDB(long? RecordID, int? RecordTypeID, List<AttachmentInfo> Attachments)
        {
            IAttachment oAttachmentClient = null;
            try
            {
                oAttachmentClient = RemotingHelper.GetAttachmentObject();
                if (RecordID.HasValue && RecordTypeID.HasValue && Attachments != null && Attachments.Count > 0)
                {
                    foreach (AttachmentInfo attachment in Attachments)
                    {
                        if (attachment.AttachmentID.HasValue)
                        {
                            oAttachmentClient.InsertAttachment(attachment, SessionHelper.CurrentReconciliationPeriodID.Value, Helper.GetAppUserInfo());
                        }
                    }
                }
            }
            finally
            {
                oAttachmentClient = null;
            }
        }

        public static List<AttachmentInfo> GetTaskAttachments(long? RecordID, ARTEnums.RecordType RecordType)
        {
            List<AttachmentInfo> oAttachmentInfoList = null;
            IAttachment oAttachmentClient = null;
            try
            {
                oAttachmentClient = RemotingHelper.GetAttachmentObject();
                oAttachmentInfoList = oAttachmentClient.GetAttachment(RecordID.Value, (Int32)RecordType, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
            }
            finally
            {
                oAttachmentClient = null;
            }
            return oAttachmentInfoList;
        }

        public static List<TaskActionTypeTaskSatusInfo> GetTaskStatusByTaskActionTypeID(short TaskActionTypeID)
        {
            return CacheHelper.GetTaskStatusByTaskActionTypeID(TaskActionTypeID);
        }

        public static void RaiseTaskAlert(List<TaskHdrInfo> oTaskHdrInfoList)
        {
            List<int> assignedAssignedToIDCollection = new List<int>();
            List<int> assignedReviewerIDCollection = new List<int>();
            List<int> assignedApproverIDCollection = new List<int>();
            List<int> assignedNotifyIDCollection = new List<int>();
            foreach (var oTaskHdr in oTaskHdrInfoList)
            {

                if (oTaskHdr.AssignedTo != null && oTaskHdr.AssignedTo.Count > 0)
                {
                    foreach (var oUserHdrInfo in oTaskHdr.AssignedTo)
                    {
                        if (oUserHdrInfo.UserID.HasValue && !assignedAssignedToIDCollection.Contains(oUserHdrInfo.UserID.Value))
                            assignedAssignedToIDCollection.Add(oUserHdrInfo.UserID.Value);
                    }
                }
                if (oTaskHdr.Reviewer != null && oTaskHdr.Reviewer.Count > 0)
                {
                    foreach (var oUserHdrInfo in oTaskHdr.Reviewer)
                    {
                        if (oUserHdrInfo.UserID.HasValue && !assignedReviewerIDCollection.Contains(oUserHdrInfo.UserID.Value))
                            assignedReviewerIDCollection.Add(oUserHdrInfo.UserID.Value);
                    }
                }
                if (oTaskHdr.Approver != null && oTaskHdr.Approver.Count > 0)
                {
                    foreach (var oUserHdrInfo in oTaskHdr.Approver)
                    {
                        if (oUserHdrInfo.UserID.HasValue && !assignedApproverIDCollection.Contains(oUserHdrInfo.UserID.Value))
                            assignedApproverIDCollection.Add(oUserHdrInfo.UserID.Value);
                    }
                }
                if (oTaskHdr.Notify != null && oTaskHdr.Notify.Count > 0)
                {
                    foreach (var oUserHdrInfo in oTaskHdr.Notify)
                    {
                        if (oUserHdrInfo.UserID.HasValue && !assignedNotifyIDCollection.Contains(oUserHdrInfo.UserID.Value))
                            assignedNotifyIDCollection.Add(oUserHdrInfo.UserID.Value);
                    }
                }
            }
            // Raise alert
            AlertHelper.RaiseAlertForAssignedTask(assignedAssignedToIDCollection, assignedReviewerIDCollection, assignedApproverIDCollection, assignedNotifyIDCollection, oTaskHdrInfoList);
        }
        public static void ShowFilterIcon(GridItemEventArgs e, ARTEnums.Grid eGrid)
        {
            string sessionKey = SessionHelper.GetSessionKeyForGridFilter(eGrid);
            List<FilterCriteria> oFilterCriteriaCollection = (List<FilterCriteria>)HttpContext.Current.Session[sessionKey];
            Control oControl = new Control();

            if (oFilterCriteriaCollection != null && oFilterCriteriaCollection.Count > 0)
            {

                foreach (FilterCriteria oFilterCriteria in oFilterCriteriaCollection)
                {
                    switch (oFilterCriteria.ParameterID)
                    {
                        case (short)WebEnums.TaskColumnForFilter.TaskNumber:
                            oControl = (e.Item as GridHeaderItem)["TaskNumber"].Controls[0];
                            break;

                        case (short)WebEnums.TaskColumnForFilter.TaskName:
                            oControl = (e.Item as GridHeaderItem)["TaskName"].Controls[0];
                            break;

                        case (short)WebEnums.TaskColumnForFilter.TaskStatus:
                            oControl = (e.Item as GridHeaderItem)["TaskStatus"].Controls[0];
                            break;

                        case (short)WebEnums.TaskColumnForFilter.Description:
                            oControl = (e.Item as GridHeaderItem)["Description"].Controls[0];
                            break;

                        case (short)WebEnums.TaskColumnForFilter.AttachmentCount:
                            oControl = (e.Item as GridHeaderItem)["AttachmentCount"].Controls[0];
                            break;
                        case (short)WebEnums.TaskColumnForFilter.StartDate:
                            oControl = (e.Item as GridHeaderItem)["StartDate"].Controls[0];
                            break;

                        case (short)WebEnums.TaskColumnForFilter.DueDate:
                            oControl = (e.Item as GridHeaderItem)["DueDate"].Controls[0];
                            break;
                        case (short)WebEnums.TaskColumnForFilter.AssigneeDueDate:
                            oControl = (e.Item as GridHeaderItem)["AssigneeDueDate"].Controls[0];
                            break;

                        case (short)WebEnums.TaskColumnForFilter.TaskDuration:
                            oControl = (e.Item as GridHeaderItem)["TaskDuration"].Controls[0];
                            break;

                        case (short)WebEnums.TaskColumnForFilter.RecurrenceType:
                            oControl = (e.Item as GridHeaderItem)["RecurrenceType"].Controls[0];
                            break;

                        case (short)WebEnums.TaskColumnForFilter.AssignedTo:
                            oControl = (e.Item as GridHeaderItem)["TaskOwner"].Controls[0];
                            break;

                        case (short)WebEnums.TaskColumnForFilter.Approver:
                            oControl = (e.Item as GridHeaderItem)["TaskApprover"].Controls[0];
                            break;

                        case (short)WebEnums.TaskColumnForFilter.ApprovalDuration:
                            oControl = (e.Item as GridHeaderItem)["ApprovalDuration"].Controls[0];
                            break;
                        case (short)WebEnums.TaskColumnForFilter.CompletionDate:
                            oControl = (e.Item as GridHeaderItem)["CompletionDate"].Controls[0];
                            break;

                        case (short)WebEnums.TaskColumnForFilter.CompletionDocs:
                            oControl = (e.Item as GridHeaderItem)["CompletionDocs"].Controls[0];
                            break;

                        case (short)WebEnums.TaskColumnForFilter.CreatedBy:
                            oControl = (e.Item as GridHeaderItem)["CreatedBy"].Controls[0];
                            break;
                        case (short)WebEnums.TaskColumnForFilter.CustomField1:
                            if ((e.Item as GridHeaderItem)["TaskCustomField1"] != null && (e.Item as GridHeaderItem)["TaskCustomField1"].Controls.Count > 0)
                                oControl = (e.Item as GridHeaderItem)["TaskCustomField1"].Controls[0];
                            break;
                        case (short)WebEnums.TaskColumnForFilter.CustomField2:
                            if ((e.Item as GridHeaderItem)["TaskCustomField2"] != null && (e.Item as GridHeaderItem)["TaskCustomField2"].Controls.Count > 0)
                                oControl = (e.Item as GridHeaderItem)["TaskCustomField2"].Controls[0];
                            break;
                    }

                    if (oControl is LinkButton)
                    {
                        LinkButton oLinkButton = (LinkButton)oControl;
                        if (!oLinkButton.Text.Contains(URLConstants.URL_TASK_FILTER_ICON))
                            oLinkButton.Text = oLinkButton.Text + URLConstants.URL_TASK_FILTER_ICON;

                    }
                    else
                    {
                        if (oControl is LiteralControl)
                        {
                            LiteralControl oLiteralControl = (LiteralControl)oControl;
                            if (!oLiteralControl.Text.Contains(URLConstants.URL_TASK_FILTER_ICON))
                                oLiteralControl.Text = oLiteralControl.Text + URLConstants.URL_TASK_FILTER_ICON;

                        }
                    }
                }
            }
        }


        public static WebEnums.FormMode GetFormModeForTaskViewer(TaskHdrInfo oTaskHdrInfo)
        {
            WebEnums.FormMode eFormMode = WebEnums.FormMode.ReadOnly;

            // Check for Task Added By 
            if (oTaskHdrInfo.AddedBy != SessionHelper.CurrentUserLoginID)
            {
                eFormMode = WebEnums.FormMode.ReadOnly;
            }
            else
            {
                eFormMode = WebEnums.FormMode.Edit;
            }

            if (eFormMode == WebEnums.FormMode.ReadOnly)
            {
                return eFormMode;
            }
            // Check for IsDeleted 
            if (oTaskHdrInfo.IsDeleted.HasValue && oTaskHdrInfo.IsDeleted.Value)
            {
                eFormMode = WebEnums.FormMode.ReadOnly;
            }
            else
            {
                eFormMode = WebEnums.FormMode.Edit;
            }
            if (eFormMode == WebEnums.FormMode.ReadOnly)
            {
                return eFormMode;
            }
            // Check for IsDeleted 
            if (oTaskHdrInfo.TaskStatusID.HasValue && oTaskHdrInfo.TaskStatusID.Value == (short)ARTEnums.TaskStatus.Completed)
            {
                eFormMode = WebEnums.FormMode.ReadOnly;
            }
            else
            {
                eFormMode = WebEnums.FormMode.Edit;
            }
            if (eFormMode == WebEnums.FormMode.ReadOnly)
            {
                return eFormMode;
            }
            return eFormMode;
        }

        public static void AddEditUserTaskVisibility(List<TaskHdrInfo> oTasksHdrList, int? CurrentUserID, int? recPeriodID, string addedBy, string revisedBy, bool isHidden)
        {
            ITaskMaster oTaskMasterClient = RemotingHelper.GetTaskMasterObject();
            oTaskMasterClient.AddEditUserTaskVisibility(oTasksHdrList, CurrentUserID, recPeriodID, addedBy, revisedBy, isHidden, Helper.GetAppUserInfo());
        }
        public static void ShowFilterIconAccountTask(GridItemEventArgs e, ARTEnums.Grid eGrid)
        {
            string sessionKey = SessionHelper.GetSessionKeyForGridFilter(eGrid);
            List<FilterCriteria> oFilterCriteriaCollection = (List<FilterCriteria>)HttpContext.Current.Session[sessionKey];
            Control oControl = new Control();

            if (oFilterCriteriaCollection != null && oFilterCriteriaCollection.Count > 0)
            {

                foreach (FilterCriteria oFilterCriteria in oFilterCriteriaCollection)
                {
                    ShowFilterIconForOrgHierarchyColumns(e, oFilterCriteriaCollection);
                    switch (oFilterCriteria.ParameterID)
                    {
                        case (short)WebEnums.StaticAccountField.AccountName:
                            oControl = (e.Item as GridHeaderItem)["AccountName"].Controls[0];
                            break;

                        case (short)WebEnums.StaticAccountField.AccountNumber:
                            oControl = (e.Item as GridHeaderItem)["AccountNumber"].Controls[0];
                            break;

                        case (short)WebEnums.StaticAccountField.FSCaption:
                            oControl = (e.Item as GridHeaderItem)["FSCaption"].Controls[0];
                            break;

                        case (short)WebEnums.StaticAccountField.AccountType:
                            oControl = (e.Item as GridHeaderItem)["AccountType"].Controls[0];
                            break;

                        case (short)WebEnums.TaskColumnForFilter.TaskNumber:
                            oControl = (e.Item as GridHeaderItem)["TaskNumber"].Controls[0];
                            break;

                        case (short)WebEnums.TaskColumnForFilter.TaskName:
                            oControl = (e.Item as GridHeaderItem)["TaskName"].Controls[0];
                            break;
                        case (short)WebEnums.TaskColumnForFilter.TaskStatus:
                            oControl = (e.Item as GridHeaderItem)["TaskStatus"].Controls[0];
                            break;

                        case (short)WebEnums.TaskColumnForFilter.Description:
                            oControl = (e.Item as GridHeaderItem)["Description"].Controls[0];
                            break;

                        case (short)WebEnums.TaskColumnForFilter.AttachmentCount:
                            oControl = (e.Item as GridHeaderItem)["AttachmentCount"].Controls[0];
                            break;
                        case (short)WebEnums.TaskColumnForFilter.StartDate:
                            oControl = (e.Item as GridHeaderItem)["StartDate"].Controls[0];
                            break;

                        case (short)WebEnums.TaskColumnForFilter.DueDate:
                            oControl = (e.Item as GridHeaderItem)["DueDate"].Controls[0];
                            break;
                        case (short)WebEnums.TaskColumnForFilter.AssigneeDueDate:
                            oControl = (e.Item as GridHeaderItem)["AssigneeDueDate"].Controls[0];
                            break;

                        case (short)WebEnums.TaskColumnForFilter.TaskDuration:
                            oControl = (e.Item as GridHeaderItem)["TaskDuration"].Controls[0];
                            break;

                        case (short)WebEnums.TaskColumnForFilter.RecurrenceType:
                            oControl = (e.Item as GridHeaderItem)["RecurrenceType"].Controls[0];
                            break;

                        case (short)WebEnums.TaskColumnForFilter.AssignedTo:
                            oControl = (e.Item as GridHeaderItem)["TaskOwner"].Controls[0];
                            break;

                        case (short)WebEnums.TaskColumnForFilter.Approver:
                            oControl = (e.Item as GridHeaderItem)["TaskApprover"].Controls[0];
                            break;

                        case (short)WebEnums.TaskColumnForFilter.ApprovalDuration:
                            oControl = (e.Item as GridHeaderItem)["ApprovalDuration"].Controls[0];
                            break;
                        case (short)WebEnums.TaskColumnForFilter.CompletionDate:
                            oControl = (e.Item as GridHeaderItem)["CompletionDate"].Controls[0];
                            break;

                        case (short)WebEnums.TaskColumnForFilter.CompletionDocs:
                            oControl = (e.Item as GridHeaderItem)["CompletionDocs"].Controls[0];
                            break;

                        case (short)WebEnums.TaskColumnForFilter.CreatedBy:
                            oControl = (e.Item as GridHeaderItem)["CreatedBy"].Controls[0];
                            break;
                    }

                    if (oControl is LinkButton)
                    {
                        LinkButton oLinkButton = (LinkButton)oControl;
                        if (!oLinkButton.Text.Contains(URLConstants.URL_TASK_FILTER_ICON))
                            oLinkButton.Text = oLinkButton.Text + URLConstants.URL_TASK_FILTER_ICON;

                    }
                    else
                    {
                        if (oControl is LiteralControl)
                        {
                            LiteralControl oLiteralControl = (LiteralControl)oControl;
                            if (!oLiteralControl.Text.Contains(URLConstants.URL_TASK_FILTER_ICON))
                                oLiteralControl.Text = oLiteralControl.Text + URLConstants.URL_TASK_FILTER_ICON;

                        }
                    }
                }
            }
        }
        private static void ShowFilterIconForOrgHierarchyColumns(GridItemEventArgs e, List<FilterCriteria> oFilterCriteriaCollection)
        {
            Control oControl = new Control();

            if (oFilterCriteriaCollection != null && oFilterCriteriaCollection.Count > 0)
            {
                foreach (FilterCriteria oFilterCriteria in oFilterCriteriaCollection)
                {
                    switch (oFilterCriteria.ParameterID)
                    {
                        case (short)WebEnums.GeographyClass.Key2:
                            oControl = (e.Item as GridHeaderItem)["Key2"].Controls[0];
                            break;

                        case (short)WebEnums.GeographyClass.Key3:
                            oControl = (e.Item as GridHeaderItem)["Key3"].Controls[0];
                            break;

                        case (short)WebEnums.GeographyClass.Key4:
                            oControl = (e.Item as GridHeaderItem)["Key4"].Controls[0];
                            break;

                        case (short)WebEnums.GeographyClass.Key5:
                            oControl = (e.Item as GridHeaderItem)["Key5"].Controls[0];
                            break;

                        case (short)WebEnums.GeographyClass.Key6:
                            oControl = (e.Item as GridHeaderItem)["Key6"].Controls[0];
                            break;

                        case (short)WebEnums.GeographyClass.Key7:
                            oControl = (e.Item as GridHeaderItem)["Key7"].Controls[0];
                            break;

                        case (short)WebEnums.GeographyClass.Key8:
                            oControl = (e.Item as GridHeaderItem)["Key8"].Controls[0];
                            break;

                        case (short)WebEnums.GeographyClass.Key9:
                            oControl = (e.Item as GridHeaderItem)["Key9"].Controls[0];
                            break;
                    }

                    if (oFilterCriteria.ParameterID >= (short)WebEnums.GeographyClass.Key2
                        && oFilterCriteria.ParameterID <= (short)WebEnums.GeographyClass.Key9)
                    {
                        if (oControl is LinkButton)
                        {
                            LinkButton oLinkButton = (LinkButton)oControl;
                            if (!oLinkButton.Text.Contains(URLConstants.URL_TASK_FILTER_ICON))
                                oLinkButton.Text = oLinkButton.Text + URLConstants.URL_TASK_FILTER_ICON;

                        }
                        else
                        {
                            if (oControl is LiteralControl)
                            {
                                LiteralControl oLiteralControl = (LiteralControl)oControl;
                                if (!oLiteralControl.Text.Contains(URLConstants.URL_TASK_FILTER_ICON))
                                    oLiteralControl.Text = oLiteralControl.Text + URLConstants.URL_TASK_FILTER_ICON;

                            }
                        }
                    }
                }
            }
        }
        public static bool EditTaskList(int? TasksListID, string TasksListName, string revisedBy, DateTime dateRevised)
        {
            ITaskMaster oTaskMasterClient = RemotingHelper.GetTaskMasterObject();
            return oTaskMasterClient.EditTaskList(TasksListID, TasksListName, revisedBy, dateRevised, Helper.GetAppUserInfo());

        }
        public static int GetTaskListIDByName(string TaskListName)
        {
            ITaskMaster oTaskMasterClient = RemotingHelper.GetTaskMasterObject();
            return oTaskMasterClient.GetTaskListIDByName(TaskListName, SessionHelper.CurrentCompanyID, Helper.GetAppUserInfo());
        }
        public static bool EditTaskSubList(int? TasksSubListID, string TasksSubListName, string revisedBy, DateTime dateRevised)
        {
            ITaskMaster oTaskMasterClient = RemotingHelper.GetTaskMasterObject();
            return oTaskMasterClient.EditTaskSubList(TasksSubListID, TasksSubListName, revisedBy, dateRevised, Helper.GetAppUserInfo());

        }
        public static int GetTaskSubListIDByName(string TaskSubListName)
        {
            ITaskMaster oTaskMasterClient = RemotingHelper.GetTaskMasterObject();
            return oTaskMasterClient.GetTaskSubListIDByName(TaskSubListName, SessionHelper.CurrentCompanyID, Helper.GetAppUserInfo());
        }

        public static List<DataImportHdrInfo> DeleteTaskLoad(int DataImportID, string revisedBy, DateTime dateRevised)
        {
            ITaskMaster oTaskMasterClient = RemotingHelper.GetTaskMasterObject();
            return oTaskMasterClient.DeleteTaskLoad(DataImportID, revisedBy, dateRevised, Helper.GetAppUserInfo());

        }
        public static List<DataImportHdrInfo> DeleteTasks(List<long> SelectedTaskIDs, int CompanyID, string revisedBy, DateTime dateRevised)
        {
            ITaskMaster oTaskMasterClient = RemotingHelper.GetTaskMasterObject();
            return oTaskMasterClient.DeleteTasks(SelectedTaskIDs, CompanyID, revisedBy, dateRevised, Helper.GetAppUserInfo());

        }
        public static void NotifyUsers(List<TaskHdrInfo> oSelectedTaskHdrInfoList, List<int> AllUserIDCollection, string BodyDescription, string MailSubject)
        {
            if (oSelectedTaskHdrInfoList != null && oSelectedTaskHdrInfoList.Count > 0 && AllUserIDCollection != null && AllUserIDCollection.Count > 0)
            {
                AlertHelper.NotifyForUser(AllUserIDCollection, oSelectedTaskHdrInfoList, BodyDescription, MailSubject);
            }
        }
        public static List<int> GetAllNotifyUserIDCollection(List<long> oSelectedTaskIDList)
        {
            List<int> AllUserIDCollection = new List<int>();
            if (oSelectedTaskIDList != null && oSelectedTaskIDList.Count > 0)
            {
                List<short> _attributeIDs = new List<short>();
                _attributeIDs.Add((short)ARTEnums.TaskAttribute.AssignedTo);
                _attributeIDs.Add((short)ARTEnums.TaskAttribute.Reviewer);
                _attributeIDs.Add((short)ARTEnums.TaskAttribute.Notify);
                var taskAttributeList = TaskMasterHelper.GetTaskAttributeList(SessionHelper.CurrentReconciliationPeriodID, oSelectedTaskIDList, _attributeIDs);
                if (taskAttributeList != null && taskAttributeList.Count > 0)
                {
                    var AssignedToList = (from ta in taskAttributeList
                                          where ta.TaskAttributeID.Value == ((short)ARTEnums.TaskAttribute.AssignedTo)
                                          select ta).ToList();
                    if (AssignedToList != null)
                    {
                        foreach (var item in AssignedToList)
                        {
                            if (item.ReferenceID.HasValue && item.ReferenceID.Value > 0)
                                AllUserIDCollection.Add(Convert.ToInt32(item.ReferenceID));
                        }
                    }
                    var ApproverList = (from ta in taskAttributeList
                                        where ta.TaskAttributeID.Value == ((short)ARTEnums.TaskAttribute.Reviewer)
                                        select ta).ToList();
                    if (ApproverList != null)
                    {
                        foreach (var item in ApproverList)
                        {
                            if (item.ReferenceID.HasValue && item.ReferenceID.Value > 0)
                                AllUserIDCollection.Add(Convert.ToInt32(item.ReferenceID));
                        }
                    }
                    var notifyList = (from ta in taskAttributeList
                                      where ta.TaskAttributeID.Value == ((short)ARTEnums.TaskAttribute.Notify)
                                      select ta).ToList();
                    if (notifyList != null)
                    {
                        foreach (var item in notifyList)
                        {
                            if (item.ReferenceID.HasValue && item.ReferenceID.Value > 0)
                                AllUserIDCollection.Add(Convert.ToInt32(item.ReferenceID));
                        }
                    }
                }
                AllUserIDCollection = AllUserIDCollection.Distinct().ToList<int>();
            }
            return AllUserIDCollection;
        }
        public static IList<ReconciliationPeriodInfo> GetRecPeriodNumberList(int? companyID, DateTime? TaskRecPeriodEndDate)
        {
            ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
            return oCompanyClient.SelectAllRecPeriodNumberByCompanyID(companyID, TaskRecPeriodEndDate, Helper.GetAppUserInfo());
        }

        public static List<TaskCustomFieldInfo> GetTaskCustomFields(int? RecPeriodID, int? CompanyID)
        {
            List<TaskCustomFieldInfo> oTaskCustomFieldInfoList = null;
            ITaskMaster oTaskMasterClient = RemotingHelper.GetTaskMasterObject();
            oTaskCustomFieldInfoList = oTaskMasterClient.GetTaskCustomFields(SessionHelper.CurrentReconciliationPeriodID, SessionHelper.CurrentCompanyID, Helper.GetAppUserInfo());
            LanguageHelper.TranslateLabelsTaskCustomFieldInfo(oTaskCustomFieldInfoList);
            return oTaskCustomFieldInfoList;
        }
        public static void SaveTaskCustomFields(List<TaskCustomFieldInfo> oTaskCustomFieldInfoList, int? CompanyID, int? RecPeriodID, string ModifiedBy, DateTime DateModified)
        {
            ITaskMaster oTaskMasterClient = RemotingHelper.GetTaskMasterObject();
            oTaskMasterClient.SaveTaskCustomFields(oTaskCustomFieldInfoList, RecPeriodID, CompanyID, ModifiedBy, DateModified, Helper.GetAppUserInfo());
        }

        public static void UpdateTaskStatusCommentAttachments(List<long> taskDetailIDList, ARTEnums.TaskActionType taskActionTypeID, string subjectLine, string comments, List<AttachmentInfo> newlyAddedAttachment)
        {
            ITaskMaster oTaskMasterClient = RemotingHelper.GetTaskMasterObject();
            oTaskMasterClient.UpdateTaskStatusCommentAttachments(taskDetailIDList, SessionHelper.CurrentUserLoginID, DateTime.Now, SessionHelper.CurrentReconciliationPeriodID, taskActionTypeID
                                    , subjectLine, comments, SessionHelper.CurrentUserID, newlyAddedAttachment, Helper.GetAppUserInfo());
        }
        public static void UpdateTaskStatus(List<long> taskDetailIDList, ARTEnums.TaskActionType taskActionTypeID)
        {
            ITaskMaster oTaskMasterClient = RemotingHelper.GetTaskMasterObject();
            oTaskMasterClient.UpdateTasksStatus(taskDetailIDList, taskActionTypeID, SessionHelper.CurrentReconciliationPeriodID, SessionHelper.CurrentUserLoginID, DateTime.Now, Helper.GetAppUserInfo());
        }
    }
}
