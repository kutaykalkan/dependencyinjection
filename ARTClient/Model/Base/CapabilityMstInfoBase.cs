

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{
    /// <summary>
    /// An object representation of the SkyStemArt CapabilityMst table
    /// </summary>
    [Serializable]
    public abstract class CapabilityMstInfoBase
    {
        protected System.String _AddedBy = "";
        protected System.String _Capability = "";
        protected System.Int16? _CapabilityID = 0;
        protected System.Int32? _CapabilityLabelID = 0;
        protected System.DateTime? _DateAdded = DateTime.Now;
        protected System.DateTime? _DateRevised = DateTime.Now;
        protected System.String _Description = "";
        protected System.String _HostName = "";
        protected System.Boolean? _IsActive = false;
        protected System.Boolean? _IsConfigurationRequired = false;
        protected System.String _RevisedBy = "";
        protected System.Int16? _SortOrder = 0;


        public bool IsAddedByNull = true;
        public bool IsCapabilityNull = true;
        public bool IsCapabilityIDNull = true;
        public bool IsCapabilityLabelIDNull = true;
        public bool IsDateAddedNull = true;
        public bool IsDateRevisedNull = true;
        public bool IsDescriptionNull = true;
        public bool IsHostNameNull = true;
        public bool IsIsActiveNull = true;
        public bool IsIsConfigurationRequiredNull = true;
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

        [XmlElement(ElementName = "Capability")]
        public virtual System.String Capability
        {
            get
            {
                return this._Capability;
            }
            set
            {
                this._Capability = value;

                this.IsCapabilityNull = (_Capability == null);
            }
        }

        [XmlElement(ElementName = "CapabilityID")]
        public virtual System.Int16? CapabilityID
        {
            get
            {
                return this._CapabilityID;
            }
            set
            {
                this._CapabilityID = value;

                this.IsCapabilityIDNull = false;
            }
        }

        [XmlElement(ElementName = "CapabilityLabelID")]
        public virtual System.Int32? CapabilityLabelID
        {
            get
            {
                return this._CapabilityLabelID;
            }
            set
            {
                this._CapabilityLabelID = value;

                this.IsCapabilityLabelIDNull = false;
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

        [XmlElement(ElementName = "IsConfigurationRequired")]
        public virtual System.Boolean? IsConfigurationRequired
        {
            get
            {
                return this._IsConfigurationRequired;
            }
            set
            {
                this._IsConfigurationRequired = value;

                this.IsIsConfigurationRequiredNull = false;
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
        public virtual System.Int16? SortOrder
        {
            get
            {
                return this._SortOrder;
            }
            set
            {
                this._SortOrder = value;

                this.IsSortOrderNull = (_SortOrder == null);
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
