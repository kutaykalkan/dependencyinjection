using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.App.DAO;
using System.Data;
using SkyStem.ART.App.Utility;
using System.Data.SqlClient;
using SkyStem.ART.Client.Data;
using System.Transactions;
using SkyStem.ART.App.DAO.CompanyDatabase;
using SkyStem.ART.Client.Model.CompanyDatabase;
using SkyStem.ART.Client.Params;

namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "User" here, you must also update the reference to "User" in Web.config.
    public class User : IUser
    {
        //Do Work - Test Method
        public UserHdrInfo GetUserDetail(int userID, AppUserInfo oAppUserInfo)
        {
            UserHdrInfo oUserHdrInfo = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UserHdrDAO oUserDAO = new UserHdrDAO(oAppUserInfo);
                oUserHdrInfo = oUserDAO.SelectById(userID);

            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oUserHdrInfo;
            //#endif
        }

        /// <summary>
        /// Verify Old Password, and Update with the New one
        /// </summary>
        /// <param name="UserID">User ID of the User whose password has to be updated</param>
        /// <param name="LoginID">Login ID of the User whose password has to be updated</param>
        /// <param name="OldPasswordHash">Hash of Old Password</param>
        /// <param name="NewPasswordHash">Hash of New Password</param>
        /// <returns></returns>
        /// <returns></returns>
        public void VerifyAndUpdatePassword(int UserID, string LoginID, string OldPasswordHash, string NewPasswordHash, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                bool bMatch = oUserHdrDAO.VerifyPassword(UserID, OldPasswordHash);
                if (bMatch)
                {
                    oUserHdrDAO.UpdatePassword(LoginID, NewPasswordHash);
                }
                else
                {
                    throw new ARTException(5000008);
                }

            }
            catch (ARTException ex)
            {
                throw ex;
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

        public int UpdatePassword(string loginID, string password, AppUserInfo oAppUserInfo)
        {
            int rowsAffected = 0;
            try
            {
                oAppUserInfo.LoginID = loginID;
                ServiceHelper.SetConnectionString(oAppUserInfo);
                if (!string.IsNullOrEmpty(oAppUserInfo.ConnectionString))
                {
                    UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                    rowsAffected = oUserHdrDAO.UpdatePassword(loginID, password);
                }
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

        //TODO: (why do we need transaction here?)
        public UserHdrInfo AuthenticateUser(string loginID, string password)
        {
            UserHdrInfo oUserHdrInfo = null;
            AppUserInfo oAppUserInfo = null;
            try
            {
                //ServiceHelper.SetConnectionString(oAppUserInfo);
                oAppUserInfo = new AppUserInfo();
                oAppUserInfo.LoginID = loginID;
                ServiceHelper.SetConnectionString(oAppUserInfo);
                if (!string.IsNullOrEmpty(oAppUserInfo.ConnectionString))
                {
                    UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                    oUserHdrInfo = oUserHdrDAO.AuthenticateUser(loginID, password);
                    if (null != oUserHdrInfo)
                    {
                        oUserHdrInfo.Password = "";// NO password to UI layer
                        UserRoleDAO oUserRoleDAO = new UserRoleDAO(oAppUserInfo);
                        oUserHdrInfo.UserRoleByUserID = oUserRoleDAO.SelectAllByUserID(oUserHdrInfo.UserID);
                    }
                    else
                    {  // This should be sent only when user is locked
                        oUserHdrInfo = UpdateFailedLoginAttempts(loginID, false, oAppUserInfo);
                        if (oUserHdrInfo != null && !oUserHdrInfo.IsUserLocked.GetValueOrDefault())
                            oUserHdrInfo = null;
                    }
                }
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oUserHdrInfo;
        }

        public UserHdrInfo GetUserByLoginID(string loginID, AppUserInfo oAppUserInfo)
        {
            UserHdrInfo oUserHdrInfo = null;
            try
            {
                oAppUserInfo.LoginID = loginID;
                ServiceHelper.SetConnectionString(oAppUserInfo);
                if (!string.IsNullOrEmpty(oAppUserInfo.ConnectionString))
                {
                    UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                    oUserHdrInfo = oUserHdrDAO.GetUserByLoginID(loginID);
                }
                return oUserHdrInfo;
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oUserHdrInfo;
        }

        public int InsertUser(UserHdrInfo oNewUser, List<int> newUserRolesList, bool IsEmailIDUniqueCheckRequired, AppUserInfo oAppUserInfo)
        {
            int newUserId;
            //*************Below Code UnCommented Temporarily by Prafull on 05-Jul-2010*********
            //int IsUnique = oUserHdrDAO.IsUniqueUserCredentials(oNewUser.LoginID, oNewUser.EmailID, 0, IsEmailIDUniqueCheckRequired);
            //if (IsUnique != 1)
            //    throw new ARTException(IsUnique);
            //*********************************************************************************
            List<UserHdrInfo> oUserHdrInfoList = new List<UserHdrInfo>();
            oUserHdrInfoList.Add(oNewUser);
            oUserHdrInfoList = CheckUsersForUniqueness(oUserHdrInfoList, false, oAppUserInfo);
            if (oUserHdrInfoList[0].IsLoginIDUnique.GetValueOrDefault() == false)
                throw new ARTException(5000016);
            if (oUserHdrInfoList[0].IsFTPLoginIDUnique.GetValueOrDefault() == false)
                throw new ARTException(5000413);
            if (IsEmailIDUniqueCheckRequired && oUserHdrInfoList[0].IsEmailIDUnique.GetValueOrDefault() == false)
                throw new ARTException(5000249);

            ServiceHelper.SetConnectionString(oAppUserInfo);
            UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
            #region "Save User and Roles Data"
            //Save User
            using (TransactionScope scope = new TransactionScope())
            {
                if (oAppUserInfo.IsDatabaseExists.GetValueOrDefault())
                {
                    oUserHdrDAO.Save(oNewUser);
                    if (oNewUser.UserID.HasValue) // that means user record has been saved.
                    {
                        //get newly inserted userid
                        newUserId = oNewUser.UserID.Value;
                        //save user roles for newly inserted userid
                        UserRoleDAO oUserRole = new UserRoleDAO(oAppUserInfo);
                        DataTable oUserRoleDataTable = SkyStem.ART.App.Utility.ServiceHelper.ConvertIDCollectionToDataTable(newUserRolesList);
                        object o = oUserRole.SaveUserRoleDataTable(oUserRoleDataTable, newUserId, true);

                        List<CompanyUserInfo> oCompanyUserInfoList = new List<CompanyUserInfo>();
                        CompanyUserInfo oCompanyUserInfo = new CompanyUserInfo();
                        oCompanyUserInfo.CompanyID = oNewUser.CompanyID;
                        oCompanyUserInfo.LoginID = oNewUser.LoginID;
                        oCompanyUserInfo.FTPLoginID = oNewUser.FTPLoginID;
                        oCompanyUserInfo.EmailID = oNewUser.EmailID;
                        oCompanyUserInfo.UserID = oNewUser.UserID;
                        oCompanyUserInfo.AddedBy = oNewUser.AddedBy;
                        oCompanyUserInfo.DateAdded = oNewUser.DateAdded;
                        oCompanyUserInfo.IsActive = oNewUser.IsActive;
                        oCompanyUserInfoList.Add(oCompanyUserInfo);
                        AddCompanyUser(oCompanyUserInfoList, oNewUser.AddedBy, oNewUser.DateAdded, oAppUserInfo);
                    }
                }
                else
                {
                    ServiceHelper.SetConnectionStringCore(oAppUserInfo);
                    UserHdrDAO oUserHdrDAOCore = new UserHdrDAO(oAppUserInfo);
                    oUserHdrDAOCore.InsertUserTransit(oNewUser);
                    UserRoleDAO oUserRole = new UserRoleDAO(oAppUserInfo);
                    DataTable oUserRoleDataTable = ServiceHelper.ConvertIDCollectionToDataTable(newUserRolesList);
                    object o = oUserRole.SaveUserRoleTransitDataTable(oUserRoleDataTable, oNewUser.UserTransitID.Value, true);
                }
                scope.Complete();
                //TODO: Send email to user
            }
            #endregion
            return (int)oNewUser.UserID;
        }

        public void UpdateUser(UserHdrInfo oUserHdrInfo, int count, List<int> listUserRoles, bool hasStatusChanged, bool IsEmailIDUniqueCheckRequired, AppUserInfo oAppUserInfo)
        {
            //int IsUnique = oUserHdrDAO.IsUniqueUserCredentials(oUserHdrInfo.LoginID, oUserHdrInfo.EmailID, (int)oUserHdrInfo.UserID, IsEmailIDUniqueCheckRequired);
            //if (IsUnique != 1)
            //    throw new ARTException(IsUnique);
            //*********************************************************************************
            List<UserHdrInfo> oUserHdrInfoList = new List<UserHdrInfo>();
            oUserHdrInfoList.Add(oUserHdrInfo);
            oUserHdrInfoList = CheckUsersForUniqueness(oUserHdrInfoList, false, oAppUserInfo);
            if (oUserHdrInfoList[0].IsLoginIDUnique.GetValueOrDefault() == false)
                throw new ARTException(5000016);
            if (oUserHdrInfoList[0].IsFTPLoginIDUnique.GetValueOrDefault() == false)
                throw new ARTException(5000413);
            if (IsEmailIDUniqueCheckRequired && oUserHdrInfoList[0].IsEmailIDUnique.GetValueOrDefault() == false)
                throw new ARTException(5000249);

            ServiceHelper.SetConnectionString(oAppUserInfo);
            UserRoleDAO oUserRoleDAO = new UserRoleDAO(oAppUserInfo);
            UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
            // Means User's Status has changed from InActive to Active
            if (hasStatusChanged)
            {
                // Check for User Limit reached
                // If limit reached, user cannot be activated
                CompanyHdrDAO oCompanyHdrDAO = new CompanyHdrDAO(oAppUserInfo);
                if (oCompanyHdrDAO.HasUserLimitReached(oUserHdrInfo.CompanyID))
                {
                    throw new ARTException(5000015);
                }
            }
            using (TransactionScope scope = new TransactionScope())
            {
                oUserHdrDAO.updateUser(oUserHdrInfo);
                if (listUserRoles.Count > 0)
                {
                    oUserRoleDAO.DeleteInsertRoleByUserID((int)oUserHdrInfo.UserID, listUserRoles);

                    DataTable oUserRoleDataTable = SkyStem.ART.App.Utility.ServiceHelper.ConvertIDCollectionToDataTable(listUserRoles);
                    object o = oUserRoleDAO.SaveUserRoleDataTable(oUserRoleDataTable, (int)oUserHdrInfo.UserID, true);
                }
                ServiceHelper.SetConnectionStringCore(oAppUserInfo);
                CompanyUserDAO oCompanyUserDAO = new CompanyUserDAO(oAppUserInfo);
                CompanyUserInfo oCompanyUserInfo = new CompanyUserInfo();
                oCompanyUserInfo.CompanyID = oUserHdrInfo.CompanyID;
                oCompanyUserInfo.UserID = oUserHdrInfo.UserID;
                oCompanyUserInfo.LoginID = oUserHdrInfo.LoginID;
                oCompanyUserInfo.FTPLoginID = oUserHdrInfo.FTPLoginID;
                oCompanyUserInfo.EmailID = oUserHdrInfo.EmailID;
                oCompanyUserInfo.IsActive = oUserHdrInfo.IsActive;
                oCompanyUserInfo.RevisedBy = oUserHdrInfo.RevisedBy;
                oCompanyUserInfo.DateRevised = oUserHdrInfo.DateRevised;
                oCompanyUserDAO.UpdateCompanyUser(oCompanyUserInfo);
                scope.Complete();
            }
        }

        // public void UpdateUserRoleByUserID(int userID, List<int> newUserRolesList)
        /*{
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            try
            {
                UserRoleDAO oUserRoleDAO = new UserRoleDAO(oAppUserInfo);
                oConnection = oUserRoleDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();
                {
                    oUserRoleDAO.DeleteInsertRoleByUserID(userID, newUserRolesList);

                    DataTable oUserRoleDataTable = SkyStem.ART.App.Utility.ServiceHelper.ConvertIDCollectionToDataTable(newUserRolesList);
                    object o = oUserRoleDAO.SaveUserRoleDataTable(oUserRoleDataTable, userID, true, oConnection, oTransaction);
                }
                oTransaction.Commit();
            }

            catch (Exception ex)
            {
                if ((oTransaction != null) && (oConnection.State == ConnectionState.Open))
                {
                    oTransaction.Rollback();
                }
                throw ex;
            }
            finally
            {
                try
                {
                    if ((null != oConnection) && (oConnection.State == ConnectionState.Open))
                        oConnection.Close();
                }
                catch (Exception)
                {
                }

            }

        }*/
        //

        public List<UserHdrInfo> SearchUser(string FirstName, string EmailID, string LastName, int Count,
            int? RoleID, bool? IsActive, int? CompanyID, int loggedInUserID, short loggedInRoleID, int? recPeriodID,
            DateTime? recPeriodEndDate, string SortExpression, string SortDirection, bool IsShowActivationHistory,
            short ActivationHistoryVal, short? ActivationStatusTypeID, short? FTPActivationStatusID, AppUserInfo oAppUserInfo)
        {
            List<UserHdrInfo> oUserHdrInfoCollection = new List<UserHdrInfo>();
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                oUserHdrInfoCollection = oUserHdrDAO.SearchUser(FirstName, EmailID, LastName, Count, RoleID, IsActive,
                    CompanyID, loggedInUserID, loggedInRoleID, recPeriodID, recPeriodEndDate, SortExpression, SortDirection,
                    IsShowActivationHistory, ActivationHistoryVal, ActivationStatusTypeID, FTPActivationStatusID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oUserHdrInfoCollection;

        }

        public List<RoleMstInfo> GetUserRole(int? userID, AppUserInfo oAppUserInfo)
        {
            List<RoleMstInfo> oRoleMstInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                RoleMstDAO oRoleMstDAO = new RoleMstDAO(oAppUserInfo);
                oRoleMstInfoCollection = (List<RoleMstInfo>)oRoleMstDAO.SelectRoleMstDetailsAssociatedToUserHdrByUserRole(userID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oRoleMstInfoCollection;
        }

        /// <summary>
        /// This method is used to auto populate User Name text box based on the basis of 
        /// the prefix text typed in the text box
        /// </summary>
        /// <param name="companyID">Company Id</param>
        /// <param name="prefixText">The text which was typed in the text box</param>
        /// <param name="count">Number of results to be returned</param>
        /// <returns>List of User Names</returns>
        public string[] SelectActiveUserNamesByCompanyIdAndPrefixText(int companyId, string prefixText, int count, AppUserInfo oAppUserInfo)
        {
            string[] oUserNameCollection = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                oUserNameCollection = oUserHdrDAO.SelectActiveUserNamesByCompanyIdAndPrefixText(companyId, prefixText, count);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oUserNameCollection;
        }

        /// <summary>
        /// Selects all user by the given company id and role id.
        /// </summary>
        /// <param name="companyID">company id</param>
        /// <param name="roleID">role id</param>
        /// <returns>List of users</returns>
        public List<UserHdrInfo> SelectAllUserHdrInfoByCompanyIDAndRoleID(int companyID, int roleID, AppUserInfo oAppUserInfo)
        {
            List<UserHdrInfo> oUserHdrInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                oUserHdrInfoCollection = oUserHdrDAO.SelectAllUserHdrInfoByCompanyIDAndRoleID(companyID, roleID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oUserHdrInfoCollection;
        }

        public int InsertUserOWnershipGeographyObjectHdr(List<UserGeographyObjectInfo> oUserGeographyObjectInfoCollection, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;

            UserGeographyObjectDAO obj = new UserGeographyObjectDAO(oAppUserInfo);
            try
            {
                //TODO: validation for number of Licenses to user company.
                ServiceHelper.SetConnectionString(oAppUserInfo);

                #region "Save User and Roles Data"
                //Save User Ownership and update old ownership
                oConnection = obj.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();

                UserGeographyObjectDAO oUserGeographyObjectDAO = new UserGeographyObjectDAO(oAppUserInfo);

                DataTable dtUserGegographyObjectDataTable = SkyStem.ART.App.Utility.ServiceHelper.ConvertUserGeographyObjectInfoCollectionToDataTable(oUserGeographyObjectInfoCollection);
                object o = oUserGeographyObjectDAO.SaveUserOwnershipDataTable(dtUserGegographyObjectDataTable, oConnection, oTransaction);

                //TODO: Send email to user
                oTransaction.Commit();
                #endregion
                return 0;
            }
            catch (Exception ex)
            {
                if ((oTransaction != null) && (oConnection.State == ConnectionState.Open))
                {
                    oTransaction.Rollback();
                }
                throw ex;
            }
            finally
            {
                try
                {
                    if ((null != oConnection) && (oConnection.State == ConnectionState.Open))
                        oConnection.Close();
                }
                catch (Exception)
                {
                }

            }

        }

        public List<UserHdrInfo> SelectAllUsersByCompanyIDAndRoleIDsForCurrentRecPeriod(int companyID, List<short> oRoleIDCollection, int? userID, short? roleID, AppUserInfo oAppUserInfo)
        {
            DataTable oUserRoleDataTable = null;
            UserHdrDAO oUserHdrDAO = null;
            List<UserHdrInfo> oUserInfoCollection = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                oUserRoleDataTable = ServiceHelper.ConvertIDCollectionToDataTable(oRoleIDCollection);
                oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                oUserInfoCollection = oUserHdrDAO.SelectAllUsersByCompanyIDRoleIDsForCurrentRecPeriod(companyID, oUserRoleDataTable, userID, roleID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oUserInfoCollection;
        }

        public List<UserHdrInfo> SelectAllUsersByCompanyIDRoleIDs(int companyID, List<short> oRoleIDCollection, int? userID, short? roleID, int recPeriodID, AppUserInfo oAppUserInfo)
        {
            DataTable oUserRoleDataTable = null;
            UserHdrDAO oUserHdrDAO = null;
            List<UserHdrInfo> oUserInfoCollection = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                oUserRoleDataTable = ServiceHelper.ConvertIDCollectionToDataTable(oRoleIDCollection);
                oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                oUserInfoCollection = oUserHdrDAO.SelectAllUsersByCompanyIDRoleIDs(companyID, oUserRoleDataTable, userID, roleID, recPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oUserInfoCollection;
        }
        public List<UserHdrInfo> SelectAllUserHdrInfoByCompanyIDRecPeriodIDAcctAttrIDForRole(int companyID, int recPeriodID, short acctAttrIDForRole, AppUserInfo oAppUserInfo)
        {
            List<UserHdrInfo> oUserInfoCollection = null;
            UserHdrDAO oUserHdrDAO = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                oUserInfoCollection = oUserHdrDAO.SelectAllUserHdrInfoByCompanyIDRecPeriodIDAcctAttrID(companyID, recPeriodID, acctAttrIDForRole);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oUserInfoCollection;
        }
        #region IUser Members


        public void UpdateLastLoggedInInfo(UserHdrInfo oUserHdrInfo, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                oUserHdrDAO.UpdateLastLoggedInInfo(oUserHdrInfo);
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


        public List<GridColumnInfo> GetGridPrefernce(int? UserID, ARTEnums.Grid eGrid, int? RecPeriodID, AppUserInfo oAppUserInfo)
        {
            List<GridColumnInfo> oGridColumnInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GridColumnDAO oGridColumnDAO = new GridColumnDAO(oAppUserInfo);
                oGridColumnInfoCollection = oGridColumnDAO.GetGridPrefernce(UserID, eGrid, RecPeriodID);
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


        public void SaveGridPrefernce(List<int> oGridColumnIDCollection, int? UserID, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GridColumnDAO oGridColumnDAO = new GridColumnDAO(oAppUserInfo);
                oGridColumnDAO.SaveGridPrefernce(oGridColumnIDCollection, UserID);
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

        public int? GetTotalAccountsCount(int? UserID, short? RoleID, int? RecPeriodID, AppUserInfo oAppUserInfo)
        {
            int? accountCount = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                accountCount = oUserHdrDAO.GetTotalAccountsCount(UserID, RoleID, RecPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return accountCount;
        }

        public List<UserHdrInfo> SelectUsersByAccountIDsAndRecPeriodIDAndAccountAttributeID(List<long> oAccountIDCollection, List<int> oNetAccountIDCollection, int recPeriodID, short accountAttributeID, short alertId, AppUserInfo oAppUserInfo)
        {
            List<UserHdrInfo> oUserHdrInfoCollection = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataTable dtAccountIDs = ServiceHelper.ConvertLongIDCollectionToDataTable(oAccountIDCollection);
                DataTable dtNetAccountIDCollection = ServiceHelper.ConvertIDCollectionToDataTable(oNetAccountIDCollection);
                UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                oUserHdrInfoCollection = oUserHdrDAO.SelectUsersByAccountIDsAndRecPeriodIDAndAccountAttributeID(dtAccountIDs, dtNetAccountIDCollection, recPeriodID, accountAttributeID, alertId);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oUserHdrInfoCollection;
        }

        public List<UserHdrInfo> SelectAllUsersRolesAndEmailID(int companyID, AppUserInfo oAppUserInfo)
        {
            List<UserHdrInfo> oUserHdrInfoCollection = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                oUserHdrInfoCollection = oUserHdrDAO.SelectAllUsersRolesAndEmailID(companyID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oUserHdrInfoCollection;
        }

        public List<UserHdrInfo> SelectUserByUserID(List<int> oUserIDCollection, AppUserInfo oAppUserInfo)
        {
            List<UserHdrInfo> oUserHdrInfoCollection = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataTable dtUserID = ServiceHelper.ConvertIDCollectionToDataTable(oUserIDCollection);
                UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                oUserHdrInfoCollection = oUserHdrDAO.SelectUserByUserID(dtUserID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oUserHdrInfoCollection;
        }

        ////public void InsertUserAccount(List<long> accountIDCollection, int userID, short roleID)
        ////{
        ////    try
        ////    {
        ////        DataTable dtAccountID = ServiceHelper.ConvertLongIDCollectionToDataTable(accountIDCollection);
        ////        UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
        ////        oUserHdrDAO.InsertUserAccount(dtAccountID, userID, roleID);
        ////    }
        ////    catch (SqlException ex)
        ////    {
        ////        ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
        ////    }
        ////}

        #endregion
        public List<UserHdrInfo> SelectPRAByGLDataID(long? glDataID, AppUserInfo oAppUserInfo)
        {
            List<UserHdrInfo> oUserHdrInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                oUserHdrInfoCollection = oUserHdrDAO.SelectPRAByGLDataID(glDataID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oUserHdrInfoCollection;
        }

        public bool IsValidAccountsAssociated(List<UserGeographyObjectInfo> oUserGeographyObjectInfoCollection, List<long> accountIDCollection, int companyID, int recPeriodID, short roleID, int userID, AppUserInfo oAppUserInfo)
        {
            bool bValidAccountsAssociated = false;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                //DataTable dtGeographyObjectID = ServiceHelper.ConvertIDCollectionToDataTable(geographyObjectIDCollection);
                DataTable dtUserGeographyObjectDataTable = SkyStem.ART.App.Utility.ServiceHelper.ConvertUserGeographyObjectInfoCollectionToDataTable(oUserGeographyObjectInfoCollection);
                DataTable dtAccountID = ServiceHelper.ConvertLongIDCollectionToDataTable(accountIDCollection);

                UserGeographyObjectDAO oUserGeographyObjectDAO = new UserGeographyObjectDAO(oAppUserInfo);
                bValidAccountsAssociated = oUserGeographyObjectDAO.IsValidAccountsAssociated(dtUserGeographyObjectDataTable, dtAccountID, companyID, recPeriodID, roleID, userID);

            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return bValidAccountsAssociated;

        }

        public int InsertUserOWnershipAccountAndGeographyObjectHdr(List<UserGeographyObjectInfo> oUserGeographyObjectInfoCollection, List<long> accountIDCollection,
            List<UserHdrInfo> oChildUserRoleDetails, bool bAllAccounts, int userID, short roleID, AppUserInfo oAppUserInfo)
        {

            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            ServiceHelper.SetConnectionString(oAppUserInfo);
            UserGeographyObjectDAO obj = new UserGeographyObjectDAO(oAppUserInfo);
            try
            {
                oConnection = obj.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();


                DataTable dtAccountID = ServiceHelper.ConvertLongIDCollectionToDataTable(accountIDCollection);
                DataTable dtUserGeographyObjectDataTable = ServiceHelper.ConvertUserGeographyObjectInfoCollectionToDataTable(oUserGeographyObjectInfoCollection);
                DataTable dtUserRole = ServiceHelper.ConvertUserAccountByUserRoleToDataTable(oChildUserRoleDetails);

                UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                UserGeographyObjectDAO oUserGeographyObjectDAO = new UserGeographyObjectDAO(oAppUserInfo);

                oUserHdrDAO.InsertUserAccount(dtAccountID, userID, roleID, oConnection, oTransaction);
                oUserHdrDAO.SaveUserAssociationByUserRole(dtUserRole, userID, roleID, oConnection, oTransaction);
                oUserHdrDAO.SaveUserAssociationAllAccounts(bAllAccounts, userID, roleID, oConnection, oTransaction);
                object o = oUserGeographyObjectDAO.SaveUserOwnershipDataTable(dtUserGeographyObjectDataTable, oConnection, oTransaction);

                oTransaction.Commit();
                return 0;
            }
            catch (Exception)
            {
                if ((oTransaction != null) && (oConnection.State == ConnectionState.Open))
                {
                    oTransaction.Rollback();
                }
                throw;
            }
            finally
            {
                if ((null != oConnection) && (oConnection.State == ConnectionState.Open))
                    oConnection.Close();
            }
        }

        public bool DeleteUserOWnershipAccountAndGeographyObjectHdr(List<UserGeographyObjectInfo> oUserGeographyObjectInfoCollection, List<UserAccountInfo> oUserAccountInfoCollection, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            ServiceHelper.SetConnectionString(oAppUserInfo);
            UserGeographyObjectDAO obj = new UserGeographyObjectDAO(oAppUserInfo);
            try
            {
                oConnection = obj.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();

                DataTable dtUserAccountDataTable = ServiceHelper.ConvertUserAccountObjectInfoCollectionToDataTable(oUserAccountInfoCollection);
                DataTable dtUserGeographyObjectDataTable = SkyStem.ART.App.Utility.ServiceHelper.ConvertUserGeographyObjectInfoCollectionToDataTable(oUserGeographyObjectInfoCollection);

                UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                UserGeographyObjectDAO oUserGeographyObjectDAO = new UserGeographyObjectDAO(oAppUserInfo);

                oUserHdrDAO.DeleteUserAccount(dtUserAccountDataTable, oConnection, oTransaction);
                object o = oUserGeographyObjectDAO.DeleteUserGeographyObject(dtUserGeographyObjectDataTable, oConnection, oTransaction);
                oTransaction.Commit();
                return true;


            }

            catch (Exception ex)
            {
                if ((oTransaction != null) && (oConnection.State == ConnectionState.Open))
                {
                    oTransaction.Rollback();
                }
                throw ex;
            }
            finally
            {
                if ((null != oConnection) && (oConnection.State == ConnectionState.Open))
                    oConnection.Close();

            }

        }

        public void SaveUserDashboardPreferences(List<DashboardMstInfo> oDashboardMstInfoCollection, int? UserID, int? RoleID, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataTable dtDashboardPreferences = ServiceHelper.ConvertDashboardMstInfoToDataTable(oDashboardMstInfoCollection);
                UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                oConnection = oUserHdrDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();
                if (oDashboardMstInfoCollection != null && oDashboardMstInfoCollection.Count > 0)
                {
                    oUserHdrDAO.SaveUserDashboardPreferences(dtDashboardPreferences, UserID, RoleID, oConnection, oTransaction);

                }
                oTransaction.Commit();
            }
            catch (SqlException ex)
            {
                if (oTransaction != null && oTransaction.Connection != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                if (oTransaction != null && oTransaction.Connection != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            finally
            {
                if ((oConnection != null) && (oConnection.State == ConnectionState.Open))
                {
                    oConnection.Close();
                    oConnection.Dispose();
                }
            }
        }

        public List<DashboardMstInfo> GetUserDashboardPreferences(int? UserID, int? RoleID, AppUserInfo oAppUserInfo)
        {
            List<DashboardMstInfo> oDashboardMstInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                oDashboardMstInfoCollection = oUserHdrDAO.GetUserDashboardPreferences(UserID, RoleID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oDashboardMstInfoCollection;
        }

        /// <summary>
        /// This method is used to auto populate User Name text box based on the basis of 
        /// the prefix text typed in the text box
        /// </summary>
        /// <param name="companyID">Company Id</param>
        /// <param name="prefixText">The text which was typed in the text box</param>
        /// <param name="count">Number of results to be returned</param>
        /// <returns>List of User Names</returns>
        public List<UserHdrInfo> SelectActiveUserHdrInfoByCompanyIdAndPrefixText(int companyId, string prefixText, int count, AppUserInfo oAppUserInfo)
        {
            List<UserHdrInfo> oUserHdrInfoList = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                oUserHdrInfoList = oUserHdrDAO.SelectActiveUserHdrInfoByCompanyIdAndPrefixText(companyId, prefixText, count);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oUserHdrInfoList;
        }

        public UserHdrInfo GetLastUserForReviewNote(long glDataID, int companyID, long? reviewNoteID, short currentUserRoleID, int currentUserID, AppUserInfo oAppUserInfo)
        {
            UserHdrInfo oUserHdrInfo = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                oUserHdrInfo = oUserHdrDAO.GetLastUserForReviewNote(glDataID, companyID, reviewNoteID, currentUserRoleID, currentUserID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oUserHdrInfo;
        }

        public List<UserRoleInfo> SelectUserActiveRolesPRA(int? userID, int? recPeriodID, AppUserInfo oAppUserInfo)
        {
            List<UserRoleInfo> oUserRoleInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UserRoleDAO oUserRoleDAO = new UserRoleDAO(oAppUserInfo);
                oUserRoleInfoList = oUserRoleDAO.SelectUserActiveRolesPRA(userID, recPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oUserRoleInfoList;
        }

        public List<UserHdrInfo> SelectUsersFromTransit(int? CompanyID, AppUserInfo oAppUserInfo)
        {
            List<UserHdrInfo> oUserHdrInfoList = null;
            try
            {
                ServiceHelper.SetConnectionStringCore(oAppUserInfo);
                UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                oUserHdrInfoList = oUserHdrDAO.SelectUsersFromTransit(CompanyID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oUserHdrInfoList;
        }

        public void DeleteUsersFromTransit(int? CompanyID, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionStringCore(oAppUserInfo);
                UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                oUserHdrDAO.DeleteUsersFromTransit(CompanyID);
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

        public void AddCompanyUser(List<CompanyUserInfo> oCompanyUserInfoList, string addedBy, DateTime? dateAdded, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionStringCore(oAppUserInfo);
                CompanyUserDAO oCompanyUserDAO = new CompanyUserDAO(oAppUserInfo);
                oCompanyUserDAO.AddCompanyUser(oCompanyUserInfoList, addedBy, dateAdded);
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

        public List<UserHdrInfo> CheckUsersForUniqueness(List<UserHdrInfo> oUserHdrInfoList, bool tryGetUserID, AppUserInfo oAppUserInfo)
        {
            try
            {
                // User Duplicity should be checked in core
                ServiceHelper.SetConnectionStringCore(oAppUserInfo);
                UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                return oUserHdrDAO.CheckUsersForUniqueness(oUserHdrInfoList, oAppUserInfo.CompanyID, tryGetUserID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return null;
        }
        public List<UserHdrInfo> SelectActiveUserHdrInfoByCompanyRoleAndPrefixText(int companyId, short roleID, string prefixText, int count, AppUserInfo oAppUserInfo)
        {
            List<UserHdrInfo> oUserHdrInfoList = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                oUserHdrInfoList = oUserHdrDAO.SelectActiveUserHdrInfoByCompanyRoleAndPrefixText(companyId, roleID, prefixText, count);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oUserHdrInfoList;
        }

        public List<UserFTPConfigurationInfo> GetUserFTPConfiguration(int? UserID, AppUserInfo oAppUserInfo)
        {
            List<UserFTPConfigurationInfo> oUserFTPConfigurationInfoList = new List<UserFTPConfigurationInfo>();
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                oUserFTPConfigurationInfoList = oUserHdrDAO.GetUserFTPConfiguration(UserID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oUserFTPConfigurationInfoList;

        }

        public void VerifyAndUpdateFTPPassword(int UserID, string LoginID, string ftpLoginID, string OldPasswordHash, string NewPasswordHash, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                bool bMatch = oUserHdrDAO.VerifyFTPPassword(UserID, OldPasswordHash);
                if (bMatch)
                {
                    oUserHdrDAO.UpdateFTPPassword(LoginID, ftpLoginID, NewPasswordHash);
                }
                else
                {
                    throw new ARTException(5000008);
                }

            }
            catch (ARTException ex)
            {
                throw ex;
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
        public int UpdateFTPPassword(string loginID, string ftpLoginID, string password, AppUserInfo oAppUserInfo)
        {
            int rowsAffected = 0;
            try
            {
                oAppUserInfo.LoginID = loginID;
                ServiceHelper.SetConnectionString(oAppUserInfo);
                if (!string.IsNullOrEmpty(oAppUserInfo.ConnectionString))
                {
                    UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                    rowsAffected = oUserHdrDAO.UpdateFTPPassword(loginID, ftpLoginID, password);
                }
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
        public int SaveUserFTPConfiguration(List<UserFTPConfigurationInfo> oUserFTPConfigurationInfoList, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            int result = 0;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                oConnection = oUserHdrDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();
                DataTable dt = ServiceHelper.ConvertUserFTPConfigurationInfoListToDataTable(oUserFTPConfigurationInfoList);
                result = oUserHdrDAO.SaveUserFTPConfiguration(oUserFTPConfigurationInfoList[0], dt, oConnection, oTransaction);
                oTransaction.Commit();
            }
            catch (ARTException ex)
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed && oTransaction != null)
                    oTransaction.Rollback();
                throw ex;
            }
            catch (SqlException ex)
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed && oTransaction != null)
                    oTransaction.Rollback();
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed && oTransaction != null)
                    oTransaction.Rollback();
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            finally
            {
                if ((null != oConnection) && (oConnection.State == ConnectionState.Open))
                    oConnection.Dispose();
            }
            return result;
        }

        public bool? IsUserLocked(string loginID)
        {
            AppUserInfo oAppUserInfo = null;
            try
            {
                oAppUserInfo = new AppUserInfo();
                oAppUserInfo.LoginID = loginID;
                ServiceHelper.SetConnectionString(oAppUserInfo);
                if (!string.IsNullOrEmpty(oAppUserInfo.ConnectionString))
                {
                    UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                    return oUserHdrDAO.IsUserLocked(loginID);
                }
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return null;
        }

        public UserHdrInfo UpdateFailedLoginAttempts(string loginID, bool isResetLoginAttempt, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                if (!string.IsNullOrEmpty(oAppUserInfo.ConnectionString))
                {
                    UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                    return oUserHdrDAO.UpdateFailedLoginAttempts(loginID, isResetLoginAttempt);
                }
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return null;
        }

        public List<UserLockdownDetailInfo> GetLockdownDetail(int? UserID, AppUserInfo oAppUserInfo)
        {
            List<UserLockdownDetailInfo> oUserLockdownDetailInfoList = new List<UserLockdownDetailInfo>();
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                oUserLockdownDetailInfoList = oUserHdrDAO.GetLockdownDetail(UserID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oUserLockdownDetailInfoList;
        }
        public List<AutoSaveAttributeValueInfo> GetAutoSaveAttributeValues(AutoSaveAttributeParamInfo oAutoSaveAttributeParamInfo, AppUserInfo oAppUserInfo)
        {
            List<AutoSaveAttributeValueInfo> oAutoSaveAttributeValueInfoList = new List<AutoSaveAttributeValueInfo>();
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                oAutoSaveAttributeValueInfoList = oUserHdrDAO.GetAutoSaveAttributeValues(oAutoSaveAttributeParamInfo.UserID, oAutoSaveAttributeParamInfo.RoleID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oAutoSaveAttributeValueInfoList;
        }
        public void SaveAutoSaveAttributeValues(AutoSaveAttributeParamInfo oAutoSaveAttributeParamInfo, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                oUserHdrDAO.SaveAutoSaveAttributeValues(oAutoSaveAttributeParamInfo.AutoSaveAttributeValueInfoList, oAutoSaveAttributeParamInfo.UserLoginID, oAutoSaveAttributeParamInfo.DateRevised);
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

        public List<UserHdrInfo> SelectUserAssociationByUserRole(int? userID, short? roleID, AppUserInfo oAppUserInfo)
        {
            List<UserHdrInfo> oUserHdrInfoList = new List<UserHdrInfo>();
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                oUserHdrInfoList = oUserHdrDAO.SelectUserAssociationByUserRole(userID, roleID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oUserHdrInfoList;
        }

        public bool SelectUserAssociationAllAccount(int? userID, short? roleID, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                return oUserHdrDAO.SelectUserAssociationAllAccount(userID, roleID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return false;
        }

        public void SaveUserAssociationByUserRole(List<UserHdrInfo> oChildUserRoleDetails, int userID, short roleID, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            ServiceHelper.SetConnectionString(oAppUserInfo);
            try
            {
                DataTable dtUserRole = ServiceHelper.ConvertUserAccountByUserRoleToDataTable(oChildUserRoleDetails);
                UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                oConnection = oUserHdrDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();
                oUserHdrDAO.SaveUserAssociationByUserRole(dtUserRole, userID, roleID, oConnection, oTransaction);
                oTransaction.Commit();
            }
            catch (Exception)
            {
                if ((oTransaction != null) && (oConnection.State == ConnectionState.Open))
                {
                    oTransaction.Rollback();
                }
                throw;
            }
            finally
            {
                if ((null != oConnection) && (oConnection.State == ConnectionState.Open))
                    oConnection.Close();
            }
        }
    }
}
