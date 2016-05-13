using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.DAO;
using SkyStem.ART.Client.IServices;
using System.Data;
using System.Data.SqlClient;
using SkyStem.ART.App.Utility;

namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "FSCaption" here, you must also update the reference to "FSCaption" in Web.config.
    public class FSCaption : IFSCaption
    {
        public void DoWork()
        {
        }

        public IList<FSCaptionHdrInfo> SelectAllFSCaptionByCompanyID(int companyID, AppUserInfo oAppUserInfo)
        {
#if DEMO
            IList<FSCaptionHdrInfo> oFSCaptionHdrInfoCollection = new List<FSCaptionHdrInfo>();
            FSCaptionHdrInfo oFSCaptionHdrInfo1 = new FSCaptionHdrInfo();
            oFSCaptionHdrInfo1.CompanyID = companyID;
            oFSCaptionHdrInfo1.FSCaption = "Demo FSCaption1";
            oFSCaptionHdrInfo1.FSCaptionID = 1;
            //oFSCaptionHdrInfo1.FSCaptionLabelID = 1;
            oFSCaptionHdrInfo1.IsActive = true;
            oFSCaptionHdrInfoCollection.Add(oFSCaptionHdrInfo1);

            FSCaptionHdrInfo oFSCaptionHdrInfo2 = new FSCaptionHdrInfo();
            oFSCaptionHdrInfo2.CompanyID = companyID;
            oFSCaptionHdrInfo2.FSCaption = "Demo FSCaption2";
            oFSCaptionHdrInfo2.FSCaptionID = 1;
            //oFSCaptionHdrInfo2.FSCaptionLabelID = 1;
            oFSCaptionHdrInfo2.IsActive = true;
            oFSCaptionHdrInfoCollection.Add(oFSCaptionHdrInfo2);

            FSCaptionHdrInfo oFSCaptionHdrInfo3 = new FSCaptionHdrInfo();
            oFSCaptionHdrInfo3.CompanyID = companyID;
            oFSCaptionHdrInfo3.FSCaption = "Demo FSCaption3";
            oFSCaptionHdrInfo3.FSCaptionID = 1;
            //oFSCaptionHdrInfo3.FSCaptionLabelID = 1;
            oFSCaptionHdrInfo3.IsActive = true;
            oFSCaptionHdrInfoCollection.Add(oFSCaptionHdrInfo3);
            return oFSCaptionHdrInfoCollection;
#else
             ServiceHelper.SetConnectionString(oAppUserInfo);
            IList<FSCaptionHdrInfo> oFSCaptionHdrInfoCollection;
            FSCaptionHdrDAO oFSCaptionHdrDAO = new FSCaptionHdrDAO(oAppUserInfo);
            return oFSCaptionHdrInfoCollection = oFSCaptionHdrDAO.SelectAllByCompanyID(companyID);
#endif

        }

        public IList<FSCaptionInfo_ExtendedWithMaterialityInfo> SelectAllFSCaptionMergeMaterilityByReconciliationPeriodID(int? reconciliationPeriodID, AppUserInfo oAppUserInfo)
        {

            //#if DEMO
            //            IList<FSCaptionInfo_ExtendedWithMaterialityInfo> lstFSCaptionInfo_ExtendedWithMaterialityInfo = new List<FSCaptionInfo_ExtendedWithMaterialityInfo>();
            //            FSCaptionInfo_ExtendedWithMaterialityInfo oFSCaptionInfo_ExtendedWithMaterialityInfo1 = new FSCaptionInfo_ExtendedWithMaterialityInfo();
            //            oFSCaptionInfo_ExtendedWithMaterialityInfo1.CompanyID = companyID;
            //            oFSCaptionInfo_ExtendedWithMaterialityInfo1.FSCaption = "Demo FSCaption1";
            //            oFSCaptionInfo_ExtendedWithMaterialityInfo1.FSCaptionID = 1;
            //            //oFSCaptionInfo_ExtendedWithMaterialityInfo1.FSCaptionLabelID = 1;
            //            oFSCaptionInfo_ExtendedWithMaterialityInfo1.MaterialityThreshold = 100;
            //            oFSCaptionInfo_ExtendedWithMaterialityInfo1.IsActive = true;
            //            lstFSCaptionInfo_ExtendedWithMaterialityInfo.Add(oFSCaptionInfo_ExtendedWithMaterialityInfo1);

            //            FSCaptionInfo_ExtendedWithMaterialityInfo oFSCaptionInfo_ExtendedWithMaterialityInfo2 = new FSCaptionInfo_ExtendedWithMaterialityInfo();
            //            oFSCaptionInfo_ExtendedWithMaterialityInfo2.CompanyID = companyID;
            //            oFSCaptionInfo_ExtendedWithMaterialityInfo2.FSCaption = "Demo FSCaption1";
            //            oFSCaptionInfo_ExtendedWithMaterialityInfo2.FSCaptionID = 2;
            //            //oFSCaptionHdrInfo2.FSCaptionLabelID = 1;
            //            oFSCaptionInfo_ExtendedWithMaterialityInfo2.MaterialityThreshold = 200;
            //            oFSCaptionInfo_ExtendedWithMaterialityInfo2.IsActive = true;
            //            lstFSCaptionInfo_ExtendedWithMaterialityInfo.Add(oFSCaptionInfo_ExtendedWithMaterialityInfo2);

            //            FSCaptionInfo_ExtendedWithMaterialityInfo oFSCaptionInfo_ExtendedWithMaterialityInfo3 = new FSCaptionInfo_ExtendedWithMaterialityInfo();
            //            oFSCaptionInfo_ExtendedWithMaterialityInfo3.CompanyID = companyID;
            //            oFSCaptionInfo_ExtendedWithMaterialityInfo3.FSCaption = "Demo FSCaption1";
            //            oFSCaptionInfo_ExtendedWithMaterialityInfo3.FSCaptionID = 3;
            //            //oFSCaptionHdrInfo3.FSCaptionLabelID = 1;
            //            oFSCaptionInfo_ExtendedWithMaterialityInfo3.MaterialityThreshold = 300;
            //            oFSCaptionInfo_ExtendedWithMaterialityInfo3.IsActive = true;
            //            lstFSCaptionInfo_ExtendedWithMaterialityInfo.Add(oFSCaptionInfo_ExtendedWithMaterialityInfo3);
            //            return lstFSCaptionInfo_ExtendedWithMaterialityInfo;
            //#else

            //            IList<FSCaptionInfo_ExtendedWithMaterialityInfo> oFSCaptionWithMaterialityInfoCollection;
            //            FSCaptionMaterialityDAO oFSCaptionMaterialityDAO = new FSCaptionMaterialityDAO(oAppUserInfo);
            //            oFSCaptionWithMaterialityInfoCollection = oFSCaptionMaterialityDAO.GetAllMaterilityWithFSCaptionByCompanyID(companyID);
            //            return oFSCaptionWithMaterialityInfoCollection;
            //#endif
            IList<FSCaptionInfo_ExtendedWithMaterialityInfo> oFSCaptionWithMaterialityInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                FSCaptionMaterialityDAO oFSCaptionMaterialityDAO = new FSCaptionMaterialityDAO(oAppUserInfo);
                oFSCaptionWithMaterialityInfoCollection = oFSCaptionMaterialityDAO.GetAllMaterilityWithFSCaptionByReconciliationPeriodID(reconciliationPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oFSCaptionWithMaterialityInfoCollection;

        }

        //public int SaveMaterilityByFSCaptionTableValue(int? reconciliationPeriodID,IList<FSCaptionMaterialityInfo> oFSCaptionMaterialityInfoCollection)
        //{
        //    FSCaptionMaterialityDAO oFSCaptionMaterialityDAO = new FSCaptionMaterialityDAO(oAppUserInfo);
        //    return oFSCaptionMaterialityDAO.InsertFSCaptionMaterialityByTableValueCommand(reconciliationPeriodID,oFSCaptionMaterialityInfoCollection);
        //}

        //Not exposed by interface
        //public int SaveMaterilityByFSCaptionTableValue(int? reconciliationPeriodID, IList<FSCaptionMaterialityInfo> oFSCaptionMaterialityInfoCollection, IDbConnection oConnection, IDbTransaction oTransaction)
        //{
        //    int intReturn = 0;
        //    try
        //    {
        //        FSCaptionMaterialityDAO oFSCaptionMaterialityDAO = new FSCaptionMaterialityDAO(oAppUserInfo);
        //        intReturn = oFSCaptionMaterialityDAO.InsertFSCaptionMaterialityByTableValueCommand(reconciliationPeriodID, oFSCaptionMaterialityInfoCollection, oConnection, oTransaction);
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
        //    return intReturn;
        //}

        /// <summary>
        /// This method is used to auto populate FS Caption text box based on the basis of 
        /// the prefix text typed in the text box
        /// </summary>
        /// <param name="companyId">Company id</param>
        /// <param name="prefixText">The text which was typed in the text box</param>
        /// <param name="count">Number of results to be returned</param>
        /// <returns>List of FS Caption</returns>
        public string[] SelectFSCaptionByCompanyIDAndPrefixText(int companyId, string prefixText, int count, int LCID, int businessEntityID, int defaultLCID, AppUserInfo oAppUserInfo)
        {
            string[] oFSCaptionCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                FSCaptionHdrDAO oFSCaptionHdrDAO = new FSCaptionHdrDAO(oAppUserInfo);
                oFSCaptionCollection = oFSCaptionHdrDAO.SelectFSCaptionByCompanyIDAndPrefixText(companyId, prefixText, count, LCID, businessEntityID, defaultLCID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oFSCaptionCollection;
        }

    }//end of class
}//end of namespace
