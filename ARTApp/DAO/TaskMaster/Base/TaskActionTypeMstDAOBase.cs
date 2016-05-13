
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
    public abstract class TaskActionTypeMstDAOBase : CustomAbstractDAO<TaskActionTypeMstInfo>//AbstractDAO<TaskActionTypeMstInfo> 
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
		/// A static representation of column TaskActionType
		/// </summary>
		public static readonly string COLUMN_TASKACTIONTYPE = "TaskActionType";
		/// <summary>
		/// A static representation of column TaskActionTypeID
		/// </summary>
		public static readonly string COLUMN_TASKACTIONTYPEID = "TaskActionTypeID";
		/// <summary>
		/// A static representation of column TaskActionTypeLabelID
		/// </summary>
		public static readonly string COLUMN_TASKACTIONTYPELABELID = "TaskActionTypeLabelID";
		/// <summary>
		/// Provides access to the name of the primary key column (TaskActionTypeID)
		/// </summary>
		public static readonly string TABLE_PRIMARYKEY = "TaskActionTypeID";

		/// <summary>
		/// Provides access to the name of the table
		/// </summary>
		public static readonly string TABLE_NAME = "TaskActionTypeMst";

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
        public TaskActionTypeMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "TaskActionTypeMst", oAppUserInfo.ConnectionString) 
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }
        
        /// <summary>
        /// Maps the IDataReader values to a TaskActionTypeMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>TaskActionTypeMstInfo</returns>
        protected override TaskActionTypeMstInfo MapObject(System.Data.IDataReader r) 
        {
            TaskActionTypeMstInfo entity = new TaskActionTypeMstInfo();
			entity.TaskActionTypeID = r.GetInt16Value("TaskActionTypeID");
			entity.TaskActionType = r.GetStringValue("TaskActionType");
			entity.TaskActionTypeLabelID = r.GetInt32Value("TaskActionTypeLabelID");
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
		/// in TaskActionTypeMstInfo object
		/// </summary>
		/// <param name="o">A TaskActionTypeMstInfo object, from which the insert values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(TaskActionTypeMstInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_TaskActionTypeMst");
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
			System.Data.IDbDataParameter parTaskActionType = cmd.CreateParameter();
			parTaskActionType.ParameterName = "@TaskActionType";
			if(entity != null)
				parTaskActionType.Value = entity.TaskActionType;
			else
				parTaskActionType.Value = System.DBNull.Value;
			cmdParams.Add(parTaskActionType);
			System.Data.IDbDataParameter parTaskActionTypeID = cmd.CreateParameter();
			parTaskActionTypeID.ParameterName = "@TaskActionTypeID";
			if(entity != null)
				parTaskActionTypeID.Value = entity.TaskActionTypeID;
			else
				parTaskActionTypeID.Value = System.DBNull.Value;
			cmdParams.Add(parTaskActionTypeID);
			System.Data.IDbDataParameter parTaskActionTypeLabelID = cmd.CreateParameter();
			parTaskActionTypeLabelID.ParameterName = "@TaskActionTypeLabelID";
			if(entity != null)
				parTaskActionTypeLabelID.Value = entity.TaskActionTypeLabelID;
			else
				parTaskActionTypeLabelID.Value = System.DBNull.Value;
			cmdParams.Add(parTaskActionTypeLabelID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql update command, using the values from the passed
		/// in TaskActionTypeMstInfo object
		/// </summary>
		/// <param name="o">A TaskActionTypeMstInfo object, from which the update values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(TaskActionTypeMstInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_TaskActionTypeMst");
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
			System.Data.IDbDataParameter parTaskActionType = cmd.CreateParameter();
			parTaskActionType.ParameterName = "@TaskActionType";
			if(entity != null)
				parTaskActionType.Value = entity.TaskActionType;
			else
				parTaskActionType.Value = System.DBNull.Value;
			cmdParams.Add(parTaskActionType);
			System.Data.IDbDataParameter parTaskActionTypeLabelID = cmd.CreateParameter();
			parTaskActionTypeLabelID.ParameterName = "@TaskActionTypeLabelID";
			if(entity != null)
				parTaskActionTypeLabelID.Value = entity.TaskActionTypeLabelID;
			else
				parTaskActionTypeLabelID.Value = System.DBNull.Value;
			cmdParams.Add(parTaskActionTypeLabelID);
			System.Data.IDbDataParameter pkparTaskActionTypeID = cmd.CreateParameter();
			pkparTaskActionTypeID.ParameterName = "@TaskActionTypeID";
			pkparTaskActionTypeID.Value = entity.TaskActionTypeID;
			cmdParams.Add(pkparTaskActionTypeID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql delete command, using the passed in primary key
		/// </summary>
		/// <param name="id">The primary key of the object to delete</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_TaskActionTypeMst");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@TaskActionTypeID";
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
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_TaskActionTypeMst");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@TaskActionTypeID";
			par.Value = id;
			cmdParams.Add(par);
            return cmd;
        }

	
    }
}
