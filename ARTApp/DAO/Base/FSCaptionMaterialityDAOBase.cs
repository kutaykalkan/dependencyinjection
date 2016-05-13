

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

    public abstract class FSCaptionMaterialityDAOBase : CustomAbstractDAO<FSCaptionMaterialityInfo>
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
        /// A static representation of column EndReconciliationPeriodID
        /// </summary>
        public static readonly string COLUMN_ENDRECONCILIATIONPERIODID = "EndReconciliationPeriodID";
        /// <summary>
        /// A static representation of column FSCaptionID
        /// </summary>
        public static readonly string COLUMN_FSCAPTIONID = "FSCaptionID";
        /// <summary>
        /// A static representation of column FSCaptionMaterialityID
        /// </summary>
        public static readonly string COLUMN_FSCAPTIONMATERIALITYID = "FSCaptionMaterialityID";
        /// <summary>
        /// A static representation of column MaterialityThreshold
        /// </summary>
        public static readonly string COLUMN_MATERIALITYTHRESHOLD = "MaterialityThreshold";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// A static representation of column StartReconciliationPeriodID
        /// </summary>
        public static readonly string COLUMN_STARTRECONCILIATIONPERIODID = "StartReconciliationPeriodID";
        /// <summary>
        /// Provides access to the name of the primary key column (FSCaptionMaterialityID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "FSCaptionMaterialityID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "FSCaptionMateriality";

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
        public FSCaptionMaterialityDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "FSCaptionMateriality", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a FSCaptionMaterialityInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>FSCaptionMaterialityInfo</returns>
        protected override FSCaptionMaterialityInfo MapObject(System.Data.IDataReader r)
        {

            FSCaptionMaterialityInfo entity = new FSCaptionMaterialityInfo();


            try
            {
                int ordinal = r.GetOrdinal("FSCaptionMaterialityID");
                if (!r.IsDBNull(ordinal)) entity.FSCaptionMaterialityID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("FSCaptionID");
                if (!r.IsDBNull(ordinal)) entity.FSCaptionID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("MaterialityThreshold");
                if (!r.IsDBNull(ordinal)) entity.MaterialityThreshold = ((System.Decimal)(r.GetValue(ordinal)));
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

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in FSCaptionMaterialityInfo object
        /// </summary>
        /// <param name="o">A FSCaptionMaterialityInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(FSCaptionMaterialityInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_FSCaptionMateriality");
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

            System.Data.IDbDataParameter parEndReconciliationPeriodID = cmd.CreateParameter();
            parEndReconciliationPeriodID.ParameterName = "@EndReconciliationPeriodID";
            if (!entity.IsEndReconciliationPeriodIDNull)
                parEndReconciliationPeriodID.Value = entity.EndReconciliationPeriodID;
            else
                parEndReconciliationPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parEndReconciliationPeriodID);

            System.Data.IDbDataParameter parFSCaptionID = cmd.CreateParameter();
            parFSCaptionID.ParameterName = "@FSCaptionID";
            if (!entity.IsFSCaptionIDNull)
                parFSCaptionID.Value = entity.FSCaptionID;
            else
                parFSCaptionID.Value = System.DBNull.Value;
            cmdParams.Add(parFSCaptionID);

            System.Data.IDbDataParameter parMaterialityThreshold = cmd.CreateParameter();
            parMaterialityThreshold.ParameterName = "@MaterialityThreshold";
            if (!entity.IsMaterialityThresholdNull)
                parMaterialityThreshold.Value = entity.MaterialityThreshold;
            else
                parMaterialityThreshold.Value = System.DBNull.Value;
            cmdParams.Add(parMaterialityThreshold);

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
        /// in FSCaptionMaterialityInfo object
        /// </summary>
        /// <param name="o">A FSCaptionMaterialityInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(FSCaptionMaterialityInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_FSCaptionMateriality");
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

            System.Data.IDbDataParameter parEndReconciliationPeriodID = cmd.CreateParameter();
            parEndReconciliationPeriodID.ParameterName = "@EndReconciliationPeriodID";
            if (!entity.IsEndReconciliationPeriodIDNull)
                parEndReconciliationPeriodID.Value = entity.EndReconciliationPeriodID;
            else
                parEndReconciliationPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parEndReconciliationPeriodID);

            System.Data.IDbDataParameter parFSCaptionID = cmd.CreateParameter();
            parFSCaptionID.ParameterName = "@FSCaptionID";
            if (!entity.IsFSCaptionIDNull)
                parFSCaptionID.Value = entity.FSCaptionID;
            else
                parFSCaptionID.Value = System.DBNull.Value;
            cmdParams.Add(parFSCaptionID);

            System.Data.IDbDataParameter parMaterialityThreshold = cmd.CreateParameter();
            parMaterialityThreshold.ParameterName = "@MaterialityThreshold";
            if (!entity.IsMaterialityThresholdNull)
                parMaterialityThreshold.Value = entity.MaterialityThreshold;
            else
                parMaterialityThreshold.Value = System.DBNull.Value;
            cmdParams.Add(parMaterialityThreshold);

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

            System.Data.IDbDataParameter pkparFSCaptionMaterialityID = cmd.CreateParameter();
            pkparFSCaptionMaterialityID.ParameterName = "@FSCaptionMaterialityID";
            pkparFSCaptionMaterialityID.Value = entity.FSCaptionMaterialityID;
            cmdParams.Add(pkparFSCaptionMaterialityID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_FSCaptionMateriality");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@FSCaptionMaterialityID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_FSCaptionMateriality");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@FSCaptionMaterialityID";
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
        public IList<FSCaptionMaterialityInfo> SelectAllByFSCaptionID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_FSCaptionMaterialityByFSCaptionID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@FSCaptionID";
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
        public IList<FSCaptionMaterialityInfo> SelectAllByStartReconciliationPeriodID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_FSCaptionMaterialityByStartReconciliationPeriodID");
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
        public IList<FSCaptionMaterialityInfo> SelectAllByEndReconciliationPeriodID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_FSCaptionMaterialityByEndReconciliationPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@EndReconciliationPeriodID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(FSCaptionMaterialityInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(FSCaptionMaterialityDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(FSCaptionMaterialityInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(FSCaptionMaterialityDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(FSCaptionMaterialityInfo entity, object id)
        {
            entity.FSCaptionMaterialityID = Convert.ToInt32(id);
        }




    }
}
