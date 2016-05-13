

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

    public abstract class MatchSetHdrDAOBase : CustomAbstractDAO<MatchSetHdrInfo>
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
        /// A static representation of column MatchingStatusID
        /// </summary>
        public static readonly string COLUMN_MATCHINGSTATUSID = "MatchingStatusID";
        /// <summary>
        /// A static representation of column MatchingTypeID
        /// </summary>
        public static readonly string COLUMN_MATCHINGTYPEID = "MatchingTypeID";
        /// <summary>
        /// A static representation of column MatchSetDescription
        /// </summary>
        public static readonly string COLUMN_MATCHSETDESCRIPTION = "MatchSetDescription";
        /// <summary>
        /// A static representation of column MatchSetID
        /// </summary>
        public static readonly string COLUMN_MATCHSETID = "MatchSetID";
        /// <summary>
        /// A static representation of column MatchSetName
        /// </summary>
        public static readonly string COLUMN_MATCHSETNAME = "MatchSetName";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// Provides access to the name of the primary key column (MatchSetID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "MatchSetID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "MatchSetHdr";

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
        public MatchSetHdrDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "MatchSetHdr", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a MatchSetHdrInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>MatchSetHdrInfo</returns>
        protected override MatchSetHdrInfo MapObject(System.Data.IDataReader r)
        {

            MatchSetHdrInfo entity = new MatchSetHdrInfo();


            try
            {
                int ordinal = r.GetOrdinal("MatchSetID");
                if (!r.IsDBNull(ordinal)) entity.MatchSetID = ((System.Int64)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("MatchSetName");
                if (!r.IsDBNull(ordinal)) entity.MatchSetName = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("MatchSetDescription");
                if (!r.IsDBNull(ordinal)) entity.MatchSetDescription = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("MatchingTypeID");
                if (!r.IsDBNull(ordinal)) entity.MatchingTypeID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("MatchingStatusID");
                if (!r.IsDBNull(ordinal)) entity.MatchingStatusID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }


            try
            {
                int ordinal = r.GetOrdinal("GLDataID");
                if (!r.IsDBNull(ordinal)) entity.GLDataID = ((System.Int64)(r.GetValue(ordinal)));
            }
            catch (Exception) { }


            try
            {
                int ordinal = r.GetOrdinal("RecPeriodID");
                if (!r.IsDBNull(ordinal)) entity.RecPeriodID = ((System.Int32)(r.GetValue(ordinal)));
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
                int ordinal = r.GetOrdinal("AddedByUserID");
                if (!r.IsDBNull(ordinal)) entity.AddedByUserID = ((System.Int32)(r.GetValue(ordinal)));
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
                int ordinal = r.GetOrdinal("MatchSetRef");
                if (!r.IsDBNull(ordinal)) entity.MatchSetRef = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("RoleID");
                if (!r.IsDBNull(ordinal)) entity.RoleID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("Message");
                if (!r.IsDBNull(ordinal)) entity.Message = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in MatchSetHdrInfo object
        /// </summary>
        /// <param name="o">A MatchSetHdrInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(MatchSetHdrInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_MatchSetHdr");
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

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (!entity.IsIsActiveNull)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parMatchingStatusID = cmd.CreateParameter();
            parMatchingStatusID.ParameterName = "@MatchingStatusID";
            if (!entity.IsMatchingStatusIDNull)
                parMatchingStatusID.Value = entity.MatchingStatusID;
            else
                parMatchingStatusID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchingStatusID);

            System.Data.IDbDataParameter parMatchingTypeID = cmd.CreateParameter();
            parMatchingTypeID.ParameterName = "@MatchingTypeID";
            if (!entity.IsMatchingTypeIDNull)
                parMatchingTypeID.Value = entity.MatchingTypeID;
            else
                parMatchingTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchingTypeID);

            System.Data.IDbDataParameter parMatchSetDescription = cmd.CreateParameter();
            parMatchSetDescription.ParameterName = "@MatchSetDescription";
            if (!entity.IsMatchSetDescriptionNull)
                parMatchSetDescription.Value = entity.MatchSetDescription;
            else
                parMatchSetDescription.Value = System.DBNull.Value;
            cmdParams.Add(parMatchSetDescription);

            System.Data.IDbDataParameter parMatchSetID = cmd.CreateParameter();
            parMatchSetID.ParameterName = "@MatchSetID";
            if (!entity.IsMatchSetIDNull)
                parMatchSetID.Value = entity.MatchSetID;
            else
                parMatchSetID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchSetID);

            System.Data.IDbDataParameter parMatchSetName = cmd.CreateParameter();
            parMatchSetName.ParameterName = "@MatchSetName";
            if (!entity.IsMatchSetNameNull)
                parMatchSetName.Value = entity.MatchSetName;
            else
                parMatchSetName.Value = System.DBNull.Value;
            cmdParams.Add(parMatchSetName);

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
        /// in MatchSetHdrInfo object
        /// </summary>
        /// <param name="o">A MatchSetHdrInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(MatchSetHdrInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_MatchSetHdr");
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

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (!entity.IsIsActiveNull)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parMatchingStatusID = cmd.CreateParameter();
            parMatchingStatusID.ParameterName = "@MatchingStatusID";
            if (!entity.IsMatchingStatusIDNull)
                parMatchingStatusID.Value = entity.MatchingStatusID;
            else
                parMatchingStatusID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchingStatusID);

            System.Data.IDbDataParameter parMatchingTypeID = cmd.CreateParameter();
            parMatchingTypeID.ParameterName = "@MatchingTypeID";
            if (!entity.IsMatchingTypeIDNull)
                parMatchingTypeID.Value = entity.MatchingTypeID;
            else
                parMatchingTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchingTypeID);

            System.Data.IDbDataParameter parMatchSetDescription = cmd.CreateParameter();
            parMatchSetDescription.ParameterName = "@MatchSetDescription";
            if (!entity.IsMatchSetDescriptionNull)
                parMatchSetDescription.Value = entity.MatchSetDescription;
            else
                parMatchSetDescription.Value = System.DBNull.Value;
            cmdParams.Add(parMatchSetDescription);

            System.Data.IDbDataParameter parMatchSetName = cmd.CreateParameter();
            parMatchSetName.ParameterName = "@MatchSetName";
            if (!entity.IsMatchSetNameNull)
                parMatchSetName.Value = entity.MatchSetName;
            else
                parMatchSetName.Value = System.DBNull.Value;
            cmdParams.Add(parMatchSetName);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter pkparMatchSetID = cmd.CreateParameter();
            pkparMatchSetID.ParameterName = "@MatchSetID";
            pkparMatchSetID.Value = entity.MatchSetID;
            cmdParams.Add(pkparMatchSetID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_MatchSetHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchSetID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_MatchSetHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchSetID";
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
        public IList<MatchSetHdrInfo> SelectAllByMatchingTypeID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_MatchSetHdrByMatchingTypeID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchingTypeID";
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
        public IList<MatchSetHdrInfo> SelectAllByMatchingStatusID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_MatchSetHdrByMatchingStatusID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchingStatusID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }








    }
}
