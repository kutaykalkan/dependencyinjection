
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Matching.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model.Matching
{    

	/// <summary>
	/// An object representation of the SkyStemART MatchSetMachingSourceDataImport table
	/// </summary>
	[Serializable]
	[DataContract]
	public class MatchSetMatchingSourceDataImportInfo : MatchSetMatchingSourceDataImportInfoBase
	{
        
        protected System.Int16? _MatchingSourceTypeID = null;

        [XmlElement(ElementName = "MatchingSourceTypeID")]
        public virtual System.Int16? MatchingSourceTypeID
        {
            get
            {
                return this._MatchingSourceTypeID;
            }
            set
            {
                this._MatchingSourceTypeID = value;

            }
        }			
	}
}
