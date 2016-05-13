
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemArt CompanyAlertDetailUser table
	/// </summary>
	[Serializable]
	[DataContract]
	public class CompanyAlertDetailUserInfo : CompanyAlertDetailUserInfoBase
	{
        [DataMember]
        public short AlertID { get; set; }
        [DataMember]
        public int LanguageID { get; set; }
        [DataMember]
        public int CompanyID { get; set; }
        [DataMember]
        public int RecPeriodID { get; set; }
        [DataMember]
        public string AlertDescription { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string EmailID { get; set; }
        [DataMember]
        public int AlertSubjectLabelID { get; set; }
        [DataMember]
        public int CompanyNameLabelID { get; set; }
        [DataMember]
        public DateTime? PeriodEndDate { get; set; }
	}
}
