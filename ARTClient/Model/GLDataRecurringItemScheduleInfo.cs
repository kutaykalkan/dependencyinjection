
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace SkyStem.ART.Client.Model
{

    /// <summary>
    /// An object representation of the SkyStemArt GLDataRecurringItemSchedule table
    /// </summary>
    [Serializable]
    [DataContract]
    public class GLDataRecurringItemScheduleInfo : GLDataRecurringItemScheduleInfoBase
    {
        private Int32? _PrevRecPeriodID = null;
        protected System.Int64? _ExcelRowNumber = 0;
        protected System.Int64? _MatchSetMatchingSourceDataImportID = 0;
        protected System.Int16? _IndexID = 0;
        protected string _MatchSetRefNumber = null;
        protected System.Int64? _MatchSetID = null;
        protected System.Int64? _MatchSetSubSetCombinationID = null;
        protected System.Int16? _GLDataRecItemTypeID = 0;
        protected System.Decimal? _CrrentRecPeriodAmount = null;

        // For amortizable schedule
        private int? _AttachmentCount = 0;
        public bool IsAttachmentCountNull = true;
        [DataMember]
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

        protected System.String _PhysicalPath = "";
        [DataMember]
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

        protected System.Decimal? _ScheduleAmountBaseCurrency = null;
        public bool IsScheduleAmountBaseCurrencyNull = true;
        [DataMember]
        [XmlElement(ElementName = "ScheduleAmountBaseCurrency")]
        public virtual System.Decimal? ScheduleAmountBaseCurrency
        {
            get
            {
                return this._ScheduleAmountBaseCurrency;
            }
            set
            {
                this._ScheduleAmountBaseCurrency = value;
                this.IsScheduleAmountBaseCurrencyNull = false;
            }
        }

        protected System.Decimal? _ScheduleAmountReportingCurrency = null;
        public bool IsScheduleAmountReportingCurrencyNull = true;
        [DataMember]
        [XmlElement(ElementName = "ScheduleAmountReportingCurrency")]
        public virtual System.Decimal? ScheduleAmountReportingCurrency
        {
            get
            {
                return this._ScheduleAmountReportingCurrency;
            }
            set
            {
                this._ScheduleAmountReportingCurrency = value;
                this.IsScheduleAmountReportingCurrencyNull = false;
            }
        }

        protected System.Int32? _CreatedInRecPeriodID = null;
        public bool IsCreatedInRecPeriodIDNull = true;
        [DataMember]
        [XmlElement(ElementName = "CreatedInRecPeriodID")]
        public virtual System.Int32? CreatedInRecPeriodID
        {
            get
            {
                return this._CreatedInRecPeriodID;
            }
            set
            {
                this._CreatedInRecPeriodID = value;

                this.IsCreatedInRecPeriodIDNull = false;
            }
        }

        protected System.String _AddedByFirstName = null;
        public bool IsAddedByFirstNameNull = true;
        [DataMember]
        [XmlElement(ElementName = "AddedByFirstName")]
        public virtual System.String AddedByFirstName
        {
            get
            {
                return this._AddedByFirstName;
            }
            set
            {
                this._AddedByFirstName = value;

                this.IsAddedByFirstNameNull = false;
            }
        }

        protected System.String _AddedByLastName = null;
        public bool IsAddedByLastNameNull = true;
        [DataMember]
        [XmlElement(ElementName = "AddedByLastName")]
        public virtual System.String AddedByLastName
        {
            get
            {
                return this._AddedByLastName;
            }
            set
            {
                this._AddedByLastName = value;

                this.IsAddedByLastNameNull = false;
            }
        }

        protected System.Int32? _DataImportID = null;
        [DataMember]
        [XmlElement(ElementName = "DataImportID")]
        public virtual System.Int32? DataImportID
        {
            get
            {
                return this._DataImportID;
            }
            set
            {
                this._DataImportID = value;
            }
        }
        protected System.Int16? _DataImportTypeID = null;
        [DataMember]
        [XmlElement(ElementName = "DataImportTypeID")]
        public virtual System.Int16? DataImportTypeID
        {
            get
            {
                return this._DataImportTypeID;
            }
            set
            {
                this._DataImportTypeID = value;
            }
        }
        private short? _RecCategoryTypeID;
        [DataMember]
        [XmlElement(ElementName = "RecCategoryTypeID")]
        public short? RecCategoryTypeID
        {
            get
            {
                return this._RecCategoryTypeID;
            }
            set
            {
                this._RecCategoryTypeID = value;
            }
        }

        [DataMember]
        [XmlElement(ElementName = "PrevRecPeriodID")]
        public virtual System.Int32? PrevRecPeriodID
        {
            get
            {
                return this._PrevRecPeriodID;
            }
            set
            {
                this._PrevRecPeriodID = value;
            }
        }

        private string _RecItemNumber = null;

        [DataMember]
        [XmlElement(ElementName = "RecItemNumber")]
        public string RecItemNumber
        {
            get { return _RecItemNumber; }
            set { _RecItemNumber = value; }
        }

        [DataMember]
        [XmlElement(ElementName = "CalculationFrequencyID")]
        public Int16? CalculationFrequencyID { get; set; }

        [DataMember]
        [XmlElement(ElementName = "TotalIntervals")]
        public Int32? TotalIntervals { get; set; }

        [DataMember]
        [XmlElement(ElementName = "CurrentInterval")]
        public Int32? CurrentInterval { get; set; }

        [DataMember]
        [XmlElement(ElementName = "StartIntervalRecPeriodID")]
        public Int32? StartIntervalRecPeriodID { get; set; }

        [DataMember]
        [XmlElement(ElementName = "RecordSourceTypeID")]
        public Int16? RecordSourceTypeID { get; set; }

        [DataMember]
        [XmlElement(ElementName = "RecordSourceID")]
        public long? RecordSourceID { get; set; }


        [DataMember]
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

        [DataMember]
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

        [DataMember]
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
        [DataMember]
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


        protected System.Int16? _WriteOnOffID = null;
        [DataMember]
        [XmlElement(ElementName = "DebitCredit")]
        public virtual System.Int16? WriteOnOffID
        {
            get
            {
                return this._WriteOnOffID;
            }
            set
            {
                this._WriteOnOffID = value;
            }
        }

        [DataMember]
        [XmlElement(ElementName = "MatchSetRefNumber")]
        public string MatchSetRefNumber
        {
            get { return _MatchSetRefNumber; }
            set { _MatchSetRefNumber = value; }
        }

        [DataMember]
        [XmlElement(ElementName = "MatchSetID")]
        public System.Int64? MatchSetID
        {
            get { return _MatchSetID; }
            set { _MatchSetID = value; }
        }

        [DataMember]
        [XmlElement(ElementName = "MatchSetSubSetCombinationID")]
        public System.Int64? MatchSetSubSetCombinationID
        {
            get { return _MatchSetSubSetCombinationID; }
            set { _MatchSetSubSetCombinationID = value; }
        }

        [DataMember]
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
        [XmlElement(ElementName = "GLDataRecurringItemScheduleIntervalDetailInfoList")]
        public List<GLDataRecurringItemScheduleIntervalDetailInfo> GLDataRecurringItemScheduleIntervalDetailInfoList { get; set; }

        [DataMember]
        [XmlElement(ElementName = "ExRateLCCYtoBCCY")]
        public System.Decimal? ExRateLCCYtoBCCY { get; set; }

        [DataMember]
        [XmlElement(ElementName = "ExRateLCCYtoRCCY")]
        public System.Decimal? ExRateLCCYtoRCCY { get; set; }
        [XmlElement(ElementName = "CrrentRecPeriodAmount")]
        public Decimal? CrrentRecPeriodAmount
        {
            get
            {
                return this._CrrentRecPeriodAmount;
            }
            set
            {
                this._CrrentRecPeriodAmount = value;

            }
        }

        [DataMember]
        [XmlElement(ElementName = "IgnoreInCalculation")]
        public System.Boolean? IgnoreInCalculation { get; set; }

        [DataMember]
        [XmlElement(ElementName = "IsCommentAvailable")]
        public bool? IsCommentAvailable { get; set; }
    }
}
