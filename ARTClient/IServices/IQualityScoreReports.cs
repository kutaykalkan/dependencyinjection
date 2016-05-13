using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Client.Model;
using System.ServiceModel;

namespace SkyStem.ART.Client.IServices
{
    [ServiceContract]
    public interface IQualityScoreReports
    {
        [OperationContract]
        IList<RangeOfScoreMstInfo> GetRangeOfScore( AppUserInfo oAppUserInfo);
        
        [OperationContract]
        IList<QualityScoreChecklistInfo> GetQualityScoreChecklist(int RecPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<QualityScoreChecklistInfo> GetQualityScoreChecklistByQualityScoreIDs(List<int> qualityScoreIDs, AppUserInfo oAppUserInfo);
    }
}
