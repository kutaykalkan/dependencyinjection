

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemArt ReconciliationPeriod table
    /// </summary>
    [Serializable]
    public abstract class ReconciliationPeriodInfoBase
    {
        protected System.DateTime? _ActivationDate = DateTime.Now;
        protected System.String _AddedBy = "";
        protected System.String _BaseCurrency = "";
        //protected System.DateTime? _CertificationStartDate = DateTime.Now;
        //protected System.DateTime? _CertificationLockDownDate = DateTime.Now;
        protected System.DateTime? _CertificationStartDate = null ;
        protected System.DateTime? _CertificationLockDownDate = null;
        protected System.Int32? _CompanyID = 0;
        protected System.Decimal? _CompanyMaterialityThreshold = 0.00M;
        protected System.DateTime? _DateAdded = DateTime.Now;
        protected System.DateTime? _DateRevised = DateTime.Now;
        protected System.String _HostName = "";
        protected System.Boolean? _IsActive = false;
        protected System.Int16? _MaterialityTypeID = 0;
        protected System.DateTime? _PeriodEndDate = null;
        protected System.Int16? _PeriodNumber = 0;
        protected System.DateTime? _ReconciliationCloseDate = null;
        //protected System.DateTime? _PeriodEndDate = DateTime.Now;
        //protected System.DateTime? _ReconciliationCloseDate = DateTime.Now;
        protected System.Int32? _ReconciliationPeriodID = 0;
        protected System.String _ReportingCurrency = "";
        protected System.String _ReportingCurrencyCode = "";
        protected System.String _RevisedBy = "";
        protected System.Decimal? _UnexplainedVarianceThreshold = 0.00M;




        public bool IsActivationDateNull = true;


        public bool IsAddedByNull = true;


        public bool IsBaseCurrencyNull = true;


        public bool IsCertificationStartDateNull = true;


        public bool IsCertificationLockDownDateNull = true;


        public bool IsCompanyIDNull = true;


        public bool IsCompanyMaterialityThresholdNull = true;


        public bool IsDateAddedNull = true;


        public bool IsDateRevisedNull = true;


        public bool IsHostNameNull = true;


        public bool IsIsActiveNull = true;


        public bool IsMaterialityTypeIDNull = true;


        public bool IsPeriodEndDateNull = true;


        public bool IsPeriodNumberNull = true;


        public bool IsReconciliationCloseDateNull = true;


        public bool IsReconciliationPeriodIDNull = true;


        public bool IsReportingCurrencyNull = true;


        public bool IsReportingCurrencyCodeNull = true;


        public bool IsRevisedByNull = true;


        public bool IsUnexplainedVarianceThresholdNull = true;

        [XmlElement(ElementName = "ActivationDate")]
        public virtual System.DateTime? ActivationDate
        {
            get
            {
                return this._ActivationDate;
            }
            set
            {
                this._ActivationDate = value;

                this.IsActivationDateNull = (_ActivationDate == DateTime.MinValue);
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

        [XmlElement(ElementName = "BaseCurrency")]
        public virtual System.String BaseCurrency
        {
            get
            {
                return this._BaseCurrency;
            }
            set
            {
                this._BaseCurrency = value;

                this.IsBaseCurrencyNull = (_BaseCurrency == null);
            }
        }

        [XmlElement(ElementName = "CertificationStartDate")]
        public virtual System.DateTime? CertificationStartDate
        {
            get
            {
                return this._CertificationStartDate;
            }
            set
            {
                this._CertificationStartDate = value;

                this.IsCertificationStartDateNull = (_CertificationStartDate == DateTime.MinValue);
            }
        }

        [XmlElement(ElementName = "CertificationLockDownDate")]
        public virtual System.DateTime? CertificationLockDownDate
        {
            get
            {
                return this._CertificationLockDownDate;
            }
            set
            {
                this._CertificationLockDownDate = value;

                this.IsCertificationLockDownDateNull = (_CertificationLockDownDate == DateTime.MinValue);
            }
        }

        [XmlElement(ElementName = "CompanyID")]
        public virtual System.Int32? CompanyID
        {
            get
            {
                return this._CompanyID;
            }
            set
            {
                this._CompanyID = value;

                this.IsCompanyIDNull = false;
            }
        }

        [XmlElement(ElementName = "CompanyMaterialityThreshold")]
        public virtual System.Decimal? CompanyMaterialityThreshold
        {
            get
            {
                return this._CompanyMaterialityThreshold;
            }
            set
            {
                this._CompanyMaterialityThreshold = value;

                this.IsCompanyMaterialityThresholdNull = false;
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

        [XmlElement(ElementName = "DateRevised")]
        public virtual System.DateTime? DateRevised
        {
            get
            {
                return this._DateRevised;
            }
            set
            {
                this._DateRevised = value;

                this.IsDateRevisedNull = (_DateRevised == DateTime.MinValue);
            }
        }

        [XmlElement(ElementName = "HostName")]
        public virtual System.String HostName
        {
            get
            {
                return this._HostName;
            }
            set
            {
                this._HostName = value;

                this.IsHostNameNull = (_HostName == null);
            }
        }

        [XmlElement(ElementName = "IsActive")]
        public virtual System.Boolean? IsActive
        {
            get
            {
                return this._IsActive;
            }
            set
            {
                this._IsActive = value;

                this.IsIsActiveNull = false;
            }
        }

        [XmlElement(ElementName = "MaterialityTypeID")]
        public virtual System.Int16? MaterialityTypeID
        {
            get
            {
                return this._MaterialityTypeID;
            }
            set
            {
                this._MaterialityTypeID = value;

                this.IsMaterialityTypeIDNull = false;
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

        [XmlElement(ElementName = "PeriodNumber")]
        public virtual System.Int16? PeriodNumber
        {
            get
            {
                return this._PeriodNumber;
            }
            set
            {
                this._PeriodNumber = value;

                this.IsPeriodNumberNull = false;
            }
        }

        [XmlElement(ElementName = "ReconciliationCloseDate")]
        public virtual System.DateTime? ReconciliationCloseDate
        {
            get
            {
                return this._ReconciliationCloseDate;
            }
            set
            {
                this._ReconciliationCloseDate = value;

                this.IsReconciliationCloseDateNull = (_ReconciliationCloseDate == DateTime.MinValue);
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

        [XmlElement(ElementName = "ReportingCurrency")]
        public virtual System.String ReportingCurrency
        {
            get
            {
                return this._ReportingCurrency;
            }
            set
            {
                this._ReportingCurrency = value;

                this.IsReportingCurrencyNull = (_ReportingCurrency == null);
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

                this.IsReportingCurrencyCodeNull = (_ReportingCurrencyCode == null);
            }
        }

        [XmlElement(ElementName = "RevisedBy")]
        public virtual System.String RevisedBy
        {
            get
            {
                return this._RevisedBy;
            }
            set
            {
                this._RevisedBy = value;

                this.IsRevisedByNull = (_RevisedBy == null);
            }
        }

        [XmlElement(ElementName = "UnexplainedVarianceThreshold")]
        public virtual System.Decimal? UnexplainedVarianceThreshold
        {
            get
            {
                return this._UnexplainedVarianceThreshold;
            }
            set
            {
                this._UnexplainedVarianceThreshold = value;

                this.IsUnexplainedVarianceThresholdNull = false;
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
