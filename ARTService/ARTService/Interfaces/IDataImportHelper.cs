using System;
using System.Collections.Generic;
using System.Data;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Model.CompanyDatabase;
using SkyStem.ART.Service.Data;
using SkyStem.ART.Service.Model;
using SkyStem.ART.Service.Utility;
using DataImportHdrInfo = SkyStem.ART.Service.Model.DataImportHdrInfo;

namespace SkyStem.ART.Service.Interfaces
{
    public interface IDataImportHelper
    {
        AccountAttributeDataImportInfo GetAcctAttrDataImportInfoForProcessing(DateTime dateRevised,
            CompanyUserInfo oCompanyUserInfo);

        List<CapabilityInfo> SelectAllCompanyCapabilityByReconciliationPeriodID(int recPeriodID,
            CompanyUserInfo oCompanyUserInfo);

        void TransferAndProcessData(DataTable dtExcel, AccountAttributeDataImportInfo oAcctAttrDataImportInfo,
            List<LogInfo> oLogInfoCache, CompanyUserInfo oCompanyUserInfo);

        void ProcessTransferredData(AccountAttributeDataImportInfo oAcctAttrDataImportInfo, List<LogInfo> oLogInfoCache,
            CompanyUserInfo oCompanyUserInfo);

        void UpdateDataImportHDR(AccountAttributeDataImportInfo oAcctAttrDataImportInfo,
            CompanyUserInfo oCompanyUserInfo);

        DataTable GetAcctAttrImportDataTableFromExcel(string fullExcelFilePath, string sheetName,
            CompanyUserInfo oCompanyUserInfo);

        List<string> GetAcctAttrDataImportStaticFields();
        List<string> GetAcctAttrDataImportAllPossibleFields(DataImportHdrInfo oEntity);
        List<string> GetAcctAttrDataImportAllMandatoryFields(DataImportHdrInfo oEntity);
        void ResetGLDataHdrObject(AccountAttributeDataImportInfo oAcctAttrDataImportInfo, Exception ex);
        GLDataImportInfo GetGLDataImportInfoForProcessing(DateTime dateRevised, CompanyUserInfo oCompanyUserInfo);

        void TransferAndProcessGLData(DataTable dtExcel, GLDataImportInfo oGLDataImportInfo,
            List<LogInfo> oLogInfoCache, CompanyUserInfo oCompanyUserInfo);

        void ProcessTransferedGLData(GLDataImportInfo oGLDataImportInfo, List<LogInfo> oLogInfoCache,
            CompanyUserInfo oCompanyUserInfo);

        void UpdateDataImportHDR(GLDataImportInfo oGLDataImportInfo, CompanyUserInfo oCompanyUserInfo);
        void UpdateDataImportHDRForUserUpload(DataImportHdrInfo oUserUploadInfo, CompanyUserInfo oCompanyUserInfo);

        DataTable GetGLDataImportDataTableFromExcel(string fullExcelFilePath, string sheetName,
            CompanyUserInfo oCompanyUserInfo);

        List<string> GetGLDataImportStaticFields();
        List<string> GetAllPossibleGLDataImportFields(DataImportHdrInfo oEntity);
        List<string> GetGLDataImportAllMandatoryFields(DataImportHdrInfo oEntity);
        List<AccountHdrInfo> GetAccountInformationWithoutGL(int? userID, short? roleID, int recPeriodID, int companyID);
        List<AccountHdrInfo> GetNewAccounts(int? dataImportID, int companyID);
        SkyStem.ART.Client.Model.DataImportHdrInfo GetDataImportHdrInfo(int? dataImportID, int companyID);

        DataTable RenameTemplateColumnNameToArtColumns(DataTable oGLDataTableFromExcel,
            List<ImportTemplateFieldMappingInfo> oImportTemplateFieldMappingInfoList);

        List<ImportTemplateFieldMappingInfo>
            GetImportTemplateFieldMappingInfoList(int? importTemplateID, int companyID);

        List<ImportTemplateFieldMappingInfo> GetAllDataImportFieldsWithMapping(int dataImportID, int companyID);

        ImportTemplateFieldMappingInfo GetImportTemplateField(
            List<ImportTemplateFieldMappingInfo> oImportTemplateFieldMappingInfoList, string importField);

        void ResetGLDataHdrObject(GLDataImportInfo oGLDataImportInfo, Exception ex);

        DataTable ConvertDataImportStatusMessageToDataTable(
            List<DataImportMessageDetailInfo> oDataImportMessageDetailInfoList);

        SubledgerDataImportInfo GetSubledgerDataImportInfoForProcessing(DateTime dateRevised,
            CompanyUserInfo oCompanyUserInfo);

        void TransferAndProcessSubledgerData(DataTable dtExcel, SubledgerDataImportInfo oSubledgerDataImportInfo,
            List<LogInfo> oLogInfoCache, CompanyUserInfo oCompanyUserInfo);

