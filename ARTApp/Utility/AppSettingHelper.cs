using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace SkyStem.ART.App.Utility
{
    public class AppSettingHelper
    {
        private AppSettingHelper()
        {
        }

        public static string GetAppSettingValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
