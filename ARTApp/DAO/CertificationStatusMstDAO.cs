


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.App.Utility;
using System.Data.SqlClient;

namespace SkyStem.ART.App.DAO
{
    public class CertificationStatusMstDAO : CertificationStatusMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CertificationStatusMstDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public List<CertificationStatusMstInfo> GetAllReason()
        {
            IDbCommand oCommand = this.GetAllCertStatusCommand();
            oCommand.Connection = this.CreateConnection();
            return this.Select(oCommand);
        }
        private IDbCommand GetAllCertStatusCommand()
        {
            IDbCommand oCommand = this.CreateCommand("usp_SEL_AllCertificatioStatus");
            oCommand.CommandType = CommandType.StoredProcedure;

            return oCommand;
        }
    }
}