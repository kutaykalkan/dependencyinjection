using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;
using System.Data;

namespace SkyStem.ART.Client.IServices
{
    // NOTE: If you change the interface name "ICompany" here, you must also update the reference to "ICompany" in Web.config.
    [ServiceContract]
    public interface ICompany
    {
        [OperationContract]
        CompanyHdrInfo GetCompanyDetail(int? CompanyID, DateTime? periodEndDate, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<CompanyHdrInfo> GetAllCompanies(AppUserInfo oAppUserInfo);

        /// <summary>
        /// Function to Get Companies Collection - Lite Object
        /// Only ID, Name and Display Name
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<CompanyHdrInfo> GetAllCompaniesLiteObject();
        [OperationContract]
        List<CompanyHdrInfo> GetAllCompaniesList(string CompanyName, string DisplayName, bool? IsActive, bool? IsFTPActive, bool IsShowActivationHistory, short ActivationHistoryVal,AppUserInfo oAppUserInfo);
        [OperationContract]
        ContactInfo GetContactInfo(int? CompanyID, AppUserInfo oAppUserInfo);

        [OperationContract]
        bool CreateNewCompany(CompanyHdrInfo objCompanyHdrInfo, ContactInfo objCompanyContact, int languageID, AppUserInfo oAppUserInfo);

        [OperationContract]
        bool UpdateCompanyLogoPhysicalPath(CompanyHdrInfo objCompanyHdrInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        bool UpdateCompany(CompanyHdrInfo objCompanyHdrInfo, ContactInfo objCompanyContact, int languageID, bool isPackageUpdated, AppUserInfo oAppUserInfo);



        [OperationContract]
        bool UpdateFinancialYear(FinancialYearHdrInfo oFinancialYearHdrInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        bool InsertFinancialYear(FinancialYearHdrInfo oFinancialYearHdrInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<FinancialYearHdrInfo> GetAllFinancialYearList(int? CompanyID, AppUserInfo oAppUserInfo);

        [OperationContract]
        FinancialYearHdrInfo GetFinancialYearByID(int? FinancialYearByID, AppUserInfo oAppUserInfo);


        /// <summary>
        /// Get the Organization Hierarchy for the Company
        /// </summary>
        /// <param name="CompanyID">Company ID for which we need the Organization Hierarchy</param>
        /// <returns></returns>
        [OperationContract]
        List<GeographyStructureHdrInfo> GetOrganizationalHierarchy(int? CompanyID, AppUserInfo oAppUserInfo);

        [OperationContract]
        IList<CompanySettingInfo> SelectAllCompanySettingByCompanyID(int? companyID, AppUserInfo oAppUserInfo);

        [OperationContract]
        void UpdateCompanySetting(CompanySettingInfo oCompanySettingInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        IList<CompanySettingInfo> SelectCompanyMaterialityType(int? reconciliationPeriodID, AppUserInfo oAppUserInfo);


        [OperationContract]
        IList<CompanyUnexplainedVarianceThresholdInfo> GetUnexplainedVarianceThresholdByRecPeriodID(int? reconciliationPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        IList<ReconciliationPeriodInfo> SelectAllReconciliationPeriodByCompanyID(int? companyID, int? CurrentFinancialYearID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<CompanyCapabilityInfo> SelectAllCompanyCapabilityByReconciliationID(int? reconciliationID, AppUserInfo oAppUserInfo);

        //Capability Main Save Method
        [OperationContract]
        int SaveAllCompanyCapability
            (int? reconciliationPeriodID
            , bool isDualReviewActivated
            , bool isDualReviewTypeToBeSaved
            , bool? isMaterialityActivated
            , bool isRiskRatingActivated
            , bool isRoleMandatoryReportActivated
            , bool IsRCCLValidationTypeToBeSaved
            , bool IsMaterialityToBeSaved
            , List<CompanyCapabilityInfo> oCompanyCapabilityInfoCollection
            , CompanySettingInfo oCompanySettingInfo, IList<FSCaptionMaterialityInfo> oFSCaptionMaterialityInfoCollection
            , IList<RoleMandatoryReportInfo> oRoleMandatoryReportInfoCollection
            , IList<RiskRatingReconciliationFrequencyInfo> oRiskRatingReconciliationFrequencyInfoCollection, IList<RiskRatingReconciliationPeriodInfo> oRiskRatingReconciliationPeriodInfoCollection, bool IsRiskRatingValueChanged
            , IList<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollection
            , CompanyUnexplainedVarianceThresholdInfo oCompanyUnexplainedVarianceThresholdInfo
            , bool IsDayTypeToBeSaved
            , DateTime dateRevised
            , string revisedBy
            , AppUserInfo oAppUserInfo
            );

        /// <summary>
        /// Gets base currency code for the given company
        /// </summary>
        /// <param name="recPeriodID">Unique identifier for Rec period</param>
        /// <returns>Base currency code</returns>
        [OperationContract]
        string GetBaseCurrencyByRecPeriodID(int recPeriodID, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Gets base currency code for the given company
        /// </summary>
        /// <param name="companyID">Unique identifier for company</param>
        /// <returns>Reporting currency code</returns>
        [OperationContract]
        string GetReportingCurrencyByRecPeriodID(int recPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        int UpdateSystemWideSetting
                (int? companyID
                , int selectedRecPeriodID
                , bool isPeriodMarkedOpen
                , DateTime? dateRevised
                , string revisedBy
                , short actionTypeID
                , short changeSourceIDSRA
                , List<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollection
                , CompanySettingInfo oCompanySettingInfo
                , AppUserInfo oAppUserInfo
            );

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool HasUserLimitReached(int? CompanyID, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Gets data storage capacity and current usage for given company
        /// </summary>
        /// <param name="companyID">Unique identifier for company</param>
        /// <param name="dataStorageCapacity">Decimal value indicating the data storage cpacity for the company</param>
        /// <param name="currentUsage">Decimal value indicating how much data storage is being used by the given company</param>
        [OperationContract]
        void GetCompanyDataStorageCapacityAndCurrentUsage(int companyID, out decimal? dataStorageCapacity, out decimal? currentUsage, AppUserInfo oAppUserInfo);

        [OperationContract]
        string GetCompanyLogo(int? CompanyID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<FeatureMstInfo> GetFeaturesByCompanyID(int? CompanyID, int? recPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        void SaveWorkWeek(List<CompanyWeekDayInfo> oCompanyWorkWeekInfoCollection, int? CompanyID, string AddedBy, DateTime? DateAdded, string RevisedBy, DateTime? DateRevised, AppUserInfo oAppUserInfo);

        [OperationContract]
        IList<CompanyWeekDayInfo> GetComapnyWorkWeek(int? RecPeriodID, int? CompanyID, AppUserInfo oAppUserInfo);


        [OperationContract]
        IList<CompanyWeekDayInfo> SelectAllWorkWeekByFinancialYearIDAndCompanyID(int? companyID, int? FinancialYearID, AppUserInfo oAppUserInfo);
        [OperationContract]
        List<SubledgerSourceInfo> SelectAllSubledgerSourceByCompanyID(int? companyID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<CompanyHdrInfo> SelectAllCompaniesForDatabaseCreation(AppUserInfo oAppUserInfo);

        [OperationContract]
        bool CreateDatabaseAndCompany(CompanyHdrInfo oCompanyHdrInfo, ContactInfo oContactInfo, List<UserHdrInfo> oUserHdrInfoList, int languageID, string BaseDBPath, AppUserInfo oAppUserInfo);

        [OperationContract]
        bool? IsDatabaseExists(int? companyID);

        [OperationContract]
        ContactInfo GetContactInfoFromCore(int? CompanyID, AppUserInfo oAppUserInfo);

        [OperationContract]
        IList<ReconciliationPeriodInfo> SelectAllRecPeriodNumberByCompanyID(int? companyID, DateTime? periodEndDate, AppUserInfo oAppUserInfo);

        [OperationContract]
        IList<CompanySettingInfo> SelectCompanyDualLevelReviewType(int? reconciliationPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        IList<CompanySettingInfo> SelectCompanyDayType(int? reconciliationPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        IList<CompanySettingInfo> SelectCompanyRCCLValidationType(int? reconciliationPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        void UpdateCompanyDataStorageCapacityAndCurrentUsage(int CurrentCompanyID, decimal FileSizeInMB, string RevisedBy,DateTime DateRevised,AppUserInfo appUserInfo);

        [OperationContract]
        List<FTPServerInfo> GetAllFTPServerListObject(int? CompanyId, AppUserInfo appUserInfo);
    }//end of interface
}//end of namespace
