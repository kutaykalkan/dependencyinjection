﻿using System.Collections.Generic;
using DataImportTask.Interfaces;
using SkyStem.ART.Client.Model.CompanyDatabase;
using SkyStem.ART.Service.DAO;
using SkyStem.ART.Shared.Interfaces;

namespace DataImportTask.Implementations.Services
{
    public class CacheService : ICacheService
    {
        private readonly IRemotingHelper _remotingHelper;

        private List<CompanyUserInfo> _companyUserInfos;

        public CacheService(IRemotingHelper remotingHelper)
        {
            _remotingHelper = remotingHelper;
        }

        public Dictionary<string, CompanyUserInfo> GetDistinctDatabaseList()
        {
            var oDictConnectionString = new Dictionary<string, CompanyUserInfo>();
            var companyList = GetCompanyList();
            foreach (var oCompanyUserInfo in companyList)
            {
                var oDA = new DataAccess(oCompanyUserInfo);
                var connectionString = oDA.GetConnectionString();
                if (!oDictConnectionString.ContainsKey(connectionString))
                    oDictConnectionString.Add(connectionString, oCompanyUserInfo);
            }
            return oDictConnectionString;
        }

        private List<CompanyUserInfo> GetCompanyList()
        {
            if (_companyUserInfos == null)
            {
                var oUtilityClient = _remotingHelper.GetUtilityObject();
                _companyUserInfos = oUtilityClient.GetAllCompanyConnectionInfo();
            }
            return _companyUserInfos;
        }
    }
}