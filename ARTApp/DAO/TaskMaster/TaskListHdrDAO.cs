

using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.Client.Data;

namespace SkyStem.ART.App.DAO
{
    public class TaskListHdrDAO : TaskListHdrDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TaskListHdrDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public List<TaskListHdrInfo> GetTaskListHdrInfoCollection(int recPeriodID)
        {
            IDbCommand oCmd = null;
            IDbConnection oConn = null;
            IDataReader reader = null;
            List<TaskListHdrInfo> oTaskListHdrInfoList = new List<TaskListHdrInfo>();
            try
            {
                oConn = this.CreateConnection();
                oCmd = this.GetTaskListHdrInfoCommand(recPeriodID);
                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                reader = oCmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    TaskListHdrInfo oTaskListHdrInfo = MapObject(reader);
                    oTaskListHdrInfoList.Add(oTaskListHdrInfo);

                }
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
            return oTaskListHdrInfoList;
        }

        private IDbCommand GetTaskListHdrInfoCommand(int recPeriodID)
        {
            IDbCommand oCommand = this.CreateCommand("[TaskMaster].[usp_SEL_TaskListHdr]");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;

            IDbDataParameter paramRecPeriodID = oCommand.CreateParameter();
            paramRecPeriodID.ParameterName = "@RecPeriodID";
            paramRecPeriodID.Value = recPeriodID;
            cmdParams.Add(paramRecPeriodID);

            return oCommand;
        }

        public List<TaskSubListHdrInfo> GetTaskSubListHdrInfoCollection(int recPeriodID)
        {
            IDbCommand oCmd = null;
            IDbConnection oConn = null;
            IDataReader reader = null;
            List<TaskSubListHdrInfo> oTaskSubListHdrInfoList = new List<TaskSubListHdrInfo>();
            try
            {
                oConn = this.CreateConnection();
                oCmd = this.GetTaskSubListHdrInfoCommand(recPeriodID);
                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                reader = oCmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    TaskSubListHdrInfo oTaskSubListHdrInfo = MapTaskSubListHdrInfoObject(reader);
                    oTaskSubListHdrInfoList.Add(oTaskSubListHdrInfo);
                }
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
            return oTaskSubListHdrInfoList;
        }

        private IDbCommand GetTaskSubListHdrInfoCommand(int recPeriodID)
        {
            IDbCommand oCommand = this.CreateCommand("[TaskMaster].[usp_SEL_TaskSubListHdr]");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;

            IDbDataParameter paramRecPeriodID = oCommand.CreateParameter();
            paramRecPeriodID.ParameterName = "@RecPeriodID";
            paramRecPeriodID.Value = recPeriodID;
            cmdParams.Add(paramRecPeriodID);

            return oCommand;
        }

        /// <summary>
        /// Maps the IDataReader values to a TaskListHdrInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>TaskListHdrInfo</returns>
        protected TaskSubListHdrInfo MapTaskSubListHdrInfoObject(System.Data.IDataReader r)
        {
            TaskSubListHdrInfo entity = new TaskSubListHdrInfo();
            entity.TaskSubListID = r.GetInt32Value("TaskSubListID");
            entity.TaskSubListName = r.GetStringValue("TaskSubListName");
            entity.IsActive = r.GetBooleanValue("IsActive");
            return entity;
        }

