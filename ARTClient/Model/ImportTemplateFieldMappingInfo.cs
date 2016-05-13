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
    public class ImportTemplateFieldMappingInfo
    {
        private System.Int32? _ImportTemplateFieldMappingID;
        private System.Int32? _ImportTemplateFieldID;
        private System.Int32? _ImportFieldID;
        private System.Boolean? _IsActive;
        private System.DateTime? _DateAdded;
        private System.String _AddedBy;
        private System.DateTime? _DateRevised;
        private System.String _RevisedBy;
        private System.Int32? _ImportTemplateID;


        [XmlElement(ElementName = "ImportTemplateFieldMappingID")]
        public System.Int32? ImportTemplateFieldMappingID
        {
            get
            {
                return this._ImportTemplateFieldMappingID;
            }
            set
            {
                this._ImportTemplateFieldMappingID = value;
            }
        }

        [XmlElement(ElementName = "ImportTemplateFieldID")]
        public System.Int32? ImportTemplateFieldID
        {
            get
            {
                return this._ImportTemplateFieldID;
            }
            set
            {
                this._ImportTemplateFieldID = value;
            }
        }

        [XmlElement(ElementName = "ImportFieldID")]
        public System.Int32? ImportFieldID
        {
            get
            {
                return this._ImportFieldID;
            }
            set
            {
                this._ImportFieldID = value;
            }
        }

        [XmlElement(ElementName = "IsActive")]
        public System.Boolean? IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

        [XmlElement(ElementName = "DateAdded")]
        public System.DateTime? DateAdded
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
        public System.DateTime? DateRevised
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
        [XmlElement(ElementName = "ImportTemplateField")]
        [DataMember]
        public string ImportTemplateField { get; set; }
        [XmlElement(ElementName = "ImportField")]
        [DataMember]
        public string ImportField { get; set; }

        [XmlElement(ElementName = "ImportTemplateID")]
        public System.Int32? ImportTemplateID
        {
            get
            {
                return this._ImportTemplateID;
            }
            set
            {
                this._ImportTemplateID = value;
            }
        }

        [DataMember]
        [XmlElement(ElementName = "ImportFieldLabelID")]
        public System.Int32? ImportFieldLabelID { get; set; }
        [DataMember]
        [XmlElement(ElementName = "MessageLabelID")]
        public int? MessageLabelID { get; set; }
    }
}
