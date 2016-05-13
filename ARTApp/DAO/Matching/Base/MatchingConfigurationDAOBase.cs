

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

    public abstract class MatchingConfigurationDAOBase : CustomAbstractDAO<MatchingConfigurationInfo>
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
        /// A static representation of column DisplayColumnName
        /// </summary>
        public static readonly string COLUMN_DISPLAYCOLUMNNAME = "DisplayColumnName";
        /// <summary>
        /// A static representation of column IsActive
        /// </summary>
        public static readonly string COLUMN_ISACTIVE = "IsActive";
        /// <summary>
        /// A static representation of column IsMatching
        /// </summary>
        public static readonly string COLUMN_ISMATCHING = "IsMatching";
        /// <summary>
        /// A static representation of column IsPartialMatching
        /// </summary>
        public static readonly string COLUMN_ISPARTIALMATCHING = "IsPartialMatching";
        /// <summary>
        /// A static representation of column MachingConfigurationID
        /// </summary>
        public static readonly string COLUMN_MATCHINGCONFIGURATIONID = "MatchingConfigurationID";
        /// <summary>
        /// A static representation of column MatchingSource1ColumnID
        /// </summary>
        public static readonly string COLUMN_MATCHINGSOURCE1COLUMNID = "MatchingSource1ColumnID";
        /// <summary>
        /// A static representation of column MatchingSource2ColumnID
        /// </summary>
        public static readonly string COLUMN_MATCHINGSOURCE2COLUMNID = "MatchingSource2ColumnID";
        /// <summary>
        /// A static representation of column MatchSetSubSetCombinationID
        /// </summary>
        public static readonly string COLUMN_MATCHSETSUBSETCOMBINATIONID = "MatchSetSubSetCombinationID";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// Provides access to the name of the primary key column (MachingConfigurationID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "MachingConfigurationID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "MatchingConfiguration";

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
        public MatchingConfigurationDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "MatchingConfiguration", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a MatchingConfigurationInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>MatchingConfigurationInfo</returns>
        protected override MatchingConfigurationInfo MapObject(System.Data.IDataReader r)
        {

            MatchingConfigurationInfo entity = new MatchingConfigurationInfo();

            entity.MatchingConfigurationID = r.GetInt64Value("MatchingConfigurationID");
            entity.MatchSetSubSetCombinationID = r.GetInt64Value("MatchSetSubSetCombinationID");
            entity.MatchingSource1ColumnID = r.GetInt64Value("MatchingSource1ColumnID");
            entity.MatchingSource2ColumnID = r.GetInt64Value("MatchingSource2ColumnID");
            entity.IsMatching = r.GetBooleanValue("IsMatching");
            entity.IsPartialMatching = r.GetBooleanValue("IsPartialMatching");
            entity.DataTypeID = r.GetInt16Value("DataTypeID");
            entity.DisplayColumnName = r.GetStringValue("DisplayColumnName");
            entity.IsDisplayedColumn = r.GetBooleanValue("IsDisplayColumn");
            entity.IsActive = r.GetBooleanValue("IsActive");
            entity.DateAdded = r.GetDateValue("DateAdded");
            entity.AddedBy = r.GetStringValue("AddedBy");
            entity.DateRevised = r.GetDateValue("DateRevised");
            entity.RevisedBy = r.GetStringValue("RevisedBy");
            entity.IsAmountColumn = r.GetBooleanValue("IsAmountColumn");

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in MatchingConfigurationInfo object
        /// </summary>
        /// <param name="o">A MatchingConfigurationInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(MatchingConfigurationInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_MatchingConfiguration");
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

            System.Data.IDbDataParameter parDisplayColumnName = cmd.CreateParameter();
            parDisplayColumnName.ParameterName = "@DisplayColumnName";
            if (entity != null)
                parDisplayColumnName.Value = entity.DisplayColumnName;
            else
                parDisplayColumnName.Value = System.DBNull.Value;
            cmdParams.Add(parDisplayColumnName);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (entity != null)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parIsMatching = cmd.CreateParameter();
            parIsMatching.ParameterName = "@IsMatching";
            if (entity != null)
                parIsMatching.Value = entity.IsMatching;
            else
                parIsMatching.Value = System.DBNull.Value;
            cmdParams.Add(parIsMatching);

            System.Data.IDbDataParameter parIsPartialMatching = cmd.CreateParameter();
            parIsPartialMatching.ParameterName = "@IsPartialMatching";
            if (entity != null)
                parIsPartialMatching.Value = entity.IsPartialMatching;
            else
                parIsPartialMatching.Value = System.DBNull.Value;
            cmdParams.Add(parIsPartialMatching);

            System.Data.IDbDataParameter parMatchingSource1ColumnID = cmd.CreateParameter();
            parMatchingSource1ColumnID.ParameterName = "@MatchingSource1ColumnID";
            if (entity != null)
                parMatchingSource1ColumnID.Value = entity.MatchingSource1ColumnID;
            else
                parMatchingSource1ColumnID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchingSource1ColumnID);

            System.Data.IDbDataParameter parMatchingSource2ColumnID = cmd.CreateParameter();
            parMatchingSource2ColumnID.ParameterName = "@MatchingSource2ColumnID";
            if (entity != null)
                parMatchingSource2ColumnID.Value = entity.MatchingSource2ColumnID;
            else
                parMatchingSource2ColumnID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchingSource2ColumnID);

            System.Data.IDbDataParameter parMatchSetSubSetCombinationID = cmd.CreateParameter();
            parMatchSetSubSetCombinationID.ParameterName = "@MatchSetSubSetCombinationID";
            if (entity != null)
                parMatchSetSubSetCombinationID.Value = entity.MatchSetSubSetCombinationID;
            else
                parMatchSetSubSetCombinationID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchSetSubSetCombinationID);

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
        /// in MatchingConfigurationInfo object
        /// </summary>
        /// <param name="o">A MatchingConfigurationInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(MatchingConfigurationInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_MatchingConfiguration");
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

            System.Data.IDbDataParameter parDisplayColumnName = cmd.CreateParameter();
            parDisplayColumnName.ParameterName = "@DisplayColumnName";
            if (entity != null)
                parDisplayColumnName.Value = entity.DisplayColumnName;
            else
                parDisplayColumnName.Value = System.DBNull.Value;
            cmdParams.Add(parDisplayColumnName);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (entity != null)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parIsMatching = cmd.CreateParameter();
            parIsMatching.ParameterName = "@IsMatching";
            if (entity != null)
                parIsMatching.Value = entity.IsMatching;
            else
                parIsMatching.Value = System.DBNull.Value;
            cmdParams.Add(parIsMatching);

            System.Data.IDbDataParameter parIsPartialMatching = cmd.CreateParameter();
            parIsPartialMatching.ParameterName = "@IsPartialMatching";
            if (entity != null)
                parIsPartialMatching.Value = entity.IsPartialMatching;
            else
                parIsPartialMatching.Value = System.DBNull.Value;
            cmdParams.Add(parIsPartialMatching);

            System.Data.IDbDataParameter parMatchingSource1ColumnID = cmd.CreateParameter();
            parMatchingSource1ColumnID.ParameterName = "@MatchingSource1ColumnID";
            if (entity != null)
                parMatchingSource1ColumnID.Value = entity.MatchingSource1ColumnID;
            else
                parMatchingSource1ColumnID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchingSource1ColumnID);

            System.Data.IDbDataParameter parMatchingSource2ColumnID = cmd.CreateParameter();
            parMatchingSource2ColumnID.ParameterName = "@MatchingSource2ColumnID";
            if (entity != null)
                parMatchingSource2ColumnID.Value = entity.MatchingSource2ColumnID;
            else
                parMatchingSource2ColumnID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchingSource2ColumnID);

            System.Data.IDbDataParameter parMatchSetSubSetCombinationID = cmd.CreateParameter();
            parMatchSetSubSetCombinationID.ParameterName = "@MatchSetSubSetCombinationID";
            if (entity != null)
                parMatchSetSubSetCombinationID.Value = entity.MatchSetSubSetCombinationID;
            else
                parMatchSetSubSetCombinationID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchSetSubSetCombinationID);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (entity != null)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter pkparMachingConfigurationID = cmd.CreateParameter();
            pkparMachingConfigurationID.ParameterName = "@MatchingConfigurationID";
            pkparMachingConfigurationID.Value = entity.MatchingConfigurationID;
            cmdParams.Add(pkparMachingConfigurationID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_MatchingConfiguration");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MachingConfigurationID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_MatchingConfiguration");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MachingConfigurationID";
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
        public IList<MatchingConfigurationInfo> SelectAllByMatchSetSubSetCombinationID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_MatchingConfigurationByMatchSetSubSetCombinationID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchSetSubSetCombinationID";
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
        public IList<MatchingConfigurationInfo> SelectAllByMatchingSource1ColumnID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_MatchingConfigurationByMatchingSource1ColumnID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchingSource1ColumnID";
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
        public IList<MatchingConfigurationInfo> SelectAllByMatchingSource2ColumnID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_MatchingConfigurationByMatchingSource2ColumnID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchingSource2ColumnID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(MatchingConfigurationInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(MatchingConfigurationDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(MatchingConfigurationInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(MatchingConfigurationDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(MatchingConfigurationInfo entity, object id)
        {
            entity.MatchingConfigurationID = Convert.ToInt64(id);
        }








        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<MatchingConfigurationInfo> SelectMatchingConfigurationDetailsAssociatedToRecItemColumnMstByMatchingConfigurationRecItemColumn(RecItemColumnMstInfo entity)
        {
            return this.SelectMatchingConfigurationDetailsAssociatedToRecItemColumnMstByMatchingConfigurationRecItemColumn(entity.RecItemColumnID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<MatchingConfigurationInfo> SelectMatchingConfigurationDetailsAssociatedToRecItemColumnMstByMatchingConfigurationRecItemColumn(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [MatchingConfiguration] INNER JOIN [MatchingConfigurationRecItemColumn] ON [MatchingConfiguration].[MachingConfigurationID] = [MatchingConfigurationRecItemColumn].[MatchingConfigurationID] INNER JOIN [RecItemColumnMst] ON [MatchingConfigurationRecItemColumn].[RecItemColumnID] = [RecItemColumnMst].[RecItemColumnID]  WHERE  [RecItemColumnMst].[RecItemColumnID] = @RecItemColumnID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RecItemColumnID";
            par.Value = id;

            cmdParams.Add(par);
            List<MatchingConfigurationInfo> objMatchingConfigurationEntityColl = new List<MatchingConfigurationInfo>(this.Select(cmd));
            return objMatchingConfigurationEntityColl;
        }

    }
}
