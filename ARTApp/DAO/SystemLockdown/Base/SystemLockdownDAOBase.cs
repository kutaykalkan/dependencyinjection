
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
	public abstract class SystemLockdownDAOBase : CustomAbstractDAO<SystemLockdownInfo> 
	{
		/// <summary>
		/// A static representation of column AccountID
		/// </summary>
		public static readonly string COLUMN_ACCOUNTID = "AccountID";
		/// <summary>
		/// A static representation of column AddedBy
		/// </summary>
		public static readonly string COLUMN_ADDEDBY = "AddedBy";
		/// <summary>
		/// A static representation of column CompanyID
		/// </summary>
		public static readonly string COLUMN_COMPANYID = "CompanyID";
		/// <summary>
		/// A static representation of column DataImportID
		/// </summary>
		public static readonly string COLUMN_DATAIMPORTID = "DataImportID";
		/// <summary>
		/// A static representation of column DateAdded
		/// </summary>
		public static readonly string COLUMN_DATEADDED = "DateAdded";
		/// <summary>
		/// A static representation of column RecPeriodID
		/// </summary>
		public static readonly string COLUMN_RECPERIODID = "RecPeriodID";
		/// <summary>
		/// A static representation of column SystemLockdownID
		/// </summary>
		public static readonly string COLUMN_SYSTEMLOCKDOWNID = "SystemLockdownID";
		/// <summary>
		/// A static representation of column SystemLockdownMessage
		/// </summary>
		public static readonly string COLUMN_SYSTEMLOCKDOWNMESSAGE = "SystemLockdownMessage";
		/// <summary>
		/// A static representation of column SystemLockdownReasonID
		/// </summary>
		public static readonly string COLUMN_SYSTEMLOCKDOWNREASONID = "SystemLockdownReasonID";
		/// <summary>
		/// Provides access to the name of the primary key column (SystemLockdownID)
		/// </summary>
		public static readonly string TABLE_PRIMARYKEY = "SystemLockdownID";

		/// <summary>
		/// Provides access to the name of the table
		/// </summary>
		public static readonly string TABLE_NAME = "SystemLockdown";

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
        public SystemLockdownDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "SystemLockdown", oAppUserInfo.ConnectionString) 
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }
        
        /// <summary>
        /// Maps the IDataReader values to a SystemLockdownInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>SystemLockdownInfo</returns>
        protected override SystemLockdownInfo MapObject(System.Data.IDataReader r) 
        {
            SystemLockdownInfo entity = new SystemLockdownInfo();
			entity.SystemLockdownID = r.GetInt32Value("SystemLockdownID");
			entity.CompanyID = r.GetInt32Value("CompanyID");
			entity.RecPeriodID = r.GetInt32Value("RecPeriodID");
			entity.DataImportID = r.GetInt32Value("DataImportID");
			entity.AccountID = r.GetInt64Value("AccountID");
			entity.SystemLockdownReasonID = r.GetInt16Value("SystemLockdownReasonID");
			entity.SystemLockdownMessage = r.GetStringValue("SystemLockdownMessage");
			entity.DateAdded = r.GetDateValue("DateAdded");
			entity.AddedBy = r.GetStringValue("AddedBy");
            return entity;
        }
 
		/// <summary>
		/// Creates the sql insert command, using the values from the passed
		/// in SystemLockdownInfo object
		/// </summary>
		/// <param name="o">A SystemLockdownInfo object, from which the insert values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(SystemLockdownInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_SystemLockdown");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter parAccountID = cmd.CreateParameter();
			parAccountID.ParameterName = "@AccountID";
			if(entity != null)
				parAccountID.Value = entity.AccountID;
			else
				parAccountID.Value = System.DBNull.Value;
			cmdParams.Add(parAccountID);
			System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
			parAddedBy.ParameterName = "@AddedBy";
			if(entity != null)
				parAddedBy.Value = entity.AddedBy;
			else
				parAddedBy.Value = System.DBNull.Value;
			cmdParams.Add(parAddedBy);
			System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
			parCompanyID.ParameterName = "@CompanyID";
			if(entity != null)
				parCompanyID.Value = entity.CompanyID;
			else
				parCompanyID.Value = System.DBNull.Value;
			cmdParams.Add(parCompanyID);
			System.Data.IDbDataParameter parDataImportID = cmd.CreateParameter();
			parDataImportID.ParameterName = "@DataImportID";
			if(entity != null)
				parDataImportID.Value = entity.DataImportID;
			else
				parDataImportID.Value = System.DBNull.Value;
			cmdParams.Add(parDataImportID);
			System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
			parDateAdded.ParameterName = "@DateAdded";
			if(entity != null)
				parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
			else
				parDateAdded.Value = System.DBNull.Value;
			cmdParams.Add(parDateAdded);
			System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
			parRecPeriodID.ParameterName = "@RecPeriodID";
			if(entity != null)
				parRecPeriodID.Value = entity.RecPeriodID;
			else
				parRecPeriodID.Value = System.DBNull.Value;
			cmdParams.Add(parRecPeriodID);
			System.Data.IDbDataParameter parSystemLockdownMessage = cmd.CreateParameter();
			parSystemLockdownMessage.ParameterName = "@SystemLockdownMessage";
			if(entity != null)
				parSystemLockdownMessage.Value = entity.SystemLockdownMessage;
			else
				parSystemLockdownMessage.Value = System.DBNull.Value;
			cmdParams.Add(parSystemLockdownMessage);
			System.Data.IDbDataParameter parSystemLockdownReasonID = cmd.CreateParameter();
			parSystemLockdownReasonID.ParameterName = "@SystemLockdownReasonID";
			if(entity != null)
				parSystemLockdownReasonID.Value = entity.SystemLockdownReasonID;
			else
				parSystemLockdownReasonID.Value = System.DBNull.Value;
			cmdParams.Add(parSystemLockdownReasonID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql update command, using the values from the passed
		/// in SystemLockdownInfo object
		/// </summary>
		/// <param name="o">A SystemLockdownInfo object, from which the update values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(SystemLockdownInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_SystemLockdown");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter parAccountID = cmd.CreateParameter();
			parAccountID.ParameterName = "@AccountID";
			if(entity != null)
				parAccountID.Value = entity.AccountID;
			else
				parAccountID.Value = System.DBNull.Value;
			cmdParams.Add(parAccountID);
			System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
			parAddedBy.ParameterName = "@AddedBy";
			if(entity != null)
				parAddedBy.Value = entity.AddedBy;
			else
				parAddedBy.Value = System.DBNull.Value;
			cmdParams.Add(parAddedBy);
			System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
			parCompanyID.ParameterName = "@CompanyID";
			if(entity != null)
				parCompanyID.Value = entity.CompanyID;
			else
				parCompanyID.Value = System.DBNull.Value;
			cmdParams.Add(parCompanyID);
			System.Data.IDbDataParameter parDataImportID = cmd.CreateParameter();
			parDataImportID.ParameterName = "@DataImportID";
			if(entity != null)
				parDataImportID.Value = entity.DataImportID;
			else
				parDataImportID.Value = System.DBNull.Value;
			cmdParams.Add(parDataImportID);
			System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
			parDateAdded.ParameterName = "@DateAdded";
			if(entity != null)
				parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
			else
				parDateAdded.Value = System.DBNull.Value;
			cmdParams.Add(parDateAdded);
			System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
			parRecPeriodID.ParameterName = "@RecPeriodID";
			if(entity != null)
				parRecPeriodID.Value = entity.RecPeriodID;
			else
				parRecPeriodID.Value = System.DBNull.Value;
			cmdParams.Add(parRecPeriodID);
			System.Data.IDbDataParameter parSystemLockdownMessage = cmd.CreateParameter();
			parSystemLockdownMessage.ParameterName = "@SystemLockdownMessage";
			if(entity != null)
				parSystemLockdownMessage.Value = entity.SystemLockdownMessage;
			else
				parSystemLockdownMessage.Value = System.DBNull.Value;
			cmdParams.Add(parSystemLockdownMessage);
			System.Data.IDbDataParameter parSystemLockdownReasonID = cmd.CreateParameter();
			parSystemLockdownReasonID.ParameterName = "@SystemLockdownReasonID";
			if(entity != null)
				parSystemLockdownReasonID.Value = entity.SystemLockdownReasonID;
			else
				parSystemLockdownReasonID.Value = System.DBNull.Value;
			cmdParams.Add(parSystemLockdownReasonID);
			System.Data.IDbDataParameter pkparSystemLockdownID = cmd.CreateParameter();
			pkparSystemLockdownID.ParameterName = "@SystemLockdownID";
			pkparSystemLockdownID.Value = entity.SystemLockdownID;
			cmdParams.Add(pkparSystemLockdownID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql delete command, using the passed in primary key
		/// </summary>
		/// <param name="id">The primary key of the object to delete</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_SystemLockdown");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@SystemLockdownID";
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
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_SystemLockdown");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@SystemLockdownID";
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
		public IList<SystemLockdownInfo> SelectAllByCompanyID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_SystemLockdownByCompanyID");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@CompanyID";
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
		public IList<SystemLockdownInfo> SelectAllByRecPeriodID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_SystemLockdownByRecPeriodID");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@RecPeriodID";
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
		public IList<SystemLockdownInfo> SelectAllByDataImportID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_SystemLockdownByDataImportID");
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
		public IList<SystemLockdownInfo> SelectAllByAccountID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_SystemLockdownByAccountID");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@AccountID";
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
		public IList<SystemLockdownInfo> SelectAllBySystemLockdownReasonID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_SystemLockdownBySystemLockdownReasonID");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@SystemLockdownReasonID";
			par.Value = id;
			cmdParams.Add(par);
            return this.Select(cmd);
		}
		protected override void CustomSave(SystemLockdownInfo o, IDbConnection connection)
		{
			string query = QueryHelper.GetSqlServerLastInsertedCommand(SystemLockdownDAO.TABLE_NAME);
			IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
			cmd.CommandText = query;
			cmd.Connection = connection;
			object id = cmd.ExecuteScalar();
			this.MapIdentity(o, id);
		}
		
		protected override void CustomSave(SystemLockdownInfo o, IDbConnection connection, IDbTransaction transaction)
		{
			string query = QueryHelper.GetSqlServerLastInsertedCommand(SystemLockdownDAO.TABLE_NAME);
			IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
			cmd.CommandText = query;
			cmd.Transaction = transaction;
			cmd.Connection = connection;
			object id = cmd.ExecuteScalar();
			this.MapIdentity(o, id);
		}		
		private void MapIdentity(SystemLockdownInfo entity, object id)
		{
			entity.SystemLockdownID = Convert.ToInt32(id);
		}
	
    }
}
