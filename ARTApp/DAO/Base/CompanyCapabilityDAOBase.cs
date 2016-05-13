

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

    public abstract class CompanyCapabilityDAOBase : CustomAbstractDAO<CompanyCapabilityInfo>
    {

        /// <summary>
        /// A static representation of column AddedBy
        /// </summary>
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
        /// <summary>
        /// A static representation of column CapabilityID
        /// </summary>
        public static readonly string COLUMN_CAPABILITYID = "CapabilityID";
        /// <summary>
        /// A static representation of column CompanyCapabilityID
        /// </summary>
        public static readonly string COLUMN_COMPANYCAPABILITYID = "CompanyCapabilityID";
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
        /// A static representation of column IsActivated
        /// </summary>
        public static readonly string COLUMN_ISACTIVATED = "IsActivated";
        /// <summary>
        /// A static representation of column IsConfigurationComplete
        /// </summary>
        public static readonly string COLUMN_ISCONFIGURATIONCOMPLETE = "IsConfigurationComplete";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// A static representation of column StartReconciliationPeriodID
        /// </summary>
        public static readonly string COLUMN_STARTRECONCILIATIONPERIODID = "StartReconciliationPeriodID";
        /// <summary>
        /// Provides access to the name of the primary key column (CompanyCapabilityID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "CompanyCapabilityID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "CompanyCapability";

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
        public CompanyCapabilityDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "CompanyCapability", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a CompanyCapabilityInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>CompanyCapabilityInfo</returns>
        protected override CompanyCapabilityInfo MapObject(System.Data.IDataReader r)
        {

            CompanyCapabilityInfo entity = new CompanyCapabilityInfo();


            try
            {
                int ordinal = r.GetOrdinal("CompanyCapabilityID");
                if (!r.IsDBNull(ordinal)) entity.CompanyCapabilityID = ((System.Int32)(r.GetValue(ordinal)));
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
                int ordinal = r.GetOrdinal("CapabilityID");
                if (!r.IsDBNull(ordinal)) entity.CapabilityID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("IsActivated");
                if (!r.IsDBNull(ordinal)) entity.IsActivated = ((System.Boolean)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("IsConfigurationComplete");
                if (!r.IsDBNull(ordinal)) entity.IsConfigurationComplete = ((System.Boolean)(r.GetValue(ordinal)));
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

            //Added for new field added in the table
            try
            {
                int ordinal = r.GetOrdinal("IsActivated");
                if (!r.IsDBNull(ordinal)) { entity.IsActivated = ((System.Boolean)(r.GetValue(ordinal))); }
                else
                {
                    entity.IsActivated = null;//TODO: To check
                }
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
        /// in CompanyCapabilityInfo object
        /// </summary>
        /// <param name="o">A CompanyCapabilityInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(CompanyCapabilityInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_CompanyCapability");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parCapabilityID = cmd.CreateParameter();
            parCapabilityID.ParameterName = "@CapabilityID";
            if (!entity.IsCapabilityIDNull)
                parCapabilityID.Value = entity.CapabilityID;
            else
                parCapabilityID.Value = System.DBNull.Value;
            cmdParams.Add(parCapabilityID);

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

            System.Data.IDbDataParameter parIsActivated = cmd.CreateParameter();
            parIsActivated.ParameterName = "@IsActivated";
            if (!entity.IsIsActivatedNull)
                parIsActivated.Value = entity.IsActivated;
            else
                parIsActivated.Value = System.DBNull.Value;
            cmdParams.Add(parIsActivated);

            System.Data.IDbDataParameter parIsConfigurationComplete = cmd.CreateParameter();
            parIsConfigurationComplete.ParameterName = "@IsConfigurationComplete";
            if (!entity.IsIsConfigurationCompleteNull)
                parIsConfigurationComplete.Value = entity.IsConfigurationComplete;
            else
                parIsConfigurationComplete.Value = System.DBNull.Value;
            cmdParams.Add(parIsConfigurationComplete);

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
        /// in CompanyCapabilityInfo object
        /// </summary>
        /// <param name="o">A CompanyCapabilityInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(CompanyCapabilityInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_CompanyCapability");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parCapabilityID = cmd.CreateParameter();
            parCapabilityID.ParameterName = "@CapabilityID";
            if (!entity.IsCapabilityIDNull)
                parCapabilityID.Value = entity.CapabilityID;
            else
                parCapabilityID.Value = System.DBNull.Value;
            cmdParams.Add(parCapabilityID);

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

            System.Data.IDbDataParameter parIsActivated = cmd.CreateParameter();
            parIsActivated.ParameterName = "@IsActivated";
            if (!entity.IsIsActivatedNull)
                parIsActivated.Value = entity.IsActivated;
            else
                parIsActivated.Value = System.DBNull.Value;
            cmdParams.Add(parIsActivated);

            System.Data.IDbDataParameter parIsConfigurationComplete = cmd.CreateParameter();
            parIsConfigurationComplete.ParameterName = "@IsConfigurationComplete";
            if (!entity.IsIsConfigurationCompleteNull)
                parIsConfigurationComplete.Value = entity.IsConfigurationComplete;
            else
                parIsConfigurationComplete.Value = System.DBNull.Value;
            cmdParams.Add(parIsConfigurationComplete);

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

            System.Data.IDbDataParameter pkparCompanyCapabilityID = cmd.CreateParameter();
            pkparCompanyCapabilityID.ParameterName = "@CompanyCapabilityID";
            pkparCompanyCapabilityID.Value = entity.CompanyCapabilityID;
            cmdParams.Add(pkparCompanyCapabilityID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_CompanyCapability");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyCapabilityID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_CompanyCapability");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyCapabilityID";
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
        //public IList<CompanyCapabilityInfo> SelectAllByCompanyID(object id)
        //{

        //        System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CompanyCapabilityByCompanyID");
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        IDataParameterCollection cmdParams = cmd.Parameters;

        //        System.Data.IDbDataParameter par = cmd.CreateParameter();
        //        par.ParameterName = "@CompanyID";
        //        par.Value = id;
        //        cmdParams.Add(par);

        //        return this.Select(cmd);
        //}


        /// <summary>
        /// Creates the sql select command, using the passed in foreign key.  This will return an
        /// IList of all objects that have that foreign key.
        /// </summary>
        /// <param name="o">The foreign key of the objects to select</param>
        /// <returns>An IList</returns>
        public IList<CompanyCapabilityInfo> SelectAllByCapabilityID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CompanyCapabilityByCapabilityID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CapabilityID";
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
        public IList<CompanyCapabilityInfo> SelectAllByStartReconciliationPeriodID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CompanyCapabilityByStartReconciliationPeriodID");
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
        public IList<CompanyCapabilityInfo> SelectAllByEndReconciliationPeriodID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CompanyCapabilityByEndReconciliationPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@EndReconciliationPeriodID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(CompanyCapabilityInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(CompanyCapabilityDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(CompanyCapabilityInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(CompanyCapabilityDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(CompanyCapabilityInfo entity, object id)
        {
            entity.CompanyCapabilityID = Convert.ToInt32(id);
        }




    }
}
