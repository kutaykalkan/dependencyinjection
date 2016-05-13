

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

    public abstract class PackageFeatureDAOBase : CustomAbstractDAO<PackageFeatureInfo>
    {

        /// <summary>
        /// A static representation of column FeatureID
        /// </summary>
        public static readonly string COLUMN_FEATUREID = "FeatureID";
        /// <summary>
        /// A static representation of column PackageFeatureID
        /// </summary>
        public static readonly string COLUMN_PACKAGEFEATUREID = "PackageFeatureID";
        /// <summary>
        /// A static representation of column PackageID
        /// </summary>
        public static readonly string COLUMN_PACKAGEID = "PackageID";
        /// <summary>
        /// Provides access to the name of the primary key column (PackageFeatureID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "PackageFeatureID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "PackageFeature";

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
        public PackageFeatureDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "PackageFeature", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a PackageFeatureInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>PackageFeatureInfo</returns>
        protected override PackageFeatureInfo MapObject(System.Data.IDataReader r)
        {

            PackageFeatureInfo entity = new PackageFeatureInfo();


            try
            {
                int ordinal = r.GetOrdinal("PackageFeatureID");
                if (!r.IsDBNull(ordinal)) entity.PackageFeatureID = ((System.Int16)(r.GetValue(ordinal)));
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
                int ordinal = r.GetOrdinal("FeatureID");
                if (!r.IsDBNull(ordinal)) entity.FeatureID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in PackageFeatureInfo object
        /// </summary>
        /// <param name="o">A PackageFeatureInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(PackageFeatureInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_PackageFeature");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parFeatureID = cmd.CreateParameter();
            parFeatureID.ParameterName = "@FeatureID";
            if (entity != null)
                parFeatureID.Value = entity.FeatureID;
            else
                parFeatureID.Value = System.DBNull.Value;
            cmdParams.Add(parFeatureID);

            System.Data.IDbDataParameter parPackageID = cmd.CreateParameter();
            parPackageID.ParameterName = "@PackageID";
            if (entity != null)
                parPackageID.Value = entity.PackageID;
            else
                parPackageID.Value = System.DBNull.Value;
            cmdParams.Add(parPackageID);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in PackageFeatureInfo object
        /// </summary>
        /// <param name="o">A PackageFeatureInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(PackageFeatureInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_PackageFeature");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parFeatureID = cmd.CreateParameter();
            parFeatureID.ParameterName = "@FeatureID";
            if (entity != null)
                parFeatureID.Value = entity.FeatureID;
            else
                parFeatureID.Value = System.DBNull.Value;
            cmdParams.Add(parFeatureID);

            System.Data.IDbDataParameter parPackageID = cmd.CreateParameter();
            parPackageID.ParameterName = "@PackageID";
            if (entity != null)
                parPackageID.Value = entity.PackageID;
            else
                parPackageID.Value = System.DBNull.Value;
            cmdParams.Add(parPackageID);

            System.Data.IDbDataParameter pkparPackageFeatureID = cmd.CreateParameter();
            pkparPackageFeatureID.ParameterName = "@PackageFeatureID";
            pkparPackageFeatureID.Value = entity.PackageFeatureID;
            cmdParams.Add(pkparPackageFeatureID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_PackageFeature");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@PackageFeatureID";
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
            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_PackageFeature");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@PackageFeatureID";
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
        public IList<PackageFeatureInfo> SelectAllByPackageID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_PackageFeatureByPackageID");
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
        public IList<PackageFeatureInfo> SelectAllByFeatureID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_PackageFeatureByFeatureID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@FeatureID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(PackageFeatureInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(PackageFeatureDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(PackageFeatureInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(PackageFeatureDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(PackageFeatureInfo entity, object id)
        {
            entity.PackageFeatureID = Convert.ToInt16(id);
        }




    }
}
