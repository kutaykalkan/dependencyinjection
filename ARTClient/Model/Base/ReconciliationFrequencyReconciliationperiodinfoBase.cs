

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemArt ReconciliationFrequencyReconciliationperiod table
    /// </summary>
    [Serializable]
    public abstract class ReconciliationFrequencyReconciliationperiodInfoBase
    {
        protected System.Int32? _ReconciliationFrequencyID = null;
        protected System.Int32? _ReconciliationFrequencyreconciliatinPeriodID = null;
        protected System.Int32? _ReconciliationPeriodID = null;

        public bool IsReconciliationFrequencyIDNull = true;


        public bool IsReconciliationFrequencyreconciliatinPeriodIDNull = true;


        public bool IsReconciliationPeriodIDNull = true;

        [XmlElement(ElementName = "ReconciliationFrequencyID")]
        public virtual System.Int32? ReconciliationFrequencyID
        {
            get
            {
                return this._ReconciliationFrequencyID;
            }
            set
            {
                this._ReconciliationFrequencyID = value;

                this.IsReconciliationFrequencyIDNull = false;
            }
        }

        [XmlElement(ElementName = "ReconciliationFrequencyreconciliatinPeriodID")]
        public virtual System.Int32? ReconciliationFrequencyreconciliatinPeriodID
        {
            get
            {
                return this._ReconciliationFrequencyreconciliatinPeriodID;
            }
            set
            {
                this._ReconciliationFrequencyreconciliatinPeriodID = value;

                this.IsReconciliationFrequencyreconciliatinPeriodIDNull = false;
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


        /// <summary>
        /// Returns a string representation of the object, displaying all property and field names and values.
        /// </summary>
        public override string ToString()
        {
            return StringUtil.ToString(this);
        }

    }
}
