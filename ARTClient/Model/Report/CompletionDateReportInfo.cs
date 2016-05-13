using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Client.Model.Base;
using System.Xml;
using System.Xml.Serialization;
using Adapdev.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model.Report
{
    [Serializable]
    public class CompletionDateReportInfo : OrganizationalHierarchyInfo
    {
        protected System.Int64? _AccountID = null;
        protected System.String _AccountName = "";
        protected System.Int32? _AccountNameLabelID = null;
        protected System.String _AccountNumber = "";
        protected System.Int16? _AccountTypeID = null;
        protected System.Int32? _FSCaptionID = null;
        protected System.Int16? _ReconciliationStatusID = null;
        protected System.Int32? _ReconciliationStatusLabelID = null;
        protected System.String _ReconciliationStatus = "";
        protected System.DateTime? _DatePrepared = null;
        protected System.DateTime? _DateReviewed = null;
        protected System.DateTime? _DateApproved = null;
        protected System.DateTime? _DateReconciled = null;
        protected System.String _PreparedBy;
        protected System.String _ReviewedBy;
        protected System.String _ApprovedBy;
        protected System.String _ReconciledBy;
        protected System.String _SysReconciledBy;

        protected Boolean? _IsSRA = null;




        #region NetAccount

        protected System.Int32? _NetAccountID = null;
        [XmlElement(ElementName = "NetAccountID")]
        public virtual System.Int32? NetAccountID
        {
            get
            {
                return this._NetAccountID;
            }
            set
            {
                this._NetAccountID = value;
            }
        }

        protected System.Int32? _NetAccountLabelID = null;
        [XmlElement(ElementName = "NetAccountLabelID")]
        public virtual System.Int32? NetAccountLabelID
        {
            get
            {
                return this._NetAccountLabelID;
            }
            set
            {
                this._NetAccountLabelID = value;
            }
        }

        protected System.String _NetAccount = "";
        [XmlElement(ElementName = "NetAccount")]
        public virtual System.String NetAccount
        {
            get
            {
                return this._NetAccount;
            }
            set
            {
                this._NetAccount = value;
            }
        }

        #endregion

        [DataMember]
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
            }
        }

        [DataMember]
        [XmlElement(ElementName = "AccountName")]
        public virtual System.String AccountName
        {
            get
            {
                return this._AccountName;
            }
            set
            {
                this._AccountName = value;
            }
        }

        [DataMember]
        [XmlElement(ElementName = "AccountNameLabelID")]
        public virtual System.Int32? AccountNameLabelID
        {
            get
            {
                return this._AccountNameLabelID;
            }
            set
            {
                this._AccountNameLabelID = value;
            }
        }
        
        [DataMember]
        [XmlElement(ElementName = "AccountNumber")]
        public virtual System.String AccountNumber
        {
            get
            {
                return this._AccountNumber;
            }
            set
            {
                this._AccountNumber = value;
            }
        }

        [DataMember]
        [XmlElement(ElementName = "AccountTypeID")]
        public virtual System.Int16? AccountTypeID
        {
            get
            {
                return this._AccountTypeID;
            }
            set
            {
                this._AccountTypeID = value;
            }
        }

        [DataMember]
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

        [DataMember]
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
            }
        }

        [DataMember]
        [XmlElement(ElementName = "ReconciliationStatusLabelID")]
        public virtual System.Int32? ReconciliationStatusLabelID
        {
            get
            {
                return this._ReconciliationStatusLabelID;
            }
            set
            {
                this._ReconciliationStatusLabelID = value;
            }
        }

        [DataMember]
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

        [DataMember]
        [XmlElement(ElementName = "PreparedBy")]
        public virtual System.String PreparedBy
        {
            get
            {
                return this._PreparedBy;
            }
            set
            {
                this._PreparedBy = value;
            }
        }

        [DataMember]
        [XmlElement(ElementName = "ReviewedBy")]
        public virtual System.String ReviewedBy
        {
            get
            {
                return this._ReviewedBy;
            }
            set
            {
                this._ReviewedBy = value;
            }
        }

        [DataMember]
        [XmlElement(ElementName = "ApprovedBy")]
        public virtual System.String ApprovedBy
        {
            get
            {
                return this._ApprovedBy;
            }
            set
            {
                this._ApprovedBy = value;
            }
        }

        [DataMember]
        [XmlElement(ElementName = "ReconciledBy")]
        public virtual System.String ReconciledBy
        {
            get
            {
                return this._ReconciledBy;
            }
            set
            {
                this._ReconciledBy = value;
            }
        }

        [DataMember]
        [XmlElement(ElementName = "SysReconciledBy")]
        public virtual System.String SysReconciledBy
        {
            get
            {
                return this._SysReconciledBy;
            }
            set
            {
                this._SysReconciledBy = value;
            }
        }

        [DataMember]
        [XmlElement(ElementName = "DatePrepared")]
        public virtual System.DateTime? DatePrepared
        {
            get
            {
                return this._DatePrepared;
            }
            set
            {
                this._DatePrepared = value;
            }
        }

        [DataMember]
        [XmlElement(ElementName = "DateReviewed")]
        public virtual System.DateTime? DateReviewed
        {
            get
            {
                return this._DateReviewed;
            }
            set
            {
                this._DateReviewed = value;
            }
        }

        [DataMember]
        [XmlElement(ElementName = "DateApproved")]
        public virtual System.DateTime? DateApproved
        {
            get
            {
                return this._DateApproved;
            }
            set
            {
                this._DateApproved = value;
            }
        }

        [DataMember]
        [XmlElement(ElementName = "DateReconciled")]
        public virtual System.DateTime? DateReconciled
        {
            get
            {
                return this._DateReconciled;
            }
            set
            {
                this._DateReconciled = value;
            }
        }

        [DataMember]
        [XmlElement(ElementName = "IsSRA")]
        public virtual System.Boolean? IsSRA
        {
            get
            {
                return this._IsSRA;
            }
            set
            {
                this._IsSRA = value;
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
