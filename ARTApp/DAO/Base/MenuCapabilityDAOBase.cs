

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

    public abstract class MenuCapabilityDAOBase : CustomAbstractDAO<MenuCapabilityInfo>
    {

        /// <summary>
        /// A static representation of column CapabilityID
        /// </summary>
        public static readonly string COLUMN_CAPABILITYID = "CapabilityID";
        /// <summary>
        /// A static representation of column MenuCapabilityID
        /// </summary>
        public static readonly string COLUMN_MENUCAPABILITYID = "MenuCapabilityID";
        /// <summary>
        /// A static representation of column MenuID
        /// </summary>
        public static readonly string COLUMN_MENUID = "MenuID";
        /// <summary>
        /// Provides access to the name of the primary key column (MenuCapabilityID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "MenuCapabilityID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "MenuCapability";

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
        public MenuCapabilityDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "MenuCapability", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a MenuCapabilityInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>MenuCapabilityInfo</returns>
        protected override MenuCapabilityInfo MapObject(System.Data.IDataReader r)
        {

            MenuCapabilityInfo entity = new MenuCapabilityInfo();


            try
            {
                int ordinal = r.GetOrdinal("MenuCapabilityID");
                if (!r.IsDBNull(ordinal)) entity.MenuCapabilityID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("MenuID");
                if (!r.IsDBNull(ordinal)) entity.MenuID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("CapabilityID");
                if (!r.IsDBNull(ordinal)) entity.CapabilityID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in MenuCapabilityInfo object
        /// </summary>
        /// <param name="o">A MenuCapabilityInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(MenuCapabilityInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_MenuCapability");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parCapabilityID = cmd.CreateParameter();
            parCapabilityID.ParameterName = "@CapabilityID";
            if (!entity.IsCapabilityIDNull)
                parCapabilityID.Value = entity.CapabilityID;
            else
                parCapabilityID.Value = System.DBNull.Value;
            cmdParams.Add(parCapabilityID);

            System.Data.IDbDataParameter parMenuID = cmd.CreateParameter();
            parMenuID.ParameterName = "@MenuID";
            if (!entity.IsMenuIDNull)
                parMenuID.Value = entity.MenuID;
            else
                parMenuID.Value = System.DBNull.Value;
            cmdParams.Add(parMenuID);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in MenuCapabilityInfo object
        /// </summary>
        /// <param name="o">A MenuCapabilityInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(MenuCapabilityInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_MenuCapability");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parCapabilityID = cmd.CreateParameter();
            parCapabilityID.ParameterName = "@CapabilityID";
            if (!entity.IsCapabilityIDNull)
                parCapabilityID.Value = entity.CapabilityID;
            else
                parCapabilityID.Value = System.DBNull.Value;
            cmdParams.Add(parCapabilityID);

            System.Data.IDbDataParameter parMenuID = cmd.CreateParameter();
            parMenuID.ParameterName = "@MenuID";
            if (!entity.IsMenuIDNull)
                parMenuID.Value = entity.MenuID;
            else
                parMenuID.Value = System.DBNull.Value;
            cmdParams.Add(parMenuID);

            System.Data.IDbDataParameter pkparMenuCapabilityID = cmd.CreateParameter();
            pkparMenuCapabilityID.ParameterName = "@MenuCapabilityID";
            pkparMenuCapabilityID.Value = entity.MenuCapabilityID;
            cmdParams.Add(pkparMenuCapabilityID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_MenuCapability");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MenuCapabilityID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_MenuCapability");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MenuCapabilityID";
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
        public IList<MenuCapabilityInfo> SelectAllByMenuID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_MenuCapabilityByMenuID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MenuID";
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
        public IList<MenuCapabilityInfo> SelectAllByCapabilityID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_MenuCapabilityByCapabilityID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CapabilityID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(MenuCapabilityInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(MenuCapabilityDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(MenuCapabilityInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(MenuCapabilityDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(MenuCapabilityInfo entity, object id)
        {
            entity.MenuCapabilityID = Convert.ToInt32(id);
        }




    }
}
