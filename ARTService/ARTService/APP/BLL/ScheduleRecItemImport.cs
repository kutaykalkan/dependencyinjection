using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Service.Model;
using SkyStem.ART.Client.Model.CompanyDatabase;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Service.Utility;
using System.Data;
using SkyStem.ART.Shared.Data;
using SkyStem.ART.Service.Data;
using System.Data.SqlClient;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Shared.Utility;
using SkyStem.Language.LanguageUtility;
using SkyStem.Language.LanguageUtility.Classes;
using SkyStem.ART.Service.APP.DAO;

namespace SkyStem.ART.Service.APP.BLL
{
    public class ScheduleRecItemImport
    {
        private ScheduleRecItemImportInfo oScheduleRecItemImportInfo;
        public CompanyUserInfo CompanyUserInfo { get; private set; }
        private List<LogInfo> LogInfoCache;

        public ScheduleRecItemImport(CompanyUserInfo oCompanyUserInfo)
        {
            this.CompanyUserInfo = oCompanyUserInfo;
            this.LogInfoCache = new List<LogInfo>();
        }
        #region "Public Methods"
        public bool IsProcessingRequiredForScheduleRecItemImport()
        {
            bool processingRequired = false;
            try
            {
                oScheduleRecItemImportInfo = this.GetScheduleRecItemImportForProcessing(DateTime.Now, this.CompanyUserInfo);
                if (oScheduleRecItemImportInfo.DataImportID > 0)
                {
                    processingRequired = true;
                    Helper.LogInfo(@"Schedule Rec Item Import required for DataImportID: " + oScheduleRecItemImportInfo.DataImportID.ToString(), this.CompanyUserInfo);
                }
                else
                {
                    Helper.LogInfo(@"No Data Available for Schedule Rec Item Import.", this.CompanyUserInfo);
                }
            }
            catch (Exception ex)
            {
                oScheduleRecItemImportInfo = null;
                processingRequired = false;
                Helper.LogError(oScheduleRecItemImportInfo, @"Error in IsProcessingRequiredForScheduleRecItemImport: ", ex, this.CompanyUserInfo);
            }
            return processingRequired;
        }

        public void ProcessScheduleRecItemImport()
        {
            try
            {
                Helper.LogInfo(@"Start Schedule Rec Item Import for DataImportID: " + oScheduleRecItemImportInfo.DataImportID.ToString(), this.CompanyUserInfo);
                ExtractAndProcessData();
            }
            catch (Exception ex)
            {
                DataImportHelper.ResetScheduleRecItemDataHdrObject(oScheduleRecItemImportInfo, ex);
                Helper.LogErrorToCache(ex, this.LogInfoCache);
            }
            finally
            {
                try
                {
                    this.UpdateDataImportHDR(oScheduleRecItemImportInfo, this.CompanyUserInfo);
                }
                catch (Exception ex)
                {
                    Helper.LogErrorToCache("Error while updating DataImportHDR - ", this.LogInfoCache);
                    Helper.LogErrorToCache(ex, this.LogInfoCache);
                }
                try
                {
                    oScheduleRecItemImportInfo.SuccessEmailIDs = DataImportHelper.GetEmailIDWithSeprator(oScheduleRecItemImportInfo.NotifySuccessEmailIds) + DataImportHelper.GetEmailIDWithSeprator(oScheduleRecItemImportInfo.NotifySuccessUserEmailIds) + DataImportHelper.GetEmailIDWithSeprator(oScheduleRecItemImportInfo.WarningEmailIds);
                    oScheduleRecItemImportInfo.FailureEmailIDs = DataImportHelper.GetEmailIDWithSeprator(oScheduleRecItemImportInfo.NotifyFailureEmailIds) + DataImportHelper.GetEmailIDWithSeprator(oScheduleRecItemImportInfo.NotifyFailureUserEmailIds) + DataImportHelper.GetEmailIDWithSeprator(oScheduleRecItemImportInfo.WarningEmailIds);
                    DataImportHelper.SendMailToUsers(oScheduleRecItemImportInfo, this.CompanyUserInfo);
                }
                catch (Exception ex)
                {
                    Helper.LogErrorToCache("Error while sending mail - ", this.LogInfoCache);
                    Helper.LogErrorToCache(ex, this.LogInfoCache);
                }
                try
                {
                    Helper.LogListViaService(this.LogInfoCache, oScheduleRecItemImportInfo.DataImportID, this.CompanyUserInfo);
                    Helper.LogInfo(@"End GLData Import for DataImportID: " + oScheduleRecItemImportInfo.DataImportID.ToString(), this.CompanyUserInfo);
                }
                catch (Exception ex)
                {
                    Helper.LogError("Error while logging - ", this.CompanyUserInfo);
                    Helper.LogError(ex, this.CompanyUserInfo);
                }
            }
        }

        #endregion

        #region "Private Methods"

        private ScheduleRecItemImportInfo GetScheduleRecItemImportForProcessing(DateTime dateRevised, CompanyUserInfo oCompanyUserInfo)
        {
            ScheduleRecItemImportDAO oScheduleRecItemImportDAO = new ScheduleRecItemImportDAO(oCompanyUserInfo);
            ScheduleRecItemImportInfo oScheduleRecItemImportInfo = oScheduleRecItemImportDAO.GetScheduleRecItemImportForProcessing(dateRevised);
            if (oScheduleRecItemImportInfo.DataImportID > 0)
            {
                oScheduleRecItemImportInfo.ExchangeRateInfoList = DataImportHelper.GetExchangeRateByRecPeriod(oScheduleRecItemImportInfo.RecPeriodID, oCompanyUserInfo);
            }
            return oScheduleRecItemImportInfo;
        }

