

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemART PackageMst table
    /// </summary>
    [Serializable]
    public abstract class PackageMstInfoBase
    {
        protected System.String _AddedBy = string.Empty;
        protected System.DateTime? _DateAdded = null;
        protected System.DateTime? _DateRevised = null;
        protected System.Decimal? _DefaultDiskSpace = null;
        protected System.Int16? _DefaultNumberOfUsers = null;
        protected System.String _HostName = string.Empty;
        protected System.Boolean? _IsActive = null;
        protected System.String _PackageDescription = string.Empty;
        protected System.Int32? _PackageDescriptionLabelID = null;
        protected System.Int16? _PackageID = null;
        protected System.String _PackageName = string.Empty;
        protected System.Int32? _PackageNameLabelID = null;
        protected System.String _RevisedBy = string.Empty;


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

            }
        }

        [XmlElement(ElementName = "DefaultDiskSpace")]
        public virtual System.Decimal? DefaultDiskSpace
        {
            get
            {
                return this._DefaultDiskSpace;
            }
            set
            {
                this._DefaultDiskSpace = value;

            }
        }

        [XmlElement(ElementName = "DefaultNumberOfUsers")]
        public virtual System.Int16? DefaultNumberOfUsers
        {
            get
            {
                return this._DefaultNumberOfUsers;
            }
            set
            {
                this._DefaultNumberOfUsers = value;

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

            }
        }

        [XmlElement(ElementName = "PackageDescription")]
        public virtual System.String PackageDescription
        {
            get
            {
                return this._PackageDescription;
            }
            set
            {
                this._PackageDescription = value;

            }
        }

        [XmlElement(ElementName = "PackageDescriptionLabelID")]
        public virtual System.Int32? PackageDescriptionLabelID
        {
            get
            {
                return this._PackageDescriptionLabelID;
            }
            set
            {
                this._PackageDescriptionLabelID = value;

            }
        }

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

        [XmlElement(ElementName = "PackageName")]
        public virtual System.String PackageName
        {
            get
            {
                return this._PackageName;
            }
            set
            {
                this._PackageName = value;

            }
        }

        [XmlElement(ElementName = "PackageNameLabelID")]
        public virtual System.Int32? PackageNameLabelID
        {
            get
            {
                return this._PackageNameLabelID;
            }
            set
            {
                this._PackageNameLabelID = value;

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
