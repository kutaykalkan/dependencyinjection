
using System;
using Adapdev.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using SkyStem.ART.Client.Model.CompanyDatabase.Base;
using System.Xml;

namespace SkyStem.ART.Client.Model.CompanyDatabase
{    

	/// <summary>
	/// An object representation of the SkyStemARTCore ServerCompany table
	/// </summary>
	[Serializable]
	[DataContract]
	public class ServerCompanyInfo : ServerCompanyInfoBase
	{
        protected System.String _CompanyName = "";

        [XmlElement(ElementName = "CompanyName")]
        public virtual System.String CompanyName
        {
            get
            {
                return this._CompanyName;
            }
            set
            {
                this._CompanyName = value;
            }
        }

        [DataMember]
        public string ServerName { get; set; }

        [DataMember]
        public string Instance { get; set; }

        [DataMember]
        public string DBUserID { get; set; }

        [DataMember]
        public string DBPassword { get; set; }
        protected System.Boolean? _IsVersionAlreadyRun = null;
        [DataMember]
        [XmlElement(ElementName = "IsActive")]
        public virtual System.Boolean? IsVersionAlreadyRun
        {
            get { return this._IsVersionAlreadyRun; }
            set { this._IsVersionAlreadyRun = value; }
        } 

	}
}
