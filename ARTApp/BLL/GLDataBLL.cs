using SkyStem.ART.App.DAO;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyStem.ART.App.BLL
{
    public class GLDataBLL
    {
        private GLDataBLL()
        {

        }
        /// <summary>
        /// Check permission for gl attachment 
        /// </summary>
        /// <param name="GLDataID"></param>
        /// <param name="UserID"></param>
        /// <param name="RoleID"></param>
        /// <param name="oAppUserInfo"></param>
        /// <returns></returns>
        public static bool CheckGLPermissions(long? GLDataID, int? UserID, short? RoleID, AppUserInfo oAppUserInfo)
        {
            // System Admin have access to all
            if (RoleID == (short)ARTEnums.UserRole.SYSTEM_ADMIN)
                return true;
            // User will not be able to get detail if GL is not accessible
            GLDataHdrDAO oGLDataHdrDAO = new GLDataHdrDAO(oAppUserInfo);
            GLDataHdrInfo oGLDataHdrInfo = oGLDataHdrDAO.GetGLDataHdrInfo(GLDataID, oAppUserInfo.RecPeriodID, UserID, RoleID);
            if (oGLDataHdrInfo != null && oGLDataHdrInfo.GLDataID.GetValueOrDefault() > 0)
                return true;
            return false;
        }
    }
}