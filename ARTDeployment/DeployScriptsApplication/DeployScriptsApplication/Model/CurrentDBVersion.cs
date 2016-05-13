
using System;
using Adapdev.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using SkyStem.ART.Client.Model.CompanyDatabase.Base;
using System.Xml;

namespace SkyStem.ART.Client.Model
{
    /// <summary>
    /// An object representation of the SkyStemARTCore VersionMst table
    /// </summary>
    [Serializable]
    [DataContract]
    public class CurrentDBVersion
    {
        [DataMember]
        [XmlElement(ElementName = "ServerCompanyID")]
        public int? ServerCompanyID { get; set; }
        [DataMember]
        [XmlElement(ElementName = "CompanyID")]
        public int? CompanyID { get; set; }
        [DataMember]
        [XmlElement(ElementName = "CurrentDBVersionID")]
        public int? CurrentDBVersionID { get; set; }
        [DataMember]
        [XmlElement(ElementName = "CurrentDBVersion")]
        public string CurrentDBVersionNumber { get; set; }
        [DataMember]
        [XmlElement(ElementName = "DBVersionDate")]
        public DateTime? DBVersionDate { get; set; }
    }

}
