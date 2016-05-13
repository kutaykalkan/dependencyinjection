using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Params;
using SkyStem.ART.Client.Model.CompanyDatabase;
using SkyStem.ART.Client.Model.RecControlCheckList;

namespace SkyStem.ART.Client.IServices
{
    // NOTE: If you change the interface name "IUtility" here, you must also update the reference to "IUtility" in Web.config.
    [ServiceContract]
    public interface IUtility
    {
        [OperationContract]
        IList<DataImportTypeMstInfo> GetAllImportType( AppUserInfo oAppUserInfo);

        /// <summary>
        /// Function to Get the RoleID based on Role Desc
        /// </summary>
        /// <param name="RoleDesc"></param>
        /// <returns></returns>
        [OperationContract]
        int? GetRoleID(string RoleDesc, AppUserInfo oAppUserInfo);

        
        [OperationContract]
        List<RiskRatingMstInfo> SelectAllRiskRating( AppUserInfo oAppUserInfo);

        [OperationContract]
        List<ReconciliationFrequencyMstInfo> GetAllReconciliationFrequencyMstInfo( AppUserInfo oAppUserInfo);

        //[OperationContract]
        //int SaveCompanyCapabilityTableValue(List<int> IDs, CompanyCapabilityInfo oCompanyCapabilityInfo);

        [OperationContract]
        IList<MaterialityTypeMstInfo> SelectAllMaterialityTypeMst( AppUserInfo oAppUserInfo);
        //IList<RoleMandatoryReport_DerivedInfo> SelectAllRoleMandatoryReportByCompanyID(int companyID);

        [OperationContract]
        List<ReconciliationArchiveInfo> GetReconciliationArchiveData(int? accountID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<GeographyClassMstInfo> GetAllOrganizationalHierarchyKeys(short? companyGeographyClassID, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Selects accounts attribute master information for all attribute given in the argument
        /// </summary>
        /// <param name="oAccountAttributeCollection">List of attributes for which details are required</param>
        /// <returns>List of Account attribute master information</returns>
        
        //[OperationContract]
        //List<AccountAttributeMstInfo> SelectAccountAttributeMstForMassUpdate(List<ARTEnums.AccountAttribute> oAccountAttributeCollection);
        
        [OperationContract]
        List<AccountAttributeMstInfo> SelectAccountAttributeMstForMassUpdate(int? iCompanyId, int? RecPeriodID, AppUserInfo oAppUserInfo);
       
        [OperationContract]
        List<ReconciliationPeriodStatusMstInfo> GetAllRecPeriodStatuses( AppUserInfo oAppUserInfo);

        /// <summary>
        /// Gets currency exchange rate from one currency to another.
        /// </summary>
        /// <param name="recPeriodID">Unique identifier for current rec period</param>
        /// <param name="fromCurrencyCode">Unique 3 digit code for source currency</param>
        /// <param name="toCurrencyCode">Unique 3 digit code for destination currecy</param>
        /// <param name="isMultiCurrencyEnabled">Specifies if multi currency is enabled for the company or not</param>
        /// <returns>Exchange rate</returns>
        [OperationContract]
        decimal GetCurrencyExchangeRate(int recPeriodID, string fromCurrencyCode, string toCurrencyCode, bool isMultiCurrencyEnabled, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<ExchangeRateInfo> GetCurrencyExchangeRateByRecPeriod(int recPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<ExchangeRateInfo> GetCurrencyExchangeRateArchieveByExchangeRateID(int exchangeRateID, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Selects all System Reconciliation Rules
        /// </summary>
        /// <returns>List of SRA Rules</returns>
        [OperationContract]
        List<SystemReconciliationRuleMstInfo> SelectAllSRARules(AppUserInfo oAppUserInfo);

        /// <summary>
        /// Inserts company sra rules
        /// </summary>
        /// <param name="oCompanySRARuleInfo">List of sra rules choosen for the company</param>
        /// <param name="recPriodID">Unique identifier of current rec period</param>
        [OperationContract]
        void InsertCompanySRARule(CompanyConfigurationParamInfo oCompanyConfigurationParamInfo, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Selects all SRA rules for the given company and rec period
        /// </summary>
        /// <param name="companyID">Unique identifier for current company</param>
        /// <param name="recPriodID">Unique identifier of current rec period</param>
        /// <returns>list of SRA Rules</returns>
        [OperationContract]
        List<CompanySystemReconciliationRuleInfo> SelectCompanySRARuleInfoByRecPeriodID(int companyID, int recPeriodID, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Get ALl Exception Types
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<ExceptionTypeMstInfo> GetAllExceptionTypes( AppUserInfo oAppUserInfo);

        [OperationContract]
        List<AppSettingsInfo> GetAllAppSettings( AppUserInfo oAppUserInfo);

        [OperationContract]
        void UpdateGLDataReconciliationStatus(GLDataHdrInfo oGLDataHdrInfo, AppUserInfo oAppUserInfo);
        [OperationContract]
        int ProcessMaterialityAndSRAByCompanyID(int CompanyID, int RecPeriodID, string revisedBy, DateTime dateRevised, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<CapabilityMstInfo> GetAllCapabilities( AppUserInfo oAppUserInfo);

        [OperationContract]
        string GetKeyFieldsByCompanyID(int companyID, AppUserInfo oAppUserInfo);

        [OperationContract]
        string GetAccountUniqueSubsetKeys(int companyID, int recPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        void LogError(LogInfo oLogInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        void LogInfo(LogInfo oLogInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<CompanyUserInfo> GetAllCompanyConnectionInfo();

        [OperationContract]
        List<ServerCompanyInfo> GetServerCompanyListForServiceProcessing();
        [OperationContract]
        void ReCreateIndexes(int? companyID, AppUserInfo oAppUserInfo);
        [OperationContract]
        IList<DualLevelReviewTypeMstInfo> SelectAllDualLevelReviewTypeMst(AppUserInfo oAppUserInfo);

        [OperationContract]
        IList<DueDaysBasisInfo> SelectAllDueDaysBasisMst(AppUserInfo oAppUserInfo);

        [OperationContract]
        List<ReconciliationCheckListStatusInfo> GetReconciliationCheckListStatus(AppUserInfo appUserInfo);

        [OperationContract]
        List<string> GetImportTemplateMandatoryFields(int? companyID, int? ImportTemplateID, List<string> MandatoryFieldList, AppUserInfo oAppUserInfo);

        [OperationContract]
        IList<DaysTypeInfo> SelectAllDaysType(AppUserInfo oAppUserInfo);

    }//end of interface
}//end of namespace