        private void UpdateDataImportHDR(ScheduleRecItemImportInfo oScheduleRecItemImportInfo, CompanyUserInfo oCompanyUserInfo)
        {
            DataImportHdrDAO oDataImportHdrDAO = new DataImportHdrDAO(oCompanyUserInfo);
            oDataImportHdrDAO.UpdateDataImportHDR(oScheduleRecItemImportInfo);
        }

        private void ExtractAndProcessData()
        {
            DataTable dtExcelData = null;
            Helper.LogInfoToCache("3. Start Reading Excel file: " + oScheduleRecItemImportInfo.PhysicalPath, this.LogInfoCache);
            dtExcelData = DataImportHelper.GetScheduleRecItemImportDataTableFromExcel(oScheduleRecItemImportInfo.PhysicalPath, ScheduleRecItemUploadConstants.SheetName, this.CompanyUserInfo);

            if (ValidateSchemaForScheduleRecItem(dtExcelData))
            {
                Helper.LogInfoToCache("4. Reading Excel file complete.", this.LogInfoCache);

                if (!oScheduleRecItemImportInfo.IsForceCommit)
                {//Mark Static Field Present
                    this.FieldPresent(dtExcelData);
                }

                //Add additional fields to ExcelDataTabel
                AddDataImportIDToDataTable(dtExcelData);

                //Validate and Convert Data
                ValidateAndConvertData(dtExcelData);

                //Process Data
                ProcessdData(dtExcelData);
            }
        }

        private void ProcessdData(DataTable dtExcelData)
        {
            int? rowsAffected = null;
            try
            {
                List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoList = new List<GLDataRecurringItemScheduleInfo>();
                foreach (DataRow dr in dtExcelData.Rows)
                {
                    if (Convert.ToBoolean(dr[ScheduleRecItemUploadConstants.AddedFields.ISVALIDROW]) == true)
                        oGLDataRecurringItemScheduleInfoList.Add(ConvertRowToScheduleInfo(dr));
                }
                IDataImport oDataImport = RemotingHelper.GetDataImportObject();
                AppUserInfo oAppUserInfo = new AppUserInfo();
                oAppUserInfo.CompanyID = this.CompanyUserInfo.CompanyID;
                oAppUserInfo.LoginID = this.CompanyUserInfo.LoginID;
                rowsAffected = oDataImport.InsertGLDataRecItemScheduleBulk(oScheduleRecItemImportInfo.GLDataID, oScheduleRecItemImportInfo.RecPeriodID,
                    oGLDataRecurringItemScheduleInfoList, oScheduleRecItemImportInfo.AddedBy, DateTime.Now, oAppUserInfo);
            }
            finally
            {
                if (rowsAffected.GetValueOrDefault() > 0)
                {
                    oScheduleRecItemImportInfo.RecordsImported = rowsAffected.Value;
                    oScheduleRecItemImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTSUCCESS;
                }
                else
                {
                    oScheduleRecItemImportInfo.RecordsImported = rowsAffected.Value;
                    oScheduleRecItemImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
                }
            }
        }

