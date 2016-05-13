using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkyStem.ART.Client.Data
{
    public class ARTConstants
    {
        public const string APP_NAME = "SkyStemART_";
        public const string CACHE_KEY_PREFIX = APP_NAME + "CacheKey_";

        public const string ROLE_TEXT_SKYSTEM_ADMIN = "SkyStem Admin";
        public const string BREADCRUMBS_SEPARATOR = " > ";

        public const string LOGGER_NAME = "SkyStemARTLogger";
        public const string UI_SOURCE_NAME = "UI";
        public const string BLL_SOURCE_NAME = "BLL";
        public const string WINDOWS_SERVICE_SOURCE_NAME = "WINDOWS SERVICE";
        public const string LOG_INFO = "INFO";
        public const string LOG_ERROR = "ERROR";
        public const string ART_TEMPLATE = "-1";
    }
}
