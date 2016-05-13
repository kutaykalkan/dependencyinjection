using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.App.Services;

namespace SkyStem.ART.Service.Utility
{
    public class RemotingHelper
    {
        public static IAccount GetAccountObject()
        {
            return new SkyStem.ART.App.Services.Account();
        }
        public static ICompany GetCompanyObject()
        {
            return new SkyStem.ART.App.Services.Company();
        }
        public static IUser GetUserObject()
        {
            return new SkyStem.ART.App.Services.User();
        }
        public static IUtility GetUtilityObject()
        {
            return new SkyStem.ART.App.Services.Utility();
        }
        public static IReconciliationPeriod GetReconciliationPeriodObject()
        {
            return new SkyStem.ART.App.Services.ReconciliationPeriod();
        }
        public static IDataImport GetDataImportObject()
        {
            return new SkyStem.ART.App.Services.DataImport();
        }
        public static ITaskMaster GetTaskMasterObject()
        {
            return new SkyStem.ART.App.Services.TaskMaster();
        }
        public static IAlert GetAlertObject()
        {
            return new SkyStem.ART.App.Services.Alert();
        }
        public static IAttachment GetAttachmentObject()
        {
            return new SkyStem.ART.App.Services.Attachment();
        }
        public static ISystemLockdown GetSystemLockdownObject()
        {
            return new SkyStem.ART.App.Services.SystemLockdown();
        }
    }
}
