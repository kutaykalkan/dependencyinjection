using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.App.DAO;
using System.Data;
using System.Data.SqlClient;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Params;

namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "RoleConfiguration" here, you must also update the reference to "RoleConfiguration" in Web.config.
    public class AttributeConfiguration : IAttributeConfiguration
    {


        #region IRoleConfiguration Members


        public void SaveAttributeConfig(AttributeConfigParamInfo oAttributeConfigParamInfo, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                AttributeConfigDAO oAttributeConfigDAO = new AttributeConfigDAO(oAppUserInfo);
                oAttributeConfigDAO.SaveAttributes(oAttributeConfigParamInfo.CompanyRoleConfigInfoList, 
                    oAttributeConfigParamInfo.CompanyID, oAttributeConfigParamInfo.UserLoginID, oAttributeConfigParamInfo.DateRevised,
                    oAttributeConfigParamInfo.UserID);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
        }

        public List<CompanyAttributeConfigInfo> GetCompanyAttributeConfigInfoList(AttributeConfigParamInfo oAttributeConfigParamInfo, AppUserInfo oAppUserInfo)
        {
            List<CompanyAttributeConfigInfo> oCompanyAttributeConfigInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                AttributeConfigDAO oAttributeConfigDAO = new AttributeConfigDAO(oAppUserInfo);
                oCompanyAttributeConfigInfoList = oAttributeConfigDAO.GetAttributeInfoList(oAttributeConfigParamInfo.CompanyID,
                    oAttributeConfigParamInfo.AttributeSetTypeID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oCompanyAttributeConfigInfoList;
        }

        #endregion


    }
}
