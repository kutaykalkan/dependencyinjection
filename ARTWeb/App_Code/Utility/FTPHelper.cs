using ART.Integration.Utility;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Shared.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SkyStem.ART.Web.Utility
{
    /// <summary>
    /// Summary description for FtpHelper
    /// </summary>
    public class FTPHelper
    {
        private FTPHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static bool IsFTPUserExists(string userLoginID)
        {
            try
            {
                if (!string.IsNullOrEmpty(userLoginID))
                {
                    string loginID = SharedDataImportHelper.GetFTPLoginID(userLoginID);
                    if (IntegrationUtil.IsUserExists(loginID))
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
                throw new ARTException(5000408);
            }
        }

        public static bool RemoveFTPUser(string userLoginID)
        {
            try
            {
                if (!string.IsNullOrEmpty(userLoginID))
                {
                    string loginID = SharedDataImportHelper.GetFTPLoginID(userLoginID);
                    IntegrationUtil.RemoveUser(loginID);
                    IntegrationUtil.RemoveUserFolders(userLoginID);
                }
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
                //throw new ARTException(5000408);
            }
            return false;
        }
    
        public static bool SetupFTPUser(UserHdrInfo oUserHdrInfo, bool isEnabled)
        {
            try
            {
                //Remove old FTP logins created using login id
                if(!string.IsNullOrEmpty(oUserHdrInfo.LoginID))
                {
                    RemoveFTPUser(oUserHdrInfo.LoginID);
                }
                //create ftp logins using ftp login id
                if (!string.IsNullOrEmpty(oUserHdrInfo.FTPLoginID))
                {
                    string ftpLoginID = SharedDataImportHelper.GetFTPLoginID(oUserHdrInfo.FTPLoginID);
                    if (IntegrationUtil.IsUserExists(ftpLoginID))
                        IntegrationUtil.EnableDisableUser(ftpLoginID, isEnabled);
                    else if (isEnabled && !string.IsNullOrEmpty(oUserHdrInfo.GeneratedFTPPassword))
                        IntegrationUtil.CreateUser(ftpLoginID, oUserHdrInfo.GeneratedFTPPassword, isEnabled);

                    if (isEnabled)
                    {
                        IntegrationUtil.CreateFolder(ftpLoginID, SessionHelper.GetCurrentCompanyHdrInfo().CompanyAlias + Path.DirectorySeparatorChar + FTPDataImportConstants.FTPDataImportFolderName.GL_DATA);
                        IntegrationUtil.CreateFolder(ftpLoginID, SessionHelper.GetCurrentCompanyHdrInfo().CompanyAlias + Path.DirectorySeparatorChar + FTPDataImportConstants.FTPDataImportFolderName.SUBLEDGER_DATA);
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
                throw new ARTException(5000408);
            }
        }
        public static List<UserFTPConfigurationInfo> GetUserFTPConfiguration()
        {
            List<UserFTPConfigurationInfo> oUserFTPConfigurationInfoList = new List<UserFTPConfigurationInfo>();
            IUser oUserClient = RemotingHelper.GetUserObject();
            oUserFTPConfigurationInfoList = oUserClient.GetUserFTPConfiguration(SessionHelper.CurrentUserID, Helper.GetAppUserInfo());
            return oUserFTPConfigurationInfoList;
        }
        public static FTPServerInfo GetFTPServerInfo(short? FTPServerID, int? CompanyID)
        {
            FTPServerInfo oFTPServerInfo = null;
            List<FTPServerInfo> oFTPServerInfoList = null;
            if (CompanyID.HasValue)
                oFTPServerInfoList = SessionHelper.GetAllFTPServerObject(CompanyID);
            if (FTPServerID.HasValue && oFTPServerInfoList != null && oFTPServerInfoList.Count > 0)
                oFTPServerInfo = oFTPServerInfoList.Find(obj => obj.FTPServerId == FTPServerID.Value);
            return oFTPServerInfo;
        }
        public static int SaveUserFTPConfiguration(List<UserFTPConfigurationInfo> oUserFTPConfigurationInfoList)
        {
            int result = 0;
            Helper.GetAppUserInfo();
            IUser oUserClient = RemotingHelper.GetUserObject();
            result = oUserClient.SaveUserFTPConfiguration(oUserFTPConfigurationInfoList, Helper.GetAppUserInfo());
            return result;
        }

        public static bool IsFTPActivatedCompanyLevel()
        {
            CompanyHdrInfo oCompanyHdrInfo = SessionHelper.GetCurrentCompanyHdrInfo();
            if (oCompanyHdrInfo != null)
                return oCompanyHdrInfo.IsFTPEnabled.GetValueOrDefault();
            return false;
        }

        public static bool IsFTPActivatedCompanyAndUserLevel()
        {
            if (IsFTPActivatedCompanyLevel())
            {
                UserHdrInfo oUserHdrInfo = SessionHelper.GetCurrentUser();
                if (oUserHdrInfo != null && oUserHdrInfo.FTPActivationStatusID.GetValueOrDefault() == (short)ARTEnums.ActivationStatus.Activated)
                    return true;
            }
            return false;
        }
    }
}