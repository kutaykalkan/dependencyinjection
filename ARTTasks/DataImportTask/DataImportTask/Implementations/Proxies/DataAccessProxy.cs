using DataImportTask.CommonInterfaces.DataAccess;
using SkyStem.ART.Client.Model.CompanyDatabase;
using SkyStem.ART.Service.APP.DAO;
using SkyStem.ART.Service.DAO;

namespace DataImportTask.Implementations.Proxies
{
    public class DataAccessProxy : IDAOBase
    {
        public string GetConnectionString(CompanyUserInfo companyUserInfo)
        {
            return new DataAccess(companyUserInfo).GetConnectionString();
        }
    }
}
