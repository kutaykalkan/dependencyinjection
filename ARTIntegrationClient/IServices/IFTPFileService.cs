using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SkyStem.ART.Integration.Client.IServices
{
    [ServiceContract]
    public interface IFTPFileService
    {
        [OperationContract]
        bool CreateFolder(string loginID, string folderName);
        
        [OperationContract]
        bool RemoveFolder(string loginID, string folderName);

        [OperationContract]
        bool RemoveUserFolders(string loginID);

        [OperationContract]
        string GetFirstFile(string loginID, string folderName, string extFilter);

        [OperationContract]
        Stream GetFileStream(string loginID, string folderName, string fileName);

        [OperationContract]
        bool DeleteFile(string loginID, string folderName, string fileName);
    }
}
