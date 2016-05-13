using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Client.Model.MappingUpload;
using System.Xml.Serialization;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{
    [Serializable]
    public abstract class MappingUploadMstInfoBase
    {

        protected System.String _AddedBy = string.Empty;
        protected System.DateTime? _DateAdded = null;
        protected System.DateTime? _DateRevised = null;
        protected System.String _HostName = string.Empty;
        protected System.Boolean? _IsActive = null;
        protected System.String _RevisedBy = string.Empty;
        protected System.Int32? _AccountMappingKeyID = null;
        protected System.String _AccountMappingKeyName = string.Empty;
        protected System.Int32? _AccountMappingKeyNameLabelID = null;
        protected System.Boolean _ToBeDisplayed = false;

        [XmlElement(ElementName = "ToBeDisplayed")]
        public virtual System.Boolean ToBeDisplayed
        {
            get
            {
                return this._ToBeDisplayed;
            }
            set
            {
                this._ToBeDisplayed = value;

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

        [XmlElement(ElementName = "AccountMappingKeyID")]
        public virtual System.Int32? AccountMappingKeyID
        {
            get
            {
                return this._AccountMappingKeyID;
            }
            set
            {
                this._AccountMappingKeyID = value;

            }
        }

        [XmlElement(ElementName = "AccountMappingKeyName")]
        public virtual System.String AccountMappingKeyName
        {
            get
            {
                return this._AccountMappingKeyName;
            }
            set
            {
                this._AccountMappingKeyName = value;

            }
        }

        [XmlElement(ElementName = "AccountMappingKeyNameLabelID")]
        public virtual System.Int32? AccountMappingKeyNameLabelID
        {
            get
            {
                return this._AccountMappingKeyNameLabelID;
            }
            set
            {
                this._AccountMappingKeyNameLabelID = value;

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
