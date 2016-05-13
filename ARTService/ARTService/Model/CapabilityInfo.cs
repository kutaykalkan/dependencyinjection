//using System;
namespace SkyStem.ART.Service.Model
{
    public class CapabilityInfo
    {
        protected System.Int16? _CapabilityID = null;
        protected System.Boolean? _IsActivated = null;

        //[XmlElement(ElementName = "CapabilityID")]
        public System.Int16? CapabilityID
        {
            get
            {
                return this._CapabilityID;
            }
            set
            {
                this._CapabilityID = value;
            }
        }

        //[XmlElement(ElementName = "IsActivated")]
        public System.Boolean? IsActivated
        {
            get
            {
                return this._IsActivated;
            }
            set
            {
                this._IsActivated = value;
            }
        }
    }//end of class
}//end of namespace
