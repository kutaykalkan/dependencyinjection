
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
	public abstract class CompanyVersionScriptDAOBase : AbstractDAO<CompanyVersionScriptInfo> 
	{
		/// <summary>
		/// A static representation of column AddedBy
		/// </summary>
		public static readonly string COLUMN_ADDEDBY = "AddedBy";
		/// <summary>
		/// A static representation of column CompanyVersionScriptID
		/// </summary>
		public static readonly string COLUMN_COMPANYVERSIONSCRIPTID = "CompanyVersionScriptID";
		/// <summary>
		/// A static representation of column DateAdded
		/// </summary>
		public static readonly string COLUMN_DATEADDED = "DateAdded";
		/// <summary>
		/// A static representation of column DateRevised
		/// </summary>
		public static readonly string COLUMN_DATEREVISED = "DateRevised";
		/// <summary>
		/// A static representation of column ErrorMsg
		/// </summary>
		public static readonly string COLUMN_ERRORMSG = "ErrorMsg";
		/// <summary>
		/// A static representation of column HostName
		/// </summary>
		public static readonly string COLUMN_HOSTNAME = "HostName";
		/// <summary>
		/// A static representation of column IsActive
		/// </summary>
		public static readonly string COLUMN_ISACTIVE = "IsActive";
		/// <summary>
		/// A static representation of column ReleaseStatusID
		/// </summary>
		public static readonly string COLUMN_RELEASESTATUSID = "ReleaseStatusID";
		/// <summary>
		/// A static representation of column RevisedBy
		/// </summary>
		public static readonly string COLUMN_REVISEDBY = "RevisedBy";
		/// <summary>
		/// A static representation of column CompanyID
		/// </summary>
		public static readonly string COLUMN_SERVERCOMPANYID = "CompanyID";
		/// <summary>
		/// A static representation of column VersionScriptID
		/// </summary>
		public static readonly string COLUMN_VERSIONSCRIPTID = "VersionScriptID";
		/// <summary>
		/// Provides access to the name of the primary key column (CompanyVersionScriptID)
		/// </summary>
		public static readonly string TABLE_PRIMARYKEY = "CompanyVersionScriptID";

		/// <summary>
		/// Provides access to the name of the table
		/// </summary>
		public static readonly string TABLE_NAME = "CompanyVersionScript";

		/// <summary>
		/// Provides access to the name of the database
		/// </summary>
		public static readonly string DATABASE_NAME = "SkyStemARTCore";

		/// <summary>
		/// Constructor
		/// </summary>
        public CompanyVersionScriptDAOBase() : 
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "CompanyVersionScript", DbConstants.ConnectionString) 
        {
        }
        
        /// <summary>
        /// Maps the IDataReader values to a CompanyVersionScriptInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>CompanyVersionScriptInfo</returns>
        protected override CompanyVersionScriptInfo MapObject(System.Data.IDataReader r) 
        {
            CompanyVersionScriptInfo entity = new CompanyVersionScriptInfo();
			entity.CompanyVersionScriptID = r.GetInt64Value("CompanyVersionScriptID");
			entity.CompanyID = r.GetInt32Value("CompanyID");
			entity.VersionScriptID = r.GetInt64Value("VersionScriptID");
			entity.ReleaseStatusID = r.GetInt16Value("ReleaseStatusID");
			entity.ErrorMsg = r.GetStringValue("ErrorMsg");
			entity.IsActive = r.GetBooleanValue("IsActive");
			entity.DateAdded = r.GetDateValue("DateAdded");
			entity.AddedBy = r.GetStringValue("AddedBy");
			entity.DateRevised = r.GetDateValue("DateRevised");
			entity.RevisedBy = r.GetStringValue("RevisedBy");
			entity.HostName = r.GetStringValue("HostName");
            entity.CompanyName = r.GetStringValue("CompanyName");
            entity.VersionNumber = r.GetStringValue("VersionNumber");
            entity.ReleaseStatus = r.GetStringValue("ReleaseStatus");
            entity.ScriptName = r.GetStringValue("ScriptName");
            entity.ScriptPath = r.GetStringValue("ScriptPath");
            return entity;
        }
 
		/// <summary>
		/// Creates the sql insert command, using the values from the passed
		/// in CompanyVersionScriptInfo object
		/// </summary>
		/// <param name="o">A CompanyVersionScriptInfo object, from which the insert values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(CompanyVersionScriptInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_CompanyVersionScript");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
			parAddedBy.ParameterName = "@AddedBy";
			if(entity != null)
				parAddedBy.Value = entity.AddedBy;
			else
				parAddedBy.Value = System.DBNull.Value;
			cmdParams.Add(parAddedBy);
			System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
			parDateAdded.ParameterName = "@DateAdded";
            if (entity != null && entity.DateAdded.HasValue)
				parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
			else
				parDateAdded.Value = System.DBNull.Value;
			cmdParams.Add(parDateAdded);
			System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
			parDateRevised.ParameterName = "@DateRevised";
            if (entity != null && entity.DateRevised.HasValue)
				parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
			else
				parDateRevised.Value = System.DBNull.Value;
			cmdParams.Add(parDateRevised);
			System.Data.IDbDataParameter parErrorMsg = cmd.CreateParameter();
			parErrorMsg.ParameterName = "@ErrorMsg";
			if(entity != null)
				parErrorMsg.Value = entity.ErrorMsg;
			else
				parErrorMsg.Value = System.DBNull.Value;
			cmdParams.Add(parErrorMsg);
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
			System.Data.IDbDataParameter parReleaseStatusID = cmd.CreateParameter();
			parReleaseStatusID.ParameterName = "@ReleaseStatusID";
			if(entity != null)
				parReleaseStatusID.Value = entity.ReleaseStatusID;
			else
				parReleaseStatusID.Value = System.DBNull.Value;
			cmdParams.Add(parReleaseStatusID);
			System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
			parRevisedBy.ParameterName = "@RevisedBy";
			if(entity != null)
				parRevisedBy.Value = entity.RevisedBy;
			else
				parRevisedBy.Value = System.DBNull.Value;
			cmdParams.Add(parRevisedBy);
			System.Data.IDbDataParameter parServerCompanyID = cmd.CreateParameter();
			parServerCompanyID.ParameterName = "@CompanyID";
			if(entity != null)
				parServerCompanyID.Value = entity.CompanyID;
			else
				parServerCompanyID.Value = System.DBNull.Value;
			cmdParams.Add(parServerCompanyID);
			System.Data.IDbDataParameter parVersionScriptID = cmd.CreateParameter();
			parVersionScriptID.ParameterName = "@VersionScriptID";
			if(entity != null)
				parVersionScriptID.Value = entity.VersionScriptID;
			else
				parVersionScriptID.Value = System.DBNull.Value;
			cmdParams.Add(parVersionScriptID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql update command, using the values from the passed
		/// in CompanyVersionScriptInfo object
		/// </summary>
		/// <param name="o">A CompanyVersionScriptInfo object, from which the update values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(CompanyVersionScriptInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_CompanyVersionScript");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
			parAddedBy.ParameterName = "@AddedBy";
			if(entity != null)
				parAddedBy.Value = entity.AddedBy;
			else
				parAddedBy.Value = System.DBNull.Value;
			cmdParams.Add(parAddedBy);
			System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
			parDateAdded.ParameterName = "@DateAdded";
            if (entity != null && entity.DateAdded.HasValue)
				parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
			else
				parDateAdded.Value = System.DBNull.Value;
			cmdParams.Add(parDateAdded);
			System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
			parDateRevised.ParameterName = "@DateRevised";
            if (entity != null && entity.DateRevised.HasValue)
				parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
			else
				parDateRevised.Value = System.DBNull.Value;
			cmdParams.Add(parDateRevised);
			System.Data.IDbDataParameter parErrorMsg = cmd.CreateParameter();
			parErrorMsg.ParameterName = "@ErrorMsg";
			if(entity != null)
				parErrorMsg.Value = entity.ErrorMsg;
			else
				parErrorMsg.Value = System.DBNull.Value;
			cmdParams.Add(parErrorMsg);
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
			System.Data.IDbDataParameter parReleaseStatusID = cmd.CreateParameter();
			parReleaseStatusID.ParameterName = "@ReleaseStatusID";
			if(entity != null)
				parReleaseStatusID.Value = entity.ReleaseStatusID;
			else
				parReleaseStatusID.Value = System.DBNull.Value;
			cmdParams.Add(parReleaseStatusID);
			System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
			parRevisedBy.ParameterName = "@RevisedBy";
			if(entity != null)
				parRevisedBy.Value = entity.RevisedBy;
			else
				parRevisedBy.Value = System.DBNull.Value;
			cmdParams.Add(parRevisedBy);
			System.Data.IDbDataParameter parServerCompanyID = cmd.CreateParameter();
			parServerCompanyID.ParameterName = "@CompanyID";
			if(entity != null)
				parServerCompanyID.Value = entity.CompanyID;
			else
				parServerCompanyID.Value = System.DBNull.Value;
			cmdParams.Add(parServerCompanyID);
			System.Data.IDbDataParameter parVersionScriptID = cmd.CreateParameter();
			parVersionScriptID.ParameterName = "@VersionScriptID";
			if(entity != null)
				parVersionScriptID.Value = entity.VersionScriptID;
			else
				parVersionScriptID.Value = System.DBNull.Value;
			cmdParams.Add(parVersionScriptID);
			System.Data.IDbDataParameter pkparCompanyVersionScriptID = cmd.CreateParameter();
			pkparCompanyVersionScriptID.ParameterName = "@CompanyVersionScriptID";
			pkparCompanyVersionScriptID.Value = entity.CompanyVersionScriptID;
			cmdParams.Add(pkparCompanyVersionScriptID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql delete command, using the passed in primary key
		/// </summary>
		/// <param name="id">The primary key of the object to delete</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_CompanyVersionScript");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@CompanyVersionScriptID";
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
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_CompanyVersionScript");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@CompanyVersionScriptID";
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
		public IList<CompanyVersionScriptInfo> SelectAllByServerCompanyID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CompanyVersionScriptByServerCompanyID");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@CompanyID";
			par.Value = id;
			cmdParams.Add(par);
            return this.Select(cmd);
		}

		/// <summary>
		/// Creates the sql select command, using the passed in foreign key.  This will return an
		/// IList of all objects that have that foreign key.
		/// </summary>
		/// <param name="o">The foreign key of the objects to select</param>
		/// <returns>An IList</returns>
		public IList<CompanyVersionScriptInfo> SelectAllByVersionScriptID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CompanyVersionScriptByVersionScriptID");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@VersionScriptID";
			par.Value = id;
			cmdParams.Add(par);
            return this.Select(cmd);
		}

		/// <summary>
		/// Creates the sql select command, using the passed in foreign key.  This will return an
		/// IList of all objects that have that foreign key.
		/// </summary>
		/// <param name="o">The foreign key of the objects to select</param>
		/// <returns>An IList</returns>
		public IList<CompanyVersionScriptInfo> SelectAllByReleaseStatusID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CompanyVersionScriptByReleaseStatusID");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@ReleaseStatusID";
			par.Value = id;
			cmdParams.Add(par);
            return this.Select(cmd);
		}
		protected override void CustomSave(CompanyVersionScriptInfo o, IDbConnection connection)
		{
			string query = QueryHelper.GetSqlServerLastInsertedCommand(CompanyVersionScriptDAO.TABLE_NAME);
			IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
			cmd.CommandText = query;
			cmd.Connection = connection;
			object id = cmd.ExecuteScalar();
			this.MapIdentity(o, id);
		}
		
		protected override void CustomSave(CompanyVersionScriptInfo o, IDbConnection connection, IDbTransaction transaction)
		{
			string query = QueryHelper.GetSqlServerLastInsertedCommand(CompanyVersionScriptDAO.TABLE_NAME);
			IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
			cmd.CommandText = query;
			cmd.Transaction = transaction;
			cmd.Connection = connection;
			object id = cmd.ExecuteScalar();
			this.MapIdentity(o, id);
		}		
		private void MapIdentity(CompanyVersionScriptInfo entity, object id)
		{
			entity.CompanyVersionScriptID = Convert.ToInt64(id);
		}
	
    }
}
