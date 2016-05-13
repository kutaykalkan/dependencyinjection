
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
    public abstract class TaskListHdrDAOBase : CustomAbstractDAO<TaskListHdrInfo>// AbstractDAO<TaskListHdrInfo> 
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
        /// A static representation of column TaskListID
        /// </summary>
        public static readonly string COLUMN_TASKLISTID = "TaskListID";
        /// <summary>
        /// A static representation of column TaskListName
        /// </summary>
        public static readonly string COLUMN_TASKLISTNAME = "TaskListName";
        /// <summary>
        /// A static representation of column TaskListNameLabelID
        /// </summary>
        public static readonly string COLUMN_TASKLISTNAMELABELID = "TaskListNameLabelID";
        /// <summary>
        /// Provides access to the name of the primary key column (TaskListID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "TaskListID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "TaskListHdr";

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
        public TaskListHdrDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "TaskListHdr", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a TaskListHdrInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>TaskListHdrInfo</returns>
        protected override TaskListHdrInfo MapObject(System.Data.IDataReader r)
        {
            TaskListHdrInfo entity = new TaskListHdrInfo();
            entity.TaskListID = r.GetInt16Value("TaskListID");
            entity.TaskListName = r.GetStringValue("TaskListName");
            entity.TaskListNameLabelID = r.GetInt32Value("TaskListNameLabelID");
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
        /// in TaskListHdrInfo object
        /// </summary>
        /// <param name="o">A TaskListHdrInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(TaskListHdrInfo entity)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_TaskListHdr");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (entity != null)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);
            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (entity != null)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);
            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (entity != null)
                parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);
            System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            parHostName.ParameterName = "@HostName";
            if (entity != null)
                parHostName.Value = entity.HostName;
            else
                parHostName.Value = System.DBNull.Value;
            cmdParams.Add(parHostName);
            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (entity != null)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);
            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (entity != null)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);
            System.Data.IDbDataParameter parTaskListName = cmd.CreateParameter();
            parTaskListName.ParameterName = "@TaskListName";
            if (entity != null)
                parTaskListName.Value = entity.TaskListName;
            else
                parTaskListName.Value = System.DBNull.Value;
            cmdParams.Add(parTaskListName);
            System.Data.IDbDataParameter parTaskListNameLabelID = cmd.CreateParameter();
            parTaskListNameLabelID.ParameterName = "@TaskListNameLabelID";
            if (entity != null)
                parTaskListNameLabelID.Value = entity.TaskListNameLabelID;
            else
                parTaskListNameLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parTaskListNameLabelID);
            return cmd;
        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in TaskListHdrInfo object
        /// </summary>
        /// <param name="o">A TaskListHdrInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(TaskListHdrInfo entity)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_TaskListHdr");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (entity != null)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);
            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (entity != null)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);
            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (entity != null)
                parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);
            System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            parHostName.ParameterName = "@HostName";
            if (entity != null)
                parHostName.Value = entity.HostName;
            else
                parHostName.Value = System.DBNull.Value;
            cmdParams.Add(parHostName);
            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (entity != null)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);
            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (entity != null)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);
            System.Data.IDbDataParameter parTaskListName = cmd.CreateParameter();
            parTaskListName.ParameterName = "@TaskListName";
            if (entity != null)
                parTaskListName.Value = entity.TaskListName;
            else
                parTaskListName.Value = System.DBNull.Value;
            cmdParams.Add(parTaskListName);
            System.Data.IDbDataParameter parTaskListNameLabelID = cmd.CreateParameter();
            parTaskListNameLabelID.ParameterName = "@TaskListNameLabelID";
            if (entity != null)
                parTaskListNameLabelID.Value = entity.TaskListNameLabelID;
            else
                parTaskListNameLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parTaskListNameLabelID);
            System.Data.IDbDataParameter pkparTaskListID = cmd.CreateParameter();
            pkparTaskListID.ParameterName = "@TaskListID";
            pkparTaskListID.Value = entity.TaskListID;
            cmdParams.Add(pkparTaskListID);
            return cmd;
        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_TaskListHdr");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@TaskListID";
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
            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_TaskListHdr");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@TaskListID";
            par.Value = id;
            cmdParams.Add(par);
            return cmd;
        }

        protected override void CustomSave(TaskListHdrInfo o, IDbConnection connection)
        {
            string query = QueryHelper.GetSqlServerLastInsertedCommand(TaskListHdrDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);
        }

        protected override void CustomSave(TaskListHdrInfo o, IDbConnection connection, IDbTransaction transaction)
        {
            string query = QueryHelper.GetSqlServerLastInsertedCommand(TaskListHdrDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);
        }
        private void MapIdentity(TaskListHdrInfo entity, object id)
        {
            entity.TaskListID = Convert.ToInt16(id);
        }

    }
}
