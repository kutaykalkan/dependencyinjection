﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.App.DAO;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Data;
using System.Data;
using System.Globalization;
using System.Data.SqlClient;

namespace SkyStem.ART.App.Services
{
    public class TaskMaster : ITaskMaster
    {
        public List<TaskDetailReviewNoteDetailInfo> SelectAllCommentsByTaskDetailID(Int64? TaskDetailID, AppUserInfo oAppUserInfo)
        {
            List<TaskDetailReviewNoteDetailInfo> oTaskDetailReviewNoteDetailInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskDetailReviewNoteDetailDAO oTaskDetailReviewNoteDetailDAO = new TaskDetailReviewNoteDetailDAO(oAppUserInfo);
                oTaskDetailReviewNoteDetailInfoCollection = oTaskDetailReviewNoteDetailDAO.SelectAllCommentsByTaskDetailID(TaskDetailID);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oTaskDetailReviewNoteDetailInfoCollection;
        }


        public List<TaskHdrInfo> GetAccessableTaskByUserID(int userID, short roleID, int recPeriodID, ARTEnums.TaskType taskType
            , List<TaskStatusMstInfo> taskStatusMstInfoList, List<FilterCriteria> filterCriteriaList, AppUserInfo oAppUserInfo)
        {
            List<TaskHdrInfo> oTaskHdrInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskHdrDAO oTaskHdrDAO = new TaskHdrDAO(oAppUserInfo);
                oTaskHdrInfoList = oTaskHdrDAO.GetAccessableTaskByUserID(userID, roleID, recPeriodID, taskType, taskStatusMstInfoList, filterCriteriaList);

                //AttachmentDAO oAttachmentDAO = new AttachmentDAO(oAppUserInfo);
                //List<AttachmentInfo> oTaskAttachmentList = oAttachmentDAO.GetTaskAttachment(oTaskHdrInfoList);

                //if (oTaskAttachmentList != null && oTaskAttachmentList.Count > 0)
                //{
                //    foreach (TaskHdrInfo oTask in oTaskHdrInfoList)
                //    {
                //        oTask.CreationAttachment = oTaskAttachmentList.FindAll(r => r.RecordID == oTask.TaskID && r.RecordTypeID.Value == (int)ARTEnums.RecordType.TaskCreation);
                //        oTask.CompletionAttachment = oTaskAttachmentList.FindAll(r => r.RecordID == oTask.TaskDetailID && r.RecordTypeID.Value == (int)ARTEnums.RecordType.TaskComplition);
                //    }
                //}

            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oTaskHdrInfoList;
        }

        public List<TaskHdrInfo> GetPendingOverdueAccessableTaskByUserID(int userID, short roleID, int recPeriodID, ARTEnums.TaskType taskType,
            List<FilterCriteria> filterCriteriaList, short? taskCategoryID, bool? isShowHidden, int? LanguageID, int? DefaultLanguageID, AppUserInfo oAppUserInfo, short? TaskCompletionStatusID, bool? isShowAllTask)
        {
            List<TaskHdrInfo> oTaskHdrInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskHdrDAO oTaskHdrDAO = new TaskHdrDAO(oAppUserInfo);
                oTaskHdrInfoList = oTaskHdrDAO.GetPendingOverdueAccessableTaskByUserID(userID, roleID, recPeriodID, taskType, filterCriteriaList,
                    taskCategoryID, isShowHidden, LanguageID, DefaultLanguageID, TaskCompletionStatusID, isShowAllTask);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oTaskHdrInfoList;
        }

        public List<TaskHdrInfo> GetCompletedAccessableTaskByUserID(int userID, short roleID, int recPeriodID, ARTEnums.TaskType taskType,
            List<FilterCriteria> filterCriteriaList, short? taskCategoryID, bool? isShowHidden, int? LanguageID, int? DefaultLanguageID, AppUserInfo oAppUserInfo)
        {
            List<TaskHdrInfo> oTaskHdrInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskHdrDAO oTaskHdrDAO = new TaskHdrDAO(oAppUserInfo);
                oTaskHdrInfoList = oTaskHdrDAO.GetCompletedAccessableTaskByUserID(userID, roleID, recPeriodID, taskType, filterCriteriaList, taskCategoryID,
                    isShowHidden, LanguageID, DefaultLanguageID);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oTaskHdrInfoList;
        }


        #region "Task Recurrence"
        public List<TaskRecurrenceTypeMstInfo> GetTaskRecurrenceTypeMstInfoCollection(AppUserInfo oAppUserInfo)
        {
            List<TaskRecurrenceTypeMstInfo> oTaskRecurrenceTypeMstInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskRecurrenceTypeMstDAO oTaskRecurrenceTypeMstDAO = new TaskRecurrenceTypeMstDAO(oAppUserInfo);
                oTaskRecurrenceTypeMstInfoList = oTaskRecurrenceTypeMstDAO.GetTaskRecurrenceTypeMstInfoCollection();
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oTaskRecurrenceTypeMstInfoList;
        }
        #endregion

        #region "Task List "
        public List<TaskListHdrInfo> GetTaskListHdrInfoCollection(int recPeriodID, AppUserInfo oAppUserInfo)
        {
            List<TaskListHdrInfo> oTaskListHdrInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskListHdrDAO oTaskListHdrDAO = new TaskListHdrDAO(oAppUserInfo);
                oTaskListHdrInfoList = oTaskListHdrDAO.GetTaskListHdrInfoCollection(recPeriodID);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oTaskListHdrInfoList;
        }
        public int GetTaskListIDByName(string TaskListName, int? CompanyID, AppUserInfo oAppUserInfo)
        {
            int TaskListID = 0;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskListHdrDAO oTaskListHdrDAO = new TaskListHdrDAO(oAppUserInfo);
                TaskListID = oTaskListHdrDAO.GetTaskListIDByName(TaskListName,CompanyID);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return TaskListID;
        }

