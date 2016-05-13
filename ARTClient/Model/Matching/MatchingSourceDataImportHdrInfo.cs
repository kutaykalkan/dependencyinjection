
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Matching.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Collections.Generic;


namespace SkyStem.ART.Client.Model.Matching
{    

	/// <summary>
	/// An object representation of the SkyStemART MatchingSourceDataImportHdr table
	/// </summary>
	[Serializable]
	[DataContract]
	public class MatchingSourceDataImportHdrInfo : MatchingSourceDataImportHdrInfoBase
	{

        protected System.Int32? _RecItemCreatedCount = null;
        protected System.Int32? _RecordsLeft = null;

        public bool IsRecItemCreatedCountNull = true;
        public bool IsRecordsLeftNull = true;
        
        public bool IsSelected = false;
        

        protected System.String _DataImportStatus = null;
        protected System.Int32? _DataImportStatusLabelID = null;

        protected System.String _MatchingSourceTypeName = null;
        protected System.Int32? _MatchingSourceTypeNameLabelID = null;

        protected System.String _IsPartofMatchSet = null;
        protected System.Int32? _IsPartofMatchSetLabelID = null;
        protected System.Int64? _MatchSetID = null;

        protected System.Int32? _DataImportTypeLabelID = null;
        protected System.Boolean? _IsHidden = null;


        private List<MatchingSourceAccountInfo> _MatchingSourceAccountList = null;
        private List<MatchingSourceColumnInfo> _MatchingSourceColumnList = null;
        private MatchingSourceDataInfo _MatchingSourceData = null;
        private MatchSetMatchingSourceDataImportInfo _MatchSetMatchingSourceSubSetData = null;


        [XmlElement(ElementName = "MatchingSourceAccountList")]
        public virtual List<MatchingSourceAccountInfo> MatchingSourceAccountList
        {
            get
            {
                return this._MatchingSourceAccountList;
            }
            set
            {
                this._MatchingSourceAccountList = value;
            }
        }


        [XmlElement(ElementName = "MatchingSourceColumnList")]
        public virtual List<MatchingSourceColumnInfo> MatchingSourceColumnList
        {
            get
            {
                return this._MatchingSourceColumnList;
            }
            set
            {
                this._MatchingSourceColumnList = value;
            }
        }


        [XmlElement(ElementName = "MatchingSourceData")]
        public virtual MatchingSourceDataInfo MatchingSourceData
        {
            get
            {
                return this._MatchingSourceData;
            }
            set
            {
                this._MatchingSourceData = value;
            }
        }

        [XmlElement(ElementName = "MatchSetMatchingSourceSubSetData")]
        public virtual MatchSetMatchingSourceDataImportInfo MatchSetMatchingSourceSubSetData
        {
            get
            {
                return this._MatchSetMatchingSourceSubSetData;
            }
            set
            {
                this._MatchSetMatchingSourceSubSetData = value;
            }
        }

        [XmlElement(ElementName = "RecItemCreatedCount")]
        public virtual System.Int32? RecItemCreatedCount
        {
            get
            {
                return this._RecItemCreatedCount;
            }
            set
            {
                this._RecItemCreatedCount = value;
                this.IsRecItemCreatedCountNull = (_RecItemCreatedCount == null);
            }
        }


        [XmlElement(ElementName = "RecordsLeft")]
        public virtual System.Int32? RecordsLeft
        {
            get
            {
                return this._RecordsLeft;
            }
            set
            {
                this._RecordsLeft = value;
                this.IsRecordsLeftNull = (_RecordsLeft == null);
            }
        }





        [XmlElement(ElementName = "DataImportStatus")]
        public virtual System.String DataImportStatus
        {
            get
            {
                return this._DataImportStatus;
            }
            set
            {
                this._DataImportStatus = value;
            }
        }

        [XmlElement(ElementName = "DataImportStatusLabelID")]
        public virtual System.Int32? DataImportStatusLabelID
        {
            get
            {
                return this._DataImportStatusLabelID;
            }
            set
            {
                this._DataImportStatusLabelID = value;
            }
        }

        [XmlElement(ElementName = "MatchingSourceTypeName")]
        public virtual System.String MatchingSourceTypeName
        {
            get
            {
                return this._MatchingSourceTypeName;
            }
            set
            {
                this._MatchingSourceTypeName = value;

            }
        }

        [XmlElement(ElementName = "MatchingSourceTypeNameLabelID")]
        public virtual System.Int32? MatchingSourceTypeNameLabelID
        {
            get
            {
                return this._MatchingSourceTypeNameLabelID;
            }
            set
            {
                this._MatchingSourceTypeNameLabelID = value;

            }
        }

        [XmlElement(ElementName = "IsPartofMatchSetLabelID")]
        public virtual System.Int32? IsPartofMatchSetLabelID
        {
            get
            {
                return this._IsPartofMatchSetLabelID;
            }
            set
            {
                this._IsPartofMatchSetLabelID = value;

            }
        }

        [XmlElement(ElementName = "IsPartofMatchSet")]
        public virtual System.String IsPartofMatchSet
        {
            get
            {
                return this._IsPartofMatchSet;
            }
            set
            {
                this._IsPartofMatchSet = value;

            }
        }
        [XmlElement(ElementName = "MatchSetID")]
        public virtual System.Int64? MatchSetID
        {
            get
            {
                return this._MatchSetID;
            }
            set
            {
                this._MatchSetID = value;
            }
        }
        [XmlElement(ElementName = "DataImportTypeLabelID")]
        public virtual System.Int32? DataImportTypeLabelID
        {
            get
            {
                return this._DataImportTypeLabelID;
            }
            set
            {
                this._DataImportTypeLabelID = value;
            }
        }

        [XmlElement(ElementName = "IsHidden")]
        public virtual System.Boolean? IsHidden
        {
            get
            {
                return this._IsHidden;
            }
            set
            {
                this._IsHidden = value;

            }
        }
		

	}
}
