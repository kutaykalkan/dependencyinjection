

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

    public abstract class RoleAnalyticsModuleDAOBase : CustomAbstractDAO<RoleAnalyticsModuleInfo>
    {

        /// <summary>
        /// A static representation of column AnalyticsModuleID
        /// </summary>
        public static readonly string COLUMN_ANALYTICSMODULEID = "AnalyticsModuleID";
        /// <summary>
        /// A static representation of column RoleAnalyticsModuleID
        /// </summary>
        public static readonly string COLUMN_ROLEANALYTICSMODULEID = "RoleAnalyticsModuleID";
        /// <summary>
        /// A static representation of column RoleID
        /// </summary>
        public static readonly string COLUMN_ROLEID = "RoleID";
        /// <summary>
        /// Provides access to the name of the primary key column (RoleAnalyticsModuleID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "RoleAnalyticsModuleID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "RoleAnalyticsModule";

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
        public RoleAnalyticsModuleDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "RoleAnalyticsModule", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a RoleAnalyticsModuleInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>RoleAnalyticsModuleInfo</returns>
        protected override RoleAnalyticsModuleInfo MapObject(System.Data.IDataReader r)
        {

            RoleAnalyticsModuleInfo entity = new RoleAnalyticsModuleInfo();


            try
            {
                int ordinal = r.GetOrdinal("RoleAnalyticsModuleID");
                if (!r.IsDBNull(ordinal)) entity.RoleAnalyticsModuleID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("RoleID");
                if (!r.IsDBNull(ordinal)) entity.RoleID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("AnalyticsModuleID");
                if (!r.IsDBNull(ordinal)) entity.AnalyticsModuleID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in RoleAnalyticsModuleInfo object
        /// </summary>
        /// <param name="o">A RoleAnalyticsModuleInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(RoleAnalyticsModuleInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_RoleAnalyticsModule");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAnalyticsModuleID = cmd.CreateParameter();
            parAnalyticsModuleID.ParameterName = "@AnalyticsModuleID";
            if (entity != null)
                parAnalyticsModuleID.Value = entity.AnalyticsModuleID;
            else
                parAnalyticsModuleID.Value = System.DBNull.Value;
            cmdParams.Add(parAnalyticsModuleID);

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
        /// in RoleAnalyticsModuleInfo object
        /// </summary>
        /// <param name="o">A RoleAnalyticsModuleInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(RoleAnalyticsModuleInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_RoleAnalyticsModule");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAnalyticsModuleID = cmd.CreateParameter();
            parAnalyticsModuleID.ParameterName = "@AnalyticsModuleID";
            if (entity != null)
                parAnalyticsModuleID.Value = entity.AnalyticsModuleID;
            else
                parAnalyticsModuleID.Value = System.DBNull.Value;
            cmdParams.Add(parAnalyticsModuleID);

            System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            if (entity != null)
                parRoleID.Value = entity.RoleID;
            else
                parRoleID.Value = System.DBNull.Value;
            cmdParams.Add(parRoleID);

            System.Data.IDbDataParameter pkparRoleAnalyticsModuleID = cmd.CreateParameter();
            pkparRoleAnalyticsModuleID.ParameterName = "@RoleAnalyticsModuleID";
            pkparRoleAnalyticsModuleID.Value = entity.RoleAnalyticsModuleID;
            cmdParams.Add(pkparRoleAnalyticsModuleID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_RoleAnalyticsModule");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleAnalyticsModuleID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_RoleAnalyticsModule");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleAnalyticsModuleID";
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
        public IList<RoleAnalyticsModuleInfo> SelectAllByRoleID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_RoleAnalyticsModuleByRoleID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleID";
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
        public IList<RoleAnalyticsModuleInfo> SelectAllByAnalyticsModuleID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_RoleAnalyticsModuleByAnalyticsModuleID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AnalyticsModuleID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(RoleAnalyticsModuleInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(RoleAnalyticsModuleDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(RoleAnalyticsModuleInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(RoleAnalyticsModuleDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(RoleAnalyticsModuleInfo entity, object id)
        {
            entity.RoleAnalyticsModuleID = Convert.ToInt16(id);
        }




    }
}
