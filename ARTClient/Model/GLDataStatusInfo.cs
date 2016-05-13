
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using SkyStem.ART.Client.Data;

namespace SkyStem.ART.Client.Model
{

    /// <summary>
    /// An object representation of the SkyStemArt GLDataStatus table
    /// </summary>
    [Serializable]
    [DataContract]
    public class GLDataStatusInfo : GLDataStatusInfoBase
    {
        UserHdrInfo _AddedByUserInfo = null;

        [XmlElement(ElementName = "AddedByUserInfo")]
        public UserHdrInfo AddedByUserInfo
        {
            get { return _AddedByUserInfo; }
            set { _AddedByUserInfo = value; }
        }

        [XmlElement(ElementName = "FullName")]
        public String FullName
        {
            get
            {
                return ModelHelper.GetFullName(this.AddedByUserInfo.FirstName, this.AddedByUserInfo.LastName);
            }
        }

        [XmlElement(ElementName = "Status")]
        public System.String Status
        {
            get
            {
                return this.Name;
            }
            set
            {
                this.Name = value;
            }
        }

        [XmlElement(ElementName = "StatusLabelID")]
        public System.Int32? StatusLabelID
        {
            get
            {
                return this.LabelID;
            }
            set
            {
                this.LabelID = value;
            }
        }


    }
}
