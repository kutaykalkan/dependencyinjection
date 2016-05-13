
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
    public abstract class TaskAttributeRecPeriodSetHdrDAOBase : CustomAbstractDAO<TaskAttributeRecPeriodSetHdrInfo>// AbstractDAO<TaskAttributeRecPeriodSetHdrInfo> 
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
		/// A static representation of column EndRecperiodID
		/// </summary>
		public static readonly string COLUMN_ENDRECPERIODID = "EndRecperiodID";
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
		/// A static representation of column StartRecperiodID
		/// </summary>
		public static readonly string COLUMN_STARTRECPERIODID = "StartRecperiodID";
		/// <summary>
		/// A static representation of column TaskAttributeID
		/// </summary>
		public static readonly string COLUMN_TASKATTRIBUTEID = "TaskAttributeID";
		/// <summary>
		/// A static representation of column TaskAttributeRecperiodSetID
		/// </summary>
		public static readonly string COLUMN_TASKATTRIBUTERECPERIODSETID = "TaskAttributeRecperiodSetID";
		/// <summary>
		/// A static representation of column TaskID
		/// </summary>
		public static readonly string COLUMN_TASKID = "TaskID";
		/// <summary>
		/// Provides access to the name of the primary key column (TaskAttributeRecperiodSetID)
		/// </summary>
		public static readonly string TABLE_PRIMARYKEY = "TaskAttributeRecperiodSetID";

		/// <summary>
		/// Provides access to the name of the table
		/// </summary>
		public static readonly string TABLE_NAME = "TaskAttributeRecPeriodSetHdr";

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
        public TaskAttributeRecPeriodSetHdrDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "TaskAttributeRecPeriodSetHdr", oAppUserInfo.ConnectionString) 
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }
        
        /// <summary>
        /// Maps the IDataReader values to a TaskAttributeRecPeriodSetHdrInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>TaskAttributeRecPeriodSetHdrInfo</returns>
        protected override TaskAttributeRecPeriodSetHdrInfo MapObject(System.Data.IDataReader r) 
        {
            TaskAttributeRecPeriodSetHdrInfo entity = new TaskAttributeRecPeriodSetHdrInfo();
			entity.TaskAttributeRecperiodSetID = r.GetInt64Value("TaskAttributeRecperiodSetID");
			entity.TaskID = r.GetInt64Value("TaskID");
			entity.TaskAttributeID = r.GetInt16Value("TaskAttributeID");
			entity.StartRecperiodID = r.GetInt32Value("StartRecperiodID");
			entity.EndRecperiodID = r.GetInt32Value("EndRecperiodID");
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
		/// in TaskAttributeRecPeriodSetHdrInfo object
		/// </summary>
		/// <param name="o">A TaskAttributeRecPeriodSetHdrInfo object, from which the insert values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(TaskAttributeRecPeriodSetHdrInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_TaskAttributeRecPeriodSetHdr");
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
			System.Data.IDbDataParameter parEndRecperiodID = cmd.CreateParameter();
			parEndRecperiodID.ParameterName = "@EndRecperiodID";
			if(entity != null)
				parEndRecperiodID.Value = entity.EndRecperiodID;
			else
				parEndRecperiodID.Value = System.DBNull.Value;
			cmdParams.Add(parEndRecperiodID);
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
			System.Data.IDbDataParameter parStartRecperiodID = cmd.CreateParameter();
			parStartRecperiodID.ParameterName = "@StartRecperiodID";
			if(entity != null)
				parStartRecperiodID.Value = entity.StartRecperiodID;
			else
				parStartRecperiodID.Value = System.DBNull.Value;
			cmdParams.Add(parStartRecperiodID);
			System.Data.IDbDataParameter parTaskAttributeID = cmd.CreateParameter();
			parTaskAttributeID.ParameterName = "@TaskAttributeID";
			if(entity != null)
				parTaskAttributeID.Value = entity.TaskAttributeID;
			else
				parTaskAttributeID.Value = System.DBNull.Value;
			cmdParams.Add(parTaskAttributeID);
			System.Data.IDbDataParameter parTaskAttributeRecperiodSetID = cmd.CreateParameter();
			parTaskAttributeRecperiodSetID.ParameterName = "@TaskAttributeRecperiodSetID";
			if(entity != null)
				parTaskAttributeRecperiodSetID.Value = entity.TaskAttributeRecperiodSetID;
			else
				parTaskAttributeRecperiodSetID.Value = System.DBNull.Value;
			cmdParams.Add(parTaskAttributeRecperiodSetID);
			System.Data.IDbDataParameter parTaskID = cmd.CreateParameter();
			parTaskID.ParameterName = "@TaskID";
			if(entity != null)
				parTaskID.Value = entity.TaskID;
			else
				parTaskID.Value = System.DBNull.Value;
			cmdParams.Add(parTaskID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql update command, using the values from the passed
		/// in TaskAttributeRecPeriodSetHdrInfo object
		/// </summary>
		/// <param name="o">A TaskAttributeRecPeriodSetHdrInfo object, from which the update values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(TaskAttributeRecPeriodSetHdrInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_TaskAttributeRecPeriodSetHdr");
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
			System.Data.IDbDataParameter parEndRecperiodID = cmd.CreateParameter();
			parEndRecperiodID.ParameterName = "@EndRecperiodID";
			if(entity != null)
				parEndRecperiodID.Value = entity.EndRecperiodID;
			else
				parEndRecperiodID.Value = System.DBNull.Value;
			cmdParams.Add(parEndRecperiodID);
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
			System.Data.IDbDataParameter parStartRecperiodID = cmd.CreateParameter();
			parStartRecperiodID.ParameterName = "@StartRecperiodID";
			if(entity != null)
				parStartRecperiodID.Value = entity.StartRecperiodID;
			else
				parStartRecperiodID.Value = System.DBNull.Value;
			cmdParams.Add(parStartRecperiodID);
			System.Data.IDbDataParameter parTaskAttributeID = cmd.CreateParameter();
			parTaskAttributeID.ParameterName = "@TaskAttributeID";
			if(entity != null)
				parTaskAttributeID.Value = entity.TaskAttributeID;
			else
				parTaskAttributeID.Value = System.DBNull.Value;
			cmdParams.Add(parTaskAttributeID);
			System.Data.IDbDataParameter parTaskID = cmd.CreateParameter();
			parTaskID.ParameterName = "@TaskID";
			if(entity != null)
				parTaskID.Value = entity.TaskID;
			else
				parTaskID.Value = System.DBNull.Value;
			cmdParams.Add(parTaskID);
			System.Data.IDbDataParameter pkparTaskAttributeRecperiodSetID = cmd.CreateParameter();
			pkparTaskAttributeRecperiodSetID.ParameterName = "@TaskAttributeRecperiodSetID";
			pkparTaskAttributeRecperiodSetID.Value = entity.TaskAttributeRecperiodSetID;
			cmdParams.Add(pkparTaskAttributeRecperiodSetID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql delete command, using the passed in primary key
		/// </summary>
		/// <param name="id">The primary key of the object to delete</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_TaskAttributeRecPeriodSetHdr");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@TaskAttributeRecperiodSetID";
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
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_TaskAttributeRecPeriodSetHdr");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@TaskAttributeRecperiodSetID";
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
		public IList<TaskAttributeRecPeriodSetHdrInfo> SelectAllByTaskID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_TaskAttributeRecPeriodSetHdrByTaskID");
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
		public IList<TaskAttributeRecPeriodSetHdrInfo> SelectAllByTaskAttributeID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_TaskAttributeRecPeriodSetHdrByTaskAttributeID");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@TaskAttributeID";
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
		public IList<TaskAttributeRecPeriodSetHdrInfo> SelectAllByStartRecperiodID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_TaskAttributeRecPeriodSetHdrByStartRecperiodID");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@StartRecperiodID";
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
		public IList<TaskAttributeRecPeriodSetHdrInfo> SelectAllByEndRecperiodID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_TaskAttributeRecPeriodSetHdrByEndRecperiodID");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@EndRecperiodID";
			par.Value = id;
			cmdParams.Add(par);
            return this.Select(cmd);
		}
	
    }
}
