


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Matching.Base;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model.Matching;
using System.Collections.Generic;
using SkyStem.ART.Client.Params.Matching;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO.Matching
{
    public class MatchSetSubSetCombinationDAO : MatchSetSubSetCombinationDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MatchSetSubSetCombinationDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        #region SaveMatchSetSubSetCombination
        /// <summary>
        /// Saves Matching Source data in the database
        /// </summary>
        /// <param name="dtMatchingSourceDataTableType">User defined table type</param>
        /// <returns></returns>
        public List<MatchSetSubSetCombinationInfo> SaveMatchSetSubSetCombination(DataTable dtMatchSetSubSetCombination)
        {
            List<MatchSetSubSetCombinationInfo> oMatchSetSubSetCombinationInfoCollection = new List<MatchSetSubSetCombinationInfo>();
            IDbCommand cmd = null;
            IDbConnection con = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateCommand("Matching.usp_INS_MatchSetSubSetCombination");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter cmdMatchingSourceDataTable = cmd.CreateParameter();
                cmdMatchingSourceDataTable.ParameterName = "@dtMatchSetSubSetCombination";
                cmdMatchingSourceDataTable.Value = dtMatchSetSubSetCombination;
                cmdParams.Add(cmdMatchingSourceDataTable);
                con.Open();
                oMatchSetSubSetCombinationInfoCollection = this.Select(cmd);
            }
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Close();
                    con.Dispose();
                }
            }

            return oMatchSetSubSetCombinationInfoCollection;
        }


        public List<MatchSetSubSetCombinationInfo> GetAllMatchSetSubSetCombination(MatchingParamInfo oMatchingParamInfo)
        {
            List<MatchSetSubSetCombinationInfo> oMatchSetSubSetCombinationInfoCollection = new List<MatchSetSubSetCombinationInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateCommand("Matching.usp_SEL_MatchSetSubSetCombination");
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

                if (oMatchingParamInfo.IsConfigurationComplete.HasValue)
                {
                    IDbDataParameter paramIsConfigurationComplete = cmd.CreateParameter();
                    paramIsConfigurationComplete.ParameterName = "@IsConfigurationComplete";
                    paramIsConfigurationComplete.Value = oMatchingParamInfo.IsConfigurationComplete;
                    cmdParams.Add(paramIsConfigurationComplete);
                }

                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                MatchSetSubSetCombinationInfo oMatchSetSubSetCombinationInfo = null;
                while (reader.Read())
                {
                    oMatchSetSubSetCombinationInfo = this.MapObject(reader);
                    oMatchSetSubSetCombinationInfoCollection.Add(oMatchSetSubSetCombinationInfo);
                }
                reader.Close();
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oMatchSetSubSetCombinationInfoCollection;
        }

        protected override MatchSetSubSetCombinationInfo MapObject(IDataReader r)
        {
            MatchSetSubSetCombinationInfo entity = base.MapObject(r);
            entity.MatchingSourceDataImport1ID = r.GetInt64Value("MatchingSourceDataImport1ID");
            entity.MatchingSourceDataImport2ID = r.GetInt64Value("MatchingSourceDataImport2ID");
            return entity;
        }


        public MatchSetSubSetCombinationInfoForNetAmountCalculation GetMatchSetSubSetCombinationForNetAmountCalculationByID(Int64? MatchSetSubSetCombinationID)
        {
            MatchSetSubSetCombinationInfoForNetAmountCalculation oMatchSetSubSetCombinationInfoForNetAmountCalculation = null;
            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateCommand("Matching.usp_SEL_MatchSetSubSetCombinationByCombinationID");
                cmd.CommandType = CommandType.StoredProcedure;
                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter paramMatchSetID = cmd.CreateParameter();
                paramMatchSetID.ParameterName = "@MatchSetSubCombinationID";
                paramMatchSetID.Value = MatchSetSubSetCombinationID;
                cmdParams.Add(paramMatchSetID);

                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    oMatchSetSubSetCombinationInfoForNetAmountCalculation = this.MapObjectForNetAmountCalculation(reader);

                }
                reader.Close();
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oMatchSetSubSetCombinationInfoForNetAmountCalculation;
        }

        protected MatchSetSubSetCombinationInfoForNetAmountCalculation MapObjectForNetAmountCalculation(IDataReader r)
        {
            MatchSetSubSetCombinationInfoForNetAmountCalculation entity = new MatchSetSubSetCombinationInfoForNetAmountCalculation();
            entity.Source1Name = r.GetStringValue("Source1Name");
            entity.Source2Name = r.GetStringValue("Source2Name");
            return entity;
        }
        #endregion

        #region
        public bool UpdateMatchSetSubSetCombinationForConfigStatus(MatchingParamInfo oMatchingParamInfo)
        {
            bool result = false;
            IDbConnection con = null;
            IDbTransaction oTransaction = null;
            IDbCommand cmd = null;
            try
            {
                DataTable dtMatchSetSubSetCombination = ServiceHelper.ConvertMatchSetSubSetCombinationToDataTable(oMatchingParamInfo.oMatchSetSubSetCombinationInfoList);
                con = this.CreateConnection();
                con.Open();
                oTransaction = con.BeginTransaction();

                cmd = this.CreateCommand("Matching.usp_UPD_MatchSetSubSetCombinationForConfigStatus");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter cmdMatchSetSubSetCombinationIDDataTable = cmd.CreateParameter();
                cmdMatchSetSubSetCombinationIDDataTable.ParameterName = "@dtMatchSetSubSetCombination";
                cmdMatchSetSubSetCombinationIDDataTable.Value = dtMatchSetSubSetCombination;
                cmdParams.Add(cmdMatchSetSubSetCombinationIDDataTable);

                IDbDataParameter cmdMatchSetID = cmd.CreateParameter();
                cmdMatchSetID.ParameterName = "@MatchSetID";
                cmdMatchSetID.Value = oMatchingParamInfo.MatchSetID;
                cmdParams.Add(cmdMatchSetID);

                IDbDataParameter cmdDateRevised = cmd.CreateParameter();
                cmdDateRevised.ParameterName = "@DateRevised";
                cmdDateRevised.Value = oMatchingParamInfo.DateRevised;
                cmdParams.Add(cmdDateRevised);

                IDbDataParameter cmdRevisedBy = cmd.CreateParameter();
                cmdRevisedBy.ParameterName = "@RevisedBy";
                cmdRevisedBy.Value = oMatchingParamInfo.RevisedBy;
                cmdParams.Add(cmdRevisedBy);

                cmd.Transaction = oTransaction;
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                    result = true;

                oTransaction.Commit();
            }
            catch (Exception ex)
            {
                if (oTransaction != null && oTransaction.Connection != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }
                ServiceHelper.LogAndThrowGenericException(ex, this.CurrentAppUserInfo);
            }
            finally
            {
                if (oTransaction != null && oTransaction.Connection != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }

                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Close();
                    con.Dispose();
                }

            }
            return result;
        }
        #endregion

    }
}