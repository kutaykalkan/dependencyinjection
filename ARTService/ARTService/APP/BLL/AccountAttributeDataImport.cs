using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Service.Utility;
using System.Data.SqlClient;
using System.Data;
using SkyStem.ART.Service.Data;
using System.Data.OleDb;
using SkyStem.ART.Service.Model;
using SkyStem.ART.Client.Model.CompanyDatabase;
using ClientModel = SkyStem.ART.Client.Model;

//TODO: get rec period to gat capabilities
namespace SkyStem.ART.Service.APP.BLL
{
    public class AccountAttributeDataImport
    {
        #region "Private Attributes"
        private AccountAttributeDataImportInfo oAcctAttrDataImportInfo;
        private CompanyUserInfo CompanyUserInfo;
        private List<ClientModel.LogInfo> LogInfoCache;

        #endregion

        public AccountAttributeDataImport(CompanyUserInfo oCompanyUserInfo)
        {
            this.CompanyUserInfo = oCompanyUserInfo;
            this.LogInfoCache = new List<ClientModel.LogInfo>();
        }

        #region "Public Methods"
        public bool IsProcessingRequiredForAccountAttributeImport()
        {
            bool processingRequired = false;
            try
            {
                oAcctAttrDataImportInfo = DataImportHelper.GetAcctAttrDataImportInfoForProcessing(DateTime.Now, this.CompanyUserInfo);
                if (oAcctAttrDataImportInfo != null && oAcctAttrDataImportInfo.DataImportID > 0)
                {
                    processingRequired = true;
                    Helper.LogInfo(@"1. Account Attribute Data Import required for DataImportID: " + oAcctAttrDataImportInfo.DataImportID.ToString(), this.CompanyUserInfo);
                }
                else
                {
                    Helper.LogInfo(@"1. No Data Available for Account Attribute Data Import.", this.CompanyUserInfo);
                }
            }
            catch (Exception ex)
            {
                oAcctAttrDataImportInfo = null;
                Helper.LogError(@"1. Error in IsProcessingRequiredForAccountAttributeImport: " + ex.Message, this.CompanyUserInfo);
            }
            return processingRequired;
        }

        public void ProcessAccountAttributeImport()
        {
            DataTable dtExcelData = null;
            try
            {
                Helper.LogInfo(@"Start GLData Import for DataImportID: " + oAcctAttrDataImportInfo.DataImportID.ToString(), this.CompanyUserInfo);
                if (!oAcctAttrDataImportInfo.IsForceCommit)//Warning and ForceUpload
                {
                    Helper.LogInfoToCache("2. Start Reading Excel file: " + oAcctAttrDataImportInfo.PhysicalPath, this.LogInfoCache);

                    //dtExcelData = Helper.GetDataTableFromExcel(oAcctAttrDataImportInfo.PhysicalPath, ServiceConstants.ACCOUNTATTRIBUTE_SHEETNAME);
                    dtExcelData = DataImportHelper.GetAcctAttrImportDataTableFromExcel(oAcctAttrDataImportInfo.PhysicalPath, ServiceConstants.ACCOUNTATTRIBUTE_SHEETNAME, this.CompanyUserInfo);

                    if (ValidateSchemaForAccountAttribute(dtExcelData))
                    {

                        Helper.LogInfoToCache("3. Reading Excel file complete.", this.LogInfoCache);

                        AddDataImportIDToDataTable(dtExcelData);//Adding Additional Fields to Excel Data Table

                        SetAttributeFieldAvalibilities(dtExcelData);//Set Account Attribute fields availability

                        ValidateDataLength(dtExcelData);

                        ValidateAcctOwnershipData(dtExcelData);

                        DataImportHelper.TransferAndProcessData(dtExcelData, oAcctAttrDataImportInfo, this.LogInfoCache, this.CompanyUserInfo);
                    }
                }
                else
                {
                    DataImportHelper.ProcessTransferredData(oAcctAttrDataImportInfo, this.LogInfoCache, this.CompanyUserInfo);
                }
            }
            catch (Exception ex)
            {
                DataImportHelper.ResetGLDataHdrObject(oAcctAttrDataImportInfo, ex);
                Helper.LogErrorToCache(ex, this.LogInfoCache);
            }
            finally
            {
                try
                {
                    DataImportHelper.UpdateDataImportHDR(oAcctAttrDataImportInfo, this.CompanyUserInfo);
                }
                catch (Exception ex)
                {
                    Helper.LogErrorToCache("Error while updating DataImportHDR - ", this.LogInfoCache);
                    Helper.LogErrorToCache(ex, this.LogInfoCache);
                }
                try
                {
                    DataImportHelper.SendMailToUsers(oAcctAttrDataImportInfo, this.CompanyUserInfo);
                }
                catch (Exception ex)
                {
                    Helper.LogErrorToCache("Error while sending mail - ", this.LogInfoCache);
                    Helper.LogErrorToCache(ex, this.LogInfoCache);
                }
                try
                {
                    Helper.LogListViaService(this.LogInfoCache, oAcctAttrDataImportInfo.DataImportID, this.CompanyUserInfo);
                    Helper.LogInfo(@"End GLData Import for DataImportID: " + oAcctAttrDataImportInfo.DataImportID.ToString(), this.CompanyUserInfo);
                }
                catch (Exception ex)
                {
                    Helper.LogError("Error while logging - ", this.CompanyUserInfo);
                    Helper.LogError(ex, this.CompanyUserInfo);
                }
            }
        }

