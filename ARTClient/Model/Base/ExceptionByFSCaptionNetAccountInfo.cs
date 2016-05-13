using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model.Base
{
    public class ExceptionByFSCaptionNetAccountInfo : MultilingualInfo
    {
        protected System.Int32? _ID = 0;
        protected System.Decimal? _UnexplainedVarianceReportingCurrency = null;
        protected System.Decimal? _WriteOnOffAmountReportingCurrency = null;
        protected System.Decimal? _TotalVar = null;

        [XmlElement(ElementName = "ID")]
        public virtual System.Int32? ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this._ID = value;
            }
        }

        [XmlElement(ElementName = "UnexplainedVarianceReportingCurrency")]
        public virtual System.Decimal? UnexplainedVarianceReportingCurrency
        {
            get
            {
                return this._UnexplainedVarianceReportingCurrency;
            }
            set
            {
                this._UnexplainedVarianceReportingCurrency = value;
            }
        }

        [XmlElement(ElementName = "WriteOffAmountReportingCurrency")]
        public virtual System.Decimal? WriteOnOffAmountReportingCurrency
        {
            get
            {
                return this._WriteOnOffAmountReportingCurrency;
            }
            set
            {
                this._WriteOnOffAmountReportingCurrency = value;
            }
        }


        [XmlElement(ElementName = "TotalVar")]
        public virtual System.Decimal? TotalVar
        {
            get
            {
                return this._TotalVar;
            }
            set
            {
                this._TotalVar = value;
            }
        }


    }
}
