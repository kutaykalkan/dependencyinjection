using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace SkyStem.ART.Integration.Services.Utility
{
    /// <summary>
    /// Summary description for AppSettingHelper
    /// </summary>
    public class AppSettingHelper
    {
        private AppSettingHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static string GetAppSettingValue(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }
    }
}
