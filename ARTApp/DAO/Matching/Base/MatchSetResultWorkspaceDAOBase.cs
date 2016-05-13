

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

    public abstract class MatchSetResultWorkspaceDAOBase : CustomAbstractDAO<MatchSetResultWorkspaceInfo>
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
        /// A static representation of column MatchSetResultWorkspaceID
        /// </summary>
        public static readonly string COLUMN_MATCHSETRESULTWORKSPACEID = "MatchSetResultWorkspaceID";
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
        /// A static representation of column WorkspaceMatchData
        /// </summary>
        public static readonly string COLUMN_WORKSPACEMATCHDATA = "WorkspaceMatchData";
        /// <summary>
        /// A static representation of column WorkspacePartialMatchData
        /// </summary>
        public static readonly string COLUMN_WORKSPACEPARTIALMATCHDATA = "WorkspacePartialMatchData";
        /// <summary>
        /// A static representation of column WorkspaceUnmatchData
        /// </summary>
        public static readonly string COLUMN_WORKSPACEUNMATCHDATA = "WorkspaceUnmatchData";
        /// <summary>
        /// Provides access to the name of the primary key column (MatchSetResultWorkspaceID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "MatchSetResultWorkspaceID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "MatchSetResultWorkspace";

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
        public MatchSetResultWorkspaceDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "MatchSetResultWorkspace", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a MatchSetResultWorkspaceInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>MatchSetResultWorkspaceInfo</returns>
        protected override MatchSetResultWorkspaceInfo MapObject(System.Data.IDataReader r)
        {

            MatchSetResultWorkspaceInfo entity = new MatchSetResultWorkspaceInfo();

            entity.MatchSetResultWorkspaceID = r.GetInt64Value("MatchSetResultWorkspaceID");
            entity.MatchSetResultID = r.GetInt64Value("MatchSetResultID");
            entity.MatchSetSubSetCombinationID = r.GetInt64Value("MatchSetSubSetCombinationID");
            entity.MatchData = r.GetStringValue("MatchData");
            entity.PartialMatchData = r.GetStringValue("PartialMatchData");
            entity.UnmatchData = r.GetStringValue("UnmatchData");
            entity.WorkspaceMatchData = r.GetStringValue("WorkspaceMatchData");
            entity.WorkspacePartialMatchData = r.GetStringValue("WorkspacePartialMatchData");
            entity.WorkspaceUnmatchData = r.GetStringValue("WorkspaceUnmatchData");
            entity.IsActive = r.GetBooleanValue("IsActive");
            entity.DateAdded = r.GetDateValue("DateAdded");
            entity.AddedBy = r.GetStringValue("AddedBy");
            entity.DateRevised = r.GetDateValue("DateRevised");
            entity.RevisedBy = r.GetStringValue("RevisedBy");

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in MatchSetResultWorkspaceInfo object
        /// </summary>
        /// <param name="o">A MatchSetResultWorkspaceInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(MatchSetResultWorkspaceInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_MatchSetResultWorkspace");
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

            System.Data.IDbDataParameter parMatchSetResultID = cmd.CreateParameter();
            parMatchSetResultID.ParameterName = "@MatchSetResultID";
            if (entity != null)
                parMatchSetResultID.Value = entity.MatchSetResultID;
            else
                parMatchSetResultID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchSetResultID);

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

            System.Data.IDbDataParameter parWorkspaceMatchData = cmd.CreateParameter();
            parWorkspaceMatchData.ParameterName = "@WorkspaceMatchData";
            if (entity != null)
                parWorkspaceMatchData.Value = entity.WorkspaceMatchData;
            else
                parWorkspaceMatchData.Value = System.DBNull.Value;
            cmdParams.Add(parWorkspaceMatchData);

            System.Data.IDbDataParameter parWorkspacePartialMatchData = cmd.CreateParameter();
            parWorkspacePartialMatchData.ParameterName = "@WorkspacePartialMatchData";
            if (entity != null)
                parWorkspacePartialMatchData.Value = entity.WorkspacePartialMatchData;
            else
                parWorkspacePartialMatchData.Value = System.DBNull.Value;
            cmdParams.Add(parWorkspacePartialMatchData);

            System.Data.IDbDataParameter parWorkspaceUnmatchData = cmd.CreateParameter();
            parWorkspaceUnmatchData.ParameterName = "@WorkspaceUnmatchData";
            if (entity != null)
                parWorkspaceUnmatchData.Value = entity.WorkspaceUnmatchData;
            else
                parWorkspaceUnmatchData.Value = System.DBNull.Value;
            cmdParams.Add(parWorkspaceUnmatchData);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in MatchSetResultWorkspaceInfo object
        /// </summary>
        /// <param name="o">A MatchSetResultWorkspaceInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(MatchSetResultWorkspaceInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_MatchSetResultWorkspace");
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

            System.Data.IDbDataParameter parMatchSetResultID = cmd.CreateParameter();
            parMatchSetResultID.ParameterName = "@MatchSetResultID";
            if (entity != null)
                parMatchSetResultID.Value = entity.MatchSetResultID;
            else
                parMatchSetResultID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchSetResultID);

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

            System.Data.IDbDataParameter parWorkspaceMatchData = cmd.CreateParameter();
            parWorkspaceMatchData.ParameterName = "@WorkspaceMatchData";
            if (entity != null)
                parWorkspaceMatchData.Value = entity.WorkspaceMatchData;
            else
                parWorkspaceMatchData.Value = System.DBNull.Value;
            cmdParams.Add(parWorkspaceMatchData);

            System.Data.IDbDataParameter parWorkspacePartialMatchData = cmd.CreateParameter();
            parWorkspacePartialMatchData.ParameterName = "@WorkspacePartialMatchData";
            if (entity != null)
                parWorkspacePartialMatchData.Value = entity.WorkspacePartialMatchData;
            else
                parWorkspacePartialMatchData.Value = System.DBNull.Value;
            cmdParams.Add(parWorkspacePartialMatchData);

            System.Data.IDbDataParameter parWorkspaceUnmatchData = cmd.CreateParameter();
            parWorkspaceUnmatchData.ParameterName = "@WorkspaceUnmatchData";
            if (entity != null)
                parWorkspaceUnmatchData.Value = entity.WorkspaceUnmatchData;
            else
                parWorkspaceUnmatchData.Value = System.DBNull.Value;
            cmdParams.Add(parWorkspaceUnmatchData);

            System.Data.IDbDataParameter pkparMatchSetResulWorkspaceID = cmd.CreateParameter();
            pkparMatchSetResulWorkspaceID.ParameterName = "@MatchSetResultWorkspaceID";
            pkparMatchSetResulWorkspaceID.Value = entity.MatchSetResultWorkspaceID;
            cmdParams.Add(pkparMatchSetResulWorkspaceID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_MatchSetResultWorkspace");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchSetResultWorkspaceID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_MatchSetResultWorkspace");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchSetResultWorkspaceID";
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
        public IList<MatchSetResultWorkspaceInfo> SelectAllByMatchSetResultID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_MatchSetResultWorkspaceByMatchSetResultID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchSetResultID";
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
        public IList<MatchSetResultWorkspaceInfo> SelectAllByMatchSetSubSetCombinationID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_MatchSetResultWorkspaceByMatchSetSubSetCombinationID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchSetSubSetCombinationID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(MatchSetResultWorkspaceInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(MatchSetResultWorkspaceDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(MatchSetResultWorkspaceInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(MatchSetResultWorkspaceDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(MatchSetResultWorkspaceInfo entity, object id)
        {
            entity.MatchSetResultWorkspaceID = Convert.ToInt64(id);
        }




    }
}
