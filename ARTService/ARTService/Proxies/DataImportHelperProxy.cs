using System;
using System.Collections.Generic;
using System.Data;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Model.CompanyDatabase;
using SkyStem.ART.Service.Data;
using SkyStem.ART.Service.Interfaces;
using SkyStem.ART.Service.Model;
using SkyStem.ART.Service.Utility;
using ClientModel = SkyStem.ART.Client.Model;

namespace SkyStem.ART.Service.Proxies
{
    /// <summary>
    ///     Proxy class for consuming static DataImportHelper. Prefer using this class over DataImportHelper.
    ///     As DataImportHelper methods are extracted, inject them here and eventually remove the static class.
    /// </summary>
    public class DataImportHelperProxy : IDataImportHelper
    {
        public AccountAttributeDataImportInfo GetAcctAttrDataImportInfoForProcessing(DateTime dateRevised,
            CompanyUserInfo oCompanyUserInfo)
        {
            return DataImportHelper.GetAcctAttrDataImportInfoForProcessing(dateRevised, oCompanyUserInfo);
        }

        public List<CapabilityInfo> SelectAllCompanyCapabilityByReconciliationPeriodID(int recPeriodID,
            CompanyUserInfo oCompanyUserInfo)
        {
            return DataImportHelper.SelectAllCompanyCapabilityByReconciliationPeriodID(recPeriodID, oCompanyUserInfo);
        }

        public void TransferAndProcessData(DataTable dtExcel, AccountAttributeDataImportInfo oAcctAttrDataImportInfo,
            List<ClientModel.LogInfo> oLogInfoCache, CompanyUserInfo oCompanyUserInfo)
        {
            DataImportHelper.TransferAndProcessData(dtExcel, oAcctAttrDataImportInfo, oLogInfoCache, oCompanyUserInfo);
        }

        public void ProcessTransferredData(AccountAttributeDataImportInfo oAcctAttrDataImportInfo,
            List<ClientModel.LogInfo> oLogInfoCache, CompanyUserInfo oCompanyUserInfo)
        {
            DataImportHelper.ProcessTransferredData(oAcctAttrDataImportInfo, oLogInfoCache, oCompanyUserInfo);
        }

        public void UpdateDataImportHDR(AccountAttributeDataImportInfo oAcctAttrDataImportInfo,
            CompanyUserInfo oCompanyUserInfo)
        {
            DataImportHelper.UpdateDataImportHDR(oAcctAttrDataImportInfo, oCompanyUserInfo);
        }

        public DataTable GetAcctAttrImportDataTableFromExcel(string fullExcelFilePath, string sheetName,
            CompanyUserInfo oCompanyUserInfo)
        {
            return DataImportHelper.GetAcctAttrImportDataTableFromExcel(fullExcelFilePath, sheetName, oCompanyUserInfo);
        }

        public List<string> GetAcctAttrDataImportStaticFields()
        {
            return DataImportHelper.GetAcctAttrDataImportStaticFields();
        }

        public List<string> GetAcctAttrDataImportAllPossibleFields(DataImportHdrInfo oEntity)
        {
            return DataImportHelper.GetAcctAttrDataImportAllPossibleFields(oEntity);
        }

        public List<string> GetAcctAttrDataImportAllMandatoryFields(DataImportHdrInfo oEntity)
        {
            return DataImportHelper.GetAcctAttrDataImportAllMandatoryFields(oEntity);
        }

        public void ResetGLDataHdrObject(AccountAttributeDataImportInfo oAcctAttrDataImportInfo, Exception ex)
        {
            DataImportHelper.ResetGLDataHdrObject(oAcctAttrDataImportInfo, ex);
        }

        public GLDataImportInfo GetGLDataImportInfoForProcessing(DateTime dateRevised, CompanyUserInfo oCompanyUserInfo)
        {
            return DataImportHelper.GetGLDataImportInfoForProcessing(dateRevised, oCompanyUserInfo);
        }

