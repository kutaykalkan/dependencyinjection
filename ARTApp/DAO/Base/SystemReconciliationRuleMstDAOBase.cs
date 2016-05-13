

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

    public abstract class SystemReconciliationRuleMstDAOBase : CustomAbstractDAO<SystemReconciliationRuleMstInfo>
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
        /// A static representation of column SystemReconciliationRule
        /// </summary>
        public static readonly string COLUMN_SYSTEMRECONCILIATIONRULE = "SystemReconciliationRule";
        /// <summary>
        /// A static representation of column SystemReconciliationRuleID
        /// </summary>
        public static readonly string COLUMN_SYSTEMRECONCILIATIONRULEID = "SystemReconciliationRuleID";
        /// <summary>
        /// A static representation of column SystemReconciliationRuleLabelID
        /// </summary>
        public static readonly string COLUMN_SYSTEMRECONCILIATIONRULELABELID = "SystemReconciliationRuleLabelID";
        /// <summary>
        /// Provides access to the name of the primary key column (SystemReconciliationRuleID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "SystemReconciliationRuleID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "SystemReconciliationRuleMst";

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
        public SystemReconciliationRuleMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "SystemReconciliationRuleMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a SystemReconciliationRuleMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>SystemReconciliationRuleMstInfo</returns>
        protected override SystemReconciliationRuleMstInfo MapObject(System.Data.IDataReader r)
        {

            SystemReconciliationRuleMstInfo entity = new SystemReconciliationRuleMstInfo();


            try
            {
                int ordinal = r.GetOrdinal("SystemReconciliationRuleID");
                if (!r.IsDBNull(ordinal)) entity.SystemReconciliationRuleID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("SystemReconciliationRule");
                if (!r.IsDBNull(ordinal)) entity.SystemReconciliationRule = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("SystemReconciliationRuleLabelID");
                if (!r.IsDBNull(ordinal)) entity.SystemReconciliationRuleLabelID = ((System.Int32)(r.GetValue(ordinal)));
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

            entity.CapabilityID = r.GetInt16Value("CapabilityID");

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in SystemReconciliationRuleMstInfo object
        /// </summary>
        /// <param name="o">A SystemReconciliationRuleMstInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(SystemReconciliationRuleMstInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_SystemReconciliationRuleMst");
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

            System.Data.IDbDataParameter parSystemReconciliationRule = cmd.CreateParameter();
            parSystemReconciliationRule.ParameterName = "@SystemReconciliationRule";
            if (!entity.IsSystemReconciliationRuleNull)
                parSystemReconciliationRule.Value = entity.SystemReconciliationRule;
            else
                parSystemReconciliationRule.Value = System.DBNull.Value;
            cmdParams.Add(parSystemReconciliationRule);

            System.Data.IDbDataParameter parSystemReconciliationRuleLabelID = cmd.CreateParameter();
            parSystemReconciliationRuleLabelID.ParameterName = "@SystemReconciliationRuleLabelID";
            if (!entity.IsSystemReconciliationRuleLabelIDNull)
                parSystemReconciliationRuleLabelID.Value = entity.SystemReconciliationRuleLabelID;
            else
                parSystemReconciliationRuleLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parSystemReconciliationRuleLabelID);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in SystemReconciliationRuleMstInfo object
        /// </summary>
        /// <param name="o">A SystemReconciliationRuleMstInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(SystemReconciliationRuleMstInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_SystemReconciliationRuleMst");
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

            System.Data.IDbDataParameter parSystemReconciliationRule = cmd.CreateParameter();
            parSystemReconciliationRule.ParameterName = "@SystemReconciliationRule";
            if (!entity.IsSystemReconciliationRuleNull)
                parSystemReconciliationRule.Value = entity.SystemReconciliationRule;
            else
                parSystemReconciliationRule.Value = System.DBNull.Value;
            cmdParams.Add(parSystemReconciliationRule);

            System.Data.IDbDataParameter parSystemReconciliationRuleLabelID = cmd.CreateParameter();
            parSystemReconciliationRuleLabelID.ParameterName = "@SystemReconciliationRuleLabelID";
            if (!entity.IsSystemReconciliationRuleLabelIDNull)
                parSystemReconciliationRuleLabelID.Value = entity.SystemReconciliationRuleLabelID;
            else
                parSystemReconciliationRuleLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parSystemReconciliationRuleLabelID);

            System.Data.IDbDataParameter pkparSystemReconciliationRuleID = cmd.CreateParameter();
            pkparSystemReconciliationRuleID.ParameterName = "@SystemReconciliationRuleID";
            pkparSystemReconciliationRuleID.Value = entity.SystemReconciliationRuleID;
            cmdParams.Add(pkparSystemReconciliationRuleID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_SystemReconciliationRuleMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@SystemReconciliationRuleID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_SystemReconciliationRuleMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@SystemReconciliationRuleID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }







        protected override void CustomSave(SystemReconciliationRuleMstInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(SystemReconciliationRuleMstDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(SystemReconciliationRuleMstInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(SystemReconciliationRuleMstDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(SystemReconciliationRuleMstInfo entity, object id)
        {
            entity.SystemReconciliationRuleID = Convert.ToInt16(id);
        }








        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<SystemReconciliationRuleMstInfo> SelectSystemReconciliationRuleMstDetailsAssociatedToCompanyHdrByCompanySystemReconciliationRule(CompanyHdrInfo entity)
        {
            return this.SelectSystemReconciliationRuleMstDetailsAssociatedToCompanyHdrByCompanySystemReconciliationRule(entity.CompanyID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<SystemReconciliationRuleMstInfo> SelectSystemReconciliationRuleMstDetailsAssociatedToCompanyHdrByCompanySystemReconciliationRule(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [SystemReconciliationRuleMst] INNER JOIN [CompanySystemReconciliationRule] ON [SystemReconciliationRuleMst].[SystemReconciliationRuleID] = [CompanySystemReconciliationRule].[SystemReconciliationRuleID] INNER JOIN [CompanyHdr] ON [CompanySystemReconciliationRule].[CompanyID] = [CompanyHdr].[CompanyID]  WHERE  [CompanyHdr].[CompanyID] = @CompanyID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyID";
            par.Value = id;

            cmdParams.Add(par);
            List<SystemReconciliationRuleMstInfo> objSystemReconciliationRuleMstEntityColl = new List<SystemReconciliationRuleMstInfo>(this.Select(cmd));
            return objSystemReconciliationRuleMstEntityColl;
        }

    }
}
