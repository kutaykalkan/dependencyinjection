
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
	public abstract class ReleaseStatusMstDAOBase : AbstractDAO<ReleaseStatusMstInfo> 
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
		/// A static representation of column ReleaseStatus
		/// </summary>
		public static readonly string COLUMN_RELEASESTATUS = "ReleaseStatus";
		/// <summary>
		/// A static representation of column ReleaseStatusID
		/// </summary>
		public static readonly string COLUMN_RELEASESTATUSID = "ReleaseStatusID";
		/// <summary>
		/// A static representation of column RevisedBy
		/// </summary>
		public static readonly string COLUMN_REVISEDBY = "RevisedBy";
		/// <summary>
		/// Provides access to the name of the primary key column (ReleaseStatusID)
		/// </summary>
		public static readonly string TABLE_PRIMARYKEY = "ReleaseStatusID";

		/// <summary>
		/// Provides access to the name of the table
		/// </summary>
		public static readonly string TABLE_NAME = "ReleaseStatusMst";

		/// <summary>
		/// Provides access to the name of the database
		/// </summary>
		public static readonly string DATABASE_NAME = "SkyStemARTCore";

		/// <summary>
		/// Constructor
		/// </summary>
        public ReleaseStatusMstDAOBase() : 
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "ReleaseStatusMst", DbConstants.ConnectionString) 
        {
        }
        
        /// <summary>
        /// Maps the IDataReader values to a ReleaseStatusMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>ReleaseStatusMstInfo</returns>
        protected override ReleaseStatusMstInfo MapObject(System.Data.IDataReader r) 
        {
            ReleaseStatusMstInfo entity = new ReleaseStatusMstInfo();
			entity.ReleaseStatusID = r.GetInt16Value("ReleaseStatusID");
			entity.ReleaseStatus = r.GetStringValue("ReleaseStatus");
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
		/// in ReleaseStatusMstInfo object
		/// </summary>
		/// <param name="o">A ReleaseStatusMstInfo object, from which the insert values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(ReleaseStatusMstInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_ReleaseStatusMst");
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
			System.Data.IDbDataParameter parReleaseStatus = cmd.CreateParameter();
			parReleaseStatus.ParameterName = "@ReleaseStatus";
			if(entity != null)
				parReleaseStatus.Value = entity.ReleaseStatus;
			else
				parReleaseStatus.Value = System.DBNull.Value;
			cmdParams.Add(parReleaseStatus);
			System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
			parRevisedBy.ParameterName = "@RevisedBy";
			if(entity != null)
				parRevisedBy.Value = entity.RevisedBy;
			else
				parRevisedBy.Value = System.DBNull.Value;
			cmdParams.Add(parRevisedBy);
			return cmd;
        }

		/// <summary>
		/// Creates the sql update command, using the values from the passed
		/// in ReleaseStatusMstInfo object
		/// </summary>
		/// <param name="o">A ReleaseStatusMstInfo object, from which the update values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(ReleaseStatusMstInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_ReleaseStatusMst");
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
			System.Data.IDbDataParameter parReleaseStatus = cmd.CreateParameter();
			parReleaseStatus.ParameterName = "@ReleaseStatus";
			if(entity != null)
				parReleaseStatus.Value = entity.ReleaseStatus;
			else
				parReleaseStatus.Value = System.DBNull.Value;
			cmdParams.Add(parReleaseStatus);
			System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
			parRevisedBy.ParameterName = "@RevisedBy";
			if(entity != null)
				parRevisedBy.Value = entity.RevisedBy;
			else
				parRevisedBy.Value = System.DBNull.Value;
			cmdParams.Add(parRevisedBy);
			System.Data.IDbDataParameter pkparReleaseStatusID = cmd.CreateParameter();
			pkparReleaseStatusID.ParameterName = "@ReleaseStatusID";
			pkparReleaseStatusID.Value = entity.ReleaseStatusID;
			cmdParams.Add(pkparReleaseStatusID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql delete command, using the passed in primary key
		/// </summary>
		/// <param name="id">The primary key of the object to delete</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_ReleaseStatusMst");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@ReleaseStatusID";
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
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_ReleaseStatusMst");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@ReleaseStatusID";
			par.Value = id;
			cmdParams.Add(par);
            return cmd;
        }

		protected override void CustomSave(ReleaseStatusMstInfo o, IDbConnection connection)
		{
			string query = QueryHelper.GetSqlServerLastInsertedCommand(ReleaseStatusMstDAO.TABLE_NAME);
			IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
			cmd.CommandText = query;
			cmd.Connection = connection;
			object id = cmd.ExecuteScalar();
			this.MapIdentity(o, id);
		}
		
		protected override void CustomSave(ReleaseStatusMstInfo o, IDbConnection connection, IDbTransaction transaction)
		{
			string query = QueryHelper.GetSqlServerLastInsertedCommand(ReleaseStatusMstDAO.TABLE_NAME);
			IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
			cmd.CommandText = query;
			cmd.Transaction = transaction;
			cmd.Connection = connection;
			object id = cmd.ExecuteScalar();
			this.MapIdentity(o, id);
		}		
		private void MapIdentity(ReleaseStatusMstInfo entity, object id)
		{
			entity.ReleaseStatusID = Convert.ToInt16(id);
		}
	
		/// <summary>
		/// Creates the Entity Collection of table related to present table by n-n relationship
		/// </summary>
		/// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
		/// <returns>A collection of related entities</returns>
		public IList<ReleaseStatusMstInfo> SelectReleaseStatusMstDetailsAssociatedToVersionScriptByCompanyVersionScript(VersionScriptInfo entity)
		{
			return this.SelectReleaseStatusMstDetailsAssociatedToVersionScriptByCompanyVersionScript(entity.VersionScriptID);		
		}

		/// <summary>
		/// Creates the Entity Collection of table related to present table by n-n relationship
		/// </summary>
		/// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
		/// <returns>A collection of related entities</returns>
		public IList<ReleaseStatusMstInfo> SelectReleaseStatusMstDetailsAssociatedToVersionScriptByCompanyVersionScript(object id)
		{							
			System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReleaseStatusMst] INNER JOIN [CompanyVersionScript] ON [ReleaseStatusMst].[ReleaseStatusID] = [CompanyVersionScript].[ReleaseStatusID] INNER JOIN [VersionScript] ON [CompanyVersionScript].[VersionScriptID] = [VersionScript].[VersionScriptID]  WHERE  [VersionScript].[VersionScriptID] = @VersionScriptID ");
			IDataParameterCollection cmdParams = cmd.Parameters;
        	System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@VersionScriptID";
			par.Value = id;
			cmdParams.Add(par);
            List<ReleaseStatusMstInfo> objReleaseStatusMstEntityColl = new List<ReleaseStatusMstInfo>(this.Select(cmd));
            return objReleaseStatusMstEntityColl;
	    }
    }
}
