using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.Client.Params
{
    [DataContract]
    public class CompanyConfigurationParamInfo : ParamInfoBase
    {
        [DataMember]
        public List<CompanySystemReconciliationRuleInfo> CompanySystemReconciliationRuleInfoList { get; set; }

        [DataMember]
        public SystemLockdownInfo SystemLockdownInfo { get; set; }
    }
}
