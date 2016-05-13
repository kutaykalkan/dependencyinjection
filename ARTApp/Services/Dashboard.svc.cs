using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.DAO;
using System.Data.SqlClient;
using SkyStem.ART.App.Utility;

namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "Dashboard" here, you must also update the reference to "Dashboard" in Web.config.
    public class Dashboard : IDashboard
    {
        public ReconciledAccountCountBalanceInfo GetReconciliableAccessibleAccounts(int UserID, short RoleID, int RecPeriodID, AppUserInfo oAppUserInfo)
        {
            ReconciledAccountCountBalanceInfo oReconciledAccountCount_BalanceInfo = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DashboardMstDAO oDashboardMstDAO = new DashboardMstDAO(oAppUserInfo);
                oReconciledAccountCount_BalanceInfo = oDashboardMstDAO.SelectUserReconciliableAccesibleAccount(UserID, RoleID, RecPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oReconciledAccountCount_BalanceInfo;
        }

        public AssignedAccountCountInfo GetAssignedAccountCount(int UserID, short RoleID, int RecPeriodID, AppUserInfo oAppUserInfo)
        {
            AssignedAccountCountInfo oAssignedAccountCountInfo = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DashboardMstDAO oDashboardMstDAO = new DashboardMstDAO(oAppUserInfo);
                oAssignedAccountCountInfo = oDashboardMstDAO.SelectAssignedAccountCount(UserID, RoleID, RecPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oAssignedAccountCountInfo;
        }

        public List<AccountOwnershipStatisticsInfo> GetAccountOwnershipStatistics(int UserID, short RoleID, int RecPeriodID, AppUserInfo oAppUserInfo)
        {
            List<AccountOwnershipStatisticsInfo> oAccountOwnershipStatisticsInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DashboardMstDAO oDashboardMstDAO = new DashboardMstDAO(oAppUserInfo);
                oAccountOwnershipStatisticsInfoCollection = oDashboardMstDAO.SelectAccountOwnershipStatistics(UserID, RoleID, RecPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oAccountOwnershipStatisticsInfoCollection;
        }

        public List<AccountOwnershipStatisticsInfo> GetAccountOwnershipStatisticsSecondLevel(int UserID, short RoleID, int RecPeriodID, int? SelectedUserID, AppUserInfo oAppUserInfo)
        {
            List<AccountOwnershipStatisticsInfo> oAccountOwnershipStatisticsInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DashboardMstDAO oDashboardMstDAO = new DashboardMstDAO(oAppUserInfo);
                oAccountOwnershipStatisticsInfoCollection = oDashboardMstDAO.SelectAccountOwnershipStatisticsSecondLevel(UserID, RoleID, RecPeriodID, SelectedUserID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oAccountOwnershipStatisticsInfoCollection;
        }


        public List<AccountOwnershipStatisticsInfo> GetAccountOwnershipStatisticsThirdLevel(int UserID, short RoleID, int RecPeriodID, int? SelectedUserID, int? SelectedUserIDSecondLevel, AppUserInfo oAppUserInfo)
        {
            List<AccountOwnershipStatisticsInfo> oAccountOwnershipStatisticsInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DashboardMstDAO oDashboardMstDAO = new DashboardMstDAO(oAppUserInfo);
                oAccountOwnershipStatisticsInfoCollection = oDashboardMstDAO.SelectAccountOwnershipStatisticsThirdLevel(UserID, RoleID, RecPeriodID, SelectedUserID, SelectedUserIDSecondLevel);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oAccountOwnershipStatisticsInfoCollection;
        }


        public ReconciliationTrackingInfo GetReconciliationTracking(int UserID, short RoleID, int RecPeriodID, AppUserInfo oAppUserInfo)
        {
            ReconciliationTrackingInfo oReconciliationTrackingInfo = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DashboardMstDAO oDashboardMstDAO = new DashboardMstDAO(oAppUserInfo);
                oReconciliationTrackingInfo = oDashboardMstDAO.SelectReconciliationTracking(UserID, RoleID, RecPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oReconciliationTrackingInfo;
        }

        #region IDashboard Members

        public DashboardExceptionInfo GetExceptionsByFSCaptionAndNetAccount(int? UserID, short? RoleID, int? RecPeriodID, AppUserInfo oAppUserInfo)
        {
            DashboardExceptionInfo oDashboardExceptionInfo = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DashboardMstDAO oDashboardMstDAO = new DashboardMstDAO(oAppUserInfo);
                oDashboardExceptionInfo = oDashboardMstDAO.GetExceptionsByFSCaptionAndNetAccount(UserID, RoleID, RecPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oDashboardExceptionInfo;
            
        }

        #endregion



        public List<IncompleteAttributeInfo> GetIncompleteAttributeList(int? UserID, short? RoleID, int? RecPeriodID, AppUserInfo oAppUserInfo)
        {
            List<IncompleteAttributeInfo> oIncompleteAttributeInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DashboardMstDAO oDashboardMstDAO = new DashboardMstDAO(oAppUserInfo);
                oIncompleteAttributeInfoCollection = oDashboardMstDAO.SelectIncompleteAttributeList(UserID, RoleID, RecPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oIncompleteAttributeInfoCollection;


        }


        public List<ReconciliationStatusFSCaptionInfo> GetReconciliationStatusByFSCaption(int? UserID, short? RoleID, int? RecPeriodID, AppUserInfo oAppUserInfo)
        {
            List<ReconciliationStatusFSCaptionInfo> oReconciliationStatusFSCaptionInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DashboardMstDAO oDashboardMstDAO = new DashboardMstDAO(oAppUserInfo);
                oReconciliationStatusFSCaptionInfoCollection = oDashboardMstDAO.GetReconciliationStatusByFSCaption(UserID, RoleID, RecPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oReconciliationStatusFSCaptionInfoCollection;



        }


        public List<OpenItemStatusInfo> GetOpenItemList(int UserID, short RoleID, int RecPeriodID, AppUserInfo oAppUserInfo)
        {
            List<OpenItemStatusInfo> oOpenItemInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DashboardMstDAO oDashboardMstDAO = new DashboardMstDAO(oAppUserInfo);
                oOpenItemInfoCollection = oDashboardMstDAO.SelectOpenItemStatusInfo(UserID, RoleID, RecPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oOpenItemInfoCollection;

        }

        public List<TaskCompletionStatusMstInfo> GetTaskCompletionStatusCount(int? UserID, int RoleID, int RecPeriodID, AppUserInfo oAppUserInfo)
        {
            List<TaskCompletionStatusMstInfo> oTaskCompletionStatusMstInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskCompletionStatusMstDAO oTaskCompletionStatusMstDAO = new TaskCompletionStatusMstDAO(oAppUserInfo);
                oTaskCompletionStatusMstInfoCollection = oTaskCompletionStatusMstDAO.GetTaskCompletionStatusCount(UserID, RoleID, RecPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oTaskCompletionStatusMstInfoCollection;

        }
        public List<TaskStatusCountInfo> GetTaskStatusCountByMonth(int? UserID, int? RoleID, int? RecPeriodID,DateTime CurrentDate, AppUserInfo oAppUserInfo)
        {
            List<TaskStatusCountInfo> oTaskStatusCountInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskCompletionStatusMstDAO oTaskCompletionStatusMstDAO = new TaskCompletionStatusMstDAO(oAppUserInfo);
                oTaskStatusCountInfoCollection = oTaskCompletionStatusMstDAO.GetTaskStatusCountByMonth(UserID, RoleID, RecPeriodID, CurrentDate);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oTaskStatusCountInfoCollection;

        }

    }
}

