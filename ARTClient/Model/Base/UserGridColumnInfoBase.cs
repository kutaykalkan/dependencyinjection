

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt UserGridColumn table
	/// </summary>
	[Serializable]
	public abstract class UserGridColumnInfoBase
	{
		protected System.Int16 _GridColumnID = 0;
		protected System.Int32 _UserGridColumnID = 0;
		protected System.Int32 _UserID = 0;

		public bool IsGridColumnIDNull = true;


		public bool IsUserGridColumnIDNull = true;


		public bool IsUserIDNull = true;

		[XmlElement(ElementName = "GridColumnID")]
		public virtual System.Int16 GridColumnID 
		{
			get 
			{
				return this._GridColumnID;
			}
			set 
			{
				this._GridColumnID = value;

									this.IsGridColumnIDNull = false;
							}
		}			

		[XmlElement(ElementName = "UserGridColumnID")]
		public virtual System.Int32 UserGridColumnID 
		{
			get 
			{
				return this._UserGridColumnID;
			}
			set 
			{
				this._UserGridColumnID = value;

									this.IsUserGridColumnIDNull = false;
							}
		}			

		[XmlElement(ElementName = "UserID")]
		public virtual System.Int32 UserID 
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