        public void TransferAndProcessGLData(DataTable dtExcel, GLDataImportInfo oGLDataImportInfo,
            List<ClientModel.LogInfo> oLogInfoCache,
            CompanyUserInfo oCompanyUserInfo)
        {
            DataImportHelper.TransferAndProcessGLData(dtExcel, oGLDataImportInfo, oLogInfoCache, oCompanyUserInfo);
        }

        public void ProcessTransferedGLData(GLDataImportInfo oGLDataImportInfo, List<ClientModel.LogInfo> oLogInfoCache,
            CompanyUserInfo oCompanyUserInfo)
        {
            DataImportHelper.ProcessTransferedGLData(oGLDataImportInfo, oLogInfoCache, oCompanyUserInfo);
        }

        public void UpdateDataImportHDR(GLDataImportInfo oGLDataImportInfo, CompanyUserInfo oCompanyUserInfo)
        {
            DataImportHelper.UpdateDataImportHDR(oGLDataImportInfo, oCompanyUserInfo);
        }

        public void UpdateDataImportHDRForUserUpload(DataImportHdrInfo oUserUploadInfo,
            CompanyUserInfo oCompanyUserInfo)
        {
            DataImportHelper.UpdateDataImportHDRForUserUpload(oUserUploadInfo, oCompanyUserInfo);
        }

        public DataTable GetGLDataImportDataTableFromExcel(string fullExcelFilePath, string sheetName,
            CompanyUserInfo oCompanyUserInfo)
        {
            return DataImportHelper.GetGLDataImportDataTableFromExcel(fullExcelFilePath, sheetName, oCompanyUserInfo);
        }

        public List<string> GetGLDataImportStaticFields()
        {
            return DataImportHelper.GetGLDataImportStaticFields();
        }

        public List<string> GetAllPossibleGLDataImportFields(DataImportHdrInfo oEntity)
        {
            return DataImportHelper.GetAllPossibleGLDataImportFields(oEntity);
        }

        public List<string> GetGLDataImportAllMandatoryFields(DataImportHdrInfo oEntity)
        {
            return DataImportHelper.GetGLDataImportAllMandatoryFields(oEntity);
        }

        public List<ClientModel.AccountHdrInfo> GetAccountInformationWithoutGL(int? userID, short? roleID,
            int recPeriodID, int companyID)
        {
            return DataImportHelper.GetAccountInformationWithoutGL(userID, roleID, recPeriodID, companyID);
        }

        public List<ClientModel.AccountHdrInfo> GetNewAccounts(int? dataImportID, int companyID)
        {
            return DataImportHelper.GetNewAccounts(dataImportID, companyID);
        }

        public ClientModel.DataImportHdrInfo GetDataImportHdrInfo(int? dataImportID, int companyID)
        {
            return DataImportHelper.GetDataImportHdrInfo(dataImportID, companyID);
        }

        public DataTable RenameTemplateColumnNameToArtColumns(DataTable oGLDataTableFromExcel,
            List<ClientModel.ImportTemplateFieldMappingInfo> oImportTemplateFieldMappingInfoList)
        {
            return DataImportHelper.RenameTemplateColumnNameToARTColumns(oGLDataTableFromExcel,
                oImportTemplateFieldMappingInfoList);
        }

        public List<ClientModel.ImportTemplateFieldMappingInfo> GetImportTemplateFieldMappingInfoList(
            int? importTemplateID, int companyID)
        {
            return DataImportHelper.GetImportTemplateFieldMappingInfoList(importTemplateID, companyID);
        }

        public List<ClientModel.ImportTemplateFieldMappingInfo> GetAllDataImportFieldsWithMapping(int dataImportID,
            int companyID)
        {
            return DataImportHelper.GetAllDataImportFieldsWithMapping(dataImportID, companyID);
        }

        public ClientModel.ImportTemplateFieldMappingInfo GetImportTemplateField(
            List<ClientModel.ImportTemplateFieldMappingInfo> oImportTemplateFieldMappingInfoList, string importField)
        {
            return DataImportHelper.GetImportTemplateField(oImportTemplateFieldMappingInfoList, importField);
        }

        public void ResetGLDataHdrObject(GLDataImportInfo oGLDataImportInfo, Exception ex)
        {
            DataImportHelper.ResetGLDataHdrObject(oGLDataImportInfo, ex);
        }

