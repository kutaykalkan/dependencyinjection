


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.App.DAO.Matching.Base;
using SkyStem.ART.Client.Model.Matching;
using System.Collections.Generic;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Params.Matching;
using System.Data.SqlClient;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO.Matching
{
    public class MatchSetHdrDAO : MatchSetHdrDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MatchSetHdrDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public List<MatchSetHdrInfo> SelectAllMatchSetHdrInfo(MatchingParamInfo oMatchingParamInfo)
        {
            List<MatchSetHdrInfo> oMatchSetHdrInfoCollection = new List<MatchSetHdrInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateCommand("[Matching].usp_SEL_AllMatchSet");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                con.Open();

                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter cmdUserID = cmd.CreateParameter();
                cmdUserID.ParameterName = "@UserID";
                if (oMatchingParamInfo.UserID.HasValue)
                    cmdUserID.Value = oMatchingParamInfo.UserID.Value;
                else
                    cmdUserID.Value = DBNull.Value;
                cmdParams.Add(cmdUserID);


                IDbDataParameter cmdRoleID = cmd.CreateParameter();
                cmdRoleID.ParameterName = "@RoleID";
                if (oMatchingParamInfo.RoleID.HasValue)
                    cmdRoleID.Value = oMatchingParamInfo.RoleID.Value;
                else
                    cmdUserID.Value = DBNull.Value;
                cmdParams.Add(cmdRoleID);

                IDbDataParameter cmdGLDataID = cmd.CreateParameter();
                cmdGLDataID.ParameterName = "@GLDataID";
                if (oMatchingParamInfo.GLDataID.HasValue)
                    cmdGLDataID.Value = oMatchingParamInfo.GLDataID.Value;
                else
                    cmdGLDataID.Value = DBNull.Value;
                cmdParams.Add(cmdGLDataID);

                IDbDataParameter cmdRecPeriodID = cmd.CreateParameter();
                cmdRecPeriodID.ParameterName = "@RecPeriodID";
                if (oMatchingParamInfo.RecPeriodID.HasValue)
                    cmdRecPeriodID.Value = oMatchingParamInfo.RecPeriodID.Value;
                else
                    cmdRecPeriodID.Value = DBNull.Value;
                cmdParams.Add(cmdRecPeriodID);

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                MatchSetHdrInfo oMatchSetHdrInfo = null;
                while (reader.Read())
                {
                    oMatchSetHdrInfo = this.MapObject(reader);
                    MapObject(reader, oMatchSetHdrInfo);
                    oMatchSetHdrInfoCollection.Add(oMatchSetHdrInfo);
                }
                reader.ClearColumnHash();
                reader.Close();
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oMatchSetHdrInfoCollection;
        }

        public void MapObject(System.Data.IDataReader r, MatchSetHdrInfo oMatchSetHdrInfo)
        {
            oMatchSetHdrInfo.MatchingType = r.GetStringValue("MatchingType");
            oMatchSetHdrInfo.MatchingStatus = r.GetStringValue("MatchingStatus");
            oMatchSetHdrInfo.AccountID = r.GetInt64Value("AccountID");
            oMatchSetHdrInfo.AccountNumber = r.GetStringValue("AccountNumber");
            oMatchSetHdrInfo.AcountNameLabelID = r.GetInt32Value("AccountNameLabelID");
            oMatchSetHdrInfo.AccountName = r.GetStringValue("AccountName");
            oMatchSetHdrInfo.GLDataID = r.GetInt64Value("GLDataID");
            oMatchSetHdrInfo.FirstName = r.GetStringValue("FirstName");
            oMatchSetHdrInfo.LastName = r.GetStringValue("LastName");
        }

        protected override MatchSetHdrInfo MapObject(IDataReader r)
        {
            MatchSetHdrInfo oMatchSetHdrInfo = base.MapObject(r);
            oMatchSetHdrInfo.PreparerUserID = r.GetInt32Value("PreparerUserID");
            oMatchSetHdrInfo.BackupPreparerUserID = r.GetInt32Value("BackupPreparerUserID");
            oMatchSetHdrInfo.LastName = r.GetStringValue("LastName");
            return oMatchSetHdrInfo;
        }

        public long SaveMatchSet(MatchingParamInfo oMatchingParamInfo, IDbConnection con, IDbTransaction oTransaction)
        {
            long matchSetID = 0;
            IDbCommand cmd = null;

            try
            {
                cmd = CreateSaveMatchSetCommand(oMatchingParamInfo, con, oTransaction, cmd);
                object obj = cmd.ExecuteScalar();
                matchSetID = Convert.ToInt64(obj);
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
            }
            return matchSetID;
        }

        private IDbCommand CreateSaveMatchSetCommand(MatchingParamInfo oMatchingParamInfo, IDbConnection con, IDbTransaction oTransaction, IDbCommand cmd)
        {
            cmd = this.CreateCommand("[Matching].usp_INS_MatchSetHdr");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Transaction = oTransaction;

            IDataParameterCollection cmdParams = cmd.Parameters;


            IDbDataParameter cmdMatchSetID = cmd.CreateParameter();
            cmdMatchSetID.ParameterName = "@MatchSetID";
            if (oMatchingParamInfo.MatchSetID == null)
            {
                cmdMatchSetID.Value = DBNull.Value;
            }
            else
            {
                cmdMatchSetID.Value = oMatchingParamInfo.MatchSetID;
            }

            cmdParams.Add(cmdMatchSetID);

            IDbDataParameter cmdCompanyID = cmd.CreateParameter();
            cmdCompanyID.ParameterName = "@CompanyID";
            cmdCompanyID.Value = oMatchingParamInfo.CompanyID;
            cmdParams.Add(cmdCompanyID);

            IDbDataParameter cmdMatchSetName = cmd.CreateParameter();
            cmdMatchSetName.ParameterName = "@MatchSetName";
            cmdMatchSetName.Value = oMatchingParamInfo.MatchSetName;
            cmdParams.Add(cmdMatchSetName);

            IDbDataParameter cmdMatchSetDescription = cmd.CreateParameter();
            cmdMatchSetDescription.ParameterName = "@MatchSetDescription";
            cmdMatchSetDescription.Value = oMatchingParamInfo.MatchSetDescription;
            cmdParams.Add(cmdMatchSetDescription);

            if (oMatchingParamInfo.GLDataID.HasValue && oMatchingParamInfo.GLDataID.Value > 0)
            {
                IDbDataParameter cmdGLDataID = cmd.CreateParameter();
                cmdGLDataID.ParameterName = "@GLDataID";
                cmdGLDataID.Value = oMatchingParamInfo.GLDataID;
                cmdParams.Add(cmdGLDataID);
            }

            IDbDataParameter cmdMatchingTypeID = cmd.CreateParameter();
            cmdMatchingTypeID.ParameterName = "@MatchingTypeID";
            cmdMatchingTypeID.Value = oMatchingParamInfo.MatchingTypeID;
            cmdParams.Add(cmdMatchingTypeID);

            IDbDataParameter cmdMatchingStatusID = cmd.CreateParameter();
            cmdMatchingStatusID.ParameterName = "@MatchingStatusID";
            cmdMatchingStatusID.Value = oMatchingParamInfo.MatchingStatusID;
            cmdParams.Add(cmdMatchingStatusID);

            IDbDataParameter cmdRecPeriodID = cmd.CreateParameter();
            cmdRecPeriodID.ParameterName = "@RecPeriodID";
            cmdRecPeriodID.Value = oMatchingParamInfo.RecPeriodID;
            cmdParams.Add(cmdRecPeriodID);


            IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            parIsActive.Value = oMatchingParamInfo.IsActive;
            cmdParams.Add(parIsActive);


            IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (oMatchingParamInfo.DateAdded != null)
            {
                parDateAdded.Value = oMatchingParamInfo.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            }
            else
            {
                parDateAdded.Value = System.DBNull.Value;
            }
            cmdParams.Add(parDateAdded);


            IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            parAddedBy.Value = oMatchingParamInfo.AddedBy;
            cmdParams.Add(parAddedBy);


            IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            parDateRevised.Value = oMatchingParamInfo.DateRevised.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            cmdParams.Add(parDateRevised);

            IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            parRevisedBy.Value = oMatchingParamInfo.RevisedBy;
            cmdParams.Add(parRevisedBy);


            IDbDataParameter parAddedByUserID = cmd.CreateParameter();
            parAddedByUserID.ParameterName = "@AddedByUserID";
            parAddedByUserID.Value = oMatchingParamInfo.UserID;
            cmdParams.Add(parAddedByUserID);

            IDbDataParameter cmdRoleID = cmd.CreateParameter();
            cmdRoleID.ParameterName = "@RoleID";
            cmdRoleID.Value = oMatchingParamInfo.RoleID;
            cmdParams.Add(cmdRoleID);

            IDbDataParameter parUserLanguageID = cmd.CreateParameter();
            parUserLanguageID.ParameterName = "@UserLanguageID";
            parUserLanguageID.Value = oMatchingParamInfo.UserLanguageID;
            cmdParams.Add(parUserLanguageID);
            return cmd;
        }

        public void DeleteMatchSet(MatchingParamInfo oMatchingParamInfo)
        {
            IDbCommand cmd = null;
            IDbConnection con = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateGetDeleteMatchSetCommand(oMatchingParamInfo);
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }

            }
        }


        private IDbCommand CreateGetDeleteMatchSetCommand(MatchingParamInfo oMatchingParamInfo)
        {
            IDbCommand cmd = this.CreateCommand("[Matching].usp_DEL_MatchSetHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter paramMatchSetID = cmd.CreateParameter();
            paramMatchSetID.ParameterName = "@MatchSetID";
            paramMatchSetID.Value = oMatchingParamInfo.MatchSetID;
            cmdParams.Add(paramMatchSetID);

            IDbDataParameter paramRecordSourceTypeID = cmd.CreateParameter();
            paramRecordSourceTypeID.ParameterName = "@RecordSourceTypeID";
            paramRecordSourceTypeID.Value = oMatchingParamInfo.RecordSourceTypeID;
            cmdParams.Add(paramRecordSourceTypeID);

            IDbDataParameter paramRevisedID = cmd.CreateParameter();
            paramRevisedID.ParameterName = "@RevisedBy";
            paramRevisedID.Value = oMatchingParamInfo.RevisedBy;
            cmdParams.Add(paramRevisedID);


            IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            parDateRevised.Value = oMatchingParamInfo.DateRevised.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();

            cmdParams.Add(parDateRevised);

            return cmd;

        }
        public void EditMatchSet(MatchingParamInfo oMatchingParamInfo, IDbConnection con, IDbTransaction oTransaction)
        {
            IDbCommand cmd = null;
            cmd = this.CreateGetEditMatchSetCommand(oMatchingParamInfo);
            cmd.Connection = con;
            cmd.Transaction = oTransaction;
            cmd.ExecuteNonQuery();
        }
        private IDbCommand CreateGetEditMatchSetCommand(MatchingParamInfo oMatchingParamInfo)
        {
            IDbCommand cmd = this.CreateCommand("[Matching].[usp_UPD_EditMatchSet]");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter paramMatchSetID = cmd.CreateParameter();
            paramMatchSetID.ParameterName = "@MatchSetHdrID";
            paramMatchSetID.Value = oMatchingParamInfo.MatchSetID;
            cmdParams.Add(paramMatchSetID);

            IDbDataParameter paramMatSetStatusID = cmd.CreateParameter();
            paramMatSetStatusID.ParameterName = "@MatSetStatusID";
            paramMatSetStatusID.Value = oMatchingParamInfo.MatchingStatusID;
            cmdParams.Add(paramMatSetStatusID);

            IDbDataParameter paramRevisedID = cmd.CreateParameter();
            paramRevisedID.ParameterName = "@RevisedBy";
            paramRevisedID.Value = oMatchingParamInfo.RevisedBy;
            cmdParams.Add(paramRevisedID);


            IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            parDateRevised.Value = oMatchingParamInfo.DateRevised.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();

            cmdParams.Add(parDateRevised);

            return cmd;

        }

        public bool IsRecItemCreated(MatchingParamInfo oMatchingParamInfo)
        {
            bool bRecItemCreated = false;
            IDbCommand cmd = null;
            IDbConnection con = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateIsRecItemCreatedCommand(oMatchingParamInfo);
                cmd.Connection = con;
                con.Open();
                object obj = cmd.ExecuteScalar();
                if (obj != null)
                {
                    bRecItemCreated = Convert.ToBoolean((int)obj);

                }

            }
            finally
            {

            }
            return bRecItemCreated;
        }

        private IDbCommand CreateIsRecItemCreatedCommand(MatchingParamInfo oMatchingParamInfo)
        {
            IDbCommand cmd = this.CreateCommand("[Matching].usp_CHK_IsRecItemCreated");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter paramMatchSetID = cmd.CreateParameter();
            paramMatchSetID.ParameterName = "@MatchSetID";
            paramMatchSetID.Value = oMatchingParamInfo.MatchSetID;
            cmdParams.Add(paramMatchSetID);

            IDbDataParameter paramRecordSourceTypeID = cmd.CreateParameter();
            paramRecordSourceTypeID.ParameterName = "@RecordSourceTypeID";
            paramRecordSourceTypeID.Value = oMatchingParamInfo.RecordSourceTypeID;
            cmdParams.Add(paramRecordSourceTypeID);

            return cmd;
        }

        public MatchSetHdrInfo GetMatchSetHdrInfo(MatchingParamInfo oMatchingParamInfo)
        {
            MatchSetHdrInfo oMatchSetHdrInfo = null;
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateCommand("[Matching].usp_GET_MatchSetHdrInfo");
                cmd.CommandType = CommandType.StoredProcedure;


                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter paramMatchSetID = cmd.CreateParameter();
                paramMatchSetID.ParameterName = "@MatchSetID";
                paramMatchSetID.Value = oMatchingParamInfo.MatchSetID;
                cmdParams.Add(paramMatchSetID);

                IDbDataParameter paramRecPeriodID = cmd.CreateParameter();
                paramRecPeriodID.ParameterName = "@RecPeriodID";
                paramRecPeriodID.Value = oMatchingParamInfo.RecPeriodID;
                cmdParams.Add(paramRecPeriodID);

                if (oMatchingParamInfo.GLDataID.HasValue)
                {
                    IDbDataParameter paramGLDataID = cmd.CreateParameter();
                    paramGLDataID.ParameterName = "@GLDataID";
                    paramGLDataID.Value = oMatchingParamInfo.GLDataID;
                    cmdParams.Add(paramGLDataID);
                }

                cmd.Connection = con;
                con.Open();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.Read())
                    oMatchSetHdrInfo = this.MapObject(dr);
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

            return oMatchSetHdrInfo;
        }
    }
}