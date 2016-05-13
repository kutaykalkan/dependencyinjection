using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.DAO;
using SkyStem.ART.Client.IServices;
using System.Data;
using System.Data.SqlClient;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.App.DAO.Report;
using SkyStem.ART.Client.Model.Report;

namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "ReportArchive" here, you must also update the reference to "ReportArchive" in Web.config.
    public class ReportArchive : IReportArchive
    {
        public void DoWork()
        {
        }

        #region IReportArchive Members


        public void SaveArchivedReport(ReportArchiveInfo oRptArchiveInfo, List<ReportArchiveParameterInfo> oRptArchiveParamInfoCollection, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            long newRptArchiveID = -1;
            short? rowsAffected = -1;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ReportArchiveDAO oRptArchiveDAO = new ReportArchiveDAO(oAppUserInfo);
                oConnection = oRptArchiveDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();
                oRptArchiveDAO.Save(oRptArchiveInfo, oConnection, oTransaction);
                if (oRptArchiveInfo.ReportArchiveID.HasValue)
                {
                    newRptArchiveID = oRptArchiveInfo.ReportArchiveID.Value;
                    ReportArchiveParameterDAO oRptParamDAO = new ReportArchiveParameterDAO(oAppUserInfo);
                    DataTable dtRptArchiveParams = ReportServiceHelper.ConvertReportArchiveParameterListToDataTable(oRptArchiveParamInfoCollection, newRptArchiveID);
                    rowsAffected = oRptParamDAO.InsertRptArchiveParams(dtRptArchiveParams, oConnection, oTransaction);
                    if (rowsAffected.HasValue && rowsAffected > 0)
                        oTransaction.Commit();
                    else
                        throw new ARTException(5000042);//Error while saving data to database
                }

            }
            catch (ARTException ex)
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed && oTransaction != null)
                    oTransaction.Rollback();
                throw ex;
            }
            catch (SqlException ex)
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed && oTransaction != null)
                    oTransaction.Rollback();
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed && oTransaction != null)
                    oTransaction.Rollback();
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            finally
            {
                if ((null != oConnection) && (oConnection.State == ConnectionState.Open))
                    oConnection.Dispose();
            }

        }

        public List<ReportArchiveInfo> GetRptActivityByReportIDUserIDRoleIDRecPeriodID(short reportID
            , int userID, short roleID, int recPeriodID, int languageID, int defaultLanguageID, int companyID, AppUserInfo oAppUserInfo)
        {
            ReportArchiveDAO oRptArchiveDAO = null;
            List<ReportArchiveInfo> oRptArchiveInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                oRptArchiveDAO = new ReportArchiveDAO(oAppUserInfo);
                oRptArchiveInfoCollection = oRptArchiveDAO.GetRptActivityByReportIDUserIDRoleIDRecPeriodID(reportID,
                    userID, roleID, recPeriodID, languageID, defaultLanguageID, companyID);

            }
            catch (SqlException ex)
            {
               
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oRptArchiveInfoCollection;
        }

        public ReportArchiveInfo GetArchivedReportByReportArchiveID(int ReportArchiveID, AppUserInfo oAppUserInfo)
        {
            ReportArchiveDAO oRptArchiveDAO = null;
            ReportArchiveInfo oRptArchiveInfo = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                oRptArchiveDAO = new ReportArchiveDAO(oAppUserInfo);
                oRptArchiveInfo = oRptArchiveDAO.GetArchiveReportByReportArchiveID(ReportArchiveID);
            }
            catch (SqlException ex)
            {

                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {

                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oRptArchiveInfo;
        }

        public List<ReportArchiveInfo> GetReportsActivity(int recPeriodID, int languageID, int defaultLanguageID, int companyID, short RoleID, AppUserInfo oAppUserInfo)
        {
            ReportArchiveDAO oRptArchiveDAO = null;
            List<ReportArchiveInfo> oRptArchiveInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                oRptArchiveDAO = new ReportArchiveDAO(oAppUserInfo);
                oRptArchiveInfoCollection = oRptArchiveDAO.GetReportsActivity(recPeriodID, languageID, defaultLanguageID, companyID,  RoleID);

            }
            catch (SqlException ex)
            {

                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {

                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oRptArchiveInfoCollection;
        }
        #endregion
    }
}
