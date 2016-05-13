
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemArt RiskRatingReconciliationFrequency table
	/// </summary>
	[Serializable]
	[DataContract]
	public class RiskRatingReconciliationFrequencyInfo : RiskRatingReconciliationFrequencyInfoBase
	{
        protected System.Boolean? _IsCarryForwardedFromPreviousRecPeriod = false;
        public bool IsIsCarryForwardedFromPreviousRecPeriodNull = true;
        [XmlElement(ElementName = "IsCarryForwardedFromPreviousRecPeriod")]
        public virtual System.Boolean? IsCarryForwardedFromPreviousRecPeriod
        {
            get
            {
                return this._IsCarryForwardedFromPreviousRecPeriod;
            }
            set
            {
                this._IsCarryForwardedFromPreviousRecPeriod = value;

                this.IsIsCarryForwardedFromPreviousRecPeriodNull = false;
            }
        }
	}//end of class
}//end of namespace
