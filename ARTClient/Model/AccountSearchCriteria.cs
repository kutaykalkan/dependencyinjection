using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{
    [Serializable]
    [DataContract]
    public class AccountSearchCriteria
    {
        private int? _CompanyID;
        private int? _Key2;
        private int? _Key3;
        private int? _Key4;
        private int? _Key5;
        private int? _Key6;
        private int? _Key7;
        private int? _Key8;
        private int? _Key9;
        private string _Key2Value;
        private string _Key3Value;
        private string _Key4Value;
        private string _Key5Value;
        private string _Key6Value;
        private string _Key7Value;
        private string _Key8Value;
        private string _Key9Value;
        private string _FromAccountNumber;
        private string _ToAccountNumber;
        private int? _RiskRatingID;
        private string _UserName;
        private string _AccountName;
        private bool? _IsKeyccount;
        private bool? _IsZeroBalanceAccount;
        private bool? _ExcludeNetAccount;
        private bool _IsShowOnlyAccountMissingAttributes;
        private string _FSCaption;
        private int _Count;
        private int _ReconciliationPeriodID;
        private bool _IsRiskRatingEnabled;
        private bool _IsDualReviewEnabled;
        private bool _IsKeyAccountEnabled;
        private bool _IsZeroBalanceAccountEnabled;
        private int _PageID;
        private string _SortExpression;
        private string _SortDirection;
        private int _LCID;
        private int _BusinessEntityID;
        private int _DefaultLanguageID;
        private int _UserID;
        private short _UserRoleID;
        private bool? _IsReconcilable;
        private bool _IsReconcilableEnabled;


        /// <summary>
        /// Unique identifier of a company
        /// </summary>
        [XmlElement(ElementName = "CompanyID")]
        public int? CompanyID
        {
            get
            {
                return this._CompanyID;
            }
            set
            {
                this._CompanyID = value;
            }
        }

        /// <summary>
        /// organizational hierarchy Key2
        /// </summary>
        [XmlElement(ElementName = "Key2")]
        public int? Key2
        {
            get
            {
                return this._Key2;
            }
            set
            {
                this._Key2 = value;
            }
        }

        /// <summary>
        /// organizational hierarchy Key3
        /// </summary>
        [XmlElement(ElementName = "Key3")]
        public int? Key3
        {
            get
            {
                return this._Key3;
            }
            set
            {
                this._Key3 = value;
            }
        }

        /// <summary>
        /// organizational hierarchy Key4
        /// </summary>
        [XmlElement(ElementName = "Key4")]
        public int? Key4
        {
            get
            {
                return this._Key4;
            }
            set
            {
                this._Key4 = value;
            }
        }

        /// <summary>
        /// organizational hierarchy Key5
        /// </summary>
        [XmlElement(ElementName = "Key5")]
        public int? Key5
        {
            get
            {
                return this._Key5;
            }
            set
            {
                this._Key5 = value;
            }
        }

        /// <summary>
        /// organizational hierarchy Key6
        /// </summary>
        [XmlElement(ElementName = "Key6")]
        public int? Key6
        {
            get
            {
                return this._Key6;
            }
            set
            {
                this._Key6 = value;
            }
        }

        /// <summary>
        /// organizational hierarchy Key7
        /// </summary>
        [XmlElement(ElementName = "Key7")]
        public int? Key7
        {
            get
            {
                return this._Key7;
            }
            set
            {
                this._Key7 = value;
            }
        }

        /// <summary>
        /// organizational hierarchy Key8
        /// </summary>
        [XmlElement(ElementName = "Key8")]
        public int? Key8
        {
            get
            {
                return this._Key8;
            }
            set
            {
                this._Key8 = value;
            }
        }

        /// <summary>
        /// organizational hierarchy Key9
        /// </summary>
        [XmlElement(ElementName = "Key9")]
        public int? Key9
        {
            get
            {
                return this._Key9;
            }
            set
            {
                this._Key9 = value;
            }
        }

        /// <summary>
        /// Value of organizational hierarchy Key2
        /// </summary>
        [XmlElement(ElementName = "Key2Value")]
        public string Key2Value
        {
            get
            {
                return this._Key2Value;
            }
            set
            {
                this._Key2Value = value;
            }
        }

        /// <summary>
        /// Value of organizational hierarchy Key3
        /// </summary>
        [XmlElement(ElementName = "Key3Value")]
        public string Key3Value
        {
            get
            {
                return this._Key3Value;
            }
            set
            {
                this._Key3Value = value;
            }
        }

        /// <summary>
        /// Value of organizational hierarchy Key4
        /// </summary>
        [XmlElement(ElementName = "Key4Value")]
        public string Key4Value
        {
            get
            {
                return this._Key4Value;
            }
            set
            {
                this._Key4Value = value;
            }
        }

        /// <summary>
        /// Value of organizational hierarchy Key5
        /// </summary>
        [XmlElement(ElementName = "Key5Value")]
        public string Key5Value
        {
            get
            {
                return this._Key5Value;
            }
            set
            {
                this._Key5Value = value;
            }
        }

        /// <summary>
        /// Value of organizational hierarchy Key6
        /// </summary>
        [XmlElement(ElementName = "Key6Value")]
        public string Key6Value
        {
            get
            {
                return this._Key6Value;
            }
            set
            {
                this._Key6Value = value;
            }
        }

        /// <summary>
        /// Value of organizational hierarchy Key7
        /// </summary>
        [XmlElement(ElementName = "Key7Value")]
        public string Key7Value
        {
            get
            {
                return this._Key7Value;
            }
            set
            {
                this._Key7Value = value;
            }
        }

        /// <summary>
        /// Value of organizational hierarchy Key8
        /// </summary>
        [XmlElement(ElementName = "Key8Value")]
        public string Key8Value
        {
            get
            {
                return this._Key8Value;
            }
            set
            {
                this._Key8Value = value;
            }
        }

        /// <summary>
        /// Value of organizational hierarchy Key9
        /// </summary>
        [XmlElement(ElementName = "Key9Value")]
        public string Key9Value
        {
            get
            {
                return this._Key9Value;
            }
            set
            {
                this._Key9Value = value;
            }
        }

        /// <summary>
        /// Starting Range of account number
        /// </summary>
        [XmlElement(ElementName = "FromAccountNumber")]
        public string FromAccountNumber
        {
            get
            {
                return this._FromAccountNumber;
            }
            set
            {
                this._FromAccountNumber = value;
            }
        }

        /// <summary>
        /// Last Range of account number
        /// </summary>
        [XmlElement(ElementName = "ToAccountNumber")]
        public string ToAccountNumber
        {
            get
            {
                return this._ToAccountNumber;
            }
            set
            {
                this._ToAccountNumber = value;
            }
        }

        /// <summary>
        /// Unique identifier for Risk rating
        /// </summary>
        [XmlElement(ElementName = "RiskRatingID")]
        public int? RiskRatingID
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

        /// <summary>
        /// Name of User who is associated with the account
        /// </summary>
        [XmlElement(ElementName = "UserName")]
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                this._UserName = value;
            }
        }

        /// <summary>
        /// Name of the account
        /// </summary>
        [XmlElement(ElementName = "AccountName")]
        public string AccountName
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

        /// <summary>
        /// Specifies if the account is key account or not.
        /// Null if all accounts are to be searched
        /// </summary>
        [XmlElement(ElementName = "IsKeyccount")]
        public bool? IsKeyccount
        {
            get
            {
                return this._IsKeyccount;
            }
            set
            {
                this._IsKeyccount = value;
            }
        }

        /// <summary>
        /// Specifies if the account is Zero balance account
        /// Null if all accounts are to be searched
        /// </summary>
        [XmlElement(ElementName = "IsZeroBalanceAccount")]
        public bool? IsZeroBalanceAccount
        {
            get
            {
                return this._IsZeroBalanceAccount;
            }
            set
            {
                this._IsZeroBalanceAccount = value;
            }
        }

        [XmlElement(ElementName = "ExcludeNetAccount")]
        public bool? ExcludeNetAccount
        {
            get
            {
                return this._ExcludeNetAccount;
            }
            set
            {
                this._ExcludeNetAccount = value;
            }
        }

        /// <summary>
        /// Specifies if the search should show only those accounts which have one or 
        /// many missing attributes
        /// </summary>
        [XmlElement(ElementName = "IsShowOnlyAccountMissingAttributes")]
        public bool IsShowOnlyAccountMissingAttributes
        {
            get
            {
                return this._IsShowOnlyAccountMissingAttributes;
            }
            set
            {
                this._IsShowOnlyAccountMissingAttributes = value;
            }
        }

        /// <summary>
        /// FS Caption for Organizational Hierarchy
        /// </summary>
        [XmlElement(ElementName = "FSCaption")]
        public string FSCaption
        {
            get
            {
                return _FSCaption;
            }
            set
            {
                _FSCaption = value;
            }
        }

        /// <summary>
        /// Number of records to be fetched from database
        /// </summary>
        [XmlElement(ElementName = "Count")]
        public int Count
        {
            get
            {
                return _Count;
            }
            set
            {
                _Count = value;
            }
        }

        /// <summary>
        /// Reconciliation period selected by user
        /// </summary>
        [XmlElement(ElementName = "ReconciliationPeriodID")]
        public int ReconciliationPeriodID
        {
            get
            {
                return _ReconciliationPeriodID;
            }
            set
            {
                _ReconciliationPeriodID = value;
            }
        }

        /// <summary>
        /// Specifies if Risk rating capability is enabled for the current company
        /// </summary>
        [XmlElement(ElementName = "IsRiskRatingEnabled")]
        public bool IsRiskRatingEnabled
        {
            get
            {
                return this._IsRiskRatingEnabled;
            }
            set
            {
                this._IsRiskRatingEnabled = value;
            }
        }

        /// <summary>
        /// Specifies if Dual Review capability is enabled for the current company
        /// </summary>
        [XmlElement(ElementName = "IsDualReviewEnabled")]
        public bool IsDualReviewEnabled
        {
            get
            {
                return this._IsDualReviewEnabled;
            }
            set
            {
                this._IsDualReviewEnabled = value;
            }
        }

        /// <summary>
        /// Specifies if Key Account capability is enabled for the current company
        /// </summary>
        [XmlElement(ElementName = "IsKeyAccountEnabled")]
        public bool IsKeyAccountEnabled
        {
            get
            {
                return this._IsKeyAccountEnabled;
            }
            set
            {
                this._IsKeyAccountEnabled = value;
            }
        }

        /// <summary>
        /// Specifies if Zero Balance capability is enabled for the current company
        /// </summary>
        [XmlElement(ElementName = "IsZeroBalanceAccountEnabled")]
        public bool IsZeroBalanceAccountEnabled
        {
            get
            {
                return this._IsZeroBalanceAccountEnabled;
            }
            set
            {
                this._IsZeroBalanceAccountEnabled = value;
            }
        }

        /// <summary>
        /// Specifies the page id for which search is done
        /// </summary>
        [XmlElement(ElementName = "PageID")]
        public int PageID
        {
            get
            {
                return this._PageID;
            }
            set
            {
                this._PageID = value;
            }
        }

        /// <summary>
        /// Value of Sort Expression as given in the grid
        /// </summary>
        [XmlElement(ElementName = "SortExpression")]
        public string SortExpression
        {
            get
            {
                return this._SortExpression;
            }
            set
            {
                this._SortExpression = value;
            }
        }

        /// <summary>
        /// Value of Sort Direction as given in the grid
        /// </summary>
        [XmlElement(ElementName = "SortDirection")]
        public string SortDirection
        {
            get
            {
                return this._SortDirection;
            }
            set
            {
                this._SortDirection = value;
            }
        }

        [XmlElement(ElementName = "LCID ")]
        public int LCID
        {
            get
            {
                return this._LCID;
            }
            set
            {
                this._LCID = value;
            }
        }

        [XmlElement(ElementName = "BusinessEntityID")]
        public int BusinessEntityID
        {
            get
            {
                return this._BusinessEntityID;
            }
            set
            {
                this._BusinessEntityID = value;
            }
        }

        [XmlElement(ElementName = "DefaultLanguageID")]
        public int DefaultLanguageID
        {
            get
            {
                return this._DefaultLanguageID;
            }
            set
            {
                this._DefaultLanguageID = value;
            }
        }

        [XmlElement(ElementName = "UserID")]
        public int UserID
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

        [XmlElement(ElementName = "UserRoleID")]
        public short UserRoleID
        {
            get
            {
                return this._UserRoleID;
            }
            set
            {
                this._UserRoleID = value;
            }
        }

        [XmlElement(ElementName = "IsReconcilable")]
        public bool? IsReconcilable
        {
            get
            {
                return this._IsReconcilable;
            }
            set
            {
                this._IsReconcilable = value;
            }
        }

        [XmlElement(ElementName = "IsReconcilableEnabled")]
        public bool IsReconcilableEnabled
        {
            get
            {
                return this._IsReconcilableEnabled;
            }
            set
            {
                this._IsReconcilableEnabled = value;
            }
        }

        /// <summary>
        /// Specifies if the search should show only those accounts which have one or 
        /// many missing backup owners
        /// </summary>
        [XmlElement(ElementName = "IsShowOnlyAccountMissingBackupOwners")]
        public bool IsShowOnlyAccountMissingBackupOwners { get; set; }

        [XmlElement(ElementName = "FromDueDays")]
        public Int32? FromDueDays { get; set; }

        [XmlElement(ElementName = "ToDueDays")]
        public Int32? ToDueDays { get; set; }
    }
}