        private GLDataRecurringItemScheduleInfo ConvertRowToScheduleInfo(DataRow dr)
        {
            GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo = new GLDataRecurringItemScheduleInfo();
            oGLDataRecurringItemScheduleInfo.RecCategoryTypeID = oScheduleRecItemImportInfo.ReconciliationCategoryTypeID;
            oGLDataRecurringItemScheduleInfo.ReconciliationCategoryTypeID = oScheduleRecItemImportInfo.ReconciliationCategoryTypeID;
            oGLDataRecurringItemScheduleInfo.GLDataID = oScheduleRecItemImportInfo.GLDataID;
            oGLDataRecurringItemScheduleInfo.ExcelRowNumber = Convert.ToInt64(dr[ScheduleRecItemUploadConstants.AddedFields.EXCELROWNUMBER]);
            oGLDataRecurringItemScheduleInfo.DataImportID = oScheduleRecItemImportInfo.DataImportID;
            oGLDataRecurringItemScheduleInfo.RecordSourceID = oScheduleRecItemImportInfo.DataImportID;
            oGLDataRecurringItemScheduleInfo.RecordSourceTypeID = (short)ARTEnums.RecordSourceType.DataImport;
            oGLDataRecurringItemScheduleInfo.ScheduleName = Convert.ToString(dr[ScheduleRecItemUploadConstants.Fields.ScheduleName]);
            if (dr[ScheduleRecItemUploadConstants.Fields.Description] != DBNull.Value
                && !string.IsNullOrEmpty(dr[ScheduleRecItemUploadConstants.Fields.Description].ToString()))
                oGLDataRecurringItemScheduleInfo.Comments = Convert.ToString(dr[ScheduleRecItemUploadConstants.Fields.Description]);
            oGLDataRecurringItemScheduleInfo.ScheduleAmount = Convert.ToDecimal(dr[ScheduleRecItemUploadConstants.Fields.OriginalAmount]);
            oGLDataRecurringItemScheduleInfo.LocalCurrencyCode = Convert.ToString(dr[ScheduleRecItemUploadConstants.Fields.LCCYCode]);
            oGLDataRecurringItemScheduleInfo.OpenDate = Convert.ToDateTime(dr[ScheduleRecItemUploadConstants.Fields.OpenDate]);
            oGLDataRecurringItemScheduleInfo.CalculationFrequencyID = Convert.ToInt16(dr[ScheduleRecItemUploadConstants.AddedFields.CALCULATIONFREQUENCYID]);
            if (oGLDataRecurringItemScheduleInfo.CalculationFrequencyID == (short)ARTEnums.CalculationFrequency.OtherInterval)
            {
                if (dr[ScheduleRecItemUploadConstants.AddedFields.STARTINTERVALRECPERIODID] != DBNull.Value
                    && !string.IsNullOrEmpty(dr[ScheduleRecItemUploadConstants.AddedFields.STARTINTERVALRECPERIODID].ToString()))
                    oGLDataRecurringItemScheduleInfo.StartIntervalRecPeriodID = Convert.ToInt32(dr[ScheduleRecItemUploadConstants.AddedFields.STARTINTERVALRECPERIODID]);
                if (dr[ScheduleRecItemUploadConstants.AddedFields.TotalInterval] != DBNull.Value
                    && !string.IsNullOrEmpty(dr[ScheduleRecItemUploadConstants.AddedFields.TotalInterval].ToString()))
                    oGLDataRecurringItemScheduleInfo.TotalIntervals = Convert.ToInt32(dr[ScheduleRecItemUploadConstants.AddedFields.TotalInterval]);
                if (dr[ScheduleRecItemUploadConstants.AddedFields.CurrentInterval] != DBNull.Value
                    && !string.IsNullOrEmpty(dr[ScheduleRecItemUploadConstants.AddedFields.CurrentInterval].ToString()))
                    oGLDataRecurringItemScheduleInfo.CurrentInterval = Convert.ToInt32(dr[ScheduleRecItemUploadConstants.AddedFields.CurrentInterval]);
            }
            oGLDataRecurringItemScheduleInfo.IgnoreInCalculation = Convert.ToBoolean(dr[ScheduleRecItemUploadConstants.AddedFields.IGNOREINCALCULATION]);
            oGLDataRecurringItemScheduleInfo.ScheduleBeginDate = Convert.ToDateTime(dr[ScheduleRecItemUploadConstants.Fields.ScheduleBeginDate]);
            oGLDataRecurringItemScheduleInfo.ScheduleEndDate = Convert.ToDateTime(dr[ScheduleRecItemUploadConstants.Fields.ScheduleEndDate]);
            oGLDataRecurringItemScheduleInfo.GLDataRecurringItemScheduleIntervalDetailInfoList = (List<GLDataRecurringItemScheduleIntervalDetailInfo>)SharedHelper.DeepClone(oScheduleRecItemImportInfo.GLDataRecurringItemScheduleIntervalDetailInfoList);
            oGLDataRecurringItemScheduleInfo.IsActive = true;
            oGLDataRecurringItemScheduleInfo.AddedBy = oScheduleRecItemImportInfo.AddedBy;
            oGLDataRecurringItemScheduleInfo.DateAdded = DateTime.Now;
            oGLDataRecurringItemScheduleInfo.AddedByUserID = oScheduleRecItemImportInfo.UserID;
            if (oGLDataRecurringItemScheduleInfo.CalculationFrequencyID == (short)ARTEnums.CalculationFrequency.DailyInterval)
            {
                SharedRecItemHelper.RecalculateScheduleDaily(oGLDataRecurringItemScheduleInfo.GLDataRecurringItemScheduleIntervalDetailInfoList,
                        oScheduleRecItemImportInfo.PeriodEndDate, oScheduleRecItemImportInfo.PeriodEndDate,
                        oGLDataRecurringItemScheduleInfo.ScheduleAmount, oGLDataRecurringItemScheduleInfo.ScheduleBeginDate,
                        oGLDataRecurringItemScheduleInfo.ScheduleEndDate);
            }
            else
            {
                SharedRecItemHelper.RecalculateScheduleOther(oGLDataRecurringItemScheduleInfo.GLDataRecurringItemScheduleIntervalDetailInfoList,
                        oScheduleRecItemImportInfo.PeriodEndDate, oGLDataRecurringItemScheduleInfo.ScheduleAmount,
                        oGLDataRecurringItemScheduleInfo.TotalIntervals, oGLDataRecurringItemScheduleInfo.StartIntervalRecPeriodID,
                        oGLDataRecurringItemScheduleInfo.CurrentInterval, null);
            }
            decimal GrandTotAmount = 0;
            decimal CurrConsumedAmount = 0;
            decimal TotConsumedAmount = 0;
            SharedRecItemHelper.GetTotals(oGLDataRecurringItemScheduleInfo.GLDataRecurringItemScheduleIntervalDetailInfoList
                , oScheduleRecItemImportInfo.PeriodEndDate, out GrandTotAmount, out CurrConsumedAmount, out TotConsumedAmount);
            oGLDataRecurringItemScheduleInfo.RecPeriodAmountLocalCurrency = TotConsumedAmount;
            oGLDataRecurringItemScheduleInfo.BalanceLocalCurrency = oGLDataRecurringItemScheduleInfo.ScheduleAmount - TotConsumedAmount;

            decimal? exRateLccyToBccy = Convert.ToDecimal(dr[ScheduleRecItemUploadConstants.AddedFields.EXRATELCCYTOBCCY]);
            decimal? exRateLccyToRccy = Convert.ToDecimal(dr[ScheduleRecItemUploadConstants.AddedFields.EXRATELCCYTORCCY]);
            SharedRecItemHelper.RecalculateRecItemScheduleAmount(oGLDataRecurringItemScheduleInfo, oScheduleRecItemImportInfo.BaseCurrencyCode, oScheduleRecItemImportInfo.ReportingCurrencyCode, exRateLccyToBccy, exRateLccyToRccy);
            return oGLDataRecurringItemScheduleInfo;
        }

