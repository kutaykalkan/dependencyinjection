

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Matching.Base
{    

	/// <summary>
	/// An object representation of the SkyStemART MatchSetResult table
	/// </summary>
	[Serializable]
	public abstract class MatchSetResultInfoBase
	{
				protected System.String _AddedBy = null;
						protected System.DateTime? _DateAdded = null;
						protected System.DateTime? _DateRevised = null;
						protected System.Boolean? _IsActive = null;
						protected System.String _MatchData = null;
						protected System.Int64? _MatchSetResultID = null;
						protected System.Int64? _MatchSetSubSetCombinationID = null;
						protected System.String _PartialMatchData = null;
						protected System.String _RevisedBy = null;
						protected System.String _UnmatchData = null;
		


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

		[XmlElement(ElementName = "MatchData")]
				public virtual System.String MatchData 
				{
			get 
			{
				return this._MatchData;
			}
			set 
			{
				this._MatchData = value;

			}
		}			

		[XmlElement(ElementName = "MatchSetResultID")]
				public virtual System.Int64? MatchSetResultID 
				{
			get 
			{
				return this._MatchSetResultID;
			}
			set 
			{
				this._MatchSetResultID = value;

			}
		}			

		[XmlElement(ElementName = "MatchSetSubSetCombinationID")]
				public virtual System.Int64? MatchSetSubSetCombinationID 
				{
			get 
			{
				return this._MatchSetSubSetCombinationID;
			}
			set 
			{
				this._MatchSetSubSetCombinationID = value;

			}
		}			

		[XmlElement(ElementName = "PartialMatchData")]
				public virtual System.String PartialMatchData 
				{
			get 
			{
				return this._PartialMatchData;
			}
			set 
			{
				this._PartialMatchData = value;

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

		[XmlElement(ElementName = "UnmatchData")]
				public virtual System.String UnmatchData 
				{
			get 
			{
				return this._UnmatchData;
			}
			set 
			{
				this._UnmatchData = value;

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
