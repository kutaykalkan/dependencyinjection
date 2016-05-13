

using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;

namespace SkyStem.ART.App.DAO
{
    public class TaskCompletionStatusMstDAO : TaskCompletionStatusMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TaskCompletionStatusMstDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public List<TaskCompletionStatusMstInfo> GetTaskCompletionStatusCount(int? UserID, int RoleID, int RecPeriodID)
        {
            IDbCommand oCommand = null;
            IDbConnection oConn = null;
            IDataReader reader = null;
            List<TaskCompletionStatusMstInfo> oTaskCompletionStatusMstInfoCollection = new List<TaskCompletionStatusMstInfo>();
            try
            {
                oConn = this.CreateConnection();

                oCommand = this.CreateCommand("[TaskMaster].[usp_GET_TaskCompletionStatusCount]");
                oCommand.CommandType = CommandType.StoredProcedure;

                IDataParameterCollection cmdParams = oCommand.Parameters;

                IDbDataParameter paramUserID = oCommand.CreateParameter();
                paramUserID.ParameterName = "@UserID";
                if (UserID.HasValue)
                    paramUserID.Value = UserID.Value;
                else
                    paramUserID.Value = DBNull.Value;

                IDbDataParameter paramRoleID = oCommand.CreateParameter();
                paramRoleID.ParameterName = "@RoleID";
                paramRoleID.Value = RoleID;

                IDbDataParameter paramRecPeriodID = oCommand.CreateParameter();
                paramRecPeriodID.ParameterName = "@RecPeriodID";
                paramRecPeriodID.Value = RecPeriodID;

                cmdParams.Add(paramUserID);
                cmdParams.Add(paramRoleID);
                cmdParams.Add(paramRecPeriodID);

                oCommand.Connection = oConn;
                oCommand.Connection.Open();
                reader = oCommand.ExecuteReader(CommandBehavior.CloseConnection);

                TaskCompletionStatusMstInfo oTaskCompletionStatusMstInfo = null;

                while (reader.Read())
                {
                    oTaskCompletionStatusMstInfo = new TaskCompletionStatusMstInfo();
                    oTaskCompletionStatusMstInfo.TaskCompletionStatusID = reader.GetInt16Value("TaskCompletionStatusID");
                    oTaskCompletionStatusMstInfo.CompletionStatusCount = reader.GetInt32Value("CompletionStatusCount");
                    oTaskCompletionStatusMstInfo.StatusColor = reader.GetStringValue("StatusColor");
                    oTaskCompletionStatusMstInfo.ATCount = reader.GetInt32Value("ATCount");
                    oTaskCompletionStatusMstInfo.GTCount = reader.GetInt32Value("GTCount");
                    oTaskCompletionStatusMstInfoCollection.Add(oTaskCompletionStatusMstInfo);
                }
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
            return oTaskCompletionStatusMstInfoCollection;
        }
        public List<TaskStatusCountInfo> GetTaskStatusCountByMonth(int? UserID, int? RoleID, int? RecPeriodID, DateTime CurrentDate)
        {
            IDbCommand oCommand = null;
            IDbConnection oConn = null;
            IDataReader reader = null;
            List<TaskStatusCountInfo> oTaskStatusCountInfoCollection = new List<TaskStatusCountInfo>();
            try
            {
                oConn = this.CreateConnection();

                oCommand = this.CreateCommand("[TaskMaster].[usp_GET_TaskStatusCountByMonth]");
                oCommand.CommandType = CommandType.StoredProcedure;

                IDataParameterCollection cmdParams = oCommand.Parameters;

                IDbDataParameter paramUserID = oCommand.CreateParameter();
                paramUserID.ParameterName = "@UserID";
                if (UserID.HasValue)
                    paramUserID.Value = UserID.Value;
                else
                    paramUserID.Value = DBNull.Value;

                IDbDataParameter paramRoleID = oCommand.CreateParameter();
                paramRoleID.ParameterName = "@RoleID";
                if (RoleID.HasValue)
                    paramRoleID.Value = RoleID.Value;
                else
                    paramUserID.Value = DBNull.Value;

                IDbDataParameter paramRecPeriodID = oCommand.CreateParameter();
                paramRecPeriodID.ParameterName = "@RecPeriodID";
                if (RecPeriodID.HasValue)
                    paramRecPeriodID.Value = RecPeriodID.Value;
                else
                    paramUserID.Value = DBNull.Value;


                IDbDataParameter paramCurrentDate = oCommand.CreateParameter();
                paramCurrentDate.ParameterName = "@CurrentDate";
                paramCurrentDate.Value = CurrentDate;

                cmdParams.Add(paramUserID);
                cmdParams.Add(paramRoleID);
                cmdParams.Add(paramRecPeriodID);
                cmdParams.Add(paramCurrentDate);

                oCommand.Connection = oConn;
                oCommand.Connection.Open();
                reader = oCommand.ExecuteReader(CommandBehavior.CloseConnection);

                TaskStatusCountInfo oTaskStatusCountInfo = null;

                while (reader.Read())
                {
                    oTaskStatusCountInfo = new TaskStatusCountInfo();
                    oTaskStatusCountInfo.MonthName = reader.GetStringValue("MonthName");
                    oTaskStatusCountInfo.TotalCount = reader.GetInt32Value("Total");
                    oTaskStatusCountInfo.Pending = reader.GetInt32Value("Pending");
                    oTaskStatusCountInfo.Overdue = reader.GetInt32Value("Overdue");
                    oTaskStatusCountInfo.Completed = reader.GetInt32Value("Completed");
                    oTaskStatusCountInfo.MonthNumber = reader.GetInt16Value("MonthNumber");
                    oTaskStatusCountInfo.TaskTypeID = reader.GetInt16Value("TaskTypeID");
                    oTaskStatusCountInfo.MonthNameLabelID = reader.GetInt32Value("MonthNameLabelID");
                    oTaskStatusCountInfo.MonthStartDate = reader.GetDateValue("MonthStartDate");
                    oTaskStatusCountInfo.MonthEndDate = reader.GetDateValue("MonthEndDate");
                    oTaskStatusCountInfo.Year = reader.GetStringValue("Year");
                    oTaskStatusCountInfoCollection.Add(oTaskStatusCountInfo);
                }
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
            return oTaskStatusCountInfoCollection;
        }
    }
}