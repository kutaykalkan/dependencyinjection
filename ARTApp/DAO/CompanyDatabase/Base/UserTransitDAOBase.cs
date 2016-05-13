
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Model.CompanyDatabase;
using SkyStem.ART.App.DAO.Base;

namespace SkyStem.ART.App.DAO.CompanyDatabase.Base 
{
	public abstract class UserTransitDAOBase : CustomAbstractDAO<UserTransitInfo> 
	{
		/// <summary>
		/// A static representation of column AddedBy
		/// </summary>
		public static readonly string COLUMN_ADDEDBY = "AddedBy";
		/// <summary>
		/// A static representation of column CompanyTransitID
		/// </summary>
		public static readonly string COLUMN_COMPANYTRANSITID = "CompanyTransitID";
		/// <summary>
		/// A static representation of column DataImportID
		/// </summary>
		public static readonly string COLUMN_DATAIMPORTID = "DataImportID";
		/// <summary>
		/// A static representation of column DateAdded
		/// </summary>
		public static readonly string COLUMN_DATEADDED = "DateAdded";
		/// <summary>
		/// A static representation of column DateRevised
		/// </summary>
		public static readonly string COLUMN_DATEREVISED = "DateRevised";
		/// <summary>
		/// A static representation of column DefaultLanguageID
		/// </summary>
		public static readonly string COLUMN_DEFAULTLANGUAGEID = "DefaultLanguageID";
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
		/// A static representation of column IsModifiedByDataImport
		/// </summary>
		public static readonly string COLUMN_ISMODIFIEDBYDATAIMPORT = "IsModifiedByDataImport";
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
		/// A static representation of column UserTransitID
		/// </summary>
		public static readonly string COLUMN_USERTRANSITID = "UserTransitID";
		/// <summary>
		/// A static representation of column WorkPhone
		/// </summary>
		public static readonly string COLUMN_WORKPHONE = "WorkPhone";
		/// <summary>
		/// Provides access to the name of the primary key column (UserTransitID)
		/// </summary>
		public static readonly string TABLE_PRIMARYKEY = "UserTransitID";

		/// <summary>
		/// Provides access to the name of the table
		/// </summary>
		public static readonly string TABLE_NAME = "UserTransit";

		/// <summary>
		/// Provides access to the name of the database
		/// </summary>
		public static readonly string DATABASE_NAME = "SkyStemARTCore";

        /// <summary>
        ///  CurrentAppUserInfo  for further use
        /// </summary>
        public AppUserInfo CurrentAppUserInfo { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
        public UserTransitDAOBase(AppUserInfo oAppUserInfo) : 
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "UserTransit", DbConstants.ConnectionStringCore) 
        {
            CurrentAppUserInfo = oAppUserInfo;
        }
        
        /// <summary>
        /// Maps the IDataReader values to a UserTransitInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>UserTransitInfo</returns>
        protected override UserTransitInfo MapObject(System.Data.IDataReader r) 
        {
            UserTransitInfo entity = new UserTransitInfo();
			entity.UserTransitID = r.GetInt32Value("UserTransitID");
			entity.FirstName = r.GetStringValue("FirstName");
			entity.LastName = r.GetStringValue("LastName");
			entity.LoginID = r.GetStringValue("LoginID");
			entity.Password = r.GetStringValue("Password");
			entity.EmailID = r.GetStringValue("EmailID");
			entity.CompanyTransitID = r.GetInt32Value("CompanyTransitID");
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
			entity.DataImportID = r.GetInt32Value("DataImportID");
			entity.IsModifiedByDataImport = r.GetBooleanValue("IsModifiedByDataImport");
            return entity;
        }
 
