using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;


namespace SkyStem.ART.Client.Model.Base
{
    [Serializable]
    public abstract class ThresholdTypeMstBase
    {
        protected System.Int16? _threshldTypeID = 0;
        protected System.String _threshldTypeName = string.Empty;
        protected System.Int32? _threshldTypeLabelID = 0;
        protected System.Boolean? _isActive = false;


        [XmlElement(ElementName = "ThresholdTypeID")]
        public virtual System.Int16? ThresholdTypeID
        {
            get
            {
                return this._threshldTypeID;
            }
            set
            {
                this._threshldTypeID = value;

            }
        }
        [XmlElement(ElementName = "ThresholdTypeName")]
        public virtual System.String ThresholdTypeName
        {
            get
            {
                return this._threshldTypeName;
            }
            set
            {
                this._threshldTypeName = value;

            }
        }
        [XmlElement(ElementName = "ThreshldTypeLabelID")]
        public virtual System.Int32? ThreshldTypeLabelID
        {
            get
            {
                return this._threshldTypeLabelID;
            }
            set
            {
                this._threshldTypeLabelID = value;

            }
        }

        [XmlElement(ElementName = "IsActive")]
        public virtual System.Boolean? IsActive
        {
            get
            {
                return this._isActive;
            }
            set
            {
                this._isActive = value;

            }
        }

    }
}
