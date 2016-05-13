using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Params;

namespace SkyStem.ART.Client.IServices
{
    [ServiceContract]
    public interface IAttributeConfiguration
    {
        [OperationContract]
        void SaveAttributeConfig(AttributeConfigParamInfo oRoleConfigparamInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<CompanyAttributeConfigInfo> GetCompanyAttributeConfigInfoList(AttributeConfigParamInfo oRoleConfigParamInfo, AppUserInfo oAppUserInfo);
    }
}
