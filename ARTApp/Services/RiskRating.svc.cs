using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.App.DAO;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.IServices;
using System.Data;
using System.Data.SqlClient;
using SkyStem.ART.App.Utility;

namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "RiskRating" here, you must also update the reference to "RiskRating" in Web.config.
    public class RiskRating : IRiskRating
    {
        public void DoWork()
        {
        }

        public List<RiskRatingReconciliationPeriodInfo> SelectAllRiskRatingReconciliationPeriodByRiskRatingIDAndReconciliationPeriodID(int? reconciliationPeriodID, short? riskRatingID, AppUserInfo oAppUserInfo)
        {
            List<RiskRatingReconciliationPeriodInfo> oRiskRatingReconciliationPeriodInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                RiskRatingReconciliationPeriodDAO oRiskRatingReconciliationPeriodDAO = new RiskRatingReconciliationPeriodDAO(oAppUserInfo);
                oRiskRatingReconciliationPeriodInfoCollection = oRiskRatingReconciliationPeriodDAO.SelectAllRiskRatingReconciliationPeriodByRiskRatingIDAndReconciliationPeriodID(reconciliationPeriodID, riskRatingID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oRiskRatingReconciliationPeriodInfoCollection;
        }


        public IList<RiskRatingReconciliationFrequencyInfo> SelectAllRiskRatingReconciliationFrequencyByReconciliationPeriodID(int? reconciliationPeriodID, AppUserInfo oAppUserInfo)
        {
            //#if DEMO
            //            IList<RiskRatingReconciliationFrequencyInfo> oRiskRatingReconciliationFrequencyInfoCollection = new List<RiskRatingReconciliationFrequencyInfo>();
            //            RiskRatingReconciliationFrequencyInfo oRiskRatingReconciliationFrequencyInfo1 = new RiskRatingReconciliationFrequencyInfo();
            //            oRiskRatingReconciliationFrequencyInfo1.RiskRatingReconciliationFrequencyID = 1;
            //            oRiskRatingReconciliationFrequencyInfo1.RiskRatingID = 1;
            //            oRiskRatingReconciliationFrequencyInfo1.ReconciliationFrequencyID = 1;
            //            oRiskRatingReconciliationFrequencyInfo1.CompanyID = companyID;
            //            oRiskRatingReconciliationFrequencyInfoCollection.Add(oRiskRatingReconciliationFrequencyInfo1);

            //            RiskRatingReconciliationFrequencyInfo oRiskRatingReconciliationFrequencyInfo2 = new RiskRatingReconciliationFrequencyInfo();
            //            oRiskRatingReconciliationFrequencyInfo2.RiskRatingReconciliationFrequencyID = 2;
            //            oRiskRatingReconciliationFrequencyInfo2.RiskRatingID = 2;
            //            oRiskRatingReconciliationFrequencyInfo2.ReconciliationFrequencyID = 2;
            //            oRiskRatingReconciliationFrequencyInfo2.CompanyID = companyID;
            //            oRiskRatingReconciliationFrequencyInfoCollection.Add(oRiskRatingReconciliationFrequencyInfo2);

            //            RiskRatingReconciliationFrequencyInfo oRiskRatingReconciliationFrequencyInfo3 = new RiskRatingReconciliationFrequencyInfo();
            //            oRiskRatingReconciliationFrequencyInfo3.RiskRatingReconciliationFrequencyID = 3;
            //            oRiskRatingReconciliationFrequencyInfo3.RiskRatingID = 3;
            //            oRiskRatingReconciliationFrequencyInfo3.ReconciliationFrequencyID = 4;//custom
            //            oRiskRatingReconciliationFrequencyInfo3.CompanyID = companyID;
            //            oRiskRatingReconciliationFrequencyInfoCollection.Add(oRiskRatingReconciliationFrequencyInfo3);

            //            return oRiskRatingReconciliationFrequencyInfoCollection;
            //#else
            //            RiskRatingReconciliationFrequencyDAO oRiskRatingReconciliationFrequencyDAO = new RiskRatingReconciliationFrequencyDAO(oAppUserInfo);
            //            return  oRiskRatingReconciliationFrequencyDAO.SelectAllByCompanyID(companyID);
            //#endif
            IList<RiskRatingReconciliationFrequencyInfo> oRiskRatingReconciliationFrequencyInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                RiskRatingReconciliationFrequencyDAO oRiskRatingReconciliationFrequencyDAO = new RiskRatingReconciliationFrequencyDAO(oAppUserInfo);
                //return oRiskRatingReconciliationFrequencyDAO.SelectAllByCompanyID(companyID);
                oRiskRatingReconciliationFrequencyInfoCollection = oRiskRatingReconciliationFrequencyDAO.SelectAllRiskRatingReconciliationFrequencyByReconciliationPeriodID(reconciliationPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oRiskRatingReconciliationFrequencyInfoCollection;
        }

        //public int SaveRiskRatingReconciliationFrequencyTableValue(IList<RiskRatingReconciliationFrequencyInfo> oRiskRatingReconciliationFrequencyInfoCollection, int? reconciliationPeriodID)
        //{
        //    RiskRatingReconciliationFrequencyDAO oRiskRatingReconciliationFrequencyDAO = new RiskRatingReconciliationFrequencyDAO(oAppUserInfo);
        //    return oRiskRatingReconciliationFrequencyDAO.SaveRiskRatingReconciliationFrequencyTableValue(oRiskRatingReconciliationFrequencyInfoCollection, reconciliationPeriodID);
        //}

        ////Not exposed by interface
        //public int SaveRiskRatingReconciliationFrequencyTableValue(IList<RiskRatingReconciliationFrequencyInfo> oRiskRatingReconciliationFrequencyInfoCollection, int? reconciliationPeriodID, IDbConnection oConnection, IDbTransaction oTransaction)
        //{
        //    int rowsAffected = 0;
        //    try
        //    {
        //        RiskRatingReconciliationFrequencyDAO oRiskRatingReconciliationFrequencyDAO = new RiskRatingReconciliationFrequencyDAO(oAppUserInfo);
        //        rowsAffected = oRiskRatingReconciliationFrequencyDAO.SaveRiskRatingReconciliationFrequencyTableValue(oRiskRatingReconciliationFrequencyInfoCollection, reconciliationPeriodID, oConnection, oTransaction);
        //    }
        //    catch (SqlException ex)
        //    {
        //        if (oTransaction != null && oConnection.State != ConnectionState.Closed)
        //            oTransaction.Rollback();
        //        ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (oTransaction != null && oConnection.State != ConnectionState.Closed)
        //            oTransaction.Rollback();
        //        ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
        //    }
        //    finally
        //    {
        //        if (oConnection != null && oConnection.State != ConnectionState.Closed)
        //            oConnection.Dispose();
        //    }
        //    return rowsAffected;
        //}

        /// <summary>
        /// Selects all risk rating defined for the company
        /// </summary>
        /// <returns>List of risk rating</returns>
        public List<RiskRatingMstInfo> SelectAllRiskRatingMstInfo( AppUserInfo oAppUserInfo)
        {
            List<RiskRatingMstInfo> oRiskRatingMstInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                RiskRatingMstDAO oRiskRatingMstDAO = new RiskRatingMstDAO(oAppUserInfo);
                oRiskRatingMstInfoCollection= (List<RiskRatingMstInfo>)oRiskRatingMstDAO.SelectAllRiskRatingMstInfo();
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oRiskRatingMstInfoCollection;
        }

    }//end of class
}//end of namespace