		/// <summary>
		/// Creates the sql insert command, using the values from the passed
		/// in UserTransitInfo object
		/// </summary>
		/// <param name="o">A UserTransitInfo object, from which the insert values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(UserTransitInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_UserTransit");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
			parAddedBy.ParameterName = "@AddedBy";
			if(entity != null)
				parAddedBy.Value = entity.AddedBy;
			else
				parAddedBy.Value = System.DBNull.Value;
			cmdParams.Add(parAddedBy);
			System.Data.IDbDataParameter parCompanyTransitID = cmd.CreateParameter();
			parCompanyTransitID.ParameterName = "@CompanyTransitID";
			if(entity != null)
				parCompanyTransitID.Value = entity.CompanyTransitID;
			else
				parCompanyTransitID.Value = System.DBNull.Value;
			cmdParams.Add(parCompanyTransitID);
			System.Data.IDbDataParameter parDataImportID = cmd.CreateParameter();
			parDataImportID.ParameterName = "@DataImportID";
			if(entity != null)
				parDataImportID.Value = entity.DataImportID;
			else
				parDataImportID.Value = System.DBNull.Value;
			cmdParams.Add(parDataImportID);
			System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
			parDateAdded.ParameterName = "@DateAdded";
			if(entity != null)
				parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
			else
				parDateAdded.Value = System.DBNull.Value;
			cmdParams.Add(parDateAdded);
			System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
			parDateRevised.ParameterName = "@DateRevised";
			if(entity != null)
				parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
			else
				parDateRevised.Value = System.DBNull.Value;
			cmdParams.Add(parDateRevised);
			System.Data.IDbDataParameter parDefaultLanguageID = cmd.CreateParameter();
			parDefaultLanguageID.ParameterName = "@DefaultLanguageID";
			if(entity != null)
				parDefaultLanguageID.Value = entity.DefaultLanguageID;
			else
				parDefaultLanguageID.Value = System.DBNull.Value;
			cmdParams.Add(parDefaultLanguageID);
			System.Data.IDbDataParameter parDefaultRoleID = cmd.CreateParameter();
			parDefaultRoleID.ParameterName = "@DefaultRoleID";
			if(entity != null)
				parDefaultRoleID.Value = entity.DefaultRoleID;
			else
				parDefaultRoleID.Value = System.DBNull.Value;
			cmdParams.Add(parDefaultRoleID);
			System.Data.IDbDataParameter parEmailID = cmd.CreateParameter();
			parEmailID.ParameterName = "@EmailID";
			if(entity != null)
				parEmailID.Value = entity.EmailID;
			else
				parEmailID.Value = System.DBNull.Value;
			cmdParams.Add(parEmailID);
			System.Data.IDbDataParameter parFirstName = cmd.CreateParameter();
			parFirstName.ParameterName = "@FirstName";
			if(entity != null)
				parFirstName.Value = entity.FirstName;
			else
				parFirstName.Value = System.DBNull.Value;
			cmdParams.Add(parFirstName);
			System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
			parHostName.ParameterName = "@HostName";
			if(entity != null)
				parHostName.Value = entity.HostName;
			else
				parHostName.Value = System.DBNull.Value;
			cmdParams.Add(parHostName);
			System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
			parIsActive.ParameterName = "@IsActive";
			if(entity != null)
				parIsActive.Value = entity.IsActive;
			else
				parIsActive.Value = System.DBNull.Value;
			cmdParams.Add(parIsActive);
			System.Data.IDbDataParameter parIsModifiedByDataImport = cmd.CreateParameter();
			parIsModifiedByDataImport.ParameterName = "@IsModifiedByDataImport";
			if(entity != null)
				parIsModifiedByDataImport.Value = entity.IsModifiedByDataImport;
			else
				parIsModifiedByDataImport.Value = System.DBNull.Value;
			cmdParams.Add(parIsModifiedByDataImport);
			System.Data.IDbDataParameter parIsNew = cmd.CreateParameter();
			parIsNew.ParameterName = "@IsNew";
			if(entity != null)
				parIsNew.Value = entity.IsNew;
			else
				parIsNew.Value = System.DBNull.Value;
			cmdParams.Add(parIsNew);
			System.Data.IDbDataParameter parJobTitle = cmd.CreateParameter();
			parJobTitle.ParameterName = "@JobTitle";
			if(entity != null)
				parJobTitle.Value = entity.JobTitle;
			else
				parJobTitle.Value = System.DBNull.Value;
			cmdParams.Add(parJobTitle);
			System.Data.IDbDataParameter parLastLoggedIn = cmd.CreateParameter();
			parLastLoggedIn.ParameterName = "@LastLoggedIn";
			if(entity != null)
				parLastLoggedIn.Value = entity.LastLoggedIn.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
			else
				parLastLoggedIn.Value = System.DBNull.Value;
			cmdParams.Add(parLastLoggedIn);
			System.Data.IDbDataParameter parLastName = cmd.CreateParameter();
			parLastName.ParameterName = "@LastName";
			if(entity != null)
				parLastName.Value = entity.LastName;
			else
				parLastName.Value = System.DBNull.Value;
			cmdParams.Add(parLastName);
			System.Data.IDbDataParameter parLoginID = cmd.CreateParameter();
			parLoginID.ParameterName = "@LoginID";
			if(entity != null)
				parLoginID.Value = entity.LoginID;
			else
				parLoginID.Value = System.DBNull.Value;
			cmdParams.Add(parLoginID);
			System.Data.IDbDataParameter parPassword = cmd.CreateParameter();
			parPassword.ParameterName = "@Password";
			if(entity != null)
				parPassword.Value = entity.Password;
			else
				parPassword.Value = System.DBNull.Value;
			cmdParams.Add(parPassword);
			System.Data.IDbDataParameter parPhone = cmd.CreateParameter();
			parPhone.ParameterName = "@Phone";
			if(entity != null)
				parPhone.Value = entity.Phone;
			else
				parPhone.Value = System.DBNull.Value;
			cmdParams.Add(parPhone);
			System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
			parRevisedBy.ParameterName = "@RevisedBy";
			if(entity != null)
				parRevisedBy.Value = entity.RevisedBy;
			else
				parRevisedBy.Value = System.DBNull.Value;
			cmdParams.Add(parRevisedBy);
			System.Data.IDbDataParameter parWorkPhone = cmd.CreateParameter();
			parWorkPhone.ParameterName = "@WorkPhone";
			if(entity != null)
				parWorkPhone.Value = entity.WorkPhone;
			else
				parWorkPhone.Value = System.DBNull.Value;
			cmdParams.Add(parWorkPhone);
			return cmd;
        }

