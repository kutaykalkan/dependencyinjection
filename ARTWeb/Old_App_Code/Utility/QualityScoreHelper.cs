using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SkyStem.ART.Client.Model.QualityScore;
using SkyStem.ART.Client.Params.QualityScore;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.Web.Utility
{
    /// <summary>
    /// Summary description for QualityScoreHelper
    /// </summary>
    public class QualityScoreHelper
    {
        private QualityScoreHelper()
        {
        }

        /// <summary>
        /// Gets the company quality score info list.
        /// </summary>
        /// <param name="RecPeriodID">The rec period ID.</param>
        /// <param name="enabledOnly">The enabled only.</param>
        /// <returns></returns>
        public static List<CompanyQualityScoreInfo> GetCompanyQualityScoreInfoList(bool? enabledOnly)
        {
            if (!Helper.IsFeatureActivated(WebEnums.Feature.QualityScore, SessionHelper.CurrentReconciliationPeriodID))
                return null;
            QualityScoreParamInfo oQualityScoreParamInfo = new QualityScoreParamInfo();
            Helper.FillCommonServiceParams(oQualityScoreParamInfo);
            oQualityScoreParamInfo.EnabledOnly = enabledOnly;
            IQualityScore oQualityScore = RemotingHelper.GetQualityScoreObject();
            List<CompanyQualityScoreInfo> oCompanyQualityScoreInfoList = oQualityScore.GetCompanyQualityScoreInfoList(oQualityScoreParamInfo, Helper.GetAppUserInfo());
            LanguageHelper.TranslateLabelsCompanyQualityScoreData(oCompanyQualityScoreInfoList);
            return oCompanyQualityScoreInfoList;
        }

        /// <summary>
        /// Saves the company quality score info list.
        /// </summary>
        /// <param name="oCompanyQualityScoreInfoList">The o company quality score info list.</param>
        /// <param name="loginID">The login ID.</param>
        /// <returns></returns>
        public static bool SaveCompanyQualityScoreInfoList(List<CompanyQualityScoreInfo> oCompanyQualityScoreInfoList, string loginID)
        {
            if (!Helper.IsFeatureActivated(WebEnums.Feature.QualityScore, SessionHelper.CurrentReconciliationPeriodID))
                return false;
            QualityScoreParamInfo oQualityScoreParamInfo = new QualityScoreParamInfo();
            Helper.FillCommonServiceParams(oQualityScoreParamInfo);
            oQualityScoreParamInfo.UserLoginID = loginID;
            oQualityScoreParamInfo.DateRevised = DateTime.Now;
            oQualityScoreParamInfo.CompanyQualityScoreInfoList = oCompanyQualityScoreInfoList;
            IQualityScore oQualityScore = RemotingHelper.GetQualityScoreObject();
            return oQualityScore.SaveCompanyQualityScoreInfoList(oQualityScoreParamInfo, Helper.GetAppUserInfo());
        }

        /// <summary>
        /// Gets the GL data quality score info list.
        /// </summary>
        /// <param name="glDataID">The gl data ID.</param>
        /// <returns></returns>
        public static List<GLDataQualityScoreInfo> GetGLDataQualityScoreInfoList(long? glDataID)
        {
            if (!Helper.IsFeatureActivated(WebEnums.Feature.QualityScore, SessionHelper.CurrentReconciliationPeriodID))
                return null;
            QualityScoreParamInfo oQualityScoreParamInfo = new QualityScoreParamInfo();
            Helper.FillCommonServiceParams(oQualityScoreParamInfo);
            oQualityScoreParamInfo.GLDataID = glDataID;
            IQualityScore oQualityScore = RemotingHelper.GetQualityScoreObject();
            List<GLDataQualityScoreInfo> oGLDataQualityScoreInfoList = oQualityScore.GetGLDataQualityScoreInfoList(oQualityScoreParamInfo, Helper.GetAppUserInfo());
            LanguageHelper.TranslateLabelsQualityScoreGLData(oGLDataQualityScoreInfoList);
            return oGLDataQualityScoreInfoList;
        }

        /// <summary>
        /// Saves the company quality score info list.
        /// </summary>
        /// <param name="gLDataID">The g L data ID.</param>
        /// <param name="oGLDataQualityScoreInfoList">The o GL data quality score info list.</param>
        /// <param name="loginID">The login ID.</param>
        /// <returns></returns>
        public static bool SaveGLDataQualityScoreInfoList(long? gLDataID, List<GLDataQualityScoreInfo> oGLDataQualityScoreInfoList, string loginID)
        {
            if (!Helper.IsFeatureActivated(WebEnums.Feature.QualityScore, SessionHelper.CurrentReconciliationPeriodID))
                return false;
            QualityScoreParamInfo oQualityScoreParamInfo = new QualityScoreParamInfo();
            Helper.FillCommonServiceParams(oQualityScoreParamInfo);
            oQualityScoreParamInfo.UserLoginID = loginID;
            oQualityScoreParamInfo.DateRevised = DateTime.Now;
            oQualityScoreParamInfo.GLDataID = gLDataID;
            oQualityScoreParamInfo.GLDataQualityScoreInfoList = oGLDataQualityScoreInfoList;
            IQualityScore oQualityScore = RemotingHelper.GetQualityScoreObject();
            return oQualityScore.SaveCompanyQualityScoreInfoList(oQualityScoreParamInfo, Helper.GetAppUserInfo());
        }

        /// <summary>
        /// Gets the GL data quality score counts.
        /// </summary>
        /// <param name="glDataID">The gl data ID.</param>
        /// <returns></returns>
        public static Dictionary<ARTEnums.QualityScoreType, Int32?> GetGLDataQualityScoreCount(long? glDataID)
        {
            if (!Helper.IsFeatureActivated(WebEnums.Feature.QualityScore, SessionHelper.CurrentReconciliationPeriodID))
                return null;
            QualityScoreParamInfo oQualityScoreParamInfo = new QualityScoreParamInfo();
            Helper.FillCommonServiceParams(oQualityScoreParamInfo);
            oQualityScoreParamInfo.GLDataID = glDataID;
            IQualityScore oQualityScore = RemotingHelper.GetQualityScoreObject();
            Dictionary<ARTEnums.QualityScoreType, Int32?> dictGLDataQualityScoreCount = oQualityScore.GetGLDataQualityScoreCount(oQualityScoreParamInfo, Helper.GetAppUserInfo());
            return dictGLDataQualityScoreCount;
        }

        /// <summary>
        /// Gets the aging days.
        /// </summary>
        /// <returns></returns>
        public static Int16? GetAgingDays()
        {
            List<AgingCategoryMstInfo> oAgingCategoryMstInfoList = SessionHelper.GetAllAgingCategories();
            if (oAgingCategoryMstInfoList != null && oAgingCategoryMstInfoList.Count > 0)
                return oAgingCategoryMstInfoList[0].ToDays;
            return null;
        }
    }
}
