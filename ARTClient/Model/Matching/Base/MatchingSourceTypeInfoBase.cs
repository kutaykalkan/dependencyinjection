

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Matching.Base
{    

	/// <summary>
	/// An object representation of the SkyStemART MatchingSourceType table
	/// </summary>
	[Serializable]
	public abstract class MatchingSourceTypeInfoBase
	{
				protected System.String _AddedBy = null;
						protected System.DateTime? _DateAdded = null;
						protected System.DateTime? _DateRevised = null;
						protected System.String _HostName = null;
						protected System.Boolean? _IsActive = null;
						protected System.Int16? _MatchingSourceTypeID = null;
						protected System.String _MatchingSourceTypeName = null;
						protected System.Int32? _MatchingSourceTypeNameLabelID = null;
						protected System.String _RevisedBy = null;
		


		[XmlElement(ElementName = "AddedBy")]
				public virtual System.String AddedBy 
				{
			get 
			{
				return this._AddedBy;
			}
			set 
			{
				this._AddedBy = value;

			}
		}			

		[XmlElement(ElementName = "DateAdded")]
				public virtual System.DateTime? DateAdded 
				{
			get 
			{
				return this._DateAdded;
			}
			set 
			{
				this._DateAdded = value;

			}
		}			

		[XmlElement(ElementName = "DateRevised")]
				public virtual System.DateTime? DateRevised 
				{
			get 
			{
				return this._DateRevised;
			}
			set 
			{
				this._DateRevised = value;

			}
		}			

		[XmlElement(ElementName = "HostName")]
				public virtual System.String HostName 
				{
			get 
			{
				return this._HostName;
			}
			set 
			{
				this._HostName = value;

			}
		}			

		[XmlElement(ElementName = "IsActive")]
				public virtual System.Boolean? IsActive 
				{
			get 
			{
				return this._IsActive;
			}
			set 
			{
				this._IsActive = value;

			}
		}			

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

		[XmlElement(ElementName = "MatchingSourceTypeName")]
				public virtual System.String MatchingSourceTypeName 
				{
			get 
			{
				return this._MatchingSourceTypeName;
			}
			set 
			{
				this._MatchingSourceTypeName = value;

			}
		}			

		[XmlElement(ElementName = "MatchingSourceTypeNameLabelID")]
				public virtual System.Int32? MatchingSourceTypeNameLabelID 
				{
			get 
			{
				return this._MatchingSourceTypeNameLabelID;
			}
			set 
			{
				this._MatchingSourceTypeNameLabelID = value;

			}
		}			

		[XmlElement(ElementName = "RevisedBy")]
				public virtual System.String RevisedBy 
				{
			get 
			{
				return this._RevisedBy;
			}
			set 
			{
				this._RevisedBy = value;

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
