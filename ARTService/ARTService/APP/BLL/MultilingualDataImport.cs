using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Service.Model;
using SkyStem.ART.Service.Utility;
using System.Data;
using SkyStem.ART.Service.Data;
using SkyStem.ART.Shared.Data;
using SkyStem.Language.LanguageClient.Model;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Shared.Utility;
using SkyStem.Language.LanguageUtility.Classes;
using SkyStem.ART.Client.Model.CompanyDatabase;

namespace SkyStem.ART.Service.APP.BLL
{
    public class MultilingualDataImport
    {
        private MultilingualDataImportHdrInfo _MultilingualDataImportHdrInfo;
        private CompanyUserInfo CompanyUserInfo;
        public MultilingualDataImport(CompanyUserInfo oCompanyUserInfo)
        {
            this.CompanyUserInfo = oCompanyUserInfo;
        }
        #region "Public Methods"
        public bool IsProcessingRequiredForMultilingualDataImport()
        {
            bool processingRequired = false;
            try
            {
                _MultilingualDataImportHdrInfo = DataImportHelper.GetMultilingualDataImportInfoForProcessing(DateTime.Now, this.CompanyUserInfo);
                if (_MultilingualDataImportHdrInfo.DataImportID > 0)
                {
                    processingRequired = true;
                    Helper.LogInfo(@"Multilingual Data Import required for DataImportID: " + _MultilingualDataImportHdrInfo.DataImportID.ToString(), this.CompanyUserInfo);
                }
                else
                {
                    Helper.LogInfo(@"No Data Available for Multilingual Data Import.", this.CompanyUserInfo);
                }
            }
            catch (Exception ex)
            {
                _MultilingualDataImportHdrInfo = null;
                processingRequired = false;
                Helper.LogError(@"Error in IsProcessingRequiredForMultilingualDataImport: " + ex.Message, this.CompanyUserInfo);
            }
            return processingRequired;
        }

        public void ProcessMultilingualDataImport()
        {

            try
            {
                ExtractTransferAndProcessData();
            }
            catch (Exception ex)
            {
                DataImportHelper.ResetMultilingualDataHdrObject(_MultilingualDataImportHdrInfo, ex);
                Helper.LogError(ex, this.CompanyUserInfo);
            }
            finally
            {
                try
                {
                    DataImportHelper.UpdateDataImportHDR(_MultilingualDataImportHdrInfo, this.CompanyUserInfo);
                }
                catch (Exception ex)
                {
                    Helper.LogError("Error while updating DataImportHDR - ", this.CompanyUserInfo);
                    Helper.LogError(ex, this.CompanyUserInfo);
                }
                try
                {
                    _MultilingualDataImportHdrInfo.SuccessEmailIDs = DataImportHelper.GetEmailIDWithSeprator(_MultilingualDataImportHdrInfo.NotifySuccessEmailIds) + DataImportHelper.GetEmailIDWithSeprator(_MultilingualDataImportHdrInfo.NotifySuccessUserEmailIds) + DataImportHelper.GetEmailIDWithSeprator(_MultilingualDataImportHdrInfo.WarningEmailIds);
                    _MultilingualDataImportHdrInfo.FailureEmailIDs = DataImportHelper.GetEmailIDWithSeprator(_MultilingualDataImportHdrInfo.NotifyFailureEmailIds) + DataImportHelper.GetEmailIDWithSeprator(_MultilingualDataImportHdrInfo.NotifyFailureUserEmailIds) + DataImportHelper.GetEmailIDWithSeprator(_MultilingualDataImportHdrInfo.WarningEmailIds); 
                    DataImportHelper.SendMailToUsers(_MultilingualDataImportHdrInfo, this.CompanyUserInfo);
                }
                catch (Exception ex)
                {
                    Helper.LogError("Error while sending mail - ", this.CompanyUserInfo);
                    Helper.LogError(ex, this.CompanyUserInfo);
                }
            }
        }

