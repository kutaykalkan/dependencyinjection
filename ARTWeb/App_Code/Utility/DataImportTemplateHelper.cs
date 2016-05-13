using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Shared.Data;
using SkyStem.ART.Web.Data;
using SkyStem.Language.LanguageUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace SkyStem.ART.Web.Utility
{
    /// <summary>
    /// Summary description for DataImportTemplateHelper
    /// </summary>
    public class DataImportTemplateHelper
    {
        public DataImportTemplateHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static int SaveImportTemplate(ImportTemplateHdrInfo oImportTemplateInfo, DataTable dt)
        {
            int result = 0;
            Helper.GetAppUserInfo();
            IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
            result = oDataImportClient.SaveImportTemplate(oImportTemplateInfo, dt, Helper.GetAppUserInfo());
            return result;
        }

        public static ImportTemplateHdrInfo GetTemplateFields(int TemplateId)
        {
            IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
            ImportTemplateHdrInfo oImportTemplateInfo = oDataImportClient.GetTemplateFields(TemplateId, Helper.GetAppUserInfo());
            return oImportTemplateInfo;
        }

        public static List<ImportFieldMstInfo> GetFieldsMst(int CompanyID, short? DataImportTypeID)
        {
            List<ImportFieldMstInfo> oImportFieldMstInfoLst = new List<ImportFieldMstInfo>();
            oImportFieldMstInfoLst = SessionHelper.GetFieldsMst(CompanyID, DataImportTypeID);
            return oImportFieldMstInfoLst;
        }

        public static int SaveImportTemplateMapping(DataTable dt, ImportTemplateFieldMappingInfo oImportTemplateFieldMappingInfo)
        {
            int result = 0;
            Helper.GetAppUserInfo();
            IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
            result = oDataImportClient.SaveImportTemplateMapping(dt, oImportTemplateFieldMappingInfo, Helper.GetAppUserInfo());
            return result;
        }

        public static List<ImportTemplateHdrInfo> GetAllTemplateImport(int CompanyID,int UserID,int RoleID)
        {
            List<ImportTemplateHdrInfo> oImportTemplateInfoLst = new List<ImportTemplateHdrInfo>();
            IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
            oImportTemplateInfoLst = oDataImportClient.GetAllTemplateImport(CompanyID,UserID,RoleID, Helper.GetAppUserInfo());
            oImportTemplateInfoLst = LanguageHelper.TranslateLabelImportTemplateHdrInfo(oImportTemplateInfoLst);
            return oImportTemplateInfoLst;

        }

        public static List<ImportTemplateFieldMappingInfo> GetTemplateFieldMappingData(int ImportTemplateID)
        {
            IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
            List<ImportTemplateFieldMappingInfo> oImportTemplateInfo = oDataImportClient.GetImportTemplateFieldMappingInfoList(ImportTemplateID, Helper.GetAppUserInfo());
            return oImportTemplateInfo;
        }

        public static void DeleteMappingData(DataTable dt, ImportTemplateHdrInfo oImportTemplateInfo)
        {
            Helper.GetAppUserInfo();
            IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
            oDataImportClient.DeleteMappingData(dt, oImportTemplateInfo, Helper.GetAppUserInfo());
        }

        public static int SaveDataImportSchedule(DataImportScheduleInfo oDataImportScheduleInfo, DataTable dt)
        {
            int result = 0;
            Helper.GetAppUserInfo();
            IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
            result = oDataImportClient.SaveDataImportSchedule(oDataImportScheduleInfo, dt, Helper.GetAppUserInfo());
            return result;
        }

        public static List<DataImportScheduleInfo> GetDataImportSchedule(int? UserID, short? RoleID)
        {
            IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
            List<DataImportScheduleInfo> oDataImportScheduleInfo = oDataImportClient.GetDataImportSchedule(UserID, RoleID, Helper.GetAppUserInfo());
            return oDataImportScheduleInfo;
        }

        public static List<DataImportMessageInfo> GetAllWarningMsg(short DataImportTypeId)
        {
            List<DataImportMessageInfo> oDataImportMessageLst = new List<DataImportMessageInfo>();
            IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
            oDataImportMessageLst = oDataImportClient.GetAllWarningMsg(DataImportTypeId, Helper.GetAppUserInfo());
            oDataImportMessageLst = LanguageHelper.TranslateLabelDataImportMessageLst(oDataImportMessageLst);
            return oDataImportMessageLst;
        }

        public static int SaveDataImportWarningPreferences(DataTable dt, DataImportWarningPreferencesInfo oDataImportWarningPreferencesInfo)
        {
            int result = 0;
            Helper.GetAppUserInfo();
            IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
            result = oDataImportClient.SaveDataImportWarningPreferences(dt, oDataImportWarningPreferencesInfo, Helper.GetAppUserInfo());
            return result;
        }

        public static List<DataImportWarningPreferencesInfo> GetDataImportWarningPreferences(int? CurrentCompanyID, short DataImportType)
        {
            IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
            List<DataImportWarningPreferencesInfo> oDataImportWarningPreferencesInfo = oDataImportClient.GetDataImportWarningPreferences(CurrentCompanyID, DataImportType, Helper.GetAppUserInfo());
            return oDataImportWarningPreferencesInfo;
        }

        public static List<DataImportWarningPreferencesAuditInfo> GetAllWarningAuditList(int CurrentCompanyID,int CurrentUserID, short CurrentRoleID)
        {
            IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
            List<DataImportWarningPreferencesAuditInfo> oDataImportWarningPreferencesAuditInfo = oDataImportClient.GetAllWarningAuditList(CurrentCompanyID,CurrentUserID, CurrentRoleID, Helper.GetAppUserInfo());
            oDataImportWarningPreferencesAuditInfo = LanguageHelper.TranslateLabelAllWarningAuditList(oDataImportWarningPreferencesAuditInfo);
            return oDataImportWarningPreferencesAuditInfo;
        }
    }
}