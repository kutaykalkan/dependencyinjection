using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{
    [Serializable]
    public abstract class MappingUploadInfoBase
    {
        protected System.Int32? _CompanyRecPeriodSetID = null;
        protected System.Int32? _CompanyMappingUploadID = null;
        protected System.Int16? _MappingKeyID = null;
        protected System.Int32? _AccountMappingKeyID = null;
        protected System.String _AccountMappingKeyName = string.Empty;
        protected System.Int32? _AccountMappingKeyNameLabelID = null;
        protected System.Int32? _SelectedKeysID = null;
        protected System.Boolean _IsEnabled = default(bool);
        protected System.Int32? _GeographyStructureLabelID = null;
        protected System.String _GeographyStructure = string.Empty;

        

        [XmlElement(ElementName = "IsEnabled")]
        public virtual System.Boolean IsEnabled
        {
            get
            {
                return this._IsEnabled;
            }
            set
            {
                this._IsEnabled = value;

            }
        }

        [XmlElement(ElementName = "SelectedKeysID")]
        public virtual System.Int32? SelectedKeysID
        {
            get
            {
                return this._SelectedKeysID;
            }
            set
            {
                this._SelectedKeysID = value;

            }
        }

        [XmlElement(ElementName = "GeographyStructureLabelID")]
        public virtual System.Int32? GeographyStructureLabelID
        {
            get
            {
                return this._GeographyStructureLabelID;
            }
            set
            {
                this._GeographyStructureLabelID = value;

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

        [XmlElement(ElementName = "CompanyRecPeriodSetID")]
        public virtual System.Int32? CompanyRecPeriodSetID
        {
            get
            {
                return this._CompanyRecPeriodSetID;
            }
            set
            {
                this._CompanyRecPeriodSetID = value;

            }
        }

        [XmlElement(ElementName = "CompanyMappingUploadID")]
        public virtual System.Int32? CompanyMappingUploadID
        {
            get
            {
                return this._CompanyMappingUploadID;
            }
            set
            {
                this._CompanyMappingUploadID = value;

            }
        }

        [XmlElement(ElementName = "MappingKeyID")]
        public virtual System.Int16? MappingKeyID
        {
            get
            {
                return this._MappingKeyID;
            }
            set
            {
                this._MappingKeyID = value;

            }
        }

        [XmlElement(ElementName = "GeographyStructure")]
        public virtual System.String GeographyStructure
        {
            get
            {
                return this._GeographyStructure;
            }
            set
            {
                this._GeographyStructure = value;

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