        private bool ValidateSchemaForScheduleRecItem(DataTable dtExcelData)
        {
            bool isValidSchema;
            bool columnFound;
            StringBuilder oSbError = new StringBuilder();

            //Get list of all mandatory fields
            List<string> ImportMandatoryFieldList = DataImportHelper.GetScheduleRecItemImportAllMandatoryFields();

            //Check if all mandatory fields exists in DataTable from Excel
            foreach (string fieldName in ImportMandatoryFieldList)
            {
                columnFound = false;
                for (int i = 0; i < dtExcelData.Columns.Count; i++)
                {
                    if (fieldName == dtExcelData.Columns[i].ColumnName)
                    {
                        columnFound = true;
                        break;
                    }
                }
                if (!columnFound)
                {
                    if (!oSbError.ToString().Equals(string.Empty))
                        oSbError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    oSbError.Append(fieldName);
                }
            }
            isValidSchema = string.IsNullOrEmpty(oSbError.ToString());

            //If schema is not valid, generate a multi lingual error message, set failure status, faliure status ID, error message 
            //in GLDataImport object and throw an exception with generated message 
            if (!isValidSchema)
            {
                string errorMessage = Helper.GetSinglePhrase(5000165, 0, oScheduleRecItemImportInfo.LanguageID, oScheduleRecItemImportInfo.DefaultLanguageID, this.CompanyUserInfo);//Mandatory columns not present: {0}

                oScheduleRecItemImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
                oScheduleRecItemImportInfo.DataImportStatusID = (short)Enums.DataImportStatus.Failure;
                oScheduleRecItemImportInfo.ErrorMessageToSave = String.Format(errorMessage, oSbError.ToString());
                throw new Exception(String.Format(errorMessage, oSbError.ToString()));
            }
            return isValidSchema;
        }

