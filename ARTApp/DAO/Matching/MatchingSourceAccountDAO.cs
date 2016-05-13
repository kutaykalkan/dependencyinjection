


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Matching.Base;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Params.Matching;
using System.Collections.Generic;

namespace SkyStem.ART.App.DAO.Matching
{
    public class MatchingSourceAccountDAO : MatchingSourceAccountDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MatchingSourceAccountDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }


        public List<GLDataHdrInfo> GetAccountDetails(MatchingParamInfo oMatchingParamInfo)
        {
            List<GLDataHdrInfo> oGLDataHdrInfoCollection = new List<GLDataHdrInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader reader = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateGetGLDataCommand(oMatchingParamInfo);
                cmd.Connection = con;
                con.Open();
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                GLDataHdrInfo oGLDataHdrInfo = null;
                while (reader.Read())
                {
                    oGLDataHdrInfo = this.MapGLDataAndAccountHdr(reader);
                    oGLDataHdrInfoCollection.Add(oGLDataHdrInfo);
                }
            }

            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }

                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oGLDataHdrInfoCollection;
        }


        private IDbCommand CreateGetGLDataCommand(MatchingParamInfo oMatchingParamInfo)
        {
            IDbCommand cmd = this.CreateCommand("[Matching].usp_SEL_AccountsDetails");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdRecPeriodId = cmd.CreateParameter();
            cmdRecPeriodId.ParameterName = "@RecPeriodID";
            cmdRecPeriodId.Value = oMatchingParamInfo.RecPeriodID;
            cmdParams.Add(cmdRecPeriodId);

            IDbDataParameter cmdMatchingSourceDataImportID = cmd.CreateParameter();
            cmdMatchingSourceDataImportID.ParameterName = "@MatchingSourceDataImportID";
            cmdMatchingSourceDataImportID.Value = oMatchingParamInfo.MatchingSourceDataImportID;
            cmdParams.Add(cmdMatchingSourceDataImportID);

            return cmd;

        }



        private GLDataAndAccountHdrInfo MapGLDataAndAccountHdr(IDataReader reader)
        {
            GLDataAndAccountHdrInfo oGLDataAndAccountHdrInfo = new GLDataAndAccountHdrInfo();
            try
            {
                int ordinal = reader.GetOrdinal("GLDataID");
                if (!reader.IsDBNull(ordinal))
                    oGLDataAndAccountHdrInfo.GLDataID = ((System.Int64)(reader.GetValue(ordinal)));
            }
            catch (IndexOutOfRangeException) { }

            try
            {
                int ordinal = reader.GetOrdinal("AccountID");
                if (!reader.IsDBNull(ordinal))
                    oGLDataAndAccountHdrInfo.AccountID = ((System.Int64)(reader.GetValue(ordinal)));
            }
            catch (IndexOutOfRangeException) { }

            try
            {
                int ordinal = reader.GetOrdinal("AccountNumber");
                if (!reader.IsDBNull(ordinal))
                    oGLDataAndAccountHdrInfo.AccountNumber = ((System.String)(reader.GetValue(ordinal)));
            }
            catch (IndexOutOfRangeException) { }


            try
            {
                int ordinal = reader.GetOrdinal("AccountName");
                if (!reader.IsDBNull(ordinal))
                    oGLDataAndAccountHdrInfo.AccountName = ((System.String)(reader.GetValue(ordinal)));

            }
            catch (IndexOutOfRangeException) { }

            try
            {
                int ordinal = reader.GetOrdinal("GLBalanceReportingCurrency");
                if (!reader.IsDBNull(ordinal))
                    oGLDataAndAccountHdrInfo.GLBalanceReportingCurrency = ((System.Decimal)(reader.GetValue(ordinal)));
            }
            catch (IndexOutOfRangeException) { }

            return oGLDataAndAccountHdrInfo;
        }

    }
}