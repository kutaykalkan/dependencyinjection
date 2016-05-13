

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemART ReportArchiveParameter table
    /// </summary>
    [Serializable]
    public abstract class ReportArchiveParameterInfoBase
    {

        protected System.String _ParameterValue = null;
        protected System.Int64? _ReportArchiveID = null;
        protected System.Int64? _ReportArchiveParameterID = null;
        protected System.Int16? _ReportParameterKeyID = null;




        public bool IsParameterValueNull = true;


        public bool IsReportArchiveIDNull = true;


        public bool IsReportArchiveParameterIDNull = true;


        public bool IsReportParameterKeyIDNull = true;

        [XmlElement(ElementName = "ParameterValue")]
        public virtual System.String ParameterValue
        {
            get
            {
                return this._ParameterValue;
            }
            set
            {
                this._ParameterValue = value;

                this.IsParameterValueNull = (_ParameterValue == null);
            }
        }

        [XmlElement(ElementName = "ReportArchiveID")]
        public virtual System.Int64? ReportArchiveID
        {
            get
            {
                return this._ReportArchiveID;
            }
            set
            {
                this._ReportArchiveID = value;

                this.IsReportArchiveIDNull = false;
            }
        }

        [XmlElement(ElementName = "ReportArchiveParameterID")]
        public virtual System.Int64? ReportArchiveParameterID
        {
            get
            {
                return this._ReportArchiveParameterID;
            }
            set
            {
                this._ReportArchiveParameterID = value;

                this.IsReportArchiveParameterIDNull = false;
            }
        }

        [XmlElement(ElementName = "ReportParameterKeyID")]
        public virtual System.Int16? ReportParameterKeyID
        {
            get
            {
                return this._ReportParameterKeyID;
            }
            set
            {
                this._ReportParameterKeyID = value;

                this.IsReportParameterKeyIDNull = false;
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
