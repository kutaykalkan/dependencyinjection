using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.App.DAO.QualityScore.Base;
using System.Collections.Generic;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Model.Report;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Data;

namespace SkyStem.ART.App.DAO.QualityScore
{
    public class QualityScoreRangeDAO : QualityScoreRangeDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public QualityScoreRangeDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public List<RangeOfScoreMstInfo> GetAllQualityScoreRanges()
        {
            IDbCommand cmd = null;
            IDbConnection cnn = null;
            IDataReader dr = null;
            List<RangeOfScoreMstInfo> oQualityScoreRangeMstInfoList = null;
            try
            {
                cmd = CreateSelectCommandQualityScoreRangeMstInfoList();
                cnn = this.CreateConnection();
                cnn.Open();
                cmd.Connection = cnn;
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                oQualityScoreRangeMstInfoList = new List<RangeOfScoreMstInfo>();
                RangeOfScoreMstInfo oQualityScoreRangeMstInfo;
                while (dr.Read())
                {
                    oQualityScoreRangeMstInfo = this.MapObject(dr);
                    oQualityScoreRangeMstInfoList.Add(oQualityScoreRangeMstInfo);
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
            return oQualityScoreRangeMstInfoList;
        }

        private IDbCommand CreateSelectCommandQualityScoreRangeMstInfoList()
        {
            IDbCommand cmd = this.CreateCommand("[QualityScore].[usp_SEL_CompanyQualityScoreRanges]");
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }

        private IDbCommand CreateSelectCommandQualityScoreChecklistInfoList(int RecPeriodID)
        {
            IDbCommand cmd = this.CreateCommand("[QualityScore].[usp_SEL_CompanyQualityScore]");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            parRecPeriodID.Value = RecPeriodID;
            cmdParams.Add(parRecPeriodID);

            IDbDataParameter isEnabled = cmd.CreateParameter();
            isEnabled.ParameterName = "@EnabledOnly";
            isEnabled.Value = 1;
            cmdParams.Add(isEnabled);

            return cmd;
        }

        private IDbCommand CreateSelectCommandQualityScoreByQualityScoreIds(DataTable tblQualityScoreID)
        {
            IDbCommand cmd = this.CreateCommand("[dbo].[usp_SEL_QualityScoreByQualityScoreIDs]");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@QualityScoreIDTable";
            parRecPeriodID.Value = tblQualityScoreID;
            cmdParams.Add(parRecPeriodID);

            return cmd;
        }

        public List<QualityScoreChecklistInfo> GetQualityScoreChecklist(int RecPeriodID)
        {
            IDbCommand cmd = null;
            IDbConnection cnn = null;
            IDataReader dr = null;
            List<QualityScoreChecklistInfo> oQualityScoreChecklistInfoList = null;
            try
            {
                cmd = CreateSelectCommandQualityScoreChecklistInfoList(RecPeriodID);
                cnn = this.CreateConnection();
                cnn.Open();
                cmd.Connection = cnn;
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                oQualityScoreChecklistInfoList = new List<QualityScoreChecklistInfo>();
                QualityScoreChecklistInfo oQualityScoreChecklistInfo;
                while (dr.Read())
                {
                    oQualityScoreChecklistInfo = this.MapCheckListObject(dr);
                    oQualityScoreChecklistInfoList.Add(oQualityScoreChecklistInfo);
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
            return oQualityScoreChecklistInfoList;
        }

        public List<QualityScoreChecklistInfo> GetQualityScoreByQualityScoreIDs(List<int> qualityScoreIDs)
        {
            IDbCommand cmd = null;
            IDbConnection cnn = null;
            IDataReader dr = null;
            List<QualityScoreChecklistInfo> oQualityScoreChecklistInfoList = null;
            try
            {
                DataTable dtQualityScore = ConvertQualityScoreListToTable(qualityScoreIDs);
                cmd = CreateSelectCommandQualityScoreByQualityScoreIds(dtQualityScore);
                cnn = this.CreateConnection();
                cnn.Open();
                cmd.Connection = cnn;
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                oQualityScoreChecklistInfoList = new List<QualityScoreChecklistInfo>();
                QualityScoreChecklistInfo oQualityScoreChecklistInfo;
                while (dr.Read())
                {
                    oQualityScoreChecklistInfo = this.MapCheckListObjectForSavedReports(dr);
                    oQualityScoreChecklistInfoList.Add(oQualityScoreChecklistInfo);
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
            return oQualityScoreChecklistInfoList;
        }

        private DataTable ConvertQualityScoreListToTable(List<int> qualityScoreIDs)
        {
            DataTable dtQualityScore = new DataTable();
            dtQualityScore.Columns.Add("ID");
            foreach (int qualityScoreID in qualityScoreIDs)
            {
                DataRow dr = dtQualityScore.NewRow();
                dr["ID"] = qualityScoreID;
                dtQualityScore.Rows.Add(dr);
            }

            return dtQualityScore;
        }

        public List<QualityScoreReportInfo> GetQualityScoreReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch)
        {
            List<QualityScoreReportInfo> oQualityScoreReportInfoCollection = new List<QualityScoreReportInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();

                cmd = this.CreateGetReportQualityScoreReportCommand(oReportSearchCriteria, tblEntitySearch);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                QualityScoreReportInfo oQualityScoreReportInfo = null;
                GeographyObjectHdrDAO oGeographyObjectHdrDAO = new GeographyObjectHdrDAO(this.CurrentAppUserInfo);

                while (reader.Read())
                {
                    oQualityScoreReportInfo = MapQualityScoreObject(reader);
                    oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oQualityScoreReportInfo);
                    oQualityScoreReportInfoCollection.Add(oQualityScoreReportInfo);
                    //this.MapAccountAttributeInfo(reader, oUnusualBalancesReportInfo);
                }
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oQualityScoreReportInfoCollection;
        }

        private IDbCommand CreateGetReportQualityScoreReportCommand(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch)
        {
            IDbCommand cmd = this.CreateCommand("usp_RPT_SEL_QualityScoreReport");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdTableKeyValue = cmd.CreateParameter();
            cmdTableKeyValue.ParameterName = "@tblKeyValue";
            cmdTableKeyValue.Value = tblEntitySearch;
            cmdParams.Add(cmdTableKeyValue);

            ServiceHelper.AddCommonParametersForAccountEntitySearchInReport(oReportSearchCriteria, true, cmd, cmdParams);

            IDbDataParameter cmdIsKeyAccount = cmd.CreateParameter();
            cmdIsKeyAccount.ParameterName = "@IsKeyAccount";
            cmdIsKeyAccount.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.IsKeyccount);
            cmdParams.Add(cmdIsKeyAccount);

            IDbDataParameter cmdIsMaterialAccount = cmd.CreateParameter();
            cmdIsMaterialAccount.ParameterName = "@IsMaterialAccount";
            cmdIsMaterialAccount.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.IsMaterialAccount);
            cmdParams.Add(cmdIsMaterialAccount);

            IDbDataParameter cmdRiskRatingIDs = cmd.CreateParameter();
            cmdRiskRatingIDs.ParameterName = "@RiskRatingIDs";
            cmdRiskRatingIDs.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.RiskRatingIDs);
            cmdParams.Add(cmdRiskRatingIDs);


            IDbDataParameter cmdKeyAccountAttributeId = cmd.CreateParameter();
            cmdKeyAccountAttributeId.ParameterName = "@KeyAccountAttributeId";
            cmdKeyAccountAttributeId.Value = (short)ARTEnums.AccountAttribute.IsKeyAccount;
            cmdParams.Add(cmdKeyAccountAttributeId);

            IDbDataParameter cmdRiskRatingAttributeId = cmd.CreateParameter();
            cmdRiskRatingAttributeId.ParameterName = "@RiskRatingAttributeId";
            cmdRiskRatingAttributeId.Value = (short)ARTEnums.AccountAttribute.RiskRating;
            cmdParams.Add(cmdRiskRatingAttributeId);

            ServiceHelper.AddCommonLanguageParameters(cmd, cmdParams, oReportSearchCriteria.LCID
                , oReportSearchCriteria.BusinessEntityID, oReportSearchCriteria.DefaultLanguageID);

            IDbDataParameter cmdExcludeNetAccount = cmd.CreateParameter();
            cmdExcludeNetAccount.ParameterName = "@ExcludeNetAccount";
            cmdExcludeNetAccount.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.ExcludeNetAccount);
            cmdParams.Add(cmdExcludeNetAccount);


