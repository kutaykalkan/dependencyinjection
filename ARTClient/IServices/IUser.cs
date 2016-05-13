using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Model.CompanyDatabase;
using SkyStem.ART.Client.Params;

namespace SkyStem.ART.Client.IServices
{
    // NOTE: If you change the interface name "IUser" here, you must also update the reference to "IUser" in Web.config.
    [ServiceContract]
    public interface IUser
    {
        [OperationContract]
        UserHdrInfo GetUserDetail(int userID, AppUserInfo oAppUserInfo);

        [OperationContract]
        UserHdrInfo AuthenticateUser(string loginID, string password);

        [OperationContract]
        UserHdrInfo GetUserByLoginID(string loginID, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Verify Old Password, and Update with the New one
        /// </summary>
        /// <param name="UserID">User ID of the User whose password has to be updated</param>
        /// <param name="LoginID">Login ID of the User whose password has to be updated</param>
        /// <param name="OldPasswordHash">Hash of Old Password</param>
        /// <param name="NewPasswordHash">Hash of New Password</param>
        /// <returns></returns>
        [OperationContract]
        void VerifyAndUpdatePassword(int UserID, string LoginID, string OldPasswordHash, string NewPasswordHash, AppUserInfo oAppUserInfo);

        [OperationContract]
        int UpdatePassword(string loginID, string password, AppUserInfo oAppUserInfo);

        [OperationContract]
        int InsertUser(UserHdrInfo newUser, List<int> newUserRolesList, bool IsEmailIDUniqueCheckRequired, AppUserInfo oAppUserInfo);

        [OperationContract]
        void UpdateUser(UserHdrInfo oUserHdrInfo, int count, List<int> listUserRoles, bool hasStatusChanged, bool IsEmailIDUniqueCheckRequired, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Search the Users based on the criteria
        /// </summary>
        /// <param name="FirstName"></param>
        /// <param name="EmailID"></param>
        /// <param name="LastName"></param>
        /// <param name="Count"></param>
        /// <param name="Role"></param>
        /// <param name="IsActive"></param>
        /// <param name="CompanyID"></param>
        /// <param name="RoleID"></param>
        /// <param name="loggedInUserID"></param>
        /// <param name="loggedInRoleID"></param>
        /// <param name="SortExpression"></param>
        /// <param name="SortDirection"></param>
        /// <returns></returns>
        [OperationContract]
        List<UserHdrInfo> SearchUser(string FirstName, string EmailID, string LastName, int Count,
            int? RoleID, bool? IsActive, int? CompanyID, int loggedInUserID, short loggedInRoleID, int? recPeriodID,
            DateTime? recPeriodEndDate, string SortExpression, string SortDirection, bool IsShowActivationHistory,
            short ActivationHistoryVal, short? ActivationStatusTypeID, short? FTPActivationStatusID, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Update the Last Logged In Information for the User
        /// </summary>
        /// <param name="oUserHdrInfo"></param>
        [OperationContract]
        void UpdateLastLoggedInInfo(UserHdrInfo oUserHdrInfo, AppUserInfo oAppUserInfo);



        [OperationContract]
        List<RoleMstInfo> GetUserRole(int? userID, AppUserInfo oAppUserInfo);

        /// <summary>
        /// This method is used to auto populate User Name text box based on the basis of 
        /// the prefix text typed in the text box
        /// </summary>
        /// <param name="companyID">Company Id</param>
        /// <param name="prefixText">The text which was typed in the text box</param>
        /// <param name="count">Number of results to be returned</param>
        /// <returns>List of User Names</returns>
        [OperationContract]
        string[] SelectActiveUserNamesByCompanyIdAndPrefixText(int companyID, string prefixText, int count, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Selects all user by the given company id and role id.
        /// </summary>
        /// <param name="companyID">company id</param>
        /// <param name="roleID">role id</param>
        /// <returns>List of users</returns>
        [OperationContract]
        List<UserHdrInfo> SelectAllUserHdrInfoByCompanyIDAndRoleID(int companyID, int roleID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<UserHdrInfo> SelectAllUsersByCompanyIDAndRoleIDsForCurrentRecPeriod(int companyID, List<short> oRoleIDCollection, int? userID, short? roleID, AppUserInfo oAppUserInfo);
        // void UpdateUserRoleByUserID(int userID, List<int> newUserRolesList);

        [OperationContract]
        int InsertUserOWnershipGeographyObjectHdr(List<UserGeographyObjectInfo> oUserGeographyObjectInfoCollection, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Function to Get the Columns Selected by User in the Grid
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="eGrid"></param>
        /// <returns></returns>
        [OperationContract]
        List<GridColumnInfo> GetGridPrefernce(int? UserID, ARTEnums.Grid eGrid, int? RecPeriodID, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Function to Save the Columns Selected by User in the Grid
        /// </summary>
        /// <param name="oGridColumnIDCollection"></param>
        /// <param name="UserID"></param>
        [OperationContract]
        void SaveGridPrefernce(List<int> oGridColumnIDCollection, int? UserID, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Function to Get Count of Total Accounts assigned to User + Role
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="RoleID"></param>
        /// <param name="RecPeriodID"></param>
        /// <returns></returns>
        [OperationContract]
        List<UserHdrInfo> SelectAllUsersByCompanyIDRoleIDs(int companyID, List<short> oRoleIDCollection, int? userID, short? roleID, int recPeriodID, AppUserInfo oAppUserInfo);
        int? GetTotalAccountsCount(int? UserID, short? RoleID, int? RecPeriodID, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Selects Preparer/Reviewer/Approver for the given accounts.
        /// </summary>
        /// <param name="oAccountIDCollection">List of account id</param>
        /// <param name="oNetAccountIDCollection">List of net account id</param>
        /// <param name="recPeriodID">Unique identifier for rec period</param>
        /// <param name="accountAttributeID">Unique identifier for P/R/A attribute id</param>
        /// <returns>List of User Info with UserID and EmailID</returns>
        [OperationContract]
        List<UserHdrInfo> SelectUsersByAccountIDsAndRecPeriodIDAndAccountAttributeID(List<long> oAccountIDCollection, List<int> oNetAccountIDCollection, int recPeriodID, short accountAttributeID, short alertId, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Selects all users, their roles and email ids for a given company
        /// </summary>
        /// <param name="companyID">Unique identifier of the company</param>
        /// <returns>List of user information</returns>
        [OperationContract]
        List<UserHdrInfo> SelectAllUsersRolesAndEmailID(int companyID, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Selects User Hdr Info for the given user id collection
        /// </summary>
        /// <param name="oUserIDCollection">List of Unique identifiers for user</param>
        /// <returns>List of user header</returns>
        [OperationContract]
        List<UserHdrInfo> SelectUserByUserID(List<int> oUserIDCollection, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Inserts user account association in UserAccount Table
        /// </summary>
        /// <param name="accountIDCollection">List of account IDs to be associated with user</param>
        /// <param name="userID">Unique identifier of user</param>
        ////[OperationContract]
        ////void InsertUserAccount(List<long> accountIDCollection, int userID, short roleID);



        [OperationContract]
        List<UserHdrInfo> SelectAllUserHdrInfoByCompanyIDRecPeriodIDAcctAttrIDForRole(int companyID, int recPeriodID, short acctAttrIDForRole, AppUserInfo oAppUserInfo);
        [OperationContract]
        List<UserHdrInfo> SelectPRAByGLDataID(long? glDataID, AppUserInfo oAppUserInfo);


        [OperationContract]
        bool IsValidAccountsAssociated(List<UserGeographyObjectInfo> oUserGeographyObjectInfoCollection, List<long> accountIDCollection, int companyID, int recPeriodID, short roleID, int userID, AppUserInfo oAppUserInfo);


        [OperationContract]
        int InsertUserOWnershipAccountAndGeographyObjectHdr(List<UserGeographyObjectInfo> oUserGeographyObjectInfoCollection, List<long> accountIDCollection, int userID, short roleID, AppUserInfo oAppUserInfo);

        [OperationContract]
        bool DeleteUserOWnershipAccountAndGeographyObjectHdr(List<UserGeographyObjectInfo> oUserGeographyObjectInfoCollection, List<UserAccountInfo> oUserAccountInfoCollection, AppUserInfo oAppUserInfo);
        [OperationContract]
        void SaveUserDashboardPreferences(List<DashboardMstInfo> oDashboardMstInfoCollection, int? UserID, int? RoleID, AppUserInfo oAppUserInfo);
        [OperationContract]
        List<DashboardMstInfo> GetUserDashboardPreferences(int? UserID, int? RoleID, AppUserInfo oAppUserInfo);
        /// <summary>
        /// This method is used to auto populate User Name text box based on the basis of 
        /// the prefix text typed in the text box
        /// </summary>
        /// <param name="companyID">Company Id</param>
        /// <param name="prefixText">The text which was typed in the text box</param>
        /// <param name="count">Number of results to be returned</param>
        /// <returns>List of User Names</returns>
        [OperationContract]
        List<UserHdrInfo> SelectActiveUserHdrInfoByCompanyIdAndPrefixText(int companyID, string prefixText, int count, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Get the list of user roles which are associated at least with one account
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="recPeriodID"></param>
        /// <returns></returns>
        List<UserRoleInfo> SelectUserActiveRolesPRA(int? userID, int? recPeriodID, AppUserInfo oAppUserInfo);


        [OperationContract]
        UserHdrInfo GetLastUserForReviewNote(long glDataID, int companyID, long? reviewNoteID, short currentUserRoleID, int currentUserID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<UserHdrInfo> SelectUsersFromTransit(int? CompanyID, AppUserInfo oAppUserInfo);

        [OperationContract]
        void DeleteUsersFromTransit(int? CompanyID, AppUserInfo oAppUserInfo);

        [OperationContract]
        void AddCompanyUser(List<CompanyUserInfo> oCompanyUserInfoList, string addedBy, DateTime? dateAdded, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<UserHdrInfo> CheckUsersForUniqueness(List<UserHdrInfo> oUserHdrInfoList, bool tryGetUserID, AppUserInfo oAppUserInfo);
        [OperationContract]
        List<UserHdrInfo> SelectActiveUserHdrInfoByCompanyRoleAndPrefixText(int companyID, short roleID, string prefixText, int count, AppUserInfo oAppUserInfo);


        [OperationContract]
        List<UserFTPConfigurationInfo> GetUserFTPConfiguration(int? UserID, AppUserInfo oAppUserInfo);



        [OperationContract]
        void VerifyAndUpdateFTPPassword(int userID, string loginID, string ftpLoginID, string oldPasswordHash, string newPasswordHash, AppUserInfo appUserInfo);
        [OperationContract]
        int UpdateFTPPassword(string loginID, string ftpLoginID, string password, AppUserInfo oAppUserInfo);
        [OperationContract]
        int SaveUserFTPConfiguration(List<UserFTPConfigurationInfo> oUserFTPConfigurationInfoList, AppUserInfo oAppUserInfo);

        [OperationContract]
        bool? IsUserLocked(string loginID);
        [OperationContract]
        UserHdrInfo UpdateFailedLoginAttempts(string loginID, bool isResetLoginAttempt, AppUserInfo oAppUserInfo);
        [OperationContract]
        List<UserLockdownDetailInfo> GetLockdownDetail(int? UserID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<AutoSaveAttributeValueInfo> GetAutoSaveAttributeValues(AutoSaveAttributeParamInfo oAutoSaveAttributeParamInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        void SaveAutoSaveAttributeValues(AutoSaveAttributeParamInfo oAutoSaveAttributeParamInfo, AppUserInfo oAppUserInfo);
    }
}
