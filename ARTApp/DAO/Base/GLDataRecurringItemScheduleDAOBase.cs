

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

    public abstract class GLDataRecurringItemScheduleDAOBase : CustomAbstractDAO<GLDataRecurringItemScheduleInfo>
    {

        /// <summary>
        /// A static representation of column AddedBy
        /// </summary>
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
        /// <summary>
        /// A static representation of column CloseComments
        /// </summary>
        public static readonly string COLUMN_CLOSECOMMENTS = "CloseComments";
        /// <summary>
        /// A static representation of column CloseDate
        /// </summary>
        public static readonly string COLUMN_CLOSEDATE = "CloseDate";
        /// <summary>
        /// A static representation of column Comments
        /// </summary>
        public static readonly string COLUMN_COMMENTS = "Comments";
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
        /// A static representation of column GLDataRecurringItemScheduleID
        /// </summary>
        public static readonly string COLUMN_GLDATARECURRINGITEMSCHEDULEID = "GLDataRecurringItemScheduleID";
        /// <summary>
        /// A static representation of column HostName
        /// </summary>
        public static readonly string COLUMN_HOSTNAME = "HostName";
        /// <summary>
        /// A static representation of column IsActive
        /// </summary>
        public static readonly string COLUMN_ISACTIVE = "IsActive";
        /// <summary>
        /// A static representation of column JournalEntryRef
        /// </summary>
        public static readonly string COLUMN_JOURNALENTRYREF = "JournalEntryRef";
        /// <summary>
        /// A static representation of column LocalCurrencyCode
        /// </summary>
        public static readonly string COLUMN_LOCALCURRENCYCODE = "LocalCurrencyCode";
        /// <summary>
        /// A static representation of column OpenDate
        /// </summary>
        public static readonly string COLUMN_OPENDATE = "OpenDate";
        /// <summary>
        /// A static representation of column OriginalGLDataRecurringItemScheduleID
        /// </summary>
        public static readonly string COLUMN_ORIGINALGLDATARECURRINGITEMSCHEDULEID = "OriginalGLDataRecurringItemScheduleID";
        /// <summary>
        /// A static representation of column PreviousGLDataRecurringItemScheduleID
        /// </summary>
        public static readonly string COLUMN_PREVIOUSGLDATARECURRINGITEMSCHEDULEID = "PreviousGLDataRecurringItemScheduleID";
        /// <summary>
        /// A static representation of column ReconciliationTypeID
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONTYPEID = "ReconciliationTypeID";
        /// <summary>
        /// A static representation of column RecPeriodAmountBaseCurrency
        /// </summary>
        public static readonly string COLUMN_RECPERIODAMOUNTBASECURRENCY = "RecPeriodAmountBaseCurrency";
        /// <summary>
        /// A static representation of column RecPeriodAmountLocalCurrency
        /// </summary>
        public static readonly string COLUMN_RECPERIODAMOUNTLOCALCURRENCY = "RecPeriodAmountLocalCurrency";
        /// <summary>
        /// A static representation of column RecPeriodAmountReportingCurrency
        /// </summary>
        public static readonly string COLUMN_RECPERIODAMOUNTREPORTINGCURRENCY = "RecPeriodAmountReportingCurrency";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// A static representation of column ScheduleAmount
        /// </summary>
        public static readonly string COLUMN_SCHEDULEAMOUNT = "ScheduleAmount";
        /// <summary>
        /// A static representation of column ScheduleBeginDate
        /// </summary>
        public static readonly string COLUMN_SCHEDULEBEGINDATE = "ScheduleBeginDate";
        /// <summary>
        /// A static representation of column ScheduleEndDate
        /// </summary>
        public static readonly string COLUMN_SCHEDULEENDDATE = "ScheduleEndDate";
        /// <summary>
        /// A static representation of column ScheduleName
        /// </summary>
        public static readonly string COLUMN_SCHEDULENAME = "ScheduleName";
        /// <summary>
        /// Provides access to the name of the primary key column (GLDataRecurringItemScheduleID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "GLDataRecurringItemScheduleID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "GLDataRecurringItemSchedule";

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
        public GLDataRecurringItemScheduleDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "GLDataRecurringItemSchedule", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a GLDataRecurringItemScheduleInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>GLDataRecurringItemScheduleInfo</returns>
        protected override GLDataRecurringItemScheduleInfo MapObject(System.Data.IDataReader r)
        {

            GLDataRecurringItemScheduleInfo entity = new GLDataRecurringItemScheduleInfo();

            entity.GLDataRecurringItemScheduleID = r.GetInt64Value("GLDataRecurringItemScheduleID");
            entity.GLDataID = r.GetInt64Value("GLDataID");
            entity.ScheduleName = r.GetStringValue("ScheduleName");
            entity.ScheduleBeginDate = r.GetDateValue("ScheduleBeginDate");
            entity.ScheduleEndDate = r.GetDateValue("ScheduleEndDate");
            entity.ScheduleAmount = r.GetDecimalValue("ScheduleAmount");
            entity.ScheduleAmountBaseCurrency = r.GetDecimalValue("ScheduleAmountBaseCurrency");
            entity.ScheduleAmountReportingCurrency = r.GetDecimalValue("ScheduleAmountReportingCurrency");
            entity.LocalCurrencyCode = r.GetStringValue("LocalCurrencyCode");
            entity.OriginalGLDataRecurringItemScheduleID = r.GetInt64Value("OriginalGLDataRecurringItemScheduleID");
            entity.PreviousGLDataRecurringItemScheduleID = r.GetInt64Value("PreviousGLDataRecurringItemScheduleID");
            entity.OpenDate = r.GetDateValue("OpenDate");
            entity.CloseDate = r.GetDateValue("CloseDate");
            entity.ReconciliationCategoryTypeID = r.GetInt16Value("ReconciliationCategoryTypeID");
            entity.RecPeriodAmountLocalCurrency = r.GetDecimalValue("RecPeriodAmountLocalCurrency");
            entity.RecPeriodAmountBaseCurrency = r.GetDecimalValue("RecPeriodAmountBaseCurrency");
            entity.RecPeriodAmountReportingCurrency = r.GetDecimalValue("RecPeriodAmountReportingCurrency");
            entity.JournalEntryRef = r.GetStringValue("JournalEntryRef");
            entity.Comments = r.GetStringValue("Comments");
            entity.CloseComments = r.GetStringValue("CloseComments");
            entity.IsActive = r.GetBooleanValue("IsActive");
            entity.DateAdded = r.GetDateValue("DateAdded");
            entity.AddedBy = r.GetStringValue("AddedBy");
            entity.DateRevised = r.GetDateValue("DateRevised");
            entity.RevisedBy = r.GetStringValue("RevisedBy");
            entity.HostName = r.GetStringValue("HostName");
            entity.DataImportID = r.GetInt32Value("DataImportID");
            entity.BalanceLocalCurrency = r.GetDecimalValue("BalanceLocalCurrency");
            entity.BalanceBaseCurrency = r.GetDecimalValue("BalanceBaseCurrency");
            entity.BalanceReportingCurrency = r.GetDecimalValue("BalanceReportingCurrency");

            entity.AddedByUserID = r.GetInt32Value("AddedByUserID");
            entity.CreatedInRecPeriodID = r.GetInt32Value("CreatedInRecPeriodID");
            entity.PrevRecPeriodID = r.GetInt32Value("PrevRecPeriodID");
            entity.AttachmentCount = r.GetInt32Value("AttachmentCount");

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in GLDataRecurringItemScheduleInfo object
        /// </summary>
        /// <param name="o">A GLDataRecurringItemScheduleInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(GLDataRecurringItemScheduleInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_GLDataRecurringItemSchedule");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parCloseComments = cmd.CreateParameter();
            parCloseComments.ParameterName = "@CloseComments";
            if (!entity.IsCloseCommentsNull)
                parCloseComments.Value = entity.CloseComments;
            else
                parCloseComments.Value = System.DBNull.Value;
            cmdParams.Add(parCloseComments);

            System.Data.IDbDataParameter parCloseDate = cmd.CreateParameter();
            parCloseDate.ParameterName = "@CloseDate";
            if (!entity.IsCloseDateNull)
                parCloseDate.Value = entity.CloseDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parCloseDate.Value = System.DBNull.Value;
            cmdParams.Add(parCloseDate);

            System.Data.IDbDataParameter parComments = cmd.CreateParameter();
            parComments.ParameterName = "@Comments";
            if (!entity.IsCommentsNull)
                parComments.Value = entity.Comments;
            else
                parComments.Value = System.DBNull.Value;
            cmdParams.Add(parComments);

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

            System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            if (!entity.IsGLDataIDNull)
                parGLDataID.Value = entity.GLDataID;
            else
                parGLDataID.Value = System.DBNull.Value;
            cmdParams.Add(parGLDataID);

            //System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            //parHostName.ParameterName = "@HostName";
            //if(!entity.IsHostNameNull)
            //    parHostName.Value = entity.HostName;
            //else
            //    parHostName.Value = System.DBNull.Value;
            //cmdParams.Add(parHostName);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (!entity.IsIsActiveNull)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parJournalEntryRef = cmd.CreateParameter();
            parJournalEntryRef.ParameterName = "@JournalEntryRef";
            if (!entity.IsJournalEntryRefNull)
                parJournalEntryRef.Value = entity.JournalEntryRef;
            else
                parJournalEntryRef.Value = System.DBNull.Value;
            cmdParams.Add(parJournalEntryRef);

            System.Data.IDbDataParameter parLocalCurrencyCode = cmd.CreateParameter();
            parLocalCurrencyCode.ParameterName = "@LocalCurrencyCode";
            if (!entity.IsLocalCurrencyCodeNull)
                parLocalCurrencyCode.Value = entity.LocalCurrencyCode;
            else
                parLocalCurrencyCode.Value = System.DBNull.Value;
            cmdParams.Add(parLocalCurrencyCode);

            System.Data.IDbDataParameter parOpenDate = cmd.CreateParameter();
            parOpenDate.ParameterName = "@OpenDate";
            if (!entity.IsOpenDateNull)
                parOpenDate.Value = entity.OpenDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parOpenDate.Value = System.DBNull.Value;
            cmdParams.Add(parOpenDate);

            System.Data.IDbDataParameter parOriginalGLDataRecurringItemScheduleID = cmd.CreateParameter();
            parOriginalGLDataRecurringItemScheduleID.ParameterName = "@OriginalGLDataRecurringItemScheduleID";
            if (!entity.IsOriginalGLDataRecurringItemScheduleIDNull)
                parOriginalGLDataRecurringItemScheduleID.Value = entity.OriginalGLDataRecurringItemScheduleID;
            else
                parOriginalGLDataRecurringItemScheduleID.Value = System.DBNull.Value;
            cmdParams.Add(parOriginalGLDataRecurringItemScheduleID);

            System.Data.IDbDataParameter parPreviousGLDataRecurringItemScheduleID = cmd.CreateParameter();
            parPreviousGLDataRecurringItemScheduleID.ParameterName = "@PreviousGLDataRecurringItemScheduleID";
            if (!entity.IsPreviousGLDataRecurringItemScheduleIDNull)
                parPreviousGLDataRecurringItemScheduleID.Value = entity.PreviousGLDataRecurringItemScheduleID;
            else
                parPreviousGLDataRecurringItemScheduleID.Value = System.DBNull.Value;
            cmdParams.Add(parPreviousGLDataRecurringItemScheduleID);

            System.Data.IDbDataParameter parReconciliationTypeID = cmd.CreateParameter();
            parReconciliationTypeID.ParameterName = "@ReconciliationCategoryTypeID";
            if (!entity.IsReconciliationCategoryTypeIDNull)
                parReconciliationTypeID.Value = entity.ReconciliationCategoryTypeID;
            else
                parReconciliationTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationTypeID);

            System.Data.IDbDataParameter parRecPeriodAmountBaseCurrency = cmd.CreateParameter();
            parRecPeriodAmountBaseCurrency.ParameterName = "@RecPeriodAmountBaseCurrency";
            if (!entity.IsRecPeriodAmountBaseCurrencyNull)
                parRecPeriodAmountBaseCurrency.Value = entity.RecPeriodAmountBaseCurrency;
            else
                parRecPeriodAmountBaseCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parRecPeriodAmountBaseCurrency);

            System.Data.IDbDataParameter parRecPeriodAmountLocalCurrency = cmd.CreateParameter();
            parRecPeriodAmountLocalCurrency.ParameterName = "@RecPeriodAmountLocalCurrency";
            if (!entity.IsRecPeriodAmountLocalCurrencyNull)
                parRecPeriodAmountLocalCurrency.Value = entity.RecPeriodAmountLocalCurrency;
            else
                parRecPeriodAmountLocalCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parRecPeriodAmountLocalCurrency);

            System.Data.IDbDataParameter parRecPeriodAmountReportingCurrency = cmd.CreateParameter();
            parRecPeriodAmountReportingCurrency.ParameterName = "@RecPeriodAmountReportingCurrency";
            if (!entity.IsRecPeriodAmountReportingCurrencyNull)
                parRecPeriodAmountReportingCurrency.Value = entity.RecPeriodAmountReportingCurrency;
            else
                parRecPeriodAmountReportingCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parRecPeriodAmountReportingCurrency);

            //
            System.Data.IDbDataParameter parBalanceBaseCurrency = cmd.CreateParameter();
            parBalanceBaseCurrency.ParameterName = "@BalanceBaseCurrency";
            if (!entity.IsBalanceBaseCurrencyNull)
                parBalanceBaseCurrency.Value = entity.BalanceBaseCurrency;
            else
                parBalanceBaseCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parBalanceBaseCurrency);


            System.Data.IDbDataParameter parBalanceLocalCurrency = cmd.CreateParameter();
            parBalanceLocalCurrency.ParameterName = "@BalanceLocalCurrency";
            if (!entity.IsBalanceLocalCurrencyNull)
                parBalanceLocalCurrency.Value = entity.BalanceLocalCurrency;
            else
                parBalanceLocalCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parBalanceLocalCurrency);


            System.Data.IDbDataParameter parBalanceReportingCurrency = cmd.CreateParameter();
            parBalanceReportingCurrency.ParameterName = "@BalanceReportingCurrency";
            if (!entity.IsBalanceReportingCurrencyNull)
                parBalanceReportingCurrency.Value = entity.BalanceReportingCurrency;
            else
                parBalanceReportingCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parBalanceReportingCurrency);
            //

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parScheduleAmount = cmd.CreateParameter();
            parScheduleAmount.ParameterName = "@ScheduleAmount";
            if (!entity.IsScheduleAmountNull)
                parScheduleAmount.Value = entity.ScheduleAmount;
            else
                parScheduleAmount.Value = System.DBNull.Value;
            cmdParams.Add(parScheduleAmount);




            System.Data.IDbDataParameter parScheduleAmountBaseCurrency = cmd.CreateParameter();
            parScheduleAmountBaseCurrency.ParameterName = "@ScheduleAmountBaseCurrency";
            if (!entity.IsScheduleAmountBaseCurrencyNull)
                parScheduleAmountBaseCurrency.Value = entity.ScheduleAmountBaseCurrency;
            else
                parScheduleAmountBaseCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parScheduleAmountBaseCurrency);


            System.Data.IDbDataParameter parScheduleAmountReportingCurrency = cmd.CreateParameter();
            parScheduleAmountReportingCurrency.ParameterName = "@ScheduleAmountReportingCurrency";
            if (!entity.IsScheduleAmountReportingCurrencyNull)
                parScheduleAmountReportingCurrency.Value = entity.ScheduleAmountReportingCurrency;
            else
                parScheduleAmountReportingCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parScheduleAmountReportingCurrency);





            System.Data.IDbDataParameter parScheduleBeginDate = cmd.CreateParameter();
            parScheduleBeginDate.ParameterName = "@ScheduleBeginDate";
            if (!entity.IsScheduleBeginDateNull)
                parScheduleBeginDate.Value = entity.ScheduleBeginDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parScheduleBeginDate.Value = System.DBNull.Value;
            cmdParams.Add(parScheduleBeginDate);

            System.Data.IDbDataParameter parScheduleEndDate = cmd.CreateParameter();
            parScheduleEndDate.ParameterName = "@ScheduleEndDate";
            if (!entity.IsScheduleEndDateNull)
                parScheduleEndDate.Value = entity.ScheduleEndDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parScheduleEndDate.Value = System.DBNull.Value;
            cmdParams.Add(parScheduleEndDate);

            System.Data.IDbDataParameter parScheduleName = cmd.CreateParameter();
            parScheduleName.ParameterName = "@ScheduleName";
            if (!entity.IsScheduleNameNull)
                parScheduleName.Value = entity.ScheduleName;
            else
                parScheduleName.Value = System.DBNull.Value;
            cmdParams.Add(parScheduleName);


            System.Data.IDbDataParameter parAddedByUserID = cmd.CreateParameter();
            parAddedByUserID.ParameterName = "@AddedByUserID";
            parAddedByUserID.Value = entity.AddedByUserID.Value;
            cmdParams.Add(parAddedByUserID);

            System.Data.IDbDataParameter parDataImportID = cmd.CreateParameter();
            parDataImportID.ParameterName = "@DataImportID";
            if (entity.DataImportID.HasValue)
                parDataImportID.Value = entity.DataImportID.Value;
            else
                parDataImportID.Value = DBNull.Value;
            cmdParams.Add(parDataImportID);

            System.Data.IDbDataParameter parRecCategoryTypeID = cmd.CreateParameter();
            parRecCategoryTypeID.ParameterName = "@RecCategoryTypeID";
            if (entity.RecCategoryTypeID.HasValue)
                parRecCategoryTypeID.Value = entity.RecCategoryTypeID.Value;
            else
                parRecCategoryTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parRecCategoryTypeID);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in GLDataRecurringItemScheduleInfo object
        /// </summary>
        /// <param name="o">A GLDataRecurringItemScheduleInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(GLDataRecurringItemScheduleInfo entity)
        {



            //System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_GLDataRecurringItemSchedule");
            System.Data.IDbCommand cmd = this.CreateCommand("usp_UPD_GLDataRecurringItemSchedule");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            //System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            //parAddedBy.ParameterName = "@AddedBy";
            //if(!entity.IsAddedByNull)
            //    parAddedBy.Value = entity.AddedBy;
            //else
            //    parAddedBy.Value = System.DBNull.Value;
            //cmdParams.Add(parAddedBy);

            //System.Data.IDbDataParameter parCloseComments = cmd.CreateParameter();
            //parCloseComments.ParameterName = "@CloseComments";
            //if(!entity.IsCloseCommentsNull)
            //    parCloseComments.Value = entity.CloseComments;
            //else
            //    parCloseComments.Value = System.DBNull.Value;
            //cmdParams.Add(parCloseComments);

            System.Data.IDbDataParameter parCloseDate = cmd.CreateParameter();
            parCloseDate.ParameterName = "@CloseDate";
            if (!entity.IsCloseDateNull)
                parCloseDate.Value = entity.CloseDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parCloseDate.Value = System.DBNull.Value;
            cmdParams.Add(parCloseDate);

            System.Data.IDbDataParameter parComments = cmd.CreateParameter();
            parComments.ParameterName = "@Comments";
            if (!entity.IsCommentsNull)
                parComments.Value = entity.Comments;
            else
                parComments.Value = System.DBNull.Value;
            cmdParams.Add(parComments);

            //System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            //parDateAdded.ParameterName = "@DateAdded";
            //if(!entity.IsDateAddedNull)
            //    parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
            //else
            //    parDateAdded.Value = System.DBNull.Value;
            //cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (!entity.IsDateRevisedNull)
                parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            if (!entity.IsGLDataIDNull)
                parGLDataID.Value = entity.GLDataID;
            else
                parGLDataID.Value = System.DBNull.Value;
            cmdParams.Add(parGLDataID);

            //System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            //parHostName.ParameterName = "@HostName";
            //if(!entity.IsHostNameNull)
            //    parHostName.Value = entity.HostName;
            //else
            //    parHostName.Value = System.DBNull.Value;
            //cmdParams.Add(parHostName);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (!entity.IsIsActiveNull)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            //System.Data.IDbDataParameter parJournalEntryRef = cmd.CreateParameter();
            //parJournalEntryRef.ParameterName = "@JournalEntryRef";
            //if(!entity.IsJournalEntryRefNull)
            //    parJournalEntryRef.Value = entity.JournalEntryRef;
            //else
            //    parJournalEntryRef.Value = System.DBNull.Value;
            //cmdParams.Add(parJournalEntryRef);

            System.Data.IDbDataParameter parLocalCurrencyCode = cmd.CreateParameter();
            parLocalCurrencyCode.ParameterName = "@LocalCurrencyCode";
            if (!entity.IsLocalCurrencyCodeNull)
                parLocalCurrencyCode.Value = entity.LocalCurrencyCode;
            else
                parLocalCurrencyCode.Value = System.DBNull.Value;
            cmdParams.Add(parLocalCurrencyCode);

            System.Data.IDbDataParameter parOpenDate = cmd.CreateParameter();
            parOpenDate.ParameterName = "@OpenDate";
            if (!entity.IsOpenDateNull)
                parOpenDate.Value = entity.OpenDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parOpenDate.Value = System.DBNull.Value;
            cmdParams.Add(parOpenDate);

            //System.Data.IDbDataParameter parOriginalGLDataRecurringItemScheduleID = cmd.CreateParameter();
            //parOriginalGLDataRecurringItemScheduleID.ParameterName = "@OriginalGLDataRecurringItemScheduleID";
            //if(!entity.IsOriginalGLDataRecurringItemScheduleIDNull)
            //    parOriginalGLDataRecurringItemScheduleID.Value = entity.OriginalGLDataRecurringItemScheduleID;
            //else
            //    parOriginalGLDataRecurringItemScheduleID.Value = System.DBNull.Value;
            //cmdParams.Add(parOriginalGLDataRecurringItemScheduleID);

            //System.Data.IDbDataParameter parPreviousGLDataRecurringItemScheduleID = cmd.CreateParameter();
            //parPreviousGLDataRecurringItemScheduleID.ParameterName = "@PreviousGLDataRecurringItemScheduleID";
            //if(!entity.IsPreviousGLDataRecurringItemScheduleIDNull)
            //    parPreviousGLDataRecurringItemScheduleID.Value = entity.PreviousGLDataRecurringItemScheduleID;
            //else
            //    parPreviousGLDataRecurringItemScheduleID.Value = System.DBNull.Value;
            //cmdParams.Add(parPreviousGLDataRecurringItemScheduleID);

            System.Data.IDbDataParameter parReconciliationTypeID = cmd.CreateParameter();
            parReconciliationTypeID.ParameterName = "@ReconciliationCategoryTypeID";
            if (!entity.IsReconciliationCategoryTypeIDNull)
                parReconciliationTypeID.Value = entity.ReconciliationCategoryTypeID;
            else
                parReconciliationTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationTypeID);

            System.Data.IDbDataParameter parRecPeriodAmountBaseCurrency = cmd.CreateParameter();
            parRecPeriodAmountBaseCurrency.ParameterName = "@RecPeriodAmountBaseCurrency";
            if (!entity.IsRecPeriodAmountBaseCurrencyNull)
                parRecPeriodAmountBaseCurrency.Value = entity.RecPeriodAmountBaseCurrency;
            else
                parRecPeriodAmountBaseCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parRecPeriodAmountBaseCurrency);

            System.Data.IDbDataParameter parRecPeriodAmountLocalCurrency = cmd.CreateParameter();
            parRecPeriodAmountLocalCurrency.ParameterName = "@RecPeriodAmountLocalCurrency";
            if (!entity.IsRecPeriodAmountLocalCurrencyNull)
                parRecPeriodAmountLocalCurrency.Value = entity.RecPeriodAmountLocalCurrency;
            else
                parRecPeriodAmountLocalCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parRecPeriodAmountLocalCurrency);

            System.Data.IDbDataParameter parRecPeriodAmountReportingCurrency = cmd.CreateParameter();
            parRecPeriodAmountReportingCurrency.ParameterName = "@RecPeriodAmountReportingCurrency";
            if (!entity.IsRecPeriodAmountReportingCurrencyNull)
                parRecPeriodAmountReportingCurrency.Value = entity.RecPeriodAmountReportingCurrency;
            else
                parRecPeriodAmountReportingCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parRecPeriodAmountReportingCurrency);



            System.Data.IDbDataParameter parBalanceBaseCurrency = cmd.CreateParameter();
            parBalanceBaseCurrency.ParameterName = "@BalanceBaseCurrency";
            if (!entity.IsBalanceBaseCurrencyNull)
                parBalanceBaseCurrency.Value = entity.BalanceBaseCurrency;
            else
                parBalanceBaseCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parBalanceBaseCurrency);

            System.Data.IDbDataParameter parBalanceLocalCurrency = cmd.CreateParameter();
            parBalanceLocalCurrency.ParameterName = "@BalanceLocalCurrency";
            if (!entity.IsBalanceLocalCurrencyNull)
                parBalanceLocalCurrency.Value = entity.BalanceLocalCurrency;
            else
                parBalanceLocalCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parBalanceLocalCurrency);

            System.Data.IDbDataParameter parBalanceReportingCurrency = cmd.CreateParameter();
            parBalanceReportingCurrency.ParameterName = "@BalanceReportingCurrency";
            if (!entity.IsBalanceReportingCurrencyNull)
                parBalanceReportingCurrency.Value = entity.BalanceReportingCurrency;
            else
                parBalanceReportingCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parBalanceReportingCurrency);




            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parScheduleAmount = cmd.CreateParameter();
            parScheduleAmount.ParameterName = "@ScheduleAmount";
            if (!entity.IsScheduleAmountNull)
                parScheduleAmount.Value = entity.ScheduleAmount;
            else
                parScheduleAmount.Value = System.DBNull.Value;
            cmdParams.Add(parScheduleAmount);



            System.Data.IDbDataParameter parScheduleAmountBaseCurrency = cmd.CreateParameter();
            parScheduleAmountBaseCurrency.ParameterName = "@ScheduleAmountBaseCurrency";
            if (!entity.IsScheduleAmountBaseCurrencyNull)
                parScheduleAmountBaseCurrency.Value = entity.ScheduleAmountBaseCurrency;
            else
                parScheduleAmountBaseCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parScheduleAmountBaseCurrency);


            System.Data.IDbDataParameter parScheduleAmountReportingCurrency = cmd.CreateParameter();
            parScheduleAmountReportingCurrency.ParameterName = "@ScheduleAmountReportingCurrency";
            if (!entity.IsScheduleAmountReportingCurrencyNull)
                parScheduleAmountReportingCurrency.Value = entity.ScheduleAmountReportingCurrency;
            else
                parScheduleAmountReportingCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parScheduleAmountReportingCurrency);


            System.Data.IDbDataParameter parScheduleBeginDate = cmd.CreateParameter();
            parScheduleBeginDate.ParameterName = "@ScheduleBeginDate";
            if (!entity.IsScheduleBeginDateNull)
                parScheduleBeginDate.Value = entity.ScheduleBeginDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parScheduleBeginDate.Value = System.DBNull.Value;
            cmdParams.Add(parScheduleBeginDate);

            System.Data.IDbDataParameter parScheduleEndDate = cmd.CreateParameter();
            parScheduleEndDate.ParameterName = "@ScheduleEndDate";
            if (!entity.IsScheduleEndDateNull)
                parScheduleEndDate.Value = entity.ScheduleEndDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parScheduleEndDate.Value = System.DBNull.Value;
            cmdParams.Add(parScheduleEndDate);

            System.Data.IDbDataParameter parScheduleName = cmd.CreateParameter();
            parScheduleName.ParameterName = "@ScheduleName";
            if (!entity.IsScheduleNameNull)
                parScheduleName.Value = entity.ScheduleName;
            else
                parScheduleName.Value = System.DBNull.Value;
            cmdParams.Add(parScheduleName);

            System.Data.IDbDataParameter pkparGLDataRecurringItemScheduleID = cmd.CreateParameter();
            pkparGLDataRecurringItemScheduleID.ParameterName = "@GLDataRecurringItemScheduleID";
            pkparGLDataRecurringItemScheduleID.Value = entity.GLDataRecurringItemScheduleID;
            cmdParams.Add(pkparGLDataRecurringItemScheduleID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_GLDataRecurringItemSchedule");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataRecurringItemScheduleID";
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


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_GLDataRecurringItemSchedule");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataRecurringItemScheduleID";
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
        public List<GLDataRecurringItemScheduleInfo> SelectAllByGLDataID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_GLDataRecurringItemScheduleByGLDataID");
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
        public IList<GLDataRecurringItemScheduleInfo> SelectAllByOriginalGLDataRecurringItemScheduleID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_GLDataRecurringItemScheduleByOriginalGLDataRecurringItemScheduleID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@OriginalGLDataRecurringItemScheduleID";
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
        public IList<GLDataRecurringItemScheduleInfo> SelectAllByPreviousGLDataRecurringItemScheduleID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_GLDataRecurringItemScheduleByPreviousGLDataRecurringItemScheduleID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@PreviousGLDataRecurringItemScheduleID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }







        protected override void CustomSave(GLDataRecurringItemScheduleInfo o, IDbConnection connection)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(GLDataRecurringItemScheduleDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        protected override void CustomSave(GLDataRecurringItemScheduleInfo o, IDbConnection connection, IDbTransaction transaction)
        {

            string query = QueryHelper.GetSqlServerLastInsertedCommand(GLDataRecurringItemScheduleDAO.TABLE_NAME);
            IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
            cmd.CommandText = query;
            cmd.Transaction = transaction;
            cmd.Connection = connection;
            object id = cmd.ExecuteScalar();
            this.MapIdentity(o, id);


        }

        private void MapIdentity(GLDataRecurringItemScheduleInfo entity, object id)
        {
            entity.GLDataRecurringItemScheduleID = Convert.ToInt64(id);
        }










        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        //public IList<GLDataRecurringItemScheduleInfo> SelectGLDataRecurringItemScheduleDetailsAssociatedToGLDataRecurringItemScheduleByGLDataRecurringItemSchedule(GLDataRecurringItemScheduleInfo entity)
        //{
        //    return this.SelectGLDataRecurringItemScheduleDetailsAssociatedToGLDataRecurringItemScheduleByGLDataRecurringItemSchedule(entity.GLDataRecurringItemScheduleID);		
        //}

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        //public IList<GLDataRecurringItemScheduleInfo> SelectGLDataRecurringItemScheduleDetailsAssociatedToGLDataRecurringItemScheduleByGLDataRecurringItemSchedule(object id)
        //{							

        //        System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [GLDataRecurringItemSchedule] INNER JOIN [GLDataRecurringItemSchedule] ON [GLDataRecurringItemSchedule].[GLDataRecurringItemScheduleID] = [GLDataRecurringItemSchedule].[OriginalGLDataRecurringItemScheduleID] INNER JOIN [GLDataRecurringItemSchedule] ON [GLDataRecurringItemSchedule].[PreviousGLDataRecurringItemScheduleID] = [GLDataRecurringItemSchedule].[GLDataRecurringItemScheduleID]  WHERE  [GLDataRecurringItemSchedule].[GLDataRecurringItemScheduleID] = @GLDataRecurringItemScheduleID ");
        //        IDataParameterCollection cmdParams = cmd.Parameters;
        //        System.Data.IDbDataParameter par = cmd.CreateParameter();
        //        par.ParameterName = "@GLDataRecurringItemScheduleID";
        //        par.Value = id;

        //        cmdParams.Add(par);
        //        List<GLDataRecurringItemScheduleInfo> objGLDataRecurringItemScheduleEntityColl = new List<GLDataRecurringItemScheduleInfo>(this.Select(cmd));
        //        return objGLDataRecurringItemScheduleEntityColl;
        //}





        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        //public IList<GLDataRecurringItemScheduleInfo> SelectGLDataRecurringItemScheduleDetailsAssociatedToGLDataRecurringItemScheduleByGLDataRecurringItemSchedule(GLDataRecurringItemScheduleInfo entity)
        //{
        //    return this.SelectGLDataRecurringItemScheduleDetailsAssociatedToGLDataRecurringItemScheduleByGLDataRecurringItemSchedule(entity.GLDataRecurringItemScheduleID);		
        //}

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        //public IList<GLDataRecurringItemScheduleInfo> SelectGLDataRecurringItemScheduleDetailsAssociatedToGLDataRecurringItemScheduleByGLDataRecurringItemSchedule(object id)
        //{							

        //        System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [GLDataRecurringItemSchedule] INNER JOIN [GLDataRecurringItemSchedule] ON [GLDataRecurringItemSchedule].[GLDataRecurringItemScheduleID] = [GLDataRecurringItemSchedule].[PreviousGLDataRecurringItemScheduleID] INNER JOIN [GLDataRecurringItemSchedule] ON [GLDataRecurringItemSchedule].[OriginalGLDataRecurringItemScheduleID] = [GLDataRecurringItemSchedule].[GLDataRecurringItemScheduleID]  WHERE  [GLDataRecurringItemSchedule].[GLDataRecurringItemScheduleID] = @GLDataRecurringItemScheduleID ");
        //        IDataParameterCollection cmdParams = cmd.Parameters;
        //        System.Data.IDbDataParameter par = cmd.CreateParameter();
        //        par.ParameterName = "@GLDataRecurringItemScheduleID";
        //        par.Value = id;

        //        cmdParams.Add(par);
        //        List<GLDataRecurringItemScheduleInfo> objGLDataRecurringItemScheduleEntityColl = new List<GLDataRecurringItemScheduleInfo>(this.Select(cmd));
        //        return objGLDataRecurringItemScheduleEntityColl;
        //}

    }
}
