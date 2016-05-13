using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{
    [Serializable]
    [DataContract]
    public class FilterCriteria
    {
        private long _MatchingSourceDataImportID;
        private short _columnType;
        private string _columnName = string.Empty;
        private short _parameterID;
        private short _operatorID;
        private string _value;
        private string _displayValue;

        //DataType for the Column
        [XmlElement(ElementName = "ColumnType")]
        public short ColumnType
        {
            get
            {
                return this._columnType;
            }
            set
            {
                this._columnType = value;
            }
        }

        [XmlElement(ElementName = "ParameterID")]
        public short ParameterID
        {
            get
            {
                return this._parameterID;
            }
            set
            {
                this._parameterID = value;
            }
        }

        [XmlElement(ElementName = "OperatorID")]
        public short OperatorID
        {
            get
            {
                return this._operatorID;
            }
            set
            {
                this._operatorID = value;
            }
        }

        [XmlElement(ElementName = "Value")]
        public string Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
            }
        }

        [XmlElement(ElementName = "DisplayValue")]
        public string DisplayValue
        {
            get
            {
                return this._displayValue;
            }
            set
            {
                this._displayValue = value;
            }
        }


        [XmlElement(ElementName = "MatchingSourceDataImportID")]
        public long MatchingSourceDataImportID
        {
            get
            {
                return this._MatchingSourceDataImportID;
            }
            set
            {
                this._MatchingSourceDataImportID = value;
            }
        }

        public string ColumnName
        {
            get { return _columnName; }
            set { _columnName = value; }
        }
    }
}
