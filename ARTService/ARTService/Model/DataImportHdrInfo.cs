using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Service.Data;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.Service.Model
{
    public class DataImportHdrInfo : AuditField
    {
        public int CompanyID { get; set; }
        public int DataImportID { get; set; }
        public int RecPeriodID { get; set; }
        public short DataImportTypeID { get; set; }
        public string PhysicalPath { get; set; }
        public DateTime PeriodEndDate { get; set; }
        public string ProfileName { get; set; }
        public string KeyFields { get; set; }
        public short DataImportStatusID { get; set; }
        public bool IsForceCommit { get; set; }
        public bool IsMultiVersionUpload { get; set; }
        public bool IsDataTransfered { get; set; }
        public bool IsDataDeletionRequired { get; set; }

        public string NotifySuccessEmailIds { get; set; }
        public string NotifySuccessUserEmailIds { get; set; }
        public string NotifyFailureEmailIds { get; set; }
        public string NotifyFailureUserEmailIds { get; set; }

        // Added for Task# 776 
        public short? RoleID { get; set; }
        public int? UserID { get; set; }

        public string WarningEmailIds
        {
            get;
            set;
        }
        public string SuccessEmailIDs
        {
            get;
            set;
        }
        public string FailureEmailIDs
        {
            get;
            set;
        }

        public DateTime? DateAdded { get; set; }
        public string AddedBy { get; set; }

        public string ErrorMessageFromSqlServer { get; set; }
        public string ErrorMessageToSave { get; set; }
        public string DataImportStatus { get; set; }
        public int RecordsImported { get; set; }

        public decimal MaxSumThreshold { get; set; }
        public decimal MinSumThreshold { get; set; }

        public short? WarningReasonID { get; set; }
        public int LanguageID { get; set; }
        public int DefaultLanguageID { get; set; }
        public string AccountUniqueSubSetKeys { get; set; }
        public string ProfilingData { get; set; }
        public List<DataImportMessageDetailInfo> DataImportMessageDetailInfoList { get; set; }
        public int? ImportTemplateID { get; set; }

        public DataImportHdrInfo()
        {
            this.CompanyID = -1;
            this.DataImportID = -1;
            this.RecPeriodID = -1;
            this.DataImportTypeID = -1;

            this.PhysicalPath = "";
            this.PeriodEndDate = DateTime.MinValue;

            this.AddedBy = "";
            this.KeyFields = "";
            this.DataImportStatusID = -1;
            this.IsForceCommit = false;
            this.NotifySuccessEmailIds = "";
            this.NotifySuccessUserEmailIds = "";
            this.NotifyFailureEmailIds = "";
            this.NotifyFailureUserEmailIds = "";
            this.ErrorMessageFromSqlServer = "";
            this.ErrorMessageToSave = "";
            this.DataImportStatus = "";
            this.RecordsImported = 0;

            this.SuccessEmailIDs = "";
            this.FailureEmailIDs = "";
            this.WarningEmailIds = "";
            this.DateAdded = DateTime.Now;

            this.ProfileName = "";
            this.WarningReasonID = null;

            this.LanguageID = -1;
            this.DefaultLanguageID = ServiceConstants.DEFAULTLANGUAGEID;

            this.IsMultiVersionUpload = false;
            this.IsDataDeletionRequired = false;
            this.IsDataTransfered = false;
            this.RoleID = null;
            this.UserID = null;
            this.DataImportMessageDetailInfoList = new List<DataImportMessageDetailInfo>();
            this.ImportTemplateID = null;
        }
    }
}
