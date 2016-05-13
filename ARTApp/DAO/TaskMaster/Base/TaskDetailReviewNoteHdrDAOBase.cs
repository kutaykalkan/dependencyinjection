
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
    public abstract class TaskDetailReviewNoteHdrDAOBase : CustomAbstractDAO<TaskDetailReviewNoteHdrInfo>// AbstractDAO<TaskDetailReviewNoteHdrInfo> 
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
		/// A static representation of column RevisedByUserID
		/// </summary>
		public static readonly string COLUMN_REVISEDBYUSERID = "RevisedByUserID";
		/// <summary>
		/// A static representation of column SubjectLine
		/// </summary>
		public static readonly string COLUMN_SUBJECTLINE = "SubjectLine";
		/// <summary>
		/// A static representation of column TaskDetailID
		/// </summary>
		public static readonly string COLUMN_TASKDETAILID = "TaskDetailID";
		/// <summary>
		/// A static representation of column TaskDetailReviewNoteID
		/// </summary>
		public static readonly string COLUMN_TASKDETAILREVIEWNOTEID = "TaskDetailReviewNoteID";
		/// <summary>
		/// Provides access to the name of the primary key column (TaskDetailReviewNoteID)
		/// </summary>
		public static readonly string TABLE_PRIMARYKEY = "TaskDetailReviewNoteID";

		/// <summary>
		/// Provides access to the name of the table
		/// </summary>
		public static readonly string TABLE_NAME = "TaskDetailReviewNoteHdr";

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
        public TaskDetailReviewNoteHdrDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "TaskDetailReviewNoteHdr", oAppUserInfo.ConnectionString) 
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }
        
        /// <summary>
        /// Maps the IDataReader values to a TaskDetailReviewNoteHdrInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>TaskDetailReviewNoteHdrInfo</returns>
        protected override TaskDetailReviewNoteHdrInfo MapObject(System.Data.IDataReader r) 
        {
            TaskDetailReviewNoteHdrInfo entity = new TaskDetailReviewNoteHdrInfo();
			entity.TaskDetailReviewNoteID = r.GetInt32Value("TaskDetailReviewNoteID");
			entity.TaskDetailID = r.GetInt64Value("TaskDetailID");
			entity.SubjectLine = r.GetStringValue("SubjectLine");
			entity.AddedByUserID = r.GetInt32Value("AddedByUserID");
			entity.RevisedByUserID = r.GetInt32Value("RevisedByUserID");
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
		/// in TaskDetailReviewNoteHdrInfo object
		/// </summary>
		/// <param name="o">A TaskDetailReviewNoteHdrInfo object, from which the insert values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(TaskDetailReviewNoteHdrInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_TaskDetailReviewNoteHdr");
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
			System.Data.IDbDataParameter parRevisedByUserID = cmd.CreateParameter();
			parRevisedByUserID.ParameterName = "@RevisedByUserID";
			if(entity != null)
				parRevisedByUserID.Value = entity.RevisedByUserID;
			else
				parRevisedByUserID.Value = System.DBNull.Value;
			cmdParams.Add(parRevisedByUserID);
			System.Data.IDbDataParameter parSubjectLine = cmd.CreateParameter();
			parSubjectLine.ParameterName = "@SubjectLine";
			if(entity != null)
				parSubjectLine.Value = entity.SubjectLine;
			else
				parSubjectLine.Value = System.DBNull.Value;
			cmdParams.Add(parSubjectLine);
			System.Data.IDbDataParameter parTaskDetailID = cmd.CreateParameter();
			parTaskDetailID.ParameterName = "@TaskDetailID";
			if(entity != null)
				parTaskDetailID.Value = entity.TaskDetailID;
			else
				parTaskDetailID.Value = System.DBNull.Value;
			cmdParams.Add(parTaskDetailID);
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
		/// in TaskDetailReviewNoteHdrInfo object
		/// </summary>
		/// <param name="o">A TaskDetailReviewNoteHdrInfo object, from which the update values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(TaskDetailReviewNoteHdrInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_TaskDetailReviewNoteHdr");
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
			System.Data.IDbDataParameter parRevisedByUserID = cmd.CreateParameter();
			parRevisedByUserID.ParameterName = "@RevisedByUserID";
			if(entity != null)
				parRevisedByUserID.Value = entity.RevisedByUserID;
			else
				parRevisedByUserID.Value = System.DBNull.Value;
			cmdParams.Add(parRevisedByUserID);
			System.Data.IDbDataParameter parSubjectLine = cmd.CreateParameter();
			parSubjectLine.ParameterName = "@SubjectLine";
			if(entity != null)
				parSubjectLine.Value = entity.SubjectLine;
			else
				parSubjectLine.Value = System.DBNull.Value;
			cmdParams.Add(parSubjectLine);
			System.Data.IDbDataParameter parTaskDetailID = cmd.CreateParameter();
			parTaskDetailID.ParameterName = "@TaskDetailID";
			if(entity != null)
				parTaskDetailID.Value = entity.TaskDetailID;
			else
				parTaskDetailID.Value = System.DBNull.Value;
			cmdParams.Add(parTaskDetailID);
			System.Data.IDbDataParameter pkparTaskDetailReviewNoteID = cmd.CreateParameter();
			pkparTaskDetailReviewNoteID.ParameterName = "@TaskDetailReviewNoteID";
			pkparTaskDetailReviewNoteID.Value = entity.TaskDetailReviewNoteID;
			cmdParams.Add(pkparTaskDetailReviewNoteID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql delete command, using the passed in primary key
		/// </summary>
		/// <param name="id">The primary key of the object to delete</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_TaskDetailReviewNoteHdr");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@TaskDetailReviewNoteID";
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
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_TaskDetailReviewNoteHdr");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@TaskDetailReviewNoteID";
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
		public IList<TaskDetailReviewNoteHdrInfo> SelectAllByTaskDetailID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_TaskDetailReviewNoteHdrByTaskDetailID");
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
		public IList<TaskDetailReviewNoteHdrInfo> SelectAllByAddedByUserID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_TaskDetailReviewNoteHdrByAddedByUserID");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@AddedByUserID";
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
		public IList<TaskDetailReviewNoteHdrInfo> SelectAllByRevisedByUserID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_TaskDetailReviewNoteHdrByRevisedByUserID");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@RevisedByUserID";
			par.Value = id;
			cmdParams.Add(par);
            return this.Select(cmd);
		}
	
    }
}
