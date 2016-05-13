


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using System.Collections.Generic;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO
{
    public class ReportParameterDAO : ReportParameterDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ReportParameterDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public List<short> GetPermittedRolesByReportID(short? reportID, short? currentUserRole, int? RecPeriodID, int? companyID)
        {

            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr = null;
            List<short> oPermittedRoleList = null;
            try
            {
                cmd = CreateGetPermittedRoleListCommand(reportID, currentUserRole, RecPeriodID, companyID);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                oPermittedRoleList = GetPermittedRoleList(dr);

            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                }

                if (cmd != null)
                {
                    if (cmd.Connection != null)
                    {
                        if (cmd.Connection.State != ConnectionState.Closed)
                        {
                            cmd.Connection.Close();
                            cmd.Connection.Dispose();
                        }
                    }
                    cmd.Dispose();
                }

            }
            return oPermittedRoleList;
        }
        private IDbCommand CreateGetPermittedRoleListCommand(short? reportID, short? currentUserRole, int? RecPeriodID, int? companyID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_RolesByReportID");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdReportID = cmd.CreateParameter();
            cmdReportID.ParameterName = "@ReportID";
            cmdReportID.Value = reportID.Value;
            cmdParams.Add(cmdReportID);

            IDbDataParameter cmdCurrentUserRole = cmd.CreateParameter();
            cmdCurrentUserRole.ParameterName = "@RoleID";
            cmdCurrentUserRole.Value = currentUserRole.Value;
            cmdParams.Add(cmdCurrentUserRole);

            IDbDataParameter cmdRecPeriodID = cmd.CreateParameter();
            cmdRecPeriodID.ParameterName = "@RecPeriodID";
            cmdRecPeriodID.Value = RecPeriodID;
            cmdParams.Add(cmdRecPeriodID);


            IDbDataParameter cmdCompanyID = cmd.CreateParameter();
            cmdCompanyID.ParameterName = "@CompanyID";
            cmdCompanyID.Value = companyID.Value;
            cmdParams.Add(cmdCompanyID);


            return cmd;
        }


        private List<short> GetPermittedRoleList(IDataReader r)
        {
            List<short> oPermittedRoleList = new List<short>();
            while (r.Read())
            {
                oPermittedRoleList.Add(r.GetInt16Value("RoleID").Value);
            }
            return oPermittedRoleList;
        }

    }
}