

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

    public abstract class DataImportFailureMessageDAOBase : CustomAbstractDAO<DataImportFailureMessageInfo>
    {

        /// <summary>
        /// A static representation of column AddedBy
        /// </summary>
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
        /// <summary>
        /// A static representation of column DataImportFailureMessageID
        /// </summary>
        public static readonly string COLUMN_DATAIMPORTFAILUREMESSAGEID = "DataImportFailureMessageID";
        /// <summary>
        /// A static representation of column DataImportID
        /// </summary>
        public static readonly string COLUMN_DATAIMPORTID = "DataImportID";
        /// <summary>
        /// A static representation of column DateAdded
        /// </summary>
        public static readonly string COLUMN_DATEADDED = "DateAdded";
        /// <summary>
        /// A static representation of column FailureMessage
        /// </summary>
        public static readonly string COLUMN_FAILUREMESSAGE = "FailureMessage";
        /// <summary>
        /// A static representation of column HostName
        /// </summary>
        public static readonly string COLUMN_HOSTNAME = "HostName";
        /// <summary>
        /// A static representation of column IsActive
        /// </summary>
        public static readonly string COLUMN_ISACTIVE = "IsActive";
        /// <summary>
        /// Provides access to the name of the primary key column (DataImportFailureMessageID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "DataImportFailureMessageID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "DataImportFailureMessage";

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
        public DataImportFailureMessageDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "DataImportFailureMessage", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a DataImportFailureMessageInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>DataImportFailureMessageInfo</returns>
        protected override DataImportFailureMessageInfo MapObject(System.Data.IDataReader r)
        {

            DataImportFailureMessageInfo entity = new DataImportFailureMessageInfo();


            try
            {
                int ordinal = r.GetOrdinal("DataImportFailureMessageID");
                if (!r.IsDBNull(ordinal)) entity.DataImportFailureMessageID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("DataImportID");
                if (!r.IsDBNull(ordinal)) entity.DataImportID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("FailureMessage");
                if (!r.IsDBNull(ordinal)) entity.FailureMessage = ((System.String)(r.GetValue(ordinal)));
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
                int ordinal = r.GetOrdinal("HostName");
                if (!r.IsDBNull(ordinal)) entity.HostName = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in DataImportFailureMessageInfo object
        /// </summary>
        /// <param name="o">A DataImportFailureMessageInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(DataImportFailureMessageInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_DataImportFailureMessage");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDataImportID = cmd.CreateParameter();
            parDataImportID.ParameterName = "@DataImportID";
            if (!entity.IsDataImportIDNull)
                parDataImportID.Value = entity.DataImportID;
            else
                parDataImportID.Value = System.DBNull.Value;
            cmdParams.Add(parDataImportID);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (!entity.IsDateAddedNull)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parFailureMessage = cmd.CreateParameter();
            parFailureMessage.ParameterName = "@FailureMessage";
            if (!entity.IsFailureMessageNull)
                parFailureMessage.Value = entity.FailureMessage;
            else
                parFailureMessage.Value = System.DBNull.Value;
            cmdParams.Add(parFailureMessage);

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

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in DataImportFailureMessageInfo object
        /// </summary>
        /// <param name="o">A DataImportFailureMessageInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(DataImportFailureMessageInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_DataImportFailureMessage");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDataImportID = cmd.CreateParameter();
            parDataImportID.ParameterName = "@DataImportID";
            if (!entity.IsDataImportIDNull)
                parDataImportID.Value = entity.DataImportID;
            else
                parDataImportID.Value = System.DBNull.Value;
            cmdParams.Add(parDataImportID);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (!entity.IsDateAddedNull)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parFailureMessage = cmd.CreateParameter();
            parFailureMessage.ParameterName = "@FailureMessage";
            if (!entity.IsFailureMessageNull)
                parFailureMessage.Value = entity.FailureMessage;
            else
                parFailureMessage.Value = System.DBNull.Value;
            cmdParams.Add(parFailureMessage);

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

            System.Data.IDbDataParameter pkparDataImportFailureMessageID = cmd.CreateParameter();
            pkparDataImportFailureMessageID.ParameterName = "@DataImportFailureMessageID";
            pkparDataImportFailureMessageID.Value = entity.DataImportFailureMessageID;
            cmdParams.Add(pkparDataImportFailureMessageID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_DataImportFailureMessage");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DataImportFailureMessageID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_DataImportFailureMessage");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DataImportFailureMessageID";
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
        public IList<DataImportFailureMessageInfo> SelectAllByDataImportID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_DataImportFailureMessageByDataImportID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DataImportID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(DataImportFailureMessageInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(DataImportFailureMessageDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(DataImportFailureMessageInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(DataImportFailureMessageDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(DataImportFailureMessageInfo entity, object id)
        {
            entity.DataImportFailureMessageID = Convert.ToInt32(id);
        }




    }
}
