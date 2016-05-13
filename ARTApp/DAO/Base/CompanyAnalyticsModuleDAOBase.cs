

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

    public abstract class CompanyAnalyticsModuleDAOBase : CustomAbstractDAO<CompanyAnalyticsModuleInfo>
    {

        /// <summary>
        /// A static representation of column AnalyticsModuleID
        /// </summary>
        public static readonly string COLUMN_ANALYTICSMODULEID = "AnalyticsModuleID";
        /// <summary>
        /// A static representation of column CompanyAnalyticsModuleID
        /// </summary>
        public static readonly string COLUMN_COMPANYANALYTICSMODULEID = "CompanyAnalyticsModuleID";
        /// <summary>
        /// A static representation of column CompanyID
        /// </summary>
        public static readonly string COLUMN_COMPANYID = "CompanyID";
        /// <summary>
        /// Provides access to the name of the primary key column (CompanyAnalyticsModuleID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "CompanyAnalyticsModuleID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "CompanyAnalyticsModule";

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
        public CompanyAnalyticsModuleDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "CompanyAnalyticsModule", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a CompanyAnalyticsModuleInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>CompanyAnalyticsModuleInfo</returns>
        protected override CompanyAnalyticsModuleInfo MapObject(System.Data.IDataReader r)
        {

            CompanyAnalyticsModuleInfo entity = new CompanyAnalyticsModuleInfo();


            try
            {
                int ordinal = r.GetOrdinal("CompanyAnalyticsModuleID");
                if (!r.IsDBNull(ordinal)) entity.CompanyAnalyticsModuleID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("CompanyID");
                if (!r.IsDBNull(ordinal)) entity.CompanyID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("AnalyticsModuleID");
                if (!r.IsDBNull(ordinal)) entity.AnalyticsModuleID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in CompanyAnalyticsModuleInfo object
        /// </summary>
        /// <param name="o">A CompanyAnalyticsModuleInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(CompanyAnalyticsModuleInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_CompanyAnalyticsModule");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAnalyticsModuleID = cmd.CreateParameter();
            parAnalyticsModuleID.ParameterName = "@AnalyticsModuleID";
            if (entity != null)
                parAnalyticsModuleID.Value = entity.AnalyticsModuleID;
            else
                parAnalyticsModuleID.Value = System.DBNull.Value;
            cmdParams.Add(parAnalyticsModuleID);

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (entity != null)
                parCompanyID.Value = entity.CompanyID;
            else
                parCompanyID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyID);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in CompanyAnalyticsModuleInfo object
        /// </summary>
        /// <param name="o">A CompanyAnalyticsModuleInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(CompanyAnalyticsModuleInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_CompanyAnalyticsModule");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAnalyticsModuleID = cmd.CreateParameter();
            parAnalyticsModuleID.ParameterName = "@AnalyticsModuleID";
            if (entity != null)
                parAnalyticsModuleID.Value = entity.AnalyticsModuleID;
            else
                parAnalyticsModuleID.Value = System.DBNull.Value;
            cmdParams.Add(parAnalyticsModuleID);

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (entity != null)
                parCompanyID.Value = entity.CompanyID;
            else
                parCompanyID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter pkparCompanyAnalyticsModuleID = cmd.CreateParameter();
            pkparCompanyAnalyticsModuleID.ParameterName = "@CompanyAnalyticsModuleID";
            pkparCompanyAnalyticsModuleID.Value = entity.CompanyAnalyticsModuleID;
            cmdParams.Add(pkparCompanyAnalyticsModuleID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_CompanyAnalyticsModule");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyAnalyticsModuleID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_CompanyAnalyticsModule");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyAnalyticsModuleID";
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
        public IList<CompanyAnalyticsModuleInfo> SelectAllByCompanyID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CompanyAnalyticsModuleByCompanyID");
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
        public IList<CompanyAnalyticsModuleInfo> SelectAllByAnalyticsModuleID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CompanyAnalyticsModuleByAnalyticsModuleID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AnalyticsModuleID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(CompanyAnalyticsModuleInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(CompanyAnalyticsModuleDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(CompanyAnalyticsModuleInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(CompanyAnalyticsModuleDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(CompanyAnalyticsModuleInfo entity, object id)
        {
            entity.CompanyAnalyticsModuleID = Convert.ToInt16(id);
        }




    }
}
