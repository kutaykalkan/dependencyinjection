

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
    public class TaskTypeMstDAO : TaskTypeMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TaskTypeMstDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        public List<TaskTypeMstInfo> GetAllTaskType()
        {
            List<TaskTypeMstInfo> oTaskTypeMstInfoList = new List<TaskTypeMstInfo>();
            using(IDbConnection oConn = this.CreateConnection())
            {
                using (IDbCommand oCmd = this.GetAllTaskTypeCommand())
                {
                    oConn.Open();
                    oCmd.Connection = oConn;
                    IDataReader dr = oCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (dr.Read())
                    {
                        TaskTypeMstInfo oTaskTypeMstInfo = MapObject(dr);
                        oTaskTypeMstInfoList.Add(oTaskTypeMstInfo);

                    }
                }
            }
            return oTaskTypeMstInfoList;
        }

        private IDbCommand GetAllTaskTypeCommand()
        {
            IDbCommand oCommand = this.CreateCommand("[TaskMaster].[usp_SEL_AllTaskType]");
            oCommand.CommandType = CommandType.StoredProcedure;
            return oCommand;
        }
    }
}