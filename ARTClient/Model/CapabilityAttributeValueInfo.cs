using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SkyStem.ART.Client.Model
{
    [DataContract]
    [Serializable]
    public class CapabilityAttributeValueInfo
    {
        [DataMember]
        public short? CapabilityID { get; set; }

        [DataMember]
        public int? CapabilityAttributeID { get; set; }

        [DataMember]
        public int? ParentCapabilityAttributeID { get; set; }

        [DataMember]
        public int? StartRecPeriodID { get; set; }

        [DataMember]
        public int? EndRecPeriodID { get; set; }

        [DataMember]
        public int? ReferenceID { get; set; }

        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public bool? IsCarryForwardedFromPreviousRecPeriod { get; set; }
    }
}
