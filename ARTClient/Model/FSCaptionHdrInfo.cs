
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemArt FSCaptionHdr table
	/// </summary>
	[Serializable]
	[DataContract]
	public class FSCaptionHdrInfo : FSCaptionHdrInfoBase
	{
        [DataMember]
        public DateTime CreationPeriodEndDate { get; set; }
	}
}
