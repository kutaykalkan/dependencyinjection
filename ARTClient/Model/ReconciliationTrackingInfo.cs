using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model
{
    [Serializable]
    [DataContract]
    public class ReconciliationTrackingInfo
    {
        System.Int32? _TotalAccounts = null;
        System.Int32? _Prepared = null;
        System.Int32? _InProgress = null;
        System.Int32? _PendingReview = null;
        System.Int32? _PendingModificationPreparer = null;
        System.Int32? _Reviewed = null;
        System.Int32? _PendingApproval = null;
        System.Int32? _Approved = null;
        System.Int32? _Notstarted = null;
        System.Int32? _SysReconciled = null;
        System.Int32? _Reconciled = null;
        System.Int32? _PendingModificationReviewer = null;
        System.Decimal? _PreparedAmount = null;
        System.Decimal? _InProgressAmount = null;
        System.Decimal? _PendingReviewAmount = null;
        System.Decimal? _PendingModificationPreparerAmount = null;
        System.Decimal? _ReviewedAmount = null;
        System.Decimal? _PendingApprovalAmount = null;
        System.Decimal? _ApprovedAmount = null;
        System.Decimal? _NotstartedAmount = null;
        System.Decimal? _SysReconciledAmount = null;
        System.Decimal? _ReconciledAmount = null;
        System.Decimal? _PendingModificationReviewerAmount = null;

        System.String _BaseCurrencyCode = null;
        System.String _ReportingCurrencyCode = null;

        
        [XmlElement(ElementName = "TotalAccounts")]
        public System.Int32? TotalAccounts
        {
            get { return _TotalAccounts; }
            set { _TotalAccounts = value; }
        }

        [XmlElement(ElementName = "Prepared")]
        public System.Int32? Prepared
        {
            get { return _Prepared; }
            set { _Prepared = value; }
        }

        [XmlElement(ElementName = "InProgress")]
        public System.Int32? InProgress
        {
            get { return _InProgress; }
            set { _InProgress = value; }
        }

        [XmlElement(ElementName = "PendingReview")]
        public System.Int32? PendingReview
        {
            get { return _PendingReview; }
            set { _PendingReview = value; }
        }

        [XmlElement(ElementName = "PendingModificationPreparer")]
        public System.Int32? PendingModificationPreparer
        {
            get { return _PendingModificationPreparer; }
            set { _PendingModificationPreparer = value; }
        }

        [XmlElement(ElementName = "Reviewed")]
        public System.Int32? Reviewed
        {
            get { return _Reviewed; }
            set { _Reviewed = value; }
        }

        [XmlElement(ElementName = "PendingApproval")]
        public System.Int32? PendingApproval
        {
            get { return _PendingApproval; }
            set { _PendingApproval = value; }
        }

        [XmlElement(ElementName = "Approved")]
        public System.Int32? Approved
        {
            get { return _Approved; }
            set { _Approved = value; }
        }

        [XmlElement(ElementName = "Notstarted")]
        public System.Int32? Notstarted
        {
            get { return _Notstarted; }
            set { _Notstarted = value; }
        }

        [XmlElement(ElementName = "SysReconciled")]
        public System.Int32? SysReconciled
        {
            get { return _SysReconciled; }
            set { _SysReconciled = value; }
        }

        [XmlElement(ElementName = "Reconciled")]
        public System.Int32? Reconciled
        {
            get { return _Reconciled; }
            set { _Reconciled = value; }
        }

        [XmlElement(ElementName = "PendingModificationReviewer")]
        public System.Int32? PendingModificationReviewer
        {
            get { return _PendingModificationReviewer; }
            set { _PendingModificationReviewer = value; }
        }


        [XmlElement(ElementName = "PreparedAmount")]
        public System.Decimal? PreparedAmount
        {
            get { return _PreparedAmount; }
            set { _PreparedAmount = value; }
        }


        [XmlElement(ElementName = "InProgressAmount")]
        public System.Decimal? InProgressAmount
        {
            get { return _InProgressAmount; }
            set { _InProgressAmount = value; }
        }


       [XmlElement(ElementName = "PendingReviewAmount")]
        public System.Decimal? PendingReviewAmount
        {
            get { return _PendingReviewAmount; }
            set { _PendingReviewAmount = value; }
        }


       [XmlElement(ElementName = "PendingModificationPreparerAmount")]
        public System.Decimal? PendingModificationPreparerAmount
        {
            get { return _PendingModificationPreparerAmount; }
            set { _PendingModificationPreparerAmount = value; }
        }

        
       [XmlElement(ElementName = "ReviewedAmount")]
        public System.Decimal? ReviewedAmount
        {
            get { return _ReviewedAmount; }
            set { _ReviewedAmount = value; }
        }

        
        [XmlElement(ElementName = "PendingApprovalAmount")]
        public System.Decimal? PendingApprovalAmount
        {
            get { return _PendingApprovalAmount; }
            set { _PendingApprovalAmount = value; }
        }

        
       [XmlElement(ElementName = "ApprovedAmount")]
        public System.Decimal? ApprovedAmount
        {
            get { return _ApprovedAmount; }
            set { _ApprovedAmount = value; }
        }

        
       [XmlElement(ElementName = "NotstartedAmount")]
        public System.Decimal? NotstartedAmount
        {
            get { return _NotstartedAmount; }
            set { _NotstartedAmount = value; }
        }


       [XmlElement(ElementName = "SysReconciledAmount")]
       public System.Decimal? SysReconciledAmount
       {
           get { return _SysReconciledAmount; }
           set { _SysReconciledAmount = value; }
       }

        
       [XmlElement(ElementName = "ReconciledAmount")]
       public System.Decimal? ReconciledAmount
       {
           get { return _ReconciledAmount; }
           set { _ReconciledAmount = value; }
       }


        [XmlElement(ElementName = "PendingModificationReviewerAmount")]
         public System.Decimal? PendingModificationReviewerAmount
         {
             get { return _PendingModificationReviewerAmount; }
             set { _PendingModificationReviewerAmount = value; }
         }


        [XmlElement(ElementName = "BaseCurrencyCode")]
        public System.String BaseCurrencyCode
        {
            get { return _BaseCurrencyCode; }
            set { _BaseCurrencyCode = value; }
        }

        [XmlElement(ElementName = "ReportingCurrencyCode")]
        public System.String ReportingCurrencyCode
        {
            get { return _ReportingCurrencyCode; }
            set { _ReportingCurrencyCode = value; }
        }

    }
}
