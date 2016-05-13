

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemArt AccountHdr table
    /// </summary>
    [Serializable]
    public abstract class AccountHdrInfoBase : OrganizationalHierarchyInfo
    {
        protected System.Int64? _AccountID = 0;
        protected System.String _AccountName = "";
        protected System.Int32? _AccountNameLabelID = 0;
        protected System.String _AccountNumber = "";
        protected System.String _AccountPolicyUrl = "";
        protected System.Int32? _AccountPolicyUrlLabelID = null;
        protected System.Int16? _AccountTypeID = 0;
        protected System.String _AddedBy = "";
        protected System.Int32? _ApproverUserID = null;
        protected System.Int32? _BackupApproverUserID = null;
        protected System.DateTime? _DateAdded = DateTime.Now;
        protected System.DateTime? _DateRevised = DateTime.Now;
        protected System.String _Description = "";
        protected System.Int32? _DescriptionLabelID = 0;
        protected System.Int32? _FSCaptionID = 0;
        protected System.Int32? _GeographyObjectID = 0;
        protected System.String _HostName = "";
        protected System.Boolean? _IsActive = false;
        protected System.Boolean? _IsKeyAccount = null;
        protected System.Boolean? _IsZeroBalance = null;
        protected System.String _NatureOfAccount = "";
        protected System.Int32? _NatureOfAccountLabelID = 0;
        protected System.Int32? _NetAccountID = 0;
        protected System.Int32? _PreparerUserID = null;
        protected System.String _ReconciliationProcedure = "";
        protected System.Int32? _ReconciliationProcedureLabelID = 0;
        protected System.Int16? _ReconciliationTemplateID = 0;
        protected System.Int32? _ReviewerUserID = null;
        protected System.Int32? _BackupReviewerUserID = null;
        protected System.String _RevisedBy = "";
        protected System.Int16? _RiskRatingID = 0;
        protected System.Int32? _SubLedgerSourceID = 0;
        public bool IsAccountIDNull = true;
        public bool IsAccountNameNull = true;
        public bool IsAccountNameLabelIDNull = true;
        public bool IsAccountNumberNull = true;
        public bool IsAccountPolicyUrlNull = true;
        public bool IsAccountTypeIDNull = true;
        public bool IsAddedByNull = true;
        public bool IsApproverUserIDNull = true;
        public bool IsBackupApproverUserIDNull = true;
        public bool IsDateAddedNull = true;
        public bool IsDateRevisedNull = true;
        public bool IsDescriptionNull = true;
        public bool IsDescriptionLabelIDNull = true;
        public bool IsFSCaptionIDNull = true;
        public bool IsGeographyObjectIDNull = true;
        public bool IsHostNameNull = true;
        public bool IsIsActiveNull = true;
        public bool IsIsKeyAccountNull = true;
        public bool IsIsZeroBalanceNull = true;
        public bool IsNatureOfAccountNull = true;
        public bool IsNatureOfAccountLabelIDNull = true;
        public bool IsNetAccountIDNull = true;
        public bool IsPreparerUserIDNull = true;
        public bool IsBackupPreparerUserIDNull = true;
        public bool IsReconciliationProcedureNull = true;
        public bool IsReconciliationProcedureLabelIDNull = true;
        public bool IsReconciliationTemplateIDNull = true;
        public bool IsReviewerUserIDNull = true;
        public bool IsBackupReviewerUserIDNull = true;
        public bool IsRevisedByNull = true;
        public bool IsRiskRatingIDNull = true;
        public bool IsSubLedgerSourceIDNull = true;

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

                this.IsAccountIDNull = false;
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

                this.IsAccountNameLabelIDNull = false;
            }
        }

        protected System.Int32? _KeySize = 0;
        [XmlElement(ElementName = "KeySize")]
        public virtual System.Int32? KeySize
        {
            get
            {
                return this._KeySize;
            }
            set
            {
                this._KeySize = value;

                
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

                this.IsAccountNumberNull = false;
            }
        }

        [XmlElement(ElementName = "AccountPolicyUrl")]
        public virtual System.String AccountPolicyUrl
        {
            get
            {
                return this._AccountPolicyUrl;
            }
            set
            {
                this._AccountPolicyUrl = value;

                this.IsAccountPolicyUrlNull = (_AccountPolicyUrl == null);
            }
        }

        [XmlElement(ElementName = "AccountPolicyUrlLabelID")]
        public virtual System.Int32? AccountPolicyUrlLabelID
        {
            get
            {
                return this._AccountPolicyUrlLabelID;
            }
            set
            {
                this._AccountPolicyUrlLabelID = value;
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

                this.IsAccountTypeIDNull = false;
            }
        }

        [XmlElement(ElementName = "AddedBy")]
        public virtual System.String AddedBy
        {
            get
            {
                return this._AddedBy;
            }
            set
            {
                this._AddedBy = value;

                this.IsAddedByNull = (_AddedBy == null);
            }
        }

        [XmlElement(ElementName = "ApproverUserID")]
        public virtual System.Int32? ApproverUserID
        {
            get
            {
                return this._ApproverUserID;
            }
            set
            {
                this._ApproverUserID = value;

                this.IsApproverUserIDNull = false;
            }
        }

        [XmlElement(ElementName = "BackupApproverUserID")]
        public virtual System.Int32? BackupApproverUserID
        {
            get
            {
                return this._BackupApproverUserID;
            }
            set
            {
                this._BackupApproverUserID = value;

                this.IsBackupApproverUserIDNull = false;
            }
        }

        [XmlElement(ElementName = "DateAdded")]
        public virtual System.DateTime? DateAdded
        {
            get
            {
                return this._DateAdded;
            }
            set
            {
                this._DateAdded = value;

                this.IsDateAddedNull = (_DateAdded == DateTime.MinValue);
            }
        }

        [XmlElement(ElementName = "DateRevised")]
        public virtual System.DateTime? DateRevised
        {
            get
            {
                return this._DateRevised;
            }
            set
            {
                this._DateRevised = value;

                this.IsDateRevisedNull = (_DateRevised == DateTime.MinValue);
            }
        }

        [XmlElement(ElementName = "Description")]
        public virtual System.String Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this._Description = value;

                this.IsDescriptionNull = (_Description == null);
            }
        }

        [XmlElement(ElementName = "DescriptionLabelID")]
        public virtual System.Int32? DescriptionLabelID
        {
            get
            {
                return this._DescriptionLabelID;
            }
            set
            {
                this._DescriptionLabelID = value;

                this.IsDescriptionLabelIDNull = false;
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

                this.IsFSCaptionIDNull = false;
            }
        }

        [XmlElement(ElementName = "GeographyObjectID")]
        public virtual System.Int32? GeographyObjectID
        {
            get
            {
                return this._GeographyObjectID;
            }
            set
            {
                this._GeographyObjectID = value;

                this.IsGeographyObjectIDNull = false;
            }
        }

        [XmlElement(ElementName = "HostName")]
        public virtual System.String HostName
        {
            get
            {
                return this._HostName;
            }
            set
            {
                this._HostName = value;

                this.IsHostNameNull = (_HostName == null);
            }
        }

        [XmlElement(ElementName = "IsActive")]
        public virtual System.Boolean? IsActive
        {
            get
            {
                return this._IsActive;
            }
            set
            {
                this._IsActive = value;

                this.IsIsActiveNull = false;
            }
        }

        [XmlElement(ElementName = "IsKeyAccount")]
        public virtual System.Boolean? IsKeyAccount
        {
            get
            {
                return this._IsKeyAccount;
            }
            set
            {
                this._IsKeyAccount = value;

                this.IsIsKeyAccountNull = false;
            }
        }

        [XmlElement(ElementName = "IsZeroBalance")]
        public virtual System.Boolean? IsZeroBalance
        {
            get
            {
                return this._IsZeroBalance;
            }
            set
            {
                this._IsZeroBalance = value;

                this.IsIsZeroBalanceNull = false;
            }
        }

        [XmlElement(ElementName = "NatureOfAccount")]
        public virtual System.String NatureOfAccount
        {
            get
            {
                return this._NatureOfAccount;
            }
            set
            {
                this._NatureOfAccount = value;

                this.IsNatureOfAccountNull = (_NatureOfAccount == null);
            }
        }

        [XmlElement(ElementName = "NatureOfAccountLabelID")]
        public virtual System.Int32? NatureOfAccountLabelID
        {
            get
            {
                return this._NatureOfAccountLabelID;
            }
            set
            {
                this._NatureOfAccountLabelID = value;

                this.IsNatureOfAccountLabelIDNull = false;
            }
        }

        [XmlElement(ElementName = "NetAccountID")]
        public virtual System.Int32? NetAccountID
        {
            get
            {
                return this._NetAccountID;
            }
            set
            {
                this._NetAccountID = value;

                this.IsNetAccountIDNull = false;
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

                this.IsPreparerUserIDNull = false;
            }
        }

        [XmlElement(ElementName = "ReconciliationProcedure")]
        public virtual System.String ReconciliationProcedure
        {
            get
            {
                return this._ReconciliationProcedure;
            }
            set
            {
                this._ReconciliationProcedure = value;

                this.IsReconciliationProcedureNull = (_ReconciliationProcedure == null);
            }
        }

        [XmlElement(ElementName = "ReconciliationProcedureLabelID")]
        public virtual System.Int32? ReconciliationProcedureLabelID
        {
            get
            {
                return this._ReconciliationProcedureLabelID;
            }
            set
            {
                this._ReconciliationProcedureLabelID = value;

                this.IsReconciliationProcedureLabelIDNull = false;
            }
        }

        [XmlElement(ElementName = "ReconciliationTemplateID")]
        public virtual System.Int16? ReconciliationTemplateID
        {
            get
            {
                return this._ReconciliationTemplateID;
            }
            set
            {
                this._ReconciliationTemplateID = value;

                this.IsReconciliationTemplateIDNull = false;
            }
        }

        [XmlElement(ElementName = "ReviewerUserID")]
        public virtual System.Int32? ReviewerUserID
        {
            get
            {
                return this._ReviewerUserID;
            }
            set
            {
                this._ReviewerUserID = value;

                this.IsReviewerUserIDNull = false;
            }
        }

        [XmlElement(ElementName = "BackupReviewerUserID")]
        public virtual System.Int32? BackupReviewerUserID
        {
            get
            {
                return this._BackupReviewerUserID;
            }
            set
            {
                this._BackupReviewerUserID = value;

                this.IsBackupReviewerUserIDNull = false;
            }
        }

        [XmlElement(ElementName = "RevisedBy")]
        public virtual System.String RevisedBy
        {
            get
            {
                return this._RevisedBy;
            }
            set
            {
                this._RevisedBy = value;

                this.IsRevisedByNull = (_RevisedBy == null);
            }
        }

        [XmlElement(ElementName = "RiskRatingID")]
        public virtual System.Int16? RiskRatingID
        {
            get
            {
                return this._RiskRatingID;
            }
            set
            {
                this._RiskRatingID = value;

                this.IsRiskRatingIDNull = false;
            }
        }

        [XmlElement(ElementName = "SubLedgerSourceID")]
        public virtual System.Int32? SubLedgerSourceID
        {
            get
            {
                return this._SubLedgerSourceID;
            }
            set
            {
                this._SubLedgerSourceID = value;

                this.IsSubLedgerSourceIDNull = false;
            }
        }

        string _SubLedgerSource = string.Empty;
        [XmlElement(ElementName = "SubLedgerSource")]
        public virtual System.String SubLedgerSource
        {
            get
            {
                return this._SubLedgerSource;
            }
            set
            {
                this._SubLedgerSource = value;
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
