
using System;
using Adapdev.Text;
using System.Runtime.Serialization;
using SkyStem.ART.Client.Model.Base;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{

    /// <summary>
    /// An object representation of the SkyStemART TaskSubListHdr table
    /// </summary>
    [Serializable]
    [DataContract]
    public class TaskSubListHdrInfo
    {

        [DataMember]
        [XmlElement(ElementName = "TaskSubListID")]
        public int? TaskSubListID { get; set; }

        [DataMember]
        [XmlElement(ElementName = "TaskSubListName")]
        public string TaskSubListName { get; set; }

        [DataMember]
        [XmlElement(ElementName = "RecPeriodID")]
        public int? RecPeriodID { get; set; }

        [DataMember]
        [XmlElement(ElementName = "IsActive")]
        public bool? IsActive { get; set; }

        [DataMember]
        [XmlElement(ElementName = "AddedBy")]
        public string AddedBy { get; set; }

    }
}
