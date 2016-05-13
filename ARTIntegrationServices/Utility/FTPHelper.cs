using SkyStem.ART.ARTSecurity.UserManagement;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
using System.Web;
using SkyStem.ART.Integration.Services.Data;

namespace SkyStem.ART.Integration.Services.Utility
{
    public class FTPHelper
    {
        private FTPHelper()
        {

        }

        #region User Helper Functions

        public static bool IsUserExists(string loginID)
        {
            UserPrincipal oUserPrincipal = UserManager.GetUser(loginID);
            if (oUserPrincipal != null)
                return true;
            return false;
        }

        public static void CreateUser(string loginID, string password, bool isEnabled)
        {
            string groupName = AppSettingHelper.GetAppSettingValue(AppSettingConstants.APP_KEY_FTP_USER_GROUP_NAME);
            UserPrincipal oUserPrincipal = UserManager.FindOrCreateUser(loginID, password);
            UserManager.UpdateUserSettings(oUserPrincipal, isEnabled, groupName, GetBaseFolderForFTPUser(loginID), true);
        }

        public static void RemoveUser(string loginID)
        {
            UserManager.RemoveUser(loginID);
        }

        public static bool EnableDisableUser(string loginID, bool isEnabled)
        {
            UserManager.EnableDisableUser(loginID, isEnabled);
            return true;
        }

        public static bool ChangePassword(string loginID, string oldPassword, string newPassword)
        {
            UserManager.ChangePassword(loginID, oldPassword, newPassword);
            return true;
        }

        public static bool ResetPassword(string loginID, string password)
        {
            UserManager.SetPassword(loginID, password);
            return true;
        }
        #endregion

        #region File Helper Functions
        private static string GetBaseFolderForFTPUser(string loginID, bool bCreate = true)
        {
            string homeDir = AppSettingHelper.GetAppSettingValue(AppSettingConstants.APP_KEY_FTP_USER_HOME_DIRECTORY_BASE_PATH);
            if (!Directory.Exists(homeDir) && bCreate)
            {
                Directory.CreateDirectory(homeDir);
            }
            homeDir += Path.DirectorySeparatorChar + loginID;
            if (!Directory.Exists(homeDir) && bCreate)
            {
                Directory.CreateDirectory(homeDir);
            }
            return homeDir;
        }

        public static bool CreateFolder(string loginID, string folderName)
        {
            string folderPath = GetBaseFolderForFTPUser(loginID);
            folderPath += Path.DirectorySeparatorChar + folderName;
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            return true;
        }

        public static bool RemoveFolder(string loginID, string folderName)
        {
            string folderPath = GetBaseFolderForFTPUser(loginID, false);
            folderPath += Path.DirectorySeparatorChar + folderName;
            if (!Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true);
            }
            return true;
        }

        public static bool RemoveUserFolders(string loginID)
        {
            string folderPath = GetBaseFolderForFTPUser(loginID, false);
            if (!Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true);
            }
            return true;
        }

        public static string GetFirstFile(string loginID, string folderName, string extFilter)
        {
            string folderPath = GetBaseFolderForFTPUser(loginID);
            folderPath += Path.DirectorySeparatorChar + folderName;
            if (string.IsNullOrEmpty(extFilter))
                extFilter = "*.*";
            string[] ext = extFilter.Split('|');
            if (Directory.Exists(folderPath))
            {
                IEnumerable<string> ienumFiles = Directory.EnumerateFiles(folderPath);
                foreach (string fileName in ienumFiles)
                {
                    FileInfo oFileInfo = new FileInfo(fileName);
                    if (Array.Exists(ext, T => T.ToLower() == "*" + oFileInfo.Extension.ToLower()))
                        return oFileInfo.Name;
                }
            }
            return null;
        }
        public static Stream GetFileStream(string loginID, string folderName, string fileName)
        {
            string filePath = GetBaseFolderForFTPUser(loginID);
            filePath += Path.DirectorySeparatorChar + folderName;
            if (Directory.Exists(filePath) && !string.IsNullOrEmpty(fileName))
            {
                filePath += Path.DirectorySeparatorChar + fileName;
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                return fs;
            }
            return null;
        }

        public static bool DeleteFile(string loginID, string folderName, string fileName)
        {
            string filePath = GetBaseFolderForFTPUser(loginID);
            filePath += Path.DirectorySeparatorChar + folderName;
            if (Directory.Exists(filePath) && !string.IsNullOrEmpty(fileName))
            {
                filePath += Path.DirectorySeparatorChar + fileName;
                File.Delete(filePath);
            }
            return false;
        }
        #endregion
    }
}