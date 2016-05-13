using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{
   public  class ReportActivityInfo
    {
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

        protected System.DateTime? _ArchivedDate = DateTime.Now;
        public bool IsArchivedDateNull = true;
        [XmlElement(ElementName = "ArchivedDate")]
        public virtual System.DateTime? ArchivedDate
        {
            get
            {
                return this._ArchivedDate;
            }
            set
            {
                this._ArchivedDate = value;
                this.IsArchivedDateNull = (_ArchivedDate == DateTime.MinValue);
            }
        }

        protected System.String _ActionPerformed = "";
        public bool IsActionPerformedNull = true;
        [XmlElement(ElementName = "ActionPerformed")]
        public virtual System.String ActionPerformed
        {
            get
            {
                return this._ActionPerformed;
            }
            set
            {
                this._ActionPerformed = value;
                this.IsActionPerformedNull = (_ActionPerformed == null);
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

        protected System.String _SignOffComment = "";
        public bool IsSignOffCommentNull = true;
        [XmlElement(ElementName = "SignOffComment")]
        public virtual System.String SignOffComment
        {
            get
            {
                return this._SignOffComment;
            }
            set
            {
                this._SignOffComment = value;
                this.IsSignOffCommentNull = (_SignOffComment == null);
            }
        }

    }//end of class
}//end of namespace
