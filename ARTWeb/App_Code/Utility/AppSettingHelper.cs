using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SkyStem.ART.Web.Data;

namespace SkyStem.ART.Web.Utility
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
                return System.Configuration.ConfigurationManager.AppSettings[key];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get the Test LCID for SkyStem - ART Application
        /// </summary>
        /// <returns></returns>
        public static int? GetTestLCID()
        {
            int? lcid = null;
            try
            {
                string appID = string.Empty;
                appID = GetAppSettingValue(AppSettingConstants.USE_TEST_LCID);
                if (!string.IsNullOrEmpty(appID))
                    lcid = Convert.ToInt32(appID);
            }
            catch 
            {
                // do nothing
            }
            return lcid;
        }


        /// <summary>
        /// Get the Application ID for SkyStem - ART Application
        /// </summary>
        /// <returns></returns>
        public static int GetApplicationID()
        {
            int applicationID = 1;
            try
            {
                string appID = string.Empty;
                appID = GetAppSettingValue(AppSettingConstants.APPLICATION_ID);
                if (!string.IsNullOrEmpty(appID))
                    applicationID = Convert.ToInt32(appID);
            }
            catch (Exception ex)
            {
                Helper.FormatAndShowErrorMessage(null, ex);
            }
            return applicationID;
        }

        public static int GetDefaultLanguageID()
        {
            int lcid = 1033;
            try
            {
                string langID = string.Empty;
                langID = GetAppSettingValue(AppSettingConstants.DEFAULT_LANGUAGE_ID);
                if (!string.IsNullOrEmpty(langID))
                    lcid = Convert.ToInt32(langID);
            }
            catch { }
            return lcid;
        }

        public static int GetDefaultBusinessEntityID()
        {
            int businessEntityID = 1;
            try
            {
                string strBusinessEntityID = string.Empty;
                strBusinessEntityID = GetAppSettingValue(AppSettingConstants.DEFAULT_BUSINESS_ENTITY_ID);
                if (!string.IsNullOrEmpty(strBusinessEntityID))
                    businessEntityID = Convert.ToInt32(strBusinessEntityID);
            }
            catch { }
            return businessEntityID;
        }


    }
}