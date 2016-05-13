

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Matching.Base
{    

	/// <summary>
	/// An object representation of the SkyStemART MatchingSourceColumn table
	/// </summary>
	[Serializable]
	public abstract class MatchingSourceColumnInfoBase
	{
				protected System.String _ColumnName = null;
						protected System.Int16? _DataTypeID = null;
						protected System.Int64? _MatchingSourceColumnID = null;
						protected System.Int64? _MatchingSourceDataImportID = null;
		


		[XmlElement(ElementName = "ColumnName")]
				public virtual System.String ColumnName 
				{
			get 
			{
				return this._ColumnName;
			}
			set 
			{
				this._ColumnName = value;

			}
		}			

		[XmlElement(ElementName = "DataTypeID")]
				public virtual System.Int16? DataTypeID 
				{
			get 
			{
				return this._DataTypeID;
			}
			set 
			{
				this._DataTypeID = value;

			}
		}			

		[XmlElement(ElementName = "MatchingSourceColumnID")]
				public virtual System.Int64? MatchingSourceColumnID 
				{
			get 
			{
				return this._MatchingSourceColumnID;
			}
			set 
			{
				this._MatchingSourceColumnID = value;

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
