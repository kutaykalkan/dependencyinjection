

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO.Base
{

    public abstract class UserHdrDAOBase : CustomAbstractDAO<UserHdrInfo>//Adapdev.Data.AbstractDAO<UserHdrInfo> 
    {

        /// <summary>
        /// A static representation of column AddedBy
        /// </summary>
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
        /// <summary>
        /// A static representation of column CompanyID
        /// </summary>
        public static readonly string COLUMN_COMPANYID = "CompanyID";
        /// <summary>
        /// A static representation of column DateAdded
        /// </summary>
        public static readonly string COLUMN_DATEADDED = "DateAdded";
        /// <summary>
        /// A static representation of column DateRevised
        /// </summary>
        public static readonly string COLUMN_DATEREVISED = "DateRevised";
        /// <summary>
        /// A static representation of column DefaultRoleID
        /// </summary>
        public static readonly string COLUMN_DEFAULTROLEID = "DefaultRoleID";
        /// <summary>
        /// A static representation of column EmailID
        /// </summary>
        public static readonly string COLUMN_EMAILID = "EmailID";
        /// <summary>
        /// A static representation of column FirstName
        /// </summary>
        public static readonly string COLUMN_FIRSTNAME = "FirstName";
        /// <summary>
        /// A static representation of column HostName
        /// </summary>
        public static readonly string COLUMN_HOSTNAME = "HostName";
        /// <summary>
        /// A static representation of column IsActive
        /// </summary>
        public static readonly string COLUMN_ISACTIVE = "IsActive";
        /// <summary>
        /// A static representation of column IsNew
        /// </summary>
        public static readonly string COLUMN_ISNEW = "IsNew";
        /// <summary>
        /// A static representation of column JobTitle
        /// </summary>
        public static readonly string COLUMN_JOBTITLE = "JobTitle";
        /// <summary>
        /// A static representation of column LastLoggedIn
        /// </summary>
        public static readonly string COLUMN_LASTLOGGEDIN = "LastLoggedIn";
        /// <summary>
        /// A static representation of column LastName
        /// </summary>
        public static readonly string COLUMN_LASTNAME = "LastName";
        /// <summary>
        /// A static representation of column LoginID
        /// </summary>
        public static readonly string COLUMN_LOGINID = "LoginID";
        /// <summary>
        /// A static representation of column Password
        /// </summary>
        public static readonly string COLUMN_PASSWORD = "Password";
        /// <summary>
        /// A static representation of column Phone
        /// </summary>
        public static readonly string COLUMN_PHONE = "Phone";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// A static representation of column UserID
        /// </summary>
        public static readonly string COLUMN_USERID = "UserID";
        /// <summary>
        /// A static representation of column WorkPhone
        /// </summary>
        public static readonly string COLUMN_WORKPHONE = "WorkPhone";
        /// <summary>
        /// Provides access to the name of the primary key column (UserID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "UserID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "UserHdr";

        /// <summary>
        /// Provides access to the name of the database
        /// </summary>
        public static readonly string DATABASE_NAME = "SkyStemArt";


        /// <summary>
        ///  CurrentAppUserInfo  for further use
        /// </summary>
        public AppUserInfo CurrentAppUserInfo { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public UserHdrDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "UserHdr", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a UserHdrInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>UserHdrInfo</returns>
        protected override UserHdrInfo MapObject(System.Data.IDataReader r)
        {

            UserHdrInfo entity = new UserHdrInfo();
            entity.UserID = r.GetInt32Value("UserID");
            entity.FirstName = r.GetStringValue("FirstName");
            entity.LastName = r.GetStringValue("LastName");
            entity.LoginID = r.GetStringValue("LoginID");
            entity.Password = r.GetStringValue("Password");
            entity.EmailID = r.GetStringValue("EmailID");
            entity.CompanyID = r.GetInt32Value("CompanyID");
            entity.JobTitle = r.GetStringValue("JobTitle");
            entity.WorkPhone = r.GetStringValue("WorkPhone");
            entity.Phone = r.GetStringValue("Phone");
            entity.DefaultRoleID = r.GetInt16Value("DefaultRoleID");
            entity.DefaultLanguageID = r.GetInt32Value("DefaultLanguageID");
            entity.LastLoggedIn = r.GetDateValue("LastLoggedIn");
            entity.IsNew = r.GetBooleanValue("IsNew");
            entity.IsActive = r.GetBooleanValue("IsActive");
            entity.DateAdded = r.GetDateValue("DateAdded");
            entity.AddedBy = r.GetStringValue("AddedBy");
            entity.DateRevised = r.GetDateValue("DateRevised");
            entity.RevisedBy = r.GetStringValue("RevisedBy");
            entity.HostName = r.GetStringValue("HostName");
            entity.CompanyDisplayNameLabelID = r.GetInt32Value("CompanyDisplayNameLabelID");
            entity.FTPActivationStatusID = r.GetInt16Value("FTPActivationStatusId");
            entity.FTPServerID = r.GetInt16Value("FTPServerId");
            entity.FTPPassword = r.GetStringValue("FTPPassword");
            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in UserHdrInfo object
        /// </summary>
        /// <param name="o">A UserHdrInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(UserHdrInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_UserHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!string.IsNullOrEmpty(entity.AddedBy))
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (entity.CompanyID.HasValue)
                parCompanyID.Value = entity.CompanyID;
            else
                parCompanyID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (entity.DateAdded.HasValue)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (entity.DateRevised.HasValue)
                parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parDefaultRoleID = cmd.CreateParameter();
            parDefaultRoleID.ParameterName = "@DefaultRoleID";
            if (entity.DefaultRoleID.HasValue)
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
            if (!string.IsNullOrEmpty(entity.EmailID))
                parEmailID.Value = entity.EmailID;
            else
                parEmailID.Value = System.DBNull.Value;
            cmdParams.Add(parEmailID);

            System.Data.IDbDataParameter parFirstName = cmd.CreateParameter();
            parFirstName.ParameterName = "@FirstName";
            if (!string.IsNullOrEmpty(entity.FirstName))
                parFirstName.Value = entity.FirstName;
            else
                parFirstName.Value = System.DBNull.Value;
            cmdParams.Add(parFirstName);

            //System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            //parHostName.ParameterName = "@HostName";
            //if(!entity.IsHostNameNull)
            //    parHostName.Value = entity.HostName;
            //else
            //    parHostName.Value = System.DBNull.Value;
            //cmdParams.Add(parHostName);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (entity.IsActive.HasValue)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parIsNew = cmd.CreateParameter();
            parIsNew.ParameterName = "@IsNew";
            if (entity.IsNew.HasValue)
                parIsNew.Value = entity.IsNew;
            else
                parIsNew.Value = System.DBNull.Value;
            cmdParams.Add(parIsNew);

            System.Data.IDbDataParameter parJobTitle = cmd.CreateParameter();
            parJobTitle.ParameterName = "@JobTitle";
            if (!string.IsNullOrEmpty(entity.JobTitle))
                parJobTitle.Value = entity.JobTitle;
            else
                parJobTitle.Value = System.DBNull.Value;
            cmdParams.Add(parJobTitle);

            System.Data.IDbDataParameter parLastLoggedIn = cmd.CreateParameter();
            parLastLoggedIn.ParameterName = "@LastLoggedIn";
            if (entity.LastLoggedIn.HasValue)
                parLastLoggedIn.Value = entity.LastLoggedIn.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parLastLoggedIn.Value = System.DBNull.Value;
            cmdParams.Add(parLastLoggedIn);

            System.Data.IDbDataParameter parLastName = cmd.CreateParameter();
            parLastName.ParameterName = "@LastName";
            if (!string.IsNullOrEmpty(entity.LastName))
                parLastName.Value = entity.LastName;
            else
                parLastName.Value = System.DBNull.Value;
            cmdParams.Add(parLastName);

            System.Data.IDbDataParameter parLoginID = cmd.CreateParameter();
            parLoginID.ParameterName = "@LoginID";
            if (!string.IsNullOrEmpty(entity.LoginID))
                parLoginID.Value = entity.LoginID;
            else
                parLoginID.Value = System.DBNull.Value;
            cmdParams.Add(parLoginID);

            System.Data.IDbDataParameter parPassword = cmd.CreateParameter();
            parPassword.ParameterName = "@Password";
            if (!string.IsNullOrEmpty(entity.Password))
                parPassword.Value = entity.Password;
            else
                parPassword.Value = System.DBNull.Value;
            cmdParams.Add(parPassword);

            System.Data.IDbDataParameter parPhone = cmd.CreateParameter();
            parPhone.ParameterName = "@Phone";
            if (!string.IsNullOrEmpty(entity.Phone))
                parPhone.Value = entity.Phone;
            else
                parPhone.Value = System.DBNull.Value;
            cmdParams.Add(parPhone);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!string.IsNullOrEmpty(entity.RevisedBy))
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parWorkPhone = cmd.CreateParameter();
            parWorkPhone.ParameterName = "@WorkPhone";
            if (!string.IsNullOrEmpty(entity.WorkPhone))
                parWorkPhone.Value = entity.WorkPhone;
            else
                parWorkPhone.Value = System.DBNull.Value;
            cmdParams.Add(parWorkPhone);

            System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            if (entity.AddedByRoleID.HasValue)
                parRoleID.Value = entity.AddedByRoleID.Value;
            else
                parRoleID.Value = System.DBNull.Value;
            cmdParams.Add(parRoleID);

            System.Data.IDbDataParameter parFTPActivationStatusId = cmd.CreateParameter();
            parFTPActivationStatusId.ParameterName = "@FTPActivationStatusId";
            if (entity.FTPActivationStatusID.HasValue)
                parFTPActivationStatusId.Value = entity.FTPActivationStatusID.Value;
            else
                parFTPActivationStatusId.Value = System.DBNull.Value;
            cmdParams.Add(parFTPActivationStatusId);

            System.Data.IDbDataParameter parFTPServerId = cmd.CreateParameter();
            parFTPServerId.ParameterName = "@FTPServerId";
            if (entity.FTPServerID.HasValue)
                parFTPServerId.Value = entity.FTPServerID.Value;
            else
                parFTPServerId.Value = System.DBNull.Value;
            cmdParams.Add(parFTPServerId);

            System.Data.IDbDataParameter parFTPPassword = cmd.CreateParameter();
            parFTPPassword.ParameterName = "@FTPPassword";
            if (!string.IsNullOrEmpty(entity.FTPPassword))
                parFTPPassword.Value = entity.FTPPassword;
            else
                parFTPPassword.Value = System.DBNull.Value;
            cmdParams.Add(parFTPPassword);
            System.Data.IDbDataParameter parFTPLoginID = cmd.CreateParameter();
            parFTPLoginID.ParameterName = "@FTPLoginID";
            if (!string.IsNullOrEmpty(entity.FTPLoginID))
                parFTPLoginID.Value = entity.FTPLoginID;
            else
                parFTPLoginID.Value = System.DBNull.Value;
            cmdParams.Add(parFTPLoginID);
            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in UserHdrInfo object
        /// </summary>
        /// <param name="o">A UserHdrInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(UserHdrInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("usp_UPD_UserHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            //System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            //parAddedBy.ParameterName = "@AddedBy";
            //if(!entity.IsAddedByNull)
            //    parAddedBy.Value = entity.AddedBy;
            //else
            //    parAddedBy.Value = System.DBNull.Value;
            //cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (!entity.IsCompanyIDNull && entity.CompanyID.HasValue)
                parCompanyID.Value = entity.CompanyID;
            else
                parCompanyID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyID);

            //System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            //parDateAdded.ParameterName = "@DateAdded";
            //if(!entity.IsDateAddedNull)
            //    parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
            //else
            //    parDateAdded.Value = System.DBNull.Value;
            //cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (entity.DateRevised.HasValue)
                parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parDefaultRoleID = cmd.CreateParameter();
            parDefaultRoleID.ParameterName = "@DefaultRoleID";
            if (entity.DefaultRoleID.HasValue)
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
            if (!string.IsNullOrEmpty(entity.EmailID))
                parEmailID.Value = entity.EmailID;
            else
                parEmailID.Value = System.DBNull.Value;
            cmdParams.Add(parEmailID);

            System.Data.IDbDataParameter parFirstName = cmd.CreateParameter();
            parFirstName.ParameterName = "@FirstName";
            if (!string.IsNullOrEmpty(entity.FirstName))
                parFirstName.Value = entity.FirstName;
            else
                parFirstName.Value = System.DBNull.Value;
            cmdParams.Add(parFirstName);

            //System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            //parHostName.ParameterName = "@HostName";
            //if(!entity.IsHostNameNull)
            //    parHostName.Value = entity.HostName;
            //else
            //    parHostName.Value = System.DBNull.Value;
            //cmdParams.Add(parHostName);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (entity.IsActive.HasValue)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parIsNew = cmd.CreateParameter();
            parIsNew.ParameterName = "@IsNew";
            if (entity.IsNew.HasValue)
                parIsNew.Value = entity.IsNew;
            else
                parIsNew.Value = System.DBNull.Value;
            cmdParams.Add(parIsNew);

            System.Data.IDbDataParameter parJobTitle = cmd.CreateParameter();
            parJobTitle.ParameterName = "@JobTitle";
            if (!string.IsNullOrEmpty(entity.JobTitle))
                parJobTitle.Value = entity.JobTitle;
            else
                parJobTitle.Value = System.DBNull.Value;
            cmdParams.Add(parJobTitle);

            //System.Data.IDbDataParameter parLastLoggedIn = cmd.CreateParameter();
            //parLastLoggedIn.ParameterName = "@LastLoggedIn";
            //if(!entity.IsLastLoggedInNull)
            //    parLastLoggedIn.Value = entity.LastLoggedIn.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
            //else
            //    parLastLoggedIn.Value = System.DBNull.Value;
            //cmdParams.Add(parLastLoggedIn);

            System.Data.IDbDataParameter parLastName = cmd.CreateParameter();
            parLastName.ParameterName = "@LastName";
            if (!string.IsNullOrEmpty(entity.LastName))
                parLastName.Value = entity.LastName;
            else
                parLastName.Value = System.DBNull.Value;
            cmdParams.Add(parLastName);

            //System.Data.IDbDataParameter parLoginID = cmd.CreateParameter();
            //parLoginID.ParameterName = "@LoginID";
            //if(!entity.IsLoginIDNull)
            //    parLoginID.Value = entity.LoginID;
            //else
            //    parLoginID.Value = System.DBNull.Value;
            //cmdParams.Add(parLoginID);

            //System.Data.IDbDataParameter parPassword = cmd.CreateParameter();
            //parPassword.ParameterName = "@Password";
            //if(!entity.IsPasswordNull)
            //    parPassword.Value = entity.Password;
            //else
            //    parPassword.Value = System.DBNull.Value;
            //cmdParams.Add(parPassword);

            System.Data.IDbDataParameter parPhone = cmd.CreateParameter();
            parPhone.ParameterName = "@Phone";
            if (!string.IsNullOrEmpty(entity.Phone))
                parPhone.Value = entity.Phone;
            else
                parPhone.Value = System.DBNull.Value;
            cmdParams.Add(parPhone);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!string.IsNullOrEmpty(entity.RevisedBy))
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parWorkPhone = cmd.CreateParameter();
            parWorkPhone.ParameterName = "@WorkPhone";
            if (!string.IsNullOrEmpty(entity.WorkPhone))
                parWorkPhone.Value = entity.WorkPhone;
            else
                parWorkPhone.Value = System.DBNull.Value;
            cmdParams.Add(parWorkPhone);

            System.Data.IDbDataParameter pkparUserID = cmd.CreateParameter();
            pkparUserID.ParameterName = "@UserID";
            pkparUserID.Value = entity.UserID;
            cmdParams.Add(pkparUserID);

            System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            if (entity.AddedByRoleID.HasValue)
                parRoleID.Value = entity.AddedByRoleID.Value;
            else
                parRoleID.Value = System.DBNull.Value;
            cmdParams.Add(parRoleID);

            System.Data.IDbDataParameter parFTPActivationStatusId = cmd.CreateParameter();
            parFTPActivationStatusId.ParameterName = "@FTPActivationStatusId";
            if (entity.FTPActivationStatusID.HasValue)
                parFTPActivationStatusId.Value = entity.FTPActivationStatusID.Value;
            else
                parFTPActivationStatusId.Value = System.DBNull.Value;
            cmdParams.Add(parFTPActivationStatusId);

            System.Data.IDbDataParameter parFTPServerId = cmd.CreateParameter();
            parFTPServerId.ParameterName = "@FTPServerId";
            if (entity.FTPServerID.HasValue)
                parFTPServerId.Value = entity.FTPServerID.Value;
            else
                parFTPServerId.Value = System.DBNull.Value;
            cmdParams.Add(parFTPServerId);

            System.Data.IDbDataParameter parFTPPassword = cmd.CreateParameter();
            parFTPPassword.ParameterName = "@FTPPassword";
            if (!string.IsNullOrEmpty(entity.FTPPassword))
                parFTPPassword.Value = entity.FTPPassword;
            else
                parFTPPassword.Value = System.DBNull.Value;
            cmdParams.Add(parFTPPassword);

            System.Data.IDbDataParameter parFTPLoginID = cmd.CreateParameter();
            parFTPLoginID.ParameterName = "@FTPLoginID";
            if (!string.IsNullOrEmpty(entity.FTPLoginID))
                parFTPLoginID.Value = entity.FTPLoginID;
            else
                parFTPLoginID.Value = System.DBNull.Value;
            cmdParams.Add(parFTPLoginID);

            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_UserHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }


        /// <summary>
        /// Creates the sql select command, using the passed in primary key
        /// </summary>
        /// <param name="o">The primary key of the object to select</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateSelectOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("usp_GET_UserHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }

        protected System.Data.IDbCommand CreateSelectOneCommand(string loginID, string password)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_GET_UserHdrAfterAuthentication");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parLoginID = cmd.CreateParameter();
            parLoginID.ParameterName = "@LoginID";
            parLoginID.Value = loginID;
            cmdParams.Add(parLoginID);

            System.Data.IDbDataParameter parPassword = cmd.CreateParameter();
            parPassword.ParameterName = "@Password";
            parPassword.Value = password;
            cmdParams.Add(parPassword);

            return cmd;

        }

        protected System.Data.IDbCommand CreateSelectOneByLoginIDCommand(string loginID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_GET_UserHdrByLoginID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parLoginID = cmd.CreateParameter();
            parLoginID.ParameterName = "@LoginID";
            parLoginID.Value = loginID;
            cmdParams.Add(parLoginID);

            return cmd;
        }

        protected System.Data.IDbCommand CreateUpdatePasswordCommand(string loginID, string password)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_UPD_UserPassword");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parLoginID = cmd.CreateParameter();
            parLoginID.ParameterName = "@LoginID";
            parLoginID.Value = loginID;
            cmdParams.Add(parLoginID);

            System.Data.IDbDataParameter parPassword = cmd.CreateParameter();
            parPassword.ParameterName = "@Password";
            parPassword.Value = password;
            cmdParams.Add(parPassword);

            //System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            //parDateRevised.ParameterName = "@DateRevised";
            //parDateRevised.Value = DateTime.Now;
            //cmdParams.Add(parDateRevised);

            //System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            //parRevisedBy.ParameterName = "@RevisedBy";
            //parRevisedBy.Value = "Test User"; //@@
            //cmdParams.Add(parRevisedBy);

            //System.Data.IDbDataParameter parEmailID = cmd.CreateParameter();
            //parEmailID.ParameterName = "@EmailID";
            //parEmailID.Direction = ParameterDirection.Output;
            //cmdParams.Add(parEmailID);

            return cmd;
        }



        /// <summary>
        /// Creates the sql select command, using the passed in foreign key.  This will return an
        /// IList of all objects that have that foreign key.
        /// </summary>
        /// <param name="o">The foreign key of the objects to select</param>
        /// <returns>An IList</returns>
        public IList<UserHdrInfo> SelectAllByDefaultRoleID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_UserHdrByDefaultRoleID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DefaultRoleID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(UserHdrInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(UserHdrDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(UserHdrInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(UserHdrDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(UserHdrInfo entity, object id)
        {
            entity.UserID = Convert.ToInt32(id);
        }
















        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<UserHdrInfo> SelectUserHdrDetailsAssociatedToCompanyHdrByCertificationSignOffStatus(CompanyHdrInfo entity)
        {
            return this.SelectUserHdrDetailsAssociatedToCompanyHdrByCertificationSignOffStatus(entity.CompanyID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<UserHdrInfo> SelectUserHdrDetailsAssociatedToCompanyHdrByCertificationSignOffStatus(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [UserHdr] INNER JOIN [CertificationSignOffStatus] ON [UserHdr].[UserID] = [CertificationSignOffStatus].[UserID] INNER JOIN [CompanyHdr] ON [CertificationSignOffStatus].[CompanyID] = [CompanyHdr].[CompanyID]  WHERE  [CompanyHdr].[CompanyID] = @CompanyID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyID";
            par.Value = id;

            cmdParams.Add(par);
            List<UserHdrInfo> objUserHdrEntityColl = new List<UserHdrInfo>(this.Select(cmd));
            return objUserHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<UserHdrInfo> SelectUserHdrDetailsAssociatedToCertificationTypeMstByCertificationSignOffStatus(CertificationTypeMstInfo entity)
        {
            return this.SelectUserHdrDetailsAssociatedToCertificationTypeMstByCertificationSignOffStatus(entity.CertificationTypeID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<UserHdrInfo> SelectUserHdrDetailsAssociatedToCertificationTypeMstByCertificationSignOffStatus(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [UserHdr] INNER JOIN [CertificationSignOffStatus] ON [UserHdr].[UserID] = [CertificationSignOffStatus].[UserID] INNER JOIN [CertificationTypeMst] ON [CertificationSignOffStatus].[CertificationTypeID] = [CertificationTypeMst].[CertificationTypeID]  WHERE  [CertificationTypeMst].[CertificationTypeID] = @CertificationTypeID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CertificationTypeID";
            par.Value = id;

            cmdParams.Add(par);
            List<UserHdrInfo> objUserHdrEntityColl = new List<UserHdrInfo>(this.Select(cmd));
            return objUserHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<UserHdrInfo> SelectUserHdrDetailsAssociatedToReconciliationPeriodByCertificationSignOffStatus(ReconciliationPeriodInfo entity)
        {
            return this.SelectUserHdrDetailsAssociatedToReconciliationPeriodByCertificationSignOffStatus(entity.ReconciliationPeriodID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<UserHdrInfo> SelectUserHdrDetailsAssociatedToReconciliationPeriodByCertificationSignOffStatus(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [UserHdr] INNER JOIN [CertificationSignOffStatus] ON [UserHdr].[UserID] = [CertificationSignOffStatus].[UserID] INNER JOIN [ReconciliationPeriod] ON [CertificationSignOffStatus].[ReconciliationPeriodID] = [ReconciliationPeriod].[ReconciliationPeriodID]  WHERE  [ReconciliationPeriod].[ReconciliationPeriodID] = @ReconciliationPeriodID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationPeriodID";
            par.Value = id;

            cmdParams.Add(par);
            List<UserHdrInfo> objUserHdrEntityColl = new List<UserHdrInfo>(this.Select(cmd));
            return objUserHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<UserHdrInfo> SelectUserHdrDetailsAssociatedToGLDataHdrByGLDataReconciliationSubmissionDate(GLDataHdrInfo entity)
        {
            return this.SelectUserHdrDetailsAssociatedToGLDataHdrByGLDataReconciliationSubmissionDate(entity.GLDataID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<UserHdrInfo> SelectUserHdrDetailsAssociatedToGLDataHdrByGLDataReconciliationSubmissionDate(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [UserHdr] INNER JOIN [GLDataReconciliationSubmissionDate] ON [UserHdr].[UserID] = [GLDataReconciliationSubmissionDate].[UserID] INNER JOIN [GLDataHdr] ON [GLDataReconciliationSubmissionDate].[GLDataID] = [GLDataHdr].[GLDataID]  WHERE  [GLDataHdr].[GLDataID] = @GLDataID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataID";
            par.Value = id;

            cmdParams.Add(par);
            List<UserHdrInfo> objUserHdrEntityColl = new List<UserHdrInfo>(this.Select(cmd));
            return objUserHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<UserHdrInfo> SelectUserHdrDetailsAssociatedToRoleMstByGLDataReconciliationSubmissionDate(RoleMstInfo entity)
        {
            return this.SelectUserHdrDetailsAssociatedToRoleMstByGLDataReconciliationSubmissionDate(entity.RoleID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<UserHdrInfo> SelectUserHdrDetailsAssociatedToRoleMstByGLDataReconciliationSubmissionDate(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [UserHdr] INNER JOIN [GLDataReconciliationSubmissionDate] ON [UserHdr].[UserID] = [GLDataReconciliationSubmissionDate].[UserID] INNER JOIN [RoleMst] ON [GLDataReconciliationSubmissionDate].[RoleID] = [RoleMst].[RoleID]  WHERE  [RoleMst].[RoleID] = @RoleID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleID";
            par.Value = id;

            cmdParams.Add(par);
            List<UserHdrInfo> objUserHdrEntityColl = new List<UserHdrInfo>(this.Select(cmd));
            return objUserHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<UserHdrInfo> SelectUserHdrDetailsAssociatedToGLDataHdrByGLDataWriteOnOff(GLDataHdrInfo entity)
        {
            return this.SelectUserHdrDetailsAssociatedToGLDataHdrByGLDataWriteOnOff(entity.GLDataID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<UserHdrInfo> SelectUserHdrDetailsAssociatedToGLDataHdrByGLDataWriteOnOff(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [UserHdr] INNER JOIN [GLDataWriteOnOff] ON [UserHdr].[UserID] = [GLDataWriteOnOff].[UserID] INNER JOIN [GLDataHdr] ON [GLDataWriteOnOff].[GLDataID] = [GLDataHdr].[GLDataID]  WHERE  [GLDataHdr].[GLDataID] = @GLDataID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataID";
            par.Value = id;

            cmdParams.Add(par);
            List<UserHdrInfo> objUserHdrEntityColl = new List<UserHdrInfo>(this.Select(cmd));
            return objUserHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<UserHdrInfo> SelectUserHdrDetailsAssociatedToRoleMandatoryReportByMandatoryReportSignoffStatus(RoleMandatoryReportInfo entity)
        {
            return this.SelectUserHdrDetailsAssociatedToRoleMandatoryReportByMandatoryReportSignoffStatus(entity.RoleMandatoryReportID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<UserHdrInfo> SelectUserHdrDetailsAssociatedToRoleMandatoryReportByMandatoryReportSignoffStatus(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [UserHdr] INNER JOIN [MandatoryReportSignoffStatus] ON [UserHdr].[UserID] = [MandatoryReportSignoffStatus].[UserID] INNER JOIN [RoleMandatoryReport] ON [MandatoryReportSignoffStatus].[RoleMandatoryReportID] = [RoleMandatoryReport].[RoleMandatoryReportID]  WHERE  [RoleMandatoryReport].[RoleMandatoryReportID] = @RoleMandatoryReportID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleMandatoryReportID";
            par.Value = id;

            cmdParams.Add(par);
            List<UserHdrInfo> objUserHdrEntityColl = new List<UserHdrInfo>(this.Select(cmd));
            return objUserHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<UserHdrInfo> SelectUserHdrDetailsAssociatedToReconciliationPeriodByMandatoryReportSignoffStatus(ReconciliationPeriodInfo entity)
        {
            return this.SelectUserHdrDetailsAssociatedToReconciliationPeriodByMandatoryReportSignoffStatus(entity.ReconciliationPeriodID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<UserHdrInfo> SelectUserHdrDetailsAssociatedToReconciliationPeriodByMandatoryReportSignoffStatus(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [UserHdr] INNER JOIN [MandatoryReportSignoffStatus] ON [UserHdr].[UserID] = [MandatoryReportSignoffStatus].[UserID] INNER JOIN [ReconciliationPeriod] ON [MandatoryReportSignoffStatus].[ReconciliationPeriodID] = [ReconciliationPeriod].[ReconciliationPeriodID]  WHERE  [ReconciliationPeriod].[ReconciliationPeriodID] = @ReconciliationPeriodID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationPeriodID";
            par.Value = id;

            cmdParams.Add(par);
            List<UserHdrInfo> objUserHdrEntityColl = new List<UserHdrInfo>(this.Select(cmd));
            return objUserHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<UserHdrInfo> SelectUserHdrDetailsAssociatedToReportMstByReportSignOffStatus(ReportMstInfo entity)
        {
            return this.SelectUserHdrDetailsAssociatedToReportMstByReportSignOffStatus(entity.ReportID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<UserHdrInfo> SelectUserHdrDetailsAssociatedToReportMstByReportSignOffStatus(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [UserHdr] INNER JOIN [ReportSignOffStatus] ON [UserHdr].[UserID] = [ReportSignOffStatus].[UserID] INNER JOIN [ReportMst] ON [ReportSignOffStatus].[ReportID] = [ReportMst].[ReportID]  WHERE  [ReportMst].[ReportID] = @ReportID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReportID";
            par.Value = id;

            cmdParams.Add(par);
            List<UserHdrInfo> objUserHdrEntityColl = new List<UserHdrInfo>(this.Select(cmd));
            return objUserHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<UserHdrInfo> SelectUserHdrDetailsAssociatedToReconciliationPeriodByReportSignOffStatus(ReconciliationPeriodInfo entity)
        {
            return this.SelectUserHdrDetailsAssociatedToReconciliationPeriodByReportSignOffStatus(entity.ReconciliationPeriodID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<UserHdrInfo> SelectUserHdrDetailsAssociatedToReconciliationPeriodByReportSignOffStatus(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [UserHdr] INNER JOIN [ReportSignOffStatus] ON [UserHdr].[UserID] = [ReportSignOffStatus].[UserID] INNER JOIN [ReconciliationPeriod] ON [ReportSignOffStatus].[ReconciliationPeriodID] = [ReconciliationPeriod].[ReconciliationPeriodID]  WHERE  [ReconciliationPeriod].[ReconciliationPeriodID] = @ReconciliationPeriodID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationPeriodID";
            par.Value = id;

            cmdParams.Add(par);
            List<UserHdrInfo> objUserHdrEntityColl = new List<UserHdrInfo>(this.Select(cmd));
            return objUserHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<UserHdrInfo> SelectUserHdrDetailsAssociatedToReportMstByUserFavoriteReport(ReportMstInfo entity)
        {
            return this.SelectUserHdrDetailsAssociatedToReportMstByUserFavoriteReport(entity.ReportID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<UserHdrInfo> SelectUserHdrDetailsAssociatedToReportMstByUserFavoriteReport(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [UserHdr] INNER JOIN [UserFavoriteReport] ON [UserHdr].[UserID] = [UserFavoriteReport].[UserID] INNER JOIN [ReportMst] ON [UserFavoriteReport].[ReportID] = [ReportMst].[ReportID]  WHERE  [ReportMst].[ReportID] = @ReportID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReportID";
            par.Value = id;

            cmdParams.Add(par);
            List<UserHdrInfo> objUserHdrEntityColl = new List<UserHdrInfo>(this.Select(cmd));
            return objUserHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<UserHdrInfo> SelectUserHdrDetailsAssociatedToGeographyObjectHdrByUserGeographyObject(GeographyObjectHdrInfo entity)
        {
            return this.SelectUserHdrDetailsAssociatedToGeographyObjectHdrByUserGeographyObject(entity.GeographyObjectID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<UserHdrInfo> SelectUserHdrDetailsAssociatedToGeographyObjectHdrByUserGeographyObject(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [UserHdr] INNER JOIN [UserGeographyObject] ON [UserHdr].[UserID] = [UserGeographyObject].[UserID] INNER JOIN [GeographyObjectHdr] ON [UserGeographyObject].[GeographyObjectID] = [GeographyObjectHdr].[GeographyObjectID]  WHERE  [GeographyObjectHdr].[GeographyObjectID] = @GeographyObjectID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GeographyObjectID";
            par.Value = id;

            cmdParams.Add(par);
            List<UserHdrInfo> objUserHdrEntityColl = new List<UserHdrInfo>(this.Select(cmd));
            return objUserHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<UserHdrInfo> SelectUserHdrDetailsAssociatedToRoleMstByUserRole(RoleMstInfo entity)
        {
            return this.SelectUserHdrDetailsAssociatedToRoleMstByUserRole(entity.RoleID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<UserHdrInfo> SelectUserHdrDetailsAssociatedToRoleMstByUserRole(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [UserHdr] INNER JOIN [UserRole] ON [UserHdr].[UserID] = [UserRole].[UserID] INNER JOIN [RoleMst] ON [UserRole].[RoleID] = [RoleMst].[RoleID]  WHERE  [RoleMst].[RoleID] = @RoleID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleID";
            par.Value = id;

            cmdParams.Add(par);
            List<UserHdrInfo> objUserHdrEntityColl = new List<UserHdrInfo>(this.Select(cmd));
            return objUserHdrEntityColl;
        }

        protected System.Data.IDbCommand CreateUpdateFTPPasswordCommand(string loginID, string ftpLoginID, string password)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_UPD_UserFTPPassword");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parLoginID = cmd.CreateParameter();
            parLoginID.ParameterName = "@LoginID";
            parLoginID.Value = loginID;
            cmdParams.Add(parLoginID);

            System.Data.IDbDataParameter parFTPLoginID = cmd.CreateParameter();
            parFTPLoginID.ParameterName = "@FTPLoginID";
            parFTPLoginID.Value = ftpLoginID;
            cmdParams.Add(parFTPLoginID);

            System.Data.IDbDataParameter parPassword = cmd.CreateParameter();
            parPassword.ParameterName = "@Password";
            parPassword.Value = password;
            cmdParams.Add(parPassword);

            return cmd;
        }


    }
}
