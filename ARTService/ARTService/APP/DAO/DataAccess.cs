using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using SkyStem.ART.Service.Log;
using SkyStem.ART.Service.APP.DAO;
using SkyStem.ART.Client.Model.CompanyDatabase;

namespace SkyStem.ART.Service.DAO
{
    public class DataAccess: AbstractDAO
    {
        public DataAccess(CompanyUserInfo oCompanyUserInfo)
            : base(oCompanyUserInfo)
        {
        }
    }
}
