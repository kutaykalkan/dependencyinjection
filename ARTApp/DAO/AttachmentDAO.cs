


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
    public class AttachmentDAO : AttachmentDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AttachmentDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        public List<AttachmentInfo> GetAttachmentByRecordIDandRecordTypeID(long RecordID, int RecordTypeID, int? RecPeriodID)
        {
            AttachmentInfo objAttachmentInfo = null;
            //IDbTransaction trans = null;
            List<AttachmentInfo> objAttachmentInfocollection = new List<AttachmentInfo>();
            using (IDbConnection conn = CreateConnection())
            {
                conn.Open();
                //trans = conn.BeginTransaction();
                IDbCommand cmd;
                cmd = CreateAttachmentByRecordIDandRecordTypeIDCommand(RecordID, RecordTypeID, RecPeriodID);
                cmd.Connection = conn;
                //cmd.Transaction = trans;
                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    objAttachmentInfo = (AttachmentInfo)MapObject(reader);
                    objAttachmentInfocollection.Add(objAttachmentInfo);
                }
                reader.Close();
                //if (objUserHdrInfo == null)
                //{
                //    trans.Rollback();
                //}
                //else
                //    if (trans.Connection != null)
                //    {
                //        trans.Commit();
                //    }
            }
            return objAttachmentInfocollection;
        }

        protected IDbCommand CreateAttachmentByRecordIDandRecordTypeIDCommand(long RecordID, int RecordTypeID, int? RecPeriodID)
        {

            IDbCommand cmd = this.CreateCommand("usp_SEL_Attachment");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter parRecordID = cmd.CreateParameter();
            parRecordID.ParameterName = "@RecordID";
            parRecordID.Value = RecordID;
            cmdParams.Add(parRecordID);

            IDbDataParameter parRecordTypeID = cmd.CreateParameter();
            parRecordTypeID.ParameterName = "@RecordTypeID";
            parRecordTypeID.Value = RecordTypeID;
            cmdParams.Add(parRecordTypeID);

            IDbDataParameter parRecPeriodIDD = cmd.CreateParameter();
            parRecPeriodIDD.ParameterName = "@RecPeriodID";
            if (RecPeriodID.HasValue)
                parRecPeriodIDD.Value = RecPeriodID.Value;
            else
                parRecPeriodIDD.Value = DBNull.Value;
            cmdParams.Add(parRecPeriodIDD);



            return cmd;
        }


        public void InsertAttachmentBulk(DataTable tblAttachment, Int32? recPeriodID, string addedBy, DateTime? dateAdded, IDbConnection con, IDbTransaction oTransaction)
        {
            IDbCommand cmd = null;
            bool isConnectionNull = (con == null);

            try
            {
                cmd = CreateInsertAttachmentBulkCommand(tblAttachment, recPeriodID, addedBy, dateAdded, cmd);

                if (isConnectionNull)
                {
                    con = this.CreateConnection();
                    con.Open();
                }
                else
                {
                    cmd.Transaction = oTransaction;
                }

                cmd.Connection = con;

                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (isConnectionNull && con != null && con.State == ConnectionState.Open)
                    con.Dispose();
            }
        }

        private IDbCommand CreateInsertAttachmentBulkCommand(DataTable tblAttachment, Int32? recPeriodID, string addedBy, DateTime? dateAdded, IDbCommand cmd)
        {
            cmd = this.CreateCommand("usp_INS_AttachmentBulk");

            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter paramAttachmentTableType = cmd.CreateParameter();
            paramAttachmentTableType.ParameterName = "@AttachmentTableType";
            paramAttachmentTableType.Value = tblAttachment;
            cmdParams.Add(paramAttachmentTableType);

            ServiceHelper.AddCommonParametersForRecPeriodID(recPeriodID, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForAddedBy(addedBy, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForDateAdded(dateAdded, cmd, cmdParams);
            return cmd;
        }


        public void DeleteAttachmentByID(long AttachmentID, IDbConnection con)
        {
            IDbCommand cmd = null;
            try
            {
                cmd = CreateDeleteAttachmentByIDCommand(AttachmentID);
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Dispose();
            }
        }


        protected IDbCommand CreateDeleteAttachmentByIDCommand(long AttachmentID)
        {

            IDbCommand cmd = this.CreateCommand("usp_DEL_Attachment");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter parAttachmentID = cmd.CreateParameter();
            parAttachmentID.ParameterName = "@AttachmentID";
            parAttachmentID.Value = AttachmentID;
            cmdParams.Add(parAttachmentID);
            return cmd;
        }

        public List<AttachmentInfo> GetTaskAttachment(List<TaskHdrInfo> oTaskHdrinfoList)
        {
            IDbCommand oCmd = null;
            IDbConnection oConn = null;
            IDataReader reader = null;
            List<AttachmentInfo> attachmentInfoList = null;
            try
            {
                oConn = this.CreateConnection();
                DataTable dtRecord = ServiceHelper.ConvertTaskHdrInfoToRecordDataTable(oTaskHdrinfoList);
                oCmd = this.GetTaskAttachmentCommand(dtRecord);
                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                reader = oCmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    if (attachmentInfoList == null)
                        attachmentInfoList = new List<AttachmentInfo>();

                    attachmentInfoList.Add(MapObject(reader));
                }
            }
            finally
            {
                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
            return attachmentInfoList;
        }

        private IDbCommand GetTaskAttachmentCommand(DataTable dtTask)
        {
            IDbCommand oCommand = this.CreateCommand("[TaskMaster].[usp_SEL_TaskAttachment]");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;

            IDbDataParameter paramTaskDataTable = oCommand.CreateParameter();
            paramTaskDataTable.ParameterName = "@udtRecordID";
            paramTaskDataTable.Value = dtTask;

            cmdParams.Add(paramTaskDataTable);
            return oCommand;
        }


        public int? DeleteAttachmentAndGetFileRefrenceCount(long AttachmentID)
        {
            IDbCommand cmd = null;
            IDbConnection con = null;
            int? FileReferenceCount = null;
            try
            {
                con = this.CreateConnection();

                cmd = cmd = this.CreateCommand("usp_DEL_AttachmentAndGetFileRefrenceCount");
                cmd.CommandType = CommandType.StoredProcedure;
                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter parAttachmentID = cmd.CreateParameter();
                parAttachmentID.ParameterName = "@AttachmentID";
                parAttachmentID.Value = AttachmentID;
                cmdParams.Add(parAttachmentID);

                IDbDataParameter parFileReferenceCount = cmd.CreateParameter();
                parFileReferenceCount.ParameterName = "@FileRefrenceCount";
                parFileReferenceCount.Direction = ParameterDirection.Output;
                parFileReferenceCount.Value = System.DBNull.Value;
                parFileReferenceCount.Size = 50;
                cmdParams.Add(parFileReferenceCount);

                con.Open();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                FileReferenceCount = Int32.Parse(((IDbDataParameter)cmd.Parameters["@FileRefrenceCount"]).Value.ToString());
                return FileReferenceCount;

            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Dispose();
            }
        }
        public List<AttachmentInfo> GetAllAttachmentForGL(long? GLDataID, int? UserID, short? RoleID)
        {

            AttachmentInfo objAttachmentInfo = null;
            List<AttachmentInfo> objAttachmentInfocollection = new List<AttachmentInfo>();
            using (IDbConnection conn = CreateConnection())
            {
                conn.Open();
                using (IDbCommand cmd = CreateGetAllAttachmentForGLCommand(GLDataID, UserID, RoleID))
                {
                    cmd.Connection = conn;
                    IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        objAttachmentInfo = (AttachmentInfo)MapObject(reader);
                        objAttachmentInfocollection.Add(objAttachmentInfo);
                    }
                    reader.Close();
                }
            }
            return objAttachmentInfocollection;
        }

        protected IDbCommand CreateGetAllAttachmentForGLCommand(long? GLDataID, int? UserID, short? RoleID)
        {

            IDbCommand cmd = this.CreateCommand("usp_SEL_AllAttachmentByGLDataID");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            if (GLDataID.HasValue)
                parGLDataID.Value = GLDataID.Value;
            else
                parGLDataID.Value = DBNull.Value;
            cmdParams.Add(parGLDataID);

            IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            if (UserID.HasValue)
                parUserID.Value = UserID.Value;
            else
                parUserID.Value = DBNull.Value;
            cmdParams.Add(parUserID);

            IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            if (RoleID.HasValue)
                parRoleID.Value = RoleID.Value;
            else
                parRoleID.Value = DBNull.Value;
            cmdParams.Add(parRoleID);
            return cmd;
        }
        public List<AttachmentInfo> GetAllAttachmentForTask(long? taskID, short? taskTypeID, int? recPeriodID, int? UserID, short? RoleID)
        {

            AttachmentInfo objAttachmentInfo = null;
            List<AttachmentInfo> objAttachmentInfocollection = new List<AttachmentInfo>();
            using (IDbConnection conn = CreateConnection())
            {
                conn.Open();
                using (IDbCommand cmd = CreateGetAllAttachmentForTaskCommand(taskID, taskTypeID, recPeriodID, UserID, RoleID))
                {
                    cmd.Connection = conn;
                    IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        objAttachmentInfo = (AttachmentInfo)MapObject(reader);
                        objAttachmentInfocollection.Add(objAttachmentInfo);
                    }
                    reader.Close();
                }
            }
            return objAttachmentInfocollection;
        }
        protected IDbCommand CreateGetAllAttachmentForTaskCommand(long? taskID, short? taskTypeID, int? recPeriodID, int? UserID, short? RoleID)
        {

            IDbCommand cmd = this.CreateCommand("usp_SEL_AllAttachmentByTaskID");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter parTaskID = cmd.CreateParameter();
            parTaskID.ParameterName = "@TaskID";
            if (taskID.HasValue)
                parTaskID.Value = taskID.Value;
            else
                parTaskID.Value = DBNull.Value;
            cmdParams.Add(parTaskID);

            IDbDataParameter parTaskTypeID = cmd.CreateParameter();
            parTaskTypeID.ParameterName = "@TaskTypeID";
            if (taskTypeID.HasValue)
                parTaskTypeID.Value = taskTypeID.Value;
            else
                parTaskTypeID.Value = DBNull.Value;
            cmdParams.Add(parTaskTypeID);

            IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            if (recPeriodID.HasValue)
                parRecPeriodID.Value = recPeriodID.Value;
            else
                parRecPeriodID.Value = DBNull.Value;
            cmdParams.Add(parRecPeriodID);

            IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            if (UserID.HasValue)
                parUserID.Value = UserID.Value;
            else
                parUserID.Value = DBNull.Value;
            cmdParams.Add(parUserID);

            IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            if (RoleID.HasValue)
                parRoleID.Value = RoleID.Value;
            else
                parRoleID.Value = DBNull.Value;
            cmdParams.Add(parRoleID);
            return cmd;
        }
    }
}