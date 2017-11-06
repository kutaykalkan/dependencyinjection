using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Service.Model;
using SkyStem.ART.Service.APP.DAO;
using SkyStem.ART.Service.Data;
using System.Data.SqlClient;
using System.Data;
using SkyStem.ART.Shared.Data;
using System.Configuration;
using System.IO;
using System.Xml;
using SkyStem.ART.Service.APP.BLL;
using SkyStem.ART.Client.Model.CompanyDatabase;
using System.Transactions;
using SkyStem.ART.Client.IServices;
using ClientModel = SkyStem.ART.Client.Model;
using SkyStem.ART.Shared.Utility;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Service.Interfaces;
using SkyStem.ART.Service.Utility;
using SkyStem.Language.LanguageUtility;

namespace DataImportTask.Implementations.Proxies
{
    public class DataImportHelperProxy : IDataImportHelper
    {
        private readonly IDataImportHelper _dataImportHelper;

        public DataImportHelperProxy(IDataImportHelper dataImportHelper)
        {
            _dataImportHelper = dataImportHelper;
        }
        #region "Account Attribute Data Import"
        #region "Public Methods"
        public AccountAttributeDataImportInfo GetAcctAttrDataImportInfoForProcessing(DateTime dateRevised, CompanyUserInfo oCompanyUserInfo)
        {
            return _dataImportHelper.GetAcctAttrDataImportInfoForProcessing(dateRevised, oCompanyUserInfo);
        }

        public List<CapabilityInfo> SelectAllCompanyCapabilityByReconciliationPeriodID(int recPeriodID, CompanyUserInfo oCompanyUserInfo)
        {
            CapabilityDAO oCapabilityDAO = new CapabilityDAO(oCompanyUserInfo);
            return oCapabilityDAO.SelectAllCompanyCapabilityByReconciliationPeriodID(recPeriodID);
        }

        public void TransferAndProcessData(DataTable dtExcel, AccountAttributeDataImportInfo oAcctAttrDataImportInfo, List<ClientModel.LogInfo> oLogInfoCache, CompanyUserInfo oCompanyUserInfo)
        {
            AccountAttributeDAO oAcctAttrDAO = new AccountAttributeDAO(oCompanyUserInfo);
            oAcctAttrDAO.LogInfoCache = oLogInfoCache;
            SqlConnection oConn = null;
            SqlTransaction oTrans = null;
            try
            {
                oConn = oAcctAttrDAO.GetConnection();
                oConn.Open();
                oTrans = oConn.BeginTransaction();
                oAcctAttrDAO.CopyAccountAttributeToSqlServer(dtExcel, oAcctAttrDataImportInfo, oConn, oTrans);
                Helper.LogInfoToCache("8. Data Transfer complete", oLogInfoCache);
                oAcctAttrDAO.ProcessImportedAccountAttribute(oAcctAttrDataImportInfo, oConn, oTrans);
                oTrans.Commit();
                oTrans.Dispose();
                oTrans = null;
                Helper.LogInfoToCache(" AccountAttribute Imported to sql server successfully", oLogInfoCache);
            }
            catch (Exception ex)
            {
                if (oTrans != null)
                {
                    oTrans.Rollback();
                    if (oAcctAttrDataImportInfo.DataImportStatus == "")
                        oAcctAttrDataImportInfo.DataImportStatus = "FAIL";
                    if (oAcctAttrDataImportInfo.ErrorMessageToSave == "")
                        oAcctAttrDataImportInfo.ErrorMessageToSave = ex.Message;
                }
                //Helper.LogError(ex);
                throw ex;
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Dispose();
            }
        }

        public void ProcessTransferredData(AccountAttributeDataImportInfo oAcctAttrDataImportInfo, List<ClientModel.LogInfo> oLogInfoCache, CompanyUserInfo oCompanyUserInfo)
        {
            SqlConnection oConn = null;
            SqlTransaction oTrans = null;
            AccountAttributeDAO oAcctAttrDAO = new AccountAttributeDAO(oCompanyUserInfo);
            oAcctAttrDAO.LogInfoCache = oLogInfoCache;
            try
            {
                oConn = oAcctAttrDAO.GetConnection();
                oConn.Open();
                oTrans = oConn.BeginTransaction();
                oAcctAttrDAO.ProcessImportedAccountAttribute(oAcctAttrDataImportInfo, oConn, oTrans);
                oTrans.Commit();
                oTrans.Dispose();
                oTrans = null;
                Helper.LogInfoToCache(" AccountAttribute Imported to sql server successfully", oLogInfoCache);
            }
            catch (Exception ex)
            {
                if (oTrans != null)
                {
                    oTrans.Rollback();
                    if (oAcctAttrDataImportInfo.DataImportStatus == "")
                        oAcctAttrDataImportInfo.DataImportStatus = "FAIL";
                    if (oAcctAttrDataImportInfo.ErrorMessageToSave == "")
                        oAcctAttrDataImportInfo.ErrorMessageToSave = ex.Message;
                }
                //Helper.LogError(ex);
                throw ex;
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Dispose();
            }
        }

        public  void UpdateDataImportHDR(AccountAttributeDataImportInfo oAcctAttrDataImportInfo, CompanyUserInfo oCompanyUserInfo)
        {
            DataImportHdrDAO oDataImportHdrDAO = new DataImportHdrDAO(oCompanyUserInfo);
            oDataImportHdrDAO.UpdateDataImportHDR(oAcctAttrDataImportInfo);
        }

        public  DataTable GetAcctAttrImportDataTableFromExcel(string fullExcelFilePath, string sheetName, CompanyUserInfo oCompanyUserInfo)
        {
            DataTable oAcctAttrDataTableFromExcel = Helper.GetDataTableFromExcel(fullExcelFilePath, sheetName, oCompanyUserInfo);

            //Rename columns as per first row and remove first row.
            RenameColumnNameAsPerFirstRow(oAcctAttrDataTableFromExcel);

            //Remove First Row
            oAcctAttrDataTableFromExcel.Rows.RemoveAt(0);

            return oAcctAttrDataTableFromExcel;
        }
        //Get a list of  Mandatory Fields
        public  List<string> GetAcctAttrDataImportStaticFields()
        {
            List<string> fieldList = new List<string>();
            fieldList.Add(AccountAttributeDataImportFields.COMPANY);
            return fieldList;
        }

        //Get list of all possible Account Attribute Data Import fields
        public  List<string> GetAcctAttrDataImportAllPossibleFields(DataImportHdrInfo oEntity)
        {
            List<string> fieldList = new List<string>();
            fieldList.AddRange(GetAcctAttrDataImportStaticFields());
            fieldList.AddRange(GetAllPossibleAccountFields(oEntity));

            return fieldList;
        }

        //Get list of all possible Account Attribute Data Import fields
        public  List<string> GetAcctAttrDataImportAllMandatoryFields(DataImportHdrInfo oEntity)
        {
            List<string> fieldList = new List<string>();
            fieldList.AddRange(GetAcctAttrDataImportStaticFields());
            fieldList.AddRange(GetAccountMandatoryFields(oEntity));

            return fieldList;
        }

        public  void ResetGLDataHdrObject(AccountAttributeDataImportInfo oAcctAttrDataImportInfo, Exception ex)
        {
            if (String.IsNullOrEmpty(oAcctAttrDataImportInfo.DataImportStatus))
                oAcctAttrDataImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;

            if (String.IsNullOrEmpty(oAcctAttrDataImportInfo.ErrorMessageToSave))
                oAcctAttrDataImportInfo.ErrorMessageToSave = ex.Message;

        }
        #endregion

        #region "Private Methods"

        #endregion
        #endregion

        #region "GL Data Import"
        #region "Public Methods"
        public  GLDataImportInfo GetGLDataImportInfoForProcessing(DateTime dateRevised, CompanyUserInfo oCompanyUserInfo)
        {
            GLDataDAO oGLDataDAO = new GLDataDAO(oCompanyUserInfo);
            return oGLDataDAO.GetGLDataImportForProcessing(dateRevised);
        }

        public  void TransferAndProcessGLData(DataTable dtExcel, GLDataImportInfo oGLDataImportInfo, List<ClientModel.LogInfo> oLogInfoCache, CompanyUserInfo oCompanyUserInfo)
        {
            GLDataDAO oGLDataDAO = new GLDataDAO(oCompanyUserInfo);
            oGLDataDAO.LogInfoCache = oLogInfoCache;
            SqlConnection oConn = null;
            SqlTransaction oTrans = null;
            try
            {
                oConn = oGLDataDAO.GetConnection();
                oConn.Open();
                oTrans = oConn.BeginTransaction();

                //Transfer Excel Data To Sql Server
                oGLDataDAO.CopyGLDataFromExcelToSqlServer(dtExcel, oGLDataImportInfo, oConn, oTrans);
                Helper.LogInfoToCache("7. Data Transfer complete", oLogInfoCache);
                oGLDataDAO.InsertDataImportWarning(oGLDataImportInfo, oConn, oTrans);
                //Process Transferred Data
                ProcessTransferedGLData(oGLDataDAO, oGLDataImportInfo, oConn, oTrans, oCompanyUserInfo);
                oTrans.Commit();
                oTrans.Dispose();
                oTrans = null;
                if (oGLDataImportInfo.IsAlertRaised)
                {
                    Helper.LogInfoToCache(@"Begin: Send Alert for GLData for DataImportID: " + oGLDataImportInfo.DataImportID.ToString(), oLogInfoCache);
                    Alert oAlert = new Alert(oCompanyUserInfo);
                    oAlert.GetUserListAndSendMail(oGLDataImportInfo.DataImportID, oGLDataImportInfo.CompanyID, oCompanyUserInfo);
                    Helper.LogInfoToCache(@"End: Send Alert for GLData for DataImportID: " + oGLDataImportInfo.DataImportID.ToString(), oLogInfoCache);
                }
            }
            catch (Exception ex)
            {
                if (oTrans != null)
                {
                    oTrans.Rollback();
                    //ResetGLDataHdrObject(oGLDataImportInfo, ex);
                }
                throw ex;
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Dispose();
            }
        }

        public  void ProcessTransferedGLData(GLDataImportInfo oGLDataImportInfo, List<ClientModel.LogInfo> oLogInfoCache, CompanyUserInfo oCompanyUserInfo)
        {
            GLDataDAO oGLDataDAO = new GLDataDAO(oCompanyUserInfo);
            oGLDataDAO.LogInfoCache = oLogInfoCache;
            //Process Transfered Data
            SqlConnection oConn = null;
            SqlTransaction oTrans = null;
            try
            {
                oConn = oGLDataDAO.GetConnection();
                oConn.Open();
                oTrans = oConn.BeginTransaction();
                ProcessTransferedGLData(oGLDataDAO, oGLDataImportInfo, oConn, oTrans, oCompanyUserInfo);
                oTrans.Commit();
                oTrans.Dispose();
                oTrans = null;
                if (oGLDataImportInfo.IsAlertRaised)
                {
                    Helper.LogInfoToCache(@"Begin: Send Alert for GLData for DataImportID: " + oGLDataImportInfo.DataImportID.ToString(), oLogInfoCache);
                    Alert oAlert = new Alert(oCompanyUserInfo);
                    oAlert.GetUserListAndSendMail(oGLDataImportInfo.DataImportID, oGLDataImportInfo.CompanyID, oCompanyUserInfo);
                    Helper.LogInfoToCache(@"End: Send Alert for GLData for DataImportID: " + oGLDataImportInfo.DataImportID.ToString(), oLogInfoCache);
                }
            }
            catch (Exception ex)
            {
                if (oTrans != null)
                {
                    oTrans.Rollback();
                    ResetGLDataHdrObject(oGLDataImportInfo, ex);
                }
                throw ex;
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Dispose();
            }
        }

        public  void UpdateDataImportHDR(GLDataImportInfo oGLDataImportInfo, CompanyUserInfo oCompanyUserInfo)
        {
            DataImportHdrDAO oDataImportHdrDAO = new DataImportHdrDAO(oCompanyUserInfo);
            oDataImportHdrDAO.UpdateDataImportHDR(oGLDataImportInfo);
        }

        public  void UpdateDataImportHDRForUserUpload(DataImportHdrInfo oUserUploadInfo, CompanyUserInfo oCompanyUserInfo)
        {
            DataImportHdrDAO oDataImportHdrDAO = new DataImportHdrDAO(oCompanyUserInfo);
            oDataImportHdrDAO.UpdateDataImportHDR(oUserUploadInfo);
        }

        public  DataTable GetGLDataImportDataTableFromExcel(string fullExcelFilePath, string sheetName, CompanyUserInfo oCompanyUserInfo)
        {
            DataTable oGLDataTableFromExcel;
            FileInfo file = new FileInfo(fullExcelFilePath);
            if (file.Extension.ToLower() == FileExtensions.csv)
                oGLDataTableFromExcel = ExcelHelper.GetDataTableFromImportDelimitedFile(fullExcelFilePath, true);
            else
            {
                oGLDataTableFromExcel = Helper.GetDataTableFromExcel(fullExcelFilePath, sheetName, oCompanyUserInfo);
                //Rename columns as per first row and remove first row.
                RenameColumnNameAsPerFirstRow(oGLDataTableFromExcel);
                //Remove First Row
                oGLDataTableFromExcel.Rows.RemoveAt(0);
            }

            return oGLDataTableFromExcel;
        }

        //Get a list of  Mandatory Fields
        public  List<string> GetGLDataImportStaticFields()
        {
            List<string> fieldList = new List<string>();
            fieldList.Add(GLDataImportFields.PERIODENDDATE);
            // fieldList.Add(GLDataImportFields.COMPANY); Commented by Manoj: As We do not Use This column Any Where
            fieldList.Add(GLDataImportFields.BCCYCODE);
            fieldList.Add(GLDataImportFields.BALANCEBCCY);
            fieldList.Add(GLDataImportFields.BALANCERCCY);
            fieldList.Add(GLDataImportFields.RCCYCODE);
            return fieldList;
        }

        //Get list of all possible GL Data Import fields
        public  List<string> GetAllPossibleGLDataImportFields(DataImportHdrInfo oEntity)
        {
            List<string> fieldList = new List<string>();
            fieldList.AddRange(GetAccountStaticFields());
            fieldList.AddRange(GetAllAccountCreationMendatoryFields());
            if (!String.IsNullOrEmpty(oEntity.KeyFields))
                fieldList.AddRange(GetAccountKeyFields(oEntity));
            return fieldList;
        }

