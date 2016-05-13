
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Matching.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model.Matching
{    

	/// <summary>
	/// An object representation of the SkyStemART MatchingSourceAccount table
	/// </summary>
	[Serializable]
	[DataContract]
	public class MatchingSourceAccountInfo : MatchingSourceAccountInfoBase
	{

        protected System.Decimal? _GLBalance = null;

        [XmlElement(ElementName = "GLBalance")]
        public virtual System.Decimal? GLBalance
        {
            get
            {
                return this._GLBalance;
            }
            set
            {
                this._GLBalance = value;

            }
        }			

	}
}
