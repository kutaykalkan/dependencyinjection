using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;
using System.Data;
using SkyStem.ART.Client.Data;

namespace SkyStem.ART.Client.IServices
{
    // NOTE: If you change the interface name "IReconciliationPeriod" here, you must also update the reference to "IReconciliationPeriod" in Web.config.
    [ServiceContract]
    public interface IReconciliationPeriod
    {
        [OperationContract]
        int InsertReconciliationPeriod(List<ReconciliationPeriodInfo> oRecPeriodInfoCollection
            , IDbConnection oConnection, IDbTransaction oTransaction, int companyID, int dataimportID, DateTime? currentReconciliationPeriodEndDate, AppUserInfo oAppUserInfo);

        //[OperationContract]
        //DateTime? GetCurrentPeriodByCompanyId(int companyID);

        [OperationContract]
        ReconciliationPeriodStatusMstInfo GetRecPeriodStatus(int? RecPeriodID, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Gets the due date for given role and for the given rec period
        /// </summary>
        /// <param name="roleId">Unique identifier of the role</param>
        /// <param name="preparerRoleID">Unque identifier for Preparer role in system</param>
        /// <param name="reviewerRoleId">Unque identifier for Reviewer role in system</param>
        /// <param name="approverRoleId">Unque identifier for Approver role in system</param>
        /// <param name="recPeriodId">Unque identifier for reconciliation period</param>
        /// <returns>due date for the given role for given rec period</returns>
        [OperationContract]
        DateTime? GetDueDateByUserRoleID(int roleId, short preparerRoleID, short reviewerRoleId, short approverRoleId, int recPeriodId, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Returns GLDataImport Processing Status. It returns following values
        /// 1- Import Data Not Available ( no active rows or (no processing (or to be processed, ie. all kind of processing is complete) and no successful upload))|(GLImport Data when @DataImportTypeID=1) 
        /// 2- Import Data ToBeProcessed |(all rows to be processed)(GLImport Data when @DataImportTypeID=1) 
        /// 3- Import Data processing |(GLImport Data when @DataImportTypeID=1) 
        /// 4- Import Data Available (at least one successfull import and no processing left)|(GLImport Data when @DataImportTypeID=1) 

        /// </summary>
        /// <param name="newCurrentRecPeriodID"></param>
        /// <param name="dataImportTypeID"></param>
        /// <returns></returns>
        [OperationContract]
        int? GetDataImportProcessingStatus(int? newCurrentRecPeriodID, int? dataImportTypeID, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Returns Following-
        /// (1,'GLData Not Available')
        /// (2,'ExchangeRate Data Not Available')
        /// (3,'Capability Configuration Not Complete')
        /// </summary>
        /// <param name="RecPeriodID"></param>
        /// <returns></returns>
        [OperationContract]
        List<int> GetIncompleteRequirementToMarkOpen(int? RecPeriodID, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Get all the Columns associated with the Grid
        /// </summary>
        /// <param name="eGrid">Grid ID</param>
        /// <returns></returns>
        [OperationContract]
        List<GridColumnInfo> GetAllGridColumnsForRecPeriod(ARTEnums.Grid eGrid, int? RecPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<ReconciliationFrequencyHdrInfo> GetAllReconciliationFrequencyHdrInfoByCompanyID(int CompanyID, AppUserInfo oAppUserInfo);

        [OperationContract]
        void InsertReconciliationPeriodreconcilationFrequency(ReconciliationFrequencyHdrInfo oReconciliationFrequencyHdrInfo, List<int> ReconcilationPeriod, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<ReconciliationFrequencyReconciliationperiodInfo> GetAllReconciliationFrequencyReconciliationperiodInfoByRecFrequencyID(int RecFrequencyID, AppUserInfo oAppUserInfo);
        [OperationContract]
        AccountCertificationStatusInfo GetAccountAndCertificationStatus(int? CurrentReconciliationPeriodID, int? CurrentUserID, int? CurrentCompanyID, bool IsCertificationActivated, short? CurrentRoleID, AppUserInfo oAppUserInfo);
        [OperationContract]
        int CloseRecPeriodByRecPeriodIdAndComanyID(int? CurrentReconciliationPeriodID, Int16? ReconciliationPeriodStatusID, DateTime? RevisedDate, String UserLoginID, short actionTypeID, short changeSourceIDSRA, AppUserInfo oAppUserInfo);
        [OperationContract]
        int MarkRecPeriodReconciledAndStartCertification(int? CurrentReconciliationPeriodID, DateTime? RevisedDate, String UserLoginID, short actionTypeID, short changeSourceIDSRA, AppUserInfo oAppUserInfo);
        [OperationContract]
        bool GetIsStopRecAndStartCertFlag(int? CurrentReconciliationPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        ReconciliationPeriodInfo GetReconciliationPeriodInfoByRecPeriodID(int? recPeriodID, DateTime? PeriodEndDate, int? CompanyID, AppUserInfo oAppUserInfo);
        [OperationContract]
        ReconciliationPeriodInfo GetMaxCurrentPeriodByCompanyId(int? companyID, AppUserInfo oAppUserInfo);
        [OperationContract]
        ReconciliationPeriodInfo GetMinCurrentPeriodByCompanyId(int? companyID, AppUserInfo oAppUserInfo);
        [OperationContract]
        bool GetIsMinimumRecPeriodExist(int? CurrentReconciliationPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        bool IsPreviousPeriodsCertified(int? recPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        void ReprocessAccountReconcilability(int? companyID, int? recPeriodID, List<long> accountIDList, short actionTypeID, short changeSourceIDSRA, AppUserInfo oAppUserInfo);
        [OperationContract]
        ReconciliationPeriodInfo GetReconciliationPeriodInfoForReopen(int? companyID, AppUserInfo oAppUserInfo);
        [OperationContract]
        int ReOpenRecPeriod(ReconciliationPeriodInfo oReconciliationPeriodInfo, short actionTypeID, short changeSourceIDSRA, AppUserInfo oAppUserInfo);
        [OperationContract]
        List<RecPeriodStatusDetailInfo> GetRecPeriodStatusDetail(int? recPeriodID, AppUserInfo oAppUserInfo);
    }
}
