using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SkyStem.ART.Service.Data;
using SkyStem.ART.Service.Model;
using SkyStem.ART.Service.Utility;
using SkyStem.ART.Shared.Data;
using SkyStem.Language.LanguageUtility;
using SkyStem.Language.LanguageUtility.Classes;
using System.Collections;
using SkyStem.ART.Client.Model.CompanyDatabase;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Shared.Utility;

namespace SkyStem.ART.Service.APP.BLL
{
    public class UserUpload
    {

        UserDataImportInfo oUserDataImportInfo;
        private CompanyUserInfo CompanyUserInfo;

        public UserUpload(CompanyUserInfo oCompanyUserInfo)
        {
            this.CompanyUserInfo = oCompanyUserInfo;
        }

        #region "Public Functions"
        public bool IsProcessingRequiredForUserDataImport()
        {
            bool isProcessingRequired;
            try
            {
                this.oUserDataImportInfo = DataImportHelper.GetUserDataImportInfoForProcessing(DateTime.Now, this.CompanyUserInfo );
                if (this.oUserDataImportInfo != null && this.oUserDataImportInfo.DataImportID > 0)
                {
                    isProcessingRequired = true;
                    Helper.LogInfo(@"User Data Import required for DataImportID: " + this.oUserDataImportInfo.DataImportID.ToString(), this.CompanyUserInfo );
                }
                else
                {
                    isProcessingRequired = false;
                    Helper.LogInfo(@"No Data Available for User Data Import.", this.CompanyUserInfo );
                }
            }
            catch (Exception ex)
            {
                oUserDataImportInfo = null;
                isProcessingRequired = false;
                Helper.LogError(@"Error in IsProcessingRequiredForUserDataImport: " + ex.Message, this.CompanyUserInfo);
            }
            return isProcessingRequired;
        }

        public void ProcessUserDataImport()
        {
            try
            {
                if (this.oUserDataImportInfo.IsDataTransfered)
                {
                    ProcessImportedUserData();
                }
                else
                {
                    ExtractTransferAndProcessData();
                }
            }
            catch (Exception ex)
            {
                DataImportHelper.ResetUserDataHdrObject(this.oUserDataImportInfo, ex);
                Helper.LogError(ex, this.CompanyUserInfo);
            }
            finally
            {
                try
                {
                    DataImportHelper.UpdateDataImportHDR(this.oUserDataImportInfo, this.CompanyUserInfo);

                }
                catch (Exception ex)
                {
                    Helper.LogError("Error while updating DataImportHDR for UserDataImport - ", this.CompanyUserInfo);
                    Helper.LogError(ex, this.CompanyUserInfo);
                }
                try
                {
                    this.oUserDataImportInfo.SuccessEmailIDs = DataImportHelper.GetEmailIDWithSeprator(this.oUserDataImportInfo.NotifySuccessEmailIds) + DataImportHelper.GetEmailIDWithSeprator(this.oUserDataImportInfo.NotifySuccessUserEmailIds) + DataImportHelper.GetEmailIDWithSeprator(oUserDataImportInfo.WarningEmailIds);
                    this.oUserDataImportInfo.FailureEmailIDs = DataImportHelper.GetEmailIDWithSeprator(this.oUserDataImportInfo.NotifyFailureEmailIds) + DataImportHelper.GetEmailIDWithSeprator(this.oUserDataImportInfo.NotifyFailureUserEmailIds) + DataImportHelper.GetEmailIDWithSeprator(oUserDataImportInfo.WarningEmailIds);
                    DataImportHelper.SendMailToUsers(this.oUserDataImportInfo, this.CompanyUserInfo);
                }
                catch (Exception ex)
                {
                    Helper.LogError("Error while sending mail - ", this.CompanyUserInfo);
                    Helper.LogError(ex, this.CompanyUserInfo);
                }
            }
        }
        #endregion

