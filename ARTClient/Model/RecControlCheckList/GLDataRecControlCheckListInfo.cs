using System;
using Adapdev.Text;
using System.Runtime.Serialization;
using SkyStem.ART.Client.Model.Base;
using SkyStem.ART.Client.Model.RecControlCheckList.Base;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model.RecControlCheckList
{
    /// <summary>
    /// An object representation of the SkyStemART GLDataRecControlCheckList table
    /// </summary>
    [Serializable]
    [DataContract]
    public class GLDataRecControlCheckListInfo : GLDataRecControlCheckListInfoBase
    {
        [DataMember]
        [XmlElement(ElementName = "IsCommentAvailable")]
        public bool? IsCommentAvailable { get; set; }

        [DataMember]
        [XmlElement(ElementName = "TotalCount")]
        public int? TotalCount { get; set; }

        [DataMember]
        [XmlElement(ElementName = "CompletedCount")]
        public int? CompletedCount { get; set; }

        [DataMember]
        [XmlElement(ElementName = "ReviewedCount")]
        public int? ReviewedCount { get; set; }
    }
}
