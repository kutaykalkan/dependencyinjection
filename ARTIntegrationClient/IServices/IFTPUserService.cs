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
    public interface IFTPUserService
    {

        [OperationContract]
        bool IsUserExists(string loginID);

        [OperationContract]
        bool CreateUser(string loginID, string password, bool isEnabled);

        [OperationContract]
        bool RemoveUser(string loginID);

        [OperationContract]
        bool EnableDisableUser(string loginID, bool isEnabled);

        [OperationContract]
        bool ChangePassword(string loginID, string oldPassword, string newPassword);

        [OperationContract]
        bool ResetPassword(string loginID, string password);
    }
}
