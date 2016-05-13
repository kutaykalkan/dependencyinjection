using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using System.Collections.Generic;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Model.Report;

namespace SkyStem.ART.App.DAO.Report
{
    public class CompletionDateReportDAO : ReportMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CompletionDateReportDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        internal List<CompletionDateReportInfo> GetCompletionDateReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch, string System)
        {
            List<CompletionDateReportInfo> oCompletionDateReportInfoList = new List<CompletionDateReportInfo>();

            IDbConnection oCon = null;
            IDbCommand oCmd = null;
            IDataReader oReader = null;

            try
            {
                oCon = this.CreateConnection();
                oCmd = this.GetCompletionDateReportCommand(oReportSearchCriteria, tblEntitySearch);
                oCmd.Connection = oCon;
                oCmd.Connection.Open();

                oReader = oCmd.ExecuteReader(CommandBehavior.CloseConnection);
                GeographyObjectHdrDAO oGeographyObjectHdrDAO = new GeographyObjectHdrDAO(this.CurrentAppUserInfo);
                CompletionDateReportInfo oCompletionDateReportInfo = null;
                // Net Accounts
                while (oReader.Read())
                {

                    oCompletionDateReportInfo = MapAccountObject(oReader, System);
                    oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(oReader, oCompletionDateReportInfo);
                    oCompletionDateReportInfoList.Add(oCompletionDateReportInfo);
                }
                oReader.ClearColumnHash();

                // Accounts
                oReader.NextResult();
                while (oReader.Read())
                {
                    oCompletionDateReportInfo = MapAccountObject(oReader, System);
                    oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(oReader, oCompletionDateReportInfo);
                    oCompletionDateReportInfoList.Add(oCompletionDateReportInfo);
                }

            }
            finally
            {
                if (oReader != null && !oReader.IsClosed)
                    oReader.Close();

                if (oCon != null && oCon.State != ConnectionState.Closed)
                    oCon.Close();
            }

            return oCompletionDateReportInfoList;

        }


        private CompletionDateReportInfo MapAccountObject(IDataReader r, string System)
        {
            CompletionDateReportInfo entity = new CompletionDateReportInfo();

            entity.AccountID = r.GetInt64Value("AccountID");
            entity.AccountNumber = r.GetStringValue("AccountNumber");
            entity.AccountName = r.GetStringValue("AccountName");
            entity.AccountNameLabelID = r.GetInt32Value("AccountNameLabelID");

            entity.AccountTypeID = r.GetInt16Value("AccountTypeID");

            entity.FSCaptionID = r.GetInt32Value("FSCaptionID");

            entity.ReconciliationStatusID = r.GetInt16Value("ReconciliationStatusID");
            entity.ReconciliationStatusLabelID = r.GetInt32Value("ReconciliationStatusLabelID");

            entity.NetAccountID = r.GetInt32Value("NetAccountID");
            entity.NetAccountLabelID = r.GetInt32Value("NetAccountLabelID");

         
            entity.ReviewedBy = r.GetStringValue("ReviewedBy");
            entity.ApprovedBy = r.GetStringValue("ApprovedBy");


            entity.DatePrepared = r.GetDateValue("DatePrepared");
            entity.DateReviewed = r.GetDateValue("DateReviewed");
            entity.DateApproved = r.GetDateValue("DateApproved");
            entity.DateReconciled = r.GetDateValue("DateReconciled");

            entity.IsSRA = r.GetBooleanValue("IsSystemReconcilied");
            if (entity.IsSRA.HasValue && entity.IsSRA.Value)
            {
                entity.SysReconciledBy = r.GetStringValue("ReconciledBy");
                entity.PreparedBy = System;
            }
            else
            {
                entity.ReconciledBy = r.GetStringValue("ReconciledBy");
                entity.PreparedBy = r.GetStringValue("PreparedBy");
            }

            return entity;
        }

        private IDbCommand GetCompletionDateReportCommand(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch)
        {
            IDbCommand cmd = this.CreateCommand("[dbo].[usp_RPT_SEL_CompletionDateReport]");
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

            IDbDataParameter cmdReconciliationStatusIDs = cmd.CreateParameter();
            cmdReconciliationStatusIDs.ParameterName = "@ReconciliationStatusIDs";
            cmdReconciliationStatusIDs.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.ReconciliationStatusIDs);
            cmdParams.Add(cmdReconciliationStatusIDs);

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

            return cmd;
        }
    }
}
