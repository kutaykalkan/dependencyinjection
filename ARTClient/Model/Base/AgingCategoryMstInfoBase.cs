

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemART AgingCategoryMst table
	/// </summary>
	[Serializable]
	public abstract class AgingCategoryMstInfoBase
	{

	
		protected System.Int16 _AgingCategoryID = 0;
		protected System.Int32 _AgingCategoryLabelID = 0;
		protected System.String _AgingCategoryName = "";
		protected System.Int16 _FromDays = 0;
		protected System.Int16 _ToDays = 0;




		public bool IsAgingCategoryIDNull = true;


		public bool IsAgingCategoryLabelIDNull = true;


		public bool IsAgingCategoryNameNull = true;


		public bool IsFromDaysNull = true;


		public bool IsToDaysNull = true;

		[XmlElement(ElementName = "AgingCategoryID")]
		public virtual System.Int16 AgingCategoryID 
		{
			get 
			{
				return this._AgingCategoryID;
			}
			set 
			{
				this._AgingCategoryID = value;

									this.IsAgingCategoryIDNull = false;
							}
		}			

		[XmlElement(ElementName = "AgingCategoryLabelID")]
		public virtual System.Int32 AgingCategoryLabelID 
		{
			get 
			{
				return this._AgingCategoryLabelID;
			}
			set 
			{
				this._AgingCategoryLabelID = value;

									this.IsAgingCategoryLabelIDNull = false;
							}
		}			

		[XmlElement(ElementName = "AgingCategoryName")]
		public virtual System.String AgingCategoryName 
		{
			get 
			{
				return this._AgingCategoryName;
			}
			set 
			{
				this._AgingCategoryName = value;

									this.IsAgingCategoryNameNull = (_AgingCategoryName == null);
							}
		}			

		[XmlElement(ElementName = "FromDays")]
		public virtual System.Int16 FromDays 
		{
			get 
			{
				return this._FromDays;
			}
			set 
			{
				this._FromDays = value;

									this.IsFromDaysNull = false;
							}
		}			

		[XmlElement(ElementName = "ToDays")]
		public virtual System.Int16 ToDays 
		{
			get 
			{
				return this._ToDays;
			}
			set 
			{
				this._ToDays = value;

									this.IsToDaysNull = false;
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
