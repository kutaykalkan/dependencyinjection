

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

    public abstract class CompanyUnexplainedVarianceThresholdDAOBase : CustomAbstractDAO<CompanyUnexplainedVarianceThresholdInfo>
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
        /// A static representation of column CompanyUnexplainedVarianceThreshold
        /// </summary>
        public static readonly string COLUMN_COMPANYUNEXPLAINEDVARIANCETHRESHOLD = "CompanyUnexplainedVarianceThreshold";
        /// <summary>
        /// A static representation of column CompanyUnexplainedVarianceThresholdID
        /// </summary>
        public static readonly string COLUMN_COMPANYUNEXPLAINEDVARIANCETHRESHOLDID = "CompanyUnexplainedVarianceThresholdID";
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
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// A static representation of column StartReconciliationPeriodID
        /// </summary>
        public static readonly string COLUMN_STARTRECONCILIATIONPERIODID = "StartReconciliationPeriodID";
        /// <summary>
        /// Provides access to the name of the primary key column (CompanyUnexplainedVarianceThresholdID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "CompanyUnexplainedVarianceThresholdID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "CompanyUnexplainedVarianceThreshold";

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
        public CompanyUnexplainedVarianceThresholdDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "CompanyUnexplainedVarianceThreshold", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a CompanyUnexplainedVarianceThresholdInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>CompanyUnexplainedVarianceThresholdInfo</returns>
        protected override CompanyUnexplainedVarianceThresholdInfo MapObject(System.Data.IDataReader r)
        {

            CompanyUnexplainedVarianceThresholdInfo entity = new CompanyUnexplainedVarianceThresholdInfo();


            try
            {
                int ordinal = r.GetOrdinal("CompanyUnexplainedVarianceThresholdID");
                if (!r.IsDBNull(ordinal)) entity.CompanyUnexplainedVarianceThresholdID = ((System.Int32)(r.GetValue(ordinal)));
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
                int ordinal = r.GetOrdinal("CompanyUnexplainedVarianceThreshold");
                if (!r.IsDBNull(ordinal)) entity.CompanyUnexplainedVarianceThreshold = ((System.Decimal)(r.GetValue(ordinal)));
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


            //Added for new field added in SP
            try
            {
                int ordinal = r.GetOrdinal("IsCarryForwardedFromPreviousRecPeriod");
                if (!r.IsDBNull(ordinal))
                {
                    string s = r.GetValue(ordinal).ToString();
                    entity.IsCarryForwardedFromPreviousRecPeriod = ((System.Boolean)(r.GetValue(ordinal)));
                }
                else
                {
                    entity.IsCarryForwardedFromPreviousRecPeriod = null;//TODO: To check
                }
            }
            catch { }

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in CompanyUnexplainedVarianceThresholdInfo object
        /// </summary>
        /// <param name="o">A CompanyUnexplainedVarianceThresholdInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(CompanyUnexplainedVarianceThresholdInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_CompanyUnexplainedVarianceThreshold");
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

            System.Data.IDbDataParameter parCompanyUnexplainedVarianceThreshold = cmd.CreateParameter();
            parCompanyUnexplainedVarianceThreshold.ParameterName = "@CompanyUnexplainedVarianceThreshold";
            if (!entity.IsCompanyUnexplainedVarianceThresholdNull)
                parCompanyUnexplainedVarianceThreshold.Value = entity.CompanyUnexplainedVarianceThreshold;
            else
                parCompanyUnexplainedVarianceThreshold.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyUnexplainedVarianceThreshold);

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

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

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
        /// in CompanyUnexplainedVarianceThresholdInfo object
        /// </summary>
        /// <param name="o">A CompanyUnexplainedVarianceThresholdInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(CompanyUnexplainedVarianceThresholdInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_CompanyUnexplainedVarianceThreshold");
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

            System.Data.IDbDataParameter parCompanyUnexplainedVarianceThreshold = cmd.CreateParameter();
            parCompanyUnexplainedVarianceThreshold.ParameterName = "@CompanyUnexplainedVarianceThreshold";
            if (!entity.IsCompanyUnexplainedVarianceThresholdNull)
                parCompanyUnexplainedVarianceThreshold.Value = entity.CompanyUnexplainedVarianceThreshold;
            else
                parCompanyUnexplainedVarianceThreshold.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyUnexplainedVarianceThreshold);

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

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parStartReconciliationPeriodID = cmd.CreateParameter();
            parStartReconciliationPeriodID.ParameterName = "@StartReconciliationPeriodID";
            if (!entity.IsStartReconciliationPeriodIDNull)
                parStartReconciliationPeriodID.Value = entity.StartReconciliationPeriodID;
            else
                parStartReconciliationPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parStartReconciliationPeriodID);

            System.Data.IDbDataParameter pkparCompanyUnexplainedVarianceThresholdID = cmd.CreateParameter();
            pkparCompanyUnexplainedVarianceThresholdID.ParameterName = "@CompanyUnexplainedVarianceThresholdID";
            pkparCompanyUnexplainedVarianceThresholdID.Value = entity.CompanyUnexplainedVarianceThresholdID;
            cmdParams.Add(pkparCompanyUnexplainedVarianceThresholdID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_CompanyUnexplainedVarianceThreshold");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyUnexplainedVarianceThresholdID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_CompanyUnexplainedVarianceThreshold");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyUnexplainedVarianceThresholdID";
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
        public IList<CompanyUnexplainedVarianceThresholdInfo> SelectAllByCompanyID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CompanyUnexplainedVarianceThresholdByCompanyID");
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
        public IList<CompanyUnexplainedVarianceThresholdInfo> SelectAllByStartReconciliationPeriodID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CompanyUnexplainedVarianceThresholdByStartReconciliationPeriodID");
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
        public IList<CompanyUnexplainedVarianceThresholdInfo> SelectAllByEndReconciliationPeriodID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CompanyUnexplainedVarianceThresholdByEndReconciliationPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@EndReconciliationPeriodID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(CompanyUnexplainedVarianceThresholdInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(CompanyUnexplainedVarianceThresholdDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(CompanyUnexplainedVarianceThresholdInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(CompanyUnexplainedVarianceThresholdDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(CompanyUnexplainedVarianceThresholdInfo entity, object id)
        {
            entity.CompanyUnexplainedVarianceThresholdID = Convert.ToInt32(id);
        }




    }
}