            IDbDataParameter cmdIsRequesterUserIDToBeConsideredForPermission = cmd.CreateParameter();
            cmdIsRequesterUserIDToBeConsideredForPermission.ParameterName = "@IsRequesterUserIDToBeConsideredForPermission";
            cmdIsRequesterUserIDToBeConsideredForPermission.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.IsRequesterUserIDToBeConsideredForPermission);
            cmdParams.Add(cmdIsRequesterUserIDToBeConsideredForPermission);


            IDbDataParameter cmdIsZeroBalanceAccountEnabled = cmd.CreateParameter();
            cmdIsZeroBalanceAccountEnabled.ParameterName = "@IsZeroBalanceAccountEnabled";
            cmdIsZeroBalanceAccountEnabled.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.IsZeroBalanceAccountEnabled);
            cmdParams.Add(cmdIsZeroBalanceAccountEnabled);

            IDbDataParameter cmdQualityScoreIDs = cmd.CreateParameter();
            cmdQualityScoreIDs.ParameterName = "@QualityScoreIDs";
            cmdQualityScoreIDs.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.QualityScoreIDs);
            cmdParams.Add(cmdQualityScoreIDs);

            return cmd;
        }

        private QualityScoreReportInfo MapQualityScoreObject(IDataReader r)
        {
            QualityScoreReportInfo entity = new QualityScoreReportInfo();
            entity.GLDataID = r.GetInt64Value("GLDataID");
            entity.AccountID = r.GetInt64Value("AccountID");
            entity.AccountNumber = r.GetStringValue("AccountNumber");
            entity.AccountName = r.GetStringValue("AccountName");
            entity.AccountNameLabelID = r.GetInt32Value("AccountNameLabelID");
            entity.FSCaptionID = r.GetInt32Value("FSCaptionID");
            entity.NetAccountLabelID = r.GetInt32Value("NetAccountLabelID");
            entity.NetAccountID = r.GetInt32Value("NetAccountID");
            entity.AccountTypeID = r.GetInt16Value("AccountTypeID");
            entity.AccountType = r.GetStringValue("AccountType");
            entity.GLBalanceReportingCurrency = r.GetDecimalValue("GLBalanceReportingCurrency");
            entity.GLBalanceBaseCurrency = r.GetDecimalValue("GLBalanceBaseCurrency");
            entity.IsKeyAccount = r.GetBooleanValue("IsKeyAccount");
            entity.RiskRatingID = r.GetInt16Value("RiskRatingID");
            entity.IsMaterial = r.GetBooleanValue("IsMaterial");
            entity.ReasonID = r.GetInt16Value("ReasonID");
            entity.ReasonLabelID = r.GetInt16Value("ReasonLabelID");
            entity.PreparerFirstName = r.GetStringValue("PreparerFirstName");
            entity.PreparerLastName = r.GetStringValue("PreparerLastName");
            entity.BaseCurrencyCode = r.GetStringValue("BaseCurrencyCode");
            entity.SystemQualityScoreStatusID = r.GetInt16Value("SystemQualityScoreStatusID");
            entity.UserQualityScoreStatusID = r.GetInt16Value("UserQualityScoreStatusID");
            entity.QualityScoreDesc = r.GetStringValue("Description");
            entity.SystemQualityScore = r.GetInt16Value("SystemScore");
            entity.UserQualityScore = r.GetInt16Value("UserScore");
            entity.Comments = r.GetStringValue("Comments");
            entity.QualityScoreNumber = r.GetStringValue("QualityScoreNumber");
            entity.QualityScoreDescLabelID = r.GetInt32Value("QualityScoreDescLabelID");
            entity.QualityScoreID = r.GetInt32Value("QualityScoreID");
            return entity;
        }
        // commented by manoj: this method is not use any where in project
        //private DataTable ConvertQualityRangeToTable(string RangeValues)
        //{
        //    DataTable dtRange = new DataTable();
        //    dtRange.Columns.Add("IDs");
        //    if (!string.IsNullOrEmpty(RangeValues))
        //    {
        //        foreach (string range in RangeValues.Split(','))
        //        {
        //            if (range == "1")
        //            {
        //                for (int i = 1; i < 6; i++)
        //                {
        //                    DataRow row = dtRange.NewRow();
        //                    row["IDs"] = i;
        //                    dtRange.Rows.Add(row);
        //                }
        //            }
        //            else if (range == "2")
        //            {
        //                for (int i = 6; i < 11; i++)
        //                {
        //                    DataRow row = dtRange.NewRow();
        //                    row["IDs"] = i;
        //                    dtRange.Rows.Add(row);
        //                }
        //            }

        //        }
        //    }
        //    return dtRange;
        //}

    }
}



