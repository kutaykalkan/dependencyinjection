using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.Client.IServices
{
    // NOTE: If you change the interface name "ICertificationStatus" here, you must also update the reference to "ICertificationStatus" in Web.config.
    [ServiceContract]
    public interface ICertificationStatus
    {
        [OperationContract]
        List<DynamicPlaceholderMstInfo> getAllDynamicPlaceholderMstInfo(AppUserInfo oAppUserInfo);

        [OperationContract]
        void InsertCertificationVerbiageInfo(List<CertificationVerbiageInfo> oCertificationVerbiageInfoCollection, int RecPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<CertificationVerbiageInfo> GetCertificationVerbiageByCompanyIDRecPeriodID(int companyID, int recPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<CertificationStatusMstInfo> GetAllCertificationStatus( AppUserInfo oAppUserInfo);
    }//end of ineterface
}//end of namespace
