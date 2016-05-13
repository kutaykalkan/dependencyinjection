using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkyStem.ART.Client.Model.Base
{
    [Serializable]
    public class MultilingualInfo
    {
        private System.String _Name = "";
        private System.Int32? _LabelID = 0;

        [XmlElement(ElementName = "Name")]
        public System.String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        [XmlElement(ElementName = "LabelID")]
        public System.Int32? LabelID
        {
            get { return _LabelID; }
            set { _LabelID = value; }
        }

    }
}
