using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using SkyStem.ART.App.Data;

namespace SkyStem.ART.App.Utility
{
    public class CacheHelper
    {
        private CacheHelper()
        {
        }

        private static DateTime GetCacheExpirationTime()
        {
            int noOfHrs = 24; // Default Value
            if (!string.IsNullOrEmpty(AppSettingHelper.GetAppSettingValue(AppSettingConstants.CACHE_REFRESH_RATE)))
            {
                noOfHrs = Convert.ToInt32(AppSettingHelper.GetAppSettingValue(AppSettingConstants.CACHE_REFRESH_RATE));
            }
            return DateTime.Now.AddHours(noOfHrs);
        }

        public static string GetCompanyConnectionString(int? companyID)
        {
            Dictionary<Int32?, string> dictCompanyConnectionString = (Dictionary<Int32?, string>)HttpRuntime.Cache[CacheConstants.CACHE_KEY_COMPANY_DATABASE];
            if (dictCompanyConnectionString != null)
            {
                if (dictCompanyConnectionString.ContainsKey(companyID))
                    return dictCompanyConnectionString[companyID];
            }
            return null;
        }

        public static void SetCompanyConnectionString(int? companyID, string cnnString)
        {
            if (companyID.GetValueOrDefault() > 0 && !string.IsNullOrEmpty(cnnString))
            {
                Dictionary<Int32?, string> dictCompanyConnectionString = (Dictionary<Int32?, string>)HttpRuntime.Cache[CacheConstants.CACHE_KEY_COMPANY_DATABASE];
                if (dictCompanyConnectionString == null)
                {
                    dictCompanyConnectionString = new Dictionary<int?, string>();
                }
                if (!dictCompanyConnectionString.ContainsKey(companyID))
                {
                    dictCompanyConnectionString.Add(companyID, cnnString);
                    HttpRuntime.Cache.Add(CacheConstants.CACHE_KEY_COMPANY_DATABASE, dictCompanyConnectionString, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
            }
        }
    }
}
