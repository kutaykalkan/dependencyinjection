

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.Client.Model.Matching;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO.Matching.Base
{

    public abstract class MatchingConfigurationRuleDAOBase : CustomAbstractDAO<MatchingConfigurationRuleInfo>
    {

        /// <summary>
        /// A static representation of column Keywords
        /// </summary>
        public static readonly string COLUMN_KEYWORDS = "Keywords";
        /// <summary>
        /// A static representation of column LowerBound
        /// </summary>
        public static readonly string COLUMN_LOWERBOUND = "LowerBound";
        /// <summary>
        /// A static representation of column MatchingConfigurationID
        /// </summary>
        public static readonly string COLUMN_MATCHINGCONFIGURATIONID = "MatchingConfigurationID";
        /// <summary>
        /// A static representation of column MatchingConfigurationRuleID
        /// </summary>
        public static readonly string COLUMN_MATCHINGCONFIGURATIONRULEID = "MatchingConfigurationRuleID";
        /// <summary>
        /// A static representation of column OperatorID
        /// </summary>
        public static readonly string COLUMN_OPERATORID = "OperatorID";
        /// <summary>
        /// A static representation of column ThresholdTypeID
        /// </summary>
        public static readonly string COLUMN_THRESHOLDTYPEID = "ThresholdTypeID";
        /// <summary>
        /// A static representation of column UpperBound
        /// </summary>
        public static readonly string COLUMN_UPPERBOUND = "UpperBound";
        /// <summary>
        /// Provides access to the name of the primary key column (MatchingConfigurationRuleID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "MatchingConfigurationRuleID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "MatchingConfigurationRule";

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
        public MatchingConfigurationRuleDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "MatchingConfigurationRule", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a MatchingConfigurationRuleInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>MatchingConfigurationRuleInfo</returns>
        protected override MatchingConfigurationRuleInfo MapObject(System.Data.IDataReader r)
        {

            MatchingConfigurationRuleInfo entity = new MatchingConfigurationRuleInfo();

            entity.MatchingConfigurationRuleID = r.GetInt64Value("MatchingConfigurationRuleID");
            entity.MatchingConfigurationID = r.GetInt64Value("MatchingConfigurationID");
            entity.OperatorID = r.GetInt16Value("OperatorID");
            entity.ThresholdTypeID = r.GetInt16Value("ThresholdTypeID");
            entity.LowerBound = r.GetDecimalValue("LowerBound");
            entity.UpperBound = r.GetDecimalValue("UpperBound");
            entity.Keywords = r.GetStringValue("Keywords");

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in MatchingConfigurationRuleInfo object
        /// </summary>
        /// <param name="o">A MatchingConfigurationRuleInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(MatchingConfigurationRuleInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_MatchingConfigurationRule");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parKeywords = cmd.CreateParameter();
            parKeywords.ParameterName = "@Keywords";
            if (entity != null)
                parKeywords.Value = entity.Keywords;
            else
                parKeywords.Value = System.DBNull.Value;
            cmdParams.Add(parKeywords);

            System.Data.IDbDataParameter parLowerBound = cmd.CreateParameter();
            parLowerBound.ParameterName = "@LowerBound";
            if (entity != null)
                parLowerBound.Value = entity.LowerBound;
            else
                parLowerBound.Value = System.DBNull.Value;
            cmdParams.Add(parLowerBound);

            System.Data.IDbDataParameter parMatchingConfigurationID = cmd.CreateParameter();
            parMatchingConfigurationID.ParameterName = "@MatchingConfigurationID";
            if (entity != null)
                parMatchingConfigurationID.Value = entity.MatchingConfigurationID;
            else
                parMatchingConfigurationID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchingConfigurationID);

            System.Data.IDbDataParameter parOperatorID = cmd.CreateParameter();
            parOperatorID.ParameterName = "@OperatorID";
            if (entity != null)
                parOperatorID.Value = entity.OperatorID;
            else
                parOperatorID.Value = System.DBNull.Value;
            cmdParams.Add(parOperatorID);

            System.Data.IDbDataParameter parThresholdTypeID = cmd.CreateParameter();
            parThresholdTypeID.ParameterName = "@ThresholdTypeID";
            if (entity != null)
                parThresholdTypeID.Value = entity.ThresholdTypeID;
            else
                parThresholdTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parThresholdTypeID);

            System.Data.IDbDataParameter parUpperBound = cmd.CreateParameter();
            parUpperBound.ParameterName = "@UpperBound";
            if (entity != null)
                parUpperBound.Value = entity.UpperBound;
            else
                parUpperBound.Value = System.DBNull.Value;
            cmdParams.Add(parUpperBound);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in MatchingConfigurationRuleInfo object
        /// </summary>
        /// <param name="o">A MatchingConfigurationRuleInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(MatchingConfigurationRuleInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_MatchingConfigurationRule");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parKeywords = cmd.CreateParameter();
            parKeywords.ParameterName = "@Keywords";
            if (entity != null)
                parKeywords.Value = entity.Keywords;
            else
                parKeywords.Value = System.DBNull.Value;
            cmdParams.Add(parKeywords);

            System.Data.IDbDataParameter parLowerBound = cmd.CreateParameter();
            parLowerBound.ParameterName = "@LowerBound";
            if (entity != null)
                parLowerBound.Value = entity.LowerBound;
            else
                parLowerBound.Value = System.DBNull.Value;
            cmdParams.Add(parLowerBound);

            System.Data.IDbDataParameter parMatchingConfigurationID = cmd.CreateParameter();
            parMatchingConfigurationID.ParameterName = "@MatchingConfigurationID";
            if (entity != null)
                parMatchingConfigurationID.Value = entity.MatchingConfigurationID;
            else
                parMatchingConfigurationID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchingConfigurationID);

            System.Data.IDbDataParameter parOperatorID = cmd.CreateParameter();
            parOperatorID.ParameterName = "@OperatorID";
            if (entity != null)
                parOperatorID.Value = entity.OperatorID;
            else
                parOperatorID.Value = System.DBNull.Value;
            cmdParams.Add(parOperatorID);

            System.Data.IDbDataParameter parThresholdTypeID = cmd.CreateParameter();
            parThresholdTypeID.ParameterName = "@ThresholdTypeID";
            if (entity != null)
                parThresholdTypeID.Value = entity.ThresholdTypeID;
            else
                parThresholdTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parThresholdTypeID);

            System.Data.IDbDataParameter parUpperBound = cmd.CreateParameter();
            parUpperBound.ParameterName = "@UpperBound";
            if (entity != null)
                parUpperBound.Value = entity.UpperBound;
            else
                parUpperBound.Value = System.DBNull.Value;
            cmdParams.Add(parUpperBound);

            System.Data.IDbDataParameter pkparMatchingConfigurationRuleID = cmd.CreateParameter();
            pkparMatchingConfigurationRuleID.ParameterName = "@MatchingConfigurationRuleID";
            pkparMatchingConfigurationRuleID.Value = entity.MatchingConfigurationRuleID;
            cmdParams.Add(pkparMatchingConfigurationRuleID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_MatchingConfigurationRule");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchingConfigurationRuleID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_MatchingConfigurationRule");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchingConfigurationRuleID";
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
        public IList<MatchingConfigurationRuleInfo> SelectAllByOperatorID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_MatchingConfigurationRuleByOperatorID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@OperatorID";
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
        public IList<MatchingConfigurationRuleInfo> SelectAllByThresholdTypeID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_MatchingConfigurationRuleByThresholdTypeID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ThresholdTypeID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(MatchingConfigurationRuleInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(MatchingConfigurationRuleDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(MatchingConfigurationRuleInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(MatchingConfigurationRuleDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(MatchingConfigurationRuleInfo entity, object id)
        {
            entity.MatchingConfigurationRuleID = Convert.ToInt64(id);
        }




    }
}
