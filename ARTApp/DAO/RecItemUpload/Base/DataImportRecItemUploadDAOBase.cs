
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO.Base 
{
	public abstract class DataImportRecItemUploadDAOBase : CustomAbstractDAO<DataImportRecItemUploadInfo> 
	{
		/// <summary>
		/// A static representation of column DataImportID
		/// </summary>
		public static readonly string COLUMN_DATAIMPORTID = "DataImportID";
		/// <summary>
		/// A static representation of column DataImportRecItemUploadID
		/// </summary>
		public static readonly string COLUMN_DATAIMPORTRECITEMUPLOADID = "DataImportRecItemUploadID";
		/// <summary>
		/// A static representation of column GLDataID
		/// </summary>
		public static readonly string COLUMN_GLDATAID = "GLDataID";
		/// <summary>
		/// A static representation of column ReconciliationCategoryID
		/// </summary>
		public static readonly string COLUMN_RECONCILIATIONCATEGORYID = "ReconciliationCategoryID";
        /// <summary>
        /// A static representation of column ReconciliationCategoryTypeID
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONCATEGORYTYPEID = "ReconciliationCategoryTypeID";
        /// <summary>
		/// Provides access to the name of the primary key column (DataImportRecItemUploadID)
		/// </summary>
		public static readonly string TABLE_PRIMARYKEY = "DataImportRecItemUploadID";

		/// <summary>
		/// Provides access to the name of the table
		/// </summary>
		public static readonly string TABLE_NAME = "DataImportRecItemUpload";

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
        public DataImportRecItemUploadDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "DataImportRecItemUpload", oAppUserInfo.ConnectionString) 
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }
        
        /// <summary>
        /// Maps the IDataReader values to a DataImportRecItemUploadInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>DataImportRecItemUploadInfo</returns>
        protected override DataImportRecItemUploadInfo MapObject(System.Data.IDataReader r) 
        {
            DataImportRecItemUploadInfo entity = new DataImportRecItemUploadInfo();
			entity.DataImportRecItemUploadID = r.GetInt32Value("DataImportRecItemUploadID");
			entity.DataImportID = r.GetInt32Value("DataImportID");
			entity.GLDataID = r.GetInt64Value("GLDataID");
			entity.ReconciliationCategoryID = r.GetInt16Value("ReconciliationCategoryID");
            entity.ReconciliationCategoryTypeID = r.GetInt16Value("ReconciliationCategoryTypeID");
            return entity;
        }
 
		/// <summary>
		/// Creates the sql insert command, using the values from the passed
		/// in DataImportRecItemUploadInfo object
		/// </summary>
		/// <param name="o">A DataImportRecItemUploadInfo object, from which the insert values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(DataImportRecItemUploadInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_DataImportRecItemUpload");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter parDataImportID = cmd.CreateParameter();
			parDataImportID.ParameterName = "@DataImportID";
			if(entity != null)
				parDataImportID.Value = entity.DataImportID;
			else
				parDataImportID.Value = System.DBNull.Value;
			cmdParams.Add(parDataImportID);
			System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
			parGLDataID.ParameterName = "@GLDataID";
			if(entity != null)
				parGLDataID.Value = entity.GLDataID;
			else
				parGLDataID.Value = System.DBNull.Value;
			cmdParams.Add(parGLDataID);
			System.Data.IDbDataParameter parReconciliationCategoryID = cmd.CreateParameter();
			parReconciliationCategoryID.ParameterName = "@ReconciliationCategoryID";
			if(entity != null)
				parReconciliationCategoryID.Value = entity.ReconciliationCategoryID;
			else
				parReconciliationCategoryID.Value = System.DBNull.Value;
			cmdParams.Add(parReconciliationCategoryID);
            
            System.Data.IDbDataParameter parReconciliationCategoryTypeID = cmd.CreateParameter();
            parReconciliationCategoryTypeID.ParameterName = "@ReconciliationCategoryTypeID";
            if (entity != null)
                parReconciliationCategoryTypeID.Value = entity.ReconciliationCategoryTypeID;
            else
                parReconciliationCategoryTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationCategoryTypeID);
            return cmd;
        }

		/// <summary>
		/// Creates the sql update command, using the values from the passed
		/// in DataImportRecItemUploadInfo object
		/// </summary>
		/// <param name="o">A DataImportRecItemUploadInfo object, from which the update values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(DataImportRecItemUploadInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("usp_UPD_DataImportRecItemUpload");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter parDataImportID = cmd.CreateParameter();
			parDataImportID.ParameterName = "@DataImportID";
			if(entity != null)
				parDataImportID.Value = entity.DataImportID;
			else
				parDataImportID.Value = System.DBNull.Value;
			cmdParams.Add(parDataImportID);
			System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
			parGLDataID.ParameterName = "@GLDataID";
			if(entity != null)
				parGLDataID.Value = entity.GLDataID;
			else
				parGLDataID.Value = System.DBNull.Value;
			cmdParams.Add(parGLDataID);
			System.Data.IDbDataParameter parReconciliationCategoryID = cmd.CreateParameter();
			parReconciliationCategoryID.ParameterName = "@ReconciliationCategoryID";
			if(entity != null)
				parReconciliationCategoryID.Value = entity.ReconciliationCategoryID;
			else
				parReconciliationCategoryID.Value = System.DBNull.Value;
			cmdParams.Add(parReconciliationCategoryID);
            System.Data.IDbDataParameter parReconciliationCategoryTypeID = cmd.CreateParameter();
            parReconciliationCategoryTypeID.ParameterName = "@ReconciliationCategoryTypeID";
            if (entity != null)
                parReconciliationCategoryTypeID.Value = entity.ReconciliationCategoryTypeID;
            else
                parReconciliationCategoryTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationCategoryTypeID);
            System.Data.IDbDataParameter pkparDataImportRecItemUploadID = cmd.CreateParameter();
			pkparDataImportRecItemUploadID.ParameterName = "@DataImportRecItemUploadID";
			pkparDataImportRecItemUploadID.Value = entity.DataImportRecItemUploadID;
			cmdParams.Add(pkparDataImportRecItemUploadID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql delete command, using the passed in primary key
		/// </summary>
		/// <param name="id">The primary key of the object to delete</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_DataImportRecItemUpload");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@DataImportRecItemUploadID";
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
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_DataImportRecItemUpload");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@DataImportRecItemUploadID";
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
		public IList<DataImportRecItemUploadInfo> SelectAllByDataImportID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_DataImportRecItemUploadByDataImportID");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@DataImportID";
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
		public IList<DataImportRecItemUploadInfo> SelectAllByGLDataID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_DataImportRecItemUploadByGLDataID");
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
		public IList<DataImportRecItemUploadInfo> SelectAllByReconciliationCategoryID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_DataImportRecItemUploadByReconciliationCategoryID");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@ReconciliationCategoryID";
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
        public IList<DataImportRecItemUploadInfo> SelectAllByReconciliationCategoryTypeID(object id)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_DataImportRecItemUploadByReconciliationCategoryTypeID");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationCategoryTypeID";
            par.Value = id;
            cmdParams.Add(par);
            return this.Select(cmd);
        }
        
        protected override void CustomSave(DataImportRecItemUploadInfo o, IDbConnection connection)
		{
			string query = QueryHelper.GetSqlServerLastInsertedCommand(DataImportRecItemUploadDAO.TABLE_NAME);
			IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
			cmd.CommandText = query;
			cmd.Connection = connection;
			object id = cmd.ExecuteScalar();
			this.MapIdentity(o, id);
		}
		
		protected override void CustomSave(DataImportRecItemUploadInfo o, IDbConnection connection, IDbTransaction transaction)
		{
			string query = QueryHelper.GetSqlServerLastInsertedCommand(DataImportRecItemUploadDAO.TABLE_NAME);
			IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
			cmd.CommandText = query;
			cmd.Transaction = transaction;
			cmd.Connection = connection;
			object id = cmd.ExecuteScalar();
			this.MapIdentity(o, id);
		}		
		private void MapIdentity(DataImportRecItemUploadInfo entity, object id)
		{
			entity.DataImportRecItemUploadID = Convert.ToInt32(id);
		}
	
    }
}
