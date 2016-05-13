

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt Sysdiagrams table
	/// </summary>
	[Serializable]
	public abstract class SysdiagramsInfoBase
	{

		protected System.Byte[] _Definition = null;
		protected System.Int32? _Diagram_id = 0;
		protected System.String _Name = "";
		protected System.Int32? _Principal_id = 0;
		protected System.Int32? _Version = 0;

		public bool IsDefinitionNull = true;


		public bool IsDiagram_idNull = true;


		public bool IsNameNull = true;


		public bool IsPrincipal_idNull = true;


		public bool IsVersionNull = true;

		[XmlElement(ElementName = "Definition")]
		public virtual System.Byte[] Definition 
		{
			get 
			{
				return this._Definition;
			}
			set 
			{
				this._Definition = value;

									this.IsDefinitionNull = false;
							}
		}			

		[XmlElement(ElementName = "Diagram_id")]
		public virtual System.Int32? Diagram_id 
		{
			get 
			{
				return this._Diagram_id;
			}
			set 
			{
				this._Diagram_id = value;

									this.IsDiagram_idNull = false;
							}
		}			

		[XmlElement(ElementName = "Name")]
		public virtual System.String Name 
		{
			get 
			{
				return this._Name;
			}
			set 
			{
				this._Name = value;

									this.IsNameNull = (_Name == null);
							}
		}			

		[XmlElement(ElementName = "Principal_id")]
		public virtual System.Int32? Principal_id 
		{
			get 
			{
				return this._Principal_id;
			}
			set 
			{
				this._Principal_id = value;

									this.IsPrincipal_idNull = false;
							}
		}			

		[XmlElement(ElementName = "Version")]
		public virtual System.Int32? Version 
		{
			get 
			{
				return this._Version;
			}
			set 
			{
				this._Version = value;

									this.IsVersionNull = false;
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
