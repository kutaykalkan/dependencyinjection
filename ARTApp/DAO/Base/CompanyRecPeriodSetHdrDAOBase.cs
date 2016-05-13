

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

    public abstract class CompanyRecPeriodSetHdrDAOBase : CustomAbstractDAO<CompanyRecPeriodSetHdrInfo>
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
        /// A static representation of column CompanyRecPeriodSetID
        /// </summary>
        public static readonly string COLUMN_COMPANYRECPERIODSETID = "CompanyRecPeriodSetID";
        /// <summary>
        /// A static representation of column CompanyRecPeriodSetName
        /// </summary>
        public static readonly string COLUMN_COMPANYRECPERIODSETNAME = "CompanyRecPeriodSetName";
        /// <summary>
        /// A static representation of column CompanyRecPeriodSetTypeID
        /// </summary>
        public static readonly string COLUMN_COMPANYRECPERIODSETTYPEID = "CompanyRecPeriodSetTypeID";
        /// <summary>
        /// A static representation of column DateAdded
        /// </summary>
        public static readonly string COLUMN_DATEADDED = "DateAdded";
        /// <summary>
        /// A static representation of column DateRevised
        /// </summary>
        public static readonly string COLUMN_DATEREVISED = "DateRevised";
        /// <summary>
        /// A static representation of column EndRecPeriodID
        /// </summary>
        public static readonly string COLUMN_ENDRECPERIODID = "EndRecPeriodID";
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
        /// A static representation of column StartRecPeriodID
        /// </summary>
        public static readonly string COLUMN_STARTRECPERIODID = "StartRecPeriodID";
        /// <summary>
        /// Provides access to the name of the primary key column (CompanyRecPeriodSetID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "CompanyRecPeriodSetID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "CompanyRecPeriodSetHdr";

        /// <summary>
        /// Provides access to the name of the database
        /// </summary>
        public static readonly string DATABASE_NAME = "SkyStemART";

        /// <summary>
        ///  CurrentAppUserInfo  for further use
        /// </summary>
        public AppUserInfo CurrentAppUserInfo { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public CompanyRecPeriodSetHdrDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "CompanyRecPeriodSetHdr", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a CompanyRecPeriodSetHdrInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>CompanyRecPeriodSetHdrInfo</returns>
        protected override CompanyRecPeriodSetHdrInfo MapObject(System.Data.IDataReader r)
        {

            CompanyRecPeriodSetHdrInfo entity = new CompanyRecPeriodSetHdrInfo();


            try
            {
                int ordinal = r.GetOrdinal("CompanyRecPeriodSetID");
                if (!r.IsDBNull(ordinal)) entity.CompanyRecPeriodSetID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("CompanyRecPeriodSetName");
                if (!r.IsDBNull(ordinal)) entity.CompanyRecPeriodSetName = ((System.String)(r.GetValue(ordinal)));
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
                int ordinal = r.GetOrdinal("StartRecPeriodID");
                if (!r.IsDBNull(ordinal)) entity.StartRecPeriodID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("EndRecPeriodID");
                if (!r.IsDBNull(ordinal)) entity.EndRecPeriodID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("CompanyRecPeriodSetTypeID");
                if (!r.IsDBNull(ordinal)) entity.CompanyRecPeriodSetTypeID = ((System.Int16)(r.GetValue(ordinal)));
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
        /// in CompanyRecPeriodSetHdrInfo object
        /// </summary>
        /// <param name="o">A CompanyRecPeriodSetHdrInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(CompanyRecPeriodSetHdrInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_CompanyRecPeriodSetHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (entity != null)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (entity != null)
                parCompanyID.Value = entity.CompanyID;
            else
                parCompanyID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parCompanyRecPeriodSetName = cmd.CreateParameter();
            parCompanyRecPeriodSetName.ParameterName = "@CompanyRecPeriodSetName";
            if (entity != null)
                parCompanyRecPeriodSetName.Value = entity.CompanyRecPeriodSetName;
            else
                parCompanyRecPeriodSetName.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyRecPeriodSetName);

            System.Data.IDbDataParameter parCompanyRecPeriodSetTypeID = cmd.CreateParameter();
            parCompanyRecPeriodSetTypeID.ParameterName = "@CompanyRecPeriodSetTypeID";
            if (entity != null)
                parCompanyRecPeriodSetTypeID.Value = entity.CompanyRecPeriodSetTypeID;
            else
                parCompanyRecPeriodSetTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyRecPeriodSetTypeID);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (entity != null)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (entity != null)
                parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parEndRecPeriodID = cmd.CreateParameter();
            parEndRecPeriodID.ParameterName = "@EndRecPeriodID";
            if (entity != null)
                parEndRecPeriodID.Value = entity.EndRecPeriodID;
            else
                parEndRecPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parEndRecPeriodID);

            System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            parHostName.ParameterName = "@HostName";
            if (entity != null)
                parHostName.Value = entity.HostName;
            else
                parHostName.Value = System.DBNull.Value;
            cmdParams.Add(parHostName);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (entity != null)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (entity != null)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parStartRecPeriodID = cmd.CreateParameter();
            parStartRecPeriodID.ParameterName = "@StartRecPeriodID";
            if (entity != null)
                parStartRecPeriodID.Value = entity.StartRecPeriodID;
            else
                parStartRecPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parStartRecPeriodID);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in CompanyRecPeriodSetHdrInfo object
        /// </summary>
        /// <param name="o">A CompanyRecPeriodSetHdrInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(CompanyRecPeriodSetHdrInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_CompanyRecPeriodSetHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (entity != null)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (entity != null)
                parCompanyID.Value = entity.CompanyID;
            else
                parCompanyID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parCompanyRecPeriodSetName = cmd.CreateParameter();
            parCompanyRecPeriodSetName.ParameterName = "@CompanyRecPeriodSetName";
            if (entity != null)
                parCompanyRecPeriodSetName.Value = entity.CompanyRecPeriodSetName;
            else
                parCompanyRecPeriodSetName.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyRecPeriodSetName);

            System.Data.IDbDataParameter parCompanyRecPeriodSetTypeID = cmd.CreateParameter();
            parCompanyRecPeriodSetTypeID.ParameterName = "@CompanyRecPeriodSetTypeID";
            if (entity != null)
                parCompanyRecPeriodSetTypeID.Value = entity.CompanyRecPeriodSetTypeID;
            else
                parCompanyRecPeriodSetTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyRecPeriodSetTypeID);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (entity != null)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (entity != null)
                parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parEndRecPeriodID = cmd.CreateParameter();
            parEndRecPeriodID.ParameterName = "@EndRecPeriodID";
            if (entity != null)
                parEndRecPeriodID.Value = entity.EndRecPeriodID;
            else
                parEndRecPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parEndRecPeriodID);

            System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            parHostName.ParameterName = "@HostName";
            if (entity != null)
                parHostName.Value = entity.HostName;
            else
                parHostName.Value = System.DBNull.Value;
            cmdParams.Add(parHostName);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (entity != null)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (entity != null)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parStartRecPeriodID = cmd.CreateParameter();
            parStartRecPeriodID.ParameterName = "@StartRecPeriodID";
            if (entity != null)
                parStartRecPeriodID.Value = entity.StartRecPeriodID;
            else
                parStartRecPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parStartRecPeriodID);

            System.Data.IDbDataParameter pkparCompanyRecPeriodSetID = cmd.CreateParameter();
            pkparCompanyRecPeriodSetID.ParameterName = "@CompanyRecPeriodSetID";
            pkparCompanyRecPeriodSetID.Value = entity.CompanyRecPeriodSetID;
            cmdParams.Add(pkparCompanyRecPeriodSetID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_CompanyRecPeriodSetHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyRecPeriodSetID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_CompanyRecPeriodSetHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyRecPeriodSetID";
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
        public IList<CompanyRecPeriodSetHdrInfo> SelectAllByCompanyID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CompanyRecPeriodSetHdrByCompanyID");
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
        public IList<CompanyRecPeriodSetHdrInfo> SelectAllByStartRecPeriodID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CompanyRecPeriodSetHdrByStartRecPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@StartRecPeriodID";
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
        public IList<CompanyRecPeriodSetHdrInfo> SelectAllByEndRecPeriodID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CompanyRecPeriodSetHdrByEndRecPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@EndRecPeriodID";
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
        public IList<CompanyRecPeriodSetHdrInfo> SelectAllByCompanyRecPeriodSetTypeID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CompanyRecPeriodSetHdrByCompanyRecPeriodSetTypeID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyRecPeriodSetTypeID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(CompanyRecPeriodSetHdrInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(CompanyRecPeriodSetHdrDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(CompanyRecPeriodSetHdrInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(CompanyRecPeriodSetHdrDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(CompanyRecPeriodSetHdrInfo entity, object id)
        {
            entity.CompanyRecPeriodSetID = Convert.ToInt32(id);
        }








    }
}
