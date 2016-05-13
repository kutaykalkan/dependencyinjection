
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemArt UserMyReportSavedReport table
	/// </summary>
	[Serializable]
	[DataContract]
	public class UserMyReportSavedReportInfo : UserMyReportSavedReportInfoBase
	{
        protected System.String _ReportName = "";

        public bool IsReportNameNull = true;
        [XmlElement(ElementName = "ReportName")]
        public virtual System.String ReportName
        {
            get
            {
                return this._ReportName;
            }
            set
            {
                this._ReportName = value;

                this.IsReportNameNull = (_ReportName == null);
            }
        }


        protected System.Int32 _UserID = 0;
        public bool IsUserIDNull = true;

        [XmlElement(ElementName = "UserID")]
        public virtual System.Int32 UserID
        {
            get
            {
                return this._UserID;
            }
            set
            {
                this._UserID = value;

                this.IsUserIDNull = false;
            }
        }


        protected System.Int32? _ReportLabelID = 0;
        public bool IsReportLabelIDNull = true;
        [XmlElement(ElementName = "ReportLabelID")]
        public virtual System.Int32? ReportLabelID
        {
            get
            {
                return this._ReportLabelID;
            }
            set
            {
                this._ReportLabelID = value;

                this.IsReportLabelIDNull = false;
            }
        }

        protected IList<UserMyReportSavedReportParameterInfo> _UserMyReportSavedReportParameterByUserMyReportSavedReportID = null;
        public bool IsUserMyReportSavedReportParameterByUserMyReportSavedReportIDNull = true;
        [XmlElement(ElementName = "UserMyReportSavedReportParameterByUserMyReportSavedReportID")]
        public virtual IList<UserMyReportSavedReportParameterInfo> UserMyReportSavedReportParameterByUserMyReportSavedReportID
        {
            get
            {
                return this._UserMyReportSavedReportParameterByUserMyReportSavedReportID;
            }
            set
            {
                this._UserMyReportSavedReportParameterByUserMyReportSavedReportID = value;
                this.IsUserMyReportSavedReportParameterByUserMyReportSavedReportIDNull = (this._UserMyReportSavedReportParameterByUserMyReportSavedReportID == null);

            }
        }


	}
}
