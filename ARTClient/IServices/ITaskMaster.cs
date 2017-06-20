using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Data;
using System.Data;

namespace SkyStem.ART.Client.IServices
{
    // NOTE: If you change the interface name "ITaskMaster" here, you must also update the reference to "ITaskMaster" in Web.config.
    [ServiceContract]
    public interface ITaskMaster
    {
        [OperationContract]
        List<TaskDetailReviewNoteDetailInfo> SelectAllCommentsByTaskDetailID(Int64? TaskDetailID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<TaskHdrInfo> GetAccessableTaskByUserID(int userID, short roleID, int recPeriodID, short taskCategoryID, ARTEnums.TaskType taskType, 
            List<TaskStatusMstInfo> taskStatusMstInfoList, List<FilterCriteria> filterCriteriaList
            , bool? isShowHidden, int? LanguageID, int? DefaultLanguageID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<TaskRecurrenceTypeMstInfo> GetTaskRecurrenceTypeMstInfoCollection(AppUserInfo oAppUserInfo);

        [OperationContract]
        List<TaskListHdrInfo> GetTaskListHdrInfoCollection(int recPeriodID, AppUserInfo oAppUserInfo);
        [OperationContract]
        List<TaskSubListHdrInfo> GetTaskSubListHdrInfoCollection(int recPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        DataTable AddTask(List<TaskHdrInfo> oTasksHdrList, int recPeriodID, string addedBy, DateTime dateAdded, List<AttachmentInfo> oAttachmentInfoCollection, AppUserInfo oAppUserInfo);

        [OperationContract]
        void AddTasksComment(List<long> TaskDetailIDs, string SubjectLine, string Comment, int AddedByUserID, string AddedBy, DateTime DateAdded, AppUserInfo oAppUserInfo);

        [OperationContract]
        TaskHdrInfo GetTaskHdrInfoByTaskID(long? TaskID, int? recPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        bool EditTask(List<TaskHdrInfo> oTasksHdrList, int recPeriodID, string revisedBy, DateTime dateRevised, AppUserInfo oAppUserInfo);

        [OperationContract]
        void AddTaskDetailStatus(List<long> TaskDetailIDs, SkyStem.ART.Client.Data.ARTEnums.TaskStatus TaskStatus, DateTime TaskStatusDate, int? AddedByUserID, AppUserInfo oAppUserInfo);

        [OperationContract]
        void UpdateTasksStatus(List<long> TaskDetailIDs, SkyStem.ART.Client.Data.ARTEnums.TaskActionType TaskAcion, int? RecPeriodID, string RevisedByUser, DateTime DateRevised, AppUserInfo oAppUserInfo);

        [OperationContract]
        void UpdateTaskStatusCommentAttachments(List<long> TaskDetailIDs, string AddedByUser, DateTime DateRevised, int? RecPeriodID, SkyStem.ART.Client.Data.ARTEnums.TaskActionType TaskAcion
            , string SubjectLine, string Comment, int? AddedByUserID, List<AttachmentInfo> AttachmentInfoList, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<TaskActionTypeTaskSatusInfo> GetTaskStatusByTaskActionTypeID(short TaskActionTypeID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<TaskHdrInfo> GetPendingOverdueAccessableTaskByUserID(int userID, short roleID, int recPeriodID, ARTEnums.TaskType taskType,
            List<FilterCriteria> filterCriteriaList, short? taskCategoryID, bool? isShowHidden, int? LanguageID, int? DefaultLanguageID, AppUserInfo oAppUserInfo, short? TaskCompletionStatusID, bool? isShowAllTask);

        [OperationContract]
        List<TaskHdrInfo> GetCompletedAccessableTaskByUserID(int userID, short roleID, int recPeriodID, ARTEnums.TaskType taskType,
            List<FilterCriteria> filterCriteriaList, short? taskCategoryID, bool? isShowHidden, int? LanguageID, int? DefaultLanguageID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<TaskHdrInfo> GetApproverAccessableTaskByUserID(int userID, short roleID, int recPeriodID, ARTEnums.TaskType taskType, List<long> AccountIDs
            , short? taskCategoryID, bool? isShowHidden, int? LanguageID, int? DefaultLanguageID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<TaskHdrInfo> GetAccessibleTasksByActionTypeID(int userID, short roleID, int recPeriodID, ARTEnums.TaskType taskType, ARTEnums.TaskActionType taskActionType, List<long> AccountIDs
            , short? taskCategoryID, bool? isShowHidden, int? LanguageID, int? DefaultLanguageID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<TaskHdrInfo> GetAssingneeAccessableTaskByUserID(int userID, short roleID, int recPeriodID, ARTEnums.TaskType taskType, List<long> AccountIDs
            , short? taskCategoryID, bool? isShowHidden, int? LanguageID, int? DefaultLanguageID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<TaskHdrInfo> GetDeleteAccessableTaskByUserID(int userID, short roleID, int recPeriodID, ARTEnums.TaskType taskType, short? taskCategoryID
            , bool? isShowHidden, int? LanguageID, int? DefaultLanguageID, AppUserInfo oAppUserInfo);
        [OperationContract]
        List<TaskHdrInfo> GetAccessableTaskForBulkEdit(int userID, short roleID, int recPeriodID, ARTEnums.TaskType taskType, short? taskCategoryID
            , bool? isShowHidden, int? LanguageID, int? DefaultLanguageID, AppUserInfo oAppUserInfo);

        [OperationContract]
        void DeleteAccessibleTasks(List<long> TaskIDs, int RecPeriodID, string ModifiedBy, DateTime DateModified, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<TaskHdrInfo> GetAccessableTaskByAccountIDs(int userID, short roleID, int recPeriodID, List<long> AccountIDs, short? taskCategoryID,
            bool? isShowHidden, int? LanguageID, int? DefaultLanguageID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<TaskCategoryMstInfo> GetTaskCategory(AppUserInfo oAppUserInfo);

        [OperationContract]
        void AddEditUserTaskVisibility(List<TaskHdrInfo> oTasksHdrList, int? CurrentUserID, int? recPeriodID, string addedBy, string revisedBy, bool isHidden, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<int> GetRecurrenceFrequencyByTaskID(long? TaskID, int? recPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<TaskAttributeValueInfo> GetTaskAttributeList(int recPeriodID, List<long> taskIDs, List<short> attributeIDs, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<TaskStatusMstInfo> GetTaskStatus(AppUserInfo oAppUserInfo);

        [OperationContract]
        bool BulkEditTask(List<TaskHdrInfo> oTasksHdrList, int recPeriodID, string revisedBy, DateTime dateRevised, AppUserInfo oAppUserInfo);
        [OperationContract]
        bool EditTaskList(int? TasksListID, string TasksListName, string revisedBy, DateTime dateRevised, AppUserInfo oAppUserInfo);
        [OperationContract]
        int GetTaskListIDByName(string TaskListName, int? CompanyID, AppUserInfo oAppUserInfo);
        [OperationContract]
        bool EditTaskSubList(int? TasksSubListID, string TasksSubListName, string revisedBy, DateTime dateRevised, AppUserInfo oAppUserInfo);
        [OperationContract]
        int GetTaskSubListIDByName(string TaskSubListName, int? CompanyID, AppUserInfo oAppUserInfo);
        [OperationContract]
        List<DataImportHdrInfo> DeleteTaskLoad(int DataImportID, string revisedBy, DateTime dateRevised, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<DataImportHdrInfo> DeleteTasks(List<long> SelectedTaskIDs, int CompanyID, string revisedBy, DateTime dateRevised, AppUserInfo oAppUserInfo);
        [OperationContract]
        List<TaskHdrInfo> GetTasksByDataImportID(int DataImportID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<TaskTypeMstInfo> GetAllTaskType(AppUserInfo oAppUserInfo);
        [OperationContract]
        List<TaskHdrInfo> GetTaskInformationForCompanyAlertMail(int RecPeriodID, int UserID, short roleID, long CompanyAlertDetailID, int LanguageID, AppUserInfo oAppUserInfo);
        [OperationContract]
        List<TaskCustomFieldInfo> GetTaskCustomFields(int? RecPeriodID, int? CompanyID, AppUserInfo oAppUserInfo);
        [OperationContract]
        void SaveTaskCustomFields(List<TaskCustomFieldInfo> oTaskCustomFieldInfoList, int? CompanyID, int? RecPeriodID, string ModifiedBy, DateTime? DateModified, AppUserInfo oAppUserInfo);
    }
}