        #region Edit Task List
        public int GetTaskListIDByName(string TaskListName, int? CompanyID)
        {
            int TaskListID;
            IDbCommand oCmd = null;
            IDbConnection oConn = null;
            try
            {
                oConn = this.CreateConnection();
                oCmd = this.GetTaskListIDByNameCommand(TaskListName, CompanyID);
                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                TaskListID = (int)oCmd.ExecuteScalar();
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
            return TaskListID;
        }

        private IDbCommand GetTaskListIDByNameCommand(string TaskListName, int? CompanyID)
        {
            IDbCommand oCommand = this.CreateCommand("[TaskMaster].[usp_GET_TaskListIDByName]");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;

            IDbDataParameter paramTaskListName = oCommand.CreateParameter();
            paramTaskListName.ParameterName = "@TaskListName";
            paramTaskListName.Value = TaskListName;
            cmdParams.Add(paramTaskListName);

            IDbDataParameter paramCompanyID = oCommand.CreateParameter();
            paramCompanyID.ParameterName = "@CompanyID";
            if (CompanyID.HasValue)
                paramCompanyID.Value = CompanyID.Value;
            else
                paramCompanyID.Value = DBNull.Value;
           
            cmdParams.Add(paramCompanyID);


            return oCommand;
        }
        
        public bool EditTaskList(int? TasksListID, string TasksListName, string revisedBy, DateTime dateRevised)
        {
            bool isUpdated = false;
            IDbCommand oCmd = null;
            IDbConnection oConn = null;
            IDbTransaction oTrans = null;
            try
            {
                oConn = this.CreateConnection();
                oCmd = this.GetEditTaskListCommand(TasksListID, TasksListName, revisedBy, dateRevised);
                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                oTrans = oConn.BeginTransaction();
                oCmd.Transaction = oTrans;
                oCmd.ExecuteNonQuery();
                oTrans.Commit();
                oTrans.Dispose();
                isUpdated = true;
            }
            catch (Exception ex)
            {
                if (oTrans != null && oConn != null)
                {
                    oTrans.Rollback();
                    oTrans.Dispose();
                }
                throw ex;
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
            return isUpdated;
        }
        private IDbCommand GetEditTaskListCommand(int? TasksListID, string TasksListName, string revisedBy, DateTime dateRevised)
        {
            IDbCommand oCommand = this.CreateCommand("[TaskMaster].[usp_UPD_TaskListHdr]");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;


            IDbDataParameter paramTasksListID = oCommand.CreateParameter();
            paramTasksListID.ParameterName = "@TasksListID";
            paramTasksListID.Value = TasksListID;

            IDbDataParameter paramTasksListName = oCommand.CreateParameter();
            paramTasksListName.ParameterName = "@TasksListName";
            paramTasksListName.Value = TasksListName;

            IDbDataParameter paramReviseBy = oCommand.CreateParameter();
            paramReviseBy.ParameterName = "@ReviseBy";
            paramReviseBy.Value = revisedBy;

            IDbDataParameter paramDateRevised = oCommand.CreateParameter();
            paramDateRevised.ParameterName = "@DateRevised";
            paramDateRevised.Value = dateRevised;

            cmdParams.Add(paramTasksListID);
            cmdParams.Add(paramTasksListName);
            cmdParams.Add(paramReviseBy);
            cmdParams.Add(paramDateRevised);

            return oCommand;
        }
        # endregion

        #region Edit Task SubList
        public bool EditTaskSubList(int? TaskSubListID, string TaskSubListName, string revisedBy, DateTime dateRevised)
        {
            bool isUpdated = false;
            IDbCommand oCmd = null;
            IDbConnection oConn = null;
            IDbTransaction oTrans = null;
            try
            {
                oConn = this.CreateConnection();
                oCmd = this.GetEditTaskSubListCommand(TaskSubListID, TaskSubListName, revisedBy, dateRevised);
                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                oTrans = oConn.BeginTransaction();
                oCmd.Transaction = oTrans;
                oCmd.ExecuteNonQuery();
                oTrans.Commit();
                oTrans.Dispose();
                isUpdated = true;
            }
            catch (Exception ex)
            {
                if (oTrans != null && oConn != null)
                {
                    oTrans.Rollback();
                    oTrans.Dispose();
                }
                throw ex;
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
            return isUpdated;
        }
        private IDbCommand GetEditTaskSubListCommand(int? TaskSubListID, string TaskSubListName, string revisedBy, DateTime dateRevised)
        {
            IDbCommand oCommand = this.CreateCommand("[TaskMaster].[usp_UPD_TaskSubListHdr]");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;


            IDbDataParameter paramTaskSubListID = oCommand.CreateParameter();
            paramTaskSubListID.ParameterName = "@TasksSubListID";
            if (TaskSubListID.HasValue)
                paramTaskSubListID.Value = TaskSubListID.Value;
            else
                paramTaskSubListID.Value = DBNull.Value;

            IDbDataParameter paramTaskSubListName = oCommand.CreateParameter();
            paramTaskSubListName.ParameterName = "@TasksSubListName";
            if (TaskSubListName != null)
                paramTaskSubListName.Value = TaskSubListName;
            else
                paramTaskSubListName.Value = DBNull.Value;

            IDbDataParameter paramReviseBy = oCommand.CreateParameter();
            paramReviseBy.ParameterName = "@ReviseBy";
            paramReviseBy.Value = revisedBy;

            IDbDataParameter paramDateRevised = oCommand.CreateParameter();
            paramDateRevised.ParameterName = "@DateRevised";
            paramDateRevised.Value = dateRevised;

            cmdParams.Add(paramTaskSubListID);
            cmdParams.Add(paramTaskSubListName);
            cmdParams.Add(paramReviseBy);
            cmdParams.Add(paramDateRevised);

            return oCommand;
        }
        public int GetTaskSubListIDByName(string TaskSubListName, int? CompanyID)
        {
            int TaskSubListID;
            IDbCommand oCmd = null;
            IDbConnection oConn = null;
            try
            {
                oConn = this.CreateConnection();
                oCmd = this.GetTaskSubListIDByNameCommand(TaskSubListName, CompanyID);
                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                TaskSubListID = (int)oCmd.ExecuteScalar();
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
            return TaskSubListID;
        }

        private IDbCommand GetTaskSubListIDByNameCommand(string TaskSubListName, int? CompanyID)
        {
            IDbCommand oCommand = this.CreateCommand("[TaskMaster].[usp_GET_TaskSubListIDByName]");
            oCommand.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = oCommand.Parameters;
            IDbDataParameter paramTaskSubListName = oCommand.CreateParameter();
            paramTaskSubListName.ParameterName = "@TaskSubListName";
            if (TaskSubListName != null)
                paramTaskSubListName.Value = TaskSubListName;
            else
                paramTaskSubListName.Value = DBNull.Value;
            cmdParams.Add(paramTaskSubListName);

            IDbDataParameter paramCompanyID = oCommand.CreateParameter();
            paramCompanyID.ParameterName = "@CompanyID";
            if (CompanyID.HasValue)
                paramCompanyID.Value = CompanyID.Value;
            else
                paramCompanyID.Value = DBNull.Value;

            cmdParams.Add(paramCompanyID);

            return oCommand;
        }
        # endregion
    }
}