using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{
    public class ReconciliationArchiveInfo
    {
        protected System.Int32? _ReconciliationPeriodID = 0;
        public bool IsReconciliationPeriodIDNull = true;
        [XmlElement(ElementName = "ReconciliationPeriodID")]
        public virtual System.Int32? ReconciliationPeriodID
        {
            get
            {
                return this._ReconciliationPeriodID;
            }
            set
            {
                this._ReconciliationPeriodID = value;
                this.IsReconciliationPeriodIDNull = false;
            }
        }	

        protected System.DateTime? _PeriodEndDate = DateTime.Now;
        public bool IsPeriodEndDateNull = true;
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

        protected System.Decimal? _GLBalanceReportingCurrency = 0.00M;
        public bool IsGLBalanceReportingCurrencyNull = true;
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

                this.IsGLBalanceReportingCurrencyNull = false;
            }
        }	

        protected System.DateTime? _DateCertified = DateTime.Now;
        public bool IsDateCertifiedNull = true;
        [XmlElement(ElementName = "DateCertified")]
        public virtual System.DateTime? DateCertified
        {
            get
            {
                return this._DateCertified;
            }
            set
            {
                this._DateCertified = value;
                this.IsDateCertifiedNull = (_DateCertified == DateTime.MinValue);
            }
        }			

    }//end of class
}//end of namespace
