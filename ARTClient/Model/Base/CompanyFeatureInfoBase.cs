

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemART CompanyFeature table
    /// </summary>
    [Serializable]
    public abstract class CompanyFeatureInfoBase
    {


        protected System.Int32? _CompanyFeatureID = null;
        protected System.Int32? _CompanyID = null;
        protected System.Int16? _FeatureID = null;

        [XmlElement(ElementName = "CompanyFeatureID")]
        public virtual System.Int32? CompanyFeatureID
        {
            get
            {
                return this._CompanyFeatureID;
            }
            set
            {
                this._CompanyFeatureID = value;

            }
        }

        [XmlElement(ElementName = "CompanyID")]
        public virtual System.Int32? CompanyID
        {
            get
            {
                return this._CompanyID;
            }
            set
            {
                this._CompanyID = value;
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


        /// <summary>
        /// Returns a string representation of the object, displaying all property and field names and values.
        /// </summary>
        public override string ToString()
        {
            return StringUtil.ToString(this);
        }




    }
}
