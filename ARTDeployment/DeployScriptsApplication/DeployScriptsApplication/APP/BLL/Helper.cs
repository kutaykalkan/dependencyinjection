using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Configuration;
using System.IO;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Xml.Serialization;
using System.Security.Cryptography;
using System.Globalization;
using DeployScriptsApplication.Data;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.DAO;
using System.Windows.Documents;
using System.Windows.Controls;
using SkyStem.ART.Client.Model.CompanyDatabase;
using SkyStem.ART.App.DAO.CompanyDatabase;
using SkyStem.ART;

namespace DeployScriptsApplication.APP.BLL
{
    class Helper
    {
        static Helper()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
        public static void LogError(string message)
        {

            log4net.ILog oLogger = log4net.LogManager.GetLogger(DeployApplicationConstants.LOGGER_NAME);
            oLogger.Error(message);
        }


        public static void LogError(Exception oException)
        {

            log4net.ILog oLogger = log4net.LogManager.GetLogger(DeployApplicationConstants.LOGGER_NAME);
            oLogger.Error("", oException);

        }
        public static void LogInfo(string message)
        {

            log4net.ILog oLogger = log4net.LogManager.GetLogger(DeployApplicationConstants.LOGGER_NAME);
            oLogger.Info(message);

        }
        public static string GetAppSettingFromKey(string keyName)
        {
            string appsettingValue = "";
            appsettingValue = ConfigurationManager.AppSettings[keyName];
            return appsettingValue;
        }
        #region DataBase Calls

        public static List<VersionMstInfo> GetAllVersionList()
        {
            VersionMstDAO oVersionMstDAO = new VersionMstDAO();
            return oVersionMstDAO.GetAllVersionList();
        }
        public static int SaveNewVersion(VersionMstInfo oVersionMstInfo)
        {
            VersionMstDAO oVersionMstDAO = new VersionMstDAO();
            return oVersionMstDAO.SaveNewVersion(oVersionMstInfo);
        }
        public static List<VersionScriptInfo> GetVersionScriptList(int VersionID)
        {
            VersionScriptDAO oVersionScriptDAO = new VersionScriptDAO();
            return oVersionScriptDAO.GetVersionScriptList(VersionID);
        }
        public static List<ServerCompanyInfo> GetAllServerCompanyList(int VersionID)
        {
            ServerCompanyDAO oServerCompanyDAO = new ServerCompanyDAO(DbConstants.ConnectionString);
            return oServerCompanyDAO.GetAllServerCompanyList(VersionID);
        }
        public static List<CompanyVersionScriptInfo> GetAllCompanyVersionScriptInfoList(int VersionID)
        {
            CompanyVersionScriptDAO oCompanyVersionScriptDAO = new CompanyVersionScriptDAO();
            return oCompanyVersionScriptDAO.GetAllCompanyVersionScriptInfoList(VersionID);
        }
        public static void RemoveExecutedScripts(int CompanyID, int VersionID, List<VersionScriptInfo> oVersionScriptInfoList)
        {
            VersionScriptDAO oVersionScriptDAO = new VersionScriptDAO();
            List<long?> oExecutedVersionScriptIDList = oVersionScriptDAO.GetExcutedVersionScriptList(CompanyID, VersionID);
            if (oExecutedVersionScriptIDList != null && oExecutedVersionScriptIDList.Count > 0)
                oVersionScriptInfoList.RemoveAll(e => oExecutedVersionScriptIDList.Contains(e.VersionScriptID));
        }
        ////public static string RunScriptServerCompany(List<VersionScriptInfo> oVersionScriptInfoList, List<ServerCompanyInfo> oServerCompanyInfoList, bool? IsTakeBackup)
        ////{
        ////    string Msg = string.Empty;
        ////    string SavingStatusMsg = string.Empty;
        ////    List<CompanyVersionScriptInfo> oCompanyVersionScriptInfoList = new List<CompanyVersionScriptInfo>();
        ////    for (int i = 0; i < oServerCompanyInfoList.Count; i++)
        ////    {
        ////        List<VersionScriptInfo> oAllSelectedVersionScriptInfoList = new List<VersionScriptInfo>();
        ////        oAllSelectedVersionScriptInfoList.AddRange(oVersionScriptInfoList);
        ////        RemoveExecutedScripts(oServerCompanyInfoList[i].CompanyID.Value, oVersionScriptInfoList[0].VersionID.Value, oAllSelectedVersionScriptInfoList);

