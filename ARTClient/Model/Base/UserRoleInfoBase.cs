

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemArt UserRole table
    /// </summary>
    [Serializable]
    public abstract class UserRoleInfoBase 
    {
        protected System.Boolean? _IsPotentialRole = false;
        protected System.Int16? _RoleID = 0;
        protected System.Int32? _UserID = 0;
        protected System.Int32? _UserRoleID = 0;

        public bool IsIsPotentialRoleNull = true;


        public bool IsRoleIDNull = true;


        public bool IsUserIDNull = true;


        public bool IsUserRoleIDNull = true;

        [XmlElement(ElementName = "IsPotentialRole")]
        public virtual System.Boolean? IsPotentialRole
        {
            get
            {
                return this._IsPotentialRole;
            }
            set
            {
                this._IsPotentialRole = value;

                this.IsIsPotentialRoleNull = false;
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

        [XmlElement(ElementName = "UserRoleID")]
        public virtual System.Int32? UserRoleID
        {
            get
            {
                return this._UserRoleID;
            }
            set
            {
                this._UserRoleID = value;

                this.IsUserRoleIDNull = false;
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
