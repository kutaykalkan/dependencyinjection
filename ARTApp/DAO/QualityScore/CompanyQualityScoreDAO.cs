

using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.App.DAO.QualityScore.Base;
using SkyStem.ART.Client.Model.QualityScore;
using System.Collections.Generic;
using SkyStem.ART.Client.Params.QualityScore;
using SkyStem.ART.App.Utility;
using System.Transactions;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO.QualityScore
{
    public class CompanyQualityScoreDAO : CompanyQualityScoreDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CompanyQualityScoreDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        /// <summary>
        /// Gets the company quality score info list.
        /// </summary>
        /// <param name="recPeriodID">The rec period ID.</param>
        /// <param name="enabledOnly">The enabled only.</param>
        /// <returns></returns>
        public List<CompanyQualityScoreInfo> GetCompanyQualityScoreInfoList(int? recPeriodID, bool? enabledOnly)
        {
            IDbCommand cmd = null;
            IDbConnection cnn = null;
            IDataReader dr = null;
            List<CompanyQualityScoreInfo> oCompanyQualityScoreInfoList = null;
            try
            {
                oCompanyQualityScoreInfoList = new List<CompanyQualityScoreInfo>();
                cmd = CreateSelectCommandCompanyQualityScoreInfoList(recPeriodID, enabledOnly);
                cnn = this.CreateConnection();
                cnn.Open();
                cmd.Connection = cnn;
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                CompanyQualityScoreInfo oCompanyQualityScoreInfo;
                int rowNumber = 1;
                while (dr.Read())
                {
                    oCompanyQualityScoreInfo = this.MapObject(dr);
                    oCompanyQualityScoreInfo.RowNumber = rowNumber++;
                    oCompanyQualityScoreInfoList.Add(oCompanyQualityScoreInfo);
                }
            }
            finally
            {
                if (dr != null)
                    dr.ClearColumnHash();
                if (cmd != null)
                    cmd.Dispose();
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return oCompanyQualityScoreInfoList;
        }

        /// <summary>
        /// Creates the select command company quality score info list.
        /// </summary>
        /// <param name="recPeriodID">The rec period ID.</param>
        /// <param name="enabledOnly">The enabled only.</param>
        /// <returns></returns>
        private IDbCommand CreateSelectCommandCompanyQualityScoreInfoList(int? recPeriodID, bool? enabledOnly)
        {
            IDbCommand cmd = this.CreateCommand("[QualityScore].[usp_SEL_CompanyQualityScore]");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdRecPeriod = cmd.CreateParameter();
            cmdRecPeriod.ParameterName = "@RecPeriodID";
            cmdRecPeriod.Value = recPeriodID.Value;
            cmdParams.Add(cmdRecPeriod);

            IDbDataParameter cmdEnabledOnly = cmd.CreateParameter();
            cmdEnabledOnly.ParameterName = "@EnabledOnly";
            cmdEnabledOnly.Value = enabledOnly.Value;
            cmdParams.Add(cmdEnabledOnly);

            return cmd;
        }

        /// <summary>
        /// Saves the company quality score info list.
        /// </summary>
        /// <param name="oCompanyQualityScoreInfoList">The o company quality score info list.</param>
        /// <param name="recPeriodID">The rec period ID.</param>
        /// <param name="companyID">The company ID.</param>
        /// <param name="userLoginID">The user login ID.</param>
        /// <param name="dateRevised">The date revised.</param>
        /// <param name="userID">The user ID.</param>
        public void SaveCompanyQualityScoreInfoList(List<CompanyQualityScoreInfo> oCompanyQualityScoreInfoList, int? recPeriodID, int? companyID, string userLoginID, DateTime dateRevised, int? userID)
        {
            IDbCommand cmd = null;
            IDbConnection cnn = null;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    cmd = CreateSaveCommandCompanyQualityScoreInfoList(oCompanyQualityScoreInfoList, recPeriodID, companyID, userLoginID, dateRevised, userID);
                    cnn = this.CreateConnection();
                    cnn.Open();
                    cmd.Connection = cnn;
                    cmd.ExecuteNonQuery();
                    scope.Complete();
                }
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
        }

        /// <summary>
        /// Creates the save command company quality score info list.
        /// </summary>
        /// <param name="oCompanyQualityScoreInfoList">The o company quality score info list.</param>
        /// <param name="recPeriodID">The rec period ID.</param>
        /// <param name="companyID">The company ID.</param>
        /// <param name="userLoginID">The user login ID.</param>
        /// <param name="dateRevised">The date revised.</param>
        /// <param name="userID">The user ID.</param>
        /// <returns></returns>
        private IDbCommand CreateSaveCommandCompanyQualityScoreInfoList(List<CompanyQualityScoreInfo> oCompanyQualityScoreInfoList, int? recPeriodID, int? companyID, string userLoginID, DateTime dateRevised, int? userID)
        {
            IDbCommand cmd = this.CreateCommand("[QualityScore].[usp_SAV_CompanyQualityScore]");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            DataTable dt = ServiceHelper.ConvertCompanyQualityScoreToDataTable(oCompanyQualityScoreInfoList);
            IDbDataParameter paramQualityScoreList = cmd.CreateParameter();
            paramQualityScoreList.ParameterName = "@udtCompanyQualityScore";
            paramQualityScoreList.Value = dt;
            cmdParams.Add(paramQualityScoreList);

            ServiceHelper.AddCommonParametersForRecPeriodID(recPeriodID, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForCompanyID(companyID, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForAddedBy(userLoginID, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForDateAdded(dateRevised, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForAddedUserID(userID, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForRevisedBy(userLoginID, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForDateRevised(dateRevised, cmd, cmdParams);

            return cmd;
        }
    }
}