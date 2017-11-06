using System.Configuration;
using SkyStem.ART.Shared.Interfaces;

namespace SkyStem.ART.Shared.Utility
{
    public class AppSettingHelper : IAppSettingHelper
    {
        public string GetAppSettingValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}