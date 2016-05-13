using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.DAO;
using System.Data.SqlClient;
using SkyStem.ART.App.Utility;
using System.Data;

namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "UnExpectedVariance" here, you must also update the reference to "UnExpectedVariance" in Web.config.
    public class UnexplainedVariance : IUnexplainedVariance
    {

        #region IUnExpectedVariance Members

        //public List<GLDataUnexplainedVarianceInfo> GetGLDataUnexplainedVarianceInfoCollectionByGLDataID(int? gLDataID, int? templateAttributeID)
        public List<GLDataUnexplainedVarianceInfo> GetGLDataUnexplainedVarianceInfoCollectionByGLDataID(long? gLDataID, AppUserInfo oAppUserInfo)
        {
            List<GLDataUnexplainedVarianceInfo> oGLDataUnexplainedVarianceInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataUnexplainedVarianceDAO oGLDataUnexplainedVarianceDAO = new GLDataUnexplainedVarianceDAO(oAppUserInfo);
                oGLDataUnexplainedVarianceInfoCollection = (List<GLDataUnexplainedVarianceInfo>)oGLDataUnexplainedVarianceDAO.SelectAllByGLDataID(gLDataID);

            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oGLDataUnexplainedVarianceInfoCollection;
        }

        public void InsertGLDataUnexplainedVariance(GLDataUnexplainedVarianceInfo GLDataUnexplainedVarianceInfo, int recPeriodID, AppUserInfo oAppUserInfo)
        {

            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                //GLDataWriteOnOffDAO oGLDataWriteOnOffDAO = new GLDataWriteOnOffDAO(oAppUserInfo);
                ReconciliationPeriodDAO oReconciliationPeriodDAO = new ReconciliationPeriodDAO(oAppUserInfo);
                oConnection = oReconciliationPeriodDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();

                GLDataUnexplainedVarianceDAO oGLDataUnexplainedVarianceDAO = new GLDataUnexplainedVarianceDAO(oAppUserInfo);
                oGLDataUnexplainedVarianceDAO.Save(GLDataUnexplainedVarianceInfo);
                oReconciliationPeriodDAO.UpdateRecPeriodStatusAsInProgress(recPeriodID, oConnection, oTransaction);

                oTransaction.Commit();
            }
            catch (SqlException ex)
            {
                if (oTransaction != null && oTransaction.Connection != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                if (oTransaction != null && oTransaction.Connection != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            finally
            {
                if ((oConnection != null) && (oConnection.State == ConnectionState.Open))
                {
                    oConnection.Close();
                    oConnection.Dispose();
                }
            }


        }

        public void UpdateGLDataUnexplainedVariance(GLDataUnexplainedVarianceInfo GLDataUnexplainedVarianceInfo, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataUnexplainedVarianceDAO oGLDataUnexplainedVarianceDAO = new GLDataUnexplainedVarianceDAO(oAppUserInfo);
                oGLDataUnexplainedVarianceDAO.Update(GLDataUnexplainedVarianceInfo);
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

        public void DeleteGLDataUnexplainedVariance(long? GLDataUnexplainedVarianceID, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataUnexplainedVarianceDAO oGLDataUnexplainedVarianceDAO = new GLDataUnexplainedVarianceDAO(oAppUserInfo);
                oGLDataUnexplainedVarianceDAO.Delete(GLDataUnexplainedVarianceID);
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

        public List<GLDataUnexplainedVarianceInfo> GetUnExplainedVarianceHistoryInfoCollection(long? glDataID, AppUserInfo oAppUserInfo)
        {
            List<GLDataUnexplainedVarianceInfo> oGLDataUnexplainedVarianceInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataUnexplainedVarianceDAO oGLDataUnexplainedVarianceDAO = new GLDataUnexplainedVarianceDAO(oAppUserInfo);
                oGLDataUnexplainedVarianceInfoCollection = oGLDataUnexplainedVarianceDAO.GetUnexplainedVarianceHistory(glDataID);

            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oGLDataUnexplainedVarianceInfoCollection;
            
        }
        #endregion
    }
}
