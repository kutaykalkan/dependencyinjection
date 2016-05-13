
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemArt GeographyStructureHdr table
	/// </summary>
	[Serializable]
	[DataContract]
	public class GeographyStructureHdrInfo : GeographyStructureHdrInfoBase
	{
        private string _DisplayText;

        public string DisplayText 
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
