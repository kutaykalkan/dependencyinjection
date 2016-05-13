
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemArt CompanyUnexplainedVarianceThreshold table
	/// </summary>
	[Serializable]
	[DataContract]
	public class CompanyUnexplainedVarianceThresholdInfo : CompanyUnexplainedVarianceThresholdInfoBase
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
