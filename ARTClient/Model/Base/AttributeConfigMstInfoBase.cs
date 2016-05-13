using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Adapdev.Text;


namespace SkyStem.ART.Client.Model.Base
{
    [Serializable]
    public class AttributeConfigMstInfoBase
    {

        protected System.String _AddedBy = string.Empty;
        protected System.DateTime? _DateAdded = null;
        protected System.DateTime? _DateRevised = null;
        protected System.String _HostName = string.Empty;
        protected System.Boolean? _IsActive = null;
        protected System.String _RevisedBy = string.Empty;
        protected System.Int16? _SortOrder = null;
        protected System.Int16? _RoleConfigID = null;
        protected System.String _RoleConfigName = string.Empty;
        protected System.Int32? _RoleConfigNameLabelID = null;


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

        [XmlElement(ElementName = "SortOrder")]
        public virtual System.Int16? SortOrder
        {
            get
            {
                return this._SortOrder;
            }
            set
            {
                this._SortOrder = value;

            }
        }

        [XmlElement(ElementName = "RoleConfigID")]
        public virtual System.Int16? RoleConfigID
        {
            get
            {
                return this._RoleConfigID;
            }
            set
            {
                this._RoleConfigID = value;

            }
        }

        [XmlElement(ElementName = "RoleConfigName")]
        public virtual System.String RoleConfigName
        {
            get
            {
                return this._RoleConfigName;
            }
            set
            {
                this._RoleConfigName = value;

            }
        }

        [XmlElement(ElementName = "RoleConfigNameLabelID")]
        public virtual System.Int32? RoleConfigNameLabelID
        {
            get
            {
                return this._RoleConfigNameLabelID;
            }
            set
            {
                this._RoleConfigNameLabelID = value;

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
