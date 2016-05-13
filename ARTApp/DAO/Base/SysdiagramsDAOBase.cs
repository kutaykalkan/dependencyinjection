

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

    public abstract class SysdiagramsDAOBase : CustomAbstractDAO<SysdiagramsInfo>
    {

        /// <summary>
        /// A static representation of column definition
        /// </summary>
        public static readonly string COLUMN_DEFINITION = "definition";
        /// <summary>
        /// A static representation of column diagram_id
        /// </summary>
        public static readonly string COLUMN_DIAGRAM_ID = "diagram_id";
        /// <summary>
        /// A static representation of column name
        /// </summary>
        public static readonly string COLUMN_NAME = "name";
        /// <summary>
        /// A static representation of column principal_id
        /// </summary>
        public static readonly string COLUMN_PRINCIPAL_ID = "principal_id";
        /// <summary>
        /// A static representation of column version
        /// </summary>
        public static readonly string COLUMN_VERSION = "version";
        /// <summary>
        /// Provides access to the name of the primary key column (diagram_id)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "diagram_id";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "sysdiagrams";

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
        public SysdiagramsDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "sysdiagrams", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a SysdiagramsInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>SysdiagramsInfo</returns>
        protected override SysdiagramsInfo MapObject(System.Data.IDataReader r)
        {

            SysdiagramsInfo entity = new SysdiagramsInfo();


            try
            {
                int ordinal = r.GetOrdinal("name");
                if (!r.IsDBNull(ordinal)) entity.Name = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("principal_id");
                if (!r.IsDBNull(ordinal)) entity.Principal_id = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("diagram_id");
                if (!r.IsDBNull(ordinal)) entity.Diagram_id = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("version");
                if (!r.IsDBNull(ordinal)) entity.Version = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("definition");
                if (!r.IsDBNull(ordinal)) entity.Definition = ((System.Byte[])(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in SysdiagramsInfo object
        /// </summary>
        /// <param name="o">A SysdiagramsInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(SysdiagramsInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_Sysdiagrams");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parDefinition = cmd.CreateParameter();
            parDefinition.ParameterName = "@definition";
            if (!entity.IsDefinitionNull)
                parDefinition.Value = entity.Definition;
            else
                parDefinition.Value = System.DBNull.Value;
            cmdParams.Add(parDefinition);

            System.Data.IDbDataParameter parName = cmd.CreateParameter();
            parName.ParameterName = "@name";
            if (!entity.IsNameNull)
                parName.Value = entity.Name;
            else
                parName.Value = System.DBNull.Value;
            cmdParams.Add(parName);

            System.Data.IDbDataParameter parPrincipal_id = cmd.CreateParameter();
            parPrincipal_id.ParameterName = "@principal_id";
            if (!entity.IsPrincipal_idNull)
                parPrincipal_id.Value = entity.Principal_id;
            else
                parPrincipal_id.Value = System.DBNull.Value;
            cmdParams.Add(parPrincipal_id);

            System.Data.IDbDataParameter parVersion = cmd.CreateParameter();
            parVersion.ParameterName = "@version";
            if (!entity.IsVersionNull)
                parVersion.Value = entity.Version;
            else
                parVersion.Value = System.DBNull.Value;
            cmdParams.Add(parVersion);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in SysdiagramsInfo object
        /// </summary>
        /// <param name="o">A SysdiagramsInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(SysdiagramsInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_Sysdiagrams");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parDefinition = cmd.CreateParameter();
            parDefinition.ParameterName = "@definition";
            if (!entity.IsDefinitionNull)
                parDefinition.Value = entity.Definition;
            else
                parDefinition.Value = System.DBNull.Value;
            cmdParams.Add(parDefinition);

            System.Data.IDbDataParameter parName = cmd.CreateParameter();
            parName.ParameterName = "@name";
            if (!entity.IsNameNull)
                parName.Value = entity.Name;
            else
                parName.Value = System.DBNull.Value;
            cmdParams.Add(parName);

            System.Data.IDbDataParameter parPrincipal_id = cmd.CreateParameter();
            parPrincipal_id.ParameterName = "@principal_id";
            if (!entity.IsPrincipal_idNull)
                parPrincipal_id.Value = entity.Principal_id;
            else
                parPrincipal_id.Value = System.DBNull.Value;
            cmdParams.Add(parPrincipal_id);

            System.Data.IDbDataParameter parVersion = cmd.CreateParameter();
            parVersion.ParameterName = "@version";
            if (!entity.IsVersionNull)
                parVersion.Value = entity.Version;
            else
                parVersion.Value = System.DBNull.Value;
            cmdParams.Add(parVersion);

            System.Data.IDbDataParameter pkparDiagram_id = cmd.CreateParameter();
            pkparDiagram_id.ParameterName = "@diagram_id";
            pkparDiagram_id.Value = entity.Diagram_id;
            cmdParams.Add(pkparDiagram_id);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_Sysdiagrams");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@diagram_id";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_Sysdiagrams");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@diagram_id";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }







        protected override void CustomSave(SysdiagramsInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(SysdiagramsDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(SysdiagramsInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(SysdiagramsDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(SysdiagramsInfo entity, object id)
        {
            entity.Diagram_id = Convert.ToInt32(id);
        }




    }
}
