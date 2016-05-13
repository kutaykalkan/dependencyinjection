
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemArt CompanyCapability table
	/// </summary>
	[Serializable]
	[DataContract]
	public class CompanyCapabilityInfo : CompanyCapabilityInfoBase
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

        [XmlElement(ElementName = "CapabilityAttributeValueList")]
        [DataMember]
        public List<CapabilityAttributeValueInfo> CapabilityAttributeValueInfoList { get; set; }
    }//end of class
}//end of namespace
