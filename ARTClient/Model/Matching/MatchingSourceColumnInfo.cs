
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Matching.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model.Matching
{    

	/// <summary>
	/// An object representation of the SkyStemART MatchingSourceColumn table
	/// </summary>
	[Serializable]
	[DataContract]
	public class MatchingSourceColumnInfo : MatchingSourceColumnInfoBase
	{
       
        protected System.Int16? _GridColumnID = null;

        [XmlElement(ElementName = "GridColumnID")]
        public virtual System.Int16? GridColumnID
        {
            get
            {
                return this._GridColumnID;
            }
            set
            {
                this._GridColumnID = value;

            }
        }
       
	}
}
