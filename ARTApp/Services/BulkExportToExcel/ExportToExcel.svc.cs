using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.IServices.BulkExportToExcel;
using SkyStem.ART.Client.Model.BulkExportExcel;
using SkyStem.ART.App.DAO.BulkExportToExcel;
using SkyStem.ART.App.ServerSideIAsyncResultOperation;
using System.Threading;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.App.Utility;
using System.Data.SqlClient;

namespace SkyStem.ART.App.Services.BulkExportToExcel
{
    // NOTE: If you change the class name "ExportToExcel" here, you must also update the reference to "ExportToExcel" in Web.config.
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ExportToExcel : IBulkExportToExcel
    {

        #region IBulkExportToExcel Members

        public void SaveBulkExportToExcelDetails(BulkExportToExcelInfo objBulkExportToExcel, AppUserInfo oAppUserInfo)
        {
            ServiceHelper.SetConnectionString(oAppUserInfo);
            BulkExportToExcelDAO objExportToExcelDAO = new BulkExportToExcelDAO(oAppUserInfo);
            objExportToExcelDAO.SaveExportDetails(objBulkExportToExcel);
        }

        public IAsyncResult BeginSaveBulkExportToExcelDetails(BulkExportToExcelInfo objBulkExportToExcel, AsyncCallback callback, object state, AppUserInfo oAppUserInfo)
        {
            GetAsyncResult asyncResult =
                new GetAsyncResult(callback, state);
            asyncResult.ExportExcelInfoValue = objBulkExportToExcel;

            ThreadPool.QueueUserWorkItem(
                new WaitCallback((Callback)),
                asyncResult);

            return asyncResult;
        }

        public void EndSaveBulkExportToExcelDetails(IAsyncResult asyncResult)
        {

        }


        public List<AccountHdrInfo> GetAccountDetails(AccountSearchCriteria objAccountSearchCriteria, AppUserInfo oAppUserInfo)
        {
            IAccount oAccountClient = new Account();
            ServiceHelper.SetConnectionString(oAppUserInfo);
            return oAccountClient.SearchAccount(objAccountSearchCriteria, oAppUserInfo);

        }

        public IAsyncResult BeginGetAccountDetails(AccountSearchCriteria objAccountSearchCriteria, AsyncCallback callback, object state, AppUserInfo oAppUserInfo)
        {
            ServiceHelper.SetConnectionString(oAppUserInfo);
            GetAsyncResult asyncResult =
                new GetAsyncResult(callback, state);
            asyncResult.AccountHdrInfoValue = objAccountSearchCriteria;
            asyncResult.AppUserInfoValue = oAppUserInfo;

            ThreadPool.QueueUserWorkItem(
                new WaitCallback((CallbackAccountDetails)),
                asyncResult);

            return asyncResult;
        }

        public void EndGetAccountDetails(IAsyncResult asyncResult)
        {

        }

        #endregion

        private void Callback(object asyncResult)
        {
            GetAsyncResult getSquareRootAsyncResult
             = (GetAsyncResult)asyncResult;
            BulkExportToExcelDAO objExportToExcelDAO = new BulkExportToExcelDAO(getSquareRootAsyncResult.AppUserInfoValue);


            try
            {
                getSquareRootAsyncResult.ExportExcelInfoResult =
                   objExportToExcelDAO.SaveExportDetails(getSquareRootAsyncResult.ExportExcelInfoValue);
            }
            finally
            {
                getSquareRootAsyncResult.OnCompleted();
            }
        }

        private void CallbackAccountDetails(object asyncResult)
        {
            IAccount oAccountClient = new Account();

            GetAsyncResult getSquareRootAsyncResult
                = (GetAsyncResult)asyncResult;
            try
            {
                getSquareRootAsyncResult.AccountHdrInfoResult =
                   oAccountClient.SearchAccount(getSquareRootAsyncResult.AccountHdrInfoValue, getSquareRootAsyncResult.AppUserInfoValue);
            }
            finally
            {
                getSquareRootAsyncResult.OnCompleted();
            }
        }

        public List<BulkExportToExcelInfo> GetRequests(int? RecPeriodID, int? UserID, short? RoleID, List<short> RequestTypeList, AppUserInfo oAppUserInfo)
        {
            List<BulkExportToExcelInfo> oBulkExportToExcelInfoList = new List<BulkExportToExcelInfo>();
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                BulkExportToExcelDAO oBulkExportToExcelDAO = new BulkExportToExcelDAO(oAppUserInfo);
                oBulkExportToExcelInfoList = oBulkExportToExcelDAO.GetRequests(RecPeriodID, UserID, RoleID, RequestTypeList);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oBulkExportToExcelInfoList;
        }

        public List<DataImportHdrInfo> DeleteRequests(List<int> SelectedRequestIDs, int CompanyID, string revisedBy, DateTime dateRevised, AppUserInfo oAppUserInfo)
        {
            List<DataImportHdrInfo> oDataImportHdrInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                BulkExportToExcelDAO oBulkExportToExcelDAO = new BulkExportToExcelDAO(oAppUserInfo);
                oDataImportHdrInfoList = oBulkExportToExcelDAO.DeleteRequests(SelectedRequestIDs, CompanyID, revisedBy, dateRevised);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oDataImportHdrInfoList;
        }

        public List<BulkExportToExcelInfo> GetAllRecBinders(int? companyID, int? UserID, short? RoleID, List<short> RequestTypeList, AppUserInfo oAppUserInfo)
        {
            List<BulkExportToExcelInfo> oBulkExportToExcelInfoList = new List<BulkExportToExcelInfo>();
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                BulkExportToExcelDAO oBulkExportToExcelDAO = new BulkExportToExcelDAO(oAppUserInfo);
                oBulkExportToExcelInfoList = oBulkExportToExcelDAO.GetAllRecBinders(companyID, UserID, RoleID, RequestTypeList);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oBulkExportToExcelInfoList;
        }
    }
}
