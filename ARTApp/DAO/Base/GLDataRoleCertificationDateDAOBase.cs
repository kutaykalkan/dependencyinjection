

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

    public abstract class GLDataRoleCertificationDateDAOBase : CustomAbstractDAO<GLDataRoleCertificationDateInfo>
    {

        /// <summary>
        /// A static representation of column CertificationDate
        /// </summary>
        public static readonly string COLUMN_CERTIFICATIONDATE = "CertificationDate";
        /// <summary>
        /// A static representation of column GLDataID
        /// </summary>
        public static readonly string COLUMN_GLDATAID = "GLDataID";
        /// <summary>
        /// A static representation of column GLDataRoleCertificationDateID
        /// </summary>
        public static readonly string COLUMN_GLDATAROLECERTIFICATIONDATEID = "GLDataRoleCertificationDateID";
        /// <summary>
        /// A static representation of column RoleID
        /// </summary>
        public static readonly string COLUMN_ROLEID = "RoleID";
        /// <summary>
        /// Provides access to the name of the primary key column (GLDataRoleCertificationDateID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "GLDataRoleCertificationDateID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "GLDataRoleCertificationDate";

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
        public GLDataRoleCertificationDateDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "GLDataRoleCertificationDate", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a GLDataRoleCertificationDateInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>GLDataRoleCertificationDateInfo</returns>
        protected override GLDataRoleCertificationDateInfo MapObject(System.Data.IDataReader r)
        {

            GLDataRoleCertificationDateInfo entity = new GLDataRoleCertificationDateInfo();


            try
            {
                int ordinal = r.GetOrdinal("GLDataRoleCertificationDateID");
                if (!r.IsDBNull(ordinal)) entity.GLDataRoleCertificationDateID = ((System.Int32)(r.GetValue(ordinal)));
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
                int ordinal = r.GetOrdinal("RoleID");
                if (!r.IsDBNull(ordinal)) entity.RoleID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("CertificationDate");
                if (!r.IsDBNull(ordinal)) entity.CertificationDate = ((System.DateTime)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in GLDataRoleCertificationDateInfo object
        /// </summary>
        /// <param name="o">A GLDataRoleCertificationDateInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(GLDataRoleCertificationDateInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_GLDataRoleCertificationDate");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parCertificationDate = cmd.CreateParameter();
            parCertificationDate.ParameterName = "@CertificationDate";
            if (!entity.IsCertificationDateNull)
                parCertificationDate.Value = entity.CertificationDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parCertificationDate.Value = System.DBNull.Value;
            cmdParams.Add(parCertificationDate);

            System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            if (!entity.IsGLDataIDNull)
                parGLDataID.Value = entity.GLDataID;
            else
                parGLDataID.Value = System.DBNull.Value;
            cmdParams.Add(parGLDataID);

            System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            if (!entity.IsRoleIDNull)
                parRoleID.Value = entity.RoleID;
            else
                parRoleID.Value = System.DBNull.Value;
            cmdParams.Add(parRoleID);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in GLDataRoleCertificationDateInfo object
        /// </summary>
        /// <param name="o">A GLDataRoleCertificationDateInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(GLDataRoleCertificationDateInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_GLDataRoleCertificationDate");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parCertificationDate = cmd.CreateParameter();
            parCertificationDate.ParameterName = "@CertificationDate";
            if (!entity.IsCertificationDateNull)
                parCertificationDate.Value = entity.CertificationDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parCertificationDate.Value = System.DBNull.Value;
            cmdParams.Add(parCertificationDate);

            System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            if (!entity.IsGLDataIDNull)
                parGLDataID.Value = entity.GLDataID;
            else
                parGLDataID.Value = System.DBNull.Value;
            cmdParams.Add(parGLDataID);

            System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            if (!entity.IsRoleIDNull)
                parRoleID.Value = entity.RoleID;
            else
                parRoleID.Value = System.DBNull.Value;
            cmdParams.Add(parRoleID);

            System.Data.IDbDataParameter pkparGLDataRoleCertificationDateID = cmd.CreateParameter();
            pkparGLDataRoleCertificationDateID.ParameterName = "@GLDataRoleCertificationDateID";
            pkparGLDataRoleCertificationDateID.Value = entity.GLDataRoleCertificationDateID;
            cmdParams.Add(pkparGLDataRoleCertificationDateID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_GLDataRoleCertificationDate");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataRoleCertificationDateID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_GLDataRoleCertificationDate");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataRoleCertificationDateID";
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
        public IList<GLDataRoleCertificationDateInfo> SelectAllByGLDataID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_GLDataRoleCertificationDateByGLDataID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataID";
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
        public IList<GLDataRoleCertificationDateInfo> SelectAllByRoleID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_GLDataRoleCertificationDateByRoleID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(GLDataRoleCertificationDateInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(GLDataRoleCertificationDateDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(GLDataRoleCertificationDateInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(GLDataRoleCertificationDateDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(GLDataRoleCertificationDateInfo entity, object id)
        {
            entity.GLDataRoleCertificationDateID = Convert.ToInt32(id);
        }




    }
}
