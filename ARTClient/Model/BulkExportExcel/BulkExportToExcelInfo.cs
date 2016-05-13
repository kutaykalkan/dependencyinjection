using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model.BulkExportExcel
{
    public class BulkExportToExcelInfo
    {
        protected System.Int32? _UserID = null;
        protected System.Int16? _RoleID = null;
        protected System.Int32? _RecperiodID = null;
        protected System.Int32? _GridID = null;
        protected System.Int16? _StatusID = null;
        protected System.Int16? _RequestTypeID = null;
        protected System.DateTime? _DateAdded = null;
        protected System.String _AddedBy = null;
        protected System.DateTime? _DateRevised = null;
        protected System.String _RevisedBy = null;
        protected System.Boolean? _IsActive = null;
        protected System.String _ToEmailID = null;
        protected System.String _FinalMessage = null;
        protected System.String _FromEmailID = null;
        protected System.String _EmailBody = null;
        protected System.String _EmailSubject = null;
        protected System.String _Data = null;


        [XmlElement(ElementName = "UserID")]
        public virtual System.Int32? UserID
        {
            get
            {
                return this._UserID;
            }
            set
            {
                this._UserID = value;

            }
        }

        [XmlElement(ElementName = "RoleID")]
        public virtual System.Int16? RoleID
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

        [XmlElement(ElementName = "RecperiodID")]
        public virtual System.Int32? RecperiodID
        {
            get
            {
                return this._RecperiodID;
            }
            set
            {
                this._RecperiodID = value;

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

        [XmlElement(ElementName = "StatusID")]
        public virtual System.Int16? StatusID
        {
            get
            {
                return this._StatusID;
            }
            set
            {
                this._StatusID = value;

            }
        }

        [XmlElement(ElementName = "RequestTypeID")]
        public virtual System.Int16? RequestTypeID
        {
            get
            {
                return this._RequestTypeID;
            }
            set
            {
                this._RequestTypeID = value;

            }
        }

        [XmlElement(ElementName = "DateAdded")]
        public virtual System.DateTime? DateAdded
        {
            get
            {
                return this._DateAdded;
            }
            set
            {
                this._DateAdded = value;

            }
        }

        [XmlElement(ElementName = "AddedBy")]
        public virtual System.String AddedBy
        {
            get
            {
                return this._AddedBy;
            }
            set
            {
                this._AddedBy = value;

            }
        }

        [XmlElement(ElementName = "DateRevised")]
        public virtual System.DateTime? DateRevised
        {
            get
            {
                return this._DateRevised;
            }
            set
            {
                this._DateRevised = value;

            }
        }

        [XmlElement(ElementName = "RevisedBy")]
        public virtual System.String RevisedBy
        {
            get
            {
                return this._RevisedBy;
            }
            set
            {
                this._RevisedBy = value;

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


        [XmlElement(ElementName = "RequestTypeLabelID")]
        public int? RequestTypeLabelID { get; set; }
        [XmlElement(ElementName = "RequestType")]
        public string RequestType { get; set; }
        [XmlElement(ElementName = "GridNameLabelID")]
        public int? GridNameLabelID { get; set; }
        [XmlElement(ElementName = "GridName")]
        public string GridName { get; set; }
        [XmlElement(ElementName = "FileName")]
        public string FileName { get; set; }
        [XmlElement(ElementName = "PhysicalPath")]
        public string PhysicalPath { get; set; }
        [XmlElement(ElementName = "AddedByUserName")]
        public string AddedByUserName { get; set; }
        [XmlElement(ElementName = "RequestStatusLabelID")]
        public int? RequestStatusLabelID { get; set; }
        [XmlElement(ElementName = "RequestStatus")]
        public string RequestStatus { get; set; }
        [XmlElement(ElementName = "LanguageID")]
        public int LanguageID { get; set; }
        [XmlElement(ElementName = "IsRecordOwner")]
        public bool IsRecordOwner { get; set; }
        [XmlElement(ElementName = "RequestID")]
        public int? RequestID { get; set; }
        [XmlElement(ElementName = "FinancialYear")]
        public string FinancialYear { get; set; }
        [XmlElement(ElementName = "PeriodEndDate")]
        public DateTime PeriodEndDate { get; set; }
        [XmlElement(ElementName = "MonthYear")]
        public string MonthYear { get; set; }
        [XmlElement(ElementName = "RequestErrorLabelID")]
        public int? RequestErrorLabelID { get; set; }
        [XmlElement(ElementName = "StatusMessage")]
        public string StatusMessage { get; set; }
    }
}
