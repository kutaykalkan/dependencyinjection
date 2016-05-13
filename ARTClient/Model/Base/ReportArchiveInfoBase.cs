

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemART ReportArchive table
    /// </summary>
    [Serializable]
    public abstract class ReportArchiveInfoBase
    {
        protected System.String _AddedBy = null;
        protected System.String _Comments = null;
        protected System.DateTime? _DateAdded = null;
        protected System.String _EmailList = null;
        protected System.Boolean? _IsActive = null;
        protected System.Int32? _ReconciliationPeriodID = null;
        protected System.Int64? _ReportArchiveID = null;
        protected System.Int16? _ReportArchiveTypeID = null;
        protected System.DateTime? _ReportCreateDateTime = null;
        protected System.Byte[] _ReportData = null;
        protected System.Int16? _ReportID = null;
        protected System.Int16? _RoleID = null;
        protected System.Int32? _UserID = null;




        public bool IsAddedByNull = true;


        public bool IsCommentsNull = true;


        public bool IsDateAddedNull = true;


        public bool IsEmailListNull = true;


        public bool IsIsActiveNull = true;


        public bool IsReconciliationPeriodIDNull = true;


        public bool IsReportArchiveIDNull = true;


        public bool IsReportArchiveTypeIDNull = true;


        public bool IsReportCreateDateTimeNull = true;


        public bool IsReportDataNull = true;


        public bool IsReportIDNull = true;


        public bool IsRoleIDNull = true;


        public bool IsUserIDNull = true;

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

        [XmlElement(ElementName = "Comments")]
        public virtual System.String Comments
        {
            get
            {
                return this._Comments;
            }
            set
            {
                this._Comments = value;

                this.IsCommentsNull = (_Comments == null);
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

        [XmlElement(ElementName = "EmailList")]
        public virtual System.String EmailList
        {
            get
            {
                return this._EmailList;
            }
            set
            {
                this._EmailList = value;

                this.IsEmailListNull = (_EmailList == null);
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

        [XmlElement(ElementName = "ReportArchiveID")]
        public virtual System.Int64? ReportArchiveID
        {
            get
            {
                return this._ReportArchiveID;
            }
            set
            {
                this._ReportArchiveID = value;

                this.IsReportArchiveIDNull = false;
            }
        }

        [XmlElement(ElementName = "ReportArchiveTypeID")]
        public virtual System.Int16? ReportArchiveTypeID
        {
            get
            {
                return this._ReportArchiveTypeID;
            }
            set
            {
                this._ReportArchiveTypeID = value;

                this.IsReportArchiveTypeIDNull = false;
            }
        }

        [XmlElement(ElementName = "ReportCreateDateTime")]
        public virtual System.DateTime? ReportCreateDateTime
        {
            get
            {
                return this._ReportCreateDateTime;
            }
            set
            {
                this._ReportCreateDateTime = value;

                this.IsReportCreateDateTimeNull = (_ReportCreateDateTime == DateTime.MinValue);
            }
        }

        [XmlElement(ElementName = "ReportData")]
        public virtual System.Byte[] ReportData
        {
            get
            {
                return this._ReportData;
            }
            set
            {
                this._ReportData = value;

                this.IsReportDataNull = false;
            }
        }

        [XmlElement(ElementName = "ReportID")]
        public virtual System.Int16? ReportID
        {
            get
            {
                return this._ReportID;
            }
            set
            {
                this._ReportID = value;

                this.IsReportIDNull = false;
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

                this.IsRoleIDNull = false;
            }
        }

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

                this.IsUserIDNull = false;
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
