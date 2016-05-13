

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

    public abstract class NetAccountAttributeValueDAOBase : CustomAbstractDAO<NetAccountAttributeValueInfo>
    {

        /// <summary>
        /// A static representation of column AccountAttributeID
        /// </summary>
        public static readonly string COLUMN_ACCOUNTATTRIBUTEID = "AccountAttributeID";
        /// <summary>
        /// A static representation of column AddedBy
        /// </summary>
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
        /// <summary>
        /// A static representation of column DataImportID
        /// </summary>
        public static readonly string COLUMN_DATAIMPORTID = "DataImportID";
        /// <summary>
        /// A static representation of column DateAdded
        /// </summary>
        public static readonly string COLUMN_DATEADDED = "DateAdded";
        /// <summary>
        /// A static representation of column EndReconciliationPeriodID
        /// </summary>
        public static readonly string COLUMN_ENDRECONCILIATIONPERIODID = "EndReconciliationPeriodID";
        /// <summary>
        /// A static representation of column HostName
        /// </summary>
        public static readonly string COLUMN_HOSTNAME = "HostName";
        /// <summary>
        /// A static representation of column NetAccountAttributeValueID
        /// </summary>
        public static readonly string COLUMN_NETACCOUNTATTRIBUTEVALUEID = "NetAccountAttributeValueID";
        /// <summary>
        /// A static representation of column NetAccountID
        /// </summary>
        public static readonly string COLUMN_NETACCOUNTID = "NetAccountID";
        /// <summary>
        /// A static representation of column StartReconciliationPeriodID
        /// </summary>
        public static readonly string COLUMN_STARTRECONCILIATIONPERIODID = "StartReconciliationPeriodID";
        /// <summary>
        /// A static representation of column Value
        /// </summary>
        public static readonly string COLUMN_VALUE = "Value";
        /// <summary>
        /// A static representation of column ValueLabelID
        /// </summary>
        public static readonly string COLUMN_VALUELABELID = "ValueLabelID";
        /// <summary>
        /// Provides access to the name of the primary key column (NetAccountAttributeValueID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "NetAccountAttributeValueID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "NetAccountAttributeValue";

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
        public NetAccountAttributeValueDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "NetAccountAttributeValue", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a NetAccountAttributeValueInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>NetAccountAttributeValueInfo</returns>
        protected override NetAccountAttributeValueInfo MapObject(System.Data.IDataReader r)
        {

            NetAccountAttributeValueInfo entity = new NetAccountAttributeValueInfo();


            try
            {
                int ordinal = r.GetOrdinal("NetAccountAttributeValueID");
                if (!r.IsDBNull(ordinal)) entity.NetAccountAttributeValueID = ((System.Int64)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("AccountAttributeID");
                if (!r.IsDBNull(ordinal)) entity.AccountAttributeID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("NetAccountID");
                if (!r.IsDBNull(ordinal)) entity.NetAccountID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("Value");
                if (!r.IsDBNull(ordinal)) entity.Value = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ValueLabelID");
                if (!r.IsDBNull(ordinal)) entity.ValueLabelID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("StartReconciliationPeriodID");
                if (!r.IsDBNull(ordinal)) entity.StartReconciliationPeriodID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("EndReconciliationPeriodID");
                if (!r.IsDBNull(ordinal)) entity.EndReconciliationPeriodID = ((System.Int32)(r.GetValue(ordinal)));
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
        /// in NetAccountAttributeValueInfo object
        /// </summary>
        /// <param name="o">A NetAccountAttributeValueInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(NetAccountAttributeValueInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_NetAccountAttributeValue");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAccountAttributeID = cmd.CreateParameter();
            parAccountAttributeID.ParameterName = "@AccountAttributeID";
            if (!entity.IsAccountAttributeIDNull)
                parAccountAttributeID.Value = entity.AccountAttributeID;
            else
                parAccountAttributeID.Value = System.DBNull.Value;
            cmdParams.Add(parAccountAttributeID);

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
                parDateAdded.Value = entity.DateAdded.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parEndReconciliationPeriodID = cmd.CreateParameter();
            parEndReconciliationPeriodID.ParameterName = "@EndReconciliationPeriodID";
            if (!entity.IsEndReconciliationPeriodIDNull)
                parEndReconciliationPeriodID.Value = entity.EndReconciliationPeriodID;
            else
                parEndReconciliationPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parEndReconciliationPeriodID);

            System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            parHostName.ParameterName = "@HostName";
            if (!entity.IsHostNameNull)
                parHostName.Value = entity.HostName;
            else
                parHostName.Value = System.DBNull.Value;
            cmdParams.Add(parHostName);

            System.Data.IDbDataParameter parNetAccountID = cmd.CreateParameter();
            parNetAccountID.ParameterName = "@NetAccountID";
            if (!entity.IsNetAccountIDNull)
                parNetAccountID.Value = entity.NetAccountID;
            else
                parNetAccountID.Value = System.DBNull.Value;
            cmdParams.Add(parNetAccountID);

            System.Data.IDbDataParameter parStartReconciliationPeriodID = cmd.CreateParameter();
            parStartReconciliationPeriodID.ParameterName = "@StartReconciliationPeriodID";
            if (!entity.IsStartReconciliationPeriodIDNull)
                parStartReconciliationPeriodID.Value = entity.StartReconciliationPeriodID;
            else
                parStartReconciliationPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parStartReconciliationPeriodID);

            System.Data.IDbDataParameter parValue = cmd.CreateParameter();
            parValue.ParameterName = "@Value";
            if (!entity.IsValueNull)
                parValue.Value = entity.Value;
            else
                parValue.Value = System.DBNull.Value;
            cmdParams.Add(parValue);

            System.Data.IDbDataParameter parValueLabelID = cmd.CreateParameter();
            parValueLabelID.ParameterName = "@ValueLabelID";
            if (!entity.IsValueLabelIDNull)
                parValueLabelID.Value = entity.ValueLabelID;
            else
                parValueLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parValueLabelID);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in NetAccountAttributeValueInfo object
        /// </summary>
        /// <param name="o">A NetAccountAttributeValueInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected System.Data.IDbCommand CreateNetAccountAttributeUpdateCommand(NetAccountAttributeValueInfo entity)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("usp_UPD_NetAccountAttributeValue");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parAccountAttributeID = cmd.CreateParameter();
            parAccountAttributeID.ParameterName = "@AccountAttributeID";
            if (!entity.IsAccountAttributeIDNull)
                parAccountAttributeID.Value = entity.AccountAttributeID;
            else
                parAccountAttributeID.Value = System.DBNull.Value;
            cmdParams.Add(parAccountAttributeID);

            System.Data.IDbDataParameter parNetAccountID = cmd.CreateParameter();
            parNetAccountID.ParameterName = "@NetAccountID";
            if (!entity.IsNetAccountIDNull)
                parNetAccountID.Value = entity.NetAccountID;
            else
                parNetAccountID.Value = System.DBNull.Value;
            cmdParams.Add(parNetAccountID);

            System.Data.IDbDataParameter parValue = cmd.CreateParameter();
            parValue.ParameterName = "@Value";
            if (!entity.IsValueNull)
                parValue.Value = entity.Value;
            else
                parValue.Value = System.DBNull.Value;
            cmdParams.Add(parValue);

            System.Data.IDbDataParameter parValueLabelID = cmd.CreateParameter();
            parValueLabelID.ParameterName = "@ValueLabelID";
            if (!entity.IsValueLabelIDNull)
                parValueLabelID.Value = entity.ValueLabelID;
            else
                parValueLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parValueLabelID);

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
                parDateAdded.Value = entity.DateAdded.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parEndReconciliationPeriodID = cmd.CreateParameter();
            parEndReconciliationPeriodID.ParameterName = "@EndReconciliationPeriodID";
            if (!entity.IsEndReconciliationPeriodIDNull)
                parEndReconciliationPeriodID.Value = entity.EndReconciliationPeriodID;
            else
                parEndReconciliationPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parEndReconciliationPeriodID);

            System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            parHostName.ParameterName = "@HostName";
            if (!entity.IsHostNameNull)
                parHostName.Value = entity.HostName;
            else
                parHostName.Value = System.DBNull.Value;
            cmdParams.Add(parHostName);

            System.Data.IDbDataParameter parStartReconciliationPeriodID = cmd.CreateParameter();
            parStartReconciliationPeriodID.ParameterName = "@StartReconciliationPeriodID";
            if (!entity.IsStartReconciliationPeriodIDNull)
                parStartReconciliationPeriodID.Value = entity.StartReconciliationPeriodID;
            else
                parStartReconciliationPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parStartReconciliationPeriodID);



            //System.Data.IDbDataParameter pkparNetAccountAttributeValueID = cmd.CreateParameter();
            //pkparNetAccountAttributeValueID.ParameterName = "@NetAccountAttributeValueID";
            //pkparNetAccountAttributeValueID.Value = entity.NetAccountAttributeValueID;
            //cmdParams.Add(pkparNetAccountAttributeValueID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("usp_DEL_NetAccountAttributeValue");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@NetAccountAttributeValueID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("usp_GET_NetAccountAttributeValue");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@NetAccountAttributeValueID";
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
        public IList<NetAccountAttributeValueInfo> SelectAllByAccountAttributeID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_NetAccountAttributeValueByAccountAttributeID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AccountAttributeID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }


        public IList<NetAccountAttributeValueInfo> SelectAllByNetAccountID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_NetAccountAttributeValueByNetAccountID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@NetAccountID";
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
        public IList<NetAccountAttributeValueInfo> SelectAllByStartReconciliationPeriodID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_NetAccountAttributeValueByStartReconciliationPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@StartReconciliationPeriodID";
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
        public IList<NetAccountAttributeValueInfo> SelectAllByEndReconciliationPeriodID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_NetAccountAttributeValueByEndReconciliationPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@EndReconciliationPeriodID";
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
        public IList<NetAccountAttributeValueInfo> SelectAllByDataImportID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_NetAccountAttributeValueByDataImportID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DataImportID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(NetAccountAttributeValueInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(NetAccountAttributeValueDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(NetAccountAttributeValueInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(NetAccountAttributeValueDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(NetAccountAttributeValueInfo entity, object id)
        {
            entity.NetAccountAttributeValueID = Convert.ToInt64(id);
        }




    }
}
