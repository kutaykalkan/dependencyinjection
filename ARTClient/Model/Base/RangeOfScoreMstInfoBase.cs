using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;


namespace SkyStem.ART.Client.Model.Base
{
    [Serializable]
    public class RangeOfScoreMstInfoBase
    {
        protected System.Int32? _RangeOfScoreCategoryID = 0;
        protected System.String _RangeOfScoreCategoryName = "";
        protected System.Int32? _RangeOfscoreCategoryLabelID = 0;

        [XmlElement(ElementName = "RangeOfScoreCategoryID ")]
        public virtual System.Int32? RangeOfScoreCategoryID 
        {
            get
            {
                return this._RangeOfScoreCategoryID;
            }
            set
            {
                this._RangeOfScoreCategoryID = value;
            }
        }

        [XmlElement(ElementName = "RangeOfScoreCategoryName ")]
        public virtual System.String RangeOfScoreCategoryName
        {
            get
            {
                return this._RangeOfScoreCategoryName;
            }
            set
            {
                this._RangeOfScoreCategoryName = value;
            }
        }

        [XmlElement(ElementName = "RangeOfscoreCategoryLabelID ")]
        public virtual System.Int32? RangeOfscoreCategoryLabelID
        {
            get
            {
                return this._RangeOfscoreCategoryLabelID;
            }
            set
            {
                this._RangeOfscoreCategoryLabelID = value;
            }
        }
    }
}
