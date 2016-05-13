
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
    public abstract class TaskHdrDAOBase : CustomAbstractDAO<TaskHdrInfo>// AbstractDAO<TaskHdrInfo>
    {
        /// <summary>
        /// A static representation of column AddedBy
        /// </summary>
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
        /// <summary>
        /// A static representation of column DataImportID
        /// </summary>
        public static readonly string COLUMN_DATAIMPORTID = "DataImportID";
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
        /// A static representation of column TaskID
        /// </summary>
        public static readonly string COLUMN_TASKID = "TaskID";
        /// <summary>
        /// A static representation of column TaskNumber
        /// </summary>
        public static readonly string COLUMN_TASKNUMBER = "TaskNumber";
        /// <summary>
        /// A static representation of column TaskTypeID
        /// </summary>
        public static readonly string COLUMN_TASKTYPEID = "TaskTypeID";
        /// <summary>
        /// Provides access to the name of the primary key column (TaskID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "TaskID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "TaskHdr";

        /// <summary>
        /// Provides access to the name of the database
        /// </summary>
        public static readonly string DATABASE_NAME = "SkyStemART";
        /// <summary>
        /// A static representation of column ACCOUNTNAMELABELID
        /// </summary>
        public static readonly string COLUMN_ACCOUNTNAMELABELID = "AccountNameLabelID";
        /// <summary>
        /// A static representation of column ACCOUNTNUMBER
        /// </summary>
        public static readonly string COLUMN_ACCOUNTNUMBER = "Accountnumber";

        /// <summary>
        ///  CurrentAppUserInfo  for further use
        /// </summary>
        public AppUserInfo CurrentAppUserInfo { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public TaskHdrDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "TaskHdr", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a TaskHdrInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>TaskHdrInfo</returns>
        protected override TaskHdrInfo MapObject(System.Data.IDataReader r)
        {
            TaskHdrInfo entity = new TaskHdrInfo();
            entity.TaskID = r.GetInt64Value(COLUMN_TASKID);
            entity.TaskNumber = r.GetStringValue(COLUMN_TASKNUMBER);
            entity.TaskTypeID = r.GetInt16Value(COLUMN_TASKTYPEID);
            entity.RecPeriodID = r.GetInt32Value(COLUMN_RECPERIODID);
            entity.DataImportID = r.GetInt32Value(COLUMN_DATAIMPORTID);
            entity.IsActive = r.GetBooleanValue(COLUMN_ISACTIVE);
            entity.DateAdded = r.GetDateValue(COLUMN_DATEADDED);
            entity.AddedBy = r.GetStringValue(COLUMN_ADDEDBY);
            entity.DateRevised = r.GetDateValue(COLUMN_DATEREVISED);
            entity.RevisedBy = r.GetStringValue(COLUMN_REVISEDBY);
            entity.HostName = r.GetStringValue(COLUMN_HOSTNAME);
            entity.AccountNameLabelID = r.GetInt32Value(COLUMN_ACCOUNTNAMELABELID);
            entity.AccountNumber = r.GetStringValue(COLUMN_ACCOUNTNUMBER);

            GeographyObjectHdrDAO oGeogObjHdrDAO = new GeographyObjectHdrDAO(this.CurrentAppUserInfo);
            oGeogObjHdrDAO.MapObjectWithOrganisationalHierarchyInfo(r, entity);
            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in TaskHdrInfo object
        /// </summary>
        /// <param name="o">A TaskHdrInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(TaskHdrInfo entity)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_TaskHdr");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (entity != null)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);
            System.Data.IDbDataParameter parDataImportID = cmd.CreateParameter();
            parDataImportID.ParameterName = "@DataImportID";
            if (entity != null)
                parDataImportID.Value = entity.DataImportID;
            else
                parDataImportID.Value = System.DBNull.Value;
            cmdParams.Add(parDataImportID);
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
            System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            if (entity != null)
                parRecPeriodID.Value = entity.RecPeriodID;
            else
                parRecPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parRecPeriodID);
            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (entity != null)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);
            System.Data.IDbDataParameter parTaskNumber = cmd.CreateParameter();
            parTaskNumber.ParameterName = "@TaskNumber";
            if (entity != null)
                parTaskNumber.Value = entity.TaskNumber;
            else
                parTaskNumber.Value = System.DBNull.Value;
            cmdParams.Add(parTaskNumber);
            System.Data.IDbDataParameter parTaskTypeID = cmd.CreateParameter();
            parTaskTypeID.ParameterName = "@TaskTypeID";
            if (entity != null)
                parTaskTypeID.Value = entity.TaskTypeID;
            else
                parTaskTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parTaskTypeID);
            return cmd;
        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in TaskHdrInfo object
        /// </summary>
        /// <param name="o">A TaskHdrInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(TaskHdrInfo entity)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_TaskHdr");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (entity != null)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);
            System.Data.IDbDataParameter parDataImportID = cmd.CreateParameter();
            parDataImportID.ParameterName = "@DataImportID";
            if (entity != null)
                parDataImportID.Value = entity.DataImportID;
            else
                parDataImportID.Value = System.DBNull.Value;
            cmdParams.Add(parDataImportID);
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
            System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            if (entity != null)
                parRecPeriodID.Value = entity.RecPeriodID;
            else
                parRecPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parRecPeriodID);
            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (entity != null)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);
            System.Data.IDbDataParameter parTaskNumber = cmd.CreateParameter();
            parTaskNumber.ParameterName = "@TaskNumber";
            if (entity != null)
                parTaskNumber.Value = entity.TaskNumber;
            else
                parTaskNumber.Value = System.DBNull.Value;
            cmdParams.Add(parTaskNumber);
            System.Data.IDbDataParameter parTaskTypeID = cmd.CreateParameter();
            parTaskTypeID.ParameterName = "@TaskTypeID";
            if (entity != null)
                parTaskTypeID.Value = entity.TaskTypeID;
            else
                parTaskTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parTaskTypeID);
            System.Data.IDbDataParameter pkparTaskID = cmd.CreateParameter();
            pkparTaskID.ParameterName = "@TaskID";
            pkparTaskID.Value = entity.TaskID;
            cmdParams.Add(pkparTaskID);
            return cmd;
        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_TaskHdr");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@TaskID";
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
            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_TaskHdr");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@TaskID";
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
        public IList<TaskHdrInfo> SelectAllByTaskTypeID(object id)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_TaskHdrByTaskTypeID");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@TaskTypeID";
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
        public IList<TaskHdrInfo> SelectAllByRecPeriodID(object id)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_TaskHdrByRecPeriodID");
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
        public IList<TaskHdrInfo> SelectAllByDataImportID(object id)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_TaskHdrByDataImportID");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DataImportID";
            par.Value = id;
            cmdParams.Add(par);
            return this.Select(cmd);
        }
        protected override void CustomSave(TaskHdrInfo o, IDbConnection connection)
        {
            string query = QueryHelper.GetSqlServerLastInsertedCommand(TaskHdrDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);
        }

        protected override void CustomSave(TaskHdrInfo o, IDbConnection connection, IDbTransaction transaction)
        {
            string query = QueryHelper.GetSqlServerLastInsertedCommand(TaskHdrDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);
        }
        private void MapIdentity(TaskHdrInfo entity, object id)
        {
            entity.TaskID = Convert.ToInt64(id);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<TaskHdrInfo> SelectTaskHdrDetailsAssociatedToTaskAttributeMstByTaskAttributeRecPeriodSetHdr(TaskAttributeMstInfo entity)
        {
            return this.SelectTaskHdrDetailsAssociatedToTaskAttributeMstByTaskAttributeRecPeriodSetHdr(entity.TaskAttributeID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<TaskHdrInfo> SelectTaskHdrDetailsAssociatedToTaskAttributeMstByTaskAttributeRecPeriodSetHdr(object id)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [TaskHdr] INNER JOIN [TaskAttributeRecPeriodSetHdr] ON [TaskHdr].[TaskID] = [TaskAttributeRecPeriodSetHdr].[TaskID] INNER JOIN [TaskAttributeMst] ON [TaskAttributeRecPeriodSetHdr].[TaskAttributeID] = [TaskAttributeMst].[TaskAttributeID]  WHERE  [TaskAttributeMst].[TaskAttributeID] = @TaskAttributeID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@TaskAttributeID";
            par.Value = id;
            cmdParams.Add(par);
            List<TaskHdrInfo> objTaskHdrEntityColl = new List<TaskHdrInfo>(this.Select(cmd));
            return objTaskHdrEntityColl;
        }
        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<TaskHdrInfo> SelectTaskHdrDetailsAssociatedToTaskStatusMstByTaskDetail(TaskStatusMstInfo entity)
        {
            return this.SelectTaskHdrDetailsAssociatedToTaskStatusMstByTaskDetail(entity.TaskStatusID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<TaskHdrInfo> SelectTaskHdrDetailsAssociatedToTaskStatusMstByTaskDetail(object id)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [TaskHdr] INNER JOIN [TaskDetail] ON [TaskHdr].[TaskID] = [TaskDetail].[TaskID] INNER JOIN [TaskStatusMst] ON [TaskDetail].[TaskStatusID] = [TaskStatusMst].[TaskStatusID]  WHERE  [TaskStatusMst].[TaskStatusID] = @TaskStatusID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@TaskStatusID";
            par.Value = id;
            cmdParams.Add(par);
            List<TaskHdrInfo> objTaskHdrEntityColl = new List<TaskHdrInfo>(this.Select(cmd));
            return objTaskHdrEntityColl;
        }
    }
}
