using SkyStem.ART.Integration.Client.IServices;
using SkyStem.ART.Integration.Services.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SkyStem.ART.Integration.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "FTPUserService" in code, svc and config file together.
    public class FTPUserService : IFTPUserService
    {

        public bool IsUserExists(string loginID)
        {
            try
            {
                return FTPHelper.IsUserExists(loginID);
            }
            catch (Exception ex) 
            {
                throw new FaultException(ex.Message);
            }
        }

        public bool CreateUser(string loginID, string password, bool isEnabled)
        {
            try
            {
                FTPHelper.CreateUser(loginID, password, isEnabled);
                return true;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public bool RemoveUser(string loginID)
        {
            try
            {
                FTPHelper.RemoveUser(loginID);
                return true;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public bool EnableDisableUser(string loginID, bool isEnabled)
        {
            try
            {
                FTPHelper.EnableDisableUser(loginID, isEnabled);
                return true;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public bool ChangePassword(string loginID, string oldPassword, string newPassword)
        {
            try
            {
                FTPHelper.ChangePassword(loginID, oldPassword, newPassword);
                return true;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public bool ResetPassword(string loginID, string password)
        {
            try
            {
                FTPHelper.ResetPassword(loginID, password);
                return true;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
    }
}