        #endregion

        #region "Private Methods"

        private bool ValidateSchemaForAccountAttribute(DataTable dtExcelData)
        {
            bool isValidSchema;
            StringBuilder oSbError = new StringBuilder();

            //Get list of all mandatory fields
            List<string> AADataImporMandatoryFieldList = DataImportHelper.GetAccountMandatoryFieldsForAccountAttributeLoad(oAcctAttrDataImportInfo);

            //Check if all mandatory fields exists in DataTable from Excel
            foreach (string fieldName in AADataImporMandatoryFieldList)
            {
                if (!dtExcelData.Columns.Contains(fieldName))
                {
                    if (!oSbError.ToString().Equals(string.Empty))
                        oSbError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    oSbError.Append(fieldName);
                }
            }
            isValidSchema = string.IsNullOrEmpty(oSbError.ToString());

            //If schema is not valid, generate a multi lingual error message, set failure status, faliure status ID, error message 
            //in GLDataImport object and throw an exception with generated message 
            if (!isValidSchema)
            {
                string errorMessage = Helper.GetSinglePhrase(5000165, 0, oAcctAttrDataImportInfo.LanguageID, oAcctAttrDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);//Mandatory columns not present: {0}

                oAcctAttrDataImportInfo.RecordsImported = 0;
                oAcctAttrDataImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
                oAcctAttrDataImportInfo.DataImportStatusID = (short)Enums.DataImportStatus.Failure;
                oAcctAttrDataImportInfo.ErrorMessageToSave = String.Format(errorMessage, oSbError.ToString());
                throw new Exception(String.Format(errorMessage, oSbError.ToString()));
            }
            return isValidSchema;
        }

        //Get list of all possible field names: No More Used, can be removed
        private string GetFieldNamesForAcctAttrUpload(DataTable dtSchema)
        {
            //prepare list of all possible Account Attribute fields to be used  in select statement
            StringBuilder oSbFieldNames = new StringBuilder();
            List<string> allPossibleFields = new List<string>();
            string[] arrKeyFields = oAcctAttrDataImportInfo.KeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> keyFlds = arrKeyFields.ToList<string>();

            allPossibleFields.AddRange(keyFlds);//key fields
            allPossibleFields.AddRange(Helper.GetAccountAttributeImportMandatoryFields());//Mandatory fields
            allPossibleFields.AddRange(Helper.GetAccountAttributeImportAttributeFields());//Attribute fields

            foreach (string fieldName in allPossibleFields)
            {
                foreach (DataRow dr in dtSchema.Rows)
                {
                    if (dr["Column_Name"].ToString().Trim() == fieldName)
                    {
                        if (!String.IsNullOrEmpty(oSbFieldNames.ToString()))
                        {
                            oSbFieldNames.Append(" , ");
                        }
                        oSbFieldNames.Append("[");
                        oSbFieldNames.Append(dr["Column_Name"].ToString());
                        oSbFieldNames.Append("]");
                        oSbFieldNames.Append(" AS [");
                        oSbFieldNames.Append(fieldName);
                        oSbFieldNames.Append("]");
                    }
                }
            }
            return oSbFieldNames.ToString();
        }

        //Add additional fields to data table
        private void AddDataImportIDToDataTable(DataTable dtExcelData)
        {
            //TODO: should add recPeriodID or Date
            DataColumn dl = new DataColumn(AddedAccountAttributeImportFields.DATAIMPORTID, typeof(System.Int32));
            DataColumn dlRowNumber = new DataColumn(AddedAccountAttributeImportFields.EXCELROWNUMBER, typeof(System.Int32));
            //dtExcelData.Columns.Add("RecPeriodEndDate", typeof(System.String));
            dtExcelData.Columns.Add(dl);
            dtExcelData.Columns.Add(dlRowNumber);
            //DateTime dtPeriodEndDate = new DateTime();
            for (int x = 0; x < dtExcelData.Rows.Count; x++)
            {
                dtExcelData.Rows[x][AddedAccountAttributeImportFields.DATAIMPORTID] = oAcctAttrDataImportInfo.DataImportID;
                dtExcelData.Rows[x][AddedAccountAttributeImportFields.EXCELROWNUMBER] = x + 2;
                //if (DateTime.TryParse(dtExcelData.Rows[x]["Period End Date"].ToString(), out dtPeriodEndDate))
                //    dtExcelData.Rows[x]["RecPeriodEndDate"] = dtPeriodEndDate.ToShortDateString();
                //dtExcelData.Rows[x]["RecPeriodEndDate"] = Convert.ToDateTime(dtPeriodEndDate.ToShortDateString());
            }

        }

