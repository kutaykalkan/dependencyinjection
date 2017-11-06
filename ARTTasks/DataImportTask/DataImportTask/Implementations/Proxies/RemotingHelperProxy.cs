﻿using SkyStem.ART.Client.IServices;
using SkyStem.ART.Service.Interfaces;

namespace DataImportTask.Implementations.Proxies
{
    internal class RemotingHelperProxy : IRemotingHelper
    {
        private readonly IRemotingHelper _remotingHelper;

        public RemotingHelperProxy(IRemotingHelper remotingHelper)
        {
            _remotingHelper = remotingHelper;
        }

        public IAccount GetAccountObject()
        {
            return _remotingHelper.GetAccountObject();
        }

        public ICompany GetCompanyObject()
        {
            return _remotingHelper.GetCompanyObject();
        }

        public IUser GetUserObject()
        {
            return _remotingHelper.GetUserObject();
        }

        public IUtility GetUtilityObject()
        {
            return _remotingHelper.GetUtilityObject();
        }

        public IReconciliationPeriod GetReconciliationPeriodObject()
        {
            return _remotingHelper.GetReconciliationPeriodObject();
        }

        public IDataImport GetDataImportObject()
        {
            return _remotingHelper.GetDataImportObject();
        }

        public ITaskMaster GetTaskMasterObject()
        {
            return _remotingHelper.GetTaskMasterObject();
        }

        public IAlert GetAlertObject()
        {
            return _remotingHelper.GetAlertObject();
        }

        public IAttachment GetAttachmentObject()
        {
            return _remotingHelper.GetAttachmentObject();
        }

        public ISystemLockdown GetSystemLockdownObject()
        {
            return _remotingHelper.GetSystemLockdownObject();
        }
    }
}