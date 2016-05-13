

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt NetAccountHdr table
	/// </summary>
	[Serializable]
	public abstract class NetAccountHdrInfoBase
	{

		protected System.String _AddedBy = "";
		protected System.DateTime? _DateAdded = DateTime.Now;
		protected System.DateTime? _DateRevised = DateTime.Now;
		protected System.String _Description = "";
		protected System.String _HostName = "";
		protected System.Boolean? _IsActive = false;
		protected System.String _NetAccount = "";
		protected System.Int32? _NetAccountID = 0;
		protected System.Int32? _NetAccountLabelID = 0;
		protected System.String _RevisedBy = "";


		public bool IsAddedByNull = true;


		public bool IsDateAddedNull = true;


		public bool IsDateRevisedNull = true;


		public bool IsDescriptionNull = true;


		public bool IsHostNameNull = true;


		public bool IsIsActiveNull = true;


		public bool IsNetAccountNull = true;


		public bool IsNetAccountIDNull = true;


		public bool IsNetAccountLabelIDNull = true;


		public bool IsRevisedByNull = true;

     
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
		public virtual System.DateTime? DateAdded 
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
		public virtual System.DateTime? DateRevised 
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

		[XmlElement(ElementName = "Description")]
		public virtual System.String Description 
		{
			get 
			{
				return this._Description;
			}
			set 
			{
				this._Description = value;

									this.IsDescriptionNull = (_Description == null);
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
		public virtual System.Boolean? IsActive 
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

		[XmlElement(ElementName = "NetAccount")]
		public virtual System.String NetAccount 
		{
			get 
			{
				return this._NetAccount;
			}
			set 
			{
				this._NetAccount = value;

									this.IsNetAccountNull = (_NetAccount == null);
							}
		}			

		[XmlElement(ElementName = "NetAccountID")]
		public virtual System.Int32? NetAccountID 
		{
			get 
			{
				return this._NetAccountID;
			}
			set 
			{
				this._NetAccountID = value;

									this.IsNetAccountIDNull = false;
							}
		}			

		[XmlElement(ElementName = "NetAccountLabelID")]
		public virtual System.Int32? NetAccountLabelID 
		{
			get 
			{
				return this._NetAccountLabelID;
			}
			set 
			{
				this._NetAccountLabelID = value;

									this.IsNetAccountLabelIDNull = false;
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

        
		/// <summary>
		/// Returns a string representation of the object, displaying all property and field names and values.
		/// </summary>
		public override string ToString() 
		{
			return StringUtil.ToString(this);
		}		
	
	}
}