        //Set Account Attribute Field Avalibility
        private void SetAttributeFieldAvalibilities(DataTable dtExcelData)
        {
            oAcctAttrDataImportInfo.IsRiskRatingFieldAvailable = Helper.IsFieldPresent(dtExcelData.Columns, AccountAttributeDataImportFields.RISKRATING);
            oAcctAttrDataImportInfo.IsRecTemplateFieldAvailable = Helper.IsFieldPresent(dtExcelData.Columns, AccountAttributeDataImportFields.RECONCILIATIONTEMPLATE);
            oAcctAttrDataImportInfo.IsKeyAccountFieldAvailable = Helper.IsFieldPresent(dtExcelData.Columns, AccountAttributeDataImportFields.ISKEYACCOUNT);
            oAcctAttrDataImportInfo.IsZeroBalanceFieldAvailable = Helper.IsFieldPresent(dtExcelData.Columns, AccountAttributeDataImportFields.ISZEROBALANCEACCOUNT);
            oAcctAttrDataImportInfo.IsSubledgerSourceFieldAvailable = Helper.IsFieldPresent(dtExcelData.Columns, AccountAttributeDataImportFields.SUBLEDGERSOURCE);
            oAcctAttrDataImportInfo.IsRecPolicyFieldAvailable = Helper.IsFieldPresent(dtExcelData.Columns, AccountAttributeDataImportFields.RECONCILIATIONPOLICY);
            oAcctAttrDataImportInfo.IsNatureOfAcctFieldAvailable = Helper.IsFieldPresent(dtExcelData.Columns, AccountAttributeDataImportFields.NATUREOFACCOUNT);
            oAcctAttrDataImportInfo.IsRecProcedureFieldAvailable = Helper.IsFieldPresent(dtExcelData.Columns, AccountAttributeDataImportFields.RECONCILIATIONPROCEDURE);
            oAcctAttrDataImportInfo.IsPreparerFieldAvailable = Helper.IsFieldPresent(dtExcelData.Columns, AccountAttributeDataImportFields.PREPARER);
            oAcctAttrDataImportInfo.IsReviewerFieldAvailable = Helper.IsFieldPresent(dtExcelData.Columns, AccountAttributeDataImportFields.REVIEWER);
            oAcctAttrDataImportInfo.IsApproverFieldAvailable = Helper.IsFieldPresent(dtExcelData.Columns, AccountAttributeDataImportFields.APPROVER);
            oAcctAttrDataImportInfo.IsBackupPreparerFieldAvailable = Helper.IsFieldPresent(dtExcelData.Columns, AccountAttributeDataImportFields.BACKUPPREPARER);
            oAcctAttrDataImportInfo.IsBackupReviewerFieldAvailable = Helper.IsFieldPresent(dtExcelData.Columns, AccountAttributeDataImportFields.BACKUPREVIEWER);
            oAcctAttrDataImportInfo.IsBackupApproverFieldAvailable = Helper.IsFieldPresent(dtExcelData.Columns, AccountAttributeDataImportFields.BACKUPAPPROVER);

            oAcctAttrDataImportInfo.IsReconcilableFieldAvailable = Helper.IsFieldPresent(dtExcelData.Columns, AccountAttributeDataImportFields.RECONCILABLE);
            oAcctAttrDataImportInfo.IsPreparerDueDaysFieldAvailable = Helper.IsFieldPresent(dtExcelData.Columns, AccountAttributeDataImportFields.PREPARERDUEDAYS);
            oAcctAttrDataImportInfo.IsReviewerDueDaysFieldAvailable = Helper.IsFieldPresent(dtExcelData.Columns, AccountAttributeDataImportFields.REVIEWERDUEDAYS);
            oAcctAttrDataImportInfo.IsApproverDueDaysFieldAvailable = Helper.IsFieldPresent(dtExcelData.Columns, AccountAttributeDataImportFields.APPROVERDUEDAYS);
            //oAcctAttrDataImportInfo.IsDayTypeFieldAvailable = Helper.IsFieldPresent(dtExcelData.Columns, AccountAttributeDataImportFields.DAYTYPE);
        }

