


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO
{
    public class RoleReconciliationDueDateDAO : RoleReconciliationDueDateDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RoleReconciliationDueDateDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
    }
}