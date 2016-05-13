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
    public class AccountStatusReportDAO : ReportMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AccountStatusReportDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        internal List<AccountStatusReportInfo> GetReportAccountStatusReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch)
        {
            List<AccountStatusReportInfo> oAccountStatusReportInfoCollection = new List<AccountStatusReportInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();

                cmd = this.CreateGetReportAccountStatusReportCommand(oReportSearchCriteria, tblEntitySearch);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                AccountStatusReportInfo oAccountStatusReportInfo = null;
                GeographyObjectHdrDAO oGeographyObjectHdrDAO = new GeographyObjectHdrDAO(this.CurrentAppUserInfo);

                // Net Accounts
                while (reader.Read())
                {
                    oAccountStatusReportInfo = MapAccountObject(reader);
                    oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oAccountStatusReportInfo);
                    oAccountStatusReportInfoCollection.Add(oAccountStatusReportInfo);
                }

                reader.ClearColumnHash();

                Int64 currentAccountHdrID = 0;
                Int64 prevAccountHdrID = 0;

                // Accounts
                reader.NextResult();
                while (reader.Read())
                {
                    currentAccountHdrID = reader.GetInt64Value("AccountID").Value;

                    if (prevAccountHdrID != currentAccountHdrID)
                    {
                        oAccountStatusReportInfo = MapAccountObject(reader);
                        oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oAccountStatusReportInfo);
                        oAccountStatusReportInfoCollection.Add(oAccountStatusReportInfo);
                    }
                    this.MapAccountAttributeInfo(reader, oAccountStatusReportInfo);
                    prevAccountHdrID = currentAccountHdrID;
                }
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oAccountStatusReportInfoCollection;
        }

        private IDbCommand CreateGetReportAccountStatusReportCommand(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch)
        {
            //IDbCommand cmd = this.CreateCommand("usp_RPT_AccountStatusReport");
            IDbCommand cmd = this.CreateCommand("usp_RPT_SEL_AccountStatusReport");
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


        private AccountStatusReportInfo MapAccountObject(IDataReader r)
        {
            AccountStatusReportInfo entity = new AccountStatusReportInfo();

            entity.AccountID = r.GetInt64Value("AccountID");
            entity.AccountNumber = r.GetStringValue("AccountNumber");
            entity.AccountName = r.GetStringValue("AccountName");
            entity.AccountNameLabelID = r.GetInt32Value("AccountNameLabelID");

            entity.AccountTypeID = r.GetInt16Value("AccountTypeID");
            entity.AccountType = r.GetStringValue("AccountType");
            entity.AccountTypeLabelID = r.GetInt32Value("AccountTypeLabelID");

            entity.FSCaptionID = r.GetInt32Value("FSCaptionID");
            entity.FSCaptionLabelID = r.GetInt32Value("FSCaptionLabelID");

            entity.GLBalanceReportingCurrency = r.GetDecimalValue("GLBalanceReportingCurrency");
            entity.GLBalanceBaseCurrency = r.GetDecimalValue("GLBalanceBaseCurrency");
            entity.ReconciliationStatusID = r.GetInt16Value("ReconciliationStatusID");
            entity.ReconciliationStatusLabelID = r.GetInt32Value("ReconciliationStatusLabelID");
            entity.IsMaterial = r.GetBooleanValue("IsMaterial");

            entity.IsKeyAccount = r.GetBooleanValue("IsKeyAccount");
            entity.RiskRatingID = r.GetInt16Value("RiskRatingID");

            entity.NetAccountID = r.GetInt32Value("NetAccountID");
            entity.NetAccountLabelID = r.GetInt32Value("NetAccountLabelID");
            //BCCY Changes
            entity.BaseCurrencyCode = r.GetStringValue("BaseCurrencyCode");
            return entity;
        }


        private void MapAccountAttributeInfo(IDataReader reader, AccountStatusReportInfo oAccountStatusReportInfo)
        {
            int? accountAttributeId = 0;
            accountAttributeId = reader.GetInt16Value("AccountAttributeID");

            if (accountAttributeId != null && accountAttributeId > 0)
            {
                ARTEnums.AccountAttribute oAccountAttribute = (ARTEnums.AccountAttribute)Enum.Parse(typeof(ARTEnums.AccountAttribute), accountAttributeId.ToString());

                switch (oAccountAttribute)
                {
                    case ARTEnums.AccountAttribute.IsKeyAccount:
                        string isKeyAccount = reader.GetStringValue("AccountAttributeValue");
                        if (!string.IsNullOrEmpty(isKeyAccount))
                        {
                            oAccountStatusReportInfo.IsKeyAccount = (Convert.ToBoolean(isKeyAccount));
                        }
                        break;

                    case ARTEnums.AccountAttribute.RiskRating:
                        string riskRatingID = reader.GetStringValue("AccountAttributeValue");
                        if (!string.IsNullOrEmpty(riskRatingID))
                        {
                            oAccountStatusReportInfo.RiskRatingID = (Convert.ToInt16(riskRatingID));
                        }
                        break;

                }
            }
        }


    }//end of class
}//end of namespace


