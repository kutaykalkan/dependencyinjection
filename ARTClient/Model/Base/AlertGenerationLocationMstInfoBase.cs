

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt AlertGenerationLocationMst table
	/// </summary>
	[Serializable]
	public abstract class AlertGenerationLocationMstInfoBase
	{
		protected System.String _AddedBy = "";
		protected System.String _AlertGenerationLocation = "";
		protected System.String _AlertGenerationLocationDesc = "";
		protected System.Int16? _AlertGenerationLocationID = 0;
		protected System.DateTime? _DateAdded = DateTime.Now;
		protected System.DateTime? _DateRevised = DateTime.Now;
		protected System.String _HostName = "";
		protected System.Boolean? _IsActive = false;
		protected System.String _RevisedBy = "";




		public bool IsAddedByNull = true;


		public bool IsAlertGenerationLocationNull = true;


		public bool IsAlertGenerationLocationDescNull = true;


		public bool IsAlertGenerationLocationIDNull = true;


		public bool IsDateAddedNull = true;


		public bool IsDateRevisedNull = true;


		public bool IsHostNameNull = true;


		public bool IsIsActiveNull = true;


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

		[XmlElement(ElementName = "AlertGenerationLocation")]
		public virtual System.String AlertGenerationLocation 
		{
			get 
			{
				return this._AlertGenerationLocation;
			}
			set 
			{
				this._AlertGenerationLocation = value;

									this.IsAlertGenerationLocationNull = (_AlertGenerationLocation == null);
							}
		}			

		[XmlElement(ElementName = "AlertGenerationLocationDesc")]
		public virtual System.String AlertGenerationLocationDesc 
		{
			get 
			{
				return this._AlertGenerationLocationDesc;
			}
			set 
			{
				this._AlertGenerationLocationDesc = value;

									this.IsAlertGenerationLocationDescNull = (_AlertGenerationLocationDesc == null);
							}
		}			

		[XmlElement(ElementName = "AlertGenerationLocationID")]
		public virtual System.Int16? AlertGenerationLocationID 
		{
			get 
			{
				return this._AlertGenerationLocationID;
			}
			set 
			{
				this._AlertGenerationLocationID = value;

									this.IsAlertGenerationLocationIDNull = false;
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
