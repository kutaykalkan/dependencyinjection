
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO.Base 
{
    public abstract class TaskCompletionStatusMstDAOBase : CustomAbstractDAO<TaskCompletionStatusMstInfo>// AbstractDAO<TaskCompletionStatusMstInfo> 
	{
		/// <summary>
		/// A static representation of column AddedBy
		/// </summary>
		public static readonly string COLUMN_ADDEDBY = "AddedBy";
		/// <summary>
		/// A static representation of column CompletionStatus
		/// </summary>
		public static readonly string COLUMN_COMPLETIONSTATUS = "CompletionStatus";
		/// <summary>
		/// A static representation of column CompletionStatusLabelID
		/// </summary>
		public static readonly string COLUMN_COMPLETIONSTATUSLABELID = "CompletionStatusLabelID";
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
		/// A static representation of column TaskCompletionStatusID
		/// </summary>
		public static readonly string COLUMN_TASKCOMPLETIONSTATUSID = "TaskCompletionStatusID";
		/// <summary>
		/// Provides access to the name of the primary key column (TaskCompletionStatusID)
		/// </summary>
		public static readonly string TABLE_PRIMARYKEY = "TaskCompletionStatusID";

		/// <summary>
		/// Provides access to the name of the table
		/// </summary>
		public static readonly string TABLE_NAME = "TaskCompletionStatusMst";

		/// <summary>
		/// Provides access to the name of the database
		/// </summary>
		public static readonly string DATABASE_NAME = "SkyStemART";

        /// <summary>
        ///  CurrentAppUserInfo  for further use
        /// </summary>
        public AppUserInfo CurrentAppUserInfo { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
        public TaskCompletionStatusMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "TaskCompletionStatusMst", oAppUserInfo.ConnectionString) 
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }
        
        /// <summary>
        /// Maps the IDataReader values to a TaskCompletionStatusMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>TaskCompletionStatusMstInfo</returns>
        protected override TaskCompletionStatusMstInfo MapObject(System.Data.IDataReader r) 
        {
            TaskCompletionStatusMstInfo entity = new TaskCompletionStatusMstInfo();
			entity.TaskCompletionStatusID = r.GetInt16Value("TaskCompletionStatusID");
			entity.CompletionStatus = r.GetStringValue("CompletionStatus");
			entity.CompletionStatusLabelID = r.GetInt32Value("CompletionStatusLabelID");
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
		/// in TaskCompletionStatusMstInfo object
		/// </summary>
		/// <param name="o">A TaskCompletionStatusMstInfo object, from which the insert values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(TaskCompletionStatusMstInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_TaskCompletionStatusMst");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
			parAddedBy.ParameterName = "@AddedBy";
			if(entity != null)
				parAddedBy.Value = entity.AddedBy;
			else
				parAddedBy.Value = System.DBNull.Value;
			cmdParams.Add(parAddedBy);
			System.Data.IDbDataParameter parCompletionStatus = cmd.CreateParameter();
			parCompletionStatus.ParameterName = "@CompletionStatus";
			if(entity != null)
				parCompletionStatus.Value = entity.CompletionStatus;
			else
				parCompletionStatus.Value = System.DBNull.Value;
			cmdParams.Add(parCompletionStatus);
			System.Data.IDbDataParameter parCompletionStatusLabelID = cmd.CreateParameter();
			parCompletionStatusLabelID.ParameterName = "@CompletionStatusLabelID";
			if(entity != null)
				parCompletionStatusLabelID.Value = entity.CompletionStatusLabelID;
			else
				parCompletionStatusLabelID.Value = System.DBNull.Value;
			cmdParams.Add(parCompletionStatusLabelID);
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
			System.Data.IDbDataParameter parTaskCompletionStatusID = cmd.CreateParameter();
			parTaskCompletionStatusID.ParameterName = "@TaskCompletionStatusID";
			if(entity != null)
				parTaskCompletionStatusID.Value = entity.TaskCompletionStatusID;
			else
				parTaskCompletionStatusID.Value = System.DBNull.Value;
			cmdParams.Add(parTaskCompletionStatusID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql update command, using the values from the passed
		/// in TaskCompletionStatusMstInfo object
		/// </summary>
		/// <param name="o">A TaskCompletionStatusMstInfo object, from which the update values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(TaskCompletionStatusMstInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_TaskCompletionStatusMst");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
			parAddedBy.ParameterName = "@AddedBy";
			if(entity != null)
				parAddedBy.Value = entity.AddedBy;
			else
				parAddedBy.Value = System.DBNull.Value;
			cmdParams.Add(parAddedBy);
			System.Data.IDbDataParameter parCompletionStatus = cmd.CreateParameter();
			parCompletionStatus.ParameterName = "@CompletionStatus";
			if(entity != null)
				parCompletionStatus.Value = entity.CompletionStatus;
			else
				parCompletionStatus.Value = System.DBNull.Value;
			cmdParams.Add(parCompletionStatus);
			System.Data.IDbDataParameter parCompletionStatusLabelID = cmd.CreateParameter();
			parCompletionStatusLabelID.ParameterName = "@CompletionStatusLabelID";
			if(entity != null)
				parCompletionStatusLabelID.Value = entity.CompletionStatusLabelID;
			else
				parCompletionStatusLabelID.Value = System.DBNull.Value;
			cmdParams.Add(parCompletionStatusLabelID);
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
			System.Data.IDbDataParameter pkparTaskCompletionStatusID = cmd.CreateParameter();
			pkparTaskCompletionStatusID.ParameterName = "@TaskCompletionStatusID";
			pkparTaskCompletionStatusID.Value = entity.TaskCompletionStatusID;
			cmdParams.Add(pkparTaskCompletionStatusID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql delete command, using the passed in primary key
		/// </summary>
		/// <param name="id">The primary key of the object to delete</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_TaskCompletionStatusMst");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@TaskCompletionStatusID";
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
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_TaskCompletionStatusMst");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@TaskCompletionStatusID";
			par.Value = id;
			cmdParams.Add(par);
            return cmd;
        }

	
    }
}
