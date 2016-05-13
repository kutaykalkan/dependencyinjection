using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Client.Model.Base;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{
    public class ReconciliationStatusFSCaptionInfo : MultilingualInfo
    {
        protected System.Int32? _FSCaptionID = 0;
        protected System.Int32? _TotalCount = 0;

        protected System.Int32? _Prepared = 0;
        protected System.Int32? _InProgress = 0;
        protected System.Int32? _PendingReview = 0;
        protected System.Int32? _PendingModificationPreparer = 0;
        protected System.Int32? _Reviewed = 0;
        protected System.Int32? _PendingApproval = 0;
        protected System.Int32? _Approved = 0;
        protected System.Int32? _Notstarted = 0;
        protected System.Int32? _SysReconciled = 0;
        protected System.Int32? _Reconciled = 0;
        protected System.Int32? _PendingModificationReviewer = 0;


        [XmlElement(ElementName = "FSCaptionID")]
        public virtual System.Int32? FSCaptionID
        {
            get
            {
                return this._FSCaptionID;
            }
            set
            {
                this._FSCaptionID = value;
            }
        }

        [XmlElement(ElementName = "FSCaption")]
        public virtual System.String FSCaption
        {
            get
            {
                return this.Name;
            }
            set
            {
                this.Name = value;
            }
        }

        [XmlElement(ElementName = "FSCaptionLabelID")]
        public virtual System.Int32? FSCaptionLabelID
        {
            get
            {
                return this.LabelID;
            }
            set
            {
                this.LabelID = value;
            }
        }

        [XmlElement(ElementName = "TotalCount")]
        public virtual System.Int32? TotalCount
        {
            get
            {
                return this._TotalCount;
            }
            set
            {
                this._TotalCount = value;
            }
        }

        [XmlElement(ElementName = "Prepared")]
        public virtual System.Int32? Prepared
        {
            get
            {
                return this._Prepared;
            }
            set
            {
                this._Prepared = value;
            }
        }

        [XmlElement(ElementName = "InProgress")]
        public virtual System.Int32? InProgress
        {
            get
            {
                return this._InProgress;
            }
            set
            {
                this._InProgress = value;
            }
        }

        [XmlElement(ElementName = "PendingReview")]
        public virtual System.Int32? PendingReview
        {
            get
            {
                return this._PendingReview;
            }
            set
            {
                this._PendingReview = value;
            }
        }


        [XmlElement(ElementName = "PendingModificationPreparer")]
        public virtual System.Int32? PendingModificationPreparer
        {
            get
            {
                return this._PendingModificationPreparer;
            }
            set
            {
                this._PendingModificationPreparer = value;
            }
        }


        [XmlElement(ElementName = "Reviewed")]
        public virtual System.Int32? Reviewed
        {
            get
            {
                return this._Reviewed;
            }
            set
            {
                this._Reviewed = value;
            }
        }


        [XmlElement(ElementName = "PendingApproval")]
        public virtual System.Int32? PendingApproval
        {
            get
            {
                return this._PendingApproval;
            }
            set
            {
                this._PendingApproval = value;
            }
        }


        [XmlElement(ElementName = "Approved")]
        public virtual System.Int32? Approved
        {
            get
            {
                return this._Approved;
            }
            set
            {
                this._Approved = value;
            }
        }


        [XmlElement(ElementName = "Notstarted")]
        public virtual System.Int32? Notstarted
        {
            get
            {
                return this._Notstarted;
            }
            set
            {
                this._Notstarted = value;
            }
        }


        [XmlElement(ElementName = "SysReconciled")]
        public virtual System.Int32? SysReconciled
        {
            get
            {
                return this._SysReconciled;
            }
            set
            {
                this._SysReconciled = value;
            }
        }

        [XmlElement(ElementName = "Reconciled")]
        public virtual System.Int32? Reconciled
        {
            get
            {
                return this._Reconciled;
            }
            set
            {
                this._Reconciled = value;
            }
        }


        [XmlElement(ElementName = "PendingModificationReviewer")]
        public virtual System.Int32? PendingModificationReviewer
        {
            get
            {
                return this._PendingModificationReviewer;
            }
            set
            {
                this._PendingModificationReviewer = value;
            }
        }


    }
}
