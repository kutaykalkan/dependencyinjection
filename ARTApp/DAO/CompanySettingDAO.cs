


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Data;

namespace SkyStem.ART.App.DAO
{
    public class CompanySettingDAO : CompanySettingDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CompanySettingDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        internal int? UpdateByCompanyID(CompanySettingInfo oCompanySettingInfo)
        {
            int result = 0;
            System.Data.IDbCommand oCommand = null;
            try
            {
                oCommand = CreateUpdateByCompanyIDCommand(oCompanySettingInfo);
                oCommand.Connection = this.CreateConnection();
                oCommand.Connection.Open();
                result = Convert.ToInt32(oCommand.ExecuteNonQuery());
                return result;
            }
            //catch (Exception ex)
            //{
            //    //return 0;
            //    throw ex;
            //}
            finally
            {
                if (oCommand != null)
                {
                    if (oCommand.Connection != null && oCommand.Connection.State != ConnectionState.Closed)
                    {
                        oCommand.Connection.Close();
                        oCommand.Connection.Dispose();
                    }
                    oCommand.Dispose();
                }
            }
        }

        public int UpdateCompanySettingForKeyCountByCompanyID(int companyID, short keyCount, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            IDbCommand oCommand = null;
            oCommand = this.GetCompanySettingUpdateCommandForKeyCountByCopanyID(companyID, keyCount);
            oCommand.Connection = oConnection;
            oCommand.Transaction = oTransaction;
            return oCommand.ExecuteNonQuery();
        }

        private IDbCommand GetCompanySettingUpdateCommandForKeyCountByCopanyID(int companyID, short keyCount)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_UPD_CompanySettingForKeyCount");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = companyID;

            System.Data.IDbDataParameter parKeyCount = cmd.CreateParameter();
            parKeyCount.ParameterName = "@KeyCount";
            parKeyCount.Value = keyCount;

            cmdParams.Add(parCompanyID);
            cmdParams.Add(parKeyCount);

