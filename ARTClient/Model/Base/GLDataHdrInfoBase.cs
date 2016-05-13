

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemArt GLDataHdr table
    /// </summary>
    [Serializable]
    public abstract class GLDataHdrInfoBase : OrganizationalHierarchyInfo
    {
        protected System.Int64? _AccountID = 0;
        protected System.String _AddedBy = "";
        protected System.DateTime? _ReconciledStatusDate = null;
        protected System.Decimal? _SupportingDetailBalanceBaseCurrency = null;
        protected System.Decimal? _SupportingDetailBalanceReportingCurrency = null;
        protected System.String _BaseCurrencyCode = "";
        protected System.Int16? _CertificationStatusID = 0;
        protected System.Int32? _DataImportID = 0;
        protected System.DateTime? _DateAdded = DateTime.Now;
        protected System.DateTime? _DateRevised = DateTime.Now;
        protected System.Decimal? _GLBalanceBaseCurrency = null;
        protected System.Decimal? _GLBalanceReportingCurrency = null;
        protected System.Int64? _GLDataID = 0;
        protected System.String _HostName = "";
        protected System.Boolean? _IsActive = false;
        protected System.Boolean? _IsAttachmentAvailable = false;
        protected System.Boolean? _IsMaterial = false;
        protected System.Boolean? _IsSystemReconcilied = null;
        protected System.DateTime? _PendingReviewStatusDate = null;
        protected System.Decimal? _ReconciliationBalanceReportingCurrency = null;
        protected System.Decimal? _ReconciliationBalanceBaseCurrency = null;
        protected System.Int32? _ReconciliationPeriodID = 0;
        protected System.DateTime? _ReconciliationStatusDate = null;
        protected System.Int16? _ReconciliationStatusID = 0;
        protected System.DateTime? _PendingApprovalStatusDate = null;
        protected System.String _RevisedBy = "";
        protected System.Decimal? _UnexplainedVarianceReportingCurrency = null;
        protected System.Decimal? _UnexplainedVarianceBaseCurrency = null;
        protected System.Decimal? _WriteOnOffAmountReportingCurrency = null;
        protected System.Decimal? _WriteOnOffAmountBaseCurrency = null;





        public bool IsAccountIDNull = true;


        public bool IsAddedByNull = true;


        public bool IsApproverSignOffDateNull = true;


        public bool IsSupportingDetailBalanceBaseCurrencyNull = true;


        public bool IsSupportingDetailBalanceReportingCurrencyNull = true;


        public bool IsBaseCurrencyCodeNull = true;


        public bool IsCertificationStatusIDNull = true;


        public bool IsDataImportIDNull = true;


        public bool IsDateAddedNull = true;


        public bool IsDateRevisedNull = true;


        public bool IsGLBalanceBaseCurrencyNull = true;


        public bool IsGLBalanceReportingCurrencyNull = true;


        public bool IsGLDataIDNull = true;


        public bool IsHostNameNull = true;


        public bool IsIsActiveNull = true;


        public bool IsIsAttachmentAvailableNull = true;


        public bool IsIsMaterialNull = true;


        public bool IsIsSystemReconciliedNull = true;


        public bool IsPreparerSignOffDateNull = true;


        public bool IsReconciliationBalanceReportingCurrencyNull = true;
        public bool IsReconciliationBalanceBaseCurrencyNull = true;


        public bool IsReconciliationPeriodIDNull = true;


        public bool IsReconciliationStatusDateNull = true;


        public bool IsReconciliationStatusIDNull = true;


        public bool IsReviewerSignOffDateNull = true;


        public bool IsRevisedByNull = true;


        public bool IsUnexplainedVarianceReportingCurrencyNull = true;
        public bool IsUnexplainedVarianceBaseCurrencyNull = true;


        public bool IsWriteOnOffAmountReportingCurrencyNull = true;
        public bool IsWriteOnOffAmountBaseCurrencyNull = true;



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

        [XmlElement(ElementName = "ApproverSignOffDate")]
        public virtual System.DateTime? ReconciledStatusDate
        {
            get
            {
                return this._ReconciledStatusDate;
            }
            set
            {
                this._ReconciledStatusDate = value;

                this.IsApproverSignOffDateNull = (_ReconciledStatusDate == DateTime.MinValue);
            }
        }

        [XmlElement(ElementName = "SupportingDetailBalanceBaseCurrency")]
        public virtual System.Decimal? SupportingDetailBalanceBaseCurrency
        {
            get
            {
                return this._SupportingDetailBalanceBaseCurrency;
            }
            set
            {
                this._SupportingDetailBalanceBaseCurrency = value;

                this.IsSupportingDetailBalanceBaseCurrencyNull = false;
            }
        }

        [XmlElement(ElementName = "SupportingDetailBalanceReportingCurrency")]
        public virtual System.Decimal? SupportingDetailBalanceReportingCurrency
        {
            get
            {
                return this._SupportingDetailBalanceReportingCurrency;
            }
            set
            {
                this._SupportingDetailBalanceReportingCurrency = value;

                this.IsSupportingDetailBalanceReportingCurrencyNull = false;
            }
        }

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

                this.IsBaseCurrencyCodeNull = (_BaseCurrencyCode == null);
            }
        }

        [XmlElement(ElementName = "CertificationStatusID")]
        public virtual System.Int16? CertificationStatusID
        {
            get
            {
                return this._CertificationStatusID;
            }
            set
            {
                this._CertificationStatusID = value;

                this.IsCertificationStatusIDNull = false;
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

        [XmlElement(ElementName = "GLBalanceBaseCurrency")]
        public virtual System.Decimal? GLBalanceBaseCurrency
        {
            get
            {
                return this._GLBalanceBaseCurrency;
            }
            set
            {
                this._GLBalanceBaseCurrency = value;

                this.IsGLBalanceBaseCurrencyNull = false;
            }
        }

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

        [XmlElement(ElementName = "IsAttachmentAvailable")]
        public virtual System.Boolean? IsAttachmentAvailable
        {
            get
            {
                return this._IsAttachmentAvailable;
            }
            set
            {
                this._IsAttachmentAvailable = value;

                this.IsIsAttachmentAvailableNull = false;
            }
        }

        [XmlElement(ElementName = "IsMaterial")]
        public virtual System.Boolean? IsMaterial
        {
            get
            {
                return this._IsMaterial;
            }
            set
            {
                this._IsMaterial = value;

                this.IsIsMaterialNull = false;
            }
        }

        [XmlElement(ElementName = "IsSystemReconcilied")]
        public virtual System.Boolean? IsSystemReconcilied
        {
            get
            {
                return this._IsSystemReconcilied;
            }
            set
            {
                this._IsSystemReconcilied = value;

                this.IsIsSystemReconciliedNull = false;
            }
        }

        [XmlElement(ElementName = "PendingReviewStatusDate")]
        public virtual System.DateTime? PendingReviewStatusDate
        {
            get
            {
                return this._PendingReviewStatusDate;
            }
            set
            {
                this._PendingReviewStatusDate = value;

                this.IsPreparerSignOffDateNull = (_PendingReviewStatusDate == DateTime.MinValue);
            }
        }

        [XmlElement(ElementName = "ReconciliationBalanceReportingCurrency")]
        public virtual System.Decimal? ReconciliationBalanceReportingCurrency
        {
            get
            {
                return this._ReconciliationBalanceReportingCurrency;
            }
            set
            {
                this._ReconciliationBalanceReportingCurrency = value;

                this.IsReconciliationBalanceReportingCurrencyNull = false;
            }
        }


        [XmlElement(ElementName = "ReconciliationBalanceBaseCurrency")]
        public virtual System.Decimal? ReconciliationBalanceBaseCurrency
        {
            get
            {
                return this._ReconciliationBalanceBaseCurrency;
            }
            set
            {
                this._ReconciliationBalanceBaseCurrency = value;

                this.IsReconciliationBalanceBaseCurrencyNull = false;
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

        [XmlElement(ElementName = "ReconciliationStatusDate")]
        public virtual System.DateTime? ReconciliationStatusDate
        {
            get
            {
                return this._ReconciliationStatusDate;
            }
            set
            {
                this._ReconciliationStatusDate = value;

                this.IsReconciliationStatusDateNull = (_ReconciliationStatusDate == DateTime.MinValue);
            }
        }

        [XmlElement(ElementName = "ReconciliationStatusID")]
        public virtual System.Int16? ReconciliationStatusID
        {
            get
            {
                return this._ReconciliationStatusID;
            }
            set
            {
                this._ReconciliationStatusID = value;

                this.IsReconciliationStatusIDNull = false;
            }
        }

        [XmlElement(ElementName = "PendingApprovalStatusDate")]
        public virtual System.DateTime? PendingApprovalStatusDate
        {
            get
            {
                return this._PendingApprovalStatusDate;
            }
            set
            {
                this._PendingApprovalStatusDate = value;

                this.IsReviewerSignOffDateNull = (_PendingApprovalStatusDate == DateTime.MinValue);
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

                this.IsUnexplainedVarianceReportingCurrencyNull = false;
            }
        }


        [XmlElement(ElementName = "UnexplainedVarianceBaseCurrency")]
        public virtual System.Decimal? UnexplainedVarianceBaseCurrency
        {
            get
            {
                return this._UnexplainedVarianceBaseCurrency;
            }
            set
            {
                this._UnexplainedVarianceBaseCurrency = value;

                this.IsUnexplainedVarianceBaseCurrencyNull = false;
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

                this.IsWriteOnOffAmountReportingCurrencyNull = false;
            }
        }

        [XmlElement(ElementName = "WriteOffAmountBaseCurrency")]
        public virtual System.Decimal? WriteOnOffAmountBaseCurrency
        {
            get
            {
                return this._WriteOnOffAmountBaseCurrency;
            }
            set
            {
                this._WriteOnOffAmountBaseCurrency = value;

                this.IsWriteOnOffAmountBaseCurrencyNull = false;
            }
        }


        /// <summary>
        /// Returns a string representation of the object, displaying all property and field names and values.
        /// </summary>
        public override string ToString()
        {
            return StringUtil.ToString(this);
        }

    }//end of class
}//end of namespace
