

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

    public abstract class GridColumnDAOBase : CustomAbstractDAO<GridColumnInfo>
    {

        /// <summary>
        /// A static representation of column ColumnID
        /// </summary>
        public static readonly string COLUMN_COLUMNID = "ColumnID";
        /// <summary>
        /// A static representation of column ColumnLabelID
        /// </summary>
        public static readonly string COLUMN_COLUMNLABELID = "ColumnLabelID";
        /// <summary>
        /// A static representation of column GridColumnID
        /// </summary>
        public static readonly string COLUMN_GRIDCOLUMNID = "GridColumnID";
        /// <summary>
        /// A static representation of column GridID
        /// </summary>
        public static readonly string COLUMN_GRIDID = "GridID";
        /// <summary>
        /// A static representation of column IsPartOfDefaultView
        /// </summary>
        public static readonly string COLUMN_ISPARTOFDEFAULTVIEW = "IsPartOfDefaultView";
        /// <summary>
        /// Provides access to the name of the primary key column (GridColumnID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "GridColumnID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "GridColumn";

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
        public GridColumnDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "GridColumn", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a GridColumnInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>GridColumnInfo</returns>
        protected override GridColumnInfo MapObject(System.Data.IDataReader r)
        {

            GridColumnInfo entity = new GridColumnInfo();
            entity.GridColumnID = r.GetInt16Value("GridColumnID");
            entity.GridID = r.GetInt16Value("GridID");
            entity.ColumnID = r.GetInt16Value("ColumnID");
            entity.ColumnUniqueName = r.GetStringValue("ColumnUniqueName");
            entity.ColumnLabelID = r.GetInt32Value("ColumnNameLabelID");
            entity.IsPartOfDefaultView = r.GetBooleanValue("IsPartOfDefaultView");
            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in GridColumnInfo object
        /// </summary>
        /// <param name="o">A GridColumnInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(GridColumnInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_GridColumn");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parColumnID = cmd.CreateParameter();
            parColumnID.ParameterName = "@ColumnID";
            if (!entity.IsColumnIDNull)
                parColumnID.Value = entity.ColumnID;
            else
                parColumnID.Value = System.DBNull.Value;
            cmdParams.Add(parColumnID);

            System.Data.IDbDataParameter parColumnLabelID = cmd.CreateParameter();
            parColumnLabelID.ParameterName = "@ColumnLabelID";
            if (!entity.IsColumnLabelIDNull)
                parColumnLabelID.Value = entity.ColumnLabelID;
            else
                parColumnLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parColumnLabelID);

            System.Data.IDbDataParameter parGridID = cmd.CreateParameter();
            parGridID.ParameterName = "@GridID";
            if (!entity.IsGridIDNull)
                parGridID.Value = entity.GridID;
            else
                parGridID.Value = System.DBNull.Value;
            cmdParams.Add(parGridID);

            System.Data.IDbDataParameter parIsPartOfDefaultView = cmd.CreateParameter();
            parIsPartOfDefaultView.ParameterName = "@IsPartOfDefaultView";
            if (!entity.IsIsPartOfDefaultViewNull)
                parIsPartOfDefaultView.Value = entity.IsPartOfDefaultView;
            else
                parIsPartOfDefaultView.Value = System.DBNull.Value;
            cmdParams.Add(parIsPartOfDefaultView);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in GridColumnInfo object
        /// </summary>
        /// <param name="o">A GridColumnInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(GridColumnInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_GridColumn");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parColumnID = cmd.CreateParameter();
            parColumnID.ParameterName = "@ColumnID";
            if (!entity.IsColumnIDNull)
                parColumnID.Value = entity.ColumnID;
            else
                parColumnID.Value = System.DBNull.Value;
            cmdParams.Add(parColumnID);

            System.Data.IDbDataParameter parColumnLabelID = cmd.CreateParameter();
            parColumnLabelID.ParameterName = "@ColumnLabelID";
            if (!entity.IsColumnLabelIDNull)
                parColumnLabelID.Value = entity.ColumnLabelID;
            else
                parColumnLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parColumnLabelID);

            System.Data.IDbDataParameter parGridID = cmd.CreateParameter();
            parGridID.ParameterName = "@GridID";
            if (!entity.IsGridIDNull)
                parGridID.Value = entity.GridID;
            else
                parGridID.Value = System.DBNull.Value;
            cmdParams.Add(parGridID);

            System.Data.IDbDataParameter parIsPartOfDefaultView = cmd.CreateParameter();
            parIsPartOfDefaultView.ParameterName = "@IsPartOfDefaultView";
            if (!entity.IsIsPartOfDefaultViewNull)
                parIsPartOfDefaultView.Value = entity.IsPartOfDefaultView;
            else
                parIsPartOfDefaultView.Value = System.DBNull.Value;
            cmdParams.Add(parIsPartOfDefaultView);

            System.Data.IDbDataParameter pkparGridColumnID = cmd.CreateParameter();
            pkparGridColumnID.ParameterName = "@GridColumnID";
            pkparGridColumnID.Value = entity.GridColumnID;
            cmdParams.Add(pkparGridColumnID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_GridColumn");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GridColumnID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_GridColumn");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GridColumnID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }







        protected override void CustomSave(GridColumnInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(GridColumnDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(GridColumnInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(GridColumnDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(GridColumnInfo entity, object id)
        {
            entity.GridColumnID = Convert.ToInt16(id);
        }




    }
}
