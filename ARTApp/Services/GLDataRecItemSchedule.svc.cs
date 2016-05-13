using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.DAO;
using System.Data.SqlClient;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.IServices;
using System.Data;

namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "GLDataRecItemSchedule" here, you must also update the reference to "GLDataRecItemSchedule" in Web.config.
    public class GLDataRecItemSchedule : IGLDataRecItemSchedule
    {
        //public List<GLDataRecurringItemScheduleInfo> GetRecItemRecurringSchedule(long accountID, int recPeriodID, short recCategoryTypeID, short glReconciliationItemInputRecordTypeID, short accountTemplateAttributeID)
        //{
        //    List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection = null;

        //    try
        //    {
        //        GLDataRecItemDAO oGLDataRecItemDAO = new GLDataRecItemDAO(oAppUserInfo);
        //        oGLDataRecurringItemScheduleInfoCollection = oGLDataRecItemDAO.GetRecItemRecurringSchedule(accountID, recPeriodID, recCategoryTypeID, glReconciliationItemInputRecordTypeID, accountTemplateAttributeID);
        //    }
        //    catch (SqlException ex)
        //    {
        //        ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
        //    }
        //    catch (Exception ex)
        //    {
        //        ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
        //    }

        //    return oGLDataRecurringItemScheduleInfoCollection;
        //}


        //public List<GLDataRecurringItemScheduleInfo> GetRecItemSchedule(long accountID, int recPeriodID, short recCategoryTypeID, short glReconciliationItemInputRecordTypeID, short accountTemplateAttributeID)
        public List<GLDataRecurringItemScheduleInfo> GetGLDataRecurringItemSchedule(long? gLDataID, AppUserInfo oAppUserInfo)
        {
            List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataRecurringItemScheduleDAO oGLDataRecurringItemScheduleDAO = new GLDataRecurringItemScheduleDAO(oAppUserInfo);
                //oGLDataRecurringItemScheduleInfoCollection = oGLDataRecurringItemScheduleDAO.SelectAllByGLDataID(gLDataID);
                oGLDataRecurringItemScheduleInfoCollection = oGLDataRecurringItemScheduleDAO.GetGLDataRecurringItemSchedule(gLDataID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oGLDataRecurringItemScheduleInfoCollection;
        }

        public void InsertGLDataRecurringItemSchedule(GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo, int recPeriodID, List<AttachmentInfo> oAttachmentInfoCollection, AppUserInfo oAppUserInfo)
        {
            long gLDataRecurringItemScheduleID = 0;
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataRecurringItemScheduleDAO oGLDataRecurringItemScheduleDAO = new GLDataRecurringItemScheduleDAO(oAppUserInfo);
                ReconciliationPeriodDAO oReconciliationPeriodDAO = new ReconciliationPeriodDAO(oAppUserInfo);
                oConnection = oGLDataRecurringItemScheduleDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();

                //oGLDataRecurringItemScheduleDAO.Save(oGLDataRecurringItemScheduleInfo, oConnection, oTransaction);
                gLDataRecurringItemScheduleID = oGLDataRecurringItemScheduleDAO.InsertGLDataRecurringItemSchedule(oGLDataRecurringItemScheduleInfo, oConnection, oTransaction);
                oReconciliationPeriodDAO.UpdateRecPeriodStatusAsInProgress(recPeriodID, oConnection, oTransaction);

                if (oAttachmentInfoCollection != null && oAttachmentInfoCollection.Count > 0)
                {
                    Array.ForEach(oAttachmentInfoCollection.ToArray(), a => a.RecordID = gLDataRecurringItemScheduleID);
                    DataTable dtAttachment = ServiceHelper.ConvertAttachmentInfoCollectionToDataTable(oAttachmentInfoCollection);
                    AttachmentDAO oAttachmentDAO = new AttachmentDAO(oAppUserInfo);
                    oAttachmentDAO.InsertAttachmentBulk(dtAttachment, recPeriodID, oGLDataRecurringItemScheduleInfo.AddedBy, oGLDataRecurringItemScheduleInfo.DateAdded, oConnection, oTransaction);
                }

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


        public void UpdateGLDataRecurringItemSchedule(GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo, short recTemplateAttributeID, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataRecurringItemScheduleDAO oGLDataRecurringItemScheduleDAO = new GLDataRecurringItemScheduleDAO(oAppUserInfo);
                oConnection = oGLDataRecurringItemScheduleDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();

                oGLDataRecurringItemScheduleDAO.UpdateGLDataRecurringItemSchedule(oGLDataRecurringItemScheduleInfo, recTemplateAttributeID, oConnection, oTransaction);

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


        public void UpdateGLDataRecurringItemScheduleCloseDate(long glDataID, List<long> glRecItemInputIDCollection, DateTime? closeDate, string closeComments, string journalEntryRef, string comments, short recCategoryTypeID, short recTemplateAttributeID, string revisedBy, DateTime? dateRevised, int recPeriodID, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataTable dtGLRecItemInputIDs = ServiceHelper.ConvertLongIDCollectionToDataTable(glRecItemInputIDCollection);
                GLDataRecurringItemScheduleDAO oGLDataRecurringItemScheduleDAO = new GLDataRecurringItemScheduleDAO(oAppUserInfo);
                ReconciliationPeriodDAO oReconciliationPeriodDAO = new ReconciliationPeriodDAO(oAppUserInfo);
                oConnection = oGLDataRecurringItemScheduleDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();

                oGLDataRecurringItemScheduleDAO.UpdateGLDataRecurringItemScheduleCloseDate(glDataID, dtGLRecItemInputIDs, closeDate, closeComments, journalEntryRef, comments, recCategoryTypeID, recTemplateAttributeID, revisedBy, dateRevised, oConnection, oTransaction);
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


        public void DeleteGLDataRecurringItemSchedule(GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo, short recTemplateAttributeID, DataTable dtGLdataRecurringScheduleIDs, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataRecurringItemScheduleDAO oGLDataRecurringItemScheduleDAO = new GLDataRecurringItemScheduleDAO(oAppUserInfo);
                oConnection = oGLDataRecurringItemScheduleDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();

                oGLDataRecurringItemScheduleDAO.DeleteGLDataRecurringItemSchedule(oGLDataRecurringItemScheduleInfo, recTemplateAttributeID, dtGLdataRecurringScheduleIDs, oConnection, oTransaction);

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




        #region IGLDataRecItemSchedule Members


        public GLDataRecurringItemScheduleInfo GetGLDataRecurringItemScheduleInfo(long? GLDataRecurringItemScheduleID, AppUserInfo oAppUserInfo)
        {
            GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataRecurringItemScheduleDAO oGLDataRecurringItemScheduleDAO = new GLDataRecurringItemScheduleDAO(oAppUserInfo);
                oGLDataRecurringItemScheduleInfo = oGLDataRecurringItemScheduleDAO.GetGLDataRecurringItemScheduleInfo(GLDataRecurringItemScheduleID);

                GLDataRecurringItemScheduleIntervalDetailDAO oGLDataRecurringItemScheduleIntervalDetailDAO = new GLDataRecurringItemScheduleIntervalDetailDAO(oAppUserInfo);
                oGLDataRecurringItemScheduleInfo.GLDataRecurringItemScheduleIntervalDetailInfoList = oGLDataRecurringItemScheduleIntervalDetailDAO.SelectRecurringItemScheduleIntervalDetailRecurringItemScheduleID(GLDataRecurringItemScheduleID);

            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oGLDataRecurringItemScheduleInfo;
        }

        #endregion

        public List<GLDataRecurringItemScheduleInfo> GetClosedGLDataRecurringItemSchedule(long? GlDataID, long? MatchsetSubSetCombinationID, long? ExCelRowNumber, long? MatchSetMatchingSourceDataImportID, short? GLDataRecItemTypeID, AppUserInfo oAppUserInfo)
        {
            List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataRecurringItemScheduleDAO oGLDataRecurringItemScheduleDAO = new GLDataRecurringItemScheduleDAO(oAppUserInfo);
                oGLDataRecurringItemScheduleInfoCollection = oGLDataRecurringItemScheduleDAO.GetClosedGLDataRecurringItemSchedule(GlDataID, MatchsetSubSetCombinationID, ExCelRowNumber, MatchSetMatchingSourceDataImportID, GLDataRecItemTypeID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oGLDataRecurringItemScheduleInfoCollection;
        }
        public List<AttachmentInfo> CopyRecurringItemSchedule(GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo, DataTable dtGLDataParams, int? recPeriodID, bool CloseSourceRecItem, AppUserInfo oAppUserInfo)
        {
            List<AttachmentInfo> oAttachmentInfoList = null;
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataRecurringItemScheduleDAO oGLDataRecurringItemScheduleDAO = new GLDataRecurringItemScheduleDAO(oAppUserInfo);
                oConnection = oGLDataRecurringItemScheduleDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();

                oAttachmentInfoList = oGLDataRecurringItemScheduleDAO.CopyRecurringItemSchedule(oGLDataRecurringItemScheduleInfo, dtGLDataParams, recPeriodID, CloseSourceRecItem, oConnection, oTransaction);

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

            return oAttachmentInfoList;
        }
    }//end of class
}//end of namespace
