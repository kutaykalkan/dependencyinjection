using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.DAO;
using System.Data.SqlClient;
using SkyStem.ART.App.Utility;
using System.Data;
namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "AppSetting" here, you must also update the reference to "AppSetting" in Web.config.
    public class AppSetting : IAppSetting
    {
        public AppSettingsInfo SelectAppSettingByAppSettingID(int AppSettingID, AppUserInfo oAppUserInfo)
        {
            AppSettingsInfo oAppSettingsInfo = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                AppSettingsDAO oAppSettingsDAO = new AppSettingsDAO(oAppUserInfo);
                oAppSettingsInfo = oAppSettingsDAO.SelectAppSettingByAppSettingID(AppSettingID);
                
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oAppSettingsInfo;
            
        }
    }
}
