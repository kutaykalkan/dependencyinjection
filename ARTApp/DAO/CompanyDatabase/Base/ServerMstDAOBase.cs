
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
	public abstract class ServerMstDAOBase : CustomAbstractDAO<ServerMstInfo> 
	{
		/// <summary>
		/// A static representation of column AddedBy
		/// </summary>
		public static readonly string COLUMN_ADDEDBY = "AddedBy";
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
		/// A static representation of column Instance
		/// </summary>
		public static readonly string COLUMN_INSTANCE = "Instance";
		/// <summary>
		/// A static representation of column IsActive
		/// </summary>
		public static readonly string COLUMN_ISACTIVE = "IsActive";
		/// <summary>
		/// A static representation of column IsFull
		/// </summary>
		public static readonly string COLUMN_ISFULL = "IsFull";
		/// <summary>
		/// A static representation of column Password
		/// </summary>
		public static readonly string COLUMN_PASSWORD = "Password";
		/// <summary>
		/// A static representation of column RevisedBy
		/// </summary>
		public static readonly string COLUMN_REVISEDBY = "RevisedBy";
		/// <summary>
		/// A static representation of column ServerID
		/// </summary>
		public static readonly string COLUMN_SERVERID = "ServerID";
		/// <summary>
		/// A static representation of column ServerName
		/// </summary>
		public static readonly string COLUMN_SERVERNAME = "ServerName";
		/// <summary>
		/// A static representation of column UserID
		/// </summary>
		public static readonly string COLUMN_USERID = "UserID";
		/// <summary>
		/// Provides access to the name of the primary key column (ServerID)
		/// </summary>
		public static readonly string TABLE_PRIMARYKEY = "ServerID";

		/// <summary>
		/// Provides access to the name of the table
		/// </summary>
		public static readonly string TABLE_NAME = "ServerMst";

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
        public ServerMstDAOBase(AppUserInfo oAppUserInfo) : 
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "ServerMst", DbConstants.ConnectionStringCore) 
        {
            CurrentAppUserInfo = oAppUserInfo;
        }
        
        /// <summary>
        /// Maps the IDataReader values to a ServerMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>ServerMstInfo</returns>
        protected override ServerMstInfo MapObject(System.Data.IDataReader r) 
        {
            ServerMstInfo entity = new ServerMstInfo();
			entity.ServerID = r.GetInt16Value("ServerID");
			entity.ServerName = r.GetStringValue("ServerName");
			entity.Instance = r.GetStringValue("Instance");
			entity.UserID = r.GetStringValue("UserID");
			entity.Password = r.GetStringValue("Password");
			entity.IsFull = r.GetBooleanValue("IsFull");
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
		/// in ServerMstInfo object
		/// </summary>
		/// <param name="o">A ServerMstInfo object, from which the insert values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(ServerMstInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_ServerMst");
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
			System.Data.IDbDataParameter parInstance = cmd.CreateParameter();
			parInstance.ParameterName = "@Instance";
			if(entity != null)
				parInstance.Value = entity.Instance;
			else
				parInstance.Value = System.DBNull.Value;
			cmdParams.Add(parInstance);
			System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
			parIsActive.ParameterName = "@IsActive";
			if(entity != null)
				parIsActive.Value = entity.IsActive;
			else
				parIsActive.Value = System.DBNull.Value;
			cmdParams.Add(parIsActive);
			System.Data.IDbDataParameter parIsFull = cmd.CreateParameter();
			parIsFull.ParameterName = "@IsFull";
			if(entity != null)
				parIsFull.Value = entity.IsFull;
			else
				parIsFull.Value = System.DBNull.Value;
			cmdParams.Add(parIsFull);
			System.Data.IDbDataParameter parPassword = cmd.CreateParameter();
			parPassword.ParameterName = "@Password";
			if(entity != null)
				parPassword.Value = entity.Password;
			else
				parPassword.Value = System.DBNull.Value;
			cmdParams.Add(parPassword);
			System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
			parRevisedBy.ParameterName = "@RevisedBy";
			if(entity != null)
				parRevisedBy.Value = entity.RevisedBy;
			else
				parRevisedBy.Value = System.DBNull.Value;
			cmdParams.Add(parRevisedBy);
			System.Data.IDbDataParameter parServerName = cmd.CreateParameter();
			parServerName.ParameterName = "@ServerName";
			if(entity != null)
				parServerName.Value = entity.ServerName;
			else
				parServerName.Value = System.DBNull.Value;
			cmdParams.Add(parServerName);
			System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
			parUserID.ParameterName = "@UserID";
			if(entity != null)
				parUserID.Value = entity.UserID;
			else
				parUserID.Value = System.DBNull.Value;
			cmdParams.Add(parUserID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql update command, using the values from the passed
		/// in ServerMstInfo object
		/// </summary>
		/// <param name="o">A ServerMstInfo object, from which the update values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(ServerMstInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_ServerMst");
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
			System.Data.IDbDataParameter parInstance = cmd.CreateParameter();
			parInstance.ParameterName = "@Instance";
			if(entity != null)
				parInstance.Value = entity.Instance;
			else
				parInstance.Value = System.DBNull.Value;
			cmdParams.Add(parInstance);
			System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
			parIsActive.ParameterName = "@IsActive";
			if(entity != null)
				parIsActive.Value = entity.IsActive;
			else
				parIsActive.Value = System.DBNull.Value;
			cmdParams.Add(parIsActive);
			System.Data.IDbDataParameter parIsFull = cmd.CreateParameter();
			parIsFull.ParameterName = "@IsFull";
			if(entity != null)
				parIsFull.Value = entity.IsFull;
			else
				parIsFull.Value = System.DBNull.Value;
			cmdParams.Add(parIsFull);
			System.Data.IDbDataParameter parPassword = cmd.CreateParameter();
			parPassword.ParameterName = "@Password";
			if(entity != null)
				parPassword.Value = entity.Password;
			else
				parPassword.Value = System.DBNull.Value;
			cmdParams.Add(parPassword);
			System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
			parRevisedBy.ParameterName = "@RevisedBy";
			if(entity != null)
				parRevisedBy.Value = entity.RevisedBy;
			else
				parRevisedBy.Value = System.DBNull.Value;
			cmdParams.Add(parRevisedBy);
			System.Data.IDbDataParameter parServerName = cmd.CreateParameter();
			parServerName.ParameterName = "@ServerName";
			if(entity != null)
				parServerName.Value = entity.ServerName;
			else
				parServerName.Value = System.DBNull.Value;
			cmdParams.Add(parServerName);
			System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
			parUserID.ParameterName = "@UserID";
			if(entity != null)
				parUserID.Value = entity.UserID;
			else
				parUserID.Value = System.DBNull.Value;
			cmdParams.Add(parUserID);
			System.Data.IDbDataParameter pkparServerID = cmd.CreateParameter();
			pkparServerID.ParameterName = "@ServerID";
			pkparServerID.Value = entity.ServerID;
			cmdParams.Add(pkparServerID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql delete command, using the passed in primary key
		/// </summary>
		/// <param name="id">The primary key of the object to delete</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_ServerMst");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@ServerID";
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
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_ServerMst");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@ServerID";
			par.Value = id;
			cmdParams.Add(par);
            return cmd;
        }

		protected override void CustomSave(ServerMstInfo o, IDbConnection connection)
		{
			string query = QueryHelper.GetSqlServerLastInsertedCommand(ServerMstDAO.TABLE_NAME);
			IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
			cmd.CommandText = query;
			cmd.Connection = connection;
			object id = cmd.ExecuteScalar();
			this.MapIdentity(o, id);
		}
		
		protected override void CustomSave(ServerMstInfo o, IDbConnection connection, IDbTransaction transaction)
		{
			string query = QueryHelper.GetSqlServerLastInsertedCommand(ServerMstDAO.TABLE_NAME);
			IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
			cmd.CommandText = query;
			cmd.Transaction = transaction;
			cmd.Connection = connection;
			object id = cmd.ExecuteScalar();
			this.MapIdentity(o, id);
		}		
		private void MapIdentity(ServerMstInfo entity, object id)
		{
			entity.ServerID = Convert.ToInt16(id);
		}
	
    }
}