        #region "Private Functions"
        private void ExtractTransferAndProcessData()
        {
            DataTable dtExcelData = null;
            Helper.LogError("3. Start Reading Excel file: " + this.oUserDataImportInfo.PhysicalPath, this.CompanyUserInfo);
            dtExcelData = DataImportHelper.GetSubledgerDataImportDataTableFromExcel(this.oUserDataImportInfo.PhysicalPath, ServiceConstants.USERDATA_SHEETNAME, this.CompanyUserInfo);
            Helper.LogError("4. Reading Excel file complete.", this.CompanyUserInfo);

            Helper.LogError("5. Validating Excel File Schema.", this.CompanyUserInfo);
            if (this.ValidateSchemaForUserData(dtExcelData))
            {
                //Add additional fields to ExcelDataTabel
                AddDataImportIDToDataTable(dtExcelData);
                Helper.LogError("6. Adding additional fields to excel data.", this.CompanyUserInfo);

                //Validate data in mandatory fields
                this.ValidateData(dtExcelData);
                Helper.LogError("7. Excel Data Validated.", this.CompanyUserInfo);

                this.CheckDuplicateLoginID(dtExcelData);
                this.CheckDuplicateLoginIDInDB(dtExcelData);

                //Transfer and Process data 
                DataImportHelper.TransferAndProcessUserData(dtExcelData, this.oUserDataImportInfo, this.CompanyUserInfo);
            }

        }
        /// <summary>
        /// Process imported data (userDataTransit table)
        /// </summary>
        private void ProcessImportedUserData()
        {
            DataImportHelper.ProcessTransferedUserData(this.oUserDataImportInfo, this.CompanyUserInfo);
        }

        /// <summary>
        /// validate schema of data retrived from excel file. Validation in done in terms of availablity of mandatory fields
        /// </summary>
        /// <param name="dtExcelData"></param>
        /// <returns></returns>
        private bool ValidateSchemaForUserData(DataTable dtExcelData)
        {
            bool isValidSchema;
            StringBuilder oSbError = new StringBuilder();

            //Get list of all mandatory fields
            List<string> userDataImporMandatoryFieldList = DataImportHelper.GetUserUploadImportMandatoryFields();

            //Check if all mandatory fields exists in DataTable from Excel
            foreach (string fieldName in userDataImporMandatoryFieldList)
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
                string errorMessage = Helper.GetSinglePhrase(5000165, 0, this.oUserDataImportInfo.LanguageID, this.oUserDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);//Mandatory columns not present: {0}

                this.oUserDataImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
                this.oUserDataImportInfo.DataImportStatusID = (short)Enums.DataImportStatus.Failure;
                this.oUserDataImportInfo.ErrorMessageToSave = String.Format(errorMessage, oSbError.ToString());
                throw new Exception(String.Format(errorMessage, oSbError.ToString()));
            }

            return isValidSchema;
        }

