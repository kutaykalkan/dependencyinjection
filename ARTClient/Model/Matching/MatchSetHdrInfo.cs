
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using SkyStem.ART.Client.Model.Matching.Base;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace SkyStem.ART.Client.Model.Matching
{    

	/// <summary>
	/// An object representation of the SkyStemART MatchSetHdr table
	/// </summary>
    [Serializable]
    [DataContract]
    public class MatchSetHdrInfo : MatchSetHdrInfoBase
    {

        protected System.String _MatchingType = null;
        protected System.String _MatchingStatus = null;

        protected System.Int64? _AccountID = null;
        protected System.String _AccountNumber = null;
        protected System.Int32? _AcountNameLabelID = null;
        protected System.String _AccountName = null;
        protected System.Int32? _PreparerUserID = null;
        protected System.Int32? _BackupPreparerUserID = null;


        protected System.String _MatchSetReferenceNumber = null;

        protected List<MatchingSourceDataImportHdrInfo> _MatchingSourceDataImportHdrInfoList = null;
        protected List<MatchSetSubSetCombinationInfo> _MatchSetSubSetCombinationInfoCollection = null;
        protected List<RecItemColumnMstInfo> _RecItemColumnMstInfoCollection = null;

        public bool IsMatchingTypeNull = true;
        public bool IsMatchingStatusNull = true;

        public bool IsMatchSetReferenceNumberNull = true;
        public bool IsMatchSetSubSetCombinationInfoCollectionNull = true;
        public bool IsRecItemColumnMstInfoCollectionNull = true;

        public bool IsMatchingSourceDataInfoCollectionNull = true;
        public bool IsMatchSetMatchingSourceDataImportInfoCollectionNull = true;
        public bool IsMatchingSourceColumnInfoCollectionNull = true;

        public bool IsMatchingConfigurationInfoCollectionNull = true;
        public bool IsMatchingConfigurationRuleInfoCollectionNull = true;
        public bool IsAccountIDNull = true;
        public bool IsAccountNumberNull = true;
        public bool IsAccountNameLabelIDNull = true;

        public bool IsAccountNameNull = true;
        



        [XmlElement(ElementName = "AccountID")]
        public virtual System.Int64? AccountID
        {
            get
            {
                return this._AccountID;
            }
            set
            {
                this._AccountID = value;

                this.IsAccountIDNull = (_AccountID == null);
            }
        }

        [XmlElement(ElementName = "AccountNumber")]
        public virtual System.String AccountNumber
        {
            get
            {
                return this._AccountNumber;
            }
            set
            {
                this._AccountNumber = value;

                this.IsAccountNumberNull = (_AccountNumber == null);
            }
        }

        [XmlElement(ElementName = "AcountNameLabelID")]
        public virtual System.Int32? AcountNameLabelID
        {
            get
            {
                return this._AcountNameLabelID;
            }
            set
            {
                this._AcountNameLabelID = value;

                this.IsAccountNameLabelIDNull = (_AcountNameLabelID == null);
            }
        }


        
        [XmlElement(ElementName = "AccountName")]
        public virtual System.String AccountName
        {
            get
            {
                return this._AccountName;
            }
            set
            {
                this._AccountName = value;
                this.IsAccountNameNull = (_AccountName == null);
            }
        }




        [XmlElement(ElementName = "MatchingType")]
        public virtual System.String MatchingType
        {
            get
            {
                return this._MatchingType;
            }
            set
            {
                this._MatchingType = value;

                this.IsMatchingTypeNull = (_MatchingType == null);
            }
        }

        [XmlElement(ElementName = "MatchingStatus")]
        public virtual System.String MatchingStatus
        {
            get
            {
                return this._MatchingStatus;
            }
            set
            {
                this._MatchingStatus = value;

                this.IsMatchingStatusNull = (_MatchingStatus == null);
            }
        }


        [XmlElement(ElementName = "MatchingSourceDataImportHdrInfoList")]
        public virtual List<MatchingSourceDataImportHdrInfo> MatchingSourceDataImportHdrInfoList
        {
            get
            {
                return _MatchingSourceDataImportHdrInfoList;
            }
            set
            {
                this._MatchingSourceDataImportHdrInfoList = value;
            }

        }

        [XmlElement(ElementName = "MatchSetReferenceNumber")]
        public virtual System.String MatchSetReferenceNumber
        {
            get
            {
                return this._MatchSetReferenceNumber;
            }
            set
            {
                this._MatchSetReferenceNumber = value;

                this.IsMatchSetReferenceNumberNull = (_MatchSetReferenceNumber == null);
            }
        }



        [XmlElement(ElementName = "MatchSetSubSetCombinationInfoCollection")]
        public virtual List<MatchSetSubSetCombinationInfo> MatchSetSubSetCombinationInfoCollection
        {
            get
            {
                return this._MatchSetSubSetCombinationInfoCollection;
            }
            set
            {
                this._MatchSetSubSetCombinationInfoCollection = value;

                this.IsRecItemColumnMstInfoCollectionNull = (_MatchSetSubSetCombinationInfoCollection == null);
            }
        }

        [XmlElement(ElementName = "RecItemColumnMstInfoCollection")]
        public virtual List<RecItemColumnMstInfo> RecItemColumnMstInfoCollection
        {
            get
            {
                return this._RecItemColumnMstInfoCollection;
            }
            set
            {
                this._RecItemColumnMstInfoCollection = value;

                this.IsRecItemColumnMstInfoCollectionNull = (_RecItemColumnMstInfoCollection == null);
            }
        }

        public bool IsFirstNameNull = true;
        protected System.String _FirstName = null;

        [XmlElement(ElementName = "FirstName")]
        public virtual System.String FirstName
        {
            get
            {
                return this._FirstName;
            }
            set
            {
                this._FirstName = value;

                this.IsFirstNameNull = (_FirstName == null);
            }
        }

        public bool IsLastNameNull = true;
        protected System.String _LastName = null;

        [XmlElement(ElementName = "LastName")]
        public virtual System.String LastName
        {
            get
            {
                return this._LastName;
            }
            set
            {
                this._LastName = value;

                this.IsLastNameNull = (_LastName == null);
            }
        }
        protected System.String _Message = null;
        [XmlElement(ElementName = "Message")]
        public virtual System.String Message
        {
            get
            {
                return this._Message;
            }
            set
            {
                this._Message = value;

            }
        }

        [XmlElement(ElementName = "BackupPreparerUserID")]
        public virtual System.Int32? BackupPreparerUserID
        {
            get
            {
                return this._BackupPreparerUserID;
            }
            set
            {
                this._BackupPreparerUserID = value;

            }
        }

        [XmlElement(ElementName = "PreparerUserID")]
        public virtual System.Int32? PreparerUserID
        {
            get
            {
                return this._PreparerUserID;
            }
            set
            {
                this._PreparerUserID = value;
            }
        }
    }
}
