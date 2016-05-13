

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemArt CompanyAlert table
    /// </summary>
    [Serializable]
    public abstract class CompanyAlertInfoBase
    {

        protected System.String _AddedBy = "";
        protected System.Int16? _AlertID = 0;
        protected System.Int32? _CompanyAlertID = 0;
        protected System.Int32? _CompanyID = 0;
        protected System.DateTime? _DateAdded = DateTime.Now;
        protected System.DateTime? _DateRevised = DateTime.Now;
        protected System.Int32? _EndReconciliationPeriodID = 0;
        protected System.Boolean? _IsActive = false;
        protected System.Int16? _NoOfHours = 0;
        protected System.String _RevisedBy = "";
        protected System.Int32? _StartReconciliationPeriodID = 0;
        protected System.Int16? _Threshold = 0;
        protected System.Int16? _ThresholdTypeID = 0;




        public bool IsAddedByNull = true;


        public bool IsAlertIDNull = true;


        public bool IsCompanyAlertIDNull = true;


        public bool IsCompanyIDNull = true;


        public bool IsDateAddedNull = true;


        public bool IsDateRevisedNull = true;


        public bool IsEndReconciliationPeriodIDNull = true;


        public bool IsIsActiveNull = true;


        public bool IsNoOfHoursNull = true;


        public bool IsRevisedByNull = true;


        public bool IsStartReconciliationPeriodIDNull = true;


        public bool IsThresholdNull = true;


        public bool IsThresholdTypeIDNull = true;

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

        [XmlElement(ElementName = "AlertID")]
        public virtual System.Int16? AlertID
        {
            get
            {
                return this._AlertID;
            }
            set
            {
                this._AlertID = value;

                this.IsAlertIDNull = false;
            }
        }

        [XmlElement(ElementName = "CompanyAlertID")]
        public virtual System.Int32? CompanyAlertID
        {
            get
            {
                return this._CompanyAlertID;
            }
            set
            {
                this._CompanyAlertID = value;

                this.IsCompanyAlertIDNull = false;
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

        [XmlElement(ElementName = "EndReconciliationPeriodID")]
        public virtual System.Int32? EndReconciliationPeriodID
        {
            get
            {
                return this._EndReconciliationPeriodID;
            }
            set
            {
                this._EndReconciliationPeriodID = value;

                this.IsEndReconciliationPeriodIDNull = false;
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

        [XmlElement(ElementName = "NoOfHours")]
        public virtual System.Int16? NoOfHours
        {
            get
            {
                return this._NoOfHours;
            }
            set
            {
                this._NoOfHours = value;

                this.IsNoOfHoursNull = false;
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

        [XmlElement(ElementName = "StartReconciliationPeriodID")]
        public virtual System.Int32? StartReconciliationPeriodID
        {
            get
            {
                return this._StartReconciliationPeriodID;
            }
            set
            {
                this._StartReconciliationPeriodID = value;

                this.IsStartReconciliationPeriodIDNull = false;
            }
        }

        [XmlElement(ElementName = "Threshold")]
        public virtual System.Int16? Threshold
        {
            get
            {
                return this._Threshold;
            }
            set
            {
                this._Threshold = value;

                this.IsThresholdNull = false;
            }
        }

        [XmlElement(ElementName = "ThresholdTypeID")]
        public virtual System.Int16? ThresholdTypeID
        {
            get
            {
                return this._ThresholdTypeID;
            }
            set
            {
                this._ThresholdTypeID = value;

                this.IsThresholdTypeIDNull = false;
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