		/// <summary>
		/// Creates the sql update command, using the values from the passed
		/// in UserTransitInfo object
		/// </summary>
		/// <param name="o">A UserTransitInfo object, from which the update values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(UserTransitInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_UserTransit");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
			parAddedBy.ParameterName = "@AddedBy";
			if(entity != null)
				parAddedBy.Value = entity.AddedBy;
			else
				parAddedBy.Value = System.DBNull.Value;
			cmdParams.Add(parAddedBy);
			System.Data.IDbDataParameter parCompanyTransitID = cmd.CreateParameter();
			parCompanyTransitID.ParameterName = "@CompanyTransitID";
			if(entity != null)
				parCompanyTransitID.Value = entity.CompanyTransitID;
			else
				parCompanyTransitID.Value = System.DBNull.Value;
			cmdParams.Add(parCompanyTransitID);
			System.Data.IDbDataParameter parDataImportID = cmd.CreateParameter();
			parDataImportID.ParameterName = "@DataImportID";
			if(entity != null)
				parDataImportID.Value = entity.DataImportID;
			else
				parDataImportID.Value = System.DBNull.Value;
			cmdParams.Add(parDataImportID);
			System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
			parDateAdded.ParameterName = "@DateAdded";
			if(entity != null)
				parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
			else
				parDateAdded.Value = System.DBNull.Value;
			cmdParams.Add(parDateAdded);
			System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
			parDateRevised.ParameterName = "@DateRevised";
			if(entity != null)
				parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
			else
				parDateRevised.Value = System.DBNull.Value;
			cmdParams.Add(parDateRevised);
			System.Data.IDbDataParameter parDefaultLanguageID = cmd.CreateParameter();
			parDefaultLanguageID.ParameterName = "@DefaultLanguageID";
			if(entity != null)
				parDefaultLanguageID.Value = entity.DefaultLanguageID;
			else
				parDefaultLanguageID.Value = System.DBNull.Value;
			cmdParams.Add(parDefaultLanguageID);
			System.Data.IDbDataParameter parDefaultRoleID = cmd.CreateParameter();
			parDefaultRoleID.ParameterName = "@DefaultRoleID";
			if(entity != null)
				parDefaultRoleID.Value = entity.DefaultRoleID;
			else
				parDefaultRoleID.Value = System.DBNull.Value;
			cmdParams.Add(parDefaultRoleID);
			System.Data.IDbDataParameter parEmailID = cmd.CreateParameter();
			parEmailID.ParameterName = "@EmailID";
			if(entity != null)
				parEmailID.Value = entity.EmailID;
			else
				parEmailID.Value = System.DBNull.Value;
			cmdParams.Add(parEmailID);
			System.Data.IDbDataParameter parFirstName = cmd.CreateParameter();
			parFirstName.ParameterName = "@FirstName";
			if(entity != null)
				parFirstName.Value = entity.FirstName;
			else
				parFirstName.Value = System.DBNull.Value;
			cmdParams.Add(parFirstName);
			System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
			parHostName.ParameterName = "@HostName";
			if(entity != null)
				parHostName.Value = entity.HostName;
			else
				parHostName.Value = System.DBNull.Value;
			cmdParams.Add(parHostName);
			System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
			parIsActive.ParameterName = "@IsActive";
			if(entity != null)
				parIsActive.Value = entity.IsActive;
			else
				parIsActive.Value = System.DBNull.Value;
			cmdParams.Add(parIsActive);
			System.Data.IDbDataParameter parIsModifiedByDataImport = cmd.CreateParameter();
			parIsModifiedByDataImport.ParameterName = "@IsModifiedByDataImport";
			if(entity != null)
				parIsModifiedByDataImport.Value = entity.IsModifiedByDataImport;
			else
				parIsModifiedByDataImport.Value = System.DBNull.Value;
			cmdParams.Add(parIsModifiedByDataImport);
			System.Data.IDbDataParameter parIsNew = cmd.CreateParameter();
			parIsNew.ParameterName = "@IsNew";
			if(entity != null)
				parIsNew.Value = entity.IsNew;
			else
				parIsNew.Value = System.DBNull.Value;
			cmdParams.Add(parIsNew);
			System.Data.IDbDataParameter parJobTitle = cmd.CreateParameter();
			parJobTitle.ParameterName = "@JobTitle";
			if(entity != null)
				parJobTitle.Value = entity.JobTitle;
			else
				parJobTitle.Value = System.DBNull.Value;
			cmdParams.Add(parJobTitle);
			System.Data.IDbDataParameter parLastLoggedIn = cmd.CreateParameter();
			parLastLoggedIn.ParameterName = "@LastLoggedIn";
			if(entity != null)
				parLastLoggedIn.Value = entity.LastLoggedIn.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
			else
				parLastLoggedIn.Value = System.DBNull.Value;
			cmdParams.Add(parLastLoggedIn);
			System.Data.IDbDataParameter parLastName = cmd.CreateParameter();
			parLastName.ParameterName = "@LastName";
			if(entity != null)
				parLastName.Value = entity.LastName;
			else
				parLastName.Value = System.DBNull.Value;
			cmdParams.Add(parLastName);
			System.Data.IDbDataParameter parLoginID = cmd.CreateParameter();
			parLoginID.ParameterName = "@LoginID";
			if(entity != null)
				parLoginID.Value = entity.LoginID;
			else
				parLoginID.Value = System.DBNull.Value;
			cmdParams.Add(parLoginID);
			System.Data.IDbDataParameter parPassword = cmd.CreateParameter();
			parPassword.ParameterName = "@Password";
			if(entity != null)
				parPassword.Value = entity.Password;
			else
				parPassword.Value = System.DBNull.Value;
			cmdParams.Add(parPassword);
			System.Data.IDbDataParameter parPhone = cmd.CreateParameter();
			parPhone.ParameterName = "@Phone";
			if(entity != null)
				parPhone.Value = entity.Phone;
			else
				parPhone.Value = System.DBNull.Value;
			cmdParams.Add(parPhone);
			System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
			parRevisedBy.ParameterName = "@RevisedBy";
			if(entity != null)
				parRevisedBy.Value = entity.RevisedBy;
			else
				parRevisedBy.Value = System.DBNull.Value;
			cmdParams.Add(parRevisedBy);
			System.Data.IDbDataParameter parWorkPhone = cmd.CreateParameter();
			parWorkPhone.ParameterName = "@WorkPhone";
			if(entity != null)
				parWorkPhone.Value = entity.WorkPhone;
			else
				parWorkPhone.Value = System.DBNull.Value;
			cmdParams.Add(parWorkPhone);
			System.Data.IDbDataParameter pkparUserTransitID = cmd.CreateParameter();
			pkparUserTransitID.ParameterName = "@UserTransitID";
			pkparUserTransitID.Value = entity.UserTransitID;
			cmdParams.Add(pkparUserTransitID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql delete command, using the passed in primary key
		/// </summary>
		/// <param name="id">The primary key of the object to delete</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_UserTransit");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@UserTransitID";
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
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_UserTransit");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@UserTransitID";
			par.Value = id;
			cmdParams.Add(par);
            return cmd;
        }


