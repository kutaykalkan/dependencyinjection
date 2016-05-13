using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SkyStem.ART.Web.Utility
{
    /// <summary>
    /// Summary description for DashboardHelper
    /// </summary>
    public class DashboardHelper
    {
        private DashboardHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static List<AccountOwnershipStatisticsInfo> GetDataForAccountOwnershipStatistics(int userID, short roleID, int recPeriodID, AppUserInfo oAppUserInfo)
        {
            IDashboard oDashboardClient = RemotingHelper.GetDashboardObject();
            return oDashboardClient.GetAccountOwnershipStatistics(userID, roleID, recPeriodID, oAppUserInfo);
        }

        public static Task<List<AccountOwnershipStatisticsInfo>> GetDataForAccountOwnershipStatisticsAsync(int userID, short roleID, int recPeriodID, AppUserInfo oAppUserInfo)
        {
            Task<List<AccountOwnershipStatisticsInfo>> resultTask = Task.Factory.StartNew(() =>
            {
                return GetDataForAccountOwnershipStatistics(userID, roleID, recPeriodID, oAppUserInfo);
            });
            return resultTask;
        }

        public static List<AccountOwnershipStatisticsInfo> GetDataForAccountOwnershipStatisticsSecondLevel(int userID, short roleID, int recPeriodID, int? SelectedUserID, AppUserInfo oAppUserInfo)
        {
            IDashboard oDashboardClient = RemotingHelper.GetDashboardObject();
            return oDashboardClient.GetAccountOwnershipStatisticsSecondLevel(userID, roleID, recPeriodID, SelectedUserID, oAppUserInfo);
        }

        public static List<AccountOwnershipStatisticsInfo> GetDataForAccountOwnershipStatisticsThirdLevel(int userID, short roleID, int recPeriodID, int? SelectedUserID, int? SelectedUserIDSecondLevel, AppUserInfo oAppUserInfo)
        {
            IDashboard oDashboardClient = RemotingHelper.GetDashboardObject();
            return oDashboardClient.GetAccountOwnershipStatisticsThirdLevel(userID, roleID, recPeriodID, SelectedUserID, SelectedUserIDSecondLevel, oAppUserInfo);
        }

        public static ReconciledAccountCountBalanceInfo GetDataForAccountReconciliationCoverage(int userID, short roleID, int recPeriodID, AppUserInfo oAppUserInfo)
        {
            IDashboard oDashboardClient = RemotingHelper.GetDashboardObject();
            return oDashboardClient.GetReconciliableAccessibleAccounts(userID, roleID, recPeriodID, oAppUserInfo);
        }
        public static Task<ReconciledAccountCountBalanceInfo> GetDataForAccountReconciliationCoverageAsync(int userID, short roleID, int recPeriodID, AppUserInfo oAppUserInfo)
        {
            Task<ReconciledAccountCountBalanceInfo> resultTask = Task.Factory.StartNew(() =>
            {
                return GetDataForAccountReconciliationCoverage(userID, roleID, recPeriodID, oAppUserInfo);
            });
            return resultTask;
        }
        public static DashboardExceptionInfo GetDataForExceptionsByFSCaption(int? userID, short? roleID, int? recPeriodID, AppUserInfo oAppUserInfo)
        {
            IDashboard oDashboardClient = RemotingHelper.GetDashboardObject();
            return oDashboardClient.GetExceptionsByFSCaptionAndNetAccount(userID, roleID, recPeriodID, oAppUserInfo);
        }

        public static Task<DashboardExceptionInfo> GetDataForExceptionsByFSCaptionAsync(int? userID, short? roleID, int? recPeriodID, AppUserInfo oAppUserInfo)
        {
            Task<DashboardExceptionInfo> resultTask = Task.Factory.StartNew(() =>
            {
                return GetDataForExceptionsByFSCaption(userID, roleID, recPeriodID, oAppUserInfo);
            });
            return resultTask;
        }

        public static List<OpenItemStatusInfo> GetDataForOpenItemList(int userID, short roleID, int recPeriodID, AppUserInfo oAppUserInfo)
        {
            IDashboard oDashboardClient = RemotingHelper.GetDashboardObject();
            return oDashboardClient.GetOpenItemList(userID, roleID, recPeriodID, oAppUserInfo);
        }
        public static Task<List<OpenItemStatusInfo>> GetDataForOpenItemListAsync(int userID, short roleID, int recPeriodID, AppUserInfo oAppUserInfo)
        {
            Task<List<OpenItemStatusInfo>> resultTask = Task.Factory.StartNew(() =>
            {
                return GetDataForOpenItemList(userID, roleID, recPeriodID, oAppUserInfo);
            });
            return resultTask;
        }

        public static List<ReconciliationStatusFSCaptionInfo> GetDataForReconciliationStatusByFSCaption(int? userID, short? roleID, int? recPeriodID, AppUserInfo oAppUserInfo)
        {
            IDashboard oDashboardClient = RemotingHelper.GetDashboardObject();
            return oDashboardClient.GetReconciliationStatusByFSCaption(userID, roleID, recPeriodID, oAppUserInfo);
        }
        public static Task<List<ReconciliationStatusFSCaptionInfo>> GetDataForReconciliationStatusByFSCaptionAsync(int? userID, short? roleID, int? recPeriodID, AppUserInfo oAppUserInfo)
        {
            Task<List<ReconciliationStatusFSCaptionInfo>> resultTask = Task.Factory.StartNew(() =>
            {
                return GetDataForReconciliationStatusByFSCaption(userID, roleID, recPeriodID, oAppUserInfo);
            });
            return resultTask;
        }

        public static ReconciliationTrackingInfo GetDataForReconciliationTracking(int userID, short roleID, int recPeriodID, AppUserInfo oAppUserInfo)
        {
            IDashboard oDashboardClient = RemotingHelper.GetDashboardObject();
            return oDashboardClient.GetReconciliationTracking(userID, roleID, recPeriodID, oAppUserInfo);
        }
        public static Task<ReconciliationTrackingInfo> GetDataForReconciliationTrackingAsync(int userID, short roleID, int recPeriodID, AppUserInfo oAppUserInfo)
        {
            Task<ReconciliationTrackingInfo> resultTask = Task.Factory.StartNew(() =>
            {
                return GetDataForReconciliationTracking(userID, roleID, recPeriodID, oAppUserInfo);
            });
            return resultTask;
        }

        public static List<TaskCompletionStatusMstInfo> GetDataForTaskCompletionStatusCount(int? userID, short roleID, int recPeriodID, AppUserInfo oAppUserInfo)
        {
            IDashboard oDashboardClient = RemotingHelper.GetDashboardObject();
            return oDashboardClient.GetTaskCompletionStatusCount(userID, roleID, recPeriodID, oAppUserInfo);
        }
        public static Task<List<TaskCompletionStatusMstInfo>> GetDataForTaskCompletionStatusCountAsync(int? userID, short roleID, int recPeriodID, AppUserInfo oAppUserInfo)
        {
            Task<List<TaskCompletionStatusMstInfo>> resultTask = Task.Factory.StartNew(() =>
            {
                return GetDataForTaskCompletionStatusCount(userID, roleID, recPeriodID, oAppUserInfo);
            });
            return resultTask;
        }

        public static List<TaskStatusCountInfo> GetDataForTaskStatusCountByMonth(int? userID, short? roleID, int? recPeriodID, DateTime currentDate, AppUserInfo oAppUserInfo)
        {
            IDashboard oDashboardClient = RemotingHelper.GetDashboardObject();
            return oDashboardClient.GetTaskStatusCountByMonth(userID, roleID, recPeriodID, currentDate, oAppUserInfo);
        }
        public static Task<List<TaskStatusCountInfo>> GetDataForTaskStatusCountByMonthAsync(int? userID, short? roleID, int? recPeriodID, DateTime currentDate, AppUserInfo oAppUserInfo)
        {
            Task<List<TaskStatusCountInfo>> resultTask = Task.Factory.StartNew(() =>
            {
                return GetDataForTaskStatusCountByMonth(userID, roleID, recPeriodID, currentDate, oAppUserInfo);
            });
            return resultTask;
        }

        public static AssignedAccountCountInfo GetDataForUnassignedAccountOwnership(int userID, short roleID, int recPeriodID, AppUserInfo oAppUserInfo)
        {
            IDashboard oDashboardClient = RemotingHelper.GetDashboardObject();
            return oDashboardClient.GetAssignedAccountCount(userID, roleID, recPeriodID, oAppUserInfo);
        }
        public static Task<AssignedAccountCountInfo> GetDataForUnassignedAccountOwnershipAsync(int userID, short roleID, int recPeriodID, AppUserInfo oAppUserInfo)
        {
            Task<AssignedAccountCountInfo> resultTask = Task.Factory.StartNew(() =>
            {
                return GetDataForUnassignedAccountOwnership(userID, roleID, recPeriodID, oAppUserInfo);
            });
            return resultTask;
        }

        public static int? GetTotalAccountsCount(int userID, short roleID, int recPeriodID, AppUserInfo oAppUserInfo)
        {
            IUser oUserClient = RemotingHelper.GetUserObject();
            return oUserClient.GetTotalAccountsCount(userID, roleID, recPeriodID, oAppUserInfo);
        }
        public static List<IncompleteAttributeInfo> GetIncompleteAttributeList(int? userID, short? roleID, int? recPeriodID, AppUserInfo oAppUserInfo)
        {
            IDashboard oDashboardClient = RemotingHelper.GetDashboardObject();
            return oDashboardClient.GetIncompleteAttributeList(userID, roleID, recPeriodID, oAppUserInfo);
        }
        public static Task<List<IncompleteAttributeInfo>> GetIncompleteAttributeListAsync(int? userID, short? roleID, int? recPeriodID, AppUserInfo oAppUserInfo)
        {
            Task<List<IncompleteAttributeInfo>> resultTask = Task.Factory.StartNew(() =>
            {
                return GetIncompleteAttributeList(userID, roleID, recPeriodID, oAppUserInfo);
            });
            return resultTask;
        }
      
    }
}