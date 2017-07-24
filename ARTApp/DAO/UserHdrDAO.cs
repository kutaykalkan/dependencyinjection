


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.App.Utility;
using System.Linq;
using System.Transactions;

namespace SkyStem.ART.App.DAO
{
    public class UserHdrDAO : UserHdrDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UserHdrDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        //public const int NULL_INT = int.MinValue; //	To be used to represent null / unassigned int

        public UserHdrInfo AuthenticateUser(string loginID, string password)
        {
            UserHdrInfo oUserHdrInfo = null;
            IDbConnection oConnection = null;
            try
            {
                oConnection = CreateConnection();
                oConnection.Open();

                IDbCommand cmd;
                cmd = CreateSelectOneCommand(loginID, password);
                cmd.Connection = oConnection;

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    oUserHdrInfo = (UserHdrInfo)MapObject(reader);
                }
                reader.ClearColumnHash();
                reader.Close();
            }
            catch (Exception ex)
            {
                //log exception
                throw ex;
            }
            finally
            {

                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                    oConnection.Dispose();
            }
            return oUserHdrInfo;
        }

        /// <summary>
        /// For forgotPassword page,(mailing)
        /// </summary>
        /// <param name="loginID"></param>
        /// <returns></returns>
        public UserHdrInfo GetUserByLoginID(string loginID)
        {
            UserHdrInfo objUserHdrInfo = null;
            IDbConnection conn = null;

            try
            {
                conn = CreateConnection();
                conn.Open();

                IDbCommand cmd;
                cmd = CreateSelectOneByLoginIDCommand(loginID);
                cmd.Connection = conn;

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    objUserHdrInfo = (UserHdrInfo)MapObject(reader);
                }
                reader.ClearColumnHash();
                reader.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    if (conn != null)
                        conn.Close();
                }
                catch (Exception)
                {
                }
            }
            return objUserHdrInfo;

        }

        public int UpdatePassword(string loginID, string password)
        {
            IDbConnection conn = null;
            try
            {
                conn = CreateConnection();
                conn.Open();
                IDbCommand cmd;
                cmd = CreateUpdatePasswordCommand(loginID, password);
                cmd.Connection = conn;
                Object oReturnObject = cmd.ExecuteScalar();
                return (oReturnObject == null) ? 0 : (int)oReturnObject;
                //intResult = Convert.ToInt32(cmd.ExecuteScalar().ToString());

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    if (conn != null)
                        conn.Close();
                }
                catch (Exception)
                {
                }
            }
        }

        /// <summary>
        /// This method returns number of active users for a company
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public int GetActiveUsersByCompanyId(int companyId)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateActiveUserByCompanyIdCommand(companyId);
                cmd.Connection = con;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                    con.Close();
            }
        }

        /// <summary>
        /// This method returns a boolean value indicating if user loginId and emailid are unique
        /// </summary>
        /// <param name="loginID">login id for user</param>
        /// <param name="emailID">email id for user</param>
        /// <returns>boolean value to indicate if liginid and emailid are unique or not</returns>
        public int IsUniqueUserCredentials(string loginID, string emailID, int UserID, bool IsEmailIDUniqueCheckRequired)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateIsUniqueCredentialsCommand(loginID, emailID, UserID, IsEmailIDUniqueCheckRequired);
                cmd.Connection = con;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                    con.Close();
            }
        }

        public int IsUniqueUserCredentialsInUpdate(string loginID, string emailID, int UserID, bool IsEmailIDUniqueCheckRequired)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateIsUniqueUpdateCredentialsCommand(loginID, emailID, UserID, IsEmailIDUniqueCheckRequired);
                cmd.Connection = con;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                    con.Close();
            }
        }

        /// <summary>
        /// This method creates command for getting number of active users in a company
        /// </summary>
        /// <param name="companyID">company id</param>
        /// <returns>An object of IDbCommand</returns>
        protected IDbCommand CreateActiveUserByCompanyIdCommand(int companyID)
        {
            IDbCommand cmd = this.CreateCommand("usp_GET_NumberOfActiveUsersByCompanyID");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = companyID;
            cmdParams.Add(parCompanyID);
            return cmd;
        }
        protected IDbCommand CreateIsUniqueCredentialsCommand(string loginID, string emailID, int UserID, bool IsEmailIDUniqueCheckRequired)
        {
            IDbCommand cmd = this.CreateCommand("usp_GET_IsUniqueUserCredentials");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter parLoginID = cmd.CreateParameter();
            IDbDataParameter parEmailID = cmd.CreateParameter();
            IDbDataParameter parUserID = cmd.CreateParameter();
            IDbDataParameter parIsEmailIDUniqueCheckRequired = cmd.CreateParameter();

            parLoginID.ParameterName = "@LoginID";
            parLoginID.Value = loginID;

            parEmailID.ParameterName = "@EmailID";
            parEmailID.Value = emailID;

            parUserID.ParameterName = "@UserID";
            parUserID.Value = UserID;

            parIsEmailIDUniqueCheckRequired.ParameterName = "@IsEmailIDUniqueCheckRequired";
            parIsEmailIDUniqueCheckRequired.Value = IsEmailIDUniqueCheckRequired;

            cmdParams.Add(parLoginID);
            cmdParams.Add(parEmailID);
            cmdParams.Add(parUserID);
            cmdParams.Add(parIsEmailIDUniqueCheckRequired);

            return cmd;
        }

        protected IDbCommand CreateIsUniqueUpdateCredentialsCommand(string loginID, string emailID, int UserID, bool IsEmailIDUniqueCheckRequired)
        {
            IDbCommand cmd = this.CreateCommand("usp_GET_IsUniqueUserCredentials");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter parLoginID = cmd.CreateParameter();
            IDbDataParameter parEmailID = cmd.CreateParameter();
            IDbDataParameter parUserID = cmd.CreateParameter();
            IDbDataParameter parIsEmailIDUniqueCheckRequired = cmd.CreateParameter();

            parLoginID.ParameterName = "@LoginID";
            parLoginID.Value = loginID;

            parEmailID.ParameterName = "@EmailID";
            parEmailID.Value = emailID;

            parUserID.ParameterName = "@UserID";
            parUserID.Value = UserID;

            parIsEmailIDUniqueCheckRequired.ParameterName = "@IsEmailIDUniqueCheckRequired";
            parIsEmailIDUniqueCheckRequired.Value = IsEmailIDUniqueCheckRequired;

            cmdParams.Add(parLoginID);
            cmdParams.Add(parEmailID);
            cmdParams.Add(parUserID);
            cmdParams.Add(parIsEmailIDUniqueCheckRequired);

            return cmd;
        }


        public List<UserHdrInfo> SearchUser(string FirstName, string EmailID, string LastName, int Count,
            int? RoleID, bool? IsActive, int? CompanyID, int loggedInUserID, short loggedInRoleID, int? recPeriodID,
            DateTime? recPeriodEndDate, string SortExpression, string SortDirection, bool IsShowActivationHistory,
            short ActivationHistoryVal, short? ActivationStatusTypeID, short? FTPActivationStatusID)
        {

            UserHdrInfo objUserHdrInfo = null;
            IDbConnection conn = null;
            //IDbTransaction trans = null;
            List<UserHdrInfo> objUserHdrInfocollection = new List<UserHdrInfo>();
            try
            {
                conn = CreateConnection();
                conn.Open();
                //trans = conn.BeginTransaction();
                IDbCommand cmd;
                cmd = CreateSearchUserCommand(FirstName, EmailID, LastName, Count, RoleID, IsActive, CompanyID, loggedInUserID,
                    loggedInRoleID, recPeriodID, recPeriodEndDate, SortExpression, SortDirection, IsShowActivationHistory,
                    ActivationHistoryVal, ActivationStatusTypeID, FTPActivationStatusID);
                cmd.Connection = conn;
                //cmd.Transaction = trans;
                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    objUserHdrInfo = (UserHdrInfo)MapObject(reader);
                    MapUserStatusDetailInfoObject(objUserHdrInfo, reader);
                    objUserHdrInfocollection.Add(objUserHdrInfo);
                }
                reader.ClearColumnHash();
                reader.Close();
                //if (objUserHdrInfo == null)
                //{
                //    trans.Rollback();
                //}
                //else
                //    if (trans.Connection != null)
                //    {
                //        trans.Commit();
                //    }
                if (objUserHdrInfocollection != null && objUserHdrInfocollection.Count > 0)
                {
                    List<int> oIDCollection = (from oUser in objUserHdrInfocollection select oUser.UserID.HasValue ? oUser.UserID.Value : 0).Distinct().ToList();
                    DataTable dtuserId = ServiceHelper.ConvertIDCollectionToDataTable(oIDCollection);
                    UserHdrInfo AllUserRolesAndStatusDetail = GetAllUserRolesAndStatusDetail(dtuserId);
                    foreach (var item in objUserHdrInfocollection)
                    {
                        if (AllUserRolesAndStatusDetail != null && AllUserRolesAndStatusDetail.UserRoleList != null && AllUserRolesAndStatusDetail.UserRoleList.Count > 0)
                            item.UserRoleList = AllUserRolesAndStatusDetail.UserRoleList.FindAll(o => o.UserID == item.UserID).ToList();
                        //if (AllUserRolesAndStatusDetail != null && AllUserRolesAndStatusDetail.UserStatusDetailList != null && AllUserRolesAndStatusDetail.UserStatusDetailList.Count > 0)
                        //    item.UserStatusDetailList = AllUserRolesAndStatusDetail.UserStatusDetailList.FindAll(o => o.UserID == item.UserID).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                //if (trans != null)
                //    trans.Rollback();
                //log exception
                throw ex;
            }
            finally
            {
                try
                {
                    if (conn != null)
                        conn.Close();
                }
                catch (Exception)
                {
                }
            }
            return objUserHdrInfocollection;


        }

        protected IDbCommand CreateSearchUserCommand(string FirstName, string EmailID, string LastName, int Count,
            int? RoleID, bool? IsActive, int? CompanyID, int loggedInUserID, short loggedInRoleID, int? recPeriodID,
            DateTime? recPeriodEndDate, string SortExpression, string SortDirection, bool? IsShowActivationHistory,
            short ActivationHistoryVal, short? ActivationStatusTypeID, short? FTPActivationStatusID)
        {

            IDbCommand cmd = this.CreateCommand("usp_SEL_SearchUser");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter parFirstName = cmd.CreateParameter();
            parFirstName.ParameterName = "@FirstName";
            parFirstName.Value = FirstName;
            cmdParams.Add(parFirstName);

            IDbDataParameter parEmailID = cmd.CreateParameter();
            parEmailID.ParameterName = "@EmailID";
            parEmailID.Value = EmailID;
            cmdParams.Add(parEmailID);

            IDbDataParameter parLastName = cmd.CreateParameter();
            parLastName.ParameterName = "@LastName";
            parLastName.Value = LastName;
            cmdParams.Add(parLastName);

            IDbDataParameter parCount = cmd.CreateParameter();
            parCount.ParameterName = "@Count";
            parCount.Value = Count;
            cmdParams.Add(parCount);

            IDbDataParameter parrole = cmd.CreateParameter();
            parrole.ParameterName = "@RoleID";
            if (RoleID == null)
            {
                parrole.Value = DBNull.Value;
            }
            else
                parrole.Value = RoleID;
            cmdParams.Add(parrole);

            IDbDataParameter parisActive = cmd.CreateParameter();
            parisActive.ParameterName = "@IsActive";
            if (IsActive == null)
            {
                parisActive.Value = DBNull.Value;
            }
            else
                parisActive.Value = IsActive;
            cmdParams.Add(parisActive);

            IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (CompanyID == null)
            {
                parCompanyID.Value = DBNull.Value;
            }
            else
                parCompanyID.Value = CompanyID.Value;
            cmdParams.Add(parCompanyID);

            IDbDataParameter parLoggedInUserID = cmd.CreateParameter();
            parLoggedInUserID.ParameterName = "@LoggedInUserID";
            parLoggedInUserID.Value = loggedInUserID;
            cmdParams.Add(parLoggedInUserID);

            IDbDataParameter parLoggedInRoleID = cmd.CreateParameter();
            parLoggedInRoleID.ParameterName = "@LoggedInRoleID";
            parLoggedInRoleID.Value = loggedInRoleID;
            cmdParams.Add(parLoggedInRoleID);



            IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            if (recPeriodID.HasValue)
                parRecPeriodID.Value = recPeriodID;
            else
                parRecPeriodID.Value = DBNull.Value;
            cmdParams.Add(parRecPeriodID);


            IDbDataParameter parRecPeriodEndDate = cmd.CreateParameter();
            parRecPeriodEndDate.ParameterName = "@RecPeriodEndDate";

            if (recPeriodEndDate.HasValue)
                parRecPeriodEndDate.Value = recPeriodEndDate;
            else
                parRecPeriodEndDate.Value = DBNull.Value;
            cmdParams.Add(parRecPeriodEndDate);

            ServiceHelper.AddCommonSortParameters(cmd, cmdParams, SortExpression, SortDirection);

            IDbDataParameter parIsShowActivationHistory = cmd.CreateParameter();
            parIsShowActivationHistory.ParameterName = "@IsShowActivationHistory";

            if (IsShowActivationHistory.HasValue)
                parIsShowActivationHistory.Value = IsShowActivationHistory;
            else
                parIsShowActivationHistory.Value = DBNull.Value;
            cmdParams.Add(parIsShowActivationHistory);

            IDbDataParameter parActivationHistoryVal = cmd.CreateParameter();
            parActivationHistoryVal.ParameterName = "@ActivationHistoryVal";

            if (ActivationHistoryVal != 0)
                parActivationHistoryVal.Value = ActivationHistoryVal;
            else
                parActivationHistoryVal.Value = DBNull.Value;
            cmdParams.Add(parActivationHistoryVal);

            IDbDataParameter parActivationStatusTypeID = cmd.CreateParameter();
            parActivationStatusTypeID.ParameterName = "@ActivationStatusTypeID";

            if (ActivationStatusTypeID.GetValueOrDefault() != 0)
                parActivationStatusTypeID.Value = ActivationStatusTypeID;
            else
                parActivationStatusTypeID.Value = DBNull.Value;
            cmdParams.Add(parActivationStatusTypeID);

            IDbDataParameter parFTPActivationStatusID = cmd.CreateParameter();
            parFTPActivationStatusID.ParameterName = "@FTPActivationStatusID";

            if (FTPActivationStatusID.HasValue)
                parFTPActivationStatusID.Value = FTPActivationStatusID;
            else
                parFTPActivationStatusID.Value = DBNull.Value;
            cmdParams.Add(parFTPActivationStatusID);

            return cmd;
        }


        private UserHdrInfo GetAllUserRolesAndStatusDetail(DataTable DtUserID)
        {

            UserHdrInfo objUserHdrInfo = new UserHdrInfo();
            List<UserRoleInfo> oUserRoleInfoList = new List<UserRoleInfo>();
            //List<UserStatusDetailInfo> oUserStatusDetailInfoList = new List<UserStatusDetailInfo>();
            //UserStatusDetailInfo oUserStatusDetailInfo;
            UserRoleInfo oUserRoleInfo;
            IDbConnection conn = null;
            try
            {
                conn = CreateConnection();
                conn.Open();
                IDbCommand cmd;
                cmd = CreateGetAllUserRolesAndStatusDetailCommand(DtUserID);
                cmd.Connection = conn;
                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                // user roles
                while (reader.Read())
                {
                    oUserRoleInfo = MapUserRoleInfoObject(reader);
                    oUserRoleInfoList.Add(oUserRoleInfo);
                }
                //reader.ClearColumnHash();
                //// user status detail
                //reader.NextResult();
                //while (reader.Read())
                //{
                //    oUserStatusDetailInfo = MapUserStatusDetailInfoObject(reader);
                //    oUserStatusDetailInfoList.Add(oUserStatusDetailInfo);
                //}

                reader.Close();
                objUserHdrInfo.UserRoleList = oUserRoleInfoList;
                //objUserHdrInfo.UserStatusDetailList = oUserStatusDetailInfoList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    if (conn != null)
                        conn.Close();
                }
                catch (Exception)
                {
                }
            }
            return objUserHdrInfo;


        }
        private UserRoleInfo MapUserRoleInfoObject(IDataReader reader)
        {
            UserRoleInfo oUserRoleInfo = new UserRoleInfo();

            oUserRoleInfo.UserID = reader.GetInt32Value("UserID");
            oUserRoleInfo.RoleLabelID = reader.GetInt32Value("RoleLabelID");
            oUserRoleInfo.Role = reader.GetStringValue("Role");
            oUserRoleInfo.RoleID = reader.GetInt16Value("RoleID");
            oUserRoleInfo.IsDefaultRole = reader.GetBooleanValue("IsDefaultRole");

            return oUserRoleInfo;
        }
        private void MapUserStatusDetailInfoObject(UserHdrInfo obj, IDataReader reader)
        {
            //UserStatusDetailInfo oUserStatusDetailInfo = new UserStatusDetailInfo();
            //oUserStatusDetailInfo.UserID = reader.GetInt32Value("UserID");
            obj.UserStatusDate = reader.GetDateValue("UserStatusDate");
            obj.UserStatusID = reader.GetInt16Value("UserStatusID");
            // obj.AddedByRoleID = reader.GetInt16Value("AddedByRoleID");
            obj.AddedByUserID = reader.GetInt32Value("AddedByUserID");
            obj.UserStatus = reader.GetStringValue("UserStatus");
            obj.UserStatusLabelID = reader.GetInt32Value("UserStatusLabelID");
            obj.AddedByUserName = reader.GetStringValue("AddedByUserName");
            //return oUserStatusDetailInfo;
        }



        protected IDbCommand CreateGetAllUserRolesAndStatusDetailCommand(DataTable DtUserID)
        {

            IDbCommand cmd = this.CreateCommand("usp_SEL_UserRoleAndStatusByUserIDs");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter parudtUserID = cmd.CreateParameter();
            parudtUserID.ParameterName = "@udtUserID";
            parudtUserID.Value = DtUserID;
            cmdParams.Add(parudtUserID);



            return cmd;
        }


        #region CreateSelectActiveUserNamesByCompanyIdAndPrefixTextCommand
        /// <summary>
        /// This method is used to auto populate User Name text box based on the basis of 
        /// the prefix text typed in the text box
        /// </summary>
        /// <param name="companyID">Company Id</param>
        /// <param name="prefixText">The text which was typed in the text box</param>
        /// <param name="count">Number of results to be returned</param>
        /// <returns>List of User Names</returns>
        public string[] SelectActiveUserNamesByCompanyIdAndPrefixText(int companyId, string prefixText, int count)
        {
            List<string> oUserHeaderInfoCollection = new List<string>();
            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateSelectActiveUserNamesByCompanyIdAndPrefixTextCommand(companyId, prefixText, count);
                cmd.Connection = con;
                con.Open();
                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    oUserHeaderInfoCollection.Add(reader.GetStringValue("Name"));
                }
                reader.Close();

            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                    con.Close();
            }

            return oUserHeaderInfoCollection.ToArray();
        }

        /// <summary>
        /// Creates command to use store procedure and fills all parameter values
        /// </summary>
        /// <param name="companyID">Company Id</param>
        /// <param name="prefixText">The text which was typed in the text box</param>
        /// <param name="count">Number of results to be returned</param>
        /// <returns>Command which is to be executed</returns>
        private IDbCommand CreateSelectActiveUserNamesByCompanyIdAndPrefixTextCommand(int companyId, string prefixText, int count)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_UserNameByCompanyIDAndPrefixText");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter parCompanyID = cmd.CreateParameter();
            IDbDataParameter parPrefixText = cmd.CreateParameter();
            IDbDataParameter parCount = cmd.CreateParameter();

            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = companyId;

            parPrefixText.ParameterName = "@PrefixText";
            parPrefixText.Value = prefixText;

            parCount.ParameterName = "@Count";
            parCount.Value = count;

            cmdParams.Add(parCompanyID);
            cmdParams.Add(parPrefixText);
            cmdParams.Add(parCount);

            return cmd;
        }
        /// <summary>
        /// This method is used to auto populate User Name text box based on the basis of 
        /// the prefix text typed in the text box
        /// </summary>
        /// <param name="companyID">Company Id</param>
        /// <param name="prefixText">The text which was typed in the text box</param>
        /// <param name="count">Number of results to be returned</param>
        /// <returns>List of User Names</returns>
        public List<UserHdrInfo> SelectActiveUserHdrInfoByCompanyIdAndPrefixText(int companyId, string prefixText, int count)
        {
            List<UserHdrInfo> oUserHdrInfoList = new List<UserHdrInfo>();
            UserHdrInfo oUserHdrInfo;
            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateSelectActiveUserHdrInfoByCompanyIdAndPrefixTextCommand(companyId, prefixText, count);
                cmd.Connection = con;
                con.Open();
                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    oUserHdrInfo = new UserHdrInfo();
                    //oUserHdrInfo = MapObject(reader);
                    oUserHdrInfo.UserID = reader.GetInt32Value("UserID");
                    oUserHdrInfo.Name = reader.GetStringValue("Name");
                    oUserHdrInfoList.Add(oUserHdrInfo);
                }
                reader.ClearColumnHash();
                reader.Close();

            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                    con.Close();
            }

            return oUserHdrInfoList;
        }
        /// <summary>
        /// Creates command to use store procedure and fills all parameter values
        /// </summary>
        /// <param name="companyID">Company Id</param>
        /// <param name="prefixText">The text which was typed in the text box</param>
        /// <param name="count">Number of results to be returned</param>
        /// <returns>Command which is to be executed</returns>
        private IDbCommand CreateSelectActiveUserHdrInfoByCompanyIdAndPrefixTextCommand(int companyId, string prefixText, int count)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_ActiveUserByCompanyIDAndPrefixText");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter parCompanyID = cmd.CreateParameter();
            IDbDataParameter parPrefixText = cmd.CreateParameter();
            IDbDataParameter parCount = cmd.CreateParameter();

            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = companyId;

            parPrefixText.ParameterName = "@PrefixText";
            parPrefixText.Value = prefixText;

            parCount.ParameterName = "@Count";
            parCount.Value = count;

            cmdParams.Add(parCompanyID);
            cmdParams.Add(parPrefixText);
            cmdParams.Add(parCount);

            return cmd;
        }

        #endregion

        #region "SelectAllUserHdrInfoByCompanyIDAndRoleID"


        public List<UserHdrInfo> SelectAllUsersByCompanyIDRoleIDsForCurrentRecPeriod(int companyID, DataTable dtRoleID, int? userID, short? roleID)
        {
            List<UserHdrInfo> oUserHdrInfoCollection = new List<UserHdrInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader reader = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateSelectAllUserByCompanyIDRoleIDsCommandForCurrentRecPeriod(companyID, dtRoleID, userID, roleID);
                cmd.Connection = con;
                con.Open();
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    UserHdrInfo oUserHdrInfo = this.MapObject(reader);
                    oUserHdrInfoCollection.Add(oUserHdrInfo);
                }
                reader.ClearColumnHash();
                reader.Close();
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                if ((con != null) && (con.State == ConnectionState.Open))
                    con.Close();
            }
            return oUserHdrInfoCollection;
        }

        public List<UserHdrInfo> SelectAllUsersByCompanyIDRoleIDs(int companyID, DataTable dtRoleID, int? userID, short? roleID, int recPeriodID)
        {
            List<UserHdrInfo> oUserHdrInfoCollection = new List<UserHdrInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader reader = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateSelectAllUserByCompanyIDRoleIDsCommand(companyID, dtRoleID, userID, roleID, recPeriodID);
                cmd.Connection = con;
                con.Open();
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    UserHdrInfo oUserHdrInfo = this.MapObject(reader);
                    oUserHdrInfoCollection.Add(oUserHdrInfo);
                }
                reader.ClearColumnHash();
                reader.Close();
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                if ((con != null) && (con.State == ConnectionState.Open))
                    con.Close();
            }
            return oUserHdrInfoCollection;
        }

        /// <summary>
        /// Selects all user by the given company id and role id.
        /// </summary>
        /// <param name="companyID">company id</param>
        /// <param name="roleID">role id</param>
        /// <returns>List of users</returns>
        public List<UserHdrInfo> SelectAllUserHdrInfoByCompanyIDAndRoleID(int companyID, int roleID)
        {
            List<UserHdrInfo> oUserHdrInfoCollection = new List<UserHdrInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateSelectAllUserHdrInfoByCompanyIDAndRoleIDCommand(companyID, roleID);
                cmd.Connection = con;
                con.Open();
                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                UserRoleDAO oUserRoleDAO = new UserRoleDAO(this.CurrentAppUserInfo);
                while (reader.Read())
                {
                    UserHdrInfo oUserHdrInfo = this.MapObject(reader);
                    oUserHdrInfo.UserRoleByUserID = oUserRoleDAO.SelectAllByUserID(oUserHdrInfo.UserID);
                    oUserHdrInfoCollection.Add(oUserHdrInfo);
                }
                reader.ClearColumnHash();
                reader.Close();
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                    con.Close();
            }

            return oUserHdrInfoCollection;
        }

        public List<UserHdrInfo> SelectAllUserHdrInfoByCompanyIDRecPeriodIDAcctAttrID(int companyID, int recPeriodID, short acctAttrIDForRole)
        {
            List<UserHdrInfo> oUserHdrInfoCollection = new List<UserHdrInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader reader = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateSelectAllUsersByCompanyIDRecPeriodIDAcctAttrIDForRole(companyID, recPeriodID, acctAttrIDForRole);
                cmd.Connection = con;
                cmd.Connection.Open();
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                oUserHdrInfoCollection = this.MapObjects(reader);
                reader.ClearColumnHash();
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                    con.Close();
            }

            return oUserHdrInfoCollection;
        }
        /// <summary>
        /// Creates command to use store procedure and fills all parameter values
        /// </summary>
        /// <param name="companyID">company id</param>
        /// <param name="roleID">role id</param>
        /// <returns>Command which needs to be executed</returns>
        private IDbCommand CreateSelectAllUserHdrInfoByCompanyIDAndRoleIDCommand(int companyID, int roleID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_AllUserHdrInfoByCompanyIDAndRoleID");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter parCompanyID = cmd.CreateParameter();
            IDbDataParameter parRoleID = cmd.CreateParameter();

            parRoleID.ParameterName = "@RoleID";
            parRoleID.Value = roleID;

            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = companyID;

            cmdParams.Add(parCompanyID);
            cmdParams.Add(parRoleID);

            return cmd;
        }

        private IDbCommand CreateSelectAllUserByCompanyIDRoleIDsCommandForCurrentRecPeriod(int companyID, DataTable dtRoles, int? userID, short? roleID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_UserByCompanyAndRoleID");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter parCompanyID = cmd.CreateParameter();
            IDbDataParameter parRoleID = cmd.CreateParameter();

            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = companyID;

            parRoleID.ParameterName = "@RoleIDTable";
            parRoleID.Value = dtRoles;

            cmdParams.Add(parCompanyID);
            cmdParams.Add(parRoleID);

            return cmd;
        }

        private IDbCommand CreateSelectAllUserByCompanyIDRoleIDsCommand(int companyID, DataTable dtRoles, int? userID, short? roleID, int recPeriodID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_AllUsersByCompanyIDRoleIDs");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter parCompanyID = cmd.CreateParameter();
            IDbDataParameter parRoleID = cmd.CreateParameter();
            IDbDataParameter parCurrentUserID = cmd.CreateParameter();
            IDbDataParameter parCurrentRoleID = cmd.CreateParameter();
            IDbDataParameter parRecPeriodID = cmd.CreateParameter();

            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = companyID;

            parRoleID.ParameterName = "@udtRoleID";
            parRoleID.Value = dtRoles;

            parCurrentUserID.ParameterName = "@CurrentUserID";
            if (userID.HasValue && userID.Value > 0)
            {
                parCurrentUserID.Value = userID.Value;
            }
            else
            {
                parCurrentUserID.Value = DBNull.Value;
            }

            parCurrentRoleID.ParameterName = "@CurrentRoleID";
            if (roleID.HasValue && roleID.Value > 0)
            {
                parCurrentRoleID.Value = roleID.Value;
            }
            else
            {
                parCurrentRoleID.Value = DBNull.Value;
            }

            parRecPeriodID.ParameterName = "@recPeriodID";
            parRecPeriodID.Value = recPeriodID;

            cmdParams.Add(parCompanyID);
            cmdParams.Add(parRoleID);
            cmdParams.Add(parCurrentUserID);
            cmdParams.Add(parCurrentRoleID);
            cmdParams.Add(parRecPeriodID);

            return cmd;
        }

        private IDbCommand CreateSelectAllUsersByCompanyIDRecPeriodIDAcctAttrIDForRole(int companyID, int recPeriodID, short acctAttrIDForRole)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_AllUsersByCompanyIDRecPeriodIDAcctAttrIDForRole");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter parCompanyID = cmd.CreateParameter();
            IDbDataParameter parAcctAttrIDForRole = cmd.CreateParameter();
            IDbDataParameter parRecPeriodID = cmd.CreateParameter();

            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = companyID;

            parRecPeriodID.ParameterName = "@RecPeriodID";
            parRecPeriodID.Value = recPeriodID;

            parAcctAttrIDForRole.ParameterName = "@AccountAttrIDForRole";
            parAcctAttrIDForRole.Value = acctAttrIDForRole;

            cmdParams.Add(parCompanyID);
            cmdParams.Add(parRecPeriodID);
            cmdParams.Add(parAcctAttrIDForRole);

            return cmd;

        }
        #endregion



        public void updateUser(UserHdrInfo objUserHdrInfo)
        {
            int intResult = 0;
            IDbCommand cmd;
            using (IDbConnection cnn = this.CreateConnection())
            {
                cnn.Open();
                cmd = CreateUpdateCommand(objUserHdrInfo);
                cmd.Connection = cnn;
                intResult = cmd.ExecuteNonQuery();
            }
        }

        #region UpdateLastLoggedInInfo
        internal void UpdateLastLoggedInInfo(UserHdrInfo oUserHdrInfo)
        {
            System.Data.IDbCommand cmd = null;

            try
            {
                cmd = CreateUpdateLastLoggedInInfoCommand(oUserHdrInfo);
                cmd.Connection = this.CreateConnection();
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (cmd != null)
                {
                    if (cmd.Connection != null)
                    {
                        if (cmd.Connection.State != ConnectionState.Closed)
                        {
                            cmd.Connection.Close();
                            cmd.Connection.Dispose();
                        }
                    }
                    cmd.Dispose();
                }

            }

        }

        private IDbCommand CreateUpdateLastLoggedInInfoCommand(UserHdrInfo oUserHdrInfo)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_UPD_LastLoggedInInfo");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parLastLoggedIn = cmd.CreateParameter();
            parLastLoggedIn.ParameterName = "@LastLoggedIn";
            parLastLoggedIn.Value = oUserHdrInfo.LastLoggedIn.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            cmdParams.Add(parLastLoggedIn);

            System.Data.IDbDataParameter pkparUserID = cmd.CreateParameter();
            pkparUserID.ParameterName = "@UserID";
            pkparUserID.Value = oUserHdrInfo.UserID;
            cmdParams.Add(pkparUserID);

            return cmd;
        }
        #endregion

        private IDbCommand SearchAccountResultOrganisationalHierarchyBYUserID(int userID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SearchAccountOwnershipAssociation");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdCompanyId = cmd.CreateParameter();
            cmdCompanyId.ParameterName = "@UserID";
            cmdCompanyId.Value = userID;
            cmdParams.Add(cmdCompanyId);


            return cmd;
        }

        #region VerifyPassword
        public bool VerifyPassword(int userID, string oldPassword)
        {
            IDbCommand cmd = null;
            bool bMatch = false;
            try
            {
                cmd = CreateVerifyPasswordCommand(userID, oldPassword);
                cmd.Connection = this.CreateConnection();
                cmd.Connection.Open();
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    if (Convert.ToInt32(result) == 1)
                        bMatch = true;
                }
                return bMatch;
            }
            finally
            {
                if (cmd != null)
                {
                    if (cmd.Connection != null)
                    {
                        if (cmd.Connection.State != ConnectionState.Closed)
                        {
                            cmd.Connection.Close();
                            cmd.Connection.Dispose();
                        }
                    }
                    cmd.Dispose();
                }

            }
        }

        protected System.Data.IDbCommand CreateVerifyPasswordCommand(int userID, string oldPassword)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_GET_VerifyPassword");
            cmd.CommandType = CommandType.StoredProcedure;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserID";
            par.Value = userID;
            cmd.Parameters.Add(par);

            System.Data.IDbDataParameter parPassword = cmd.CreateParameter();
            parPassword.ParameterName = "@Password";
            parPassword.Value = oldPassword;
            cmd.Parameters.Add(parPassword);

            return cmd;
        }


        #endregion


        internal int? GetTotalAccountsCount(int? UserID, short? RoleID, int? RecPeriodID)
        {
            IDbCommand cmd = null;
            int? accountCount = null;
            try
            {
                cmd = CreateGetTotalAccountsCountCommand(UserID, RoleID, RecPeriodID);
                cmd.Connection = this.CreateConnection();
                cmd.Connection.Open();
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    accountCount = Convert.ToInt32(result);
                }
            }
            finally
            {
                if (cmd != null)
                {
                    if (cmd.Connection != null)
                    {
                        if (cmd.Connection.State != ConnectionState.Closed)
                        {
                            cmd.Connection.Close();
                            cmd.Connection.Dispose();
                        }
                    }
                    cmd.Dispose();
                }

            }
            return accountCount;
        }

        private IDbCommand CreateGetTotalAccountsCountCommand(int? UserID, short? RoleID, int? RecPeriodID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_GET_TotalAccountsCount");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserID";
            par.Value = UserID.Value;
            cmdParams.Add(par);

            System.Data.IDbDataParameter parPassword = cmd.CreateParameter();
            parPassword.ParameterName = "@RoleID";
            parPassword.Value = RoleID.Value;
            cmdParams.Add(parPassword);

            IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            parRecPeriodID.Value = RecPeriodID.Value;
            cmdParams.Add(parRecPeriodID);

            return cmd;
        }

        #region SelectUsersByAccountIDsAndRecPeriodIDAndAccountAttributeID

        public List<UserHdrInfo> SelectUsersByAccountIDsAndRecPeriodIDAndAccountAttributeID(DataTable dtAccountIDCollection, DataTable dtNetAccountIDCollection, int recPeriodID, short accountAttributeID, short alertId)
        {
            IDbCommand cmd = null;
            IDbConnection con = null;
            List<UserHdrInfo> oUserHdrInfoCollection = new List<UserHdrInfo>();
            try
            {
                cmd = this.CreateCommand("usp_SEL_UsersByAccountIDsAndRecPeriodIDAndAccountAttributeID");
                cmd.CommandType = CommandType.StoredProcedure;

                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter paramAccountIDTable = cmd.CreateParameter();
                paramAccountIDTable.ParameterName = "@AccountIDTable";
                paramAccountIDTable.Value = dtAccountIDCollection;
                cmdParams.Add(paramAccountIDTable);

                IDbDataParameter paramNetAccountIDTable = cmd.CreateParameter();
                paramNetAccountIDTable.ParameterName = "@NetAccountIDTable";
                paramNetAccountIDTable.Value = dtNetAccountIDCollection;
                cmdParams.Add(paramNetAccountIDTable);

                IDbDataParameter paramRecPeriodID = cmd.CreateParameter();
                paramRecPeriodID.ParameterName = "@RecPeriodID";
                paramRecPeriodID.Value = recPeriodID;
                cmdParams.Add(paramRecPeriodID);

                IDbDataParameter paramAccountAttributeID = cmd.CreateParameter();
                paramAccountAttributeID.ParameterName = "@AccountAttributeID";
                paramAccountAttributeID.Value = accountAttributeID;
                cmdParams.Add(paramAccountAttributeID);

                IDbDataParameter paramAlertID = cmd.CreateParameter();
                paramAlertID.ParameterName = "@AlertID";
                paramAlertID.Value = alertId;
                cmdParams.Add(paramAlertID);

                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                UserHdrInfo oUserHdrInfo = null;
                while (reader.Read())
                {
                    oUserHdrInfo = MapUserHdrInfo(reader);
                    oUserHdrInfo.AlertReplacement = reader.GetStringValue("Replacement");
                    oUserHdrInfoCollection.Add(oUserHdrInfo);
                }
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Dispose();
            }

            return oUserHdrInfoCollection;
        }

        private UserHdrInfo MapUserHdrInfo(IDataReader reader)
        {
            UserHdrInfo oUserHdrInfo = new UserHdrInfo();
            oUserHdrInfo.UserID = reader.GetInt32Value("UserID");
            oUserHdrInfo.EmailID = reader.GetStringValue("EmailID");
            oUserHdrInfo.FirstName = reader.GetStringValue("FirstName");
            oUserHdrInfo.LastName = reader.GetStringValue("LastName");
            oUserHdrInfo.RoleID = reader.GetInt16Value("RoleID");
            oUserHdrInfo.LoginID = reader.GetStringValue("LoginID");
            oUserHdrInfo.DefaultLanguageID = reader.GetInt32Value("DefaultLanguageID");

            return oUserHdrInfo;
        }


        #endregion

        #region SelectAllUsersRolesAndEmailID

        public List<UserHdrInfo> SelectAllUsersRolesAndEmailID(int companyID)
        {
            IDbCommand cmd = null;
            IDbConnection con = null;
            List<UserHdrInfo> oUserHdrInfoCollection = new List<UserHdrInfo>();
            try
            {
                cmd = this.CreateCommand("usp_SEL_AllUserIDRoleIDAndEmailIDByCompanyID");
                cmd.CommandType = CommandType.StoredProcedure;

                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter paramCompanyID = cmd.CreateParameter();
                paramCompanyID.ParameterName = "@CompanyID";
                paramCompanyID.Value = companyID;
                cmdParams.Add(paramCompanyID);

                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                UserHdrInfo oUserHdrInfo = null;
                while (reader.Read())
                {
                    oUserHdrInfo = MapUserHdrInfo(reader);

                    oUserHdrInfoCollection.Add(oUserHdrInfo);
                }
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Dispose();
            }

            return oUserHdrInfoCollection;
        }

        #endregion

        #region SelectUserByUserID

        public List<UserHdrInfo> SelectUserByUserID(DataTable dtUserIDCollection)
        {
            IDbCommand cmd = null;
            IDbConnection con = null;
            List<UserHdrInfo> oUserHdrInfoCollection = new List<UserHdrInfo>();
            try
            {
                cmd = this.CreateCommand("usp_SEL_UserByUserID");
                cmd.CommandType = CommandType.StoredProcedure;

                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter paramUserIDTable = cmd.CreateParameter();
                paramUserIDTable.ParameterName = "@UserIDTable";
                paramUserIDTable.Value = dtUserIDCollection;
                cmdParams.Add(paramUserIDTable);

                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                UserHdrInfo oUserHdrInfo = null;
                while (reader.Read())
                {
                    oUserHdrInfo = oUserHdrInfo = MapUserHdrInfo(reader);

                    oUserHdrInfoCollection.Add(oUserHdrInfo);
                }
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Dispose();
            }

            return oUserHdrInfoCollection;
        }

        #endregion


        #region InsertUserAccount




        internal void InsertUserAccount(DataTable dtAccountID, int userID, short roleID, IDbConnection connection, IDbTransaction transaction)
        {
            IDbCommand cmd = this.CreateInsertUserAccountCommand(dtAccountID, userID, roleID);

            cmd.Connection = connection;
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();

        }

        private IDbCommand CreateInsertUserAccountCommand(DataTable dtAccountID, int userID, short roleID)
        {
            IDbCommand cmd = this.CreateCommand("usp_INS_UserAccount");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter paramAccountIDTable = cmd.CreateParameter();
            paramAccountIDTable.ParameterName = "@AccountIDTable";
            paramAccountIDTable.Value = dtAccountID;
            cmdParams.Add(paramAccountIDTable);

            IDbDataParameter paramUserID = cmd.CreateParameter();
            paramUserID.ParameterName = "@UserID";
            paramUserID.Value = userID;
            cmdParams.Add(paramUserID);

            IDbDataParameter paramRoleID = cmd.CreateParameter();
            paramRoleID.ParameterName = "@RoleID";
            paramRoleID.Value = roleID;
            cmdParams.Add(paramRoleID);

            return cmd;
        }

        #endregion
        #region Save User Account By User Role

        internal void SaveUserAssociationByUserRole(DataTable dtUserRole, int userID, short roleID, IDbConnection connection, IDbTransaction transaction)
        {
            IDbCommand cmd = this.CreateSaveUserAssociationByUserRoleCommand(dtUserRole, userID, roleID);

            cmd.Connection = connection;
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();

        }
        private IDbCommand CreateSaveUserAssociationByUserRoleCommand(DataTable dtUserRole, int userID, short roleID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SAV_UserAssociationByUserRole");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter paramAccountIDTable = cmd.CreateParameter();
            paramAccountIDTable.ParameterName = "@udtUserAssociationByUserRole";
            paramAccountIDTable.Value = dtUserRole;
            cmdParams.Add(paramAccountIDTable);

            IDbDataParameter paramUserID = cmd.CreateParameter();
            paramUserID.ParameterName = "@UserID";
            paramUserID.Value = userID;
            cmdParams.Add(paramUserID);

            IDbDataParameter paramRoleID = cmd.CreateParameter();
            paramRoleID.ParameterName = "@RoleID";
            paramRoleID.Value = roleID;
            cmdParams.Add(paramRoleID);

            return cmd;
        }
        #endregion

        #region Save User Account All Accounts

        internal void SaveUserAssociationAllAccounts(bool isAllAccounts, int userID, short roleID, IDbConnection connection, IDbTransaction transaction)
        {
            IDbCommand cmd = this.CreateSaveUserAssociationAllAccountsCommand(isAllAccounts, userID, roleID);

            cmd.Connection = connection;
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();

        }
        private IDbCommand CreateSaveUserAssociationAllAccountsCommand(bool isAllAccounts, int userID, short roleID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SAV_UserAssociationAllAccounts");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter paramIsAllAccounts = cmd.CreateParameter();
            paramIsAllAccounts.ParameterName = "@IsAllAccounts";
            paramIsAllAccounts.Value = isAllAccounts;
            cmdParams.Add(paramIsAllAccounts);

            IDbDataParameter paramUserID = cmd.CreateParameter();
            paramUserID.ParameterName = "@UserID";
            paramUserID.Value = userID;
            cmdParams.Add(paramUserID);

            IDbDataParameter paramRoleID = cmd.CreateParameter();
            paramRoleID.ParameterName = "@RoleID";
            paramRoleID.Value = roleID;
            cmdParams.Add(paramRoleID);

            return cmd;
        }
        #endregion
        public List<UserHdrInfo> SelectPRAByGLDataID(long? glDataID)
        {
            List<UserHdrInfo> oUserHdrInfoCollection = new List<UserHdrInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader reader = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateSelectPRAByGLDataIDCommand(glDataID);
                cmd.Connection = con;
                con.Open();
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    UserHdrInfo oUserHdrInfo = MapUserHdrInfo(reader);
                    oUserHdrInfoCollection.Add(oUserHdrInfo);
                }
                reader.Close();
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                if ((con != null) && (con.State == ConnectionState.Open))
                    con.Close();
            }
            return oUserHdrInfoCollection;
        }

        private IDbCommand CreateSelectPRAByGLDataIDCommand(long? glDataID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_PRAbyGLDataID");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            parGLDataID.Value = glDataID;
            cmdParams.Add(parGLDataID);
            return cmd;
        }


        #region WriteOffOnApprover

        public List<UserHdrInfo> SelectWriteOffOnApproversByCompanyID(int? companyID)
        {
            List<UserHdrInfo> oUserHdrInfoCollection = new List<UserHdrInfo>();

            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader reader = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateSelectWriteOffOnApproversByCompanyIDCommand(companyID);
                cmd.Connection = con;
                con.Open();
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    UserHdrInfo oUserHdrInfo = MapUserHdrInfo(reader);
                    oUserHdrInfoCollection.Add(oUserHdrInfo);
                }
                reader.Close();
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                if ((con != null) && (con.State == ConnectionState.Open))
                    con.Close();
            }
            return oUserHdrInfoCollection;

        }

        private IDbCommand CreateSelectWriteOffOnApproversByCompanyIDCommand(int? companyID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_JEApprover");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (companyID != null)
                parCompanyID.Value = companyID;
            else
                parCompanyID.Value = DBNull.Value;
            cmdParams.Add(parCompanyID);

            return cmd;
        }

        #endregion

        internal void DeleteUserAccount(DataTable dtUserAccount, IDbConnection connection, IDbTransaction transaction)
        {
            IDbCommand cmd = this.CreateDeleteUserAccountCommand(dtUserAccount);

            cmd.Connection = connection;
            cmd.Transaction = transaction;
            cmd.ExecuteNonQuery();

        }

        private IDbCommand CreateDeleteUserAccountCommand(DataTable dtAccountID)
        {
            IDbCommand cmd = this.CreateCommand("usp_DEL_UserAccountAssociation");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter paramAccountIDTable = cmd.CreateParameter();
            paramAccountIDTable.ParameterName = "@UserAccountTable";
            paramAccountIDTable.Value = dtAccountID;
            cmdParams.Add(paramAccountIDTable);

            return cmd;
        }

        public void SaveUserDashboardPreferences(DataTable dtDashboardPreferences, int? UserID, int? RoleID, IDbConnection con, IDbTransaction oTransaction)
        {
            IDbCommand cmd = null;
            bool isConnectionNull = (con == null);
            cmd = this.CreateSaveUserDashboardPreferencesCommand(dtDashboardPreferences, UserID, RoleID);
            cmd.Transaction = oTransaction;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();

        }

        private IDbCommand CreateSaveUserDashboardPreferencesCommand(DataTable dtDashboardPreferences, int? UserID, int? RoleID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SAV_UserDashBordPreferences");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parTBLDashBoardPreferences = cmd.CreateParameter();
            parTBLDashBoardPreferences.ParameterName = "@TBLDashBoardPreferences";
            parTBLDashBoardPreferences.Value = dtDashboardPreferences;
            cmdParams.Add(parTBLDashBoardPreferences);

            System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            parRoleID.Value = RoleID.Value;
            cmdParams.Add(parRoleID);

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            parUserID.Value = UserID.Value;
            cmdParams.Add(parUserID);
            return cmd;
        }

        public List<DashboardMstInfo> GetUserDashboardPreferences(int? UserID, int? RoleID)
        {
            List<DashboardMstInfo> oDashboardMstInfoCollection = new List<DashboardMstInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader reader = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateGetUserDashboardPreferencesCommand(UserID, RoleID);
                cmd.Connection = con;
                con.Open();
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    DashboardMstInfo oDashboardMstInfo = MapDashboardMstInfo(reader);
                    oDashboardMstInfoCollection.Add(oDashboardMstInfo);
                }
                reader.Close();
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                if ((con != null) && (con.State == ConnectionState.Open))
                    con.Close();
            }
            return oDashboardMstInfoCollection;
        }

        private IDbCommand CreateGetUserDashboardPreferencesCommand(int? UserID, int? RoleID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_UserDashboardPreferences");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            parUserID.Value = UserID;
            cmdParams.Add(parUserID);

            IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            parRoleID.Value = RoleID;
            cmdParams.Add(parRoleID);

            return cmd;
        }

        private DashboardMstInfo MapDashboardMstInfo(IDataReader reader)
        {
            DashboardMstInfo oDashboardMstInfo = new DashboardMstInfo();
            oDashboardMstInfo.DashboardID = reader.GetInt16Value("DashboardID");
            oDashboardMstInfo.IsActive = reader.GetBooleanValue("IsActive");
            return oDashboardMstInfo;
        }

        public UserHdrInfo GetLastUserForReviewNote(long glDataID, int companyID, long? reviewNoteID, short currentUserRoleID, int currentUserID)
        {
            UserHdrInfo objUserHdrInfo = null;
            IDbConnection conn = null;

            try
            {
                conn = CreateConnection();
                conn.Open();

                IDbCommand cmd;
                cmd = CreateLastUserForReviewNoteCommand(glDataID, companyID, reviewNoteID, currentUserRoleID, currentUserID);
                cmd.Connection = conn;

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    objUserHdrInfo = (UserHdrInfo)MapObject(reader);
                }
                reader.ClearColumnHash();
                reader.Close();

            }
            finally
            {
                try
                {
                    if (conn != null && conn.State != ConnectionState.Closed)
                        conn.Close();
                }
                catch (Exception)
                {
                }
            }
            return objUserHdrInfo;

        }

        private IDbCommand CreateLastUserForReviewNoteCommand(long glDataID, int companyID, long? reviewNoteID, short currentUserRoleID, int currentUserID)
        {
            IDbCommand cmd = this.CreateCommand("[dbo].[usp_GET_LastUserForReviewNote]");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            parGLDataID.Value = glDataID;

            IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = companyID;

            IDbDataParameter parReviewNoteID = cmd.CreateParameter();
            parReviewNoteID.ParameterName = "@ReviewNoteID";
            if (reviewNoteID.HasValue)
                parReviewNoteID.Value = reviewNoteID;
            else
                parReviewNoteID.Value = DBNull.Value;

            IDbDataParameter parCurrentUserRoleID = cmd.CreateParameter();
            parCurrentUserRoleID.ParameterName = "@CurrentUserRoleID";
            parCurrentUserRoleID.Value = currentUserRoleID;

            IDbDataParameter parCurrentUserID = cmd.CreateParameter();
            parCurrentUserID.ParameterName = "@CurrentUserID";
            parCurrentUserID.Value = currentUserID;

            cmdParams.Add(parGLDataID);
            cmdParams.Add(parCompanyID);
            cmdParams.Add(parReviewNoteID);
            cmdParams.Add(parCurrentUserRoleID);
            cmdParams.Add(parCurrentUserID);

            return cmd;
        }

        public Int32? InsertUserTransit(UserHdrInfo oUserHdrInfo)
        {
            Int32? userTransitID = null;
            using (IDbConnection cnn = this.CreateConnection())
            {
                cnn.Open();
                using (IDbCommand cmd = CreateInsertUserTransitCommand(oUserHdrInfo))
                {
                    cmd.Connection = cnn;
                    userTransitID = Convert.ToInt32(cmd.ExecuteScalar());
                    oUserHdrInfo.UserTransitID = userTransitID;
                }
                cnn.Close();
            }
            return userTransitID;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in UserHdrInfo object
        /// </summary>
        /// <param name="o">A UserHdrInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected IDbCommand CreateInsertUserTransitCommand(UserHdrInfo entity)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_UserTransit");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (!entity.IsCompanyIDNull)
                parCompanyID.Value = entity.CompanyID;
            else
                parCompanyID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (!entity.IsDateAddedNull)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (!entity.IsDateRevisedNull)
                parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parDefaultRoleID = cmd.CreateParameter();
            parDefaultRoleID.ParameterName = "@DefaultRoleID";
            if (!entity.IsDefaultRoleIDNull)
                parDefaultRoleID.Value = entity.DefaultRoleID;
            else
                parDefaultRoleID.Value = System.DBNull.Value;
            cmdParams.Add(parDefaultRoleID);

            System.Data.IDbDataParameter parDefaultLanguageID = cmd.CreateParameter();
            parDefaultLanguageID.ParameterName = "@DefaultLanguageID";
            if (entity.DefaultLanguageID.HasValue)
                parDefaultLanguageID.Value = entity.DefaultLanguageID;
            else
                parDefaultLanguageID.Value = System.DBNull.Value;
            cmdParams.Add(parDefaultLanguageID);

            System.Data.IDbDataParameter parEmailID = cmd.CreateParameter();
            parEmailID.ParameterName = "@EmailID";
            if (!entity.IsEmailIDNull)
                parEmailID.Value = entity.EmailID;
            else
                parEmailID.Value = System.DBNull.Value;
            cmdParams.Add(parEmailID);

            System.Data.IDbDataParameter parFirstName = cmd.CreateParameter();
            parFirstName.ParameterName = "@FirstName";
            if (!entity.IsFirstNameNull)
                parFirstName.Value = entity.FirstName;
            else
                parFirstName.Value = System.DBNull.Value;
            cmdParams.Add(parFirstName);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (!entity.IsIsActiveNull)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parIsNew = cmd.CreateParameter();
            parIsNew.ParameterName = "@IsNew";
            if (!entity.IsIsNewNull)
                parIsNew.Value = entity.IsNew;
            else
                parIsNew.Value = System.DBNull.Value;
            cmdParams.Add(parIsNew);

            System.Data.IDbDataParameter parJobTitle = cmd.CreateParameter();
            parJobTitle.ParameterName = "@JobTitle";
            if (!entity.IsJobTitleNull)
                parJobTitle.Value = entity.JobTitle;
            else
                parJobTitle.Value = System.DBNull.Value;
            cmdParams.Add(parJobTitle);

            System.Data.IDbDataParameter parLastLoggedIn = cmd.CreateParameter();
            parLastLoggedIn.ParameterName = "@LastLoggedIn";
            if (!entity.IsLastLoggedInNull)
                parLastLoggedIn.Value = entity.LastLoggedIn.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parLastLoggedIn.Value = System.DBNull.Value;
            cmdParams.Add(parLastLoggedIn);

            System.Data.IDbDataParameter parLastName = cmd.CreateParameter();
            parLastName.ParameterName = "@LastName";
            if (!entity.IsLastNameNull)
                parLastName.Value = entity.LastName;
            else
                parLastName.Value = System.DBNull.Value;
            cmdParams.Add(parLastName);

            System.Data.IDbDataParameter parLoginID = cmd.CreateParameter();
            parLoginID.ParameterName = "@LoginID";
            if (!entity.IsLoginIDNull)
                parLoginID.Value = entity.LoginID;
            else
                parLoginID.Value = System.DBNull.Value;
            cmdParams.Add(parLoginID);

            System.Data.IDbDataParameter parGeneratedPassword = cmd.CreateParameter();
            parGeneratedPassword.ParameterName = "@GeneratedPassword";
            if (!string.IsNullOrEmpty(entity.GeneratedPassword))
                parGeneratedPassword.Value = entity.GeneratedPassword;
            else
                parGeneratedPassword.Value = System.DBNull.Value;
            cmdParams.Add(parGeneratedPassword);

            System.Data.IDbDataParameter parPassword = cmd.CreateParameter();
            parPassword.ParameterName = "@Password";
            if (!entity.IsPasswordNull)
                parPassword.Value = entity.Password;
            else
                parPassword.Value = System.DBNull.Value;
            cmdParams.Add(parPassword);

            System.Data.IDbDataParameter parPhone = cmd.CreateParameter();
            parPhone.ParameterName = "@Phone";
            if (!entity.IsPhoneNull)
                parPhone.Value = entity.Phone;
            else
                parPhone.Value = System.DBNull.Value;
            cmdParams.Add(parPhone);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parWorkPhone = cmd.CreateParameter();
            parWorkPhone.ParameterName = "@WorkPhone";
            if (!entity.IsWorkPhoneNull)
                parWorkPhone.Value = entity.WorkPhone;
            else
                parWorkPhone.Value = System.DBNull.Value;
            cmdParams.Add(parWorkPhone);

            return cmd;
        }

        public List<UserHdrInfo> SelectUsersFromTransit(int? CompanyID)
        {
            List<UserHdrInfo> oUserHdrInfoList = new List<UserHdrInfo>();
            UserHdrInfo oUserHdrInfo = null;
            UserRoleInfo oUserRoleInfo = null;
            using (IDbConnection cnn = this.CreateConnection())
            {
                cnn.Open();
                using (IDbCommand cmd = CreateCommandSelectUsersFromTransit(CompanyID))
                {
                    cmd.Connection = cnn;
                    IDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (dr.Read())
                    {
                        oUserRoleInfo = MapUserRoleObject(dr);
                        if (oUserHdrInfo == null || oUserHdrInfo.UserTransitID != oUserRoleInfo.UserTransitID)
                        {
                            oUserHdrInfo = MapObject(dr);
                            oUserHdrInfo.UserRoleInfoList = new List<UserRoleInfo>();
                            oUserHdrInfoList.Add(oUserHdrInfo);
                        }
                        oUserHdrInfo.UserRoleInfoList.Add(oUserRoleInfo);
                    }
                    dr.ClearColumnHash();
                }
                cnn.Close();
            }
            return oUserHdrInfoList;
        }

        protected IDbCommand CreateCommandSelectUsersFromTransit(int? companyID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_UserTransit");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = companyID;
            cmdParams.Add(parCompanyID);
            return cmd;
        }

        protected override UserHdrInfo MapObject(IDataReader r)
        {
            UserHdrInfo oUserHdrInfo = base.MapObject(r);
            oUserHdrInfo.UserTransitID = r.GetInt32Value("UserTransitID");
            oUserHdrInfo.GeneratedPassword = r.GetStringValue("GeneratedPassword");
            oUserHdrInfo.ExcelRowNumber = r.GetInt32Value("ExcelRowNumber");
            oUserHdrInfo.IsLoginIDUnique = r.GetBooleanValue("IsLoginIDUnique");
            oUserHdrInfo.IsFTPLoginIDUnique = r.GetBooleanValue("IsFTPLoginIDUnique");
            oUserHdrInfo.IsEmailIDUnique = r.GetBooleanValue("IsEmailIDUnique");
            oUserHdrInfo.IsPasswordResetRequired = r.GetBooleanValue("IsPasswordResetRequired");
            oUserHdrInfo.PasswordResetDays = r.GetInt16Value("PasswordResetDays");
            oUserHdrInfo.DefaultLanguageID = r.GetInt32Value("DefaultLanguageID");
            oUserHdrInfo.IsFTPPasswordResetRequired = r.GetInt32Value("IsFTPPasswordResetRequired");
            oUserHdrInfo.CurrentFTPActivationStatusID = r.GetInt16Value("CurrentFTPActivationStatusID");
            oUserHdrInfo.FTPActivationStatusID = r.GetInt16Value("FTPActivationStatusID");
            oUserHdrInfo.FTPActivationStatusLabelID = r.GetInt32Value("FTPActivationStatusLabelID");
            oUserHdrInfo.FTPActivationDate = r.GetDateValue("FTPActivationDate");
            oUserHdrInfo.IsUserLocked = r.GetBooleanValue("IsUserLocked");
            oUserHdrInfo.LockdownCount = r.GetInt32Value("LockdownCount");
            oUserHdrInfo.LockdownDateTime = r.GetDateValue("LockdownDateTime");
            oUserHdrInfo.CompanyDisplayName = r.GetStringValue("CompanyDisplayName");
            oUserHdrInfo.FTPLoginID = r.GetStringValue("FTPLoginID");
            oUserHdrInfo.ChildUserID = r.GetInt32Value("ChildUserID");
            oUserHdrInfo.ChildRoleID = r.GetInt16Value("ChildROleID");
            return oUserHdrInfo;
        }

        private UserRoleInfo MapUserRoleObject(IDataReader r)
        {
            UserRoleInfo oUserRoleInfo = new UserRoleInfo();
            oUserRoleInfo.UserTransitID = r.GetInt32Value("UserTransitID");
            oUserRoleInfo.RoleID = r.GetInt16Value("RoleID");
            oUserRoleInfo.IsPotentialRole = r.GetBooleanValue("IsPotentialRole");
            return oUserRoleInfo;
        }

        public void DeleteUsersFromTransit(int? CompanyID)
        {
            using (IDbConnection cnn = this.CreateConnection())
            {
                cnn.Open();
                using (IDbCommand cmd = CreateCommandDeleteUsersFromTransit(CompanyID))
                {
                    cmd.Connection = cnn;
                    cmd.ExecuteNonQuery();
                }
                cnn.Close();
            }
        }

        protected IDbCommand CreateCommandDeleteUsersFromTransit(int? companyID)
        {
            IDbCommand cmd = this.CreateCommand("usp_DEL_UserTransit");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = companyID;
            cmdParams.Add(parCompanyID);
            return cmd;
        }

        public List<UserHdrInfo> CheckUsersForUniqueness(List<UserHdrInfo> oUserHdrInfoList, int? companyID, bool tryGetUserID)
        {
            List<UserHdrInfo> oUserHdrInfoListLocal = null;
            using (IDbConnection cnn = this.CreateConnection())
            {
                cnn.Open();
                using (IDbCommand cmd = CreateCommandCheckUsersForUniqueness(oUserHdrInfoList, companyID, tryGetUserID))
                {
                    cmd.Connection = cnn;
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (oUserHdrInfoListLocal == null)
                            oUserHdrInfoListLocal = new List<UserHdrInfo>();
                        oUserHdrInfoListLocal.Add(MapObject(dr));
                    }
                    dr.ClearColumnHash();
                }
                cnn.Close();
            }
            return oUserHdrInfoListLocal;
        }

        protected IDbCommand CreateCommandCheckUsersForUniqueness(List<UserHdrInfo> oUserHdrInfoList, int? companyID, bool tryGetUserID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SVC_CHK_ValidateUserListForUniqueness");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = companyID;
            cmdParams.Add(parCompanyID);

            IDbDataParameter parUserList = cmd.CreateParameter();
            parUserList.ParameterName = "@xmlUserList";
            parUserList.Value = ServiceHelper.ConvertUserHdrInfoListToXML(oUserHdrInfoList);
            cmdParams.Add(parUserList);

            IDbDataParameter parTryGetUserID = cmd.CreateParameter();
            parTryGetUserID.ParameterName = "@TryGetUserID";
            parTryGetUserID.Value = tryGetUserID;
            cmdParams.Add(parTryGetUserID);

            return cmd;
        }
        public List<UserHdrInfo> SelectActiveUserHdrInfoByCompanyRoleAndPrefixText(int companyId, short roleID, string prefixText, int count)
        {
            List<UserHdrInfo> oUserHdrInfoList = new List<UserHdrInfo>();
            UserHdrInfo oUserHdrInfo;
            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateSelectActiveUserHdrInfoByCompanyRoleAndPrefixTextCommand(companyId, roleID, prefixText, count);
                cmd.Connection = con;
                con.Open();
                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    oUserHdrInfo = new UserHdrInfo();
                    //oUserHdrInfo = MapObject(reader);
                    oUserHdrInfo.UserID = reader.GetInt32Value("UserID");
                    oUserHdrInfo.Name = reader.GetStringValue("Name");
                    oUserHdrInfoList.Add(oUserHdrInfo);
                }
                reader.ClearColumnHash();
                reader.Close();

            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                    con.Close();
            }

            return oUserHdrInfoList;
        }
        private IDbCommand CreateSelectActiveUserHdrInfoByCompanyRoleAndPrefixTextCommand(int companyId, short roleID, string prefixText, int count)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_ActiveUserByCompanyIDRoleIDAndPrefixText");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter parCompanyID = cmd.CreateParameter();
            IDbDataParameter parRoleID = cmd.CreateParameter();
            IDbDataParameter parPrefixText = cmd.CreateParameter();
            IDbDataParameter parCount = cmd.CreateParameter();

            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = companyId;

            parRoleID.ParameterName = "@RoleID";
            parRoleID.Value = roleID;

            parPrefixText.ParameterName = "@PrefixText";
            parPrefixText.Value = prefixText;

            parCount.ParameterName = "@Count";
            parCount.Value = count;

            cmdParams.Add(parCompanyID);
            cmdParams.Add(parRoleID);
            cmdParams.Add(parPrefixText);
            cmdParams.Add(parCount);

            return cmd;
        }

        public List<UserFTPConfigurationInfo> GetUserFTPConfiguration(int? UserID)
        {
            List<UserFTPConfigurationInfo> oUserFTPConfigurationInfoList = new List<UserFTPConfigurationInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateGetUserFTPConfigurationCommand(UserID);
                cmd.Connection = con;
                con.Open();
                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    UserFTPConfigurationInfo oUserFTPConfigurationInfo = this.MapoUserFTPConfigurationInfoObject(reader);
                    oUserFTPConfigurationInfoList.Add(oUserFTPConfigurationInfo);
                }
                reader.Close();
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                    con.Close();
            }

            return oUserFTPConfigurationInfoList;
        }
        private IDbCommand CreateGetUserFTPConfigurationCommand(int? UserID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_UserFTPConfiguration");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            if (UserID.HasValue)
                parUserID.Value = UserID.Value;
            else
                parUserID.Value = DBNull.Value;
            cmdParams.Add(parUserID);
            return cmd;
        }
        protected UserFTPConfigurationInfo MapoUserFTPConfigurationInfoObject(IDataReader r)
        {
            UserFTPConfigurationInfo oUserFTPConfigurationInfo = new UserFTPConfigurationInfo();
            oUserFTPConfigurationInfo.UserID = r.GetInt32Value("UserID");
            oUserFTPConfigurationInfo.FirstName = r.GetStringValue("FirstName");
            oUserFTPConfigurationInfo.LastName = r.GetStringValue("LastName");
            oUserFTPConfigurationInfo.LoginID = r.GetStringValue("LoginID");
            oUserFTPConfigurationInfo.FTPLoginID = r.GetStringValue("FTPLoginID");
            oUserFTPConfigurationInfo.EmailID = r.GetStringValue("EmailID");
            oUserFTPConfigurationInfo.CompanyID = r.GetInt32Value("CompanyID");
            oUserFTPConfigurationInfo.DefaultLanguageID = r.GetInt32Value("DefaultLanguageID");
            oUserFTPConfigurationInfo.FTPServerId = r.GetInt16Value("FTPServerId");
            oUserFTPConfigurationInfo.FTPActivationStatusId = r.GetInt16Value("FTPActivationStatusId");
            oUserFTPConfigurationInfo.DataImportTypeID = r.GetInt16Value("DataImportTypeID");
            oUserFTPConfigurationInfo.ImportTemplateID = r.GetInt32Value("ImportTemplateID");
            oUserFTPConfigurationInfo.FTPUploadRoleID = r.GetInt16Value("RoleID");
            oUserFTPConfigurationInfo.ProfileName = r.GetStringValue("ProfileName");
            oUserFTPConfigurationInfo.UserFTPConfigurationID = r.GetInt32Value("UserFTPConfigurationID");
            oUserFTPConfigurationInfo.IsFTPEnabled = r.GetBooleanValue("IsFTPEnabled");
            return oUserFTPConfigurationInfo;
        }

        internal bool VerifyFTPPassword(int UserID, string OldPasswordHash)
        {
            IDbCommand cmd = null;
            bool bMatch = false;
            try
            {
                cmd = CreateVerifyFTPPasswordCommand(UserID, OldPasswordHash);
                cmd.Connection = this.CreateConnection();
                cmd.Connection.Open();
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    if (Convert.ToInt32(result) == 1)
                        bMatch = true;
                }
                return bMatch;
            }
            finally
            {
                if (cmd != null)
                {
                    if (cmd.Connection != null)
                    {
                        if (cmd.Connection.State != ConnectionState.Closed)
                        {
                            cmd.Connection.Close();
                            cmd.Connection.Dispose();
                        }
                    }
                    cmd.Dispose();
                }

            }
        }
        protected System.Data.IDbCommand CreateVerifyFTPPasswordCommand(int userID, string oldPassword)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_GET_VerifyFTPPassword");
            cmd.CommandType = CommandType.StoredProcedure;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserID";
            par.Value = userID;
            cmd.Parameters.Add(par);

            System.Data.IDbDataParameter parPassword = cmd.CreateParameter();
            parPassword.ParameterName = "@Password";
            parPassword.Value = oldPassword;
            cmd.Parameters.Add(parPassword);

            return cmd;
        }

        internal int UpdateFTPPassword(string LoginID, string ftpLoginID, string NewPasswordHash)
        {
            IDbConnection conn = null;
            try
            {
                conn = CreateConnection();
                conn.Open();
                IDbCommand cmd;
                cmd = CreateUpdateFTPPasswordCommand(LoginID, ftpLoginID, NewPasswordHash);
                cmd.Connection = conn;
                Object oReturnObject = cmd.ExecuteScalar();
                return (oReturnObject == null) ? 0 : (int)oReturnObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    if (conn != null)
                        conn.Close();
                }
                catch (Exception)
                {
                }
            }
        }
        public int SaveUserFTPConfiguration(UserFTPConfigurationInfo oUserFTPConfigurationInfo, DataTable dt, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            int result = 0;
            using (IDbCommand cmd = CreateSaveUserFTPConfigurationCommand(oUserFTPConfigurationInfo, dt))
            {
                cmd.Connection = oConnection;
                cmd.Transaction = oTransaction;
                result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return result;
        }

        private IDbCommand CreateSaveUserFTPConfigurationCommand(UserFTPConfigurationInfo oUserFTPConfigurationInfo, DataTable dt)
        {
            IDbCommand cmd = CreateCommand("usp_SAVE_UserFTPConfiguration");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parUserFTPConfigurationTbl = cmd.CreateParameter();
            parUserFTPConfigurationTbl.ParameterName = "@dtUserFTPConfiguration";
            parUserFTPConfigurationTbl.Value = dt;
            cmdParams.Add(parUserFTPConfigurationTbl);

            System.Data.IDbDataParameter parModifyBy = cmd.CreateParameter();
            parModifyBy.ParameterName = "@ModifyBy";
            if (!string.IsNullOrEmpty(oUserFTPConfigurationInfo.ModifyBy))
                parModifyBy.Value = oUserFTPConfigurationInfo.ModifyBy;
            else
                parModifyBy.Value = DBNull.Value;
            cmdParams.Add(parModifyBy);

            System.Data.IDbDataParameter parDateModified = cmd.CreateParameter();
            parDateModified.ParameterName = "@DateModified";
            if (oUserFTPConfigurationInfo.DateModified.HasValue)
                parDateModified.Value = oUserFTPConfigurationInfo.DateModified.Value;
            cmdParams.Add(parDateModified);
            return cmd;
        }

        public bool? IsUserLocked(string loginID)
        {
            bool? isUserLocked = true;
            using (IDbConnection oConnection = CreateConnection())
            {
                oConnection.Open();
                using (IDbCommand cmd = CreateIsUserLockedCommand(loginID))
                {
                    cmd.Connection = oConnection;
                    IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (reader.Read())
                    {
                        isUserLocked = reader.GetBooleanValue("IsUserLocked");
                    }
                }
            }
            return isUserLocked;
        }
        private IDbCommand CreateIsUserLockedCommand(string loginID)
        {
            IDbCommand cmd = CreateCommand("usp_GET_IsUserLocked");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parLoginID = cmd.CreateParameter();
            parLoginID.ParameterName = "@LoginID";
            if (!string.IsNullOrEmpty(loginID))
                parLoginID.Value = loginID;
            else
                parLoginID.Value = DBNull.Value;
            cmdParams.Add(parLoginID);
            return cmd;
        }

        public UserHdrInfo UpdateFailedLoginAttempts(string loginID, bool isResetLoginAttempt)
        {
            UserHdrInfo oUserHdrInfo = null;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                using (IDbConnection oConnection = CreateConnection())
                {
                    oConnection.Open();
                    using (IDbCommand cmd = CreateUpdateFailedLoginAttemptsCommand(loginID, isResetLoginAttempt))
                    {
                        cmd.Connection = oConnection;
                        IDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        if (dr.Read())
                        {
                            oUserHdrInfo = MapObject(dr);
                        }
                        dr.ClearColumnHash();
                    }
                }
                scope.Complete();
            }
            return oUserHdrInfo;
        }
        private IDbCommand CreateUpdateFailedLoginAttemptsCommand(string loginID, bool isResetLoginAttempt)
        {
            IDbCommand cmd = CreateCommand("usp_UPD_FailedLoginAttempts");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parLoginID = cmd.CreateParameter();
            parLoginID.ParameterName = "@LoginID";
            if (!string.IsNullOrEmpty(loginID))
                parLoginID.Value = loginID;
            else
                parLoginID.Value = DBNull.Value;
            cmdParams.Add(parLoginID);

            System.Data.IDbDataParameter parIsResetLoginAttempt = cmd.CreateParameter();
            parIsResetLoginAttempt.ParameterName = "@IsResetLoginAttempt";
            parIsResetLoginAttempt.Value = isResetLoginAttempt;
            cmdParams.Add(parIsResetLoginAttempt);
            return cmd;
        }

        internal void SaveAutoSaveAttributeValues(List<AutoSaveAttributeValueInfo> oAutoSaveAttributeValueInfoList, string revisedBy, DateTime? dateRevised)
        {
            using (IDbConnection cnn = this.CreateConnection())
            {
                cnn.Open();
                using (IDbCommand cmd = CreateSaveAutoSaveAttributeValuesCommand(oAutoSaveAttributeValueInfoList, revisedBy, dateRevised))
                {
                    cmd.Connection = cnn;
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private IDbCommand CreateSaveAutoSaveAttributeValuesCommand(List<AutoSaveAttributeValueInfo> oAutoSaveAttributeValueInfoList, string revisedBy, DateTime? dateRevised)
        {
            IDbCommand cmd = CreateCommand("usp_SAV_AutoSaveAttributeValue");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parAutoSaveAttributeTBL = cmd.CreateParameter();
            parAutoSaveAttributeTBL.ParameterName = "@udtAutoSaveAttributeValue";
            parAutoSaveAttributeTBL.Value = ServiceHelper.ConvertAutoSaveAttributeValueInfoToDataTable(oAutoSaveAttributeValueInfoList);
            cmdParams.Add(parAutoSaveAttributeTBL);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!string.IsNullOrEmpty(revisedBy))
                parRevisedBy.Value = revisedBy;
            else
                parRevisedBy.Value = DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (dateRevised.HasValue && dateRevised != DateTime.MinValue)
                parDateRevised.Value = dateRevised;
            else
                parDateRevised.Value = DBNull.Value;
            cmdParams.Add(parDateRevised);
            return cmd;
        }

        internal List<UserHdrInfo> SelectUserAssociationByUserRole(int? userID, short? roleID)
        {
            List<UserHdrInfo> oUserHdrInfoList = new List<UserHdrInfo>();
            using (IDbConnection cnn = this.CreateConnection())
            {
                cnn.Open();
                using (IDbCommand cmd = CreateSelectUserAssociationByUserRoleCommand(userID, roleID))
                {
                    cmd.Connection = cnn;
                    IDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (dr.Read())
                    {
                        oUserHdrInfoList.Add(MapObject(dr));
                    }
                    dr.ClearColumnHash();
                }
            }
            return oUserHdrInfoList;
        }
        private IDbCommand CreateSelectUserAssociationByUserRoleCommand(int? userID, short? roleID)
        {
            IDbCommand cmd = CreateCommand("usp_SEL_UserAssociationByUserRole");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            if (userID.HasValue)
                parUserID.Value = userID;
            else
                parUserID.Value = DBNull.Value;
            cmdParams.Add(parUserID);

            System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            if (roleID.HasValue)
                parRoleID.Value = roleID;
            else
                parRoleID.Value = DBNull.Value;
            cmdParams.Add(parRoleID);
            return cmd;
        }

        
        internal bool SelectUserAssociationAllAccount(int? userID, short? roleID)
        {
            bool result = false;
            using (IDbConnection cnn = this.CreateConnection())
            {
                cnn.Open();
                using (IDbCommand cmd = CreateSelectUserAssociationAllAccountCommand(userID, roleID))
                {
                    cmd.Connection = cnn;
                    result = (bool) cmd.ExecuteScalar();
                }
                cnn.Close();
            }
            return result;
        }
        private IDbCommand CreateSelectUserAssociationAllAccountCommand(int? userID, short? roleID)
        {
            IDbCommand cmd = CreateCommand("usp_SEL_UserAllAccounts");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            if (userID.HasValue)
                parUserID.Value = userID;
            else
                parUserID.Value = DBNull.Value;
            cmdParams.Add(parUserID);

            System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            if (roleID.HasValue)
                parRoleID.Value = roleID;
            else
                parRoleID.Value = DBNull.Value;
            cmdParams.Add(parRoleID);
            return cmd;
        }

        internal List<AutoSaveAttributeValueInfo> GetAutoSaveAttributeValues(int? userID, short? roleID)
        {
            List<AutoSaveAttributeValueInfo> oAutoSaveAttributeValueInfoList = new List<AutoSaveAttributeValueInfo>();
            using (IDbConnection cnn = this.CreateConnection())
            {
                cnn.Open();
                using (IDbCommand cmd = CreateGetAutoSaveAttributeValuesCommand(userID, roleID))
                {
                    cmd.Connection = cnn;
                    IDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (dr.Read())
                    {
                        oAutoSaveAttributeValueInfoList.Add(MapAutoSaveAttributeValue(dr));
                    }
                    dr.ClearColumnHash();
                }
            }
            return oAutoSaveAttributeValueInfoList;
        }
        private IDbCommand CreateGetAutoSaveAttributeValuesCommand(int? userID, short? roleID)
        {
            IDbCommand cmd = CreateCommand("usp_SEL_AutoSaveAttributeValue");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            if (userID.HasValue)
                parUserID.Value = userID;
            else
                parUserID.Value = DBNull.Value;
            cmdParams.Add(parUserID);

            System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            if (roleID.HasValue)
                parRoleID.Value = roleID;
            else
                parRoleID.Value = DBNull.Value;
            cmdParams.Add(parRoleID);
            return cmd;
        }

        private AutoSaveAttributeValueInfo MapAutoSaveAttributeValue(IDataReader dr)
        {
            AutoSaveAttributeValueInfo entity = new AutoSaveAttributeValueInfo();
            entity.AutoSaveAttributeValueID = dr.GetInt64Value("AutoSaveAttributeValueID");
            entity.AutoSaveAttributeID = dr.GetInt32Value("AutoSaveAttributeID");
            entity.UserID = dr.GetInt32Value("UserID");
            entity.RoleID = dr.GetInt16Value("RoleID");
            entity.ReferenceID = dr.GetInt32Value("ReferenceID");
            entity.Value = dr.GetStringValue("Value");
            entity.IsActive = dr.GetBooleanValue("IsActive");
            entity.AddedBy = dr.GetStringValue("AddedBy");
            entity.DateAdded = dr.GetDateValue("DateAdded");
            entity.RevisedBy = dr.GetStringValue("RevisedBy");
            entity.DateRevised = dr.GetDateValue("DateRevised");

            return entity;
        }


        internal List<UserLockdownDetailInfo> GetLockdownDetail(int? UserID)
        {
            List<UserLockdownDetailInfo> oUserLockdownDetailInfoList = new List<UserLockdownDetailInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateGetLockdownDetailCommand(UserID);
                cmd.Connection = con;
                con.Open();
                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    UserLockdownDetailInfo oUserLockdownDetailInfo = this.MapUserLockdownDetailInfoObject(reader);
                    oUserLockdownDetailInfoList.Add(oUserLockdownDetailInfo);
                }
                reader.Close();
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                    con.Close();
            }

            return oUserLockdownDetailInfoList;
        }
        private IDbCommand CreateGetLockdownDetailCommand(int? UserID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_UserLockdownDetail");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            if (UserID.HasValue)
                parUserID.Value = UserID.Value;
            else
                parUserID.Value = DBNull.Value;
            cmdParams.Add(parUserID);
            return cmd;
        }
        protected UserLockdownDetailInfo MapUserLockdownDetailInfoObject(IDataReader r)
        {
            UserLockdownDetailInfo oUserLockdownDetailInfo = new UserLockdownDetailInfo();
            oUserLockdownDetailInfo.UserID = r.GetInt32Value("UserID");
            oUserLockdownDetailInfo.LockdownDateTime = r.GetDateValue("LockdownDateTime");
            oUserLockdownDetailInfo.ResetDateTime = r.GetDateValue("ResetDateTime");
            return oUserLockdownDetailInfo;
        }
    }
}