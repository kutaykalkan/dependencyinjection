using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using SkyStem.ART.Client.Data;

namespace SkyStem.ART.Client.Model.Report
{

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ExceptionStatusReportInfo : OrganizationalHierarchyInfo
    {
        protected System.Int64? _AccountID = null;
        protected System.String _AccountName = "";
        protected System.Int32? _AccountNameLabelID = null;
        protected System.String _AccountNumber = "";
        protected System.Boolean? _IsKeyAccount = null ;
        protected System.Boolean? _IsMaterial = null ;
        protected System.Int16? _RiskRatingID = null;
        protected System.String _Aging = null;
        protected System.Int16? _AgingDays = null;
        protected System.Decimal? _WriteOnOffReportingCurrency = null;
        protected System.Decimal? _UnexpVarReportingCurrency = null;
        protected System.Decimal? _DelinquentAmountReportingCurrency = null;
        protected bool? _IsDueDatePast = null;
        

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

        [XmlElement(ElementName = "WriteOnOffReportingCurrency")]
        public virtual System.Decimal? WriteOnOffReportingCurrency
        {
            get
            {
                return this._WriteOnOffReportingCurrency;
            }
            set
            {
                this._WriteOnOffReportingCurrency = value;
            }
        }

        [XmlElement(ElementName = "UnexpVarReportingCurrency")]
        public virtual System.Decimal? UnexpVarReportingCurrency
        {
            get
            {
                return this._UnexpVarReportingCurrency;
            }
            set
            {
                this._UnexpVarReportingCurrency = value;
            }
        }

        [XmlElement(ElementName = "_DelinquentAmountReportingCurrency")]
        public virtual System.Decimal? DelinquentAmountReportingCurrency
        {
            get
            {
                return this._DelinquentAmountReportingCurrency;
            }
            set
            {
                this._DelinquentAmountReportingCurrency = value;
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

         [XmlElement(ElementName = "PreparerFullName")]
         public virtual System.String PreparerFullName
         {
             get
             {
                 return ModelHelper.GetFullName(this._PreparerFirstName, this._PreparerLastName);
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

        [XmlElement(ElementName = "Aging")]
        public virtual System.String Aging
        {
            get
            {
                return this._Aging;
            }
            set
            {
                this._Aging = value;
            }
        }

        [XmlElement(ElementName = "AgingDays")]
        public virtual System.Int16? AgingDays
        {
            get
            {
                return this._AgingDays;
            }
            set
            {
                this._AgingDays = value;
            }
        }

        [XmlElement(ElementName = "IsDueDatePast")]
        public virtual bool? IsDueDatePast
        {
            get
            {
                return this._IsDueDatePast;
            }
            set
            {
                this._IsDueDatePast = value;
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
