using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using SkyStem.ART.Client.Model.CompanyDatabase;
using SkyStem.ART.Service.Data;
using SkyStem.ART.Service.Interfaces;
using SkyStem.ART.Service.Model;
using SkyStem.ART.Service.Utility;
using SkyStem.ART.Shared.Data;
using ClientModel = SkyStem.ART.Client.Model;

namespace SkyStem.ART.Service.APP.BLL
{
    public class GLDataImport : IGLDataImport
    {
        private readonly CompanyUserInfo CompanyUserInfo;
        private readonly List<ClientModel.LogInfo> LogInfoCache;

        private GLDataImportInfo oGLDataImportInfo;

        public GLDataImport(CompanyUserInfo oCompanyUserInfo)
        {
            CompanyUserInfo = oCompanyUserInfo;
            LogInfoCache = new List<ClientModel.LogInfo>();
        }

        #region "Public Methods"

        public bool IsProcessingRequiredForGLDataImport()
        {
            var processingRequired = false;
            try
            {
                oGLDataImportInfo = DataImportHelper.GetGLDataImportInfoForProcessing(DateTime.Now, CompanyUserInfo);
                if (oGLDataImportInfo.DataImportID > 0)
                {
                    processingRequired = true;
                    Helper.LogInfo(@"GLData Import required for DataImportID: " + oGLDataImportInfo.DataImportID,
                        CompanyUserInfo);
                }
                else
                {
                    Helper.LogInfo(@"No Data Available for GL Data Import.", CompanyUserInfo);
                }
            }
            catch (Exception ex)
            {
                oGLDataImportInfo = null;
                processingRequired = false;
                Helper.LogError(@"Error in IsProcessingRequiredForGLDataImport: " + ex.Message, CompanyUserInfo);
            }
            return processingRequired;
        }

        public void ProcessGLDataImport()
        {
            try
            {
                Helper.LogInfo(@"Start GLData Import for DataImportID: " + oGLDataImportInfo.DataImportID,
                    CompanyUserInfo);
                if (oGLDataImportInfo.IsDataTransfered)
                    ProcessImportedGLData();
                else
                    ExtractTransferAndProcessData();
            }
            catch (Exception ex)
            {
                DataImportHelper.ResetGLDataHdrObject(oGLDataImportInfo, ex);
                Helper.LogErrorToCache(ex, LogInfoCache);
            }
            finally
            {
                try
                {
                    DataImportHelper.UpdateDataImportHDR(oGLDataImportInfo, CompanyUserInfo);
                }
                catch (Exception ex)
                {
                    Helper.LogErrorToCache("Error while updating DataImportHDR - ", LogInfoCache);
                    Helper.LogErrorToCache(ex, LogInfoCache);
                }
                try
                {
                    oGLDataImportInfo.SuccessEmailIDs =
                        DataImportHelper.GetEmailIDWithSeprator(oGLDataImportInfo.NotifySuccessEmailIds) +
                        DataImportHelper.GetEmailIDWithSeprator(oGLDataImportInfo.NotifySuccessUserEmailIds) +
                        DataImportHelper.GetEmailIDWithSeprator(oGLDataImportInfo.WarningEmailIds);
                    oGLDataImportInfo.FailureEmailIDs =
                        DataImportHelper.GetEmailIDWithSeprator(oGLDataImportInfo.NotifyFailureEmailIds) +
                        DataImportHelper.GetEmailIDWithSeprator(oGLDataImportInfo.NotifyFailureUserEmailIds) +
                        DataImportHelper.GetEmailIDWithSeprator(oGLDataImportInfo.WarningEmailIds);
                    DataImportHelper.SendMailToUsers(oGLDataImportInfo, CompanyUserInfo);
                }
                catch (Exception ex)
                {
                    Helper.LogErrorToCache("Error while sending mail - ", LogInfoCache);
                    Helper.LogErrorToCache(ex, LogInfoCache);
                }
                try
                {
                    Helper.LogListViaService(LogInfoCache, oGLDataImportInfo.DataImportID, CompanyUserInfo);
                    Helper.LogInfo(@"End GLData Import for DataImportID: " + oGLDataImportInfo.DataImportID,
                        CompanyUserInfo);
                }
                catch (Exception ex)
                {
                    Helper.LogError("Error while logging - ", CompanyUserInfo);
                    Helper.LogError(ex, CompanyUserInfo);
                }
            }
        }

        #endregion

        #region "Private Methods"

        private void ExtractTransferAndProcessData()
        {
            DataTable TempdtExcelData = null;
            List<ClientModel.ImportTemplateFieldMappingInfo> oImportTemplateFieldMappingInfoList = null;
            DataTable dtExcelData = null;
            Helper.LogInfoToCache("3. Start Reading Excel file: " + oGLDataImportInfo.PhysicalPath, LogInfoCache);

            //dtExcelData = Helper.GetDataTableFromExcel(oGLDataImportInfo.PhysicalPath, ServiceConstants.GLDATA_SHEETNAME);
            var SheetName = DataImportHelper.GetSheetName(Enums.DataImportType.GLData,
                oGLDataImportInfo.ImportTemplateID, oGLDataImportInfo.CompanyID);
            TempdtExcelData =
                DataImportHelper.GetGLDataImportDataTableFromExcel(oGLDataImportInfo.PhysicalPath, SheetName,
                    CompanyUserInfo);
            if (oGLDataImportInfo != null && oGLDataImportInfo.ImportTemplateID.HasValue &&
                oGLDataImportInfo.ImportTemplateID.Value != Convert.ToInt32(ServiceConstants.ART_TEMPLATE))
            {
                oImportTemplateFieldMappingInfoList =
                    DataImportHelper.GetImportTemplateFieldMappingInfoList(oGLDataImportInfo.ImportTemplateID,
                        oGLDataImportInfo.CompanyID);
                dtExcelData =
                    DataImportHelper.RenameTemplateColumnNameToARTColumns(TempdtExcelData,
                        oImportTemplateFieldMappingInfoList);
            }
            else
            {
                oImportTemplateFieldMappingInfoList =
                    DataImportHelper.GetAllDataImportFieldsWithMapping(oGLDataImportInfo.DataImportID,
                        oGLDataImportInfo.CompanyID);
                dtExcelData = TempdtExcelData;
            }

            if (ValidateSchemaForGLData_New(dtExcelData, oImportTemplateFieldMappingInfoList))
            {
                Helper.LogInfoToCache("4. Reading Excel file complete.", LogInfoCache);

                //Mark Static Field Present
                FieldPresent(dtExcelData);

                //Add additional fields to ExcelDataTabel
                AddDataImportIDToDataTable(dtExcelData);

                //Validate and Convert Data
                ValidateAndConvertData(dtExcelData, oImportTemplateFieldMappingInfoList);
                //  DataImportHelper.RenameAndTrimColumnNames(dtExcelData);
                //Transfer and Process data 
                DataImportHelper.TransferAndProcessGLData(dtExcelData, oGLDataImportInfo, LogInfoCache,
                    CompanyUserInfo);
            }
        }

