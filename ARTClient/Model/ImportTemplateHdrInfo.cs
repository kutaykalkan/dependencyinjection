
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace SkyStem.ART.Client.Model
{

    /// <summary>
    /// An object representation of the SkyStemArt DataImportHdr table
    /// </summary>
    [Serializable]
    [DataContract]
    public class ImportTemplateHdrInfo
    {
        protected System.Int32? _ImportTemplateID = null;
        protected System.String _TemplateName = "";
        protected List<ImportTemplateFieldsInfo> _ImportTemplateFieldsInfoList;


        [XmlElement(ElementName = "ImportTemplateID")]
        [DataMember]
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

        [XmlElement(ElementName = "TemplateName")]
        [DataMember]
        public System.String TemplateName
        {
            get
            {
                return this._TemplateName;
            }
            set
            {
                this._TemplateName = value;
            }
        }
        [XmlElement(ElementName = "DataImportTypeID")]
        [DataMember]
        public short? DataImportTypeID { get; set; }
        [XmlElement(ElementName = "DataImportType")]
        [DataMember]
        public string DataImportType { get; set; }
        [XmlElement(ElementName = "DataImportTypeLabelID")]
        [DataMember]
        public int? DataImportTypeLabelID { get; set; }
        [XmlElement(ElementName = "LanguageID")]
        [DataMember]
        public int? LanguageID { get; set; }
        [XmlElement(ElementName = "Language")]
        [DataMember]
        public string Language { get; set; }
        [XmlElement(ElementName = "SheetName")]
        [DataMember]
        public string SheetName { get; set; }
        [XmlElement(ElementName = "TemplateFileName")]
        [DataMember]
        public string TemplateFileName { get; set; }
        [XmlElement(ElementName = "AddedBy")]
        [DataMember]
        public string AddedBy { get; set; }
        [XmlElement(ElementName = "DateAdded")]
        [DataMember]
        public DateTime? DateAdded { get; set; }

        [XmlElement(ElementName = "LanguageName")]
        [DataMember]
        public string LanguageName { get; set; }

        [XmlElement(ElementName = "PhysicalPath")]
        [DataMember]
        public string PhysicalPath { get; set; }

        [XmlElement(ElementName = "RevisedBy")]
        [DataMember]
        public string RevisedBy { get; set; }

        [XmlElement(ElementName = "DateRevised")]
        [DataMember]
        public DateTime? DateRevised { get; set; }

        [DataMember]
        [XmlElement(ElementName = "ImportTemplateFieldsInfoList")]
        public List<ImportTemplateFieldsInfo> ImportTemplateFieldsInfoList
        {
            get { return _ImportTemplateFieldsInfoList; }
            set { _ImportTemplateFieldsInfoList = value; }
        }

        [XmlElement(ElementName = "CompanyID")]
        [DataMember]
        public int? CompanyID { get; set; }

        [XmlElement(ElementName = "DataImportTemplateID")]
        [DataMember]
        public int? DataImportTemplateID { get; set; }

        [XmlElement(ElementName = "NumberOfMappedColumns")]
        [DataMember]
        public int? NumberOfMappedColumns { get; set; }

        [XmlElement(ElementName = "UserID")]
        [DataMember]
        public int? UserID { get; set; }

        [XmlElement(ElementName = "RoleId")]
        [DataMember]
        public int? RoleId { get; set; }

    }
}
