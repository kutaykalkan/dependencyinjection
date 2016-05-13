using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using System.Collections.Generic;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Model.Report;

namespace SkyStem.ART.App.DAO.Report
{
    public class TaskCompletionReportDAO : ReportMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TaskCompletionReportDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        internal List<TaskCompletionReportInfo> GetTaskCompletionReport(short taskType, ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch, DataTable dtUser, DataTable dtRole, List<FilterCriteria> oTaskFilterCriteriaCollection, string System)
        {
            List<TaskCompletionReportInfo> oTaskCompletionReportInfoList = new List<TaskCompletionReportInfo>();

            IDbConnection oCon = null;
            IDbCommand oCmd = null;
            IDataReader oReader = null;

            try
            {
                oCon = this.CreateConnection();
                DataTable dtFilterCriteria = ServiceHelper.ConvertFilterCriteriaIntoDataTable(oTaskFilterCriteriaCollection);
                oCmd = this.GetTaskCompletionReportCommand(taskType,oReportSearchCriteria, tblEntitySearch,  dtUser,  dtRole, dtFilterCriteria);
                oCmd.Connection = oCon;
                oCmd.Connection.Open();
                oReader = oCmd.ExecuteReader(CommandBehavior.CloseConnection);
                TaskCompletionReportInfo oTaskCompletionReportInfo = null;
                while (oReader.Read())
                {
                    oTaskCompletionReportInfo = MapObjectTaskCompletionReportInfo(oReader);
                    oTaskCompletionReportInfoList.Add(oTaskCompletionReportInfo);
                }
            }
            finally
            {
                if (oReader != null && !oReader.IsClosed)
                    oReader.Close();

                if (oCon != null && oCon.State != ConnectionState.Closed)
                    oCon.Close();
            }
            return oTaskCompletionReportInfoList;
        }


        protected TaskCompletionReportInfo MapObjectTaskCompletionReportInfo(IDataReader reader)
        {
            TaskCompletionReportInfo oEntity = new TaskCompletionReportInfo();
            oEntity.TaskID = reader.GetInt64Value("TaskID");
            oEntity.TaskNumber = reader.GetStringValue("TaskNumber");
            oEntity.TaskTypeID = reader.GetInt16Value("TaskTypeID");
            oEntity.AccountID = reader.GetInt64Value("AccountID");            
            oEntity.AccountNameLabelID = reader.GetInt32Value("AccountNameLabelID");
            oEntity.AccountNumber = reader.GetStringValue("AccountNumber");

            GeographyObjectHdrDAO oGeogObjHdrDAO = new GeographyObjectHdrDAO(this.CurrentAppUserInfo);
            oGeogObjHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oEntity);

            oEntity.TaskDetailID = reader.GetInt64Value("TaskDetailID");
            oEntity.TaskDetailRecPeriodID = reader.GetInt32Value("TaskDetailRecPeriodID");
            oEntity.TaskStatusID = reader.GetInt16Value("TaskStatusID");
            oEntity.TaskStatusLabelID = reader.GetInt32Value("TaskStatusLabelID");
            oEntity.TaskStatusDate = reader.GetDateValue("TaskStatusDate");
            oEntity.DateAdded = reader.GetDateValue("DateAdded");
            oEntity.DateDone = reader.GetDateValue("DateDone");
            oEntity.DateApproved = reader.GetDateValue("DateApproved");
            oEntity.DateReviewed = reader.GetDateValue("DateReviewed");         
            UserHdrInfo oAddedByUser = new UserHdrInfo();
            oAddedByUser.FirstName = reader.GetStringValue("AddedByFirstName");
            oAddedByUser.LastName = reader.GetStringValue("AddedByLastName");
            oAddedByUser.UserID = reader.GetInt32Value("AddedByUserID");
            oEntity.TaskDetailAddedByUser = oAddedByUser;

            UserHdrInfo oDoneByUser = new UserHdrInfo();
            oDoneByUser.FirstName = reader.GetStringValue("DoneByFirstName");
            oDoneByUser.LastName = reader.GetStringValue("DoneByLastName");
            oDoneByUser.UserID = reader.GetInt32Value("DoneByUserID");
            oEntity.TaskDetailDoneByUser = oDoneByUser;

            UserHdrInfo oReviewedByUser = new UserHdrInfo();
            oReviewedByUser.FirstName = reader.GetStringValue("ReviewedByFirstName");
            oReviewedByUser.LastName = reader.GetStringValue("ReviewedByLastName");
            oReviewedByUser.UserID = reader.GetInt32Value("ReviewedByUserID");
            oEntity.TaskDetailReviewedByUser = oReviewedByUser;       

            UserHdrInfo oApprovedByUser = new UserHdrInfo();
            oApprovedByUser.FirstName = reader.GetStringValue("ApprovedByFirstName");
            oApprovedByUser.LastName = reader.GetStringValue("ApprovedByLastName");
            oApprovedByUser.UserID = reader.GetInt32Value("ApprovedByUserID");
            oEntity.TaskDetailApprovedByUser = oApprovedByUser;             

