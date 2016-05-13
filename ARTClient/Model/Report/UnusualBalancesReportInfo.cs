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
    public  class UnusualBalancesReportInfo : OrganizationalHierarchyInfo
    {
        protected System.Int64? _AccountID = null;
        protected System.String _AccountName = "";
        protected System.Int32? _AccountNameLabelID = null;
        protected System.String _AccountNumber = "";
        protected System.Int16? _AccountTypeID = null;

        protected System.Int32? _FSCaptionID = null;
        protected System.Boolean? _IsKeyAccount = null ;
        protected System.Boolean? _IsZeroBalance = null ;
        protected System.Int16? _RiskRatingID = null;
        
        protected System.Decimal? _GLBalanceReportingCurrency = null;
        [XmlElement(ElementName = "GLBalanceReportingCurrency")]
        public virtual System.Decimal? GLBalanceReportingCurrency
        {
            get
            {
                return this._GLBalanceReportingCurrency;
            }
            set
            {
                this._GLBalanceReportingCurrency = value;
            }
        }

        protected System.Decimal? _GLBalanceBaseCurrency = null;
        [XmlElement(ElementName = "GLBalanceBaseCurrency")]
        public virtual System.Decimal? GLBalanceBaseCurrency
        {
            get
            {
                return this._GLBalanceBaseCurrency;
            }
            set
            {
                this._GLBalanceBaseCurrency = value;
            }
        }
        
        protected System.String _RiskRating = "";
         [XmlElement(ElementName = "RiskRating")]
        public virtual System.String RiskRating
        {
            get
            {
                return this._RiskRating;
            }
            set
            {
                this._RiskRating = value;
            }
        }

        protected System.Boolean? _IsMaterial = null;
        [XmlElement(ElementName = "IsMaterial")]
        public virtual System.Boolean? IsMaterial
        {
            get
            {
                return this._IsMaterial;
            }
            set
            {
                this._IsMaterial = value;
            }
        }


        protected System.Int16? _ReasonID = null;
        [XmlElement(ElementName = "ReasonID")]
        public virtual System.Int16? ReasonID
        {
            get
            {
                return this._ReasonID;
            }
            set
            {
                this._ReasonID = value;
            }
        }

        protected System.Int32? _ReasonLabelID = null;
        [XmlElement(ElementName = "ReasonLabelID")]
        public virtual System.Int32? ReasonLabelID
        {
            get
            {
                return this._ReasonLabelID;
            }
            set
            {
                this._ReasonLabelID = value;
            }
        }


        protected System.String _Reason = "";
        [XmlElement(ElementName = "Reason")]
        public virtual System.String Reason
        {
            get
            {
                return this._Reason;
            }
            set
            {
                this._Reason = value;
            }
        }

        protected System.Int32? _PreparerUserID = null;
        [XmlElement(ElementName = "PreparerUserID")]
        public virtual System.Int32? PreparerUserID
        {
            get
            {
                return this._PreparerUserID;
            }
            set
            {
                this._PreparerUserID = value;
            }
        }

        
        protected System.String _PreparerFirstName = "";
         [XmlElement(ElementName = "PreparerFirstName")]
        public virtual System.String PreparerFirstName
        {
            get
            {
                return this._PreparerFirstName;
            }
            set
            {
                this._PreparerFirstName = value;
            }
        }


        
        protected System.String _PreparerLastName = "";
         [XmlElement(ElementName = "PreparerLastName")]
        public virtual System.String PreparerLastName
        {
            get
            {
                return this._PreparerLastName;
            }
            set
            {
                this._PreparerLastName = value;
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

        [XmlElement(ElementName = "IsKeyAccount")]
        public virtual System.Boolean? IsKeyAccount
        {
            get
            {
                return this._IsKeyAccount;
            }
            set
            {
                this._IsKeyAccount = value;
            }
        }

        [XmlElement(ElementName = "IsZeroBalance")]
        public virtual System.Boolean? IsZeroBalance
        {
            get
            {
                return this._IsZeroBalance;
            }
            set
            {
                this._IsZeroBalance = value;
            }
        }

        [XmlElement(ElementName = "RiskRatingID")]
        public virtual System.Int16? RiskRatingID
        {
            get
            {
                return this._RiskRatingID;
            }
            set
            {
                this._RiskRatingID = value;
            }
        }

        //BCCY Changes
        protected string _baseCurrencyCode;
        [XmlElement(ElementName="BaseCurrencyCode")]
        public virtual string BaseCurrencyCode
        {
            get
            {
                return this._baseCurrencyCode;
            }
            set
            {
                this._baseCurrencyCode = value;
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
