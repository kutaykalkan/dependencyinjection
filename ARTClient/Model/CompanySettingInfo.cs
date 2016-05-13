
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace SkyStem.ART.Client.Model
{

    /// <summary>
    /// An object representation of the SkyStemArt CompanySetting table
    /// </summary>
    [Serializable]
    [DataContract]
    public class CompanySettingInfo : CompanySettingInfoBase
    {
        //TODO: Move it to CompanyMaterialityTypeInfo to be generated from codus
        protected System.Boolean? _IsCarryForwardedFromPreviousRecPeriod = false;
        public bool IsIsCarryForwardedFromPreviousRecPeriodNull = true;
        protected System.Int32? _CurrentFinancialYearID = 0;
        public bool IsCurrentFinancialYearIDNull = true;

        [XmlElement(ElementName = "CurrentFinancialYearID")]
        public virtual System.Int32? CurrentFinancialYearID
        {
            get
            {
                return this._CurrentFinancialYearID;
            }
            set
            {
                this._CurrentFinancialYearID = value;

                this.IsCurrentFinancialYearIDNull = false;
            }
        }

        [XmlElement(ElementName = "IsCarryForwardedFromPreviousRecPeriod")]
        public virtual System.Boolean? IsCarryForwardedFromPreviousRecPeriod
        {
            get
            {
                return this._IsCarryForwardedFromPreviousRecPeriod;
            }
            set
            {
                this._IsCarryForwardedFromPreviousRecPeriod = value;

                this.IsIsCarryForwardedFromPreviousRecPeriodNull = false;
            }
        }

        private System.Decimal? _CurrentUsage = 0.00M;

        [XmlElement(ElementName = "CurrentUsageDataStorageCapacity")]
        public System.Decimal? CurrentUsage
        {
            get
            {
                return _CurrentUsage;
            }
            set
            {
                _CurrentUsage = value;
            }
        }

        private System.Int16? _DualLevelReviewTypeID = 0;
        [XmlElement(ElementName = "DualLevelReviewTypeID")]
        public virtual System.Int16? DualLevelReviewTypeID
        {
            get
            {
                return this._DualLevelReviewTypeID;
            }
            set
            {
                this._DualLevelReviewTypeID = value;
            }
        }

        private System.Int16? _DayTypeID = null;
        [XmlElement(ElementName = "DayTypeID")]
        public virtual System.Int16? DayTypeID
        {
            get
            {
                return this._DayTypeID;
            }
            set
            {
                this._DayTypeID = value;
            }
        }

        [XmlElement(ElementName = "RCCValidationTypeID")]
        public System.Int16? RCCValidationTypeID { get; set; }
        [XmlElement(ElementName = "IsRCCValidationCarryForwardedFromPreviousRecPeriod")]
        public System.Boolean? IsRCCValidationCarryForwardedFromPreviousRecPeriod { get; set; }

    }//end of class
}//end of namespace
