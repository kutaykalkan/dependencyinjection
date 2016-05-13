using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{
    [Serializable]
    [DataContract]
    public class FSCaptionInfo_ExtendedWithMaterialityInfo : FSCaptionHdrInfo
    {
        protected System.Int32? _FSCaptionMaterialityID = 0;
        protected System.Decimal? _MaterialityThreshold = null;

        public bool IsFSCaptionMaterialityIDNull = true;
        [XmlElement(ElementName = "FSCaptionMaterialityID")]
        public virtual System.Int32? FSCaptionMaterialityID
        {
            get
            {
                return this._FSCaptionMaterialityID;
            }
            set
            {
                this._FSCaptionMaterialityID = value;

                this.IsFSCaptionMaterialityIDNull = false;
            }
        }

        public bool IsMaterialityThresholdNull = true;
        [XmlElement(ElementName = "MaterialityThreshold")]
        public virtual System.Decimal? MaterialityThreshold
        {
            get
            {
                return this._MaterialityThreshold;
            }
            set
            {
                this._MaterialityThreshold = value;

                this.IsMaterialityThresholdNull = false;
            }
        }			


    }
}