        ////        string BackupStatus = string.Empty;
        ////        if (IsTakeBackup.Value)
        ////        {
        ////            BackupStatus = CreateDBBackup(oServerCompanyInfoList[i]);
        ////            if (BackupStatus != string.Empty && BackupStatus == "Taking  DataBase Backup successfully")
        ////            {
        ////                ServerCompanyDAO oServerCompanyDAO = new ServerCompanyDAO(DbHelper.GetConnectionString(oServerCompanyInfoList[i]));
        ////                Msg = oServerCompanyDAO.RunCompanyScript(oAllSelectedVersionScriptInfoList, oServerCompanyInfoList[i], oCompanyVersionScriptInfoList);
        ////                if (Msg != "All Scripts Execute successfully")
        ////                    break;
        ////            }
        ////            else
        ////            {
        ////                Msg = BackupStatus;
        ////                break;
        ////            }
        ////        }
        ////        else
        ////        {
        ////            ServerCompanyDAO oServerCompanyDAO = new ServerCompanyDAO(DbHelper.GetConnectionString(oServerCompanyInfoList[i]));
        ////            Msg = oServerCompanyDAO.RunCompanyScript(oAllSelectedVersionScriptInfoList, oServerCompanyInfoList[i], oCompanyVersionScriptInfoList);
        ////            if (Msg != "All Scripts Execute successfully")
        ////                break;

        ////        }
        ////    }
        ////    if (oCompanyVersionScriptInfoList.Count > 0)
        ////    {
        ////        CompanyVersionScriptDAO oCompanyVersionScriptDAO = new CompanyVersionScriptDAO();
        ////        SavingStatusMsg = oCompanyVersionScriptDAO.InsertCompanyVersionScriptStatus(oCompanyVersionScriptInfoList);
        ////        Msg = Msg + " And " + SavingStatusMsg;
        ////    }
        ////    return Msg;
        ////}

