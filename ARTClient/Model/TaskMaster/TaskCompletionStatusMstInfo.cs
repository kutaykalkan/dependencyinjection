
using System;
using Adapdev.Text;
using System.Runtime.Serialization;
using SkyStem.ART.Client.Model.Base;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemART TaskCompletionStatusMst table
	/// </summary>
	[Serializable]
	[DataContract]
	public class TaskCompletionStatusMstInfo : TaskCompletionStatusMstInfoBase
	{
        private System.Int32? _CompletionStatusCount = null;
        private System.String _StatusColor = null;
        private System.Int32? _ATCount = null;
        private System.Int32? _GTCount = null;


        [DataMember]
        [XmlElement(ElementName = "CompletionStatusCount")]
        public System.Int32? CompletionStatusCount
        {
            get { return _CompletionStatusCount; }
            set { _CompletionStatusCount = value; }
        }

        [DataMember]
        [XmlElement(ElementName = "StatusColor")]
        public System.String StatusColor
        {
            get { return _StatusColor; }
            set { _StatusColor = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "ATCount")]
        public System.Int32? ATCount
        {
            get { return _ATCount; }
            set { _ATCount = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "GTCount")]
        public System.Int32? GTCount
        {
            get { return _GTCount; }
            set { _GTCount = value; }
        }

	}
}
