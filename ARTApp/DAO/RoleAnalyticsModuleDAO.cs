


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO
{
    public class RoleAnalyticsModuleDAO : RoleAnalyticsModuleDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RoleAnalyticsModuleDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
    }
}