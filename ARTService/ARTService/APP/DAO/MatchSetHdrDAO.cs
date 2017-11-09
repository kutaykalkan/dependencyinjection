using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using SkyStem.ART.Service.Model;
using System.Data;
using SkyStem.ART.Service.APP.DAO;
using SkyStem.ART.Service.Data;
using SkyStem.ART.Service.Utility;
using SkyStem.ART.Client.Model.CompanyDatabase;

namespace SkyStem.ART.Service.APP.DAO
{
    public class MatchSetHdrDAO : AbstractDAO
    {
        #region "Column Name"
        public static readonly string COLUMN_MATCHSETID = "MatchSetID";
        public static readonly string COLUMN_MATCHSETNAME = "MatchSetName";
        public static readonly string COLUMN_MATCHSETDESCRIPTION = "MatchingDescription";
        public static readonly string COLUMN_GLDATAID = "GLDataID";
        public static readonly string COLUMN_MATCHINGTYPEID = "MatchingTypeID";
        public static readonly string COLUMN_MATCHINGSTATUSID = "MatchingStatusID";
        public static readonly string COLUMN_RECPERIODID = "RecPeriodID";
        public static readonly string COLUMN_EMAILID = "EmailID";
        public static readonly string COLUMN_USERLANGUAGEID = "UserLanguageID";

        public static readonly string COLUMN_ISACTIVE = "IsActive";
        public static readonly string COLUMN_DATEADDED = "DateAdded";
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
        public static readonly string COLUMN_DATEREVISED = "DateRevised";
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        #endregion

        public MatchSetHdrDAO(CompanyUserInfo oCompanyUserInfo)
            : base(oCompanyUserInfo)
        {
        }

        public MatchSetHdrInfo GetMatchSetHdrForProcessing(Enums.MatchingStatus toBeProcessed, Enums.MatchingStatus inProgress)
        {
            SqlConnection oConn = null;
            SqlCommand oCmd = null;
            SqlDataReader reader = null;
            MatchSetHdrInfo oMatchSetHdrInfo = null;
            try
            {
                oConn = this.GetConnection();
                oCmd = this.GetMatchSetHdrForProcessingCommand(toBeProcessed, inProgress);
                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                reader = oCmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (reader.HasRows)
                {
                    reader.Read();
                    oMatchSetHdrInfo = this.MapObject(reader);
                }
                return oMatchSetHdrInfo;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
            }


        }

        public int UpdateMatchSetHdrStatus(MatchSetHdrInfo oMatchSetHdrInfo, DateTime DateRevised, SqlConnection oConnection, SqlTransaction oTransaction)
        {
            SqlCommand oCommand = null;
            oCommand = this.GetMatchSetHdrStatusUpdateCommand(oMatchSetHdrInfo.MatchSetID.Value, oMatchSetHdrInfo.Message, oMatchSetHdrInfo.MatchingStatusID.Value, oMatchSetHdrInfo.AddedBy, DateRevised);
            oCommand.Connection = oConnection;
            oCommand.Transaction = oTransaction;
            return oCommand.ExecuteNonQuery();
        }

        public int UpdateMatchSetHdrStatus(MatchSetHdrInfo oMatchSetHdrInfo, DateTime DateRevised)
        {
            SqlConnection oConnection = null;
            SqlCommand oCommand = null;
            try
            {
                oConnection = this.GetConnection();
                oCommand = this.GetMatchSetHdrStatusUpdateCommand(oMatchSetHdrInfo.MatchSetID.Value, oMatchSetHdrInfo.Message, oMatchSetHdrInfo.MatchingStatusID.Value, oMatchSetHdrInfo.AddedBy, DateRevised);
                oCommand.Connection = oConnection;
                oConnection.Open();
                return oCommand.ExecuteNonQuery();
            }
            finally
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                    oConnection.Close();
            }
        }

