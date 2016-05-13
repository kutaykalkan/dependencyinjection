

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

    public abstract class OperatorMstDAOBase : CustomAbstractDAO<OperatorMstInfo>
    {

        /// <summary>
        /// A static representation of column IsActive
        /// </summary>
        public static readonly string COLUMN_ISACTIVE = "IsActive";
        /// <summary>
        /// A static representation of column OperatorID
        /// </summary>
        public static readonly string COLUMN_OPERATORID = "OperatorID";
        /// <summary>
        /// A static representation of column OperatorName
        /// </summary>
        public static readonly string COLUMN_OPERATORNAME = "OperatorName";
        /// <summary>
        /// A static representation of column OperatorNameLabelID
        /// </summary>
        public static readonly string COLUMN_OPERATORNAMELABELID = "OperatorNameLabelID";
        /// <summary>
        /// Provides access to the name of the primary key column (OperatorID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "OperatorID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "OperatorMst";

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
        public OperatorMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "OperatorMst", oAppUserInfo.ConnectionString)
        {

            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a OperatorMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>OperatorMstInfo</returns>
        protected override OperatorMstInfo MapObject(System.Data.IDataReader r)
        {

            OperatorMstInfo entity = new OperatorMstInfo();

            entity.OperatorID = r.GetInt16Value("OperatorID");
            entity.OperatorName = r.GetStringValue("OperatorName");
            entity.OperatorNameLabelID = r.GetInt32Value("OperatorNameLabelID");
            entity.IsActive = r.GetBooleanValue("IsActive");

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in OperatorMstInfo object
        /// </summary>
        /// <param name="o">A OperatorMstInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(OperatorMstInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_OperatorMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (entity.IsActive.HasValue )
                parIsActive.Value = entity.IsActive.Value ;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parOperatorID = cmd.CreateParameter();
            parOperatorID.ParameterName = "@OperatorID";
            if (entity.OperatorID.HasValue )
                parOperatorID.Value = entity.OperatorID.Value ;
            else
                parOperatorID.Value = System.DBNull.Value;
            cmdParams.Add(parOperatorID);

            System.Data.IDbDataParameter parOperatorName = cmd.CreateParameter();
            parOperatorName.ParameterName = "@OperatorName";
            if (!entity.IsOperatorNameNull)
                parOperatorName.Value = entity.OperatorName;
            else
                parOperatorName.Value = System.DBNull.Value;
            cmdParams.Add(parOperatorName);

            System.Data.IDbDataParameter parOperatorNameLabelID = cmd.CreateParameter();
            parOperatorNameLabelID.ParameterName = "@OperatorNameLabelID";
            if (entity.OperatorNameLabelID.HasValue )
                parOperatorNameLabelID.Value = entity.OperatorNameLabelID.Value ;
            else
                parOperatorNameLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parOperatorNameLabelID);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in OperatorMstInfo object
        /// </summary>
        /// <param name="o">A OperatorMstInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(OperatorMstInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_OperatorMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (entity.IsActive.HasValue )
                parIsActive.Value = entity.IsActive.Value ;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parOperatorName = cmd.CreateParameter();
            parOperatorName.ParameterName = "@OperatorName";
            if (!entity.IsOperatorNameNull)
                parOperatorName.Value = entity.OperatorName;
            else
                parOperatorName.Value = System.DBNull.Value;
            cmdParams.Add(parOperatorName);

            System.Data.IDbDataParameter parOperatorNameLabelID = cmd.CreateParameter();
            parOperatorNameLabelID.ParameterName = "@OperatorNameLabelID";
            if (entity.OperatorNameLabelID.HasValue )
                parOperatorNameLabelID.Value = entity.OperatorNameLabelID.Value ;
            else
                parOperatorNameLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parOperatorNameLabelID);

            System.Data.IDbDataParameter pkparOperatorID = cmd.CreateParameter();
            pkparOperatorID.ParameterName = "@OperatorID";
            pkparOperatorID.Value = entity.OperatorID;
            cmdParams.Add(pkparOperatorID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_OperatorMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@OperatorID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_OperatorMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@OperatorID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }









    }
}
