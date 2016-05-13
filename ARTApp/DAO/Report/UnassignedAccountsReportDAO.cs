


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
    public class UnassignedAccountsReportDAO : ReportMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UnassignedAccountsReportDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        internal List<UnassignedAccountsReportInfo> GetReportUnassignedAccountsReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch)
        {
            List<UnassignedAccountsReportInfo> oUnassignedAccountsReportInfoCollection = new List<UnassignedAccountsReportInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();

                cmd = this.CreateGetReportUnassignedAccountsReportCommand(oReportSearchCriteria, tblEntitySearch);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                UnassignedAccountsReportInfo oUnassignedAccountsReportInfo = null;
                GeographyObjectHdrDAO oGeographyObjectHdrDAO = new GeographyObjectHdrDAO(this.CurrentAppUserInfo);

                while (reader.Read())
                {
                    oUnassignedAccountsReportInfo = MapAccountObject(reader);
                    oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oUnassignedAccountsReportInfo);
                    oUnassignedAccountsReportInfoCollection.Add(oUnassignedAccountsReportInfo);
                    //this.MapAccountAttributeInfo(reader, oUnassignedAccountsReportInfo);
                }
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oUnassignedAccountsReportInfoCollection;
        }

        private IDbCommand CreateGetReportUnassignedAccountsReportCommand(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch)
        {
            IDbCommand cmd = this.CreateCommand("usp_RPT_SEL_UnassignedAccountsReport");
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


            IDbDataParameter cmdIsDualReviewEnabled = cmd.CreateParameter();
            cmdIsDualReviewEnabled.ParameterName = "@IsDualReviewEnabled";
            cmdIsDualReviewEnabled.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.IsDualReviewEnabled);
            cmdParams.Add(cmdIsDualReviewEnabled);


            return cmd;
        }


        private UnassignedAccountsReportInfo MapAccountObject(IDataReader r)
        {
            UnassignedAccountsReportInfo entity = new UnassignedAccountsReportInfo();

            entity.AccountID = r.GetInt64Value("AccountID");
            entity.AccountNumber = r.GetStringValue("AccountNumber");
            entity.AccountName = r.GetStringValue("AccountName");
            //entity.AccountNameLabelID = r.GetInt32Value("AccountNameLabelID");
            entity.FSCaptionID = r.GetInt32Value("FSCaptionID");
            //entity.NetAccountID = r.GetInt32Value("NetAccountID");
            entity.AccountTypeID = r.GetInt16Value("AccountTypeID");
            entity.AccountType = r.GetStringValue("AccountType");

            entity.IsPreparerSet = r.GetBooleanValue("IsPreparerSet");
            entity.IsReviewerSet = r.GetBooleanValue("IsReviewerSet");
            entity.IsApproverSet = r.GetBooleanValue("IsApproverSet");
            entity.IsNetAccount = r.GetBooleanValue("IsNetAccount");
            return entity;
        }




    }//end of class
}//end of namespace


