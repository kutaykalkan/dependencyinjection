

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

    public abstract class ReconciliationStatusMstDAOBase : CustomAbstractDAO<ReconciliationStatusMstInfo>
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
        /// A static representation of column ReconciliationStatus
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONSTATUS = "ReconciliationStatus";
        /// <summary>
        /// A static representation of column ReconciliationStatusID
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONSTATUSID = "ReconciliationStatusID";
        /// <summary>
        /// A static representation of column ReconciliationStatusLabelID
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONSTATUSLABELID = "ReconciliationStatusLabelID";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// Provides access to the name of the primary key column (ReconciliationStatusID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "ReconciliationStatusID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "ReconciliationStatusMst";

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
        public ReconciliationStatusMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "ReconciliationStatusMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a ReconciliationStatusMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>ReconciliationStatusMstInfo</returns>
        protected override ReconciliationStatusMstInfo MapObject(System.Data.IDataReader r)
        {
            ReconciliationStatusMstInfo entity = new ReconciliationStatusMstInfo();
            entity.ReconciliationStatusID = r.GetInt16Value("ReconciliationStatusID");
            entity.ReconciliationStatus = r.GetStringValue("ReconciliationStatus");
            entity.ReconciliationStatusLabelID = r.GetInt32Value("ReconciliationStatusLabelID");
            entity.Description = r.GetStringValue("Description");
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
        /// in ReconciliationStatusMstInfo object
        /// </summary>
        /// <param name="o">A ReconciliationStatusMstInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(ReconciliationStatusMstInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_ReconciliationStatusMst");
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

            System.Data.IDbDataParameter parReconciliationStatus = cmd.CreateParameter();
            parReconciliationStatus.ParameterName = "@ReconciliationStatus";
            if (!entity.IsReconciliationStatusNull)
                parReconciliationStatus.Value = entity.ReconciliationStatus;
            else
                parReconciliationStatus.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationStatus);

            System.Data.IDbDataParameter parReconciliationStatusLabelID = cmd.CreateParameter();
            parReconciliationStatusLabelID.ParameterName = "@ReconciliationStatusLabelID";
            if (!entity.IsReconciliationStatusLabelIDNull)
                parReconciliationStatusLabelID.Value = entity.ReconciliationStatusLabelID;
            else
                parReconciliationStatusLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationStatusLabelID);

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
        /// in ReconciliationStatusMstInfo object
        /// </summary>
        /// <param name="o">A ReconciliationStatusMstInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(ReconciliationStatusMstInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_ReconciliationStatusMst");
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

            System.Data.IDbDataParameter parReconciliationStatus = cmd.CreateParameter();
            parReconciliationStatus.ParameterName = "@ReconciliationStatus";
            if (!entity.IsReconciliationStatusNull)
                parReconciliationStatus.Value = entity.ReconciliationStatus;
            else
                parReconciliationStatus.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationStatus);

            System.Data.IDbDataParameter parReconciliationStatusLabelID = cmd.CreateParameter();
            parReconciliationStatusLabelID.ParameterName = "@ReconciliationStatusLabelID";
            if (!entity.IsReconciliationStatusLabelIDNull)
                parReconciliationStatusLabelID.Value = entity.ReconciliationStatusLabelID;
            else
                parReconciliationStatusLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationStatusLabelID);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter pkparReconciliationStatusID = cmd.CreateParameter();
            pkparReconciliationStatusID.ParameterName = "@ReconciliationStatusID";
            pkparReconciliationStatusID.Value = entity.ReconciliationStatusID;
            cmdParams.Add(pkparReconciliationStatusID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_ReconciliationStatusMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationStatusID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_ReconciliationStatusMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationStatusID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }







        protected override void CustomSave(ReconciliationStatusMstInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(ReconciliationStatusMstDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(ReconciliationStatusMstInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(ReconciliationStatusMstDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(ReconciliationStatusMstInfo entity, object id)
        {
            entity.ReconciliationStatusID = Convert.ToInt16(id);
        }








        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationStatusMstInfo> SelectReconciliationStatusMstDetailsAssociatedToAccountHdrByGLDataHdr(AccountHdrInfo entity)
        {
            return this.SelectReconciliationStatusMstDetailsAssociatedToAccountHdrByGLDataHdr(entity.AccountID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationStatusMstInfo> SelectReconciliationStatusMstDetailsAssociatedToAccountHdrByGLDataHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReconciliationStatusMst] INNER JOIN [GLDataHdr] ON [ReconciliationStatusMst].[ReconciliationStatusID] = [GLDataHdr].[ReconciliationStatusID] INNER JOIN [AccountHdr] ON [GLDataHdr].[AccountID] = [AccountHdr].[AccountID]  WHERE  [AccountHdr].[AccountID] = @AccountID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AccountID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReconciliationStatusMstInfo> objReconciliationStatusMstEntityColl = new List<ReconciliationStatusMstInfo>(this.Select(cmd));
            return objReconciliationStatusMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationStatusMstInfo> SelectReconciliationStatusMstDetailsAssociatedToCertificationStatusMstByGLDataHdr(CertificationStatusMstInfo entity)
        {
            return this.SelectReconciliationStatusMstDetailsAssociatedToCertificationStatusMstByGLDataHdr(entity.CertificationStatusID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationStatusMstInfo> SelectReconciliationStatusMstDetailsAssociatedToCertificationStatusMstByGLDataHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReconciliationStatusMst] INNER JOIN [GLDataHdr] ON [ReconciliationStatusMst].[ReconciliationStatusID] = [GLDataHdr].[ReconciliationStatusID] INNER JOIN [CertificationStatusMst] ON [GLDataHdr].[CertificationStatusID] = [CertificationStatusMst].[CertificationStatusID]  WHERE  [CertificationStatusMst].[CertificationStatusID] = @CertificationStatusID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CertificationStatusID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReconciliationStatusMstInfo> objReconciliationStatusMstEntityColl = new List<ReconciliationStatusMstInfo>(this.Select(cmd));
            return objReconciliationStatusMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationStatusMstInfo> SelectReconciliationStatusMstDetailsAssociatedToDataImportHdrByGLDataHdr(DataImportHdrInfo entity)
        {
            return this.SelectReconciliationStatusMstDetailsAssociatedToDataImportHdrByGLDataHdr(entity.DataImportID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationStatusMstInfo> SelectReconciliationStatusMstDetailsAssociatedToDataImportHdrByGLDataHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReconciliationStatusMst] INNER JOIN [GLDataHdr] ON [ReconciliationStatusMst].[ReconciliationStatusID] = [GLDataHdr].[ReconciliationStatusID] INNER JOIN [DataImportHdr] ON [GLDataHdr].[DataImportID] = [DataImportHdr].[DataImportID]  WHERE  [DataImportHdr].[DataImportID] = @DataImportID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DataImportID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReconciliationStatusMstInfo> objReconciliationStatusMstEntityColl = new List<ReconciliationStatusMstInfo>(this.Select(cmd));
            return objReconciliationStatusMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationStatusMstInfo> SelectReconciliationStatusMstDetailsAssociatedToReconciliationPeriodByGLDataHdr(ReconciliationPeriodInfo entity)
        {
            return this.SelectReconciliationStatusMstDetailsAssociatedToReconciliationPeriodByGLDataHdr(entity.ReconciliationPeriodID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationStatusMstInfo> SelectReconciliationStatusMstDetailsAssociatedToReconciliationPeriodByGLDataHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReconciliationStatusMst] INNER JOIN [GLDataHdr] ON [ReconciliationStatusMst].[ReconciliationStatusID] = [GLDataHdr].[ReconciliationStatusID] INNER JOIN [ReconciliationPeriod] ON [GLDataHdr].[ReconciliationPeriodID] = [ReconciliationPeriod].[ReconciliationPeriodID]  WHERE  [ReconciliationPeriod].[ReconciliationPeriodID] = @ReconciliationPeriodID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationPeriodID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReconciliationStatusMstInfo> objReconciliationStatusMstEntityColl = new List<ReconciliationStatusMstInfo>(this.Select(cmd));
            return objReconciliationStatusMstEntityColl;
        }

    }
}
