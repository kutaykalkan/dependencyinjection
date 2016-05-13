using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;

namespace SkyStem.ART.Client.Model.Report
{

    /// <summary>
    /// An object representation of the SkyStemArt AccountHdr table
    /// </summary>
    [Serializable]
    public class IncompleteAccountAttributeReportInfo : OrganizationalHierarchyInfo
    {
        protected System.Int64? _AccountID = null;
        protected System.String _AccountName = "";
        protected System.Int32? _AccountNameLabelID = null;
        protected System.String _AccountNumber = "";
        protected System.Int16? _AccountTypeID = null;
        protected System.Int32? _FSCaptionID = null;
        protected System.Boolean? _IsTemplateAttributeMissing = null ;
        protected System.Boolean? _IsKeyAccountAttributeMissing = null;
        protected System.Boolean? _IsZeroBalanceAttributeMissing = null;
        protected System.Boolean? _IsRiskRatingAttributeMissing = null;
        protected System.Boolean? _IsFrequencyAttributeMissing = null;

        [XmlElement(ElementName = "IsTemplateAttributeMissing")]
        public virtual System.Boolean? IsTemplateAttributeMissing
        {
            get
            {
                return this._IsTemplateAttributeMissing;
            }
            set
            {
                this._IsTemplateAttributeMissing = value;
            }
        }
        [XmlElement(ElementName = "IsKeyAccountAttributeMissing")]
        public virtual System.Boolean? IsKeyAccountAttributeMissing
        {
            get
            {
                return this._IsKeyAccountAttributeMissing;
            }
            set
            {
                this._IsKeyAccountAttributeMissing = value;
            }
        }

        [XmlElement(ElementName = "IsZeroBalanceAttributeMissing")]
        public virtual System.Boolean? IsZeroBalanceAttributeMissing
        {
            get
            {
                return this._IsZeroBalanceAttributeMissing;
            }
            set
            {
                this._IsZeroBalanceAttributeMissing = value;
            }
        }

        [XmlElement(ElementName = "IsRiskRatingAttributeMissing")]
        public virtual System.Boolean? IsRiskRatingAttributeMissing
        {
            get
            {
                return this._IsRiskRatingAttributeMissing;
            }
            set
            {
                this._IsRiskRatingAttributeMissing = value;
            }
        }
        
        [XmlElement(ElementName = "IsFrequencyAttributeMissing")]
        public virtual System.Boolean? IsFrequencyAttributeMissing
        {
            get
            {
                return this._IsFrequencyAttributeMissing;
            }
            set
            {
                this._IsFrequencyAttributeMissing = value;
            }
        }
        

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

        [XmlElement(ElementName = "IsPreparerDueDaysAttributeMissing")]
        public Boolean? IsPreparerDueDaysAttributeMissing { get; set; }

        [XmlElement(ElementName = "IsReviewerDueDaysAttributeMissing")]
        public Boolean? IsReviewerDueDaysAttributeMissing { get; set; }

        [XmlElement(ElementName = "IsApproverDueDaysAttributeMissing")]
        public Boolean? IsApproverDueDaysAttributeMissing { get; set; }

        /// <summary>
        /// Returns a string representation of the object, displaying all property and field names and values.
        /// </summary>
        public override string ToString()
        {
            return StringUtil.ToString(this);
        }

    }
}
