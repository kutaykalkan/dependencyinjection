

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO.Base
{

    public abstract class DynamicPlaceholderTypeMstDAOBase : CustomAbstractDAO<DynamicPlaceholderTypeMstInfo>
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
        /// A static representation of column DynamicPlaceholderType
        /// </summary>
        public static readonly string COLUMN_DYNAMICPLACEHOLDERTYPE = "DynamicPlaceholderType";
        /// <summary>
        /// A static representation of column DynamicPlaceholderTypeID
        /// </summary>
        public static readonly string COLUMN_DYNAMICPLACEHOLDERTYPEID = "DynamicPlaceholderTypeID";
        /// <summary>
        /// A static representation of column HostName
        /// </summary>
        public static readonly string COLUMN_HOSTNAME = "HostName";
        /// <summary>
        /// A static representation of column IsActive
        /// </summary>
        public static readonly string COLUMN_ISACTIVE = "IsActive";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// Provides access to the name of the primary key column (DynamicPlaceholderTypeID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "DynamicPlaceholderTypeID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "DynamicPlaceholderTypeMst";

        /// <summary>
        /// Provides access to the name of the database
        /// </summary>
        public static readonly string DATABASE_NAME = "SkyStemArt";

        /// <summary>
        ///  CurrentAppUserInfo  for further use
        /// </summary>
        public AppUserInfo CurrentAppUserInfo { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public DynamicPlaceholderTypeMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "DynamicPlaceholderTypeMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a DynamicPlaceholderTypeMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>DynamicPlaceholderTypeMstInfo</returns>
        protected override DynamicPlaceholderTypeMstInfo MapObject(System.Data.IDataReader r)
        {

            DynamicPlaceholderTypeMstInfo entity = new DynamicPlaceholderTypeMstInfo();


            try
            {
                int ordinal = r.GetOrdinal("DynamicPlaceholderTypeID");
                if (!r.IsDBNull(ordinal)) entity.DynamicPlaceholderTypeID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("DynamicPlaceholderType");
                if (!r.IsDBNull(ordinal)) entity.DynamicPlaceholderType = ((System.String)(r.GetValue(ordinal)));
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
        /// in DynamicPlaceholderTypeMstInfo object
        /// </summary>
        /// <param name="o">A DynamicPlaceholderTypeMstInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(DynamicPlaceholderTypeMstInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_DynamicPlaceholderTypeMst");
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
                parDateAdded.Value = entity.DateAdded.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (!entity.IsDateRevisedNull)
                parDateRevised.Value = entity.DateRevised.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parDynamicPlaceholderType = cmd.CreateParameter();
            parDynamicPlaceholderType.ParameterName = "@DynamicPlaceholderType";
            if (!entity.IsDynamicPlaceholderTypeNull)
                parDynamicPlaceholderType.Value = entity.DynamicPlaceholderType;
            else
                parDynamicPlaceholderType.Value = System.DBNull.Value;
            cmdParams.Add(parDynamicPlaceholderType);

            System.Data.IDbDataParameter parDynamicPlaceholderTypeID = cmd.CreateParameter();
            parDynamicPlaceholderTypeID.ParameterName = "@DynamicPlaceholderTypeID";
            if (!entity.IsDynamicPlaceholderTypeIDNull)
                parDynamicPlaceholderTypeID.Value = entity.DynamicPlaceholderTypeID;
            else
                parDynamicPlaceholderTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parDynamicPlaceholderTypeID);

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
        /// in DynamicPlaceholderTypeMstInfo object
        /// </summary>
        /// <param name="o">A DynamicPlaceholderTypeMstInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(DynamicPlaceholderTypeMstInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_DynamicPlaceholderTypeMst");
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
                parDateAdded.Value = entity.DateAdded.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (!entity.IsDateRevisedNull)
                parDateRevised.Value = entity.DateRevised.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parDynamicPlaceholderType = cmd.CreateParameter();
            parDynamicPlaceholderType.ParameterName = "@DynamicPlaceholderType";
            if (!entity.IsDynamicPlaceholderTypeNull)
                parDynamicPlaceholderType.Value = entity.DynamicPlaceholderType;
            else
                parDynamicPlaceholderType.Value = System.DBNull.Value;
            cmdParams.Add(parDynamicPlaceholderType);

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

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter pkparDynamicPlaceholderTypeID = cmd.CreateParameter();
            pkparDynamicPlaceholderTypeID.ParameterName = "@DynamicPlaceholderTypeID";
            pkparDynamicPlaceholderTypeID.Value = entity.DynamicPlaceholderTypeID;
            cmdParams.Add(pkparDynamicPlaceholderTypeID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_DynamicPlaceholderTypeMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DynamicPlaceholderTypeID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_DynamicPlaceholderTypeMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DynamicPlaceholderTypeID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }









    }
}
