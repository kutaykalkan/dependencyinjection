


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
    public class DerivedCalculationSupportingDetailDAO : DerivedCalculationSupportingDetailDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DerivedCalculationSupportingDetailDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        public List<DerivedCalculationSupportingDetailInfo> SelectAllDerivedCalculationSupportingDetailInfoByGLDataID(long? glDataID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_DerivedCalculationSupportingDetailByGLDataID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataID";
            par.Value = glDataID;
            cmdParams.Add(par);

            return this.Select(cmd);
        }



        public int InsertDerivedCalculationSupportingDetail(DerivedCalculationSupportingDetailInfo oDerivedCalculationSupportingDetailInfo, short? reconciliationCategoryTypeIDForSupportingDetail, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            int intResult = 0;
            IDbCommand oCommand = CreateInsertDerivedCalculationSupportingDetailCommand(oDerivedCalculationSupportingDetailInfo, reconciliationCategoryTypeIDForSupportingDetail);
            oCommand.Connection = oConnection;
            oCommand.Transaction = oTransaction;
            intResult = oCommand.ExecuteNonQuery();
            return intResult;
        }

        private IDbCommand CreateInsertDerivedCalculationSupportingDetailCommand(DerivedCalculationSupportingDetailInfo entity, short? reconciliationCategoryTypeIDForSupportingDetail)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_DerivedCalculationSupportingDetail");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parBaseCurrencyBalance = cmd.CreateParameter();
            parBaseCurrencyBalance.ParameterName = "@BaseCurrencyBalance";
            if (!entity.IsBaseCurrencyBalanceNull)
                parBaseCurrencyBalance.Value = entity.BaseCurrencyBalance;
            else
                parBaseCurrencyBalance.Value = System.DBNull.Value;
            cmdParams.Add(parBaseCurrencyBalance);

            System.Data.IDbDataParameter parBasisForDerivedCalculation = cmd.CreateParameter();
            parBasisForDerivedCalculation.ParameterName = "@BasisForDerivedCalculation";
            if (!entity.IsBasisForDerivedCalculationNull)
                parBasisForDerivedCalculation.Value = entity.BasisForDerivedCalculation;
            else
                parBasisForDerivedCalculation.Value = System.DBNull.Value;
            cmdParams.Add(parBasisForDerivedCalculation);

            System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            if (!entity.IsGLDataIDNull)
                parGLDataID.Value = entity.GLDataID;
            else
                parGLDataID.Value = System.DBNull.Value;
            cmdParams.Add(parGLDataID);

            System.Data.IDbDataParameter parReportingCurrencyBalance = cmd.CreateParameter();
            parReportingCurrencyBalance.ParameterName = "@ReportingCurrencyBalance";
            if (!entity.IsReportingCurrencyBalanceNull)
                parReportingCurrencyBalance.Value = entity.ReportingCurrencyBalance;
            else
                parReportingCurrencyBalance.Value = System.DBNull.Value;
            cmdParams.Add(parReportingCurrencyBalance);


            System.Data.IDbDataParameter parReconciliationCategoryTypeIDForSupportingDetail = cmd.CreateParameter();
            parReconciliationCategoryTypeIDForSupportingDetail.ParameterName = "@ReconciliationCategoryTypeIDForSupportingDetail";
            if (reconciliationCategoryTypeIDForSupportingDetail.HasValue)
                parReconciliationCategoryTypeIDForSupportingDetail.Value = reconciliationCategoryTypeIDForSupportingDetail;
            else
                parReconciliationCategoryTypeIDForSupportingDetail.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationCategoryTypeIDForSupportingDetail);

            IDbDataParameter paramRevisedBy = cmd.CreateParameter();
            paramRevisedBy.ParameterName = "@RevisedBy";
            paramRevisedBy.Value = entity.RevisedBy;
            cmdParams.Add(paramRevisedBy);

            IDbDataParameter paramDateRevised = cmd.CreateParameter();
            paramDateRevised.ParameterName = "@DateRevised";
            paramDateRevised.Value = entity.DateRevised;
            cmdParams.Add(paramDateRevised);

            return cmd;

        }



    }//end of class
}//end of namespace