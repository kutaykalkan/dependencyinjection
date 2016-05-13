
using System;
using Adapdev.Text;
using System.Runtime.Serialization;
using SkyStem.ART.Client.Model.Base;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
    /// An object representation of the SkyStemART TaskCustomFieldInfo table
	/// </summary>
	[Serializable]
	[DataContract]
    public class TaskCustomFieldInfo
	{        

        [DataMember]
        [XmlElement(ElementName = "TaskCustomFieldID")]
        public short? TaskCustomFieldID { get; set; }

        [DataMember]
        [XmlElement(ElementName = "CustomField")]
        public string CustomField { get; set; }

        [DataMember]
        [XmlElement(ElementName = "CustomFieldLabelID")]
        public int? CustomFieldLabelID { get; set; }

        [DataMember]
        [XmlElement(ElementName = "CustomFieldValue")]
        public string CustomFieldValue { get; set; }

        [DataMember]
        [XmlElement(ElementName = "CustomFieldValueLabelID")]
        public int? CustomFieldValueLabelID { get; set; }

	}
}
