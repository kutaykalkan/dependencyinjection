


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
    public class IncompleteAccountAttributeReportDAO : ReportMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public IncompleteAccountAttributeReportDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        internal List<IncompleteAccountAttributeReportInfo> GetReportIncompleteAccountAttributeReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch)
        {
            List<IncompleteAccountAttributeReportInfo> oIncompleteAccountAttributeReportInfoCollection = new List<IncompleteAccountAttributeReportInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();

                cmd = this.CreateGetReportIncompleteAccountAttributeReportCommand(oReportSearchCriteria, tblEntitySearch);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                IncompleteAccountAttributeReportInfo oIncompleteAccountAttributeReportInfo = null;
                GeographyObjectHdrDAO oGeographyObjectHdrDAO = new GeographyObjectHdrDAO(this.CurrentAppUserInfo);

                while (reader.Read())
                {
                    oIncompleteAccountAttributeReportInfo = MapAccountObject(reader);
                    oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oIncompleteAccountAttributeReportInfo);
                    oIncompleteAccountAttributeReportInfoCollection.Add(oIncompleteAccountAttributeReportInfo);
                }
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oIncompleteAccountAttributeReportInfoCollection;
        }

        private IDbCommand CreateGetReportIncompleteAccountAttributeReportCommand(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch)
        {
            IDbCommand cmd = this.CreateCommand("usp_RPT_SEL_IncompleteAccountAttributeReport");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdTableKeyValue = cmd.CreateParameter();
            cmdTableKeyValue.ParameterName = "@tblKeyValue";
            cmdTableKeyValue.Value = tblEntitySearch;
            cmdParams.Add(cmdTableKeyValue);

            ServiceHelper.AddCommonParametersForAccountEntitySearchInReport(oReportSearchCriteria, true, cmd, cmdParams);

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


            IDbDataParameter cmdIsRiskRatingEnabled = cmd.CreateParameter();
            cmdIsRiskRatingEnabled.ParameterName = "@IsRiskRatingEnabled";
            cmdIsRiskRatingEnabled.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.IsRiskRatingEnabled);
            cmdParams.Add(cmdIsRiskRatingEnabled);


            IDbDataParameter cmdIsKeyAccountEnabled = cmd.CreateParameter();
            cmdIsKeyAccountEnabled.ParameterName = "@IsKeyAccountEnabled";
            cmdIsKeyAccountEnabled.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.IsKeyAccountEnabled);
            cmdParams.Add(cmdIsKeyAccountEnabled);

            IDbDataParameter cmdIsDueDateByAccountEnabled = cmd.CreateParameter();
            cmdIsDueDateByAccountEnabled.ParameterName = "@IsDueDateByAccountEnabled";
            cmdIsDueDateByAccountEnabled.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.IsDueDateByAccountEnabled);
            cmdParams.Add(cmdIsDueDateByAccountEnabled);

            return cmd;
        }


        private IncompleteAccountAttributeReportInfo MapAccountObject(IDataReader r)
        {
            IncompleteAccountAttributeReportInfo entity = new IncompleteAccountAttributeReportInfo();

            entity.AccountID = r.GetInt64Value("AccountID");
            entity.AccountNumber = r.GetStringValue("AccountNumber");
            entity.AccountName = r.GetStringValue("AccountName");
            //entity.AccountNameLabelID = r.GetInt32Value("AccountNameLabelID");
            entity.FSCaptionID = r.GetInt32Value("FSCaptionID");
            //entity.NetAccountID = r.GetInt32Value("NetAccountID");
            entity.AccountTypeID = r.GetInt16Value("AccountTypeID");
            entity.AccountType = r.GetStringValue("AccountType");

            entity.IsKeyAccountAttributeMissing = r.GetBooleanValue("IsKeyAccountAttributeMissing");
            entity.IsRiskRatingAttributeMissing = r.GetBooleanValue("IsRiskRatingAttributeMissing");
            entity.IsTemplateAttributeMissing = r.GetBooleanValue("IsTemplateAttributeMissing");
            entity.IsZeroBalanceAttributeMissing = r.GetBooleanValue("IsZeroBalanceAttributeMissing");
            entity.IsFrequencyAttributeMissing = r.GetBooleanValue("IsFrequencyAttributeMissing");
            entity.IsPreparerDueDaysAttributeMissing = r.GetBooleanValue("IsPreparerDueDaysAttributeMissing");
            entity.IsReviewerDueDaysAttributeMissing = r.GetBooleanValue("IsReviewerDueDaysAttributeMissing");
            entity.IsApproverDueDaysAttributeMissing = r.GetBooleanValue("IsApproverDueDaysAttributeMissing");
            return entity;
        }




    }//end of class
}//end of namespace


