using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;

namespace SkyStem.ART.Client.Model.Report
{

    /// <summary>
    /// An object representation of the SkyStemArt AccountHdr table
    /// </summary>
    [Serializable]
    public class ReconciliationStatusCountReportInfo
    {
        #region UserRole fields

        protected System.Int32? _UserID = null;
        protected System.Int16? _RoleID = null;
        protected System.String _FirstName = "";
        protected System.String _LastName = "";
        protected System.Int32? _RoleLabelID = null;
        protected System.String _Role = "";

        [XmlElement(ElementName = "UserID")]
        public virtual System.Int32? UserID
        {
            get
            {
                return this._UserID;
            }
            set
            {
                this._UserID = value;
            }
        }
        [XmlElement(ElementName = "RoleID")]
        public virtual System.Int16? RoleID
        {
            get
            {
                return this._RoleID;
            }
            set
            {
                this._RoleID = value;
            }
        }
        [XmlElement(ElementName = "FirstName")]
        public virtual System.String FirstName
        {
            get
            {
                return this._FirstName;
            }
            set
            {
                this._FirstName = value;
            }
        }
        [XmlElement(ElementName = "LastName")]
        public virtual System.String LastName
        {
            get
            {
                return this._LastName;
            }
            set
            {
                this._LastName = value;
            }
        }
        [XmlElement(ElementName = "RoleLabelID")]
        public virtual System.Int32? RoleLabelID
        {
            get
            {
                return this._RoleLabelID;
            }
            set
            {
                this._RoleLabelID = value;
            }
        }
        [XmlElement(ElementName = "Role")]
        public virtual System.String Role
        {
            get
            {
                return this._Role;
            }
            set
            {
                this._Role = value;
            }
        }
        #endregion

        protected System.Int16? _CountTotalAccountAssigned = null;
        [XmlElement(ElementName = "CountTotalAccountAssigned")]
        public virtual System.Int16? CountTotalAccountAssigned
        {
            get
            {
                return this._CountTotalAccountAssigned;
            }
            set
            {
                this._CountTotalAccountAssigned = value;
            }
        }
        protected System.Int16? _CountPrepared = null;
        [XmlElement(ElementName = "CountPrepared")]
        public virtual System.Int16? CountPrepared
        {
            get
            {
                return this._CountPrepared;
            }
            set
            {
                this._CountPrepared = value;
            }
        }
        protected System.Int16? _CountInProgress = null;
        [XmlElement(ElementName = "CountInProgress")]
        public virtual System.Int16? CountInProgress
        {
            get
            {
                return this._CountInProgress;
            }
            set
            {
                this._CountInProgress = value;
            }
        }
        protected System.Int16? _CountPendingReview = null;
        [XmlElement(ElementName = "CountPendingReview")]
        public virtual System.Int16? CountPendingReview
        {
            get
            {
                return this._CountPendingReview;
            }
            set
            {
                this._CountPendingReview = value;
            }
        }
        protected System.Int16? _CountPendingModificationPreparer = null;
        [XmlElement(ElementName = "CountPendingModificationPreparer")]
        public virtual System.Int16? CountPendingModificationPreparer
        {
            get
            {
                return this._CountPendingModificationPreparer;
            }
            set
            {
                this._CountPendingModificationPreparer = value;
            }
        }
        protected System.Int16? _CountReviewed = null;
        [XmlElement(ElementName = "CountReviewed")]
        public virtual System.Int16? CountReviewed
        {
            get
            {
                return this._CountReviewed;
            }
            set
            {
                this._CountReviewed = value;
            }
        }
        protected System.Int16? _CountPendingApproval = null;
        [XmlElement(ElementName = "CountPendingApproval")]
        public virtual System.Int16? CountPendingApproval
        {
            get
            {
                return this._CountPendingApproval;
            }
            set
            {
                this._CountPendingApproval = value;
            }
        }
        protected System.Int16? _CountApproved = null;
        [XmlElement(ElementName = "CountApproved")]
        public virtual System.Int16? CountApproved
        {
            get
            {
                return this._CountApproved;
            }
            set
            {
                this._CountApproved = value;
            }
        }
        protected System.Int16? _CountNotStarted = null;
        [XmlElement(ElementName = "CountNotStarted")]
        public virtual System.Int16? CountNotStarted
        {
            get
            {
                return this._CountNotStarted;
            }
            set
            {
                this._CountNotStarted = value;
            }
        }
        protected System.Int16? _CountSysReconciled = null;
        [XmlElement(ElementName = "CountSysReconciled")]
        public virtual System.Int16? CountSysReconciled
        {
            get
            {
                return this._CountSysReconciled;
            }
            set
            {
                this._CountSysReconciled = value;
            }
        }
        protected System.Int16? _CountReconciled = null;
        [XmlElement(ElementName = "CountReconciled")]
        public virtual System.Int16? CountReconciled
        {
            get
            {
                return this._CountReconciled;
            }
            set
            {
                this._CountReconciled = value;
            }
        }
        protected System.Int16? _CountPendingModificationReviewer = null;
        [XmlElement(ElementName = "CountPendingModificationReviewer")]
        public virtual System.Int16? CountPendingModificationReviewer
        {
            get
            {
                return this._CountPendingModificationReviewer;
            }
            set
            {
                this._CountPendingModificationReviewer = value;
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
