using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Shared.Data;
using System.Configuration;
using System.IO;

namespace SkyStem.ART.Shared.Utility
{
    /// <summary>
    /// Summary description for AppSettingHelper
    /// </summary>
    public class SharedAppSettingHelper
    {
        public SharedAppSettingHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static string GetAppSettingValue(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
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
                appID = GetAppSettingValue(SharedAppSettingConstants.APPLICATION_ID);
                if (!string.IsNullOrEmpty(appID))
                    applicationID = Convert.ToInt32(appID);
            }
            catch
            {
                // do nothing
            }
            return applicationID;
        }

        public static int GetDefaultLanguageID()
        {
            int lcid = 1033;
            try
            {
                string langID = string.Empty;
                langID = GetAppSettingValue(SharedAppSettingConstants.DEFAULT_LANGUAGE_ID);
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
                strBusinessEntityID = GetAppSettingValue(SharedAppSettingConstants.DEFAULT_BUSINESS_ENTITY_ID);
                if (!string.IsNullOrEmpty(strBusinessEntityID))
                    businessEntityID = Convert.ToInt32(strBusinessEntityID);
            }
            catch { }
            return businessEntityID;
        }

        public static string GetConnectionStringForExcelFile(string excelFileFullPath, bool bReadHeaders)
        {
            string connStr = "";
            FileInfo excelFile = new FileInfo(excelFileFullPath);
            switch (excelFile.Extension.ToLower())
            {
                case FileExtensions.xls:
                    connStr = SharedAppSettingHelper.GetAppSettingValue("xlsExport");
                    break;
                case FileExtensions.xlsx:
                    connStr = SharedAppSettingHelper.GetAppSettingValue("xlsx");
                    break;
            }
            if (bReadHeaders)
            {
                connStr = string.Format(connStr, excelFile.FullName, "YES");
            }
            else
            {
                connStr = string.Format(connStr, excelFile.FullName, "NO");
            }
            return connStr;
        }
        public static string GetConnectionStringForDelimitedFile(string DelimitedFileFullPath, bool bReadHeaders)
        {
            string connStr = "";
            FileInfo DelimitedFile = new FileInfo(DelimitedFileFullPath);
            switch (DelimitedFile.Extension.ToLower())
            {
                case FileExtensions.csv:
                    connStr = SharedAppSettingHelper.GetAppSettingValue("DelimitedFileConnString");
                    break;
            }
            if (bReadHeaders)
            {
                connStr = string.Format(connStr, DelimitedFile.DirectoryName, "YES", GetDelimiter());
            }
            else
            {
                connStr = string.Format(connStr, DelimitedFile.DirectoryName, "NO", GetDelimiter());
            }
            return connStr;
        }
        public static string GetDelimiter()
        {
            string strDelimiter = "Delimited(,)";
            if (!string.IsNullOrEmpty(SharedAppSettingHelper.GetAppSettingValue("Delimiter")))
                strDelimiter = "Delimited(" + SharedAppSettingHelper.GetAppSettingValue("Delimiter") + ")";
            return strDelimiter;
        }

    }
}
