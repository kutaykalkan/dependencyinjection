

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemART PackageRole table
    /// </summary>
    [Serializable]
    public abstract class PackageRoleInfoBase
    {
        protected System.Int16? _PackageID = null;
        protected System.Int16? _PackageRoleID = null;
        protected System.Int16? _RoleID = null;

        [XmlElement(ElementName = "PackageID")]
        public virtual System.Int16? PackageID
        {
            get
            {
                return this._PackageID;
            }
            set
            {
                this._PackageID = value;

            }
        }

        [XmlElement(ElementName = "PackageRoleID")]
        public virtual System.Int16? PackageRoleID
        {
            get
            {
                return this._PackageRoleID;
            }
            set
            {
                this._PackageRoleID = value;

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
