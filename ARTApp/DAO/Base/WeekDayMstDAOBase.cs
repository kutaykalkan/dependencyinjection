

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

    public abstract class WeekDayMstDAOBase : CustomAbstractDAO<WeekDayMstInfo>
    {

        /// <summary>
        /// A static representation of column AddedBy
        /// </summary>
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
        /// <summary>
        /// A static representation of column DateAdded
        /// </summary>
        public static readonly string COLUMN_DATEADDED = "DateAdded";
        /// <summary>
        /// A static representation of column DateRevised
        /// </summary>
        public static readonly string COLUMN_DATEREVISED = "DateRevised";
        /// <summary>
        /// A static representation of column HostName
        /// </summary>
        public static readonly string COLUMN_HOSTNAME = "HostName";
        /// <summary>
        /// A static representation of column IsActive
        /// </summary>
        public static readonly string COLUMN_ISACTIVE = "IsActive";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// A static representation of column SortOrder
        /// </summary>
        public static readonly string COLUMN_SORTORDER = "SortOrder";
        /// <summary>
        /// A static representation of column WeekDayID
        /// </summary>
        public static readonly string COLUMN_WEEKDAYID = "WeekDayID";
        /// <summary>
        /// A static representation of column WeekDayName
        /// </summary>
        public static readonly string COLUMN_WEEKDAYNAME = "WeekDayName";
        /// <summary>
        /// A static representation of column WeekDayNameLabelID
        /// </summary>
        public static readonly string COLUMN_WEEKDAYNAMELABELID = "WeekDayNameLabelID";
        /// <summary>
        /// A static representation of column WeekDayNumber
        /// </summary>
        public static readonly string COLUMN_WEEKDAYNUMBER = "WeekDayNumber";
        /// <summary>
        /// Provides access to the name of the primary key column (WeekDayID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "WeekDayID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "WeekDayMst";

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
        public WeekDayMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "WeekDayMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a WeekDayMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>WeekDayMstInfo</returns>
        protected override WeekDayMstInfo MapObject(System.Data.IDataReader r)
        {

            WeekDayMstInfo entity = new WeekDayMstInfo();


            try
            {
                int ordinal = r.GetOrdinal("WeekDayID");
                if (!r.IsDBNull(ordinal)) entity.WeekDayID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("WeekDayName");
                if (!r.IsDBNull(ordinal)) entity.WeekDayName = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("WeekDayNameLabelID");
                if (!r.IsDBNull(ordinal)) entity.WeekDayNameLabelID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("WeekDayNumber");
                if (!r.IsDBNull(ordinal)) entity.WeekDayNumber = ((System.Byte)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("SortOrder");
                if (!r.IsDBNull(ordinal)) entity.SortOrder = ((System.Int16)(r.GetValue(ordinal)));
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
                int ordinal = r.GetOrdinal("AddedBy");
                if (!r.IsDBNull(ordinal)) entity.AddedBy = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("DateRevised");
                if (!r.IsDBNull(ordinal)) entity.DateRevised = ((System.DateTime)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("RevisedBy");
                if (!r.IsDBNull(ordinal)) entity.RevisedBy = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("HostName");
                if (!r.IsDBNull(ordinal)) entity.HostName = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in WeekDayMstInfo object
        /// </summary>
        /// <param name="o">A WeekDayMstInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(WeekDayMstInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_WeekDayMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (entity != null)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (entity != null)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (entity != null)
                parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            parHostName.ParameterName = "@HostName";
            if (entity != null)
                parHostName.Value = entity.HostName;
            else
                parHostName.Value = System.DBNull.Value;
            cmdParams.Add(parHostName);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (entity != null)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (entity != null)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parSortOrder = cmd.CreateParameter();
            parSortOrder.ParameterName = "@SortOrder";
            if (entity != null)
                parSortOrder.Value = entity.SortOrder;
            else
                parSortOrder.Value = System.DBNull.Value;
            cmdParams.Add(parSortOrder);

            System.Data.IDbDataParameter parWeekDayName = cmd.CreateParameter();
            parWeekDayName.ParameterName = "@WeekDayName";
            if (entity != null)
                parWeekDayName.Value = entity.WeekDayName;
            else
                parWeekDayName.Value = System.DBNull.Value;
            cmdParams.Add(parWeekDayName);

            System.Data.IDbDataParameter parWeekDayNameLabelID = cmd.CreateParameter();
            parWeekDayNameLabelID.ParameterName = "@WeekDayNameLabelID";
            if (entity != null)
                parWeekDayNameLabelID.Value = entity.WeekDayNameLabelID;
            else
                parWeekDayNameLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parWeekDayNameLabelID);

            System.Data.IDbDataParameter parWeekDayNumber = cmd.CreateParameter();
            parWeekDayNumber.ParameterName = "@WeekDayNumber";
            if (entity != null)
                parWeekDayNumber.Value = entity.WeekDayNumber;
            else
                parWeekDayNumber.Value = System.DBNull.Value;
            cmdParams.Add(parWeekDayNumber);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in WeekDayMstInfo object
        /// </summary>
        /// <param name="o">A WeekDayMstInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(WeekDayMstInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_WeekDayMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (entity != null)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (entity != null)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (entity != null)
                parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            parHostName.ParameterName = "@HostName";
            if (entity != null)
                parHostName.Value = entity.HostName;
            else
                parHostName.Value = System.DBNull.Value;
            cmdParams.Add(parHostName);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (entity != null)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (entity != null)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parSortOrder = cmd.CreateParameter();
            parSortOrder.ParameterName = "@SortOrder";
            if (entity != null)
                parSortOrder.Value = entity.SortOrder;
            else
                parSortOrder.Value = System.DBNull.Value;
            cmdParams.Add(parSortOrder);

            System.Data.IDbDataParameter parWeekDayName = cmd.CreateParameter();
            parWeekDayName.ParameterName = "@WeekDayName";
            if (entity != null)
                parWeekDayName.Value = entity.WeekDayName;
            else
                parWeekDayName.Value = System.DBNull.Value;
            cmdParams.Add(parWeekDayName);

            System.Data.IDbDataParameter parWeekDayNameLabelID = cmd.CreateParameter();
            parWeekDayNameLabelID.ParameterName = "@WeekDayNameLabelID";
            if (entity != null)
                parWeekDayNameLabelID.Value = entity.WeekDayNameLabelID;
            else
                parWeekDayNameLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parWeekDayNameLabelID);

            System.Data.IDbDataParameter parWeekDayNumber = cmd.CreateParameter();
            parWeekDayNumber.ParameterName = "@WeekDayNumber";
            if (entity != null)
                parWeekDayNumber.Value = entity.WeekDayNumber;
            else
                parWeekDayNumber.Value = System.DBNull.Value;
            cmdParams.Add(parWeekDayNumber);

            System.Data.IDbDataParameter pkparWeekDayID = cmd.CreateParameter();
            pkparWeekDayID.ParameterName = "@WeekDayID";
            pkparWeekDayID.Value = entity.WeekDayID;
            cmdParams.Add(pkparWeekDayID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_WeekDayMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@WeekDayID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_WeekDayMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@WeekDayID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }







        protected override void CustomSave(WeekDayMstInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(WeekDayMstDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(WeekDayMstInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(WeekDayMstDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(WeekDayMstInfo entity, object id)
        {
            entity.WeekDayID = Convert.ToInt16(id);
        }






    }
}
