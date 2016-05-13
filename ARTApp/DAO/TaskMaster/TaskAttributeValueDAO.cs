

using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using System.Collections.Generic;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.Utility;

namespace SkyStem.ART.App.DAO
{
    public class TaskAttributeValueDAO : TaskAttributeValueDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TaskAttributeValueDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        internal List<TaskAttributeValueInfo> GetTaskAttributeListByTaskID(long? TaskID, int? recPeriodID)
        {
            IDbCommand oCmd = null;
            IDbConnection oConn = null;
            IDataReader reader = null;
            List<TaskAttributeValueInfo> oTaskAttributeValueInfoList = new List<TaskAttributeValueInfo>();
            try
            {
                oConn = this.CreateConnection();
                oCmd = this.GetTaskAttributeListByTaskIDCommand(TaskID, recPeriodID);
                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                reader = oCmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    TaskAttributeValueInfo oTaskAttributeValueInfo = MapTaskAttributeValueInfoObject(reader);
                    oTaskAttributeValueInfoList.Add(oTaskAttributeValueInfo);
                }
                reader.NextResult();
                while (reader.Read())
                {
                    if (oTaskAttributeValueInfoList.Count > 0)
                        MapTaskCommonAttributeObject(reader, oTaskAttributeValueInfoList[0]);
                }


            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
            return oTaskAttributeValueInfoList;
        }
        private IDbCommand GetTaskAttributeListByTaskIDCommand(long? TaskID, int? recPeriodID)
        {
            IDbCommand oCommand = this.CreateCommand("[TaskMaster].[usp_SEL_TaskAttributeByTaskID]");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;

            IDbDataParameter paramTaskID = oCommand.CreateParameter();
            paramTaskID.ParameterName = "@TaskID";
            paramTaskID.Value = TaskID;

            IDbDataParameter paramRecPeriodID = oCommand.CreateParameter();
            paramRecPeriodID.ParameterName = "@RecPeriodID";
            paramRecPeriodID.Value = recPeriodID;

            cmdParams.Add(paramTaskID);
            cmdParams.Add(paramRecPeriodID);
            return oCommand;
        }

        internal TaskAttributeValueInfo MapTaskAttributeValueInfoObject(IDataReader reader)
        {

            TaskAttributeValueInfo oTaskAttributeValueInfo = this.MapObject(reader);
            oTaskAttributeValueInfo.TaskAttributeID = reader.GetInt16Value("TaskAttributeID");
            oTaskAttributeValueInfo.IsTaskCompleted = reader.GetBooleanValue("IsCompleted");
            oTaskAttributeValueInfo.TaskID = reader.GetInt64Value("TaskID");
            return oTaskAttributeValueInfo;
        }
        internal void MapTaskCommonAttributeObject(IDataReader reader, TaskAttributeValueInfo oTaskAttributeValueInfo)
        {
            oTaskAttributeValueInfo.AssigneeUserName = reader.GetStringValue("AssigneeUserName");
            oTaskAttributeValueInfo.ReviewerUserName = reader.GetStringValue("ReviewerUserName");
            oTaskAttributeValueInfo.ApproverUserName = reader.GetStringValue("ApproverUserName");
            oTaskAttributeValueInfo.NotifyUserName = reader.GetStringValue("NotifyUserName");
            oTaskAttributeValueInfo.TaskRecPeriodEndDate = reader.GetDateValue("TaskRecPeriodEndDate");
            oTaskAttributeValueInfo.TaskNumber = reader.GetStringValue("TaskNumber");
            oTaskAttributeValueInfo.TaskAddedBy = reader.GetStringValue("TaskAddedBy");
            oTaskAttributeValueInfo.DateCreated = reader.GetDateValue("DateCreated");
            
        }

        internal List<int> GetRecurrenceFrequencyByTaskID(long? TaskID, int? recPeriodID)
        {
            IDbCommand oCmd = null;
            IDbConnection oConn = null;
            IDataReader reader = null;
            List<int> oRecPeriodIDList = new List<int>();
            try
            {
                oConn = this.CreateConnection();
                oCmd = this.GetRecurrenceFrequencyByTaskIDCommand(TaskID, recPeriodID);
                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                reader = oCmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    int? RecPeriodID = Convert.ToInt32(reader.GetInt64Value("ReferenceID"));
                    if (RecPeriodID.HasValue)
                        oRecPeriodIDList.Add(RecPeriodID.Value);
                }
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
            return oRecPeriodIDList;

        }
        private IDbCommand GetRecurrenceFrequencyByTaskIDCommand(long? TaskID, int? recPeriodID)
        {
            IDbCommand oCommand = this.CreateCommand("[TaskMaster].[usp_SEL_RecurrenceFrequencyByTaskID]");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;
            IDbDataParameter paramTaskID = oCommand.CreateParameter();
            paramTaskID.ParameterName = "@TaskID";
            paramTaskID.Value = TaskID;

            IDbDataParameter paramRecPeriodID = oCommand.CreateParameter();
            paramRecPeriodID.ParameterName = "@RecPeriodID";
            paramRecPeriodID.Value = recPeriodID;

            cmdParams.Add(paramTaskID);
            cmdParams.Add(paramRecPeriodID);
            return oCommand;
        }

        public List<TaskAttributeValueInfo> GetTaskAttributeList(int recPeriodID, List<long> taskIDs, List<short> attributeIDs)
        {
            IDbCommand oCmd = null;
            IDbConnection oConn = null;
            IDataReader reader = null;
            List<TaskAttributeValueInfo> oTaskAttributeValueInfoList = new List<TaskAttributeValueInfo>();
            try
            {
                oConn = this.CreateConnection();
                oCmd = this.CreateCommand("[TaskMaster].[usp_GET_TaskAttribute]");
                oCmd.CommandType = CommandType.StoredProcedure;

                IDataParameterCollection cmdParams = oCmd.Parameters;

                IDbDataParameter paramAttributeIDs = oCmd.CreateParameter();
                paramAttributeIDs.ParameterName = "@RecPeriodID";
                paramAttributeIDs.Value = recPeriodID;

                IDbDataParameter paramAccIDs = oCmd.CreateParameter();
                paramAccIDs.ParameterName = "@TaskList";
                paramAccIDs.Value = ServiceHelper.ConvertLongIDCollectionToDataTable(taskIDs);

                IDbDataParameter paramRecPeriodID = oCmd.CreateParameter();
                paramRecPeriodID.ParameterName = "@AttributeList";
                paramRecPeriodID.Value = ServiceHelper.ConvertShortIDCollectionToDataTable(attributeIDs);

                cmdParams.Add(paramRecPeriodID);
                cmdParams.Add(paramAttributeIDs);
                cmdParams.Add(paramAccIDs);

                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                reader = oCmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    TaskAttributeValueInfo oTaskListinfo = this.MapTaskAttributeValueInfoObject(reader);
                    oTaskAttributeValueInfoList.Add(oTaskListinfo);
                }
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
            return oTaskAttributeValueInfoList;
        }
    }
}