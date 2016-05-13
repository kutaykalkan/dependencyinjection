
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemArt CompanyAlert table
	/// </summary>
	[Serializable]
	[DataContract]
	public class CompanyAlertInfo : CompanyAlertInfoBase
	{
        [DataMember]
        public int RecPeriodID { get; set; }

        [DataMember]
        public int AlertLabelID { get; set; }

        [DataMember]
        public int AlertSubjectLabelID { get; set; }

        [DataMember]
        public int CompanyNameLabelID { get; set; }
    }
}
