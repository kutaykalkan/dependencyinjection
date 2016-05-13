using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using System.Collections.Generic;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Data;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Params;

namespace SkyStem.ART.App.DAO
{
    public class GLDataHdrDAO : GLDataHdrDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GLDataHdrDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        #region SelectGLDataAndAccountInfoByUserID

        public List<GLDataHdrInfo> SelectGLDataAndAccountInfoByUserID(
                                                                    DataTable dtFilterCriteria
                                                                    , int recPeriodID
                                                                    , int companyID
                                                                    , int userID
                                                                    , short userRoleID
                                                                    , bool isDualReviewEnabled
                                                                    , bool isMaterialityEnabled
                                                                    , short preparerAttributeID
                                                                    , short reviewerAttributeID
                                                                    , short approverAttributeID
                                                                    , short preparerRoleID
                                                                    , short reviewerRoleID
                                                                    , short approverRoleID
                                                                    , short systemAdminRoleID
                                                                    , short CEO_CFORoleID
                                                                    , short skyStemAdminRoleID
                                                                    , bool IsIncludeSRA
                                                                    , int? count
                                                                    , List<Int16> AccountAttributeIDCollection
                                                                    , int languageID
                                                                    , int businessEntityID
                                                                    , int defaultLanguageID
                                                                    , string sortExpression
                                                                    , string sortDirection
                                                                )
        {
            List<GLDataHdrInfo> oGLDataHdrInfoCollection = new List<GLDataHdrInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateSelectGLDataAndAccountInfoByUserIDCommand(dtFilterCriteria, recPeriodID, companyID, userID, userRoleID,
                    isDualReviewEnabled, isMaterialityEnabled, preparerAttributeID, reviewerAttributeID, approverAttributeID
                    , preparerRoleID, reviewerRoleID, approverRoleID, systemAdminRoleID, CEO_CFORoleID, skyStemAdminRoleID,
                    IsIncludeSRA, count, AccountAttributeIDCollection, languageID, businessEntityID, defaultLanguageID,
                    sortExpression, sortDirection);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                Int64 currentAccountHdrID = 0;
                Int64 prevAccountHdrID = 0;
                Int32 prevNetAccountID = 0;
                Int32 currentNetAccountID = 0;
                GLDataHdrInfo oGLDataHdrInfo = null;
                GeographyObjectHdrDAO oGeographyObjectHdrDAO = new GeographyObjectHdrDAO(this.CurrentAppUserInfo);
                while (reader.Read())
                {
                    if (reader.GetInt64Value("AccountID") != null)
                    {
                        currentAccountHdrID = reader.GetInt64Value("AccountID").Value;
                        oGLDataHdrInfo = oGLDataHdrInfoCollection.Find(gld => gld.AccountID == currentAccountHdrID);

                        if (prevAccountHdrID != currentAccountHdrID)
                        {
                            if (oGLDataHdrInfo == null)
                            {
                                oGLDataHdrInfo = MapAccountObject(reader);
                                oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oGLDataHdrInfo);
                                oGLDataHdrInfoCollection.Add(oGLDataHdrInfo);
                            }
                            prevAccountHdrID = currentAccountHdrID;
                        }

                        this.MapAccountAttributeInfo(reader, oGLDataHdrInfo);
                        this.MapAccountTaskInfo(reader, oGLDataHdrInfo);
                    }
                    else
                    {
                        if (reader.GetInt32Value("NetAccountID") != null)
                        {
                            currentNetAccountID = reader.GetInt32Value("NetAccountID").Value;
                            oGLDataHdrInfo = oGLDataHdrInfoCollection.Find(gld => gld.NetAccountID == currentNetAccountID);
                            if (prevNetAccountID != currentNetAccountID)
                            {
                                if (oGLDataHdrInfo == null)
                                {
                                    oGLDataHdrInfo = MapAccountObject(reader);
                                    oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oGLDataHdrInfo);
                                    oGLDataHdrInfoCollection.Add(oGLDataHdrInfo);
                                }
                                prevNetAccountID = currentNetAccountID;
                            }

                            this.MapAccountAttributeInfo(reader, oGLDataHdrInfo);
                            this.MapAccountTaskInfo(reader, oGLDataHdrInfo);
                        }
                    }
                }
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oGLDataHdrInfoCollection;
        }




        public List<GLDataHdrInfo> SelectGLDataAndAccountInfoByUserIDForCertificationBalances(
                                                                    int recPeriodID
                                                                    , int companyID
                                                                    , int userID
                                                                    , short userRoleID
                                                                    , bool isDualReviewEnabled
                                                                    , bool isMaterialityEnabled
                                                                    , short preparerAttributeID
                                                                    , short reviewerAttributeID
                                                                    , short approverAttributeID
                                                                    , short preparerRoleID
                                                                    , short reviewerRoleID
                                                                    , short approverRoleID
                                                                    , short systemAdminRoleID
                                                                    , short CEO_CFORoleID
                                                                    , short skyStemAdminRoleID
                                                                    , bool IsIncludeSRA
                                                                    , int? count
                                                                    , List<Int16> AccountAttributeIDCollection
                                                                    , int languageID
                                                                    , int businessEntityID
                                                                    , int defaultLanguageID
            //, string sortExpression
            //, string sortDirection
                                                                )
        {
            List<GLDataHdrInfo> oGLDataHdrInfoCollection = new List<GLDataHdrInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateSelectGLDataAndAccountInfoByUserIDCommand(null, recPeriodID, companyID, userID, userRoleID,
                    isDualReviewEnabled, isMaterialityEnabled, preparerAttributeID, reviewerAttributeID, approverAttributeID
                    , preparerRoleID, reviewerRoleID, approverRoleID, systemAdminRoleID, CEO_CFORoleID, skyStemAdminRoleID,
                    IsIncludeSRA, count, AccountAttributeIDCollection, languageID, businessEntityID, defaultLanguageID,
                    "", "");
                cmd.CommandText = "usp_SEL_GLDataAndAccountInfoByUserIDWithAllSingleAccounts";
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                Int64 currentAccountHdrID = 0;
                Int64 prevAccountHdrID = 0;
                Int32 prevNetAccountID = 0;
                Int32 currentNetAccountID = 0;
                GLDataHdrInfo oGLDataHdrInfo = null;
                GeographyObjectHdrDAO oGeographyObjectHdrDAO = new GeographyObjectHdrDAO(this.CurrentAppUserInfo);
                while (reader.Read())
                {
                    if (reader.GetInt64Value("AccountID") != null)
                    {
                        currentAccountHdrID = reader.GetInt64Value("AccountID").Value;
                        oGLDataHdrInfo = oGLDataHdrInfoCollection.Find(gld => gld.AccountID == currentAccountHdrID);

                        if (prevAccountHdrID != currentAccountHdrID)
                        {
                            if (oGLDataHdrInfo == null)
                            {
                                oGLDataHdrInfo = MapAccountObject(reader);
                                oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oGLDataHdrInfo);
                                oGLDataHdrInfoCollection.Add(oGLDataHdrInfo);
                            }
                            prevAccountHdrID = currentAccountHdrID;
                        }

                        this.MapAccountAttributeInfo(reader, oGLDataHdrInfo);
                    }
                    else
                    {
                        if (reader.GetInt32Value("NetAccountID") != null)
                        {
                            currentNetAccountID = reader.GetInt32Value("NetAccountID").Value;
                            oGLDataHdrInfo = oGLDataHdrInfoCollection.Find(gld => gld.NetAccountID == currentNetAccountID);
                            if (prevNetAccountID != currentNetAccountID)
                            {
                                if (oGLDataHdrInfo == null)
                                {
                                    oGLDataHdrInfo = MapAccountObject(reader);
                                    oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oGLDataHdrInfo);
                                    oGLDataHdrInfoCollection.Add(oGLDataHdrInfo);
                                }
                                prevNetAccountID = currentNetAccountID;
                            }

                            this.MapAccountAttributeInfo(reader, oGLDataHdrInfo);
                        }
                    }
                }
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oGLDataHdrInfoCollection;
        }

        private IDbCommand CreateSelectGLDataAndAccountInfoByUserIDCommand(DataTable dtFilterCriteria, int recPeriodID, int companyID, int userID, short userRoleID, bool isDualReviewEnabled, bool isMaterialityEnabled, short preparerAttributeID, short reviewerAttributeID, short approverAttributeID, short preparerRoleID, short reviewerRoleID, short approverRoleID, short systemAdminRoleID, short CEO_CFORoleID, short skyStemAdminRoleID, bool IsIncludeSRA
            , int? count, List<short> AccountAttributeIDCollection, int languageID, int businessEntityID, int defaultLanguageID, string sortExpression, string sortDirection)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_GLDataAndAccountInfoByUserID");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            if (dtFilterCriteria != null)
            {
                IDbDataParameter cmdFilterCriteriaTableType = cmd.CreateParameter();
                cmdFilterCriteriaTableType.ParameterName = "FilterCriteriaTableType";
                cmdFilterCriteriaTableType.Value = dtFilterCriteria;
                cmdParams.Add(cmdFilterCriteriaTableType);
            }

            IDbDataParameter cmdRecPeriodId = cmd.CreateParameter();
            cmdRecPeriodId.ParameterName = "@RecPeriodID";
            cmdRecPeriodId.Value = recPeriodID;
            cmdParams.Add(cmdRecPeriodId);

            IDbDataParameter cmdCompanyId = cmd.CreateParameter();
            cmdCompanyId.ParameterName = "@CompanyID";
            cmdCompanyId.Value = companyID;
            cmdParams.Add(cmdCompanyId);

            IDbDataParameter cmdUserId = cmd.CreateParameter();
            cmdUserId.ParameterName = "@UserID";
            cmdUserId.Value = userID;
            cmdParams.Add(cmdUserId);

            IDbDataParameter cmdUserRoleId = cmd.CreateParameter();
            cmdUserRoleId.ParameterName = "@UserRoleID";
            cmdUserRoleId.Value = userRoleID;
            cmdParams.Add(cmdUserRoleId);

            IDbDataParameter cmdIsDualReviewEnabled = cmd.CreateParameter();
            cmdIsDualReviewEnabled.ParameterName = "@IsDualReviewEnabled";
            cmdIsDualReviewEnabled.Value = isDualReviewEnabled;
            cmdParams.Add(cmdIsDualReviewEnabled);

            IDbDataParameter cmdIsMaterialityEnabled = cmd.CreateParameter();
            cmdIsMaterialityEnabled.ParameterName = "@IsMaterialityEnabled";
            cmdIsMaterialityEnabled.Value = isMaterialityEnabled;
            cmdParams.Add(cmdIsMaterialityEnabled);

            IDbDataParameter cmdPreparerAttributeID = cmd.CreateParameter();
            cmdPreparerAttributeID.ParameterName = "@PreparerAttributeID";
            cmdPreparerAttributeID.Value = preparerAttributeID;
            cmdParams.Add(cmdPreparerAttributeID);

            IDbDataParameter cmdReviewerAttributeID = cmd.CreateParameter();
            cmdReviewerAttributeID.ParameterName = "@ReviewerAttributeID";
            cmdReviewerAttributeID.Value = reviewerAttributeID;
            cmdParams.Add(cmdReviewerAttributeID);

            IDbDataParameter cmdApproverAttributeID = cmd.CreateParameter();
            cmdApproverAttributeID.ParameterName = "@ApproverAttributeID";
            cmdApproverAttributeID.Value = approverAttributeID;
            cmdParams.Add(cmdApproverAttributeID);

            IDbDataParameter cmdPreparerRoleID = cmd.CreateParameter();
            cmdPreparerRoleID.ParameterName = "@PreparerRoleID";
            cmdPreparerRoleID.Value = preparerRoleID;
            cmdParams.Add(cmdPreparerRoleID);

            IDbDataParameter cmdReviewerRoleID = cmd.CreateParameter();
            cmdReviewerRoleID.ParameterName = "@ReviewerRoleID";
            cmdReviewerRoleID.Value = reviewerRoleID;
            cmdParams.Add(cmdReviewerRoleID);

            IDbDataParameter cmdApproverRoleID = cmd.CreateParameter();
            cmdApproverRoleID.ParameterName = "@ApproverRoleID";
            cmdApproverRoleID.Value = approverRoleID;
            cmdParams.Add(cmdApproverRoleID);

            IDbDataParameter cmdSystemAdminRoleId = cmd.CreateParameter();
            cmdSystemAdminRoleId.ParameterName = "@SystemAdminRoleId";
            cmdSystemAdminRoleId.Value = systemAdminRoleID;
            cmdParams.Add(cmdSystemAdminRoleId);

            IDbDataParameter cmdCEO_CFORoleID = cmd.CreateParameter();
            cmdCEO_CFORoleID.ParameterName = "@CEO_CFORoleID";
            cmdCEO_CFORoleID.Value = CEO_CFORoleID;
            cmdParams.Add(cmdCEO_CFORoleID);

            IDbDataParameter cmdSkyStemAdminRoleID = cmd.CreateParameter();
            cmdSkyStemAdminRoleID.ParameterName = "@SkyStemAdminRoleID";
            cmdSkyStemAdminRoleID.Value = skyStemAdminRoleID;
            cmdParams.Add(cmdSkyStemAdminRoleID);

            IDbDataParameter cmdIsIncludeSRA = cmd.CreateParameter();
            cmdIsIncludeSRA.ParameterName = "@IsIncludeSRA";
            cmdIsIncludeSRA.Value = IsIncludeSRA;
            cmdParams.Add(cmdIsIncludeSRA);

            IDbDataParameter cmdCount = cmd.CreateParameter();
            cmdCount.ParameterName = "@Count";
            if (count.HasValue)
            {
                cmdCount.Value = count.Value;
            }
            else
            {
                cmdCount.Value = DBNull.Value;
            }

            IDbDataParameter cmdAccountAttributeIDCollection = cmd.CreateParameter();
            cmdAccountAttributeIDCollection.ParameterName = "@tblAccountAttributeID";
            cmdAccountAttributeIDCollection.Value = ServiceHelper.ConvertIDCollectionToDataTable(AccountAttributeIDCollection); ;
            cmdParams.Add(cmdAccountAttributeIDCollection);

            IDbDataParameter cmdLanguageID = cmd.CreateParameter();
            cmdLanguageID.ParameterName = "@LCID";
            cmdLanguageID.Value = languageID;
            cmdParams.Add(cmdLanguageID);

            IDbDataParameter cmdBusinessEntityID = cmd.CreateParameter();
            cmdBusinessEntityID.ParameterName = "@BusinessEntityID";
            cmdBusinessEntityID.Value = businessEntityID;
            cmdParams.Add(cmdBusinessEntityID);

            IDbDataParameter cmdDefaultLanguageID = cmd.CreateParameter();
            cmdDefaultLanguageID.ParameterName = "@DefaultLCID";
            cmdDefaultLanguageID.Value = defaultLanguageID;
            cmdParams.Add(cmdDefaultLanguageID);

            IDbDataParameter cmdSortExpression = cmd.CreateParameter();
            cmdSortExpression.ParameterName = "@SortExpression";
            cmdSortExpression.Value = sortExpression;
            cmdParams.Add(cmdSortExpression);

            IDbDataParameter cmdSortDirection = cmd.CreateParameter();
            cmdSortDirection.ParameterName = "@SortDirection";
            cmdSortDirection.Value = sortDirection;
            cmdParams.Add(cmdSortDirection);


            cmdParams.Add(cmdCount);

            return cmd;
        }

        private GLDataHdrInfo MapAccountObject(IDataReader r)
        {
            GLDataHdrInfo entity = new GLDataHdrInfo();

            entity.GLDataID = r.GetInt64Value("GLDataID");
            entity.ReconciliationStatusID = r.GetInt16Value("ReconciliationStatusID");
            entity.CertificationStatusID = r.GetInt16Value("CertificationStatusID");
            entity.GLBalanceReportingCurrency = r.GetDecimalValue("GLBalanceReportingCurrency");
            entity.BaseCurrencyCode = r.GetStringValue("BaseCurrencyCode");
            if (r.GetDecimalValue("GLBalanceReportingCurrency") != null)
            {
                entity.GLBalanceBaseCurrency = r.GetDecimalValue("GLBalanceBaseCurrency");
            }

            entity.ReconciliationBalanceReportingCurrency = r.GetDecimalValue("ReconciliationBalanceReportingCurrency");
            entity.ReconciliationBalanceBaseCurrency = r.GetDecimalValue("ReconciliationBalanceBaseCurrency");
            entity.UnexplainedVarianceReportingCurrency = r.GetDecimalValue("UnexplainedVarianceReportingCurrency");
            entity.UnexplainedVarianceBaseCurrency = r.GetDecimalValue("UnexplainedVarianceBaseCurrency");
            entity.WriteOnOffAmountReportingCurrency = r.GetDecimalValue("WriteOnOffAmountReportingCurrency");
            entity.WriteOnOffAmountBaseCurrency = r.GetDecimalValue("WriteOnOffAmountBaseCurrency");
            entity.CertificationStatus = r.GetStringValue("CertificationStatus");
            entity.CertificationStatusLabelID = r.GetInt32Value("CertificationStatusLabelID");
            entity.ReconciliationStatus = r.GetStringValue("ReconciliationStatus");
            entity.ReconciliationStatusLabelID = r.GetInt32Value("ReconciliationStatusLabelID");
            entity.AccountID = r.GetInt64Value("AccountID");
            entity.AccountNumber = r.GetStringValue("AccountNumber");
            entity.AccountName = r.GetStringValue("AccountName");
            entity.AccountNameLabelID = r.GetInt32Value("AccountNameLabelID");
            entity.NetAccount = r.GetStringValue("NetAccount");
            entity.NetAccountLabelID = r.GetInt32Value("NetAccountLabelID");
            entity.NetAccountID = r.GetInt32Value("NetAccountID");
            entity.FSCaption = r.GetStringValue("FSCaption");
            entity.FSCaptionLabelID = r.GetInt32Value("FSCaptionLabelID");
            entity.AccountMateriality = r.GetStringValue("AccountMateriality");
            entity.AccountTypeID = r.GetInt16Value("AccountTypeID");
            entity.IsFlagged = r.GetBooleanValue("IsFlagged");
            entity.IsSystemReconcilied = r.GetBooleanValue("IsSystemReconcilied");
            entity.SystemReconciliationRuleNumber = r.GetStringValue("SystemReconciliationRuleNumber");
            entity.SystemReconciliationRuleLabelID = r.GetInt32Value("SystemReconciliationRuleLabelID");

            entity.AddedBy = r.GetStringValue("AddedBy");
            entity.DateAdded = r.GetDateValue("DateAdded");
            entity.ReportingCurrencyCode = r.GetStringValue("ReportingCurrencyCode");
            entity.IsLocked = r.GetBooleanValue("IsLocked");
            entity.IsEditable = r.GetBooleanValue("IsEditable");
            entity.PreparerDueDate = r.GetDateValue("PreparerDueDate");
            entity.ReviewerDueDate = r.GetDateValue("ReviewerDueDate");
            entity.ApproverDueDate = r.GetDateValue("ApproverDueDate");
            entity.IsRCCValidation = r.GetBooleanValue("IsRCCValidation");
            return entity;
        }

        private void MapAccountTaskInfo(IDataReader reader, GLDataHdrInfo oGLDataHdrInfo)
        {
            oGLDataHdrInfo.TotalTaskCount = reader.GetInt32Value("TotalTaskCount");
            oGLDataHdrInfo.CompletedTaskCount = reader.GetInt32Value("CompletedTaskCount");
        }

        private void MapAccountAttributeInfo(IDataReader reader, GLDataHdrInfo oGLDataHdrInfo)
        {
            int? accountAttributeId = 0;
            accountAttributeId = reader.GetInt16Value("AccountAttributeID");

            if (accountAttributeId != null && accountAttributeId > 0)
            {
                ARTEnums.AccountAttribute oAccountAttribute = (ARTEnums.AccountAttribute)Enum.Parse(typeof(ARTEnums.AccountAttribute), accountAttributeId.ToString());

                switch (oAccountAttribute)
                {
                    case ARTEnums.AccountAttribute.Approver:
                        string approverID = reader.GetStringValue("AccountAttributeValue");
                        if (!string.IsNullOrEmpty(approverID))
                        {
                            oGLDataHdrInfo.ApproverUserID = Convert.ToInt32(approverID);
                            oGLDataHdrInfo.ApproverFullName = reader.GetStringValue("ApproverFullName");
                        }
                        break;
                    case ARTEnums.AccountAttribute.BackupApprover:
                        string backupApproverID = reader.GetStringValue("AccountAttributeValue");
                        if (!string.IsNullOrEmpty(backupApproverID))
                        {
                            oGLDataHdrInfo.BackupApproverUserID = Convert.ToInt32(backupApproverID);
                            oGLDataHdrInfo.BackupApproverFullName = reader.GetStringValue("BackupApproverFullName");
                        }
                        break;
                    case ARTEnums.AccountAttribute.IsKeyAccount:
                        string isKeyAccount = reader.GetStringValue("AccountAttributeValue");
                        if (!string.IsNullOrEmpty(isKeyAccount))
                        {
                            oGLDataHdrInfo.IsKeyAccount = (Convert.ToBoolean(isKeyAccount));
                        }
                        oGLDataHdrInfo.KeyAccount = reader.GetStringValue("KeyAccount");
                        break;

                    case ARTEnums.AccountAttribute.IsZeroBalanceAccount:
                        string isZeroBalance = reader.GetStringValue("AccountAttributeValue");
                        if (!string.IsNullOrEmpty(isZeroBalance))
                        {
                            oGLDataHdrInfo.IsZeroBalance = (Convert.ToBoolean(isZeroBalance));
                        }
                        oGLDataHdrInfo.ZeroBalance = reader.GetStringValue("ZeroBalance");
                        break;

                    case ARTEnums.AccountAttribute.Preparer:
                        string preparerID = reader.GetStringValue("AccountAttributeValue");
                        if (!string.IsNullOrEmpty(preparerID))
                        {
                            oGLDataHdrInfo.PreparerUserID = (Convert.ToInt32(preparerID));
                            oGLDataHdrInfo.PreparerFullName = reader.GetStringValue("PreparerFullName");
                        }
                        break;
                    case ARTEnums.AccountAttribute.BackupPreparer:
                        string backupPreparerID = reader.GetStringValue("AccountAttributeValue");
                        if (!string.IsNullOrEmpty(backupPreparerID))
                        {
                            oGLDataHdrInfo.BackupPreparerUserID = (Convert.ToInt32(backupPreparerID));
                            oGLDataHdrInfo.BackupPreparerFullName = reader.GetStringValue("BackupPreparerFullName");
                        }
                        break;
                    case ARTEnums.AccountAttribute.ReconciliationTemplate:
                        string reconciliationTemplateID = reader.GetStringValue("AccountAttributeValue");
                        if (!string.IsNullOrEmpty(reconciliationTemplateID))
                        {
                            oGLDataHdrInfo.ReconciliationTemplateID = (Convert.ToInt16(reconciliationTemplateID));
                        }
                        break;

                    case ARTEnums.AccountAttribute.Reviewer:
                        string reviewerID = reader.GetStringValue("AccountAttributeValue");
                        if (!string.IsNullOrEmpty(reviewerID))
                        {
                            oGLDataHdrInfo.ReviewerUserID = (Convert.ToInt32(reviewerID));
                            oGLDataHdrInfo.ReviewerFullName = reader.GetStringValue("ReviewerFullName");
                        }
                        break;
                    case ARTEnums.AccountAttribute.BackupReviewer:
                        string backupReviewerID = reader.GetStringValue("AccountAttributeValue");
                        if (!string.IsNullOrEmpty(backupReviewerID))
                        {
                            oGLDataHdrInfo.BackupReviewerUserID = (Convert.ToInt32(backupReviewerID));
                            oGLDataHdrInfo.BackupReviewerFullName = reader.GetStringValue("BackupReviewerFullName");
                        }
                        break;
                    case ARTEnums.AccountAttribute.RiskRating:
                        string riskRatingID = reader.GetStringValue("AccountAttributeValue");
                        if (!string.IsNullOrEmpty(riskRatingID))
                        {
                            oGLDataHdrInfo.RiskRatingID = (Convert.ToInt16(riskRatingID));
                        }
                        oGLDataHdrInfo.RiskRating = reader.GetStringValue("RiskRating");
                        break;

                    case ARTEnums.AccountAttribute.SubledgerSource:
                        string subledgerSource = reader.GetStringValue("AccountAttributeValue");
                        if (!string.IsNullOrEmpty(subledgerSource))
                        {
                            oGLDataHdrInfo.SubledgerSourceID = (Convert.ToInt32(subledgerSource));
                        }
                        break;


                    case ARTEnums.AccountAttribute.NetAccount:
                        string netAccountID = reader.GetStringValue("AccountAttributeValue");
                        if (!string.IsNullOrEmpty(netAccountID))
                        {
                            oGLDataHdrInfo.NetAccountID = (Convert.ToInt32(netAccountID));
                        }
                        break;
                }
            }
        }

        #endregion

        #region GetAccountReconciledPercentage

        public AccountCountInfo GetTotalAndCompletedAccountCount(
                                                        int recPeriodID
                                                        , int companyID
                                                        , int userID
                                                        , short userRoleID
                                                        , bool isDualReviewEnabled
                                                        , short preparerAttributeID
                                                        , short reviewerAttributeID
                                                        , short approverAttributeID
                                                        , short preparerRoleID
                                                        , short reviewerRoleID
                                                        , short approverRoleID
                                                        , short systemAdminRoleID
                                                        , short CEO_CFORoleID
                                                        , short skyStemAdminRoleID
                                                        , short sysReconciledStatusID
                                                        , bool isIncludeSRA
                                                    )
        {
            AccountCountInfo oAccountCountInfo = null;
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateGetTotalAndAssignedAccountCountCommand(recPeriodID, companyID, userID, userRoleID,
                    isDualReviewEnabled, preparerAttributeID, reviewerAttributeID, approverAttributeID
                    , preparerRoleID, reviewerRoleID, approverRoleID, systemAdminRoleID, CEO_CFORoleID
                    , skyStemAdminRoleID, sysReconciledStatusID, isIncludeSRA);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                oAccountCountInfo = new AccountCountInfo();
                while (reader.Read())
                {
                    oAccountCountInfo.TotalCompletedAccounts = reader.GetInt32Value("TotalCompletedAccounts");
                    oAccountCountInfo.TotalAccounts = reader.GetInt32Value("TotalAccounts");
                }
                reader.ClearColumnHash();
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }
            return oAccountCountInfo;
        }

        private IDbCommand CreateGetTotalAndAssignedAccountCountCommand(int recPeriodID, int companyID, int userID, short userRoleID, bool isDualReviewEnabled, short preparerAttributeID, short reviewerAttributeID, short approverAttributeID, short preparerRoleID, short reviewerRoleID, short approverRoleID, short systemAdminRoleID, short CEO_CFORoleID, short skyStemAdminRoleID, short sysReconciledStatusID, bool isIncludeSRA)
        {
            IDbCommand cmd = this.CreateCommand("usp_GET_TotalAndCompletedAccountCounts");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdRecPeriodId = cmd.CreateParameter();
            cmdRecPeriodId.ParameterName = "@RecPeriodID";
            cmdRecPeriodId.Value = recPeriodID;
            cmdParams.Add(cmdRecPeriodId);

            IDbDataParameter cmdCompanyId = cmd.CreateParameter();
            cmdCompanyId.ParameterName = "@CompanyID";
            cmdCompanyId.Value = companyID;
            cmdParams.Add(cmdCompanyId);

            IDbDataParameter cmdUserId = cmd.CreateParameter();
            cmdUserId.ParameterName = "@UserID";
            cmdUserId.Value = userID;
            cmdParams.Add(cmdUserId);

            IDbDataParameter cmdUserRoleId = cmd.CreateParameter();
            cmdUserRoleId.ParameterName = "@UserRoleID";
            cmdUserRoleId.Value = userRoleID;
            cmdParams.Add(cmdUserRoleId);

            IDbDataParameter cmdIsDualReviewEnabled = cmd.CreateParameter();
            cmdIsDualReviewEnabled.ParameterName = "@IsDualReviewEnabled";
            cmdIsDualReviewEnabled.Value = isDualReviewEnabled;
            cmdParams.Add(cmdIsDualReviewEnabled);

            IDbDataParameter cmdPreparerAttributeID = cmd.CreateParameter();
            cmdPreparerAttributeID.ParameterName = "@PreparerAttributeID";
            cmdPreparerAttributeID.Value = preparerAttributeID;
            cmdParams.Add(cmdPreparerAttributeID);

            IDbDataParameter cmdReviewerAttributeID = cmd.CreateParameter();
            cmdReviewerAttributeID.ParameterName = "@ReviewerAttributeID";
            cmdReviewerAttributeID.Value = reviewerAttributeID;
            cmdParams.Add(cmdReviewerAttributeID);

            IDbDataParameter cmdApproverAttributeID = cmd.CreateParameter();
            cmdApproverAttributeID.ParameterName = "@ApproverAttributeID";
            cmdApproverAttributeID.Value = approverAttributeID;
            cmdParams.Add(cmdApproverAttributeID);

            IDbDataParameter cmdPreparerRoleID = cmd.CreateParameter();
            cmdPreparerRoleID.ParameterName = "@PreparerRoleID";
            cmdPreparerRoleID.Value = preparerRoleID;
            cmdParams.Add(cmdPreparerRoleID);

            IDbDataParameter cmdReviewerRoleID = cmd.CreateParameter();
            cmdReviewerRoleID.ParameterName = "@ReviewerRoleID";
            cmdReviewerRoleID.Value = reviewerRoleID;
            cmdParams.Add(cmdReviewerRoleID);

            IDbDataParameter cmdApproverRoleID = cmd.CreateParameter();
            cmdApproverRoleID.ParameterName = "@ApproverRoleID";
            cmdApproverRoleID.Value = approverRoleID;
            cmdParams.Add(cmdApproverRoleID);

            IDbDataParameter cmdSystemAdminRoleId = cmd.CreateParameter();
            cmdSystemAdminRoleId.ParameterName = "@SystemAdminRoleId";
            cmdSystemAdminRoleId.Value = systemAdminRoleID;
            cmdParams.Add(cmdSystemAdminRoleId);

            IDbDataParameter cmdCEO_CFORoleID = cmd.CreateParameter();
            cmdCEO_CFORoleID.ParameterName = "@CEO_CFORoleID";
            cmdCEO_CFORoleID.Value = CEO_CFORoleID;
            cmdParams.Add(cmdCEO_CFORoleID);

            IDbDataParameter cmdSkyStemAdminRoleID = cmd.CreateParameter();
            cmdSkyStemAdminRoleID.ParameterName = "@SkyStemAdminRoleID";
            cmdSkyStemAdminRoleID.Value = skyStemAdminRoleID;
            cmdParams.Add(cmdSkyStemAdminRoleID);

            IDbDataParameter cmdSysReconciledStatusID = cmd.CreateParameter();
            cmdSysReconciledStatusID.ParameterName = "@SysReconciledStatusID";
            cmdSysReconciledStatusID.Value = sysReconciledStatusID;
            cmdParams.Add(cmdSysReconciledStatusID);

            IDbDataParameter cmdIsIncludeSRA = cmd.CreateParameter();
            cmdIsIncludeSRA.ParameterName = "@IsIncludeSRA";
            cmdIsIncludeSRA.Value = isIncludeSRA;
            cmdParams.Add(cmdIsIncludeSRA);

            return cmd;
        }

        #endregion

        #region SaveGLDataReconciliationStatus



        public bool SaveGLDataReconciliationStatus(DataTable dtGLDataIDs, int currentRecPeriodId, short reconciliationStatusID, string userLoginID, DateTime dateRevised, short actionTypeID, short changeSourceIDSRA, IDbConnection con, IDbTransaction oTransaction)
        {
            bool result = false;
            IDbCommand cmd = null;

            try
            {
                cmd = this.CreateCommand("usp_UPD_GLDataReconciliationStatus");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Transaction = oTransaction;

                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter cmdAccountTable = cmd.CreateParameter();
                cmdAccountTable.ParameterName = "@GLDataIDTable";
                cmdAccountTable.Value = dtGLDataIDs;
                cmdParams.Add(cmdAccountTable);


                IDbDataParameter cmdRecPeriodID = cmd.CreateParameter();
                cmdRecPeriodID.ParameterName = "@RecPeriodID";
                cmdRecPeriodID.Value = currentRecPeriodId;
                cmdParams.Add(cmdRecPeriodID);

                IDbDataParameter cmdAccountAttributeId = cmd.CreateParameter();
                cmdAccountAttributeId.ParameterName = "@ReconciliationStatusID";
                cmdAccountAttributeId.Value = reconciliationStatusID;
                cmdParams.Add(cmdAccountAttributeId);

                IDbDataParameter cmdRevisedBy = cmd.CreateParameter();
                cmdRevisedBy.ParameterName = "@RevisedBy";
                cmdRevisedBy.Value = userLoginID;
                cmdParams.Add(cmdRevisedBy);

                IDbDataParameter cmdDateRevised = cmd.CreateParameter();
                cmdDateRevised.ParameterName = "@DateRevised";
                //Modified By Harsh 
                cmdDateRevised.Value = dateRevised;
                //cmdDateRevised.Value = reconciliationStatusID;
                cmdParams.Add(cmdDateRevised);

                ServiceHelper.AddCommonParametersForActionTypeID(actionTypeID, cmd, cmdParams);
                ServiceHelper.AddCommonParametersForChangeSourceIDSRA(changeSourceIDSRA, cmd, cmdParams);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                    result = true;
            }
            finally
            {

            }

            return result;
        }

        #endregion


        #region GetGLDataAndAccountInfoByGLDataID

        public GLDataAndAccountHdrInfo GetGLDataAndAccountInfoByGLDataID(long glDataID, int recPeriodID, int companyID, int userID, int roleID, bool isDualReviewEnabled, bool isCertificationEnabled, bool isMaterialityEnabled, short certificationTypeID,
            short preparerAttributeId, short reviewerAttributeId, short approverAttributeId,
            short backupPreparerAttributeId, short backupReviewerAttributeId, short backupApproverAttributeId)
        {
            GLDataAndAccountHdrInfo oGLDataAndAccountHdrInfo = null;
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateGetGLDataAndAccountInfoByGLDataIDCommand(glDataID, recPeriodID, companyID, userID, roleID, isDualReviewEnabled,
                    isCertificationEnabled, isMaterialityEnabled, certificationTypeID, preparerAttributeId, reviewerAttributeId, approverAttributeId,
                    backupPreparerAttributeId, backupReviewerAttributeId, backupApproverAttributeId);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                Int64 currentAccountHdrID = 0;
                Int64 prevAccountHdrID = 0;

                while (reader.Read())
                {
                    currentAccountHdrID = reader.GetInt64Value("AccountID").Value;

                    if (prevAccountHdrID != currentAccountHdrID)
                    {
                        oGLDataAndAccountHdrInfo = this.MapGLDataAndAccountHdr(reader);
                        prevAccountHdrID = currentAccountHdrID;
                    }

                    this.MapAccountAttributeInfo(reader, oGLDataAndAccountHdrInfo);
                }
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oGLDataAndAccountHdrInfo;
        }

        private GLDataAndAccountHdrInfo MapGLDataAndAccountHdr(IDataReader reader)
        {
            GLDataAndAccountHdrInfo oGLDataAndAccountHdrInfo = new GLDataAndAccountHdrInfo();

            oGLDataAndAccountHdrInfo.GLDataID = reader.GetInt64Value("GLDataID");
            oGLDataAndAccountHdrInfo.ReconciliationStatusID = reader.GetInt16Value("ReconciliationStatusID");
            oGLDataAndAccountHdrInfo.CertificationStatusID = reader.GetInt16Value("CertificationStatusID");
            oGLDataAndAccountHdrInfo.CertificationStatus = reader.GetStringValue("CertificationStatus");
            oGLDataAndAccountHdrInfo.CertificationStatusLabelID = reader.GetInt32Value("CertificationStatusLabelID");
            oGLDataAndAccountHdrInfo.ReconciliationStatusLabelID = reader.GetInt32Value("ReconciliationStatusLabelID");
            oGLDataAndAccountHdrInfo.AccountID = reader.GetInt64Value("AccountID");
            oGLDataAndAccountHdrInfo.AccountNumber = reader.GetStringValue("AccountNumber");
            oGLDataAndAccountHdrInfo.AccountName = reader.GetStringValue("AccountName");
            oGLDataAndAccountHdrInfo.AccountNameLabelID = reader.GetInt32Value("AccountNameLabelID");
            oGLDataAndAccountHdrInfo.NetAccount = reader.GetStringValue("NetAccount");
            oGLDataAndAccountHdrInfo.NetAccountLabelID = reader.GetInt32Value("NetAccountLabelID");
            oGLDataAndAccountHdrInfo.FSCaptionLabelID = reader.GetInt32Value("FSCaptionLabelID");
            oGLDataAndAccountHdrInfo.AccountTypeLabelID = reader.GetInt32Value("AccountTypeLabelID");
            oGLDataAndAccountHdrInfo.ReconciledStatusDate = reader.GetDateValue("ReconciledStatusDate");
            oGLDataAndAccountHdrInfo.BaseCurrencyCode = reader.GetStringValue("BaseCurrencyCode");
            oGLDataAndAccountHdrInfo.IsMaterial = reader.GetBooleanValue("IsMaterial");
            oGLDataAndAccountHdrInfo.IsSystemReconcilied = reader.GetBooleanValue("IsSystemReconcilied");
            oGLDataAndAccountHdrInfo.PendingReviewStatusDate = reader.GetDateValue("PendingReviewStatusDate");
            oGLDataAndAccountHdrInfo.ReconciliationStatusDate = reader.GetDateValue("ReconciliationStatusDate");
            oGLDataAndAccountHdrInfo.PendingApprovalStatusDate = reader.GetDateValue("PendingApprovalStatusDate");
            oGLDataAndAccountHdrInfo.ReconciliationPeriodCloseDate = reader.GetDateValue("RecPeriodCloseDate");
            oGLDataAndAccountHdrInfo.CertificationStartDate = reader.GetDateValue("CertificationStartDate");
            oGLDataAndAccountHdrInfo.PreparerCertificationSignOffDate = reader.GetDateValue("PreparerCertificationSignOffDate");
            oGLDataAndAccountHdrInfo.ReviewerCertificationSignOffDate = reader.GetDateValue("ReviewerCertificationSignOffDate");
            oGLDataAndAccountHdrInfo.ApproverCertificationSignOffDate = reader.GetDateValue("ApproverCertificationSignOffDate");
            oGLDataAndAccountHdrInfo.AccountMaterialityThreshold = reader.GetDecimalValue("AccountMaterialityThreshold");
            oGLDataAndAccountHdrInfo.SubmittedDate = reader.GetDateValue("SubmittedDate");
            oGLDataAndAccountHdrInfo.UnexplainedVarianceThreshold = reader.GetDecimalValue("UnExplainedVarianceThreshold");
            oGLDataAndAccountHdrInfo.PreparerDueDate = reader.GetDateValue("PreparerDueDate");
            oGLDataAndAccountHdrInfo.ReviewerDueDate = reader.GetDateValue("ReviewerDueDate");
            oGLDataAndAccountHdrInfo.ApproverDueDate = reader.GetDateValue("ApproverDueDate");

            return oGLDataAndAccountHdrInfo;
        }

        private IDbCommand CreateGetGLDataAndAccountInfoByGLDataIDCommand(long glDataID, int recPeriodID, int companyID, int userID, int roleID, bool isDualReviewEnabled, bool isCertificationEnabled, bool isMaterialityEnabled, short certificationTypeID,
            short preparerAttributeId, short reviewerAttributeId, short approverAttributeId, short backupPreparerAttributeId, short backupReviewerAttributeId, short backupApproverAttributeId)
        {
            IDbCommand cmd = this.CreateCommand("usp_GET_GLDataAndAccountInfoByGLDataID");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdGLDataId = cmd.CreateParameter();
            cmdGLDataId.ParameterName = "@GLDataID";
            cmdGLDataId.Value = glDataID;
            cmdParams.Add(cmdGLDataId);

            IDbDataParameter cmdRecPeriodId = cmd.CreateParameter();
            cmdRecPeriodId.ParameterName = "@RecPeriodID";
            cmdRecPeriodId.Value = recPeriodID;
            cmdParams.Add(cmdRecPeriodId);

            IDbDataParameter cmdCompanyId = cmd.CreateParameter();
            cmdCompanyId.ParameterName = "@CompanyID";
            cmdCompanyId.Value = companyID;
            cmdParams.Add(cmdCompanyId);

            IDbDataParameter cmdUserID = cmd.CreateParameter();
            cmdUserID.ParameterName = "@UserID";
            cmdUserID.Value = userID;
            cmdParams.Add(cmdUserID);

            IDbDataParameter cmdRoleID = cmd.CreateParameter();
            cmdRoleID.ParameterName = "@RoleID";
            cmdRoleID.Value = roleID;
            cmdParams.Add(cmdRoleID);

            IDbDataParameter cmdIsDualReviewEnabled = cmd.CreateParameter();
            cmdIsDualReviewEnabled.ParameterName = "@IsDualReviewEnabled";
            cmdIsDualReviewEnabled.Value = isDualReviewEnabled;
            cmdParams.Add(cmdIsDualReviewEnabled);

            IDbDataParameter cmdIsCertificationEnabled = cmd.CreateParameter();
            cmdIsCertificationEnabled.ParameterName = "@IsCertificationEnabled";
            cmdIsCertificationEnabled.Value = isCertificationEnabled;
            cmdParams.Add(cmdIsCertificationEnabled);

            IDbDataParameter cmdIsMaterialityEnabled = cmd.CreateParameter();
            cmdIsMaterialityEnabled.ParameterName = "@IsMaterialityEnabled";
            cmdIsMaterialityEnabled.Value = isMaterialityEnabled;
            cmdParams.Add(cmdIsMaterialityEnabled);

            IDbDataParameter cmdCertificationTypeId = cmd.CreateParameter();
            cmdCertificationTypeId.ParameterName = "@CertificationTypeId";
            cmdCertificationTypeId.Value = certificationTypeID;
            cmdParams.Add(cmdCertificationTypeId);

            IDbDataParameter cmdPreparerAttributeId = cmd.CreateParameter();
            cmdPreparerAttributeId.ParameterName = "@PreparerAttributeId";
            cmdPreparerAttributeId.Value = preparerAttributeId;
            cmdParams.Add(cmdPreparerAttributeId);

            IDbDataParameter cmdReviewerAttributeID = cmd.CreateParameter();
            cmdReviewerAttributeID.ParameterName = "@ReviewerAttributeID";
            cmdReviewerAttributeID.Value = reviewerAttributeId;
            cmdParams.Add(cmdReviewerAttributeID);

            IDbDataParameter cmdApproverAttributeID = cmd.CreateParameter();
            cmdApproverAttributeID.ParameterName = "@ApproverAttributeID";
            cmdApproverAttributeID.Value = approverAttributeId;
            cmdParams.Add(cmdApproverAttributeID);

            IDbDataParameter cmdBackupPreparerAttributeId = cmd.CreateParameter();
            cmdBackupPreparerAttributeId.ParameterName = "@BackupPreparerAttributeId";
            cmdBackupPreparerAttributeId.Value = backupPreparerAttributeId;
            cmdParams.Add(cmdBackupPreparerAttributeId);

            IDbDataParameter cmdBackupReviewerAttributeID = cmd.CreateParameter();
            cmdBackupReviewerAttributeID.ParameterName = "@BackupReviewerAttributeID";
            cmdBackupReviewerAttributeID.Value = backupReviewerAttributeId;
            cmdParams.Add(cmdBackupReviewerAttributeID);

            IDbDataParameter cmdBackupApproverAttributeID = cmd.CreateParameter();
            cmdBackupApproverAttributeID.ParameterName = "@BackupApproverAttributeID";
            cmdBackupApproverAttributeID.Value = backupApproverAttributeId;
            cmdParams.Add(cmdBackupApproverAttributeID);

            return cmd;
        }

        #endregion

        public List<GLDataHdrInfo> SelectGLDataHdrByGLDataID(long? gLDataID)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_GLHdrByGLDataID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataID";
            par.Value = gLDataID;
            cmdParams.Add(par);

            return this.Select(cmd);
        }

        protected override GLDataHdrInfo MapObject(IDataReader r)
        {
            GLDataHdrInfo oGLDataHdrInfo = base.MapObject(r);
            oGLDataHdrInfo.SubledgerBalanceBCCY = r.GetDecimalValue("SubledgerBalanceBCCY");
            oGLDataHdrInfo.SubledgerBalanceRCCY = r.GetDecimalValue("SubledgerBalanceRCCY");
            return oGLDataHdrInfo;
        }


        internal int? GetCountAttachedDocumentByGLDataID(long? gLDataID)
        {
            System.Data.IDbCommand oCommand = null;
            try
            {
                oCommand = GetCountAttachedDocumentByGLDataIDCommand(gLDataID);
                oCommand.Connection = this.CreateConnection();
                oCommand.Connection.Open();
                int? countDocument = Convert.ToInt32(oCommand.ExecuteScalar());
                return countDocument;
            }
            finally
            {
                if (oCommand != null)
                {
                    if (oCommand.Connection != null && oCommand.Connection.State != ConnectionState.Closed)
                    {
                        oCommand.Connection.Dispose();
                    }
                    oCommand.Dispose();
                }
            }
        }


        internal int? GetCountGLReviewNoteByGLDataID(long? gLDataID)
        {
            System.Data.IDbCommand oCommand = null;
            try
            {
                oCommand = GetCountGLReviewNoteByGLDataIDCommand(gLDataID);
                oCommand.Connection = this.CreateConnection();
                oCommand.Connection.Open();
                int? countDocument = Convert.ToInt32(oCommand.ExecuteScalar());
                return countDocument;
            }
            finally
            {
                if (oCommand != null)
                {
                    if (oCommand.Connection != null && oCommand.Connection.State != ConnectionState.Closed)
                    {
                        oCommand.Connection.Dispose();
                    }
                    oCommand.Dispose();
                }
            }
        }

        public System.Data.IDbCommand GetCountAttachedDocumentByGLDataIDCommand(long? gLDataID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_GET_CountAttachedDocumentByGLDataID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            parGLDataID.Value = gLDataID;
            cmdParams.Add(parGLDataID);

            return cmd;
        }

        public System.Data.IDbCommand GetCountGLReviewNoteByGLDataIDCommand(long? gLDataID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_GET_CountGLReviewNoteByGLDataID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            parGLDataID.Value = gLDataID;
            cmdParams.Add(parGLDataID);

            return cmd;
        }

        public int UpdateRecStatusAndIsSRAByGLDataIDCommand(GLDataHdrInfo oGLDataHdrInfo, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            int intResult = 0;
            IDbCommand oCommand = CreateUpdateRecStatusAndIsSRAByGLDataIDCommand(oGLDataHdrInfo);
            oCommand.Connection = oConnection;
            oCommand.Transaction = oTransaction;
            intResult = oCommand.ExecuteNonQuery();
            return intResult;
        }

        protected System.Data.IDbCommand CreateUpdateRecStatusAndIsSRAByGLDataIDCommand(GLDataHdrInfo oGLDataHdrInfo)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_UPD_RecStatusAndIsSRAByGLDataID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            parGLDataID.Value = oGLDataHdrInfo.GLDataID;
            cmdParams.Add(parGLDataID);


            System.Data.IDbDataParameter parReconciliationStatusID = cmd.CreateParameter();
            parReconciliationStatusID.ParameterName = "@ReconciliationStatusID";
            parReconciliationStatusID.Value = oGLDataHdrInfo.ReconciliationStatusID;
            cmdParams.Add(parReconciliationStatusID);

            System.Data.IDbDataParameter parReconciliationStatusDate = cmd.CreateParameter();
            parReconciliationStatusDate.ParameterName = "@ReconciliationStatusDate";
            parReconciliationStatusDate.Value = oGLDataHdrInfo.ReconciliationStatusDate;
            cmdParams.Add(parReconciliationStatusDate);

            System.Data.IDbDataParameter parIsSystemReconcilied = cmd.CreateParameter();
            parIsSystemReconcilied.ParameterName = "@IsSystemReconcilied";
            if (oGLDataHdrInfo.IsSystemReconcilied != null)
            {
                parIsSystemReconcilied.Value = oGLDataHdrInfo.IsSystemReconcilied;
            }
            else
            {
                parIsSystemReconcilied.Value = DBNull.Value;
            }
            cmdParams.Add(parIsSystemReconcilied);

            IDbDataParameter paramRevisedBy = cmd.CreateParameter();
            paramRevisedBy.ParameterName = "@RevisedBy";
            paramRevisedBy.Value = oGLDataHdrInfo.RevisedBy;
            cmdParams.Add(paramRevisedBy);

            IDbDataParameter paramDateRevised = cmd.CreateParameter();
            paramDateRevised.ParameterName = "@DateRevised";
            paramDateRevised.Value = oGLDataHdrInfo.DateRevised;
            cmdParams.Add(paramDateRevised);


            return cmd;
        }



        public List<GLDataAndAccountHdrInfo> GLDataIDAndRecTemplateIDByAccountIDAndRecPeriodID(long? accountID, int? netAccountID, int? recPeriodID, int? companyID, short? recTemplateAttributeID)
        {
            List<GLDataAndAccountHdrInfo> oGLDataAndAccountHdrInfoCollection = new List<GLDataAndAccountHdrInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateGLDataIDAndRecTemplateIDByAccountIDAndRecPeriodIDCommand(accountID, netAccountID, recPeriodID, companyID, recTemplateAttributeID);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    GLDataAndAccountHdrInfo oGLDataAndAccountHdrInfo = null;
                    oGLDataAndAccountHdrInfo = this.MapForGLDataIDAndRecTemplateID(reader);
                    oGLDataAndAccountHdrInfoCollection.Add(oGLDataAndAccountHdrInfo);
                }
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oGLDataAndAccountHdrInfoCollection;
        }

        private IDbCommand CreateGLDataIDAndRecTemplateIDByAccountIDAndRecPeriodIDCommand(long? accountID, int? netAccountID, int? recPeriodID, int? companyID, short? recTemplateAttributeID)
        {
            IDbCommand cmd = this.CreateCommand("usp_GET_GLDataIDAndRecTemplateIDByAccountIDAndRecPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdAccountID = cmd.CreateParameter();
            cmdAccountID.ParameterName = "@AccountID";

            if (accountID.HasValue && accountID.Value > 0)
            {
                cmdAccountID.Value = accountID;
            }
            else
            {
                cmdAccountID.Value = DBNull.Value;
            }
            cmdParams.Add(cmdAccountID);


            IDbDataParameter cmdNetAccountID = cmd.CreateParameter();
            cmdNetAccountID.ParameterName = "@NetAccountID";
            if (netAccountID.HasValue && netAccountID.Value > 0)
            {
                cmdNetAccountID.Value = netAccountID;
            }
            else
            {
                cmdNetAccountID.Value = DBNull.Value;
            }
            cmdParams.Add(cmdNetAccountID);

            IDbDataParameter cmdRecPeriodId = cmd.CreateParameter();
            cmdRecPeriodId.ParameterName = "@ReconciliationPeriodID";
            if (recPeriodID.HasValue)
            {
                cmdRecPeriodId.Value = recPeriodID;
            }
            else
            {
                cmdRecPeriodId.Value = DBNull.Value;
            }
            cmdParams.Add(cmdRecPeriodId);

            IDbDataParameter cmdCompanyId = cmd.CreateParameter();
            cmdCompanyId.ParameterName = "@CompanyID";
            cmdCompanyId.Value = companyID;
            cmdParams.Add(cmdCompanyId);


            IDbDataParameter cmdRecTemplateAttributeID = cmd.CreateParameter();
            cmdRecTemplateAttributeID.ParameterName = "@RecTemplateAttributeID";
            if (recTemplateAttributeID.HasValue && recTemplateAttributeID.Value > 0)
            {
                cmdRecTemplateAttributeID.Value = recTemplateAttributeID;
            }
            else
            {
                cmdRecTemplateAttributeID.Value = DBNull.Value;
            }

            cmdParams.Add(cmdRecTemplateAttributeID);

            return cmd;
        }

        private GLDataAndAccountHdrInfo MapForGLDataIDAndRecTemplateID(IDataReader reader)
        {
            GLDataAndAccountHdrInfo oGLDataAndAccountHdrInfo = new GLDataAndAccountHdrInfo();

            oGLDataAndAccountHdrInfo.GLDataID = reader.GetInt64Value("GLDataID");
            oGLDataAndAccountHdrInfo.ReconciliationTemplateID = reader.GetInt16Value("ReconciliationTemplateID");
            oGLDataAndAccountHdrInfo.ReconciliationStatusID = reader.GetInt16Value("ReconciliationStatusID");
            oGLDataAndAccountHdrInfo.IsSystemReconcilied = reader.GetBooleanValue("IsSystemReconcilied");

            return oGLDataAndAccountHdrInfo;
        }



        public int UpdateSignOffDates(GLDataHdrInfo oGLDataHdrInfo, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            int intResult = 0;
            IDbCommand oCommand = CreateUpdateSignOffDatesCommand(oGLDataHdrInfo);
            oCommand.Connection = oConnection;
            oCommand.Transaction = oTransaction;
            intResult = oCommand.ExecuteNonQuery();
            return intResult;
        }

        private IDbCommand CreateUpdateSignOffDatesCommand(GLDataHdrInfo entity)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("usp_UPD_SignOffDates");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parApproverSignOffDate = cmd.CreateParameter();
            parApproverSignOffDate.ParameterName = "@ApproverSignOffDate";
            if (!entity.IsApproverSignOffDateNull)
                parApproverSignOffDate.Value = entity.ReconciledStatusDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parApproverSignOffDate.Value = System.DBNull.Value;
            cmdParams.Add(parApproverSignOffDate);

            System.Data.IDbDataParameter parPreparerSignOffDate = cmd.CreateParameter();
            parPreparerSignOffDate.ParameterName = "@PreparerSignOffDate";
            if (!entity.IsPreparerSignOffDateNull)
                parPreparerSignOffDate.Value = entity.PendingReviewStatusDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parPreparerSignOffDate.Value = System.DBNull.Value;
            cmdParams.Add(parPreparerSignOffDate);

            System.Data.IDbDataParameter parReviewerSignOffDate = cmd.CreateParameter();
            parReviewerSignOffDate.ParameterName = "@ReviewerSignOffDate";
            if (!entity.IsReviewerSignOffDateNull)
                parReviewerSignOffDate.Value = entity.PendingApprovalStatusDate.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parReviewerSignOffDate.Value = System.DBNull.Value;
            cmdParams.Add(parReviewerSignOffDate);

            System.Data.IDbDataParameter pkparGLDataID = cmd.CreateParameter();
            pkparGLDataID.ParameterName = "@GLDataID";
            pkparGLDataID.Value = entity.GLDataID;
            cmdParams.Add(pkparGLDataID);

            IDbDataParameter paramRevisedBy = cmd.CreateParameter();
            paramRevisedBy.ParameterName = "@RevisedBy";
            paramRevisedBy.Value = entity.RevisedBy;
            cmdParams.Add(paramRevisedBy);

            return cmd;
        }


        public int UpdateBankFormSupportingDetail(GLDataHdrInfo oGLDataHdrInfo, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            int intResult = 0;
            IDbCommand oCommand = CreateUpdateBankFormSupportingDetailCommand(oGLDataHdrInfo);
            oCommand.Connection = oConnection;
            oCommand.Transaction = oTransaction;
            intResult = oCommand.ExecuteNonQuery();
            return intResult;
        }

        private IDbCommand CreateUpdateBankFormSupportingDetailCommand(GLDataHdrInfo entity)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_UPD_BankFormSupportingDetail");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parBaseCurrencyBalance = cmd.CreateParameter();
            parBaseCurrencyBalance.ParameterName = "@BaseCurrencyBalance";
            if (!entity.IsSupportingDetailBalanceBaseCurrencyNull)
                parBaseCurrencyBalance.Value = entity.SupportingDetailBalanceBaseCurrency;
            else
                parBaseCurrencyBalance.Value = System.DBNull.Value;
            cmdParams.Add(parBaseCurrencyBalance);

            System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
            parGLDataID.ParameterName = "@GLDataID";
            if (!entity.IsGLDataIDNull)
                parGLDataID.Value = entity.GLDataID;
            else
                parGLDataID.Value = System.DBNull.Value;
            cmdParams.Add(parGLDataID);

            System.Data.IDbDataParameter parReportingCurrencyBalance = cmd.CreateParameter();
            parReportingCurrencyBalance.ParameterName = "@ReportingCurrencyBalance";
            if (!entity.IsSupportingDetailBalanceReportingCurrencyNull)
                parReportingCurrencyBalance.Value = entity.SupportingDetailBalanceReportingCurrency;
            else
                parReportingCurrencyBalance.Value = System.DBNull.Value;
            cmdParams.Add(parReportingCurrencyBalance);

            IDbDataParameter paramRevisedBy = cmd.CreateParameter();
            paramRevisedBy.ParameterName = "@RevisedBy";
            paramRevisedBy.Value = entity.RevisedBy;
            cmdParams.Add(paramRevisedBy);

            IDbDataParameter paramDateRevised = cmd.CreateParameter();
            paramDateRevised.ParameterName = "@DateRevised";
            paramDateRevised.Value = entity.DateRevised;
            cmdParams.Add(paramDateRevised);

            return cmd;

        }


        internal List<GLDataHdrInfo> GetAccountInfoForNetAccount(int? NetAccountID, int? RecPeriodID, int? CompanyID, List<short> oAttributeIDCollection, int LCID, int BusinessEntityID, int DefaultLCID)
        {
            List<GLDataHdrInfo> oGLDataHdrInfoCollection = new List<GLDataHdrInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateGetAccountInfoForNetAccountCommand(NetAccountID, RecPeriodID, CompanyID, oAttributeIDCollection, LCID, BusinessEntityID, DefaultLCID);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                Int64 currentAccountHdrID = 0;
                Int64 prevAccountHdrID = 0;
                GLDataHdrInfo oGLDataHdrInfo = null;
                GeographyObjectHdrDAO oGeographyObjectHdrDAO = new GeographyObjectHdrDAO(this.CurrentAppUserInfo);

                while (reader.Read())
                {
                    if (reader.GetInt64Value("AccountID") != null)
                    {
                        currentAccountHdrID = reader.GetInt64Value("AccountID").Value;

                        if (prevAccountHdrID != currentAccountHdrID)
                        {
                            oGLDataHdrInfo = MapAccountObject(reader);
                            oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oGLDataHdrInfo);
                            oGLDataHdrInfoCollection.Add(oGLDataHdrInfo);
                            prevAccountHdrID = currentAccountHdrID;
                        }

                        this.MapAccountAttributeInfo(reader, oGLDataHdrInfo);
                    }
                    else
                    {
                        oGLDataHdrInfo = MapAccountObject(reader);
                        oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oGLDataHdrInfo);
                        oGLDataHdrInfoCollection.Add(oGLDataHdrInfo);
                        prevAccountHdrID = currentAccountHdrID;
                    }
                }
                reader.ClearColumnHash();
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oGLDataHdrInfoCollection;
        }

        private IDbCommand CreateGetAccountInfoForNetAccountCommand(int? NetAccountID, int? RecPeriodID, int? CompanyID, List<short> oAttributeIDCollection, int LCID, int BusinessEntityID, int DefaultLCID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_AccountInfoForNetAccount");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdCompanyID = cmd.CreateParameter();
            cmdCompanyID.ParameterName = "@CompanyID";
            cmdCompanyID.Value = CompanyID.Value;
            cmdParams.Add(cmdCompanyID);

            IDbDataParameter cmdRecPeriodID = cmd.CreateParameter();
            cmdRecPeriodID.ParameterName = "@RecPeriodID";
            cmdRecPeriodID.Value = RecPeriodID.Value;
            cmdParams.Add(cmdRecPeriodID);

            IDbDataParameter parNetAccountID = cmd.CreateParameter();
            parNetAccountID.ParameterName = "@NetAccountID";
            parNetAccountID.Value = NetAccountID.Value;
            cmdParams.Add(parNetAccountID);

            IDbDataParameter cmdAccountAttributeIDCollection = cmd.CreateParameter();
            cmdAccountAttributeIDCollection.ParameterName = "@tblAccountAttributeID";
            cmdAccountAttributeIDCollection.Value = ServiceHelper.ConvertIDCollectionToDataTable(oAttributeIDCollection); ;
            cmdParams.Add(cmdAccountAttributeIDCollection);

            ServiceHelper.AddCommonLanguageParameters(cmd, cmdParams, LCID, BusinessEntityID, DefaultLCID);
            return cmd;
        }

        internal GLDataHdrInfo GetLiteGLDataInfoByGLDataID(long? GLDataID)
        {
            GLDataHdrInfo oGLDataHdrInfo = new GLDataHdrInfo();
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateGetLiteGLDataInfoByGLDataIDCommand(GLDataID);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                reader.Read();
                oGLDataHdrInfo = MapObject(reader);
                reader.ClearColumnHash();
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oGLDataHdrInfo;
        }

        private IDbCommand CreateGetLiteGLDataInfoByGLDataIDCommand(long? GLDataID)
        {
            IDbCommand cmd = this.CreateCommand("usp_GET_LiteGLDataInfo");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdGLDataID = cmd.CreateParameter();
            cmdGLDataID.ParameterName = "@GLDataID";
            cmdGLDataID.Value = GLDataID;
            cmdParams.Add(cmdGLDataID);

            return cmd;
        }

        #region InsertUserGLDataFlag

        public void InsertUserGLDataFlag(UserGLDataFlagInfo oUserGLDataFlagInfo)
        {
            IDbCommand cmd = null;
            IDbConnection con = null;

            try
            {
                cmd = this.CreateInsertUserGLDataFlagCommand(oUserGLDataFlagInfo);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();

                cmd.ExecuteNonQuery();
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }
        }

        private IDbCommand CreateInsertUserGLDataFlagCommand(UserGLDataFlagInfo oUserGLDataFlagInfo)
        {
            IDbCommand cmd = this.CreateCommand("usp_UPD_UserGLDataFlag");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter paramUserID = cmd.CreateParameter();
            paramUserID.ParameterName = "@UserID";
            paramUserID.Value = oUserGLDataFlagInfo.UserID;
            cmdParams.Add(paramUserID);

            IDbDataParameter paramGLDataID = cmd.CreateParameter();
            paramGLDataID.ParameterName = "@GLDataID";
            paramGLDataID.Value = oUserGLDataFlagInfo.GLDataID;
            cmdParams.Add(paramGLDataID);

            IDbDataParameter paramAddedBy = cmd.CreateParameter();
            paramAddedBy.ParameterName = "@AddedBy";
            paramAddedBy.Value = oUserGLDataFlagInfo.AddedBy;
            cmdParams.Add(paramAddedBy);

            IDbDataParameter paramDateAdded = cmd.CreateParameter();
            paramDateAdded.ParameterName = "@DateAdded";
            paramDateAdded.Value = oUserGLDataFlagInfo.DateAdded;
            cmdParams.Add(paramDateAdded);

            return cmd;
        }

        #endregion

        #region DeleteUserGLDataFlag

        public void DeleteUserGLDataFlag(UserGLDataFlagInfo oUserGLDataFlagInfo)
        {
            IDbCommand cmd = null;
            IDbConnection con = null;

            try
            {
                cmd = this.CreateDeleteUserGLDataFlagCommand(oUserGLDataFlagInfo);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();

                cmd.ExecuteNonQuery();
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }
        }

        private IDbCommand CreateDeleteUserGLDataFlagCommand(UserGLDataFlagInfo oUserGLDataFlagInfo)
        {
            IDbCommand cmd = this.CreateCommand("usp_DEL_UserGLDataFlag");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter paramUserID = cmd.CreateParameter();
            paramUserID.ParameterName = "@UserID";
            paramUserID.Value = oUserGLDataFlagInfo.UserID;
            cmdParams.Add(paramUserID);

            IDbDataParameter paramGLDataID = cmd.CreateParameter();
            paramGLDataID.ParameterName = "@GLDataID";
            paramGLDataID.Value = oUserGLDataFlagInfo.GLDataID;
            cmdParams.Add(paramGLDataID);

            IDbDataParameter paramRevisedBy = cmd.CreateParameter();
            paramRevisedBy.ParameterName = "@RevisedBy";
            paramRevisedBy.Value = oUserGLDataFlagInfo.RevisedBy;
            cmdParams.Add(paramRevisedBy);

            IDbDataParameter paramDateRevised = cmd.CreateParameter();
            paramDateRevised.ParameterName = "@DateRevised";
            paramDateRevised.Value = oUserGLDataFlagInfo.DateRevised;
            cmdParams.Add(paramDateRevised);

            return cmd;
        }

        #endregion

        #region GetIsAllAccountsReconciledForUserAndRole

        public bool? GetIsAllAccountsReconciledForUserAndRole(int userID, short roleID, int recPeriodID)
        {
            bool? isAllAccountsReconciled = false;
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateGetIsAllAccountsReconciledForUserAndRoleCommand(userID, roleID, recPeriodID);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    isAllAccountsReconciled = reader.GetBooleanValue("IsAllAccountsReconciled");
                }
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return isAllAccountsReconciled;
        }

        private IDbCommand CreateGetIsAllAccountsReconciledForUserAndRoleCommand(int userID, short roleID, int recPeriodID)
        {
            IDbCommand cmd = this.CreateCommand("usp_GET_IsAllAccountsReconciledForUserAndRole");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdUserID = cmd.CreateParameter();
            cmdUserID.ParameterName = "@UserID";
            cmdUserID.Value = userID;
            cmdParams.Add(cmdUserID);

            IDbDataParameter cmdRoleID = cmd.CreateParameter();
            cmdRoleID.ParameterName = "@RoleID";
            cmdRoleID.Value = roleID;
            cmdParams.Add(cmdRoleID);

            IDbDataParameter cmdRecPeriodID = cmd.CreateParameter();
            cmdRecPeriodID.ParameterName = "@RecPeriodID";
            cmdRecPeriodID.Value = recPeriodID;
            cmdParams.Add(cmdRecPeriodID);

            return cmd;
        }

        #endregion


        #region GetIsAllAccountsReconciledForUserAndRole

        public bool? GetIsAllJuniorsCompletedCertificationForUserRoleAndCertificationType(int userID, short roleID, int recPeriodID, short certificationTypeID)
        {
            bool? isAllJuniorsCompletedCertification = false;
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateGetIsAllJuniorsCompletedCertificationForUserRoleAndCertificationTypeCommand(userID, roleID, recPeriodID, certificationTypeID);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    isAllJuniorsCompletedCertification = reader.GetBooleanValue("IsAllJuniorsCompletedCertification");
                }
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return isAllJuniorsCompletedCertification;
        }

        private IDbCommand CreateGetIsAllJuniorsCompletedCertificationForUserRoleAndCertificationTypeCommand(int userID, short roleID, int recPeriodID, short certificationTypeID)
        {
            IDbCommand cmd = this.CreateCommand("usp_GET_IsAllJuniorsCompletedCertificationForUserRoleAndCertificationType");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdUserID = cmd.CreateParameter();
            cmdUserID.ParameterName = "@UserID";
            cmdUserID.Value = userID;
            cmdParams.Add(cmdUserID);

            IDbDataParameter cmdRoleID = cmd.CreateParameter();
            cmdRoleID.ParameterName = "@RoleID";
            cmdRoleID.Value = roleID;
            cmdParams.Add(cmdRoleID);

            IDbDataParameter cmdRecPeriodID = cmd.CreateParameter();
            cmdRecPeriodID.ParameterName = "@RecPeriodID";
            cmdRecPeriodID.Value = recPeriodID;
            cmdParams.Add(cmdRecPeriodID);

            IDbDataParameter cmdCertificationTypeID = cmd.CreateParameter();
            cmdCertificationTypeID.ParameterName = "@CertificationTypeID";
            cmdCertificationTypeID.Value = certificationTypeID;
            cmdParams.Add(cmdCertificationTypeID);

            return cmd;
        }

        #endregion

        #region SelectGLDataAndAccountInfoByUserIDForSRA

        public List<GLDataHdrInfo> SelectGLDataAndAccountInfoByUserIDForSRA(
                                                                    DataTable dtFilterCriteria
                                                                    , int recPeriodID
                                                                    , int companyID
                                                                    , int userID
                                                                    , short userRoleID
                                                                    , bool isDualReviewEnabled
                                                                    , bool isMaterialityEnabled
                                                                    , short preparerAttributeID
                                                                    , short reviewerAttributeID
                                                                    , short approverAttributeID
                                                                    , short preparerRoleID
                                                                    , short reviewerRoleID
                                                                    , short approverRoleID
                                                                    , short systemAdminRoleID
                                                                    , short CEO_CFORoleID
                                                                    , short skyStemAdminRoleID
                                                                    , int? count
                                                                    , List<Int16> AccountAttributeIDCollection
                                                                    , int languageID
                                                                    , int businessEntityID
                                                                    , int defaultLanguageID
                                                                    , string sortExpression
                                                                    , string sortDirection
                                                                )
        {
            List<GLDataHdrInfo> oGLDataHdrInfoCollection = new List<GLDataHdrInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateSelectGLDataAndAccountInfoByUserIDForSRACommand(dtFilterCriteria, recPeriodID, companyID, userID, userRoleID,
                    isDualReviewEnabled, isMaterialityEnabled, preparerAttributeID, reviewerAttributeID, approverAttributeID
                    , preparerRoleID, reviewerRoleID, approverRoleID, systemAdminRoleID, CEO_CFORoleID, skyStemAdminRoleID,
                     count, AccountAttributeIDCollection, languageID, businessEntityID, defaultLanguageID,
                    sortExpression, sortDirection);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                Int64 currentAccountHdrID = 0;
                Int64 prevAccountHdrID = 0;
                Int32 prevNetAccountID = 0;
                Int32 currentNetAccountID = 0;
                GLDataHdrInfo oGLDataHdrInfo = null;
                GeographyObjectHdrDAO oGeographyObjectHdrDAO = new GeographyObjectHdrDAO(this.CurrentAppUserInfo);
                while (reader.Read())
                {
                    if (reader.GetInt64Value("AccountID") != null)
                    {
                        currentAccountHdrID = reader.GetInt64Value("AccountID").Value;

                        if (prevAccountHdrID != currentAccountHdrID)
                        {
                            oGLDataHdrInfo = oGLDataHdrInfoCollection.Find(gld => gld.AccountID == currentAccountHdrID);

                            if (oGLDataHdrInfo == null)
                            {
                                oGLDataHdrInfo = MapAccountObject(reader);
                                oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oGLDataHdrInfo);
                                oGLDataHdrInfoCollection.Add(oGLDataHdrInfo);
                            }
                            prevAccountHdrID = currentAccountHdrID;
                        }

                        this.MapAccountAttributeInfo(reader, oGLDataHdrInfo);
                        this.MapAccountTaskInfo(reader, oGLDataHdrInfo);
                    }
                    else
                    {
                        if (reader.GetInt32Value("NetAccountID") != null)
                        {
                            currentNetAccountID = reader.GetInt32Value("NetAccountID").Value;

                            if (prevNetAccountID != currentNetAccountID)
                            {
                                oGLDataHdrInfo = oGLDataHdrInfoCollection.Find(gld => gld.NetAccountID == currentNetAccountID);

                                if (oGLDataHdrInfo == null)
                                {
                                    oGLDataHdrInfo = MapAccountObject(reader);
                                    oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oGLDataHdrInfo);
                                    oGLDataHdrInfoCollection.Add(oGLDataHdrInfo);
                                }
                                prevNetAccountID = currentNetAccountID;
                            }

                            this.MapAccountAttributeInfo(reader, oGLDataHdrInfo);
                            this.MapAccountTaskInfo(reader, oGLDataHdrInfo);
                        }
                    }
                }
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oGLDataHdrInfoCollection;
        }

        private IDbCommand CreateSelectGLDataAndAccountInfoByUserIDForSRACommand(DataTable dtFilterCriteria, int recPeriodID, int companyID, int userID, short userRoleID, bool isDualReviewEnabled, bool isMaterialityEnabled, short preparerAttributeID, short reviewerAttributeID, short approverAttributeID, short preparerRoleID, short reviewerRoleID, short approverRoleID, short systemAdminRoleID, short CEO_CFORoleID, short skyStemAdminRoleID
            , int? count, List<short> AccountAttributeIDCollection, int languageID, int businessEntityID, int defaultLanguageID, string sortExpression, string sortDirection)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_GLDataAndAccountInfoByUserIDForSRA");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            if (dtFilterCriteria != null)
            {
                IDbDataParameter cmdFilterCriteriaTableType = cmd.CreateParameter();
                cmdFilterCriteriaTableType.ParameterName = "@FilterCriteriaTableType";
                cmdFilterCriteriaTableType.Value = dtFilterCriteria;
                cmdParams.Add(cmdFilterCriteriaTableType);
            }

            IDbDataParameter cmdRecPeriodId = cmd.CreateParameter();
            cmdRecPeriodId.ParameterName = "@RecPeriodID";
            cmdRecPeriodId.Value = recPeriodID;
            cmdParams.Add(cmdRecPeriodId);

            IDbDataParameter cmdCompanyId = cmd.CreateParameter();
            cmdCompanyId.ParameterName = "@CompanyID";
            cmdCompanyId.Value = companyID;
            cmdParams.Add(cmdCompanyId);

            IDbDataParameter cmdUserId = cmd.CreateParameter();
            cmdUserId.ParameterName = "@UserID";
            cmdUserId.Value = userID;
            cmdParams.Add(cmdUserId);

            IDbDataParameter cmdUserRoleId = cmd.CreateParameter();
            cmdUserRoleId.ParameterName = "@UserRoleID";
            cmdUserRoleId.Value = userRoleID;
            cmdParams.Add(cmdUserRoleId);

            IDbDataParameter cmdIsDualReviewEnabled = cmd.CreateParameter();
            cmdIsDualReviewEnabled.ParameterName = "@IsDualReviewEnabled";
            cmdIsDualReviewEnabled.Value = isDualReviewEnabled;
            cmdParams.Add(cmdIsDualReviewEnabled);

            IDbDataParameter cmdIsMaterialityEnabled = cmd.CreateParameter();
            cmdIsMaterialityEnabled.ParameterName = "@IsMaterialityEnabled";
            cmdIsMaterialityEnabled.Value = isMaterialityEnabled;
            cmdParams.Add(cmdIsMaterialityEnabled);

            IDbDataParameter cmdPreparerAttributeID = cmd.CreateParameter();
            cmdPreparerAttributeID.ParameterName = "@PreparerAttributeID";
            cmdPreparerAttributeID.Value = preparerAttributeID;
            cmdParams.Add(cmdPreparerAttributeID);

            IDbDataParameter cmdReviewerAttributeID = cmd.CreateParameter();
            cmdReviewerAttributeID.ParameterName = "@ReviewerAttributeID";
            cmdReviewerAttributeID.Value = reviewerAttributeID;
            cmdParams.Add(cmdReviewerAttributeID);

            IDbDataParameter cmdApproverAttributeID = cmd.CreateParameter();
            cmdApproverAttributeID.ParameterName = "@ApproverAttributeID";
            cmdApproverAttributeID.Value = approverAttributeID;
            cmdParams.Add(cmdApproverAttributeID);

            IDbDataParameter cmdPreparerRoleID = cmd.CreateParameter();
            cmdPreparerRoleID.ParameterName = "@PreparerRoleID";
            cmdPreparerRoleID.Value = preparerRoleID;
            cmdParams.Add(cmdPreparerRoleID);

            IDbDataParameter cmdReviewerRoleID = cmd.CreateParameter();
            cmdReviewerRoleID.ParameterName = "@ReviewerRoleID";
            cmdReviewerRoleID.Value = reviewerRoleID;
            cmdParams.Add(cmdReviewerRoleID);

            IDbDataParameter cmdApproverRoleID = cmd.CreateParameter();
            cmdApproverRoleID.ParameterName = "@ApproverRoleID";
            cmdApproverRoleID.Value = approverRoleID;
            cmdParams.Add(cmdApproverRoleID);

            IDbDataParameter cmdSystemAdminRoleId = cmd.CreateParameter();
            cmdSystemAdminRoleId.ParameterName = "@SystemAdminRoleId";
            cmdSystemAdminRoleId.Value = systemAdminRoleID;
            cmdParams.Add(cmdSystemAdminRoleId);

            IDbDataParameter cmdCEO_CFORoleID = cmd.CreateParameter();
            cmdCEO_CFORoleID.ParameterName = "@CEO_CFORoleID";
            cmdCEO_CFORoleID.Value = CEO_CFORoleID;
            cmdParams.Add(cmdCEO_CFORoleID);

            IDbDataParameter cmdSkyStemAdminRoleID = cmd.CreateParameter();
            cmdSkyStemAdminRoleID.ParameterName = "@SkyStemAdminRoleID";
            cmdSkyStemAdminRoleID.Value = skyStemAdminRoleID;
            cmdParams.Add(cmdSkyStemAdminRoleID);

            IDbDataParameter cmdCount = cmd.CreateParameter();
            cmdCount.ParameterName = "@Count";
            if (count.HasValue)
            {
                cmdCount.Value = count.Value;
            }
            else
            {
                cmdCount.Value = DBNull.Value;
            }

            IDbDataParameter cmdAccountAttributeIDCollection = cmd.CreateParameter();
            cmdAccountAttributeIDCollection.ParameterName = "@tblAccountAttributeID";
            cmdAccountAttributeIDCollection.Value = ServiceHelper.ConvertIDCollectionToDataTable(AccountAttributeIDCollection); ;
            cmdParams.Add(cmdAccountAttributeIDCollection);

            IDbDataParameter cmdLanguageID = cmd.CreateParameter();
            cmdLanguageID.ParameterName = "@LCID";
            cmdLanguageID.Value = languageID;
            cmdParams.Add(cmdLanguageID);

            IDbDataParameter cmdBusinessEntityID = cmd.CreateParameter();
            cmdBusinessEntityID.ParameterName = "@BusinessEntityID";
            cmdBusinessEntityID.Value = businessEntityID;
            cmdParams.Add(cmdBusinessEntityID);

            IDbDataParameter cmdDefaultLanguageID = cmd.CreateParameter();
            cmdDefaultLanguageID.ParameterName = "@DefaultLCID";
            cmdDefaultLanguageID.Value = defaultLanguageID;
            cmdParams.Add(cmdDefaultLanguageID);

            IDbDataParameter cmdSortExpression = cmd.CreateParameter();
            cmdSortExpression.ParameterName = "@SortExpression";
            cmdSortExpression.Value = sortExpression;
            cmdParams.Add(cmdSortExpression);

            IDbDataParameter cmdSortDirection = cmd.CreateParameter();
            cmdSortDirection.ParameterName = "@SortDirection";
            cmdSortDirection.Value = sortDirection;
            cmdParams.Add(cmdSortDirection);


            cmdParams.Add(cmdCount);

            return cmd;
        }

        #endregion

        #region UpdateGLDataIsSRA

        public bool UpdateGLDataIsSRA(DataTable dtAccountIds, int currentRecPeriodId, bool isSRA, string userLoginID, DateTime dateRevised, IDbConnection con, IDbTransaction oTransaction)
        {
            bool result = false;
            IDbCommand cmd = null;

            try
            {
                cmd = this.CreateUpdateGLDataIsSRACommand(dtAccountIds, currentRecPeriodId, isSRA, userLoginID, dateRevised);
                cmd.Connection = con;
                cmd.Transaction = oTransaction;

                cmd.ExecuteNonQuery();
            }
            finally
            {

            }

            return result;
        }

        private IDbCommand CreateUpdateGLDataIsSRACommand(DataTable dtAccountIds, int currentRecPeriodId, bool isSRA, string userLoginID, DateTime dateRevised)
        {
            IDbCommand cmd = this.CreateCommand("usp_UPD_GLDataIsSRA");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdAccountTable = cmd.CreateParameter();
            cmdAccountTable.ParameterName = "@AccountTable";
            cmdAccountTable.Value = dtAccountIds;
            cmdParams.Add(cmdAccountTable);

            IDbDataParameter cmdRecPeriodID = cmd.CreateParameter();
            cmdRecPeriodID.ParameterName = "@RecPeriodID";
            cmdRecPeriodID.Value = currentRecPeriodId;
            cmdParams.Add(cmdRecPeriodID);

            IDbDataParameter cmdIsSRA = cmd.CreateParameter();
            cmdIsSRA.ParameterName = "@IsSRA";
            cmdIsSRA.Value = isSRA;
            cmdParams.Add(cmdIsSRA);

            IDbDataParameter cmdRevisedBy = cmd.CreateParameter();
            cmdRevisedBy.ParameterName = "@RevisedBy";
            cmdRevisedBy.Value = userLoginID;
            cmdParams.Add(cmdRevisedBy);

            IDbDataParameter cmdDateRevised = cmd.CreateParameter();
            cmdDateRevised.ParameterName = "@DateRevised";
            cmdDateRevised.Value = dateRevised;
            cmdParams.Add(cmdDateRevised);

            return cmd;
        }

        #endregion

        #region UpdateGLDataForRemoveAccountSignOff
        internal void UpdateGLDataForRemoveAccountSignOff(List<long> oAccountIDCollection, List<int> oNetAccountIDCollection, int? RecPeriodID, string RevisedBy, DateTime DateRevised)
        {
            IDbCommand cmd = null;
            IDbConnection con = null;

            try
            {
                cmd = this.CreateUpdateGLDataForRemoveAccountSignOffCommand(oAccountIDCollection, oNetAccountIDCollection, RecPeriodID, RevisedBy, DateRevised);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();

                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Dispose();
            }

        }

        private IDbCommand CreateUpdateGLDataForRemoveAccountSignOffCommand(List<long> oAccountIDCollection, List<int> oNetAccountIDCollection, int? RecPeriodID, string RevisedBy, DateTime DateRevised)
        {
            IDbCommand cmd = this.CreateCommand("usp_UPD_GLDataForRemoveAccountSignOff");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdAccountTable = cmd.CreateParameter();
            cmdAccountTable.ParameterName = "@AccountTable";
            cmdAccountTable.Value = ServiceHelper.ConvertLongIDCollectionToDataTable(oAccountIDCollection);
            cmdParams.Add(cmdAccountTable);

            IDbDataParameter cmdNetAccountTable = cmd.CreateParameter();
            cmdNetAccountTable.ParameterName = "@NetAccountTable";
            cmdNetAccountTable.Value = ServiceHelper.ConvertIDCollectionToDataTable(oNetAccountIDCollection);
            cmdParams.Add(cmdNetAccountTable);

            IDbDataParameter paramRecPeriodID = cmd.CreateParameter();
            paramRecPeriodID.ParameterName = "@RecPeriodID";
            paramRecPeriodID.Value = RecPeriodID.Value;
            cmdParams.Add(paramRecPeriodID);

            IDbDataParameter paramDateRevised = cmd.CreateParameter();
            paramDateRevised.ParameterName = "@DateRevised";
            paramDateRevised.Value = DateRevised;
            cmdParams.Add(paramDateRevised);

            IDbDataParameter paramRevisedBy = cmd.CreateParameter();
            paramRevisedBy.ParameterName = "@RevisedBy";
            paramRevisedBy.Value = RevisedBy;
            cmdParams.Add(paramRevisedBy);

            return cmd;
        }
        #endregion

        public string GetBCCYByGLDataID(long gldataID)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            string Bccy = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.GetBCCYbyGlDataIDCommand(gldataID);
                cmd.Connection = con;
                con.Open();
                object obj = cmd.ExecuteScalar();
                if (obj != null)
                    Bccy = obj.ToString();
                return Bccy;
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }
        }
        private IDbCommand GetBCCYbyGlDataIDCommand(long gldataID)
        {
            IDbCommand cmd = this.CreateCommand("usp_GET_BaseCurrencyByGLDataID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdGldataID = cmd.CreateParameter();
            cmdGldataID.ParameterName = "@GLDataID";
            cmdGldataID.Value = gldataID;
            cmdParams.Add(cmdGldataID);

            return cmd;
        }

        public List<GLDataHdrInfo> GetGLVersionByGLDataID(GLDataParamInfo oGLDataParamInfo)
        {
            List<GLDataHdrInfo> oGLDataHdrInfoCollection = new List<GLDataHdrInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;
            GLDataHdrInfo oGLDataHdrInfo = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateCommand("usp_SEL_GLDataHdrArchiveByGLDataID");
                cmd.Connection = con;
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                IDataParameterCollection cmdParams = cmd.Parameters;
                System.Data.IDbDataParameter par = cmd.CreateParameter();
                par.ParameterName = "@GLDataID";
                par.Value = oGLDataParamInfo.GLDataID.Value;
                cmdParams.Add(par);
                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    oGLDataHdrInfo = MapAccountObject(reader);
                    oGLDataHdrInfoCollection.Add(oGLDataHdrInfo);
                }
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oGLDataHdrInfoCollection;
        }

        #region ReopenAccount
        internal void UpdateReOpenAccount(List<long> oGLDataIDCollection, string RevisedBy, DateTime dateRevised, short actionTypeID, short changeSourceIDSRA, IDbConnection con, IDbTransaction oTransaction)
        {
            IDbCommand cmd = null;
            cmd = this.CreateUpdateReOpenAccount(oGLDataIDCollection, RevisedBy, dateRevised, actionTypeID, changeSourceIDSRA);
            cmd.Connection = con;
            cmd.Transaction = oTransaction;
            cmd.ExecuteNonQuery();

        }

        private IDbCommand CreateUpdateReOpenAccount(List<long> oGLDataIDCollection, string RevisedBy, DateTime DateRevised, short actionTypeID, short changeSourceIDSRA)
        {
            IDbCommand cmd = this.CreateCommand("usp_UPD_ReOpenAccountsByGLDataID");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter paramGLDataIDTable = cmd.CreateParameter();
            paramGLDataIDTable.ParameterName = "@GLDataIDTable";
            paramGLDataIDTable.Value = ServiceHelper.ConvertLongIDCollectionToDataTable(oGLDataIDCollection);
            cmdParams.Add(paramGLDataIDTable);

            IDbDataParameter paramDateRevised = cmd.CreateParameter();
            paramDateRevised.ParameterName = "@DateRevised";
            paramDateRevised.Value = DateRevised;
            cmdParams.Add(paramDateRevised);

            IDbDataParameter paramRevisedBy = cmd.CreateParameter();
            paramRevisedBy.ParameterName = "@RevisedBy";
            paramRevisedBy.Value = RevisedBy;
            cmdParams.Add(paramRevisedBy);

            ServiceHelper.AddCommonParametersForActionTypeID(actionTypeID, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForChangeSourceIDSRA(changeSourceIDSRA, cmd, cmdParams);

            return cmd;
        }
        #endregion

        public bool IsGLDataIDEditable(Int64 glDataID)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateCommand("usp_CHK_IsGLDataIDEditable");
                cmd.Connection = con;
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                IDataParameterCollection cmdParams = cmd.Parameters;

                System.Data.IDbDataParameter parGLDataID = cmd.CreateParameter();
                parGLDataID.ParameterName = "@GLDataID";
                parGLDataID.Value = glDataID;
                cmdParams.Add(parGLDataID);

                System.Data.IDbDataParameter parRetVal = cmd.CreateParameter();
                parRetVal.ParameterName = "@UnReconciledOrNotAvailableGLDataIDCount";
                parRetVal.Direction = ParameterDirection.ReturnValue;
                cmdParams.Add(parRetVal);

                cmd.ExecuteNonQuery();

                int UnReconciledOrNotAvailableGLDataIDCount = parRetVal.Value == DBNull.Value ? 0 : (int)parRetVal.Value;
                if (UnReconciledOrNotAvailableGLDataIDCount > 0)
                    return false;
                else
                    return true;

            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }
        }

        public GLDataHdrInfo GetGLDataHdrInfo(Int64? glDataID, Int32? recPeriodID, Int32? CurrentUserID, Int16? CurrentRoleID)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader reader = null;
            GLDataHdrInfo oGLDataHdrInfo = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.GetGLDataHdrInfoCommand(glDataID, recPeriodID, CurrentUserID, CurrentRoleID);
                cmd.Connection = con;
                con.Open();
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (reader.Read())
                    oGLDataHdrInfo = this.MapObject(reader);
                return oGLDataHdrInfo;
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }
        }

        private IDbCommand GetGLDataHdrInfoCommand(Int64? glDataID, Int32? recPeriodID, Int32? CurrentUserID, Int16? CurrentRoleID)
        {
            IDbCommand cmd = this.CreateCommand("usp_GET_GLDataHdrInfo");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdGldataID = cmd.CreateParameter();
            cmdGldataID.ParameterName = "@GLDataID";
            cmdGldataID.Value = glDataID.Value;
            cmdParams.Add(cmdGldataID);

            IDbDataParameter cmdRecPeriodID = cmd.CreateParameter();
            cmdRecPeriodID.ParameterName = "@RecPeriodID";
            cmdRecPeriodID.Value = recPeriodID.Value;
            cmdParams.Add(cmdRecPeriodID);

            IDbDataParameter cmdCurrentUserID = cmd.CreateParameter();
            cmdCurrentUserID.ParameterName = "@CurrentUserID";
            cmdCurrentUserID.Value = CurrentUserID.Value;
            cmdParams.Add(cmdCurrentUserID);

            IDbDataParameter cmdCurrentRoleID = cmd.CreateParameter();
            cmdCurrentRoleID.ParameterName = "@CurrentRoleID";
            cmdCurrentRoleID.Value = CurrentRoleID.Value;
            cmdParams.Add(cmdCurrentRoleID);



            return cmd;
        }
        #region ResetAccount
        internal void UpdateReSetAccount(List<long> oGLDataIDCollection, string RevisedBy, DateTime dateRevised, short actionTypeID, short changeSourceIDSRA, IDbConnection con, IDbTransaction oTransaction)
        {
            IDbCommand cmd = null;
            cmd = this.CreateUpdateReSetAccount(oGLDataIDCollection, RevisedBy, dateRevised, actionTypeID, changeSourceIDSRA);
            cmd.Connection = con;
            cmd.Transaction = oTransaction;
            cmd.ExecuteNonQuery();

        }

        private IDbCommand CreateUpdateReSetAccount(List<long> oGLDataIDCollection, string RevisedBy, DateTime DateRevised, short actionTypeID, short changeSourceIDSRA)
        {
            IDbCommand cmd = this.CreateCommand("usp_UPD_ReSetAccountsByGLDataID");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter paramGLDataIDTable = cmd.CreateParameter();
            paramGLDataIDTable.ParameterName = "@GLDataIDTable";
            paramGLDataIDTable.Value = ServiceHelper.ConvertLongIDCollectionToDataTable(oGLDataIDCollection);
            cmdParams.Add(paramGLDataIDTable);

            IDbDataParameter paramDateRevised = cmd.CreateParameter();
            paramDateRevised.ParameterName = "@DateRevised";
            paramDateRevised.Value = DateRevised;
            cmdParams.Add(paramDateRevised);

            IDbDataParameter paramRevisedBy = cmd.CreateParameter();
            paramRevisedBy.ParameterName = "@RevisedBy";
            paramRevisedBy.Value = RevisedBy;
            cmdParams.Add(paramRevisedBy);

            ServiceHelper.AddCommonParametersForActionTypeID(actionTypeID, cmd, cmdParams);
            ServiceHelper.AddCommonParametersForChangeSourceIDSRA(actionTypeID, cmd, cmdParams);

            return cmd;
        }
        #endregion

    }//end of class
}//end of namespace



