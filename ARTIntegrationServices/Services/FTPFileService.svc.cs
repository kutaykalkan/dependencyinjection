using SkyStem.ART.Integration.Client.IServices;
using SkyStem.ART.Integration.Services.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SkyStem.ART.Integration.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "FtpFileService" in code, svc and config file together.
    public class FTPFileService : IFTPFileService
    {
        public bool CreateFolder(string loginID, string folderName)
        {
            try
            {
                FTPHelper.CreateFolder(loginID, folderName);
                return true;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public bool RemoveFolder(string loginID, string folderName)
        {
            try
            {
                FTPHelper.RemoveFolder(loginID, folderName);
                return true;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public bool RemoveUserFolders(string loginID)
        {
            try
            {
                FTPHelper.RemoveUserFolders(loginID);
                return true;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        public string GetFirstFile(string loginID, string folderName, string extFilter)
        {
            try
            {
                return FTPHelper.GetFirstFile(loginID, folderName, extFilter);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public Stream GetFileStream(string loginID, string folderName, string fileName)
        {
            try
            {
                return FTPHelper.GetFileStream(loginID, folderName, fileName);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public bool DeleteFile(string loginID, string folderName, string fileName)
        {
            try
            {
                FTPHelper.DeleteFile(loginID, folderName, fileName);
                return true;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
    }
}