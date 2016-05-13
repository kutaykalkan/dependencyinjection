
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Matching.Base;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model.Matching
{

    /// <summary>
    /// An object representation of the SkyStemART MatchingConfiguration table
    /// </summary>
    [Serializable]
    [DataContract]
    public class MatchingConfigurationInfo : MatchingConfigurationInfoBase
    {
        protected System.String _ColumnName1 = null;
        protected System.String _ColumnName2 = null;
        protected List<MatchingConfigurationRuleInfo> _MatchingConfigurationRuleInfoCollection;

        protected System.Int16? _Source1ColumnDataTypeID = null;
        protected System.Int16? _Source2ColumnDataTypeID = null;

        protected System.Boolean? _IsDisplayedColumn = null;
        protected System.Boolean? _IsAmountColumn = null;



        bool IsMatchingConfigurationRuleInfoCollectionNull = true;

        //bool IsSource1ColumnNameNull = true;
        bool IsSource1ColumnDataTypeIDNull = true;
        //bool IsSource2ColumnNameNull = true;
        bool IsSource2ColumnDataTypeIDNull = true;


        [XmlElement(ElementName = "MatchingConfigurationRuleInfoCollection")]
        public List<MatchingConfigurationRuleInfo> MatchingConfigurationRuleInfoCollection
        {
            get
            {
                return _MatchingConfigurationRuleInfoCollection;
            }
            set
            {
                _MatchingConfigurationRuleInfoCollection = value;
                IsMatchingConfigurationRuleInfoCollectionNull = (_MatchingConfigurationRuleInfoCollection == null);
            }

        }
		
		 protected System.Int32? _RecItemColumnID = null;
        [XmlElement(ElementName = "RecItemColumnID")]
        public virtual System.Int32? RecItemColumnID
        {
            get
            {
                return this._RecItemColumnID;
            }
            set
            {
                this._RecItemColumnID = value;
            }
        }
		
		
		


     
        [XmlElement(ElementName = "Source1ColumnDataTypeID")]
        public System.Int16? Source1ColumnDataTypeID
        {
            get
            {
                return _Source1ColumnDataTypeID;
            }
            set
            {
                _Source1ColumnDataTypeID = value;
                IsSource1ColumnDataTypeIDNull = (_Source1ColumnDataTypeID == null);
            }
        }

       
        [XmlElement(ElementName = "Source2ColumnDataTypeID")]
        public System.Int16? Source2ColumnDataTypeID
        {
            get
            {
                return _Source2ColumnDataTypeID;
            }
            set
            {
                _Source2ColumnDataTypeID = value;
                IsSource2ColumnDataTypeIDNull = (_Source2ColumnDataTypeID == null);
            }
        }

        [XmlElement(ElementName = "IsDisplayedColumn")]
        public virtual System.Boolean? IsDisplayedColumn
        {
            get
            {
                return this._IsDisplayedColumn;
            }
            set
            {
                this._IsDisplayedColumn = value;

            }
        }
		
        [XmlElement(ElementName = "ColumnName1")]
        public virtual System.String ColumnName1
        {
            get
            {
                return this._ColumnName1;
            }
            set
            {
                this._ColumnName1 = value;

            }
        }
        [XmlElement(ElementName = "ColumnName2")]
        public virtual System.String ColumnName2
        {
            get
            {
                return this._ColumnName2;
            }
            set
            {
                this._ColumnName2 = value;

            }
        }
        [XmlElement(ElementName = "IsAmountColumn")]
        public virtual System.Boolean? IsAmountColumn
        {
            get
            {
                return this._IsAmountColumn;
            }
            set
            {
                this._IsAmountColumn = value;

            }
        }
		
    }
}
