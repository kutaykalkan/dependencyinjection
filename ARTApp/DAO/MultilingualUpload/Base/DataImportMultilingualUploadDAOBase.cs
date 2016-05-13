
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
	public abstract class DataImportMultilingualUploadDAOBase : CustomAbstractDAO<DataImportMultilingualUploadInfo> 
	{
		/// <summary>
		/// A static representation of column DataImportID
		/// </summary>
		public static readonly string COLUMN_DATAIMPORTID = "DataImportID";
		/// <summary>
		/// A static representation of column DataImportMultilingualUploadID
		/// </summary>
		public static readonly string COLUMN_DATAIMPORTMULTILINGUALUPLOADID = "DataImportMultilingualUploadID";
		/// <summary>
		/// A static representation of column FromLanguageID
		/// </summary>
		public static readonly string COLUMN_FROMLANGUAGEID = "FromLanguageID";
		/// <summary>
		/// A static representation of column ToLanguageID
		/// </summary>
		public static readonly string COLUMN_TOLANGUAGEID = "ToLanguageID";
		/// <summary>
		/// Provides access to the name of the primary key column (DataImportMultilingualUploadID)
		/// </summary>
		public static readonly string TABLE_PRIMARYKEY = "DataImportMultilingualUploadID";

		/// <summary>
		/// Provides access to the name of the table
		/// </summary>
		public static readonly string TABLE_NAME = "DataImportMultilingualUpload";

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
        public DataImportMultilingualUploadDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "DataImportMultilingualUpload", oAppUserInfo.ConnectionString) 
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }
        
        /// <summary>
        /// Maps the IDataReader values to a DataImportMultilingualUploadInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>DataImportMultilingualUploadInfo</returns>
        protected override DataImportMultilingualUploadInfo MapObject(System.Data.IDataReader r) 
        {
            DataImportMultilingualUploadInfo entity = new DataImportMultilingualUploadInfo();
			entity.DataImportMultilingualUploadID = r.GetInt32Value("DataImportMultilingualUploadID");
			entity.DataImportID = r.GetInt32Value("DataImportID");
			entity.FromLanguageID = r.GetInt32Value("FromLanguageID");
			entity.ToLanguageID = r.GetInt32Value("ToLanguageID");
            return entity;
        }
 
		/// <summary>
		/// Creates the sql insert command, using the values from the passed
		/// in DataImportMultilingualUploadInfo object
		/// </summary>
		/// <param name="o">A DataImportMultilingualUploadInfo object, from which the insert values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(DataImportMultilingualUploadInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_DataImportMultilingualUpload");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter parDataImportID = cmd.CreateParameter();
			parDataImportID.ParameterName = "@DataImportID";
			if(entity != null)
				parDataImportID.Value = entity.DataImportID;
			else
				parDataImportID.Value = System.DBNull.Value;
			cmdParams.Add(parDataImportID);
			System.Data.IDbDataParameter parFromLanguageID = cmd.CreateParameter();
			parFromLanguageID.ParameterName = "@FromLanguageID";
			if(entity != null)
				parFromLanguageID.Value = entity.FromLanguageID;
			else
				parFromLanguageID.Value = System.DBNull.Value;
			cmdParams.Add(parFromLanguageID);
			System.Data.IDbDataParameter parToLanguageID = cmd.CreateParameter();
			parToLanguageID.ParameterName = "@ToLanguageID";
			if(entity != null)
				parToLanguageID.Value = entity.ToLanguageID;
			else
				parToLanguageID.Value = System.DBNull.Value;
			cmdParams.Add(parToLanguageID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql update command, using the values from the passed
		/// in DataImportMultilingualUploadInfo object
		/// </summary>
		/// <param name="o">A DataImportMultilingualUploadInfo object, from which the update values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(DataImportMultilingualUploadInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_DataImportMultilingualUpload");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter parDataImportID = cmd.CreateParameter();
			parDataImportID.ParameterName = "@DataImportID";
			if(entity != null)
				parDataImportID.Value = entity.DataImportID;
			else
				parDataImportID.Value = System.DBNull.Value;
			cmdParams.Add(parDataImportID);
			System.Data.IDbDataParameter parFromLanguageID = cmd.CreateParameter();
			parFromLanguageID.ParameterName = "@FromLanguageID";
			if(entity != null)
				parFromLanguageID.Value = entity.FromLanguageID;
			else
				parFromLanguageID.Value = System.DBNull.Value;
			cmdParams.Add(parFromLanguageID);
			System.Data.IDbDataParameter parToLanguageID = cmd.CreateParameter();
			parToLanguageID.ParameterName = "@ToLanguageID";
			if(entity != null)
				parToLanguageID.Value = entity.ToLanguageID;
			else
				parToLanguageID.Value = System.DBNull.Value;
			cmdParams.Add(parToLanguageID);
			System.Data.IDbDataParameter pkparDataImportMultilingualUploadID = cmd.CreateParameter();
			pkparDataImportMultilingualUploadID.ParameterName = "@DataImportMultilingualUploadID";
			pkparDataImportMultilingualUploadID.Value = entity.DataImportMultilingualUploadID;
			cmdParams.Add(pkparDataImportMultilingualUploadID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql delete command, using the passed in primary key
		/// </summary>
		/// <param name="id">The primary key of the object to delete</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_DataImportMultilingualUpload");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@DataImportMultilingualUploadID";
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
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_DataImportMultilingualUpload");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@DataImportMultilingualUploadID";
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
		public IList<DataImportMultilingualUploadInfo> SelectAllByDataImportID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_DataImportMultilingualUploadByDataImportID");
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
		public IList<DataImportMultilingualUploadInfo> SelectAllByFromLanguageID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_DataImportMultilingualUploadByFromLanguageID");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@FromLanguageID";
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
		public IList<DataImportMultilingualUploadInfo> SelectAllByToLanguageID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_DataImportMultilingualUploadByToLanguageID");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@ToLanguageID";
			par.Value = id;
			cmdParams.Add(par);
            return this.Select(cmd);
		}
		protected override void CustomSave(DataImportMultilingualUploadInfo o, IDbConnection connection)
		{
			string query = QueryHelper.GetSqlServerLastInsertedCommand(DataImportMultilingualUploadDAO.TABLE_NAME);
			IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
			cmd.CommandText = query;
			cmd.Connection = connection;
			object id = cmd.ExecuteScalar();
			this.MapIdentity(o, id);
		}
		
		protected override void CustomSave(DataImportMultilingualUploadInfo o, IDbConnection connection, IDbTransaction transaction)
		{
			string query = QueryHelper.GetSqlServerLastInsertedCommand(DataImportMultilingualUploadDAO.TABLE_NAME);
			IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
			cmd.CommandText = query;
			cmd.Transaction = transaction;
			cmd.Connection = connection;
			object id = cmd.ExecuteScalar();
			this.MapIdentity(o, id);
		}		
		private void MapIdentity(DataImportMultilingualUploadInfo entity, object id)
		{
			entity.DataImportMultilingualUploadID = Convert.ToInt32(id);
		}
	
    }
}