        #endregion
        #region Private Methods
        private void ExtractTransferAndProcessData()
        {
            DataTable dtExcelData = null;
            Helper.LogInfo("3. Start Reading Excel file: " + _MultilingualDataImportHdrInfo.PhysicalPath, this.CompanyUserInfo);
            dtExcelData = DataImportHelper.GetMultilingualImportDataTableFromExcel(_MultilingualDataImportHdrInfo.PhysicalPath, MultilingualUploadConstants.SheetName, this.CompanyUserInfo);

            if (ValidateSchemaForMultilingualData(dtExcelData))
            {
                Helper.LogInfo("4. Reading Excel file complete.", this.CompanyUserInfo);

                //Add additional fields to ExcelDataTabel
                AddDataImportIDToDataTable(dtExcelData);

                //Validate data length
                if (!_MultilingualDataImportHdrInfo.IsForceCommit)
                    ValidateDataLength(dtExcelData);

                //Transform and Process data 
                TransformAndProcessData(dtExcelData, _MultilingualDataImportHdrInfo);

                //Take Backup of Skystem ART Base Database
                DatabaseBackup oDatabaseBackup = new DatabaseBackup();
                oDatabaseBackup.BackupDatabaseSkyStemARTBase(this.CompanyUserInfo);
            }
        }

        private bool ValidateSchemaForMultilingualData(DataTable dtExcelData)
        {
            bool isValidSchema;
            bool columnFound;
            StringBuilder oSbError = new StringBuilder();

            //Get list of all mandatory fields
            List<string> MultilingualDataImporMandatoryFieldList = DataImportHelper.GetMultilingualDataImportAllMandatoryFields();

            //Check if all mandatory fields exists in DataTable from Excel
            foreach (string fieldName in MultilingualDataImporMandatoryFieldList)
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
            //in MultilingualDataImport object and throw an exception with generated message 
            if (!isValidSchema)
            {
                string errorMessage = Helper.GetSinglePhrase(5000165, 0, _MultilingualDataImportHdrInfo.LanguageID, _MultilingualDataImportHdrInfo.DefaultLanguageID, this.CompanyUserInfo);//Mandatory columns not present: {0}

                _MultilingualDataImportHdrInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
                _MultilingualDataImportHdrInfo.DataImportStatusID = (short)Enums.DataImportStatus.Failure;
                _MultilingualDataImportHdrInfo.ErrorMessageToSave = String.Format(errorMessage, oSbError.ToString());
                throw new Exception(String.Format(errorMessage, oSbError.ToString()));
            }
            return isValidSchema;
        }

