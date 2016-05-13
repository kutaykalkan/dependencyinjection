using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;
using SkyStem.ART.Service.DAO;
using SkyStem.ART.Service.Data;
using SkyStem.ART.Service.Utility;
using System.Data.OleDb;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using SkyStem.ART.Service.Model;
using SkyStem.ART.Client.Model.CompanyDatabase;
using ClientModel = SkyStem.ART.Client.Model;
using SkyStem.ART.Shared.Data;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.Service.APP.BLL
{
    public class GLDataImport
    {

        private GLDataImportInfo oGLDataImportInfo;
        private CompanyUserInfo CompanyUserInfo;
        private List<ClientModel.LogInfo> LogInfoCache;

        public GLDataImport(CompanyUserInfo oCompanyUserInfo)
        {
            this.CompanyUserInfo = oCompanyUserInfo;
            this.LogInfoCache = new List<ClientModel.LogInfo>();
        }
        #region "Public Methods"
        public bool IsProcessingRequiredForGLDataImport()
        {
            bool processingRequired = false;
            try
            {
                oGLDataImportInfo = DataImportHelper.GetGLDataImportInfoForProcessing(DateTime.Now, this.CompanyUserInfo);
                if (oGLDataImportInfo.DataImportID > 0)
                {
                    processingRequired = true;
                    Helper.LogInfo(@"GLData Import required for DataImportID: " + oGLDataImportInfo.DataImportID.ToString(), this.CompanyUserInfo);
                }
                else
                {
                    Helper.LogInfo(@"No Data Available for GL Data Import.", this.CompanyUserInfo);
                }
            }
            catch (Exception ex)
            {
                oGLDataImportInfo = null;
                processingRequired = false;
                Helper.LogError(@"Error in IsProcessingRequiredForGLDataImport: " + ex.Message, this.CompanyUserInfo);
            }
            return processingRequired;
        }

        public void ProcessGLDataImport()
        {
            try
            {
                Helper.LogInfo(@"Start GLData Import for DataImportID: " + oGLDataImportInfo.DataImportID.ToString(), this.CompanyUserInfo);
                if (oGLDataImportInfo.IsDataTransfered)
                {
                    ProcessImportedGLData();
                }
                else
                {
                    ExtractTransferAndProcessData();
                }
            }
            catch (Exception ex)
            {
                DataImportHelper.ResetGLDataHdrObject(oGLDataImportInfo, ex);
                Helper.LogErrorToCache(ex, this.LogInfoCache);
            }
            finally
            {
                try
                {
                    DataImportHelper.UpdateDataImportHDR(oGLDataImportInfo, this.CompanyUserInfo);
                }
                catch (Exception ex)
                {
                    Helper.LogErrorToCache("Error while updating DataImportHDR - ", this.LogInfoCache);
                    Helper.LogErrorToCache(ex, this.LogInfoCache);
                }
                try
                {
                    oGLDataImportInfo.SuccessEmailIDs = DataImportHelper.GetEmailIDWithSeprator(oGLDataImportInfo.NotifySuccessEmailIds) + DataImportHelper.GetEmailIDWithSeprator(oGLDataImportInfo.NotifySuccessUserEmailIds) + DataImportHelper.GetEmailIDWithSeprator( oGLDataImportInfo.WarningEmailIds);
                    oGLDataImportInfo.FailureEmailIDs = DataImportHelper.GetEmailIDWithSeprator(oGLDataImportInfo.NotifyFailureEmailIds) + DataImportHelper.GetEmailIDWithSeprator(oGLDataImportInfo.NotifyFailureUserEmailIds) + DataImportHelper.GetEmailIDWithSeprator(oGLDataImportInfo.WarningEmailIds);
                    DataImportHelper.SendMailToUsers(oGLDataImportInfo, this.CompanyUserInfo);
                }
                catch (Exception ex)
                {
                    Helper.LogErrorToCache("Error while sending mail - ", this.LogInfoCache);
                    Helper.LogErrorToCache(ex, this.LogInfoCache);
                }
                try
                {
                    Helper.LogListViaService(this.LogInfoCache, oGLDataImportInfo.DataImportID, this.CompanyUserInfo);
                    Helper.LogInfo(@"End GLData Import for DataImportID: " + oGLDataImportInfo.DataImportID.ToString(), this.CompanyUserInfo);
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
        private void ExtractTransferAndProcessData()
        {
            DataTable TempdtExcelData = null;
            List<SkyStem.ART.Client.Model.ImportTemplateFieldMappingInfo> oImportTemplateFieldMappingInfoList = null;
            DataTable dtExcelData = null;
            Helper.LogInfoToCache("3. Start Reading Excel file: " + oGLDataImportInfo.PhysicalPath, this.LogInfoCache);

            //dtExcelData = Helper.GetDataTableFromExcel(oGLDataImportInfo.PhysicalPath, ServiceConstants.GLDATA_SHEETNAME);
            string SheetName = DataImportHelper.GetSheetName(Enums.DataImportType.GLData, oGLDataImportInfo.ImportTemplateID, oGLDataImportInfo.CompanyID);
            TempdtExcelData = DataImportHelper.GetGLDataImportDataTableFromExcel(oGLDataImportInfo.PhysicalPath, SheetName, this.CompanyUserInfo);
            if (oGLDataImportInfo != null && oGLDataImportInfo.ImportTemplateID.HasValue && oGLDataImportInfo.ImportTemplateID.Value != Convert.ToInt32(ServiceConstants.ART_TEMPLATE))
            {
                oImportTemplateFieldMappingInfoList = DataImportHelper.GetImportTemplateFieldMappingInfoList(oGLDataImportInfo.ImportTemplateID, oGLDataImportInfo.CompanyID);
                dtExcelData = DataImportHelper.RenameTemplateColumnNameToARTColumns(TempdtExcelData, oImportTemplateFieldMappingInfoList);
            }
            else
            {
                oImportTemplateFieldMappingInfoList = DataImportHelper.GetAllDataImportFieldsWithMapping(oGLDataImportInfo.DataImportID, oGLDataImportInfo.CompanyID);
                dtExcelData = TempdtExcelData;
            }

            if (ValidateSchemaForGLData_New(dtExcelData, oImportTemplateFieldMappingInfoList))
            {
                Helper.LogInfoToCache("4. Reading Excel file complete.", this.LogInfoCache);

                //Mark Static Field Present
                this.FieldPresent(dtExcelData);

                //Add additional fields to ExcelDataTabel
                AddDataImportIDToDataTable(dtExcelData);

                //Validate and Convert Data
                ValidateAndConvertData(dtExcelData, oImportTemplateFieldMappingInfoList);
                //  DataImportHelper.RenameAndTrimColumnNames(dtExcelData);
                //Transfer and Process data 
                DataImportHelper.TransferAndProcessGLData(dtExcelData, oGLDataImportInfo, this.LogInfoCache, this.CompanyUserInfo);
            }
        }

        private void ProcessImportedGLData()
        {
            DataImportHelper.ProcessTransferedGLData(oGLDataImportInfo, this.LogInfoCache, this.CompanyUserInfo);
        }

        private bool ValidateSchemaForGLData_New(DataTable dtExcelData, List<SkyStem.ART.Client.Model.ImportTemplateFieldMappingInfo> oImportTemplateFieldMappingInfoList)
        {
            bool isValidSchema;
            bool columnFound;
            StringBuilder oSbError = new StringBuilder();
            DataTable dtMessage = DataImportHelper.CreateDataImportMandatoryFieldsNotPresentMessageTable();
            DataTable dtWarnningMessage = DataImportHelper.CreateDataImportMandatoryFieldsNotPresentMessageTable();
            DataImportMessageInfo DataImportMessageInfoMandatoryFieldsNotPresent = DataImportHelper.GetDataImportMessageInfo((short)Enums.DataImportMessage.MandatoryFieldsNotPresent, this.CompanyUserInfo.CompanyID);
            DataImportMessageInfo DataImportMessageInfoColumnsForNewAccountCreationNotPresent = DataImportHelper.GetDataImportMessageInfo((short)Enums.DataImportMessage.ColumnsForNewAccountCreationNotPresent, this.CompanyUserInfo.CompanyID);
            List<ClientModel.DataImportMessageDetailInfo> oDataImportMessageDetailInfoList = new List<ClientModel.DataImportMessageDetailInfo>();
            //Get list of all mandatory fields
            List<string> GLDataImporMandatoryFieldList = DataImportHelper.GetGLDataImportAllMandatoryFields(oGLDataImportInfo);
            ClientModel.DataImportHdrInfo oDataImportHdrInfoBlank = new ClientModel.DataImportHdrInfo();
            List<string> ALLGLDataImporMandatoryFieldList = DataImportHelper.GetAllPossibleGLDataImportFields(oGLDataImportInfo);
            List<string> MandatoryFieldsNotPresentList = new List<string>();




            //Check if all mandatory fields exists in DataTable from Excel
            foreach (string fieldName in GLDataImporMandatoryFieldList)
            {
                columnFound = false;
                for (int i = 0; i < dtExcelData.Columns.Count; i++)
                {
                    if (!string.IsNullOrEmpty(fieldName) && !string.IsNullOrEmpty(dtExcelData.Columns[i].ColumnName) && fieldName.ToLower().Trim() == dtExcelData.Columns[i].ColumnName.ToLower().Trim())
                    {
                        columnFound = true;
                        break;
                    }
                }
                if (!columnFound)
                {
                    if (!oSbError.ToString().Equals(string.Empty))
                        oSbError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    var oImportTemplateFieldMappingInfo = DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList, fieldName);
                    if (oImportTemplateFieldMappingInfo != null)
                    {
                        string ImportTemplateFieldName = DataImportHelper.GetImportTemplateFieldName(oImportTemplateFieldMappingInfo, oGLDataImportInfo.LanguageID, oGLDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);
                        oSbError.Append(ImportTemplateFieldName);

                        DataRow drMessage = dtMessage.NewRow();
                        if (oImportTemplateFieldMappingInfo.ImportFieldID.HasValue)
                            drMessage[DataImportMessageConstants.Fields.ImportFieldID] = oImportTemplateFieldMappingInfo.ImportFieldID.Value;
                        drMessage[DataImportMessageConstants.Fields.ImportField] = ImportTemplateFieldName;
                        drMessage[DataImportMessageConstants.Fields.MessageLabelID] = DataImportMessageInfoMandatoryFieldsNotPresent.DataImportMessageLabelID;
                        dtMessage.Rows.Add(drMessage);
                        MandatoryFieldsNotPresentList.Add(ImportTemplateFieldName);
                    }
                    //oSbError.Append(fieldName);
                }
            }
            isValidSchema = string.IsNullOrEmpty(oSbError.ToString());




            //Check if All Possible GLDataImport Fields  exists in DataTable from Excel
            foreach (string fieldName in ALLGLDataImporMandatoryFieldList)
            {
                columnFound = false;
                for (int i = 0; i < dtExcelData.Columns.Count; i++)
                {
                    if (!string.IsNullOrEmpty(fieldName) && !string.IsNullOrEmpty(dtExcelData.Columns[i].ColumnName) && fieldName.ToLower().Trim() == dtExcelData.Columns[i].ColumnName.ToLower().Trim())
                    {
                        columnFound = true;
                        break;
                    }
                }
                if (!columnFound)
                {

                    var oImportTemplateFieldMappingInfo = DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList, fieldName);
                    if (oImportTemplateFieldMappingInfo != null)
                    {
                        string ImportTemplateFieldName = DataImportHelper.GetImportTemplateFieldName(oImportTemplateFieldMappingInfo, oGLDataImportInfo.LanguageID, oGLDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);
                        if (!MandatoryFieldsNotPresentList.Contains(ImportTemplateFieldName))
                        {
                            DataRow drMessage = dtWarnningMessage.NewRow();
                            if (oImportTemplateFieldMappingInfo.ImportFieldID.HasValue)
                                drMessage[DataImportMessageConstants.Fields.ImportFieldID] = oImportTemplateFieldMappingInfo.ImportFieldID.Value;
                            drMessage[DataImportMessageConstants.Fields.ImportField] = ImportTemplateFieldName;
                            drMessage[DataImportMessageConstants.Fields.MessageLabelID] = DataImportMessageInfoColumnsForNewAccountCreationNotPresent.DataImportMessageLabelID;
                            dtWarnningMessage.Rows.Add(drMessage);
                        }
                    }
                }
            }
            if (dtWarnningMessage.Rows.Count > 0)
            {
                ClientModel.DataImportMessageDetailInfo oDataImportMessageDetailInfo = new ClientModel.DataImportMessageDetailInfo();
                oDataImportMessageDetailInfo.DataImportMessageTypeID = (short)DataImportMessageInfoColumnsForNewAccountCreationNotPresent.DataImportMessageTypeID;
                oDataImportMessageDetailInfo.DataImportMessageID = DataImportMessageInfoColumnsForNewAccountCreationNotPresent.DataImportMessageID;
                oDataImportMessageDetailInfo.DataImportMessageLabelID = DataImportMessageInfoColumnsForNewAccountCreationNotPresent.DataImportMessageLabelID;
                DataSet ds = new DataSet();
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
                    ClientModel.DataImportMessageDetailInfo oDataImportMessageDetailInfo = new ClientModel.DataImportMessageDetailInfo();
                    oDataImportMessageDetailInfo.DataImportMessageTypeID = (short)DataImportMessageInfoMandatoryFieldsNotPresent.DataImportMessageTypeID;
                    oDataImportMessageDetailInfo.DataImportMessageID = DataImportMessageInfoMandatoryFieldsNotPresent.DataImportMessageID;
                    oDataImportMessageDetailInfo.DataImportMessageLabelID = DataImportMessageInfoMandatoryFieldsNotPresent.DataImportMessageLabelID;
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dtMessage);
                    oDataImportMessageDetailInfo.MessageSchema = ds.GetXmlSchema();
                    oDataImportMessageDetailInfo.MessageData = ds.GetXml();
                    oDataImportMessageDetailInfoList.Add(oDataImportMessageDetailInfo);
                }
                string errorMessage = Helper.GetSinglePhrase(5000165, 0, oGLDataImportInfo.LanguageID, oGLDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);//Mandatory columns not present: {0}

                oGLDataImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
                oGLDataImportInfo.DataImportStatusID = (short)Enums.DataImportStatus.Failure;
                oGLDataImportInfo.ErrorMessageToSave = String.Format(errorMessage, oSbError.ToString());
                oGLDataImportInfo.DataImportMessageDetailInfoList = oDataImportMessageDetailInfoList;
                throw new Exception(String.Format(errorMessage, oSbError.ToString()));
            }
            else
            {
                if (oDataImportMessageDetailInfoList != null && oDataImportMessageDetailInfoList.Count > 0)
                    oGLDataImportInfo.DataImportMessageDetailInfoList = oDataImportMessageDetailInfoList;
            }

            return isValidSchema;
        }

        private bool ValidateSchemaForGLData(DataTable dtExcelData)
        {
            bool isValidSchema;
            StringBuilder oSbError = new StringBuilder();

            //Get a List of GL Data Import Mandatory Fields
            List<string> staticFieldList = Helper.GetGLDataImportMandatoryFields();

            //Get a List of Key Fields
            List<string> keyFieldList = oGLDataImportInfo.KeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();

            //Define a new resultant list which will contain both above lists
            List<string> allMandatoryFieldsList = new List<string>();

            //Copy Mandatory fields to Resultant List
            allMandatoryFieldsList.AddRange(staticFieldList);

            //Copy Key fields list to resultant list
            allMandatoryFieldsList.AddRange(keyFieldList);


            string columnName = "";
            int? columnIndex = null;

            //Run a loop on resultant List. If ColumnName is found in first row of ExcelDataTabel, Rename that ExcelDataSet Column
            //else store that field name in a stringbuilder which will be used later to format exception message
            foreach (string fieldName in allMandatoryFieldsList)
            {
                columnIndex = null;
                for (int j = 0; j < dtExcelData.Columns.Count; j++)
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
                string errorMessage = Helper.GetSinglePhrase(5000165, 0, oGLDataImportInfo.LanguageID, oGLDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);//Mandatory columns not present: {0}

                oGLDataImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
                oGLDataImportInfo.DataImportStatusID = (short)Enums.DataImportStatus.Failure;
                oGLDataImportInfo.ErrorMessageToSave = String.Format(errorMessage, oSbError.ToString());
                throw new Exception(String.Format(errorMessage, oSbError.ToString()));
            }
            return isValidSchema;
        }
        private void ValidateAndConvertData(DataTable dtExcelData, List<SkyStem.ART.Client.Model.ImportTemplateFieldMappingInfo> oImportTemplateFieldMappingInfoList)
        {
            StringBuilder oSBError = new StringBuilder();
            string msg = Helper.GetDataLengthErrorMessage(ServiceConstants.DEFAULTBUSINESSENTITYID, oGLDataImportInfo.LanguageID, oGLDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);
            string InvalidDataMsg = Helper.GetInvalidDataErrorMessage(ServiceConstants.DEFAULTBUSINESSENTITYID, oGLDataImportInfo.LanguageID, oGLDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);
            List<ClientModel.DataImportMessageDetailInfo> oDataImportMessageDetailInfoList = new List<ClientModel.DataImportMessageDetailInfo>();
            DataImportMessageInfo DataImportMessageDataLengthExceeded = DataImportHelper.GetDataImportMessageInfo((short)Enums.DataImportMessage.DataLengthExceeded, this.CompanyUserInfo.CompanyID);
            DataImportMessageInfo DataImportMessageInfoInvalidValue = DataImportHelper.GetDataImportMessageInfo((short)Enums.DataImportMessage.InvalidValue, this.CompanyUserInfo.CompanyID);
            DataImportMessageInfo DataImportMessageInfoNoDataForMandatoryField = DataImportHelper.GetDataImportMessageInfo((short)Enums.DataImportMessage.NoDataForMandatoryField, this.CompanyUserInfo.CompanyID);
            for (int x = 0; x < dtExcelData.Rows.Count; x++)
            {
                DataTable dtMessage = DataImportHelper.CreateDataImportMessageTable();
                DataTable dtMessageNoDataForMandatoryField = DataImportHelper.CreateDataImportMandatoryFieldsNotPresentMessageTable();
                DataTable dtMessageInvalidValue = DataImportHelper.CreateDataImportMandatoryFieldsNotPresentMessageTable();

                DataRow dr = dtExcelData.Rows[x];
                string excelRowNumber = dr[AddedGLDataImportFields.EXCELROWNUMBER].ToString();

                if (oGLDataImportInfo.IsFSCaptionFieldAvailable)
                {
                    if (dr[GLDataImportFields.FSCAPTION] != DBNull.Value)
                        dr[GLDataImportFields.FSCAPTION] = dr[GLDataImportFields.FSCAPTION].ToString().Trim();
                    if (dr[GLDataImportFields.FSCAPTION].ToString().Length > (int)Enums.DataImportFieldsMaxLength.FSCaption)
                    {
                        var oImportTemplateFieldMappingInfo = DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList, GLDataImportFields.FSCAPTION);
                        if (oImportTemplateFieldMappingInfo != null)
                        {
                            string ImportTemplateFieldName = DataImportHelper.GetImportTemplateFieldName(oImportTemplateFieldMappingInfo, oGLDataImportInfo.LanguageID, oGLDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);
                            oSBError.Append(String.Format(msg, ImportTemplateFieldName, excelRowNumber, ((int)Enums.DataImportFieldsMaxLength.FSCaption).ToString()));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            DataRow drMessage = dtMessage.NewRow();
                            if (oImportTemplateFieldMappingInfo.ImportFieldID.HasValue)
                                drMessage[DataImportMessageConstants.Fields.ImportFieldID] = oImportTemplateFieldMappingInfo.ImportFieldID.Value;
                            drMessage[DataImportMessageConstants.Fields.ImportField] = ImportTemplateFieldName;
                            //if (oImportTemplateFieldMappingInfo.MessageLabelID.HasValue)
                            //    drMessage[DataImportMessageConstants.Fields.MessageLabelID] = oImportTemplateFieldMappingInfo.MessageLabelID.Value;
                            //else
                            drMessage[DataImportMessageConstants.Fields.MessageLabelID] = DataImportMessageDataLengthExceeded.DataImportMessageLabelID;
                            drMessage[DataImportMessageConstants.Fields.Allowed] = (int)Enums.DataImportFieldsMaxLength.FSCaption;
                            drMessage[DataImportMessageConstants.Fields.Actual] = dr[GLDataImportFields.FSCAPTION].ToString().Length;
                            dtMessage.Rows.Add(drMessage);
                        }
                    }
                }

                if (oGLDataImportInfo.IsProfitAndLossAvailable)
                {
                    if (dr[GLDataImportFields.ISPROFITANDLOSS] != DBNull.Value)
                        dr[GLDataImportFields.ISPROFITANDLOSS] = dr[GLDataImportFields.ISPROFITANDLOSS].ToString().Trim();
                    if (dr[GLDataImportFields.ISPROFITANDLOSS].ToString().Length > (int)Enums.DataImportFieldsMaxLength.IsProfitAndLoss)
                    {
                        var oImportTemplateFieldMappingInfo = DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList, GLDataImportFields.ISPROFITANDLOSS);
                        if (oImportTemplateFieldMappingInfo != null)
                        {
                            string ImportTemplateFieldName = DataImportHelper.GetImportTemplateFieldName(oImportTemplateFieldMappingInfo, oGLDataImportInfo.LanguageID, oGLDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);
                            oSBError.Append(String.Format(msg, ImportTemplateFieldName, excelRowNumber, ((int)Enums.DataImportFieldsMaxLength.IsProfitAndLoss).ToString()));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            DataRow drMessage = dtMessage.NewRow();
                            if (oImportTemplateFieldMappingInfo.ImportFieldID.HasValue)
                                drMessage[DataImportMessageConstants.Fields.ImportFieldID] = oImportTemplateFieldMappingInfo.ImportFieldID.Value;
                            drMessage[DataImportMessageConstants.Fields.ImportField] = ImportTemplateFieldName;
                            //if (oImportTemplateFieldMappingInfo.MessageLabelID.HasValue)
                            //    drMessage[DataImportMessageConstants.Fields.MessageLabelID] = oImportTemplateFieldMappingInfo.MessageLabelID.Value;
                            //else
                            drMessage[DataImportMessageConstants.Fields.MessageLabelID] = DataImportMessageDataLengthExceeded.DataImportMessageLabelID;
                            drMessage[DataImportMessageConstants.Fields.Allowed] = (int)Enums.DataImportFieldsMaxLength.IsProfitAndLoss;
                            drMessage[DataImportMessageConstants.Fields.Actual] = dr[GLDataImportFields.ISPROFITANDLOSS].ToString().Length;
                            dtMessage.Rows.Add(drMessage);
                        }
                    }
                }

                if (oGLDataImportInfo.IsAccountNameFieldAvailable)
                {
                    if (dr[GLDataImportFields.GLACCOUNTNAME] != DBNull.Value)
                        dr[GLDataImportFields.GLACCOUNTNAME] = dr[GLDataImportFields.GLACCOUNTNAME].ToString().Trim();
                    if (dr[GLDataImportFields.GLACCOUNTNAME].ToString().Length > (int)Enums.DataImportFieldsMaxLength.AccountName)
                    {
                        var oImportTemplateFieldMappingInfo = DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList, GLDataImportFields.GLACCOUNTNAME);
                        if (oImportTemplateFieldMappingInfo != null)
                        {
                            string ImportTemplateFieldName = DataImportHelper.GetImportTemplateFieldName(oImportTemplateFieldMappingInfo, oGLDataImportInfo.LanguageID, oGLDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);
                            oSBError.Append(String.Format(msg, ImportTemplateFieldName, excelRowNumber, ((int)Enums.DataImportFieldsMaxLength.AccountName).ToString()));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            DataRow drMessage = dtMessage.NewRow();
                            if (oImportTemplateFieldMappingInfo.ImportFieldID.HasValue)
                                drMessage[DataImportMessageConstants.Fields.ImportFieldID] = oImportTemplateFieldMappingInfo.ImportFieldID.Value;
                            drMessage[DataImportMessageConstants.Fields.ImportField] = ImportTemplateFieldName;
                            //if (oImportTemplateFieldMappingInfo.MessageLabelID.HasValue)
                            //    drMessage[DataImportMessageConstants.Fields.MessageLabelID] = oImportTemplateFieldMappingInfo.MessageLabelID.Value;
                            //else
                            drMessage[DataImportMessageConstants.Fields.MessageLabelID] = DataImportMessageDataLengthExceeded.DataImportMessageLabelID;
                            drMessage[DataImportMessageConstants.Fields.Allowed] = (int)Enums.DataImportFieldsMaxLength.AccountName;
                            drMessage[DataImportMessageConstants.Fields.Actual] = dr[GLDataImportFields.GLACCOUNTNAME].ToString().Length;
                            dtMessage.Rows.Add(drMessage);
                        }
                    }
                }

                if (oGLDataImportInfo.IsAccountNumberFieldAvailable)
                {
                    if (dr[GLDataImportFields.GLACCOUNTNUMBER] != DBNull.Value)
                        dr[GLDataImportFields.GLACCOUNTNUMBER] = dr[GLDataImportFields.GLACCOUNTNUMBER].ToString().Trim();
                    if (dr[GLDataImportFields.GLACCOUNTNUMBER].ToString().Length > (int)Enums.DataImportFieldsMaxLength.AccountNumber)
                    {
                        var oImportTemplateFieldMappingInfo = DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList, GLDataImportFields.GLACCOUNTNUMBER);
                        if (oImportTemplateFieldMappingInfo != null)
                        {
                            string ImportTemplateFieldName = DataImportHelper.GetImportTemplateFieldName(oImportTemplateFieldMappingInfo, oGLDataImportInfo.LanguageID, oGLDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);
                            oSBError.Append(String.Format(msg, ImportTemplateFieldName, excelRowNumber, ((int)Enums.DataImportFieldsMaxLength.AccountNumber).ToString()));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            DataRow drMessage = dtMessage.NewRow();
                            if (oImportTemplateFieldMappingInfo.ImportFieldID.HasValue)
                                drMessage[DataImportMessageConstants.Fields.ImportFieldID] = oImportTemplateFieldMappingInfo.ImportFieldID.Value;
                            drMessage[DataImportMessageConstants.Fields.ImportField] = ImportTemplateFieldName;
                            //if (oImportTemplateFieldMappingInfo.MessageLabelID.HasValue)
                            //    drMessage[DataImportMessageConstants.Fields.MessageLabelID] = oImportTemplateFieldMappingInfo.MessageLabelID.Value;
                            //else
                            drMessage[DataImportMessageConstants.Fields.MessageLabelID] = DataImportMessageDataLengthExceeded.DataImportMessageLabelID;
                            drMessage[DataImportMessageConstants.Fields.Allowed] = (int)Enums.DataImportFieldsMaxLength.AccountNumber;
                            drMessage[DataImportMessageConstants.Fields.Actual] = dr[GLDataImportFields.GLACCOUNTNUMBER].ToString().Length;
                            dtMessage.Rows.Add(drMessage);
                        }
                    }
                }

                //Keyfields
                string[] arrKeyFields = oGLDataImportInfo.KeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int k = 0; k < arrKeyFields.Length; k++)
                {
                    string sourceField = arrKeyFields[k].ToString();
                    if (dtExcelData.Columns.Contains(sourceField))
                    {
                        if (dr[sourceField] != DBNull.Value)
                            dr[sourceField] = dr[sourceField].ToString().Trim();
                        if (dr[sourceField].ToString().Length > (int)Enums.DataImportFieldsMaxLength.KeyFields)
                        {
                            var oImportTemplateFieldMappingInfo = DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList, sourceField);
                            if (oImportTemplateFieldMappingInfo != null)
                            {
                                string ImportTemplateFieldName = DataImportHelper.GetImportTemplateFieldName(oImportTemplateFieldMappingInfo, oGLDataImportInfo.LanguageID, oGLDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);
                                oSBError.Append(String.Format(msg, ImportTemplateFieldName, excelRowNumber, ((int)Enums.DataImportFieldsMaxLength.KeyFields).ToString()));
                                oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                                DataRow drMessage = dtMessage.NewRow();
                                if (oImportTemplateFieldMappingInfo.ImportFieldID.HasValue)
                                    drMessage[DataImportMessageConstants.Fields.ImportFieldID] = oImportTemplateFieldMappingInfo.ImportFieldID.Value;
                                drMessage[DataImportMessageConstants.Fields.ImportField] = ImportTemplateFieldName;
                                //if (oImportTemplateFieldMappingInfo.MessageLabelID.HasValue)
                                //    drMessage[DataImportMessageConstants.Fields.MessageLabelID] = oImportTemplateFieldMappingInfo.MessageLabelID.Value;
                                //else
                                drMessage[DataImportMessageConstants.Fields.MessageLabelID] = DataImportMessageDataLengthExceeded.DataImportMessageLabelID;
                                drMessage[DataImportMessageConstants.Fields.Allowed] = (int)Enums.DataImportFieldsMaxLength.KeyFields;
                                drMessage[DataImportMessageConstants.Fields.Actual] = dr[sourceField].ToString().Length;
                                dtMessage.Rows.Add(drMessage);
                            }
                        }
                    }
                }
                // Invalid Data Validations
                DateTime periodEndDate;
                if (Helper.IsValidDateTime(dr[GLDataImportFields.PERIODENDDATE].ToString(), oGLDataImportInfo.LanguageID, out periodEndDate))
                {
                    dr[GLDataImportFields.PERIODENDDATE] = periodEndDate.ToShortDateString();
                    dr[AddedGLDataImportFields.RECPERIODENDDATE] = periodEndDate.ToShortDateString();
                }
                else if (string.IsNullOrEmpty(Convert.ToString(dr[GLDataImportFields.PERIODENDDATE])))
                {
                    var oImportTemplateFieldMappingInfo = DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList, GLDataImportFields.PERIODENDDATE);
                    if (oImportTemplateFieldMappingInfo != null)
                    {
                        string ImportTemplateFieldName = DataImportHelper.GetImportTemplateFieldName(oImportTemplateFieldMappingInfo, oGLDataImportInfo.LanguageID, oGLDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);
                        oSBError.Append(String.Format(InvalidDataMsg, ImportTemplateFieldName, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        DataRow drMessage = dtMessageNoDataForMandatoryField.NewRow();
                        if (oImportTemplateFieldMappingInfo.ImportFieldID.HasValue)
                            drMessage[DataImportMessageConstants.Fields.ImportFieldID] = oImportTemplateFieldMappingInfo.ImportFieldID.Value;
                        drMessage[DataImportMessageConstants.Fields.ImportField] = ImportTemplateFieldName;
                        drMessage[DataImportMessageConstants.Fields.MessageLabelID] = DataImportMessageInfoNoDataForMandatoryField.DataImportMessageLabelID;
                        dtMessageNoDataForMandatoryField.Rows.Add(drMessage);
                    }
                }
                else
                {
                    var oImportTemplateFieldMappingInfo = DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList, GLDataImportFields.PERIODENDDATE);
                    if (oImportTemplateFieldMappingInfo != null)
                    {
                        string ImportTemplateFieldName = DataImportHelper.GetImportTemplateFieldName(oImportTemplateFieldMappingInfo, oGLDataImportInfo.LanguageID, oGLDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);
                        oSBError.Append(String.Format(InvalidDataMsg, ImportTemplateFieldName, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        DataRow drMessage = dtMessageInvalidValue.NewRow();
                        if (oImportTemplateFieldMappingInfo.ImportFieldID.HasValue)
                            drMessage[DataImportMessageConstants.Fields.ImportFieldID] = oImportTemplateFieldMappingInfo.ImportFieldID.Value;
                        drMessage[DataImportMessageConstants.Fields.ImportField] = ImportTemplateFieldName;
                        drMessage[DataImportMessageConstants.Fields.MessageLabelID] = DataImportMessageInfoInvalidValue.DataImportMessageLabelID;
                        dtMessageInvalidValue.Rows.Add(drMessage);
                    }
                }

                decimal BalBCCY = 0;
                if (Helper.IsValidDecimal(dr[GLDataImportFields.BALANCEBCCY].ToString(), oGLDataImportInfo.LanguageID, out BalBCCY))
                {
                    dr[GLDataImportFields.BALANCEBCCY] = BalBCCY.ToString();
                }
                else if (string.IsNullOrEmpty(Convert.ToString(dr[GLDataImportFields.BALANCEBCCY])))
                {
                    var oImportTemplateFieldMappingInfo = DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList, GLDataImportFields.BALANCEBCCY);
                    if (oImportTemplateFieldMappingInfo != null)
                    {
                        string ImportTemplateFieldName = DataImportHelper.GetImportTemplateFieldName(oImportTemplateFieldMappingInfo, oGLDataImportInfo.LanguageID, oGLDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);
                        oSBError.Append(String.Format(InvalidDataMsg, DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList, GLDataImportFields.BALANCEBCCY), excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        DataRow drMessage = dtMessageNoDataForMandatoryField.NewRow();
                        if (oImportTemplateFieldMappingInfo.ImportFieldID.HasValue)
                            drMessage[DataImportMessageConstants.Fields.ImportFieldID] = oImportTemplateFieldMappingInfo.ImportFieldID.Value;
                        drMessage[DataImportMessageConstants.Fields.ImportField] = ImportTemplateFieldName;
                        drMessage[DataImportMessageConstants.Fields.MessageLabelID] = DataImportMessageInfoNoDataForMandatoryField.DataImportMessageLabelID;
                        dtMessageNoDataForMandatoryField.Rows.Add(drMessage);
                    }
                }
                else
                {
                    var oImportTemplateFieldMappingInfo = DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList, GLDataImportFields.BALANCEBCCY);
                    if (oImportTemplateFieldMappingInfo != null)
                    {
                        string ImportTemplateFieldName = DataImportHelper.GetImportTemplateFieldName(oImportTemplateFieldMappingInfo, oGLDataImportInfo.LanguageID, oGLDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);
                        oSBError.Append(String.Format(InvalidDataMsg, DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList, GLDataImportFields.BALANCEBCCY), excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        DataRow drMessage = dtMessageInvalidValue.NewRow();
                        if (oImportTemplateFieldMappingInfo.ImportFieldID.HasValue)
                            drMessage[DataImportMessageConstants.Fields.ImportFieldID] = oImportTemplateFieldMappingInfo.ImportFieldID.Value;
                        drMessage[DataImportMessageConstants.Fields.ImportField] = ImportTemplateFieldName;
                        drMessage[DataImportMessageConstants.Fields.MessageLabelID] = DataImportMessageInfoInvalidValue.DataImportMessageLabelID;
                        dtMessageInvalidValue.Rows.Add(drMessage);
                    }
                }

                decimal BalRCCY = 0;
                if (Helper.IsValidDecimal(dr[GLDataImportFields.BALANCERCCY].ToString(), oGLDataImportInfo.LanguageID, out BalRCCY))
                {
                    dr[GLDataImportFields.BALANCERCCY] = BalRCCY.ToString();
                }
                else if (string.IsNullOrEmpty(Convert.ToString(dr[GLDataImportFields.BALANCERCCY])))
                {
                    var oImportTemplateFieldMappingInfo = DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList, GLDataImportFields.BALANCERCCY);
                    if (oImportTemplateFieldMappingInfo != null)
                    {
                        string ImportTemplateFieldName = DataImportHelper.GetImportTemplateFieldName(oImportTemplateFieldMappingInfo, oGLDataImportInfo.LanguageID, oGLDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);
                        oSBError.Append(String.Format(InvalidDataMsg, ImportTemplateFieldName, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        DataRow drMessage = dtMessageNoDataForMandatoryField.NewRow();
                        if (oImportTemplateFieldMappingInfo.ImportFieldID.HasValue)
                            drMessage[DataImportMessageConstants.Fields.ImportFieldID] = oImportTemplateFieldMappingInfo.ImportFieldID.Value;
                        drMessage[DataImportMessageConstants.Fields.ImportField] = ImportTemplateFieldName;
                        drMessage[DataImportMessageConstants.Fields.MessageLabelID] = DataImportMessageInfoNoDataForMandatoryField.DataImportMessageLabelID;
                        dtMessageNoDataForMandatoryField.Rows.Add(drMessage);
                    }
                }
                else
                {
                    var oImportTemplateFieldMappingInfo = DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList, GLDataImportFields.BALANCERCCY);
                    if (oImportTemplateFieldMappingInfo != null)
                    {
                        string ImportTemplateFieldName = DataImportHelper.GetImportTemplateFieldName(oImportTemplateFieldMappingInfo, oGLDataImportInfo.LanguageID, oGLDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);
                        oSBError.Append(String.Format(InvalidDataMsg, ImportTemplateFieldName, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        DataRow drMessage = dtMessageInvalidValue.NewRow();
                        if (oImportTemplateFieldMappingInfo.ImportFieldID.HasValue)
                            drMessage[DataImportMessageConstants.Fields.ImportFieldID] = oImportTemplateFieldMappingInfo.ImportFieldID.Value;
                        drMessage[DataImportMessageConstants.Fields.ImportField] = ImportTemplateFieldName;
                        drMessage[DataImportMessageConstants.Fields.MessageLabelID] = DataImportMessageInfoInvalidValue.DataImportMessageLabelID;
                        dtMessageInvalidValue.Rows.Add(drMessage);
                    }
                }

                if (dtMessage.Rows.Count > 0)
                {
                    ClientModel.DataImportMessageDetailInfo oDataImportMessageDetailInfo = new ClientModel.DataImportMessageDetailInfo();
                    oDataImportMessageDetailInfo.ExcelRowNumber = Convert.ToInt32(excelRowNumber);
                    oDataImportMessageDetailInfo.DataImportMessageTypeID = (short)DataImportMessageDataLengthExceeded.DataImportMessageTypeID;
                    oDataImportMessageDetailInfo.DataImportMessageID = DataImportMessageDataLengthExceeded.DataImportMessageID;
                    oDataImportMessageDetailInfo.DataImportMessageLabelID = DataImportMessageDataLengthExceeded.DataImportMessageLabelID;
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dtMessage);
                    oDataImportMessageDetailInfo.MessageSchema = ds.GetXmlSchema();
                    oDataImportMessageDetailInfo.MessageData = ds.GetXml();
                    oDataImportMessageDetailInfoList.Add(oDataImportMessageDetailInfo);
                }
                if (dtMessageNoDataForMandatoryField.Rows.Count > 0)
                {
                    ClientModel.DataImportMessageDetailInfo oDataImportMessageDetailInfo = new ClientModel.DataImportMessageDetailInfo();
                    oDataImportMessageDetailInfo.ExcelRowNumber = Convert.ToInt32(excelRowNumber);
                    oDataImportMessageDetailInfo.DataImportMessageTypeID = (short)DataImportMessageInfoNoDataForMandatoryField.DataImportMessageTypeID;
                    oDataImportMessageDetailInfo.DataImportMessageID = DataImportMessageInfoNoDataForMandatoryField.DataImportMessageID;
                    oDataImportMessageDetailInfo.DataImportMessageLabelID = DataImportMessageInfoNoDataForMandatoryField.DataImportMessageLabelID;
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dtMessageNoDataForMandatoryField);
                    oDataImportMessageDetailInfo.MessageSchema = ds.GetXmlSchema();
                    oDataImportMessageDetailInfo.MessageData = ds.GetXml();
                    oDataImportMessageDetailInfoList.Add(oDataImportMessageDetailInfo);
                }
                if (dtMessageInvalidValue.Rows.Count > 0)
                {
                    ClientModel.DataImportMessageDetailInfo oDataImportMessageDetailInfo = new ClientModel.DataImportMessageDetailInfo();
                    oDataImportMessageDetailInfo.ExcelRowNumber = Convert.ToInt32(excelRowNumber);
                    oDataImportMessageDetailInfo.DataImportMessageTypeID = (short)DataImportMessageInfoInvalidValue.DataImportMessageTypeID;
                    oDataImportMessageDetailInfo.DataImportMessageID = DataImportMessageInfoInvalidValue.DataImportMessageID;
                    oDataImportMessageDetailInfo.DataImportMessageLabelID = DataImportMessageInfoInvalidValue.DataImportMessageLabelID;
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dtMessageInvalidValue);
                    oDataImportMessageDetailInfo.MessageSchema = ds.GetXmlSchema();
                    oDataImportMessageDetailInfo.MessageData = ds.GetXml();
                    oDataImportMessageDetailInfoList.Add(oDataImportMessageDetailInfo);
                }

            }
            if (!oSBError.ToString().Equals(String.Empty))
            {
                oGLDataImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
                oGLDataImportInfo.DataImportStatusID = (short)Enums.DataImportStatus.Failure;
                oGLDataImportInfo.ErrorMessageToSave = oSBError.ToString();
                oGLDataImportInfo.DataImportMessageDetailInfoList = oDataImportMessageDetailInfoList;
                throw new Exception(oSBError.ToString());
            }
        }



        private void AddDataImportIDToDataTable(DataTable dtExcelData)
        {
            dtExcelData.Columns.Add(AddedGLDataImportFields.DATAIMPORTID, typeof(System.Int32));
            dtExcelData.Columns.Add(AddedGLDataImportFields.EXCELROWNUMBER, typeof(System.Int32));
            dtExcelData.Columns.Add(AddedGLDataImportFields.RECPERIODENDDATE, typeof(System.String));

            //DateTime dtPeriodEndDate = new DateTime();

            for (int x = 0; x < dtExcelData.Rows.Count; x++)
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

        private static void MapUserAccountInfoObject(SqlDataReader r, List<ClientModel.UserAccountInfo> oUserAccountInfoCollection)
        {
            ClientModel.UserAccountInfo oUserAccountInfo;
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
                oUserAccountInfo = new ClientModel.UserAccountInfo();
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
            oGLDataImportInfo.IsFSCaptionFieldAvailable = oDTExcel.Columns.Contains(GLDataImportFields.FSCAPTION);
            oGLDataImportInfo.IsAccountNameFieldAvailable = oDTExcel.Columns.Contains(GLDataImportFields.GLACCOUNTNAME);
            oGLDataImportInfo.IsAccountNumberFieldAvailable = oDTExcel.Columns.Contains(GLDataImportFields.GLACCOUNTNUMBER);
            oGLDataImportInfo.IsAccountTypeFieldAvailable = oDTExcel.Columns.Contains(GLDataImportFields.ACCOUNTTYPE);
            oGLDataImportInfo.IsProfitAndLossAvailable = oDTExcel.Columns.Contains(GLDataImportFields.ISPROFITANDLOSS);
        }
        #endregion

    }
}
