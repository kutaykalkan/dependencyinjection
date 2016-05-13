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
    public class GLDataReviewNoteHdrDAO : GLDataReviewNoteHdrDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GLDataReviewNoteHdrDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        #region GetReviewNoteStatusForReviewerAndApprover

        public bool GetReviewNoteStatusForReviewerAndApprover(long glDataID, int recPeriodID, int userID)
        {
            bool? result = false;
            IDbCommand cmd = null;
            IDbConnection con = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateGetReviewNoteStatusForReviewerAndApprover(glDataID, recPeriodID, userID);
                cmd.Connection = con;

                con.Open();

                result = Convert.ToBoolean((int)cmd.ExecuteScalar());
            }
            finally
            {
                if (con != null || con.State != ConnectionState.Closed)
                    con.Dispose();
            }

            return result.HasValue ? result.Value : false;
        }

        private IDbCommand CreateGetReviewNoteStatusForReviewerAndApprover(long glDataID, int recPeriodID, int userID)
        {
            IDbCommand cmd = this.CreateCommand("usp_GET_ReviewNoteStatusForReviewerAndApprover");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter paramGLDataID = cmd.CreateParameter();
            paramGLDataID.ParameterName = "@GLDataID";
            paramGLDataID.Value = glDataID;
            cmdParams.Add(paramGLDataID);


            IDbDataParameter paramRecPeriodID = cmd.CreateParameter();
            paramRecPeriodID.ParameterName = "@RecPeriodID";
            paramRecPeriodID.Value = recPeriodID;
            cmdParams.Add(paramRecPeriodID);

            IDbDataParameter paramUserLoginID = cmd.CreateParameter();
            paramUserLoginID.ParameterName = "@UserID";
            paramUserLoginID.Value = userID;
            cmdParams.Add(paramUserLoginID);

            return cmd;
        }

        #endregion


        #region GetReviewNotes
        internal List<GLDataReviewNoteHdrInfo> GetReviewNotes(long? GLDataID, int? RecPeriodID)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr = null;
            List<GLDataReviewNoteHdrInfo> oGLDataReviewNoteHdrInfoCollection = null;

            try
            {
                cmd = CreateGetReviewNotesCommand(GLDataID, RecPeriodID);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                oGLDataReviewNoteHdrInfoCollection = MapObjects(dr);
                dr.ClearColumnHash();
            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                }

                if (cmd != null)
                {
                    if (cmd.Connection != null)
                    {
                        if (cmd.Connection.State != ConnectionState.Closed)
                        {
                            cmd.Connection.Close();
                            cmd.Connection.Dispose();
                        }
                    }
                    cmd.Dispose();
                }

            }
            return oGLDataReviewNoteHdrInfoCollection;
        }

        private IDbCommand CreateGetReviewNotesCommand(long? GLDataID, int? RecPeriodID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_GLDataReviewNoteHdrByGLDataID");

            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            ServiceHelper.AddCommonParametersForGLDataIDAndRecPeriodID(GLDataID, RecPeriodID, cmd, cmdParams);
            return cmd;
        }

        #endregion

        #region GetReviewNoteInfo
        internal GLDataReviewNoteHdrInfo GetReviewNoteInfo(long? GLDataReviewNoteID)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr = null;
            GLDataReviewNoteHdrInfo oGLDataReviewNoteHdrInfo = null;

            try
            {
                cmd = CreateGetReviewNoteInfoCommand(GLDataReviewNoteID);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dr.Read();
                oGLDataReviewNoteHdrInfo = MapObject(dr);
                dr.ClearColumnHash();

                // Fetch the Detail Records
                dr.NextResult();
                GLDataReviewNoteDetailDAO oGLDataReviewNoteDetailDAO = new GLDataReviewNoteDetailDAO(this.CurrentAppUserInfo);
                oGLDataReviewNoteHdrInfo.GLDataReviewNoteDetailInfoCollection = oGLDataReviewNoteDetailDAO.MapObjects(dr);

                dr.ClearColumnHash();
            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                }

                if (cmd != null)
                {
                    if (cmd.Connection != null)
                    {
                        if (cmd.Connection.State != ConnectionState.Closed)
                        {
                            cmd.Connection.Close();
                            cmd.Connection.Dispose();
                        }
                    }
                    cmd.Dispose();
                }

            }
            return oGLDataReviewNoteHdrInfo;
        }

        private IDbCommand CreateGetReviewNoteInfoCommand(Int64? GLDataReviewNoteID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_GET_GLDataReviewNoteInfo");

            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter parReviewNoteID = cmd.CreateParameter();
            parReviewNoteID.ParameterName = "@GLDataReviewNoteID";
            parReviewNoteID.Value = GLDataReviewNoteID.Value;
            cmdParams.Add(parReviewNoteID);

            return cmd;
        }

        #endregion


        #region DeleteReviewNote
        internal void DeleteReviewNote(GLDataReviewNoteHdrInfo oGLDataReviewNoteHdrInfo)
        {
            IDbCommand cmd = null;
            try
            {
                cmd = CreateDeleteReviewNoteCommand(oGLDataReviewNoteHdrInfo);
                this.ExecuteNonQuery(cmd);
            }
            finally
            {
                if (cmd != null)
                {
                    if (cmd.Connection != null)
                    {
                        if (cmd.Connection.State != ConnectionState.Closed)
                        {
                            cmd.Connection.Close();
                            cmd.Connection.Dispose();
                        }
                    }
                    cmd.Dispose();
                }

            }

        }

        private IDbCommand CreateDeleteReviewNoteCommand(GLDataReviewNoteHdrInfo oGLDataReviewNoteHdrInfo)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_DEL_GLDataReviewNoteHdr");

            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (!oGLDataReviewNoteHdrInfo.IsDateRevisedNull)
                parDateRevised.Value = oGLDataReviewNoteHdrInfo.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parRevisedByUserID = cmd.CreateParameter();
            parRevisedByUserID.ParameterName = "@RevisedByUserID";
            if (oGLDataReviewNoteHdrInfo.RevisedByUserID != null)
                parRevisedByUserID.Value = oGLDataReviewNoteHdrInfo.RevisedByUserID;
            else
                parRevisedByUserID.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedByUserID);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!oGLDataReviewNoteHdrInfo.IsRevisedByNull)
                parRevisedBy.Value = oGLDataReviewNoteHdrInfo.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            IDbDataParameter parReviewNoteID = cmd.CreateParameter();
            parReviewNoteID.ParameterName = "@GLDataReviewNoteID";
            parReviewNoteID.Value = oGLDataReviewNoteHdrInfo.GLDataReviewNoteID;
            cmdParams.Add(parReviewNoteID);

            return cmd;
        }
        #endregion


        #region DeleteReviewNotesAfterCertification

        public void DeleteReviewNotesAfterCertification(int recPeriodID, string revisedBy, DateTime dateRevised)
        {
            IDbCommand cmd = null;
            IDbConnection con = null;

            try
            {
                cmd = this.CreateDeleteReviewNotesAfterCertificationCommand(recPeriodID, revisedBy, dateRevised);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();

                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Dispose();
            }
        }

        private IDbCommand CreateDeleteReviewNotesAfterCertificationCommand(int recPeriodID, string revisedBy, DateTime dateRevised)
        {
            IDbCommand cmd = this.CreateCommand("usp_DEL_GLDataReviewNoteAfterCertification");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter paramRecPeriodID = cmd.CreateParameter();
            paramRecPeriodID.ParameterName = "@RecPeriodID";
            paramRecPeriodID.Value = recPeriodID;
            cmdParams.Add(paramRecPeriodID);

            IDbDataParameter paramDateRevised = cmd.CreateParameter();
            paramDateRevised.ParameterName = "@DateRevised";
            paramDateRevised.Value = dateRevised;
            cmdParams.Add(paramDateRevised);

            IDbDataParameter paramRevisedBy = cmd.CreateParameter();
            paramRevisedBy.ParameterName = "@RevisedBy";
            paramRevisedBy.Value = revisedBy;
            cmdParams.Add(paramRevisedBy);

            return cmd;
        }

        #endregion

    }
}