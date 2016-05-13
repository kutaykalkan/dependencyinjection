

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt GLDataRoleCertificationDate table
	/// </summary>
	[Serializable]
	public abstract class GLDataRoleCertificationDateInfoBase
	{

		protected System.DateTime? _CertificationDate = DateTime.Now;
		protected System.Int64? _GLDataID = 0;
		protected System.Int32? _GLDataRoleCertificationDateID = 0;
		protected System.Int16? _RoleID = 0;


		public bool IsCertificationDateNull = true;


		public bool IsGLDataIDNull = true;


		public bool IsGLDataRoleCertificationDateIDNull = true;


		public bool IsRoleIDNull = true;

		[XmlElement(ElementName = "CertificationDate")]
		public virtual System.DateTime? CertificationDate 
		{
			get 
			{
				return this._CertificationDate;
			}
			set 
			{
				this._CertificationDate = value;

									this.IsCertificationDateNull = (_CertificationDate == DateTime.MinValue);
							}
		}			

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

		[XmlElement(ElementName = "GLDataRoleCertificationDateID")]
		public virtual System.Int32? GLDataRoleCertificationDateID 
		{
			get 
			{
				return this._GLDataRoleCertificationDateID;
			}
			set 
			{
				this._GLDataRoleCertificationDateID = value;

									this.IsGLDataRoleCertificationDateIDNull = false;
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

        
		/// <summary>
		/// Returns a string representation of the object, displaying all property and field names and values.
		/// </summary>
		public override string ToString() 
		{
			return StringUtil.ToString(this);
		}		
	
	}
}
