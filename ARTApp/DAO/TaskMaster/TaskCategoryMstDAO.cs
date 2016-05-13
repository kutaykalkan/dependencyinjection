

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
    public class TaskCategoryMstDAO : TaskCategoryMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TaskCategoryMstDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public List<TaskCategoryMstInfo> GetTaskCategoryMstInfoCollection()
        {
            IDbCommand oCmd = null;
            IDbConnection oConn = null;
            IDataReader reader = null;
            List<TaskCategoryMstInfo> oTaskCategoryTypeMstInfoList = new List<TaskCategoryMstInfo>();
            try
            {
                oConn = this.CreateConnection();
                oCmd = this.GetTaskCategoryMstInfoListCommand();
                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                reader = oCmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    TaskCategoryMstInfo oTaskCategoryMstInfo = MapObject(reader);
                    oTaskCategoryTypeMstInfoList.Add(oTaskCategoryMstInfo);
                }
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
            return oTaskCategoryTypeMstInfoList;
        }

        private IDbCommand GetTaskCategoryMstInfoListCommand()
        {
            IDbCommand oCommand = this.CreateCommand("[TaskMaster].[usp_SEL_TaskCategory]");
            oCommand.CommandType = CommandType.StoredProcedure;
            return oCommand;
        }
    }
}