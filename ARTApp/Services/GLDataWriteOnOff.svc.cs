using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.App.DAO;
using System.Data.SqlClient;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.IServices;
using System.Data;

namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "GLDataWriteOnOff" here, you must also update the reference to "GLDataWriteOnOff" in Web.config.
    public class GLDataWriteOnOff : IGLDataWriteOnOff
    {
        //TODO: remove this method if not being used anywhere
        public List<GLDataWriteOnOffInfo> GetTotalGLDataWriteOnByGLDataID(long? gLDataID, int? templateAttributeID, AppUserInfo oAppUserInfo)
        {
            List<GLDataWriteOnOffInfo> oGLDataWriteOnOffInfoCollection = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataWriteOnOffDAO oGLDataWriteOnOffDAO = new GLDataWriteOnOffDAO(oAppUserInfo);
                oGLDataWriteOnOffInfoCollection = oGLDataWriteOnOffDAO.GetTotalGLDataWriteOnByGLDataID(gLDataID, templateAttributeID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oGLDataWriteOnOffInfoCollection;
        }

        public List<GLDataWriteOnOffInfo> GetGLDataWriteOnOffInfoCollectionByGLDataID(long? gLDataID, int? templateAttributeID, AppUserInfo oAppUserInfo)
        {
            //#if DEMO
            //            List<GLDataWriteOnOffInfo> oGLDataWOCollection = new List<GLDataWriteOnOffInfo>();
            //            for (int i = 0; i <= 4; i++)
            //            {
            //                GLDataWriteOnOffInfo oGLDataWO = new GLDataWriteOnOffInfo();
            //                Random oRnd = new Random();
            //                oGLDataWO.GLDataWriteOnOffID = oRnd.Next(i, 9999);
            //                oGLDataWO.GLDataID = i;
            //                switch (i)
            //                {
            //                    case 0:
            //                        oGLDataWO.Amount = 550;
            //                        break;
            //                    case 1:
            //                        oGLDataWO.Amount = 500;
            //                        break;
            //                    case 2:
            //                        oGLDataWO.Amount = 400;
            //                        break;
            //                    case 3:
            //                        oGLDataWO.Amount = 300;
            //                        break;
            //                    case 4:
            //                        oGLDataWO.Amount = 200;
            //                        break;


            //                }

            //                oGLDataWO.Comments = "Testing Write Off/On Comments" + i.ToString();
            //                oGLDataWO.Date = DateTime.Now;
            //                oGLDataWO.DebitCredit = ((i % 2) == 0);
            //                oGLDataWO.JournalEntryRef = oRnd.Next(i, 9999);
            //                oGLDataWO.LocalCurrency = "USD";
            //                oGLDataWO.ProposedEntryAccountRef = oRnd.Next(i, 9999);
            //                oGLDataWO.ResolutionComments = "Resolved ";
            //                oGLDataWO.ResolutionDate = DateTime.Now;
            //                oGLDataWO.TransactionDate = DateTime.Now;
            //                oGLDataWO.UserID = 1;
            //                oGLDataWO.WriteOnOff = oRnd.Next(i, 9999);
            //                oGLDataWOCollection.Add(oGLDataWO);
            //                oGLDataWO = null;
            //            }
            //            return oGLDataWOCollection;
            //#else
            //            try
            //            {
            //                GLDataWriteOnOffDAO oGLWO = new GLDataWriteOnOffDAO(oAppUserInfo);
            //                oGLWO.SelectAll();    
            //            }
            //            catch(Exception ex)
            //            {
            //                throw ex;
            //            }
            //#endif
            List<GLDataWriteOnOffInfo> oGLDataWriteOnOffInfoCollection = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataWriteOnOffDAO oGLDataWriteOnOffDAO = new GLDataWriteOnOffDAO(oAppUserInfo);
                //oGLDataWriteOnOffInfoCollection = (List<GLDataWriteOnOffInfo>)oGLDataWriteOnOffDAO.SelectAllByGLDataID(gLDataID);
                oGLDataWriteOnOffInfoCollection = (List<GLDataWriteOnOffInfo>)oGLDataWriteOnOffDAO.SelectAllOpenGLDataWriteOnOffInfoItemByGLDataID(gLDataID, templateAttributeID);

            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oGLDataWriteOnOffInfoCollection;

        }

        public void InsertGLDataWriteOnOff(GLDataWriteOnOffInfo GLDataWriteOnOffInfo, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataWriteOnOffDAO oGLDataWriteOnOffDAO = new GLDataWriteOnOffDAO(oAppUserInfo);
                oGLDataWriteOnOffDAO.Save(GLDataWriteOnOffInfo);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
        }

        public void InsertGLDataWriteOnOff(GLDataWriteOnOffInfo oGLDataWriteOnOffInfo, short? recCategoryID, short? recCategoryTypeID, int recPeriodID, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataWriteOnOffDAO oGLDataWriteOnOffDAO = new GLDataWriteOnOffDAO(oAppUserInfo);
                ReconciliationPeriodDAO oReconciliationPeriodDAO = new ReconciliationPeriodDAO(oAppUserInfo);
                oConnection = oGLDataWriteOnOffDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();

                oGLDataWriteOnOffDAO.InsertGLDataWriteOnOff(oGLDataWriteOnOffInfo, recCategoryID, recCategoryTypeID, oConnection, oTransaction);
                oReconciliationPeriodDAO.UpdateRecPeriodStatusAsInProgress(recPeriodID, oConnection, oTransaction);

                oTransaction.Commit();
            }
            catch (SqlException ex)
            {
                if (oTransaction != null && oTransaction.Connection != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                if (oTransaction != null && oTransaction.Connection != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            finally
            {
                if ((oConnection != null) && (oConnection.State == ConnectionState.Open))
                {
                    oConnection.Close();
                    oConnection.Dispose();
                }
            }
        }


        public void UpdateGLDataWriteOnOff(GLDataWriteOnOffInfo GLDataWriteOnOffInfo, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataWriteOnOffDAO oGLDataWriteOnOffDAO = new GLDataWriteOnOffDAO(oAppUserInfo);
                oGLDataWriteOnOffDAO.Update(GLDataWriteOnOffInfo);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
        }

        public void UpdateGLDataWriteOnOff(GLDataWriteOnOffInfo GLDataWriteOnOffInfo, short? recCategoryTypeID, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataWriteOnOffDAO oGLDataWriteOnOffDAO = new GLDataWriteOnOffDAO(oAppUserInfo);
                oGLDataWriteOnOffDAO.UpdateGLDataWriteOnOff(GLDataWriteOnOffInfo, recCategoryTypeID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
        }


        public void DeleteGLDataWriteOnOff(long? gLDataWriteOnOffID, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataWriteOnOffDAO oGLDataWriteOnOffDAO = new GLDataWriteOnOffDAO(oAppUserInfo);
                oGLDataWriteOnOffDAO.Delete(gLDataWriteOnOffID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
        }

        public void DeleteGLDataWriteOnOff(long? gLDataWriteOnOffID, long? glDataID, short? recCategoryTypeID, string revisedBy, DateTime dateRevised, DataTable dtGLDataWO, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataWriteOnOffDAO oGLDataWriteOnOffDAO = new GLDataWriteOnOffDAO(oAppUserInfo);
                oGLDataWriteOnOffDAO.Delete(gLDataWriteOnOffID, glDataID, recCategoryTypeID, revisedBy, dateRevised,dtGLDataWO);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
        }



        public void UpdateGLDataWriteOnOffCloseDate(long glDataID
            , List<long> glGLDataWriteOnOffIDCollection
            , DateTime? closeDate
            , string closeComments
            , string journalEntryRef
            , string comments
            , short recCategoryTypeID
            , short recTemplateAttributeID
            , string revisedBy
            , DateTime? dateRevised
            , int recPeriodID
            , AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataTable dtGLDataWriteOnOffIDs = ServiceHelper.ConvertLongIDCollectionToDataTable(glGLDataWriteOnOffIDCollection);
                GLDataWriteOnOffDAO oGLDataWriteOnOffDAO = new GLDataWriteOnOffDAO(oAppUserInfo);
                ReconciliationPeriodDAO oReconciliationPeriodDAO = new ReconciliationPeriodDAO(oAppUserInfo);
                oConnection = oGLDataWriteOnOffDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();

                oGLDataWriteOnOffDAO.UpdateGLDataWriteOnOffCloseDate(glDataID, dtGLDataWriteOnOffIDs, closeDate, closeComments, journalEntryRef, comments, recCategoryTypeID, recTemplateAttributeID, revisedBy, dateRevised, oConnection, oTransaction);
                oReconciliationPeriodDAO.UpdateRecPeriodStatusAsInProgress(recPeriodID, oConnection, oTransaction);

                oTransaction.Commit();
            }
            catch (SqlException ex)
            {
                if (oTransaction != null && oTransaction.Connection != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                if (oTransaction != null && oTransaction.Connection != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            finally
            {
                if ((oConnection != null) && (oConnection.State == ConnectionState.Open))
                {
                    oConnection.Close();
                    oConnection.Dispose();
                }
            }
        }

        public List<GLDataWriteOnOffInfo> GetClosedGLDataWriteOnOffItemByMatching(long? GLDataID, long? MatchsetSubSetCombinationID, long? ExCelRowNumber, long? MatchSetMatchingSourceDataImportID, short? GLDataRecItemTypeID, AppUserInfo oAppUserInfo)
        {
            List<GLDataWriteOnOffInfo> oGLDataWriteOnOffInfoCollection = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataWriteOnOffDAO oGLDataWriteOnOffDAO = new GLDataWriteOnOffDAO(oAppUserInfo);
                oGLDataWriteOnOffInfoCollection = oGLDataWriteOnOffDAO.GetClosedGLDataWriteOnOffItemByMatching(GLDataID, MatchsetSubSetCombinationID, ExCelRowNumber, MatchSetMatchingSourceDataImportID, GLDataRecItemTypeID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oGLDataWriteOnOffInfoCollection;
        }

    }
}
