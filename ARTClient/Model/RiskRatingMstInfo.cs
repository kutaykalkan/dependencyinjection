
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemArt RiskRatingMst table
	/// </summary>
	[Serializable]
	[DataContract]
	public class RiskRatingMstInfo : RiskRatingMstInfoBase
	{
        private string _DisplayText;

        public string  DisplayText 
        {
            get
            {
                return this._DisplayText;
            }
            set
            {
                this._DisplayText = value;
            }
        }
	}
}
