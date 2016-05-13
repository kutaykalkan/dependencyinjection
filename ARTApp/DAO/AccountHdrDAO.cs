


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.Client.Data;
using System.EnterpriseServices;
using SkyStem.ART.App.Utility;
using System.Data.SqlClient;

namespace SkyStem.ART.App.DAO
{
    public class AccountHdrDAO : AccountHdrDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AccountHdrDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        #region SearchAccount

        /// <summary>
        /// Searches database and selects all accounts which matches the search criteria
        /// </summary>
        /// <param name="oAccountSearchCriteria">object containing all search criteria</param>
        /// <returns>List of accounts which matches the search criteria</returns>
        internal List<AccountHdrInfo> SearchAccount(AccountSearchCriteria oAccountSearchCriteria)
        {
            List<AccountHdrInfo> oAccountHdrInfoCollection = new List<AccountHdrInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateSearchAccountCommand(oAccountSearchCriteria);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                Int64 currentAccountHdrID = 0;
                Int64 prevAccountHdrID = 0;
                AccountHdrInfo oAccountHdrInfo = null;
                GeographyObjectHdrDAO oGeographyObjectHdrDAO = new GeographyObjectHdrDAO(this.CurrentAppUserInfo);
                while (reader.Read())
                {
                    currentAccountHdrID = reader.GetInt64Value("AccountID").Value;

                    if (prevAccountHdrID != currentAccountHdrID)
                    {
                        oAccountHdrInfo = oAccountHdrInfoCollection.Find(ah => ah.AccountID == currentAccountHdrID);
                        if (oAccountHdrInfo == null)
                        {
                            oAccountHdrInfo = MapAccountObject(reader);
                            oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oAccountHdrInfo);
                            oAccountHdrInfoCollection.Add(oAccountHdrInfo);
                        }
                        prevAccountHdrID = currentAccountHdrID;
                    }

                    this.MapAccountAttributeInfo(reader, oAccountHdrInfo);
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

            return oAccountHdrInfoCollection;
        }

        /// <summary>
        /// Creates command to use stored procedure by filling up all the parameters.
        /// </summary>
        /// <param name="oAccountSearchCriteria">object containing all search criteria</param>
        /// <returns>Command which needs to be executed</returns>
        private IDbCommand CreateSearchAccountCommand(AccountSearchCriteria oAccountSearchCriteria)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_SearchAccountResult");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdCompanyId = cmd.CreateParameter();
            cmdCompanyId.ParameterName = "@CompanyID";
            cmdCompanyId.Value = oAccountSearchCriteria.CompanyID;
            cmdParams.Add(cmdCompanyId);

            IDbDataParameter cmdKey2 = cmd.CreateParameter();
            cmdKey2.ParameterName = "@Key2";
            if (oAccountSearchCriteria.Key2 != null)
                cmdKey2.Value = oAccountSearchCriteria.Key2;
            else
                cmdKey2.Value = DBNull.Value;
            cmdParams.Add(cmdKey2);

            IDbDataParameter cmdKey2Value = cmd.CreateParameter();
            cmdKey2Value.ParameterName = "@Key2Value";
            if (oAccountSearchCriteria.Key2Value != null)
                cmdKey2Value.Value = oAccountSearchCriteria.Key2Value;
            else
                cmdKey2Value.Value = DBNull.Value;
            cmdParams.Add(cmdKey2Value);

            IDbDataParameter cmdKey3 = cmd.CreateParameter();
            cmdKey3.ParameterName = "@Key3";
            if (oAccountSearchCriteria.Key3 != null)
                cmdKey3.Value = oAccountSearchCriteria.Key3;
            else
                cmdKey3.Value = DBNull.Value;
            cmdParams.Add(cmdKey3);

            IDbDataParameter cmdKey3Value = cmd.CreateParameter();
            cmdKey3Value.ParameterName = "@Key3Value";
            if (oAccountSearchCriteria.Key3Value != null)
                cmdKey3Value.Value = oAccountSearchCriteria.Key3Value;
            else
                cmdKey3Value.Value = DBNull.Value;
            cmdParams.Add(cmdKey3Value);

            IDbDataParameter cmdKey4 = cmd.CreateParameter();
            cmdKey4.ParameterName = "@Key4";
            if (oAccountSearchCriteria.Key4 != null)
                cmdKey4.Value = oAccountSearchCriteria.Key4;
            else
                cmdKey4.Value = DBNull.Value;
            cmdParams.Add(cmdKey4);

            IDbDataParameter cmdKey4Value = cmd.CreateParameter();
            cmdKey4Value.ParameterName = "@Key4Value";
            if (oAccountSearchCriteria.Key4Value != null)
                cmdKey4Value.Value = oAccountSearchCriteria.Key4Value;
            else
                cmdKey4Value.Value = DBNull.Value;
            cmdParams.Add(cmdKey4Value);

            IDbDataParameter cmdKey5 = cmd.CreateParameter();
            cmdKey5.ParameterName = "@Key5";
            if (oAccountSearchCriteria.Key5 != null)
                cmdKey5.Value = oAccountSearchCriteria.Key5;
            else
                cmdKey5.Value = DBNull.Value;
            cmdParams.Add(cmdKey5);

            IDbDataParameter cmdKey5Value = cmd.CreateParameter();
            cmdKey5Value.ParameterName = "@Key5Value";
            if (oAccountSearchCriteria.Key5Value != null)
                cmdKey5Value.Value = oAccountSearchCriteria.Key5Value;
            else
                cmdKey5Value.Value = DBNull.Value;
            cmdParams.Add(cmdKey5Value);

            IDbDataParameter cmdKey6 = cmd.CreateParameter();
            cmdKey6.ParameterName = "@Key6";
            if (oAccountSearchCriteria.Key6 != null)
                cmdKey6.Value = oAccountSearchCriteria.Key6;
            else
                cmdKey6.Value = DBNull.Value;
            cmdParams.Add(cmdKey6);

            IDbDataParameter cmdKey6Value = cmd.CreateParameter();
            cmdKey6Value.ParameterName = "@Key6Value";
            if (oAccountSearchCriteria.Key6Value != null)
                cmdKey6Value.Value = oAccountSearchCriteria.Key6Value;
            else
                cmdKey6Value.Value = DBNull.Value;
            cmdParams.Add(cmdKey6Value);

            IDbDataParameter cmdKey7 = cmd.CreateParameter();
            cmdKey7.ParameterName = "@Key7";
            if (oAccountSearchCriteria.Key7 != null)
                cmdKey7.Value = oAccountSearchCriteria.Key7;
            else
                cmdKey7.Value = DBNull.Value;
            cmdParams.Add(cmdKey7);

            IDbDataParameter cmdKey7Value = cmd.CreateParameter();
            cmdKey7Value.ParameterName = "@Key7Value";
            if (oAccountSearchCriteria.Key7Value != null)
                cmdKey7Value.Value = oAccountSearchCriteria.Key7Value;
            else
                cmdKey7Value.Value = DBNull.Value;
            cmdParams.Add(cmdKey7Value);

            IDbDataParameter cmdKey8 = cmd.CreateParameter();
            cmdKey8.ParameterName = "@Key8";
            if (oAccountSearchCriteria.Key8 != null)
                cmdKey8.Value = oAccountSearchCriteria.Key8;
            else
                cmdKey8.Value = DBNull.Value;
            cmdParams.Add(cmdKey8);

            IDbDataParameter cmdKey8Value = cmd.CreateParameter();
            cmdKey8Value.ParameterName = "@Key8Value";
            if (oAccountSearchCriteria.Key8Value != null)
                cmdKey8Value.Value = oAccountSearchCriteria.Key8Value;
            else
                cmdKey8Value.Value = DBNull.Value;
            cmdParams.Add(cmdKey8Value);

            IDbDataParameter cmdKey9 = cmd.CreateParameter();
            cmdKey9.ParameterName = "@Key9";
            if (oAccountSearchCriteria.Key9 != null)
                cmdKey9.Value = oAccountSearchCriteria.Key9;
            else
                cmdKey9.Value = DBNull.Value;
            cmdParams.Add(cmdKey9);

            IDbDataParameter cmdKey9Value = cmd.CreateParameter();
            cmdKey9Value.ParameterName = "@Key9Value";
            if (oAccountSearchCriteria.Key9Value != null)
                cmdKey9Value.Value = oAccountSearchCriteria.Key9Value;
            else
                cmdKey9Value.Value = DBNull.Value;
            cmdParams.Add(cmdKey9Value);

            IDbDataParameter cmdFromAccountNumber = cmd.CreateParameter();
            cmdFromAccountNumber.ParameterName = "@FromAccountNumber";
            if (!string.IsNullOrEmpty(oAccountSearchCriteria.FromAccountNumber))
                cmdFromAccountNumber.Value = oAccountSearchCriteria.FromAccountNumber;
            else
                cmdFromAccountNumber.Value = DBNull.Value;
            cmdParams.Add(cmdFromAccountNumber);

            IDbDataParameter cmdToAccountNumber = cmd.CreateParameter();
            cmdToAccountNumber.ParameterName = "@ToAccountNumber";
            if (!string.IsNullOrEmpty(oAccountSearchCriteria.ToAccountNumber))
                cmdToAccountNumber.Value = oAccountSearchCriteria.ToAccountNumber;
            else
                cmdToAccountNumber.Value = DBNull.Value;
            cmdParams.Add(cmdToAccountNumber);

            IDbDataParameter cmdFSCaption = cmd.CreateParameter();
            cmdFSCaption.ParameterName = "@FSCaption";
            if (!string.IsNullOrEmpty(oAccountSearchCriteria.FSCaption))
                cmdFSCaption.Value = oAccountSearchCriteria.FSCaption;
            else
                cmdFSCaption.Value = DBNull.Value;
            cmdParams.Add(cmdFSCaption);

            IDbDataParameter cmdRiskRatingID = cmd.CreateParameter();
            cmdRiskRatingID.ParameterName = "@RiskRatingID";
            if (oAccountSearchCriteria.RiskRatingID != null)
                cmdRiskRatingID.Value = oAccountSearchCriteria.RiskRatingID;
            else
                cmdRiskRatingID.Value = DBNull.Value;
            cmdParams.Add(cmdRiskRatingID);

            IDbDataParameter cmdUserName = cmd.CreateParameter();
            cmdUserName.ParameterName = "@UserName";
            if (!string.IsNullOrEmpty(oAccountSearchCriteria.UserName))
                cmdUserName.Value = oAccountSearchCriteria.UserName;
            else
                cmdUserName.Value = DBNull.Value;
            cmdParams.Add(cmdUserName);

            IDbDataParameter cmdAccountName = cmd.CreateParameter();
            cmdAccountName.ParameterName = "@AccountName";
            if (!string.IsNullOrEmpty(oAccountSearchCriteria.AccountName))
                cmdAccountName.Value = oAccountSearchCriteria.AccountName;
            else
                cmdAccountName.Value = DBNull.Value;
            cmdParams.Add(cmdAccountName);

            IDbDataParameter cmdIsKeyAccount = cmd.CreateParameter();
            cmdIsKeyAccount.ParameterName = "@IsKeyAccount";
            if (oAccountSearchCriteria.IsKeyccount != null)
                cmdIsKeyAccount.Value = oAccountSearchCriteria.IsKeyccount;
            else
                cmdIsKeyAccount.Value = DBNull.Value;
            cmdParams.Add(cmdIsKeyAccount);

