using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{
    [Serializable]
    [DataContract]
    public class CompanyAttributeConfigInfo:CompanyAttributeConfigInfoBase
    {
        [DataMember]
        public int RowNumber { get; set; }

        [DataMember]
        public long? AttributeSetValueID { get; set; }

        [DataMember]
        public long? AttributeSetID { get; set; }

        [DataMember]
        public int? AttributeID { get; set; }

        [DataMember]
        public int? ParentAttributeID { get; set; }

        [DataMember]
        public int? ReferenceID { get; set; }

        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public int? ValueLabelID { get; set; }

    }
}
