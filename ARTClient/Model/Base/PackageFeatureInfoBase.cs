

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemART PackageFeature table
    /// </summary>
    [Serializable]
    public abstract class PackageFeatureInfoBase
    {
        protected System.Int16? _FeatureID = null;
        protected System.Int16? _PackageFeatureID = null;
        protected System.Int16? _PackageID = null;
        protected System.Boolean? _IsFullFeatureAvailable = null;

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

        [XmlElement(ElementName = "PackageFeatureID")]
        public virtual System.Int16? PackageFeatureID
        {
            get
            {
                return this._PackageFeatureID;
            }
            set
            {
                this._PackageFeatureID = value;

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

        [XmlElement(ElementName = "IsFullFeatureAvailable")]
        public virtual System.Boolean? IsFullFeatureAvailable
        {
            get
            {
                return this._IsFullFeatureAvailable;
            }
            set
            {
                this._IsFullFeatureAvailable = value;

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
