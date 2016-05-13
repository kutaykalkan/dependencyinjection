
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{

    /// <summary>
    /// An object representation of the SkyStemArt GLDataUnexplainedVariance table
    /// </summary>
    [Serializable]
    [DataContract]
    public class GLDataUnexplainedVarianceInfo : GLDataUnexplainedVarianceInfoBase
    {
        protected System.DateTime? _PeriodEndDate = DateTime.Now;

        public bool IsPeriodEndDateNull = true;

        protected System.String _AddedBy = "";
        public bool IsAddedByNull = true;
        UserHdrInfo _AddedByUserInfo = null;
        protected System.Int16? _RecordSourceTypeID = 0;
        protected System.Int64? _RecordSourceID = 0;

        [XmlElement(ElementName = "AddedByUserInfo")]
        public UserHdrInfo AddedByUserInfo
        {
            get { return _AddedByUserInfo; }
            set { _AddedByUserInfo = value; }
        }


        [XmlElement(ElementName = "AddedBy")]
        public virtual System.String AddedBy
        {
            get
            {
                return this._AddedBy;
            }
            set
            {
                this._AddedBy = value;

                this.IsAddedByNull = (_AddedBy == null);
            }
        }

        protected System.Int32? _AddedByUserID = null;
        public bool IsAddedByUserIDNull = true;
        [XmlElement(ElementName = "AddedByUserID")]
        public virtual System.Int32? AddedByUserID
        {
            get
            {
                return this._AddedByUserID;
            }
            set
            {
                this._AddedByUserID = value;

                this.IsAddedByUserIDNull = (_AddedByUserID == null);
            }
        }


        protected System.DateTime? _DateAdded = null;
        public bool IsDateAddedNull = true;
        [XmlElement(ElementName = "DateAdded")]
        public virtual System.DateTime? DateAdded
        {
            get
            {
                return this._DateAdded;
            }
            set
            {
                this._DateAdded = value;

                this.IsDateAddedNull = (_DateAdded == DateTime.MinValue || _DateAdded == null);
            }
        }


        protected System.Decimal? _AmountBaseCurrency = null;
        public bool IsAmountBaseCurrencyNull = true;
        [XmlElement(ElementName = "AmountBaseCurrency")]
        public virtual System.Decimal? AmountBaseCurrency
        {
            get
            {
                return this._AmountBaseCurrency;
            }
            set
            {
                this._AmountBaseCurrency = value;

                this.IsAmountBaseCurrencyNull = (_AmountBaseCurrency == null);
            }
        }


        protected System.Decimal? _AmountReportingCurrency = null;
        public bool IsAmountReportingCurrencyNull = true;
        [XmlElement(ElementName = "AmountReportingCurrency")]
        public virtual System.Decimal? AmountReportingCurrency
        {
            get
            {
                return this._AmountReportingCurrency;
            }
            set
            {
                this._AmountReportingCurrency = value;

                this.IsAmountReportingCurrencyNull = (_AmountReportingCurrency == null);
            }
        }


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

                this.IsPeriodEndDateNull = (_PeriodEndDate == DateTime.MinValue);
            }
        }



        protected System.Int32? _RecCategoryTypeID = null;
        [XmlElement(ElementName = "RecCategoryTypeID")]
        public virtual System.Int32? RecCategoryTypeID
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


        protected System.Int32? _RecCategoryID = null;
        [XmlElement(ElementName = "RecCategoryID")]
        public virtual System.Int32? RecCategoryID
        {
            get
            {
                return this._RecCategoryID;
            }
            set
            {
                this._RecCategoryID = value;
            }
        }

        private string _reportingCurrencyCode;
        public virtual System.String ReportingCurrencyCode
        {
            get
            {
                return this._reportingCurrencyCode;
            }
            set
            {
                this._reportingCurrencyCode = value;

            }
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

    }
}
