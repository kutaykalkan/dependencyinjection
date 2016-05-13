

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

    public abstract class ReconciliationPeriodDAOBase : CustomAbstractDAO<ReconciliationPeriodInfo>//Adapdev.Data.AbstractDAO<ReconciliationPeriodInfo>
    {

        /// <summary>
        /// A static representation of column ActivationDate
        /// </summary>
        public static readonly string COLUMN_ACTIVATIONDATE = "ActivationDate";
        /// <summary>
        /// A static representation of column AddedBy
        /// </summary>
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
        /// <summary>
        /// A static representation of column BaseCurrency
        /// </summary>
        public static readonly string COLUMN_BASECURRENCY = "BaseCurrency";
        /// <summary>
        /// A static representation of column CertificationStartDate
        /// </summary>
        public static readonly string COLUMN_CertificationStartDate = "CertificationStartDate";
        /// <summary>
        /// A static representation of column CertificationLockDownDate
        /// </summary>
        public static readonly string COLUMN_CERTIFICATIONLOCKDOWNDATE = "CertificationLockDownDate";
        /// <summary>
        /// A static representation of column CompanyID
        /// </summary>
        public static readonly string COLUMN_COMPANYID = "CompanyID";
        /// <summary>
        /// A static representation of column CompanyMaterialityThreshold
        /// </summary>
        public static readonly string COLUMN_COMPANYMATERIALITYTHRESHOLD = "CompanyMaterialityThreshold";
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
        /// A static representation of column MaterialityTypeID
        /// </summary>
        public static readonly string COLUMN_MATERIALITYTYPEID = "MaterialityTypeID";
        /// <summary>
        /// A static representation of column PeriodEndDate
        /// </summary>
        public static readonly string COLUMN_PERIODENDDATE = "PeriodEndDate";
        /// <summary>
        /// A static representation of column PeriodNumber
        /// </summary>
        public static readonly string COLUMN_PERIODNUMBER = "PeriodNumber";
        /// <summary>
        /// A static representation of column ReconciliationCloseDate
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONCLOSEDATE = "ReconciliationCloseDate";
        /// <summary>
        /// A static representation of column ReconciliationPeriodID
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONPERIODID = "ReconciliationPeriodID";
        /// <summary>
        /// A static representation of column ReportingCurrency
        /// </summary>
        public static readonly string COLUMN_REPORTINGCURRENCY = "ReportingCurrency";
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
        /// Provides access to the name of the primary key column (ReconciliationPeriodID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "ReconciliationPeriodID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "ReconciliationPeriod";

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
        public ReconciliationPeriodDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "ReconciliationPeriod", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a ReconciliationPeriodInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>ReconciliationPeriodInfo</returns>
        protected override ReconciliationPeriodInfo MapObject(System.Data.IDataReader r)
        {
            ReconciliationPeriodInfo entity = new ReconciliationPeriodInfo();
            entity.PreparerDueDate = r.GetDateValue("PreparerDueDate");
            entity.ReviewerDueDate = r.GetDateValue("ReviewerDueDate");
            entity.ApproverDueDate = r.GetDateValue("ApproverDueDate");
            entity.AllowCertificationLockdown = r.GetBooleanValue("AllowCertificationLockdown");
            entity.ReconciliationPeriodID = r.GetInt32Value("ReconciliationPeriodID");
            entity.CompanyID = r.GetInt32Value("CompanyID");
            entity.DataImportID = r.GetInt32Value("DataImportID");
            entity.PeriodNumber = r.GetInt16Value("PeriodNumber");
            entity.PeriodEndDate = r.GetDateValue("PeriodEndDate");
            entity.ReconciliationPeriodStatusID = r.GetInt16Value("ReconciliationPeriodStatusID");
            entity.CertificationStartDate = r.GetDateValue("CertificationStartDate");
            entity.CertificationLockDownDate = r.GetDateValue("CertificationLockDownDate");
            entity.ReconciliationCloseDate = r.GetDateValue("ReconciliationCloseDate");
            entity.ReportingCurrencyCode = r.GetStringValue("ReportingCurrencyCode");
            entity.ActivationDate = r.GetDateValue("ActivationDate");
            entity.BaseCurrency = r.GetStringValue("BaseCurrency");
            entity.ReportingCurrency = r.GetStringValue("ReportingCurrency");
            entity.MaterialityTypeID = r.GetInt16Value("MaterialityTypeID");
            entity.CompanyMaterialityThreshold = r.GetDecimalValue("CompanyMaterialityThreshold");
            entity.UnexplainedVarianceThreshold = r.GetDecimalValue("UnexplainedVarianceThreshold");
            entity.IsActive = r.GetBooleanValue("IsActive");
            entity.DateAdded = r.GetDateValue("DateAdded");
            entity.AddedBy = r.GetStringValue("AddedBy");
            entity.DateRevised = r.GetDateValue("DateRevised");
            entity.RevisedBy = r.GetStringValue("RevisedBy");
            entity.HostName = r.GetStringValue("HostName");
            entity.FinancialYearID = r.GetInt32Value("FinancialYearID");
            entity.IsStopRecAndStartCert = r.GetBooleanValue("IsStopRecAndStartCert");
            entity.ReconciliationPeriodStatusLabelID = r.GetInt32Value("ReconciliationPeriodStatusLabelID");
            entity.NextReconciliationCloseDate = r.GetDateValue("NextReconciliationCloseDate");
            entity.PreviousReconciliationCloseDate = r.GetDateValue("PreviousReconciliationCloseDate");
            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in ReconciliationPeriodInfo object
        /// </summary>
        /// <param name="o">A ReconciliationPeriodInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(ReconciliationPeriodInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_ReconciliationPeriod");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parActivationDate = cmd.CreateParameter();
            parActivationDate.ParameterName = "@ActivationDate";
            if (!entity.IsActivationDateNull)
                parActivationDate.Value = entity.ActivationDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parActivationDate.Value = System.DBNull.Value;
            cmdParams.Add(parActivationDate);

            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parBaseCurrency = cmd.CreateParameter();
            parBaseCurrency.ParameterName = "@BaseCurrency";
            if (!entity.IsBaseCurrencyNull)
                parBaseCurrency.Value = entity.BaseCurrency;
            else
                parBaseCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parBaseCurrency);

            System.Data.IDbDataParameter parCertificationStartDate = cmd.CreateParameter();
            parCertificationStartDate.ParameterName = "@CertificationStartDate";
            if (!entity.IsCertificationStartDateNull)
                parCertificationStartDate.Value = entity.CertificationStartDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parCertificationStartDate.Value = System.DBNull.Value;
            cmdParams.Add(parCertificationStartDate);

            System.Data.IDbDataParameter parCertificationLockDownDate = cmd.CreateParameter();
            parCertificationLockDownDate.ParameterName = "@CertificationLockDownDate";
            if (!entity.IsCertificationLockDownDateNull)
                parCertificationLockDownDate.Value = entity.CertificationLockDownDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parCertificationLockDownDate.Value = System.DBNull.Value;
            cmdParams.Add(parCertificationLockDownDate);

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

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (!entity.IsIsActiveNull)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parMaterialityTypeID = cmd.CreateParameter();
            parMaterialityTypeID.ParameterName = "@MaterialityTypeID";
            if (!entity.IsMaterialityTypeIDNull)
                parMaterialityTypeID.Value = entity.MaterialityTypeID;
            else
                parMaterialityTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parMaterialityTypeID);

            System.Data.IDbDataParameter parPeriodEndDate = cmd.CreateParameter();
            parPeriodEndDate.ParameterName = "@PeriodEndDate";
            if (!entity.IsPeriodEndDateNull)
                parPeriodEndDate.Value = entity.PeriodEndDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parPeriodEndDate.Value = System.DBNull.Value;
            cmdParams.Add(parPeriodEndDate);

            System.Data.IDbDataParameter parPeriodNumber = cmd.CreateParameter();
            parPeriodNumber.ParameterName = "@PeriodNumber";
            if (!entity.IsPeriodNumberNull)
                parPeriodNumber.Value = entity.PeriodNumber;
            else
                parPeriodNumber.Value = System.DBNull.Value;
            cmdParams.Add(parPeriodNumber);

            System.Data.IDbDataParameter parReconciliationCloseDate = cmd.CreateParameter();
            parReconciliationCloseDate.ParameterName = "@ReconciliationCloseDate";
            if (!entity.IsReconciliationCloseDateNull)
                parReconciliationCloseDate.Value = entity.ReconciliationCloseDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parReconciliationCloseDate.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationCloseDate);

            System.Data.IDbDataParameter parReportingCurrency = cmd.CreateParameter();
            parReportingCurrency.ParameterName = "@ReportingCurrency";
            if (!entity.IsReportingCurrencyNull)
                parReportingCurrency.Value = entity.ReportingCurrency;
            else
                parReportingCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parReportingCurrency);

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
        /// in ReconciliationPeriodInfo object
        /// </summary>
        /// <param name="o">A ReconciliationPeriodInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(ReconciliationPeriodInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_ReconciliationPeriod");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parActivationDate = cmd.CreateParameter();
            parActivationDate.ParameterName = "@ActivationDate";
            if (!entity.IsActivationDateNull)
                parActivationDate.Value = entity.ActivationDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parActivationDate.Value = System.DBNull.Value;
            cmdParams.Add(parActivationDate);

            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parBaseCurrency = cmd.CreateParameter();
            parBaseCurrency.ParameterName = "@BaseCurrency";
            if (!entity.IsBaseCurrencyNull)
                parBaseCurrency.Value = entity.BaseCurrency;
            else
                parBaseCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parBaseCurrency);

            System.Data.IDbDataParameter parCertificationStartDate = cmd.CreateParameter();
            parCertificationStartDate.ParameterName = "@CertificationStartDate";
            if (!entity.IsCertificationStartDateNull)
                parCertificationStartDate.Value = entity.CertificationStartDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parCertificationStartDate.Value = System.DBNull.Value;
            cmdParams.Add(parCertificationStartDate);

            System.Data.IDbDataParameter parCertificationLockDownDate = cmd.CreateParameter();
            parCertificationLockDownDate.ParameterName = "@CertificationLockDownDate";
            if (!entity.IsCertificationLockDownDateNull)
                parCertificationLockDownDate.Value = entity.CertificationLockDownDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parCertificationLockDownDate.Value = System.DBNull.Value;
            cmdParams.Add(parCertificationLockDownDate);

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

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (!entity.IsIsActiveNull)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parMaterialityTypeID = cmd.CreateParameter();
            parMaterialityTypeID.ParameterName = "@MaterialityTypeID";
            if (!entity.IsMaterialityTypeIDNull)
                parMaterialityTypeID.Value = entity.MaterialityTypeID;
            else
                parMaterialityTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parMaterialityTypeID);

            System.Data.IDbDataParameter parPeriodEndDate = cmd.CreateParameter();
            parPeriodEndDate.ParameterName = "@PeriodEndDate";
            if (!entity.IsPeriodEndDateNull)
                parPeriodEndDate.Value = entity.PeriodEndDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parPeriodEndDate.Value = System.DBNull.Value;
            cmdParams.Add(parPeriodEndDate);

            System.Data.IDbDataParameter parPeriodNumber = cmd.CreateParameter();
            parPeriodNumber.ParameterName = "@PeriodNumber";
            if (!entity.IsPeriodNumberNull)
                parPeriodNumber.Value = entity.PeriodNumber;
            else
                parPeriodNumber.Value = System.DBNull.Value;
            cmdParams.Add(parPeriodNumber);

            System.Data.IDbDataParameter parReconciliationCloseDate = cmd.CreateParameter();
            parReconciliationCloseDate.ParameterName = "@ReconciliationCloseDate";
            if (!entity.IsReconciliationCloseDateNull)
                parReconciliationCloseDate.Value = entity.ReconciliationCloseDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parReconciliationCloseDate.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationCloseDate);

            System.Data.IDbDataParameter parReportingCurrency = cmd.CreateParameter();
            parReportingCurrency.ParameterName = "@ReportingCurrency";
            if (!entity.IsReportingCurrencyNull)
                parReportingCurrency.Value = entity.ReportingCurrency;
            else
                parReportingCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parReportingCurrency);

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

            System.Data.IDbDataParameter pkparReconciliationPeriodID = cmd.CreateParameter();
            pkparReconciliationPeriodID.ParameterName = "@ReconciliationPeriodID";
            pkparReconciliationPeriodID.Value = entity.ReconciliationPeriodID;
            cmdParams.Add(pkparReconciliationPeriodID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_ReconciliationPeriod");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationPeriodID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_ReconciliationPeriod");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationPeriodID";
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
        public IList<ReconciliationPeriodInfo> SelectAllByCompanyID(object id, int? CurrentFinancialYearID)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_ReconciliationPeriodByCompanyID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyID";
            if (id != null)
                par.Value = id;
            else
                par.Value = DBNull.Value;
            cmdParams.Add(par);

            System.Data.IDbDataParameter parCurrentFinancialYearID = cmd.CreateParameter();
            parCurrentFinancialYearID.ParameterName = "@CurrentFinancialYearID";
            if (CurrentFinancialYearID.HasValue)
                parCurrentFinancialYearID.Value = CurrentFinancialYearID.Value;
            else
                parCurrentFinancialYearID.Value = DBNull.Value;
            cmdParams.Add(parCurrentFinancialYearID);

            return this.Select(cmd);
        }


        /// <summary>
        /// Creates the sql select command, using the passed in foreign key.  This will return an
        /// IList of all objects that have that foreign key.
        /// </summary>
        /// <param name="o">The foreign key of the objects to select</param>
        /// <returns>An IList</returns>
        public IList<ReconciliationPeriodInfo> SelectAllByMaterialityTypeID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_ReconciliationPeriodByMaterialityTypeID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MaterialityTypeID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(ReconciliationPeriodInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(ReconciliationPeriodDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(ReconciliationPeriodInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(ReconciliationPeriodDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(ReconciliationPeriodInfo entity, object id)
        {
            entity.ReconciliationPeriodID = Convert.ToInt32(id);
        }


















        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToAccountHdrByAccountReconciliationPeriod(AccountHdrInfo entity)
        {
            return this.SelectReconciliationPeriodDetailsAssociatedToAccountHdrByAccountReconciliationPeriod(entity.AccountID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToAccountHdrByAccountReconciliationPeriod(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReconciliationPeriod] INNER JOIN [AccountReconciliationPeriod] ON [ReconciliationPeriod].[ReconciliationPeriodID] = [AccountReconciliationPeriod].[ReconciliationPeriodID] INNER JOIN [AccountHdr] ON [AccountReconciliationPeriod].[AccountID] = [AccountHdr].[AccountID]  WHERE  [AccountHdr].[AccountID] = @AccountID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AccountID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReconciliationPeriodInfo> objReconciliationPeriodEntityColl = new List<ReconciliationPeriodInfo>(this.Select(cmd));
            return objReconciliationPeriodEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToCompanyHdrByCertificationSignOffStatus(CompanyHdrInfo entity)
        {
            return this.SelectReconciliationPeriodDetailsAssociatedToCompanyHdrByCertificationSignOffStatus(entity.CompanyID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToCompanyHdrByCertificationSignOffStatus(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReconciliationPeriod] INNER JOIN [CertificationSignOffStatus] ON [ReconciliationPeriod].[ReconciliationPeriodID] = [CertificationSignOffStatus].[ReconciliationPeriodID] INNER JOIN [CompanyHdr] ON [CertificationSignOffStatus].[CompanyID] = [CompanyHdr].[CompanyID]  WHERE  [CompanyHdr].[CompanyID] = @CompanyID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReconciliationPeriodInfo> objReconciliationPeriodEntityColl = new List<ReconciliationPeriodInfo>(this.Select(cmd));
            return objReconciliationPeriodEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToCertificationTypeMstByCertificationSignOffStatus(CertificationTypeMstInfo entity)
        {
            return this.SelectReconciliationPeriodDetailsAssociatedToCertificationTypeMstByCertificationSignOffStatus(entity.CertificationTypeID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToCertificationTypeMstByCertificationSignOffStatus(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReconciliationPeriod] INNER JOIN [CertificationSignOffStatus] ON [ReconciliationPeriod].[ReconciliationPeriodID] = [CertificationSignOffStatus].[ReconciliationPeriodID] INNER JOIN [CertificationTypeMst] ON [CertificationSignOffStatus].[CertificationTypeID] = [CertificationTypeMst].[CertificationTypeID]  WHERE  [CertificationTypeMst].[CertificationTypeID] = @CertificationTypeID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CertificationTypeID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReconciliationPeriodInfo> objReconciliationPeriodEntityColl = new List<ReconciliationPeriodInfo>(this.Select(cmd));
            return objReconciliationPeriodEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToUserHdrByCertificationSignOffStatus(UserHdrInfo entity)
        {
            return this.SelectReconciliationPeriodDetailsAssociatedToUserHdrByCertificationSignOffStatus(entity.UserID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToUserHdrByCertificationSignOffStatus(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReconciliationPeriod] INNER JOIN [CertificationSignOffStatus] ON [ReconciliationPeriod].[ReconciliationPeriodID] = [CertificationSignOffStatus].[ReconciliationPeriodID] INNER JOIN [UserHdr] ON [CertificationSignOffStatus].[UserID] = [UserHdr].[UserID]  WHERE  [UserHdr].[UserID] = @UserID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReconciliationPeriodInfo> objReconciliationPeriodEntityColl = new List<ReconciliationPeriodInfo>(this.Select(cmd));
            return objReconciliationPeriodEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToCompanyHdrByCompanySetting(CompanyHdrInfo entity)
        {
            return this.SelectReconciliationPeriodDetailsAssociatedToCompanyHdrByCompanySetting(entity.CompanyID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToCompanyHdrByCompanySetting(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReconciliationPeriod] INNER JOIN [CompanySetting] ON [ReconciliationPeriod].[ReconciliationPeriodID] = [CompanySetting].[OpenReconciliationPeriodID] INNER JOIN [CompanyHdr] ON [CompanySetting].[CompanyID] = [CompanyHdr].[CompanyID]  WHERE  [CompanyHdr].[CompanyID] = @CompanyID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReconciliationPeriodInfo> objReconciliationPeriodEntityColl = new List<ReconciliationPeriodInfo>(this.Select(cmd));
            return objReconciliationPeriodEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToMaterialityTypeMstByCompanySetting(MaterialityTypeMstInfo entity)
        {
            return this.SelectReconciliationPeriodDetailsAssociatedToMaterialityTypeMstByCompanySetting(entity.MaterialityTypeID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToMaterialityTypeMstByCompanySetting(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReconciliationPeriod] INNER JOIN [CompanySetting] ON [ReconciliationPeriod].[ReconciliationPeriodID] = [CompanySetting].[OpenReconciliationPeriodID] INNER JOIN [MaterialityTypeMst] ON [CompanySetting].[MaterialityTypeID] = [MaterialityTypeMst].[MaterialityTypeID]  WHERE  [MaterialityTypeMst].[MaterialityTypeID] = @MaterialityTypeID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MaterialityTypeID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReconciliationPeriodInfo> objReconciliationPeriodEntityColl = new List<ReconciliationPeriodInfo>(this.Select(cmd));
            return objReconciliationPeriodEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToCompanyHdrByDataImportHdr(CompanyHdrInfo entity)
        {
            return this.SelectReconciliationPeriodDetailsAssociatedToCompanyHdrByDataImportHdr(entity.CompanyID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToCompanyHdrByDataImportHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReconciliationPeriod] INNER JOIN [DataImportHdr] ON [ReconciliationPeriod].[ReconciliationPeriodID] = [DataImportHdr].[ReonciliationPeriodID] INNER JOIN [CompanyHdr] ON [DataImportHdr].[CompanyID] = [CompanyHdr].[CompanyID]  WHERE  [CompanyHdr].[CompanyID] = @CompanyID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReconciliationPeriodInfo> objReconciliationPeriodEntityColl = new List<ReconciliationPeriodInfo>(this.Select(cmd));
            return objReconciliationPeriodEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToDataImportTypeMstByDataImportHdr(DataImportTypeMstInfo entity)
        {
            return this.SelectReconciliationPeriodDetailsAssociatedToDataImportTypeMstByDataImportHdr(entity.DataImportTypeID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToDataImportTypeMstByDataImportHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReconciliationPeriod] INNER JOIN [DataImportHdr] ON [ReconciliationPeriod].[ReconciliationPeriodID] = [DataImportHdr].[ReonciliationPeriodID] INNER JOIN [DataImportTypeMst] ON [DataImportHdr].[DataImportTypeID] = [DataImportTypeMst].[DataImportTypeID]  WHERE  [DataImportTypeMst].[DataImportTypeID] = @DataImportTypeID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DataImportTypeID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReconciliationPeriodInfo> objReconciliationPeriodEntityColl = new List<ReconciliationPeriodInfo>(this.Select(cmd));
            return objReconciliationPeriodEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToDataImportStatusMstByDataImportHdr(DataImportStatusMstInfo entity)
        {
            return this.SelectReconciliationPeriodDetailsAssociatedToDataImportStatusMstByDataImportHdr(entity.DataImportStatusID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToDataImportStatusMstByDataImportHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReconciliationPeriod] INNER JOIN [DataImportHdr] ON [ReconciliationPeriod].[ReconciliationPeriodID] = [DataImportHdr].[ReonciliationPeriodID] INNER JOIN [DataImportStatusMst] ON [DataImportHdr].[DataImportStatusID] = [DataImportStatusMst].[DataImportStatusID]  WHERE  [DataImportStatusMst].[DataImportStatusID] = @DataImportStatusID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DataImportStatusID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReconciliationPeriodInfo> objReconciliationPeriodEntityColl = new List<ReconciliationPeriodInfo>(this.Select(cmd));
            return objReconciliationPeriodEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToAccountHdrByGLDataHdr(AccountHdrInfo entity)
        {
            return this.SelectReconciliationPeriodDetailsAssociatedToAccountHdrByGLDataHdr(entity.AccountID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToAccountHdrByGLDataHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReconciliationPeriod] INNER JOIN [GLDataHdr] ON [ReconciliationPeriod].[ReconciliationPeriodID] = [GLDataHdr].[ReconciliationPeriodID] INNER JOIN [AccountHdr] ON [GLDataHdr].[AccountID] = [AccountHdr].[AccountID]  WHERE  [AccountHdr].[AccountID] = @AccountID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AccountID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReconciliationPeriodInfo> objReconciliationPeriodEntityColl = new List<ReconciliationPeriodInfo>(this.Select(cmd));
            return objReconciliationPeriodEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToReconciliationStatusMstByGLDataHdr(ReconciliationStatusMstInfo entity)
        {
            return this.SelectReconciliationPeriodDetailsAssociatedToReconciliationStatusMstByGLDataHdr(entity.ReconciliationStatusID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToReconciliationStatusMstByGLDataHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReconciliationPeriod] INNER JOIN [GLDataHdr] ON [ReconciliationPeriod].[ReconciliationPeriodID] = [GLDataHdr].[ReconciliationPeriodID] INNER JOIN [ReconciliationStatusMst] ON [GLDataHdr].[ReconciliationStatusID] = [ReconciliationStatusMst].[ReconciliationStatusID]  WHERE  [ReconciliationStatusMst].[ReconciliationStatusID] = @ReconciliationStatusID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationStatusID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReconciliationPeriodInfo> objReconciliationPeriodEntityColl = new List<ReconciliationPeriodInfo>(this.Select(cmd));
            return objReconciliationPeriodEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToCertificationStatusMstByGLDataHdr(CertificationStatusMstInfo entity)
        {
            return this.SelectReconciliationPeriodDetailsAssociatedToCertificationStatusMstByGLDataHdr(entity.CertificationStatusID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToCertificationStatusMstByGLDataHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReconciliationPeriod] INNER JOIN [GLDataHdr] ON [ReconciliationPeriod].[ReconciliationPeriodID] = [GLDataHdr].[ReconciliationPeriodID] INNER JOIN [CertificationStatusMst] ON [GLDataHdr].[CertificationStatusID] = [CertificationStatusMst].[CertificationStatusID]  WHERE  [CertificationStatusMst].[CertificationStatusID] = @CertificationStatusID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CertificationStatusID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReconciliationPeriodInfo> objReconciliationPeriodEntityColl = new List<ReconciliationPeriodInfo>(this.Select(cmd));
            return objReconciliationPeriodEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToDataImportHdrByGLDataHdr(DataImportHdrInfo entity)
        {
            return this.SelectReconciliationPeriodDetailsAssociatedToDataImportHdrByGLDataHdr(entity.DataImportID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToDataImportHdrByGLDataHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReconciliationPeriod] INNER JOIN [GLDataHdr] ON [ReconciliationPeriod].[ReconciliationPeriodID] = [GLDataHdr].[ReconciliationPeriodID] INNER JOIN [DataImportHdr] ON [GLDataHdr].[DataImportID] = [DataImportHdr].[DataImportID]  WHERE  [DataImportHdr].[DataImportID] = @DataImportID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DataImportID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReconciliationPeriodInfo> objReconciliationPeriodEntityColl = new List<ReconciliationPeriodInfo>(this.Select(cmd));
            return objReconciliationPeriodEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToRoleMandatoryReportByMandatoryReportSignoffStatus(RoleMandatoryReportInfo entity)
        {
            return this.SelectReconciliationPeriodDetailsAssociatedToRoleMandatoryReportByMandatoryReportSignoffStatus(entity.RoleMandatoryReportID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToRoleMandatoryReportByMandatoryReportSignoffStatus(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReconciliationPeriod] INNER JOIN [MandatoryReportSignoffStatus] ON [ReconciliationPeriod].[ReconciliationPeriodID] = [MandatoryReportSignoffStatus].[ReconciliationPeriodID] INNER JOIN [RoleMandatoryReport] ON [MandatoryReportSignoffStatus].[RoleMandatoryReportID] = [RoleMandatoryReport].[RoleMandatoryReportID]  WHERE  [RoleMandatoryReport].[RoleMandatoryReportID] = @RoleMandatoryReportID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleMandatoryReportID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReconciliationPeriodInfo> objReconciliationPeriodEntityColl = new List<ReconciliationPeriodInfo>(this.Select(cmd));
            return objReconciliationPeriodEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToUserHdrByMandatoryReportSignoffStatus(UserHdrInfo entity)
        {
            return this.SelectReconciliationPeriodDetailsAssociatedToUserHdrByMandatoryReportSignoffStatus(entity.UserID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToUserHdrByMandatoryReportSignoffStatus(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReconciliationPeriod] INNER JOIN [MandatoryReportSignoffStatus] ON [ReconciliationPeriod].[ReconciliationPeriodID] = [MandatoryReportSignoffStatus].[ReconciliationPeriodID] INNER JOIN [UserHdr] ON [MandatoryReportSignoffStatus].[UserID] = [UserHdr].[UserID]  WHERE  [UserHdr].[UserID] = @UserID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReconciliationPeriodInfo> objReconciliationPeriodEntityColl = new List<ReconciliationPeriodInfo>(this.Select(cmd));
            return objReconciliationPeriodEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToRecurringItemScheduleByRecurringItemScheduleReconciliationPeriod(RecurringItemScheduleInfo entity)
        {
            return this.SelectReconciliationPeriodDetailsAssociatedToRecurringItemScheduleByRecurringItemScheduleReconciliationPeriod(entity.RecurringItemScheduleID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToRecurringItemScheduleByRecurringItemScheduleReconciliationPeriod(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReconciliationPeriod] INNER JOIN [RecurringItemScheduleReconciliationPeriod] ON [ReconciliationPeriod].[ReconciliationPeriodID] = [RecurringItemScheduleReconciliationPeriod].[ReconciliationPeriodID] INNER JOIN [RecurringItemSchedule] ON [RecurringItemScheduleReconciliationPeriod].[AccurableItemScheduleID] = [RecurringItemSchedule].[RecurringItemScheduleID]  WHERE  [RecurringItemSchedule].[RecurringItemScheduleID] = @RecurringItemScheduleID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RecurringItemScheduleID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReconciliationPeriodInfo> objReconciliationPeriodEntityColl = new List<ReconciliationPeriodInfo>(this.Select(cmd));
            return objReconciliationPeriodEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToReportMstByReportSignOffStatus(ReportMstInfo entity)
        {
            return this.SelectReconciliationPeriodDetailsAssociatedToReportMstByReportSignOffStatus(entity.ReportID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToReportMstByReportSignOffStatus(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReconciliationPeriod] INNER JOIN [ReportSignOffStatus] ON [ReconciliationPeriod].[ReconciliationPeriodID] = [ReportSignOffStatus].[ReconciliationPeriodID] INNER JOIN [ReportMst] ON [ReportSignOffStatus].[ReportID] = [ReportMst].[ReportID]  WHERE  [ReportMst].[ReportID] = @ReportID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReportID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReconciliationPeriodInfo> objReconciliationPeriodEntityColl = new List<ReconciliationPeriodInfo>(this.Select(cmd));
            return objReconciliationPeriodEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToUserHdrByReportSignOffStatus(UserHdrInfo entity)
        {
            return this.SelectReconciliationPeriodDetailsAssociatedToUserHdrByReportSignOffStatus(entity.UserID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToUserHdrByReportSignOffStatus(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReconciliationPeriod] INNER JOIN [ReportSignOffStatus] ON [ReconciliationPeriod].[ReconciliationPeriodID] = [ReportSignOffStatus].[ReconciliationPeriodID] INNER JOIN [UserHdr] ON [ReportSignOffStatus].[UserID] = [UserHdr].[UserID]  WHERE  [UserHdr].[UserID] = @UserID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReconciliationPeriodInfo> objReconciliationPeriodEntityColl = new List<ReconciliationPeriodInfo>(this.Select(cmd));
            return objReconciliationPeriodEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToCompanyHdrByRiskRatingReconciliationPeriod(CompanyHdrInfo entity)
        {
            return this.SelectReconciliationPeriodDetailsAssociatedToCompanyHdrByRiskRatingReconciliationPeriod(entity.CompanyID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToCompanyHdrByRiskRatingReconciliationPeriod(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReconciliationPeriod] INNER JOIN [RiskRatingReconciliationPeriod] ON [ReconciliationPeriod].[ReconciliationPeriodID] = [RiskRatingReconciliationPeriod].[ReconciliationPeriodID] INNER JOIN [CompanyHdr] ON [RiskRatingReconciliationPeriod].[CompanyID] = [CompanyHdr].[CompanyID]  WHERE  [CompanyHdr].[CompanyID] = @CompanyID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReconciliationPeriodInfo> objReconciliationPeriodEntityColl = new List<ReconciliationPeriodInfo>(this.Select(cmd));
            return objReconciliationPeriodEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToRiskRatingMstByRiskRatingReconciliationPeriod(RiskRatingMstInfo entity)
        {
            return this.SelectReconciliationPeriodDetailsAssociatedToRiskRatingMstByRiskRatingReconciliationPeriod(entity.RiskRatingID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToRiskRatingMstByRiskRatingReconciliationPeriod(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReconciliationPeriod] INNER JOIN [RiskRatingReconciliationPeriod] ON [ReconciliationPeriod].[ReconciliationPeriodID] = [RiskRatingReconciliationPeriod].[ReconciliationPeriodID] INNER JOIN [RiskRatingMst] ON [RiskRatingReconciliationPeriod].[RiskRatingID] = [RiskRatingMst].[RiskRatingID]  WHERE  [RiskRatingMst].[RiskRatingID] = @RiskRatingID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RiskRatingID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReconciliationPeriodInfo> objReconciliationPeriodEntityColl = new List<ReconciliationPeriodInfo>(this.Select(cmd));
            return objReconciliationPeriodEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToRoleReconciliationDueDateByRoleReconciliationDueDate(RoleReconciliationDueDateInfo entity)
        {
            return this.SelectReconciliationPeriodDetailsAssociatedToRoleReconciliationDueDateByRoleReconciliationDueDate(entity.RoleReconciliationDueDateID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToRoleReconciliationDueDateByRoleReconciliationDueDate(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReconciliationPeriod] INNER JOIN [RoleReconciliationDueDate] ON [ReconciliationPeriod].[ReconciliationPeriodID] = [RoleReconciliationDueDate].[ReconciliationPeriodID] INNER JOIN [RoleReconciliationDueDate] ON [RoleReconciliationDueDate].[RoleReconciliationDueDateID] = [RoleReconciliationDueDate].[RoleReconciliationDueDateID]  WHERE  [RoleReconciliationDueDate].[RoleReconciliationDueDateID] = @RoleReconciliationDueDateID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleReconciliationDueDateID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReconciliationPeriodInfo> objReconciliationPeriodEntityColl = new List<ReconciliationPeriodInfo>(this.Select(cmd));
            return objReconciliationPeriodEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToRoleMstByRoleReconciliationDueDate(RoleMstInfo entity)
        {
            return this.SelectReconciliationPeriodDetailsAssociatedToRoleMstByRoleReconciliationDueDate(entity.RoleID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<ReconciliationPeriodInfo> SelectReconciliationPeriodDetailsAssociatedToRoleMstByRoleReconciliationDueDate(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [ReconciliationPeriod] INNER JOIN [RoleReconciliationDueDate] ON [ReconciliationPeriod].[ReconciliationPeriodID] = [RoleReconciliationDueDate].[ReconciliationPeriodID] INNER JOIN [RoleMst] ON [RoleReconciliationDueDate].[RoleID] = [RoleMst].[RoleID]  WHERE  [RoleMst].[RoleID] = @RoleID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleID";
            par.Value = id;

            cmdParams.Add(par);
            List<ReconciliationPeriodInfo> objReconciliationPeriodEntityColl = new List<ReconciliationPeriodInfo>(this.Select(cmd));
            return objReconciliationPeriodEntityColl;
        }

        public IList<ReconciliationPeriodInfo> SelectAllPeriodNumberByCompanyID(int? CompanyID, DateTime? periodEndDate)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_RecPeriodNumberByCompanyID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = CompanyID;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parPeriodEndDate = cmd.CreateParameter();
            parPeriodEndDate.ParameterName = "@PeriodEndDate";
            parPeriodEndDate.Value = periodEndDate;
            cmdParams.Add(parPeriodEndDate);

            return this.Select(cmd);
        }

    }
}
