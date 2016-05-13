

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

    public abstract class ColumnOperatorMappingDAOBase : CustomAbstractDAO<ColumnOperatorMappingInfo>
    {

        /// <summary>
        /// A static representation of column ColumnID
        /// </summary>
        public static readonly string COLUMN_COLUMNID = "ColumnID";
        /// <summary>
        /// A static representation of column ColumnOperatorMappingID
        /// </summary>
        public static readonly string COLUMN_COLUMNOPERATORMAPPINGID = "ColumnOperatorMappingID";
        /// <summary>
        /// A static representation of column IsActive
        /// </summary>
        public static readonly string COLUMN_ISACTIVE = "IsActive";
        /// <summary>
        /// A static representation of column OperatorID
        /// </summary>
        public static readonly string COLUMN_OPERATORID = "OperatorID";
        /// <summary>
        /// Provides access to the name of the primary key column (ColumnOperatorMappingID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "ColumnOperatorMappingID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "ColumnOperatorMapping";

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
        public ColumnOperatorMappingDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "ColumnOperatorMapping", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a ColumnOperatorMappingInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>ColumnOperatorMappingInfo</returns>
        protected override ColumnOperatorMappingInfo MapObject(System.Data.IDataReader r)
        {

            ColumnOperatorMappingInfo entity = new ColumnOperatorMappingInfo();

            entity.ColumnOperatorMappingID = r.GetInt16Value("ColumnOperatorMappingID");
            entity.ColumnID = r.GetInt16Value("ColumnID");
            entity.OperatorID = r.GetInt16Value("OperatorID");
            entity.IsActive = r.GetBooleanValue("IsActive");

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in ColumnOperatorMappingInfo object
        /// </summary>
        /// <param name="o">A ColumnOperatorMappingInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(ColumnOperatorMappingInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_ColumnOperatorMapping");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parColumnID = cmd.CreateParameter();
            parColumnID.ParameterName = "@ColumnID";
            if (entity.ColumnID.HasValue)
                parColumnID.Value = entity.ColumnID.Value;
            else
                parColumnID.Value = System.DBNull.Value;
            cmdParams.Add(parColumnID);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (entity.IsActive.HasValue)
                parIsActive.Value = entity.IsActive.Value;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parOperatorID = cmd.CreateParameter();
            parOperatorID.ParameterName = "@OperatorID";
            if (entity.OperatorID.HasValue)
                parOperatorID.Value = entity.OperatorID.Value;
            else
                parOperatorID.Value = System.DBNull.Value;
            cmdParams.Add(parOperatorID);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in ColumnOperatorMappingInfo object
        /// </summary>
        /// <param name="o">A ColumnOperatorMappingInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(ColumnOperatorMappingInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_ColumnOperatorMapping");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parColumnID = cmd.CreateParameter();
            parColumnID.ParameterName = "@ColumnID";
            if (entity.ColumnID.HasValue)
                parColumnID.Value = entity.ColumnID.Value;
            else
                parColumnID.Value = System.DBNull.Value;
            cmdParams.Add(parColumnID);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (entity.IsActive.HasValue)
                parIsActive.Value = entity.IsActive.Value;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parOperatorID = cmd.CreateParameter();
            parOperatorID.ParameterName = "@OperatorID";
            if (entity.OperatorID.HasValue)
                parOperatorID.Value = entity.OperatorID.Value;
            else
                parOperatorID.Value = System.DBNull.Value;
            cmdParams.Add(parOperatorID);

            System.Data.IDbDataParameter pkparColumnOperatorMappingID = cmd.CreateParameter();
            pkparColumnOperatorMappingID.ParameterName = "@ColumnOperatorMappingID";
            pkparColumnOperatorMappingID.Value = entity.ColumnOperatorMappingID;
            cmdParams.Add(pkparColumnOperatorMappingID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_ColumnOperatorMapping");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ColumnOperatorMappingID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_ColumnOperatorMapping");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ColumnOperatorMappingID";
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
        public IList<ColumnOperatorMappingInfo> SelectAllByOperatorID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_ColumnOperatorMappingByOperatorID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@OperatorID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }

        private void MapIdentity(ColumnOperatorMappingInfo entity, object id)
        {
            entity.ColumnOperatorMappingID = Convert.ToInt16(id);
        }
    }
}
