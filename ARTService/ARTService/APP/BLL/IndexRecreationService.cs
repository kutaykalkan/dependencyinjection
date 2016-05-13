using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Service.Model;
using SkyStem.ART.Service.Utility;
using System.Data;
using SkyStem.ART.Service.Data;
using SkyStem.ART.Shared.Data;
using SkyStem.Language.LanguageClient.Model;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Shared.Utility;
using SkyStem.Language.LanguageUtility.Classes;
using SkyStem.ART.Client.Model.CompanyDatabase;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Data;

namespace SkyStem.ART.Service.APP.BLL
{
    public class IndexReCreationService
    {
        public void ReCreateIndexs()
        {
            IUtility oUtility = RemotingHelper.GetUtilityObject();
            IReconciliationPeriod oReconciliationPeriod = RemotingHelper.GetReconciliationPeriodObject();
            List<ServerCompanyInfo> oServerCompanyInfoList = oUtility.GetServerCompanyListForServiceProcessing();
            foreach (ServerCompanyInfo oServerCompanyInfo in oServerCompanyInfoList)
            {
                try
                {
                    Helper.LogInfo("Re-Create Indexes for CompanyID: " + oServerCompanyInfo.CompanyID.ToString() + " Database : " + oServerCompanyInfo.DatabaseName, null);
                    AppUserInfo oAppUserInfo = new AppUserInfo();
                    oAppUserInfo.CompanyID = oServerCompanyInfo.CompanyID;
                    oUtility.ReCreateIndexes(oServerCompanyInfo.CompanyID, oAppUserInfo);
                    Helper.LogInfo("Re-Creation Indexes successful for CompanyID: " + oServerCompanyInfo.CompanyID.ToString() + " Database : " + oServerCompanyInfo.DatabaseName, null);
                }
                catch (Exception ex)
                {
                    Helper.LogError("Unable to re-create indexes for CompanyID: " + oServerCompanyInfo.CompanyID.ToString() + " Database : " + oServerCompanyInfo.DatabaseName + " Error: " + ex.ToString(), null);
                }
            }
        }
    }
}
