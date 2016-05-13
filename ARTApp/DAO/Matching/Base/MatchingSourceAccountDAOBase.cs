

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

    public abstract class MatchingSourceAccountDAOBase : CustomAbstractDAO<MatchingSourceAccountInfo>
    {

        /// <summary>
        /// A static representation of column AccountID
        /// </summary>
        public static readonly string COLUMN_ACCOUNTID = "AccountID";
        /// <summary>
        /// A static representation of column MatchingSourceAccountID
        /// </summary>
        public static readonly string COLUMN_MATCHINGSOURCEACCOUNTID = "MatchingSourceAccountID";
        /// <summary>
        /// A static representation of column MatchingSourceDataImportID
        /// </summary>
        public static readonly string COLUMN_MATCHINGSOURCEDATAIMPORTID = "MatchingSourceDataImportID";
        /// <summary>
        /// Provides access to the name of the primary key column (MatchingSourceAccountID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "MatchingSourceAccountID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "MatchingSourceAccount";

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
        public MatchingSourceAccountDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "MatchingSourceAccount", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a MatchingSourceAccountInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>MatchingSourceAccountInfo</returns>
        protected override MatchingSourceAccountInfo MapObject(System.Data.IDataReader r)
        {

            MatchingSourceAccountInfo entity = new MatchingSourceAccountInfo();

            entity.MatchingSourceAccountID = r.GetInt64Value("MatchingSourceAccountID");
            entity.MatchingSourceDataImportID = r.GetInt64Value("MatchingSourceDataImportID");
            entity.AccountID = r.GetInt64Value("AccountID");

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in MatchingSourceAccountInfo object
        /// </summary>
        /// <param name="o">A MatchingSourceAccountInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(MatchingSourceAccountInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_MatchingSourceAccount");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAccountID = cmd.CreateParameter();
            parAccountID.ParameterName = "@AccountID";
            if (entity != null)
                parAccountID.Value = entity.AccountID;
            else
                parAccountID.Value = System.DBNull.Value;
            cmdParams.Add(parAccountID);

            System.Data.IDbDataParameter parMatchingSourceDataImportID = cmd.CreateParameter();
            parMatchingSourceDataImportID.ParameterName = "@MatchingSourceDataImportID";
            if (entity != null)
                parMatchingSourceDataImportID.Value = entity.MatchingSourceDataImportID;
            else
                parMatchingSourceDataImportID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchingSourceDataImportID);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in MatchingSourceAccountInfo object
        /// </summary>
        /// <param name="o">A MatchingSourceAccountInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(MatchingSourceAccountInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_MatchingSourceAccount");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAccountID = cmd.CreateParameter();
            parAccountID.ParameterName = "@AccountID";
            if (entity != null)
                parAccountID.Value = entity.AccountID;
            else
                parAccountID.Value = System.DBNull.Value;
            cmdParams.Add(parAccountID);

            System.Data.IDbDataParameter parMatchingSourceDataImportID = cmd.CreateParameter();
            parMatchingSourceDataImportID.ParameterName = "@MatchingSourceDataImportID";
            if (entity != null)
                parMatchingSourceDataImportID.Value = entity.MatchingSourceDataImportID;
            else
                parMatchingSourceDataImportID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchingSourceDataImportID);

            System.Data.IDbDataParameter pkparMatchingSourceAccountID = cmd.CreateParameter();
            pkparMatchingSourceAccountID.ParameterName = "@MatchingSourceAccountID";
            pkparMatchingSourceAccountID.Value = entity.MatchingSourceAccountID;
            cmdParams.Add(pkparMatchingSourceAccountID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_MatchingSourceAccount");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchingSourceAccountID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_MatchingSourceAccount");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchingSourceAccountID";
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
        public IList<MatchingSourceAccountInfo> SelectAllByMatchingSourceDataImportID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_MatchingSourceAccountByMatchingSourceDataImportID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchingSourceDataImportID";
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
        public IList<MatchingSourceAccountInfo> SelectAllByAccountID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_MatchingSourceAccountByAccountID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AccountID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(MatchingSourceAccountInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(MatchingSourceAccountDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(MatchingSourceAccountInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(MatchingSourceAccountDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(MatchingSourceAccountInfo entity, object id)
        {
            entity.MatchingSourceAccountID = Convert.ToInt64(id);
        }




    }
}
