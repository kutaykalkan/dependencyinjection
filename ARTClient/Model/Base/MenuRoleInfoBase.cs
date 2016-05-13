

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt MenuRole table
	/// </summary>
	[Serializable]
	public abstract class MenuRoleInfoBase
	{

		protected System.Int16? _MenuID = 0;
		protected System.Int16? _MenuRoleID = 0;
		protected System.Int16? _RoleID = 0;

		public bool IsMenuIDNull = true;


		public bool IsMenuRoleIDNull = true;


		public bool IsRoleIDNull = true;

		[XmlElement(ElementName = "MenuID")]
		public virtual System.Int16? MenuID 
		{
			get 
			{
				return this._MenuID;
			}
			set 
			{
				this._MenuID = value;

									this.IsMenuIDNull = false;
							}
		}			

		[XmlElement(ElementName = "MenuRoleID")]
		public virtual System.Int16? MenuRoleID 
		{
			get 
			{
				return this._MenuRoleID;
			}
			set 
			{
				this._MenuRoleID = value;

									this.IsMenuRoleIDNull = false;
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
