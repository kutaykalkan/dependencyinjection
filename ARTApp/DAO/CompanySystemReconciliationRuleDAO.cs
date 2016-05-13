


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
    public class CompanySystemReconciliationRuleDAO : CompanySystemReconciliationRuleDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CompanySystemReconciliationRuleDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public void InsertCompanySRARule(DataTable dtSRARuleID, int companyID, int recPeriodID, string addedBy, DateTime dateAdded, IDbConnection con, IDbTransaction oTransaction)
        {
            IDbCommand cmd = null;
            bool isConnectionNull = (con == null);

            try
            {
                cmd = this.CreateInsertCompanySRARuleCommand(dtSRARuleID, companyID, recPeriodID, addedBy, dateAdded);

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

        private IDbCommand CreateInsertCompanySRARuleCommand(DataTable dtSRARuleID, int companyID, int recPeriodID, string addedBy, DateTime dateAdded)
        {
            IDbCommand cmd = this.CreateCommand("usp_INS_CompanySystemReconciliationRule");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parTBLSystemReconciliationRuleID = cmd.CreateParameter();
            parTBLSystemReconciliationRuleID.ParameterName = "@TBLSystemReconciliationRuleID";
            parTBLSystemReconciliationRuleID.Value = dtSRARuleID;
            cmdParams.Add(parTBLSystemReconciliationRuleID);



            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = companyID;
            cmdParams.Add(parCompanyID);



            System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            parRecPeriodID.Value = recPeriodID;
            cmdParams.Add(parRecPeriodID);

            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!string.IsNullOrEmpty(addedBy))
                parAddedBy.Value = addedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            parDateAdded.Value = dateAdded;
            cmdParams.Add(parDateAdded);

            return cmd;
        }

        public List<CompanySystemReconciliationRuleInfo> SelectCompanySRARuleInfoByRecPeriodID(int companyID, int recPeriodID)
        {
            IDbCommand cmd = null;
            IDbConnection con = null;
            List<CompanySystemReconciliationRuleInfo> oCompanySRARuleInfoCollection = new List<CompanySystemReconciliationRuleInfo>();

            try
            {
                cmd = this.CreateSelectCompanySRARuleInfoByRecPeriodIDCommand(companyID, recPeriodID);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    oCompanySRARuleInfoCollection.Add(this.MapCompanySRARuleInfo(reader));
                }
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Dispose();
            }

            return oCompanySRARuleInfoCollection;
        }

        private CompanySystemReconciliationRuleInfo MapCompanySRARuleInfo(IDataReader reader)
        {
            CompanySystemReconciliationRuleInfo oComapnySRARuleInfo = new CompanySystemReconciliationRuleInfo();
            oComapnySRARuleInfo.CompanyID = reader.GetInt32Value("CompanyID");
            oComapnySRARuleInfo.CompanySystemReconciliationRuleID = reader.GetInt32Value("CompanySystemReconciliationRuleID");
            oComapnySRARuleInfo.IsActive = reader.GetBooleanValue("IsActivated");
            oComapnySRARuleInfo.SystemReconciliationRuleID = reader.GetInt16Value("SystemReconciliationRuleID");

            return oComapnySRARuleInfo;
        }

        private IDbCommand CreateSelectCompanySRARuleInfoByRecPeriodIDCommand(int companyID, int recPeriodID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_CompanySystemReconciliationRuleForRecPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = companyID;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            parRecPeriodID.Value = recPeriodID;
            cmdParams.Add(parRecPeriodID);

            return cmd;
        }



        public int ProcessMaterialityAndSRAByCompanyID(int CompanyID, int RecPeriodID, string revisedBy, DateTime dateRevised, IDbConnection con, IDbTransaction oTransaction)
        {

            int recordsAffected = 0;
            IDbCommand cmd = null;
            bool isConnectionNull = (con == null);

            try
            {
                cmd = GetUpdateCommandForMaterialAndSRAFlags(CompanyID, RecPeriodID, revisedBy, dateRevised);

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
                recordsAffected = cmd.ExecuteNonQuery();
            }
            finally
            {
                if (isConnectionNull && con != null && con.State == ConnectionState.Open)
                    con.Dispose();
            }

            return recordsAffected;

        }

        private IDbCommand GetUpdateCommandForMaterialAndSRAFlags(int CompanyID, int RecPeriodID, string revisedBy, DateTime dateRevised)
        {



            IDbCommand oCommand = this.CreateCommand("usp_UPD_GLDataHdrForUpdateMaterialAndSRAForceFromUI");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParamCollection = oCommand.Parameters;

            IDbDataParameter paramcompanyID = oCommand.CreateParameter();
            paramcompanyID.ParameterName = "@companyID";
            paramcompanyID.Value = CompanyID;
            cmdParamCollection.Add(paramcompanyID);

            IDbDataParameter paramRecPeriodID = oCommand.CreateParameter();
            paramRecPeriodID.ParameterName = "@RecPeriodID";
            paramRecPeriodID.Value = RecPeriodID;
            cmdParamCollection.Add(paramRecPeriodID);

            IDbDataParameter paramrevisedBy = oCommand.CreateParameter();
            paramrevisedBy.ParameterName = "@revisedBy";
            paramrevisedBy.Value = revisedBy;
            cmdParamCollection.Add(paramrevisedBy);


            IDbDataParameter paradateRevised = oCommand.CreateParameter();
            paradateRevised.ParameterName = "@dateRevised";
            paradateRevised.Value = dateRevised;
            cmdParamCollection.Add(paradateRevised);

            return oCommand;
        }
    }
}