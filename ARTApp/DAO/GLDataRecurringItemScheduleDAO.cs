using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Data;

namespace SkyStem.ART.App.DAO
{
    public class GLDataRecurringItemScheduleDAO : GLDataRecurringItemScheduleDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GLDataRecurringItemScheduleDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        public long InsertGLDataRecurringItemSchedule(GLDataRecurringItemScheduleInfo entity, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            long gLDataRecurringItemScheduleID = 0;
            IDbCommand cmd = null;
            cmd = this.CreateInsertGLDataRecurringItemScheduleCommand(entity);
            cmd.Connection = oConnection;
            cmd.Transaction = oTransaction;
            object obj = cmd.ExecuteScalar();
            gLDataRecurringItemScheduleID = Convert.ToInt64(obj);
            return gLDataRecurringItemScheduleID;

        }

        protected IDbCommand CreateInsertGLDataRecurringItemScheduleCommand(GLDataRecurringItemScheduleInfo entity)
        {

            DataTable oGLDataRecurringItemScheduleIntervalDetailDataTable = ServiceHelper.ConvertGLDataRecurringItemScheduleIntervalDetailToDataTable(entity.GLDataRecurringItemScheduleIntervalDetailInfoList);

            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_GLDataRecurringItemSchedule");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!string.IsNullOrEmpty(entity.AddedBy))
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parCloseComments = cmd.CreateParameter();
            parCloseComments.ParameterName = "@CloseComments";
            if (!string.IsNullOrEmpty(entity.CloseComments))
                parCloseComments.Value = entity.CloseComments;
            else
                parCloseComments.Value = System.DBNull.Value;
            cmdParams.Add(parCloseComments);

            System.Data.IDbDataParameter parCloseDate = cmd.CreateParameter();
            parCloseDate.ParameterName = "@CloseDate";
            if (entity.CloseDate.HasValue)
                parCloseDate.Value = entity.CloseDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parCloseDate.Value = System.DBNull.Value;
            cmdParams.Add(parCloseDate);

            System.Data.IDbDataParameter parComments = cmd.CreateParameter();
            parComments.ParameterName = "@Comments";
            if (!string.IsNullOrEmpty(entity.Comments))
                parComments.Value = entity.Comments;
            else
                parComments.Value = System.DBNull.Value;
            cmdParams.Add(parComments);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (entity.DateAdded.HasValue)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (entity.DateRevised.HasValue)
                parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            if (entity.GLDataID.HasValue)
                parGLDataID.Value = entity.GLDataID;
            else
                parGLDataID.Value = System.DBNull.Value;
            cmdParams.Add(parGLDataID);


            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (entity.IsActive.HasValue)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parJournalEntryRef = cmd.CreateParameter();
            parJournalEntryRef.ParameterName = "@JournalEntryRef";
            if (!string.IsNullOrEmpty(entity.JournalEntryRef))
                parJournalEntryRef.Value = entity.JournalEntryRef;
            else
                parJournalEntryRef.Value = System.DBNull.Value;
            cmdParams.Add(parJournalEntryRef);

            System.Data.IDbDataParameter parLocalCurrencyCode = cmd.CreateParameter();
            parLocalCurrencyCode.ParameterName = "@LocalCurrencyCode";
            if (!string.IsNullOrEmpty(entity.LocalCurrencyCode))
                parLocalCurrencyCode.Value = entity.LocalCurrencyCode;
            else
                parLocalCurrencyCode.Value = System.DBNull.Value;
            cmdParams.Add(parLocalCurrencyCode);

            System.Data.IDbDataParameter parOpenDate = cmd.CreateParameter();
            parOpenDate.ParameterName = "@OpenDate";
            if (entity.OpenDate.HasValue)
                parOpenDate.Value = entity.OpenDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parOpenDate.Value = System.DBNull.Value;
            cmdParams.Add(parOpenDate);

            System.Data.IDbDataParameter parOriginalGLDataRecurringItemScheduleID = cmd.CreateParameter();
            parOriginalGLDataRecurringItemScheduleID.ParameterName = "@OriginalGLDataRecurringItemScheduleID";
            if (entity.OriginalGLDataRecurringItemScheduleID.HasValue)
                parOriginalGLDataRecurringItemScheduleID.Value = entity.OriginalGLDataRecurringItemScheduleID;
            else
                parOriginalGLDataRecurringItemScheduleID.Value = System.DBNull.Value;
            cmdParams.Add(parOriginalGLDataRecurringItemScheduleID);

            System.Data.IDbDataParameter parPreviousGLDataRecurringItemScheduleID = cmd.CreateParameter();
            parPreviousGLDataRecurringItemScheduleID.ParameterName = "@PreviousGLDataRecurringItemScheduleID";
            if (entity.PreviousGLDataRecurringItemScheduleID.HasValue)
                parPreviousGLDataRecurringItemScheduleID.Value = entity.PreviousGLDataRecurringItemScheduleID;
            else
                parPreviousGLDataRecurringItemScheduleID.Value = System.DBNull.Value;
            cmdParams.Add(parPreviousGLDataRecurringItemScheduleID);

            System.Data.IDbDataParameter parReconciliationTypeID = cmd.CreateParameter();
            parReconciliationTypeID.ParameterName = "@ReconciliationCategoryTypeID";
            if (entity.ReconciliationCategoryTypeID.HasValue)
                parReconciliationTypeID.Value = entity.ReconciliationCategoryTypeID;
            else
                parReconciliationTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationTypeID);

