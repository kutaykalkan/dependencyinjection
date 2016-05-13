

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

    public abstract class ReconciliationFrequencyReconciliationperiodDAOBase : CustomAbstractDAO<ReconciliationFrequencyReconciliationperiodInfo>
    {

        /// <summary>
        /// A static representation of column ReconciliationFrequencyID
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONFREQUENCYID = "ReconciliationFrequencyID";
        /// <summary>
        /// A static representation of column ReconciliationFrequencyreconciliatinPeriodID
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONFREQUENCYRECONCILIATINPERIODID = "ReconciliationFrequencyreconciliatinPeriodID";
        /// <summary>
        /// A static representation of column ReconciliationPeriodID
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONPERIODID = "ReconciliationPeriodID";
        /// <summary>
        /// Provides access to the name of the primary key column (ReconciliationFrequencyreconciliatinPeriodID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "ReconciliationFrequencyreconciliatinPeriodID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "ReconciliationFrequencyReconciliationperiod";

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
        public ReconciliationFrequencyReconciliationperiodDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "ReconciliationFrequencyReconciliationperiod", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a ReconciliationFrequencyReconciliationperiodInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>ReconciliationFrequencyReconciliationperiodInfo</returns>
        protected override ReconciliationFrequencyReconciliationperiodInfo MapObject(System.Data.IDataReader r)
        {

            ReconciliationFrequencyReconciliationperiodInfo entity = new ReconciliationFrequencyReconciliationperiodInfo();

            entity.ReconciliationFrequencyreconciliatinPeriodID = r.GetInt32Value("ReconciliationFrequencyreconciliatinPeriodID");
            entity.ReconciliationFrequencyID = r.GetInt32Value("ReconciliationFrequencyID");
            entity.ReconciliationPeriodID = r.GetInt32Value("ReconciliationPeriodID");
            entity.PeriodEndDate = r.GetDateValue("PeriodEndDate");
            entity.PeriodNumber = r.GetInt16Value("PeriodNumber");
            entity.FinancialYear = r.GetStringValue("FinancialYear");
            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in ReconciliationFrequencyReconciliationperiodInfo object
        /// </summary>
        /// <param name="o">A ReconciliationFrequencyReconciliationperiodInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(ReconciliationFrequencyReconciliationperiodInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_ReconciliationFrequencyReconciliationperiod");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parReconciliationFrequencyID = cmd.CreateParameter();
            parReconciliationFrequencyID.ParameterName = "@ReconciliationFrequencyID";
            if (!entity.IsReconciliationFrequencyIDNull)
                parReconciliationFrequencyID.Value = entity.ReconciliationFrequencyID;
            else
                parReconciliationFrequencyID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationFrequencyID);

            System.Data.IDbDataParameter parReconciliationFrequencyreconciliatinPeriodID = cmd.CreateParameter();
            parReconciliationFrequencyreconciliatinPeriodID.ParameterName = "@ReconciliationFrequencyreconciliatinPeriodID";
            if (!entity.IsReconciliationFrequencyreconciliatinPeriodIDNull)
                parReconciliationFrequencyreconciliatinPeriodID.Value = entity.ReconciliationFrequencyreconciliatinPeriodID;
            else
                parReconciliationFrequencyreconciliatinPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationFrequencyreconciliatinPeriodID);

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
        /// in ReconciliationFrequencyReconciliationperiodInfo object
        /// </summary>
        /// <param name="o">A ReconciliationFrequencyReconciliationperiodInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(ReconciliationFrequencyReconciliationperiodInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_ReconciliationFrequencyReconciliationperiod");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parReconciliationFrequencyID = cmd.CreateParameter();
            parReconciliationFrequencyID.ParameterName = "@ReconciliationFrequencyID";
            if (!entity.IsReconciliationFrequencyIDNull)
                parReconciliationFrequencyID.Value = entity.ReconciliationFrequencyID;
            else
                parReconciliationFrequencyID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationFrequencyID);

            System.Data.IDbDataParameter parReconciliationPeriodID = cmd.CreateParameter();
            parReconciliationPeriodID.ParameterName = "@ReconciliationPeriodID";
            if (!entity.IsReconciliationPeriodIDNull)
                parReconciliationPeriodID.Value = entity.ReconciliationPeriodID;
            else
                parReconciliationPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationPeriodID);

            System.Data.IDbDataParameter pkparReconciliationFrequencyreconciliatinPeriodID = cmd.CreateParameter();
            pkparReconciliationFrequencyreconciliatinPeriodID.ParameterName = "@ReconciliationFrequencyreconciliatinPeriodID";
            pkparReconciliationFrequencyreconciliatinPeriodID.Value = entity.ReconciliationFrequencyreconciliatinPeriodID;
            cmdParams.Add(pkparReconciliationFrequencyreconciliatinPeriodID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_ReconciliationFrequencyReconciliationperiod");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationFrequencyreconciliatinPeriodID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_ReconciliationFrequencyReconciliationperiod");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationFrequencyreconciliatinPeriodID";
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
        public IList<ReconciliationFrequencyReconciliationperiodInfo> SelectAllByReconciliationFrequencyID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_ReconciliationFrequencyReconciliationperiodByReconciliationFrequencyID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationFrequencyID";
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
        public IList<ReconciliationFrequencyReconciliationperiodInfo> SelectAllByReconciliationPeriodID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_ReconciliationFrequencyReconciliationperiodByReconciliationPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationPeriodID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }








    }
}
