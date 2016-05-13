


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO
{
    public class CompanyRoleDAO : CompanyRoleDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CompanyRoleDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public void SaveCompanyRole(int CompanyId, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            IDbCommand IDBCmmd = this.CreateCommand("usp_INS_CompanyRoles");
            IDBCmmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = IDBCmmd.Parameters;
            IDbDataParameter cmdParamCompanyId = IDBCmmd.CreateParameter();
            cmdParamCompanyId.ParameterName = "@companyid";
            cmdParamCompanyId.Value = CompanyId;
            cmdParams.Add(cmdParamCompanyId);
            IDBCmmd.Connection = oConnection;
            IDBCmmd.Transaction = oTransaction;
            IDBCmmd.ExecuteNonQuery();

        }
    }




}