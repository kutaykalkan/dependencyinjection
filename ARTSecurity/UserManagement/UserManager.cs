using SkyStem.ART.ARTSecurity.Data;
using SkyStem.ART.ARTSecurity.Utility;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace SkyStem.ART.ARTSecurity.UserManagement
{
    public class UserManager
    {
        private UserManager()
        {

        }

        public static void UpdateUserSettings(UserPrincipal oUserPrincipal, bool isEnabled, string groupName, string homeDir, bool passwordNeverExpires)
        {
            oUserPrincipal.HomeDirectory = homeDir;
            oUserPrincipal.Enabled = isEnabled;
            oUserPrincipal.PasswordNeverExpires = passwordNeverExpires;
            AddUserToGroup(oUserPrincipal, groupName);
            oUserPrincipal.Save();
        }

        public static void ChangePassword(string userName, string oldPassword, string newPassword)
        {
            UserPrincipal oUserPrincipal = GetUser(userName);
            if (oUserPrincipal != null)
            {
                PrincipalContext oPrincipalContext = GetPrincipalContext();
                if (oPrincipalContext.ValidateCredentials(userName, oldPassword))
                {
                    oUserPrincipal.SetPassword(newPassword);
                    oUserPrincipal.Save();
                }
                else
                {
                    throw new Exception("Invalid Old Password");
                }
            }
        }

        public static void SetPassword(string userName, string password)
        {
            UserPrincipal oUserPrincipal = GetUser(userName);
            if (oUserPrincipal != null)
            {
                oUserPrincipal.SetPassword(password);
                oUserPrincipal.Save();
            }
        }

        public static void EnableDisableUser(string userName, bool bEnable)
        {
            UserPrincipal oUserPrincipal = GetUser(userName);
            if (oUserPrincipal != null)
            {
                oUserPrincipal.Enabled = bEnable;
                oUserPrincipal.Save();
            }
        }

        public static void AddUserToGroup(UserPrincipal oUserPrincipal, string groupName)
        {
            GroupPrincipal oGroupPrincipal = FindOrCreateGroup(groupName);
            if (oUserPrincipal != null && oGroupPrincipal != null && !oUserPrincipal.IsMemberOf(oGroupPrincipal))
            {
                oGroupPrincipal.Members.Add(oUserPrincipal);
                oGroupPrincipal.Save();
            }
        }

        public static void RemoveUser(string userName)
        {
            UserPrincipal oUserPrincipal = GetUser(userName);
            if (oUserPrincipal != null)
            {
                oUserPrincipal.Delete();
            }
        }

        public static UserPrincipal FindOrCreateUser(string userName, string password)
        {
            PrincipalContext oPrincipalContext = GetPrincipalContext();
            UserPrincipal oUserPrincipal = GetUser(userName);
            if (oUserPrincipal == null)
            {
                oUserPrincipal = new UserPrincipal(oPrincipalContext, userName, password, true);
                oUserPrincipal.Save();
            }
            return oUserPrincipal;
        }

        public static GroupPrincipal FindOrCreateGroup(string groupName)
        {
            PrincipalContext oPrincipalContext = GetPrincipalContext();
            GroupPrincipal oGroupPrincipal = GroupPrincipal.FindByIdentity(oPrincipalContext, groupName);
            if (oGroupPrincipal == null)
            {
                oGroupPrincipal = new GroupPrincipal(oPrincipalContext, groupName);
                oGroupPrincipal.Description = groupName;
                oGroupPrincipal.IsSecurityGroup = true;
                oGroupPrincipal.Save();
            }
            return oGroupPrincipal;
        }

        public static UserPrincipal GetUser(string userName)
        {
            PrincipalContext oPrincipalContext = GetPrincipalContext();
            if (oPrincipalContext != null)
            {
                UserPrincipal oUserPrincipal = UserPrincipal.FindByIdentity(oPrincipalContext, userName);
                return oUserPrincipal;
            }
            return null;
        }

        public static GroupPrincipal GetUserGroup(string groupName)
        {
            PrincipalContext oPrincipalContext = GetPrincipalContext();
            if (oPrincipalContext != null)
            {
                GroupPrincipal oGroupPrincipal = GroupPrincipal.FindByIdentity(oPrincipalContext, groupName);
                return oGroupPrincipal;
            }
            return null;
        }

        public static PrincipalContext GetPrincipalContext()
        {
            PrincipalContext oPrincipalContext = null;
            string domainName = AppSettingHelper.GetAppSettingValue(AppSettingConstants.APP_KEY_DOMAIN_NAME);
            string rootName = AppSettingHelper.GetAppSettingValue(AppSettingConstants.APP_KEY_ROOT_NAME);
            string machineName = AppSettingHelper.GetAppSettingValue(AppSettingConstants.APP_KEY_MACHINE_NAME);
            if (!string.IsNullOrEmpty(domainName))
                oPrincipalContext = new PrincipalContext(ContextType.Domain, domainName, rootName);
            else if (!string.IsNullOrEmpty(machineName))
                oPrincipalContext = new PrincipalContext(ContextType.Machine, machineName, null);
            return oPrincipalContext;
        }
    }
}
