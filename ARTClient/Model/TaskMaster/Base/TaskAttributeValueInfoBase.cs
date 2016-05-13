
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model.Base
{    
	/// <summary>
	/// An object representation of the SkyStemART TaskAttributeValue table
	/// </summary>
	[DataContract]
	[Serializable]
	public abstract class TaskAttributeValueInfoBase
	{
		protected System.Int64? _ReferenceID = null;
		protected System.Int64? _TaskAttributeRecperiodSetID = null;
		protected System.Int64? _TaskAttributeValueID = null;
		protected System.String _Value = null;
		[DataMember]
		[XmlElement(ElementName = "ReferenceID")]
		public virtual System.Int64? ReferenceID 
		{
			get { return this._ReferenceID; }
			set { this._ReferenceID = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "TaskAttributeRecperiodSetID")]
		public virtual System.Int64? TaskAttributeRecperiodSetID 
		{
			get { return this._TaskAttributeRecperiodSetID; }
			set { this._TaskAttributeRecperiodSetID = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "TaskAttributeValueID")]
		public virtual System.Int64? TaskAttributeValueID 
		{
			get { return this._TaskAttributeValueID; }
			set { this._TaskAttributeValueID = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "Value")]
		public virtual System.String Value 
		{
			get { return this._Value; }
			set { this._Value = value; }
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
