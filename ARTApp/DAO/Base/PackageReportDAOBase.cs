

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

    public abstract class PackageReportDAOBase : CustomAbstractDAO<PackageReportInfo>
    {

        /// <summary>
        /// A static representation of column PackageID
        /// </summary>
        public static readonly string COLUMN_PACKAGEID = "PackageID";
        /// <summary>
        /// A static representation of column PackageReportID
        /// </summary>
        public static readonly string COLUMN_PACKAGEREPORTID = "PackageReportID";
        /// <summary>
        /// A static representation of column ReportID
        /// </summary>
        public static readonly string COLUMN_REPORTID = "ReportID";
        /// <summary>
        /// Provides access to the name of the primary key column (PackageReportID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "PackageReportID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "PackageReport";

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
        public PackageReportDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "PackageReport", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a PackageReportInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>PackageReportInfo</returns>
        protected override PackageReportInfo MapObject(System.Data.IDataReader r)
        {

            PackageReportInfo entity = new PackageReportInfo();


            try
            {
                int ordinal = r.GetOrdinal("PackageReportID");
                if (!r.IsDBNull(ordinal)) entity.PackageReportID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("PackageID");
                if (!r.IsDBNull(ordinal)) entity.PackageID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ReportID");
                if (!r.IsDBNull(ordinal)) entity.ReportID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in PackageReportInfo object
        /// </summary>
        /// <param name="o">A PackageReportInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(PackageReportInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_PackageReport");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parPackageID = cmd.CreateParameter();
            parPackageID.ParameterName = "@PackageID";
            if (entity != null)
                parPackageID.Value = entity.PackageID;
            else
                parPackageID.Value = System.DBNull.Value;
            cmdParams.Add(parPackageID);

            System.Data.IDbDataParameter parReportID = cmd.CreateParameter();
            parReportID.ParameterName = "@ReportID";
            if (entity != null)
                parReportID.Value = entity.ReportID;
            else
                parReportID.Value = System.DBNull.Value;
            cmdParams.Add(parReportID);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in PackageReportInfo object
        /// </summary>
        /// <param name="o">A PackageReportInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(PackageReportInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_PackageReport");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parPackageID = cmd.CreateParameter();
            parPackageID.ParameterName = "@PackageID";
            if (entity != null)
                parPackageID.Value = entity.PackageID;
            else
                parPackageID.Value = System.DBNull.Value;
            cmdParams.Add(parPackageID);

            System.Data.IDbDataParameter parReportID = cmd.CreateParameter();
            parReportID.ParameterName = "@ReportID";
            if (entity != null)
                parReportID.Value = entity.ReportID;
            else
                parReportID.Value = System.DBNull.Value;
            cmdParams.Add(parReportID);

            System.Data.IDbDataParameter pkparPackageReportID = cmd.CreateParameter();
            pkparPackageReportID.ParameterName = "@PackageReportID";
            pkparPackageReportID.Value = entity.PackageReportID;
            cmdParams.Add(pkparPackageReportID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_PackageReport");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@PackageReportID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_PackageReport");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@PackageReportID";
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
        public IList<PackageReportInfo> SelectAllByPackageID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_PackageReportByPackageID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@PackageID";
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
        public IList<PackageReportInfo> SelectAllByReportID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_PackageReportByReportID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReportID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(PackageReportInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(PackageReportDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(PackageReportInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(PackageReportDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(PackageReportInfo entity, object id)
        {
            entity.PackageReportID = Convert.ToInt16(id);
        }




    }
}
