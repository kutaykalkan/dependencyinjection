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
    public abstract class QualityScoreRangeDAOBase : CustomAbstractDAO<RangeOfScoreMstInfo> 
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
        public QualityScoreRangeDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "QualityScoreRangeMst", oAppUserInfo.ConnectionString) 
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }
        
        /// <summary>
        /// Maps the IDataReader values to a QualityScoreRangeMstInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>QualityScoreStatusMstInfo</returns>
        protected override RangeOfScoreMstInfo MapObject(System.Data.IDataReader r) 
        {
            RangeOfScoreMstInfo entity = new RangeOfScoreMstInfo();
			entity.RangeOfScoreCategoryID = r.GetInt32Value("QualityScoreRangeID");
			entity.RangeOfScoreCategoryName = r.GetStringValue("QualityScoreRangeName");
			entity.RangeOfscoreCategoryLabelID = r.GetInt32Value("QualityScoreRangeLabelID");
            return entity;
        }

        /// <summary>
        /// Maps the IDataReader values to a QualityScoreChecklistInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>QualityScoreStatusMstInfo</returns>
        protected QualityScoreChecklistInfo MapCheckListObject(System.Data.IDataReader r)
        {
            QualityScoreChecklistInfo entity = new QualityScoreChecklistInfo();
            entity.QualityScoreID = r.GetInt32Value("QualityScoreID");
            entity.QualityScoreDescriptionLabelID = r.GetInt32Value("DescriptionLabelID");
            return entity;
        }

        protected QualityScoreChecklistInfo MapCheckListObjectForSavedReports(System.Data.IDataReader r)
        {
            QualityScoreChecklistInfo entity = new QualityScoreChecklistInfo();
            entity.QualityScoreID = r.GetInt32Value("QualityScoreID");
            entity.QualityScoreNumber = r.GetStringValue("QualityScoreNumber");
            return entity;
        }

		/// <summary>
		/// Creates the sql insert command, using the values from the passed
		/// in QualityScoreStatusMstInfo object
		/// </summary>
		/// <param name="o">A QualityScoreStatusMstInfo object, from which the insert values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(RangeOfScoreMstInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_QualityScoreStatusMst");			
			return cmd;
        }

		/// <summary>
		/// Creates the sql update command, using the values from the passed
		/// in QualityScoreStatusMstInfo object
		/// </summary>
		/// <param name="o">A QualityScoreStatusMstInfo object, from which the update values are pulled</param>
		/// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(RangeOfScoreMstInfo entity) 
        {
			System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_QualityScoreStatusMst");
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
            return cmd;
        }

    }
}
