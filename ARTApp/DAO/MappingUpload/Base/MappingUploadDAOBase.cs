using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SkyStem.ART.Client.Model.MappingUpload;
using Adapdev.Data;
using Adapdev.Data.Sql;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.DAO.Base;



namespace SkyStem.ART.App.DAO.MappingUpload.Base
{
    public class MappingUploadDAOBase : CustomAbstractDAO<MappingUploadInfo> 
    {
        /// <summary>
        ///  CurrentAppUserInfo  for further use
        /// </summary>
        public AppUserInfo CurrentAppUserInfo { get; set; }

        public MappingUploadDAOBase(AppUserInfo oAppUserInfo)
            : base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "MappingUpload", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }
        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in CompanyQualityScoreInfo object
        /// </summary>
        /// <param name="o">A CompanyQualityScoreInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(MappingUploadInfo entity)
        {
            //System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_CompanyQualityScore");
            //cmd.CommandType = CommandType.StoredProcedure;
            //IDataParameterCollection cmdParams = cmd.Parameters;
            //System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            //parAddedBy.ParameterName = "@AddedBy";
            //if (entity != null)
            //    parAddedBy.Value = entity.AddedBy;
            //else
            //    parAddedBy.Value = System.DBNull.Value;
            //cmdParams.Add(parAddedBy);
            //System.Data.IDbDataParameter parAddedByUserID = cmd.CreateParameter();
            //parAddedByUserID.ParameterName = "@AddedByUserID";
            //if (entity != null)
            //    parAddedByUserID.Value = entity.AddedByUserID;
            //else
            //    parAddedByUserID.Value = System.DBNull.Value;
            //cmdParams.Add(parAddedByUserID);
            //System.Data.IDbDataParameter parCompanyRecPeriodSetID = cmd.CreateParameter();
            //parCompanyRecPeriodSetID.ParameterName = "@CompanyRecPeriodSetID";
            //if (entity != null)
            //    parCompanyRecPeriodSetID.Value = entity.CompanyRecPeriodSetID;
            //else
            //    parCompanyRecPeriodSetID.Value = System.DBNull.Value;
            //cmdParams.Add(parCompanyRecPeriodSetID);
            //System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            //parDateAdded.ParameterName = "@DateAdded";
            //if (entity != null)
            //    parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            //else
            //    parDateAdded.Value = System.DBNull.Value;
            //cmdParams.Add(parDateAdded);
            //System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            //parDateRevised.ParameterName = "@DateRevised";
            //if (entity != null)
            //    parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            //else
            //    parDateRevised.Value = System.DBNull.Value;
            //cmdParams.Add(parDateRevised);
            //System.Data.IDbDataParameter parDescription = cmd.CreateParameter();
            //parDescription.ParameterName = "@Description";
            //if (entity != null)
            //    parDescription.Value = entity.Description;
            //else
            //    parDescription.Value = System.DBNull.Value;
            //cmdParams.Add(parDescription);
            //System.Data.IDbDataParameter parDescriptionLabelID = cmd.CreateParameter();
            //parDescriptionLabelID.ParameterName = "@DescriptionLabelID";
            //if (entity != null)
            //    parDescriptionLabelID.Value = entity.DescriptionLabelID;
            //else
            //    parDescriptionLabelID.Value = System.DBNull.Value;
            //cmdParams.Add(parDescriptionLabelID);
            //System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            //parIsActive.ParameterName = "@IsActive";
            //if (entity != null)
            //    parIsActive.Value = entity.IsActive;
            //else
            //    parIsActive.Value = System.DBNull.Value;
            //cmdParams.Add(parIsActive);
            //System.Data.IDbDataParameter parIsApplicableForSRA = cmd.CreateParameter();
            //parIsApplicableForSRA.ParameterName = "@IsApplicableForSRA";
            //if (entity != null)
            //    parIsApplicableForSRA.Value = entity.IsApplicableForSRA;
            //else
            //    parIsApplicableForSRA.Value = System.DBNull.Value;
            //cmdParams.Add(parIsApplicableForSRA);
            //System.Data.IDbDataParameter parIsEnabled = cmd.CreateParameter();
            //parIsEnabled.ParameterName = "@IsEnabled";
            //if (entity != null)
            //    parIsEnabled.Value = entity.IsEnabled;
            //else
            //    parIsEnabled.Value = System.DBNull.Value;
            //cmdParams.Add(parIsEnabled);
            //System.Data.IDbDataParameter parIsUserScoreEnabled = cmd.CreateParameter();
            //parIsUserScoreEnabled.ParameterName = "@IsUserScoreEnabled";
            //if (entity != null)
            //    parIsUserScoreEnabled.Value = entity.IsUserScoreEnabled;
            //else
            //    parIsUserScoreEnabled.Value = System.DBNull.Value;
            //cmdParams.Add(parIsUserScoreEnabled);
            //System.Data.IDbDataParameter parQualityScoreID = cmd.CreateParameter();
            //parQualityScoreID.ParameterName = "@QualityScoreID";
            //if (entity != null)
            //    parQualityScoreID.Value = entity.QualityScoreID;
            //else
            //    parQualityScoreID.Value = System.DBNull.Value;
            //cmdParams.Add(parQualityScoreID);
            //System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            //parRevisedBy.ParameterName = "@RevisedBy";
            //if (entity != null)
            //    parRevisedBy.Value = entity.RevisedBy;
            //else
            //    parRevisedBy.Value = System.DBNull.Value;
            //cmdParams.Add(parRevisedBy);
            //System.Data.IDbDataParameter parSortOrder = cmd.CreateParameter();
            //parSortOrder.ParameterName = "@SortOrder";
            //if (entity != null)
            //    parSortOrder.Value = entity.SortOrder;
            //else
            //    parSortOrder.Value = System.DBNull.Value;
            //cmdParams.Add(parSortOrder);
            //System.Data.IDbDataParameter parWeightage = cmd.CreateParameter();
            //parWeightage.ParameterName = "@Weightage";
            //if (entity != null)
            //    parWeightage.Value = entity.Weightage;
            //else
            //    parWeightage.Value = System.DBNull.Value;
            //cmdParams.Add(parWeightage);
            return null;
        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in CompanyQualityScoreInfo object
        /// </summary>
        /// <param name="o">A CompanyQualityScoreInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(MappingUploadInfo entity)
        {
            //System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_CompanyQualityScore");
            //cmd.CommandType = CommandType.StoredProcedure;
            //IDataParameterCollection cmdParams = cmd.Parameters;
            //System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            //parAddedBy.ParameterName = "@AddedBy";
            //if (entity != null)
            //    parAddedBy.Value = entity.AddedBy;
            //else
            //    parAddedBy.Value = System.DBNull.Value;
            //cmdParams.Add(parAddedBy);
            //System.Data.IDbDataParameter parAddedByUserID = cmd.CreateParameter();
            //parAddedByUserID.ParameterName = "@AddedByUserID";
            //if (entity != null)
            //    parAddedByUserID.Value = entity.AddedByUserID;
            //else
            //    parAddedByUserID.Value = System.DBNull.Value;
            //cmdParams.Add(parAddedByUserID);
            //System.Data.IDbDataParameter parCompanyRecPeriodSetID = cmd.CreateParameter();
            //parCompanyRecPeriodSetID.ParameterName = "@CompanyRecPeriodSetID";
            //if (entity != null)
            //    parCompanyRecPeriodSetID.Value = entity.CompanyRecPeriodSetID;
            //else
            //    parCompanyRecPeriodSetID.Value = System.DBNull.Value;
            //cmdParams.Add(parCompanyRecPeriodSetID);
            //System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            //parDateAdded.ParameterName = "@DateAdded";
            //if (entity != null)
            //    parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            //else
            //    parDateAdded.Value = System.DBNull.Value;
            //cmdParams.Add(parDateAdded);
            //System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            //parDateRevised.ParameterName = "@DateRevised";
            //if (entity != null)
            //    parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            //else
            //    parDateRevised.Value = System.DBNull.Value;
            //cmdParams.Add(parDateRevised);
            //System.Data.IDbDataParameter parDescription = cmd.CreateParameter();
            //parDescription.ParameterName = "@Description";
            //if (entity != null)
            //    parDescription.Value = entity.Description;
            //else
            //    parDescription.Value = System.DBNull.Value;
            //cmdParams.Add(parDescription);
            //System.Data.IDbDataParameter parDescriptionLabelID = cmd.CreateParameter();
            //parDescriptionLabelID.ParameterName = "@DescriptionLabelID";
            //if (entity != null)
            //    parDescriptionLabelID.Value = entity.DescriptionLabelID;
            //else
            //    parDescriptionLabelID.Value = System.DBNull.Value;
            //cmdParams.Add(parDescriptionLabelID);
            //System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            //parIsActive.ParameterName = "@IsActive";
            //if (entity != null)
            //    parIsActive.Value = entity.IsActive;
            //else
            //    parIsActive.Value = System.DBNull.Value;
            //cmdParams.Add(parIsActive);
            //System.Data.IDbDataParameter parIsApplicableForSRA = cmd.CreateParameter();
            //parIsApplicableForSRA.ParameterName = "@IsApplicableForSRA";
            //if (entity != null)
            //    parIsApplicableForSRA.Value = entity.IsApplicableForSRA;
            //else
            //    parIsApplicableForSRA.Value = System.DBNull.Value;
            //cmdParams.Add(parIsApplicableForSRA);
            //System.Data.IDbDataParameter parIsEnabled = cmd.CreateParameter();
            //parIsEnabled.ParameterName = "@IsEnabled";
            //if (entity != null)
            //    parIsEnabled.Value = entity.IsEnabled;
            //else
            //    parIsEnabled.Value = System.DBNull.Value;
            //cmdParams.Add(parIsEnabled);
            //System.Data.IDbDataParameter parIsUserScoreEnabled = cmd.CreateParameter();
            //parIsUserScoreEnabled.ParameterName = "@IsUserScoreEnabled";
            //if (entity != null)
            //    parIsUserScoreEnabled.Value = entity.IsUserScoreEnabled;
            //else
            //    parIsUserScoreEnabled.Value = System.DBNull.Value;
            //cmdParams.Add(parIsUserScoreEnabled);
            //System.Data.IDbDataParameter parQualityScoreID = cmd.CreateParameter();
            //parQualityScoreID.ParameterName = "@QualityScoreID";
            //if (entity != null)
            //    parQualityScoreID.Value = entity.QualityScoreID;
            //else
            //    parQualityScoreID.Value = System.DBNull.Value;
            //cmdParams.Add(parQualityScoreID);
            //System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            //parRevisedBy.ParameterName = "@RevisedBy";
            //if (entity != null)
            //    parRevisedBy.Value = entity.RevisedBy;
            //else
            //    parRevisedBy.Value = System.DBNull.Value;
            //cmdParams.Add(parRevisedBy);
            //System.Data.IDbDataParameter parSortOrder = cmd.CreateParameter();
            //parSortOrder.ParameterName = "@SortOrder";
            //if (entity != null)
            //    parSortOrder.Value = entity.SortOrder;
            //else
            //    parSortOrder.Value = System.DBNull.Value;
            //cmdParams.Add(parSortOrder);
            //System.Data.IDbDataParameter parWeightage = cmd.CreateParameter();
            //parWeightage.ParameterName = "@Weightage";
            //if (entity != null)
            //    parWeightage.Value = entity.Weightage;
            //else
            //    parWeightage.Value = System.DBNull.Value;
            //cmdParams.Add(parWeightage);
            //System.Data.IDbDataParameter pkparCompanyQualityScoreID = cmd.CreateParameter();
            //pkparCompanyQualityScoreID.ParameterName = "@CompanyQualityScoreID";
            //pkparCompanyQualityScoreID.Value = entity.CompanyQualityScoreID;
            //cmdParams.Add(pkparCompanyQualityScoreID);
            return null;
        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {
            //System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_CompanyQualityScore");
            //cmd.CommandType = CommandType.StoredProcedure;
            //IDataParameterCollection cmdParams = cmd.Parameters;
            //System.Data.IDbDataParameter par = cmd.CreateParameter();
            //par.ParameterName = "@CompanyQualityScoreID";
            //par.Value = id;
            //cmdParams.Add(par);
            return null;
        }


        /// <summary>
        /// Creates the sql select command, using the passed in primary key
        /// </summary>
        /// <param name="o">The primary key of the object to select</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateSelectOneCommand(object id)
        {
            //System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_CompanyQualityScore");
            //cmd.CommandType = CommandType.StoredProcedure;
            //IDataParameterCollection cmdParams = cmd.Parameters;
            //System.Data.IDbDataParameter par = cmd.CreateParameter();
            //par.ParameterName = "@CompanyQualityScoreID";
            //par.Value = id;
            //cmdParams.Add(par);
            return null;
        }

        protected override MappingUploadInfo MapObject(System.Data.IDataReader r)
        {
            MappingUploadInfo entity = new MappingUploadInfo();
            entity.AccountMappingKeyID = r.GetInt16Value("AccountMappingKeyID");
            entity.AccountMappingKeyName = r.GetStringValue("AccountMappingKeyName");
            entity.AccountMappingKeyNameLabelID = r.GetInt16Value("AccountMappingKeyNameLabelID");
            entity.GeographyStructureLabelID = r.GetInt32Value("GeographyStructureLabelID");
            entity.SelectedKeysID = r.GetInt16Value("SelectedKeysID");
            entity.IsEnabled = Convert.ToBoolean(r.GetInt32Value("IsEnabled"));
            entity.GeographyStructure = r.GetStringValue("GeographyStructure");
            return entity;
        }

        //protected override MappingUploadMasterInfo MapAllKeyObject(System.Data.IDataReader r)
        //{
        //    MappingUploadMasterInfo entity = new MappingUploadMasterInfo();
        //    entity.AccountMappingKeyID = r.GetInt32Value("AccountMappingKeyID");
        //    entity.AccountMappingKeyName = r.GetStringValue("AccountMappingKeyName");
        //    entity.AccountMappingKeyNameLabelID = r.GetInt32Value("AccountMappingKeyNameLabelID");
        //    entity.ToBeDisplayed = Convert.ToBoolean(r.GetInt32Value("ToBeDisplayed"));
        //    return entity;
        //}
    }
}
