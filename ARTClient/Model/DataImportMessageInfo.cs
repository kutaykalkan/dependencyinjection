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
    public class DataImportMessageInfo
    {
        [XmlElement(ElementName = "DataImportMessageID")]
        public Int16 DataImportMessageID { get; set; }
        [XmlElement(ElementName = "Description")]
        public string Description { get; set; }
        [XmlElement(ElementName = "DescriptionLabelID")]
        public Int32 DescriptionLabelID { get; set; }
        [XmlElement(ElementName = "DataImportMessageLabelID")]
        public Int32? DataImportMessageLabelID { get; set; }
        [XmlElement(ElementName = "DataImportMessageTypeID")]
        public int? DataImportMessageTypeID { get; set; }
        [XmlElement(ElementName = "IsActive")]
        public bool IsActive { get; set; }
        [XmlElement(ElementName = "DateAdded")]
        public DateTime DateAdded { get; set; }
        [XmlElement(ElementName = "AddedBy")]
        public string AddedBy { get; set; }
        [XmlElement(ElementName = "DateRevised")]
        public DateTime? DateRevised { get; set; }
        [XmlElement(ElementName = "RevisedBy")]
        public string RevisedBy { get; set; }

        [XmlElement(ElementName = "DataImportMessageLabel")]
        public string DataImportMessageLabel { get; set; }
    }
}