        private void ValidateDataLength(DataTable dtExcelData)
        {
            StringBuilder oSBError = new StringBuilder();
            StringBuilder oSBWarning = new StringBuilder();
            string msg = Helper.GetDataLengthErrorMessage(ServiceConstants.DEFAULTBUSINESSENTITYID, _MultilingualDataImportHdrInfo.LanguageID, _MultilingualDataImportHdrInfo.DefaultLanguageID, this.CompanyUserInfo);
            string invalidDataMsg = Helper.GetInvalidDataErrorMessage(ServiceConstants.DEFAULTBUSINESSENTITYID, _MultilingualDataImportHdrInfo.LanguageID, _MultilingualDataImportHdrInfo.DefaultLanguageID, this.CompanyUserInfo);
            string msgWarning = Helper.GetDataWarningMessage(ServiceConstants.DEFAULTBUSINESSENTITYID, _MultilingualDataImportHdrInfo.LanguageID, _MultilingualDataImportHdrInfo.DefaultLanguageID, this.CompanyUserInfo);
            string msgDuplicateLabelID = Helper.GetSinglePhrase(5000324, ServiceConstants.DEFAULTBUSINESSENTITYID, _MultilingualDataImportHdrInfo.LanguageID, _MultilingualDataImportHdrInfo.DefaultLanguageID, this.CompanyUserInfo);
            string msgTransNotAvailable = Helper.GetSinglePhrase(5000325, ServiceConstants.DEFAULTBUSINESSENTITYID, _MultilingualDataImportHdrInfo.LanguageID, _MultilingualDataImportHdrInfo.DefaultLanguageID, this.CompanyUserInfo);
            string msgLabelIDNotFound = Helper.GetSinglePhrase(5000326, ServiceConstants.DEFAULTBUSINESSENTITYID, _MultilingualDataImportHdrInfo.LanguageID, _MultilingualDataImportHdrInfo.DefaultLanguageID, this.CompanyUserInfo);
            string msgTranslationNotValid = Helper.GetSinglePhrase(5000366, ServiceConstants.DEFAULTBUSINESSENTITYID, _MultilingualDataImportHdrInfo.LanguageID, _MultilingualDataImportHdrInfo.DefaultLanguageID, this.CompanyUserInfo);
            string msgFromLanguageDataMismatch = Helper.GetSinglePhrase(5000367, ServiceConstants.DEFAULTBUSINESSENTITYID, _MultilingualDataImportHdrInfo.LanguageID, _MultilingualDataImportHdrInfo.DefaultLanguageID, this.CompanyUserInfo);

            List<TranslationInfo> oTranslationInfoList = GetTranslations(_MultilingualDataImportHdrInfo.CompanyID, _MultilingualDataImportHdrInfo.FromLanguageID, _MultilingualDataImportHdrInfo.ToLanguageID);
            bool bLabelIDFound = false;

            for (int x = 0; x < dtExcelData.Rows.Count; x++)
            {
                DataRow dr = dtExcelData.Rows[x];
                string excelRowNumber = dr[AddedGLDataImportFields.EXCELROWNUMBER].ToString();
                // Errors
                try
                {
                    Int32 labelID = Int32.Parse(dr[MultilingualUploadConstants.Fields.LabelID].ToString(), System.Globalization.NumberStyles.Integer);
                    if (labelID < 1)
                        throw new Exception();
                    dr[MultilingualUploadConstants.Fields.LabelID] = labelID;
                    bLabelIDFound = false;
                    TranslationInfo oTranslationInfo = null;
                    if (oTranslationInfoList != null && oTranslationInfoList.Count > 0)
                    {
                        oTranslationInfo = oTranslationInfoList.Find(T => T.LabelID == labelID);
                        if (oTranslationInfo != null)
                            bLabelIDFound = true;
                    }
                    if (!bLabelIDFound)
                    {
                        oSBError.Append(String.Format(invalidDataMsg, MultilingualUploadConstants.Fields.LabelID, excelRowNumber) + ", " + msgLabelIDNotFound);
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    }
                    if (bLabelIDFound && !IsTranslationValid(oTranslationInfo.FromLanguagePhrase, dr[MultilingualUploadConstants.Fields.ToLanguage].ToString()))
                    {
                        oSBError.Append(String.Format(invalidDataMsg, MultilingualUploadConstants.Fields.ToLanguage, excelRowNumber) + ", " + msgTranslationNotValid);
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    }
                }
                catch
                {
                    oSBError.Append(String.Format(invalidDataMsg, MultilingualUploadConstants.Fields.LabelID, excelRowNumber));
                    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                }


                // Warnings
                if (dr[MultilingualUploadConstants.Fields.ToLanguage] != DBNull.Value)
                    dr[MultilingualUploadConstants.Fields.ToLanguage] = dr[MultilingualUploadConstants.Fields.ToLanguage].ToString().Trim();
                string phrase = dr[MultilingualUploadConstants.Fields.ToLanguage].ToString();
                if (string.IsNullOrEmpty(phrase))
                {
                    oSBWarning.Append(String.Format(msgWarning, excelRowNumber, msgTransNotAvailable));
                    oSBWarning.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                }
                for (int i = x - 1; i >= 0; i--)
                {
                    if (dtExcelData.Rows[x][MultilingualUploadConstants.Fields.LabelID].ToString() == dtExcelData.Rows[i][MultilingualUploadConstants.Fields.LabelID].ToString())
                    {
                        oSBWarning.Append(String.Format(msgWarning, excelRowNumber, msgDuplicateLabelID));
                        oSBWarning.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    }
                }
            }
            if (!oSBError.ToString().Equals(String.Empty))
            {
                _MultilingualDataImportHdrInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
                _MultilingualDataImportHdrInfo.DataImportStatusID = (short)Enums.DataImportStatus.Failure;
                _MultilingualDataImportHdrInfo.ErrorMessageToSave = oSBError.ToString();
                throw new Exception(oSBError.ToString());
            }
            if (!oSBWarning.ToString().Equals(String.Empty))
            {
                _MultilingualDataImportHdrInfo.DataImportStatus = DataImportStatus.DATAIMPORTWARNING;
                _MultilingualDataImportHdrInfo.DataImportStatusID = (short)Enums.DataImportStatus.Warning;
                _MultilingualDataImportHdrInfo.ErrorMessageToSave = oSBWarning.ToString();
                throw new Exception(oSBWarning.ToString());
            }
        }

