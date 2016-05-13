

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.Utility;

namespace SkyStem.ART.App.DAO.Base
{

    public abstract class GLDataRecurringItemScheduleIntervalDetailDAOBase : CustomAbstractDAO<GLDataRecurringItemScheduleIntervalDetailInfo>
    {

        /// <summary>
        /// A static representation of column GLDataRecurringItemScheduleID
        /// </summary>
        public static readonly string COLUMN_GLDATARECURRINGITEMSCHEDULEID = "GLDataRecurringItemScheduleID";
        /// <summary>
        /// A static representation of column GLDataRecurringItemScheduleIntervalDetailID
        /// </summary>
        public static readonly string COLUMN_GLDATARECURRINGITEMSCHEDULEINTERVALDETAILID = "GLDataRecurringItemScheduleIntervalDetailID";
        /// <summary>
        /// A static representation of column IntervalAmount
        /// </summary>
        public static readonly string COLUMN_INTERVALAMOUNT = "IntervalAmount";
        /// <summary>
        /// A static representation of column ReconciliationPeriodID
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONPERIODID = "ReconciliationPeriodID";
        /// <summary>
        /// Provides access to the name of the primary key column (GLDataRecurringItemScheduleIntervalDetailID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "GLDataRecurringItemScheduleIntervalDetailID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "GLDataRecurringItemScheduleIntervalDetail";

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
        public GLDataRecurringItemScheduleIntervalDetailDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "GLDataRecurringItemScheduleIntervalDetail", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a GLDataRecurringItemScheduleIntervalDetailInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>GLDataRecurringItemScheduleIntervalDetailInfo</returns>
        protected override GLDataRecurringItemScheduleIntervalDetailInfo MapObject(System.Data.IDataReader r)
        {

            GLDataRecurringItemScheduleIntervalDetailInfo entity = new GLDataRecurringItemScheduleIntervalDetailInfo();

            entity.GLDataRecurringItemScheduleIntervalDetailID = r.GetInt64Value("GLDataRecurringItemScheduleIntervalDetailID");
            entity.GLDataRecurringItemScheduleID = r.GetInt64Value("GLDataRecurringItemScheduleID");
            entity.ReconciliationPeriodID = r.GetInt32Value("ReconciliationPeriodID");
            entity.IntervalAmount = r.GetDecimalValue("IntervalAmount");
            entity.SystemIntervalAmount = r.GetDecimalValue("SystemIntervalAmount");

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in GLDataRecurringItemScheduleIntervalDetailInfo object
        /// </summary>
        /// <param name="o">A GLDataRecurringItemScheduleIntervalDetailInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(GLDataRecurringItemScheduleIntervalDetailInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_GLDataRecurringItemScheduleIntervalDetail");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parGLDataRecurringItemScheduleID = cmd.CreateParameter();
            parGLDataRecurringItemScheduleID.ParameterName = "@GLDataRecurringItemScheduleID";
            if (entity != null)
                parGLDataRecurringItemScheduleID.Value = entity.GLDataRecurringItemScheduleID;
            else
                parGLDataRecurringItemScheduleID.Value = System.DBNull.Value;
            cmdParams.Add(parGLDataRecurringItemScheduleID);

            System.Data.IDbDataParameter parIntervalAmount = cmd.CreateParameter();
            parIntervalAmount.ParameterName = "@IntervalAmount";
            if (entity != null)
                parIntervalAmount.Value = entity.IntervalAmount;
            else
                parIntervalAmount.Value = System.DBNull.Value;
            cmdParams.Add(parIntervalAmount);

            System.Data.IDbDataParameter parReconciliationPeriodID = cmd.CreateParameter();
            parReconciliationPeriodID.ParameterName = "@ReconciliationPeriodID";
            if (entity != null)
                parReconciliationPeriodID.Value = entity.ReconciliationPeriodID;
            else
                parReconciliationPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationPeriodID);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in GLDataRecurringItemScheduleIntervalDetailInfo object
        /// </summary>
        /// <param name="o">A GLDataRecurringItemScheduleIntervalDetailInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(GLDataRecurringItemScheduleIntervalDetailInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_GLDataRecurringItemScheduleIntervalDetail");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parGLDataRecurringItemScheduleID = cmd.CreateParameter();
            parGLDataRecurringItemScheduleID.ParameterName = "@GLDataRecurringItemScheduleID";
            if (entity != null)
                parGLDataRecurringItemScheduleID.Value = entity.GLDataRecurringItemScheduleID;
            else
                parGLDataRecurringItemScheduleID.Value = System.DBNull.Value;
            cmdParams.Add(parGLDataRecurringItemScheduleID);

            System.Data.IDbDataParameter parIntervalAmount = cmd.CreateParameter();
            parIntervalAmount.ParameterName = "@IntervalAmount";
            if (entity != null)
                parIntervalAmount.Value = entity.IntervalAmount;
            else
                parIntervalAmount.Value = System.DBNull.Value;
            cmdParams.Add(parIntervalAmount);

            System.Data.IDbDataParameter parReconciliationPeriodID = cmd.CreateParameter();
            parReconciliationPeriodID.ParameterName = "@ReconciliationPeriodID";
            if (entity != null)
                parReconciliationPeriodID.Value = entity.ReconciliationPeriodID;
            else
                parReconciliationPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationPeriodID);

            System.Data.IDbDataParameter pkparGLDataRecurringItemScheduleIntervalDetailID = cmd.CreateParameter();
            pkparGLDataRecurringItemScheduleIntervalDetailID.ParameterName = "@GLDataRecurringItemScheduleIntervalDetailID";
            pkparGLDataRecurringItemScheduleIntervalDetailID.Value = entity.GLDataRecurringItemScheduleIntervalDetailID;
            cmdParams.Add(pkparGLDataRecurringItemScheduleIntervalDetailID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_GLDataRecurringItemScheduleIntervalDetail");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataRecurringItemScheduleIntervalDetailID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_GLDataRecurringItemScheduleIntervalDetail");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataRecurringItemScheduleIntervalDetailID";
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
        public IList<GLDataRecurringItemScheduleIntervalDetailInfo> SelectAllByGLDataRecurringItemScheduleID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_GLDataRecurringItemScheduleIntervalDetailByGLDataRecurringItemScheduleID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataRecurringItemScheduleID";
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
        public IList<GLDataRecurringItemScheduleIntervalDetailInfo> SelectAllByReconciliationPeriodID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_GLDataRecurringItemScheduleIntervalDetailByReconciliationPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationPeriodID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(GLDataRecurringItemScheduleIntervalDetailInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(GLDataRecurringItemScheduleIntervalDetailDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(GLDataRecurringItemScheduleIntervalDetailInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(GLDataRecurringItemScheduleIntervalDetailDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(GLDataRecurringItemScheduleIntervalDetailInfo entity, object id)
        {
            entity.GLDataRecurringItemScheduleIntervalDetailID = Convert.ToInt64(id);
        }




    }
}
