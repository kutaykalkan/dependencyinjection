

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt UserGeographyObject table
	/// </summary>
	[Serializable]
	public abstract class UserGeographyObjectInfoBase
	{

		protected System.Int32? _GeographyObjectID = 0;
		protected System.Int32? _UserGeographyObjectID = 0;
		protected System.Int32? _UserID = 0;
        protected bool _IsActive = false;
        protected System.Int32? _KeySize = 0;
        protected System.Int16? _RoleID = 0;

		public bool IsGeographyObjectIDNull = true;


		public bool IsUserGeographyObjectIDNull = true;


		public bool IsUserIDNull = true;

        public bool IsRoleIDNull = true;



		[XmlElement(ElementName = "GeographyObjectID")]
		public virtual System.Int32? GeographyObjectID 
		{
			get 
			{
				return this._GeographyObjectID;
			}
			set 
			{
				this._GeographyObjectID = value;

									this.IsGeographyObjectIDNull = false;
							}
		}			

		[XmlElement(ElementName = "UserGeographyObjectID")]
		public virtual System.Int32? UserGeographyObjectID 
		{
			get 
			{
				return this._UserGeographyObjectID;
			}
			set 
			{
				this._UserGeographyObjectID = value;

									this.IsUserGeographyObjectIDNull = false;
							}
		}			

		[XmlElement(ElementName = "UserID")]
		public virtual System.Int32? UserID 
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



        [XmlElement(ElementName = "KeySize")]
        public virtual System.Int32? KeySize 
		{
			get 
			{
                return this._KeySize;
			}
			set 
			{
                this._KeySize = value;

									
							}
		}

        [XmlElement(ElementName = "IsActive")]
        public bool  IsActive
        {
            get
            {
                return this._IsActive;
            }
            set
            {
                this._IsActive = value;

                
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
