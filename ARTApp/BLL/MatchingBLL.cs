using SkyStem.ART.App.DAO.Matching;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Model.Matching;
using SkyStem.ART.Client.Params.Matching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyStem.ART.App.BLL
{
    public class MatchingBLL
    {
        private MatchingBLL()
        {

        }
        /// <summary>
        /// Get Matching Source Data Import Info
        /// </summary>
        /// <param name="oMatchingParamInfo"></param>
        /// <param name="oAppUserInfo"></param>
        /// <returns></returns>
        public static MatchingSourceDataImportHdrInfo GetMatchingSourceDataImportInfo(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
            ServiceHelper.SetConnectionString(oAppUserInfo);
            MatchingSourceDataImportHdrDAO oMatchingSourceDataImportHdrDAO = new MatchingSourceDataImportHdrDAO(oAppUserInfo);
            MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo = oMatchingSourceDataImportHdrDAO.GetMatchingSourceDataImportInfo(oMatchingParamInfo);
            if (!CheckDataImportPermissions(oMatchingSourceDataImportHdrInfo, oMatchingParamInfo, oAppUserInfo))
                return null;
            return oMatchingSourceDataImportHdrInfo;
        }
        /// <summary>
        /// Check permissions
        /// </summary>
        /// <param name="oMatchingSourceDataImportHdrInfo"></param>
        /// <param name="oMatchingParamInfo"></param>
        /// <param name="oAppUserInfo"></param>
        /// <returns></returns>
        public static bool CheckDataImportPermissions(MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo, MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
            switch ((ARTEnums.UserRole)oAppUserInfo.RoleID)
            {
                case ARTEnums.UserRole.SYSTEM_ADMIN:
                    if (//oMatchingSourceDataImportHdrInfo.RoleID == oAppUserInfo.RoleID &&
                        (oMatchingSourceDataImportHdrInfo.MatchingSourceTypeID == (short)ARTEnums.MatchingSourceType.GLTBS
                        || oMatchingSourceDataImportHdrInfo.MatchingSourceTypeID == (short)ARTEnums.DataImportType.NBF
                        ))
                        return true;
                    break;
            }
            if (oMatchingSourceDataImportHdrInfo.RoleID == oAppUserInfo.RoleID && oMatchingSourceDataImportHdrInfo.UserID == oAppUserInfo.UserID &&
                        (oMatchingSourceDataImportHdrInfo.MatchingSourceTypeID == (short)ARTEnums.MatchingSourceType.GLTBS
                        || oMatchingSourceDataImportHdrInfo.MatchingSourceTypeID == (short)ARTEnums.DataImportType.NBF
                        ))
                return true;
            else if (GLDataBLL.CheckGLPermissions(oMatchingParamInfo.GLDataID, oAppUserInfo.UserID, oAppUserInfo.RoleID, oAppUserInfo))
                return true;
            return false;
        }
    }
}