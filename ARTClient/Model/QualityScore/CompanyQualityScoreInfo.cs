
using System;
using Adapdev.Text;
using System.Runtime.Serialization;
using SkyStem.ART.Client.Model.Base;
using SkyStem.ART.Client.Model.QualityScore.Base;

namespace SkyStem.ART.Client.Model.QualityScore
{    

	/// <summary>
	/// An object representation of the SkyStemART CompanyQualityScore table
	/// </summary>
	[Serializable]
	[DataContract]
	public class CompanyQualityScoreInfo : CompanyQualityScoreInfoBase
	{
        [DataMember]
        public int RowNumber { get; set; }
	}
}
