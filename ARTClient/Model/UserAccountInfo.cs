using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace SkyStem.ART.Client.Model
{

    [Serializable]
	[DataContract]
	public class UserAccountInfo : UserAccountInfoBase
    {
        protected string _EmailID = null;
        protected List<string> _AccountInfoCollection = new List<string>();
        [DataMember]
        [XmlElement(ElementName = "EmailID")]
        public string EmailID
        {
            get
            {
                return this._EmailID;
            }
            set
            {
                this._EmailID = value;
            }
        }

        [DataMember]
        [XmlElement(ElementName = "AccountInfoCollection")]
        public List<string> AccountInfoCollection
        {
            get
            {
                return _AccountInfoCollection;
            }
            set
            {
                _AccountInfoCollection = value;
            }
        }
    }
}