        public DataTable ConvertDataImportStatusMessageToDataTable(
            List<ClientModel.DataImportMessageDetailInfo> oDataImportMessageDetailInfoList)
        {
            return DataImportHelper.ConvertDataImportStatusMessageToDataTable(oDataImportMessageDetailInfoList);
        }

        public SubledgerDataImportInfo GetSubledgerDataImportInfoForProcessing(DateTime dateRevised,
            CompanyUserInfo oCompanyUserInfo)
        {
            return DataImportHelper.GetSubledgerDataImportInfoForProcessing(dateRevised, oCompanyUserInfo);
        }

        public void TransferAndProcessSubledgerData(DataTable dtExcel, SubledgerDataImportInfo oSubledgerDataImportInfo,
            List<ClientModel.LogInfo> oLogInfoCache, CompanyUserInfo oCompanyUserInfo)
        {
            DataImportHelper.TransferAndProcessSubledgerData(dtExcel, oSubledgerDataImportInfo, oLogInfoCache,
                oCompanyUserInfo);
        }

        public void ProcessTransferedSubledgerData(SubledgerDataImportInfo oSubledgerDataImportInfo,
            List<ClientModel.LogInfo> oLogInfoCache,
            CompanyUserInfo companyUserInfo)
        {
            DataImportHelper.ProcessTransferedSubledgerData(oSubledgerDataImportInfo, oLogInfoCache, companyUserInfo);
        }

        public void UpdateDataImportHDR(SubledgerDataImportInfo oSubledgerDataImportInfo,
            CompanyUserInfo oCompanyUserInfo)
        {
            DataImportHelper.UpdateDataImportHDR(oSubledgerDataImportInfo, oCompanyUserInfo);
        }

        public DataTable GetSubledgerDataImportDataTableFromExcel(string fullExcelFilePath, string sheetName,
            CompanyUserInfo oCopmpnayUserInfo)
        {
            return DataImportHelper.GetSubledgerDataImportDataTableFromExcel(fullExcelFilePath, sheetName,
                oCopmpnayUserInfo);
        }

        public List<string> GetSubledgerDataImportStaticFields()
        {
            return DataImportHelper.GetSubledgerDataImportStaticFields();
        }

        public List<string> GetSubledgerDataImportAllMandatoryFields(DataImportHdrInfo oEntity)
        {
            return DataImportHelper.GetSubledgerDataImportAllMandatoryFields(oEntity);
        }

        public List<string> GetSubledgerDataImportAllPossibleMandatoryFields(DataImportHdrInfo oEntity)
        {
            return DataImportHelper.GetSubledgerDataImportAllPossibleMandatoryFields(oEntity);
        }

        public void ResetSubledgerDataHdrObject(SubledgerDataImportInfo oSubledgerDataImportInfo, Exception ex)
        {
            DataImportHelper.ResetSubledgerDataHdrObject(oSubledgerDataImportInfo, ex);
        }

        public MultilingualDataImportHdrInfo GetMultilingualDataImportInfoForProcessing(DateTime dateRevised,
            CompanyUserInfo oCompanyUserInfo)
        {
            return DataImportHelper.GetMultilingualDataImportInfoForProcessing(dateRevised, oCompanyUserInfo);
        }

        public void UpdateDataImportHDR(MultilingualDataImportHdrInfo oMultilingualDataImportHdrInfo,
            CompanyUserInfo oCompanyUserInfo)
        {
            DataImportHelper.UpdateDataImportHDR(oMultilingualDataImportHdrInfo, oCompanyUserInfo);
        }

        public DataTable GetMultilingualImportDataTableFromExcel(string fullExcelFilePath, string sheetName,
            CompanyUserInfo oCompanyUserInfo)
        {
            return DataImportHelper.GetMultilingualImportDataTableFromExcel(fullExcelFilePath, sheetName,
                oCompanyUserInfo);
        }

