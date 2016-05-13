

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

    public abstract class UserGridColumnDAOBase : CustomAbstractDAO<UserGridColumnInfo>
    {

        /// <summary>
        /// A static representation of column GridColumnID
        /// </summary>
        public static readonly string COLUMN_GRIDCOLUMNID = "GridColumnID";
        /// <summary>
        /// A static representation of column UserGridColumnID
        /// </summary>
        public static readonly string COLUMN_USERGRIDCOLUMNID = "UserGridColumnID";
        /// <summary>
        /// A static representation of column UserID
        /// </summary>
        public static readonly string COLUMN_USERID = "UserID";
        /// <summary>
        /// Provides access to the name of the primary key column (UserGridColumnID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "UserGridColumnID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "UserGridColumn";

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
        public UserGridColumnDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "UserGridColumn", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a UserGridColumnInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>UserGridColumnInfo</returns>
        protected override UserGridColumnInfo MapObject(System.Data.IDataReader r)
        {

            UserGridColumnInfo entity = new UserGridColumnInfo();


            try
            {
                int ordinal = r.GetOrdinal("UserGridColumnID");
                if (!r.IsDBNull(ordinal)) entity.UserGridColumnID = ((System.Int32)(r.GetValue(ordinal)));
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
                int ordinal = r.GetOrdinal("GridColumnID");
                if (!r.IsDBNull(ordinal)) entity.GridColumnID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in UserGridColumnInfo object
        /// </summary>
        /// <param name="o">A UserGridColumnInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(UserGridColumnInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_UserGridColumn");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parGridColumnID = cmd.CreateParameter();
            parGridColumnID.ParameterName = "@GridColumnID";
            if (!entity.IsGridColumnIDNull)
                parGridColumnID.Value = entity.GridColumnID;
            else
                parGridColumnID.Value = System.DBNull.Value;
            cmdParams.Add(parGridColumnID);

            System.Data.IDbDataParameter parUserGridColumnID = cmd.CreateParameter();
            parUserGridColumnID.ParameterName = "@UserGridColumnID";
            if (!entity.IsUserGridColumnIDNull)
                parUserGridColumnID.Value = entity.UserGridColumnID;
            else
                parUserGridColumnID.Value = System.DBNull.Value;
            cmdParams.Add(parUserGridColumnID);

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            if (!entity.IsUserIDNull)
                parUserID.Value = entity.UserID;
            else
                parUserID.Value = System.DBNull.Value;
            cmdParams.Add(parUserID);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in UserGridColumnInfo object
        /// </summary>
        /// <param name="o">A UserGridColumnInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(UserGridColumnInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_UserGridColumn");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parGridColumnID = cmd.CreateParameter();
            parGridColumnID.ParameterName = "@GridColumnID";
            if (!entity.IsGridColumnIDNull)
                parGridColumnID.Value = entity.GridColumnID;
            else
                parGridColumnID.Value = System.DBNull.Value;
            cmdParams.Add(parGridColumnID);

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            if (!entity.IsUserIDNull)
                parUserID.Value = entity.UserID;
            else
                parUserID.Value = System.DBNull.Value;
            cmdParams.Add(parUserID);

            System.Data.IDbDataParameter pkparUserGridColumnID = cmd.CreateParameter();
            pkparUserGridColumnID.ParameterName = "@UserGridColumnID";
            pkparUserGridColumnID.Value = entity.UserGridColumnID;
            cmdParams.Add(pkparUserGridColumnID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_UserGridColumn");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserGridColumnID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_UserGridColumn");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserGridColumnID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }








    }
}
