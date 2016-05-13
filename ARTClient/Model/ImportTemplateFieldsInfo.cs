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
    public class ImportTemplateFieldsInfo
    {
        private System.Int32 _ImportTemplateFieldID;
        private System.Int32 _ImportTemplateID;
        private System.String _FieldName;
        private System.Boolean _IsActive;
        private System.DateTime _DateAdded;
        private System.String _AddedBy;
        private System.DateTime _DateRevised;
        private System.String _RevisedBy;

        [XmlElement(ElementName = "ImportTemplateFieldID")]
        public System.Int32 ImportTemplateFieldID
        {
            get { return _ImportTemplateFieldID; }
            set { _ImportTemplateFieldID = value; }
        }

        [XmlElement(ElementName = "ImportTemplateID")]
        public System.Int32 ImportTemplateID
        {
            get { return _ImportTemplateID; }
            set { _ImportTemplateID = value; }
        }

        [XmlElement(ElementName = "FieldName")]
        public System.String FieldName
        {
            get { return _FieldName; }
            set { _FieldName = value; }
        }

        [XmlElement(ElementName = "IsActive")]
        public System.Boolean IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

        [XmlElement(ElementName = "DateAdded")]
        public System.DateTime DateAdded
        {
            get { return _DateAdded; }
            set { _DateAdded = value; }
        }

        [XmlElement(ElementName = "AddedBy")]
        public System.String AddedBy
        {
            get { return _AddedBy; }
            set { _AddedBy = value; }
        }

        [XmlElement(ElementName = "DateRevised")]
        public System.DateTime DateRevised
        {
            get { return _DateRevised; }
            set { _DateRevised = value; }
        }

        [XmlElement(ElementName = "RevisedBy")]
        public System.String RevisedBy
        {
            get { return _RevisedBy; }
            set { _RevisedBy = value; }
        }
    }
}
