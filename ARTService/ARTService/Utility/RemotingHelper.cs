using SkyStem.ART.App.Services;
using SkyStem.ART.Client.Interfaces;
using SkyStem.ART.Client.IServices;

namespace SkyStem.ART.Service.Utility
{
    public class RemotingHelper : IRemotingHelper
    {
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

        /// <summary>
        ///     KK: Legacy static fields should be rerouted to the interface methods.
        /// </summary>
        public static IAccount GetAccountObject()
        {
            return new Account();
        }

        public static ICompany GetCompanyObject()
        {
            return new Company();
        }

        public static IUser GetUserObject()
        {
            return new User();
        }

        public static IUtility GetUtilityObject()
        {
            return new App.Services.Utility();
        }

        public static IReconciliationPeriod GetReconciliationPeriodObject()
        {
            return new ReconciliationPeriod();
        }

        public static IDataImport GetDataImportObject()
        {
            return new DataImport();
        }

        public static ITaskMaster GetTaskMasterObject()
        {
            return new TaskMaster();
        }

        public static IAlert GetAlertObject()
        {
            return new Alert();
        }

        public static IAttachment GetAttachmentObject()
        {
            return new Attachment();
        }

        public static ISystemLockdown GetSystemLockdownObject()
        {
            return new SystemLockdown();
        }
    }
}