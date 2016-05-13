using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using SkyStem.ART.Client.Data;

namespace SkyStem.ART.Client.Model
{

    /// <summary>
    /// An object representation of the SkyStemArt RecPeriodStatusDetail table
    /// </summary>
    [Serializable]
    [DataContract]
    public class RecPeriodStatusDetailInfo
    {
        [XmlElement(ElementName = "RecPeriodStatusDetailID")]
        public int? RecPeriodStatusDetailID { get; set; }
        [XmlElement(ElementName = "RecPeriodID")]
        public int? RecPeriodID { get; set; }
        [XmlElement(ElementName = "StatusID")]
        public short? StatusID { get; set; }
        [XmlElement(ElementName = "StatusDate")]
        public DateTime? StatusDate { get; set; }
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
        [XmlElement(ElementName = "ReconciliationPeriodStatus")]
        public string ReconciliationPeriodStatus { get; set; }
          [XmlElement(ElementName = "ReconciliationPeriodStatusLabelID")]
        public int? ReconciliationPeriodStatusLabelID { get; set; }
    }
}
