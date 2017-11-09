using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;
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
    public class CurrencyDataImport
    {

        private CurrencyDataImportInfo oCurrencyDataImportInfo;
        private CompanyUserInfo CompanyUserInfo;
        private List<ClientModel.LogInfo> LogInfoCache;

        public CurrencyDataImport(CompanyUserInfo oCompanyUserInfo)
        {
            this.CompanyUserInfo = oCompanyUserInfo;
            this.LogInfoCache = new List<ClientModel.LogInfo>();
        }
        #region "Public Methods"
        public bool IsProcessingRequiredForCurrencyDataImport()
        {
            bool processingRequired = false;
            try
            {
                oCurrencyDataImportInfo = DataImportHelper.GetCurrencyDataImportInfoForProcessing(DateTime.Now, this.CompanyUserInfo);
                if (oCurrencyDataImportInfo.DataImportID > 0)
                {
                    processingRequired = true;
                    Helper.LogInfo(@"Currency Data Import required for DataImportID: " + oCurrencyDataImportInfo.DataImportID.ToString(), this.CompanyUserInfo);
                }
                else
                {
                    Helper.LogInfo(@"No Data Available for Currency Data Import.", this.CompanyUserInfo);
                }
            }
            catch (Exception ex)
            {
                oCurrencyDataImportInfo = null;
                processingRequired = false;
                Helper.LogError(@"Error in IsProcessingRequiredForCurrencyDataImport: " + ex.Message, this.CompanyUserInfo);
            }
            return processingRequired;
        }

        public void ProcessCurrencyDataImport()
        {
            try
            {
                Helper.LogInfo(@"Start Currency Data Import for DataImportID: " + oCurrencyDataImportInfo.DataImportID.ToString(), this.CompanyUserInfo);
                if (oCurrencyDataImportInfo.IsDataTransfered)
                {
                    ProcessImportedCurrencyData();
                }
                else
                {
                    ExtractTransferAndProcessData();
                }
            }
            catch (Exception ex)
            {
                DataImportHelper.ResetCurrencyDataHdrObject(oCurrencyDataImportInfo, ex);
                Helper.LogErrorToCache(ex, this.LogInfoCache);
            }
            finally
            {
                try
                {
                    DataImportHelper.UpdateDataImportHDR(oCurrencyDataImportInfo, this.CompanyUserInfo);
                }
                catch (Exception ex)
                {
                    Helper.LogErrorToCache("Error while updating DataImportHDR - ", this.LogInfoCache);
                    Helper.LogErrorToCache(ex, this.LogInfoCache);
                }
                try
                {
                    oCurrencyDataImportInfo.SuccessEmailIDs = DataImportHelper.GetEmailIDWithSeprator(oCurrencyDataImportInfo.NotifySuccessEmailIds) + DataImportHelper.GetEmailIDWithSeprator(oCurrencyDataImportInfo.NotifySuccessUserEmailIds) + DataImportHelper.GetEmailIDWithSeprator(oCurrencyDataImportInfo.WarningEmailIds);
                    oCurrencyDataImportInfo.FailureEmailIDs = DataImportHelper.GetEmailIDWithSeprator(oCurrencyDataImportInfo.NotifyFailureEmailIds) + DataImportHelper.GetEmailIDWithSeprator(oCurrencyDataImportInfo.NotifyFailureUserEmailIds) + DataImportHelper.GetEmailIDWithSeprator(oCurrencyDataImportInfo.WarningEmailIds);
                    DataImportHelper.SendMailToUsers(oCurrencyDataImportInfo, this.CompanyUserInfo);
                }
                catch (Exception ex)
                {
                    Helper.LogErrorToCache("Error while sending mail - ", this.LogInfoCache);
                    Helper.LogErrorToCache(ex, this.LogInfoCache);
                }
                try
                {
                    Helper.LogListViaService(this.LogInfoCache, oCurrencyDataImportInfo.DataImportID, this.CompanyUserInfo);
                    Helper.LogInfo(@"End GLData Import for DataImportID: " + oCurrencyDataImportInfo.DataImportID.ToString(), this.CompanyUserInfo);
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
            Helper.LogInfoToCache("3. Start Reading Excel file: " + oCurrencyDataImportInfo.PhysicalPath, this.LogInfoCache);

            dtExcelData = DataImportHelper.GetGLDataImportDataTableFromExcel(oCurrencyDataImportInfo.PhysicalPath, CurrencyExchangeUploadConstants.SheetName, this.CompanyUserInfo);
            if (ValidateSchemaForCurrencyData(dtExcelData))
            {
                Helper.LogInfoToCache("4. Reading Excel file complete.", this.LogInfoCache);

                //Add additional fields to ExcelDataTabel
                AddDataImportIDToDataTable(dtExcelData);

                //Validate and Convert Data
                ValidateAndConvertData(dtExcelData);
                //  DataImportHelper.RenameAndTrimColumnNames(dtExcelData);
                //Transfer and Process data 
                DataImportHelper.TransferAndProcessCurrencyData(dtExcelData, oCurrencyDataImportInfo, this.LogInfoCache, this.CompanyUserInfo);
            }
        }

        private void ProcessImportedCurrencyData()
        {
            DataImportHelper.ProcessTransferedCurrencyData(oCurrencyDataImportInfo, this.LogInfoCache, this.CompanyUserInfo);
        }

        private bool ValidateSchemaForCurrencyData(DataTable dtExcelData)
        {
            bool isValidSchema;
            bool columnFound;
            StringBuilder oSbError = new StringBuilder();
            DataTable dtMessage = DataImportHelper.CreateDataImportMandatoryFieldsNotPresentMessageTable();
            DataTable dtWarnningMessage = DataImportHelper.CreateDataImportMandatoryFieldsNotPresentMessageTable();
            DataImportMessageInfo DataImportMessageInfoMandatoryFieldsNotPresent = DataImportHelper.GetDataImportMessageInfo((short)Enums.DataImportMessage.MandatoryFieldsNotPresent, this.CompanyUserInfo.CompanyID);
            List<ClientModel.DataImportMessageDetailInfo> oDataImportMessageDetailInfoList = new List<ClientModel.DataImportMessageDetailInfo>();
            //Get list of all mandatory fields
            List<string> CurrencyDataImporMandatoryFieldList = DataImportHelper.GetCurrencyDataImportMandatoryFields();
            ClientModel.DataImportHdrInfo oDataImportHdrInfoBlank = new ClientModel.DataImportHdrInfo();
            List<string> MandatoryFieldsNotPresentList = new List<string>();

            //Check if all mandatory fields exists in DataTable from Excel
            foreach (string fieldName in CurrencyDataImporMandatoryFieldList)
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

                    oSbError.Append(fieldName);

                    DataRow drMessage = dtMessage.NewRow();
                    drMessage[DataImportMessageConstants.Fields.ImportField] = fieldName;
                    dtMessage.Rows.Add(drMessage);
                    MandatoryFieldsNotPresentList.Add(fieldName);
                    //oSbError.Append(fieldName);
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
                string errorMessage = Helper.GetSinglePhrase(5000165, 0, oCurrencyDataImportInfo.LanguageID, oCurrencyDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);//Mandatory columns not present: {0}

                oCurrencyDataImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
                oCurrencyDataImportInfo.DataImportStatusID = (short)Enums.DataImportStatus.Failure;
                oCurrencyDataImportInfo.ErrorMessageToSave = String.Format(errorMessage, oSbError.ToString());
                oCurrencyDataImportInfo.DataImportMessageDetailInfoList = oDataImportMessageDetailInfoList;
                throw new Exception(String.Format(errorMessage, oSbError.ToString()));
            }
            else
            {
                if (oDataImportMessageDetailInfoList != null && oDataImportMessageDetailInfoList.Count > 0)
                    oCurrencyDataImportInfo.DataImportMessageDetailInfoList = oDataImportMessageDetailInfoList;
            }
            return isValidSchema;
        }

        private void ValidateAndConvertData(DataTable dtExcelData)
        {
            StringBuilder oSBError = new StringBuilder();
            string msg = Helper.GetDataLengthErrorMessage(ServiceConstants.DEFAULTBUSINESSENTITYID, oCurrencyDataImportInfo.LanguageID, oCurrencyDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);
            string InvalidDataMsg = Helper.GetInvalidDataErrorMessage(ServiceConstants.DEFAULTBUSINESSENTITYID, oCurrencyDataImportInfo.LanguageID, oCurrencyDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);
            List<ClientModel.DataImportMessageDetailInfo> oDataImportMessageDetailInfoList = new List<ClientModel.DataImportMessageDetailInfo>();
            //DataImportMessageInfo DataImportMessageDataLengthExceeded = DataImportHelper.GetDataImportMessageInfo((short)Enums.DataImportMessage.DataLengthExceeded, this.CompanyUserInfo.CompanyID);
            DataImportMessageInfo DataImportMessageInvalidDataLength = DataImportHelper.GetDataImportMessageInfo((short)Enums.DataImportMessage.InvalidDataLength, this.CompanyUserInfo.CompanyID);
            DataImportMessageInfo DataImportMessageInfoInvalidValue = DataImportHelper.GetDataImportMessageInfo((short)Enums.DataImportMessage.InvalidValue, this.CompanyUserInfo.CompanyID);
            DataImportMessageInfo DataImportMessageInfoNoDataForMandatoryField = DataImportHelper.GetDataImportMessageInfo((short)Enums.DataImportMessage.NoDataForMandatoryField, this.CompanyUserInfo.CompanyID);
            for (int x = 0; x < dtExcelData.Rows.Count; x++)
            {
                DataTable dtMessage = DataImportHelper.CreateDataImportMessageTable();
                DataTable dtMessageNoDataForMandatoryField = DataImportHelper.CreateDataImportMandatoryFieldsNotPresentMessageTable();
                DataTable dtMessageInvalidValue = DataImportHelper.CreateDataImportMandatoryFieldsNotPresentMessageTable();

                DataRow dr = dtExcelData.Rows[x];
                string excelRowNumber = dr[CurrencyExchangeUploadConstants.AddedFields.EXCELROWNUMBER].ToString();

                if (dr[CurrencyExchangeUploadConstants.UploadFields.FROMCURRENCYCODE] != DBNull.Value)
                    dr[CurrencyExchangeUploadConstants.UploadFields.FROMCURRENCYCODE] = dr[CurrencyExchangeUploadConstants.UploadFields.FROMCURRENCYCODE].ToString().Trim();
                if (dr[CurrencyExchangeUploadConstants.UploadFields.FROMCURRENCYCODE].ToString().Length != (int)CurrencyExchangeUploadConstants.DataLength.FROMCURRENCYCODE)
                {
                    oSBError.Append(String.Format(msg, CurrencyExchangeUploadConstants.UploadFields.FROMCURRENCYCODE, excelRowNumber, ((int)CurrencyExchangeUploadConstants.DataLength.FROMCURRENCYCODE).ToString()));
                    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    DataRow drMessage = dtMessage.NewRow();
                    drMessage[DataImportMessageConstants.Fields.ImportField] = CurrencyExchangeUploadConstants.UploadFields.FROMCURRENCYCODE;
                    drMessage[DataImportMessageConstants.Fields.MessageLabelID] = DataImportMessageInvalidDataLength.DataImportMessageLabelID;
                    drMessage[DataImportMessageConstants.Fields.Allowed] = (int)CurrencyExchangeUploadConstants.DataLength.FROMCURRENCYCODE;
                    drMessage[DataImportMessageConstants.Fields.Actual] = dr[CurrencyExchangeUploadConstants.UploadFields.FROMCURRENCYCODE].ToString().Length;
                    dtMessage.Rows.Add(drMessage);
                }

                if (dr[CurrencyExchangeUploadConstants.UploadFields.TOCURRENCYCODE] != DBNull.Value)
                    dr[CurrencyExchangeUploadConstants.UploadFields.TOCURRENCYCODE] = dr[CurrencyExchangeUploadConstants.UploadFields.TOCURRENCYCODE].ToString().Trim();
                if (dr[CurrencyExchangeUploadConstants.UploadFields.TOCURRENCYCODE].ToString().Length != (int)CurrencyExchangeUploadConstants.DataLength.TOCURRENCYCODE)
                {
                    oSBError.Append(String.Format(msg, CurrencyExchangeUploadConstants.UploadFields.TOCURRENCYCODE, excelRowNumber, ((int)CurrencyExchangeUploadConstants.DataLength.TOCURRENCYCODE).ToString()));
                    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    DataRow drMessage = dtMessage.NewRow();
                    drMessage[DataImportMessageConstants.Fields.ImportField] = CurrencyExchangeUploadConstants.UploadFields.TOCURRENCYCODE;
                    drMessage[DataImportMessageConstants.Fields.MessageLabelID] = DataImportMessageInvalidDataLength.DataImportMessageLabelID;
                    drMessage[DataImportMessageConstants.Fields.Allowed] = (int)CurrencyExchangeUploadConstants.DataLength.TOCURRENCYCODE;
                    drMessage[DataImportMessageConstants.Fields.Actual] = dr[CurrencyExchangeUploadConstants.UploadFields.TOCURRENCYCODE].ToString().Length;
                    dtMessage.Rows.Add(drMessage);
                }

                // Invalid Data Validations
                DateTime periodEndDate;
                if (Helper.IsValidDateTime(dr[CurrencyExchangeUploadConstants.UploadFields.PERIODENDDATE].ToString(), oCurrencyDataImportInfo.LanguageID, out periodEndDate))
                {
                    dr[CurrencyExchangeUploadConstants.UploadFields.PERIODENDDATE] = periodEndDate.ToShortDateString();
                }
                else if (string.IsNullOrEmpty(Convert.ToString(dr[CurrencyExchangeUploadConstants.UploadFields.PERIODENDDATE])))
                {
                    oSBError.Append(String.Format(InvalidDataMsg, CurrencyExchangeUploadConstants.UploadFields.PERIODENDDATE, excelRowNumber));
                    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    DataRow drMessage = dtMessageNoDataForMandatoryField.NewRow();
                    drMessage[DataImportMessageConstants.Fields.ImportField] = CurrencyExchangeUploadConstants.UploadFields.PERIODENDDATE;
                    drMessage[DataImportMessageConstants.Fields.MessageLabelID] = DataImportMessageInfoNoDataForMandatoryField.DataImportMessageLabelID;
                    dtMessageNoDataForMandatoryField.Rows.Add(drMessage);
                }
                else
                {
                    oSBError.Append(String.Format(InvalidDataMsg, CurrencyExchangeUploadConstants.UploadFields.PERIODENDDATE, excelRowNumber));
                    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    DataRow drMessage = dtMessageInvalidValue.NewRow();
                    drMessage[DataImportMessageConstants.Fields.ImportField] = CurrencyExchangeUploadConstants.UploadFields.PERIODENDDATE;
                    drMessage[DataImportMessageConstants.Fields.MessageLabelID] = DataImportMessageInfoInvalidValue.DataImportMessageLabelID;
                    dtMessageInvalidValue.Rows.Add(drMessage);
                }

                decimal rate = 0;
                if (Helper.IsValidDecimal(dr[CurrencyExchangeUploadConstants.UploadFields.RATE].ToString(), oCurrencyDataImportInfo.LanguageID, out rate))
                {
                    dr[CurrencyExchangeUploadConstants.UploadFields.RATE] = rate.ToString();
                }
                else if (string.IsNullOrEmpty(Convert.ToString(dr[CurrencyExchangeUploadConstants.UploadFields.RATE])))
                {
                        oSBError.Append(String.Format(InvalidDataMsg, CurrencyExchangeUploadConstants.UploadFields.RATE, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        DataRow drMessage = dtMessageNoDataForMandatoryField.NewRow();
                        drMessage[DataImportMessageConstants.Fields.ImportField] = CurrencyExchangeUploadConstants.UploadFields.RATE;
                        drMessage[DataImportMessageConstants.Fields.MessageLabelID] = DataImportMessageInfoNoDataForMandatoryField.DataImportMessageLabelID;
                        dtMessageNoDataForMandatoryField.Rows.Add(drMessage);
                }
                else
                {
                        oSBError.Append(String.Format(InvalidDataMsg, CurrencyExchangeUploadConstants.UploadFields.RATE, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        DataRow drMessage = dtMessageInvalidValue.NewRow();
                        drMessage[DataImportMessageConstants.Fields.ImportField] = CurrencyExchangeUploadConstants.UploadFields.RATE;
                        drMessage[DataImportMessageConstants.Fields.MessageLabelID] = DataImportMessageInfoInvalidValue.DataImportMessageLabelID;
                        dtMessageInvalidValue.Rows.Add(drMessage);
                }

                if (dtMessage.Rows.Count > 0)
                {
                    ClientModel.DataImportMessageDetailInfo oDataImportMessageDetailInfo = new ClientModel.DataImportMessageDetailInfo();
                    oDataImportMessageDetailInfo.ExcelRowNumber = Convert.ToInt32(excelRowNumber);
                    oDataImportMessageDetailInfo.DataImportMessageTypeID = (short)DataImportMessageInvalidDataLength.DataImportMessageTypeID;
                    oDataImportMessageDetailInfo.DataImportMessageID = DataImportMessageInvalidDataLength.DataImportMessageID;
                    oDataImportMessageDetailInfo.DataImportMessageLabelID = DataImportMessageInvalidDataLength.DataImportMessageLabelID;
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
                oCurrencyDataImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
                oCurrencyDataImportInfo.DataImportStatusID = (short)Enums.DataImportStatus.Failure;
                oCurrencyDataImportInfo.ErrorMessageToSave = oSBError.ToString();
                oCurrencyDataImportInfo.DataImportMessageDetailInfoList = oDataImportMessageDetailInfoList;
                throw new Exception(oSBError.ToString());
            }
        }

        private void AddDataImportIDToDataTable(DataTable dtExcelData)
        {
            dtExcelData.Columns.Add(CurrencyExchangeUploadConstants.AddedFields.DATAIMPORTID, typeof(System.Int32));
            dtExcelData.Columns.Add(CurrencyExchangeUploadConstants.AddedFields.EXCELROWNUMBER, typeof(System.Int32));
            dtExcelData.Columns.Add(CurrencyExchangeUploadConstants.AddedFields.COMPANYID, typeof(System.Int32));
            for (int x = 0; x < dtExcelData.Rows.Count; x++)
            {
                dtExcelData.Rows[x][CurrencyExchangeUploadConstants.AddedFields.DATAIMPORTID] = oCurrencyDataImportInfo.DataImportID;
                dtExcelData.Rows[x][CurrencyExchangeUploadConstants.AddedFields.COMPANYID] = oCurrencyDataImportInfo.CompanyID;
                dtExcelData.Rows[x][CurrencyExchangeUploadConstants.AddedFields.EXCELROWNUMBER] = x + 2;
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
        #endregion
    }
}
