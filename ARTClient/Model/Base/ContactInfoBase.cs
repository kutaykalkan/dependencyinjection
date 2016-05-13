

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt Contact table
	/// </summary>
	[Serializable]
	public abstract class ContactInfoBase
	{
		protected System.String _AddedBy = "";
		protected System.Int32? _CompanyID = 0;
		protected System.Int32? _ContactID = 0;
		protected System.DateTime? _DateAdded = DateTime.Now;
		protected System.DateTime? _DateRevised = DateTime.Now;
		protected System.String _Email = "";
		protected System.String _HostName = "";
		protected System.Boolean? _IsActive = false;
		protected System.Boolean? _IsPrimaryContact = false;
		protected System.String _Name = "";
		protected System.String _Phone = "";
		protected System.String _RevisedBy = "";




		public bool IsAddedByNull = true;


		public bool IsCompanyIDNull = true;


		public bool IsContactIDNull = true;


		public bool IsDateAddedNull = true;


		public bool IsDateRevisedNull = true;


		public bool IsEmailNull = true;


		public bool IsHostNameNull = true;


		public bool IsIsActiveNull = true;


		public bool IsIsPrimaryContactNull = true;


		public bool IsNameNull = true;


		public bool IsPhoneNull = true;


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

		[XmlElement(ElementName = "ContactID")]
		public virtual System.Int32? ContactID 
		{
			get 
			{
				return this._ContactID;
			}
			set 
			{
				this._ContactID = value;

									this.IsContactIDNull = false;
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

		[XmlElement(ElementName = "Email")]
		public virtual System.String Email 
		{
			get 
			{
				return this._Email;
			}
			set 
			{
				this._Email = value;

									this.IsEmailNull = (_Email == null);
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

		[XmlElement(ElementName = "IsPrimaryContact")]
		public virtual System.Boolean? IsPrimaryContact 
		{
			get 
			{
				return this._IsPrimaryContact;
			}
			set 
			{
				this._IsPrimaryContact = value;

									this.IsIsPrimaryContactNull = false;
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

		[XmlElement(ElementName = "Phone")]
		public virtual System.String Phone 
		{
			get 
			{
				return this._Phone;
			}
			set 
			{
				this._Phone = value;

									this.IsPhoneNull = (_Phone == null);
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
