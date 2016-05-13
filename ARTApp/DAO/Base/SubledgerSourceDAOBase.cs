

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

    public abstract class SubledgerSourceDAOBase : CustomAbstractDAO<SubledgerSourceInfo>
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
        /// A static representation of column SubledgerCode
        /// </summary>
        public static readonly string COLUMN_SUBLEDGERCODE = "SubledgerCode";
        /// <summary>
        /// A static representation of column SubledgerSource
        /// </summary>
        public static readonly string COLUMN_SUBLEDGERSOURCE = "SubledgerSource";
        /// <summary>
        /// A static representation of column SubledgerSourceID
        /// </summary>
        public static readonly string COLUMN_SUBLEDGERSOURCEID = "SubledgerSourceID";
        /// <summary>
        /// A static representation of column SubledgerSourceLabelID
        /// </summary>
        public static readonly string COLUMN_SUBLEDGERSOURCELABELID = "SubledgerSourceLabelID";
        /// <summary>
        /// Provides access to the name of the primary key column (SubledgerSourceID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "SubledgerSourceID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "SubledgerSource";

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
        public SubledgerSourceDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "SubledgerSource", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a SubledgerSourceInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>SubledgerSourceInfo</returns>
        protected override SubledgerSourceInfo MapObject(System.Data.IDataReader r)
        {

            SubledgerSourceInfo entity = new SubledgerSourceInfo();


            try
            {
                int ordinal = r.GetOrdinal("SubledgerSourceID");
                if (!r.IsDBNull(ordinal)) entity.SubledgerSourceID = ((System.Int32)(r.GetValue(ordinal)));
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
                int ordinal = r.GetOrdinal("SubledgerCode");
                if (!r.IsDBNull(ordinal)) entity.SubledgerCode = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("SubledgerSource");
                if (!r.IsDBNull(ordinal)) entity.SubledgerSource = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("SubledgerSourceLabelID");
                if (!r.IsDBNull(ordinal)) entity.SubledgerSourceLabelID = ((System.Int32)(r.GetValue(ordinal)));
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
        /// in SubledgerSourceInfo object
        /// </summary>
        /// <param name="o">A SubledgerSourceInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(SubledgerSourceInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_SubledgerSource");
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

            System.Data.IDbDataParameter parSubledgerCode = cmd.CreateParameter();
            parSubledgerCode.ParameterName = "@SubledgerCode";
            if (!entity.IsSubledgerCodeNull)
                parSubledgerCode.Value = entity.SubledgerCode;
            else
                parSubledgerCode.Value = System.DBNull.Value;
            cmdParams.Add(parSubledgerCode);

            System.Data.IDbDataParameter parSubledgerSource = cmd.CreateParameter();
            parSubledgerSource.ParameterName = "@SubledgerSource";
            if (!entity.IsSubledgerSourceNull)
                parSubledgerSource.Value = entity.SubledgerSource;
            else
                parSubledgerSource.Value = System.DBNull.Value;
            cmdParams.Add(parSubledgerSource);

            System.Data.IDbDataParameter parSubledgerSourceLabelID = cmd.CreateParameter();
            parSubledgerSourceLabelID.ParameterName = "@SubledgerSourceLabelID";
            if (!entity.IsSubledgerSourceLabelIDNull)
                parSubledgerSourceLabelID.Value = entity.SubledgerSourceLabelID;
            else
                parSubledgerSourceLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parSubledgerSourceLabelID);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in SubledgerSourceInfo object
        /// </summary>
        /// <param name="o">A SubledgerSourceInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(SubledgerSourceInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_SubledgerSource");
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

            System.Data.IDbDataParameter parSubledgerCode = cmd.CreateParameter();
            parSubledgerCode.ParameterName = "@SubledgerCode";
            if (!entity.IsSubledgerCodeNull)
                parSubledgerCode.Value = entity.SubledgerCode;
            else
                parSubledgerCode.Value = System.DBNull.Value;
            cmdParams.Add(parSubledgerCode);

            System.Data.IDbDataParameter parSubledgerSource = cmd.CreateParameter();
            parSubledgerSource.ParameterName = "@SubledgerSource";
            if (!entity.IsSubledgerSourceNull)
                parSubledgerSource.Value = entity.SubledgerSource;
            else
                parSubledgerSource.Value = System.DBNull.Value;
            cmdParams.Add(parSubledgerSource);

            System.Data.IDbDataParameter parSubledgerSourceLabelID = cmd.CreateParameter();
            parSubledgerSourceLabelID.ParameterName = "@SubledgerSourceLabelID";
            if (!entity.IsSubledgerSourceLabelIDNull)
                parSubledgerSourceLabelID.Value = entity.SubledgerSourceLabelID;
            else
                parSubledgerSourceLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parSubledgerSourceLabelID);

            System.Data.IDbDataParameter pkparSubledgerSourceID = cmd.CreateParameter();
            pkparSubledgerSourceID.ParameterName = "@SubledgerSourceID";
            pkparSubledgerSourceID.Value = entity.SubledgerSourceID;
            cmdParams.Add(pkparSubledgerSourceID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_SubledgerSource");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@SubledgerSourceID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_SubledgerSource");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@SubledgerSourceID";
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
        public IList<SubledgerSourceInfo> SelectAllByCompanyID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_SubledgerSourceByCompanyID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(SubledgerSourceInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(SubledgerSourceDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(SubledgerSourceInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(SubledgerSourceDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(SubledgerSourceInfo entity, object id)
        {
            entity.SubledgerSourceID = Convert.ToInt32(id);
        }








        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<SubledgerSourceInfo> SelectSubledgerSourceDetailsAssociatedToGeographyObjectHdrByAccountHdr(GeographyObjectHdrInfo entity)
        {
            return this.SelectSubledgerSourceDetailsAssociatedToGeographyObjectHdrByAccountHdr(entity.GeographyObjectID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<SubledgerSourceInfo> SelectSubledgerSourceDetailsAssociatedToGeographyObjectHdrByAccountHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [SubledgerSource] INNER JOIN [AccountHdr] ON [SubledgerSource].[SubledgerSourceID] = [AccountHdr].[SubLedgerSourceID] INNER JOIN [GeographyObjectHdr] ON [AccountHdr].[GeographyObjectID] = [GeographyObjectHdr].[GeographyObjectID]  WHERE  [GeographyObjectHdr].[GeographyObjectID] = @GeographyObjectID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GeographyObjectID";
            par.Value = id;

            cmdParams.Add(par);
            List<SubledgerSourceInfo> objSubledgerSourceEntityColl = new List<SubledgerSourceInfo>(this.Select(cmd));
            return objSubledgerSourceEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<SubledgerSourceInfo> SelectSubledgerSourceDetailsAssociatedToAccountTypeMstByAccountHdr(AccountTypeMstInfo entity)
        {
            return this.SelectSubledgerSourceDetailsAssociatedToAccountTypeMstByAccountHdr(entity.AccountTypeID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<SubledgerSourceInfo> SelectSubledgerSourceDetailsAssociatedToAccountTypeMstByAccountHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [SubledgerSource] INNER JOIN [AccountHdr] ON [SubledgerSource].[SubledgerSourceID] = [AccountHdr].[SubLedgerSourceID] INNER JOIN [AccountTypeMst] ON [AccountHdr].[AccountTypeID] = [AccountTypeMst].[AccountTypeID]  WHERE  [AccountTypeMst].[AccountTypeID] = @AccountTypeID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AccountTypeID";
            par.Value = id;

            cmdParams.Add(par);
            List<SubledgerSourceInfo> objSubledgerSourceEntityColl = new List<SubledgerSourceInfo>(this.Select(cmd));
            return objSubledgerSourceEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<SubledgerSourceInfo> SelectSubledgerSourceDetailsAssociatedToReconciliationTemplateMstByAccountHdr(ReconciliationTemplateMstInfo entity)
        {
            return this.SelectSubledgerSourceDetailsAssociatedToReconciliationTemplateMstByAccountHdr(entity.ReconciliationTemplateID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<SubledgerSourceInfo> SelectSubledgerSourceDetailsAssociatedToReconciliationTemplateMstByAccountHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [SubledgerSource] INNER JOIN [AccountHdr] ON [SubledgerSource].[SubledgerSourceID] = [AccountHdr].[SubLedgerSourceID] INNER JOIN [ReconciliationTemplateMst] ON [AccountHdr].[ReconciliationTemplateID] = [ReconciliationTemplateMst].[ReconciliationTemplateID]  WHERE  [ReconciliationTemplateMst].[ReconciliationTemplateID] = @ReconciliationTemplateID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationTemplateID";
            par.Value = id;

            cmdParams.Add(par);
            List<SubledgerSourceInfo> objSubledgerSourceEntityColl = new List<SubledgerSourceInfo>(this.Select(cmd));
            return objSubledgerSourceEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<SubledgerSourceInfo> SelectSubledgerSourceDetailsAssociatedToFSCaptionHdrByAccountHdr(FSCaptionHdrInfo entity)
        {
            return this.SelectSubledgerSourceDetailsAssociatedToFSCaptionHdrByAccountHdr(entity.FSCaptionID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<SubledgerSourceInfo> SelectSubledgerSourceDetailsAssociatedToFSCaptionHdrByAccountHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [SubledgerSource] INNER JOIN [AccountHdr] ON [SubledgerSource].[SubledgerSourceID] = [AccountHdr].[SubLedgerSourceID] INNER JOIN [FSCaptionHdr] ON [AccountHdr].[FSCaptionID] = [FSCaptionHdr].[FSCaptionID]  WHERE  [FSCaptionHdr].[FSCaptionID] = @FSCaptionID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@FSCaptionID";
            par.Value = id;

            cmdParams.Add(par);
            List<SubledgerSourceInfo> objSubledgerSourceEntityColl = new List<SubledgerSourceInfo>(this.Select(cmd));
            return objSubledgerSourceEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<SubledgerSourceInfo> SelectSubledgerSourceDetailsAssociatedToRiskRatingMstByAccountHdr(RiskRatingMstInfo entity)
        {
            return this.SelectSubledgerSourceDetailsAssociatedToRiskRatingMstByAccountHdr(entity.RiskRatingID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<SubledgerSourceInfo> SelectSubledgerSourceDetailsAssociatedToRiskRatingMstByAccountHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [SubledgerSource] INNER JOIN [AccountHdr] ON [SubledgerSource].[SubledgerSourceID] = [AccountHdr].[SubLedgerSourceID] INNER JOIN [RiskRatingMst] ON [AccountHdr].[RiskRatingID] = [RiskRatingMst].[RiskRatingID]  WHERE  [RiskRatingMst].[RiskRatingID] = @RiskRatingID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RiskRatingID";
            par.Value = id;

            cmdParams.Add(par);
            List<SubledgerSourceInfo> objSubledgerSourceEntityColl = new List<SubledgerSourceInfo>(this.Select(cmd));
            return objSubledgerSourceEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<SubledgerSourceInfo> SelectSubledgerSourceDetailsAssociatedToNetAccountHdrByAccountHdr(NetAccountHdrInfo entity)
        {
            return this.SelectSubledgerSourceDetailsAssociatedToNetAccountHdrByAccountHdr(entity.NetAccountID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<SubledgerSourceInfo> SelectSubledgerSourceDetailsAssociatedToNetAccountHdrByAccountHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [SubledgerSource] INNER JOIN [AccountHdr] ON [SubledgerSource].[SubledgerSourceID] = [AccountHdr].[SubLedgerSourceID] INNER JOIN [NetAccountHdr] ON [AccountHdr].[NetAccountID] = [NetAccountHdr].[NetAccountID]  WHERE  [NetAccountHdr].[NetAccountID] = @NetAccountID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@NetAccountID";
            par.Value = id;

            cmdParams.Add(par);
            List<SubledgerSourceInfo> objSubledgerSourceEntityColl = new List<SubledgerSourceInfo>(this.Select(cmd));
            return objSubledgerSourceEntityColl;
        }

    }
}
