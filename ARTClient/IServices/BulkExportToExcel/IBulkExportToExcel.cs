using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model.BulkExportExcel;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.Client.IServices.BulkExportToExcel
{
    // NOTE: If you change the interface name "IBulkExportToExcel" here, you must also update the reference to "IBulkExportToExcel" in App.config.
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IBulkExportToExcel
    {
        [OperationContract]
        void SaveBulkExportToExcelDetails(BulkExportToExcelInfo objBulkExportToExcel, AppUserInfo oAppUserInfo);
        
        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginSaveBulkExportToExcelDetails(BulkExportToExcelInfo objBulkExportToExcel, AsyncCallback callback, object state, AppUserInfo oAppUserInfo);

        void EndSaveBulkExportToExcelDetails(IAsyncResult asyncResult);


        [OperationContract]
        List<AccountHdrInfo> GetAccountDetails(AccountSearchCriteria objAccountSearchCriteria, AppUserInfo oAppUserInfo);

        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginGetAccountDetails(AccountSearchCriteria objAccountSearchCriteria, AsyncCallback callback, object state, AppUserInfo oAppUserInfo);

        void EndGetAccountDetails(IAsyncResult asyncResult);
        [OperationContract]
        List<BulkExportToExcelInfo> GetRequests(int? RecPeriodID, int? UserID, short? RoleID, List<short> RequestTypeList, AppUserInfo oAppUserInfo);
        [OperationContract]
        List<DataImportHdrInfo> DeleteRequests(List<int> SelectedRequestIDs, int CompanyID, string revisedBy, DateTime dateRevised, AppUserInfo oAppUserInfo);
        [OperationContract]
        List<BulkExportToExcelInfo> GetAllRecBinders(int? companyID, int? UserID, short? RoleID, List<short> RequestTypeList, AppUserInfo oAppUserInfo);
    }
}


