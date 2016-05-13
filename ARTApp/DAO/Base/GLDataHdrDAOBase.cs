

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

    public abstract class GLDataHdrDAOBase : CustomAbstractDAO<GLDataHdrInfo>
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
        /// A static representation of column ApproverSignOffDate
        /// </summary>
        public static readonly string COLUMN_APPROVERSIGNOFFDATE = "ApproverSignOffDate";
        /// <summary>
        /// A static representation of column SupportingDetailBalanceBaseCurrency
        /// </summary>
        public static readonly string COLUMN_SUPPORTINGDETAILBALANCEBASECURRENCY = "SupportingDetailBalanceBaseCurrency";
        /// <summary>
        /// A static representation of column SupportingDetailBalanceReportingCurrency
        /// </summary>
        public static readonly string COLUMN_SUPPORTINGDETAILBALANCEREPORTINGCURRENCY = "SupportingDetailBalanceReportingCurrency";
        /// <summary>
        /// A static representation of column BaseCurrencyCode
        /// </summary>
        public static readonly string COLUMN_BASECURRENCYCODE = "BaseCurrencyCode";
        /// <summary>
        /// A static representation of column CertificationStatusID
        /// </summary>
        public static readonly string COLUMN_CERTIFICATIONSTATUSID = "CertificationStatusID";
        /// <summary>
        /// A static representation of column DataImportID
        /// </summary>
        public static readonly string COLUMN_DATAIMPORTID = "DataImportID";
        /// <summary>
        /// A static representation of column DateAdded
        /// </summary>
        public static readonly string COLUMN_DATEADDED = "DateAdded";
        /// <summary>
        /// A static representation of column DateRevised
        /// </summary>
        public static readonly string COLUMN_DATEREVISED = "DateRevised";
        /// <summary>
        /// A static representation of column GLBalanceBaseCurrency
        /// </summary>
        public static readonly string COLUMN_GLBALANCEBASECURRENCY = "GLBalanceBaseCurrency";
        /// <summary>
        /// A static representation of column GLBalanceReportingCurrency
        /// </summary>
        public static readonly string COLUMN_GLBALANCEREPORTINGCURRENCY = "GLBalanceReportingCurrency";
        /// <summary>
        /// A static representation of column GLDataID
        /// </summary>
        public static readonly string COLUMN_GLDATAID = "GLDataID";
        /// <summary>
        /// A static representation of column HostName
        /// </summary>
        public static readonly string COLUMN_HOSTNAME = "HostName";
        /// <summary>
        /// A static representation of column IsActive
        /// </summary>
        public static readonly string COLUMN_ISACTIVE = "IsActive";
        /// <summary>
        /// A static representation of column IsAttachmentAvailable
        /// </summary>
        public static readonly string COLUMN_ISATTACHMENTAVAILABLE = "IsAttachmentAvailable";
        /// <summary>
        /// A static representation of column IsMaterial
        /// </summary>
        public static readonly string COLUMN_ISMATERIAL = "IsMaterial";
        /// <summary>
        /// A static representation of column IsSystemReconcilied
        /// </summary>
        public static readonly string COLUMN_ISSYSTEMRECONCILIED = "IsSystemReconcilied";
        /// <summary>
        /// A static representation of column PreparerSignOffDate
        /// </summary>
        public static readonly string COLUMN_PREPARERSIGNOFFDATE = "PreparerSignOffDate";
        /// <summary>
        /// A static representation of column ReconciliationBalance
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONBALANCEREPORTINGCURRENCY = "ReconciliationBalanceReportingCurrency";

        /// <summary>
        /// A static representation of column ReconciliationBalance
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONBALANCEBASECURRENCY = "ReconciliationBalanceBaseCurrency";
        /// <summary>
        /// A static representation of column ReconciliationPeriodID
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONPERIODID = "ReconciliationPeriodID";
        /// <summary>
        /// A static representation of column ReconciliationStatusDate
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONSTATUSDATE = "ReconciliationStatusDate";
        /// <summary>
        /// A static representation of column ReconciliationStatusID
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONSTATUSID = "ReconciliationStatusID";
        /// <summary>
        /// A static representation of column ReviewerSignOffDate
        /// </summary>
        public static readonly string COLUMN_REVIEWERSIGNOFFDATE = "ReviewerSignOffDate";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// A static representation of column UnexplainedVariance
        /// </summary>
        public static readonly string COLUMN_UNEXPLAINEDVARIANCEREPORTINGCURRENCY = "UnexplainedVarianceReportingCurrency";
        /// <summary>
        /// A static representation of column UnexplainedVariance
        /// </summary>
        public static readonly string COLUMN_UNEXPLAINEDVARIANCEBASECURRENCY = "UnexplainedVarianceBaseCurrency";
        /// <summary>
        /// A static representation of column WriteOffAmount
        /// </summary>
        public static readonly string COLUMN_WRITEOFFAmount = "WriteOffAmount";
        /// <summary>
        /// A static representation of column WriteOnAmount
        /// </summary>
        public static readonly string COLUMN_WRITEONAmount = "WriteOnAmount";
        /// <summary>
        /// Provides access to the name of the primary key column (GLDataID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "GLDataID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "GLDataHdr";

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
        public GLDataHdrDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "GLDataHdr", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a GLDataHdrInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>GLDataHdrInfo</returns>
        protected override GLDataHdrInfo MapObject(System.Data.IDataReader r)
        {

            GLDataHdrInfo entity = new GLDataHdrInfo();


            try
            {
                int ordinal = r.GetOrdinal("GLDataID");
                if (!r.IsDBNull(ordinal)) entity.GLDataID = ((System.Int64)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("AccountID");
                if (!r.IsDBNull(ordinal)) entity.AccountID = ((System.Int64)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("GLBalanceBaseCurrency");
                if (!r.IsDBNull(ordinal)) entity.GLBalanceBaseCurrency = ((System.Decimal)(r.GetValue(ordinal)));
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
                int ordinal = r.GetOrdinal("GLBalanceReportingCurrency");
                if (!r.IsDBNull(ordinal)) entity.GLBalanceReportingCurrency = ((System.Decimal)(r.GetValue(ordinal)));
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
                int ordinal = r.GetOrdinal("ReconciliationStatusLabelID");
                if (!r.IsDBNull(ordinal)) entity.ReconciliationStatusLabelID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ReconciliationStatusID");
                if (!r.IsDBNull(ordinal)) entity.ReconciliationStatusID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("CertificationStatusID");
                if (!r.IsDBNull(ordinal)) entity.CertificationStatusID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("DataImportID");
                if (!r.IsDBNull(ordinal)) entity.DataImportID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ReconciliationBalanceReportingCurrency");
                if (!r.IsDBNull(ordinal)) entity.ReconciliationBalanceReportingCurrency = ((System.Decimal)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ReconciliationBalanceBaseCurrency");
                if (!r.IsDBNull(ordinal)) entity.ReconciliationBalanceBaseCurrency = ((System.Decimal)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("UnexplainedVarianceReportingCurrency");
                if (!r.IsDBNull(ordinal)) entity.UnexplainedVarianceReportingCurrency = ((System.Decimal)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("UnexplainedVarianceBaseCurrency");
                if (!r.IsDBNull(ordinal)) entity.UnexplainedVarianceBaseCurrency = ((System.Decimal)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("WriteOnOffAmountBaseCurrency");
                if (!r.IsDBNull(ordinal)) entity.WriteOnOffAmountBaseCurrency = ((System.Decimal)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("WriteOnOffAmountReportingCurrency");
                if (!r.IsDBNull(ordinal)) entity.WriteOnOffAmountReportingCurrency = ((System.Decimal)(r.GetValue(ordinal)));
            }
            catch (Exception) { }


            try
            {
                int ordinal = r.GetOrdinal("ReconciliationPeriodID");
                if (!r.IsDBNull(ordinal)) entity.ReconciliationPeriodID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ReconciliationStatusDate");
                if (!r.IsDBNull(ordinal)) entity.ReconciliationStatusDate = ((System.DateTime)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            //try{
            //    int ordinal = r.GetOrdinal("PreparerSignOffDate");
            //    if (!r.IsDBNull(ordinal)) entity.PendingReviewStatusDate = ((System.DateTime)(r.GetValue(ordinal)));
            //}
            //catch(Exception){}

            //try{
            //    int ordinal = r.GetOrdinal("ReviewerSignOffDate");
            //    if (!r.IsDBNull(ordinal)) entity.PendingApprovalStatusDate = ((System.DateTime)(r.GetValue(ordinal)));
            //}
            //catch(Exception){}

            //try{
            //    int ordinal = r.GetOrdinal("ApproverSignOffDate");
            //    if (!r.IsDBNull(ordinal)) entity.ReconciledStatusDate = ((System.DateTime)(r.GetValue(ordinal)));
            //}
            //catch(Exception){}

            try
            {
                int ordinal = r.GetOrdinal("SupportingDetailBalanceBaseCurrency");
                if (!r.IsDBNull(ordinal)) entity.SupportingDetailBalanceBaseCurrency = ((System.Decimal)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("SupportingDetailBalanceReportingCurrency");
                if (!r.IsDBNull(ordinal)) entity.SupportingDetailBalanceReportingCurrency = ((System.Decimal)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("IsAttachmentAvailable");
                if (!r.IsDBNull(ordinal)) entity.IsAttachmentAvailable = ((System.Boolean)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("IsMaterial");
                if (!r.IsDBNull(ordinal)) entity.IsMaterial = ((System.Boolean)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("IsSystemReconcilied");
                if (!r.IsDBNull(ordinal)) entity.IsSystemReconcilied = ((System.Boolean)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("IsActive");
                if (!r.IsDBNull(ordinal)) entity.IsActive = ((System.Boolean)(r.GetValue(ordinal)));
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

            try
            {
                int ordinal = r.GetOrdinal("IsVersionAvailable");
                if (!r.IsDBNull(ordinal)) entity.IsVersionAvailable = ((System.Boolean)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("IsSubledgerVersionAvailable");
                if (!r.IsDBNull(ordinal)) entity.IsSubledgerVersionAvailable = ((System.Boolean)(r.GetValue(ordinal)));
            }
            catch (Exception) { }
            try
            {
                int ordinal = r.GetOrdinal("ReconciliationTemplateID");
                if (!r.IsDBNull(ordinal)) entity.ReconciliationTemplateID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }
            try
            {
                int ordinal = r.GetOrdinal("NetAccountID");
                if (!r.IsDBNull(ordinal)) entity.NetAccountID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }
            try
            {
                int ordinal = r.GetOrdinal("IsEditable");
                if (!r.IsDBNull(ordinal)) entity.IsEditable = ((System.Boolean)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in GLDataHdrInfo object
        /// </summary>
        /// <param name="o">A GLDataHdrInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(GLDataHdrInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_GLDataHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAccountID = cmd.CreateParameter();
            parAccountID.ParameterName = "@AccountID";
            if (!entity.IsAccountIDNull)
                parAccountID.Value = entity.AccountID;
            else
                parAccountID.Value = System.DBNull.Value;
            cmdParams.Add(parAccountID);

            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            //System.Data.IDbDataParameter parApproverSignOffDate = cmd.CreateParameter();
            //parApproverSignOffDate.ParameterName = "@ApproverSignOffDate";
            //if(!entity.IsApproverSignOffDateNull)
            //    parApproverSignOffDate.Value = entity.ReconciledStatusDate.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
            //else
            //    parApproverSignOffDate.Value = System.DBNull.Value;
            //cmdParams.Add(parApproverSignOffDate);

            System.Data.IDbDataParameter parSupportingDetailBalanceBaseCurrency = cmd.CreateParameter();
            parSupportingDetailBalanceBaseCurrency.ParameterName = "@SupportingDetailBalanceBaseCurrency";
            if (!entity.IsSupportingDetailBalanceBaseCurrencyNull)
                parSupportingDetailBalanceBaseCurrency.Value = entity.SupportingDetailBalanceBaseCurrency;
            else
                parSupportingDetailBalanceBaseCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parSupportingDetailBalanceBaseCurrency);

            System.Data.IDbDataParameter parSupportingDetailBalanceReportingCurrency = cmd.CreateParameter();
            parSupportingDetailBalanceReportingCurrency.ParameterName = "@SupportingDetailBalanceReportingCurrency";
            if (!entity.IsSupportingDetailBalanceReportingCurrencyNull)
                parSupportingDetailBalanceReportingCurrency.Value = entity.SupportingDetailBalanceReportingCurrency;
            else
                parSupportingDetailBalanceReportingCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parSupportingDetailBalanceReportingCurrency);

            System.Data.IDbDataParameter parBaseCurrencyCode = cmd.CreateParameter();
            parBaseCurrencyCode.ParameterName = "@BaseCurrencyCode";
            if (!entity.IsBaseCurrencyCodeNull)
                parBaseCurrencyCode.Value = entity.BaseCurrencyCode;
            else
                parBaseCurrencyCode.Value = System.DBNull.Value;
            cmdParams.Add(parBaseCurrencyCode);

            System.Data.IDbDataParameter parCertificationStatusID = cmd.CreateParameter();
            parCertificationStatusID.ParameterName = "@CertificationStatusID";
            if (!entity.IsCertificationStatusIDNull)
                parCertificationStatusID.Value = entity.CertificationStatusID;
            else
                parCertificationStatusID.Value = System.DBNull.Value;
            cmdParams.Add(parCertificationStatusID);

            System.Data.IDbDataParameter parDataImportID = cmd.CreateParameter();
            parDataImportID.ParameterName = "@DataImportID";
            if (!entity.IsDataImportIDNull)
                parDataImportID.Value = entity.DataImportID;
            else
                parDataImportID.Value = System.DBNull.Value;
            cmdParams.Add(parDataImportID);

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

            System.Data.IDbDataParameter parGLBalanceBaseCurrency = cmd.CreateParameter();
            parGLBalanceBaseCurrency.ParameterName = "@GLBalanceBaseCurrency";
            if (!entity.IsGLBalanceBaseCurrencyNull)
                parGLBalanceBaseCurrency.Value = entity.GLBalanceBaseCurrency;
            else
                parGLBalanceBaseCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parGLBalanceBaseCurrency);

            System.Data.IDbDataParameter parGLBalanceReportingCurrency = cmd.CreateParameter();
            parGLBalanceReportingCurrency.ParameterName = "@GLBalanceReportingCurrency";
            if (!entity.IsGLBalanceReportingCurrencyNull)
                parGLBalanceReportingCurrency.Value = entity.GLBalanceReportingCurrency;
            else
                parGLBalanceReportingCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parGLBalanceReportingCurrency);

            System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            parHostName.ParameterName = "@HostName";
            if (!entity.IsHostNameNull)
                parHostName.Value = entity.HostName;
            else
                parHostName.Value = System.DBNull.Value;
            cmdParams.Add(parHostName);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (!entity.IsIsActiveNull)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parIsAttachmentAvailable = cmd.CreateParameter();
            parIsAttachmentAvailable.ParameterName = "@IsAttachmentAvailable";
            if (!entity.IsIsAttachmentAvailableNull)
                parIsAttachmentAvailable.Value = entity.IsAttachmentAvailable;
            else
                parIsAttachmentAvailable.Value = System.DBNull.Value;
            cmdParams.Add(parIsAttachmentAvailable);

            System.Data.IDbDataParameter parIsMaterial = cmd.CreateParameter();
            parIsMaterial.ParameterName = "@IsMaterial";
            if (!entity.IsIsMaterialNull)
                parIsMaterial.Value = entity.IsMaterial;
            else
                parIsMaterial.Value = System.DBNull.Value;
            cmdParams.Add(parIsMaterial);

            System.Data.IDbDataParameter parIsSystemReconcilied = cmd.CreateParameter();
            parIsSystemReconcilied.ParameterName = "@IsSystemReconcilied";
            if (!entity.IsIsSystemReconciliedNull)
                parIsSystemReconcilied.Value = entity.IsSystemReconcilied;
            else
                parIsSystemReconcilied.Value = System.DBNull.Value;
            cmdParams.Add(parIsSystemReconcilied);

            //System.Data.IDbDataParameter parPreparerSignOffDate = cmd.CreateParameter();
            //parPreparerSignOffDate.ParameterName = "@PreparerSignOffDate";
            //if(!entity.IsPreparerSignOffDateNull)
            //    parPreparerSignOffDate.Value = entity.PendingReviewStatusDate.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
            //else
            //    parPreparerSignOffDate.Value = System.DBNull.Value;
            //cmdParams.Add(parPreparerSignOffDate);

            System.Data.IDbDataParameter parReconciliationBalanceReportingCurrency = cmd.CreateParameter();
            parReconciliationBalanceReportingCurrency.ParameterName = "@ReconciliationBalanceReportingCurrency";
            if (!entity.IsReconciliationBalanceReportingCurrencyNull)
                parReconciliationBalanceReportingCurrency.Value = entity.ReconciliationBalanceReportingCurrency;
            else
                parReconciliationBalanceReportingCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationBalanceReportingCurrency);

            System.Data.IDbDataParameter parReconciliationBalanceBaseCurrency = cmd.CreateParameter();
            parReconciliationBalanceBaseCurrency.ParameterName = "@ReconciliationBalanceBaseCurrency";
            if (!entity.IsReconciliationBalanceBaseCurrencyNull)
                parReconciliationBalanceBaseCurrency.Value = entity.ReconciliationBalanceBaseCurrency;
            else
                parReconciliationBalanceBaseCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationBalanceBaseCurrency);

            System.Data.IDbDataParameter parReconciliationPeriodID = cmd.CreateParameter();
            parReconciliationPeriodID.ParameterName = "@ReconciliationPeriodID";
            if (!entity.IsReconciliationPeriodIDNull)
                parReconciliationPeriodID.Value = entity.ReconciliationPeriodID;
            else
                parReconciliationPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationPeriodID);

            System.Data.IDbDataParameter parReconciliationStatusDate = cmd.CreateParameter();
            parReconciliationStatusDate.ParameterName = "@ReconciliationStatusDate";
            if (!entity.IsReconciliationStatusDateNull)
                parReconciliationStatusDate.Value = entity.ReconciliationStatusDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parReconciliationStatusDate.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationStatusDate);

            System.Data.IDbDataParameter parReconciliationStatusID = cmd.CreateParameter();
            parReconciliationStatusID.ParameterName = "@ReconciliationStatusID";
            if (!entity.IsReconciliationStatusIDNull)
                parReconciliationStatusID.Value = entity.ReconciliationStatusID;
            else
                parReconciliationStatusID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationStatusID);

            //System.Data.IDbDataParameter parReviewerSignOffDate = cmd.CreateParameter();
            //parReviewerSignOffDate.ParameterName = "@ReviewerSignOffDate";
            //if(!entity.IsReviewerSignOffDateNull)
            //    parReviewerSignOffDate.Value = entity.PendingApprovalStatusDate.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
            //else
            //    parReviewerSignOffDate.Value = System.DBNull.Value;
            //cmdParams.Add(parReviewerSignOffDate);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parUnexplainedVarianceReportingCurrency = cmd.CreateParameter();
            parUnexplainedVarianceReportingCurrency.ParameterName = "@UnexplainedVarianceReportingCurrency";
            if (!entity.IsUnexplainedVarianceReportingCurrencyNull)
                parUnexplainedVarianceReportingCurrency.Value = entity.UnexplainedVarianceReportingCurrency;
            else
                parUnexplainedVarianceReportingCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parUnexplainedVarianceReportingCurrency);

            System.Data.IDbDataParameter parUnexplainedVarianceBaseCurrency = cmd.CreateParameter();
            parUnexplainedVarianceBaseCurrency.ParameterName = "@UnexplainedVarianceBaseCurrency";
            if (!entity.IsUnexplainedVarianceBaseCurrencyNull)
                parUnexplainedVarianceBaseCurrency.Value = entity.UnexplainedVarianceBaseCurrency;
            else
                parUnexplainedVarianceBaseCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parUnexplainedVarianceBaseCurrency);

            System.Data.IDbDataParameter parWriteOffAmountBaseCurrency = cmd.CreateParameter();
            parWriteOffAmountBaseCurrency.ParameterName = "@WriteOnOffAmountBaseCurrency";
            if (!entity.IsWriteOnOffAmountBaseCurrencyNull)
                parWriteOffAmountBaseCurrency.Value = entity.WriteOnOffAmountBaseCurrency;
            else
                parWriteOffAmountBaseCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parWriteOffAmountBaseCurrency);

            System.Data.IDbDataParameter parWriteOffAmountReportingCurrency = cmd.CreateParameter();
            parWriteOffAmountReportingCurrency.ParameterName = "@WriteOnOffAmountReportingCurrency";
            if (!entity.IsWriteOnOffAmountReportingCurrencyNull)
                parWriteOffAmountReportingCurrency.Value = entity.WriteOnOffAmountReportingCurrency;
            else
                parWriteOffAmountReportingCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parWriteOffAmountReportingCurrency);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in GLDataHdrInfo object
        /// </summary>
        /// <param name="o">A GLDataHdrInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(GLDataHdrInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_GLDataHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAccountID = cmd.CreateParameter();
            parAccountID.ParameterName = "@AccountID";
            if (!entity.IsAccountIDNull)
                parAccountID.Value = entity.AccountID;
            else
                parAccountID.Value = System.DBNull.Value;
            cmdParams.Add(parAccountID);

            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            //System.Data.IDbDataParameter parApproverSignOffDate = cmd.CreateParameter();
            //parApproverSignOffDate.ParameterName = "@ApproverSignOffDate";
            //if(!entity.IsApproverSignOffDateNull)
            //    parApproverSignOffDate.Value = entity.ReconciledStatusDate.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
            //else
            //    parApproverSignOffDate.Value = System.DBNull.Value;
            //cmdParams.Add(parApproverSignOffDate);

            System.Data.IDbDataParameter parSupportingDetailBalanceBaseCurrency = cmd.CreateParameter();
            parSupportingDetailBalanceBaseCurrency.ParameterName = "@SupportingDetailBalanceBaseCurrency";
            if (!entity.IsSupportingDetailBalanceBaseCurrencyNull)
                parSupportingDetailBalanceBaseCurrency.Value = entity.SupportingDetailBalanceBaseCurrency;
            else
                parSupportingDetailBalanceBaseCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parSupportingDetailBalanceBaseCurrency);

            System.Data.IDbDataParameter parSupportingDetailBalanceReportingCurrency = cmd.CreateParameter();
            parSupportingDetailBalanceReportingCurrency.ParameterName = "@SupportingDetailBalanceReportingCurrency";
            if (!entity.IsSupportingDetailBalanceReportingCurrencyNull)
                parSupportingDetailBalanceReportingCurrency.Value = entity.SupportingDetailBalanceReportingCurrency;
            else
                parSupportingDetailBalanceReportingCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parSupportingDetailBalanceReportingCurrency);

            System.Data.IDbDataParameter parBaseCurrencyCode = cmd.CreateParameter();
            parBaseCurrencyCode.ParameterName = "@BaseCurrencyCode";
            if (!entity.IsBaseCurrencyCodeNull)
                parBaseCurrencyCode.Value = entity.BaseCurrencyCode;
            else
                parBaseCurrencyCode.Value = System.DBNull.Value;
            cmdParams.Add(parBaseCurrencyCode);

            System.Data.IDbDataParameter parCertificationStatusID = cmd.CreateParameter();
            parCertificationStatusID.ParameterName = "@CertificationStatusID";
            if (!entity.IsCertificationStatusIDNull)
                parCertificationStatusID.Value = entity.CertificationStatusID;
            else
                parCertificationStatusID.Value = System.DBNull.Value;
            cmdParams.Add(parCertificationStatusID);

            System.Data.IDbDataParameter parDataImportID = cmd.CreateParameter();
            parDataImportID.ParameterName = "@DataImportID";
            if (!entity.IsDataImportIDNull)
                parDataImportID.Value = entity.DataImportID;
            else
                parDataImportID.Value = System.DBNull.Value;
            cmdParams.Add(parDataImportID);

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

            System.Data.IDbDataParameter parGLBalanceBaseCurrency = cmd.CreateParameter();
            parGLBalanceBaseCurrency.ParameterName = "@GLBalanceBaseCurrency";
            if (!entity.IsGLBalanceBaseCurrencyNull)
                parGLBalanceBaseCurrency.Value = entity.GLBalanceBaseCurrency;
            else
                parGLBalanceBaseCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parGLBalanceBaseCurrency);

            System.Data.IDbDataParameter parGLBalanceReportingCurrency = cmd.CreateParameter();
            parGLBalanceReportingCurrency.ParameterName = "@GLBalanceReportingCurrency";
            if (!entity.IsGLBalanceReportingCurrencyNull)
                parGLBalanceReportingCurrency.Value = entity.GLBalanceReportingCurrency;
            else
                parGLBalanceReportingCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parGLBalanceReportingCurrency);

            System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            parHostName.ParameterName = "@HostName";
            if (!entity.IsHostNameNull)
                parHostName.Value = entity.HostName;
            else
                parHostName.Value = System.DBNull.Value;
            cmdParams.Add(parHostName);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (!entity.IsIsActiveNull)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parIsAttachmentAvailable = cmd.CreateParameter();
            parIsAttachmentAvailable.ParameterName = "@IsAttachmentAvailable";
            if (!entity.IsIsAttachmentAvailableNull)
                parIsAttachmentAvailable.Value = entity.IsAttachmentAvailable;
            else
                parIsAttachmentAvailable.Value = System.DBNull.Value;
            cmdParams.Add(parIsAttachmentAvailable);

            System.Data.IDbDataParameter parIsMaterial = cmd.CreateParameter();
            parIsMaterial.ParameterName = "@IsMaterial";
            if (!entity.IsIsMaterialNull)
                parIsMaterial.Value = entity.IsMaterial;
            else
                parIsMaterial.Value = System.DBNull.Value;
            cmdParams.Add(parIsMaterial);

            System.Data.IDbDataParameter parIsSystemReconcilied = cmd.CreateParameter();
            parIsSystemReconcilied.ParameterName = "@IsSystemReconcilied";
            if (!entity.IsIsSystemReconciliedNull)
                parIsSystemReconcilied.Value = entity.IsSystemReconcilied;
            else
                parIsSystemReconcilied.Value = System.DBNull.Value;
            cmdParams.Add(parIsSystemReconcilied);

            //System.Data.IDbDataParameter parPreparerSignOffDate = cmd.CreateParameter();
            //parPreparerSignOffDate.ParameterName = "@PreparerSignOffDate";
            //if(!entity.IsPreparerSignOffDateNull)
            //    parPreparerSignOffDate.Value = entity.PendingReviewStatusDate.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
            //else
            //    parPreparerSignOffDate.Value = System.DBNull.Value;
            //cmdParams.Add(parPreparerSignOffDate);

            System.Data.IDbDataParameter parReconciliationBalanceReportingCurrency = cmd.CreateParameter();
            parReconciliationBalanceReportingCurrency.ParameterName = "@ReconciliationBalanceReportingCurrency";
            if (!entity.IsReconciliationBalanceReportingCurrencyNull)
                parReconciliationBalanceReportingCurrency.Value = entity.ReconciliationBalanceReportingCurrency;
            else
                parReconciliationBalanceReportingCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationBalanceReportingCurrency);

            System.Data.IDbDataParameter parReconciliationBalanceBaseCurrency = cmd.CreateParameter();
            parReconciliationBalanceBaseCurrency.ParameterName = "@ReconciliationBalanceBaseCurrency";
            if (!entity.IsReconciliationBalanceBaseCurrencyNull)
                parReconciliationBalanceBaseCurrency.Value = entity.ReconciliationBalanceBaseCurrency;
            else
                parReconciliationBalanceBaseCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationBalanceBaseCurrency);


            System.Data.IDbDataParameter parReconciliationPeriodID = cmd.CreateParameter();
            parReconciliationPeriodID.ParameterName = "@ReconciliationPeriodID";
            if (!entity.IsReconciliationPeriodIDNull)
                parReconciliationPeriodID.Value = entity.ReconciliationPeriodID;
            else
                parReconciliationPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationPeriodID);

            System.Data.IDbDataParameter parReconciliationStatusDate = cmd.CreateParameter();
            parReconciliationStatusDate.ParameterName = "@ReconciliationStatusDate";
            if (!entity.IsReconciliationStatusDateNull)
                parReconciliationStatusDate.Value = entity.ReconciliationStatusDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parReconciliationStatusDate.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationStatusDate);

            System.Data.IDbDataParameter parReconciliationStatusID = cmd.CreateParameter();
            parReconciliationStatusID.ParameterName = "@ReconciliationStatusID";
            if (!entity.IsReconciliationStatusIDNull)
                parReconciliationStatusID.Value = entity.ReconciliationStatusID;
            else
                parReconciliationStatusID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationStatusID);

            //System.Data.IDbDataParameter parReviewerSignOffDate = cmd.CreateParameter();
            //parReviewerSignOffDate.ParameterName = "@ReviewerSignOffDate";
            //if(!entity.IsReviewerSignOffDateNull)
            //    parReviewerSignOffDate.Value = entity.PendingApprovalStatusDate.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
            //else
            //    parReviewerSignOffDate.Value = System.DBNull.Value;
            //cmdParams.Add(parReviewerSignOffDate);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parUnexplainedVarianceReportingCurrency = cmd.CreateParameter();
            parUnexplainedVarianceReportingCurrency.ParameterName = "@UnexplainedVarianceReportingCurrency";
            if (!entity.IsUnexplainedVarianceReportingCurrencyNull)
                parUnexplainedVarianceReportingCurrency.Value = entity.UnexplainedVarianceReportingCurrency;
            else
                parUnexplainedVarianceReportingCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parUnexplainedVarianceReportingCurrency);

            System.Data.IDbDataParameter parUnexplainedVarianceBaseCurrency = cmd.CreateParameter();
            parUnexplainedVarianceBaseCurrency.ParameterName = "@UnexplainedVarianceBaseCurrency";
            if (!entity.IsUnexplainedVarianceBaseCurrencyNull)
                parUnexplainedVarianceBaseCurrency.Value = entity.UnexplainedVarianceBaseCurrency;
            else
                parUnexplainedVarianceBaseCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parUnexplainedVarianceBaseCurrency);

            System.Data.IDbDataParameter parWriteOffAmountBaseCurrency = cmd.CreateParameter();
            parWriteOffAmountBaseCurrency.ParameterName = "@WriteOnOffAmountBaseCurrency";
            if (!entity.IsWriteOnOffAmountBaseCurrencyNull)
                parWriteOffAmountBaseCurrency.Value = entity.WriteOnOffAmountBaseCurrency;
            else
                parWriteOffAmountBaseCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parWriteOffAmountBaseCurrency);

            System.Data.IDbDataParameter parWriteOffAmountReportingCurrency = cmd.CreateParameter();
            parWriteOffAmountReportingCurrency.ParameterName = "@WriteOnOffAmountReportingCurrency";
            if (!entity.IsWriteOnOffAmountReportingCurrencyNull)
                parWriteOffAmountReportingCurrency.Value = entity.WriteOnOffAmountReportingCurrency;
            else
                parWriteOffAmountReportingCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parWriteOffAmountReportingCurrency);


            System.Data.IDbDataParameter pkparGLDataID = cmd.CreateParameter();
            pkparGLDataID.ParameterName = "@GLDataID";
            pkparGLDataID.Value = entity.GLDataID;
            cmdParams.Add(pkparGLDataID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_GLDataHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_GLDataHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataID";
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
        public IList<GLDataHdrInfo> SelectAllByAccountID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_GLDataHdrByAccountID");
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
        public IList<GLDataHdrInfo> SelectAllByReconciliationStatusID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_GLDataHdrByReconciliationStatusID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationStatusID";
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
        public IList<GLDataHdrInfo> SelectAllByCertificationStatusID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_GLDataHdrByCertificationStatusID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CertificationStatusID";
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
        public IList<GLDataHdrInfo> SelectAllByDataImportID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_GLDataHdrByDataImportID");
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
        public IList<GLDataHdrInfo> SelectAllByReconciliationPeriodID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_GLDataHdrByReconciliationPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationPeriodID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(GLDataHdrInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(GLDataHdrDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(GLDataHdrInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(GLDataHdrDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(GLDataHdrInfo entity, object id)
        {
            entity.GLDataID = Convert.ToInt64(id);
        }















        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<GLDataHdrInfo> SelectGLDataHdrDetailsAssociatedToUserHdrByGLDataReconciliationSubmissionDate(UserHdrInfo entity)
        {
            return this.SelectGLDataHdrDetailsAssociatedToUserHdrByGLDataReconciliationSubmissionDate(entity.UserID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<GLDataHdrInfo> SelectGLDataHdrDetailsAssociatedToUserHdrByGLDataReconciliationSubmissionDate(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [GLDataHdr] INNER JOIN [GLDataReconciliationSubmissionDate] ON [GLDataHdr].[GLDataID] = [GLDataReconciliationSubmissionDate].[GLDataID] INNER JOIN [UserHdr] ON [GLDataReconciliationSubmissionDate].[UserID] = [UserHdr].[UserID]  WHERE  [UserHdr].[UserID] = @UserID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserID";
            par.Value = id;

            cmdParams.Add(par);
            List<GLDataHdrInfo> objGLDataHdrEntityColl = new List<GLDataHdrInfo>(this.Select(cmd));
            return objGLDataHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<GLDataHdrInfo> SelectGLDataHdrDetailsAssociatedToRoleMstByGLDataReconciliationSubmissionDate(RoleMstInfo entity)
        {
            return this.SelectGLDataHdrDetailsAssociatedToRoleMstByGLDataReconciliationSubmissionDate(entity.RoleID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<GLDataHdrInfo> SelectGLDataHdrDetailsAssociatedToRoleMstByGLDataReconciliationSubmissionDate(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [GLDataHdr] INNER JOIN [GLDataReconciliationSubmissionDate] ON [GLDataHdr].[GLDataID] = [GLDataReconciliationSubmissionDate].[GLDataID] INNER JOIN [RoleMst] ON [GLDataReconciliationSubmissionDate].[RoleID] = [RoleMst].[RoleID]  WHERE  [RoleMst].[RoleID] = @RoleID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleID";
            par.Value = id;

            cmdParams.Add(par);
            List<GLDataHdrInfo> objGLDataHdrEntityColl = new List<GLDataHdrInfo>(this.Select(cmd));
            return objGLDataHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<GLDataHdrInfo> SelectGLDataHdrDetailsAssociatedToRoleMstByGLDataRoleCertificationDate(RoleMstInfo entity)
        {
            return this.SelectGLDataHdrDetailsAssociatedToRoleMstByGLDataRoleCertificationDate(entity.RoleID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<GLDataHdrInfo> SelectGLDataHdrDetailsAssociatedToRoleMstByGLDataRoleCertificationDate(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [GLDataHdr] INNER JOIN [GLDataRoleCertificationDate] ON [GLDataHdr].[GLDataID] = [GLDataRoleCertificationDate].[GLDataID] INNER JOIN [RoleMst] ON [GLDataRoleCertificationDate].[RoleID] = [RoleMst].[RoleID]  WHERE  [RoleMst].[RoleID] = @RoleID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleID";
            par.Value = id;

            cmdParams.Add(par);
            List<GLDataHdrInfo> objGLDataHdrEntityColl = new List<GLDataHdrInfo>(this.Select(cmd));
            return objGLDataHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<GLDataHdrInfo> SelectGLDataHdrDetailsAssociatedToUserHdrByGLDataWriteOnOff(UserHdrInfo entity)
        {
            return this.SelectGLDataHdrDetailsAssociatedToUserHdrByGLDataWriteOnOff(entity.UserID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<GLDataHdrInfo> SelectGLDataHdrDetailsAssociatedToUserHdrByGLDataWriteOnOff(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [GLDataHdr] INNER JOIN [GLDataWriteOnOff] ON [GLDataHdr].[GLDataID] = [GLDataWriteOnOff].[GLDataID] INNER JOIN [UserHdr] ON [GLDataWriteOnOff].[UserID] = [UserHdr].[UserID]  WHERE  [UserHdr].[UserID] = @UserID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserID";
            par.Value = id;

            cmdParams.Add(par);
            List<GLDataHdrInfo> objGLDataHdrEntityColl = new List<GLDataHdrInfo>(this.Select(cmd));
            return objGLDataHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<GLDataHdrInfo> SelectGLDataHdrDetailsAssociatedToReconciliationCategoryMstByGLReconciliationItemInput(ReconciliationCategoryMstInfo entity)
        {
            return this.SelectGLDataHdrDetailsAssociatedToReconciliationCategoryMstByGLReconciliationItemInput(entity.ReconciliationCategoryID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<GLDataHdrInfo> SelectGLDataHdrDetailsAssociatedToReconciliationCategoryMstByGLReconciliationItemInput(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [GLDataHdr] INNER JOIN [GLReconciliationItemInput] ON [GLDataHdr].[GLDataID] = [GLReconciliationItemInput].[GLDataID] INNER JOIN [ReconciliationCategoryMst] ON [GLReconciliationItemInput].[ReconciliationCategoryID] = [ReconciliationCategoryMst].[ReconciliationCategoryID]  WHERE  [ReconciliationCategoryMst].[ReconciliationCategoryID] = @ReconciliationCategoryID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationCategoryID";
            par.Value = id;

            cmdParams.Add(par);
            List<GLDataHdrInfo> objGLDataHdrEntityColl = new List<GLDataHdrInfo>(this.Select(cmd));
            return objGLDataHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<GLDataHdrInfo> SelectGLDataHdrDetailsAssociatedToReconciliationCategoryTypeMstByGLReconciliationItemInput(ReconciliationCategoryTypeMstInfo entity)
        {
            return this.SelectGLDataHdrDetailsAssociatedToReconciliationCategoryTypeMstByGLReconciliationItemInput(entity.ReconciliationCategoryTypeID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<GLDataHdrInfo> SelectGLDataHdrDetailsAssociatedToReconciliationCategoryTypeMstByGLReconciliationItemInput(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [GLDataHdr] INNER JOIN [GLReconciliationItemInput] ON [GLDataHdr].[GLDataID] = [GLReconciliationItemInput].[GLDataID] INNER JOIN [ReconciliationCategoryTypeMst] ON [GLReconciliationItemInput].[ReconciliationCategoryTypeID] = [ReconciliationCategoryTypeMst].[ReconciliationCategoryTypeID]  WHERE  [ReconciliationCategoryTypeMst].[ReconciliationCategoryTypeID] = @ReconciliationCategoryTypeID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationCategoryTypeID";
            par.Value = id;

            cmdParams.Add(par);
            List<GLDataHdrInfo> objGLDataHdrEntityColl = new List<GLDataHdrInfo>(this.Select(cmd));
            return objGLDataHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<GLDataHdrInfo> SelectGLDataHdrDetailsAssociatedToDataImportHdrByGLReconciliationItemInput(DataImportHdrInfo entity)
        {
            return this.SelectGLDataHdrDetailsAssociatedToDataImportHdrByGLReconciliationItemInput(entity.DataImportID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<GLDataHdrInfo> SelectGLDataHdrDetailsAssociatedToDataImportHdrByGLReconciliationItemInput(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [GLDataHdr] INNER JOIN [GLReconciliationItemInput] ON [GLDataHdr].[GLDataID] = [GLReconciliationItemInput].[GLDataID] INNER JOIN [DataImportHdr] ON [GLReconciliationItemInput].[DataImportID] = [DataImportHdr].[DataImportID]  WHERE  [DataImportHdr].[DataImportID] = @DataImportID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DataImportID";
            par.Value = id;

            cmdParams.Add(par);
            List<GLDataHdrInfo> objGLDataHdrEntityColl = new List<GLDataHdrInfo>(this.Select(cmd));
            return objGLDataHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<GLDataHdrInfo> SelectGLDataHdrDetailsAssociatedToAccountHdrBySubledgerData(AccountHdrInfo entity)
        {
            return this.SelectGLDataHdrDetailsAssociatedToAccountHdrBySubledgerData(entity.AccountID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<GLDataHdrInfo> SelectGLDataHdrDetailsAssociatedToAccountHdrBySubledgerData(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [GLDataHdr] INNER JOIN [SubledgerData] ON [GLDataHdr].[GLDataID] = [SubledgerData].[GLDataID] INNER JOIN [AccountHdr] ON [SubledgerData].[AccountID] = [AccountHdr].[AccountID]  WHERE  [AccountHdr].[AccountID] = @AccountID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AccountID";
            par.Value = id;

            cmdParams.Add(par);
            List<GLDataHdrInfo> objGLDataHdrEntityColl = new List<GLDataHdrInfo>(this.Select(cmd));
            return objGLDataHdrEntityColl;
        }

    }
}
