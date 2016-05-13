

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

    public abstract class GeographyObjectHdrDAOBase : CustomAbstractDAO<GeographyObjectHdrInfo>
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
        /// A static representation of column GeographyObject
        /// </summary>
        public static readonly string COLUMN_GEOGRAPHYOBJECT = "GeographyObject";
        /// <summary>
        /// A static representation of column GeographyObjectID
        /// </summary>
        public static readonly string COLUMN_GEOGRAPHYOBJECTID = "GeographyObjectID";
        /// <summary>
        /// A static representation of column GeographyObjectNumber
        /// </summary>
        public static readonly string COLUMN_GEOGRAPHYOBJECTNUMBER = "GeographyObjectNumber";
        /// <summary>
        /// A static representation of column GeographyStructureID
        /// </summary>
        public static readonly string COLUMN_GEOGRAPHYSTRUCTUREID = "GeographyStructureID";
        /// <summary>
        /// A static representation of column HostName
        /// </summary>
        public static readonly string COLUMN_HOSTNAME = "HostName";
        /// <summary>
        /// A static representation of column IsActive
        /// </summary>
        public static readonly string COLUMN_ISACTIVE = "IsActive";
        /// <summary>
        /// A static representation of column ParentGeographyObjectID
        /// </summary>
        public static readonly string COLUMN_PARENTGEOGRAPHYOBJECTID = "ParentGeographyObjectID";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// Provides access to the name of the primary key column (GeographyObjectID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "GeographyObjectID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "GeographyObjectHdr";

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
        public GeographyObjectHdrDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "GeographyObjectHdr", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a GeographyObjectHdrInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>GeographyObjectHdrInfo</returns>
        protected override GeographyObjectHdrInfo MapObject(System.Data.IDataReader r)
        {

            GeographyObjectHdrInfo entity = new GeographyObjectHdrInfo();


            try
            {
                int ordinal = r.GetOrdinal("GeographyObjectID");
                if (!r.IsDBNull(ordinal)) entity.GeographyObjectID = ((System.Int32)(r.GetValue(ordinal)));
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
                int ordinal = r.GetOrdinal("GeographyStructureID");
                if (!r.IsDBNull(ordinal)) entity.GeographyStructureID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("GeographyObjectNumber");
                if (!r.IsDBNull(ordinal)) entity.GeographyObjectNumber = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("GeographyObject");
                if (!r.IsDBNull(ordinal)) entity.GeographyObject = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ParentGeographyObjectID");
                if (!r.IsDBNull(ordinal)) entity.ParentGeographyObjectID = ((System.Int32)(r.GetValue(ordinal)));
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
        /// in GeographyObjectHdrInfo object
        /// </summary>
        /// <param name="o">A GeographyObjectHdrInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(GeographyObjectHdrInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_GeographyObjectHdr");
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

            System.Data.IDbDataParameter parGeographyObject = cmd.CreateParameter();
            parGeographyObject.ParameterName = "@GeographyObject";
            if (!entity.IsGeographyObjectNull)
                parGeographyObject.Value = entity.GeographyObject;
            else
                parGeographyObject.Value = System.DBNull.Value;
            cmdParams.Add(parGeographyObject);

            System.Data.IDbDataParameter parGeographyObjectNumber = cmd.CreateParameter();
            parGeographyObjectNumber.ParameterName = "@GeographyObjectNumber";
            if (!entity.IsGeographyObjectNumberNull)
                parGeographyObjectNumber.Value = entity.GeographyObjectNumber;
            else
                parGeographyObjectNumber.Value = System.DBNull.Value;
            cmdParams.Add(parGeographyObjectNumber);

            System.Data.IDbDataParameter parGeographyStructureID = cmd.CreateParameter();
            parGeographyStructureID.ParameterName = "@GeographyStructureID";
            if (!entity.IsGeographyStructureIDNull)
                parGeographyStructureID.Value = entity.GeographyStructureID;
            else
                parGeographyStructureID.Value = System.DBNull.Value;
            cmdParams.Add(parGeographyStructureID);

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

            System.Data.IDbDataParameter parParentGeographyObjectID = cmd.CreateParameter();
            parParentGeographyObjectID.ParameterName = "@ParentGeographyObjectID";
            if (!entity.IsParentGeographyObjectIDNull)
                parParentGeographyObjectID.Value = entity.ParentGeographyObjectID;
            else
                parParentGeographyObjectID.Value = System.DBNull.Value;
            cmdParams.Add(parParentGeographyObjectID);

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
        /// in GeographyObjectHdrInfo object
        /// </summary>
        /// <param name="o">A GeographyObjectHdrInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(GeographyObjectHdrInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_GeographyObjectHdr");
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

            System.Data.IDbDataParameter parGeographyObject = cmd.CreateParameter();
            parGeographyObject.ParameterName = "@GeographyObject";
            if (!entity.IsGeographyObjectNull)
                parGeographyObject.Value = entity.GeographyObject;
            else
                parGeographyObject.Value = System.DBNull.Value;
            cmdParams.Add(parGeographyObject);

            System.Data.IDbDataParameter parGeographyObjectNumber = cmd.CreateParameter();
            parGeographyObjectNumber.ParameterName = "@GeographyObjectNumber";
            if (!entity.IsGeographyObjectNumberNull)
                parGeographyObjectNumber.Value = entity.GeographyObjectNumber;
            else
                parGeographyObjectNumber.Value = System.DBNull.Value;
            cmdParams.Add(parGeographyObjectNumber);

            System.Data.IDbDataParameter parGeographyStructureID = cmd.CreateParameter();
            parGeographyStructureID.ParameterName = "@GeographyStructureID";
            if (!entity.IsGeographyStructureIDNull)
                parGeographyStructureID.Value = entity.GeographyStructureID;
            else
                parGeographyStructureID.Value = System.DBNull.Value;
            cmdParams.Add(parGeographyStructureID);

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

            System.Data.IDbDataParameter parParentGeographyObjectID = cmd.CreateParameter();
            parParentGeographyObjectID.ParameterName = "@ParentGeographyObjectID";
            if (!entity.IsParentGeographyObjectIDNull)
                parParentGeographyObjectID.Value = entity.ParentGeographyObjectID;
            else
                parParentGeographyObjectID.Value = System.DBNull.Value;
            cmdParams.Add(parParentGeographyObjectID);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter pkparGeographyObjectID = cmd.CreateParameter();
            pkparGeographyObjectID.ParameterName = "@GeographyObjectID";
            pkparGeographyObjectID.Value = entity.GeographyObjectID;
            cmdParams.Add(pkparGeographyObjectID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_GeographyObjectHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GeographyObjectID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_GeographyObjectHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GeographyObjectID";
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
        public IList<GeographyObjectHdrInfo> SelectAllByCompanyID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_GeographyObjectHdrByCompanyID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }


        /// <summary>
        /// Creates the sql select command, using the passed in foreign key.  This will return an
        /// IList of all objects that have that foreign key.
        /// </summary>
        /// <param name="o">The foreign key of the objects to select</param>
        /// <returns>An IList</returns>
        public IList<GeographyObjectHdrInfo> SelectAllByGeographyStructureID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_GeographyObjectHdrByGeographyStructureID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GeographyStructureID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }


        /// <summary>
        /// Creates the sql select command, using the passed in foreign key.  This will return an
        /// IList of all objects that have that foreign key.
        /// </summary>
        /// <param name="o">The foreign key of the objects to select</param>
        /// <returns>An IList</returns>
        public IList<GeographyObjectHdrInfo> SelectAllByParentGeographyObjectID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_GeographyObjectHdrByParentGeographyObjectID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ParentGeographyObjectID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(GeographyObjectHdrInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(GeographyObjectHdrDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(GeographyObjectHdrInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(GeographyObjectHdrDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(GeographyObjectHdrInfo entity, object id)
        {
            entity.GeographyObjectID = Convert.ToInt32(id);
        }











        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<GeographyObjectHdrInfo> SelectGeographyObjectHdrDetailsAssociatedToAccountTypeMstByAccountHdr(AccountTypeMstInfo entity)
        {
            return this.SelectGeographyObjectHdrDetailsAssociatedToAccountTypeMstByAccountHdr(entity.AccountTypeID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<GeographyObjectHdrInfo> SelectGeographyObjectHdrDetailsAssociatedToAccountTypeMstByAccountHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [GeographyObjectHdr] INNER JOIN [AccountHdr] ON [GeographyObjectHdr].[GeographyObjectID] = [AccountHdr].[GeographyObjectID] INNER JOIN [AccountTypeMst] ON [AccountHdr].[AccountTypeID] = [AccountTypeMst].[AccountTypeID]  WHERE  [AccountTypeMst].[AccountTypeID] = @AccountTypeID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AccountTypeID";
            par.Value = id;

            cmdParams.Add(par);
            List<GeographyObjectHdrInfo> objGeographyObjectHdrEntityColl = new List<GeographyObjectHdrInfo>(this.Select(cmd));
            return objGeographyObjectHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<GeographyObjectHdrInfo> SelectGeographyObjectHdrDetailsAssociatedToReconciliationTemplateMstByAccountHdr(ReconciliationTemplateMstInfo entity)
        {
            return this.SelectGeographyObjectHdrDetailsAssociatedToReconciliationTemplateMstByAccountHdr(entity.ReconciliationTemplateID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<GeographyObjectHdrInfo> SelectGeographyObjectHdrDetailsAssociatedToReconciliationTemplateMstByAccountHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [GeographyObjectHdr] INNER JOIN [AccountHdr] ON [GeographyObjectHdr].[GeographyObjectID] = [AccountHdr].[GeographyObjectID] INNER JOIN [ReconciliationTemplateMst] ON [AccountHdr].[ReconciliationTemplateID] = [ReconciliationTemplateMst].[ReconciliationTemplateID]  WHERE  [ReconciliationTemplateMst].[ReconciliationTemplateID] = @ReconciliationTemplateID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationTemplateID";
            par.Value = id;

            cmdParams.Add(par);
            List<GeographyObjectHdrInfo> objGeographyObjectHdrEntityColl = new List<GeographyObjectHdrInfo>(this.Select(cmd));
            return objGeographyObjectHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<GeographyObjectHdrInfo> SelectGeographyObjectHdrDetailsAssociatedToFSCaptionHdrByAccountHdr(FSCaptionHdrInfo entity)
        {
            return this.SelectGeographyObjectHdrDetailsAssociatedToFSCaptionHdrByAccountHdr(entity.FSCaptionID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<GeographyObjectHdrInfo> SelectGeographyObjectHdrDetailsAssociatedToFSCaptionHdrByAccountHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [GeographyObjectHdr] INNER JOIN [AccountHdr] ON [GeographyObjectHdr].[GeographyObjectID] = [AccountHdr].[GeographyObjectID] INNER JOIN [FSCaptionHdr] ON [AccountHdr].[FSCaptionID] = [FSCaptionHdr].[FSCaptionID]  WHERE  [FSCaptionHdr].[FSCaptionID] = @FSCaptionID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@FSCaptionID";
            par.Value = id;

            cmdParams.Add(par);
            List<GeographyObjectHdrInfo> objGeographyObjectHdrEntityColl = new List<GeographyObjectHdrInfo>(this.Select(cmd));
            return objGeographyObjectHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<GeographyObjectHdrInfo> SelectGeographyObjectHdrDetailsAssociatedToRiskRatingMstByAccountHdr(RiskRatingMstInfo entity)
        {
            return this.SelectGeographyObjectHdrDetailsAssociatedToRiskRatingMstByAccountHdr(entity.RiskRatingID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<GeographyObjectHdrInfo> SelectGeographyObjectHdrDetailsAssociatedToRiskRatingMstByAccountHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [GeographyObjectHdr] INNER JOIN [AccountHdr] ON [GeographyObjectHdr].[GeographyObjectID] = [AccountHdr].[GeographyObjectID] INNER JOIN [RiskRatingMst] ON [AccountHdr].[RiskRatingID] = [RiskRatingMst].[RiskRatingID]  WHERE  [RiskRatingMst].[RiskRatingID] = @RiskRatingID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RiskRatingID";
            par.Value = id;

            cmdParams.Add(par);
            List<GeographyObjectHdrInfo> objGeographyObjectHdrEntityColl = new List<GeographyObjectHdrInfo>(this.Select(cmd));
            return objGeographyObjectHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<GeographyObjectHdrInfo> SelectGeographyObjectHdrDetailsAssociatedToSubledgerSourceByAccountHdr(SubledgerSourceInfo entity)
        {
            return this.SelectGeographyObjectHdrDetailsAssociatedToSubledgerSourceByAccountHdr(entity.SubledgerSourceID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<GeographyObjectHdrInfo> SelectGeographyObjectHdrDetailsAssociatedToSubledgerSourceByAccountHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [GeographyObjectHdr] INNER JOIN [AccountHdr] ON [GeographyObjectHdr].[GeographyObjectID] = [AccountHdr].[GeographyObjectID] INNER JOIN [SubledgerSource] ON [AccountHdr].[SubLedgerSourceID] = [SubledgerSource].[SubledgerSourceID]  WHERE  [SubledgerSource].[SubledgerSourceID] = @SubledgerSourceID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@SubledgerSourceID";
            par.Value = id;

            cmdParams.Add(par);
            List<GeographyObjectHdrInfo> objGeographyObjectHdrEntityColl = new List<GeographyObjectHdrInfo>(this.Select(cmd));
            return objGeographyObjectHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<GeographyObjectHdrInfo> SelectGeographyObjectHdrDetailsAssociatedToNetAccountHdrByAccountHdr(NetAccountHdrInfo entity)
        {
            return this.SelectGeographyObjectHdrDetailsAssociatedToNetAccountHdrByAccountHdr(entity.NetAccountID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<GeographyObjectHdrInfo> SelectGeographyObjectHdrDetailsAssociatedToNetAccountHdrByAccountHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [GeographyObjectHdr] INNER JOIN [AccountHdr] ON [GeographyObjectHdr].[GeographyObjectID] = [AccountHdr].[GeographyObjectID] INNER JOIN [NetAccountHdr] ON [AccountHdr].[NetAccountID] = [NetAccountHdr].[NetAccountID]  WHERE  [NetAccountHdr].[NetAccountID] = @NetAccountID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@NetAccountID";
            par.Value = id;

            cmdParams.Add(par);
            List<GeographyObjectHdrInfo> objGeographyObjectHdrEntityColl = new List<GeographyObjectHdrInfo>(this.Select(cmd));
            return objGeographyObjectHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<GeographyObjectHdrInfo> SelectGeographyObjectHdrDetailsAssociatedToCompanyHdrByGeographyObjectHdr(CompanyHdrInfo entity)
        {
            return this.SelectGeographyObjectHdrDetailsAssociatedToCompanyHdrByGeographyObjectHdr(entity.CompanyID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<GeographyObjectHdrInfo> SelectGeographyObjectHdrDetailsAssociatedToCompanyHdrByGeographyObjectHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [GeographyObjectHdr] INNER JOIN [GeographyObjectHdr] ON [GeographyObjectHdr].[GeographyObjectID] = [GeographyObjectHdr].[ParentGeographyObjectID] INNER JOIN [CompanyHdr] ON [GeographyObjectHdr].[CompanyID] = [CompanyHdr].[CompanyID]  WHERE  [CompanyHdr].[CompanyID] = @CompanyID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyID";
            par.Value = id;

            cmdParams.Add(par);
            List<GeographyObjectHdrInfo> objGeographyObjectHdrEntityColl = new List<GeographyObjectHdrInfo>(this.Select(cmd));
            return objGeographyObjectHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<GeographyObjectHdrInfo> SelectGeographyObjectHdrDetailsAssociatedToGeographyStructureHdrByGeographyObjectHdr(GeographyStructureHdrInfo entity)
        {
            return this.SelectGeographyObjectHdrDetailsAssociatedToGeographyStructureHdrByGeographyObjectHdr(entity.GeographyStructureID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<GeographyObjectHdrInfo> SelectGeographyObjectHdrDetailsAssociatedToGeographyStructureHdrByGeographyObjectHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [GeographyObjectHdr] INNER JOIN [GeographyObjectHdr] ON [GeographyObjectHdr].[GeographyObjectID] = [GeographyObjectHdr].[ParentGeographyObjectID] INNER JOIN [GeographyStructureHdr] ON [GeographyObjectHdr].[GeographyStructureID] = [GeographyStructureHdr].[GeographyStructureID]  WHERE  [GeographyStructureHdr].[GeographyStructureID] = @GeographyStructureID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GeographyStructureID";
            par.Value = id;

            cmdParams.Add(par);
            List<GeographyObjectHdrInfo> objGeographyObjectHdrEntityColl = new List<GeographyObjectHdrInfo>(this.Select(cmd));
            return objGeographyObjectHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<GeographyObjectHdrInfo> SelectGeographyObjectHdrDetailsAssociatedToUserHdrByUserGeographyObject(UserHdrInfo entity)
        {
            return this.SelectGeographyObjectHdrDetailsAssociatedToUserHdrByUserGeographyObject(entity.UserID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<GeographyObjectHdrInfo> SelectGeographyObjectHdrDetailsAssociatedToUserHdrByUserGeographyObject(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [GeographyObjectHdr] INNER JOIN [UserGeographyObject] ON [GeographyObjectHdr].[GeographyObjectID] = [UserGeographyObject].[GeographyObjectID] INNER JOIN [UserHdr] ON [UserGeographyObject].[UserID] = [UserHdr].[UserID]  WHERE  [UserHdr].[UserID] = @UserID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserID";
            par.Value = id;

            cmdParams.Add(par);
            List<GeographyObjectHdrInfo> objGeographyObjectHdrEntityColl = new List<GeographyObjectHdrInfo>(this.Select(cmd));
            return objGeographyObjectHdrEntityColl;
        }

    }
}
