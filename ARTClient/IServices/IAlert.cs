using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.Client.IServices
{
    // NOTE: If you change the interface name "IAlert" here, you must also update the reference to "IAlert" in Web.config.
    [ServiceContract]
    public interface IAlert
    {
        /// <summary>
        /// Selects all alerts in the system
        /// </summary>
        /// <param name="languageID">Current user Language ID</param>
        /// <param name="businessEntityID">Current company ID</param>
        /// <param name="defaultLanguageID">English(1033)</param>
        /// <returns>List of Alert Mst Info</returns>
        [OperationContract]
        List<AlertMstInfo> SelectAllAlertMstInfo(int languageID, int businessEntityID, int defaultLanguageID, int? companyID, int? recPeriodID, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Selects all alert for given company and rec period
        /// </summary>
        /// <param name="companyID">Unique identifier for current company</param>
        /// <param name="recPeriodID">Unique identifier for rec period</param>
        /// <returns>List of company alerts</returns>
        [OperationContract]
        List<CompanyAlertInfo> SelectComapnyAlertByCompanyIDAndRecPeriodID(int companyID, int? recPeriodID, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Inserts new record in company alert table if not already exists
        /// Otherwise updates existing record and inserts new one.
        /// </summary>
        /// <param name="oCompanyAlertInfoCollection">List of company alerts activated in the given rec period</param>
        /// <param name="recPeriodID">Unique identifier for the rec period</param>
        [OperationContract]
        void UpdateCompanyAlert(List<CompanyAlertInfo> oCompanyAlertInfoCollection, int recPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<CompanyAlertDetailInfo> GetCompanyAlertDetailByRoleId(int? UserID, int? RoleID, int? RecID, int? AlertTpye, AppUserInfo oAppUserInfo);
        [OperationContract]
        bool UpdateIsRead(List<long> CompanyAlertDetailUserIDCollection, AppUserInfo oAppUserInfo);

        [OperationContract]
        bool CheckIsReadMsg(int? UserID, int? RoleID, int? RecID, int? AlertTpye, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Gets description for the given alert as well as the replacement string if any
        /// </summary>
        /// <param name="alertID">Unique identifier for alert for which description is to be fetched</param>
        /// <param name="recPeriodID">Unique identifier of Rec period</param>
        /// <param name="oAccountIDCollection">List of account IDs which are being updated</param>
        /// <param name="replacement">The string which needs to be replaced in the alert decsription</param>
        /// <returns>Alert description label id</returns>
        [OperationContract]
        int? GetAlertDescriptionAndReplacementString(short alertID, int recPeriodID, List<long> oAccountIDCollection, out string replacement, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Inserts record in CompanyAlertDetail and CompanyAlertDetailUser table
        /// </summary>
        /// <param name="oCompanyAlertDetailInfoCollection">List of Company Detail Info object</param>
        [OperationContract]
        void InsertCompanyAlertDetail(List<CompanyAlertDetailInfo> oCompanyAlertDetailInfoCollection, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<CompanyAlertInfo> GetRaiseAlertData(AppUserInfo oAppUserInfo);

        [OperationContract]
        void CreateDataForCompanyAlertID(CompanyAlertInfo oCompanyAlertInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<CompanyAlertDetailUserInfo> GetAlertMailDataForCompanyAlertID(CompanyAlertInfo oCompanyAlertInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        void UpdateSentMailStatus(List<CompanyAlertDetailUserInfo> oCompanyAlertDetailUserInfoList, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<CompanyAlertDetailUserInfo> GetUserListForNewAccountAlert(int dataImportID, int companyID, AppUserInfo oAppUserInfo);

        [OperationContract]
         List<CompanyAlertDetailInfo> GetCompanyAlertDetail(long? CompanyAlertDetailID, AppUserInfo oAppUserInfo);
    }
}
