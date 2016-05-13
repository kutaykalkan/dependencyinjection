

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Matching.Base
{    

	/// <summary>
	/// An object representation of the SkyStemART MatchingConfigurationRule table
	/// </summary>
	[Serializable]
	public abstract class MatchingConfigurationRuleInfoBase
	{

				protected System.String _Keywords = null;
						protected System.Decimal? _LowerBound = null;
						protected System.Int64? _MatchingConfigurationID = null;
						protected System.Int64? _MatchingConfigurationRuleID = null;
						protected System.Int16? _OperatorID = null;
						protected System.Int16? _ThresholdTypeID = null;
						protected System.Decimal? _UpperBound = null;
		
		[XmlElement(ElementName = "Keywords")]
				public virtual System.String Keywords 
				{
			get 
			{
				return this._Keywords;
			}
			set 
			{
				this._Keywords = value;

			}
		}			

		[XmlElement(ElementName = "LowerBound")]
				public virtual System.Decimal? LowerBound 
				{
			get 
			{
				return this._LowerBound;
			}
			set 
			{
				this._LowerBound = value;

			}
		}			

		[XmlElement(ElementName = "MatchingConfigurationID")]
				public virtual System.Int64? MatchingConfigurationID 
				{
			get 
			{
				return this._MatchingConfigurationID;
			}
			set 
			{
				this._MatchingConfigurationID = value;

			}
		}			

		[XmlElement(ElementName = "MatchingConfigurationRuleID")]
				public virtual System.Int64? MatchingConfigurationRuleID 
				{
			get 
			{
				return this._MatchingConfigurationRuleID;
			}
			set 
			{
				this._MatchingConfigurationRuleID = value;

			}
		}			

		[XmlElement(ElementName = "OperatorID")]
				public virtual System.Int16? OperatorID 
				{
			get 
			{
				return this._OperatorID;
			}
			set 
			{
				this._OperatorID = value;

			}
		}			

		[XmlElement(ElementName = "ThresholdTypeID")]
				public virtual System.Int16? ThresholdTypeID 
				{
			get 
			{
				return this._ThresholdTypeID;
			}
			set 
			{
				this._ThresholdTypeID = value;

			}
		}			

		[XmlElement(ElementName = "UpperBound")]
				public virtual System.Decimal? UpperBound 
				{
			get 
			{
				return this._UpperBound;
			}
			set 
			{
				this._UpperBound = value;

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
