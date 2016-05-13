

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

    public abstract class ExceptionTypeMstDAOBase : CustomAbstractDAO<ExceptionTypeMstInfo>
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
        /// A static representation of column ExceptionType
        /// </summary>
        public static readonly string COLUMN_EXCEPTIONTYPE = "ExceptionType";
        /// <summary>
        /// A static representation of column ExceptionTypeID
        /// </summary>
        public static readonly string COLUMN_EXCEPTIONTYPEID = "ExceptionTypeID";
        /// <summary>
        /// A static representation of column ExceptionTypeLabelID
        /// </summary>
        public static readonly string COLUMN_EXCEPTIONTYPELABELID = "ExceptionTypeLabelID";
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
        /// Provides access to the name of the primary key column (ExceptionTypeID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "ExceptionTypeID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "ExceptionTypeMst";

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
        public ExceptionTypeMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "ExceptionTypeMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a ExceptionTypeMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>ExceptionTypeMstInfo</returns>
        protected override ExceptionTypeMstInfo MapObject(System.Data.IDataReader r)
        {

            ExceptionTypeMstInfo entity = new ExceptionTypeMstInfo();
            entity.ExceptionTypeID = r.GetInt16Value("ExceptionTypeID");
            entity.ExceptionTypeLabelID = r.GetInt32Value("ExceptionTypeLabelID");
            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in ExceptionTypeMstInfo object
        /// </summary>
        /// <param name="o">A ExceptionTypeMstInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(ExceptionTypeMstInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_ExceptionTypeMst");
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

            System.Data.IDbDataParameter parExceptionType = cmd.CreateParameter();
            parExceptionType.ParameterName = "@ExceptionType";
            if (!entity.IsExceptionTypeNull)
                parExceptionType.Value = entity.ExceptionType;
            else
                parExceptionType.Value = System.DBNull.Value;
            cmdParams.Add(parExceptionType);

            System.Data.IDbDataParameter parExceptionTypeID = cmd.CreateParameter();
            parExceptionTypeID.ParameterName = "@ExceptionTypeID";
            if (!entity.IsExceptionTypeIDNull)
                parExceptionTypeID.Value = entity.ExceptionTypeID;
            else
                parExceptionTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parExceptionTypeID);

            System.Data.IDbDataParameter parExceptionTypeLabelID = cmd.CreateParameter();
            parExceptionTypeLabelID.ParameterName = "@ExceptionTypeLabelID";
            if (!entity.IsExceptionTypeLabelIDNull)
                parExceptionTypeLabelID.Value = entity.ExceptionTypeLabelID;
            else
                parExceptionTypeLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parExceptionTypeLabelID);

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
        /// in ExceptionTypeMstInfo object
        /// </summary>
        /// <param name="o">A ExceptionTypeMstInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(ExceptionTypeMstInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_ExceptionTypeMst");
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

            System.Data.IDbDataParameter parExceptionType = cmd.CreateParameter();
            parExceptionType.ParameterName = "@ExceptionType";
            if (!entity.IsExceptionTypeNull)
                parExceptionType.Value = entity.ExceptionType;
            else
                parExceptionType.Value = System.DBNull.Value;
            cmdParams.Add(parExceptionType);

            System.Data.IDbDataParameter parExceptionTypeLabelID = cmd.CreateParameter();
            parExceptionTypeLabelID.ParameterName = "@ExceptionTypeLabelID";
            if (!entity.IsExceptionTypeLabelIDNull)
                parExceptionTypeLabelID.Value = entity.ExceptionTypeLabelID;
            else
                parExceptionTypeLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parExceptionTypeLabelID);

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

            System.Data.IDbDataParameter pkparExceptionTypeID = cmd.CreateParameter();
            pkparExceptionTypeID.ParameterName = "@ExceptionTypeID";
            pkparExceptionTypeID.Value = entity.ExceptionTypeID;
            cmdParams.Add(pkparExceptionTypeID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_ExceptionTypeMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ExceptionTypeID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_ExceptionTypeMst");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ExceptionTypeID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }








    }
}
