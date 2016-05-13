
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using SkyStem.ART.Client.Data;

namespace SkyStem.ART.Client.Model
{    
	/// <summary>
	/// An object representation of the SkyStemArt GLDataWriteOnOff table
	/// </summary>
	[Serializable]
	[DataContract]
	public class GLDataWriteOnOffInfo : GLDataWriteOnOffInfoBase
	{

        protected System.Boolean? _IsForwardedItem = null;
        protected System.Int64? _ExcelRowNumber = 0;
        protected System.Int64? _MatchSetMatchingSourceDataImportID = 0;
        protected System.Int16? _IndexID = 0;
        public bool IsIsForwardedItemNull = true;
        protected string _MatchSetRefNumber = null;
        protected System.Int64? _MatchSetID = null;
        protected System.Int64? _MatchSetSubSetCombinationID = null;
        protected System.Int16? _GLDataRecItemTypeID = 0;
        [XmlElement(ElementName = "IsForwardedItem")]
        public virtual System.Boolean? IsForwardedItem
        {
            get
            {
                return this._IsForwardedItem;
            }
            set
            {
                this._IsForwardedItem = value;

                this.IsIsForwardedItemNull = false;
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

                this.IsAmountBaseCurrencyNull = false;
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

                this.IsAmountReportingCurrencyNull = false;
            }
        }

        private string _RecItemNumber = null;
        [XmlElement(ElementName = "RecItemNumber")]
        public string RecItemNumber
        {
            get { return _RecItemNumber; }
            set { _RecItemNumber = value; }
        }



        public int Aging
        {
            get
            {
                if (OpenDate.HasValue)
                    return ModelHelper.GetAging(this.OpenDate);
                else return 0;

            }
        }
        /// <summary>
        /// String equivalant of DebitCredit boolean property
        /// </summary>
        //public string DbCr
        //{
        //    get
        //    {
        //        if (this.WriteOnOffID.HasValue)
        //            return this.WriteOnOffID.Value ? "Debit" : "Credit";
        //        else
        //            return "";
        //    }
        //}

        private string _UserName;
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
        public Int16? RecordSourceTypeID { get; set; }
        public long? RecordSourceID { get; set; }


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


        protected System.Int16? _ReconciliationCategoryID = 0;
        protected System.Int16? _ReconciliationCategoryTypeID = 0;
        [XmlElement(ElementName = "ReconciliationCategoryID")]
        public virtual System.Int16? ReconciliationCategoryID
        {
            get
            {
                return this._ReconciliationCategoryID;
            }
            set
            {
                this._ReconciliationCategoryID = value;
            }
        }

        [XmlElement(ElementName = "ReconciliationCategoryTypeID")]
        public virtual System.Int16? ReconciliationCategoryTypeID
        {
            get
            {
                return this._ReconciliationCategoryTypeID;
            }
            set
            {
                this._ReconciliationCategoryTypeID = value;
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
    }
}
