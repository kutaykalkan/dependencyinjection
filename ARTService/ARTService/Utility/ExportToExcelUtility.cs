using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Configuration;
using System.Data.OleDb;
using System.Net.Mail;
using ARTExportToExcelApp.APP.DAO;
using SkyStem.ART.Shared.Utility;
using SkyStem.ART.Service.Model;
using SkyStem.ART.Service.APP.BLL;
using SkyStem.ART.Service.Data;
using SkyStem.ART.Client.Model.CompanyDatabase;
using ClientModel = SkyStem.ART.Client.Model;

namespace SkyStem.ART.Service.Utility
{
    public class ExportToExcelUtility
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="filepath"></param>
        /// <param name="tablename"></param>
        public static void CreateExcelFile(DataTable dtExport, ExportToExcelInfo objExportToExcel, string filePath, string sheetName, List<ClientModel.LogInfo> oLogInfoCache, CompanyUserInfo oCompanyUserInfo)
        {
            try
            {
                string connectionString = GetConnectionStringForExcelFile(filePath, true);
                if (!ExcelHelper.ExportDataToExcel(dtExport, filePath, sheetName))
                {
                    filePath = null;
                }
            }
            catch (Exception ex)
            {
                Helper.LogInfoToCache(ex, oLogInfoCache);
            }
            if (oLogInfoCache.Count == 0)
            {
                objExportToExcel.OutputFile = filePath;
                objExportToExcel.OutputFileSize = Helper.GetFileSize(filePath);
                objExportToExcel.IsRequestSuccessFull = true;
            }
            else
            {
                objExportToExcel.OutputFile = null;
                objExportToExcel.IsRequestSuccessFull = false;
            }
        }

        public static OleDbConnection GetExcelFileConnection(string excelFileFullPath, bool bReadHeaders)
        {
            string conStrexcel = GetConnectionStringForExcelFile(excelFileFullPath, bReadHeaders);
            OleDbConnection oConnExcelFile = new OleDbConnection(conStrexcel);
            return oConnExcelFile;
        }

        public static string GetConnectionStringForExcelFile(string excelFileFullPath, bool bReadHeaders)
        {
            string connStr = "";
            FileInfo excelFile = new FileInfo(excelFileFullPath);
            switch (excelFile.Extension)
            {
                case ".xls":
                    connStr = GetAppSettingFromKey("xls");
                    break;
                case ".xlsx":
                    connStr = GetAppSettingFromKey("xlsx");
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

        public static string GetAppSettingFromKey(string keyName)
        {
            string appsettingValue = "";
            appsettingValue = ConfigurationManager.AppSettings[keyName];
            return appsettingValue;
        }
    }
}
