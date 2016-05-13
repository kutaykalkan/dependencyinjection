

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemArt ReportColumn table
    /// </summary>
    [Serializable]
    public abstract class ReportColumnInfoBase
    {

        protected System.String _AddedBy = "";
        protected System.Int32 _ReportColumnID = 0;
        protected System.Int16 _ColumnID = 0;
        protected System.Int16 _ReportID = 0;
        protected System.Int32 _ColumnNameLabelID = 0;
        protected System.DateTime _DateAdded = DateTime.Now;
        protected System.DateTime _DateRevised = DateTime.Now;
        protected System.String _HostName = "";
        protected System.Boolean _IsActive = false;
        protected System.Boolean _IsOptional = false;
        protected System.String _RevisedBy = "";

        public bool IsAddedByNull = true;

        public bool IsReportColumnIDNull = true;

        public bool IsColumnIDNull = true;

        public bool IsReportIDNull = true;

        public bool IsColumnNameLabelIDNull = true;

        public bool IsDateAddedNull = true;

        public bool IsDateRevisedNull = true;

        public bool IsHostNameNull = true;

        public bool IsIsActiveNull = true;

        public bool IsIsOptionalNull = true;

        public bool IsRevisedByNull = true;

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

                this.IsAddedByNull = (_AddedBy == null);
            }
        }

        [XmlElement(ElementName = "ReportColumnID")]
        public virtual System.Int32 ReportColumnID
        {
            get
            {
                return this._ReportColumnID;
            }
            set
            {
                this._ReportColumnID = value;

                this.IsReportColumnIDNull = false;
            }
        }

        [XmlElement(ElementName = "ColumnID")]
        public virtual System.Int16 ColumnID
        {
            get
            {
                return this._ColumnID;
            }
            set
            {
                this._ColumnID = value;

                this.IsColumnIDNull = false;
            }
        }


        [XmlElement(ElementName = "ReportID")]
        public virtual System.Int16 ReportID
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

        [XmlElement(ElementName = "ColumnNameLabelID")]
        public virtual System.Int32 ColumnNameLabelID
        {
            get
            {
                return this._ColumnNameLabelID;
            }
            set
            {
                this._ColumnNameLabelID = value;

                this.IsColumnNameLabelIDNull = false;
            }
        }

        [XmlElement(ElementName = "DateAdded")]
        public virtual System.DateTime DateAdded
        {
            get
            {
                return this._DateAdded;
            }
            set
            {
                this._DateAdded = value;

                this.IsDateAddedNull = (_DateAdded == DateTime.MinValue);
            }
        }

        [XmlElement(ElementName = "DateRevised")]
        public virtual System.DateTime DateRevised
        {
            get
            {
                return this._DateRevised;
            }
            set
            {
                this._DateRevised = value;

                this.IsDateRevisedNull = (_DateRevised == DateTime.MinValue);
            }
        }

        [XmlElement(ElementName = "HostName")]
        public virtual System.String HostName
        {
            get
            {
                return this._HostName;
            }
            set
            {
                this._HostName = value;

                this.IsHostNameNull = (_HostName == null);
            }
        }

        [XmlElement(ElementName = "IsActive")]
        public virtual System.Boolean IsActive
        {
            get
            {
                return this._IsActive;
            }
            set
            {
                this._IsActive = value;

                this.IsIsActiveNull = false;
            }
        }

        [XmlElement(ElementName = "IsOptional")]
        public virtual System.Boolean IsOptional
        {
            get
            {
                return this._IsOptional;
            }
            set
            {
                this._IsOptional = value;

                this.IsIsOptionalNull = false;
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

                this.IsRevisedByNull = (_RevisedBy == null);
            }
        }


        /// <summary>
        /// Returns a string representation of the object, displaying all property and field names and values.
        /// </summary>
        public override string ToString()
        {
            return StringUtil.ToString(this);
        }

    }
}
