
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{

    /// <summary>
    /// An object representation of the SkyStemArt DualLevelReviewTypeMstInfo table
    /// </summary>
    [Serializable]
    [DataContract]
    public class DualLevelReviewTypeMstInfo : MultilingualInfo
    {

        private Int16? _DualLevelReviewTypeID;
        [XmlElement(ElementName = "DualLevelReviewTypeID")]
        public Int16? DualLevelReviewTypeID
        {
            get { return _DualLevelReviewTypeID; }
            set { _DualLevelReviewTypeID = value; }
        }
        private int? _DualLevelReviewTypeLabelID;
        [XmlElement(ElementName = "DualLevelReviewTypeLabelID")]
        public int? DualLevelReviewTypeLabelID
        {
            get { return _DualLevelReviewTypeLabelID; }
            set { _DualLevelReviewTypeLabelID = value; }
        }
        string _DualLevelReviewType;
        [XmlElement(ElementName = "DualLevelReviewType")]
        public string DualLevelReviewType
        {
            get { return _DualLevelReviewType; }
            set { _DualLevelReviewType = value; }
        }
        bool? _IsActive;
        [XmlElement(ElementName = "IsActive")]
        public bool? IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

    }
}
