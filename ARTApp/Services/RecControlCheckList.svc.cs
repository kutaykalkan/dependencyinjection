using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.App.DAO;
using SkyStem.ART.Client.Model;
using System.Data.SqlClient;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Model.RecControlCheckList;
using System.Data;
using SkyStem.ART.Client.Exception;

namespace SkyStem.ART.App.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RecControlCheckList" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select RecControlCheckList.svc or RecControlCheckList.svc.cs at the Solution Explorer and start debugging.
    public class RecControlCheckList : IRecControlCheckList
    {
        public List<RecControlCheckListInfo> GetRecControlCheckListInfoList(long? GlDataID, int? RecPeriodID, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                RecControlChecklistDAO oRecControlChecklistDAO = new RecControlChecklistDAO(oAppUserInfo);
                return oRecControlChecklistDAO.GetRecControlCheckListInfoList(GlDataID, RecPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return null;
        }
        public List<RecControlCheckListInfo> InsertDataImportRecControlChecklist(DataImportHdrInfo oDataImportHdrInfo
          , List<RecControlCheckListInfo> oRecControlCheckListInfoList, string failureMsg, out int rowAffected, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            int companyID = oDataImportHdrInfo.CompanyID.Value;
            DateTime dateAdded = oDataImportHdrInfo.DateAdded.Value;
            string addedBy = oDataImportHdrInfo.AddedBy;
            int newDataImportId;
            int? rowsAffected = 0;
            rowAffected = 0;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportHdrDAO oDataImportHrdDAO = new DataImportHdrDAO(oAppUserInfo);
                oConnection = oDataImportHrdDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();//Begin transaction
                //Save DataImportHDR
                oDataImportHrdDAO.Save(oDataImportHdrInfo, oConnection, oTransaction);
                if (oDataImportHdrInfo.DataImportID.HasValue)
                {
                    newDataImportId = oDataImportHdrInfo.DataImportID.Value;
                    oRecControlCheckListInfoList.ForEach(T => T.DataImportID = newDataImportId);
                    RecControlChecklistDAO oRecControlChecklistDAO = new RecControlChecklistDAO(oAppUserInfo);
                    oRecControlCheckListInfoList = oRecControlChecklistDAO.SaveRecControlChecklist(oRecControlCheckListInfoList, oAppUserInfo.RecPeriodID, oDataImportHdrInfo.DataImportID, addedBy, dateAdded, oConnection, oTransaction);
                    rowAffected = (int)oRecControlCheckListInfoList.Count();
                    if (rowsAffected.HasValue && rowsAffected >= 0)
                    {
                        newDataImportId = oDataImportHdrInfo.DataImportID.Value;
                        DataImportFailureMessageDAO oDataImportFailureMessageDAO = new DataImportFailureMessageDAO(oAppUserInfo);
                        //Save Failure Message
                        rowsAffected = oDataImportFailureMessageDAO.InsertDataImportFailureMsg(newDataImportId
                            , failureMsg, dateAdded, addedBy, oConnection, oTransaction);
                        if (rowsAffected > 0)
                        {
                            oTransaction.Commit();
                            return oRecControlCheckListInfoList;
                        }
                        else
                        {
                            throw new ARTException(5000042);//Error while saving data to database
                        }
                    }
                    else
                    {
                        throw new ARTException(5000042);//Error while saving data to database
                    }
                }
                else
                {
                    throw new ARTException(5000042);//Error while saving data to database
                }
            }
            catch (ARTException ex)
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed && oTransaction != null)
                    oTransaction.Rollback();
                throw ex;
            }
            catch (SqlException ex)
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed && oTransaction != null)
                    oTransaction.Rollback();
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed && oTransaction != null)
                    oTransaction.Rollback();
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            finally
            {
                if ((null != oConnection) && (oConnection.State == ConnectionState.Open))
                    oConnection.Dispose();
            }
            return null;
        }
        public void DeleteRecControlCheckListItems(List<RecControlCheckListInfo> SelectedRecControlCheckListInfoList, int? RecPeriodID, string RevisedBy, DateTime DateRevised, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            List<RecControlCheckListInfo> oRecControlCheckListInfoWithSameRecPeriod = new List<RecControlCheckListInfo>();
            List<RecControlCheckListInfo> oRecControlCheckListInfoWithDiffrentRecPeriod = new List<RecControlCheckListInfo>();

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                RecControlChecklistDAO oRecControlChecklistDAO = new RecControlChecklistDAO(oAppUserInfo);
                oConnection = oRecControlChecklistDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();//Begin transaction     
                oRecControlCheckListInfoWithSameRecPeriod = (from oRecControlCheckListInfo in SelectedRecControlCheckListInfoList
                                                             where oRecControlCheckListInfo.StartRecPeriodID == RecPeriodID
                                                             select oRecControlCheckListInfo).ToList();
                if (oRecControlCheckListInfoWithSameRecPeriod.Count > 0)
                {
                    foreach (var o in oRecControlCheckListInfoWithSameRecPeriod)
                    {
                        o.IsActive = false;
                    }
                    oRecControlChecklistDAO.DeleteRecControlChecklist(oRecControlCheckListInfoWithSameRecPeriod, RevisedBy, DateRevised, RecPeriodID, oConnection, oTransaction);
                }
                oRecControlCheckListInfoWithDiffrentRecPeriod = (from oRecControlCheckListInfo in SelectedRecControlCheckListInfoList
                                                                 where oRecControlCheckListInfo.StartRecPeriodID != RecPeriodID
                                                                 select oRecControlCheckListInfo).ToList();

                if (oRecControlCheckListInfoWithDiffrentRecPeriod.Count > 0)
                {
                    foreach (var o in oRecControlCheckListInfoWithDiffrentRecPeriod)
                    {
                        o.EndRecPeriodID = RecPeriodID;
                        o.IsActive = true;
                    }
                    oRecControlChecklistDAO.SaveRecControlChecklist(oRecControlCheckListInfoWithDiffrentRecPeriod, RecPeriodID, null, RevisedBy, DateRevised, oConnection, oTransaction);
                }


                oTransaction.Commit();
            }
            catch (Exception ex)
            {
                if ((oTransaction != null) && (oConnection.State == ConnectionState.Open))
                {
                    oTransaction.Rollback();
                }
                throw ex;
            }
            finally
            {
                try
                {
                    if ((null != oConnection) && (oConnection.State == ConnectionState.Open))
                        oConnection.Close();
                }
                catch (Exception)
                {
                }
            }

        }
        public List<GLDataRecControlCheckListCommentInfo> GetGLDataRecControlCheckListCommentInfoList(long? GLDataRecControlCheckListID, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                RecControlChecklistDAO oRecControlChecklistDAO = new RecControlChecklistDAO(oAppUserInfo);
                return oRecControlChecklistDAO.GetGLDataRecControlCheckListCommentInfoList(GLDataRecControlCheckListID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return null;
        }
        public void SaveGLDataRecControlCheckListComment(GLDataRecControlCheckListCommentInfo oGLDataRecControlCheckListCommentInfo, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                RecControlChecklistDAO oRecControlChecklistDAO = new RecControlChecklistDAO(oAppUserInfo);
                oConnection = oRecControlChecklistDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();//Begin transaction     
                oRecControlChecklistDAO.SaveGLDataRecControlCheckListComment(oGLDataRecControlCheckListCommentInfo, oConnection, oTransaction);
                oTransaction.Commit();
            }
            catch (Exception ex)
            {
                if ((oTransaction != null) && (oConnection.State == ConnectionState.Open))
                {
                    oTransaction.Rollback();
                }
                throw ex;
            }
            finally
            {
                try
                {
                    if ((null != oConnection) && (oConnection.State == ConnectionState.Open))
                        oConnection.Close();
                }
                catch (Exception)
                {
                }
            }

        }
        public long? SaveGLDataRecControlCheckListAndComment(GLDataRecControlCheckListInfo oGLDataRecControlCheckListInfo, GLDataRecControlCheckListCommentInfo oGLDataRecControlCheckListCommentInfo, AppUserInfo oAppUserInfo)
        {
            long? GLDataRecControlCheckListID = null;
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                RecControlChecklistDAO oRecControlChecklistDAO = new RecControlChecklistDAO(oAppUserInfo);
                oConnection = oRecControlChecklistDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();//Begin transaction  
                List<GLDataRecControlCheckListInfo> oGLDataRecControlCheckListInfoList = new List<GLDataRecControlCheckListInfo>();
                oGLDataRecControlCheckListInfoList.Add(oGLDataRecControlCheckListInfo);
                oGLDataRecControlCheckListInfoList = oRecControlChecklistDAO.SaveGLDataRecControlChecklist(oGLDataRecControlCheckListInfoList, oGLDataRecControlCheckListInfo.AddedBy, oGLDataRecControlCheckListInfo.DateAdded, oConnection, oTransaction);
                if (oGLDataRecControlCheckListInfoList[0].GLDataRecControlCheckListID.HasValue && oGLDataRecControlCheckListInfoList[0].GLDataRecControlCheckListID.Value > 0)
                {
                    GLDataRecControlCheckListID = oGLDataRecControlCheckListInfoList[0].GLDataRecControlCheckListID;
                    oGLDataRecControlCheckListCommentInfo.GLDataRecControlCheckListID = GLDataRecControlCheckListID;
                    oRecControlChecklistDAO.SaveGLDataRecControlCheckListComment(oGLDataRecControlCheckListCommentInfo, oConnection, oTransaction);
                }
                oTransaction.Commit();
            }
            catch (Exception ex)
            {
                if ((oTransaction != null) && (oConnection.State == ConnectionState.Open))
                {
                    oTransaction.Rollback();
                }
                throw ex;
            }
            finally
            {
                try
                {
                    if ((null != oConnection) && (oConnection.State == ConnectionState.Open))
                        oConnection.Close();
                }
                catch (Exception)
                {
                }
            }
            return GLDataRecControlCheckListID;

        }
        public List<RecControlCheckListAccountInfo> InsertAccountRecControlChecklist(List<RecControlCheckListInfo> oRecControlCheckListInfoList, List<RecControlCheckListAccountInfo> oRecControlCheckListAccountInfoList, long? GLDataID, AppUserInfo oAppUserInfo)
        {

            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            int? rowsAffected = 0;
            int rowAffected = 0;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                RecControlChecklistDAO oRecControlChecklistDAO = new RecControlChecklistDAO(oAppUserInfo);
                oConnection = oRecControlChecklistDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();//Begin transaction

                oRecControlCheckListInfoList = oRecControlChecklistDAO.SaveRecControlChecklist(oRecControlCheckListInfoList, oAppUserInfo.RecPeriodID, null, oRecControlCheckListInfoList[0].AddedBy, oRecControlCheckListInfoList[0].DateAdded, oConnection, oTransaction);
                rowsAffected = (int)oRecControlCheckListInfoList.Count();
                if (rowsAffected.HasValue && rowsAffected >= 0)
                {
                    foreach (var oRecControlCheckListAccountInfo in oRecControlCheckListAccountInfoList)
                    {
                        oRecControlCheckListAccountInfo.RecControlCheckListID = oRecControlCheckListInfoList.Find(T => T.RowNumber == oRecControlCheckListAccountInfo.RowNumber).RecControlCheckListID;
                    }
                    oRecControlCheckListAccountInfoList = oRecControlChecklistDAO.SaveAccountRecControlChecklist(oRecControlCheckListAccountInfoList, oAppUserInfo.RecPeriodID, null, oRecControlCheckListInfoList[0].AddedBy, oRecControlCheckListInfoList[0].DateAdded, GLDataID, oConnection, oTransaction);
                    rowAffected = oRecControlCheckListAccountInfoList.Count;
                    if (rowAffected > 0)
                    {
                        oTransaction.Commit();
                        return oRecControlCheckListAccountInfoList;
                    }
                    else
                    {
                        throw new ARTException(5000042);//Error while saving data to database
                    }
                }
                else
                {
                    rowAffected = 0;
                    throw new ARTException(5000042);//Error while saving data to database
                }

            }
            catch (ARTException ex)
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed && oTransaction != null)
                    oTransaction.Rollback();
                throw ex;
            }
            catch (SqlException ex)
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed && oTransaction != null)
                    oTransaction.Rollback();
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed && oTransaction != null)
                    oTransaction.Rollback();
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            finally
            {
                if ((null != oConnection) && (oConnection.State == ConnectionState.Open))
                    oConnection.Dispose();
            }
            return null;


        }
        public List<RecControlCheckListInfo> InsertDataImportRecControlChecklistAccount(DataImportHdrInfo oDataImportHdrInfo
        , List<RecControlCheckListInfo> oRecControlCheckListInfoList, string failureMsg, out int rowAffected, long? AccountID, int? NetAccountID, long? GLDataID, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            int companyID = oDataImportHdrInfo.CompanyID.Value;
            DateTime dateAdded = oDataImportHdrInfo.DateAdded.Value;
            string addedBy = oDataImportHdrInfo.AddedBy;
            int newDataImportId;
            int? rowsAffected = 0;
            rowAffected = 0;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportHdrDAO oDataImportHrdDAO = new DataImportHdrDAO(oAppUserInfo);
                oConnection = oDataImportHrdDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();//Begin transaction
                //Save DataImportHDR
                oDataImportHrdDAO.Save(oDataImportHdrInfo, oConnection, oTransaction);
                if (oDataImportHdrInfo.DataImportID.HasValue)
                {
                    newDataImportId = oDataImportHdrInfo.DataImportID.Value;
                    oRecControlCheckListInfoList.ForEach(T => T.DataImportID = newDataImportId);
                    RecControlChecklistDAO oRecControlChecklistDAO = new RecControlChecklistDAO(oAppUserInfo);
                    oRecControlCheckListInfoList = oRecControlChecklistDAO.SaveRecControlChecklist(oRecControlCheckListInfoList, oAppUserInfo.RecPeriodID, oDataImportHdrInfo.DataImportID, addedBy, dateAdded, oConnection, oTransaction);
                    rowAffected = (int)oRecControlCheckListInfoList.Count();
                    if (rowsAffected.HasValue && rowsAffected >= 0)
                    {
                        List<RecControlCheckListAccountInfo> oRecControlCheckListAccountInfoList = new List<RecControlCheckListAccountInfo>();
                        RecControlCheckListAccountInfo oRecControlCheckListAccountInfo;
                        foreach (var oRecControlCheckListInfo in oRecControlCheckListInfoList)
                        {
                            oRecControlCheckListAccountInfo = new RecControlCheckListAccountInfo();
                            oRecControlCheckListAccountInfo.RecControlCheckListID = oRecControlCheckListInfo.RecControlCheckListID;
                            oRecControlCheckListAccountInfo.AccountID = AccountID;
                            oRecControlCheckListAccountInfo.NetAccountID = NetAccountID;
                            oRecControlCheckListAccountInfo.AddedBy = oRecControlCheckListInfo.AddedBy;
                            oRecControlCheckListAccountInfo.DateAdded = oRecControlCheckListInfo.DateAdded;
                            oRecControlCheckListAccountInfo.AddedByUserID = oRecControlCheckListInfo.AddedByUserID;
                            oRecControlCheckListAccountInfo.RoleID = oRecControlCheckListInfo.RoleID;
                            oRecControlCheckListAccountInfo.StartRecPeriodID = oAppUserInfo.RecPeriodID;
                            oRecControlCheckListAccountInfo.EndRecPeriodID = null;
                            oRecControlCheckListAccountInfo.IsActive = true;
                            oRecControlCheckListAccountInfo.RowNumber = oRecControlCheckListInfo.RowNumber;
                            oRecControlCheckListAccountInfoList.Add(oRecControlCheckListAccountInfo);
                        }
                        oRecControlCheckListAccountInfoList = oRecControlChecklistDAO.SaveAccountRecControlChecklist(oRecControlCheckListAccountInfoList, oAppUserInfo.RecPeriodID, oDataImportHdrInfo.DataImportID, oRecControlCheckListInfoList[0].AddedBy, oRecControlCheckListInfoList[0].DateAdded, GLDataID, oConnection, oTransaction);

                    }
                    if (rowsAffected.HasValue && rowsAffected >= 0)
                    {
                        newDataImportId = oDataImportHdrInfo.DataImportID.Value;
                        DataImportFailureMessageDAO oDataImportFailureMessageDAO = new DataImportFailureMessageDAO(oAppUserInfo);
                        //Save Failure Message
                        rowsAffected = oDataImportFailureMessageDAO.InsertDataImportFailureMsg(newDataImportId
                            , failureMsg, dateAdded, addedBy, oConnection, oTransaction);
                        if (rowsAffected > 0)
                        {
                            oTransaction.Commit();
                            return oRecControlCheckListInfoList;
                        }
                        else
                        {
                            throw new ARTException(5000042);//Error while saving data to database
                        }
                    }
                    else
                    {
                        throw new ARTException(5000042);//Error while saving data to database
                    }
                }
                else
                {
                    throw new ARTException(5000042);//Error while saving data to database
                }
            }
            catch (ARTException ex)
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed && oTransaction != null)
                    oTransaction.Rollback();
                throw ex;
            }
            catch (SqlException ex)
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed && oTransaction != null)
                    oTransaction.Rollback();
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed && oTransaction != null)
                    oTransaction.Rollback();
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            finally
            {
                if ((null != oConnection) && (oConnection.State == ConnectionState.Open))
                    oConnection.Dispose();
            }
            return null;
        }

        public GLDataRecControlCheckListInfo GetRecControlCheckListCount(long? GlDataID, int? RecPeriodID, AppUserInfo oAppUserInfo)
        {

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                RecControlChecklistDAO oRecControlChecklistDAO = new RecControlChecklistDAO(oAppUserInfo);
                return oRecControlChecklistDAO.GetRecControlCheckListCount(GlDataID, RecPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return null;
        }
        public List<RCCValidationTypeMstInfo> GetRCCValidationTypeMstInfoList(AppUserInfo oAppUserInfo)
        {
            List<RCCValidationTypeMstInfo> oRCCValidationTypeMstInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                RecControlChecklistDAO oRecControlChecklistDAO = new RecControlChecklistDAO(oAppUserInfo);
                oRCCValidationTypeMstInfoList = oRecControlChecklistDAO.GetRCCValidationTypeMstInfoList();

            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oRCCValidationTypeMstInfoList;
        }

    }
}