        //Get list of all mandatory fields in GLDataImport
        public  List<string> GetGLDataImportAllMandatoryFields(DataImportHdrInfo oEntity)
        {
            List<string> fieldList = new List<string>();
            fieldList.AddRange(GetGLDataImportStaticFields());
            fieldList.AddRange(GetAccountMandatoryFields(oEntity));
            // fieldList.AddRange(GetAllAccountCreationMendatoryFields());
            return fieldList;
        }

        public  List<SkyStem.ART.Client.Model.AccountHdrInfo> GetAccountInformationWithoutGL(int? UserID, short? RoleID, int RecPeriodID, int CompanyID)
        {
            IAccount oAccount = RemotingHelper.GetAccountObject();
            ClientModel.AppUserInfo oAppUserInfo = new ClientModel.AppUserInfo();
            oAppUserInfo.CompanyID = CompanyID;
            List<SkyStem.ART.Client.Model.AccountHdrInfo> oListAccountHdrInfo = oAccount.GetAccountInformationWithoutGL(UserID, RoleID, RecPeriodID, oAppUserInfo);
            return oListAccountHdrInfo;
        }

        public  List<SkyStem.ART.Client.Model.AccountHdrInfo> GetNewAccounts(int? DataImportID, int CompanyID)
        {
            IAccount oAccount = RemotingHelper.GetAccountObject();
            ClientModel.AppUserInfo oAppUserInfo = new ClientModel.AppUserInfo();
            oAppUserInfo.CompanyID = CompanyID;
            List<SkyStem.ART.Client.Model.AccountHdrInfo> oListAccountHdrInfo = oAccount.GetNewAccounts(DataImportID, oAppUserInfo);
            return oListAccountHdrInfo;
        }

        public  ClientModel.DataImportHdrInfo GetDataImportHdrInfo(int? DataImportID, int CompanyID)
        {
            IDataImport oDataImport = RemotingHelper.GetDataImportObject();
            ClientModel.AppUserInfo oAppUserInfo = new ClientModel.AppUserInfo();
            oAppUserInfo.CompanyID = CompanyID;
            ClientModel.DataImportHdrInfo oDataImportHdrInfo = oDataImport.GetDataImportInfo(DataImportID, oAppUserInfo);
            return oDataImportHdrInfo;
        }

