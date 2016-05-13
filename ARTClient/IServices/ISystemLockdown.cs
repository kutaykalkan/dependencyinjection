using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.Client.IServices
{
    [ServiceContract]
    public interface ISystemLockdown
    {
        [OperationContract]
        SystemLockdownInfo GetSystemLockdownStautsAndHandleTimeout(int? companyID, int timeOutMinutes, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<SystemLockdownReasonMstInfo> GetAllSystemLockdownReasons(AppUserInfo oAppUserInfo);
    }
}