        private void ValidateData(DataTable dtExcelData)
        {
            //Row:{0} Column:{1} {2}
            //Row:5 Column FirstName - No Data in Mandatory field
            //Field: {0}; Row: {1} Data cannot be more than {2} characters 1827

            StringBuilder oSBErrors = new StringBuilder();
            MultilingualAttributeInfo oMultilingualAttributeInfo = Helper.GetMultilingualAttributeInfo(this.oUserDataImportInfo.LanguageID, this.oUserDataImportInfo.CompanyID);
            string multiLingualErrorPhrase = LanguageUtil.GetValue(5000190, oMultilingualAttributeInfo);
            string multiLingualErrorPhraseForDataLength = LanguageUtil.GetValue(1827, oMultilingualAttributeInfo);
            string mandatoryFieldPhrase = LanguageUtil.GetValue(5000201, oMultilingualAttributeInfo);
            foreach (DataRow dr in dtExcelData.Rows)
            {
                if (dr[UserUploadConstants.UploadFields.FIRSTNAME] != DBNull.Value)
                    dr[UserUploadConstants.UploadFields.FIRSTNAME] = dr[UserUploadConstants.UploadFields.FIRSTNAME].ToString().Trim();
                if (dr[UserUploadConstants.UploadFields.LASTTNAME] != DBNull.Value)
                    dr[UserUploadConstants.UploadFields.LASTTNAME] = dr[UserUploadConstants.UploadFields.LASTTNAME].ToString().Trim();
                if (dr[UserUploadConstants.UploadFields.LOGINID] != DBNull.Value)
                    dr[UserUploadConstants.UploadFields.LOGINID] = dr[UserUploadConstants.UploadFields.LOGINID].ToString().Trim();
                if (dr[UserUploadConstants.UploadFields.EMAILID] != DBNull.Value)
                    dr[UserUploadConstants.UploadFields.EMAILID] = dr[UserUploadConstants.UploadFields.EMAILID].ToString().Trim();
                if (dr[UserUploadConstants.UploadFields.DEFAULTROLE] != DBNull.Value)
                    dr[UserUploadConstants.UploadFields.DEFAULTROLE] = dr[UserUploadConstants.UploadFields.DEFAULTROLE].ToString().Trim();

                string FirstName = Convert.ToString(dr[UserUploadConstants.UploadFields.FIRSTNAME]);
                string LastName = Convert.ToString(dr[UserUploadConstants.UploadFields.LASTTNAME]);
                string LoginID = Convert.ToString(dr[UserUploadConstants.UploadFields.LOGINID]);
                string EmailID = Convert.ToString(dr[UserUploadConstants.UploadFields.EMAILID]);
                string DefaultRole = Convert.ToString(dr[UserUploadConstants.UploadFields.DEFAULTROLE]);
                string rowNumber = Convert.ToString(dr[AddedGLDataImportFields.EXCELROWNUMBER]);

                if (string.IsNullOrEmpty(FirstName))
                {
                    if (!string.IsNullOrEmpty(oSBErrors.ToString()))
                        oSBErrors.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    oSBErrors.Append(string.Format(multiLingualErrorPhrase, rowNumber, UserUploadConstants.UploadFields.FIRSTNAME, mandatoryFieldPhrase));
                }
                else if (FirstName.Length > (int)Enums.UserUploadFieldMaxLength.FirstName)
                {
                    if (!string.IsNullOrEmpty(oSBErrors.ToString()))
                        oSBErrors.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    oSBErrors.Append(string.Format(multiLingualErrorPhraseForDataLength, UserUploadConstants.UploadFields.FIRSTNAME, rowNumber, (int)Enums.UserUploadFieldMaxLength.FirstName));
                }

                if (string.IsNullOrEmpty(LastName))
                {
                    if (!string.IsNullOrEmpty(oSBErrors.ToString()))
                        oSBErrors.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    oSBErrors.Append(string.Format(multiLingualErrorPhrase, rowNumber, UserUploadConstants.UploadFields.LASTTNAME, mandatoryFieldPhrase));
                }
                else if (LastName.Length > (int)Enums.UserUploadFieldMaxLength.LastName)
                {
                    if (!string.IsNullOrEmpty(oSBErrors.ToString()))
                        oSBErrors.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    oSBErrors.Append(string.Format(multiLingualErrorPhraseForDataLength, UserUploadConstants.UploadFields.LASTTNAME, rowNumber, (int)Enums.UserUploadFieldMaxLength.LastName));
                }

                if (string.IsNullOrEmpty(LoginID))
                {
                    if (!string.IsNullOrEmpty(oSBErrors.ToString()))
                        oSBErrors.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    oSBErrors.Append(string.Format(multiLingualErrorPhrase, rowNumber, UserUploadConstants.UploadFields.LOGINID, mandatoryFieldPhrase));
                }
                else if (LoginID.Length > (int)Enums.UserUploadFieldMaxLength.LoginID)
                {
                    if (!string.IsNullOrEmpty(oSBErrors.ToString()))
                        oSBErrors.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    oSBErrors.Append(string.Format(multiLingualErrorPhraseForDataLength, UserUploadConstants.UploadFields.LOGINID, rowNumber, (int)Enums.UserUploadFieldMaxLength.LastName));
                }


                if (string.IsNullOrEmpty(EmailID))
                {
                    if (!string.IsNullOrEmpty(oSBErrors.ToString()))
                        oSBErrors.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    oSBErrors.Append(string.Format(multiLingualErrorPhrase, rowNumber, UserUploadConstants.UploadFields.EMAILID, mandatoryFieldPhrase));
                }
                else if (EmailID.Length > (int)Enums.UserUploadFieldMaxLength.EmailID)
                {
                    if (!string.IsNullOrEmpty(oSBErrors.ToString()))
                        oSBErrors.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    oSBErrors.Append(string.Format(multiLingualErrorPhraseForDataLength, UserUploadConstants.UploadFields.EMAILID, rowNumber, (int)Enums.UserUploadFieldMaxLength.EmailID));
                }

                if (string.IsNullOrEmpty(DefaultRole))
                {
                    if (!string.IsNullOrEmpty(oSBErrors.ToString()))
                        oSBErrors.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    oSBErrors.Append(string.Format(multiLingualErrorPhrase, rowNumber, UserUploadConstants.UploadFields.DEFAULTROLE, mandatoryFieldPhrase));
                }
                else if (DefaultRole.Length > (int)Enums.UserUploadFieldMaxLength.DefaultRole)
                {
                    if (!string.IsNullOrEmpty(oSBErrors.ToString()))
                        oSBErrors.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    oSBErrors.Append(string.Format(multiLingualErrorPhraseForDataLength, UserUploadConstants.UploadFields.DEFAULTROLE, rowNumber, (int)Enums.UserUploadFieldMaxLength.DefaultRole));
                }

            }
            if (!oSBErrors.ToString().Equals(String.Empty))
            {
                this.oUserDataImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
                this.oUserDataImportInfo.DataImportStatusID = (short)Enums.DataImportStatus.Failure;
                this.oUserDataImportInfo.RecordsImported = 0;
                this.oUserDataImportInfo.ErrorMessageToSave = oSBErrors.ToString();
                throw new Exception(oSBErrors.ToString());
            }
        }

