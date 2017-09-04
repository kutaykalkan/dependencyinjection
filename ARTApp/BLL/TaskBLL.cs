using SkyStem.ART.App.DAO;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace SkyStem.ART.App.BLL
{
    public class TaskBLL
    {
        private TaskBLL()
        {

        }

        public static TaskHdrInfo GetTaskHdrInfoByTaskID(long? TaskID, int? recPeriodID, AppUserInfo oAppUserInfo)
        {
            TaskHdrInfo oTaskHdrInfo = null;
            List<TaskAttributeValueInfo> oTaskAttributeValueInfoList = null;
            ServiceHelper.SetConnectionString(oAppUserInfo);
            TaskAttributeValueDAO oTaskAttributeValueDAO = new TaskAttributeValueDAO(oAppUserInfo);
            oTaskAttributeValueInfoList = oTaskAttributeValueDAO.GetTaskAttributeListByTaskID(TaskID, recPeriodID);
            oTaskHdrInfo = GetTaskHdrInfoFromAttributes(oTaskAttributeValueInfoList, oAppUserInfo);
            return oTaskHdrInfo;
        }

        private static TaskHdrInfo GetTaskHdrInfoFromAttributes(List<TaskAttributeValueInfo> oTaskAttributeValueInfoList, AppUserInfo oAppUserInfo)
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
                        oTaskHdrInfo.DaysTypeID = Convert.ToInt16(oTaskAttributeValueInfo.ReferenceID);
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


        public static bool CheckTaskPermissions(long? taskID, AppUserInfo oAppUserInfo)
        {
            // System Admin have access to all
            if (oAppUserInfo.RoleID == (short)ARTEnums.UserRole.SYSTEM_ADMIN)
                return true;
            // User will not be able to get detail if GL is not accessible
            TaskHdrInfo oTaskHdr = GetTaskHdrInfoByTaskID(taskID, oAppUserInfo.RecPeriodID, oAppUserInfo);
            if (oTaskHdr != null && !string.IsNullOrEmpty(oTaskHdr.TaskNumber))
            {
                UserHdrInfo oUserHdrInfo = null;
                if (oTaskHdr.AssignedTo != null && oTaskHdr.AssignedTo.Count > 0)
                {
                    oUserHdrInfo = oTaskHdr.AssignedTo.Find(T => T.UserID == oAppUserInfo.UserID);
                    if (oUserHdrInfo != null && oUserHdrInfo.UserID.GetValueOrDefault() > 0)
                        return true;
                }
                if (oTaskHdr.Reviewer != null && oTaskHdr.Reviewer.Count > 0)
                {
                    oUserHdrInfo = oTaskHdr.Reviewer.Find(T => T.UserID == oAppUserInfo.UserID);
                    if (oUserHdrInfo != null && oUserHdrInfo.UserID.GetValueOrDefault() > 0)
                        return true;
                }
                if (oTaskHdr.Approver != null && oTaskHdr.Approver.Count > 0)
                {
                    oUserHdrInfo = oTaskHdr.Approver.Find(T => T.UserID == oAppUserInfo.UserID);
                    if (oUserHdrInfo != null && oUserHdrInfo.UserID.GetValueOrDefault() > 0)
                        return true;
                }
                if (oTaskHdr.AssignedTo != null && oTaskHdr.AssignedTo.Count > 0)
                {
                    oUserHdrInfo = oTaskHdr.Notify.Find(T => T.UserID == oAppUserInfo.UserID);
                    if (oUserHdrInfo != null && oUserHdrInfo.UserID.GetValueOrDefault() > 0)
                        return true;
                }
            }
            return false;
        }
    }
}