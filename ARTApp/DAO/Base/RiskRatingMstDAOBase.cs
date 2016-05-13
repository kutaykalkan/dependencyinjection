

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

    public abstract class RiskRatingMstDAOBase : CustomAbstractDAO<RiskRatingMstInfo>
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
        /// A static representation of column ReconciliationFrequencyID
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONFREQUENCYID = "ReconciliationFrequencyID";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// A static representation of column RiskRating
        /// </summary>
        public static readonly string COLUMN_RISKRATING = "RiskRating";
        /// <summary>
        /// A static representation of column RiskRatingID
        /// </summary>
        public static readonly string COLUMN_RISKRATINGID = "RiskRatingID";
        /// <summary>
        /// A static representation of column RiskRatingLabelID
        /// </summary>
        public static readonly string COLUMN_RISKRATINGLABELID = "RiskRatingLabelID";
        /// <summary>
        /// Provides access to the name of the primary key column (RiskRatingID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "RiskRatingID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "RiskRatingMst";

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
        public RiskRatingMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "RiskRatingMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a RiskRatingMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>RiskRatingMstInfo</returns>
        protected override RiskRatingMstInfo MapObject(System.Data.IDataReader r)
        {

            RiskRatingMstInfo entity = new RiskRatingMstInfo();

            entity.RiskRatingID = r.GetInt16Value("RiskRatingID");
            entity.RiskRating = r.GetStringValue("RiskRating");
            entity.RiskRatingLabelID = r.GetInt32Value("RiskRatingLabelID");
            entity.Description = r.GetStringValue("Description");
            entity.ReconciliationFrequencyID = r.GetInt16Value("ReconciliationFrequencyID");
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
        /// in RiskRatingMstInfo object
        /// </summary>
        /// <param name="o">A RiskRatingMstInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(RiskRatingMstInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_RiskRatingMst");
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

            System.Data.IDbDataParameter parReconciliationFrequencyID = cmd.CreateParameter();
            parReconciliationFrequencyID.ParameterName = "@ReconciliationFrequencyID";
            if (!entity.IsReconciliationFrequencyIDNull)
                parReconciliationFrequencyID.Value = entity.ReconciliationFrequencyID;
            else
                parReconciliationFrequencyID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationFrequencyID);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parRiskRating = cmd.CreateParameter();
            parRiskRating.ParameterName = "@RiskRating";
            if (!entity.IsRiskRatingNull)
                parRiskRating.Value = entity.RiskRating;
            else
                parRiskRating.Value = System.DBNull.Value;
            cmdParams.Add(parRiskRating);

            System.Data.IDbDataParameter parRiskRatingLabelID = cmd.CreateParameter();
            parRiskRatingLabelID.ParameterName = "@RiskRatingLabelID";
            if (!entity.IsRiskRatingLabelIDNull)
                parRiskRatingLabelID.Value = entity.RiskRatingLabelID;
            else
                parRiskRatingLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parRiskRatingLabelID);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in RiskRatingMstInfo object
        /// </summary>
        /// <param name="o">A RiskRatingMstInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(RiskRatingMstInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_RiskRatingMst");
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

            System.Data.IDbDataParameter parReconciliationFrequencyID = cmd.CreateParameter();
            parReconciliationFrequencyID.ParameterName = "@ReconciliationFrequencyID";
            if (!entity.IsReconciliationFrequencyIDNull)
                parReconciliationFrequencyID.Value = entity.ReconciliationFrequencyID;
            else
                parReconciliationFrequencyID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationFrequencyID);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parRiskRating = cmd.CreateParameter();
            parRiskRating.ParameterName = "@RiskRating";
            if (!entity.IsRiskRatingNull)
                parRiskRating.Value = entity.RiskRating;
            else
                parRiskRating.Value = System.DBNull.Value;
            cmdParams.Add(parRiskRating);

            System.Data.IDbDataParameter parRiskRatingLabelID = cmd.CreateParameter();
            parRiskRatingLabelID.ParameterName = "@RiskRatingLabelID";
            if (!entity.IsRiskRatingLabelIDNull)
                parRiskRatingLabelID.Value = entity.RiskRatingLabelID;
            else
                parRiskRatingLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parRiskRatingLabelID);

            System.Data.IDbDataParameter pkparRiskRatingID = cmd.CreateParameter();
            pkparRiskRatingID.ParameterName = "@RiskRatingID";
            pkparRiskRatingID.Value = entity.RiskRatingID;
            cmdParams.Add(pkparRiskRatingID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_RiskRatingMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RiskRatingID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_RiskRatingMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RiskRatingID";
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
        public IList<RiskRatingMstInfo> SelectAllByReconciliationFrequencyID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_RiskRatingMstByReconciliationFrequencyID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationFrequencyID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(RiskRatingMstInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(RiskRatingMstDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(RiskRatingMstInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(RiskRatingMstDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(RiskRatingMstInfo entity, object id)
        {
            entity.RiskRatingID = Convert.ToInt16(id);
        }










        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RiskRatingMstInfo> SelectRiskRatingMstDetailsAssociatedToGeographyObjectHdrByAccountHdr(GeographyObjectHdrInfo entity)
        {
            return this.SelectRiskRatingMstDetailsAssociatedToGeographyObjectHdrByAccountHdr(entity.GeographyObjectID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RiskRatingMstInfo> SelectRiskRatingMstDetailsAssociatedToGeographyObjectHdrByAccountHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [RiskRatingMst] INNER JOIN [AccountHdr] ON [RiskRatingMst].[RiskRatingID] = [AccountHdr].[RiskRatingID] INNER JOIN [GeographyObjectHdr] ON [AccountHdr].[GeographyObjectID] = [GeographyObjectHdr].[GeographyObjectID]  WHERE  [GeographyObjectHdr].[GeographyObjectID] = @GeographyObjectID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GeographyObjectID";
            par.Value = id;

            cmdParams.Add(par);
            List<RiskRatingMstInfo> objRiskRatingMstEntityColl = new List<RiskRatingMstInfo>(this.Select(cmd));
            return objRiskRatingMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RiskRatingMstInfo> SelectRiskRatingMstDetailsAssociatedToAccountTypeMstByAccountHdr(AccountTypeMstInfo entity)
        {
            return this.SelectRiskRatingMstDetailsAssociatedToAccountTypeMstByAccountHdr(entity.AccountTypeID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RiskRatingMstInfo> SelectRiskRatingMstDetailsAssociatedToAccountTypeMstByAccountHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [RiskRatingMst] INNER JOIN [AccountHdr] ON [RiskRatingMst].[RiskRatingID] = [AccountHdr].[RiskRatingID] INNER JOIN [AccountTypeMst] ON [AccountHdr].[AccountTypeID] = [AccountTypeMst].[AccountTypeID]  WHERE  [AccountTypeMst].[AccountTypeID] = @AccountTypeID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AccountTypeID";
            par.Value = id;

            cmdParams.Add(par);
            List<RiskRatingMstInfo> objRiskRatingMstEntityColl = new List<RiskRatingMstInfo>(this.Select(cmd));
            return objRiskRatingMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RiskRatingMstInfo> SelectRiskRatingMstDetailsAssociatedToReconciliationTemplateMstByAccountHdr(ReconciliationTemplateMstInfo entity)
        {
            return this.SelectRiskRatingMstDetailsAssociatedToReconciliationTemplateMstByAccountHdr(entity.ReconciliationTemplateID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RiskRatingMstInfo> SelectRiskRatingMstDetailsAssociatedToReconciliationTemplateMstByAccountHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [RiskRatingMst] INNER JOIN [AccountHdr] ON [RiskRatingMst].[RiskRatingID] = [AccountHdr].[RiskRatingID] INNER JOIN [ReconciliationTemplateMst] ON [AccountHdr].[ReconciliationTemplateID] = [ReconciliationTemplateMst].[ReconciliationTemplateID]  WHERE  [ReconciliationTemplateMst].[ReconciliationTemplateID] = @ReconciliationTemplateID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationTemplateID";
            par.Value = id;

            cmdParams.Add(par);
            List<RiskRatingMstInfo> objRiskRatingMstEntityColl = new List<RiskRatingMstInfo>(this.Select(cmd));
            return objRiskRatingMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RiskRatingMstInfo> SelectRiskRatingMstDetailsAssociatedToFSCaptionHdrByAccountHdr(FSCaptionHdrInfo entity)
        {
            return this.SelectRiskRatingMstDetailsAssociatedToFSCaptionHdrByAccountHdr(entity.FSCaptionID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RiskRatingMstInfo> SelectRiskRatingMstDetailsAssociatedToFSCaptionHdrByAccountHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [RiskRatingMst] INNER JOIN [AccountHdr] ON [RiskRatingMst].[RiskRatingID] = [AccountHdr].[RiskRatingID] INNER JOIN [FSCaptionHdr] ON [AccountHdr].[FSCaptionID] = [FSCaptionHdr].[FSCaptionID]  WHERE  [FSCaptionHdr].[FSCaptionID] = @FSCaptionID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@FSCaptionID";
            par.Value = id;

            cmdParams.Add(par);
            List<RiskRatingMstInfo> objRiskRatingMstEntityColl = new List<RiskRatingMstInfo>(this.Select(cmd));
            return objRiskRatingMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RiskRatingMstInfo> SelectRiskRatingMstDetailsAssociatedToSubledgerSourceByAccountHdr(SubledgerSourceInfo entity)
        {
            return this.SelectRiskRatingMstDetailsAssociatedToSubledgerSourceByAccountHdr(entity.SubledgerSourceID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RiskRatingMstInfo> SelectRiskRatingMstDetailsAssociatedToSubledgerSourceByAccountHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [RiskRatingMst] INNER JOIN [AccountHdr] ON [RiskRatingMst].[RiskRatingID] = [AccountHdr].[RiskRatingID] INNER JOIN [SubledgerSource] ON [AccountHdr].[SubLedgerSourceID] = [SubledgerSource].[SubledgerSourceID]  WHERE  [SubledgerSource].[SubledgerSourceID] = @SubledgerSourceID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@SubledgerSourceID";
            par.Value = id;

            cmdParams.Add(par);
            List<RiskRatingMstInfo> objRiskRatingMstEntityColl = new List<RiskRatingMstInfo>(this.Select(cmd));
            return objRiskRatingMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RiskRatingMstInfo> SelectRiskRatingMstDetailsAssociatedToNetAccountHdrByAccountHdr(NetAccountHdrInfo entity)
        {
            return this.SelectRiskRatingMstDetailsAssociatedToNetAccountHdrByAccountHdr(entity.NetAccountID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RiskRatingMstInfo> SelectRiskRatingMstDetailsAssociatedToNetAccountHdrByAccountHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [RiskRatingMst] INNER JOIN [AccountHdr] ON [RiskRatingMst].[RiskRatingID] = [AccountHdr].[RiskRatingID] INNER JOIN [NetAccountHdr] ON [AccountHdr].[NetAccountID] = [NetAccountHdr].[NetAccountID]  WHERE  [NetAccountHdr].[NetAccountID] = @NetAccountID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@NetAccountID";
            par.Value = id;

            cmdParams.Add(par);
            List<RiskRatingMstInfo> objRiskRatingMstEntityColl = new List<RiskRatingMstInfo>(this.Select(cmd));
            return objRiskRatingMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RiskRatingMstInfo> SelectRiskRatingMstDetailsAssociatedToCompanyHdrByRiskRatingReconciliationFrequency(CompanyHdrInfo entity)
        {
            return this.SelectRiskRatingMstDetailsAssociatedToCompanyHdrByRiskRatingReconciliationFrequency(entity.CompanyID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RiskRatingMstInfo> SelectRiskRatingMstDetailsAssociatedToCompanyHdrByRiskRatingReconciliationFrequency(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [RiskRatingMst] INNER JOIN [RiskRatingReconciliationFrequency] ON [RiskRatingMst].[RiskRatingID] = [RiskRatingReconciliationFrequency].[RiskRatingID] INNER JOIN [CompanyHdr] ON [RiskRatingReconciliationFrequency].[CompanyID] = [CompanyHdr].[CompanyID]  WHERE  [CompanyHdr].[CompanyID] = @CompanyID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyID";
            par.Value = id;

            cmdParams.Add(par);
            List<RiskRatingMstInfo> objRiskRatingMstEntityColl = new List<RiskRatingMstInfo>(this.Select(cmd));
            return objRiskRatingMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RiskRatingMstInfo> SelectRiskRatingMstDetailsAssociatedToReconciliationFrequencyMstByRiskRatingReconciliationFrequency(ReconciliationFrequencyMstInfo entity)
        {
            return this.SelectRiskRatingMstDetailsAssociatedToReconciliationFrequencyMstByRiskRatingReconciliationFrequency(entity.ReconciliationFrequencyID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RiskRatingMstInfo> SelectRiskRatingMstDetailsAssociatedToReconciliationFrequencyMstByRiskRatingReconciliationFrequency(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [RiskRatingMst] INNER JOIN [RiskRatingReconciliationFrequency] ON [RiskRatingMst].[RiskRatingID] = [RiskRatingReconciliationFrequency].[RiskRatingID] INNER JOIN [ReconciliationFrequencyMst] ON [RiskRatingReconciliationFrequency].[ReconciliationFrequencyID] = [ReconciliationFrequencyMst].[ReconciliationFrequencyID]  WHERE  [ReconciliationFrequencyMst].[ReconciliationFrequencyID] = @ReconciliationFrequencyID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationFrequencyID";
            par.Value = id;

            cmdParams.Add(par);
            List<RiskRatingMstInfo> objRiskRatingMstEntityColl = new List<RiskRatingMstInfo>(this.Select(cmd));
            return objRiskRatingMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RiskRatingMstInfo> SelectRiskRatingMstDetailsAssociatedToCompanyHdrByRiskRatingReconciliationPeriod(CompanyHdrInfo entity)
        {
            return this.SelectRiskRatingMstDetailsAssociatedToCompanyHdrByRiskRatingReconciliationPeriod(entity.CompanyID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RiskRatingMstInfo> SelectRiskRatingMstDetailsAssociatedToCompanyHdrByRiskRatingReconciliationPeriod(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [RiskRatingMst] INNER JOIN [RiskRatingReconciliationPeriod] ON [RiskRatingMst].[RiskRatingID] = [RiskRatingReconciliationPeriod].[RiskRatingID] INNER JOIN [CompanyHdr] ON [RiskRatingReconciliationPeriod].[CompanyID] = [CompanyHdr].[CompanyID]  WHERE  [CompanyHdr].[CompanyID] = @CompanyID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyID";
            par.Value = id;

            cmdParams.Add(par);
            List<RiskRatingMstInfo> objRiskRatingMstEntityColl = new List<RiskRatingMstInfo>(this.Select(cmd));
            return objRiskRatingMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RiskRatingMstInfo> SelectRiskRatingMstDetailsAssociatedToReconciliationPeriodByRiskRatingReconciliationPeriod(ReconciliationPeriodInfo entity)
        {
            return this.SelectRiskRatingMstDetailsAssociatedToReconciliationPeriodByRiskRatingReconciliationPeriod(entity.ReconciliationPeriodID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<RiskRatingMstInfo> SelectRiskRatingMstDetailsAssociatedToReconciliationPeriodByRiskRatingReconciliationPeriod(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [RiskRatingMst] INNER JOIN [RiskRatingReconciliationPeriod] ON [RiskRatingMst].[RiskRatingID] = [RiskRatingReconciliationPeriod].[RiskRatingID] INNER JOIN [ReconciliationPeriod] ON [RiskRatingReconciliationPeriod].[ReconciliationPeriodID] = [ReconciliationPeriod].[ReconciliationPeriodID]  WHERE  [ReconciliationPeriod].[ReconciliationPeriodID] = @ReconciliationPeriodID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationPeriodID";
            par.Value = id;

            cmdParams.Add(par);
            List<RiskRatingMstInfo> objRiskRatingMstEntityColl = new List<RiskRatingMstInfo>(this.Select(cmd));
            return objRiskRatingMstEntityColl;
        }

    }
}
