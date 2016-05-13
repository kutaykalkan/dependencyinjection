using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Params;


namespace SkyStem.ART.Client.IServices
{
    // NOTE: If you change the interface name "IRole" here, you must also update the reference to "IRole" in Web.config.
    [ServiceContract]
    public interface IRole
    {
        [OperationContract]
        List<RoleMstInfo> GetAllRole(int? companyID, DateTime? periodEndDate, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<RoleMstInfo> GetAllRolesFromCore(AppUserInfo oAppUserInfo);

        [OperationContract]
        RoleMstInfo GetRole(short? roleID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<DashboardMstInfo> GetDashboardsByRole(short? RoleID, int? RecPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<MenuMstInfo> GetMenuByRoleID(MenuParamInfo oMenuParamInfo, AppUserInfo oAppUserInfo);
    }
}
