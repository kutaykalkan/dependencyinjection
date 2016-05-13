

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

    public abstract class AccountTypeMstDAOBase : CustomAbstractDAO<AccountTypeMstInfo>
    {

        /// <summary>
        /// A static representation of column AccountType
        /// </summary>
        public static readonly string COLUMN_ACCOUNTTYPE = "AccountType";
        /// <summary>
        /// A static representation of column AccountTypeID
        /// </summary>
        public static readonly string COLUMN_ACCOUNTTYPEID = "AccountTypeID";
        /// <summary>
        /// A static representation of column AccountTypeLabelID
        /// </summary>
        public static readonly string COLUMN_ACCOUNTTYPELABELID = "AccountTypeLabelID";
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
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// Provides access to the name of the primary key column (AccountTypeID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "AccountTypeID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "AccountTypeMst";

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
        public AccountTypeMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "AccountTypeMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a AccountTypeMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>AccountTypeMstInfo</returns>
        protected override AccountTypeMstInfo MapObject(System.Data.IDataReader r)
        {

            AccountTypeMstInfo entity = new AccountTypeMstInfo();

            entity.AccountTypeID = r.GetInt16Value("AccountTypeID");
            entity.AccountType = r.GetStringValue("AccountType");
            entity.AccountTypeLabelID = r.GetInt32Value("AccountTypeLabelID");
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
        /// in AccountTypeMstInfo object
        /// </summary>
        /// <param name="o">A AccountTypeMstInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(AccountTypeMstInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_AccountTypeMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAccountType = cmd.CreateParameter();
            parAccountType.ParameterName = "@AccountType";
            if (!entity.IsAccountTypeNull)
                parAccountType.Value = entity.AccountType;
            else
                parAccountType.Value = System.DBNull.Value;
            cmdParams.Add(parAccountType);

            System.Data.IDbDataParameter parAccountTypeLabelID = cmd.CreateParameter();
            parAccountTypeLabelID.ParameterName = "@AccountTypeLabelID";
            if (!entity.IsAccountTypeLabelIDNull)
                parAccountTypeLabelID.Value = entity.AccountTypeLabelID;
            else
                parAccountTypeLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parAccountTypeLabelID);

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
        /// in AccountTypeMstInfo object
        /// </summary>
        /// <param name="o">A AccountTypeMstInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(AccountTypeMstInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_AccountTypeMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAccountType = cmd.CreateParameter();
            parAccountType.ParameterName = "@AccountType";
            if (!entity.IsAccountTypeNull)
                parAccountType.Value = entity.AccountType;
            else
                parAccountType.Value = System.DBNull.Value;
            cmdParams.Add(parAccountType);

            System.Data.IDbDataParameter parAccountTypeLabelID = cmd.CreateParameter();
            parAccountTypeLabelID.ParameterName = "@AccountTypeLabelID";
            if (!entity.IsAccountTypeLabelIDNull)
                parAccountTypeLabelID.Value = entity.AccountTypeLabelID;
            else
                parAccountTypeLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parAccountTypeLabelID);

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

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter pkparAccountTypeID = cmd.CreateParameter();
            pkparAccountTypeID.ParameterName = "@AccountTypeID";
            pkparAccountTypeID.Value = entity.AccountTypeID;
            cmdParams.Add(pkparAccountTypeID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_AccountTypeMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AccountTypeID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_AccountTypeMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AccountTypeID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }

        private void MapIdentity(AccountTypeMstInfo entity, object id)
        {
            entity.AccountTypeID = Convert.ToInt16(id);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<AccountTypeMstInfo> SelectAccountTypeMstDetailsAssociatedToGeographyObjectHdrByAccountHdr(GeographyObjectHdrInfo entity)
        {
            return this.SelectAccountTypeMstDetailsAssociatedToGeographyObjectHdrByAccountHdr(entity.GeographyObjectID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<AccountTypeMstInfo> SelectAccountTypeMstDetailsAssociatedToGeographyObjectHdrByAccountHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [AccountTypeMst] INNER JOIN [AccountHdr] ON [AccountTypeMst].[AccountTypeID] = [AccountHdr].[AccountTypeID] INNER JOIN [GeographyObjectHdr] ON [AccountHdr].[GeographyObjectID] = [GeographyObjectHdr].[GeographyObjectID]  WHERE  [GeographyObjectHdr].[GeographyObjectID] = @GeographyObjectID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GeographyObjectID";
            par.Value = id;

            cmdParams.Add(par);
            List<AccountTypeMstInfo> objAccountTypeMstEntityColl = new List<AccountTypeMstInfo>(this.Select(cmd));
            return objAccountTypeMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<AccountTypeMstInfo> SelectAccountTypeMstDetailsAssociatedToReconciliationTemplateMstByAccountHdr(ReconciliationTemplateMstInfo entity)
        {
            return this.SelectAccountTypeMstDetailsAssociatedToReconciliationTemplateMstByAccountHdr(entity.ReconciliationTemplateID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<AccountTypeMstInfo> SelectAccountTypeMstDetailsAssociatedToReconciliationTemplateMstByAccountHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [AccountTypeMst] INNER JOIN [AccountHdr] ON [AccountTypeMst].[AccountTypeID] = [AccountHdr].[AccountTypeID] INNER JOIN [ReconciliationTemplateMst] ON [AccountHdr].[ReconciliationTemplateID] = [ReconciliationTemplateMst].[ReconciliationTemplateID]  WHERE  [ReconciliationTemplateMst].[ReconciliationTemplateID] = @ReconciliationTemplateID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationTemplateID";
            par.Value = id;

            cmdParams.Add(par);
            List<AccountTypeMstInfo> objAccountTypeMstEntityColl = new List<AccountTypeMstInfo>(this.Select(cmd));
            return objAccountTypeMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<AccountTypeMstInfo> SelectAccountTypeMstDetailsAssociatedToFSCaptionHdrByAccountHdr(FSCaptionHdrInfo entity)
        {
            return this.SelectAccountTypeMstDetailsAssociatedToFSCaptionHdrByAccountHdr(entity.FSCaptionID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<AccountTypeMstInfo> SelectAccountTypeMstDetailsAssociatedToFSCaptionHdrByAccountHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [AccountTypeMst] INNER JOIN [AccountHdr] ON [AccountTypeMst].[AccountTypeID] = [AccountHdr].[AccountTypeID] INNER JOIN [FSCaptionHdr] ON [AccountHdr].[FSCaptionID] = [FSCaptionHdr].[FSCaptionID]  WHERE  [FSCaptionHdr].[FSCaptionID] = @FSCaptionID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@FSCaptionID";
            par.Value = id;

            cmdParams.Add(par);
            List<AccountTypeMstInfo> objAccountTypeMstEntityColl = new List<AccountTypeMstInfo>(this.Select(cmd));
            return objAccountTypeMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<AccountTypeMstInfo> SelectAccountTypeMstDetailsAssociatedToRiskRatingMstByAccountHdr(RiskRatingMstInfo entity)
        {
            return this.SelectAccountTypeMstDetailsAssociatedToRiskRatingMstByAccountHdr(entity.RiskRatingID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<AccountTypeMstInfo> SelectAccountTypeMstDetailsAssociatedToRiskRatingMstByAccountHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [AccountTypeMst] INNER JOIN [AccountHdr] ON [AccountTypeMst].[AccountTypeID] = [AccountHdr].[AccountTypeID] INNER JOIN [RiskRatingMst] ON [AccountHdr].[RiskRatingID] = [RiskRatingMst].[RiskRatingID]  WHERE  [RiskRatingMst].[RiskRatingID] = @RiskRatingID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RiskRatingID";
            par.Value = id;

            cmdParams.Add(par);
            List<AccountTypeMstInfo> objAccountTypeMstEntityColl = new List<AccountTypeMstInfo>(this.Select(cmd));
            return objAccountTypeMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<AccountTypeMstInfo> SelectAccountTypeMstDetailsAssociatedToSubledgerSourceByAccountHdr(SubledgerSourceInfo entity)
        {
            return this.SelectAccountTypeMstDetailsAssociatedToSubledgerSourceByAccountHdr(entity.SubledgerSourceID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<AccountTypeMstInfo> SelectAccountTypeMstDetailsAssociatedToSubledgerSourceByAccountHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [AccountTypeMst] INNER JOIN [AccountHdr] ON [AccountTypeMst].[AccountTypeID] = [AccountHdr].[AccountTypeID] INNER JOIN [SubledgerSource] ON [AccountHdr].[SubLedgerSourceID] = [SubledgerSource].[SubledgerSourceID]  WHERE  [SubledgerSource].[SubledgerSourceID] = @SubledgerSourceID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@SubledgerSourceID";
            par.Value = id;

            cmdParams.Add(par);
            List<AccountTypeMstInfo> objAccountTypeMstEntityColl = new List<AccountTypeMstInfo>(this.Select(cmd));
            return objAccountTypeMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<AccountTypeMstInfo> SelectAccountTypeMstDetailsAssociatedToNetAccountHdrByAccountHdr(NetAccountHdrInfo entity)
        {
            return this.SelectAccountTypeMstDetailsAssociatedToNetAccountHdrByAccountHdr(entity.NetAccountID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<AccountTypeMstInfo> SelectAccountTypeMstDetailsAssociatedToNetAccountHdrByAccountHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [AccountTypeMst] INNER JOIN [AccountHdr] ON [AccountTypeMst].[AccountTypeID] = [AccountHdr].[AccountTypeID] INNER JOIN [NetAccountHdr] ON [AccountHdr].[NetAccountID] = [NetAccountHdr].[NetAccountID]  WHERE  [NetAccountHdr].[NetAccountID] = @NetAccountID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@NetAccountID";
            par.Value = id;

            cmdParams.Add(par);
            List<AccountTypeMstInfo> objAccountTypeMstEntityColl = new List<AccountTypeMstInfo>(this.Select(cmd));
            return objAccountTypeMstEntityColl;
        }

    }
}
