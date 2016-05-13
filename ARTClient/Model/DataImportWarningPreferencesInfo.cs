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
    public class DataImportWarningPreferencesInfo
    {
        [XmlElement(ElementName = "DataImportWarningPreferencesID")]
        public Int32 DataImportWarningPreferencesID { get; set; }
        [XmlElement(ElementName = "UserID")]
        public Int32 UserID { get; set; }
        [XmlElement(ElementName = "RoleID")]
        public Int16 RoleID { get; set; }
        [XmlElement(ElementName = "DataImportMessageID")]
        public Int16 DataImportMessageID { get; set; }
        [XmlElement(ElementName = "IsEnabled")]
        public bool? IsEnabled { get; set; }
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
        [XmlElement(ElementName = "DataImportTypeID")]
        public Int16 DataImportTypeID { get; set; }

        [XmlElement(ElementName = "CompanyID")]
        public Int32 CompanyID { get; set; }
    }
}
