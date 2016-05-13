using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Params
{
    [DataContract]
    public class ReportParamInfo : ParamInfoBase
    {
        [DataMember]
        public Int16? ReportID { get; set; }

        [DataMember]
        public bool? IsOptional { get; set; }
    }
}
