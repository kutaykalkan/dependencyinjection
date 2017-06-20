
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using SkyStem.ART.Client.Data;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemArt GLReconciliationItemInput table
	/// </summary>
	[Serializable]
	[DataContract]
	public class GLDataRecItemInfo : GLDataRecItemInfoBase
	{
        protected System.Decimal? _CalculatedMonthlyAmount = null;
        public bool IsCalculatedMonthlyAmountNull = true;
        [XmlElement(ElementName = "CalculatedMonthlyAmount")]
        public virtual System.Decimal? CalculatedMonthlyAmount
        {
            get
            {
                return this._CalculatedMonthlyAmount;
            }
            set
            {
                this._CalculatedMonthlyAmount = value;

                this.IsCalculatedMonthlyAmountNull = false;
            }
        }
        protected System.String _PhysicalPath = "";
        [XmlElement(ElementName = "PhysicalPath")]
        public virtual System.String PhysicalPath
        {
            get
            {
                return this._PhysicalPath;
            }
            set
            {
                this._PhysicalPath = value;
            }
        }


        protected string _ImportName;
        protected string _Documents;
        protected int _Aging;
        public System.String ImportName
        {
            get
            {
                return this._ImportName;
            }
            set
            {
                this._ImportName = value;

            }
        }
        public System.String Documents
        {
            get
            {
                return this._Documents;
            }
            set
            {
                this._Documents = value;

            }
        }

        public int Aging {get; set;}

        //TODO: Remove following Accrual Schedule properties.
        //Properties for Accrual Schedule have been defined here for demo purpose only.
        protected System.Decimal? _Balance = 0.00M;
        protected System.DateTime? _BeginDate = DateTime.Now;
        protected System.DateTime? _EndDate = DateTime.Now;
        protected System.Int32? _RecurringItemScheduleID = 0;
        protected System.String _ScheduleName = "";

        protected System.Int16? _RecordSourceTypeID = 0;
        protected System.Int64? _RecordSourceID = 0;
        protected System.Int64? _ExcelRowNumber = 0;
        protected System.Int64? _MatchSetMatchingSourceDataImportID = 0;
        protected System.Int16? _IndexID = 0;
        protected string _MatchSetRefNumber = null;
        protected System.Int64? _MatchSetID = null;
        protected System.Int64? _MatchSetSubSetCombinationID = null;
        protected System.Int16? _GLDataRecItemTypeID = 0;




        public bool IsBalanceNull = true;
        public bool IsBeginDateNull = true;
        public bool IsEndDateNull = true;
        public bool IsRecurringItemScheduleIDNull = true;
        public bool IsScheduleNameNull = true;


        [XmlElement(ElementName = "Balance")]
        public virtual System.Decimal? Balance
        {
            get
            {
                return this._Balance;
            }
            set
            {
                this._Balance = value;

                this.IsBalanceNull = false;
            }
        }

        [XmlElement(ElementName = "BeginDate")]
        public virtual System.DateTime? BeginDate
        {
            get
            {
                return this._BeginDate;
            }
            set
            {
                this._BeginDate = value;

                this.IsBeginDateNull = (_BeginDate == DateTime.MinValue);
            }
        }

        [XmlElement(ElementName = "EndDate")]
        public virtual System.DateTime? EndDate
        {
            get
            {
                return this._EndDate;
            }
            set
            {
                this._EndDate = value;

                this.IsEndDateNull = (_EndDate == DateTime.MinValue);
            }
        }

        [XmlElement(ElementName = "RecurringItemScheduleID")]
        public virtual System.Int32? RecurringItemScheduleID
        {
            get
            {
                return this._RecurringItemScheduleID;
            }
            set
            {
                this._RecurringItemScheduleID = value;

                this.IsRecurringItemScheduleIDNull = false;
            }
        }

        [XmlElement(ElementName = "ScheduleName")]
        public virtual System.String ScheduleName
        {
            get
            {
                return this._ScheduleName;
            }
            set
            {
                this._ScheduleName = value;

                this.IsScheduleNameNull = (_ScheduleName == null);
            }
        }

        private int? _AttachmentCount = 0;

        public bool IsAttachmentCountNull = true; 

        [XmlElement(ElementName = "AttachmentCount")]
        public virtual System.Int32? AttachmentCount
        {
            get
            {
                return this._AttachmentCount;
            }
            set
            {
                this._AttachmentCount = value;

                this.IsAttachmentCountNull = (_AttachmentCount == null);
            }
        }

        private bool? _IsForwardedItem = false;

        [XmlElement(ElementName="IsForwardedItem")]
        public System.Boolean? IsForwardedItem
        { 
            get
            {
                return this._IsForwardedItem;
            }
            set
            {
                this._IsForwardedItem = value;
            }
        }

        private string _UserName = "";

        [XmlElement(ElementName="UserName")]
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

        protected System.Decimal? _AccruedAmountBaseCurrency = null;
        public bool IsAccruedAmountBaseCurrencyNull = true;
        [XmlElement(ElementName = "AccruedAmountBaseCurrency")]
        public virtual System.Decimal? AccruedAmountBaseCurrency
        {
            get
            {
                return this._AccruedAmountBaseCurrency;
            }
            set
            {
                this._AccruedAmountBaseCurrency = value;

                this.IsAccruedAmountBaseCurrencyNull = false;
            }
        }

        protected System.Decimal? _AccruedAmountReportingCurrency = null;
        public bool IsAccruedAmountReportingCurrencyNull = true;
        [XmlElement(ElementName = "AccruedAmountReportingCurrency")]
        public virtual System.Decimal? AccruedAmountReportingCurrency
        {
            get
            {
                return this._AccruedAmountReportingCurrency;
            }
            set
            {
                this._AccruedAmountReportingCurrency = value;

                this.IsAccruedAmountReportingCurrencyNull = false;
            }
        }

        protected System.Boolean ? _IsScheduleItem = null;
        public bool IsIsScheduleItemNull = true;
        [XmlElement(ElementName = "IsScheduleItem")]
        public virtual System.Boolean? IsScheduleItem
        {
            get
            {
                return this._IsScheduleItem;
            }
            set
            {
                this._IsScheduleItem = value;

                this.IsIsScheduleItemNull = false;
            }
        }

        private string _RecItemNumber = null;

        [XmlElement(ElementName = "RecItemNumber")]
        public string RecItemNumber
        {
            get { return _RecItemNumber; }
            set { _RecItemNumber = value; }
        }


        [XmlElement(ElementName = "RecordSourceTypeID")]
        public virtual System.Int16? RecordSourceTypeID
        {
            get
            {
                return this._RecordSourceTypeID;
            }
            set
            {
                this._RecordSourceTypeID = value;

               
            }
        }

        [XmlElement(ElementName = "RecordSourceID")]
        public virtual System.Int64? RecordSourceID
        {
            get
            {
                return this._RecordSourceID;
            }
            set
            {
                this._RecordSourceID = value;


            }
        }

        [XmlElement(ElementName = "ExcelRowNumber")]
        public virtual System.Int64? ExcelRowNumber
        {
            get
            {
                return this._ExcelRowNumber;
            }
            set
            {
                this._ExcelRowNumber = value;


            }
        }

        [XmlElement(ElementName = "MatchSetMatchingSourceDataImportID")]
        public virtual System.Int64? MatchSetMatchingSourceDataImportID
        {
            get
            {
                return this._MatchSetMatchingSourceDataImportID;
            }
            set
            {
                this._MatchSetMatchingSourceDataImportID = value;


            }
        }
        [XmlElement(ElementName = "IndexID")]
        public virtual System.Int16? IndexID
        {
            get
            {
                return this._IndexID;
            }
            set
            {
                this._IndexID = value;


            }
        }

        protected System.String _DataSourceName = "";
        [XmlElement(ElementName = "DataSourceName")]
        public virtual System.String DataSourceName
        {
            get
            {
                return this._DataSourceName;
            }
            set
            {
                this._DataSourceName = value;               
            }
        }

        [XmlElement(ElementName = "MatchSetRefNumber")]
        public string MatchSetRefNumber
        {
            get { return _MatchSetRefNumber; }
            set { _MatchSetRefNumber = value; }
        }


        [XmlElement(ElementName = "MatchSetID")]
        public System.Int64? MatchSetID
        {
            get { return _MatchSetID; }
            set { _MatchSetID = value; }
        }


        [XmlElement(ElementName = "MatchSetSubSetCombinationID")]
        public System.Int64? MatchSetSubSetCombinationID
        {
            get { return _MatchSetSubSetCombinationID; }
            set { _MatchSetSubSetCombinationID = value; }
        }

        [XmlElement(ElementName = "GLDataRecItemTypeID")]
        public virtual System.Int16? GLDataRecItemTypeID
        {
            get
            {
                return this._GLDataRecItemTypeID;
            }
            set
            {
                this._GLDataRecItemTypeID = value;
            }
        }
        [DataMember]
        [XmlElement(ElementName = "ExRateLCCYtoBCCY")]
        public System.Decimal? ExRateLCCYtoBCCY { get; set; }

        [DataMember]
        [XmlElement(ElementName = "ExRateLCCYtoRCCY")]
        public System.Decimal? ExRateLCCYtoRCCY { get; set; }
        [DataMember]
        [XmlElement(ElementName = "IsCommentAvailable")]
        public bool? IsCommentAvailable { get; set; }

        [DataMember]
        [XmlElement(ElementName = "DataImportTypeID")]
        public short? DataImportTypeID { get; set; }
    }
}