        //Validate data length if field is available
        private void ValidateDataLength(DataTable dtExcelData)
        {
            StringBuilder oSBError = new StringBuilder();
            string msg = Helper.GetDataLengthErrorMessage(ServiceConstants.DEFAULTBUSINESSENTITYID, oAcctAttrDataImportInfo.LanguageID, oAcctAttrDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);
            string msgInvalidData = Helper.GetInvalidDataErrorMessage(ServiceConstants.DEFAULTBUSINESSENTITYID, oAcctAttrDataImportInfo.LanguageID, oAcctAttrDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);
            string msgCapability = Helper.GetDataInDisabledCapabilityColumnErrorMessage(ServiceConstants.DEFAULTBUSINESSENTITYID, oAcctAttrDataImportInfo.LanguageID, oAcctAttrDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);

            //Todo: pass rec period// static field recPeriod? does it give correct value as set earlier
            //List<CapabilityInfo> oCapabilityInfoCollection = SelectAllCompanyCapabilityByReconciliationPeriodID(oConnection, oTransaction, recPeriod);
            List<CapabilityInfo> oCapabilityInfoCollection = DataImportHelper.SelectAllCompanyCapabilityByReconciliationPeriodID(oAcctAttrDataImportInfo.RecPeriodID, this.CompanyUserInfo);

            bool IsRiskRatingEnabled = IsCapabilityEnabled(oCapabilityInfoCollection, Enums.Capability.RiskRating);
            bool IsZeroBalanceEnabled = IsCapabilityEnabled(oCapabilityInfoCollection, Enums.Capability.ZeroBalanceAccount);
            bool IsKeyAccountEnabled = IsCapabilityEnabled(oCapabilityInfoCollection, Enums.Capability.KeyAccount);
            bool IsDualReviewEnabled = IsCapabilityEnabled(oCapabilityInfoCollection, Enums.Capability.DualLevelReview);
            bool IsDueDateByAccountEnabled = IsCapabilityEnabled(oCapabilityInfoCollection, Enums.Capability.DueDateByAccount);

            string preparer;
            string reviewer;
            string approver;


            //TODO: set capability values.


            for (int x = 0; x < dtExcelData.Rows.Count; x++)
            {
                DataRow dr = dtExcelData.Rows[x];
                string excelRowNumber = dr[AddedGLDataImportFields.EXCELROWNUMBER].ToString();

                preparer = string.Empty;
                reviewer = string.Empty;
                approver = string.Empty;

                #region "Mandatory Fields"
                if (dtExcelData.Columns.Contains(AccountAttributeDataImportFields.FSCAPTION))
                {
                    if (dr[AccountAttributeDataImportFields.FSCAPTION] != DBNull.Value)
                        dr[AccountAttributeDataImportFields.FSCAPTION] = dr[AccountAttributeDataImportFields.FSCAPTION].ToString().Trim();
                    if (dr[AccountAttributeDataImportFields.FSCAPTION].ToString().Length > (int)Enums.DataImportFieldsMaxLength.FSCaption)
                    {
                        oSBError.Append(String.Format(msg, AccountAttributeDataImportFields.FSCAPTION, excelRowNumber, Enums.DataImportFieldsMaxLength.FSCaption));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    }
                }

                if (dtExcelData.Columns.Contains(AccountAttributeDataImportFields.GLACCOUNTNAME))
                {
                    if (dr[AccountAttributeDataImportFields.GLACCOUNTNAME] != DBNull.Value)
                        dr[AccountAttributeDataImportFields.GLACCOUNTNAME] = dr[AccountAttributeDataImportFields.GLACCOUNTNAME].ToString().Trim();
                    if (dr[AccountAttributeDataImportFields.GLACCOUNTNAME].ToString().Length > (int)Enums.DataImportFieldsMaxLength.AccountName)
                    {
                        oSBError.Append(String.Format(msg, AccountAttributeDataImportFields.GLACCOUNTNAME, excelRowNumber, Enums.DataImportFieldsMaxLength.AccountName));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    }
                }

                if (dtExcelData.Columns.Contains(AccountAttributeDataImportFields.GLACCOUNTNUMBER))
                {
                    if (dr[AccountAttributeDataImportFields.GLACCOUNTNUMBER] != DBNull.Value)
                        dr[AccountAttributeDataImportFields.GLACCOUNTNUMBER] = dr[AccountAttributeDataImportFields.GLACCOUNTNUMBER].ToString().Trim();
                    if (dr[AccountAttributeDataImportFields.GLACCOUNTNUMBER].ToString().Length > (int)Enums.DataImportFieldsMaxLength.AccountNumber)
                    {
                        oSBError.Append(String.Format(msg, AccountAttributeDataImportFields.GLACCOUNTNUMBER, excelRowNumber, Enums.DataImportFieldsMaxLength.AccountNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    }
                }

                if (dtExcelData.Columns.Contains(AccountAttributeDataImportFields.ACCOUNTTYPE))
                {
                    if (dr[AccountAttributeDataImportFields.ACCOUNTTYPE] != DBNull.Value)
                        dr[AccountAttributeDataImportFields.ACCOUNTTYPE] = dr[AccountAttributeDataImportFields.ACCOUNTTYPE].ToString().Trim();
                    if (dr[AccountAttributeDataImportFields.ACCOUNTTYPE].ToString().Length > (int)Enums.DataImportFieldsMaxLength.AccountType)
                    {
                        oSBError.Append(String.Format(msg, AccountAttributeDataImportFields.ACCOUNTTYPE, excelRowNumber, Enums.DataImportFieldsMaxLength.AccountType));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    }
                }

                //Keyfields
                if (!String.IsNullOrEmpty(oAcctAttrDataImportInfo.KeyFields))
                {
                    string[] arrKeyFields = oAcctAttrDataImportInfo.KeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int k = 0; k < arrKeyFields.Length; k++)
                    {
                        string sourceField = arrKeyFields[k].ToString();
                        if (dtExcelData.Columns.Contains(sourceField))
                        {
                            if (dr[sourceField] != DBNull.Value)
                                dr[sourceField] = dr[sourceField].ToString().Trim();
                            if (dr[sourceField].ToString().Length > (int)Enums.DataImportFieldsMaxLength.KeyFields)
                            {
                                oSBError.Append(String.Format(msg, sourceField, excelRowNumber, Enums.DataImportFieldsMaxLength.KeyFields));
                                oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            }
                        }

                    }
                }
                #endregion

                #region "Attribute Fields"
                //RiskRating
                if (oAcctAttrDataImportInfo.IsRiskRatingFieldAvailable)
                {
                    if (dr[AccountAttributeDataImportFields.RISKRATING] != DBNull.Value)
                        dr[AccountAttributeDataImportFields.RISKRATING] = dr[AccountAttributeDataImportFields.RISKRATING].ToString().Trim();
                    if (IsRiskRatingEnabled == false && dr[AccountAttributeDataImportFields.RISKRATING].ToString().Length > 0)
                    {
                        oSBError.Append(String.Format(msgCapability, AccountAttributeDataImportFields.RISKRATING, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    }
                    else
                        if (dr[AccountAttributeDataImportFields.RISKRATING].ToString().Length > (int)Enums.DataImportFieldsMaxLength.RiskRating)
                        {
                            oSBError.Append(String.Format(msg, AccountAttributeDataImportFields.RISKRATING, excelRowNumber, Enums.DataImportFieldsMaxLength.RiskRating));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        }
                }
                //RecTemplate
                if (oAcctAttrDataImportInfo.IsRecTemplateFieldAvailable)
                {
                    if (dr[AccountAttributeDataImportFields.RECONCILIATIONTEMPLATE] != DBNull.Value)
                        dr[AccountAttributeDataImportFields.RECONCILIATIONTEMPLATE] = dr[AccountAttributeDataImportFields.RECONCILIATIONTEMPLATE].ToString().Trim();
                    if (dr[AccountAttributeDataImportFields.RECONCILIATIONTEMPLATE].ToString().Length > (int)Enums.DataImportFieldsMaxLength.RecTemplate)
                    {
                        oSBError.Append(String.Format(msg, AccountAttributeDataImportFields.RECONCILIATIONTEMPLATE, excelRowNumber, Enums.DataImportFieldsMaxLength.RecTemplate));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    }
                }
                //KeyAccount
                if (oAcctAttrDataImportInfo.IsKeyAccountFieldAvailable)
                {
                    if (dr[AccountAttributeDataImportFields.ISKEYACCOUNT] != DBNull.Value)
                        dr[AccountAttributeDataImportFields.ISKEYACCOUNT] = dr[AccountAttributeDataImportFields.ISKEYACCOUNT].ToString().Trim();
                    if (IsKeyAccountEnabled == false && dr[AccountAttributeDataImportFields.ISKEYACCOUNT].ToString().Length > 0)
                    {
                        oSBError.Append(String.Format(msgCapability, AccountAttributeDataImportFields.ISKEYACCOUNT, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    }
                    else
                        if (dr[AccountAttributeDataImportFields.ISKEYACCOUNT].ToString().Length > (int)Enums.DataImportFieldsMaxLength.IsKeyAccount)
                        {
                            oSBError.Append(String.Format(msg, AccountAttributeDataImportFields.ISKEYACCOUNT, excelRowNumber, Enums.DataImportFieldsMaxLength.IsKeyAccount));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        }
                }
                //ZeroBalance
                if (oAcctAttrDataImportInfo.IsZeroBalanceFieldAvailable)
                {
                    if (dr[AccountAttributeDataImportFields.ISZEROBALANCEACCOUNT] != DBNull.Value)
                        dr[AccountAttributeDataImportFields.ISZEROBALANCEACCOUNT] = dr[AccountAttributeDataImportFields.ISZEROBALANCEACCOUNT].ToString().Trim();
                    if (IsZeroBalanceEnabled == false && dr[AccountAttributeDataImportFields.ISZEROBALANCEACCOUNT].ToString().Length > 0)
                    {
                        oSBError.Append(String.Format(msgCapability, AccountAttributeDataImportFields.ISZEROBALANCEACCOUNT, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    }
                    else
                        if (dr[AccountAttributeDataImportFields.ISZEROBALANCEACCOUNT].ToString().Length > (int)Enums.DataImportFieldsMaxLength.IsZeroBalance)
                        {
                            oSBError.Append(String.Format(msg, AccountAttributeDataImportFields.ISZEROBALANCEACCOUNT, excelRowNumber, Enums.DataImportFieldsMaxLength.IsZeroBalance));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        }
                }
                //Subledger Source
                if (oAcctAttrDataImportInfo.IsSubledgerSourceFieldAvailable)
                {
                    if (dr[AccountAttributeDataImportFields.SUBLEDGERSOURCE] != DBNull.Value)
                        dr[AccountAttributeDataImportFields.SUBLEDGERSOURCE] = dr[AccountAttributeDataImportFields.SUBLEDGERSOURCE].ToString().Trim();
                    if (dr[AccountAttributeDataImportFields.SUBLEDGERSOURCE].ToString().Length > (int)Enums.DataImportFieldsMaxLength.SubLedgerName)
                    {
                        oSBError.Append(String.Format(msg, AccountAttributeDataImportFields.SUBLEDGERSOURCE, excelRowNumber, Enums.DataImportFieldsMaxLength.SubLedgerName));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    }
                }
                //ReconciliationPloicy
                if (oAcctAttrDataImportInfo.IsRecPolicyFieldAvailable)
                {
                    if (dr[AccountAttributeDataImportFields.RECONCILIATIONPOLICY] != DBNull.Value)
                        dr[AccountAttributeDataImportFields.RECONCILIATIONPOLICY] = dr[AccountAttributeDataImportFields.RECONCILIATIONPOLICY].ToString().Trim();
                    if (dr[AccountAttributeDataImportFields.RECONCILIATIONPOLICY].ToString().Length > (int)Enums.DataImportFieldsMaxLength.AccountPolicyURL)//? is AccountPolicy= rec policy
                    {
                        oSBError.Append(String.Format(msg, AccountAttributeDataImportFields.RECONCILIATIONPOLICY, excelRowNumber, Enums.DataImportFieldsMaxLength.AccountPolicyURL));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    }
                }
                //Nature Of Accounts
                if (oAcctAttrDataImportInfo.IsNatureOfAcctFieldAvailable)
                {
                    if (dr[AccountAttributeDataImportFields.NATUREOFACCOUNT] != DBNull.Value)
                        dr[AccountAttributeDataImportFields.NATUREOFACCOUNT] = dr[AccountAttributeDataImportFields.NATUREOFACCOUNT].ToString().Trim();
                    if (dr[AccountAttributeDataImportFields.NATUREOFACCOUNT].ToString().Length > (int)Enums.DataImportFieldsMaxLength.NatureOfAccount)
                    {
                        oSBError.Append(String.Format(msg, AccountAttributeDataImportFields.NATUREOFACCOUNT, excelRowNumber, Enums.DataImportFieldsMaxLength.NatureOfAccount));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    }
                }
                //Reconciliation Procedure
                if (oAcctAttrDataImportInfo.IsRecProcedureFieldAvailable)
                {
                    if (dr[AccountAttributeDataImportFields.RECONCILIATIONPROCEDURE] != DBNull.Value)
                        dr[AccountAttributeDataImportFields.RECONCILIATIONPROCEDURE] = dr[AccountAttributeDataImportFields.RECONCILIATIONPROCEDURE].ToString().Trim();
                    if (dr[AccountAttributeDataImportFields.RECONCILIATIONPROCEDURE].ToString().Length > (int)Enums.DataImportFieldsMaxLength.ReconciliationProcedure)
                    {
                        oSBError.Append(String.Format(msg, AccountAttributeDataImportFields.RECONCILIATIONPROCEDURE, excelRowNumber, Enums.DataImportFieldsMaxLength.ReconciliationProcedure));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    }
                }
                //Ownership Fields
                //Preparer
                if (oAcctAttrDataImportInfo.IsPreparerFieldAvailable)
                {
                    if (dr[AccountAttributeDataImportFields.PREPARER] != DBNull.Value)
                        dr[AccountAttributeDataImportFields.PREPARER] = dr[AccountAttributeDataImportFields.PREPARER].ToString().Trim();
                    if (dr[AccountAttributeDataImportFields.PREPARER].ToString().Length > (int)Enums.DataImportFieldsMaxLength.Preparer)
                    {
                        oSBError.Append(String.Format(msg, AccountAttributeDataImportFields.PREPARER, excelRowNumber, Enums.DataImportFieldsMaxLength.Preparer));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    }
                }
                //Reviewer
                if (oAcctAttrDataImportInfo.IsReviewerFieldAvailable)
                {
                    if (dr[AccountAttributeDataImportFields.REVIEWER] != DBNull.Value)
                        dr[AccountAttributeDataImportFields.REVIEWER] = dr[AccountAttributeDataImportFields.REVIEWER].ToString().Trim();
                    if (dr[AccountAttributeDataImportFields.REVIEWER].ToString().Length > (int)Enums.DataImportFieldsMaxLength.Reviewer)
                    {
                        oSBError.Append(String.Format(msg, AccountAttributeDataImportFields.REVIEWER, excelRowNumber, Enums.DataImportFieldsMaxLength.Reviewer));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    }
                }
                //Approver
                if (oAcctAttrDataImportInfo.IsApproverFieldAvailable)
                {
                    if (dr[AccountAttributeDataImportFields.APPROVER] != DBNull.Value)
                        dr[AccountAttributeDataImportFields.APPROVER] = dr[AccountAttributeDataImportFields.APPROVER].ToString().Trim();
                    if (IsDualReviewEnabled == false && dr[AccountAttributeDataImportFields.APPROVER].ToString().Length > 0)
                    {
                        oSBError.Append(String.Format(msgCapability, AccountAttributeDataImportFields.APPROVER, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    }
                    else
                    {
                        if (dr[AccountAttributeDataImportFields.APPROVER].ToString().Length > (int)Enums.DataImportFieldsMaxLength.Approver)
                        {
                            oSBError.Append(String.Format(msg, AccountAttributeDataImportFields.APPROVER, excelRowNumber, Enums.DataImportFieldsMaxLength.Approver));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        }
                    }
                }

                //Backup Preparer
                if (oAcctAttrDataImportInfo.IsBackupPreparerFieldAvailable)
                {
                    if (dr[AccountAttributeDataImportFields.BACKUPPREPARER] != DBNull.Value)
                        dr[AccountAttributeDataImportFields.BACKUPPREPARER] = dr[AccountAttributeDataImportFields.BACKUPPREPARER].ToString().Trim();
                    if (dr[AccountAttributeDataImportFields.BACKUPPREPARER].ToString().Length > (int)Enums.DataImportFieldsMaxLength.Preparer)
                    {
                        oSBError.Append(String.Format(msg, AccountAttributeDataImportFields.BACKUPPREPARER, excelRowNumber, Enums.DataImportFieldsMaxLength.Preparer));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    }
                }
                //Backup Reviewer
                if (oAcctAttrDataImportInfo.IsBackupReviewerFieldAvailable)
                {
                    if (dr[AccountAttributeDataImportFields.BACKUPREVIEWER] != DBNull.Value)
                        dr[AccountAttributeDataImportFields.BACKUPREVIEWER] = dr[AccountAttributeDataImportFields.BACKUPREVIEWER].ToString().Trim();
                    if (dr[AccountAttributeDataImportFields.BACKUPREVIEWER].ToString().Length > (int)Enums.DataImportFieldsMaxLength.Reviewer)
                    {
                        oSBError.Append(String.Format(msg, AccountAttributeDataImportFields.BACKUPREVIEWER, excelRowNumber, Enums.DataImportFieldsMaxLength.Reviewer));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    }
                }
                // Backup Approver
                if (oAcctAttrDataImportInfo.IsBackupApproverFieldAvailable)
                {
                    if (dr[AccountAttributeDataImportFields.BACKUPAPPROVER] != DBNull.Value)
                        dr[AccountAttributeDataImportFields.BACKUPAPPROVER] = dr[AccountAttributeDataImportFields.BACKUPAPPROVER].ToString().Trim();
                    if (IsDualReviewEnabled == false && dr[AccountAttributeDataImportFields.BACKUPAPPROVER].ToString().Length > 0)
                    {
                        oSBError.Append(String.Format(msgCapability, AccountAttributeDataImportFields.BACKUPAPPROVER, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    }
                    else
                    {
                        if (dr[AccountAttributeDataImportFields.BACKUPAPPROVER].ToString().Length > (int)Enums.DataImportFieldsMaxLength.Approver)
                        {
                            oSBError.Append(String.Format(msg, AccountAttributeDataImportFields.BACKUPAPPROVER, excelRowNumber, Enums.DataImportFieldsMaxLength.Approver));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        }
                    }
                }

                //Preparer Due Days
                if (oAcctAttrDataImportInfo.IsPreparerDueDaysFieldAvailable)
                {
                    if (dr[AccountAttributeDataImportFields.PREPARERDUEDAYS] != DBNull.Value)
                    {
                        if (!IsDueDateByAccountEnabled)
                        {
                            oSBError.Append(String.Format(msgCapability, AccountAttributeDataImportFields.PREPARERDUEDAYS, excelRowNumber));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        }
                        else
                        {
                            int preparerDueDays = 0;
                            if (Helper.IsValidInt32(dr[AccountAttributeDataImportFields.PREPARERDUEDAYS].ToString(), oAcctAttrDataImportInfo.LanguageID, out preparerDueDays)
                                && preparerDueDays != 0)
                                dr[AccountAttributeDataImportFields.PREPARERDUEDAYS] = preparerDueDays.ToString();
                            else
                            {
                                oSBError.Append(String.Format(msgInvalidData, AccountAttributeDataImportFields.PREPARERDUEDAYS, excelRowNumber));
                                oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            }
                        }
                    }
                }
                //Reviewer Due Days
                if (oAcctAttrDataImportInfo.IsReviewerDueDaysFieldAvailable)
                {
                    if (dr[AccountAttributeDataImportFields.REVIEWERDUEDAYS] != DBNull.Value)
                    {
                        if (!IsDueDateByAccountEnabled)
                        {
                            oSBError.Append(String.Format(msgCapability, AccountAttributeDataImportFields.REVIEWERDUEDAYS, excelRowNumber));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        }
                        else
                        {
                            int reviewerDueDays = 0;
                            if (Helper.IsValidInt32(dr[AccountAttributeDataImportFields.REVIEWERDUEDAYS].ToString(), oAcctAttrDataImportInfo.LanguageID, out reviewerDueDays)
                                && reviewerDueDays != 0)
                                dr[AccountAttributeDataImportFields.REVIEWERDUEDAYS] = reviewerDueDays.ToString();
                            else
                            {
                                oSBError.Append(String.Format(msgInvalidData, AccountAttributeDataImportFields.REVIEWERDUEDAYS, excelRowNumber));
                                oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            }
                        }
                    }
                }
                //Approver Due Days
                if (oAcctAttrDataImportInfo.IsApproverDueDaysFieldAvailable)
                {
                    if (dr[AccountAttributeDataImportFields.APPROVERDUEDAYS] != DBNull.Value)
                    {
                        if (!IsDueDateByAccountEnabled || !IsDualReviewEnabled)
                        {
                            oSBError.Append(String.Format(msgCapability, AccountAttributeDataImportFields.APPROVERDUEDAYS, excelRowNumber));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        }
                        else
                        {
                            int approverDueDays = 0;
                            if (Helper.IsValidInt32(dr[AccountAttributeDataImportFields.APPROVERDUEDAYS].ToString(), oAcctAttrDataImportInfo.LanguageID, out approverDueDays)
                                && approverDueDays != 0)
                                dr[AccountAttributeDataImportFields.APPROVERDUEDAYS] = approverDueDays.ToString();
                            else
                            {
                                oSBError.Append(String.Format(msgInvalidData, AccountAttributeDataImportFields.APPROVERDUEDAYS, excelRowNumber));
                                oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            }
                        }
                    }
                }
                //Day Type
                //if (oAcctAttrDataImportInfo.IsDayTypeFieldAvailable)
                //{
                //    if (dr[AccountAttributeDataImportFields.DAYTYPE] != DBNull.Value)
                //        dr[AccountAttributeDataImportFields.DAYTYPE] = dr[AccountAttributeDataImportFields.DAYTYPE].ToString().Trim();
                //    if (IsDueDateByAccountEnabled == false && dr[AccountAttributeDataImportFields.DAYTYPE].ToString().Length > 0)
                //    {
                //        oSBError.Append(String.Format(msgCapability, AccountAttributeDataImportFields.DAYTYPE, excelRowNumber));
                //        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                //    }
                //    else
                //    {
                //        if (dr[AccountAttributeDataImportFields.DAYTYPE].ToString().Length > (int)Enums.DataImportFieldsMaxLength.DayType)
                //        {
                //            oSBError.Append(String.Format(msg, AccountAttributeDataImportFields.DAYTYPE, excelRowNumber, Enums.DataImportFieldsMaxLength.DayType));
                //            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                //        }
                //    }
                //}
                #endregion
            }
            if (!oSBError.ToString().Equals(String.Empty))
            {
                oAcctAttrDataImportInfo.RecordsImported = 0;
                oAcctAttrDataImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
                oAcctAttrDataImportInfo.DataImportStatusID = (short)Enums.DataImportStatus.Failure;
                oAcctAttrDataImportInfo.ErrorMessageToSave = oSBError.ToString();

                throw new Exception(oSBError.ToString());
            }
        }

        private void ValidateAcctOwnershipData(DataTable dtExcelData)
        {
            string preparer = string.Empty;
            string reviewer = string.Empty;
            string approver = string.Empty;
            string backuppreparer = string.Empty;
            string backupreviewer = string.Empty;
            string backupapprover = string.Empty;
            StringBuilder oSBError = new StringBuilder();

            //Row: {0} Preparer, Reviewer or Approver field has same value which is not valid
            string errorMessage = Helper.GetSinglePhrase(5000236, ServiceConstants.DEFAULTBUSINESSENTITYID, oAcctAttrDataImportInfo.LanguageID, oAcctAttrDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);

            for (int x = 0; x < dtExcelData.Rows.Count; x++)
            {
                DataRow dr = dtExcelData.Rows[x];
                if (oAcctAttrDataImportInfo.IsPreparerFieldAvailable)
                    preparer = dr[AccountAttributeDataImportFields.PREPARER].ToString();

                if (oAcctAttrDataImportInfo.IsReviewerFieldAvailable)
                    reviewer = dr[AccountAttributeDataImportFields.REVIEWER].ToString();

                if (oAcctAttrDataImportInfo.IsApproverFieldAvailable)
                    approver = dr[AccountAttributeDataImportFields.APPROVER].ToString();

                if (oAcctAttrDataImportInfo.IsBackupPreparerFieldAvailable)
                    backuppreparer = dr[AccountAttributeDataImportFields.BACKUPPREPARER].ToString();

                if (oAcctAttrDataImportInfo.IsBackupReviewerFieldAvailable)
                    backupreviewer = dr[AccountAttributeDataImportFields.BACKUPREVIEWER].ToString();

                if (oAcctAttrDataImportInfo.IsBackupApproverFieldAvailable)
                    backupapprover = dr[AccountAttributeDataImportFields.BACKUPAPPROVER].ToString();

                if ( //preparer
                    (!string.IsNullOrEmpty(preparer) && !string.IsNullOrEmpty(reviewer) && preparer == reviewer)
                    || (!string.IsNullOrEmpty(preparer) && !string.IsNullOrEmpty(approver) && preparer == approver)
                    || (!string.IsNullOrEmpty(preparer) && !string.IsNullOrEmpty(backuppreparer) && preparer == backuppreparer)
                    || (!string.IsNullOrEmpty(preparer) && !string.IsNullOrEmpty(backupreviewer) && preparer == backupreviewer)
                    || (!string.IsNullOrEmpty(preparer) && !string.IsNullOrEmpty(backupapprover) && preparer == backupapprover)
                    //reviewer
                    || (!string.IsNullOrEmpty(reviewer) && !string.IsNullOrEmpty(approver) && reviewer == approver)
                    || (!string.IsNullOrEmpty(reviewer) && !string.IsNullOrEmpty(backuppreparer) && reviewer == backuppreparer)
                    || (!string.IsNullOrEmpty(reviewer) && !string.IsNullOrEmpty(backupreviewer) && reviewer == backupreviewer)
                    || (!string.IsNullOrEmpty(reviewer) && !string.IsNullOrEmpty(backupapprover) && reviewer == backupapprover)
                    //approver
                    || (!string.IsNullOrEmpty(approver) && !string.IsNullOrEmpty(backuppreparer) && approver == backuppreparer)
                    || (!string.IsNullOrEmpty(approver) && !string.IsNullOrEmpty(backupreviewer) && approver == backupreviewer)
                    || (!string.IsNullOrEmpty(approver) && !string.IsNullOrEmpty(backupapprover) && approver == backupapprover)
                    //backuppreparer
                    || (!string.IsNullOrEmpty(backuppreparer) && !string.IsNullOrEmpty(backupreviewer) && backuppreparer == backupreviewer)
                    || (!string.IsNullOrEmpty(backuppreparer) && !string.IsNullOrEmpty(backupapprover) && backuppreparer == backupapprover)
                    //backupreviewer
                    || (!string.IsNullOrEmpty(backupreviewer) && !string.IsNullOrEmpty(backupapprover) && backupreviewer == backupapprover)
                  )
                {
                    if (!String.IsNullOrEmpty(oSBError.ToString()))
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    oSBError.Append(string.Format(errorMessage, dr[AddedAccountAttributeImportFields.EXCELROWNUMBER].ToString()));

                }

            }
            if (!String.IsNullOrEmpty(oSBError.ToString()))
            {
                oAcctAttrDataImportInfo.RecordsImported = 0;
                oAcctAttrDataImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
                oAcctAttrDataImportInfo.DataImportStatusID = (short)Enums.DataImportStatus.Failure;
                oAcctAttrDataImportInfo.ErrorMessageToSave = oSBError.ToString();
                throw new Exception(oSBError.ToString());
            }

        }
        #endregion

        #region "To Get Capability values"
        private static bool IsCapabilityEnabled(List<CapabilityInfo> oCapabilityInfoCollection, Enums.Capability eCapability)
        {
            bool isActivated = false;
            int capabilityID = (int)eCapability;
            CapabilityInfo oCapabilityInfo = oCapabilityInfoCollection.Find(c => c.CapabilityID == capabilityID);
            if (oCapabilityInfo != null && oCapabilityInfo.IsActivated.HasValue && oCapabilityInfo.IsActivated.Value)
            {
                isActivated = true;
            }
            return isActivated;
        }
        #endregion

    }
}
