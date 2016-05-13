using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using SkyStem.ART.Client.Model.Base;


namespace SkyStem.ART.Client.Model.MappingUpload
{
    [DataContract]
    [Serializable]
    public class MappingUploadInfo : MappingUploadInfoBase
    {
        protected System.Int32? _RecPeriodID = null;

        [XmlElement(ElementName = "RecPeriodID")]
        public virtual System.Int32? RecPeriodID
        {
            get
            {
                return this._RecPeriodID;
            }
            set
            {
                this._RecPeriodID = value;

            }
        }

        protected System.Int32? _StartRecPeriodID = null;
        protected System.Int32? _EndRecPeriodID = null;
        [XmlElement(ElementName = "StartRecPeriodID")]
        public virtual System.Int32? StartRecPeriodID
        {
            get
            {
                return this._StartRecPeriodID;
            }
            set
            {
                this._StartRecPeriodID = value;

            }
        }

        [XmlElement(ElementName = "EndRecPeriodID")]
        public virtual System.Int32? EndRecPeriodID
        {
            get
            {
                return this._EndRecPeriodID;
            }
            set
            {
                this._EndRecPeriodID = value;

            }
        }

    }
}
