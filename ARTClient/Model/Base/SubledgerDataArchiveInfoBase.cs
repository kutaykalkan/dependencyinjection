using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{
    [Serializable]
    public abstract class SubledgerDataArchiveInfoBase
    {
        protected System.Int64? _SubledgerDataArchiveID;
        protected System.Int64? _AccountID = 0;
        protected System.Int64? _GLDataID = 0;
        protected System.Int32? _ReconciliationPeriodID = 0;
        protected System.Int32? _DataImportID = 0;
        protected System.Decimal? _SubledgerBalanceBaseCCY = 0.00M;
        protected System.Decimal? _SubledgerBalanceReportingCCY = 0.00M;
        protected System.String _BaseCurrencyCode;
        protected System.String _ReportingCurrencyCode;
        protected System.String _AddedBy = "";
        protected System.DateTime? _DateAdded = DateTime.Now;
        protected System.String _ReconciliationStatus; 
        public bool IsSubledgerDataArchiveIDNull = true;
        public bool IsAccountIDNull = true;
        public bool IsGLDataIDNull = true;
        public bool IsSubledgerBalanceBaseCCYNull = true;
        public bool IsSubledgerBalanceReportingCCYNull = true;
        public bool IsDataImportIDNull = true;
        public bool IsReconciliationPeriodIDNull = true;
        public bool IsAddedByNull = true;
        public bool IsDateAddedNull = true;
        public bool IsReconciliationStatusNull = true;


        [XmlElement(ElementName = "SubledgerDataArchiveID")]
        public virtual System.Int64? SubledgerDataArchiveID
        {
            get
            {
                return this._SubledgerDataArchiveID;
            }
            set
            {
                this._SubledgerDataArchiveID = value;
                this.IsSubledgerDataArchiveIDNull = false;
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
                this.IsAccountIDNull = false;
            }
        }

        [XmlElement(ElementName = "GLDataID")]
        public virtual System.Int64? GLDataID
        {
            get
            {
                return this._GLDataID;
            }
            set
            {
                this._GLDataID = value;
                this.IsGLDataIDNull = false;
            }
        }

        [XmlElement(ElementName = "SubledgerBalanceBaseCCY")]
        public virtual System.Decimal? SubledgerBalanceBaseCCY
        {
            get
            {
                return this._SubledgerBalanceBaseCCY;
            }
            set
            {
                this._SubledgerBalanceBaseCCY = value;
                this.IsSubledgerBalanceBaseCCYNull = false;
            }
        }

        [XmlElement(ElementName = "SubledgerBalanceReportingCCY")]
        public virtual System.Decimal? SubledgerBalanceReportingCCY
        {
            get
            {
                return this._SubledgerBalanceReportingCCY;
            }
            set
            {
                this._SubledgerBalanceReportingCCY = value;
                this.IsSubledgerBalanceReportingCCYNull = false;
            }
        }

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
                this.IsDataImportIDNull = false;
            }
        }

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
                this.IsDateAddedNull = (_DateAdded == DateTime.MinValue);
            }
        }
        
        //
        [XmlElement(ElementName = "BaseCurrencyCode")]
        public virtual System.String BaseCurrencyCode
        {
            get
            {
                return this._BaseCurrencyCode;
            }
            set
            {
                this._BaseCurrencyCode = value;
                
            }
        }

        [XmlElement(ElementName = "ReportingCurrencyCode")]
        public virtual System.String ReportingCurrencyCode
        {
            get
            {
                return this._ReportingCurrencyCode;
            }
            set
            {
                this._ReportingCurrencyCode = value;
                
            }
        }

        [XmlElement(ElementName = "ReconciliationStatus")]
        public virtual System.String ReconciliationStatus
        {
            get
            {
                return this._ReconciliationStatus;
            }
            set
            {
                this._ReconciliationStatus = value;
                
            }
        }
    }
}
