

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemART DataTypeOperator table
	/// </summary>
	[Serializable]
	public abstract class DataTypeOperatorInfoBase
	{

	
				protected System.Int16? _DataTypeID = null;
						protected System.Int16? _DataTypeOperatorID = null;
						protected System.Int16? _OperatorID = null;
		


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

		[XmlElement(ElementName = "DataTypeOperatorID")]
				public virtual System.Int16? DataTypeOperatorID 
				{
			get 
			{
				return this._DataTypeOperatorID;
			}
			set 
			{
				this._DataTypeOperatorID = value;

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

        
		/// <summary>
		/// Returns a string representation of the object, displaying all property and field names and values.
		/// </summary>
		public override string ToString() 
		{
			return StringUtil.ToString(this);
		}		
	
	
			
				
	}
}
