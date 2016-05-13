
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.Utility;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model.CompanyDatabase;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO.CompanyDatabase.Base 
{
	public abstract class ServerCompanyDAOBase : CustomAbstractDAO<ServerCompanyInfo> 
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
		/// A static representation of column DatabaseName
		/// </summary>
		public static readonly string COLUMN_DATABASENAME = "DatabaseName";
		/// <summary>
		/// A static representation of column DateAdded
		/// </summary>
		public static readonly string COLUMN_DATEADDED = "DateAdded";
		/// <summary>
		/// A static representation of column DateRevised
		/// </summary>
		public static readonly string COLUMN_DATEREVISED = "DateRevised";
		/// <summary>
		/// A static representation of column HostName
		/// </summary>
		public static readonly string COLUMN_HOSTNAME = "HostName";
		/// <summary>
		/// A static representation of column IsActive
		/// </summary>
		public static readonly string COLUMN_ISACTIVE = "IsActive";
		/// <summary>
		/// A static representation of column RevisedBy
		/// </summary>
		public static readonly string COLUMN_REVISEDBY = "RevisedBy";
		/// <summary>
		/// A static representation of column ServerCompanyID
		/// </summary>
		public static readonly string COLUMN_SERVERCOMPANYID = "ServerCompanyID";
		/// <summary>
		/// A static representation of column ServerID
		/// </summary>
		public static readonly string COLUMN_SERVERID = "ServerID";
		/// <summary>
		/// Provides access to the name of the primary key column (ServerCompanyID)
		/// </summary>
		public static readonly string TABLE_PRIMARYKEY = "ServerCompanyID";

		/// <summary>
		/// Provides access to the name of the table
		/// </summary>
		public static readonly string TABLE_NAME = "ServerCompany";

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
        public ServerCompanyDAOBase(AppUserInfo oAppUserInfo) : 
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "ServerCompany", DbConstants.ConnectionStringCore) 
        {
            CurrentAppUserInfo = oAppUserInfo;
        }
        
        /// <summary>
        /// Maps the IDataReader values to a ServerCompanyInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>ServerCompanyInfo</returns>
        protected override ServerCompanyInfo MapObject(System.Data.IDataReader r) 
        {
            ServerCompanyInfo entity = new ServerCompanyInfo();
			entity.ServerCompanyID = r.GetInt32Value("ServerCompanyID");
			entity.ServerID = r.GetInt16Value("ServerID");
			entity.CompanyID = r.GetInt32Value("CompanyID");
			entity.DatabaseName = r.GetStringValue("DatabaseName");
			entity.IsActive = r.GetBooleanValue("IsActive");
			entity.DateAdded = r.GetDateValue("DateAdded");
			entity.AddedBy = r.GetStringValue("AddedBy");
			entity.DateRevised = r.GetDateValue("DateRevised");
			entity.RevisedBy = r.GetStringValue("RevisedBy");
			entity.HostName = r.GetStringValue("HostName");
            return entity;
        }
 
		/// <summary>
		/// Creates the sql insert command, using the values from the passed
		/// in ServerCompanyInfo object
		/// </summary>
		/// <param name="o">A ServerCompanyInfo object, from which the insert values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(ServerCompanyInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_ServerCompany");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
			parAddedBy.ParameterName = "@AddedBy";
			if(entity != null)
				parAddedBy.Value = entity.AddedBy;
			else
				parAddedBy.Value = System.DBNull.Value;
			cmdParams.Add(parAddedBy);
			System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
			parCompanyID.ParameterName = "@CompanyID";
			if(entity != null)
				parCompanyID.Value = entity.CompanyID;
			else
				parCompanyID.Value = System.DBNull.Value;
			cmdParams.Add(parCompanyID);
			System.Data.IDbDataParameter parDatabaseName = cmd.CreateParameter();
			parDatabaseName.ParameterName = "@DatabaseName";
			if(entity != null)
				parDatabaseName.Value = entity.DatabaseName;
			else
				parDatabaseName.Value = System.DBNull.Value;
			cmdParams.Add(parDatabaseName);
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
			System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
			parRevisedBy.ParameterName = "@RevisedBy";
			if(entity != null)
				parRevisedBy.Value = entity.RevisedBy;
			else
				parRevisedBy.Value = System.DBNull.Value;
			cmdParams.Add(parRevisedBy);
			System.Data.IDbDataParameter parServerID = cmd.CreateParameter();
			parServerID.ParameterName = "@ServerID";
			if(entity != null)
				parServerID.Value = entity.ServerID;
			else
				parServerID.Value = System.DBNull.Value;
			cmdParams.Add(parServerID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql update command, using the values from the passed
		/// in ServerCompanyInfo object
		/// </summary>
		/// <param name="o">A ServerCompanyInfo object, from which the update values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(ServerCompanyInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_ServerCompany");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
			parAddedBy.ParameterName = "@AddedBy";
			if(entity != null)
				parAddedBy.Value = entity.AddedBy;
			else
				parAddedBy.Value = System.DBNull.Value;
			cmdParams.Add(parAddedBy);
			System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
			parCompanyID.ParameterName = "@CompanyID";
			if(entity != null)
				parCompanyID.Value = entity.CompanyID;
			else
				parCompanyID.Value = System.DBNull.Value;
			cmdParams.Add(parCompanyID);
			System.Data.IDbDataParameter parDatabaseName = cmd.CreateParameter();
			parDatabaseName.ParameterName = "@DatabaseName";
			if(entity != null)
				parDatabaseName.Value = entity.DatabaseName;
			else
				parDatabaseName.Value = System.DBNull.Value;
			cmdParams.Add(parDatabaseName);
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
			System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
			parRevisedBy.ParameterName = "@RevisedBy";
			if(entity != null)
				parRevisedBy.Value = entity.RevisedBy;
			else
				parRevisedBy.Value = System.DBNull.Value;
			cmdParams.Add(parRevisedBy);
			System.Data.IDbDataParameter parServerID = cmd.CreateParameter();
			parServerID.ParameterName = "@ServerID";
			if(entity != null)
				parServerID.Value = entity.ServerID;
			else
				parServerID.Value = System.DBNull.Value;
			cmdParams.Add(parServerID);
			System.Data.IDbDataParameter pkparServerCompanyID = cmd.CreateParameter();
			pkparServerCompanyID.ParameterName = "@ServerCompanyID";
			pkparServerCompanyID.Value = entity.ServerCompanyID;
			cmdParams.Add(pkparServerCompanyID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql delete command, using the passed in primary key
		/// </summary>
		/// <param name="id">The primary key of the object to delete</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_ServerCompany");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@ServerCompanyID";
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
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_ServerCompany");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@ServerCompanyID";
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
		public IList<ServerCompanyInfo> SelectAllByServerID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_ServerCompanyByServerID");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@ServerID";
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
		public IList<ServerCompanyInfo> SelectAllByCompanyID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_ServerCompanyByCompanyID");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@CompanyID";
			par.Value = id;
			cmdParams.Add(par);
            return this.Select(cmd);
		}
		protected override void CustomSave(ServerCompanyInfo o, IDbConnection connection)
		{
			string query = QueryHelper.GetSqlServerLastInsertedCommand(ServerCompanyDAO.TABLE_NAME);
			IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
			cmd.CommandText = query;
			cmd.Connection = connection;
			object id = cmd.ExecuteScalar();
			this.MapIdentity(o, id);
		}
		
		protected override void CustomSave(ServerCompanyInfo o, IDbConnection connection, IDbTransaction transaction)
		{
			string query = QueryHelper.GetSqlServerLastInsertedCommand(ServerCompanyDAO.TABLE_NAME);
			IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
			cmd.CommandText = query;
			cmd.Transaction = transaction;
			cmd.Connection = connection;
			object id = cmd.ExecuteScalar();
			this.MapIdentity(o, id);
		}		
		private void MapIdentity(ServerCompanyInfo entity, object id)
		{
			entity.ServerCompanyID = Convert.ToInt32(id);
		}
	
    }
}
