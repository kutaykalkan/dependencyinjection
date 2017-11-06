using System.Collections.Generic;
using SkyStem.ART.Client.Model.CompanyDatabase;

namespace SkyStem.ART.Service.Interfaces
{
    public interface ICacheService
    {
        Dictionary<string, CompanyUserInfo> GetDistinctDatabaseList();
    }
}