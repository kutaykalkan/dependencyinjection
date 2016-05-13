
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace SkyStem.ART.Client.Model
{

    /// <summary>
    /// An object representation of the SkyStemArt AccountHdr table
    /// </summary>
    [Serializable]
    [DataContract]
    public class AccountHdrInfo : AccountHdrInfoBase
    {
        private string _NetAccount;
        private int? _NetAccountLabelID;
        private string _RiskRating;
        private string _KeyAccount;
        private string _ZeroBalance;
        private string _ReconciliationTemplate;
        private List<int> _RecPeriodIDCollection;
        private bool? _IsExcludeOwnershipForZBA;
        private decimal? _AccountGLBalance;
        private bool? _IsUserGeographyObject;
        private string _baseCurrencyCode;
        //IsReconcilable is marked as ture initially because we are assuming if Reconcilable attribute is not set, it is reconcilable by default.
        private bool? _IsReconcilable = true;
        private string _Reconcilable;

        private string _CertificationStatus;
        protected System.Int32? _CertificationStatusLabelID = 0;
        private string _ReconciliationStatus;
        protected System.Int32? _ReconciliationStatusLabelID = 0;
        private string _AccountMateriality = "";
        private System.Int32? _AccountMaterialityLabelID = 0;

        [XmlElement(ElementName = "BaseCurrencyCode")]
        public string BaseCurrencyCode
        {
            get
            {
                return this._baseCurrencyCode;
            }
            set
            {
                this._baseCurrencyCode = value;
            }
        }
        [XmlElement(ElementName = "NetAccount")]
        public string NetAccount
        {
            get
            {
                return this._NetAccount;
            }
            set
            {
                this._NetAccount = value;
            }
        }

        [XmlElement(ElementName = "NetAccountLabelID")]
        public int? NetAccountLabelID
        {
            get
            {
                return this._NetAccountLabelID;
            }
            set
            {
                this._NetAccountLabelID = value;
            }
        }

        [XmlElement(ElementName = "RiskRating")]
        public string RiskRating
        {
            get
            {
                return this._RiskRating;
            }
            set
            {
                this._RiskRating = value;
            }
        }

        [XmlElement(ElementName = "ZeroBalance")]
        public string ZeroBalance
        {
            get
            {
                return this._ZeroBalance;
            }
            set
            {
                this._ZeroBalance = value;
            }
        }

        [XmlElement(ElementName = "KeyAccount")]
        public string KeyAccount
        {
            get
            {
                return this._KeyAccount;
            }
            set
            {
                this._KeyAccount = value;
            }
        }

        [XmlElement(ElementName = "ReconciliationTemplate")]
        public string ReconciliationTemplate
        {
            get
            {
                return this._ReconciliationTemplate;
            }
            set
            {
                this._ReconciliationTemplate = value;
            }
        }

        [XmlElement(ElementName = "RecPeriodIDCollection")]
        public List<int> RecPeriodIDCollection
        {
            get
            {
                return this._RecPeriodIDCollection;
            }
            set
            {
                this._RecPeriodIDCollection = value;
            }
        }

        [XmlElement(ElementName = "IsExcludeOwnershipForZBA")]
        public bool? IsExcludeOwnershipForZBA
        {
            get
            {
                return this._IsExcludeOwnershipForZBA;
            }
            set
            {
                this._IsExcludeOwnershipForZBA = value;
            }
        }

        [XmlElement(ElementName = "AccountGLBalance")]
        public decimal? AccountGLBalance
        {
            get
            {
                return this._AccountGLBalance;
            }
            set
            {
                this._AccountGLBalance = value;
            }
        }

        [XmlElement(ElementName = "IsUserGeographyObject")]
        public bool? IsUserGeographyObject
        {
            get
            {
                return this._IsUserGeographyObject;
            }
            set
            {
                this._IsUserGeographyObject = value;
            }
        }

        protected System.String _PreparerFullName = "";
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
            }
        }

        protected System.String _ReviewerFullName = "";
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
            }
        }

        protected System.String _ApproverFullName = "";
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
            }
        }
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
            }
        }

        [XmlElement(ElementName = "AccountMaterialityLabelID")]
        public virtual System.Int32? AccountMaterialityLabelID
        {
            get
            {
                return this._AccountMaterialityLabelID;
            }
            set
            {
                this._AccountMaterialityLabelID = value;
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
            }
        }

        [XmlElement(ElementName = "IsReconcilable")]
        public bool? IsReconcilable
        {
            get
            {
                return this._IsReconcilable;
            }
            set
            {
                this._IsReconcilable = value;
            }
        }

        [XmlElement(ElementName = "Reconcilable")]
        public string Reconcilable
        {
            get
            {
                return this._Reconcilable;
            }
            set
            {
                this._Reconcilable = value;
            }
        }

        #region For Backup Owners

        [XmlElement(ElementName = "BackupPreparerUserID")]
        [DataMember]
        public virtual Int32? BackupPreparerUserID { get; set; }

        [XmlElement(ElementName = "BackupPreparerFullName")]
        [DataMember]
        public virtual System.String BackupPreparerFullName { get; set; }

        //[XmlElement(ElementName = "BackupReviewerUserID")]
        //[DataMember]
        //public virtual Int32? BackupReviewerUserID { get; set; }

        [XmlElement(ElementName = "BackupReviewerFullName")]
        [DataMember]
        public virtual System.String BackupReviewerFullName { get; set; }

        //[XmlElement(ElementName = "BackupApproverUserID")]sss
        //[DataMember]
        //public virtual Int32? BackupApproverUserID { get; set; }

        [XmlElement(ElementName = "BackupApproverFullName")]
        [DataMember]
        public virtual System.String BackupApproverFullName { get; set; }

        #endregion

        [XmlElement(ElementName = "IsLocked")]
        public bool? IsLocked { get; set; }

        [XmlElement(ElementName = "ReconciliationStatusID")]
        public short? ReconciliationStatusID { get; set; }

        [XmlElement(ElementName = "IsSystemReconcilied")]
        public bool? IsSystemReconcilied { get; set; }

        protected System.Boolean? _IsTaskCompleted = null;
        [XmlElement(ElementName = "IsTaskCompleted")]
        public virtual System.Boolean? IsTaskCompleted
        {
            get
            {
                return this._IsTaskCompleted;
            }
            set
            {
                this._IsTaskCompleted = value;

            }
        }

        [XmlElement(ElementName = "PreparerDueDays")]
        public Int32? PreparerDueDays { get; set; }

        [XmlElement(ElementName = "ReviewerDueDays")]
        public Int32? ReviewerDueDays { get; set; }

        [XmlElement(ElementName = "ApproverDueDays")]
        public Int32? ApproverDueDays { get; set; }

        //[XmlElement(ElementName = "DayTypeID")]
        //public Int16? DayTypeID { get; set; }

        //[XmlElement(ElementName = "DayType")]
        //public string DayType { get; set; }

        [XmlElement(ElementName = "PreparerDueDate")]
        public DateTime? PreparerDueDate { get; set; }
        [XmlElement(ElementName = "ReviewerDueDate")]
        public DateTime? ReviewerDueDate { get; set; }
        [XmlElement(ElementName = "ApproverDueDate")]
        public DateTime? ApproverDueDate { get; set; }

        public static List<AccountHdrInfo> DeSerializeAccountList(string xmlString)
        {
            XmlSerializer xmlSerial = null;
            StringReader strReader = null;
            XmlTextReader txtReader = null;
            try
            {
                xmlSerial = new XmlSerializer(typeof(List<AccountHdrInfo>));
                strReader = new StringReader(xmlString);
                txtReader = new XmlTextReader(strReader);
                return (List<AccountHdrInfo>)xmlSerial.Deserialize(txtReader);
            }
            finally
            {
                txtReader.Close();
                strReader.Close();
            }
        }
        [DataMember]
        public int? NumberValue { get; set; }
        [DataMember]
        public DateTime? DateValue { get; set; }
        [DataMember]
        public int? ChangeTypeLabelID { get; set; }
        [DataMember]
        public string ExistingGLBalanceRCCY { get; set; }
        [DataMember]
        public string CurrentGLBalanceRCCY { get; set; }
        [DataMember]
        public string ExistingGLBalanceBCCY { get; set; }
        [DataMember]
        public string CurrentGLBalanceBCCY { get; set; }
        [DataMember]
        public bool? ShowBalanceChangeColumnInMail { get; set; }
        [DataMember]
        public short? ActionTypeID { get; set; }
        [DataMember]
        [XmlElement(ElementName = "CreationPeriodEndDate")]
        public DateTime? CreationPeriodEndDate { get; set; }
        [XmlElement(ElementName = "PreparerLanguageID")]
        public int? PreparerLanguageID { get; set; }
        [XmlElement(ElementName = "ReviewerLanguageID")]
        public int? ReviewerLanguageID { get; set; }
        [XmlElement(ElementName = "ApproverLanguageID")]
        public int? ApproverLanguageID { get; set; }

    }
}
