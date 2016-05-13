using System;
using System.Data;
using System.Configuration;
using System.Linq;

namespace SkyStem.ART.ARTSecurity.Utility
{

    /// <summary>
    /// Summary description for AppSettingHelper
    /// </summary>
    public class AppSettingHelper
    {
        public AppSettingHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static string GetAppSettingValue(string key)
        {
            try
            {
                return ConfigurationManager.AppSettings[key];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}