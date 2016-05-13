using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Params;

namespace SkyStem.ART.Client.IServices
{
    // NOTE: If you change the interface name "IAccount" here, you must also update the reference to "IAccount" in Web.config.
    [ServiceContract]
    public interface IAccount
    {
        [OperationContract]
        AccountHdrInfo GetAccountHdrInfoByAccountID(long accountID, int companyID, int recPeriodID, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Selects all account types for the system
        /// </summary>
        /// <returns>List of account types</returns>
        [OperationContract]
        List<AccountTypeMstInfo> SelectAllAccountTypeMstInfo(AppUserInfo oAppUserInfo);

        /// <summary>
        /// Searches database and selects all accounts which matches the search criteria
        /// </summary>
        /// <param name="oAccountSearchCriteria">object containing all search criteria</param>
        /// <returns>List of accounts which matches the search criteria</returns>
        [OperationContract]
        List<AccountHdrInfo> SearchAccount(AccountSearchCriteria oAccountSearchCriteria, AppUserInfo oAppUserInfo);



        [OperationContract]
        List<AccountHdrInfo> GetAccountByOrganisationalHierarchy(AccountSearchCriteria oAccountSearchCriteria, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Saves account ownership information in the database
        /// </summary>
        /// <param name="oAccountHdrInfoCollection">List of account header info</param>
        /// <returns>True/false</returns>

        [OperationContract]
        bool SaveAccountOwnerships(List<AccountHdrInfo> oAccountHdrInfoCollection, int comapnyId, int recPeriodID, bool isDualReviewEnabled,
            string userLoginID, DateTime updateTime, short preparerRoleID, short reviewerRoleID, short approverRoleID,
            short backupPreparerRoleID, short backupReviewerRoleID, short backupApproverRoleID, short actionTypeID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<AccountHdrInfo> SelectOrganisationHiearachyByUserIDRoleID(int userID, short roleID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<NetAccountHdrInfo> GetNetAccountHdrInfoCollection(NetAccountSearchParamInfo oNetAccountSearchParamInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<AccountHdrInfo> GetNetAccountHdrInfoCollectionByNetID(int recperiodID, int netAccountID, AppUserInfo oAppUserInfo);

        [OperationContract]
        int? UpdateNetAccount(NetAccountHdrInfo oNetAccountHdrInfo, List<AccountHdrInfo> oAccountHdrInfocollection, List<AccountHdrInfo> oDeletedAccountHdrInfoCollection, int companyID, int recPeriodID, DateTime recPeriodEndDate, string addedBy, DateTime dateRevised, short actionType, short changeSourceIDSRA, AppUserInfo oAppUserInfo);

        [OperationContract]
        void UpdateNetAccountAttributeValue(NetAccountHdrInfo oNetAccountHdrInfo, List<NetAccountAttributeValueInfo> lstNetAccountAttributeValueInfo, int recPeriodID, AppUserInfo oAppUserInfo);

        /// <summary>
        ///  Saves a single attribute value for multiple Accounts at a time
        /// </summary>
        /// <param name="oAccountIDCollection">List of accounts Ids which is to be updated</param>
        /// <param name="companyId">unique identifier of current company</param>
        /// <param name="currentRecPeriodId">unique identifier of current rec period</param>
        /// <param name="oAccountAttribute">Account attribute enum</param>
        /// <param name="accountAttributeValue">value of attribute converted into string</param>
        /// <returns>success/failure of update operation</returns>
        [OperationContract]
        bool SaveAccountMassUpdate(List<long> oAccountIDCollection, int companyId, int currentRecPeriodId, ARTEnums.AccountAttribute oAccountAttribute, string accountAttributeValue, string userLoginID, DateTime updateTime, short actionTypeID, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Saves Reconciliation frequency for multiple accounts
        /// </summary>
        /// <param name="oAccountIDCollection">List of accounts Ids which is to be updated</param>
        /// <param name="oRecPeriodIDCollection">List of reconciliation period ids</param>
        /// <param name="companyId">unique identifier of current company</param>
        /// <param name="currentRecPeriodId">unique identifier of current rec period</param>
        /// <returns>success/failure of update operation</returns>
        [OperationContract]
        bool SaveAccountRecFrequecy(List<long> oAccountIDCollection, List<int> oRecPeriodIDCollection, int companyId, int currentRecPeriodId, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Gets account materiality as true/false
        /// </summary>
        /// <param name="companyID">Unique identifier for company</param>
        /// <param name="accountID">Unique identifier for account</param>
        /// <param name="recPeriodID">Unique identifier for Reconciliation period</param>
        /// <param name="isMaterialityEnabled">Boolean value indicating if materiality is enabled for the given company</param>
        /// <returns>True/false</returns>
        [OperationContract]
        bool? GetAccountMateriality(int companyID, long accountID, int recPeriodID, bool isMaterialityEnabled, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Saves account profile
        /// </summary>
        /// <param name="oAccountHdrInfo">Account information which is to be updated</param>
        /// <param name="recPeriodIDCollection">ollection of rec periods (If risk rating is disabled)</param>
        /// <param name="companyId">Unique isentifier of the company</param>
        /// <param name="currentRecPeriodId">Unique isentifier of the rec period</param>
        /// <returns>result of update operation</returns>
        [OperationContract]
        bool SaveAccountProfile(List<AccountHdrInfo> oAccountHdrInfoCollection, int companyId, int currentRecPeriodId, string userLoginID, DateTime updateTime, short actionTypeID, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Selects all reconciliation period info associated with the account
        /// </summary>
        /// <param name="accountID">Unique identifier of the account for which rec periods will be fetched</param>
        /// <returns>List of rec periods associated with the account</returns>
        [OperationContract]
        List<AccountReconciliationPeriodInfo> SelectAccountRecPeriodByAccountID(long accountID, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Gets name of the given net account
        /// </summary>
        /// <param name="netAccountID">Unique identifier for net accounts</param>
        /// <param name="companyID">Unique identifier for Company</param>
        /// <param name="lcID">Unique identifier for user language</param>
        /// <param name="businessEntityID">Unique identifier for businessEntity</param>
        /// <param name="defaultLCID">Unique identifier for default user language</param>
        /// <returns></returns>
        [OperationContract]
        string GetNetAccountNameByNetAccountID(int netAccountID, int companyID, int lcID, int businessEntityID, int defaultLCID, AppUserInfo oAppUserInfo);

        [OperationContract]
        void DeleteAccountsFromNetAccount(NetAccountParamInfo oNetAccountParamInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        int? DeleteNetAccount(NetAccountParamInfo oNetAccountParamInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<NetAccountAttributeValueInfo> GetNetAccountAttributeValuesByNetAccountIDAndRecPeriodEndDate(int netAccountID, int companyID, int ReconciliationPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        bool IsNetAccountDuplicate(string netAccountName, int companyID, int ReconciliationPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<AccountHdrInfo> GetAccountHdrInfoListByAccountIDs(List<long> oAccountIDList, int? recperiodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<long> GetAccountIDListByUserIDs(List<int> oUserIDList, int? recperiodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<AccountHdrInfo> GetAccountInformationWithoutGL(int? UserID, short? RoleID, int RecPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        AccountAttributeWarningInfo GetAccountAttributeChangeWarning(List<AccountAttributeWarningInfo> oAccountIDNetAccountIDCollection, List<AccountHdrInfo> oAccountAttributeCollection, int RecPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<AccountHdrInfo> GetNewAccounts(int? DataImportID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<AccountHdrInfo> GetAccountInformationForCompanyAlertMail(int RecPeriodID, int UserID, short RoleID, long CompanyAlertDetailID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<AccountHdrInfo> GetAccountInformationForAlertMail(List<long> oAccountIDList, List<int> oNetAccountIDList, int recperiodID,short? AlertID , short? RoleID ,AppUserInfo oAppUserInfo);
        [OperationContract]
        List<AccountHdrInfo> GetAccountHdrInfo(List<long> oAccountIDList, AppUserInfo oAppUserInfo);
        [OperationContract]
        List<long> GetAccountsHaveDiffrentAttributeValueInFuture(List<long> AccountIDList, int? recPeriodID, AppUserInfo oAppUserInfo);

    }
}
