
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
    public abstract class TaskAttributeValueDAOBase : CustomAbstractDAO<TaskAttributeValueInfo>// AbstractDAO<TaskAttributeValueInfo> 
	{
		/// <summary>
		/// A static representation of column ReferenceID
		/// </summary>
		public static readonly string COLUMN_REFERENCEID = "ReferenceID";
		/// <summary>
		/// A static representation of column TaskAttributeRecperiodSetID
		/// </summary>
		public static readonly string COLUMN_TASKATTRIBUTERECPERIODSETID = "TaskAttributeRecperiodSetID";
		/// <summary>
		/// A static representation of column TaskAttributeValueID
		/// </summary>
		public static readonly string COLUMN_TASKATTRIBUTEVALUEID = "TaskAttributeValueID";
		/// <summary>
		/// A static representation of column Value
		/// </summary>
		public static readonly string COLUMN_VALUE = "Value";
		/// <summary>
		/// Provides access to the name of the primary key column (TaskAttributeValueID)
		/// </summary>
		public static readonly string TABLE_PRIMARYKEY = "TaskAttributeValueID";

		/// <summary>
		/// Provides access to the name of the table
		/// </summary>
		public static readonly string TABLE_NAME = "TaskAttributeValue";

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
        public TaskAttributeValueDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "TaskAttributeValue", oAppUserInfo.ConnectionString) 
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }
        
        /// <summary>
        /// Maps the IDataReader values to a TaskAttributeValueInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>TaskAttributeValueInfo</returns>
        protected override TaskAttributeValueInfo MapObject(System.Data.IDataReader r) 
        {
            TaskAttributeValueInfo entity = new TaskAttributeValueInfo();
			entity.TaskAttributeValueID = r.GetInt64Value("TaskAttributeValueID");
			entity.TaskAttributeRecperiodSetID = r.GetInt64Value("TaskAttributeRecperiodSetID");
			entity.ReferenceID = r.GetInt64Value("ReferenceID");
			entity.Value = r.GetStringValue("Value");
            return entity;
        }
 
		/// <summary>
		/// Creates the sql insert command, using the values from the passed
		/// in TaskAttributeValueInfo object
		/// </summary>
		/// <param name="o">A TaskAttributeValueInfo object, from which the insert values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(TaskAttributeValueInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_TaskAttributeValue");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter parReferenceID = cmd.CreateParameter();
			parReferenceID.ParameterName = "@ReferenceID";
			if(entity != null)
				parReferenceID.Value = entity.ReferenceID;
			else
				parReferenceID.Value = System.DBNull.Value;
			cmdParams.Add(parReferenceID);
			System.Data.IDbDataParameter parTaskAttributeRecperiodSetID = cmd.CreateParameter();
			parTaskAttributeRecperiodSetID.ParameterName = "@TaskAttributeRecperiodSetID";
			if(entity != null)
				parTaskAttributeRecperiodSetID.Value = entity.TaskAttributeRecperiodSetID;
			else
				parTaskAttributeRecperiodSetID.Value = System.DBNull.Value;
			cmdParams.Add(parTaskAttributeRecperiodSetID);
			System.Data.IDbDataParameter parTaskAttributeValueID = cmd.CreateParameter();
			parTaskAttributeValueID.ParameterName = "@TaskAttributeValueID";
			if(entity != null)
				parTaskAttributeValueID.Value = entity.TaskAttributeValueID;
			else
				parTaskAttributeValueID.Value = System.DBNull.Value;
			cmdParams.Add(parTaskAttributeValueID);
			System.Data.IDbDataParameter parValue = cmd.CreateParameter();
			parValue.ParameterName = "@Value";
			if(entity != null)
				parValue.Value = entity.Value;
			else
				parValue.Value = System.DBNull.Value;
			cmdParams.Add(parValue);
			return cmd;
        }

		/// <summary>
		/// Creates the sql update command, using the values from the passed
		/// in TaskAttributeValueInfo object
		/// </summary>
		/// <param name="o">A TaskAttributeValueInfo object, from which the update values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(TaskAttributeValueInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_TaskAttributeValue");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter parReferenceID = cmd.CreateParameter();
			parReferenceID.ParameterName = "@ReferenceID";
			if(entity != null)
				parReferenceID.Value = entity.ReferenceID;
			else
				parReferenceID.Value = System.DBNull.Value;
			cmdParams.Add(parReferenceID);
			System.Data.IDbDataParameter parTaskAttributeRecperiodSetID = cmd.CreateParameter();
			parTaskAttributeRecperiodSetID.ParameterName = "@TaskAttributeRecperiodSetID";
			if(entity != null)
				parTaskAttributeRecperiodSetID.Value = entity.TaskAttributeRecperiodSetID;
			else
				parTaskAttributeRecperiodSetID.Value = System.DBNull.Value;
			cmdParams.Add(parTaskAttributeRecperiodSetID);
			System.Data.IDbDataParameter parValue = cmd.CreateParameter();
			parValue.ParameterName = "@Value";
			if(entity != null)
				parValue.Value = entity.Value;
			else
				parValue.Value = System.DBNull.Value;
			cmdParams.Add(parValue);
			System.Data.IDbDataParameter pkparTaskAttributeValueID = cmd.CreateParameter();
			pkparTaskAttributeValueID.ParameterName = "@TaskAttributeValueID";
			pkparTaskAttributeValueID.Value = entity.TaskAttributeValueID;
			cmdParams.Add(pkparTaskAttributeValueID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql delete command, using the passed in primary key
		/// </summary>
		/// <param name="id">The primary key of the object to delete</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_TaskAttributeValue");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@TaskAttributeValueID";
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
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_TaskAttributeValue");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@TaskAttributeValueID";
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
		public IList<TaskAttributeValueInfo> SelectAllByTaskAttributeRecperiodSetID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_TaskAttributeValueByTaskAttributeRecperiodSetID");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@TaskAttributeRecperiodSetID";
			par.Value = id;
			cmdParams.Add(par);
            return this.Select(cmd);
		}
	
    }
}
