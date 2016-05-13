
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Model.QualityScore;
using SkyStem.ART.App.DAO.Base;

namespace SkyStem.ART.App.DAO.QualityScore.Base 
{
	public abstract class GLDataQualityScoreDAOBase : CustomAbstractDAO<GLDataQualityScoreInfo> 
	{
		/// <summary>
		/// A static representation of column AddedBy
		/// </summary>
		public static readonly string COLUMN_ADDEDBY = "AddedBy";
		/// <summary>
		/// A static representation of column AddedByUserID
		/// </summary>
		public static readonly string COLUMN_ADDEDBYUSERID = "AddedByUserID";
		/// <summary>
		/// A static representation of column Comments
		/// </summary>
		public static readonly string COLUMN_COMMENTS = "Comments";
		/// <summary>
		/// A static representation of column CompanyQualityScoreID
		/// </summary>
		public static readonly string COLUMN_COMPANYQUALITYSCOREID = "CompanyQualityScoreID";
		/// <summary>
		/// A static representation of column DateAdded
		/// </summary>
		public static readonly string COLUMN_DATEADDED = "DateAdded";
		/// <summary>
		/// A static representation of column DateRevised
		/// </summary>
		public static readonly string COLUMN_DATEREVISED = "DateRevised";
		/// <summary>
		/// A static representation of column GLDataID
		/// </summary>
		public static readonly string COLUMN_GLDATAID = "GLDataID";
		/// <summary>
		/// A static representation of column GLDataQualityScoreID
		/// </summary>
		public static readonly string COLUMN_GLDATAQUALITYSCOREID = "GLDataQualityScoreID";
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
		/// A static representation of column SystemQualityScoreStatusID
		/// </summary>
		public static readonly string COLUMN_SYSTEMQUALITYSCORESTATUSID = "SystemQualityScoreStatusID";
		/// <summary>
		/// A static representation of column UserQualityScoreStatusID
		/// </summary>
		public static readonly string COLUMN_USERQUALITYSCORESTATUSID = "UserQualityScoreStatusID";
		/// <summary>
		/// Provides access to the name of the primary key column (GLDataQualityScoreID)
		/// </summary>
		public static readonly string TABLE_PRIMARYKEY = "GLDataQualityScoreID";

