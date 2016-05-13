using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Service.Utility;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Shared.Data;
using SkyStem.ART.Shared.Utility;

namespace SkyStem.ART.Service.APP.BLL
{
    public class CompanyCreation
    {
        private string BaseDBPath = SharedAppSettingHelper.GetAppSettingValue(SharedAppSettingConstants.SKYSTEMART_BASE_DATABASE_PATH);
        public void ProcessCompanyCreation()
        {
            ICompany oCompany = RemotingHelper.GetCompanyObject();
            IUser oUser = RemotingHelper.GetUserObject();
            AppUserInfo oAppUserInfo = new AppUserInfo();
            List<CompanyHdrInfo> oCompanyHdrInfoList = oCompany.SelectAllCompaniesForDatabaseCreation(oAppUserInfo);
            if (oCompanyHdrInfoList != null && oCompanyHdrInfoList.Count > 0)
            {
                foreach (CompanyHdrInfo oCompanyHdrInfo in oCompanyHdrInfoList)
                {
                    string msgSuffix = oCompanyHdrInfo.DisplayName + " CompanyID: " + oCompanyHdrInfo.CompanyID.ToString();
                    try
                    {
                        Helper.LogInfo("Creating Database for " + msgSuffix, null);
                        oAppUserInfo.CompanyID = oCompanyHdrInfo.CompanyID;
                        ContactInfo oContactInfo = oCompany.GetContactInfoFromCore(oCompanyHdrInfo.CompanyID, oAppUserInfo);
                        List<UserHdrInfo> oUserHdrInfoList = oUser.SelectUsersFromTransit(oCompanyHdrInfo.CompanyID, oAppUserInfo);
                        if (oUserHdrInfoList != null && oUserHdrInfoList.Count > 0)
                        {
                            oCompany.CreateDatabaseAndCompany(oCompanyHdrInfo, oContactInfo, oUserHdrInfoList, 1033, BaseDBPath, oAppUserInfo);
                            Helper.LogInfo("Database Creation Successful for " + msgSuffix, null);
                            Helper.LogInfo("Sending Mails for " + msgSuffix, null);
                            foreach (UserHdrInfo oUserHdrInfo in oUserHdrInfoList)
                            {
                                oUserHdrInfo.Password = oUserHdrInfo.GeneratedPassword;
                                oUserHdrInfo.CompanyDisplayName = oCompanyHdrInfo.DisplayName;
                                SkyStem.ART.Service.Utility.MailHelper.SendMailToNewUser(oUserHdrInfo, null);
                            }
                            Helper.LogInfo("Sending Mails Successful for " + msgSuffix, null);
                        }
                        else
                            Helper.LogInfo("No User found for " + oCompanyHdrInfo.DisplayName, null);
                    }
                    catch(Exception ex)
                    {
                        Helper.LogError("Unable to create database for " + msgSuffix + " Error: " + ex.ToString(), null);
                    }
                }
                CacheHelper.ClearCompanyList();
            }
        }
    }
}
