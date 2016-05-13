using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Params.RecItemUpload;
using SkyStem.ART.Client.Params;
using SkyStem.ART.Client.Model.RecControlCheckList;
using System.Data;

namespace SkyStem.ART.Client.IServices
{
    // NOTE: If you change the interface name "IDataImport" here, you must also update the reference to "IDataImport" in Web.config.
    [ServiceContract]
    public interface IDataImport
    {
        [OperationContract]
        void InsertDataImportHolidayCalendar(DataImportHdrInfo newDataImport, List<HolidayCalendarInfo> newHolidayCalendarList, string failureMsg, out int rowAffected, AppUserInfo oAppUserInfo);

        [OperationContract]
        long GetAvailableFileStorageSpaceByCompanyID(int companyID, AppUserInfo oAppUserInfo);

        [OperationContract]
        decimal? GetMaxFileSizeByCompanyID(int companyID, AppUserInfo oAppUserInfo);

        [OperationContract]
        bool isFirstTimeGLDataImportByCompanyID(int companyID, short DataImportTypeID, short FailureStatusID, AppUserInfo oAppUserInfo);

        [OperationContract]
        void InsertDataImportRecPeriod(DataImportHdrInfo oDataImportHdrInfo, List<ReconciliationPeriodInfo> oRecPeriodCollection, string failureMsg, DateTime? currentReconciliationPeriodEndDate, out int rowAffected, AppUserInfo oAppUserInfo);

        [OperationContract]
        void InsertDataImportWithFailureMsg(DataImportHdrInfo oDataImportHdrInfo, string failureMsg, AppUserInfo oAppUserInfo);

