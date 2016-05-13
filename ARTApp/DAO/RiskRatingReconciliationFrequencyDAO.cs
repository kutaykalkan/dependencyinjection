


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.App.Utility;

namespace SkyStem.ART.App.DAO
{
    public class RiskRatingReconciliationFrequencyDAO : RiskRatingReconciliationFrequencyDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RiskRatingReconciliationFrequencyDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }


        public IList<RiskRatingReconciliationFrequencyInfo> SelectAllRiskRatingReconciliationFrequencyByReconciliationPeriodID(int? reconciliationPeriodID)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_RiskRatingReconciliationFrequencyByRecPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationPeriodID";
            par.Value = reconciliationPeriodID;
            cmdParams.Add(par);

            return this.Select(cmd);
        }

        //DO NOT DELETE THIS COMENETED CODE FOR NOW
        //public int SaveRiskRatingReconciliationFrequencyTableValue(IList<RiskRatingReconciliationFrequencyInfo> oRiskRatingReconciliationFrequencyInfoCollection, int? reconciliationPeriodID)
        //{
        //    IDbConnection oConnection = null;
        //    IDbTransaction oTransaction = null;
        //    int intResult = 0;
        //    try
        //    {
        //        DataTable dt = ServiceHelper.ConvertRiskRatingReconciliationFrequencyInfoCollectionToDataTable(oRiskRatingReconciliationFrequencyInfoCollection);
        //        IDbCommand oCommand = CreateInsertCommandTableValue(dt, reconciliationPeriodID);
        //        oConnection = CreateConnection();
        //        oConnection.Open();
        //        oCommand.Connection = oConnection;
        //        oTransaction = oConnection.BeginTransaction();
        //        oCommand.Transaction = oTransaction;
        //        intResult = oCommand.ExecuteNonQuery();
        //        oTransaction.Commit();
        //    }
        //    catch (Exception ex)
        //    {
        //        if ((oTransaction != null) && (oConnection.State == ConnectionState.Open))
        //        {
        //            oTransaction.Rollback();
        //        }
        //        throw ex;
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            if (oConnection != null)
        //                oConnection.Close();
        //        }
        //        catch (Exception)
        //        {
        //        }
        //    }
        //    return intResult;
        //}

        public int SaveRiskRatingReconciliationFrequencyAndPeriodTableValue(IList<RiskRatingReconciliationFrequencyInfo> oRiskRatingReconciliationFrequencyInfoCollection, IList<RiskRatingReconciliationPeriodInfo> oRiskRatingReconciliationPeriodInfoCollection, int? reconciliationPeriodID, DateTime dateRevised , string revisedBy, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            int intResult = 0;
            //try
            //{
            DataTable dtRiskRatingReconciliationFrequencyInfoCollection = ServiceHelper.ConvertRiskRatingReconciliationFrequencyInfoCollectionToDataTable(oRiskRatingReconciliationFrequencyInfoCollection);
            DataTable dtRiskRatingReconciliationPeriodInfoCollection = ServiceHelper.ConvertRiskRatingReconciliationPeriodInfoCollectionToDataTable(oRiskRatingReconciliationPeriodInfoCollection);
            IDbCommand oCommand = CreateInsertCommandTableValue(dtRiskRatingReconciliationFrequencyInfoCollection, dtRiskRatingReconciliationPeriodInfoCollection, reconciliationPeriodID,dateRevised , revisedBy);

            oCommand.Connection = oConnection;

            oCommand.Transaction = oTransaction;
            intResult = oCommand.ExecuteNonQuery();
            return intResult;
        }



        protected System.Data.IDbCommand CreateInsertCommandTableValue(DataTable dtRiskRatingReconciliationFrequencyInfoCollection, DataTable dtRiskRatingReconciliationPeriodInfoCollection, int? reconciliationPeriodID, DateTime dateRevised, string revisedBy)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_RiskRatingCapability");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parReconciliationPeriodID = cmd.CreateParameter();
            parReconciliationPeriodID.ParameterName = "@ReconciliationPeriodID";
            //if (!entity.IsCompanyIDNull)
            parReconciliationPeriodID.Value = reconciliationPeriodID;
            //else
            //    parCompanyID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationPeriodID);

            System.Data.IDbDataParameter parReconciliationFrequencyIDTable = cmd.CreateParameter();
            parReconciliationFrequencyIDTable.ParameterName = "@tblRiskRatingReconciliationFrequency";
            //if (!entity.IsReconciliationFrequencyIDNull)
            parReconciliationFrequencyIDTable.Value = dtRiskRatingReconciliationFrequencyInfoCollection;
            //else
            //    parReconciliationFrequencyID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationFrequencyIDTable);


            System.Data.IDbDataParameter parReconciliationPeriodIDTable = cmd.CreateParameter();
            parReconciliationPeriodIDTable.ParameterName = "@tblRiskRatingReconciliationPeriodInfo";
            //if (!entity.IsReconciliationFrequencyIDNull)
            parReconciliationPeriodIDTable.Value = dtRiskRatingReconciliationPeriodInfoCollection;
            //else
            //    parReconciliationFrequencyID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationPeriodIDTable);

            IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            parRevisedBy.Value = revisedBy;
            cmdParams.Add(parRevisedBy);

            IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            parDateRevised.Value = dateRevised;
            cmdParams.Add(parDateRevised);

            return cmd;
        }



    }//end of class
}//end of namespace