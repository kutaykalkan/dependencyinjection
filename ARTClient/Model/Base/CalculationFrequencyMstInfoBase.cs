

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemART CalculationFrequencyMst table
	/// </summary>
	[Serializable]
	public abstract class CalculationFrequencyMstInfoBase
	{


				protected System.String _AddedBy = null;
						protected System.String _CalculationFrequency = null;
						protected System.Int16? _CalculationFrequencyID = null;
						protected System.Int32? _CalculationFrequencyLabelID = null;
						protected System.DateTime? _DateAdded = null;
						protected System.DateTime? _DateRevised = null;
						protected System.String _HostName = null;
						protected System.Boolean? _IsActive = null;
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

		[XmlElement(ElementName = "CalculationFrequency")]
				public virtual System.String CalculationFrequency 
				{
			get 
			{
				return this._CalculationFrequency;
			}
			set 
			{
				this._CalculationFrequency = value;

			}
		}			

		[XmlElement(ElementName = "CalculationFrequencyID")]
				public virtual System.Int16? CalculationFrequencyID 
				{
			get 
			{
				return this._CalculationFrequencyID;
			}
			set 
			{
				this._CalculationFrequencyID = value;

			}
		}			

		[XmlElement(ElementName = "CalculationFrequencyLabelID")]
				public virtual System.Int32? CalculationFrequencyLabelID 
				{
			get 
			{
				return this._CalculationFrequencyLabelID;
			}
			set 
			{
				this._CalculationFrequencyLabelID = value;

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
