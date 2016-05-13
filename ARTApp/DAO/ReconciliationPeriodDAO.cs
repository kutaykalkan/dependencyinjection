using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.App.Utility;
using System.Transactions;

namespace SkyStem.ART.App.DAO
{
    public class ReconciliationPeriodDAO : ReconciliationPeriodDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ReconciliationPeriodDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        public int InsertReconciliationPeriodDataTable(DataTable dtRecPeriod
            , IDbConnection oConnection, IDbTransaction oTransaction, int companyID, int dataimportID, DateTime? currentReconciliationPeriodEndDate)
        {
            IDbCommand oDBCommand = null;
            oDBCommand = this.InsertRecPeriodIDBCommand(dtRecPeriod, companyID, dataimportID, currentReconciliationPeriodEndDate);
            oDBCommand.Connection = oConnection;
            oDBCommand.Transaction = oTransaction;
            //IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            int rowsAffected = Convert.ToInt32(oDBCommand.ExecuteScalar());
            return rowsAffected;
        }



        //TODO: we dont need this
        //public List<ReconciliationPeriodInfo> GetSkippedReconciliationPeriod(int? newCurrentRecPeriodID)
        //{
        //    System.Data.IDbCommand cmd = this.CreateCommand("usp_Test_GET_CountRecPeriodSkippedToBeCurrent");
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    IDataParameterCollection cmdParams = cmd.Parameters;

        //    System.Data.IDbDataParameter par = cmd.CreateParameter();
        //    par.ParameterName = "@ReconciliationPeriodID";
        //    par.Value = newCurrentRecPeriodID;
        //    cmdParams.Add(par);

        //    return this.Select(cmd);
        //}

        #region GetDataImportProcessingStatus
        internal int? GetDataImportProcessingStatus(int? newCurrentRecPeriodID, int? dataImportTypeID)
        {
            System.Data.IDbCommand oCommand = null;

            try
            {
                oCommand = GetDataImportProcessingStatusCommand(newCurrentRecPeriodID, dataImportTypeID);
                oCommand.Connection = this.CreateConnection();
                oCommand.Connection.Open();
                int? dataImportProcessingStatusID = Convert.ToInt32(oCommand.ExecuteScalar());
                return dataImportProcessingStatusID;
            }
            finally
            {
                if (oCommand != null)
                {
                    if (oCommand.Connection != null && oCommand.Connection.State != ConnectionState.Closed)
                    {
                        oCommand.Connection.Dispose();
                    }
                    oCommand.Dispose();
                }
            }

        }

        public System.Data.IDbCommand GetDataImportProcessingStatusCommand(int? newCurrentRecPeriodID, int? dataImportTypeID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_GET_DataImportProcessingStatus");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parReconciliationPeriodID = cmd.CreateParameter();
            parReconciliationPeriodID.ParameterName = "@ReconciliationPeriodID";
            parReconciliationPeriodID.Value = newCurrentRecPeriodID;
            cmdParams.Add(parReconciliationPeriodID);

            System.Data.IDbDataParameter parDataImportTypeID = cmd.CreateParameter();
            parDataImportTypeID.ParameterName = "@DataImportTypeID";
            parDataImportTypeID.Value = newCurrentRecPeriodID;
            cmdParams.Add(parDataImportTypeID);

            return cmd;
        }
        #endregion


        private int MapIncompleteRequirementToMarkOpen(IDataReader reader)
        {
            int entity = 0;

            try
            {
                int ordinal = reader.GetOrdinal("IncompleteRequirementID");
                if (!reader.IsDBNull(ordinal)) entity = ((System.Int32)(reader.GetValue(ordinal)));
            }
            catch { }

            //try
            //{
            //    int ordinal = reader.GetOrdinal("IncompleteRequireMent");
            //    if (!reader.IsDBNull(ordinal)) entity.AccountAttribute = ((System.String)(reader.GetValue(ordinal)));
            //}
            //catch (Exception ex) { }

            return entity;
        }


        public List<int> GetIncompleteRequirementToMarkOpen(int? RecPeriodID)
        {
            List<int> oIncompleteRequirementCollection = new List<int>();
            IDbCommand cmd = null;

            try
            {
                cmd = this.GetIncompleteRequirementToMarkOpenCommand(RecPeriodID);
                cmd.Connection = this.CreateConnection();
                cmd.Connection.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    oIncompleteRequirementCollection.Add(this.MapIncompleteRequirementToMarkOpen(reader));
                }
            }
            finally
            {
                if (cmd != null)
                {
                    if (cmd.Connection != null && cmd.Connection.State != ConnectionState.Closed)
                    {
                        cmd.Connection.Dispose();
                    }
                    cmd.Dispose();
                }
            }

