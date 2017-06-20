using System;
using Adapdev.Text;
using System.Runtime.Serialization;
using SkyStem.ART.Client.Model.Base;
using SkyStem.ART.Client.Model.RecControlCheckList.Base;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model.RecControlCheckList
{
    /// <summary>
    /// An object representation of the SkyStemART RecControlCheckListHdr table
    /// </summary>
    [Serializable]
    [DataContract]
    public class RecControlCheckListInfo : RecControlCheckListInfoBase
    {
        protected Int64? _GLDataRecControlCheckListID = null;

        protected GLDataRecControlCheckListInfo _oGLDataRecControlCheckListInfo = null;

        [DataMember]
        [XmlElement(ElementName = "GLDataRecControlCheckListID")]
        public virtual System.Int64? GLDataRecControlCheckListID
        {
            get { return this._GLDataRecControlCheckListID; }
            set { this._GLDataRecControlCheckListID = value; }
        }

        [DataMember]
        [XmlElement(ElementName = "oGLDataRecControlCheckListInfo")]
        public GLDataRecControlCheckListInfo oGLDataRecControlCheckListInfo
        {
            get
            {
                return _oGLDataRecControlCheckListInfo;
            }
            set
            {
                _oGLDataRecControlCheckListInfo = value;
            }

        }
        [DataMember]
        [XmlElement(ElementName = "CompanyID")]
        public int? CompanyID { get; set; }
        [DataMember]
        [XmlElement(ElementName = "PhysicalPath")]
        public string PhysicalPath { get; set; }
        [DataMember]
        [XmlElement(ElementName = "RowNumber")]
        public int RowNumber { get; set; }
        [DataMember]
        [XmlElement(ElementName = "DataImportTypeID")]
        public short? DataImportTypeID { get; set; }
    }
}
