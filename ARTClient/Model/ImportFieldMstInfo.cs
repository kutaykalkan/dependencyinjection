using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{
    [Serializable]
    [DataContract]
    public class ImportFieldMstInfo
    {
        private Int16 _ImportFieldID;
        private String _Description;
        private Int32 _DescriptionLabelID;
        private Int32? _GeographyClassID;

        [XmlElement(ElementName = "ImportFieldID")]
        public Int16 ImportFieldID
        {
            get { return _ImportFieldID; }
            set { _ImportFieldID = value; }
        }

        [XmlElement(ElementName = "Description")]
        public String Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        [XmlElement(ElementName = "DescriptionLabelID")]
        public Int32 DescriptionLabelID
        {
            get { return _DescriptionLabelID; }
            set { _DescriptionLabelID = value; }
        }

        [XmlElement(ElementName = "GeographyClassID")]
        public Int32? GeographyClassID
        {
            get { return _GeographyClassID; }
            set { _GeographyClassID = value; }
        }
    }
}
