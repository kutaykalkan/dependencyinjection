using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.App.DAO;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.Utility;

namespace SkyStem.ART.App.Services
{
    public class SystemLockdown : ISystemLockdown
    {
        #region ISystemLockdown Members

        public SystemLockdownInfo GetSystemLockdownStautsAndHandleTimeout(int? companyID, int timeOutMinutes, AppUserInfo oAppUserInfo)
        {
            ServiceHelper.SetConnectionString(oAppUserInfo);
            SystemLockdownDAO oSystemLockdownDAO = new SystemLockdownDAO(oAppUserInfo);
            return oSystemLockdownDAO.GetSystemLockdownStautsAndHandleTimeout(companyID, timeOutMinutes);
        }

        public List<SystemLockdownReasonMstInfo> GetAllSystemLockdownReasons(AppUserInfo oAppUserInfo)
        {
            ServiceHelper.SetConnectionString(oAppUserInfo);
            SystemLockdownReasonMstDAO oSystemLockdownReasonMstDAO = new SystemLockdownReasonMstDAO(oAppUserInfo);
            return oSystemLockdownReasonMstDAO.GetAllSystemLockdownReasons();
        }
        
        #endregion
    }
}
