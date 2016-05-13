
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
    public abstract class TaskDetailReviewNoteDetailDAOBase : CustomAbstractDAO<TaskDetailReviewNoteDetailInfo>// AbstractDAO<TaskDetailReviewNoteDetailInfo> 
	{
		/// <summary>
		/// A static representation of column AddedBy
		/// </summary>
		public static readonly string COLUMN_ADDEDBY = "AddedBy";
		/// <summary>
		/// A static representation of column AddedByUserID
		/// </summary>
		public static readonly string COLUMN_ADDEDBYUSERID = "AddedByUserID";
		/// <summary>
		/// A static representation of column DateAdded
		/// </summary>
		public static readonly string COLUMN_DATEADDED = "DateAdded";
		/// <summary>
		/// A static representation of column IsActive
		/// </summary>
		public static readonly string COLUMN_ISACTIVE = "IsActive";
		/// <summary>
		/// A static representation of column ReviewNote
		/// </summary>
		public static readonly string COLUMN_REVIEWNOTE = "ReviewNote";
		/// <summary>
		/// A static representation of column TaskDetailReviewNoteDetailID
		/// </summary>
		public static readonly string COLUMN_TASKDETAILREVIEWNOTEDETAILID = "TaskDetailReviewNoteDetailID";
		/// <summary>
		/// A static representation of column TaskDetailReviewNoteID
		/// </summary>
		public static readonly string COLUMN_TASKDETAILREVIEWNOTEID = "TaskDetailReviewNoteID";
		/// <summary>
		/// Provides access to the name of the primary key column (TaskDetailReviewNoteDetailID)
		/// </summary>
		public static readonly string TABLE_PRIMARYKEY = "TaskDetailReviewNoteDetailID";

		/// <summary>
		/// Provides access to the name of the table
		/// </summary>
		public static readonly string TABLE_NAME = "TaskDetailReviewNoteDetail";

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
        public TaskDetailReviewNoteDetailDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "TaskDetailReviewNoteDetail", oAppUserInfo.ConnectionString) 
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }
        
        /// <summary>
        /// Maps the IDataReader values to a TaskDetailReviewNoteDetailInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>TaskDetailReviewNoteDetailInfo</returns>
        protected override TaskDetailReviewNoteDetailInfo MapObject(System.Data.IDataReader r) 
        {
            TaskDetailReviewNoteDetailInfo entity = new TaskDetailReviewNoteDetailInfo();
			entity.TaskDetailReviewNoteDetailID = r.GetInt32Value("TaskDetailReviewNoteDetailID");
			entity.TaskDetailReviewNoteID = r.GetInt32Value("TaskDetailReviewNoteID");
			entity.ReviewNote = r.GetStringValue("ReviewNote");
			entity.AddedByUserID = r.GetInt32Value("AddedByUserID");
			entity.IsActive = r.GetBooleanValue("IsActive");
			entity.DateAdded = r.GetDateValue("DateAdded");
			entity.AddedBy = r.GetStringValue("AddedBy");
            return entity;
        }
 
		/// <summary>
		/// Creates the sql insert command, using the values from the passed
		/// in TaskDetailReviewNoteDetailInfo object
		/// </summary>
		/// <param name="o">A TaskDetailReviewNoteDetailInfo object, from which the insert values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(TaskDetailReviewNoteDetailInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_TaskDetailReviewNoteDetail");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
			parAddedBy.ParameterName = "@AddedBy";
			if(entity != null)
				parAddedBy.Value = entity.AddedBy;
			else
				parAddedBy.Value = System.DBNull.Value;
			cmdParams.Add(parAddedBy);
			System.Data.IDbDataParameter parAddedByUserID = cmd.CreateParameter();
			parAddedByUserID.ParameterName = "@AddedByUserID";
			if(entity != null)
				parAddedByUserID.Value = entity.AddedByUserID;
			else
				parAddedByUserID.Value = System.DBNull.Value;
			cmdParams.Add(parAddedByUserID);
			System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
			parDateAdded.ParameterName = "@DateAdded";
			if(entity != null)
				parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
			else
				parDateAdded.Value = System.DBNull.Value;
			cmdParams.Add(parDateAdded);
			System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
			parIsActive.ParameterName = "@IsActive";
			if(entity != null)
				parIsActive.Value = entity.IsActive;
			else
				parIsActive.Value = System.DBNull.Value;
			cmdParams.Add(parIsActive);
			System.Data.IDbDataParameter parReviewNote = cmd.CreateParameter();
			parReviewNote.ParameterName = "@ReviewNote";
			if(entity != null)
				parReviewNote.Value = entity.ReviewNote;
			else
				parReviewNote.Value = System.DBNull.Value;
			cmdParams.Add(parReviewNote);
			System.Data.IDbDataParameter parTaskDetailReviewNoteDetailID = cmd.CreateParameter();
			parTaskDetailReviewNoteDetailID.ParameterName = "@TaskDetailReviewNoteDetailID";
			if(entity != null)
				parTaskDetailReviewNoteDetailID.Value = entity.TaskDetailReviewNoteDetailID;
			else
				parTaskDetailReviewNoteDetailID.Value = System.DBNull.Value;
			cmdParams.Add(parTaskDetailReviewNoteDetailID);
			System.Data.IDbDataParameter parTaskDetailReviewNoteID = cmd.CreateParameter();
			parTaskDetailReviewNoteID.ParameterName = "@TaskDetailReviewNoteID";
			if(entity != null)
				parTaskDetailReviewNoteID.Value = entity.TaskDetailReviewNoteID;
			else
				parTaskDetailReviewNoteID.Value = System.DBNull.Value;
			cmdParams.Add(parTaskDetailReviewNoteID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql update command, using the values from the passed
		/// in TaskDetailReviewNoteDetailInfo object
		/// </summary>
		/// <param name="o">A TaskDetailReviewNoteDetailInfo object, from which the update values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(TaskDetailReviewNoteDetailInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_TaskDetailReviewNoteDetail");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
			parAddedBy.ParameterName = "@AddedBy";
			if(entity != null)
				parAddedBy.Value = entity.AddedBy;
			else
				parAddedBy.Value = System.DBNull.Value;
			cmdParams.Add(parAddedBy);
			System.Data.IDbDataParameter parAddedByUserID = cmd.CreateParameter();
			parAddedByUserID.ParameterName = "@AddedByUserID";
			if(entity != null)
				parAddedByUserID.Value = entity.AddedByUserID;
			else
				parAddedByUserID.Value = System.DBNull.Value;
			cmdParams.Add(parAddedByUserID);
			System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
			parDateAdded.ParameterName = "@DateAdded";
			if(entity != null)
				parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
			else
				parDateAdded.Value = System.DBNull.Value;
			cmdParams.Add(parDateAdded);
			System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
			parIsActive.ParameterName = "@IsActive";
			if(entity != null)
				parIsActive.Value = entity.IsActive;
			else
				parIsActive.Value = System.DBNull.Value;
			cmdParams.Add(parIsActive);
			System.Data.IDbDataParameter parReviewNote = cmd.CreateParameter();
			parReviewNote.ParameterName = "@ReviewNote";
			if(entity != null)
				parReviewNote.Value = entity.ReviewNote;
			else
				parReviewNote.Value = System.DBNull.Value;
			cmdParams.Add(parReviewNote);
			System.Data.IDbDataParameter parTaskDetailReviewNoteID = cmd.CreateParameter();
			parTaskDetailReviewNoteID.ParameterName = "@TaskDetailReviewNoteID";
			if(entity != null)
				parTaskDetailReviewNoteID.Value = entity.TaskDetailReviewNoteID;
			else
				parTaskDetailReviewNoteID.Value = System.DBNull.Value;
			cmdParams.Add(parTaskDetailReviewNoteID);
			System.Data.IDbDataParameter pkparTaskDetailReviewNoteDetailID = cmd.CreateParameter();
			pkparTaskDetailReviewNoteDetailID.ParameterName = "@TaskDetailReviewNoteDetailID";
			pkparTaskDetailReviewNoteDetailID.Value = entity.TaskDetailReviewNoteDetailID;
			cmdParams.Add(pkparTaskDetailReviewNoteDetailID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql delete command, using the passed in primary key
		/// </summary>
		/// <param name="id">The primary key of the object to delete</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_TaskDetailReviewNoteDetail");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@TaskDetailReviewNoteDetailID";
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
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_TaskDetailReviewNoteDetail");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@TaskDetailReviewNoteDetailID";
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
		public IList<TaskDetailReviewNoteDetailInfo> SelectAllByTaskDetailReviewNoteID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_TaskDetailReviewNoteDetailByTaskDetailReviewNoteID");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@TaskDetailReviewNoteID";
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
		public IList<TaskDetailReviewNoteDetailInfo> SelectAllByAddedByUserID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_TaskDetailReviewNoteDetailByAddedByUserID");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@AddedByUserID";
			par.Value = id;
			cmdParams.Add(par);
            return this.Select(cmd);
		}
	
    }
}
