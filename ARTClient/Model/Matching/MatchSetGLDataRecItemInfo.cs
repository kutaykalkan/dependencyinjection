
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Matching.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model.Matching
{    

	/// <summary>
	/// An object representation of the SkyStemART MatchSetGLDataRecItem table
	/// </summary>
	[Serializable]
	[DataContract]
	public class MatchSetGLDataRecItemInfo : MatchSetGLDataRecItemInfoBase
	{
        Int64? _MatchingSourceDataImportID = null;
        System.DateTime? _CloseDate = DateTime.Now;

        [XmlElement(ElementName = "MatchingSourceDataImportID")]
        public virtual System.Int64? MatchingSourceDataImportID
        {
            get
            {
                return this._MatchingSourceDataImportID;
            }
            set
            {
                this._MatchingSourceDataImportID = value;

            }
        }
        [XmlElement(ElementName = "CloseDate")]
        public virtual System.DateTime? CloseDate
        {
            get
            {
                return this._CloseDate;
            }
            set
            {
                this._CloseDate = value;
            }
        }

	}
}
