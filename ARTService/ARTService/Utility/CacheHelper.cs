using System.Collections.Generic;
using SkyStem.ART.Client.Model.CompanyDatabase;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Service.DAO;

namespace SkyStem.ART.Service.Utility
{
    public class CacheHelper
    {
        public static List<CompanyUserInfo> CompanyUserInfoList;

        public static List<CompanyUserInfo> GetCompanyList()
        {
            if (CompanyUserInfoList == null)
            {
                IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
                CompanyUserInfoList = oUtilityClient.GetAllCompanyConnectionInfo();
            }
            return CompanyUserInfoList;
        }

        public static void ClearCompanyList()
        {
            CompanyUserInfoList = null;
        }

        public static Dictionary<string, CompanyUserInfo> GetDistinctDatabaseList()
        {
            Dictionary<string, CompanyUserInfo> oDictConnectionString = new Dictionary<string, CompanyUserInfo>();
            List<CompanyUserInfo> companyList = CacheHelper.GetCompanyList();
            foreach (CompanyUserInfo oCompanyUserInfo in companyList)
            {
                DataAccess oDA = new DataAccess(oCompanyUserInfo);
                string connectionString = oDA.GetConnectionString();
                if (!oDictConnectionString.ContainsKey(connectionString))
                    oDictConnectionString.Add(connectionString, oCompanyUserInfo);
            }
            return oDictConnectionString;
        }
    }
}
