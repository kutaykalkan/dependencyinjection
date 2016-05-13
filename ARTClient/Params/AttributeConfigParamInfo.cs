using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.Client.Params
{
    [DataContract]
    public class AttributeConfigParamInfo:ParamInfoBase
    {
        [DataMember]
        public List<CompanyAttributeConfigInfo> CompanyRoleConfigInfoList { get; set; }

        [DataMember]
        public bool? EnabledOnly { get; set; }

        [DataMember]
        public System.Int32? AttributeSetTypeID { get; set; }
    }
}