        private bool IsTranslationValid(string fromPhrase, string toPhrase)
        {
            //Validate for Place Holders
            if (!string.IsNullOrEmpty(fromPhrase) && !string.IsNullOrEmpty(toPhrase))
            {
                int i = 0;
                string placeHolder = "{0}";
                while (fromPhrase.IndexOf(placeHolder) > 0)
                {
                    if (toPhrase.IndexOf(placeHolder) < 0)
                        return false;
                    i++;
                    placeHolder = "{" + i.ToString() + "}";
                }
            }
            return true;
        }

        private void AddDataImportIDToDataTable(DataTable dtExcelData)
        {
            dtExcelData.Columns.Add(AddedGLDataImportFields.DATAIMPORTID, typeof(System.Int32));
            dtExcelData.Columns.Add(AddedGLDataImportFields.EXCELROWNUMBER, typeof(System.Int32));

            for (int x = 0; x < dtExcelData.Rows.Count; x++)
            {
                dtExcelData.Rows[x][AddedGLDataImportFields.DATAIMPORTID] = _MultilingualDataImportHdrInfo.DataImportID;
                dtExcelData.Rows[x][AddedGLDataImportFields.EXCELROWNUMBER] = x + 2;
            }
        }

        private void TransformAndProcessData(DataTable dtExcelData, MultilingualDataImportHdrInfo _MultilingualDataImportHdrInfo)
        {
            MultilingualAttributeInfo oMultilingualAttributeInfo = Helper.GetMultilingualAttributeInfo(_MultilingualDataImportHdrInfo.LanguageID, _MultilingualDataImportHdrInfo.CompanyID);
            List<TranslationInfo> oTranslationInfoList = TransformData(dtExcelData, _MultilingualDataImportHdrInfo);
            int applicationID = SharedAppSettingHelper.GetApplicationID();
            int count = LanguageUtil.SaveTranslations(applicationID, _MultilingualDataImportHdrInfo.CompanyID, oTranslationInfoList, _MultilingualDataImportHdrInfo.AddedBy);
            if (count > 0)
            {
                _MultilingualDataImportHdrInfo.DataImportStatus = DataImportStatus.DATAIMPORTSUCCESS;
                _MultilingualDataImportHdrInfo.ErrorMessageToSave = LanguageUtil.GetValue(1743, oMultilingualAttributeInfo);
                _MultilingualDataImportHdrInfo.RecordsImported = count;
            }
            else
            {
                _MultilingualDataImportHdrInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
                _MultilingualDataImportHdrInfo.ErrorMessageToSave = LanguageUtil.GetValue(5000164, oMultilingualAttributeInfo);
            }
        }

        private List<TranslationInfo> TransformData(DataTable dtExcelData, MultilingualDataImportHdrInfo _MultilingualDataImportHdrInfo)
        {
            List<TranslationInfo> oTranslationInfoList = new List<TranslationInfo>();
            TranslationInfo oTranslationInfo = null;
            int labelID = 0;
            string phrase = string.Empty;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dtExcelData.Rows.Count; i++)
            {
                oTranslationInfo = new TranslationInfo();
                oTranslationInfo.FromLanguageID = _MultilingualDataImportHdrInfo.FromLanguageID;
                oTranslationInfo.ToLanguageID = _MultilingualDataImportHdrInfo.ToLanguageID;
                labelID = Convert.ToInt32(dtExcelData.Rows[i][MultilingualUploadConstants.Fields.LabelID]);
                phrase = dtExcelData.Rows[i][MultilingualUploadConstants.Fields.ToLanguage].ToString();
                if (labelID > 0 && !string.IsNullOrEmpty(phrase))
                {
                    oTranslationInfo.LabelID = labelID;
                    oTranslationInfo.ToLanguagePhrase = phrase;
                    oTranslationInfoList.Add(oTranslationInfo);
                }
            }
            return oTranslationInfoList;
        }

        public List<TranslationInfo> GetTranslations(int businessEntityID, int fromLanguageID, int toLanguageID)
        {
            int applicationID = SharedAppSettingHelper.GetApplicationID();
            int startLabelID = Convert.ToInt32(SharedAppSettingHelper.GetAppSettingValue(SharedAppSettingConstants.APP_KEY_START_LABEL_ID));
            List<TranslationInfo> oTranslationInfoList = LanguageUtil.GetTranslations(applicationID, businessEntityID, fromLanguageID, toLanguageID, startLabelID, null);
            return oTranslationInfoList;
        }

        #endregion
    }
}
