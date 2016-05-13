
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
	public abstract class CompanyQualityScoreDAOBase : CustomAbstractDAO <CompanyQualityScoreInfo>//AbstractDAO<CompanyQualityScoreInfo> 
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
		/// A static representation of column CompanyQualityScoreID
		/// </summary>
		public static readonly string COLUMN_COMPANYQUALITYSCOREID = "CompanyQualityScoreID";
		/// <summary>
		/// A static representation of column CompanyRecPeriodSetID
		/// </summary>
		public static readonly string COLUMN_COMPANYRECPERIODSETID = "CompanyRecPeriodSetID";
		/// <summary>
		/// A static representation of column DateAdded
		/// </summary>
		public static readonly string COLUMN_DATEADDED = "DateAdded";
		/// <summary>
		/// A static representation of column DateRevised
		/// </summary>
		public static readonly string COLUMN_DATEREVISED = "DateRevised";
		/// <summary>
		/// A static representation of column Description
		/// </summary>
		public static readonly string COLUMN_DESCRIPTION = "Description";
		/// <summary>
		/// A static representation of column DescriptionLabelID
		/// </summary>
		public static readonly string COLUMN_DESCRIPTIONLABELID = "DescriptionLabelID";
		/// <summary>
		/// A static representation of column IsActive
		/// </summary>
		public static readonly string COLUMN_ISACTIVE = "IsActive";
		/// <summary>
		/// A static representation of column IsApplicableForSRA
		/// </summary>
		public static readonly string COLUMN_ISAPPLICABLEFORSRA = "IsApplicableForSRA";
		/// <summary>
		/// A static representation of column IsEnabled
		/// </summary>
		public static readonly string COLUMN_ISENABLED = "IsEnabled";
		/// <summary>
		/// A static representation of column IsUserScoreEnabled
		/// </summary>
		public static readonly string COLUMN_ISUSERSCOREENABLED = "IsUserScoreEnabled";
		/// <summary>
		/// A static representation of column QualityScoreID
		/// </summary>
		public static readonly string COLUMN_QUALITYSCOREID = "QualityScoreID";
		/// <summary>
		/// A static representation of column RevisedBy
		/// </summary>
		public static readonly string COLUMN_REVISEDBY = "RevisedBy";
		/// <summary>
		/// A static representation of column SortOrder
		/// </summary>
		public static readonly string COLUMN_SORTORDER = "SortOrder";
		/// <summary>
		/// A static representation of column Weightage
		/// </summary>
		public static readonly string COLUMN_WEIGHTAGE = "Weightage";
		/// <summary>
		/// Provides access to the name of the primary key column (CompanyQualityScoreID)
		/// </summary>
		public static readonly string TABLE_PRIMARYKEY = "CompanyQualityScoreID";

		/// <summary>
		/// Provides access to the name of the table
		/// </summary>
		public static readonly string TABLE_NAME = "CompanyQualityScore";

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
        public CompanyQualityScoreDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "CompanyQualityScore", oAppUserInfo.ConnectionString) 
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }
        
        /// <summary>
        /// Maps the IDataReader values to a CompanyQualityScoreInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>CompanyQualityScoreInfo</returns>
        protected override CompanyQualityScoreInfo MapObject(System.Data.IDataReader r) 
        {
            CompanyQualityScoreInfo entity = new CompanyQualityScoreInfo();
			entity.CompanyQualityScoreID = r.GetInt32Value("CompanyQualityScoreID");
			entity.CompanyRecPeriodSetID = r.GetInt32Value("CompanyRecPeriodSetID");
			entity.QualityScoreID = r.GetInt32Value("QualityScoreID");
			entity.Description = r.GetStringValue("Description");
			entity.DescriptionLabelID = r.GetInt32Value("DescriptionLabelID");
            entity.QualityScoreNumber = r.GetStringValue("QualityScoreNumber");
			entity.IsApplicableForSRA = r.GetBooleanValue("IsApplicableForSRA");
			entity.IsUserScoreEnabled = r.GetBooleanValue("IsUserScoreEnabled");
			entity.Weightage = r.GetDecimalValue("Weightage");
			entity.SortOrder = r.GetInt32Value("SortOrder");
			entity.IsEnabled = r.GetBooleanValue("IsEnabled");
			entity.IsActive = r.GetBooleanValue("IsActive");
			entity.DateAdded = r.GetDateValue("DateAdded");
			entity.AddedBy = r.GetStringValue("AddedBy");
			entity.AddedByUserID = r.GetInt32Value("AddedByUserID");
			entity.DateRevised = r.GetDateValue("DateRevised");
			entity.RevisedBy = r.GetStringValue("RevisedBy");
            return entity;
        }
 
		/// <summary>
		/// Creates the sql insert command, using the values from the passed
		/// in CompanyQualityScoreInfo object
		/// </summary>
		/// <param name="o">A CompanyQualityScoreInfo object, from which the insert values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(CompanyQualityScoreInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_CompanyQualityScore");
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
			System.Data.IDbDataParameter parCompanyRecPeriodSetID = cmd.CreateParameter();
			parCompanyRecPeriodSetID.ParameterName = "@CompanyRecPeriodSetID";
			if(entity != null)
				parCompanyRecPeriodSetID.Value = entity.CompanyRecPeriodSetID;
			else
				parCompanyRecPeriodSetID.Value = System.DBNull.Value;
			cmdParams.Add(parCompanyRecPeriodSetID);
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
			System.Data.IDbDataParameter parDescription = cmd.CreateParameter();
			parDescription.ParameterName = "@Description";
			if(entity != null)
				parDescription.Value = entity.Description;
			else
				parDescription.Value = System.DBNull.Value;
			cmdParams.Add(parDescription);
			System.Data.IDbDataParameter parDescriptionLabelID = cmd.CreateParameter();
			parDescriptionLabelID.ParameterName = "@DescriptionLabelID";
			if(entity != null)
				parDescriptionLabelID.Value = entity.DescriptionLabelID;
			else
				parDescriptionLabelID.Value = System.DBNull.Value;
			cmdParams.Add(parDescriptionLabelID);
			System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
			parIsActive.ParameterName = "@IsActive";
			if(entity != null)
				parIsActive.Value = entity.IsActive;
			else
				parIsActive.Value = System.DBNull.Value;
			cmdParams.Add(parIsActive);
			System.Data.IDbDataParameter parIsApplicableForSRA = cmd.CreateParameter();
			parIsApplicableForSRA.ParameterName = "@IsApplicableForSRA";
			if(entity != null)
				parIsApplicableForSRA.Value = entity.IsApplicableForSRA;
			else
				parIsApplicableForSRA.Value = System.DBNull.Value;
			cmdParams.Add(parIsApplicableForSRA);
			System.Data.IDbDataParameter parIsEnabled = cmd.CreateParameter();
			parIsEnabled.ParameterName = "@IsEnabled";
			if(entity != null)
				parIsEnabled.Value = entity.IsEnabled;
			else
				parIsEnabled.Value = System.DBNull.Value;
			cmdParams.Add(parIsEnabled);
			System.Data.IDbDataParameter parIsUserScoreEnabled = cmd.CreateParameter();
			parIsUserScoreEnabled.ParameterName = "@IsUserScoreEnabled";
			if(entity != null)
				parIsUserScoreEnabled.Value = entity.IsUserScoreEnabled;
			else
				parIsUserScoreEnabled.Value = System.DBNull.Value;
			cmdParams.Add(parIsUserScoreEnabled);
			System.Data.IDbDataParameter parQualityScoreID = cmd.CreateParameter();
			parQualityScoreID.ParameterName = "@QualityScoreID";
			if(entity != null)
				parQualityScoreID.Value = entity.QualityScoreID;
			else
				parQualityScoreID.Value = System.DBNull.Value;
			cmdParams.Add(parQualityScoreID);
			System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
			parRevisedBy.ParameterName = "@RevisedBy";
			if(entity != null)
				parRevisedBy.Value = entity.RevisedBy;
			else
				parRevisedBy.Value = System.DBNull.Value;
			cmdParams.Add(parRevisedBy);
			System.Data.IDbDataParameter parSortOrder = cmd.CreateParameter();
			parSortOrder.ParameterName = "@SortOrder";
			if(entity != null)
				parSortOrder.Value = entity.SortOrder;
			else
				parSortOrder.Value = System.DBNull.Value;
			cmdParams.Add(parSortOrder);
			System.Data.IDbDataParameter parWeightage = cmd.CreateParameter();
			parWeightage.ParameterName = "@Weightage";
			if(entity != null)
				parWeightage.Value = entity.Weightage;
			else
				parWeightage.Value = System.DBNull.Value;
			cmdParams.Add(parWeightage);
			return cmd;
        }

		/// <summary>
		/// Creates the sql update command, using the values from the passed
		/// in CompanyQualityScoreInfo object
		/// </summary>
		/// <param name="o">A CompanyQualityScoreInfo object, from which the update values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(CompanyQualityScoreInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_CompanyQualityScore");
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
			System.Data.IDbDataParameter parCompanyRecPeriodSetID = cmd.CreateParameter();
			parCompanyRecPeriodSetID.ParameterName = "@CompanyRecPeriodSetID";
			if(entity != null)
				parCompanyRecPeriodSetID.Value = entity.CompanyRecPeriodSetID;
			else
				parCompanyRecPeriodSetID.Value = System.DBNull.Value;
			cmdParams.Add(parCompanyRecPeriodSetID);
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
			System.Data.IDbDataParameter parDescription = cmd.CreateParameter();
			parDescription.ParameterName = "@Description";
			if(entity != null)
				parDescription.Value = entity.Description;
			else
				parDescription.Value = System.DBNull.Value;
			cmdParams.Add(parDescription);
			System.Data.IDbDataParameter parDescriptionLabelID = cmd.CreateParameter();
			parDescriptionLabelID.ParameterName = "@DescriptionLabelID";
			if(entity != null)
				parDescriptionLabelID.Value = entity.DescriptionLabelID;
			else
				parDescriptionLabelID.Value = System.DBNull.Value;
			cmdParams.Add(parDescriptionLabelID);
			System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
			parIsActive.ParameterName = "@IsActive";
			if(entity != null)
				parIsActive.Value = entity.IsActive;
			else
				parIsActive.Value = System.DBNull.Value;
			cmdParams.Add(parIsActive);
			System.Data.IDbDataParameter parIsApplicableForSRA = cmd.CreateParameter();
			parIsApplicableForSRA.ParameterName = "@IsApplicableForSRA";
			if(entity != null)
				parIsApplicableForSRA.Value = entity.IsApplicableForSRA;
			else
				parIsApplicableForSRA.Value = System.DBNull.Value;
			cmdParams.Add(parIsApplicableForSRA);
			System.Data.IDbDataParameter parIsEnabled = cmd.CreateParameter();
			parIsEnabled.ParameterName = "@IsEnabled";
			if(entity != null)
				parIsEnabled.Value = entity.IsEnabled;
			else
				parIsEnabled.Value = System.DBNull.Value;
			cmdParams.Add(parIsEnabled);
			System.Data.IDbDataParameter parIsUserScoreEnabled = cmd.CreateParameter();
			parIsUserScoreEnabled.ParameterName = "@IsUserScoreEnabled";
			if(entity != null)
				parIsUserScoreEnabled.Value = entity.IsUserScoreEnabled;
			else
				parIsUserScoreEnabled.Value = System.DBNull.Value;
			cmdParams.Add(parIsUserScoreEnabled);
			System.Data.IDbDataParameter parQualityScoreID = cmd.CreateParameter();
			parQualityScoreID.ParameterName = "@QualityScoreID";
			if(entity != null)
				parQualityScoreID.Value = entity.QualityScoreID;
			else
				parQualityScoreID.Value = System.DBNull.Value;
			cmdParams.Add(parQualityScoreID);
			System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
			parRevisedBy.ParameterName = "@RevisedBy";
			if(entity != null)
				parRevisedBy.Value = entity.RevisedBy;
			else
				parRevisedBy.Value = System.DBNull.Value;
			cmdParams.Add(parRevisedBy);
			System.Data.IDbDataParameter parSortOrder = cmd.CreateParameter();
			parSortOrder.ParameterName = "@SortOrder";
			if(entity != null)
				parSortOrder.Value = entity.SortOrder;
			else
				parSortOrder.Value = System.DBNull.Value;
			cmdParams.Add(parSortOrder);
			System.Data.IDbDataParameter parWeightage = cmd.CreateParameter();
			parWeightage.ParameterName = "@Weightage";
			if(entity != null)
				parWeightage.Value = entity.Weightage;
			else
				parWeightage.Value = System.DBNull.Value;
			cmdParams.Add(parWeightage);
			System.Data.IDbDataParameter pkparCompanyQualityScoreID = cmd.CreateParameter();
			pkparCompanyQualityScoreID.ParameterName = "@CompanyQualityScoreID";
			pkparCompanyQualityScoreID.Value = entity.CompanyQualityScoreID;
			cmdParams.Add(pkparCompanyQualityScoreID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql delete command, using the passed in primary key
		/// </summary>
		/// <param name="id">The primary key of the object to delete</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_CompanyQualityScore");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@CompanyQualityScoreID";
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
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_CompanyQualityScore");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@CompanyQualityScoreID";
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
		public IList<CompanyQualityScoreInfo> SelectAllByCompanyRecPeriodSetID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CompanyQualityScoreByCompanyRecPeriodSetID");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@CompanyRecPeriodSetID";
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
		public IList<CompanyQualityScoreInfo> SelectAllByQualityScoreID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CompanyQualityScoreByQualityScoreID");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@QualityScoreID";
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
		public IList<CompanyQualityScoreInfo> SelectAllByAddedByUserID(object id)
		{
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CompanyQualityScoreByAddedByUserID");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@AddedByUserID";
			par.Value = id;
			cmdParams.Add(par);
            return this.Select(cmd);
		}
		protected override void CustomSave(CompanyQualityScoreInfo o, IDbConnection connection)
		{
			string query = QueryHelper.GetSqlServerLastInsertedCommand(CompanyQualityScoreDAO.TABLE_NAME);
			IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
			cmd.CommandText = query;
			cmd.Connection = connection;
			object id = cmd.ExecuteScalar();
			this.MapIdentity(o, id);
		}
		
		protected override void CustomSave(CompanyQualityScoreInfo o, IDbConnection connection, IDbTransaction transaction)
		{
			string query = QueryHelper.GetSqlServerLastInsertedCommand(CompanyQualityScoreDAO.TABLE_NAME);
			IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
			cmd.CommandText = query;
			cmd.Transaction = transaction;
			cmd.Connection = connection;
			object id = cmd.ExecuteScalar();
			this.MapIdentity(o, id);
		}		
		private void MapIdentity(CompanyQualityScoreInfo entity, object id)
		{
			entity.CompanyQualityScoreID = Convert.ToInt32(id);
		}
	
    }
}