            System.Data.IDbDataParameter parRecPeriodAmountBaseCurrency = cmd.CreateParameter();
            parRecPeriodAmountBaseCurrency.ParameterName = "@RecPeriodAmountBaseCurrency";
            if (entity.RecPeriodAmountBaseCurrency.HasValue)
                parRecPeriodAmountBaseCurrency.Value = entity.RecPeriodAmountBaseCurrency;
            else
                parRecPeriodAmountBaseCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parRecPeriodAmountBaseCurrency);

            System.Data.IDbDataParameter parRecPeriodAmountLocalCurrency = cmd.CreateParameter();
            parRecPeriodAmountLocalCurrency.ParameterName = "@RecPeriodAmountLocalCurrency";
            if (entity.RecPeriodAmountLocalCurrency.HasValue)
                parRecPeriodAmountLocalCurrency.Value = entity.RecPeriodAmountLocalCurrency;
            else
                parRecPeriodAmountLocalCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parRecPeriodAmountLocalCurrency);

            System.Data.IDbDataParameter parRecPeriodAmountReportingCurrency = cmd.CreateParameter();
            parRecPeriodAmountReportingCurrency.ParameterName = "@RecPeriodAmountReportingCurrency";
            if (entity.RecPeriodAmountReportingCurrency.HasValue)
                parRecPeriodAmountReportingCurrency.Value = entity.RecPeriodAmountReportingCurrency;
            else
                parRecPeriodAmountReportingCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parRecPeriodAmountReportingCurrency);

            //
            System.Data.IDbDataParameter parBalanceBaseCurrency = cmd.CreateParameter();
            parBalanceBaseCurrency.ParameterName = "@BalanceBaseCurrency";
            if (entity.BalanceBaseCurrency.HasValue)
                parBalanceBaseCurrency.Value = entity.BalanceBaseCurrency;
            else
                parBalanceBaseCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parBalanceBaseCurrency);


            System.Data.IDbDataParameter parBalanceLocalCurrency = cmd.CreateParameter();
            parBalanceLocalCurrency.ParameterName = "@BalanceLocalCurrency";
            if (entity.BalanceLocalCurrency.HasValue)
                parBalanceLocalCurrency.Value = entity.BalanceLocalCurrency;
            else
                parBalanceLocalCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parBalanceLocalCurrency);


            System.Data.IDbDataParameter parBalanceReportingCurrency = cmd.CreateParameter();
            parBalanceReportingCurrency.ParameterName = "@BalanceReportingCurrency";
            if (entity.BalanceReportingCurrency.HasValue)
                parBalanceReportingCurrency.Value = entity.BalanceReportingCurrency;
            else
                parBalanceReportingCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parBalanceReportingCurrency);
            //

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!string.IsNullOrEmpty(entity.RevisedBy))
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parScheduleAmount = cmd.CreateParameter();
            parScheduleAmount.ParameterName = "@ScheduleAmount";
            if (entity.ScheduleAmount.HasValue)
                parScheduleAmount.Value = entity.ScheduleAmount;
            else
                parScheduleAmount.Value = System.DBNull.Value;
            cmdParams.Add(parScheduleAmount);


            System.Data.IDbDataParameter parScheduleAmountBaseCurrency = cmd.CreateParameter();
            parScheduleAmountBaseCurrency.ParameterName = "@ScheduleAmountBaseCurrency";
            if (entity.ScheduleAmountBaseCurrency.HasValue)
                parScheduleAmountBaseCurrency.Value = entity.ScheduleAmountBaseCurrency;
            else
                parScheduleAmountBaseCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parScheduleAmountBaseCurrency);


            System.Data.IDbDataParameter parScheduleAmountReportingCurrency = cmd.CreateParameter();
            parScheduleAmountReportingCurrency.ParameterName = "@ScheduleAmountReportingCurrency";
            if (entity.ScheduleAmountReportingCurrency.HasValue)
                parScheduleAmountReportingCurrency.Value = entity.ScheduleAmountReportingCurrency;
            else
                parScheduleAmountReportingCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parScheduleAmountReportingCurrency);


            System.Data.IDbDataParameter parScheduleBeginDate = cmd.CreateParameter();
            parScheduleBeginDate.ParameterName = "@ScheduleBeginDate";
            if (entity.ScheduleBeginDate.HasValue)
                parScheduleBeginDate.Value = entity.ScheduleBeginDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parScheduleBeginDate.Value = System.DBNull.Value;
            cmdParams.Add(parScheduleBeginDate);

            System.Data.IDbDataParameter parScheduleEndDate = cmd.CreateParameter();
            parScheduleEndDate.ParameterName = "@ScheduleEndDate";
            if (entity.ScheduleEndDate.HasValue)
                parScheduleEndDate.Value = entity.ScheduleEndDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parScheduleEndDate.Value = System.DBNull.Value;
            cmdParams.Add(parScheduleEndDate);

            System.Data.IDbDataParameter parScheduleName = cmd.CreateParameter();
            parScheduleName.ParameterName = "@ScheduleName";
            if (!string.IsNullOrEmpty(entity.ScheduleName))
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


            System.Data.IDbDataParameter parGLDataRecurringItemScheduleIntervalDetailList = cmd.CreateParameter();
            parGLDataRecurringItemScheduleIntervalDetailList.ParameterName = "@dtGLDataRecurringItemScheduleIntervalDetailList";
            parGLDataRecurringItemScheduleIntervalDetailList.Value = oGLDataRecurringItemScheduleIntervalDetailDataTable;
            cmdParams.Add(parGLDataRecurringItemScheduleIntervalDetailList);


            System.Data.IDbDataParameter parCalculationFrequencyID = cmd.CreateParameter();
            parCalculationFrequencyID.ParameterName = "@CalculationFrequencyID";
            if (entity.CalculationFrequencyID.HasValue)
                parCalculationFrequencyID.Value = entity.CalculationFrequencyID.Value;
            else
                parCalculationFrequencyID.Value = DBNull.Value;
            cmdParams.Add(parCalculationFrequencyID);


            System.Data.IDbDataParameter parTotalIntervals = cmd.CreateParameter();
            parTotalIntervals.ParameterName = "@TotalIntervals";
            if (entity.TotalIntervals.HasValue)
                parTotalIntervals.Value = entity.TotalIntervals.Value;
            else
                parTotalIntervals.Value = DBNull.Value;
            cmdParams.Add(parTotalIntervals);


            System.Data.IDbDataParameter parCurrentInterval = cmd.CreateParameter();
            parCurrentInterval.ParameterName = "@CurrentInterval";
            if (entity.CurrentInterval.HasValue)
                parCurrentInterval.Value = entity.CurrentInterval.Value;
            else
                parCurrentInterval.Value = DBNull.Value;
            cmdParams.Add(parCurrentInterval);

            System.Data.IDbDataParameter parStartIntervalRecPeriodID = cmd.CreateParameter();
            parStartIntervalRecPeriodID.ParameterName = "@StartIntervalRecPeriodID";
            if (entity.StartIntervalRecPeriodID.HasValue)
                parStartIntervalRecPeriodID.Value = entity.StartIntervalRecPeriodID.Value;
            else
                parStartIntervalRecPeriodID.Value = DBNull.Value;
            cmdParams.Add(parStartIntervalRecPeriodID);

            System.Data.IDbDataParameter parRecordSourceTypeID = cmd.CreateParameter();
            parRecordSourceTypeID.ParameterName = "@RecordSourceTypeID";
            parRecordSourceTypeID.Value = entity.RecordSourceTypeID;
            cmdParams.Add(parRecordSourceTypeID);

            System.Data.IDbDataParameter parRecordSourceID = cmd.CreateParameter();
            parRecordSourceID.ParameterName = "@RecordSourceID";
            parRecordSourceID.Value = entity.RecordSourceID;
            cmdParams.Add(parRecordSourceID);

            System.Data.IDbDataParameter parExRateLCCYtoBCCY = cmd.CreateParameter();
            parExRateLCCYtoBCCY.ParameterName = "@ExRateLCCYtoBCCY";
            if (entity.ExRateLCCYtoBCCY.HasValue)
                parExRateLCCYtoBCCY.Value = entity.ExRateLCCYtoBCCY;
            else
                parExRateLCCYtoBCCY.Value = System.DBNull.Value;
            cmd.Parameters.Add(parExRateLCCYtoBCCY);

            System.Data.IDbDataParameter parExRateLCCYtoRCCY = cmd.CreateParameter();
            parExRateLCCYtoRCCY.ParameterName = "@ExRateLCCYtoRCCY";
            if (entity.ExRateLCCYtoRCCY.HasValue)
                parExRateLCCYtoRCCY.Value = entity.ExRateLCCYtoRCCY;
            else
                parExRateLCCYtoRCCY.Value = System.DBNull.Value;
            cmd.Parameters.Add(parExRateLCCYtoRCCY);

            System.Data.IDbDataParameter parIgnoreInCalculation = cmd.CreateParameter();
            parIgnoreInCalculation.ParameterName = "@IgnoreInCalculation";
            if (entity.IgnoreInCalculation.HasValue)
                parIgnoreInCalculation.Value = entity.IgnoreInCalculation;
            else
                parIgnoreInCalculation.Value = System.DBNull.Value;
            cmd.Parameters.Add(parIgnoreInCalculation);

            return cmd;
        }


        public List<GLDataRecurringItemScheduleInfo> GetGLDataRecurringItemSchedule(long? gLDataID)
        {
            List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection = new List<GLDataRecurringItemScheduleInfo>();
            IDbCommand cmd = null;
            IDbConnection con = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateGetGLDataRecurringItemScheduleCommand(gLDataID);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo = null;
                while (reader.Read())
                {
                    oGLDataRecurringItemScheduleInfo = this.MapObject(reader);
                    oGLDataRecurringItemScheduleInfo.AddedByFirstName = reader.GetStringValue("AddedByFirstName");
                    oGLDataRecurringItemScheduleInfo.AddedByLastName = reader.GetStringValue("AddedByLastName");
                    oGLDataRecurringItemScheduleInfo.RecItemNumber = reader.GetStringValue("RecItemNumber");
                    oGLDataRecurringItemScheduleInfo.CalculationFrequencyID = reader.GetInt16Value("CalculationFrequencyID");
                    oGLDataRecurringItemScheduleInfo.TotalIntervals = reader.GetInt32Value("TotalIntervals");
                    oGLDataRecurringItemScheduleInfo.CurrentInterval = reader.GetInt32Value("CurrentInterval");
                    oGLDataRecurringItemScheduleInfo.StartIntervalRecPeriodID = reader.GetInt32Value("StartIntervalRecPeriodID");
                    oGLDataRecurringItemScheduleInfo.MatchSetRefNumber = reader.GetStringValue("MatchSetRef");
                    oGLDataRecurringItemScheduleInfo.MatchSetID = reader.GetInt64Value("MatchSetID");
                    oGLDataRecurringItemScheduleInfo.MatchSetSubSetCombinationID = reader.GetInt64Value("MatchSetSubSetCombinationID");
                    oGLDataRecurringItemScheduleInfo.ExRateLCCYtoBCCY = reader.GetDecimalValue("ExRateLCCYtoBCCY");
                    oGLDataRecurringItemScheduleInfo.ExRateLCCYtoRCCY = reader.GetDecimalValue("ExRateLCCYtoRCCY");
                    oGLDataRecurringItemScheduleInfo.PhysicalPath = reader.GetStringValue("PhysicalPath");
                    oGLDataRecurringItemScheduleInfo.CrrentRecPeriodAmount = reader.GetDecimalValue("CrrentRecPeriodAmount");
                    oGLDataRecurringItemScheduleInfo.IgnoreInCalculation = reader.GetBooleanValue("IgnoreInCalculation");
                    oGLDataRecurringItemScheduleInfo.IsCommentAvailable = reader.GetBooleanValue("IsCommentAvailable");
                    oGLDataRecurringItemScheduleInfo.DataImportTypeID = reader.GetInt16Value("DataImportTypeID");
                    oGLDataRecurringItemScheduleInfoCollection.Add(oGLDataRecurringItemScheduleInfo);

                }
                reader.Close();
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed)
                    con.Dispose();
            }

            return oGLDataRecurringItemScheduleInfoCollection;
        }


        private System.Data.IDbCommand CreateGetGLDataRecurringItemScheduleCommand(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_GLDataRecurringItemScheduleByGLDataID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;
        }

        public void UpdateGLDataRecurringItemSchedule(GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo, short recTemplateAttributeID, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            IDbCommand cmd = CreateUpdateGLDataRecurringItemScheduleCommand(oGLDataRecurringItemScheduleInfo);

            IDbDataParameter paramRecTemplateAttributeID = cmd.CreateParameter();
            paramRecTemplateAttributeID.ParameterName = "@RecTemplateAttributeID";
            paramRecTemplateAttributeID.Value = recTemplateAttributeID;
            cmd.Parameters.Add(paramRecTemplateAttributeID);


            cmd.Connection = oConnection;
            cmd.Transaction = oTransaction;

            cmd.ExecuteNonQuery();
        }


        protected System.Data.IDbCommand CreateUpdateGLDataRecurringItemScheduleCommand(GLDataRecurringItemScheduleInfo entity)
        {

            DataTable oGLDataRecurringItemScheduleIntervalDetailDataTable = ServiceHelper.ConvertGLDataRecurringItemScheduleIntervalDetailToDataTable(entity.GLDataRecurringItemScheduleIntervalDetailInfoList);
            System.Data.IDbCommand cmd = this.CreateCommand("usp_UPD_GLDataRecurringItemSchedule");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parCloseDate = cmd.CreateParameter();
            parCloseDate.ParameterName = "@CloseDate";
            if (entity.CloseDate.HasValue)
                parCloseDate.Value = entity.CloseDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parCloseDate.Value = System.DBNull.Value;
            cmdParams.Add(parCloseDate);

            System.Data.IDbDataParameter parComments = cmd.CreateParameter();
            parComments.ParameterName = "@Comments";
            if (!String.IsNullOrEmpty(entity.Comments))
                parComments.Value = entity.Comments;
            else
                parComments.Value = System.DBNull.Value;
            cmdParams.Add(parComments);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (entity.DateRevised.HasValue)
                parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            if (entity.GLDataID.HasValue)
                parGLDataID.Value = entity.GLDataID;
            else
                parGLDataID.Value = System.DBNull.Value;
            cmdParams.Add(parGLDataID);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (entity.IsActive.HasValue)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parLocalCurrencyCode = cmd.CreateParameter();
            parLocalCurrencyCode.ParameterName = "@LocalCurrencyCode";
            if (!string.IsNullOrEmpty(entity.LocalCurrencyCode))
                parLocalCurrencyCode.Value = entity.LocalCurrencyCode;
            else
                parLocalCurrencyCode.Value = System.DBNull.Value;
            cmdParams.Add(parLocalCurrencyCode);

            System.Data.IDbDataParameter parOpenDate = cmd.CreateParameter();
            parOpenDate.ParameterName = "@OpenDate";
            if (entity.OpenDate.HasValue)
                parOpenDate.Value = entity.OpenDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parOpenDate.Value = System.DBNull.Value;
            cmdParams.Add(parOpenDate);

            System.Data.IDbDataParameter parReconciliationTypeID = cmd.CreateParameter();
            parReconciliationTypeID.ParameterName = "@ReconciliationCategoryTypeID";
            if (entity.ReconciliationCategoryTypeID.HasValue)
                parReconciliationTypeID.Value = entity.ReconciliationCategoryTypeID;
            else
                parReconciliationTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationTypeID);

            System.Data.IDbDataParameter parRecPeriodAmountBaseCurrency = cmd.CreateParameter();
            parRecPeriodAmountBaseCurrency.ParameterName = "@RecPeriodAmountBaseCurrency";
            if (entity.RecPeriodAmountBaseCurrency.HasValue)
                parRecPeriodAmountBaseCurrency.Value = entity.RecPeriodAmountBaseCurrency;
            else
                parRecPeriodAmountBaseCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parRecPeriodAmountBaseCurrency);

            System.Data.IDbDataParameter parRecPeriodAmountLocalCurrency = cmd.CreateParameter();
            parRecPeriodAmountLocalCurrency.ParameterName = "@RecPeriodAmountLocalCurrency";
            if (entity.RecPeriodAmountLocalCurrency.HasValue)
                parRecPeriodAmountLocalCurrency.Value = entity.RecPeriodAmountLocalCurrency;
            else
                parRecPeriodAmountLocalCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parRecPeriodAmountLocalCurrency);

            System.Data.IDbDataParameter parRecPeriodAmountReportingCurrency = cmd.CreateParameter();
            parRecPeriodAmountReportingCurrency.ParameterName = "@RecPeriodAmountReportingCurrency";
            if (entity.RecPeriodAmountReportingCurrency.HasValue)
                parRecPeriodAmountReportingCurrency.Value = entity.RecPeriodAmountReportingCurrency;
            else
                parRecPeriodAmountReportingCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parRecPeriodAmountReportingCurrency);

            System.Data.IDbDataParameter parBalanceBaseCurrency = cmd.CreateParameter();
            parBalanceBaseCurrency.ParameterName = "@BalanceBaseCurrency";
            if (entity.BalanceBaseCurrency.HasValue)
                parBalanceBaseCurrency.Value = entity.BalanceBaseCurrency;
            else
                parBalanceBaseCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parBalanceBaseCurrency);

            System.Data.IDbDataParameter parBalanceLocalCurrency = cmd.CreateParameter();
            parBalanceLocalCurrency.ParameterName = "@BalanceLocalCurrency";
            if (entity.BalanceLocalCurrency.HasValue)
                parBalanceLocalCurrency.Value = entity.BalanceLocalCurrency;
            else
                parBalanceLocalCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parBalanceLocalCurrency);

            System.Data.IDbDataParameter parBalanceReportingCurrency = cmd.CreateParameter();
            parBalanceReportingCurrency.ParameterName = "@BalanceReportingCurrency";
            if (entity.BalanceReportingCurrency.HasValue)
                parBalanceReportingCurrency.Value = entity.BalanceReportingCurrency;
            else
                parBalanceReportingCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parBalanceReportingCurrency);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!string.IsNullOrEmpty(entity.RevisedBy))
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parScheduleAmount = cmd.CreateParameter();
            parScheduleAmount.ParameterName = "@ScheduleAmount";
            if (entity.ScheduleAmount.HasValue)
                parScheduleAmount.Value = entity.ScheduleAmount;
            else
                parScheduleAmount.Value = System.DBNull.Value;
            cmdParams.Add(parScheduleAmount);

            System.Data.IDbDataParameter parScheduleAmountBaseCurrency = cmd.CreateParameter();
            parScheduleAmountBaseCurrency.ParameterName = "@ScheduleAmountBaseCurrency";
            if (entity.ScheduleAmountBaseCurrency.HasValue)
                parScheduleAmountBaseCurrency.Value = entity.ScheduleAmountBaseCurrency;
            else
                parScheduleAmountBaseCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parScheduleAmountBaseCurrency);


            System.Data.IDbDataParameter parScheduleAmountReportingCurrency = cmd.CreateParameter();
            parScheduleAmountReportingCurrency.ParameterName = "@ScheduleAmountReportingCurrency";
            if (entity.ScheduleAmountReportingCurrency.HasValue)
                parScheduleAmountReportingCurrency.Value = entity.ScheduleAmountReportingCurrency;
            else
                parScheduleAmountReportingCurrency.Value = System.DBNull.Value;
            cmdParams.Add(parScheduleAmountReportingCurrency);


            System.Data.IDbDataParameter parScheduleBeginDate = cmd.CreateParameter();
            parScheduleBeginDate.ParameterName = "@ScheduleBeginDate";
            if (entity.ScheduleBeginDate.HasValue)
                parScheduleBeginDate.Value = entity.ScheduleBeginDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parScheduleBeginDate.Value = System.DBNull.Value;
            cmdParams.Add(parScheduleBeginDate);

            System.Data.IDbDataParameter parScheduleEndDate = cmd.CreateParameter();
            parScheduleEndDate.ParameterName = "@ScheduleEndDate";
            if (entity.ScheduleEndDate.HasValue)
                parScheduleEndDate.Value = entity.ScheduleEndDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parScheduleEndDate.Value = System.DBNull.Value;
            cmdParams.Add(parScheduleEndDate);

            System.Data.IDbDataParameter parScheduleName = cmd.CreateParameter();
            parScheduleName.ParameterName = "@ScheduleName";
            if (!string.IsNullOrEmpty(entity.ScheduleName))
                parScheduleName.Value = entity.ScheduleName;
            else
                parScheduleName.Value = System.DBNull.Value;
            cmdParams.Add(parScheduleName);

            System.Data.IDbDataParameter parCalculationFrequencyID = cmd.CreateParameter();
            parCalculationFrequencyID.ParameterName = "@CalculationFrequencyID";
            if (entity.CalculationFrequencyID.HasValue)
                parCalculationFrequencyID.Value = entity.CalculationFrequencyID.Value;
            else
                parCalculationFrequencyID.Value = DBNull.Value;
            cmdParams.Add(parCalculationFrequencyID);


            System.Data.IDbDataParameter parTotalIntervals = cmd.CreateParameter();
            parTotalIntervals.ParameterName = "@TotalIntervals";
            if (entity.TotalIntervals.HasValue)
                parTotalIntervals.Value = entity.TotalIntervals.Value;
            else
                parTotalIntervals.Value = DBNull.Value;
            cmdParams.Add(parTotalIntervals);


            System.Data.IDbDataParameter parCurrentInterval = cmd.CreateParameter();
            parCurrentInterval.ParameterName = "@CurrentInterval";
            if (entity.CurrentInterval.HasValue)
                parCurrentInterval.Value = entity.CurrentInterval.Value;
            else
                parCurrentInterval.Value = DBNull.Value;
            cmdParams.Add(parCurrentInterval);

            System.Data.IDbDataParameter parStartIntervalRecPeriodID = cmd.CreateParameter();
            parStartIntervalRecPeriodID.ParameterName = "@StartIntervalRecPeriodID";
            if (entity.StartIntervalRecPeriodID.HasValue)
                parStartIntervalRecPeriodID.Value = entity.StartIntervalRecPeriodID.Value;
            else
                parStartIntervalRecPeriodID.Value = DBNull.Value;
            cmdParams.Add(parStartIntervalRecPeriodID);

            System.Data.IDbDataParameter pkparGLDataRecurringItemScheduleID = cmd.CreateParameter();
            pkparGLDataRecurringItemScheduleID.ParameterName = "@GLDataRecurringItemScheduleID";
            pkparGLDataRecurringItemScheduleID.Value = entity.GLDataRecurringItemScheduleID;
            cmdParams.Add(pkparGLDataRecurringItemScheduleID);


            System.Data.IDbDataParameter parGLDataRecurringItemScheduleIntervalDetailList = cmd.CreateParameter();
            parGLDataRecurringItemScheduleIntervalDetailList.ParameterName = "@dtGLDataRecurringItemScheduleIntervalDetailList";
            parGLDataRecurringItemScheduleIntervalDetailList.Value = oGLDataRecurringItemScheduleIntervalDetailDataTable;
            cmdParams.Add(parGLDataRecurringItemScheduleIntervalDetailList);

            System.Data.IDbDataParameter parExRateLCCYtoBCCY = cmd.CreateParameter();
            parExRateLCCYtoBCCY.ParameterName = "@ExRateLCCYtoBCCY";
            if (entity.ExRateLCCYtoBCCY.HasValue)
                parExRateLCCYtoBCCY.Value = entity.ExRateLCCYtoBCCY;
            else
                parExRateLCCYtoBCCY.Value = System.DBNull.Value;
            cmd.Parameters.Add(parExRateLCCYtoBCCY);

            System.Data.IDbDataParameter parExRateLCCYtoRCCY = cmd.CreateParameter();
            parExRateLCCYtoRCCY.ParameterName = "@ExRateLCCYtoRCCY";
            if (entity.ExRateLCCYtoRCCY.HasValue)
                parExRateLCCYtoRCCY.Value = entity.ExRateLCCYtoRCCY;
            else
                parExRateLCCYtoRCCY.Value = System.DBNull.Value;
            cmd.Parameters.Add(parExRateLCCYtoRCCY);

            System.Data.IDbDataParameter parIgnoreInCalculation = cmd.CreateParameter();
            parIgnoreInCalculation.ParameterName = "@IgnoreInCalculation";
            if (entity.IgnoreInCalculation.HasValue)
                parIgnoreInCalculation.Value = entity.IgnoreInCalculation;
            else
                parIgnoreInCalculation.Value = System.DBNull.Value;
            cmd.Parameters.Add(parIgnoreInCalculation);

            return cmd;

        }


        #region UpdateGLDataRecurringItemScheduleCloseDate

        public void UpdateGLDataRecurringItemScheduleCloseDate(long glDataID, DataTable dtGLDataRecurringItemScheduleInputIDs, DateTime? closeDate, string closeComments, string journalEntryRef, string comments, short recCategoryTypeID, short recTemplateAttributeID, string revisedBy, DateTime? dateRevised, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            IDbCommand cmd = this.CreateUpdateGLDataRecurringItemScheduleCloseDateCommand(glDataID, dtGLDataRecurringItemScheduleInputIDs, closeDate, closeComments, journalEntryRef, comments, recCategoryTypeID, recTemplateAttributeID, revisedBy, dateRevised);
            cmd.Connection = oConnection;
            cmd.Transaction = oTransaction;

            cmd.ExecuteNonQuery();
        }

        private IDbCommand CreateUpdateGLDataRecurringItemScheduleCloseDateCommand(long glDataID, DataTable dtGLDataRecurringItemScheduleInputIDs, DateTime? closeDate, string closeComments, string journalEntryRef, string comments, short recCategoryTypeID, short recTemplateAttributeID, string revisedBy, DateTime? dateRevised)
        {
            IDbCommand cmd = this.CreateCommand("usp_UPD_GLDataRecurringItemScheduleCloseDate");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection paramCollection = cmd.Parameters;

            IDbDataParameter paramGLDataID = cmd.CreateParameter();
            paramGLDataID.ParameterName = "@GLDataID";
            paramGLDataID.Value = glDataID;
            paramCollection.Add(paramGLDataID);

            IDbDataParameter paramGLDataRecurringItemScheduleInputIDTable = cmd.CreateParameter();
            paramGLDataRecurringItemScheduleInputIDTable.ParameterName = "@GLRecItemInputIDTable";
            paramGLDataRecurringItemScheduleInputIDTable.Value = dtGLDataRecurringItemScheduleInputIDs;
            paramCollection.Add(paramGLDataRecurringItemScheduleInputIDTable);

            IDbDataParameter paramCloseDate = cmd.CreateParameter();
            paramCloseDate.ParameterName = "@CloseDate";
            if (closeDate != null)
            {
                paramCloseDate.Value = closeDate;
            }
            else
            {
                paramCloseDate.Value = DBNull.Value;
            }
            paramCollection.Add(paramCloseDate);

            IDbDataParameter paramCloseComments = cmd.CreateParameter();
            paramCloseComments.ParameterName = "@CloseComments";
            if (!string.IsNullOrEmpty(closeComments))
            {
                paramCloseComments.Value = closeComments;
            }
            else
            {
                paramCloseComments.Value = DBNull.Value;
            }
            paramCollection.Add(paramCloseComments);

            IDbDataParameter paramAccountJournalEntryRef = cmd.CreateParameter();
            paramAccountJournalEntryRef.ParameterName = "@JournalEntryRef";
            if (journalEntryRef != null)
            {
                paramAccountJournalEntryRef.Value = journalEntryRef;
            }
            else
            {
                paramAccountJournalEntryRef.Value = DBNull.Value;
            }
            paramCollection.Add(paramAccountJournalEntryRef);

            IDbDataParameter paramComments = cmd.CreateParameter();
            paramComments.ParameterName = "@Comments";
            if (!string.IsNullOrEmpty(comments))
            {
                paramComments.Value = comments;
            }
            else
            {
                paramComments.Value = DBNull.Value;
            }
            paramCollection.Add(paramComments);

            IDbDataParameter paramRecCategoryTypeID = cmd.CreateParameter();
            paramRecCategoryTypeID.ParameterName = "@RecCategoryTypeID";
            paramRecCategoryTypeID.Value = recCategoryTypeID;
            paramCollection.Add(paramRecCategoryTypeID);

            IDbDataParameter paramRecTemplateAttributeID = cmd.CreateParameter();
            paramRecTemplateAttributeID.ParameterName = "@RecTemplateAttributeID";
            paramRecTemplateAttributeID.Value = recTemplateAttributeID;
            paramCollection.Add(paramRecTemplateAttributeID);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            parRevisedBy.Value = revisedBy;
            paramCollection.Add(parRevisedBy);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (dateRevised.HasValue)
                parDateRevised.Value = dateRevised.Value;
            else
                parDateRevised.Value = DBNull.Value;
            paramCollection.Add(parDateRevised);

            return cmd;
        }

        #endregion


        #region DeleteGLDataRecurringItemSchedule

        public void DeleteGLDataRecurringItemSchedule(GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo, short recTemplateAttributeID, DataTable dtGLdataRecurringScheduleIDs, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            IDbCommand cmd = this.CreateDeleteGLDataRecurringItemScheduleCommand(oGLDataRecurringItemScheduleInfo, recTemplateAttributeID, dtGLdataRecurringScheduleIDs);
            cmd.Connection = oConnection;
            cmd.Transaction = oTransaction;

            cmd.ExecuteNonQuery();
        }

        private IDbCommand CreateDeleteGLDataRecurringItemScheduleCommand(GLDataRecurringItemScheduleInfo entity, short recTemplateAttributeID, DataTable dtGLdataRecurringScheduleIDs)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_DEL_GLDataRecurringItemSchedule");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            if (!entity.IsGLDataIDNull)
                parGLDataID.Value = entity.GLDataID;
            else
                parGLDataID.Value = System.DBNull.Value;
            cmdParams.Add(parGLDataID);

            System.Data.IDbDataParameter parGLRecItemInputID = cmd.CreateParameter();
            parGLRecItemInputID.ParameterName = "@tblGLRecItemIDs";
            parGLRecItemInputID.Value = dtGLdataRecurringScheduleIDs;
            cmdParams.Add(parGLRecItemInputID);

            System.Data.IDbDataParameter parReconciliationCategoryTypeID = cmd.CreateParameter();
            parReconciliationCategoryTypeID.ParameterName = "@ReconciliationCategoryTypeID";
            if (!entity.IsReconciliationCategoryTypeIDNull)
                parReconciliationCategoryTypeID.Value = entity.ReconciliationCategoryTypeID;
            else
                parReconciliationCategoryTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationCategoryTypeID);

            IDbDataParameter paramRecTemplateAttributeID = cmd.CreateParameter();
            paramRecTemplateAttributeID.ParameterName = "@RecTemplateAttributeID";
            paramRecTemplateAttributeID.Value = recTemplateAttributeID;
            cmdParams.Add(paramRecTemplateAttributeID);

            IDbDataParameter paramRevisedBy = cmd.CreateParameter();
            paramRevisedBy.ParameterName = "@RevisedBy";
            paramRevisedBy.Value = entity.RevisedBy;
            cmdParams.Add(paramRevisedBy);

            IDbDataParameter paramDateRevised = cmd.CreateParameter();
            paramDateRevised.ParameterName = "@DateRevised";
            paramDateRevised.Value = entity.DateRevised;
            cmdParams.Add(paramDateRevised);

            return cmd;
        }

        #endregion

        public void InsertRecInputItem(GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo, short recTemplateAttributeID, IDbConnection con, IDbTransaction oTransaction)
        {
            IDbCommand cmd;

            cmd = this.CreateInsertCommand(oGLDataRecurringItemScheduleInfo);
            cmd.Connection = con;
            cmd.Transaction = oTransaction;
            cmd.ExecuteNonQuery();
        }


        #region GetGLDataRecurringItemScheduleInfo
        internal GLDataRecurringItemScheduleInfo GetGLDataRecurringItemScheduleInfo(long? GLDataRecurringItemScheduleID)
        {
            GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo = new GLDataRecurringItemScheduleInfo();
            IDbCommand cmd = null;
            IDbConnection con = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateGetGLDataRecurringItemScheduleInfoCommand(GLDataRecurringItemScheduleID);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                oGLDataRecurringItemScheduleInfo = null;
                reader.Read();
                oGLDataRecurringItemScheduleInfo = this.MapObject(reader);
                oGLDataRecurringItemScheduleInfo.AddedByFirstName = reader.GetStringValue("AddedByFirstName");
                oGLDataRecurringItemScheduleInfo.AddedByLastName = reader.GetStringValue("AddedByLastName");
                oGLDataRecurringItemScheduleInfo.RecItemNumber = reader.GetStringValue("RecItemNumber");
                oGLDataRecurringItemScheduleInfo.CalculationFrequencyID = reader.GetInt16Value("CalculationFrequencyID");
                oGLDataRecurringItemScheduleInfo.TotalIntervals = reader.GetInt32Value("TotalIntervals");
                oGLDataRecurringItemScheduleInfo.CurrentInterval = reader.GetInt32Value("CurrentInterval");
                oGLDataRecurringItemScheduleInfo.StartIntervalRecPeriodID = reader.GetInt32Value("StartIntervalRecPeriodID");
                oGLDataRecurringItemScheduleInfo.MatchSetRefNumber = reader.GetStringValue("MatchSetRef");
                oGLDataRecurringItemScheduleInfo.MatchSetID = reader.GetInt64Value("MatchSetID");
                oGLDataRecurringItemScheduleInfo.MatchSetSubSetCombinationID = reader.GetInt64Value("MatchSetSubSetCombinationID");
                oGLDataRecurringItemScheduleInfo.ExRateLCCYtoBCCY = reader.GetDecimalValue("ExRateLCCYtoBCCY");
                oGLDataRecurringItemScheduleInfo.ExRateLCCYtoRCCY = reader.GetDecimalValue("ExRateLCCYtoRCCY");
                oGLDataRecurringItemScheduleInfo.IgnoreInCalculation = reader.GetBooleanValue("IgnoreInCalculation");
                reader.ClearColumnHash();
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed)
                    con.Dispose();
            }

            return oGLDataRecurringItemScheduleInfo;
        }


        private System.Data.IDbCommand CreateGetGLDataRecurringItemScheduleInfoCommand(long? GLDataRecurringItemScheduleID)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("usp_GET_GLDataRecurringItemSchedule");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataRecurringItemScheduleID";
            par.Value = GLDataRecurringItemScheduleID.Value;
            cmdParams.Add(par);
            return cmd;
        }

        public List<GLDataRecurringItemScheduleInfo> InsertGLDataRecurringItemScheduleInfoCollection(List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollectionReturn = new List<GLDataRecurringItemScheduleInfo>();
            IDbCommand cmd = null;
            IDataReader dr = null;
            try
            {

                cmd = this.CreateInsertGLDataRecurringItemScheduleInfoCollectionCommand(oGLDataRecurringItemScheduleInfoCollection);
                cmd.Connection = oConnection;
                cmd.Transaction = oTransaction;
                dr = cmd.ExecuteReader();
                GLDataRecurringItemScheduleInfo objGLDataRecurringItemScheduleInfo;
                while (dr.Read())
                {
                    objGLDataRecurringItemScheduleInfo = new GLDataRecurringItemScheduleInfo();
                    objGLDataRecurringItemScheduleInfo.IndexID = dr.GetInt16Value("IndexID");
                    objGLDataRecurringItemScheduleInfo.RecItemNumber = dr.GetStringValue("RecItemNumber");
                    oGLDataRecurringItemScheduleInfoCollectionReturn.Add(objGLDataRecurringItemScheduleInfo);
                }
            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                }
            }

            return oGLDataRecurringItemScheduleInfoCollectionReturn;

        }

        protected IDbCommand CreateInsertGLDataRecurringItemScheduleInfoCollectionCommand(List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection)
        {

            DataTable oGLDataRecurringItemScheduleDataTable = ServiceHelper.ConvertGLDataRecurringItemScheduleToDataTable(oGLDataRecurringItemScheduleInfoCollection);

            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_GLDataRecurringItemScheduleInfoCollection");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (oGLDataRecurringItemScheduleInfoCollection != null && oGLDataRecurringItemScheduleInfoCollection.Count > 0)
            {
                if (oGLDataRecurringItemScheduleInfoCollection[0].AddedBy != null)
                {
                    parAddedBy.Value = oGLDataRecurringItemScheduleInfoCollection[0].AddedBy;
                }
                else
                {
                    parAddedBy.Value = System.DBNull.Value;
                }
            }
            else
            {
                parAddedBy.Value = System.DBNull.Value;
            }
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parAddedByUserID = cmd.CreateParameter();
            parAddedByUserID.ParameterName = "@AddedByUserID";
            if (oGLDataRecurringItemScheduleInfoCollection != null && oGLDataRecurringItemScheduleInfoCollection.Count > 0)
            {
                if (oGLDataRecurringItemScheduleInfoCollection[0].AddedByUserID != null)
                {
                    parAddedByUserID.Value = oGLDataRecurringItemScheduleInfoCollection[0].AddedByUserID;
                }
                else
                {
                    parAddedByUserID.Value = System.DBNull.Value;
                }
            }
            else
            {
                parAddedByUserID.Value = System.DBNull.Value;
            }
            cmdParams.Add(parAddedByUserID);


            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (oGLDataRecurringItemScheduleInfoCollection != null && oGLDataRecurringItemScheduleInfoCollection.Count > 0)
            {
                if (oGLDataRecurringItemScheduleInfoCollection[0].DateAdded != null)
                {
                    parDateAdded.Value = oGLDataRecurringItemScheduleInfoCollection[0].DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate(); ;
                }
                else
                {
                    parDateAdded.Value = System.DBNull.Value;
                }
            }
            else
            {
                parDateAdded.Value = System.DBNull.Value;
            }
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            if (oGLDataRecurringItemScheduleInfoCollection != null && oGLDataRecurringItemScheduleInfoCollection.Count > 0)
            {
                parGLDataID.Value = oGLDataRecurringItemScheduleInfoCollection[0].GLDataID;
            }
            else
            {
                parGLDataID.Value = System.DBNull.Value;
            }
            cmdParams.Add(parGLDataID);

            System.Data.IDbDataParameter parRecCategoryTypeID = cmd.CreateParameter();
            parRecCategoryTypeID.ParameterName = "@RecCategoryTypeID";
            if (oGLDataRecurringItemScheduleInfoCollection != null && oGLDataRecurringItemScheduleInfoCollection.Count > 0)
            {
                parRecCategoryTypeID.Value = oGLDataRecurringItemScheduleInfoCollection[0].RecCategoryTypeID;
            }
            else
            {
                parRecCategoryTypeID.Value = System.DBNull.Value;
            }
            cmdParams.Add(parRecCategoryTypeID);

            System.Data.IDbDataParameter parCalculationFrequencyID = cmd.CreateParameter();
            parCalculationFrequencyID.ParameterName = "@CalculationFrequencyID";
            if (oGLDataRecurringItemScheduleInfoCollection != null && oGLDataRecurringItemScheduleInfoCollection.Count > 0)
            {
                if (oGLDataRecurringItemScheduleInfoCollection[0].CalculationFrequencyID != null)
                {
                    parCalculationFrequencyID.Value = oGLDataRecurringItemScheduleInfoCollection[0].CalculationFrequencyID;
                }
                else
                {
                    parCalculationFrequencyID.Value = System.DBNull.Value;
                }
            }
            else
            {
                parCalculationFrequencyID.Value = System.DBNull.Value;
            }
            cmdParams.Add(parCalculationFrequencyID);

            System.Data.IDbDataParameter parTotalIntervals = cmd.CreateParameter();
            parTotalIntervals.ParameterName = "@TotalIntervals";
            if (oGLDataRecurringItemScheduleInfoCollection != null && oGLDataRecurringItemScheduleInfoCollection.Count > 0)
            {
                if (oGLDataRecurringItemScheduleInfoCollection[0].TotalIntervals != null)
                {
                    parTotalIntervals.Value = oGLDataRecurringItemScheduleInfoCollection[0].TotalIntervals;
                }
                else
                {
                    parTotalIntervals.Value = System.DBNull.Value;
                }
            }
            else
            {
                parTotalIntervals.Value = System.DBNull.Value;
            }
            cmdParams.Add(parTotalIntervals);

            System.Data.IDbDataParameter parCurrentInterval = cmd.CreateParameter();
            parCurrentInterval.ParameterName = "@CurrentInterval";
            if (oGLDataRecurringItemScheduleInfoCollection != null && oGLDataRecurringItemScheduleInfoCollection.Count > 0)
            {
                if (oGLDataRecurringItemScheduleInfoCollection[0].CurrentInterval != null)
                {
                    parCurrentInterval.Value = oGLDataRecurringItemScheduleInfoCollection[0].CurrentInterval;
                }
                else
                {
                    parCurrentInterval.Value = System.DBNull.Value;
                }
            }
            else
            {
                parCurrentInterval.Value = System.DBNull.Value;
            }
            cmdParams.Add(parCurrentInterval);

            System.Data.IDbDataParameter parRecordSourceTypeID = cmd.CreateParameter();
            parRecordSourceTypeID.ParameterName = "@RecordSourceTypeID";
            parRecordSourceTypeID.Value = oGLDataRecurringItemScheduleInfoCollection[0].RecordSourceTypeID;
            cmdParams.Add(parRecordSourceTypeID);

            System.Data.IDbDataParameter parRecordSourceID = cmd.CreateParameter();
            parRecordSourceID.ParameterName = "@RecordSourceID";
            parRecordSourceID.Value = oGLDataRecurringItemScheduleInfoCollection[0].RecordSourceID;
            cmdParams.Add(parRecordSourceID);

            System.Data.IDbDataParameter parGLDataRecurringItemScheduleList = cmd.CreateParameter();
            parGLDataRecurringItemScheduleList.ParameterName = "@dtGLDataRecurringItemScheduleList";
            parGLDataRecurringItemScheduleList.Value = oGLDataRecurringItemScheduleDataTable;
            cmdParams.Add(parGLDataRecurringItemScheduleList);


            return cmd;
        }


        public List<GLDataRecurringItemScheduleInfo> GetGLDataRecItemsListByMatchSetSubSetCombinationID(long? MatchSetSubSetCombinationID)
        {
            List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection = new List<GLDataRecurringItemScheduleInfo>();
            IDbCommand cmd = null;
            IDbConnection con = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.GetGLDataRecItemsListCommand(MatchSetSubSetCombinationID);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo = null;
                while (reader.Read())
                {
                    oGLDataRecurringItemScheduleInfo = MapObjectRecItemsListByMatchSetSubSetCombinationID(reader);
                    oGLDataRecurringItemScheduleInfoCollection.Add(oGLDataRecurringItemScheduleInfo);
                }
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed)
                    con.Dispose();
            }
            return oGLDataRecurringItemScheduleInfoCollection;
        }
        private IDbCommand GetGLDataRecItemsListCommand(long? MatchSetSubSetCombinationID)
        {
            IDbCommand oCommand = this.CreateCommand("usp_SEL_GLDataRecItemsListByRecordSourceID");
            oCommand.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection paramCollection = oCommand.Parameters;
            IDbDataParameter paramMatchSetSubSetCombinationID = oCommand.CreateParameter();
            paramMatchSetSubSetCombinationID.ParameterName = "@MatchSetSubSetCombinationID";
            if (MatchSetSubSetCombinationID.HasValue)
                paramMatchSetSubSetCombinationID.Value = MatchSetSubSetCombinationID.Value;
            else
                paramMatchSetSubSetCombinationID.Value = 0;
            paramCollection.Add(paramMatchSetSubSetCombinationID);

            return oCommand;
        }

        private GLDataRecurringItemScheduleInfo MapObjectRecItemsListByMatchSetSubSetCombinationID(IDataReader r)
        {

            GLDataRecurringItemScheduleInfo entity = new GLDataRecurringItemScheduleInfo();

            entity.ScheduleName = r.GetStringValue("ScheduleName");
            entity.ScheduleBeginDate = r.GetDateValue("ScheduleBeginDate");
            entity.ScheduleEndDate = r.GetDateValue("ScheduleEndDate");
            entity.ScheduleAmount = r.GetDecimalValue("ScheduleAmount");
            entity.LocalCurrencyCode = r.GetStringValue("LocalCurrencyCode");
            entity.OpenDate = r.GetDateValue("OpenDate");
            entity.Comments = r.GetStringValue("Comments");
            entity.RecItemNumber = r.GetStringValue("RecItemNumber");
            entity.WriteOnOffID = r.GetInt16Value("WriteOnOffID");


            return entity;
        }


        #endregion

        public List<GLDataRecurringItemScheduleInfo> GetClosedGLDataRecurringItemSchedule(long? GlDataID, long? MatchsetSubSetCombinationID, long? ExCelRowNumber, long? MatchSetMatchingSourceDataImportID, short? GLDataRecItemTypeID)
        {
            List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection = new List<GLDataRecurringItemScheduleInfo>();
            IDbCommand cmd = null;
            IDbConnection con = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateGeGetClosedGLDataRecurringItemScheduleCommand(GlDataID, MatchsetSubSetCombinationID, ExCelRowNumber, MatchSetMatchingSourceDataImportID, GLDataRecItemTypeID);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo = null;
                while (reader.Read())
                {
                    oGLDataRecurringItemScheduleInfo = this.MapObject(reader);
                    oGLDataRecurringItemScheduleInfo.AddedByFirstName = reader.GetStringValue("AddedByFirstName");
                    oGLDataRecurringItemScheduleInfo.AddedByLastName = reader.GetStringValue("AddedByLastName");
                    oGLDataRecurringItemScheduleInfo.RecItemNumber = reader.GetStringValue("RecItemNumber");
                    oGLDataRecurringItemScheduleInfo.CalculationFrequencyID = reader.GetInt16Value("CalculationFrequencyID");
                    oGLDataRecurringItemScheduleInfo.TotalIntervals = reader.GetInt32Value("TotalIntervals");
                    oGLDataRecurringItemScheduleInfo.CurrentInterval = reader.GetInt32Value("CurrentInterval");
                    oGLDataRecurringItemScheduleInfo.StartIntervalRecPeriodID = reader.GetInt32Value("StartIntervalRecPeriodID");
                    oGLDataRecurringItemScheduleInfo.MatchSetRefNumber = reader.GetStringValue("MatchSetRef");
                    oGLDataRecurringItemScheduleInfo.MatchSetID = reader.GetInt64Value("MatchSetID");
                    oGLDataRecurringItemScheduleInfo.MatchSetSubSetCombinationID = reader.GetInt64Value("MatchSetSubSetCombinationID");
                    oGLDataRecurringItemScheduleInfo.ExRateLCCYtoBCCY = reader.GetDecimalValue("ExRateLCCYtoBCCY");
                    oGLDataRecurringItemScheduleInfo.ExRateLCCYtoRCCY = reader.GetDecimalValue("ExRateLCCYtoRCCY");
                    oGLDataRecurringItemScheduleInfoCollection.Add(oGLDataRecurringItemScheduleInfo);

                }
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed)
                    con.Dispose();
            }

            return oGLDataRecurringItemScheduleInfoCollection;
        }

        private IDbCommand CreateGeGetClosedGLDataRecurringItemScheduleCommand(long? glDataID, long? MatchsetSubSetCombinationID, long? ExCelRowNumber, long? MatchSetMatchingSourceDataImportID, short? GLDataRecItemTypeID)
        {
            IDbCommand cmd = this.CreateCommand("[Matching].[usp_SEL_ClosedGLDataRecurringItemScheduleByMatching]");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection paramCollection = cmd.Parameters;

            IDbDataParameter paramAccountID = cmd.CreateParameter();
            paramAccountID.ParameterName = "@GLDataID";
            paramAccountID.Value = glDataID;
            paramCollection.Add(paramAccountID);

            IDbDataParameter paramMatchsetSubSetCombinationID = cmd.CreateParameter();
            paramMatchsetSubSetCombinationID.ParameterName = "@MatchsetSubSetCombinationID";
            paramMatchsetSubSetCombinationID.Value = MatchsetSubSetCombinationID;
            paramCollection.Add(paramMatchsetSubSetCombinationID);

            IDbDataParameter paramExCelRowNumber = cmd.CreateParameter();
            paramExCelRowNumber.ParameterName = "@ExCelRowNumber";
            paramExCelRowNumber.Value = ExCelRowNumber;
            paramCollection.Add(paramExCelRowNumber);

            IDbDataParameter paramMatchSetMatchingSourceDataImportID = cmd.CreateParameter();
            paramMatchSetMatchingSourceDataImportID.ParameterName = "@MatchSetMatchingSourceDataImportID";
            paramMatchSetMatchingSourceDataImportID.Value = MatchSetMatchingSourceDataImportID;
            paramCollection.Add(paramMatchSetMatchingSourceDataImportID);

            IDbDataParameter paramGLDataRecItemTypeID = cmd.CreateParameter();
            paramGLDataRecItemTypeID.ParameterName = "@GLDataRecItemTypeID";
            paramGLDataRecItemTypeID.Value = GLDataRecItemTypeID;
            paramCollection.Add(paramGLDataRecItemTypeID);

            return cmd;
        }

        public List<GLDataRecurringItemScheduleInfo> CloseMatchingRecurringScheduleItems(DataTable dtMatchSetGLDataRecItem, GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo, short recTemplateAttributeID, DateTime? CloseDate, out int? rowsAffected, IDbConnection con, IDbTransaction oTransaction)
        {
            List<GLDataRecurringItemScheduleInfo> oTempGLDataRecurringItemScheduleInfoCollection = new List<GLDataRecurringItemScheduleInfo>();
            IDbCommand cmd = null;
            IDataReader dr = null;
            try
            {

                cmd = GetCloseMatchingRecurringScheduleItemsCommand(dtMatchSetGLDataRecItem, oGLDataRecurringItemScheduleInfo, recTemplateAttributeID, CloseDate);
                cmd.Connection = con;
                cmd.Transaction = oTransaction;
                dr = cmd.ExecuteReader();
                GLDataRecurringItemScheduleInfo objGLDataRecurringItemScheduleInfo;
                while (dr.Read())
                {
                    objGLDataRecurringItemScheduleInfo = new GLDataRecurringItemScheduleInfo();
                    objGLDataRecurringItemScheduleInfo.GLDataRecurringItemScheduleID = dr.GetInt64Value("GLDataRecItemID");
                    objGLDataRecurringItemScheduleInfo.CloseDate = dr.GetDateValue("CloseDate");
                    oTempGLDataRecurringItemScheduleInfoCollection.Add(objGLDataRecurringItemScheduleInfo);
                }
                rowsAffected = oTempGLDataRecurringItemScheduleInfoCollection.Count;
            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                }
            }

            return oTempGLDataRecurringItemScheduleInfoCollection;
        }

        private System.Data.IDbCommand GetCloseMatchingRecurringScheduleItemsCommand(DataTable dtMatchSetGLDataRecItem, GLDataRecurringItemScheduleInfo entity, short recTemplateAttributeID, DateTime? CloseDate)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("Matching.usp_UPD_CloseGLDataRecItemByMatching");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdParamdtMatchSetGLDataRecItem = cmd.CreateParameter();
            cmdParamdtMatchSetGLDataRecItem.ParameterName = "@MatchSetGLDataRecItem";
            cmdParamdtMatchSetGLDataRecItem.Value = dtMatchSetGLDataRecItem;
            cmdParams.Add(cmdParamdtMatchSetGLDataRecItem);

            IDbDataParameter paramCloseDate = cmd.CreateParameter();
            paramCloseDate.ParameterName = "@CloseDate";
            if (CloseDate.HasValue)
            {
                paramCloseDate.Value = CloseDate;
            }
            else
            {
                paramCloseDate.Value = DBNull.Value;
            }
            cmdParams.Add(paramCloseDate);

            System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            if (!entity.IsGLDataIDNull)
                parGLDataID.Value = entity.GLDataID;
            else
                parGLDataID.Value = System.DBNull.Value;
            cmdParams.Add(parGLDataID);


            System.Data.IDbDataParameter parReconciliationCategoryID = cmd.CreateParameter();
            parReconciliationCategoryID.ParameterName = "@ReconciliationCategoryID";
            if (entity.RecCategoryTypeID.HasValue)
                parReconciliationCategoryID.Value = entity.RecCategoryTypeID;
            else
                parReconciliationCategoryID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationCategoryID);

            System.Data.IDbDataParameter parReconciliationCategoryTypeID = cmd.CreateParameter();
            parReconciliationCategoryTypeID.ParameterName = "@ReconciliationCategoryTypeID";
            if (entity.ReconciliationCategoryTypeID.HasValue)
                parReconciliationCategoryTypeID.Value = entity.ReconciliationCategoryTypeID;
            else
                parReconciliationCategoryTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationCategoryTypeID);

            IDbDataParameter paramRecTemplateAttributeID = cmd.CreateParameter();
            paramRecTemplateAttributeID.ParameterName = "@RecTemplateAttributeID";
            paramRecTemplateAttributeID.Value = recTemplateAttributeID;
            cmd.Parameters.Add(paramRecTemplateAttributeID);

            IDbDataParameter paramGLDataRecItemTypeID = cmd.CreateParameter();
            paramGLDataRecItemTypeID.ParameterName = "@GLDataRecItemTypeID";
            if (entity.ReconciliationCategoryTypeID.HasValue)
                paramGLDataRecItemTypeID.Value = entity.GLDataRecItemTypeID;
            else
                paramGLDataRecItemTypeID.Value = System.DBNull.Value;
            cmd.Parameters.Add(paramGLDataRecItemTypeID);

            System.Data.IDbDataParameter ParmRevisedBy = cmd.CreateParameter();
            ParmRevisedBy.ParameterName = "@RevisedBy";
            if (entity.RevisedBy != null)
                ParmRevisedBy.Value = entity.RevisedBy;
            else
                ParmRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(ParmRevisedBy);

            System.Data.IDbDataParameter parmDateRevised = cmd.CreateParameter();
            parmDateRevised.ParameterName = "@DateRevised";
            if (entity.DateRevised.HasValue)
                parmDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parmDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parmDateRevised);
            return cmd;
        }

        public int? InsertGLDataRecItemScheduleBulk(Int64? GLDataID, Int32? RecPeriodID,
            List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoList,
            string addedBy, DateTime? dateAdded,
            IDbConnection con, IDbTransaction oTransaction)
        {
            int? rowsAffected = null;
            using (IDbCommand cmd = CreateInsertGLDataRecItemScheduleBulkCommand(GLDataID, RecPeriodID, oGLDataRecurringItemScheduleInfoList, addedBy, dateAdded))
            {
                cmd.Connection = con;
                cmd.Transaction = oTransaction;
                rowsAffected = cmd.ExecuteScalar() as int?;
            }
            return rowsAffected;
        }

        private IDbCommand CreateInsertGLDataRecItemScheduleBulkCommand(Int64? GLDataID, Int32? RecPeriodID,
            List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoList,
            string addedBy, DateTime? dateAdded
            )
        {
            IDbCommand cmd = CreateCommand("usp_INS_GLDataRecItemScheduleBulk");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            DataTable dtSchedules = ServiceHelper.ConvertGLDataRecurringItemScheduleToDataTable(oGLDataRecurringItemScheduleInfoList);
            DataTable dtScheduleIntervalDetails = ServiceHelper.ConvertGLDataRecurringItemScheduleIntervalDetailToDataTable(oGLDataRecurringItemScheduleInfoList);

            ServiceHelper.AddCommonParametersForGLDataIDAndRecPeriodID(GLDataID, RecPeriodID, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForDateAdded(dateAdded, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForAddedBy(addedBy, cmd, cmdParams);

            System.Data.IDbDataParameter parScheduleRecItem = cmd.CreateParameter();
            parScheduleRecItem.ParameterName = "@udtGLDataRecurringItemSchedule";
            parScheduleRecItem.Value = dtSchedules;
            cmdParams.Add(parScheduleRecItem);

            System.Data.IDbDataParameter parScheduleIntervalDetails = cmd.CreateParameter();
            parScheduleIntervalDetails.ParameterName = "@udtScheduleIntervalDetails";
            parScheduleIntervalDetails.Value = dtScheduleIntervalDetails;
            cmdParams.Add(parScheduleIntervalDetails);

            return cmd;
        }

        public List<AttachmentInfo> CopyRecurringItemSchedule(GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo, DataTable dtGLDataParams, int? recPeriodID, bool CloseSourceRecItem, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            List<AttachmentInfo> oAttachmentInfoList = null;
            IDbCommand cmd = null;
            IDataReader reader = null;
            try
            {
                cmd = this.CreateCopyRecurringItemScheduleCommand(oGLDataRecurringItemScheduleInfo, dtGLDataParams, recPeriodID, CloseSourceRecItem);
                cmd.Connection = oConnection;
                cmd.Transaction = oTransaction;
                reader = cmd.ExecuteReader();
                oAttachmentInfoList = new List<AttachmentInfo>();
                while (reader.Read())
                {
                    oAttachmentInfoList.Add(MapObjectAttachmentInfo(reader));
                }
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
            }
            return oAttachmentInfoList;
        }

        private IDbCommand CreateCopyRecurringItemScheduleCommand(GLDataRecurringItemScheduleInfo entity, DataTable dtGLDataParams, int? recPeriodID, bool CloseSourceRecItem)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_CopyGLDataRecurringItemSchedule");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parTblSourceRecItemID = cmd.CreateParameter();
            parTblSourceRecItemID.ParameterName = "@tblSourceRecItemID";
            parTblSourceRecItemID.Value = dtGLDataParams;
            cmdParams.Add(parTblSourceRecItemID);

            System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            if (!entity.IsGLDataIDNull)
                parGLDataID.Value = entity.GLDataID;
            else
                parGLDataID.Value = System.DBNull.Value;
            cmdParams.Add(parGLDataID);

            System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            if (recPeriodID.HasValue)
                parRecPeriodID.Value = recPeriodID.Value;
            else
                parRecPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parRecPeriodID);

            System.Data.IDbDataParameter parAddedByUserID = cmd.CreateParameter();
            parAddedByUserID.ParameterName = "@AddedByUserID";
            parAddedByUserID.Value = entity.AddedByUserID.Value;
            cmdParams.Add(parAddedByUserID);

            IDbDataParameter paramAddedBy = cmd.CreateParameter();
            paramAddedBy.ParameterName = "@AddedBy";
            paramAddedBy.Value = entity.AddedBy;
            cmdParams.Add(paramAddedBy);

            IDbDataParameter paramDateAdded = cmd.CreateParameter();
            paramDateAdded.ParameterName = "@DateAdded";
            paramDateAdded.Value = entity.DateAdded;
            cmdParams.Add(paramDateAdded);

            IDbDataParameter paramCloseSourceRecItem = cmd.CreateParameter();
            paramCloseSourceRecItem.ParameterName = "@CloseSourceRecItem";
            paramCloseSourceRecItem.Value = CloseSourceRecItem;
            cmdParams.Add(paramCloseSourceRecItem);

            return cmd;
        }


        public AttachmentInfo MapObjectAttachmentInfo(System.Data.IDataReader reader)
        {

            AttachmentInfo entity = new AttachmentInfo();
            entity.RecordID = reader.GetInt64Value("RecordID");
            entity.FileName = reader.GetStringValue("FileName");
            entity.FileSize = reader.GetInt32Value("FileSize");
            entity.PhysicalPath = reader.GetStringValue("PhysicalPath");
            entity.Comments = reader.GetStringValue("Comments");
            entity.IsPermanentOrTemporary = reader.GetBooleanValue("IsPermanentOrTemporary");
            entity.RecordTypeID = reader.GetInt16Value("RecordTypeID");
            entity.DocumentName = reader.GetStringValue("DocumentName");

            return entity;
        }
    }
}