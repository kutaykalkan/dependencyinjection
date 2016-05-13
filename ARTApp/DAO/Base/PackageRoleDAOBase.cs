

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

    public abstract class PackageRoleDAOBase : CustomAbstractDAO<PackageRoleInfo>
    {

        /// <summary>
        /// A static representation of column PackageID
        /// </summary>
        public static readonly string COLUMN_PACKAGEID = "PackageID";
        /// <summary>
        /// A static representation of column PackageRoleID
        /// </summary>
        public static readonly string COLUMN_PACKAGEROLEID = "PackageRoleID";
        /// <summary>
        /// A static representation of column RoleID
        /// </summary>
        public static readonly string COLUMN_ROLEID = "RoleID";
        /// <summary>
        /// Provides access to the name of the primary key column (PackageRoleID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "PackageRoleID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "PackageRole";

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
        public PackageRoleDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "PackageRole", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a PackageRoleInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>PackageRoleInfo</returns>
        protected override PackageRoleInfo MapObject(System.Data.IDataReader r)
        {

            PackageRoleInfo entity = new PackageRoleInfo();


            try
            {
                int ordinal = r.GetOrdinal("PackageRoleID");
                if (!r.IsDBNull(ordinal)) entity.PackageRoleID = ((System.Int16)(r.GetValue(ordinal)));
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
                int ordinal = r.GetOrdinal("RoleID");
                if (!r.IsDBNull(ordinal)) entity.RoleID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in PackageRoleInfo object
        /// </summary>
        /// <param name="o">A PackageRoleInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(PackageRoleInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_PackageRole");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parPackageID = cmd.CreateParameter();
            parPackageID.ParameterName = "@PackageID";
            if (entity != null)
                parPackageID.Value = entity.PackageID;
            else
                parPackageID.Value = System.DBNull.Value;
            cmdParams.Add(parPackageID);

            System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            if (entity != null)
                parRoleID.Value = entity.RoleID;
            else
                parRoleID.Value = System.DBNull.Value;
            cmdParams.Add(parRoleID);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in PackageRoleInfo object
        /// </summary>
        /// <param name="o">A PackageRoleInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(PackageRoleInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_PackageRole");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parPackageID = cmd.CreateParameter();
            parPackageID.ParameterName = "@PackageID";
            if (entity != null)
                parPackageID.Value = entity.PackageID;
            else
                parPackageID.Value = System.DBNull.Value;
            cmdParams.Add(parPackageID);

            System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            if (entity != null)
                parRoleID.Value = entity.RoleID;
            else
                parRoleID.Value = System.DBNull.Value;
            cmdParams.Add(parRoleID);

            System.Data.IDbDataParameter pkparPackageRoleID = cmd.CreateParameter();
            pkparPackageRoleID.ParameterName = "@PackageRoleID";
            pkparPackageRoleID.Value = entity.PackageRoleID;
            cmdParams.Add(pkparPackageRoleID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_PackageRole");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@PackageRoleID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_PackageRole");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@PackageRoleID";
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
        public IList<PackageRoleInfo> SelectAllByPackageID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_PackageRoleByPackageID");
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
        public IList<PackageRoleInfo> SelectAllByRoleID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_PackageRoleByRoleID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(PackageRoleInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(PackageRoleDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(PackageRoleInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(PackageRoleDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(PackageRoleInfo entity, object id)
        {
            entity.PackageRoleID = Convert.ToInt16(id);
        }




    }
}
