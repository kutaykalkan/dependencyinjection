 


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
    public class AccountReconciliationPeriodDAO : AccountReconciliationPeriodDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AccountReconciliationPeriodDAO(AppUserInfo oAppUserInfo) :
            base( oAppUserInfo)
        {           
        }

        #region SaveAccountRecFrequecy

        public bool SaveAccountRecFrequecy(DataTable dtAccountIds, DataTable dtRecPeriodIds, int companyId, int currentRecPeriodId, IDbConnection con, IDbTransaction oTransaction)
        {
            bool result = false;
            IDbCommand cmd = null;

            try
            {
                cmd = this.CreateCommand("usp_UPD_AccountRecPeriod");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Transaction = oTransaction;

                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter cmdAccountTable = cmd.CreateParameter();
                cmdAccountTable.ParameterName = "@AccountTable";
                cmdAccountTable.Value = dtAccountIds;
                cmdParams.Add(cmdAccountTable);

                IDbDataParameter cmdRecPeriodIds = cmd.CreateParameter();
                cmdRecPeriodIds.ParameterName = "@RecPeriodTable";
                cmdRecPeriodIds.Value = dtRecPeriodIds;
                cmdParams.Add(cmdRecPeriodIds);

                IDbDataParameter cmdCompanyID = cmd.CreateParameter();
                cmdCompanyID.ParameterName = "@CompanyID";
                cmdCompanyID.Value = companyId;
                cmdParams.Add(cmdCompanyID);

                IDbDataParameter cmdRecPeriodID = cmd.CreateParameter();
                cmdRecPeriodID.ParameterName = "@RecPeriodID";
                cmdRecPeriodID.Value = currentRecPeriodId;
                cmdParams.Add(cmdRecPeriodID);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                    result = true;
            }
            finally
            {
            }

            return result;
        }

        #endregion

        #region SelectAccountRecPeriodByAccountID

        internal List<AccountReconciliationPeriodInfo> SelectAccountRecPeriodByAccountID(long accountID)
        {
            List<AccountReconciliationPeriodInfo> oAccountHdrInfoCollection = new List<AccountReconciliationPeriodInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateCommand("usp_SEL_AccountReconcililiationPeriodByAccountID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                con.Open();

                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter paramAccountID = cmd.CreateParameter();
                paramAccountID.ParameterName = "@AccountID";
                paramAccountID.Value = accountID;
                cmdParams.Add(paramAccountID);

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);                

                while (reader.Read())
                {
                    oAccountHdrInfoCollection.Add(this.MapObject(reader));
                }
                reader.ClearColumnHash();
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oAccountHdrInfoCollection;
        }

        #endregion

    }
}