
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model.Base
{    
	/// <summary>
	/// An object representation of the SkyStemART TaskDetailStatus table
	/// </summary>
	[DataContract]
	[Serializable]
	public abstract class TaskDetailStatusInfoBase
	{
		protected System.Int32? _AddedByUserID = null;
		protected System.Int64? _TaskDetailID = null;
		protected System.Int64? _TaskDetailStatusID = null;
		protected System.DateTime? _TaskStatusDate = null;
		protected System.Int16? _TaskStatusID = null;
		[DataMember]
		[XmlElement(ElementName = "AddedByUserID")]
		public virtual System.Int32? AddedByUserID 
		{
			get { return this._AddedByUserID; }
			set { this._AddedByUserID = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "TaskDetailID")]
		public virtual System.Int64? TaskDetailID 
		{
			get { return this._TaskDetailID; }
			set { this._TaskDetailID = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "TaskDetailStatusID")]
		public virtual System.Int64? TaskDetailStatusID 
		{
			get { return this._TaskDetailStatusID; }
			set { this._TaskDetailStatusID = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "TaskStatusDate")]
		public virtual System.DateTime? TaskStatusDate 
		{
			get { return this._TaskStatusDate; }
			set { this._TaskStatusDate = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "TaskStatusID")]
		public virtual System.Int16? TaskStatusID 
		{
			get { return this._TaskStatusID; }
			set { this._TaskStatusID = value; }
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
