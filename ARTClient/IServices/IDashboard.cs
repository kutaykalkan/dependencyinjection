using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.Client.IServices
{
    // NOTE: If you change the interface name "IDashboard" here, you must also update the reference to "IDashboard" in Web.config.
    [ServiceContract]
    public interface IDashboard
    {
        [OperationContract]
        ReconciledAccountCountBalanceInfo GetReconciliableAccessibleAccounts(int UserID, short RoleID, int RecPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        AssignedAccountCountInfo GetAssignedAccountCount(int UserID, short RoleID, int RecPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<AccountOwnershipStatisticsInfo> GetAccountOwnershipStatistics(int UserID, short RoleID, int RecPeriodID, AppUserInfo oAppUserInfo);
        [OperationContract]

        List<AccountOwnershipStatisticsInfo> GetAccountOwnershipStatisticsSecondLevel(int UserID, short RoleID, int RecPeriodID, int? SelectedUserID, AppUserInfo oAppUserInfo);
        [OperationContract]
        List<AccountOwnershipStatisticsInfo> GetAccountOwnershipStatisticsThirdLevel(int UserID, short RoleID, int RecPeriodID, int? SelectedUserID, int? SelectedUserIDSecondLevel, AppUserInfo oAppUserInfo);

        [OperationContract]
        ReconciliationTrackingInfo GetReconciliationTracking(int UserID, short RoleID, int RecPeriodID, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Function to Get Exceptions By FS Caption
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="RoleID"></param>
        /// <param name="RecPeriodID"></param>
        /// <returns></returns>
        [OperationContract]
        DashboardExceptionInfo GetExceptionsByFSCaptionAndNetAccount(int? UserID, short? RoleID, int? RecPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<IncompleteAttributeInfo> GetIncompleteAttributeList(int? UserID, short? RoleID, int? RecPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<ReconciliationStatusFSCaptionInfo> GetReconciliationStatusByFSCaption(int? UserID, short? RoleID, int? RecPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<OpenItemStatusInfo> GetOpenItemList(int UserID, short RoleID, int RecPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<TaskCompletionStatusMstInfo> GetTaskCompletionStatusCount(int? UserID, int RoleID, int RecPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<TaskStatusCountInfo> GetTaskStatusCountByMonth(int? UserID, int? RoleID, int? RecPeriodID, DateTime CurrentDate, AppUserInfo oAppUserInfo);

    }
}
