using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Client.Model.Base;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model.Report
{
    [Serializable]
    public class QualityScoreReportInfo : OrganizationalHierarchyInfo
    {
        protected System.Int64? _GLDataID = null;
        protected System.Int64? _AccountID = null;
        protected System.String _AccountName = "";
        protected System.Int32? _AccountNameLabelID = null;
        protected System.String _AccountNumber = "";
        protected System.Int16? _AccountTypeID = null;
        protected System.Int32? _FSCaptionID = null;
        protected System.Boolean? _IsKeyAccount = null;
        protected System.Boolean? _IsZeroBalance = null;
        protected System.Int16? _RiskRatingID = null;
        protected System.Decimal? _GLBalanceReportingCurrency = null;
        protected System.Int16? _SystemQualityScoreStatusID = null;
        protected System.Int16? _UserQualityScoreStatusID = null;
        protected System.String _QualityScoreDesc = "";
        protected System.Int16? _SystemQualityScore = null;
        protected System.Int16? _UserQualityScore = null;
        protected System.String _Comments = "";
        protected System.String _QualityScoreNumber= "";
        protected System.Int32? _QualityScoreDescLabelID = null;
        protected System.Int32? _QualityScoreID = null;

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

        [XmlElement(ElementName = "Comments ")]
        public virtual System.String Comments 
        {
            get
            {
                return this._Comments;
            }
            set
            {
                this._Comments = value;
            }
        }

        [XmlElement(ElementName = "QualityScoreNumber")]
        public virtual System.String QualityScoreNumber
        {
            get
            {
                return this._QualityScoreNumber;
            }
            set
            {
                this._QualityScoreNumber = value;
            }
        }

        [XmlElement(ElementName = "SystemQualityScore")]
        public virtual System.Int16? SystemQualityScore
        {
            get
            {
                return this._SystemQualityScore;
            }
            set
            {
                this._SystemQualityScore = value;
            }
        }

        [XmlElement(ElementName = "UserQualityScore")]
        public virtual System.Int16? UserQualityScore
        {
            get
            {
                return this._UserQualityScore;
            }
            set
            {
                this._UserQualityScore = value;
            }
        }


        [XmlElement(ElementName = "QualityScoreDesc")]
        public virtual System.String QualityScoreDesc
        {
            get
            {
                return this._QualityScoreDesc;
            }
            set
            {
                this._QualityScoreDesc = value;
            }
        }

        [XmlElement(ElementName = "SystemQualityScoreStatusID")]
        public virtual System.Int16? SystemQualityScoreStatusID
        {
            get
            {
                return this._SystemQualityScoreStatusID;
            }
            set
            {
                this._SystemQualityScoreStatusID = value;
            }
        }

        [XmlElement(ElementName = "UserQualityScoreStatusID")]
        public virtual System.Int16? UserQualityScoreStatusID
        {
            get
            {
                return this._UserQualityScoreStatusID;
            }
            set
            {
                this._UserQualityScoreStatusID = value;
            }
        }

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
        [XmlElement(ElementName = "QualityScoreDescLabelID")]
        public virtual System.Int32? QualityScoreDescLabelID
        {
            get
            {
                return this._QualityScoreDescLabelID;
            }
            set
            {
                this._QualityScoreDescLabelID = value;
            }
        }       
        [XmlElement(ElementName = "QualityScoreID")]
        public virtual System.Int32? QualityScoreID
        {
            get { return this._QualityScoreID; }
            set { this._QualityScoreID = value; }
        }			
    }
}
