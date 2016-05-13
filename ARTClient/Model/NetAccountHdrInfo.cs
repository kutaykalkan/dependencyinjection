
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemArt NetAccountHdr table
	/// </summary>
	[Serializable]
	[DataContract]
	public class NetAccountHdrInfo : NetAccountHdrInfoBase
	{
        protected System.Int32? _CompanyID = 0;
        protected System.String _NetAccountLabelText=string.Empty;
        [XmlElement(ElementName = "CompanyID")]
        public  System.Int32? CompanyID
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
        [XmlElement(ElementName = "NetAccountLabelText")]
        public System.String NetAccountLabelText
        {
            get
            {
                return this._NetAccountLabelText;
            }
            set
            {
                this._NetAccountLabelText = value;
            }
        }

        [XmlElement(ElementName = "NetAccountAttributeValueInfoList")]
        public List<NetAccountAttributeValueInfo> NetAccountAttributeValueInfoList { get; set; }

        [XmlElement(ElementName = "IsLocked")]
        public bool? IsLocked { get; set; }

        [XmlElement(ElementName = "ReconciliationStatusID")]
        public short? ReconciliationStatusID { get; set; }

        [XmlElement(ElementName = "IsSystemReconciled")]
        public bool? IsSystemReconciled { get; set; }

        [XmlElement(ElementName = "IsAccountSelectionChanged")]
        public bool? IsAccountSelectionChanged { get; set; }
    }
}
