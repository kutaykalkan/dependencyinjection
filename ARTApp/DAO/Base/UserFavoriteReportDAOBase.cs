

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

    public abstract class UserFavoriteReportDAOBase : CustomAbstractDAO<UserFavoriteReportInfo>
    {

        /// <summary>
        /// A static representation of column ReportID
        /// </summary>
        public static readonly string COLUMN_REPORTID = "ReportID";
        /// <summary>
        /// A static representation of column UserFavoriteReportID
        /// </summary>
        public static readonly string COLUMN_USERFAVORITEREPORTID = "UserFavoriteReportID";
        /// <summary>
        /// A static representation of column UserID
        /// </summary>
        public static readonly string COLUMN_USERID = "UserID";
        /// <summary>
        /// Provides access to the name of the primary key column (UserFavoriteReportID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "UserFavoriteReportID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "UserFavoriteReport";

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
        public UserFavoriteReportDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "UserFavoriteReport", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a UserFavoriteReportInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>UserFavoriteReportInfo</returns>
        protected override UserFavoriteReportInfo MapObject(System.Data.IDataReader r)
        {

            UserFavoriteReportInfo entity = new UserFavoriteReportInfo();


            try
            {
                int ordinal = r.GetOrdinal("UserFavoriteReportID");
                if (!r.IsDBNull(ordinal)) entity.UserFavoriteReportID = ((System.Int32)(r.GetValue(ordinal)));
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
                int ordinal = r.GetOrdinal("ReportID");
                if (!r.IsDBNull(ordinal)) entity.ReportID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in UserFavoriteReportInfo object
        /// </summary>
        /// <param name="o">A UserFavoriteReportInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(UserFavoriteReportInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_UserFavoriteReport");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parReportID = cmd.CreateParameter();
            parReportID.ParameterName = "@ReportID";
            if (!entity.IsReportIDNull)
                parReportID.Value = entity.ReportID;
            else
                parReportID.Value = System.DBNull.Value;
            cmdParams.Add(parReportID);

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
        /// in UserFavoriteReportInfo object
        /// </summary>
        /// <param name="o">A UserFavoriteReportInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(UserFavoriteReportInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_UserFavoriteReport");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parReportID = cmd.CreateParameter();
            parReportID.ParameterName = "@ReportID";
            if (!entity.IsReportIDNull)
                parReportID.Value = entity.ReportID;
            else
                parReportID.Value = System.DBNull.Value;
            cmdParams.Add(parReportID);

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            if (!entity.IsUserIDNull)
                parUserID.Value = entity.UserID;
            else
                parUserID.Value = System.DBNull.Value;
            cmdParams.Add(parUserID);

            System.Data.IDbDataParameter pkparUserFavoriteReportID = cmd.CreateParameter();
            pkparUserFavoriteReportID.ParameterName = "@UserFavoriteReportID";
            pkparUserFavoriteReportID.Value = entity.UserFavoriteReportID;
            cmdParams.Add(pkparUserFavoriteReportID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_UserFavoriteReport");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserFavoriteReportID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_UserFavoriteReport");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserFavoriteReportID";
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
        public IList<UserFavoriteReportInfo> SelectAllByUserID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_UserFavoriteReportByUserID");
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
        public IList<UserFavoriteReportInfo> SelectAllByReportID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_UserFavoriteReportByReportID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReportID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(UserFavoriteReportInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(UserFavoriteReportDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(UserFavoriteReportInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(UserFavoriteReportDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(UserFavoriteReportInfo entity, object id)
        {
            entity.UserFavoriteReportID = Convert.ToInt32(id);
        }




    }
}
