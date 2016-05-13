
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
    public abstract class TaskStatusMstDAOBase : CustomAbstractDAO<TaskStatusMstInfo>// AbstractDAO<TaskStatusMstInfo> 
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
		/// A static representation of column TaskStatus
		/// </summary>
		public static readonly string COLUMN_TASKSTATUS = "TaskStatus";
		/// <summary>
		/// A static representation of column TaskStatusID
		/// </summary>
		public static readonly string COLUMN_TASKSTATUSID = "TaskStatusID";
		/// <summary>
		/// A static representation of column TaskStatusLabelID
		/// </summary>
		public static readonly string COLUMN_TASKSTATUSLABELID = "TaskStatusLabelID";
		/// <summary>
		/// Provides access to the name of the primary key column (TaskStatusID)
		/// </summary>
		public static readonly string TABLE_PRIMARYKEY = "TaskStatusID";

		/// <summary>
		/// Provides access to the name of the table
		/// </summary>
		public static readonly string TABLE_NAME = "[TaskMaster].[TaskStatusMst]";

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
        public TaskStatusMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "TaskStatusMst", oAppUserInfo.ConnectionString) 
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }
        
        /// <summary>
        /// Maps the IDataReader values to a TaskStatusMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>TaskStatusMstInfo</returns>
        protected override TaskStatusMstInfo MapObject(System.Data.IDataReader r) 
        {
            TaskStatusMstInfo entity = new TaskStatusMstInfo();
			entity.TaskStatusID = r.GetInt16Value("TaskStatusID");
			entity.TaskStatus = r.GetStringValue("TaskStatus");
			entity.TaskStatusLabelID = r.GetInt32Value("TaskStatusLabelID");
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
		/// in TaskStatusMstInfo object
		/// </summary>
		/// <param name="o">A TaskStatusMstInfo object, from which the insert values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(TaskStatusMstInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_TaskStatusMst");
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
			System.Data.IDbDataParameter parTaskStatus = cmd.CreateParameter();
			parTaskStatus.ParameterName = "@TaskStatus";
			if(entity != null)
				parTaskStatus.Value = entity.TaskStatus;
			else
				parTaskStatus.Value = System.DBNull.Value;
			cmdParams.Add(parTaskStatus);
			System.Data.IDbDataParameter parTaskStatusID = cmd.CreateParameter();
			parTaskStatusID.ParameterName = "@TaskStatusID";
			if(entity != null)
				parTaskStatusID.Value = entity.TaskStatusID;
			else
				parTaskStatusID.Value = System.DBNull.Value;
			cmdParams.Add(parTaskStatusID);
			System.Data.IDbDataParameter parTaskStatusLabelID = cmd.CreateParameter();
			parTaskStatusLabelID.ParameterName = "@TaskStatusLabelID";
			if(entity != null)
				parTaskStatusLabelID.Value = entity.TaskStatusLabelID;
			else
				parTaskStatusLabelID.Value = System.DBNull.Value;
			cmdParams.Add(parTaskStatusLabelID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql update command, using the values from the passed
		/// in TaskStatusMstInfo object
		/// </summary>
		/// <param name="o">A TaskStatusMstInfo object, from which the update values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(TaskStatusMstInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_TaskStatusMst");
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
			System.Data.IDbDataParameter parTaskStatus = cmd.CreateParameter();
			parTaskStatus.ParameterName = "@TaskStatus";
			if(entity != null)
				parTaskStatus.Value = entity.TaskStatus;
			else
				parTaskStatus.Value = System.DBNull.Value;
			cmdParams.Add(parTaskStatus);
			System.Data.IDbDataParameter parTaskStatusLabelID = cmd.CreateParameter();
			parTaskStatusLabelID.ParameterName = "@TaskStatusLabelID";
			if(entity != null)
				parTaskStatusLabelID.Value = entity.TaskStatusLabelID;
			else
				parTaskStatusLabelID.Value = System.DBNull.Value;
			cmdParams.Add(parTaskStatusLabelID);
			System.Data.IDbDataParameter pkparTaskStatusID = cmd.CreateParameter();
			pkparTaskStatusID.ParameterName = "@TaskStatusID";
			pkparTaskStatusID.Value = entity.TaskStatusID;
			cmdParams.Add(pkparTaskStatusID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql delete command, using the passed in primary key
		/// </summary>
		/// <param name="id">The primary key of the object to delete</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_TaskStatusMst");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@TaskStatusID";
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
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_TaskStatusMst");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@TaskStatusID";
			par.Value = id;
			cmdParams.Add(par);
            return cmd;
        }

	
		/// <summary>
		/// Creates the Entity Collection of table related to present table by n-n relationship
		/// </summary>
		/// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
		/// <returns>A collection of related entities</returns>
		public IList<TaskStatusMstInfo> SelectTaskStatusMstDetailsAssociatedToTaskHdrByTaskDetail(TaskHdrInfo entity)
		{
			return this.SelectTaskStatusMstDetailsAssociatedToTaskHdrByTaskDetail(entity.TaskID);		
		}

		/// <summary>
		/// Creates the Entity Collection of table related to present table by n-n relationship
		/// </summary>
		/// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
		/// <returns>A collection of related entities</returns>
		public IList<TaskStatusMstInfo> SelectTaskStatusMstDetailsAssociatedToTaskHdrByTaskDetail(object id)
		{							
			System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [TaskStatusMst] INNER JOIN [TaskDetail] ON [TaskStatusMst].[TaskStatusID] = [TaskDetail].[TaskStatusID] INNER JOIN [TaskHdr] ON [TaskDetail].[TaskID] = [TaskHdr].[TaskID]  WHERE  [TaskHdr].[TaskID] = @TaskID ");
			IDataParameterCollection cmdParams = cmd.Parameters;
        	System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@TaskID";
			par.Value = id;
			cmdParams.Add(par);
            List<TaskStatusMstInfo> objTaskStatusMstEntityColl = new List<TaskStatusMstInfo>(this.Select(cmd));
            return objTaskStatusMstEntityColl;
	    }
		/// <summary>
		/// Creates the Entity Collection of table related to present table by n-n relationship
		/// </summary>
		/// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
		/// <returns>A collection of related entities</returns>
		public IList<TaskStatusMstInfo> SelectTaskStatusMstDetailsAssociatedToTaskDetailByTaskDetailStatus(TaskDetailInfo entity)
		{
			return this.SelectTaskStatusMstDetailsAssociatedToTaskDetailByTaskDetailStatus(entity.TaskDetailID);		
		}

		/// <summary>
		/// Creates the Entity Collection of table related to present table by n-n relationship
		/// </summary>
		/// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
		/// <returns>A collection of related entities</returns>
		public IList<TaskStatusMstInfo> SelectTaskStatusMstDetailsAssociatedToTaskDetailByTaskDetailStatus(object id)
		{							
			System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [TaskStatusMst] INNER JOIN [TaskDetailStatus] ON [TaskStatusMst].[TaskStatusID] = [TaskDetailStatus].[TaskStatusID] INNER JOIN [TaskDetail] ON [TaskDetailStatus].[TaskDetailID] = [TaskDetail].[TaskDetailID]  WHERE  [TaskDetail].[TaskDetailID] = @TaskDetailID ");
			IDataParameterCollection cmdParams = cmd.Parameters;
        	System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@TaskDetailID";
			par.Value = id;
			cmdParams.Add(par);
            List<TaskStatusMstInfo> objTaskStatusMstEntityColl = new List<TaskStatusMstInfo>(this.Select(cmd));
            return objTaskStatusMstEntityColl;
	    }
    }
}
