using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{
    [Serializable]
    [DataContract]
    public class RCCValidationTypeMstInfo
    {
        [XmlElement(ElementName = "RCCValidationTypeID")]
        public System.Int16? RCCValidationTypeID { get; set; }
        [XmlElement(ElementName = "RCCValidationTypeName")]
        public string RCCValidationTypeName { get; set; }
        [XmlElement(ElementName = "RCCValidationTypeNameLabelID")]
        public int? RCCValidationTypeNameLabelID { get; set; }
        [XmlElement(ElementName = "IsActive")]
        public Boolean? IsActive { get; set; }
    }
}
