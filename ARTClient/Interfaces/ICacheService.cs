using System.Collections.Generic;
using SkyStem.ART.Client.Model.CompanyDatabase;

namespace SkyStem.ART.Client.Interfaces
{
    public interface ICacheService
    {
        Dictionary<string, CompanyUserInfo> GetDistinctDatabaseList();
    }
}