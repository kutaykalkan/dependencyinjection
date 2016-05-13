

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Matching.Base
{    

	/// <summary>
	/// An object representation of the SkyStemART MatchSetMachingSourceDataImport table
	/// </summary>
	[Serializable]
	public abstract class MatchSetMatchingSourceDataImportInfoBase
	{
				protected System.String _FilterCriteria = null;
						protected System.Int64? _MatchingSourceDataImportID = null;
						protected System.Int64? _MatchSetID = null;
						protected System.Int64? _MatchSetMatchingSourceDataImportID = null;
						protected System.String _SubSetData = null;
						protected System.String _SubSetName = null;
		


		[XmlElement(ElementName = "FilterCriteria")]
				public virtual System.String FilterCriteria 
				{
			get 
			{
				return this._FilterCriteria;
			}
			set 
			{
				this._FilterCriteria = value;

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

		[XmlElement(ElementName = "MatchSetID")]
				public virtual System.Int64? MatchSetID 
				{
			get 
			{
				return this._MatchSetID;
			}
			set 
			{
				this._MatchSetID = value;

			}
		}			

		[XmlElement(ElementName = "MatchSetMatchingSourceDataImportID")]
				public virtual System.Int64? MatchSetMatchingSourceDataImportID 
				{
			get 
			{
				return this._MatchSetMatchingSourceDataImportID;
			}
			set 
			{
				this._MatchSetMatchingSourceDataImportID = value;

			}
		}			

		[XmlElement(ElementName = "SubSetData")]
				public virtual System.String SubSetData 
				{
			get 
			{
				return this._SubSetData;
			}
			set 
			{
				this._SubSetData = value;

			}
		}			

		[XmlElement(ElementName = "SubSetName")]
				public virtual System.String SubSetName 
				{
			get 
			{
				return this._SubSetName;
			}
			set 
			{
				this._SubSetName = value;

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
