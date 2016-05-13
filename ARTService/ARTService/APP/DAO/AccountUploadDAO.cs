using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Service.Model;
using SkyStem.ART.Service.Data;
using System.Data.SqlClient;
using System.Data;
using SkyStem.ART.Service.Utility;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using SkyStem.ART.Service.APP.BLL;
using SkyStem.ART.Client.Model.CompanyDatabase;

namespace SkyStem.ART.Service.APP.DAO
{
    public class AccountUploadDAO : DataImportHdrDAO
    {
        public AccountUploadDAO(CompanyUserInfo oCompanyUserInfo)
            : base(oCompanyUserInfo)
        {
            
        }
        public AccountDataImportInfo GetAccountDataImportForProcessing(DateTime dateRevised)
        {
            AccountDataImportInfo oEntity = new AccountDataImportInfo();
            if (this.IsDataImportProcessingRequired(Enums.DataImportType.AccountDataImport))
            {
                this.GetDataImportForProcessing(oEntity, Enums.DataImportType.AccountDataImport, dateRevised);
            }
            return oEntity;
        }

        //Copy information in Data Table to Destination Table in Sql Server
        public void CopyAccountDataFromExcelToSqlServer(DataTable dtExcelData, AccountDataImportInfo oAccountDataImportInfo, SqlConnection oConnection, SqlTransaction oTransaction)
        {
            DateTime dateAdded = DateTime.Now.Date;
            using (SqlBulkCopy oSqlBlkCopy = new SqlBulkCopy(oConnection, SqlBulkCopyOptions.Default, oTransaction))
            {
                oSqlBlkCopy.DestinationTableName = "GLDataTransit";
                oSqlBlkCopy.ColumnMappings.Add(AddedGLDataImportFields.DATAIMPORTID, GLDataImportTransitFields.DATAIMPORTID);
                oSqlBlkCopy.ColumnMappings.Add(AddedGLDataImportFields.EXCELROWNUMBER, GLDataImportTransitFields.EXCELROWNUMBER);
                //oSqlBlkCopy.ColumnMappings.Add(AddedGLDataImportFields.RECPERIODENDDATE, GLDataImportTransitFields.PERIODENDDATE);
                oSqlBlkCopy.ColumnMappings.Add(GLDataImportFields.COMPANY, GLDataImportTransitFields.COMPANY);
                oSqlBlkCopy.ColumnMappings.Add(GLDataImportFields.GLACCOUNTNUMBER, GLDataImportTransitFields.GLACCOUNTNUMBER);
                oSqlBlkCopy.ColumnMappings.Add(GLDataImportFields.GLACCOUNTNAME, GLDataImportTransitFields.GLACCOUNTNAME);
                oSqlBlkCopy.ColumnMappings.Add(GLDataImportFields.FSCAPTION, GLDataImportTransitFields.FSCAPTION);
                oSqlBlkCopy.ColumnMappings.Add(GLDataImportFields.ACCOUNTTYPE, GLDataImportTransitFields.ACCOUNTTYPE);
                oSqlBlkCopy.ColumnMappings.Add(GLDataImportFields.ISPROFITANDLOSS, GLDataImportTransitFields.ISPROFITANDLOSS);
                string[] arrKeyFields = oAccountDataImportInfo.KeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int k = 0; k < arrKeyFields.Length; k++)
                {
                    string sourceField = arrKeyFields[k].ToString();
                    string targetField = "Key" + (k + 2).ToString();
                    oSqlBlkCopy.ColumnMappings.Add(sourceField, targetField);
                }

                Helper.LogInfo("6. Transfering data to sql server destination table.", this.CompanyUserInfo);

                //Write Data to Sql Server destination Table
                oSqlBlkCopy.WriteToServer(dtExcelData);

                //Mark Data Transfer Flag
                oAccountDataImportInfo.IsDataTransfered = true;
                this.UpdateDataTransferStatus(oAccountDataImportInfo, oConnection, oTransaction);
            }

        }

