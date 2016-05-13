using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Model.Base;


namespace SkyStem.ART.App.DAO
{
    public class DashboardMstDAO : DashboardMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DashboardMstDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        internal List<DashboardMstInfo> GetDashboardsByRole(short? RoleID, int? RecPeriodID)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr = null;
            List<DashboardMstInfo> oDashboardMstInfoCollection = null;

            try
            {
                cmd = CreateGetDashboardsByRoleCommand(RoleID, RecPeriodID);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                oDashboardMstInfoCollection = MapObjects(dr);
                dr.ClearColumnHash();
            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                }

                if (cmd != null)
                {
                    if (cmd.Connection != null)
                    {
                        if (cmd.Connection.State != ConnectionState.Closed)
                        {
                            cmd.Connection.Close();
                            cmd.Connection.Dispose();
                        }
                    }
                    cmd.Dispose();
                }

            }
            return oDashboardMstInfoCollection;

        }

        private IDbCommand CreateGetDashboardsByRoleCommand(short? RoleID, int? RecPeriodID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_DashboardByRole");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            if (RoleID.HasValue)
                parRoleID.Value = RoleID.Value;
            else
                parRoleID.Value = DBNull.Value;
            cmdParams.Add(parRoleID);

            IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            if (RecPeriodID.HasValue)
                parRecPeriodID.Value = RecPeriodID.Value;
            else
                parRecPeriodID.Value = DBNull.Value;
            cmdParams.Add(parRecPeriodID);

            return cmd;
        }


        #region  SelectUserReconciliableAccesibleAccount (Account Reconciliation Coverage)
        //******************************************************************************
        //******Added by: Prafull
        //******Added on: 03-Jun-2010
        //******Purpose:Get the No of Reconciliable Accessible Account and its balance along with the total no.of Account and total Balance

        public ReconciledAccountCountBalanceInfo SelectUserReconciliableAccesibleAccount(int userID, short userRoleID, int recPeriodID)
        {
            ReconciledAccountCountBalanceInfo oReconciledAccountCountBalanceInfo = new ReconciledAccountCountBalanceInfo();
            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateSelectUserReconciliableAccesibleAccount(userID, userRoleID, recPeriodID);
                cmd.Connection = con;
                con.Open();
                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                oReconciledAccountCountBalanceInfo = null;
                while (reader.Read())
                {
                    oReconciledAccountCountBalanceInfo = MapReconciledAccountBalanceInfoObject(reader);
                }
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oReconciledAccountCountBalanceInfo;
        }

        private IDbCommand CreateSelectUserReconciliableAccesibleAccount(int userID, short userRoleID, int recPeriodID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_AccountReconciliationCoverage");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdRecPeriodId = cmd.CreateParameter();
            cmdRecPeriodId.ParameterName = "@RecPeriodID";
            cmdRecPeriodId.Value = recPeriodID;
            cmdParams.Add(cmdRecPeriodId);

            IDbDataParameter cmdUserId = cmd.CreateParameter();
            cmdUserId.ParameterName = "@UserID";
            cmdUserId.Value = userID;
            cmdParams.Add(cmdUserId);

            IDbDataParameter cmdUserRoleId = cmd.CreateParameter();
            cmdUserRoleId.ParameterName = "@RoleID";
            cmdUserRoleId.Value = userRoleID;
            cmdParams.Add(cmdUserRoleId);
            return cmd;
        }

        private ReconciledAccountCountBalanceInfo MapReconciledAccountBalanceInfoObject(IDataReader r)
        {
            ReconciledAccountCountBalanceInfo entity = new ReconciledAccountCountBalanceInfo();
            entity.TotalAccounts = r.GetInt32Value("TotalAccounts");
            entity.TotalReconciledAccounts = r.GetInt32Value("TotalAccountsReconciled");

            entity.TotalAccountGLBalance = r.GetDecimalValue("TotalAccountsAmount");
            entity.TotalReconciledAccountGLBalance = r.GetDecimalValue("ReconciledAccountsAmount");

            entity.BaseCurrencyCode = r.GetStringValue("BaseCurrencyCode");
            entity.ReportingCurrencyCode = r.GetStringValue("ReportingCurrencyCode");

            return entity;
        }
        #endregion

        #region  SelectUserAssignedAccount
        //******************************************************************************
        //******Added by : Prafull
        //******Added on : 03-Jun-2010
        //******Purpose:Get the No of Reconciliable Accessible Account and its balance along with the total no. of Account and total Balance 

        public AssignedAccountCountInfo SelectAssignedAccountCount(int userID, short userRoleID, int recPeriodID)
        {
            AssignedAccountCountInfo oAssignedAccountCountInfo = new AssignedAccountCountInfo();
            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateSelectAssignedAccountCount(userID, userRoleID, recPeriodID);
                cmd.Connection = con;
                con.Open();
                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                oAssignedAccountCountInfo = null;
                while (reader.Read())
                {
                    oAssignedAccountCountInfo = MapAssignedAccountCountInfoObject(reader);
                }
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oAssignedAccountCountInfo;
        }

        private IDbCommand CreateSelectAssignedAccountCount(int userID, short userRoleID, int recPeriodID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_AssignedAccountsCount");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdRecPeriodId = cmd.CreateParameter();
            cmdRecPeriodId.ParameterName = "@RecPeriodID";
            cmdRecPeriodId.Value = recPeriodID;
            cmdParams.Add(cmdRecPeriodId);

            IDbDataParameter cmdUserId = cmd.CreateParameter();
            cmdUserId.ParameterName = "@UserID";
            cmdUserId.Value = userID;
            cmdParams.Add(cmdUserId);

            IDbDataParameter cmdUserRoleId = cmd.CreateParameter();
            cmdUserRoleId.ParameterName = "@RoleID";
            cmdUserRoleId.Value = userRoleID;
            cmdParams.Add(cmdUserRoleId);
            return cmd;
        }

        private AssignedAccountCountInfo MapAssignedAccountCountInfoObject(IDataReader r)
        {
            AssignedAccountCountInfo entity = new AssignedAccountCountInfo();
            entity.TotalAccounts = r.GetInt32Value("TotalAccount");
            entity.TotalAssignedAccounts = r.GetInt32Value("AssignedAccount");
            return entity;
        }
        #endregion

        #region AccountOwnershipStatistics
        //******************************************************************************
        //******Added by : Prafull
        //******Added on : 11-Jun-2010
        //******Purpose:Get the Account Ownership Statistics

        public List<AccountOwnershipStatisticsInfo> SelectAccountOwnershipStatistics(int userID, short userRoleID, int recPeriodID)
        {
            List<AccountOwnershipStatisticsInfo> oAccountOwnershipStatisticsInfoCollection = new List<AccountOwnershipStatisticsInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateSelectAccountOwnershipStatistics(userID, userRoleID, recPeriodID);
                cmd.Connection = con;
                con.Open();
                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    AccountOwnershipStatisticsInfo oAccountOwnershipStatisticsInfo = null;
                    oAccountOwnershipStatisticsInfo = this.MapAccountOwnershipStatistics(reader);
                    oAccountOwnershipStatisticsInfoCollection.Add(oAccountOwnershipStatisticsInfo);
                }
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }
            return oAccountOwnershipStatisticsInfoCollection;
        }

        private IDbCommand CreateSelectAccountOwnershipStatistics(int userID, short userRoleID, int recPeriodID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_AccountOwnerShipStatistics");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdRecPeriodId = cmd.CreateParameter();
            cmdRecPeriodId.ParameterName = "@RecPeriodID";
            cmdRecPeriodId.Value = recPeriodID;
            cmdParams.Add(cmdRecPeriodId);

            IDbDataParameter cmdUserId = cmd.CreateParameter();
            cmdUserId.ParameterName = "@UserID";
            cmdUserId.Value = userID;
            cmdParams.Add(cmdUserId);

            IDbDataParameter cmdUserRoleId = cmd.CreateParameter();
            cmdUserRoleId.ParameterName = "@RoleID";
            cmdUserRoleId.Value = userRoleID;
            cmdParams.Add(cmdUserRoleId);
            return cmd;
        }

        private AccountOwnershipStatisticsInfo MapAccountOwnershipStatistics(IDataReader r)
        {
            AccountOwnershipStatisticsInfo entity = new AccountOwnershipStatisticsInfo();
            entity.FirstName = r.GetStringValue("FirstName");
            entity.LastName = r.GetStringValue("LastName");
            entity.UserID = r.GetInt32Value("UserID");
            entity.RoleID = r.GetInt32Value("RoleID");
            entity.NoOfAccounts = r.GetInt32Value("NoOfAccount");

            ////GetDisplayPercentageValue


            return entity;
        }


        #endregion


        #region AccountOwnershipStatisticsSecondLevel
        //******************************************************************************
        //******Added by : Prafull
        //******Added on : 11-Jun-2010
        //******Purpose:Get the Account Ownership Statistics Second Level

        public List<AccountOwnershipStatisticsInfo> SelectAccountOwnershipStatisticsSecondLevel(int userID, short userRoleID, int recPeriodID, int? SelectedUserID)
        {

            List<AccountOwnershipStatisticsInfo> oAccountOwnershipStatisticsInfoCollection = new List<AccountOwnershipStatisticsInfo>();

            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateSelectAccountOwnershipStatisticsSecondLevel(userID, userRoleID, recPeriodID, SelectedUserID);
                cmd.Connection = con;
                con.Open();
                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    AccountOwnershipStatisticsInfo oAccountOwnershipStatisticsInfo = null;
                    oAccountOwnershipStatisticsInfo = this.MapAccountOwnershipStatistics(reader);
                    oAccountOwnershipStatisticsInfoCollection.Add(oAccountOwnershipStatisticsInfo);
                }


            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oAccountOwnershipStatisticsInfoCollection;
        }

        private IDbCommand CreateSelectAccountOwnershipStatisticsSecondLevel(int userID, short userRoleID, int recPeriodID, int? SelectedUserID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_AccountOwnerShipStatisticsSecondLevel");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdRecPeriodId = cmd.CreateParameter();
            cmdRecPeriodId.ParameterName = "@RecPeriodID";
            cmdRecPeriodId.Value = recPeriodID;
            cmdParams.Add(cmdRecPeriodId);

            IDbDataParameter cmdUserId = cmd.CreateParameter();
            cmdUserId.ParameterName = "@UserID";
            cmdUserId.Value = userID;
            cmdParams.Add(cmdUserId);

            IDbDataParameter cmdUserRoleId = cmd.CreateParameter();
            cmdUserRoleId.ParameterName = "@RoleID";
            cmdUserRoleId.Value = userRoleID;
            cmdParams.Add(cmdUserRoleId);


            IDbDataParameter cmdSelectedUserID = cmd.CreateParameter();
            cmdSelectedUserID.ParameterName = "@SelectedUserID";
            if (SelectedUserID.HasValue)
                cmdSelectedUserID.Value = SelectedUserID;
            else
                cmdSelectedUserID.Value = DBNull.Value;
            cmdParams.Add(cmdSelectedUserID);

            return cmd;

        }


        #endregion


        #region AccountOwnershipStatisticsThirdLevel
        //******************************************************************************
        //******Added by : Prafull
        //******Added on : 14-Jun-2010
        //******Purpose  : Get the Account Ownership Statistics Third Level

        public List<AccountOwnershipStatisticsInfo> SelectAccountOwnershipStatisticsThirdLevel(int userID, short userRoleID, int recPeriodID, int? SelectedUserID, int? SelectedUserIDSecondLevel)
        {

            List<AccountOwnershipStatisticsInfo> oAccountOwnershipStatisticsInfoCollection = new List<AccountOwnershipStatisticsInfo>();

            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateSelectAccountOwnershipStatisticsThirdLevel(userID, userRoleID, recPeriodID, SelectedUserID, SelectedUserIDSecondLevel);
                cmd.Connection = con;
                con.Open();
                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    AccountOwnershipStatisticsInfo oAccountOwnershipStatisticsInfo = null;
                    oAccountOwnershipStatisticsInfo = this.MapAccountOwnershipStatistics(reader);
                    oAccountOwnershipStatisticsInfoCollection.Add(oAccountOwnershipStatisticsInfo);
                }


            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oAccountOwnershipStatisticsInfoCollection;
        }

        private IDbCommand CreateSelectAccountOwnershipStatisticsThirdLevel(int userID, short userRoleID, int recPeriodID, int? SelectedUserID, int? SelectedUserIDSecondLevel)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_AccountOwnerShipStatisticsThirdLevel");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdRecPeriodId = cmd.CreateParameter();
            cmdRecPeriodId.ParameterName = "@RecPeriodID";
            cmdRecPeriodId.Value = recPeriodID;
            cmdParams.Add(cmdRecPeriodId);

            IDbDataParameter cmdUserId = cmd.CreateParameter();
            cmdUserId.ParameterName = "@UserID";
            cmdUserId.Value = userID;
            cmdParams.Add(cmdUserId);

            IDbDataParameter cmdUserRoleId = cmd.CreateParameter();
            cmdUserRoleId.ParameterName = "@RoleID";
            cmdUserRoleId.Value = userRoleID;
            cmdParams.Add(cmdUserRoleId);


            IDbDataParameter cmdSelectedUserID = cmd.CreateParameter();
            cmdSelectedUserID.ParameterName = "@SelectedUserID";
            if (SelectedUserID.HasValue)
                cmdSelectedUserID.Value = SelectedUserID;
            else
                cmdSelectedUserID.Value = DBNull.Value;
            cmdParams.Add(cmdSelectedUserID);


            IDbDataParameter cmdSelectedUserIDSecondLevel = cmd.CreateParameter();
            cmdSelectedUserIDSecondLevel.ParameterName = "@SelectedUserIDSecondLevel";
            if (SelectedUserIDSecondLevel.HasValue)
                cmdSelectedUserIDSecondLevel.Value = SelectedUserIDSecondLevel;
            else
                cmdSelectedUserIDSecondLevel.Value = DBNull.Value;
            cmdParams.Add(cmdSelectedUserIDSecondLevel);





            return cmd;

        }


        #endregion


        #region Reconciliation Tracking
        //******************************************************************************
        //******Added by : Prafull
        //******Added on : 14-Jun-2010
        //******Purpose:   Reconciliation Tracking(Reconciliation Status of the Account for a given (UserId + RoleID + RecPeriodID) Combination)

        public ReconciliationTrackingInfo SelectReconciliationTracking(int userID, short userRoleID, int recPeriodID)
        {

            ReconciliationTrackingInfo oReconciliationTrackingInfo = new ReconciliationTrackingInfo();


            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateSelectReconciliationTracking(userID, userRoleID, recPeriodID);
                cmd.Connection = con;
                con.Open();
                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {

                    oReconciliationTrackingInfo = this.MapReconciliationTracking(reader);

                }


            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oReconciliationTrackingInfo;
        }

        private IDbCommand CreateSelectReconciliationTracking(int userID, short userRoleID, int recPeriodID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_ReconciliationTracking");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdRecPeriodId = cmd.CreateParameter();
            cmdRecPeriodId.ParameterName = "@RecPeriodID";
            cmdRecPeriodId.Value = recPeriodID;
            cmdParams.Add(cmdRecPeriodId);

            IDbDataParameter cmdUserId = cmd.CreateParameter();
            cmdUserId.ParameterName = "@UserID";
            cmdUserId.Value = userID;
            cmdParams.Add(cmdUserId);

            IDbDataParameter cmdUserRoleId = cmd.CreateParameter();
            cmdUserRoleId.ParameterName = "@RoleID";
            cmdUserRoleId.Value = userRoleID;
            cmdParams.Add(cmdUserRoleId);
            return cmd;
        }

        private ReconciliationTrackingInfo MapReconciliationTracking(IDataReader r)
        {
            ReconciliationTrackingInfo entity = new ReconciliationTrackingInfo();
            entity.TotalAccounts = r.GetInt32Value("TotalAccount");

            entity.Prepared = r.GetInt32Value("Prepared");
            entity.InProgress = r.GetInt32Value("InProgress");
            entity.PendingReview = r.GetInt32Value("PendingReview");
            entity.PendingModificationPreparer = r.GetInt32Value("PendingModificationPreparer");
            entity.Reviewed = r.GetInt32Value("Reviewed");
            entity.PendingApproval = r.GetInt32Value("PendingApproval");
            entity.Approved = r.GetInt32Value("Approved");
            entity.Notstarted = r.GetInt32Value("Notstarted");
            entity.SysReconciled = r.GetInt32Value("SysReconciled");
            entity.Reconciled = r.GetInt32Value("Reconciled");
            entity.PendingModificationReviewer = r.GetInt32Value("PendingModificationReviewer");

            //******Get the sum of  GL Balance Reporting Currency for the various statuses
            entity.PreparedAmount = r.GetDecimalValue("PreparedAmount");
            entity.InProgressAmount = r.GetDecimalValue("InProgressAmount");
            entity.PendingReviewAmount = r.GetDecimalValue("PendingReviewAmount");
            entity.PendingModificationPreparerAmount = r.GetDecimalValue("PendingModificationPreparerAmount");
            entity.ReviewedAmount = r.GetDecimalValue("ReviewedAmount");
            entity.PendingApprovalAmount = r.GetDecimalValue("PendingApprovalAmount");
            entity.ApprovedAmount = r.GetDecimalValue("ApprovedAmount");
            entity.NotstartedAmount = r.GetDecimalValue("NotstartedAmount");
            entity.SysReconciledAmount = r.GetDecimalValue("SysReconciledAmount");
            entity.ReconciledAmount = r.GetDecimalValue("ReconciledAmount");
            entity.PendingModificationReviewerAmount = r.GetDecimalValue("PendingModificationReviewerAmount");
            //******************************************************************************

            entity.BaseCurrencyCode = r.GetStringValue("BaseCurrencyCode");
            entity.ReportingCurrencyCode = r.GetStringValue("ReportingCurrencyCode");




            return entity;
        }


        #endregion


        #region "GetExceptionsByFSCaption"
        internal DashboardExceptionInfo GetExceptionsByFSCaptionAndNetAccount(int? UserID, short? RoleID, int? RecPeriodID)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr = null;
            DashboardExceptionInfo oDashboardExceptionInfo = new DashboardExceptionInfo();
            try
            {
                cmd = CreateGetExceptionsByFSCaptionCommand(UserID, RoleID, RecPeriodID);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                List<ExceptionByFSCaptionNetAccountInfo> oFSCaptionExceptionInfoCollection = new List<ExceptionByFSCaptionNetAccountInfo>();
                ExceptionByFSCaptionNetAccountInfo oFSCaptionExceptionInfo = null;
                while (dr.Read())
                {
                    oFSCaptionExceptionInfo = new ExceptionByFSCaptionNetAccountInfo();
                    oFSCaptionExceptionInfo.ID = dr.GetInt32Value("FSCaptionID");
                    oFSCaptionExceptionInfo.LabelID = dr.GetInt32Value("FSCaptionLabelID");
                    oFSCaptionExceptionInfo.WriteOnOffAmountReportingCurrency = dr.GetDecimalValue("WriteOnOffAmountReportingCurrency");
                    oFSCaptionExceptionInfo.UnexplainedVarianceReportingCurrency = dr.GetDecimalValue("UnexplainedVarianceReportingCurrency");
                    oFSCaptionExceptionInfo.TotalVar = oFSCaptionExceptionInfo.WriteOnOffAmountReportingCurrency.GetValueOrDefault() + oFSCaptionExceptionInfo.UnexplainedVarianceReportingCurrency.GetValueOrDefault();

                    oFSCaptionExceptionInfoCollection.Add(oFSCaptionExceptionInfo);
                }
                oDashboardExceptionInfo.FSCaptionExceptionInfoCollection = oFSCaptionExceptionInfoCollection;
                dr.ClearColumnHash();

                // Net Resultset for Net Account
                dr.NextResult();
                List<ExceptionByFSCaptionNetAccountInfo> oNetAccountExceptionInfoCollection = new List<ExceptionByFSCaptionNetAccountInfo>();
                ExceptionByFSCaptionNetAccountInfo oNetAccountExceptionInfo = null;

                while (dr.Read())
                {
                    oNetAccountExceptionInfo = new ExceptionByFSCaptionNetAccountInfo();
                    oNetAccountExceptionInfo.ID = dr.GetInt32Value("NetAccountID");
                    oNetAccountExceptionInfo.LabelID = dr.GetInt32Value("NetAccountLabelID");
                    oNetAccountExceptionInfo.WriteOnOffAmountReportingCurrency = dr.GetDecimalValue("WriteOnOffAmountReportingCurrency");
                    oNetAccountExceptionInfo.UnexplainedVarianceReportingCurrency = dr.GetDecimalValue("UnexplainedVarianceReportingCurrency");
                    oNetAccountExceptionInfo.TotalVar = oNetAccountExceptionInfo.WriteOnOffAmountReportingCurrency.GetValueOrDefault() + oNetAccountExceptionInfo.UnexplainedVarianceReportingCurrency.GetValueOrDefault();

                    oNetAccountExceptionInfoCollection.Add(oNetAccountExceptionInfo);
                }
                oDashboardExceptionInfo.NetAccountExceptionInfoCollection = oNetAccountExceptionInfoCollection;
                dr.ClearColumnHash();
            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                }

                if (cmd != null)
                {
                    if (cmd.Connection != null)
                    {
                        if (cmd.Connection.State != ConnectionState.Closed)
                        {
                            cmd.Connection.Close();
                            cmd.Connection.Dispose();
                        }
                    }
                    cmd.Dispose();
                }

            }
            return oDashboardExceptionInfo;

        }

        private IDbCommand CreateGetExceptionsByFSCaptionCommand(int? UserID, short? RoleID, int? RecPeriodID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_ExceptionsByFSCaption");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;
            ServiceHelper.AddCommonUserRoleAndRecPeriodParameters(UserID, RoleID, RecPeriodID, cmd, cmdParams);

            return cmd;
        }

        #endregion


        #region InCompleteAttributeList
        //******************************************************************************
        //******Added by : Prafull
        //******Added on : 16-Jun-2010
        //******Purpose:   InCompleteAttributeList

        public List<IncompleteAttributeInfo> SelectIncompleteAttributeList(int? userID, short? userRoleID, int? recPeriodID)
        {

            List<IncompleteAttributeInfo> oIncompleteAttributeInfoCollection = new List<IncompleteAttributeInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateSelectIncompleteAttributeList(userID, userRoleID, recPeriodID);
                cmd.Connection = con;
                con.Open();
                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                IncompleteAttributeInfo oIncompleteAttributeInfo = null;

                while (reader.Read())
                {
                    oIncompleteAttributeInfo = MapIncompleteAttributeList(reader);
                    oIncompleteAttributeInfoCollection.Add(oIncompleteAttributeInfo);
                }

                reader.NextResult();

                while (reader.Read())
                {
                    if (oIncompleteAttributeInfoCollection != null && oIncompleteAttributeInfoCollection.Count > 0)
                        oIncompleteAttributeInfoCollection[0].TotalRecordCountToDisplay = reader.GetInt32Value("Total");
                }

            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oIncompleteAttributeInfoCollection;
        }

        private IDbCommand CreateSelectIncompleteAttributeList(int? userID, short? userRoleID, int? recPeriodID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_IncompleteAttributeList");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdRecPeriodId = cmd.CreateParameter();
            cmdRecPeriodId.ParameterName = "@RecPeriodID";
            if (recPeriodID.HasValue)
                cmdRecPeriodId.Value = recPeriodID;
            else
                cmdRecPeriodId.Value = DBNull.Value;
            cmdParams.Add(cmdRecPeriodId);

            IDbDataParameter cmdUserId = cmd.CreateParameter();
            cmdUserId.ParameterName = "@UserID";
            if (userID.HasValue)
                cmdUserId.Value = userID;
            else
                cmdUserId.Value = DBNull.Value;
            cmdParams.Add(cmdUserId);

            IDbDataParameter cmdUserRoleId = cmd.CreateParameter();
            cmdUserRoleId.ParameterName = "@RoleID";
            if (userRoleID.HasValue)
                cmdUserRoleId.Value = userRoleID;
            else
                cmdUserRoleId.Value = DBNull.Value;
            cmdParams.Add(cmdUserRoleId);

            return cmd;
        }

        private IncompleteAttributeInfo MapIncompleteAttributeList(IDataReader r)
        {
            IncompleteAttributeInfo entity = new IncompleteAttributeInfo();
            entity.AccountAttributeID = r.GetInt32Value("AccountAttributeID");
            entity.CompletedAttributeAccountCount = r.GetInt32Value("CompletedAttributeAccountCount");
            entity.TotalAccounts = r.GetInt32Value("TotalAccountCount");

            return entity;
        }

        #endregion


        #region "GetReconciliationStatusByFSCaption"
        internal List<ReconciliationStatusFSCaptionInfo> GetReconciliationStatusByFSCaption(int? UserID, short? RoleID, int? RecPeriodID)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr = null;
            List<ReconciliationStatusFSCaptionInfo> oReconciliationStatusFSCaptionInfoCollection = new List<ReconciliationStatusFSCaptionInfo>();
            ReconciliationStatusFSCaptionInfo oReconciliationStatusFSCaptionInfo = null;
            try
            {
                cmd = CreateGetReconciliationStatusByFSCaptionCommand(UserID, RoleID, RecPeriodID);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    oReconciliationStatusFSCaptionInfo = new ReconciliationStatusFSCaptionInfo();
                    oReconciliationStatusFSCaptionInfo.FSCaptionID = dr.GetInt32Value("FSCaptionID");
                    oReconciliationStatusFSCaptionInfo.FSCaptionLabelID = dr.GetInt32Value("FSCaptionLabelID");

                    oReconciliationStatusFSCaptionInfo.TotalCount = dr.GetInt32Value("Total");
                    oReconciliationStatusFSCaptionInfo.Prepared = dr.GetInt32Value("Prepared");
                    oReconciliationStatusFSCaptionInfo.InProgress = dr.GetInt32Value("InProgress");
                    oReconciliationStatusFSCaptionInfo.PendingReview = dr.GetInt32Value("PendingReview");
                    oReconciliationStatusFSCaptionInfo.PendingModificationPreparer = dr.GetInt32Value("PendingModificationPreparer");
                    oReconciliationStatusFSCaptionInfo.Reviewed = dr.GetInt32Value("Reviewed");
                    oReconciliationStatusFSCaptionInfo.PendingApproval = dr.GetInt32Value("PendingApproval");
                    oReconciliationStatusFSCaptionInfo.Approved = dr.GetInt32Value("Approved");
                    oReconciliationStatusFSCaptionInfo.Notstarted = dr.GetInt32Value("Notstarted");
                    oReconciliationStatusFSCaptionInfo.SysReconciled = dr.GetInt32Value("SysReconciled");
                    oReconciliationStatusFSCaptionInfo.Reconciled = dr.GetInt32Value("Reconciled");
                    oReconciliationStatusFSCaptionInfo.PendingModificationReviewer = dr.GetInt32Value("PendingModificationReviewer");

                    oReconciliationStatusFSCaptionInfoCollection.Add(oReconciliationStatusFSCaptionInfo);
                }
                dr.ClearColumnHash();
            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                }

                if (cmd != null)
                {
                    if (cmd.Connection != null)
                    {
                        if (cmd.Connection.State != ConnectionState.Closed)
                        {
                            cmd.Connection.Close();
                            cmd.Connection.Dispose();
                        }
                    }
                    cmd.Dispose();
                }

            }
            return oReconciliationStatusFSCaptionInfoCollection;
        }

        private IDbCommand CreateGetReconciliationStatusByFSCaptionCommand(int? UserID, short? RoleID, int? RecPeriodID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_ReconciliationStatusByFSCaption");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;
            ServiceHelper.AddCommonUserRoleAndRecPeriodParameters(UserID, RoleID, RecPeriodID, cmd, cmdParams);
            return cmd;
        }

        #endregion


        #region OpenItemStatus
        public List<OpenItemStatusInfo> SelectOpenItemStatusInfo(int userID, short userRoleID, int recPeriodID)
        {

            List<OpenItemStatusInfo> oOpenItemStatusInfoCollection = new List<OpenItemStatusInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateSelectOpenItemStatusInfo(userID, userRoleID, recPeriodID);
                cmd.Connection = con;
                con.Open();
                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                OpenItemStatusInfo oOpenItemStausInfo = null;

                while (reader.Read())
                {
                    oOpenItemStausInfo = MapOpenItemList(reader);
                    oOpenItemStatusInfoCollection.Add(oOpenItemStausInfo);
                }

                //reader.NextResult();
                //while (reader.Read())
                //{
                //    totalRecordCountToDisplay = reader.GetInt32Value("Total");
                //}

            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oOpenItemStatusInfoCollection;
        }

        private IDbCommand CreateSelectOpenItemStatusInfo(int userID, short userRoleID, int recPeriodID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_OpenItemStatusForDashboard");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdRecPeriodId = cmd.CreateParameter();
            cmdRecPeriodId.ParameterName = "@RecPeriodID";
            cmdRecPeriodId.Value = recPeriodID;
            cmdParams.Add(cmdRecPeriodId);

            IDbDataParameter cmdUserId = cmd.CreateParameter();
            cmdUserId.ParameterName = "@UserID";
            cmdUserId.Value = userID;
            cmdParams.Add(cmdUserId);

            IDbDataParameter cmdUserRoleId = cmd.CreateParameter();
            cmdUserRoleId.ParameterName = "@RoleID";
            cmdUserRoleId.Value = userRoleID;
            cmdParams.Add(cmdUserRoleId);

            return cmd;
        }

        private OpenItemStatusInfo MapOpenItemList(IDataReader r)
        {
            OpenItemStatusInfo entity = new OpenItemStatusInfo();
            entity.TotalAmountForOpenRecItem = r.GetDecimalValue("AMOUNT");
            entity.AgingCategoryId = r.GetInt16Value("AgingCategoryid");
            entity.AgingCategoryName = r.GetStringValue("AgingCategoryName");
            return entity;
        }

        #endregion
    }
}