        private void FieldPresent(DataTable dtExcelData)
        {

        }

        /// <summary>
        /// Add additional fields to excel data table
        /// </summary>
        /// <param name="dtExcelData"></param>
        private void AddDataImportIDToDataTable(DataTable dtExcelData)
        {
            dtExcelData.Columns.Add(UserUploadConstants.AddedFields.DATAIMPORTID, typeof(System.Int32));
            dtExcelData.Columns.Add(UserUploadConstants.AddedFields.EXCELROWNUMBER, typeof(System.Int32));
            dtExcelData.Columns.Add(UserUploadConstants.AddedFields.PASSWORD, typeof(System.String));
            dtExcelData.Columns.Add(UserUploadConstants.AddedFields.UNHASHED_PASSWORD, typeof(System.String));


            Random oRand = new Random();
            for (int x = 0; x < dtExcelData.Rows.Count; x++)
            {
                string loginID = dtExcelData.Rows[x][UserUploadConstants.UploadFields.LOGINID].ToString();
                dtExcelData.Rows[x][UserUploadConstants.AddedFields.DATAIMPORTID] = this.oUserDataImportInfo.DataImportID;
                dtExcelData.Rows[x][UserUploadConstants.AddedFields.EXCELROWNUMBER] = x + 2;
                string generatedPassword = Helper.CreateRandomPassword(SharedConstants.LENGTH_GENERATED_PASSWORD, loginID, oRand);
                dtExcelData.Rows[x][UserUploadConstants.AddedFields.UNHASHED_PASSWORD] = generatedPassword;
                dtExcelData.Rows[x][UserUploadConstants.AddedFields.PASSWORD] = Helper.GetHashedPassword(generatedPassword);
            }
        }

        private void CheckDuplicateLoginID(DataTable dtExcelData)
        {

            //5000340 : Duplicate Value

            StringBuilder oSBErrors = new StringBuilder();
            MultilingualAttributeInfo oMultilingualAttributeInfo = Helper.GetMultilingualAttributeInfo(this.oUserDataImportInfo.LanguageID, this.oUserDataImportInfo.CompanyID);
            Hashtable ht = new Hashtable();

            //5000190 : Error in Row# {0}, Column '{1}'. {2} 
            string errorPhrase = LanguageUtil.GetValue(5000190, oMultilingualAttributeInfo);
            string dupliateRowPhrase = LanguageUtil.GetValue(5000340, oMultilingualAttributeInfo);
            //ArrayList duplicateRows = new ArrayList();
            foreach (DataRow dr in dtExcelData.Rows)
            {
                string loginid = dr[UserUploadConstants.UploadFields.LOGINID].ToString().ToUpper();
                string excelRowNumber = dr[UserUploadConstants.AddedFields.EXCELROWNUMBER].ToString();
                if (ht.Contains(loginid))
                {
                    //duplicateRows.Add(dr);
                    if (!String.IsNullOrEmpty(oSBErrors.ToString()))
                        oSBErrors.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    oSBErrors.Append(string.Format(errorPhrase, excelRowNumber, UserUploadConstants.UploadFields.LOGINID, dupliateRowPhrase));
                    //oSBErrors.Append("Row: " + dr[UserUploadConstants.AddedFields.EXCELROWNUMBER].ToString() + " is duplicate of " + ht[loginid].ToString());
                }
                else
                    ht.Add(loginid, excelRowNumber);
            }
            if (!oSBErrors.ToString().Equals(String.Empty))
            {
                this.oUserDataImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
                this.oUserDataImportInfo.DataImportStatusID = (short)Enums.DataImportStatus.Failure;
                this.oUserDataImportInfo.RecordsImported = 0;
                this.oUserDataImportInfo.ErrorMessageToSave = oSBErrors.ToString();
                throw new Exception(oSBErrors.ToString());
            }
        }

