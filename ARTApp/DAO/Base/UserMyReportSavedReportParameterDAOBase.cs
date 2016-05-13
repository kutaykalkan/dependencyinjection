

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

    public abstract class UserMyReportSavedReportParameterDAOBase : CustomAbstractDAO<UserMyReportSavedReportParameterInfo>
    {

        /// <summary>
        /// A static representation of column ParameterValue
        /// </summary>
        public static readonly string COLUMN_PARAMETERVALUE = "ParameterValue";
        /// <summary>
        /// A static representation of column ReportParameterID
        /// </summary>
        public static readonly string COLUMN_REPORTPARAMETERID = "ReportParameterID";
        /// <summary>
        /// A static representation of column UserMyReportSavedReportID
        /// </summary>
        public static readonly string COLUMN_USERMYREPORTSAVEDREPORTID = "UserMyReportSavedReportID";
        /// <summary>
        /// A static representation of column UserMyReportSavedReportParameterID
        /// </summary>
        public static readonly string COLUMN_USERMYREPORTSAVEDREPORTPARAMETERID = "UserMyReportSavedReportParameterID";
        /// <summary>
        /// Provides access to the name of the primary key column (UserMyReportSavedReportParameterID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "UserMyReportSavedReportParameterID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "UserMyReportSavedReportParameter";

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
        public UserMyReportSavedReportParameterDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "UserMyReportSavedReportParameter", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a UserMyReportSavedReportParameterInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>UserMyReportSavedReportParameterInfo</returns>
        protected override UserMyReportSavedReportParameterInfo MapObject(System.Data.IDataReader r)
        {

            UserMyReportSavedReportParameterInfo entity = new UserMyReportSavedReportParameterInfo();


            try
            {
                int ordinal = r.GetOrdinal("UserMyReportSavedReportParameterID");
                if (!r.IsDBNull(ordinal)) entity.UserMyReportSavedReportParameterID = ((System.Int64)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("UserMyReportSavedReportID");
                if (!r.IsDBNull(ordinal)) entity.UserMyReportSavedReportID = ((System.Int64)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ReportParameterID");
                if (!r.IsDBNull(ordinal)) entity.ReportParameterID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ParameterValue");
                if (!r.IsDBNull(ordinal)) entity.ParameterValue = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in UserMyReportSavedReportParameterInfo object
        /// </summary>
        /// <param name="o">A UserMyReportSavedReportParameterInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(UserMyReportSavedReportParameterInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_UserMyReportSavedReportParameter");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parParameterValue = cmd.CreateParameter();
            parParameterValue.ParameterName = "@ParameterValue";
            if (!entity.IsParameterValueNull)
                parParameterValue.Value = entity.ParameterValue;
            else
                parParameterValue.Value = System.DBNull.Value;
            cmdParams.Add(parParameterValue);

            System.Data.IDbDataParameter parReportParameterID = cmd.CreateParameter();
            parReportParameterID.ParameterName = "@ReportParameterID";
            if (!entity.IsReportParameterIDNull)
                parReportParameterID.Value = entity.ReportParameterID;
            else
                parReportParameterID.Value = System.DBNull.Value;
            cmdParams.Add(parReportParameterID);

            System.Data.IDbDataParameter parUserMyReportSavedReportParameterID = cmd.CreateParameter();
            parUserMyReportSavedReportParameterID.ParameterName = "@UserMyReportSavedReportParameterID";
            if (!entity.IsUserMyReportSavedReportParameterIDNull)
                parUserMyReportSavedReportParameterID.Value = entity.UserMyReportSavedReportParameterID;
            else
                parUserMyReportSavedReportParameterID.Value = System.DBNull.Value;
            cmdParams.Add(parUserMyReportSavedReportParameterID);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in UserMyReportSavedReportParameterInfo object
        /// </summary>
        /// <param name="o">A UserMyReportSavedReportParameterInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(UserMyReportSavedReportParameterInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_UserMyReportSavedReportParameter");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parParameterValue = cmd.CreateParameter();
            parParameterValue.ParameterName = "@ParameterValue";
            if (!entity.IsParameterValueNull)
                parParameterValue.Value = entity.ParameterValue;
            else
                parParameterValue.Value = System.DBNull.Value;
            cmdParams.Add(parParameterValue);

            System.Data.IDbDataParameter parReportParameterID = cmd.CreateParameter();
            parReportParameterID.ParameterName = "@ReportParameterID";
            if (!entity.IsReportParameterIDNull)
                parReportParameterID.Value = entity.ReportParameterID;
            else
                parReportParameterID.Value = System.DBNull.Value;
            cmdParams.Add(parReportParameterID);

            System.Data.IDbDataParameter pkparUserMyReportSavedReportParameterID = cmd.CreateParameter();
            pkparUserMyReportSavedReportParameterID.ParameterName = "@UserMyReportSavedReportParameterID";
            pkparUserMyReportSavedReportParameterID.Value = entity.UserMyReportSavedReportParameterID;
            cmdParams.Add(pkparUserMyReportSavedReportParameterID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_UserMyReportSavedReportParameter");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserMyReportSavedReportParameterID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_UserMyReportSavedReportParameter");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserMyReportSavedReportParameterID";
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
        public IList<UserMyReportSavedReportParameterInfo> SelectAllByUserMyReportSavedReportID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_UserMyReportSavedReportParameterByUserMyReportSavedReportID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserMyReportSavedReportID";
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
        public IList<UserMyReportSavedReportParameterInfo> SelectAllByReportParameterID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_UserMyReportSavedReportParameterByReportParameterID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReportParameterID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }








    }
}
