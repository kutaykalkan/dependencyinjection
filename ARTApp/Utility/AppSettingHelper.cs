using System.Configuration;

namespace SkyStem.ART.App.Utility
{
    /// <summary>
    /// Refactor to use IAppSettingHelper Interface in the shared project.
    /// </summary>
    public class AppSettingHelper
    {
        public static string GetAppSettingValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
