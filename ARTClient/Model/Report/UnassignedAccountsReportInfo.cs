using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;

namespace SkyStem.ART.Client.Model.Report
{

    /// <summary>
    /// An object representation of the SkyStemArt AccountHdr table
    /// </summary>
    [Serializable]
    public class UnassignedAccountsReportInfo : OrganizationalHierarchyInfo
    {
        protected System.Int64? _AccountID = null;
        protected System.String _AccountName = "";
        protected System.Int32? _AccountNameLabelID = null;
        protected System.String _AccountNumber = "";
        protected System.Int16? _AccountTypeID = null;

        protected System.Int32? _FSCaptionID = null;

        protected System.Boolean? _IsPreparerSet = null ;
        protected System.Boolean? _IsReviewerSet = null;
        protected System.Boolean? _IsApproverSet = null;
        protected System.Boolean? _IsNetAccount = null;

        [XmlElement(ElementName = "IsPreparerSet")]
        public virtual System.Boolean? IsPreparerSet
        {
            get
            {
                return this._IsPreparerSet;
            }
            set
            {
                this._IsPreparerSet = value;
            }
        }
        [XmlElement(ElementName = "IsReviewerSet")]
        public virtual System.Boolean? IsReviewerSet
        {
            get
            {
                return this._IsReviewerSet;
            }
            set
            {
                this._IsReviewerSet = value;
            }
        }

        [XmlElement(ElementName = "IsApproverSet")]
        public virtual System.Boolean? IsApproverSet
        {
            get
            {
                return this._IsApproverSet;
            }
            set
            {
                this._IsApproverSet = value;
            }
        }

        [XmlElement(ElementName = "IsNetAccount")]
        public virtual System.Boolean? IsNetAccount
        {
            get
            {
                return this._IsNetAccount;
            }
            set
            {
                this._IsNetAccount = value;
            }
        }



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
            }
        }

        [XmlElement(ElementName = "AccountNameLabelID")]
        public virtual System.Int32? AccountNameLabelID
        {
            get
            {
                return this._AccountNameLabelID;
            }
            set
            {
                this._AccountNameLabelID = value;
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
            }
        }


        [XmlElement(ElementName = "AccountTypeID")]
        public virtual System.Int16? AccountTypeID
        {
            get
            {
                return this._AccountTypeID;
            }
            set
            {
                this._AccountTypeID = value;
            }
        }


        [XmlElement(ElementName = "FSCaptionID")]
        public virtual System.Int32? FSCaptionID
        {
            get
            {
                return this._FSCaptionID;
            }
            set
            {
                this._FSCaptionID = value;
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
