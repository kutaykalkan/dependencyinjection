using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Client.Model.QualityScore;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Params.QualityScore
{
    [DataContract]
    public class QualityScoreParamInfo: ParamInfoBase
    {
        [DataMember]
        public List<CompanyQualityScoreInfo> CompanyQualityScoreInfoList { get; set; }
        
        [DataMember]
        public List<GLDataQualityScoreInfo> GLDataQualityScoreInfoList { get; set; }

        [DataMember]
        public long? GLDataID { get; set; }

        [DataMember]
        public bool? EnabledOnly { get; set; }
    }
}
