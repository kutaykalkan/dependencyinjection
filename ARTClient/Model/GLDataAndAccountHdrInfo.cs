

using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{    
	[Serializable]
	[DataContract]
    //TODO: change MapObject()  method to accomodate new fields
	public class GLDataAndAccountHdrInfo : GLDataHdrInfo
    {   
        protected System.Int32? _RiskRatingLabelID = 0;        
        public bool IsRiskRatingLabelIDNull = true;
        
        [XmlElement(ElementName = "RiskRatingLabelID")]
        public virtual System.Int32? RiskRatingLabelID
        {
            get
            {
                return this._RiskRatingLabelID;
            }
            set
            {
                this._RiskRatingLabelID = value;

                this.IsRiskRatingLabelIDNull = (this._RiskRatingLabelID == null);
            }
        }


        protected System.String _ReconciliationFrequency = "";
        protected System.Int32? _ReconciliationFrequencyLabelID = 0;
        public bool IsReconciliationFrequencyNull = true;
        public bool IsReconciliationFrequencyLabelIDNull = true;
        [XmlElement(ElementName = "ReconciliationFrequency")]
        public virtual System.String ReconciliationFrequency
        {
            get
            {
                return this._ReconciliationFrequency;
            }
            set
            {
                this._ReconciliationFrequency = value;

                this.IsReconciliationFrequencyNull = (this._ReconciliationFrequency == null);
            }
        }

        [XmlElement(ElementName = "ReconciliationFrequencyLabelID")]
        public virtual System.Int32? ReconciliationFrequencyLabelID
        {
            get
            {
                return this._ReconciliationFrequencyLabelID;
            }
            set
            {
                this._ReconciliationFrequencyLabelID = value;

                this.IsReconciliationFrequencyLabelIDNull = (this._ReconciliationFrequencyLabelID == null);
            }
        }


        protected System.String _ReconciliationTemplate = "";
        protected System.Int32? _ReconciliationTemplateLabelID = 0;
        public bool IsReconciliationTemplateNull = true;
        public bool IsReconciliationTemplateLabelIDNull = true;
        [XmlElement(ElementName = "ReconciliationTemplate")]
        public virtual System.String ReconciliationTemplate
        {
            get
            {
                return this._ReconciliationTemplate;
            }
            set
            {
                this._ReconciliationTemplate = value;

                this.IsReconciliationTemplateNull = (_ReconciliationTemplate == null);
            }
        }

        [XmlElement(ElementName = "ReconciliationTemplateLabelID")]
        public virtual System.Int32? ReconciliationTemplateLabelID
        {
            get
            {
                return this._ReconciliationTemplateLabelID;
            }
            set
            {
                this._ReconciliationTemplateLabelID = value;

                this.IsReconciliationTemplateLabelIDNull = (this._ReconciliationTemplateLabelID == null);
            }
        }


        protected System.String _SubledgerSource = "";
        protected System.Int32? _SubledgerSourceLabelID = 0;

        public bool IsSubledgerSourceNull = true;
        public bool IsSubledgerSourceLabelIDNull = true;

        [XmlElement(ElementName = "SubledgerSource")]
        public virtual System.String SubledgerSource
        {
            get
            {
                return this._SubledgerSource;
            }
            set
            {
                this._SubledgerSource = value;

                this.IsSubledgerSourceNull = (_SubledgerSource == null);
            }
        }

        //[XmlElement(ElementName = "SubledgerSourceID")]
        //public virtual System.Int32? SubledgerSourceID
        //{
        //    get
        //    {
        //        return this._SubledgerSourceID;
        //    }
        //    set
        //    {
        //        this._SubledgerSourceID = value;

        //        this.IsSubledgerSourceIDNull = (this._SubledgerSourceID == null);
        //    }
        //}

        [XmlElement(ElementName = "SubledgerSourceLabelID")]
        public virtual System.Int32? SubledgerSourceLabelID
        {
            get
            {
                return this._SubledgerSourceLabelID;
            }
            set
            {
                this._SubledgerSourceLabelID = value;

                this.IsSubledgerSourceLabelIDNull = (this._SubledgerSourceLabelID == null);
            }
        }



        protected System.DateTime? _CertificationStartDate = DateTime.Now;
        protected System.DateTime? _ReconciliationCloseDate = DateTime.Now;
        public bool IsCertificationStartDateNull = true;
        public bool IsReconciliationCloseDateNull = true;

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


        protected System.DateTime? _SubmittedDate = DateTime.Now;
        public bool IsSubmittedDateNull = true;

        [XmlElement(ElementName = "SubmittedDate")]
        public virtual System.DateTime? SubmittedDate
        {
            get
            {
                return this._SubmittedDate;
            }
            set
            {
                this._SubmittedDate = value;

                this.IsSubmittedDateNull = (_SubmittedDate == DateTime.MinValue);
            }
        }


        protected System.DateTime? _ReviewedDate = DateTime.Now;
        public bool IsReviewedDateNull = true;

        [XmlElement(ElementName = "ReviewedDate")]
        public virtual System.DateTime? ReviewedDate
        {
            get
            {
                return this._ReviewedDate;
            }
            set
            {
                this._ReviewedDate = value;

                this.IsReviewedDateNull = (_ReviewedDate == DateTime.MinValue);
            }
        }

        protected System.DateTime? _ApprovedDate = DateTime.Now;
        public bool IsApprovedDateNull = true;

        [XmlElement(ElementName = "ApprovedDate")]
        public virtual System.DateTime? ApprovedDate
        {
            get
            {
                return this._ApprovedDate;
            }
            set
            {
                this._ApprovedDate = value;

                this.IsApprovedDateNull = (_ApprovedDate == DateTime.MinValue);
            }
        }


        protected System.Decimal? _UnexplainedVarianceThreshold = 0.00M;
        public bool IsUnexplainedVarianceThresholdNull = true;
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

                this.IsUnexplainedVarianceThresholdNull = (this._UnexplainedVarianceThreshold == null);
            }
        }			

        protected System.Decimal? _AccountMaterialityThreshold = 0.00M;
        public bool IsAccountMaterialityThresholdNull = true;
        [XmlElement(ElementName = "AccountMaterialityThreshold")]
        public virtual System.Decimal? AccountMaterialityThreshold
        {
            get
            {
                return this._AccountMaterialityThreshold;
            }
            set
            {
                this._AccountMaterialityThreshold = value;

                this.IsAccountMaterialityThresholdNull = (this._AccountMaterialityThreshold == null);
            }
        }

        protected System.DateTime? _PreparerCertificationSignOffDate;
        public bool IsPreparerCertificationSignOffDateNull = true;
        [XmlElement(ElementName = "PreparerCertificationSignOffDate")]
        public virtual System.DateTime? PreparerCertificationSignOffDate
        {
            get
            {
                return this._PreparerCertificationSignOffDate;
            }
            set
            {
                this._PreparerCertificationSignOffDate = value;

                this.IsPreparerCertificationSignOffDateNull = (this._PreparerCertificationSignOffDate == null);
            }
        }

        protected System.DateTime? _ReviewerCertificationSignOffDate;
        public bool IsReviewerCertificationSignOffDateNull = true;
        [XmlElement(ElementName = "ReviewerCertificationSignOffDate")]
        public virtual System.DateTime? ReviewerCertificationSignOffDate
        {
            get
            {
                return this._ReviewerCertificationSignOffDate;
            }
            set
            {
                this._ReviewerCertificationSignOffDate = value;

                this.IsReviewerCertificationSignOffDateNull = (this._ReviewerCertificationSignOffDate == null);
            }
        }

        protected System.DateTime? _ApproverCertificationSignOffDate;
        public bool IsApproverCertificationSignOffDateNull = true;
        [XmlElement(ElementName = "ApproverCertificationSignOffDate")]
        public virtual System.DateTime? ApproverCertificationSignOffDate
        {
            get
            {
                return this._ApproverCertificationSignOffDate;
            }
            set
            {
                this._ApproverCertificationSignOffDate = value;

                this.IsApproverCertificationSignOffDateNull = (this._ApproverCertificationSignOffDate == null);
            }
        }

        

    }//end of class
}//end of namespace

