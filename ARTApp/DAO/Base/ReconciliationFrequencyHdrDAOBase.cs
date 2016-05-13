

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

    public abstract class ReconciliationFrequencyHdrDAOBase : CustomAbstractDAO<ReconciliationFrequencyHdrInfo>
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
        /// A static representation of column ReconciliationFrequencyID
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONFREQUENCYID = "ReconciliationFrequencyID";
        /// <summary>
        /// A static representation of column ReconciliationFrequencyName
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONFREQUENCYNAME = "ReconciliationFrequencyName";
        /// <summary>
        /// A static representation of column ReconciliationFrequencyNameLabelID
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONFREQUENCYNAMELABELID = "ReconciliationFrequencyNameLabelID";
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
        public static readonly string TABLE_NAME = "ReconciliationFrequencyHdr";

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
        public ReconciliationFrequencyHdrDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "ReconciliationFrequencyHdr", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a ReconciliationFrequencyHdrInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>ReconciliationFrequencyHdrInfo</returns>
        protected override ReconciliationFrequencyHdrInfo MapObject(System.Data.IDataReader r)
        {

            ReconciliationFrequencyHdrInfo entity = new ReconciliationFrequencyHdrInfo();


            try
            {
                int ordinal = r.GetOrdinal("ReconciliationFrequencyID");
                if (!r.IsDBNull(ordinal)) entity.ReconciliationFrequencyID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ReconciliationFrequencyName");
                if (!r.IsDBNull(ordinal)) entity.ReconciliationFrequencyName = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ReconciliationFrequencyNameLabelID");
                if (!r.IsDBNull(ordinal)) entity.ReconciliationFrequencyNameLabelID = ((System.Int32)(r.GetValue(ordinal)));
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

            try
            {
                int ordinal = r.GetOrdinal("CompanyID");
                if (!r.IsDBNull(ordinal)) entity.CompanyID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in ReconciliationFrequencyHdrInfo object
        /// </summary>
        /// <param name="o">A ReconciliationFrequencyHdrInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(ReconciliationFrequencyHdrInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_ReconciliationFrequencyHdr");
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



            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (!entity.IsIsActiveNull)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);



            System.Data.IDbDataParameter parReconciliationFrequencyName = cmd.CreateParameter();
            parReconciliationFrequencyName.ParameterName = "@ReconciliationFrequencyName";
            if (!entity.IsReconciliationFrequencyNameNull)
                parReconciliationFrequencyName.Value = entity.ReconciliationFrequencyName;
            else
                parReconciliationFrequencyName.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationFrequencyName);

            System.Data.IDbDataParameter parReconciliationFrequencyNameLabelID = cmd.CreateParameter();
            parReconciliationFrequencyNameLabelID.ParameterName = "@ReconciliationFrequencyNameLabelID";
            if (!entity.IsReconciliationFrequencyNameLabelIDNull)
                parReconciliationFrequencyNameLabelID.Value = entity.ReconciliationFrequencyNameLabelID;
            else
                parReconciliationFrequencyNameLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationFrequencyNameLabelID);

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
        /// in ReconciliationFrequencyHdrInfo object
        /// </summary>
        /// <param name="o">A ReconciliationFrequencyHdrInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(ReconciliationFrequencyHdrInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_ReconciliationFrequencyHdr");
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

            System.Data.IDbDataParameter parReconciliationFrequencyName = cmd.CreateParameter();
            parReconciliationFrequencyName.ParameterName = "@ReconciliationFrequencyName";
            if (!entity.IsReconciliationFrequencyNameNull)
                parReconciliationFrequencyName.Value = entity.ReconciliationFrequencyName;
            else
                parReconciliationFrequencyName.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationFrequencyName);

            System.Data.IDbDataParameter parReconciliationFrequencyNameLabelID = cmd.CreateParameter();
            parReconciliationFrequencyNameLabelID.ParameterName = "@ReconciliationFrequencyNameLabelID";
            if (!entity.IsReconciliationFrequencyNameLabelIDNull)
                parReconciliationFrequencyNameLabelID.Value = entity.ReconciliationFrequencyNameLabelID;
            else
                parReconciliationFrequencyNameLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationFrequencyNameLabelID);

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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_ReconciliationFrequencyHdr");
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_ReconciliationFrequencyHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationFrequencyID";
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
        public IList<ReconciliationFrequencyHdrInfo> SelectAllByCompanyID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_ReconciliationFrequencyHdrByCompanyID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }

        private void MapIdentity(ReconciliationFrequencyHdrInfo entity, object id)
        {
            entity.ReconciliationFrequencyID = Convert.ToInt32(id);
        }


        protected override void CustomSave(ReconciliationFrequencyHdrInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(UserHdrDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(ReconciliationFrequencyHdrInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(ReconciliationFrequencyHdrDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }	










    }
}
