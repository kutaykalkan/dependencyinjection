

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

    public abstract class MatchSetSubSetCombinationDAOBase : CustomAbstractDAO<MatchSetSubSetCombinationInfo>
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
        /// A static representation of column IsActive
        /// </summary>
        public static readonly string COLUMN_ISACTIVE = "IsActive";
        /// <summary>
        /// A static representation of column MatchSetMatchingSourceDataImport1ID
        /// </summary>
        public static readonly string COLUMN_MATCHSETMATCHINGSOURCEDATAIMPORT1ID = "MatchSetMatchingSourceDataImport1ID";
        /// <summary>
        /// A static representation of column MatchSetMatchingSourceDataImport2ID
        /// </summary>
        public static readonly string COLUMN_MATCHSETMATCHINGSOURCEDATAIMPORT2ID = "MatchSetMatchingSourceDataImport2ID";
        /// <summary>
        /// A static representation of column MatchSetSubSetCombinationID
        /// </summary>
        public static readonly string COLUMN_MATCHSETSUBSETCOMBINATIONID = "MatchSetSubSetCombinationID";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// Provides access to the name of the primary key column (MatchSetSubSetCombinationID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "MatchSetSubSetCombinationID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "MatchSetSubSetCombination";

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
        public MatchSetSubSetCombinationDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "MatchSetSubSetCombination", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a MatchSetSubSetCombinationInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>MatchSetSubSetCombinationInfo</returns>
        protected override MatchSetSubSetCombinationInfo MapObject(System.Data.IDataReader r)
        {

            MatchSetSubSetCombinationInfo entity = new MatchSetSubSetCombinationInfo();

            entity.MatchSetSubSetCombinationID = r.GetInt64Value("MatchSetSubSetCombinationID");
            entity.MatchSetMatchingSourceDataImport1ID = r.GetInt64Value("MatchSetMatchingSourceDataImport1ID");
            entity.MatchSetMatchingSourceDataImport2ID = r.GetInt64Value("MatchSetMatchingSourceDataImport2ID");
            entity.MatchSetSubSetCombinationName = r.GetStringValue("MatchSetSubSetCombinationName");
            entity.IsConfigurationComplete = r.GetBooleanValue("IsConfigurationComplete");
            entity.IsActive = r.GetBooleanValue("IsActive");
            entity.DateAdded = r.GetDateValue("DateAdded");
            entity.AddedBy = r.GetStringValue("AddedBy");
            entity.DateRevised = r.GetDateValue("DateRevised");
            entity.RevisedBy = r.GetStringValue("RevisedBy");

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in MatchSetSubSetCombinationInfo object
        /// </summary>
        /// <param name="o">A MatchSetSubSetCombinationInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(MatchSetSubSetCombinationInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_MatchSetSubSetCombination");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (entity != null)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (entity != null)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (entity != null)
                parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (entity != null)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parMatchSetMatchingSourceDataImport1ID = cmd.CreateParameter();
            parMatchSetMatchingSourceDataImport1ID.ParameterName = "@MatchSetMatchingSourceDataImport1ID";
            if (entity != null)
                parMatchSetMatchingSourceDataImport1ID.Value = entity.MatchSetMatchingSourceDataImport1ID;
            else
                parMatchSetMatchingSourceDataImport1ID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchSetMatchingSourceDataImport1ID);

            System.Data.IDbDataParameter parMatchSetMatchingSourceDataImport2ID = cmd.CreateParameter();
            parMatchSetMatchingSourceDataImport2ID.ParameterName = "@MatchSetMatchingSourceDataImport2ID";
            if (entity != null)
                parMatchSetMatchingSourceDataImport2ID.Value = entity.MatchSetMatchingSourceDataImport2ID;
            else
                parMatchSetMatchingSourceDataImport2ID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchSetMatchingSourceDataImport2ID);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (entity != null)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in MatchSetSubSetCombinationInfo object
        /// </summary>
        /// <param name="o">A MatchSetSubSetCombinationInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(MatchSetSubSetCombinationInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_MatchSetSubSetCombination");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (entity != null)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (entity != null)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (entity != null)
                parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (entity != null)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parMatchSetMatchingSourceDataImport1ID = cmd.CreateParameter();
            parMatchSetMatchingSourceDataImport1ID.ParameterName = "@MatchSetMatchingSourceDataImport1ID";
            if (entity != null)
                parMatchSetMatchingSourceDataImport1ID.Value = entity.MatchSetMatchingSourceDataImport1ID;
            else
                parMatchSetMatchingSourceDataImport1ID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchSetMatchingSourceDataImport1ID);

            System.Data.IDbDataParameter parMatchSetMatchingSourceDataImport2ID = cmd.CreateParameter();
            parMatchSetMatchingSourceDataImport2ID.ParameterName = "@MatchSetMatchingSourceDataImport2ID";
            if (entity != null)
                parMatchSetMatchingSourceDataImport2ID.Value = entity.MatchSetMatchingSourceDataImport2ID;
            else
                parMatchSetMatchingSourceDataImport2ID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchSetMatchingSourceDataImport2ID);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (entity != null)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter pkparMatchSetSubSetCombinationID = cmd.CreateParameter();
            pkparMatchSetSubSetCombinationID.ParameterName = "@MatchSetSubSetCombinationID";
            pkparMatchSetSubSetCombinationID.Value = entity.MatchSetSubSetCombinationID;
            cmdParams.Add(pkparMatchSetSubSetCombinationID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_MatchSetSubSetCombination");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchSetSubSetCombinationID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_MatchSetSubSetCombination");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchSetSubSetCombinationID";
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
        public IList<MatchSetSubSetCombinationInfo> SelectAllByMatchSetMatchingSourceDataImport1ID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_MatchSetSubSetCombinationByMatchSetMatchingSourceDataImport1ID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchSetMatchingSourceDataImport1ID";
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
        public IList<MatchSetSubSetCombinationInfo> SelectAllByMatchSetMatchingSourceDataImport2ID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_MatchSetSubSetCombinationByMatchSetMatchingSourceDataImport2ID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchSetMatchingSourceDataImport2ID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(MatchSetSubSetCombinationInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(MatchSetSubSetCombinationDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(MatchSetSubSetCombinationInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(MatchSetSubSetCombinationDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(MatchSetSubSetCombinationInfo entity, object id)
        {
            entity.MatchSetSubSetCombinationID = Convert.ToInt64(id);
        }










        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<MatchSetSubSetCombinationInfo> SelectMatchSetSubSetCombinationDetailsAssociatedToMatchingSourceColumnByMatchingConfiguration(MatchingSourceColumnInfo entity)
        {
            return this.SelectMatchSetSubSetCombinationDetailsAssociatedToMatchingSourceColumnByMatchingConfiguration(entity.MatchingSourceColumnID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<MatchSetSubSetCombinationInfo> SelectMatchSetSubSetCombinationDetailsAssociatedToMatchingSourceColumnByMatchingConfiguration(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [MatchSetSubSetCombination] INNER JOIN [MatchingConfiguration] ON [MatchSetSubSetCombination].[MatchSetSubSetCombinationID] = [MatchingConfiguration].[MatchSetSubSetCombinationID] INNER JOIN [MatchingSourceColumn] ON [MatchingConfiguration].[MatchingSource1ColumnID] = [MatchingSourceColumn].[MatchingSourceColumnID]  WHERE  [MatchingSourceColumn].[MatchingSourceColumnID] = @MatchingSourceColumnID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchingSourceColumnID";
            par.Value = id;

            cmdParams.Add(par);
            List<MatchSetSubSetCombinationInfo> objMatchSetSubSetCombinationEntityColl = new List<MatchSetSubSetCombinationInfo>(this.Select(cmd));
            return objMatchSetSubSetCombinationEntityColl;
        }


        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<MatchSetSubSetCombinationInfo> SelectMatchSetSubSetCombinationDetailsAssociatedToMatchSetResultByMatchSetResultWorkspace(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [MatchSetSubSetCombination] INNER JOIN [MatchSetResultWorkspace] ON [MatchSetSubSetCombination].[MatchSetSubSetCombinationID] = [MatchSetResultWorkspace].[MatchSetSubSetCombinationID] INNER JOIN [MatchSetResult] ON [MatchSetResultWorkspace].[MatchSetResultID] = [MatchSetResult].[MatchSetResultID]  WHERE  [MatchSetResult].[MatchSetResultID] = @MatchSetResultID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchSetResultID";
            par.Value = id;

            cmdParams.Add(par);
            List<MatchSetSubSetCombinationInfo> objMatchSetSubSetCombinationEntityColl = new List<MatchSetSubSetCombinationInfo>(this.Select(cmd));
            return objMatchSetSubSetCombinationEntityColl;
        }

    }
}
