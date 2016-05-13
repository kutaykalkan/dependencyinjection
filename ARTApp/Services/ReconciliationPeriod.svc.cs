using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.DAO;
using System.Data;
using SkyStem.ART.App.Utility;
using System.Data.SqlClient;
using SkyStem.ART.Client.Data;

namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "ReconciliationPeriod" here, you must also update the reference to "ReconciliationPeriod" in Web.config.
    public class ReconciliationPeriod : IReconciliationPeriod
    {
        public int InsertReconciliationPeriod(List<ReconciliationPeriodInfo> oRecPeriodInfoCollection
            , IDbConnection oConnection, IDbTransaction oTransaction, int companyID, int dataimportID, DateTime? currentReconciliationPeriodEndDate, AppUserInfo oAppUserInfo)
        {
            ServiceHelper.SetConnectionString(oAppUserInfo);
            ReconciliationPeriodDAO oRecPeriodDAO = new ReconciliationPeriodDAO(oAppUserInfo);
            DataTable dt = DataImportServiceHelper.ConvertRecPeriodListToDataTable(oRecPeriodInfoCollection);
            return oRecPeriodDAO.InsertReconciliationPeriodDataTable(dt, oConnection, oTransaction, companyID, dataimportID, currentReconciliationPeriodEndDate);
        }



        //public DateTime? GetCurrentPeriodByCompanyId(int companyID)
        //{
        //    ReconciliationPeriodDAO oRecPeriodDAO = new ReconciliationPeriodDAO(oAppUserInfo);
        //    return oRecPeriodDAO.GetCurrentReconciliationPeriodByCompanyID(companyID);
        //}

        public ReconciliationPeriodInfo GetMaxCurrentPeriodByCompanyId(int? companyID, AppUserInfo oAppUserInfo)
        {
            ReconciliationPeriodInfo oReconciliationPeriodInfo = null;
            ServiceHelper.SetConnectionString(oAppUserInfo);
            ReconciliationPeriodDAO oRecPeriodDAO = new ReconciliationPeriodDAO(oAppUserInfo);
            oReconciliationPeriodInfo = oRecPeriodDAO.GetCurrentReconciliationPeriod(companyID, true);
            return oReconciliationPeriodInfo;
        }
        public ReconciliationPeriodInfo GetMinCurrentPeriodByCompanyId(int? companyID, AppUserInfo oAppUserInfo)
        {
            ReconciliationPeriodInfo oReconciliationPeriodInfo = null;
            ServiceHelper.SetConnectionString(oAppUserInfo);
            ReconciliationPeriodDAO oRecPeriodDAO = new ReconciliationPeriodDAO(oAppUserInfo);
            oReconciliationPeriodInfo = oRecPeriodDAO.GetCurrentReconciliationPeriod(companyID, false);
            return oReconciliationPeriodInfo;
        }

        public int? GetDataImportProcessingStatus(int? newCurrentRecPeriodID, int? dataImportTypeID, AppUserInfo oAppUserInfo)
        {
            int? dataImportProcessingStatusID = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ReconciliationPeriodDAO oReconciliationPeriodDAO = new ReconciliationPeriodDAO(oAppUserInfo);
                dataImportProcessingStatusID = oReconciliationPeriodDAO.GetDataImportProcessingStatus(newCurrentRecPeriodID, dataImportTypeID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return dataImportProcessingStatusID;
        }

        /// <summary>
        /// Get the Rec Period Status for the Rec Period Passed
        /// </summary>
        /// <param name="RecPeriodID"></param>
        /// <returns>Rec Period Status Info</returns>
        public ReconciliationPeriodStatusMstInfo GetRecPeriodStatus(int? RecPeriodID, AppUserInfo oAppUserInfo)
        {
            ReconciliationPeriodStatusMstInfo oReconciliationPeriodStatusMstInfo = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ReconciliationPeriodStatusMstDAO oReconciliationPeriodStatusMstDAO = new ReconciliationPeriodStatusMstDAO(oAppUserInfo);
                oReconciliationPeriodStatusMstInfo = oReconciliationPeriodStatusMstDAO.GetRecPeriodStatus(RecPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oReconciliationPeriodStatusMstInfo;
        }

        public DateTime? GetDueDateByUserRoleID(int roleId, short preparerRoleID, short reviewerRoleId, short approverRoleId, int recPeriodId, AppUserInfo oAppUserInfo)
        {
            DateTime? dueDate = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ReconciliationPeriodDAO oReconciliationPeriodDAO = new ReconciliationPeriodDAO(oAppUserInfo);
                dueDate = oReconciliationPeriodDAO.GetDueDateByUserRoleID(roleId, preparerRoleID, reviewerRoleId, approverRoleId, recPeriodId);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return dueDate;
        }

        public List<int> GetIncompleteRequirementToMarkOpen(int? RecPeriodID, AppUserInfo oAppUserInfo)
        {
            List<int> oIncompleteRequirementCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ReconciliationPeriodDAO oReconciliationPeriodDAO = new ReconciliationPeriodDAO(oAppUserInfo);
                oIncompleteRequirementCollection = oReconciliationPeriodDAO.GetIncompleteRequirementToMarkOpen(RecPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oIncompleteRequirementCollection;
        }


        public List<GridColumnInfo> GetAllGridColumnsForRecPeriod(ARTEnums.Grid eGrid, int? RecPeriodID, AppUserInfo oAppUserInfo)
        {
            List<GridColumnInfo> oGridColumnInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GridColumnDAO oGridColumnDAO = new GridColumnDAO(oAppUserInfo);
                oGridColumnInfoCollection = oGridColumnDAO.GetAllGridColumnsForRecPeriod(eGrid, RecPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oGridColumnInfoCollection;
        }

        public List<ReconciliationFrequencyHdrInfo> GetAllReconciliationFrequencyHdrInfoByCompanyID(int CompanyID, AppUserInfo oAppUserInfo)
        {
            List<ReconciliationFrequencyHdrInfo> oReconciliationFrequencyHdrInfocollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ReconciliationFrequencyHdrDAO oReconciliationFrequencyHdrDAO = new ReconciliationFrequencyHdrDAO(oAppUserInfo);
                oReconciliationFrequencyHdrInfocollection = oReconciliationFrequencyHdrDAO.GetAllRecFrequencyHdrByCompanyID(CompanyID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oReconciliationFrequencyHdrInfocollection;
        }


        public void InsertReconciliationPeriodreconcilationFrequency(ReconciliationFrequencyHdrInfo oReconciliationFrequencyHdrInfo, List<int> ReconcilationPeriod, AppUserInfo oAppUserInfo)
        {
            ServiceHelper.SetConnectionString(oAppUserInfo);
            ReconciliationFrequencyHdrDAO oReconciliationFrequencyHdrDAO = new ReconciliationFrequencyHdrDAO(oAppUserInfo);
            oReconciliationFrequencyHdrDAO.InsertReconciliationFrequencyHdrInfo(oReconciliationFrequencyHdrInfo, ReconcilationPeriod);
        }

        public List<ReconciliationFrequencyReconciliationperiodInfo> GetAllReconciliationFrequencyReconciliationperiodInfoByRecFrequencyID(int RecFrequencyID, AppUserInfo oAppUserInfo)
        {
            List<ReconciliationFrequencyReconciliationperiodInfo> oReconciliationFrequencyReconciliationperiodInfocollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ReconciliationFrequencyReconciliationperiodDAO oReconciliationFrequencyReconciliationperiodDAO = new ReconciliationFrequencyReconciliationperiodDAO(oAppUserInfo);
                oReconciliationFrequencyReconciliationperiodInfocollection = oReconciliationFrequencyReconciliationperiodDAO.GetAllRecPeriodByRecFrequencyID(RecFrequencyID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oReconciliationFrequencyReconciliationperiodInfocollection;
        }


        public AccountCertificationStatusInfo GetAccountAndCertificationStatus(int? CurrentReconciliationPeriodID, int? CurrentUserID, int? CurrentCompanyID, bool IsCertificationActivated, short? CurrentRoleID, AppUserInfo oAppUserInfo)
        {
            AccountCertificationStatusInfo oAccountCertificationStatusInfo = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ReconciliationPeriodDAO oReconciliationPeriodDAO = new ReconciliationPeriodDAO(oAppUserInfo);
                oAccountCertificationStatusInfo = oReconciliationPeriodDAO.GetAccountAndCertificationStatus(CurrentReconciliationPeriodID, CurrentUserID, CurrentCompanyID, IsCertificationActivated, CurrentRoleID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }



            return oAccountCertificationStatusInfo;

        }
        public int CloseRecPeriodByRecPeriodIdAndComanyID(int? CurrentReconciliationPeriodID, Int16? ReconciliationPeriodStatusID, DateTime? RevisedDate, String UserLoginID, short actionTypeID, short changeSourceIDSRA, AppUserInfo oAppUserInfo)
        {
            int rowsAffected = 0;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ReconciliationPeriodDAO oReconciliationPeriodDAO = new ReconciliationPeriodDAO(oAppUserInfo);
                rowsAffected = oReconciliationPeriodDAO.CloseRecPeriodByRecPeriodIdAndComanyID(CurrentReconciliationPeriodID, ReconciliationPeriodStatusID, RevisedDate, UserLoginID, actionTypeID, changeSourceIDSRA);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return rowsAffected;

        }

        public int MarkRecPeriodReconciledAndStartCertification(int? CurrentReconciliationPeriodID, DateTime? RevisedDate, String UserLoginID, short actionTypeID, short changeSourceIDSRA, AppUserInfo oAppUserInfo)
        {
            int rowsAffected = 0;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ReconciliationPeriodDAO oReconciliationPeriodDAO = new ReconciliationPeriodDAO(oAppUserInfo);
                rowsAffected = oReconciliationPeriodDAO.MarkRecPeriodReconciledAndStartCertification(CurrentReconciliationPeriodID, RevisedDate, UserLoginID, actionTypeID, changeSourceIDSRA);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return rowsAffected;

        }

        public bool GetIsStopRecAndStartCertFlag(int? CurrentReconciliationPeriodID, AppUserInfo oAppUserInfo)
        {
            bool rowsAffected = false;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ReconciliationPeriodDAO oReconciliationPeriodDAO = new ReconciliationPeriodDAO(oAppUserInfo);
                rowsAffected = oReconciliationPeriodDAO.GetIsStopRecAndStartCertFlag(CurrentReconciliationPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return rowsAffected;
        }

        public ReconciliationPeriodInfo GetReconciliationPeriodInfoByRecPeriodID(int? recPeriodID, DateTime? PeriodEndDate, int? CompanyID, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ReconciliationPeriodDAO oReconciliationPeriodDAO = new ReconciliationPeriodDAO(oAppUserInfo);
                return oReconciliationPeriodDAO.GetReconciliationPeriodInfoByRecPeriodID(recPeriodID, PeriodEndDate, CompanyID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool GetIsMinimumRecPeriodExist(int? CurrentReconciliationPeriodID, AppUserInfo oAppUserInfo)
        {
            bool IsMinimumRecPeriodExist = false;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ReconciliationPeriodDAO oReconciliationPeriodDAO = new ReconciliationPeriodDAO(oAppUserInfo);
                IsMinimumRecPeriodExist = oReconciliationPeriodDAO.GetIsMinimumRecPeriodExist(CurrentReconciliationPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return IsMinimumRecPeriodExist;
        }

        public bool IsPreviousPeriodsCertified(int? recPeriodID, AppUserInfo oAppUserInfo)
        {
            bool isPreviousPeriodsCertified = false;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ReconciliationPeriodDAO oReconciliationPeriodDAO = new ReconciliationPeriodDAO(oAppUserInfo);
                isPreviousPeriodsCertified = oReconciliationPeriodDAO.IsPreviousPeriodsCertified(recPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return isPreviousPeriodsCertified;
        }

        public void ReprocessAccountReconcilability(int? companyID, int? recPeriodID, List<long> accountIDList, short actionTypeID, short changeSourceIDSRA, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ReconciliationPeriodDAO oReconciliationPeriodDAO = new ReconciliationPeriodDAO(oAppUserInfo);
                oReconciliationPeriodDAO.ReprocessAccountReconcilability(companyID, recPeriodID, accountIDList, actionTypeID, changeSourceIDSRA);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
        }
        public ReconciliationPeriodInfo GetReconciliationPeriodInfoForReopen(int? companyID, AppUserInfo oAppUserInfo)
        {
            ReconciliationPeriodInfo oReconciliationPeriodInfo = null;
            ServiceHelper.SetConnectionString(oAppUserInfo);
            ReconciliationPeriodDAO oRecPeriodDAO = new ReconciliationPeriodDAO(oAppUserInfo);
            oReconciliationPeriodInfo = oRecPeriodDAO.GetReconciliationPeriodInfoForReopen(companyID);
            return oReconciliationPeriodInfo;
        }
        public int ReOpenRecPeriod(ReconciliationPeriodInfo oReconciliationPeriodInfo, short actionTypeID, short changeSourceIDSRA, AppUserInfo oAppUserInfo)
        {
            int rowsAffected = 0;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ReconciliationPeriodDAO oReconciliationPeriodDAO = new ReconciliationPeriodDAO(oAppUserInfo);
                rowsAffected = oReconciliationPeriodDAO.ReOpenRecPeriod(oReconciliationPeriodInfo, actionTypeID, changeSourceIDSRA);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return rowsAffected;

        }

        public List<RecPeriodStatusDetailInfo> GetRecPeriodStatusDetail(int? recPeriodID, AppUserInfo oAppUserInfo)
        {
            List<RecPeriodStatusDetailInfo> oRecPeriodStatusDetailInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ReconciliationPeriodDAO oReconciliationPeriodDAO = new ReconciliationPeriodDAO(oAppUserInfo);
                oRecPeriodStatusDetailInfoList = oReconciliationPeriodDAO.GetRecPeriodStatusDetail(recPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oRecPeriodStatusDetailInfoList;
        }


    }//end of class
}//end of namespace
