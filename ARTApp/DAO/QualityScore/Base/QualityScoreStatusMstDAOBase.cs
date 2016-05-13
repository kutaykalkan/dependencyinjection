
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
	public abstract class QualityScoreStatusMstDAOBase : CustomAbstractDAO<QualityScoreStatusMstInfo> 
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
		/// A static representation of column HostName
		/// </summary>
		public static readonly string COLUMN_HOSTNAME = "HostName";
		/// <summary>
		/// A static representation of column IsActive
		/// </summary>
		public static readonly string COLUMN_ISACTIVE = "IsActive";
		/// <summary>
		/// A static representation of column QualityScoreStatus
		/// </summary>
		public static readonly string COLUMN_QUALITYSCORESTATUS = "QualityScoreStatus";
		/// <summary>
		/// A static representation of column QualityScoreStatusID
		/// </summary>
		public static readonly string COLUMN_QUALITYSCORESTATUSID = "QualityScoreStatusID";
		/// <summary>
		/// A static representation of column QualityScoreStatusLabelID
		/// </summary>
		public static readonly string COLUMN_QUALITYSCORESTATUSLABELID = "QualityScoreStatusLabelID";
		/// <summary>
		/// A static representation of column RevisedBy
		/// </summary>
		public static readonly string COLUMN_REVISEDBY = "RevisedBy";
		/// <summary>
		/// Provides access to the name of the primary key column (QualityScoreStatusID)
		/// </summary>
		public static readonly string TABLE_PRIMARYKEY = "QualityScoreStatusID";

		/// <summary>
		/// Provides access to the name of the table
		/// </summary>
		public static readonly string TABLE_NAME = "QualityScoreStatusMst";

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
        public QualityScoreStatusMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "QualityScoreStatusMst", oAppUserInfo.ConnectionString) 
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }
        
        /// <summary>
        /// Maps the IDataReader values to a QualityScoreStatusMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>QualityScoreStatusMstInfo</returns>
        protected override QualityScoreStatusMstInfo MapObject(System.Data.IDataReader r) 
        {
            QualityScoreStatusMstInfo entity = new QualityScoreStatusMstInfo();
			entity.QualityScoreStatusID = r.GetInt16Value("QualityScoreStatusID");
			entity.QualityScoreStatus = r.GetStringValue("QualityScoreStatus");
			entity.QualityScoreStatusLabelID = r.GetInt32Value("QualityScoreStatusLabelID");
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
		/// in QualityScoreStatusMstInfo object
		/// </summary>
		/// <param name="o">A QualityScoreStatusMstInfo object, from which the insert values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(QualityScoreStatusMstInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_QualityScoreStatusMst");
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
			System.Data.IDbDataParameter parQualityScoreStatus = cmd.CreateParameter();
			parQualityScoreStatus.ParameterName = "@QualityScoreStatus";
			if(entity != null)
				parQualityScoreStatus.Value = entity.QualityScoreStatus;
			else
				parQualityScoreStatus.Value = System.DBNull.Value;
			cmdParams.Add(parQualityScoreStatus);
			System.Data.IDbDataParameter parQualityScoreStatusID = cmd.CreateParameter();
			parQualityScoreStatusID.ParameterName = "@QualityScoreStatusID";
			if(entity != null)
				parQualityScoreStatusID.Value = entity.QualityScoreStatusID;
			else
				parQualityScoreStatusID.Value = System.DBNull.Value;
			cmdParams.Add(parQualityScoreStatusID);
			System.Data.IDbDataParameter parQualityScoreStatusLabelID = cmd.CreateParameter();
			parQualityScoreStatusLabelID.ParameterName = "@QualityScoreStatusLabelID";
			if(entity != null)
				parQualityScoreStatusLabelID.Value = entity.QualityScoreStatusLabelID;
			else
				parQualityScoreStatusLabelID.Value = System.DBNull.Value;
			cmdParams.Add(parQualityScoreStatusLabelID);
			System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
			parRevisedBy.ParameterName = "@RevisedBy";
			if(entity != null)
				parRevisedBy.Value = entity.RevisedBy;
			else
				parRevisedBy.Value = System.DBNull.Value;
			cmdParams.Add(parRevisedBy);
			return cmd;
        }

		/// <summary>
		/// Creates the sql update command, using the values from the passed
		/// in QualityScoreStatusMstInfo object
		/// </summary>
		/// <param name="o">A QualityScoreStatusMstInfo object, from which the update values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(QualityScoreStatusMstInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_QualityScoreStatusMst");
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
			System.Data.IDbDataParameter parQualityScoreStatus = cmd.CreateParameter();
			parQualityScoreStatus.ParameterName = "@QualityScoreStatus";
			if(entity != null)
				parQualityScoreStatus.Value = entity.QualityScoreStatus;
			else
				parQualityScoreStatus.Value = System.DBNull.Value;
			cmdParams.Add(parQualityScoreStatus);
			System.Data.IDbDataParameter parQualityScoreStatusLabelID = cmd.CreateParameter();
			parQualityScoreStatusLabelID.ParameterName = "@QualityScoreStatusLabelID";
			if(entity != null)
				parQualityScoreStatusLabelID.Value = entity.QualityScoreStatusLabelID;
			else
				parQualityScoreStatusLabelID.Value = System.DBNull.Value;
			cmdParams.Add(parQualityScoreStatusLabelID);
			System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
			parRevisedBy.ParameterName = "@RevisedBy";
			if(entity != null)
				parRevisedBy.Value = entity.RevisedBy;
			else
				parRevisedBy.Value = System.DBNull.Value;
			cmdParams.Add(parRevisedBy);
			System.Data.IDbDataParameter pkparQualityScoreStatusID = cmd.CreateParameter();
			pkparQualityScoreStatusID.ParameterName = "@QualityScoreStatusID";
			pkparQualityScoreStatusID.Value = entity.QualityScoreStatusID;
			cmdParams.Add(pkparQualityScoreStatusID);
			return cmd;
        }

		/// <summary>
		/// Creates the sql delete command, using the passed in primary key
		/// </summary>
		/// <param name="id">The primary key of the object to delete</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_QualityScoreStatusMst");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@QualityScoreStatusID";
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
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_QualityScoreStatusMst");
			cmd.CommandType = CommandType.StoredProcedure;
			IDataParameterCollection cmdParams = cmd.Parameters;
			System.Data.IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = "@QualityScoreStatusID";
			par.Value = id;
			cmdParams.Add(par);
            return cmd;
        }

	
    }
}
