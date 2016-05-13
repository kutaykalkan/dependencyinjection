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
using System.Linq;
using SkyStem.ART.Client.Data;

namespace SkyStem.ART.Service.APP.BLL
{
    public class TaskUpload
    {

        TaskImportInfo oTaskImportInfo;
        private CompanyUserInfo CompanyUserInfo;
        private List<LogInfo> LogInfoCache;
        public TaskUpload(CompanyUserInfo oCompanyUserInfo)
        {
            this.CompanyUserInfo = oCompanyUserInfo;
            this.LogInfoCache = new List<LogInfo>();
        }
        #region "Public Functions"
        public bool IsProcessingRequiredForTaskUpload()
        {
            bool isProcessingRequired;
            try
            {
                this.oTaskImportInfo = DataImportHelper.GetTaskImportInfoForProcessing(DateTime.Now, this.CompanyUserInfo);
                if (this.oTaskImportInfo != null && this.oTaskImportInfo.DataImportID > 0)
                {
                    isProcessingRequired = true;
                    Helper.LogInfo(@"Task Upload required for DataImportID: " + this.oTaskImportInfo.DataImportID.ToString(), this.CompanyUserInfo);
                }
                else
                {
                    isProcessingRequired = false;
                    Helper.LogInfo(@"No Data Available for Task Upload.", this.CompanyUserInfo);
                }
            }
            catch (Exception ex)
            {
                oTaskImportInfo = null;
                isProcessingRequired = false;
                Helper.LogError(@"Error in IsProcessingRequiredForTaskUpload: " + ex.Message, this.CompanyUserInfo);
            }
            return isProcessingRequired;
        }
        public void ProcessTaskImport()
        {
            try
            {
                Helper.LogInfo(@"Start Task Upload for DataImportID: " + oTaskImportInfo.DataImportID.ToString(), this.CompanyUserInfo);
                ExtractAndProcessData();
            }
            catch (Exception ex)
            {
                DataImportHelper.ResetTaskImportInfoObject(oTaskImportInfo, ex);
                Helper.LogErrorToCache(ex, this.LogInfoCache);
            }
            finally
            {
                try
                {
                    DataImportHelper.UpdateDataImportHDR(oTaskImportInfo, this.CompanyUserInfo);
                }
                catch (Exception ex)
                {
                    Helper.LogErrorToCache("Error while updating DataImportHDR - ", this.LogInfoCache);
                    Helper.LogErrorToCache(ex, this.LogInfoCache);
                }
                try
                {
                    oTaskImportInfo.SuccessEmailIDs = DataImportHelper.GetEmailIDWithSeprator(oTaskImportInfo.NotifySuccessEmailIds) + DataImportHelper.GetEmailIDWithSeprator( oTaskImportInfo.NotifySuccessUserEmailIds) + DataImportHelper.GetEmailIDWithSeprator(oTaskImportInfo.WarningEmailIds);
                    oTaskImportInfo.FailureEmailIDs = DataImportHelper.GetEmailIDWithSeprator(oTaskImportInfo.NotifyFailureEmailIds )+ DataImportHelper.GetEmailIDWithSeprator(oTaskImportInfo.NotifyFailureUserEmailIds) + DataImportHelper.GetEmailIDWithSeprator(oTaskImportInfo.WarningEmailIds);
                    DataImportHelper.SendMailToUsers(oTaskImportInfo, this.CompanyUserInfo);
                }
                catch (Exception ex)
                {
                    Helper.LogErrorToCache("Error while sending mail - ", this.LogInfoCache);
                    Helper.LogErrorToCache(ex, this.LogInfoCache);
                }
                try
                {
                    Helper.LogListViaService(this.LogInfoCache, oTaskImportInfo.DataImportID, this.CompanyUserInfo);
                    Helper.LogInfo(@"End Task Upload for DataImportID: " + oTaskImportInfo.DataImportID.ToString(), this.CompanyUserInfo);
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
        private void ExtractAndProcessData()
        {
            DataTable dtExcelData = null;
            Helper.LogInfoToCache("3. Start Reading Excel file: " + oTaskImportInfo.PhysicalPath, this.LogInfoCache);
            dtExcelData = DataImportHelper.GetScheduleRecItemImportDataTableFromExcel(oTaskImportInfo.PhysicalPath, TaskUploadConstants.SheetName, this.CompanyUserInfo);

            if (ValidateSchemaForTaskImport(dtExcelData))
            {
                Helper.LogInfoToCache("4. Reading Excel file complete.", this.LogInfoCache);

                if (!oTaskImportInfo.IsForceCommit)
                {//Mark Static Field Present
                    this.FieldPresent(dtExcelData);
                }


                AppUserInfo oAppUserInfo = new AppUserInfo();
                oAppUserInfo.CompanyID = this.CompanyUserInfo.CompanyID;
                oAppUserInfo.LoginID = this.CompanyUserInfo.LoginID;
                ITaskMaster oTaskMasterClient = RemotingHelper.GetTaskMasterObject();
                List<TaskCustomFieldInfo> oTaskCustomFieldInfoList = oTaskMasterClient.GetTaskCustomFields(oTaskImportInfo.RecPeriodID, oTaskImportInfo.CompanyID, oAppUserInfo);
                List<string> CustomFieldList = null;
                if (oTaskCustomFieldInfoList != null && oTaskCustomFieldInfoList.Count > 0)
                {
                    CustomFieldList = new List<string>();
                    int? CustomField1LabelID = oTaskCustomFieldInfoList.Find(obj => obj.TaskCustomFieldID.GetValueOrDefault() == 1).CustomFieldValueLabelID;
                    if (CustomField1LabelID.HasValue)
                    {
                        string CustomField1 = Helper.GetSinglePhrase(CustomField1LabelID.Value, ServiceConstants.DEFAULTBUSINESSENTITYID, oTaskImportInfo.LanguageID, oTaskImportInfo.DefaultLanguageID, this.CompanyUserInfo);
                        if (CustomField1 != null && this.IsColumnExist(dtExcelData, CustomField1))
                            CustomFieldList.Add(CustomField1);
                    }
                    int? CustomField2LabelID = oTaskCustomFieldInfoList.Find(obj => obj.TaskCustomFieldID.GetValueOrDefault() == 2).CustomFieldValueLabelID;
                    if (CustomField2LabelID.HasValue)
                    {
                        string CustomField2 = Helper.GetSinglePhrase(CustomField2LabelID.Value, ServiceConstants.DEFAULTBUSINESSENTITYID, oTaskImportInfo.LanguageID, oTaskImportInfo.DefaultLanguageID, this.CompanyUserInfo);
                        if (CustomField2 != null && this.IsColumnExist(dtExcelData, CustomField2))
                            CustomFieldList.Add(CustomField2);
                        //CustomFieldList.Add(Helper.GetSinglePhrase(CustomField2LabelID.Value, ServiceConstants.DEFAULTBUSINESSENTITYID, oTaskImportInfo.LanguageID, oTaskImportInfo.DefaultLanguageID, this.CompanyUserInfo));
                    }
                }

                //Add additional fields to ExcelDataTabel
                AddDataImportIDToDataTable(dtExcelData);

                //Validate and Convert Data
                ValidateAndConvertData(dtExcelData, CustomFieldList);

                //Process Data
                ProcessdData(dtExcelData, CustomFieldList);
            }
        }
        private void ProcessdData(DataTable dtExcelData, List<string> CustomFieldList)
        {
            int? rowsAffected = null;
            try
            {
                List<TaskHdrInfo> oTaskHdrInfoList = new List<TaskHdrInfo>();
                List<AttachmentInfo> oAttachmentInfoList = new List<AttachmentInfo>();
                IDataImport oDataImport = RemotingHelper.GetDataImportObject();
                AppUserInfo oAppUserInfo = new AppUserInfo();
                oAppUserInfo.CompanyID = this.CompanyUserInfo.CompanyID;
                oAppUserInfo.LoginID = this.CompanyUserInfo.LoginID;
                ITaskMaster oTaskMasterClient = RemotingHelper.GetTaskMasterObject();
                foreach (DataRow dr in dtExcelData.Rows)
                {
                    if (Convert.ToBoolean(dr[TaskUploadConstants.AddedFields.ISVALIDROW]) == true)
                        oTaskHdrInfoList.Add(ConvertRowToTaskHdrInfo(dr, CustomFieldList));
                }
                if (oTaskHdrInfoList.Count > 0)
                {
                    DataTable dtSequence = oTaskMasterClient.AddTask(oTaskHdrInfoList, oTaskImportInfo.RecPeriodID, oTaskImportInfo.AddedBy, System.DateTime.Now, oAttachmentInfoList, oAppUserInfo);
                    if (dtSequence != null && dtSequence.Rows.Count > 0 && oTaskHdrInfoList.Count > 0)
                    {
                        rowsAffected = dtSequence.Rows.Count;
                        RaiseTaskAlert(oTaskHdrInfoList);
                    }

                }
                else
                {
                    rowsAffected = 0;
                    throw new Exception(Helper.GetSinglePhrase(1520, ServiceConstants.DEFAULTBUSINESSENTITYID, oTaskImportInfo.LanguageID, oTaskImportInfo.DefaultLanguageID, this.CompanyUserInfo));
                }

            }
            finally
            {
                if (rowsAffected.GetValueOrDefault() > 0)
                {
                    oTaskImportInfo.RecordsImported = rowsAffected.Value;
                    oTaskImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTSUCCESS;
                }
                else
                {
                    oTaskImportInfo.RecordsImported = rowsAffected.Value;
                    oTaskImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
                }
            }
        }
        private TaskHdrInfo ConvertRowToTaskHdrInfo(DataRow dr, List<string> CustomFieldList)
        {
            TaskHdrInfo oTaskHdrInfo = new TaskHdrInfo();
            oTaskHdrInfo.AddedBy = oTaskImportInfo.AddedBy;
            oTaskHdrInfo.DateAdded = System.DateTime.Now;
            oTaskHdrInfo.IsDeleted = false;
            oTaskHdrInfo.RecPeriodID = oTaskImportInfo.RecPeriodID;
            oTaskHdrInfo.TaskTypeID = (short)ARTEnums.TaskType.GeneralTask;
            oTaskHdrInfo.DataImportID = oTaskImportInfo.DataImportID;
            oTaskHdrInfo.TempTaskSequenceNumber = Convert.ToInt32(dr[ScheduleRecItemUploadConstants.AddedFields.EXCELROWNUMBER]);
            oTaskHdrInfo.IsActive = true;
            //Task List Name 
            TaskListHdrInfo oTaskListHdrInfo = new TaskListHdrInfo();
            oTaskListHdrInfo.TaskListName = Convert.ToString(dr[TaskUploadConstants.TaskUploadFields.TASKLISTNAME]);
            oTaskHdrInfo.TaskList = oTaskListHdrInfo;
            //Task Sublist Name 
            if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.TASKSUBLISTNAME))
            {
                TaskSubListHdrInfo oTaskSubListHdrInfo = new TaskSubListHdrInfo();
                oTaskSubListHdrInfo.TaskSubListName = Convert.ToString(dr[TaskUploadConstants.TaskUploadFields.TASKSUBLISTNAME]);
                oTaskHdrInfo.TaskSubList = oTaskSubListHdrInfo;
            }
            //Task Name       
            oTaskHdrInfo.TaskName = Convert.ToString(dr[TaskUploadConstants.TaskUploadFields.TASKNAME]);
            //Description
            if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.DESCRIPTION))
                oTaskHdrInfo.TaskDescription = Convert.ToString(dr[TaskUploadConstants.TaskUploadFields.DESCRIPTION]);
            if (CustomFieldList != null && CustomFieldList.Count > 0)
            {

                //CustomField1
                if (!string.IsNullOrEmpty(CustomFieldList[0]) && IsDataPresent(dr, CustomFieldList[0]))
                    oTaskHdrInfo.CustomField1 = Convert.ToString(dr[CustomFieldList[0]]);
                //CustomField2
                if (CustomFieldList.Count > 1 && !string.IsNullOrEmpty(CustomFieldList[1]) && IsDataPresent(dr, CustomFieldList[1]))
                    oTaskHdrInfo.CustomField2 = Convert.ToString(dr[CustomFieldList[1]]);
            }
            //Assigned To
            if (IsDataPresent(dr, TaskUploadConstants.AddedFields.ASSIGNEDTOUSERID))
            {

                List<UserHdrInfo> oAssignToUserHdrInfoList = new List<UserHdrInfo>();
                string[] AssignTo = Convert.ToString(dr[TaskUploadConstants.AddedFields.ASSIGNEDTOUSERID]).Split(',');
                for (int i = 0; i < AssignTo.Length; i++)
                {
                    UserHdrInfo oAssignToUserHdrInfo = new UserHdrInfo();
                    oAssignToUserHdrInfo.UserID = Convert.ToInt32(AssignTo[i]);
                    oAssignToUserHdrInfoList.Add(oAssignToUserHdrInfo);
                }
                oTaskHdrInfo.AssignedTo = oAssignToUserHdrInfoList;
            }
            //Reviewer
            if (IsDataPresent(dr, TaskUploadConstants.AddedFields.REVIEWERUSERID))
            {
                List<UserHdrInfo> oReviewerUserHdrInfoList = new List<UserHdrInfo>();
                string[] Reviewer = Convert.ToString(dr[TaskUploadConstants.AddedFields.REVIEWERUSERID]).Split(',');
                for (int i = 0; i < Reviewer.Length; i++)
                {
                    UserHdrInfo oReviewerUserHdrInfo = new UserHdrInfo();
                    oReviewerUserHdrInfo.UserID = Convert.ToInt32(Reviewer[i]);
                    oReviewerUserHdrInfoList.Add(oReviewerUserHdrInfo);
                }
                oTaskHdrInfo.Reviewer = oReviewerUserHdrInfoList;
            }
            //Approver
            if (IsDataPresent(dr, TaskUploadConstants.AddedFields.APPROVERUSERID))
            {
                List<UserHdrInfo> oApproverUserHdrInfoList = new List<UserHdrInfo>();
                string[] Approver = Convert.ToString(dr[TaskUploadConstants.AddedFields.APPROVERUSERID]).Split(',');
                for (int i = 0; i < Approver.Length; i++)
                {
                    UserHdrInfo oApproverUserHdrInfo = new UserHdrInfo();
                    oApproverUserHdrInfo.UserID = Convert.ToInt32(Approver[i]);
                    oApproverUserHdrInfoList.Add(oApproverUserHdrInfo);
                }
                oTaskHdrInfo.Approver = oApproverUserHdrInfoList;
            }
            //Notify
            if (IsDataPresent(dr, TaskUploadConstants.AddedFields.NOTIFYUSERID))
            {
                List<UserHdrInfo> oNotifyUserHdrInfoList = new List<UserHdrInfo>();
                string[] Notiy = Convert.ToString(dr[TaskUploadConstants.AddedFields.NOTIFYUSERID]).Split(',');
                for (int i = 0; i < Notiy.Length; i++)
                {
                    UserHdrInfo oNotiyUserHdrInfo = new UserHdrInfo();
                    oNotiyUserHdrInfo.UserID = Convert.ToInt32(Notiy[i]);
                    oNotifyUserHdrInfoList.Add(oNotiyUserHdrInfo);
                }
                oTaskHdrInfo.Notify = oNotifyUserHdrInfoList;
            }
            //Recurrence Type
            short TaskRecurrenceTypeID = Convert.ToInt16(dr[TaskUploadConstants.AddedFields.RECURRENCETYPEID]);
            TaskRecurrenceTypeMstInfo oTaskRecurr = new TaskRecurrenceTypeMstInfo();
            oTaskRecurr.TaskRecurrenceTypeID = TaskRecurrenceTypeID;
            oTaskHdrInfo.RecurrenceType = oTaskRecurr;
            if (TaskRecurrenceTypeID == (short)ARTEnums.TaskRecurrenceType.NoRecurrence)
            {
                //Task Due Date
                oTaskHdrInfo.TaskDueDate = Convert.ToDateTime(dr[TaskUploadConstants.TaskUploadFields.TASKDUEDATE]);
                //Assignee Due Date
                if (IsDataPresent(dr, TaskUploadConstants.AddedFields.REVIEWERUSERID) && IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDATE))
                    oTaskHdrInfo.AssigneeDueDate = Convert.ToDateTime(dr[TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDATE]);
                //Reviewer Due Date
                if (IsDataPresent(dr, TaskUploadConstants.AddedFields.APPROVERUSERID) && IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.REVIEWERDUEDATE))
                    oTaskHdrInfo.ReviewerDueDate = Convert.ToDateTime(dr[TaskUploadConstants.TaskUploadFields.REVIEWERDUEDATE]);
            }
            else
            {
                //Task Due Days
                oTaskHdrInfo.TaskDueDays = Convert.ToInt32(dr[TaskUploadConstants.TaskUploadFields.TASKDUEDAYS]);
                //Assignee Due Days  
                if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDAYS) && IsDataPresent(dr, TaskUploadConstants.AddedFields.REVIEWERUSERID))
                    oTaskHdrInfo.AssigneeDueDays = Convert.ToInt32(dr[TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDAYS]);
                //Reviewer Due Days  
                if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.REVIEWERDUEDAYS) && IsDataPresent(dr, TaskUploadConstants.AddedFields.APPROVERUSERID))
                    oTaskHdrInfo.ReviewerDueDays = Convert.ToInt32(dr[TaskUploadConstants.TaskUploadFields.REVIEWERDUEDAYS]);
                //Task Due Days Basis
                oTaskHdrInfo.TaskDueDaysBasis = Convert.ToInt16(dr[TaskUploadConstants.AddedFields.TASKDUEDAYSBASISID]);
                //Task Due Days skip number
                if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.TASKDUEDAYSSKIPNUMBER))
                    oTaskHdrInfo.TaskDueDaysBasisSkipNumber = Convert.ToInt16(dr[TaskUploadConstants.TaskUploadFields.TASKDUEDAYSSKIPNUMBER]);
                else
                    oTaskHdrInfo.TaskDueDaysBasisSkipNumber = 0;
                if (IsDataPresent(dr, TaskUploadConstants.AddedFields.ASSIGNEEDUEDAYSBASISID) && IsDataPresent(dr, TaskUploadConstants.AddedFields.REVIEWERUSERID))
                {
                    oTaskHdrInfo.AssigneeDueDaysBasis = Convert.ToInt16(dr[TaskUploadConstants.AddedFields.ASSIGNEEDUEDAYSBASISID]);
                    //Assignee Due Days skip number
                    if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDAYSSKIPNUMBER))
                        oTaskHdrInfo.AssigneeDueDaysBasisSkipNumber = Convert.ToInt16(dr[TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDAYSSKIPNUMBER]);
                    else
                        oTaskHdrInfo.AssigneeDueDaysBasisSkipNumber = 0;
                }
                if (IsDataPresent(dr, TaskUploadConstants.AddedFields.REVIEWERDUEDAYSBASISID) && IsDataPresent(dr, TaskUploadConstants.AddedFields.APPROVERUSERID))
                {
                    oTaskHdrInfo.ReviewerDueDaysBasis = Convert.ToInt16(dr[TaskUploadConstants.AddedFields.REVIEWERDUEDAYSBASISID]);
                    //Reviewer Due Days skip number
                    if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.REVIEWERDUEDAYSSKIPNUMBER))
                        oTaskHdrInfo.ReviewerDueDaysBasisSkipNumber = Convert.ToInt16(dr[TaskUploadConstants.TaskUploadFields.REVIEWERDUEDAYSSKIPNUMBER]);
                    else
                        oTaskHdrInfo.ReviewerDueDaysBasisSkipNumber = 0;
                }
                //Day Type  
                if (IsDataPresent(dr, TaskUploadConstants.AddedFields.DAYTYPEID))
                    oTaskHdrInfo.DaysTypeID = Convert.ToInt16(dr[TaskUploadConstants.AddedFields.DAYTYPEID]);
            }
            if (TaskRecurrenceTypeID == (short)ARTEnums.TaskRecurrenceType.Custom)
            {
                //Recurrence Frequency
                oTaskHdrInfo.RecurrenceFrequency = GetRecurrencePeriodIDList(dr, TaskUploadConstants.AddedFields.RECURRENCEPERIODID);
            }
            else if (TaskRecurrenceTypeID == (short)ARTEnums.TaskRecurrenceType.Quarterly)
            {
                oTaskHdrInfo.RecurrencePeriodNumberList = GetRecurrencePeriodNumberList(dr, TaskUploadConstants.TaskUploadFields.PERIODNUMBER);
            }
            else if (TaskRecurrenceTypeID == (short)ARTEnums.TaskRecurrenceType.Annually)
            {
                oTaskHdrInfo.RecurrencePeriodNumberList = GetRecurrencePeriodNumberList(dr, TaskUploadConstants.TaskUploadFields.PERIODNUMBER);
            }
            else if (TaskRecurrenceTypeID == (short)ARTEnums.TaskRecurrenceType.MultipleRecPeriod)
            {
                oTaskHdrInfo.RecurrencePeriodNumberList = GetRecurrencePeriodNumberList(dr, TaskUploadConstants.TaskUploadFields.PERIODNUMBER);
            }

            return oTaskHdrInfo;
        }
        private bool ValidateSchemaForTaskImport(DataTable dtExcelData)
        {
            bool isValidSchema;
            bool columnFound;
            StringBuilder oSbError = new StringBuilder();

            //Get list of all mandatory fields
            List<string> ImportMandatoryFieldList = DataImportHelper.GetTaskImportMandatoryFields();

            //Check if all mandatory fields exists in DataTable from Excel
            foreach (string fieldName in ImportMandatoryFieldList)
            {
                columnFound = false;
                for (int i = 0; i < dtExcelData.Columns.Count; i++)
                {
                    if (fieldName == dtExcelData.Columns[i].ColumnName)
                    {
                        columnFound = true;
                        break;
                    }
                }
                if (!columnFound)
                {
                    if (!oSbError.ToString().Equals(string.Empty))
                        oSbError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    oSbError.Append(fieldName);
                }
            }
            isValidSchema = string.IsNullOrEmpty(oSbError.ToString());

            //If schema is not valid, generate a multi lingual error message, set failure status, faliure status ID, error message 
            //in TaskImport object and throw an exception with generated message 
            if (!isValidSchema)
            {
                string errorMessage = Helper.GetSinglePhrase(5000165, 0, oTaskImportInfo.LanguageID, oTaskImportInfo.DefaultLanguageID, this.CompanyUserInfo);//Mandatory columns not present: {0}

                oTaskImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
                oTaskImportInfo.DataImportStatusID = (short)Enums.DataImportStatus.Failure;
                oTaskImportInfo.ErrorMessageToSave = String.Format(errorMessage, oSbError.ToString());
                throw new Exception(String.Format(errorMessage, oSbError.ToString()));
            }
            return isValidSchema;
        }
        private bool IsColumnExist(DataTable dtExcelData, string fieldName)
        {
            bool columnFound = false;
            for (int i = 0; i < dtExcelData.Columns.Count; i++)
            {
                if (fieldName.Trim().ToUpper() == dtExcelData.Columns[i].ColumnName.Trim().ToUpper())
                {
                    columnFound = true;
                    break;
                }
            }
            return columnFound;
        }
        private void ValidateAndConvertData(DataTable dtExcelData, List<string> CustomFieldList)
        {

            StringBuilder oSBError = new StringBuilder();
            string msg = Helper.GetDataLengthErrorMessage(ServiceConstants.DEFAULTBUSINESSENTITYID, oTaskImportInfo.LanguageID, oTaskImportInfo.DefaultLanguageID, this.CompanyUserInfo);
            string InvalidDataMsg = Helper.GetInvalidDataErrorMessage(ServiceConstants.DEFAULTBUSINESSENTITYID, oTaskImportInfo.LanguageID, oTaskImportInfo.DefaultLanguageID, this.CompanyUserInfo);
            string msgMandatoryField = Helper.GetMandatoryFieldErrorMessage(ServiceConstants.DEFAULTBUSINESSENTITYID, oTaskImportInfo.LanguageID, oTaskImportInfo.DefaultLanguageID, this.CompanyUserInfo);
            string msgLessThan = Helper.GetLessThanErrorMessage(ServiceConstants.DEFAULTBUSINESSENTITYID, oTaskImportInfo.LanguageID, oTaskImportInfo.DefaultLanguageID, this.CompanyUserInfo);
            string msgDifferentValue = Helper.GetSinglePhrase(5000354, ServiceConstants.DEFAULTBUSINESSENTITYID, oTaskImportInfo.LanguageID, oTaskImportInfo.DefaultLanguageID, this.CompanyUserInfo);


            //2560 -- No Recurrence
            //1733 -- Every Rec Period
            //2740 -- Custom
            //1087 -- Quarterly
            //2730 -- Annually
            //1185 -- Multiple Rec Period
            //5000140 -- Current Date
            // 1978 - Period End Date
            //2776 -- Month End Date
            string strNoRecurrence = Helper.GetSinglePhrase(2560, ServiceConstants.DEFAULTBUSINESSENTITYID, oTaskImportInfo.LanguageID, oTaskImportInfo.DefaultLanguageID, this.CompanyUserInfo).ToLower().Trim();
            string strEveryRecPeriod = Helper.GetSinglePhrase(1733, ServiceConstants.DEFAULTBUSINESSENTITYID, oTaskImportInfo.LanguageID, oTaskImportInfo.DefaultLanguageID, this.CompanyUserInfo).ToLower().Trim();
            string strCustom = Helper.GetSinglePhrase(2740, ServiceConstants.DEFAULTBUSINESSENTITYID, oTaskImportInfo.LanguageID, oTaskImportInfo.DefaultLanguageID, this.CompanyUserInfo).ToLower().Trim();
            string strQuarterly = Helper.GetSinglePhrase(1087, ServiceConstants.DEFAULTBUSINESSENTITYID, oTaskImportInfo.LanguageID, oTaskImportInfo.DefaultLanguageID, this.CompanyUserInfo).ToLower().Trim();
            string strAnnually = Helper.GetSinglePhrase(2730, ServiceConstants.DEFAULTBUSINESSENTITYID, oTaskImportInfo.LanguageID, oTaskImportInfo.DefaultLanguageID, this.CompanyUserInfo).ToLower().Trim();
            string strMultipleRecPeriod = Helper.GetSinglePhrase(1185, ServiceConstants.DEFAULTBUSINESSENTITYID, oTaskImportInfo.LanguageID, oTaskImportInfo.DefaultLanguageID, this.CompanyUserInfo).ToLower().Trim();
            string strCurrentDate = Helper.GetSinglePhrase(5000140, ServiceConstants.DEFAULTBUSINESSENTITYID, oTaskImportInfo.LanguageID, oTaskImportInfo.DefaultLanguageID, this.CompanyUserInfo).ToLower().Trim();
            string strPeriodEndDate = Helper.GetSinglePhrase(1978, ServiceConstants.DEFAULTBUSINESSENTITYID, oTaskImportInfo.LanguageID, oTaskImportInfo.DefaultLanguageID, this.CompanyUserInfo).ToLower().Trim();
            string strMonthEndDate = Helper.GetSinglePhrase(2776, ServiceConstants.DEFAULTBUSINESSENTITYID, oTaskImportInfo.LanguageID, oTaskImportInfo.DefaultLanguageID, this.CompanyUserInfo).ToLower().Trim();
            string strCalendarDays = Helper.GetSinglePhrase(2960, ServiceConstants.DEFAULTBUSINESSENTITYID, oTaskImportInfo.LanguageID, oTaskImportInfo.DefaultLanguageID, this.CompanyUserInfo).ToLower().Trim();
            string strBusinessDays = Helper.GetSinglePhrase(2961, ServiceConstants.DEFAULTBUSINESSENTITYID, oTaskImportInfo.LanguageID, oTaskImportInfo.DefaultLanguageID, this.CompanyUserInfo).ToLower().Trim();

            int validRowCount = 0;

            // Get User  list for current company
            IUser oUser = RemotingHelper.GetUserObject();
            AppUserInfo oAppUserInfo = new AppUserInfo();
            oAppUserInfo.CompanyID = this.CompanyUserInfo.CompanyID;
            oAppUserInfo.LoginID = this.CompanyUserInfo.LoginID;
            List<UserHdrInfo> oUserHdrInfoList = oUser.SelectAllUsersRolesAndEmailID(this.oTaskImportInfo.CompanyID, oAppUserInfo);
            ICompany oCompany = RemotingHelper.GetCompanyObject();
            IList<ReconciliationPeriodInfo> oReconciliationPeriodInfoList = oCompany.SelectAllReconciliationPeriodByCompanyID(this.oTaskImportInfo.CompanyID, null, oAppUserInfo);

            for (int x = 0; x < dtExcelData.Rows.Count; x++)
            {
                DataRow dr = dtExcelData.Rows[x];
                string excelRowNumber = dr[ScheduleRecItemUploadConstants.AddedFields.EXCELROWNUMBER].ToString();
                bool isValidRow = true;
                List<int> ValidAssignToIDList = new List<int>();
                List<int> ValidReviewerIDList = new List<int>();
                List<int> ValidApproverIDLIst = new List<int>();
                List<int> ValidNotifyIDList = new List<int>();


                // Validate TaskListName
                if (!IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.TASKLISTNAME))
                {
                    oSBError.Append(String.Format(msgMandatoryField, TaskUploadConstants.TaskUploadFields.TASKLISTNAME, excelRowNumber));
                    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    isValidRow = false;
                }
                else if (dr[TaskUploadConstants.TaskUploadFields.TASKLISTNAME].ToString().Trim().Length > TaskUploadConstants.DataLength.TaskListName)
                {
                    oSBError.Append(String.Format(msg, TaskUploadConstants.TaskUploadFields.TASKLISTNAME, excelRowNumber));
                    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    isValidRow = false;
                }
                else
                {
                    dr[TaskUploadConstants.TaskUploadFields.TASKLISTNAME] = dr[TaskUploadConstants.TaskUploadFields.TASKLISTNAME].ToString().Trim();
                }

                // Validate TaskSubListName
                if (dr[TaskUploadConstants.TaskUploadFields.TASKSUBLISTNAME].ToString().Trim().Length > TaskUploadConstants.DataLength.TaskSubListName)
                {
                    oSBError.Append(String.Format(msg, TaskUploadConstants.TaskUploadFields.TASKSUBLISTNAME, excelRowNumber));
                    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    isValidRow = false;
                }
                else
                {
                    dr[TaskUploadConstants.TaskUploadFields.TASKSUBLISTNAME] = dr[TaskUploadConstants.TaskUploadFields.TASKSUBLISTNAME].ToString().Trim();
                }

                // Validate Task Name
                if (!IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.TASKNAME))
                {
                    oSBError.Append(String.Format(msgMandatoryField, TaskUploadConstants.TaskUploadFields.TASKNAME, excelRowNumber));
                    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    isValidRow = false;
                }
                else if (dr[TaskUploadConstants.TaskUploadFields.TASKNAME].ToString().Trim().Length > TaskUploadConstants.DataLength.TaskAttributeValue)
                {
                    oSBError.Append(String.Format(msg, TaskUploadConstants.TaskUploadFields.TASKNAME, excelRowNumber));
                    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    isValidRow = false;
                }
                else
                {
                    dr[TaskUploadConstants.TaskUploadFields.TASKNAME] = dr[TaskUploadConstants.TaskUploadFields.TASKNAME].ToString().Trim();
                }

                // Validate Description
                if (dr[TaskUploadConstants.TaskUploadFields.DESCRIPTION].ToString().Trim().Length > TaskUploadConstants.DataLength.TaskAttributeValue)
                {
                    oSBError.Append(String.Format(msg, TaskUploadConstants.TaskUploadFields.DESCRIPTION, excelRowNumber));
                    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    isValidRow = false;
                }
                else
                {
                    dr[TaskUploadConstants.TaskUploadFields.DESCRIPTION] = dr[TaskUploadConstants.TaskUploadFields.DESCRIPTION].ToString().Trim();
                }


                // Validate AssignedTO           
                if (!IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.ASSIGNEDTO))
                {
                    oSBError.Append(String.Format(msgMandatoryField, TaskUploadConstants.TaskUploadFields.ASSIGNEDTO, excelRowNumber));
                    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    isValidRow = false;
                }
                else if (dr[TaskUploadConstants.TaskUploadFields.ASSIGNEDTO].ToString().Trim().Length > TaskUploadConstants.DataLength.UserLoginID)
                {
                    oSBError.Append(String.Format(msg, TaskUploadConstants.TaskUploadFields.ASSIGNEDTO, excelRowNumber));
                    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    isValidRow = false;
                }
                else
                {

                    string ValidUserIDs = string.Empty;
                    string InvalidLoginIds = string.Empty;
                    if (CheckValidLoginID(oUserHdrInfoList, dr[TaskUploadConstants.TaskUploadFields.ASSIGNEDTO].ToString().Trim(), out ValidUserIDs, ValidAssignToIDList, out  InvalidLoginIds))
                    {
                        dr[TaskUploadConstants.AddedFields.ASSIGNEDTOUSERID] = ValidUserIDs;
                    }
                    else
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.ASSIGNEDTO, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        ValidAssignToIDList = null;
                        isValidRow = false;
                    }
                }


                // Validate REVIEWER
                if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.REVIEWER))
                {
                    if (dr[TaskUploadConstants.TaskUploadFields.REVIEWER].ToString().Trim().Length > TaskUploadConstants.DataLength.UserLoginID)
                    {
                        oSBError.Append(String.Format(msg, TaskUploadConstants.TaskUploadFields.REVIEWER, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                    else
                    {

                        string ValidUserIDs = string.Empty;
                        string InvalidLoginIds = string.Empty;
                        if (CheckValidLoginID(oUserHdrInfoList, dr[TaskUploadConstants.TaskUploadFields.REVIEWER].ToString().Trim(), out ValidUserIDs, ValidReviewerIDList, out  InvalidLoginIds))
                        {
                            dr[TaskUploadConstants.AddedFields.REVIEWERUSERID] = ValidUserIDs;
                        }
                        else
                        {
                            oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.REVIEWER, excelRowNumber));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            ValidReviewerIDList = null;
                            isValidRow = false;
                        }
                    }
                }

                // Validate APPROVER
                if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.APPROVER))
                {
                    if (dr[TaskUploadConstants.TaskUploadFields.APPROVER].ToString().Trim().Length > TaskUploadConstants.DataLength.UserLoginID)
                    {
                        oSBError.Append(String.Format(msg, TaskUploadConstants.TaskUploadFields.APPROVER, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                    else
                    {

                        string ValidUserIDs = string.Empty;
                        string InvalidLoginIds = string.Empty;
                        if (CheckValidLoginID(oUserHdrInfoList, dr[TaskUploadConstants.TaskUploadFields.APPROVER].ToString().Trim(), out ValidUserIDs, ValidApproverIDLIst, out  InvalidLoginIds))
                        {
                            dr[TaskUploadConstants.AddedFields.APPROVERUSERID] = ValidUserIDs;
                        }
                        else
                        {
                            oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.APPROVER, excelRowNumber));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            ValidApproverIDLIst = null;
                            isValidRow = false;
                        }
                    }
                }

                // Validate NOTIFY
                if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.NOTIFY))
                {
                    //string[] arrUserLoginID = dr[TaskUploadConstants.TaskUploadFields.NOTIFY].ToString().Trim().Split(',');
                    //string UserIDs = null;
                    //bool IsAllValidUserIDs = true;
                    //for (int i = 0; i < arrUserLoginID.Length; i++)
                    //{
                    //    string tmpUserLoginID = arrUserLoginID[i].ToString().Trim();
                    //    int? UserId = GetUserID(oUserHdrInfoList, tmpUserLoginID);
                    //    if (UserId.HasValue && UserId.Value > 0)
                    //    {
                    //        if (UserIDs == null)
                    //            UserIDs = UserId.Value.ToString();
                    //        else
                    //            UserIDs = UserIDs + "," + UserId.Value.ToString();

                    //        if ((AssignedTOUserID.HasValue && AssignedTOUserID.Value == UserId.Value) || (ApproverUserID.HasValue && ApproverUserID.Value == UserId.Value))
                    //        {
                    //            oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.NOTIFY, excelRowNumber));
                    //            oSBError.Append(" ");
                    //            oSBError.Append(String.Format(msgDifferentValue, TaskUploadConstants.TaskUploadFields.ASSIGNEDTO, TaskUploadConstants.TaskUploadFields.APPROVER, TaskUploadConstants.TaskUploadFields.NOTIFY));
                    //            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    //            isValidRow = false;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        IsAllValidUserIDs = false;
                    //        break;
                    //    }
                    //}
                    //if (IsAllValidUserIDs)
                    //{
                    //    dr[TaskUploadConstants.AddedFields.NOTIFYUSERID] = UserIDs;
                    //}
                    //else
                    //{
                    //    oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.NOTIFY, excelRowNumber));
                    //    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    //    isValidRow = false;
                    //}

                    if (dr[TaskUploadConstants.TaskUploadFields.NOTIFY].ToString().Trim().Length > TaskUploadConstants.DataLength.UserLoginID)
                    {
                        oSBError.Append(String.Format(msg, TaskUploadConstants.TaskUploadFields.NOTIFY, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                    else
                    {

                        string ValidUserIDs = string.Empty;
                        string InvalidLoginIds = string.Empty;
                        if (CheckValidLoginID(oUserHdrInfoList, dr[TaskUploadConstants.TaskUploadFields.NOTIFY].ToString().Trim(), out ValidUserIDs, ValidNotifyIDList, out  InvalidLoginIds))
                        {
                            dr[TaskUploadConstants.AddedFields.NOTIFYUSERID] = ValidUserIDs;
                        }
                        else
                        {
                            oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.NOTIFY, excelRowNumber));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            ValidNotifyIDList = null;
                            isValidRow = false;
                        }
                    }

                }
                if (CheckDuplicateUser(ValidAssignToIDList, ValidReviewerIDList, ValidApproverIDLIst, ValidNotifyIDList))
                {
                    oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.ASSIGNEDTO, excelRowNumber));
                    oSBError.Append(System.Environment.NewLine);
                    oSBError.Append(String.Format(msgDifferentValue, TaskUploadConstants.TaskUploadFields.ASSIGNEDTO, TaskUploadConstants.TaskUploadFields.REVIEWER, TaskUploadConstants.TaskUploadFields.APPROVER, TaskUploadConstants.TaskUploadFields.NOTIFY));
                    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    isValidRow = false;
                }
                // Recurrence Type
                string RecurrenceType = strNoRecurrence;
                if (!IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.RECURRENCETYPE))
                {
                    oSBError.Append(String.Format(msgMandatoryField, TaskUploadConstants.TaskUploadFields.RECURRENCETYPE, excelRowNumber));
                    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    isValidRow = false;
                }
                else
                {
                    RecurrenceType = dr[TaskUploadConstants.TaskUploadFields.RECURRENCETYPE].ToString().Trim().ToLower();
                }

                if (RecurrenceType == strNoRecurrence)
                {
                    dr[TaskUploadConstants.AddedFields.RECURRENCETYPEID] = (short)ARTEnums.TaskRecurrenceType.NoRecurrence;

                    if (!IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.TASKDUEDATE))
                    {
                        oSBError.Append(String.Format(msgMandatoryField, TaskUploadConstants.TaskUploadFields.TASKDUEDATE, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                    if (IsDataPresent(dr, TaskUploadConstants.AddedFields.REVIEWERUSERID))
                    {
                        if (!IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDATE))
                        {
                            oSBError.Append(String.Format(msgMandatoryField, TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDATE, excelRowNumber));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            isValidRow = false;
                        }
                    }
                    else
                    {
                        if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDATE))
                        {
                            oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDATE, excelRowNumber));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            isValidRow = false;
                        }
                    }
                    if (IsDataPresent(dr, TaskUploadConstants.AddedFields.APPROVERUSERID))
                    {
                        if (!IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.REVIEWERDUEDATE))
                        {
                            oSBError.Append(String.Format(msgMandatoryField, TaskUploadConstants.TaskUploadFields.REVIEWERDUEDATE, excelRowNumber));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            isValidRow = false;
                        }
                    }
                    else
                    {
                        if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.REVIEWERDUEDATE))
                        {
                            oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.REVIEWERDUEDATE, excelRowNumber));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            isValidRow = false;
                        }
                    }

                    if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.TASKDUEDAYS))
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.TASKDUEDAYS, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                    if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDAYS))
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDAYS, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                    if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.REVIEWERDUEDAYS))
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.REVIEWERDUEDAYS, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }

                    if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.RECURRENCEFREQUENCY))
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.RECURRENCEFREQUENCY, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                    if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.PERIODNUMBER))
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.PERIODNUMBER, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }


                }
                else if (RecurrenceType == strEveryRecPeriod)
                {
                    dr[TaskUploadConstants.AddedFields.RECURRENCETYPEID] = (short)ARTEnums.TaskRecurrenceType.EveryRecPeriod;

                    if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.RECURRENCEFREQUENCY))
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.RECURRENCEFREQUENCY, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                    if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.PERIODNUMBER))
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.PERIODNUMBER, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }

                }
                else if (RecurrenceType == strCustom)
                {
                    dr[TaskUploadConstants.AddedFields.RECURRENCETYPEID] = (short)ARTEnums.TaskRecurrenceType.Custom;
                    if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.PERIODNUMBER))
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.PERIODNUMBER, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                    if (!IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.RECURRENCEFREQUENCY))
                    {
                        oSBError.Append(String.Format(msgMandatoryField, TaskUploadConstants.TaskUploadFields.RECURRENCEFREQUENCY, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                    string PeriodIDs = null;
                    string[] arrPeriodEndDates = dr[TaskUploadConstants.TaskUploadFields.RECURRENCEFREQUENCY].ToString().Trim().Split(',').Distinct().ToArray();
                    bool IsAllValidPeriodIDs = true;
                    DateTime CurrentPeriodEndDate = DateTime.MinValue;
                    Helper.IsValidDateTime(oTaskImportInfo.PeriodEndDate.ToString(), oTaskImportInfo.LanguageID, out CurrentPeriodEndDate);

                    for (int i = 0; i < arrPeriodEndDates.Length; i++)
                    {
                        DateTime tmpPeriodEndDate = DateTime.MinValue;
                        if (Helper.IsValidDateTime(arrPeriodEndDates[i], oTaskImportInfo.LanguageID, out tmpPeriodEndDate))
                        {


                            int? PeriodID = GetRecPeriodID(oReconciliationPeriodInfoList, tmpPeriodEndDate);
                            if (PeriodID.HasValue)
                            {
                                if (PeriodIDs == null)
                                    PeriodIDs = PeriodID.Value.ToString();
                                else
                                    PeriodIDs = PeriodIDs + "," + PeriodID.Value.ToString();

                                if (CurrentPeriodEndDate > tmpPeriodEndDate)
                                {
                                    oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.RECURRENCEFREQUENCY, excelRowNumber));
                                    oSBError.Append(" ");
                                    string msgPeriodDateLessThan = Helper.GetSinglePhrase(2746, ServiceConstants.DEFAULTBUSINESSENTITYID, oTaskImportInfo.LanguageID, oTaskImportInfo.DefaultLanguageID, this.CompanyUserInfo);
                                    oSBError.Append(msgPeriodDateLessThan);
                                    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                                    isValidRow = false;
                                    break;
                                }

                            }
                            else
                            {
                                IsAllValidPeriodIDs = false;
                                break;
                            }
                        }
                        else
                        {

                            oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.RECURRENCEFREQUENCY, excelRowNumber));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            isValidRow = false;
                            break;
                        }
                    }
                    if (IsAllValidPeriodIDs)
                    {
                        dr[TaskUploadConstants.AddedFields.RECURRENCEPERIODID] = PeriodIDs;
                    }
                    else
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.RECURRENCEFREQUENCY, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }

                }
                else if (RecurrenceType == strQuarterly)
                {
                    dr[TaskUploadConstants.AddedFields.RECURRENCETYPEID] = (short)ARTEnums.TaskRecurrenceType.Quarterly;
                    string[] arrPeriodNumbers = dr[TaskUploadConstants.TaskUploadFields.PERIODNUMBER].ToString().Trim().Split(',').Distinct().ToArray();
                    isValidRow = ValiDatePeriodNumber(arrPeriodNumbers, oSBError, InvalidDataMsg, oReconciliationPeriodInfoList, dr, excelRowNumber, isValidRow, 4);

                    if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.RECURRENCEFREQUENCY))
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.RECURRENCEFREQUENCY, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                    if (!IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.PERIODNUMBER))
                    {
                        oSBError.Append(String.Format(msgMandatoryField, TaskUploadConstants.TaskUploadFields.PERIODNUMBER, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                }
                else if (RecurrenceType == strAnnually)
                {
                    dr[TaskUploadConstants.AddedFields.RECURRENCETYPEID] = (short)ARTEnums.TaskRecurrenceType.Annually;
                    string[] arrPeriodNumbers = dr[TaskUploadConstants.TaskUploadFields.PERIODNUMBER].ToString().Trim().Split(',');
                    isValidRow = ValiDatePeriodNumber(arrPeriodNumbers, oSBError, InvalidDataMsg, oReconciliationPeriodInfoList, dr, excelRowNumber, isValidRow, 1);

                    if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.RECURRENCEFREQUENCY))
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.RECURRENCEFREQUENCY, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                    if (!IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.PERIODNUMBER))
                    {
                        oSBError.Append(String.Format(msgMandatoryField, TaskUploadConstants.TaskUploadFields.PERIODNUMBER, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                }
                else if (RecurrenceType == strMultipleRecPeriod)
                {
                    dr[TaskUploadConstants.AddedFields.RECURRENCETYPEID] = (short)ARTEnums.TaskRecurrenceType.MultipleRecPeriod;
                    string[] arrPeriodNumbers = dr[TaskUploadConstants.TaskUploadFields.PERIODNUMBER].ToString().Trim().Split(',').Distinct().ToArray();
                    isValidRow = ValiDatePeriodNumber(arrPeriodNumbers, oSBError, InvalidDataMsg, oReconciliationPeriodInfoList, dr, excelRowNumber, isValidRow, arrPeriodNumbers.Length);

                    if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.RECURRENCEFREQUENCY))
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.RECURRENCEFREQUENCY, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                    if (!IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.PERIODNUMBER))
                    {
                        oSBError.Append(String.Format(msgMandatoryField, TaskUploadConstants.TaskUploadFields.PERIODNUMBER, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                }
                else
                {
                    oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.RECURRENCETYPE, excelRowNumber));
                    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    isValidRow = false;
                }
                string TaskDueDaysBasis = string.Empty;
                string ReviewerDueDaysBasis = string.Empty;
                string AssigneeDueDaysBasis = string.Empty;
                if (RecurrenceType == strEveryRecPeriod || RecurrenceType == strCustom || RecurrenceType == strQuarterly
                    || RecurrenceType == strAnnually || RecurrenceType == strMultipleRecPeriod)
                {
                    if (!IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.TASKDUEDAYS))
                    {
                        oSBError.Append(String.Format(msgMandatoryField, TaskUploadConstants.TaskUploadFields.TASKDUEDAYS, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                    if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.TASKDUEDATE))
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.TASKDUEDATE, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                    if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDATE))
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDATE, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                    if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.REVIEWERDUEDATE))
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.REVIEWERDUEDATE, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                    if (IsDataPresent(dr, TaskUploadConstants.AddedFields.REVIEWERUSERID))
                    {
                        if (!IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDAYS))
                        {
                            oSBError.Append(String.Format(msgMandatoryField, TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDAYS, excelRowNumber));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            isValidRow = false;
                        }
                    }
                    else
                    {
                        if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDAYS))
                        {
                            oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDAYS, excelRowNumber));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            isValidRow = false;
                        }
                    }
                    if (IsDataPresent(dr, TaskUploadConstants.AddedFields.APPROVERUSERID))
                    {
                        if (!IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.REVIEWERDUEDAYS))
                        {
                            oSBError.Append(String.Format(msgMandatoryField, TaskUploadConstants.TaskUploadFields.REVIEWERDUEDAYS, excelRowNumber));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            isValidRow = false;
                        }
                    }
                    else
                    {
                        if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.REVIEWERDUEDAYS))
                        {
                            oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.REVIEWERDUEDAYS, excelRowNumber));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            isValidRow = false;
                        }
                    }
                    // Days Type
                    string DaysType = string.Empty;
                    if (!IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.DAYTYPE))
                    {
                        oSBError.Append(String.Format(msgMandatoryField, TaskUploadConstants.TaskUploadFields.DAYTYPE, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                    else
                    {
                        DaysType = dr[TaskUploadConstants.TaskUploadFields.DAYTYPE].ToString().Trim().ToLower();
                    }

                    if (DaysType == strCalendarDays)
                    {
                        dr[TaskUploadConstants.AddedFields.DAYTYPEID] = (short)ARTEnums.DaysType.CalendarDays;
                    }
                    else if (DaysType == strBusinessDays)
                    {
                        dr[TaskUploadConstants.AddedFields.DAYTYPEID] = (short)ARTEnums.DaysType.BusinessDays;
                    }
                    else
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.DAYTYPE, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }

                    // Task Due Days Basis

                    if (!IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.TASKDUEDAYSBASIS))
                    {
                        oSBError.Append(String.Format(msgMandatoryField, TaskUploadConstants.TaskUploadFields.TASKDUEDAYSBASIS, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                    else
                    {
                        TaskDueDaysBasis = dr[TaskUploadConstants.TaskUploadFields.TASKDUEDAYSBASIS].ToString().Trim().ToLower();
                    }

                    if (TaskDueDaysBasis == strPeriodEndDate)
                    {
                        dr[TaskUploadConstants.AddedFields.TASKDUEDAYSBASISID] = (short)ARTEnums.DueDaysBasis.PeriodEndDate;
                    }
                    else if (TaskDueDaysBasis == strMonthEndDate)
                    {
                        dr[TaskUploadConstants.AddedFields.TASKDUEDAYSBASISID] = (short)ARTEnums.DueDaysBasis.MonthEndDate;
                    }
                    else
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.TASKDUEDAYSBASIS, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                    // Reviewer Due Days Basis                  
                    if (IsDataPresent(dr, TaskUploadConstants.AddedFields.APPROVERUSERID))
                    {
                        if (!IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.REVIEWERDUEDAYSBASIS))
                        {
                            oSBError.Append(String.Format(msgMandatoryField, TaskUploadConstants.TaskUploadFields.REVIEWERDUEDAYSBASIS, excelRowNumber));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            isValidRow = false;
                        }
                        else
                        {
                            ReviewerDueDaysBasis = dr[TaskUploadConstants.TaskUploadFields.REVIEWERDUEDAYSBASIS].ToString().Trim().ToLower();
                        }

                        if (ReviewerDueDaysBasis == strPeriodEndDate)
                        {
                            dr[TaskUploadConstants.AddedFields.REVIEWERDUEDAYSBASISID] = (short)ARTEnums.DueDaysBasis.PeriodEndDate;
                        }
                        else if (ReviewerDueDaysBasis == strMonthEndDate)
                        {
                            dr[TaskUploadConstants.AddedFields.REVIEWERDUEDAYSBASISID] = (short)ARTEnums.DueDaysBasis.MonthEndDate;
                        }
                        else
                        {
                            oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.REVIEWERDUEDAYSBASIS, excelRowNumber));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            isValidRow = false;
                        }
                    }
                    else
                    {
                        if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.REVIEWERDUEDAYSBASIS))
                        {
                            oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.REVIEWERDUEDAYSBASIS, excelRowNumber));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            isValidRow = false;
                        }
                    }

                    // Assignee Due Days Basis                  
                    if (IsDataPresent(dr, TaskUploadConstants.AddedFields.REVIEWERUSERID))
                    {
                        if (!IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDAYSBASIS))
                        {
                            oSBError.Append(String.Format(msgMandatoryField, TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDAYSBASIS, excelRowNumber));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            isValidRow = false;
                        }
                        else
                        {
                            AssigneeDueDaysBasis = dr[TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDAYSBASIS].ToString().Trim().ToLower();
                        }

                        if (AssigneeDueDaysBasis == strPeriodEndDate)
                        {
                            dr[TaskUploadConstants.AddedFields.ASSIGNEEDUEDAYSBASISID] = (short)ARTEnums.DueDaysBasis.PeriodEndDate;
                        }
                        else if (AssigneeDueDaysBasis == strMonthEndDate)
                        {
                            dr[TaskUploadConstants.AddedFields.ASSIGNEEDUEDAYSBASISID] = (short)ARTEnums.DueDaysBasis.MonthEndDate;
                        }
                        else
                        {
                            oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDAYSBASIS, excelRowNumber));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            isValidRow = false;
                        }
                    }
                    else
                    {
                        if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDAYSBASIS))
                        {
                            oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDAYSBASIS, excelRowNumber));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            isValidRow = false;
                        }
                    }
                }

                // Validate Recurrence Frequency 
                if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.RECURRENCEFREQUENCY))
                {
                    dr[TaskUploadConstants.TaskUploadFields.RECURRENCEFREQUENCY] = dr[TaskUploadConstants.TaskUploadFields.RECURRENCEFREQUENCY].ToString().Trim();
                }
                // Validate period#
                if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.PERIODNUMBER))
                {
                    dr[TaskUploadConstants.TaskUploadFields.PERIODNUMBER] = dr[TaskUploadConstants.TaskUploadFields.PERIODNUMBER].ToString().Trim();
                }
                // Task Due Date
                DateTime TaskDueDate = DateTime.MinValue;
                if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.TASKDUEDATE))
                {
                    if (!Helper.IsValidDateTime(dr[TaskUploadConstants.TaskUploadFields.TASKDUEDATE].ToString(), oTaskImportInfo.LanguageID, out TaskDueDate))
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.TASKDUEDATE, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                    else if (TaskDueDate.Date < DateTime.Now.Date)
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.TASKDUEDATE, excelRowNumber));
                        oSBError.Append(" ");
                        string msgTaskduedateLessThan = Helper.GetSinglePhrase(2737, ServiceConstants.DEFAULTBUSINESSENTITYID, oTaskImportInfo.LanguageID, oTaskImportInfo.DefaultLanguageID, this.CompanyUserInfo);
                        oSBError.Append(msgTaskduedateLessThan);
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                    else
                    {
                        dr[TaskUploadConstants.TaskUploadFields.TASKDUEDATE] = TaskDueDate.ToShortDateString();
                    }
                }

                // Reviewer Due Date
                DateTime ReviewerDueDate = DateTime.MinValue;
                if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.REVIEWERDUEDATE))
                {
                    if (!Helper.IsValidDateTime(dr[TaskUploadConstants.TaskUploadFields.REVIEWERDUEDATE].ToString(), oTaskImportInfo.LanguageID, out ReviewerDueDate))
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.REVIEWERDUEDATE, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                    else if (ReviewerDueDate.Date < DateTime.Now.Date)
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.REVIEWERDUEDATE, excelRowNumber));
                        string msgReviewerduedateLessThan = Helper.GetSinglePhrase(2965, ServiceConstants.DEFAULTBUSINESSENTITYID, oTaskImportInfo.LanguageID, oTaskImportInfo.DefaultLanguageID, this.CompanyUserInfo);
                        oSBError.Append(" ");
                        oSBError.Append(msgReviewerduedateLessThan);
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                    else
                    {
                        dr[TaskUploadConstants.TaskUploadFields.REVIEWERDUEDATE] = ReviewerDueDate.ToShortDateString();
                    }

                    if (TaskDueDate.Date < ReviewerDueDate.Date)
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.REVIEWERDUEDATE, excelRowNumber));
                        oSBError.Append(" ");
                        oSBError.Append(String.Format(msgLessThan, TaskUploadConstants.TaskUploadFields.REVIEWERDUEDATE, TaskUploadConstants.TaskUploadFields.TASKDUEDATE));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                }
                // Assignee Due Date
                DateTime AssigneeDueDate = DateTime.MinValue;
                if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDATE))
                {
                    if (!Helper.IsValidDateTime(dr[TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDATE].ToString(), oTaskImportInfo.LanguageID, out AssigneeDueDate))
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDATE, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                    else if (AssigneeDueDate.Date < DateTime.Now.Date)
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDATE, excelRowNumber));
                        string msgAssigneduedateLessThan = Helper.GetSinglePhrase(2738, ServiceConstants.DEFAULTBUSINESSENTITYID, oTaskImportInfo.LanguageID, oTaskImportInfo.DefaultLanguageID, this.CompanyUserInfo);
                        oSBError.Append(" ");
                        oSBError.Append(msgAssigneduedateLessThan);
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                    else
                    {
                        dr[TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDATE] = AssigneeDueDate.ToShortDateString();
                    }

                    if (ReviewerDueDate.Date < AssigneeDueDate.Date)
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDATE, excelRowNumber));
                        oSBError.Append(" ");
                        oSBError.Append(String.Format(msgLessThan, TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDATE, TaskUploadConstants.TaskUploadFields.REVIEWERDUEDATE));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                }

                // Task Due Days
                int TaskDueDays = 0;
                if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.TASKDUEDAYS))
                {
                    if (!Int32.TryParse(dr[TaskUploadConstants.TaskUploadFields.TASKDUEDAYS].ToString(), out TaskDueDays))
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.TASKDUEDAYS, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                }
                // Task Due Days skip number
                int TaskDueDaysSkipNumber = 0;
                if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.TASKDUEDAYSSKIPNUMBER))
                {
                    if (!Int32.TryParse(dr[TaskUploadConstants.TaskUploadFields.TASKDUEDAYSSKIPNUMBER].ToString(), out TaskDueDaysSkipNumber))
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.TASKDUEDAYSSKIPNUMBER, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                }


                // Reviewer Due Days skip number
                int ReviewerDueDaysSkipNumber = 0;
                if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.REVIEWERDUEDAYSSKIPNUMBER))
                {
                    if (!Int32.TryParse(dr[TaskUploadConstants.TaskUploadFields.REVIEWERDUEDAYSSKIPNUMBER].ToString(), out ReviewerDueDaysSkipNumber))
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.REVIEWERDUEDAYSSKIPNUMBER, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }

                    if (TaskDueDaysBasis == ReviewerDueDaysBasis && TaskDueDaysSkipNumber < ReviewerDueDaysSkipNumber)
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.REVIEWERDUEDAYSSKIPNUMBER, excelRowNumber));
                        oSBError.Append(" ");
                        oSBError.Append(String.Format(msgLessThan, TaskUploadConstants.TaskUploadFields.REVIEWERDUEDAYSSKIPNUMBER, TaskUploadConstants.TaskUploadFields.TASKDUEDAYSSKIPNUMBER));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                }

                // Reviewer Due Days
                int ReviewerDueDays = 0;
                if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.REVIEWERDUEDAYS))
                {
                    if (!Int32.TryParse(dr[TaskUploadConstants.TaskUploadFields.REVIEWERDUEDAYS].ToString(), out ReviewerDueDays))
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.REVIEWERDUEDAYS, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }

                    if (TaskDueDaysBasis == ReviewerDueDaysBasis && TaskDueDaysSkipNumber == ReviewerDueDaysSkipNumber && TaskDueDays < ReviewerDueDays)
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.REVIEWERDUEDAYS, excelRowNumber));
                        oSBError.Append(" ");
                        oSBError.Append(String.Format(msgLessThan, TaskUploadConstants.TaskUploadFields.REVIEWERDUEDAYS, TaskUploadConstants.TaskUploadFields.TASKDUEDAYS));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                }

                // Assignee Due Days skip number
                int AssigneeDueDaysSkipNumber = 0;
                if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDAYSSKIPNUMBER))
                {
                    if (!Int32.TryParse(dr[TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDAYSSKIPNUMBER].ToString(), out AssigneeDueDaysSkipNumber))
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDAYSSKIPNUMBER, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }

                    if (ReviewerDueDaysBasis == AssigneeDueDaysBasis && ReviewerDueDaysSkipNumber < AssigneeDueDaysSkipNumber)
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDAYSSKIPNUMBER, excelRowNumber));
                        oSBError.Append(" ");
                        oSBError.Append(String.Format(msgLessThan, TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDAYSSKIPNUMBER, TaskUploadConstants.TaskUploadFields.REVIEWERDUEDAYSSKIPNUMBER));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                }

                // Assignee Due Days
                int AssigneeDueDays = 0;
                if (IsDataPresent(dr, TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDAYS))
                {
                    if (!Int32.TryParse(dr[TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDAYS].ToString(), out AssigneeDueDays))
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDAYS, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }

                    if (ReviewerDueDaysBasis == AssigneeDueDaysBasis && ReviewerDueDaysSkipNumber == AssigneeDueDaysSkipNumber && ReviewerDueDays < AssigneeDueDays)
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDAYS, excelRowNumber));
                        oSBError.Append(" ");
                        oSBError.Append(String.Format(msgLessThan, TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDAYS, TaskUploadConstants.TaskUploadFields.REVIEWERDUEDAYS));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                }

                dr[TaskUploadConstants.AddedFields.ISVALIDROW] = isValidRow;
                if (isValidRow)
                    validRowCount++;
            }
            //if (!oSBError.ToString().Equals(String.Empty))
            //{
            //    oTaskImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
            //    oTaskImportInfo.DataImportStatusID = (short)Enums.DataImportStatus.Failure;
            //    oTaskImportInfo.ErrorMessageToSave = oSBError.ToString();
            //    throw new Exception(oSBError.ToString());
            //}

            if (!oSBError.ToString().Equals(String.Empty) && !oTaskImportInfo.IsForceCommit)
            {
                if (validRowCount == 0)
                {
                    oTaskImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTFAIL;
                    oTaskImportInfo.DataImportStatusID = (short)Enums.DataImportStatus.Failure;
                }
                else
                {
                    oTaskImportInfo.DataImportStatus = DataImportStatus.DATAIMPORTWARNING;
                    oTaskImportInfo.DataImportStatusID = (short)Enums.DataImportStatus.Warning;
                }
                oTaskImportInfo.ErrorMessageToSave = oSBError.ToString();
                throw new Exception(oSBError.ToString());
            }
        }
        private bool CheckValidLoginID(List<UserHdrInfo> oUserHdrInfoList, string LoginIDs, out string ValidUserIDs, List<int> ValidAssignToIDList, out string InvalidLoginIds)
        {
            string[] arrUserLoginID = LoginIDs.Trim().Split(',');
            string UserIDs = null;
            string AllInvalidLoginId = null;
            bool IsAllValidUserIDs = true;
            for (int i = 0; i < arrUserLoginID.Length; i++)
            {
                string tmpUserLoginID = arrUserLoginID[i].ToString().Trim();
                int? UserId = GetUserID(oUserHdrInfoList, tmpUserLoginID);
                if (UserId.HasValue && UserId.Value > 0)
                {
                    ValidAssignToIDList.Add(UserId.Value);
                    if (UserIDs == null)
                        UserIDs = UserId.Value.ToString();
                    else
                        UserIDs = UserIDs + "," + UserId.Value.ToString();
                }
                else
                {
                    IsAllValidUserIDs = false;
                    if (AllInvalidLoginId == null)
                        AllInvalidLoginId = tmpUserLoginID;
                    else
                        AllInvalidLoginId = AllInvalidLoginId + "," + tmpUserLoginID;
                }
            }
            ValidUserIDs = UserIDs;
            InvalidLoginIds = AllInvalidLoginId;
            return IsAllValidUserIDs;
        }
        private bool CheckDuplicateUser(List<int> AssignToUserIDList, List<int> ReviewerUserIDList, List<int> ApproverUserIDList, List<int> NotifyUserIDList)
        {
            bool IsDuplicate = false;
            int MaxArrayLength = 0;
            if (AssignToUserIDList != null)
                MaxArrayLength = AssignToUserIDList.Count;
            if (ReviewerUserIDList != null && ReviewerUserIDList.Count > MaxArrayLength)
                MaxArrayLength = ReviewerUserIDList.Count;
            if (ApproverUserIDList != null && ApproverUserIDList.Count > MaxArrayLength)
                MaxArrayLength = ApproverUserIDList.Count;
            if (NotifyUserIDList != null && NotifyUserIDList.Count > MaxArrayLength)
                MaxArrayLength = NotifyUserIDList.Count;
            if (MaxArrayLength > 0)
            {
                List<int> TempArray = new List<int>();
                int j = 0;
                for (int i = 0; i < MaxArrayLength; i++)
                {
                    if (AssignToUserIDList != null && AssignToUserIDList.Count > i)
                    {
                        if (TempArray.Contains(AssignToUserIDList[i]))
                        {
                            IsDuplicate = true;
                            break;
                        }
                        TempArray.Add(AssignToUserIDList[i]);
                    }
                    if (ReviewerUserIDList != null && ReviewerUserIDList.Count > i)
                    {
                        if (TempArray.Contains(ReviewerUserIDList[i]))
                        {
                            IsDuplicate = true;
                            break;
                        }
                        TempArray.Add(ReviewerUserIDList[i]);
                    }
                    if (ApproverUserIDList != null && ApproverUserIDList.Count > i)
                    {
                        if (TempArray.Contains(ApproverUserIDList[i]))
                        {
                            IsDuplicate = true;
                            break;
                        }
                        TempArray.Add(ApproverUserIDList[i]);
                    }
                    if (NotifyUserIDList != null && NotifyUserIDList.Count > i)
                    {
                        if (TempArray.Contains(NotifyUserIDList[i]))
                        {
                            IsDuplicate = true;
                            break;
                        }
                        TempArray.Add(NotifyUserIDList[i]);
                    }

                }
            }

            return IsDuplicate;
        }
        private bool ValiDatePeriodNumber(string[] arrPeriodNumbers, StringBuilder oSBError, string InvalidDataMsg, IList<ReconciliationPeriodInfo> oReconciliationPeriodInfoList, DataRow dr, string excelRowNumber, bool isValidRow, int PeriodNumberLength)
        {
            if (arrPeriodNumbers.Length != PeriodNumberLength)
            {
                oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.PERIODNUMBER, excelRowNumber));
                if (PeriodNumberLength == 4 || PeriodNumberLength == 1)
                {
                    string msgPeriodNumber;
                    if (PeriodNumberLength == 4)
                        msgPeriodNumber = Helper.GetSinglePhrase(2739, ServiceConstants.DEFAULTBUSINESSENTITYID, oTaskImportInfo.LanguageID, oTaskImportInfo.DefaultLanguageID, this.CompanyUserInfo);
                    else
                        msgPeriodNumber = Helper.GetSinglePhrase(2745, ServiceConstants.DEFAULTBUSINESSENTITYID, oTaskImportInfo.LanguageID, oTaskImportInfo.DefaultLanguageID, this.CompanyUserInfo);
                    oSBError.Append(" ");
                    oSBError.Append(msgPeriodNumber);
                }
                oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                isValidRow = false;
            }
            else
            {
                string PeriodNumbers = null;
                bool IsAllValidPeriodNumbers = true;
                for (int i = 0; i < arrPeriodNumbers.Length; i++)
                {
                    short tmpPeriodNumber = 0;
                    if (short.TryParse(arrPeriodNumbers[i].ToString().Trim(), out tmpPeriodNumber))
                    {
                        short? PeriodNumber = GetRecPeriodNumber(oReconciliationPeriodInfoList, tmpPeriodNumber);
                        if (PeriodNumber.HasValue)
                        {
                            if (PeriodNumbers == null)
                                PeriodNumbers = PeriodNumber.Value.ToString();
                            else
                                PeriodNumbers = PeriodNumbers + "," + PeriodNumber.Value.ToString();
                        }
                        else
                        {
                            IsAllValidPeriodNumbers = false;
                            break;
                        }
                    }
                    else
                    {
                        oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.PERIODNUMBER, excelRowNumber));
                        oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        isValidRow = false;
                    }
                }
                if (IsAllValidPeriodNumbers)
                {
                    dr[TaskUploadConstants.TaskUploadFields.PERIODNUMBER] = PeriodNumbers;
                }
                else
                {
                    oSBError.Append(String.Format(InvalidDataMsg, TaskUploadConstants.TaskUploadFields.PERIODNUMBER, excelRowNumber));
                    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                    isValidRow = false;
                }
            }
            return isValidRow;
        }
        private void AddDataImportIDToDataTable(DataTable dtExcelData)
        {
            dtExcelData.Columns.Add(TaskUploadConstants.AddedFields.DATAIMPORTID, typeof(System.Int64));
            dtExcelData.Columns.Add(TaskUploadConstants.AddedFields.EXCELROWNUMBER, typeof(System.Int64));
            dtExcelData.Columns.Add(TaskUploadConstants.AddedFields.ASSIGNEDTOUSERID, typeof(System.String));
            dtExcelData.Columns.Add(TaskUploadConstants.AddedFields.REVIEWERUSERID, typeof(System.String));
            dtExcelData.Columns.Add(TaskUploadConstants.AddedFields.APPROVERUSERID, typeof(System.String));
            dtExcelData.Columns.Add(TaskUploadConstants.AddedFields.NOTIFYUSERID, typeof(System.String));
            dtExcelData.Columns.Add(TaskUploadConstants.AddedFields.RECURRENCETYPEID, typeof(System.Int16));
            dtExcelData.Columns.Add(TaskUploadConstants.AddedFields.ISVALIDROW, typeof(System.Boolean));
            dtExcelData.Columns.Add(TaskUploadConstants.AddedFields.RECURRENCEPERIODID, typeof(System.String));
            dtExcelData.Columns.Add(TaskUploadConstants.AddedFields.TASKDUEDAYSBASISID, typeof(System.Int16));
            dtExcelData.Columns.Add(TaskUploadConstants.AddedFields.ASSIGNEEDUEDAYSBASISID, typeof(System.Int16));
            dtExcelData.Columns.Add(TaskUploadConstants.AddedFields.REVIEWERDUEDAYSBASISID, typeof(System.Int16));
            dtExcelData.Columns.Add(TaskUploadConstants.AddedFields.DAYTYPEID, typeof(System.Int16));

            for (int x = 0; x < dtExcelData.Rows.Count; x++)
            {
                dtExcelData.Rows[x][TaskUploadConstants.AddedFields.DATAIMPORTID] = oTaskImportInfo.DataImportID;
                dtExcelData.Rows[x][TaskUploadConstants.AddedFields.EXCELROWNUMBER] = x + 2;
            }
        }
        private void FieldPresent(DataTable dtExcelData)
        {

        }
        private int? GetUserID(List<UserHdrInfo> oUserHdrInfoList, string LoginID)
        {

            //var s = string.Join(",", oUserHdrInfoList.Select(U => U.UserID.ToString()));


            int? UserID = null;
            if (string.IsNullOrEmpty(LoginID))
                return null;
            UserHdrInfo oUserHdrInfo = oUserHdrInfoList.FirstOrDefault(T => T.LoginID.Trim().ToLower() == LoginID.Trim().ToLower());
            if (oUserHdrInfo != null)
                UserID = oUserHdrInfo.UserID;
            return UserID;
        }
        private bool IsDataPresent(DataRow dr, string ColumnName)
        {
            if (dr[ColumnName] == DBNull.Value
                || string.IsNullOrEmpty(dr[ColumnName].ToString())
                || dr[ColumnName].ToString().Trim().Length <= 0
              )
                return false;
            else
                return true;
        }
        private List<ReconciliationPeriodInfo> GetRecurrencePeriodIDList(DataRow dr, string ColumnName)
        {
            string[] RecurrencePeriodIDs = Convert.ToString(dr[ColumnName]).Split(',');
            List<ReconciliationPeriodInfo> oReconciliationPeriodInfolist = new List<ReconciliationPeriodInfo>();
            for (int i = 0; i < RecurrencePeriodIDs.Length; i++)
            {
                int periodID = 0;
                if (int.TryParse(RecurrencePeriodIDs[i], out periodID))
                {
                    ReconciliationPeriodInfo oRecPeriod = new ReconciliationPeriodInfo();
                    oRecPeriod.ReconciliationPeriodID = periodID;
                    oReconciliationPeriodInfolist.Add(oRecPeriod);
                }
            }
            return oReconciliationPeriodInfolist;
        }
        private List<ReconciliationPeriodInfo> GetRecurrencePeriodNumberList(DataRow dr, string ColumnName)
        {
            string[] RecurrencePeriodNumbers = Convert.ToString(dr[ColumnName]).Split(',');
            List<ReconciliationPeriodInfo> oReconciliationPeriodInfolist = new List<ReconciliationPeriodInfo>();
            for (int i = 0; i < RecurrencePeriodNumbers.Length; i++)
            {
                short periodnumber = 0;
                if (short.TryParse(RecurrencePeriodNumbers[i], out periodnumber))
                {
                    ReconciliationPeriodInfo oRecPeriod = new ReconciliationPeriodInfo();
                    oRecPeriod.PeriodNumber = periodnumber;
                    oReconciliationPeriodInfolist.Add(oRecPeriod);
                }
            }
            return oReconciliationPeriodInfolist;
        }
        private int? GetRecPeriodID(IList<ReconciliationPeriodInfo> oReconciliationPeriodInfoList, DateTime PeriodEndDate)
        {
            int? RecPeriodID = null;
            DateTime tmpPeriodEndDate = DateTime.MinValue;
            for (int i = 0; i < oReconciliationPeriodInfoList.Count; i++)
            {
                Helper.IsValidDateTime(oReconciliationPeriodInfoList[i].PeriodEndDate.ToString(), oTaskImportInfo.LanguageID, out tmpPeriodEndDate);
                if (tmpPeriodEndDate == PeriodEndDate)
                    RecPeriodID = oReconciliationPeriodInfoList[i].ReconciliationPeriodID;
            }
            return RecPeriodID;
        }
        private short? GetRecPeriodNumber(IList<ReconciliationPeriodInfo> oReconciliationPeriodInfoList, short PeriodNumber)
        {
            short? RecPeriodNumber = null;
            ReconciliationPeriodInfo oReconciliationPeriodInfo = oReconciliationPeriodInfoList.FirstOrDefault(T => T.PeriodNumber == PeriodNumber);
            if (oReconciliationPeriodInfo != null)
                RecPeriodNumber = oReconciliationPeriodInfo.PeriodNumber;
            return RecPeriodNumber;
        }

        public void RaiseTaskAlert(List<TaskHdrInfo> oTaskHdrInfoList)
        {
            try
            {
                List<int> assignedAssignedToIDCollection = new List<int>();
                List<int> assignedReviewerIDCollection = new List<int>();
                List<int> assignedApproverIDCollection = new List<int>();
                List<int> assignedNotifyIDCollection = new List<int>();
                foreach (var oTaskHdr in oTaskHdrInfoList)
                {

                    if (oTaskHdr.AssignedTo != null && oTaskHdr.AssignedTo.Count > 0)
                    {
                        foreach (var oUserHdrInfo in oTaskHdr.AssignedTo)
                        {
                            if (oUserHdrInfo.UserID.HasValue && !assignedAssignedToIDCollection.Contains(oUserHdrInfo.UserID.Value))
                                assignedAssignedToIDCollection.Add(oUserHdrInfo.UserID.Value);
                        }
                    }
                    if (oTaskHdr.Reviewer != null && oTaskHdr.Reviewer.Count > 0)
                    {
                        foreach (var oUserHdrInfo in oTaskHdr.Reviewer)
                        {
                            if (oUserHdrInfo.UserID.HasValue && !assignedReviewerIDCollection.Contains(oUserHdrInfo.UserID.Value))
                                assignedReviewerIDCollection.Add(oUserHdrInfo.UserID.Value);
                        }
                    }
                    if (oTaskHdr.Approver != null && oTaskHdr.Approver.Count > 0)
                    {
                        foreach (var oUserHdrInfo in oTaskHdr.Approver)
                        {
                            if (oUserHdrInfo.UserID.HasValue && !assignedApproverIDCollection.Contains(oUserHdrInfo.UserID.Value))
                                assignedApproverIDCollection.Add(oUserHdrInfo.UserID.Value);
                        }
                    }
                    if (oTaskHdr.Notify != null && oTaskHdr.Notify.Count > 0)
                    {
                        foreach (var oUserHdrInfo in oTaskHdr.Notify)
                        {
                            if (oUserHdrInfo.UserID.HasValue && !assignedNotifyIDCollection.Contains(oUserHdrInfo.UserID.Value))
                                assignedNotifyIDCollection.Add(oUserHdrInfo.UserID.Value);
                        }
                    }
                }
                // Raise alert
                Helper.RaiseAlertForAssignedTask(assignedAssignedToIDCollection, assignedReviewerIDCollection, assignedApproverIDCollection, assignedNotifyIDCollection, oTaskHdrInfoList, oTaskImportInfo, this.CompanyUserInfo);
            }
            catch (Exception ex)
            {
               
                Helper.LogError(@"Error in Sending Task Alert Mails: " + ex.Message, this.CompanyUserInfo);
            }
        }

        #endregion

    }
}
