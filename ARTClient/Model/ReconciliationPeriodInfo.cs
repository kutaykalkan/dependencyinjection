
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace SkyStem.ART.Client.Model
{

    /// <summary>
    /// An object representation of the SkyStemArt ReconciliationPeriod table
    /// </summary>
    [Serializable]
    [DataContract]
    public class ReconciliationPeriodInfo : ReconciliationPeriodInfoBase
    {

        protected System.Boolean? _AllowCertificationLockdown = null;
        public bool IsAllowCertificationLockdownNull = true;
        protected System.String _FinancialYear = null;

        [XmlElement(ElementName = "AllowCertificationLockdown")]
        public virtual System.Boolean? AllowCertificationLockdown
        {
            get
            {
                return this._AllowCertificationLockdown;
            }
            set
            {
                this._AllowCertificationLockdown = value;

                this.IsAllowCertificationLockdownNull = false;
            }
        }

        //protected System.DateTime? _PreparerDueDate = DateTime.Now;
        protected System.DateTime? _PreparerDueDate = null;
        public bool IsPreparerDueDateNull = true;
        [XmlElement(ElementName = "PreparerDueDate")]
        public virtual System.DateTime? PreparerDueDate
        {
            get
            {
                return this._PreparerDueDate;
            }
            set
            {
                this._PreparerDueDate = value;
                this.IsPreparerDueDateNull = (_PreparerDueDate == DateTime.MinValue);
            }
        }

        //protected System.DateTime? _ReviewerDueDate = DateTime.Now;
        protected System.DateTime? _ReviewerDueDate = null;
        public bool IsReviewerDueDateNull = true;
        [XmlElement(ElementName = "ReviewerDueDate")]
        public virtual System.DateTime? ReviewerDueDate
        {
            get
            {
                return this._ReviewerDueDate;
            }
            set
            {
                this._ReviewerDueDate = value;
                this.IsReviewerDueDateNull = (_ReviewerDueDate == DateTime.MinValue);
            }
        }

        //protected System.DateTime? _ApproverDueDate = DateTime.Now;
        protected System.DateTime? _ApproverDueDate = null;
        public bool IsApproverDueDateNull = true;
        [XmlElement(ElementName = "ApproverDueDate")]
        public virtual System.DateTime? ApproverDueDate
        {
            get
            {
                return this._ApproverDueDate;
            }
            set
            {
                this._ApproverDueDate = value;
                this.IsApproverDueDateNull = (_ApproverDueDate == DateTime.MinValue);
            }
        }

        protected System.Int16? _ReconciliationPeriodStatusID = 0;
        public bool IsReconciliationPeriodStatusIDNull = true;
        [XmlElement(ElementName = "ReconciliationPeriodStatusID")]
        public virtual System.Int16? ReconciliationPeriodStatusID
        {
            get
            {
                return this._ReconciliationPeriodStatusID;
            }
            set
            {
                this._ReconciliationPeriodStatusID = value;
                this.IsReconciliationPeriodStatusIDNull = false;
            }
        }

        protected System.Int32? _DataImportID = 0;
        public bool IsDataImportIDNull = true;

        [XmlElement(ElementName = "DataImportID")]
        public virtual System.Int32? DataImportID
        {
            get
            {
                return this._DataImportID;
            }
            set
            {
                this._DataImportID = value;

                this.IsDataImportIDNull = false;
            }
        }

        protected System.Int32? _financialYearID = 0;
        public bool IsFinancialYearIDNull = true;

        [XmlElement(ElementName = "FinancialYearID")]
        public virtual System.Int32? FinancialYearID
        {
            get { return this._financialYearID; }
            set
            {
                this._financialYearID = value;
                this.IsFinancialYearIDNull = false;
            }
        }


        [XmlElement(ElementName = "FinancialYear")]
        public virtual System.String FinancialYear
        {
            get
            {
                return this._FinancialYear;
            }
            set
            {
                this._FinancialYear = value;
            }
        }




        protected System.Boolean? _IsStopRecAndStartCert = null;
        [XmlElement(ElementName = "IsStopRecAndStartCert")]
        public virtual System.Boolean? IsStopRecAndStartCert
        {
            get
            {
                return this._IsStopRecAndStartCert;
            }
            set
            {
                this._IsStopRecAndStartCert = value;

            }
        }

        protected System.Int32? _ReconciliationPeriodStatusLabelID = 0;
        public bool IsReconciliationPeriodStatusLabelIDNull = true;
        [XmlElement(ElementName = "ReconciliationPeriodStatuslabelID")]
        public virtual System.Int32? ReconciliationPeriodStatusLabelID
        {
            get
            {
                return this._ReconciliationPeriodStatusLabelID;
            }
            set
            {
                this._ReconciliationPeriodStatusLabelID = value;
                this.IsReconciliationPeriodStatusLabelIDNull = false;
            }
        }

        protected System.String _ReconciliationPeriodStatus;
        public bool IsReconciliationPeriodStatusNull = true;
        [XmlElement(ElementName = "ReconciliationPeriodStatus")]
        public virtual System.String ReconciliationPeriodStatus
        {
            get
            {
                return this._ReconciliationPeriodStatus;
            }
            set
            {
                this._ReconciliationPeriodStatus = value;
                this.IsReconciliationPeriodStatusNull = false;
            }
        }
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

        [XmlElement(ElementName = "NextReconciliationCloseDate")]
        public DateTime? NextReconciliationCloseDate { get; set; }

        [XmlElement(ElementName = "PreviousReconciliationCloseDate")]
        public DateTime? PreviousReconciliationCloseDate { get; set; }

        public static List<ReconciliationPeriodInfo> DeSerializeRecPeriodInfoList(string xmlString)
        {
            XmlSerializer xmlSerial = null;
            StringReader strReader = null;
            XmlTextReader txtReader = null;
            try
            {
                xmlSerial = new XmlSerializer(typeof(List<ReconciliationPeriodInfo>));
                strReader = new StringReader(xmlString);
                txtReader = new XmlTextReader(strReader);
                return (List<ReconciliationPeriodInfo>)xmlSerial.Deserialize(txtReader);
            }
            finally
            {
                txtReader.Close();
                strReader.Close();
            }
        }
    }//end of class
}//end of namespace
