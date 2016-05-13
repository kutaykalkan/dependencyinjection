
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{

    /// <summary>
    /// An object representation of the SkyStemArt GLDataReviewNoteDetail table
    /// </summary>
    [Serializable]
    [DataContract]
    public class GLDataReviewNoteDetailInfo : GLDataReviewNoteDetailInfoBase
    {
        UserHdrInfo _AddedByUserInfo = null;
        short? _AddedByRole = null;

        [XmlElement(ElementName = "AddedByUserInfo")]
        public UserHdrInfo AddedByUserInfo
        {
            get { return _AddedByUserInfo; }
            set { _AddedByUserInfo = value; }
        }

        [XmlElement(ElementName = "AddedByRole")]
        public short? AddedByRole
        {
            get { return _AddedByRole; }
            set { _AddedByRole = value; }
        }

    }
}
