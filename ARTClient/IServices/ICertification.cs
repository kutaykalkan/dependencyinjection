using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.Client.IServices
{
    // NOTE: If you change the interface name "ICertification" here, you must also update the reference to "ICertification" in Web.config.
    [ServiceContract]
    public interface ICertification
    {
        [OperationContract]
        void SaveCertificationSignoffDetail(CertificationSignOffInfo oCertificationSignOffInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<CertificationVerbiageInfo> GetCertificationVerbiage(int? reconciliationPeriodID, int? companyID, short? certificationTypeID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<CertificationSignOffInfo> GetCertificationSignOff(int? reconciliationPeriodID, int? userID, short? roleID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<CertificationSignOffInfo> GetCertificationSignOffForJuniors(int? reconciliationPeriodID, int? userID, short? roleID, int? UserIDForAccess, short? RoleIDForAccess, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<MandatoryReportSignOffInfo> GetMandatoryReportSignOff(int? reportRoleMandatoryReportID, int? userID, int? reconciliationPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        void SaveMandatoryReportSignoff(MandatoryReportSignOffInfo oMandatoryReportSignOffInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<CertificationSignOffInfo> GetCertificationSignOffForJuniorsOfControllerAndCEOCFO(int? reconciliationPeriodID, int? userID, short? roleID, int? CompanyID, AppUserInfo oAppUserInfo);
       
        [OperationContract]
        bool GetIsCertificationStarted(int? reconciliationPeriodID, AppUserInfo oAppUserInfo);
    
    }
}
