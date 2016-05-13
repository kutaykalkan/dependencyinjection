using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using SkyStem.ART.Service.Data;
using SkyStem.ART.Service.Utility;
using SkyStem.ART.Service.DAO;
using System.Data.OleDb;
using SkyStem.ART.Service.Model;
using SkyStem.ART.Client.Model.CompanyDatabase;
using ClientModel = SkyStem.ART.Client.Model;
using SkyStem.ART.Shared.Data;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.Service.APP.BLL
{
    public class SubledgerDataImport
    {
        #region "Private Attributes"
        private SubledgerDataImportInfo oSubledgerDataImportInfo = new SubledgerDataImportInfo();
        private CompanyUserInfo CompanyUserInfo;
        private List<ClientModel.LogInfo> LogInfoCache;
        #endregion

        public SubledgerDataImport(CompanyUserInfo oCompanyUserInfo)
        {
            this.CompanyUserInfo = oCompanyUserInfo;
            this.LogInfoCache = new List<ClientModel.LogInfo>();
        }

        #region "Public Methods"
        public bool IsProcessingRequiredForSubledgerDataImport()
        {
            bool processingRequired = false;
            try
            {
                oSubledgerDataImportInfo = DataImportHelper.GetSubledgerDataImportInfoForProcessing(DateTime.Now, this.CompanyUserInfo);
                if (oSubledgerDataImportInfo.DataImportID > 0)
                {
                    processingRequired = true;
                    Helper.LogInfo(@"Subledger Data Import required for DataImportID: " + oSubledgerDataImportInfo.DataImportID.ToString(), this.CompanyUserInfo);
                }
                else
                {
                    Helper.LogInfo(@"No Data Available for Subledger Data Import.", this.CompanyUserInfo);
                }
            }
            catch (Exception ex)
            {
                oSubledgerDataImportInfo = null;
                processingRequired = false;
                Helper.LogError(@"Error in IsProcessingRequiredForSubledgerDataImport: " + ex.Message, this.CompanyUserInfo);
            }
            return processingRequired;
        }

        public void ProcessSubledgerDataImport()
        {
            try
            {
                Helper.LogInfo(@"Start GLData Import for DataImportID: " + oSubledgerDataImportInfo.DataImportID.ToString(), this.CompanyUserInfo);
                if (oSubledgerDataImportInfo.IsDataTransfered)
                {
                    ProcessImportedSubledgerData();
                }
                else
                {
                    ExtractTransferAndProcessData();
                }
            }
            catch (Exception ex)
            {
                DataImportHelper.ResetSubledgerDataHdrObject(oSubledgerDataImportInfo, ex);
                Helper.LogErrorToCache(ex, this.LogInfoCache);
            }
            finally
            {
                try
                {
                    DataImportHelper.UpdateDataImportHDR(oSubledgerDataImportInfo, this.CompanyUserInfo);
                }
                catch (Exception ex)
                {
                    Helper.LogErrorToCache("Error while updating DataImportHDR - ", this.LogInfoCache);
                    Helper.LogErrorToCache(ex, this.LogInfoCache);
                }
                try
                {
                    oSubledgerDataImportInfo.SuccessEmailIDs = DataImportHelper.GetEmailIDWithSeprator(oSubledgerDataImportInfo.NotifySuccessEmailIds) + DataImportHelper.GetEmailIDWithSeprator(oSubledgerDataImportInfo.NotifySuccessUserEmailIds) + DataImportHelper.GetEmailIDWithSeprator(oSubledgerDataImportInfo.WarningEmailIds);
                    oSubledgerDataImportInfo.FailureEmailIDs = DataImportHelper.GetEmailIDWithSeprator(oSubledgerDataImportInfo.NotifyFailureEmailIds) + DataImportHelper.GetEmailIDWithSeprator(oSubledgerDataImportInfo.NotifyFailureUserEmailIds) + DataImportHelper.GetEmailIDWithSeprator(oSubledgerDataImportInfo.WarningEmailIds);
                    DataImportHelper.SendMailToUsers(oSubledgerDataImportInfo, this.CompanyUserInfo);
                }
                catch (Exception ex)
                {
                    Helper.LogErrorToCache("Error while sending mail - ", this.LogInfoCache);
                    Helper.LogErrorToCache(ex, this.LogInfoCache);
                }
                try
                {
                    Helper.LogListViaService(this.LogInfoCache, oSubledgerDataImportInfo.DataImportID, this.CompanyUserInfo);
                    Helper.LogInfo(@"End GLData Import for DataImportID: " + oSubledgerDataImportInfo.DataImportID.ToString(), this.CompanyUserInfo);
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
            DataTable dtExcelData = null;
            DataTable TempdtExcelData = null;
              List<SkyStem.ART.Client.Model.ImportTemplateFieldMappingInfo> oImportTemplateFieldMappingInfoList =null;
            Helper.LogInfoToCache("3. Start Reading Excel file: " + oSubledgerDataImportInfo.PhysicalPath, this.LogInfoCache);
            //dtExcelData = Helper.GetDataTableFromExcel(oSubledgerDataImportInfo.PhysicalPath, ServiceConstants.SUBLEDGER_SHEETNAME);
            string SheetName = DataImportHelper.GetSheetName(Enums.DataImportType.SubledgerData, oSubledgerDataImportInfo.ImportTemplateID, oSubledgerDataImportInfo.CompanyID);
            TempdtExcelData = DataImportHelper.GetSubledgerDataImportDataTableFromExcel(oSubledgerDataImportInfo.PhysicalPath, SheetName, this.CompanyUserInfo);
            if ( oSubledgerDataImportInfo != null && oSubledgerDataImportInfo.ImportTemplateID.HasValue && oSubledgerDataImportInfo.ImportTemplateID.Value != Convert.ToInt32(ServiceConstants.ART_TEMPLATE))
            {
                oImportTemplateFieldMappingInfoList = DataImportHelper.GetImportTemplateFieldMappingInfoList(oSubledgerDataImportInfo.ImportTemplateID, oSubledgerDataImportInfo.CompanyID);
                dtExcelData = DataImportHelper.RenameTemplateColumnNameToARTColumns(TempdtExcelData, oImportTemplateFieldMappingInfoList);
            }
            else
            {
                oImportTemplateFieldMappingInfoList = DataImportHelper.GetAllDataImportFieldsWithMapping(oSubledgerDataImportInfo.DataImportID, oSubledgerDataImportInfo.CompanyID);
                dtExcelData = TempdtExcelData;
            }

            if (ValidateSchemaForSubledgerData_New(dtExcelData,oImportTemplateFieldMappingInfoList))
            {
                Helper.LogInfoToCache("4. Reading Excel file complete.", this.LogInfoCache);

                //Mark Static Field Present
                this.FieldPresent(dtExcelData);

                //Add additional fields to ExcelDataTabel
                AddDataImportIDToDataTable(dtExcelData);

                //Validate and convert data
                ValidateAndConvertData(dtExcelData, oImportTemplateFieldMappingInfoList);

                //Transfer and Process data 
                DataImportHelper.TransferAndProcessSubledgerData(dtExcelData, oSubledgerDataImportInfo, this.LogInfoCache, this.CompanyUserInfo);
            }
        }

        private void ProcessImportedSubledgerData()
        {
            DataImportHelper.ProcessTransferedSubledgerData(oSubledgerDataImportInfo, this.LogInfoCache, this.CompanyUserInfo);
        }

        private bool ValidateSchemaForSubledgerData_New(DataTable dtExcelData, List<SkyStem.ART.Client.Model.ImportTemplateFieldMappingInfo> oImportTemplateFieldMappingInfoList)
        {
            bool isValidSchema;
            bool columnFound;
            StringBuilder oSbError = new StringBuilder();
            DataTable dtMessage = DataImportHelper.CreateDataImportMandatoryFieldsNotPresentMessageTable();
            DataImportMessageInfo DataImportMessageInfoMandatoryFieldsNotPresent = DataImportHelper.GetDataImportMessageInfo((short)Enums.DataImportMessage.MandatoryFieldsNotPresent, this.CompanyUserInfo.CompanyID);
            List<ClientModel.DataImportMessageDetailInfo> oDataImportMessageDetailInfoList = new List<ClientModel.DataImportMessageDetailInfo>();

            //Get list of all mandatory fields
            List<string> SubledgerDataImporMandatoryFieldList = DataImportHelper.GetSubledgerDataImportAllMandatoryFields(oSubledgerDataImportInfo);

            //Check if all mandatory fields exists in DataTable from Excel
            foreach (string fieldName in SubledgerDataImporMandatoryFieldList)
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
                    //oSbError.Append(DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList, fieldName));
                    var oImportTemplateFieldMappingInfo = DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList, fieldName);
                    if (oImportTemplateFieldMappingInfo != null)
                    {
                        string ImportTemplateFieldName = DataImportHelper.GetImportTemplateFieldName(oImportTemplateFieldMappingInfo, oSubledgerDataImportInfo.LanguageID, oSubledgerDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);
                        oSbError.Append(ImportTemplateFieldName);

                        DataRow drMessage = dtMessage.NewRow();
                        if (oImportTemplateFieldMappingInfo.ImportFieldID.HasValue)
                            drMessage[DataImportMessageConstants.Fields.ImportFieldID] = oImportTemplateFieldMappingInfo.ImportFieldID.Value;
                        drMessage[DataImportMessageConstants.Fields.ImportField] = ImportTemplateFieldName;
                        drMessage[DataImportMessageConstants.Fields.MessageLabelID] = DataImportMessageInfoMandatoryFieldsNotPresent.DataImportMessageLabelID;
                        dtMessage.Rows.Add(drMessage);
                    }
                }
            }
            isValidSchema = string.IsNullOrEmpty(oSbError.ToString());

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
                string errorMessage = Helper.GetSinglePhrase(5000165, 0, oSubledgerDataImportInfo.LanguageID, oSubledgerDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);//Mandatory columns not present: {0}

                oSubledgerDataImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
                oSubledgerDataImportInfo.DataImportStatusID = (short)Enums.DataImportStatus.Failure;
                oSubledgerDataImportInfo.ErrorMessageToSave = String.Format(errorMessage, oSbError.ToString());
                oSubledgerDataImportInfo.DataImportMessageDetailInfoList = oDataImportMessageDetailInfoList;
                throw new Exception(String.Format(errorMessage, oSbError.ToString()));
            }
            return isValidSchema;
        }

        private void AddDataImportIDToDataTable(DataTable dtExcelData)
        {
            DataColumn dl = new DataColumn(AddedGLDataImportFields.DATAIMPORTID, typeof(System.Int32));
            DataColumn dlRowNumber = new DataColumn(AddedGLDataImportFields.EXCELROWNUMBER, typeof(System.Int32));
            dtExcelData.Columns.Add(AddedGLDataImportFields.RECPERIODENDDATE, typeof(System.String));
            dtExcelData.Columns.Add(dl);
            dtExcelData.Columns.Add(dlRowNumber);        
            for (int x = 0; x < dtExcelData.Rows.Count; x++)
            {
                dtExcelData.Rows[x][AddedGLDataImportFields.DATAIMPORTID] = oSubledgerDataImportInfo.DataImportID;
                dtExcelData.Rows[x][AddedGLDataImportFields.EXCELROWNUMBER] = x + 2;
            }
        }

        //private void ValidateAndConvertData(DataTable dtExcelData, List<SkyStem.ART.Client.Model.ImportTemplateFieldMappingInfo> oImportTemplateFieldMappingInfoList )
        //{
        //    StringBuilder oSBError = new StringBuilder();
        //    string msg = Helper.GetDataLengthErrorMessage(ServiceConstants.DEFAULTBUSINESSENTITYID, oSubledgerDataImportInfo.LanguageID, oSubledgerDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);
        //    string InvalidDataMsg = Helper.GetInvalidDataErrorMessage(ServiceConstants.DEFAULTBUSINESSENTITYID, oSubledgerDataImportInfo.LanguageID, oSubledgerDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);

        //    for (int x = 0; x < dtExcelData.Rows.Count; x++)
        //    {
        //        DataRow dr = dtExcelData.Rows[x];
        //        string excelRowNumber = dr[AddedGLDataImportFields.EXCELROWNUMBER].ToString();
        //        // Validate Data Length Validations
        //        if (oSubledgerDataImportInfo.IsFSCaptionFieldAvailable)
        //        {
        //            if (dr[GLDataImportFields.FSCAPTION] != DBNull.Value)
        //                dr[GLDataImportFields.FSCAPTION] = dr[GLDataImportFields.FSCAPTION].ToString().Trim();
        //            if (dr[GLDataImportFields.FSCAPTION].ToString().Length > (int)Enums.DataImportFieldsMaxLength.FSCaption)
        //            {
        //                oSBError.Append(String.Format(msg, DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList, GLDataImportFields.FSCAPTION), excelRowNumber, Enums.DataImportFieldsMaxLength.FSCaption));
        //                oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
        //            }
        //        }

        //        if (oSubledgerDataImportInfo.IsProfitAndLossAvailable)
        //        {
        //            if (dr[GLDataImportFields.ISPROFITANDLOSS] != DBNull.Value)
        //                dr[GLDataImportFields.ISPROFITANDLOSS] = dr[GLDataImportFields.ISPROFITANDLOSS].ToString().Trim();
        //            if (dr[GLDataImportFields.ISPROFITANDLOSS].ToString().Length > (int)Enums.DataImportFieldsMaxLength.IsProfitAndLoss)
        //            {
        //                oSBError.Append(String.Format(msg, DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList, GLDataImportFields.ISPROFITANDLOSS), excelRowNumber, ((int)Enums.DataImportFieldsMaxLength.IsProfitAndLoss).ToString()));
        //                oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
        //            }
        //        }

        //        if (oSubledgerDataImportInfo.IsAccountNameFieldAvailable)
        //        {
        //            if (dr[GLDataImportFields.GLACCOUNTNAME] != DBNull.Value)
        //                dr[GLDataImportFields.GLACCOUNTNAME] = dr[GLDataImportFields.GLACCOUNTNAME].ToString().Trim();
        //            if (dr[GLDataImportFields.GLACCOUNTNAME].ToString().Length > (int)Enums.DataImportFieldsMaxLength.AccountName)
        //            {
        //                oSBError.Append(String.Format(msg, DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList, GLDataImportFields.GLACCOUNTNAME), excelRowNumber, Enums.DataImportFieldsMaxLength.AccountName));
        //                oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
        //            }
        //        }

        //        if (oSubledgerDataImportInfo.IsAccountNumberFieldAvailable)
        //        {
        //            if (dr[GLDataImportFields.GLACCOUNTNUMBER] != DBNull.Value)
        //                dr[GLDataImportFields.GLACCOUNTNUMBER] = dr[GLDataImportFields.GLACCOUNTNUMBER].ToString().Trim();
        //            if (dr[GLDataImportFields.GLACCOUNTNUMBER].ToString().Length > (int)Enums.DataImportFieldsMaxLength.AccountNumber)
        //            {
        //                oSBError.Append(String.Format(msg, DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList, GLDataImportFields.GLACCOUNTNUMBER), excelRowNumber, Enums.DataImportFieldsMaxLength.AccountNumber));
        //                oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
        //            }
        //        }

        //        //Keyfields
        //        if (!string.IsNullOrEmpty(oSubledgerDataImportInfo.KeyFields))
        //        {
        //            string[] arrKeyFields = oSubledgerDataImportInfo.KeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        //            for (int k = 0; k < arrKeyFields.Length; k++)
        //            {
        //                string sourceField = arrKeyFields[k].ToString();
        //                if (dtExcelData.Columns.Contains(sourceField))
        //                {
        //                    if (dr[sourceField] != DBNull.Value)
        //                        dr[sourceField] = dr[sourceField].ToString().Trim();
        //                    if (dr[sourceField].ToString().Length > (int)Enums.DataImportFieldsMaxLength.KeyFields)
        //                    {
        //                        oSBError.Append(String.Format(msg, DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList, sourceField), excelRowNumber, Enums.DataImportFieldsMaxLength.KeyFields));
        //                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
        //                    }
        //                }
        //            }
        //        }
        //        // Invalid Data Validations
        //        DateTime periodEndDate;
        //        if (Helper.IsValidDateTime(dr[GLDataImportFields.PERIODENDDATE].ToString(), oSubledgerDataImportInfo.LanguageID, out periodEndDate))
        //        {
        //            dr[GLDataImportFields.PERIODENDDATE] = periodEndDate.ToShortDateString();
        //            dr[AddedGLDataImportFields.RECPERIODENDDATE] = periodEndDate.ToShortDateString();
        //        }
        //        else
        //        {
        //            oSBError.Append(String.Format(InvalidDataMsg, DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList, GLDataImportFields.PERIODENDDATE), excelRowNumber));
        //            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
        //        }

        //        decimal BalBCCY = 0;
        //        if (Helper.IsValidDecimal(dr[GLDataImportFields.BALANCEBCCY].ToString(), oSubledgerDataImportInfo.LanguageID, out BalBCCY))
        //        {
        //            dr[GLDataImportFields.BALANCEBCCY] = BalBCCY.ToString();
        //        }
        //        else
        //        {
        //            oSBError.Append(String.Format(InvalidDataMsg, DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList, GLDataImportFields.BALANCEBCCY), excelRowNumber));
        //            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
        //        }

        //        decimal BalRCCY = 0;
        //        if (Helper.IsValidDecimal(dr[GLDataImportFields.BALANCERCCY].ToString(), oSubledgerDataImportInfo.LanguageID, out BalRCCY))
        //        {
        //            dr[GLDataImportFields.BALANCERCCY] = BalRCCY.ToString();
        //        }
        //        else
        //        {
        //            oSBError.Append(String.Format(InvalidDataMsg, DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList, GLDataImportFields.BALANCERCCY), excelRowNumber));
        //            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
        //        }
        //    }
        //    if (!oSBError.ToString().Equals(String.Empty))
        //    {
        //        oSubledgerDataImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
        //        oSubledgerDataImportInfo.DataImportStatusID = (short)Enums.DataImportStatus.Failure;
        //        oSubledgerDataImportInfo.ErrorMessageToSave = oSBError.ToString();
        //        throw new Exception(oSBError.ToString());
        //    }
        //}

        private void ValidateAndConvertData(DataTable dtExcelData, List<SkyStem.ART.Client.Model.ImportTemplateFieldMappingInfo> oImportTemplateFieldMappingInfoList)
        {
            StringBuilder oSBError = new StringBuilder();
            string msg = Helper.GetDataLengthErrorMessage(ServiceConstants.DEFAULTBUSINESSENTITYID, oSubledgerDataImportInfo.LanguageID, oSubledgerDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);
            string InvalidDataMsg = Helper.GetInvalidDataErrorMessage(ServiceConstants.DEFAULTBUSINESSENTITYID, oSubledgerDataImportInfo.LanguageID, oSubledgerDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);
            List<ClientModel.DataImportMessageDetailInfo> oDataImportMessageDetailInfoList = new List<ClientModel.DataImportMessageDetailInfo>();
            DataImportMessageInfo DataImportMessageDataLengthExceeded = DataImportHelper.GetDataImportMessageInfo((short)Enums.DataImportMessage.DataLengthExceeded, this.CompanyUserInfo.CompanyID);
            DataImportMessageInfo DataImportMessageInfoInvalidValue = DataImportHelper.GetDataImportMessageInfo((short)Enums.DataImportMessage.InvalidValue, this.CompanyUserInfo.CompanyID);
            for (int x = 0; x < dtExcelData.Rows.Count; x++)
            {
                DataTable dtMessage = DataImportHelper.CreateDataImportMessageTable();
                DataRow dr = dtExcelData.Rows[x];
                string excelRowNumber = dr[AddedGLDataImportFields.EXCELROWNUMBER].ToString();
                // Validate Data Length Validations
                if (oSubledgerDataImportInfo.IsFSCaptionFieldAvailable)
                {
                    if (dr[GLDataImportFields.FSCAPTION] != DBNull.Value)
                        dr[GLDataImportFields.FSCAPTION] = dr[GLDataImportFields.FSCAPTION].ToString().Trim();
                    if (dr[GLDataImportFields.FSCAPTION].ToString().Length > (int)Enums.DataImportFieldsMaxLength.FSCaption)
                    {
                        var oImportTemplateFieldMappingInfo = DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList, GLDataImportFields.FSCAPTION);
                        if (oImportTemplateFieldMappingInfo != null)
                        {
                            string ImportTemplateFieldName = DataImportHelper.GetImportTemplateFieldName(oImportTemplateFieldMappingInfo,oSubledgerDataImportInfo.LanguageID, oSubledgerDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);
                            oSBError.Append(String.Format(msg, ImportTemplateFieldName, excelRowNumber, ((int)Enums.DataImportFieldsMaxLength.FSCaption).ToString()));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            DataRow drMessage = dtMessage.NewRow();
                            if (oImportTemplateFieldMappingInfo.ImportFieldID.HasValue)
                                drMessage[DataImportMessageConstants.Fields.ImportFieldID] = oImportTemplateFieldMappingInfo.ImportFieldID.Value;
                            drMessage[DataImportMessageConstants.Fields.ImportField] = ImportTemplateFieldName;
                            drMessage[DataImportMessageConstants.Fields.MessageLabelID] = DataImportMessageDataLengthExceeded.DataImportMessageLabelID;
                            drMessage[DataImportMessageConstants.Fields.Allowed] = (int)Enums.DataImportFieldsMaxLength.FSCaption;
                            drMessage[DataImportMessageConstants.Fields.Actual] = dr[GLDataImportFields.FSCAPTION].ToString().Length;
                            dtMessage.Rows.Add(drMessage);
                        }
                    }
                }

                if (oSubledgerDataImportInfo.IsProfitAndLossAvailable)
                {
                    if (dr[GLDataImportFields.ISPROFITANDLOSS] != DBNull.Value)
                        dr[GLDataImportFields.ISPROFITANDLOSS] = dr[GLDataImportFields.ISPROFITANDLOSS].ToString().Trim();
                    if (dr[GLDataImportFields.ISPROFITANDLOSS].ToString().Length > (int)Enums.DataImportFieldsMaxLength.IsProfitAndLoss)
                    {
                        var oImportTemplateFieldMappingInfo = DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList, GLDataImportFields.ISPROFITANDLOSS);
                        if (oImportTemplateFieldMappingInfo != null)
                        {
                            string ImportTemplateFieldName = DataImportHelper.GetImportTemplateFieldName(oImportTemplateFieldMappingInfo, oSubledgerDataImportInfo.LanguageID, oSubledgerDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);
                            oSBError.Append(String.Format(msg, ImportTemplateFieldName, excelRowNumber, ((int)Enums.DataImportFieldsMaxLength.IsProfitAndLoss).ToString()));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            DataRow drMessage = dtMessage.NewRow();
                            if (oImportTemplateFieldMappingInfo.ImportFieldID.HasValue)
                                drMessage[DataImportMessageConstants.Fields.ImportFieldID] = oImportTemplateFieldMappingInfo.ImportFieldID.Value;
                            drMessage[DataImportMessageConstants.Fields.ImportField] = ImportTemplateFieldName;
                            drMessage[DataImportMessageConstants.Fields.MessageLabelID] = DataImportMessageDataLengthExceeded.DataImportMessageLabelID;
                            drMessage[DataImportMessageConstants.Fields.Allowed] = (int)Enums.DataImportFieldsMaxLength.IsProfitAndLoss;
                            drMessage[DataImportMessageConstants.Fields.Actual] = dr[GLDataImportFields.ISPROFITANDLOSS].ToString().Length;
                            dtMessage.Rows.Add(drMessage);
                        }
                    }
                }

                if (oSubledgerDataImportInfo.IsAccountNameFieldAvailable)
                {
                    if (dr[GLDataImportFields.GLACCOUNTNAME] != DBNull.Value)
                        dr[GLDataImportFields.GLACCOUNTNAME] = dr[GLDataImportFields.GLACCOUNTNAME].ToString().Trim();
                    if (dr[GLDataImportFields.GLACCOUNTNAME].ToString().Length > (int)Enums.DataImportFieldsMaxLength.AccountName)
                    {
                        var oImportTemplateFieldMappingInfo = DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList, GLDataImportFields.GLACCOUNTNAME);
                        if (oImportTemplateFieldMappingInfo != null)
                        {
                            string ImportTemplateFieldName = DataImportHelper.GetImportTemplateFieldName(oImportTemplateFieldMappingInfo, oSubledgerDataImportInfo.LanguageID, oSubledgerDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);
                            oSBError.Append(String.Format(msg, ImportTemplateFieldName, excelRowNumber, ((int)Enums.DataImportFieldsMaxLength.AccountName).ToString()));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            DataRow drMessage = dtMessage.NewRow();
                            if (oImportTemplateFieldMappingInfo.ImportFieldID.HasValue)
                                drMessage[DataImportMessageConstants.Fields.ImportFieldID] = oImportTemplateFieldMappingInfo.ImportFieldID.Value;
                            drMessage[DataImportMessageConstants.Fields.ImportField] = ImportTemplateFieldName;
                            drMessage[DataImportMessageConstants.Fields.MessageLabelID] = DataImportMessageDataLengthExceeded.DataImportMessageLabelID;
                            drMessage[DataImportMessageConstants.Fields.Allowed] = (int)Enums.DataImportFieldsMaxLength.AccountName;
                            drMessage[DataImportMessageConstants.Fields.Actual] = dr[GLDataImportFields.GLACCOUNTNAME].ToString().Length;
                            dtMessage.Rows.Add(drMessage);
                        }
                    }
                }

                if (oSubledgerDataImportInfo.IsAccountNumberFieldAvailable)
                {
                    if (dr[GLDataImportFields.GLACCOUNTNUMBER] != DBNull.Value)
                        dr[GLDataImportFields.GLACCOUNTNUMBER] = dr[GLDataImportFields.GLACCOUNTNUMBER].ToString().Trim();
                    if (dr[GLDataImportFields.GLACCOUNTNUMBER].ToString().Length > (int)Enums.DataImportFieldsMaxLength.AccountNumber)
                    {
                        var oImportTemplateFieldMappingInfo = DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList, GLDataImportFields.GLACCOUNTNUMBER);
                        if (oImportTemplateFieldMappingInfo != null)
                        {
                            string ImportTemplateFieldName = DataImportHelper.GetImportTemplateFieldName(oImportTemplateFieldMappingInfo, oSubledgerDataImportInfo.LanguageID, oSubledgerDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);
                            oSBError.Append(String.Format(msg, ImportTemplateFieldName, excelRowNumber, ((int)Enums.DataImportFieldsMaxLength.AccountNumber).ToString()));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            DataRow drMessage = dtMessage.NewRow();
                            if (oImportTemplateFieldMappingInfo.ImportFieldID.HasValue)
                                drMessage[DataImportMessageConstants.Fields.ImportFieldID] = oImportTemplateFieldMappingInfo.ImportFieldID.Value;
                            drMessage[DataImportMessageConstants.Fields.ImportField] = ImportTemplateFieldName;
                            drMessage[DataImportMessageConstants.Fields.MessageLabelID] = DataImportMessageDataLengthExceeded.DataImportMessageLabelID;
                            drMessage[DataImportMessageConstants.Fields.Allowed] = (int)Enums.DataImportFieldsMaxLength.AccountNumber;
                            drMessage[DataImportMessageConstants.Fields.Actual] = dr[GLDataImportFields.GLACCOUNTNUMBER].ToString().Length;
                            dtMessage.Rows.Add(drMessage);
                        }
                    }
                }

                //Keyfields
                string[] arrKeyFields = oSubledgerDataImportInfo.KeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
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
                                string ImportTemplateFieldName = DataImportHelper.GetImportTemplateFieldName(oImportTemplateFieldMappingInfo, oSubledgerDataImportInfo.LanguageID, oSubledgerDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);
                                oSBError.Append(String.Format(msg, ImportTemplateFieldName, excelRowNumber, ((int)Enums.DataImportFieldsMaxLength.KeyFields).ToString()));
                                oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                                DataRow drMessage = dtMessage.NewRow();
                                if (oImportTemplateFieldMappingInfo.ImportFieldID.HasValue)
                                    drMessage[DataImportMessageConstants.Fields.ImportFieldID] = oImportTemplateFieldMappingInfo.ImportFieldID.Value;
                                drMessage[DataImportMessageConstants.Fields.ImportField] = ImportTemplateFieldName;
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
                if (Helper.IsValidDateTime(dr[GLDataImportFields.PERIODENDDATE].ToString(), oSubledgerDataImportInfo.LanguageID, out periodEndDate))
                {
                    dr[GLDataImportFields.PERIODENDDATE] = periodEndDate.ToShortDateString();
                    dr[AddedGLDataImportFields.RECPERIODENDDATE] = periodEndDate.ToShortDateString();
                }
                else
                {
                    var oImportTemplateFieldMappingInfo = DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList, GLDataImportFields.PERIODENDDATE);
                    if (oImportTemplateFieldMappingInfo != null)
                    {
                        string ImportTemplateFieldName = DataImportHelper.GetImportTemplateFieldName(oImportTemplateFieldMappingInfo, oSubledgerDataImportInfo.LanguageID, oSubledgerDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);
                        oSBError.Append(String.Format(InvalidDataMsg, ImportTemplateFieldName, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        DataRow drMessage = dtMessage.NewRow();
                        if (oImportTemplateFieldMappingInfo.ImportFieldID.HasValue)
                            drMessage[DataImportMessageConstants.Fields.ImportFieldID] = oImportTemplateFieldMappingInfo.ImportFieldID.Value;
                        drMessage[DataImportMessageConstants.Fields.ImportField] = ImportTemplateFieldName;
                        drMessage[DataImportMessageConstants.Fields.MessageLabelID] = DataImportMessageInfoInvalidValue.DataImportMessageLabelID;
                        dtMessage.Rows.Add(drMessage);
                    }
                }

                decimal BalBCCY = 0;
                if (Helper.IsValidDecimal(dr[GLDataImportFields.BALANCEBCCY].ToString(), oSubledgerDataImportInfo.LanguageID, out BalBCCY))
                {
                    dr[GLDataImportFields.BALANCEBCCY] = BalBCCY.ToString();
                }
                else
                {
                    var oImportTemplateFieldMappingInfo = DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList, GLDataImportFields.BALANCEBCCY);
                    if (oImportTemplateFieldMappingInfo != null)
                    {
                        string ImportTemplateFieldName = DataImportHelper.GetImportTemplateFieldName(oImportTemplateFieldMappingInfo, oSubledgerDataImportInfo.LanguageID, oSubledgerDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);
                        oSBError.Append(String.Format(InvalidDataMsg, DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList, GLDataImportFields.BALANCEBCCY), excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        DataRow drMessage = dtMessage.NewRow();
                        if (oImportTemplateFieldMappingInfo.ImportFieldID.HasValue)
                            drMessage[DataImportMessageConstants.Fields.ImportFieldID] = oImportTemplateFieldMappingInfo.ImportFieldID.Value;
                        drMessage[DataImportMessageConstants.Fields.ImportField] = ImportTemplateFieldName;                        
                        drMessage[DataImportMessageConstants.Fields.MessageLabelID] = DataImportMessageInfoInvalidValue.DataImportMessageLabelID;
                        dtMessage.Rows.Add(drMessage);
                    }
                }

                decimal BalRCCY = 0;
                if (Helper.IsValidDecimal(dr[GLDataImportFields.BALANCERCCY].ToString(), oSubledgerDataImportInfo.LanguageID, out BalRCCY))
                {
                    dr[GLDataImportFields.BALANCERCCY] = BalRCCY.ToString();
                }
                else
                {
                    var oImportTemplateFieldMappingInfo = DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList, GLDataImportFields.BALANCERCCY);
                    if (oImportTemplateFieldMappingInfo != null)
                    {
                        string ImportTemplateFieldName = DataImportHelper.GetImportTemplateFieldName(oImportTemplateFieldMappingInfo, oSubledgerDataImportInfo.LanguageID, oSubledgerDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);
                        oSBError.Append(String.Format(InvalidDataMsg, ImportTemplateFieldName, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        DataRow drMessage = dtMessage.NewRow();
                        if (oImportTemplateFieldMappingInfo.ImportFieldID.HasValue)
                            drMessage[DataImportMessageConstants.Fields.ImportFieldID] = oImportTemplateFieldMappingInfo.ImportFieldID.Value;
                        drMessage[DataImportMessageConstants.Fields.ImportField] = ImportTemplateFieldName;                      
                        drMessage[DataImportMessageConstants.Fields.MessageLabelID] = DataImportMessageInfoInvalidValue.DataImportMessageLabelID;
                        dtMessage.Rows.Add(drMessage);
                    }
                }
                if (dtMessage.Rows.Count > 0)
                {
                    ClientModel.DataImportMessageDetailInfo oDataImportMessageDetailInfo = new ClientModel.DataImportMessageDetailInfo();
                    oDataImportMessageDetailInfo.ExcelRowNumber = Convert.ToInt32(excelRowNumber);
                    oDataImportMessageDetailInfo.DataImportMessageTypeID = (short)DataImportMessageInfoInvalidValue.DataImportMessageTypeID;
                    oDataImportMessageDetailInfo.DataImportMessageID = DataImportMessageInfoInvalidValue.DataImportMessageID;
                    oDataImportMessageDetailInfo.DataImportMessageLabelID = DataImportMessageInfoInvalidValue.DataImportMessageLabelID;
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dtMessage);
                    oDataImportMessageDetailInfo.MessageSchema = ds.GetXmlSchema();
                    oDataImportMessageDetailInfo.MessageData = ds.GetXml();
                    oDataImportMessageDetailInfoList.Add(oDataImportMessageDetailInfo);
                }
            }
            if (!oSBError.ToString().Equals(String.Empty))
            {
                oSubledgerDataImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
                oSubledgerDataImportInfo.DataImportStatusID = (short)Enums.DataImportStatus.Failure;
                oSubledgerDataImportInfo.ErrorMessageToSave = oSBError.ToString();
                oSubledgerDataImportInfo.DataImportMessageDetailInfoList = oDataImportMessageDetailInfoList;
                throw new Exception(oSBError.ToString());
            }
        }

        private void FieldPresent(DataTable oDTExcel)
        {
            oSubledgerDataImportInfo.IsFSCaptionFieldAvailable = oDTExcel.Columns.Contains(GLDataImportFields.FSCAPTION);
            oSubledgerDataImportInfo.IsAccountNameFieldAvailable = oDTExcel.Columns.Contains(GLDataImportFields.GLACCOUNTNAME);
            oSubledgerDataImportInfo.IsAccountNumberFieldAvailable = oDTExcel.Columns.Contains(GLDataImportFields.GLACCOUNTNUMBER);
            oSubledgerDataImportInfo.IsAccountTypeFieldAvailable = oDTExcel.Columns.Contains(GLDataImportFields.ACCOUNTTYPE);
            oSubledgerDataImportInfo.IsProfitAndLossAvailable = oDTExcel.Columns.Contains(GLDataImportFields.ISPROFITANDLOSS);
        }
        #endregion
    }
}
