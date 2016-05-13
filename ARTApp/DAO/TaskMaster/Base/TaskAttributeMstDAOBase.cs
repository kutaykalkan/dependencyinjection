
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
    public abstract class TaskAttributeMstDAOBase : CustomAbstractDAO<TaskAttributeMstInfo>// AbstractDAO<TaskAttributeMstInfo> 
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
		/// A static representation of column TaskAttribute
		/// </summary>
		public static readonly string COLUMN_TASKATTRIBUTE = "TaskAttribute";
		/// <summary>
		/// A static representation of column TaskAttributeID
		/// </summary>
		public static readonly string COLUMN_TASKATTRIBUTEID = "TaskAttributeID";
		/// <summary>
		/// A static representation of column TaskAttributeLabelID
		/// </summary>
		public static readonly string COLUMN_TASKATTRIBUTELABELID = "TaskAttributeLabelID";
		/// <summary>
		/// Provides access to the name of the primary key column (TaskAttributeID)
		/// </summary>
		public static readonly string TABLE_PRIMARYKEY = "TaskAttributeID";

		/// <summary>
		/// Provides access to the name of the table
		/// </summary>
		public static readonly string TABLE_NAME = "TaskAttributeMst";

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
        public TaskAttributeMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "TaskAttributeMst", oAppUserInfo.ConnectionString) 
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }
        
        /// <summary>
        /// Maps the IDataReader values to a TaskAttributeMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>TaskAttributeMstInfo</returns>
        protected override TaskAttributeMstInfo MapObject(System.Data.IDataReader r) 
        {
            TaskAttributeMstInfo entity = new TaskAttributeMstInfo();
			entity.TaskAttributeID = r.GetInt16Value("TaskAttributeID");
			entity.TaskAttribute = r.GetStringValue("TaskAttribute");
			entity.TaskAttributeLabelID = r.GetInt32Value("TaskAttributeLabelID");
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
		/// in TaskAttributeMstInfo object
		/// </summary>
		/// <param name="o">A TaskAttributeMstInfo object, from which the insert values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(TaskAttributeMstInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_TaskAttributeMst");
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
			System.Data.IDbDataParameter parTaskAttribute = cmd.CreateParameter();
			parTaskAttribute.ParameterName = "@TaskAttribute";
			if(entity != null)
				parTaskAttribute.Value = entity.TaskAttribute;
			else
				parTaskAttribute.Value = System.DBNull.Value;
			cmdParams.Add(parTaskAttribute);
			System.Data.IDbDataParameter parTaskAttributeID = cmd.CreateParameter();
			parTaskAttributeID.ParameterName = "@TaskAttributeID";
			if(entity != null)
				parTaskAttributeID.Value = entity.TaskAttributeID;
			else
				parTaskAttributeID.Value = System.DBNull.Value;
			cmdParams.Add(parTaskAttributeID);
			System.Data.IDbDataParameter parTaskAttributeLabelID = cmd.CreateParameter();
			parTaskAttributeLabelID.ParameterName = "@TaskAttributeLabelID";
			if(entity != null)
				parTaskAttributeLabelID.Value = entity.TaskAttributeLabelID;
			else
				parTaskAttributeLabelID.Value = System.DBNull.Value;
			cmdParams.Add(parTaskAttributeLabelID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql update command, using the values from the passed
		/// in TaskAttributeMstInfo object
		/// </summary>
		/// <param name="o">A TaskAttributeMstInfo object, from which the update values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(TaskAttributeMstInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_TaskAttributeMst");
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
			System.Data.IDbDataParameter parTaskAttribute = cmd.CreateParameter();
			parTaskAttribute.ParameterName = "@TaskAttribute";
			if(entity != null)
				parTaskAttribute.Value = entity.TaskAttribute;
			else
				parTaskAttribute.Value = System.DBNull.Value;
			cmdParams.Add(parTaskAttribute);
			System.Data.IDbDataParameter parTaskAttributeLabelID = cmd.CreateParameter();
			parTaskAttributeLabelID.ParameterName = "@TaskAttributeLabelID";
			if(entity != null)
				parTaskAttributeLabelID.Value = entity.TaskAttributeLabelID;
			else
				parTaskAttributeLabelID.Value = System.DBNull.Value;
			cmdParams.Add(parTaskAttributeLabelID);
			System.Data.IDbDataParameter pkparTaskAttributeID = cmd.CreateParameter();
			pkparTaskAttributeID.ParameterName = "@TaskAttributeID";
			pkparTaskAttributeID.Value = entity.TaskAttributeID;
			cmdParams.Add(pkparTaskAttributeID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql delete command, using the passed in primary key
		/// </summary>
		/// <param name="id">The primary key of the object to delete</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_TaskAttributeMst");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@TaskAttributeID";
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
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_TaskAttributeMst");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@TaskAttributeID";
			par.Value = id;
			cmdParams.Add(par);
            return cmd;
        }

	
		/// <summary>
		/// Creates the Entity Collection of table related to present table by n-n relationship
		/// </summary>
		/// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
		/// <returns>A collection of related entities</returns>
		public IList<TaskAttributeMstInfo> SelectTaskAttributeMstDetailsAssociatedToTaskHdrByTaskAttributeRecPeriodSetHdr(TaskHdrInfo entity)
		{
			return this.SelectTaskAttributeMstDetailsAssociatedToTaskHdrByTaskAttributeRecPeriodSetHdr(entity.TaskID);		
		}

		/// <summary>
		/// Creates the Entity Collection of table related to present table by n-n relationship
		/// </summary>
		/// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
		/// <returns>A collection of related entities</returns>
		public IList<TaskAttributeMstInfo> SelectTaskAttributeMstDetailsAssociatedToTaskHdrByTaskAttributeRecPeriodSetHdr(object id)
		{							
			System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [TaskAttributeMst] INNER JOIN [TaskAttributeRecPeriodSetHdr] ON [TaskAttributeMst].[TaskAttributeID] = [TaskAttributeRecPeriodSetHdr].[TaskAttributeID] INNER JOIN [TaskHdr] ON [TaskAttributeRecPeriodSetHdr].[TaskID] = [TaskHdr].[TaskID]  WHERE  [TaskHdr].[TaskID] = @TaskID ");
			IDataParameterCollection cmdParams = cmd.Parameters;
        	System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@TaskID";
			par.Value = id;
			cmdParams.Add(par);
            List<TaskAttributeMstInfo> objTaskAttributeMstEntityColl = new List<TaskAttributeMstInfo>(this.Select(cmd));
            return objTaskAttributeMstEntityColl;
	    }
    }
}
