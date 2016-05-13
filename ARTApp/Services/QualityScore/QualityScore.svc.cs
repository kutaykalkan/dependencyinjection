using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Model.QualityScore;
using System.Collections.Generic;
using SkyStem.ART.Client.Params.QualityScore;
using SkyStem.ART.App.DAO.QualityScore;
using System.Data.SqlClient;
using SkyStem.ART.App.Utility;
using System;
using SkyStem.ART.Client.Data;

namespace SkyStem.ART.App.Services
{
    public class QualityScore : IQualityScore
    {
        #region IQualityScore Members


        public List<QualityScoreStatusMstInfo> GetAllQualityScoreStatuses( AppUserInfo oAppUserInfo)
        {
            List<QualityScoreStatusMstInfo> oQualityScoreStatusMstInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                QualityScoreStatusMstDAO oQualityScoreStatusMstDAO = new QualityScoreStatusMstDAO(oAppUserInfo);
                oQualityScoreStatusMstInfoList = oQualityScoreStatusMstDAO.GetAllQualityScoreStatuses();
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oQualityScoreStatusMstInfoList;
        }

        /// <summary>
        /// Gets the company quality score info list.
        /// </summary>
        /// <param name="oQualityScoreParamInfo">The o quality score param info.</param>
        /// <returns></returns>
        public List<CompanyQualityScoreInfo> GetCompanyQualityScoreInfoList(QualityScoreParamInfo oQualityScoreParamInfo, AppUserInfo oAppUserInfo)
        {
            List<CompanyQualityScoreInfo> oCompanyQualityScoreInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CompanyQualityScoreDAO oCompanyQualityScoreDAO = new CompanyQualityScoreDAO(oAppUserInfo);
                oCompanyQualityScoreInfoList = oCompanyQualityScoreDAO.GetCompanyQualityScoreInfoList(oQualityScoreParamInfo.RecPeriodID, oQualityScoreParamInfo.EnabledOnly);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oCompanyQualityScoreInfoList;
        }

        /// <summary>
        /// Saves the company quality score info list.
        /// </summary>
        /// <param name="oQSParamInfo">The o QS param info.</param>
        /// <returns></returns>
        public bool SaveCompanyQualityScoreInfoList(QualityScoreParamInfo oQSParamInfo, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CompanyQualityScoreDAO oCompanyQualityScoreDAO = new CompanyQualityScoreDAO(oAppUserInfo);
                oCompanyQualityScoreDAO.SaveCompanyQualityScoreInfoList(oQSParamInfo.CompanyQualityScoreInfoList, oQSParamInfo.RecPeriodID, oQSParamInfo.CompanyID, oQSParamInfo.UserLoginID, oQSParamInfo.DateRevised, oQSParamInfo.UserID);
                return true;
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return false;
        }

        /// <summary>
        /// Gets the GL data quality score info list.
        /// </summary>
        /// <param name="oQualityScoreParamInfo">The o quality score param info.</param>
        /// <returns></returns>
        public List<GLDataQualityScoreInfo> GetGLDataQualityScoreInfoList(QualityScoreParamInfo oQualityScoreParamInfo, AppUserInfo oAppUserInfo)
        {
            List<GLDataQualityScoreInfo> oGLDataQualityScoreInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataQualityScoreDAO oGLDataQualityScoreDAO = new GLDataQualityScoreDAO(oAppUserInfo);
                oGLDataQualityScoreInfoList = oGLDataQualityScoreDAO.GetGLDataQualityScoreInfoList(oQualityScoreParamInfo.RecPeriodID, oQualityScoreParamInfo.GLDataID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oGLDataQualityScoreInfoList;
        }

        /// <summary>
        /// Saves the GL data quality score info list.
        /// </summary>
        /// <param name="oQSParamInfo">The o QS param info.</param>
        /// <returns></returns>
        public bool SaveGLDataQualityScoreInfoList(QualityScoreParamInfo oQSParamInfo, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataQualityScoreDAO oGLDataQualityScoreDAO = new GLDataQualityScoreDAO(oAppUserInfo);
                oGLDataQualityScoreDAO.SaveGLDataQualityScoreInfoList(oQSParamInfo.RecPeriodID, oQSParamInfo.GLDataID, oQSParamInfo.GLDataQualityScoreInfoList, oQSParamInfo.UserLoginID, oQSParamInfo.DateRevised);
                return true;
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return false;
        }

        /// <summary>
        /// Gets the GL data quality score count.
        /// </summary>
        /// <param name="oQSParamInfo">The o QS param info.</param>
        /// <returns></returns>
        public Dictionary<ARTEnums.QualityScoreType, Int32?> GetGLDataQualityScoreCount(QualityScoreParamInfo oQSParamInfo, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataQualityScoreDAO oGLDataQualityScoreDAO = new GLDataQualityScoreDAO(oAppUserInfo);
                Dictionary<ARTEnums.QualityScoreType, Int32?> dictGLDataQualityScoreCount = oGLDataQualityScoreDAO.GetGLDataQualityScoreCount(oQSParamInfo.GLDataID, oQSParamInfo.RecPeriodID);
                return dictGLDataQualityScoreCount;
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return null;
        }

        #endregion
    }
}
