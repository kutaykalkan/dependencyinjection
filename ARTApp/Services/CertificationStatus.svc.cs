using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Data;
using SkyStem.ART.App.DAO;
using SkyStem.ART.App.Utility;
using System.Data.SqlClient;
using System.Data;

namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "CertificationStatus" here, you must also update the reference to "CertificationStatus" in Web.config.
    public class CertificationStatus : ICertificationStatus
    {
        public List<DynamicPlaceholderMstInfo> getAllDynamicPlaceholderMstInfo( AppUserInfo oAppUserInfo)
        {
            List<DynamicPlaceholderMstInfo> objDynamicPlaceholderMstInfoCollection = new List<DynamicPlaceholderMstInfo>();
            //DynamicPlaceholderMstInfo oDynamicPlaceholderMstInfo = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DynamicPlaceholderMstDAO oDynamicPlaceholderMstDAO = new DynamicPlaceholderMstDAO(oAppUserInfo);
                objDynamicPlaceholderMstInfoCollection = oDynamicPlaceholderMstDAO.GetAllDynamicPlaceholderMstInfo();

            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return objDynamicPlaceholderMstInfoCollection;
        }

        public void InsertCertificationVerbiageInfo(List<CertificationVerbiageInfo> oCertificationVerbiageInfoCollection, int RecPeriodID, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
           
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
               CertificationVerbiageDAO oCertificationVerbiageDAO=new CertificationVerbiageDAO(oAppUserInfo);
                //Save User
                oConnection = oCertificationVerbiageDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();
                foreach(CertificationVerbiageInfo objCertificationVerbiageInfo in oCertificationVerbiageInfoCollection)
                {
                    oCertificationVerbiageDAO.saveCertificationVerbiage(objCertificationVerbiageInfo, RecPeriodID, oTransaction, oConnection);
                }
               
                //TODO: Send email to user
                oTransaction.Commit();
                
            }
            catch (Exception ex)
            {
                if ((oTransaction != null) && (oConnection.State == ConnectionState.Open))
                {
                    oTransaction.Rollback();
                }
                throw ex;
            }
            finally
            {
                try
                {
                    if ((null != oConnection) && (oConnection.State == ConnectionState.Open))
                        oConnection.Close();
                }
                catch (Exception)
                {
                }

            }

        }

        public List<CertificationVerbiageInfo> GetCertificationVerbiageByCompanyIDRecPeriodID(int companyID, int recPeriodID, AppUserInfo oAppUserInfo)
        {
            List<CertificationVerbiageInfo> oCertVerbiageCollection = null;
            CertificationVerbiageDAO oCertVerbiageDAO = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                //oCertVerbiageCollection = new List<CertificationVerbiageInfo>();
                oCertVerbiageDAO = new CertificationVerbiageDAO(oAppUserInfo);
                oCertVerbiageCollection = oCertVerbiageDAO.GetCertificationVerbiageByCompanyIDRecPeriodID(companyID, recPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oCertVerbiageCollection;
        }

        public List<CertificationStatusMstInfo> GetAllCertificationStatus(AppUserInfo oAppUserInfo)
        {
            List<CertificationStatusMstInfo> oCertificationStatusCollection = null;
            CertificationStatusMstDAO CertStatusDAO = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CertStatusDAO = new CertificationStatusMstDAO(oAppUserInfo);
                oCertificationStatusCollection = CertStatusDAO.GetAllReason();
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oCertificationStatusCollection;
        }
    }
}
