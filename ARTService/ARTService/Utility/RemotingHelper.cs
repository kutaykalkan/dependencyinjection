using SkyStem.ART.Client.IServices;
using SkyStem.ART.Shared.Interfaces;

namespace SkyStem.ART.Service.Utility
{
    public class RemotingHelper : IRemotingHelper
    {
        /// <summary>
        /// KK: Legacy static fields should be rerouted to the interface methods.
        /// </summary>
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
        //---------------------------------------
        ICompany IRemotingHelper.GetCompanyObject()
        {
            return GetCompanyObject();
        }

        IUser IRemotingHelper.GetUserObject()
        {
            return GetUserObject();
        }

        IUtility IRemotingHelper.GetUtilityObject()
        {
            return GetUtilityObject();
        }

        IReconciliationPeriod IRemotingHelper.GetReconciliationPeriodObject()
        {
            return GetReconciliationPeriodObject();
        }

        IDataImport IRemotingHelper.GetDataImportObject()
        {
            return GetDataImportObject();
        }

        ITaskMaster IRemotingHelper.GetTaskMasterObject()
        {
            return GetTaskMasterObject();
        }

        IAlert IRemotingHelper.GetAlertObject()
        {
            return GetAlertObject();
        }

        IAttachment IRemotingHelper.GetAttachmentObject()
        {
            return GetAttachmentObject();
        }

        ISystemLockdown IRemotingHelper.GetSystemLockdownObject()
        {
            return GetSystemLockdownObject();
        }

        IAccount IRemotingHelper.GetAccountObject()
        {
            return GetAccountObject();
        }
    }
}