		/// <summary>
		/// Creates the sql select command, using the passed in foreign key.  This will return an
		/// IList of all objects that have that foreign key.
		/// </summary>
		/// <param name="o">The foreign key of the objects to select</param>
		/// <returns>An IList</returns>
		public IList<UserTransitInfo> SelectAllByCompanyTransitID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_UserTransitByCompanyTransitID");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@CompanyTransitID";
			par.Value = id;
			cmdParams.Add(par);
            return this.Select(cmd);
		}
		protected override void CustomSave(UserTransitInfo o, IDbConnection connection)
		{
			string query = QueryHelper.GetSqlServerLastInsertedCommand(UserTransitDAO.TABLE_NAME);
			IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
			cmd.CommandText = query;
			cmd.Connection = connection;
			object id = cmd.ExecuteScalar();
			this.MapIdentity(o, id);
		}
		
		protected override void CustomSave(UserTransitInfo o, IDbConnection connection, IDbTransaction transaction)
		{
			string query = QueryHelper.GetSqlServerLastInsertedCommand(UserTransitDAO.TABLE_NAME);
			IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
			cmd.CommandText = query;
			cmd.Transaction = transaction;
			cmd.Connection = connection;
			object id = cmd.ExecuteScalar();
			this.MapIdentity(o, id);
		}		
		private void MapIdentity(UserTransitInfo entity, object id)
		{
			entity.UserTransitID = Convert.ToInt32(id);
		}
	
    }
}
