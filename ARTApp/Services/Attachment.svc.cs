using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.DAO;
using System.Data.SqlClient;
using SkyStem.ART.App.Utility;
using System.Data;
namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "Attachment" here, you must also update the reference to "Attachment" in Web.config.
    public class Attachment : IAttachment
    {
        public void DoWork()
        {
        }
        public List<AttachmentInfo> GetAttachment(long RecordID, int RecordTypeID, int? RecPeriodID, AppUserInfo oAppUserInfo)
        {
            List<AttachmentInfo> oAttachmentInfoCollection = new List<AttachmentInfo>();
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                AttachmentDAO oAttachmentDAO = new AttachmentDAO(oAppUserInfo);
                oAttachmentInfoCollection = oAttachmentDAO.GetAttachmentByRecordIDandRecordTypeID(RecordID, RecordTypeID, RecPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oAttachmentInfoCollection;

        }



        public void InsertAttachment(AttachmentInfo oAttachmentInfo, int recPeriodID, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                AttachmentDAO oAttachmentDAO = new AttachmentDAO(oAppUserInfo);
                ReconciliationPeriodDAO oReconciliationPeriodDAO = new ReconciliationPeriodDAO(oAppUserInfo);
                using (oConnection = oAttachmentDAO.CreateConnection())
                {
                    oConnection.Open();
                    using (oTransaction = oConnection.BeginTransaction())
                    {
                        oAttachmentDAO.Save(oAttachmentInfo, oConnection, oTransaction);
                        oReconciliationPeriodDAO.UpdateRecPeriodStatusAsInProgress(recPeriodID, oConnection, oTransaction);

                        oTransaction.Commit();
                    }
                }
            }
            catch (SqlException ex)
            {
                try
                {
                    oTransaction.Rollback();
                }
                catch { }
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                try
                {
                    oTransaction.Rollback();
                }
                catch { }
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
        }

        public void DeleteAttachment(long AttachmentID, AppUserInfo oAppUserInfo)
        {

            IDbConnection oConnection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                AttachmentDAO oAttachmentDAO = new AttachmentDAO(oAppUserInfo);
                oConnection = oAttachmentDAO.CreateConnection();
                oConnection.Open();
                oAttachmentDAO.DeleteAttachmentByID(AttachmentID, oConnection);


            }
            finally
            {
                if ((oConnection != null) && (oConnection.State == ConnectionState.Open))
                {
                    oConnection.Close();
                }
            }
        }

        public int? DeleteAttachmentAndGetFileRefrenceCount(long AttachmentID, AppUserInfo oAppUserInfo)
        {
            int? FileReferenceCount = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                AttachmentDAO oAttachmentDAO = new AttachmentDAO(oAppUserInfo);
                FileReferenceCount = oAttachmentDAO.DeleteAttachmentAndGetFileRefrenceCount(AttachmentID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return FileReferenceCount;
        }
        public void InsertAttachmentBulk(List<AttachmentInfo> oAttachmentInfoList, DateTime? dateAdded, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                AttachmentDAO oAttachmentDAO = new AttachmentDAO(oAppUserInfo);
                using (oConnection = oAttachmentDAO.CreateConnection())
                {
                    oConnection.Open();
                    using (oTransaction = oConnection.BeginTransaction())
                    {
                        DataTable dtAttachment = ServiceHelper.ConvertAttachmentInfoCollectionToDataTable(oAttachmentInfoList);
                        oAttachmentDAO.InsertAttachmentBulk(dtAttachment, oAppUserInfo.RecPeriodID, oAppUserInfo.LoginID, dateAdded, oConnection, oTransaction);
                        oTransaction.Commit();
                    }
                }
            }
            catch (SqlException ex)
            {
                try
                {
                    oTransaction.Rollback();
                }
                catch { }
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                try
                {
                    oTransaction.Rollback();
                }
                catch { }
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
        }

        public List<AttachmentInfo> GetAllAttachmentForGL(long? GLDataID, int? UserID, short? RoleID, AppUserInfo oAppUserInfo)
        {
            List<AttachmentInfo> oAttachmentInfoCollection = new List<AttachmentInfo>();
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                AttachmentDAO oAttachmentDAO = new AttachmentDAO(oAppUserInfo);
                oAttachmentInfoCollection = oAttachmentDAO.GetAllAttachmentForGL(GLDataID, UserID, RoleID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oAttachmentInfoCollection;

        }

    }//end of class
}//end of namespace
