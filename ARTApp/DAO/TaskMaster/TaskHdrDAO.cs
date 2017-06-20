using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.Client.Data;
using SkyStem.ART.App.Utility;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace SkyStem.ART.App.DAO
{
    public class TaskHdrDAO : TaskHdrDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TaskHdrDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        public static readonly string COLUMN_TASKLDETAIL_ID = "TaskDetailID";
        public static readonly string COLUMN_TASK_DETAIL_RECPERIODID = "TaskDetailRecPeriodID";
        public static readonly string COLUMN_TASK_APPROVER_LAST_COMMENT = "TaskApproverLastComment";
        public static readonly string COLUMN_TASK_COMPLETION_COMMENT = "TaskCompletionComment";
        public static readonly string COLUMN_TASK_APPROVAL_DURATION = "ApprovalDuration";
        public static readonly string COLUMN_TASK_STATUS_ID = "TaskStatusID";
        public static readonly string COLUMN_TASK_STATUS_LABEL_ID = "TaskStatusLabelID";
        public static readonly string COLUMN_TASK_STATUS_DATE = "TaskStatusDate";
        public static readonly string COLUMN_TASK_COMPLETION_DATE = "TaskCompletionDate";
        public static readonly string COLUMN_COMMENT = "Comment";
        public static readonly string COLUMN_ADDED_BY_USER = "AddedByUser";
        public static readonly string COLUMN_TASK_DETAIL_REVISED_BY_USER = "TaskDetaiRevisedByUser";
        public static readonly string COLUMN_TASK_DETAIL_DATE_REVISED = "TaskDetailDateRevised";

        //Task Attributes
        public static readonly string COLUMN_TASKNAME = "TaskName";//Task Name
        public static readonly string COLUMN_TASK_LIST = "TaskList";//TaskListID
        public static readonly string COLUMN_TASK_DESCRIPTION = "TaskDescription";//Description
        public static readonly string COLUMN_ASSIGNEE = "Assignee";//Assigned To
        public static readonly string COLUMN_APPROVER = "Approver";//Approver
        public static readonly string COLUMN_NOTIFY = "Notify";//Notify
        public static readonly string COLUMN_CREATION_ATTACHMENT = "CreationAttachment"; //Attached Documents
        public static readonly string COLUMN_TASK_RECURRENCE_TYPE = "TaskRecurrenceType";//Recurrence Type
        public static readonly string COLUMN_RECURRENCE_FREQUENCY = "RecurrenceFrequency";//Recurrence Frequency
        public static readonly string COLUMN_TASK_START_DATE = "TaskStartDate";//Task Start Date
        public static readonly string COLUMN_TASK_DUE_DATE = "TaskDueDate";//Task Due Date
        public static readonly string COLUMN_ASSIGNEE_DUE_DATE = "AssigneeDueDate";//Assignee Due Date
        public static readonly string COLUMN_REVIEWER_DUE_DATE = "ReviewerDueDate";//Reviewer Due Date
        public static readonly string COLUMN_ASSIGNEE_DUE_DAYS = "AssigneeDueDays";//Assignee Due Days
        public static readonly string COLUMN_REVIEWER_DUE_DAYS = "ReviewerDueDays";//Reviewer Due Days
        public static readonly string COLUMN_TASK_ACCOUNT = "TaskAccount";//Task Account
        public static readonly string COLUMN_TASK_DUE_DAYS = "TaskDueDays";//Task Due Days
        public static readonly string COLUMN_TASK_ISTASKHIDDEN = "IsHidden";
        public static readonly string COLUMN_TASK_ISWORKSTARTED = "IsWorkStarted";

        public DataTable AddTask(List<TaskHdrInfo> oTasksHdrList, int recPeriodID, string addedBy, DateTime dateAdded, List<AttachmentInfo> oAttachmentInfoCollection)
        {
            IDbCommand oCmd = null;
            IDbConnection oConn = null;
            IDbTransaction oTrans = null;
            DataTable dtTaskHdr = null;
            DataTable dtTaskAttribute = null;
            DataTable dtAttachment = null;
            DataTable dtTaskNumber = null;
            try
            {
                dtTaskHdr = ServiceHelper.ConvertTaskHdrToDataTable(oTasksHdrList);
                dtTaskAttribute = ServiceHelper.ConvertTaskAttributeToDataTable(oTasksHdrList);
                if (oAttachmentInfoCollection != null && oAttachmentInfoCollection.Count > 0)
                {
                    //Array.ForEach(oAttachmentInfoCollection.ToArray(), a => a.RecordID = oGLRecItemInput.GLDataRecItemID);
                    dtAttachment = ServiceHelper.ConvertAttachmentInfoCollectionToDataTable(oAttachmentInfoCollection);

                }


                oConn = this.CreateConnection();
                oCmd = this.GetAddTaskCommand(recPeriodID, dtTaskHdr, dtTaskAttribute, dtAttachment, addedBy, dateAdded);
                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                oTrans = oConn.BeginTransaction();
                oCmd.Transaction = oTrans;

                //Get TaskNumber of inserted
                dtTaskNumber = new DataTable();
                dtTaskNumber.Load(oCmd.ExecuteReader());
                //oCmd.ExecuteNonQuery();

                oTrans.Commit();
                oTrans.Dispose();
            }
            catch (Exception ex)
            {
                if (oTrans != null && oConn != null)
                {
                    oTrans.Rollback();
                    oTrans.Dispose();
                }
                throw ex;
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
            return dtTaskNumber;
        }

        public List<TaskHdrInfo> GetAccessableTaskByUserID(int userID, short roleID, int recPeriodID, short taskCategoryID, ARTEnums.TaskType taskType
            , List<TaskStatusMstInfo> taskStatusMstInfoList, List<FilterCriteria> filterCriteriaList
            , bool? isShowHidden, int? LanguageID, int? DefaultLanguageID)
        {
            IDbCommand oCmd = null;
            IDbConnection oConn = null;
            IDataReader reader = null;
            List<TaskHdrInfo> oTaskHdrInfoList = new List<TaskHdrInfo>();
            try
            {
                DataTable dtFilterCriteria = ServiceHelper.ConvertFilterCriteriaIntoDataTable(filterCriteriaList);
                DataTable dtTaskStatus = ServiceHelper.ConvertTaskStatusMstInfoListToDataTable(taskStatusMstInfoList);

                oConn = this.CreateConnection();
                oCmd = this.GetAccessableTaskByUserIDCommand(userID, roleID, recPeriodID, taskCategoryID, taskType, dtFilterCriteria, dtTaskStatus, isShowHidden, LanguageID, DefaultLanguageID);
                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                reader = oCmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    TaskHdrInfo oTaskListinfo = this.MapObject(reader);
                    oTaskHdrInfoList.Add(oTaskListinfo);
                }
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
            return oTaskHdrInfoList;
        }


        public List<TaskHdrInfo> GetPendingOverdueAccessableTaskByUserID(int userID, short roleID, int recPeriodID, ARTEnums.TaskType taskType,
            List<FilterCriteria> filterCriteriaList, short? taskCategoryID, bool? isShowHidden, int? LanguageID, int? DefaultLanguageID, short? TaskCompletionStatusID, bool? isShowAllTask)
        {
            IDbCommand oCmd = null;
            IDbConnection oConn = null;
            IDataReader reader = null;
            List<TaskHdrInfo> oTaskHdrInfoList = new List<TaskHdrInfo>();
            try
            {
                DataTable dtFilterCriteria = ServiceHelper.ConvertFilterCriteriaIntoDataTable(filterCriteriaList);

                oConn = this.CreateConnection();
                oCmd = this.GetPendingOverdueAccessableTaskByUserIDCommand(userID, roleID, recPeriodID, taskType, dtFilterCriteria, taskCategoryID,
                    isShowHidden, LanguageID, DefaultLanguageID, TaskCompletionStatusID, isShowAllTask);
                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                reader = oCmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    TaskHdrInfo oTaskListinfo = this.MapObject(reader);
                    if (!string.IsNullOrEmpty(oTaskListinfo.TaskName))
                        oTaskHdrInfoList.Add(oTaskListinfo);
                }
                reader.ClearColumnHash();
                // Net Resultset for TaskOwners
                reader.NextResult();
                while (reader.Read())
                {
                    AddTaskOwners(oTaskHdrInfoList, reader);
                }
                reader.ClearColumnHash();
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
            return oTaskHdrInfoList;
        }
        public void AddTaskOwners(List<TaskHdrInfo> oTaskHdrInfoList, IDataReader reader)
        {
            long? TaskID = reader.GetInt64Value(COLUMN_TASKID);
            long? TaskDetailID = reader.GetInt64Value("TaskDetailID");
            short? TaskAttributeID = reader.GetInt16Value("TaskAttributeID");
            UserHdrInfo oUserHdrInfo = new UserHdrInfo();
            oUserHdrInfo.UserID = reader.GetInt32Value("UserID");
            oUserHdrInfo.FirstName = reader.GetStringValue("FirstName");
            oUserHdrInfo.LastName = reader.GetStringValue("LastName");
            oUserHdrInfo.EmailID = reader.GetStringValue("EmailID");
            oUserHdrInfo.LoginID = reader.GetStringValue("LoginID");
            if (TaskID.HasValue && oTaskHdrInfoList.Count > 0)
            {
                TaskHdrInfo oTaskHdrInfo = oTaskHdrInfoList.Find(o => o.TaskID.Value == TaskID.Value && o.TaskDetailID.GetValueOrDefault() == TaskDetailID.GetValueOrDefault());
                if (oTaskHdrInfo != null)
                {
                    if (TaskAttributeID.HasValue && TaskAttributeID.Value == (short)ARTEnums.TaskAttribute.AssignedTo)
                    {
                        if (oTaskHdrInfo.AssignedTo != null)
                            oTaskHdrInfo.AssignedTo.Add(oUserHdrInfo);
                        else
                        {
                            oTaskHdrInfo.AssignedTo = new List<UserHdrInfo>();
                            oTaskHdrInfo.AssignedTo.Add(oUserHdrInfo);
                        }
                    }
                    if (oTaskHdrInfo != null && TaskAttributeID.HasValue && TaskAttributeID.Value == (short)ARTEnums.TaskAttribute.Reviewer)
                    {
                        if (oTaskHdrInfo.Reviewer != null)
                            oTaskHdrInfo.Reviewer.Add(oUserHdrInfo);
                        else
                        {
                            oTaskHdrInfo.Reviewer = new List<UserHdrInfo>();
                            oTaskHdrInfo.Reviewer.Add(oUserHdrInfo);
                        }
                    }
                    if (oTaskHdrInfo != null && TaskAttributeID.HasValue && TaskAttributeID.Value == (short)ARTEnums.TaskAttribute.Approver)
                    {
                        if (oTaskHdrInfo.Approver != null)
                            oTaskHdrInfo.Approver.Add(oUserHdrInfo);
                        else
                        {
                            oTaskHdrInfo.Approver = new List<UserHdrInfo>();
                            oTaskHdrInfo.Approver.Add(oUserHdrInfo);
                        }
                    }
                }
            }
        }

        public List<TaskHdrInfo> GetCompletedAccessableTaskByUserID(int userID, short roleID, int recPeriodID, ARTEnums.TaskType taskType,
            List<FilterCriteria> filterCriteriaList, short? taskCategoryID, bool? isShowHidden, int? LanguageID, int? DefaultLanguageID)
        {
            IDbCommand oCmd = null;
            IDbConnection oConn = null;
            IDataReader reader = null;
            List<TaskHdrInfo> oTaskHdrInfoList = new List<TaskHdrInfo>();
            try
            {
                DataTable dtFilterCriteria = ServiceHelper.ConvertFilterCriteriaIntoDataTable(filterCriteriaList);

                oConn = this.CreateConnection();
                oCmd = this.GetCompletedAccessableTaskByUserIDCommand(userID, roleID, recPeriodID, taskType, dtFilterCriteria, taskCategoryID,
                    isShowHidden, LanguageID, DefaultLanguageID);
                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                reader = oCmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    TaskHdrInfo oTaskListinfo = this.MapObject(reader);
                    if (!string.IsNullOrEmpty(oTaskListinfo.TaskName))
                        oTaskHdrInfoList.Add(oTaskListinfo);
                }
                reader.ClearColumnHash();
                // Net Resultset for TaskOwners
                reader.NextResult();
                while (reader.Read())
                {
                    AddTaskOwners(oTaskHdrInfoList, reader);
                }
                reader.ClearColumnHash();
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
            return oTaskHdrInfoList;
        }


        public void AddEditUserTaskVisibility(List<TaskHdrInfo> oTasksHdrList, int? CurrentUserID, int? recPeriodID, string addedBy, string revisedBy, bool isHidden)
        {
            IDbCommand oCmd = null;
            IDbConnection oConn = null;
            IDbTransaction oTrans = null;
            DataTable dtUserTaskVisibility = null;
            try
            {
                dtUserTaskVisibility = ServiceHelper.ConvertTaskUserVisibilityToDataTable(oTasksHdrList, CurrentUserID);
                oConn = this.CreateConnection();
                oCmd = this.GetAddEditUserTaskVisibilityCommand(recPeriodID, dtUserTaskVisibility, addedBy, revisedBy, isHidden);
                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                oTrans = oConn.BeginTransaction();
                oCmd.Transaction = oTrans;
                oCmd.ExecuteNonQuery();
                oTrans.Commit();
                oTrans.Dispose();
            }
            catch (Exception ex)
            {
                if (oTrans != null && oConn != null)
                {
                    oTrans.Rollback();
                    oTrans.Dispose();
                }
                throw ex;
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
        }


        private IDbCommand GetAccessableTaskByUserIDCommand(int userID, short roleID, int recPeriodID, short taskCategoryID, ARTEnums.TaskType taskType
            , DataTable dtFilterCriteria, DataTable dtTaskStatus, bool? isShowHidden, int? LanguageID, int? DefaultLanguageID)
        {
            IDbCommand oCommand = this.CreateCommand("[TaskMaster].[usp_GET_AccessableTaskByUserID]");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;

            IDbDataParameter paramUserID = oCommand.CreateParameter();
            paramUserID.ParameterName = "@UserID";
            paramUserID.Value = userID;
            cmdParams.Add(paramUserID);

            IDbDataParameter paramRoleID = oCommand.CreateParameter();
            paramRoleID.ParameterName = "@RoleID";
            paramRoleID.Value = roleID;
            cmdParams.Add(paramRoleID);

            IDbDataParameter paramRecPeriodID = oCommand.CreateParameter();
            paramRecPeriodID.ParameterName = "@RecPeriodID";
            paramRecPeriodID.Value = recPeriodID;
            cmdParams.Add(paramRecPeriodID);

            IDbDataParameter paramTaskTypeID = oCommand.CreateParameter();
            paramTaskTypeID.ParameterName = "@TaskTypeID";
            paramTaskTypeID.Value = Convert.ToInt16(taskType);
            cmdParams.Add(paramTaskTypeID);

            IDbDataParameter paramTaskCategoryID = oCommand.CreateParameter();
            paramTaskCategoryID.ParameterName = "@TaskCategoryID";
            paramTaskCategoryID.Value = taskCategoryID;
            cmdParams.Add(paramTaskCategoryID);

            IDbDataParameter paramFilterCriteria = oCommand.CreateParameter();
            paramFilterCriteria.ParameterName = "@FilterCriteriaTable";
            paramFilterCriteria.Value = dtFilterCriteria;
            cmdParams.Add(paramFilterCriteria);

            //IDbDataParameter paramTaskStatusTable = oCommand.CreateParameter();
            //paramTaskStatusTable.ParameterName = "@TaskStatusTable";
            //paramTaskStatusTable.Value = dtTaskStatus;
            //cmdParams.Add(paramTaskStatusTable);

            IDbDataParameter paramIsShowHidden = oCommand.CreateParameter();
            paramIsShowHidden.ParameterName = "@IsShowHidden";
            paramIsShowHidden.Value = isShowHidden.Value;
            cmdParams.Add(paramIsShowHidden);

            IDbDataParameter paramLanguageID = oCommand.CreateParameter();
            paramLanguageID.ParameterName = "@LCID";
            if (LanguageID.HasValue)
                paramLanguageID.Value = LanguageID.Value;
            else
                paramLanguageID.Value = System.DBNull.Value;
            cmdParams.Add(paramLanguageID);

            IDbDataParameter paramDefualtLanguageID = oCommand.CreateParameter();
            paramDefualtLanguageID.ParameterName = "@DefaultLCID";
            if (DefaultLanguageID.HasValue)
                paramDefualtLanguageID.Value = LanguageID.Value;
            else
                paramDefualtLanguageID.Value = System.DBNull.Value;
            cmdParams.Add(paramDefualtLanguageID);

            return oCommand;
        }

        private IDbCommand GetPendingOverdueAccessableTaskByUserIDCommand(int userID, short roleID, int recPeriodID, ARTEnums.TaskType taskType,
            DataTable dtFilterCriteria, short? taskCategoryID, bool? isShowHidden, int? LanguageID, int? DefaultLanguageID, short? TaskCompletionStatusID, bool? isShowAllTask)
        {
            IDbCommand oCommand = this.CreateCommand("[TaskMaster].[usp_GET_PendingOverdueAccessableTaskByUserID]");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;

            IDbDataParameter paramUserID = oCommand.CreateParameter();
            paramUserID.ParameterName = "@UserID";
            paramUserID.Value = userID;

            IDbDataParameter paramRoleID = oCommand.CreateParameter();
            paramRoleID.ParameterName = "@RoleID";
            paramRoleID.Value = roleID;

            IDbDataParameter paramRecPeriodID = oCommand.CreateParameter();
            paramRecPeriodID.ParameterName = "@RecPeriodID";
            paramRecPeriodID.Value = recPeriodID;

            IDbDataParameter paramTaskTypeID = oCommand.CreateParameter();
            paramTaskTypeID.ParameterName = "@TaskTypeID";
            paramTaskTypeID.Value = Convert.ToInt16(taskType);

            IDbDataParameter paramFilterCriteria = oCommand.CreateParameter();
            paramFilterCriteria.ParameterName = "@FilterCriteriaTable";
            paramFilterCriteria.Value = dtFilterCriteria;

            IDbDataParameter paramTaskCategoryID = oCommand.CreateParameter();
            paramTaskCategoryID.ParameterName = "@TaskCategoryID";
            if (taskCategoryID.HasValue)
                paramTaskCategoryID.Value = taskCategoryID.Value;
            else
                paramTaskCategoryID.Value = System.DBNull.Value;

            IDbDataParameter paramIsShowHidden = oCommand.CreateParameter();
            paramIsShowHidden.ParameterName = "@IsShowHidden";
            paramIsShowHidden.Value = isShowHidden.Value;

            IDbDataParameter paramLanguageID = oCommand.CreateParameter();
            paramLanguageID.ParameterName = "@LCID";
            if (LanguageID.HasValue)
                paramLanguageID.Value = LanguageID.Value;
            else
                paramLanguageID.Value = System.DBNull.Value;

            IDbDataParameter paramDefualtLanguageID = oCommand.CreateParameter();
            paramDefualtLanguageID.ParameterName = "@DefaultLCID";
            if (DefaultLanguageID.HasValue)
                paramDefualtLanguageID.Value = LanguageID.Value;
            else
                paramDefualtLanguageID.Value = System.DBNull.Value;

            IDbDataParameter paramTaskCompletionStatusID = oCommand.CreateParameter();
            paramTaskCompletionStatusID.ParameterName = "@TaskCompletionStatusID";
            if (TaskCompletionStatusID.HasValue)
                paramTaskCompletionStatusID.Value = TaskCompletionStatusID.Value;
            else
                paramTaskCompletionStatusID.Value = System.DBNull.Value;

            IDbDataParameter paramIsShowAllTask = oCommand.CreateParameter();
            paramIsShowAllTask.ParameterName = "@IsShowAllTask";
            paramIsShowAllTask.Value = isShowAllTask.Value;

            cmdParams.Add(paramUserID);
            cmdParams.Add(paramRoleID);
            cmdParams.Add(paramRecPeriodID);
            cmdParams.Add(paramTaskTypeID);
            cmdParams.Add(paramFilterCriteria);
            cmdParams.Add(paramTaskCategoryID);
            cmdParams.Add(paramIsShowHidden);
            cmdParams.Add(paramLanguageID);
            cmdParams.Add(paramDefualtLanguageID);
            cmdParams.Add(paramTaskCompletionStatusID);
            cmdParams.Add(paramIsShowAllTask);

            return oCommand;
        }

        private IDbCommand GetCompletedAccessableTaskByUserIDCommand(int userID, short roleID, int recPeriodID, ARTEnums.TaskType taskType,
            DataTable dtFilterCriteria, short? taskCategoryID, bool? isShowHidden, int? LanguageID, int? DefaultLanguageID)
        {
            IDbCommand oCommand = this.CreateCommand("[TaskMaster].[usp_GET_CompletedAccessableTaskByUserID]");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;

            IDbDataParameter paramUserID = oCommand.CreateParameter();
            paramUserID.ParameterName = "@UserID";
            paramUserID.Value = userID;

            IDbDataParameter paramRoleID = oCommand.CreateParameter();
            paramRoleID.ParameterName = "@RoleID";
            paramRoleID.Value = roleID;

            IDbDataParameter paramRecPeriodID = oCommand.CreateParameter();
            paramRecPeriodID.ParameterName = "@RecPeriodID";
            paramRecPeriodID.Value = recPeriodID;

            IDbDataParameter paramTaskTypeID = oCommand.CreateParameter();
            paramTaskTypeID.ParameterName = "@TaskTypeID";
            paramTaskTypeID.Value = Convert.ToInt16(taskType);

            IDbDataParameter paramFilterCriteria = oCommand.CreateParameter();
            paramFilterCriteria.ParameterName = "@FilterCriteriaTable";
            paramFilterCriteria.Value = dtFilterCriteria;

            IDbDataParameter paramTaskCategoryID = oCommand.CreateParameter();
            paramTaskCategoryID.ParameterName = "@TaskCategoryID";
            if (taskCategoryID.HasValue)
                paramTaskCategoryID.Value = taskCategoryID.Value;
            else
                paramTaskCategoryID.Value = System.DBNull.Value;

            IDbDataParameter paramIsShowHidden = oCommand.CreateParameter();
            paramIsShowHidden.ParameterName = "@IsShowHidden";
            paramIsShowHidden.Value = isShowHidden.Value;

            IDbDataParameter paramLanguageID = oCommand.CreateParameter();
            paramLanguageID.ParameterName = "@LCID";
            if (LanguageID.HasValue)
                paramLanguageID.Value = LanguageID.Value;
            else
                paramLanguageID.Value = System.DBNull.Value;

            IDbDataParameter paramDefualtLanguageID = oCommand.CreateParameter();
            paramDefualtLanguageID.ParameterName = "@DefaultLCID";
            if (DefaultLanguageID.HasValue)
                paramDefualtLanguageID.Value = LanguageID.Value;
            else
                paramDefualtLanguageID.Value = System.DBNull.Value;

            cmdParams.Add(paramUserID);
            cmdParams.Add(paramRoleID);
            cmdParams.Add(paramRecPeriodID);
            cmdParams.Add(paramTaskTypeID);
            cmdParams.Add(paramFilterCriteria);
            cmdParams.Add(paramTaskCategoryID);
            cmdParams.Add(paramIsShowHidden);
            cmdParams.Add(paramLanguageID);
            cmdParams.Add(paramDefualtLanguageID);

            return oCommand;
        }


        private IDbCommand GetAddTaskCommand(int recPeriodID, DataTable dtTaskHdr, DataTable dtTaskAttribute, DataTable dtAttachment, string addedBy, DateTime dateAdded)
        {
            IDbCommand oCommand = this.CreateCommand("[TaskMaster].[usp_INS_TaskHdr]");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;


            IDbDataParameter paramRecPeriodID = oCommand.CreateParameter();
            paramRecPeriodID.ParameterName = "@RecPeriodID";
            paramRecPeriodID.Value = recPeriodID;

            IDbDataParameter paramDtTaskHdr = oCommand.CreateParameter();
            paramDtTaskHdr.ParameterName = "@udtTaskHdr";
            paramDtTaskHdr.Value = dtTaskHdr;

            IDbDataParameter paramDtTaskAttribute = oCommand.CreateParameter();
            paramDtTaskAttribute.ParameterName = "@udtTaskAttributes";
            paramDtTaskAttribute.Value = dtTaskAttribute;

            IDbDataParameter paramdtAttachment = oCommand.CreateParameter();
            paramdtAttachment.ParameterName = "@udtAttachment";
            paramdtAttachment.Value = dtAttachment;

            IDbDataParameter paramAddedBy = oCommand.CreateParameter();
            paramAddedBy.ParameterName = "@AddedBy";
            paramAddedBy.Value = addedBy;

            IDbDataParameter paramDateAdded = oCommand.CreateParameter();
            paramDateAdded.ParameterName = "@DateAdded";
            paramDateAdded.Value = dateAdded;

            cmdParams.Add(paramRecPeriodID);
            cmdParams.Add(paramDtTaskHdr);
            cmdParams.Add(paramDtTaskAttribute);
            cmdParams.Add(paramdtAttachment);
            cmdParams.Add(paramAddedBy);
            cmdParams.Add(paramDateAdded);

            return oCommand;
        }

        private IDbCommand GetAddEditUserTaskVisibilityCommand(int? recPeriodID, DataTable dtUserTaskVisibility, string addedBy, string RevisedBy, bool IsHidden)
        {
            IDbCommand oCommand = this.CreateCommand("[TaskMaster].[usp_SAV_UserTaskVisibility]");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;

            IDbDataParameter paramDtUserTaskVisibility = oCommand.CreateParameter();
            paramDtUserTaskVisibility.ParameterName = "@udtUserTask";
            paramDtUserTaskVisibility.Value = dtUserTaskVisibility;

            IDbDataParameter paramAddedBy = oCommand.CreateParameter();
            paramAddedBy.ParameterName = "@AddedBy";
            paramAddedBy.Value = addedBy;

            IDbDataParameter paramRevisedBy = oCommand.CreateParameter();
            paramRevisedBy.ParameterName = "@RevisedBy";
            paramRevisedBy.Value = RevisedBy;

            IDbDataParameter paramIsHidden = oCommand.CreateParameter();
            paramIsHidden.ParameterName = "@IsHidden";
            paramIsHidden.Value = IsHidden;

            IDbDataParameter paramRecPeriodID = oCommand.CreateParameter();
            paramRecPeriodID.ParameterName = "@RecPeriodID";
            paramRecPeriodID.Value = recPeriodID;


            cmdParams.Add(paramDtUserTaskVisibility);
            cmdParams.Add(paramAddedBy);
            cmdParams.Add(paramRevisedBy);
            cmdParams.Add(paramIsHidden);
            cmdParams.Add(paramRecPeriodID);

            return oCommand;
        }

        protected override TaskHdrInfo MapObject(IDataReader reader)
        {
            TaskHdrInfo oEntity = base.MapObject(reader);

            oEntity.TaskDetailID = reader.GetInt64Value(COLUMN_TASKLDETAIL_ID);
            oEntity.TaskDetailRecPeriodID = reader.GetInt32Value(COLUMN_TASK_DETAIL_RECPERIODID);
            oEntity.TaskApproverLastComment = reader.GetStringValue(COLUMN_TASK_APPROVER_LAST_COMMENT);
            oEntity.TaskApprovalDuration = reader.GetInt32Value(COLUMN_TASK_APPROVAL_DURATION);
            oEntity.TaskStatusID = reader.GetInt16Value(COLUMN_TASK_STATUS_ID);
            oEntity.TaskStatusLabelID = reader.GetInt32Value(COLUMN_TASK_STATUS_LABEL_ID);
            oEntity.TaskStatusDate = reader.GetDateValue(COLUMN_TASK_STATUS_DATE);
            oEntity.TaskCompletionDate = reader.GetDateValue(COLUMN_TASK_COMPLETION_DATE);
            oEntity.Comment = reader.GetStringValue(COLUMN_COMMENT);
            oEntity.IsActive = reader.GetBooleanValue("IsActive");
            oEntity.IsDeleted = reader.GetBooleanValue("IsDeleted");
            oEntity.IsHidden = reader.GetBooleanValue(COLUMN_TASK_ISTASKHIDDEN);
            oEntity.IsWorkStarted = reader.GetBooleanValue(COLUMN_TASK_ISWORKSTARTED);
            oEntity.NumberValue = reader.GetInt32Value("NumberValue");
            //xmlString = reader.GetStringValue(COLUMN_ADDED_BY_USER);
            //oEntity.TaskDetailAddedByUser = DeSerializeGenericObject<UserHdrInfo>(xmlString);
            UserHdrInfo oAddedByUser = new UserHdrInfo();
            oAddedByUser.FirstName = reader.GetStringValue("TaskDetailAddedByFirstName");
            oAddedByUser.LastName = reader.GetStringValue("TaskDetailAddedByLastName");
            oAddedByUser.UserID = reader.GetInt32Value("AddedByUserID");
            //if (oAddedByUser.UserID.HasValue)
            oEntity.TaskDetailAddedByUser = oAddedByUser;

            //xmlString = reader.GetStringValue(COLUMN_TASK_DETAIL_REVISED_BY_USER);
            //oEntity.TaskDetailRevisedByUser = DeSerializeGenericObject<UserHdrInfo>(xmlString);
            UserHdrInfo oRevisedByUser = new UserHdrInfo();
            oRevisedByUser.FirstName = reader.GetStringValue("TaskDetaiRevisedByFirstName");
            oRevisedByUser.LastName = reader.GetStringValue("TaskDetaiRevisedByLastName");
            oRevisedByUser.UserID = reader.GetInt32Value("RevisedByUserID");
            //if (oRevisedByUser.UserID.HasValue)
            oEntity.TaskDetailRevisedByUser = oRevisedByUser;


            oEntity.TaskDetailDateRevised = reader.GetDateValue(COLUMN_TASK_DETAIL_DATE_REVISED);

            #region "Attributes"
            //TaskName
            oEntity.TaskName = reader.GetStringValue(COLUMN_TASKNAME);

            //TaskList

            TaskListHdrInfo oTaskList = new TaskListHdrInfo();
            oTaskList.TaskListID = reader.GetInt16Value("TaskListID");
            oTaskList.TaskListName = reader.GetStringValue("TaskListName");
            oTaskList.AddedBy = reader.GetStringValue("TaskListAddedBy");
            if (oTaskList.TaskListID.HasValue)
                oEntity.TaskList = oTaskList;
            //TaskSubList
            TaskSubListHdrInfo oTaskSubListHdrInfo = new TaskSubListHdrInfo();
            oTaskSubListHdrInfo.TaskSubListID = reader.GetInt16Value("TaskSubListID");
            oTaskSubListHdrInfo.TaskSubListName = reader.GetStringValue("TaskSubListName");
            oTaskSubListHdrInfo.AddedBy = reader.GetStringValue("TaskSubListAddedBy");
            if (oTaskSubListHdrInfo.TaskSubListID.HasValue)
                oEntity.TaskSubList = oTaskSubListHdrInfo;
            //Description
            oEntity.TaskDescription = reader.GetStringValue(COLUMN_TASK_DESCRIPTION);

            ////Assignee
            ////xmlString = reader.GetStringValue(COLUMN_ASSIGNEE);
            ////oEntity.AssignedTo = DeSerializeGenericObject<UserHdrInfo>(xmlString);
            //UserHdrInfo oAssignee = new UserHdrInfo();
            //oAssignee.UserID = reader.GetInt32Value("AssigneeID");
            //oAssignee.FirstName = reader.GetStringValue("AssigneeFirstName");
            //oAssignee.LastName = reader.GetStringValue("AssigneeLastName");
            ////if (oAssignee.UserID.HasValue)
            //oEntity.AssignedTo = oAssignee;
            ////Approver
            ////xmlString = reader.GetStringValue(COLUMN_APPROVER);
            ////oEntity.Approver = DeSerializeGenericObject<UserHdrInfo>(xmlString);
            //UserHdrInfo oApprover = new UserHdrInfo();
            //oApprover.UserID = reader.GetInt32Value("ApproverID");
            //oApprover.FirstName = reader.GetStringValue("ApproverFirstName");
            //oApprover.LastName = reader.GetStringValue("ApproverLastName");
            ////if (oApprover.UserID.HasValue)
            //oEntity.Reviewer = oApprover;
            ////Notify-List

            //Attached Documents-List

            //Recurrence Type
            //xmlString = reader.GetStringValue(COLUMN_TASK_RECURRENCE_TYPE);
            //oEntity.RecurrenceType = DeSerializeGenericObject<TaskRecurrenceTypeMstInfo>(xmlString);
            //oEntity.RecurrenceTypeID = reader.GetInt16Value(COLUMN_TASK_RECURRENCE_TYPE_ID);
            TaskRecurrenceTypeMstInfo oRecurrence = new TaskRecurrenceTypeMstInfo();
            oRecurrence.TaskRecurrenceTypeID = reader.GetInt16Value("TaskRecurrenceTypeID");
            oRecurrence.RecurrenceTypeLabelID = reader.GetInt32Value("TaskRecurrenceTypeLabelID");
            if (oRecurrence.TaskRecurrenceTypeID.HasValue)
                oEntity.RecurrenceType = oRecurrence;
            //Recurrence Frequency-List

            //Task Start Date
            oEntity.TaskStartDate = reader.GetDateValue(COLUMN_TASK_START_DATE);

            //Task Due Date
            oEntity.TaskDueDate = reader.GetDateValue(COLUMN_TASK_DUE_DATE);

            //Assignee Due Date
            oEntity.AssigneeDueDate = reader.GetDateValue(COLUMN_ASSIGNEE_DUE_DATE);

            //Approval Due Date
            oEntity.ReviewerDueDate = reader.GetDateValue(COLUMN_REVIEWER_DUE_DATE);

            //Assignee Due Days
            oEntity.AssigneeDueDays = reader.GetInt32Value(COLUMN_ASSIGNEE_DUE_DAYS);

            //Approval Due Days
            oEntity.ReviewerDueDays = reader.GetInt32Value(COLUMN_REVIEWER_DUE_DAYS);

            //Task Account-List

            //Task Due Days
            oEntity.TaskDueDays = reader.GetInt32Value(COLUMN_TASK_DUE_DAYS);

            //CreationDocCount 
            oEntity.CreationDocCount = reader.GetInt32Value("CreationDocCount");

            //CompletionDocCount
            oEntity.CompletionDocCount = reader.GetInt32Value("CompletionDocCount");
            oEntity.CustomField1 = reader.GetStringValue("CustomField1");
            oEntity.CustomField2 = reader.GetStringValue("CustomField2");
            #endregion

            return oEntity;
        }

        //Deserialize generic type
        private static T DeSerializeGenericObject<T>(string xmlString)
        {
            XmlSerializer xmlSerial = null;
            StringReader strReader = null;
            XmlTextReader txtReader = null;
            T oEntity = default(T);

            if (!String.IsNullOrEmpty(xmlString))
            {
                try
                {
                    xmlSerial = new XmlSerializer(typeof(T));
                    strReader = new StringReader(xmlString);
                    txtReader = new XmlTextReader(strReader);
                    oEntity = (T)xmlSerial.Deserialize(txtReader);
                }
                finally
                {
                    txtReader.Close();
                    strReader.Close();
                }
            }
            return oEntity;

        }

        //private static UserHdrInfo DeSerializeUserHdr(string xmlString)
        //{
        //    XmlSerializer xmlSerial = null;
        //    StringReader strReader = null;
        //    XmlTextReader txtReader = null;
        //    try
        //    {
        //        xmlSerial = new XmlSerializer(typeof(UserHdrInfo));
        //        strReader = new StringReader(xmlString);
        //        txtReader = new XmlTextReader(strReader);
        //        return (UserHdrInfo)xmlSerial.Deserialize(txtReader);
        //    }
        //    finally
        //    {
        //        txtReader.Close();
        //        strReader.Close();
        //    }
        //}
        //private static TaskListHdrInfo DeSerializeTaskList(string xmlString)
        //{
        //    XmlSerializer xmlSerial = null;
        //    StringReader strReader = null;
        //    XmlTextReader txtReader = null;
        //    try
        //    {
        //        xmlSerial = new XmlSerializer(typeof(TaskListHdrInfo));
        //        strReader = new StringReader(xmlString);
        //        txtReader = new XmlTextReader(strReader);
        //        return (TaskListHdrInfo)xmlSerial.Deserialize(txtReader);
        //    }
        //    finally
        //    {
        //        txtReader.Close();
        //        strReader.Close();
        //    }
        //}
        //Deserialize list of generic type

        private static List<T> DeSerializeObjectList<T>(string xmlString)
        {
            XmlSerializer xmlSerial = null;
            StringReader strReader = null;
            XmlTextReader txtReader = null;
            List<T> oEntityList = default(List<T>);

            if (!String.IsNullOrEmpty(xmlString))
            {
                try
                {
                    xmlSerial = new XmlSerializer(typeof(List<T>));
                    strReader = new StringReader(xmlString);
                    txtReader = new XmlTextReader(strReader);
                    oEntityList = (List<T>)xmlSerial.Deserialize(txtReader);
                }
                finally
                {
                    txtReader.Close();
                    strReader.Close();
                }
            }
            return oEntityList;
        }
        #region Edit task
        public bool EditTask(List<TaskHdrInfo> oTasksHdrList, int recPeriodID, string revisedBy, DateTime dateRevised)
        {
            bool isUpdated = false;
            IDbCommand oCmd = null;
            IDbConnection oConn = null;
            IDbTransaction oTrans = null;
            DataTable dtTaskHdr = null;
            DataTable dtTaskAttribute = null;
            try
            {
                dtTaskHdr = ServiceHelper.ConvertTaskHdrToDataTable(oTasksHdrList);
                dtTaskAttribute = ServiceHelper.ConvertTaskAttributeToDataTable(oTasksHdrList);
                oConn = this.CreateConnection();
                oCmd = this.GetEditTaskCommand(recPeriodID, dtTaskHdr, dtTaskAttribute, revisedBy, dateRevised);
                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                oTrans = oConn.BeginTransaction();
                oCmd.Transaction = oTrans;
                oCmd.ExecuteNonQuery();
                oTrans.Commit();
                oTrans.Dispose();
                isUpdated = true;
            }
            catch (Exception ex)
            {
                if (oTrans != null && oConn != null)
                {
                    oTrans.Rollback();
                    oTrans.Dispose();
                }
                throw ex;
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
            return isUpdated;
        }
        private IDbCommand GetEditTaskCommand(int recPeriodID, DataTable dtTaskHdr, DataTable dtTaskAttribute, string revisedBy, DateTime dateRevised)
        {
            IDbCommand oCommand = this.CreateCommand("[TaskMaster].[usp_UPD_TaskHdr]");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;


            IDbDataParameter paramRecPeriodID = oCommand.CreateParameter();
            paramRecPeriodID.ParameterName = "@RecPeriodID";
            paramRecPeriodID.Value = recPeriodID;

            IDbDataParameter paramDtTaskHdr = oCommand.CreateParameter();
            paramDtTaskHdr.ParameterName = "@udtTaskHdr";
            paramDtTaskHdr.Value = dtTaskHdr;

            IDbDataParameter paramDtTaskAttribute = oCommand.CreateParameter();
            paramDtTaskAttribute.ParameterName = "@udtTaskAttributes";
            paramDtTaskAttribute.Value = dtTaskAttribute;

            IDbDataParameter paramReviseBy = oCommand.CreateParameter();
            paramReviseBy.ParameterName = "@ReviseBy";
            paramReviseBy.Value = revisedBy;

            IDbDataParameter paramDateRevised = oCommand.CreateParameter();
            paramDateRevised.ParameterName = "@DateRevised";
            paramDateRevised.Value = dateRevised;

            cmdParams.Add(paramRecPeriodID);
            cmdParams.Add(paramDtTaskHdr);
            cmdParams.Add(paramDtTaskAttribute);
            cmdParams.Add(paramReviseBy);
            cmdParams.Add(paramDateRevised);

            return oCommand;
        }
        # endregion

        public List<TaskHdrInfo> GetAccessibleTasksByActionTypeID(int userID, short roleID, int recPeriodID, ARTEnums.TaskType taskType, ARTEnums.TaskActionType taskActionType, List<long> AccountIDs
            , short? taskCategoryID, bool? isShowHidden, int? LanguageID, int? DefaultLanguageID)
        {
            IDbCommand oCmd = null;
            IDbConnection oConn = null;
            IDataReader reader = null;
            List<TaskHdrInfo> oTaskHdrInfoList = new List<TaskHdrInfo>();
            try
            {
                oConn = this.CreateConnection();
                oCmd = this.GetAccessibleTasksByActionTypeIDCommand("[TaskMaster].[usp_SEL_AccessibleTasksByActionTypeID]", userID, roleID, recPeriodID, taskType, taskActionType, AccountIDs
                    , taskCategoryID, isShowHidden, true, LanguageID, DefaultLanguageID);
                oCmd.CommandType = CommandType.StoredProcedure;


                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                reader = oCmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    TaskHdrInfo oTaskListinfo = this.MapObject(reader);
                    if (!string.IsNullOrEmpty(oTaskListinfo.TaskName))
                        oTaskHdrInfoList.Add(oTaskListinfo);
                }
                reader.ClearColumnHash();
                // Net Resultset for TaskOwners
                reader.NextResult();
                while (reader.Read())
                {
                    AddTaskOwners(oTaskHdrInfoList, reader);
                }
                reader.ClearColumnHash();
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
            return oTaskHdrInfoList;
        }

        public List<TaskHdrInfo> GetApproverAccessableTaskByUserID(int userID, short roleID, int recPeriodID, ARTEnums.TaskType taskType, List<long> AccountIDs
            , short? taskCategoryID, bool? isShowHidden, int? LanguageID, int? DefaultLanguageID)
        {
            IDbCommand oCmd = null;
            IDbConnection oConn = null;
            IDataReader reader = null;
            List<TaskHdrInfo> oTaskHdrInfoList = new List<TaskHdrInfo>();
            try
            {
                oConn = this.CreateConnection();
                oCmd = this.GetAccessableTaskByUserIDCommand("[TaskMaster].[usp_GET_ApproverAccessableTaskByUserID]", userID, roleID, recPeriodID, taskType, AccountIDs
                    , taskCategoryID, isShowHidden, true, LanguageID, DefaultLanguageID);
                oCmd.CommandType = CommandType.StoredProcedure;


                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                reader = oCmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    TaskHdrInfo oTaskListinfo = this.MapObject(reader);
                    oTaskHdrInfoList.Add(oTaskListinfo);
                }
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
            return oTaskHdrInfoList;
        }

        public List<TaskHdrInfo> GetAssingneeAccessableTaskByUserID(int userID, short roleID, int recPeriodID, ARTEnums.TaskType taskType, List<long> AccountIDs
            , short? taskCategoryID, bool? isShowHidden, int? LanguageID, int? DefaultLanguageID)
        {
            IDbCommand oCmd = null;
            IDbConnection oConn = null;
            IDataReader reader = null;
            List<TaskHdrInfo> oTaskHdrInfoList = new List<TaskHdrInfo>();
            try
            {
                oConn = this.CreateConnection();
                oCmd = this.GetAccessableTaskByUserIDCommand("[TaskMaster].[usp_GET_AssigneeAccessableTaskByUserID]", userID, roleID, recPeriodID, taskType, AccountIDs
                    , taskCategoryID, isShowHidden, true, LanguageID, DefaultLanguageID);
                oCmd.CommandType = CommandType.StoredProcedure;

                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                reader = oCmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    TaskHdrInfo oTaskListinfo = this.MapObject(reader);
                    oTaskHdrInfoList.Add(oTaskListinfo);
                }
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
            return oTaskHdrInfoList;
        }

        public List<TaskHdrInfo> GetDeleteAccessableTaskByUserID(int userID, short roleID, int recPeriodID, ARTEnums.TaskType taskType
            , short? taskCategoryID, bool? isShowHidden, int? LanguageID, int? DefaultLanguageID)
        {
            IDbCommand oCmd = null;
            IDbConnection oConn = null;
            IDataReader reader = null;
            List<TaskHdrInfo> oTaskHdrInfoList = new List<TaskHdrInfo>();
            try
            {
                oConn = this.CreateConnection();
                oCmd = this.GetAccessableTaskByUserIDCommand("[TaskMaster].[usp_GET_DeleteAccessableTaskByUserID]", userID, roleID, recPeriodID, taskType, null
                    , taskCategoryID, isShowHidden, false, LanguageID, DefaultLanguageID);
                oCmd.CommandType = CommandType.StoredProcedure;

                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                reader = oCmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    TaskHdrInfo oTaskListinfo = this.MapObject(reader);
                    if (!string.IsNullOrEmpty(oTaskListinfo.TaskName))
                        oTaskHdrInfoList.Add(oTaskListinfo);
                }
                reader.ClearColumnHash();
                // Net Resultset for TaskOwners
                reader.NextResult();
                while (reader.Read())
                {
                    AddTaskOwners(oTaskHdrInfoList, reader);
                }
                reader.ClearColumnHash();
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
            return oTaskHdrInfoList;
        }

        private IDbCommand GetAccessableTaskByUserIDCommand(string USPName, int userID, short roleID, int recPeriodID, ARTEnums.TaskType taskType,
            List<long> AccountIDs, short? taskCategoryID, bool? isShowHidden, bool addAccountIDParam, int? LanguageID, int? DefaultLanguageID)
        {
            IDbCommand oCommand = this.CreateCommand(USPName);
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;

            IDbDataParameter paramUserID = oCommand.CreateParameter();
            paramUserID.ParameterName = "@UserID";
            paramUserID.Value = userID;

            IDbDataParameter paramRoleID = oCommand.CreateParameter();
            paramRoleID.ParameterName = "@RoleID";
            paramRoleID.Value = roleID;

            IDbDataParameter paramRecPeriodID = oCommand.CreateParameter();
            paramRecPeriodID.ParameterName = "@RecPeriodID";
            paramRecPeriodID.Value = recPeriodID;

            IDbDataParameter paramTaskTypeID = oCommand.CreateParameter();
            paramTaskTypeID.ParameterName = "@TaskTypeID";
            paramTaskTypeID.Value = Convert.ToInt16(taskType);

            IDbDataParameter paramTaskCategoryID = oCommand.CreateParameter();
            paramTaskCategoryID.ParameterName = "@TaskCategoryID";
            if (taskCategoryID.HasValue)
                paramTaskCategoryID.Value = taskCategoryID.Value;
            else
                paramTaskCategoryID.Value = System.DBNull.Value;

            IDbDataParameter paramIsShowHidden = oCommand.CreateParameter();
            paramIsShowHidden.ParameterName = "@IsShowHidden";
            paramIsShowHidden.Value = isShowHidden.Value;

            IDbDataParameter paramLanguageID = oCommand.CreateParameter();
            paramLanguageID.ParameterName = "@LCID";
            if (LanguageID.HasValue)
                paramLanguageID.Value = LanguageID.Value;
            else
                paramLanguageID.Value = System.DBNull.Value;

            IDbDataParameter paramDefualtLanguageID = oCommand.CreateParameter();
            paramDefualtLanguageID.ParameterName = "@DefaultLCID";
            if (DefaultLanguageID.HasValue)
                paramDefualtLanguageID.Value = LanguageID.Value;
            else
                paramDefualtLanguageID.Value = System.DBNull.Value;


            cmdParams.Add(paramUserID);
            cmdParams.Add(paramRoleID);
            cmdParams.Add(paramRecPeriodID);
            cmdParams.Add(paramTaskTypeID);
            cmdParams.Add(paramTaskCategoryID);
            cmdParams.Add(paramIsShowHidden);
            cmdParams.Add(paramLanguageID);
            cmdParams.Add(paramDefualtLanguageID);

            if (addAccountIDParam)
            {
                IDbDataParameter paramAccIDs = oCommand.CreateParameter();
                paramAccIDs.ParameterName = "@udtAccountID";
                paramAccIDs.Value = ServiceHelper.ConvertLongIDCollectionToDataTable(AccountIDs);
                cmdParams.Add(paramAccIDs);
            }



            return oCommand;
        }

        private IDbCommand GetAccessibleTasksByActionTypeIDCommand(string USPName, int userID, short roleID, int recPeriodID, ARTEnums.TaskType taskType,
            ARTEnums.TaskActionType taskActionType, List<long> AccountIDs, short? taskCategoryID, bool? isShowHidden, bool addAccountIDParam, int? LanguageID, int? DefaultLanguageID)
        {
            IDbCommand oCommand = this.CreateCommand(USPName);
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;

            IDbDataParameter paramUserID = oCommand.CreateParameter();
            paramUserID.ParameterName = "@UserID";
            paramUserID.Value = userID;

            IDbDataParameter paramRoleID = oCommand.CreateParameter();
            paramRoleID.ParameterName = "@RoleID";
            paramRoleID.Value = roleID;

            IDbDataParameter paramRecPeriodID = oCommand.CreateParameter();
            paramRecPeriodID.ParameterName = "@RecPeriodID";
            paramRecPeriodID.Value = recPeriodID;

            IDbDataParameter paramTaskTypeID = oCommand.CreateParameter();
            paramTaskTypeID.ParameterName = "@TaskTypeID";
            paramTaskTypeID.Value = Convert.ToInt16(taskType);

            IDbDataParameter paramTaskActionTypeID = oCommand.CreateParameter();
            paramTaskActionTypeID.ParameterName = "@TaskActionTypeID";
            paramTaskActionTypeID.Value = Convert.ToInt16(taskActionType);

            IDbDataParameter paramTaskCategoryID = oCommand.CreateParameter();
            paramTaskCategoryID.ParameterName = "@TaskCategoryID";
            if (taskCategoryID.HasValue)
                paramTaskCategoryID.Value = taskCategoryID.Value;
            else
                paramTaskCategoryID.Value = System.DBNull.Value;

            IDbDataParameter paramIsShowHidden = oCommand.CreateParameter();
            paramIsShowHidden.ParameterName = "@IsShowHidden";
            paramIsShowHidden.Value = isShowHidden.Value;

            IDbDataParameter paramLanguageID = oCommand.CreateParameter();
            paramLanguageID.ParameterName = "@LCID";
            if (LanguageID.HasValue)
                paramLanguageID.Value = LanguageID.Value;
            else
                paramLanguageID.Value = System.DBNull.Value;

            IDbDataParameter paramDefualtLanguageID = oCommand.CreateParameter();
            paramDefualtLanguageID.ParameterName = "@DefaultLCID";
            if (DefaultLanguageID.HasValue)
                paramDefualtLanguageID.Value = LanguageID.Value;
            else
                paramDefualtLanguageID.Value = System.DBNull.Value;


            cmdParams.Add(paramUserID);
            cmdParams.Add(paramRoleID);
            cmdParams.Add(paramRecPeriodID);
            cmdParams.Add(paramTaskTypeID);
            cmdParams.Add(paramTaskActionTypeID);
            cmdParams.Add(paramTaskCategoryID);
            cmdParams.Add(paramIsShowHidden);
            cmdParams.Add(paramLanguageID);
            cmdParams.Add(paramDefualtLanguageID);

            if (addAccountIDParam)
            {
                IDbDataParameter paramAccIDs = oCommand.CreateParameter();
                paramAccIDs.ParameterName = "@udtAccountID";
                paramAccIDs.Value = ServiceHelper.ConvertLongIDCollectionToDataTable(AccountIDs);
                cmdParams.Add(paramAccIDs);
            }
            return oCommand;
        }

        public void DeleteAccessibleTasks(List<long> TaskIDs, int RecPeriodID, string ModifiedBy, DateTime DateModified)
        {

            IDbCommand oCmd = null;
            IDbConnection oConn = null;
            try
            {
                oConn = this.CreateConnection();
                oCmd = this.CreateCommand("[TaskMaster].[usp_DEL_TaskHdr]");
                oCmd.CommandType = CommandType.StoredProcedure;

                IDataParameterCollection cmdParams = oCmd.Parameters;

                IDbDataParameter paramTaskIDList = oCmd.CreateParameter();
                paramTaskIDList.ParameterName = "@udtTaskIDs";
                paramTaskIDList.Value = ServiceHelper.ConvertLongIDCollectionToDataTable(TaskIDs);

                IDbDataParameter paramRecPeriodID = oCmd.CreateParameter();
                paramRecPeriodID.ParameterName = "@RecPeriodID";
                paramRecPeriodID.Value = RecPeriodID;

                IDbDataParameter paramModifiedBy = oCmd.CreateParameter();
                paramModifiedBy.ParameterName = "@ModifiedBy";
                paramModifiedBy.Value = ModifiedBy;

                IDbDataParameter paramDateModified = oCmd.CreateParameter();
                paramDateModified.ParameterName = "@DateModified";
                paramDateModified.Value = DateModified;


                cmdParams.Add(paramTaskIDList);
                cmdParams.Add(paramRecPeriodID);
                cmdParams.Add(paramModifiedBy);
                cmdParams.Add(paramDateModified);

                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                oCmd.ExecuteNonQuery();
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
        }

        public List<TaskHdrInfo> GetAccessableTaskByAccountIDs(int userID, short roleID, int recPeriodID, List<long> AccountIDs,
            short? taskCategoryID, bool? isShowHidden, int? LanguageID, int? DefaultLanguageID)
        {
            IDbCommand oCmd = null;
            IDbConnection oConn = null;
            IDataReader reader = null;
            List<TaskHdrInfo> oTaskHdrInfoList = new List<TaskHdrInfo>();
            try
            {
                oConn = this.CreateConnection();
                oCmd = this.CreateCommand("[TaskMaster].[usp_GET_AccountAccessableTaskByUserID]");
                oCmd.CommandType = CommandType.StoredProcedure;

                IDataParameterCollection cmdParams = oCmd.Parameters;

                IDbDataParameter paramUserID = oCmd.CreateParameter();
                paramUserID.ParameterName = "@UserID";
                paramUserID.Value = userID;

                IDbDataParameter paramRoleID = oCmd.CreateParameter();
                paramRoleID.ParameterName = "@RoleID";
                paramRoleID.Value = roleID;

                IDbDataParameter paramAccIDs = oCmd.CreateParameter();
                paramAccIDs.ParameterName = "@udtAccountID";
                paramAccIDs.Value = ServiceHelper.ConvertLongIDCollectionToDataTable(AccountIDs);

                IDbDataParameter paramRecPeriodID = oCmd.CreateParameter();
                paramRecPeriodID.ParameterName = "@RecPeriodID";
                paramRecPeriodID.Value = recPeriodID;

                IDbDataParameter paramTaskCategoryID = oCmd.CreateParameter();
                paramTaskCategoryID.ParameterName = "@TaskCategoryID";
                if (taskCategoryID.HasValue)
                    paramTaskCategoryID.Value = taskCategoryID.Value;
                else
                    paramTaskCategoryID.Value = System.DBNull.Value;

                IDbDataParameter paramIsShowHidden = oCmd.CreateParameter();
                paramIsShowHidden.ParameterName = "@IsShowHidden";
                paramIsShowHidden.Value = isShowHidden.Value;

                IDbDataParameter paramLanguageID = oCmd.CreateParameter();
                paramLanguageID.ParameterName = "@LCID";
                if (LanguageID.HasValue)
                    paramLanguageID.Value = LanguageID.Value;
                else
                    paramLanguageID.Value = System.DBNull.Value;

                IDbDataParameter paramDefualtLanguageID = oCmd.CreateParameter();
                paramDefualtLanguageID.ParameterName = "@DefaultLCID";
                if (DefaultLanguageID.HasValue)
                    paramDefualtLanguageID.Value = LanguageID.Value;
                else
                    paramDefualtLanguageID.Value = System.DBNull.Value;


                cmdParams.Add(paramUserID);
                cmdParams.Add(paramRecPeriodID);
                cmdParams.Add(paramRoleID);
                cmdParams.Add(paramAccIDs);
                cmdParams.Add(paramTaskCategoryID);
                cmdParams.Add(paramIsShowHidden);
                cmdParams.Add(paramLanguageID);
                cmdParams.Add(paramDefualtLanguageID);

                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                reader = oCmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    TaskHdrInfo oTaskHdrInfo = this.MapObject(reader);
                    if (!string.IsNullOrEmpty(oTaskHdrInfo.TaskName))
                        oTaskHdrInfoList.Add(oTaskHdrInfo);
                }
                reader.ClearColumnHash();
                // Net Resultset for TaskOwners
                reader.NextResult();
                while (reader.Read())
                {
                    AddTaskOwners(oTaskHdrInfoList, reader);
                }
                reader.ClearColumnHash();
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
            return oTaskHdrInfoList;
        }

        public bool BulkEditTask(List<TaskHdrInfo> oTasksHdrList, int recPeriodID, string revisedBy, DateTime dateRevised)
        {
            bool isUpdated = false;
            IDbCommand oCmd = null;
            IDbConnection oConn = null;
            IDbTransaction oTrans = null;
            DataTable dtTaskAttribute = null;
            try
            {
                //dtTaskHdr = ServiceHelper.ConvertTaskHdrToDataTable(oTasksHdrList);
                dtTaskAttribute = ServiceHelper.ConvertTaskAttributeToDataTableBulkEdit(oTasksHdrList);
                oConn = this.CreateConnection();
                //oCmd = this.GetEditTaskCommand(recPeriodID, dtTaskHdr, dtTaskAttribute, revisedBy, dateRevised);

                oCmd = this.CreateCommand("[TaskMaster].[usp_UPD_BulkTaskHdr]");
                oCmd.CommandType = CommandType.StoredProcedure;

                IDataParameterCollection cmdParams = oCmd.Parameters;

                IDbDataParameter paramTaskIDList = oCmd.CreateParameter();
                paramTaskIDList.ParameterName = "@udtTaskAttributeValue";
                paramTaskIDList.Value = dtTaskAttribute;

                IDbDataParameter paramRecPeriodID = oCmd.CreateParameter();
                paramRecPeriodID.ParameterName = "@RecPeriodID";
                paramRecPeriodID.Value = recPeriodID;

                IDbDataParameter paramModifiedBy = oCmd.CreateParameter();
                paramModifiedBy.ParameterName = "@ModifiedBy";
                paramModifiedBy.Value = revisedBy;

                IDbDataParameter paramDateModified = oCmd.CreateParameter();
                paramDateModified.ParameterName = "@DateModified";
                paramDateModified.Value = dateRevised;


                cmdParams.Add(paramTaskIDList);
                cmdParams.Add(paramRecPeriodID);
                cmdParams.Add(paramModifiedBy);
                cmdParams.Add(paramDateModified);

                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                oTrans = oConn.BeginTransaction();
                oCmd.Transaction = oTrans;
                oCmd.ExecuteNonQuery();
                oTrans.Commit();
                oTrans.Dispose();
                isUpdated = true;
            }
            catch (Exception ex)
            {
                if (oTrans != null && oConn != null)
                {
                    oTrans.Rollback();
                    oTrans.Dispose();
                }
                throw ex;
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
            return isUpdated;
        }



        #region "Delete TaskLoad"
        public List<DataImportHdrInfo> DeleteTaskLoad(int DataImportID, string revisedBy, DateTime dateRevised)
        {
            IDbCommand oCmd = null;
            IDbConnection oConn = null;
            IDataReader reader = null;
            IDbTransaction oTrans = null;
            List<DataImportHdrInfo> oDataImportHdrInfoList = new List<DataImportHdrInfo>();
            try
            {
                oConn = this.CreateConnection();
                oCmd = this.GetDeleteTaskLoadCommand(DataImportID, revisedBy, dateRevised);
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                oTrans = oConn.BeginTransaction();
                oCmd.Transaction = oTrans;
                reader = oCmd.ExecuteReader();
                while (reader.Read())
                {
                    DataImportHdrInfo oDataImportHdrInfo = this.MapObjectDataImportHdr(reader);
                    oDataImportHdrInfoList.Add(oDataImportHdrInfo);
                }
                reader.Close();
                oTrans.Commit();
                oTrans.Dispose();
            }
            catch (Exception ex)
            {
                if (oTrans != null && oConn != null)
                {
                    oTrans.Rollback();
                    oTrans.Dispose();
                }
                throw ex;
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
            return oDataImportHdrInfoList;
        }
        private IDbCommand GetDeleteTaskLoadCommand(int DataImportID, string revisedBy, DateTime dateRevised)
        {
            IDbCommand oCommand = this.CreateCommand("[TaskMaster].[usp_DEL_TaskLoadPermanent]");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;

            IDbDataParameter paramDataImportID = oCommand.CreateParameter();
            paramDataImportID.ParameterName = "@DataImportID";
            paramDataImportID.Value = DataImportID;

            IDbDataParameter paramRevisedBy = oCommand.CreateParameter();
            paramRevisedBy.ParameterName = "@RevisedBy";
            paramRevisedBy.Value = revisedBy;

            IDbDataParameter paramDateRevised = oCommand.CreateParameter();
            paramDateRevised.ParameterName = "@DateRevised";
            paramDateRevised.Value = dateRevised;

            cmdParams.Add(paramDataImportID);
            cmdParams.Add(paramRevisedBy);
            cmdParams.Add(paramDateRevised);


            return oCommand;
        }
        private DataImportHdrInfo MapObjectDataImportHdr(System.Data.IDataReader r)
        {

            DataImportHdrInfo entity = new DataImportHdrInfo();
            entity.FileName = r.GetStringValue("FileName");
            entity.PhysicalPath = r.GetStringValue("PhysicalPath");
            entity.FileSize = r.GetDecimalValue("FileSize");

            return entity;
        }
        #endregion
        #region "Delete Tasks"
        public List<DataImportHdrInfo> DeleteTasks(List<long> SelectedTaskIDs, int CompanyID, string revisedBy, DateTime dateRevised)
        {
            IDbCommand oCmd = null;
            IDbConnection oConn = null;
            IDataReader reader = null;
            IDbTransaction oTrans = null;
            List<DataImportHdrInfo> oDataImportHdrInfoList = new List<DataImportHdrInfo>();
            try
            {
                oConn = this.CreateConnection();
                DataTable dtSelectedTaskIDs = ServiceHelper.ConvertLongIDCollectionToDataTable(SelectedTaskIDs);
                oCmd = this.GetDeleteTasksCommand(dtSelectedTaskIDs, CompanyID, revisedBy, dateRevised);
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                oTrans = oConn.BeginTransaction();
                oCmd.Transaction = oTrans;
                reader = oCmd.ExecuteReader();
                while (reader.Read())
                {
                    DataImportHdrInfo oDataImportHdrInfo = this.MapObjectDataImportHdr(reader);
                    oDataImportHdrInfoList.Add(oDataImportHdrInfo);
                }
                reader.Close();
                oTrans.Commit();
                oTrans.Dispose();
            }
            catch (Exception ex)
            {
                if (oTrans != null && oConn != null)
                {
                    oTrans.Rollback();
                    oTrans.Dispose();
                }
                throw ex;
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
            return oDataImportHdrInfoList;
        }
        private IDbCommand GetDeleteTasksCommand(DataTable dtSelectedTaskIDs, int CompanyID, string revisedBy, DateTime dateRevised)
        {
            IDbCommand oCommand = this.CreateCommand("[TaskMaster].[usp_DEL_TaskHdrPermanent]");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;

            IDbDataParameter paramudtTaskIDs = oCommand.CreateParameter();
            paramudtTaskIDs.ParameterName = "@udtTaskIDs";
            paramudtTaskIDs.Value = dtSelectedTaskIDs;

            IDbDataParameter paramCompanyID = oCommand.CreateParameter();
            paramCompanyID.ParameterName = "@CompanyID";
            paramCompanyID.Value = CompanyID;

            IDbDataParameter paramRevisedBy = oCommand.CreateParameter();
            paramRevisedBy.ParameterName = "@RevisedBy";
            paramRevisedBy.Value = revisedBy;

            IDbDataParameter paramDateRevised = oCommand.CreateParameter();
            paramDateRevised.ParameterName = "@DateRevised";
            paramDateRevised.Value = dateRevised;

            cmdParams.Add(paramudtTaskIDs);
            cmdParams.Add(paramCompanyID);
            cmdParams.Add(paramRevisedBy);
            cmdParams.Add(paramDateRevised);

            return oCommand;
        }
        #endregion

        public List<TaskHdrInfo> GetTasksByDataImportID(int DataImportID)
        {
            IDbCommand oCmd = null;
            IDbConnection oConn = null;
            IDataReader reader = null;
            List<TaskHdrInfo> oTaskHdrInfoList = new List<TaskHdrInfo>();
            try
            {
                oConn = this.CreateConnection();
                oCmd = this.GetTasksByDataImportIDCommand(DataImportID);
                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                reader = oCmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    TaskHdrInfo oTaskListinfo = this.MapObject(reader);
                    if (!string.IsNullOrEmpty(oTaskListinfo.TaskName))
                        oTaskHdrInfoList.Add(oTaskListinfo);
                }
                reader.ClearColumnHash();
                // Net Resultset for TaskOwners
                reader.NextResult();
                while (reader.Read())
                {
                    AddTaskOwners(oTaskHdrInfoList, reader);
                }
                reader.ClearColumnHash();
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
            return oTaskHdrInfoList;
        }

        private IDbCommand GetTasksByDataImportIDCommand(int DataImportID)
        {
            IDbCommand oCommand = this.CreateCommand("[TaskMaster].[usp_GET_TasksByDataImportID]");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;

            IDbDataParameter paramDataImportID = oCommand.CreateParameter();
            paramDataImportID.ParameterName = "@DataImportID";
            paramDataImportID.Value = DataImportID;

            cmdParams.Add(paramDataImportID);


            return oCommand;
        }

        public List<TaskHdrInfo> GetTaskInformationForCompanyAlertMail(int RecPeriodID, int UserID, short roleID, long CompanyAlertDetailID, int LanguageID)
        {
            IDbCommand oCmd = null;
            IDbConnection oConn = null;
            IDataReader reader = null;
            List<TaskHdrInfo> oTaskHdrInfoList = new List<TaskHdrInfo>();
            try
            {
                oConn = this.CreateConnection();
                oCmd = this.GetTaskInformationForCompanyAlertMailCommand(RecPeriodID, UserID, roleID, CompanyAlertDetailID, LanguageID);
                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                reader = oCmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    TaskHdrInfo oTaskListinfo = this.MapObject(reader);
                    oTaskHdrInfoList.Add(oTaskListinfo);
                }
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
            return oTaskHdrInfoList;
        }

        private IDbCommand GetTaskInformationForCompanyAlertMailCommand(int RecPeriodID, int UserID, short RoleID, long CompanyAlertDetailID, int LanguageID)
        {
            IDbCommand oCommand = this.CreateCommand("[TaskMaster].[usp_GET_TaskInformationForCompanyAlertMail]");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;

            IDbDataParameter paramRecPeriodID = oCommand.CreateParameter();
            paramRecPeriodID.ParameterName = "@RecPeriodID";
            paramRecPeriodID.Value = RecPeriodID;
            cmdParams.Add(paramRecPeriodID);

            IDbDataParameter paramUserID = oCommand.CreateParameter();
            paramUserID.ParameterName = "@UserID";
            paramUserID.Value = UserID;
            cmdParams.Add(paramUserID);

            IDbDataParameter paramRoleID = oCommand.CreateParameter();
            paramRoleID.ParameterName = "@RoleID";
            paramRoleID.Value = RoleID;
            cmdParams.Add(paramRoleID);

            IDbDataParameter paramCompanyAlertDetailID = oCommand.CreateParameter();
            paramCompanyAlertDetailID.ParameterName = "@CompanyAlertDetailID";
            paramCompanyAlertDetailID.Value = CompanyAlertDetailID;
            cmdParams.Add(paramCompanyAlertDetailID);

            IDbDataParameter paramLanguageID = oCommand.CreateParameter();
            paramLanguageID.ParameterName = "@LanguageID";
            paramLanguageID.Value = LanguageID;
            cmdParams.Add(paramLanguageID);


            return oCommand;
        }

        internal List<TaskCustomFieldInfo> GetTaskCustomFields(int? RecPeriodID, int? CompanyID)
        {
            IDbCommand oCmd = null;
            IDbConnection oConn = null;
            IDataReader reader = null;
            List<TaskCustomFieldInfo> oTaskCustomFieldInfoList = new List<TaskCustomFieldInfo>();
            try
            {
                oConn = this.CreateConnection();
                oCmd = this.GetGetTaskCustomFieldsCommand(RecPeriodID, CompanyID);
                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                reader = oCmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    TaskCustomFieldInfo oTaskCustomFieldInfo = this.MapTaskCustomFieldInfoObject(reader);
                    oTaskCustomFieldInfoList.Add(oTaskCustomFieldInfo);
                }
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
            return oTaskCustomFieldInfoList;
        }

        private TaskCustomFieldInfo MapTaskCustomFieldInfoObject(IDataReader r)
        {
            TaskCustomFieldInfo oTaskCustomFieldInfo = new TaskCustomFieldInfo();
            oTaskCustomFieldInfo.TaskCustomFieldID = r.GetInt16Value("TaskCustomFieldID");
            oTaskCustomFieldInfo.CustomField = r.GetStringValue("CustomField");
            oTaskCustomFieldInfo.CustomFieldLabelID = r.GetInt32Value("CustomFieldLabelID");
            oTaskCustomFieldInfo.CustomFieldValue = r.GetStringValue("CustomFieldValue");
            oTaskCustomFieldInfo.CustomFieldValueLabelID = r.GetInt32Value("CustomFieldValueLabelID");
            return oTaskCustomFieldInfo;
        }

        private IDbCommand GetGetTaskCustomFieldsCommand(int? RecPeriodID, int? CompanyID)
        {
            IDbCommand oCommand = this.CreateCommand("[TaskMaster].[usp_SEL_TaskCustomFieldValue]");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;

            IDbDataParameter paramRecPeriodID = oCommand.CreateParameter();
            paramRecPeriodID.ParameterName = "@RecPeriodID";
            if (RecPeriodID.HasValue)
                paramRecPeriodID.Value = RecPeriodID.Value;
            else
                paramRecPeriodID.Value = DBNull.Value;
            cmdParams.Add(paramRecPeriodID);

            IDbDataParameter paramCompanyID = oCommand.CreateParameter();
            paramCompanyID.ParameterName = "@CompanyID";
            if (CompanyID.HasValue)
                paramCompanyID.Value = CompanyID.Value;
            else
                paramCompanyID.Value = DBNull.Value;
            cmdParams.Add(paramCompanyID);

            return oCommand;
        }

        public void SaveTaskCustomFields(DataTable dtTaskCustomField, int? CompanyID, int? RecPeriodID, string ModifiedBy, DateTime? DateModified, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            IDbCommand cmd = this.CreateSaveTaskCustomFieldsCommand(dtTaskCustomField, CompanyID, RecPeriodID, ModifiedBy, DateModified);
            cmd.Connection = oConnection;
            cmd.Transaction = oTransaction;
            cmd.ExecuteNonQuery();
        }

        private IDbCommand CreateSaveTaskCustomFieldsCommand(DataTable dtTaskCustomField, int? CompanyID, int? RecPeriodID, string ModifiedBy, DateTime? DateModified)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("[TaskMaster].[usp_SAV_TaskCustomFieldValue]");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parTblTaskCustomFieldValue = cmd.CreateParameter();
            parTblTaskCustomFieldValue.ParameterName = "@TBLTaskCustomFieldValue";
            if (dtTaskCustomField != null)
                parTblTaskCustomFieldValue.Value = dtTaskCustomField;
            else
                parTblTaskCustomFieldValue.Value = System.DBNull.Value;
            cmdParams.Add(parTblTaskCustomFieldValue);

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (CompanyID.HasValue)
                parCompanyID.Value = CompanyID.Value;
            else
                parCompanyID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            if (RecPeriodID.HasValue)
                parRecPeriodID.Value = RecPeriodID.Value;
            else
                parRecPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parRecPeriodID);

            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            parAddedBy.Value = ModifiedBy;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (DateModified.HasValue)
                parDateAdded.Value = DateModified.Value;
            else
                parDateAdded.Value = DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            parRevisedBy.Value = ModifiedBy;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (DateModified.HasValue)
                parDateRevised.Value = DateModified.Value;
            else
                parDateRevised.Value = DBNull.Value;
            cmdParams.Add(parDateRevised);


            return cmd;
        }


        public List<TaskHdrInfo> GetAccessableTaskForBulkEdit(int userID, short roleID, int recPeriodID, ARTEnums.TaskType taskType
         , short? taskCategoryID, bool? isShowHidden, int? LanguageID, int? DefaultLanguageID)
        {
            IDbCommand oCmd = null;
            IDbConnection oConn = null;
            IDataReader reader = null;
            List<TaskHdrInfo> oTaskHdrInfoList = new List<TaskHdrInfo>();
            try
            {
                oConn = this.CreateConnection();
                oCmd = this.GetAccessableTaskForBulkEditCommand("[TaskMaster].[usp_GET_DeleteAccessableTaskByUserID]", userID, roleID, recPeriodID, taskType, null
                    , taskCategoryID, isShowHidden, false, LanguageID, DefaultLanguageID);
                oCmd.CommandType = CommandType.StoredProcedure;

                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                reader = oCmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    TaskHdrInfo oTaskListinfo = this.MapObject(reader);
                    if (!string.IsNullOrEmpty(oTaskListinfo.TaskName))
                        oTaskHdrInfoList.Add(oTaskListinfo);
                }
                reader.ClearColumnHash();
                // Net Resultset for TaskOwners
                reader.NextResult();
                while (reader.Read())
                {
                    AddTaskOwners(oTaskHdrInfoList, reader);
                }
                reader.ClearColumnHash();
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
            return oTaskHdrInfoList.FindAll(obj=>obj.TaskStatusID.GetValueOrDefault()==(short) ARTEnums.TaskStatus.NotStarted) ;
        }

        private IDbCommand GetAccessableTaskForBulkEditCommand(string USPName, int userID, short roleID, int recPeriodID, ARTEnums.TaskType taskType,
            List<long> AccountIDs, short? taskCategoryID, bool? isShowHidden, bool addAccountIDParam, int? LanguageID, int? DefaultLanguageID)
        {
            IDbCommand oCommand = this.CreateCommand(USPName);
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;

            IDbDataParameter paramUserID = oCommand.CreateParameter();
            paramUserID.ParameterName = "@UserID";
            paramUserID.Value = userID;

            IDbDataParameter paramRoleID = oCommand.CreateParameter();
            paramRoleID.ParameterName = "@RoleID";
            paramRoleID.Value = roleID;

            IDbDataParameter paramRecPeriodID = oCommand.CreateParameter();
            paramRecPeriodID.ParameterName = "@RecPeriodID";
            paramRecPeriodID.Value = recPeriodID;

            IDbDataParameter paramTaskTypeID = oCommand.CreateParameter();
            paramTaskTypeID.ParameterName = "@TaskTypeID";
            paramTaskTypeID.Value = Convert.ToInt16(taskType);

            IDbDataParameter paramTaskCategoryID = oCommand.CreateParameter();
            paramTaskCategoryID.ParameterName = "@TaskCategoryID";
            if (taskCategoryID.HasValue)
                paramTaskCategoryID.Value = taskCategoryID.Value;
            else
                paramTaskCategoryID.Value = System.DBNull.Value;

            IDbDataParameter paramIsShowHidden = oCommand.CreateParameter();
            paramIsShowHidden.ParameterName = "@IsShowHidden";
            paramIsShowHidden.Value = isShowHidden.Value;

            IDbDataParameter paramLanguageID = oCommand.CreateParameter();
            paramLanguageID.ParameterName = "@LCID";
            if (LanguageID.HasValue)
                paramLanguageID.Value = LanguageID.Value;
            else
                paramLanguageID.Value = System.DBNull.Value;

            IDbDataParameter paramDefualtLanguageID = oCommand.CreateParameter();
            paramDefualtLanguageID.ParameterName = "@DefaultLCID";
            if (DefaultLanguageID.HasValue)
                paramDefualtLanguageID.Value = LanguageID.Value;
            else
                paramDefualtLanguageID.Value = System.DBNull.Value;


            cmdParams.Add(paramUserID);
            cmdParams.Add(paramRoleID);
            cmdParams.Add(paramRecPeriodID);
            cmdParams.Add(paramTaskTypeID);
            cmdParams.Add(paramTaskCategoryID);
            cmdParams.Add(paramIsShowHidden);
            cmdParams.Add(paramLanguageID);
            cmdParams.Add(paramDefualtLanguageID);

            if (addAccountIDParam)
            {
                IDbDataParameter paramAccIDs = oCommand.CreateParameter();
                paramAccIDs.ParameterName = "@udtAccountID";
                paramAccIDs.Value = ServiceHelper.ConvertLongIDCollectionToDataTable(AccountIDs);
                cmdParams.Add(paramAccIDs);
            }



            return oCommand;
        }


    }




}