        public DataTable GetUserUploadDataTableFromExcel(string fullExcelFilePath, string sheetName,
            CompanyUserInfo oCompanyUserInfo)
        {
            return DataImportHelper.GetUserUploadDataTableFromExcel(fullExcelFilePath, sheetName, oCompanyUserInfo);
        }

        public List<string> GetMultilingualDataImportAllMandatoryFields()
        {
            return DataImportHelper.GetMultilingualDataImportAllMandatoryFields();
        }

        public void ResetMultilingualDataHdrObject(MultilingualDataImportHdrInfo oMultilingualDataImportHdrInfo,
            Exception ex)
        {
            DataImportHelper.ResetMultilingualDataHdrObject(oMultilingualDataImportHdrInfo, ex);
        }

        public List<string> GetUserUploadImportMandatoryFields()
        {
            return DataImportHelper.GetUserUploadImportMandatoryFields();
        }

        public DataTable GetUserUploadDataImportDataTableFromExcel(string fullExcelFilePath, string sheetName,
            CompanyUserInfo oCompanyUserInfo)
        {
            return DataImportHelper.GetUserUploadDataImportDataTableFromExcel(fullExcelFilePath, sheetName,
                oCompanyUserInfo);
        }

        public void ProcessTransferedAccountData(AccountDataImportInfo oAccountDataImportInfo,
            CompanyUserInfo oCompanyUserInfo)
        {
            DataImportHelper.ProcessTransferedAccountData(oAccountDataImportInfo, oCompanyUserInfo);
        }

        public AccountDataImportInfo GetAccountDataImportInfoForProcessing(DateTime dateRevised,
            CompanyUserInfo oCompanyUserInfo)
        {
            return DataImportHelper.GetAccountDataImportInfoForProcessing(dateRevised, oCompanyUserInfo);
        }

        public void ResetAccountDataHdrObject(AccountDataImportInfo oAccountDataImportInfo, Exception ex)
        {
            DataImportHelper.ResetAccountDataHdrObject(oAccountDataImportInfo, ex);
        }

        public void TransferAndProcessAccountData(DataTable dtExcel, AccountDataImportInfo oAccountDataImportInfo,
            CompanyUserInfo oCompanyUserHdrInfo)
        {
            DataImportHelper.TransferAndProcessAccountData(dtExcel, oAccountDataImportInfo, oCompanyUserHdrInfo);
        }

        public void UpdateDataImportHDR(AccountDataImportInfo oAccountDataImportInfo, CompanyUserInfo oCompanyUserInfo)
        {
            DataImportHelper.UpdateDataImportHDR(oAccountDataImportInfo, oCompanyUserInfo);
        }

        public void SendMailToUsers(DataImportHdrInfo oDataImportInfo, CompanyUserInfo oCompanyUserInfo)
        {
            DataImportHelper.SendMailToUsers(oDataImportInfo, oCompanyUserInfo);
        }

        public void SendMailToUsers(CurrencyDataImportInfo oCurrencyDataImportInfo, CompanyUserInfo oCompanyUserInfo)
        {
            DataImportHelper.SendMailToUsers(oCurrencyDataImportInfo, oCompanyUserInfo);
        }

        public void SendMailToUsers(GLDataImportInfo oGLDataImportInfo, CompanyUserInfo oCompanyUserInfo)
        {
            DataImportHelper.SendMailToUsers(oGLDataImportInfo, oCompanyUserInfo);
        }

        public void SendMailToUsers(SubledgerDataImportInfo oSubledgerDataImportInfo, CompanyUserInfo oCompanyUserInfo)
        {
            DataImportHelper.SendMailToUsers(oSubledgerDataImportInfo, oCompanyUserInfo);
        }

        public void SendMailToUsers(UserDataImportInfo oUserDataImportInfo, CompanyUserInfo oCompanyUserInfo)
        {
            DataImportHelper.SendMailToUsers(oUserDataImportInfo, oCompanyUserInfo);
        }

        public List<string> GetAccountStaticFields()
        {
            return DataImportHelper.GetAccountStaticFields();
        }

        public List<string> GetAllAccountCreationMendatoryFields()
        {
            return DataImportHelper.GetAllAccountCreationMendatoryFields();
        }

