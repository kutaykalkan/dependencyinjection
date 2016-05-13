

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO.Base
{

    public abstract class CompanySettingDAOBase : CustomAbstractDAO<CompanySettingInfo>
    {

        /// <summary>
        /// A static representation of column AddedBy
        /// </summary>
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
        /// <summary>
        /// A static representation of column AllowCertificationLockdown
        /// </summary>
        public static readonly string COLUMN_ALLOWCERTIFICATIONLOCKDOWN = "AllowCertificationLockdown";
        /// <summary>
        /// A static representation of column AllowCustomReconciliationFrequency
        /// </summary>
        public static readonly string COLUMN_ALLOWCUSTOMRECONCILIATIONFREQUENCY = "AllowCustomReconciliationFrequency";
        /// <summary>
        /// A static representation of column AllowReviewNotesDeletion
        /// </summary>
        public static readonly string COLUMN_ALLOWREVIEWNOTESDELETION = "AllowReviewNotesDeletion";
        /// <summary>
        /// A static representation of column BaseCurrencyCode
        /// </summary>
        public static readonly string COLUMN_BASECURRENCYCODE = "BaseCurrencyCode";
        /// <summary>
        /// A static representation of column CompanyID
        /// </summary>
        public static readonly string COLUMN_COMPANYID = "CompanyID";
        /// <summary>
        /// A static representation of column CompanyMaterialityThreshold
        /// </summary>
        public static readonly string COLUMN_COMPANYMATERIALITYTHRESHOLD = "CompanyMaterialityThreshold";
        /// <summary>
        /// A static representation of column CompanySettingID
        /// </summary>
        public static readonly string COLUMN_COMPANYSETTINGID = "CompanySettingID";
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
        /// A static representation of column MaterialityTypeID
        /// </summary>
        public static readonly string COLUMN_MATERIALITYTYPEID = "MaterialityTypeID";
        /// <summary>
        /// A static representation of column MaximumDocumentUploadSize
        /// </summary>
        public static readonly string COLUMN_MAXIMUMDOCUMENTUPLOADSIZE = "MaximumDocumentUploadSize";
        /// <summary>
        /// A static representation of column CurrentReconciliationPeriodID
        /// </summary>
        public static readonly string COLUMN_CURRENTRECONCILIATIONPERIODID = "CurrentReconciliationPeriodID";
        /// <summary>
        /// A static representation of column ReportingCurrencyCode
        /// </summary>
        public static readonly string COLUMN_REPORTINGCURRENCYCODE = "ReportingCurrencyCode";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// A static representation of column UnexplainedVarianceThreshold
        /// </summary>
        public static readonly string COLUMN_UNEXPLAINEDVARIANCETHRESHOLD = "UnexplainedVarianceThreshold";
        /// <summary>
        /// Provides access to the name of the primary key column (CompanySettingID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "CompanySettingID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "CompanySetting";

        /// <summary>
        /// Provides access to the name of the database
        /// </summary>
        public static readonly string DATABASE_NAME = "SkyStemArt";


        /// <summary>
        ///  CurrentAppUserInfo  for further use
        /// </summary>
        public AppUserInfo CurrentAppUserInfo { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public CompanySettingDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "CompanySetting", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a CompanySettingInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>CompanySettingInfo</returns>
        protected override CompanySettingInfo MapObject(System.Data.IDataReader r)
        {

            CompanySettingInfo entity = new CompanySettingInfo();


            try
            {
                int ordinal = r.GetOrdinal("CompanySettingID");
                if (!r.IsDBNull(ordinal)) entity.CompanySettingID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("CompanyID");
                if (!r.IsDBNull(ordinal)) entity.CompanyID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("CurrentReconciliationPeriodID");
                if (!r.IsDBNull(ordinal)) entity.CurrentReconciliationPeriodID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("BaseCurrencyCode");
                if (!r.IsDBNull(ordinal)) entity.BaseCurrencyCode = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ReportingCurrencyCode");
                if (!r.IsDBNull(ordinal)) entity.ReportingCurrencyCode = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("MaterialityTypeID");
                if (!r.IsDBNull(ordinal)) entity.MaterialityTypeID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("CompanyMaterialityThreshold");
                if (!r.IsDBNull(ordinal)) entity.CompanyMaterialityThreshold = ((System.Decimal)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("AllowCertificationLockdown");
                if (!r.IsDBNull(ordinal)) entity.AllowCertificationLockdown = ((System.Boolean)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("UnexplainedVarianceThreshold");
                if (!r.IsDBNull(ordinal)) entity.UnexplainedVarianceThreshold = ((System.Decimal)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("MaximumDocumentUploadSize");
                if (!r.IsDBNull(ordinal)) entity.MaximumDocumentUploadSize = ((System.Decimal)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("AllowReviewNotesDeletion");
                if (!r.IsDBNull(ordinal)) entity.AllowReviewNotesDeletion = ((System.Boolean)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("AllowCustomReconciliationFrequency");
                if (!r.IsDBNull(ordinal)) entity.AllowCustomReconciliationFrequency = ((System.Boolean)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("DateAdded");
                if (!r.IsDBNull(ordinal)) entity.DateAdded = ((System.DateTime)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("AddedBy");
                if (!r.IsDBNull(ordinal)) entity.AddedBy = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("DateRevised");
                if (!r.IsDBNull(ordinal)) entity.DateRevised = ((System.DateTime)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("RevisedBy");
                if (!r.IsDBNull(ordinal)) entity.RevisedBy = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("HostName");
                if (!r.IsDBNull(ordinal)) entity.HostName = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            //for SelectCompanyMaterialityType() method.
            try
            {
                int ordinal = r.GetOrdinal("IsCarryForwardedFromPreviousRecPeriod");
                if (!r.IsDBNull(ordinal)) entity.IsCarryForwardedFromPreviousRecPeriod = ((System.Boolean)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("DualLevelReviewTypeID");
                if (!r.IsDBNull(ordinal)) entity.DualLevelReviewTypeID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("DayTypeID");
                if (!r.IsDBNull(ordinal)) entity.DayTypeID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("RCCValidationTypeID");
                if (!r.IsDBNull(ordinal)) entity.RCCValidationTypeID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("IsRCCValidationCarryForwardedFromPreviousRecPeriod");
                if (!r.IsDBNull(ordinal)) entity.IsRCCValidationCarryForwardedFromPreviousRecPeriod = ((System.Boolean)(r.GetValue(ordinal)));
            }
            catch (Exception) { }


            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in CompanySettingInfo object
        /// </summary>
        /// <param name="o">A CompanySettingInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(CompanySettingInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_CompanySetting");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parAllowCertificationLockdown = cmd.CreateParameter();
            parAllowCertificationLockdown.ParameterName = "@AllowCertificationLockdown";
            if (!entity.IsAllowCertificationLockdownNull)
                parAllowCertificationLockdown.Value = entity.AllowCertificationLockdown;
            else
                parAllowCertificationLockdown.Value = System.DBNull.Value;
            cmdParams.Add(parAllowCertificationLockdown);

            System.Data.IDbDataParameter parAllowCustomReconciliationFrequency = cmd.CreateParameter();
            parAllowCustomReconciliationFrequency.ParameterName = "@AllowCustomReconciliationFrequency";
            if (!entity.IsAllowCustomReconciliationFrequencyNull)
                parAllowCustomReconciliationFrequency.Value = entity.AllowCustomReconciliationFrequency;
            else
                parAllowCustomReconciliationFrequency.Value = System.DBNull.Value;
            cmdParams.Add(parAllowCustomReconciliationFrequency);

            System.Data.IDbDataParameter parAllowReviewNotesDeletion = cmd.CreateParameter();
            parAllowReviewNotesDeletion.ParameterName = "@AllowReviewNotesDeletion";
            if (!entity.IsAllowReviewNotesDeletionNull)
                parAllowReviewNotesDeletion.Value = entity.AllowReviewNotesDeletion;
            else
                parAllowReviewNotesDeletion.Value = System.DBNull.Value;
            cmdParams.Add(parAllowReviewNotesDeletion);

            System.Data.IDbDataParameter parBaseCurrencyCode = cmd.CreateParameter();
            parBaseCurrencyCode.ParameterName = "@BaseCurrencyCode";
            if (!entity.IsBaseCurrencyCodeNull)
                parBaseCurrencyCode.Value = entity.BaseCurrencyCode;
            else
                parBaseCurrencyCode.Value = System.DBNull.Value;
            cmdParams.Add(parBaseCurrencyCode);

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (!entity.IsCompanyIDNull)
                parCompanyID.Value = entity.CompanyID;
            else
                parCompanyID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parCompanyMaterialityThreshold = cmd.CreateParameter();
            parCompanyMaterialityThreshold.ParameterName = "@CompanyMaterialityThreshold";
            if (!entity.IsCompanyMaterialityThresholdNull)
                parCompanyMaterialityThreshold.Value = entity.CompanyMaterialityThreshold;
            else
                parCompanyMaterialityThreshold.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyMaterialityThreshold);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (!entity.IsDateAddedNull)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (!entity.IsDateRevisedNull)
                parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            parHostName.ParameterName = "@HostName";
            if (!entity.IsHostNameNull)
                parHostName.Value = entity.HostName;
            else
                parHostName.Value = System.DBNull.Value;
            cmdParams.Add(parHostName);

            System.Data.IDbDataParameter parMaterialityTypeID = cmd.CreateParameter();
            parMaterialityTypeID.ParameterName = "@MaterialityTypeID";
            if (!entity.IsMaterialityTypeIDNull)
                parMaterialityTypeID.Value = entity.MaterialityTypeID;
            else
                parMaterialityTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parMaterialityTypeID);

            System.Data.IDbDataParameter parMaximumDocumentUploadSize = cmd.CreateParameter();
            parMaximumDocumentUploadSize.ParameterName = "@MaximumDocumentUploadSize";
            if (!entity.IsMaximumDocumentUploadSizeNull)
                parMaximumDocumentUploadSize.Value = entity.MaximumDocumentUploadSize;
            else
                parMaximumDocumentUploadSize.Value = System.DBNull.Value;
            cmdParams.Add(parMaximumDocumentUploadSize);

            System.Data.IDbDataParameter parCurrentReconciliationPeriodID = cmd.CreateParameter();
            parCurrentReconciliationPeriodID.ParameterName = "@CurrentReconciliationPeriodID";
            if (!entity.IsCurrentReconciliationPeriodIDNull)
                parCurrentReconciliationPeriodID.Value = entity.CurrentReconciliationPeriodID;
            else
                parCurrentReconciliationPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parCurrentReconciliationPeriodID);

            System.Data.IDbDataParameter parReportingCurrencyCode = cmd.CreateParameter();
            parReportingCurrencyCode.ParameterName = "@ReportingCurrencyCode";
            if (!entity.IsReportingCurrencyCodeNull)
                parReportingCurrencyCode.Value = entity.ReportingCurrencyCode;
            else
                parReportingCurrencyCode.Value = System.DBNull.Value;
            cmdParams.Add(parReportingCurrencyCode);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parUnexplainedVarianceThreshold = cmd.CreateParameter();
            parUnexplainedVarianceThreshold.ParameterName = "@UnexplainedVarianceThreshold";
            if (!entity.IsUnexplainedVarianceThresholdNull)
                parUnexplainedVarianceThreshold.Value = entity.UnexplainedVarianceThreshold;
            else
                parUnexplainedVarianceThreshold.Value = System.DBNull.Value;
            cmdParams.Add(parUnexplainedVarianceThreshold);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in CompanySettingInfo object
        /// </summary>
        /// <param name="o">A CompanySettingInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(CompanySettingInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_CompanySetting");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parAllowCertificationLockdown = cmd.CreateParameter();
            parAllowCertificationLockdown.ParameterName = "@AllowCertificationLockdown";
            if (!entity.IsAllowCertificationLockdownNull)
                parAllowCertificationLockdown.Value = entity.AllowCertificationLockdown;
            else
                parAllowCertificationLockdown.Value = System.DBNull.Value;
            cmdParams.Add(parAllowCertificationLockdown);

            System.Data.IDbDataParameter parAllowCustomReconciliationFrequency = cmd.CreateParameter();
            parAllowCustomReconciliationFrequency.ParameterName = "@AllowCustomReconciliationFrequency";
            if (!entity.IsAllowCustomReconciliationFrequencyNull)
                parAllowCustomReconciliationFrequency.Value = entity.AllowCustomReconciliationFrequency;
            else
                parAllowCustomReconciliationFrequency.Value = System.DBNull.Value;
            cmdParams.Add(parAllowCustomReconciliationFrequency);

            System.Data.IDbDataParameter parAllowReviewNotesDeletion = cmd.CreateParameter();
            parAllowReviewNotesDeletion.ParameterName = "@AllowReviewNotesDeletion";
            if (!entity.IsAllowReviewNotesDeletionNull)
                parAllowReviewNotesDeletion.Value = entity.AllowReviewNotesDeletion;
            else
                parAllowReviewNotesDeletion.Value = System.DBNull.Value;
            cmdParams.Add(parAllowReviewNotesDeletion);

            System.Data.IDbDataParameter parBaseCurrencyCode = cmd.CreateParameter();
            parBaseCurrencyCode.ParameterName = "@BaseCurrencyCode";
            if (!entity.IsBaseCurrencyCodeNull)
                parBaseCurrencyCode.Value = entity.BaseCurrencyCode;
            else
                parBaseCurrencyCode.Value = System.DBNull.Value;
            cmdParams.Add(parBaseCurrencyCode);

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (!entity.IsCompanyIDNull)
                parCompanyID.Value = entity.CompanyID;
            else
                parCompanyID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parCompanyMaterialityThreshold = cmd.CreateParameter();
            parCompanyMaterialityThreshold.ParameterName = "@CompanyMaterialityThreshold";
            if (!entity.IsCompanyMaterialityThresholdNull)
                parCompanyMaterialityThreshold.Value = entity.CompanyMaterialityThreshold;
            else
                parCompanyMaterialityThreshold.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyMaterialityThreshold);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (!entity.IsDateAddedNull)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (!entity.IsDateRevisedNull)
                parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            parHostName.ParameterName = "@HostName";
            if (!entity.IsHostNameNull)
                parHostName.Value = entity.HostName;
            else
                parHostName.Value = System.DBNull.Value;
            cmdParams.Add(parHostName);

            System.Data.IDbDataParameter parMaterialityTypeID = cmd.CreateParameter();
            parMaterialityTypeID.ParameterName = "@MaterialityTypeID";
            if (!entity.IsMaterialityTypeIDNull)
                parMaterialityTypeID.Value = entity.MaterialityTypeID;
            else
                parMaterialityTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parMaterialityTypeID);

            System.Data.IDbDataParameter parMaximumDocumentUploadSize = cmd.CreateParameter();
            parMaximumDocumentUploadSize.ParameterName = "@MaximumDocumentUploadSize";
            if (!entity.IsMaximumDocumentUploadSizeNull)
                parMaximumDocumentUploadSize.Value = entity.MaximumDocumentUploadSize;
            else
                parMaximumDocumentUploadSize.Value = System.DBNull.Value;
            cmdParams.Add(parMaximumDocumentUploadSize);

            System.Data.IDbDataParameter parCurrentReconciliationPeriodID = cmd.CreateParameter();
            parCurrentReconciliationPeriodID.ParameterName = "@CurrentReconciliationPeriodID";
            if (!entity.IsCurrentReconciliationPeriodIDNull)
                parCurrentReconciliationPeriodID.Value = entity.CurrentReconciliationPeriodID;
            else
                parCurrentReconciliationPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parCurrentReconciliationPeriodID);

            System.Data.IDbDataParameter parReportingCurrencyCode = cmd.CreateParameter();
            parReportingCurrencyCode.ParameterName = "@ReportingCurrencyCode";
            if (!entity.IsReportingCurrencyCodeNull)
                parReportingCurrencyCode.Value = entity.ReportingCurrencyCode;
            else
                parReportingCurrencyCode.Value = System.DBNull.Value;
            cmdParams.Add(parReportingCurrencyCode);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parUnexplainedVarianceThreshold = cmd.CreateParameter();
            parUnexplainedVarianceThreshold.ParameterName = "@UnexplainedVarianceThreshold";
            if (!entity.IsUnexplainedVarianceThresholdNull)
                parUnexplainedVarianceThreshold.Value = entity.UnexplainedVarianceThreshold;
            else
                parUnexplainedVarianceThreshold.Value = System.DBNull.Value;
            cmdParams.Add(parUnexplainedVarianceThreshold);

            System.Data.IDbDataParameter pkparCompanySettingID = cmd.CreateParameter();
            pkparCompanySettingID.ParameterName = "@CompanySettingID";
            pkparCompanySettingID.Value = entity.CompanySettingID;
            cmdParams.Add(pkparCompanySettingID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_CompanySetting");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanySettingID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_CompanySetting");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanySettingID";
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
        public IList<CompanySettingInfo> SelectAllByCompanyID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CompanySettingByCompanyID");
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
        public IList<CompanySettingInfo> SelectAllByCurrentReconciliationPeriodID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CompanySettingByCurrentReconciliationPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CurrentReconciliationPeriodID";
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
        public IList<CompanySettingInfo> SelectAllByMaterialityTypeID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CompanySettingByMaterialityTypeID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MaterialityTypeID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(CompanySettingInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(CompanySettingDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(CompanySettingInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(CompanySettingDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(CompanySettingInfo entity, object id)
        {
            entity.CompanySettingID = Convert.ToInt32(id);
        }




    }
}
