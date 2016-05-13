


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
    public class RecItemCommentDAO : RecItemCommentDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RecItemCommentDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public List<RecItemCommentInfo> GetRecItemCommentList(long? RecordID, short? RecordTypeID)
        {
            List<RecItemCommentInfo> oRecItemCommentInfoCollection = new List<RecItemCommentInfo>();
            IDbCommand cmd = null;
            IDbConnection con = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateGetRecItemCommentListCommand(RecordID, RecordTypeID);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                RecItemCommentInfo oRecItemCommentInfo = null;
                while (reader.Read())
                {
                    oRecItemCommentInfo = this.MapObject(reader);
                    oRecItemCommentInfoCollection.Add(oRecItemCommentInfo);
                }
            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed)
                    con.Dispose();
            }

            return oRecItemCommentInfoCollection;
        }

        private IDbCommand CreateGetRecItemCommentListCommand(long? RecordID, short? RecordTypeID)
        {
            IDbCommand cmd = this.CreateCommand("[dbo].[usp_SEL_RecItemComments]");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection paramCollection = cmd.Parameters;

            IDbDataParameter paramRecordID = cmd.CreateParameter();
            paramRecordID.ParameterName = "@RecordID";
            paramRecordID.Value = RecordID;
            paramCollection.Add(paramRecordID);

            IDbDataParameter paramRecordTypeID = cmd.CreateParameter();
            paramRecordTypeID.ParameterName = "@RecordTypeID";
            paramRecordTypeID.Value = RecordTypeID;
            paramCollection.Add(paramRecordTypeID);

            return cmd;
        }


        public void SaveRecItemComment(RecItemCommentInfo oRecItemCommentInfo, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            IDbCommand cmd = CreateSaveRecItemComment(oRecItemCommentInfo);
            cmd.Connection = oConnection;
            cmd.Transaction = oTransaction;
            cmd.ExecuteNonQuery();
        }


        private IDbCommand CreateSaveRecItemComment(RecItemCommentInfo oRecItemCommentInfo)
        {
            IDbCommand cmd = this.CreateCommand("[dbo].[usp_SAV_RecItemComment]");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            IDbDataParameter paramRecordID = cmd.CreateParameter();
            paramRecordID.ParameterName = "@RecordID";
            if (oRecItemCommentInfo.RecItemID.HasValue)
                paramRecordID.Value = oRecItemCommentInfo.RecItemID.Value;
            else
                paramRecordID.Value = DBNull.Value;
            cmdParams.Add(paramRecordID);
            IDbDataParameter paramRecordTypeID = cmd.CreateParameter();
            paramRecordTypeID.ParameterName = "@RecordTypeID";
            if (oRecItemCommentInfo.RecItemID.HasValue)
                paramRecordTypeID.Value = oRecItemCommentInfo.RecordTypeID.Value;
            else
                paramRecordTypeID.Value = DBNull.Value;
            cmdParams.Add(paramRecordTypeID);
            IDbDataParameter paramComment = cmd.CreateParameter();
            paramComment.ParameterName = "@Comment";
            if (oRecItemCommentInfo.Comment != null)
                paramComment.Value = oRecItemCommentInfo.Comment;
            else
                paramComment.Value = DBNull.Value;
            cmdParams.Add(paramComment);
            ServiceHelper.AddCommonParametersForAddedBy(oRecItemCommentInfo.AddedBy, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForDateAdded(oRecItemCommentInfo.DateAdded, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForAddedUserID(oRecItemCommentInfo.AddedByUserID,cmd, cmdParams);
            IDbDataParameter cmdAddedByUserRoleID = cmd.CreateParameter();
            cmdAddedByUserRoleID.ParameterName = "@AddedByUserRoleID";
            cmdAddedByUserRoleID.Value = ServiceHelper.ReturnDBNullWhenNull(oRecItemCommentInfo.AddedByUserRoleID);
            cmdParams.Add(cmdAddedByUserRoleID);
            return cmd;
        }
    }
}