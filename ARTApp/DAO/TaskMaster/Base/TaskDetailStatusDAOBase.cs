
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
    public abstract class TaskDetailStatusDAOBase : CustomAbstractDAO<TaskDetailStatusInfo>// AbstractDAO<TaskDetailStatusInfo> 
	{
		/// <summary>
		/// A static representation of column AddedByUserID
		/// </summary>
		public static readonly string COLUMN_ADDEDBYUSERID = "AddedByUserID";
		/// <summary>
		/// A static representation of column TaskDetailID
		/// </summary>
		public static readonly string COLUMN_TASKDETAILID = "TaskDetailID";
		/// <summary>
		/// A static representation of column TaskDetailStatusID
		/// </summary>
		public static readonly string COLUMN_TASKDETAILSTATUSID = "TaskDetailStatusID";
		/// <summary>
		/// A static representation of column TaskStatusDate
		/// </summary>
		public static readonly string COLUMN_TASKSTATUSDATE = "TaskStatusDate";
		/// <summary>
		/// A static representation of column TaskStatusID
		/// </summary>
		public static readonly string COLUMN_TASKSTATUSID = "TaskStatusID";
		/// <summary>
		/// Provides access to the name of the primary key column (TaskDetailStatusID)
		/// </summary>
		public static readonly string TABLE_PRIMARYKEY = "TaskDetailStatusID";

		/// <summary>
		/// Provides access to the name of the table
		/// </summary>
		public static readonly string TABLE_NAME = "TaskDetailStatus";

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
        public TaskDetailStatusDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "TaskDetailStatus", oAppUserInfo.ConnectionString) 
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }
        
        /// <summary>
        /// Maps the IDataReader values to a TaskDetailStatusInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>TaskDetailStatusInfo</returns>
        protected override TaskDetailStatusInfo MapObject(System.Data.IDataReader r) 
        {
            TaskDetailStatusInfo entity = new TaskDetailStatusInfo();
			entity.TaskDetailStatusID = r.GetInt64Value("TaskDetailStatusID");
			entity.TaskDetailID = r.GetInt64Value("TaskDetailID");
			entity.TaskStatusID = r.GetInt16Value("TaskStatusID");
			entity.TaskStatusDate = r.GetDateValue("TaskStatusDate");
			entity.AddedByUserID = r.GetInt32Value("AddedByUserID");
            return entity;
        }
 
		/// <summary>
		/// Creates the sql insert command, using the values from the passed
		/// in TaskDetailStatusInfo object
		/// </summary>
		/// <param name="o">A TaskDetailStatusInfo object, from which the insert values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(TaskDetailStatusInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_TaskDetailStatus");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter parAddedByUserID = cmd.CreateParameter();
			parAddedByUserID.ParameterName = "@AddedByUserID";
			if(entity != null)
				parAddedByUserID.Value = entity.AddedByUserID;
			else
				parAddedByUserID.Value = System.DBNull.Value;
			cmdParams.Add(parAddedByUserID);
			System.Data.IDbDataParameter parTaskDetailID = cmd.CreateParameter();
			parTaskDetailID.ParameterName = "@TaskDetailID";
			if(entity != null)
				parTaskDetailID.Value = entity.TaskDetailID;
			else
				parTaskDetailID.Value = System.DBNull.Value;
			cmdParams.Add(parTaskDetailID);
			System.Data.IDbDataParameter parTaskStatusDate = cmd.CreateParameter();
			parTaskStatusDate.ParameterName = "@TaskStatusDate";
			if(entity != null)
				parTaskStatusDate.Value = entity.TaskStatusDate.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
			else
				parTaskStatusDate.Value = System.DBNull.Value;
			cmdParams.Add(parTaskStatusDate);
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
		/// in TaskDetailStatusInfo object
		/// </summary>
		/// <param name="o">A TaskDetailStatusInfo object, from which the update values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(TaskDetailStatusInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_TaskDetailStatus");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter parAddedByUserID = cmd.CreateParameter();
			parAddedByUserID.ParameterName = "@AddedByUserID";
			if(entity != null)
				parAddedByUserID.Value = entity.AddedByUserID;
			else
				parAddedByUserID.Value = System.DBNull.Value;
			cmdParams.Add(parAddedByUserID);
			System.Data.IDbDataParameter parTaskDetailID = cmd.CreateParameter();
			parTaskDetailID.ParameterName = "@TaskDetailID";
			if(entity != null)
				parTaskDetailID.Value = entity.TaskDetailID;
			else
				parTaskDetailID.Value = System.DBNull.Value;
			cmdParams.Add(parTaskDetailID);
			System.Data.IDbDataParameter parTaskStatusDate = cmd.CreateParameter();
			parTaskStatusDate.ParameterName = "@TaskStatusDate";
			if(entity != null)
				parTaskStatusDate.Value = entity.TaskStatusDate.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
			else
				parTaskStatusDate.Value = System.DBNull.Value;
			cmdParams.Add(parTaskStatusDate);
			System.Data.IDbDataParameter parTaskStatusID = cmd.CreateParameter();
			parTaskStatusID.ParameterName = "@TaskStatusID";
			if(entity != null)
				parTaskStatusID.Value = entity.TaskStatusID;
			else
				parTaskStatusID.Value = System.DBNull.Value;
			cmdParams.Add(parTaskStatusID);
			System.Data.IDbDataParameter pkparTaskDetailStatusID = cmd.CreateParameter();
			pkparTaskDetailStatusID.ParameterName = "@TaskDetailStatusID";
			pkparTaskDetailStatusID.Value = entity.TaskDetailStatusID;
			cmdParams.Add(pkparTaskDetailStatusID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql delete command, using the passed in primary key
		/// </summary>
		/// <param name="id">The primary key of the object to delete</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_TaskDetailStatus");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@TaskDetailStatusID";
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
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_TaskDetailStatus");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@TaskDetailStatusID";
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
		public IList<TaskDetailStatusInfo> SelectAllByTaskDetailID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_TaskDetailStatusByTaskDetailID");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@TaskDetailID";
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
		public IList<TaskDetailStatusInfo> SelectAllByTaskStatusID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_TaskDetailStatusByTaskStatusID");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@TaskStatusID";
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
		public IList<TaskDetailStatusInfo> SelectAllByAddedByUserID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_TaskDetailStatusByAddedByUserID");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@AddedByUserID";
			par.Value = id;
			cmdParams.Add(par);
            return this.Select(cmd);
		}
		protected override void CustomSave(TaskDetailStatusInfo o, IDbConnection connection)
		{
			string query = QueryHelper.GetSqlServerLastInsertedCommand(TaskDetailStatusDAO.TABLE_NAME);
			IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
			cmd.CommandText = query;
			cmd.Connection = connection;
			object id = cmd.ExecuteScalar();
			this.MapIdentity(o, id);
		}
		
		protected override void CustomSave(TaskDetailStatusInfo o, IDbConnection connection, IDbTransaction transaction)
		{
			string query = QueryHelper.GetSqlServerLastInsertedCommand(TaskDetailStatusDAO.TABLE_NAME);
			IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
			cmd.CommandText = query;
			cmd.Transaction = transaction;
			cmd.Connection = connection;
			object id = cmd.ExecuteScalar();
			this.MapIdentity(o, id);
		}		
		private void MapIdentity(TaskDetailStatusInfo entity, object id)
		{
			entity.TaskDetailStatusID = Convert.ToInt64(id);
		}
	
    }
}
