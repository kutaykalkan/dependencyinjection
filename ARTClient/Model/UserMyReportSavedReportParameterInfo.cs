
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemArt UserMyReportSavedReportParameter table
	/// </summary>
	[Serializable]
	[DataContract]
	public class UserMyReportSavedReportParameterInfo : UserMyReportSavedReportParameterInfoBase
	{


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


        protected System.Int16? _ParameterMstID = null;
        public bool IsParameterMstIDNull = true;
        [XmlElement(ElementName = "ParameterMstID")]
        public virtual System.Int16? ParameterMstID
        {
            get
            {
                return this._ParameterMstID;
            }
            set
            {
                this._ParameterMstID = value;

                this.IsParameterMstIDNull = false;
            }
        }

	}
}
