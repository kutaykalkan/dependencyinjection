

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
    public class TaskDetailReviewNoteHdrDAO : TaskDetailReviewNoteHdrDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TaskDetailReviewNoteHdrDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public void AddTasksComment(List<long> TaskDetailIDs, string SubjectLine, string Comment, int AddedByUserID, string AddedBy, DateTime DateAdded)
        {
            IDbCommand oCmd = null;
            IDbConnection oConn = null;
            try
            {
                oConn = this.CreateConnection();
                oCmd = CreateAddTasksCommentCommand(TaskDetailIDs, SubjectLine, Comment, AddedByUserID, AddedBy, DateAdded);
                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                oCmd.ExecuteNonQuery();
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
        }

        public void AddTasksCommentWithTransaction(List<long> TaskDetailIDs, string SubjectLine, string Comment, int? AddedByUserID, string AddedBy,
            DateTime DateAdded, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            IDbCommand oCmd = null;
            try
            {
                oCmd = CreateAddTasksCommentCommand(TaskDetailIDs, SubjectLine, Comment, AddedByUserID, AddedBy, DateAdded);

                oCmd.Connection = oConnection;
                oCmd.Transaction = oTransaction;
                oCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        IDbCommand CreateAddTasksCommentCommand(List<long> TaskDetailIDs, string SubjectLine, string Comment, int? AddedByUserID, string AddedBy,
            DateTime DateAdded)
        {
            IDbCommand oCmd = this.CreateCommand("[TaskMaster].[usp_INS_TaskDetailReviewNote]");
            oCmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCmd.Parameters;

            IDbDataParameter param = oCmd.CreateParameter();

            param.ParameterName = "@TaskDetailIDs";
            param.Value = ServiceHelper.ConvertLongIDCollectionToDataTable(TaskDetailIDs);
            cmdParams.Add(param);

            param = oCmd.CreateParameter();
            param.ParameterName = "@SubjectLine";
            param.Value = SubjectLine;
            cmdParams.Add(param);

            param = oCmd.CreateParameter();
            param.ParameterName = "@ReviewNote";
            param.Value = Comment;
            cmdParams.Add(param);

            param = oCmd.CreateParameter();
            param.ParameterName = "@AddedByUserID";
            param.Value = AddedByUserID;
            cmdParams.Add(param);

            param = oCmd.CreateParameter();
            param.ParameterName = "@AddedBy";
            param.Value = AddedBy;
            cmdParams.Add(param);

            param = oCmd.CreateParameter();
            param.ParameterName = "@DateAdded";
            param.Value = DateAdded;
            cmdParams.Add(param);

            return oCmd;
        }

    }
}