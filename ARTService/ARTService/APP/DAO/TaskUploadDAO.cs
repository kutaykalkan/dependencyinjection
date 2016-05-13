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
    public class TaskUploadDAO : DataImportHdrDAO
    {
        public TaskUploadDAO(CompanyUserInfo oCompanyUserInfo)
            : base(oCompanyUserInfo)
        {
        }

        public TaskImportInfo GetTaskImportForProcessing(DateTime dateRevised)
        {
            TaskImportInfo oTaskImportInfo = new TaskImportInfo();
            if (this.IsDataImportProcessingRequired(Enums.DataImportType.GeneralTaskImport))
            {
                this.GetDataImportForProcessing(oTaskImportInfo, Enums.DataImportType.GeneralTaskImport, dateRevised);
            }
            return oTaskImportInfo;
        }

       

        public void ProcessTransferedTaskData(TaskImportInfo oTaskImportInfo)
        {
            string xmlReturnString;
            SqlDataReader reader = null;
            List<TaskHdrInfo> taskCreatedList = null;
         
            using (SqlConnection cnn = GetConnection())
            {
                SqlCommand oCommand = GetTaskUploadProcessingCommand(oTaskImportInfo);
                cnn.Open();
                oCommand.Connection = cnn;
                //oCommand.ExecuteNonQuery();
                try
                {
                    reader = oCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        taskCreatedList = new List<TaskHdrInfo>();
                        TaskHdrInfo oTaskHdrInfo;
                         
                        while (reader.Read())
                        {
                            oTaskHdrInfo = this.MapTaskHdrInfoObject(reader);
                            if (oTaskHdrInfo.TaskID.HasValue && oTaskHdrInfo.TaskID.Value >0)
                            {

                                taskCreatedList.Add(oTaskHdrInfo);
                            }
                          
                        }
                        oTaskImportInfo.CreatedTaskList = taskCreatedList;                      
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
                oTaskImportInfo.ErrorMessageFromSqlServer = oRetVal.ErrorMessageForServiceToLog;
                oTaskImportInfo.ErrorMessageToSave = oRetVal.ErrorMessageToSave;
                oTaskImportInfo.DataImportStatus = oRetVal.ImportStatus;
                if (oRetVal.WarningReasonID.HasValue)
                {
                    oTaskImportInfo.WarningReasonID = oRetVal.WarningReasonID.Value;
                }

                if (oRetVal.RecordsImported.HasValue)
                {
                    oTaskImportInfo.RecordsImported = oRetVal.RecordsImported.Value;
                }
                else
                {
                    oTaskImportInfo.RecordsImported = 0;
                }
            }
            Helper.LogError("8. Data Processing Complete.", this.CompanyUserInfo);
            Helper.LogError(" - Status: " + oTaskImportInfo.DataImportStatus, this.CompanyUserInfo);
            Helper.LogError(" - Message: " + oTaskImportInfo.ErrorMessageFromSqlServer, this.CompanyUserInfo);
            Helper.LogError(" - Records Imported: " + oTaskImportInfo.RecordsImported.ToString(), this.CompanyUserInfo);

            // Data Should be deleted in case there is no warning
            if (oTaskImportInfo.DataImportStatus != DataImportStatus.DATAIMPORTWARNING)
                oTaskImportInfo.IsDataDeletionRequired = true;

            //Raise exception if dataImportStatus = "FAIL". This exception message will be logged into logfile
            if (oTaskImportInfo.DataImportStatus == DataImportStatus.DATAIMPORTFAIL)
                throw new Exception(oTaskImportInfo.ErrorMessageToSave);
        }


        private SqlCommand GetTaskUploadProcessingCommand(TaskImportInfo oTaskImportInfo)
        {
            SqlCommand oCommand = this.CreateCommand();
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.CommandText = "usp_SCV_INS_ProcessUserUploadTransit";

            SqlParameterCollection cmdParamCollectionImport = oCommand.Parameters;
            SqlParameter paramCompanyID = new SqlParameter("@companyID", oTaskImportInfo.CompanyID);
            SqlParameter paramDataImportID = new SqlParameter("@dataImportID", oTaskImportInfo.DataImportID);
            SqlParameter paramAddedBy = new SqlParameter("@addedBy", oTaskImportInfo.AddedBy);
            SqlParameter paramDateAdded = new SqlParameter("@dateAdded", oTaskImportInfo.DateAdded);
            SqlParameter paramIsForceCommit = new SqlParameter("@isForceCommit", oTaskImportInfo.IsForceCommit);

            SqlParameter paramWarningReasonID = new SqlParameter();
            paramWarningReasonID.ParameterName = "@warningReasonID";
            if (oTaskImportInfo.WarningReasonID.HasValue)
                paramWarningReasonID.Value = oTaskImportInfo.WarningReasonID;
            else
                paramWarningReasonID.Value = DBNull.Value;

            SqlParameter paramReturnValue = new SqlParameter("@ReturnValue", SqlDbType.NVarChar, -1);
            paramReturnValue.Direction = ParameterDirection.Output;
           
            cmdParamCollectionImport.Add(paramCompanyID);
            cmdParamCollectionImport.Add(paramDataImportID);
            cmdParamCollectionImport.Add(paramAddedBy);
            cmdParamCollectionImport.Add(paramDateAdded);
            cmdParamCollectionImport.Add(paramIsForceCommit);

            cmdParamCollectionImport.Add(paramWarningReasonID);
            cmdParamCollectionImport.Add(paramReturnValue);

            return oCommand;

        }
        protected TaskHdrInfo MapTaskHdrInfoObject(IDataReader reader)
        {
            TaskHdrInfo oTaskHdrInfo = new TaskHdrInfo();
            int ordinal;
            //TaskID
            try
            {
                ordinal = reader.GetOrdinal("TaskID");
                if (!reader.IsDBNull(ordinal))
                    oTaskHdrInfo.TaskID = reader.GetInt64(ordinal);
                   
            }
            catch (IndexOutOfRangeException indexEx)
            {
            }
            catch (Exception ex)
            {
            }
            //TaskNumber
            try
            {
                ordinal = reader.GetOrdinal("TaskNumber");
                if (!reader.IsDBNull(ordinal))
                    oTaskHdrInfo.TaskNumber = reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException indexEx)
            {
            }
            catch (Exception ex)
            {
            }
            //DataImportID
            try
            {
                ordinal = reader.GetOrdinal("DataImportID");
                if (!reader.IsDBNull(ordinal))
                    oTaskHdrInfo.DataImportID = reader.GetInt32(ordinal);

            }
            catch (IndexOutOfRangeException indexEx)
            {
            }
            catch (Exception ex)
            {
            }
            return oTaskHdrInfo;
        }

    }
}
