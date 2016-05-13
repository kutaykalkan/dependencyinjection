using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model.GridInfo
{
    [Serializable]
    [DataContract]
    public class GridDataItemBindingInfo
    {
        private string _uniqueName;
        private string _headerText;
        private string _value;
        private Type _dataType;

        [XmlElement(ElementName = "UniqueName")]
        public string UniqueName
        {
            get { return _uniqueName; }
            set { _uniqueName = value; }
        }

        [XmlElement(ElementName = "HeaderText")]
        public string HeaderText
        {
            get { return _headerText; }
            set { _headerText = value; }
        }

        [XmlElement(ElementName = "Value")]
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        [XmlElement(ElementName = "DataType")]
        public Type DataType
        {
            get { return _dataType; }
            set { _dataType = value; }
        }
    }
}
