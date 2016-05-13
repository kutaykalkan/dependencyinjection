

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

    public abstract class GLDataStatusDAOBase : CustomAbstractDAO<GLDataStatusInfo>
    {

        /// <summary>
        /// A static representation of column GLDataID
        /// </summary>
        public static readonly string COLUMN_GLDATAID = "GLDataID";
        /// <summary>
        /// A static representation of column GLDataStatusID
        /// </summary>
        public static readonly string COLUMN_GLDATASTATUSID = "GLDataStatusID";
        /// <summary>
        /// A static representation of column StatusDate
        /// </summary>
        public static readonly string COLUMN_STATUSDATE = "StatusDate";
        /// <summary>
        /// A static representation of column StatusID
        /// </summary>
        public static readonly string COLUMN_STATUSID = "StatusID";
        /// <summary>
        /// A static representation of column StatusTypeID
        /// </summary>
        public static readonly string COLUMN_STATUSTYPEID = "StatusTypeID";
        /// <summary>
        /// Provides access to the name of the primary key column (GLDataStatusID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "GLDataStatusID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "GLDataStatus";

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
        public GLDataStatusDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "GLDataStatus", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a GLDataStatusInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>GLDataStatusInfo</returns>
        protected override GLDataStatusInfo MapObject(System.Data.IDataReader r)
        {

            GLDataStatusInfo entity = new GLDataStatusInfo();
            entity.GLDataStatusID = r.GetInt64Value("GLDataStatusID");
            entity.GLDataID = r.GetInt64Value("GLDataID");

            entity.StatusID = r.GetInt16Value("StatusID");
            entity.StatusTypeID = r.GetInt16Value("StatusTypeID");
            entity.StatusDate = r.GetDateValue("StatusDate");
            entity.StatusLabelID = r.GetInt32Value("StatusLabelID");
            entity.AddedByUserInfo = new UserHdrInfo();
            entity.AddedByUserInfo.FirstName = r.GetStringValue("AddedByFirstName");
            entity.AddedByUserInfo.LastName = r.GetStringValue("AddedByLastName");

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in GLDataStatusInfo object
        /// </summary>
        /// <param name="o">A GLDataStatusInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(GLDataStatusInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_GLDataStatus");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            if (!entity.IsGLDataIDNull)
                parGLDataID.Value = entity.GLDataID;
            else
                parGLDataID.Value = System.DBNull.Value;
            cmdParams.Add(parGLDataID);

            System.Data.IDbDataParameter parStatusDate = cmd.CreateParameter();
            parStatusDate.ParameterName = "@StatusDate";
            if (!entity.IsStatusDateNull)
                parStatusDate.Value = entity.StatusDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parStatusDate.Value = System.DBNull.Value;
            cmdParams.Add(parStatusDate);

            System.Data.IDbDataParameter parStatusID = cmd.CreateParameter();
            parStatusID.ParameterName = "@StatusID";
            if (!entity.IsStatusIDNull)
                parStatusID.Value = entity.StatusID;
            else
                parStatusID.Value = System.DBNull.Value;
            cmdParams.Add(parStatusID);

            System.Data.IDbDataParameter parStatusTypeID = cmd.CreateParameter();
            parStatusTypeID.ParameterName = "@StatusTypeID";
            if (!entity.IsStatusTypeIDNull)
                parStatusTypeID.Value = entity.StatusTypeID;
            else
                parStatusTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parStatusTypeID);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in GLDataStatusInfo object
        /// </summary>
        /// <param name="o">A GLDataStatusInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(GLDataStatusInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_GLDataStatus");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            if (!entity.IsGLDataIDNull)
                parGLDataID.Value = entity.GLDataID;
            else
                parGLDataID.Value = System.DBNull.Value;
            cmdParams.Add(parGLDataID);

            System.Data.IDbDataParameter parStatusDate = cmd.CreateParameter();
            parStatusDate.ParameterName = "@StatusDate";
            if (!entity.IsStatusDateNull)
                parStatusDate.Value = entity.StatusDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parStatusDate.Value = System.DBNull.Value;
            cmdParams.Add(parStatusDate);

            System.Data.IDbDataParameter parStatusID = cmd.CreateParameter();
            parStatusID.ParameterName = "@StatusID";
            if (!entity.IsStatusIDNull)
                parStatusID.Value = entity.StatusID;
            else
                parStatusID.Value = System.DBNull.Value;
            cmdParams.Add(parStatusID);

            System.Data.IDbDataParameter parStatusTypeID = cmd.CreateParameter();
            parStatusTypeID.ParameterName = "@StatusTypeID";
            if (!entity.IsStatusTypeIDNull)
                parStatusTypeID.Value = entity.StatusTypeID;
            else
                parStatusTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parStatusTypeID);

            System.Data.IDbDataParameter pkparGLDataStatusID = cmd.CreateParameter();
            pkparGLDataStatusID.ParameterName = "@GLDataStatusID";
            pkparGLDataStatusID.Value = entity.GLDataStatusID;
            cmdParams.Add(pkparGLDataStatusID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_GLDataStatus");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataStatusID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_GLDataStatus");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataStatusID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }







        protected override void CustomSave(GLDataStatusInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(GLDataStatusDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(GLDataStatusInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(GLDataStatusDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(GLDataStatusInfo entity, object id)
        {
            entity.GLDataStatusID = Convert.ToInt64(id);
        }




    }
}
