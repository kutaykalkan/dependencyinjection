

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

    public abstract class UserGeographyObjectDAOBase : CustomAbstractDAO<UserGeographyObjectInfo>
    {

        /// <summary>
        /// A static representation of column GeographyObjectID
        /// </summary>
        public static readonly string COLUMN_GEOGRAPHYOBJECTID = "GeographyObjectID";
        /// <summary>
        /// A static representation of column UserGeographyObjectID
        /// </summary>
        public static readonly string COLUMN_USERGEOGRAPHYOBJECTID = "UserGeographyObjectID";
        /// <summary>
        /// A static representation of column UserID
        /// </summary>
        public static readonly string COLUMN_USERID = "UserID";
        /// <summary>
        /// Provides access to the name of the primary key column (UserGeographyObjectID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "UserGeographyObjectID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "UserGeographyObject";

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
        public UserGeographyObjectDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "UserGeographyObject", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a UserGeographyObjectInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>UserGeographyObjectInfo</returns>
        protected override UserGeographyObjectInfo MapObject(System.Data.IDataReader r)
        {

            UserGeographyObjectInfo entity = new UserGeographyObjectInfo();


            try
            {
                int ordinal = r.GetOrdinal("UserGeographyObjectID");
                if (!r.IsDBNull(ordinal)) entity.UserGeographyObjectID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("UserID");
                if (!r.IsDBNull(ordinal)) entity.UserID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("GeographyObjectID");
                if (!r.IsDBNull(ordinal)) entity.GeographyObjectID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in UserGeographyObjectInfo object
        /// </summary>
        /// <param name="o">A UserGeographyObjectInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(UserGeographyObjectInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_UserGeographyObject");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parGeographyObjectID = cmd.CreateParameter();
            parGeographyObjectID.ParameterName = "@GeographyObjectID";
            if (!entity.IsGeographyObjectIDNull)
                parGeographyObjectID.Value = entity.GeographyObjectID;
            else
                parGeographyObjectID.Value = System.DBNull.Value;
            cmdParams.Add(parGeographyObjectID);

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            if (!entity.IsUserIDNull)
                parUserID.Value = entity.UserID;
            else
                parUserID.Value = System.DBNull.Value;
            cmdParams.Add(parUserID);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            parIsActive.Value = entity.IsActive;
            cmdParams.Add(parIsActive);


            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in UserGeographyObjectInfo object
        /// </summary>
        /// <param name="o">A UserGeographyObjectInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(UserGeographyObjectInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_UserGeographyObject");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parGeographyObjectID = cmd.CreateParameter();
            parGeographyObjectID.ParameterName = "@GeographyObjectID";
            if (!entity.IsGeographyObjectIDNull)
                parGeographyObjectID.Value = entity.GeographyObjectID;
            else
                parGeographyObjectID.Value = System.DBNull.Value;
            cmdParams.Add(parGeographyObjectID);

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            if (!entity.IsUserIDNull)
                parUserID.Value = entity.UserID;
            else
                parUserID.Value = System.DBNull.Value;
            cmdParams.Add(parUserID);

            System.Data.IDbDataParameter pkparUserGeographyObjectID = cmd.CreateParameter();
            pkparUserGeographyObjectID.ParameterName = "@UserGeographyObjectID";
            pkparUserGeographyObjectID.Value = entity.UserGeographyObjectID;
            cmdParams.Add(pkparUserGeographyObjectID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_UserGeographyObject");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserGeographyObjectID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_UserGeographyObject");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserGeographyObjectID";
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
        public IList<UserGeographyObjectInfo> SelectAllByUserID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_UserGeographyObjectByUserID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserID";
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
        public IList<UserGeographyObjectInfo> SelectAllByGeographyObjectID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_UserGeographyObjectByGeographyObjectID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GeographyObjectID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(UserGeographyObjectInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(UserGeographyObjectDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(UserGeographyObjectInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(UserGeographyObjectDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(UserGeographyObjectInfo entity, object id)
        {
            entity.UserGeographyObjectID = Convert.ToInt32(id);
        }




    }
}
