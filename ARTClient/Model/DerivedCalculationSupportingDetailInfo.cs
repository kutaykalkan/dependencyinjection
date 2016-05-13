
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemArt DerivedCalculationSupportingDetail table
	/// </summary>
	[Serializable]
	[DataContract]
	public class DerivedCalculationSupportingDetailInfo : DerivedCalculationSupportingDetailInfoBase
	{
        private string _RevisedBy;
        private DateTime? _DateRevised;

        [XmlElement(ElementName="RevisedBy")]
        public string RevisedBy
        {
            get
            {
                return this._RevisedBy;
            }
            set
            {
                this._RevisedBy = value;
            }
        }

        [XmlElement(ElementName = "DateRevised")]
        public DateTime? DateRevised
        {
            get
            {
                return this._DateRevised;
            }
            set
            {
                this._DateRevised = value;
            }
        }
	}
}
