

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO.Base
{

    public abstract class AccountHdrDAOBase : CustomAbstractDAO<AccountHdrInfo>//Adapdev.Data.AbstractDAO<AccountHdrInfo> 
    {

        /// <summary>
        /// A static representation of column AccountID
        /// </summary>
        public static readonly string COLUMN_ACCOUNTID = "AccountID";
        /// <summary>
        /// A static representation of column AccountName
        /// </summary>
        public static readonly string COLUMN_ACCOUNTNAME = "AccountName";
        /// <summary>
        /// A static representation of column AccountNameLabelID
        /// </summary>
        public static readonly string COLUMN_ACCOUNTNAMELABELID = "AccountNameLabelID";
        /// <summary>
        /// A static representation of column AccountNumber
        /// </summary>
        public static readonly string COLUMN_ACCOUNTNUMBER = "AccountNumber";
        /// <summary>
        /// A static representation of column AccountPolicyUrl
        /// </summary>
        public static readonly string COLUMN_ACCOUNTPOLICYURL = "AccountPolicyUrl";
        /// <summary>
        /// A static representation of column AccountTypeID
        /// </summary>
        public static readonly string COLUMN_ACCOUNTTYPEID = "AccountTypeID";
        /// <summary>
        /// A static representation of column AddedBy
        /// </summary>
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
        /// <summary>
        /// A static representation of column ApproverUserID
        /// </summary>
        public static readonly string COLUMN_APPROVERUSERID = "ApproverUserID";
        /// <summary>
        /// A static representation of column DateAdded
        /// </summary>
        public static readonly string COLUMN_DATEADDED = "DateAdded";
        /// <summary>
        /// A static representation of column DateRevised
        /// </summary>
        public static readonly string COLUMN_DATEREVISED = "DateRevised";
        /// <summary>
        /// A static representation of column Description
        /// </summary>
        public static readonly string COLUMN_DESCRIPTION = "Description";
        /// <summary>
        /// A static representation of column DescriptionLabelID
        /// </summary>
        public static readonly string COLUMN_DESCRIPTIONLABELID = "DescriptionLabelID";
        /// <summary>
        /// A static representation of column FSCaptionID
        /// </summary>
        public static readonly string COLUMN_FSCAPTIONID = "FSCaptionID";
        /// <summary>
        /// A static representation of column GeographyObjectID
        /// </summary>
        public static readonly string COLUMN_GEOGRAPHYOBJECTID = "GeographyObjectID";
        /// <summary>
        /// A static representation of column HostName
        /// </summary>
        public static readonly string COLUMN_HOSTNAME = "HostName";
        /// <summary>
        /// A static representation of column IsActive
        /// </summary>
        public static readonly string COLUMN_ISACTIVE = "IsActive";
        /// <summary>
        /// A static representation of column IsKeyAccount
        /// </summary>
        public static readonly string COLUMN_ISKEYACCOUNT = "IsKeyAccount";
        /// <summary>
        /// A static representation of column IsZeroBalance
        /// </summary>
        public static readonly string COLUMN_ISZEROBALANCE = "IsZeroBalance";
        /// <summary>
        /// A static representation of column NatureOfAccount
        /// </summary>
        public static readonly string COLUMN_NATUREOFACCOUNT = "NatureOfAccount";
        /// <summary>
        /// A static representation of column NatureOfAccountLabelID
        /// </summary>
        public static readonly string COLUMN_NATUREOFACCOUNTLABELID = "NatureOfAccountLabelID";
        /// <summary>
        /// A static representation of column NetAccountID
        /// </summary>
        public static readonly string COLUMN_NETACCOUNTID = "NetAccountID";
        /// <summary>
        /// A static representation of column PreparerUserID
        /// </summary>
        public static readonly string COLUMN_PREPARERUSERID = "PreparerUserID";
        /// <summary>
        /// A static representation of column ReconciliationProcedure
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONPROCEDURE = "ReconciliationProcedure";
        /// <summary>
        /// A static representation of column ReconciliationProcedureLabelID
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONPROCEDURELABELID = "ReconciliationProcedureLabelID";
        /// <summary>
        /// A static representation of column ReconciliationTemplateID
        /// </summary>
        public static readonly string COLUMN_RECONCILIATIONTEMPLATEID = "ReconciliationTemplateID";
        /// <summary>
        /// A static representation of column ReviewerUserID
        /// </summary>
        public static readonly string COLUMN_REVIEWERUSERID = "ReviewerUserID";
        /// <summary>
        /// A static representation of column RevisedBy
        /// </summary>
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        /// <summary>
        /// A static representation of column RiskRatingID
        /// </summary>
        public static readonly string COLUMN_RISKRATINGID = "RiskRatingID";
        /// <summary>
        /// A static representation of column SubLedgerSourceID
        /// </summary>
        public static readonly string COLUMN_SUBLEDGERSOURCEID = "SubLedgerSourceID";
        /// <summary>
        /// Provides access to the name of the primary key column (AccountID)
        /// </summary>
        public static readonly string TABLE_PRIMARYKEY = "AccountID";

        /// <summary>
        /// Provides access to the name of the table
        /// </summary>
        public static readonly string TABLE_NAME = "AccountHdr";

        /// <summary>
        /// Provides access to the name of the database
        /// </summary>
        public static readonly string DATABASE_NAME = "SkyStemArt";

        /// <summary>
        ///  CurrentAppUserInfo  for further use
        /// </summary>
        public AppUserInfo CurrentAppUserInfo;
        /// <summary>
        /// Constructor
        /// </summary>
        public AccountHdrDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "AccountHdr", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo = oAppUserInfo;
        }

        /// <summary>
        /// Maps the IDataReader values to a AccountHdrInfo object
        /// </summary>
        /// <param name="r">The IDataReader to map</param>
        /// <returns>AccountHdrInfo</returns>
        protected override AccountHdrInfo MapObject(System.Data.IDataReader r)
        {

            AccountHdrInfo entity = new AccountHdrInfo();


            entity.AccountID = r.GetInt64Value("AccountID");
            entity.AccountNumber = r.GetStringValue("AccountNumber");
            entity.AccountName = r.GetStringValue("AccountName");
            entity.AccountNameLabelID = r.GetInt32Value("AccountNameLabelID");
            entity.Description = r.GetStringValue("Description");
            entity.DescriptionLabelID = r.GetInt32Value("DescriptionLabelID");
            entity.GeographyObjectID = r.GetInt32Value("GeographyObjectID");
            entity.AccountTypeID = r.GetInt16Value("AccountTypeID");
            entity.ReconciliationTemplateID = r.GetInt16Value("ReconciliationTemplateID");
            entity.IsKeyAccount = r.GetBooleanValue("IsKeyAccount");
            entity.IsZeroBalance = r.GetBooleanValue("IsZeroBalance");
            entity.FSCaptionID = r.GetInt32Value("FSCaptionID");
            entity.RiskRatingID = r.GetInt16Value("RiskRatingID");
            entity.SubLedgerSourceID = r.GetInt32Value("SubLedgerSourceID");
            entity.AccountPolicyUrl = r.GetStringValue("AccountPolicyUrl");
            entity.NatureOfAccount = r.GetStringValue("NatureOfAccount");
            entity.NatureOfAccountLabelID = r.GetInt32Value("NatureOfAccountLabelID");
            entity.ReconciliationProcedure = r.GetStringValue("ReconciliationProcedure");
            entity.ReconciliationProcedureLabelID = r.GetInt32Value("ReconciliationProcedureLabelID");
            entity.PreparerUserID = r.GetInt32Value("PreparerUserID");
            entity.ReviewerUserID = r.GetInt32Value("ReviewerUserID");
            entity.ApproverUserID = r.GetInt32Value("ApproverUserID");
            entity.NetAccountID = r.GetInt32Value("NetAccountID");
            entity.IsActive = r.GetBooleanValue("IsActive");
            entity.DateAdded = r.GetDateValue("DateAdded");
            entity.AddedBy = r.GetStringValue("AddedBy");
            entity.DateRevised = r.GetDateValue("DateRevised");
            entity.RevisedBy = r.GetStringValue("RevisedBy");
            entity.HostName = r.GetStringValue("HostName");

            return entity;
        }

        /// <summary>
        /// Creates the sql insert command, using the values from the passed
        /// in AccountHdrInfo object
        /// </summary>
        /// <param name="o">A AccountHdrInfo object, from which the insert values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateInsertCommand(AccountHdrInfo entity)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_AccountHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAccountName = cmd.CreateParameter();
            parAccountName.ParameterName = "@AccountName";
            if (!entity.IsAccountNameNull)
                parAccountName.Value = entity.AccountName;
            else
                parAccountName.Value = System.DBNull.Value;
            cmdParams.Add(parAccountName);

            System.Data.IDbDataParameter parAccountNameLabelID = cmd.CreateParameter();
            parAccountNameLabelID.ParameterName = "@AccountNameLabelID";
            if (!entity.IsAccountNameLabelIDNull)
                parAccountNameLabelID.Value = entity.AccountNameLabelID;
            else
                parAccountNameLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parAccountNameLabelID);

            System.Data.IDbDataParameter parAccountNumber = cmd.CreateParameter();
            parAccountNumber.ParameterName = "@AccountNumber";
            if (!entity.IsAccountNumberNull)
                parAccountNumber.Value = entity.AccountNumber;
            else
                parAccountNumber.Value = System.DBNull.Value;
            cmdParams.Add(parAccountNumber);

            System.Data.IDbDataParameter parAccountPolicyUrl = cmd.CreateParameter();
            parAccountPolicyUrl.ParameterName = "@AccountPolicyUrl";
            if (!entity.IsAccountPolicyUrlNull)
                parAccountPolicyUrl.Value = entity.AccountPolicyUrl;
            else
                parAccountPolicyUrl.Value = System.DBNull.Value;
            cmdParams.Add(parAccountPolicyUrl);

            System.Data.IDbDataParameter parAccountTypeID = cmd.CreateParameter();
            parAccountTypeID.ParameterName = "@AccountTypeID";
            if (!entity.IsAccountTypeIDNull)
                parAccountTypeID.Value = entity.AccountTypeID;
            else
                parAccountTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parAccountTypeID);

            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parApproverUserID = cmd.CreateParameter();
            parApproverUserID.ParameterName = "@ApproverUserID";
            if (!entity.IsApproverUserIDNull)
                parApproverUserID.Value = entity.ApproverUserID;
            else
                parApproverUserID.Value = System.DBNull.Value;
            cmdParams.Add(parApproverUserID);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (!entity.IsDateAddedNull)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (!entity.IsDateRevisedNull)
                parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parDescription = cmd.CreateParameter();
            parDescription.ParameterName = "@Description";
            if (!entity.IsDescriptionNull)
                parDescription.Value = entity.Description;
            else
                parDescription.Value = System.DBNull.Value;
            cmdParams.Add(parDescription);

            System.Data.IDbDataParameter parDescriptionLabelID = cmd.CreateParameter();
            parDescriptionLabelID.ParameterName = "@DescriptionLabelID";
            if (!entity.IsDescriptionLabelIDNull)
                parDescriptionLabelID.Value = entity.DescriptionLabelID;
            else
                parDescriptionLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parDescriptionLabelID);

            System.Data.IDbDataParameter parFSCaptionID = cmd.CreateParameter();
            parFSCaptionID.ParameterName = "@FSCaptionID";
            if (!entity.IsFSCaptionIDNull)
                parFSCaptionID.Value = entity.FSCaptionID;
            else
                parFSCaptionID.Value = System.DBNull.Value;
            cmdParams.Add(parFSCaptionID);

            System.Data.IDbDataParameter parGeographyObjectID = cmd.CreateParameter();
            parGeographyObjectID.ParameterName = "@GeographyObjectID";
            if (!entity.IsGeographyObjectIDNull)
                parGeographyObjectID.Value = entity.GeographyObjectID;
            else
                parGeographyObjectID.Value = System.DBNull.Value;
            cmdParams.Add(parGeographyObjectID);

            System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            parHostName.ParameterName = "@HostName";
            if (!entity.IsHostNameNull)
                parHostName.Value = entity.HostName;
            else
                parHostName.Value = System.DBNull.Value;
            cmdParams.Add(parHostName);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (!entity.IsIsActiveNull)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parIsKeyAccount = cmd.CreateParameter();
            parIsKeyAccount.ParameterName = "@IsKeyAccount";
            if (!entity.IsIsKeyAccountNull)
                parIsKeyAccount.Value = entity.IsKeyAccount;
            else
                parIsKeyAccount.Value = System.DBNull.Value;
            cmdParams.Add(parIsKeyAccount);

            System.Data.IDbDataParameter parIsZeroBalance = cmd.CreateParameter();
            parIsZeroBalance.ParameterName = "@IsZeroBalance";
            if (!entity.IsIsZeroBalanceNull)
                parIsZeroBalance.Value = entity.IsZeroBalance;
            else
                parIsZeroBalance.Value = System.DBNull.Value;
            cmdParams.Add(parIsZeroBalance);

            System.Data.IDbDataParameter parNatureOfAccount = cmd.CreateParameter();
            parNatureOfAccount.ParameterName = "@NatureOfAccount";
            if (!entity.IsNatureOfAccountNull)
                parNatureOfAccount.Value = entity.NatureOfAccount;
            else
                parNatureOfAccount.Value = System.DBNull.Value;
            cmdParams.Add(parNatureOfAccount);

            System.Data.IDbDataParameter parNatureOfAccountLabelID = cmd.CreateParameter();
            parNatureOfAccountLabelID.ParameterName = "@NatureOfAccountLabelID";
            if (!entity.IsNatureOfAccountLabelIDNull)
                parNatureOfAccountLabelID.Value = entity.NatureOfAccountLabelID;
            else
                parNatureOfAccountLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parNatureOfAccountLabelID);

            System.Data.IDbDataParameter parNetAccountID = cmd.CreateParameter();
            parNetAccountID.ParameterName = "@NetAccountID";
            if (!entity.IsNetAccountIDNull)
                parNetAccountID.Value = entity.NetAccountID;
            else
                parNetAccountID.Value = System.DBNull.Value;
            cmdParams.Add(parNetAccountID);

            System.Data.IDbDataParameter parPreparerUserID = cmd.CreateParameter();
            parPreparerUserID.ParameterName = "@PreparerUserID";
            if (!entity.IsPreparerUserIDNull)
                parPreparerUserID.Value = entity.PreparerUserID;
            else
                parPreparerUserID.Value = System.DBNull.Value;
            cmdParams.Add(parPreparerUserID);

            System.Data.IDbDataParameter parReconciliationProcedure = cmd.CreateParameter();
            parReconciliationProcedure.ParameterName = "@ReconciliationProcedure";
            if (!entity.IsReconciliationProcedureNull)
                parReconciliationProcedure.Value = entity.ReconciliationProcedure;
            else
                parReconciliationProcedure.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationProcedure);

            System.Data.IDbDataParameter parReconciliationProcedureLabelID = cmd.CreateParameter();
            parReconciliationProcedureLabelID.ParameterName = "@ReconciliationProcedureLabelID";
            if (!entity.IsReconciliationProcedureLabelIDNull)
                parReconciliationProcedureLabelID.Value = entity.ReconciliationProcedureLabelID;
            else
                parReconciliationProcedureLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationProcedureLabelID);

            System.Data.IDbDataParameter parReconciliationTemplateID = cmd.CreateParameter();
            parReconciliationTemplateID.ParameterName = "@ReconciliationTemplateID";
            if (!entity.IsReconciliationTemplateIDNull)
                parReconciliationTemplateID.Value = entity.ReconciliationTemplateID;
            else
                parReconciliationTemplateID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationTemplateID);

            System.Data.IDbDataParameter parReviewerUserID = cmd.CreateParameter();
            parReviewerUserID.ParameterName = "@ReviewerUserID";
            if (!entity.IsReviewerUserIDNull)
                parReviewerUserID.Value = entity.ReviewerUserID;
            else
                parReviewerUserID.Value = System.DBNull.Value;
            cmdParams.Add(parReviewerUserID);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parRiskRatingID = cmd.CreateParameter();
            parRiskRatingID.ParameterName = "@RiskRatingID";
            if (!entity.IsRiskRatingIDNull)
                parRiskRatingID.Value = entity.RiskRatingID;
            else
                parRiskRatingID.Value = System.DBNull.Value;
            cmdParams.Add(parRiskRatingID);

            System.Data.IDbDataParameter parSubLedgerSourceID = cmd.CreateParameter();
            parSubLedgerSourceID.ParameterName = "@SubLedgerSourceID";
            if (!entity.IsSubLedgerSourceIDNull)
                parSubLedgerSourceID.Value = entity.SubLedgerSourceID;
            else
                parSubLedgerSourceID.Value = System.DBNull.Value;
            cmdParams.Add(parSubLedgerSourceID);

            return cmd;

        }

        /// <summary>
        /// Creates the sql update command, using the values from the passed
        /// in AccountHdrInfo object
        /// </summary>
        /// <param name="o">A AccountHdrInfo object, from which the update values are pulled</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateUpdateCommand(AccountHdrInfo entity)
        {



            System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_AccountHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parAccountName = cmd.CreateParameter();
            parAccountName.ParameterName = "@AccountName";
            if (!entity.IsAccountNameNull)
                parAccountName.Value = entity.AccountName;
            else
                parAccountName.Value = System.DBNull.Value;
            cmdParams.Add(parAccountName);

            System.Data.IDbDataParameter parAccountNameLabelID = cmd.CreateParameter();
            parAccountNameLabelID.ParameterName = "@AccountNameLabelID";
            if (!entity.IsAccountNameLabelIDNull)
                parAccountNameLabelID.Value = entity.AccountNameLabelID;
            else
                parAccountNameLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parAccountNameLabelID);

            System.Data.IDbDataParameter parAccountNumber = cmd.CreateParameter();
            parAccountNumber.ParameterName = "@AccountNumber";
            if (!entity.IsAccountNumberNull)
                parAccountNumber.Value = entity.AccountNumber;
            else
                parAccountNumber.Value = System.DBNull.Value;
            cmdParams.Add(parAccountNumber);

            System.Data.IDbDataParameter parAccountPolicyUrl = cmd.CreateParameter();
            parAccountPolicyUrl.ParameterName = "@AccountPolicyUrl";
            if (!entity.IsAccountPolicyUrlNull)
                parAccountPolicyUrl.Value = entity.AccountPolicyUrl;
            else
                parAccountPolicyUrl.Value = System.DBNull.Value;
            cmdParams.Add(parAccountPolicyUrl);

            System.Data.IDbDataParameter parAccountTypeID = cmd.CreateParameter();
            parAccountTypeID.ParameterName = "@AccountTypeID";
            if (!entity.IsAccountTypeIDNull)
                parAccountTypeID.Value = entity.AccountTypeID;
            else
                parAccountTypeID.Value = System.DBNull.Value;
            cmdParams.Add(parAccountTypeID);

            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            if (!entity.IsAddedByNull)
                parAddedBy.Value = entity.AddedBy;
            else
                parAddedBy.Value = System.DBNull.Value;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parApproverUserID = cmd.CreateParameter();
            parApproverUserID.ParameterName = "@ApproverUserID";
            if (!entity.IsApproverUserIDNull)
                parApproverUserID.Value = entity.ApproverUserID;
            else
                parApproverUserID.Value = System.DBNull.Value;
            cmdParams.Add(parApproverUserID);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            if (!entity.IsDateAddedNull)
                parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateAdded.Value = System.DBNull.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            if (!entity.IsDateRevisedNull)
                parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            else
                parDateRevised.Value = System.DBNull.Value;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parDescription = cmd.CreateParameter();
            parDescription.ParameterName = "@Description";
            if (!entity.IsDescriptionNull)
                parDescription.Value = entity.Description;
            else
                parDescription.Value = System.DBNull.Value;
            cmdParams.Add(parDescription);

            System.Data.IDbDataParameter parDescriptionLabelID = cmd.CreateParameter();
            parDescriptionLabelID.ParameterName = "@DescriptionLabelID";
            if (!entity.IsDescriptionLabelIDNull)
                parDescriptionLabelID.Value = entity.DescriptionLabelID;
            else
                parDescriptionLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parDescriptionLabelID);

            System.Data.IDbDataParameter parFSCaptionID = cmd.CreateParameter();
            parFSCaptionID.ParameterName = "@FSCaptionID";
            if (!entity.IsFSCaptionIDNull)
                parFSCaptionID.Value = entity.FSCaptionID;
            else
                parFSCaptionID.Value = System.DBNull.Value;
            cmdParams.Add(parFSCaptionID);

            System.Data.IDbDataParameter parGeographyObjectID = cmd.CreateParameter();
            parGeographyObjectID.ParameterName = "@GeographyObjectID";
            if (!entity.IsGeographyObjectIDNull)
                parGeographyObjectID.Value = entity.GeographyObjectID;
            else
                parGeographyObjectID.Value = System.DBNull.Value;
            cmdParams.Add(parGeographyObjectID);

            System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
            parHostName.ParameterName = "@HostName";
            if (!entity.IsHostNameNull)
                parHostName.Value = entity.HostName;
            else
                parHostName.Value = System.DBNull.Value;
            cmdParams.Add(parHostName);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            if (!entity.IsIsActiveNull)
                parIsActive.Value = entity.IsActive;
            else
                parIsActive.Value = System.DBNull.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parIsKeyAccount = cmd.CreateParameter();
            parIsKeyAccount.ParameterName = "@IsKeyAccount";
            if (!entity.IsIsKeyAccountNull)
                parIsKeyAccount.Value = entity.IsKeyAccount;
            else
                parIsKeyAccount.Value = System.DBNull.Value;
            cmdParams.Add(parIsKeyAccount);

            System.Data.IDbDataParameter parIsZeroBalance = cmd.CreateParameter();
            parIsZeroBalance.ParameterName = "@IsZeroBalance";
            if (!entity.IsIsZeroBalanceNull)
                parIsZeroBalance.Value = entity.IsZeroBalance;
            else
                parIsZeroBalance.Value = System.DBNull.Value;
            cmdParams.Add(parIsZeroBalance);

            System.Data.IDbDataParameter parNatureOfAccount = cmd.CreateParameter();
            parNatureOfAccount.ParameterName = "@NatureOfAccount";
            if (!entity.IsNatureOfAccountNull)
                parNatureOfAccount.Value = entity.NatureOfAccount;
            else
                parNatureOfAccount.Value = System.DBNull.Value;
            cmdParams.Add(parNatureOfAccount);

            System.Data.IDbDataParameter parNatureOfAccountLabelID = cmd.CreateParameter();
            parNatureOfAccountLabelID.ParameterName = "@NatureOfAccountLabelID";
            if (!entity.IsNatureOfAccountLabelIDNull)
                parNatureOfAccountLabelID.Value = entity.NatureOfAccountLabelID;
            else
                parNatureOfAccountLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parNatureOfAccountLabelID);

            System.Data.IDbDataParameter parNetAccountID = cmd.CreateParameter();
            parNetAccountID.ParameterName = "@NetAccountID";
            if (!entity.IsNetAccountIDNull)
                parNetAccountID.Value = entity.NetAccountID;
            else
                parNetAccountID.Value = System.DBNull.Value;
            cmdParams.Add(parNetAccountID);

            System.Data.IDbDataParameter parPreparerUserID = cmd.CreateParameter();
            parPreparerUserID.ParameterName = "@PreparerUserID";
            if (!entity.IsPreparerUserIDNull)
                parPreparerUserID.Value = entity.PreparerUserID;
            else
                parPreparerUserID.Value = System.DBNull.Value;
            cmdParams.Add(parPreparerUserID);

            System.Data.IDbDataParameter parReconciliationProcedure = cmd.CreateParameter();
            parReconciliationProcedure.ParameterName = "@ReconciliationProcedure";
            if (!entity.IsReconciliationProcedureNull)
                parReconciliationProcedure.Value = entity.ReconciliationProcedure;
            else
                parReconciliationProcedure.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationProcedure);

            System.Data.IDbDataParameter parReconciliationProcedureLabelID = cmd.CreateParameter();
            parReconciliationProcedureLabelID.ParameterName = "@ReconciliationProcedureLabelID";
            if (!entity.IsReconciliationProcedureLabelIDNull)
                parReconciliationProcedureLabelID.Value = entity.ReconciliationProcedureLabelID;
            else
                parReconciliationProcedureLabelID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationProcedureLabelID);

            System.Data.IDbDataParameter parReconciliationTemplateID = cmd.CreateParameter();
            parReconciliationTemplateID.ParameterName = "@ReconciliationTemplateID";
            if (!entity.IsReconciliationTemplateIDNull)
                parReconciliationTemplateID.Value = entity.ReconciliationTemplateID;
            else
                parReconciliationTemplateID.Value = System.DBNull.Value;
            cmdParams.Add(parReconciliationTemplateID);

            System.Data.IDbDataParameter parReviewerUserID = cmd.CreateParameter();
            parReviewerUserID.ParameterName = "@ReviewerUserID";
            if (!entity.IsReviewerUserIDNull)
                parReviewerUserID.Value = entity.ReviewerUserID;
            else
                parReviewerUserID.Value = System.DBNull.Value;
            cmdParams.Add(parReviewerUserID);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            if (!entity.IsRevisedByNull)
                parRevisedBy.Value = entity.RevisedBy;
            else
                parRevisedBy.Value = System.DBNull.Value;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parRiskRatingID = cmd.CreateParameter();
            parRiskRatingID.ParameterName = "@RiskRatingID";
            if (!entity.IsRiskRatingIDNull)
                parRiskRatingID.Value = entity.RiskRatingID;
            else
                parRiskRatingID.Value = System.DBNull.Value;
            cmdParams.Add(parRiskRatingID);

            System.Data.IDbDataParameter parSubLedgerSourceID = cmd.CreateParameter();
            parSubLedgerSourceID.ParameterName = "@SubLedgerSourceID";
            if (!entity.IsSubLedgerSourceIDNull)
                parSubLedgerSourceID.Value = entity.SubLedgerSourceID;
            else
                parSubLedgerSourceID.Value = System.DBNull.Value;
            cmdParams.Add(parSubLedgerSourceID);

            System.Data.IDbDataParameter pkparAccountID = cmd.CreateParameter();
            pkparAccountID.ParameterName = "@AccountID";
            pkparAccountID.Value = entity.AccountID;
            cmdParams.Add(pkparAccountID);


            return cmd;

        }

        /// <summary>
        /// Creates the sql delete command, using the passed in primary key
        /// </summary>
        /// <param name="id">The primary key of the object to delete</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_AccountHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AccountID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }


        /// <summary>
        /// Creates the sql select command, using the passed in primary key
        /// </summary>
        /// <param name="o">The primary key of the object to select</param>
        /// <returns>An IDbCommand</returns>
        protected override System.Data.IDbCommand CreateSelectOneCommand(object id)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_AccountHdr");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AccountID";
            par.Value = id;
            cmdParams.Add(par);

            return cmd;

        }


        /// <summary>
        /// Creates the sql select command, using the passed in foreign key.  This will return an
        /// IList of all objects that have that foreign key.
        /// </summary>
        /// <param name="o">The foreign key of the objects to select</param>
        /// <returns>An IList</returns>
        public IList<AccountHdrInfo> SelectAllByGeographyObjectID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_AccountHdrByGeographyObjectID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GeographyObjectID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }


        /// <summary>
        /// Creates the sql select command, using the passed in foreign key.  This will return an
        /// IList of all objects that have that foreign key.
        /// </summary>
        /// <param name="o">The foreign key of the objects to select</param>
        /// <returns>An IList</returns>
        public IList<AccountHdrInfo> SelectAllByAccountTypeID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_AccountHdrByAccountTypeID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@AccountTypeID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }


        /// <summary>
        /// Creates the sql select command, using the passed in foreign key.  This will return an
        /// IList of all objects that have that foreign key.
        /// </summary>
        /// <param name="o">The foreign key of the objects to select</param>
        /// <returns>An IList</returns>
        public IList<AccountHdrInfo> SelectAllByReconciliationTemplateID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_AccountHdrByReconciliationTemplateID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationTemplateID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }


        /// <summary>
        /// Creates the sql select command, using the passed in foreign key.  This will return an
        /// IList of all objects that have that foreign key.
        /// </summary>
        /// <param name="o">The foreign key of the objects to select</param>
        /// <returns>An IList</returns>
        public IList<AccountHdrInfo> SelectAllByFSCaptionID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_AccountHdrByFSCaptionID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@FSCaptionID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }


        /// <summary>
        /// Creates the sql select command, using the passed in foreign key.  This will return an
        /// IList of all objects that have that foreign key.
        /// </summary>
        /// <param name="o">The foreign key of the objects to select</param>
        /// <returns>An IList</returns>
        public IList<AccountHdrInfo> SelectAllByRiskRatingID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_AccountHdrByRiskRatingID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RiskRatingID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }


        /// <summary>
        /// Creates the sql select command, using the passed in foreign key.  This will return an
        /// IList of all objects that have that foreign key.
        /// </summary>
        /// <param name="o">The foreign key of the objects to select</param>
        /// <returns>An IList</returns>
        public IList<AccountHdrInfo> SelectAllBySubLedgerSourceID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_AccountHdrBySubLedgerSourceID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@SubLedgerSourceID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }


        /// <summary>
        /// Creates the sql select command, using the passed in foreign key.  This will return an
        /// IList of all objects that have that foreign key.
        /// </summary>
        /// <param name="o">The foreign key of the objects to select</param>
        /// <returns>An IList</returns>
        public IList<AccountHdrInfo> SelectAllByNetAccountID(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_AccountHdrByNetAccountID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@NetAccountID";
            par.Value = id;
            cmdParams.Add(par);

            return this.Select(cmd);
        }

        private void MapIdentity(AccountHdrInfo entity, object id)
        {
            entity.AccountID = Convert.ToInt64(id);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<AccountHdrInfo> SelectAccountHdrDetailsAssociatedToReconciliationPeriodByAccountReconciliationPeriod(ReconciliationPeriodInfo entity)
        {
            return this.SelectAccountHdrDetailsAssociatedToReconciliationPeriodByAccountReconciliationPeriod(entity.ReconciliationPeriodID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<AccountHdrInfo> SelectAccountHdrDetailsAssociatedToReconciliationPeriodByAccountReconciliationPeriod(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [AccountHdr] INNER JOIN [AccountReconciliationPeriod] ON [AccountHdr].[AccountID] = [AccountReconciliationPeriod].[AccountID] INNER JOIN [ReconciliationPeriod] ON [AccountReconciliationPeriod].[ReconciliationPeriodID] = [ReconciliationPeriod].[ReconciliationPeriodID]  WHERE  [ReconciliationPeriod].[ReconciliationPeriodID] = @ReconciliationPeriodID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationPeriodID";
            par.Value = id;

            cmdParams.Add(par);
            List<AccountHdrInfo> objAccountHdrEntityColl = new List<AccountHdrInfo>(this.Select(cmd));
            return objAccountHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<AccountHdrInfo> SelectAccountHdrDetailsAssociatedToReconciliationStatusMstByGLDataHdr(ReconciliationStatusMstInfo entity)
        {
            return this.SelectAccountHdrDetailsAssociatedToReconciliationStatusMstByGLDataHdr(entity.ReconciliationStatusID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<AccountHdrInfo> SelectAccountHdrDetailsAssociatedToReconciliationStatusMstByGLDataHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [AccountHdr] INNER JOIN [GLDataHdr] ON [AccountHdr].[AccountID] = [GLDataHdr].[AccountID] INNER JOIN [ReconciliationStatusMst] ON [GLDataHdr].[ReconciliationStatusID] = [ReconciliationStatusMst].[ReconciliationStatusID]  WHERE  [ReconciliationStatusMst].[ReconciliationStatusID] = @ReconciliationStatusID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationStatusID";
            par.Value = id;

            cmdParams.Add(par);
            List<AccountHdrInfo> objAccountHdrEntityColl = new List<AccountHdrInfo>(this.Select(cmd));
            return objAccountHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<AccountHdrInfo> SelectAccountHdrDetailsAssociatedToCertificationStatusMstByGLDataHdr(CertificationStatusMstInfo entity)
        {
            return this.SelectAccountHdrDetailsAssociatedToCertificationStatusMstByGLDataHdr(entity.CertificationStatusID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<AccountHdrInfo> SelectAccountHdrDetailsAssociatedToCertificationStatusMstByGLDataHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [AccountHdr] INNER JOIN [GLDataHdr] ON [AccountHdr].[AccountID] = [GLDataHdr].[AccountID] INNER JOIN [CertificationStatusMst] ON [GLDataHdr].[CertificationStatusID] = [CertificationStatusMst].[CertificationStatusID]  WHERE  [CertificationStatusMst].[CertificationStatusID] = @CertificationStatusID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CertificationStatusID";
            par.Value = id;

            cmdParams.Add(par);
            List<AccountHdrInfo> objAccountHdrEntityColl = new List<AccountHdrInfo>(this.Select(cmd));
            return objAccountHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<AccountHdrInfo> SelectAccountHdrDetailsAssociatedToDataImportHdrByGLDataHdr(DataImportHdrInfo entity)
        {
            return this.SelectAccountHdrDetailsAssociatedToDataImportHdrByGLDataHdr(entity.DataImportID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<AccountHdrInfo> SelectAccountHdrDetailsAssociatedToDataImportHdrByGLDataHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [AccountHdr] INNER JOIN [GLDataHdr] ON [AccountHdr].[AccountID] = [GLDataHdr].[AccountID] INNER JOIN [DataImportHdr] ON [GLDataHdr].[DataImportID] = [DataImportHdr].[DataImportID]  WHERE  [DataImportHdr].[DataImportID] = @DataImportID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@DataImportID";
            par.Value = id;

            cmdParams.Add(par);
            List<AccountHdrInfo> objAccountHdrEntityColl = new List<AccountHdrInfo>(this.Select(cmd));
            return objAccountHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<AccountHdrInfo> SelectAccountHdrDetailsAssociatedToReconciliationPeriodByGLDataHdr(ReconciliationPeriodInfo entity)
        {
            return this.SelectAccountHdrDetailsAssociatedToReconciliationPeriodByGLDataHdr(entity.ReconciliationPeriodID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<AccountHdrInfo> SelectAccountHdrDetailsAssociatedToReconciliationPeriodByGLDataHdr(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [AccountHdr] INNER JOIN [GLDataHdr] ON [AccountHdr].[AccountID] = [GLDataHdr].[AccountID] INNER JOIN [ReconciliationPeriod] ON [GLDataHdr].[ReconciliationPeriodID] = [ReconciliationPeriod].[ReconciliationPeriodID]  WHERE  [ReconciliationPeriod].[ReconciliationPeriodID] = @ReconciliationPeriodID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@ReconciliationPeriodID";
            par.Value = id;

            cmdParams.Add(par);
            List<AccountHdrInfo> objAccountHdrEntityColl = new List<AccountHdrInfo>(this.Select(cmd));
            return objAccountHdrEntityColl;
        }




        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<AccountHdrInfo> SelectAccountHdrDetailsAssociatedToGLDataHdrBySubledgerData(GLDataHdrInfo entity)
        {
            return this.SelectAccountHdrDetailsAssociatedToGLDataHdrBySubledgerData(entity.GLDataID);
        }

        /// <summary>
        /// Creates the Entity Collection of table related to present table by n-n relationship
        /// </summary>
        /// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
        /// <returns>A collection of related entities</returns>
        public IList<AccountHdrInfo> SelectAccountHdrDetailsAssociatedToGLDataHdrBySubledgerData(object id)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [AccountHdr] INNER JOIN [SubledgerData] ON [AccountHdr].[AccountID] = [SubledgerData].[AccountID] INNER JOIN [GLDataHdr] ON [SubledgerData].[GLDataID] = [GLDataHdr].[GLDataID]  WHERE  [GLDataHdr].[GLDataID] = @GLDataID ");
            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataID";
            par.Value = id;

            cmdParams.Add(par);
            List<AccountHdrInfo> objAccountHdrEntityColl = new List<AccountHdrInfo>(this.Select(cmd));
            return objAccountHdrEntityColl;
        }

    }
}
