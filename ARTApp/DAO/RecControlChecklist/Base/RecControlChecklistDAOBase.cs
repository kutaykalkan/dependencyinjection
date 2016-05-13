

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Model.RecControlCheckList;

namespace SkyStem.ART.App.DAO.Base
{

    public abstract class RecControlChecklistDAOBase : CustomAbstractDAO<RecControlCheckListInfo>
    {

        /// <summary>
        /// A static representation of column RecControlChecklistID
        /// </summary>
        public static readonly string COLUMN_RECCONTROLCHECKLISTID = "RecControlChecklistID";
        /// <summary>
        /// A static representation of column ChecklistNumber
        /// </summary>
        public static readonly string COLUMN_CHECKLISTNUMBER = "ChecklistNumber";
        /// <summary>
        /// A static representation of column Description
        /// </summary>
        public static readonly string COLUMN_DESCRIPTION = "Description";
        /// <summary>
        /// A static representation of column DescriptionLabelID
        /// </summary>
        public static readonly string COLUMN_DESCRIPTIONLABELID = "DescriptionLabelID";
        /// <summary>
        /// A static representation of column StartRecPeriodID
        /// </summary>
        public static readonly string COLUMN_STARTRECPERIODID = "StartRecPeriodID";
        /// <summary>
        /// A static representation of column EndRecPeriodID
        /// </summary>
        public static readonly string COLUMN_ENDRECPERIODID = "EndRecPeriodID";
        /// <summary>
        /// A static representation of column DataImportID
        /// </summary>
        public static readonly string COLUMN_DATAIMPORTID = "DataImportID";
        /// <summary>
        /// A static representation of column IsActive
        /// </summary>
        public static readonly string COLUMN_ISACTIVE = "IsActive";
        /// <summary>
        /// A static representation of column AddedByUserID
        /// </summary>
        public static readonly string COLUMN_ADDEDBYUSERID = "AddedByUserID";
        /// <summary>
        /// A static representation of column RoleID
        /// </summary>
        public static readonly string COLUMN_ROLEID = "RoleID";
        /// <summary>
        /// A static representation of column DateAdded
        /// </summary>
        public static readonly string COLUMN_DATEADDED = "DateAdded";
        /// <summary>
        /// A static representation of column AddedBy
        /// </summary>
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
        /// <summary>
        /// A static representation of column DateRevised
        /// </summary>
        public static readonly string COLUMN_DATEREVISED = "DateRevised";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// Provides access to the name of the primary key column (RecControlChecklistID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "RecControlChecklistID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "RecControlChecklistHdr";

        /// <summary>
        /// Provides access to the name of the database
        /// </summary>
        public static readonly string DATABASE_NAME = "SkyStemArt";


        /// <summary>
        ///  CurrentAppUserInfo  for further use
        /// </summary>
        public AppUserInfo CurrentAppUserInfo { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public RecControlChecklistDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "RecControlCheckListHdr", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a RecControlCheckListInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>RecControlCheckListInfo</returns>
        protected override RecControlCheckListInfo MapObject(System.Data.IDataReader r)
        {

            RecControlCheckListInfo entity = new RecControlCheckListInfo();
            entity.RecControlCheckListID = r.GetInt32Value("RecControlCheckListID");
            entity.CheckListNumber = r.GetStringValue("CheckListNumber");
            entity.Description = r.GetStringValue("Description");
            entity.DescriptionLabelID = r.GetInt32Value("DescriptionLabelID");
            entity.StartRecPeriodID = r.GetInt32Value("StartRecPeriodID");
            entity.EndRecPeriodID = r.GetInt32Value("EndRecPeriodID");
            entity.DataImportID = r.GetInt32Value("DataImportID");
            entity.IsActive = r.GetBooleanValue("IsActive");
            entity.AddedByUserID = r.GetInt32Value("AddedByUserID");
            entity.RoleID = r.GetInt16Value("RoleID");
            entity.DateAdded = r.GetDateValue("DateAdded");
            entity.AddedBy = r.GetStringValue("AddedBy");
            entity.DateRevised = r.GetDateValue("DateRevised");
            entity.RevisedBy = r.GetStringValue("RevisedBy");
            entity.PhysicalPath = r.GetStringValue("PhysicalPath");
            entity.GLDataRecControlCheckListID = r.GetInt64Value("GLDataRecControlCheckListID");

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in RecControlCheckListInfo object
        /// </summary>
        /// <param name="o">A RecControlCheckListInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(RecControlCheckListInfo entity)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_RecControlCheckListHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parChecklistNumber = cmd.CreateParameter();
            parChecklistNumber.ParameterName = "@CheckListNumber";
            if (!string.IsNullOrEmpty(entity.CheckListNumber))
                parChecklistNumber.Value = entity.CheckListNumber;
            else
                parChecklistNumber.Value = System.DBNull.Value;
            cmdParams.Add(parChecklistNumber);

            System.Data.IDbDataParameter parDescription = cmd.CreateParameter();
            parDescription.ParameterName = "@Description";
            if (!string.IsNullOrEmpty(entity.Description))
                parDescription.Value = entity.Description;
            else
                parDescription.Value = System.DBNull.Value;
            cmdParams.Add(parDescription);

            System.Data.IDbDataParameter parDescriptionLabelID = cmd.CreateParameter();
            parDescriptionLabelID.ParameterName = "@DescriptionLabelID";
            if (entity.DescriptionLabelID.HasValue)
                parDescriptionLabelID.Value = entity.DescriptionLabelID;
            else
                parDescriptionLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parDescriptionLabelID);

            System.Data.IDbDataParameter parStartRecPeriodID = cmd.CreateParameter();
            parStartRecPeriodID.ParameterName = "@StartRecPeriodID";
            if (entity.StartRecPeriodID.HasValue)
                parStartRecPeriodID.Value = entity.StartRecPeriodID;
            else
                parStartRecPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parStartRecPeriodID);

