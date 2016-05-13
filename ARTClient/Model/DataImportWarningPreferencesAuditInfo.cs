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
    public class DataImportWarningPreferencesAuditInfo
    {
        [XmlElement(ElementName = "DataImportWarningPreferencesAuditId")]
        public Int32 DataImportWarningPreferencesAuditId { get; set; }

        [XmlElement(ElementName = "DataImportWarningPreferencesId")]
        public Int32 DataImportWarningPreferencesId { get; set; }

        [XmlElement(ElementName = "IsEnabled")]
        public bool IsEnabled { get; set; }

        [XmlElement(ElementName = "ChangeDate")]
        public DateTime? ChangeDate { get; set; }

        [XmlElement(ElementName = "UserID")]
        public Int32 UserID { get; set; }

        [XmlElement(ElementName = "RoleID")]
        public Int16 RoleID { get; set; }

        [XmlElement(ElementName = "DataImportMessageLabelID")]
        public Int32 DataImportMessageLabelID { get; set; }

        [XmlElement(ElementName = "FirstName")]
        public String FirstName { get; set; }

        [XmlElement(ElementName = "DataImportMessageLabel")]
        public String DataImportMessageLabel { get; set; }

        [XmlElement(ElementName = "DataImportTypeLabelID")]
        public Int32 DataImportTypeLabelID { get; set; }

        [XmlElement(ElementName = "DataImportTypeLabel")]
        public String DataImportTypeLabel { get; set; }

        [XmlElement(ElementName = "LastName")]
        public String LastName { get; set; }

        [XmlElement(ElementName = "RoleLabelID")]
        public int RoleLabelID { get; set; }

        [XmlElement(ElementName = "RoleName")]
        public String RoleName { get; set; }

    }
}
