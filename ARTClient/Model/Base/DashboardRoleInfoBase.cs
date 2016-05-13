

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt DashboardRole table
	/// </summary>
	[Serializable]
	public abstract class DashboardRoleInfoBase
	{
		protected System.Int16? _DashboardID = 0;
		protected System.Int16? _DashboardRoleID = 0;
		protected System.Int16? _RoleID = 0;

		public bool IsDashboardIDNull = true;


		public bool IsDashboardRoleIDNull = true;


		public bool IsRoleIDNull = true;

		[XmlElement(ElementName = "DashboardID")]
		public virtual System.Int16? DashboardID 
		{
			get 
			{
				return this._DashboardID;
			}
			set 
			{
				this._DashboardID = value;

									this.IsDashboardIDNull = false;
							}
		}			

		[XmlElement(ElementName = "DashboardRoleID")]
		public virtual System.Int16? DashboardRoleID 
		{
			get 
			{
				return this._DashboardRoleID;
			}
			set 
			{
				this._DashboardRoleID = value;

									this.IsDashboardRoleIDNull = false;
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
