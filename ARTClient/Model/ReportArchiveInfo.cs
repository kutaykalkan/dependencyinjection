
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace SkyStem.ART.Client.Model
{

    /// <summary>
    /// An object representation of the SkyStemART ReportArchive table
    /// </summary>
    [Serializable]
    [DataContract]
    public class ReportArchiveInfo : ReportArchiveInfoBase
    {
        protected System.String _ArchiveType = null;
        public bool IsArchiveTypeNull = true;
        [XmlElement(ElementName = "ArchiveType")]
        public virtual System.String ArchiveType
        {
            get
            {
                return this._ArchiveType;
            }
            set
            {
                this._ArchiveType = value;

                this.IsArchiveTypeNull = (_ArchiveType == null);
            }
        }

        protected System.String _ReportUrl = null;
        public bool IsReportUrlNull = true;
        [XmlElement(ElementName = "ReportUrl")]
        public virtual System.String ReportUrl
        {
            get
            {
                return this._ReportUrl;
            }
            set
            {
                this._ReportUrl = value;

                this.IsReportUrlNull = (_ReportUrl == null);
            }
        }

        protected System.String _ReportPrintUrl = null;
        [XmlElement(ElementName = "ReportPrintUrl")]
        public virtual System.String ReportPrintUrl
        {
            get
            {
                return this._ReportPrintUrl;
            }
            set
            {
                this._ReportPrintUrl = value;
            }
        }


        protected IList<ReportArchiveParameterInfo> _ReportArchiveParameterByRptArchiveID = null;
        public bool IsReportArchiveParameterByRptArchiveIDNull = true;
        [XmlElement(ElementName = "ReportArchiveParameterByReportArchiveID")]
        public virtual IList<ReportArchiveParameterInfo> ReportArchiveParameterByRptArchiveID
        {
            get
            {
                return this._ReportArchiveParameterByRptArchiveID;
            }
            set
            {
                this._ReportArchiveParameterByRptArchiveID = value;
                this.IsReportArchiveParameterByRptArchiveIDNull = (this._ReportArchiveParameterByRptArchiveID == null);

            }
        }

        protected System.DateTime? _PeriodEndDate = null;
        public bool IsPeriodEndDateNull = true;
        [XmlElement(ElementName = "PeriodEndDate")]
        public virtual System.DateTime? PeriodEndDate
        {
            get
            {
                return this._PeriodEndDate;
            }
            set
            {
                this._PeriodEndDate = value;

                this.IsPeriodEndDateNull = (_PeriodEndDate == DateTime.MinValue);
            }
        }

        protected System.String _FirstName = null;
        public bool IsFirstNameNull = true;
        [XmlElement(ElementName = "FirstName")]
        public virtual System.String FirstName
        {
            get
            {
                return this._FirstName;
            }
            set
            {
                this._FirstName = value;

                this.IsFirstNameNull = (_FirstName == null);
            }
        }

        protected System.String _LastName = null;
        public bool IsLastNameNull = true;
        [XmlElement(ElementName = "LastName")]
        public virtual System.String LastName
        {
            get
            {
                return this._LastName;
            }
            set
            {
                this._LastName = value;

                this.IsLastNameNull = (_LastName == null);
            }
        }
        [XmlElement(ElementName = "SignedBy")]
        public string SignedBy { get; set; }
        [XmlElement(ElementName = "SignedByRole")]
        public string SignedByRole { get; set; }
        [XmlElement(ElementName = "ReportName")]
        public string ReportName { get; set; }

    }
}
