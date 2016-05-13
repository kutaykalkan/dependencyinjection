

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt GLDataReconciliationSubmissionDate table
	/// </summary>
	[Serializable]
	public abstract class GLDataReconciliationSubmissionDateInfoBase
	{

		protected System.Int64? _GLDataID = 0;
		protected System.Int32? _GLDataReconciliationSubmissionDateID = 0;
		protected System.Int16? _RoleID = 0;
		protected System.DateTime? _SubmissionDate = DateTime.Now;
		protected System.Int32? _UserID = 0;

		public bool IsGLDataIDNull = true;


		public bool IsGLDataReconciliationSubmissionDateIDNull = true;


		public bool IsRoleIDNull = true;


		public bool IsSubmissionDateNull = true;


		public bool IsUserIDNull = true;

		[XmlElement(ElementName = "GLDataID")]
		public virtual System.Int64? GLDataID 
		{
			get 
			{
				return this._GLDataID;
			}
			set 
			{
				this._GLDataID = value;

									this.IsGLDataIDNull = false;
							}
		}			

		[XmlElement(ElementName = "GLDataReconciliationSubmissionDateID")]
		public virtual System.Int32? GLDataReconciliationSubmissionDateID 
		{
			get 
			{
				return this._GLDataReconciliationSubmissionDateID;
			}
			set 
			{
				this._GLDataReconciliationSubmissionDateID = value;

									this.IsGLDataReconciliationSubmissionDateIDNull = false;
							}
		}			

		[XmlElement(ElementName = "RoleID")]
		public virtual System.Int16? RoleID 
		{
			get 
			{
				return this._RoleID;
			}
			set 
			{
				this._RoleID = value;

									this.IsRoleIDNull = false;
							}
		}			

		[XmlElement(ElementName = "SubmissionDate")]
		public virtual System.DateTime? SubmissionDate 
		{
			get 
			{
				return this._SubmissionDate;
			}
			set 
			{
				this._SubmissionDate = value;

									this.IsSubmissionDateNull = (_SubmissionDate == DateTime.MinValue);
							}
		}			

		[XmlElement(ElementName = "UserID")]
		public virtual System.Int32? UserID 
		{
			get 
			{
				return this._UserID;
			}
			set 
			{
				this._UserID = value;

									this.IsUserIDNull = false;
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