        public static string RunScriptServerCompany(List<ServerCompanyInfo> oServerCompanyInfoList, bool? IsTakeBackup, CurrentDBVersion oNewCurrentDBVersion, List<CurrentDBVersion> oAllCurrentDBVersion)
        {

            string Msg = string.Empty;
            string ErrorMsg = string.Empty;
            string SavingStatusMsg = string.Empty;
            string BackupStatus = string.Empty;
            List<VersionScriptInfo> oAllVersionScriptInfoListToBeExecuted;
            List<CompanyVersionScriptInfo> oCompanyVersionScriptInfoList;
            foreach (ServerCompanyInfo oServerCompanyInfo in oServerCompanyInfoList)
            {
                oCompanyVersionScriptInfoList = new List<CompanyVersionScriptInfo>();
                var oCurrentDBVersion = oAllCurrentDBVersion.Find(obj => obj.CompanyID == oServerCompanyInfo.CompanyID || obj.ServerCompanyID == oServerCompanyInfo.ServerCompanyID);
                oAllVersionScriptInfoListToBeExecuted = GetAllVersionScriptInfoListToBeExecuted(oCurrentDBVersion.CurrentDBVersionID, oNewCurrentDBVersion.CurrentDBVersionID);

                if (oCurrentDBVersion != null && oAllVersionScriptInfoListToBeExecuted != null && oAllVersionScriptInfoListToBeExecuted.Count > 0)
                {

                    if (IsTakeBackup.Value)
                    {
                        // Take Backup For the company
                        BackupStatus = CreateDBBackup(oServerCompanyInfo);

                        if (BackupStatus != string.Empty && BackupStatus == "Taking  DataBase Backup successfully")
                        {
                            // Execute Scripts For the company
                            ServerCompanyDAO oServerCompanyDAO = new ServerCompanyDAO(DbHelper.GetConnectionString(oServerCompanyInfo));
                            Msg = oServerCompanyDAO.RunCompanyScript(oAllVersionScriptInfoListToBeExecuted, oServerCompanyInfo, oCompanyVersionScriptInfoList, oNewCurrentDBVersion);
                            if (Msg != "All Scripts Execute successfully")
                                ErrorMsg = Msg;
                        }
                        else
                        {
                            Msg = BackupStatus;
                            break;
                        }
                    }
                    else
                    {
                        // Execute Scripts For the company
                        ServerCompanyDAO oServerCompanyDAO = new ServerCompanyDAO(DbHelper.GetConnectionString(oServerCompanyInfo));
                        Msg = oServerCompanyDAO.RunCompanyScript(oAllVersionScriptInfoListToBeExecuted, oServerCompanyInfo, oCompanyVersionScriptInfoList, oNewCurrentDBVersion);
                        if (Msg != "All Scripts Execute successfully")
                            ErrorMsg = Msg;
                    }
                    // Save Status Message For the company
                    if (oCompanyVersionScriptInfoList.Count > 0)
                    {
                        CompanyVersionScriptDAO oCompanyVersionScriptDAO = new CompanyVersionScriptDAO();
                        SavingStatusMsg = oCompanyVersionScriptDAO.InsertCompanyVersionScriptStatus(oCompanyVersionScriptInfoList);
                        Msg = Msg + " And " + SavingStatusMsg;
                    }
                }

            }
            if (string.IsNullOrEmpty(ErrorMsg))
                return Msg;
            else
                return ErrorMsg;
        }
        private static List<VersionScriptInfo> GetAllVersionScriptInfoListToBeExecuted(int? LowerVersionID, int? HigherVersionID)
        {
            List<VersionScriptInfo> AllVersionScriptInfoListToBeExecuted = null;
            AllVersionScriptInfoListToBeExecuted = GetVersionScriptList(LowerVersionID, HigherVersionID);
            return AllVersionScriptInfoListToBeExecuted;
        }


