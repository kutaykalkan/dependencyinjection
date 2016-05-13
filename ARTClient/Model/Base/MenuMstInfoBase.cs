

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{
    /// <summary>
    /// An object representation of the SkyStemArt MenuMst table
    /// </summary>
    [Serializable]
    public abstract class MenuMstInfoBase
    {
        protected System.String _AddedBy = "";
        protected System.DateTime? _DateAdded = DateTime.Now;
        protected System.DateTime? _DateRevised = DateTime.Now;
        protected System.String _Description = "";
        protected System.String _HostName = "";
        protected System.Boolean? _IsActive = false;
        protected System.String _MenuKey = "";
        protected System.String _Menu = "";
        protected System.Int16? _MenuID = 0;
        protected System.Int32? _MenuLabelID = 0;
        protected System.String _MenuURL = null;
        protected System.Int16? _ParentMenuID = null;
        protected System.String _RevisedBy = "";
        public bool IsAddedByNull = true;
        public bool IsDateAddedNull = true;
        public bool IsDateRevisedNull = true;
        public bool IsDescriptionNull = true;
        public bool IsHostNameNull = true;
        public bool IsIsActiveNull = true;
        public bool IsMenuNull = true;
        public bool IsMenuIDNull = true;
        public bool IsMenuLabelIDNull = true;
        public bool IsMenuURLNull = true;
        public bool IsParentMenuIDNull = true;
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

        [XmlElement(ElementName = "MenuKey")]
        public virtual System.String MenuKey
        {
            get
            {
                return this._MenuKey;
            }
            set
            {
                this._MenuKey = value;
            }
        }


        [XmlElement(ElementName = "Menu")]
        public virtual System.String Menu
        {
            get
            {
                return this._Menu;
            }
            set
            {
                this._Menu = value;

                this.IsMenuNull = (_Menu == null);
            }
        }

        [XmlElement(ElementName = "MenuID")]
        public virtual System.Int16? MenuID
        {
            get
            {
                return this._MenuID;
            }
            set
            {
                this._MenuID = value;

                this.IsMenuIDNull = false;
            }
        }

        [XmlElement(ElementName = "MenuLabelID")]
        public virtual System.Int32? MenuLabelID
        {
            get
            {
                return this._MenuLabelID;
            }
            set
            {
                this._MenuLabelID = value;

                this.IsMenuLabelIDNull = false;
            }
        }

        [XmlElement(ElementName = "MenuURL")]
        public virtual System.String MenuURL
        {
            get
            {
                return this._MenuURL;
            }
            set
            {
                this._MenuURL = value;

                this.IsMenuURLNull = (_MenuURL == null);
            }
        }

        [XmlElement(ElementName = "ParentMenuID")]
        public virtual System.Int16? ParentMenuID
        {
            get
            {
                return this._ParentMenuID;
            }
            set
            {
                this._ParentMenuID = value;

                this.IsParentMenuIDNull = false;
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
