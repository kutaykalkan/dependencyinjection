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
    public  class OpenItemsReportInfo : OrganizationalHierarchyInfo
    {
        #region UserRole fields

        protected System.Int32? _UserID = null;
        protected System.Int16? _RoleID = null;
        protected System.String _FirstName = "";
        protected System.String _LastName = "";
        protected System.Int32? _RoleLabelID = null;
        protected System.String _Role = "";

        [XmlElement(ElementName = "UserID")]
        public virtual System.Int32? UserID
        {
            get
            {
                return this._UserID;
            }
            set
            {
                this._UserID = value;
            }
        }
        [XmlElement(ElementName = "RoleID")]
        public virtual System.Int16? RoleID
        {
            get
            {
                return this._RoleID;
            }
            set
            {
                this._RoleID = value;
            }
        }
        [XmlElement(ElementName = "FirstName")]
        public virtual System.String FirstName
        {
            get
            {
                return this._FirstName;
            }
            set
            {
                this._FirstName = value;
            }
        }
        [XmlElement(ElementName = "LastName")]
        public virtual System.String LastName
        {
            get
            {
                return this._LastName;
            }
            set
            {
                this._LastName = value;
            }
        }
        [XmlElement(ElementName = "RoleLabelID")]
        public virtual System.Int32? RoleLabelID
        {
            get
            {
                return this._RoleLabelID;
            }
            set
            {
                this._RoleLabelID = value;
            }
        }
        [XmlElement(ElementName = "Role")]
        public virtual System.String Role
        {
            get
            {
                return this._Role;
            }
            set
            {
                this._Role = value;
            }
        }
        #endregion

        protected System.Int64? _AccountID = null;
        protected System.String _AccountName = "";
        protected System.Int32? _AccountNameLabelID = null;
        protected System.String _AccountNumber = "";
        protected System.Int16? _AccountTypeID = null;

        protected System.Int32? _FSCaptionID = null;
        protected System.Boolean? _IsKeyAccount = null ;
        protected System.Boolean? _IsZeroBalance = null ;
        protected System.Int16? _RiskRatingID = null;
        protected System.DateTime? _PeriodEndDate = null;

        #region NetAccount

        protected System.Int32? _NetAccountID = null;
        [XmlElement(ElementName = "NetAccountID")]
        public virtual System.Int32? NetAccountID
        {
            get
            {
                return this._NetAccountID;
            }
            set
            {
                this._NetAccountID = value;
            }
        }

        protected System.Int32? _NetAccountLabelID = null;
        [XmlElement(ElementName = "NetAccountLabelID")]
        public virtual System.Int32? NetAccountLabelID
        {
            get
            {
                return this._NetAccountLabelID;
            }
            set
            {
                this._NetAccountLabelID = value;
            }
        }

        protected System.String _NetAccount = "";
        [XmlElement(ElementName = "NetAccount")]
        public virtual System.String NetAccount
        {
            get
            {
                return this._NetAccount;
            }
            set
            {
                this._NetAccount = value;
            }
        }

        #endregion


        [XmlElement(ElementName = "PeriodEndDate")]
        public virtual System.DateTime? PeriodEndDate
        {
            get
            {
                return this._PeriodEndDate;
            }
            set
            {
                this._PeriodEndDate = value;
            }
        }

        protected System.Int64? _GLDataID = null;
        [XmlElement(ElementName = "GLDataID")]
        public virtual System.Int64? GLDataID
        {
            get
            {
                return this._GLDataID;
            }
            set
            {
                this._GLDataID = value;
            }
        }


        protected System.Int64? _RecItemID = null;
        [XmlElement(ElementName = "RecItemID")]
        public virtual System.Int64? RecItemID
        {
            get
            {
                return this._RecItemID;
            }
            set
            {
                this._RecItemID = value;
            }
        }


        protected System.Decimal? _RecItemAmountReportingCurrency = null;
        [XmlElement(ElementName = "RecItemAmountReportingCurrency")]
        public virtual System.Decimal? RecItemAmountReportingCurrency
        {
            get
            {
                return this._RecItemAmountReportingCurrency;
            }
            set
            {
                this._RecItemAmountReportingCurrency = value;
            }
        }

        protected System.Decimal? _RecItemAmountBaseCurrency = null;
        [XmlElement(ElementName = "RecItemAmountBaseCurrency")]
        public virtual System.Decimal? RecItemAmountBaseCurrency
        {
            get
            {
                return this._RecItemAmountBaseCurrency;
            }
            set
            {
                this._RecItemAmountBaseCurrency = value;
            }
        }

        protected System.Int16? _AgingInDays = null;
        [XmlElement(ElementName = "AgingInDays")]
        public virtual System.Int16? AgingInDays
        {
            get
            {
                return this._AgingInDays;
            }
            set
            {
                this._AgingInDays = value;
            }
        }


        protected System.DateTime ? _OpenDate = null;
        [XmlElement(ElementName = "OpenDate")]
        public virtual System.DateTime? OpenDate
        {
            get
            {
                return this._OpenDate;
            }
            set
            {
                this._OpenDate = value;
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


        protected System.Int16? _OpenItemClassificationID = null;
        [XmlElement(ElementName = "OpenItemClassificationID")]
        public virtual System.Int16? OpenItemClassificationID
        {
            get
            {
                return this._OpenItemClassificationID;
            }
            set
            {
                this._OpenItemClassificationID = value;
            }
        }

        protected System.Int32? _OpenItemClassificationLabelID = null;
        [XmlElement(ElementName = "OpenItemClassificationLabelID")]
        public virtual System.Int32? OpenItemClassificationLabelID
        {
            get
            {
                return this._OpenItemClassificationLabelID;
            }
            set
            {
                this._OpenItemClassificationLabelID = value;
            }
        }


        protected System.String _OpenItemClassification = "";
        [XmlElement(ElementName = "OpenItemClassification")]
        public virtual System.String OpenItemClassification
        {
            get
            {
                return this._OpenItemClassification;
            }
            set
            {
                this._OpenItemClassification = value;
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

        private string _RecItemNumber = null;

        [XmlElement(ElementName = "RecItemNumber")]
        public string RecItemNumber
        {
            get { return _RecItemNumber; }
            set { _RecItemNumber = value; }
        }
        //BCCY Changes
        protected string _baseCurrencyCode;
        [XmlElement(ElementName = "BaseCurrencyCode")]
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