        public List<string> GetAccountKeyFields(DataImportHdrInfo oEntity)
        {
            return DataImportHelper.GetAccountKeyFields(oEntity);
        }

        public List<string> GetAccountUniqueSubsetFields(DataImportHdrInfo oEntity)
        {
            return DataImportHelper.GetAccountUniqueSubsetFields(oEntity);
        }

        public List<string> GetAccountMandatoryFields(DataImportHdrInfo oEntity)
        {
            return DataImportHelper.GetAccountMandatoryFields(oEntity);
        }

        public List<string> GetAccountMandatoryFieldsForAccountAttributeLoad(DataImportHdrInfo oEntity)
        {
            return DataImportHelper.GetAccountMandatoryFieldsForAccountAttributeLoad(oEntity);
        }

        public List<string> GetAllPossibleAccountFields(DataImportHdrInfo oEntity)
        {
            return DataImportHelper.GetAllPossibleAccountFields(oEntity);
        }

        public string GetBaseFolder()
        {
            return DataImportHelper.GetBaseFolder();
        }

        public string GetFolderForDownloadRequests(int companyID, int recPeriodID)
        {
            return DataImportHelper.GetFolderForDownloadRequests(companyID, recPeriodID);
        }

        public string GetAppSettingValue(string key)
        {
            return DataImportHelper.GetAppSettingValue(key);
        }

        public ReturnValue DeSerializeReturnValue(string xmlReturnString)
        {
            return DataImportHelper.DeSerializeReturnValue(xmlReturnString);
        }

        public List<ClientModel.ExchangeRateInfo> GetExchangeRateByRecPeriod(int RecPeriodID,
            CompanyUserInfo oCompanyUserInfo)
        {
            return DataImportHelper.GetExchangeRateByRecPeriod(RecPeriodID, oCompanyUserInfo);
        }

        public List<string> GetTaskImportMandatoryFields()
        {
            return DataImportHelper.GetTaskImportMandatoryFields();
        }

        public DataTable GetScheduleRecItemImportDataTableFromExcel(string fullExcelFilePath, string sheetName,
            CompanyUserInfo oCompanyUserInfo)
        {
            return DataImportHelper.GetScheduleRecItemImportDataTableFromExcel(fullExcelFilePath, sheetName,
                oCompanyUserInfo);
        }

        public List<string> GetScheduleRecItemImportAllMandatoryFields()
        {
            return DataImportHelper.GetScheduleRecItemImportAllMandatoryFields();
        }

        public void ResetScheduleRecItemDataHdrObject(ScheduleRecItemImportInfo oScheduleRecItemImportInfo,
            Exception ex)
        {
            DataImportHelper.ResetScheduleRecItemDataHdrObject(oScheduleRecItemImportInfo, ex);
        }

        public void ResetTaskImportInfoObject(TaskImportInfo oTaskImportInfo, Exception ex)
        {
            DataImportHelper.ResetTaskImportInfoObject(oTaskImportInfo, ex);
        }

        public CurrencyDataImportInfo GetCurrencyDataImportInfoForProcessing(DateTime dateRevised,
            CompanyUserInfo oCompanyUserInfo)
        {
            return DataImportHelper.GetCurrencyDataImportInfoForProcessing(dateRevised, oCompanyUserInfo);
        }

        public void TransferAndProcessCurrencyData(DataTable dtExcel, CurrencyDataImportInfo oCurrencyDataImportInfo,
            List<ClientModel.LogInfo> oLogInfoCache, CompanyUserInfo oCompanyUserInfo)
        {
            DataImportHelper.TransferAndProcessCurrencyData(dtExcel, oCurrencyDataImportInfo, oLogInfoCache,
                oCompanyUserInfo);
        }

        public void ProcessTransferedCurrencyData(CurrencyDataImportInfo oCurrencyDataImportInfo,
            List<ClientModel.LogInfo> oLogInfoCache,
            CompanyUserInfo oCompanyUserInfo)
        {
            DataImportHelper.ProcessTransferedCurrencyData(oCurrencyDataImportInfo, oLogInfoCache, oCompanyUserInfo);
        }

