

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
    public class TaskDetailReviewNoteDetailDAO : TaskDetailReviewNoteDetailDAOBase
    {
         /// <summary>
        /// Constructor
        /// </summary>
        public TaskDetailReviewNoteDetailDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public List<TaskDetailReviewNoteDetailInfo> SelectAllCommentsByTaskDetailID(Int64? TaskDetailID)
        {
            IDbCommand cmd = null;
            IDbConnection con = null;
            List<TaskDetailReviewNoteDetailInfo> oTaskDetailReviewNoteDetailColl = null;
            try
            {
                cmd = this.CreateCommand("TaskMaster.usp_SEL_TaskDetailCommentsByTaskDetailID");
                cmd.CommandType = CommandType.StoredProcedure;
                IDataParameterCollection cmdPrams = cmd.Parameters;

                IDbDataParameter parTaskDetailID = cmd.CreateParameter();
                parTaskDetailID.ParameterName = "@TaskDetailID";
                if (TaskDetailID.HasValue)
                    parTaskDetailID.Value = TaskDetailID;
                else
                    parTaskDetailID.Value = System.DBNull.Value;
                cmdPrams.Add(parTaskDetailID);

                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                oTaskDetailReviewNoteDetailColl = new List<TaskDetailReviewNoteDetailInfo>();
                TaskDetailReviewNoteDetailInfo obj = null;
                while (reader.Read())
                {
                    obj = this.MapObject(reader);
                    obj.AddedByUserFirstName = reader.GetStringValue("AddedByUserFirstName");
                    obj.AddedByUserLastName = reader.GetStringValue("AddedByUserLastName");
                    oTaskDetailReviewNoteDetailColl.Add(obj);
                }
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                }
            }
            return oTaskDetailReviewNoteDetailColl;
        }


    }
}