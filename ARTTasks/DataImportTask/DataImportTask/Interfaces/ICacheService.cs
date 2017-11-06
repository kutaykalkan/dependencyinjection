using System.Collections.Generic;
using SkyStem.ART.Client.Model.CompanyDatabase;

namespace DataImportTask.Interfaces
{
    public interface ICacheService
    {
        Dictionary<string, CompanyUserInfo> GetDistinctDatabaseList();
    }
}