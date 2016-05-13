

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

    public abstract class DynamicPlaceholderMstDAOBase : CustomAbstractDAO<DynamicPlaceholderMstInfo>
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
        /// A static representation of column DynamicPlaceholder
        /// </summary>
        public static readonly string COLUMN_DYNAMICPLACEHOLDER = "DynamicPlaceholder";
        /// <summary>
        /// A static representation of column DynamicPlaceholderID
        /// </summary>
        public static readonly string COLUMN_DYNAMICPLACEHOLDERID = "DynamicPlaceholderID";
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
        /// A static representation of column SortOrder
        /// </summary>
        public static readonly string COLUMN_SORTORDER = "SortOrder";
        /// <summary>
        /// Provides access to the name of the primary key column (DynamicPlaceholderID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "DynamicPlaceholderID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "DynamicPlaceholderMst";

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
        public DynamicPlaceholderMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "DynamicPlaceholderMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a DynamicPlaceholderMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>DynamicPlaceholderMstInfo</returns>
        protected override DynamicPlaceholderMstInfo MapObject(System.Data.IDataReader r)
        {

            DynamicPlaceholderMstInfo entity = new DynamicPlaceholderMstInfo();


            try
            {
                int ordinal = r.GetOrdinal("DynamicPlaceholderID");
                if (!r.IsDBNull(ordinal)) entity.DynamicPlaceholderID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("DynamicPlaceholder");
                if (!r.IsDBNull(ordinal)) entity.DynamicPlaceholder = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("DynamicPlaceholderTypeID");
                if (!r.IsDBNull(ordinal)) entity.DynamicPlaceholderTypeID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("SortOrder");
                if (!r.IsDBNull(ordinal)) entity.SortOrder = ((System.Int16)(r.GetValue(ordinal)));
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
        /// in DynamicPlaceholderMstInfo object
        /// </summary>
        /// <param name="o">A DynamicPlaceholderMstInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(DynamicPlaceholderMstInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_DynamicPlaceholderMst");
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

            System.Data.IDbDataParameter parDynamicPlaceholder = cmd.CreateParameter();
            parDynamicPlaceholder.ParameterName = "@DynamicPlaceholder";
            if (!entity.IsDynamicPlaceholderNull)
                parDynamicPlaceholder.Value = entity.DynamicPlaceholder;
            else
                parDynamicPlaceholder.Value = System.DBNull.Value;
            cmdParams.Add(parDynamicPlaceholder);

            System.Data.IDbDataParameter parDynamicPlaceholderID = cmd.CreateParameter();
            parDynamicPlaceholderID.ParameterName = "@DynamicPlaceholderID";
            if (!entity.IsDynamicPlaceholderIDNull)
                parDynamicPlaceholderID.Value = entity.DynamicPlaceholderID;
            else
                parDynamicPlaceholderID.Value = System.DBNull.Value;
            cmdParams.Add(parDynamicPlaceholderID);

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

            System.Data.IDbDataParameter parSortOrder = cmd.CreateParameter();
            parSortOrder.ParameterName = "@SortOrder";
            if (!entity.IsSortOrderNull)
                parSortOrder.Value = entity.SortOrder;
            else
                parSortOrder.Value = System.DBNull.Value;
            cmdParams.Add(parSortOrder);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in DynamicPlaceholderMstInfo object
        /// </summary>
        /// <param name="o">A DynamicPlaceholderMstInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(DynamicPlaceholderMstInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_DynamicPlaceholderMst");
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

            System.Data.IDbDataParameter parDynamicPlaceholder = cmd.CreateParameter();
            parDynamicPlaceholder.ParameterName = "@DynamicPlaceholder";
            if (!entity.IsDynamicPlaceholderNull)
                parDynamicPlaceholder.Value = entity.DynamicPlaceholder;
            else
                parDynamicPlaceholder.Value = System.DBNull.Value;
            cmdParams.Add(parDynamicPlaceholder);

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

            System.Data.IDbDataParameter parSortOrder = cmd.CreateParameter();
            parSortOrder.ParameterName = "@SortOrder";
            if (!entity.IsSortOrderNull)
                parSortOrder.Value = entity.SortOrder;
            else
                parSortOrder.Value = System.DBNull.Value;
            cmdParams.Add(parSortOrder);

            System.Data.IDbDataParameter pkparDynamicPlaceholderID = cmd.CreateParameter();
            pkparDynamicPlaceholderID.ParameterName = "@DynamicPlaceholderID";
            pkparDynamicPlaceholderID.Value = entity.DynamicPlaceholderID;
            cmdParams.Add(pkparDynamicPlaceholderID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_DynamicPlaceholderMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DynamicPlaceholderID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_DynamicPlaceholderMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DynamicPlaceholderID";
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
        public IList<DynamicPlaceholderMstInfo> SelectAllByDynamicPlaceholderTypeID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_DynamicPlaceholderMstByDynamicPlaceholderTypeID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DynamicPlaceholderTypeID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }








    }
}
