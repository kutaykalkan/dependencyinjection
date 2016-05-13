

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO.Base
{

    public abstract class ReconciliationCategoryTypeMstDAOBase : CustomAbstractDAO<ReconciliationCategoryTypeMstInfo>
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
        /// A static representation of column Description
        /// </summary>
        public static readonly string COLUMN_DESCRIPTION = "Description";
        /// <summary>
        /// A static representation of column HostName
        /// </summary>
        public static readonly string COLUMN_HOSTNAME = "HostName";
        /// <summary>
        /// A static representation of column IsActive
        /// </summary>
        public static readonly string COLUMN_ISACTIVE = "IsActive";
        /// <summary>
        /// A static representation of column ReconciliationCategoryID
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONCATEGORYID = "ReconciliationCategoryID";
        /// <summary>
        /// A static representation of column ReconciliationCategoryType
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONCATEGORYTYPE = "ReconciliationCategoryType";
        /// <summary>
        /// A static representation of column ReconciliationCategoryTypeID
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONCATEGORYTYPEID = "ReconciliationCategoryTypeID";
        /// <summary>
        /// A static representation of column ReconciliationCategoryTypeLabelID
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONCATEGORYTYPELABELID = "ReconciliationCategoryTypeLabelID";
        /// <summary>
        /// A static representation of column ReconciliationTemplateID
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONTEMPLATEID = "ReconciliationTemplateID";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// Provides access to the name of the primary key column (ReconciliationCategoryTypeID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "ReconciliationCategoryTypeID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "ReconciliationCategoryTypeMst";

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
        public ReconciliationCategoryTypeMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "ReconciliationCategoryTypeMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a ReconciliationCategoryTypeMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>ReconciliationCategoryTypeMstInfo</returns>
        protected override ReconciliationCategoryTypeMstInfo MapObject(System.Data.IDataReader r)
        {

            ReconciliationCategoryTypeMstInfo entity = new ReconciliationCategoryTypeMstInfo();


            try
            {
                int ordinal = r.GetOrdinal("ReconciliationCategoryTypeID");
                if (!r.IsDBNull(ordinal)) entity.ReconciliationCategoryTypeID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ReconciliationCategoryType");
                if (!r.IsDBNull(ordinal)) entity.ReconciliationCategoryType = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ReconciliationCategoryTypeLabelID");
                if (!r.IsDBNull(ordinal)) entity.ReconciliationCategoryTypeLabelID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ReconciliationTemplateID");
                if (!r.IsDBNull(ordinal)) entity.ReconciliationTemplateID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ReconciliationCategoryID");
                if (!r.IsDBNull(ordinal)) entity.ReconciliationCategoryID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("Description");
                if (!r.IsDBNull(ordinal)) entity.Description = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("IsActive");
                if (!r.IsDBNull(ordinal)) entity.IsActive = ((System.Boolean)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("DateAdded");
                if (!r.IsDBNull(ordinal)) entity.DateAdded = ((System.DateTime)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("AddedBy");
                if (!r.IsDBNull(ordinal)) entity.AddedBy = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("DateRevised");
                if (!r.IsDBNull(ordinal)) entity.DateRevised = ((System.DateTime)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("RevisedBy");
                if (!r.IsDBNull(ordinal)) entity.RevisedBy = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("HostName");
                if (!r.IsDBNull(ordinal)) entity.HostName = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in ReconciliationCategoryTypeMstInfo object
        /// </summary>
        /// <param name="o">A ReconciliationCategoryTypeMstInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(ReconciliationCategoryTypeMstInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_ReconciliationCategoryTypeMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (!entity.IsDateAddedNull)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (!entity.IsDateRevisedNull)
                parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parDescription = cmd.CreateParameter();
            parDescription.ParameterName = "@Description";
            if (!entity.IsDescriptionNull)
                parDescription.Value = entity.Description;
            else
                parDescription.Value = System.DBNull.Value;
            cmdParams.Add(parDescription);

            System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            parHostName.ParameterName = "@HostName";
            if (!entity.IsHostNameNull)
                parHostName.Value = entity.HostName;
            else
                parHostName.Value = System.DBNull.Value;
            cmdParams.Add(parHostName);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (!entity.IsIsActiveNull)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parReconciliationCategoryID = cmd.CreateParameter();
            parReconciliationCategoryID.ParameterName = "@ReconciliationCategoryID";
            if (!entity.IsReconciliationCategoryIDNull)
                parReconciliationCategoryID.Value = entity.ReconciliationCategoryID;
            else
                parReconciliationCategoryID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationCategoryID);

            System.Data.IDbDataParameter parReconciliationCategoryType = cmd.CreateParameter();
            parReconciliationCategoryType.ParameterName = "@ReconciliationCategoryType";
            if (!entity.IsReconciliationCategoryTypeNull)
                parReconciliationCategoryType.Value = entity.ReconciliationCategoryType;
            else
                parReconciliationCategoryType.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationCategoryType);

            System.Data.IDbDataParameter parReconciliationCategoryTypeLabelID = cmd.CreateParameter();
            parReconciliationCategoryTypeLabelID.ParameterName = "@ReconciliationCategoryTypeLabelID";
            if (!entity.IsReconciliationCategoryTypeLabelIDNull)
                parReconciliationCategoryTypeLabelID.Value = entity.ReconciliationCategoryTypeLabelID;
            else
                parReconciliationCategoryTypeLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationCategoryTypeLabelID);

            System.Data.IDbDataParameter parReconciliationTemplateID = cmd.CreateParameter();
            parReconciliationTemplateID.ParameterName = "@ReconciliationTemplateID";
            if (!entity.IsReconciliationTemplateIDNull)
                parReconciliationTemplateID.Value = entity.ReconciliationTemplateID;
            else
                parReconciliationTemplateID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationTemplateID);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in ReconciliationCategoryTypeMstInfo object
        /// </summary>
        /// <param name="o">A ReconciliationCategoryTypeMstInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(ReconciliationCategoryTypeMstInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_ReconciliationCategoryTypeMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (!entity.IsDateAddedNull)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (!entity.IsDateRevisedNull)
                parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parDescription = cmd.CreateParameter();
            parDescription.ParameterName = "@Description";
            if (!entity.IsDescriptionNull)
                parDescription.Value = entity.Description;
            else
                parDescription.Value = System.DBNull.Value;
            cmdParams.Add(parDescription);

            System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            parHostName.ParameterName = "@HostName";
            if (!entity.IsHostNameNull)
                parHostName.Value = entity.HostName;
            else
                parHostName.Value = System.DBNull.Value;
            cmdParams.Add(parHostName);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (!entity.IsIsActiveNull)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parReconciliationCategoryID = cmd.CreateParameter();
            parReconciliationCategoryID.ParameterName = "@ReconciliationCategoryID";
            if (!entity.IsReconciliationCategoryIDNull)
                parReconciliationCategoryID.Value = entity.ReconciliationCategoryID;
            else
                parReconciliationCategoryID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationCategoryID);

            System.Data.IDbDataParameter parReconciliationCategoryType = cmd.CreateParameter();
            parReconciliationCategoryType.ParameterName = "@ReconciliationCategoryType";
            if (!entity.IsReconciliationCategoryTypeNull)
                parReconciliationCategoryType.Value = entity.ReconciliationCategoryType;
            else
                parReconciliationCategoryType.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationCategoryType);

            System.Data.IDbDataParameter parReconciliationCategoryTypeLabelID = cmd.CreateParameter();
            parReconciliationCategoryTypeLabelID.ParameterName = "@ReconciliationCategoryTypeLabelID";
            if (!entity.IsReconciliationCategoryTypeLabelIDNull)
                parReconciliationCategoryTypeLabelID.Value = entity.ReconciliationCategoryTypeLabelID;
            else
                parReconciliationCategoryTypeLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationCategoryTypeLabelID);

            System.Data.IDbDataParameter parReconciliationTemplateID = cmd.CreateParameter();
            parReconciliationTemplateID.ParameterName = "@ReconciliationTemplateID";
            if (!entity.IsReconciliationTemplateIDNull)
                parReconciliationTemplateID.Value = entity.ReconciliationTemplateID;
            else
                parReconciliationTemplateID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationTemplateID);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter pkparReconciliationCategoryTypeID = cmd.CreateParameter();
            pkparReconciliationCategoryTypeID.ParameterName = "@ReconciliationCategoryTypeID";
            pkparReconciliationCategoryTypeID.Value = entity.ReconciliationCategoryTypeID;
            cmdParams.Add(pkparReconciliationCategoryTypeID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_ReconciliationCategoryTypeMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationCategoryTypeID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_ReconciliationCategoryTypeMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationCategoryTypeID";
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
        public IList<ReconciliationCategoryTypeMstInfo> SelectAllByReconciliationTemplateID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_ReconciliationCategoryTypeMstByReconciliationTemplateID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationTemplateID";
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
        public IList<ReconciliationCategoryTypeMstInfo> SelectAllByReconciliationCategoryID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_ReconciliationCategoryTypeMstByReconciliationCategoryID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationCategoryID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(ReconciliationCategoryTypeMstInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(ReconciliationCategoryTypeMstDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(ReconciliationCategoryTypeMstInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(ReconciliationCategoryTypeMstDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(ReconciliationCategoryTypeMstInfo entity, object id)
        {
            entity.ReconciliationCategoryTypeID = Convert.ToInt16(id);
        }








        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationCategoryTypeMstInfo> SelectReconciliationCategoryTypeMstDetailsAssociatedToGLDataHdrByGLReconciliationItemInput(GLDataHdrInfo entity)
        {
            return this.SelectReconciliationCategoryTypeMstDetailsAssociatedToGLDataHdrByGLReconciliationItemInput(entity.GLDataID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationCategoryTypeMstInfo> SelectReconciliationCategoryTypeMstDetailsAssociatedToGLDataHdrByGLReconciliationItemInput(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReconciliationCategoryTypeMst] INNER JOIN [GLReconciliationItemInput] ON [ReconciliationCategoryTypeMst].[ReconciliationCategoryTypeID] = [GLReconciliationItemInput].[ReconciliationCategoryTypeID] INNER JOIN [GLDataHdr] ON [GLReconciliationItemInput].[GLDataID] = [GLDataHdr].[GLDataID]  WHERE  [GLDataHdr].[GLDataID] = @GLDataID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReconciliationCategoryTypeMstInfo> objReconciliationCategoryTypeMstEntityColl = new List<ReconciliationCategoryTypeMstInfo>(this.Select(cmd));
            return objReconciliationCategoryTypeMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationCategoryTypeMstInfo> SelectReconciliationCategoryTypeMstDetailsAssociatedToReconciliationCategoryMstByGLReconciliationItemInput(ReconciliationCategoryMstInfo entity)
        {
            return this.SelectReconciliationCategoryTypeMstDetailsAssociatedToReconciliationCategoryMstByGLReconciliationItemInput(entity.ReconciliationCategoryID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationCategoryTypeMstInfo> SelectReconciliationCategoryTypeMstDetailsAssociatedToReconciliationCategoryMstByGLReconciliationItemInput(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReconciliationCategoryTypeMst] INNER JOIN [GLReconciliationItemInput] ON [ReconciliationCategoryTypeMst].[ReconciliationCategoryTypeID] = [GLReconciliationItemInput].[ReconciliationCategoryTypeID] INNER JOIN [ReconciliationCategoryMst] ON [GLReconciliationItemInput].[ReconciliationCategoryID] = [ReconciliationCategoryMst].[ReconciliationCategoryID]  WHERE  [ReconciliationCategoryMst].[ReconciliationCategoryID] = @ReconciliationCategoryID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationCategoryID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReconciliationCategoryTypeMstInfo> objReconciliationCategoryTypeMstEntityColl = new List<ReconciliationCategoryTypeMstInfo>(this.Select(cmd));
            return objReconciliationCategoryTypeMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationCategoryTypeMstInfo> SelectReconciliationCategoryTypeMstDetailsAssociatedToDataImportHdrByGLReconciliationItemInput(DataImportHdrInfo entity)
        {
            return this.SelectReconciliationCategoryTypeMstDetailsAssociatedToDataImportHdrByGLReconciliationItemInput(entity.DataImportID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationCategoryTypeMstInfo> SelectReconciliationCategoryTypeMstDetailsAssociatedToDataImportHdrByGLReconciliationItemInput(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReconciliationCategoryTypeMst] INNER JOIN [GLReconciliationItemInput] ON [ReconciliationCategoryTypeMst].[ReconciliationCategoryTypeID] = [GLReconciliationItemInput].[ReconciliationCategoryTypeID] INNER JOIN [DataImportHdr] ON [GLReconciliationItemInput].[DataImportID] = [DataImportHdr].[DataImportID]  WHERE  [DataImportHdr].[DataImportID] = @DataImportID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DataImportID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReconciliationCategoryTypeMstInfo> objReconciliationCategoryTypeMstEntityColl = new List<ReconciliationCategoryTypeMstInfo>(this.Select(cmd));
            return objReconciliationCategoryTypeMstEntityColl;
        }

    }
}
