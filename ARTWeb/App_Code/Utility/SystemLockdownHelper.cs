using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.IServices;

namespace SkyStem.ART.Web.Utility
{
    /// <summary>
    /// Summary description for SystemLockdownHelper
    /// </summary>
    public class SystemLockdownHelper
    {
        private SystemLockdownHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region Service Calls
        public static List<SystemLockdownReasonMstInfo> GetAllSystemLockdownReasons()
        {
            ISystemLockdown oSystemLockdown = RemotingHelper.GetSystemLockdownObject();
            List<SystemLockdownReasonMstInfo> oSystemLockdownReasonMstInfoList = oSystemLockdown.GetAllSystemLockdownReasons(Helper.GetAppUserInfo());
            return oSystemLockdownReasonMstInfoList;
        }
        #endregion
    }
}