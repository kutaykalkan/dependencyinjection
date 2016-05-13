
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemArt ReportParameter table
	/// </summary>
	[Serializable]
	[DataContract]
	public class ReportParameterInfo : ReportParameterInfoBase
	{
        protected System.String _ParameterName = "";
        
        public bool IsReportParameterNameNull = true;
        
        [XmlElement(ElementName = "ParameterName")]
        public virtual System.String ParameterName
        {
            get
            {
                return this._ParameterName;
            }
            set
            {
                this._ParameterName = value;

                this.IsReportParameterNameNull = false;
            }
        }


        protected System.String _ParameterUrl = "";

        public bool IsReportParameterUrlNull = true;

        [XmlElement(ElementName = "ParameterUrl")]
        public virtual System.String ParameterUrl
        {
            get
            {
                return this._ParameterUrl;
            }
            set
            {
                this._ParameterUrl = value;

                this.IsReportParameterUrlNull = false;
            }
        }

	}
}