            return oIncompleteRequirementCollection;
        }



        //public bool? GetIncompleteRequirementToMarkOpen(int? RecPeriodID)
        //{
        //    //ReconciliationPeriodInfo oCurrentRecPeriod = null;
        //    IDbCommand oDBCommand = null;
        //    IDbConnection oConnection = null;
        //    bool? oIsCapabilityConfigurationComplete;
        //    //IDataReader oReader = null;
        //    try
        //    {
        //        oDBCommand = this.GetIncompleteRequirementToMarkOpenCommand(RecPeriodID);
        //        oConnection = this.CreateConnection();
        //        oConnection.Open();
        //        oDBCommand.Connection = oConnection;
        //        //TODO: will return table
        //        Object obj = oDBCommand.ExecuteScalar();
        //        //oRecPeriodEndDate = Convert.ToDateTime(obj);
        //        oIsCapabilityConfigurationComplete = obj == null ? null :  (bool?)obj;
        //    }
        //    finally
        //    {
        //        if (oConnection != null && oConnection.State != ConnectionState.Closed)
        //            oConnection.Dispose();
        //    }

        //    return oIsCapabilityConfigurationComplete;
        //}




        //public System.Data.IDbCommand GetDataImportProcessingStatusCommand(int? newCurrentRecPeriodID, int? dataImportTypeID)
        //{
        //    System.Data.IDbCommand cmd = this.CreateCommand("usp_Test_GET_DataImportProcessingStatus");
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    IDataParameterCollection cmdParams = cmd.Parameters;

        //    System.Data.IDbDataParameter parReconciliationPeriodID = cmd.CreateParameter();
        //    parReconciliationPeriodID.ParameterName = "@ReconciliationPeriodID";
        //    parReconciliationPeriodID.Value = newCurrentRecPeriodID;
        //    cmdParams.Add(parReconciliationPeriodID);

        //    System.Data.IDbDataParameter parDataImportTypeID = cmd.CreateParameter();
        //    parDataImportTypeID.ParameterName = "@DataImportTypeID";
        //    parDataImportTypeID.Value = dataImportTypeID;
        //    cmdParams.Add(parDataImportTypeID);

        //    return cmd;
        //}


        public System.Data.IDbCommand GetIncompleteRequirementToMarkOpenCommand(int? newCurrentRecPeriodID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_IncompleteRequirementToMarkOpen");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parReconciliationPeriodID = cmd.CreateParameter();
            parReconciliationPeriodID.ParameterName = "@ReconciliationPeriodID";
            parReconciliationPeriodID.Value = newCurrentRecPeriodID;
            cmdParams.Add(parReconciliationPeriodID);

            return cmd;
        }


        public int UpdateSystemWideSettingDates(int? companyID, List<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollection, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            int rowsAffected = 0;
            DataTable dtReconciliationPeriodInfoCollection = ServiceHelper.ConvertReconciliationPeriodDatesCollectionToDataTable(oReconciliationPeriodInfoCollection);
            IDbCommand oCommand = CreateUpdateSystemWideSettingDatesCommand(companyID, dtReconciliationPeriodInfoCollection);
            oCommand.Connection = oConnection;
            oCommand.Transaction = oTransaction;
            rowsAffected = oCommand.ExecuteNonQuery();
            return rowsAffected;
        }

        public int UpdateReconciliationPeriodStatusAndMarkSkipped(int? reconciliationPeriodID, int? reconciliationPeriodStatusID, DateTime? dateRevised, string revisedBy, short actionTypeID, short changeSourceIDSRA, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            int rowsAffected = 0;
            IDbCommand oCommand = UpdateReconciliationPeriodStatusAndMarkSkippedCommand(reconciliationPeriodID, reconciliationPeriodStatusID, dateRevised, revisedBy, actionTypeID, changeSourceIDSRA);
            oCommand.Connection = oConnection;
            oCommand.Transaction = oTransaction;
            rowsAffected = oCommand.ExecuteNonQuery();
            return rowsAffected;
        }

        #region "Create Command Methods"

        protected System.Data.IDbCommand CreateUpdateSystemWideSettingDatesCommand(int? companyID, DataTable dtReconciliationPeriodInfoCollection)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_SystemWideSettingDates");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            //if (!entity.IsCompanyIDNull)
            parCompanyID.Value = companyID;
            //else
            //    parCompanyID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parReconciliationPeriodTable = cmd.CreateParameter();
            parReconciliationPeriodTable.ParameterName = "@tblReconciliationPeriod";
            //if (!entity.IsReconciliationFrequencyIDNull)
            parReconciliationPeriodTable.Value = dtReconciliationPeriodInfoCollection;
            //else
            //    parReconciliationFrequencyID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationPeriodTable);

            return cmd;
        }



        protected IDbCommand InsertRecPeriodIDBCommand(DataTable dtRecPeriod, int companyID, int dataimportID, DateTime? currentReconciliationPeriodEndDate)
        {
            IDbCommand oIDBCommand = this.CreateCommand("usp_INS_ReconciliationPeriod");
            oIDBCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oIDBCommand.Parameters;

            IDbDataParameter cmdParamRecPeriodTable = oIDBCommand.CreateParameter();
            cmdParamRecPeriodTable.ParameterName = "@RecPeriodTable";
            cmdParamRecPeriodTable.Value = dtRecPeriod;
            cmdParams.Add(cmdParamRecPeriodTable);

            IDbDataParameter cmdParamCompanyID = oIDBCommand.CreateParameter();
            cmdParamCompanyID.ParameterName = "@companyID";
            cmdParamCompanyID.Value = companyID;
            cmdParams.Add(cmdParamCompanyID);

            IDbDataParameter cmdParamDataImportID = oIDBCommand.CreateParameter();
            cmdParamDataImportID.ParameterName = "@dataimportID";
            cmdParamDataImportID.Value = dataimportID;
            cmdParams.Add(cmdParamDataImportID);

            IDbDataParameter cmdcurrentReconciliationPeriodEndDate = oIDBCommand.CreateParameter();
            cmdcurrentReconciliationPeriodEndDate.ParameterName = "@currentReconciliationPeriodEndDate";
            if (currentReconciliationPeriodEndDate != null)
            {
                cmdcurrentReconciliationPeriodEndDate.Value = currentReconciliationPeriodEndDate;
            }
            else
            {
                cmdcurrentReconciliationPeriodEndDate.Value = DBNull.Value;

            }
            cmdParams.Add(cmdcurrentReconciliationPeriodEndDate);


            return oIDBCommand;
        }

        protected IDbCommand UpdateReconciliationPeriodStatusAndMarkSkippedCommand(int? reconciliationPeriodID, int? reconciliationPeriodStatusID, DateTime? dateRevised, string revisedBy, short actionTypeID, short changeSourceIDSRA)
        {
            IDbCommand cmd = this.CreateCommand("usp_UPD_ReconciliationPeriodStatusAndMarkSkipped");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parReconciliationPeriodID = cmd.CreateParameter();
            parReconciliationPeriodID.ParameterName = "@CurrentReconciliationPeriodIDNew";
            parReconciliationPeriodID.Value = reconciliationPeriodID;
            cmdParams.Add(parReconciliationPeriodID);

            System.Data.IDbDataParameter parReconciliationPeriodStatusID = cmd.CreateParameter();
            parReconciliationPeriodStatusID.ParameterName = "@ReconciliationPeriodStatusID";
            if (reconciliationPeriodStatusID.HasValue)
            {
                parReconciliationPeriodStatusID.Value = reconciliationPeriodStatusID;
            }
            else
            {
                parReconciliationPeriodStatusID.Value = System.DBNull.Value;
            }
            cmdParams.Add(parReconciliationPeriodStatusID);

            ServiceHelper.AddCommonParametersForDateRevised(dateRevised, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForRevisedBy(revisedBy, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForActionTypeID(actionTypeID, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForChangeSourceIDSRA(changeSourceIDSRA, cmd, cmdParams);

            return cmd;

        }



        #endregion

        #region GetDueDateByUserRoleID

        public DateTime? GetDueDateByUserRoleID(int roleId, short preparerRoleID, short reviewerRoleId, short approverRoleId, int recPeriodId)
        {
            DateTime? dueDate = null;
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateGetDueDateByUserRoleIDCommand(roleId, preparerRoleID, reviewerRoleId, approverRoleId, recPeriodId);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    dueDate = reader.GetDateValue("DueDate");
                }


            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return dueDate;
        }

        private IDbCommand CreateGetDueDateByUserRoleIDCommand(int roleId, short preparerRoleID, short reviewerRoleId, short approverRoleId, int recPeriodId)
        {
            IDbCommand cmd = this.CreateCommand("usp_GET_DuedateByUserIDAndRoleID");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdUserRoleId = cmd.CreateParameter();
            cmdUserRoleId.ParameterName = "@RoleID";
            cmdUserRoleId.Value = roleId;
            cmdParams.Add(cmdUserRoleId);

            IDbDataParameter cmdPreparerRoleID = cmd.CreateParameter();
            cmdPreparerRoleID.ParameterName = "@PreparerRoleID";
            cmdPreparerRoleID.Value = preparerRoleID;
            cmdParams.Add(cmdPreparerRoleID);

            IDbDataParameter cmdReviewerRoleID = cmd.CreateParameter();
            cmdReviewerRoleID.ParameterName = "@ReviewerRoleID";
            cmdReviewerRoleID.Value = reviewerRoleId;
            cmdParams.Add(cmdReviewerRoleID);

            IDbDataParameter cmdApproverRoleID = cmd.CreateParameter();
            cmdApproverRoleID.ParameterName = "@ApproverRoleID";
            cmdApproverRoleID.Value = approverRoleId;
            cmdParams.Add(cmdApproverRoleID);

            IDbDataParameter cmdRecPeriodId = cmd.CreateParameter();
            cmdRecPeriodId.ParameterName = "@RecPeriodID";
            cmdRecPeriodId.Value = recPeriodId;
            cmdParams.Add(cmdRecPeriodId);

            return cmd;
        }

        #endregion

        #region GetBaseCurrencyByCompanyID

        public string GetBaseCurrencyByCompanyID(int recPeriodID)
        {
            string baseCurrencyCode = null;
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateCommand("usp_GET_BaseCurrencyByCompanyID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter cmdRecPeriodID = cmd.CreateParameter();
                cmdRecPeriodID.ParameterName = "@RecPeriodID";
                cmdRecPeriodID.Value = recPeriodID;
                cmdParams.Add(cmdRecPeriodID);

                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    try
                    {
                        int ordinal = reader.GetOrdinal("BaseCurrencyCode");
                        if (!reader.IsDBNull(ordinal)) baseCurrencyCode = ((System.String)(reader.GetValue(ordinal)));
                    }
                    catch { }


                }
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return baseCurrencyCode;
        }

        #endregion

        #region GetReportingCurrencyByCompanyID

        public string GetReportingCurrencyByCompanyID(int recPeriodID)
        {
            string reportingCurrencyCode = null;
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateCommand("usp_GET_ReportingCurrencyByCompanyID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter cmdRecPeriodID = cmd.CreateParameter();
                cmdRecPeriodID.ParameterName = "@RecPeriodID";
                cmdRecPeriodID.Value = recPeriodID;
                cmdParams.Add(cmdRecPeriodID);

                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    try
                    {
                        int ordinal = reader.GetOrdinal("ReportingCurrencyCode");
                        if (!reader.IsDBNull(ordinal)) reportingCurrencyCode = ((System.String)(reader.GetValue(ordinal)));
                    }
                    catch { }


                }
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return reportingCurrencyCode;
        }
        #endregion

        #region UpdateRecPeriodStatusAsInProgress

        public void UpdateRecPeriodStatusAsInProgress(int recPeriodID, IDbConnection con, IDbTransaction otransaction)
        {
            IDbCommand cmd = null;
            bool isConnectionNull = (con == null);

            try
            {
                cmd = this.CreateCommand("usp_UPD_RecPeriodStatusAsInProgress");
                cmd.CommandType = CommandType.StoredProcedure;

                if (con == null)
                {
                    con = this.CreateConnection();
                    cmd.Connection = con;
                    con.Open();
                }
                else
                {
                    cmd.Connection = con;
                    cmd.Transaction = otransaction;
                }


                //@RecPeriodID INT  
                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter paramRecPeriodID = cmd.CreateParameter();
                paramRecPeriodID.ParameterName = "@RecPeriodID";
                paramRecPeriodID.Value = recPeriodID;
                cmdParams.Add(paramRecPeriodID);

                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (isConnectionNull && con != null && con.State == ConnectionState.Open)
                    con.Dispose();
            }
        }

        #endregion

        #region ForceCloseRecPeriod

        public AccountCertificationStatusInfo GetAccountAndCertificationStatus(int? CurrentReconciliationPeriodID, int? CurrentUserID, int? CurrentCompanyID, bool IsCertificationActivated, short? CurrentRoleID)
        {
            AccountCertificationStatusInfo oAccountCertificationStatusInfo = new AccountCertificationStatusInfo();


            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                con.Open();
                IDataReader reader;
                cmd = this.CreateSelectAccountStatusByRecPeriodIDCommand(CurrentReconciliationPeriodID, CurrentUserID, CurrentCompanyID, CurrentRoleID);
                cmd.Connection = con;
                reader = cmd.ExecuteReader();
                AccountStatusInfo oAccountStatusInfo = null;
                while (reader.Read())
                {
                    oAccountStatusInfo = this.MapAccountStatusInfo(reader);
                    oAccountCertificationStatusInfo.oAccountStatusInfoCollection.Add(oAccountStatusInfo);
                }
                reader.Close();
                if (IsCertificationActivated)
                {
                    cmd = this.CreateSelectCertificationStatusByRecPeriodIDCommand(CurrentReconciliationPeriodID, CurrentUserID, CurrentCompanyID, CurrentRoleID);
                    cmd.Connection = con;
                    reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    CertificationStatusInfo oCertificationStatusInfo = new CertificationStatusInfo();
                    while (reader.Read())
                    {
                        oCertificationStatusInfo = this.MapCertificationStatusInfo(reader);
                    }
                    oAccountCertificationStatusInfo.oCertificationStatusInfo = oCertificationStatusInfo;
                }
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }
            return oAccountCertificationStatusInfo;

        }

        private AccountStatusInfo MapAccountStatusInfo(IDataReader r)
        {
            AccountStatusInfo entity = new AccountStatusInfo();
            entity.UnReconciledAccountsCount = r.GetInt32Value("UnReconciledAccountsCount");
            entity.UnReconciledAccountsDollarAmmount = r.GetDecimalValue("UnReconciledAccountsDollarAmmount");
            entity.UnReconciledAccountsPercentage = r.GetDecimalValue("UnReconciledAccountsPercentage");
            entity.IsSRA = r.GetBooleanValue("IsSRA");
            return entity;
        }
        private IDbCommand CreateSelectAccountStatusByRecPeriodIDCommand(int? CurrentReconciliationPeriodID, int? CurrentUserID, int? CurrentCompanyID, short? CurrentRoleID)
        {
            IDbCommand cmd = this.CreateCommand("usp_GET_AccountStatusByRecPeriodIDAndUserID");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdRecPeriodId = cmd.CreateParameter();
            cmdRecPeriodId.ParameterName = "@RecPeriodID";
            cmdRecPeriodId.Value = CurrentReconciliationPeriodID;
            cmdParams.Add(cmdRecPeriodId);

            IDbDataParameter cmdUserID = cmd.CreateParameter();
            cmdUserID.ParameterName = "@UserID";
            cmdUserID.Value = CurrentUserID;
            cmdParams.Add(cmdUserID);

            IDbDataParameter cmdCompanyID = cmd.CreateParameter();
            cmdCompanyID.ParameterName = "@CompanyID";
            cmdCompanyID.Value = CurrentCompanyID;
            cmdParams.Add(cmdCompanyID);

            IDbDataParameter cmdCurrentRoleID = cmd.CreateParameter();
            cmdCurrentRoleID.ParameterName = "@UserRoleID";
            cmdCurrentRoleID.Value = CurrentRoleID;
            cmdParams.Add(cmdCurrentRoleID);

            return cmd;
        }

        private CertificationStatusInfo MapCertificationStatusInfo(IDataReader r)
        {
            CertificationStatusInfo entity = new CertificationStatusInfo();
            entity.UnCertifiedAccountsCount = r.GetInt32Value("UnCertifiedAccountsCount");
            if (r.GetDecimalValue("UnCertifiedAccountsDollarAmmount") != null)
                entity.UnCertifiedAccountsDollarAmmount = r.GetDecimalValue("UnCertifiedAccountsDollarAmmount");
            entity.UnCertifiedAccountsPercentage = r.GetDecimalValue("UnCertifiedAccountsPercentage");
            return entity;
        }

        private IDbCommand CreateSelectCertificationStatusByRecPeriodIDCommand(int? CurrentReconciliationPeriodID, int? CurrentUserID, int? CurrentCompanyID, short? CurrentRoleID)
        {
            IDbCommand cmd = this.CreateCommand("usp_GET_CertificationStatusByRecPeriodIDAndUserID");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdRecPeriodId = cmd.CreateParameter();
            cmdRecPeriodId.ParameterName = "@RecPeriodID";
            cmdRecPeriodId.Value = CurrentReconciliationPeriodID;
            cmdParams.Add(cmdRecPeriodId);

            IDbDataParameter cmdUserID = cmd.CreateParameter();
            cmdUserID.ParameterName = "@UserID";
            cmdUserID.Value = CurrentUserID;
            cmdParams.Add(cmdUserID);

            IDbDataParameter cmdCompanyID = cmd.CreateParameter();
            cmdCompanyID.ParameterName = "@CompanyID";
            cmdCompanyID.Value = CurrentCompanyID;
            cmdParams.Add(cmdCompanyID);

            IDbDataParameter cmdCurrentRoleID = cmd.CreateParameter();
            cmdCurrentRoleID.ParameterName = "@UserRoleID";
            cmdCurrentRoleID.Value = CurrentRoleID;
            cmdParams.Add(cmdCurrentRoleID);

            return cmd;
        }

        public int CloseRecPeriodByRecPeriodIdAndComanyID(int? CurrentReconciliationPeriodID, Int16? ReconciliationPeriodStatusID, DateTime? RevisedDate, String UserLoginID, short actionTypeID, short changeSourceIDSRA)
        {
            int rowsAffected = 0;
            IDbCommand cmd = null;
            using (TransactionScope scope = new TransactionScope())
            {
                using (IDbConnection con = this.CreateConnection())
                {
                    try
                    {
                        con.Open();
                        cmd = this.CreateCloseRecPeriodByRecPeriodIdAndComanyIDCommand(CurrentReconciliationPeriodID, ReconciliationPeriodStatusID, RevisedDate, UserLoginID, actionTypeID, changeSourceIDSRA);
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        rowsAffected = Convert.ToInt32(((IDbDataParameter)cmd.Parameters["@rowsEffectedByupdate"]).Value);
                        scope.Complete();
                    }
                    finally
                    {
                        if (cmd != null)
                            cmd.Dispose();
                    }
                }
            }
            return rowsAffected;
        }

        private IDbCommand CreateCloseRecPeriodByRecPeriodIdAndComanyIDCommand(int? CurrentReconciliationPeriodID, Int16? ReconciliationPeriodStatusID, DateTime? RevisedDate, String revisedBy, short actionTypeID, short changeSourceIDSRA)
        {
            IDbCommand cmd = this.CreateCommand("usp_UPD_RecPeriodStatusByRecPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdRecPeriodId = cmd.CreateParameter();
            cmdRecPeriodId.ParameterName = "@RecPeriodID";
            cmdRecPeriodId.Value = CurrentReconciliationPeriodID;
            cmdParams.Add(cmdRecPeriodId);

            IDbDataParameter cmdReconciliationPeriodStatusID = cmd.CreateParameter();
            cmdReconciliationPeriodStatusID.ParameterName = "@recPeriodStatusID";
            cmdReconciliationPeriodStatusID.Value = ReconciliationPeriodStatusID;
            cmdParams.Add(cmdReconciliationPeriodStatusID);

            IDbDataParameter cmdRevisedDate = cmd.CreateParameter();
            cmdRevisedDate.ParameterName = "@dateRevised";
            cmdRevisedDate.Value = RevisedDate;
            cmdParams.Add(cmdRevisedDate);

            IDbDataParameter cmdRevisedBy = cmd.CreateParameter();
            cmdRevisedBy.ParameterName = "@revisedBy";
            cmdRevisedBy.Value = revisedBy;
            cmdParams.Add(cmdRevisedBy);

            IDbDataParameter cmdRowsEffected = cmd.CreateParameter();
            cmdRowsEffected.ParameterName = "@rowsEffectedByupdate";
            cmdRowsEffected.DbType = System.Data.DbType.Int32;
            cmdRowsEffected.Direction = ParameterDirection.Output;
            cmdParams.Add(cmdRowsEffected);

            ServiceHelper.AddCommonParametersForActionTypeID(actionTypeID, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForChangeSourceIDSRA(changeSourceIDSRA, cmd, cmdParams);

            return cmd;
        }

        public int MarkRecPeriodReconciledAndStartCertification(int? CurrentReconciliationPeriodID, DateTime? RevisedDate, String UserLoginID, short actionTypeID, short changeSourceIDSRA)
        {
            int rowsAffected = 0;
            IDbCommand cmd = null;
            using (TransactionScope scope = new TransactionScope())
            {
                using (IDbConnection con = this.CreateConnection())
                {
                    try
                    {
                        con.Open();
                        cmd = this.MarkRecPeriodReconciledAndStartCertificationCommand(CurrentReconciliationPeriodID, RevisedDate, UserLoginID, actionTypeID, changeSourceIDSRA);
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        rowsAffected = Convert.ToInt32(((IDbDataParameter)cmd.Parameters["@rowsEffectedByupdate"]).Value);
                        scope.Complete();
                    }
                    finally
                    {
                        if (cmd != null)
                            cmd.Dispose();
                    }
                }

            }
            return rowsAffected;
        }

        private IDbCommand MarkRecPeriodReconciledAndStartCertificationCommand(int? CurrentReconciliationPeriodID, DateTime? RevisedDate, String revisedBy, short actionTypeID, short changeSourceIDSRA)
        {
            IDbCommand cmd = this.CreateCommand("usp_UPD_MarkRecPeriodReconciledAndStartCertification");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdRecPeriodId = cmd.CreateParameter();
            cmdRecPeriodId.ParameterName = "@RecPeriodID";
            cmdRecPeriodId.Value = CurrentReconciliationPeriodID;
            cmdParams.Add(cmdRecPeriodId);


            IDbDataParameter cmdRevisedDate = cmd.CreateParameter();
            cmdRevisedDate.ParameterName = "@dateRevised";
            cmdRevisedDate.Value = RevisedDate;
            cmdParams.Add(cmdRevisedDate);

            IDbDataParameter cmdRevisedBy = cmd.CreateParameter();
            cmdRevisedBy.ParameterName = "@revisedBy";
            cmdRevisedBy.Value = revisedBy;
            cmdParams.Add(cmdRevisedBy);

            IDbDataParameter cmdRowsEffected = cmd.CreateParameter();
            cmdRowsEffected.ParameterName = "@rowsEffectedByupdate";
            cmdRowsEffected.DbType = System.Data.DbType.Int32;
            cmdRowsEffected.Direction = ParameterDirection.Output;
            cmdParams.Add(cmdRowsEffected);

            ServiceHelper.AddCommonParametersForActionTypeID(actionTypeID, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForChangeSourceIDSRA(actionTypeID, cmd, cmdParams);

            return cmd;
        }

        public bool GetIsStopRecAndStartCertFlag(int? CurrentReconciliationPeriodID)
        {
            System.Data.IDbCommand oCommand = null;
            bool IsStopRecAndStartCertFlag = false;
            try
            {
                oCommand = GetIsStopRecAndStartCertFlagCommand(CurrentReconciliationPeriodID);
                oCommand.Connection = this.CreateConnection();
                oCommand.Connection.Open();
                IsStopRecAndStartCertFlag = Convert.ToBoolean(oCommand.ExecuteScalar());
                return IsStopRecAndStartCertFlag;
            }
            finally
            {
                if (oCommand != null)
                {
                    if (oCommand.Connection != null && oCommand.Connection.State != ConnectionState.Closed)
                    {
                        oCommand.Connection.Dispose();
                    }
                    oCommand.Dispose();
                }
            }

        }

        private IDbCommand GetIsStopRecAndStartCertFlagCommand(int? CurrentReconciliationPeriodID)
        {
            IDbCommand cmd = this.CreateCommand("usp_UPD_GetIsStopRecAndStartCertFlag");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdRecPeriodId = cmd.CreateParameter();
            cmdRecPeriodId.ParameterName = "@RecPeriodID";
            cmdRecPeriodId.Value = CurrentReconciliationPeriodID;
            cmdParams.Add(cmdRecPeriodId);


            return cmd;
        }



        #endregion


        public IList<CompanyWeekDayInfo> SelectAllWorkWeekByFinancialYearIDAndCompanyID(int? companyID, int? FinancialYearID)
        {
            List<CompanyWeekDayInfo> oCompanyWeekDayInfoCollection = new List<CompanyWeekDayInfo>();


            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                con.Open();
                IDataReader reader;
                cmd = this.CreateSelectAllWorkWeekByFinancialYearIDAndCompanyIDCommand(companyID, FinancialYearID);
                cmd.Connection = con;
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    CompanyWeekDayInfo oCompanyWeekDayInfo = null;
                    oCompanyWeekDayInfo = this.MapRecPeriodWorkWeekInfo(reader);
                    oCompanyWeekDayInfoCollection.Add(oCompanyWeekDayInfo);
                }

            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }
            return oCompanyWeekDayInfoCollection;

        }

        protected IDbCommand CreateSelectAllWorkWeekByFinancialYearIDAndCompanyIDCommand(int? companyID, int? FinancialYearID)
        {
            IDbCommand oIDBCommand = this.CreateCommand("usp_GET_AllWorkWeekByFinancialYearIDAndCompanyID");
            oIDBCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oIDBCommand.Parameters;

            IDbDataParameter cmdParamCompanyID = oIDBCommand.CreateParameter();
            cmdParamCompanyID.ParameterName = "@CompanyID";
            cmdParamCompanyID.Value = companyID.Value;
            cmdParams.Add(cmdParamCompanyID);

            IDbDataParameter cmdParamFinancialYearID = oIDBCommand.CreateParameter();
            cmdParamFinancialYearID.ParameterName = "@FinancialYearID";
            if (FinancialYearID != null)
            {
                cmdParamFinancialYearID.Value = FinancialYearID.Value;
            }
            else
            {
                cmdParamFinancialYearID.Value = DBNull.Value;
            }
            cmdParams.Add(cmdParamFinancialYearID);

            return oIDBCommand;

        }

        private CompanyWeekDayInfo MapRecPeriodWorkWeekInfo(IDataReader r)
        {
            CompanyWeekDayInfo entity = new CompanyWeekDayInfo();

            entity.RecPeriodID = r.GetInt32Value("RecPeriodID");
            entity.CompanyWeekDayID = r.GetInt32Value("CompanyWeekDayID");
            entity.WeekDayID = r.GetInt16Value("WeekDayID");
            entity.StartRecPeriodID = r.GetInt32Value("StartRecPeriodID");
            entity.EndRecPeriodID = r.GetInt32Value("EndRecPeriodID");
            return entity;
        }


        public ReconciliationPeriodInfo GetReconciliationPeriodInfoByRecPeriodID(int? recPeriodID, DateTime? PeriodEndDate, int? CompanyID)
        {
            ReconciliationPeriodInfo oReconciliationPeriodInfo = null;

            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                con.Open();
                IDataReader reader;
                cmd = this.CreateReconciliationPeriodInfoByRecPeriodIDCommand(recPeriodID, PeriodEndDate, CompanyID);
                cmd.Connection = con;
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    oReconciliationPeriodInfo = this.MapObject(reader);

                }

            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }
            return oReconciliationPeriodInfo;

        }

        protected IDbCommand CreateReconciliationPeriodInfoByRecPeriodIDCommand(int? recPeriodID, DateTime? PeriodEndDate, int? CompanyID)
        {
            IDbCommand oIDBCommand = this.CreateCommand("usp_GET_ReconciliationPeriodInfoByRecPeriodID");
            oIDBCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oIDBCommand.Parameters;

            IDbDataParameter cmdParamRecPeriodID = oIDBCommand.CreateParameter();
            cmdParamRecPeriodID.ParameterName = "@RecPeriodID";
            if (recPeriodID.HasValue)
            {
                cmdParamRecPeriodID.Value = recPeriodID.Value;
            }
            else
            {
                cmdParamRecPeriodID.Value = DBNull.Value;
            }
            cmdParams.Add(cmdParamRecPeriodID);

            IDbDataParameter cmdParamPeriodEndDate = oIDBCommand.CreateParameter();
            cmdParamPeriodEndDate.ParameterName = "@PeriodEndDate";
            if (PeriodEndDate.HasValue)
            {
                cmdParamPeriodEndDate.Value = PeriodEndDate.Value;
            }
            else
            {
                cmdParamPeriodEndDate.Value = DBNull.Value;
            }
            cmdParams.Add(cmdParamPeriodEndDate);

            IDbDataParameter cmdParamCompanyID = oIDBCommand.CreateParameter();
            cmdParamCompanyID.ParameterName = "@CompanyID";
            if (CompanyID.HasValue)
            {
                cmdParamCompanyID.Value = CompanyID.Value;
            }
            else
            {
                cmdParamCompanyID.Value = DBNull.Value;
            }
            cmdParams.Add(cmdParamCompanyID);

            return oIDBCommand;

        }

        internal ReconciliationPeriodInfo GetCurrentReconciliationPeriod(int? CompanyID, bool IsGetMax)
        {
            System.Data.IDbCommand cmd = null;
            ReconciliationPeriodInfo oReconciliationPeriodInfo = null;
            IDataReader dr = null;

            try
            {
                cmd = CreateGetCurrentReconciliationPeriodCommand(CompanyID, IsGetMax);
                cmd.Connection = this.CreateConnection();
                cmd.Connection.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                oReconciliationPeriodInfo = CustomMapObjectForCurrentFYandRecPeriod(dr);
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
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

            return oReconciliationPeriodInfo;
        }


        protected ReconciliationPeriodInfo CustomMapObjectForCurrentFYandRecPeriod(System.Data.IDataReader r)
        {
            ReconciliationPeriodInfo entity = new ReconciliationPeriodInfo();
            r.Read();
            entity.ReconciliationPeriodID = r.GetInt32Value("ReconciliationPeriodID");
            entity.FinancialYearID = r.GetInt32Value("FinancialYearID");
            entity.PeriodEndDate = r.GetDateValue("PeriodEndDate");
            return entity;
        }


        private IDbCommand CreateGetCurrentReconciliationPeriodCommand(int? CompanyID, bool IsGetMax)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_GET_CurrentReconciliationPeriod");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = CompanyID.Value;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parIsGetMax = cmd.CreateParameter();
            parIsGetMax.ParameterName = "@IsGetMax";
            parIsGetMax.Value = IsGetMax;
            cmdParams.Add(parIsGetMax);

            return cmd;
        }

        public bool GetIsMinimumRecPeriodExist(int? CurrentReconciliationPeriodID)
        {
            System.Data.IDbCommand oCommand = null;
            bool IsMinimumRecPeriodExist = false;
            try
            {
                oCommand = GetIsMinimumRecPeriodExistCommand(CurrentReconciliationPeriodID);
                oCommand.Connection = this.CreateConnection();
                oCommand.Connection.Open();
                IsMinimumRecPeriodExist = Convert.ToBoolean(oCommand.ExecuteScalar());
                return IsMinimumRecPeriodExist;
            }
            finally
            {
                if (oCommand != null)
                {
                    if (oCommand.Connection != null && oCommand.Connection.State != ConnectionState.Closed)
                    {
                        oCommand.Connection.Dispose();
                    }
                    oCommand.Dispose();
                }
            }

        }

        private IDbCommand GetIsMinimumRecPeriodExistCommand(int? CurrentReconciliationPeriodID)
        {
            IDbCommand cmd = this.CreateCommand("usp_UPD_GetIsMinimumRecPeriodExist");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdRecPeriodId = cmd.CreateParameter();
            cmdRecPeriodId.ParameterName = "@RecPeriodID";
            cmdRecPeriodId.Value = CurrentReconciliationPeriodID;
            cmdParams.Add(cmdRecPeriodId);


            return cmd;
        }

        public bool IsPreviousPeriodsCertified(int? recPeriodID)
        {
            System.Data.IDbCommand oCommand = null;
            bool isPreviousPeriodsCertified = false;
            try
            {
                oCommand = GetIsPreviousPeriodsCertifiedCommand(recPeriodID);
                oCommand.Connection = this.CreateConnection();
                oCommand.Connection.Open();
                isPreviousPeriodsCertified = Convert.ToBoolean(oCommand.ExecuteScalar());
                return isPreviousPeriodsCertified;
            }
            finally
            {
                if (oCommand != null)
                {
                    if (oCommand.Connection != null && oCommand.Connection.State != ConnectionState.Closed)
                    {
                        oCommand.Connection.Dispose();
                    }
                    oCommand.Dispose();
                }
            }
        }

        private IDbCommand GetIsPreviousPeriodsCertifiedCommand(int? recPeriodID)
        {
            IDbCommand cmd = this.CreateCommand("usp_GET_IsPreviousRecPeriodsCertified");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdRecPeriodId = cmd.CreateParameter();
            cmdRecPeriodId.ParameterName = "@RecPeriodID";
            cmdRecPeriodId.Value = recPeriodID;
            cmdParams.Add(cmdRecPeriodId);

            return cmd;
        }

        public void ReprocessAccountReconcilability(int? companyID, int? recPeriodID, List<long> accountIDList, short actionTypeID, short changeSourceIDSRA)
        {
            DataTable dtAccount = ServiceHelper.ConvertLongIDCollectionToDataTable(accountIDList);
            using (IDbConnection cnn = this.CreateConnection())
            {
                cnn.Open();
                using (IDbCommand cmd = CreateCommandReprocessAccountReconcilability(companyID, recPeriodID, dtAccount, actionTypeID, changeSourceIDSRA))
                {
                    cmd.Connection = cnn;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private IDbCommand CreateCommandReprocessAccountReconcilability(int? companyID, int? recPeriodID, DataTable dtAccount, short actionTypeID, short changeSourceIDSRA)
        {
            IDbCommand cmd = this.CreateCommand("usp_UPD_ReprocessAccountRecFrequency");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            ServiceHelper.AddCommonParametersForCompanyID(companyID, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForRecPeriodID(recPeriodID, cmd, cmdParams);

            IDbDataParameter cmdAccountIDList = cmd.CreateParameter();
            cmdAccountIDList.ParameterName = "@AccountTable";
            cmdAccountIDList.Value = dtAccount;
            cmdParams.Add(cmdAccountIDList);

            ServiceHelper.AddCommonParametersForActionTypeID(actionTypeID, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForChangeSourceIDSRA(changeSourceIDSRA, cmd, cmdParams);

            return cmd;
        }
        internal ReconciliationPeriodInfo GetReconciliationPeriodInfoForReopen(int? CompanyID)
        {
            System.Data.IDbCommand cmd = null;
            ReconciliationPeriodInfo oReconciliationPeriodInfo = null;
            IDataReader dr = null;

            try
            {
                cmd = CreateGetReconciliationPeriodInfoForReopenCommand(CompanyID);
                cmd.Connection = this.CreateConnection();
                cmd.Connection.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    oReconciliationPeriodInfo = this.MapObject(dr);
                }
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
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

            return oReconciliationPeriodInfo;
        }
        private IDbCommand CreateGetReconciliationPeriodInfoForReopenCommand(int? CompanyID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("[dbo].[usp_GET_ReconciliationPeriodInfoForReopen]");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (CompanyID.HasValue)
                parCompanyID.Value = CompanyID.Value;
            else
                parCompanyID.Value = DBNull.Value;
            cmdParams.Add(parCompanyID);

            return cmd;
        }
        public int ReOpenRecPeriod(ReconciliationPeriodInfo oReconciliationPeriodInfo, short actionTypeID, short changeSourceIDSRA)
        {
            int rowsAffected = 0;
            IDbCommand cmd = null;
            using (TransactionScope scope = new TransactionScope())
            {
                using (IDbConnection con = this.CreateConnection())
                {
                    try
                    {
                        con.Open();
                        cmd = this.CreateReOpenRecPeriodCommand( oReconciliationPeriodInfo, actionTypeID, changeSourceIDSRA);
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        rowsAffected = Convert.ToInt32(((IDbDataParameter)cmd.Parameters["@rowsEffectedByupdate"]).Value);
                        scope.Complete();
                    }
                    finally
                    {
                        if (cmd != null)
                            cmd.Dispose();
                    }
                }
            }
            return rowsAffected;
        }

        private IDbCommand CreateReOpenRecPeriodCommand(ReconciliationPeriodInfo oReconciliationPeriodInfo, short actionTypeID, short changeSourceIDSRA)
        {
            IDbCommand cmd = this.CreateCommand("[dbo].[usp_UPD_ReOpenRecPeriod]");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdRecPeriodId = cmd.CreateParameter();
            cmdRecPeriodId.ParameterName = "@RecPeriodID";
            if (oReconciliationPeriodInfo.ReconciliationPeriodID.HasValue)
                cmdRecPeriodId.Value = oReconciliationPeriodInfo.ReconciliationPeriodID.Value;
            else
                cmdRecPeriodId.Value = DBNull.Value;
            cmdParams.Add(cmdRecPeriodId);

            IDbDataParameter cmdRecCloseDate = cmd.CreateParameter();
            cmdRecCloseDate.ParameterName = "@RecCloseDate";
            cmdRecCloseDate.Value = oReconciliationPeriodInfo.ReconciliationCloseDate;
            cmdParams.Add(cmdRecCloseDate);

            IDbDataParameter cmdRevisedDate = cmd.CreateParameter();
            cmdRevisedDate.ParameterName = "@dateRevised";
            cmdRevisedDate.Value = oReconciliationPeriodInfo.DateRevised;
            cmdParams.Add(cmdRevisedDate);

            IDbDataParameter cmdRevisedBy = cmd.CreateParameter();
            cmdRevisedBy.ParameterName = "@revisedBy";
            cmdRevisedBy.Value = oReconciliationPeriodInfo.RevisedBy;
            cmdParams.Add(cmdRevisedBy);         

            ServiceHelper.AddCommonParametersForActionTypeID(actionTypeID, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForChangeSourceIDSRA(changeSourceIDSRA, cmd, cmdParams);

            IDbDataParameter cmdRowsEffected = cmd.CreateParameter();
            cmdRowsEffected.ParameterName = "@rowsEffectedByupdate";
            cmdRowsEffected.DbType = System.Data.DbType.Int32;
            cmdRowsEffected.Direction = ParameterDirection.Output;
            cmdParams.Add(cmdRowsEffected);

            return cmd;
        }

        internal List<RecPeriodStatusDetailInfo> GetRecPeriodStatusDetail(int? recPeriodID)
        {
            System.Data.IDbCommand cmd = null;
            RecPeriodStatusDetailInfo oRecPeriodStatusDetailInfo = null;
            List<RecPeriodStatusDetailInfo> oRecPeriodStatusDetailInfoList =  new List<RecPeriodStatusDetailInfo> ();
            IDataReader dr = null;

            try
            {
                cmd = CreateGetRecPeriodStatusDetailCommand(recPeriodID);
                cmd.Connection = this.CreateConnection();
                cmd.Connection.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    oRecPeriodStatusDetailInfo = MapRecPeriodStatusDetailObject(dr);
                    oRecPeriodStatusDetailInfoList.Add(oRecPeriodStatusDetailInfo);
                }
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
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

            return oRecPeriodStatusDetailInfoList;
        }
        private IDbCommand CreateGetRecPeriodStatusDetailCommand(int? recPeriodID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("[dbo].[usp_GET_GetRecPeriodStatusDetail]");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            if (recPeriodID.HasValue)
                parRecPeriodID.Value = recPeriodID.Value;
            else
                parRecPeriodID.Value = DBNull.Value;
            cmdParams.Add(parRecPeriodID);

            return cmd;
        }
        private RecPeriodStatusDetailInfo MapRecPeriodStatusDetailObject(IDataReader dr)
        {
            RecPeriodStatusDetailInfo oRecPeriodStatusDetailInfo = new RecPeriodStatusDetailInfo();
            oRecPeriodStatusDetailInfo.RecPeriodStatusDetailID = dr.GetInt32Value("RecPeriodStatusDetailID");
            oRecPeriodStatusDetailInfo.RecPeriodID = dr.GetInt32Value("RecPeriodID");
            oRecPeriodStatusDetailInfo.StatusDate = dr.GetDateValue("StatusDate");
            oRecPeriodStatusDetailInfo.StatusID = dr.GetInt16Value("StatusID");
            oRecPeriodStatusDetailInfo.ReconciliationPeriodStatus = dr.GetStringValue("ReconciliationPeriodStatus");
            oRecPeriodStatusDetailInfo.ReconciliationPeriodStatusLabelID = dr.GetInt32Value("ReconciliationPeriodStatusLabelID");
            UserHdrInfo oUserHdrInfo = new UserHdrInfo();
            oUserHdrInfo.UserID = dr.GetInt16Value("UserID");
            oUserHdrInfo.FirstName = dr.GetStringValue("FirstName");
            oUserHdrInfo.LastName = dr.GetStringValue("LastName");
            oRecPeriodStatusDetailInfo.AddedByUserInfo = oUserHdrInfo;
            return oRecPeriodStatusDetailInfo;        
        }
    }
}