
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemArt ReportMst table
	/// </summary>
	[Serializable]
	[DataContract]
	public class ReportMstInfo : ReportMstInfoBase
	{
        private System.Int32? _ReportTypeLabelID = 0;
        private String _ReportType = null;
        private String _ReportUrl = null;
        private String _ReportPrintUrl = null;
        private System.DateTime? _SignOffDate = null;
        private System.Int32? _UserMyReportID = 0;
        private System.Int32? _ReportRoleMandatoryReportID = 0;

        private System.String _Comments = string.Empty;

        [XmlElement(ElementName = "ReportTypeLabelID")]
        public System.Int32? ReportTypeLabelID
        {
            get
            {
                return _ReportTypeLabelID;
            }
            set
            {
                _ReportTypeLabelID = value;
            }
        }

        [XmlElement(ElementName = "ReportType")]
        public String ReportType
        {
            get
            {
                return _ReportType;
            }
            set
            {
                _ReportType = value;
            }
        }

        [XmlElement(ElementName = "SignOffDate")]
        public virtual System.DateTime? SignOffDate
        {
            get
            {
                return this._SignOffDate;
            }
            set
            {
                this._SignOffDate = value;                
            }
        }

        [XmlElement(ElementName = "UserMyReportID")]
        public System.Int32? UserMyReportID
        {
            get
            {
                return _UserMyReportID;
            }
            set
            {
                _UserMyReportID = value;
            }
        }


        [XmlElement(ElementName = "ReportUrl")]
        public String ReportUrl
        {
            get
            {
                return _ReportUrl;
            }
            set
            {
                _ReportUrl = value;
            }
        }

        [XmlElement(ElementName = "ReportPrintUrl")]
        public String ReportPrintUrl
        {
            get
            {
                return _ReportPrintUrl;
            }
            set
            {
                _ReportPrintUrl = value;
            }
        }

        [XmlElement(ElementName = "ReportRoleMandatoryReportID")]
        public System.Int32? ReportRoleMandatoryReportID
        {
            get
            {
                return _ReportRoleMandatoryReportID;
            }
            set
            {
                _ReportRoleMandatoryReportID = value;
            }
        }

        // Used for Printing of Report
        [XmlElement(ElementName = "Comments")]
        public String Comments
        {
            get
            {
                return _Comments;
            }
            set
            {
                _Comments = value;
            }
        }


    }
}
