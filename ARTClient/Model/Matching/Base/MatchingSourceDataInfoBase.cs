

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Matching.Base
{    

	/// <summary>
	/// An object representation of the SkyStemART MatchingSourceData table
	/// </summary>
	[Serializable]
	public abstract class MatchingSourceDataInfoBase
	{

				protected System.String _Data = null;
						protected System.Int64? _MatchingSourceDataID = null;
						protected System.Int64? _MatchingSourceDataImportID = null;
		
		[XmlElement(ElementName = "Data")]
				public virtual System.String Data 
				{
			get 
			{
				return this._Data;
			}
			set 
			{
				this._Data = value;

			}
		}			

		[XmlElement(ElementName = "MatchingSourceDataID")]
				public virtual System.Int64? MatchingSourceDataID 
				{
			get 
			{
				return this._MatchingSourceDataID;
			}
			set 
			{
				this._MatchingSourceDataID = value;

			}
		}			

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

        
		/// <summary>
		/// Returns a string representation of the object, displaying all property and field names and values.
		/// </summary>
		public override string ToString() 
		{
			return StringUtil.ToString(this);
		}		
	
	}
}
