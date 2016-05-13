

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

    public abstract class DataImportStatusMstDAOBase : CustomAbstractDAO<DataImportStatusMstInfo>
    {

        /// <summary>
        /// A static representation of column AddedBy
        /// </summary>
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
        /// <summary>
        /// A static representation of column DataImportStatus
        /// </summary>
        public static readonly string COLUMN_DATAIMPORTSTATUS = "DataImportStatus";
        /// <summary>
        /// A static representation of column DataImportStatusID
        /// </summary>
        public static readonly string COLUMN_DATAIMPORTSTATUSID = "DataImportStatusID";
        /// <summary>
        /// A static representation of column DataImportStatusLabelID
        /// </summary>
        public static readonly string COLUMN_DATAIMPORTSTATUSLABELID = "DataImportStatusLabelID";
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
        /// Provides access to the name of the primary key column (DataImportStatusID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "DataImportStatusID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "DataImportStatusMst";

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
        public DataImportStatusMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "DataImportStatusMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;

        }

        /// <summary>
        /// Maps the IDataReader values to a DataImportStatusMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>DataImportStatusMstInfo</returns>
        protected override DataImportStatusMstInfo MapObject(System.Data.IDataReader r)
        {

            DataImportStatusMstInfo entity = new DataImportStatusMstInfo();


            try
            {
                int ordinal = r.GetOrdinal("DataImportStatusID");
                if (!r.IsDBNull(ordinal)) entity.DataImportStatusID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("DataImportStatus");
                if (!r.IsDBNull(ordinal)) entity.DataImportStatus = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("DataImportStatusLabelID");
                if (!r.IsDBNull(ordinal)) entity.DataImportStatusLabelID = ((System.Int32)(r.GetValue(ordinal)));
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
        /// in DataImportStatusMstInfo object
        /// </summary>
        /// <param name="o">A DataImportStatusMstInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(DataImportStatusMstInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_DataImportStatusMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDataImportStatus = cmd.CreateParameter();
            parDataImportStatus.ParameterName = "@DataImportStatus";
            if (!entity.IsDataImportStatusNull)
                parDataImportStatus.Value = entity.DataImportStatus;
            else
                parDataImportStatus.Value = System.DBNull.Value;
            cmdParams.Add(parDataImportStatus);

            System.Data.IDbDataParameter parDataImportStatusLabelID = cmd.CreateParameter();
            parDataImportStatusLabelID.ParameterName = "@DataImportStatusLabelID";
            if (!entity.IsDataImportStatusLabelIDNull)
                parDataImportStatusLabelID.Value = entity.DataImportStatusLabelID;
            else
                parDataImportStatusLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parDataImportStatusLabelID);

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
        /// in DataImportStatusMstInfo object
        /// </summary>
        /// <param name="o">A DataImportStatusMstInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(DataImportStatusMstInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_DataImportStatusMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDataImportStatus = cmd.CreateParameter();
            parDataImportStatus.ParameterName = "@DataImportStatus";
            if (!entity.IsDataImportStatusNull)
                parDataImportStatus.Value = entity.DataImportStatus;
            else
                parDataImportStatus.Value = System.DBNull.Value;
            cmdParams.Add(parDataImportStatus);

            System.Data.IDbDataParameter parDataImportStatusLabelID = cmd.CreateParameter();
            parDataImportStatusLabelID.ParameterName = "@DataImportStatusLabelID";
            if (!entity.IsDataImportStatusLabelIDNull)
                parDataImportStatusLabelID.Value = entity.DataImportStatusLabelID;
            else
                parDataImportStatusLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parDataImportStatusLabelID);

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

            System.Data.IDbDataParameter pkparDataImportStatusID = cmd.CreateParameter();
            pkparDataImportStatusID.ParameterName = "@DataImportStatusID";
            pkparDataImportStatusID.Value = entity.DataImportStatusID;
            cmdParams.Add(pkparDataImportStatusID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_DataImportStatusMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DataImportStatusID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_DataImportStatusMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DataImportStatusID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }







        protected override void CustomSave(DataImportStatusMstInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(DataImportStatusMstDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(DataImportStatusMstInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(DataImportStatusMstDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(DataImportStatusMstInfo entity, object id)
        {
            entity.DataImportStatusID = Convert.ToInt16(id);
        }








        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DataImportStatusMstInfo> SelectDataImportStatusMstDetailsAssociatedToCompanyHdrByDataImportHdr(CompanyHdrInfo entity)
        {
            return this.SelectDataImportStatusMstDetailsAssociatedToCompanyHdrByDataImportHdr(entity.CompanyID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DataImportStatusMstInfo> SelectDataImportStatusMstDetailsAssociatedToCompanyHdrByDataImportHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [DataImportStatusMst] INNER JOIN [DataImportHdr] ON [DataImportStatusMst].[DataImportStatusID] = [DataImportHdr].[DataImportStatusID] INNER JOIN [CompanyHdr] ON [DataImportHdr].[CompanyID] = [CompanyHdr].[CompanyID]  WHERE  [CompanyHdr].[CompanyID] = @CompanyID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyID";
            par.Value = id;

            cmdParams.Add(par);
            List<DataImportStatusMstInfo> objDataImportStatusMstEntityColl = new List<DataImportStatusMstInfo>(this.Select(cmd));
            return objDataImportStatusMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DataImportStatusMstInfo> SelectDataImportStatusMstDetailsAssociatedToReconciliationPeriodByDataImportHdr(ReconciliationPeriodInfo entity)
        {
            return this.SelectDataImportStatusMstDetailsAssociatedToReconciliationPeriodByDataImportHdr(entity.ReconciliationPeriodID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DataImportStatusMstInfo> SelectDataImportStatusMstDetailsAssociatedToReconciliationPeriodByDataImportHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [DataImportStatusMst] INNER JOIN [DataImportHdr] ON [DataImportStatusMst].[DataImportStatusID] = [DataImportHdr].[DataImportStatusID] INNER JOIN [ReconciliationPeriod] ON [DataImportHdr].[ReonciliationPeriodID] = [ReconciliationPeriod].[ReconciliationPeriodID]  WHERE  [ReconciliationPeriod].[ReconciliationPeriodID] = @ReconciliationPeriodID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationPeriodID";
            par.Value = id;

            cmdParams.Add(par);
            List<DataImportStatusMstInfo> objDataImportStatusMstEntityColl = new List<DataImportStatusMstInfo>(this.Select(cmd));
            return objDataImportStatusMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DataImportStatusMstInfo> SelectDataImportStatusMstDetailsAssociatedToDataImportTypeMstByDataImportHdr(DataImportTypeMstInfo entity)
        {
            return this.SelectDataImportStatusMstDetailsAssociatedToDataImportTypeMstByDataImportHdr(entity.DataImportTypeID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<DataImportStatusMstInfo> SelectDataImportStatusMstDetailsAssociatedToDataImportTypeMstByDataImportHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [DataImportStatusMst] INNER JOIN [DataImportHdr] ON [DataImportStatusMst].[DataImportStatusID] = [DataImportHdr].[DataImportStatusID] INNER JOIN [DataImportTypeMst] ON [DataImportHdr].[DataImportTypeID] = [DataImportTypeMst].[DataImportTypeID]  WHERE  [DataImportTypeMst].[DataImportTypeID] = @DataImportTypeID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DataImportTypeID";
            par.Value = id;

            cmdParams.Add(par);
            List<DataImportStatusMstInfo> objDataImportStatusMstEntityColl = new List<DataImportStatusMstInfo>(this.Select(cmd));
            return objDataImportStatusMstEntityColl;
        }

    }
}
