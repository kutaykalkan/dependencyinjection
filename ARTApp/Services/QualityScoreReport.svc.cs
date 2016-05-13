using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.DAO.QualityScore;
using System.Data.SqlClient;
using SkyStem.ART.App.Utility;

namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "RangeOfScore" here, you must also update the reference to "RangeOfScore" in Web.config.
    public class QualityScoreReports : IQualityScoreReports
    {
       #region IRangeOfScore Members

        public IList<RangeOfScoreMstInfo> GetRangeOfScore( AppUserInfo oAppUserInfo)
        {
            List<RangeOfScoreMstInfo> oRangeOfScoreMstInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                QualityScoreRangeDAO oQualityScoreStatusMstDAO = new QualityScoreRangeDAO(oAppUserInfo);
                oRangeOfScoreMstInfoList = oQualityScoreStatusMstDAO.GetAllQualityScoreRanges();
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oRangeOfScoreMstInfoList;
        }

        #endregion

        #region IQualityScoreReports Members


        public IList<QualityScoreChecklistInfo> GetQualityScoreChecklist(int RecPeriodID, AppUserInfo oAppUserInfo)
        {
            List<QualityScoreChecklistInfo> oQualityscoreChecklistInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                QualityScoreRangeDAO oQualityScoreStatusMstDAO = new QualityScoreRangeDAO(oAppUserInfo);
                oQualityscoreChecklistInfoList = oQualityScoreStatusMstDAO.GetQualityScoreChecklist(RecPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oQualityscoreChecklistInfoList;
        }

        public List<QualityScoreChecklistInfo> GetQualityScoreChecklistByQualityScoreIDs(List<int> qualityScoreIDs, AppUserInfo oAppUserInfo)
        {
            List<QualityScoreChecklistInfo> oQualityscoreChecklistInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                QualityScoreRangeDAO oQualityScoreStatusMstDAO = new QualityScoreRangeDAO(oAppUserInfo);
                oQualityscoreChecklistInfoList = oQualityScoreStatusMstDAO.GetQualityScoreByQualityScoreIDs(qualityScoreIDs);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oQualityscoreChecklistInfoList;
        }
        #endregion
    }
}
