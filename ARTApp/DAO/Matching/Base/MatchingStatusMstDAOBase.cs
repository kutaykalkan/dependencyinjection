

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Model.Matching;
using SkyStem.ART.App.DAO.Base;

namespace SkyStem.ART.App.DAO.Matching.Base
{

    public abstract class MatchingStatusMstDAOBase : CustomAbstractDAO<MatchingStatusMstInfo>
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
        /// A static representation of column Description
        /// </summary>
        public static readonly string COLUMN_DESCRIPTION = "Description";
        /// <summary>
        /// A static representation of column HostName
        /// </summary>
        public static readonly string COLUMN_HOSTNAME = "HostName";
        /// <summary>
        /// A static representation of column IsActive
        /// </summary>
        public static readonly string COLUMN_ISACTIVE = "IsActive";
        /// <summary>
        /// A static representation of column MatchingStatus
        /// </summary>
        public static readonly string COLUMN_MATCHINGSTATUS = "MatchingStatus";
        /// <summary>
        /// A static representation of column MatchingStatusID
        /// </summary>
        public static readonly string COLUMN_MATCHINGSTATUSID = "MatchingStatusID";
        /// <summary>
        /// A static representation of column MatchingStatusLabelID
        /// </summary>
        public static readonly string COLUMN_MATCHINGSTATUSLABELID = "MatchingStatusLabelID";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// Provides access to the name of the primary key column (MatchingStatusID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "MatchingStatusID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "MatchingStatusMst";

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
        public MatchingStatusMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "MatchingStatusMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a MatchingStatusMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>MatchingStatusMstInfo</returns>
        protected override MatchingStatusMstInfo MapObject(System.Data.IDataReader r)
        {

            MatchingStatusMstInfo entity = new MatchingStatusMstInfo();


            try
            {
                int ordinal = r.GetOrdinal("MatchingStatusID");
                if (!r.IsDBNull(ordinal)) entity.MatchingStatusID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("MatchingStatus");
                if (!r.IsDBNull(ordinal)) entity.MatchingStatus = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("MatchingStatusLabelID");
                if (!r.IsDBNull(ordinal)) entity.MatchingStatusLabelID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("Description");
                if (!r.IsDBNull(ordinal)) entity.Description = ((System.String)(r.GetValue(ordinal)));
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

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in MatchingStatusMstInfo object
        /// </summary>
        /// <param name="o">A MatchingStatusMstInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(MatchingStatusMstInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_MatchingStatusMst");
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

            System.Data.IDbDataParameter parDescription = cmd.CreateParameter();
            parDescription.ParameterName = "@Description";
            if (!entity.IsDescriptionNull)
                parDescription.Value = entity.Description;
            else
                parDescription.Value = System.DBNull.Value;
            cmdParams.Add(parDescription);

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

            System.Data.IDbDataParameter parMatchingStatus = cmd.CreateParameter();
            parMatchingStatus.ParameterName = "@MatchingStatus";
            if (!entity.IsMatchingStatusNull)
                parMatchingStatus.Value = entity.MatchingStatus;
            else
                parMatchingStatus.Value = System.DBNull.Value;
            cmdParams.Add(parMatchingStatus);

            System.Data.IDbDataParameter parMatchingStatusID = cmd.CreateParameter();
            parMatchingStatusID.ParameterName = "@MatchingStatusID";
            if (!entity.IsMatchingStatusIDNull)
                parMatchingStatusID.Value = entity.MatchingStatusID;
            else
                parMatchingStatusID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchingStatusID);

            System.Data.IDbDataParameter parMatchingStatusLabelID = cmd.CreateParameter();
            parMatchingStatusLabelID.ParameterName = "@MatchingStatusLabelID";
            if (!entity.IsMatchingStatusLabelIDNull)
                parMatchingStatusLabelID.Value = entity.MatchingStatusLabelID;
            else
                parMatchingStatusLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchingStatusLabelID);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in MatchingStatusMstInfo object
        /// </summary>
        /// <param name="o">A MatchingStatusMstInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(MatchingStatusMstInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_MatchingStatusMst");
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

            System.Data.IDbDataParameter parDescription = cmd.CreateParameter();
            parDescription.ParameterName = "@Description";
            if (!entity.IsDescriptionNull)
                parDescription.Value = entity.Description;
            else
                parDescription.Value = System.DBNull.Value;
            cmdParams.Add(parDescription);

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

            System.Data.IDbDataParameter parMatchingStatus = cmd.CreateParameter();
            parMatchingStatus.ParameterName = "@MatchingStatus";
            if (!entity.IsMatchingStatusNull)
                parMatchingStatus.Value = entity.MatchingStatus;
            else
                parMatchingStatus.Value = System.DBNull.Value;
            cmdParams.Add(parMatchingStatus);

            System.Data.IDbDataParameter parMatchingStatusLabelID = cmd.CreateParameter();
            parMatchingStatusLabelID.ParameterName = "@MatchingStatusLabelID";
            if (!entity.IsMatchingStatusLabelIDNull)
                parMatchingStatusLabelID.Value = entity.MatchingStatusLabelID;
            else
                parMatchingStatusLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchingStatusLabelID);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter pkparMatchingStatusID = cmd.CreateParameter();
            pkparMatchingStatusID.ParameterName = "@MatchingStatusID";
            pkparMatchingStatusID.Value = entity.MatchingStatusID;
            cmdParams.Add(pkparMatchingStatusID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_MatchingStatusMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchingStatusID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_MatchingStatusMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchingStatusID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }












        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<MatchingStatusMstInfo> SelectMatchingStatusMstDetailsAssociatedToMatchingTypeMstByMatchSetHdr(MatchingTypeMstInfo entity)
        {
            return this.SelectMatchingStatusMstDetailsAssociatedToMatchingTypeMstByMatchSetHdr(entity.MatchingTypeID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<MatchingStatusMstInfo> SelectMatchingStatusMstDetailsAssociatedToMatchingTypeMstByMatchSetHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [MatchingStatusMst] INNER JOIN [MatchSetHdr] ON [MatchingStatusMst].[MatchingStatusID] = [MatchSetHdr].[MatchingStatusID] INNER JOIN [MatchingTypeMst] ON [MatchSetHdr].[MatchingTypeID] = [MatchingTypeMst].[MatchingTypeID]  WHERE  [MatchingTypeMst].[MatchingTypeID] = @MatchingTypeID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchingTypeID";
            par.Value = id;

            cmdParams.Add(par);
            List<MatchingStatusMstInfo> objMatchingStatusMstEntityColl = new List<MatchingStatusMstInfo>(this.Select(cmd));
            return objMatchingStatusMstEntityColl;
        }

    }
}
