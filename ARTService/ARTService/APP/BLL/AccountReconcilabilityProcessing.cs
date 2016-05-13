using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Service.Utility;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model.CompanyDatabase;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Data;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace SkyStem.ART.Service.APP.BLL
{
    public class AccountReconcilabilityProcessing
    {
        public void ReprocessAccountReconcilability()
        {
            IUtility oUtility = RemotingHelper.GetUtilityObject();
            IReconciliationPeriod oReconciliationPeriod = RemotingHelper.GetReconciliationPeriodObject();
            List<ServerCompanyInfo> oServerCompanyInfoList = oUtility.GetServerCompanyListForServiceProcessing();
            ConcurrentQueue<Exception> exceptions = new ConcurrentQueue<Exception>();
            Parallel.ForEach<ServerCompanyInfo>(oServerCompanyInfoList, oServerCompanyInfo =>
            {
                try
                {
                    Helper.LogInfo("Reprocessing Reconcilability for CompanyID: " + oServerCompanyInfo.CompanyID.ToString() + " Database : " + oServerCompanyInfo.DatabaseName, null);
                    AppUserInfo oAppUserInfo = new AppUserInfo();
                    oAppUserInfo.CompanyID = oServerCompanyInfo.CompanyID;
                    oReconciliationPeriod.ReprocessAccountReconcilability(null, null, null, (short)ARTEnums.ActionType.CalculateReconciliability, (short)ARTEnums.ChangeSource.ProcessReconcilabilityFlag, oAppUserInfo);
                    Helper.LogInfo("Reprocessing Reconcilability successful for CompanyID: " + oServerCompanyInfo.CompanyID.ToString() + " Database : " + oServerCompanyInfo.DatabaseName, null);
                }
                catch (Exception ex)
                {
                    exceptions.Enqueue(ex);
                }
            });
            while (exceptions.Count > 0)
            {
                Exception exOut;
                if (exceptions.TryDequeue(out exOut))
                    Helper.LogError(exOut, null);
            }
        }
    }
}