        public void UpdateDataImportHDR(CurrencyDataImportInfo oCurrencyDataImportInfo,
            CompanyUserInfo oCompanyUserInfo)
        {
            DataImportHelper.UpdateDataImportHDR(oCurrencyDataImportInfo, oCompanyUserInfo);
        }

        public DataTable GetCurrencyDataImportDataTableFromExcel(string fullExcelFilePath, string sheetName,
            CompanyUserInfo oCompanyUserInfo)
        {
            return DataImportHelper.GetCurrencyDataImportDataTableFromExcel(fullExcelFilePath, sheetName,
                oCompanyUserInfo);
        }

        public List<string> GetCurrencyDataImportMandatoryFields()
        {
            return DataImportHelper.GetCurrencyDataImportMandatoryFields();
        }

        public void ResetCurrencyDataHdrObject(CurrencyDataImportInfo oCurrencyDataImportInfo, Exception ex)
        {
            DataImportHelper.ResetCurrencyDataHdrObject(oCurrencyDataImportInfo, ex);
        }

        public List<ClientModel.AccountHdrInfo> GetAccountInformationForCompanyAlertMail(
            ClientModel.CompanyAlertDetailUserInfo oCompanyAlertDetailUserInfo)
        {
            return DataImportHelper.GetAccountInformationForCompanyAlertMail(oCompanyAlertDetailUserInfo);
        }

        public List<ClientModel.CompanyAlertInfo> GetRaiseAlertData(CompanyUserInfo oCompanyUserInfo)
        {
            return DataImportHelper.GetRaiseAlertData(oCompanyUserInfo);
        }

        public void CreateDataForCompanyAlertID(ClientModel.CompanyAlertInfo oCompanyAlertInfo,
            CompanyUserInfo oCompanyUserInfo)
        {
            DataImportHelper.CreateDataForCompanyAlertID(oCompanyAlertInfo, oCompanyUserInfo);
        }

        public List<ClientModel.CompanyAlertDetailUserInfo> GetAlertMailDataForCompanyAlertID(
            ClientModel.CompanyAlertInfo oCompanyAlertInfo, CompanyUserInfo oCompanyUserInfo)
        {
            return DataImportHelper.GetAlertMailDataForCompanyAlertID(oCompanyAlertInfo, oCompanyUserInfo);
        }

        public void UpdateSentMailStatus(List<ClientModel.CompanyAlertDetailUserInfo> oCompanyAlertDetailUserInfoList,
            CompanyUserInfo oCompanyUserInfo)
        {
            DataImportHelper.UpdateSentMailStatus(oCompanyAlertDetailUserInfoList, oCompanyUserInfo);
        }

        public List<ClientModel.CompanyAlertDetailUserInfo> GetUserListForNewAccountAlert(int dataImportID,
            int companyID, CompanyUserInfo oCompanyUserInfo)
        {
            return DataImportHelper.GetUserListForNewAccountAlert(dataImportID, companyID, oCompanyUserInfo);
        }

        public List<ClientModel.CompanyAlertDetailInfo> GetCompanyAlertDetail(long? companyAlertDetailID,
            CompanyUserInfo oCompanyUserInfo)
        {
            return DataImportHelper.GetCompanyAlertDetail(companyAlertDetailID, oCompanyUserInfo);
        }

        public List<ClientModel.TaskHdrInfo> GetTaskInformationForCompanyAlertMail(
            ClientModel.CompanyAlertDetailUserInfo oCompanyAlertDetailUserInfo)
        {
            return DataImportHelper.GetTaskInformationForCompanyAlertMail(oCompanyAlertDetailUserInfo);
        }

        public string GetSheetName(Enums.DataImportType dataImportType, int? importTemplateID, int? companyID)
        {
            return DataImportHelper.GetSheetName(dataImportType, importTemplateID, companyID);
        }

        public ClientModel.DataImportMessageInfo GetDataImportMessageInfo(short dataImportmessageID, int? companyID)
        {
            return DataImportHelper.GetDataImportMessageInfo(dataImportmessageID, companyID);
        }