        #region "Private Methods"
        private MatchSetHdrInfo MapObject(SqlDataReader reader)
        {
            MatchSetHdrInfo oMatchSetHdrInfo = new MatchSetHdrInfo();
            int ordinal = -1;

            try
            {
                ordinal = reader.GetOrdinal(COLUMN_MATCHSETID);
                if (!reader.IsDBNull(ordinal))
                    oMatchSetHdrInfo.MatchSetID = reader.GetInt64(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            try
            {
                ordinal = reader.GetOrdinal(COLUMN_MATCHSETNAME);
                if (!reader.IsDBNull(ordinal))
                    oMatchSetHdrInfo.MatchSetName = reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            try
            {
                ordinal = reader.GetOrdinal(COLUMN_MATCHSETDESCRIPTION);
                if (!reader.IsDBNull(ordinal))
                    oMatchSetHdrInfo.MatchSetDescription = reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            try
            {
                ordinal = reader.GetOrdinal(COLUMN_GLDATAID);
                if (!reader.IsDBNull(ordinal))
                    oMatchSetHdrInfo.GLDataID = reader.GetInt64(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            try
            {
                ordinal = reader.GetOrdinal(COLUMN_MATCHINGTYPEID);
                if (!reader.IsDBNull(ordinal))
                    oMatchSetHdrInfo.MatchingTypeID = reader.GetInt16(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            try
            {
                ordinal = reader.GetOrdinal(COLUMN_MATCHINGSTATUSID);
                if (!reader.IsDBNull(ordinal))
                    oMatchSetHdrInfo.MatchingStatusID = reader.GetInt16(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            try
            {
                ordinal = reader.GetOrdinal(COLUMN_RECPERIODID);
                if (!reader.IsDBNull(ordinal))
                    oMatchSetHdrInfo.RecPeriodID = reader.GetInt32(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            try
            {
                ordinal = reader.GetOrdinal(COLUMN_ISACTIVE);
                if (!reader.IsDBNull(ordinal))
                    oMatchSetHdrInfo.IsActive = reader.GetBoolean(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            try
            {
                ordinal = reader.GetOrdinal(COLUMN_ADDEDBY);
                if (!reader.IsDBNull(ordinal))
                    oMatchSetHdrInfo.AddedBy = reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            try
            {
                ordinal = reader.GetOrdinal(COLUMN_DATEADDED);
                if (!reader.IsDBNull(ordinal))
                    oMatchSetHdrInfo.DateAdded = reader.GetDateTime(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            try
            {
                ordinal = reader.GetOrdinal(COLUMN_REVISEDBY);
                if (!reader.IsDBNull(ordinal))
                    oMatchSetHdrInfo.RevisedBy = reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            try
            {
                ordinal = reader.GetOrdinal(COLUMN_DATEREVISED);
                if (!reader.IsDBNull(ordinal))
                    oMatchSetHdrInfo.DateRevised = reader.GetDateTime(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            try
            {
                ordinal = reader.GetOrdinal(COLUMN_EMAILID);
                if (!reader.IsDBNull(ordinal))
                    oMatchSetHdrInfo.UserEmailId = reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            try
            {
                ordinal = reader.GetOrdinal(COLUMN_USERLANGUAGEID);
                if (!reader.IsDBNull(ordinal))
                    oMatchSetHdrInfo.UserLanguageID = reader.GetInt32(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }


            return oMatchSetHdrInfo;
        }
        #endregion

        #region "Command Functions"
        private SqlCommand GetMatchSetHdrForProcessingCommand(Enums.MatchingStatus toBeProcessed, Enums.MatchingStatus inProgress)
        {
            SqlCommand oCommand = this.CreateCommand();
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.CommandText = "[Matching].[usp_SVC_GET_MatchSetHdrByMatchingStatusID]";

            SqlParameterCollection oParams = oCommand.Parameters;

            SqlParameter oParamToBeProcessedMatchingStatusID = new SqlParameter();
            oParamToBeProcessedMatchingStatusID.ParameterName = "@ToBeProcessedMatchingStatusID";
            oParamToBeProcessedMatchingStatusID.Value = (short)toBeProcessed;

            SqlParameter oParamInProgressMatchingStatusID = new SqlParameter();
            oParamInProgressMatchingStatusID.ParameterName = "@InProgressMatchingStatusID";
            oParamInProgressMatchingStatusID.Value = (short)inProgress;

            oParams.Add(oParamToBeProcessedMatchingStatusID);
            oParams.Add(oParamInProgressMatchingStatusID);

            return oCommand;
        }

        private SqlCommand GetMatchSetHdrStatusUpdateCommand(long MatchSetHdrID, string Message, short MatSetStatusID, string RevisedBy, DateTime DateRevised)
        {
            SqlCommand oCommand = this.CreateCommand();
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.CommandText = "Matching.usp_UPD_MatchSetHdrStatus";

            SqlParameterCollection oParam = oCommand.Parameters;

            SqlParameter oParamMatchSetHdrID = new SqlParameter("@MatchSetHdrID", MatchSetHdrID);
            SqlParameter oParamMatchSetHdrStatusID = new SqlParameter("@MatSetStatusID", MatSetStatusID);
            SqlParameter oParamRevisedBy = new SqlParameter("@RevisedBy", RevisedBy);
            SqlParameter oParamDateRevised = new SqlParameter("@DateRevised", DateRevised);
            SqlParameter oParamMessage = new SqlParameter("@Message", Message);

            oParam.Add(oParamMatchSetHdrID);
            oParam.Add(oParamMatchSetHdrStatusID);
            oParam.Add(oParamRevisedBy);
            oParam.Add(oParamDateRevised);
            oParam.Add(oParamMessage);

            return oCommand;
        }
        #endregion

    }
}
