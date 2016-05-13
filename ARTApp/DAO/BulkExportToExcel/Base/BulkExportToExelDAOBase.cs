using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SkyStem.ART.Client.Model.BulkExportExcel;
using Adapdev.Data;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO.BulkExportToExcel.Base
{
    public class BulkExportToExelDAOBase: CustomAbstractDAO<BulkExportToExcelInfo> 
    {
        /// <summary>
        ///  CurrentAppUserInfo  for further use
        /// </summary>
        public AppUserInfo CurrentAppUserInfo { get; set; }

        public BulkExportToExelDAOBase(AppUserInfo oAppUserInfo)
            : base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "BulkExportToExcel", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }
        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in CompanyQualityScoreInfo object
        /// </summary>
        /// <param name="o">A CompanyQualityScoreInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(BulkExportToExcelInfo entity)
        {
             return null;
        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in CompanyQualityScoreInfo object
        /// </summary>
        /// <param name="o">A CompanyQualityScoreInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(BulkExportToExcelInfo entity)
        {
             return null;
        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {
            return null;
        }


        /// <summary>
        /// Creates the sql select command, using the passed in primary key
        /// </summary>
        /// <param name="o">The primary key of the object to select</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateSelectOneCommand(object id)
        {
            return null;
        }

        protected override BulkExportToExcelInfo MapObject(System.Data.IDataReader r)
        {
            BulkExportToExcelInfo entity = new BulkExportToExcelInfo();
            entity.RequestID = r.GetInt32Value("RequestID");
            entity.UserID = r.GetInt32Value("UserID");
            entity.RoleID = r.GetInt16Value("RoleID");
            entity.RecperiodID = r.GetInt32Value("RecperiodID");
            entity.GridID = r.GetInt32Value("GridID");
            entity.GridNameLabelID = r.GetInt32Value("GridNameLabelID");            
            entity.StatusID = r.GetInt16Value("StatusID");
            entity.RequestTypeID = r.GetInt16Value("RequestTypeID");
            entity.RequestTypeLabelID = r.GetInt32Value("RequestTypeLabelID");
            entity.DateAdded = r.GetDateValue("DateAdded");
            entity.AddedBy = r.GetStringValue("AddedBy");
            entity.AddedByUserName = r.GetStringValue("AddedByUserName");
            entity.FileName = r.GetStringValue("FileName");
            entity.PhysicalPath = r.GetStringValue("PhysicalFilePath");
            entity.RequestStatusLabelID = r.GetInt32Value("RequestStatusLabelID");
            entity.FinancialYear = r.GetStringValue("FinancialYear");
            entity.PeriodEndDate = r.GetDateValue("PeriodEndDate").Value;
            entity.RequestErrorLabelID = r.GetInt32Value("RequestErrorLabelID");
            
            return entity;
        }
    }
}
