using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.DAO;
using System.Data;
using System.Data.SqlClient;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Data;

namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "JournalEntry" here, you must also update the reference to "JournalEntry" in Web.config.
    public class JournalEntry : IJournalEntry
    {

        public List<CompanyGLToolColumnInfo> GetGLToolColumnsByRecPeriodID(int? RecPeriodID, int? CompanyID, AppUserInfo oAppUserInfo)
        {
            List<CompanyGLToolColumnInfo> oCompanyGLToolColumnInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CompanyGLToolColumnDAO oCompanyGLToolColumnDAO = new CompanyGLToolColumnDAO(oAppUserInfo);
                oCompanyGLToolColumnInfoCollection = (List<CompanyGLToolColumnInfo>)oCompanyGLToolColumnDAO.GetGLToolColumnsByRecPeriodID(RecPeriodID, CompanyID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oCompanyGLToolColumnInfoCollection;
        }

        public List<UserHdrInfo> SelectWriteOffOnApproversByCompanyID(int? companyID, AppUserInfo oAppUserInfo)
        {
            List<UserHdrInfo> oUserHdrInfoCollection = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UserHdrDAO oUserHdrDAO = new UserHdrDAO(oAppUserInfo);
                oUserHdrInfoCollection = oUserHdrDAO.SelectWriteOffOnApproversByCompanyID(companyID);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oUserHdrInfoCollection;
        }

        public void SaveCompanyGLToolColumns(List<CompanyGLToolColumnInfo> oCompanyGLToolColumnInfoCollection, int? CompanyID, int? StartRecPeriodID, string AddedBy, DateTime? DateAdded, string RevisedBy, DateTime? DateRevised, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataTable dtCompanyGLToolColumns = ServiceHelper.ConvertCompanyGLToolColumnInfoToDataTable(oCompanyGLToolColumnInfoCollection);
                CompanyGLToolColumnDAO oCompanyGLToolColumnDAO = new CompanyGLToolColumnDAO(oAppUserInfo);
                oConnection = oCompanyGLToolColumnDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();
                oCompanyGLToolColumnDAO.SaveCompanyGLToolColumns(dtCompanyGLToolColumns, CompanyID, StartRecPeriodID, AddedBy, DateAdded, RevisedBy, DateRevised, oConnection, oTransaction);
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

        public List<CompanyJEWriteOffOnApproverInfo> GetCompanyJEWriteOffOnApproversByRecPeriodID(int? RecPeriodID, int? CompanyID, AppUserInfo oAppUserInfo)
        {
            List<CompanyJEWriteOffOnApproverInfo> oCompanyJEWriteOffOnApproverInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CompanyJEWriteOffOnApproverDAO oCompanyJEWriteOffOnApproverDAO = new CompanyJEWriteOffOnApproverDAO(oAppUserInfo);
                oCompanyJEWriteOffOnApproverInfoCollection = (List<CompanyJEWriteOffOnApproverInfo>)oCompanyJEWriteOffOnApproverDAO.GetCompanyJEWriteOffOnApproversByRecPeriodID(RecPeriodID, CompanyID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oCompanyJEWriteOffOnApproverInfoCollection;
        }

        public void SaveCompanyJEWriteOffOnApprovers(List<CompanyJEWriteOffOnApproverInfo> oCompanyJEWriteOffOnApproverInfoCollection, int? CompanyID, int? StartRecPeriodID, string AddedBy, DateTime? DateAdded, string RevisedBy, DateTime? DateRevised, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataTable dtCompanyJEWriteOffOnApprovers = ServiceHelper.ConvertCompanyJEWriteOffOnApproverInfoToDataTable(oCompanyJEWriteOffOnApproverInfoCollection);
                CompanyJEWriteOffOnApproverDAO oCompanyJEWriteOffOnApproverDAO = new CompanyJEWriteOffOnApproverDAO(oAppUserInfo);
                oConnection = oCompanyJEWriteOffOnApproverDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();
                oCompanyJEWriteOffOnApproverDAO.SaveCompanyJEWriteOffOnApprovers(dtCompanyJEWriteOffOnApprovers, CompanyID, StartRecPeriodID, AddedBy, DateAdded, RevisedBy, DateRevised, oConnection, oTransaction);
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

    }
}
