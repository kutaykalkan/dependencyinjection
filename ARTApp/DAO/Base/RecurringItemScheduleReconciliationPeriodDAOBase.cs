

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

    public abstract class RecurringItemScheduleReconciliationPeriodDAOBase : CustomAbstractDAO<RecurringItemScheduleReconciliationPeriodInfo>
    {

        /// <summary>
        /// A static representation of column AccruedAmount
        /// </summary>
        public static readonly string COLUMN_ACCRUEDAMOUNT = "AccruedAmount";
        /// <summary>
        /// A static representation of column AccurableItemScheduleID
        /// </summary>
        public static readonly string COLUMN_ACCURABLEITEMSCHEDULEID = "AccurableItemScheduleID";
        /// <summary>
        /// A static representation of column ActualAmount
        /// </summary>
        public static readonly string COLUMN_ACTUALAMOUNT = "ActualAmount";
        /// <summary>
        /// A static representation of column Date
        /// </summary>
        public static readonly string COLUMN_DATE = "Date";
        /// <summary>
        /// A static representation of column ReconciliationPeriodID
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONPERIODID = "ReconciliationPeriodID";
        /// <summary>
        /// A static representation of column RecurringItemScheduleReconciliationPeriodID
        /// </summary>
        public static readonly string COLUMN_RECURRINGITEMSCHEDULERECONCILIATIONPERIODID = "RecurringItemScheduleReconciliationPeriodID";
        /// <summary>
        /// Provides access to the name of the primary key column (RecurringItemScheduleReconciliationPeriodID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "RecurringItemScheduleReconciliationPeriodID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "RecurringItemScheduleReconciliationPeriod";

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
        public RecurringItemScheduleReconciliationPeriodDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "RecurringItemScheduleReconciliationPeriod", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a RecurringItemScheduleReconciliationPeriodInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>RecurringItemScheduleReconciliationPeriodInfo</returns>
        protected override RecurringItemScheduleReconciliationPeriodInfo MapObject(System.Data.IDataReader r)
        {

            RecurringItemScheduleReconciliationPeriodInfo entity = new RecurringItemScheduleReconciliationPeriodInfo();


            try
            {
                int ordinal = r.GetOrdinal("RecurringItemScheduleReconciliationPeriodID");
                if (!r.IsDBNull(ordinal)) entity.RecurringItemScheduleReconciliationPeriodID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("AccurableItemScheduleID");
                if (!r.IsDBNull(ordinal)) entity.AccurableItemScheduleID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ReconciliationPeriodID");
                if (!r.IsDBNull(ordinal)) entity.ReconciliationPeriodID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("Date");
                if (!r.IsDBNull(ordinal)) entity.Date = ((System.DateTime)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("AccruedAmount");
                if (!r.IsDBNull(ordinal)) entity.AccruedAmount = ((System.Decimal)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ActualAmount");
                if (!r.IsDBNull(ordinal)) entity.ActualAmount = ((System.Decimal)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in RecurringItemScheduleReconciliationPeriodInfo object
        /// </summary>
        /// <param name="o">A RecurringItemScheduleReconciliationPeriodInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(RecurringItemScheduleReconciliationPeriodInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_RecurringItemScheduleReconciliationPeriod");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAccruedAmount = cmd.CreateParameter();
            parAccruedAmount.ParameterName = "@AccruedAmount";
            if (!entity.IsAccruedAmountNull)
                parAccruedAmount.Value = entity.AccruedAmount;
            else
                parAccruedAmount.Value = System.DBNull.Value;
            cmdParams.Add(parAccruedAmount);

            System.Data.IDbDataParameter parAccurableItemScheduleID = cmd.CreateParameter();
            parAccurableItemScheduleID.ParameterName = "@AccurableItemScheduleID";
            if (!entity.IsAccurableItemScheduleIDNull)
                parAccurableItemScheduleID.Value = entity.AccurableItemScheduleID;
            else
                parAccurableItemScheduleID.Value = System.DBNull.Value;
            cmdParams.Add(parAccurableItemScheduleID);

            System.Data.IDbDataParameter parActualAmount = cmd.CreateParameter();
            parActualAmount.ParameterName = "@ActualAmount";
            if (!entity.IsActualAmountNull)
                parActualAmount.Value = entity.ActualAmount;
            else
                parActualAmount.Value = System.DBNull.Value;
            cmdParams.Add(parActualAmount);

            System.Data.IDbDataParameter parDate = cmd.CreateParameter();
            parDate.ParameterName = "@Date";
            if (!entity.IsDateNull)
                parDate.Value = entity.Date.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDate.Value = System.DBNull.Value;
            cmdParams.Add(parDate);

            System.Data.IDbDataParameter parReconciliationPeriodID = cmd.CreateParameter();
            parReconciliationPeriodID.ParameterName = "@ReconciliationPeriodID";
            if (!entity.IsReconciliationPeriodIDNull)
                parReconciliationPeriodID.Value = entity.ReconciliationPeriodID;
            else
                parReconciliationPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationPeriodID);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in RecurringItemScheduleReconciliationPeriodInfo object
        /// </summary>
        /// <param name="o">A RecurringItemScheduleReconciliationPeriodInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(RecurringItemScheduleReconciliationPeriodInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_RecurringItemScheduleReconciliationPeriod");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAccruedAmount = cmd.CreateParameter();
            parAccruedAmount.ParameterName = "@AccruedAmount";
            if (!entity.IsAccruedAmountNull)
                parAccruedAmount.Value = entity.AccruedAmount;
            else
                parAccruedAmount.Value = System.DBNull.Value;
            cmdParams.Add(parAccruedAmount);

            System.Data.IDbDataParameter parAccurableItemScheduleID = cmd.CreateParameter();
            parAccurableItemScheduleID.ParameterName = "@AccurableItemScheduleID";
            if (!entity.IsAccurableItemScheduleIDNull)
                parAccurableItemScheduleID.Value = entity.AccurableItemScheduleID;
            else
                parAccurableItemScheduleID.Value = System.DBNull.Value;
            cmdParams.Add(parAccurableItemScheduleID);

            System.Data.IDbDataParameter parActualAmount = cmd.CreateParameter();
            parActualAmount.ParameterName = "@ActualAmount";
            if (!entity.IsActualAmountNull)
                parActualAmount.Value = entity.ActualAmount;
            else
                parActualAmount.Value = System.DBNull.Value;
            cmdParams.Add(parActualAmount);

            System.Data.IDbDataParameter parDate = cmd.CreateParameter();
            parDate.ParameterName = "@Date";
            if (!entity.IsDateNull)
                parDate.Value = entity.Date.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDate.Value = System.DBNull.Value;
            cmdParams.Add(parDate);

            System.Data.IDbDataParameter parReconciliationPeriodID = cmd.CreateParameter();
            parReconciliationPeriodID.ParameterName = "@ReconciliationPeriodID";
            if (!entity.IsReconciliationPeriodIDNull)
                parReconciliationPeriodID.Value = entity.ReconciliationPeriodID;
            else
                parReconciliationPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationPeriodID);

            System.Data.IDbDataParameter pkparRecurringItemScheduleReconciliationPeriodID = cmd.CreateParameter();
            pkparRecurringItemScheduleReconciliationPeriodID.ParameterName = "@RecurringItemScheduleReconciliationPeriodID";
            pkparRecurringItemScheduleReconciliationPeriodID.Value = entity.RecurringItemScheduleReconciliationPeriodID;
            cmdParams.Add(pkparRecurringItemScheduleReconciliationPeriodID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_RecurringItemScheduleReconciliationPeriod");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RecurringItemScheduleReconciliationPeriodID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_RecurringItemScheduleReconciliationPeriod");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RecurringItemScheduleReconciliationPeriodID";
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
        public IList<RecurringItemScheduleReconciliationPeriodInfo> SelectAllByAccurableItemScheduleID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_RecurringItemScheduleReconciliationPeriodByAccurableItemScheduleID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AccurableItemScheduleID";
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
        public IList<RecurringItemScheduleReconciliationPeriodInfo> SelectAllByReconciliationPeriodID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_RecurringItemScheduleReconciliationPeriodByReconciliationPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationPeriodID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(RecurringItemScheduleReconciliationPeriodInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(RecurringItemScheduleReconciliationPeriodDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(RecurringItemScheduleReconciliationPeriodInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(RecurringItemScheduleReconciliationPeriodDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(RecurringItemScheduleReconciliationPeriodInfo entity, object id)
        {
            entity.RecurringItemScheduleReconciliationPeriodID = Convert.ToInt32(id);
        }




    }
}
