

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

    public abstract class RiskRatingReconciliationPeriodDAOBase : CustomAbstractDAO<RiskRatingReconciliationPeriodInfo>
    {

        /// <summary>
        /// A static representation of column AddedBy
        /// </summary>
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
        /// <summary>
        /// A static representation of column CompanyID
        /// </summary>
        public static readonly string COLUMN_COMPANYID = "CompanyID";
        /// <summary>
        /// A static representation of column DateAdded
        /// </summary>
        public static readonly string COLUMN_DATEADDED = "DateAdded";
        /// <summary>
        /// A static representation of column EndReconciliationPeriodID
        /// </summary>
        public static readonly string COLUMN_ENDRECONCILIATIONPERIODID = "EndReconciliationPeriodID";
        /// <summary>
        /// A static representation of column RiskRatingID
        /// </summary>
        public static readonly string COLUMN_RISKRATINGID = "RiskRatingID";
        /// <summary>
        /// A static representation of column RiskRatingReconciliationPeriodID
        /// </summary>
        public static readonly string COLUMN_RISKRATINGRECONCILIATIONPERIODID = "RiskRatingReconciliationPeriodID";
        /// <summary>
        /// A static representation of column StartReconciliationPeriodID
        /// </summary>
        public static readonly string COLUMN_STARTRECONCILIATIONPERIODID = "StartReconciliationPeriodID";
        /// <summary>
        /// Provides access to the name of the primary key column (RiskRatingReconciliationPeriodID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "RiskRatingReconciliationPeriodID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "RiskRatingReconciliationPeriod";

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
        public RiskRatingReconciliationPeriodDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "RiskRatingReconciliationPeriod", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a RiskRatingReconciliationPeriodInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>RiskRatingReconciliationPeriodInfo</returns>
        protected override RiskRatingReconciliationPeriodInfo MapObject(System.Data.IDataReader r)
        {

            RiskRatingReconciliationPeriodInfo entity = new RiskRatingReconciliationPeriodInfo();

            entity.RiskRatingReconciliationPeriodID = r.GetInt32Value("RiskRatingReconciliationPeriodID");
            entity.CompanyID = r.GetInt32Value("CompanyID");
            entity.RiskRatingID = r.GetInt16Value("RiskRatingID");
            entity.StartReconciliationPeriodID = r.GetInt32Value("StartReconciliationPeriodID");
            entity.EndReconciliationPeriodID = r.GetInt32Value("EndReconciliationPeriodID");
            entity.DateAdded = r.GetDateValue("DateAdded");
            entity.AddedBy = r.GetStringValue("AddedBy");
            entity.ReconciliationPeriodID = r.GetInt32Value("ReconciliationPeriodID");
            entity.PeriodEndDate = r.GetDateValue("PeriodEndDate");
            entity.PeriodNumber = r.GetInt16Value("PeriodNumber");
            entity.FinancialYear = r.GetStringValue("FinancialYear");
            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in RiskRatingReconciliationPeriodInfo object
        /// </summary>
        /// <param name="o">A RiskRatingReconciliationPeriodInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(RiskRatingReconciliationPeriodInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_RiskRatingReconciliationPeriod");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (!entity.IsCompanyIDNull)
                parCompanyID.Value = entity.CompanyID;
            else
                parCompanyID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (!entity.IsDateAddedNull)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
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

            System.Data.IDbDataParameter parRiskRatingID = cmd.CreateParameter();
            parRiskRatingID.ParameterName = "@RiskRatingID";
            if (!entity.IsRiskRatingIDNull)
                parRiskRatingID.Value = entity.RiskRatingID;
            else
                parRiskRatingID.Value = System.DBNull.Value;
            cmdParams.Add(parRiskRatingID);

            System.Data.IDbDataParameter parStartReconciliationPeriodID = cmd.CreateParameter();
            parStartReconciliationPeriodID.ParameterName = "@StartReconciliationPeriodID";
            if (!entity.IsStartReconciliationPeriodIDNull)
                parStartReconciliationPeriodID.Value = entity.StartReconciliationPeriodID;
            else
                parStartReconciliationPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parStartReconciliationPeriodID);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in RiskRatingReconciliationPeriodInfo object
        /// </summary>
        /// <param name="o">A RiskRatingReconciliationPeriodInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(RiskRatingReconciliationPeriodInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_RiskRatingReconciliationPeriod");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (!entity.IsCompanyIDNull)
                parCompanyID.Value = entity.CompanyID;
            else
                parCompanyID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (!entity.IsDateAddedNull)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
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

            System.Data.IDbDataParameter parRiskRatingID = cmd.CreateParameter();
            parRiskRatingID.ParameterName = "@RiskRatingID";
            if (!entity.IsRiskRatingIDNull)
                parRiskRatingID.Value = entity.RiskRatingID;
            else
                parRiskRatingID.Value = System.DBNull.Value;
            cmdParams.Add(parRiskRatingID);

            System.Data.IDbDataParameter parStartReconciliationPeriodID = cmd.CreateParameter();
            parStartReconciliationPeriodID.ParameterName = "@StartReconciliationPeriodID";
            if (!entity.IsStartReconciliationPeriodIDNull)
                parStartReconciliationPeriodID.Value = entity.StartReconciliationPeriodID;
            else
                parStartReconciliationPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parStartReconciliationPeriodID);

            System.Data.IDbDataParameter pkparRiskRatingReconciliationPeriodID = cmd.CreateParameter();
            pkparRiskRatingReconciliationPeriodID.ParameterName = "@RiskRatingReconciliationPeriodID";
            pkparRiskRatingReconciliationPeriodID.Value = entity.RiskRatingReconciliationPeriodID;
            cmdParams.Add(pkparRiskRatingReconciliationPeriodID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_RiskRatingReconciliationPeriod");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RiskRatingReconciliationPeriodID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_RiskRatingReconciliationPeriod");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RiskRatingReconciliationPeriodID";
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
        public IList<RiskRatingReconciliationPeriodInfo> SelectAllByCompanyID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_RiskRatingReconciliationPeriodByCompanyID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyID";
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
        public IList<RiskRatingReconciliationPeriodInfo> SelectAllByRiskRatingID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_RiskRatingReconciliationPeriodByRiskRatingID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RiskRatingID";
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
        public IList<RiskRatingReconciliationPeriodInfo> SelectAllByStartReconciliationPeriodID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_RiskRatingReconciliationPeriodByStartReconciliationPeriodID");
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
        public IList<RiskRatingReconciliationPeriodInfo> SelectAllByEndReconciliationPeriodID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_RiskRatingReconciliationPeriodByEndReconciliationPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@EndReconciliationPeriodID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(RiskRatingReconciliationPeriodInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(RiskRatingReconciliationPeriodDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(RiskRatingReconciliationPeriodInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(RiskRatingReconciliationPeriodDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(RiskRatingReconciliationPeriodInfo entity, object id)
        {
            entity.RiskRatingReconciliationPeriodID = Convert.ToInt32(id);
        }




    }
}
