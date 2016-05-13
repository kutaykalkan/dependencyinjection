

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

    public abstract class RiskRatingReconciliationFrequencyDAOBase : CustomAbstractDAO<RiskRatingReconciliationFrequencyInfo>
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
        /// A static representation of column EndReconciliationPeriodID
        /// </summary>
        public static readonly string COLUMN_ENDRECONCILIATIONPERIODID = "EndReconciliationPeriodID";
        /// <summary>
        /// A static representation of column ReconciliationFrequencyID
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONFREQUENCYID = "ReconciliationFrequencyID";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// A static representation of column RiskRatingID
        /// </summary>
        public static readonly string COLUMN_RISKRATINGID = "RiskRatingID";
        /// <summary>
        /// A static representation of column RiskRatingReconciliationFrequencyID
        /// </summary>
        public static readonly string COLUMN_RISKRATINGRECONCILIATIONFREQUENCYID = "RiskRatingReconciliationFrequencyID";
        /// <summary>
        /// A static representation of column StartReconciliationPeriodID
        /// </summary>
        public static readonly string COLUMN_STARTRECONCILIATIONPERIODID = "StartReconciliationPeriodID";
        /// <summary>
        /// Provides access to the name of the primary key column (RiskRatingReconciliationFrequencyID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "RiskRatingReconciliationFrequencyID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "RiskRatingReconciliationFrequency";

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
        public RiskRatingReconciliationFrequencyDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "RiskRatingReconciliationFrequency", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a RiskRatingReconciliationFrequencyInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>RiskRatingReconciliationFrequencyInfo</returns>
        protected override RiskRatingReconciliationFrequencyInfo MapObject(System.Data.IDataReader r)
        {

            RiskRatingReconciliationFrequencyInfo entity = new RiskRatingReconciliationFrequencyInfo();


            try
            {
                int ordinal = r.GetOrdinal("RiskRatingReconciliationFrequencyID");
                if (!r.IsDBNull(ordinal)) entity.RiskRatingReconciliationFrequencyID = ((System.Int32)(r.GetValue(ordinal)));
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
                int ordinal = r.GetOrdinal("RiskRatingID");
                if (!r.IsDBNull(ordinal)) entity.RiskRatingID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ReconciliationFrequencyID");
                if (!r.IsDBNull(ordinal)) entity.ReconciliationFrequencyID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("StartReconciliationPeriodID");
                if (!r.IsDBNull(ordinal)) entity.StartReconciliationPeriodID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("EndReconciliationPeriodID");
                if (!r.IsDBNull(ordinal)) entity.EndReconciliationPeriodID = ((System.Int32)(r.GetValue(ordinal)));
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

            //Added to show the CarryForwardedFromPreviousRecPeriod status on UI
            try
            {
                int ordinal = r.GetOrdinal("IsCarryForwardedFromPreviousRecPeriod");
                if (!r.IsDBNull(ordinal)) entity.IsCarryForwardedFromPreviousRecPeriod = ((System.Boolean)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in RiskRatingReconciliationFrequencyInfo object
        /// </summary>
        /// <param name="o">A RiskRatingReconciliationFrequencyInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(RiskRatingReconciliationFrequencyInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_RiskRatingReconciliationFrequency");
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

            System.Data.IDbDataParameter parEndReconciliationPeriodID = cmd.CreateParameter();
            parEndReconciliationPeriodID.ParameterName = "@EndReconciliationPeriodID";
            if (!entity.IsEndReconciliationPeriodIDNull)
                parEndReconciliationPeriodID.Value = entity.EndReconciliationPeriodID;
            else
                parEndReconciliationPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parEndReconciliationPeriodID);

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

            System.Data.IDbDataParameter parRiskRatingID = cmd.CreateParameter();
            parRiskRatingID.ParameterName = "@RiskRatingID";
            if (!entity.IsRiskRatingIDNull)
                parRiskRatingID.Value = entity.RiskRatingID;
            else
                parRiskRatingID.Value = System.DBNull.Value;
            cmdParams.Add(parRiskRatingID);

            System.Data.IDbDataParameter parStartReconciliationPeriodID = cmd.CreateParameter();
            parStartReconciliationPeriodID.ParameterName = "@StartReconciliationPeriodID";
            if (!entity.IsStartReconciliationPeriodIDNull)
                parStartReconciliationPeriodID.Value = entity.StartReconciliationPeriodID;
            else
                parStartReconciliationPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parStartReconciliationPeriodID);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in RiskRatingReconciliationFrequencyInfo object
        /// </summary>
        /// <param name="o">A RiskRatingReconciliationFrequencyInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(RiskRatingReconciliationFrequencyInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_RiskRatingReconciliationFrequency");
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

            System.Data.IDbDataParameter parEndReconciliationPeriodID = cmd.CreateParameter();
            parEndReconciliationPeriodID.ParameterName = "@EndReconciliationPeriodID";
            if (!entity.IsEndReconciliationPeriodIDNull)
                parEndReconciliationPeriodID.Value = entity.EndReconciliationPeriodID;
            else
                parEndReconciliationPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parEndReconciliationPeriodID);

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

            System.Data.IDbDataParameter parRiskRatingID = cmd.CreateParameter();
            parRiskRatingID.ParameterName = "@RiskRatingID";
            if (!entity.IsRiskRatingIDNull)
                parRiskRatingID.Value = entity.RiskRatingID;
            else
                parRiskRatingID.Value = System.DBNull.Value;
            cmdParams.Add(parRiskRatingID);

            System.Data.IDbDataParameter parStartReconciliationPeriodID = cmd.CreateParameter();
            parStartReconciliationPeriodID.ParameterName = "@StartReconciliationPeriodID";
            if (!entity.IsStartReconciliationPeriodIDNull)
                parStartReconciliationPeriodID.Value = entity.StartReconciliationPeriodID;
            else
                parStartReconciliationPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parStartReconciliationPeriodID);

            System.Data.IDbDataParameter pkparRiskRatingReconciliationFrequencyID = cmd.CreateParameter();
            pkparRiskRatingReconciliationFrequencyID.ParameterName = "@RiskRatingReconciliationFrequencyID";
            pkparRiskRatingReconciliationFrequencyID.Value = entity.RiskRatingReconciliationFrequencyID;
            cmdParams.Add(pkparRiskRatingReconciliationFrequencyID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_RiskRatingReconciliationFrequency");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RiskRatingReconciliationFrequencyID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_RiskRatingReconciliationFrequency");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RiskRatingReconciliationFrequencyID";
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
        public IList<RiskRatingReconciliationFrequencyInfo> SelectAllByCompanyID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_RiskRatingReconciliationFrequencyByCompanyID");
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
        public IList<RiskRatingReconciliationFrequencyInfo> SelectAllByRiskRatingID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_RiskRatingReconciliationFrequencyByRiskRatingID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RiskRatingID";
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
        public IList<RiskRatingReconciliationFrequencyInfo> SelectAllByReconciliationFrequencyID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_RiskRatingReconciliationFrequencyByReconciliationFrequencyID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationFrequencyID";
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
        public IList<RiskRatingReconciliationFrequencyInfo> SelectAllByStartReconciliationPeriodID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_RiskRatingReconciliationFrequencyByStartReconciliationPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@StartReconciliationPeriodID";
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
        public IList<RiskRatingReconciliationFrequencyInfo> SelectAllByEndReconciliationPeriodID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_RiskRatingReconciliationFrequencyByEndReconciliationPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@EndReconciliationPeriodID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(RiskRatingReconciliationFrequencyInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(RiskRatingReconciliationFrequencyDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(RiskRatingReconciliationFrequencyInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(RiskRatingReconciliationFrequencyDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(RiskRatingReconciliationFrequencyInfo entity, object id)
        {
            entity.RiskRatingReconciliationFrequencyID = Convert.ToInt32(id);
        }




    }
}
