using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{
    [Serializable]
    public class AddressInfo
    {
        protected System.String _Address1 = "";
        protected System.String _Address2 = "";
        protected System.String _City = "";
        protected System.String _State = "";
        protected System.String _Country = "";
        protected System.String _Zip = "";

        [XmlElement(ElementName = "Address1")]
        public virtual System.String Address1
        {
            get
            {
                return this._Address1;
            }
            set
            {
                this._Address1 = value;
            }
        }

        [XmlElement(ElementName = "Address2")]
        public virtual System.String Address2
        {
            get
            {
                return this._Address2;
            }
            set
            {
                this._Address2 = value;
            }
        }

        [XmlElement(ElementName = "City")]
        public virtual System.String City
        {
            get
            {
                return this._City;
            }
            set
            {
                this._City = value;
            }
        }

        [XmlElement(ElementName = "State")]
        public virtual System.String State
        {
            get
            {
                return this._State;
            }
            set
            {
                this._State = value;
            }
        }

        [XmlElement(ElementName = "Zip")]
        public virtual System.String Zip
        {
            get
            {
                return this._Zip;
            }
            set
            {
                this._Zip = value;
            }
        }

        [XmlElement(ElementName = "Country")]
        public virtual System.String Country
        {
            get
            {
                return this._Country;
            }
            set
            {
                this._Country = value;
            }
        }


    }
}
