

using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;

namespace SkyStem.ART.App.DAO
{
    public class SystemLockdownReasonMstDAO : SystemLockdownReasonMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SystemLockdownReasonMstDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public List<SystemLockdownReasonMstInfo> GetAllSystemLockdownReasons()
        {
            using (IDbConnection cnn = this.CreateConnection())
            {
                cnn.Open();
                using (IDbCommand oCommand = this.GetAllSystemLockdownReasonsCommand())
                {
                    oCommand.Connection = cnn;
                    return this.Select(oCommand);
                }
            }
        }

        private IDbCommand GetAllSystemLockdownReasonsCommand()
        {
            IDbCommand oCommand = this.CreateCommand("usp_SEL_AllSystemLockdownReasons");
            oCommand.CommandType = CommandType.StoredProcedure;
            return oCommand;
        }
    }
}