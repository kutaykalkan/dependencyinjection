using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyStem.ART.App.Data
{
    internal class AppConstants
    {
    }

    internal class AppSettingConstants
    {
        public const string CACHE_REFRESH_RATE = "CacheRefreshRate";
        public const string DB_COMMAND_TIMEOUT = "DBCommandTimeOut";


        #region FilterSettings
        public const string ACCOUNT_FILTER_VALUE_SEPARATOR = "AccountFilterValueSeparator";
        public const string FILTER_VALUE_SEPARATOR = "FilterValueSeparator";
        #endregion
    }

    internal class ConnectionStringConstants
    {
        public const string CONNECTION_STRING_SKYSTEMART = "connectionString";
        public const string CONNECTION_STRING_SKYSTEMART_CREATE_COMPANY = "connectionStringCreateCompany";
        public const string CONNECTION_STRING_SKYSTEMART_CORE = "connectionStringCore";
        public const string CONNECTION_STRING_SKYSTEMART_SPECIFIC = "connectionStringSpecific";
    }

    internal class CacheConstants
    {
        public const string CACHE_KEY_PREFIX = "AppCache_";
        public const string CACHE_KEY_COMPANY_DATABASE = CACHE_KEY_PREFIX + "CompanyDatabase";
    }

}