        private void ProcessImportedGLData()
        {
            DataImportHelper.ProcessTransferedGLData(oGLDataImportInfo, LogInfoCache, CompanyUserInfo);
        }

        private bool ValidateSchemaForGLData_New(DataTable dtExcelData,
            List<ClientModel.ImportTemplateFieldMappingInfo> oImportTemplateFieldMappingInfoList)
        {
            bool isValidSchema;
            bool columnFound;
            var oSbError = new StringBuilder();
            var dtMessage = DataImportHelper.CreateDataImportMandatoryFieldsNotPresentMessageTable();
            var dtWarnningMessage = DataImportHelper.CreateDataImportMandatoryFieldsNotPresentMessageTable();
            var DataImportMessageInfoMandatoryFieldsNotPresent =
                DataImportHelper.GetDataImportMessageInfo((short) Enums.DataImportMessage.MandatoryFieldsNotPresent,
                    CompanyUserInfo.CompanyID);
            var DataImportMessageInfoColumnsForNewAccountCreationNotPresent = DataImportHelper.GetDataImportMessageInfo(
                (short) Enums.DataImportMessage.ColumnsForNewAccountCreationNotPresent, CompanyUserInfo.CompanyID);
            var oDataImportMessageDetailInfoList = new List<ClientModel.DataImportMessageDetailInfo>();
            //Get list of all mandatory fields
            var GLDataImporMandatoryFieldList = DataImportHelper.GetGLDataImportAllMandatoryFields(oGLDataImportInfo);
            var oDataImportHdrInfoBlank = new ClientModel.DataImportHdrInfo();
            var ALLGLDataImporMandatoryFieldList = DataImportHelper.GetAllPossibleGLDataImportFields(oGLDataImportInfo);
            var MandatoryFieldsNotPresentList = new List<string>();


            //Check if all mandatory fields exists in DataTable from Excel
            foreach (var fieldName in GLDataImporMandatoryFieldList)
            {
                columnFound = false;
                for (var i = 0; i < dtExcelData.Columns.Count; i++)
                    if (!string.IsNullOrEmpty(fieldName) && !string.IsNullOrEmpty(dtExcelData.Columns[i].ColumnName) &&
                        fieldName.ToLower().Trim() == dtExcelData.Columns[i].ColumnName.ToLower().Trim())
                    {
                        columnFound = true;
                        break;
                    }
                if (!columnFound)
                {
                    if (!oSbError.ToString().Equals(string.Empty))
                        oSbError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    var oImportTemplateFieldMappingInfo =
                        DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList, fieldName);
                    if (oImportTemplateFieldMappingInfo != null)
                    {
                        var ImportTemplateFieldName = DataImportHelper.GetImportTemplateFieldName(
                            oImportTemplateFieldMappingInfo, oGLDataImportInfo.LanguageID,
                            oGLDataImportInfo.DefaultLanguageID, CompanyUserInfo);
                        oSbError.Append(ImportTemplateFieldName);

                        var drMessage = dtMessage.NewRow();
                        if (oImportTemplateFieldMappingInfo.ImportFieldID.HasValue)
                            drMessage[DataImportMessageConstants.Fields.ImportFieldID] =
                                oImportTemplateFieldMappingInfo.ImportFieldID.Value;
                        drMessage[DataImportMessageConstants.Fields.ImportField] = ImportTemplateFieldName;
                        drMessage[DataImportMessageConstants.Fields.MessageLabelID] =
                            DataImportMessageInfoMandatoryFieldsNotPresent.DataImportMessageLabelID;
                        dtMessage.Rows.Add(drMessage);
                        MandatoryFieldsNotPresentList.Add(ImportTemplateFieldName);
                    }
                    //oSbError.Append(fieldName);
                }
            }
            isValidSchema = string.IsNullOrEmpty(oSbError.ToString());