        private void ValidateAndConvertData(DataTable dtExcelData)
        {

            StringBuilder oSBError = new StringBuilder();
            string msg = Helper.GetDataLengthErrorMessage(ServiceConstants.DEFAULTBUSINESSENTITYID, oScheduleRecItemImportInfo.LanguageID, oScheduleRecItemImportInfo.DefaultLanguageID, this.CompanyUserInfo);
            string InvalidDataMsg = Helper.GetInvalidDataErrorMessage(ServiceConstants.DEFAULTBUSINESSENTITYID, oScheduleRecItemImportInfo.LanguageID, oScheduleRecItemImportInfo.DefaultLanguageID, this.CompanyUserInfo);
            string msgMandatoryField = Helper.GetMandatoryFieldErrorMessage(ServiceConstants.DEFAULTBUSINESSENTITYID, oScheduleRecItemImportInfo.LanguageID, oScheduleRecItemImportInfo.DefaultLanguageID, this.CompanyUserInfo);
            string msgExRateMissing = Helper.GetSinglePhrase(5000216, ServiceConstants.DEFAULTBUSINESSENTITYID, oScheduleRecItemImportInfo.LanguageID, oScheduleRecItemImportInfo.DefaultLanguageID, this.CompanyUserInfo);
            string msgLessThan = Helper.GetLessThanErrorMessage(ServiceConstants.DEFAULTBUSINESSENTITYID, oScheduleRecItemImportInfo.LanguageID, oScheduleRecItemImportInfo.DefaultLanguageID, this.CompanyUserInfo);

            //2247 -- Daily Interval
            //2248 -- Other Interval
            //1252 -- Yes
            //1251 -- No
            //5000140 -- Current Date
            string strDaily = Helper.GetSinglePhrase(2247, ServiceConstants.DEFAULTBUSINESSENTITYID, oScheduleRecItemImportInfo.LanguageID, oScheduleRecItemImportInfo.DefaultLanguageID, this.CompanyUserInfo).ToLower();
            string strOther = Helper.GetSinglePhrase(2248, ServiceConstants.DEFAULTBUSINESSENTITYID, oScheduleRecItemImportInfo.LanguageID, oScheduleRecItemImportInfo.DefaultLanguageID, this.CompanyUserInfo).ToLower();
            string strYes = Helper.GetSinglePhrase(1252, ServiceConstants.DEFAULTBUSINESSENTITYID, oScheduleRecItemImportInfo.LanguageID, oScheduleRecItemImportInfo.DefaultLanguageID, this.CompanyUserInfo).ToLower();
            string strNo = Helper.GetSinglePhrase(1251, ServiceConstants.DEFAULTBUSINESSENTITYID, oScheduleRecItemImportInfo.LanguageID, oScheduleRecItemImportInfo.DefaultLanguageID, this.CompanyUserInfo).ToLower();
            string strCurrentDate = Helper.GetSinglePhrase(5000140, ServiceConstants.DEFAULTBUSINESSENTITYID, oScheduleRecItemImportInfo.LanguageID, oScheduleRecItemImportInfo.DefaultLanguageID, this.CompanyUserInfo).ToLower();

            int validRowCount = 0;
            for (int x = 0; x < dtExcelData.Rows.Count; x++)
            {
                DataRow dr = dtExcelData.Rows[x];
                string excelRowNumber = dr[ScheduleRecItemUploadConstants.AddedFields.EXCELROWNUMBER].ToString();
                bool isValidRow = true;

                // Validate Schedule Name
                if (dr[ScheduleRecItemUploadConstants.Fields.ScheduleName] == DBNull.Value
                    || string.IsNullOrEmpty(dr[ScheduleRecItemUploadConstants.Fields.ScheduleName].ToString()))
                {
                    oSBError.Append(String.Format(msgMandatoryField, ScheduleRecItemUploadConstants.Fields.ScheduleName, excelRowNumber));
                    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    isValidRow = false;
                }
                else if (dr[ScheduleRecItemUploadConstants.Fields.ScheduleName].ToString().Trim().Length > ScheduleRecItemUploadConstants.DataLength.ScheduleName)
                {
                    oSBError.Append(String.Format(msg, ScheduleRecItemUploadConstants.Fields.ScheduleName, excelRowNumber, ScheduleRecItemUploadConstants.DataLength.ScheduleName));
                    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    isValidRow = false;
                }
                //Store Schedule Name
                else
                {
                    dr[ScheduleRecItemUploadConstants.Fields.ScheduleName] = dr[ScheduleRecItemUploadConstants.Fields.ScheduleName].ToString().Trim();
                }

                // Validate Original Amount
                decimal originalAmt = 0;
                if (dr[ScheduleRecItemUploadConstants.Fields.OriginalAmount] == DBNull.Value
                    || string.IsNullOrEmpty(dr[ScheduleRecItemUploadConstants.Fields.OriginalAmount].ToString()))
                {
                    oSBError.Append(String.Format(msgMandatoryField, ScheduleRecItemUploadConstants.Fields.OriginalAmount, excelRowNumber));
                    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    isValidRow = false;
                }
                else if (!Helper.IsValidDecimal(dr[ScheduleRecItemUploadConstants.Fields.OriginalAmount].ToString(), oScheduleRecItemImportInfo.LanguageID, out originalAmt))
                {
                    oSBError.Append(String.Format(InvalidDataMsg, ScheduleRecItemUploadConstants.Fields.OriginalAmount, excelRowNumber));
                    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    isValidRow = false;
                }
                // Store Original Amount
                else
                {
                    dr[ScheduleRecItemUploadConstants.Fields.OriginalAmount] = originalAmt.ToString();
                }

                //Validate Local Currency Code
                if (dr[ScheduleRecItemUploadConstants.Fields.LCCYCode] == DBNull.Value
                    || string.IsNullOrEmpty(dr[ScheduleRecItemUploadConstants.Fields.LCCYCode].ToString()))
                {
                    oSBError.Append(String.Format(msgMandatoryField, ScheduleRecItemUploadConstants.Fields.LCCYCode, excelRowNumber));
                    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    isValidRow = false;
                }
                else if (dr[ScheduleRecItemUploadConstants.Fields.LCCYCode].ToString().Trim().Length > ScheduleRecItemUploadConstants.DataLength.LCCYCode)
                {
                    oSBError.Append(String.Format(msg, ScheduleRecItemUploadConstants.Fields.LCCYCode, excelRowNumber, ScheduleRecItemUploadConstants.DataLength.LCCYCode));
                    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    isValidRow = false;
                }
                else
                {
                    // Store LCCY Code
                    dr[ScheduleRecItemUploadConstants.Fields.LCCYCode] = dr[ScheduleRecItemUploadConstants.Fields.LCCYCode].ToString().Trim();
                    string lccyCode = dr[ScheduleRecItemUploadConstants.Fields.LCCYCode].ToString();
                    if (!string.IsNullOrEmpty(oScheduleRecItemImportInfo.BaseCurrencyCode))
                    {
                        decimal? exRateLCCYToBCCY = SharedRecItemHelper.GetExchangeRate(oScheduleRecItemImportInfo.ExchangeRateInfoList, lccyCode, oScheduleRecItemImportInfo.BaseCurrencyCode);
                        if (exRateLCCYToBCCY.GetValueOrDefault() != 0)
                            dr[ScheduleRecItemUploadConstants.AddedFields.EXRATELCCYTOBCCY] = exRateLCCYToBCCY.Value;
                        else
                        {
                            oSBError.Append(String.Format(InvalidDataMsg, ScheduleRecItemUploadConstants.Fields.LCCYCode, excelRowNumber));
                            oSBError.Append(" ");
                            oSBError.Append(String.Format(msgExRateMissing, dr[ScheduleRecItemUploadConstants.Fields.LCCYCode].ToString(), oScheduleRecItemImportInfo.BaseCurrencyCode));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            isValidRow = false;
                        }
                    }
                    if (!string.IsNullOrEmpty(oScheduleRecItemImportInfo.ReportingCurrencyCode))
                    {
                        decimal? exRateLCCYToRCCY = SharedRecItemHelper.GetExchangeRate(oScheduleRecItemImportInfo.ExchangeRateInfoList, lccyCode, oScheduleRecItemImportInfo.ReportingCurrencyCode);
                        if (exRateLCCYToRCCY.GetValueOrDefault() != 0)
                            dr[ScheduleRecItemUploadConstants.AddedFields.EXRATELCCYTORCCY] = exRateLCCYToRCCY.Value;
                        else
                        {
                            oSBError.Append(String.Format(InvalidDataMsg, ScheduleRecItemUploadConstants.Fields.LCCYCode, excelRowNumber));
                            oSBError.Append(" ");
                            oSBError.Append(String.Format(msgExRateMissing, dr[ScheduleRecItemUploadConstants.Fields.LCCYCode].ToString(), oScheduleRecItemImportInfo.ReportingCurrencyCode));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            isValidRow = false;
                        }
                    }
                }

                // Validate Open Date
                DateTime openDate;
                if (dr[ScheduleRecItemUploadConstants.Fields.OpenDate] == DBNull.Value
                    || string.IsNullOrEmpty(dr[ScheduleRecItemUploadConstants.Fields.OpenDate].ToString()))
                {
                    oSBError.Append(String.Format(msgMandatoryField, ScheduleRecItemUploadConstants.Fields.OpenDate, excelRowNumber));
                    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    isValidRow = false;
                }
                else if (!Helper.IsValidDateTime(dr[ScheduleRecItemUploadConstants.Fields.OpenDate].ToString(), oScheduleRecItemImportInfo.LanguageID, out openDate))
                {
                    oSBError.Append(String.Format(InvalidDataMsg, ScheduleRecItemUploadConstants.Fields.OpenDate, excelRowNumber));
                    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    isValidRow = false;
                }
                else if (openDate.Date > DateTime.Now.Date)
                {
                    oSBError.Append(String.Format(InvalidDataMsg, ScheduleRecItemUploadConstants.Fields.OpenDate, excelRowNumber));
                    oSBError.Append(" ");
                    oSBError.Append(String.Format(msgLessThan, ScheduleRecItemUploadConstants.Fields.OpenDate, strCurrentDate));
                    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    isValidRow = false;
                }
                else
                {
                    dr[ScheduleRecItemUploadConstants.Fields.OpenDate] = openDate.ToShortDateString();
                }
                // Validate Begin Schedule On
                DateTime BeginScheduleOn = DateTime.MinValue;
                if (dr[ScheduleRecItemUploadConstants.Fields.BeginScheduleOn] != DBNull.Value &&
                    !string.IsNullOrEmpty(dr[ScheduleRecItemUploadConstants.Fields.BeginScheduleOn].ToString()))
                {
                    if (!Helper.IsValidDateTime(dr[ScheduleRecItemUploadConstants.Fields.BeginScheduleOn].ToString(), oScheduleRecItemImportInfo.LanguageID, out BeginScheduleOn))
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, ScheduleRecItemUploadConstants.Fields.BeginScheduleOn, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                    else
                    {
                        dr[ScheduleRecItemUploadConstants.Fields.BeginScheduleOn] = BeginScheduleOn.ToShortDateString();
                        GLDataRecurringItemScheduleIntervalDetailInfo oGLDataRecurringItemScheduleIntervalDetailInfo = oScheduleRecItemImportInfo.GLDataRecurringItemScheduleIntervalDetailInfoList.FirstOrDefault(T => T.PeriodEndDate == BeginScheduleOn);
                        if (oGLDataRecurringItemScheduleIntervalDetailInfo != null)
                            dr[ScheduleRecItemUploadConstants.AddedFields.STARTINTERVALRECPERIODID] = oGLDataRecurringItemScheduleIntervalDetailInfo.ReconciliationPeriodID;
                        else
                        {
                            oSBError.Append(String.Format(InvalidDataMsg, ScheduleRecItemUploadConstants.Fields.BeginScheduleOn, excelRowNumber));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            isValidRow = false;
                        }
                    }
                }
                // Dont show on Rec Form
                string dontShowOnRecForm = strNo;
                if (dr[ScheduleRecItemUploadConstants.Fields.IncludeOnBeginDate] != DBNull.Value &&
                    !string.IsNullOrEmpty(dr[ScheduleRecItemUploadConstants.Fields.IncludeOnBeginDate].ToString()))
                {
                    dontShowOnRecForm = dr[ScheduleRecItemUploadConstants.Fields.IncludeOnBeginDate].ToString().Trim().ToLower();
                }
                if (dontShowOnRecForm == strYes)
                {
                    dr[ScheduleRecItemUploadConstants.AddedFields.IGNOREINCALCULATION] = true;
                }
                else if (dontShowOnRecForm == strNo)
                {
                    dr[ScheduleRecItemUploadConstants.AddedFields.IGNOREINCALCULATION] = false;
                }
                else
                {
                    oSBError.Append(String.Format(InvalidDataMsg, ScheduleRecItemUploadConstants.Fields.IncludeOnBeginDate, excelRowNumber));
                    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    isValidRow = false;
                }

                // Schedule Begin Date
                DateTime scheduleBeginDate = DateTime.MinValue;
                if (dr[ScheduleRecItemUploadConstants.AddedFields.STARTINTERVALRECPERIODID] == DBNull.Value
                    && (dr[ScheduleRecItemUploadConstants.Fields.ScheduleBeginDate] == DBNull.Value
                        || string.IsNullOrEmpty(dr[ScheduleRecItemUploadConstants.Fields.ScheduleBeginDate].ToString())))
                {
                    oSBError.Append(String.Format(msgMandatoryField, ScheduleRecItemUploadConstants.Fields.ScheduleBeginDate, excelRowNumber));
                    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    isValidRow = false;
                }
                else if (dr[ScheduleRecItemUploadConstants.Fields.ScheduleBeginDate] != DBNull.Value
                    && !string.IsNullOrEmpty(dr[ScheduleRecItemUploadConstants.Fields.ScheduleBeginDate].ToString()))
                {
                    if (!Helper.IsValidDateTime(dr[ScheduleRecItemUploadConstants.Fields.ScheduleBeginDate].ToString(), oScheduleRecItemImportInfo.LanguageID, out scheduleBeginDate))
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, ScheduleRecItemUploadConstants.Fields.ScheduleBeginDate, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                    else
                        dr[ScheduleRecItemUploadConstants.Fields.ScheduleBeginDate] = scheduleBeginDate.ToShortDateString();
                }
                else if (dr[ScheduleRecItemUploadConstants.AddedFields.STARTINTERVALRECPERIODID] != DBNull.Value)
                {
                    if (dr[ScheduleRecItemUploadConstants.Fields.ScheduleBeginDate] == DBNull.Value
                        || string.IsNullOrEmpty(dr[ScheduleRecItemUploadConstants.Fields.ScheduleBeginDate].ToString()))
                    {
                        dr[ScheduleRecItemUploadConstants.Fields.ScheduleBeginDate] = BeginScheduleOn.ToShortDateString();
                        scheduleBeginDate = BeginScheduleOn;
                    }
                }
                if (dr[ScheduleRecItemUploadConstants.AddedFields.STARTINTERVALRECPERIODID] != DBNull.Value)
                {
                    if (scheduleBeginDate != BeginScheduleOn)
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, ScheduleRecItemUploadConstants.Fields.ScheduleBeginDate, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                }
                else if (scheduleBeginDate.Date > oScheduleRecItemImportInfo.PeriodEndDate)
                {
                    oSBError.Append(String.Format(InvalidDataMsg, ScheduleRecItemUploadConstants.Fields.ScheduleBeginDate, excelRowNumber));
                    oSBError.Append(" ");
                    oSBError.Append(String.Format(msgLessThan, ScheduleRecItemUploadConstants.Fields.ScheduleBeginDate, strCurrentDate));
                    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    isValidRow = false;
                }

                DateTime scheduleEndDate = DateTime.MinValue;
                if (dr[ScheduleRecItemUploadConstants.Fields.ScheduleEndDate] == DBNull.Value
                    || string.IsNullOrEmpty(dr[ScheduleRecItemUploadConstants.Fields.ScheduleEndDate].ToString()))
                {
                    oSBError.Append(String.Format(msgMandatoryField, ScheduleRecItemUploadConstants.Fields.ScheduleEndDate, excelRowNumber));
                    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    isValidRow = false;
                }
                else
                {
                    if (!Helper.IsValidDateTime(dr[ScheduleRecItemUploadConstants.Fields.ScheduleEndDate].ToString(), oScheduleRecItemImportInfo.LanguageID, out scheduleEndDate))
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, ScheduleRecItemUploadConstants.Fields.ScheduleEndDate, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                    else
                    {
                        if (scheduleEndDate.Date < scheduleBeginDate.Date)
                        {
                            oSBError.Append(String.Format(InvalidDataMsg, ScheduleRecItemUploadConstants.Fields.ScheduleEndDate, excelRowNumber));
                            oSBError.Append(" ");
                            oSBError.Append(String.Format(msgLessThan, ScheduleRecItemUploadConstants.Fields.ScheduleBeginDate, ScheduleRecItemUploadConstants.Fields.ScheduleEndDate));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            isValidRow = false;
                        }
                        else
                            dr[ScheduleRecItemUploadConstants.Fields.ScheduleEndDate] = scheduleEndDate.ToShortDateString();
                    }
                }

                // Calculation Frequency
                string CalcFrequency = string.Empty;
                if (dr[ScheduleRecItemUploadConstants.Fields.CalculationFrequency] == DBNull.Value
                    || string.IsNullOrEmpty(dr[ScheduleRecItemUploadConstants.Fields.CalculationFrequency].ToString()))
                {
                    oSBError.Append(String.Format(msgMandatoryField, ScheduleRecItemUploadConstants.Fields.CalculationFrequency, excelRowNumber));
                    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    isValidRow = false;
                }
                else
                {
                    CalcFrequency = dr[ScheduleRecItemUploadConstants.Fields.CalculationFrequency].ToString().Trim().ToLower();
                    if (CalcFrequency == strDaily)
                    {
                        dr[ScheduleRecItemUploadConstants.AddedFields.CALCULATIONFREQUENCYID] = (short)ARTEnums.CalculationFrequency.DailyInterval;
                    }
                    else if (CalcFrequency == strOther)
                    {
                        dr[ScheduleRecItemUploadConstants.AddedFields.CALCULATIONFREQUENCYID] = (short)ARTEnums.CalculationFrequency.OtherInterval;
                    }
                    else
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, ScheduleRecItemUploadConstants.Fields.CalculationFrequency, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                }

                // Calculate Total Intervals
                if (CalcFrequency == strOther)
                {
                    if (dr[ScheduleRecItemUploadConstants.AddedFields.TotalInterval] == DBNull.Value
                        || string.IsNullOrEmpty(dr[ScheduleRecItemUploadConstants.AddedFields.TotalInterval].ToString()))
                        dr[ScheduleRecItemUploadConstants.AddedFields.TotalInterval] = SharedHelper.GetMonthsBetweenDates(scheduleEndDate, scheduleBeginDate).GetValueOrDefault();
                }

                // Total Intervals
                int totalInterval = 0;
                if (CalcFrequency == strDaily)
                {
                    if (dr[ScheduleRecItemUploadConstants.AddedFields.TotalInterval] != DBNull.Value)
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, ScheduleRecItemUploadConstants.AddedFields.TotalInterval, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                }
                else if (CalcFrequency == strOther)
                {
                    if (!Int32.TryParse(dr[ScheduleRecItemUploadConstants.AddedFields.TotalInterval].ToString(), out totalInterval))
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, ScheduleRecItemUploadConstants.AddedFields.TotalInterval, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                }
                // Calculate Current Interval
                if (CalcFrequency == strOther)
                {
                    if (dr[ScheduleRecItemUploadConstants.AddedFields.CurrentInterval] == DBNull.Value
                        || string.IsNullOrEmpty(dr[ScheduleRecItemUploadConstants.AddedFields.CurrentInterval].ToString()))
                    {
                        if (BeginScheduleOn == oScheduleRecItemImportInfo.PeriodEndDate)
                            dr[ScheduleRecItemUploadConstants.AddedFields.CurrentInterval] = 1;
                        else
                            dr[ScheduleRecItemUploadConstants.AddedFields.CurrentInterval] = SharedHelper.GetMonthsBetweenDates(oScheduleRecItemImportInfo.PeriodEndDate, scheduleBeginDate).GetValueOrDefault();
                    }
                }

                // Current Interval
                int currentInterval = 0;
                if (CalcFrequency == strDaily)
                {
                    if (dr[ScheduleRecItemUploadConstants.AddedFields.CurrentInterval] != DBNull.Value)
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, ScheduleRecItemUploadConstants.AddedFields.CurrentInterval, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                }
                else if (CalcFrequency == strOther)
                {
                    if (!Int32.TryParse(dr[ScheduleRecItemUploadConstants.AddedFields.CurrentInterval].ToString(), out currentInterval))
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, ScheduleRecItemUploadConstants.AddedFields.CurrentInterval, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                    if (totalInterval < currentInterval)
                    {
                        oSBError.Append(String.Format(msgLessThan, ScheduleRecItemUploadConstants.AddedFields.CurrentInterval, ScheduleRecItemUploadConstants.AddedFields.TotalInterval));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                }

                // Schedule Begin On based upon Calculation Frequency
                if (CalcFrequency == strDaily)
                {
                    if (dr[ScheduleRecItemUploadConstants.Fields.BeginScheduleOn] != DBNull.Value)
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, ScheduleRecItemUploadConstants.Fields.BeginScheduleOn, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                }

                dr[ScheduleRecItemUploadConstants.AddedFields.ISVALIDROW] = isValidRow;
                if (isValidRow)
                    validRowCount++;
            }
            if (!oSBError.ToString().Equals(String.Empty) && !oScheduleRecItemImportInfo.IsForceCommit)
            {
                if (validRowCount == 0)
                {
                    oScheduleRecItemImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
                    oScheduleRecItemImportInfo.DataImportStatusID = (short)Enums.DataImportStatus.Failure;
                }
                else
                {
                    oScheduleRecItemImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTWARNING;
                    oScheduleRecItemImportInfo.DataImportStatusID = (short)Enums.DataImportStatus.Warning;
                }
                oScheduleRecItemImportInfo.ErrorMessageToSave = oSBError.ToString();
                throw new Exception(oSBError.ToString());
            }
        }

        private void AddDataImportIDToDataTable(DataTable dtExcelData)
        {
            dtExcelData.Columns.Add(ScheduleRecItemUploadConstants.AddedFields.DATAIMPORTID, typeof(System.Int64));
            dtExcelData.Columns.Add(ScheduleRecItemUploadConstants.AddedFields.EXCELROWNUMBER, typeof(System.Int64));
            dtExcelData.Columns.Add(ScheduleRecItemUploadConstants.AddedFields.STARTINTERVALRECPERIODID, typeof(System.Int32));
            dtExcelData.Columns.Add(ScheduleRecItemUploadConstants.AddedFields.IGNOREINCALCULATION, typeof(System.Boolean));
            dtExcelData.Columns.Add(ScheduleRecItemUploadConstants.AddedFields.CALCULATIONFREQUENCYID, typeof(System.Int16));
            dtExcelData.Columns.Add(ScheduleRecItemUploadConstants.AddedFields.EXRATELCCYTOBCCY, typeof(System.Decimal));
            dtExcelData.Columns.Add(ScheduleRecItemUploadConstants.AddedFields.EXRATELCCYTORCCY, typeof(System.Decimal));
            dtExcelData.Columns.Add(ScheduleRecItemUploadConstants.AddedFields.ISVALIDROW, typeof(System.Boolean));
            dtExcelData.Columns.Add(ScheduleRecItemUploadConstants.AddedFields.TotalInterval, typeof(System.Int32));
            dtExcelData.Columns.Add(ScheduleRecItemUploadConstants.AddedFields.CurrentInterval, typeof(System.Int32));

            for (int x = 0; x < dtExcelData.Rows.Count; x++)
            {
                dtExcelData.Rows[x][ScheduleRecItemUploadConstants.AddedFields.DATAIMPORTID] = oScheduleRecItemImportInfo.DataImportID;
                dtExcelData.Rows[x][ScheduleRecItemUploadConstants.AddedFields.EXCELROWNUMBER] = x + 2;
            }
        }

        #endregion

        #region "Command Methods"

        private static void MapUserAccountInfoObject(SqlDataReader r, List<UserAccountInfo> oUserAccountInfoCollection)
        {
            UserAccountInfo oUserAccountInfo;
            string EmailID = String.Empty;
            try
            {
                int ordinal = r.GetOrdinal("Email");
                if (!r.IsDBNull(ordinal))
                    EmailID = ((string)(r.GetValue(ordinal)));
            }
            catch (Exception) { }


            oUserAccountInfo = (from o in oUserAccountInfoCollection
                                where o.EmailID == EmailID
                                select o).FirstOrDefault();
            if (oUserAccountInfo == null)
            {
                oUserAccountInfo = new UserAccountInfo();
                oUserAccountInfo.EmailID = EmailID;
                oUserAccountInfoCollection.Add(oUserAccountInfo);

            }

            try
            {
                int ordinal = r.GetOrdinal("AccountInfo");
                if (!r.IsDBNull(ordinal))
                {
                    string AcInfo = ((string)(r.GetValue(ordinal)));
                    oUserAccountInfo.AccountInfoCollection.Add(AcInfo);
                }
            }
            catch (Exception) { }
        }

        private void FieldPresent(DataTable oDTExcel)
        {
        }
        #endregion
    }
}