        private void CheckDuplicateLoginIDInDB(DataTable dtExcelData)
        {

            //5000340 : Duplicate Value

            StringBuilder oSBErrors = new StringBuilder();
            MultilingualAttributeInfo oMultilingualAttributeInfo = Helper.GetMultilingualAttributeInfo(this.oUserDataImportInfo.LanguageID, this.oUserDataImportInfo.CompanyID);
            bool IsEmailIDUniqueCheckRequired = false;
            bool.TryParse(SharedAppSettingHelper.GetAppSettingValue(SharedAppSettingConstants.IS_EMAIL_ID_UNIQUE_CHECK_REQUIRED), out IsEmailIDUniqueCheckRequired);

            //5000193 : Error in Row# {0}. {1}  
            string errorPhrase = LanguageUtil.GetValue(5000193, oMultilingualAttributeInfo);
            string dupliateLoginIDPhrase = LanguageUtil.GetValue(5000016, oMultilingualAttributeInfo);
            string dupliateEmailIDPhrase = LanguageUtil.GetValue(5000249, oMultilingualAttributeInfo);
            List<UserHdrInfo> oUserHdrInfoList = new List<UserHdrInfo>();
            //ArrayList duplicateRows = new ArrayList();
            foreach (DataRow dr in dtExcelData.Rows)
            {
                string loginid = dr[UserUploadConstants.UploadFields.LOGINID].ToString().ToUpper();
                string emailid = dr[UserUploadConstants.UploadFields.EMAILID].ToString().ToUpper();
                string excelRowNumber = dr[UserUploadConstants.AddedFields.EXCELROWNUMBER].ToString();
                UserHdrInfo oUserHdrInfo = new UserHdrInfo();
                oUserHdrInfo.LoginID = loginid;
                oUserHdrInfo.EmailID = emailid;
                oUserHdrInfo.ExcelRowNumber = Int32.Parse(excelRowNumber);
                oUserHdrInfoList.Add(oUserHdrInfo);
            }
            IUser oUser = RemotingHelper.GetUserObject();
            AppUserInfo oAppUserInfo = new AppUserInfo();
            oAppUserInfo.CompanyID = oUserDataImportInfo.CompanyID;
            oUserHdrInfoList = oUser.CheckUsersForUniqueness(oUserHdrInfoList, true, oAppUserInfo);
            foreach (UserHdrInfo item in oUserHdrInfoList)
            {
                if(!item.IsLoginIDUnique.GetValueOrDefault())
                    oSBErrors.AppendLine(string.Format(errorPhrase, item.ExcelRowNumber, dupliateLoginIDPhrase));
                if(IsEmailIDUniqueCheckRequired && !item.IsEmailIDUnique.GetValueOrDefault())
                    oSBErrors.AppendLine(string.Format(errorPhrase, item.ExcelRowNumber, dupliateEmailIDPhrase));
            }
            if (!oSBErrors.ToString().Equals(String.Empty))
            {
                this.oUserDataImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
                this.oUserDataImportInfo.DataImportStatusID = (short)Enums.DataImportStatus.Failure;
                this.oUserDataImportInfo.RecordsImported = 0;
                this.oUserDataImportInfo.ErrorMessageToSave = oSBErrors.ToString();
                throw new Exception(oSBErrors.ToString());
            }
        }
        
        #endregion


