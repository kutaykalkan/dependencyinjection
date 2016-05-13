

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

    public abstract class ReconciliationFrequencyMstDAOBase : CustomAbstractDAO<ReconciliationFrequencyMstInfo>
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
        /// A static representation of column ReconciliationFrequency
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONFREQUENCY = "ReconciliationFrequency";
        /// <summary>
        /// A static representation of column ReconciliationFrequencyID
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONFREQUENCYID = "ReconciliationFrequencyID";
        /// <summary>
        /// A static representation of column ReconciliationFrequencyLabelID
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONFREQUENCYLABELID = "ReconciliationFrequencyLabelID";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// Provides access to the name of the primary key column (ReconciliationFrequencyID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "ReconciliationFrequencyID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "ReconciliationFrequencyMst";

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
        public ReconciliationFrequencyMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "ReconciliationFrequencyMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a ReconciliationFrequencyMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>ReconciliationFrequencyMstInfo</returns>
        protected override ReconciliationFrequencyMstInfo MapObject(System.Data.IDataReader r)
        {

            ReconciliationFrequencyMstInfo entity = new ReconciliationFrequencyMstInfo();


            try
            {
                int ordinal = r.GetOrdinal("ReconciliationFrequencyID");
                if (!r.IsDBNull(ordinal)) entity.ReconciliationFrequencyID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ReconciliationFrequency");
                if (!r.IsDBNull(ordinal)) entity.ReconciliationFrequency = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ReconciliationFrequencyLabelID");
                if (!r.IsDBNull(ordinal)) entity.ReconciliationFrequencyLabelID = ((System.Int32)(r.GetValue(ordinal)));
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
        /// in ReconciliationFrequencyMstInfo object
        /// </summary>
        /// <param name="o">A ReconciliationFrequencyMstInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(ReconciliationFrequencyMstInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_ReconciliationFrequencyMst");
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

            System.Data.IDbDataParameter parReconciliationFrequency = cmd.CreateParameter();
            parReconciliationFrequency.ParameterName = "@ReconciliationFrequency";
            if (!entity.IsReconciliationFrequencyNull)
                parReconciliationFrequency.Value = entity.ReconciliationFrequency;
            else
                parReconciliationFrequency.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationFrequency);

            System.Data.IDbDataParameter parReconciliationFrequencyLabelID = cmd.CreateParameter();
            parReconciliationFrequencyLabelID.ParameterName = "@ReconciliationFrequencyLabelID";
            if (!entity.IsReconciliationFrequencyLabelIDNull)
                parReconciliationFrequencyLabelID.Value = entity.ReconciliationFrequencyLabelID;
            else
                parReconciliationFrequencyLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationFrequencyLabelID);

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
        /// in ReconciliationFrequencyMstInfo object
        /// </summary>
        /// <param name="o">A ReconciliationFrequencyMstInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(ReconciliationFrequencyMstInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_ReconciliationFrequencyMst");
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

            System.Data.IDbDataParameter parReconciliationFrequency = cmd.CreateParameter();
            parReconciliationFrequency.ParameterName = "@ReconciliationFrequency";
            if (!entity.IsReconciliationFrequencyNull)
                parReconciliationFrequency.Value = entity.ReconciliationFrequency;
            else
                parReconciliationFrequency.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationFrequency);

            System.Data.IDbDataParameter parReconciliationFrequencyLabelID = cmd.CreateParameter();
            parReconciliationFrequencyLabelID.ParameterName = "@ReconciliationFrequencyLabelID";
            if (!entity.IsReconciliationFrequencyLabelIDNull)
                parReconciliationFrequencyLabelID.Value = entity.ReconciliationFrequencyLabelID;
            else
                parReconciliationFrequencyLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationFrequencyLabelID);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter pkparReconciliationFrequencyID = cmd.CreateParameter();
            pkparReconciliationFrequencyID.ParameterName = "@ReconciliationFrequencyID";
            pkparReconciliationFrequencyID.Value = entity.ReconciliationFrequencyID;
            cmdParams.Add(pkparReconciliationFrequencyID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_ReconciliationFrequencyMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationFrequencyID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_ReconciliationFrequencyMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationFrequencyID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }







        protected override void CustomSave(ReconciliationFrequencyMstInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(ReconciliationFrequencyMstDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(ReconciliationFrequencyMstInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(ReconciliationFrequencyMstDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(ReconciliationFrequencyMstInfo entity, object id)
        {
            entity.ReconciliationFrequencyID = Convert.ToInt16(id);
        }









        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationFrequencyMstInfo> SelectReconciliationFrequencyMstDetailsAssociatedToCompanyHdrByRiskRatingReconciliationFrequency(CompanyHdrInfo entity)
        {
            return this.SelectReconciliationFrequencyMstDetailsAssociatedToCompanyHdrByRiskRatingReconciliationFrequency(entity.CompanyID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationFrequencyMstInfo> SelectReconciliationFrequencyMstDetailsAssociatedToCompanyHdrByRiskRatingReconciliationFrequency(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReconciliationFrequencyMst] INNER JOIN [RiskRatingReconciliationFrequency] ON [ReconciliationFrequencyMst].[ReconciliationFrequencyID] = [RiskRatingReconciliationFrequency].[ReconciliationFrequencyID] INNER JOIN [CompanyHdr] ON [RiskRatingReconciliationFrequency].[CompanyID] = [CompanyHdr].[CompanyID]  WHERE  [CompanyHdr].[CompanyID] = @CompanyID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReconciliationFrequencyMstInfo> objReconciliationFrequencyMstEntityColl = new List<ReconciliationFrequencyMstInfo>(this.Select(cmd));
            return objReconciliationFrequencyMstEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationFrequencyMstInfo> SelectReconciliationFrequencyMstDetailsAssociatedToRiskRatingMstByRiskRatingReconciliationFrequency(RiskRatingMstInfo entity)
        {
            return this.SelectReconciliationFrequencyMstDetailsAssociatedToRiskRatingMstByRiskRatingReconciliationFrequency(entity.RiskRatingID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationFrequencyMstInfo> SelectReconciliationFrequencyMstDetailsAssociatedToRiskRatingMstByRiskRatingReconciliationFrequency(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReconciliationFrequencyMst] INNER JOIN [RiskRatingReconciliationFrequency] ON [ReconciliationFrequencyMst].[ReconciliationFrequencyID] = [RiskRatingReconciliationFrequency].[ReconciliationFrequencyID] INNER JOIN [RiskRatingMst] ON [RiskRatingReconciliationFrequency].[RiskRatingID] = [RiskRatingMst].[RiskRatingID]  WHERE  [RiskRatingMst].[RiskRatingID] = @RiskRatingID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RiskRatingID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReconciliationFrequencyMstInfo> objReconciliationFrequencyMstEntityColl = new List<ReconciliationFrequencyMstInfo>(this.Select(cmd));
            return objReconciliationFrequencyMstEntityColl;
        }

    }
}
