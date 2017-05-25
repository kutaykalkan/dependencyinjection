using SkyStem.ART.App.DAO;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyStem.ART.App.BLL
{
    public class DataImportBLL
    {
        public DataImportHdrInfo GetDataImportInfo(int? DataImportID, AppUserInfo oAppUserInfo)
        {
            ServiceHelper.SetConnectionString(oAppUserInfo);
            DataImportHdrDAO oDataImportHrdDAO = new DataImportHdrDAO(oAppUserInfo);
            DataImportHdrInfo oDataImportHdrInfo = oDataImportHrdDAO.GetDataImportInfo(DataImportID);
            // Check for null
            if (oDataImportHdrInfo != null)
            {
                // Check permissions
                if (!CheckDataImportPermissions(oDataImportHdrInfo, oAppUserInfo))
                    return null;
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

        private bool CheckDataImportPermissions(DataImportHdrInfo oDataImportHdrInfo, AppUserInfo oAppUserInfo)
        {
            // Check for permissions
            if (oAppUserInfo.RoleID == (short)ARTEnums.UserRole.SKYSTEM_ADMIN)
            {
                if (oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.MultilingualUpload)
                    return true;
                return false;
            }
            else if (oAppUserInfo.RoleID == (short)ARTEnums.UserRole.SYSTEM_ADMIN
                || oAppUserInfo.RoleID == (short)ARTEnums.UserRole.USER_ADMIN)
            {
                if (oAppUserInfo.RoleID == oDataImportHdrInfo.RoleID
                    && (oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.GLData
                        || oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.CurrencyExchangeRateData
                        || oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.SubledgerData
                        || oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.AccountAttributeList
                        || oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.HolidayCalendar
                        || oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.PeriodEndDates
                        || oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.SubledgerSource
                        || oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.AccountUpload
                        || oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.UserUpload
                        || oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.RecControlChecklist

                    ))
                    return true;
                return false;
            }
            else
            {
                if (oAppUserInfo.RoleID == oDataImportHdrInfo.RoleID
                    && oAppUserInfo.LoginID == oDataImportHdrInfo.AddedBy)
                    return true;
                return false;
            }
        }
    }
}