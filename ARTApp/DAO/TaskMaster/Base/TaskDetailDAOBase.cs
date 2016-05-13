
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
    public abstract class TaskDetailDAOBase : CustomAbstractDAO<TaskDetailInfo>//AbstractDAO<TaskDetailInfo> 
	{
		/// <summary>
		/// A static representation of column AccountID
		/// </summary>
		public static readonly string COLUMN_ACCOUNTID = "AccountID";
		/// <summary>
		/// A static representation of column AddedBy
		/// </summary>
		public static readonly string COLUMN_ADDEDBY = "AddedBy";
		/// <summary>
		/// A static representation of column ApprovalDueDate
		/// </summary>
		public static readonly string COLUMN_APPROVALDUEDATE = "ApprovalDueDate";
		/// <summary>
		/// A static representation of column AssigneeDueDate
		/// </summary>
		public static readonly string COLUMN_ASSIGNEEDUEDATE = "AssigneeDueDate";
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
		/// A static representation of column RecPeriodID
		/// </summary>
		public static readonly string COLUMN_RECPERIODID = "RecPeriodID";
		/// <summary>
		/// A static representation of column RevisedBy
		/// </summary>
		public static readonly string COLUMN_REVISEDBY = "RevisedBy";
		/// <summary>
		/// A static representation of column StatusDate
		/// </summary>
		public static readonly string COLUMN_STATUSDATE = "StatusDate";
		/// <summary>
		/// A static representation of column TaskDetailID
		/// </summary>
		public static readonly string COLUMN_TASKDETAILID = "TaskDetailID";
		/// <summary>
		/// A static representation of column TaskDueDate
		/// </summary>
		public static readonly string COLUMN_TASKDUEDATE = "TaskDueDate";
		/// <summary>
		/// A static representation of column TaskID
		/// </summary>
		public static readonly string COLUMN_TASKID = "TaskID";
		/// <summary>
		/// A static representation of column TaskStartDate
		/// </summary>
		public static readonly string COLUMN_TASKSTARTDATE = "TaskStartDate";
		/// <summary>
		/// A static representation of column TaskStatusID
		/// </summary>
		public static readonly string COLUMN_TASKSTATUSID = "TaskStatusID";
		/// <summary>
		/// Provides access to the name of the primary key column (TaskDetailID)
		/// </summary>
		public static readonly string TABLE_PRIMARYKEY = "TaskDetailID";

		/// <summary>
		/// Provides access to the name of the table
		/// </summary>
		public static readonly string TABLE_NAME = "TaskDetail";

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
        public TaskDetailDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "TaskDetail", oAppUserInfo.ConnectionString) 
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }
        
        /// <summary>
        /// Maps the IDataReader values to a TaskDetailInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>TaskDetailInfo</returns>
        protected override TaskDetailInfo MapObject(System.Data.IDataReader r) 
        {
            TaskDetailInfo entity = new TaskDetailInfo();
			entity.TaskDetailID = r.GetInt64Value("TaskDetailID");
			entity.TaskID = r.GetInt64Value("TaskID");
			entity.RecPeriodID = r.GetInt32Value("RecPeriodID");
			entity.AccountID = r.GetInt64Value("AccountID");
			entity.TaskStartDate = r.GetDateValue("TaskStartDate");
			entity.TaskDueDate = r.GetDateValue("TaskDueDate");
			entity.AssigneeDueDate = r.GetDateValue("AssigneeDueDate");
			entity.ApprovalDueDate = r.GetDateValue("ApprovalDueDate");
			entity.TaskStatusID = r.GetInt16Value("TaskStatusID");
			entity.StatusDate = r.GetDateValue("StatusDate");
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
		/// in TaskDetailInfo object
		/// </summary>
		/// <param name="o">A TaskDetailInfo object, from which the insert values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(TaskDetailInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_TaskDetail");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter parAccountID = cmd.CreateParameter();
			parAccountID.ParameterName = "@AccountID";
			if(entity != null)
				parAccountID.Value = entity.AccountID;
			else
				parAccountID.Value = System.DBNull.Value;
			cmdParams.Add(parAccountID);
			System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
			parAddedBy.ParameterName = "@AddedBy";
			if(entity != null)
				parAddedBy.Value = entity.AddedBy;
			else
				parAddedBy.Value = System.DBNull.Value;
			cmdParams.Add(parAddedBy);
			System.Data.IDbDataParameter parApprovalDueDate = cmd.CreateParameter();
			parApprovalDueDate.ParameterName = "@ApprovalDueDate";
			if(entity != null)
				parApprovalDueDate.Value = entity.ApprovalDueDate.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
			else
				parApprovalDueDate.Value = System.DBNull.Value;
			cmdParams.Add(parApprovalDueDate);
			System.Data.IDbDataParameter parAssigneeDueDate = cmd.CreateParameter();
			parAssigneeDueDate.ParameterName = "@AssigneeDueDate";
			if(entity != null)
				parAssigneeDueDate.Value = entity.AssigneeDueDate.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
			else
				parAssigneeDueDate.Value = System.DBNull.Value;
			cmdParams.Add(parAssigneeDueDate);
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
			System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
			parRecPeriodID.ParameterName = "@RecPeriodID";
			if(entity != null)
				parRecPeriodID.Value = entity.RecPeriodID;
			else
				parRecPeriodID.Value = System.DBNull.Value;
			cmdParams.Add(parRecPeriodID);
			System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
			parRevisedBy.ParameterName = "@RevisedBy";
			if(entity != null)
				parRevisedBy.Value = entity.RevisedBy;
			else
				parRevisedBy.Value = System.DBNull.Value;
			cmdParams.Add(parRevisedBy);
			System.Data.IDbDataParameter parStatusDate = cmd.CreateParameter();
			parStatusDate.ParameterName = "@StatusDate";
			if(entity != null)
				parStatusDate.Value = entity.StatusDate.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
			else
				parStatusDate.Value = System.DBNull.Value;
			cmdParams.Add(parStatusDate);
			System.Data.IDbDataParameter parTaskDueDate = cmd.CreateParameter();
			parTaskDueDate.ParameterName = "@TaskDueDate";
			if(entity != null)
				parTaskDueDate.Value = entity.TaskDueDate.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
			else
				parTaskDueDate.Value = System.DBNull.Value;
			cmdParams.Add(parTaskDueDate);
			System.Data.IDbDataParameter parTaskID = cmd.CreateParameter();
			parTaskID.ParameterName = "@TaskID";
			if(entity != null)
				parTaskID.Value = entity.TaskID;
			else
				parTaskID.Value = System.DBNull.Value;
			cmdParams.Add(parTaskID);
			System.Data.IDbDataParameter parTaskStartDate = cmd.CreateParameter();
			parTaskStartDate.ParameterName = "@TaskStartDate";
			if(entity != null)
				parTaskStartDate.Value = entity.TaskStartDate.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
			else
				parTaskStartDate.Value = System.DBNull.Value;
			cmdParams.Add(parTaskStartDate);
			System.Data.IDbDataParameter parTaskStatusID = cmd.CreateParameter();
			parTaskStatusID.ParameterName = "@TaskStatusID";
			if(entity != null)
				parTaskStatusID.Value = entity.TaskStatusID;
			else
				parTaskStatusID.Value = System.DBNull.Value;
			cmdParams.Add(parTaskStatusID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql update command, using the values from the passed
		/// in TaskDetailInfo object
		/// </summary>
		/// <param name="o">A TaskDetailInfo object, from which the update values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(TaskDetailInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_TaskDetail");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter parAccountID = cmd.CreateParameter();
			parAccountID.ParameterName = "@AccountID";
			if(entity != null)
				parAccountID.Value = entity.AccountID;
			else
				parAccountID.Value = System.DBNull.Value;
			cmdParams.Add(parAccountID);
			System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
			parAddedBy.ParameterName = "@AddedBy";
			if(entity != null)
				parAddedBy.Value = entity.AddedBy;
			else
				parAddedBy.Value = System.DBNull.Value;
			cmdParams.Add(parAddedBy);
			System.Data.IDbDataParameter parApprovalDueDate = cmd.CreateParameter();
			parApprovalDueDate.ParameterName = "@ApprovalDueDate";
			if(entity != null)
				parApprovalDueDate.Value = entity.ApprovalDueDate.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
			else
				parApprovalDueDate.Value = System.DBNull.Value;
			cmdParams.Add(parApprovalDueDate);
			System.Data.IDbDataParameter parAssigneeDueDate = cmd.CreateParameter();
			parAssigneeDueDate.ParameterName = "@AssigneeDueDate";
			if(entity != null)
				parAssigneeDueDate.Value = entity.AssigneeDueDate.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
			else
				parAssigneeDueDate.Value = System.DBNull.Value;
			cmdParams.Add(parAssigneeDueDate);
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
			System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
			parRecPeriodID.ParameterName = "@RecPeriodID";
			if(entity != null)
				parRecPeriodID.Value = entity.RecPeriodID;
			else
				parRecPeriodID.Value = System.DBNull.Value;
			cmdParams.Add(parRecPeriodID);
			System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
			parRevisedBy.ParameterName = "@RevisedBy";
			if(entity != null)
				parRevisedBy.Value = entity.RevisedBy;
			else
				parRevisedBy.Value = System.DBNull.Value;
			cmdParams.Add(parRevisedBy);
			System.Data.IDbDataParameter parStatusDate = cmd.CreateParameter();
			parStatusDate.ParameterName = "@StatusDate";
			if(entity != null)
				parStatusDate.Value = entity.StatusDate.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
			else
				parStatusDate.Value = System.DBNull.Value;
			cmdParams.Add(parStatusDate);
			System.Data.IDbDataParameter parTaskDueDate = cmd.CreateParameter();
			parTaskDueDate.ParameterName = "@TaskDueDate";
			if(entity != null)
				parTaskDueDate.Value = entity.TaskDueDate.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
			else
				parTaskDueDate.Value = System.DBNull.Value;
			cmdParams.Add(parTaskDueDate);
			System.Data.IDbDataParameter parTaskID = cmd.CreateParameter();
			parTaskID.ParameterName = "@TaskID";
			if(entity != null)
				parTaskID.Value = entity.TaskID;
			else
				parTaskID.Value = System.DBNull.Value;
			cmdParams.Add(parTaskID);
			System.Data.IDbDataParameter parTaskStartDate = cmd.CreateParameter();
			parTaskStartDate.ParameterName = "@TaskStartDate";
			if(entity != null)
				parTaskStartDate.Value = entity.TaskStartDate.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
			else
				parTaskStartDate.Value = System.DBNull.Value;
			cmdParams.Add(parTaskStartDate);
			System.Data.IDbDataParameter parTaskStatusID = cmd.CreateParameter();
			parTaskStatusID.ParameterName = "@TaskStatusID";
			if(entity != null)
				parTaskStatusID.Value = entity.TaskStatusID;
			else
				parTaskStatusID.Value = System.DBNull.Value;
			cmdParams.Add(parTaskStatusID);
			System.Data.IDbDataParameter pkparTaskDetailID = cmd.CreateParameter();
			pkparTaskDetailID.ParameterName = "@TaskDetailID";
			pkparTaskDetailID.Value = entity.TaskDetailID;
			cmdParams.Add(pkparTaskDetailID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql delete command, using the passed in primary key
		/// </summary>
		/// <param name="id">The primary key of the object to delete</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_TaskDetail");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@TaskDetailID";
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
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_TaskDetail");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@TaskDetailID";
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
		public IList<TaskDetailInfo> SelectAllByTaskID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_TaskDetailByTaskID");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@TaskID";
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
		public IList<TaskDetailInfo> SelectAllByRecPeriodID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_TaskDetailByRecPeriodID");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@RecPeriodID";
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
		public IList<TaskDetailInfo> SelectAllByAccountID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_TaskDetailByAccountID");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@AccountID";
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
		public IList<TaskDetailInfo> SelectAllByTaskStatusID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_TaskDetailByTaskStatusID");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@TaskStatusID";
			par.Value = id;
			cmdParams.Add(par);
            return this.Select(cmd);
		}
		protected override void CustomSave(TaskDetailInfo o, IDbConnection connection)
		{
			string query = QueryHelper.GetSqlServerLastInsertedCommand(TaskDetailDAO.TABLE_NAME);
			IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
			cmd.CommandText = query;
			cmd.Connection = connection;
			object id = cmd.ExecuteScalar();
			this.MapIdentity(o, id);
		}
		
		protected override void CustomSave(TaskDetailInfo o, IDbConnection connection, IDbTransaction transaction)
		{
			string query = QueryHelper.GetSqlServerLastInsertedCommand(TaskDetailDAO.TABLE_NAME);
			IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
			cmd.CommandText = query;
			cmd.Transaction = transaction;
			cmd.Connection = connection;
			object id = cmd.ExecuteScalar();
			this.MapIdentity(o, id);
		}		
		private void MapIdentity(TaskDetailInfo entity, object id)
		{
			entity.TaskDetailID = Convert.ToInt64(id);
		}
	
		/// <summary>
		/// Creates the Entity Collection of table related to present table by n-n relationship
		/// </summary>
		/// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
		/// <returns>A collection of related entities</returns>
		public IList<TaskDetailInfo> SelectTaskDetailDetailsAssociatedToTaskStatusMstByTaskDetailStatus(TaskStatusMstInfo entity)
		{
			return this.SelectTaskDetailDetailsAssociatedToTaskStatusMstByTaskDetailStatus(entity.TaskStatusID);		
		}

		/// <summary>
		/// Creates the Entity Collection of table related to present table by n-n relationship
		/// </summary>
		/// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
		/// <returns>A collection of related entities</returns>
		public IList<TaskDetailInfo> SelectTaskDetailDetailsAssociatedToTaskStatusMstByTaskDetailStatus(object id)
		{							
			System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [TaskDetail] INNER JOIN [TaskDetailStatus] ON [TaskDetail].[TaskDetailID] = [TaskDetailStatus].[TaskDetailID] INNER JOIN [TaskStatusMst] ON [TaskDetailStatus].[TaskStatusID] = [TaskStatusMst].[TaskStatusID]  WHERE  [TaskStatusMst].[TaskStatusID] = @TaskStatusID ");
			IDataParameterCollection cmdParams = cmd.Parameters;
        	System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@TaskStatusID";
			par.Value = id;
			cmdParams.Add(par);
            List<TaskDetailInfo> objTaskDetailEntityColl = new List<TaskDetailInfo>(this.Select(cmd));
            return objTaskDetailEntityColl;
	    }
    }
}
