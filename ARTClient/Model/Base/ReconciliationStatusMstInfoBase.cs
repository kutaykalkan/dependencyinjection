

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemArt ReconciliationStatusMst table
    /// </summary>
    [Serializable]
    public abstract class ReconciliationStatusMstInfoBase : MultilingualInfo
    {
        
        protected System.String _AddedBy = "";
        protected System.DateTime? _DateAdded = DateTime.Now;
        protected System.DateTime? _DateRevised = DateTime.Now;
        protected System.String _Description = "";
        protected System.String _HostName = "";
        protected System.Boolean? _IsActive = false;
        protected System.String _ReconciliationStatus = "";
        protected System.Int16? _ReconciliationStatusID = 0;
        protected System.Int32? _ReconciliationStatusLabelID = 0;
        protected System.String _RevisedBy = "";
        public bool IsAddedByNull = true;
        public bool IsDateAddedNull = true;
        public bool IsDateRevisedNull = true;
        public bool IsDescriptionNull = true;
        public bool IsHostNameNull = true;
        public bool IsIsActiveNull = true;
        public bool IsReconciliationStatusNull = true;
        public bool IsReconciliationStatusIDNull = true;
        public bool IsReconciliationStatusLabelIDNull = true;
        public bool IsRevisedByNull = true;

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

        [XmlElement(ElementName = "Description")]
        public virtual System.String Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this._Description = value;

                this.IsDescriptionNull = (_Description == null);
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

        [XmlElement(ElementName = "ReconciliationStatus")]
        public virtual System.String ReconciliationStatus
        {
            get
            {
                //return this._ReconciliationStatus;
                return this.Name;
            }
            set
            {
                //this._ReconciliationStatus = value;
                this.Name = value;
                //this.IsReconciliationStatusNull = (_ReconciliationStatus == null);
                this.IsReconciliationStatusNull = (Name == null);
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

        [XmlElement(ElementName = "ReconciliationStatusLabelID")]
        public virtual System.Int32? ReconciliationStatusLabelID
        {
            get
            {
                //return this._ReconciliationStatusLabelID;
                return this.LabelID;
            }
            set
            {
                //this._ReconciliationStatusLabelID = value;
                //this.IsReconciliationStatusLabelIDNull = false;
                this.LabelID = value;
                this.IsReconciliationStatusLabelIDNull = false;
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


        /// <summary>
        /// Returns a string representation of the object, displaying all property and field names and values.
        /// </summary>
        public override string ToString()
        {
            return StringUtil.ToString(this);
        }

    }
}
