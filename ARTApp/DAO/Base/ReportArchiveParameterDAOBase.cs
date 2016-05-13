

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

    public abstract class ReportArchiveParameterDAOBase : CustomAbstractDAO<ReportArchiveParameterInfo>
    {

        /// <summary>
        /// A static representation of column ParameterValue
        /// </summary>
        public static readonly string COLUMN_PARAMETERVALUE = "ParameterValue";
        /// <summary>
        /// A static representation of column ReportArchiveID
        /// </summary>
        public static readonly string COLUMN_REPORTARCHIVEID = "ReportArchiveID";
        /// <summary>
        /// A static representation of column ReportArchiveParameterID
        /// </summary>
        public static readonly string COLUMN_REPORTARCHIVEPARAMETERID = "ReportArchiveParameterID";
        /// <summary>
        /// A static representation of column ReportParameterKeyID
        /// </summary>
        public static readonly string COLUMN_REPORTPARAMETERKEYID = "ReportParameterKeyID";
        /// <summary>
        /// Provides access to the name of the primary key column (ReportArchiveParameterID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "ReportArchiveParameterID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "ReportArchiveParameter";

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
        public ReportArchiveParameterDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "ReportArchiveParameter", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a ReportArchiveParameterInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>ReportArchiveParameterInfo</returns>
        protected override ReportArchiveParameterInfo MapObject(System.Data.IDataReader r)
        {

            ReportArchiveParameterInfo entity = new ReportArchiveParameterInfo();


            try
            {
                int ordinal = r.GetOrdinal("ReportArchiveParameterID");
                if (!r.IsDBNull(ordinal)) entity.ReportArchiveParameterID = ((System.Int64?)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ReportArchiveID");
                if (!r.IsDBNull(ordinal)) entity.ReportArchiveID = ((System.Int64?)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ReportParameterKeyID");
                if (!r.IsDBNull(ordinal)) entity.ReportParameterKeyID = ((System.Int16?)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ParameterValue");
                if (!r.IsDBNull(ordinal)) entity.ParameterValue = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }
            //Additional Properties
            try
            {
                int ordinal = r.GetOrdinal("ReportParameterKeyName");
                if (!r.IsDBNull(ordinal)) entity.ReportParameterKeyName = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }
            try
            {
                int ordinal = r.GetOrdinal("ReportParameterDisplayName");
                if (!r.IsDBNull(ordinal)) entity.ReportParameterDisplayName = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }
            try
            {
                int ordinal = r.GetOrdinal("ParameterID");
                if (!r.IsDBNull(ordinal)) entity.ParameterID = ((System.Int16?)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

           return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in ReportArchiveParameterInfo object
        /// </summary>
        /// <param name="o">A ReportArchiveParameterInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(ReportArchiveParameterInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_ReportArchiveParameter");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parParameterValue = cmd.CreateParameter();
            parParameterValue.ParameterName = "@ParameterValue";
            if (!entity.IsParameterValueNull)
                parParameterValue.Value = entity.ParameterValue;
            else
                parParameterValue.Value = System.DBNull.Value;
            cmdParams.Add(parParameterValue);

            System.Data.IDbDataParameter parReportArchiveID = cmd.CreateParameter();
            parReportArchiveID.ParameterName = "@ReportArchiveID";
            if (!entity.IsReportArchiveIDNull)
                parReportArchiveID.Value = entity.ReportArchiveID;
            else
                parReportArchiveID.Value = System.DBNull.Value;
            cmdParams.Add(parReportArchiveID);

            System.Data.IDbDataParameter parReportParameterKeyID = cmd.CreateParameter();
            parReportParameterKeyID.ParameterName = "@ReportParameterKeyID";
            if (!entity.IsReportParameterKeyIDNull)
                parReportParameterKeyID.Value = entity.ReportParameterKeyID;
            else
                parReportParameterKeyID.Value = System.DBNull.Value;
            cmdParams.Add(parReportParameterKeyID);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in ReportArchiveParameterInfo object
        /// </summary>
        /// <param name="o">A ReportArchiveParameterInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(ReportArchiveParameterInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_ReportArchiveParameter");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parParameterValue = cmd.CreateParameter();
            parParameterValue.ParameterName = "@ParameterValue";
            if (!entity.IsParameterValueNull)
                parParameterValue.Value = entity.ParameterValue;
            else
                parParameterValue.Value = System.DBNull.Value;
            cmdParams.Add(parParameterValue);

            System.Data.IDbDataParameter parReportArchiveID = cmd.CreateParameter();
            parReportArchiveID.ParameterName = "@ReportArchiveID";
            if (!entity.IsReportArchiveIDNull)
                parReportArchiveID.Value = entity.ReportArchiveID;
            else
                parReportArchiveID.Value = System.DBNull.Value;
            cmdParams.Add(parReportArchiveID);

            System.Data.IDbDataParameter parReportParameterKeyID = cmd.CreateParameter();
            parReportParameterKeyID.ParameterName = "@ReportParameterKeyID";
            if (!entity.IsReportParameterKeyIDNull)
                parReportParameterKeyID.Value = entity.ReportParameterKeyID;
            else
                parReportParameterKeyID.Value = System.DBNull.Value;
            cmdParams.Add(parReportParameterKeyID);

            System.Data.IDbDataParameter pkparReportArchiveParameterID = cmd.CreateParameter();
            pkparReportArchiveParameterID.ParameterName = "@ReportArchiveParameterID";
            pkparReportArchiveParameterID.Value = entity.ReportArchiveParameterID;
            cmdParams.Add(pkparReportArchiveParameterID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_ReportArchiveParameter");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReportArchiveParameterID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_ReportArchiveParameter");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReportArchiveParameterID";
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
        public IList<ReportArchiveParameterInfo> SelectAllByReportArchiveID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_ReportArchiveParameterByReportArchiveID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReportArchiveID";
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
        public IList<ReportArchiveParameterInfo> SelectAllByReportParameterKeyID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_ReportArchiveParameterByReportParameterKeyID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReportParameterKeyID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(ReportArchiveParameterInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(ReportArchiveParameterDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(ReportArchiveParameterInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(ReportArchiveParameterDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(ReportArchiveParameterInfo entity, object id)
        {
            entity.ReportArchiveParameterID = Convert.ToInt64(id);
        }




    }
}
