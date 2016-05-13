using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{
    [Serializable]
    [DataContract]
    public class DaysTypeInfo
    {
        [XmlElement(ElementName = "DaysTypeID")]
        public short? DaysTypeID { get; set; }
        [XmlElement(ElementName = "DaysType")]
        public string DaysType { get; set; }
        [XmlElement(ElementName = "DaysTypeLabelID")]
        public int? DaysTypeLabelID { get; set; }


    }
}