        #region "Old Code"
        // public UserUpload()
        // {
        // }

        // UserHdrInfo _objUserHdrInfo = new UserHdrInfo();
        // //public List<UserHdrInfo> GetDataToExport()
        // //{
        // //    UserUploadDAO objExportDAO = new UserUploadDAO();
        // //    return objExportDAO.GetUserDataForProcessing();
        // //}

        // public bool IsProcessingRequiredForUserUploadDataImport()
        // {
        //     bool processingRequired = false;
        //     try
        //     {
        //         DataImportHdrDAO oDataImprtHdrDAO = new DataImportHdrDAO();
        //         oDataImprtHdrDAO.GetDataImportForProcessing(_objUserHdrInfo, Enums.DataImportType.UserUpload, DateTime.Now);
        //         if (_objUserHdrInfo.DataImportID > 0)
        //         {
        //             processingRequired = true;
        //             Helper.LogError(@"User Upload required for DataImportID: " + _objUserHdrInfo.DataImportID.ToString());
        //         }
        //         else
        //         {
        //             Helper.LogError(@"No Data Available for User Upload.");
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         _objUserHdrInfo = null;
        //         processingRequired = false;
        //         Helper.LogError(@"Error in IsProcessingRequiredForUserUploadDataImport: " + ex.Message);
        //     }
        //     return processingRequired;
        // }

        // public void ProcessUserUpload()
        // {

        //     DataTable dtExcelData = null;
        //     Helper.LogError("3. Start Reading Excel file: " + _objUserHdrInfo.PhysicalPath);
        //     dtExcelData = DataImportHelper.GetUserUploadDataTableFromExcel(_objUserHdrInfo.PhysicalPath, UserUploadConstants.SheetName);


        //     UserUploadDAO oUserUploadDAO = new UserUploadDAO();
        //     List<UserRoleInfo> lstExistingUserRoleInfo = oUserUploadDAO.GetRolesByLoginIDs(dtExcelData);

        //     dtExcelData.Columns.Add("IsValid");
        //     dtExcelData.Columns.Add("ValidationMessage");
        //     dtExcelData.Columns.Add("SelectedRoleIDs");

        //     if (ValidateSchemaForUserUploadData(dtExcelData))
        //     {
        //         oUserUploadDAO.InsertUserTransit(_objUserHdrInfo, dtExcelData);
        //         ValidateData(dtExcelData);

        //         int invalidRows = dtExcelData.Select("IsValid = false").Length;
        //         if (invalidRows > 0)
        //         {
        //             _objUserHdrInfo.DataImportStatusID = (short)Enums.DataImportStatus.Warning;
        //             _objUserHdrInfo.DataImportStatus = DataImportStatus.DATAIMPORTWARNING;
        //             _objUserHdrInfo.ErrorMessageToSave = GetErrorMessageForUserUpload(dtExcelData);
        //             DataImportHelper.UpdateDataImportHDRForUserUpload(_objUserHdrInfo);
        //             return;
        //         }

        //         if (_objUserHdrInfo.IsForceCommit)
        //         {
        //             UserUploadDAO objUserUploadDAO = new UserUploadDAO();
        //             DataRow[] validRows = dtExcelData.Select("IsValid <> false");
        //             DataTable dtValidRows = new DataTable();
        //             dtValidRows = dtExcelData.Clone();
        //             foreach (DataRow dr in validRows)
        //             {
        //                 dtExcelData.ImportRow(dr);
        //             }
        //             objUserUploadDAO.UpdateUserHDR(_objUserHdrInfo,  dtValidRows);
        //         }
        //         else
        //         {
        //             UserUploadDAO objUserUploadDAO = new UserUploadDAO();
        //             objUserUploadDAO.UpdateUserHDR(_objUserHdrInfo, dtExcelData);
        //         }
        //     }
        //     else
        //     {
        //         _objUserHdrInfo.DataImportStatusID = (short)Enums.DataImportStatus.Failure;
        //         _objUserHdrInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
        //         _objUserHdrInfo.ErrorMessageToSave = "Invalid Table Schema";
        //         DataImportHelper.UpdateDataImportHDRForUserUpload(_objUserHdrInfo);
        //     }
        // }

