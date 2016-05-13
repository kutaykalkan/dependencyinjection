
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{

    /// <summary>
    /// An object representation of the SkyStemART ReportArchiveParameter table
    /// </summary>
    [Serializable]
    [DataContract]
    public class ReportArchiveParameterInfo : ReportArchiveParameterInfoBase
    {
        protected System.Int16? _ParameterID = null;
        public bool IsParameterIDNull = true;
        [XmlElement(ElementName = "ParameterID")]
        public virtual System.Int16? ParameterID
        {
            get
            {
                return this._ParameterID;
            }
            set
            {
                this._ParameterID = value;

                this.IsParameterIDNull = false;
            }
        }
        
        
        public bool IsReportParameterKeyNameNull = true;
        protected System.String _ReportParameterKeyName = null;
        [XmlElement(ElementName = "ReportParameterKeyName")]
        public virtual System.String ReportParameterKeyName
        {
            get
            {
                return this._ReportParameterKeyName;
            }
            set
            {
                this._ReportParameterKeyName = value;

                this.IsReportParameterKeyNameNull = (_ReportParameterKeyName == null);
            }
        }

        //ReportParameterDisplayName
        public bool IsReportParameterDisplayNameNull = true;
        protected System.String _ReportParameterDisplayName = null;
        [XmlElement(ElementName = "ReportParameterDisplayName")]
        public virtual System.String ReportParameterDisplayName
        {
            get
            {
                return this._ReportParameterDisplayName;
            }
            set
            {
                this._ReportParameterDisplayName = value;

                this.IsReportParameterDisplayNameNull = (_ReportParameterDisplayName == null);
            }
        }
    }
}