        void ProcessTransferedSubledgerData(SubledgerDataImportInfo oSubledgerDataImportInfo,
            List<LogInfo> oLogInfoCache, CompanyUserInfo companyUserInfo);

        void UpdateDataImportHDR(SubledgerDataImportInfo oSubledgerDataImportInfo, CompanyUserInfo oCompanyUserInfo);

        DataTable GetSubledgerDataImportDataTableFromExcel(string fullExcelFilePath, string sheetName,
            CompanyUserInfo oCopmpnayUserInfo);

        List<string> GetSubledgerDataImportStaticFields();
        List<string> GetSubledgerDataImportAllMandatoryFields(DataImportHdrInfo oEntity);
        List<string> GetSubledgerDataImportAllPossibleMandatoryFields(DataImportHdrInfo oEntity);
        void ResetSubledgerDataHdrObject(SubledgerDataImportInfo oSubledgerDataImportInfo, Exception ex);

        MultilingualDataImportHdrInfo GetMultilingualDataImportInfoForProcessing(DateTime dateRevised,
            CompanyUserInfo oCompanyUserInfo);

        void UpdateDataImportHDR(MultilingualDataImportHdrInfo oMultilingualDataImportHdrInfo,
            CompanyUserInfo oCompanyUserInfo);

        DataTable GetMultilingualImportDataTableFromExcel(string fullExcelFilePath, string sheetName,
            CompanyUserInfo oCompanyUserInfo);

        DataTable GetUserUploadDataTableFromExcel(string fullExcelFilePath, string sheetName,
            CompanyUserInfo oCompanyUserInfo);

        List<string> GetMultilingualDataImportAllMandatoryFields();
        void ResetMultilingualDataHdrObject(MultilingualDataImportHdrInfo oMultilingualDataImportHdrInfo, Exception ex);
        List<string> GetUserUploadImportMandatoryFields();

        DataTable GetUserUploadDataImportDataTableFromExcel(string fullExcelFilePath, string sheetName,
            CompanyUserInfo oCompanyUserInfo);

        void ProcessTransferedAccountData(AccountDataImportInfo oAccountDataImportInfo,
            CompanyUserInfo oCompanyUserInfo);

        AccountDataImportInfo GetAccountDataImportInfoForProcessing(DateTime dateRevised,
            CompanyUserInfo oCompanyUserInfo);

        void ResetAccountDataHdrObject(AccountDataImportInfo oAccountDataImportInfo, Exception ex);

        void TransferAndProcessAccountData(DataTable dtExcel, AccountDataImportInfo oAccountDataImportInfo,
            CompanyUserInfo oCompanyUserHdrInfo);

        void UpdateDataImportHDR(AccountDataImportInfo oAccountDataImportInfo, CompanyUserInfo oCompanyUserInfo);
        void SendMailToUsers(DataImportHdrInfo oDataImportInfo, CompanyUserInfo oCompanyUserInfo);
        void SendMailToUsers(CurrencyDataImportInfo oCurrencyDataImportInfo, CompanyUserInfo oCompanyUserInfo);
        void SendMailToUsers(GLDataImportInfo oGLDataImportInfo, CompanyUserInfo oCompanyUserInfo);
        void SendMailToUsers(SubledgerDataImportInfo oSubledgerDataImportInfo, CompanyUserInfo oCompanyUserInfo);
        void SendMailToUsers(UserDataImportInfo oUserDataImportInfo, CompanyUserInfo oCompanyUserInfo);
        List<string> GetAccountStaticFields();
        List<string> GetAllAccountCreationMendatoryFields();
        List<string> GetAccountKeyFields(DataImportHdrInfo oEntity);
        List<string> GetAccountUniqueSubsetFields(DataImportHdrInfo oEntity);
        List<string> GetAccountMandatoryFields(DataImportHdrInfo oEntity);
        List<string> GetAccountMandatoryFieldsForAccountAttributeLoad(DataImportHdrInfo oEntity);
        List<string> GetAllPossibleAccountFields(DataImportHdrInfo oEntity);

        /// <summary>
        ///     Gets folder name for import type as per company id
        /// </summary>
        /// <returns>folder name</returns>
        string GetBaseFolder();

        string GetFolderForDownloadRequests(int companyID, int recPeriodID);
        string GetAppSettingValue(string key);

        /// <summary>
        ///     Deserialize Return Value
        /// </summary>
        /// <param name="xmlReturnString"></param>
        /// <returns></returns>
        ReturnValue DeSerializeReturnValue(string xmlReturnString);

        List<ExchangeRateInfo> GetExchangeRateByRecPeriod(int RecPeriodID, CompanyUserInfo oCompanyUserInfo);
        List<string> GetTaskImportMandatoryFields();

        DataTable GetScheduleRecItemImportDataTableFromExcel(string fullExcelFilePath, string sheetName,
            CompanyUserInfo oCompanyUserInfo);

