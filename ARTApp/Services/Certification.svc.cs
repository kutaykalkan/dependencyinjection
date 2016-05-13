using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using SkyStem.ART.App.DAO;
using SkyStem.ART.App.Utility;
using System.Data.SqlClient;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.IServices;

namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "Certification" here, you must also update the reference to "Certification" in Web.config.
    public class Certification : ICertification
    {
        public void SaveCertificationSignoffDetail(CertificationSignOffInfo oCertificationSignOffInfo, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CertificationSignOffDAO oCertificationSignOffDAO = new CertificationSignOffDAO(oAppUserInfo);
                oCertificationSignOffDAO.Save(oCertificationSignOffInfo);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
        }
        public List<MandatoryReportSignOffInfo> GetMandatoryReportSignOff(int? reportRoleMandatoryReportID, int? userID, int? reconciliationPeriodID, AppUserInfo oAppUserInfo)
        {
            List<MandatoryReportSignOffInfo > oMandatoryReportSignOffInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                MandatoryReportSignOffDAO oMandatoryReportSignOffDAO = new MandatoryReportSignOffDAO(oAppUserInfo);
                oMandatoryReportSignOffInfoCollection = oMandatoryReportSignOffDAO.SelectAllByReportRoleMandatoryReportIDUserIDRecPeriodID(reportRoleMandatoryReportID ,userID, reconciliationPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oMandatoryReportSignOffInfoCollection;
        }

        public void SaveMandatoryReportSignoff(MandatoryReportSignOffInfo oMandatoryReportSignOffInfo, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                MandatoryReportSignOffDAO oMandatoryReportSignOffDAO = new MandatoryReportSignOffDAO(oAppUserInfo);
                oMandatoryReportSignOffDAO.Save(oMandatoryReportSignOffInfo);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
        }




        public List<CertificationVerbiageInfo> GetCertificationVerbiage(int? reconciliationPeriodID, int? companyID, short? certificationTypeID, AppUserInfo oAppUserInfo)
        {
            List<CertificationVerbiageInfo> oCertificationVerbiageInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CertificationVerbiageDAO oCertificationVerbiageDAO = new CertificationVerbiageDAO(oAppUserInfo);
                oCertificationVerbiageInfoCollection = oCertificationVerbiageDAO.GetCertificationVerbiage ( reconciliationPeriodID, companyID,  certificationTypeID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oCertificationVerbiageInfoCollection;
        }


        public List<CertificationSignOffInfo> GetCertificationSignOff(int? reconciliationPeriodID, int? userID, short? roleID, AppUserInfo oAppUserInfo)
        {
            List<CertificationSignOffInfo> oCertificationSignOffInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CertificationSignOffDAO oCertificationSignOffDAO = new CertificationSignOffDAO(oAppUserInfo);
                oCertificationSignOffInfoCollection = oCertificationSignOffDAO.GetCertificationSignOff(reconciliationPeriodID,userID,roleID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oCertificationSignOffInfoCollection;
        }


        public List<CertificationSignOffInfo> GetCertificationSignOffForJuniors(int? reconciliationPeriodID, int? userID, short? roleID, int? UserIDForAccess, short? RoleIDForAccess, AppUserInfo oAppUserInfo)
        {
            List<CertificationSignOffInfo> oCertificationSignOffInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CertificationSignOffDAO oCertificationSignOffDAO = new CertificationSignOffDAO(oAppUserInfo);
                oCertificationSignOffInfoCollection = oCertificationSignOffDAO.GetCertificationSignOffForJuniors(reconciliationPeriodID, userID, roleID,  UserIDForAccess,  RoleIDForAccess);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oCertificationSignOffInfoCollection;
        }


        public List<CertificationSignOffInfo> GetCertificationSignOffForJuniorsOfControllerAndCEOCFO(int? reconciliationPeriodID, int? userID, short? roleID, int? CompanyID, AppUserInfo oAppUserInfo)
        {
            List<CertificationSignOffInfo> oCertificationSignOffInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CertificationSignOffDAO oCertificationSignOffDAO = new CertificationSignOffDAO(oAppUserInfo);
                oCertificationSignOffInfoCollection = oCertificationSignOffDAO.GetCertificationSignOffForJuniorsOfControllerAndCEOCFO(reconciliationPeriodID, userID, roleID, CompanyID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oCertificationSignOffInfoCollection;
        }


        public bool GetIsCertificationStarted(int? reconciliationPeriodID, AppUserInfo oAppUserInfo)
        {
            bool IsCertificationStarted = false;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CertificationSignOffDAO oCertificationSignOffDAO = new CertificationSignOffDAO(oAppUserInfo);
                IsCertificationStarted = oCertificationSignOffDAO.GetIsCertificationStarted(reconciliationPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return IsCertificationStarted;
        }
        
    }//end of class
}//end of namespace