        public static List<CompanyVersionScriptInfo> GetCompanyVersionScriptInfoListForSelectedRows(List<long> VersionScriptIDList, List<int> CompanyIDList)
        {
            CompanyVersionScriptDAO oCompanyVersionScriptDAO = new CompanyVersionScriptDAO();
            return oCompanyVersionScriptDAO.GetCompanyVersionScriptInfoListForSelectedRows(VersionScriptIDList, CompanyIDList);
        }
        public static string CreateDBBackup(ServerCompanyInfo oServerCompanyInfo)
        {
            string Status = String.Empty;

            string StrDate = DateTime.Now.Date.ToShortDateString().Replace('/', '_');


            string DBName = oServerCompanyInfo.DatabaseName;
            string DBBackupPath = ConfigurationManager.AppSettings["DBBackupPath"] + DBName + "_" + StrDate + ".bak";
            string DBBackUpName = DBName + "_" + StrDate;
            string BackUpScript = DBBackUpScript(DBName, DBBackupPath, DBBackUpName);
            ServerCompanyDAO oServerCompanyDAO = new ServerCompanyDAO(DbHelper.GetConnectionString(oServerCompanyInfo));
            Status = oServerCompanyDAO.RunDBBackupScript(BackUpScript);
            return Status;
        }
        public static string DBBackUpScript(string DBName, string DBBackupPath, string DBBackUpName)
        {
            StringBuilder oDBBackUpScript = new StringBuilder();
            oDBBackUpScript.AppendFormat(" BACKUP DATABASE {0} TO  DISK =N'{1}'  WITH NOFORMAT, NOINIT,  NAME =N'{2}', SKIP, NOREWIND, NOUNLOAD,  STATS = 10", DBName, DBBackupPath, DBBackUpName);
            return oDBBackUpScript.ToString();
        }
        public static string GetBaseFolder()
        {
            // There will be a Base folder(path from App config). 
            string baseFolderPath = @"";//read base folder path from  config.

            //Read base folder name and physical path
            baseFolderPath = ConfigurationManager.AppSettings["BaseFolderForScriptFiles"];

            //if folder for Files is not created, then create it
            if (!Directory.Exists(baseFolderPath))
            {
                Directory.CreateDirectory(baseFolderPath);
            }

            return baseFolderPath;
        }
        public static string GetBaseFolderForVersionNumber(string VersionID)
        {
            // There will be a Base folder(path from  config). 
            // Within Base Folder, there will be folders
            string baseFolderPath = @"";//read base folder path from  config.
            //Read base folder name and physical path
            baseFolderPath = Helper.GetBaseFolder();

            if (!baseFolderPath.EndsWith("\\"))
            {
                baseFolderPath += "\\";
            }
            baseFolderPath += VersionID;

            //if folder for company is not created at "Create Company", create it.
            if (!Directory.Exists(baseFolderPath))
            {
                Directory.CreateDirectory(baseFolderPath);
            }
            return baseFolderPath + @"\";
        }
        public static string GetVersionNumberFolderPath(string VersionID)
        {

            string baseFolderPath = @"";
            if (!baseFolderPath.EndsWith("\\"))
            {
                baseFolderPath += "\\";
            }
            baseFolderPath += VersionID;
            return baseFolderPath + @"\";
        }
        public static string SaveVersionScript(List<VersionScriptInfo> oVersionScriptInfoList)
        {
            VersionScriptDAO oVersionScriptDAO = new VersionScriptDAO();
            return oVersionScriptDAO.InsertVersionScript(oVersionScriptInfoList);

        }
        public static int DeleteVersionScript(VersionScriptInfo oVersionScriptInfo)
        {
            VersionScriptDAO oVersionScriptDAO = new VersionScriptDAO();
            return oVersionScriptDAO.DeleteVersionScript(oVersionScriptInfo);
        }
        public static string GetFullScriptPath(string ScriptPath)
        {
            string baseFolderPath = @"";
            string FullPath = @"";
            //Read base folder name and physical path
            baseFolderPath = ConfigurationManager.AppSettings["BaseFolderForScriptFiles"];
            FullPath = baseFolderPath + ScriptPath;
            return FullPath;

        }
        public static int GetDBCommandTimeOut()
        {
            //ConfigurationManager.AppSettings["ClientsFilePath"];


            int DBCommandTimeOut = 300;// in sec
            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["DBCommandTimeOut"]))
                DBCommandTimeOut = Convert.ToInt32(ConfigurationManager.AppSettings["DBCommandTimeOut"]);
            return DBCommandTimeOut;
        }
        public static List<VersionTypeMstInfo> GetAllVersionTypeList()
        {
            VersionMstDAO oVersionMstDAO = new VersionMstDAO();
            return oVersionMstDAO.GetAllVersionTypeList();
        }

        public static CurrentDBVersion GetCurrentDBVersion(ServerCompanyInfo oServerCompanyInfo)
        {
            CurrentDBVersion oCurrentDBVersion = null;
            ServerCompanyDAO oServerCompanyDAO = new ServerCompanyDAO(DbHelper.GetConnectionString(oServerCompanyInfo));
            oCurrentDBVersion = oServerCompanyDAO.GetCurrentDBVersion();
            return oCurrentDBVersion;
        }

        public static List<VersionScriptInfo> GetVersionScriptList(int? LowerVersionID, int? HigherVersionID)
        {
            VersionScriptDAO oVersionScriptDAO = new VersionScriptDAO();
            return oVersionScriptDAO.GetVersionScriptList(LowerVersionID, HigherVersionID);
        }
        #endregion


    }
}
