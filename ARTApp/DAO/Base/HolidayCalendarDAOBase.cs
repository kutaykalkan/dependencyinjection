

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

    public abstract class HolidayCalendarDAOBase : CustomAbstractDAO<HolidayCalendarInfo>
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
        /// A static representation of column GeographyObjectID
        /// </summary>
        public static readonly string COLUMN_GEOGRAPHYOBJECTID = "GeographyObjectID";
        /// <summary>
        /// A static representation of column HolidayCalendarID
        /// </summary>
        public static readonly string COLUMN_HOLIDAYCALENDARID = "HolidayCalendarID";
        /// <summary>
        /// A static representation of column HolidayDate
        /// </summary>
        public static readonly string COLUMN_HOLIDAYDATE = "HolidayDate";
        /// <summary>
        /// A static representation of column HolidayName
        /// </summary>
        public static readonly string COLUMN_HOLIDAYNAME = "HolidayName";
        /// <summary>
        /// A static representation of column HolidayNameLabelID
        /// </summary>
        public static readonly string COLUMN_HOLIDAYNAMELABELID = "HolidayNameLabelID";
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
        /// Provides access to the name of the primary key column (HolidayCalendarID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "HolidayCalendarID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "HolidayCalendar";

        /// <summary>
        /// Provides access to the name of the database
        /// </summary>
        public static readonly string DATABASE_NAME = "SkyStemArt";

        /// <summary>
        /// Provides access to the name of the primary key column (HolidayCalendarID)
        /// </summary>
        public static readonly string COLUMN_COMPANYID = "CompanyID";
        /// <summary>
        ///  CurrentAppUserInfo  for further use
        /// </summary>
        public AppUserInfo CurrentAppUserInfo { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public HolidayCalendarDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "HolidayCalendar", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a HolidayCalendarInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>HolidayCalendarInfo</returns>
        protected override HolidayCalendarInfo MapObject(System.Data.IDataReader r)
        {

            HolidayCalendarInfo entity = new HolidayCalendarInfo();


            try
            {
                int ordinal = r.GetOrdinal("HolidayCalendarID");
                if (!r.IsDBNull(ordinal)) entity.HolidayCalendarID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("HolidayName");
                if (!r.IsDBNull(ordinal)) entity.HolidayName = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("HolidayNameLabelID");
                if (!r.IsDBNull(ordinal)) entity.HolidayNameLabelID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("HolidayDate");
                if (!r.IsDBNull(ordinal)) entity.HolidayDate = ((System.DateTime)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("GeographyObjectID");
                if (!r.IsDBNull(ordinal)) entity.GeographyObjectID = ((System.Int32)(r.GetValue(ordinal)));
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
            //CompanyID
            try
            {
                int ordinal = r.GetOrdinal("CompanyID");
                if (!r.IsDBNull(ordinal)) entity.CompanyID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }
            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in HolidayCalendarInfo object
        /// </summary>
        /// <param name="o">A HolidayCalendarInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(HolidayCalendarInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_HolidayCalendar");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (!entity.IsDateAddedNull)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (!entity.IsDateRevisedNull)
                parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            //companyid
            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (!entity.IsCompanyIDNull)
                parCompanyID.Value = entity.CompanyID;
            else
                parCompanyID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parGeographyObjectID = cmd.CreateParameter();
            parGeographyObjectID.ParameterName = "@GeographyObjectID";
            if (!entity.IsGeographyObjectIDNull)
                parGeographyObjectID.Value = entity.GeographyObjectID;
            else
                parGeographyObjectID.Value = System.DBNull.Value;
            cmdParams.Add(parGeographyObjectID);

            System.Data.IDbDataParameter parHolidayDate = cmd.CreateParameter();
            parHolidayDate.ParameterName = "@HolidayDate";
            if (!entity.IsHolidayDateNull)
                parHolidayDate.Value = entity.HolidayDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parHolidayDate.Value = System.DBNull.Value;
            cmdParams.Add(parHolidayDate);

            System.Data.IDbDataParameter parHolidayName = cmd.CreateParameter();
            parHolidayName.ParameterName = "@HolidayName";
            if (!entity.IsHolidayNameNull)
                parHolidayName.Value = entity.HolidayName;
            else
                parHolidayName.Value = System.DBNull.Value;
            cmdParams.Add(parHolidayName);

            System.Data.IDbDataParameter parHolidayNameLabelID = cmd.CreateParameter();
            parHolidayNameLabelID.ParameterName = "@HolidayNameLabelID";
            if (!entity.IsHolidayNameLabelIDNull)
                parHolidayNameLabelID.Value = entity.HolidayNameLabelID;
            else
                parHolidayNameLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parHolidayNameLabelID);

            System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            parHostName.ParameterName = "@HostName";
            if (!entity.IsHostNameNull)
                parHostName.Value = entity.HostName;
            else
                parHostName.Value = System.DBNull.Value;
            cmdParams.Add(parHostName);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (!entity.IsIsActiveNull)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in HolidayCalendarInfo object
        /// </summary>
        /// <param name="o">A HolidayCalendarInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(HolidayCalendarInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_HolidayCalendar");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (!entity.IsDateAddedNull)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (!entity.IsDateRevisedNull)
                parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            //companyid
            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (!entity.IsCompanyIDNull)
                parCompanyID.Value = entity.CompanyID;
            else
                parCompanyID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parGeographyObjectID = cmd.CreateParameter();
            parGeographyObjectID.ParameterName = "@GeographyObjectID";
            if (!entity.IsGeographyObjectIDNull)
                parGeographyObjectID.Value = entity.GeographyObjectID;
            else
                parGeographyObjectID.Value = System.DBNull.Value;
            cmdParams.Add(parGeographyObjectID);

            System.Data.IDbDataParameter parHolidayDate = cmd.CreateParameter();
            parHolidayDate.ParameterName = "@HolidayDate";
            if (!entity.IsHolidayDateNull)
                parHolidayDate.Value = entity.HolidayDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parHolidayDate.Value = System.DBNull.Value;
            cmdParams.Add(parHolidayDate);

            System.Data.IDbDataParameter parHolidayName = cmd.CreateParameter();
            parHolidayName.ParameterName = "@HolidayName";
            if (!entity.IsHolidayNameNull)
                parHolidayName.Value = entity.HolidayName;
            else
                parHolidayName.Value = System.DBNull.Value;
            cmdParams.Add(parHolidayName);

            System.Data.IDbDataParameter parHolidayNameLabelID = cmd.CreateParameter();
            parHolidayNameLabelID.ParameterName = "@HolidayNameLabelID";
            if (!entity.IsHolidayNameLabelIDNull)
                parHolidayNameLabelID.Value = entity.HolidayNameLabelID;
            else
                parHolidayNameLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parHolidayNameLabelID);

            System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            parHostName.ParameterName = "@HostName";
            if (!entity.IsHostNameNull)
                parHostName.Value = entity.HostName;
            else
                parHostName.Value = System.DBNull.Value;
            cmdParams.Add(parHostName);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (!entity.IsIsActiveNull)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter pkparHolidayCalendarID = cmd.CreateParameter();
            pkparHolidayCalendarID.ParameterName = "@HolidayCalendarID";
            pkparHolidayCalendarID.Value = entity.HolidayCalendarID;
            cmdParams.Add(pkparHolidayCalendarID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_HolidayCalendar");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@HolidayCalendarID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_HolidayCalendar");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@HolidayCalendarID";
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
        public IList<HolidayCalendarInfo> SelectAllByGeographyObjectID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_HolidayCalendarByGeographyObjectID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GeographyObjectID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(HolidayCalendarInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(HolidayCalendarDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(HolidayCalendarInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(HolidayCalendarDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(HolidayCalendarInfo entity, object id)
        {
            entity.HolidayCalendarID = Convert.ToInt32(id);
        }




    }
}
