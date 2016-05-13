using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{
    [Serializable]
    [DataContract]
    public class FilterInfo
    {
        private long? _columnID;
        private string _columnName = string.Empty;
        private short? _columnDataType = 0;
        private short? _operatorID = 0;
        private string _value = string.Empty;
        private string _displayColumnName = string.Empty;

        [XmlElement(ElementName = "ColumnID")]
        public long? ColumnID
        {
            get { return _columnID; }
            set { _columnID = value; }
        }
        [XmlElement(ElementName = "ColumnName")]
        public string ColumnName
        {
            get { return _columnName; }
            set { _columnName = value; }
        }
        [XmlElement(ElementName = "DataType")]
        public short? DataType
        {
            get { return _columnDataType; }
            set { _columnDataType = value; }
        }
        [XmlElement(ElementName = "OperatorID")]
        public short? OperatorID
        {
            get { return _operatorID; }
            set { _operatorID = value; }
        }
        [XmlElement(ElementName = "Value")]
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
        [XmlElement(ElementName = "DisplayColumnName")]
        public string DisplayColumnName
        {
            get { return _displayColumnName; }
            set { _displayColumnName = value; }
        }

    }
}
