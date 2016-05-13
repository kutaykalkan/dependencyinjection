

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

    public abstract class FSCaptionHdrDAOBase : CustomAbstractDAO<FSCaptionHdrInfo>
    {

        /// <summary>
        /// A static representation of column AddedBy
        /// </summary>
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
        /// <summary>
        /// A static representation of column CompanyID
        /// </summary>
        public static readonly string COLUMN_COMPANYID = "CompanyID";
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
        /// A static representation of column FSCaption
        /// </summary>
        public static readonly string COLUMN_FSCAPTION = "FSCaption";
        /// <summary>
        /// A static representation of column FSCaptionID
        /// </summary>
        public static readonly string COLUMN_FSCAPTIONID = "FSCaptionID";
        /// <summary>
        /// A static representation of column FSCaptionLabelID
        /// </summary>
        public static readonly string COLUMN_FSCAPTIONLABELID = "FSCaptionLabelID";
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
        /// Provides access to the name of the primary key column (FSCaptionID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "FSCaptionID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "FSCaptionHdr";

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
        public FSCaptionHdrDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "FSCaptionHdr", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a FSCaptionHdrInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>FSCaptionHdrInfo</returns>
        protected override FSCaptionHdrInfo MapObject(System.Data.IDataReader r)
        {

            FSCaptionHdrInfo entity = new FSCaptionHdrInfo();


            try
            {
                int ordinal = r.GetOrdinal("FSCaptionID");
                if (!r.IsDBNull(ordinal)) entity.FSCaptionID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("FSCaption");
                if (!r.IsDBNull(ordinal)) entity.FSCaption = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("FSCaptionLabelID");
                if (!r.IsDBNull(ordinal)) entity.FSCaptionLabelID = ((System.Int32)(r.GetValue(ordinal)));
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
                int ordinal = r.GetOrdinal("CompanyID");
                if (!r.IsDBNull(ordinal)) entity.CompanyID = ((System.Int32)(r.GetValue(ordinal)));
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
        /// in FSCaptionHdrInfo object
        /// </summary>
        /// <param name="o">A FSCaptionHdrInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(FSCaptionHdrInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_FSCaptionHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (!entity.IsCompanyIDNull)
                parCompanyID.Value = entity.CompanyID;
            else
                parCompanyID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyID);

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

            System.Data.IDbDataParameter parFSCaption = cmd.CreateParameter();
            parFSCaption.ParameterName = "@FSCaption";
            if (!entity.IsFSCaptionNull)
                parFSCaption.Value = entity.FSCaption;
            else
                parFSCaption.Value = System.DBNull.Value;
            cmdParams.Add(parFSCaption);

            System.Data.IDbDataParameter parFSCaptionLabelID = cmd.CreateParameter();
            parFSCaptionLabelID.ParameterName = "@FSCaptionLabelID";
            if (!entity.IsFSCaptionLabelIDNull)
                parFSCaptionLabelID.Value = entity.FSCaptionLabelID;
            else
                parFSCaptionLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parFSCaptionLabelID);

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
        /// in FSCaptionHdrInfo object
        /// </summary>
        /// <param name="o">A FSCaptionHdrInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(FSCaptionHdrInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_FSCaptionHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (!entity.IsCompanyIDNull)
                parCompanyID.Value = entity.CompanyID;
            else
                parCompanyID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyID);

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

            System.Data.IDbDataParameter parFSCaption = cmd.CreateParameter();
            parFSCaption.ParameterName = "@FSCaption";
            if (!entity.IsFSCaptionNull)
                parFSCaption.Value = entity.FSCaption;
            else
                parFSCaption.Value = System.DBNull.Value;
            cmdParams.Add(parFSCaption);

            System.Data.IDbDataParameter parFSCaptionLabelID = cmd.CreateParameter();
            parFSCaptionLabelID.ParameterName = "@FSCaptionLabelID";
            if (!entity.IsFSCaptionLabelIDNull)
                parFSCaptionLabelID.Value = entity.FSCaptionLabelID;
            else
                parFSCaptionLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parFSCaptionLabelID);

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

            System.Data.IDbDataParameter pkparFSCaptionID = cmd.CreateParameter();
            pkparFSCaptionID.ParameterName = "@FSCaptionID";
            pkparFSCaptionID.Value = entity.FSCaptionID;
            cmdParams.Add(pkparFSCaptionID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_FSCaptionHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@FSCaptionID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_FSCaptionHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@FSCaptionID";
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
        public IList<FSCaptionHdrInfo> SelectAllByCompanyID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_FSCaptionHdrByCompanyID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(FSCaptionHdrInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(FSCaptionHdrDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(FSCaptionHdrInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(FSCaptionHdrDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(FSCaptionHdrInfo entity, object id)
        {
            entity.FSCaptionID = Convert.ToInt32(id);
        }









        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<FSCaptionHdrInfo> SelectFSCaptionHdrDetailsAssociatedToGeographyObjectHdrByAccountHdr(GeographyObjectHdrInfo entity)
        {
            return this.SelectFSCaptionHdrDetailsAssociatedToGeographyObjectHdrByAccountHdr(entity.GeographyObjectID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<FSCaptionHdrInfo> SelectFSCaptionHdrDetailsAssociatedToGeographyObjectHdrByAccountHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [FSCaptionHdr] INNER JOIN [AccountHdr] ON [FSCaptionHdr].[FSCaptionID] = [AccountHdr].[FSCaptionID] INNER JOIN [GeographyObjectHdr] ON [AccountHdr].[GeographyObjectID] = [GeographyObjectHdr].[GeographyObjectID]  WHERE  [GeographyObjectHdr].[GeographyObjectID] = @GeographyObjectID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GeographyObjectID";
            par.Value = id;

            cmdParams.Add(par);
            List<FSCaptionHdrInfo> objFSCaptionHdrEntityColl = new List<FSCaptionHdrInfo>(this.Select(cmd));
            return objFSCaptionHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<FSCaptionHdrInfo> SelectFSCaptionHdrDetailsAssociatedToAccountTypeMstByAccountHdr(AccountTypeMstInfo entity)
        {
            return this.SelectFSCaptionHdrDetailsAssociatedToAccountTypeMstByAccountHdr(entity.AccountTypeID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<FSCaptionHdrInfo> SelectFSCaptionHdrDetailsAssociatedToAccountTypeMstByAccountHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [FSCaptionHdr] INNER JOIN [AccountHdr] ON [FSCaptionHdr].[FSCaptionID] = [AccountHdr].[FSCaptionID] INNER JOIN [AccountTypeMst] ON [AccountHdr].[AccountTypeID] = [AccountTypeMst].[AccountTypeID]  WHERE  [AccountTypeMst].[AccountTypeID] = @AccountTypeID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AccountTypeID";
            par.Value = id;

            cmdParams.Add(par);
            List<FSCaptionHdrInfo> objFSCaptionHdrEntityColl = new List<FSCaptionHdrInfo>(this.Select(cmd));
            return objFSCaptionHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<FSCaptionHdrInfo> SelectFSCaptionHdrDetailsAssociatedToReconciliationTemplateMstByAccountHdr(ReconciliationTemplateMstInfo entity)
        {
            return this.SelectFSCaptionHdrDetailsAssociatedToReconciliationTemplateMstByAccountHdr(entity.ReconciliationTemplateID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<FSCaptionHdrInfo> SelectFSCaptionHdrDetailsAssociatedToReconciliationTemplateMstByAccountHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [FSCaptionHdr] INNER JOIN [AccountHdr] ON [FSCaptionHdr].[FSCaptionID] = [AccountHdr].[FSCaptionID] INNER JOIN [ReconciliationTemplateMst] ON [AccountHdr].[ReconciliationTemplateID] = [ReconciliationTemplateMst].[ReconciliationTemplateID]  WHERE  [ReconciliationTemplateMst].[ReconciliationTemplateID] = @ReconciliationTemplateID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationTemplateID";
            par.Value = id;

            cmdParams.Add(par);
            List<FSCaptionHdrInfo> objFSCaptionHdrEntityColl = new List<FSCaptionHdrInfo>(this.Select(cmd));
            return objFSCaptionHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<FSCaptionHdrInfo> SelectFSCaptionHdrDetailsAssociatedToRiskRatingMstByAccountHdr(RiskRatingMstInfo entity)
        {
            return this.SelectFSCaptionHdrDetailsAssociatedToRiskRatingMstByAccountHdr(entity.RiskRatingID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<FSCaptionHdrInfo> SelectFSCaptionHdrDetailsAssociatedToRiskRatingMstByAccountHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [FSCaptionHdr] INNER JOIN [AccountHdr] ON [FSCaptionHdr].[FSCaptionID] = [AccountHdr].[FSCaptionID] INNER JOIN [RiskRatingMst] ON [AccountHdr].[RiskRatingID] = [RiskRatingMst].[RiskRatingID]  WHERE  [RiskRatingMst].[RiskRatingID] = @RiskRatingID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RiskRatingID";
            par.Value = id;

            cmdParams.Add(par);
            List<FSCaptionHdrInfo> objFSCaptionHdrEntityColl = new List<FSCaptionHdrInfo>(this.Select(cmd));
            return objFSCaptionHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<FSCaptionHdrInfo> SelectFSCaptionHdrDetailsAssociatedToSubledgerSourceByAccountHdr(SubledgerSourceInfo entity)
        {
            return this.SelectFSCaptionHdrDetailsAssociatedToSubledgerSourceByAccountHdr(entity.SubledgerSourceID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<FSCaptionHdrInfo> SelectFSCaptionHdrDetailsAssociatedToSubledgerSourceByAccountHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [FSCaptionHdr] INNER JOIN [AccountHdr] ON [FSCaptionHdr].[FSCaptionID] = [AccountHdr].[FSCaptionID] INNER JOIN [SubledgerSource] ON [AccountHdr].[SubLedgerSourceID] = [SubledgerSource].[SubledgerSourceID]  WHERE  [SubledgerSource].[SubledgerSourceID] = @SubledgerSourceID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@SubledgerSourceID";
            par.Value = id;

            cmdParams.Add(par);
            List<FSCaptionHdrInfo> objFSCaptionHdrEntityColl = new List<FSCaptionHdrInfo>(this.Select(cmd));
            return objFSCaptionHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<FSCaptionHdrInfo> SelectFSCaptionHdrDetailsAssociatedToNetAccountHdrByAccountHdr(NetAccountHdrInfo entity)
        {
            return this.SelectFSCaptionHdrDetailsAssociatedToNetAccountHdrByAccountHdr(entity.NetAccountID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<FSCaptionHdrInfo> SelectFSCaptionHdrDetailsAssociatedToNetAccountHdrByAccountHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [FSCaptionHdr] INNER JOIN [AccountHdr] ON [FSCaptionHdr].[FSCaptionID] = [AccountHdr].[FSCaptionID] INNER JOIN [NetAccountHdr] ON [AccountHdr].[NetAccountID] = [NetAccountHdr].[NetAccountID]  WHERE  [NetAccountHdr].[NetAccountID] = @NetAccountID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@NetAccountID";
            par.Value = id;

            cmdParams.Add(par);
            List<FSCaptionHdrInfo> objFSCaptionHdrEntityColl = new List<FSCaptionHdrInfo>(this.Select(cmd));
            return objFSCaptionHdrEntityColl;
        }

    }
}
