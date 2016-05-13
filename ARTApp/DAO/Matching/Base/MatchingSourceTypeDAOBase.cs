

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

    public abstract class MatchingSourceTypeDAOBase : CustomAbstractDAO<MatchingSourceTypeInfo>
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
        /// A static representation of column HostName
        /// </summary>
        public static readonly string COLUMN_HOSTNAME = "HostName";
        /// <summary>
        /// A static representation of column IsActive
        /// </summary>
        public static readonly string COLUMN_ISACTIVE = "IsActive";
        /// <summary>
        /// A static representation of column MatchingSourceTypeID
        /// </summary>
        public static readonly string COLUMN_MATCHINGSOURCETYPEID = "MatchingSourceTypeID";
        /// <summary>
        /// A static representation of column MatchingSourceTypeName
        /// </summary>
        public static readonly string COLUMN_MATCHINGSOURCETYPENAME = "MatchingSourceTypeName";
        /// <summary>
        /// A static representation of column MatchingSourceTypeNameLabelID
        /// </summary>
        public static readonly string COLUMN_MATCHINGSOURCETYPENAMELABELID = "MatchingSourceTypeNameLabelID";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// Provides access to the name of the primary key column (MatchingSourceTypeID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "MatchingSourceTypeID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "MatchingSourceType";

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
        public MatchingSourceTypeDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "MatchingSourceType", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a MatchingSourceTypeInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>MatchingSourceTypeInfo</returns>
        protected override MatchingSourceTypeInfo MapObject(System.Data.IDataReader r)
        {

            MatchingSourceTypeInfo entity = new MatchingSourceTypeInfo();

            entity.MatchingSourceTypeID = r.GetInt16Value("MatchingSourceTypeID");
            entity.MatchingSourceTypeName = r.GetStringValue("MatchingSourceTypeName");
            entity.MatchingSourceTypeNameLabelID = r.GetInt32Value("MatchingSourceTypeNameLabelID");
            entity.IsActive = r.GetBooleanValue("IsActive");
            entity.DateAdded = r.GetDateValue("DateAdded");
            entity.AddedBy = r.GetStringValue("AddedBy");
            entity.DateRevised = r.GetDateValue("DateRevised");
            entity.RevisedBy = r.GetStringValue("RevisedBy");
            entity.HostName = r.GetStringValue("HostName");

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in MatchingSourceTypeInfo object
        /// </summary>
        /// <param name="o">A MatchingSourceTypeInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(MatchingSourceTypeInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_MatchingSourceType");
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

            System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            parHostName.ParameterName = "@HostName";
            if (entity != null)
                parHostName.Value = entity.HostName;
            else
                parHostName.Value = System.DBNull.Value;
            cmdParams.Add(parHostName);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (entity != null)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parMatchingSourceTypeID = cmd.CreateParameter();
            parMatchingSourceTypeID.ParameterName = "@MatchingSourceTypeID";
            if (entity != null)
                parMatchingSourceTypeID.Value = entity.MatchingSourceTypeID;
            else
                parMatchingSourceTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchingSourceTypeID);

            System.Data.IDbDataParameter parMatchingSourceTypeName = cmd.CreateParameter();
            parMatchingSourceTypeName.ParameterName = "@MatchingSourceTypeName";
            if (entity != null)
                parMatchingSourceTypeName.Value = entity.MatchingSourceTypeName;
            else
                parMatchingSourceTypeName.Value = System.DBNull.Value;
            cmdParams.Add(parMatchingSourceTypeName);

            System.Data.IDbDataParameter parMatchingSourceTypeNameLabelID = cmd.CreateParameter();
            parMatchingSourceTypeNameLabelID.ParameterName = "@MatchingSourceTypeNameLabelID";
            if (entity != null)
                parMatchingSourceTypeNameLabelID.Value = entity.MatchingSourceTypeNameLabelID;
            else
                parMatchingSourceTypeNameLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchingSourceTypeNameLabelID);

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
        /// in MatchingSourceTypeInfo object
        /// </summary>
        /// <param name="o">A MatchingSourceTypeInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(MatchingSourceTypeInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_MatchingSourceType");
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

            System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            parHostName.ParameterName = "@HostName";
            if (entity != null)
                parHostName.Value = entity.HostName;
            else
                parHostName.Value = System.DBNull.Value;
            cmdParams.Add(parHostName);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (entity != null)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parMatchingSourceTypeName = cmd.CreateParameter();
            parMatchingSourceTypeName.ParameterName = "@MatchingSourceTypeName";
            if (entity != null)
                parMatchingSourceTypeName.Value = entity.MatchingSourceTypeName;
            else
                parMatchingSourceTypeName.Value = System.DBNull.Value;
            cmdParams.Add(parMatchingSourceTypeName);

            System.Data.IDbDataParameter parMatchingSourceTypeNameLabelID = cmd.CreateParameter();
            parMatchingSourceTypeNameLabelID.ParameterName = "@MatchingSourceTypeNameLabelID";
            if (entity != null)
                parMatchingSourceTypeNameLabelID.Value = entity.MatchingSourceTypeNameLabelID;
            else
                parMatchingSourceTypeNameLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parMatchingSourceTypeNameLabelID);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (entity != null)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter pkparMatchingSourceTypeID = cmd.CreateParameter();
            pkparMatchingSourceTypeID.ParameterName = "@MatchingSourceTypeID";
            pkparMatchingSourceTypeID.Value = entity.MatchingSourceTypeID;
            cmdParams.Add(pkparMatchingSourceTypeID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_MatchingSourceType");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchingSourceTypeID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_MatchingSourceType");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchingSourceTypeID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }














    }
}