            //Check if All Possible GLDataImport Fields  exists in DataTable from Excel
            foreach (var fieldName in ALLGLDataImporMandatoryFieldList)
            {
                columnFound = false;
                for (var i = 0; i < dtExcelData.Columns.Count; i++)
                    if (!string.IsNullOrEmpty(fieldName) && !string.IsNullOrEmpty(dtExcelData.Columns[i].ColumnName) &&
                        fieldName.ToLower().Trim() == dtExcelData.Columns[i].ColumnName.ToLower().Trim())
                    {
                        columnFound = true;
                        break;
                    }
                if (!columnFound)
                {
                    var oImportTemplateFieldMappingInfo =
                        DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList, fieldName);
                    if (oImportTemplateFieldMappingInfo != null)
                    {
                        var ImportTemplateFieldName = DataImportHelper.GetImportTemplateFieldName(
                            oImportTemplateFieldMappingInfo, oGLDataImportInfo.LanguageID,
                            oGLDataImportInfo.DefaultLanguageID, CompanyUserInfo);
                        if (!MandatoryFieldsNotPresentList.Contains(ImportTemplateFieldName))
                        {
                            var drMessage = dtWarnningMessage.NewRow();
                            if (oImportTemplateFieldMappingInfo.ImportFieldID.HasValue)
                                drMessage[DataImportMessageConstants.Fields.ImportFieldID] =
                                    oImportTemplateFieldMappingInfo.ImportFieldID.Value;
                            drMessage[DataImportMessageConstants.Fields.ImportField] = ImportTemplateFieldName;
                            drMessage[DataImportMessageConstants.Fields.MessageLabelID] =
                                DataImportMessageInfoColumnsForNewAccountCreationNotPresent.DataImportMessageLabelID;
                            dtWarnningMessage.Rows.Add(drMessage);
                        }
                    }
                }
            }
            if (dtWarnningMessage.Rows.Count > 0)
            {
                var oDataImportMessageDetailInfo = new ClientModel.DataImportMessageDetailInfo();
                oDataImportMessageDetailInfo.DataImportMessageTypeID =
                    (short) DataImportMessageInfoColumnsForNewAccountCreationNotPresent.DataImportMessageTypeID;
                oDataImportMessageDetailInfo.DataImportMessageID =
                    DataImportMessageInfoColumnsForNewAccountCreationNotPresent.DataImportMessageID;
                oDataImportMessageDetailInfo.DataImportMessageLabelID =
                    DataImportMessageInfoColumnsForNewAccountCreationNotPresent.DataImportMessageLabelID;
                var ds = new DataSet();
                ds.Tables.Add(dtWarnningMessage);
                oDataImportMessageDetailInfo.MessageSchema = ds.GetXmlSchema();
                oDataImportMessageDetailInfo.MessageData = ds.GetXml();
                oDataImportMessageDetailInfoList.Add(oDataImportMessageDetailInfo);
            }


            //If schema is not valid, generate a multi lingual error message, set failure status, faliure status ID, error message 
            //in GLDataImport object and throw an exception with generated message 
            if (!isValidSchema)
            {
                if (dtMessage.Rows.Count > 0)
                {
                    var oDataImportMessageDetailInfo = new ClientModel.DataImportMessageDetailInfo();
                    oDataImportMessageDetailInfo.DataImportMessageTypeID =
                        (short) DataImportMessageInfoMandatoryFieldsNotPresent.DataImportMessageTypeID;
                    oDataImportMessageDetailInfo.DataImportMessageID =
                        DataImportMessageInfoMandatoryFieldsNotPresent.DataImportMessageID;
                    oDataImportMessageDetailInfo.DataImportMessageLabelID =
                        DataImportMessageInfoMandatoryFieldsNotPresent.DataImportMessageLabelID;
                    var ds = new DataSet();
                    ds.Tables.Add(dtMessage);
                    oDataImportMessageDetailInfo.MessageSchema = ds.GetXmlSchema();
                    oDataImportMessageDetailInfo.MessageData = ds.GetXml();
                    oDataImportMessageDetailInfoList.Add(oDataImportMessageDetailInfo);
                }
                var errorMessage = Helper.GetSinglePhrase(5000165, 0, oGLDataImportInfo.LanguageID,
                    oGLDataImportInfo.DefaultLanguageID, CompanyUserInfo); //Mandatory columns not present: {0}

                oGLDataImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
                oGLDataImportInfo.DataImportStatusID = (short) Enums.DataImportStatus.Failure;
                oGLDataImportInfo.ErrorMessageToSave = string.Format(errorMessage, oSbError);
                oGLDataImportInfo.DataImportMessageDetailInfoList = oDataImportMessageDetailInfoList;
                throw new Exception(string.Format(errorMessage, oSbError));
            }
            if (oDataImportMessageDetailInfoList != null && oDataImportMessageDetailInfoList.Count > 0)
                oGLDataImportInfo.DataImportMessageDetailInfoList = oDataImportMessageDetailInfoList;

