

using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.App.Utility;

namespace SkyStem.ART.App.DAO
{
    public class TaskStatusMstDAO : TaskStatusMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TaskStatusMstDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public List<TaskActionTypeTaskSatusInfo> GetTaskStatusByTaskActionTypeID(short TaskActionTypeID)
        {
            List<TaskActionTypeTaskSatusInfo> TaskActionTypeTaskSatusInfoList = null;
            IDbCommand oCmd = null;
            IDbConnection oConn = null;
            IDataReader reader = null;
            try
            {

                oConn = this.CreateConnection();
                oCmd = this.CreateCommand("[TaskMaster].[usp_SEL_TaskSatusByTaskActionType]");
                oCmd.CommandType = CommandType.StoredProcedure;

                IDataParameterCollection cmdParams = oCmd.Parameters;

                IDbDataParameter paramUserID = oCmd.CreateParameter();
                paramUserID.ParameterName = "@TaskActionTypeID";
                paramUserID.Value = TaskActionTypeID;

                oCmd.Connection.Open();
                reader = oCmd.ExecuteReader(CommandBehavior.CloseConnection);

                TaskActionTypeTaskSatusInfoList = new List<TaskActionTypeTaskSatusInfo>();
                TaskActionTypeTaskSatusInfo oTaskActionTypeTaskSatusInfo = null;
                while (reader.Read())
                {
                    oTaskActionTypeTaskSatusInfo = this.MapTaskActionTypeTaskSatusInfo(reader);
                    TaskActionTypeTaskSatusInfoList.Add(oTaskActionTypeTaskSatusInfo);
                }

            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
            return TaskActionTypeTaskSatusInfoList;
        }

        TaskActionTypeTaskSatusInfo MapTaskActionTypeTaskSatusInfo(IDataReader IDataReader)
        {
            TaskActionTypeTaskSatusInfo oTaskActionTypeTaskSatusInfo = new TaskActionTypeTaskSatusInfo();
            oTaskActionTypeTaskSatusInfo.TaskActionTypeTaskStatusID = IDataReader.GetInt16Value("TaskActionTypeTaskStatusID");
            oTaskActionTypeTaskSatusInfo.TaskActionTypeID = IDataReader.GetInt16Value("TaskActionTypeID");
            oTaskActionTypeTaskSatusInfo.TaskStatusID = IDataReader.GetInt16Value("TaskStatusID");
            return oTaskActionTypeTaskSatusInfo;
        }

        public List<TaskStatusMstInfo> GetTaskStatus()
        {
            IDbConnection oCon = null;
            IDbCommand oCmd = null;
            IDataReader reader = null;
            try
            {
                oCon = this.CreateConnection();
                oCmd = GetTaskStatusCommand();
                oCmd.Connection = oCon;
                oCon.Open();
                reader = oCmd.ExecuteReader(CommandBehavior.CloseConnection);
                return this.MapObjects(reader);

            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                if (oCon != null && oCon.State == ConnectionState.Open)
                    oCon.Close();
            }
        }

        private IDbCommand GetTaskStatusCommand()
        {
            IDbCommand oCmd = this.CreateCommand("[TaskMaster].[usp_SEL_TaskStatus]");
            oCmd.CommandType = CommandType.StoredProcedure;

            return oCmd;
        }

    }
}