

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

    public abstract class AccountReconciliationPeriodDAOBase : CustomAbstractDAO<AccountReconciliationPeriodInfo>
    {

        /// <summary>
        /// A static representation of column AccountID
        /// </summary>
        public static readonly string COLUMN_ACCOUNTID = "AccountID";
        /// <summary>
        /// A static representation of column AccountReconciliationPeriodID
        /// </summary>
        public static readonly string COLUMN_ACCOUNTRECONCILIATIONPERIODID = "AccountReconciliationPeriodID";
        /// <summary>
        /// A static representation of column ReconciliationPeriodID
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONPERIODID = "ReconciliationPeriodID";
        /// <summary>
        /// Provides access to the name of the primary key column (AccountReconciliationPeriodID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "AccountReconciliationPeriodID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "AccountReconciliationPeriod";

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
        public AccountReconciliationPeriodDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "AccountReconciliationPeriod", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a AccountReconciliationPeriodInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>AccountReconciliationPeriodInfo</returns>
        protected override AccountReconciliationPeriodInfo MapObject(System.Data.IDataReader r)
        {

            AccountReconciliationPeriodInfo entity = new AccountReconciliationPeriodInfo();

            entity.AccountReconciliationPeriodID = r.GetInt32Value("AccountReconciliationPeriodID");
            entity.AccountID = r.GetInt64Value("AccountID");
            entity.ReconciliationPeriodID = r.GetInt32Value("ReconciliationPeriodID");
            entity.PeriodEndDate = r.GetDateValue("PeriodEndDate");
            entity.PeriodNumber = r.GetInt16Value("PeriodNumber"); ;
            entity.FinancialYear = r.GetStringValue("FinancialYear");

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in AccountReconciliationPeriodInfo object
        /// </summary>
        /// <param name="o">A AccountReconciliationPeriodInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(AccountReconciliationPeriodInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_AccountReconciliationPeriod");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAccountID = cmd.CreateParameter();
            parAccountID.ParameterName = "@AccountID";
            if (!entity.IsAccountIDNull)
                parAccountID.Value = entity.AccountID;
            else
                parAccountID.Value = System.DBNull.Value;
            cmdParams.Add(parAccountID);

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
        /// in AccountReconciliationPeriodInfo object
        /// </summary>
        /// <param name="o">A AccountReconciliationPeriodInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(AccountReconciliationPeriodInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_AccountReconciliationPeriod");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAccountID = cmd.CreateParameter();
            parAccountID.ParameterName = "@AccountID";
            if (!entity.IsAccountIDNull)
                parAccountID.Value = entity.AccountID;
            else
                parAccountID.Value = System.DBNull.Value;
            cmdParams.Add(parAccountID);

            System.Data.IDbDataParameter parReconciliationPeriodID = cmd.CreateParameter();
            parReconciliationPeriodID.ParameterName = "@ReconciliationPeriodID";
            if (!entity.IsReconciliationPeriodIDNull)
                parReconciliationPeriodID.Value = entity.ReconciliationPeriodID;
            else
                parReconciliationPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationPeriodID);

            System.Data.IDbDataParameter pkparAccountReconciliationPeriodID = cmd.CreateParameter();
            pkparAccountReconciliationPeriodID.ParameterName = "@AccountReconciliationPeriodID";
            pkparAccountReconciliationPeriodID.Value = entity.AccountReconciliationPeriodID;
            cmdParams.Add(pkparAccountReconciliationPeriodID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_AccountReconciliationPeriod");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AccountReconciliationPeriodID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_AccountReconciliationPeriod");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AccountReconciliationPeriodID";
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
        public IList<AccountReconciliationPeriodInfo> SelectAllByAccountID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_AccountReconciliationPeriodByAccountID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AccountID";
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
        public IList<AccountReconciliationPeriodInfo> SelectAllByReconciliationPeriodID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_AccountReconciliationPeriodByReconciliationPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationPeriodID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }

        private void MapIdentity(AccountReconciliationPeriodInfo entity, object id)
        {
            entity.AccountReconciliationPeriodID = Convert.ToInt32(id);
        }
    }
}
