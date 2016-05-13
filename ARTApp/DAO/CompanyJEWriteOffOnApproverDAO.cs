


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
    public class CompanyJEWriteOffOnApproverDAO : CompanyJEWriteOffOnApproverDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CompanyJEWriteOffOnApproverDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public List<CompanyJEWriteOffOnApproverInfo> GetCompanyJEWriteOffOnApproversByRecPeriodID(int? RecPeriodID, int? CompanyID)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr = null;
            List<CompanyJEWriteOffOnApproverInfo> oCompanyJEWriteOffOnApproverInfoCollection = null;
            try
            {
                cmd = CreateGetCompanyJEWriteOffOnApproversListCommand(RecPeriodID, CompanyID);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                oCompanyJEWriteOffOnApproverInfoCollection = MapObjects(dr);

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
            return oCompanyJEWriteOffOnApproverInfoCollection;
        }
        private IDbCommand CreateGetCompanyJEWriteOffOnApproversListCommand(int? RecPeriodID, int? CompanyID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_CompanyJEWriteOffOnApproversByRecPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdRecPeriodId = cmd.CreateParameter();
            cmdRecPeriodId.ParameterName = "@RecPeriodID";
            cmdRecPeriodId.Value = RecPeriodID;
            cmdParams.Add(cmdRecPeriodId);

            IDbDataParameter cmdCompanyId = cmd.CreateParameter();
            cmdCompanyId.ParameterName = "@CompanyID";
            cmdCompanyId.Value = CompanyID;
            cmdParams.Add(cmdCompanyId);


            return cmd;
        }

        public void SaveCompanyJEWriteOffOnApprovers(DataTable dtCompanyJEWriteOffOnApprovers, int? companyID, int? startRecPeriodID, string addedBy, DateTime? dateAdded, string revisedBy, DateTime? dateRevised, IDbConnection con, IDbTransaction oTransaction)
        {
            IDbCommand cmd = null;
            bool isConnectionNull = (con == null);
            try
            {
                cmd = this.CreateSaveCompanyJEWriteOffOnApproversCommand(dtCompanyJEWriteOffOnApprovers, companyID, startRecPeriodID, addedBy, dateAdded, revisedBy, dateRevised);
                if (con == null)
                {
                    con = this.CreateConnection();
                    con.Open();
                }
                else
                {
                    cmd.Transaction = oTransaction;
                }
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (isConnectionNull && con != null && con.State == ConnectionState.Open)
                    con.Dispose();
            }
        }

        private IDbCommand CreateSaveCompanyJEWriteOffOnApproversCommand(DataTable dtCompanyJEWriteOffOnApprovers, int? companyID, int? startRecPeriodID, string addedBy, DateTime? dateAdded, string revisedBy, DateTime? dateRevised)
        {
            IDbCommand cmd = this.CreateCommand("usp_SAV_CompanyJEWriteOffOnApprovers");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parTBLCompanyJEWriteOffOnApprovers = cmd.CreateParameter();
            parTBLCompanyJEWriteOffOnApprovers.ParameterName = "@TBLCompanyJEWriteOffOnApprovers";
            parTBLCompanyJEWriteOffOnApprovers.Value = dtCompanyJEWriteOffOnApprovers;
            cmdParams.Add(parTBLCompanyJEWriteOffOnApprovers);

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = companyID.Value;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parStartRecPeriodID = cmd.CreateParameter();
            parStartRecPeriodID.ParameterName = "@StartRecPeriodID";
            parStartRecPeriodID.Value = startRecPeriodID.Value;
            cmdParams.Add(parStartRecPeriodID);

            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            parAddedBy.Value = addedBy;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            parDateAdded.Value = dateAdded.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            parRevisedBy.Value = revisedBy;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            parDateRevised.Value = dateRevised.Value;
            cmdParams.Add(parDateRevised);

            return cmd;
        }



    }
}