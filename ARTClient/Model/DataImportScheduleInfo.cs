using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{
    [Serializable]
    [DataContract]
    public class DataImportScheduleInfo
    {
        [XmlElement(ElementName = "DataImportScheduleID")]
        [DataMember]
        public Int32 DataImportScheduleID { get; set; }

        [XmlElement(ElementName = "UserID")]
        [DataMember]
        public Int32 UserID { get; set; }

        [XmlElement(ElementName = "DataImportTypeID")]
        [DataMember]
        public short DataImportTypeID { get; set; }

        [XmlElement(ElementName = "RoleID")]
        [DataMember]
        public short RoleID { get; set; }

        [XmlElement(ElementName = "Description")]
        [DataMember]
        public string Description { get; set; }

        [XmlElement(ElementName = "Hours")]
        [DataMember]
        public short? Hours { get; set; }

        [XmlElement(ElementName = "Minutes")]
        [DataMember]
        public short? Minutes { get; set; }

        [XmlElement(ElementName = "HoursDefaultValue")]
        [DataMember]
        public short? HoursDefaultValue { get; set; }

        [XmlElement(ElementName = "MinutesDefaultValue")]
        [DataMember]
        public short? MinutesDefaultValue { get; set; }

        [XmlElement(ElementName = "IsActive")]
        [DataMember]
        public bool IsActive { get; set; }

        [XmlElement(ElementName = "DateAdded")]
        [DataMember]
        public DateTime DateAdded { get; set; }

        [XmlElement(ElementName = "AddedBy")]
        [DataMember]
        public String AddedBy { get; set; }

        [XmlElement(ElementName = "DateRevised")]
        [DataMember]
        public DateTime? DateRevised { get; set; }

        [XmlElement(ElementName = "RevisedBy")]
        [DataMember]
        public string RevisedBy { get; set; }
    }
}
