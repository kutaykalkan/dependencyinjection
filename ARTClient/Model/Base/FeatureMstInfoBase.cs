

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemART FeatureMst table
    /// </summary>
    [Serializable]
    public abstract class FeatureMstInfoBase
    {

        protected System.String _AddedBy = string.Empty;
        protected System.DateTime? _DateAdded = null;
        protected System.DateTime? _DateRevised = null;
        protected System.Int16? _FeatureID = null;
        protected System.String _FeatureName = string.Empty;
        protected System.Int32? _FeatureNameLabelID = null;
        protected System.String _HostName = string.Empty;
        protected System.Boolean? _IsActive = null;
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

        [XmlElement(ElementName = "FeatureID")]
        public virtual System.Int16? FeatureID
        {
            get
            {
                return this._FeatureID;
            }
            set
            {
                this._FeatureID = value;

            }
        }

        [XmlElement(ElementName = "FeatureName")]
        public virtual System.String FeatureName
        {
            get
            {
                return this._FeatureName;
            }
            set
            {
                this._FeatureName = value;

            }
        }

        [XmlElement(ElementName = "FeatureNameLabelID")]
        public virtual System.Int32? FeatureNameLabelID
        {
            get
            {
                return this._FeatureNameLabelID;
            }
            set
            {
                this._FeatureNameLabelID = value;

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


        /// <summary>
        /// Returns a string representation of the object, displaying all property and field names and values.
        /// </summary>
        public override string ToString()
        {
            return StringUtil.ToString(this);
        }







    }
}
