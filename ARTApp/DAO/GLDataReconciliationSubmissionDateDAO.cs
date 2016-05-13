


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO
{
    public class GLDataReconciliationSubmissionDateDAO : GLDataReconciliationSubmissionDateDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GLDataReconciliationSubmissionDateDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

    }
}