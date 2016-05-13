
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the Account And Certification Status
	/// </summary>
	[Serializable]
	[DataContract]
	public class AccountCertificationStatusInfo 
	{
        List<AccountStatusInfo> _oAccountStatusInfoCollection = new  List<AccountStatusInfo>();

        [XmlElement(ElementName = "oAccountStatusInfo")]
        public List< AccountStatusInfo> oAccountStatusInfoCollection
        {
            get { return _oAccountStatusInfoCollection; }
            set { _oAccountStatusInfoCollection = value; }
        }

        CertificationStatusInfo _oCertificationStatusInfo = new CertificationStatusInfo();

        [XmlElement(ElementName = "oCertificationStatusInfo")]
        public CertificationStatusInfo oCertificationStatusInfo
        {
            get
            {
                return _oCertificationStatusInfo;
            }
            set
            {
                _oCertificationStatusInfo = value;               
            }
        }



	}
}
