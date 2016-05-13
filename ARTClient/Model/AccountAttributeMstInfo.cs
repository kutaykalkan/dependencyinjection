using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using SkyStem.ART.Client.Model.Base;

namespace SkyStem.ART.Client.Model
{
    /// <summary>
    /// An object representation of the SkyStemArt AccountAttributeMst table
    /// </summary>
    [Serializable]
    [DataContract]
    public class AccountAttributeMstInfo : MultilingualInfo
    {
        protected System.Int16? _AccountAttributeID;
        protected System.String _AccountAttribute;
        protected System.Int32? _AccountAttributeLabelID;
        protected System.String _Description;
        protected System.Int16? _CapabilityID;
        protected System.Int16? _DataTypeID;
        protected System.Int16? _SortOrder;
        protected System.Boolean? _IsActive;
        protected System.DateTime? _DateAdded;
        protected System.String _AddedBy;
        protected System.DateTime? _DateRevised;
        protected System.String _RevisedBy;
        protected System.String _HostName;

        [XmlElement(ElementName = "AccountAttributeID")]
        public System.Int16? AccountAttributeID 
        {
            get
            {
                return this._AccountAttributeID;
            }
            set
            {
                this._AccountAttributeID = value;
            }
        }

        [XmlElement(ElementName = "AccountAttribute")]
        public System.String AccountAttribute
        {
            get
            {
                return this._AccountAttribute;
            }
            set
            {
                this._AccountAttribute = value;
            }
        }

        [XmlElement(ElementName = "AccountAttributeLabelID")]
        public System.Int32? AccountAttributeLabelID
        {
            get
            {
                return this._AccountAttributeLabelID;
            }
            set
            {
                this._AccountAttributeLabelID = value;
            }
        }

        [XmlElement(ElementName = "Description")]
        public System.String Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this._Description = value;
            }
        }

        [XmlElement(ElementName = "DataTypeID")]
        public System.Int16? DataTypeID
        {
            get
            {
                return this._DataTypeID;
            }
            set
            {
                this._DataTypeID = value;
            }
        }

        [XmlElement(ElementName = "SortOrder")]
        public System.Int16? SortOrder
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

        [XmlElement(ElementName = "IsActive")]
        public System.Boolean? IsActive
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
    }
}
