

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemArt ReconciliationPeriodStatusMst table
    /// </summary>
    [Serializable]
    public abstract class ReconciliationPeriodStatusMstInfoBase : MultilingualInfo
    {


        protected System.String _AddedBy = "";
        protected System.DateTime? _DateAdded = DateTime.Now;
        protected System.DateTime? _DateRevised = DateTime.Now;
        protected System.String _HostName = "";
        protected System.Boolean? _IsActive = false;
        //protected System.String _ReconciliationPeriodStatus = "";
        protected System.Int16? _ReconciliationPeriodStatusID = 0;
        //protected System.Int32 _ReconciliationPeriodStatusLabelID = 0;
        protected System.String _RevisedBy = "";


        public bool IsAddedByNull = true;


        public bool IsDateAddedNull = true;


        public bool IsDateRevisedNull = true;


        public bool IsHostNameNull = true;


        public bool IsIsActiveNull = true;


        public bool IsReconciliationPeriodStatusNull = true;


        public bool IsReconciliationPeriodStatusIDNull = true;


        public bool IsReconciliationPeriodStatusLabelIDNull = true;


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

        [XmlElement(ElementName = "ReconciliationPeriodStatus")]
        public virtual System.String ReconciliationPeriodStatus
        {
            get
            {
                return this.Name;
            }
            set
            {
                this.Name = value;
                this.IsReconciliationPeriodStatusNull = (this.Name == null);
            }
        }

        [XmlElement(ElementName = "ReconciliationPeriodStatusID")]
        public virtual System.Int16? ReconciliationPeriodStatusID
        {
            get
            {
                return this._ReconciliationPeriodStatusID;
            }
            set
            {
                this._ReconciliationPeriodStatusID = value;

                this.IsReconciliationPeriodStatusIDNull = false;
            }
        }

        [XmlElement(ElementName = "ReconciliationPeriodStatusLabelID")]
        public virtual System.Int32? ReconciliationPeriodStatusLabelID
        {
            get
            {
                return this.LabelID;
            }
            set
            {
                this.LabelID = value;
                this.IsReconciliationPeriodStatusLabelIDNull = false;
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