        [OperationContract]
        bool InsertDataImportGLData(DataImportHdrInfo oDataImportHdrInfo
            , List<GeographyStructureHdrInfo> oGeoStructCollection, string failureMsg, short companyGeographyClassID, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Get the Data Import Info for the Data Import ID
        /// </summary>
        /// <param name="DataImportID"></param>
        /// <returns></returns>
        [OperationContract]
        DataImportHdrInfo GetDataImportInfo(int? DataImportID, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Get Data Imports done based on the Company
        /// for Rec Period Upload, and
        /// Holiday Calendar Upload
        /// </summary>
        /// <param name="CompanyID">Company ID for which Data Imports are to be fetched</param>
        /// <returns></returns>
        [OperationContract]
        List<DataImportHdrInfo> GetDataImportStatusByCompanyID(int? CompanyID, int? UserID, short? RoleID, AppUserInfo oAppUserInfo);


        /// <summary>
        /// Get all the Data Imports done for the specified Rec Period
        /// </summary>
        /// <param name="RecPeriodID">Rec Period ID for which Data Imports are to be fetched</param>
        /// <returns></returns>

        [OperationContract]
        List<DataImportHdrInfo> GetDataImportStatusByUserID(int? RecPeriodID, bool showHiddenRows, int? UserID, short? RoleID, AppUserInfo oAppUserInfo);


        [OperationContract]
        void InsertDataImportExchangeRate(DataImportHdrInfo oDataImportHdrInfo
           , List<ExchangeRateInfo> oExchangeRateInfoCollection, string failureMsg, List<CurrencyCodeInfo> oCurrencyCodeInfoCollection, out int rowAffected, AppUserInfo oAppUserInfo);

        [OperationContract]
        void InsertDataImportSubledgerSourceRate(DataImportHdrInfo oDataImportHdrInfo
          , List<SubledgerSourceInfo> oSubledgerSourceInfoCollection, string failureMsg, out int rowAffected, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Inserts GL Data Rec Items from uploaded file
        /// </summary>
        /// <param name="oDataImportHdrInfo"></param>
        /// <param name="oGLDataRecItemInfoCollection"></param>
        /// <param name="failureMsg"></param>
        /// <param name="rowAffected"></param>
        [OperationContract]
        void InsertDataImportGLDataRecItem(DataImportHdrInfo oDataImportHdrInfo
            , List<GLDataRecItemInfo> oGLDataRecItemInfoCollection, string failureMsg, out int rowAffected, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Function to Update Force Commit Status for the Data Import Record
        /// </summary>
        /// <param name="oDataImportHdrInfo"></param>
        [OperationContract]
        void UpdateDataImportForForceCommit(DataImportHdrInfo oDataImportHdrInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        void InsertDataImportGLDataRecItemSchedule(DataImportHdrInfo oDataImportHdrInfo
           , List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection, string failureMsg, out int rowAffected, AppUserInfo oAppUserInfo);


        [OperationContract]
        int GetMaxFileSizeByCompanyIDInt(int companyID, AppUserInfo oAppUserInfo);

        [OperationContract]
        short? isKeyMappingDoneByCompanyID(int companyID, AppUserInfo oAppUserInfo);

        [OperationContract]
        void InsertDataImportWithFailureMsgAndKeyCount(DataImportHdrInfo oDataImportHdrInfo, string failureMsg, short keyCount, AppUserInfo oAppUserInfo);

        [OperationContract]
        int? DeleteDataImportByDataImportIDs(List<int> oDataImportIDCollection, int companyID
            , int recPeriodID
            , string revisedBy
            , DateTime dateRevised
            , short NotStartedRecPeriodStatusID
            , AppUserInfo oAppUserInfo);

        [OperationContract]
        void UpdateDataImportHiddenStatusByDataImportID(int dataImportID, bool hiddenStatus, AppUserInfo oAppUserInfo);

        bool IsGLDataUploaded(int recPeriodID, byte DataImportID, byte DataImportStatusID, AppUserInfo oAppUserInfo);

        [OperationContract]
        bool IsAnyAccountAssigned(short? roleID, int? userID, int? recPeriodID, AppUserInfo oAppUserInfo);
        [OperationContract]
        void InsertMatchingGLDataRecItem(List<GLDataRecItemInfo> oGLDataRecItemInfoCollection, out int rowAffected, AppUserInfo oAppUserInfo);
        [OperationContract]
        void InsertMatchingGLDataScheduleRecItem(List<GLDataRecurringItemScheduleInfo> oGLDataRecItemInfoCollection, out int rowAffected, AppUserInfo oAppUserInfo);
        [OperationContract]
        void InsertMatchingGLDataWriteOnOffRecItem(List<GLDataWriteOnOffInfo> oGLDataWriteOnOffInfoCollection, out int rowAffected, AppUserInfo oAppUserInfo);
        [OperationContract]
        void CloseMatchingGLDataRecItem(List<GLDataRecItemInfo> oGLDataRecItemInfoCollection, DateTime? CloseDate, out int rowAffected, AppUserInfo oAppUserInfo);
        [OperationContract]
        void CloseMatchingGLDataWriteOnOffItem(List<GLDataWriteOnOffInfo> oGLDataWriteOnOffInfoCollection, DateTime? CloseDate, out int rowAffected, AppUserInfo oAppUserInfo);
        [OperationContract]
        void CloseMatchingRecurringScheduleItems(List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection, DateTime? CloseDate, out int rowAffected, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<DataImportHdrInfo> GetRecItemDataImportStatus(RecItemUploadParamInfo oRecItemUploadParamInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        int? InsertGLDataRecItemScheduleBulk(Int64? GLDataID, Int32? RecPeriodID,
                    List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoList,
                    string addedBy, DateTime? dateAdded, AppUserInfo oAppUserInfo);
        [OperationContract]
        List<DataImportHdrInfo> GetGeneralTaskImportStatus(DataImportParamInfo oRecItemUploadParamInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        int SaveImportTemplate(ImportTemplateHdrInfo oImportTemplateInfo, DataTable dt, AppUserInfo oAppUserInfo);

        [OperationContract]
        ImportTemplateHdrInfo GetTemplateFields(int TemplateId, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<ImportFieldMstInfo> GetFieldsMst(int CompanyID, short? DataImportTypeID, AppUserInfo appUserInfo);

        [OperationContract]
        int SaveImportTemplateMapping(DataTable dt, ImportTemplateFieldMappingInfo oImportTemplateFieldMappingInfo, AppUserInfo appUserInfo);

        [OperationContract]
        List<ImportTemplateHdrInfo> GetAllTemplateImport(int CompanyID,int UserID, int RoleId, AppUserInfo appUserInfo);

        [OperationContract]
        void DeleteMappingData(DataTable dt, ImportTemplateHdrInfo oImportTemplateInfo, AppUserInfo appUserInfo);
        [OperationContract]
        string GetImportTemplateSheetName(int ImportTemplateID, AppUserInfo oAppUserInfo);
        [OperationContract]
        List<ImportTemplateFieldMappingInfo> GetImportTemplateFieldMappingInfoList(int? ImportTemplateID, AppUserInfo oAppUserInfo);
        [OperationContract]
        int SaveDataImportSchedule(DataImportScheduleInfo oDataImportScheduleInfo, DataTable dt, AppUserInfo appUserInfo);
        [OperationContract]
        List<DataImportScheduleInfo> GetDataImportSchedule(int? UserID, short? RoleID, AppUserInfo appUserInfo);
        [OperationContract]
        List<DataImportMessageInfo> GetAllWarningMsg(short DataImportTypeId, AppUserInfo appUserInfo);
        [OperationContract]
        int SaveDataImportWarningPreferences(DataTable dt, DataImportWarningPreferencesInfo oDataImportWarningPreferencesInfo, AppUserInfo appUserInfo);
        [OperationContract]
        List<DataImportWarningPreferencesInfo> GetDataImportWarningPreferences(int? CurrentCompanyID, short DataImportType, AppUserInfo appUserInfo);

        [OperationContract]
        List<ImportTemplateFieldMappingInfo> GetAllDataImportFieldsWithMapping(int dataImportID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<DataImportMessageInfo> GetDataImportMessageList(AppUserInfo oAppUserInfo);

        [OperationContract]
        List<DataImportWarningPreferencesAuditInfo> GetAllWarningAuditList(int CurrentCompanyID,int CurrentUserID, short CurrentRoleID, AppUserInfo appUserInfo);
    }
}
