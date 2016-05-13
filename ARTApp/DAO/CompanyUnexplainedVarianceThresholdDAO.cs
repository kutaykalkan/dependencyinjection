


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using System.Collections.Generic;
using SkyStem.ART.Client.Model;
using System.Data.SqlClient;

namespace SkyStem.ART.App.DAO
{
    public class CompanyUnexplainedVarianceThresholdDAO : CompanyUnexplainedVarianceThresholdDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CompanyUnexplainedVarianceThresholdDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        public IList<CompanyUnexplainedVarianceThresholdInfo> GetUnexplainedVarianceThresholdByRecPeriodID(int? reconciliationPeriodID)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_UnexplainedVarianceThresholdByRecPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@InputReconciliationPeriodID";
            par.Value = reconciliationPeriodID;
            cmdParams.Add(par);

            return this.Select(cmd);
        }
        internal int? UpdateCompanyUnexplainedVarianceThreshold(int? reconciliationPeriodID
            , CompanyUnexplainedVarianceThresholdInfo oCompanyUnexplainedVarianceThresholdInfo, IDbConnection oConnection
            , IDbTransaction oTransaction, out Boolean isThresholdChanged)
        {
            int result = 0;
            System.Data.IDbCommand oCommand = null;
            oCommand = CreateVarianceThresholdUpdateCommand(reconciliationPeriodID, oCompanyUnexplainedVarianceThresholdInfo);
            oCommand.Connection = oConnection;
            oCommand.Transaction = oTransaction;
            result = Convert.ToInt32(oCommand.ExecuteNonQuery());
            IDbDataParameter paramIsThresholdChanged = (IDbDataParameter)oCommand.Parameters["@isThresholdChanged"];
            if (!Boolean.TryParse(paramIsThresholdChanged.Value.ToString(), out isThresholdChanged))
            {
                isThresholdChanged = false;
            }
            return result;
        }



        protected System.Data.IDbCommand CreateVarianceThresholdUpdateCommand(int? reconciliationPeriodID, CompanyUnexplainedVarianceThresholdInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_CompanyUnexplainedVarianceThreshold");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;



            System.Data.IDbDataParameter parReconciliationPeriodID = cmd.CreateParameter();
            parReconciliationPeriodID.ParameterName = "@InputReconciliationPeriodID";
            if (reconciliationPeriodID.HasValue)
                parReconciliationPeriodID.Value = reconciliationPeriodID.Value;
            else
                parReconciliationPeriodID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationPeriodID);


            System.Data.IDbDataParameter parCompanyUnexplainedVarianceThreshold = cmd.CreateParameter();
            parCompanyUnexplainedVarianceThreshold.ParameterName = "@InputCompanyUnexplainedVarianceThreshold";
            if (!entity.IsCompanyUnexplainedVarianceThresholdNull)
                parCompanyUnexplainedVarianceThreshold.Value = entity.CompanyUnexplainedVarianceThreshold;
            else
                parCompanyUnexplainedVarianceThreshold.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyUnexplainedVarianceThreshold);

            System.Data.IDbDataParameter parIsThresholdChanged = cmd.CreateParameter();
            parIsThresholdChanged.ParameterName = "@isThresholdChanged";
            parIsThresholdChanged.DbType = System.Data.DbType.Boolean;
            parIsThresholdChanged.Direction = ParameterDirection.Output;
            cmdParams.Add(parIsThresholdChanged);

            return cmd;

        }



    }//end of class
}//end of namespace