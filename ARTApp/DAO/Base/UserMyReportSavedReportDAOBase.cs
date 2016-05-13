

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

    public abstract class UserMyReportSavedReportDAOBase : CustomAbstractDAO<UserMyReportSavedReportInfo>
    {

        /// <summary>
        /// A static representation of column DateAdded
        /// </summary>
        public static readonly string COLUMN_DATEADDED = "DateAdded";
        /// <summary>
        /// A static representation of column DateRevised
        /// </summary>
        public static readonly string COLUMN_DATEREVISED = "DateRevised";
        /// <summary>
        /// A static representation of column IsActive
        /// </summary>
        public static readonly string COLUMN_ISACTIVE = "IsActive";
        /// <summary>
        /// A static representation of column UserMyReportID
        /// </summary>
        public static readonly string COLUMN_USERMYREPORTID = "UserMyReportID";
        /// <summary>
        /// A static representation of column UserMyReportSavedReportID
        /// </summary>
        public static readonly string COLUMN_USERMYREPORTSAVEDREPORTID = "UserMyReportSavedReportID";
        /// <summary>
        /// A static representation of column UserMyReportSavedReportName
        /// </summary>
        public static readonly string COLUMN_USERMYREPORTSAVEDREPORTNAME = "UserMyReportSavedReportName";
        /// <summary>
        /// Provides access to the name of the primary key column (UserMyReportSavedReportID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "UserMyReportSavedReportID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "UserMyReportSavedReport";

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
        public UserMyReportSavedReportDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "UserMyReportSavedReport", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a UserMyReportSavedReportInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>UserMyReportSavedReportInfo</returns>
        protected override UserMyReportSavedReportInfo MapObject(System.Data.IDataReader r)
        {

            UserMyReportSavedReportInfo entity = new UserMyReportSavedReportInfo();


            try
            {
                int ordinal = r.GetOrdinal("UserMyReportSavedReportID");
                if (!r.IsDBNull(ordinal)) entity.UserMyReportSavedReportID = ((System.Int64)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("UserMyReportID");
                if (!r.IsDBNull(ordinal)) entity.UserMyReportID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("UserMyReportSavedReportName");
                if (!r.IsDBNull(ordinal)) entity.UserMyReportSavedReportName = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("IsActive");
                if (!r.IsDBNull(ordinal)) entity.IsActive = ((System.Boolean)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("DateAdded");
                if (!r.IsDBNull(ordinal)) entity.DateAdded = ((System.DateTime)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("DateRevised");
                if (!r.IsDBNull(ordinal)) entity.DateRevised = ((System.DateTime)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in UserMyReportSavedReportInfo object
        /// </summary>
        /// <param name="o">A UserMyReportSavedReportInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(UserMyReportSavedReportInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_UserMyReportSavedReport");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (!entity.IsDateAddedNull)
                parDateAdded.Value = entity.DateAdded.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (!entity.IsDateRevisedNull)
                parDateRevised.Value = entity.DateRevised.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (!entity.IsIsActiveNull)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parUserMyReportID = cmd.CreateParameter();
            parUserMyReportID.ParameterName = "@UserMyReportID";
            if (!entity.IsUserMyReportIDNull)
                parUserMyReportID.Value = entity.UserMyReportID;
            else
                parUserMyReportID.Value = System.DBNull.Value;
            cmdParams.Add(parUserMyReportID);

            System.Data.IDbDataParameter parUserMyReportSavedReportName = cmd.CreateParameter();
            parUserMyReportSavedReportName.ParameterName = "@UserMyReportSavedReportName";
            if (!entity.IsUserMyReportSavedReportNameNull)
                parUserMyReportSavedReportName.Value = entity.UserMyReportSavedReportName;
            else
                parUserMyReportSavedReportName.Value = System.DBNull.Value;
            cmdParams.Add(parUserMyReportSavedReportName);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in UserMyReportSavedReportInfo object
        /// </summary>
        /// <param name="o">A UserMyReportSavedReportInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(UserMyReportSavedReportInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_UserMyReportSavedReport");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (!entity.IsDateAddedNull)
                parDateAdded.Value = entity.DateAdded.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (!entity.IsDateRevisedNull)
                parDateRevised.Value = entity.DateRevised.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (!entity.IsIsActiveNull)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parUserMyReportID = cmd.CreateParameter();
            parUserMyReportID.ParameterName = "@UserMyReportID";
            if (!entity.IsUserMyReportIDNull)
                parUserMyReportID.Value = entity.UserMyReportID;
            else
                parUserMyReportID.Value = System.DBNull.Value;
            cmdParams.Add(parUserMyReportID);

            System.Data.IDbDataParameter parUserMyReportSavedReportName = cmd.CreateParameter();
            parUserMyReportSavedReportName.ParameterName = "@UserMyReportSavedReportName";
            if (!entity.IsUserMyReportSavedReportNameNull)
                parUserMyReportSavedReportName.Value = entity.UserMyReportSavedReportName;
            else
                parUserMyReportSavedReportName.Value = System.DBNull.Value;
            cmdParams.Add(parUserMyReportSavedReportName);

            System.Data.IDbDataParameter pkparUserMyReportSavedReportID = cmd.CreateParameter();
            pkparUserMyReportSavedReportID.ParameterName = "@UserMyReportSavedReportID";
            pkparUserMyReportSavedReportID.Value = entity.UserMyReportSavedReportID;
            cmdParams.Add(pkparUserMyReportSavedReportID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_UserMyReportSavedReport");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserMyReportSavedReportID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_UserMyReportSavedReport");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserMyReportSavedReportID";
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
        public IList<UserMyReportSavedReportInfo> SelectAllByUserMyReportID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_UserMyReportSavedReportByUserMyReportID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserMyReportID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(UserMyReportSavedReportInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(UserMyReportSavedReportDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(UserMyReportSavedReportInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(UserMyReportSavedReportDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(UserMyReportSavedReportInfo entity, object id)
        {
            entity.UserMyReportSavedReportID = Convert.ToInt64(id);
        }








        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<UserMyReportSavedReportInfo> SelectUserMyReportSavedReportDetailsAssociatedToReportParameterByUserMyReportSavedReportParameter(ReportParameterInfo entity)
        {
            return this.SelectUserMyReportSavedReportDetailsAssociatedToReportParameterByUserMyReportSavedReportParameter(entity.ReportParameterID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<UserMyReportSavedReportInfo> SelectUserMyReportSavedReportDetailsAssociatedToReportParameterByUserMyReportSavedReportParameter(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [UserMyReportSavedReport] INNER JOIN [UserMyReportSavedReportParameter] ON [UserMyReportSavedReport].[UserMyReportSavedReportID] = [UserMyReportSavedReportParameter].[UserMyReportSavedReportID] INNER JOIN [ReportParameter] ON [UserMyReportSavedReportParameter].[ReportParameterID] = [ReportParameter].[ReportParameterID]  WHERE  [ReportParameter].[ReportParameterID] = @ReportParameterID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReportParameterID";
            par.Value = id;

            cmdParams.Add(par);
            List<UserMyReportSavedReportInfo> objUserMyReportSavedReportEntityColl = new List<UserMyReportSavedReportInfo>(this.Select(cmd));
            return objUserMyReportSavedReportEntityColl;
        }

    }
}
