/******************************************
 * Auto-generated by Adapdev Codus v1.4.0 - Trial Use Only
 * 
 ******************************************/
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

    /// <summary>
    /// Base Data Access Object for the ReasonMst table.
    /// </summary>
    public abstract class ReasonMstDAOBase : CustomAbstractDAO<ReasonMstInfo>
    {

        /// <summary>
        /// A static representation of column Description
        /// </summary>
        public static readonly string COLUMN_DESCRIPTION = "Description";
        /// <summary>
        /// A static representation of column IsActive
        /// </summary>
        public static readonly string COLUMN_ISACTIVE = "IsActive";
        /// <summary>
        /// A static representation of column Reason
        /// </summary>
        public static readonly string COLUMN_REASON = "Reason";
        /// <summary>
        /// A static representation of column ReasonID
        /// </summary>
        public static readonly string COLUMN_REASONID = "ReasonID";
        /// <summary>
        /// A static representation of column ReasonLabelID
        /// </summary>
        public static readonly string COLUMN_REASONLABELID = "ReasonLabelID";
        /// <summary>
        /// Provides access to the name of the primary key column (ReasonID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "ReasonID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "ReasonMst";

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
        public ReasonMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "ReasonMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a ReasonMstEntity object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>ReasonMstEntity</returns>
        protected override ReasonMstInfo MapObject(System.Data.IDataReader r)
        {

            ReasonMstInfo entity = new ReasonMstInfo();


            try
            {
                int ordinal = r.GetOrdinal("ReasonID");
                if (!r.IsDBNull(ordinal)) entity.ReasonID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("Reason");
                if (!r.IsDBNull(ordinal)) entity.Reason = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ReasonLabelID");
                if (!r.IsDBNull(ordinal)) entity.ReasonLabelID = ((System.Int32)(r.GetValue(ordinal)));
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

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in ReasonMstEntity object
        /// </summary>
        /// <param name="o">A ReasonMstEntity object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(ReasonMstInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_ReasonMstInsert");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parDescription = cmd.CreateParameter();
            parDescription.ParameterName = "@Description";
            if (!entity.IsDescriptionNull)
                parDescription.Value = entity.Description;
            else
                parDescription.Value = System.DBNull.Value;
            cmdParams.Add(parDescription);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (!entity.IsIsActiveNull)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parReason = cmd.CreateParameter();
            parReason.ParameterName = "@Reason";
            if (!entity.IsReasonNull)
                parReason.Value = entity.Reason;
            else
                parReason.Value = System.DBNull.Value;
            cmdParams.Add(parReason);

            System.Data.IDbDataParameter parReasonLabelID = cmd.CreateParameter();
            parReasonLabelID.ParameterName = "@ReasonLabelID";
            if (!entity.IsReasonLabelIDNull)
                parReasonLabelID.Value = entity.ReasonLabelID;
            else
                parReasonLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parReasonLabelID);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in ReasonMstEntity object
        /// </summary>
        /// <param name="o">A ReasonMstEntity object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(ReasonMstInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_ReasonMstUpdate");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parDescription = cmd.CreateParameter();
            parDescription.ParameterName = "@Description";
            if (!entity.IsDescriptionNull)
                parDescription.Value = entity.Description;
            else
                parDescription.Value = System.DBNull.Value;
            cmdParams.Add(parDescription);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (!entity.IsIsActiveNull)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parReason = cmd.CreateParameter();
            parReason.ParameterName = "@Reason";
            if (!entity.IsReasonNull)
                parReason.Value = entity.Reason;
            else
                parReason.Value = System.DBNull.Value;
            cmdParams.Add(parReason);

            System.Data.IDbDataParameter parReasonLabelID = cmd.CreateParameter();
            parReasonLabelID.ParameterName = "@ReasonLabelID";
            if (!entity.IsReasonLabelIDNull)
                parReasonLabelID.Value = entity.ReasonLabelID;
            else
                parReasonLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parReasonLabelID);

            System.Data.IDbDataParameter pkparReasonID = cmd.CreateParameter();
            pkparReasonID.ParameterName = "@ReasonID";
            pkparReasonID.Value = entity.ReasonID;
            cmdParams.Add(pkparReasonID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_ReasonMstDeleteOne");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReasonID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_ReasonMstSelectOne");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReasonID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }







        protected override void CustomSave(ReasonMstInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(ReasonMstDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(ReasonMstInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(ReasonMstDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(ReasonMstInfo entity, object id)
        {
            entity.ReasonID = Convert.ToInt16(id);
        }




    }
}
