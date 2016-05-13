using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{
    [Serializable]
    [DataContract]
    public  class ReportSavedInfo
    {
        protected System.Int16? _SavedReportID = 0;
        public bool IsSavedReportIDNull = true;
        [XmlElement(ElementName = "SavedReportID")]
        public virtual System.Int16? SavedReportID
        {
            get
            {
                return this._SavedReportID;
            }
            set
            {
                this._SavedReportID = value;

                this.IsSavedReportIDNull = false;
            }
        }

        protected System.Int16? _ReportID = 0;
        public bool IsReportIDNull = true;
        [XmlElement(ElementName = "ReportID")]
        public virtual System.Int16? ReportID
        {
            get
            {
                return this._ReportID;
            }
            set
            {
                this._ReportID = value;

                this.IsReportIDNull = false;
            }
        }

        protected System.String _Report = "";
        public bool IsReportNull = true;
        [XmlElement(ElementName = "Report")]
        public virtual System.String Report
        {
            get
            {
                return this._Report;
            }
            set
            {
                this._Report = value;

                this.IsReportNull = (_Report == null);
            }
        }

        protected System.String _SavedReport = "";
        public bool IsSavedReportNull = true;
        [XmlElement(ElementName = "SavedReport")]
        public virtual System.String SavedReport
        {
            get
            {
                return this._SavedReport;
            }
            set
            {
                this._SavedReport = value;

                this.IsSavedReportNull = (_SavedReport == null);
            }
        }


        protected System.String _Parameters = "";
        public bool IsParametersNull = true;
        [XmlElement(ElementName = "Parameters")]
        public virtual System.String Parameters
        {
            get
            {
                return this._Parameters;
            }
            set
            {
                this._Parameters = value;
                this.IsParametersNull = (_Parameters == null);
            }
        }


        protected System.String _SavedBy = "";
        public bool IsSavedByNull = true;
        [XmlElement(ElementName = "SavedBy")]
        public virtual System.String SavedBy
        {
            get
            {
                return this._SavedBy;
            }
            set
            {
                this._SavedBy = value;

                this.IsSavedByNull = (_SavedBy == null);
            }
        }

        protected System.DateTime? _DateSaved = DateTime.Now;
        public bool IsDateSavedNull = true;
        [XmlElement(ElementName = "DateSaved")]
        public virtual System.DateTime? DateSaved
        {
            get
            {
                return this._DateSaved;
            }
            set
            {
                this._DateSaved = value;

                this.IsDateSavedNull = (_DateSaved == DateTime.MinValue);
            }
        }

    }
}
