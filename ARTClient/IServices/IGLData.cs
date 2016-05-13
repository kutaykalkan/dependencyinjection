using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Params;

namespace SkyStem.ART.Client.IServices
{
    // NOTE: If you change the interface name "IGLData" here, you must also update the reference to "IGLData" in Web.config.
    [ServiceContract]
    public interface IGLData
    {
        [OperationContract]
        List<GLDataHdrInfo> SelectGLDataHdrByGLDataID(long? gLDataID, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Selects all GL data information along with account information based on user and its role.
        /// If user is Preparer, reviewer Or Approver then only those accounts which are assigned to the user will be displayed
        /// Otherwise all accounts which falls at the organizational level assigned to the user will be shown
        /// </summary>
        /// <param name="recPeriodID">Unique identifier of the rec period for which data is being fetched</param>
        /// <param name="companyID">Unique identifier for the company</param>
        /// <param name="userID">Unique identifier of the user who is making the request</param>
        /// <param name="userRoleID">Unique identifier of the role which is assigned to the user</param>
        /// <param name="isDualReviewEnabled">Specifies if dual review is enabled for the company</param>
        /// <param name="isMaterialityEnabled">Specifies if Mateiality is enabled for the company</param>
        /// <param name="preparerAttributeID">Unique identifier of attribute for Preparer</param>
        /// <param name="reviewerAttributeID">Unique identifier of attribute for Reviewer</param>
        /// <param name="approverAttributeID">Unique identifier of attribute for Approver</param>
        /// <param name="preparerRoleID">Unique identifier of role for Preparer</param>
        /// <param name="reviewerRoleID">Unique identifier of role for Reviewer</param>
        /// <param name="approverRoleID">Unique identifier of role for Approver</param>
        /// <param name="systemAdminRoleID">Unique identifier of role for System admin</param>
        /// <param name="CEO_CFORoleID">Unique identifier of role for CEO</param>
        /// <param name="CFORoleID">Unique identifier of role for CFO</param>
        /// <param name="IsIncludeSRA">Specifies if System reconciled accounts are to be included or not</param>
        /// <param name="count">number of records to be returned null means unlimited</param>
        /// <returns>List of GL data for the given user</returns>
        [OperationContract]
        List<GLDataHdrInfo> SelectGLDataAndAccountInfoByUserID(
                                                                    List<FilterCriteria> oFilterCriteriaCollection
                                                                    , int recPeriodID
                                                                    , int companyID
                                                                    , int userID
                                                                    , short userRoleID
                                                                    , bool isDualReviewEnabled
                                                                    , bool isMaterialityEnabled
                                                                    , short preparerAttributeID
                                                                    , short reviewerAttributeID
                                                                    , short approverAttributeID
                                                                    , short preparerRoleID
                                                                    , short reviewerRoleID
                                                                    , short approverRoleID
                                                                    , short systemAdminRoleID
                                                                    , short CEO_CFORoleID
                                                                    , short skyStemAdminRoleID
                                                                    , bool IsIncludeSRA
                                                                    , int? count
                                                                    , List<Int16> AccountAttributeIDCollection
                                                                    , int languageID
                                                                    , int businessEntityID
                                                                    , int defaultLanguageID
                                                                    , string sortExpression
                                                                    , string sortDirection
                                                                    , AppUserInfo oAppUserInfo
                                                                );

        [OperationContract]
        List<GLDataHdrInfo> SelectGLDataAndAccountInfoByUserIDForCertificationBalances(
                                                            int recPeriodID
                                                            , int companyID
                                                            , int userID
                                                            , short userRoleID
                                                            , bool isDualReviewEnabled
                                                            , bool isMaterialityEnabled
                                                            , short preparerAttributeID
                                                            , short reviewerAttributeID
                                                            , short approverAttributeID
                                                            , short preparerRoleID
                                                            , short reviewerRoleID
                                                            , short approverRoleID
                                                            , short systemAdminRoleID
                                                            , short CEO_CFORoleID
                                                            , short skyStemAdminRoleID
                                                            , bool IsIncludeSRA
                                                            , int? count
                                                            , List<Int16> AccountAttributeIDCollection
                                                            , int languageID
                                                            , int businessEntityID
                                                            , int defaultLanguageID
                                                            , AppUserInfo oAppUserInfo
            //, string sortExpression
            //, string sortDirection
                                                        );

        /// <summary>
        /// Selects all GL data information along with account information based on user and its role.
        /// If user is Preparer, reviewer Or Approver then only those accounts which are assigned to the user will be displayed
        /// Otherwise all accounts which falls at the organizational level assigned to the user will be shown
        /// </summary>
        /// <param name="recPeriodID">Unique identifier of the rec period for which data is being fetched</param>
        /// <param name="companyID">Unique identifier for the company</param>
        /// <param name="userID">Unique identifier of the user who is making the request</param>
        /// <param name="userRoleID">Unique identifier of the role which is assigned to the user</param>
        /// <param name="isDualReviewEnabled">Specifies if dual review is enabled for the company</param>
        /// <param name="preparerAttributeID">Unique identifier of attribute for Preparer</param>
        /// <param name="reviewerAttributeID">Unique identifier of attribute for Reviewer</param>
        /// <param name="approverAttributeID">Unique identifier of attribute for Approver</param>
        /// <param name="preparerRoleID">Unique identifier of role for Preparer</param>
        /// <param name="reviewerRoleID">Unique identifier of role for Reviewer</param>
        /// <param name="approverRoleID">Unique identifier of role for Approver</param>
        /// <param name="systemAdminRoleID">Unique identifier of role for System admin</param>
        /// <param name="CEO_CFORoleID">Unique identifier of role for CEO</param>
        /// <param name="sysReconciledStatusID">Unique identifier for sys reconciled status</param>
        /// <returns>Percentage of completed/ reconciled accounts</returns>
        [OperationContract]
        AccountCountInfo GetTotalAndCompletedAccountCount(
                                                        int recPeriodID
                                                        , int companyID
                                                        , int userID
                                                        , short userRoleID
                                                        , bool isDualReviewEnabled
                                                        , short preparerAttributeID
                                                        , short reviewerAttributeID
                                                        , short approverAttributeID
                                                        , short preparerRoleID
                                                        , short reviewerRoleID
                                                        , short approverRoleID
                                                        , short systemAdminRoleID
                                                        , short CEO_CFORoleID
                                                        , short skyStemAdminRoleID
                                                        , short sysReconciledStatusID
                                                        , bool isIncludeSRA
                                                        , AppUserInfo oAppUserInfo
                                                    );

        /// <summary>
        /// Saves reconciliation status in GL data
        /// </summary>
        /// <param name="oAccountIDCollection">List of accounts for which Rec status is changed</param>
        /// <param name="oNetAccountIDCollection">List of net accounts for which Rec status is changed</param>
        /// <param name="currentRecPeriodId">Unique identifier for rec periods</param>
        /// <param name="reconciliationStatusID">Unique identifier for Rec Status which is to be updated</param>
        /// <returns>Success/Failure of the update operation</returns>


        [OperationContract]
        bool SaveGLDataReconciliationStatus(List<long> oGLDataIDCollection, int currentRecPeriodId, short reconciliationStatusID, string userLoginID, DateTime dateRevised, short actionTypeID, short changeSourceIDSRA, AppUserInfo oAppUserInfo);


        [OperationContract]
        int? GetCountAttachedDocumentByGLDataID(long? gLDataID, AppUserInfo oAppUserInfo);

        [OperationContract]
        int? GetCountGLReviewNoteByGLDataID(long? gLDataID, AppUserInfo oAppUserInfo);


        /// <summary>
        /// Gets details of GLData and Account for the given GL Data Id
        /// </summary>
        /// <param name="glDataID">Unique identifier of GL data</param>
        /// <param name="recPeriodID">Unique identifier for rec period</param>
        /// <param name="companyID">Unique identifier for company</param>
        /// <param name="isDualReviewEnabled">Specifies if dual review is enabled or not</param>
        /// <param name="isCertificationEnabled">Specifies if certification is activated for the company or not</param>
        /// <param name="certificationTypeID">Unique identifier for certification type</param>
        /// <returns>list of gl data and account information</returns>
        [OperationContract]
        GLDataAndAccountHdrInfo GetGLDataAndAccountInfoByGLDataID(long glDataID, int recPeriodID, int companyID, int userID, int roleID, bool isDualReviewEnabled, bool isCertificationEnabled, bool isMaterialityEnabled, short certificationTypeID,
            short preparerAttributeId, short reviewerAttributeId, short approverAttributeId,
             short backupPreparerAttributeId, short backupReviewerAttributeId, short backupApproverAttributeId, AppUserInfo oAppUserInfo);

        [OperationContract]
        int SaveReconciliationForm(
            long? gLDataID
            , GLDataHdrInfo oGLDataHdrInfo
            , bool isFormDataToBeSaved
            , bool isSignOff
            , bool saveSupportingDetailSection
            , bool isBankFormTemplate
            , DerivedCalculationSupportingDetailInfo oDerivedCalculationSupportingDetailInfo
            , short? reconciliationCategoryTypeIDForSupportingDetail
            , AppUserInfo oAppUserInfo);

        [OperationContract]
        List<GLDataAndAccountHdrInfo> GLDataIDAndRecTemplateIDByAccountIDAndRecPeriodID(long? accountID, int? netAccountID, int? recPeriodID, int? companyID, short? recTemplateAttributeID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<DerivedCalculationSupportingDetailInfo> SelectAllDerivedCalculationSupportingDetailInfoByGLDataID(long? gLDataID, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Gets review note updation status for reviewer/approver after the rec has been submitted by Preparer/Reviewer
        /// </summary>
        /// <param name="glDataID">Unique identifier for gl data</param>
        /// <param name="recPeriodID">Unique identifier for current rec period</param>
        /// <param name="userLoginID">Login id of current user</param>
        /// <returns>True/false</returns>
        [OperationContract]
        bool GetReviewNoteStatusForReviewerAndApprover(long glDataID, int recPeriodID, int userID, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Get local Currency Code for given rec period for which
        /// Exchange rate for both base and reporting currency is available
        /// </summary>
        /// <param name="recPeriodID">Unique identifier for current rec period</param>
        /// <param name="isMultiCurrencyEnabled">Specifies if Multi currency is enabled for the company</param>
        /// <returns>List of local currencies</returns>
        [OperationContract]
        List<string> SelectLocalCurrency(int recPeriodID, bool isMultiCurrencyEnabled, AppUserInfo oAppUserInfo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="GLDataID"></param>
        /// <param name="RecPeriodID"></param>
        /// <returns></returns>
        [OperationContract]
        List<GLDataReviewNoteHdrInfo> GetReviewNotes(Int64? GLDataID, Int32? RecPeriodID, AppUserInfo oAppUserInfo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reviewNoteID"></param>
        /// <returns></returns>
        [OperationContract]
        GLDataReviewNoteHdrInfo GetReviewNoteInfo(long? GLDataReviewNoteID, AppUserInfo oAppUserInfo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oGLDataReviewNoteHdrInfo"></param>
        [OperationContract]
        void AddReviewNote(GLDataReviewNoteHdrInfo oGLDataReviewNoteHdrInfo, int recPeriodID, List<AttachmentInfo> oAttachmentInfoCollection, AppUserInfo oAppUserInfo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oGLDataReviewNoteHdrInfo"></param>
        [OperationContract]
        void UpdateReviewNote(GLDataReviewNoteHdrInfo oGLDataReviewNoteHdrInfo, AppUserInfo oAppUserInfo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="GLDataReviewNoteID"></param>
        [OperationContract]
        void DeleteReviewNote(GLDataReviewNoteHdrInfo oGLDataReviewNoteHdrInfo, AppUserInfo oAppUserInfo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="NetAccountID"></param>
        /// <param name="RecPeriodID"></param>
        /// <param name="CompanyID"></param>
        /// <param name="oAttributeIDCollection"></param>
        /// <param name="LCID"></param>
        /// <param name="BusinessEntityID"></param>
        /// <param name="DefaultLCID"></param>
        /// <returns></returns>
        [OperationContract]
        List<GLDataHdrInfo> GetAccountInfoForNetAccount(int? NetAccountID, int? RecPeriodID, int? CompanyID, List<short> oAttributeIDCollection, int LCID, int BusinessEntityID, int DefaultLCID, AppUserInfo oAppUserInfo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nullable"></param>
        /// <returns></returns>
        GLDataHdrInfo GetLiteGLDataInfoByGLDataID(long? nullable, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Deletes User Gldata Flag by marking isactive to false
        /// </summary>
        /// <param name="oUserGLDataFlagInfo">User and Gl data info which is to be unflagged</param>
        [OperationContract]
        void DeleteUserGLDataFlag(UserGLDataFlagInfo oUserGLDataFlagInfo, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Inserts UserGLDataFlag info if it not exists already otherwise updates isactive flag to true.
        /// </summary>
        /// <param name="oUserGLDataFlagInfo">User and Gl data info which is to be flagged</param>
        [OperationContract]
        void InsertUserGLDataFlag(UserGLDataFlagInfo oUserGLDataFlagInfo, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Get the Data For Audit Trail for the GL Record
        /// </summary>
        /// <param name="GLDataID"></param>
        /// <returns></returns>
        [OperationContract]
        List<GLDataStatusInfo> GetAuditTrailData(Int64? GLDataID, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Returns true if all accounts assigned to a user is in reconciled state
        /// </summary>
        /// <param name="userID">Unique identifier of Logged in user</param>
        /// <param name="roleID">Unique identifier for logged in user role</param>
        /// <param name="recPeriodID">Unique identifier for current reconciliation period</param>
        /// <returns>true/false</returns>
        [OperationContract]
        bool? GetIsAllAccountsReconciledForUserAndRole(int userID, short roleID, int recPeriodID, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Returns true if all juniors have completed the given certification type for Reviewer/Approver
        /// For Preparer if he/she have completed previous certification then only it returns true
        /// </summary>
        /// <param name="userID">Unique identifier of Logged in user</param>
        /// <param name="roleID">Unique identifier for logged in user role</param>
        /// <param name="recPeriodID">Unique identifier for current reconciliation period</param>
        /// <param name="certificationTypeID">Unique identifier for certification type</param>
        /// <returns>true/false</returns>
        [OperationContract]
        bool? GetIsAllJuniorsCompletedCertificationForUserRoleAndCertificationType(int userID, short roleID, int recPeriodID, short certificationTypeID, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Selects all GL data information Where account is marked as SRA along with account information based on user and its role.
        /// If user is Preparer, reviewer Or Approver then only those accounts which are assigned to the user will be displayed
        /// Otherwise all accounts which falls at the organizational level assigned to the user will be shown
        /// </summary>
        /// <param name="recPeriodID">Unique identifier of the rec period for which data is being fetched</param>
        /// <param name="companyID">Unique identifier for the company</param>
        /// <param name="userID">Unique identifier of the user who is making the request</param>
        /// <param name="userRoleID">Unique identifier of the role which is assigned to the user</param>
        /// <param name="isDualReviewEnabled">Specifies if dual review is enabled for the company</param>
        /// <param name="isMaterialityEnabled">Specifies if Mateiality is enabled for the company</param>
        /// <param name="preparerAttributeID">Unique identifier of attribute for Preparer</param>
        /// <param name="reviewerAttributeID">Unique identifier of attribute for Reviewer</param>
        /// <param name="approverAttributeID">Unique identifier of attribute for Approver</param>
        /// <param name="preparerRoleID">Unique identifier of role for Preparer</param>
        /// <param name="reviewerRoleID">Unique identifier of role for Reviewer</param>
        /// <param name="approverRoleID">Unique identifier of role for Approver</param>
        /// <param name="systemAdminRoleID">Unique identifier of role for System admin</param>
        /// <param name="CEO_CFORoleID">Unique identifier of role for CEO</param>
        /// <param name="CFORoleID">Unique identifier of role for CFO</param>
        /// <param name="skyStemAdminRoleID">Unique identifier for Sky Stem Admin role</param>
        /// <param name="count">number of records to be returned null means unlimited</param>
        /// <param name="AccountAttributeIDCollection">Collection of account attribute ID which should be returned</param>
        /// <param name="businessEntityID">Current company ID</param>
        /// <param name="defaultLanguageID">English(1033)</param>
        /// <param name="languageID">Current user Language ID</param>
        /// <param name="sortDirection">ASC/DESC</param>
        /// <param name="sortExpression">Column name on which sorting is to be done</param>
        /// <returns>List of GL data for the given user</returns>
        [OperationContract]
        List<GLDataHdrInfo> SelectGLDataAndAccountInfoByUserIDForSRA(
                                                                    List<FilterCriteria> oFilterCriteriaCollection
                                                                    , int recPeriodID
                                                                    , int companyID
                                                                    , int userID
                                                                    , short userRoleID
                                                                    , bool isDualReviewEnabled
                                                                    , bool isMaterialityEnabled
                                                                    , short preparerAttributeID
                                                                    , short reviewerAttributeID
                                                                    , short approverAttributeID
                                                                    , short preparerRoleID
                                                                    , short reviewerRoleID
                                                                    , short approverRoleID
                                                                    , short systemAdminRoleID
                                                                    , short CEO_CFORoleID
                                                                    , short skyStemAdminRoleID
                                                                    , int? count
                                                                    , List<Int16> AccountAttributeIDCollection
                                                                    , int languageID
                                                                    , int businessEntityID
                                                                    , int defaultLanguageID
                                                                    , string sortExpression
                                                                    , string sortDirection
                                                                    , AppUserInfo oAppUserInfo
                                                                );
        /// <summary>
        /// Updates IsSRA flag of GLData
        /// </summary>
        /// <param name="oAccountIDCollection">List of accounts for which Rec status is changed</param>
        /// <param name="currentRecPeriodId">Unique identifier for rec periods</param>
        /// <param name="isSRA">true/false</param>
        /// <param name="dateRevised">current date</param>
        /// <param name="userLoginID">login id of current user</param>
        [OperationContract]
        void UpdateGLDataIsSRA(List<long> oAccountIDCollection, int currentRecPeriodId, bool isSRA, string userLoginID, DateTime dateRevised, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Deletes all review notes after certification which are marked to be deleted after certification
        /// </summary>
        /// <param name="recPeriodID">Unique identifier of current rec period</param>
        /// <param name="revisedBy">The login id of user who is sending the request</param>
        /// <param name="dateRevised">current date</param>
        [OperationContract]
        void DeleteReviewNotesAfterCertification(int recPeriodID, string revisedBy, DateTime dateRevised, AppUserInfo oAppUserInfo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oAccountIDCollection"></param>
        /// <param name="RecPeriodID"></param>
        /// <param name="RevisedBy"></param>
        /// <param name="DateRevised"></param>
        [OperationContract]
        void UpdateGLDataForRemoveAccountSignOff(List<long> oAccountIDCollection, List<int> oNetAccountIDCollection, int? RecPeriodID, string RevisedBy, DateTime DateRevised, AppUserInfo oAppUserInfo);

        List<string> SelectLocalCurrencyByAccountID(long gldataID, bool isMultiCurrencyEnabled, AppUserInfo oAppUserInfo);

        string GetBCCYByGLDataID(long gldataID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<GLDataHdrInfo> GetGLVersionByGLDataID(GLDataParamInfo oGLDataParamInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        void UpdateReOpenAccount(List<long> oGLDataIDCollection, string RevisedBy, DateTime dateRevised, short actionTypeID, short changeSourceIDSRA, AppUserInfo oAppUserInfo);

        [OperationContract]
        bool IsGLDataIDEditable(Int64 glDataID, AppUserInfo oAppUserInfo);

        [OperationContract]
        GLDataHdrInfo GetGLDataHdrInfo(Int64? glDataID, Int32? recPeriodID, Int32? CurrentUserID, Int16? CurrentRoleID, AppUserInfo oAppUserInfo);

        [OperationContract]
        void UpdateReSetAccount(List<long> oGLDataIDCollection, string RevisedBy, DateTime dateRevised, short actionTypeID, short changeSourceIDSRA, AppUserInfo oAppUserInfo);
    }//end of interface
}//end of class
