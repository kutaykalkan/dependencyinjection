

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemART CompanyJEWriteOffOnApprover table
    /// </summary>
    [Serializable]
    public abstract class CompanyJEWriteOffOnApproverInfoBase
    {

        protected System.Int32? _CompanyJEWriteOffOnApproverID = null;
        protected System.Int32? _CompanyRecPeriodSetID = null;
        protected System.Decimal? _FromAmount = null;
        protected System.Int32? _PrimaryApproverUserID = null;
        protected System.Int32? _SecondaryApproverUserID = null;
        protected System.Decimal? _ToAmount = null;


        [XmlElement(ElementName = "CompanyJEWriteOffOnApproverID")]
        public virtual System.Int32? CompanyJEWriteOffOnApproverID
        {
            get
            {
                return this._CompanyJEWriteOffOnApproverID;
            }
            set
            {
                this._CompanyJEWriteOffOnApproverID = value;

            }
        }

        [XmlElement(ElementName = "CompanyRecPeriodSetID")]
        public virtual System.Int32? CompanyRecPeriodSetID
        {
            get
            {
                return this._CompanyRecPeriodSetID;
            }
            set
            {
                this._CompanyRecPeriodSetID = value;

            }
        }

        [XmlElement(ElementName = "FromAmount")]
        public virtual System.Decimal? FromAmount
        {
            get
            {
                return this._FromAmount;
            }
            set
            {
                this._FromAmount = value;

            }
        }

        [XmlElement(ElementName = "PrimaryApproverUserID")]
        public virtual System.Int32? PrimaryApproverUserID
        {
            get
            {
                return this._PrimaryApproverUserID;
            }
            set
            {
                this._PrimaryApproverUserID = value;

            }
        }

        [XmlElement(ElementName = "SecondaryApproverUserID")]
        public virtual System.Int32? SecondaryApproverUserID
        {
            get
            {
                return this._SecondaryApproverUserID;
            }
            set
            {
                this._SecondaryApproverUserID = value;

            }
        }

        [XmlElement(ElementName = "ToAmount")]
        public virtual System.Decimal? ToAmount
        {
            get
            {
                return this._ToAmount;
            }
            set
            {
                this._ToAmount = value;

            }
        }


        /// <summary>
        /// Returns a string representation of the object, displaying all property and field names and values.
        /// </summary>
        public override string ToString()
        {
            return StringUtil.ToString(this);
        }




    }
}
