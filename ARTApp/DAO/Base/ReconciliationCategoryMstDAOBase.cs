

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

    public abstract class ReconciliationCategoryMstDAOBase : CustomAbstractDAO<ReconciliationCategoryMstInfo>
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
        /// A static representation of column ReconciliationCategory
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONCATEGORY = "ReconciliationCategory";
        /// <summary>
        /// A static representation of column ReconciliationCategoryID
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONCATEGORYID = "ReconciliationCategoryID";
        /// <summary>
        /// A static representation of column ReconciliationCategoryLabelID
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONCATEGORYLABELID = "ReconciliationCategoryLabelID";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// Provides access to the name of the primary key column (ReconciliationCategoryID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "ReconciliationCategoryID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "ReconciliationCategoryMst";

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
        public ReconciliationCategoryMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "ReconciliationCategoryMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a ReconciliationCategoryMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>ReconciliationCategoryMstInfo</returns>
        protected override ReconciliationCategoryMstInfo MapObject(System.Data.IDataReader r)
        {

            ReconciliationCategoryMstInfo entity = new ReconciliationCategoryMstInfo();


            try
            {
                int ordinal = r.GetOrdinal("ReconciliationCategoryID");
                if (!r.IsDBNull(ordinal)) entity.ReconciliationCategoryID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ReconciliationCategory");
                if (!r.IsDBNull(ordinal)) entity.ReconciliationCategory = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ReconciliationCategoryLabelID");
                if (!r.IsDBNull(ordinal)) entity.ReconciliationCategoryLabelID = ((System.Int32)(r.GetValue(ordinal)));
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
        /// in ReconciliationCategoryMstInfo object
        /// </summary>
        /// <param name="o">A ReconciliationCategoryMstInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(ReconciliationCategoryMstInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_ReconciliationCategoryMst");
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

            System.Data.IDbDataParameter parReconciliationCategory = cmd.CreateParameter();
            parReconciliationCategory.ParameterName = "@ReconciliationCategory";
            if (!entity.IsReconciliationCategoryNull)
                parReconciliationCategory.Value = entity.ReconciliationCategory;
            else
                parReconciliationCategory.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationCategory);

            System.Data.IDbDataParameter parReconciliationCategoryLabelID = cmd.CreateParameter();
            parReconciliationCategoryLabelID.ParameterName = "@ReconciliationCategoryLabelID";
            if (!entity.IsReconciliationCategoryLabelIDNull)
                parReconciliationCategoryLabelID.Value = entity.ReconciliationCategoryLabelID;
            else
                parReconciliationCategoryLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationCategoryLabelID);

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
        /// in ReconciliationCategoryMstInfo object
        /// </summary>
        /// <param name="o">A ReconciliationCategoryMstInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(ReconciliationCategoryMstInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_ReconciliationCategoryMst");
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

            System.Data.IDbDataParameter parReconciliationCategory = cmd.CreateParameter();
            parReconciliationCategory.ParameterName = "@ReconciliationCategory";
            if (!entity.IsReconciliationCategoryNull)
                parReconciliationCategory.Value = entity.ReconciliationCategory;
            else
                parReconciliationCategory.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationCategory);

            System.Data.IDbDataParameter parReconciliationCategoryLabelID = cmd.CreateParameter();
            parReconciliationCategoryLabelID.ParameterName = "@ReconciliationCategoryLabelID";
            if (!entity.IsReconciliationCategoryLabelIDNull)
                parReconciliationCategoryLabelID.Value = entity.ReconciliationCategoryLabelID;
            else
                parReconciliationCategoryLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationCategoryLabelID);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter pkparReconciliationCategoryID = cmd.CreateParameter();
            pkparReconciliationCategoryID.ParameterName = "@ReconciliationCategoryID";
            pkparReconciliationCategoryID.Value = entity.ReconciliationCategoryID;
            cmdParams.Add(pkparReconciliationCategoryID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_ReconciliationCategoryMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationCategoryID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_ReconciliationCategoryMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationCategoryID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }







        protected override void CustomSave(ReconciliationCategoryMstInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(ReconciliationCategoryMstDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(ReconciliationCategoryMstInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(ReconciliationCategoryMstDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(ReconciliationCategoryMstInfo entity, object id)
        {
            entity.ReconciliationCategoryID = Convert.ToInt16(id);
        }









        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationCategoryMstInfo> SelectReconciliationCategoryMstDetailsAssociatedToGLDataHdrByGLReconciliationItemInput(GLDataHdrInfo entity)
        {
            return this.SelectReconciliationCategoryMstDetailsAssociatedToGLDataHdrByGLReconciliationItemInput(entity.GLDataID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationCategoryMstInfo> SelectReconciliationCategoryMstDetailsAssociatedToGLDataHdrByGLReconciliationItemInput(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReconciliationCategoryMst] INNER JOIN [GLReconciliationItemInput] ON [ReconciliationCategoryMst].[ReconciliationCategoryID] = [GLReconciliationItemInput].[ReconciliationCategoryID] INNER JOIN [GLDataHdr] ON [GLReconciliationItemInput].[GLDataID] = [GLDataHdr].[GLDataID]  WHERE  [GLDataHdr].[GLDataID] = @GLDataID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReconciliationCategoryMstInfo> objReconciliationCategoryMstEntityColl = new List<ReconciliationCategoryMstInfo>(this.Select(cmd));
            return objReconciliationCategoryMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationCategoryMstInfo> SelectReconciliationCategoryMstDetailsAssociatedToReconciliationCategoryTypeMstByGLReconciliationItemInput(ReconciliationCategoryTypeMstInfo entity)
        {
            return this.SelectReconciliationCategoryMstDetailsAssociatedToReconciliationCategoryTypeMstByGLReconciliationItemInput(entity.ReconciliationCategoryTypeID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationCategoryMstInfo> SelectReconciliationCategoryMstDetailsAssociatedToReconciliationCategoryTypeMstByGLReconciliationItemInput(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReconciliationCategoryMst] INNER JOIN [GLReconciliationItemInput] ON [ReconciliationCategoryMst].[ReconciliationCategoryID] = [GLReconciliationItemInput].[ReconciliationCategoryID] INNER JOIN [ReconciliationCategoryTypeMst] ON [GLReconciliationItemInput].[ReconciliationCategoryTypeID] = [ReconciliationCategoryTypeMst].[ReconciliationCategoryTypeID]  WHERE  [ReconciliationCategoryTypeMst].[ReconciliationCategoryTypeID] = @ReconciliationCategoryTypeID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationCategoryTypeID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReconciliationCategoryMstInfo> objReconciliationCategoryMstEntityColl = new List<ReconciliationCategoryMstInfo>(this.Select(cmd));
            return objReconciliationCategoryMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationCategoryMstInfo> SelectReconciliationCategoryMstDetailsAssociatedToDataImportHdrByGLReconciliationItemInput(DataImportHdrInfo entity)
        {
            return this.SelectReconciliationCategoryMstDetailsAssociatedToDataImportHdrByGLReconciliationItemInput(entity.DataImportID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationCategoryMstInfo> SelectReconciliationCategoryMstDetailsAssociatedToDataImportHdrByGLReconciliationItemInput(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReconciliationCategoryMst] INNER JOIN [GLReconciliationItemInput] ON [ReconciliationCategoryMst].[ReconciliationCategoryID] = [GLReconciliationItemInput].[ReconciliationCategoryID] INNER JOIN [DataImportHdr] ON [GLReconciliationItemInput].[DataImportID] = [DataImportHdr].[DataImportID]  WHERE  [DataImportHdr].[DataImportID] = @DataImportID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DataImportID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReconciliationCategoryMstInfo> objReconciliationCategoryMstEntityColl = new List<ReconciliationCategoryMstInfo>(this.Select(cmd));
            return objReconciliationCategoryMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationCategoryMstInfo> SelectReconciliationCategoryMstDetailsAssociatedToReconciliationTemplateMstByReconciliationCategoryTypeMst(ReconciliationTemplateMstInfo entity)
        {
            return this.SelectReconciliationCategoryMstDetailsAssociatedToReconciliationTemplateMstByReconciliationCategoryTypeMst(entity.ReconciliationTemplateID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationCategoryMstInfo> SelectReconciliationCategoryMstDetailsAssociatedToReconciliationTemplateMstByReconciliationCategoryTypeMst(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReconciliationCategoryMst] INNER JOIN [ReconciliationCategoryTypeMst] ON [ReconciliationCategoryMst].[ReconciliationCategoryID] = [ReconciliationCategoryTypeMst].[ReconciliationCategoryID] INNER JOIN [ReconciliationTemplateMst] ON [ReconciliationCategoryTypeMst].[ReconciliationTemplateID] = [ReconciliationTemplateMst].[ReconciliationTemplateID]  WHERE  [ReconciliationTemplateMst].[ReconciliationTemplateID] = @ReconciliationTemplateID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationTemplateID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReconciliationCategoryMstInfo> objReconciliationCategoryMstEntityColl = new List<ReconciliationCategoryMstInfo>(this.Select(cmd));
            return objReconciliationCategoryMstEntityColl;
        }

    }
}
