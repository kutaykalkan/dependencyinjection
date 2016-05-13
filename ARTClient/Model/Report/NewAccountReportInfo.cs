using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Client.Model.Base;
using System.Xml.Serialization;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Report
{
    /// <summary>
    /// An object representation of the New Account Report
    /// </summary>
    [Serializable]
    public class NewAccountReportInfo : OrganizationalHierarchyInfo
    {
        protected System.Int64? _AccountID = null;
        protected System.String _AccountName = "";
        protected System.Int32? _AccountNameLabelID = null;
        protected System.String _AccountNumber = "";
        protected System.Int16? _AccountTypeID = null;
        protected System.Int32? _FSCaptionID = null;

        [XmlElement(ElementName = "AccountID")]
        public virtual System.Int64? AccountID
        {
            get
            {
                return this._AccountID;
            }
            set
            {
                this._AccountID = value;
            }
        }

        [XmlElement(ElementName = "AccountName")]
        public virtual System.String AccountName
        {
            get
            {
                return this._AccountName;
            }
            set
            {
                this._AccountName = value;
            }
        }

        [XmlElement(ElementName = "AccountNameLabelID")]
        public virtual System.Int32? AccountNameLabelID
        {
            get
            {
                return this._AccountNameLabelID;
            }
            set
            {
                this._AccountNameLabelID = value;
            }
        }


        [XmlElement(ElementName = "AccountNumber")]
        public virtual System.String AccountNumber
        {
            get
            {
                return this._AccountNumber;
            }
            set
            {
                this._AccountNumber = value;
            }
        }


        [XmlElement(ElementName = "AccountTypeID")]
        public virtual System.Int16? AccountTypeID
        {
            get
            {
                return this._AccountTypeID;
            }
            set
            {
                this._AccountTypeID = value;
            }
        }


        [XmlElement(ElementName = "FSCaptionID")]
        public virtual System.Int32? FSCaptionID
        {
            get
            {
                return this._FSCaptionID;
            }
            set
            {
                this._FSCaptionID = value;
            }
        }

        [XmlElement(ElementName = "CreationPeriod")]
        public DateTime? CreationPeriod { get; set; }

        [XmlElement(ElementName = "ProfileName")]
        public string ProfileName { get; set; }

        [XmlElement(ElementName = "UploadedBy")]
        public string UploadedBy { get; set; }

        [XmlElement(ElementName = "DateUploaded")]
        public DateTime? DateUploaded { get; set; }

        /// <summary>
        /// Returns a string representation of the object, displaying all property and field names and values.
        /// </summary>
        public override string ToString()
        {
            return StringUtil.ToString(this);
        }
    }
}
