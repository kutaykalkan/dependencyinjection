
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
	public abstract class QualityScoreMstDAOBase : CustomAbstractDAO<QualityScoreMstInfo> 
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
		/// A static representation of column Description
		/// </summary>
		public static readonly string COLUMN_DESCRIPTION = "Description";
		/// <summary>
		/// A static representation of column DescriptionLabelID
		/// </summary>
		public static readonly string COLUMN_DESCRIPTIONLABELID = "DescriptionLabelID";
		/// <summary>
		/// A static representation of column HostName
		/// </summary>
		public static readonly string COLUMN_HOSTNAME = "HostName";
		/// <summary>
		/// A static representation of column IsActive
		/// </summary>
		public static readonly string COLUMN_ISACTIVE = "IsActive";
		/// <summary>
		/// A static representation of column IsApplicableForSRA
		/// </summary>
		public static readonly string COLUMN_ISAPPLICABLEFORSRA = "IsApplicableForSRA";
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
		/// Provides access to the name of the primary key column (QualityScoreID)
		/// </summary>
		public static readonly string TABLE_PRIMARYKEY = "QualityScoreID";

		/// <summary>
		/// Provides access to the name of the table
		/// </summary>
		public static readonly string TABLE_NAME = "QualityScoreMst";

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
        public QualityScoreMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "QualityScoreMst", oAppUserInfo.ConnectionString) 
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }
        
        /// <summary>
        /// Maps the IDataReader values to a QualityScoreMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>QualityScoreMstInfo</returns>
        protected override QualityScoreMstInfo MapObject(System.Data.IDataReader r) 
        {
            QualityScoreMstInfo entity = new QualityScoreMstInfo();
			entity.QualityScoreID = r.GetInt32Value("QualityScoreID");
			entity.Description = r.GetStringValue("Description");
			entity.DescriptionLabelID = r.GetInt32Value("DescriptionLabelID");
			entity.IsApplicableForSRA = r.GetBooleanValue("IsApplicableForSRA");
			entity.IsUserScoreEnabled = r.GetBooleanValue("IsUserScoreEnabled");
			entity.Weightage = r.GetDecimalValue("Weightage");
			entity.SortOrder = r.GetInt32Value("SortOrder");
			entity.IsActive = r.GetBooleanValue("IsActive");
			entity.DateAdded = r.GetDateValue("DateAdded");
			entity.AddedBy = r.GetStringValue("AddedBy");
			entity.DateRevised = r.GetDateValue("DateRevised");
			entity.RevisedBy = r.GetStringValue("RevisedBy");
			entity.HostName = r.GetStringValue("HostName");
            return entity;
        }
 
		/// <summary>
		/// Creates the sql insert command, using the values from the passed
		/// in QualityScoreMstInfo object
		/// </summary>
		/// <param name="o">A QualityScoreMstInfo object, from which the insert values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(QualityScoreMstInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_QualityScoreMst");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
			parAddedBy.ParameterName = "@AddedBy";
			if(entity != null)
				parAddedBy.Value = entity.AddedBy;
			else
				parAddedBy.Value = System.DBNull.Value;
			cmdParams.Add(parAddedBy);
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
			System.Data.IDbDataParameter parIsApplicableForSRA = cmd.CreateParameter();
			parIsApplicableForSRA.ParameterName = "@IsApplicableForSRA";
			if(entity != null)
				parIsApplicableForSRA.Value = entity.IsApplicableForSRA;
			else
				parIsApplicableForSRA.Value = System.DBNull.Value;
			cmdParams.Add(parIsApplicableForSRA);
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
		/// in QualityScoreMstInfo object
		/// </summary>
		/// <param name="o">A QualityScoreMstInfo object, from which the update values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(QualityScoreMstInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_QualityScoreMst");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
			parAddedBy.ParameterName = "@AddedBy";
			if(entity != null)
				parAddedBy.Value = entity.AddedBy;
			else
				parAddedBy.Value = System.DBNull.Value;
			cmdParams.Add(parAddedBy);
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
			System.Data.IDbDataParameter parIsApplicableForSRA = cmd.CreateParameter();
			parIsApplicableForSRA.ParameterName = "@IsApplicableForSRA";
			if(entity != null)
				parIsApplicableForSRA.Value = entity.IsApplicableForSRA;
			else
				parIsApplicableForSRA.Value = System.DBNull.Value;
			cmdParams.Add(parIsApplicableForSRA);
			System.Data.IDbDataParameter parIsUserScoreEnabled = cmd.CreateParameter();
			parIsUserScoreEnabled.ParameterName = "@IsUserScoreEnabled";
			if(entity != null)
				parIsUserScoreEnabled.Value = entity.IsUserScoreEnabled;
			else
				parIsUserScoreEnabled.Value = System.DBNull.Value;
			cmdParams.Add(parIsUserScoreEnabled);
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
			System.Data.IDbDataParameter pkparQualityScoreID = cmd.CreateParameter();
			pkparQualityScoreID.ParameterName = "@QualityScoreID";
			pkparQualityScoreID.Value = entity.QualityScoreID;
			cmdParams.Add(pkparQualityScoreID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql delete command, using the passed in primary key
		/// </summary>
		/// <param name="id">The primary key of the object to delete</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_QualityScoreMst");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@QualityScoreID";
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
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_QualityScoreMst");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@QualityScoreID";
			par.Value = id;
			cmdParams.Add(par);
            return cmd;
        }

	
    }
}