            #region "Attributes"
            //TaskName
            oEntity.TaskName = reader.GetStringValue("TaskName");
            //TaskList
            TaskListHdrInfo oTaskList = new TaskListHdrInfo();
            oTaskList.TaskListID = reader.GetInt16Value("TaskListID");
            oTaskList.TaskListName = reader.GetStringValue("TaskListName");
            oTaskList.AddedBy = reader.GetStringValue("TaskListAddedBy");
            if (oTaskList.TaskListID.HasValue)
                oEntity.TaskList = oTaskList;
            //Description
            oEntity.TaskDescription = reader.GetStringValue("TaskDescription");
            //Assignee
            //UserHdrInfo oAssignee = new UserHdrInfo();
            //oAssignee.UserID = reader.GetInt32Value("AssigneeID");
            //oAssignee.FirstName = reader.GetStringValue("AssigneeFirstName");
            //oAssignee.LastName = reader.GetStringValue("AssigneeLastName");
            //oEntity.AssignedTo = oAssignee;
            //Approver          
            //UserHdrInfo oApprover = new UserHdrInfo();
            //oApprover.UserID = reader.GetInt32Value("ApproverID");
            //oApprover.FirstName = reader.GetStringValue("ApproverFirstName");
            //oApprover.LastName = reader.GetStringValue("ApproverLastName");
            //oEntity.Approver = oApprover;
            oEntity.TaskOwner = reader.GetStringValue("TaskOwner");
            oEntity.TaskReviewer = reader.GetStringValue("TaskReviewer");
            oEntity.TaskApprover = reader.GetStringValue("TaskApprover");
            oEntity.CustomField1 = reader.GetStringValue("CustomField1");
            oEntity.CustomField2 = reader.GetStringValue("CustomField2");
            #endregion

            return oEntity;
        }

        private IDbCommand GetTaskCompletionReportCommand(short taskType, ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch, DataTable tblUserSearch, DataTable tblRoleSearch, DataTable dtTaskFilterCriteria)
        {
            IDbCommand cmd = this.CreateCommand("[dbo].[usp_RPT_SEL_TaskCompletionReport]");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdTableKeyValue = cmd.CreateParameter();
            cmdTableKeyValue.ParameterName = "@tblKeyValue";
            cmdTableKeyValue.Value = tblEntitySearch;
            cmdParams.Add(cmdTableKeyValue);
            ServiceHelper.AddCommonParametersForUserRoleSearchInReport(tblUserSearch, tblRoleSearch, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForAccountEntitySearchInReport(oReportSearchCriteria, true, cmd, cmdParams);
            IDbDataParameter cmdIsKeyAccount = cmd.CreateParameter();
            cmdIsKeyAccount.ParameterName = "@IsKeyAccount";
            cmdIsKeyAccount.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.IsKeyccount);
            cmdParams.Add(cmdIsKeyAccount);

            IDbDataParameter cmdIsMaterialAccount = cmd.CreateParameter();
            cmdIsMaterialAccount.ParameterName = "@IsMaterialAccount";
            cmdIsMaterialAccount.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.IsMaterialAccount);
            cmdParams.Add(cmdIsMaterialAccount);

            IDbDataParameter cmdRiskRatingIDs = cmd.CreateParameter();
            cmdRiskRatingIDs.ParameterName = "@RiskRatingIDs";
            cmdRiskRatingIDs.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.RiskRatingIDs);
            cmdParams.Add(cmdRiskRatingIDs);

            IDbDataParameter cmdReconciliationStatusIDs = cmd.CreateParameter();
            cmdReconciliationStatusIDs.ParameterName = "@ReconciliationStatusIDs";
            cmdReconciliationStatusIDs.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.ReconciliationStatusIDs);
            cmdParams.Add(cmdReconciliationStatusIDs);

            IDbDataParameter cmdKeyAccountAttributeId = cmd.CreateParameter();
            cmdKeyAccountAttributeId.ParameterName = "@KeyAccountAttributeId";
            cmdKeyAccountAttributeId.Value = (short)ARTEnums.AccountAttribute.IsKeyAccount;
            cmdParams.Add(cmdKeyAccountAttributeId);

            IDbDataParameter cmdRiskRatingAttributeId = cmd.CreateParameter();
            cmdRiskRatingAttributeId.ParameterName = "@RiskRatingAttributeId";
            cmdRiskRatingAttributeId.Value = (short)ARTEnums.AccountAttribute.RiskRating;
            cmdParams.Add(cmdRiskRatingAttributeId);

            ServiceHelper.AddCommonLanguageParameters(cmd, cmdParams, oReportSearchCriteria.LCID
                , oReportSearchCriteria.BusinessEntityID, oReportSearchCriteria.DefaultLanguageID);

            IDbDataParameter cmdExcludeNetAccount = cmd.CreateParameter();
            cmdExcludeNetAccount.ParameterName = "@ExcludeNetAccount";
            cmdExcludeNetAccount.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.ExcludeNetAccount);
            cmdParams.Add(cmdExcludeNetAccount);


            IDbDataParameter cmdIsRequesterUserIDToBeConsideredForPermission = cmd.CreateParameter();
            cmdIsRequesterUserIDToBeConsideredForPermission.ParameterName = "@IsRequesterUserIDToBeConsideredForPermission";
            cmdIsRequesterUserIDToBeConsideredForPermission.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.IsRequesterUserIDToBeConsideredForPermission);
            cmdParams.Add(cmdIsRequesterUserIDToBeConsideredForPermission);

            IDbDataParameter paramTaskTypeID = cmd.CreateParameter();
            paramTaskTypeID.ParameterName = "@TaskTypeID";
            paramTaskTypeID.Value = taskType;
            cmdParams.Add(paramTaskTypeID);

            IDbDataParameter paramFilterCriteria = cmd.CreateParameter();
            paramFilterCriteria.ParameterName = "@FilterCriteriaTable";
            paramFilterCriteria.Value = dtTaskFilterCriteria;
            cmdParams.Add(paramFilterCriteria);

        

            return cmd;
        }
    }
}
