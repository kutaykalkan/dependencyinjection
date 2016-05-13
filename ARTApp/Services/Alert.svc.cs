using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using System.Data.SqlClient;
using SkyStem.ART.App.Utility;
using SkyStem.ART.App.DAO;
using System.Data;

namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "Alert" here, you must also update the reference to "Alert" in Web.config.
    public class Alert : IAlert
    {
        public List<AlertMstInfo> SelectAllAlertMstInfo(int languageID, int businessEntityID, int defaultLanguageID, int? companyID, int? recPeriodID, AppUserInfo oAppUserInfo)
        {
            List<AlertMstInfo> oAlertMstInfoCollection = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                AlertMstDAO oAlertMstDAO = new AlertMstDAO(oAppUserInfo);
                oAlertMstInfoCollection = oAlertMstDAO.SelectAllAlertMstInfo(languageID, businessEntityID, defaultLanguageID, companyID, recPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oAlertMstInfoCollection;
        }

        public List<CompanyAlertInfo> SelectComapnyAlertByCompanyIDAndRecPeriodID(int companyID, int? recPeriodID, AppUserInfo oAppUserInfo)
        {
            List<CompanyAlertInfo> oCompanyAlertInfoCollection = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CompanyAlertDAO oCompanyAlertDAO = new CompanyAlertDAO(oAppUserInfo);
                oCompanyAlertInfoCollection = oCompanyAlertDAO.SelectComapnyAlertByCompanyIDAndRecPeriodID(companyID, recPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oCompanyAlertInfoCollection;
        }

        public void UpdateCompanyAlert(List<CompanyAlertInfo> oCompanyAlertInfoCollection, int recPeriodID, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CompanyAlertDAO oCompanyAlertDAO = new CompanyAlertDAO(oAppUserInfo);
                using (oConnection = oCompanyAlertDAO.CreateConnection())
                {
                    oConnection.Open();
                    using (oTransaction = oConnection.BeginTransaction())
                    {
                        DataTable dtCompanyAlert = ServiceHelper.ConvertCompanyAlertInfoCollectionToDataTable(oCompanyAlertInfoCollection);
                        oCompanyAlertDAO.UpdateCompanyAlert(dtCompanyAlert, recPeriodID, oConnection, oTransaction);

                        oTransaction.Commit();
                    }
                }
            }
            catch (SqlException ex)
            {
                try
                {
                    oTransaction.Rollback();
                }
                catch { }
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                try
                {
                    oTransaction.Rollback();
                }
                catch { }
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
        }
        public List<CompanyAlertDetailInfo> GetCompanyAlertDetailByRoleId(int? UserID, int? RoleID, int? RecID, int? AlertTpye, AppUserInfo oAppUserInfo)
        {
            List<CompanyAlertDetailInfo> oCompanyAlertDetailInfoCollection = null;


            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CompanyAlertDetailDAO oCompanyAlertDetailDAO = new CompanyAlertDetailDAO(oAppUserInfo);
                oCompanyAlertDetailInfoCollection = oCompanyAlertDetailDAO.GetCompanyAlertDetailByRoleId(UserID, RoleID, RecID, AlertTpye);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oCompanyAlertDetailInfoCollection;


        }


        public bool UpdateIsRead(List<long> CompanyAlertDetailUserIDCollection, AppUserInfo oAppUserInfo)
        {
            bool isSuccess = false;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CompanyAlertDetailDAO oCompanyAlertDetailDAO = new CompanyAlertDetailDAO(oAppUserInfo);
                isSuccess = oCompanyAlertDetailDAO.UpdateIsRead(CompanyAlertDetailUserIDCollection);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return isSuccess;

        }

        public bool CheckIsReadMsg(int? UserID, int? RoleID, int? RecID, int? AlertTpye, AppUserInfo oAppUserInfo)
        {
            bool IsUnReadmsg = false;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CompanyAlertDetailDAO oCompanyAlertDetailDAO = new CompanyAlertDetailDAO(oAppUserInfo);
                IsUnReadmsg = oCompanyAlertDetailDAO.CheckIsReadMsg(UserID, RoleID, RecID, AlertTpye);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return IsUnReadmsg;

        }

        public int? GetAlertDescriptionAndReplacementString(short alertID, int recPeriodID, List<long> oAccountIDCollection, out string replacement, AppUserInfo oAppUserInfo)
        {
            int? alertLabelID = null;
            replacement = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataTable dtAccountID = ServiceHelper.ConvertLongIDCollectionToDataTable(oAccountIDCollection);
                AlertMstDAO oAlertMstDAO = new AlertMstDAO(oAppUserInfo);
                alertLabelID = oAlertMstDAO.GetAlertDescriptionAndReplacementString(alertID, recPeriodID, dtAccountID, out replacement);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return alertLabelID;
        }

        public void InsertCompanyAlertDetail(List<CompanyAlertDetailInfo> oCompanyAlertDetailInfoCollection, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataTable dtCompanyAlertDetail = ServiceHelper.ConvertCompanyAlertDetailInfoCollectionToDataTable(oCompanyAlertDetailInfoCollection);

                CompanyAlertDetailDAO oCompanyAlertDetailDAO = new CompanyAlertDetailDAO(oAppUserInfo);
                oCompanyAlertDetailDAO.InsertCompanyAlertDetail(dtCompanyAlertDetail);
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
        public List<CompanyAlertInfo> GetRaiseAlertData(AppUserInfo oAppUserInfo)
        {
            List<CompanyAlertInfo> oCompanyAlertInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CompanyAlertDAO oCompanyAlertDAO = new CompanyAlertDAO(oAppUserInfo);
                oCompanyAlertInfoList = oCompanyAlertDAO.GetRaiseAlertData();
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oCompanyAlertInfoList;

        }
        public void CreateDataForCompanyAlertID(CompanyAlertInfo oCompanyAlertInfo, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CompanyAlertDAO oCompanyAlertDAO = new CompanyAlertDAO(oAppUserInfo);
                using (oConnection = oCompanyAlertDAO.CreateConnection())
                {
                    oConnection.Open();
                    using (oTransaction = oConnection.BeginTransaction())
                    {
                        oCompanyAlertDAO.CreateDataForCompanyAlertID(oCompanyAlertInfo, oConnection, oTransaction);
                        oTransaction.Commit();
                    }
                }
            }
            catch (SqlException ex)
            {
                try
                {
                    oTransaction.Rollback();
                }
                catch { }
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                try
                {
                    oTransaction.Rollback();
                }
                catch { }
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
        }
        public List<CompanyAlertDetailUserInfo> GetAlertMailDataForCompanyAlertID(CompanyAlertInfo oCompanyAlertInfo, AppUserInfo oAppUserInfo)
        {
            List<CompanyAlertDetailUserInfo> oCompanyAlertDetailUserInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CompanyAlertDetailUserDAO oCompanyAlertDAO = new CompanyAlertDetailUserDAO(oAppUserInfo);
                oCompanyAlertDetailUserInfoList = oCompanyAlertDAO.GetAlertMailDataForCompanyAlertID(oCompanyAlertInfo);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oCompanyAlertDetailUserInfoList;

        }
        public void UpdateSentMailStatus(List<CompanyAlertDetailUserInfo> oCompanyAlertDetailUserInfoList, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CompanyAlertDetailUserDAO oCompanyAlertDetailUserDAO = new CompanyAlertDetailUserDAO(oAppUserInfo);
                using (oConnection = oCompanyAlertDetailUserDAO.CreateConnection())
                {
                    oConnection.Open();
                    using (oTransaction = oConnection.BeginTransaction())
                    {
                        DataTable dtCompanyAlertDetailUserIDTable = ServiceHelper.ConvertCompanyAlertDetailUserToDataTable(oCompanyAlertDetailUserInfoList);
                        oCompanyAlertDetailUserDAO.UpdateSentMailStatus(dtCompanyAlertDetailUserIDTable, oConnection, oTransaction);
                        oTransaction.Commit();
                    }
                }
            }
            catch (SqlException ex)
            {
                try
                {
                    oTransaction.Rollback();
                }
                catch { }
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                try
                {
                    oTransaction.Rollback();
                }
                catch { }
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
        }
        public List<CompanyAlertDetailUserInfo> GetUserListForNewAccountAlert(int dataImportID, int companyID, AppUserInfo oAppUserInfo)
        {
            List<CompanyAlertDetailUserInfo> oCompanyAlertDetailUserInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CompanyAlertDetailUserDAO oCompanyAlertDAO = new CompanyAlertDetailUserDAO(oAppUserInfo);
                oCompanyAlertDetailUserInfoList = oCompanyAlertDAO.GetUserListForNewAccountAlert(dataImportID, companyID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oCompanyAlertDetailUserInfoList;
        }

        public List<CompanyAlertDetailInfo> GetCompanyAlertDetail(long? CompanyAlertDetailID, AppUserInfo oAppUserInfo)
        {
            List<CompanyAlertDetailInfo> oCompanyAlertDetailInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CompanyAlertDetailDAO oCompanyAlertDetailDAO = new CompanyAlertDetailDAO(oAppUserInfo);
                oCompanyAlertDetailInfoList = oCompanyAlertDetailDAO.GetCompanyAlertDetail(CompanyAlertDetailID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oCompanyAlertDetailInfoList;
        }
    }
}
