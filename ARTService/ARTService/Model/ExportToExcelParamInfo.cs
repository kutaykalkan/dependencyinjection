using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SkyStem.ART.Service.Model
{
    [Serializable]
    public class ExportToExcelParamInfo
    {
        protected System.Int32? _RequestID = null;
        protected System.String _FileName = null;
        protected System.String _PhysicalPath = null;
        protected System.Boolean? _IsFileDeleted = null;
        protected System.Boolean? _IsActive = null;
        protected System.Boolean? _IsMailSendingErrorFlag = null;

        [XmlElement(ElementName = "RequestID")]
        public virtual System.Int32? RequestID
        {
            get
            {
                return this._RequestID;
            }
            set
            {
                this._RequestID = value;

            }
        }

        [XmlElement(ElementName = "FileName")]
        public virtual System.String FileName
        {
            get
            {
                return this._FileName;
            }
            set
            {
                this._FileName = value;

            }
        }

        [XmlElement(ElementName = "PhysicalPath")]
        public virtual System.String PhysicalPath
        {
            get
            {
                return this._PhysicalPath;
            }
            set
            {
                this._PhysicalPath = value;

            }
        }

        [XmlElement(ElementName = "IsFileDeleted ")]
        public virtual System.Boolean? IsFileDeleted 
        {
            get
            {
                return this._IsFileDeleted;
            }
            set
            {
                this._IsFileDeleted = value;

            }
        }

        [XmlElement(ElementName = "IsActive")]
        public virtual System.Boolean? IsActive
        {
            get
            {
                return this._IsActive;
            }
            set
            {
                this._IsActive = value;

            }
        }

        [XmlElement(ElementName = "IsMailSendingErrorFlag")]
        public virtual System.Boolean? IsMailSendingErrorFlag
        {
            get
            {
                return this._IsMailSendingErrorFlag;
            }
            set
            {
                this._IsMailSendingErrorFlag = value;

            }
        }

        [XmlElement(ElementName = "IsRequestSuccessFull")]
        public bool? IsRequestSuccessFull { get; set; }

        [XmlElement(ElementName = "FileSize")]
        public decimal? FileSize { get; set; }

        [XmlElement(ElementName = "RequestStatusID")]
        public Int16? RequestStatusID { get; set; }

        [XmlElement(ElementName = "RequestErrorCodeID")]
        public Int16? RequestErrorCodeID { get; set; }

        [XmlElement(ElementName = "CompanyID")]
        public Int32? CompanyID { get; set; }

        [XmlElement(ElementName = "RevisedBy")]
        public string RevisedBy { get; set; }

        [XmlElement(ElementName = "DateRevised")]
        public DateTime DateRevised { get; set; }
    }
}
