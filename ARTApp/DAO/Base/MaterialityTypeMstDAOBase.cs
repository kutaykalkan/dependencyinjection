

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

    public abstract class MaterialityTypeMstDAOBase : CustomAbstractDAO<MaterialityTypeMstInfo>
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
        /// A static representation of column MaterialityType
        /// </summary>
        public static readonly string COLUMN_MATERIALITYTYPE = "MaterialityType";
        /// <summary>
        /// A static representation of column MaterialityTypeID
        /// </summary>
        public static readonly string COLUMN_MATERIALITYTYPEID = "MaterialityTypeID";
        /// <summary>
        /// A static representation of column MaterialityTypeLabelID
        /// </summary>
        public static readonly string COLUMN_MATERIALITYTYPELABELID = "MaterialityTypeLabelID";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// Provides access to the name of the primary key column (MaterialityTypeID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "MaterialityTypeID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "MaterialityTypeMst";

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
        public MaterialityTypeMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "MaterialityTypeMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a MaterialityTypeMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>MaterialityTypeMstInfo</returns>
        protected override MaterialityTypeMstInfo MapObject(System.Data.IDataReader r)
        {

            MaterialityTypeMstInfo entity = new MaterialityTypeMstInfo();


            try
            {
                int ordinal = r.GetOrdinal("MaterialityTypeID");
                if (!r.IsDBNull(ordinal)) entity.MaterialityTypeID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("MaterialityType");
                if (!r.IsDBNull(ordinal)) entity.MaterialityType = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("MaterialityTypeLabelID");
                if (!r.IsDBNull(ordinal)) entity.MaterialityTypeLabelID = ((System.Int32)(r.GetValue(ordinal)));
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
        /// in MaterialityTypeMstInfo object
        /// </summary>
        /// <param name="o">A MaterialityTypeMstInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(MaterialityTypeMstInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_MaterialityTypeMst");
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

            System.Data.IDbDataParameter parMaterialityType = cmd.CreateParameter();
            parMaterialityType.ParameterName = "@MaterialityType";
            if (!entity.IsMaterialityTypeNull)
                parMaterialityType.Value = entity.MaterialityType;
            else
                parMaterialityType.Value = System.DBNull.Value;
            cmdParams.Add(parMaterialityType);

            System.Data.IDbDataParameter parMaterialityTypeLabelID = cmd.CreateParameter();
            parMaterialityTypeLabelID.ParameterName = "@MaterialityTypeLabelID";
            if (!entity.IsMaterialityTypeLabelIDNull)
                parMaterialityTypeLabelID.Value = entity.MaterialityTypeLabelID;
            else
                parMaterialityTypeLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parMaterialityTypeLabelID);

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
        /// in MaterialityTypeMstInfo object
        /// </summary>
        /// <param name="o">A MaterialityTypeMstInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(MaterialityTypeMstInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_MaterialityTypeMst");
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

            System.Data.IDbDataParameter parMaterialityType = cmd.CreateParameter();
            parMaterialityType.ParameterName = "@MaterialityType";
            if (!entity.IsMaterialityTypeNull)
                parMaterialityType.Value = entity.MaterialityType;
            else
                parMaterialityType.Value = System.DBNull.Value;
            cmdParams.Add(parMaterialityType);

            System.Data.IDbDataParameter parMaterialityTypeLabelID = cmd.CreateParameter();
            parMaterialityTypeLabelID.ParameterName = "@MaterialityTypeLabelID";
            if (!entity.IsMaterialityTypeLabelIDNull)
                parMaterialityTypeLabelID.Value = entity.MaterialityTypeLabelID;
            else
                parMaterialityTypeLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parMaterialityTypeLabelID);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter pkparMaterialityTypeID = cmd.CreateParameter();
            pkparMaterialityTypeID.ParameterName = "@MaterialityTypeID";
            pkparMaterialityTypeID.Value = entity.MaterialityTypeID;
            cmdParams.Add(pkparMaterialityTypeID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_MaterialityTypeMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MaterialityTypeID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_MaterialityTypeMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MaterialityTypeID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }







        protected override void CustomSave(MaterialityTypeMstInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(MaterialityTypeMstDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(MaterialityTypeMstInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(MaterialityTypeMstDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(MaterialityTypeMstInfo entity, object id)
        {
            entity.MaterialityTypeID = Convert.ToInt16(id);
        }









        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<MaterialityTypeMstInfo> SelectMaterialityTypeMstDetailsAssociatedToCompanyHdrByCompanySetting(CompanyHdrInfo entity)
        {
            return this.SelectMaterialityTypeMstDetailsAssociatedToCompanyHdrByCompanySetting(entity.CompanyID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<MaterialityTypeMstInfo> SelectMaterialityTypeMstDetailsAssociatedToCompanyHdrByCompanySetting(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [MaterialityTypeMst] INNER JOIN [CompanySetting] ON [MaterialityTypeMst].[MaterialityTypeID] = [CompanySetting].[MaterialityTypeID] INNER JOIN [CompanyHdr] ON [CompanySetting].[CompanyID] = [CompanyHdr].[CompanyID]  WHERE  [CompanyHdr].[CompanyID] = @CompanyID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyID";
            par.Value = id;

            cmdParams.Add(par);
            List<MaterialityTypeMstInfo> objMaterialityTypeMstEntityColl = new List<MaterialityTypeMstInfo>(this.Select(cmd));
            return objMaterialityTypeMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<MaterialityTypeMstInfo> SelectMaterialityTypeMstDetailsAssociatedToReconciliationPeriodByCompanySetting(ReconciliationPeriodInfo entity)
        {
            return this.SelectMaterialityTypeMstDetailsAssociatedToReconciliationPeriodByCompanySetting(entity.ReconciliationPeriodID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<MaterialityTypeMstInfo> SelectMaterialityTypeMstDetailsAssociatedToReconciliationPeriodByCompanySetting(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [MaterialityTypeMst] INNER JOIN [CompanySetting] ON [MaterialityTypeMst].[MaterialityTypeID] = [CompanySetting].[MaterialityTypeID] INNER JOIN [ReconciliationPeriod] ON [CompanySetting].[OpenReconciliationPeriodID] = [ReconciliationPeriod].[ReconciliationPeriodID]  WHERE  [ReconciliationPeriod].[ReconciliationPeriodID] = @ReconciliationPeriodID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationPeriodID";
            par.Value = id;

            cmdParams.Add(par);
            List<MaterialityTypeMstInfo> objMaterialityTypeMstEntityColl = new List<MaterialityTypeMstInfo>(this.Select(cmd));
            return objMaterialityTypeMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<MaterialityTypeMstInfo> SelectMaterialityTypeMstDetailsAssociatedToCompanyHdrByReconciliationPeriod(CompanyHdrInfo entity)
        {
            return this.SelectMaterialityTypeMstDetailsAssociatedToCompanyHdrByReconciliationPeriod(entity.CompanyID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<MaterialityTypeMstInfo> SelectMaterialityTypeMstDetailsAssociatedToCompanyHdrByReconciliationPeriod(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [MaterialityTypeMst] INNER JOIN [ReconciliationPeriod] ON [MaterialityTypeMst].[MaterialityTypeID] = [ReconciliationPeriod].[MaterialityTypeID] INNER JOIN [CompanyHdr] ON [ReconciliationPeriod].[CompanyID] = [CompanyHdr].[CompanyID]  WHERE  [CompanyHdr].[CompanyID] = @CompanyID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyID";
            par.Value = id;

            cmdParams.Add(par);
            List<MaterialityTypeMstInfo> objMaterialityTypeMstEntityColl = new List<MaterialityTypeMstInfo>(this.Select(cmd));
            return objMaterialityTypeMstEntityColl;
        }

    }
}