        // private string GetErrorMessageForUserUpload(DataTable dtExcelData)
        // {
        //     string errorMessage = string.Empty;
        //     int rowCount = 0;
        //     foreach (DataRow dr in dtExcelData.Rows)
        //     {
        //         rowCount += 1;
        //         if (!string.IsNullOrEmpty(Convert.ToString(dr["ValidationMessage"])))
        //         {
        //             errorMessage = "Error in Row: " + rowCount.ToString() + ": ";
        //             errorMessage = errorMessage + Convert.ToString(dr["ValidationMessage"]);
        //         }
        //     }

        //     return errorMessage;
        // }

        //private void ValidateData(DataTable dtUserUpload)
        // {
        //     foreach (DataRow dr in dtUserUpload.Rows)
        //     {
        //         string FirstName = Convert.ToString(dr["First Name"]);
        //         string LastName = Convert.ToString(dr["Last Name"]);
        //         string LoginID = Convert.ToString(dr["Login ID"]);
        //         string EmailID = Convert.ToString(dr["Email ID"]);
        //         string DefaultRole = Convert.ToString(dr["Default Role"]);

        //         StringBuilder oSBError = new StringBuilder();

        //         if (string.IsNullOrEmpty(FirstName))
        //         {
        //             oSBError.Append("First Name is mandatory");
        //         }
        //         if (string.IsNullOrEmpty(LastName))
        //         {
        //             oSBError.Append("Last Name is mandatory");
        //         }
        //         if (string.IsNullOrEmpty(LoginID))
        //         {
        //             oSBError.Append("LoginID is mandatory");
        //         }
        //         if (string.IsNullOrEmpty(EmailID))
        //         {
        //             oSBError.Append("EmailID is mandatory");
        //         }
        //         if (string.IsNullOrEmpty(DefaultRole))
        //         {
        //             oSBError.Append("Default Role is mandatory");
        //         }

        //         if (!string.IsNullOrEmpty(oSBError.ToString()))
        //         {
        //             dr["IsValid"] = "false";
        //             dr["ValidationMessage"] = oSBError.ToString();
        //         }
        //         else
        //         {
        //             dr["IsValid"] = "true";
        //             dr["ValidationMessage"] = string.Empty;
        //         }

        //     }
        // }

        // private bool ValidateSchemaForUserUploadData(DataTable dtExcelData)
        // {
        //     bool isValidSchema;
        //     bool columnFound;
        //     StringBuilder oSbError = new StringBuilder();

        //     //Get list of all mandatory fields
        //     List<string> UserUploadMandatoryFieldList = DataImportHelper.GetUserUploadImportMandatoryFields();

        //     //Check if all mandatory fields exists in DataTable from Excel
        //     foreach (string fieldName in UserUploadMandatoryFieldList)
        //     {
        //         columnFound = false;
        //         for (int i = 0; i < dtExcelData.Columns.Count; i++)
        //         {
        //             if (fieldName == dtExcelData.Columns[i].ColumnName)
        //             {
        //                 columnFound = true;
        //                 break;
        //             }
        //         }
        //         if (!columnFound)
        //         {
        //             if (!oSbError.ToString().Equals(string.Empty))
        //                 oSbError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
        //             oSbError.Append(fieldName);
        //         }
        //     }
        //     isValidSchema = string.IsNullOrEmpty(oSbError.ToString());

        //     //If schema is not valid, generate a multi lingual error message, set failure status, faliure status ID, error message 
        //     //in MultilingualDataImport object and throw an exception with generated message 
        //     if (!isValidSchema)
        //     {
        //         //string errorMessage = Helper.GetSinglePhrase(5000165, 0, _MultilingualDataImportHdrInfo.LanguageID, _MultilingualDataImportHdrInfo.DefaultLanguageID);//Mandatory columns not present: {0}

        //         //_MultilingualDataImportHdrInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
        //         //_MultilingualDataImportHdrInfo.DataImportStatusID = (short)Enums.DataImportStatus.Failure;
        //         //_MultilingualDataImportHdrInfo.ErrorMessageToSave = String.Format(errorMessage, oSbError.ToString());
        //         //throw new Exception(String.Format(errorMessage, oSbError.ToString()));
        //     }
        //     return isValidSchema;
        // }
        #endregion
    }
}
