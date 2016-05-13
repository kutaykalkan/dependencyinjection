
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
	public abstract class VersionScriptDAOBase : AbstractDAO<VersionScriptInfo> 
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
		/// A static representation of column IsActive
		/// </summary>
		public static readonly string COLUMN_ISACTIVE = "IsActive";
		/// <summary>
		/// A static representation of column RevisedBy
		/// </summary>
		public static readonly string COLUMN_REVISEDBY = "RevisedBy";
		/// <summary>
		/// A static representation of column ScriptName
		/// </summary>
		public static readonly string COLUMN_SCRIPTNAME = "ScriptName";
		/// <summary>
		/// A static representation of column ScriptOrder
		/// </summary>
		public static readonly string COLUMN_SCRIPTORDER = "ScriptOrder";
		/// <summary>
		/// A static representation of column ScriptPath
		/// </summary>
		public static readonly string COLUMN_SCRIPTPATH = "ScriptPath";
		/// <summary>
		/// A static representation of column VersionID
		/// </summary>
		public static readonly string COLUMN_VERSIONID = "VersionID";
		/// <summary>
		/// A static representation of column VersionScriptID
		/// </summary>
		public static readonly string COLUMN_VERSIONSCRIPTID = "VersionScriptID";
		/// <summary>
		/// Provides access to the name of the primary key column (VersionScriptID)
		/// </summary>
		public static readonly string TABLE_PRIMARYKEY = "VersionScriptID";

		/// <summary>
		/// Provides access to the name of the table
		/// </summary>
		public static readonly string TABLE_NAME = "VersionScript";

		/// <summary>
		/// Provides access to the name of the database
		/// </summary>
		public static readonly string DATABASE_NAME = "SkyStemARTCore";

		/// <summary>
		/// Constructor
		/// </summary>
        public VersionScriptDAOBase() : 
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "VersionScript", DbConstants.ConnectionString) 
        {
        }
        
        /// <summary>
        /// Maps the IDataReader values to a VersionScriptInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>VersionScriptInfo</returns>
        protected override VersionScriptInfo MapObject(System.Data.IDataReader r) 
        {
            VersionScriptInfo entity = new VersionScriptInfo();
			entity.VersionScriptID = r.GetInt64Value("VersionScriptID");
			entity.VersionID = r.GetInt32Value("VersionID");
			entity.ScriptName = r.GetStringValue("ScriptName");
			entity.ScriptPath = r.GetStringValue("ScriptPath");
			entity.ScriptOrder = r.GetInt16Value("ScriptOrder");
			entity.IsActive = r.GetBooleanValue("IsActive");
			entity.DateAdded = r.GetDateValue("DateAdded");
			entity.AddedBy = r.GetStringValue("AddedBy");
			entity.DateRevised = r.GetDateValue("DateRevised");
			entity.RevisedBy = r.GetStringValue("RevisedBy");
			entity.HostName = r.GetStringValue("HostName");
            if(r.GetBooleanValue("IsVersionScriptExecuted") !=null && r.GetBooleanValue("IsVersionScriptExecuted").HasValue)
            entity.IsVersionScriptExecuted = r.GetBooleanValue("IsVersionScriptExecuted").Value;
            return entity;
        }
 
		/// <summary>
		/// Creates the sql insert command, using the values from the passed
		/// in VersionScriptInfo object
		/// </summary>
		/// <param name="o">A VersionScriptInfo object, from which the insert values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(VersionScriptInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_VersionScript");
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
			System.Data.IDbDataParameter parScriptName = cmd.CreateParameter();
			parScriptName.ParameterName = "@ScriptName";
			if(entity != null)
				parScriptName.Value = entity.ScriptName;
			else
				parScriptName.Value = System.DBNull.Value;
			cmdParams.Add(parScriptName);
			System.Data.IDbDataParameter parScriptOrder = cmd.CreateParameter();
			parScriptOrder.ParameterName = "@ScriptOrder";
			if(entity != null)
				parScriptOrder.Value = entity.ScriptOrder;
			else
				parScriptOrder.Value = System.DBNull.Value;
			cmdParams.Add(parScriptOrder);
			System.Data.IDbDataParameter parScriptPath = cmd.CreateParameter();
			parScriptPath.ParameterName = "@ScriptPath";
			if(entity != null)
				parScriptPath.Value = entity.ScriptPath;
			else
				parScriptPath.Value = System.DBNull.Value;
			cmdParams.Add(parScriptPath);
			System.Data.IDbDataParameter parVersionID = cmd.CreateParameter();
			parVersionID.ParameterName = "@VersionID";
			if(entity != null)
				parVersionID.Value = entity.VersionID;
			else
				parVersionID.Value = System.DBNull.Value;
			cmdParams.Add(parVersionID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql update command, using the values from the passed
		/// in VersionScriptInfo object
		/// </summary>
		/// <param name="o">A VersionScriptInfo object, from which the update values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(VersionScriptInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_VersionScript");
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
			System.Data.IDbDataParameter parScriptName = cmd.CreateParameter();
			parScriptName.ParameterName = "@ScriptName";
			if(entity != null)
				parScriptName.Value = entity.ScriptName;
			else
				parScriptName.Value = System.DBNull.Value;
			cmdParams.Add(parScriptName);
			System.Data.IDbDataParameter parScriptOrder = cmd.CreateParameter();
			parScriptOrder.ParameterName = "@ScriptOrder";
			if(entity != null)
				parScriptOrder.Value = entity.ScriptOrder;
			else
				parScriptOrder.Value = System.DBNull.Value;
			cmdParams.Add(parScriptOrder);
			System.Data.IDbDataParameter parScriptPath = cmd.CreateParameter();
			parScriptPath.ParameterName = "@ScriptPath";
			if(entity != null)
				parScriptPath.Value = entity.ScriptPath;
			else
				parScriptPath.Value = System.DBNull.Value;
			cmdParams.Add(parScriptPath);
			System.Data.IDbDataParameter parVersionID = cmd.CreateParameter();
			parVersionID.ParameterName = "@VersionID";
			if(entity != null)
				parVersionID.Value = entity.VersionID;
			else
				parVersionID.Value = System.DBNull.Value;
			cmdParams.Add(parVersionID);
			System.Data.IDbDataParameter pkparVersionScriptID = cmd.CreateParameter();
			pkparVersionScriptID.ParameterName = "@VersionScriptID";
			pkparVersionScriptID.Value = entity.VersionScriptID;
			cmdParams.Add(pkparVersionScriptID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql delete command, using the passed in primary key
		/// </summary>
		/// <param name="id">The primary key of the object to delete</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_VersionScript");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@VersionScriptID";
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
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_VersionScript");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@VersionScriptID";
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
		public IList<VersionScriptInfo> SelectAllByVersionID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_VersionScriptByVersionID");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@VersionID";
			par.Value = id;
			cmdParams.Add(par);
            return this.Select(cmd);
		}
		protected override void CustomSave(VersionScriptInfo o, IDbConnection connection)
		{
			string query = QueryHelper.GetSqlServerLastInsertedCommand(VersionScriptDAO.TABLE_NAME);
			IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
			cmd.CommandText = query;
			cmd.Connection = connection;
			object id = cmd.ExecuteScalar();
			this.MapIdentity(o, id);
		}
		
		protected override void CustomSave(VersionScriptInfo o, IDbConnection connection, IDbTransaction transaction)
		{
			string query = QueryHelper.GetSqlServerLastInsertedCommand(VersionScriptDAO.TABLE_NAME);
			IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
			cmd.CommandText = query;
			cmd.Transaction = transaction;
			cmd.Connection = connection;
			object id = cmd.ExecuteScalar();
			this.MapIdentity(o, id);
		}		
		private void MapIdentity(VersionScriptInfo entity, object id)
		{
			entity.VersionScriptID = Convert.ToInt64(id);
		}
	
		/// <summary>
		/// Creates the Entity Collection of table related to present table by n-n relationship
		/// </summary>
		/// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
		/// <returns>A collection of related entities</returns>
		public IList<VersionScriptInfo> SelectVersionScriptDetailsAssociatedToReleaseStatusMstByCompanyVersionScript(ReleaseStatusMstInfo entity)
		{
			return this.SelectVersionScriptDetailsAssociatedToReleaseStatusMstByCompanyVersionScript(entity.ReleaseStatusID);		
		}

		/// <summary>
		/// Creates the Entity Collection of table related to present table by n-n relationship
		/// </summary>
		/// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
		/// <returns>A collection of related entities</returns>
		public IList<VersionScriptInfo> SelectVersionScriptDetailsAssociatedToReleaseStatusMstByCompanyVersionScript(object id)
		{							
			System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [VersionScript] INNER JOIN [CompanyVersionScript] ON [VersionScript].[VersionScriptID] = [CompanyVersionScript].[VersionScriptID] INNER JOIN [ReleaseStatusMst] ON [CompanyVersionScript].[ReleaseStatusID] = [ReleaseStatusMst].[ReleaseStatusID]  WHERE  [ReleaseStatusMst].[ReleaseStatusID] = @ReleaseStatusID ");
			IDataParameterCollection cmdParams = cmd.Parameters;
        	System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@ReleaseStatusID";
			par.Value = id;
			cmdParams.Add(par);
            List<VersionScriptInfo> objVersionScriptEntityColl = new List<VersionScriptInfo>(this.Select(cmd));
            return objVersionScriptEntityColl;
	    }
    }
}
