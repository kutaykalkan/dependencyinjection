using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model.Report
{
    [Serializable]
    [DataContract]
    public class ReportSearchCriteria
    {
        private short _ReportID;
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
        //private string _UserName;
        //private string _AccountName;
        private bool? _IsKeyccount;
        private bool? _IsZeroBalanceAccount;
        private bool? _ExcludeNetAccount;
        private int _Count;
        private int _ReconciliationPeriodID;
        private bool _IsRiskRatingEnabled;
	    private bool _IsDualReviewEnabled;
	    private bool _IsKeyAccountEnabled;
	    private bool _IsZeroBalanceAccountEnabled;
        private bool _IsDueDateByAccountEnabled;
        //private string _SortExpression;
        //private string _SortDirection;
        private int _LCID;
        private int _BusinessEntityID;
        private int _DefaultLanguageID;
        private int _RequesterUserID;
        private short _RequesterRoleID;

        private string _FromSystemScore;
        private string _ToSystemScore;

        private string _FromUserScore;
        private string _ToUserScore;


        /// <summary>
        /// Starting Range of User Score
        /// </summary>
        [XmlElement(ElementName = "FromUserScore")]
        public string FromUserScore
        {
            get
            {
                return this._FromUserScore;
            }
            set
            {
                this._FromUserScore = value;
            }
        }

        /// <summary>
        /// Last Range of User Score
        /// </summary>
        [XmlElement(ElementName = "ToUserScore")]
        public string ToUserScore
        {
            get
            {
                return this._ToUserScore;
            }
            set
            {
                this._ToUserScore = value;
            }
        }

        /// <summary>
        /// Starting Range of System Score
        /// </summary>
        [XmlElement(ElementName = "FromSystemScore")]
        public string FromSystemScore
        {
            get
            {
                return this._FromSystemScore;
            }
            set
            {
                this._FromSystemScore = value;
            }
        }

        /// <summary>
        /// Last Range of System Score
        /// </summary>
        [XmlElement(ElementName = "ToSystemScore")]
        public string ToSystemScore
        {
            get
            {
                return this._ToSystemScore;
            }
            set
            {
                this._ToSystemScore = value;
            }
        }

        private DateTime ? _FromOpenDate;
        [XmlElement(ElementName = "FromOpenDate")]
        public DateTime? FromOpenDate
        {
            get
            {
                return this._FromOpenDate;
            }
            set
            {
                this._FromOpenDate = value;
            }
        }
        private DateTime? _ToOpenDate;
        [XmlElement(ElementName = "ToOpenDate")]
        public DateTime? ToOpenDate
        {
            get
            {
                return this._ToOpenDate;
            }
            set
            {
                this._ToOpenDate = value;
            }
        }

        
                   
         private bool? _IsMaterialAccount;
        /// <summary>
        /// Specifies if IsMaterialAccount
        /// </summary>
        [XmlElement(ElementName = "IsMaterialAccount")]
        public bool? IsMaterialAccount 
        {
            get
            {
                return this._IsMaterialAccount;
            }
            set
            {
                this._IsMaterialAccount = value;
            }
        }


         private bool _IsRequesterUserIDToBeConsideredForPermission;
        /// <summary>
        /// Specifies if IsRequesterUserIDToBeConsideredForPermission in SPs
        /// </summary>
        [XmlElement(ElementName = "IsRequesterUserIDToBeConsideredForPermission")]
        public bool IsRequesterUserIDToBeConsideredForPermission 
        {
            get
            {
                return this._IsRequesterUserIDToBeConsideredForPermission;
            }
            set
            {
                this._IsRequesterUserIDToBeConsideredForPermission = value;
            }
        }
        private string _RiskRatingIDs;
        /// <summary>
        /// ',' separated RiskRatingIDs
        /// </summary>
        [XmlElement(ElementName = "RiskRatingIDs")]
        public string RiskRatingIDs
        {
            get
            {
                return this._RiskRatingIDs;
            }
            set
            {
                this._RiskRatingIDs = value;
            }
        }

        
        private string _ReasonCodeIDs;
        /// <summary>
        /// ',' separated ReasonCodeIDs
        /// </summary>
        [XmlElement(ElementName = "ReasonCodeIDs")]
        public string ReasonCodeIDs
        {
            get
            {
                return this._ReasonCodeIDs;
            }
            set
            {
                this._ReasonCodeIDs = value;
            }
        }

        private string _ExceptionTypeIDs;
        /// <summary>
        /// ',' separated ExceptionTypeIDs
        /// </summary>
        [XmlElement(ElementName = "ExceptionTypeIDs")]
        public string ExceptionTypeIDs
        {
            get
            {
                return this._ExceptionTypeIDs;
            }
            set
            {
                this._ExceptionTypeIDs = value;
            }
        }

        private string _ReconciliationStatusIDs;
        /// <summary>
        /// ',' separated ReconciliationStatusIDs
        /// </summary>
        [XmlElement(ElementName = "ReconciliationStatusIDs")]
        public string ReconciliationStatusIDs
        {
            get
            {
                return this._ReconciliationStatusIDs;
            }
            set
            {
                this._ReconciliationStatusIDs = value;
            }
        }

        private string _AgingIDs;
        /// <summary>
        /// ',' separated AgingIDs
        /// </summary>
        [XmlElement(ElementName = "AgingIDs")]
        public string AgingIDs
        {
            get
            {
                return this._AgingIDs;
            }
            set
            {
                this._AgingIDs = value;
            }
        }

        private string _OpenItemClassificationIDs;
        /// <summary>
        /// ',' separated OpenItemClassificationIDs
        /// </summary>
        [XmlElement(ElementName = "OpenItemClassificationIDs")]
        public string OpenItemClassificationIDs
        {
            get
            {
                return this._OpenItemClassificationIDs;
            }
            set
            {
                this._OpenItemClassificationIDs = value;
            }
        }



        [XmlElement(ElementName = "ReportID")]
        public short ReportID
        {
            get
            {
                return this._ReportID;
            }
            set
            {
                this._ReportID = value;
            }
        }

        private string _QualityScoreIDs;
        /// <summary>
        /// ',' separated QualityScoreIDs
        /// </summary>
        [XmlElement(ElementName = "QualityScoreIDs")]
        public string QualityScoreIDs
        {
            get
            {
                return this._QualityScoreIDs;
            }
            set
            {
                this._QualityScoreIDs = value;
            }
        }

        private string _QualityScoreRange;
        /// <summary>
        /// ',' separated QualityScoreRange
        /// </summary>
        [XmlElement(ElementName = "QualityScoreRange")]
        public string QualityScoreRange
        {
            get
            {
                return this._QualityScoreRange;
            }
            set
            {
                this._QualityScoreRange = value;
            }
        }














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

        ///// <summary>
        ///// Name of User who is associated with the account
        ///// </summary>
        //[XmlElement(ElementName = "UserName")]
        //public string UserName
        //{
        //    get
        //    {
        //        return this._UserName;
        //    }
        //    set
        //    {
        //        this._UserName = value;
        //    }
        //}

        ///// <summary>
        ///// Name of the account
        ///// </summary>
        //[XmlElement(ElementName = "AccountName")]
        //public string AccountName
        //{
        //    get
        //    {
        //        return this._AccountName;
        //    }
        //    set
        //    {
        //        this._AccountName = value;
        //    }
        //}

        /// <summary>
        /// Specifies if the account is key account or not.
        /// Null if all accounts are to be searched
        /// </summary>
        [XmlElement(ElementName="IsKeyccount")]
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
        [XmlElement(ElementName="IsZeroBalanceAccount")]
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

        [XmlElement(ElementName="ExcludeNetAccount")]
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
        [XmlElement(ElementName="ReconciliationPeriodID")]
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

        [XmlElement(ElementName = "IsDueDateByAccountEnabled")]
        public bool IsDueDateByAccountEnabled
        {
            get
            {
                return this._IsDueDateByAccountEnabled;
            }
            set
            {
                this._IsDueDateByAccountEnabled = value;
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

        [XmlElement(ElementName = "RequesterUserID")]
        public int RequesterUserID
        {
            get
            {
                return this._RequesterUserID;
            }
            set
            {
                this._RequesterUserID = value;
            }
        }

        [XmlElement(ElementName = "RequesterRoleID")]
        public short RequesterRoleID
        {
            get
            {
                return this._RequesterRoleID;
            }
            set
            {
                this._RequesterRoleID = value;
            }
        }

        private DateTime? _AsOnDate;
        [XmlElement(ElementName = "AsOnDate")]
        public DateTime? AsOnDate
        {
            get
            {
                return this._AsOnDate;
            }
            set
            {
                this._AsOnDate = value;
            }
        }

        [XmlElement(ElementName = "FromDateCreated")]
        public DateTime? FromDateCreated { get; set; }

        [XmlElement(ElementName = "ToDateCreated")]
        public DateTime? ToDateCreated { get; set; }

        [XmlElement(ElementName = "FromChangeDate")]
        public DateTime? FromChangeDate { get; set; }

        [XmlElement(ElementName = "ToChangeDate")]
        public DateTime? ToChangeDate { get; set; }


    }
}