        public  DataTable RenameTemplateColumnNameToArtColumns(DataTable oGLDataTableFromExcel, List<SkyStem.ART.Client.Model.ImportTemplateFieldMappingInfo> oImportTemplateFieldMappingInfoList)
        {
            if (oImportTemplateFieldMappingInfoList != null && oImportTemplateFieldMappingInfoList.Count > 0)
                RenameColumnNameAsPerARTColumns(oGLDataTableFromExcel, oImportTemplateFieldMappingInfoList);
            return oGLDataTableFromExcel;
        }
        //Rename column names as per Template Columns to ART Columns
        private  void RenameColumnNameAsPerARTColumns(DataTable dt, List<SkyStem.ART.Client.Model.ImportTemplateFieldMappingInfo> oImportTemplateFieldMappingInfoList)
        {
            string currentColumnName = string.Empty;
            DataRow dr = dt.Rows[0];
            if (dr != null)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    DataColumn dc = dt.Columns[i];
                    //Rename columnName
                    currentColumnName = dc.ColumnName;
                    var oTempImportTemplateFieldMappingInfoList = oImportTemplateFieldMappingInfoList.FindAll(obj => obj.ImportTemplateField == currentColumnName);
                    if (oTempImportTemplateFieldMappingInfoList != null && oTempImportTemplateFieldMappingInfoList.Count > 0)
                    {
                        if (oTempImportTemplateFieldMappingInfoList.Count > 1)
                        {
                            for (int j = 1; j < oTempImportTemplateFieldMappingInfoList.Count; j++)
                            {
                                AddNewColumns(dt, oTempImportTemplateFieldMappingInfoList[j].ImportField, currentColumnName);
                            }
                            if (!String.IsNullOrEmpty(oTempImportTemplateFieldMappingInfoList[0].ImportField))
                                dc.ColumnName = oTempImportTemplateFieldMappingInfoList[0].ImportField.Trim();
                        }
                        else
                            if (!String.IsNullOrEmpty(oTempImportTemplateFieldMappingInfoList[0].ImportField))
                            dc.ColumnName = oTempImportTemplateFieldMappingInfoList[0].ImportField.Trim();
                    }
                }
            }
        }
        private  void AddNewColumns(DataTable SourceDT, string TargerColumnName, string SourceColumnName)
        {
            DataColumn NewColumn = new DataColumn(TargerColumnName, typeof(System.String));
            SourceDT.Columns.Add(NewColumn);
            foreach (DataRow Row in SourceDT.Rows)
            {
                Row[TargerColumnName] = Row[SourceColumnName];
            }
        }
        public  List<SkyStem.ART.Client.Model.ImportTemplateFieldMappingInfo> GetImportTemplateFieldMappingInfoList(int? ImportTemplateID, int CompanyID)
        {
            IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
            ClientModel.AppUserInfo oAppUserInfo = new ClientModel.AppUserInfo();
            oAppUserInfo.CompanyID = CompanyID;
            List<SkyStem.ART.Client.Model.ImportTemplateFieldMappingInfo> oImportTemplateFieldMappingInfoList = oDataImportClient.GetImportTemplateFieldMappingInfoList(ImportTemplateID, oAppUserInfo);
            return oImportTemplateFieldMappingInfoList;
        }
        public  List<ClientModel.ImportTemplateFieldMappingInfo> GetAllDataImportFieldsWithMapping(int dataImportID, int CompanyID)
        {
            IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
            ClientModel.AppUserInfo oAppUserInfo = new ClientModel.AppUserInfo();
            oAppUserInfo.CompanyID = CompanyID;
            List<SkyStem.ART.Client.Model.ImportTemplateFieldMappingInfo> oImportTemplateFieldMappingInfoList = oDataImportClient.GetAllDataImportFieldsWithMapping(dataImportID, oAppUserInfo);
            return oImportTemplateFieldMappingInfoList;
        }
        public  SkyStem.ART.Client.Model.ImportTemplateFieldMappingInfo GetImportTemplateField(List<SkyStem.ART.Client.Model.ImportTemplateFieldMappingInfo> oImportTemplateFieldMappingInfoList, string ImportField)
        {
            SkyStem.ART.Client.Model.ImportTemplateFieldMappingInfo oImportTemplateFieldMappingInfo = null;
            if (oImportTemplateFieldMappingInfoList != null && oImportTemplateFieldMappingInfoList.Count > 0)
            {
                oImportTemplateFieldMappingInfo = oImportTemplateFieldMappingInfoList.Find(obj => obj.ImportField == ImportField);
            }
            return oImportTemplateFieldMappingInfo;
        }

        #endregion

        #region "Private Methods"
        private  void ProcessTransferedGLData(GLDataDAO oGLDataDAO, GLDataImportInfo oGLDataImportInfo, SqlConnection oConn, SqlTransaction oTrans, CompanyUserInfo oCompanyUserInfo)
        {
            if (oGLDataImportInfo.IsMultiVersionUpload)
                oGLDataDAO.ProcessImportedMultiVersionGLData(oGLDataImportInfo, oConn, oTrans);
            else
                oGLDataDAO.ProcessImportedGLData(oGLDataImportInfo, oConn, oTrans);

            Helper.LogInfoToCache("8. Data Processing Complete.", oGLDataDAO.LogInfoCache);
            Helper.LogInfoToCache(" - Status: " + oGLDataImportInfo.DataImportStatus, oGLDataDAO.LogInfoCache);
            Helper.LogInfoToCache(" - Message: " + oGLDataImportInfo.ErrorMessageFromSqlServer, oGLDataDAO.LogInfoCache);
            Helper.LogInfoToCache(" - Records Imported: " + oGLDataImportInfo.RecordsImported.ToString(), oGLDataDAO.LogInfoCache);
            // Data Should be deleted in case there is no warning
            if (oGLDataImportInfo.DataImportStatus != DataImportStatus.DATAIMPORTWARNING)
                oGLDataImportInfo.IsDataDeletionRequired = true;
            // Raise exception if dataImportStatus = "FAIL". This exception message will be logged into logfile
            if (oGLDataImportInfo.DataImportStatus == DataImportStatus.DATAIMPORTFAIL
                    || oGLDataImportInfo.DataImportStatus == DataImportStatus.DATAIMPORTSEVEREWARNING)
                throw new Exception(oGLDataImportInfo.ErrorMessageToSave);
            //if (oGLDataImportInfo.DataImportStatus == DataImportStatus.DATAIMPORTSUCCESS)
            //{
            //    if (oGLDataImportInfo.DataImportMessageDetailInfoList == null)
            //        oGLDataImportInfo.DataImportMessageDetailInfoList = new List<ClientModel.DataImportMessageDetailInfo>();
            //    ClientModel.DataImportMessageDetailInfo oDataImportMessageDetailInfo = new ClientModel.DataImportMessageDetailInfo();
            //    oDataImportMessageDetailInfo.DataImportID = oGLDataImportInfo.DataImportID;
            //    oDataImportMessageDetailInfo.DataImportMessageID = 23;
            //    oDataImportMessageDetailInfo.DataImportMessageLabelID = 1743;
            //    oGLDataImportInfo.DataImportMessageDetailInfoList.Add(oDataImportMessageDetailInfo);
            //}
        }

        public  void ResetGLDataHdrObject(GLDataImportInfo oGLDataImportInfo, Exception ex)
        {
            if (oGLDataImportInfo.DataImportStatus == DataImportStatus.DATAIMPORTSEVEREWARNING)
            {
                if (string.IsNullOrEmpty(oGLDataImportInfo.WarningEmailIds))
                    oGLDataImportInfo.WarningEmailIds = oGLDataImportInfo.OverridenAcctEmailID;
                else
                    oGLDataImportInfo.WarningEmailIds = oGLDataImportInfo.WarningEmailIds + ';' + oGLDataImportInfo.OverridenAcctEmailID;
                oGLDataImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTWARNING;
                oGLDataImportInfo.DataImportMessageDetailInfoList = new List<ClientModel.DataImportMessageDetailInfo>();
            }
            else
            {
                if (string.IsNullOrEmpty(oGLDataImportInfo.DataImportStatus))
                    oGLDataImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
                if (string.IsNullOrEmpty(oGLDataImportInfo.ErrorMessageToSave))
                    oGLDataImportInfo.ErrorMessageToSave = ex.Message;
            }
        }

        public  DataTable ConvertDataImportStatusMessageToDataTable(List<ClientModel.DataImportMessageDetailInfo> oDataImportMessageDetailInfoList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("DataImportWarningDetailID", typeof(System.Int64)));
            dt.Columns.Add(new DataColumn("DataImportID", typeof(System.Int32)));
            dt.Columns.Add(new DataColumn("DataImportMessageID", typeof(System.Int16)));
            dt.Columns.Add(new DataColumn("DataImportMessageLabelID", typeof(System.Int32)));
            dt.Columns.Add(new DataColumn("DataImportMessage", typeof(System.String)));
            dt.Columns.Add(new DataColumn("ExcelRowNumber", typeof(System.Int32)));
            dt.Columns.Add(new DataColumn("AccountID", typeof(System.Int64)));
            dt.Columns.Add(new DataColumn("MessageSchema", typeof(System.String)));
            dt.Columns.Add(new DataColumn("MessageData", typeof(System.String)));
            dt.Columns.Add(new DataColumn("IsActive", typeof(System.Boolean)));
            dt.Columns.Add(new DataColumn("DateAdded", typeof(System.DateTime)));
            if (oDataImportMessageDetailInfoList != null && oDataImportMessageDetailInfoList.Count > 0)
            {
                foreach (ClientModel.DataImportMessageDetailInfo oItem in oDataImportMessageDetailInfoList)
                {
                    DataRow dr = dt.NewRow();
                    dt.Rows.Add(dr);
                    if (oItem.DataImportMessageDetailID.HasValue)
                        dr["DataImportWarningDetailID"] = oItem.DataImportMessageDetailID;
                    else
                        dr["DataImportWarningDetailID"] = DBNull.Value;

                    if (oItem.DataImportID.HasValue)
                        dr["DataImportID"] = oItem.DataImportID;
                    else
                        dr["DataImportID"] = DBNull.Value;

                    if (oItem.DataImportMessageID.HasValue)
                        dr["DataImportMessageID"] = oItem.DataImportMessageID;
                    else
                        dr["DataImportMessageID"] = DBNull.Value;

                    if (oItem.DataImportMessageLabelID.HasValue)
                        dr["DataImportMessageLabelID"] = oItem.DataImportMessageLabelID;
                    else
                        dr["DataImportMessageLabelID"] = DBNull.Value;

                    if (!string.IsNullOrEmpty(oItem.DataImportMessage))
                        dr["DataImportMessage"] = oItem.DataImportMessage;
                    else
                        dr["DataImportMessage"] = DBNull.Value;

                    if (oItem.ExcelRowNumber.HasValue)
                        dr["ExcelRowNumber"] = oItem.ExcelRowNumber;
                    else
                        dr["ExcelRowNumber"] = DBNull.Value;

                    if (oItem.AccountInfo != null && oItem.AccountInfo.AccountID.HasValue)
                        dr["AccountID"] = oItem.AccountInfo.AccountID.Value;
                    else
                        dr["AccountID"] = DBNull.Value;


                    if (!string.IsNullOrEmpty(oItem.MessageSchema))
                        dr["MessageSchema"] = oItem.MessageSchema;
                    else
                        dr["MessageSchema"] = DBNull.Value;

                    if (!string.IsNullOrEmpty(oItem.MessageData))
                        dr["MessageData"] = oItem.MessageData;
                    else
                        dr["MessageData"] = DBNull.Value;

                    if (oItem.IsActive.HasValue)
                        dr["IsActive"] = oItem.IsActive;
                    else
                        dr["IsActive"] = 1;

                    if (oItem.DateAdded.HasValue && oItem.DateAdded.Value != DateTime.MinValue)
                        dr["DateAdded"] = oItem.DateAdded;
                    else
                        dr["DateAdded"] = DBNull.Value;
                }
            }
            return dt;
        }

        #endregion
        #endregion

        #region "Subledger Data Import"
        #region "Public Methods"
        public  SubledgerDataImportInfo GetSubledgerDataImportInfoForProcessing(DateTime dateRevised, CompanyUserInfo oCompanyUserInfo)
        {
            SubledgerDataImportDAO oSubledgerDataImportDAO = new SubledgerDataImportDAO(oCompanyUserInfo);
            return oSubledgerDataImportDAO.GetSubledgerDataImportForProcessing(dateRevised);
        }

        public  void TransferAndProcessSubledgerData(DataTable dtExcel, SubledgerDataImportInfo oSubledgerDataImportInfo, List<ClientModel.LogInfo> oLogInfoCache, CompanyUserInfo oCompanyUserInfo)
        {
            SubledgerDataImportDAO oSubledgerDataImportDAO = new SubledgerDataImportDAO(oCompanyUserInfo);
            oSubledgerDataImportDAO.LogInfoCache = oLogInfoCache;
            SqlConnection oConn = null;
            SqlTransaction oTrans = null;
            try
            {
                oConn = oSubledgerDataImportDAO.GetConnection();
                oConn.Open();
                oTrans = oConn.BeginTransaction();

                //Transfer Excel Data To Sql Server
                oSubledgerDataImportDAO.CopySubledgerDataToSqlServer(dtExcel, oSubledgerDataImportInfo, oConn, oTrans);
                Helper.LogInfoToCache("7. Data Transfer complete", oLogInfoCache);
                //Process Transferred Data
                ProcessTransferedSubledgerData(oSubledgerDataImportDAO, oSubledgerDataImportInfo, oConn, oTrans, oCompanyUserInfo);
                oTrans.Commit();
                oTrans.Dispose();
                oTrans = null;

            }
            catch (Exception ex)
            {
                if (oTrans != null)
                {
                    oTrans.Rollback();
                    //ResetSubledgerDataHdrObject(oSubledgerDataImportInfo, ex);
                }
                throw ex;
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Dispose();
            }
        }

        public  void ProcessTransferedSubledgerData(SubledgerDataImportInfo oSubledgerDataImportInfo, List<ClientModel.LogInfo> oLogInfoCache, CompanyUserInfo CompanyUserInfo)
        {
            SubledgerDataImportDAO oSubledgerDataImportDAO = new SubledgerDataImportDAO(CompanyUserInfo);
            oSubledgerDataImportDAO.LogInfoCache = oLogInfoCache;
            //Process Transfered Data
            SqlConnection oConn = null;
            SqlTransaction oTrans = null;
            try
            {
                oConn = oSubledgerDataImportDAO.GetConnection();
                oConn.Open();
                oTrans = oConn.BeginTransaction();
                ProcessTransferedSubledgerData(oSubledgerDataImportDAO, oSubledgerDataImportInfo, oConn, oTrans, CompanyUserInfo);
                oTrans.Commit();
                oTrans.Dispose();
                oTrans = null;
            }
            catch (Exception ex)
            {
                if (oTrans != null)
                {
                    oTrans.Rollback();
                    ResetSubledgerDataHdrObject(oSubledgerDataImportInfo, ex);
                }
                throw ex;
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Dispose();
            }
        }

        public  void UpdateDataImportHDR(SubledgerDataImportInfo oSubledgerDataImportInfo, CompanyUserInfo oCompanyUserInfo)
        {
            DataImportHdrDAO oDataImportHdrDAO = new DataImportHdrDAO(oCompanyUserInfo);
            oDataImportHdrDAO.UpdateDataImportHDR(oSubledgerDataImportInfo);
        }

        public  DataTable GetSubledgerDataImportDataTableFromExcel(string fullExcelFilePath, string sheetName, CompanyUserInfo oCopmpnayUserInfo)
        {

            DataTable oSubledgerDataTableFromExcel;
            FileInfo file = new FileInfo(fullExcelFilePath);
            if (file.Extension.ToLower() == FileExtensions.csv)
                oSubledgerDataTableFromExcel = ExcelHelper.GetDataTableFromImportDelimitedFile(fullExcelFilePath, true);
            else
            {
                oSubledgerDataTableFromExcel = Helper.GetDataTableFromExcel(fullExcelFilePath, sheetName, oCopmpnayUserInfo);
                //Rename columns as per first row and remove first row.
                RenameColumnNameAsPerFirstRow(oSubledgerDataTableFromExcel);
                //Remove First Row
                oSubledgerDataTableFromExcel.Rows.RemoveAt(0);
            }
            return oSubledgerDataTableFromExcel;
        }

        //Get a list of  Mandatory Fields
        public  List<string> GetSubledgerDataImportStaticFields()
        {
            List<string> fieldList = new List<string>();

            //Get All Mandatory  Fields: PeriodEndDate, BCCYCode, RccyCode, BalanceBCCY, BalanceRCCY
            fieldList.Add(SubledgerDataImportFields.PERIODENDDATE);
            fieldList.Add(SubledgerDataImportFields.BCCYCODE);
            fieldList.Add(SubledgerDataImportFields.BALANCEBCCY);
            fieldList.Add(SubledgerDataImportFields.BALANCERCCY);
            fieldList.Add(SubledgerDataImportFields.RCCYCODE);

            return fieldList;
        }

        //Get list of Mandatory fields in Subledger data Import
        public  List<string> GetSubledgerDataImportAllMandatoryFields(DataImportHdrInfo oEntity)
        {
            List<string> fieldList = new List<string>();

            fieldList.AddRange(GetSubledgerDataImportStaticFields());
            fieldList.AddRange(GetAccountMandatoryFields(oEntity));
            //fieldList.AddRange(GetAllAccountCreationMendatoryFields());
            return fieldList;
        }

        //Get list of all possible Subledger data import fields
        public  List<string> GetSubledgerDataImportAllPossibleMandatoryFields(DataImportHdrInfo oEntity)
        {
            List<string> fieldList = new List<string>();
            fieldList.AddRange(GetSubledgerDataImportStaticFields());
            fieldList.AddRange(GetAllPossibleAccountFields(oEntity));

            return fieldList;
        }
        #endregion
        #region "Private Methods"
        private  void ProcessTransferedSubledgerData(SubledgerDataImportDAO oSubledgerDataImportDAO, SubledgerDataImportInfo oSubledgerDataImportInfo, SqlConnection oConn, SqlTransaction oTrans, CompanyUserInfo oCompanyUserInfo)
        {
            if (oSubledgerDataImportInfo.IsMultiVersionUpload)
                oSubledgerDataImportDAO.ProcessImportedMultiversionSubledgerData(oSubledgerDataImportInfo, oConn, oTrans);
            else
                oSubledgerDataImportDAO.ProcessImportedSubledgerData(oSubledgerDataImportInfo, oConn, oTrans);

            Helper.LogInfoToCache("8. Data Processing Complete.", oSubledgerDataImportDAO.LogInfoCache);
            Helper.LogInfoToCache(" - Status: " + oSubledgerDataImportInfo.DataImportStatus, oSubledgerDataImportDAO.LogInfoCache);
            Helper.LogInfoToCache(" - Message: " + oSubledgerDataImportInfo.ErrorMessageFromSqlServer, oSubledgerDataImportDAO.LogInfoCache);
            Helper.LogInfoToCache(" - Records Imported: " + oSubledgerDataImportInfo.RecordsImported.ToString(), oSubledgerDataImportDAO.LogInfoCache);

            // Data Should be deleted in case there is no warning
            if (oSubledgerDataImportInfo.DataImportStatus != DataImportStatus.DATAIMPORTWARNING)
                oSubledgerDataImportInfo.IsDataDeletionRequired = true;
            //Raise exception if dataImportStatus = "FAIL". This exception message will be logged into logfile
            if (oSubledgerDataImportInfo.DataImportStatus == DataImportStatus.DATAIMPORTFAIL
                || oSubledgerDataImportInfo.DataImportStatus == DataImportStatus.DATAIMPORTSEVEREWARNING)
                throw new Exception(oSubledgerDataImportInfo.ErrorMessageToSave);
        }

        public  void ResetSubledgerDataHdrObject(SubledgerDataImportInfo oSubledgerDataImportInfo, Exception ex)
        {
            if (oSubledgerDataImportInfo.DataImportStatus == DataImportStatus.DATAIMPORTSEVEREWARNING)
            {
                if (string.IsNullOrEmpty(oSubledgerDataImportInfo.WarningEmailIds))
                    oSubledgerDataImportInfo.WarningEmailIds = oSubledgerDataImportInfo.OverridenAcctEmailID;
                else
                    oSubledgerDataImportInfo.WarningEmailIds = oSubledgerDataImportInfo.WarningEmailIds + ';' + oSubledgerDataImportInfo.OverridenAcctEmailID;

                oSubledgerDataImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTWARNING;
            }
            else
            {
                if (string.IsNullOrEmpty(oSubledgerDataImportInfo.DataImportStatus))
                    oSubledgerDataImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
                if (string.IsNullOrEmpty(oSubledgerDataImportInfo.ErrorMessageToSave))
                    oSubledgerDataImportInfo.ErrorMessageToSave = ex.Message;
            }
        }

        #endregion
        #endregion

        #region Multilingual Data Import
        #region "Public Methods"

        public  MultilingualDataImportHdrInfo GetMultilingualDataImportInfoForProcessing(DateTime dateRevised, CompanyUserInfo oCompanyUserInfo)
        {
            MultilingualDataDAO oMultilingualDataDAO = new MultilingualDataDAO(oCompanyUserInfo);
            return oMultilingualDataDAO.GetMultilingualDataImportForProcessing(dateRevised);
        }

        public  void UpdateDataImportHDR(MultilingualDataImportHdrInfo oMultilingualDataImportHdrInfo, CompanyUserInfo oCompanyUserInfo)
        {
            DataImportHdrDAO oDataImportHdrDAO = new DataImportHdrDAO(oCompanyUserInfo);
            oDataImportHdrDAO.UpdateDataImportHDR(oMultilingualDataImportHdrInfo);
        }

        public  DataTable GetMultilingualImportDataTableFromExcel(string fullExcelFilePath, string sheetName, CompanyUserInfo oCompanyUserInfo)
        {
            DataTable oMultilingualDataTableFromExcel = Helper.GetDataTableFromExcel(fullExcelFilePath, sheetName, oCompanyUserInfo);

            //Rename columns as per first row and remove first row.
            RenameColumnNameAsPerFirstRow(oMultilingualDataTableFromExcel);

            //Remove First Row
            oMultilingualDataTableFromExcel.Rows.RemoveAt(0);

            return oMultilingualDataTableFromExcel;
        }

        public  DataTable GetUserUploadDataTableFromExcel(string fullExcelFilePath, string sheetName, CompanyUserInfo oCompanyUserInfo)
        {
            DataTable oUserUploadDataTableFromExcel = Helper.GetDataTableFromExcel(fullExcelFilePath, sheetName, oCompanyUserInfo);

            //Rename columns as per first row and remove first row.
            RenameColumnNameAsPerFirstRow(oUserUploadDataTableFromExcel);

            //Remove First Row
            oUserUploadDataTableFromExcel.Rows.RemoveAt(0);

            return oUserUploadDataTableFromExcel;
        }

        //Get list of all mandatory fields in Multilingual Data Import
        public  List<string> GetMultilingualDataImportAllMandatoryFields()
        {
            List<string> fieldList = new List<string>();
            fieldList.Add(MultilingualUploadConstants.Fields.LabelID);
            fieldList.Add(MultilingualUploadConstants.Fields.FromLanguage);
            fieldList.Add(MultilingualUploadConstants.Fields.ToLanguage);
            return fieldList;
        }

        //public  List<string> GetUserUploadImportMandatoryFields()
        //{
        //    List<string> mandatoryFields = new List<string>();
        //    mandatoryFields.Add(UserUploadConstants.Fields.FIRSTNAME);
        //    mandatoryFields.Add(UserUploadConstants.Fields.LASTTNAME);
        //    mandatoryFields.Add(UserUploadConstants.Fields.LOGINID);
        //    mandatoryFields.Add(UserUploadConstants.Fields.EMAILID);
        //    mandatoryFields.Add(UserUploadConstants.Fields.DEFAULTROLE);

        //    return mandatoryFields;
        //}

        public  void ResetMultilingualDataHdrObject(MultilingualDataImportHdrInfo oMultilingualDataImportHdrInfo, Exception ex)
        {
            if (oMultilingualDataImportHdrInfo.DataImportStatus == DataImportStatus.DATAIMPORTSEVEREWARNING)
            {
                oMultilingualDataImportHdrInfo.DataImportStatus = DataImportStatus.DATAIMPORTWARNING;
            }
            else
            {
                if (string.IsNullOrEmpty(oMultilingualDataImportHdrInfo.DataImportStatus))
                    oMultilingualDataImportHdrInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
                if (string.IsNullOrEmpty(oMultilingualDataImportHdrInfo.ErrorMessageToSave))
                    oMultilingualDataImportHdrInfo.ErrorMessageToSave = ex.Message;
            }
        }
        #endregion
        #region Private Methods
        #endregion
        #endregion

        #region "User Data Import"
        internal  UserDataImportInfo GetUserDataImportInfoForProcessing(DateTime dateRevised, CompanyUserInfo oCompanyUserInfo)
        {
            UserUploadDAO oUserDataImportDAO = new UserUploadDAO(oCompanyUserInfo);
            return oUserDataImportDAO.GetUserDataImportForProcessing(dateRevised);
        }
        internal  void UpdateDataImportHDR(UserDataImportInfo oUserDataImportInfo, CompanyUserInfo oCompanyUserInfo)
        {
            DataImportHdrDAO oDataImportHdrDAO = new DataImportHdrDAO(oCompanyUserInfo);
            oDataImportHdrDAO.UpdateDataImportHDR(oUserDataImportInfo);
        }

        /// <summary>
        /// copies data to transit table and calls a stored proc to process that data
        /// </summary>
        /// <param name="dtExcelData"></param>
        /// <param name="oUserDataImportInfo"></param>
        internal  void TransferAndProcessUserData(DataTable dtExcelData, UserDataImportInfo oUserDataImportInfo, CompanyUserInfo oCompanyUserInfo)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                UserUploadDAO oUserDataImportDAO = new UserUploadDAO(oCompanyUserInfo);

                //Transfer Excel Data To Sql Server
                Helper.LogError("8. Transferring data to sql server", oCompanyUserInfo);
                oUserDataImportDAO.CopyGLDataFromExcelToSqlServer(dtExcelData, oUserDataImportInfo);
                Helper.LogError("9. Data Transfer complete", oCompanyUserInfo);

                //Process Transferred Data
                Helper.LogError("10. start processing transfered data", oCompanyUserInfo);
                oUserDataImportDAO.ProcessTransferedUserData(oUserDataImportInfo);

                if (oUserDataImportInfo.CompanyUserInfoList != null && oUserDataImportInfo.CompanyUserInfoList.Count > 0)
                {
                    Helper.LogError("11. Creating Company User Mapping", oCompanyUserInfo);
                    //Create Company User Mapping in Core
                    IUser oUser = RemotingHelper.GetUserObject();
                    ClientModel.AppUserInfo oAppUserInfo = new ClientModel.AppUserInfo();
                    oAppUserInfo.CompanyID = oUserDataImportInfo.CompanyID;
                    oUser.AddCompanyUser(oUserDataImportInfo.CompanyUserInfoList, oUserDataImportInfo.AddedBy, oUserDataImportInfo.DateAdded, oAppUserInfo);
                }

                Helper.LogError("12. Processing complete", oCompanyUserInfo);
                scope.Complete();
            }
        }

        internal  void ProcessTransferedUserData(UserDataImportInfo oUserDataImportInfo, CompanyUserInfo oCompanyUserInfo)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                UserUploadDAO oUserDataImportDAO = new UserUploadDAO(oCompanyUserInfo);

                //Process Transferred Data
                oUserDataImportDAO.ProcessTransferedUserData(oUserDataImportInfo);

                if (oUserDataImportInfo.CompanyUserInfoList != null && oUserDataImportInfo.CompanyUserInfoList.Count > 0)
                {
                    //Create Company User Mapping in Core
                    IUser oUser = RemotingHelper.GetUserObject();
                    ClientModel.AppUserInfo oAppUserInfo = new ClientModel.AppUserInfo();
                    oAppUserInfo.CompanyID = oUserDataImportInfo.CompanyID;
                    oUser.AddCompanyUser(oUserDataImportInfo.CompanyUserInfoList, oUserDataImportInfo.AddedBy, oUserDataImportInfo.DateAdded, oAppUserInfo);
                }
                scope.Complete();
            }
        }

        internal  void ResetUserDataHdrObject(UserDataImportInfo oUserDataImportInfo, Exception ex)
        {
            if (oUserDataImportInfo.DataImportStatus == DataImportStatus.DATAIMPORTSEVEREWARNING)
            {
                oUserDataImportInfo.WarningEmailIds = oUserDataImportInfo.WarningEmailIds;
                oUserDataImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTWARNING;
            }
            else
            {
                if (string.IsNullOrEmpty(oUserDataImportInfo.DataImportStatus))
                    oUserDataImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
                if (string.IsNullOrEmpty(oUserDataImportInfo.ErrorMessageToSave))
                    oUserDataImportInfo.ErrorMessageToSave = ex.Message;
            }
        }

        //Get list of Mandatory fields in Subledger data Import
        public  List<string> GetUserUploadImportMandatoryFields()
        {
            List<string> mandatoryFields = new List<string>();
            mandatoryFields.Add(UserUploadConstants.UploadFields.FIRSTNAME);
            mandatoryFields.Add(UserUploadConstants.UploadFields.LASTTNAME);
            mandatoryFields.Add(UserUploadConstants.UploadFields.LOGINID);
            mandatoryFields.Add(UserUploadConstants.UploadFields.EMAILID);
            mandatoryFields.Add(UserUploadConstants.UploadFields.DEFAULTROLE);

            return mandatoryFields;
        }

        public  DataTable GetUserUploadDataImportDataTableFromExcel(string fullExcelFilePath, string sheetName, CompanyUserInfo oCompanyUserInfo)
        {
            DataTable oUserUploadDataTableFromExcel = Helper.GetDataTableFromExcel(fullExcelFilePath, sheetName, oCompanyUserInfo);

            //Rename columns as per first row and remove first row.
            RenameColumnNameAsPerFirstRow(oUserUploadDataTableFromExcel);

            //Remove First Row
            oUserUploadDataTableFromExcel.Rows.RemoveAt(0);

            return oUserUploadDataTableFromExcel;
        }

        #endregion

        #region "Account Data Import"
        public  void ProcessTransferedAccountData(AccountDataImportInfo oAccountDataImportInfo, CompanyUserInfo oCompanyUserInfo)
        {
            AccountUploadDAO oAccountDAO = new AccountUploadDAO(oCompanyUserInfo);
            SqlConnection oConn = null;
            SqlTransaction oTrans = null;
            try
            {
                oConn = oAccountDAO.GetConnection();
                oConn.Open();
                oTrans = oConn.BeginTransaction();
                ProcessTransferedAccountData(oAccountDAO, oAccountDataImportInfo, oConn, oTrans, oCompanyUserInfo);
                oTrans.Commit();
                oTrans.Dispose();
                oTrans = null;
                if (oAccountDataImportInfo.IsAlertRaised)
                {
                    Helper.LogInfo(@"Begin: Send Alert for GLData for DataImportID: " + oAccountDataImportInfo.DataImportID.ToString(), oCompanyUserInfo);
                    Alert oAlert = new Alert(oCompanyUserInfo);
                    oAlert.GetUserListAndSendMail(oAccountDataImportInfo.DataImportID, oAccountDataImportInfo.CompanyID, oCompanyUserInfo);
                    Helper.LogInfo(@"End: Send Alert for GLData for DataImportID: " + oAccountDataImportInfo.DataImportID.ToString(), oCompanyUserInfo);
                }
            }
            catch (Exception ex)
            {
                if (oTrans != null)
                {
                    oTrans.Rollback();
                    ResetAccountDataHdrObject(oAccountDataImportInfo, ex);
                }
                throw ex;
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Dispose();
            }
        }

        private  void ProcessTransferedAccountData(AccountUploadDAO oAccountDAO, AccountDataImportInfo oAccountDataImportInfo, SqlConnection oConn, SqlTransaction oTrans, CompanyUserInfo oCompanyUserInfo)
        {

            oAccountDAO.ProcessImportedAccountData(oAccountDataImportInfo, oConn, oTrans);

            Helper.LogInfo("8. Data Processing Complete.", oCompanyUserInfo);
            Helper.LogInfo(" - Status: " + oAccountDataImportInfo.DataImportStatus, oCompanyUserInfo);
            Helper.LogInfo(" - Message: " + oAccountDataImportInfo.ErrorMessageFromSqlServer, oCompanyUserInfo);
            Helper.LogInfo(" - Records Imported: " + oAccountDataImportInfo.RecordsImported.ToString(), oCompanyUserInfo);

            // Data Should be deleted in case there is no warning
            if (oAccountDataImportInfo.DataImportStatus != DataImportStatus.DATAIMPORTWARNING)
                oAccountDataImportInfo.IsDataDeletionRequired = true;

            // Raise exception if dataImportStatus = "FAIL". This exception message will be logged into logfile
            if (oAccountDataImportInfo.DataImportStatus == DataImportStatus.DATAIMPORTFAIL)
                throw new Exception(oAccountDataImportInfo.ErrorMessageToSave);
        }

        public  AccountDataImportInfo GetAccountDataImportInfoForProcessing(DateTime dateRevised, CompanyUserInfo oCompanyUserInfo)
        {
            AccountUploadDAO oAccountDAO = new AccountUploadDAO(oCompanyUserInfo);
            return oAccountDAO.GetAccountDataImportForProcessing(dateRevised);
        }

        public  void ResetAccountDataHdrObject(AccountDataImportInfo oAccountDataImportInfo, Exception ex)
        {
            if (oAccountDataImportInfo.DataImportStatus == DataImportStatus.DATAIMPORTSEVEREWARNING)
            {
                if (string.IsNullOrEmpty(oAccountDataImportInfo.WarningEmailIds))
                    oAccountDataImportInfo.WarningEmailIds = oAccountDataImportInfo.OverridenAcctEmailID;
                else
                    oAccountDataImportInfo.WarningEmailIds = oAccountDataImportInfo.WarningEmailIds + ';' + oAccountDataImportInfo.OverridenAcctEmailID;

                oAccountDataImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTWARNING;
            }
            else
            {
                if (string.IsNullOrEmpty(oAccountDataImportInfo.DataImportStatus))
                    oAccountDataImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
                if (string.IsNullOrEmpty(oAccountDataImportInfo.ErrorMessageToSave))
                    oAccountDataImportInfo.ErrorMessageToSave = ex.Message;
            }
        }

        public  void TransferAndProcessAccountData(DataTable dtExcel, AccountDataImportInfo oAccountDataImportInfo, CompanyUserInfo oCompanyUserHdrInfo)
        {
            AccountUploadDAO oAccountDAO = new AccountUploadDAO(oCompanyUserHdrInfo);
            SqlConnection oConn = null;
            SqlTransaction oTrans = null;
            try
            {
                oConn = oAccountDAO.GetConnection();
                oConn.Open();
                oTrans = oConn.BeginTransaction();

                //Transfer Excel Data To Sql Server
                oAccountDAO.CopyAccountDataFromExcelToSqlServer(dtExcel, oAccountDataImportInfo, oConn, oTrans);
                Helper.LogInfo("7. Data Transfer complete", oCompanyUserHdrInfo);

                //Process Transferred Data
                ProcessTransferedAccountData(oAccountDAO, oAccountDataImportInfo, oConn, oTrans, oCompanyUserHdrInfo);
                oTrans.Commit();
                oTrans.Dispose();
                oTrans = null;
                if (oAccountDataImportInfo.IsAlertRaised)
                {
                    Helper.LogInfo(@"Begin: Send Alert for GLData for DataImportID: " + oAccountDataImportInfo.DataImportID.ToString(), oCompanyUserHdrInfo);
                    Alert oAlert = new Alert(oCompanyUserHdrInfo);
                    oAlert.GetUserListAndSendMail(oAccountDataImportInfo.DataImportID, oAccountDataImportInfo.CompanyID, oCompanyUserHdrInfo);
                    Helper.LogInfo(@"End: Send Alert for GLData for DataImportID: " + oAccountDataImportInfo.DataImportID.ToString(), oCompanyUserHdrInfo);
                }
            }
            catch (Exception ex)
            {
                if (oTrans != null)
                {
                    oTrans.Rollback();
                    //ResetGLDataHdrObject(oGLDataImportInfo, ex);
                }
                throw ex;
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Dispose();
            }
        }

        public  void UpdateDataImportHDR(AccountDataImportInfo oAccountDataImportInfo, CompanyUserInfo oCompanyUserInfo)
        {
            DataImportHdrDAO oDataImportHdrDAO = new DataImportHdrDAO(oCompanyUserInfo);
            oDataImportHdrDAO.UpdateDataImportHDR(oAccountDataImportInfo);
        }

        #endregion

        #region "Generic Methods"
        public  void SendMailToUsers(DataImportHdrInfo oDataImportInfo, CompanyUserInfo oCompanyUserInfo)
        {
            SkyStem.ART.Service.Utility.MailHelper.SendEmailToUserByDataImportStatus(oDataImportInfo.DataImportStatus, oDataImportInfo.SuccessEmailIDs
                , oDataImportInfo.FailureEmailIDs, oDataImportInfo.WarningEmailIds, oDataImportInfo.DataImportTypeID
                , oDataImportInfo.RecordsImported, oDataImportInfo.ProfileName, oDataImportInfo.ErrorMessageToSave
                , ServiceConstants.DEFAULTBUSINESSENTITYID, oDataImportInfo.LanguageID, ServiceConstants.DEFAULTLANGUAGEID
                , oDataImportInfo.DateAdded
                , oDataImportInfo.UserID, oDataImportInfo.RoleID, oDataImportInfo.RecPeriodID, oDataImportInfo.CompanyID
                , oDataImportInfo, oCompanyUserInfo, oDataImportInfo.DataImportID);
        }

        public  void SendMailToUsers(CurrencyDataImportInfo oCurrencyDataImportInfo, CompanyUserInfo oCompanyUserInfo)
        {
            if (oCurrencyDataImportInfo.IsMultiVersionUpload)
            {
                SkyStem.ART.Service.Utility.MailHelper.SendEmailToUserByDataImportStatusMultiVersion(oCurrencyDataImportInfo.DataImportStatus, oCurrencyDataImportInfo.SuccessEmailIDs
                , oCurrencyDataImportInfo.FailureEmailIDs, oCurrencyDataImportInfo.WarningEmailIds, oCurrencyDataImportInfo.DataImportTypeID
                , oCurrencyDataImportInfo.RecordsImported, oCurrencyDataImportInfo.ProfileName, oCurrencyDataImportInfo.ErrorMessageToSave
                , ServiceConstants.DEFAULTBUSINESSENTITYID, oCurrencyDataImportInfo.LanguageID, ServiceConstants.DEFAULTLANGUAGEID
                , oCurrencyDataImportInfo.DateAdded, oCurrencyDataImportInfo.UserAccountInfoCollection
                , oCurrencyDataImportInfo.UserID, oCurrencyDataImportInfo.RoleID, oCurrencyDataImportInfo.RecPeriodID, oCurrencyDataImportInfo.CompanyID
                , oCompanyUserInfo, oCurrencyDataImportInfo.DataImportID, oCurrencyDataImportInfo);

            }
        }

        public  void SendMailToUsers(GLDataImportInfo oGLDataImportInfo, CompanyUserInfo oCompanyUserInfo)
        {
            if (oGLDataImportInfo.IsMultiVersionUpload)
            {
                SkyStem.ART.Service.Utility.MailHelper.SendEmailToUserByDataImportStatusMultiVersion(oGLDataImportInfo.DataImportStatus, oGLDataImportInfo.SuccessEmailIDs
                , oGLDataImportInfo.FailureEmailIDs, oGLDataImportInfo.WarningEmailIds, oGLDataImportInfo.DataImportTypeID
                , oGLDataImportInfo.RecordsImported, oGLDataImportInfo.ProfileName, oGLDataImportInfo.ErrorMessageToSave
                , ServiceConstants.DEFAULTBUSINESSENTITYID, oGLDataImportInfo.LanguageID, ServiceConstants.DEFAULTLANGUAGEID
                , oGLDataImportInfo.DateAdded, oGLDataImportInfo.UserAccountInfoCollection
                , oGLDataImportInfo.UserID, oGLDataImportInfo.RoleID, oGLDataImportInfo.RecPeriodID, oGLDataImportInfo.CompanyID
                , oCompanyUserInfo, oGLDataImportInfo.DataImportID, oGLDataImportInfo);
            }
            else
            {
                SkyStem.ART.Service.Utility.MailHelper.SendEmailToUserByDataImportStatus(oGLDataImportInfo.DataImportStatus, oGLDataImportInfo.SuccessEmailIDs
                , oGLDataImportInfo.FailureEmailIDs, oGLDataImportInfo.WarningEmailIds, oGLDataImportInfo.DataImportTypeID
                , oGLDataImportInfo.RecordsImported, oGLDataImportInfo.ProfileName, oGLDataImportInfo.ErrorMessageToSave
                , ServiceConstants.DEFAULTBUSINESSENTITYID, oGLDataImportInfo.LanguageID, ServiceConstants.DEFAULTLANGUAGEID
                , oGLDataImportInfo.DateAdded
                , oGLDataImportInfo.UserID, oGLDataImportInfo.RoleID, oGLDataImportInfo.RecPeriodID, oGLDataImportInfo.CompanyID
                , oGLDataImportInfo, oCompanyUserInfo, oGLDataImportInfo.DataImportID);

            }
        }

        public  void SendMailToUsers(SubledgerDataImportInfo oSubledgerDataImportInfo, CompanyUserInfo oCompanyUserInfo)
        {
            if (oSubledgerDataImportInfo.IsMultiVersionUpload)
            {
                //if (oSubledgerDataImportInfo.UserAccountInfoCollection != null && oSubledgerDataImportInfo.UserAccountInfoCollection.Count > 0)
                //{
                SkyStem.ART.Service.Utility.MailHelper.SendEmailToUserByDataImportStatusMultiVersion(oSubledgerDataImportInfo.DataImportStatus, oSubledgerDataImportInfo.SuccessEmailIDs
                , oSubledgerDataImportInfo.FailureEmailIDs, oSubledgerDataImportInfo.WarningEmailIds, oSubledgerDataImportInfo.DataImportTypeID
                , oSubledgerDataImportInfo.RecordsImported, oSubledgerDataImportInfo.ProfileName, oSubledgerDataImportInfo.ErrorMessageToSave
                , ServiceConstants.DEFAULTBUSINESSENTITYID, oSubledgerDataImportInfo.LanguageID, ServiceConstants.DEFAULTLANGUAGEID
                , oSubledgerDataImportInfo.DateAdded, oSubledgerDataImportInfo.UserAccountInfoCollection
                , oSubledgerDataImportInfo.UserID, oSubledgerDataImportInfo.RoleID, oSubledgerDataImportInfo.RecPeriodID, oSubledgerDataImportInfo.CompanyID
                , oCompanyUserInfo, oSubledgerDataImportInfo.DataImportID, oSubledgerDataImportInfo);
                //}

            }
            else
            {
                SkyStem.ART.Service.Utility.MailHelper.SendEmailToUserByDataImportStatus(oSubledgerDataImportInfo.DataImportStatus, oSubledgerDataImportInfo.SuccessEmailIDs
              , oSubledgerDataImportInfo.FailureEmailIDs, oSubledgerDataImportInfo.WarningEmailIds, oSubledgerDataImportInfo.DataImportTypeID
              , oSubledgerDataImportInfo.RecordsImported, oSubledgerDataImportInfo.ProfileName, oSubledgerDataImportInfo.ErrorMessageToSave
              , ServiceConstants.DEFAULTBUSINESSENTITYID, oSubledgerDataImportInfo.LanguageID, ServiceConstants.DEFAULTLANGUAGEID
              , oSubledgerDataImportInfo.DateAdded
              , oSubledgerDataImportInfo.UserID, oSubledgerDataImportInfo.RoleID, oSubledgerDataImportInfo.RecPeriodID, oSubledgerDataImportInfo.CompanyID
              , oSubledgerDataImportInfo, oCompanyUserInfo, oSubledgerDataImportInfo.DataImportID);

            }
        }

        public  void SendMailToUsers(UserDataImportInfo oUserDataImportInfo, CompanyUserInfo oCompanyUserInfo)
        {

            DataImportHdrInfo oDataImportHdrInfo = oUserDataImportInfo;
            //If it is successful, send mail to newly created users
            if (oUserDataImportInfo.DataImportStatus == DataImportStatus.DATAIMPORTSUCCESS)
            {
                if (oUserDataImportInfo.CreatedUserList != null)
                {
                    foreach (SkyStem.ART.Client.Model.UserHdrInfo oUser in oUserDataImportInfo.CreatedUserList)
                    {
                        SkyStem.ART.Service.Utility.MailHelper.SendMailToNewUser(oUser, oCompanyUserInfo);
                    }
                }
            }
            //Send mail to user who has perofrmed data import           
            SendMailToUsers(oDataImportHdrInfo, oCompanyUserInfo);
        }


        //Rename column names as per first row
        private  void RenameColumnNameAsPerFirstRow(DataTable dt)
        {
            List<int> unwantedColumnIndexList = new List<int>();
            DataRow dr = dt.Rows[0];
            if (dr != null)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    DataColumn dc = dt.Columns[i];
                    //Rename columnName
                    if (!String.IsNullOrEmpty(dr[i].ToString().Trim()))
                        dc.ColumnName = dr[i].ToString().Trim();
                    else
                        unwantedColumnIndexList.Add(i);
                }
            }
            //Delete columns for which there is no name in first row
            for (int x = unwantedColumnIndexList.Count - 1; x >= 0; x--)
            {
                int index = unwantedColumnIndexList[x];
                DataColumn dc = dt.Columns[index];
                if (dc != null)
                    dt.Columns.Remove(dc);
            }
        }

        //Get a list of  fields
        public  List<string> GetAccountStaticFields()
        {
            List<string> fieldList = new List<string>();
            fieldList.Add(GLDataImportFields.FSCAPTION);
            fieldList.Add(GLDataImportFields.ACCOUNTTYPE);
            fieldList.Add(GLDataImportFields.GLACCOUNTNAME);
            fieldList.Add(GLDataImportFields.GLACCOUNTNUMBER);
            return fieldList;
        }

        public  List<string> GetAllAccountCreationMendatoryFields()
        {
            List<string> fieldList = new List<string>();
            fieldList.Add(AccountDataImportFields.ISPROFITANDLOSS);
            return fieldList;
        }

        //Get a list of Mapper keys for Account hierarchy
        public  List<string> GetAccountKeyFields(DataImportHdrInfo oEntity)
        {
            return oEntity.KeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
        }

        //Get a list of Account unique subset keys
        public  List<string> GetAccountUniqueSubsetFields(DataImportHdrInfo oEntity)
        {
            List<string> fieldList = new List<string>();
            if (!String.IsNullOrEmpty(oEntity.AccountUniqueSubSetKeys))
            {
                string[] arryAccountUniqueKeys = oEntity.AccountUniqueSubSetKeys.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                fieldList = arryAccountUniqueKeys.ToList<string>();
            }
            return fieldList;
        }

        public  List<string> GetAccountMandatoryFields(DataImportHdrInfo oEntity)
        {
            List<string> fieldList = new List<string>();

            if (!String.IsNullOrEmpty(oEntity.AccountUniqueSubSetKeys))
            {
                fieldList.AddRange(GetAccountUniqueSubsetFields(oEntity));
            }
            else
            {
                fieldList.AddRange(GetAccountStaticFields());
                fieldList.AddRange(GetAllAccountCreationMendatoryFields());//888888
                //Get Account Key fields as used by this company
                if (!String.IsNullOrEmpty(oEntity.KeyFields))
                    fieldList.AddRange(GetAccountKeyFields(oEntity));
            }
            return fieldList;
        }
        public  List<string> GetAccountMandatoryFieldsForAccountAttributeLoad(DataImportHdrInfo oEntity)
        {
            List<string> fieldList = new List<string>();

            if (!String.IsNullOrEmpty(oEntity.AccountUniqueSubSetKeys))
            {
                fieldList.AddRange(GetAccountUniqueSubsetFields(oEntity));
            }
            else
            {
                fieldList.AddRange(GetAccountStaticFields());
                if (!String.IsNullOrEmpty(oEntity.KeyFields))
                    fieldList.AddRange(GetAccountKeyFields(oEntity));
            }
            return fieldList;
        }

        //Get all possible Account Fields
        public  List<string> GetAllPossibleAccountFields(DataImportHdrInfo oEntity)
        {
            List<string> fieldList = new List<string>();

            fieldList.AddRange(GetAccountStaticFields());

            //Get Account Key fields as used by this company
            if (!String.IsNullOrEmpty(oEntity.KeyFields))
                fieldList.AddRange(GetAccountKeyFields(oEntity));

            return fieldList;
        }

        /// <summary>
        /// Gets folder name for import type as per company id
        /// </summary>
        /// <param name="companyID">id of the company</param>
        /// <param name="importType">import type</param>
        /// <returns>folder name</returns>
        public  string GetBaseFolder()
        {
            // There will be a Base folder(path from web config). 
            string baseFolderPath = @"";//read base folder path from web config.

            //Read base folder name and physical path
            baseFolderPath = GetAppSettingValue(AppSettingConstants.BASE_FOLDER_FOR_FILES);
            if (baseFolderPath == null)
                //throw new ARTException(5000039);

                //if folder for Files is not created, then create it
                if (!Directory.Exists(baseFolderPath))
                {
                    Directory.CreateDirectory(baseFolderPath);
                }

            return baseFolderPath + @"\";
        }

        public  string GetFolderForDownloadRequests(int companyID, int recPeriodID)
        {
            string folderPath = string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.Append(GetBaseFolder());
            sb.Append(@"\");
            sb.Append(companyID.ToString());
            sb.Append(@"\");
            sb.Append(GetAppSettingValue(AppSettingConstants.FOLDER_FOR_DOWNLOAD_REQUESTS));
            sb.Append(@"\");
            sb.Append(recPeriodID.ToString());
            folderPath = sb.ToString();

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            return folderPath + @"\"; ;
        }

        public  string GetAppSettingValue(string key)
        {
            try
            {
                return ConfigurationSettings.AppSettings[key];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Deserialize Return Value
        /// </summary>
        /// <param name="xmlReturnString"></param>
        /// <returns></returns>
        public  ReturnValue DeSerializeReturnValue(string xmlReturnString)
        {
            ReturnValue oRetVal = null;
            if (!string.IsNullOrEmpty(xmlReturnString))
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(xmlReturnString);
                XmlNode xParentnode = xdoc.GetElementsByTagName("ReturnValue")[0];
                XmlNode xnode = xdoc.GetElementsByTagName("ProfilingData")[0];
                if (xParentnode != null && xnode != null)
                {
                    xParentnode.RemoveChild(xnode);
                    xmlReturnString = xParentnode.OuterXml;
                }

                //Deserialize returned info
                oRetVal = ReturnValue.DeSerialize(xmlReturnString);

                if (xnode != null)
                    oRetVal.ProfilingData = xnode.InnerXml;
            }
            return oRetVal;
        }

        public  List<ClientModel.ExchangeRateInfo> GetExchangeRateByRecPeriod(int RecPeriodID, CompanyUserInfo oCompanyUserInfo)
        {
            IUtility oUtility = RemotingHelper.GetUtilityObject();
            ClientModel.AppUserInfo oAppUserInfo = Helper.GetAppUserFromCompanyUserInfo(oCompanyUserInfo);
            return oUtility.GetCurrencyExchangeRateByRecPeriod(RecPeriodID, oAppUserInfo);
        }
        #endregion

        #region "Task Upload"
        internal  TaskImportInfo GetTaskImportInfoForProcessing(DateTime dateRevised, CompanyUserInfo oCompanyUserInfo)
        {
            TaskUploadDAO oTaskUploadDAO = new TaskUploadDAO(oCompanyUserInfo);
            return oTaskUploadDAO.GetTaskImportForProcessing(dateRevised);
        }
        internal  void UpdateDataImportHDR(TaskImportInfo oTaskImportInfo, CompanyUserInfo oCompanyUserInfo)
        {
            DataImportHdrDAO oDataImportHdrDAO = new DataImportHdrDAO(oCompanyUserInfo);
            oDataImportHdrDAO.UpdateDataImportHDR(oTaskImportInfo);
        }



        //Get list of Mandatory fields in Task Import
        public  List<string> GetTaskImportMandatoryFields()
        {
            List<string> mandatoryFields = new List<string>();
            mandatoryFields.Add(TaskUploadConstants.TaskUploadFields.TASKLISTNAME);
            mandatoryFields.Add(TaskUploadConstants.TaskUploadFields.TASKSUBLISTNAME);
            mandatoryFields.Add(TaskUploadConstants.TaskUploadFields.TASKNAME);
            mandatoryFields.Add(TaskUploadConstants.TaskUploadFields.DESCRIPTION);
            mandatoryFields.Add(TaskUploadConstants.TaskUploadFields.ASSIGNEDTO);
            mandatoryFields.Add(TaskUploadConstants.TaskUploadFields.REVIEWER);
            mandatoryFields.Add(TaskUploadConstants.TaskUploadFields.APPROVER);
            mandatoryFields.Add(TaskUploadConstants.TaskUploadFields.NOTIFY);
            mandatoryFields.Add(TaskUploadConstants.TaskUploadFields.RECURRENCETYPE);
            mandatoryFields.Add(TaskUploadConstants.TaskUploadFields.RECURRENCEFREQUENCY);
            mandatoryFields.Add(TaskUploadConstants.TaskUploadFields.PERIODNUMBER);
            mandatoryFields.Add(TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDATE);
            mandatoryFields.Add(TaskUploadConstants.TaskUploadFields.REVIEWERDUEDATE);
            mandatoryFields.Add(TaskUploadConstants.TaskUploadFields.TASKDUEDATE);
            mandatoryFields.Add(TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDAYS);
            mandatoryFields.Add(TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDAYSBASIS);
            mandatoryFields.Add(TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDAYSSKIPNUMBER);
            mandatoryFields.Add(TaskUploadConstants.TaskUploadFields.REVIEWERDUEDAYS);
            mandatoryFields.Add(TaskUploadConstants.TaskUploadFields.REVIEWERDUEDAYSBASIS);
            mandatoryFields.Add(TaskUploadConstants.TaskUploadFields.REVIEWERDUEDAYSSKIPNUMBER);
            mandatoryFields.Add(TaskUploadConstants.TaskUploadFields.TASKDUEDAYS);
            mandatoryFields.Add(TaskUploadConstants.TaskUploadFields.TASKDUEDAYSBASIS);
            mandatoryFields.Add(TaskUploadConstants.TaskUploadFields.TASKDUEDAYSSKIPNUMBER);
            mandatoryFields.Add(TaskUploadConstants.TaskUploadFields.DAYTYPE);
            return mandatoryFields;
        }
        #endregion

        #region Schedule Rec Item Data Import
        #region "Public Methods"

        public  DataTable GetScheduleRecItemImportDataTableFromExcel(string fullExcelFilePath, string sheetName, CompanyUserInfo oCompanyUserInfo)
        {
            DataTable oDataTableFromExcel = Helper.GetDataTableFromExcel(fullExcelFilePath, sheetName, oCompanyUserInfo);

            //Rename columns as per first row and remove first row.
            RenameColumnNameAsPerFirstRow(oDataTableFromExcel);

            //Remove First Row
            oDataTableFromExcel.Rows.RemoveAt(0);

            return oDataTableFromExcel;
        }

        //Get list of all mandatory fields in Schedule Rec Item Import
        public  List<string> GetScheduleRecItemImportAllMandatoryFields()
        {
            List<string> fieldList = new List<string>();
            fieldList.Add(ScheduleRecItemUploadConstants.Fields.RefNo);
            fieldList.Add(ScheduleRecItemUploadConstants.Fields.ScheduleName);
            fieldList.Add(ScheduleRecItemUploadConstants.Fields.Description);
            fieldList.Add(ScheduleRecItemUploadConstants.Fields.OriginalAmount);
            fieldList.Add(ScheduleRecItemUploadConstants.Fields.LCCYCode);
            fieldList.Add(ScheduleRecItemUploadConstants.Fields.OpenDate);
            fieldList.Add(ScheduleRecItemUploadConstants.Fields.BeginScheduleOn);
            fieldList.Add(ScheduleRecItemUploadConstants.Fields.IncludeOnBeginDate);
            fieldList.Add(ScheduleRecItemUploadConstants.Fields.ScheduleBeginDate);
            fieldList.Add(ScheduleRecItemUploadConstants.Fields.ScheduleEndDate);
            fieldList.Add(ScheduleRecItemUploadConstants.Fields.CalculationFrequency);
            //fieldList.Add(ScheduleRecItemUploadConstants.Fields.TotalInterval);
            //fieldList.Add(ScheduleRecItemUploadConstants.Fields.CurrentInterval);
            return fieldList;
        }

        public  void ResetScheduleRecItemDataHdrObject(ScheduleRecItemImportInfo oScheduleRecItemImportInfo, Exception ex)
        {
            if (oScheduleRecItemImportInfo.DataImportStatus == DataImportStatus.DATAIMPORTSEVEREWARNING)
            {
                oScheduleRecItemImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTWARNING;
            }
            else
            {
                if (string.IsNullOrEmpty(oScheduleRecItemImportInfo.DataImportStatus))
                    oScheduleRecItemImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
                if (string.IsNullOrEmpty(oScheduleRecItemImportInfo.ErrorMessageToSave))
                    oScheduleRecItemImportInfo.ErrorMessageToSave = ex.Message;
            }
        }

        public  void ResetTaskImportInfoObject(TaskImportInfo oTaskImportInfo, Exception ex)
        {
            if (oTaskImportInfo.DataImportStatus == DataImportStatus.DATAIMPORTSEVEREWARNING)
            {
                oTaskImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTWARNING;
            }
            else
            {
                if (string.IsNullOrEmpty(oTaskImportInfo.DataImportStatus))
                    oTaskImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
                if (string.IsNullOrEmpty(oTaskImportInfo.ErrorMessageToSave))
                    oTaskImportInfo.ErrorMessageToSave = ex.Message;
            }
        }
        #endregion
        #region Private Methods
        #endregion
        #endregion

        #region "Currency Data Import"
        #region "Public Methods"
        public  CurrencyDataImportInfo GetCurrencyDataImportInfoForProcessing(DateTime dateRevised, CompanyUserInfo oCompanyUserInfo)
        {
            CurrencyDataImportDAO oCurrencyDataImportDAO = new CurrencyDataImportDAO(oCompanyUserInfo);
            return oCurrencyDataImportDAO.GetCurrencyDataImportForProcessing(dateRevised);
        }

        public  void TransferAndProcessCurrencyData(DataTable dtExcel, CurrencyDataImportInfo oCurrencyDataImportInfo, List<ClientModel.LogInfo> oLogInfoCache, CompanyUserInfo oCompanyUserInfo)
        {
            CurrencyDataImportDAO oCurrencyDataImportDAO = new CurrencyDataImportDAO(oCompanyUserInfo);
            oCurrencyDataImportDAO.LogInfoCache = oLogInfoCache;
            SqlConnection oConn = null;
            SqlTransaction oTrans = null;
            try
            {
                oConn = oCurrencyDataImportDAO.GetConnection();
                oConn.Open();
                oTrans = oConn.BeginTransaction();

                //Transfer Excel Data To Sql Server
                oCurrencyDataImportDAO.CopyCurrencyDataFromExcelToSqlServer(dtExcel, oCurrencyDataImportInfo, oConn, oTrans);
                Helper.LogInfoToCache("7. Data Transfer complete", oLogInfoCache);
                oCurrencyDataImportDAO.InsertDataImportWarning(oCurrencyDataImportInfo, oConn, oTrans);
                //Process Transferred Data
                ProcessTransferedCurrencyData(oCurrencyDataImportDAO, oCurrencyDataImportInfo, oConn, oTrans, oCompanyUserInfo);
                oTrans.Commit();
                oTrans.Dispose();
                oTrans = null;
                if (oCurrencyDataImportInfo.IsAlertRaised)
                {
                    Helper.LogInfoToCache(@"Begin: Send Alert for Currency Data for DataImportID: " + oCurrencyDataImportInfo.DataImportID.ToString(), oLogInfoCache);
                    Alert oAlert = new Alert(oCompanyUserInfo);
                    oAlert.GetUserListAndSendMail(oCurrencyDataImportInfo.DataImportID, oCurrencyDataImportInfo.CompanyID, oCompanyUserInfo);
                    Helper.LogInfoToCache(@"End: Send Alert for Currency Data for DataImportID: " + oCurrencyDataImportInfo.DataImportID.ToString(), oLogInfoCache);
                }
            }
            catch (Exception)
            {
                if (oTrans != null)
                {
                    oTrans.Rollback();
                    //ResetGLDataHdrObject(oGLDataImportInfo, ex);
                }
                throw;
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Dispose();
            }
        }



        public  void ProcessTransferedCurrencyData(CurrencyDataImportInfo oCurrencyDataImportInfo, List<ClientModel.LogInfo> oLogInfoCache, CompanyUserInfo oCompanyUserInfo)
        {
            CurrencyDataImportDAO oCurrencyDataImportDAO = new CurrencyDataImportDAO(oCompanyUserInfo);
            oCurrencyDataImportDAO.LogInfoCache = oLogInfoCache;
            //Process Transfered Data
            SqlConnection oConn = null;
            SqlTransaction oTrans = null;
            try
            {
                oConn = oCurrencyDataImportDAO.GetConnection();
                oConn.Open();
                oTrans = oConn.BeginTransaction();
                ProcessTransferedCurrencyData(oCurrencyDataImportDAO, oCurrencyDataImportInfo, oConn, oTrans, oCompanyUserInfo);
                oTrans.Commit();
                oTrans.Dispose();
                oTrans = null;
                if (oCurrencyDataImportInfo.IsAlertRaised)
                {
                    Helper.LogInfoToCache(@"Begin: Send Alert for Currency Data for DataImportID: " + oCurrencyDataImportInfo.DataImportID.ToString(), oLogInfoCache);
                    Alert oAlert = new Alert(oCompanyUserInfo);
                    oAlert.GetUserListAndSendMail(oCurrencyDataImportInfo.DataImportID, oCurrencyDataImportInfo.CompanyID, oCompanyUserInfo);
                    Helper.LogInfoToCache(@"End: Send Alert for Currency Data for DataImportID: " + oCurrencyDataImportInfo.DataImportID.ToString(), oLogInfoCache);
                }
            }
            catch (Exception ex)
            {
                if (oTrans != null)
                {
                    oTrans.Rollback();
                    ResetCurrencyDataHdrObject(oCurrencyDataImportInfo, ex);
                }
                throw;
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Dispose();
            }
        }

        public  void UpdateDataImportHDR(CurrencyDataImportInfo oCurrencyDataImportInfo, CompanyUserInfo oCompanyUserInfo)
        {
            DataImportHdrDAO oDataImportHdrDAO = new DataImportHdrDAO(oCompanyUserInfo);
            oDataImportHdrDAO.UpdateDataImportHDR(oCurrencyDataImportInfo);
        }

        public  DataTable GetCurrencyDataImportDataTableFromExcel(string fullExcelFilePath, string sheetName, CompanyUserInfo oCompanyUserInfo)
        {
            DataTable oCurrencyDataTableFromExcel;
            FileInfo file = new FileInfo(fullExcelFilePath);
            if (file.Extension.ToLower() == FileExtensions.csv)
                oCurrencyDataTableFromExcel = ExcelHelper.GetDataTableFromImportDelimitedFile(fullExcelFilePath, true);
            else
            {
                oCurrencyDataTableFromExcel = Helper.GetDataTableFromExcel(fullExcelFilePath, sheetName, oCompanyUserInfo);
                //Rename columns as per first row and remove first row.
                RenameColumnNameAsPerFirstRow(oCurrencyDataTableFromExcel);
                //Remove First Row
                oCurrencyDataTableFromExcel.Rows.RemoveAt(0);
            }
            return oCurrencyDataTableFromExcel;
        }

        public  List<string> GetCurrencyDataImportMandatoryFields()
        {
            List<string> fieldList = new List<string>();
            fieldList.Add(CurrencyExchangeUploadConstants.UploadFields.PERIODENDDATE);
            fieldList.Add(CurrencyExchangeUploadConstants.UploadFields.FROMCURRENCYCODE);
            fieldList.Add(CurrencyExchangeUploadConstants.UploadFields.TOCURRENCYCODE);
            fieldList.Add(CurrencyExchangeUploadConstants.UploadFields.RATE);
            return fieldList;
        }
        #endregion

        #region "Private Methods"
        private  void ProcessTransferedCurrencyData(CurrencyDataImportDAO oCurrencyDataImportDAO, CurrencyDataImportInfo oCurrencyDataImportInfo, SqlConnection oConn, SqlTransaction oTrans, CompanyUserInfo oCompanyUserInfo)
        {
            oCurrencyDataImportDAO.ProcessImportedMultiVersionCurrencyData(oCurrencyDataImportInfo, oConn, oTrans);
            Helper.LogInfoToCache("8. Data Processing Complete.", oCurrencyDataImportDAO.LogInfoCache);
            Helper.LogInfoToCache(" - Status: " + oCurrencyDataImportInfo.DataImportStatus, oCurrencyDataImportDAO.LogInfoCache);
            Helper.LogInfoToCache(" - Message: " + oCurrencyDataImportInfo.ErrorMessageFromSqlServer, oCurrencyDataImportDAO.LogInfoCache);
            Helper.LogInfoToCache(" - Records Imported: " + oCurrencyDataImportInfo.RecordsImported.ToString(), oCurrencyDataImportDAO.LogInfoCache);
            // Data Should be deleted in case there is no warning
            if (oCurrencyDataImportInfo.DataImportStatus != DataImportStatus.DATAIMPORTWARNING)
                oCurrencyDataImportInfo.IsDataDeletionRequired = true;
            // Raise exception if dataImportStatus = "FAIL". This exception message will be logged into logfile
            if (oCurrencyDataImportInfo.DataImportStatus == DataImportStatus.DATAIMPORTFAIL
                    || oCurrencyDataImportInfo.DataImportStatus == DataImportStatus.DATAIMPORTSEVEREWARNING)
                throw new Exception(oCurrencyDataImportInfo.ErrorMessageToSave);
        }

        public  void ResetCurrencyDataHdrObject(CurrencyDataImportInfo oCurrencyDataImportInfo, Exception ex)
        {
            if (oCurrencyDataImportInfo.DataImportStatus == DataImportStatus.DATAIMPORTSEVEREWARNING)
            {
                if (string.IsNullOrEmpty(oCurrencyDataImportInfo.WarningEmailIds))
                    oCurrencyDataImportInfo.WarningEmailIds = oCurrencyDataImportInfo.OverridenAcctEmailID;
                else
                    oCurrencyDataImportInfo.WarningEmailIds = oCurrencyDataImportInfo.WarningEmailIds + ';' + oCurrencyDataImportInfo.OverridenAcctEmailID;
                oCurrencyDataImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTWARNING;
                oCurrencyDataImportInfo.DataImportMessageDetailInfoList = new List<ClientModel.DataImportMessageDetailInfo>();
            }
            else
            {
                if (string.IsNullOrEmpty(oCurrencyDataImportInfo.DataImportStatus))
                    oCurrencyDataImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
                if (string.IsNullOrEmpty(oCurrencyDataImportInfo.ErrorMessageToSave))
                    oCurrencyDataImportInfo.ErrorMessageToSave = ex.Message;
            }
        }

        #endregion
        #endregion

        #region Alert
        public  List<ClientModel.AccountHdrInfo> GetAccountInformationForCompanyAlertMail(ClientModel.CompanyAlertDetailUserInfo oCompanyAlertDetailUserInfo)
        {
            IAccount oAccount = RemotingHelper.GetAccountObject();
            ClientModel.AppUserInfo oAppUserInfo = new ClientModel.AppUserInfo();
            oAppUserInfo.CompanyID = oCompanyAlertDetailUserInfo.CompanyID;
            List<ClientModel.AccountHdrInfo> oListAccountHdrInfo = oAccount.GetAccountInformationForCompanyAlertMail(oCompanyAlertDetailUserInfo.RecPeriodID, oCompanyAlertDetailUserInfo.UserID.Value, oCompanyAlertDetailUserInfo.RoleID.Value, oCompanyAlertDetailUserInfo.CompanyAlertDetailID.Value, oAppUserInfo);
            return oListAccountHdrInfo;
        }
        public  List<ClientModel.CompanyAlertInfo> GetRaiseAlertData(CompanyUserInfo oCompanyUserInfo)
        {
            IAlert oAlertClient = RemotingHelper.GetAlertObject();
            ClientModel.AppUserInfo oAppUserInfo = new ClientModel.AppUserInfo();
            oAppUserInfo.CompanyID = oCompanyUserInfo.CompanyID;
            List<ClientModel.CompanyAlertInfo> oCompanyAlertInfoList = oAlertClient.GetRaiseAlertData(oAppUserInfo);
            return oCompanyAlertInfoList;
        }
        public  void CreateDataForCompanyAlertID(ClientModel.CompanyAlertInfo oCompanyAlertInfo, CompanyUserInfo oCompanyUserInfo)
        {
            IAlert oAlertClient = RemotingHelper.GetAlertObject();
            ClientModel.AppUserInfo oAppUserInfo = new ClientModel.AppUserInfo();
            oAppUserInfo.CompanyID = oCompanyUserInfo.CompanyID;
            oAlertClient.CreateDataForCompanyAlertID(oCompanyAlertInfo, oAppUserInfo);
        }
        public  List<ClientModel.CompanyAlertDetailUserInfo> GetAlertMailDataForCompanyAlertID(ClientModel.CompanyAlertInfo oCompanyAlertInfo, CompanyUserInfo oCompanyUserInfo)
        {
            IAlert oAlertClient = RemotingHelper.GetAlertObject();
            ClientModel.AppUserInfo oAppUserInfo = new ClientModel.AppUserInfo();
            oAppUserInfo.CompanyID = oCompanyUserInfo.CompanyID;
            List<ClientModel.CompanyAlertDetailUserInfo> oCompanyAlertDetailUserInfoList = oAlertClient.GetAlertMailDataForCompanyAlertID(oCompanyAlertInfo, oAppUserInfo);
            return oCompanyAlertDetailUserInfoList;
        }
        public  void UpdateSentMailStatus(List<ClientModel.CompanyAlertDetailUserInfo> oCompanyAlertDetailUserInfoList, CompanyUserInfo oCompanyUserInfo)
        {
            IAlert oAlertClient = RemotingHelper.GetAlertObject();
            ClientModel.AppUserInfo oAppUserInfo = new ClientModel.AppUserInfo();
            oAppUserInfo.CompanyID = oCompanyUserInfo.CompanyID;
            oAlertClient.UpdateSentMailStatus(oCompanyAlertDetailUserInfoList, oAppUserInfo);

        }
        public  List<ClientModel.CompanyAlertDetailUserInfo> GetUserListForNewAccountAlert(int dataImportID, int companyID, CompanyUserInfo oCompanyUserInfo)
        {
            IAlert oAlertClient = RemotingHelper.GetAlertObject();
            ClientModel.AppUserInfo oAppUserInfo = new ClientModel.AppUserInfo();
            oAppUserInfo.CompanyID = oCompanyUserInfo.CompanyID;
            List<ClientModel.CompanyAlertDetailUserInfo> oCompanyAlertDetailUserInfoList = oAlertClient.GetUserListForNewAccountAlert(dataImportID, companyID, oAppUserInfo);
            return oCompanyAlertDetailUserInfoList;
        }
        public  List<ClientModel.CompanyAlertDetailInfo> GetCompanyAlertDetail(long? CompanyAlertDetailID, CompanyUserInfo oCompanyUserInfo)
        {
            IAlert oAlertClient = RemotingHelper.GetAlertObject();
            ClientModel.AppUserInfo oAppUserInfo = new ClientModel.AppUserInfo();
            oAppUserInfo.CompanyID = oCompanyUserInfo.CompanyID;
            List<ClientModel.CompanyAlertDetailInfo> oCompanyAlertDetailInfoList = oAlertClient.GetCompanyAlertDetail(CompanyAlertDetailID, oAppUserInfo);
            return oCompanyAlertDetailInfoList;
        }
        public  List<ClientModel.TaskHdrInfo> GetTaskInformationForCompanyAlertMail(ClientModel.CompanyAlertDetailUserInfo oCompanyAlertDetailUserInfo)
        {
            ITaskMaster oTaskMaster = RemotingHelper.GetTaskMasterObject();
            ClientModel.AppUserInfo oAppUserInfo = new ClientModel.AppUserInfo();
            oAppUserInfo.CompanyID = oCompanyAlertDetailUserInfo.CompanyID;
            List<ClientModel.TaskHdrInfo> oListTaskHdrInfo = oTaskMaster.GetTaskInformationForCompanyAlertMail(oCompanyAlertDetailUserInfo.RecPeriodID, oCompanyAlertDetailUserInfo.UserID.Value, oCompanyAlertDetailUserInfo.RoleID.Value, oCompanyAlertDetailUserInfo.CompanyAlertDetailID.Value, oCompanyAlertDetailUserInfo.LanguageID, oAppUserInfo);
            return oListTaskHdrInfo;
        }



        #endregion

        public  string GetSheetName(Enums.DataImportType dataImportType, int? ImportTemplateID, int? CompanyID)
        {
            string sheetName = "";
            ClientModel.AppUserInfo oAppUserInfo = new ClientModel.AppUserInfo();
            oAppUserInfo.CompanyID = CompanyID;
            switch (dataImportType)
            {
                case Enums.DataImportType.GLData:
                    if (ImportTemplateID.HasValue && ImportTemplateID != Convert.ToInt32(ServiceConstants.ART_TEMPLATE))
                    {
                        IDataImport oDataImport = RemotingHelper.GetDataImportObject();
                        sheetName = oDataImport.GetImportTemplateSheetName(ImportTemplateID.Value, oAppUserInfo);
                    }
                    else
                        sheetName = ServiceConstants.GLDATA_SHEETNAME;
                    break;
                case Enums.DataImportType.SubledgerData:
                    if (ImportTemplateID.HasValue && ImportTemplateID != Convert.ToInt32(ServiceConstants.ART_TEMPLATE))
                    {
                        IDataImport oDataImport = RemotingHelper.GetDataImportObject();
                        sheetName = oDataImport.GetImportTemplateSheetName(ImportTemplateID.Value, oAppUserInfo);
                    }
                    else
                        sheetName = ServiceConstants.SUBLEDGER_SHEETNAME;
                    break;
                case Enums.DataImportType.AccountAttributeList:
                    sheetName = ServiceConstants.ACCOUNTATTRIBUTE_SHEETNAME;
                    break;
                case Enums.DataImportType.GLTBS:
                    sheetName = ServiceConstants.MATCHING_SHEETNAME;
                    break;
                case Enums.DataImportType.NBF:
                    sheetName = ServiceConstants.MATCHING_SHEETNAME;
                    break;
                case Enums.DataImportType.MultilingualUpload:
                    sheetName = MultilingualUploadConstants.SheetName;
                    break;
                case Enums.DataImportType.ScheduleRecItems:
                    sheetName = ScheduleRecItemUploadConstants.SheetName;
                    break;
                case Enums.DataImportType.GeneralTaskImport:
                    sheetName = TaskUploadConstants.SheetName;
                    break;
            }
            return sheetName;

        }

        public  ClientModel.DataImportMessageInfo GetDataImportMessageInfo(short DataImportmessageID, int? CompanyID)
        {
            ClientModel.DataImportMessageInfo oDataImportMessageInfo = null;
            List<ClientModel.DataImportMessageInfo> oDataImportMessageInfoList;
            ClientModel.AppUserInfo oAppUserInfo = new ClientModel.AppUserInfo();
            oAppUserInfo.CompanyID = CompanyID;
            IDataImport oDataImport = RemotingHelper.GetDataImportObject();
            oDataImportMessageInfoList = oDataImport.GetDataImportMessageList(oAppUserInfo);
            oDataImportMessageInfo = oDataImportMessageInfoList.Find(obj => obj.DataImportMessageID == DataImportmessageID);

            return oDataImportMessageInfo;
        }
        public  DataTable CreateDataImportMessageTable()
        {
            DataTable oDataTable = new DataTable(DataImportMessageConstants.TableName);
            DataColumn dcFieldLabelID = new DataColumn(DataImportMessageConstants.Fields.ImportFieldID, typeof(System.Int32));
            dcFieldLabelID.ExtendedProperties.Add(DataImportMessageConstants.Attributes.IsLabelID, "false");
            dcFieldLabelID.ExtendedProperties.Add(DataImportMessageConstants.Attributes.IsVisible, "false");
            oDataTable.Columns.Add(dcFieldLabelID);

            DataColumn dcField = new DataColumn(DataImportMessageConstants.Fields.ImportField, typeof(System.String));
            dcField.ExtendedProperties.Add(DataImportMessageConstants.Attributes.IsLabelID, "false");
            dcField.ExtendedProperties.Add(DataImportMessageConstants.Attributes.IsVisible, "true");
            dcField.ExtendedProperties.Add(DataImportMessageConstants.Attributes.HeaderLabelID, 2104);
            oDataTable.Columns.Add(dcField);

            DataColumn dcMessageLabelID = new DataColumn(DataImportMessageConstants.Fields.MessageLabelID, typeof(System.Int32));
            dcMessageLabelID.ExtendedProperties.Add(DataImportMessageConstants.Attributes.IsLabelID, "true");
            dcMessageLabelID.ExtendedProperties.Add(DataImportMessageConstants.Attributes.IsVisible, "false");
            oDataTable.Columns.Add(dcMessageLabelID);

            DataColumn dcMessage = new DataColumn(DataImportMessageConstants.Fields.Message, typeof(System.String));
            dcMessage.ExtendedProperties.Add(DataImportMessageConstants.Attributes.IsLabelID, "false");
            dcMessage.ExtendedProperties.Add(DataImportMessageConstants.Attributes.IsVisible, "true");
            dcMessage.ExtendedProperties.Add(DataImportMessageConstants.Attributes.HeaderLabelID, 1051);
            dcMessage.ExtendedProperties.Add(DataImportMessageConstants.Attributes.LabelFieldName, DataImportMessageConstants.Fields.MessageLabelID);
            oDataTable.Columns.Add(dcMessage);

            DataColumn dcAllowed = new DataColumn(DataImportMessageConstants.Fields.Allowed, typeof(System.Int32));
            dcAllowed.ExtendedProperties.Add(DataImportMessageConstants.Attributes.IsLabelID, "false");
            dcAllowed.ExtendedProperties.Add(DataImportMessageConstants.Attributes.IsVisible, "true");
            dcAllowed.ExtendedProperties.Add(DataImportMessageConstants.Attributes.HeaderLabelID, 2880);
            oDataTable.Columns.Add(dcAllowed);

            DataColumn dcActual = new DataColumn(DataImportMessageConstants.Fields.Actual, typeof(System.Int32));
            dcActual.ExtendedProperties.Add(DataImportMessageConstants.Attributes.IsLabelID, "false");
            dcActual.ExtendedProperties.Add(DataImportMessageConstants.Attributes.IsVisible, "true");
            dcActual.ExtendedProperties.Add(DataImportMessageConstants.Attributes.HeaderLabelID, 2881);
            oDataTable.Columns.Add(dcActual);
            return oDataTable;
        }
        public  string GetImportTemplateFieldName(SkyStem.ART.Client.Model.ImportTemplateFieldMappingInfo oImportTemplateFieldMappingInfo, int LanguageID, int DefaultLanguageID, CompanyUserInfo oCompanyUserInfo)
        {
            string ImportTemplateFieldName;
            if (!string.IsNullOrEmpty(oImportTemplateFieldMappingInfo.ImportTemplateField))
                ImportTemplateFieldName = oImportTemplateFieldMappingInfo.ImportTemplateField;
            else
            {

                ImportTemplateFieldName = Helper.GetSinglePhraseValue(oImportTemplateFieldMappingInfo.ImportFieldLabelID.Value, ServiceConstants.DEFAULTBUSINESSENTITYID, LanguageID, DefaultLanguageID, oCompanyUserInfo);
            }
            return ImportTemplateFieldName;
        }
        public  DataTable CreateDataImportMandatoryFieldsNotPresentMessageTable()
        {
            DataTable oDataTable = new DataTable(DataImportMessageConstants.TableName);
            DataColumn dcFieldLabelID = new DataColumn(DataImportMessageConstants.Fields.ImportFieldID, typeof(System.Int32));
            dcFieldLabelID.ExtendedProperties.Add(DataImportMessageConstants.Attributes.IsLabelID, "false");
            dcFieldLabelID.ExtendedProperties.Add(DataImportMessageConstants.Attributes.IsVisible, "false");
            oDataTable.Columns.Add(dcFieldLabelID);

            DataColumn dcField = new DataColumn(DataImportMessageConstants.Fields.ImportField, typeof(System.String));
            dcField.ExtendedProperties.Add(DataImportMessageConstants.Attributes.IsLabelID, "false");
            dcField.ExtendedProperties.Add(DataImportMessageConstants.Attributes.IsVisible, "true");
            dcField.ExtendedProperties.Add(DataImportMessageConstants.Attributes.HeaderLabelID, 2104);
            oDataTable.Columns.Add(dcField);

            DataColumn dcMessageLabelID = new DataColumn(DataImportMessageConstants.Fields.MessageLabelID, typeof(System.Int32));
            dcMessageLabelID.ExtendedProperties.Add(DataImportMessageConstants.Attributes.IsLabelID, "true");
            dcMessageLabelID.ExtendedProperties.Add(DataImportMessageConstants.Attributes.IsVisible, "false");
            oDataTable.Columns.Add(dcMessageLabelID);

            DataColumn dcMessage = new DataColumn(DataImportMessageConstants.Fields.Message, typeof(System.String));
            dcMessage.ExtendedProperties.Add(DataImportMessageConstants.Attributes.IsLabelID, "false");
            dcMessage.ExtendedProperties.Add(DataImportMessageConstants.Attributes.IsVisible, "false");
            dcMessage.ExtendedProperties.Add(DataImportMessageConstants.Attributes.HeaderLabelID, 1051);
            dcMessage.ExtendedProperties.Add(DataImportMessageConstants.Attributes.LabelFieldName, DataImportMessageConstants.Fields.MessageLabelID);
            oDataTable.Columns.Add(dcMessage);

            DataColumn dcAllowed = new DataColumn(DataImportMessageConstants.Fields.Allowed, typeof(System.Int32));
            dcAllowed.ExtendedProperties.Add(DataImportMessageConstants.Attributes.IsLabelID, "false");
            dcAllowed.ExtendedProperties.Add(DataImportMessageConstants.Attributes.IsVisible, "false");
            dcAllowed.ExtendedProperties.Add(DataImportMessageConstants.Attributes.HeaderLabelID, 2880);
            oDataTable.Columns.Add(dcAllowed);

            DataColumn dcActual = new DataColumn(DataImportMessageConstants.Fields.Actual, typeof(System.Int32));
            dcActual.ExtendedProperties.Add(DataImportMessageConstants.Attributes.IsLabelID, "false");
            dcActual.ExtendedProperties.Add(DataImportMessageConstants.Attributes.IsVisible, "false");
            dcActual.ExtendedProperties.Add(DataImportMessageConstants.Attributes.HeaderLabelID, 2881);
            oDataTable.Columns.Add(dcActual);
            return oDataTable;
        }
        public  List<SkyStem.ART.Client.Model.AccountHdrInfo> GetAccountInformationWithBalanceChange(List<string> AccountInfoCollection, int CompanyID)
        {
            List<long> oAccountIDList = new List<long>();
            foreach (var AccountInfo in AccountInfoCollection)
            {
                long ActID;
                if (long.TryParse(AccountInfo.Split('|')[0], out ActID))
                    oAccountIDList.Add(ActID);
            }
            IAccount oAccount = RemotingHelper.GetAccountObject();
            ClientModel.AppUserInfo oAppUserInfo = new ClientModel.AppUserInfo();
            oAppUserInfo.CompanyID = CompanyID;
            List<SkyStem.ART.Client.Model.AccountHdrInfo> oListAccountHdrInfo = oAccount.GetAccountHdrInfo(oAccountIDList, oAppUserInfo);
            foreach (var AccountInfo in AccountInfoCollection)
            {
                var AccountInfoArr = AccountInfo.Split('|');
                long ActID;
                if (long.TryParse(AccountInfoArr[0], out ActID))
                {
                    oListAccountHdrInfo.Find(obj => obj.AccountID.Value == ActID).ExistingGLBalanceRCCY = AccountInfoArr[1] + " " + SharedHelper.GetDisplayDecimalValue(Convert.ToDecimal(AccountInfoArr[2]), DecimalConstants.DECIMAL_PLACES_FOR_MATH_ROUND);
                    oListAccountHdrInfo.Find(obj => obj.AccountID.Value == ActID).CurrentGLBalanceRCCY = AccountInfoArr[3] + " " + SharedHelper.GetDisplayDecimalValue(Convert.ToDecimal(AccountInfoArr[4]), DecimalConstants.DECIMAL_PLACES_FOR_MATH_ROUND);
                    oListAccountHdrInfo.Find(obj => obj.AccountID.Value == ActID).ExistingGLBalanceBCCY = AccountInfoArr[5] + " " + SharedHelper.GetDisplayDecimalValue(Convert.ToDecimal(AccountInfoArr[6]), DecimalConstants.DECIMAL_PLACES_FOR_MATH_ROUND);
                    oListAccountHdrInfo.Find(obj => obj.AccountID.Value == ActID).CurrentGLBalanceBCCY = AccountInfoArr[7] + " " + SharedHelper.GetDisplayDecimalValue(Convert.ToDecimal(AccountInfoArr[8]), DecimalConstants.DECIMAL_PLACES_FOR_MATH_ROUND);
                    oListAccountHdrInfo.Find(obj => obj.AccountID.Value == ActID).ShowBalanceChangeColumnInMail = true;
                }
            }
            return oListAccountHdrInfo;
        }

        public  List<SkyStem.ART.Client.Model.AccountHdrInfo> GetAccountInformationWithKeyValue(List<string> AccountInfoCollection, int CompanyID)
        {
            List<long> oAccountIDList = new List<long>();
            foreach (var AccountInfo in AccountInfoCollection)
            {
                long ActID;
                if (long.TryParse(AccountInfo.Split('|')[0], out ActID))
                    oAccountIDList.Add(ActID);
            }
            IAccount oAccount = RemotingHelper.GetAccountObject();
            ClientModel.AppUserInfo oAppUserInfo = new ClientModel.AppUserInfo();
            oAppUserInfo.CompanyID = CompanyID;
            List<SkyStem.ART.Client.Model.AccountHdrInfo> oListAccountHdrInfo = oAccount.GetAccountHdrInfo(oAccountIDList, oAppUserInfo);
            foreach (var AccountInfo in AccountInfoCollection)
            {
                var AccountInfoArr = AccountInfo.Split('|');
                long ActID;
                if (long.TryParse(AccountInfoArr[0], out ActID))
                {
                    ClientModel.AccountHdrInfo oAccountHdrInfo = oListAccountHdrInfo.FirstOrDefault(obj => obj.AccountID.Value == ActID);
                    if (oAccountHdrInfo != null)
                    {
                        for (int i = 1; i < AccountInfoArr.Length; i++)
                        {
                            var KeyValArr = AccountInfoArr[i].Split('^');
                            if (KeyValArr.Length == 2)
                            {
                                decimal val = 0;
                                switch (KeyValArr[0])
                                {
                                    case "GLBalanceBCCY-Old":
                                        if (Decimal.TryParse(KeyValArr[1], out val))
                                            oAccountHdrInfo.ExistingGLBalanceBCCY = SharedHelper.GetDisplayDecimalValue(val, DecimalConstants.DECIMAL_PLACES_FOR_MATH_ROUND);
                                        break;
                                    case "GLBalanceBCCY-New":
                                        if (Decimal.TryParse(KeyValArr[1], out val))
                                            oAccountHdrInfo.CurrentGLBalanceBCCY = SharedHelper.GetDisplayDecimalValue(val, DecimalConstants.DECIMAL_PLACES_FOR_MATH_ROUND);
                                        break;
                                    case "GLBalanceRCCY-Old":
                                        if (Decimal.TryParse(KeyValArr[1], out val))
                                            oAccountHdrInfo.ExistingGLBalanceRCCY = SharedHelper.GetDisplayDecimalValue(val, DecimalConstants.DECIMAL_PLACES_FOR_MATH_ROUND);
                                        break;
                                    case "GLBalanceRCCY-New":
                                        if (Decimal.TryParse(KeyValArr[1], out val))
                                            oAccountHdrInfo.CurrentGLBalanceRCCY = SharedHelper.GetDisplayDecimalValue(val, DecimalConstants.DECIMAL_PLACES_FOR_MATH_ROUND);
                                        break;
                                }
                                oAccountHdrInfo.ShowBalanceChangeColumnInMail = true;
                            }
                        }
                    }
                }
            }
            return oListAccountHdrInfo;
        }
        public  ClientModel.UserHdrInfo GetUserDetail(int UserID, int CompanyID)
        {
            ClientModel.AppUserInfo oAppUserInfo = new ClientModel.AppUserInfo();
            oAppUserInfo.CompanyID = CompanyID;
            IUser oUserClient = RemotingHelper.GetUserObject();
            ClientModel.UserHdrInfo GetUserDetail = oUserClient.GetUserDetail(UserID, oAppUserInfo);
            return GetUserDetail;
        }
        //Rename And Trim Column Names
        //public  void RenameAndTrimColumnNames(DataTable dt)
        //{
        //    string currentColumnName = string.Empty;
        //    DataRow dr = dt.Rows[0];
        //    if (dr != null)
        //    {
        //        for (int i = 0; i < dt.Columns.Count; i++)
        //        {
        //            DataColumn dc = dt.Columns[i];
        //            //Rename columnName
        //            currentColumnName = dc.ColumnName;

        //                dc.ColumnName = Helper.TrimAndMakeLower(currentColumnName);

        //        }
        //    }
        //}

        #region "FTP DataImport"
        public  List<ClientModel.UserFTPConfigurationInfo> GetFtpUsers(CompanyUserInfo oCompanyUserInfo)
        {
            FTPDataImportDAO oFTPDataImportDAO = new FTPDataImportDAO(oCompanyUserInfo);
            return oFTPDataImportDAO.GetFTPUsers();
        }
        public  ClientModel.ReconciliationPeriodInfo GetReconciliationPeriodInfo(DateTime? RecPeriodEndDate, int? CompanyID)
        {

            ClientModel.AppUserInfo oAppUserInfo = new ClientModel.AppUserInfo();
            oAppUserInfo.CompanyID = CompanyID.Value;
            IReconciliationPeriod oReconciliationPeriodClient = RemotingHelper.GetReconciliationPeriodObject();
            ClientModel.ReconciliationPeriodInfo oReconciliationPeriodInfo = oReconciliationPeriodClient.GetReconciliationPeriodInfoByRecPeriodID(null, RecPeriodEndDate, CompanyID, oAppUserInfo);

            return oReconciliationPeriodInfo;
        }

        public  ClientModel.SystemLockdownInfo GetSystemLockdownInfo(ARTEnums.SystemLockdownReason eSystemLockdownReason, ClientModel.UserFTPConfigurationInfo oUserFTPConfigurationInfo, int? RecPeriodID)
        {
            ClientModel.SystemLockdownInfo oSystemLockdownInfo = new ClientModel.SystemLockdownInfo();
            oSystemLockdownInfo.CompanyID = oUserFTPConfigurationInfo.CompanyID;
            oSystemLockdownInfo.RecPeriodID = RecPeriodID;
            oSystemLockdownInfo.AddedBy = oUserFTPConfigurationInfo.LoginID;
            ISystemLockdown oSystemLockdown = RemotingHelper.GetSystemLockdownObject();
            ClientModel.AppUserInfo oAppUserInfo = new ClientModel.AppUserInfo();
            oAppUserInfo.CompanyID = oUserFTPConfigurationInfo.CompanyID;
            List<ClientModel.SystemLockdownReasonMstInfo> oSystemLockdownReasonMstInfoList = oSystemLockdown.GetAllSystemLockdownReasons(oAppUserInfo);
            ClientModel.SystemLockdownReasonMstInfo oSystemLockdownReasonMstInfo = oSystemLockdownReasonMstInfoList.Find(T => T.SystemLockdownReasonID == (short)eSystemLockdownReason);
            if (oSystemLockdownReasonMstInfo != null)
            {
                oSystemLockdownInfo.SystemLockdownReasonID = oSystemLockdownReasonMstInfo.SystemLockdownReasonID;
                oSystemLockdownInfo.SystemLockdownMessage = LanguageUtil.GetValue(oSystemLockdownReasonMstInfo.DescriptionLabelID.Value);
            }
            return oSystemLockdownInfo;
        }
        public  void InsertDataImportHdr(ClientModel.UserFTPConfigurationInfo oUserFTPConfigurationInfo, ClientModel.DataImportHdrInfo oDataImportHrdInfo)
        {
            ClientModel.AppUserInfo oAppUserInfo = new ClientModel.AppUserInfo();
            oAppUserInfo.CompanyID = oUserFTPConfigurationInfo.CompanyID;
            IDataImport oDataImport = RemotingHelper.GetDataImportObject();
            string failureMsg = LanguageUtil.GetValue(1730);
            oDataImport.InsertDataImportWithFailureMsg(oDataImportHrdInfo, failureMsg, oAppUserInfo);

        }
        public  short? GetKeyCount(ClientModel.UserFTPConfigurationInfo oUserFTPConfigurationInfo)
        {
            short? keyCount = null;
            ClientModel.AppUserInfo oAppUserInfo = new ClientModel.AppUserInfo();
            oAppUserInfo.CompanyID = oUserFTPConfigurationInfo.CompanyID;
            IDataImport oDataImport = RemotingHelper.GetDataImportObject();
            keyCount = oDataImport.isKeyMappingDoneByCompanyID(oUserFTPConfigurationInfo.CompanyID.Value, oAppUserInfo);
            return keyCount;
        }


        #endregion
        # region Account Mandatory Fields
        //Returns list of Account key fields
        public  List<string> GetDataImportAllMandatoryFields(int companyID, int recPeriodID)
        {
            List<string> fieldList = new List<string>();

            fieldList.AddRange(DataImportHelper.GetGLDataImportStaticFields());
            fieldList.AddRange(GetAccountFields(companyID, recPeriodID, true));
            return fieldList;
        }
        //Returns Account fields (if mapping is done then unique subset keys, else  fields + key fields)
        private  List<string> GetAccountFields(int companyID, int recPeriodID, bool IsColumnsOptional)
        {
            List<string> fieldList = new List<string>();
            List<string> uniqueSubSetFielsList = GetAccountUniqueSubsetFields(companyID, recPeriodID);
            if (uniqueSubSetFielsList.Count > 0)
            {
                //Add Account Unique Subset Keys
                fieldList.AddRange(uniqueSubSetFielsList);
            }
            else
            {
                fieldList.AddRange(GetAllPossibleAccountFields(companyID));
                fieldList.AddRange(DataImportHelper.GetAllAccountCreationMendatoryFields());
            }
            return fieldList;
        }
        private  List<string> GetAllPossibleAccountFields(int companyID)
        {
            List<string> fieldList = new List<string>();
            //Add Account  Fields
            fieldList.AddRange(DataImportHelper.GetAccountStaticFields());
            //Add mapped key fields
            fieldList.AddRange(DataImportHelper.GetAccountKeyFields(companyID));
            return fieldList;
        }

        public  List<string> GetAccountKeyFields(int companyID)
        {
            IUtility oClient = RemotingHelper.GetUtilityObject();
            ClientModel.AppUserInfo oAppUserInfo = new ClientModel.AppUserInfo();
            oAppUserInfo.CompanyID = companyID;
            string keyFields = oClient.GetKeyFieldsByCompanyID(companyID, oAppUserInfo);
            return keyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
        }
        //Returns list of Account Unique Subset keys
        public  List<string> GetAccountUniqueSubsetFields(int companyID, int recPeriodID)
        {
            IUtility oClient = RemotingHelper.GetUtilityObject();
            ClientModel.AppUserInfo oAppUserInfo = new ClientModel.AppUserInfo();
            oAppUserInfo.CompanyID = companyID;
            string keyFields = oClient.GetAccountUniqueSubsetKeys(companyID, recPeriodID, oAppUserInfo);
            string[] arryAccountUniqueKeys = keyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            return arryAccountUniqueKeys.ToList<string>();
        }
        public  List<string> GetImportTemplateMandatoryFields(int? companyID, int? ImportTemplateID, List<string> MandatoryFieldList)
        {
            IUtility oClient = RemotingHelper.GetUtilityObject();
            ClientModel.AppUserInfo oAppUserInfo = new ClientModel.AppUserInfo();
            oAppUserInfo.CompanyID = companyID;
            List<string> fieldList = new List<string>();
            fieldList.AddRange(oClient.GetImportTemplateMandatoryFields(companyID, ImportTemplateID, MandatoryFieldList, oAppUserInfo));
            return fieldList;
        }
        public  List<string> GetAllMandatoryFields(int? CompanyID, int? ImportTemplateID, int recPeriodID)
        {
            List<string> tmp;
            List<string> tmpMandatoryFieldList = GetDataImportAllMandatoryFields(CompanyID.Value, recPeriodID);
            if (ImportTemplateID.HasValue && ImportTemplateID.Value != Convert.ToInt32(ServiceConstants.ART_TEMPLATE))
                tmp = GetImportTemplateMandatoryFields(CompanyID, ImportTemplateID, tmpMandatoryFieldList);
            else
                tmp = tmpMandatoryFieldList;
            return tmp;

        }
        #endregion
        public  string GetEmailIDWithSeprator(string MailidList)
        {
            if (string.IsNullOrEmpty(MailidList))
                return "";
            else
                return MailidList + ",";
        }

    }
}