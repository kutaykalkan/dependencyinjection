using SkyStem.ART.Client.IServices;

namespace SkyStem.ART.Shared.Interfaces
{
    public interface IRemotingHelper
    {
        IAccount GetAccountObject();

        ICompany GetCompanyObject();

        IUser GetUserObject();

        IUtility GetUtilityObject();

        IReconciliationPeriod GetReconciliationPeriodObject();

        IDataImport GetDataImportObject();

        ITaskMaster GetTaskMasterObject();

        IAlert GetAlertObject();

        IAttachment GetAttachmentObject();
        
        ISystemLockdown GetSystemLockdownObject();
    }
}
