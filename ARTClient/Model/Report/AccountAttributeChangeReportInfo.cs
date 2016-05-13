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
    /// An object representation of the Account Attribute Change Report
    /// </summary>
    [Serializable]
    public class AccountAttributeChangeReportInfo : OrganizationalHierarchyInfo
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

        [XmlElement(ElementName = "AccountAttributeID")]
        public short? AccountAttributeID { get; set; }

        [XmlElement(ElementName = "AccountAttribute")]
        public string AccountAttribute { get; set; }

        [XmlElement(ElementName = "AccountAttributeLabelID")]
        public int? AccountAttributeLabelID { get; set; }

        [XmlElement(ElementName = "Value")]
        public string Value { get; set; }

        [XmlElement(ElementName = "ValueLabelID")]
        public int? ValueLabelID { get; set; }

        [XmlElement(ElementName = "ValidFrom")]
        public DateTime? ValidFrom { get; set; }

        [XmlElement(ElementName = "ValidUntil")]
        public DateTime? ValidUntil { get; set; }

        [XmlElement(ElementName = "ChangeDate")]
        public DateTime? ChangeDate { get; set; }

        [XmlElement(ElementName = "ChangePeriod")]
        public DateTime? ChangePeriod { get; set; }

        [XmlElement(ElementName = "UpdatedBy")]
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Returns a string representation of the object, displaying all property and field names and values.
        /// </summary>
        public override string ToString()
        {
            return StringUtil.ToString(this);
        }
    }
}
