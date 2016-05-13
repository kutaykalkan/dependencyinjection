using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SkyStem.ART.Service.Model
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ExportToExcelInfo
    {
        protected System.Int32? _GridID = null;
        protected System.String _ToEmailID = null;
        protected System.String _FinalMessage = null;
        protected System.String _FromEmailID = null;
        protected System.String _EmailBody = null;
        protected System.String _EmailSubject = null;
        protected System.String _Data = null;
        protected System.Int32? _RequestID= null;

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


        [XmlElement(ElementName = "GridID")]
        public virtual System.Int32? GridID
        {
            get
            {
                return this._GridID;
            }
            set
            {
                this._GridID = value;

            }
        }

        [XmlElement(ElementName = "ToEmailID")]
        public virtual System.String ToEmailID
        {
            get
            {
                return this._ToEmailID;
            }
            set
            {
                this._ToEmailID = value;

            }
        }

        [XmlElement(ElementName = "FinalMessage")]
        public virtual System.String FinalMessage
        {
            get
            {
                return this._FinalMessage;
            }
            set
            {
                this._FinalMessage = value;

            }
        }

        [XmlElement(ElementName = "FromEmailID")]
        public virtual System.String FromEmailID
        {
            get
            {
                return this._FromEmailID;
            }
            set
            {
                this._FromEmailID = value;

            }
        }

        [XmlElement(ElementName = "EmailBody")]
        public virtual System.String EmailBody
        {
            get
            {
                return this._EmailBody;
            }
            set
            {
                this._EmailBody = value;

            }
        }

        [XmlElement(ElementName = "EmailSubject")]
        public virtual System.String EmailSubject
        {
            get
            {
                return this._EmailSubject;
            }
            set
            {
                this._EmailSubject = value;

            }
        }

        [XmlElement(ElementName = "Data")]
        public virtual System.String Data
        {
            get
            {
                return this._Data;
            }
            set
            {
                this._Data = value;

            }
        }

        [XmlElement(ElementName = "RequestTypeID")]
        public Int16? RequestTypeID { get; set; }

        [XmlElement(ElementName = "UserID")]
        public Int32? UserID { get; set; }

        [XmlElement(ElementName = "RoleID")]
        public Int16? RoleID { get; set; }

        [XmlElement(ElementName = "LanguageID")]
        public Int32? LanguageID { get; set; }

        [XmlElement(ElementName = "RecPeriodID")]
        public Int32? RecPeriodID { get; set; }

        [XmlElement(ElementName = "CompanyID")]
        public Int32? CompanyID { get; set; }

        [XmlElement(ElementName = "IsEmailSuccessFull")]
        public bool? IsEmailSuccessFull { get; set; }

        [XmlElement(ElementName = "IsRequestSuccessFull")]
        public bool? IsRequestSuccessFull { get; set; }

        [XmlElement(ElementName = "OutputFile")]
        public string OutputFile { get; set; }

        [XmlElement(ElementName = "OutputFileSize")]
        public decimal? OutputFileSize { get; set; }

        [XmlElement(ElementName = "AddedBy")]
        public string AddedBy { get; set; }

        [XmlElement(ElementName = "IsPartOfStorageSpace")]
        public bool? IsPartOfStorageSpace { get; set; }
    }
}