        //Process imported GLData
        public void ProcessImportedAccountData(AccountDataImportInfo oAccountDataImportInfo, SqlConnection oConnection, SqlTransaction oTransaction)
        {
            string xmlReturnString;
            ReturnValue oRetVal = null;

            SqlCommand oCommand = GetProcessImportedAccountDataCommand(oAccountDataImportInfo);
            if (oConnection.State != ConnectionState.Open)
                oConnection.Open();
            oCommand.Connection = oConnection;
            oCommand.Transaction = oTransaction;
            try
            {
                Helper.LogInfo(@"Begin: AccountData Processing SP for DataImportID: " + oAccountDataImportInfo.DataImportID.ToString(), this.CompanyUserInfo);
                oCommand.ExecuteNonQuery();
                Helper.LogInfo(@"End: AccountData Processing SP for DataImportID: " + oAccountDataImportInfo.DataImportID.ToString(), this.CompanyUserInfo);
            }
            finally
            {
                //Get Return Value from Sql Server
                Helper.LogInfo(@"Read Return Value from AccountData Processing SP for DataImportID: " + oAccountDataImportInfo.DataImportID.ToString(), this.CompanyUserInfo);
                xmlReturnString = oCommand.Parameters["@ReturnValue"].Value.ToString();

                //Deserialize returned info
                oRetVal = DataImportHelper.DeSerializeReturnValue(xmlReturnString);
                oAccountDataImportInfo.IsAlertRaised = false;

                if (oRetVal != null)
                {

                    oAccountDataImportInfo.ErrorMessageFromSqlServer = oRetVal.ErrorMessageForServiceToLog;
                    oAccountDataImportInfo.ErrorMessageToSave = oRetVal.ErrorMessageToSave;
                    oAccountDataImportInfo.DataImportStatus = oRetVal.ImportStatus;

                    if (oRetVal.IsAlertRaised.HasValue)
                    {
                        oAccountDataImportInfo.IsAlertRaised = oRetVal.IsAlertRaised.Value;
                        //if (isAlertRaised)
                        //    Alert.GetUserListAndSendMail(dataImportID, companyID);
                    }
                    if (oRetVal.RecordsImported.HasValue)
                    {
                        oAccountDataImportInfo.RecordsImported = oRetVal.RecordsImported.Value;
                    }
                    else
                    {
                        oAccountDataImportInfo.RecordsImported = 0;
                    }
                    if (oRetVal.WarningReasonID.HasValue)
                    {
                        oAccountDataImportInfo.WarningReasonID = oRetVal.WarningReasonID.Value;
                    }
                }
            }
        }






        #region "Private Methods"
        private SqlCommand GetProcessImportedAccountDataCommand(AccountDataImportInfo oAccountDataImportInfo)
        {
            SqlCommand oCommand = this.CreateCommand();
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.CommandText = "usp_SVC_INS_ProcessGLDataTransitForAccountDataImport";
            this.AddParamsToCommandForAccountDataImportProcessing(oAccountDataImportInfo, oCommand);
            return oCommand;
        }
        private void AddParamsToCommandForAccountDataImportProcessing(AccountDataImportInfo oAccountDataImportInfo, SqlCommand oCommand)
        {
            SqlParameterCollection cmdParamCollectionImport = oCommand.Parameters;
            SqlParameter paramCompanyID = new SqlParameter("@companyID", oAccountDataImportInfo.CompanyID);

            SqlParameter paramDataImportID = new SqlParameter("@dataImportID", oAccountDataImportInfo.DataImportID);

            SqlParameter paramAddedBy = new SqlParameter("@addedBy", oAccountDataImportInfo.AddedBy);

            SqlParameter paramDateAdded = new SqlParameter("@dateAdded", oAccountDataImportInfo.DateAdded);

            SqlParameter paramReturnValue = new SqlParameter("@ReturnValue", SqlDbType.NVarChar, -1);
            paramReturnValue.Direction = ParameterDirection.Output;

            cmdParamCollectionImport.Add(paramCompanyID);
            cmdParamCollectionImport.Add(paramDataImportID);
            cmdParamCollectionImport.Add(paramAddedBy);
            cmdParamCollectionImport.Add(paramDateAdded);
            cmdParamCollectionImport.Add(paramReturnValue);

        }
        #endregion
    }
}
