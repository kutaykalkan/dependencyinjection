using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Service.Model;
using System.Data;
using System.Data.SqlClient;
using SkyStem.ART.Service.Utility;
using SkyStem.ART.Service.APP.BLL;
using SkyStem.ART.Service.Data;
using SkyStem.ART.Shared.Data;
using SkyStem.ART.Client.Model.CompanyDatabase;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.Service.APP.DAO
{
    public class UserUploadDAO : DataImportHdrDAO
    {
        #region "User Hdr Column Names"
        private const string UserHdr_Column_UserID = "UserID";
        private const string UserHdr_Column_FirstName = "FirstName";
        private const string UserHdr_Column_LastName = "LastName";
        private const string UserHdr_Column_LoginID = "LoginID";
        private const string UserHdr_Column_EmailID = "EmailID";
        private const string UserHdr_Column_IsCreated = "IsCreated";
        private const string UserHdr_Column_IsUpdated = "IsUpdated";
        private const string UserHdr_Column_CompanyID = "CompanyID";
        private const string UserHdr_Column_LanguageID = "LanguageID";
        private const string UserHdr_Column_Password = "Password";
        private const string UserHdr_Column_CompanyDisplayName = "CompanyDisplayName";
        private const string UserHdr_Column_IsActive = "IsActive";

        #endregion

        public UserUploadDAO(CompanyUserInfo oCompanyUserInfo)
            : base(oCompanyUserInfo)
        {
        }

        public UserDataImportInfo GetUserDataImportForProcessing(DateTime dateRevised)
        {
            UserDataImportInfo oUserDataImportInfo = new UserDataImportInfo();
            if (this.IsDataImportProcessingRequired(Enums.DataImportType.UserUpload))
            {
                this.GetDataImportForProcessing(oUserDataImportInfo, Enums.DataImportType.UserUpload, dateRevised);
            }
            return oUserDataImportInfo;
        }

        public void CopyGLDataFromExcelToSqlServer(DataTable dtExcelData, UserDataImportInfo oUserDataImportInfo)
        {
            DateTime dateAdded = DateTime.Now.Date;
            using (SqlBulkCopy oSqlBlkCopy = new SqlBulkCopy(GetConnectionString(), SqlBulkCopyOptions.Default))
            {
                Helper.LogError("5. Mapping fields from source to destination.", this.CompanyUserInfo);

                oSqlBlkCopy.DestinationTableName = "UserUploadTransit";

                //Mapping mandatory fields
                if (dtExcelData.Columns.Contains(UserUploadConstants.UploadFields.FIRSTNAME))
                    oSqlBlkCopy.ColumnMappings.Add(UserUploadConstants.UploadFields.FIRSTNAME, UserUploadConstants.UserUploadTransitFields.FIRSTNAME);

                if (dtExcelData.Columns.Contains(UserUploadConstants.UploadFields.LASTTNAME))
                    oSqlBlkCopy.ColumnMappings.Add(UserUploadConstants.UploadFields.LASTTNAME, UserUploadConstants.UserUploadTransitFields.LASTNAME);

                if (dtExcelData.Columns.Contains(UserUploadConstants.UploadFields.EMAILID))
                    oSqlBlkCopy.ColumnMappings.Add(UserUploadConstants.UploadFields.EMAILID, UserUploadConstants.UserUploadTransitFields.EMAILID);

                if (dtExcelData.Columns.Contains(UserUploadConstants.UploadFields.LOGINID))
                    oSqlBlkCopy.ColumnMappings.Add(UserUploadConstants.UploadFields.LOGINID, UserUploadConstants.UserUploadTransitFields.LOGINID);

                if (dtExcelData.Columns.Contains(UserUploadConstants.UploadFields.DEFAULTROLE))
                    oSqlBlkCopy.ColumnMappings.Add(UserUploadConstants.UploadFields.DEFAULTROLE, UserUploadConstants.UserUploadTransitFields.DEFAULTROLE);

                //Mapping additional columns
                if (dtExcelData.Columns.Contains(UserUploadConstants.AddedFields.DATAIMPORTID))
                    oSqlBlkCopy.ColumnMappings.Add(UserUploadConstants.AddedFields.DATAIMPORTID, UserUploadConstants.UserUploadTransitFields.DATAIMPORTID);
                if (dtExcelData.Columns.Contains(UserUploadConstants.AddedFields.EXCELROWNUMBER))
                    oSqlBlkCopy.ColumnMappings.Add(UserUploadConstants.AddedFields.EXCELROWNUMBER, UserUploadConstants.UserUploadTransitFields.EXCELROWNUMBER);
                if (dtExcelData.Columns.Contains(UserUploadConstants.AddedFields.PASSWORD))
                    oSqlBlkCopy.ColumnMappings.Add(UserUploadConstants.AddedFields.PASSWORD, UserUploadConstants.UserUploadTransitFields.PASSWORD);
                if (dtExcelData.Columns.Contains(UserUploadConstants.AddedFields.UNHASHED_PASSWORD))
                    oSqlBlkCopy.ColumnMappings.Add(UserUploadConstants.AddedFields.UNHASHED_PASSWORD, UserUploadConstants.UserUploadTransitFields.UNHASHED_PASSWORD);


                //Mapping optioal Columns
                if (dtExcelData.Columns.Contains(UserUploadConstants.UploadFields.SYSADMIN))
                    oSqlBlkCopy.ColumnMappings.Add(UserUploadConstants.UploadFields.SYSADMIN, UserUploadConstants.UserUploadTransitFields.SYSADMIN);
                if (dtExcelData.Columns.Contains(UserUploadConstants.UploadFields.PREPARER))
                    oSqlBlkCopy.ColumnMappings.Add(UserUploadConstants.UploadFields.PREPARER, UserUploadConstants.UserUploadTransitFields.PREPARER);
                if (dtExcelData.Columns.Contains(UserUploadConstants.UploadFields.REVIEWER))
                    oSqlBlkCopy.ColumnMappings.Add(UserUploadConstants.UploadFields.REVIEWER, UserUploadConstants.UserUploadTransitFields.REVIEWER);
                if (dtExcelData.Columns.Contains(UserUploadConstants.UploadFields.APPROVER))
                    oSqlBlkCopy.ColumnMappings.Add(UserUploadConstants.UploadFields.APPROVER, UserUploadConstants.UserUploadTransitFields.APPROVER);

                if (dtExcelData.Columns.Contains(UserUploadConstants.UploadFields.BUSINESSADMIN))
                    oSqlBlkCopy.ColumnMappings.Add(UserUploadConstants.UploadFields.BUSINESSADMIN, UserUploadConstants.UserUploadTransitFields.BUSINESSADMIN);

                if (dtExcelData.Columns.Contains(UserUploadConstants.UploadFields.FINANCIALMANAGER))
                    oSqlBlkCopy.ColumnMappings.Add(UserUploadConstants.UploadFields.FINANCIALMANAGER, UserUploadConstants.UserUploadTransitFields.FINANCIALMANAGER);

                if (dtExcelData.Columns.Contains(UserUploadConstants.UploadFields.ACCOUNTMANAGER))
                    oSqlBlkCopy.ColumnMappings.Add(UserUploadConstants.UploadFields.ACCOUNTMANAGER, UserUploadConstants.UserUploadTransitFields.ACCOUNTMANAGER);

                if (dtExcelData.Columns.Contains(UserUploadConstants.UploadFields.CONTROLLER))
                    oSqlBlkCopy.ColumnMappings.Add(UserUploadConstants.UploadFields.CONTROLLER, UserUploadConstants.UserUploadTransitFields.CONTROLLER);

                if (dtExcelData.Columns.Contains(UserUploadConstants.UploadFields.EXECUTIVE))
                    oSqlBlkCopy.ColumnMappings.Add(UserUploadConstants.UploadFields.EXECUTIVE, UserUploadConstants.UserUploadTransitFields.EXECUTIVE);

                if (dtExcelData.Columns.Contains(UserUploadConstants.UploadFields.CEOCFO))
                    oSqlBlkCopy.ColumnMappings.Add(UserUploadConstants.UploadFields.CEOCFO, UserUploadConstants.UserUploadTransitFields.CEOCFO);

                if (dtExcelData.Columns.Contains(UserUploadConstants.UploadFields.BACKUPPREPARER))
                    oSqlBlkCopy.ColumnMappings.Add(UserUploadConstants.UploadFields.BACKUPPREPARER, UserUploadConstants.UserUploadTransitFields.BACKUPPREPARER);

                if (dtExcelData.Columns.Contains(UserUploadConstants.UploadFields.BACKUPREVIEWER))
                    oSqlBlkCopy.ColumnMappings.Add(UserUploadConstants.UploadFields.BACKUPREVIEWER, UserUploadConstants.UserUploadTransitFields.BACKUPREVIEWER);

                if (dtExcelData.Columns.Contains(UserUploadConstants.UploadFields.BACKUPAPPROVER))
                    oSqlBlkCopy.ColumnMappings.Add(UserUploadConstants.UploadFields.BACKUPAPPROVER, UserUploadConstants.UserUploadTransitFields.BACKUPAPPROVER);

                if (dtExcelData.Columns.Contains(UserUploadConstants.UploadFields.AUDIT))
                    oSqlBlkCopy.ColumnMappings.Add(UserUploadConstants.UploadFields.AUDIT, UserUploadConstants.UserUploadTransitFields.AUDIT);

                if (dtExcelData.Columns.Contains(UserUploadConstants.UploadFields.TASKOWNER))
                    oSqlBlkCopy.ColumnMappings.Add(UserUploadConstants.UploadFields.TASKOWNER, UserUploadConstants.UserUploadTransitFields.TASKOWNER);

                if (dtExcelData.Columns.Contains(UserUploadConstants.UploadFields.USERADMIN))
                    oSqlBlkCopy.ColumnMappings.Add(UserUploadConstants.UploadFields.USERADMIN, UserUploadConstants.UserUploadTransitFields.USERADMIN);

                oSqlBlkCopy.WriteToServer(dtExcelData);

                //Mark Data Transfer Flag
                DataImportHdrDAO oDataImportHdrDAO = new DataImportHdrDAO(this.CompanyUserInfo);
                oUserDataImportInfo.IsDataTransfered = true;
                oDataImportHdrDAO.UpdateDataTransferStatus(oUserDataImportInfo);
            }
        }

        public void ProcessTransferedUserData(UserDataImportInfo oUserDataImportInfo)
        {
            string xmlReturnString;
            SqlDataReader reader = null;
            List<UserHdrInfo> userCreatedList = null;
            List<UserHdrInfo> userUpdatedList = null;
            List<CompanyUserInfo> oCompanyUserInfoList = null;
            using (SqlConnection cnn = GetConnection())
            {
                SqlCommand oCommand = GetUserUploadProcessingCommand(oUserDataImportInfo);
                cnn.Open();
                oCommand.Connection = cnn;
                //oCommand.ExecuteNonQuery();
                try
                {
                    reader = oCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        userCreatedList = new List<UserHdrInfo>();
                        userUpdatedList = new List<UserHdrInfo>();
                        oCompanyUserInfoList = new List<CompanyUserInfo>();
                        UserHdrInfo oUser;
                        while (reader.Read())
                        {
                            oUser = this.MapUserHdrObject(reader);
                            if (oUser.IsCreated)
                            {
                                userCreatedList.Add(oUser);
                                oCompanyUserInfoList.Add(MapCompanyUserInfo(oUser));
                            }
                            if (oUser.IsUpdated)
                                userUpdatedList.Add(oUser);
                        }
                        oUserDataImportInfo.CreatedUserList = userCreatedList;
                        oUserDataImportInfo.UpdatedUserList = userUpdatedList;
                        oUserDataImportInfo.CompanyUserInfoList = oCompanyUserInfoList;
                    }
                }
                finally
                {
                    if (reader != null && !reader.IsClosed)
                        reader.Close();
                }

                //Get Return Value from Sql Server
                xmlReturnString = oCommand.Parameters["@ReturnValue"].Value.ToString();
            }
            //Deserialize returned info
            ReturnValue oRetVal = ReturnValue.DeSerialize(xmlReturnString);
            if (oRetVal != null)
            {
                oUserDataImportInfo.ErrorMessageFromSqlServer = oRetVal.ErrorMessageForServiceToLog;
                oUserDataImportInfo.ErrorMessageToSave = oRetVal.ErrorMessageToSave;
                oUserDataImportInfo.DataImportStatus = oRetVal.ImportStatus;
                if (oRetVal.WarningReasonID.HasValue)
                {
                    oUserDataImportInfo.WarningReasonID = oRetVal.WarningReasonID.Value;
                }

                if (oRetVal.RecordsImported.HasValue)
                {
                    oUserDataImportInfo.RecordsImported = oRetVal.RecordsImported.Value;
                }
                else
                {
                    oUserDataImportInfo.RecordsImported = 0;
                }
            }
            Helper.LogError("8. Data Processing Complete.", this.CompanyUserInfo);
            Helper.LogError(" - Status: " + oUserDataImportInfo.DataImportStatus, this.CompanyUserInfo);
            Helper.LogError(" - Message: " + oUserDataImportInfo.ErrorMessageFromSqlServer, this.CompanyUserInfo);
            Helper.LogError(" - Records Imported: " + oUserDataImportInfo.RecordsImported.ToString(), this.CompanyUserInfo);

            // Data Should be deleted in case there is no warning
            if (oUserDataImportInfo.DataImportStatus != DataImportStatus.DATAIMPORTWARNING)
                oUserDataImportInfo.IsDataDeletionRequired = true;

            //Raise exception if dataImportStatus = "FAIL". This exception message will be logged into logfile
            if (oUserDataImportInfo.DataImportStatus == DataImportStatus.DATAIMPORTFAIL)
                throw new Exception(oUserDataImportInfo.ErrorMessageToSave);
        }


        private SqlCommand GetUserUploadProcessingCommand(UserDataImportInfo oUserDataImportInfo)
        {
            SqlCommand oCommand = this.CreateCommand();
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.CommandText = "usp_SCV_INS_ProcessUserUploadTransit";

            SqlParameterCollection cmdParamCollectionImport = oCommand.Parameters;
            SqlParameter paramCompanyID = new SqlParameter("@companyID", oUserDataImportInfo.CompanyID);
            SqlParameter paramDataImportID = new SqlParameter("@dataImportID", oUserDataImportInfo.DataImportID);
            SqlParameter paramAddedBy = new SqlParameter("@addedBy", oUserDataImportInfo.AddedBy);
            SqlParameter paramDateAdded = new SqlParameter("@dateAdded", oUserDataImportInfo.DateAdded);
            SqlParameter paramIsForceCommit = new SqlParameter("@isForceCommit", oUserDataImportInfo.IsForceCommit);

            SqlParameter paramWarningReasonID = new SqlParameter();
            paramWarningReasonID.ParameterName = "@warningReasonID";
            if (oUserDataImportInfo.WarningReasonID.HasValue)
                paramWarningReasonID.Value = oUserDataImportInfo.WarningReasonID;
            else
                paramWarningReasonID.Value = DBNull.Value;

            SqlParameter paramReturnValue = new SqlParameter("@ReturnValue", SqlDbType.NVarChar, -1);
            paramReturnValue.Direction = ParameterDirection.Output;
            //SqlParameter paramErrorMessageSRV = new SqlParameter("@errorMessageForServiceToLog", SqlDbType.VarChar, -1);
            //paramErrorMessageSRV.Direction = ParameterDirection.Output;

            //SqlParameter paramErrorMessageToSave = new SqlParameter("@errorMessageToSave", SqlDbType.NVarChar, -1);
            //paramErrorMessageToSave.Direction = ParameterDirection.Output;

            //SqlParameter paramImportStatus = new SqlParameter("@importStatus", SqlDbType.VarChar, 15);
            //paramImportStatus.Direction = ParameterDirection.Output;

            //SqlParameter paramRecordsImported = new SqlParameter("@recordsImported", SqlDbType.Int);
            //paramRecordsImported.Direction = ParameterDirection.Output;


            cmdParamCollectionImport.Add(paramCompanyID);
            cmdParamCollectionImport.Add(paramDataImportID);
            cmdParamCollectionImport.Add(paramAddedBy);
            cmdParamCollectionImport.Add(paramDateAdded);
            cmdParamCollectionImport.Add(paramIsForceCommit);

            cmdParamCollectionImport.Add(paramWarningReasonID);
            cmdParamCollectionImport.Add(paramReturnValue);

            return oCommand;

        }

        private UserHdrInfo MapUserHdrObject(SqlDataReader reader)
        {
            UserHdrInfo oUser = new UserHdrInfo();
            int ordinal;

            //UserID
            try
            {
                ordinal = reader.GetOrdinal(UserHdr_Column_UserID);
                if (!reader.IsDBNull(ordinal))
                    oUser.UserID = reader.GetInt32(ordinal);
            }
            catch (IndexOutOfRangeException indexEx)
            {
            }
            catch (Exception ex)
            {
            }

            //FirstName
            try
            {
                ordinal = reader.GetOrdinal(UserHdr_Column_FirstName);
                if (!reader.IsDBNull(ordinal))
                    oUser.FirstName = reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException indexEx)
            {
            }
            catch (Exception ex)
            {
            }

            //LastName
            try
            {
                ordinal = reader.GetOrdinal(UserHdr_Column_LastName);
                if (!reader.IsDBNull(ordinal))
                    oUser.LastName = reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException indexEx)
            {
            }
            catch (Exception ex)
            {
            }

            //LoginID
            try
            {
                ordinal = reader.GetOrdinal(UserHdr_Column_LoginID);
                if (!reader.IsDBNull(ordinal))
                    oUser.LoginID = reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException indexEx)
            {
            }
            catch (Exception ex)
            {
            }

            //EmailID
            try
            {
                ordinal = reader.GetOrdinal(UserHdr_Column_EmailID);
                if (!reader.IsDBNull(ordinal))
                    oUser.EmailID = reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException indexEx)
            {
            }
            catch (Exception ex)
            {
            }

            //IsCreated
            try
            {
                ordinal = reader.GetOrdinal(UserHdr_Column_IsCreated);
                if (!reader.IsDBNull(ordinal))
                    oUser.IsCreated = reader.GetBoolean(ordinal);
            }
            catch (IndexOutOfRangeException indexEx)
            {
            }
            catch (Exception ex)
            {
            }
            //IsUpdated
            try
            {
                ordinal = reader.GetOrdinal(UserHdr_Column_IsUpdated);
                if (!reader.IsDBNull(ordinal))
                    oUser.IsUpdated = reader.GetBoolean(ordinal);
            }
            catch (IndexOutOfRangeException indexEx)
            {
            }
            catch (Exception ex)
            {
            }

            //CompanyID
            try
            {
                ordinal = reader.GetOrdinal(UserHdr_Column_CompanyID);
                if (!reader.IsDBNull(ordinal))
                    oUser.CompanyID = reader.GetInt32(ordinal);
            }
            catch (IndexOutOfRangeException indexEx)
            {
            }
            catch (Exception ex)
            {
            }
            //LanguageID
            try
            {
                ordinal = reader.GetOrdinal(UserHdr_Column_LanguageID);
                if (!reader.IsDBNull(ordinal))
                    oUser.DefaultLanguageID = reader.GetInt32(ordinal);
            }
            catch (IndexOutOfRangeException indexEx)
            {
            }
            catch (Exception ex)
            {
            }
            //Password
            try
            {
                ordinal = reader.GetOrdinal(UserHdr_Column_Password);
                if (!reader.IsDBNull(ordinal))
                    oUser.Password = reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException indexEx)
            {
            }
            catch (Exception ex)
            {
            }
            //UserHdr_Column_CompanyDisplayName
            try
            {
                ordinal = reader.GetOrdinal(UserHdr_Column_CompanyDisplayName);
                if (!reader.IsDBNull(ordinal))
                    oUser.CompanyDisplayName = reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException indexEx)
            {
            }
            catch (Exception ex)
            {
            }
            try
            {
                ordinal = reader.GetOrdinal(UserHdr_Column_IsActive);
                if (!reader.IsDBNull(ordinal))
                    oUser.IsActive = reader.GetBoolean(ordinal);
            }
            catch (IndexOutOfRangeException indexEx)
            {
            }
            catch (Exception ex)
            {
            }
            return oUser;
        }

        private CompanyUserInfo MapCompanyUserInfo(UserHdrInfo oUserHdrInfo)
        {
            CompanyUserInfo oCompanyUserInfo = new CompanyUserInfo();
            oCompanyUserInfo.CompanyID = oUserHdrInfo.CompanyID;
            oCompanyUserInfo.UserID = oUserHdrInfo.UserID;
            oCompanyUserInfo.LoginID = oUserHdrInfo.LoginID;
            oCompanyUserInfo.EmailID = oUserHdrInfo.EmailID;
            oCompanyUserInfo.IsActive = oUserHdrInfo.IsActive;
            return oCompanyUserInfo;
        }
    }
}