        List<string> GetScheduleRecItemImportAllMandatoryFields();
        void ResetScheduleRecItemDataHdrObject(ScheduleRecItemImportInfo oScheduleRecItemImportInfo, Exception ex);
        void ResetTaskImportInfoObject(TaskImportInfo oTaskImportInfo, Exception ex);

        CurrencyDataImportInfo GetCurrencyDataImportInfoForProcessing(DateTime dateRevised,
            CompanyUserInfo oCompanyUserInfo);

        void TransferAndProcessCurrencyData(DataTable dtExcel, CurrencyDataImportInfo oCurrencyDataImportInfo,
            List<LogInfo> oLogInfoCache, CompanyUserInfo oCompanyUserInfo);

        void ProcessTransferedCurrencyData(CurrencyDataImportInfo oCurrencyDataImportInfo, List<LogInfo> oLogInfoCache,
            CompanyUserInfo oCompanyUserInfo);

        void UpdateDataImportHDR(CurrencyDataImportInfo oCurrencyDataImportInfo, CompanyUserInfo oCompanyUserInfo);

        DataTable GetCurrencyDataImportDataTableFromExcel(string fullExcelFilePath, string sheetName,
            CompanyUserInfo oCompanyUserInfo);

        List<string> GetCurrencyDataImportMandatoryFields();
        void ResetCurrencyDataHdrObject(CurrencyDataImportInfo oCurrencyDataImportInfo, Exception ex);

        List<AccountHdrInfo> GetAccountInformationForCompanyAlertMail(
            CompanyAlertDetailUserInfo oCompanyAlertDetailUserInfo);

        List<CompanyAlertInfo> GetRaiseAlertData(CompanyUserInfo oCompanyUserInfo);
        void CreateDataForCompanyAlertID(CompanyAlertInfo oCompanyAlertInfo, CompanyUserInfo oCompanyUserInfo);

        List<CompanyAlertDetailUserInfo> GetAlertMailDataForCompanyAlertID(CompanyAlertInfo oCompanyAlertInfo,
            CompanyUserInfo oCompanyUserInfo);

        void UpdateSentMailStatus(List<CompanyAlertDetailUserInfo> oCompanyAlertDetailUserInfoList,
            CompanyUserInfo oCompanyUserInfo);

        List<CompanyAlertDetailUserInfo> GetUserListForNewAccountAlert(int dataImportID, int companyID,
            CompanyUserInfo oCompanyUserInfo);

        List<CompanyAlertDetailInfo>
            GetCompanyAlertDetail(long? companyAlertDetailID, CompanyUserInfo oCompanyUserInfo);

        List<TaskHdrInfo> GetTaskInformationForCompanyAlertMail(CompanyAlertDetailUserInfo oCompanyAlertDetailUserInfo);
        string GetSheetName(Enums.DataImportType dataImportType, int? importTemplateID, int? companyID);
        DataImportMessageInfo GetDataImportMessageInfo(short dataImportmessageID, int? companyID);
        DataTable CreateDataImportMessageTable();

        string GetImportTemplateFieldName(ImportTemplateFieldMappingInfo oImportTemplateFieldMappingInfo,
            int languageID, int defaultLanguageID, CompanyUserInfo oCompanyUserInfo);

        DataTable CreateDataImportMandatoryFieldsNotPresentMessageTable();
        List<AccountHdrInfo> GetAccountInformationWithBalanceChange(List<string> accountInfoCollection, int companyID);
        List<AccountHdrInfo> GetAccountInformationWithKeyValue(List<string> accountInfoCollection, int companyID);
        UserHdrInfo GetUserDetail(int userID, int companyID);
        List<UserFTPConfigurationInfo> GetFtpUsers(CompanyUserInfo oCompanyUserInfo);
        ReconciliationPeriodInfo GetReconciliationPeriodInfo(DateTime? recPeriodEndDate, int? companyID);

        SystemLockdownInfo GetSystemLockdownInfo(ARTEnums.SystemLockdownReason eSystemLockdownReason,
            UserFTPConfigurationInfo oUserFtpConfigurationInfo, int? recPeriodID);

        void InsertDataImportHdr(UserFTPConfigurationInfo oUserFtpConfigurationInfo,
            SkyStem.ART.Client.Model.DataImportHdrInfo oDataImportHrdInfo);

        short? GetKeyCount(UserFTPConfigurationInfo oUserFtpConfigurationInfo);
        List<string> GetDataImportAllMandatoryFields(int companyID, int recPeriodID);
        List<string> GetAccountKeyFields(int companyID);
        List<string> GetAccountUniqueSubsetFields(int companyID, int recPeriodID);

        List<string> GetImportTemplateMandatoryFields(int? companyID, int? importTemplateID,
            List<string> mandatoryFieldList);

        List<string> GetAllMandatoryFields(int? companyID, int? importTemplateID, int recPeriodID);
        string GetEmailIDWithSeprator(string mailidList);

    }
}