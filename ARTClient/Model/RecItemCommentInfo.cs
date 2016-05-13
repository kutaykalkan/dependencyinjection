
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{

    /// <summary>
    /// An object representation of the SkyStemArt RecItemCommentHdr table
    /// </summary>
    [Serializable]
    [DataContract]
    public class RecItemCommentInfo : RecItemCommentInfoBase
    {
        string _RecItemNumber;
        string _RecItemDescription;
        UserHdrInfo _AddedByUserInfo = null;

        [XmlElement(ElementName = "AddedByUserInfo")]
        public UserHdrInfo AddedByUserInfo
        {
            get { return _AddedByUserInfo; }
            set { _AddedByUserInfo = value; }
        }

        public string RecItemDescription
        {
            get { return _RecItemDescription; }
            set { _RecItemDescription = value; }
        }
        public string RecItemNumber
        {
            get { return _RecItemNumber; }
            set { _RecItemNumber = value; }
        }
         

    }
}
