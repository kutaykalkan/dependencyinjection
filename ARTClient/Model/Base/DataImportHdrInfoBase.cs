

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemArt DataImportHdr table
    /// </summary>
    [Serializable]
    public abstract class DataImportHdrInfoBase
    {

        protected System.String _AddedBy = "";
        protected System.Int32? _CompanyID =null;
        protected System.Int32? _DataImportID = 0;
        protected System.String _DataImportName = "";
        protected System.Int16? _DataImportStatusID = 0;
        protected System.Int16? _DataImportTypeID = 0;
        protected System.DateTime? _DateAdded = DateTime.Now;
        protected System.DateTime? _DateRevised = DateTime.Now;
        protected System.String _FileName = "";
        protected System.Decimal? _FileSize = 0.00M;
        protected System.DateTime? _ForceCommitDate = DateTime.Now;
        protected System.String _HostName = "";
        protected System.Boolean? _IsActive = false;
        protected System.Boolean? _IsForceCommit = false;
        protected System.String _PhysicalPath = "";
        protected System.Int32? _RecordsImported = 0;
        //Modified By Harsh due to field name change
        //protected System.Int32? _ReonciliationPeriodID = 0;
        protected System.Int32? _ReconciliationPeriodID = null;
        protected System.String _RevisedBy = "";
        protected System.String _NotifySuccessEmailIDs = "";
        protected System.String _NotifySuccessUserEmailIDs = "";
        protected System.String _NotifyFailureEmailIDs = "";
        protected System.String _NotifyFailureUserEmailIDs = "";
        protected System.Decimal? _NetValue = 0;

        protected System.Boolean? _IsHidden = false;


        public bool IsAddedByNull = true;


        public bool IsCompanyIDNull = true;


        public bool IsDataImportIDNull = true;


        public bool IsDataImportNameNull = true;


        public bool IsDataImportStatusIDNull = true;


        public bool IsDataImportTypeIDNull = true;


        public bool IsDateAddedNull = true;


        public bool IsDateRevisedNull = true;


        public bool IsFileNameNull = true;


        public bool IsFileSizeNull = true;


        public bool IsForceCommitDateNull = true;


        public bool IsHostNameNull = true;


        public bool IsIsActiveNull = true;


        public bool IsIsForceCommitNull = true;


        public bool IsPhysicalPathNull = true;


        public bool IsRecordsImportedNull = true;
        
        //Modified by harsh on 18th feb due to field name change
        //public bool IsReonciliationPeriodIDNull = true;
        public bool IsReconciliationPeriodIDNull = true;


        public bool IsRevisedByNull = true;

        public bool IsNetValueNull = true;

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

        [XmlElement(ElementName = "DataImportName")]
        public virtual System.String DataImportName
        {
            get
            {
                return this._DataImportName;
            }
            set
            {
                this._DataImportName = value;

                this.IsDataImportNameNull = (_DataImportName == null);
            }
        }

        [XmlElement(ElementName = "DataImportStatusID")]
        public virtual System.Int16? DataImportStatusID
        {
            get
            {
                return this._DataImportStatusID;
            }
            set
            {
                this._DataImportStatusID = value;

                this.IsDataImportStatusIDNull = false;
            }
        }

        [XmlElement(ElementName = "DataImportTypeID")]
        public virtual System.Int16? DataImportTypeID
        {
            get
            {
                return this._DataImportTypeID;
            }
            set
            {
                this._DataImportTypeID = value;

                this.IsDataImportTypeIDNull = false;
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

        [XmlElement(ElementName = "FileName")]
        public virtual System.String FileName
        {
            get
            {
                return this._FileName;
            }
            set
            {
                this._FileName = value;

                this.IsFileNameNull = (_FileName == null);
            }
        }

        [XmlElement(ElementName = "FileSize")]
        public virtual System.Decimal? FileSize
        {
            get
            {
                return this._FileSize;
            }
            set
            {
                this._FileSize = value;

                this.IsFileSizeNull = false;
            }
        }

        [XmlElement(ElementName = "ForceCommitDate")]
        public virtual System.DateTime? ForceCommitDate
        {
            get
            {
                return this._ForceCommitDate;
            }
            set
            {
                this._ForceCommitDate = value;

                this.IsForceCommitDateNull = (_ForceCommitDate == DateTime.MinValue);
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

        [XmlElement(ElementName = "IsForceCommit")]
        public virtual System.Boolean? IsForceCommit
        {
            get
            {
                return this._IsForceCommit;
            }
            set
            {
                this._IsForceCommit = value;

                this.IsIsForceCommitNull = false;
            }
        }

        [XmlElement(ElementName = "PhysicalPath")]
        public virtual System.String PhysicalPath
        {
            get
            {
                return this._PhysicalPath;
            }
            set
            {
                this._PhysicalPath = value;

                this.IsPhysicalPathNull = (_PhysicalPath == null);
            }
        }

        [XmlElement(ElementName = "RecordsImported")]
        public virtual System.Int32? RecordsImported
        {
            get
            {
                return this._RecordsImported;
            }
            set
            {
                this._RecordsImported = value;

                this.IsRecordsImportedNull = false;
            }
        }

        //Modified By Harsh on 18th feb 2010 due to field name change
        //[XmlElement(ElementName = "ReonciliationPeriodID")]
        //public virtual System.Int32? ReonciliationPeriodID
        //{
        //    get
        //    {
        //        return this._ReonciliationPeriodID;
        //    }
        //    set
        //    {
        //        this._ReonciliationPeriodID = value;

        //        this.IsReonciliationPeriodIDNull = false;
        //    }
        //}
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
      
        [XmlElement(ElementName = "NotifySuccessEmailIDs")]
        public virtual System.String NotifySuccessEmailIDs
        {
            get
            {
                return this._NotifySuccessEmailIDs;
            }
            set
            {
                this._NotifySuccessEmailIDs = value;

               
            }
        }

        [XmlElement(ElementName = "NotifySuccessUserEmailIDs")]
        public virtual System.String NotifySuccessUserEmailIDs
        {
            get
            {
                return this._NotifySuccessUserEmailIDs;
            }
            set
            {
                this._NotifySuccessUserEmailIDs = value;

                
            }
        }

        [XmlElement(ElementName = "NotifyFailureEmailIDs")]
        public virtual System.String NotifyFailureEmailIDs
        {
            get
            {
                return this._NotifyFailureEmailIDs;
            }
            set
            {
                this._NotifyFailureEmailIDs = value;

               
            }
        }

        [XmlElement(ElementName = "NotifyFailureUserEmailIDs")]
        public virtual System.String NotifyFailureUserEmailIDs
        {
            get
            {
                return this._NotifyFailureUserEmailIDs;
            }
            set
            {
                this._NotifyFailureUserEmailIDs = value;

                
            }
        }

        [XmlElement(ElementName = "NetValue")]
        public virtual System.Decimal? NetValue
        {
            get
            {
                return this._NetValue;
            }
            set
            {
                this._NetValue = value;
                this.IsNetValueNull = false;
            }
        }


        [XmlElement(ElementName = "IsHidden")]
        public virtual System.Boolean? IsHidden
        {
            get
            {
                return this._IsHidden;
            }
            set
            {
                this._IsHidden = value;
               
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
