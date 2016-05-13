

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Matching.Base
{    

	/// <summary>
	/// An object representation of the SkyStemART MatchingSourceAccount table
	/// </summary>
	[Serializable]
	public abstract class MatchingSourceAccountInfoBase
	{

				protected System.Int64? _AccountID = null;
						protected System.Int64? _MatchingSourceAccountID = null;
						protected System.Int64? _MatchingSourceDataImportID = null;
		
		[XmlElement(ElementName = "AccountID")]
				public virtual System.Int64? AccountID 
				{
			get 
			{
				return this._AccountID;
			}
			set 
			{
				this._AccountID = value;

			}
		}			

		[XmlElement(ElementName = "MatchingSourceAccountID")]
				public virtual System.Int64? MatchingSourceAccountID 
				{
			get 
			{
				return this._MatchingSourceAccountID;
			}
			set 
			{
				this._MatchingSourceAccountID = value;

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