            IDbDataParameter cmdIsZeroBalance = cmd.CreateParameter();
            cmdIsZeroBalance.ParameterName = "@IsZeroBalance";
            if (oAccountSearchCriteria.IsZeroBalanceAccount != null)
                cmdIsZeroBalance.Value = oAccountSearchCriteria.IsZeroBalanceAccount;
            else
                cmdIsZeroBalance.Value = DBNull.Value;
            cmdParams.Add(cmdIsZeroBalance);

            IDbDataParameter cmdIsShowOnlyAccountMissingAttributes = cmd.CreateParameter();
            cmdIsShowOnlyAccountMissingAttributes.ParameterName = "@IsShowOnlyAccountMissingAttributes";
            cmdIsShowOnlyAccountMissingAttributes.Value = oAccountSearchCriteria.IsShowOnlyAccountMissingAttributes;
            cmdParams.Add(cmdIsShowOnlyAccountMissingAttributes);

            IDbDataParameter cmdIsShowOnlyAccountMissingBackupOwners = cmd.CreateParameter();
            cmdIsShowOnlyAccountMissingBackupOwners.ParameterName = "@IsShowOnlyAccountMissingBackupOwners";
            cmdIsShowOnlyAccountMissingBackupOwners.Value = oAccountSearchCriteria.IsShowOnlyAccountMissingBackupOwners;
            cmdParams.Add(cmdIsShowOnlyAccountMissingBackupOwners);

            IDbDataParameter cmdCount = cmd.CreateParameter();
            cmdCount.ParameterName = "@Count";
            cmdCount.Value = oAccountSearchCriteria.Count;
            cmdParams.Add(cmdCount);

            IDbDataParameter cmdRecPeriod = cmd.CreateParameter();
            cmdRecPeriod.ParameterName = "@RecPeriodID";
            cmdRecPeriod.Value = oAccountSearchCriteria.ReconciliationPeriodID;
            cmdParams.Add(cmdRecPeriod);

            IDbDataParameter cmdIsDualReviewEnabled = cmd.CreateParameter();
            cmdIsDualReviewEnabled.ParameterName = "@IsDualReviewEnabled";
            cmdIsDualReviewEnabled.Value = oAccountSearchCriteria.IsDualReviewEnabled;
            cmdParams.Add(cmdIsDualReviewEnabled);

            IDbDataParameter cmdIsKeyAccountEnabled = cmd.CreateParameter();
            cmdIsKeyAccountEnabled.ParameterName = "@IsKeyAccountEnabled";
            cmdIsKeyAccountEnabled.Value = oAccountSearchCriteria.IsKeyAccountEnabled;
            cmdParams.Add(cmdIsKeyAccountEnabled);

            IDbDataParameter cmdIsRiskRatingEnabled = cmd.CreateParameter();
            cmdIsRiskRatingEnabled.ParameterName = "@IsRiskRatingEnabled";
            cmdIsRiskRatingEnabled.Value = oAccountSearchCriteria.IsRiskRatingEnabled;
            cmdParams.Add(cmdIsRiskRatingEnabled);

            IDbDataParameter cmdIsZeroBalanceAccountEnabled = cmd.CreateParameter();
            cmdIsZeroBalanceAccountEnabled.ParameterName = "@IsZeroBalanceAccountEnabled";
            cmdIsZeroBalanceAccountEnabled.Value = oAccountSearchCriteria.IsZeroBalanceAccountEnabled;
            cmdParams.Add(cmdIsZeroBalanceAccountEnabled);


            //IsReconcilable
            IDbDataParameter cmdIsReconcilable = cmd.CreateParameter();
            cmdIsReconcilable.ParameterName = "@IsReconcilable";
            if (oAccountSearchCriteria.IsReconcilable.HasValue)
                cmdIsReconcilable.Value = oAccountSearchCriteria.IsReconcilable.Value;
            else
                cmdIsReconcilable.Value = DBNull.Value;
            cmdParams.Add(cmdIsReconcilable);

            IDbDataParameter cmdIsReconcilableEnabled = cmd.CreateParameter();
            cmdIsReconcilableEnabled.ParameterName = "@IsReconcilableEnabled";
            cmdIsReconcilableEnabled.Value = oAccountSearchCriteria.IsReconcilableEnabled;
            cmdParams.Add(cmdIsReconcilableEnabled);

            IDbDataParameter cmdPageID = cmd.CreateParameter();
            cmdPageID.ParameterName = "@PageID";
            cmdPageID.Value = oAccountSearchCriteria.PageID;
            cmdParams.Add(cmdPageID);

            IDbDataParameter cmdKeyAccountAttributeId = cmd.CreateParameter();
            cmdKeyAccountAttributeId.ParameterName = "@KeyAccountAttributeId";
            cmdKeyAccountAttributeId.Value = (short)ARTEnums.AccountAttribute.IsKeyAccount;
            cmdParams.Add(cmdKeyAccountAttributeId);

            IDbDataParameter cmdZeroBalanceAttributeID = cmd.CreateParameter();
            cmdZeroBalanceAttributeID.ParameterName = "@ZeroBalanceAttributeID";
            cmdZeroBalanceAttributeID.Value = (short)ARTEnums.AccountAttribute.IsZeroBalanceAccount;
            cmdParams.Add(cmdZeroBalanceAttributeID);

            IDbDataParameter cmdRiskRatingAttributeId = cmd.CreateParameter();
            cmdRiskRatingAttributeId.ParameterName = "@RiskRatingAttributeId";
            cmdRiskRatingAttributeId.Value = (short)ARTEnums.AccountAttribute.RiskRating;
            cmdParams.Add(cmdRiskRatingAttributeId);

            IDbDataParameter cmdSubledgerSourecAttributeId = cmd.CreateParameter();
            cmdSubledgerSourecAttributeId.ParameterName = "@SubledgerSourecAttributeId";
            cmdSubledgerSourecAttributeId.Value = (short)ARTEnums.AccountAttribute.SubledgerSource;
            cmdParams.Add(cmdSubledgerSourecAttributeId);

            IDbDataParameter cmdPreparerAttributeID = cmd.CreateParameter();
            cmdPreparerAttributeID.ParameterName = "@PreparerAttributeID";
            cmdPreparerAttributeID.Value = (short)ARTEnums.AccountAttribute.Preparer;
            cmdParams.Add(cmdPreparerAttributeID);

            IDbDataParameter cmdReviewerAttributeId = cmd.CreateParameter();
            cmdReviewerAttributeId.ParameterName = "@ReviewerAttributeId";
            cmdReviewerAttributeId.Value = (short)ARTEnums.AccountAttribute.Reviewer;
            cmdParams.Add(cmdReviewerAttributeId);

            IDbDataParameter cmdApproverAttributeID = cmd.CreateParameter();
            cmdApproverAttributeID.ParameterName = "@ApproverAttributeID";
            cmdApproverAttributeID.Value = (short)ARTEnums.AccountAttribute.Approver;
            cmdParams.Add(cmdApproverAttributeID);

            //IsReconcilable
            IDbDataParameter cmdIsReconcilableAttributeId = cmd.CreateParameter();
            cmdIsReconcilableAttributeId.ParameterName = "@IsReconcilableAttributeId";
            cmdIsReconcilableAttributeId.Value = (short)ARTEnums.AccountAttribute.IsReconcilable;
            cmdParams.Add(cmdIsReconcilableAttributeId);

            IDbDataParameter cmdSortExpression = cmd.CreateParameter();
            cmdSortExpression.ParameterName = "@SortExpression";
            if (!string.IsNullOrEmpty(oAccountSearchCriteria.SortExpression))
            {
                cmdSortExpression.Value = oAccountSearchCriteria.SortExpression;
            }
            else
                cmdSortExpression.Value = DBNull.Value;
            cmdParams.Add(cmdSortExpression);

            IDbDataParameter cmdSortDirection = cmd.CreateParameter();
            cmdSortDirection.ParameterName = "@SortDirection";

            if (!string.IsNullOrEmpty(oAccountSearchCriteria.SortDirection))
            {
                cmdSortDirection.Value = oAccountSearchCriteria.SortDirection;
            }
            else
            {
                cmdSortDirection.Value = DBNull.Value;
            }
            cmdParams.Add(cmdSortDirection);

            IDbDataParameter cmdexcludeNetAccount = cmd.CreateParameter();
            cmdexcludeNetAccount.ParameterName = "@ExcludeNetAccount";
            if (oAccountSearchCriteria.ExcludeNetAccount != null)
                cmdexcludeNetAccount.Value = oAccountSearchCriteria.ExcludeNetAccount;
            else
                cmdexcludeNetAccount.Value = DBNull.Value;
            cmdParams.Add(cmdexcludeNetAccount);

            IDbDataParameter cmdRequesterUserID = cmd.CreateParameter();
            cmdRequesterUserID.ParameterName = "@RequesterUserID";
            cmdRequesterUserID.Value = oAccountSearchCriteria.UserID;
            cmdParams.Add(cmdRequesterUserID);

            IDbDataParameter cmdRequesterRoleID = cmd.CreateParameter();
            cmdRequesterRoleID.ParameterName = "@RequesterRoleID";
            cmdRequesterRoleID.Value = oAccountSearchCriteria.UserRoleID;
            cmdParams.Add(cmdRequesterRoleID);

            IDbDataParameter cmdFromDueDays = cmd.CreateParameter();
            cmdFromDueDays.ParameterName = "@FromDueDays";
            if (oAccountSearchCriteria.FromDueDays.HasValue)
                cmdFromDueDays.Value = oAccountSearchCriteria.FromDueDays.Value;
            else
                cmdFromDueDays.Value = DBNull.Value;
            cmdParams.Add(cmdFromDueDays);

            IDbDataParameter cmdToDueDays = cmd.CreateParameter();
            cmdToDueDays.ParameterName = "@ToDueDays";
            if (oAccountSearchCriteria.ToDueDays.HasValue)
                cmdToDueDays.Value = oAccountSearchCriteria.ToDueDays.Value;
            else
                cmdToDueDays.Value = DBNull.Value;
            cmdParams.Add(cmdToDueDays);

            ServiceHelper.AddCommonLanguageParameters(cmd, cmdParams, oAccountSearchCriteria.LCID
                , oAccountSearchCriteria.BusinessEntityID, oAccountSearchCriteria.DefaultLanguageID);

