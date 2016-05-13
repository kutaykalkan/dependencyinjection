

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
    public class TaskDetailDAO : TaskDetailDAOBase
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public TaskDetailDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public static readonly string COLUMN_TASKLISTNAME = "TaskListName";
        public static readonly string COLUMN_CREATEDRECPERIODID = "CreatedRecPeriodID";
        public static readonly string COLUMN_TASKTYPEID = "TaskTypeID";
        public static readonly string COLUMN_ASSIGNEEID = "AssigneeID";
        public static readonly string COLUMN_APPROVERID = "ApproverID";




        protected override TaskDetailInfo MapObject(IDataReader reader)
        {
            TaskDetailInfo oEntity = new TaskDetailInfo();
            int ordinal = 0;
            
            //TaskID
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_TASKID);
                if (!reader.IsDBNull(ordinal))
                    oEntity.TaskID = reader.GetInt64(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            //TaskDetailID
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_TASKDETAILID);
                if (!reader.IsDBNull(ordinal))
                    oEntity.TaskDetailID = reader.GetInt64(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            //RecPeriodID
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_RECPERIODID);
                if (!reader.IsDBNull(ordinal))
                    oEntity.RecPeriodID = reader.GetInt32(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            //CreatedRecPeriodID
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_CREATEDRECPERIODID);
                if (!reader.IsDBNull(ordinal))
                    oEntity.CreatedRecPeriodID = reader.GetInt32(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            //TaskTypeID
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_TASKTYPEID);
                if (!reader.IsDBNull(ordinal))
                    oEntity.TaskTypeID = reader.GetInt16(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            //AssigneeID
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_ASSIGNEEID);
                if (!reader.IsDBNull(ordinal))
                    oEntity.AssigneeID = reader.GetInt32(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            //ApproverID
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_APPROVERID);
                if (!reader.IsDBNull(ordinal))
                    oEntity.ApproverID = reader.GetInt32(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            //AssigneeDueDate
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_ASSIGNEEDUEDATE);
                if (!reader.IsDBNull(ordinal))
                    oEntity.AssigneeDueDate = reader.GetDateTime(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            //ApprovalDueDate
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_APPROVALDUEDATE );
                if (!reader.IsDBNull(ordinal))
                    oEntity.ApprovalDueDate = reader.GetDateTime(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }
            return oEntity;
        }
    }
}