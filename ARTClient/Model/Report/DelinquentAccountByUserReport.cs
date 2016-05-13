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
    public class DelinquentAccountByUserReportInfo : OrganizationalHierarchyInfo
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



        protected System.DateTime ? _DueDate = null;
        [XmlElement(ElementName = "DueDate")]
        public virtual System.DateTime? DueDate
        {
            get
            {
                return this._DueDate;
            }
            set
            {
                this._DueDate = value;
            }
        }


        protected System.Int32 ? _DaysLate = null;
        [XmlElement(ElementName = "DaysLate")]
        public virtual System.Int32? DaysLate
        {
            get
            {
                return this._DaysLate;
            }
            set
            {
                this._DaysLate = value;
            }
        }


        protected System.Int32? _CountDelinquentAccount = null;
        [XmlElement(ElementName = "CountDelinquentAccount")]
        public virtual System.Int32? CountDelinquentAccount
        {
            get
            {
                return this._CountDelinquentAccount;
            }
            set
            {
                this._CountDelinquentAccount = value;
            }
        }








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
