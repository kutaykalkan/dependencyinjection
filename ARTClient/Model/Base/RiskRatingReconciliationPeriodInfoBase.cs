

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemArt RiskRatingReconciliationPeriod table
    /// </summary>
    [Serializable]
    public abstract class RiskRatingReconciliationPeriodInfoBase
    {


        protected System.String _AddedBy = "";
        protected System.Int32? _CompanyID = 0;
        protected System.DateTime? _DateAdded = DateTime.Now;
        protected System.Int32? _ReconciliationPeriodID = 0;
        protected System.Int32? _EndReconciliationPeriodID = 0;
        protected System.Int16? _RiskRatingID = 0;
        protected System.Int32? _RiskRatingReconciliationPeriodID = 0;
        protected System.Int32? _StartReconciliationPeriodID = 0;




        public bool IsAddedByNull = true;


        public bool IsCompanyIDNull = true;


        public bool IsDateAddedNull = true;


        public bool IsReconciliationPeriodIDNull = true;

        public bool IsEndReconciliationPeriodIDNull = true;


        public bool IsRiskRatingIDNull = true;


        public bool IsRiskRatingReconciliationPeriodIDNull = true;


        public bool IsStartReconciliationPeriodIDNull = true;

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

        [XmlElement(ElementName = "RiskRatingID")]
        public virtual System.Int16? RiskRatingID
        {
            get
            {
                return this._RiskRatingID;
            }
            set
            {
                this._RiskRatingID = value;

                this.IsRiskRatingIDNull = false;
            }
        }

        [XmlElement(ElementName = "RiskRatingReconciliationPeriodID")]
        public virtual System.Int32? RiskRatingReconciliationPeriodID
        {
            get
            {
                return this._RiskRatingReconciliationPeriodID;
            }
            set
            {
                this._RiskRatingReconciliationPeriodID = value;

                this.IsRiskRatingReconciliationPeriodIDNull = false;
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


        /// <summary>
        /// Returns a string representation of the object, displaying all property and field names and values.
        /// </summary>
        public override string ToString()
        {
            return StringUtil.ToString(this);
        }




    }
}
