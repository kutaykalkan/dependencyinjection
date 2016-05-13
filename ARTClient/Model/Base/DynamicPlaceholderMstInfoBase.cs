

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt DynamicPlaceholderMst table
	/// </summary>
	[Serializable]
	public abstract class DynamicPlaceholderMstInfoBase
	{

	
		protected System.String _AddedBy = "";
		protected System.DateTime _DateAdded = DateTime.Now;
		protected System.DateTime _DateRevised = DateTime.Now;
		protected System.String _DynamicPlaceholder = "";
		protected System.Int16 _DynamicPlaceholderID = 0;
		protected System.Int16 _DynamicPlaceholderTypeID = 0;
		protected System.String _HostName = "";
		protected System.Boolean _IsActive = false;
		protected System.String _RevisedBy = "";
		protected System.Int16 _SortOrder = 0;




		public bool IsAddedByNull = true;


		public bool IsDateAddedNull = true;


		public bool IsDateRevisedNull = true;


		public bool IsDynamicPlaceholderNull = true;


		public bool IsDynamicPlaceholderIDNull = true;


		public bool IsDynamicPlaceholderTypeIDNull = true;


		public bool IsHostNameNull = true;


		public bool IsIsActiveNull = true;


		public bool IsRevisedByNull = true;


		public bool IsSortOrderNull = true;

		[XmlElement(ElementName = "AddedBy")]
		public virtual System.String AddedBy 
		{
			get 
			{
				return this._AddedBy;
			}
			set 
			{
				this._AddedBy = value;

									this.IsAddedByNull = (_AddedBy == null);
							}
		}			

		[XmlElement(ElementName = "DateAdded")]
		public virtual System.DateTime DateAdded 
		{
			get 
			{
				return this._DateAdded;
			}
			set 
			{
				this._DateAdded = value;

									this.IsDateAddedNull = (_DateAdded == DateTime.MinValue);
							}
		}			

		[XmlElement(ElementName = "DateRevised")]
		public virtual System.DateTime DateRevised 
		{
			get 
			{
				return this._DateRevised;
			}
			set 
			{
				this._DateRevised = value;

									this.IsDateRevisedNull = (_DateRevised == DateTime.MinValue);
							}
		}			

		[XmlElement(ElementName = "DynamicPlaceholder")]
		public virtual System.String DynamicPlaceholder 
		{
			get 
			{
				return this._DynamicPlaceholder;
			}
			set 
			{
				this._DynamicPlaceholder = value;

									this.IsDynamicPlaceholderNull = (_DynamicPlaceholder == null);
							}
		}			

		[XmlElement(ElementName = "DynamicPlaceholderID")]
		public virtual System.Int16 DynamicPlaceholderID 
		{
			get 
			{
				return this._DynamicPlaceholderID;
			}
			set 
			{
				this._DynamicPlaceholderID = value;

									this.IsDynamicPlaceholderIDNull = false;
							}
		}			

		[XmlElement(ElementName = "DynamicPlaceholderTypeID")]
		public virtual System.Int16 DynamicPlaceholderTypeID 
		{
			get 
			{
				return this._DynamicPlaceholderTypeID;
			}
			set 
			{
				this._DynamicPlaceholderTypeID = value;

									this.IsDynamicPlaceholderTypeIDNull = false;
							}
		}			

		[XmlElement(ElementName = "HostName")]
		public virtual System.String HostName 
		{
			get 
			{
				return this._HostName;
			}
			set 
			{
				this._HostName = value;

									this.IsHostNameNull = (_HostName == null);
							}
		}			

		[XmlElement(ElementName = "IsActive")]
		public virtual System.Boolean IsActive 
		{
			get 
			{
				return this._IsActive;
			}
			set 
			{
				this._IsActive = value;

									this.IsIsActiveNull = false;
							}
		}			

		[XmlElement(ElementName = "RevisedBy")]
		public virtual System.String RevisedBy 
		{
			get 
			{
				return this._RevisedBy;
			}
			set 
			{
				this._RevisedBy = value;

									this.IsRevisedByNull = (_RevisedBy == null);
							}
		}			

		[XmlElement(ElementName = "SortOrder")]
		public virtual System.Int16 SortOrder 
		{
			get 
			{
				return this._SortOrder;
			}
			set 
			{
				this._SortOrder = value;

									this.IsSortOrderNull = false;
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
