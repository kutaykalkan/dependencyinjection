

using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.App.DAO.QualityScore.Base;
using SkyStem.ART.Client.Model.QualityScore;
using System.Collections.Generic;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO.QualityScore
{
    public class GLDataQualityScoreDAO : GLDataQualityScoreDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GLDataQualityScoreDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        /// <summary>
        /// Gets the GL data quality score info list.
        /// </summary>
        /// <param name="recPeriodID">The rec period ID.</param>
        /// <param name="glDataID">The gl data ID.</param>
        /// <returns></returns>
        public List<GLDataQualityScoreInfo> GetGLDataQualityScoreInfoList(int? recPeriodID, long? glDataID)
        {
            IDbCommand cmd = null;
            IDbConnection cnn = null;
            IDataReader dr = null;
            List<GLDataQualityScoreInfo> oGLDataQualityScoreInfoList = null;
            try
            {
                cmd = CreateSelectCommandGLDataQualityScoreInfoList(recPeriodID, glDataID);
                cnn = this.CreateConnection();
                cnn.Open();
                cmd.Connection = cnn;
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                oGLDataQualityScoreInfoList = new List<GLDataQualityScoreInfo>();
                GLDataQualityScoreInfo oGLDataQualityScoreInfo;
                while (dr.Read())
                {
                    oGLDataQualityScoreInfo = this.MapObject(dr);
                    oGLDataQualityScoreInfoList.Add(oGLDataQualityScoreInfo);
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
            return oGLDataQualityScoreInfoList;
        }

        /// <summary>
        /// Maps the IDataReader values to a GLDataQualityScoreInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>
        /// GLDataQualityScoreInfo
        /// </returns>
        protected override GLDataQualityScoreInfo MapObject(IDataReader r)
        {
            GLDataQualityScoreInfo entity = base.MapObject(r);
            entity.CompanyQualityScoreInfo = new CompanyQualityScoreInfo();
            entity.CompanyQualityScoreInfo.CompanyQualityScoreID = r.GetInt32Value("CompanyQualityScoreID");
            entity.CompanyQualityScoreInfo.DescriptionLabelID = r.GetInt32Value("DescriptionLabelID");
            entity.CompanyQualityScoreInfo.IsApplicableForSRA = r.GetBooleanValue("IsApplicableForSRA");
            entity.CompanyQualityScoreInfo.IsUserScoreEnabled = r.GetBooleanValue("IsUserScoreEnabled");
            entity.CompanyQualityScoreInfo.QualityScoreID = r.GetInt32Value("QualityScoreID");
            entity.CompanyQualityScoreInfo.QualityScoreNumber = r.GetStringValue("QualityScoreNumber");
            entity.CompanyQualityScoreInfo.Weightage = r.GetDecimalValue("Weightage");
            return entity;
        }

        /// <summary>
        /// Creates the select command GL data quality score info list.
        /// </summary>
        /// <param name="recPeriodID">The rec period ID.</param>
        /// <param name="gLDataID">The g L data ID.</param>
        /// <returns></returns>
        private IDbCommand CreateSelectCommandGLDataQualityScoreInfoList(int? recPeriodID, long? gLDataID)
        {
            IDbCommand cmd = this.CreateCommand("[QualityScore].[usp_SEL_GLDataQualityScore]");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            ServiceHelper.AddCommonParametersForGLDataIDAndRecPeriodID(gLDataID, recPeriodID, cmd, cmdParams);

            return cmd;
        }

        /// <summary>
        /// Saves the GL data quality score info list.
        /// </summary>
        /// <param name="recPeriodID">The rec period ID.</param>
        /// <param name="glDataID">The gl data ID.</param>
        /// <param name="oGLDataQualityScoreInfoList">The o GL data quality score info list.</param>
        /// <param name="userLoginID">The user login ID.</param>
        /// <param name="dateRevised">The date revised.</param>
        public void SaveGLDataQualityScoreInfoList(int? recPeriodID, long? glDataID, List<GLDataQualityScoreInfo> oGLDataQualityScoreInfoList, string userLoginID, DateTime? dateRevised)
        {
            IDbCommand cmd = null;
            IDbConnection cnn = null;
            try
            {
                cmd = CreateSaveCommandGLDataQualityScoreInfoList(recPeriodID, glDataID, oGLDataQualityScoreInfoList, userLoginID, dateRevised);
                cnn = this.CreateConnection();
                cnn.Open();
                cmd.Connection = cnn;
                cmd.ExecuteNonQuery();
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
        /// Saves the GL data quality score info list.
        /// </summary>
        /// <param name="recPeriodID">The rec period ID.</param>
        /// <param name="glDataID">The gl data ID.</param>
        /// <param name="oGLDataQualityScoreInfoList">The o GL data quality score info list.</param>
        /// <param name="userLoginID">The user login ID.</param>
        /// <param name="dateRevised">The date revised.</param>
        /// <param name="oConnection">The o connection.</param>
        /// <param name="oTransaction">The o transaction.</param>
        public void SaveGLDataQualityScoreInfoList(int? recPeriodID, long? glDataID, List<GLDataQualityScoreInfo> oGLDataQualityScoreInfoList, string userLoginID, DateTime? dateRevised, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            IDbCommand cmd = CreateSaveCommandGLDataQualityScoreInfoList(recPeriodID, glDataID, oGLDataQualityScoreInfoList, userLoginID, dateRevised);
            cmd.Connection = oConnection;
            cmd.Transaction = oTransaction;
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Creates the save command GL data quality score info list.
        /// </summary>
        /// <param name="recPeriodID">The rec period ID.</param>
        /// <param name="glDataID">The gl data ID.</param>
        /// <param name="oGLDataQualityScoreInfoList">The o GL data quality score info list.</param>
        /// <param name="userLoginID">The user login ID.</param>
        /// <param name="dateRevised">The date revised.</param>
        /// <param name="userID">The user ID.</param>
        /// <returns></returns>
        private IDbCommand CreateSaveCommandGLDataQualityScoreInfoList(int? recPeriodID, long? glDataID, List<GLDataQualityScoreInfo> oGLDataQualityScoreInfoList, string userLoginID, DateTime? dateRevised)
        {
            IDbCommand cmd = this.CreateCommand("[QualityScore].[usp_SAV_GLDataQualityScore]");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            DataTable dt = ServiceHelper.ConvertGLDataQualityScoreToDataTable(oGLDataQualityScoreInfoList);
            IDbDataParameter paramQualityScoreList = cmd.CreateParameter();
            paramQualityScoreList.ParameterName = "@udtGLDataQualityScore";
            paramQualityScoreList.Value = dt;
            cmdParams.Add(paramQualityScoreList);

            ServiceHelper.AddCommonParametersForGLDataIDAndRecPeriodID(glDataID, recPeriodID, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForAddedBy(userLoginID, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForDateAdded(dateRevised, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForRevisedBy(userLoginID, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForDateRevised(dateRevised, cmd, cmdParams);

            return cmd;
        }

        /// <summary>
        /// Gets the GL data quality score count.
        /// </summary>
        /// <param name="glDataID">The gl data ID.</param>
        /// <returns></returns>
        public Dictionary<ARTEnums.QualityScoreType, Int32?> GetGLDataQualityScoreCount(long? glDataID, int? recPeriodID)
        {
            IDbCommand cmd = null;
            IDbConnection cnn = null;
            IDataReader dr = null;
            Dictionary<ARTEnums.QualityScoreType, Int32?> dictGLDataQualityScoreCount = null;
            try
            {
                cmd = CreateSelectCommandGLDataQualityScoreCount(glDataID, recPeriodID);
                cnn = this.CreateConnection();
                cnn.Open();
                cmd.Connection = cnn;
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dictGLDataQualityScoreCount = new Dictionary<ARTEnums.QualityScoreType, Int32?>();
                if (dr.Read())
                {
                    dictGLDataQualityScoreCount.Add(ARTEnums.QualityScoreType.SystemScore, dr.GetInt32Value("SystemScore"));
                    dictGLDataQualityScoreCount.Add(ARTEnums.QualityScoreType.UserScore, dr.GetInt32Value("UserScore"));
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
            return dictGLDataQualityScoreCount;
        }

        /// <summary>
        /// Creates the select command GL data quality score count.
        /// </summary>
        /// <param name="gLDataID">The g L data ID.</param>
        /// <returns></returns>
        private IDbCommand CreateSelectCommandGLDataQualityScoreCount(long? gLDataID, int? recPeriodID)
        {
            IDbCommand cmd = this.CreateCommand("[QualityScore].[usp_GET_GLDataQualityScoreCount]");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            ServiceHelper.AddCommonParametersForGLDataID(gLDataID, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForRecPeriodID(recPeriodID, cmd, cmdParams);

            return cmd;
        }

        /// <summary>
        /// Recalculates the quality score and update.
        /// </summary>
        /// <param name="recPeriodID">The rec period ID.</param>
        /// <param name="glDataID">The gl data ID.</param>
        /// <param name="userLoginID">The user login ID.</param>
        /// <param name="dateRevised">The date revised.</param>
        /// <param name="oConnection">The o connection.</param>
        /// <param name="oTransaction">The o transaction.</param>
        public void RecalculateQualityScoreAndUpdate(int? recPeriodID, long? glDataID, string userLoginID, DateTime? dateRevised, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            IDbCommand cmd = CreateSaveCommandRecalculateQualityScoreAndUpdate(recPeriodID, glDataID, userLoginID, dateRevised);
            cmd.Connection = oConnection;
            cmd.Transaction = oTransaction;
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Creates the save command recalculate quality score and update.
        /// </summary>
        /// <param name="recPeriodID">The rec period ID.</param>
        /// <param name="glDataID">The gl data ID.</param>
        /// <param name="userLoginID">The user login ID.</param>
        /// <param name="dateRevised">The date revised.</param>
        /// <returns></returns>
        private IDbCommand CreateSaveCommandRecalculateQualityScoreAndUpdate(int? recPeriodID, long? glDataID, string userLoginID, DateTime? dateRevised)
        {
            IDbCommand cmd = this.CreateCommand("[QualityScore].[usp_UPD_RecalculateQualityScores]");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            List<long> longList = new List<long>();
            longList.Add(glDataID.Value);
            DataTable dt = ServiceHelper.ConvertLongIDCollectionToDataTable(longList);
            IDbDataParameter paramQualityScoreList = cmd.CreateParameter();
            paramQualityScoreList.ParameterName = "@udtGLDataIDTable";
            paramQualityScoreList.Value = dt;
            cmdParams.Add(paramQualityScoreList);

            ServiceHelper.AddCommonParametersForRecPeriodID(recPeriodID, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForRevisedBy(userLoginID, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForDateRevised(dateRevised, cmd, cmdParams);

            return cmd;
        }
    }
}