		/// <summary>
		/// Provides access to the name of the table
		/// </summary>
		public static readonly string TABLE_NAME = "GLDataQualityScore";

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
        public GLDataQualityScoreDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "GLDataQualityScore", oAppUserInfo.ConnectionString) 
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }
        
        /// <summary>
        /// Maps the IDataReader values to a GLDataQualityScoreInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>GLDataQualityScoreInfo</returns>
        protected override GLDataQualityScoreInfo MapObject(System.Data.IDataReader r) 
        {
            GLDataQualityScoreInfo entity = new GLDataQualityScoreInfo();
			entity.GLDataQualityScoreID = r.GetInt64Value("GLDataQualityScoreID");
			entity.GLDataID = r.GetInt64Value("GLDataID");
			entity.CompanyQualityScoreID = r.GetInt32Value("CompanyQualityScoreID");
            entity.SystemQualityScoreStatusID = r.GetInt16Value("SystemQualityScoreStatusID");
            entity.UserQualityScoreStatusID = r.GetInt16Value("UserQualityScoreStatusID");
			entity.Comments = r.GetStringValue("Comments");
			entity.IsActive = r.GetBooleanValue("IsActive");
			entity.DateAdded = r.GetDateValue("DateAdded");
			entity.AddedBy = r.GetStringValue("AddedBy");
			entity.AddedByUserID = r.GetInt32Value("AddedByUserID");
			entity.DateRevised = r.GetDateValue("DateRevised");
			entity.RevisedBy = r.GetStringValue("RevisedBy");
			entity.HostName = r.GetStringValue("HostName");
            return entity;
        }
 
		/// <summary>
		/// Creates the sql insert command, using the values from the passed
		/// in GLDataQualityScoreInfo object
		/// </summary>
		/// <param name="o">A GLDataQualityScoreInfo object, from which the insert values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(GLDataQualityScoreInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_GLDataQualityScore");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
			parAddedBy.ParameterName = "@AddedBy";
			if(entity != null)
				parAddedBy.Value = entity.AddedBy;
			else
				parAddedBy.Value = System.DBNull.Value;
			cmdParams.Add(parAddedBy);
			System.Data.IDbDataParameter parAddedByUserID = cmd.CreateParameter();
			parAddedByUserID.ParameterName = "@AddedByUserID";
			if(entity != null)
				parAddedByUserID.Value = entity.AddedByUserID;
			else
				parAddedByUserID.Value = System.DBNull.Value;
			cmdParams.Add(parAddedByUserID);
			System.Data.IDbDataParameter parComments = cmd.CreateParameter();
			parComments.ParameterName = "@Comments";
			if(entity != null)
				parComments.Value = entity.Comments;
			else
				parComments.Value = System.DBNull.Value;
			cmdParams.Add(parComments);
			System.Data.IDbDataParameter parCompanyQualityScoreID = cmd.CreateParameter();
			parCompanyQualityScoreID.ParameterName = "@CompanyQualityScoreID";
			if(entity != null)
				parCompanyQualityScoreID.Value = entity.CompanyQualityScoreID;
			else
				parCompanyQualityScoreID.Value = System.DBNull.Value;
			cmdParams.Add(parCompanyQualityScoreID);
			System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
			parDateAdded.ParameterName = "@DateAdded";
			if(entity != null)
				parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
			else
				parDateAdded.Value = System.DBNull.Value;
			cmdParams.Add(parDateAdded);
			System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
			parDateRevised.ParameterName = "@DateRevised";
			if(entity != null)
				parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
			else
				parDateRevised.Value = System.DBNull.Value;
			cmdParams.Add(parDateRevised);
			System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
			parGLDataID.ParameterName = "@GLDataID";
			if(entity != null)
				parGLDataID.Value = entity.GLDataID;
			else
				parGLDataID.Value = System.DBNull.Value;
			cmdParams.Add(parGLDataID);
			System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
			parHostName.ParameterName = "@HostName";
			if(entity != null)
				parHostName.Value = entity.HostName;
			else
				parHostName.Value = System.DBNull.Value;
			cmdParams.Add(parHostName);
			System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
			parIsActive.ParameterName = "@IsActive";
			if(entity != null)
				parIsActive.Value = entity.IsActive;
			else
				parIsActive.Value = System.DBNull.Value;
			cmdParams.Add(parIsActive);
			System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
			parRevisedBy.ParameterName = "@RevisedBy";
			if(entity != null)
				parRevisedBy.Value = entity.RevisedBy;
			else
				parRevisedBy.Value = System.DBNull.Value;
			cmdParams.Add(parRevisedBy);
			System.Data.IDbDataParameter parSystemQualityScoreStatusID = cmd.CreateParameter();
            parSystemQualityScoreStatusID.ParameterName = "@SystemQualityScoreStatusID";
			if(entity != null)
				parSystemQualityScoreStatusID.Value = entity.SystemQualityScoreStatusID;
			else
				parSystemQualityScoreStatusID.Value = System.DBNull.Value;
			cmdParams.Add(parSystemQualityScoreStatusID);
			System.Data.IDbDataParameter parUserQualityScoreStatusID = cmd.CreateParameter();
			parUserQualityScoreStatusID.ParameterName = "@UserQualityScoreStatusID";
			if(entity != null)
				parUserQualityScoreStatusID.Value = entity.UserQualityScoreStatusID;
			else
				parUserQualityScoreStatusID.Value = System.DBNull.Value;
			cmdParams.Add(parUserQualityScoreStatusID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql update command, using the values from the passed
		/// in GLDataQualityScoreInfo object
		/// </summary>
		/// <param name="o">A GLDataQualityScoreInfo object, from which the update values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(GLDataQualityScoreInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_GLDataQualityScore");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
			parAddedBy.ParameterName = "@AddedBy";
			if(entity != null)
				parAddedBy.Value = entity.AddedBy;
			else
				parAddedBy.Value = System.DBNull.Value;
			cmdParams.Add(parAddedBy);
			System.Data.IDbDataParameter parAddedByUserID = cmd.CreateParameter();
			parAddedByUserID.ParameterName = "@AddedByUserID";
			if(entity != null)
				parAddedByUserID.Value = entity.AddedByUserID;
			else
				parAddedByUserID.Value = System.DBNull.Value;
			cmdParams.Add(parAddedByUserID);
			System.Data.IDbDataParameter parComments = cmd.CreateParameter();
			parComments.ParameterName = "@Comments";
			if(entity != null)
				parComments.Value = entity.Comments;
			else
				parComments.Value = System.DBNull.Value;
			cmdParams.Add(parComments);
			System.Data.IDbDataParameter parCompanyQualityScoreID = cmd.CreateParameter();
			parCompanyQualityScoreID.ParameterName = "@CompanyQualityScoreID";
			if(entity != null)
				parCompanyQualityScoreID.Value = entity.CompanyQualityScoreID;
			else
				parCompanyQualityScoreID.Value = System.DBNull.Value;
			cmdParams.Add(parCompanyQualityScoreID);
			System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
			parDateAdded.ParameterName = "@DateAdded";
			if(entity != null)
				parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
			else
				parDateAdded.Value = System.DBNull.Value;
			cmdParams.Add(parDateAdded);
			System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
			parDateRevised.ParameterName = "@DateRevised";
			if(entity != null)
				parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
			else
				parDateRevised.Value = System.DBNull.Value;
			cmdParams.Add(parDateRevised);
			System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
			parGLDataID.ParameterName = "@GLDataID";
			if(entity != null)
				parGLDataID.Value = entity.GLDataID;
			else
				parGLDataID.Value = System.DBNull.Value;
			cmdParams.Add(parGLDataID);
			System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
			parHostName.ParameterName = "@HostName";
			if(entity != null)
				parHostName.Value = entity.HostName;
			else
				parHostName.Value = System.DBNull.Value;
			cmdParams.Add(parHostName);
			System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
			parIsActive.ParameterName = "@IsActive";
			if(entity != null)
				parIsActive.Value = entity.IsActive;
			else
				parIsActive.Value = System.DBNull.Value;
			cmdParams.Add(parIsActive);
			System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
			parRevisedBy.ParameterName = "@RevisedBy";
			if(entity != null)
				parRevisedBy.Value = entity.RevisedBy;
			else
				parRevisedBy.Value = System.DBNull.Value;
			cmdParams.Add(parRevisedBy);
			System.Data.IDbDataParameter parSystemQualityScoreStatusID = cmd.CreateParameter();
			parSystemQualityScoreStatusID.ParameterName = "@SystemQualityScoreStatusID";
			if(entity != null)
				parSystemQualityScoreStatusID.Value = entity.SystemQualityScoreStatusID;
			else
				parSystemQualityScoreStatusID.Value = System.DBNull.Value;
			cmdParams.Add(parSystemQualityScoreStatusID);
			System.Data.IDbDataParameter parUserQualityScoreStatusID = cmd.CreateParameter();
			parUserQualityScoreStatusID.ParameterName = "@UserQualityScoreStatusID";
			if(entity != null)
				parUserQualityScoreStatusID.Value = entity.UserQualityScoreStatusID;
			else
				parUserQualityScoreStatusID.Value = System.DBNull.Value;
			cmdParams.Add(parUserQualityScoreStatusID);
			System.Data.IDbDataParameter pkparGLDataQualityScoreID = cmd.CreateParameter();
			pkparGLDataQualityScoreID.ParameterName = "@GLDataQualityScoreID";
			pkparGLDataQualityScoreID.Value = entity.GLDataQualityScoreID;
			cmdParams.Add(pkparGLDataQualityScoreID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql delete command, using the passed in primary key
		/// </summary>
		/// <param name="id">The primary key of the object to delete</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_GLDataQualityScore");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@GLDataQualityScoreID";
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
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_GLDataQualityScore");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@GLDataQualityScoreID";
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
		public IList<GLDataQualityScoreInfo> SelectAllByGLDataID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_GLDataQualityScoreByGLDataID");
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
		public IList<GLDataQualityScoreInfo> SelectAllByCompanyQualityScoreID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_GLDataQualityScoreByCompanyQualityScoreID");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@CompanyQualityScoreID";
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
		public IList<GLDataQualityScoreInfo> SelectAllByAddedByUserID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_GLDataQualityScoreByAddedByUserID");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@AddedByUserID";
			par.Value = id;
			cmdParams.Add(par);
            return this.Select(cmd);
		}
		protected override void CustomSave(GLDataQualityScoreInfo o, IDbConnection connection)
		{
			string query = QueryHelper.GetSqlServerLastInsertedCommand(GLDataQualityScoreDAO.TABLE_NAME);
			IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
			cmd.CommandText = query;
			cmd.Connection = connection;
			object id = cmd.ExecuteScalar();
			this.MapIdentity(o, id);
		}
		
		protected override void CustomSave(GLDataQualityScoreInfo o, IDbConnection connection, IDbTransaction transaction)
		{
			string query = QueryHelper.GetSqlServerLastInsertedCommand(GLDataQualityScoreDAO.TABLE_NAME);
			IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
			cmd.CommandText = query;
			cmd.Transaction = transaction;
			cmd.Connection = connection;
			object id = cmd.ExecuteScalar();
			this.MapIdentity(o, id);
		}		
		private void MapIdentity(GLDataQualityScoreInfo entity, object id)
		{
			entity.GLDataQualityScoreID = Convert.ToInt64(id);
		}
	
    }
}