            return cmd;
        }
        public void SaveCompanySetting(CompanyHdrInfo oCompanyHdrInfo)
        {
            IDbCommand IDBCmmd = this.CreateCommand("usp_INS_CompanySetting");
            IDBCmmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = IDBCmmd.Parameters;

            IDbDataParameter cmdParamCompanyId = IDBCmmd.CreateParameter();
            cmdParamCompanyId.ParameterName = "@companyid";
            cmdParamCompanyId.Value = oCompanyHdrInfo.CompanyID;
            cmdParams.Add(cmdParamCompanyId);

            IDbDataParameter cmdParamAddedBy = IDBCmmd.CreateParameter();
            cmdParamAddedBy.ParameterName = "@AddedBy";
            cmdParamAddedBy.Value = oCompanyHdrInfo.AddedBy;
            cmdParams.Add(cmdParamAddedBy);

            IDbDataParameter cmdParamDateAdded = IDBCmmd.CreateParameter();
            cmdParamDateAdded.ParameterName = "@DateAdded";
            cmdParamDateAdded.Value = oCompanyHdrInfo.DateAdded;
            cmdParams.Add(cmdParamDateAdded);

            using (IDbConnection cnn = this.CreateConnection())
            {
                cnn.Open();
                IDBCmmd.Connection = cnn;

                IDBCmmd.ExecuteNonQuery();
            }
        }


        public void UpdateCompanySetting(CompanyHdrInfo oCompanyHdrInfo, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            try
            {
                IDbCommand IDBCmmd = this.CreateCommand("usp_UPD_CompanySetting");
                IDBCmmd.CommandType = CommandType.StoredProcedure;
                IDataParameterCollection cmdParams = IDBCmmd.Parameters;

                IDbDataParameter cmdParamCompanyId = IDBCmmd.CreateParameter();
                cmdParamCompanyId.ParameterName = "@companyid";
                cmdParamCompanyId.Value = oCompanyHdrInfo.CompanyID;
                cmdParams.Add(cmdParamCompanyId);

                IDbDataParameter cmdParamAddedBy = IDBCmmd.CreateParameter();
                cmdParamAddedBy.ParameterName = "@AddedBy";
                cmdParamAddedBy.Value = oCompanyHdrInfo.AddedBy;
                cmdParams.Add(cmdParamAddedBy);

                IDbDataParameter cmdParamDateAdded = IDBCmmd.CreateParameter();
                cmdParamDateAdded.ParameterName = "@DateAdded";
                cmdParamDateAdded.Value = oCompanyHdrInfo.DateAdded;
                cmdParams.Add(cmdParamDateAdded);

                IDBCmmd.Connection = oConnection;
                IDBCmmd.Transaction = oTransaction;
                IDBCmmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IList<CompanySettingInfo> SelectCompanyMaterialityType(int? reconciliationPeriodID)
        {

            //System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CompanySettingByCompanyID");
            System.Data.IDbCommand cmd = this.CreateCommand("usp_GET_MaterialityTypeByRecPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationPeriodID";
            par.Value = reconciliationPeriodID;
            cmdParams.Add(par);

            return this.Select(cmd);
        }


        //TODO: handle transaction fail
        internal int? UpdateMaterialityTypeSetting(int? reconciliationPeriodID, IList<FSCaptionMaterialityInfo> oFSCaptionMaterialityInfoCollection, CompanySettingInfo oCompanySettingInfo, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            int result = 0;
            System.Data.IDbCommand oCommand = null;
            DataTable dtFSCaptionMateriality = ServiceHelper.ConvertFSCaptionWithMaterialityInfoCollectionToDataTable(oFSCaptionMaterialityInfoCollection);

            oCommand = CreateUpdateMaterialityTypeCommand(reconciliationPeriodID, dtFSCaptionMateriality, oCompanySettingInfo);
            oCommand.Connection = oConnection;
            //oCommand.Connection.Open();
            oCommand.Transaction = oTransaction;
            result = Convert.ToInt32(oCommand.ExecuteNonQuery());
            return result;

        }

        internal int? UpdateDualLevelReviewType(int? reconciliationPeriodID, CompanySettingInfo oCompanySettingInfo, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            int result = 0;
            System.Data.IDbCommand oCommand = null;
            oCommand = CreateUpdateDualLevelReviewTypeCommand(reconciliationPeriodID, oCompanySettingInfo);
            oCommand.Connection = oConnection;
            //oCommand.Connection.Open();
            oCommand.Transaction = oTransaction;
            result = Convert.ToInt32(oCommand.ExecuteNonQuery());
            return result;

        }
        protected System.Data.IDbCommand CreateUpdateDualLevelReviewTypeCommand(int? reconciliationPeriodID, CompanySettingInfo oCompanySettingInfo)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_DualLevelReviewType");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parReconciliationPeriodID = cmd.CreateParameter();
            parReconciliationPeriodID.ParameterName = "@InputReconciliationPeriodID";
            if (reconciliationPeriodID.HasValue)
                parReconciliationPeriodID.Value = reconciliationPeriodID.Value;
            else
                parReconciliationPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationPeriodID);

            System.Data.IDbDataParameter parInputDualLevelReviewTypeID = cmd.CreateParameter();
            parInputDualLevelReviewTypeID.ParameterName = "@InputDualLevelReviewTypeID";
            if (oCompanySettingInfo.DualLevelReviewTypeID.HasValue)
                parInputDualLevelReviewTypeID.Value = oCompanySettingInfo.DualLevelReviewTypeID.Value;
            else
                parInputDualLevelReviewTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parInputDualLevelReviewTypeID);

            return cmd;
        }


        internal int? UpdateCompanyDayType(int? reconciliationPeriodID, string addedBy, DateTime? dateAdded, CompanySettingInfo oCompanySettingInfo, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            int result = 0;
            System.Data.IDbCommand oCommand = null;
            oCommand = CreateUpdateCompanyDayTypeCommand(reconciliationPeriodID, addedBy, dateAdded, oCompanySettingInfo);
            oCommand.Connection = oConnection;
            //oCommand.Connection.Open();
            oCommand.Transaction = oTransaction;
            result = Convert.ToInt32(oCommand.ExecuteNonQuery());
            return result;
        }
        protected System.Data.IDbCommand CreateUpdateCompanyDayTypeCommand(int? reconciliationPeriodID, string addedBy, DateTime? dateAdded,  CompanySettingInfo oCompanySettingInfo)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_CompanyDayType");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parReconciliationPeriodID = cmd.CreateParameter();
            parReconciliationPeriodID.ParameterName = "@RecPeriodID";
            if (reconciliationPeriodID.HasValue)
                parReconciliationPeriodID.Value = reconciliationPeriodID.Value;
            else
                parReconciliationPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationPeriodID);

            System.Data.IDbDataParameter parDayTypeID = cmd.CreateParameter();
            parDayTypeID.ParameterName = "@DayTypeID";
            if (oCompanySettingInfo.DayTypeID.HasValue)
                parDayTypeID.Value = oCompanySettingInfo.DayTypeID.Value;
            else
                parDayTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parDayTypeID);

            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!string.IsNullOrEmpty(addedBy))
                parAddedBy.Value = addedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (dateAdded.HasValue)
                parDateAdded.Value = dateAdded.Value;
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            return cmd;
        }

        internal int? MarkForReprocessing(bool? isMaterialityActivated, int? reconciliationPeriodID
            , IList<FSCaptionMaterialityInfo> oFSCaptionMaterialityInfoCollection, CompanySettingInfo oCompanySettingInfo
            , IDbConnection oConnection, IDbTransaction oTransaction, ARTEnums.GLDataProcessingStatus glDataToBeProcessedStatusID,
            string revisedBy, DateTime dateRevised)
        {
            int result = 0;
            System.Data.IDbCommand oCommand = null;
            DataTable dtFSCaptionMateriality = ServiceHelper.ConvertFSCaptionWithMaterialityInfoCollectionToDataTable(oFSCaptionMaterialityInfoCollection);

            oCommand = CreateUpdateMaterialityTypeCommand(reconciliationPeriodID, dtFSCaptionMateriality, oCompanySettingInfo);
            oCommand.CommandText = "usp_UPD_MarkToBeReprocessed";

            System.Data.IDbDataParameter parIsMaterialityActivated = oCommand.CreateParameter();
            parIsMaterialityActivated.ParameterName = "@IsMaterialityActivated";
            if (isMaterialityActivated.HasValue)
                parIsMaterialityActivated.Value = isMaterialityActivated.Value;
            else
                parIsMaterialityActivated.Value = System.DBNull.Value;
            oCommand.Parameters.Add(parIsMaterialityActivated);

            IDbDataParameter parGLDataToBeProcessedStatusID = oCommand.CreateParameter();
            parGLDataToBeProcessedStatusID.ParameterName = "@GLDataToBeProcessedStatusID";
            parGLDataToBeProcessedStatusID.Value = (short)glDataToBeProcessedStatusID;
            oCommand.Parameters.Add(parGLDataToBeProcessedStatusID);

            IDbDataParameter parRevisedBy = oCommand.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            parRevisedBy.Value = revisedBy;
            oCommand.Parameters.Add(parRevisedBy);

            IDbDataParameter parDateRevised = oCommand.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            parDateRevised.Value = dateRevised;
            oCommand.Parameters.Add(parDateRevised);

            oCommand.Connection = oConnection;
            //oCommand.Connection.Open();
            oCommand.Transaction = oTransaction;
            result = Convert.ToInt32(oCommand.ExecuteNonQuery());
            return result;
        }




        //TODO: handle transaction fail
        internal int? UpdateByCompanyID(CompanySettingInfo oCompanySettingInfo, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            int result = 0;
            System.Data.IDbCommand oCommand = null;
            oCommand = CreateUpdateByCompanyIDCommand(oCompanySettingInfo);
            oCommand.Connection = oConnection;
            oCommand.Transaction = oTransaction;
            result = Convert.ToInt32(oCommand.ExecuteNonQuery());
            return result;

        }


        protected System.Data.IDbCommand CreateUpdateMaterialityTypeCommand(int? reconciliationPeriodID, DataTable tblFSCaptionMateriality, CompanySettingInfo oCompanySettingInfo)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_MaterialityCapability");
            //System.Data.IDbCommand cmd = this.CreateCommand("usp_UPD_CompanySettingByCompanyID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter FSCaptionWithMaterialityInfoTable = cmd.CreateParameter();
            FSCaptionWithMaterialityInfoTable.ParameterName = "@tblFSCaptionWithMaterialityInfo";
            FSCaptionWithMaterialityInfoTable.Value = tblFSCaptionMateriality;
            cmdParams.Add(FSCaptionWithMaterialityInfoTable);

            System.Data.IDbDataParameter parReconciliationPeriodID = cmd.CreateParameter();
            parReconciliationPeriodID.ParameterName = "@InputReconciliationPeriodID";
            if (reconciliationPeriodID.HasValue)
                parReconciliationPeriodID.Value = reconciliationPeriodID.Value;
            else
                parReconciliationPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationPeriodID);

            System.Data.IDbDataParameter parCompanyMaterialityThreshold = cmd.CreateParameter();
            parCompanyMaterialityThreshold.ParameterName = "@InputCompanyMaterialityThreshold";
            if (!oCompanySettingInfo.IsCompanyMaterialityThresholdNull)
                parCompanyMaterialityThreshold.Value = oCompanySettingInfo.CompanyMaterialityThreshold;
            else
                parCompanyMaterialityThreshold.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyMaterialityThreshold);

            System.Data.IDbDataParameter parMaterialityTypeID = cmd.CreateParameter();
            parMaterialityTypeID.ParameterName = "@InputMaterialityTypeID";
            if (!oCompanySettingInfo.IsMaterialityTypeIDNull)
                parMaterialityTypeID.Value = oCompanySettingInfo.MaterialityTypeID;
            else
                parMaterialityTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parMaterialityTypeID);

            return cmd;
        }

        protected System.Data.IDbCommand CreateUpdateByCompanyIDCommand(CompanySettingInfo oCompanySettingInfo)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_UPD_CompanySettingByCompanyID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            //System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            //parAddedBy.ParameterName = "@AddedBy";
            //if (!oCompanySettingInfo.IsAddedByNull)
            //    parAddedBy.Value = oCompanySettingInfo.AddedBy;
            //else
            //    parAddedBy.Value = System.DBNull.Value;
            //cmdParams.Add(parAddedBy);

            //System.Data.IDbDataParameter parAllowCertificationLockdown = cmd.CreateParameter();
            //parAllowCertificationLockdown.ParameterName = "@AllowCertificationLockdown";
            //if (!oCompanySettingInfo.IsAllowCertificationLockdownNull)
            //    parAllowCertificationLockdown.Value = oCompanySettingInfo.AllowCertificationLockdown;
            //else
            //    parAllowCertificationLockdown.Value = System.DBNull.Value;
            //cmdParams.Add(parAllowCertificationLockdown);

            //System.Data.IDbDataParameter parAllowCustomReconciliationFrequency = cmd.CreateParameter();
            //parAllowCustomReconciliationFrequency.ParameterName = "@AllowCustomReconciliationFrequency";
            //if (!oCompanySettingInfo.IsAllowCustomReconciliationFrequencyNull)
            //    parAllowCustomReconciliationFrequency.Value = oCompanySettingInfo.AllowCustomReconciliationFrequency;
            //else
            //    parAllowCustomReconciliationFrequency.Value = System.DBNull.Value;
            //cmdParams.Add(parAllowCustomReconciliationFrequency);

            System.Data.IDbDataParameter parAllowReviewNotesDeletion = cmd.CreateParameter();
            parAllowReviewNotesDeletion.ParameterName = "@AllowReviewNotesDeletion";
            if (!oCompanySettingInfo.IsAllowReviewNotesDeletionNull)
                parAllowReviewNotesDeletion.Value = oCompanySettingInfo.AllowReviewNotesDeletion;
            else
                parAllowReviewNotesDeletion.Value = System.DBNull.Value;
            cmdParams.Add(parAllowReviewNotesDeletion);

            //System.Data.IDbDataParameter parBaseCurrencyCode = cmd.CreateParameter();
            //parBaseCurrencyCode.ParameterName = "@BaseCurrencyCode";
            //if (!oCompanySettingInfo.IsBaseCurrencyCodeNull)
            //    parBaseCurrencyCode.Value = oCompanySettingInfo.BaseCurrencyCode;
            //else
            //    parBaseCurrencyCode.Value = System.DBNull.Value;
            //cmdParams.Add(parBaseCurrencyCode);

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (!oCompanySettingInfo.IsCompanyIDNull)
                parCompanyID.Value = oCompanySettingInfo.CompanyID;
            else
                parCompanyID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyID);

            //System.Data.IDbDataParameter parCompanyMaterialityThreshold = cmd.CreateParameter();
            //parCompanyMaterialityThreshold.ParameterName = "@CompanyMaterialityThreshold";
            //if (!oCompanySettingInfo.IsCompanyMaterialityThresholdNull)
            //    parCompanyMaterialityThreshold.Value = oCompanySettingInfo.CompanyMaterialityThreshold;
            //else
            //    parCompanyMaterialityThreshold.Value = System.DBNull.Value;
            //cmdParams.Add(parCompanyMaterialityThreshold);

            //System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            //parDateAdded.ParameterName = "@DateAdded";
            //if (!oCompanySettingInfo.IsDateAddedNull)
            //    parDateAdded.Value = oCompanySettingInfo.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            //else
            //    parDateAdded.Value = System.DBNull.Value;
            //cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (!oCompanySettingInfo.IsDateRevisedNull)
                parDateRevised.Value = oCompanySettingInfo.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            //System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            //parHostName.ParameterName = "@HostName";
            //if (!oCompanySettingInfo.IsHostNameNull)
            //    parHostName.Value = oCompanySettingInfo.HostName;
            //else
            //    parHostName.Value = System.DBNull.Value;
            //cmdParams.Add(parHostName);

            //System.Data.IDbDataParameter parMaterialityTypeID = cmd.CreateParameter();
            //parMaterialityTypeID.ParameterName = "@MaterialityTypeID";
            //if (!oCompanySettingInfo.IsMaterialityTypeIDNull)
            //    parMaterialityTypeID.Value = oCompanySettingInfo.MaterialityTypeID;
            //else
            //    parMaterialityTypeID.Value = System.DBNull.Value;
            //cmdParams.Add(parMaterialityTypeID);

            System.Data.IDbDataParameter parMaximumDocumentUploadSize = cmd.CreateParameter();
            parMaximumDocumentUploadSize.ParameterName = "@MaximumDocumentUploadSize";
            if (!oCompanySettingInfo.IsMaximumDocumentUploadSizeNull)
                parMaximumDocumentUploadSize.Value = oCompanySettingInfo.MaximumDocumentUploadSize;
            else
                parMaximumDocumentUploadSize.Value = System.DBNull.Value;
            cmdParams.Add(parMaximumDocumentUploadSize);

            System.Data.IDbDataParameter parCurrentReconciliationPeriodID = cmd.CreateParameter();
            parCurrentReconciliationPeriodID.ParameterName = "@CurrentReconciliationPeriodID";
            if (!oCompanySettingInfo.IsCurrentReconciliationPeriodIDNull)
                parCurrentReconciliationPeriodID.Value = oCompanySettingInfo.CurrentReconciliationPeriodID;
            else
                parCurrentReconciliationPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parCurrentReconciliationPeriodID);

            //System.Data.IDbDataParameter parReportingCurrencyCode = cmd.CreateParameter();
            //parReportingCurrencyCode.ParameterName = "@ReportingCurrencyCode";
            //if (!oCompanySettingInfo.IsReportingCurrencyCodeNull)
            //    parReportingCurrencyCode.Value = oCompanySettingInfo.ReportingCurrencyCode;
            //else
            //    parReportingCurrencyCode.Value = System.DBNull.Value;
            //cmdParams.Add(parReportingCurrencyCode);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!oCompanySettingInfo.IsRevisedByNull)
                parRevisedBy.Value = oCompanySettingInfo.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            //System.Data.IDbDataParameter parUnexplainedVarianceThreshold = cmd.CreateParameter();
            //parUnexplainedVarianceThreshold.ParameterName = "@UnexplainedVarianceThreshold";
            //if (!oCompanySettingInfo.IsUnexplainedVarianceThresholdNull)
            //    parUnexplainedVarianceThreshold.Value = oCompanySettingInfo.UnexplainedVarianceThreshold;
            //else
            //    parUnexplainedVarianceThreshold.Value = System.DBNull.Value;
            //cmdParams.Add(parUnexplainedVarianceThreshold);

            //System.Data.IDbDataParameter pkparCompanySettingID = cmd.CreateParameter();
            //pkparCompanySettingID.ParameterName = "@CompanySettingID";
            //pkparCompanySettingID.Value = oCompanySettingInfo.CompanySettingID;
            //cmdParams.Add(pkparCompanySettingID);

            System.Data.IDbDataParameter parCurrentFinancialYearID = cmd.CreateParameter();
            parCurrentFinancialYearID.ParameterName = "@CurrentFinancialYearID";
            if (!oCompanySettingInfo.IsCurrentFinancialYearIDNull)
                parCurrentFinancialYearID.Value = oCompanySettingInfo.CurrentFinancialYearID;
            else
                parCurrentFinancialYearID.Value = System.DBNull.Value;
            cmdParams.Add(parCurrentFinancialYearID);

            return cmd;
        }

        public void UpdateReconciliationPeriodForReProcessing(int? RecPeriodID, ARTEnums.GLDataProcessingStatus glDataToBeProcessedStatusID,
            string revisedBy, DateTime dateRevised, IDbConnection oConnection, IDbTransaction oTransaction)
        {

            IDbCommand oCommand = this.CreateUpdateReconciliationPeriodForReProcessingCommand(RecPeriodID, glDataToBeProcessedStatusID, revisedBy, dateRevised);
            oCommand.Connection = oConnection;
            oCommand.Transaction = oTransaction;
            oCommand.ExecuteNonQuery();

        }

        private IDbCommand CreateUpdateReconciliationPeriodForReProcessingCommand(int? RecPeriodID, ARTEnums.GLDataProcessingStatus glDataToBeProcessedStatusID, string revisedBy, DateTime dateRevised)
        {
            IDbCommand cmd = this.CreateCommand("usp_UPD_GLDataProcessingStatus");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            if (RecPeriodID.HasValue)
                parRecPeriodID.Value = RecPeriodID.Value;
            else
                parRecPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parRecPeriodID);

            IDbDataParameter parGLDataToBeProcessedStatusID = cmd.CreateParameter();
            parGLDataToBeProcessedStatusID.ParameterName = "@GLDataProcessingStatusID";
            parGLDataToBeProcessedStatusID.Value = (short)glDataToBeProcessedStatusID;
            cmdParams.Add(parGLDataToBeProcessedStatusID);

            IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            parRevisedBy.Value = revisedBy;
            cmdParams.Add(parRevisedBy);

            IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            parDateRevised.Value = dateRevised;
            cmdParams.Add(parDateRevised);



            return cmd;

        }

        public IList<CompanySettingInfo> SelectCompanyDualLevelReviewType(int? reconciliationPeriodID)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("usp_GET_DualLevelReviewTypeByRecPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationPeriodID";
            par.Value = reconciliationPeriodID;
            cmdParams.Add(par);

            return this.Select(cmd);
        }

        public IList<CompanySettingInfo> SelectCompanyDayType(int? reconciliationPeriodID)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("usp_GET_CompanyDayTypeByRecPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationPeriodID";
            par.Value = reconciliationPeriodID;
            cmdParams.Add(par);

            return this.Select(cmd);
        }

        public IList<CompanySettingInfo> SelectCompanyRCCLValidationType(int? reconciliationPeriodID)
        {
            using (System.Data.IDbCommand cmd = this.CreateCommand("usp_GET_CompanyRCCValidationTypeByRecPeriodID"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                IDataParameterCollection cmdParams = cmd.Parameters;
                System.Data.IDbDataParameter par = cmd.CreateParameter();
                par.ParameterName = "@ReconciliationPeriodID";
                par.Value = reconciliationPeriodID;
                cmdParams.Add(par);
                return this.Select(cmd);
            }
        }
        internal int? UpdateRCCValidationType(int? reconciliationPeriodID, CompanySettingInfo oCompanySettingInfo, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            int result = 0;
            System.Data.IDbCommand oCommand = null;
            oCommand = CreateUpdateRCCValidationTypeCommand(reconciliationPeriodID, oCompanySettingInfo);
            oCommand.Connection = oConnection;
            oCommand.Transaction = oTransaction;
            result = Convert.ToInt32(oCommand.ExecuteNonQuery());
            return result;

        }
        protected System.Data.IDbCommand CreateUpdateRCCValidationTypeCommand(int? reconciliationPeriodID, CompanySettingInfo oCompanySettingInfo)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("[dbo].[usp_INS_CompanyRCCValidationType]");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parReconciliationPeriodID = cmd.CreateParameter();
            parReconciliationPeriodID.ParameterName = "@InputReconciliationPeriodID";
            if (reconciliationPeriodID.HasValue)
                parReconciliationPeriodID.Value = reconciliationPeriodID.Value;
            else
                parReconciliationPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationPeriodID);

            System.Data.IDbDataParameter parmInputCompanyRCCValidationTypeID = cmd.CreateParameter();
            parmInputCompanyRCCValidationTypeID.ParameterName = "@InputCompanyRCCValidationTypeID";
            if (oCompanySettingInfo.RCCValidationTypeID.HasValue)
                parmInputCompanyRCCValidationTypeID.Value = oCompanySettingInfo.RCCValidationTypeID.Value;
            else
                parmInputCompanyRCCValidationTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parmInputCompanyRCCValidationTypeID);

            IDbDataParameter cmdParamModifiedBy = cmd.CreateParameter();
            cmdParamModifiedBy.ParameterName = "@ModifiedBy";
            cmdParamModifiedBy.Value = oCompanySettingInfo.AddedBy;
            cmdParams.Add(cmdParamModifiedBy);

            IDbDataParameter cmdParamDateModified = cmd.CreateParameter();
            cmdParamDateModified.ParameterName = "@DateModified";
            cmdParamDateModified.Value = oCompanySettingInfo.DateAdded;
            cmdParams.Add(cmdParamDateModified);

            return cmd;
        }


        internal void UpdateCompanyDataStorageCapacityAndCurrentUsage(int CurrentCompanyID, decimal FileSizeInMB, string RevisedBy, DateTime DateRevised, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            using (IDbCommand cmd = CreateUpdateCompanyDataStorageCapacityAndCurrentUsage(CurrentCompanyID, FileSizeInMB,RevisedBy,DateRevised))
            {
                cmd.Connection = oConnection;
                cmd.Transaction = oTransaction;
                cmd.ExecuteNonQuery();
            }
        }
        private IDbCommand CreateUpdateCompanyDataStorageCapacityAndCurrentUsage(int CurrentCompanyID, decimal FileSizeInMB,string RevisedBy,DateTime DateRevised)
        {
            IDbCommand cmd = CreateCommand("usp_UPD_CurrentUsageForComapanyAdded");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parCurrentCompanyID = cmd.CreateParameter();
            parCurrentCompanyID.ParameterName = "@CompanyID";
            parCurrentCompanyID.Value = CurrentCompanyID;
            cmdParams.Add(parCurrentCompanyID);

            System.Data.IDbDataParameter parFileSizeInMB = cmd.CreateParameter();
            parFileSizeInMB.ParameterName = "@FileSize";
            parFileSizeInMB.Value = FileSizeInMB;
            cmdParams.Add(parFileSizeInMB);

            System.Data.IDbDataParameter parModifiedBy = cmd.CreateParameter();
            parModifiedBy.ParameterName = "@RevisedBy";
            parModifiedBy.Value = RevisedBy;
            cmdParams.Add(parModifiedBy);

            System.Data.IDbDataParameter parDateModified = cmd.CreateParameter();
            parDateModified.ParameterName = "@DateRevised";
            parDateModified.Value = DateRevised;
            cmdParams.Add(parDateModified);

            return cmd;
        }
    }
}