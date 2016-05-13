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
    public class TaskRecurrenceTypeMstDAO : TaskRecurrenceTypeMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TaskRecurrenceTypeMstDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public List<TaskRecurrenceTypeMstInfo> GetTaskRecurrenceTypeMstInfoCollection()
        {
            IDbCommand oCmd = null;
            IDbConnection oConn = null;
            IDataReader reader = null;
            List<TaskRecurrenceTypeMstInfo> oTaskRecurrenceTypeMstInfoList = new List<TaskRecurrenceTypeMstInfo>();
            try
            {
                oConn = this.CreateConnection();
                oCmd = this.GetTaskRecurrenceTypeMstInfoListCommand();
                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                reader = oCmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    TaskRecurrenceTypeMstInfo oTaskRecurrenceTypeMstInfo = MapObject(reader);
                    oTaskRecurrenceTypeMstInfoList.Add(oTaskRecurrenceTypeMstInfo);
                }
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
            return oTaskRecurrenceTypeMstInfoList;
        }

        private IDbCommand GetTaskRecurrenceTypeMstInfoListCommand()
        {
            IDbCommand oCommand = this.CreateCommand("[TaskMaster].[usp_SEL_TaskRecurrenceType]");
            oCommand.CommandType = CommandType.StoredProcedure;
            return oCommand;
        }
    }
}