            return isValidSchema;
        }

        private bool ValidateSchemaForGLData(DataTable dtExcelData)
        {
            bool isValidSchema;
            var oSbError = new StringBuilder();

            //Get a List of GL Data Import Mandatory Fields
            var staticFieldList = Helper.GetGLDataImportMandatoryFields();

            //Get a List of Key Fields
            var keyFieldList = oGLDataImportInfo.KeyFields.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            //Define a new resultant list which will contain both above lists
            var allMandatoryFieldsList = new List<string>();

            //Copy Mandatory fields to Resultant List
            allMandatoryFieldsList.AddRange(staticFieldList);

            //Copy Key fields list to resultant list
            allMandatoryFieldsList.AddRange(keyFieldList);


            var columnName = "";
            int? columnIndex = null;

            //Run a loop on resultant List. If ColumnName is found in first row of ExcelDataTabel, Rename that ExcelDataSet Column
            //else store that field name in a stringbuilder which will be used later to format exception message
            foreach (var fieldName in allMandatoryFieldsList)
            {
                columnIndex = null;
                for (var j = 0; j < dtExcelData.Columns.Count; j++)
                {
                    columnName = dtExcelData.Rows[0][j].ToString().Trim();
                    if (columnName == fieldName)
                    {
                        columnIndex = j;
                        break;
                    }
                }
                if (columnIndex != null)
                {
                    dtExcelData.Columns[columnIndex.Value].ColumnName = fieldName;
                }
                else
                {
                    if (!oSbError.ToString().Equals(string.Empty))
                        oSbError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    oSbError.Append(fieldName);
                }
            }
            //If stringbuilder object is empty, it is valid schema
            isValidSchema = string.IsNullOrEmpty(oSbError.ToString());
            if (!isValidSchema)
            {
                var errorMessage = Helper.GetSinglePhrase(5000165, 0, oGLDataImportInfo.LanguageID,
                    oGLDataImportInfo.DefaultLanguageID, CompanyUserInfo); //Mandatory columns not present: {0}

                oGLDataImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
                oGLDataImportInfo.DataImportStatusID = (short) Enums.DataImportStatus.Failure;
                oGLDataImportInfo.ErrorMessageToSave = string.Format(errorMessage, oSbError);
                throw new Exception(string.Format(errorMessage, oSbError));
            }
            return isValidSchema;
        }

        private void ValidateAndConvertData(DataTable dtExcelData,
            List<ClientModel.ImportTemplateFieldMappingInfo> oImportTemplateFieldMappingInfoList)
        {
            var oSBError = new StringBuilder();
            var msg = Helper.GetDataLengthErrorMessage(ServiceConstants.DEFAULTBUSINESSENTITYID,
                oGLDataImportInfo.LanguageID, oGLDataImportInfo.DefaultLanguageID, CompanyUserInfo);
            var InvalidDataMsg = Helper.GetInvalidDataErrorMessage(ServiceConstants.DEFAULTBUSINESSENTITYID,
                oGLDataImportInfo.LanguageID, oGLDataImportInfo.DefaultLanguageID, CompanyUserInfo);
            var oDataImportMessageDetailInfoList = new List<ClientModel.DataImportMessageDetailInfo>();
            var DataImportMessageDataLengthExceeded =
                DataImportHelper.GetDataImportMessageInfo((short) Enums.DataImportMessage.DataLengthExceeded,
                    CompanyUserInfo.CompanyID);
            var DataImportMessageInfoInvalidValue =
                DataImportHelper.GetDataImportMessageInfo((short) Enums.DataImportMessage.InvalidValue,
                    CompanyUserInfo.CompanyID);
            var DataImportMessageInfoNoDataForMandatoryField =
                DataImportHelper.GetDataImportMessageInfo((short) Enums.DataImportMessage.NoDataForMandatoryField,
                    CompanyUserInfo.CompanyID);
            for (var x = 0; x < dtExcelData.Rows.Count; x++)
            {
                var dtMessage = DataImportHelper.CreateDataImportMessageTable();
                var dtMessageNoDataForMandatoryField =
                    DataImportHelper.CreateDataImportMandatoryFieldsNotPresentMessageTable();
                var dtMessageInvalidValue = DataImportHelper.CreateDataImportMandatoryFieldsNotPresentMessageTable();

                var dr = dtExcelData.Rows[x];
                var excelRowNumber = dr[AddedGLDataImportFields.EXCELROWNUMBER].ToString();

                if (oGLDataImportInfo.IsFSCaptionFieldAvailable)
                {
                    if (dr[GLDataImportFields.FSCAPTION] != DBNull.Value)
                        dr[GLDataImportFields.FSCAPTION] = dr[GLDataImportFields.FSCAPTION].ToString().Trim();
                    if (dr[GLDataImportFields.FSCAPTION].ToString().Length >
                        (int) Enums.DataImportFieldsMaxLength.FSCaption)
                    {
                        var oImportTemplateFieldMappingInfo =
                            DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList,
                                GLDataImportFields.FSCAPTION);
                        if (oImportTemplateFieldMappingInfo != null)
                        {
                            var ImportTemplateFieldName = DataImportHelper.GetImportTemplateFieldName(
                                oImportTemplateFieldMappingInfo, oGLDataImportInfo.LanguageID,
                                oGLDataImportInfo.DefaultLanguageID, CompanyUserInfo);
                            oSBError.Append(string.Format(msg, ImportTemplateFieldName, excelRowNumber,
                                (int) Enums.DataImportFieldsMaxLength.FSCaption));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            var drMessage = dtMessage.NewRow();
                            if (oImportTemplateFieldMappingInfo.ImportFieldID.HasValue)
                                drMessage[DataImportMessageConstants.Fields.ImportFieldID] =
                                    oImportTemplateFieldMappingInfo.ImportFieldID.Value;
                            drMessage[DataImportMessageConstants.Fields.ImportField] = ImportTemplateFieldName;
                            //if (oImportTemplateFieldMappingInfo.MessageLabelID.HasValue)
                            //    drMessage[DataImportMessageConstants.Fields.MessageLabelID] = oImportTemplateFieldMappingInfo.MessageLabelID.Value;
                            //else
                            drMessage[DataImportMessageConstants.Fields.MessageLabelID] =
                                DataImportMessageDataLengthExceeded.DataImportMessageLabelID;
                            drMessage[DataImportMessageConstants.Fields.Allowed] =
                                (int) Enums.DataImportFieldsMaxLength.FSCaption;
                            drMessage[DataImportMessageConstants.Fields.Actual] =
                                dr[GLDataImportFields.FSCAPTION].ToString().Length;
                            dtMessage.Rows.Add(drMessage);
                        }
                    }
                }

                if (oGLDataImportInfo.IsProfitAndLossAvailable)
                {
                    if (dr[GLDataImportFields.ISPROFITANDLOSS] != DBNull.Value)
                        dr[GLDataImportFields.ISPROFITANDLOSS] =
                            dr[GLDataImportFields.ISPROFITANDLOSS].ToString().Trim();
                    if (dr[GLDataImportFields.ISPROFITANDLOSS].ToString().Length >
                        (int) Enums.DataImportFieldsMaxLength.IsProfitAndLoss)
                    {
                        var oImportTemplateFieldMappingInfo =
                            DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList,
                                GLDataImportFields.ISPROFITANDLOSS);
                        if (oImportTemplateFieldMappingInfo != null)
                        {
                            var ImportTemplateFieldName = DataImportHelper.GetImportTemplateFieldName(
                                oImportTemplateFieldMappingInfo, oGLDataImportInfo.LanguageID,
                                oGLDataImportInfo.DefaultLanguageID, CompanyUserInfo);
                            oSBError.Append(string.Format(msg, ImportTemplateFieldName, excelRowNumber,
                                (int) Enums.DataImportFieldsMaxLength.IsProfitAndLoss));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            var drMessage = dtMessage.NewRow();
                            if (oImportTemplateFieldMappingInfo.ImportFieldID.HasValue)
                                drMessage[DataImportMessageConstants.Fields.ImportFieldID] =
                                    oImportTemplateFieldMappingInfo.ImportFieldID.Value;
                            drMessage[DataImportMessageConstants.Fields.ImportField] = ImportTemplateFieldName;
                            //if (oImportTemplateFieldMappingInfo.MessageLabelID.HasValue)
                            //    drMessage[DataImportMessageConstants.Fields.MessageLabelID] = oImportTemplateFieldMappingInfo.MessageLabelID.Value;
                            //else
                            drMessage[DataImportMessageConstants.Fields.MessageLabelID] =
                                DataImportMessageDataLengthExceeded.DataImportMessageLabelID;
                            drMessage[DataImportMessageConstants.Fields.Allowed] =
                                (int) Enums.DataImportFieldsMaxLength.IsProfitAndLoss;
                            drMessage[DataImportMessageConstants.Fields.Actual] =
                                dr[GLDataImportFields.ISPROFITANDLOSS].ToString().Length;
                            dtMessage.Rows.Add(drMessage);
                        }
                    }
                }

                if (oGLDataImportInfo.IsAccountNameFieldAvailable)
                {
                    if (dr[GLDataImportFields.GLACCOUNTNAME] != DBNull.Value)
                        dr[GLDataImportFields.GLACCOUNTNAME] = dr[GLDataImportFields.GLACCOUNTNAME].ToString().Trim();
                    if (dr[GLDataImportFields.GLACCOUNTNAME].ToString().Length >
                        (int) Enums.DataImportFieldsMaxLength.AccountName)
                    {
                        var oImportTemplateFieldMappingInfo =
                            DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList,
                                GLDataImportFields.GLACCOUNTNAME);
                        if (oImportTemplateFieldMappingInfo != null)
                        {
                            var ImportTemplateFieldName = DataImportHelper.GetImportTemplateFieldName(
                                oImportTemplateFieldMappingInfo, oGLDataImportInfo.LanguageID,
                                oGLDataImportInfo.DefaultLanguageID, CompanyUserInfo);
                            oSBError.Append(string.Format(msg, ImportTemplateFieldName, excelRowNumber,
                                (int) Enums.DataImportFieldsMaxLength.AccountName));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            var drMessage = dtMessage.NewRow();
                            if (oImportTemplateFieldMappingInfo.ImportFieldID.HasValue)
                                drMessage[DataImportMessageConstants.Fields.ImportFieldID] =
                                    oImportTemplateFieldMappingInfo.ImportFieldID.Value;
                            drMessage[DataImportMessageConstants.Fields.ImportField] = ImportTemplateFieldName;
                            //if (oImportTemplateFieldMappingInfo.MessageLabelID.HasValue)
                            //    drMessage[DataImportMessageConstants.Fields.MessageLabelID] = oImportTemplateFieldMappingInfo.MessageLabelID.Value;
                            //else
                            drMessage[DataImportMessageConstants.Fields.MessageLabelID] =
                                DataImportMessageDataLengthExceeded.DataImportMessageLabelID;
                            drMessage[DataImportMessageConstants.Fields.Allowed] =
                                (int) Enums.DataImportFieldsMaxLength.AccountName;
                            drMessage[DataImportMessageConstants.Fields.Actual] =
                                dr[GLDataImportFields.GLACCOUNTNAME].ToString().Length;
                            dtMessage.Rows.Add(drMessage);
                        }
                    }
                }

                if (oGLDataImportInfo.IsAccountNumberFieldAvailable)
                {
                    if (dr[GLDataImportFields.GLACCOUNTNUMBER] != DBNull.Value)
                        dr[GLDataImportFields.GLACCOUNTNUMBER] =
                            dr[GLDataImportFields.GLACCOUNTNUMBER].ToString().Trim();
                    if (dr[GLDataImportFields.GLACCOUNTNUMBER].ToString().Length >
                        (int) Enums.DataImportFieldsMaxLength.AccountNumber)
                    {
                        var oImportTemplateFieldMappingInfo =
                            DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList,
                                GLDataImportFields.GLACCOUNTNUMBER);
                        if (oImportTemplateFieldMappingInfo != null)
                        {
                            var ImportTemplateFieldName = DataImportHelper.GetImportTemplateFieldName(
                                oImportTemplateFieldMappingInfo, oGLDataImportInfo.LanguageID,
                                oGLDataImportInfo.DefaultLanguageID, CompanyUserInfo);
                            oSBError.Append(string.Format(msg, ImportTemplateFieldName, excelRowNumber,
                                (int) Enums.DataImportFieldsMaxLength.AccountNumber));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            var drMessage = dtMessage.NewRow();
                            if (oImportTemplateFieldMappingInfo.ImportFieldID.HasValue)
                                drMessage[DataImportMessageConstants.Fields.ImportFieldID] =
                                    oImportTemplateFieldMappingInfo.ImportFieldID.Value;
                            drMessage[DataImportMessageConstants.Fields.ImportField] = ImportTemplateFieldName;
                            //if (oImportTemplateFieldMappingInfo.MessageLabelID.HasValue)
                            //    drMessage[DataImportMessageConstants.Fields.MessageLabelID] = oImportTemplateFieldMappingInfo.MessageLabelID.Value;
                            //else
                            drMessage[DataImportMessageConstants.Fields.MessageLabelID] =
                                DataImportMessageDataLengthExceeded.DataImportMessageLabelID;
                            drMessage[DataImportMessageConstants.Fields.Allowed] =
                                (int) Enums.DataImportFieldsMaxLength.AccountNumber;
                            drMessage[DataImportMessageConstants.Fields.Actual] =
                                dr[GLDataImportFields.GLACCOUNTNUMBER].ToString().Length;
                            dtMessage.Rows.Add(drMessage);
                        }
                    }
                }

                //Keyfields
                var arrKeyFields =
                    oGLDataImportInfo.KeyFields.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
                for (var k = 0; k < arrKeyFields.Length; k++)
                {
                    var sourceField = arrKeyFields[k];
                    if (dtExcelData.Columns.Contains(sourceField))
                    {
                        if (dr[sourceField] != DBNull.Value)
                            dr[sourceField] = dr[sourceField].ToString().Trim();
                        if (dr[sourceField].ToString().Length > (int) Enums.DataImportFieldsMaxLength.KeyFields)
                        {
                            var oImportTemplateFieldMappingInfo =
                                DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList,
                                    sourceField);
                            if (oImportTemplateFieldMappingInfo != null)
                            {
                                var ImportTemplateFieldName =
                                    DataImportHelper.GetImportTemplateFieldName(oImportTemplateFieldMappingInfo,
                                        oGLDataImportInfo.LanguageID, oGLDataImportInfo.DefaultLanguageID,
                                        CompanyUserInfo);
                                oSBError.Append(string.Format(msg, ImportTemplateFieldName, excelRowNumber,
                                    (int) Enums.DataImportFieldsMaxLength.KeyFields));
                                oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                                var drMessage = dtMessage.NewRow();
                                if (oImportTemplateFieldMappingInfo.ImportFieldID.HasValue)
                                    drMessage[DataImportMessageConstants.Fields.ImportFieldID] =
                                        oImportTemplateFieldMappingInfo.ImportFieldID.Value;
                                drMessage[DataImportMessageConstants.Fields.ImportField] = ImportTemplateFieldName;
                                //if (oImportTemplateFieldMappingInfo.MessageLabelID.HasValue)
                                //    drMessage[DataImportMessageConstants.Fields.MessageLabelID] = oImportTemplateFieldMappingInfo.MessageLabelID.Value;
                                //else
                                drMessage[DataImportMessageConstants.Fields.MessageLabelID] =
                                    DataImportMessageDataLengthExceeded.DataImportMessageLabelID;
                                drMessage[DataImportMessageConstants.Fields.Allowed] =
                                    (int) Enums.DataImportFieldsMaxLength.KeyFields;
                                drMessage[DataImportMessageConstants.Fields.Actual] = dr[sourceField].ToString().Length;
                                dtMessage.Rows.Add(drMessage);
                            }
                        }
                    }
                }
                // Invalid Data Validations
                DateTime periodEndDate;
                if (Helper.IsValidDateTime(dr[GLDataImportFields.PERIODENDDATE].ToString(),
                    oGLDataImportInfo.LanguageID, out periodEndDate))
                {
                    dr[GLDataImportFields.PERIODENDDATE] = periodEndDate.ToShortDateString();
                    dr[AddedGLDataImportFields.RECPERIODENDDATE] = periodEndDate.ToShortDateString();
                }
                else if (string.IsNullOrEmpty(Convert.ToString(dr[GLDataImportFields.PERIODENDDATE])))
                {
                    var oImportTemplateFieldMappingInfo =
                        DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList,
                            GLDataImportFields.PERIODENDDATE);
                    if (oImportTemplateFieldMappingInfo != null)
                    {
                        var ImportTemplateFieldName = DataImportHelper.GetImportTemplateFieldName(
                            oImportTemplateFieldMappingInfo, oGLDataImportInfo.LanguageID,
                            oGLDataImportInfo.DefaultLanguageID, CompanyUserInfo);
                        oSBError.Append(string.Format(InvalidDataMsg, ImportTemplateFieldName, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        var drMessage = dtMessageNoDataForMandatoryField.NewRow();
                        if (oImportTemplateFieldMappingInfo.ImportFieldID.HasValue)
                            drMessage[DataImportMessageConstants.Fields.ImportFieldID] =
                                oImportTemplateFieldMappingInfo.ImportFieldID.Value;
                        drMessage[DataImportMessageConstants.Fields.ImportField] = ImportTemplateFieldName;
                        drMessage[DataImportMessageConstants.Fields.MessageLabelID] =
                            DataImportMessageInfoNoDataForMandatoryField.DataImportMessageLabelID;
                        dtMessageNoDataForMandatoryField.Rows.Add(drMessage);
                    }
                }
                else
                {
                    var oImportTemplateFieldMappingInfo =
                        DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList,
                            GLDataImportFields.PERIODENDDATE);
                    if (oImportTemplateFieldMappingInfo != null)
                    {
                        var ImportTemplateFieldName = DataImportHelper.GetImportTemplateFieldName(
                            oImportTemplateFieldMappingInfo, oGLDataImportInfo.LanguageID,
                            oGLDataImportInfo.DefaultLanguageID, CompanyUserInfo);
                        oSBError.Append(string.Format(InvalidDataMsg, ImportTemplateFieldName, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        var drMessage = dtMessageInvalidValue.NewRow();
                        if (oImportTemplateFieldMappingInfo.ImportFieldID.HasValue)
                            drMessage[DataImportMessageConstants.Fields.ImportFieldID] =
                                oImportTemplateFieldMappingInfo.ImportFieldID.Value;
                        drMessage[DataImportMessageConstants.Fields.ImportField] = ImportTemplateFieldName;
                        drMessage[DataImportMessageConstants.Fields.MessageLabelID] =
                            DataImportMessageInfoInvalidValue.DataImportMessageLabelID;
                        dtMessageInvalidValue.Rows.Add(drMessage);
                    }
                }

                decimal BalBCCY = 0;
                if (Helper.IsValidDecimal(dr[GLDataImportFields.BALANCEBCCY].ToString(), oGLDataImportInfo.LanguageID,
                    out BalBCCY))
                {
                    dr[GLDataImportFields.BALANCEBCCY] = BalBCCY.ToString();
                }
                else if (string.IsNullOrEmpty(Convert.ToString(dr[GLDataImportFields.BALANCEBCCY])))
                {
                    var oImportTemplateFieldMappingInfo =
                        DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList,
                            GLDataImportFields.BALANCEBCCY);
                    if (oImportTemplateFieldMappingInfo != null)
                    {
                        var ImportTemplateFieldName = DataImportHelper.GetImportTemplateFieldName(
                            oImportTemplateFieldMappingInfo, oGLDataImportInfo.LanguageID,
                            oGLDataImportInfo.DefaultLanguageID, CompanyUserInfo);
                        oSBError.Append(string.Format(InvalidDataMsg,
                            DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList,
                                GLDataImportFields.BALANCEBCCY), excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        var drMessage = dtMessageNoDataForMandatoryField.NewRow();
                        if (oImportTemplateFieldMappingInfo.ImportFieldID.HasValue)
                            drMessage[DataImportMessageConstants.Fields.ImportFieldID] =
                                oImportTemplateFieldMappingInfo.ImportFieldID.Value;
                        drMessage[DataImportMessageConstants.Fields.ImportField] = ImportTemplateFieldName;
                        drMessage[DataImportMessageConstants.Fields.MessageLabelID] =
                            DataImportMessageInfoNoDataForMandatoryField.DataImportMessageLabelID;
                        dtMessageNoDataForMandatoryField.Rows.Add(drMessage);
                    }
                }
                else
                {
                    var oImportTemplateFieldMappingInfo =
                        DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList,
                            GLDataImportFields.BALANCEBCCY);
                    if (oImportTemplateFieldMappingInfo != null)
                    {
                        var ImportTemplateFieldName = DataImportHelper.GetImportTemplateFieldName(
                            oImportTemplateFieldMappingInfo, oGLDataImportInfo.LanguageID,
                            oGLDataImportInfo.DefaultLanguageID, CompanyUserInfo);
                        oSBError.Append(string.Format(InvalidDataMsg,
                            DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList,
                                GLDataImportFields.BALANCEBCCY), excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        var drMessage = dtMessageInvalidValue.NewRow();
                        if (oImportTemplateFieldMappingInfo.ImportFieldID.HasValue)
                            drMessage[DataImportMessageConstants.Fields.ImportFieldID] =
                                oImportTemplateFieldMappingInfo.ImportFieldID.Value;
                        drMessage[DataImportMessageConstants.Fields.ImportField] = ImportTemplateFieldName;
                        drMessage[DataImportMessageConstants.Fields.MessageLabelID] =
                            DataImportMessageInfoInvalidValue.DataImportMessageLabelID;
                        dtMessageInvalidValue.Rows.Add(drMessage);
                    }
                }

                decimal BalRCCY = 0;
                if (Helper.IsValidDecimal(dr[GLDataImportFields.BALANCERCCY].ToString(), oGLDataImportInfo.LanguageID,
                    out BalRCCY))
                {
                    dr[GLDataImportFields.BALANCERCCY] = BalRCCY.ToString();
                }
                else if (string.IsNullOrEmpty(Convert.ToString(dr[GLDataImportFields.BALANCERCCY])))
                {
                    var oImportTemplateFieldMappingInfo =
                        DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList,
                            GLDataImportFields.BALANCERCCY);
                    if (oImportTemplateFieldMappingInfo != null)
                    {
                        var ImportTemplateFieldName = DataImportHelper.GetImportTemplateFieldName(
                            oImportTemplateFieldMappingInfo, oGLDataImportInfo.LanguageID,
                            oGLDataImportInfo.DefaultLanguageID, CompanyUserInfo);
                        oSBError.Append(string.Format(InvalidDataMsg, ImportTemplateFieldName, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        var drMessage = dtMessageNoDataForMandatoryField.NewRow();
                        if (oImportTemplateFieldMappingInfo.ImportFieldID.HasValue)
                            drMessage[DataImportMessageConstants.Fields.ImportFieldID] =
                                oImportTemplateFieldMappingInfo.ImportFieldID.Value;
                        drMessage[DataImportMessageConstants.Fields.ImportField] = ImportTemplateFieldName;
                        drMessage[DataImportMessageConstants.Fields.MessageLabelID] =
                            DataImportMessageInfoNoDataForMandatoryField.DataImportMessageLabelID;
                        dtMessageNoDataForMandatoryField.Rows.Add(drMessage);
                    }
                }
                else
                {
                    var oImportTemplateFieldMappingInfo =
                        DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList,
                            GLDataImportFields.BALANCERCCY);
                    if (oImportTemplateFieldMappingInfo != null)
                    {
                        var ImportTemplateFieldName = DataImportHelper.GetImportTemplateFieldName(
                            oImportTemplateFieldMappingInfo, oGLDataImportInfo.LanguageID,
                            oGLDataImportInfo.DefaultLanguageID, CompanyUserInfo);
                        oSBError.Append(string.Format(InvalidDataMsg, ImportTemplateFieldName, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        var drMessage = dtMessageInvalidValue.NewRow();
                        if (oImportTemplateFieldMappingInfo.ImportFieldID.HasValue)
                            drMessage[DataImportMessageConstants.Fields.ImportFieldID] =
                                oImportTemplateFieldMappingInfo.ImportFieldID.Value;
                        drMessage[DataImportMessageConstants.Fields.ImportField] = ImportTemplateFieldName;
                        drMessage[DataImportMessageConstants.Fields.MessageLabelID] =
                            DataImportMessageInfoInvalidValue.DataImportMessageLabelID;
                        dtMessageInvalidValue.Rows.Add(drMessage);
                    }
                }

                if (dtMessage.Rows.Count > 0)
                {
                    var oDataImportMessageDetailInfo = new ClientModel.DataImportMessageDetailInfo();
                    oDataImportMessageDetailInfo.ExcelRowNumber = Convert.ToInt32(excelRowNumber);
                    oDataImportMessageDetailInfo.DataImportMessageTypeID =
                        (short) DataImportMessageDataLengthExceeded.DataImportMessageTypeID;
                    oDataImportMessageDetailInfo.DataImportMessageID =
                        DataImportMessageDataLengthExceeded.DataImportMessageID;
                    oDataImportMessageDetailInfo.DataImportMessageLabelID =
                        DataImportMessageDataLengthExceeded.DataImportMessageLabelID;
                    var ds = new DataSet();
                    ds.Tables.Add(dtMessage);
                    oDataImportMessageDetailInfo.MessageSchema = ds.GetXmlSchema();
                    oDataImportMessageDetailInfo.MessageData = ds.GetXml();
                    oDataImportMessageDetailInfoList.Add(oDataImportMessageDetailInfo);
                }
                if (dtMessageNoDataForMandatoryField.Rows.Count > 0)
                {
                    var oDataImportMessageDetailInfo = new ClientModel.DataImportMessageDetailInfo();
                    oDataImportMessageDetailInfo.ExcelRowNumber = Convert.ToInt32(excelRowNumber);
                    oDataImportMessageDetailInfo.DataImportMessageTypeID =
                        (short) DataImportMessageInfoNoDataForMandatoryField.DataImportMessageTypeID;
                    oDataImportMessageDetailInfo.DataImportMessageID =
                        DataImportMessageInfoNoDataForMandatoryField.DataImportMessageID;
                    oDataImportMessageDetailInfo.DataImportMessageLabelID =
                        DataImportMessageInfoNoDataForMandatoryField.DataImportMessageLabelID;
                    var ds = new DataSet();
                    ds.Tables.Add(dtMessageNoDataForMandatoryField);
                    oDataImportMessageDetailInfo.MessageSchema = ds.GetXmlSchema();
                    oDataImportMessageDetailInfo.MessageData = ds.GetXml();
                    oDataImportMessageDetailInfoList.Add(oDataImportMessageDetailInfo);
                }
                if (dtMessageInvalidValue.Rows.Count > 0)
                {
                    var oDataImportMessageDetailInfo = new ClientModel.DataImportMessageDetailInfo();
                    oDataImportMessageDetailInfo.ExcelRowNumber = Convert.ToInt32(excelRowNumber);
                    oDataImportMessageDetailInfo.DataImportMessageTypeID =
                        (short) DataImportMessageInfoInvalidValue.DataImportMessageTypeID;
                    oDataImportMessageDetailInfo.DataImportMessageID =
                        DataImportMessageInfoInvalidValue.DataImportMessageID;
                    oDataImportMessageDetailInfo.DataImportMessageLabelID =
                        DataImportMessageInfoInvalidValue.DataImportMessageLabelID;
                    var ds = new DataSet();
                    ds.Tables.Add(dtMessageInvalidValue);
                    oDataImportMessageDetailInfo.MessageSchema = ds.GetXmlSchema();
                    oDataImportMessageDetailInfo.MessageData = ds.GetXml();
                    oDataImportMessageDetailInfoList.Add(oDataImportMessageDetailInfo);
                }
            }
            if (!oSBError.ToString().Equals(string.Empty))
            {
                oGLDataImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
                oGLDataImportInfo.DataImportStatusID = (short) Enums.DataImportStatus.Failure;
                oGLDataImportInfo.ErrorMessageToSave = oSBError.ToString();
                oGLDataImportInfo.DataImportMessageDetailInfoList = oDataImportMessageDetailInfoList;
                throw new Exception(oSBError.ToString());
            }
        }


        private void AddDataImportIDToDataTable(DataTable dtExcelData)
        {
            dtExcelData.Columns.Add(AddedGLDataImportFields.DATAIMPORTID, typeof(int));
            dtExcelData.Columns.Add(AddedGLDataImportFields.EXCELROWNUMBER, typeof(int));
            dtExcelData.Columns.Add(AddedGLDataImportFields.RECPERIODENDDATE, typeof(string));

            //DateTime dtPeriodEndDate = new DateTime();

            for (var x = 0; x < dtExcelData.Rows.Count; x++)
            {
                dtExcelData.Rows[x][AddedGLDataImportFields.DATAIMPORTID] = oGLDataImportInfo.DataImportID;
                dtExcelData.Rows[x][AddedGLDataImportFields.EXCELROWNUMBER] = x + 2;
                //if (DateTime.TryParse(dtExcelData.Rows[x][GLDataImportFields.PERIODENDDATE].ToString(), out dtPeriodEndDate))
                //    dtExcelData.Rows[x][AddedGLDataImportFields.RECPERIODENDDATE] = dtPeriodEndDate.ToShortDateString();
                //dtExcelData.Rows[x]["RecPeriodEndDate"] = Convert.ToDateTime(dtPeriodEndDate.ToShortDateString());
            }
        }

        #endregion

        #region "Command Methods"

        private static void MapUserAccountInfoObject(SqlDataReader r,
            List<ClientModel.UserAccountInfo> oUserAccountInfoCollection)
        {
            ClientModel.UserAccountInfo oUserAccountInfo;
            var EmailID = string.Empty;
            try
            {
                var ordinal = r.GetOrdinal("Email");
                if (!r.IsDBNull(ordinal))
                    EmailID = (string) r.GetValue(ordinal);
            }
            catch (Exception)
            {
            }


            oUserAccountInfo = (from o in oUserAccountInfoCollection
                where o.EmailID == EmailID
                select o).FirstOrDefault();
            if (oUserAccountInfo == null)
            {
                oUserAccountInfo = new ClientModel.UserAccountInfo();
                oUserAccountInfo.EmailID = EmailID;
                oUserAccountInfoCollection.Add(oUserAccountInfo);
            }

            try
            {
                var ordinal = r.GetOrdinal("AccountInfo");
                if (!r.IsDBNull(ordinal))
                {
                    var AcInfo = (string) r.GetValue(ordinal);
                    oUserAccountInfo.AccountInfoCollection.Add(AcInfo);
                }
            }
            catch (Exception)
            {
            }
        }

        private void FieldPresent(DataTable oDTExcel)
        {
            oGLDataImportInfo.IsFSCaptionFieldAvailable = oDTExcel.Columns.Contains(GLDataImportFields.FSCAPTION);
            oGLDataImportInfo.IsAccountNameFieldAvailable = oDTExcel.Columns.Contains(GLDataImportFields.GLACCOUNTNAME);
            oGLDataImportInfo.IsAccountNumberFieldAvailable =
                oDTExcel.Columns.Contains(GLDataImportFields.GLACCOUNTNUMBER);
            oGLDataImportInfo.IsAccountTypeFieldAvailable = oDTExcel.Columns.Contains(GLDataImportFields.ACCOUNTTYPE);
            oGLDataImportInfo.IsProfitAndLossAvailable = oDTExcel.Columns.Contains(GLDataImportFields.ISPROFITANDLOSS);
        }

        #endregion
    }
}