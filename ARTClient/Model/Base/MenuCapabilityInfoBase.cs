

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt MenuCapability table
	/// </summary>
	[Serializable]
	public abstract class MenuCapabilityInfoBase
	{

		protected System.Int16? _CapabilityID = 0;
		protected System.Int32? _MenuCapabilityID = 0;
		protected System.Int16? _MenuID = 0;

		public bool IsCapabilityIDNull = true;


		public bool IsMenuCapabilityIDNull = true;


		public bool IsMenuIDNull = true;

		[XmlElement(ElementName = "CapabilityID")]
		public virtual System.Int16? CapabilityID 
		{
			get 
			{
				return this._CapabilityID;
			}
			set 
			{
				this._CapabilityID = value;

									this.IsCapabilityIDNull = false;
							}
		}			

		[XmlElement(ElementName = "MenuCapabilityID")]
		public virtual System.Int32? MenuCapabilityID 
		{
			get 
			{
				return this._MenuCapabilityID;
			}
			set 
			{
				this._MenuCapabilityID = value;

									this.IsMenuCapabilityIDNull = false;
							}
		}			

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

        
		/// <summary>
		/// Returns a string representation of the object, displaying all property and field names and values.
		/// </summary>
		public override string ToString() 
		{
			return StringUtil.ToString(this);
		}		
	}
}
