

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

    public abstract class MatchSetResultDAOBase : CustomAbstractDAO<MatchSetResultInfo>
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
        /// A static representation of column MatchData
        /// </summary>
        public static readonly string COLUMN_MATCHDATA = "MatchData";
        /// <summary>
        /// A static representation of column MatchSetResultID
        /// </summary>
        public static readonly string COLUMN_MATCHSETRESULTID = "MatchSetResultID";
        /// <summary>
        /// A static representation of column MatchSetSubSetCombinationID
        /// </summary>
        public static readonly string COLUMN_MATCHSETSUBSETCOMBINATIONID = "MatchSetSubSetCombinationID";
        /// <summary>
        /// A static representation of column PartialMatchData
        /// </summary>
        public static readonly string COLUMN_PARTIALMATCHDATA = "PartialMatchData";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// A static representation of column UnmatchData
        /// </summary>
        public static readonly string COLUMN_UNMATCHDATA = "UnmatchData";
        /// <summary>
        /// Provides access to the name of the primary key column (MatchSetResultID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "MatchSetResultID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "MatchSetResult";

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
        public MatchSetResultDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "MatchSetResult", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a MatchSetResultInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>MatchSetResultInfo</returns>
        protected override MatchSetResultInfo MapObject(System.Data.IDataReader r)
        {

            MatchSetResultInfo entity = new MatchSetResultInfo();

            entity.MatchSetResultID = r.GetInt64Value("MatchSetResultID");
            entity.MatchSetSubSetCombinationID = r.GetInt64Value("MatchSetSubSetCombinationID");
            entity.MatchData = r.GetStringValue("MatchData");
            entity.PartialMatchData = r.GetStringValue("PartialMatchData");
            entity.UnmatchData = r.GetStringValue("UnmatchData");
            entity.IsActive = r.GetBooleanValue("IsActive");
            entity.DateAdded = r.GetDateValue("DateAdded");
            entity.AddedBy = r.GetStringValue("AddedBy");
            entity.DateRevised = r.GetDateValue("DateRevised");
            entity.RevisedBy = r.GetStringValue("RevisedBy");

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in MatchSetResultInfo object
        /// </summary>
        /// <param name="o">A MatchSetResultInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(MatchSetResultInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_MatchSetResult");
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

            System.Data.IDbDataParameter parMatchData = cmd.CreateParameter();
            parMatchData.ParameterName = "@MatchData";
            if (entity != null)
                parMatchData.Value = entity.MatchData;
            else
                parMatchData.Value = System.DBNull.Value;
            cmdParams.Add(parMatchData);

            System.Data.IDbDataParameter parMatchSetSubSetCombinationID = cmd.CreateParameter();
            parMatchSetSubSetCombinationID.ParameterName = "@MatchSetSubSetCombinationID";
            if (entity != null)
                parMatchSetSubSetCombinationID.Value = entity.MatchSetSubSetCombinationID;
            else
                parMatchSetSubSetCombinationID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchSetSubSetCombinationID);

            System.Data.IDbDataParameter parPartialMatchData = cmd.CreateParameter();
            parPartialMatchData.ParameterName = "@PartialMatchData";
            if (entity != null)
                parPartialMatchData.Value = entity.PartialMatchData;
            else
                parPartialMatchData.Value = System.DBNull.Value;
            cmdParams.Add(parPartialMatchData);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (entity != null)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parUnmatchData = cmd.CreateParameter();
            parUnmatchData.ParameterName = "@UnmatchData";
            if (entity != null)
                parUnmatchData.Value = entity.UnmatchData;
            else
                parUnmatchData.Value = System.DBNull.Value;
            cmdParams.Add(parUnmatchData);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in MatchSetResultInfo object
        /// </summary>
        /// <param name="o">A MatchSetResultInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(MatchSetResultInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_MatchSetResult");
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

            System.Data.IDbDataParameter parMatchData = cmd.CreateParameter();
            parMatchData.ParameterName = "@MatchData";
            if (entity != null)
                parMatchData.Value = entity.MatchData;
            else
                parMatchData.Value = System.DBNull.Value;
            cmdParams.Add(parMatchData);

            System.Data.IDbDataParameter parMatchSetSubSetCombinationID = cmd.CreateParameter();
            parMatchSetSubSetCombinationID.ParameterName = "@MatchSetSubSetCombinationID";
            if (entity != null)
                parMatchSetSubSetCombinationID.Value = entity.MatchSetSubSetCombinationID;
            else
                parMatchSetSubSetCombinationID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchSetSubSetCombinationID);

            System.Data.IDbDataParameter parPartialMatchData = cmd.CreateParameter();
            parPartialMatchData.ParameterName = "@PartialMatchData";
            if (entity != null)
                parPartialMatchData.Value = entity.PartialMatchData;
            else
                parPartialMatchData.Value = System.DBNull.Value;
            cmdParams.Add(parPartialMatchData);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (entity != null)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parUnmatchData = cmd.CreateParameter();
            parUnmatchData.ParameterName = "@UnmatchData";
            if (entity != null)
                parUnmatchData.Value = entity.UnmatchData;
            else
                parUnmatchData.Value = System.DBNull.Value;
            cmdParams.Add(parUnmatchData);

            System.Data.IDbDataParameter pkparMatchSetResultID = cmd.CreateParameter();
            pkparMatchSetResultID.ParameterName = "@MatchSetResultID";
            pkparMatchSetResultID.Value = entity.MatchSetResultID;
            cmdParams.Add(pkparMatchSetResultID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_MatchSetResult");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchSetResultID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_MatchSetResult");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchSetResultID";
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
        public IList<MatchSetResultInfo> SelectAllByMatchSetSubSetCombinationID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_MatchSetResultByMatchSetSubSetCombinationID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchSetSubSetCombinationID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(MatchSetResultInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(MatchSetResultDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(MatchSetResultInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(MatchSetResultDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(MatchSetResultInfo entity, object id)
        {
            entity.MatchSetResultID = Convert.ToInt64(id);
        }








        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<MatchSetResultInfo> SelectMatchSetResultDetailsAssociatedToMatchSetSubSetCombinationByMatchSetResultWorkspace(MatchSetSubSetCombinationInfo entity)
        {
            return this.SelectMatchSetResultDetailsAssociatedToMatchSetSubSetCombinationByMatchSetResultWorkspace(entity.MatchSetSubSetCombinationID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<MatchSetResultInfo> SelectMatchSetResultDetailsAssociatedToMatchSetSubSetCombinationByMatchSetResultWorkspace(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [MatchSetResult] INNER JOIN [MatchSetResultWorkspace] ON [MatchSetResult].[MatchSetResultID] = [MatchSetResultWorkspace].[MatchSetResultID] INNER JOIN [MatchSetSubSetCombination] ON [MatchSetResultWorkspace].[MatchSetSubSetCombinationID] = [MatchSetSubSetCombination].[MatchSetSubSetCombinationID]  WHERE  [MatchSetSubSetCombination].[MatchSetSubSetCombinationID] = @MatchSetSubSetCombinationID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchSetSubSetCombinationID";
            par.Value = id;

            cmdParams.Add(par);
            List<MatchSetResultInfo> objMatchSetResultEntityColl = new List<MatchSetResultInfo>(this.Select(cmd));
            return objMatchSetResultEntityColl;
        }

    }
}
