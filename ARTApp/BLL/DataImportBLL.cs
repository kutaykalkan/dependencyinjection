using SkyStem.ART.App.DAO;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyStem.ART.App.BLL
{
    public class DataImportBLL
    {
        private DataImportBLL()
        {

        }
        /// <summary>
        /// Get Data Import Info
        /// </summary>
        /// <param name="DataImportID"></param>
        /// <param name="oAppUserInfo"></param>
        /// <returns></returns>
        public static DataImportHdrInfo GetDataImportInfo(int? DataImportID, AppUserInfo oAppUserInfo)
        {
            ServiceHelper.SetConnectionString(oAppUserInfo);
            DataImportHdrDAO oDataImportHrdDAO = new DataImportHdrDAO(oAppUserInfo);
            DataImportHdrInfo oDataImportHdrInfo = oDataImportHrdDAO.GetDataImportInfo(DataImportID);
            // Check for null
            if (oDataImportHdrInfo != null)
            {
                // Check permissions
                //if (!CheckDataImportPermissions(oDataImportHdrInfo, oAppUserInfo))
                //    return null;
                // Get message details
                List<DataImportMessageDetailInfo> oDataImportMessageDetailInfoList = oDataImportHrdDAO.GetDataImportMessageDetailInfoList(DataImportID);
                if (oDataImportMessageDetailInfoList != null && oDataImportMessageDetailInfoList.Count > 0)
                {
                    foreach (DataImportMessageDetailInfo oDataImportMessageDetailInfo in oDataImportMessageDetailInfoList)
                    {
                        if (oDataImportMessageDetailInfo.DataImportMessageCategoryID.HasValue)
                        {
                            if (oDataImportMessageDetailInfo.DataImportMessageCategoryID == (short)ARTEnums.DataImportMessageCategory.AccountMessages)
                            {
                                if (oDataImportHdrInfo.DataImportAccountMessageDetailInfoList == null)
                                    oDataImportHdrInfo.DataImportAccountMessageDetailInfoList = new List<DataImportMessageDetailInfo>();
                                oDataImportHdrInfo.DataImportAccountMessageDetailInfoList.Add(oDataImportMessageDetailInfo);
                            }
                            else
                            {
                                if (oDataImportHdrInfo.DataImportMessageDetailInfoList == null)
                                    oDataImportHdrInfo.DataImportMessageDetailInfoList = new List<DataImportMessageDetailInfo>();
                                oDataImportHdrInfo.DataImportMessageDetailInfoList.Add(oDataImportMessageDetailInfo);
                            }
                        }
                    }
                }
            }
            return oDataImportHdrInfo;
        }
        /// <summary>
        /// Check access and return Data Import Hdr Info
        /// </summary>
        /// <param name="oDataImportParamInfo"></param>
        /// <param name="oAppUserInfo"></param>
        /// <returns></returns>
        public static DataImportHdrInfo GetAccessibleDataImportInfo(DataImportParamInfo oDataImportParamInfo, AppUserInfo oAppUserInfo)
        {
            ServiceHelper.SetConnectionString(oAppUserInfo);
            DataImportHdrInfo oDataImportHdrInfo = DataImportBLL.GetDataImportInfo(oDataImportParamInfo.DataImportID, oAppUserInfo);
            if (!CheckDataImportPermissions(oDataImportHdrInfo, oDataImportParamInfo, oAppUserInfo))
                return null;
            return oDataImportHdrInfo;
        }
        /// <summary>
        /// Check permissions
        /// </summary>
        /// <param name="oDataImportHdrInfo"></param>
        /// <param name="oAppUserInfo"></param>
        /// <returns></returns>
        public static bool CheckDataImportPermissions(DataImportHdrInfo oDataImportHdrInfo, DataImportParamInfo oDataImportParamInfo, AppUserInfo oAppUserInfo)
        {
            switch ((ARTEnums.UserRole)oAppUserInfo.RoleID)
            {
                case ARTEnums.UserRole.SKYSTEM_ADMIN:
                    if (oDataImportHdrInfo.RoleID == oAppUserInfo.RoleID
                        && (oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.MultilingualUpload
                        ))
                        return true;
                    return false;
                case ARTEnums.UserRole.SYSTEM_ADMIN:
                    if (//oDataImportHdrInfo.RoleID == oAppUserInfo.RoleID  &&
                        (oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.GLData
                        || oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.CurrencyExchangeRateData
                        || oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.SubledgerData
                        || oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.AccountAttributeList
                        || oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.HolidayCalendar
                        || oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.PeriodEndDates
                        || oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.SubledgerSource
                        || oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.GLTBS
                        || oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.AccountUpload
                        || oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.UserUpload
                        || oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.RecControlChecklist
                        || oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.RecControlChecklistAccount
                        ))
                        return true;
                    break;
                case ARTEnums.UserRole.BUSINESS_ADMIN:
                    if ( oDataImportHdrInfo.RoleID == oAppUserInfo.RoleID && oDataImportHdrInfo.AddedBy == oAppUserInfo.LoginID 
                        && (oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.GLData
                        || oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.SubledgerData
                        || oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.AccountAttributeList
                        || oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.SubledgerSource
                        || oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.GLTBS
                        ))
                        return true;
                    break;
                case ARTEnums.UserRole.USER_ADMIN:
                    if (oDataImportHdrInfo.RoleID == oAppUserInfo.RoleID
                        && (oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.UserUpload
                        ))
                        return true;
                    return false;
            }
            if (oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.SubledgerData
                || oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.RecItems
                || oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.ScheduleRecItems
                || oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.RecControlChecklist
                || oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.RecControlChecklistAccount)
            {
                if (GLDataBLL.CheckGLPermissions(oDataImportParamInfo.GLDataID, oAppUserInfo.UserID, oAppUserInfo.RoleID, oAppUserInfo))
                    return true;
                return false;
            }
            else if (oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.GeneralTaskImport)
            {
                if (TaskBLL.CheckTaskPermissions(oDataImportParamInfo.TaskID, oAppUserInfo) || oDataImportHdrInfo.AddedBy == oAppUserInfo.LoginID)
                    return true;
                return false;
            }
            return false;
        }
    }
}