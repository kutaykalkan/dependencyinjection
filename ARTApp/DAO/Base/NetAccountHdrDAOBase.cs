

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

    public abstract class NetAccountHdrDAOBase : CustomAbstractDAO<NetAccountHdrInfo>
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
        /// A static representation of column NetAccount
        /// </summary>
        public static readonly string COLUMN_NETACCOUNT = "NetAccount";
        /// <summary>
        /// A static representation of column NetAccountID
        /// </summary>
        public static readonly string COLUMN_NETACCOUNTID = "NetAccountID";
        /// <summary>
        /// A static representation of column NetAccountLabelID
        /// </summary>
        public static readonly string COLUMN_NETACCOUNTLABELID = "NetAccountLabelID";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// Provides access to the name of the primary key column (NetAccountID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "NetAccountID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "NetAccountHdr";

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
        public NetAccountHdrDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "NetAccountHdr", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a NetAccountHdrInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>NetAccountHdrInfo</returns>
        protected override NetAccountHdrInfo MapObject(System.Data.IDataReader r)
        {

            NetAccountHdrInfo entity = new NetAccountHdrInfo();


            try
            {
                int ordinal = r.GetOrdinal("NetAccountID");
                if (!r.IsDBNull(ordinal)) entity.NetAccountID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("NetAccount");
                if (!r.IsDBNull(ordinal)) entity.NetAccount = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("NetAccountLabelID");
                if (!r.IsDBNull(ordinal)) entity.NetAccountLabelID = ((System.Int32)(r.GetValue(ordinal)));
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
        /// in NetAccountHdrInfo object
        /// </summary>
        /// <param name="o">A NetAccountHdrInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(NetAccountHdrInfo entity)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_NetAccountHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parNetAccountID = cmd.CreateParameter();
            parNetAccountID.ParameterName = "@NetAccountID";
            if (!entity.IsNetAccountIDNull)
                parNetAccountID.Value = entity.NetAccountID;
            else
                parNetAccountID.Value = System.DBNull.Value;
            cmdParams.Add(parNetAccountID);

            System.Data.IDbDataParameter parDescription = cmd.CreateParameter();
            parDescription.ParameterName = "@Description";
            if (!entity.IsDescriptionNull)
                parDescription.Value = entity.Description;
            else
                parDescription.Value = System.DBNull.Value;
            cmdParams.Add(parDescription);

            System.Data.IDbDataParameter parComanyID = cmd.CreateParameter();
            parComanyID.ParameterName = "@CompanyID";
            if (entity.CompanyID != null)
                parComanyID.Value = entity.CompanyID;
            else
                parComanyID.Value = System.DBNull.Value;
            cmdParams.Add(parComanyID);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (!entity.IsIsActiveNull)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parNetAccount = cmd.CreateParameter();
            parNetAccount.ParameterName = "@NetAccount";
            if (!entity.IsNetAccountNull)
                parNetAccount.Value = entity.NetAccount;
            else
                parNetAccount.Value = System.DBNull.Value;
            cmdParams.Add(parNetAccount);

            System.Data.IDbDataParameter parNetAccountLabelID = cmd.CreateParameter();
            parNetAccountLabelID.ParameterName = "@NetAccountLabelID";
            if (!entity.IsNetAccountLabelIDNull)
                parNetAccountLabelID.Value = entity.NetAccountLabelID;
            else
                parNetAccountLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parNetAccountLabelID);


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

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (!entity.IsDateRevisedNull)
                parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            return cmd;
        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in NetAccountHdrInfo object
        /// </summary>
        /// <param name="o">A NetAccountHdrInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(NetAccountHdrInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("usp_UPD_NetAccountHdr");
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

            System.Data.IDbDataParameter parNetAccount = cmd.CreateParameter();
            parNetAccount.ParameterName = "@NetAccount";
            if (!entity.IsNetAccountNull)
                parNetAccount.Value = entity.NetAccount;
            else
                parNetAccount.Value = System.DBNull.Value;
            cmdParams.Add(parNetAccount);

            System.Data.IDbDataParameter parNetAccountLabelID = cmd.CreateParameter();
            parNetAccountLabelID.ParameterName = "@NetAccountLabelID";
            if (!entity.IsNetAccountLabelIDNull)
                parNetAccountLabelID.Value = entity.NetAccountLabelID;
            else
                parNetAccountLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parNetAccountLabelID);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter pkparNetAccountID = cmd.CreateParameter();
            pkparNetAccountID.ParameterName = "@NetAccountID";
            pkparNetAccountID.Value = entity.NetAccountID;
            cmdParams.Add(pkparNetAccountID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_NetAccountHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@NetAccountID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_NetAccountHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@NetAccountID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }







        protected override void CustomSave(NetAccountHdrInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(NetAccountHdrDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(NetAccountHdrInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(NetAccountHdrDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(NetAccountHdrInfo entity, object id)
        {
            entity.NetAccountID = Convert.ToInt32(id);
        }








        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<NetAccountHdrInfo> SelectNetAccountHdrDetailsAssociatedToGeographyObjectHdrByAccountHdr(GeographyObjectHdrInfo entity)
        {
            return this.SelectNetAccountHdrDetailsAssociatedToGeographyObjectHdrByAccountHdr(entity.GeographyObjectID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<NetAccountHdrInfo> SelectNetAccountHdrDetailsAssociatedToGeographyObjectHdrByAccountHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [NetAccountHdr] INNER JOIN [AccountHdr] ON [NetAccountHdr].[NetAccountID] = [AccountHdr].[NetAccountID] INNER JOIN [GeographyObjectHdr] ON [AccountHdr].[GeographyObjectID] = [GeographyObjectHdr].[GeographyObjectID]  WHERE  [GeographyObjectHdr].[GeographyObjectID] = @GeographyObjectID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GeographyObjectID";
            par.Value = id;

            cmdParams.Add(par);
            List<NetAccountHdrInfo> objNetAccountHdrEntityColl = new List<NetAccountHdrInfo>(this.Select(cmd));
            return objNetAccountHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<NetAccountHdrInfo> SelectNetAccountHdrDetailsAssociatedToAccountTypeMstByAccountHdr(AccountTypeMstInfo entity)
        {
            return this.SelectNetAccountHdrDetailsAssociatedToAccountTypeMstByAccountHdr(entity.AccountTypeID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<NetAccountHdrInfo> SelectNetAccountHdrDetailsAssociatedToAccountTypeMstByAccountHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [NetAccountHdr] INNER JOIN [AccountHdr] ON [NetAccountHdr].[NetAccountID] = [AccountHdr].[NetAccountID] INNER JOIN [AccountTypeMst] ON [AccountHdr].[AccountTypeID] = [AccountTypeMst].[AccountTypeID]  WHERE  [AccountTypeMst].[AccountTypeID] = @AccountTypeID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AccountTypeID";
            par.Value = id;

            cmdParams.Add(par);
            List<NetAccountHdrInfo> objNetAccountHdrEntityColl = new List<NetAccountHdrInfo>(this.Select(cmd));
            return objNetAccountHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<NetAccountHdrInfo> SelectNetAccountHdrDetailsAssociatedToReconciliationTemplateMstByAccountHdr(ReconciliationTemplateMstInfo entity)
        {
            return this.SelectNetAccountHdrDetailsAssociatedToReconciliationTemplateMstByAccountHdr(entity.ReconciliationTemplateID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<NetAccountHdrInfo> SelectNetAccountHdrDetailsAssociatedToReconciliationTemplateMstByAccountHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [NetAccountHdr] INNER JOIN [AccountHdr] ON [NetAccountHdr].[NetAccountID] = [AccountHdr].[NetAccountID] INNER JOIN [ReconciliationTemplateMst] ON [AccountHdr].[ReconciliationTemplateID] = [ReconciliationTemplateMst].[ReconciliationTemplateID]  WHERE  [ReconciliationTemplateMst].[ReconciliationTemplateID] = @ReconciliationTemplateID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationTemplateID";
            par.Value = id;

            cmdParams.Add(par);
            List<NetAccountHdrInfo> objNetAccountHdrEntityColl = new List<NetAccountHdrInfo>(this.Select(cmd));
            return objNetAccountHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<NetAccountHdrInfo> SelectNetAccountHdrDetailsAssociatedToFSCaptionHdrByAccountHdr(FSCaptionHdrInfo entity)
        {
            return this.SelectNetAccountHdrDetailsAssociatedToFSCaptionHdrByAccountHdr(entity.FSCaptionID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<NetAccountHdrInfo> SelectNetAccountHdrDetailsAssociatedToFSCaptionHdrByAccountHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [NetAccountHdr] INNER JOIN [AccountHdr] ON [NetAccountHdr].[NetAccountID] = [AccountHdr].[NetAccountID] INNER JOIN [FSCaptionHdr] ON [AccountHdr].[FSCaptionID] = [FSCaptionHdr].[FSCaptionID]  WHERE  [FSCaptionHdr].[FSCaptionID] = @FSCaptionID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@FSCaptionID";
            par.Value = id;

            cmdParams.Add(par);
            List<NetAccountHdrInfo> objNetAccountHdrEntityColl = new List<NetAccountHdrInfo>(this.Select(cmd));
            return objNetAccountHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<NetAccountHdrInfo> SelectNetAccountHdrDetailsAssociatedToRiskRatingMstByAccountHdr(RiskRatingMstInfo entity)
        {
            return this.SelectNetAccountHdrDetailsAssociatedToRiskRatingMstByAccountHdr(entity.RiskRatingID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<NetAccountHdrInfo> SelectNetAccountHdrDetailsAssociatedToRiskRatingMstByAccountHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [NetAccountHdr] INNER JOIN [AccountHdr] ON [NetAccountHdr].[NetAccountID] = [AccountHdr].[NetAccountID] INNER JOIN [RiskRatingMst] ON [AccountHdr].[RiskRatingID] = [RiskRatingMst].[RiskRatingID]  WHERE  [RiskRatingMst].[RiskRatingID] = @RiskRatingID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RiskRatingID";
            par.Value = id;

            cmdParams.Add(par);
            List<NetAccountHdrInfo> objNetAccountHdrEntityColl = new List<NetAccountHdrInfo>(this.Select(cmd));
            return objNetAccountHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<NetAccountHdrInfo> SelectNetAccountHdrDetailsAssociatedToSubledgerSourceByAccountHdr(SubledgerSourceInfo entity)
        {
            return this.SelectNetAccountHdrDetailsAssociatedToSubledgerSourceByAccountHdr(entity.SubledgerSourceID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<NetAccountHdrInfo> SelectNetAccountHdrDetailsAssociatedToSubledgerSourceByAccountHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [NetAccountHdr] INNER JOIN [AccountHdr] ON [NetAccountHdr].[NetAccountID] = [AccountHdr].[NetAccountID] INNER JOIN [SubledgerSource] ON [AccountHdr].[SubLedgerSourceID] = [SubledgerSource].[SubledgerSourceID]  WHERE  [SubledgerSource].[SubledgerSourceID] = @SubledgerSourceID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@SubledgerSourceID";
            par.Value = id;

            cmdParams.Add(par);
            List<NetAccountHdrInfo> objNetAccountHdrEntityColl = new List<NetAccountHdrInfo>(this.Select(cmd));
            return objNetAccountHdrEntityColl;
        }

    }
}
