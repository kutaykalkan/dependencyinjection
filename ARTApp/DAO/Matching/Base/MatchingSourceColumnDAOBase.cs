

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

    public abstract class MatchingSourceColumnDAOBase : CustomAbstractDAO<MatchingSourceColumnInfo>
    {

        /// <summary>
        /// A static representation of column ColumnName
        /// </summary>
        public static readonly string COLUMN_COLUMNNAME = "ColumnName";
        /// <summary>
        /// A static representation of column DataTypeID
        /// </summary>
        public static readonly string COLUMN_DATATYPEID = "DataTypeID";
        /// <summary>
        /// A static representation of column MatchingSourceColumnID
        /// </summary>
        public static readonly string COLUMN_MATCHINGSOURCECOLUMNID = "MatchingSourceColumnID";
        /// <summary>
        /// A static representation of column MatchingSourceDataImportID
        /// </summary>
        public static readonly string COLUMN_MATCHINGSOURCEDATAIMPORTID = "MatchingSourceDataImportID";
        /// <summary>
        /// Provides access to the name of the primary key column (MatchingSourceColumnID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "MatchingSourceColumnID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "MatchingSourceColumn";

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
        public MatchingSourceColumnDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "MatchingSourceColumn", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a MatchingSourceColumnInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>MatchingSourceColumnInfo</returns>
        protected override MatchingSourceColumnInfo MapObject(System.Data.IDataReader r)
        {

            MatchingSourceColumnInfo entity = new MatchingSourceColumnInfo();

            entity.MatchingSourceColumnID = r.GetInt64Value("MatchingSourceColumnID");
            entity.MatchingSourceDataImportID = r.GetInt64Value("MatchingSourceDataImportID");
            entity.ColumnName = r.GetStringValue("ColumnName");
            entity.DataTypeID = r.GetInt16Value("DataTypeID");

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in MatchingSourceColumnInfo object
        /// </summary>
        /// <param name="o">A MatchingSourceColumnInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(MatchingSourceColumnInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_MatchingSourceColumn");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parColumnName = cmd.CreateParameter();
            parColumnName.ParameterName = "@ColumnName";
            if (entity != null)
                parColumnName.Value = entity.ColumnName;
            else
                parColumnName.Value = System.DBNull.Value;
            cmdParams.Add(parColumnName);

            System.Data.IDbDataParameter parDataTypeID = cmd.CreateParameter();
            parDataTypeID.ParameterName = "@DataTypeID";
            if (entity != null)
                parDataTypeID.Value = entity.DataTypeID;
            else
                parDataTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parDataTypeID);

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
        /// in MatchingSourceColumnInfo object
        /// </summary>
        /// <param name="o">A MatchingSourceColumnInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(MatchingSourceColumnInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_MatchingSourceColumn");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parColumnName = cmd.CreateParameter();
            parColumnName.ParameterName = "@ColumnName";
            if (entity != null)
                parColumnName.Value = entity.ColumnName;
            else
                parColumnName.Value = System.DBNull.Value;
            cmdParams.Add(parColumnName);

            System.Data.IDbDataParameter parDataTypeID = cmd.CreateParameter();
            parDataTypeID.ParameterName = "@DataTypeID";
            if (entity != null)
                parDataTypeID.Value = entity.DataTypeID;
            else
                parDataTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parDataTypeID);

            System.Data.IDbDataParameter parMatchingSourceDataImportID = cmd.CreateParameter();
            parMatchingSourceDataImportID.ParameterName = "@MatchingSourceDataImportID";
            if (entity != null)
                parMatchingSourceDataImportID.Value = entity.MatchingSourceDataImportID;
            else
                parMatchingSourceDataImportID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchingSourceDataImportID);

            System.Data.IDbDataParameter pkparMatchingSourceColumnID = cmd.CreateParameter();
            pkparMatchingSourceColumnID.ParameterName = "@MatchingSourceColumnID";
            pkparMatchingSourceColumnID.Value = entity.MatchingSourceColumnID;
            cmdParams.Add(pkparMatchingSourceColumnID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_MatchingSourceColumn");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchingSourceColumnID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_MatchingSourceColumn");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchingSourceColumnID";
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
        public IList<MatchingSourceColumnInfo> SelectAllByMatchingSourceDataImportID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_MatchingSourceColumnByMatchingSourceDataImportID");
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
        public IList<MatchingSourceColumnInfo> SelectAllByDataTypeID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_MatchingSourceColumnByDataTypeID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DataTypeID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(MatchingSourceColumnInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(MatchingSourceColumnDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(MatchingSourceColumnInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(MatchingSourceColumnDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(MatchingSourceColumnInfo entity, object id)
        {
            entity.MatchingSourceColumnID = Convert.ToInt64(id);
        }









        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<MatchingSourceColumnInfo> SelectMatchingSourceColumnDetailsAssociatedToMatchSetSubSetCombinationByMatchingConfiguration(MatchSetSubSetCombinationInfo entity)
        {
            return this.SelectMatchingSourceColumnDetailsAssociatedToMatchSetSubSetCombinationByMatchingConfiguration(entity.MatchSetSubSetCombinationID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<MatchingSourceColumnInfo> SelectMatchingSourceColumnDetailsAssociatedToMatchSetSubSetCombinationByMatchingConfiguration(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [MatchingSourceColumn] INNER JOIN [MatchingConfiguration] ON [MatchingSourceColumn].[MatchingSourceColumnID] = [MatchingConfiguration].[MatchingSource1ColumnID] INNER JOIN [MatchSetSubSetCombination] ON [MatchingConfiguration].[MatchSetSubSetCombinationID] = [MatchSetSubSetCombination].[MatchSetSubSetCombinationID]  WHERE  [MatchSetSubSetCombination].[MatchSetSubSetCombinationID] = @MatchSetSubSetCombinationID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchSetSubSetCombinationID";
            par.Value = id;

            cmdParams.Add(par);
            List<MatchingSourceColumnInfo> objMatchingSourceColumnEntityColl = new List<MatchingSourceColumnInfo>(this.Select(cmd));
            return objMatchingSourceColumnEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<MatchingSourceColumnInfo> SelectMatchingSourceColumnDetailsAssociatedToMatchingSourceColumnByMatchingConfiguration(MatchingSourceColumnInfo entity)
        {
            return this.SelectMatchingSourceColumnDetailsAssociatedToMatchingSourceColumnByMatchingConfiguration(entity.MatchingSourceColumnID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<MatchingSourceColumnInfo> SelectMatchingSourceColumnDetailsAssociatedToMatchingSourceColumnByMatchingConfiguration(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [MatchingSourceColumn] INNER JOIN [MatchingConfiguration] ON [MatchingSourceColumn].[MatchingSourceColumnID] = [MatchingConfiguration].[MatchingSource1ColumnID] INNER JOIN [MatchingSourceColumn] ON [MatchingConfiguration].[MatchingSource2ColumnID] = [MatchingSourceColumn].[MatchingSourceColumnID]  WHERE  [MatchingSourceColumn].[MatchingSourceColumnID] = @MatchingSourceColumnID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchingSourceColumnID";
            par.Value = id;

            cmdParams.Add(par);
            List<MatchingSourceColumnInfo> objMatchingSourceColumnEntityColl = new List<MatchingSourceColumnInfo>(this.Select(cmd));
            return objMatchingSourceColumnEntityColl;
        }




    }
}
