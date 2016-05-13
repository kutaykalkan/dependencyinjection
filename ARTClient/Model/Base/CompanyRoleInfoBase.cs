

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt CompanyRole table
	/// </summary>
	[Serializable]
	public abstract class CompanyRoleInfoBase
	{

		protected System.Int32? _CompanyID = 0;
		protected System.Int32? _CompanyRoleID = 0;
		protected System.Int16? _RoleID = 0;

		public bool IsCompanyIDNull = true;


		public bool IsCompanyRoleIDNull = true;


		public bool IsRoleIDNull = true;

		[XmlElement(ElementName = "CompanyID")]
		public virtual System.Int32? CompanyID 
		{
			get 
			{
				return this._CompanyID;
			}
			set 
			{
				this._CompanyID = value;

									this.IsCompanyIDNull = false;
							}
		}			

		[XmlElement(ElementName = "CompanyRoleID")]
		public virtual System.Int32? CompanyRoleID 
		{
			get 
			{
				return this._CompanyRoleID;
			}
			set 
			{
				this._CompanyRoleID = value;

									this.IsCompanyRoleIDNull = false;
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
