using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.DAO;
using SkyStem.ART.App.Utility;
using System.Data.SqlClient;
using SkyStem.ART.Client.Params;
using SkyStem.ART.App.DAO.CompanyDatabase;
using SkyStem.ART.Client.Model.CompanyDatabase;

namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "Role" here, you must also update the reference to "Role" in Web.config.
    public class Role : IRole
    {
        public List<RoleMstInfo> GetAllRole(int? companyID, DateTime? periodEndDate, AppUserInfo oAppUserInfo)
        {
            List<RoleMstInfo> oRoleMstInfoList = null; 
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                RoleMstDAO oRoleMstDAO = new RoleMstDAO(oAppUserInfo);
                oRoleMstInfoList = oRoleMstDAO.GetAllRole(companyID, periodEndDate);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oRoleMstInfoList;
        }

        public List<RoleMstInfo> GetAllRolesFromCore(AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionStringCore(oAppUserInfo);
                RoleMstDAO oRoleMstDAO = new RoleMstDAO(oAppUserInfo);
                return oRoleMstDAO.GetAllRoles();
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return null;
        }

        public RoleMstInfo GetRole(short? roleID, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                RoleMstDAO oRoleMstDAO = new RoleMstDAO(oAppUserInfo);
                return oRoleMstDAO.SelectById(roleID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return null;
        }


        public List<DashboardMstInfo> GetDashboardsByRole(short? RoleID, int? RecPeriodID, AppUserInfo oAppUserInfo)
        {
            List<DashboardMstInfo> oDashboardMstInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DashboardMstDAO oDashboardMstDAO = new DashboardMstDAO(oAppUserInfo);
                oDashboardMstInfoCollection = oDashboardMstDAO.GetDashboardsByRole(RoleID, RecPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oDashboardMstInfoCollection;
        }


        public List<MenuMstInfo> GetMenuByRoleID(MenuParamInfo oMenuParamInfo, AppUserInfo oAppUserInfo)
        {
            List<MenuMstInfo> oMenuMstInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                MenuMstDAO oMenuMstDAO = new MenuMstDAO(oAppUserInfo);
                oMenuMstInfoCollection = oMenuMstDAO.GetMenuByRoleID(oMenuParamInfo);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oMenuMstInfoCollection;
        }
    }
}
