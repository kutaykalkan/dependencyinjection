
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{

    /// <summary>
    /// An object representation of the SkyStemART DataTypeMst table
    /// </summary>
    [Serializable]
    [DataContract]
    public class DataTypeMstInfo : DataTypeMstInfoBase
    {
        [XmlElement(ElementName = "DataTypeNameLabelID")]
        public int? DataTypeNameLabelID { get; set; }
        [XmlElement(ElementName = "DataTypeName")]
        public string DataTypeName { get; set; }
    }
}