        public List<TaskSubListHdrInfo> GetTaskSubListHdrInfoCollection(int recPeriodID, AppUserInfo oAppUserInfo)
        {
            List<TaskSubListHdrInfo> oTaskSubListHdrInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskListHdrDAO oTaskListHdrDAO = new TaskListHdrDAO(oAppUserInfo);
                oTaskSubListHdrInfoList = oTaskListHdrDAO.GetTaskSubListHdrInfoCollection(recPeriodID);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oTaskSubListHdrInfoList;
        }
        #endregion

        #region "Add Task"
        public DataTable AddTask(List<TaskHdrInfo> oTasksHdrList, int recPeriodID, string addedBy, DateTime dateAdded, List<AttachmentInfo> oAttachmentInfoCollection, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskHdrDAO oTaskHdrDAO = new TaskHdrDAO(oAppUserInfo);
                return oTaskHdrDAO.AddTask(oTasksHdrList, recPeriodID, addedBy, dateAdded, oAttachmentInfoCollection);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return null;
        }
        #endregion

        #region "TaskAttribute"
        public TaskHdrInfo GetTaskHdrInfoByTaskID(long? TaskID, int? recPeriodID, AppUserInfo oAppUserInfo)
        {
            TaskHdrInfo oTaskHdrInfo = null;
            List<TaskAttributeValueInfo> oTaskAttributeValueInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskAttributeValueDAO oTaskAttributeValueDAO = new TaskAttributeValueDAO(oAppUserInfo);
                oTaskAttributeValueInfoList = oTaskAttributeValueDAO.GetTaskAttributeListByTaskID(TaskID, recPeriodID);
                oTaskHdrInfo = GetTaskHdrInfoFromAttributes(oTaskAttributeValueInfoList, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oTaskHdrInfo;
        }
        private TaskHdrInfo GetTaskHdrInfoFromAttributes(List<TaskAttributeValueInfo> oTaskAttributeValueInfoList, AppUserInfo oAppUserInfo)
        {
            CultureInfo oCult = new CultureInfo(1033);
            TaskHdrInfo oTaskHdrInfo = new TaskHdrInfo();
            ServiceHelper.SetConnectionString(oAppUserInfo);
            if (oTaskAttributeValueInfoList != null && oTaskAttributeValueInfoList.Count > 0)
            {
                oTaskHdrInfo.TaskRecPeriodEndDate = oTaskAttributeValueInfoList[0].TaskRecPeriodEndDate;
                oTaskHdrInfo.TaskNumber = oTaskAttributeValueInfoList[0].TaskNumber;
                oTaskHdrInfo.AddedBy = oTaskAttributeValueInfoList[0].TaskAddedBy;
                oTaskHdrInfo.DateAdded = oTaskAttributeValueInfoList[0].DateCreated;
                foreach (TaskAttributeValueInfo oTaskAttributeValueInfo in oTaskAttributeValueInfoList)
                {
                    if (oTaskAttributeValueInfo.TaskAttributeID.Value == (short)ARTEnums.TaskAttribute.TaskName)
                        oTaskHdrInfo.TaskName = oTaskAttributeValueInfo.Value;
                    if (oTaskAttributeValueInfo.TaskAttributeID.Value == (short)ARTEnums.TaskAttribute.TaskListID)
                    {
                        TaskListHdrInfo oTaskListHdrInfo = new TaskListHdrInfo();
                        oTaskListHdrInfo.TaskListID = (short)oTaskAttributeValueInfo.ReferenceID.GetValueOrDefault();
                        oTaskHdrInfo.TaskList = oTaskListHdrInfo;
                    }
                    if (oTaskAttributeValueInfo.TaskAttributeID.Value == (short)ARTEnums.TaskAttribute.TaskSubListID)
                    {
                        TaskSubListHdrInfo oTaskSubListHdrInfo = new TaskSubListHdrInfo();
                        oTaskSubListHdrInfo.TaskSubListID = (short)oTaskAttributeValueInfo.ReferenceID.GetValueOrDefault();
                        oTaskHdrInfo.TaskSubList = oTaskSubListHdrInfo;
                    }
                    if (oTaskAttributeValueInfo.TaskAttributeID.Value == (short)ARTEnums.TaskAttribute.Description)
                        oTaskHdrInfo.TaskDescription = oTaskAttributeValueInfo.Value;
                    if (oTaskAttributeValueInfo.TaskAttributeID.Value == (short)ARTEnums.TaskAttribute.RecurrenceType)
                    {
                        short? RecurrenceType = (short)oTaskAttributeValueInfo.ReferenceID;
                        if (RecurrenceType.HasValue)
                        {
                            TaskRecurrenceTypeMstInfo oTaskRecurr = new TaskRecurrenceTypeMstInfo();
                            oTaskRecurr.TaskRecurrenceTypeID = RecurrenceType;
                            oTaskRecurr.IsTaskCompleted = oTaskAttributeValueInfo.IsTaskCompleted;
                            oTaskHdrInfo.RecurrenceType = oTaskRecurr;
                        }
                    }
                    if (oTaskAttributeValueInfo.TaskAttributeID.Value == (short)ARTEnums.TaskAttribute.TaskDueDate)
                    {
                        DateTime dt = new DateTime();
                        if (DateTime.TryParse(oTaskAttributeValueInfo.Value, oCult.DateTimeFormat, DateTimeStyles.AssumeLocal, out dt))
                        {
                            oTaskHdrInfo.TaskDueDate = dt;
                        }
                    }
                    if (oTaskAttributeValueInfo.TaskAttributeID.Value == (short)ARTEnums.TaskAttribute.TaskStartDate)
                    {
                        DateTime dt = new DateTime();
                        if (DateTime.TryParse(oTaskAttributeValueInfo.Value, oCult.DateTimeFormat, DateTimeStyles.AssumeLocal, out dt))
                        {
                            oTaskHdrInfo.TaskStartDate = dt;
                        }
                    }

                    if (oTaskAttributeValueInfo.TaskAttributeID.Value == (short)ARTEnums.TaskAttribute.AssigneeDueDate)
                    {
                        DateTime dt = new DateTime();
                        if (DateTime.TryParse(oTaskAttributeValueInfo.Value, oCult.DateTimeFormat, DateTimeStyles.AssumeLocal, out dt))
                        {
                            oTaskHdrInfo.AssigneeDueDate = dt;
                        }
                    }

                    if (oTaskAttributeValueInfo.TaskAttributeID.Value == (short)ARTEnums.TaskAttribute.ReviewerDueDate)
                        oTaskHdrInfo.ReviewerDueDate = Convert.ToDateTime(oTaskAttributeValueInfo.Value.ToString(new CultureInfo(1034)));
                    if (oTaskAttributeValueInfo.TaskAttributeID.Value == (short)ARTEnums.TaskAttribute.TaskDueDays)
                        oTaskHdrInfo.TaskDueDays = Convert.ToInt32(oTaskAttributeValueInfo.Value);
                    if (oTaskAttributeValueInfo.TaskAttributeID.Value == (short)ARTEnums.TaskAttribute.AssigneeDueDays)
                        oTaskHdrInfo.AssigneeDueDays = Convert.ToInt32(oTaskAttributeValueInfo.Value);
                    if (oTaskAttributeValueInfo.TaskAttributeID.Value == (short)ARTEnums.TaskAttribute.ReviewerDueDays)
                        oTaskHdrInfo.ReviewerDueDays = Convert.ToInt32(oTaskAttributeValueInfo.Value);
                    //if (oTaskAttributeValueInfo.TaskAttributeID.Value == (short)ARTEnums.TaskAttribute.AssignedTo)
                    //{
                    //    UserHdrInfo oUserHdrInfo = new UserHdrInfo();
                    //    oUserHdrInfo.UserID = Convert.ToInt32(oTaskAttributeValueInfo.ReferenceID);
                    //    oUserHdrInfo.Name = oTaskAttributeValueInfoList[0].AssigneeName;
                    //    oTaskHdrInfo.AssignedTo = oUserHdrInfo;
                    //}
                    //if (oTaskAttributeValueInfo.TaskAttributeID.Value == (short)ARTEnums.TaskAttribute.Reviewer)
                    //{
                    //    UserHdrInfo oUserHdrInfo = new UserHdrInfo();
                    //    oUserHdrInfo.UserID = Convert.ToInt32(oTaskAttributeValueInfo.ReferenceID);
                    //    oUserHdrInfo.Name = oTaskAttributeValueInfoList[0].ApproverName;
                    //    oTaskHdrInfo.Reviewer = oUserHdrInfo;
                    //}
                    if (oTaskAttributeValueInfo.TaskAttributeID.Value == (short)ARTEnums.TaskAttribute.TaskDueDaysBasis)
                        oTaskHdrInfo.TaskDueDaysBasis = Convert.ToInt16(oTaskAttributeValueInfo.ReferenceID);
                    if (oTaskAttributeValueInfo.TaskAttributeID.Value == (short)ARTEnums.TaskAttribute.TaskDueDaysBasisSkipNumber)
                        oTaskHdrInfo.TaskDueDaysBasisSkipNumber = Convert.ToInt16(oTaskAttributeValueInfo.Value);
                    if (oTaskAttributeValueInfo.TaskAttributeID.Value == (short)ARTEnums.TaskAttribute.AssigneeDueDaysBasis)
                        oTaskHdrInfo.AssigneeDueDaysBasis = Convert.ToInt16(oTaskAttributeValueInfo.ReferenceID);
                    if (oTaskAttributeValueInfo.TaskAttributeID.Value == (short)ARTEnums.TaskAttribute.AssigneeDueDaysBasisSkipNumber)
                        oTaskHdrInfo.AssigneeDueDaysBasisSkipNumber = Convert.ToInt16(oTaskAttributeValueInfo.Value);

                    if (oTaskAttributeValueInfo.TaskAttributeID.Value == (short)ARTEnums.TaskAttribute.ReviewerDueDaysBasis)
                        oTaskHdrInfo.ReviewerDueDaysBasis = Convert.ToInt16(oTaskAttributeValueInfo.ReferenceID);
                    if (oTaskAttributeValueInfo.TaskAttributeID.Value == (short)ARTEnums.TaskAttribute.ReviewerDueDaysBasisSkipNumber)
                        oTaskHdrInfo.ReviewerDueDaysBasisSkipNumber = Convert.ToInt16(oTaskAttributeValueInfo.Value);
                    if (oTaskAttributeValueInfo.TaskAttributeID.Value == (short)ARTEnums.TaskAttribute.CustomField1)
                        oTaskHdrInfo.CustomField1 = oTaskAttributeValueInfo.Value;
                    if (oTaskAttributeValueInfo.TaskAttributeID.Value == (short)ARTEnums.TaskAttribute.CustomField2)
                        oTaskHdrInfo.CustomField2 = oTaskAttributeValueInfo.Value;
                    if (oTaskAttributeValueInfo.TaskAttributeID.Value == (short)ARTEnums.TaskAttribute.DaysType)
                        oTaskHdrInfo.DaysTypeID= Convert.ToInt16(oTaskAttributeValueInfo.ReferenceID);
                }
                // Task  Accounts
                List<TaskAttributeValueInfo> oTaskAccountsValueInfoList = (from oTaskAttributeValueInfo in oTaskAttributeValueInfoList
                                                                           where oTaskAttributeValueInfo.TaskAttributeID.Value == (short)ARTEnums.TaskAttribute.TaskAccount
                                                                           select oTaskAttributeValueInfo).ToList();
                if (oTaskAccountsValueInfoList != null)
                {
                    List<AccountHdrInfo> oAccountHdrInfolist = new List<AccountHdrInfo>();
                    for (int i = 0; i < oTaskAccountsValueInfoList.Count; i++)
                    {
                        AccountHdrInfo oAccountHdrInfo = new AccountHdrInfo();
                        oAccountHdrInfo.AccountID = oTaskAccountsValueInfoList[i].ReferenceID;
                        oAccountHdrInfo.IsTaskCompleted = oTaskAccountsValueInfoList[i].IsTaskCompleted;
                        oAccountHdrInfolist.Add(oAccountHdrInfo);
                    }
                    oTaskHdrInfo.TaskAccount = oAccountHdrInfolist;
                }
                // AssignedTo
                List<long> oAssignedToUserIDList = (from oTaskAttributeValueInfo in oTaskAttributeValueInfoList
                                                    where oTaskAttributeValueInfo.TaskAttributeID.Value == (short)ARTEnums.TaskAttribute.AssignedTo
                                                select oTaskAttributeValueInfo.ReferenceID.Value).ToList();
                if (oAssignedToUserIDList != null && oAssignedToUserIDList.Count > 0)
                {
                    List<UserHdrInfo> oAssignedToUserHdrInfoList = new List<UserHdrInfo>();
                    for (int i = 0; i < oAssignedToUserIDList.Count; i++)
                    {
                        UserHdrInfo oUserHdrInfo = new UserHdrInfo();
                        oUserHdrInfo.UserID = Convert.ToInt32(oAssignedToUserIDList[i]);
                        oAssignedToUserHdrInfoList.Add(oUserHdrInfo);
                    }
                    oTaskHdrInfo.AssignedTo = oAssignedToUserHdrInfoList;
                    oTaskHdrInfo.AssignedToUserName = oTaskAttributeValueInfoList[0].AssigneeUserName;
                }

                // Reviewer
                List<long> oReviewerUserIDList = (from oTaskAttributeValueInfo in oTaskAttributeValueInfoList
                                                  where oTaskAttributeValueInfo.TaskAttributeID.Value == (short)ARTEnums.TaskAttribute.Reviewer
                                                    select oTaskAttributeValueInfo.ReferenceID.Value).ToList();
                if (oReviewerUserIDList != null && oReviewerUserIDList.Count > 0)
                {
                    List<UserHdrInfo> oReviewerUserHdrInfoList = new List<UserHdrInfo>();
                    for (int i = 0; i < oReviewerUserIDList.Count; i++)
                    {
                        UserHdrInfo oUserHdrInfo = new UserHdrInfo();
                        oUserHdrInfo.UserID = Convert.ToInt32(oReviewerUserIDList[i]);
                        oReviewerUserHdrInfoList.Add(oUserHdrInfo);
                    }
                    oTaskHdrInfo.Reviewer = oReviewerUserHdrInfoList;
                    oTaskHdrInfo.ReviewerUserName = oTaskAttributeValueInfoList[0].ReviewerUserName;
                }

                // Approver
                List<long> oApproverUserIDList = (from oTaskAttributeValueInfo in oTaskAttributeValueInfoList
                                                  where oTaskAttributeValueInfo.TaskAttributeID.Value == (short)ARTEnums.TaskAttribute.Approver
                                                    select oTaskAttributeValueInfo.ReferenceID.Value).ToList();
                if (oApproverUserIDList != null && oApproverUserIDList.Count > 0)
                {
                    List<UserHdrInfo> oApproverUserHdrInfoList = new List<UserHdrInfo>();
                    for (int i = 0; i < oApproverUserIDList.Count; i++)
                    {
                        UserHdrInfo oUserHdrInfo = new UserHdrInfo();
                        oUserHdrInfo.UserID = Convert.ToInt32(oApproverUserIDList[i]);
                        oApproverUserHdrInfoList.Add(oUserHdrInfo);
                    }
                    oTaskHdrInfo.Approver = oApproverUserHdrInfoList;
                    oTaskHdrInfo.ApproverUserName = oTaskAttributeValueInfoList[0].ApproverUserName;
                }


                // Notify
                List<long> oNotifyUserIDList = (from oTaskAttributeValueInfo in oTaskAttributeValueInfoList
                                                where oTaskAttributeValueInfo.TaskAttributeID.Value == (short)ARTEnums.TaskAttribute.Notify
                                                select oTaskAttributeValueInfo.ReferenceID.Value).ToList();
                if (oNotifyUserIDList != null && oNotifyUserIDList.Count > 0)
                {
                    List<UserHdrInfo> oNotifyUserHdrInfoList = new List<UserHdrInfo>();
                    for (int i = 0; i < oNotifyUserIDList.Count; i++)
                    {
                        UserHdrInfo oUserHdrInfo = new UserHdrInfo();
                        oUserHdrInfo.UserID = Convert.ToInt32(oNotifyUserIDList[i]);
                        oNotifyUserHdrInfoList.Add(oUserHdrInfo);
                    }
                    oTaskHdrInfo.Notify = oNotifyUserHdrInfoList;
                    oTaskHdrInfo.NotifyUserName = oTaskAttributeValueInfoList[0].NotifyUserName;
                }
                // Recurrence Frequency              
                List<TaskAttributeValueInfo> oTaskRecurrenceFrequencyValueInfoList = (from objTaskAttributeValueInfo in oTaskAttributeValueInfoList
                                                                                      where objTaskAttributeValueInfo.TaskAttributeID.Value == (short)ARTEnums.TaskAttribute.RecurrenceFrequency
                                                                                      select objTaskAttributeValueInfo).ToList();
                if (oTaskRecurrenceFrequencyValueInfoList != null)
                {
                    List<ReconciliationPeriodInfo> oReconciliationPeriodInfolist = new List<ReconciliationPeriodInfo>();
                    for (int i = 0; i < oTaskRecurrenceFrequencyValueInfoList.Count; i++)
                    {
                        ReconciliationPeriodInfo oReconciliationPeriodInfo = new ReconciliationPeriodInfo();
                        oReconciliationPeriodInfo.ReconciliationPeriodID = Convert.ToInt32(oTaskRecurrenceFrequencyValueInfoList[i].ReferenceID.Value);
                        oReconciliationPeriodInfo.IsTaskCompleted = oTaskRecurrenceFrequencyValueInfoList[i].IsTaskCompleted;
                        oReconciliationPeriodInfolist.Add(oReconciliationPeriodInfo);
                    }
                    oTaskHdrInfo.RecurrenceFrequency = oReconciliationPeriodInfolist;
                }
                // RecurrencePeriodNumber              
                List<TaskAttributeValueInfo> oTaskRecurrencePeriodNumberValueInfoList = (from objTaskAttributeValueInfo in oTaskAttributeValueInfoList
                                                                                         where objTaskAttributeValueInfo.TaskAttributeID.Value == (short)ARTEnums.TaskAttribute.RecurrencePeriodNumber
                                                                                         select objTaskAttributeValueInfo).ToList();
                if (oTaskRecurrencePeriodNumberValueInfoList != null)
                {
                    List<ReconciliationPeriodInfo> oRecurrencePeriodNumberList = new List<ReconciliationPeriodInfo>();
                    for (int i = 0; i < oTaskRecurrencePeriodNumberValueInfoList.Count; i++)
                    {
                        ReconciliationPeriodInfo oReconciliationPeriodInfo = new ReconciliationPeriodInfo();
                        oReconciliationPeriodInfo.PeriodNumber = Convert.ToInt16(oTaskRecurrencePeriodNumberValueInfoList[i].ReferenceID.Value);
                        oReconciliationPeriodInfo.IsTaskCompleted = oTaskRecurrencePeriodNumberValueInfoList[i].IsTaskCompleted;
                        oRecurrencePeriodNumberList.Add(oReconciliationPeriodInfo);
                    }
                    oTaskHdrInfo.RecurrencePeriodNumberList = oRecurrencePeriodNumberList;
                }


            }
            return oTaskHdrInfo;
        }
        public List<int> GetRecurrenceFrequencyByTaskID(long? TaskID, int? recPeriodID, AppUserInfo oAppUserInfo)
        {
            List<int> oRecPeriodList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskAttributeValueDAO oTaskAttributeValueDAO = new TaskAttributeValueDAO(oAppUserInfo);
                oRecPeriodList = oTaskAttributeValueDAO.GetRecurrenceFrequencyByTaskID(TaskID, recPeriodID);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oRecPeriodList;

        }
        #endregion

        #region "Edit Task"
        public bool EditTask(List<TaskHdrInfo> oTasksHdrList, int recPeriodID, string revisedBy, DateTime dateRevised, AppUserInfo oAppUserInfo)
        {
            bool isUpdated = false;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskHdrDAO oTaskHdrDAO = new TaskHdrDAO(oAppUserInfo);
                isUpdated = oTaskHdrDAO.EditTask(oTasksHdrList, recPeriodID, revisedBy, dateRevised);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return isUpdated;
        }
        #endregion

        public void AddTasksComment(List<long> TaskDetailIDs, string SubjectLine, string Comment, int AddedByUserID, string AddedBy, DateTime DateAdded, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskDetailReviewNoteHdrDAO oTaskDetailReviewNoteHdrDAO = new TaskDetailReviewNoteHdrDAO(oAppUserInfo);
                oTaskDetailReviewNoteHdrDAO.AddTasksComment(TaskDetailIDs, SubjectLine, Comment, AddedByUserID, AddedBy, DateAdded);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
        }

        public void AddTaskDetailStatus(List<long> TaskDetailIDs, SkyStem.ART.Client.Data.ARTEnums.TaskStatus TaskStatus, DateTime TaskStatusDate, int? AddedByUserID, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskDetailStatusDAO oTaskDetailStatusDAO = new TaskDetailStatusDAO(oAppUserInfo);
                oTaskDetailStatusDAO.AddTaskDetailStatus(TaskDetailIDs, TaskStatus, TaskStatusDate, AddedByUserID);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
        }

        public void UpdateTasksStatus(List<long> TaskDetailIDs, SkyStem.ART.Client.Data.ARTEnums.TaskActionType TaskAcion, int? RecPeriodID, string RevisedByUser, DateTime DateRevised, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskDetailStatusDAO oTaskDetailStatusDAO = new TaskDetailStatusDAO(oAppUserInfo);
                oTaskDetailStatusDAO.UpdateTasksStatus(TaskDetailIDs, TaskAcion, RecPeriodID, RevisedByUser, DateRevised, oAppUserInfo.UserID, oAppUserInfo.RoleID);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
        }


        public void UpdateTaskStatusCommentAttachments(List<long> TaskDetailIDs, string AddedByUser, DateTime DateRevised, int? RecPeriodID, SkyStem.ART.Client.Data.ARTEnums.TaskActionType TaskAcion
            , string SubjectLine, string Comment, int? AddedByUserID, List<AttachmentInfo> AttachmentInfoList, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            TaskDetailStatusDAO oTaskDetailStatusDAO = null;
            TaskDetailReviewNoteHdrDAO oTaskDetailReviewNoteHdrDAO = null;
            AttachmentDAO oAttachmentDAO = null;
            DataTable dtAttachment = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                oTaskDetailStatusDAO = new TaskDetailStatusDAO(oAppUserInfo);
                oTaskDetailReviewNoteHdrDAO = new TaskDetailReviewNoteHdrDAO(oAppUserInfo);

                if (AttachmentInfoList != null && AttachmentInfoList.Count > 0)
                {
                    oAttachmentDAO = new AttachmentDAO(oAppUserInfo);

                    var attachmentList = (from a in AttachmentInfoList
                                          from t in TaskDetailIDs
                                          select new AttachmentInfo()
                                          {
                                              AttachmentID = a.AttachmentID,
                                              Comments = a.Comments,
                                              Date = a.Date,
                                              DocumentName = a.DocumentName,
                                              FileName = a.FileName
                                              ,
                                              FileSize = a.FileSize,
                                              IsActive = a.IsActive,
                                              IsPermanentOrTemporary = a.IsPermanentOrTemporary,
                                              OriginalAttachmentID = a.OriginalAttachmentID,
                                              PhysicalPath = a.PhysicalPath
                                              ,
                                              PreviousAttachmentID = a.PreviousAttachmentID,
                                              RecordID = t,
                                              RecordTypeID = a.RecordTypeID,
                                              UserFullName = a.UserFullName,
                                              UserID = a.UserID
                                          }).ToList();

                    dtAttachment = ServiceHelper.ConvertAttachmentInfoCollectionToDataTable(attachmentList);
                }
                oConnection = oTaskDetailStatusDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();

                //Update Task status
                oTaskDetailStatusDAO.UpdateTasksStatusWithTransaction(TaskDetailIDs, TaskAcion, RecPeriodID, AddedByUser, DateRevised, oAppUserInfo.UserID, oAppUserInfo.RoleID, oConnection, oTransaction);

                //Insert Tasks Comment
                oTaskDetailReviewNoteHdrDAO.AddTasksCommentWithTransaction(TaskDetailIDs, SubjectLine, Comment, AddedByUserID, AddedByUser, DateRevised, oConnection, oTransaction);

                //Insert Attachments
                if (dtAttachment != null)
                {
                    oAttachmentDAO.InsertAttachmentBulk(dtAttachment, RecPeriodID, AddedByUser, DateRevised, oConnection, oTransaction);
                }
                oTransaction.Commit();
            }
            catch (Exception ex)
            {
                if ((oTransaction != null) && (oConnection.State == ConnectionState.Open))
                {
                    oTransaction.Rollback();
                }
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            finally
            {
                try
                {
                    if ((null != oConnection) && (oConnection.State == ConnectionState.Open))
                        oConnection.Close();
                }
                catch (Exception)
                {
                }

            }
        }

        public List<TaskActionTypeTaskSatusInfo> GetTaskStatusByTaskActionTypeID(short TaskActionTypeID, AppUserInfo oAppUserInfo)
        {
            List<TaskActionTypeTaskSatusInfo> TaskActionTypeTaskSatusInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskStatusMstDAO oTaskDetailStatusDAO = new TaskStatusMstDAO(oAppUserInfo);
                TaskActionTypeTaskSatusInfoList = oTaskDetailStatusDAO.GetTaskStatusByTaskActionTypeID(TaskActionTypeID);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return TaskActionTypeTaskSatusInfoList;
        }

        public List<TaskHdrInfo> GetApproverAccessableTaskByUserID(int userID, short roleID, int recPeriodID, ARTEnums.TaskType taskType
            , List<long> AccountIDs, short? taskCategoryID, bool? isShowHidden, int? LanguageID, int? DefaultLanguageID, AppUserInfo oAppUserInfo)
        {
            List<TaskHdrInfo> oTaskHdrInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskHdrDAO oTaskHdrDAO = new TaskHdrDAO(oAppUserInfo);
                oTaskHdrInfoList = oTaskHdrDAO.GetApproverAccessableTaskByUserID(userID, roleID, recPeriodID, taskType, AccountIDs, taskCategoryID,
                    isShowHidden, LanguageID, DefaultLanguageID);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oTaskHdrInfoList;
        }

        public List<TaskHdrInfo> GetAccessibleTasksByActionTypeID(int userID, short roleID, int recPeriodID, ARTEnums.TaskType taskType,
            ARTEnums.TaskActionType taskActionType, List<long> AccountIDs, short? taskCategoryID, bool? isShowHidden, int? LanguageID, int? DefaultLanguageID, AppUserInfo oAppUserInfo)
        {
            List<TaskHdrInfo> oTaskHdrInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskHdrDAO oTaskHdrDAO = new TaskHdrDAO(oAppUserInfo);
                oTaskHdrInfoList = oTaskHdrDAO.GetAccessibleTasksByActionTypeID(userID, roleID, recPeriodID, taskType, taskActionType, AccountIDs, taskCategoryID,
                    isShowHidden, LanguageID, DefaultLanguageID);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oTaskHdrInfoList;
        }

        public List<TaskHdrInfo> GetAssingneeAccessableTaskByUserID(int userID, short roleID, int recPeriodID, ARTEnums.TaskType taskType
            , List<long> AccountIDs, short? taskCategoryID, bool? isShowHidden, int? LanguageID, int? DefaultLanguageID, AppUserInfo oAppUserInfo)
        {
            List<TaskHdrInfo> oTaskHdrInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskHdrDAO oTaskHdrDAO = new TaskHdrDAO(oAppUserInfo);
                oTaskHdrInfoList = oTaskHdrDAO.GetAssingneeAccessableTaskByUserID(userID, roleID, recPeriodID, taskType, AccountIDs, taskCategoryID,
                    isShowHidden, LanguageID, DefaultLanguageID);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oTaskHdrInfoList;
        }

        public List<TaskHdrInfo> GetDeleteAccessableTaskByUserID(int userID, short roleID, int recPeriodID, ARTEnums.TaskType taskType
            , short? taskCategoryID, bool? isShowHidden, int? LanguageID, int? DefaultLanguageID, AppUserInfo oAppUserInfo)
        {
            List<TaskHdrInfo> oTaskHdrInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskHdrDAO oTaskHdrDAO = new TaskHdrDAO(oAppUserInfo);
                oTaskHdrInfoList = oTaskHdrDAO.GetDeleteAccessableTaskByUserID(userID, roleID, recPeriodID, taskType, taskCategoryID, isShowHidden, LanguageID, DefaultLanguageID);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oTaskHdrInfoList;
        }

        public List<TaskHdrInfo> GetAccessableTaskForBulkEdit(int userID, short roleID, int recPeriodID, ARTEnums.TaskType taskType
         , short? taskCategoryID, bool? isShowHidden, int? LanguageID, int? DefaultLanguageID, AppUserInfo oAppUserInfo)
        {
            List<TaskHdrInfo> oTaskHdrInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskHdrDAO oTaskHdrDAO = new TaskHdrDAO(oAppUserInfo);
                oTaskHdrInfoList = oTaskHdrDAO.GetAccessableTaskForBulkEdit(userID, roleID, recPeriodID, taskType, taskCategoryID, isShowHidden, LanguageID, DefaultLanguageID);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oTaskHdrInfoList;
        }

        public void DeleteAccessibleTasks(List<long> TaskIDs, int RecPeriodID, string ModifiedBy, DateTime DateModified, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskHdrDAO oTaskHdrDAO = new TaskHdrDAO(oAppUserInfo);
                oTaskHdrDAO.DeleteAccessibleTasks(TaskIDs, RecPeriodID, ModifiedBy, DateModified);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
        }

        public List<TaskHdrInfo> GetAccessableTaskByAccountIDs(int userID, short roleID, int recPeriodID, List<long> AccountIDs, short? taskCategoryID,
            bool? isShowHidden, int? LanguageID, int? DefaultLanguageID, AppUserInfo oAppUserInfo)
        {
            List<TaskHdrInfo> oTaskHdrInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskHdrDAO oTaskHdrDAO = new TaskHdrDAO(oAppUserInfo);
                oTaskHdrInfoList = oTaskHdrDAO.GetAccessableTaskByAccountIDs(userID, roleID, recPeriodID, AccountIDs, taskCategoryID, isShowHidden, LanguageID, DefaultLanguageID);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oTaskHdrInfoList;
        }

        public List<TaskCategoryMstInfo> GetTaskCategory(AppUserInfo oAppUserInfo)
        {
            List<TaskCategoryMstInfo> oTaskCategoryMstInfoList = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskCategoryMstDAO oTaskCategoryMstDAO = new TaskCategoryMstDAO(oAppUserInfo);
                oTaskCategoryMstInfoList = oTaskCategoryMstDAO.GetTaskCategoryMstInfoCollection();
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oTaskCategoryMstInfoList;
        }

        public List<TaskStatusMstInfo> GetTaskStatus(AppUserInfo oAppUserInfo)
        {
            List<TaskStatusMstInfo> oTaskStatusMstInfoList = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskStatusMstDAO oTaskStatusMstDAO = new TaskStatusMstDAO(oAppUserInfo);
                oTaskStatusMstInfoList = oTaskStatusMstDAO.GetTaskStatus();
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oTaskStatusMstInfoList;
        }


        #region "AddEditUserTaskVisibility"
        public void AddEditUserTaskVisibility(List<TaskHdrInfo> oTasksHdrList, int? CurrentUserID, int? recPeriodID, string addedBy, string revisedBy, bool isHidden, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskHdrDAO oTaskHdrDAO = new TaskHdrDAO(oAppUserInfo);
                oTaskHdrDAO.AddEditUserTaskVisibility(oTasksHdrList, CurrentUserID, recPeriodID, addedBy, revisedBy, isHidden);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
        }
        #endregion

        public List<TaskAttributeValueInfo> GetTaskAttributeList(int recPeriodID, List<long> taskIDs, List<short> attributeIDs, AppUserInfo oAppUserInfo)
        {
            List<TaskAttributeValueInfo> oTaskHdrInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskAttributeValueDAO oTaskAttributeValueDAO = new TaskAttributeValueDAO(oAppUserInfo);
                oTaskHdrInfoList = oTaskAttributeValueDAO.GetTaskAttributeList(recPeriodID, taskIDs, attributeIDs);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oTaskHdrInfoList;
        }

        public bool BulkEditTask(List<TaskHdrInfo> oTasksHdrList, int recPeriodID, string revisedBy, DateTime dateRevised, AppUserInfo oAppUserInfo)
        {
            bool isUpdated = false;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskHdrDAO oTaskHdrDAO = new TaskHdrDAO(oAppUserInfo);
                isUpdated = oTaskHdrDAO.BulkEditTask(oTasksHdrList, recPeriodID, revisedBy, dateRevised);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return isUpdated;
        }
        #region "Edit Task List"
        public bool EditTaskList(int? TasksListID, string TasksListName, string revisedBy, DateTime dateRevised, AppUserInfo oAppUserInfo)
        {
            bool isUpdated = false;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskListHdrDAO oTaskListHdrDAO = new TaskListHdrDAO(oAppUserInfo);
                isUpdated = oTaskListHdrDAO.EditTaskList(TasksListID, TasksListName, revisedBy, dateRevised);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return isUpdated;
        }
        #endregion
        #region "Edit Task SubList"
        public bool EditTaskSubList(int? TasksSubListID, string TasksSubListName, string revisedBy, DateTime dateRevised, AppUserInfo oAppUserInfo)
        {
            bool isUpdated = false;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskListHdrDAO oTaskListHdrDAO = new TaskListHdrDAO(oAppUserInfo);
                isUpdated = oTaskListHdrDAO.EditTaskSubList(TasksSubListID, TasksSubListName, revisedBy, dateRevised);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return isUpdated;
        }
        public int GetTaskSubListIDByName(string TaskSubListName, int? CompanyID, AppUserInfo oAppUserInfo)
        {
            int TaskSubListID = 0;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskListHdrDAO oTaskListHdrDAO = new TaskListHdrDAO(oAppUserInfo);
                TaskSubListID = oTaskListHdrDAO.GetTaskSubListIDByName(TaskSubListName,CompanyID);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return TaskSubListID;
        }
        #endregion

        #region "Delete TaskLoad"
        public List<DataImportHdrInfo> DeleteTaskLoad(int DataImportID, string revisedBy, DateTime dateRevised, AppUserInfo oAppUserInfo)
        {
            List<DataImportHdrInfo> oDataImportHdrInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskHdrDAO oTaskHdrDAO = new TaskHdrDAO(oAppUserInfo);
                oDataImportHdrInfoList = oTaskHdrDAO.DeleteTaskLoad(DataImportID, revisedBy, dateRevised);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oDataImportHdrInfoList;
        }
        #endregion
        #region "Delete Tasks Permanently"
        public List<DataImportHdrInfo> DeleteTasks(List<long> SelectedTaskIDs, int CompanyID, string revisedBy, DateTime dateRevised, AppUserInfo oAppUserInfo)
        {
            List<DataImportHdrInfo> oDataImportHdrInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskHdrDAO oTaskHdrDAO = new TaskHdrDAO(oAppUserInfo);
                oDataImportHdrInfoList = oTaskHdrDAO.DeleteTasks(SelectedTaskIDs, CompanyID, revisedBy, dateRevised);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oDataImportHdrInfoList;
        }
        #endregion

        public List<TaskHdrInfo> GetTasksByDataImportID(int DataImportID, AppUserInfo oAppUserInfo)
        {
            List<TaskHdrInfo> oTaskHdrInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskHdrDAO oTaskHdrDAO = new TaskHdrDAO(oAppUserInfo);
                oTaskHdrInfoList = oTaskHdrDAO.GetTasksByDataImportID(DataImportID);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oTaskHdrInfoList;
        }

        public List<TaskTypeMstInfo> GetAllTaskType(AppUserInfo oAppUserInfo)
        {
            List<TaskTypeMstInfo> oTaskTypeMstInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskTypeMstDAO oTaskTypeMstDAO = new TaskTypeMstDAO(oAppUserInfo);
                oTaskTypeMstInfoList = oTaskTypeMstDAO.GetAllTaskType();
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oTaskTypeMstInfoList;
        }
        public List<TaskHdrInfo> GetTaskInformationForCompanyAlertMail(int RecPeriodID, int UserID, short roleID, long CompanyAlertDetailID, int LanguageID, AppUserInfo oAppUserInfo)
        {
            List<TaskHdrInfo> oTaskHdrInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskHdrDAO oTaskHdrDAO = new TaskHdrDAO(oAppUserInfo);
                oTaskHdrInfoList = oTaskHdrDAO.GetTaskInformationForCompanyAlertMail(RecPeriodID, UserID, roleID, CompanyAlertDetailID, LanguageID);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oTaskHdrInfoList;

        }
        public List<TaskCustomFieldInfo> GetTaskCustomFields(int? RecPeriodID, int? CompanyID, AppUserInfo oAppUserInfo)
        {
            List<TaskCustomFieldInfo> oTaskCustomFieldInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskHdrDAO oTaskHdrDAO = new TaskHdrDAO(oAppUserInfo);
                oTaskCustomFieldInfoList = oTaskHdrDAO.GetTaskCustomFields(RecPeriodID, CompanyID);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oTaskCustomFieldInfoList;
        }


        public void SaveTaskCustomFields(List<TaskCustomFieldInfo> oTaskCustomFieldInfoList, int? CompanyID, int? RecPeriodID, string ModifiedBy, DateTime? DateModified, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskHdrDAO oTaskHdrDAO = new TaskHdrDAO(oAppUserInfo);
                oConnection = oTaskHdrDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();
                DataTable dtTaskCustomField = ServiceHelper.ConvertTaskCustomFieldInfoCollectionToDataTable(oTaskCustomFieldInfoList);
                oTaskHdrDAO.SaveTaskCustomFields(dtTaskCustomField, CompanyID, RecPeriodID, ModifiedBy, DateModified, oConnection, oTransaction);

                oTransaction.Commit();
            }
            catch (SqlException ex)
            {
                if (oTransaction != null && oTransaction.Connection != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                if (oTransaction != null && oTransaction.Connection != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            finally
            {
                if ((oConnection != null) && (oConnection.State == ConnectionState.Open))
                {
                    oConnection.Close();
                    oConnection.Dispose();
                }
            }
        }
    }
}