            System.Data.IDbDataParameter parEndRecPeriodID = cmd.CreateParameter();
            parEndRecPeriodID.ParameterName = "@EndRecPeriodID";
            if (entity.EndRecPeriodID.HasValue)
                parEndRecPeriodID.Value = entity.EndRecPeriodID;
            else
                parEndRecPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parEndRecPeriodID);

            System.Data.IDbDataParameter parDataImportID = cmd.CreateParameter();
            parDataImportID.ParameterName = "@DataImportID";
            if (entity.DataImportID.HasValue)
                parDataImportID.Value = entity.DataImportID;
            else
                parDataImportID.Value = System.DBNull.Value;
            cmdParams.Add(parDataImportID);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (entity.IsActive.HasValue)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            ServiceHelper.AddCommonParametersForAddedUserID(entity.AddedByUserID, cmd, cmdParams);

            System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            if (entity.RoleID.HasValue)
                parRoleID.Value = entity.RoleID;
            else
                parRoleID.Value = System.DBNull.Value;
            cmdParams.Add(parRoleID);

            ServiceHelper.AddCommonParametersForDateAdded(entity.DateAdded, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForAddedBy(entity.AddedBy, cmd, cmdParams);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in RecControlCheckListInfo object
        /// </summary>
        /// <param name="o">A RecControlCheckListInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(RecControlCheckListInfo entity)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_RecControlChecklistHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parChecklistNumber = cmd.CreateParameter();
            parChecklistNumber.ParameterName = "@CheckListNumber";
            if (!string.IsNullOrEmpty(entity.CheckListNumber))
                parChecklistNumber.Value = entity.CheckListNumber;
            else
                parChecklistNumber.Value = System.DBNull.Value;
            cmdParams.Add(parChecklistNumber);

            System.Data.IDbDataParameter parDescription = cmd.CreateParameter();
            parDescription.ParameterName = "@Description";
            if (!string.IsNullOrEmpty(entity.Description))
                parDescription.Value = entity.Description;
            else
                parDescription.Value = System.DBNull.Value;
            cmdParams.Add(parDescription);

            System.Data.IDbDataParameter parDescriptionLabelID = cmd.CreateParameter();
            parDescriptionLabelID.ParameterName = "@DescriptionLabelID";
            if (entity.DescriptionLabelID.HasValue)
                parDescriptionLabelID.Value = entity.DescriptionLabelID;
            else
                parDescriptionLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parDescriptionLabelID);

            System.Data.IDbDataParameter parStartRecPeriodID = cmd.CreateParameter();
            parStartRecPeriodID.ParameterName = "@StartRecPeriodID";
            if (entity.StartRecPeriodID.HasValue)
                parStartRecPeriodID.Value = entity.StartRecPeriodID;
            else
                parStartRecPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parStartRecPeriodID);

            System.Data.IDbDataParameter parEndRecPeriodID = cmd.CreateParameter();
            parEndRecPeriodID.ParameterName = "@EndRecPeriodID";
            if (entity.EndRecPeriodID.HasValue)
                parEndRecPeriodID.Value = entity.EndRecPeriodID;
            else
                parEndRecPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parEndRecPeriodID);

            System.Data.IDbDataParameter parDataImportID = cmd.CreateParameter();
            parDataImportID.ParameterName = "@DataImportID";
            if (entity.DataImportID.HasValue)
                parDataImportID.Value = entity.DataImportID;
            else
                parDataImportID.Value = System.DBNull.Value;
            cmdParams.Add(parDataImportID);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (entity.IsActive.HasValue)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            ServiceHelper.AddCommonParametersForAddedUserID(entity.AddedByUserID, cmd, cmdParams);

            System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            if (entity.RoleID.HasValue)
                parRoleID.Value = entity.RoleID;
            else
                parRoleID.Value = System.DBNull.Value;
            cmdParams.Add(parRoleID);

            ServiceHelper.AddCommonParametersForDateRevised(entity.DateRevised, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForRevisedBy(entity.RevisedBy, cmd, cmdParams);

            System.Data.IDbDataParameter pkparRecControlCheckListID = cmd.CreateParameter();
            pkparRecControlCheckListID.ParameterName = "@RecControlCheckListID";
            pkparRecControlCheckListID.Value = entity.RecControlCheckListID;
            cmdParams.Add(pkparRecControlCheckListID);

            return cmd;
        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_RecControlChecklistHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RecControlChecklistID";
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
            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_RecControlChecklistHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RecControlChecklistID";
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
        public IList<RecControlCheckListInfo> SelectAllByDataImportID(object id)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_RecControlChecklistByDataImportID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DataImportID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }


        protected override void CustomSave(RecControlCheckListInfo o, IDbConnection connection)
        {
            string query = QueryHelper.GetSqlServerLastInsertedCommand(RecControlChecklistDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);
        }

        protected override void CustomSave(RecControlCheckListInfo o, IDbConnection connection, IDbTransaction transaction)
        {
            string query = QueryHelper.GetSqlServerLastInsertedCommand(RecControlChecklistDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);
        }

        private void MapIdentity(RecControlCheckListInfo entity, object id)
        {
            entity.RecControlCheckListID = Convert.ToInt32(id);
        }
    }
}