        public DataTable CreateDataImportMessageTable()
        {
            return DataImportHelper.CreateDataImportMessageTable();
        }

        public string GetImportTemplateFieldName(
            ClientModel.ImportTemplateFieldMappingInfo oImportTemplateFieldMappingInfo, int languageID,
            int defaultLanguageID, CompanyUserInfo oCompanyUserInfo)
        {
            return DataImportHelper.GetImportTemplateFieldName(oImportTemplateFieldMappingInfo, languageID,
                defaultLanguageID, oCompanyUserInfo);
        }

        public DataTable CreateDataImportMandatoryFieldsNotPresentMessageTable()
        {
            return DataImportHelper.CreateDataImportMandatoryFieldsNotPresentMessageTable();
        }

        public List<ClientModel.AccountHdrInfo> GetAccountInformationWithBalanceChange(
            List<string> accountInfoCollection, int companyID)
        {
            return DataImportHelper.GetAccountInformationWithBalanceChange(accountInfoCollection, companyID);
        }

        public List<ClientModel.AccountHdrInfo> GetAccountInformationWithKeyValue(List<string> accountInfoCollection,
            int companyID)
        {
            return DataImportHelper.GetAccountInformationWithKeyValue(accountInfoCollection, companyID);
        }

        public ClientModel.UserHdrInfo GetUserDetail(int userID, int companyID)
        {
            return DataImportHelper.GetUserDetail(userID, companyID);
        }

        public List<ClientModel.UserFTPConfigurationInfo> GetFtpUsers(CompanyUserInfo oCompanyUserInfo)
        {
            return DataImportHelper.GetFTPUsers(oCompanyUserInfo);
        }

        public ClientModel.ReconciliationPeriodInfo GetReconciliationPeriodInfo(DateTime? recPeriodEndDate,
            int? companyID)
        {
            return DataImportHelper.GetReconciliationPeriodInfo(recPeriodEndDate, companyID);
        }

        public ClientModel.SystemLockdownInfo GetSystemLockdownInfo(ARTEnums.SystemLockdownReason eSystemLockdownReason,
            ClientModel.UserFTPConfigurationInfo oUserFtpConfigurationInfo, int? recPeriodID)
        {
            return DataImportHelper.GetSystemLockdownInfo(eSystemLockdownReason, oUserFtpConfigurationInfo,
                recPeriodID);
        }

        public void InsertDataImportHdr(ClientModel.UserFTPConfigurationInfo oUserFtpConfigurationInfo,
            ClientModel.DataImportHdrInfo oDataImportHrdInfo)
        {
            DataImportHelper.InsertDataImportHdr(oUserFtpConfigurationInfo, oDataImportHrdInfo);
        }

        public short? GetKeyCount(ClientModel.UserFTPConfigurationInfo oUserFtpConfigurationInfo)
        {
            return DataImportHelper.GetKeyCount(oUserFtpConfigurationInfo);
        }

        public List<string> GetDataImportAllMandatoryFields(int companyID, int recPeriodID)
        {
            return DataImportHelper.GetDataImportAllMandatoryFields(companyID, recPeriodID);
        }

        public List<string> GetAccountKeyFields(int companyID)
        {
            return DataImportHelper.GetAccountKeyFields(companyID);
        }

        public List<string> GetAccountUniqueSubsetFields(int companyID, int recPeriodID)
        {
            return DataImportHelper.GetAccountUniqueSubsetFields(companyID, recPeriodID);
        }

        public List<string> GetImportTemplateMandatoryFields(int? companyID, int? importTemplateID,
            List<string> mandatoryFieldList)
        {
            return DataImportHelper.GetImportTemplateMandatoryFields(companyID, importTemplateID, mandatoryFieldList);
        }

        public List<string> GetAllMandatoryFields(int? companyID, int? importTemplateID, int recPeriodID)
        {
            return DataImportHelper.GetAllMandatoryFields(companyID, importTemplateID, recPeriodID);
        }

        public string GetEmailIDWithSeprator(string mailidList)
        {
            return DataImportHelper.GetEmailIDWithSeprator(mailidList);
        }
    }
}