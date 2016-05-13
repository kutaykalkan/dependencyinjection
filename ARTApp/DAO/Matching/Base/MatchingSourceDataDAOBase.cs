

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

    public abstract class MatchingSourceDataDAOBase : CustomAbstractDAO<MatchingSourceDataInfo>
    {

        /// <summary>
        /// A static representation of column Data
        /// </summary>
        public static readonly string COLUMN_DATA = "Data";
        /// <summary>
        /// A static representation of column MatchingSourceDataID
        /// </summary>
        public static readonly string COLUMN_MATCHINGSOURCEDATAID = "MatchingSourceDataID";
        /// <summary>
        /// A static representation of column MatchingSourceDataImportID
        /// </summary>
        public static readonly string COLUMN_MATCHINGSOURCEDATAIMPORTID = "MatchingSourceDataImportID";
        /// <summary>
        /// Provides access to the name of the primary key column (MatchingSourceDataID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "MatchingSourceDataID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "MatchingSourceData";

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
        public MatchingSourceDataDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "MatchingSourceData", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a MatchingSourceDataInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>MatchingSourceDataInfo</returns>
        protected override MatchingSourceDataInfo MapObject(System.Data.IDataReader r)
        {

            MatchingSourceDataInfo entity = new MatchingSourceDataInfo();

            entity.MatchingSourceDataID = r.GetInt64Value("MatchingSourceDataID");
            entity.MatchingSourceDataImportID = r.GetInt64Value("MatchingSourceDataImportID");
            entity.Data = r.GetStringValue("Data");

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in MatchingSourceDataInfo object
        /// </summary>
        /// <param name="o">A MatchingSourceDataInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(MatchingSourceDataInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_MatchingSourceData");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parData = cmd.CreateParameter();
            parData.ParameterName = "@Data";
            if (entity != null)
                parData.Value = entity.Data;
            else
                parData.Value = System.DBNull.Value;
            cmdParams.Add(parData);

            System.Data.IDbDataParameter parMatchingSourceDataImportID = cmd.CreateParameter();
            parMatchingSourceDataImportID.ParameterName = "@MatcihngSourceDataImportID";
            if (entity != null)
                parMatchingSourceDataImportID.Value = entity.MatchingSourceDataImportID;
            else
                parMatchingSourceDataImportID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchingSourceDataImportID);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in MatchingSourceDataInfo object
        /// </summary>
        /// <param name="o">A MatchingSourceDataInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(MatchingSourceDataInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_MatchingSourceData");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parData = cmd.CreateParameter();
            parData.ParameterName = "@Data";
            if (entity != null)
                parData.Value = entity.Data;
            else
                parData.Value = System.DBNull.Value;
            cmdParams.Add(parData);

            System.Data.IDbDataParameter parMatchingSourceDataImportID = cmd.CreateParameter();
            parMatchingSourceDataImportID.ParameterName = "@MatcihngSourceDataImportID";
            if (entity != null)
                parMatchingSourceDataImportID.Value = entity.MatchingSourceDataImportID;
            else
                parMatchingSourceDataImportID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchingSourceDataImportID);

            System.Data.IDbDataParameter pkparMatchingSourceDataID = cmd.CreateParameter();
            pkparMatchingSourceDataID.ParameterName = "@MatchingSourceDataID";
            pkparMatchingSourceDataID.Value = entity.MatchingSourceDataID;
            cmdParams.Add(pkparMatchingSourceDataID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_MatchingSourceData");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchingSourceDataID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_MatchingSourceData");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchingSourceDataID";
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
        public IList<MatchingSourceDataInfo> SelectAllByMatchingSourceDataImportID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_MatchingSourceDataByMatcihngSourceDataImportID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatcihngSourceDataImportID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(MatchingSourceDataInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(MatchingSourceDataDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(MatchingSourceDataInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(MatchingSourceDataDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(MatchingSourceDataInfo entity, object id)
        {
            entity.MatchingSourceDataID = Convert.ToInt64(id);
        }




    }
}
