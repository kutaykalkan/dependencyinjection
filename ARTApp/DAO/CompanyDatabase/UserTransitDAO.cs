 

using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.App.DAO.CompanyDatabase.Base;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO.CompanyDatabase
{   
    public class UserTransitDAO : UserTransitDAOBase
    {
        public UserTransitDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
    }
}