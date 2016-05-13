


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
    public class UnusualBalancesReportDAO : ReportMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UnusualBalancesReportDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        internal List<UnusualBalancesReportInfo> GetReportUnusualBalancesReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch)
        {
            List<UnusualBalancesReportInfo> oUnusualBalancesReportInfoCollection = new List<UnusualBalancesReportInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();

                cmd = this.CreateGetReportUnusualBalancesReportCommand(oReportSearchCriteria, tblEntitySearch);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                UnusualBalancesReportInfo oUnusualBalancesReportInfo = null;
                GeographyObjectHdrDAO oGeographyObjectHdrDAO = new GeographyObjectHdrDAO(this.CurrentAppUserInfo);

                while (reader.Read())
                {
                    oUnusualBalancesReportInfo = MapAccountObject(reader);
                    oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oUnusualBalancesReportInfo);
                    oUnusualBalancesReportInfoCollection.Add(oUnusualBalancesReportInfo);
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

            return oUnusualBalancesReportInfoCollection;
        }

        private IDbCommand CreateGetReportUnusualBalancesReportCommand(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch)
        {
            IDbCommand cmd = this.CreateCommand("usp_RPT_SEL_UnusualBalancesReport");
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

            IDbDataParameter cmdReasonCodeIDs = cmd.CreateParameter();
            cmdReasonCodeIDs.ParameterName = "@ReasonCodeIDs";
            cmdReasonCodeIDs.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.ReasonCodeIDs);
            cmdParams.Add(cmdReasonCodeIDs);

            IDbDataParameter cmdIsZeroBalanceAccountEnabled = cmd.CreateParameter();
            cmdIsZeroBalanceAccountEnabled.ParameterName = "@IsZeroBalanceAccountEnabled";
            cmdIsZeroBalanceAccountEnabled.Value = ServiceHelper.ReturnDBNullWhenNull(oReportSearchCriteria.IsZeroBalanceAccountEnabled);
            cmdParams.Add(cmdIsZeroBalanceAccountEnabled);

            return cmd;
        }

        private UnusualBalancesReportInfo MapAccountObject(IDataReader r)
        {
            UnusualBalancesReportInfo entity = new UnusualBalancesReportInfo();

            entity.AccountID = r.GetInt64Value("AccountID");
            entity.AccountNumber = r.GetStringValue("AccountNumber");
            entity.AccountName = r.GetStringValue("AccountName");
            //entity.AccountNameLabelID = r.GetInt32Value("AccountNameLabelID");
            entity.FSCaptionID = r.GetInt32Value("FSCaptionID");
            //entity.NetAccountID = r.GetInt32Value("NetAccountID");
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
            return entity;
        }




    }//end of class
}//end of namespace


