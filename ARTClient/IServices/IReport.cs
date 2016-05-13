using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;
using System.Data;
using SkyStem.ART.Client.Model.Report;
using SkyStem.ART.Client.Params;

namespace SkyStem.ART.Client.IServices
{
    // NOTE: If you change the interface name "IReport" here, you must also update the reference to "IReport" in Web.config.
    [ServiceContract]
    public interface IReport
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        List<RoleMandatoryReportInfo> SelectAllRoleMandatoryReportByReconciliationPeriodID(int? reconciliationPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<RoleReportInfo_ExtendedWithReportName> SelectAllRoleReportByRoleID(ReportParamInfo oReportParamInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<ReportMstInfo> SelectAllReportByRoleID(short? roleID, int? RecPeriodID, int? companyID, AppUserInfo oAppUserInfo);
        [OperationContract]
        List<ReportMstInfo> SelectAllMyReportByRoleID(short? roleID, int? userID, int? RecPeriodID, AppUserInfo oAppUserInfo);


        [OperationContract]
        List<ReportMstInfo> SelectMandatoryReportByRoleID(short? roleID, int? userID, int? recPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        ReportMstInfo GetReportByID(short? reportID, int languageID, int businessEntityID, int defaultLanguageID, int? companyID, AppUserInfo oAppUserInfo);

        //[OperationContract]
        //List<ReportSavedInfo> GetSavedReportData(short? reportID);

        [OperationContract]
        List<ReportActivityInfo> GetReportActivityData(short? reportID, AppUserInfo oAppUserInfo);


        [OperationContract]
        List<UnusualBalancesReportInfo> GetReportUnusualBalancesReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<UnassignedAccountsReportInfo> GetReportUnassignedAccountsReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<IncompleteAccountAttributeReportInfo> GetReportIncompleteAccountAttributeReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<AccountStatusReportInfo> GetReportAccountStatusReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<AccountOwnershipReportInfo> GetReportAccountOwnershipReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblUserSearch, DataTable tblRoleSearch, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<OpenItemsReportInfo> GetReportOpenItemsReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch, DataTable tblUserSearch, DataTable tblRoleSearch, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<OpenItemsReportInfo> GetReportOpenItemsReportForCurrentRecPeriod(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch, DataTable tblUserSearch, DataTable tblRoleSearch, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<ReconciliationStatusCountReportInfo> GetReportReconciliationStatusCountReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblUserSearch, DataTable tblRoleSearch, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<CertificationTrackingReportInfo> GetReportCertificationTrackingReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblUserSearch, DataTable tblRoleSearch, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<DelinquentAccountByUserReportInfo> GetReportDelinquentAccountByUserReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblUserSearch, DataTable tblRoleSearch, AppUserInfo oAppUserInfo);

        [OperationContract]
        bool InsertMyReport(short? roleID, int? userID, ReportMstInfo oReportInfo, string myReportName, List<UserMyReportSavedReportParameterInfo> oUserMyReportSavedReportParameterCollection, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<UserMyReportSavedReportInfo> GetSavedReportData(short? roleID, int? userID, short? reportID, int languageID, int defaultLanguageID, int companyID, AppUserInfo oAppUserInfo);

        [OperationContract]
        bool DeleteSavedReportData(List<long> UserMyReportIDCollection, AppUserInfo oAppUserInfo);

        [OperationContract]
        IList<UserMyReportSavedReportParameterInfo> GetAllParametersByMySavedReportID(int UserMyReportSavedReportID, AppUserInfo oAppUserInfo);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="oReportSearchCriteria"></param>
        /// <param name="dtEntity"></param>
        /// <param name="dtUser"></param>
        /// <param name="dtRole"></param>
        /// <returns></returns>
        [OperationContract]
        List<ExceptionStatusReportInfo> GetExceptionStatusReport(ReportSearchCriteria oReportSearchCriteria, DataTable dtEntity, DataTable dtUser, DataTable dtRole, AppUserInfo oAppUserInfo);
        [OperationContract]
        int NoOfSavedMyReportByReportID(short? reportID, AppUserInfo oAppUserInfo);
        [OperationContract]
        bool DeleteMyReportByReportID(short? roleID, int? userID, short? reportID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<ReportMstInfo> GetAllReportsByPackageId(short? iPackageId, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<QualityScoreReportInfo> GetReportQualityScoreReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<ReviewNotesReportInfo> GetReportReviewNotesReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch, AppUserInfo oAppUserInfo);
        [OperationContract]
        List<CompletionDateReportInfo> GetReportCompletionDateReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch,string System, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<NewAccountReportInfo> GetReportNewAccountReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<AccountAttributeChangeReportInfo> GetReportAccountAttributeChangeReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<ReportColumnInfo> SelectAllReportColumnsByReportID(ReportParamInfo oReportParamInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<TaskCompletionReportInfo> GetReportTaskCompletionReport(short taskType, ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch, DataTable dtUser, DataTable dtRole, List<FilterCriteria> oTaskFilterCriteriaCollection, string System, AppUserInfo oAppUserInfo);
    
    }//end of interface
}//end of namespace
