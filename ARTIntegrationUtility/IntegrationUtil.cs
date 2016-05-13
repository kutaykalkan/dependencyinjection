using ART.Integration.Utility.ARTIntegrationServices.FTPFileService;
using ART.Integration.Utility.ARTIntegrationServices.FTPUserService;
using ART.Integration.Utility.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ART.Integration.Utility
{
    public class IntegrationUtil
    {
        private IntegrationUtil()
        {

        }

        #region User Management functions
        public static bool IsUserExists(string loginID) 
        {
            IFTPUserService oIFTPUserService = RemotingHelper.GetFTPUserServiceClientObject();
            return oIFTPUserService.IsUserExists(loginID);
        }

        public static bool EnableDisableUser(string loginID, bool isEnabled)
        {
            IFTPUserService oIFTPUserService = RemotingHelper.GetFTPUserServiceClientObject();
            return oIFTPUserService.EnableDisableUser(loginID, isEnabled);
        }

        public static bool CreateUser(string loginID, string password, bool isEnabled)
        {
            IFTPUserService oIFTPUserService = RemotingHelper.GetFTPUserServiceClientObject();
            return oIFTPUserService.CreateUser(loginID, password, isEnabled);
        }

        public static void RemoveUser(string loginID)
        {
            IFTPUserService oIFTPUserService = RemotingHelper.GetFTPUserServiceClientObject();
            oIFTPUserService.RemoveUser(loginID);
        }

        public static bool ChangePassword(string loginID, string oldPassword, string newPassword)
        {
            IFTPUserService oIFTPUserService = RemotingHelper.GetFTPUserServiceClientObject();
            return oIFTPUserService.ChangePassword(loginID, oldPassword, newPassword);
        }

        public static bool ResetPassword(string loginID, string password)
        {
            IFTPUserService oIFTPUserService = RemotingHelper.GetFTPUserServiceClientObject();
            return oIFTPUserService.ResetPassword(loginID, password);
        }

        #endregion

        #region File Management Functions

        public static bool CreateFolder(string loginID, string folderName)
        {
            IFTPFileService oIFTPFileService = RemotingHelper.GetFTPFileServiceClientObject();
            return oIFTPFileService.CreateFolder(loginID, folderName);
        }

        public static bool RemoveFolder(string loginID, string folderName)
        {
            IFTPFileService oIFTPFileService = RemotingHelper.GetFTPFileServiceClientObject();
            return oIFTPFileService.RemoveFolder(loginID, folderName);
        }

        public static bool RemoveUserFolders(string loginID)
        {
            IFTPFileService oIFTPFileService = RemotingHelper.GetFTPFileServiceClientObject();
            return oIFTPFileService.RemoveUserFolders(loginID);
        }

        public static string GetFirstFile(string loginID, string folderName, string extFilter)
        {
            IFTPFileService oIFTPFileService = RemotingHelper.GetFTPFileServiceClientObject();
            return oIFTPFileService.GetFirstFile(loginID, folderName, extFilter);
        }

        public static Stream GetFileStream(string loginID, string folderName, string fileName)
        {
            IFTPFileService oIFTPFileService = RemotingHelper.GetFTPFileServiceClientObject();
            return oIFTPFileService.GetFileStream(loginID, folderName, fileName);
        }

        public static bool DeleteFile(string loginID, string folderName, string fileName)
        {
            IFTPFileService oIFTPFileService = RemotingHelper.GetFTPFileServiceClientObject();
            return oIFTPFileService.DeleteFile(loginID, folderName, fileName);
        }

#endregion

    }
}