            return cmd;
        }



        public List<AccountHdrInfo> GetAccountByOrganisationalHierarchy(AccountSearchCriteria oAccountSearchCriteria)
        {
            List<AccountHdrInfo> oAccountHdrInfoCollection = new List<AccountHdrInfo>();
            using (IDbConnection con = this.CreateConnection())
            {
                using (IDbCommand cmd = this.SearchAccountResultCommandByOrganisationalHierarchy(oAccountSearchCriteria))
                {
                    cmd.Connection = con;
                    con.Open();
                    IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    AccountHdrInfo oAccountHdrInfo;
                    GeographyObjectHdrDAO oGeographyObjectHdrDAO = new GeographyObjectHdrDAO(this.CurrentAppUserInfo);

                    while (reader.Read())
                    {
                        oAccountHdrInfo = MapAccountObject(reader);
                        oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oAccountHdrInfo);
                        oAccountHdrInfoCollection.Add(oAccountHdrInfo);
                    }
                }
            }
            return oAccountHdrInfoCollection;
        }
        private IDbCommand SearchAccountResultCommandByOrganisationalHierarchy(AccountSearchCriteria oAccountSearchCriteria)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_SearchAccountResultByOrganisationalHiearachy");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdCompanyId = cmd.CreateParameter();
            cmdCompanyId.ParameterName = "@CompanyID";
            cmdCompanyId.Value = oAccountSearchCriteria.CompanyID;
            cmdParams.Add(cmdCompanyId);

            IDbDataParameter cmdKey2 = cmd.CreateParameter();
            cmdKey2.ParameterName = "@Key2";
            if (oAccountSearchCriteria.Key2 != null)
                cmdKey2.Value = oAccountSearchCriteria.Key2;
            else
                cmdKey2.Value = DBNull.Value;
            cmdParams.Add(cmdKey2);


            IDbDataParameter cmdKey2Value = cmd.CreateParameter();
            cmdKey2Value.ParameterName = "@Key2_Value";
            if (oAccountSearchCriteria.Key2Value != null)
                cmdKey2Value.Value = oAccountSearchCriteria.Key2Value;
            else
                cmdKey2Value.Value = DBNull.Value;
            cmdParams.Add(cmdKey2Value);

            ServiceHelper.AddCommonLanguageParameters(cmd, cmdParams, oAccountSearchCriteria.LCID, oAccountSearchCriteria.BusinessEntityID, oAccountSearchCriteria.DefaultLanguageID);

            return cmd;
        }

        private IDbCommand SearchAccountResultOrganisationalHierarchyByUserIDRoleID(int userID, short roleID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_SearchAccountAndGeographyObjectOwnershipAssociation");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdUserID = cmd.CreateParameter();
            cmdUserID.ParameterName = "@UserID";
            cmdUserID.Value = userID;
            cmdParams.Add(cmdUserID);

            IDbDataParameter cmdRoleId = cmd.CreateParameter();
            cmdRoleId.ParameterName = "@RoleID";
            cmdRoleId.Value = roleID;
            cmdParams.Add(cmdRoleId);


            return cmd;
        }

        public List<AccountHdrInfo> GetAccountOrganisationalHierarchyByUserIDRoleID(int userID, short roleID)
        {
            List<AccountHdrInfo> oAccountHdrInfoCollection = new List<AccountHdrInfo>();
            using (IDbConnection con = this.CreateConnection())
            {
                using (IDbCommand cmd = this.SearchAccountResultOrganisationalHierarchyByUserIDRoleID(userID, roleID))
                {
                    cmd.Connection = con;
                    con.Open();

                    IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    AccountHdrInfo oAccountHdrInfo = null;

                    GeographyObjectHdrDAO oGeographyObjectHdrDAO = new GeographyObjectHdrDAO(this.CurrentAppUserInfo);
                    bool? isUserGeographyObject = false;
                    long? currentAccountHdrID = 0;
                    long? prevAccountHdrID = 0;

                    while (reader.Read())
                    {
                        isUserGeographyObject = reader.GetBooleanValue("IsUserGeographyObject");

                        if (isUserGeographyObject.HasValue && isUserGeographyObject.Value)
                        {
                            oAccountHdrInfo = MapAccountObject(reader);
                            oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oAccountHdrInfo);
                            oAccountHdrInfo.KeySize = reader.GetInt32Value("KeySize");
                            oAccountHdrInfoCollection.Add(oAccountHdrInfo);
                        }
                        else
                        {
                            currentAccountHdrID = reader.GetInt64Value("AccountID").Value;

                            if (prevAccountHdrID != currentAccountHdrID)
                            {
                                oAccountHdrInfo = oAccountHdrInfoCollection.Find(ah => ah.AccountID == currentAccountHdrID);
                                if (oAccountHdrInfo == null)
                                {
                                    oAccountHdrInfo = MapAccountObject(reader);
                                    oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oAccountHdrInfo);
                                    oAccountHdrInfoCollection.Add(oAccountHdrInfo);
                                }
                                prevAccountHdrID = currentAccountHdrID;
                            }

                            this.MapAccountAttributeInfo(reader, oAccountHdrInfo);
                        }

                        oAccountHdrInfo.IsUserGeographyObject = isUserGeographyObject;
                    }
                }
            }
            return oAccountHdrInfoCollection;
        }



        /// <summary>
        /// maps search account result to accountHdr object along with organizational hierarchy data
        /// </summary>
        /// <param name="reader">reader object containing information from database</param>
        /// <returns>Account Hdr object</returns>

        private AccountHdrInfo MapObjectWithOrganisationalHierarchyInfo(IDataReader reader)
        {
            AccountHdrInfo oAccountHdrInfo = this.MapAccountObject(reader);


            oAccountHdrInfo.Key2LabelID = reader.GetInt32Value("Key2LabelID");

            oAccountHdrInfo.Key3LabelID = reader.GetInt32Value("Key3LabelID");

            oAccountHdrInfo.Key4LabelID = reader.GetInt32Value("Key4LabelID");

            oAccountHdrInfo.Key5LabelID = reader.GetInt32Value("Key5LabelID");

            oAccountHdrInfo.Key6LabelID = reader.GetInt32Value("Key6LabelID");

            oAccountHdrInfo.Key7LabelID = reader.GetInt32Value("Key7LabelID");

            oAccountHdrInfo.Key8LabelID = reader.GetInt32Value("Key8LabelID");

            oAccountHdrInfo.Key9LabelID = reader.GetInt32Value("Key9LabelID");
            oAccountHdrInfo.FSCaption = reader.GetStringValue("FSCaption");
            oAccountHdrInfo.FSCaptionLabelID = reader.GetInt32Value("FSCaptionLabelID");
            oAccountHdrInfo.KeySize = reader.GetInt32Value("KeySize");
            oAccountHdrInfo.KeySize = reader.GetInt32Value("KeySize");
            oAccountHdrInfo.NetAccount = reader.GetStringValue("NetAccount");
            oAccountHdrInfo.NetAccountLabelID = reader.GetInt32Value("NetAccountLabelID");

            return oAccountHdrInfo;
        }


        private AccountHdrInfo MapAccountObject(IDataReader r)
        {
            AccountHdrInfo entity = new AccountHdrInfo();

            entity.AccountID = r.GetInt64Value("AccountID");
            entity.AccountNumber = r.GetStringValue("AccountNumber");
            entity.AccountName = r.GetStringValue("AccountName");
            entity.AccountNameLabelID = r.GetInt32Value("AccountNameLabelID");
            entity.FSCaptionID = r.GetInt32Value("FSCaptionID");
            entity.NatureOfAccount = r.GetStringValue("NatureOfAccount");
            entity.NatureOfAccountLabelID = r.GetInt32Value("NatureOfAccountLabelID");
            entity.NetAccountID = r.GetInt32Value("NetAccountID");
            entity.AccountTypeID = r.GetInt16Value("AccountTypeID");
            entity.GeographyObjectID = r.GetInt32Value("GeographyObjectID");
            entity.AccountGLBalance = r.GetDecimalValue("GLBalanceReportingCurrency");
            entity.BaseCurrencyCode = r.GetStringValue("BaseCurrencyCode");
            entity.CertificationStatus = r.GetStringValue("CertificationStatus");
            entity.CertificationStatusLabelID = r.GetInt32Value("CertificationStatusLabelID");
            entity.ReconciliationStatus = r.GetStringValue("ReconciliationStatus");
            entity.ReconciliationStatusLabelID = r.GetInt32Value("ReconciliationStatusLabelID");
            entity.AccountMateriality = r.GetStringValue("AccountMateriality");
            entity.AccountMaterialityLabelID = r.GetInt32Value("AccountMaterialityLabelID");
            entity.NumberValue = r.GetInt32Value("NumberValue");
            entity.DateValue = r.GetDateValue("DateValue");
            string accountRecPeriods = r.GetStringValue("AccountRecPeriods");
            if (!String.IsNullOrEmpty(accountRecPeriods))
            {
                string[] recPeriods = accountRecPeriods.Split(',');
                foreach (string str in recPeriods)
                {
                    int i;
                    if (int.TryParse(str, out i))
                    {
                        if (entity.RecPeriodIDCollection == null)
                            entity.RecPeriodIDCollection = new List<int>();
                        entity.RecPeriodIDCollection.Add(i);
                    }
                }
            }
            entity.IsLocked = r.GetBooleanValue("IsLocked");
            entity.ReconciliationStatusID = r.GetInt16Value("ReconciliationStatusID");
            entity.IsSystemReconcilied = r.GetBooleanValue("IsSystemReconcilied");
            entity.PreparerDueDate = r.GetDateValue("PreparerDueDate");
            entity.ReviewerDueDate = r.GetDateValue("ReviewerDueDate");
            entity.ApproverDueDate = r.GetDateValue("ApproverDueDate");
            entity.ChangeTypeLabelID = r.GetInt32Value("ChangeTypeLabelID");
            entity.AccountTypeLabelID = r.GetInt32Value("AccountTypeLabelID");
            entity.PreparerUserID = r.GetInt32Value("PreparerUserID");
            entity.ReviewerUserID = r.GetInt32Value("ReviewerUserID");
            entity.ApproverUserID = r.GetInt32Value("ApproverUserID");
            entity.ActionTypeID = r.GetInt16Value("ActionTypeId");
            entity.CreationPeriodEndDate = r.GetDateValue("CreationPeriodEndDate");
            entity.PreparerLanguageID = r.GetInt32Value("PreparerLanguageID");
            entity.ReviewerLanguageID = r.GetInt32Value("ReviewerLanguageID");
            entity.ApproverLanguageID = r.GetInt32Value("ApproverLanguageID");


            return entity;
        }

        private void MapAccountAttributeInfo(IDataReader reader, AccountHdrInfo oAccountHdrInfo)
        {
            int? accountAttributeId = 0;
            accountAttributeId = reader.GetInt16Value("AccountAttributeID");

            if (accountAttributeId != null && accountAttributeId > 0)
            {
                ARTEnums.AccountAttribute oAccountAttribute = (ARTEnums.AccountAttribute)Enum.Parse(typeof(ARTEnums.AccountAttribute), accountAttributeId.ToString());

                switch (oAccountAttribute)
                {
                    case ARTEnums.AccountAttribute.AccountPolicyURL:
                        oAccountHdrInfo.AccountPolicyUrl = reader.GetStringValue("Value");
                        oAccountHdrInfo.AccountPolicyUrlLabelID = reader.GetInt32Value("ValueLabelID");
                        break;

                    //case ARTEnums.AccountAttribute.AccountType:
                    //    oAccountHdrInfo.AccountTypeID = Convert.ToInt16(reader.GetStringValue("Value"));
                    //    break;

                    case ARTEnums.AccountAttribute.Approver:
                        string approverID = reader.GetStringValue("Value");
                        if (!string.IsNullOrEmpty(approverID))
                        {
                            oAccountHdrInfo.ApproverUserID = Convert.ToInt32(approverID);
                        }
                        break;

                    case ARTEnums.AccountAttribute.BackupApprover:
                        string backupApproverID = reader.GetStringValue("Value");
                        if (!string.IsNullOrEmpty(backupApproverID))
                        {
                            oAccountHdrInfo.BackupApproverUserID = Convert.ToInt32(backupApproverID);
                        }
                        break;

                    case ARTEnums.AccountAttribute.Description:
                        oAccountHdrInfo.Description = reader.GetStringValue("Value");
                        oAccountHdrInfo.DescriptionLabelID = reader.GetInt32Value("ValueLabelID");
                        break;

                    case ARTEnums.AccountAttribute.IsKeyAccount:
                        string isKeyAccount = reader.GetStringValue("Value");
                        if (!string.IsNullOrEmpty(isKeyAccount))
                        {
                            oAccountHdrInfo.IsKeyAccount = (Convert.ToBoolean(isKeyAccount));
                        }
                        break;

                    case ARTEnums.AccountAttribute.IsZeroBalanceAccount:
                        string isZeroBalance = reader.GetStringValue("Value");
                        if (!string.IsNullOrEmpty(isZeroBalance))
                        {
                            oAccountHdrInfo.IsZeroBalance = (Convert.ToBoolean(isZeroBalance));
                        }
                        break;

                    case ARTEnums.AccountAttribute.Preparer:
                        string preparerID = reader.GetStringValue("Value");
                        if (!string.IsNullOrEmpty(preparerID))
                        {
                            oAccountHdrInfo.PreparerUserID = (Convert.ToInt32(preparerID));
                        }
                        break;
                    case ARTEnums.AccountAttribute.BackupPreparer:
                        string backupPreparerID = reader.GetStringValue("Value");
                        if (!string.IsNullOrEmpty(backupPreparerID))
                        {
                            oAccountHdrInfo.BackupPreparerUserID = (Convert.ToInt32(backupPreparerID));
                        }
                        break;

                    case ARTEnums.AccountAttribute.ReconciliationProcedure:
                        oAccountHdrInfo.ReconciliationProcedure = reader.GetStringValue("Value");
                        oAccountHdrInfo.ReconciliationProcedureLabelID = reader.GetInt32Value("ValueLabelID");
                        break;

                    case ARTEnums.AccountAttribute.ReconciliationTemplate:
                        string reconciliationTemplateID = reader.GetStringValue("Value");
                        if (!string.IsNullOrEmpty(reconciliationTemplateID))
                        {
                            oAccountHdrInfo.ReconciliationTemplateID = (Convert.ToInt16(reconciliationTemplateID));
                        }
                        break;

                    case ARTEnums.AccountAttribute.Reviewer:
                        string reviewerID = reader.GetStringValue("Value");
                        if (!string.IsNullOrEmpty(reviewerID))
                        {
                            oAccountHdrInfo.ReviewerUserID = (Convert.ToInt32(reviewerID));
                        }
                        break;
                    case ARTEnums.AccountAttribute.BackupReviewer:
                        string backupReviewerID = reader.GetStringValue("Value");
                        if (!string.IsNullOrEmpty(backupReviewerID))
                        {
                            oAccountHdrInfo.BackupReviewerUserID = (Convert.ToInt32(backupReviewerID));
                        }
                        break;

                    case ARTEnums.AccountAttribute.RiskRating:
                        string riskRatingID = reader.GetStringValue("Value");
                        if (!string.IsNullOrEmpty(riskRatingID))
                        {
                            oAccountHdrInfo.RiskRatingID = (Convert.ToInt16(riskRatingID));
                        }
                        break;

                    case ARTEnums.AccountAttribute.SubledgerSource:
                        string subledgerSourceID = reader.GetStringValue("Value");

                        if (!string.IsNullOrEmpty(subledgerSourceID))
                        {
                            oAccountHdrInfo.SubLedgerSourceID = (Convert.ToInt32(subledgerSourceID));
                        }
                        break;

                    case ARTEnums.AccountAttribute.NetAccount:
                        string netAccountID = reader.GetStringValue("Value");
                        if (!string.IsNullOrEmpty(netAccountID))
                        {
                            oAccountHdrInfo.NetAccountID = (Convert.ToInt16(netAccountID));
                            oAccountHdrInfo.NetAccountLabelID = reader.GetInt32Value("ValueLabelID");
                        }
                        break;

                    case ARTEnums.AccountAttribute.IsExcludeOwnershipForZBA:
                        string isExcludeOwnershipForZBA = reader.GetStringValue("Value");
                        if (!string.IsNullOrEmpty(isExcludeOwnershipForZBA))
                        {
                            oAccountHdrInfo.IsExcludeOwnershipForZBA = (Convert.ToBoolean(isExcludeOwnershipForZBA));
                        }
                        break;
                    //Reconcilable
                    case ARTEnums.AccountAttribute.IsReconcilable:
                        string isReconcilable = reader.GetStringValue("Value");
                        if (!string.IsNullOrEmpty(isReconcilable))
                        {
                            oAccountHdrInfo.IsReconcilable = (Convert.ToBoolean(isReconcilable));
                        }
                        break;
                    //Preparer Due Days
                    case ARTEnums.AccountAttribute.PreparerDueDays:
                        string preparerDueDays = reader.GetStringValue("Value");
                        if (!string.IsNullOrEmpty(preparerDueDays))
                        {
                            oAccountHdrInfo.PreparerDueDays = (Convert.ToInt32(preparerDueDays));
                        }
                        break;
                    //Reviewer Due Days
                    case ARTEnums.AccountAttribute.ReviewerDueDays:
                        string reviewerDueDays = reader.GetStringValue("Value");
                        if (!string.IsNullOrEmpty(reviewerDueDays))
                        {
                            oAccountHdrInfo.ReviewerDueDays = (Convert.ToInt32(reviewerDueDays));
                        }
                        break;
                    //Approver Due Days
                    case ARTEnums.AccountAttribute.ApproverDueDays:
                        string approverDueDays = reader.GetStringValue("Value");
                        if (!string.IsNullOrEmpty(approverDueDays))
                        {
                            oAccountHdrInfo.ApproverDueDays = (Convert.ToInt32(approverDueDays));
                        }
                        break;
                    //case ARTEnums.AccountAttribute.DayType:
                    //    string dayTypeID = reader.GetStringValue("Value");
                    //    if (!string.IsNullOrEmpty(dayTypeID))
                    //    {
                    //        oAccountHdrInfo.DayTypeID = (Convert.ToInt16(dayTypeID));
                    //    }
                    //    break;
                }
            }
        }

        #endregion

        private IDbCommand SearchNetAccountsByIDCommand(int recperiodID, int netAccountID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_NetAccountByID");
            cmd.CommandType = CommandType.StoredProcedure;


            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdrecperiodID = cmd.CreateParameter();
            cmdrecperiodID.ParameterName = "@RecPeriodID";
            cmdrecperiodID.Value = recperiodID;
            cmdParams.Add(cmdrecperiodID);

            IDbDataParameter cmdNetAccountID = cmd.CreateParameter();
            cmdNetAccountID.ParameterName = "@NetAccountID";
            cmdNetAccountID.Value = netAccountID;
            cmdParams.Add(cmdNetAccountID);

            return cmd;
        }

        public List<AccountHdrInfo> GetNetAccountHdrInfoCollectionByNetID(int recperiodID, int netAccountID)
        {
            List<AccountHdrInfo> oAccountHdrInfoCollection = new List<AccountHdrInfo>();
            using (IDbConnection con = this.CreateConnection())
            {
                using (IDbCommand cmd = this.SearchNetAccountsByIDCommand(recperiodID, netAccountID))
                {
                    cmd.Connection = con;
                    con.Open();

                    IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    Int64 currentAccountHdrID = 0;
                    Int64 prevAccountHdrID = 0;
                    AccountHdrInfo oAccountHdrInfo = null;
                    GeographyObjectHdrDAO oGeographyObjectHdrDAO = new GeographyObjectHdrDAO(this.CurrentAppUserInfo);

                    while (reader.Read())
                    {
                        int ordinal = reader.GetOrdinal("AccountID");
                        if (!reader.IsDBNull(ordinal)) currentAccountHdrID = ((System.Int64)(reader.GetValue(ordinal)));

                        if (prevAccountHdrID != currentAccountHdrID)
                        {
                            oAccountHdrInfo = MapAccountObject(reader);
                            oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oAccountHdrInfo);

                            oAccountHdrInfoCollection.Add(oAccountHdrInfo);
                            prevAccountHdrID = currentAccountHdrID;
                        }

                        this.MapAccountAttributeInfo(reader, oAccountHdrInfo);
                    }
                }
            }
            return oAccountHdrInfoCollection;
        }

        #region SaveAccountOwnerships
        /// <summary>
        /// Saves account ownership information in the database
        /// </summary>
        /// <param name="dtAccountHdrTableType">User defined table type</param>
        /// <returns>True/false</returns>
        public bool SaveAccountOwnerships(DataTable dtAccountHdrTableType, int comapnyId, int recPeriodID, bool isDualReviewEnabled, string userLoginID,
            DateTime updateTime, short preparerRoleID, short reviewerRoleID, short approverRoleID, short backupPreparerRoleID, short backupReviewerRoleID,
            short backupApproverRoleID, short actionTypeID)
        {
            bool result = false;
            IDbCommand cmd = null;
            IDbConnection con = null;
            IDbTransaction oTransaction = null;

            try
            {
                using (con = this.CreateConnection())
                {
                    using (cmd = this.CreateCommand("usp_UPD_AccountOwnership"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;

                        IDataParameterCollection cmdParams = cmd.Parameters;

                        IDbDataParameter cmdAccountTable = cmd.CreateParameter();
                        cmdAccountTable.ParameterName = "@AccountTable";
                        cmdAccountTable.Value = dtAccountHdrTableType;
                        cmdParams.Add(cmdAccountTable);

                        IDbDataParameter cmdCompanyID = cmd.CreateParameter();
                        cmdCompanyID.ParameterName = "@CompanyID";
                        cmdCompanyID.Value = comapnyId;
                        cmdParams.Add(cmdCompanyID);

                        IDbDataParameter cmdRecPeriodID = cmd.CreateParameter();
                        cmdRecPeriodID.ParameterName = "@RecPeriodID";
                        cmdRecPeriodID.Value = recPeriodID;
                        cmdParams.Add(cmdRecPeriodID);

                        IDbDataParameter cmdIsDualReviewEnabled = cmd.CreateParameter();
                        cmdIsDualReviewEnabled.ParameterName = "@IsDualReviewEnabled";
                        cmdIsDualReviewEnabled.Value = isDualReviewEnabled;
                        cmdParams.Add(cmdIsDualReviewEnabled);

                        IDbDataParameter cmdUserLoginID = cmd.CreateParameter();
                        cmdUserLoginID.ParameterName = "@UserLoginID";
                        cmdUserLoginID.Value = userLoginID;
                        cmdParams.Add(cmdUserLoginID);

                        IDbDataParameter cmdUpdateTime = cmd.CreateParameter();
                        cmdUpdateTime.ParameterName = "@UpdateTime";
                        cmdUpdateTime.Value = updateTime;
                        cmdParams.Add(cmdUpdateTime);

                        IDbDataParameter cmdPreparerAttributeID = cmd.CreateParameter();
                        cmdPreparerAttributeID.ParameterName = "@PreparerAttributeID";
                        cmdPreparerAttributeID.Value = (short)ARTEnums.AccountAttribute.Preparer;
                        cmdParams.Add(cmdPreparerAttributeID);

                        IDbDataParameter cmdReviewerAttributeId = cmd.CreateParameter();
                        cmdReviewerAttributeId.ParameterName = "@ReviewerAttributeId";
                        cmdReviewerAttributeId.Value = (short)ARTEnums.AccountAttribute.Reviewer;
                        cmdParams.Add(cmdReviewerAttributeId);

                        IDbDataParameter cmdApproverAttributeID = cmd.CreateParameter();
                        cmdApproverAttributeID.ParameterName = "@ApproverAttributeID";
                        cmdApproverAttributeID.Value = (short)ARTEnums.AccountAttribute.Approver;
                        cmdParams.Add(cmdApproverAttributeID);

                        IDbDataParameter cmdPreparerRoleID = cmd.CreateParameter();
                        cmdPreparerRoleID.ParameterName = "@PreparerRoleID";
                        cmdPreparerRoleID.Value = preparerRoleID;
                        cmdParams.Add(cmdPreparerRoleID);

                        IDbDataParameter cmdReviewerRoleId = cmd.CreateParameter();
                        cmdReviewerRoleId.ParameterName = "@ReviewerRoleId";
                        cmdReviewerRoleId.Value = reviewerRoleID;
                        cmdParams.Add(cmdReviewerRoleId);

                        IDbDataParameter cmdApproverRoleID = cmd.CreateParameter();
                        cmdApproverRoleID.ParameterName = "@ApproverRoleID";
                        cmdApproverRoleID.Value = approverRoleID;
                        cmdParams.Add(cmdApproverRoleID);

                        ServiceHelper.AddCommonParametersForActionTypeID(actionTypeID, cmd, cmdParams);

                        #region Backup Roles

                        IDbDataParameter cmdBackupPreparerAttributeID = cmd.CreateParameter();
                        cmdBackupPreparerAttributeID.ParameterName = "@BackupPreparerAttributeID";
                        cmdBackupPreparerAttributeID.Value = (short)ARTEnums.AccountAttribute.BackupPreparer;
                        cmdParams.Add(cmdBackupPreparerAttributeID);

                        IDbDataParameter cmdBackupReviewerAttributeId = cmd.CreateParameter();
                        cmdBackupReviewerAttributeId.ParameterName = "@BackupReviewerAttributeID";
                        cmdBackupReviewerAttributeId.Value = (short)ARTEnums.AccountAttribute.BackupReviewer;
                        cmdParams.Add(cmdBackupReviewerAttributeId);

                        IDbDataParameter cmdBackupApproverAttributeID = cmd.CreateParameter();
                        cmdBackupApproverAttributeID.ParameterName = "@BackupApproverAttributeID";
                        cmdBackupApproverAttributeID.Value = (short)ARTEnums.AccountAttribute.BackupApprover;
                        cmdParams.Add(cmdBackupApproverAttributeID);

                        IDbDataParameter cmdBackupPreparerRoleID = cmd.CreateParameter();
                        cmdBackupPreparerRoleID.ParameterName = "@BackupPreparerRoleID";
                        cmdBackupPreparerRoleID.Value = backupPreparerRoleID;
                        cmdParams.Add(cmdBackupPreparerRoleID);

                        IDbDataParameter cmdBackupReviewerRoleId = cmd.CreateParameter();
                        cmdBackupReviewerRoleId.ParameterName = "@BackupReviewerRoleID";
                        cmdBackupReviewerRoleId.Value = backupReviewerRoleID;
                        cmdParams.Add(cmdBackupReviewerRoleId);

                        IDbDataParameter cmdBackupApproverRoleID = cmd.CreateParameter();
                        cmdBackupApproverRoleID.ParameterName = "@BackupApproverRoleID";
                        cmdBackupApproverRoleID.Value = backupApproverRoleID;
                        cmdParams.Add(cmdBackupApproverRoleID);

                        #endregion
                        con.Open();
                        oTransaction = con.BeginTransaction();
                        cmd.Transaction = oTransaction;

                        int rowsAffected = cmd.ExecuteNonQuery();
                        oTransaction.Commit();
                        if (rowsAffected > 0)
                            result = true;
                    }
                }
            }
            catch (SqlException ex)
            {
                try
                {
                    oTransaction.Rollback();
                }
                catch { }
                ServiceHelper.LogAndThrowGenericSqlException(ex, CurrentAppUserInfo);
            }
            catch (Exception ex)
            {
                try
                {
                    oTransaction.Rollback();
                }
                catch { }
                ServiceHelper.LogAndThrowGenericException(ex, CurrentAppUserInfo);
            }
            return result;
        }

        #endregion

        #region SaveAccountMassUpdate
        //private bool SaveAccountMassUpdate(DataTable dtAccountIds, int companyId, int currentRecPeriodId, short accountAttributeID, string accountAttributeValue, int? accountAttributeLabelID, string userLoginID, DateTime updateTime, IDbConnection con, IDbTransaction oTransaction)
        //{
        //    bool result = false;
        //    IDbCommand cmd = null;

        //    try
        //    {
        //        cmd = this.CreateCommand("usp_UPD_AccountMassUpdate");
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Connection = con;
        //        cmd.Transaction = oTransaction;

        //        IDataParameterCollection cmdParams = cmd.Parameters;

        //        IDbDataParameter cmdAccountTable = cmd.CreateParameter();
        //        cmdAccountTable.ParameterName = "@AccountTable";
        //        cmdAccountTable.Value = dtAccountIds;
        //        cmdParams.Add(cmdAccountTable);

        //        IDbDataParameter cmdCompanyID = cmd.CreateParameter();
        //        cmdCompanyID.ParameterName = "@CompanyID";
        //        cmdCompanyID.Value = companyId;
        //        cmdParams.Add(cmdCompanyID);

        //        IDbDataParameter cmdRecPeriodID = cmd.CreateParameter();
        //        cmdRecPeriodID.ParameterName = "@RecPeriodID";
        //        cmdRecPeriodID.Value = currentRecPeriodId;
        //        cmdParams.Add(cmdRecPeriodID);

        //        IDbDataParameter cmdAccountAttributeId = cmd.CreateParameter();
        //        cmdAccountAttributeId.ParameterName = "@AccountAttributeID";
        //        cmdAccountAttributeId.Value = accountAttributeID;
        //        cmdParams.Add(cmdAccountAttributeId);

        //        IDbDataParameter cmdAccountAttributeValue = cmd.CreateParameter();
        //        cmdAccountAttributeValue.ParameterName = "@AccountAttributeValue";
        //        if (accountAttributeValue == null)
        //        {
        //            cmdAccountAttributeValue.Value = DBNull.Value;
        //        }
        //        else
        //        {
        //            cmdAccountAttributeValue.Value = accountAttributeValue;
        //        }
        //        cmdParams.Add(cmdAccountAttributeValue);

        //        IDbDataParameter cmdAccountAttributeValueLabelID = cmd.CreateParameter();
        //        cmdAccountAttributeValueLabelID.ParameterName = "@AccountAttributeValueLabelID";
        //        if (accountAttributeLabelID.HasValue && accountAttributeLabelID.Value > 0)
        //        {
        //            cmdAccountAttributeValueLabelID.Value = accountAttributeLabelID;
        //        }
        //        else
        //        {
        //            cmdAccountAttributeValueLabelID.Value = DBNull.Value;
        //        }
        //        cmdParams.Add(cmdAccountAttributeValueLabelID);

        //        IDbDataParameter cmdUserLoginID = cmd.CreateParameter();
        //        cmdUserLoginID.ParameterName = "@UserLoginID";
        //        cmdUserLoginID.Value = userLoginID;
        //        cmdParams.Add(cmdUserLoginID);

        //        IDbDataParameter cmdUpdateTime = cmd.CreateParameter();
        //        cmdUpdateTime.ParameterName = "@UpdateTime";
        //        cmdUpdateTime.Value = updateTime;
        //        cmdParams.Add(cmdUpdateTime);


        //        int rowsAffected = cmd.ExecuteNonQuery();

        //        if (rowsAffected > 0)
        //            result = true;
        //    }
        //    finally
        //    {

        //    }

        //    return result;
        //}

        public bool SaveAccountAttributeValue(DataTable dtAccountAttributeValue, int recPeriodID, string userLoginID, DateTime updateTime, short actionTypeID, IDbConnection con, IDbTransaction oTransaction)
        {
            bool result = false;
            IDbCommand cmd = CreateSaveAccountAttributeValueCommand(dtAccountAttributeValue, recPeriodID, userLoginID, updateTime, actionTypeID, con, oTransaction);
            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
                result = true;
            return result;
        }

        private IDbCommand CreateSaveAccountAttributeValueCommand(DataTable dtAccountAttributeValue, int currentRecPeriodId, string userLoginID, DateTime updateTime, short actionTypeID, IDbConnection con, IDbTransaction oTransaction)
        {
            IDbCommand cmd = this.CreateCommand("usp_SAV_AccountAttributeValue");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Transaction = oTransaction;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdAccountTable = cmd.CreateParameter();
            cmdAccountTable.ParameterName = "@udtAccountAttributeValue";
            cmdAccountTable.Value = dtAccountAttributeValue;
            cmdParams.Add(cmdAccountTable);

            ServiceHelper.AddCommonParametersForRecPeriodID(currentRecPeriodId, cmd, cmdParams);

            IDbDataParameter cmdUserLoginID = cmd.CreateParameter();
            cmdUserLoginID.ParameterName = "@ModifiedBy";
            cmdUserLoginID.Value = userLoginID;
            cmdParams.Add(cmdUserLoginID);

            IDbDataParameter cmdUpdateTime = cmd.CreateParameter();
            cmdUpdateTime.ParameterName = "@DateModified";
            cmdUpdateTime.Value = updateTime;
            cmdParams.Add(cmdUpdateTime);

            ServiceHelper.AddCommonParametersForActionTypeID(actionTypeID, cmd, cmdParams);

            return cmd;
        }

        #endregion

        #region GetAccountMateriality

        public bool? GetAccountMateriality(int companyID, long accountID, int recPeriodID, bool isMaterialityEnabled)
        {
            bool? accountMateriality = false;
            using (IDbConnection con = this.CreateConnection())
            {
                using (IDbCommand cmd = this.CreateGetAccountMaterialityCommand(companyID, accountID, recPeriodID, isMaterialityEnabled))
                {
                    cmd.Connection = con;
                    con.Open();
                    IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    while (reader.Read())
                    {
                        accountMateriality = reader.GetBooleanValue("AccountMateriality");
                    }
                }
            }
            return accountMateriality;
        }

        private IDbCommand CreateGetAccountMaterialityCommand(int companyID, long accountID, int recPeriodID, bool isMaterialityEnabled)
        {
            IDbCommand cmd = this.CreateCommand("usp_GET_AccountMaterialityByAccountID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter paramComapnyId = cmd.CreateParameter();
            paramComapnyId.ParameterName = "@CompanyID";
            paramComapnyId.Value = companyID;
            cmdParams.Add(paramComapnyId);

            IDbDataParameter paramAccountID = cmd.CreateParameter();
            paramAccountID.ParameterName = "@AccountID";
            paramAccountID.Value = accountID;
            cmdParams.Add(paramAccountID);

            IDbDataParameter paramRecPeriodID = cmd.CreateParameter();
            paramRecPeriodID.ParameterName = "@RecPeriodID";
            paramRecPeriodID.Value = recPeriodID;
            cmdParams.Add(paramRecPeriodID);

            IDbDataParameter paramIsMaterialityEnabled = cmd.CreateParameter();
            paramIsMaterialityEnabled.ParameterName = "@IsMaterialityEnabled";
            paramIsMaterialityEnabled.Value = isMaterialityEnabled;
            cmdParams.Add(paramIsMaterialityEnabled);

            return cmd;
        }

        #endregion

        //public object UpdateNetAccountHdrDataTable(DataTable oNetAccountDT, IDbConnection connection, IDbTransaction transaction, int CompanyID, int ReconciliationPeriodID, int AccountAttributeID, string AccountAttributeValue, int labelIDValue, string AddedBy)
        //{
        //    try
        //    {
        //        IDbCommand IDBCmmd = this.CreateCommand("usp_UPD_AccountMassUpdate");
        //        IDBCmmd.CommandType = CommandType.StoredProcedure;
        //        IDataParameterCollection cmdParams = IDBCmmd.Parameters;
        //        IDbDataParameter cmdParamAccountAttributValueTable = IDBCmmd.CreateParameter();
        //        IDbDataParameter cmdParamCompanyID = IDBCmmd.CreateParameter();
        //        IDbDataParameter cmdParamRecPeriodID = IDBCmmd.CreateParameter();
        //        IDbDataParameter cmdParamAccountAttributeID = IDBCmmd.CreateParameter();
        //        IDbDataParameter cmdParamAccountAttributeValue = IDBCmmd.CreateParameter();
        //        IDbDataParameter cmdParamUserLoginID = IDBCmmd.CreateParameter();
        //        IDbDataParameter cmdParamUserUpdateTime = IDBCmmd.CreateParameter();
        //        IDbDataParameter cmdParamAccountAttributeValueLabelId = IDBCmmd.CreateParameter();

        //        cmdParamAccountAttributValueTable.ParameterName = "@AccountTable";
        //        cmdParamAccountAttributValueTable.Value = oNetAccountDT;
        //        cmdParams.Add(cmdParamAccountAttributValueTable);

        //        cmdParamCompanyID.ParameterName = "@CompanyID";
        //        cmdParamCompanyID.Value = CompanyID;
        //        cmdParams.Add(cmdParamCompanyID);
        //        cmdParamRecPeriodID.ParameterName = "@RecPeriodID";
        //        cmdParamRecPeriodID.Value = ReconciliationPeriodID;
        //        cmdParams.Add(cmdParamRecPeriodID);
        //        cmdParamAccountAttributeID.ParameterName = "@AccountAttributeID";
        //        cmdParamAccountAttributeID.Value = AccountAttributeID;
        //        cmdParams.Add(cmdParamAccountAttributeID);
        //        cmdParamAccountAttributeValue.ParameterName = "@AccountAttributeValue";
        //        if (!string.IsNullOrEmpty(AccountAttributeValue))
        //        {
        //            cmdParamAccountAttributeValue.Value = AccountAttributeValue;
        //        }
        //        else
        //        {
        //            cmdParamAccountAttributeValue.Value = DBNull.Value;
        //        }
        //        cmdParams.Add(cmdParamAccountAttributeValue);

        //        cmdParamAccountAttributeValueLabelId.ParameterName = "@AccountAttributeValueLabelID";
        //        if (labelIDValue == 0)
        //        {
        //            cmdParamAccountAttributeValueLabelId.Value = DBNull.Value;
        //        }
        //        else
        //        {
        //            cmdParamAccountAttributeValueLabelId.Value = labelIDValue;
        //        }
        //        cmdParams.Add(cmdParamAccountAttributeValueLabelId);

        //        cmdParamUserLoginID.ParameterName = "@UserLoginID";
        //        cmdParamUserLoginID.Value = AddedBy;
        //        cmdParams.Add(cmdParamUserLoginID);

        //        cmdParamUserUpdateTime.ParameterName = "@UpdateTime";
        //        cmdParamUserUpdateTime.Value = DateTime.Now;
        //        cmdParams.Add(cmdParamUserUpdateTime);

        //        IDBCmmd.Connection = connection;
        //        IDBCmmd.Transaction = transaction;
        //        return IDBCmmd.ExecuteScalar();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        #region GetAccountHdrInfoByAccountID

        public AccountHdrInfo GetAccountHdrInfoByAccountID(long accountID, int companyID, int recPeriodID)
        {
            AccountHdrInfo oAccountHdrInfo = null;
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateGetAccountHdrInfoCommand(accountID, companyID, recPeriodID);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                GeographyObjectHdrDAO oGeographyObjectHdrDAO = new GeographyObjectHdrDAO(this.CurrentAppUserInfo);

                Int64 currentAccountHdrID = 0;

                while (reader.Read())
                {
                    //currentAccountHdrID = reader.GetInt64Value("AccountID").Value;

                    if (currentAccountHdrID == 0)
                    {
                        oAccountHdrInfo = MapAccountObject(reader);
                        oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oAccountHdrInfo);

                        currentAccountHdrID += 1;
                    }

                    this.MapAccountAttributeInfo(reader, oAccountHdrInfo);
                }
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oAccountHdrInfo;
        }

        private IDbCommand CreateGetAccountHdrInfoCommand(long? accountID, int companyID, int recPeriodID)
        {
            IDbCommand cmd = this.CreateCommand("usp_GET_AccountHdrInfoByAccountID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter paramAccountID = cmd.CreateParameter();
            paramAccountID.ParameterName = "@AccountID";
            paramAccountID.Value = accountID;
            cmdParams.Add(paramAccountID);

            IDbDataParameter paramComapnyId = cmd.CreateParameter();
            paramComapnyId.ParameterName = "@CompanyID";
            paramComapnyId.Value = companyID;
            cmdParams.Add(paramComapnyId);

            IDbDataParameter paramRecPeriodID = cmd.CreateParameter();
            paramRecPeriodID.ParameterName = "@RecPeriodID";
            paramRecPeriodID.Value = recPeriodID;
            cmdParams.Add(paramRecPeriodID);

            return cmd;
        }

        #endregion

        private object DeleteNetAccountHdrDataTable(DataTable oDeleteNetAccountHdrDataTable, IDbConnection oConnection, IDbTransaction oTransaction, int CompanyID, int ReconciliationPeriodID, int AccountAttributeID)
        {
            IDbCommand IDBCmmd = this.CreateCommand("usp_DEL_NetAccount");
            IDBCmmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = IDBCmmd.Parameters;
            IDbDataParameter cmdParamAccountAttributValueTable = IDBCmmd.CreateParameter();
            IDbDataParameter cmdParamCompanyID = IDBCmmd.CreateParameter();
            IDbDataParameter cmdParamRecPeriodID = IDBCmmd.CreateParameter();
            IDbDataParameter cmdParamAccountAttributeID = IDBCmmd.CreateParameter();
            IDbDataParameter cmdParamAccountAttributeValue = IDBCmmd.CreateParameter();
            IDbDataParameter cmdParamUserLoginID = IDBCmmd.CreateParameter();
            IDbDataParameter cmdParamUserUpdateTime = IDBCmmd.CreateParameter();
            IDbDataParameter cmdParamAccountAttributeValueLabelId = IDBCmmd.CreateParameter();

            cmdParamAccountAttributValueTable.ParameterName = "@NetAccountDeleteTable";
            cmdParamAccountAttributValueTable.Value = oDeleteNetAccountHdrDataTable;
            cmdParams.Add(cmdParamAccountAttributValueTable);

            //cmdParamCompanyID.ParameterName = "@CompanyID";
            //cmdParamCompanyID.Value = CompanyID;
            //cmdParams.Add(cmdParamCompanyID);
            cmdParamRecPeriodID.ParameterName = "@RecPeriodID";
            cmdParamRecPeriodID.Value = ReconciliationPeriodID;
            cmdParams.Add(cmdParamRecPeriodID);
            cmdParamAccountAttributeID.ParameterName = "@AccountAttributeID";
            cmdParamAccountAttributeID.Value = AccountAttributeID;
            cmdParams.Add(cmdParamAccountAttributeID);
            IDBCmmd.Connection = oConnection;
            IDBCmmd.Transaction = oTransaction;
            return IDBCmmd.ExecuteScalar();
        }

        public object UpdateGLDataNetAccountHdrForSRA(int netAccountID, IDbConnection connection, IDbTransaction transaction, int CompanyID, int ReconciliationPeriodID, DateTime recPeriodEnddate, string AddedBy, DateTime dateRevised, short actionTypeID, short changeSourceIDSRA)
        {
            IDbCommand IDBCmmd = this.CreateCommand("usp_UPD_GLDataNetAccountHdrForSRA");
            IDBCmmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = IDBCmmd.Parameters;
            IDbDataParameter cmdParamNetAccountID = IDBCmmd.CreateParameter();
            IDbDataParameter cmdParamCompanyID = IDBCmmd.CreateParameter();
            IDbDataParameter cmdParamRecPeriodID = IDBCmmd.CreateParameter();
            IDbDataParameter cmdParamrecPeriodEnddateValue = IDBCmmd.CreateParameter();
            IDbDataParameter cmdParamAccountAttributeValue = IDBCmmd.CreateParameter();
            IDbDataParameter cmdParamUserLoginID = IDBCmmd.CreateParameter();
            IDbDataParameter cmdParamRevisedPeriodValue = IDBCmmd.CreateParameter();
            IDbDataParameter cmdParamAccountAttributeValueLabelId = IDBCmmd.CreateParameter();
            IDbDataParameter cmdParamMinSumThreshold = IDBCmmd.CreateParameter();
            IDbDataParameter cmdParamMaxSumThreshold = IDBCmmd.CreateParameter();
            cmdParamCompanyID.ParameterName = "@CompanyID";
            cmdParamCompanyID.Value = CompanyID;
            cmdParams.Add(cmdParamCompanyID);

            cmdParamNetAccountID.ParameterName = "@NetAccountID";
            cmdParamNetAccountID.Value = netAccountID;
            cmdParams.Add(cmdParamNetAccountID);

            cmdParamAccountAttributeValue.ParameterName = "@revisedBy";
            cmdParamAccountAttributeValue.Value = AddedBy;
            cmdParams.Add(cmdParamAccountAttributeValue);

            cmdParamRevisedPeriodValue.ParameterName = "@dateRevised";
            cmdParamRevisedPeriodValue.Value = dateRevised.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate(); ;
            cmdParams.Add(cmdParamRevisedPeriodValue);

            cmdParamrecPeriodEnddateValue.ParameterName = "@recPeriodDate";
            cmdParamrecPeriodEnddateValue.Value = recPeriodEnddate.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate(); ;
            cmdParams.Add(cmdParamrecPeriodEnddateValue);

            cmdParamRecPeriodID.ParameterName = "@recPeriodID";
            cmdParamRecPeriodID.Value = ReconciliationPeriodID;
            cmdParams.Add(cmdParamRecPeriodID);

            ServiceHelper.AddCommonParametersForActionTypeID(actionTypeID, IDBCmmd, cmdParams);
            ServiceHelper.AddCommonParametersForChangeSourceIDSRA(changeSourceIDSRA, IDBCmmd, cmdParams);

            IDBCmmd.Connection = connection;
            IDBCmmd.Transaction = transaction;
            return IDBCmmd.ExecuteScalar();
        }

        public object UpdateGLDataHdrForNetAccount(IDbConnection connection, IDbTransaction transaction, int ReconciliationPeriodID, DataTable dtNetAccountID, string AddedBy, DateTime dateAdded, Int16 actionTypeID)
        {
            IDbCommand IDBCmmd = this.CreateCommand("usp_INS_NetAccountRecordInGLdataHdr");
            IDBCmmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = IDBCmmd.Parameters;
            IDbDataParameter cmdParamRecPeriodID = IDBCmmd.CreateParameter();
            IDbDataParameter cmdParamNetAccountID = IDBCmmd.CreateParameter();
            IDbDataParameter cmdParamDataimportValue = IDBCmmd.CreateParameter();
            //IDbDataParameter cmdParamnewGLDataHdrID = IDBCmmd.CreateParameter();
            IDbDataParameter cmdParamdateAdded = IDBCmmd.CreateParameter();
            IDbDataParameter cmdParamaddedBy = IDBCmmd.CreateParameter();

            cmdParamDataimportValue.ParameterName = "@dataImportID";
            cmdParamDataimportValue.Value = System.DBNull.Value;
            cmdParams.Add(cmdParamDataimportValue);

            cmdParamRecPeriodID.ParameterName = "@recPeriodId";
            cmdParamRecPeriodID.Value = ReconciliationPeriodID;
            cmdParams.Add(cmdParamRecPeriodID);

            cmdParamNetAccountID.ParameterName = "@udt_NetAccountID";
            cmdParamNetAccountID.Value = dtNetAccountID;
            cmdParams.Add(cmdParamNetAccountID);

            cmdParamdateAdded.ParameterName = "@dateAdded";
            cmdParamdateAdded.Value = dateAdded;
            cmdParams.Add(cmdParamdateAdded);

            cmdParamaddedBy.ParameterName = "@addedBy";
            cmdParamaddedBy.Value = AddedBy;
            cmdParams.Add(cmdParamaddedBy);

            ServiceHelper.AddCommonParametersForActionTypeID(actionTypeID, IDBCmmd, cmdParams);

            IDBCmmd.Connection = connection;
            IDBCmmd.Transaction = transaction;
            return IDBCmmd.ExecuteScalar();
        }

        public void DeleteNetAccountCollection(List<AccountHdrInfo> oDeleteAccountHdrInfoCollection, int CompanyID, int ReconciliationPeriodID, int AccountAttributeID)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            try
            {
                NetAccountHdrDAO oNetAccountHdrDAO = new NetAccountHdrDAO(this.CurrentAppUserInfo);
                //TODO: validation for number of Licenses to user company.

                using (oConnection = oNetAccountHdrDAO.CreateConnection())
                {
                    oConnection.Open();
                    using (oTransaction = oConnection.BeginTransaction())
                    {
                        DataTable oDeleteNetAccountHdrDataTable = SkyStem.ART.App.Utility.ServiceHelper.ConvertNetAccounthdrtoDataTable(oDeleteAccountHdrInfoCollection);
                        object obj = this.DeleteNetAccountHdrDataTable(oDeleteNetAccountHdrDataTable, oConnection, oTransaction, CompanyID, ReconciliationPeriodID, AccountAttributeID);
                        oTransaction.Commit();
                    }
                }
            }
            catch (SqlException ex)
            {
                try
                {
                    oTransaction.Rollback();
                }
                catch { }
                ServiceHelper.LogAndThrowGenericSqlException(ex, CurrentAppUserInfo);
            }
            catch (Exception ex)
            {
                try
                {
                    oTransaction.Rollback();
                }
                catch { }
                ServiceHelper.LogAndThrowGenericException(ex, CurrentAppUserInfo);
            }
        }

        internal List<AccountHdrInfo> GetAccountHdrInfoListByAccountIDs(DataTable oAccountIDTable, int? recperiodID)
        {
            List<AccountHdrInfo> oAccountHdrInfoCollection = new List<AccountHdrInfo>();
            using (IDbConnection con = this.CreateConnection())
            {
                using (IDbCommand cmd = this.AccountHdrInfoListByAccountIDsCommand(oAccountIDTable, recperiodID))
                {
                    cmd.Connection = con;
                    con.Open();

                    IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    Int64 currentAccountHdrID = 0;
                    Int64 prevAccountHdrID = 0;
                    AccountHdrInfo oAccountHdrInfo = null;
                    GeographyObjectHdrDAO oGeographyObjectHdrDAO = new GeographyObjectHdrDAO(this.CurrentAppUserInfo);

                    while (reader.Read())
                    {
                        int ordinal = reader.GetOrdinal("AccountID");
                        if (!reader.IsDBNull(ordinal)) currentAccountHdrID = ((System.Int64)(reader.GetValue(ordinal)));

                        if (prevAccountHdrID != currentAccountHdrID)
                        {
                            oAccountHdrInfo = MapAccountObject(reader);
                            oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oAccountHdrInfo);

                            oAccountHdrInfoCollection.Add(oAccountHdrInfo);
                            prevAccountHdrID = currentAccountHdrID;
                        }

                        this.MapAccountAttributeInfo(reader, oAccountHdrInfo);
                    }
                }
            }
            return oAccountHdrInfoCollection;
        }
        private IDbCommand AccountHdrInfoListByAccountIDsCommand(DataTable oAccountIDTable, int? recperiodID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_AccountInfoByAccountIDList");
            cmd.CommandType = CommandType.StoredProcedure;


            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdAccountIDTable = cmd.CreateParameter();
            cmdAccountIDTable.ParameterName = "@AccountIDTbl";
            cmdAccountIDTable.Value = oAccountIDTable;
            cmdParams.Add(cmdAccountIDTable);

            IDbDataParameter cmdrecperiodID = cmd.CreateParameter();
            cmdrecperiodID.ParameterName = "@RecPeriodID";
            cmdrecperiodID.Value = recperiodID;
            cmdParams.Add(cmdrecperiodID);
            return cmd;
        }
        internal List<long> GetAccountIDListByUserIDs(DataTable oUserIDTable, int? recperiodID)
        {
            List<long> oAccountIDCollection = new List<long>();
            using (IDbConnection con = this.CreateConnection())
            {
                using (IDbCommand cmd = this.GetAccountIDListByUserIDsCommand(oUserIDTable, recperiodID))
                {
                    cmd.Connection = con;
                    con.Open();
                    IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        long? AccountID = reader.GetInt64Value("AccountID");
                        if (AccountID.HasValue)
                            oAccountIDCollection.Add(AccountID.Value);
                    }
                }
            }
            return oAccountIDCollection;
        }

        private IDbCommand GetAccountIDListByUserIDsCommand(DataTable oUserIDTable, int? recperiodID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_AccessibleAccountIDByUserIDList");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdUserIDTable = cmd.CreateParameter();
            cmdUserIDTable.ParameterName = "@UserIDTbl";
            cmdUserIDTable.Value = oUserIDTable;
            cmdParams.Add(cmdUserIDTable);

            IDbDataParameter cmdrecperiodID = cmd.CreateParameter();
            cmdrecperiodID.ParameterName = "@RecPeriodID";
            cmdrecperiodID.Value = recperiodID;
            cmdParams.Add(cmdrecperiodID);
            return cmd;
        }

        internal List<AccountHdrInfo> GetAccountInformationWithoutGL(int? UserID, short? RoleID, int RecPeriodID)
        {
            List<AccountHdrInfo> oAccountHdrInfoCollection = new List<AccountHdrInfo>();
            using (IDbConnection con = this.CreateConnection())
            {
                using (IDbCommand cmd = this.AccountHdrInfoListWithOutGlDataCommand(UserID, RoleID, RecPeriodID))
                {
                    cmd.Connection = con;
                    con.Open();

                    IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    Int64 currentAccountHdrID = 0;
                    Int64 prevAccountHdrID = 0;
                    AccountHdrInfo oAccountHdrInfo = null;
                    GeographyObjectHdrDAO oGeographyObjectHdrDAO = new GeographyObjectHdrDAO(this.CurrentAppUserInfo);

                    while (reader.Read())
                    {
                        int ordinal = reader.GetOrdinal("AccountID");
                        if (!reader.IsDBNull(ordinal)) currentAccountHdrID = ((System.Int64)(reader.GetValue(ordinal)));

                        if (prevAccountHdrID != currentAccountHdrID)
                        {
                            oAccountHdrInfo = MapAccountObject(reader);
                            oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oAccountHdrInfo);

                            oAccountHdrInfoCollection.Add(oAccountHdrInfo);
                            prevAccountHdrID = currentAccountHdrID;
                        }

                        this.MapAccountAttributeInfo(reader, oAccountHdrInfo);
                    }
                }
            }
            return oAccountHdrInfoCollection;
        }

        private IDbCommand AccountHdrInfoListWithOutGlDataCommand(int? UserID, short? RoleID, int RecPeriodID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SRV_SEL_AccountsDetailsForWhichGLDataNotUploadedByUserIDAndRoleID");
            cmd.CommandType = CommandType.StoredProcedure;


            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdUserID = cmd.CreateParameter();
            cmdUserID.ParameterName = "@UserID";
            cmdUserID.Value = UserID;
            cmdParams.Add(cmdUserID);

            IDbDataParameter cmdUserRoleID = cmd.CreateParameter();
            cmdUserRoleID.ParameterName = "@RoleID";
            cmdUserRoleID.Value = RoleID;
            cmdParams.Add(cmdUserRoleID);

            IDbDataParameter cmdrecperiodID = cmd.CreateParameter();
            cmdrecperiodID.ParameterName = "@RecPeriodID";
            cmdrecperiodID.Value = RecPeriodID;
            cmdParams.Add(cmdrecperiodID);
            return cmd;
        }

        internal AccountAttributeWarningInfo GetAccountAttributeChangeWarning(DataTable dtActIDNetActID, DataTable dtAccountAttributeValue, int RecPeriodID)
        {
            AccountAttributeWarningInfo oAccountAttributeWarningInfo = new AccountAttributeWarningInfo();
            using (IDbConnection con = this.CreateConnection())
            {
                using (IDbCommand cmd = this.GetAccountAttributeChangeWarningCommand(dtActIDNetActID, dtAccountAttributeValue, RecPeriodID))
                {
                    cmd.Connection = con;
                    con.Open();
                    IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        oAccountAttributeWarningInfo.FutureNetAccountWarning = reader.GetBooleanValue("FutureNetAccountWarning");
                        oAccountAttributeWarningInfo.LossOfWorkWarning = reader.GetBooleanValue("LossOfWorkWarning");
                    }
                }
            }
            return oAccountAttributeWarningInfo;
        }

        private IDbCommand GetAccountAttributeChangeWarningCommand(DataTable dtActIDNetActID, DataTable dtAccountAttributeValue, int RecPeriodID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_AccountAttributeChangeWarning");
            cmd.CommandType = CommandType.StoredProcedure;


            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdAccountIdNetAccountIDTbl = cmd.CreateParameter();
            cmdAccountIdNetAccountIDTbl.ParameterName = "@AccountIdNetAccountIDTbl";
            cmdAccountIdNetAccountIDTbl.Value = dtActIDNetActID;
            cmdParams.Add(cmdAccountIdNetAccountIDTbl);

            IDbDataParameter cmdAccountAttributeTable = cmd.CreateParameter();
            cmdAccountAttributeTable.ParameterName = "@udtAccountAttributeValue";
            cmdAccountAttributeTable.Value = dtAccountAttributeValue;
            cmdParams.Add(cmdAccountAttributeTable);

            IDbDataParameter cmdrecperiodID = cmd.CreateParameter();
            cmdrecperiodID.ParameterName = "@RecPeriodID";
            cmdrecperiodID.Value = RecPeriodID;
            cmdParams.Add(cmdrecperiodID);
            return cmd;
        }
        internal List<AccountHdrInfo> GetNewAccounts(int? DataImportID)
        {
            List<AccountHdrInfo> oAccountHdrInfoCollection = new List<AccountHdrInfo>();
            using (IDbConnection con = this.CreateConnection())
            {
                using (IDbCommand cmd = this.GetGetNewAccountsCommand(DataImportID))
                {
                    cmd.Connection = con;
                    con.Open();

                    IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    Int64 currentAccountHdrID = 0;
                    Int64 prevAccountHdrID = 0;
                    AccountHdrInfo oAccountHdrInfo = null;
                    GeographyObjectHdrDAO oGeographyObjectHdrDAO = new GeographyObjectHdrDAO(this.CurrentAppUserInfo);

                    while (reader.Read())
                    {
                        int ordinal = reader.GetOrdinal("AccountID");
                        if (!reader.IsDBNull(ordinal)) currentAccountHdrID = ((System.Int64)(reader.GetValue(ordinal)));

                        if (prevAccountHdrID != currentAccountHdrID)
                        {
                            oAccountHdrInfo = MapAccountObject(reader);
                            oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oAccountHdrInfo);

                            oAccountHdrInfoCollection.Add(oAccountHdrInfo);
                            prevAccountHdrID = currentAccountHdrID;
                        }

                        this.MapAccountAttributeInfo(reader, oAccountHdrInfo);
                    }
                }
            }
            return oAccountHdrInfoCollection;
        }

        private IDbCommand GetGetNewAccountsCommand(int? DataImportID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SRV_SEL_NewAccounts");
            cmd.CommandType = CommandType.StoredProcedure;


            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdDataImportID = cmd.CreateParameter();
            cmdDataImportID.ParameterName = "@DataImportID";
            cmdDataImportID.Value = DataImportID.GetValueOrDefault();
            cmdParams.Add(cmdDataImportID);

            return cmd;
        }
        internal List<AccountHdrInfo> GetAccountInformationForCompanyAlertMail(int RecPeriodID, int UserID, short RoleID, long CompanyAlertDetailID)
        {
            List<AccountHdrInfo> oAccountHdrInfoCollection = new List<AccountHdrInfo>();
            using (IDbConnection con = this.CreateConnection())
            {
                using (IDbCommand cmd = this.GetAccountInformationForCompanyAlertMailCommand(RecPeriodID, UserID, RoleID, CompanyAlertDetailID))
                {
                    cmd.Connection = con;
                    con.Open();

                    IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    Int64 currentAccountHdrID = 0;
                    Int64 prevAccountHdrID = 0;
                    AccountHdrInfo oAccountHdrInfo = null;
                    GeographyObjectHdrDAO oGeographyObjectHdrDAO = new GeographyObjectHdrDAO(this.CurrentAppUserInfo);

                    while (reader.Read())
                    {
                        int ordinal = reader.GetOrdinal("AccountID");
                        if (!reader.IsDBNull(ordinal)) currentAccountHdrID = ((System.Int64)(reader.GetValue(ordinal)));

                        if (prevAccountHdrID != currentAccountHdrID)
                        {
                            oAccountHdrInfo = MapAccountObject(reader);
                            oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oAccountHdrInfo);

                            oAccountHdrInfoCollection.Add(oAccountHdrInfo);
                            prevAccountHdrID = currentAccountHdrID;
                        }

                        this.MapAccountAttributeInfo(reader, oAccountHdrInfo);
                    }
                }
            }
            return oAccountHdrInfoCollection;
        }
        private IDbCommand GetAccountInformationForCompanyAlertMailCommand(int RecPeriodID, int UserID, short RoleID, long CompanyAlertDetailID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_AccountInfoByCompanyAlertDetailID");
            cmd.CommandType = CommandType.StoredProcedure;


            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdrecperiodID = cmd.CreateParameter();
            cmdrecperiodID.ParameterName = "@RecPeriodID";
            cmdrecperiodID.Value = RecPeriodID;
            cmdParams.Add(cmdrecperiodID);

            IDbDataParameter cmdCompanyAlertDetailID = cmd.CreateParameter();
            cmdCompanyAlertDetailID.ParameterName = "@CompanyAlertDetailID";
            cmdCompanyAlertDetailID.Value = CompanyAlertDetailID;
            cmdParams.Add(cmdCompanyAlertDetailID);

            IDbDataParameter cmdUserID = cmd.CreateParameter();
            cmdUserID.ParameterName = "@UserID";
            cmdUserID.Value = UserID;
            cmdParams.Add(cmdUserID);

            IDbDataParameter cmdUserRoleID = cmd.CreateParameter();
            cmdUserRoleID.ParameterName = "@RoleID";
            cmdUserRoleID.Value = RoleID;
            cmdParams.Add(cmdUserRoleID);


            return cmd;
        }


        internal List<AccountHdrInfo> GetAccountInformationForAlertMail(DataTable oAccountIDTable, DataTable oNetAccountIDTable, int RecPeriodID, short? AlertID, short? RoleID)
        {
            List<AccountHdrInfo> oAccountHdrInfoCollection = new List<AccountHdrInfo>();
            using (IDbConnection con = this.CreateConnection())
            {
                using (IDbCommand cmd = this.GetAccountInformationForAlertMailCommand(oAccountIDTable, oNetAccountIDTable, RecPeriodID, AlertID, RoleID))
                {
                    cmd.Connection = con;
                    con.Open();
                    IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    AccountHdrInfo oAccountHdrInfo = null;
                    GeographyObjectHdrDAO oGeographyObjectHdrDAO = new GeographyObjectHdrDAO(this.CurrentAppUserInfo);

                    while (reader.Read())
                    {
                        oAccountHdrInfo = MapAccountObject(reader);
                        oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oAccountHdrInfo);
                        MapAccountOwners(reader, oAccountHdrInfo);
                        oAccountHdrInfoCollection.Add(oAccountHdrInfo);
                    }
                }
            }
            return oAccountHdrInfoCollection;
        }

        private static void MapAccountOwners(IDataReader reader, AccountHdrInfo oAccountHdrInfo)
        {
            string preparerID = reader.GetStringValue("Preparer");
            if (!string.IsNullOrEmpty(preparerID))
            {
                oAccountHdrInfo.PreparerUserID = (Convert.ToInt32(preparerID));
            }

            string backupPreparerID = reader.GetStringValue("BackupPreparer");
            if (!string.IsNullOrEmpty(backupPreparerID))
            {
                oAccountHdrInfo.BackupPreparerUserID = (Convert.ToInt32(backupPreparerID));
            }
            string ReviewerID = reader.GetStringValue("Reviewer");
            if (!string.IsNullOrEmpty(ReviewerID))
            {
                oAccountHdrInfo.ReviewerUserID = (Convert.ToInt32(ReviewerID));
            }

            string backupReviewerID = reader.GetStringValue("BackupReviewer");
            if (!string.IsNullOrEmpty(backupReviewerID))
            {
                oAccountHdrInfo.BackupReviewerUserID = (Convert.ToInt32(backupReviewerID));
            }

            string ApproverID = reader.GetStringValue("Approver");
            if (!string.IsNullOrEmpty(ApproverID))
            {
                oAccountHdrInfo.ApproverUserID = (Convert.ToInt32(ApproverID));
            }

            string backupApproverID = reader.GetStringValue("BackupApprover");
            if (!string.IsNullOrEmpty(backupApproverID))
            {
                oAccountHdrInfo.BackupApproverUserID = (Convert.ToInt32(backupApproverID));
            }
        }
        private IDbCommand GetAccountInformationForAlertMailCommand(DataTable oAccountIDTable, DataTable oNetAccountIDTable, int RecPeriodID, short? AlertID, short? RoleID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_AccountInfoByAccountIDsAndNetAccountIDs");
            cmd.CommandType = CommandType.StoredProcedure;


            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdrecperiodID = cmd.CreateParameter();
            cmdrecperiodID.ParameterName = "@RecPeriodID";
            cmdrecperiodID.Value = RecPeriodID;
            cmdParams.Add(cmdrecperiodID);

            IDbDataParameter cmdAccountIDTable = cmd.CreateParameter();
            cmdAccountIDTable.ParameterName = "@AccountIDTbl";
            cmdAccountIDTable.Value = oAccountIDTable;
            cmdParams.Add(cmdAccountIDTable);

            IDbDataParameter cmdNetAccountIDTbl = cmd.CreateParameter();
            cmdNetAccountIDTbl.ParameterName = "@NetAccountIDTbl";
            cmdNetAccountIDTbl.Value = oNetAccountIDTable;
            cmdParams.Add(cmdNetAccountIDTbl);
            IDbDataParameter cmdAlertID = cmd.CreateParameter();
            cmdAlertID.ParameterName = "@AlertID";
            if (AlertID.HasValue)
                cmdAlertID.Value = AlertID.Value;
            else
                cmdAlertID.Value = DBNull.Value;
            cmdParams.Add(cmdAlertID);
            IDbDataParameter cmdRoleID = cmd.CreateParameter();
            cmdRoleID.ParameterName = "@RoleID";
            if (RoleID.HasValue)
                cmdRoleID.Value = RoleID.Value;
            else
                cmdRoleID.Value = DBNull.Value;

            cmdParams.Add(cmdRoleID);


            return cmd;
        }

        internal List<AccountHdrInfo> GetAccountHdrInfo(DataTable oAccountIDTable)
        {
            List<AccountHdrInfo> oAccountHdrInfoCollection = new List<AccountHdrInfo>();
            using (IDbConnection con = this.CreateConnection())
            {
                using (IDbCommand cmd = this.GetAccountHdrInfoCommand(oAccountIDTable))
                {
                    cmd.Connection = con;
                    con.Open();

                    IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    Int64 currentAccountHdrID = 0;
                    Int64 prevAccountHdrID = 0;
                    AccountHdrInfo oAccountHdrInfo = null;
                    GeographyObjectHdrDAO oGeographyObjectHdrDAO = new GeographyObjectHdrDAO(this.CurrentAppUserInfo);

                    while (reader.Read())
                    {
                        int ordinal = reader.GetOrdinal("AccountID");
                        if (!reader.IsDBNull(ordinal)) currentAccountHdrID = ((System.Int64)(reader.GetValue(ordinal)));

                        if (prevAccountHdrID != currentAccountHdrID)
                        {
                            oAccountHdrInfo = MapAccountObject(reader);
                            oGeographyObjectHdrDAO.MapObjectWithOrganisationalHierarchyInfo(reader, oAccountHdrInfo);

                            oAccountHdrInfoCollection.Add(oAccountHdrInfo);
                            prevAccountHdrID = currentAccountHdrID;
                        }

                        this.MapAccountAttributeInfo(reader, oAccountHdrInfo);
                    }
                }
            }
            return oAccountHdrInfoCollection;
        }

        private IDbCommand GetAccountHdrInfoCommand(DataTable oAccountIDTable)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_Accounts");
            cmd.CommandType = CommandType.StoredProcedure;


            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdAccountIDTable = cmd.CreateParameter();
            cmdAccountIDTable.ParameterName = "@AccountIDTbl";
            cmdAccountIDTable.Value = oAccountIDTable;
            cmdParams.Add(cmdAccountIDTable);

            return cmd;
        }

        private IDbCommand GetAccountsHaveDiffrentAttributeValueInFutureCommand(DataTable oAccountIDTable, int? recperiodID)
        {
            IDbCommand cmd = this.CreateCommand("usp_CHK_AccountsHaveDiffrentAttributeValueInFuture");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            IDbDataParameter cmdudtAccountID = cmd.CreateParameter();
            cmdudtAccountID.ParameterName = "@udtAccountID";
            cmdudtAccountID.Value = oAccountIDTable;
            cmdParams.Add(cmdudtAccountID);
            IDbDataParameter cmdrecperiodID = cmd.CreateParameter();
            cmdrecperiodID.ParameterName = "@RecPeriodID";
            if (recperiodID.HasValue)
                cmdrecperiodID.Value = recperiodID.Value;
            else
                cmdrecperiodID.Value = DBNull.Value;
            cmdParams.Add(cmdrecperiodID);
            return cmd;
        }

        public List<long> GetAccountsHaveDiffrentAttributeValueInFuture(DataTable oAccountIDTable, int? recperiodID)
        {
            List<long> oAccountIDList = new List<long>();
            using (IDbConnection con = this.CreateConnection())
            {
                using (IDbCommand cmd = this.GetAccountsHaveDiffrentAttributeValueInFutureCommand(oAccountIDTable, recperiodID))
                {
                    cmd.Connection = con;
                    con.Open();
                    IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);                   
                    while (reader.Read())
                    {
                        long? ActID = reader.GetInt64Value("AccountID");
                        if (ActID.HasValue)
                            oAccountIDList.Add(ActID.Value);
                    }
                }
            }
            return oAccountIDList;
        }

    }
}