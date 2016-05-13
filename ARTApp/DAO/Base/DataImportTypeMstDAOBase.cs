

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

    public abstract class DataImportTypeMstDAOBase : CustomAbstractDAO<DataImportTypeMstInfo>
    {

        /// <summary>
        /// A static representation of column AddedBy
        /// </summary>
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
        /// <summary>
        /// A static representation of column DataImportType
        /// </summary>
        public static readonly string COLUMN_DATAIMPORTTYPE = "DataImportType";
        /// <summary>
        /// A static representation of column DataImportTypeID
        /// </summary>
        public static readonly string COLUMN_DATAIMPORTTYPEID = "DataImportTypeID";
        /// <summary>
        /// A static representation of column DataImportTypeLabelID
        /// </summary>
        public static readonly string COLUMN_DATAIMPORTTYPELABELID = "DataImportTypeLabelID";
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
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// Provides access to the name of the primary key column (DataImportTypeID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "DataImportTypeID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "DataImportTypeMst";

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
        public DataImportTypeMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "DataImportTypeMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a DataImportTypeMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>DataImportTypeMstInfo</returns>
        protected override DataImportTypeMstInfo MapObject(System.Data.IDataReader r)
        {

            DataImportTypeMstInfo entity = new DataImportTypeMstInfo();


            try
            {
                int ordinal = r.GetOrdinal("RoleID");
                if (!r.IsDBNull(ordinal)) entity.RoleID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("DataImportTypeID");
                if (!r.IsDBNull(ordinal)) entity.DataImportTypeID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("DataImportType");
                if (!r.IsDBNull(ordinal)) entity.DataImportType = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("DataImportTypeLabelID");
                if (!r.IsDBNull(ordinal)) entity.DataImportTypeLabelID = ((System.Int32)(r.GetValue(ordinal)));
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
                int ordinal = r.GetOrdinal("UploadRulesLabelID");
                if (!r.IsDBNull(ordinal)) entity.UploadRulesLabelID = ((System.Int32)(r.GetValue(ordinal)));
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
        /// in DataImportTypeMstInfo object
        /// </summary>
        /// <param name="o">A DataImportTypeMstInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(DataImportTypeMstInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_DataImportTypeMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDataImportType = cmd.CreateParameter();
            parDataImportType.ParameterName = "@DataImportType";
            if (!entity.IsDataImportTypeNull)
                parDataImportType.Value = entity.DataImportType;
            else
                parDataImportType.Value = System.DBNull.Value;
            cmdParams.Add(parDataImportType);

            System.Data.IDbDataParameter parDataImportTypeLabelID = cmd.CreateParameter();
            parDataImportTypeLabelID.ParameterName = "@DataImportTypeLabelID";
            if (!entity.IsDataImportTypeLabelIDNull)
                parDataImportTypeLabelID.Value = entity.DataImportTypeLabelID;
            else
                parDataImportTypeLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parDataImportTypeLabelID);

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
        /// in DataImportTypeMstInfo object
        /// </summary>
        /// <param name="o">A DataImportTypeMstInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(DataImportTypeMstInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_DataImportTypeMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDataImportType = cmd.CreateParameter();
            parDataImportType.ParameterName = "@DataImportType";
            if (!entity.IsDataImportTypeNull)
                parDataImportType.Value = entity.DataImportType;
            else
                parDataImportType.Value = System.DBNull.Value;
            cmdParams.Add(parDataImportType);

            System.Data.IDbDataParameter parDataImportTypeLabelID = cmd.CreateParameter();
            parDataImportTypeLabelID.ParameterName = "@DataImportTypeLabelID";
            if (!entity.IsDataImportTypeLabelIDNull)
                parDataImportTypeLabelID.Value = entity.DataImportTypeLabelID;
            else
                parDataImportTypeLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parDataImportTypeLabelID);

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

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter pkparDataImportTypeID = cmd.CreateParameter();
            pkparDataImportTypeID.ParameterName = "@DataImportTypeID";
            pkparDataImportTypeID.Value = entity.DataImportTypeID;
            cmdParams.Add(pkparDataImportTypeID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_DataImportTypeMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DataImportTypeID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_DataImportTypeMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DataImportTypeID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }







        protected override void CustomSave(DataImportTypeMstInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(DataImportTypeMstDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(DataImportTypeMstInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(DataImportTypeMstDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(DataImportTypeMstInfo entity, object id)
        {
            entity.DataImportTypeID = Convert.ToInt16(id);
        }









        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DataImportTypeMstInfo> SelectDataImportTypeMstDetailsAssociatedToCompanyHdrByDataImportHdr(CompanyHdrInfo entity)
        {
            return this.SelectDataImportTypeMstDetailsAssociatedToCompanyHdrByDataImportHdr(entity.CompanyID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DataImportTypeMstInfo> SelectDataImportTypeMstDetailsAssociatedToCompanyHdrByDataImportHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [DataImportTypeMst] INNER JOIN [DataImportHdr] ON [DataImportTypeMst].[DataImportTypeID] = [DataImportHdr].[DataImportTypeID] INNER JOIN [CompanyHdr] ON [DataImportHdr].[CompanyID] = [CompanyHdr].[CompanyID]  WHERE  [CompanyHdr].[CompanyID] = @CompanyID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyID";
            par.Value = id;

            cmdParams.Add(par);
            List<DataImportTypeMstInfo> objDataImportTypeMstEntityColl = new List<DataImportTypeMstInfo>(this.Select(cmd));
            return objDataImportTypeMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DataImportTypeMstInfo> SelectDataImportTypeMstDetailsAssociatedToReconciliationPeriodByDataImportHdr(ReconciliationPeriodInfo entity)
        {
            return this.SelectDataImportTypeMstDetailsAssociatedToReconciliationPeriodByDataImportHdr(entity.ReconciliationPeriodID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DataImportTypeMstInfo> SelectDataImportTypeMstDetailsAssociatedToReconciliationPeriodByDataImportHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [DataImportTypeMst] INNER JOIN [DataImportHdr] ON [DataImportTypeMst].[DataImportTypeID] = [DataImportHdr].[DataImportTypeID] INNER JOIN [ReconciliationPeriod] ON [DataImportHdr].[ReonciliationPeriodID] = [ReconciliationPeriod].[ReconciliationPeriodID]  WHERE  [ReconciliationPeriod].[ReconciliationPeriodID] = @ReconciliationPeriodID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationPeriodID";
            par.Value = id;

            cmdParams.Add(par);
            List<DataImportTypeMstInfo> objDataImportTypeMstEntityColl = new List<DataImportTypeMstInfo>(this.Select(cmd));
            return objDataImportTypeMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DataImportTypeMstInfo> SelectDataImportTypeMstDetailsAssociatedToDataImportStatusMstByDataImportHdr(DataImportStatusMstInfo entity)
        {
            return this.SelectDataImportTypeMstDetailsAssociatedToDataImportStatusMstByDataImportHdr(entity.DataImportStatusID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DataImportTypeMstInfo> SelectDataImportTypeMstDetailsAssociatedToDataImportStatusMstByDataImportHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [DataImportTypeMst] INNER JOIN [DataImportHdr] ON [DataImportTypeMst].[DataImportTypeID] = [DataImportHdr].[DataImportTypeID] INNER JOIN [DataImportStatusMst] ON [DataImportHdr].[DataImportStatusID] = [DataImportStatusMst].[DataImportStatusID]  WHERE  [DataImportStatusMst].[DataImportStatusID] = @DataImportStatusID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DataImportStatusID";
            par.Value = id;

            cmdParams.Add(par);
            List<DataImportTypeMstInfo> objDataImportTypeMstEntityColl = new List<DataImportTypeMstInfo>(this.Select(cmd));
            return objDataImportTypeMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DataImportTypeMstInfo> SelectDataImportTypeMstDetailsAssociatedToCapabilityMstByDataImportTypeCapability(CapabilityMstInfo entity)
        {
            return this.SelectDataImportTypeMstDetailsAssociatedToCapabilityMstByDataImportTypeCapability(entity.CapabilityID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DataImportTypeMstInfo> SelectDataImportTypeMstDetailsAssociatedToCapabilityMstByDataImportTypeCapability(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [DataImportTypeMst] INNER JOIN [DataImportTypeCapability] ON [DataImportTypeMst].[DataImportTypeID] = [DataImportTypeCapability].[DataImportTypeID] INNER JOIN [CapabilityMst] ON [DataImportTypeCapability].[CapabilityID] = [CapabilityMst].[CapabilityID]  WHERE  [CapabilityMst].[CapabilityID] = @CapabilityID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CapabilityID";
            par.Value = id;

            cmdParams.Add(par);
            List<DataImportTypeMstInfo> objDataImportTypeMstEntityColl = new List<DataImportTypeMstInfo>(this.Select(cmd));
            return objDataImportTypeMstEntityColl;
        }



    }
}
