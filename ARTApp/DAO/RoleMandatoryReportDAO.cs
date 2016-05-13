


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
    public class RoleMandatoryReportDAO : RoleMandatoryReportDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RoleMandatoryReportDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public IList<RoleMandatoryReportInfo> SelectAllRoleMandatoryReportInfoByReconciliationPeriodID(int? reconciliationPeriodID)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_MandatoryReportByRecPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            //System.Data.IDbDataParameter par = cmd.CreateParameter();
            //par.ParameterName = "@CompanyID";
            //par.Value = id;
            //cmdParams.Add(par);
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@InputReconciliationPeriodID";
            par.Value = reconciliationPeriodID;
            cmdParams.Add(par);

            return this.Select(cmd);
        }

        //public int InsertRoleMandatoryReportByTableValue(IList<RoleMandatoryReportInfo> lstRoleMandatoryReportInfo, int? reconciliationPeriodID, bool? isDualReview)
        //{
        //    IDbConnection oConnection = null;
        //    IDbTransaction oTransaction = null;
        //    int intResult = 0;
        //    try
        //    {
        //        DataTable dtRoleMandatoryReportInfo = ServiceHelper.ConvertIDListToDataTable_RoleMandatoryReportInfo(lstRoleMandatoryReportInfo);
        //        IDbCommand oCommand = CreateInsertCommandInfoTableValue(dtRoleMandatoryReportInfo,reconciliationPeriodID,isDualReview);

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

        public int InsertRoleMandatoryReportByTableValue(IList<RoleMandatoryReportInfo> oRoleMandatoryReportInfoCollection, int? reconciliationPeriodID, bool isDualReview, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            int intResult = 0;
            //try
            //{
            DataTable dtRoleMandatoryReportInfo = ServiceHelper.ConvertRoleMandatoryReportInfoCollectionToDataTable(oRoleMandatoryReportInfoCollection);
            IDbCommand oCommand = CreateInsertCommandInfoTableValue(dtRoleMandatoryReportInfo, reconciliationPeriodID, isDualReview);

            //oConnection = CreateConnection();
            //oConnection.Open();
            oCommand.Connection = oConnection;
            //oTransaction = oConnection.BeginTransaction();
            oCommand.Transaction = oTransaction;
            intResult = oCommand.ExecuteNonQuery();
            //oTransaction.Commit();
            //}
            //catch (Exception ex)
            //{
            //    if ((oTransaction != null) && (oConnection.State == ConnectionState.Open))
            //    {
            //        oTransaction.Rollback();
            //    }
            //    throw ex;
            //}
            //finally
            //{
            //    try
            //    {
            //        if (oConnection != null)
            //            oConnection.Close();
            //    }
            //    catch (Exception)
            //    {
            //    }
            //}
            return intResult;
        }





        protected System.Data.IDbCommand CreateInsertCommandInfoTableValue(DataTable dtRoleMandatoryReportInfo, int? reconciliationPeriodID, bool isDualReview)
        {
            //System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_RoleMandatoryReportInfoTableValue");//TODO: create sp
            System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_MandatoryReportCapability");//TODO: create sp
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            //System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            //parCompanyID.ParameterName = "@CompanyID";
            ////if (!entity.IsCompanyIDNull)
            //    parCompanyID.Value = companyID;
            ////else
            ////    parCompanyID.Value = System.DBNull.Value;
            //cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parRoleMandatoryReportID = cmd.CreateParameter();
            parRoleMandatoryReportID.ParameterName = "@tblRoleMandatoryReportInfo";
            //if (!entity.IsReportIDNull)
            parRoleMandatoryReportID.Value = dtRoleMandatoryReportInfo;
            //else
            //    parRoleMandatoryReportID.Value = System.DBNull.Value;
            cmdParams.Add(parRoleMandatoryReportID);


            System.Data.IDbDataParameter parReconciliationPeriodID = cmd.CreateParameter();
            parReconciliationPeriodID.ParameterName = "@InputReconciliationPeriodID";
            parReconciliationPeriodID.Value = reconciliationPeriodID;
            cmdParams.Add(parReconciliationPeriodID);


            System.Data.IDbDataParameter parIsDualReview = cmd.CreateParameter();
            parIsDualReview.ParameterName = "@IsDualReview";
            parIsDualReview.Value = isDualReview;
            cmdParams.Add(parIsDualReview);

            return cmd;
        }

    }//end of class
}//end of namespace