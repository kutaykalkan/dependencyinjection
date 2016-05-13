
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
    public class DataImportHdrInfo : DataImportHdrInfoBase
    {
        protected System.Int32? _DataImportTypeLabelID = null;
        protected System.Int32? _DataImportStatusLabelID = null;
        protected System.String _DataImportStatus = "";
        protected System.String _DataImportType = "";
        protected System.Int32? _LanguageID = 0;
        protected System.Int16? _RoleID = 0;
        protected System.Boolean? _IsMultiVersionUpload = false;
        protected System.Boolean? _IsRecordOwner = false;
        protected System.String _EmailID = null;

        protected DataImportFailureMessageInfo _DataImportFailureMessageInfo = null;

        [XmlElement(ElementName = "DataImportTypeLabelID")]
        public System.Int32? DataImportTypeLabelID
        {
            get
            {
                return this._DataImportTypeLabelID;
            }
            set
            {
                this._DataImportTypeLabelID = value;
            }
        }

        [XmlElement(ElementName = "DataImportStatusLabelID")]
        public System.Int32? DataImportStatusLabelID
        {
            get
            {
                return this._DataImportStatusLabelID;
            }
            set
            {
                this._DataImportStatusLabelID = value;
            }
        }

        [XmlElement(ElementName = "DataImportStatus")]
        public System.String DataImportStatus
        {
            get
            {
                return this._DataImportStatus;
            }
            set
            {
                this._DataImportStatus = value;
            }
        }

        [XmlElement(ElementName = "DataImportType")]
        public System.String DataImportType
        {
            get
            {
                return this._DataImportType;
            }
            set
            {
                this._DataImportType = value;
            }
        }

        [XmlElement(ElementName = "LanguageID")]
        public System.Int32? LanguageID
        {
            get
            {
                return this._LanguageID;
            }
            set
            {
                this._LanguageID = value;
            }
        }


        [XmlElement(ElementName = "DataImportFailureMessageInfo")]
        public DataImportFailureMessageInfo DataImportFailureMessageInfo
        {
            get
            {
                return this._DataImportFailureMessageInfo;
            }
            set
            {
                this._DataImportFailureMessageInfo = value;
            }
        }

        [XmlElement(ElementName = "RoleID")]
        public System.Int16? RoleID
        {
            get
            {
                return this._RoleID;
            }
            set
            {
                this._RoleID = value;
            }
        }
        [XmlElement(ElementName = "IsMultiVersionUpload")]
        public System.Boolean? IsMultiVersionUpload
        {



            get
            {
                return this._IsMultiVersionUpload;
            }
            set
            {
                this._IsMultiVersionUpload = value;
            }
        }

        [XmlElement(ElementName = "IsRecordOwner")]
        public System.Boolean? IsRecordOwner
        {
            get
            {
                return this._IsRecordOwner;
            }
            set
            {
                this._IsRecordOwner = value;
            }
        }
        [XmlElement(ElementName = "EmailID")]
        public System.String EmailID
        {
            get
            {
                return this._EmailID;
            }
            set
            {
                this._EmailID = value;
            }
        }

        [XmlElement(ElementName = "DataImportMultilingualUploadInfo")]
        [DataMember]
        public DataImportMultilingualUploadInfo DataImportMultilingualUploadInfo { get; set; }

        [XmlElement(ElementName = "SystemLockdownInfo")]
        [DataMember]
        public SystemLockdownInfo SystemLockdownInfo { get; set; }

        [XmlElement(ElementName = "DataImportRecItemUploadInfo")]
        [DataMember]
        public DataImportRecItemUploadInfo DataImportRecItemUploadInfo { get; set; }

        [XmlElement(ElementName = "DataImportAccountMessageDetailInfoList")]
        [DataMember]
        public List<DataImportMessageDetailInfo> DataImportAccountMessageDetailInfoList { get; set; }

        [XmlElement(ElementName = "DataImportMessageDetailInfoList")]
        [DataMember]
        public List<DataImportMessageDetailInfo> DataImportMessageDetailInfoList { get; set; }

        [XmlElement(ElementName = "ImportTemplateID")]
        [DataMember]
        public int? ImportTemplateID { get; set; }


        [XmlElement(ElementName = "TemplateName")]
        [DataMember]
        public string TemplateName { get; set; }

        [XmlElement(ElementName = "RecordSourceTypeID")]
        [DataMember]
        public short? RecordSourceTypeID { get; set; }
    }
}
