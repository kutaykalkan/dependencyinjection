

using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using System.Collections.Generic;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Data;

namespace SkyStem.ART.App.DAO
{
    public class TaskDetailStatusDAO : TaskDetailStatusDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TaskDetailStatusDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public void AddTaskDetailStatus(List<long> TaskDetailIDs, SkyStem.ART.Client.Data.ARTEnums.TaskStatus TaskStatus, DateTime TaskStatusDate, int? AddedByUserID)
        {
            IDbCommand cmd = null;
            IDbConnection con = null;
            try
            {
                cmd = this.CreateCommand("TaskMaster.usp_SEL_TaskDetailCommentsByTaskDetailID");
                cmd.CommandType = CommandType.StoredProcedure;
                IDataParameterCollection cmdPrams = cmd.Parameters;

                IDbDataParameter parTaskDetailIDStatusID = cmd.CreateParameter();
                parTaskDetailIDStatusID.ParameterName = "@TaskDetailIDStatusID";
                if (TaskDetailIDs != null)
                    parTaskDetailIDStatusID.Value = ServiceHelper.ConvertLongIDCollectionShortToDataTable(TaskDetailIDs, (short)TaskStatus);
                else
                    parTaskDetailIDStatusID.Value = System.DBNull.Value;

                cmdPrams.Add(parTaskDetailIDStatusID);

                IDbDataParameter parTaskStatusDate = cmd.CreateParameter();
                parTaskStatusDate.ParameterName = "@TaskStatusDate";
                parTaskStatusDate.Value = TaskStatusDate;
                cmdPrams.Add(parTaskStatusDate);

                IDbDataParameter parAddedByUserID = cmd.CreateParameter();
                parAddedByUserID.ParameterName = "@AddedByUserID";
                parAddedByUserID.Value = AddedByUserID;
                cmdPrams.Add(parAddedByUserID);

                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();

                cmd.ExecuteNonQuery();

            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }

        public void UpdateTasksStatus(List<long> TaskDetailIDs, SkyStem.ART.Client.Data.ARTEnums.TaskActionType TaskAcion, int? RecPeriodID, string RevisedByUser,
            DateTime DateRevised, int? UserID, short? RoleID)
        {
            IDbCommand cmd = null;
            IDbConnection con = null;
            try
            {
                cmd = CreateUpdateTaskStatusCommand(TaskDetailIDs, TaskAcion, RecPeriodID, RevisedByUser, DateRevised, UserID, RoleID);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public void UpdateTasksStatusWithTransaction(List<long> TaskDetailIDs, SkyStem.ART.Client.Data.ARTEnums.TaskActionType TaskAcion, int? RecPeriodID, string RevisedByUser,
            DateTime DateRevised, int? UserID, short? RoleID, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            IDbCommand cmd = null;
            try
            {
                cmd = CreateUpdateTaskStatusCommand(TaskDetailIDs, TaskAcion, RecPeriodID, RevisedByUser, DateRevised, UserID, RoleID);
                cmd.Connection = oConnection;
                cmd.Transaction = oTransaction;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        IDbCommand CreateUpdateTaskStatusCommand(List<long> TaskDetailIDs, ARTEnums.TaskActionType TaskAcion, int? RecPeriodID, string RevisedByUser,
            DateTime DateRevised, int? UserID, short? RoleID)
        {
            IDbCommand cmd = this.CreateCommand("TaskMaster.usp_UPD_TaskStatus");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdPrams = cmd.Parameters;

            IDbDataParameter parTaskDetailIDs = cmd.CreateParameter();
            parTaskDetailIDs.ParameterName = "@TaskDetailIDs";
            if (TaskDetailIDs != null)
                parTaskDetailIDs.Value = ServiceHelper.ConvertLongIDCollectionToDataTable(TaskDetailIDs);
            else
                parTaskDetailIDs.Value = System.DBNull.Value;

            cmdPrams.Add(parTaskDetailIDs);

            ServiceHelper.AddCommonUserAndRoleParameters(UserID, RoleID, cmd, cmdPrams);

            IDbDataParameter parActionTypeID = cmd.CreateParameter();
            parActionTypeID.ParameterName = "@ActionTypeID";
            parActionTypeID.Value = (short)TaskAcion;
            cmdPrams.Add(parActionTypeID);

            IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            parRecPeriodID.Value = RecPeriodID;
            cmdPrams.Add(parRecPeriodID);

            IDbDataParameter parRevisedByUser = cmd.CreateParameter();
            parRevisedByUser.ParameterName = "@RevisedBy";
            parRevisedByUser.Value = RevisedByUser;
            cmdPrams.Add(parRevisedByUser);

            IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            parDateRevised.Value = DateRevised;
            cmdPrams.Add(parDateRevised);

            return cmd;

        }

    }
}