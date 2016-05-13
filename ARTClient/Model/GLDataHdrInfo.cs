
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using SkyStem.ART.Client.Model.QualityScore;
using System.Collections.Generic;
using SkyStem.ART.Client.Model.RecControlCheckList;

namespace SkyStem.ART.Client.Model
{

    /// <summary>
    /// An object representation of the SkyStemArt GLDataHdr table
    /// </summary>
    [Serializable]
    [DataContract]
    //TODO: change MapObject()  method to accomodate new fields
    public class GLDataHdrInfo : GLDataHdrInfoBase
    {

        # region From AccountHdrInfoBase

        protected System.String _AccountName = "";
        protected System.Int32? _AccountNameLabelID = 0;
        protected System.String _AccountNumber = "";
        protected System.Int16? _ReconciliationTemplateID = 0;

        public bool IsAccountNameNull = true;

        public bool IsAccountNameLabelIDNull = true;

        public bool IsAccountNumberNull = true;

        public bool IsReconciliationTemplateIDNull = true;
        protected System.Boolean? _IsLocked = false;



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

                this.IsAccountNameLabelIDNull = (this._AccountNameLabelID == null);
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

                this.IsAccountNumberNull = (this._AccountNumber == null);
            }
        }
        [XmlElement(ElementName = "IsLocked")]
        public virtual System.Boolean? IsLocked
        {
            get
            {
                return this._IsLocked;
            }
            set
            {
                this._IsLocked = value;
            }
        }

        [XmlElement(ElementName = "IsEditable")]
        public virtual System.Boolean? IsEditable { get; set; }

        [XmlElement(ElementName = "IsRCCValidation")]
        public virtual System.Boolean? IsRCCValidation { get; set; }

        #endregion

        #region CertificationStatusMst, ReconciliationStatusMst

        protected System.String _CertificationStatus = "";
        protected System.Int32? _CertificationStatusLabelID = 0;
        protected System.String _ReconciliationStatus = "";
        protected System.Int32? _ReconciliationStatusLabelID = 0;

        public bool IsCertificationStatusNull = true;
        public bool IsCertificationStatusLabelIDNull = true;
        public bool IsReconciliationStatusNull = true;
        public bool IsReconciliationStatusLabelIDNull = true;


        [XmlElement(ElementName = "CertificationStatus")]
        public virtual System.String CertificationStatus
        {
            get
            {
                return this._CertificationStatus;
            }
            set
            {
                this._CertificationStatus = value;

                this.IsCertificationStatusNull = (_CertificationStatus == null);
            }
        }

        [XmlElement(ElementName = "CertificationStatusLabelID")]
        public virtual System.Int32? CertificationStatusLabelID
        {
            get
            {
                return this._CertificationStatusLabelID;
            }
            set
            {
                this._CertificationStatusLabelID = value;

                this.IsCertificationStatusLabelIDNull = (this._CertificationStatusLabelID == null);
            }
        }


        [XmlElement(ElementName = "ReconciliationStatus")]
        public virtual System.String ReconciliationStatus
        {
            get
            {
                return this._ReconciliationStatus;
            }
            set
            {
                this._ReconciliationStatus = value;

                this.IsReconciliationStatusNull = (_ReconciliationStatus == null);
            }
        }

        [XmlElement(ElementName = "ReconciliationStatusLabelID")]
        public virtual System.Int32? ReconciliationStatusLabelID
        {
            get
            {
                return this._ReconciliationStatusLabelID;
            }
            set
            {
                this._ReconciliationStatusLabelID = value;

                this.IsReconciliationStatusLabelIDNull = (this._ReconciliationStatusLabelID == null);
            }
        }
        #endregion

        # region From ReconciliationPeriodInfo
        //protected System.DateTime? _DueDate = DateTime.Now;// perhaps will be filled based on whether we need to show rec due date por cert due date

        //public bool IsDueDateNull = true;

        //[XmlElement(ElementName = "CertificationStartDate")]
        //public virtual System.DateTime? DueDate
        //{
        //    get
        //    {
        //        return this._DueDate;
        //    }
        //    set
        //    {
        //        this._DueDate = value;

        //        this.IsDueDateNull = (_DueDate == DateTime.MinValue);
        //    }
        //}
        protected System.String _ReportingCurrencyCode = "";
        public bool IsReportingCurrencyCodeNull = true;
        [XmlElement(ElementName = "ReportingCurrencyCode")]
        public virtual System.String ReportingCurrencyCode
        {
            get
            {
                return this._ReportingCurrencyCode;
            }
            set
            {
                this._ReportingCurrencyCode = value;

                this.IsReportingCurrencyCodeNull = (_ReportingCurrencyCode == null);
            }
        }


        #endregion

        #region For SRA screen
        //from accountHdr
        protected System.Int16? _RiskRatingID = 0;
        protected System.Boolean? _IsKeyAccount = false;
        protected System.Boolean? _IsZeroBalance = false;

        protected System.Boolean? _IsNetAccount = false;
        protected System.DateTime? _DateApproved = DateTime.Now;


        public bool IsRiskRatingIDNull = true;
        public bool IsKeyAccountNull = true;
        public bool IsZeroBalanceNull = true;
        public bool IsNetAccountNull = true;
        public bool IsDateApprovedeNull = true;

        public System.String _SystemReconciliationRuleNumber = string.Empty;
        public System.Int32? _SystemReconciliationRuleLabelID = null;

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

                this.IsRiskRatingIDNull = (this._RiskRatingID == null);
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

                this.IsKeyAccountNull = (this._KeyAccount == null);
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

                this.IsZeroBalanceNull = (this._ZeroBalance == null);
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

                this.IsNetAccountNull = (this._IsNetAccount == null);
            }
        }
        [XmlElement(ElementName = "DateApproved")]
        public virtual System.DateTime? DateApproved
        {
            get
            {
                return this._DateApproved;
            }
            set
            {
                this._DateApproved = value;
                this.IsDateApprovedeNull = (_DateApproved == DateTime.MinValue);
            }
        }
        [XmlElement(ElementName = "_SystemReconciliationRuleNumber")]
        public virtual System.String SystemReconciliationRuleNumber
        {
            get
            {
                return this._SystemReconciliationRuleNumber;
            }
            set
            {
                this._SystemReconciliationRuleNumber = value;
            }
        }
        [XmlElement(ElementName = "SystemReconciliationRuleLabelID")]
        public virtual System.Int32? SystemReconciliationRuleLabelID
        {
            get
            {
                return this._SystemReconciliationRuleLabelID;
            }
            set
            {
                this._SystemReconciliationRuleLabelID = value;
            }
        }
        #endregion

        #region For reporting

        protected System.Int16? _AccountTypeID = 0;
        public bool IsAccountTypeIDNull = true;
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
                this.IsAccountTypeIDNull = (_AccountTypeID == null);
            }
        }

        protected System.String _Reason = "";
        public bool IsReasonNull = true;
        [XmlElement(ElementName = "Reason")]
        public virtual System.String Reason
        {
            get
            {
                return this._Reason;
            }
            set
            {
                this._Reason = value;

                this.IsReasonNull = (_Reason == null);
            }
        }

        protected System.String _PreparerFullName = "";
        public bool IsPreparerFullNameNull = true;
        [XmlElement(ElementName = "PreparerFullName")]
        public virtual System.String PreparerFullName
        {
            get
            {
                return this._PreparerFullName;
            }
            set
            {
                this._PreparerFullName = value;

                this.IsPreparerFullNameNull = (_PreparerFullName == null);
            }
        }


        protected System.String _BackupPreparerFullName = "";
        public bool IsBackupPreparerFullNameNull = true;
        [XmlElement(ElementName = "BackupPreparerFullName")]
        public virtual System.String BackupPreparerFullName
        {
            get
            {
                return this._BackupPreparerFullName;
            }
            set
            {
                this._BackupPreparerFullName = value;

                this.IsBackupPreparerFullNameNull = (_BackupPreparerFullName == null);
            }
        }

        protected System.String _NetAccount = "";
        protected System.Int32? _NetAccountLabelID = 0;
        protected System.Int32? _PreparerUserID = 0;
        protected System.Int32? _ReviewerUserID = 0;
        protected System.Int32? _ApproverUserID = 0;
        protected System.String _ReviewerFullName = "";
        protected System.String _ApproverFullName = "";
        protected System.Boolean? _Materiality = false;
        protected System.String _AccountMateriality = "";
        protected System.String _RiskRating = "";
        protected System.String _KeyAccount = "";
        protected System.String _ZeroBalance = "";
        protected System.String _SystemReconciled = "";

        protected System.Int32? _BackupPreparerUserID = 0;
        protected System.Int32? _BackupReviewerUserID = 0;
        protected System.Int32? _BackupApproverUserID = 0;
        protected System.String _BackupReviewerFullName = "";
        protected System.String _BackupApproverFullName = "";

        public bool IsNetAccountNameNull = true;

        public bool IsNetAccountLabelIDNull = true;

        public bool IsApproverUserIDNull = true;
        public bool IsBackupApproverUserIDNull = true;

        public bool IsPreparerUserIDNull = true;
        public bool IsReviewerUserIDNull = true;

        public bool IsBackupPreparerUserIDNull = true;
        public bool IsBackupReviewerUserIDNull = true;

        public bool IsMaterialityNull = true;

        public bool IsMaterialityValueNull = true;

        public bool IsRiskRatingNull = true;

        public bool IsKeyAccountValueNull = true;

        public bool IsZeroBalanceValueNull = true;

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

                this.IsApproverUserIDNull = (this._ApproverUserID == null);
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

                this.IsBackupApproverUserIDNull = (this._BackupApproverUserID == null);
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

                this.IsReconciliationTemplateIDNull = (this._ReconciliationTemplateID == null);
            }
        }

        [XmlElement(ElementName = "NetAccount")]
        public virtual System.String NetAccount
        {
            get
            {
                return this._NetAccount;
            }
            set
            {
                this._NetAccount = value;

                this.IsNetAccountNameNull = (_NetAccount == null);
            }
        }

        [XmlElement(ElementName = "NetAccountLabelID")]
        public virtual System.Int32? NetAccountLabelID
        {
            get
            {
                return this._NetAccountLabelID;
            }
            set
            {
                this._NetAccountLabelID = value;

                this.IsNetAccountLabelIDNull = (this._NetAccountLabelID == null);
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

                this.IsPreparerUserIDNull = (this._PreparerUserID == null);
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

                this.IsBackupPreparerUserIDNull = (this._BackupPreparerUserID == null);
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

                this.IsReviewerUserIDNull = (this._ReviewerUserID == null);
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

                this.IsBackupReviewerUserIDNull = (this._BackupReviewerUserID == null);
            }
        }

        public bool IsApproverFullNameNull = true;
        [XmlElement(ElementName = "ApproverFullName")]
        public virtual System.String ApproverFullName
        {
            get
            {
                return this._ApproverFullName;
            }
            set
            {
                this._ApproverFullName = value;

                this.IsApproverFullNameNull = (_PreparerFullName == null);
            }
        }


        public bool IsBackupApproverFullNameNull = true;
        [XmlElement(ElementName = "BackupApproverFullName")]
        public virtual System.String BackupApproverFullName
        {
            get
            {
                return this._BackupApproverFullName;
            }
            set
            {
                this._BackupApproverFullName = value;

                this.IsBackupApproverFullNameNull = (_BackupApproverFullName == null);
            }
        }

        public bool IsReviewerFullNameNull = true;
        [XmlElement(ElementName = "ReviewerFullName")]
        public virtual System.String ReviewerFullName
        {
            get
            {
                return this._ReviewerFullName;
            }
            set
            {
                this._ReviewerFullName = value;

                this.IsReviewerFullNameNull = (_PreparerFullName == null);
            }
        }

        public bool IsBackupReviewerFullNameNull = true;
        [XmlElement(ElementName = "BackupReviewerFullName")]
        public virtual System.String BackupReviewerFullName
        {
            get
            {
                return this._BackupReviewerFullName;
            }
            set
            {
                this._BackupReviewerFullName = value;

                this.IsBackupReviewerFullNameNull = (_BackupReviewerFullName == null);
            }
        }


        [XmlElement(ElementName = "Materiality")]
        public virtual System.Boolean? Materiality
        {
            get
            {
                return this._Materiality;
            }
            set
            {
                this._Materiality = value;

                this.IsMaterialityNull = (_Materiality == null);
            }
        }

        [XmlElement(ElementName = "AccountMateriality")]
        public virtual System.String AccountMateriality
        {
            get
            {
                return this._AccountMateriality;
            }
            set
            {
                this._AccountMateriality = value;

                this.IsMaterialityValueNull = (_AccountMateriality == null);
            }
        }

        [XmlElement(ElementName = "RiskRating")]
        public virtual System.String RiskRating
        {
            get
            {
                return this._RiskRating;
            }
            set
            {
                this._RiskRating = value;

                this.IsRiskRatingNull = (_RiskRating == null);
            }
        }

        [XmlElement(ElementName = "KeyAccount")]
        public virtual System.String KeyAccount
        {
            get
            {
                return this._KeyAccount;
            }
            set
            {
                this._KeyAccount = value;

                this.IsKeyAccountValueNull = (_KeyAccount == null);
            }
        }

        [XmlElement(ElementName = "SystemReconciled")]
        public virtual System.String SystemReconciled
        {
            get
            {
                return this._SystemReconciled;
            }
            set
            {
                this._SystemReconciled = value;
            }
        }


        [XmlElement(ElementName = "ZeroBalance")]
        public virtual System.String ZeroBalance
        {
            get
            {
                return this._ZeroBalance;
            }
            set
            {
                this._ZeroBalance = value;

                this.IsZeroBalanceValueNull = (_ZeroBalance == null);
            }
        }

        protected System.Int32? _SubledgerSourceID = 0;
        public bool IsSubledgerSourceIDNull = true;
        [XmlElement(ElementName = "SubledgerSourceID")]
        public virtual System.Int32? SubledgerSourceID
        {
            get
            {
                return this._SubledgerSourceID;
            }
            set
            {
                this._SubledgerSourceID = value;
                this.IsSubledgerSourceIDNull = (_SubledgerSourceID == null);
            }
        }

        protected System.DateTime? _ReconciliationPeriodCloseDate;
        public bool IsReconciliationPeriodCloseDateNull = true;
        [XmlElement(ElementName = "ReconciliationPeriodCloseDate")]
        public virtual System.DateTime? ReconciliationPeriodCloseDate
        {
            get
            {
                return this._ReconciliationPeriodCloseDate;
            }
            set
            {
                this._ReconciliationPeriodCloseDate = value;
                this.IsReconciliationPeriodCloseDateNull = (_ReconciliationPeriodCloseDate == null);
            }
        }


        protected System.Int32? _NetAccountID = 0;
        public bool IsNetAccountIDNull = true;
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
                this.IsNetAccountIDNull = (_NetAccountID == null);
            }
        }

        private bool? _IsFlagged = false;
        public bool IsIsFlaggedNull = true;

        [XmlElement(ElementName = "IsFlagged")]
        public virtual bool? IsFlagged
        {
            get
            {
                return this._IsFlagged;
            }
            set
            {
                this._IsFlagged = value;
                this.IsIsFlaggedNull = (_IsFlagged == null);
            }
        }

        public bool IsVersionAvailableNull = true;
        protected System.Boolean? _IsVersionAvailable = false;

        [XmlElement(ElementName = "IsVersionAvailable")]
        public virtual System.Boolean? IsVersionAvailable
        {
            get
            {
                return this._IsVersionAvailable;
            }
            set
            {
                this._IsVersionAvailable = value;

                this.IsVersionAvailableNull = false;
            }
        }


        public bool IsSubledgerVersionAvailableNull = true;
        protected System.Boolean? _IsSubledgerVersionAvailable = false;

        [XmlElement(ElementName = "IsSubledgerVersionAvailable")]
        public virtual System.Boolean? IsSubledgerVersionAvailable
        {
            get
            {
                return this._IsSubledgerVersionAvailable;
            }
            set
            {
                this._IsSubledgerVersionAvailable = value;

                this.IsSubledgerVersionAvailableNull = false;
            }
        }

        #endregion

        #region For QualityScore

        [DataMember]
        [XmlElement(ElementName = "GLDataQualityScoreInfoList")]
        public List<GLDataQualityScoreInfo> GLDataQualityScoreInfoList { get; set; }

        #endregion

        #region For Backup Owners

        //[XmlElement(ElementName = "BackupPreparerUserID")]
        //[DataMember]
        //public virtual Int32? BackupPreparerUserID { get; set; }

        //[XmlElement(ElementName = "BackupPreparerFullName")]
        //[DataMember]
        //public virtual System.String BackupPreparerFullName { get; set; }

        //[XmlElement(ElementName = "BackupReviewerUserID")]
        //[DataMember]
        //public virtual Int32? BackupReviewerUserID { get; set; }

        //[XmlElement(ElementName = "BackupReviewerFullName")]
        //[DataMember]
        //public virtual System.String BackupReviewerFullName { get; set; }

        //[XmlElement(ElementName = "BackupApproverUserID")]
        //[DataMember]
        //public virtual Int32? BackupApproverUserID { get; set; }

        //[XmlElement(ElementName = "BackupApproverFullName")]
        //[DataMember]
        //public virtual System.String BackupApproverFullName {get; set;}

        #endregion

        [DataMember]
        public decimal? SubledgerBalanceBCCY { get; set; }
        [DataMember]
        public decimal? SubledgerBalanceRCCY { get; set; }
        [DataMember]
        public DateTime? PreparerDueDate { get; set; }
        [DataMember]
        public DateTime? ReviewerDueDate { get; set; }
        [DataMember]
        public DateTime? ApproverDueDate { get; set; }

        #region For Task Info
        [DataMember]
        public int? TotalTaskCount { get; set; }
        [DataMember]
        public int? CompletedTaskCount { get; set; }

        #endregion

        #region For Rec Control Check List

        [DataMember]
        [XmlElement(ElementName = "GLDataRecControlCheckListInfoList")]
        public List<GLDataRecControlCheckListInfo> GLDataRecControlCheckListInfoList { get; set; }

        #endregion

    }//end of class
}//end of namespace
