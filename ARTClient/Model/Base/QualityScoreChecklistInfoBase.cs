using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model.Base
{
    [Serializable]
    public class QualityScoreChecklistInfoBase
    {
        protected System.Int32? _QualityScoreID = 0;
        protected System.Int32? _QualityScoreDescriptionLabelID = 0;
        protected System.String _QualityScoreNumber = null;
        protected System.String _QualityScoreDescription = null;

        [XmlElement(ElementName = "QualityScoreID ")]
        public virtual System.Int32? QualityScoreID
        {
            get
            {
                return this._QualityScoreID;
            }
            set
            {
                this._QualityScoreID = value;
            }
        }

        [XmlElement(ElementName = "QualityScoreDescriptionLabelID ")]
        public virtual System.Int32? QualityScoreDescriptionLabelID
        {
            get
            {
                return this._QualityScoreDescriptionLabelID;
            }
            set
            {
                this._QualityScoreDescriptionLabelID = value;
            }
        }

        [XmlElement(ElementName = "QualityScoreNumber ")]
        public virtual System.String QualityScoreNumber
        {
            get
            {
                return this._QualityScoreNumber;
            }
            set
            {
                this._QualityScoreNumber = value;
            }
        }

        [XmlElement(ElementName = "QualityScoreDescription")]
        public virtual System.String QualityScoreDescription
        {
            get
            {
                return this._QualityScoreDescription;
            }
            set
            {
                this._QualityScoreDescription = value;
            }
        }

    }
}
